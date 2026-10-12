using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Media;
using System.Windows.Forms;
using System.Xml.Serialization;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Main;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Microsoft.Win32;

namespace GTSErpSystem;

public class FrmActivtion : MetroForm
{
	private ChangeConnection setting = new ChangeConnection();

	private string PathValue;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private GroupBox groupBox2;

	private Label label2;

	private Label label3;

	private MetroTextBox.MetroTextButton BtnExit;

	private MetroTextBox.MetroTextButton BtnActive;

	private MetroTextBox TxtActive;

	private MetroTextBox TxtSerial;

	private LinkLabel linkLabel1;

	public FrmActivtion(string path)
	{
		InitializeComponent(); 
 this.DialogResult = DialogResult.OK; 
 this.Close();
		PathValue = path;
		string plainText = Class_Encryption.EncryptString("MS-" + Convert.ToString(GetSerial()) + "-19-5-01-" + Convert.ToString(GetDiskSerial()), LoginDetails.AppVerg);
		string text = Class_Encryption.EncryptString(plainText, LoginDetails.AppVerg);
		TxtSerial.Text = text;
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}

	private void BtnActive_Click(object sender, EventArgs e)
	{
		if (TxtActive.Text != "")
		{
			if (TxtActive.Text == Class_Encryption.DecryptString(TxtSerial.Text, LoginDetails.AppVerg))
			{
				try
				{
					if (passwordEntry(TxtActive.Text))
					{
						InsertDataInToXml();
						MessageBox.Show("تم تأكيد ترخيص التفعيل بنجاح \r\n يمكنكم الان استخدام البرنامج \r\n شكرا علي اهتمامكم شركة G.T.S للبرمجيات ", "تم التفعيل بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						SoundPlayer soundPlayer = new SoundPlayer("Attention.WAV");
						soundPlayer.Play();
						Hide();
						FrmSqlConnection frmSqlConnection = new FrmSqlConnection();
						frmSqlConnection.Show();
					}
					else
					{
						MessageBox.Show("لم يتم الفعيل طŒ برجاء المراجعة مع الدعم الفني", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						TxtActive.Focus();
					}
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString(), "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
			MessageBox.Show("الرقم غير صحيح طŒ برجاء التاكد من ادخال رقم التفعيل بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			TxtActive.Focus();
		}
		else
		{
			MessageBox.Show("فضلا ادخل رقم التفعيل طŒ ثم اضغط علي تفعيل", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			TxtActive.Focus();
		}
	}

	public bool passwordEntry(string ActiveValue)
	{
		RegistryKey currentUser = Registry.CurrentUser;
		currentUser = currentUser.CreateSubKey(PathValue);
		if (currentUser != null)
		{
			currentUser.SetValue("Password", ActiveValue);
			return true;
		}
		return false;
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

	private string GetDiskSerial()
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

	private void TxtSerial_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		TxtSerial.Text = "";
		Clipboard.SetText(TxtSerial.Text);
	}

	private void TxtActive_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		TxtActive.Text = "";
		TxtActive.Text = Clipboard.GetText();
	}

	private void InsertDataInToXml()
	{
		try
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SaveActiveForXml>));
			List<SaveActiveForXml> list = new List<SaveActiveForXml>();
			FileStream fileStream = new FileStream(Application.StartupPath + "\\Serial.xml", FileMode.Create, FileAccess.Write);
			SaveActiveForXml saveActiveForXml = new SaveActiveForXml();
			saveActiveForXml._Active = TxtActive.Text;
			list.Add(saveActiveForXml);
			xmlSerializer.Serialize(fileStream, list);
			fileStream.Close();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void RemoveActiveXml()
	{
		try
		{
			File.Delete(Application.StartupPath + "\\Serial.xml");
		}
		catch (Exception)
		{
			throw;
		}
		MessageBox.Show("تم حذف ملف الاعدادات بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void FrmActivition_FormClosed(object sender, FormClosedEventArgs e)
	{
		Application.Exit();
	}

	private void TxtSerial_ButtonClick(object sender, EventArgs e)
	{
		Clipboard.SetText(TxtSerial.Text);
	}

	private void TxtActive_ButtonClick(object sender, EventArgs e)
	{
		TxtActive.Text = "";
		TxtActive.Text = Clipboard.GetText();
	}

	private void FrmActivtion_FormClosing(object sender, FormClosingEventArgs e)
	{
		Application.Exit();
	}

	private void linkLabel1_Click(object sender, EventArgs e)
	{
		linkLabel1.LinkVisited = true;
		Process.Start("http://www.gts-sa.net");
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.FrmActivtion));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnExit = new MetroFramework.Controls.MetroTextBox.MetroTextButton();
		this.BtnActive = new MetroFramework.Controls.MetroTextBox.MetroTextButton();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.TxtActive = new MetroFramework.Controls.MetroTextBox();
		this.TxtSerial = new MetroFramework.Controls.MetroTextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.BtnExit);
		this.groupBox1.Controls.Add(this.BtnActive);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Location = new System.Drawing.Point(20, 86);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
		this.groupBox1.Size = new System.Drawing.Size(512, 276);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.BtnExit.Image = null;
		this.BtnExit.Location = new System.Drawing.Point(29, 225);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(2);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(104, 38);
		this.BtnExit.Style = MetroFramework.MetroColorStyle.Silver;
		this.BtnExit.TabIndex = 8;
		this.BtnExit.Text = "ESC خروج";
		this.BtnExit.UseSelectable = true;
		this.BtnExit.UseVisualStyleBackColor = true;
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnActive.ForeColor = System.Drawing.Color.White;
		this.BtnActive.Image = null;
		this.BtnActive.Location = new System.Drawing.Point(292, 225);
		this.BtnActive.Margin = new System.Windows.Forms.Padding(2);
		this.BtnActive.Name = "BtnActive";
		this.BtnActive.Size = new System.Drawing.Size(104, 38);
		this.BtnActive.Style = MetroFramework.MetroColorStyle.Silver;
		this.BtnActive.TabIndex = 7;
		this.BtnActive.Text = "Save حفظ";
		this.BtnActive.UseSelectable = true;
		this.BtnActive.UseVisualStyleBackColor = true;
		this.BtnActive.Click += new System.EventHandler(BtnActive_Click);
		this.label1.Font = new System.Drawing.Font("Tahoma", 9.1f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(12, 18);
		this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(484, 81);
		this.label1.TabIndex = 1;
		this.label1.Text = resources.GetString("label1.Text");
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.groupBox2.Controls.Add(this.TxtActive);
		this.groupBox2.Controls.Add(this.TxtSerial);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Location = new System.Drawing.Point(12, 102);
		this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
		this.groupBox2.Size = new System.Drawing.Size(484, 113);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.TxtActive.CustomButton.Image = null;
		this.TxtActive.CustomButton.Location = new System.Drawing.Point(344, 1);
		this.TxtActive.CustomButton.Margin = new System.Windows.Forms.Padding(2);
		this.TxtActive.CustomButton.Name = "";
		this.TxtActive.CustomButton.Size = new System.Drawing.Size(23, 23);
		this.TxtActive.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
		this.TxtActive.CustomButton.TabIndex = 1;
		this.TxtActive.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
		this.TxtActive.CustomButton.UseSelectable = true;
		this.TxtActive.ForeColor = System.Drawing.Color.Green;
		this.TxtActive.Lines = new string[0];
		this.TxtActive.Location = new System.Drawing.Point(16, 66);
		this.TxtActive.Margin = new System.Windows.Forms.Padding(2);
		this.TxtActive.MaxLength = 32767;
		this.TxtActive.Name = "TxtActive";
		this.TxtActive.PasswordChar = '\0';
		this.TxtActive.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.TxtActive.SelectedText = "";
		this.TxtActive.SelectionLength = 0;
		this.TxtActive.SelectionStart = 0;
		this.TxtActive.ShowButton = true;
		this.TxtActive.Size = new System.Drawing.Size(368, 25);
		this.TxtActive.Style = MetroFramework.MetroColorStyle.Silver;
		this.TxtActive.TabIndex = 48;
		this.TxtActive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtActive.Theme = MetroFramework.MetroThemeStyle.Dark;
		this.TxtActive.UseSelectable = true;
		this.TxtActive.WaterMarkColor = System.Drawing.Color.FromArgb(109, 109, 109);
		this.TxtActive.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
		this.TxtActive.ButtonClick += new MetroFramework.Controls.MetroTextBox.ButClick(TxtActive_ButtonClick);
		this.TxtSerial.CustomButton.Image = null;
		this.TxtSerial.CustomButton.Location = new System.Drawing.Point(344, 1);
		this.TxtSerial.CustomButton.Margin = new System.Windows.Forms.Padding(2);
		this.TxtSerial.CustomButton.Name = "";
		this.TxtSerial.CustomButton.Size = new System.Drawing.Size(23, 23);
		this.TxtSerial.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
		this.TxtSerial.CustomButton.TabIndex = 1;
		this.TxtSerial.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
		this.TxtSerial.CustomButton.UseSelectable = true;
		this.TxtSerial.ForeColor = System.Drawing.Color.White;
		this.TxtSerial.Lines = new string[0];
		this.TxtSerial.Location = new System.Drawing.Point(16, 30);
		this.TxtSerial.Margin = new System.Windows.Forms.Padding(2);
		this.TxtSerial.MaxLength = 32767;
		this.TxtSerial.Name = "TxtSerial";
		this.TxtSerial.PasswordChar = '\0';
		this.TxtSerial.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.TxtSerial.SelectedText = "";
		this.TxtSerial.SelectionLength = 0;
		this.TxtSerial.SelectionStart = 0;
		this.TxtSerial.ShowButton = true;
		this.TxtSerial.Size = new System.Drawing.Size(368, 25);
		this.TxtSerial.Style = MetroFramework.MetroColorStyle.Silver;
		this.TxtSerial.TabIndex = 47;
		this.TxtSerial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtSerial.Theme = MetroFramework.MetroThemeStyle.Dark;
		this.TxtSerial.UseSelectable = true;
		this.TxtSerial.WaterMarkColor = System.Drawing.Color.FromArgb(109, 109, 109);
		this.TxtSerial.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
		this.TxtSerial.ButtonClick += new MetroFramework.Controls.MetroTextBox.ButClick(TxtSerial_ButtonClick);
		this.label2.AutoSize = true;
		this.label2.ForeColor = System.Drawing.Color.White;
		this.label2.Location = new System.Drawing.Point(402, 69);
		this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(74, 17);
		this.label2.TabIndex = 2;
		this.label2.Text = "رقم التفعيل";
		this.label3.AutoSize = true;
		this.label3.ForeColor = System.Drawing.Color.White;
		this.label3.Location = new System.Drawing.Point(397, 32);
		this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(93, 17);
		this.label3.TabIndex = 3;
		this.label3.Text = "الرقم المرجعي";
		this.linkLabel1.AutoSize = true;
		this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.linkLabel1.LinkColor = System.Drawing.Color.White;
		this.linkLabel1.Location = new System.Drawing.Point(371, 64);
		this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(159, 25);
		this.linkLabel1.TabIndex = 9;
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Text = "www.gts-sa.net";
		this.linkLabel1.Click += new System.EventHandler(linkLabel1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(548, 385);
		base.Controls.Add(this.linkLabel1);
		base.Controls.Add(this.groupBox1);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(2);
		base.MaximizeBox = false;
		base.Name = "FrmActivtion";
		base.Padding = new System.Windows.Forms.Padding(18, 74, 18, 20);
		base.Resizable = false;
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.Style = MetroFramework.MetroColorStyle.White;
		this.Text = "تفعيل البرنامج";
		base.Theme = MetroFramework.MetroThemeStyle.Dark;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmActivtion_FormClosing);
		this.groupBox1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}



