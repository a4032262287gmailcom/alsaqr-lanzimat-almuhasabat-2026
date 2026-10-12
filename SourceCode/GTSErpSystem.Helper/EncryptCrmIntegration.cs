using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace GTSErpSystem.Helper;

public static class EncryptCrmIntegration
{
	public static void CreateOrUpdateFile(string crmUrl)
	{
		string plainText = JsonConvert.SerializeObject(new
		{
			Crm = crmUrl
		}, Formatting.Indented);
		string contents = Encrypt(plainText, "MyStrongPassword123");
		string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
		Directory.CreateDirectory(text);
		string path = Path.Combine(text, "CrmIntegration.enc");
		File.WriteAllText(path, contents);
	}

	private static string Encrypt(string plainText, string password)
	{
		byte[] bytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("MySalt123"), 10000).GetBytes(32);
		using Aes aes = Aes.Create();
		aes.Key = bytes;
		aes.GenerateIV();
		using MemoryStream memoryStream = new MemoryStream();
		memoryStream.Write(aes.IV, 0, aes.IV.Length);
		using (CryptoStream stream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
		{
			using StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.Write(plainText);
		}
		return Convert.ToBase64String(memoryStream.ToArray());
	}
}


