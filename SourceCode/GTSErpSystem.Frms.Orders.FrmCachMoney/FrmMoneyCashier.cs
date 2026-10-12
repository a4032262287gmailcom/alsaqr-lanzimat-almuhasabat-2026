using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.Orders.FrmCachMoney;

public class FrmMoneyCashier : XtraForm
{
	public bool IsComplateMony = false;

	private int DecimalNum = LoginDetails.DecimalNum;

	private string ActiveTxt = "TxtMoneyPaying";

	private IContainer components = null;

	private SimpleButton BtnSave;

	private SimpleButton BtnExit;

	private ToggleSwitch toggleCash;

	private ToggleSwitch toggleVisa;

	private GroupBox groupBox1;

	public NumericEditBox TxtVisa;

	public NumericEditBox TxtCash;

	private GroupBox groupBox2;

	private SimpleButton BtnClear;

	private Label label3;

	public NumericEditBox TxtMoneyNet;

	private Label label2;

	private Label label1;

	public NumericEditBox TxtMoneyNeed;

	private SimpleButton Btn8;

	private SimpleButton Btn7;

	private SimpleButton Btn9;

	private SimpleButton Btn5;

	private SimpleButton Btn4;

	private SimpleButton Btn6;

	private SimpleButton Btn2;

	private SimpleButton Btn1;

	private SimpleButton Btn3;

	private SimpleButton Btn000;

	private SimpleButton Btn0;

	private SimpleButton Btn00;

	public NumericEditBox TxtMoneyPaying;

	public CheckEdit CHPrint;

	public CheckEdit CHPrintA4;

	public CheckEdit CkSentInvoice;

	public FrmMoneyCashier()
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
		TxtDigital();
		IsComplateMony = false;
		TxtCash.Text = Convert.ToString(LoginDetails.TotalFatorPrice);
		if (LoginDetails.IsPrintCashier)
		{
			CHPrint.Checked = true;
		}
		else
		{
			CHPrint.Checked = false;
		}
		CkSentInvoice.Checked = LoginDetails.SentInvoice;
		base.Shown += FrmMoneyCashier_Shown;
	}

	private void FrmMoneyCashier_Shown(object sender, EventArgs e)
	{
		if (toggleCash.IsOn)
		{
			TxtMoneyPaying.Focus();
		}
		else
		{
			TxtVisa.Focus();
		}
	}

	private void toggleCash_Toggled(object sender, EventArgs e)
	{
		ToggleIsOn();
	}

	private void TxtCash_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (!toggleVisa.IsOn)
			{
				TxtCash.ReadOnly = true;
			}
			else
			{
				TxtCash.ReadOnly = false;
			}
			if (Convert.ToDecimal(TxtCash.Text) <= LoginDetails.TotalFatorPrice)
			{
				TxtVisa.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtCash.Text)).ToString();
				TxtMoneyNeed.Text = TxtCash.Text;
				TxtMoneyPaying.Text = TxtCash.Text;
			}
			else
			{
				TxtCash.Text = "0.00";
				TxtVisa.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtCash.Text)).ToString();
				TxtMoneyNeed.Text = TxtCash.Text;
				TxtMoneyPaying.Text = TxtCash.Text;
			}
		}
		catch (Exception)
		{
		}
	}

	private void TxtVisa_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (!toggleCash.IsOn)
			{
				TxtVisa.ReadOnly = true;
			}
			else
			{
				TxtVisa.ReadOnly = false;
			}
			if (Convert.ToDecimal(TxtVisa.Text) <= LoginDetails.TotalFatorPrice)
			{
				TxtCash.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtVisa.Text)).ToString();
				TxtMoneyPaying.Text = TxtCash.Text;
			}
			else
			{
				TxtVisa.Text = "0.00";
				TxtCash.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtVisa.Text)).ToString();
				TxtMoneyPaying.Text = TxtCash.Text;
			}
		}
		catch (Exception)
		{
		}
	}

	private void toggleVisa_Toggled(object sender, EventArgs e)
	{
		ToggleIsOff();
	}

	private void ToggleIsOn()
	{
		try
		{
			if (toggleCash.IsOn)
			{
				TxtCash.ReadOnly = false;
				TxtVisa.ReadOnly = false;
				if (!toggleVisa.IsOn)
				{
					TxtVisa.Text = "0.00";
					TxtVisa.Enabled = false;
					TxtCash.Enabled = true;
					TxtCash.ReadOnly = false;
				}
				else if (toggleVisa.IsOn)
				{
					TxtCash.Text = LoginDetails.TotalFatorPrice.ToString();
					TxtVisa.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtCash.Text)).ToString();
					TxtVisa.Enabled = true;
					TxtCash.Enabled = true;
				}
			}
			else if (!toggleCash.IsOn)
			{
				toggleCash.IsOn = false;
				TxtVisa.ReadOnly = true;
				TxtCash.Text = "0.00";
				TxtCash.Enabled = false;
				toggleVisa.IsOn = true;
				TxtVisa.Enabled = true;
				TxtCash.ReadOnly = true;
			}
			if (toggleCash.IsOn)
			{
				TxtMoneyPaying.Focus();
			}
			else
			{
				TxtVisa.Focus();
			}
		}
		catch (Exception)
		{
		}
	}

	private void ToggleIsOff()
	{
		try
		{
			if (toggleVisa.IsOn)
			{
				TxtVisa.ReadOnly = false;
				TxtCash.ReadOnly = false;
				if (!toggleCash.IsOn)
				{
					TxtCash.Text = "0.00";
					TxtCash.Enabled = false;
					TxtVisa.Enabled = true;
					TxtVisa.ReadOnly = false;
				}
				else if (toggleCash.IsOn)
				{
					TxtVisa.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtCash.Text)).ToString();
					TxtVisa.Enabled = true;
					TxtCash.Enabled = true;
				}
			}
			else if (!toggleVisa.IsOn)
			{
				TxtVisa.ReadOnly = true;
				TxtCash.ReadOnly = true;
				TxtVisa.Text = "0.00";
				TxtVisa.Enabled = false;
				toggleCash.IsOn = true;
				TxtCash.Enabled = true;
			}
			if (toggleCash.IsOn)
			{
				TxtMoneyPaying.Focus();
			}
			else
			{
				TxtVisa.Focus();
			}
		}
		catch (Exception)
		{
		}
	}

	private void TxtDigital()
	{
		TxtCash.DecimalDigits = DecimalNum;
		TxtMoneyNeed.DecimalDigits = DecimalNum;
		TxtMoneyNet.DecimalDigits = DecimalNum;
		TxtMoneyPaying.DecimalDigits = DecimalNum;
		TxtVisa.DecimalDigits = DecimalNum;
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		LoginDetails.FrmOpenDayIsPrint = CHPrint.Checked;
		LoginDetails.FrmOpenDayIsPrintA4 = CHPrintA4.Checked;
		LoginDetails.AmountPaid = Convert.ToDecimal(TxtMoneyPaying.Text);
		LoginDetails.Rest = Convert.ToDecimal(TxtMoneyNet.Text);
		Save();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		LoginDetails.IsComplateMony = false;
		Close();
	}

	private void BtnClear_Click(object sender, EventArgs e)
	{
		try
		{
			if (ActiveTxt == "TxtCash")
			{
				TxtCash.Text = "0.00";
			}
			else if (ActiveTxt == "TxtVisa")
			{
				TxtVisa.Text = "0.00";
			}
			else if (ActiveTxt == "TxtMoneyPaying")
			{
				TxtMoneyPaying.Text = "0.00";
			}
		}
		catch (Exception)
		{
		}
	}

	private void Btn000_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn000);
	}

	private void Btn00_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn00);
	}

	private void Btn0_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn0);
	}

	private void Btn1_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn1);
	}

	private void Btn2_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn2);
	}

	private void Btn3_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn3);
	}

	private void Btn4_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn4);
	}

	private void Btn5_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn5);
	}

	private void Btn6_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn6);
	}

	private void Btn7_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn7);
	}

	private void Btn8_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn8);
	}

	private void Btn9_Click(object sender, EventArgs e)
	{
		BtnNumber(Btn9);
	}

	private void TxtCashNeed_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (TxtMoneyPaying.Text == "")
			{
				TxtMoneyPaying.Text = "0.00";
			}
			TxtMoneyNet.Text = (Convert.ToDecimal(TxtMoneyNeed.Text) - Convert.ToDecimal(TxtMoneyPaying.Text)).ToString();
		}
		catch (Exception)
		{
		}
	}

	private void TxtMoneyNeed_TextChanged(object sender, EventArgs e)
	{
		if (Convert.ToDecimal(TxtMoneyNeed.Text) == 0m)
		{
			TxtMoneyPaying.Text = "0.00";
			TxtMoneyNet.Text = "0.00";
		}
		else
		{
			TxtMoneyNet.Text = (Convert.ToDecimal(TxtMoneyNeed.Text) - Convert.ToDecimal(TxtMoneyPaying.Text)).ToString();
		}
	}

	private void FrmMoneyCashier_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F2")
		{
			toggleCash.IsOn = true;
			toggleVisa.IsOn = false;
		}
		if (e.KeyCode.ToString() == "F3")
		{
			toggleCash.IsOn = false;
			toggleVisa.IsOn = true;
		}
		if (e.KeyCode.ToString() == "F4")
		{
			toggleCash.IsOn = true;
			toggleVisa.IsOn = true;
		}
		if (e.KeyCode.ToString() == "F5")
		{
			Save();
		}
		if (e.KeyCode.ToString() == "F6")
		{
			if (toggleCash.IsOn)
			{
				TxtMoneyPaying.Focus();
			}
			else
			{
				TxtVisa.Focus();
			}
		}
	}

	private void FrmMoneyCashier_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
	}

	private void Save()
	{
		try
		{
			if (LoginDetails.TotalFatorPrice == Convert.ToDecimal(TxtCash.Text) + Convert.ToDecimal(TxtVisa.Text))
			{
				if (Convert.ToDecimal(TxtMoneyNet.Text) <= 0m)
				{
					IsComplateMony = true;
					Close();
				}
				else
				{
					LoginDetails.IsComplateMony = false;
					MessageBox.Show("المبلغ المدفوع لا يساوى المبلغ المطلوب", "مستكشف السداد ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				LoginDetails.IsComplateMony = false;
				MessageBox.Show("المبلغ المسدد لا يساوى المبلغ المطلوب", "مستكشف السداد ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		catch (Exception)
		{
		}
	}

	private void TxtCash_Enter(object sender, EventArgs e)
	{
		ActiveTxt = "TxtCash";
	}

	private void TxtCash_Click(object sender, EventArgs e)
	{
	}

	private void TxtVisa_Enter(object sender, EventArgs e)
	{
		ActiveTxt = "TxtVisa";
	}

	private void TxtMoneyPaying_Enter(object sender, EventArgs e)
	{
		ActiveTxt = "TxtMoneyPaying";
	}

	private void BtnNumber(SimpleButton btn)
	{
		try
		{
			if (ActiveTxt == "TxtCash")
			{
				TxtCash.Text = Convert.ToInt32(TxtCash.Value) + btn.Text;
			}
			else if (ActiveTxt == "TxtVisa")
			{
				TxtVisa.Text = Convert.ToInt32(TxtVisa.Value) + btn.Text;
			}
			else if (ActiveTxt == "TxtMoneyPaying")
			{
				TxtMoneyPaying.Text = Convert.ToInt32(TxtMoneyPaying.Value) + btn.Text;
			}
		}
		catch (Exception)
		{
		}
	}

	private void CkSentInvoice_CheckedChanged(object sender, EventArgs e)
	{
		if (CkSentInvoice.Checked)
		{
			LoginDetails.SentInvoice = true;
		}
		else
		{
			LoginDetails.SentInvoice = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmCachMoney.FrmMoneyCashier));
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.toggleCash = new DevExpress.XtraEditors.ToggleSwitch();
		this.toggleVisa = new DevExpress.XtraEditors.ToggleSwitch();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TxtVisa = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtCash = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.CHPrint = new DevExpress.XtraEditors.CheckEdit();
		this.CHPrintA4 = new DevExpress.XtraEditors.CheckEdit();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.TxtMoneyPaying = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnClear = new DevExpress.XtraEditors.SimpleButton();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtMoneyNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtMoneyNeed = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.Btn8 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn7 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn9 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn5 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn4 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn6 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn2 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn1 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn3 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn000 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn0 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn00 = new DevExpress.XtraEditors.SimpleButton();
		this.CkSentInvoice = new DevExpress.XtraEditors.CheckEdit();
		((System.ComponentModel.ISupportInitialize)this.toggleCash.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleVisa.Properties).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.CHPrint.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CHPrintA4.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.CkSentInvoice.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.toggleCash, "toggleCash");
		this.toggleCash.Name = "toggleCash";
		this.toggleCash.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleCash.Properties.Appearance.Font");
		this.toggleCash.Properties.Appearance.Options.UseFont = true;
		this.toggleCash.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleCash.Properties.GlyphVerticalAlignment");
		this.toggleCash.Properties.OffText = resources.GetString("toggleCash.Properties.OffText");
		this.toggleCash.Properties.OnText = resources.GetString("toggleCash.Properties.OnText");
		this.toggleCash.Toggled += new System.EventHandler(toggleCash_Toggled);
		resources.ApplyResources(this.toggleVisa, "toggleVisa");
		this.toggleVisa.Name = "toggleVisa";
		this.toggleVisa.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleVisa.Properties.Appearance.Font");
		this.toggleVisa.Properties.Appearance.Options.UseFont = true;
		this.toggleVisa.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleVisa.Properties.GlyphVerticalAlignment");
		this.toggleVisa.Properties.OffText = resources.GetString("toggleVisa.Properties.OffText");
		this.toggleVisa.Properties.OnText = resources.GetString("toggleVisa.Properties.OnText");
		this.toggleVisa.Toggled += new System.EventHandler(toggleVisa_Toggled);
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.TxtVisa);
		this.groupBox1.Controls.Add(this.TxtCash);
		this.groupBox1.Controls.Add(this.toggleVisa);
		this.groupBox1.Controls.Add(this.toggleCash);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.TxtVisa, "TxtVisa");
		this.TxtVisa.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtVisa.DecimalDigits = 3;
		this.TxtVisa.ForeColor = System.Drawing.Color.Black;
		this.TxtVisa.Name = "TxtVisa";
		this.TxtVisa.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVisa.UseCompatibleTextRendering = false;
		this.TxtVisa.Value = new decimal(new int[4]);
		this.TxtVisa.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtVisa.TextChanged += new System.EventHandler(TxtVisa_TextChanged);
		this.TxtVisa.Enter += new System.EventHandler(TxtVisa_Enter);
		resources.ApplyResources(this.TxtCash, "TxtCash");
		this.TxtCash.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtCash.DecimalDigits = 3;
		this.TxtCash.ForeColor = System.Drawing.Color.Blue;
		this.TxtCash.Name = "TxtCash";
		this.TxtCash.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCash.UseCompatibleTextRendering = false;
		this.TxtCash.Value = new decimal(new int[4]);
		this.TxtCash.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtCash.TextChanged += new System.EventHandler(TxtCash_TextChanged);
		this.TxtCash.Click += new System.EventHandler(TxtCash_Click);
		this.TxtCash.Enter += new System.EventHandler(TxtCash_Enter);
		resources.ApplyResources(this.CHPrint, "CHPrint");
		this.CHPrint.Name = "CHPrint";
		this.CHPrint.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("CHPrint.Properties.Appearance.Font");
		this.CHPrint.Properties.Appearance.Options.UseFont = true;
		this.CHPrint.Properties.Caption = resources.GetString("CHPrint.Properties.Caption");
		this.CHPrint.Properties.DisplayValueChecked = resources.GetString("CHPrint.Properties.DisplayValueChecked");
		this.CHPrint.Properties.DisplayValueGrayed = resources.GetString("CHPrint.Properties.DisplayValueGrayed");
		this.CHPrint.Properties.DisplayValueUnchecked = resources.GetString("CHPrint.Properties.DisplayValueUnchecked");
		this.CHPrint.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("CHPrint.Properties.GlyphVerticalAlignment");
		resources.ApplyResources(this.CHPrintA4, "CHPrintA4");
		this.CHPrintA4.Name = "CHPrintA4";
		this.CHPrintA4.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("CHPrintA4.Properties.Appearance.Font");
		this.CHPrintA4.Properties.Appearance.Options.UseFont = true;
		this.CHPrintA4.Properties.Caption = resources.GetString("CHPrintA4.Properties.Caption");
		this.CHPrintA4.Properties.DisplayValueChecked = resources.GetString("CHPrintA4.Properties.DisplayValueChecked");
		this.CHPrintA4.Properties.DisplayValueGrayed = resources.GetString("CHPrintA4.Properties.DisplayValueGrayed");
		this.CHPrintA4.Properties.DisplayValueUnchecked = resources.GetString("CHPrintA4.Properties.DisplayValueUnchecked");
		this.CHPrintA4.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("CHPrintA4.Properties.GlyphVerticalAlignment");
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.TxtMoneyPaying);
		this.groupBox2.Controls.Add(this.BtnClear);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtMoneyNet);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.TxtMoneyNeed);
		this.groupBox2.Controls.Add(this.Btn8);
		this.groupBox2.Controls.Add(this.Btn7);
		this.groupBox2.Controls.Add(this.Btn9);
		this.groupBox2.Controls.Add(this.Btn5);
		this.groupBox2.Controls.Add(this.Btn4);
		this.groupBox2.Controls.Add(this.Btn6);
		this.groupBox2.Controls.Add(this.Btn2);
		this.groupBox2.Controls.Add(this.Btn1);
		this.groupBox2.Controls.Add(this.Btn3);
		this.groupBox2.Controls.Add(this.Btn000);
		this.groupBox2.Controls.Add(this.Btn0);
		this.groupBox2.Controls.Add(this.Btn00);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.TxtMoneyPaying, "TxtMoneyPaying");
		this.TxtMoneyPaying.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtMoneyPaying.DecimalDigits = 3;
		this.TxtMoneyPaying.ForeColor = System.Drawing.Color.Green;
		this.TxtMoneyPaying.Name = "TxtMoneyPaying";
		this.TxtMoneyPaying.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMoneyPaying.UseCompatibleTextRendering = false;
		this.TxtMoneyPaying.Value = new decimal(new int[4]);
		this.TxtMoneyPaying.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtMoneyPaying.TextChanged += new System.EventHandler(TxtCashNeed_TextChanged);
		this.TxtMoneyPaying.Enter += new System.EventHandler(TxtMoneyPaying_Enter);
		resources.ApplyResources(this.BtnClear, "BtnClear");
		this.BtnClear.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnClear.Appearance.Font");
		this.BtnClear.Appearance.Options.UseFont = true;
		this.BtnClear.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnClear.Name = "BtnClear";
		this.BtnClear.Click += new System.EventHandler(BtnClear_Click);
		resources.ApplyResources(this.label3, "label3");
		this.label3.ForeColor = System.Drawing.Color.Black;
		this.label3.Name = "label3";
		resources.ApplyResources(this.TxtMoneyNet, "TxtMoneyNet");
		this.TxtMoneyNet.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtMoneyNet.DecimalDigits = 3;
		this.TxtMoneyNet.ForeColor = System.Drawing.Color.Black;
		this.TxtMoneyNet.Name = "TxtMoneyNet";
		this.TxtMoneyNet.ReadOnly = true;
		this.TxtMoneyNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMoneyNet.UseCompatibleTextRendering = false;
		this.TxtMoneyNet.Value = new decimal(new int[4]);
		this.TxtMoneyNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label2, "label2");
		this.label2.ForeColor = System.Drawing.Color.Green;
		this.label2.Name = "label2";
		resources.ApplyResources(this.label1, "label1");
		this.label1.ForeColor = System.Drawing.Color.Blue;
		this.label1.Name = "label1";
		resources.ApplyResources(this.TxtMoneyNeed, "TxtMoneyNeed");
		this.TxtMoneyNeed.Cursor = System.Windows.Forms.Cursors.Default;
		this.TxtMoneyNeed.DecimalDigits = 3;
		this.TxtMoneyNeed.ForeColor = System.Drawing.Color.Blue;
		this.TxtMoneyNeed.Name = "TxtMoneyNeed";
		this.TxtMoneyNeed.ReadOnly = true;
		this.TxtMoneyNeed.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMoneyNeed.UseCompatibleTextRendering = false;
		this.TxtMoneyNeed.Value = new decimal(new int[4]);
		this.TxtMoneyNeed.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtMoneyNeed.TextChanged += new System.EventHandler(TxtMoneyNeed_TextChanged);
		resources.ApplyResources(this.Btn8, "Btn8");
		this.Btn8.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn8.Appearance.Font");
		this.Btn8.Appearance.Options.UseFont = true;
		this.Btn8.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn8.Name = "Btn8";
		this.Btn8.Click += new System.EventHandler(Btn8_Click);
		resources.ApplyResources(this.Btn7, "Btn7");
		this.Btn7.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn7.Appearance.Font");
		this.Btn7.Appearance.Options.UseFont = true;
		this.Btn7.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn7.Name = "Btn7";
		this.Btn7.Click += new System.EventHandler(Btn7_Click);
		resources.ApplyResources(this.Btn9, "Btn9");
		this.Btn9.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn9.Appearance.Font");
		this.Btn9.Appearance.Options.UseFont = true;
		this.Btn9.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn9.Name = "Btn9";
		this.Btn9.Click += new System.EventHandler(Btn9_Click);
		resources.ApplyResources(this.Btn5, "Btn5");
		this.Btn5.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn5.Appearance.Font");
		this.Btn5.Appearance.Options.UseFont = true;
		this.Btn5.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn5.Name = "Btn5";
		this.Btn5.Click += new System.EventHandler(Btn5_Click);
		resources.ApplyResources(this.Btn4, "Btn4");
		this.Btn4.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn4.Appearance.Font");
		this.Btn4.Appearance.Options.UseFont = true;
		this.Btn4.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn4.Name = "Btn4";
		this.Btn4.Click += new System.EventHandler(Btn4_Click);
		resources.ApplyResources(this.Btn6, "Btn6");
		this.Btn6.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn6.Appearance.Font");
		this.Btn6.Appearance.Options.UseFont = true;
		this.Btn6.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn6.Name = "Btn6";
		this.Btn6.Click += new System.EventHandler(Btn6_Click);
		resources.ApplyResources(this.Btn2, "Btn2");
		this.Btn2.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn2.Appearance.Font");
		this.Btn2.Appearance.Options.UseFont = true;
		this.Btn2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn2.Name = "Btn2";
		this.Btn2.Click += new System.EventHandler(Btn2_Click);
		resources.ApplyResources(this.Btn1, "Btn1");
		this.Btn1.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn1.Appearance.Font");
		this.Btn1.Appearance.Options.UseFont = true;
		this.Btn1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn1.Name = "Btn1";
		this.Btn1.Click += new System.EventHandler(Btn1_Click);
		resources.ApplyResources(this.Btn3, "Btn3");
		this.Btn3.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn3.Appearance.Font");
		this.Btn3.Appearance.Options.UseFont = true;
		this.Btn3.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn3.Name = "Btn3";
		this.Btn3.Click += new System.EventHandler(Btn3_Click);
		resources.ApplyResources(this.Btn000, "Btn000");
		this.Btn000.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn000.Appearance.Font");
		this.Btn000.Appearance.Options.UseFont = true;
		this.Btn000.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn000.Name = "Btn000";
		this.Btn000.Click += new System.EventHandler(Btn000_Click);
		resources.ApplyResources(this.Btn0, "Btn0");
		this.Btn0.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn0.Appearance.Font");
		this.Btn0.Appearance.Options.UseFont = true;
		this.Btn0.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn0.Name = "Btn0";
		this.Btn0.Click += new System.EventHandler(Btn0_Click);
		resources.ApplyResources(this.Btn00, "Btn00");
		this.Btn00.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn00.Appearance.Font");
		this.Btn00.Appearance.Options.UseFont = true;
		this.Btn00.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn00.Name = "Btn00";
		this.Btn00.Click += new System.EventHandler(Btn00_Click);
		resources.ApplyResources(this.CkSentInvoice, "CkSentInvoice");
		this.CkSentInvoice.Name = "CkSentInvoice";
		this.CkSentInvoice.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("CkSentInvoice.Properties.Appearance.Font");
		this.CkSentInvoice.Properties.Appearance.Options.UseFont = true;
		this.CkSentInvoice.Properties.Caption = resources.GetString("CkSentInvoice.Properties.Caption");
		this.CkSentInvoice.Properties.DisplayValueChecked = resources.GetString("CkSentInvoice.Properties.DisplayValueChecked");
		this.CkSentInvoice.Properties.DisplayValueGrayed = resources.GetString("CkSentInvoice.Properties.DisplayValueGrayed");
		this.CkSentInvoice.Properties.DisplayValueUnchecked = resources.GetString("CkSentInvoice.Properties.DisplayValueUnchecked");
		this.CkSentInvoice.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("CkSentInvoice.Properties.GlyphVerticalAlignment");
		this.CkSentInvoice.CheckedChanged += new System.EventHandler(CkSentInvoice_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.CkSentInvoice);
		base.Controls.Add(this.CHPrintA4);
		base.Controls.Add(this.CHPrint);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnSave);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmMoneyCashier";
		base.Load += new System.EventHandler(FrmMoneyCashier_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FrmMoneyCashier_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.toggleCash.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleVisa.Properties).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.CHPrint.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CHPrintA4.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.CkSentInvoice.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


