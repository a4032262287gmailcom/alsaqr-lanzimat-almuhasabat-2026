using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Reports.Public;

public class FrmCloseDayByUser : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private IContainer components = null;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	private GroupBox groupBox1;

	private Label label2;

	private System.Windows.Forms.ComboBox TxtUser;

	public FrmCloseDayByUser()
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
		BindComboBox();
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		if (!CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 117))
		{
			return;
		}
		if (TxtUser.Text != "")
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableCloseDayByUser"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Report_CloseDayByUser", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@UserID", Convert.ToInt32(TxtUser.SelectedValue));
			sqlDataAdapter.Fill(dataSetALL.TableCloseDayByUser);
			sqlConnection.Close();
			if (dataSetALL.Tables["TableCloseDayByUser"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\ReportCloseDayByUser.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportCloseDayByUser.mrt");
				}
				stiReport.RegData(dataSetALL.TableCloseDayByUser);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				stiReport["UserName"] = TxtUser.Text;
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
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void BindComboBox()
	{
		try
		{
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
			DataTable dataTable = new DataTable();
			List<User_Login> list = gTSdbEntities.User_Login.ToList();
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns.Add("Name", typeof(string));
			foreach (User_Login item in list)
			{
				dataTable.Rows.Add(item.ID, item.Name);
			}
			DataRow dataRow = dataTable.NewRow();
			dataRow.ItemArray = new object[2] { 0, "-- الكل --" };
			dataTable.Rows.InsertAt(dataRow, 0);
			TxtUser.ValueMember = "ID";
			TxtUser.DisplayMember = "Name";
			TxtUser.DataSource = dataTable;
			TxtUser.SelectedValue = 0;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.Public.FrmCloseDayByUser));
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtUser = new System.Windows.Forms.ComboBox();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.BtnContinue, "BtnContinue");
		this.BtnContinue.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnContinue.Appearance.Font");
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		this.BtnContinue.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtUser);
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtUser, "TxtUser");
		this.TxtUser.FormattingEnabled = true;
		this.TxtUser.Name = "TxtUser";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnContinue);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmCloseDayByUser";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}


