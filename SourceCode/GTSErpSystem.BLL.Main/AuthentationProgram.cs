using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace GTSErpSystem.BLL.Main;

internal class AuthentationProgram
{
	public string isActive()
	{
		string result = "";
		try
		{
			string text = Class_Encryption.EncryptString("MS-" + Convert.ToString(GetSerial()) + "-19-5-01-" + Convert.ToString(GetDiskSerial()), "EFms");
			string text2 = Class_Encryption.DecryptString(text, "EFms");
			string text3 = Class_Encryption.EncryptString(text, "EFms");
			string text4 = "";
			if (File.Exists(Application.StartupPath + "\\Serial.xml"))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SaveActiveForXml>));
				List<SaveActiveForXml> list = new List<SaveActiveForXml>();
				FileStream fileStream = new FileStream(Application.StartupPath + "\\Serial.xml", FileMode.Open, FileAccess.Read);
				list = (List<SaveActiveForXml>)xmlSerializer.Deserialize(fileStream);
				SaveActiveForXml saveActiveForXml = list[0];
				text4 = saveActiveForXml._Active;
				fileStream.Close();
				if (text == text4)
				{
					result = text;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		return result;
	}

	public string GetSerial()
	{
		string result = "";
		ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
		foreach (ManagementObject item in managementObjectSearcher.Get())
		{
			result = item["SerialNumber"].ToString();
		}
		return result;
	}

	public string GetDiskSerial()
	{
		string result = "";
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
					result = "SerialNumber: " + current2["SerialNumber"];
				}
			}
		}
		return result;
	}
}


