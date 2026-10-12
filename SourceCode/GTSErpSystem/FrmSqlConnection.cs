using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using System.Xml.Serialization;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Main;

namespace GTSErpSystem;

public class FrmSqlConnection : XtraForm
{
	private SqlConnection Conn;

	private SqlCommand Command;

	private SqlDataReader Reader;

	private string sql = "";

	private string ConnectionString = "";

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox3;

	private GroupBox groupBox2;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private System.Windows.Forms.ComboBox comboServerName;

	private ComboBoxEdit comboDatabaseName;

	private TextBox TxtPassword;

	private TextBox TxtUser;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	private SimpleButton BtnTest;

	private SimpleButton BtnSearch;

	public FrmSqlConnection()
	{
		InitializeComponent();
		BindServer();
		UserLookAndFeel.Default.SkinName = "Office 2013";
		comboServerName.Focus();
	}

		private void BtnConnection_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(comboServerName.Text) ||
			string.IsNullOrWhiteSpace(TxtUser.Text) ||
			string.IsNullOrEmpty(TxtPassword.Text))
		{
			MessageBox.Show(
				"عفوًا، أدخل اسم الخادم واسم المستخدم وكلمة المرور.",
				"مستكشف الاتصال",
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				MessageBoxOptions.RightAlign);
			TxtUser.Focus();
			return;
		}

		try
		{
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
			{
				DataSource = comboServerName.Text.Trim(),
				UserID = TxtUser.Text,
				Password = TxtPassword.Text,
				PersistSecurityInfo = true,
				ConnectTimeout = 5,
				Encrypt = false,
				TrustServerCertificate = true
			};
			ConnectionString = builder.ConnectionString;

			comboDatabaseName.Properties.Items.Clear();
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				const string query = "SELECT name FROM sys.databases " +
					"WHERE database_id > 4 AND name NOT IN ('ReportServer', 'ReportServerTempDB') " +
					"ORDER BY create_date DESC";

				using (SqlCommand command = new SqlCommand(query, connection))
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
						comboDatabaseName.Properties.Items.Add(reader["name"].ToString());
				}
			}

			MessageBox.Show(
				"تم الاتصال بالخادم بنجاح. اختر قاعدة البيانات المطلوبة ثم احفظ الإعدادات.",
				"مستكشف الاتصال",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1,
				MessageBoxOptions.RightAlign);
		}
		catch (Exception ex)
		{
			MessageBox.Show(
				"تعذر الاتصال بالخادم أو قراءة قواعد البيانات. تحقق من اسم الخادم وصلاحيات الحساب.\r\n" + ex.Message,
				"مستكشف الاتصال",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				MessageBoxOptions.RightAlign);
		}
	}

		private int GetAllServer()
	{
		int added = 0;
		DataTable dataSources = SqlDataSourceEnumerator.Instance.GetDataSources();
		foreach (DataRow row in dataSources.Rows)
		{
			string serverName = Convert.ToString(row[dataSources.Columns["ServerName"]]);
			string instanceName = dataSources.Columns.Contains("InstanceName")
				? Convert.ToString(row[dataSources.Columns["InstanceName"]])
				: string.Empty;

			if (string.IsNullOrWhiteSpace(serverName))
				continue;

			string fullName = string.IsNullOrWhiteSpace(instanceName)
				? serverName
				: serverName + "\\" + instanceName;

			if (!comboServerName.Items.Contains(fullName))
			{
				comboServerName.Items.Add(fullName);
				added++;
			}
		}
		return added;
	}

		private bool InsertDataInToXml()
	{
		string xmlPath = Path.Combine(Application.StartupPath, "ERP.xml");
		string tempPath = xmlPath + ".tmp";
		try
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<SaveConnectionForXml>));
			SaveConnectionForXml settings = new SaveConnectionForXml
			{
				_comboServer = comboServerName.Text.Trim(),
				_comboDatabase = comboDatabaseName.Text.Trim(),
				_TxtUser = Class_Encryption.EncryptString(TxtUser.Text, "EF"),
				_TxtPassword = Class_Encryption.EncryptString(TxtPassword.Text, "EF")
			};

			using (FileStream stream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				serializer.Serialize(stream, new List<SaveConnectionForXml> { settings });
				stream.Flush(true);
			}

			if (File.Exists(xmlPath))
				File.Replace(tempPath, xmlPath, null);
			else
				File.Move(tempPath, xmlPath);

			return true;
		}
		catch (Exception ex)
		{
			try
			{
				if (File.Exists(tempPath))
					File.Delete(tempPath);
			}
			catch
			{
			}

			MessageBox.Show(
				"تعذر حفظ إعدادات الاتصال. لم يتم إعلان النجاح.\r\n" + ex.Message,
				"مستكشف الاتصال",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				MessageBoxOptions.RightAlign);
			return false;
		}
	}

		private void BtnSave_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(comboServerName.Text) ||
			string.IsNullOrWhiteSpace(TxtUser.Text) ||
			string.IsNullOrEmpty(TxtPassword.Text) ||
			string.IsNullOrWhiteSpace(comboDatabaseName.Text))
		{
			MessageBox.Show(
				"عفوًا، أدخل الخادم والمستخدم وكلمة المرور واختر قاعدة البيانات.",
				"مستكشف الاتصال",
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				MessageBoxOptions.RightAlign);
			comboDatabaseName.Focus();
			return;
		}

		// Do not report success or restart unless the XML was written successfully.
		if (!InsertDataInToXml())
			return;

		MessageBox.Show(
			"تم حفظ إعدادات الاتصال بنجاح. سيُعاد تشغيل البرنامج لتطبيقها.",
			"مستكشف الاتصال",
			MessageBoxButtons.OK,
			MessageBoxIcon.Information,
			MessageBoxDefaultButton.Button1,
			MessageBoxOptions.RightAlign);

		try
		{
			string executablePath = Application.ExecutablePath;
			Process.Start(new ProcessStartInfo(executablePath) { UseShellExecute = true });
			Application.Exit();
		}
		catch (Exception ex)
		{
			MessageBox.Show(
				"تم حفظ إعدادات الاتصال، لكن تعذر إعادة تشغيل البرنامج تلقائيًا. أغلق البرنامج وشغّله يدويًا.\r\n" + ex.Message,
				"إعدادات الاتصال",
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				MessageBoxOptions.RightAlign);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void ControlSave_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnTest.Focus();
		}
	}

	private void textBox1_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("en"));
	}

	public static InputLanguage GetInputLanguageByName(string inputName)
	{
		foreach (InputLanguage installedInputLanguage in InputLanguage.InstalledInputLanguages)
		{
			if (installedInputLanguage.Culture.EnglishName.ToLower().StartsWith(inputName))
			{
				return installedInputLanguage;
			}
		}
		return null;
	}

	private void SetKeyboardLayout(InputLanguage layout)
	{
		InputLanguage.CurrentInputLanguage = layout;
	}

	private void comboDatabaseName_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

		private void BtnSearch_Click(object sender, EventArgs e)
	{
		try
		{
			int added = GetAllServer();
			if (added > 0)
			{
				MessageBox.Show(
					"تم العثور على " + added + " خادم/خوادم. اختر الخادم أو اكتب اسمه يدويًا.",
					"مستكشف الاتصال",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information,
					MessageBoxDefaultButton.Button1,
					MessageBoxOptions.RightAlign);
			}
			else
			{
				MessageBox.Show(
					"لم يتم العثور على خوادم تلقائيًا. يمكنك كتابة اسم الخادم يدويًا، مثل .\\SQLEXPRESS.",
					"مستكشف الاتصال",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning,
					MessageBoxDefaultButton.Button1,
					MessageBoxOptions.RightAlign);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(
				"تعذر اكتشاف الخوادم تلقائيًا. اكتب اسم الخادم يدويًا إذا كان معروفًا.\r\n" + ex.Message,
				"مستكشف الاتصال",
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				MessageBoxOptions.RightAlign);
		}
	}

		private void BindServer()
	{
		string xmlPath = Path.Combine(Application.StartupPath, "ERP.xml");
		if (!File.Exists(xmlPath))
			return;

		try
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<SaveConnectionForXml>));
			List<SaveConnectionForXml> settings;
			using (FileStream stream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				settings = (List<SaveConnectionForXml>)serializer.Deserialize(stream);
			}

			if (settings == null || settings.Count == 0 || settings[0] == null)
				throw new InvalidDataException("ملف ERP.xml لا يحتوي على إعدادات اتصال.");

			SaveConnectionForXml saved = settings[0];
			comboServerName.Text = saved._comboServer;
			comboDatabaseName.Text = saved._comboDatabase;
			TxtUser.Text = Class_Encryption.DecryptString(saved._TxtUser, "EF");
			TxtPassword.Text = Class_Encryption.DecryptString(saved._TxtPassword, "EF");
		}
		catch (Exception ex)
		{
			MessageBox.Show(
				"تعذر قراءة إعدادات الاتصال المحفوظة من ERP.xml. يمكنك تصحيحها ثم حفظها من جديد.\r\n" + ex.Message,
				"مستكشف الاتصال",
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1,
				MessageBoxOptions.RightAlign);
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.FrmSqlConnection));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnTest = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
		this.TxtPassword = new System.Windows.Forms.TextBox();
		this.TxtUser = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.comboServerName = new System.Windows.Forms.ComboBox();
		this.comboDatabaseName = new DevExpress.XtraEditors.ComboBoxEdit();
		this.groupBox1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.comboDatabaseName.Properties).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.groupBox3);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Location = new System.Drawing.Point(13, 26);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(623, 246);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "البيانات الرئيسية";
		this.groupBox3.Controls.Add(this.BtnExit);
		this.groupBox3.Controls.Add(this.BtnSave);
		this.groupBox3.Controls.Add(this.BtnTest);
		this.groupBox3.Location = new System.Drawing.Point(16, 166);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(590, 72);
		this.groupBox3.TabIndex = 11;
		this.groupBox3.TabStop = false;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(27, 23);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(142, 40);
		this.BtnExit.TabIndex = 2;
		this.BtnExit.Text = "خروج Exit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(177, 23);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(142, 40);
		this.BtnSave.TabIndex = 1;
		this.BtnSave.Text = "حفظ Save";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnTest.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnTest.ImageOptions.Image");
		this.BtnTest.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnTest.Location = new System.Drawing.Point(327, 23);
		this.BtnTest.Margin = new System.Windows.Forms.Padding(4);
		this.BtnTest.Name = "BtnTest";
		this.BtnTest.Size = new System.Drawing.Size(142, 40);
		this.BtnTest.TabIndex = 0;
		this.BtnTest.Text = "اختبار Test";
		this.BtnTest.Click += new System.EventHandler(BtnConnection_Click);
		this.BtnTest.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.groupBox2.Controls.Add(this.BtnSearch);
		this.groupBox2.Controls.Add(this.TxtPassword);
		this.groupBox2.Controls.Add(this.TxtUser);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.comboServerName);
		this.groupBox2.Controls.Add(this.comboDatabaseName);
		this.groupBox2.Location = new System.Drawing.Point(16, 21);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(590, 139);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.BtnSearch.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSearch.ImageOptions.Image");
		this.BtnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSearch.Location = new System.Drawing.Point(27, 30);
		this.BtnSearch.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSearch.Name = "BtnSearch";
		this.BtnSearch.Size = new System.Drawing.Size(112, 24);
		this.BtnSearch.TabIndex = 3;
		this.BtnSearch.Text = "بحث Search";
		this.BtnSearch.Click += new System.EventHandler(BtnSearch_Click);
		this.TxtPassword.Location = new System.Drawing.Point(27, 66);
		this.TxtPassword.Name = "TxtPassword";
		this.TxtPassword.PasswordChar = '#';
		this.TxtPassword.Size = new System.Drawing.Size(187, 23);
		this.TxtPassword.TabIndex = 2;
		this.TxtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPassword.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtUser.Location = new System.Drawing.Point(282, 66);
		this.TxtUser.Name = "TxtUser";
		this.TxtUser.Size = new System.Drawing.Size(187, 23);
		this.TxtUser.TabIndex = 1;
		this.TxtUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtUser.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtUser.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(475, 103);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(89, 17);
		this.label4.TabIndex = 43;
		this.label4.Text = "قاعدة البيانات ";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(475, 69);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(101, 17);
		this.label3.TabIndex = 42;
		this.label3.Text = "اسم المستخدم";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(220, 69);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(56, 17);
		this.label2.TabIndex = 41;
		this.label2.Text = "الباسورد";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(475, 33);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(84, 17);
		this.label1.TabIndex = 40;
		this.label1.Text = "اسم السيرفر";
		this.comboServerName.FormattingEnabled = true;
		this.comboServerName.Location = new System.Drawing.Point(146, 30);
		this.comboServerName.Name = "comboServerName";
		this.comboServerName.Size = new System.Drawing.Size(323, 24);
		this.comboServerName.TabIndex = 0;
		this.comboServerName.Text = ".";
		this.comboServerName.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.comboServerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(comboDatabaseName_KeyPress);
		this.comboDatabaseName.Location = new System.Drawing.Point(27, 100);
		this.comboDatabaseName.Name = "comboDatabaseName";
		this.comboDatabaseName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.comboDatabaseName.Size = new System.Drawing.Size(442, 22);
		this.comboDatabaseName.TabIndex = 3;
		this.comboDatabaseName.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
		this.comboDatabaseName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(comboDatabaseName_KeyPress);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(649, 299);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmSqlConnection";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "الاتصال بقاعدة البيانات";
		this.groupBox1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.comboDatabaseName.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


