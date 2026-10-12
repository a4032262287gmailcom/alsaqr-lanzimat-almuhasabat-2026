using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GTSErpSystem.Frms.FrmRestaurant;

public class FrmPrint : XtraForm
{
	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox3;

	private GroupBox groupBox2;

	public CheckBox CKRest;

	public CheckBox CKCust;

	private SimpleButton BtnPrint;

	private GroupBox groupBox4;

	public CheckBox CKCustPrintSecond;

	public FrmPrint()
	{
		InitializeComponent();
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmRestaurant.FrmPrint));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.CKRest = new System.Windows.Forms.CheckBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.CKCust = new System.Windows.Forms.CheckBox();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.CKCustPrintSecond = new System.Windows.Forms.CheckBox();
		this.groupBox1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.groupBox3);
		this.groupBox1.Controls.Add(this.groupBox4);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(373, 171);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox3.Controls.Add(this.CKRest);
		this.groupBox3.Location = new System.Drawing.Point(18, 22);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(149, 62);
		this.groupBox3.TabIndex = 2;
		this.groupBox3.TabStop = false;
		this.CKRest.AutoSize = true;
		this.CKRest.Location = new System.Drawing.Point(17, 22);
		this.CKRest.Name = "CKRest";
		this.CKRest.Size = new System.Drawing.Size(107, 21);
		this.CKRest.TabIndex = 0;
		this.CKRest.Text = "فاتورة المطبخ";
		this.CKRest.UseVisualStyleBackColor = true;
		this.groupBox2.Controls.Add(this.CKCust);
		this.groupBox2.Location = new System.Drawing.Point(206, 22);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(149, 62);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.CKCust.AutoSize = true;
		this.CKCust.Checked = true;
		this.CKCust.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CKCust.Location = new System.Drawing.Point(17, 22);
		this.CKCust.Name = "CKCust";
		this.CKCust.Size = new System.Drawing.Size(104, 21);
		this.CKCust.TabIndex = 0;
		this.CKCust.Text = "فاتورة العميل";
		this.CKCust.UseVisualStyleBackColor = true;
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(30, 190);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(337, 43);
		this.BtnPrint.TabIndex = 112;
		this.BtnPrint.Text = "طباعة";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.groupBox4.Controls.Add(this.CKCustPrintSecond);
		this.groupBox4.Location = new System.Drawing.Point(18, 90);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(337, 62);
		this.groupBox4.TabIndex = 1;
		this.groupBox4.TabStop = false;
		this.CKCustPrintSecond.AutoSize = true;
		this.CKCustPrintSecond.Location = new System.Drawing.Point(124, 22);
		this.CKCustPrintSecond.Name = "CKCustPrintSecond";
		this.CKCustPrintSecond.Size = new System.Drawing.Size(185, 21);
		this.CKCustPrintSecond.TabIndex = 0;
		this.CKCustPrintSecond.Text = "فاتورة العميل الطابعة الثانية";
		this.CKCustPrintSecond.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(397, 246);
		base.Controls.Add(this.BtnPrint);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.IconOptions.Image = (System.Drawing.Image)resources.GetObject("FrmPrint.IconOptions.Image");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmPrint";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "طباعة الفواتير";
		this.groupBox1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
	}
}


