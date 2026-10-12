using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Account;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmItemVat : XtraForm
{
	private IContainer components = null;

	private Label label1;

	private GroupBox groupBox1;

	private NumericUpDown TxtVat;

	private SimpleButton BtnSave;

	private SimpleButton BtnExit;

	private CheckBox CBISVat;

	private Label label2;

	public FrmItemVat()
	{
		InitializeComponent();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave);
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		FrmPassWordVat frmPassWordVat = new FrmPassWordVat();
		frmPassWordVat.ShowDialog();
		if (frmPassWordVat.ISOK == Convert.ToBoolean("True"))
		{
			int num = 0;
			string text = "";
			string nameServer = LoginDetails.nameServer;
			string databaseServer = LoginDetails.databaseServer;
			string userNameServer = LoginDetails.userNameServer;
			string passServer = LoginDetails.passServer;
			SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
			num = (CBISVat.Checked ? 1 : 0);
			text = "UPDATE dbo.Item_Items SET Tax_Value = " + TxtVat.Value + ",Is_Tax = " + num + "UPDATE dbo.TblSetting SET PerVat = " + TxtVat.Value;
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
			sqlCommand.CommandType = CommandType.Text;
			sqlCommand.CommandTimeout = 900000;
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			LoginDetails.PerVat = TxtVat.Value;
			MessageBox.Show("شكرا\u064b لك، \r\n تم تحديث ضريبة القيمة المضافة علي كل الاصناف بنجاح", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		else
		{
			MessageBox.Show(" فشل العملية، \r\n يرجي المحاولة مرة أخري او مراجعة الدعم الفني الخاص بالبرنامج", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.FrmItemVat));
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.CBISVat = new System.Windows.Forms.CheckBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVat = new System.Windows.Forms.NumericUpDown();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtVat).BeginInit();
		base.SuspendLayout();
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(6, 19);
		this.label1.Name = "label1";
		this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label1.Size = new System.Drawing.Size(423, 64);
		this.label1.TabIndex = 65;
		this.label1.Text = "هذه الخاصية من حقوق ملكية شركة G.T.S فقط وليس من حق العميل امتلاكها.";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.CBISVat);
		this.groupBox1.Controls.Add(this.BtnExit);
		this.groupBox1.Controls.Add(this.BtnSave);
		this.groupBox1.Controls.Add(this.TxtVat);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(435, 225);
		this.groupBox1.TabIndex = 66;
		this.groupBox1.TabStop = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label2.Location = new System.Drawing.Point(6, 136);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(27, 21);
		this.label2.TabIndex = 70;
		this.label2.Text = "%";
		this.CBISVat.AutoSize = true;
		this.CBISVat.Checked = true;
		this.CBISVat.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CBISVat.Font = new System.Drawing.Font("Tahoma", 10f);
		this.CBISVat.Location = new System.Drawing.Point(66, 96);
		this.CBISVat.Name = "CBISVat";
		this.CBISVat.Size = new System.Drawing.Size(347, 25);
		this.CBISVat.TabIndex = 69;
		this.CBISVat.Text = "تفعيل ضريبة القيمة المضافة علي كل الاصناف";
		this.CBISVat.UseVisualStyleBackColor = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(21, 178);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(192, 40);
		this.BtnExit.TabIndex = 68;
		this.BtnExit.Text = "خروج Exit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(221, 178);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(192, 40);
		this.BtnSave.TabIndex = 67;
		this.BtnSave.Text = "تنفيذ  Install";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.TxtVat.DecimalPlaces = 3;
		this.TxtVat.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtVat.Location = new System.Drawing.Point(39, 134);
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVat.Size = new System.Drawing.Size(374, 28);
		this.TxtVat.TabIndex = 66;
		this.TxtVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4] { 15, 0, 0, 0 });
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(459, 249);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmItemVat";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "ضريبة القيمة المضافة علي الاصناف";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtVat).EndInit();
		base.ResumeLayout(false);
	}
}


