using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QRCoder;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmAbout : XtraForm
{
	private IContainer components = null;

	private SimpleButton BtnOk;

	private Label label1;

	private Label label4;

	private LinkLabel linkLabel1;

	private Label LDateExpir;

	private Label label2;

	private Label label3;

	private Label label5;

	private PictureBox pictureBoxAndroid;

	private PictureBox pictureBoxIos;

	private Label label6;

	public FrmAbout()
	{
		InitializeComponent();
		LDateExpir.Text = LoginDetails.Active_DateTo.ToShortDateString();
	}

	private void BtnOk_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void linkLabel1_Click(object sender, EventArgs e)
	{
		linkLabel1.LinkVisited = true;
		Process.Start("http://www.gts-sa.net");
	}

	private void GenerateQrCode(string url, PictureBox pictureBox)
	{
		QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
		QRCodeData data = qRCodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.L);
		QRCode qRCode = new QRCode(data);
		Bitmap graphic = qRCode.GetGraphic(20, Color.Black, Color.White, true);
		pictureBox.Image = graphic;
		pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
	}

	private void FrmAbout_Load(object sender, EventArgs e)
	{
		GenerateQrCode("https://play.google.com/store/apps/details?id=com.test.android", pictureBoxAndroid);
		GenerateQrCode("https://drive.google.com/file/d/1xdwR5k3-MSyOa2wkCtmFJDXRgU9Irzis/view?usp=sharing", pictureBoxIos);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.FrmAbout));
		this.label1 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.BtnOk = new DevExpress.XtraEditors.SimpleButton();
		this.LDateExpir = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.pictureBoxAndroid = new System.Windows.Forms.PictureBox();
		this.pictureBoxIos = new System.Windows.Forms.PictureBox();
		this.label6 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxAndroid).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxIos).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.label1, "label1");
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Name = "label1";
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.linkLabel1, "linkLabel1");
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Click += new System.EventHandler(linkLabel1_Click);
		resources.ApplyResources(this.BtnOk, "BtnOk");
		this.BtnOk.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnOk.Appearance.Font");
		this.BtnOk.Appearance.Options.UseFont = true;
		this.BtnOk.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOk.ImageOptions.Image");
		this.BtnOk.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnOk.Name = "BtnOk";
		this.BtnOk.Click += new System.EventHandler(BtnOk_Click);
		resources.ApplyResources(this.LDateExpir, "LDateExpir");
		this.LDateExpir.Name = "LDateExpir";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.pictureBoxAndroid, "pictureBoxAndroid");
		this.pictureBoxAndroid.Name = "pictureBoxAndroid";
		this.pictureBoxAndroid.TabStop = false;
		resources.ApplyResources(this.pictureBoxIos, "pictureBoxIos");
		this.pictureBoxIos.Name = "pictureBoxIos";
		this.pictureBoxIos.TabStop = false;
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.label6);
		base.Controls.Add(this.pictureBoxIos);
		base.Controls.Add(this.pictureBoxAndroid);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.LDateExpir);
		base.Controls.Add(this.linkLabel1);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.BtnOk);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmAbout";
		base.Load += new System.EventHandler(FrmAbout_Load);
		((System.ComponentModel.ISupportInitialize)this.pictureBoxAndroid).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxIos).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


