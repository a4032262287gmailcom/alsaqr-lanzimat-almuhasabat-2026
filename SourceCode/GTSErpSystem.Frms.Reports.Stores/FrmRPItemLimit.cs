using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Reports.Stores;

public class FrmRPItemLimit : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private Class_Stores LGN = new Class_Stores();

	private IContainer components = null;

	private GroupBox groupBox22;

	private GroupBox groupBox23;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	private GroupBox groupBox24;

	private System.Windows.Forms.ComboBox comboStore;

	private Label label16;

	public FrmRPItemLimit()
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
		BindComboStore();
	}

	public void BindComboStore()
	{
		try
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns.Add("Store_Name", typeof(string));
			foreach (Account_Stores item in LGN.GetAll().ToList())
			{
				dataTable.Rows.Add(item.ID, item.Store_Name);
			}
			DataRow dataRow = dataTable.NewRow();
			dataRow.ItemArray = new object[2] { 0, "الـــــكـــــل" };
			dataTable.Rows.InsertAt(dataRow, 0);
			comboStore.ValueMember = "ID";
			comboStore.DisplayMember = "Store_Name";
			comboStore.DataSource = dataTable;
			comboStore.SelectedValue = 0;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void CombNoDelete_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		if (!CheckPermession.CheckUserPage(LoginDetails.GroupID, 74))
		{
			return;
		}
		if (comboStore.Text != "")
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableItemLimit"].Clear();
			SqlCommand sqlCommand = new SqlCommand("GetReportItemLimit", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@StoreID", Convert.ToInt32(comboStore.SelectedValue));
			sqlDataAdapter.Fill(dataSetALL.TableItemLimit);
			sqlConnection.Close();
			if (dataSetALL.Tables["TableItemLimit"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\ReportItemLimit.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportItemLimit.mrt");
				}
				stiReport.RegData(dataSetALL.TableItemLimit);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				stiReport["StoreName"] = comboStore.Text;
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

	private void groupBox22_Enter(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.Stores.FrmRPItemLimit));
		this.groupBox22 = new System.Windows.Forms.GroupBox();
		this.groupBox23 = new System.Windows.Forms.GroupBox();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox24 = new System.Windows.Forms.GroupBox();
		this.comboStore = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.groupBox22.SuspendLayout();
		this.groupBox23.SuspendLayout();
		this.groupBox24.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox22, "groupBox22");
		this.groupBox22.Controls.Add(this.groupBox23);
		this.groupBox22.Controls.Add(this.groupBox24);
		this.groupBox22.Name = "groupBox22";
		this.groupBox22.TabStop = false;
		this.groupBox22.Enter += new System.EventHandler(groupBox22_Enter);
		resources.ApplyResources(this.groupBox23, "groupBox23");
		this.groupBox23.Controls.Add(this.BtnContinue);
		this.groupBox23.Controls.Add(this.BtnExit);
		this.groupBox23.Name = "groupBox23";
		this.groupBox23.TabStop = false;
		resources.ApplyResources(this.BtnContinue, "BtnContinue");
		this.BtnContinue.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnContinue.Appearance.Font");
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.groupBox24, "groupBox24");
		this.groupBox24.Controls.Add(this.comboStore);
		this.groupBox24.Controls.Add(this.label16);
		this.groupBox24.Name = "groupBox24";
		this.groupBox24.TabStop = false;
		this.comboStore.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboStore.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		resources.ApplyResources(this.comboStore, "comboStore");
		this.comboStore.FormattingEnabled = true;
		this.comboStore.Name = "comboStore";
		this.comboStore.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombNoDelete_KeyPress);
		resources.ApplyResources(this.label16, "label16");
		this.label16.Name = "label16";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox22);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmRPItemLimit";
		this.groupBox22.ResumeLayout(false);
		this.groupBox23.ResumeLayout(false);
		this.groupBox24.ResumeLayout(false);
		this.groupBox24.PerformLayout();
		base.ResumeLayout(false);
	}
}


