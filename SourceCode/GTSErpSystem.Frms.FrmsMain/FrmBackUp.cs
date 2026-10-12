using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmBackUp : XtraForm
{
	private Class_Setting STTG = new Class_Setting();

	private IContainer components = null;

	private Label label1;

	private Label LblServer;

	private Label label3;

	private Label label4;

	private Label LblPath;

	private Label LblDataBase;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	public FrmBackUp()
	{
		InitializeComponent();
		LblServer.Text = LoginDetails.nameServer;
		LblDataBase.Text = LoginDetails.databaseServer;
		LblPath.Text = LoginDetails.BackupPath;
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		if (LoginDetails.IsBackupWhenClose)
		{
			int num = STTG.TakeBackup();
			if (num > 0)
			{
				MessageBox.Show("شكرا\u064b لك، \r\n تم أخذ نسخة احتياطية بنجاح ", "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
			else
			{
				MessageBox.Show("نعتذر، تم اكتشاف خطأ \r\n يجب التأكد من مسار مجلد النسخ الاحتياطي ثم حاول مرة أخري", "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.FrmBackUp));
		this.label1 = new System.Windows.Forms.Label();
		this.LblServer = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.LblPath = new System.Windows.Forms.Label();
		this.LblDataBase = new System.Windows.Forms.Label();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(22, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(84, 17);
		this.label1.TabIndex = 0;
		this.label1.Text = "اسم السيرفر";
		this.LblServer.AutoSize = true;
		this.LblServer.Location = new System.Drawing.Point(153, 21);
		this.LblServer.Name = "LblServer";
		this.LblServer.Size = new System.Drawing.Size(88, 17);
		this.LblServer.TabIndex = 1;
		this.LblServer.Text = "....................";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(22, 79);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(77, 17);
		this.label3.TabIndex = 2;
		this.label3.Text = "مسار الحفظ";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(22, 50);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(116, 17);
		this.label4.TabIndex = 3;
		this.label4.Text = "اسم قاعدة البيانات";
		this.LblPath.AutoSize = true;
		this.LblPath.Location = new System.Drawing.Point(153, 79);
		this.LblPath.Name = "LblPath";
		this.LblPath.Size = new System.Drawing.Size(88, 17);
		this.LblPath.TabIndex = 4;
		this.LblPath.Text = "....................";
		this.LblDataBase.AutoSize = true;
		this.LblDataBase.Location = new System.Drawing.Point(153, 50);
		this.LblDataBase.Name = "LblDataBase";
		this.LblDataBase.Size = new System.Drawing.Size(88, 17);
		this.LblDataBase.TabIndex = 5;
		this.LblDataBase.Text = "....................";
		this.BtnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnContinue.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Location = new System.Drawing.Point(55, 126);
		this.BtnContinue.Margin = new System.Windows.Forms.Padding(4);
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Size = new System.Drawing.Size(186, 42);
		this.BtnContinue.TabIndex = 12;
		this.BtnContinue.Text = "استمرار";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(288, 126);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(187, 42);
		this.BtnExit.TabIndex = 13;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(533, 181);
		base.Controls.Add(this.BtnContinue);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.LblDataBase);
		base.Controls.Add(this.LblPath);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.LblServer);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmBackUp";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "النسخ الاحتياطي لقواعد البيانات";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


