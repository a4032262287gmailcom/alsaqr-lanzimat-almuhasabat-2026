using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmVAT : XtraForm
{
	public bool ISFrmClosing = false;

	private int DecimalNum = LoginDetails.DecimalNum;

	private IContainer components = null;

	public NumericEditBox TxtVat;

	public NumericEditBox TxtVatNum;

	public NumericEditBox TxtNeed;

	public NumericEditBox TxtCut;

	private SimpleButton BtnSave;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	public FrmVAT()
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
	}

	private void TxtVat_ValueChanged(object sender, EventArgs e)
	{
		VAT();
	}

	private void TxtVatNum_ValueChanged(object sender, EventArgs e)
	{
		VAT();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		ISFrmClosing = true;
		Close();
	}

	private void VAT()
	{
		try
		{
			double num = Convert.ToDouble(TxtVat.Value);
			double num2 = Convert.ToDouble(TxtVatNum.Value) / 100.0;
			double num3 = num / 1.15;
			double num4 = num - num3;
			TxtNeed.Value = num3;
			TxtCut.Value = num4;
			if (!(num4 <= 0.0))
			{
			}
		}
		catch (Exception)
		{
		}
	}

	private void TxtDigital()
	{
		TxtVat.DecimalDigits = DecimalNum;
		TxtVatNum.DecimalDigits = DecimalNum;
		TxtNeed.DecimalDigits = DecimalNum;
		TxtCut.DecimalDigits = DecimalNum;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.FrmVAT));
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatNum = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtNeed = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtCut = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		resources.ApplyResources(this.TxtVat, "TxtVat");
		this.TxtVat.DecimalDigits = 3;
		this.TxtVat.ForeColor = System.Drawing.Color.Blue;
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat.UseCompatibleTextRendering = false;
		this.TxtVat.Value = new decimal(new int[4]);
		this.TxtVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtVat.ValueChanged += new System.EventHandler(TxtVat_ValueChanged);
		resources.ApplyResources(this.TxtVatNum, "TxtVatNum");
		this.TxtVatNum.DecimalDigits = 3;
		this.TxtVatNum.ForeColor = System.Drawing.Color.Green;
		this.TxtVatNum.Name = "TxtVatNum";
		this.TxtVatNum.ReadOnly = true;
		this.TxtVatNum.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatNum.UseCompatibleTextRendering = false;
		this.TxtVatNum.Value = new decimal(new int[4] { 15000, 0, 0, 196608 });
		this.TxtVatNum.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtVatNum.ValueChanged += new System.EventHandler(TxtVatNum_ValueChanged);
		resources.ApplyResources(this.TxtNeed, "TxtNeed");
		this.TxtNeed.DecimalDigits = 3;
		this.TxtNeed.ForeColor = System.Drawing.Color.Black;
		this.TxtNeed.Name = "TxtNeed";
		this.TxtNeed.ReadOnly = true;
		this.TxtNeed.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNeed.UseCompatibleTextRendering = false;
		this.TxtNeed.Value = new decimal(new int[4]);
		this.TxtNeed.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtCut, "TxtCut");
		this.TxtCut.DecimalDigits = 3;
		this.TxtCut.ForeColor = System.Drawing.Color.Red;
		this.TxtCut.Name = "TxtCut";
		this.TxtCut.ReadOnly = true;
		this.TxtCut.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCut.UseCompatibleTextRendering = false;
		this.TxtCut.Value = new decimal(new int[4]);
		this.TxtCut.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.TxtCut);
		base.Controls.Add(this.TxtNeed);
		base.Controls.Add(this.TxtVatNum);
		base.Controls.Add(this.TxtVat);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmVAT";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


