using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Account;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmItemProfit : XtraForm
{
	private IContainer components = null;

	private GroupBox groupBox1;

	private NumericUpDown TxtPrice1;

	private SimpleButton BtnSave;

	private SimpleButton BtnExit;

	private Label label1;

	private Label label4;

	private NumericUpDown TxtCost;

	private Label label3;

	private NumericUpDown TxtPrice2;

	private Label label2;

	private NumericUpDown TxtPrice3;

	public FrmItemProfit()
	{
		InitializeComponent();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave);
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		try
		{
			int num = 0;
			string text = "";
			string nameServer = LoginDetails.nameServer;
			string databaseServer = LoginDetails.databaseServer;
			string userNameServer = LoginDetails.userNameServer;
			string passServer = LoginDetails.passServer;
			SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
			decimal num2 = TxtPrice1.Value / 100m + 1m;
			decimal num3 = TxtPrice2.Value / 100m + 1m;
			decimal num4 = TxtPrice3.Value / 100m + 1m;
			decimal num5 = TxtCost.Value / 100m + 1m;
			text = "UPDATE dbo.Item_Items SET SellPriceSmall = SellPriceSmall * " + num2 + ", SellPriceSmall2 = SellPriceSmall2 * " + num3 + ", SellPriceSmall3 = SellPriceSmall3 * " + num4 + ", LastCost = LastCost * " + num5;
			if (sqlConnection.State == ConnectionState.Closed)
			{
				sqlConnection.Open();
			}
			SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
			sqlCommand.CommandType = CommandType.Text;
			sqlCommand.CommandTimeout = 99999999;
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			LoginDetails.PerVat = TxtPrice1.Value;
			MessageBox.Show("شكرا\u064b لك، \r\n تم تحديث الاسعار علي كل الاصناف بنجاح", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		catch (Exception)
		{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.FrmItemProfit));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.TxtPrice1 = new System.Windows.Forms.NumericUpDown();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtPrice3 = new System.Windows.Forms.NumericUpDown();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtPrice2 = new System.Windows.Forms.NumericUpDown();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtCost = new System.Windows.Forms.NumericUpDown();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtPrice1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPrice3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPrice2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCost).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.TxtCost);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.TxtPrice2);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtPrice3);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.BtnExit);
		this.groupBox1.Controls.Add(this.BtnSave);
		this.groupBox1.Controls.Add(this.TxtPrice1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(435, 225);
		this.groupBox1.TabIndex = 66;
		this.groupBox1.TabStop = false;
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
		this.TxtPrice1.DecimalPlaces = 2;
		this.TxtPrice1.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPrice1.Location = new System.Drawing.Point(21, 27);
		this.TxtPrice1.Minimum = new decimal(new int[4] { 100, 0, 0, -2147483648 });
		this.TxtPrice1.Name = "TxtPrice1";
		this.TxtPrice1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPrice1.Size = new System.Drawing.Size(240, 28);
		this.TxtPrice1.TabIndex = 66;
		this.TxtPrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label1.Location = new System.Drawing.Point(284, 29);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(145, 21);
		this.label1.TabIndex = 71;
		this.label1.Text = "سعر البيع القطاعي";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label2.Location = new System.Drawing.Point(284, 97);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(136, 21);
		this.label2.TabIndex = 73;
		this.label2.Text = "سعر جملة الجملة";
		this.TxtPrice3.DecimalPlaces = 2;
		this.TxtPrice3.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPrice3.Location = new System.Drawing.Point(21, 95);
		this.TxtPrice3.Minimum = new decimal(new int[4] { 100, 0, 0, -2147483648 });
		this.TxtPrice3.Name = "TxtPrice3";
		this.TxtPrice3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPrice3.Size = new System.Drawing.Size(240, 28);
		this.TxtPrice3.TabIndex = 72;
		this.TxtPrice3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label3.Location = new System.Drawing.Point(284, 63);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(130, 21);
		this.label3.TabIndex = 75;
		this.label3.Text = "سعر البيع الجملة";
		this.TxtPrice2.DecimalPlaces = 2;
		this.TxtPrice2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPrice2.Location = new System.Drawing.Point(21, 61);
		this.TxtPrice2.Minimum = new decimal(new int[4] { 100, 0, 0, -2147483648 });
		this.TxtPrice2.Name = "TxtPrice2";
		this.TxtPrice2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPrice2.Size = new System.Drawing.Size(240, 28);
		this.TxtPrice2.TabIndex = 74;
		this.TxtPrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label4.Location = new System.Drawing.Point(284, 131);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(91, 21);
		this.label4.TabIndex = 77;
		this.label4.Text = "سعر الشراء";
		this.TxtCost.DecimalPlaces = 2;
		this.TxtCost.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCost.Location = new System.Drawing.Point(21, 129);
		this.TxtCost.Minimum = new decimal(new int[4] { 100, 0, 0, -2147483648 });
		this.TxtCost.Name = "TxtCost";
		this.TxtCost.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtCost.Size = new System.Drawing.Size(240, 28);
		this.TxtCost.TabIndex = 76;
		this.TxtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(459, 249);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmItemProfit";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "زيادة النسبة المئوية علي الاصناف";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtPrice1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPrice3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPrice2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCost).EndInit();
		base.ResumeLayout(false);
	}
}


