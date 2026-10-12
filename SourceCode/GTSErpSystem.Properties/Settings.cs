using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GTSErpSystem.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.14.0.0")]
internal sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[SpecialSetting(SpecialSetting.ConnectionString)]
	[DefaultSettingValue("Data Source=DESKTOP-DVLU8RG\\SQL2014;Initial Catalog=GTSdb;Integrated Security=True;Encrypt=False")]
	public string GTSdbConnectionString => (string)this["GTSdbConnectionString"];
}


