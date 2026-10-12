using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GTSErpSystem.BLL.Main;

internal class Class_Encryption
{
	private const string initVector = "pemgail9uzpgzl88";

	private const int keysize = 256;

	public static string Encrypt(string clearText)
	{
		string password = "abc123";
		byte[] bytes = Encoding.Unicode.GetBytes(clearText);
		using (Aes aes = Aes.Create())
		{
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
			{
				73, 118, 97, 110, 32, 77, 101, 100, 118, 101,
				100, 101, 118
			});
			aes.Key = rfc2898DeriveBytes.GetBytes(32);
			aes.IV = rfc2898DeriveBytes.GetBytes(16);
			using MemoryStream memoryStream = new MemoryStream();
			using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
			{
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.Close();
			}
			clearText = Convert.ToBase64String(memoryStream.ToArray());
		}
		return clearText;
	}

	public static string Decrypt(string cipherText)
	{
		string password = "abc123";
		cipherText = cipherText.Replace(" ", "+");
		byte[] array = Convert.FromBase64String(cipherText);
		using (Aes aes = Aes.Create())
		{
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
			{
				73, 118, 97, 110, 32, 77, 101, 100, 118, 101,
				100, 101, 118
			});
			aes.Key = rfc2898DeriveBytes.GetBytes(32);
			aes.IV = rfc2898DeriveBytes.GetBytes(16);
			using MemoryStream memoryStream = new MemoryStream();
			using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
			{
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.Close();
			}
			cipherText = Encoding.Unicode.GetString(memoryStream.ToArray());
		}
		return cipherText;
	}

	public static string EncryptString(string plainText, string passPhrase)
	{
		byte[] bytes = Encoding.UTF8.GetBytes("pemgail9uzpgzl88");
		byte[] bytes2 = Encoding.UTF8.GetBytes(plainText);
		PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, null);
		byte[] bytes3 = passwordDeriveBytes.GetBytes(32);
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.Mode = CipherMode.CBC;
		ICryptoTransform transform = rijndaelManaged.CreateEncryptor(bytes3, bytes);
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
		cryptoStream.Write(bytes2, 0, bytes2.Length);
		cryptoStream.FlushFinalBlock();
		byte[] inArray = memoryStream.ToArray();
		memoryStream.Close();
		cryptoStream.Close();
		return Convert.ToBase64String(inArray);
	}

	public static string DecryptString(string cipherText, string passPhrase)
	{
		byte[] bytes = Encoding.UTF8.GetBytes("pemgail9uzpgzl88");
		byte[] array = Convert.FromBase64String(cipherText);
		PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, null);
		byte[] bytes2 = passwordDeriveBytes.GetBytes(32);
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.Mode = CipherMode.CBC;
		ICryptoTransform transform = rijndaelManaged.CreateDecryptor(bytes2, bytes);
		MemoryStream memoryStream = new MemoryStream(array);
		CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
		byte[] array2 = new byte[array.Length];
		int count = cryptoStream.Read(array2, 0, array2.Length);
		memoryStream.Close();
		cryptoStream.Close();
		return Encoding.UTF8.GetString(array2, 0, count);
	}
}


