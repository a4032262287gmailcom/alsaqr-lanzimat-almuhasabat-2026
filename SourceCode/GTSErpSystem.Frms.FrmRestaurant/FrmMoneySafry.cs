using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Account;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.FrmRestaurant;

public class FrmMoneySafry : XtraForm
{
	public bool IsComplateMony = false;

	private double resultValue = 0.0;

	private string operationPerformed = "";

	private bool isOperationPerformed = false;

	private int DecimalNum = LoginDetails.DecimalNum;

	private IContainer components = null;

	private SimpleButton BtnSave;

	private SimpleButton BtnExit;

	private ToggleSwitch toggleCash;

	private ToggleSwitch toggleVisa;

	private GroupBox groupBox1;

	public NumericEditBox TxtVisa;

	public NumericEditBox TxtCash;

	private GroupBox groupBox2;

	private Label label3;

	public NumericEditBox TxtMoneyNet;

	private Label label2;

	private Label label1;

	public NumericEditBox TxtMoneyNeed;

	public NumericEditBox TxtMoneyPaying;

	public NumericEditBox TxtHanger;

	public ToggleSwitch ToggelHanger;

	private SimpleButton BtnCalcCachRecipt;

	private SimpleButton BtnCalcVisa;

	private SimpleButton BtnCalcCash;

	public FrmMoneySafry()
	{
		InitializeComponent();
		TxtDigital();
		IsComplateMony = false;
		TxtCash.Text = Convert.ToString(LoginDetails.TotalFatorPrice);
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave);
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
			if (!ToggelHanger.IsOn)
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
		}
		catch (Exception)
		{
		}
	}

	private void TxtVisa_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (!ToggelHanger.IsOn)
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
					return;
				}
				TxtVisa.Text = "0.00";
				TxtCash.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtVisa.Text)).ToString();
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
			if (ToggelHanger.IsOn)
			{
				return;
			}
			if (toggleCash.IsOn)
			{
				TxtCash.ReadOnly = false;
				TxtVisa.ReadOnly = false;
				BtnCalcCash.Enabled = true;
				BtnCalcCachRecipt.Enabled = true;
				if (!toggleVisa.IsOn)
				{
					TxtVisa.Text = "0.00";
					TxtVisa.Enabled = false;
					TxtCash.Enabled = true;
					TxtCash.ReadOnly = false;
					BtnCalcVisa.Enabled = false;
				}
				else if (toggleVisa.IsOn)
				{
					TxtCash.Text = LoginDetails.TotalFatorPrice.ToString();
					TxtVisa.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtCash.Text)).ToString();
					TxtVisa.Enabled = true;
					TxtCash.Enabled = true;
					BtnCalcVisa.Enabled = true;
				}
			}
			else if (!toggleCash.IsOn)
			{
				BtnCalcCash.Enabled = false;
				BtnCalcCachRecipt.Enabled = false;
				TxtVisa.ReadOnly = true;
				TxtCash.Text = "0.00";
				TxtCash.Enabled = false;
				toggleVisa.IsOn = true;
				TxtVisa.Enabled = true;
				TxtCash.ReadOnly = true;
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
			if (ToggelHanger.IsOn)
			{
				return;
			}
			if (toggleVisa.IsOn)
			{
				TxtVisa.ReadOnly = false;
				TxtCash.ReadOnly = false;
				BtnCalcVisa.Enabled = true;
				if (!toggleCash.IsOn)
				{
					TxtCash.Text = "0.00";
					TxtCash.Enabled = false;
					TxtVisa.Enabled = true;
					TxtVisa.ReadOnly = false;
					BtnCalcCash.Enabled = false;
					BtnCalcCachRecipt.Enabled = false;
				}
				else if (toggleCash.IsOn)
				{
					TxtVisa.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtCash.Text)).ToString();
					TxtVisa.Enabled = true;
					TxtCash.Enabled = true;
					BtnCalcCash.Enabled = true;
					BtnCalcCachRecipt.Enabled = true;
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
				BtnCalcVisa.Enabled = false;
			}
		}
		catch (Exception)
		{
		}
	}

	private void TxtDigital()
	{
		TxtCash.DecimalDigits = DecimalNum;
		TxtVisa.DecimalDigits = DecimalNum;
		TxtHanger.DecimalDigits = DecimalNum;
		TxtMoneyNeed.DecimalDigits = DecimalNum;
		TxtMoneyNet.DecimalDigits = DecimalNum;
		TxtMoneyPaying.DecimalDigits = DecimalNum;
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		try
		{
			if (ToggelHanger.IsOn)
			{
				IsComplateMony = true;
				Close();
			}
			else if (LoginDetails.TotalFatorPrice == Convert.ToDecimal(TxtCash.Text) + Convert.ToDecimal(TxtVisa.Text))
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

	private void BtnExit_Click(object sender, EventArgs e)
	{
		LoginDetails.IsComplateMony = false;
		Close();
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

	private void ToggelHanger_Toggled(object sender, EventArgs e)
	{
		if (ToggelHanger.IsOn)
		{
			BtnCalcVisa.Enabled = false;
			BtnCalcCash.Enabled = false;
			toggleCash.IsOn = false;
			toggleVisa.IsOn = false;
			toggleCash.Enabled = false;
			toggleVisa.Enabled = false;
			TxtVisa.Enabled = false;
			TxtCash.Enabled = false;
			TxtHanger.Enabled = true;
			TxtVisa.Text = "0.00";
			TxtCash.Text = "0.00";
			TxtMoneyNeed.Text = "0.00";
			TxtMoneyPaying.Text = "0.00";
			TxtMoneyNet.Text = "0.00";
			TxtHanger.Text = LoginDetails.TotalFatorPrice.ToString();
			groupBox2.Enabled = false;
		}
		else
		{
			BtnCalcVisa.Enabled = true;
			BtnCalcCash.Enabled = true;
			groupBox2.Enabled = true;
			TxtHanger.Enabled = false;
			toggleCash.Enabled = true;
			toggleVisa.Enabled = true;
			TxtHanger.Text = "0.00";
			TxtVisa.Text = "0.00";
			TxtCash.Text = LoginDetails.TotalFatorPrice.ToString();
			toggleVisa.IsOn = false;
			toggleCash.IsOn = true;
			ToggleIsOn();
		}
	}

	private void TxtMoneyPaying_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void BtnCalcCash_Click(object sender, EventArgs e)
	{
		FrmCalculator frmCalculator = new FrmCalculator();
		frmCalculator.ShowDialog();
		TxtCash.Text = frmCalculator.textBox_Result.Text;
	}

	private void BtnCalcVisa_Click(object sender, EventArgs e)
	{
		FrmCalculator frmCalculator = new FrmCalculator();
		frmCalculator.ShowDialog();
		TxtVisa.Text = frmCalculator.textBox_Result.Text;
	}

	private void BtnCalcNeedCash_Click(object sender, EventArgs e)
	{
		FrmCalculator frmCalculator = new FrmCalculator();
		frmCalculator.ShowDialog();
		TxtMoneyNeed.Text = frmCalculator.textBox_Result.Text;
	}

	private void BtnCalcCachRecipt_Click(object sender, EventArgs e)
	{
		FrmCalculator frmCalculator = new FrmCalculator();
		frmCalculator.ShowDialog();
		TxtMoneyPaying.Text = frmCalculator.textBox_Result.Text;
	}

	private void BtnCalcNet_Click(object sender, EventArgs e)
	{
		FrmCalculator frmCalculator = new FrmCalculator();
		frmCalculator.ShowDialog();
		TxtMoneyNet.Text = frmCalculator.textBox_Result.Text;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmRestaurant.FrmMoneySafry));
		this.toggleCash = new DevExpress.XtraEditors.ToggleSwitch();
		this.toggleVisa = new DevExpress.XtraEditors.ToggleSwitch();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnCalcVisa = new DevExpress.XtraEditors.SimpleButton();
		this.ToggelHanger = new DevExpress.XtraEditors.ToggleSwitch();
		this.BtnCalcCash = new DevExpress.XtraEditors.SimpleButton();
		this.TxtHanger = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVisa = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtCash = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnCalcCachRecipt = new DevExpress.XtraEditors.SimpleButton();
		this.TxtMoneyPaying = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtMoneyNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtMoneyNeed = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.toggleCash.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleVisa.Properties).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ToggelHanger.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.toggleCash.EditValue = true;
		this.toggleCash.Location = new System.Drawing.Point(364, 34);
		this.toggleCash.Name = "toggleCash";
		this.toggleCash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.toggleCash.Properties.Appearance.Options.UseFont = true;
		this.toggleCash.Properties.OffText = "إلغاء النقدي";
		this.toggleCash.Properties.OnText = "نقدي";
		this.toggleCash.Size = new System.Drawing.Size(226, 25);
		this.toggleCash.TabIndex = 48;
		this.toggleCash.Toggled += new System.EventHandler(toggleCash_Toggled);
		this.toggleVisa.Location = new System.Drawing.Point(364, 95);
		this.toggleVisa.Name = "toggleVisa";
		this.toggleVisa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.toggleVisa.Properties.Appearance.Options.UseFont = true;
		this.toggleVisa.Properties.OffText = "إلغاء الشبكة";
		this.toggleVisa.Properties.OnText = "شبكة";
		this.toggleVisa.Size = new System.Drawing.Size(226, 25);
		this.toggleVisa.TabIndex = 49;
		this.toggleVisa.Toggled += new System.EventHandler(toggleVisa_Toggled);
		this.groupBox1.Controls.Add(this.BtnCalcVisa);
		this.groupBox1.Controls.Add(this.BtnCalcCash);
		this.groupBox1.Controls.Add(this.TxtVisa);
		this.groupBox1.Controls.Add(this.TxtCash);
		this.groupBox1.Controls.Add(this.toggleVisa);
		this.groupBox1.Controls.Add(this.toggleCash);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(603, 146);
		this.groupBox1.TabIndex = 67;
		this.groupBox1.TabStop = false;
		this.BtnCalcVisa.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.BtnCalcVisa.Enabled = false;
		this.BtnCalcVisa.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcVisa.ImageOptions.Image");
		this.BtnCalcVisa.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcVisa.Location = new System.Drawing.Point(299, 88);
		this.BtnCalcVisa.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCalcVisa.Name = "BtnCalcVisa";
		this.BtnCalcVisa.Size = new System.Drawing.Size(70, 38);
		this.BtnCalcVisa.TabIndex = 82;
		this.BtnCalcVisa.Text = "Calc";
		this.BtnCalcVisa.Click += new System.EventHandler(BtnCalcVisa_Click);
		this.ToggelHanger.Location = new System.Drawing.Point(352, 217);
		this.ToggelHanger.Name = "ToggelHanger";
		this.ToggelHanger.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.ToggelHanger.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.ToggelHanger.Properties.Appearance.Options.UseFont = true;
		this.ToggelHanger.Properties.Appearance.Options.UseForeColor = true;
		this.ToggelHanger.Properties.OffText = "إلغاء الهنقرستيشن";
		this.ToggelHanger.Properties.OnText = "هنقرستيشن";
		this.ToggelHanger.Size = new System.Drawing.Size(248, 25);
		this.ToggelHanger.TabIndex = 54;
		this.ToggelHanger.Visible = false;
		this.ToggelHanger.Toggled += new System.EventHandler(ToggelHanger_Toggled);
		this.BtnCalcCash.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.BtnCalcCash.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcCash.ImageOptions.Image");
		this.BtnCalcCash.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcCash.Location = new System.Drawing.Point(299, 31);
		this.BtnCalcCash.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCalcCash.Name = "BtnCalcCash";
		this.BtnCalcCash.Size = new System.Drawing.Size(70, 38);
		this.BtnCalcCash.TabIndex = 81;
		this.BtnCalcCash.Text = "Calc";
		this.BtnCalcCash.Click += new System.EventHandler(BtnCalcCash_Click);
		this.TxtHanger.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtHanger.DecimalDigits = 3;
		this.TxtHanger.Enabled = false;
		this.TxtHanger.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtHanger.ForeColor = System.Drawing.Color.Red;
		this.TxtHanger.Location = new System.Drawing.Point(28, 212);
		this.TxtHanger.Name = "TxtHanger";
		this.TxtHanger.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtHanger.Size = new System.Drawing.Size(274, 38);
		this.TxtHanger.TabIndex = 53;
		this.TxtHanger.Text = "0.000";
		this.TxtHanger.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtHanger.UseCompatibleTextRendering = false;
		this.TxtHanger.Value = new decimal(new int[4]);
		this.TxtHanger.Visible = false;
		this.TxtHanger.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtVisa.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtVisa.DecimalDigits = 3;
		this.TxtVisa.Enabled = false;
		this.TxtVisa.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtVisa.ForeColor = System.Drawing.Color.Black;
		this.TxtVisa.Location = new System.Drawing.Point(18, 88);
		this.TxtVisa.Name = "TxtVisa";
		this.TxtVisa.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVisa.Size = new System.Drawing.Size(274, 38);
		this.TxtVisa.TabIndex = 52;
		this.TxtVisa.Text = "0.000";
		this.TxtVisa.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVisa.UseCompatibleTextRendering = false;
		this.TxtVisa.Value = new decimal(new int[4]);
		this.TxtVisa.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtVisa.TextChanged += new System.EventHandler(TxtVisa_TextChanged);
		this.TxtCash.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCash.DecimalDigits = 3;
		this.TxtCash.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtCash.ForeColor = System.Drawing.Color.Blue;
		this.TxtCash.Location = new System.Drawing.Point(18, 31);
		this.TxtCash.Name = "TxtCash";
		this.TxtCash.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtCash.Size = new System.Drawing.Size(274, 38);
		this.TxtCash.TabIndex = 51;
		this.TxtCash.Text = "0.000";
		this.TxtCash.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCash.UseCompatibleTextRendering = false;
		this.TxtCash.Value = new decimal(new int[4]);
		this.TxtCash.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtCash.TextChanged += new System.EventHandler(TxtCash_TextChanged);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox2.Controls.Add(this.BtnCalcCachRecipt);
		this.groupBox2.Controls.Add(this.TxtMoneyPaying);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtMoneyNet);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.TxtMoneyNeed);
		this.groupBox2.Location = new System.Drawing.Point(12, 159);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(603, 183);
		this.groupBox2.TabIndex = 68;
		this.groupBox2.TabStop = false;
		this.BtnCalcCachRecipt.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.BtnCalcCachRecipt.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcCachRecipt.ImageOptions.Image");
		this.BtnCalcCachRecipt.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcCachRecipt.Location = new System.Drawing.Point(299, 72);
		this.BtnCalcCachRecipt.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCalcCachRecipt.Name = "BtnCalcCachRecipt";
		this.BtnCalcCachRecipt.Size = new System.Drawing.Size(70, 38);
		this.BtnCalcCachRecipt.TabIndex = 93;
		this.BtnCalcCachRecipt.Text = "Calc";
		this.BtnCalcCachRecipt.Click += new System.EventHandler(BtnCalcCachRecipt_Click);
		this.TxtMoneyPaying.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtMoneyPaying.DecimalDigits = 3;
		this.TxtMoneyPaying.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtMoneyPaying.ForeColor = System.Drawing.Color.Green;
		this.TxtMoneyPaying.Location = new System.Drawing.Point(18, 72);
		this.TxtMoneyPaying.Name = "TxtMoneyPaying";
		this.TxtMoneyPaying.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMoneyPaying.Size = new System.Drawing.Size(274, 38);
		this.TxtMoneyPaying.TabIndex = 92;
		this.TxtMoneyPaying.Text = "0.000";
		this.TxtMoneyPaying.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMoneyPaying.UseCompatibleTextRendering = false;
		this.TxtMoneyPaying.Value = new decimal(new int[4]);
		this.TxtMoneyPaying.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtMoneyPaying.TextChanged += new System.EventHandler(TxtCashNeed_TextChanged);
		this.TxtMoneyPaying.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtMoneyPaying_KeyPress);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label3.ForeColor = System.Drawing.Color.Black;
		this.label3.Location = new System.Drawing.Point(395, 128);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(113, 21);
		this.label3.TabIndex = 90;
		this.label3.Text = "المبلغ المتبقي";
		this.TxtMoneyNet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtMoneyNet.DecimalDigits = 3;
		this.TxtMoneyNet.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtMoneyNet.ForeColor = System.Drawing.Color.Black;
		this.TxtMoneyNet.Location = new System.Drawing.Point(18, 122);
		this.TxtMoneyNet.Name = "TxtMoneyNet";
		this.TxtMoneyNet.ReadOnly = true;
		this.TxtMoneyNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMoneyNet.Size = new System.Drawing.Size(274, 38);
		this.TxtMoneyNet.TabIndex = 89;
		this.TxtMoneyNet.Text = "0.000";
		this.TxtMoneyNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMoneyNet.UseCompatibleTextRendering = false;
		this.TxtMoneyNet.Value = new decimal(new int[4]);
		this.TxtMoneyNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label2.ForeColor = System.Drawing.Color.Green;
		this.label2.Location = new System.Drawing.Point(395, 77);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(109, 21);
		this.label2.TabIndex = 88;
		this.label2.Text = "المبلغ المدفوع";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label1.ForeColor = System.Drawing.Color.Blue;
		this.label1.Location = new System.Drawing.Point(392, 27);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(112, 21);
		this.label1.TabIndex = 87;
		this.label1.Text = "المطلوب نقدي";
		this.TxtMoneyNeed.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtMoneyNeed.DecimalDigits = 3;
		this.TxtMoneyNeed.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtMoneyNeed.ForeColor = System.Drawing.Color.Blue;
		this.TxtMoneyNeed.Location = new System.Drawing.Point(18, 22);
		this.TxtMoneyNeed.Name = "TxtMoneyNeed";
		this.TxtMoneyNeed.ReadOnly = true;
		this.TxtMoneyNeed.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMoneyNeed.Size = new System.Drawing.Size(274, 38);
		this.TxtMoneyNeed.TabIndex = 85;
		this.TxtMoneyNeed.Text = "0.000";
		this.TxtMoneyNeed.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMoneyNeed.UseCompatibleTextRendering = false;
		this.TxtMoneyNeed.Value = new decimal(new int[4]);
		this.TxtMoneyNeed.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtMoneyNeed.TextChanged += new System.EventHandler(TxtMoneyNeed_TextChanged);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(323, 349);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(274, 56);
		this.BtnExit.TabIndex = 48;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(25, 349);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(277, 56);
		this.BtnSave.TabIndex = 47;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(627, 418);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.ToggelHanger);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.TxtHanger);
		base.Controls.Add(this.BtnSave);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmMoneySafry";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "السداد";
		((System.ComponentModel.ISupportInitialize)this.toggleCash.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleVisa.Properties).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ToggelHanger.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


