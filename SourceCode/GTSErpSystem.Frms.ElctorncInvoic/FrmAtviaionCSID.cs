using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;

namespace GTSErpSystem.Frms.ElctorncInvoic;

public class FrmAtviaionCSID : XtraForm
{
	private Class_Setting Userx = new Class_Setting();

	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox TxtCSID;

	private SimpleButton BtnSave;

	private SimpleButton BtnPost;

	private SimpleButton BtnNew;

	private SimpleButton BtnClose;

	public string Mode { get; set; } = "Production";

	public string CSR { get; set; }

	public string PrivateKey { get; set; }

	public string PublicKey { get; set; }

	public string Secrt { get; set; }

	public bool IsUser { get; set; }

	public FrmAtviaionCSID()
	{
		InitializeComponent();
	}

	private void simpleButton2_Click(object sender, EventArgs e)
	{
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		TxtCSID.Text = string.Empty;
	}

	private void BtnPost_Click(object sender, EventArgs e)
	{
		TxtCSID.Text = string.Empty;
		string text = Clipboard.GetText();
		TxtCSID.Text = text;
	}

	private void BtnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
	}

	private void Decryption()
	{
		if (TxtCSID.Text == string.Empty)
		{
			MessageBox.Show("من فضلك ادخل ال CSID", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void Save()
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.ElctorncInvoic.FrmAtviaionCSID));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnPost = new DevExpress.XtraEditors.SimpleButton();
		this.TxtCSID = new System.Windows.Forms.TextBox();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.BtnPost);
		this.groupBox1.Controls.Add(this.TxtCSID);
		this.groupBox1.Controls.Add(this.BtnNew);
		this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1033, 512);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "فك تشفير ال CSID";
		this.BtnPost.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPost.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPost.Appearance.Options.UseFont = true;
		this.BtnPost.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPost.ImageOptions.Image");
		this.BtnPost.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPost.Location = new System.Drawing.Point(6, 460);
		this.BtnPost.Margin = new System.Windows.Forms.Padding(5);
		this.BtnPost.Name = "BtnPost";
		this.BtnPost.Size = new System.Drawing.Size(244, 39);
		this.BtnPost.TabIndex = 238;
		this.BtnPost.Text = "لصق";
		this.BtnPost.Click += new System.EventHandler(BtnPost_Click);
		this.TxtCSID.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCSID.Location = new System.Drawing.Point(6, 25);
		this.TxtCSID.Multiline = true;
		this.TxtCSID.Name = "TxtCSID";
		this.TxtCSID.Size = new System.Drawing.Size(1021, 427);
		this.TxtCSID.TabIndex = 231;
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(781, 465);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(5);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(244, 39);
		this.BtnNew.TabIndex = 237;
		this.BtnNew.Text = "جديد";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(12, 544);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(5);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(252, 39);
		this.BtnSave.TabIndex = 235;
		this.BtnSave.Text = "حفظ في قاعده البيانات";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnClose.Appearance.Options.UseFont = true;
		this.BtnClose.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnClose.ImageOptions.Image");
		this.BtnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnClose.Location = new System.Drawing.Point(795, 544);
		this.BtnClose.Margin = new System.Windows.Forms.Padding(5);
		this.BtnClose.Name = "BtnClose";
		this.BtnClose.Size = new System.Drawing.Size(244, 39);
		this.BtnClose.TabIndex = 238;
		this.BtnClose.Text = "خروج";
		this.BtnClose.Click += new System.EventHandler(BtnClose_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1054, 597);
		base.Controls.Add(this.BtnClose);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmAtviaionCSID";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "AtviaionCSID";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}


