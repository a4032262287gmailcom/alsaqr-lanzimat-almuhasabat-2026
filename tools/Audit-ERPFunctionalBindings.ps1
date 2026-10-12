[CmdletBinding()]
param(
    [string]$ProjectRoot = (Join-Path $PSScriptRoot '..\SourceCode'),
    [string]$ReportRoot = (Join-Path (Split-Path -Parent $PSScriptRoot) 'artifacts\EventAudit')
)

$ErrorActionPreference = 'Stop'

function Write-Stage([string]$Message) {
    Write-Host ("`n=== {0} ===" -f $Message) -ForegroundColor Cyan
}

function Get-FormFamily([System.IO.FileInfo]$File) {
    $n = $File.Name -replace '\.Designer\.cs$', '' -replace '\.cs$', ''
    if ($n -match '^([^\.]+)\.') { $n = $Matches[1] }
    return $n
}

function Find-CSharpClosingBrace([string]$Text, [int]$OpenIndex) {
    $depth = 0
    $state = 'code'
    $verbatim = $false
    $escape = $false
    for ($i = $OpenIndex; $i -lt $Text.Length; $i++) {
        $ch = $Text[$i]
        $next = [char]0
        if ($i + 1 -lt $Text.Length) { $next = $Text[$i + 1] }
        switch ($state) {
            'code' {
                if ($ch -eq '/' -and $next -eq '/') { $state = 'linecomment'; $i++; continue }
                if ($ch -eq '/' -and $next -eq '*') { $state = 'blockcomment'; $i++; continue }
                if ($ch -eq [char]34) {
                    $verbatim = ($i -gt 0 -and $Text[$i - 1] -eq '@')
                    $state = 'string'
                    $escape = $false
                    continue
                }
                if ($ch -eq [char]39) { $state = 'char'; $escape = $false; continue }
                if ($ch -eq '{') { $depth++; continue }
                if ($ch -eq '}') { $depth--; if ($depth -eq 0) { return $i }; continue }
            }
            'linecomment' { if ($ch -eq "`n") { $state = 'code' }; continue }
            'blockcomment' { if ($ch -eq '*' -and $next -eq '/') { $state = 'code'; $i++ }; continue }
            'string' {
                if ($verbatim) {
                    if ($ch -eq [char]34 -and $next -eq [char]34) { $i++; continue }
                    if ($ch -eq [char]34) { $state = 'code'; continue }
                } else {
                    if ($escape) { $escape = $false; continue }
                    if ($ch -eq '\') { $escape = $true; continue }
                    if ($ch -eq [char]34) { $state = 'code'; continue }
                }
                continue
            }
            'char' {
                if ($escape) { $escape = $false; continue }
                if ($ch -eq '\') { $escape = $true; continue }
                if ($ch -eq [char]39) { $state = 'code'; continue }
            }
        }
    }
    return -1
}

Write-Stage 'Checking project path'
if (-not (Test-Path -LiteralPath $ProjectRoot -PathType Container)) {
    throw ('Project folder not found: ' + $ProjectRoot)
}

$root = (Resolve-Path -LiteralPath $ProjectRoot).Path.TrimEnd('\')
$stamp = Get-Date -Format 'yyyyMMdd_HHmmss'
$reportRoot = Join-Path $ReportRoot $stamp
New-Item -ItemType Directory -Path $reportRoot -Force | Out-Null
$skipPattern = '[\\/](bin|obj|packages|\.vs|\.git|Backups?|node_modules|TestResults|publish|Debug|Release)[\\/]'
$files = @(Get-ChildItem -LiteralPath $root -Recurse -File -Filter '*.cs' -ErrorAction SilentlyContinue | Where-Object { $_.FullName -notmatch $skipPattern })
Write-Host ('C# source files discovered: {0}' -f $files.Count)

$methodPattern = [regex]'(?m)^[\t ]*(?:(?:public|private|protected|internal|static|virtual|override|async|new|sealed|extern|unsafe|partial|readonly)\s+)*(?:[A-Za-z_][\w.<>,?\[\]]*\s+)+(?<name>[A-Za-z_]\w*)\s*\([^;{}]*\)\s*(?:where\s+\w+[^{}]*)?\{'
$eventPattern = [regex]'(?<control>(?:this\.)?[A-Za-z_]\w*)\s*\.\s*(?<event>Click|ItemClick|DoubleClick|MouseClick|SelectedIndexChanged|SelectedValueChanged|CheckedChanged|TextChanged|FormClosing|FormClosed|Load|KeyDown|KeyUp|CellClick|RowClick|ButtonClick|EditValueChanged|ValueChanged|DateChanged)\s*\+=\s*(?:new\s+[\w.<>]+\s*\(\s*(?:this\.)?(?<h1>[A-Za-z_]\w*)\s*\)|(?:this\.)?(?<h2>[A-Za-z_]\w*)\s*;)' 
$methodMap = @{}
$allMethods = New-Object System.Collections.ArrayList
$hooks = New-Object System.Collections.ArrayList
$contents = @{}

foreach ($file in $files) {
    try { $txt = [System.IO.File]::ReadAllText($file.FullName) } catch { continue }
    $contents[$file.FullName] = $txt
    $family = Get-FormFamily $file
    foreach ($m in $methodPattern.Matches($txt)) {
        $open = $m.Index + $m.Length - 1
        $close = Find-CSharpClosingBrace $txt $open
        if ($close -lt 0) { continue }
        $body = $txt.Substring($open + 1, $close - $open - 1)
        $line = ([regex]::Matches($txt.Substring(0, $m.Index), "`n")).Count + 1
        $rec = [pscustomobject]@{ File=$file.FullName; Family=$family; Method=$m.Groups['name'].Value; Line=$line; Body=$body }
        [void]$allMethods.Add($rec)
        $key = $family + '|' + $rec.Method
        if (-not $methodMap.ContainsKey($key)) { $methodMap[$key] = New-Object System.Collections.ArrayList }
        [void]$methodMap[$key].Add($rec)
    }
    foreach ($h in $eventPattern.Matches($txt)) {
        $handler = if ($h.Groups['h1'].Success) { $h.Groups['h1'].Value } else { $h.Groups['h2'].Value }
        $control = $h.Groups['control'].Value -replace '^this\.', ''
        $line = ([regex]::Matches($txt.Substring(0, $h.Index), "`n")).Count + 1
        [void]$hooks.Add([pscustomobject]@{ File=$file.FullName; Family=$family; Line=$line; Control=$control; Event=$h.Groups['event'].Value; Handler=$handler })
    }
}

$stubs = New-Object System.Collections.ArrayList
$missing = New-Object System.Collections.ArrayList
foreach ($h in $hooks) {
    $key = $h.Family + '|' + $h.Handler
    if (-not $methodMap.ContainsKey($key)) {
        $globalMatch = @($allMethods | Where-Object { $_.Method -eq $h.Handler }).Count
        $status = if ($globalMatch -gt 0) { 'Definition found outside same filename family; inspect inheritance/partial-class layout' } else { 'No method definition found by scanner' }
        [void]$missing.Add([pscustomobject]@{ File=$h.File; Family=$h.Family; Line=$h.Line; Control=$h.Control; Event=$h.Event; Handler=$h.Handler; GlobalDefinitions=$globalMatch; Status=$status })
        continue
    }
    foreach ($m in $methodMap[$key]) {
        $clean = [regex]::Replace($m.Body, '(?s)/\*.*?\*/|//[^\r\n]*', '').Trim()
        $kind = $null
        if ([string]::IsNullOrWhiteSpace($clean) -or $clean -match '^return\s*;?$') { $kind = 'EmptyOrImmediateReturn' }
        elseif ($clean -match '(?i)NotImplementedException|NotSupportedException') { $kind = 'ThrowsNotImplementedOrNotSupported' }
        elseif ($clean -match '(?i)TODO|FIXME|throw\s+new\s+NotImplemented') { $kind = 'ContainsTODOorFIXME' }
        if ($kind) {
            $flat = $clean -replace '\s+', ' '
            $preview = $flat.Substring(0, [Math]::Min(240, $flat.Length))
            [void]$stubs.Add([pscustomobject]@{ File=$m.File; Family=$m.Family; Line=$m.Line; Handler=$m.Method; Kind=$kind; BodyPreview=$preview })
        }
    }
}

$hooks | Export-Csv -LiteralPath (Join-Path $reportRoot 'NamedEventBindings.csv') -NoTypeInformation -Encoding UTF8
$stubs | Export-Csv -LiteralPath (Join-Path $reportRoot 'HandlersThatLookIncomplete.csv') -NoTypeInformation -Encoding UTF8
$missing | Export-Csv -LiteralPath (Join-Path $reportRoot 'HandlersOutsideFormFamilyOrMissing.csv') -NoTypeInformation -Encoding UTF8

$summaryLines = @(
    '# KhaledERP Functional Event Handler Audit',
    '',
    ('- Project: ' + $root),
    ('- Run time: ' + (Get-Date -Format 'yyyy-MM-dd HH:mm:ss')),
    ('- C# source files scanned: ' + $files.Count),
    ('- Named event hookup statements: ' + $hooks.Count),
    ('- Handler definitions that look empty/placeholders: ' + $stubs.Count),
    ('- Handler references not found in same filename family: ' + $missing.Count),
    '',
    'Static heuristic only. Findings are review leads, not proof of defects. Lambdas, inherited handlers, partial classes with nonstandard file names, reflection-based routes, and runtime behavior may not be fully represented.',
    'No project source files or database content were modified.',
    '',
    'CSV reports: NamedEventBindings.csv; HandlersThatLookIncomplete.csv; HandlersOutsideFormFamilyOrMissing.csv'
)
$summaryPath = Join-Path $reportRoot 'SUMMARY.md'
[System.IO.File]::WriteAllText($summaryPath, ($summaryLines -join [Environment]::NewLine), (New-Object System.Text.UTF8Encoding($true)))

Write-Stage 'Audit summary'
Write-Host ('C# source files scanned: {0}' -f $files.Count)
Write-Host ('Named event bindings: {0}' -f $hooks.Count)
Write-Host ('Handlers that look empty/incomplete: {0}' -f $stubs.Count) -ForegroundColor Yellow
Write-Host ('Handler references not found in same filename family: {0}' -f $missing.Count) -ForegroundColor Yellow
if ($stubs.Count -gt 0) {
    Write-Host "`nFirst 15 possibly incomplete handlers:" -ForegroundColor Cyan
    $stubs | Select-Object -First 15 | ForEach-Object { Write-Host ('{0}:{1} | {2} | {3}' -f $_.File, $_.Line, $_.Handler, $_.Kind) }
}
if ($missing.Count -gt 0) {
    Write-Host "`nFirst 15 handler references to review:" -ForegroundColor Cyan
    $missing | Select-Object -First 15 | ForEach-Object { Write-Host ('{0}:{1} | {2}.{3} => {4} | {5}' -f $_.File, $_.Line, $_.Control, $_.Event, $_.Handler, $_.Status) }
}
Write-Host ('Detailed report: ' + $reportRoot) -ForegroundColor Green
Write-Host 'Read-only scan. No project files were changed.' -ForegroundColor Yellow