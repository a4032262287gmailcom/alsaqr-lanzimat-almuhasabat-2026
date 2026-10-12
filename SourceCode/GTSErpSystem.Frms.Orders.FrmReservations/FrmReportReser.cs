using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Orders.FrmReservations;

public class FrmReportReser : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private int Type;

	private IContainer components = null;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	private GroupBox groupBox1;

	private TimeEdit TxtTime2;

	private TimeEdit TxtTime1;

	private DateEdit TxtDate2;

	private Label label1;

	private DateEdit TxtDate1;

	private Label label21;

	private RadioButton DateBook;

	private RadioButton DateInstall;

	private RadioButton DateJaw;

	private GroupBox groupBox2;

	public FrmReportReser()
	{
		InitializeComponent();
		TxtDate1.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
		TxtDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");
		TxtTime1.Text = "00:00:00";
		TxtTime2.Text = "23:59:59";
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TxtDate2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnContinue.Focus();
		}
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		if (DateBook.Checked)
		{
			Type = 1;
		}
		else if (DateInstall.Checked)
		{
			Type = 2;
		}
		else if (DateJaw.Checked)
		{
			Type = 3;
		}
		else
		{
			MessageBox.Show("يرجي تحديد نوع البحث", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		if (!CheckPermession.CheckUserPageEventPrint(LoginDetails.GroupID, 158))
		{
			return;
		}
		if (TxtDate1.Text != "")
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["GetReportORD_PUR"].Clear();
			SqlCommand sqlCommand = new SqlCommand("GetOrderReservations", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@Date1", Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text));
			sqlCommand.Parameters.AddWithValue("@Date2", Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text));
			sqlCommand.Parameters.AddWithValue("@MM", Convert.ToInt32(Type));
			sqlCommand.CommandTimeout = 999999;
			sqlDataAdapter.Fill(dataSetALL.GetReportORD_PUR);
			sqlConnection.Close();
			if (dataSetALL.Tables["GetReportORD_PUR"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\ReportReservations.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\ReportReservations.mrt");
				}
				stiReport.RegData(dataSetALL.GetReportORD_PUR);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				stiReport["Date1"] = Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text);
				stiReport["Date2"] = Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text);
				stiReport.Render();
				stiReport.Show(dialogForm: true);
			}
			else
			{
				MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmReservations.FrmReportReser));
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TxtTime2 = new DevExpress.XtraEditors.TimeEdit();
		this.TxtTime1 = new DevExpress.XtraEditors.TimeEdit();
		this.TxtDate2 = new DevExpress.XtraEditors.DateEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtDate1 = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.DateBook = new System.Windows.Forms.RadioButton();
		this.DateInstall = new System.Windows.Forms.RadioButton();
		this.DateJaw = new System.Windows.Forms.RadioButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.BtnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnContinue.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Location = new System.Drawing.Point(95, 197);
		this.BtnContinue.Margin = new System.Windows.Forms.Padding(4);
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Size = new System.Drawing.Size(152, 39);
		this.BtnContinue.TabIndex = 7;
		this.BtnContinue.Text = "استمرار";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(327, 197);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(152, 39);
		this.BtnExit.TabIndex = 8;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.groupBox1.Controls.Add(this.TxtTime2);
		this.groupBox1.Controls.Add(this.TxtTime1);
		this.groupBox1.Controls.Add(this.TxtDate2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtDate1);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Location = new System.Drawing.Point(13, 0);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox1.Size = new System.Drawing.Size(551, 120);
		this.groupBox1.TabIndex = 6;
		this.groupBox1.TabStop = false;
		this.TxtTime2.EditValue = new System.DateTime(2020, 6, 22, 23, 59, 59, 0);
		this.TxtTime2.Location = new System.Drawing.Point(26, 76);
		this.TxtTime2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTime2.Name = "TxtTime2";
		this.TxtTime2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTime2.Properties.Appearance.Options.UseFont = true;
		this.TxtTime2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTime2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTime2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtTime2.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
		this.TxtTime2.Size = new System.Drawing.Size(194, 26);
		this.TxtTime2.TabIndex = 196;
		this.TxtTime1.EditValue = new System.DateTime(2020, 6, 22, 0, 0, 0, 0);
		this.TxtTime1.Location = new System.Drawing.Point(26, 34);
		this.TxtTime1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTime1.Name = "TxtTime1";
		this.TxtTime1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTime1.Properties.Appearance.Options.UseFont = true;
		this.TxtTime1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTime1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTime1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtTime1.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
		this.TxtTime1.Size = new System.Drawing.Size(194, 26);
		this.TxtTime1.TabIndex = 195;
		this.TxtDate2.EditValue = null;
		this.TxtDate2.Location = new System.Drawing.Point(225, 76);
		this.TxtDate2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDate2.Name = "TxtDate2";
		this.TxtDate2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDate2.Properties.Appearance.Options.UseFont = true;
		this.TxtDate2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate2.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate2.Properties.MaskSettings.Set("mask", "yyyy/MM/dd");
		this.TxtDate2.Properties.TodayDate = new System.DateTime(2019, 6, 17, 21, 19, 56, 0);
		this.TxtDate2.Size = new System.Drawing.Size(233, 26);
		this.TxtDate2.TabIndex = 2;
		this.TxtDate2.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtDate2_KeyDown);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label1.Location = new System.Drawing.Point(465, 79);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(69, 19);
		this.label1.TabIndex = 190;
		this.label1.Text = "الي تاريخ";
		this.TxtDate1.EditValue = null;
		this.TxtDate1.Location = new System.Drawing.Point(225, 34);
		this.TxtDate1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDate1.Name = "TxtDate1";
		this.TxtDate1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDate1.Properties.Appearance.Options.UseFont = true;
		this.TxtDate1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate1.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate1.Properties.MaskSettings.Set("mask", "yyyy/MM/dd");
		this.TxtDate1.Size = new System.Drawing.Size(233, 26);
		this.TxtDate1.TabIndex = 1;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label21.Location = new System.Drawing.Point(465, 37);
		this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(64, 19);
		this.label21.TabIndex = 188;
		this.label21.Text = "من تاريخ";
		this.DateBook.AutoSize = true;
		this.DateBook.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DateBook.Location = new System.Drawing.Point(370, 27);
		this.DateBook.Name = "DateBook";
		this.DateBook.Size = new System.Drawing.Size(88, 20);
		this.DateBook.TabIndex = 9;
		this.DateBook.TabStop = true;
		this.DateBook.Text = "تاريخ الحجز";
		this.DateBook.UseVisualStyleBackColor = true;
		this.DateInstall.AutoSize = true;
		this.DateInstall.Location = new System.Drawing.Point(189, 27);
		this.DateInstall.Name = "DateInstall";
		this.DateInstall.Size = new System.Drawing.Size(98, 20);
		this.DateInstall.TabIndex = 10;
		this.DateInstall.TabStop = true;
		this.DateInstall.Text = "تاريخ التركيب";
		this.DateInstall.UseVisualStyleBackColor = true;
		this.DateJaw.AutoSize = true;
		this.DateJaw.Location = new System.Drawing.Point(26, 27);
		this.DateJaw.Name = "DateJaw";
		this.DateJaw.Size = new System.Drawing.Size(82, 20);
		this.DateJaw.TabIndex = 11;
		this.DateJaw.TabStop = true;
		this.DateJaw.Text = "تاريخ الفك";
		this.DateJaw.UseVisualStyleBackColor = true;
		this.groupBox2.Controls.Add(this.DateBook);
		this.groupBox2.Controls.Add(this.DateJaw);
		this.groupBox2.Controls.Add(this.DateInstall);
		this.groupBox2.Location = new System.Drawing.Point(13, 124);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(551, 66);
		this.groupBox2.TabIndex = 12;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "البحث بواسطه ";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(569, 241);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.BtnContinue);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmReportReser";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تقرير الحجوزات";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}


