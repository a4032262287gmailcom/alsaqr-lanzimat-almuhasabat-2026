using System;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GTSErpSystem.BLL.Main;

public class Class_Crypto
{
	public static string SetData(string key, string space, params string[] data)
	{
		string text = string.Empty;
		foreach (string text2 in data)
		{
			text = text + text2 + space;
		}
		string text3 = text;
		return Encrypt(key, text3);
	}

	public static string SetDataSecondWay(string key, string space, params string[] data)
	{
		string text = string.Empty;
		foreach (string text2 in data)
		{
			text = text + space + text2;
		}
		string text3 = text;
		return Encrypt(key, text3);
	}

	public static string GetData(string code, string key)
	{
		return Decrypt(key, code);
	}

	public static string[] ShowData(string code, string space)
	{
		return code.Split(new string[1] { space }, StringSplitOptions.None);
	}

	public static string Encrypt(string key, string text)
	{
		byte[] iV = new byte[16];
		byte[] inArray;
		using (Aes aes = Aes.Create())
		{
			aes.Key = Encoding.UTF8.GetBytes(key);
			aes.IV = iV;
			ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
			using MemoryStream memoryStream = new MemoryStream();
			using CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				streamWriter.Write(text);
			}
			inArray = memoryStream.ToArray();
		}
		return Convert.ToBase64String(inArray);
	}

	public static string Decrypt(string key, string EncryptText)
	{
		byte[] iV = new byte[16];
		byte[] buffer = Convert.FromBase64String(EncryptText);
		using Aes aes = Aes.Create();
		aes.Key = Encoding.UTF8.GetBytes(key);
		aes.IV = iV;
		ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
		using MemoryStream stream = new MemoryStream(buffer);
		using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
		using StreamReader streamReader = new StreamReader(stream2);
		return streamReader.ReadToEnd();
	}

	public static string MacAddress()
	{
		string result = string.Empty;
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		foreach (NetworkInterface networkInterface in allNetworkInterfaces)
		{
			if (networkInterface.OperationalStatus == OperationalStatus.Up)
			{
				result = networkInterface.GetPhysicalAddress().ToString();
				break;
			}
		}
		return result;
	}

	public static string SerialNumber()
	{
		string result = string.Empty;
		ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * from Win32_BaseBoard");
		foreach (ManagementObject item in managementObjectSearcher.Get())
		{
			result = item["SerialNumber"].ToString().Trim();
		}
		return result;
	}

	public static string HardDisk()
	{
		string result = string.Empty;
		string text = Path.GetPathRoot(Environment.SystemDirectory).TrimEnd('\\');
		using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" + text + "'} WHERE ResultClass=Win32_DiskPartition"))
		{
			using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = managementObjectSearcher.Get().GetEnumerator();
			if (managementObjectEnumerator.MoveNext())
			{
				ManagementBaseObject current = managementObjectEnumerator.Current;
				using ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + current["DeviceID"]?.ToString() + "'} WHERE ResultClass=Win32_DiskDrive");
				using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator2 = managementObjectSearcher2.Get().GetEnumerator();
				if (managementObjectEnumerator2.MoveNext())
				{
					ManagementBaseObject current2 = managementObjectEnumerator2.Current;
					result = current2["SerialNumber"].ToString();
				}
			}
		}
		return result;
	}

	public bool GetDataFromEncrption()
	{
		return true;
	}
}


