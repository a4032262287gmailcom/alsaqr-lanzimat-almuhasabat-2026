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

public class FrmRPTotalAvergForSore_ByGroupProperties : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private Class_Stores LGN = new Class_Stores();

	private GTSdbEntities db = new GTSdbEntities();

	private IContainer components = null;

	private GroupBox groupBox22;

	private GroupBox groupBox23;

	private GroupBox groupBox24;

	private RadioButton RadioAll;

	private RadioButton RadioC;

	private RadioButton RadioB;

	private RadioButton RadioA;

	private System.Windows.Forms.ComboBox comboStore;

	private Label label16;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	private System.Windows.Forms.ComboBox comboGroup;

	private Label label2;

	private System.Windows.Forms.ComboBox comboItemType;

	private Label label1;

	private System.Windows.Forms.ComboBox comboCompany;

	private Label label3;

	private RadioButton RadioD;

	private System.Windows.Forms.ComboBox CBClass;

	private Label label4;

	public FrmRPTotalAvergForSore_ByGroupProperties()
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
			comboStore.DataSource = LGN.GetAll();
			comboStore.DisplayMember = "Store_Name";
			comboStore.ValueMember = "ID";
			comboItemType.DataSource = db.Item_Type.ToList();
			comboItemType.DisplayMember = "TypeName";
			comboItemType.ValueMember = "SN";
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns.Add("Name", typeof(string));
			foreach (Item_Groups item in db.Item_Groups.ToList())
			{
				dataTable.Rows.Add(item.ID, item.Name);
			}
			DataRow dataRow = dataTable.NewRow();
			dataRow.ItemArray = new object[2] { 0, "الـــــكـــــل" };
			dataTable.Rows.InsertAt(dataRow, 0);
			comboGroup.ValueMember = "ID";
			comboGroup.DisplayMember = "Name";
			comboGroup.DataSource = dataTable;
			comboGroup.SelectedValue = 0;
			DataTable dataTable2 = new DataTable();
			dataTable2.Columns.Add("ID", typeof(int));
			dataTable2.Columns.Add("Name", typeof(string));
			foreach (Item_Company item2 in db.Item_Company.ToList())
			{
				dataTable2.Rows.Add(item2.ID, item2.Name);
			}
			DataRow dataRow2 = dataTable2.NewRow();
			dataRow2.ItemArray = new object[2] { 0, "الـــــكـــــل" };
			dataTable2.Rows.InsertAt(dataRow2, 0);
			comboCompany.ValueMember = "ID";
			comboCompany.DisplayMember = "Name";
			comboCompany.DataSource = dataTable2;
			comboCompany.SelectedValue = -1;
			DataTable dataTable3 = new DataTable();
			dataTable3.Columns.Add("ID", typeof(int));
			dataTable3.Columns.Add("Name", typeof(string));
			foreach (Item_Class item3 in db.Item_Class.ToList())
			{
				dataTable3.Rows.Add(item3.ID, item3.Name);
			}
			DataRow dataRow3 = dataTable3.NewRow();
			dataRow3.ItemArray = new object[2] { -1, "الـــــكـــــل" };
			dataTable3.Rows.InsertAt(dataRow3, 0);
			CBClass.ValueMember = "ID";
			CBClass.DisplayMember = "Name";
			CBClass.DataSource = dataTable3;
			CBClass.SelectedValue = -1;
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
		if (!CheckPermession.CheckUserPage(LoginDetails.GroupID, 42))
		{
			return;
		}
		if (comboStore.Text != "")
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["StoreTable"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Select_ItemQuantity_ByProperties", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@StoreID", Convert.ToInt32(comboStore.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@TypeID", Convert.ToInt32(comboItemType.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@GroupID", Convert.ToInt32(comboGroup.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@CompID", Convert.ToInt32(comboCompany.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@ClassID", Convert.ToInt32(CBClass.SelectedValue));
			if (RadioA.Checked)
			{
				sqlCommand.Parameters.AddWithValue("@ID ", 1);
				sqlDataAdapter.Fill(dataSetALL.StoreTable);
				sqlConnection.Close();
			}
			else if (RadioB.Checked)
			{
				sqlCommand.Parameters.AddWithValue("@ID ", 0);
				sqlDataAdapter.Fill(dataSetALL.StoreTable);
				sqlConnection.Close();
			}
			else if (RadioC.Checked)
			{
				sqlCommand.Parameters.AddWithValue("@ID ", -1);
				sqlDataAdapter.Fill(dataSetALL.StoreTable);
				sqlConnection.Close();
			}
			else if (RadioD.Checked)
			{
				sqlCommand.Parameters.AddWithValue("@ID ", 3);
				sqlDataAdapter.Fill(dataSetALL.StoreTable);
				sqlConnection.Close();
			}
			else if (RadioAll.Checked)
			{
				sqlCommand.Parameters.AddWithValue("@ID ", 2);
				sqlDataAdapter.Fill(dataSetALL.StoreTable);
				sqlConnection.Close();
			}
			if (dataSetALL.Tables["StoreTable"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\Reportgard.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\Reportgard.mrt");
				}
				stiReport.RegData(dataSetALL.StoreTable);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				stiReport["GroupName"] = comboGroup.Text;
				stiReport["TypeName"] = comboItemType.Text;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.Stores.FrmRPTotalAvergForSore_ByGroupProperties));
		this.groupBox22 = new System.Windows.Forms.GroupBox();
		this.groupBox23 = new System.Windows.Forms.GroupBox();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox24 = new System.Windows.Forms.GroupBox();
		this.CBClass = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.RadioD = new System.Windows.Forms.RadioButton();
		this.comboCompany = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.comboGroup = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.comboItemType = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.RadioAll = new System.Windows.Forms.RadioButton();
		this.RadioC = new System.Windows.Forms.RadioButton();
		this.RadioB = new System.Windows.Forms.RadioButton();
		this.RadioA = new System.Windows.Forms.RadioButton();
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
		this.groupBox24.Controls.Add(this.CBClass);
		this.groupBox24.Controls.Add(this.label4);
		this.groupBox24.Controls.Add(this.RadioD);
		this.groupBox24.Controls.Add(this.comboCompany);
		this.groupBox24.Controls.Add(this.label3);
		this.groupBox24.Controls.Add(this.comboGroup);
		this.groupBox24.Controls.Add(this.label2);
		this.groupBox24.Controls.Add(this.comboItemType);
		this.groupBox24.Controls.Add(this.label1);
		this.groupBox24.Controls.Add(this.RadioAll);
		this.groupBox24.Controls.Add(this.RadioC);
		this.groupBox24.Controls.Add(this.RadioB);
		this.groupBox24.Controls.Add(this.RadioA);
		this.groupBox24.Controls.Add(this.comboStore);
		this.groupBox24.Controls.Add(this.label16);
		this.groupBox24.Name = "groupBox24";
		this.groupBox24.TabStop = false;
		resources.ApplyResources(this.CBClass, "CBClass");
		this.CBClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.CBClass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.CBClass.FormattingEnabled = true;
		this.CBClass.Name = "CBClass";
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.RadioD, "RadioD");
		this.RadioD.Name = "RadioD";
		this.RadioD.TabStop = true;
		this.RadioD.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.comboCompany, "comboCompany");
		this.comboCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.comboCompany.FormattingEnabled = true;
		this.comboCompany.Name = "comboCompany";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.comboGroup, "comboGroup");
		this.comboGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.comboGroup.FormattingEnabled = true;
		this.comboGroup.Name = "comboGroup";
		this.comboGroup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombNoDelete_KeyPress);
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.comboItemType, "comboItemType");
		this.comboItemType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboItemType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.comboItemType.FormattingEnabled = true;
		this.comboItemType.Name = "comboItemType";
		this.comboItemType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombNoDelete_KeyPress);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.RadioAll, "RadioAll");
		this.RadioAll.Name = "RadioAll";
		this.RadioAll.TabStop = true;
		this.RadioAll.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.RadioC, "RadioC");
		this.RadioC.Name = "RadioC";
		this.RadioC.TabStop = true;
		this.RadioC.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.RadioB, "RadioB");
		this.RadioB.Name = "RadioB";
		this.RadioB.TabStop = true;
		this.RadioB.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.RadioA, "RadioA");
		this.RadioA.Checked = true;
		this.RadioA.Name = "RadioA";
		this.RadioA.TabStop = true;
		this.RadioA.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.comboStore, "comboStore");
		this.comboStore.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboStore.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
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
		base.Name = "FrmRPTotalAvergForSore_ByGroupProperties";
		this.groupBox22.ResumeLayout(false);
		this.groupBox23.ResumeLayout(false);
		this.groupBox24.ResumeLayout(false);
		this.groupBox24.PerformLayout();
		base.ResumeLayout(false);
	}
}


