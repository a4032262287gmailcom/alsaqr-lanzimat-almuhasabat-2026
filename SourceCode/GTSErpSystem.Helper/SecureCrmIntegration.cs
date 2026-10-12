using System;
using System.IO;
using Newtonsoft.Json;

namespace GTSErpSystem.Helper;

public static class SecureCrmIntegration
{
	private static readonly string password = "MyStrongPassword123";

	public static string Crm { get; private set; }

	public static void Load()
	{
		string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "CrmIntegration.json");
		if (!File.Exists(path))
		{
			Crm = string.Empty;
			return;
		}
		string value = File.ReadAllText(path);
		dynamic val = JsonConvert.DeserializeObject<object>(value);
		Crm = val.Crm;
	}
}


