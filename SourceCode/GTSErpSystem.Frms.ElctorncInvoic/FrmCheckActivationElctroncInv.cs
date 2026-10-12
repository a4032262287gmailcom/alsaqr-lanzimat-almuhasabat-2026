using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GTSErpSystem.Frms.ElctorncInvoic;

public class FrmCheckActivationElctroncInv : XtraForm
{
	public bool Active = false;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private RichTextBox TxtEncryption;

	private Label label2;

	private Label label1;

	private TextBox TxtOtp;

	private SimpleButton BtnActive;

	private SimpleButton BtnPasteEncryption;

	private SimpleButton BtnPasteOtp;

	public FrmCheckActivationElctroncInv()
	{
		InitializeComponent();
		ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("لصق");
		toolStripMenuItem.Click += PasteItem_Click;
		contextMenuStrip.Items.Add(toolStripMenuItem);
		TxtEncryption.ContextMenuStrip = contextMenuStrip;
	}

	private void PasteItem_Click(object sender, EventArgs e)
	{
		if (Clipboard.ContainsText())
		{
			TxtEncryption.Paste();
		}
	}

	private void BtnActive_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(TxtOtp.Text))
		{
			MessageBox.Show("يرجى إدخال الـOTP", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		else if (string.IsNullOrEmpty(TxtEncryption.Text))
		{
			MessageBox.Show("يرجى إدخال الـEncryption", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		else
		{
			Checked();
		}
	}

	public static string Decrypt(string key, string cipherText)
	{
		byte[] iV = new byte[16];
		byte[] buffer = Convert.FromBase64String(cipherText);
		using Aes aes = Aes.Create();
		aes.Key = Encoding.UTF8.GetBytes(key);
		aes.IV = iV;
		ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
		using MemoryStream stream = new MemoryStream(buffer);
		using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
		using StreamReader streamReader = new StreamReader(stream2);
		return streamReader.ReadToEnd();
	}

	private string ConvertArabicToEnglish(string input)
	{
		if (string.IsNullOrWhiteSpace(input))
		{
			return input;
		}
		char[] array = new char[10] { '٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩' };
		char[] array2 = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
		StringBuilder stringBuilder = new StringBuilder(input.Length);
		foreach (char c in input)
		{
			int num = Array.IndexOf(array, c);
			stringBuilder.Append((num >= 0) ? array2[num] : c);
		}
		return stringBuilder.ToString();
	}

	private void Checked()
	{
		string cipherText = TxtEncryption.Text;
		string key = "Mo4545ah5454med1Mo4545ah5454med1";
		string text = "//Mm78789878Mm//";
		try
		{
			string text2 = ConvertArabicToEnglish(LoginDetails.VatNum);
			string text3 = Decrypt(key, cipherText);
			string[] array = text3.Split(new string[1] { text }, StringSplitOptions.None);
			if (array.Length >= 4)
			{
				string text4 = array[0];
				string text5 = array[1];
				string text6 = array[2];
				string text7 = array[3];
				string text8 = DateTime.Today.ToString("yyyy-MM-dd");
				if (text4 == LoginDetails.CompanyNameAR && text5 == text2 && text6 == TxtOtp.Text)
				{
					MessageBox.Show("✔ تم توثيق اذن الدخول للفاتورة الإلكترونية بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					Active = true;
					Close();
				}
				else
				{
					MessageBox.Show("✖ البيانات غير متطابقة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("تنسيق البيانات المشفرة غير صحيح");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("خطأ في فك التشفير: " + ex.Message);
		}
	}

	private void FrmCheckActivationElctroncInv_FormClosing(object sender, FormClosingEventArgs e)
	{
	}

	private void BtnPasteOtp_Click(object sender, EventArgs e)
	{
		string text = Clipboard.GetText();
		TxtOtp.Text = text;
	}

	private void BtnPasteEncryption_Click(object sender, EventArgs e)
	{
		string text = Clipboard.GetText();
		TxtEncryption.Text = text;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.ElctorncInvoic.FrmCheckActivationElctroncInv));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnPasteOtp = new DevExpress.XtraEditors.SimpleButton();
		this.TxtEncryption = new System.Windows.Forms.RichTextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtOtp = new System.Windows.Forms.TextBox();
		this.BtnActive = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPasteEncryption = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.Color.SteelBlue;
		this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.ForeColor = System.Drawing.Color.White;
		this.groupBox1.Location = new System.Drawing.Point(4, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.groupBox1.Size = new System.Drawing.Size(507, 34);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Verify Electronic Invoice Activation";
		this.groupBox2.Controls.Add(this.BtnPasteOtp);
		this.groupBox2.Controls.Add(this.TxtEncryption);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.TxtOtp);
		this.groupBox2.Location = new System.Drawing.Point(4, 52);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(507, 227);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.BtnPasteOtp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPasteOtp.ImageOptions.Image");
		this.BtnPasteOtp.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPasteOtp.Location = new System.Drawing.Point(10, 78);
		this.BtnPasteOtp.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPasteOtp.Name = "BtnPasteOtp";
		this.BtnPasteOtp.Size = new System.Drawing.Size(156, 39);
		this.BtnPasteOtp.TabIndex = 188;
		this.BtnPasteOtp.Text = "لصق Otp";
		this.BtnPasteOtp.Click += new System.EventHandler(BtnPasteOtp_Click);
		this.TxtEncryption.Location = new System.Drawing.Point(9, 122);
		this.TxtEncryption.Name = "TxtEncryption";
		this.TxtEncryption.Size = new System.Drawing.Size(486, 96);
		this.TxtEncryption.TabIndex = 187;
		this.TxtEncryption.Text = "";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(405, 98);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(94, 21);
		this.label2.TabIndex = 186;
		this.label2.Text = "كود التشفير";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(382, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(117, 21);
		this.label1.TabIndex = 185;
		this.label1.Text = "Otp     (6ارقام)";
		this.TxtOtp.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtOtp.ForeColor = System.Drawing.Color.Teal;
		this.TxtOtp.Location = new System.Drawing.Point(9, 39);
		this.TxtOtp.MaxLength = 6;
		this.TxtOtp.Name = "TxtOtp";
		this.TxtOtp.PasswordChar = '#';
		this.TxtOtp.Size = new System.Drawing.Size(486, 32);
		this.TxtOtp.TabIndex = 184;
		this.TxtOtp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.BtnActive.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnActive.ImageOptions.Image");
		this.BtnActive.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnActive.Location = new System.Drawing.Point(16, 282);
		this.BtnActive.Margin = new System.Windows.Forms.Padding(4);
		this.BtnActive.Name = "BtnActive";
		this.BtnActive.Size = new System.Drawing.Size(192, 39);
		this.BtnActive.TabIndex = 2;
		this.BtnActive.Text = "تفعيل الفاتوره الالكترونيه";
		this.BtnActive.Click += new System.EventHandler(BtnActive_Click);
		this.BtnPasteEncryption.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
		this.BtnPasteEncryption.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPasteEncryption.Location = new System.Drawing.Point(346, 282);
		this.BtnPasteEncryption.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPasteEncryption.Name = "BtnPasteEncryption";
		this.BtnPasteEncryption.Size = new System.Drawing.Size(156, 39);
		this.BtnPasteEncryption.TabIndex = 3;
		this.BtnPasteEncryption.Text = "لصق كود التشفير";
		this.BtnPasteEncryption.Click += new System.EventHandler(BtnPasteEncryption_Click);
		base.Appearance.BackColor = System.Drawing.Color.White;
		base.Appearance.Options.UseBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(514, 325);
		base.Controls.Add(this.BtnPasteEncryption);
		base.Controls.Add(this.BtnActive);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmCheckActivationElctroncInv";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "التحقق من تفعيل الفاتوره الاكترونيه";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmCheckActivationElctroncInv_FormClosing);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
