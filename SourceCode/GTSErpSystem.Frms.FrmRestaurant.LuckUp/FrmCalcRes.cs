using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.FrmRestaurant.LuckUp;

public class FrmCalcRes : XtraForm
{
	private IContainer components = null;

	private GroupBox groupBox2;

	public NumericEditBox TxtMoneyPaying;

	private Label label3;

	public NumericEditBox TxtMoneyNet;

	private Label label2;

	private Label label1;

	public NumericEditBox TxtMoneyNeed;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	public FrmCalcRes()
	{
		InitializeComponent();
		TxtMoneyNeed.Text = LoginDetails.TotalCalc.ToString();
		TxtMoneyPaying.Focus();
	}

	private void TxtMoneyNeed_TextChanged(object sender, EventArgs e)
	{
	}

	private void TxtMoneyPaying_TextChanged(object sender, EventArgs e)
	{
		try
		{
			TxtMoneyNet.Text = (LoginDetails.TotalCalc - Convert.ToDecimal(TxtMoneyPaying.Text)).ToString();
		}
		catch (Exception)
		{
			TxtMoneyNet.Text = "0.00";
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		LoginDetails.NetCalc = Convert.ToDecimal(TxtMoneyNet.Text);
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmRestaurant.LuckUp.FrmCalcRes));
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.TxtMoneyPaying = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtMoneyNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtMoneyNeed = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox2.Controls.Add(this.TxtMoneyPaying);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtMoneyNet);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.TxtMoneyNeed);
		this.groupBox2.Location = new System.Drawing.Point(12, 12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(603, 183);
		this.groupBox2.TabIndex = 69;
		this.groupBox2.TabStop = false;
		this.TxtMoneyPaying.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtMoneyPaying.DecimalDigits = 2;
		this.TxtMoneyPaying.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtMoneyPaying.ForeColor = System.Drawing.Color.Green;
		this.TxtMoneyPaying.Location = new System.Drawing.Point(18, 72);
		this.TxtMoneyPaying.Name = "TxtMoneyPaying";
		this.TxtMoneyPaying.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMoneyPaying.Size = new System.Drawing.Size(274, 38);
		this.TxtMoneyPaying.TabIndex = 92;
		this.TxtMoneyPaying.Text = "0.00";
		this.TxtMoneyPaying.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMoneyPaying.UseCompatibleTextRendering = false;
		this.TxtMoneyPaying.Value = new decimal(new int[4]);
		this.TxtMoneyPaying.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtMoneyPaying.TextChanged += new System.EventHandler(TxtMoneyPaying_TextChanged);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label3.ForeColor = System.Drawing.Color.Black;
		this.label3.Location = new System.Drawing.Point(395, 128);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(113, 21);
		this.label3.TabIndex = 90;
		this.label3.Text = "المبلغ المتبقي";
		this.TxtMoneyNet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtMoneyNet.DecimalDigits = 2;
		this.TxtMoneyNet.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtMoneyNet.ForeColor = System.Drawing.Color.Black;
		this.TxtMoneyNet.Location = new System.Drawing.Point(18, 122);
		this.TxtMoneyNet.Name = "TxtMoneyNet";
		this.TxtMoneyNet.ReadOnly = true;
		this.TxtMoneyNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMoneyNet.Size = new System.Drawing.Size(274, 38);
		this.TxtMoneyNet.TabIndex = 89;
		this.TxtMoneyNet.Text = "0.00";
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
		this.label1.Location = new System.Drawing.Point(416, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(74, 21);
		this.label1.TabIndex = 87;
		this.label1.Text = "المطلوب ";
		this.TxtMoneyNeed.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtMoneyNeed.DecimalDigits = 2;
		this.TxtMoneyNeed.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtMoneyNeed.ForeColor = System.Drawing.Color.Blue;
		this.TxtMoneyNeed.Location = new System.Drawing.Point(18, 22);
		this.TxtMoneyNeed.Name = "TxtMoneyNeed";
		this.TxtMoneyNeed.ReadOnly = true;
		this.TxtMoneyNeed.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMoneyNeed.Size = new System.Drawing.Size(274, 38);
		this.TxtMoneyNeed.TabIndex = 85;
		this.TxtMoneyNeed.Text = "0.00";
		this.TxtMoneyNeed.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMoneyNeed.UseCompatibleTextRendering = false;
		this.TxtMoneyNeed.Value = new decimal(new int[4]);
		this.TxtMoneyNeed.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(323, 202);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(274, 56);
		this.BtnExit.TabIndex = 71;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(25, 202);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(277, 56);
		this.BtnSave.TabIndex = 70;
		this.BtnSave.Text = "موافق";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		base.Appearance.BackColor = System.Drawing.Color.White;
		base.Appearance.Options.UseBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(621, 271);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.groupBox2);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmCalcRes";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "حاسبة الاسعار";
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}


