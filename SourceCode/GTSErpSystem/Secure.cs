using System;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GTSErpSystem;

public class Secure
{
	private string globalPath;

	private void firstTime()
	{
		RegistryKey currentUser = Registry.CurrentUser;
		currentUser = currentUser.CreateSubKey(globalPath);
		GregorianCalendar calendar = new GregorianCalendar();
		DateTime now = DateTime.Now;
		string value = new DateTime(now.Year, now.Month, now.Day, calendar).ToShortDateString();
		currentUser.SetValue("Install", value);
		currentUser.SetValue("Use", value);
	}

	private string checkfirstDate()
	{
		RegistryKey currentUser = Registry.CurrentUser;
		currentUser = currentUser.CreateSubKey(globalPath);
		string result = (string)currentUser.GetValue("Install");
		if (currentUser.GetValue("Install") == null)
		{
			return "First";
		}
		return result;
	}

	private bool checkPassword(string pass)
	{
		RegistryKey currentUser = Registry.CurrentUser;
		currentUser = currentUser.CreateSubKey(globalPath);
		string text = (string)currentUser.GetValue("Password");
		if (text == pass)
		{
			return true;
		}
		return false;
	}

	private string dayDifPutPresent()
	{
		string value = DateTime.Now.ToShortDateString();
		DateTime dateTime = Convert.ToDateTime(value);
		RegistryKey currentUser = Registry.CurrentUser;
		currentUser = currentUser.CreateSubKey(globalPath);
		string value2 = (string)currentUser.GetValue("Install");
		DateTime value3;
		try
		{
			value3 = Convert.ToDateTime(value2);
		}
		catch (Exception)
		{
			value3 = Convert.ToDateTime("1001/01/01");
		}
		int num = (int)dateTime.Subtract(value3).TotalDays;
		string value4 = (string)currentUser.GetValue("Use");
		DateTime value5;
		try
		{
			value5 = Convert.ToDateTime(value4);
		}
		catch (Exception)
		{
			value5 = Convert.ToDateTime("3003/03/03");
		}
		int num2 = (int)dateTime.Subtract(value5).TotalDays;
		currentUser.SetValue("Use", value);
		if (num2 >= 0)
		{
			if (num < 0)
			{
				return "Error";
			}
			if (num >= 0 && num <= 5)
			{
				return Convert.ToString(5 - num);
			}
			return "Expired";
		}
		return "Error";
	}

	private void blackList()
	{
		RegistryKey currentUser = Registry.CurrentUser;
		currentUser = currentUser.CreateSubKey(globalPath);
		currentUser.SetValue("Black", "True");
	}

	private bool blackListCheck()
	{
		RegistryKey currentUser = Registry.CurrentUser;
		currentUser = currentUser.CreateSubKey(globalPath);
		string text = (string)currentUser.GetValue("Black");
		if (currentUser.GetValue("Black") == null)
		{
			return false;
		}
		return true;
	}

	public bool RunAppAfter(string appPassword, string pass)
	{
		RegistryKey currentUser = Registry.CurrentUser;
		currentUser = currentUser.CreateSubKey(globalPath);
		string text = (string)currentUser.GetValue("Black");
		if (currentUser.GetValue("Black") == null)
		{
			return false;
		}
		return true;
	}

	public bool Algorithm(string appPassword, string pass)
	{
		globalPath = pass;
		if (checkPassword(appPassword))
		{
			LoginDetails.Status = "نسخة مرخصة";
			return true;
		}
		if (!blackListCheck())
		{
			string text = checkfirstDate();
			if (text == "First")
			{
				firstTime();
				return MessageBox.Show("مؤسسة G.T.S ترحب بكم \r\n انت تستخدم النسخة التجريبية الان لمدة 5 يوم، لتفعيل برنامج G.T.S الرجاء التواصل مع فريق التسويق   \r\n هل تريد تفعيل البرنامج الان؟ ", "النسخة التجريبية", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign) switch
				{
					DialogResult.Yes => false, 
					DialogResult.No => true, 
					_ => false, 
				};
			}
			string text2 = dayDifPutPresent();
			if (text2 == "Error")
			{
				blackList();
				DialogResult dialogResult = MessageBox.Show("لا يمكن تحميل التطبيق طŒ حدث مقاطعة غير مصرح بها! بدون تفعيل لا يمكن تشغيله! هل تريد تنشيطه؟", "النسخة التجريبية - 01", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				if (dialogResult == DialogResult.OK)
				{
					return false;
				}
				return false;
			}
			if (text2 == "Expired")
			{
				DialogResult dialogResult2 = MessageBox.Show("نعتذر انتهاء فترة النسخة التجريبية \r\n للتفعيل الرجاء التواصل مع فريق التسويق ", "النسخة التجريبية", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				if (dialogResult2 == DialogResult.OK)
				{
					return false;
				}
				return false;
			}
			return MessageBox.Show("انت تستخدم النسخة التجريبية، متبقي  " + text2 + " يوم علي انتهاء فترة النسخة التجريبية \r\n للتفعيل الرجاء التواصل مع فريق التسويق \r\n هل تريد تفعيل البرنامج الان؟", "النسخة التجريبية", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign) switch
			{
				DialogResult.Yes => false, 
				DialogResult.No => true, 
				_ => false, 
			};
		}
		DialogResult dialogResult3 = MessageBox.Show("لا يمكن تحميل التطبيق طŒ تم عمل بلوك لبرنامج G.T.S \r\n  يرجي التواصل مع خدمة الدعم الفني ", "النسخة التجريبية -02", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		if (dialogResult3 == DialogResult.OK)
		{
			return false;
		}
		return false;
	}
}


