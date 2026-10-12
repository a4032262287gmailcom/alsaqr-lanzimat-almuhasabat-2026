using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmPassWordOrderReturn : XtraForm
{
	public bool ISOK = false;

	private IContainer components = null;

	private TextBox TxtPassWord;

	private Label label1;

	private SimpleButton BtnContinue;

	public FrmPassWordOrderReturn()
	{
		InitializeComponent();
		TxtPassWord.Focus();
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		RunFuctn();
	}

	private void TxtPassWord_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			RunFuctn();
		}
	}

	public void RunFuctn()
	{
		if (TxtPassWord.Text == "6789")
		{
			MessageBox.Show("شكرا\u064b لك، تم توثيق كلمة المرور بنجاح", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			ISOK = true;
			Close();
		}
		else
		{
			MessageBox.Show("عفوا\u064bطŒ تم ادخال كلمة المرور غير صحيحة", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			ISOK = false;
			TxtPassWord.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.FrmPassWordOrderReturn));
		this.TxtPassWord = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		base.SuspendLayout();
		this.TxtPassWord.ForeColor = System.Drawing.Color.Teal;
		this.TxtPassWord.Location = new System.Drawing.Point(13, 68);
		this.TxtPassWord.Name = "TxtPassWord";
		this.TxtPassWord.PasswordChar = '#';
		this.TxtPassWord.Size = new System.Drawing.Size(299, 23);
		this.TxtPassWord.TabIndex = 0;
		this.TxtPassWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtPassWord_KeyDown);
		this.label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Teal;
		this.label1.Location = new System.Drawing.Point(13, 9);
		this.label1.Name = "label1";
		this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label1.Size = new System.Drawing.Size(299, 56);
		this.label1.TabIndex = 181;
		this.label1.Text = "للتحقق من التوثيق فضلا ادخل كلمة المرور ثم اضغط استمرار ";
		this.BtnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnContinue.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Location = new System.Drawing.Point(13, 98);
		this.BtnContinue.Margin = new System.Windows.Forms.Padding(4);
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Size = new System.Drawing.Size(299, 43);
		this.BtnContinue.TabIndex = 1;
		this.BtnContinue.Text = "استمرار";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(325, 154);
		base.Controls.Add(this.BtnContinue);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.TxtPassWord);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmPassWord";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "توثيق كلمة المرور";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


