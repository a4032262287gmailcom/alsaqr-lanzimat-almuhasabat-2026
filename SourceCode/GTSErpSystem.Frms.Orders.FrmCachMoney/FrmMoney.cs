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
using GTSErpSystem.Frms.FrmRestaurant;
using GTSErpSystem.Frms.FrmsMain.Search;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.Orders.FrmCachMoney;

public class FrmMoney : XtraForm
{
	public bool IsComplateMony = false;

	private int DecimalNum = LoginDetails.DecimalNum;

	private IContainer components = null;

	private SimpleButton BtnSave;

	private GroupBox groupBox1;

	private ButtonEdit BtnVisaAcc;

	private ButtonEdit BtnCashAcc;

	private Label label2;

	private Label label1;

	private SimpleButton BtnCalc2;

	private SimpleButton BtnCalc1;

	public NumericEditBox TxtVisa;

	public NumericEditBox TxtCash;

	public Label LblCash;

	public Label LblVisa;

	private SimpleButton BtnExit;

	public FrmMoney()
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
		IsComplateMony = false;
		TxtCash.Text = Convert.ToString(LoginDetails.TotalFatorPrice);
		TxtCash.DecimalDigits = DecimalNum;
		TxtVisa.DecimalDigits = DecimalNum;
		LblCash.Text = LoginDetails.Acc_CashBox.ToString();
		BtnCashAcc.Text = GetAccountName(LoginDetails.Acc_CashBox);
		LblVisa.Text = LoginDetails.Acc_BankNumCachir.ToString();
		BtnVisaAcc.Text = GetAccountName(LoginDetails.Acc_BankNumCachir);
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave);
		}
	}

	public string GetAccountName(int AccountID)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		Account_Accounts account_Accounts = gTSdbEntities.Account_Accounts.Where((Account_Accounts x) => x.ID == AccountID).FirstOrDefault();
		return account_Accounts.Account_Name;
	}

	private void Save()
	{
		if (LoginDetails.TotalFatorPrice == Convert.ToDecimal(TxtCash.Text) + Convert.ToDecimal(TxtVisa.Text))
		{
			IsComplateMony = true;
			Close();
		}
		else
		{
			LoginDetails.IsComplateMony = false;
			MessageBox.Show("المبلغ المسدد لا يساوى المبلغ المطلوب", "مستكشف السداد ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
	}

	private void Frms_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F2")
		{
			Save();
		}
		else if (e.KeyCode == Keys.Escape)
		{
			Close();
		}
	}

	private void TxtCash_TextChanged_1(object sender, EventArgs e)
	{
		if (Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtCash.Value) >= 0m)
		{
			TxtVisa.Value = Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtCash.Value);
			return;
		}
		TxtVisa.Value = 0;
		TxtCash.Value = LoginDetails.TotalFatorPrice;
	}

	private void TxtVisa_TextChanged_1(object sender, EventArgs e)
	{
		if (Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtVisa.Value) >= 0m)
		{
			TxtCash.Value = Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtVisa.Value);
			return;
		}
		TxtCash.Value = 0;
		TxtVisa.Value = LoginDetails.TotalFatorPrice;
	}

	private void BtnCashAcc_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			LoginDetails.FrmName = "FrmCashOnly";
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblCash.Text = custCode.ToString();
				BtnCashAcc.Text = frmSearchAccount.AccountName;
			}
			else
			{
				BtnCashAcc.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void BtnVisaAcc_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			LoginDetails.FrmName = "FrmReceiptsCheckBank";
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblVisa.Text = custCode.ToString();
				BtnVisaAcc.Text = frmSearchAccount.AccountName;
			}
			else
			{
				BtnVisaAcc.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnCalc1_Click(object sender, EventArgs e)
	{
		FrmCalculator frmCalculator = new FrmCalculator();
		frmCalculator.ShowDialog();
		TxtCash.Text = frmCalculator.textBox_Result.Text;
	}

	private void BtnCalc2_Click(object sender, EventArgs e)
	{
		FrmCalculator frmCalculator = new FrmCalculator();
		frmCalculator.ShowDialog();
		TxtVisa.Text = frmCalculator.textBox_Result.Text;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmCachMoney.FrmMoney));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnVisaAcc = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnCashAcc = new DevExpress.XtraEditors.ButtonEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.BtnCalc2 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCalc1 = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVisa = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtCash = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.LblCash = new System.Windows.Forms.Label();
		this.LblVisa = new System.Windows.Forms.Label();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnVisaAcc.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnCashAcc.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.BtnVisaAcc);
		this.groupBox1.Controls.Add(this.BtnCashAcc);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.BtnCalc2);
		this.groupBox1.Controls.Add(this.BtnCalc1);
		this.groupBox1.Controls.Add(this.TxtVisa);
		this.groupBox1.Controls.Add(this.TxtCash);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.BtnVisaAcc, "BtnVisaAcc");
		this.BtnVisaAcc.Name = "BtnVisaAcc";
		this.BtnVisaAcc.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnVisaAcc.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnVisaAcc.Properties.Appearance.Font");
		this.BtnVisaAcc.Properties.Appearance.Options.UseBackColor = true;
		this.BtnVisaAcc.Properties.Appearance.Options.UseFont = true;
		this.BtnVisaAcc.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnVisaAcc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnVisaAcc.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.BtnVisaAcc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnVisaAcc.Properties.Buttons"), resources.GetString("BtnVisaAcc.Properties.Buttons1"), (int)resources.GetObject("BtnVisaAcc.Properties.Buttons2"), (bool)resources.GetObject("BtnVisaAcc.Properties.Buttons3"), (bool)resources.GetObject("BtnVisaAcc.Properties.Buttons4"), (bool)resources.GetObject("BtnVisaAcc.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("BtnVisaAcc.Properties.Buttons6"), resources.GetObject("BtnVisaAcc.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("BtnVisaAcc.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("BtnVisaAcc.Properties.Buttons9"))
		});
		this.BtnVisaAcc.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnVisaAcc.Properties.NullText = resources.GetString("BtnVisaAcc.Properties.NullText");
		this.BtnVisaAcc.Properties.ReadOnly = true;
		this.BtnVisaAcc.Tag = "بحث بالكود";
		this.BtnVisaAcc.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnVisaAcc_ButtonClick);
		resources.ApplyResources(this.BtnCashAcc, "BtnCashAcc");
		this.BtnCashAcc.Name = "BtnCashAcc";
		this.BtnCashAcc.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnCashAcc.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCashAcc.Properties.Appearance.Font");
		this.BtnCashAcc.Properties.Appearance.Options.UseBackColor = true;
		this.BtnCashAcc.Properties.Appearance.Options.UseFont = true;
		this.BtnCashAcc.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnCashAcc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnCashAcc.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.BtnCashAcc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnCashAcc.Properties.Buttons"), resources.GetString("BtnCashAcc.Properties.Buttons1"), (int)resources.GetObject("BtnCashAcc.Properties.Buttons2"), (bool)resources.GetObject("BtnCashAcc.Properties.Buttons3"), (bool)resources.GetObject("BtnCashAcc.Properties.Buttons4"), (bool)resources.GetObject("BtnCashAcc.Properties.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("BtnCashAcc.Properties.Buttons6"), resources.GetObject("BtnCashAcc.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("BtnCashAcc.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("BtnCashAcc.Properties.Buttons9"))
		});
		this.BtnCashAcc.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCashAcc.Properties.NullText = resources.GetString("BtnCashAcc.Properties.NullText");
		this.BtnCashAcc.Properties.ReadOnly = true;
		this.BtnCashAcc.Tag = "بحث بالكود";
		this.BtnCashAcc.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnCashAcc_ButtonClick);
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.BtnCalc2, "BtnCalc2");
		this.BtnCalc2.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCalc2.Appearance.Font");
		this.BtnCalc2.Appearance.Options.UseFont = true;
		this.BtnCalc2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalc2.Name = "BtnCalc2";
		this.BtnCalc2.Click += new System.EventHandler(BtnCalc2_Click);
		resources.ApplyResources(this.BtnCalc1, "BtnCalc1");
		this.BtnCalc1.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCalc1.Appearance.Font");
		this.BtnCalc1.Appearance.Options.UseFont = true;
		this.BtnCalc1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalc1.Name = "BtnCalc1";
		this.BtnCalc1.Click += new System.EventHandler(BtnCalc1_Click);
		resources.ApplyResources(this.TxtVisa, "TxtVisa");
		this.TxtVisa.DecimalDigits = 3;
		this.TxtVisa.FlatBorderColor = System.Drawing.Color.White;
		this.TxtVisa.ForeColor = System.Drawing.Color.Green;
		this.TxtVisa.Name = "TxtVisa";
		this.TxtVisa.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVisa.UseCompatibleTextRendering = false;
		this.TxtVisa.Value = new decimal(new int[4]);
		this.TxtVisa.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtVisa.TextChanged += new System.EventHandler(TxtVisa_TextChanged_1);
		resources.ApplyResources(this.TxtCash, "TxtCash");
		this.TxtCash.DecimalDigits = 3;
		this.TxtCash.ForeColor = System.Drawing.Color.Blue;
		this.TxtCash.Name = "TxtCash";
		this.TxtCash.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCash.UseCompatibleTextRendering = false;
		this.TxtCash.Value = new decimal(new int[4]);
		this.TxtCash.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtCash.TextChanged += new System.EventHandler(TxtCash_TextChanged_1);
		resources.ApplyResources(this.LblCash, "LblCash");
		this.LblCash.Name = "LblCash";
		resources.ApplyResources(this.LblVisa, "LblVisa");
		this.LblVisa.Name = "LblVisa";
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.LblCash);
		base.Controls.Add(this.LblVisa);
		base.Controls.Add(this.BtnExit);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmMoney";
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnVisaAcc.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnCashAcc.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


