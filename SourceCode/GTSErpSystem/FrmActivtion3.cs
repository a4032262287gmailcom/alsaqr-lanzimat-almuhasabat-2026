using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using GTSErpSystem.BLL.Main;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Microsoft.Win32;

namespace GTSErpSystem;

public class FrmActivtion3 : MetroForm
{
	private ChangeConnection setting = new ChangeConnection();

	private string PathValue;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private GroupBox groupBox2;

	private MetroTextBox.MetroTextButton BtnExit;

	private MetroTextBox.MetroTextButton BtnActive;

	private MetroTextBox TxtActive;

	private MetroTextBox TxtSerial;

	private LinkLabel linkLabel1;

	private Label label2;

	public FrmActivtion3(string path)
	{
		InitializeComponent(); 
 this.DialogResult = DialogResult.OK; 
 this.Close();
		PathValue = path;
		DecriptData();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Clipboard.SetText(TxtSerial.Text);
	}

	private void BtnActive_Click(object sender, EventArgs e)
	{
		TxtActive.Text = string.Empty;
		TxtActive.Text = Clipboard.GetText();
		if (TxtActive.Text != string.Empty)
		{
			if (passwordEntry(TxtActive.Text))
			{
				Application.Exit();
				Process.Start("الصقر لأنظمة المحاسبة.exe");
			}
		}
		else
		{
			MessageBox.Show("فضلا ادخل رقم التفعيل طŒ ثم اضغط علي لصق وحفظ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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

	private void TxtSerial_ButtonClick(object sender, EventArgs e)
	{
		Clipboard.SetText(TxtSerial.Text);
	}

	private void TxtActive_ButtonClick(object sender, EventArgs e)
	{
		TxtActive.Text = string.Empty;
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

	private void DecriptData()
	{
		string machineName = Environment.MachineName;
		string text = Class_Crypto.SetData("b14ca5898a4e4133bbce2ea2315a1500", "b14ca5898a4e4133bbce2ea2315a1916", MacAddress(), SerialNumber(), HardDisk(), LoginDetails.VergDate, DateTime.Now.ToString("yyyy-MM-dd"), machineName);
		string text2 = Class_Crypto.SetDataSecondWay("b14ca5898a4e4133bbce2ea2315a1500", "b14ca5898a4e4133bbce2ea2315a1916", text);
		TxtSerial.Text = text2;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.FrmActivtion3));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnActive = new MetroFramework.Controls.MetroTextBox.MetroTextButton();
		this.TxtActive = new MetroFramework.Controls.MetroTextBox();
		this.BtnExit = new MetroFramework.Controls.MetroTextBox.MetroTextButton();
		this.TxtSerial = new MetroFramework.Controls.MetroTextBox();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.linkLabel1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.ForeColor = System.Drawing.Color.Green;
		this.groupBox1.Location = new System.Drawing.Point(20, 86);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
		this.groupBox1.Size = new System.Drawing.Size(771, 469);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.label2.Font = new System.Drawing.Font("Cairo SemiBold", 9.1f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.label2.Location = new System.Drawing.Point(604, 72);
		this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(153, 26);
		this.label2.TabIndex = 10;
		this.label2.Text = "00966115102777";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Green;
		this.linkLabel1.AutoSize = true;
		this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.linkLabel1.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.linkLabel1.Location = new System.Drawing.Point(22, 75);
		this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(123, 20);
		this.linkLabel1.TabIndex = 9;
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Text = "www.gts-sa.net";
		this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Green;
		this.linkLabel1.Click += new System.EventHandler(linkLabel1_Click);
		this.label1.Font = new System.Drawing.Font("Cairo SemiBold", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.label1.Location = new System.Drawing.Point(12, 18);
		this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(751, 31);
		this.label1.TabIndex = 1;
		this.label1.Text = "شركة G. T. S للبرمجيات وتقنية نظم المعلومات";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.groupBox2.Controls.Add(this.BtnActive);
		this.groupBox2.Controls.Add(this.TxtActive);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.TxtSerial);
		this.groupBox2.ForeColor = System.Drawing.Color.Green;
		this.groupBox2.Location = new System.Drawing.Point(12, 102);
		this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
		this.groupBox2.Size = new System.Drawing.Size(751, 363);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.BtnActive.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnActive.ForeColor = System.Drawing.Color.White;
		this.BtnActive.Image = null;
		this.BtnActive.Location = new System.Drawing.Point(5, 25);
		this.BtnActive.Margin = new System.Windows.Forms.Padding(2);
		this.BtnActive.Name = "BtnActive";
		this.BtnActive.Size = new System.Drawing.Size(369, 38);
		this.BtnActive.Style = MetroFramework.MetroColorStyle.Green;
		this.BtnActive.TabIndex = 7;
		this.BtnActive.Text = "لصق وحفظ";
		this.BtnActive.UseSelectable = true;
		this.BtnActive.UseVisualStyleBackColor = true;
		this.BtnActive.Click += new System.EventHandler(BtnActive_Click);
		this.TxtActive.CustomButton.Image = null;
		this.TxtActive.CustomButton.Location = new System.Drawing.Point(79, 2);
		this.TxtActive.CustomButton.Margin = new System.Windows.Forms.Padding(2);
		this.TxtActive.CustomButton.Name = "";
		this.TxtActive.CustomButton.Size = new System.Drawing.Size(287, 287);
		this.TxtActive.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
		this.TxtActive.CustomButton.TabIndex = 1;
		this.TxtActive.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
		this.TxtActive.CustomButton.UseSelectable = true;
		this.TxtActive.CustomButton.Visible = false;
		this.TxtActive.ForeColor = System.Drawing.Color.Green;
		this.TxtActive.Lines = new string[0];
		this.TxtActive.Location = new System.Drawing.Point(5, 67);
		this.TxtActive.Margin = new System.Windows.Forms.Padding(2);
		this.TxtActive.MaxLength = 32767;
		this.TxtActive.Multiline = true;
		this.TxtActive.Name = "TxtActive";
		this.TxtActive.PasswordChar = '\0';
		this.TxtActive.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtActive.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.TxtActive.SelectedText = "";
		this.TxtActive.SelectionLength = 0;
		this.TxtActive.SelectionStart = 0;
		this.TxtActive.Size = new System.Drawing.Size(369, 292);
		this.TxtActive.Style = MetroFramework.MetroColorStyle.Silver;
		this.TxtActive.TabIndex = 48;
		this.TxtActive.Theme = MetroFramework.MetroThemeStyle.Light;
		this.TxtActive.UseSelectable = true;
		this.TxtActive.WaterMarkColor = System.Drawing.Color.FromArgb(109, 109, 109);
		this.TxtActive.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
		this.TxtActive.ButtonClick += new MetroFramework.Controls.MetroTextBox.ButClick(TxtActive_ButtonClick);
		this.BtnExit.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.ForeColor = System.Drawing.Color.White;
		this.BtnExit.Image = null;
		this.BtnExit.Location = new System.Drawing.Point(378, 25);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(2);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(369, 38);
		this.BtnExit.Style = MetroFramework.MetroColorStyle.Green;
		this.BtnExit.TabIndex = 8;
		this.BtnExit.Text = "نسخ";
		this.BtnExit.UseSelectable = true;
		this.BtnExit.UseVisualStyleBackColor = true;
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.TxtSerial.CustomButton.Image = null;
		this.TxtSerial.CustomButton.Location = new System.Drawing.Point(79, 2);
		this.TxtSerial.CustomButton.Margin = new System.Windows.Forms.Padding(2);
		this.TxtSerial.CustomButton.Name = "";
		this.TxtSerial.CustomButton.Size = new System.Drawing.Size(287, 287);
		this.TxtSerial.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
		this.TxtSerial.CustomButton.TabIndex = 1;
		this.TxtSerial.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
		this.TxtSerial.CustomButton.UseSelectable = true;
		this.TxtSerial.CustomButton.Visible = false;
		this.TxtSerial.ForeColor = System.Drawing.Color.Green;
		this.TxtSerial.Lines = new string[0];
		this.TxtSerial.Location = new System.Drawing.Point(378, 67);
		this.TxtSerial.Margin = new System.Windows.Forms.Padding(2);
		this.TxtSerial.MaxLength = 32767;
		this.TxtSerial.Multiline = true;
		this.TxtSerial.Name = "TxtSerial";
		this.TxtSerial.PasswordChar = '\0';
		this.TxtSerial.ReadOnly = true;
		this.TxtSerial.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtSerial.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.TxtSerial.SelectedText = "";
		this.TxtSerial.SelectionLength = 0;
		this.TxtSerial.SelectionStart = 0;
		this.TxtSerial.Size = new System.Drawing.Size(369, 292);
		this.TxtSerial.Style = MetroFramework.MetroColorStyle.Silver;
		this.TxtSerial.TabIndex = 47;
		this.TxtSerial.Theme = MetroFramework.MetroThemeStyle.Light;
		this.TxtSerial.UseSelectable = true;
		this.TxtSerial.WaterMarkColor = System.Drawing.Color.FromArgb(109, 109, 109);
		this.TxtSerial.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
		this.TxtSerial.ButtonClick += new MetroFramework.Controls.MetroTextBox.ButClick(TxtSerial_ButtonClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(807, 578);
		base.Controls.Add(this.groupBox1);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(2);
		base.MaximizeBox = false;
		base.Name = "FrmActivtion3";
		base.Padding = new System.Windows.Forms.Padding(18, 74, 18, 20);
		base.Resizable = false;
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.Style = MetroFramework.MetroColorStyle.Green;
		this.Text = "برنامج G.T.S المحاسبي";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmActivtion_FormClosing);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}



