using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.Orders.FrmCachMoney;

public class FrmCashierQuantity : XtraForm
{
	private int DecimalNum = LoginDetails.DecimalNum;

	private IContainer components = null;

	private SimpleButton Btn7;

	private SimpleButton Btn8;

	private SimpleButton Btn9;

	private SimpleButton Btn3;

	private SimpleButton Btn2;

	private SimpleButton Btn1;

	private SimpleButton Btn6;

	private SimpleButton Btn5;

	private SimpleButton Btn4;

	private SimpleButton Btn0;

	private SimpleButton BtnPoint;

	private SimpleButton BtnClear;

	private SimpleButton BtnOK;

	public NumericEditBox TxtQuant;

	public FrmCashierQuantity()
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
		TxtQuant.DecimalDigits = DecimalNum;
	}

	private void BtnClear_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Text = "";
		}
		catch (Exception)
		{
		}
	}

	private void Btn00_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Text += ".";
		}
		catch (Exception)
		{
		}
	}

	private void Btn0_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Value = Convert.ToInt32(TxtQuant.Value) + "0";
		}
		catch (Exception)
		{
		}
	}

	private void Btn1_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Value = TxtQuant.Value?.ToString() + "1";
		}
		catch (Exception)
		{
		}
	}

	private void Btn2_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Value = Convert.ToInt32(TxtQuant.Value) + "2";
		}
		catch (Exception)
		{
		}
	}

	private void Btn3_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Value = Convert.ToInt32(TxtQuant.Value) + "3";
		}
		catch (Exception)
		{
		}
	}

	private void Btn4_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Value = Convert.ToInt32(TxtQuant.Value) + "4";
		}
		catch (Exception)
		{
		}
	}

	private void Btn5_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Value = Convert.ToInt32(TxtQuant.Value) + "5";
		}
		catch (Exception)
		{
		}
	}

	private void Btn6_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Value = Convert.ToInt32(TxtQuant.Value) + "6";
		}
		catch (Exception)
		{
		}
	}

	private void Btn7_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Value = Convert.ToInt32(TxtQuant.Value) + "7";
		}
		catch (Exception)
		{
		}
	}

	private void Btn8_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Value = Convert.ToInt32(TxtQuant.Value) + "8";
		}
		catch (Exception)
		{
		}
	}

	private void Btn9_Click(object sender, EventArgs e)
	{
		try
		{
			TxtQuant.Value = Convert.ToInt32(TxtQuant.Value) + "9";
		}
		catch (Exception)
		{
		}
	}

	private void TxtNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmCachMoney.FrmCashierQuantity));
		this.Btn7 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn8 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn9 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn3 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn2 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn1 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn6 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn5 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn4 = new DevExpress.XtraEditors.SimpleButton();
		this.Btn0 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPoint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnClear = new DevExpress.XtraEditors.SimpleButton();
		this.BtnOK = new DevExpress.XtraEditors.SimpleButton();
		this.TxtQuant = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		base.SuspendLayout();
		resources.ApplyResources(this.Btn7, "Btn7");
		this.Btn7.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn7.Appearance.Font");
		this.Btn7.Appearance.Options.UseFont = true;
		this.Btn7.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn7.Name = "Btn7";
		this.Btn7.Click += new System.EventHandler(Btn7_Click);
		resources.ApplyResources(this.Btn8, "Btn8");
		this.Btn8.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn8.Appearance.Font");
		this.Btn8.Appearance.Options.UseFont = true;
		this.Btn8.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn8.Name = "Btn8";
		this.Btn8.Click += new System.EventHandler(Btn8_Click);
		resources.ApplyResources(this.Btn9, "Btn9");
		this.Btn9.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn9.Appearance.Font");
		this.Btn9.Appearance.Options.UseFont = true;
		this.Btn9.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn9.Name = "Btn9";
		this.Btn9.Click += new System.EventHandler(Btn9_Click);
		resources.ApplyResources(this.Btn3, "Btn3");
		this.Btn3.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn3.Appearance.Font");
		this.Btn3.Appearance.Options.UseFont = true;
		this.Btn3.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn3.Name = "Btn3";
		this.Btn3.Click += new System.EventHandler(Btn3_Click);
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
		resources.ApplyResources(this.Btn6, "Btn6");
		this.Btn6.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn6.Appearance.Font");
		this.Btn6.Appearance.Options.UseFont = true;
		this.Btn6.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn6.Name = "Btn6";
		this.Btn6.Click += new System.EventHandler(Btn6_Click);
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
		resources.ApplyResources(this.Btn0, "Btn0");
		this.Btn0.Appearance.Font = (System.Drawing.Font)resources.GetObject("Btn0.Appearance.Font");
		this.Btn0.Appearance.Options.UseFont = true;
		this.Btn0.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.Btn0.Name = "Btn0";
		this.Btn0.Click += new System.EventHandler(Btn0_Click);
		resources.ApplyResources(this.BtnPoint, "BtnPoint");
		this.BtnPoint.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPoint.Appearance.Font");
		this.BtnPoint.Appearance.Options.UseFont = true;
		this.BtnPoint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPoint.Name = "BtnPoint";
		this.BtnPoint.Click += new System.EventHandler(Btn00_Click);
		resources.ApplyResources(this.BtnClear, "BtnClear");
		this.BtnClear.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnClear.Appearance.Font");
		this.BtnClear.Appearance.Options.UseFont = true;
		this.BtnClear.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnClear.Name = "BtnClear";
		this.BtnClear.Click += new System.EventHandler(BtnClear_Click);
		resources.ApplyResources(this.BtnOK, "BtnOK");
		this.BtnOK.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnOK.Appearance.Font");
		this.BtnOK.Appearance.Options.UseFont = true;
		this.BtnOK.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOK.ImageOptions.Image");
		this.BtnOK.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnOK.Name = "BtnOK";
		resources.ApplyResources(this.TxtQuant, "TxtQuant");
		this.TxtQuant.DecimalDigits = 3;
		this.TxtQuant.ForeColor = System.Drawing.Color.Black;
		this.TxtQuant.Name = "TxtQuant";
		this.TxtQuant.ReadOnly = true;
		this.TxtQuant.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtQuant.UseCompatibleTextRendering = false;
		this.TxtQuant.Value = new decimal(new int[4]);
		this.TxtQuant.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.TxtQuant);
		base.Controls.Add(this.BtnOK);
		base.Controls.Add(this.BtnClear);
		base.Controls.Add(this.BtnPoint);
		base.Controls.Add(this.Btn0);
		base.Controls.Add(this.Btn6);
		base.Controls.Add(this.Btn5);
		base.Controls.Add(this.Btn4);
		base.Controls.Add(this.Btn3);
		base.Controls.Add(this.Btn2);
		base.Controls.Add(this.Btn1);
		base.Controls.Add(this.Btn9);
		base.Controls.Add(this.Btn8);
		base.Controls.Add(this.Btn7);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmCashierQuantity";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


