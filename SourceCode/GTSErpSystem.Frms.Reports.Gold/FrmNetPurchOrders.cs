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
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Reports.Gold;

public class FrmNetPurchOrders : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_Orders ORD = new Class_Orders();

	private Class_Customer Cus = new Class_Customer();

	private GTSdbEntities db = new GTSdbEntities();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private Class_Stores stores = new Class_Stores();

	private DataTable dtCus;

	private DataTable dtStore;

	private IContainer components = null;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	private GroupBox groupBox1;

	private Label label20;

	private System.Windows.Forms.ComboBox TxtType;

	private DateEdit TxtDate2;

	private Label label1;

	private DateEdit TxtDate1;

	private Label label21;

	private TimeEdit TxtTime2;

	private TimeEdit TxtTime1;

	private Label label2;

	private System.Windows.Forms.ComboBox TxtCustomer;

	private Label LblAccountID;

	private Label label3;

	private System.Windows.Forms.ComboBox TxtStore;

	private Label label8;

	private System.Windows.Forms.ComboBox TxtUser;

	private Label label6;

	private System.Windows.Forms.ComboBox TxtCompany;

	private Label label5;

	private System.Windows.Forms.ComboBox TxtGroup;

	private Label label4;

	private System.Windows.Forms.ComboBox TxtClass;

	public FrmNetPurchOrders()
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
		TxtDate1.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
		TxtDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");
		TxtTime1.Text = "00:00:00";
		TxtTime2.Text = "23:59:59";
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		if (!CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 54))
		{
			return;
		}
		if (TxtDate1.Text != "")
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["GetReportGoldenORD_PURDetails"].Clear();
			SqlCommand sqlCommand = new SqlCommand("GetReport_OrdersPurchNet_ByProperties", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@Date1", Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text));
			sqlCommand.Parameters.AddWithValue("@Date2", Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text));
			sqlCommand.Parameters.AddWithValue("@CashType", Convert.ToInt32(TxtType.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@CustID", Convert.ToInt32(TxtCustomer.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@EmpID", Convert.ToInt32(TxtUser.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@GroupID", Convert.ToInt32(TxtGroup.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@CompanyID", Convert.ToInt32(TxtCompany.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@BranchID", Convert.ToInt32(LoginDetails.BranchID));
			sqlCommand.Parameters.AddWithValue("@ClassID", Convert.ToInt32(TxtClass.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@StoreId", Convert.ToInt32(TxtStore.SelectedValue));
			sqlDataAdapter.Fill(dataSetALL.GetReportGoldenORD_PURDetails);
			sqlConnection.Close();
			if (dataSetALL.Tables["GetReportGoldenORD_PURDetails"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\ReportGoldOrdersPurchNet.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportGoldOrdersPurchNet.mrt");
				}
				stiReport.RegData(dataSetALL.GetReportGoldenORD_PURDetails);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				stiReport["Date1"] = Convert.ToDateTime(TxtDate1.Text);
				stiReport["Date2"] = Convert.ToDateTime(TxtDate2.Text);
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
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns.Add("Name", typeof(string));
			foreach (Account_AccountType item in ORD.GetAccountType().ToList())
			{
				dataTable.Rows.Add(item.ID, item.Name);
			}
			DataRow dataRow = dataTable.NewRow();
			dataRow.ItemArray = new object[2] { 0, "الـــــكـــــل" };
			dataTable.Rows.InsertAt(dataRow, 0);
			TxtType.ValueMember = "ID";
			TxtType.DisplayMember = "Name";
			TxtType.DataSource = dataTable;
			TxtType.SelectedValue = 0;
			dtCus = new DataTable();
			dtCus.Columns.Add("ID", typeof(int));
			dtCus.Columns.Add("CustSuppName", typeof(string));
			dtCus.Columns.Add("AccountID", typeof(string));
			foreach (Account_CustSup item2 in (from x in Cus.GetAll()
				where x.IsCustomers == true
				select x).ToList())
			{
				dtCus.Rows.Add(item2.ID, item2.CustSuppName, item2.AccountNo);
			}
			DataRow dataRow2 = dtCus.NewRow();
			dataRow2.ItemArray = new object[2] { 0, "الـــــكـــــل" };
			dtCus.Rows.InsertAt(dataRow2, 0);
			TxtCustomer.ValueMember = "ID";
			TxtCustomer.DisplayMember = "CustSuppName";
			TxtCustomer.DataSource = dtCus;
			TxtCustomer.SelectedValue = 0;
			dtStore = new DataTable();
			dtStore.Columns.Add("ID", typeof(int));
			dtStore.Columns.Add("Store_Name", typeof(string));
			foreach (Account_Stores item3 in stores.GetAll().ToList())
			{
				dtStore.Rows.Add(item3.ID, item3.Store_Name);
			}
			DataRow dataRow3 = dtStore.NewRow();
			dataRow3.ItemArray = new object[2] { 0, "الـــــكـــــل" };
			dtStore.Rows.InsertAt(dataRow3, 0);
			TxtStore.ValueMember = "ID";
			TxtStore.DisplayMember = "Store_Name";
			TxtStore.DataSource = dtStore;
			TxtStore.SelectedValue = 0;
			DataTable dataTable2 = new DataTable();
			dataTable2.Columns.Add("ID", typeof(int));
			dataTable2.Columns.Add("Name", typeof(string));
			foreach (Item_Class item4 in db.Item_Class.ToList())
			{
				dataTable2.Rows.Add(item4.ID, item4.Name);
			}
			DataRow dataRow4 = dataTable2.NewRow();
			dataRow4.ItemArray = new object[2] { -1, "الـــــكـــــل" };
			dataTable2.Rows.InsertAt(dataRow4, 0);
			TxtClass.ValueMember = "ID";
			TxtClass.DisplayMember = "Name";
			TxtClass.DataSource = dataTable2;
			TxtClass.SelectedValue = -1;
			DataTable dataTable3 = new DataTable();
			dataTable3.Columns.Add("ID", typeof(int));
			dataTable3.Columns.Add("Name", typeof(string));
			foreach (Item_Groups item5 in db.Item_Groups.ToList())
			{
				dataTable3.Rows.Add(item5.ID, item5.Name);
			}
			DataRow dataRow5 = dataTable3.NewRow();
			dataRow5.ItemArray = new object[2] { 0, "الـــــكـــــل" };
			dataTable3.Rows.InsertAt(dataRow5, 0);
			TxtGroup.ValueMember = "ID";
			TxtGroup.DisplayMember = "Name";
			TxtGroup.DataSource = dataTable3;
			TxtGroup.SelectedValue = 0;
			DataTable dataTable4 = new DataTable();
			dataTable4.Columns.Add("ID", typeof(int));
			dataTable4.Columns.Add("Name", typeof(string));
			foreach (Item_Company item6 in db.Item_Company.ToList())
			{
				dataTable4.Rows.Add(item6.ID, item6.Name);
			}
			DataRow dataRow6 = dataTable4.NewRow();
			dataRow6.ItemArray = new object[2] { -1, "الـــــكـــــل" };
			dataTable4.Rows.InsertAt(dataRow6, 0);
			TxtCompany.ValueMember = "ID";
			TxtCompany.DisplayMember = "Name";
			TxtCompany.DataSource = dataTable4;
			TxtCompany.SelectedValue = -1;
			DataTable dataTable5 = new DataTable();
			dataTable5.Columns.Add("ID", typeof(int));
			dataTable5.Columns.Add("Name", typeof(string));
			foreach (User_Login item7 in db.User_Login.ToList())
			{
				dataTable5.Rows.Add(item7.ID, item7.Name);
			}
			DataRow dataRow7 = dataTable5.NewRow();
			dataRow7.ItemArray = new object[2] { 0, "الـــــكـــــل" };
			dataTable5.Rows.InsertAt(dataRow7, 0);
			TxtUser.ValueMember = "ID";
			TxtUser.DisplayMember = "Name";
			TxtUser.DataSource = dataTable5;
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

	private void TxtDate2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnContinue.Focus();
		}
	}

	private void TxtCustomer_SelectedIndexChanged(object sender, EventArgs e)
	{
		int x = Convert.ToInt32(TxtCustomer.SelectedValue);
		DataRow dataRow = dtCus.AsEnumerable().SingleOrDefault((DataRow r) => r.Field<int>("ID") == x);
		LblAccountID.Text = dataRow[2].ToString();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.Gold.FrmNetPurchOrders));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtUser = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.TxtCompany = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtGroup = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtClass = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtStore = new System.Windows.Forms.ComboBox();
		this.LblAccountID = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtCustomer = new System.Windows.Forms.ComboBox();
		this.TxtTime2 = new DevExpress.XtraEditors.TimeEdit();
		this.TxtTime1 = new DevExpress.XtraEditors.TimeEdit();
		this.label20 = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.TxtDate2 = new DevExpress.XtraEditors.DateEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtDate1 = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.TxtUser);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.TxtCompany);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.TxtGroup);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.TxtClass);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.TxtStore);
		this.groupBox1.Controls.Add(this.LblAccountID);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtCustomer);
		this.groupBox1.Controls.Add(this.TxtTime2);
		this.groupBox1.Controls.Add(this.TxtTime1);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.TxtType);
		this.groupBox1.Controls.Add(this.TxtDate2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtDate1);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.TxtUser, "TxtUser");
		this.TxtUser.FormattingEnabled = true;
		this.TxtUser.Name = "TxtUser";
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.TxtCompany, "TxtCompany");
		this.TxtCompany.FormattingEnabled = true;
		this.TxtCompany.Name = "TxtCompany";
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.TxtGroup, "TxtGroup");
		this.TxtGroup.FormattingEnabled = true;
		this.TxtGroup.Name = "TxtGroup";
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.TxtClass, "TxtClass");
		this.TxtClass.FormattingEnabled = true;
		this.TxtClass.Name = "TxtClass";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.TxtStore, "TxtStore");
		this.TxtStore.FormattingEnabled = true;
		this.TxtStore.Name = "TxtStore";
		resources.ApplyResources(this.LblAccountID, "LblAccountID");
		this.LblAccountID.Name = "LblAccountID";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtCustomer, "TxtCustomer");
		this.TxtCustomer.FormattingEnabled = true;
		this.TxtCustomer.Name = "TxtCustomer";
		this.TxtCustomer.SelectedIndexChanged += new System.EventHandler(TxtCustomer_SelectedIndexChanged);
		resources.ApplyResources(this.TxtTime2, "TxtTime2");
		this.TxtTime2.Name = "TxtTime2";
		this.TxtTime2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtTime2.Properties.Appearance.Font");
		this.TxtTime2.Properties.Appearance.Options.UseFont = true;
		this.TxtTime2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTime2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTime2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtTime2.Properties.Buttons"))
		});
		this.TxtTime2.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
		resources.ApplyResources(this.TxtTime1, "TxtTime1");
		this.TxtTime1.Name = "TxtTime1";
		this.TxtTime1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtTime1.Properties.Appearance.Font");
		this.TxtTime1.Properties.Appearance.Options.UseFont = true;
		this.TxtTime1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTime1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTime1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtTime1.Properties.Buttons"))
		});
		this.TxtTime1.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
		resources.ApplyResources(this.label20, "label20");
		this.label20.Name = "label20";
		resources.ApplyResources(this.TxtType, "TxtType");
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Name = "TxtType";
		this.TxtType.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtDate2, "TxtDate2");
		this.TxtDate2.Name = "TxtDate2";
		this.TxtDate2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDate2.Properties.Appearance.Font");
		this.TxtDate2.Properties.Appearance.Options.UseFont = true;
		this.TxtDate2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate2.Properties.Buttons"))
		});
		this.TxtDate2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate2.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate2.Properties.Mask.EditMask = resources.GetString("TxtDate2.Properties.Mask.EditMask");
		this.TxtDate2.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate2.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate2.Properties.TodayDate = new System.DateTime(2019, 6, 17, 21, 19, 56, 0);
		this.TxtDate2.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtDate2_KeyDown);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.TxtDate1, "TxtDate1");
		this.TxtDate1.Name = "TxtDate1";
		this.TxtDate1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDate1.Properties.Appearance.Font");
		this.TxtDate1.Properties.Appearance.Options.UseFont = true;
		this.TxtDate1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate1.Properties.Buttons"))
		});
		this.TxtDate1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate1.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate1.Properties.Mask.EditMask = resources.GetString("TxtDate1.Properties.Mask.EditMask");
		this.TxtDate1.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate1.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
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
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnContinue);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmNetPurchOrders";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


