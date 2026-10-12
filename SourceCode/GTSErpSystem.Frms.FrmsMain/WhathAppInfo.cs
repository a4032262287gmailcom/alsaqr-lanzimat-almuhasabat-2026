using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GTSErpSystem.Frms.FrmsMain;

public class WhathAppInfo : XtraForm
{
	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label3;

	private Label label2;

	private Label label1;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label label8;

	private Label label9;

	private LinkLabel linkLabel1;

	public WhathAppInfo()
	{
		InitializeComponent();
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		string fileName = "https://youtu.be/HY-jasYfVhE?si=k0dn6hFOfYbv5r4U";
		try
		{
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			};
			Process.Start(startInfo);
		}
		catch (Exception ex)
		{
			MessageBox.Show("تعذر فتح الرابط: " + ex.Message);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.WhathAppInfo));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label9 = new System.Windows.Forms.Label();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.linkLabel1);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(6, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1069, 471);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "طريقه تفعيل الارسال علي الواتساب";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(758, 405);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(303, 18);
		this.label9.TabIndex = 8;
		this.label9.Text = "فديو توضيحي لطريقه ربط الرقم ط¨ Twilio :";
		this.linkLabel1.AutoSize = true;
		this.linkLabel1.Location = new System.Drawing.Point(389, 405);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(346, 18);
		this.linkLabel1.TabIndex = 7;
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Text = "https://youtu.be/HY-jasYfVhE?si=vc7i6olIcYIp46pB";
		this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(430, 438);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(204, 21);
		this.label8.TabIndex = 2;
		this.label8.Text = "نتمني لكم تجربه سعيده";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(759, 321);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(302, 36);
		this.label7.TabIndex = 6;
		this.label7.Text = "تفعيل ارسال الواتس اب من خلال البرنامج :\r\n\r\n";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(91, 357);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(941, 21);
		this.label5.TabIndex = 4;
		this.label5.Text = "يوجد في الشاشات CheckBox تسمي  الارسال علي الواتس اب برجاء تفعيلها وسيتم ارسال الفاتوره تلقائي بعد الحفظ علي الواتس اب\r\n";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(659, 279);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(352, 42);
		this.label6.TabIndex = 5;
		this.label6.Text = "هذه المعلومات عباره عن AuthToken,AccountSid\r\n\r\n";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(71, 237);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(961, 42);
		this.label4.TabIndex = 3;
		this.label4.Text = "بعد ما يتم التسجيل في Twilio سنقوم بالدخول الي المنصه الخاصه بكم لاخذ بعض المعلومات ليتم تشغيل ارسال الرسايل علي الوتس اب\r\n\r\n";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(619, 200);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(442, 18);
		this.label3.TabIndex = 2;
		this.label3.Text = "تفعيل رقمك كرقم واتساب بزنس على  Twilio (Self-Sign Up) :";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(10, 61);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(1035, 126);
		this.label2.TabIndex = 1;
		this.label2.Text = resources.GetString("label2.Text");
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(905, 43);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(156, 18);
		this.label1.TabIndex = 0;
		this.label1.Text = "المتتطلبات قبل البدء :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1087, 495);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "WhathAppInfo";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "واتساب";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}


