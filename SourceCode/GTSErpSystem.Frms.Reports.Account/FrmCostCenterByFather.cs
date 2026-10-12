using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Reports.Account;

public class FrmCostCenterByFather : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private ClassProject project = new ClassProject();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private IContainer components = null;

	private System.Windows.Forms.ComboBox CBProject;

	private TimeEdit TxtTime2;

	private TimeEdit TxtTime1;

	private Label LblToAccount_No;

	private Label LblFromAccount_No;

	private Label label2;

	private DateEdit TxtDate2;

	private Label label1;

	private Label label21;

	private DateEdit TxtDate1;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	public FrmCostCenterByFather()
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
		TxtDate1.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
		TxtDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");
		TxtTime1.Text = "00:00:00";
		TxtTime2.Text = "23:59:59";
		BindProject();
		TxtDate1.Focus();
	}

	public void BindProject()
	{
		try
		{
			CBProject.DataSource = Cost.GetSearchAccountCostCenter();
			CBProject.DisplayMember = "CostCentersName";
			CBProject.ValueMember = "CostCentersID";
			if (CBProject.Items.Count <= 0)
			{
				return;
			}
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns.Add("Name", typeof(string));
			foreach (Select_SearchAccountCostCenterByProject_Result item in Cost.GetCostCentersByProject(Convert.ToInt32(CBProject.SelectedValue)).ToList())
			{
				dataTable.Rows.Add(item.CostCentersID, item.CostCentersName);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void TxtDate_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnContinue.Focus();
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 62))
		{
			if (Convert.ToDateTime(TxtDate1.Text) <= Convert.ToDateTime(TxtDate2.Text))
			{
				SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["GetReport_Project"].Clear();
				SqlCommand sqlCommand = new SqlCommand("GetReport_CostCentersByFather", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@CostCentersID", CBProject.SelectedValue);
				sqlCommand.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text));
				sqlCommand.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text));
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlCommand.Parameters.AddWithValue("@pflag", 1);
				sqlDataAdapter.Fill(dataSetALL.GetReport_Project);
				sqlConnection.Close();
				if (dataSetALL.Tables["GetReport_Project"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					if (LoginDetails.Lang == "ar-EG")
					{
						stiReport.Load(Application.StartupPath + "\\Reports\\ReportCostCenterByFather.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportCostCenterByFather.mrt");
					}
					stiReport.RegData(dataSetALL.GetReport_Project);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
					stiReport["Address"] = LoginDetails.Address;
					stiReport["Phone"] = LoginDetails.Phone;
					stiReport["Bank"] = LoginDetails.CompanyBank;
					stiReport["ProjectName"] = CBProject.Text;
					stiReport["Date1"] = TxtDate1.DateTime;
					stiReport["Date2"] = TxtDate2.DateTime;
					stiReport.Render();
					stiReport.Show(dialogForm: true);
					FrmReportGetOrder frmReportGetOrder = new FrmReportGetOrder();
					frmReportGetOrder.stiViewerControl1.Report = stiReport;
				}
				else
				{
					MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("عفوا، لايمكن ان يكون التاريخ الاول اقل من التاريخ الثاني", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				TxtDate1.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void TxtNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void ControlSave_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnContinue.Focus();
		}
	}

	private void FrmRPProject_FormClosing(object sender, FormClosingEventArgs e)
	{
	}

	private void CBProject_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (CBProject.Items.Count > 0)
			{
				DataTable dataTable = new DataTable();
				DataRow dataRow = dataTable.NewRow();
				dataRow.ItemArray = new object[2] { 0, "الكل" };
				dataTable.Rows.InsertAt(dataRow, 0);
			}
		}
		catch
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.Account.FrmCostCenterByFather));
		this.CBProject = new System.Windows.Forms.ComboBox();
		this.TxtTime2 = new DevExpress.XtraEditors.TimeEdit();
		this.TxtTime1 = new DevExpress.XtraEditors.TimeEdit();
		this.LblToAccount_No = new System.Windows.Forms.Label();
		this.LblFromAccount_No = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtDate2 = new DevExpress.XtraEditors.DateEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.TxtDate1 = new DevExpress.XtraEditors.DateEdit();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).BeginInit();
		base.SuspendLayout();
		this.CBProject.Font = new System.Drawing.Font("Tahoma", 10f);
		this.CBProject.FormattingEnabled = true;
		this.CBProject.Location = new System.Drawing.Point(110, 106);
		this.CBProject.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBProject.Name = "CBProject";
		this.CBProject.Size = new System.Drawing.Size(448, 29);
		this.CBProject.TabIndex = 204;
		this.CBProject.SelectedIndexChanged += new System.EventHandler(CBProject_SelectedIndexChanged);
		this.TxtTime2.EditValue = new System.DateTime(2020, 6, 22, 23, 59, 59, 0);
		this.TxtTime2.Location = new System.Drawing.Point(337, 69);
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
		this.TxtTime2.Size = new System.Drawing.Size(222, 26);
		this.TxtTime2.TabIndex = 203;
		this.TxtTime1.EditValue = new System.DateTime(2020, 6, 22, 0, 0, 0, 0);
		this.TxtTime1.Location = new System.Drawing.Point(337, 32);
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
		this.TxtTime1.Size = new System.Drawing.Size(222, 26);
		this.TxtTime1.TabIndex = 202;
		this.LblToAccount_No.AutoSize = true;
		this.LblToAccount_No.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblToAccount_No.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblToAccount_No.Location = new System.Drawing.Point(198, 9);
		this.LblToAccount_No.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblToAccount_No.Name = "LblToAccount_No";
		this.LblToAccount_No.Size = new System.Drawing.Size(18, 19);
		this.LblToAccount_No.TabIndex = 201;
		this.LblToAccount_No.Text = "1";
		this.LblToAccount_No.Visible = false;
		this.LblFromAccount_No.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblFromAccount_No.AutoSize = true;
		this.LblFromAccount_No.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblFromAccount_No.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblFromAccount_No.Location = new System.Drawing.Point(432, 9);
		this.LblFromAccount_No.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblFromAccount_No.Name = "LblFromAccount_No";
		this.LblFromAccount_No.Size = new System.Drawing.Size(18, 19);
		this.LblFromAccount_No.TabIndex = 200;
		this.LblFromAccount_No.Text = "1";
		this.LblFromAccount_No.Visible = false;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(21, 110);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(82, 19);
		this.label2.TabIndex = 196;
		this.label2.Text = "المركز الاب";
		this.TxtDate2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDate2.EditValue = null;
		this.TxtDate2.Location = new System.Drawing.Point(110, 69);
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
		this.TxtDate2.Properties.Mask.EditMask = "yyyy/MM/dd";
		this.TxtDate2.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate2.Size = new System.Drawing.Size(220, 26);
		this.TxtDate2.TabIndex = 1;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label1.Location = new System.Drawing.Point(34, 75);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(69, 19);
		this.label1.TabIndex = 190;
		this.label1.Text = "الي تاريخ";
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label21.Location = new System.Drawing.Point(37, 38);
		this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(64, 19);
		this.label21.TabIndex = 111;
		this.label21.Text = "من تاريخ";
		this.TxtDate1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDate1.EditValue = null;
		this.TxtDate1.Location = new System.Drawing.Point(110, 32);
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
		this.TxtDate1.Properties.Mask.EditMask = "yyyy/MM/dd";
		this.TxtDate1.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate1.Size = new System.Drawing.Size(220, 26);
		this.TxtDate1.TabIndex = 0;
		this.BtnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnContinue.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Location = new System.Drawing.Point(110, 152);
		this.BtnContinue.Margin = new System.Windows.Forms.Padding(4);
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Size = new System.Drawing.Size(220, 42);
		this.BtnContinue.TabIndex = 13;
		this.BtnContinue.Text = "استمرار";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(337, 152);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(222, 42);
		this.BtnExit.TabIndex = 14;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(603, 212);
		base.Controls.Add(this.CBProject);
		base.Controls.Add(this.BtnContinue);
		base.Controls.Add(this.TxtTime2);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.TxtTime1);
		base.Controls.Add(this.LblToAccount_No);
		base.Controls.Add(this.TxtDate1);
		base.Controls.Add(this.label21);
		base.Controls.Add(this.LblFromAccount_No);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.TxtDate2);
		base.IconOptions.Image = (System.Drawing.Image)resources.GetObject("FrmCostCenterByFather.IconOptions.Image");
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "FrmCostCenterByFather";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تقرير مراكز التكلفه";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmRPProject_FormClosing);
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


