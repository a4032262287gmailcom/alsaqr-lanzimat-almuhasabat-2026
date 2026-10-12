using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;

namespace GTSErpSystem.Frms.Security;

public class FrmPassword : XtraForm
{
	private IContainer components = null;

	private SimpleButton BtnExit;

	private SimpleButton BtnEncript;

	private GroupBox groupBox1;

	private TextBox TxtEncript;

	private Label label1;

	private Label label2;

	private TextBox TxtPassword;

	private Label label3;

	private NumericUpDown TxtID;

	private SimpleButton BtnDecript;

	public FrmPassword()
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
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnEncript_Click(object sender, EventArgs e)
	{
		try
		{
			TxtPassword.Text = Class_Encryption.EncryptString(TxtEncript.Text, Convert.ToString(TxtID.Value));
		}
		catch (Exception)
		{
		}
	}

	private void BtnDecript_Click(object sender, EventArgs e)
	{
		try
		{
			TxtEncript.Text = Class_Encryption.DecryptString(TxtPassword.Text, Convert.ToString(TxtID.Value));
		}
		catch (Exception)
		{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Security.FrmPassword));
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEncript = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtID = new System.Windows.Forms.NumericUpDown();
		this.TxtEncript = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtPassword = new System.Windows.Forms.TextBox();
		this.BtnDecript = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtID).BeginInit();
		base.SuspendLayout();
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(348, 175);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(110, 40);
		this.BtnExit.TabIndex = 29;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnEncript.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEncript.ImageOptions.Image");
		this.BtnEncript.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEncript.Location = new System.Drawing.Point(102, 175);
		this.BtnEncript.Margin = new System.Windows.Forms.Padding(4);
		this.BtnEncript.Name = "BtnEncript";
		this.BtnEncript.Size = new System.Drawing.Size(115, 40);
		this.BtnEncript.TabIndex = 28;
		this.BtnEncript.Text = "تشفير";
		this.BtnEncript.Click += new System.EventHandler(BtnEncript_Click);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.TxtID);
		this.groupBox1.Controls.Add(this.TxtEncript);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtPassword);
		this.groupBox1.Location = new System.Drawing.Point(13, 13);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.groupBox1.Size = new System.Drawing.Size(466, 154);
		this.groupBox1.TabIndex = 27;
		this.groupBox1.TabStop = false;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(389, 27);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(65, 17);
		this.label3.TabIndex = 24;
		this.label3.Text = "التسلسل";
		this.TxtID.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtID.Location = new System.Drawing.Point(21, 23);
		this.TxtID.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.TxtID.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.TxtID.Name = "TxtID";
		this.TxtID.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtID.Size = new System.Drawing.Size(356, 28);
		this.TxtID.TabIndex = 23;
		this.TxtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtID.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.TxtEncript.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtEncript.ForeColor = System.Drawing.Color.Red;
		this.TxtEncript.Location = new System.Drawing.Point(21, 63);
		this.TxtEncript.Margin = new System.Windows.Forms.Padding(4);
		this.TxtEncript.Name = "TxtEncript";
		this.TxtEncript.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtEncript.Size = new System.Drawing.Size(356, 28);
		this.TxtEncript.TabIndex = 0;
		this.TxtEncript.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(389, 111);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 17);
		this.label1.TabIndex = 13;
		this.label1.Text = "التشفير";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(384, 70);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(74, 17);
		this.label2.TabIndex = 16;
		this.label2.Text = "كلمة المرور";
		this.TxtPassword.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPassword.ForeColor = System.Drawing.Color.Green;
		this.TxtPassword.Location = new System.Drawing.Point(21, 104);
		this.TxtPassword.Margin = new System.Windows.Forms.Padding(4);
		this.TxtPassword.Name = "TxtPassword";
		this.TxtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPassword.Size = new System.Drawing.Size(356, 28);
		this.TxtPassword.TabIndex = 1;
		this.TxtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.BtnDecript.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDecript.ImageOptions.Image");
		this.BtnDecript.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDecript.Location = new System.Drawing.Point(225, 175);
		this.BtnDecript.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDecript.Name = "BtnDecript";
		this.BtnDecript.Size = new System.Drawing.Size(115, 40);
		this.BtnDecript.TabIndex = 30;
		this.BtnDecript.Text = "فك تشفير";
		this.BtnDecript.Click += new System.EventHandler(BtnDecript_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(492, 228);
		base.Controls.Add(this.BtnDecript);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnEncript);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmPassword";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تشفير كلمة المرور";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtID).EndInit();
		base.ResumeLayout(false);
	}
}


