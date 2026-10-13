#requires -Version 5.1
[CmdletBinding()]
param(
    [switch]$Build
)

$ErrorActionPreference = "Stop"
$RepoRoot = Split-Path -Parent $PSScriptRoot
$SolutionPath = Join-Path $RepoRoot "AlsaqrERP.sln"
$ProjectPath = Join-Path $RepoRoot "SourceCode\GTSErpSystem.csproj"
$ProjectDir = Split-Path -Parent $ProjectPath

function Assert-True {
    param([bool]$Condition, [string]$Message)
    if (-not $Condition) { throw $Message }
    Write-Host "[OK] $Message" -ForegroundColor Green
}

Assert-True (Test-Path $SolutionPath) "ملف الحل AlsaqrERP.sln موجود"
Assert-True (Test-Path $ProjectPath) "ملف المشروع GTSErpSystem.csproj موجود"

$SolutionText = Get-Content -LiteralPath $SolutionPath -Raw
Assert-True ($SolutionText.Contains("SourceCode\GTSErpSystem.csproj")) "الحل يشير إلى المشروع الموجود"

[xml]$ProjectXml = Get-Content -LiteralPath $ProjectPath -Raw
$DependencyRoot = [System.IO.Path]::GetFullPath(
    (Join-Path $ProjectDir "..\App\SourceCode\bin\Debug\net48")
)
Assert-True (Test-Path $DependencyRoot) "مجلد الاعتماديات المضمن موجود"

$ReferenceErrors = New-Object System.Collections.Generic.List[string]
$RuntimePrefix = '$(RuntimeDependencyRoot)\'
$HintNodes = $ProjectXml.SelectNodes("//HintPath")
foreach ($Node in $HintNodes) {
    $Hint = $Node.InnerText.Trim()
    if ([string]::IsNullOrWhiteSpace($Hint)) { continue }

    if ($Hint.StartsWith($RuntimePrefix, [System.StringComparison]::OrdinalIgnoreCase)) {
        $Relative = $Hint.Substring($RuntimePrefix.Length)
        $Resolved = Join-Path $DependencyRoot $Relative
    } elseif ([System.IO.Path]::IsPathRooted($Hint)) {
        $Resolved = $Hint
    } else {
        $Resolved = Join-Path $ProjectDir $Hint
    }

    if (-not (Test-Path -LiteralPath $Resolved -PathType Leaf)) {
        $ReferenceErrors.Add("$Hint => $Resolved")
    }
}

Assert-True ($ReferenceErrors.Count -eq 0) "مسارات HintPath صالحة ($($HintNodes.Count) مرجعًا)"
$EfProviderPath = Join-Path $ProjectDir "Libs\EF6\EntityFramework.SqlServer.dll"
Assert-True (Test-Path $EfProviderPath -PathType Leaf) "موفر EF6 المحلي موجود"

$ProgramPath = Join-Path $ProjectDir "GTSErpSystem\Program.cs"
$LoginPath = Join-Path $ProjectDir "GTSErpSystem\FrmLogin3.cs"
$ConnectionPath = Join-Path $ProjectDir "GTSErpSystem\FrmSqlConnection.cs"
$PermissionPath = Join-Path $ProjectDir "GTSErpSystem.BLL.Security\CheckPrvlg.cs"
$LauncherPath = Join-Path $ProjectDir "GTSErpSystem\FrmMainUnified.cs"

foreach ($Path in @($ProgramPath, $LoginPath, $ConnectionPath, $PermissionPath, $LauncherPath)) {
    Assert-True (Test-Path $Path -PathType Leaf) ("ملف الإصلاح موجود: " + (Split-Path $Path -Leaf))
}

$ProgramText = Get-Content -LiteralPath $ProgramPath -Raw
$LoginText = Get-Content -LiteralPath $LoginPath -Raw
$ConnectionText = Get-Content -LiteralPath $ConnectionPath -Raw
$PermissionText = Get-Content -LiteralPath $PermissionPath -Raw
$LauncherText = Get-Content -LiteralPath $LauncherPath -Raw

Assert-True ($ProgramText.Contains("GetSqlConnectionFromFile()")) "بدء التشغيل يقرأ إعداد ERP.xml المحفوظ"
Assert-True (-not $ProgramText.Contains('SaveConnectionString("GTSdbEntities", "Data Source=.\SQLEXPRESS')) "لا يوجد تجاوز ثابت لسلسلة اتصال البداية"
Assert-True ($LoginText.Contains("EntityConnectionStringBuilder(configured.ConnectionString)")) "شاشة الدخول تدعم الرجوع إلى إعداد app.config"
Assert-True ($ConnectionText.Contains("if (!InsertDataInToXml())")) "الحفظ لا يعلن النجاح إذا فشل حفظ XML"
Assert-True ($ConnectionText.Contains("Application.ExecutablePath")) "إعادة التشغيل تستخدم الملف التنفيذي الحالي"
Assert-True ($PermissionText.Contains("GetAllowedScreenIds")) "طبقة الصلاحيات توفر معرّفات الدخول المسموحة"
Assert-True ($LauncherText.Contains("PermissionChecker.CheckUserPage")) "الداشبورد يعيد فحص الصلاحية عند فتح الشاشة"
$ScreenMapCount = [regex]::Matches($LauncherText, '\{\s*"[^"]+",\s*new\[\]\s*\{[^}]+\}\s*\}').Count
Assert-True ($ScreenMapCount -eq 69) "خريطة التنقل تحتوي 69 ربط شاشة موثقًا"

Write-Host ([Environment]::NewLine + "نجحت الفحوصات الساكنة. هذه الفحوصات لا تُعد بديلًا عن بناء المشروع أو الاتصال بقاعدة البيانات.") -ForegroundColor Cyan

if ($Build) {
    $Dotnet = Get-Command dotnet -ErrorAction SilentlyContinue
    $MsBuild = Get-Command msbuild.exe -ErrorAction SilentlyContinue

    if (-not $Dotnet -and -not $MsBuild) {
        throw "لم يتم العثور على dotnet أو msbuild.exe. افتح Developer PowerShell الخاص بـ Visual Studio أو ثبّت أدوات بناء .NET."
    }

    if ($Dotnet) {
        $Exe = $Dotnet.Source
        $Arguments = @("build", $SolutionPath, "--configuration", "Release", "--no-incremental")
    } else {
        $Exe = $MsBuild.Source
        $Arguments = @($SolutionPath, "/restore", "/p:Configuration=Release")
    }

    Write-Host ([Environment]::NewLine + "بدء بناء Release...") -ForegroundColor Cyan
    $PreviousPreference = $ErrorActionPreference
    try {
        $ErrorActionPreference = "Continue"
        $BuildOutput = @(& $Exe @Arguments 2>&1)
        $BuildCode = $LASTEXITCODE
    }
    finally {
        $ErrorActionPreference = $PreviousPreference
    }

    $BuildOutput | ForEach-Object { Write-Host "$_" }
    if ($BuildCode -ne 0) {
        throw "فشل البناء (Exit code: $BuildCode). راجع أول خطأ C# أو اعتماديات يظهر في المخرجات."
    }

    Write-Host ([Environment]::NewLine + "نجح بناء Release. لم يجر هذا السكربت أي تعديل على قاعدة البيانات.") -ForegroundColor Green
}
