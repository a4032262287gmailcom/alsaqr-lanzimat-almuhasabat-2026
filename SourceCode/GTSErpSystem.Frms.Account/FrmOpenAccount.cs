using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.FrmsMain.Search;

namespace GTSErpSystem.Frms.Account;

public class FrmOpenAccount : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_OpenAccount OPN = new Class_OpenAccount();

	private Class_AccountCard ACT = new Class_AccountCard();

	private IContainer components = null;

	private GroupBox groupBox1;

	private SimpleButton BtnSave;

	private SimpleButton BtnExit;

	private Label label1;

	private ButtonEdit TxtAccountNumber;

	private Label label9;

	private ButtonEdit TxtAccountName;

	private NumericUpDown TxtDepit;

	private Label label2;

	private NumericUpDown TxtCredit;

	private Label label19;

	private DateEdit TxtDate;

	private Label label21;

	public FrmOpenAccount()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
		}
		InitializeComponent();
		TxtDate.DateTime = DateTime.Now;
		TxtAccountNumber.Focus();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave);
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 30))
		{
			if (TxtAccountNumber.Text != "")
			{
				int num = 0;
				num = OPN.Update(Convert.ToInt32(TxtAccountNumber.Text), Convert.ToDecimal(TxtDepit.Text), Convert.ToDecimal(TxtCredit.Text), TxtDate.DateTime);
				if (num > 0)
				{
					MessageBox.Show("تم حفظ بيانات ارصدة اول المدة لحساب " + TxtAccountName.Text + " بنجاح ", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					Clear();
				}
			}
			else
			{
				MessageBox.Show("من فضلك قم باختيار رقم الحساب ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TxtAccountNumber_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchAccount();
	}

	private void SearchAccount()
	{
		FrmSearchAccount ff = new FrmSearchAccount();
		ff.ShowDialog();
		if (ff.Account_Number > 0)
		{
			Account_Accounts account_Accounts = (from x in ACT.GetAccounts()
				where x.Account_No == ff.Account_Number
				select x).FirstOrDefault();
			TxtAccountNumber.Text = account_Accounts.Account_No.ToString();
			TxtAccountName.Text = account_Accounts.Account_Name;
			TxtDepit.Value = Convert.ToDecimal(account_Accounts.Priv_Debit);
			TxtCredit.Value = Convert.ToDecimal(account_Accounts.Priv_Credit);
			ff.Close();
		}
	}

	private void Clear()
	{
		TxtAccountName.Text = "";
		TxtAccountNumber.Text = "";
		TxtCredit.Value = 0m;
		TxtDepit.Value = 0m;
		TxtAccountNumber.Focus();
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void TxtCredit_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnSave.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Account.FrmOpenAccount));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions value = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.TxtDepit = new System.Windows.Forms.NumericUpDown();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtCredit = new System.Windows.Forms.NumericUpDown();
		this.label19 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtAccountNumber = new DevExpress.XtraEditors.ButtonEdit();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtAccountName = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDepit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCredit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccountNumber.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccountName.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.TxtDepit);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtCredit);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtAccountNumber);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.TxtAccountName);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.TxtDate, "TxtDate");
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDate.Properties.Appearance.Font");
		this.TxtDate.Properties.Appearance.Options.UseFont = true;
		this.TxtDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate.Properties.Buttons"))
		});
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate.Properties.Mask.EditMask = resources.GetString("TxtDate.Properties.Mask.EditMask");
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate.Properties.Mask.UseMaskAsDisplayFormat");
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.TxtDepit, "TxtDepit");
		this.TxtDepit.DecimalPlaces = 2;
		this.TxtDepit.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtDepit.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.TxtDepit.Name = "TxtDepit";
		this.TxtDepit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtCredit, "TxtCredit");
		this.TxtCredit.DecimalPlaces = 2;
		this.TxtCredit.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtCredit.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.TxtCredit.Name = "TxtCredit";
		this.TxtCredit.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtCredit_KeyDown);
		resources.ApplyResources(this.label19, "label19");
		this.label19.Name = "label19";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.TxtAccountNumber, "TxtAccountNumber");
		this.TxtAccountNumber.Name = "TxtAccountNumber";
		this.TxtAccountNumber.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtAccountNumber.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAccountNumber.Properties.Appearance.Font");
		this.TxtAccountNumber.Properties.Appearance.Options.UseBackColor = true;
		this.TxtAccountNumber.Properties.Appearance.Options.UseFont = true;
		this.TxtAccountNumber.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAccountNumber.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAccountNumber.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtAccountNumber.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.TxtAccountNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtAccountNumber.Properties.Buttons"), resources.GetString("TxtAccountNumber.Properties.Buttons1"), (int)resources.GetObject("TxtAccountNumber.Properties.Buttons2"), (bool)resources.GetObject("TxtAccountNumber.Properties.Buttons3"), (bool)resources.GetObject("TxtAccountNumber.Properties.Buttons4"), (bool)resources.GetObject("TxtAccountNumber.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtAccountNumber.Properties.Buttons6"), resources.GetObject("TxtAccountNumber.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtAccountNumber.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtAccountNumber.Properties.Buttons9"))
		});
		this.TxtAccountNumber.Properties.ReadOnly = true;
		this.TxtAccountNumber.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtAccountNumber_ButtonClick);
		this.TxtAccountNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.TxtAccountName, "TxtAccountName");
		this.TxtAccountName.Name = "TxtAccountName";
		this.TxtAccountName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtAccountName.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAccountName.Properties.Appearance.Font");
		this.TxtAccountName.Properties.Appearance.Options.UseBackColor = true;
		this.TxtAccountName.Properties.Appearance.Options.UseFont = true;
		this.TxtAccountName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAccountName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAccountName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtAccountName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(value, "editorButtonImageOptions2");
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmOpenAccount";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDepit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCredit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccountNumber.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccountName.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


