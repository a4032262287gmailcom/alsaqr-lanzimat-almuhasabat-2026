using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmPassExcel : XtraForm
{
	public bool ISOK = false;

	private IContainer components = null;

	private SimpleButton BtnContinue;

	private Label label1;

	private TextBox TxtPassWord;

	public FrmPassExcel()
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
		if (TxtPassWord.Text == "EXCEL_GTS")
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
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtPassWord = new System.Windows.Forms.TextBox();
		base.SuspendLayout();
		this.BtnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnContinue.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Location = new System.Drawing.Point(12, 83);
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Size = new System.Drawing.Size(256, 35);
		this.BtnContinue.TabIndex = 183;
		this.BtnContinue.Text = "استمرار";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		this.label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Teal;
		this.label1.Location = new System.Drawing.Point(12, 9);
		this.label1.Name = "label1";
		this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label1.Size = new System.Drawing.Size(256, 46);
		this.label1.TabIndex = 184;
		this.label1.Text = "عزيزى موظف GTS يرجى إدخال كلمة المرور الخاصة بكم لإكمال عملية دخول الأصناف";
		this.TxtPassWord.ForeColor = System.Drawing.Color.Teal;
		this.TxtPassWord.Location = new System.Drawing.Point(12, 57);
		this.TxtPassWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtPassWord.Name = "TxtPassWord";
		this.TxtPassWord.PasswordChar = '#';
		this.TxtPassWord.Size = new System.Drawing.Size(257, 21);
		this.TxtPassWord.TabIndex = 182;
		this.TxtPassWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtPassWord_KeyDown);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(281, 130);
		base.Controls.Add(this.BtnContinue);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.TxtPassWord);
		base.Name = "FrmPassExcel";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Excel";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


