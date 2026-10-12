using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Contract;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items.FrmSearch;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Reports.Stores;

public class FrmStoreGardCat : XtraForm
{
	private Class_Items ITM = new Class_Items();

	private GTSdbEntities GTSdbEntities = new GTSdbEntities();

	private Class_AccountCard AccountCard = new Class_AccountCard();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private DataTable datatable = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	public string txtWordAr;

	public string txtWordEn;

	public string CodeItm;

	private int DecimalNum = LoginDetails.DecimalNum;

	private Class_Stores LGN = new Class_Stores();

	private GTSdbEntities db = new GTSdbEntities();

	private Class_Contract Contract = new Class_Contract();

	private IContainer components = null;

	private ToolStripMenuItem RecoveredMenuItemFix1;

	private SimpleButton BtnSave;

	private ToolStripMenuItem RecoveredMenuItemFix2;

	private ToolStripMenuItem RecoveredMenuItemFix3;

	private ToolStripMenuItem RecoveredMenuItemFix4;

	private ToolStripMenuItem RecoveredMenuItemFix5;

	private ToolStripMenuItem RecoveredMenuItemFix6;

	private ToolStripMenuItem RecoveredMenuItemFix7;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem RecoveredMenuItemFix8;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem MenuExit;

	private GroupBox groupBox3;

	private SimpleButton BtnSearch;

	private SimpleButton BtnPrint;

	private SimpleButton BtnExit;

	private DateEdit TxtDate;

	private Label label24;

	private TextEdit TxtNumNote;

	private Label label21;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private GroupBox groupBox1;

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem MenuSearchItem;

	private ToolStripMenuItem RecoveredMenuItemFix9;

	private ToolStripMenuItem MenuNewItem;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripMenuItem MenuPreviewFatoura;

	private ToolStripMenuItem MenuPrintQued;

	private ToolStripMenuItem RecoveredMenuItemFix10;

	private ToolStripMenuItem RecoveredMenuItemFix11;

	private ToolStripMenuItem MenuDelete;

	private ToolStripMenuItem MenuSave;

	private ToolStripMenuItem MenuNew;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem MenuEdit;

	private RibbonPage ribbonPage2;

	private GroupBox groupBox2;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn gridColumn19;

	private GridColumn gridColumn4;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn6;

	private GridColumn gridColumn8;

	private GridColumn gridColumn9;

	private CardView cardView1;

	private GridColumn gridColumn10;

	private GridColumn gridColumn11;

	private GridColumn gridColumn12;

	private GridColumn gridColumn13;

	private GridColumn gridColumn14;

	private GridColumn gridColumn15;

	private GridColumn gridColumn16;

	private GridColumn gridColumn17;

	private GridColumn gridColumn18;

	private GridColumn gridColumn7;

	private GridColumn gridColumn23;

	private GridColumn gridColumn25;

	private GridColumn gridColumn5;

	private System.Windows.Forms.ComboBox comboCompany;

	private Label label17;

	private System.Windows.Forms.ComboBox comboGroup;

	private Label label18;

	private System.Windows.Forms.ComboBox comboItemType;

	private System.Windows.Forms.ComboBox comboStore;

	private Label label20;

	private Label label1;

	private RadioButton RadioD;

	private RadioButton RadioAll;

	private RadioButton RadioA;

	private RadioButton RadioC;

	private RadioButton RadioB;

	private ButtonEdit TxtCode;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private RepositoryItemButtonEdit SellPriceWithVat;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private SimpleButton BtnNew;

	private System.Windows.Forms.ComboBox comboClass;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private ButtonEdit TxtBarcode;

	public FrmStoreGardCat()
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
		SetInitialRow();
		BindComboStore();
		TxtDate.DateTime = DateTime.Now;
		if (LoginDetails.FrmOrderClose)
		{
			BtnNew.Visible = false;
			BtnSave.Visible = false;
			BtnExit.Visible = false;
			BtnPrint.Visible = false;
		}
	}

	public void BindComboStore()
	{
		try
		{
			comboStore.DataSource = Contract.Get_Stores();
			comboStore.DisplayMember = "Store_Name";
			comboStore.ValueMember = "ID";
			comboStore.SelectedValue = LoginDetails.StoreID;
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
			dataRow2.ItemArray = new object[2] { -1, "الـــــكـــــل" };
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
			comboClass.ValueMember = "ID";
			comboClass.DisplayMember = "Name";
			comboClass.DataSource = dataTable3;
			comboClass.SelectedValue = -1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("ItemID", typeof(int));
		datatable.Columns.Add("ItemUnitID", typeof(int));
		datatable.Columns.Add("كود_الصنف", typeof(string));
		datatable.Columns.Add("أسم الصنف", typeof(string));
		datatable.Columns.Add("الوحدة", typeof(string));
		datatable.Columns.Add("كمية البرنامج", typeof(decimal));
		datatable.Columns.Add("المخزن", typeof(string));
		datatable.Columns.Add("سعر البيع", typeof(decimal));
		datatable.Columns.Add("فرق الكميات", typeof(decimal));
		datatable.Columns.Add("المتوسط", typeof(decimal));
		datatable.Columns.Add("سعرالتكلفة", typeof(decimal));
		datatable.Columns.Add("كمية الجرد", typeof(decimal));
		datatable.Columns.Add("الإعتماد", typeof(int));
		datatable.Columns.Add("ظ…", typeof(int));
		dtCurrentTable.Columns.Add("SN", typeof(int));
		dtCurrentTable.Columns.Add("GardID", typeof(int));
		dtCurrentTable.Columns.Add("ItemID", typeof(int));
		dtCurrentTable.Columns.Add("StoreID", typeof(int));
		dtCurrentTable.Columns.Add("ItemUnitID", typeof(int));
		dtCurrentTable.Columns.Add("SellPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("LastPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("AvergPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("QuantityApp", typeof(decimal));
		dtCurrentTable.Columns.Add("QuantityGard", typeof(decimal));
		dtCurrentTable.Columns.Add("QuantitiesDifference", typeof(decimal));
		dtCurrentTable.Columns.Add("ItemUnitType", typeof(string));
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmStoreGardCatSearch frmStoreGardCatSearch = new FrmStoreGardCatSearch();
			frmStoreGardCatSearch.ShowDialog();
			if (frmStoreGardCatSearch.CustCode <= 0)
			{
				return;
			}
			int custCode = frmStoreGardCatSearch.CustCode;
			datatable.Rows.Clear();
			List<Select_OrderGard_Result> list = db.Select_OrderGard(custCode).ToList();
			int num = 1;
			foreach (Select_OrderGard_Result item in list)
			{
				DataRow dataRow = null;
				dataRow = datatable.NewRow();
				dataRow["ItemID"] = item.ItemID;
				dataRow["ItemUnitID"] = item.ItemUnitID;
				dataRow["كود_الصنف"] = item.Item_code;
				dataRow["أسم الصنف"] = item.item_Name;
				dataRow["الوحدة"] = item.ItemUnitID;
				dataRow["المخزن"] = item.StoreName;
				dataRow["كمية البرنامج"] = item.QuantityApp;
				dataRow["سعر البيع"] = item.SellPrice;
				dataRow["فرق الكميات"] = item.QuantitiesDifference;
				dataRow["المتوسط"] = item.AvergPrice;
				dataRow["سعرالتكلفة"] = item.LastPrice;
				dataRow["كمية الجرد"] = item.QuantityGard;
				datatable.Rows.Add(dataRow);
				num++;
			}
			gridControl1.DataSource = datatable;
			TxtCode.Text = frmStoreGardCatSearch.CustCode.ToString();
			TxtDate.DateTime = Convert.ToDateTime(frmStoreGardCatSearch.DateGard);
			TxtNumNote.Text = frmStoreGardCatSearch.NoteNum;
			BtnSave.Enabled = false;
		}
		catch (Exception)
		{
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.Tables["StoreTable"].Clear();
		SqlCommand sqlCommand = new SqlCommand("Select_ItemQuantity_ByProperties", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlCommand.Parameters.AddWithValue("@StoreID", Convert.ToInt32(comboStore.SelectedValue));
		sqlCommand.Parameters.AddWithValue("@TypeID", Convert.ToInt32(comboItemType.SelectedValue));
		sqlCommand.Parameters.AddWithValue("@GroupID", Convert.ToInt32(comboGroup.SelectedValue));
		sqlCommand.Parameters.AddWithValue("@CompID", Convert.ToInt32(comboCompany.SelectedValue));
		sqlCommand.Parameters.AddWithValue("@ClassID", Convert.ToInt32(comboClass.SelectedValue));
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
		datatable.Rows.Clear();
		DataSetALL.StoreTableDataTable storeTable = dataSetALL.StoreTable;
		if (storeTable.Count() <= 0)
		{
			return;
		}
		int num = 1;
		foreach (DataSetALL.StoreTableRow item in storeTable)
		{
			DataRow dataRow = null;
			dataRow = datatable.NewRow();
			dataRow["ظ…"] = num;
			dataRow["ItemID"] = item.ItemId;
			dataRow["ItemUnitID"] = item.ItemUnitID;
			dataRow["كود_الصنف"] = item.Item_Code;
			dataRow["أسم الصنف"] = item.Item_Name;
			dataRow["سعر البيع"] = Math.Round(Convert.ToDecimal(item.SellPriceSmall), LoginDetails.DecimalNum);
			dataRow["الوحدة"] = item.UnitsName;
			dataRow["المخزن"] = item.StoreName;
			dataRow["المتوسط"] = Math.Round(Convert.ToDecimal(item.Average_cost), LoginDetails.DecimalNum);
			dataRow["كمية البرنامج"] = Math.Round(Convert.ToDecimal(item.Quantity), LoginDetails.DecimalNum);
			dataRow["كمية الجرد"] = Math.Round(Convert.ToDecimal(item.Quantity), LoginDetails.DecimalNum);
			dataRow["فرق الكميات"] = 0;
			dataRow["سعرالتكلفة"] = Math.Round(Convert.ToDecimal(item.LastCost), LoginDetails.DecimalNum);
			num++;
			datatable.Rows.Add(dataRow);
		}
		gridControl1.DataSource = datatable;
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		Save();
	}

	public void Save()
	{
		if (datatable.Rows.Count > 0)
		{
			FilterDataTableSave(datatable);
			int num = AccountCard.SaveGard(LoginDetails.BranchID, TxtDate.DateTime, TxtNumNote.Text, LoginDetails.UserID, LoginDetails.UserID, LoginDetails.macAddress, dtCurrentTable);
			if (num > 0)
			{
				MessageBox.Show("تم حفظ بيانات التسوية الجردية بنجاح  \r\n بتاريخ" + DateTime.Now.ToString(), "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
			else
			{
				MessageBox.Show("لم يتم حفظ التسوية الجردية ", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، الرجاء عمل بحث واضافة اصناف ثم حاول مرة اخري ", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	public void FilterDataTableSave(DataTable ItemGard)
	{
		int num = 1;
		dtCurrentTable.Clear();
		foreach (DataRow row in ItemGard.Rows)
		{
			if (row["كود_الصنف"].ToString() != "")
			{
				dtCurrentTable.Rows.Add(num, 0, Convert.ToInt32(row["ItemID"]), Convert.ToInt32(comboStore.SelectedValue), Convert.ToInt32(row["ItemUnitID"]), Convert.ToDecimal(row["سعر البيع"]), Convert.ToDecimal(row["سعرالتكلفة"]), Convert.ToDecimal(row["سعرالتكلفة"]), Convert.ToDecimal(row["كمية البرنامج"]), Convert.ToDecimal(row["كمية الجرد"]), Convert.ToDecimal(row["فرق الكميات"]), "Pice");
				num++;
			}
		}
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		if (!CheckPermession.CheckUserPage(LoginDetails.GroupID, 42))
		{
			return;
		}
		if (comboStore.Text != "")
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["StoreTable"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Select_ItemQuantity_ByProperties", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@StoreID", Convert.ToInt32(comboStore.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@TypeID", Convert.ToInt32(comboItemType.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@GroupID", Convert.ToInt32(comboGroup.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@CompID", Convert.ToInt32(comboCompany.SelectedValue));
			sqlCommand.Parameters.AddWithValue("@ClassID", Convert.ToInt32(comboClass.SelectedValue));
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

	private void BtnNew_Click(object sender, EventArgs e)
	{
		TxtCode.Text = "";
		TxtDate.DateTime = DateTime.Now;
		TxtNumNote.Text = "";
		datatable.Rows.Clear();
		gridControl1.DataSource = datatable;
		BtnSave.Enabled = true;
		BindComboStore();
	}

	private void comboGroup_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	public void SearchItems()
	{
		try
		{
			LoginDetails.ItemSearch = CodeItm;
			LoginDetails.ISCheckSearchItem = false;
			LoginDetails.ISPurches = true;
			FrmSearchItems1 frmSearchItems = new FrmSearchItems1();
			frmSearchItems.ShowDialog();
			if (LoginDetails.ItemSearchAll2.Count > 0)
			{
				int num = 1;
				foreach (string item in LoginDetails.ItemSearchAll2)
				{
					string code = item.ToString();
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(code).FirstOrDefault();
					if (get_Item_ByCodeALL_Result != null)
					{
						DataRow dataRow = datatable.NewRow();
						dataRow["ظ…"] = num;
						dataRow["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
						dataRow["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
						dataRow["كود_الصنف"] = get_Item_ByCodeALL_Result.Item_code;
						dataRow["أسم الصنف"] = get_Item_ByCodeALL_Result.item_Name;
						dataRow["سعر البيع"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), LoginDetails.DecimalNum);
						dataRow["الوحدة"] = ITM.GetUnitNameById(Convert.ToInt32(get_Item_ByCodeALL_Result.UnitSmallID));
						dataRow["المخزن"] = get_Item_ByCodeALL_Result.StoreName;
						dataRow["المتوسط"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.Average_cost), LoginDetails.DecimalNum);
						decimal itemTotalQuantity = AccountCard.GetItemTotalQuantity(get_Item_ByCodeALL_Result.ItemId, Convert.ToInt32(comboStore.SelectedValue));
						dataRow["كمية البرنامج"] = Math.Round(itemTotalQuantity, LoginDetails.DecimalNum);
						dataRow["كمية الجرد"] = Math.Round(itemTotalQuantity, LoginDetails.DecimalNum);
						dataRow["فرق الكميات"] = 0;
						dataRow["سعرالتكلفة"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost), LoginDetails.DecimalNum);
						datatable.Rows.Add(dataRow);
						num++;
					}
				}
				frmSearchItems.Close();
				frmSearchItems.Hide();
				LoginDetails.ItemSearchAll2.Clear();
				LoginDetails.ISPurches = false;
			}
			frmSearchItems.Close();
			frmSearchItems.Hide();
			LoginDetails.ItemSearchAll2.Clear();
			LoginDetails.ISCheckSearchItem = true;
			LoginDetails.ISPurches = false;
			gridControl1.DataSource = datatable;
		}
		catch (Exception)
		{
		}
	}

	private void TxtBarcode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchItems();
	}

	private void TxtBarcode_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode != Keys.Return)
			{
				return;
			}
			string text = TxtBarcode.Text.Trim();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			Get_Item_ByCodeALL_Result itemDetails = ITM.GetItemByCode(text).FirstOrDefault();
			if (itemDetails != null)
			{
				decimal itemTotalQuantity = AccountCard.GetItemTotalQuantity(itemDetails.ItemId, Convert.ToInt32(comboStore.SelectedValue));
				DataRow dataRow = datatable.AsEnumerable().FirstOrDefault((DataRow r) => r.Field<int>("ItemID") == itemDetails.ItemId);
				if (dataRow != null)
				{
					dataRow["كمية البرنامج"] = Math.Round(itemTotalQuantity, LoginDetails.DecimalNum);
					dataRow["كمية الجرد"] = Math.Round(itemTotalQuantity, LoginDetails.DecimalNum);
					dataRow["فرق الكميات"] = 0;
				}
				else
				{
					DataRow dataRow2 = datatable.NewRow();
					dataRow2["ظ…"] = datatable.Rows.Count + 1;
					dataRow2["ItemID"] = itemDetails.ItemId;
					dataRow2["ItemUnitID"] = itemDetails.UnitSmallID;
					dataRow2["كود_الصنف"] = itemDetails.Item_code;
					dataRow2["أسم الصنف"] = itemDetails.item_Name;
					dataRow2["سعر البيع"] = Math.Round(Convert.ToDecimal(itemDetails.SellPriceSmall), LoginDetails.DecimalNum);
					dataRow2["الوحدة"] = ITM.GetUnitNameById(Convert.ToInt32(itemDetails.UnitSmallID));
					dataRow2["المخزن"] = itemDetails.StoreName;
					dataRow2["المتوسط"] = Math.Round(Convert.ToDecimal(itemDetails.Average_cost), LoginDetails.DecimalNum);
					dataRow2["كمية البرنامج"] = Math.Round(itemTotalQuantity, LoginDetails.DecimalNum);
					dataRow2["كمية الجرد"] = Math.Round(itemTotalQuantity, LoginDetails.DecimalNum);
					dataRow2["فرق الكميات"] = 0;
					dataRow2["سعرالتكلفة"] = Math.Round(Convert.ToDecimal(itemDetails.LastCost), LoginDetails.DecimalNum);
					datatable.Rows.Add(dataRow2);
				}
				gridControl1.DataSource = datatable;
			}
			else
			{
				SoundPlayer soundPlayer = new SoundPlayer("SOUND38.WAV");
				soundPlayer.Play();
				MessageBox.Show("عفوا، لايوجد صنف بهذا الرقم \r\n الرجاء المراجعة والمحاولة مرة اخري.", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
			TxtBarcode.Text = "";
			TxtBarcode.Focus();
		}
		catch (Exception)
		{
			TxtBarcode.Text = "";
		}
	}

	private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
	{
		if (!(e.Column.FieldName == "كمية الجرد"))
		{
			return;
		}
		decimal num = Convert.ToDecimal(e.Value);
		string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كمية الجرد");
		string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("ظ…");
		if (!(focusedRowCellDisplayText != ""))
		{
			return;
		}
		foreach (DataRow row in datatable.Rows)
		{
			if (row["ظ…"].ToString() == focusedRowCellDisplayText2)
			{
				row["فرق الكميات"] = Convert.ToDecimal(row["كمية الجرد"]) - Convert.ToDecimal(row["كمية البرنامج"]);
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.Stores.FrmStoreGardCat));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions5 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions6 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions7 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions8 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled8 = new DevExpress.Utils.SerializableAppearanceObject();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.SellPriceWithVat = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.RecoveredMenuItemFix1 = new System.Windows.Forms.ToolStripMenuItem();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.RecoveredMenuItemFix2 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix3 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix4 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix5 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix6 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix7 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.RecoveredMenuItemFix8 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
		this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label24 = new System.Windows.Forms.Label();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.comboClass = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.RadioD = new System.Windows.Forms.RadioButton();
		this.RadioAll = new System.Windows.Forms.RadioButton();
		this.RadioA = new System.Windows.Forms.RadioButton();
		this.RadioC = new System.Windows.Forms.RadioButton();
		this.RadioB = new System.Windows.Forms.RadioButton();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.comboCompany = new System.Windows.Forms.ComboBox();
		this.label17 = new System.Windows.Forms.Label();
		this.comboGroup = new System.Windows.Forms.ComboBox();
		this.label18 = new System.Windows.Forms.Label();
		this.comboItemType = new System.Windows.Forms.ComboBox();
		this.comboStore = new System.Windows.Forms.ComboBox();
		this.label20 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix9 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuNewItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPreviewFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintQued = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix10 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix11 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.TxtBarcode = new DevExpress.XtraEditors.ButtonEdit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.SellPriceWithVat).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.repositoryItemButtonEditCodeSearch, "repositoryItemButtonEditCodeSearch");
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions65");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons"), resources.GetString("repositoryItemButtonEditCodeSearch.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEditCodeSearch.Buttons6"), resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons9"))
		});
		this.repositoryItemButtonEditCodeSearch.Name = "repositoryItemButtonEditCodeSearch";
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions66");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"), resources.GetString("repositoryItemButtonEdit5.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit5.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEdit5.Buttons6"), resources.GetObject("repositoryItemButtonEdit5.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit5.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit5.Buttons9"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions67");
		this.SellPriceWithVat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("SellPriceWithVat.Buttons"), resources.GetString("SellPriceWithVat.Buttons1"), (int)resources.GetObject("SellPriceWithVat.Buttons2"), (bool)resources.GetObject("SellPriceWithVat.Buttons3"), (bool)resources.GetObject("SellPriceWithVat.Buttons4"), (bool)resources.GetObject("SellPriceWithVat.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("SellPriceWithVat.Buttons6"), resources.GetObject("SellPriceWithVat.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("SellPriceWithVat.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("SellPriceWithVat.Buttons9"))
		});
		this.SellPriceWithVat.Name = "SellPriceWithVat";
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit6, "repositoryItemButtonEdit6");
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions68");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		resources.ApplyResources(this.RecoveredMenuItemFix1, "اسمالموردToolStripMenuItem1");
		this.RecoveredMenuItemFix1.Name = "اسمالموردToolStripMenuItem1";
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnEdit_Click);
		resources.ApplyResources(this.RecoveredMenuItemFix2, "رقمالفاتورةToolStripMenuItem1");
		this.RecoveredMenuItemFix2.Name = "رقمالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix3, "نوعالفاتورةToolStripMenuItem1");
		this.RecoveredMenuItemFix3.Name = "نوعالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix4, "الكمياتToolStripMenuItem1");
		this.RecoveredMenuItemFix4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix5, this.RecoveredMenuItemFix6, this.RecoveredMenuItemFix7 });
		this.RecoveredMenuItemFix4.Name = "الكمياتToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix5, "الكميةالحاليةToolStripMenuItem");
		this.RecoveredMenuItemFix5.Name = "الكميةالحاليةToolStripMenuItem";
		resources.ApplyResources(this.RecoveredMenuItemFix6, "كلالكمياتبالمستودعاتToolStripMenuItem");
		this.RecoveredMenuItemFix6.Name = "كلالكمياتبالمستودعاتToolStripMenuItem";
		resources.ApplyResources(this.RecoveredMenuItemFix7, "كلالكمياتبالفروعToolStripMenuItem");
		this.RecoveredMenuItemFix7.Name = "كلالكمياتبالفروعToolStripMenuItem";
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.RecoveredMenuItemFix8, "تخصيصToolStripMenuItem1");
		this.RecoveredMenuItemFix8.Name = "تخصيصToolStripMenuItem1";
		resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		resources.ApplyResources(this.MenuExit, "MenuExit");
		this.MenuExit.Name = "MenuExit";
		resources.ApplyResources(this.groupBox3, "groupBox3");
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.TabStop = false;
		resources.ApplyResources(this.gridControl1, "gridControl1");
		this.gridControl1.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControl1.EmbeddedNavigator.AccessibleDescription");
		this.gridControl1.EmbeddedNavigator.AccessibleName = resources.GetString("gridControl1.EmbeddedNavigator.AccessibleName");
		this.gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip = (DevExpress.Utils.DefaultBoolean)resources.GetObject("gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip");
		this.gridControl1.EmbeddedNavigator.Anchor = (System.Windows.Forms.AnchorStyles)resources.GetObject("gridControl1.EmbeddedNavigator.Anchor");
		this.gridControl1.EmbeddedNavigator.BackgroundImage = (System.Drawing.Image)resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImage");
		this.gridControl1.EmbeddedNavigator.BackgroundImageLayout = (System.Windows.Forms.ImageLayout)resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImageLayout");
		this.gridControl1.EmbeddedNavigator.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("gridControl1.EmbeddedNavigator.ImeMode");
		this.gridControl1.EmbeddedNavigator.Margin = (System.Windows.Forms.Padding)resources.GetObject("gridControl1.EmbeddedNavigator.Margin");
		this.gridControl1.EmbeddedNavigator.MaximumSize = (System.Drawing.Size)resources.GetObject("gridControl1.EmbeddedNavigator.MaximumSize");
		this.gridControl1.EmbeddedNavigator.TextLocation = (DevExpress.XtraEditors.NavigatorButtonsTextLocation)resources.GetObject("gridControl1.EmbeddedNavigator.TextLocation");
		this.gridControl1.EmbeddedNavigator.ToolTip = resources.GetString("gridControl1.EmbeddedNavigator.ToolTip");
		this.gridControl1.EmbeddedNavigator.ToolTipIconType = (DevExpress.Utils.ToolTipIconType)resources.GetObject("gridControl1.EmbeddedNavigator.ToolTipIconType");
		this.gridControl1.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControl1.EmbeddedNavigator.ToolTipTitle");
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView1.Appearance.HeaderPanel.Font");
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		resources.ApplyResources(this.gridView1, "gridView1");
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[13]
		{
			this.gridColumn9, this.gridColumn1, this.gridColumn2, this.gridColumn19, this.gridColumn4, this.gridColumn21, this.gridColumn23, this.gridColumn6, this.gridColumn20, this.gridColumn5,
			this.gridColumn25, this.gridColumn8, this.gridColumn7
		});
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.Name = "gridView1";
		this.gridView1.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.False;
		this.gridView1.OptionsBehavior.KeepGroupExpandedOnSorting = false;
		this.gridView1.OptionsCustomization.AllowSort = false;
		this.gridView1.OptionsMenu.ShowGroupSortSummaryItems = false;
		this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
		this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
		this.gridView1.OptionsPrint.EnableAppearanceOddRow = true;
		this.gridView1.OptionsPrint.PrintFilterInfo = true;
		this.gridView1.OptionsPrint.PrintPreview = true;
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridView1.OptionsView.ShowGroupPanel = false;
		this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanged);
		resources.ApplyResources(this.gridColumn9, "gridColumn9");
		this.gridColumn9.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceCell.Font");
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceHeader.Font");
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.FieldName = "ظ…";
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn1, "gridColumn1");
		this.gridColumn1.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceCell.Font");
		this.gridColumn1.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn1.AppearanceCell.FontStyleDelta");
		this.gridColumn1.AppearanceCell.Options.UseFont = true;
		this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceHeader.Font");
		this.gridColumn1.AppearanceHeader.Options.UseFont = true;
		this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.ColumnEdit = this.repositoryItemButtonEditCodeSearch;
		this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.FieldName = "كود_الصنف";
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.OptionsColumn.AllowEdit = false;
		this.gridColumn1.OptionsColumn.AllowFocus = false;
		this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn2, "gridColumn2");
		this.gridColumn2.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn2.AppearanceCell.Font");
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn2.AppearanceHeader.Font");
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.FieldName = "أسم الصنف";
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.gridColumn2.OptionsColumn.AllowFocus = false;
		this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.FieldName = "الوحدة";
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.AllowFocus = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		this.gridColumn19.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn4, "gridColumn4");
		this.gridColumn4.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceCell.Font");
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceHeader.Font");
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.ColumnEdit = this.repositoryItemButtonEdit5;
		this.gridColumn4.FieldName = "المخزن";
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.AllowEdit = false;
		this.gridColumn4.OptionsColumn.AllowFocus = false;
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn21, "gridColumn21");
		this.gridColumn21.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn21.AppearanceCell.Font");
		this.gridColumn21.AppearanceCell.Options.UseFont = true;
		this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn21.AppearanceHeader.Font");
		this.gridColumn21.AppearanceHeader.Options.UseFont = true;
		this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.gridColumn21.FieldName = "سعرالتكلفة";
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.OptionsColumn.AllowEdit = false;
		this.gridColumn21.OptionsColumn.AllowFocus = false;
		this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn21.Summary"), resources.GetString("gridColumn21.Summary1"), resources.GetString("gridColumn21.Summary2"))
		});
		resources.ApplyResources(this.gridColumn23, "gridColumn23");
		this.gridColumn23.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceCell.Font");
		this.gridColumn23.AppearanceCell.Options.UseFont = true;
		this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceHeader.Font");
		this.gridColumn23.AppearanceHeader.Options.UseFont = true;
		this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.FieldName = "المتوسط";
		this.gridColumn23.MinWidth = 23;
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.OptionsColumn.AllowEdit = false;
		this.gridColumn23.OptionsColumn.AllowFocus = false;
		this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn23.Summary"), resources.GetString("gridColumn23.Summary1"), resources.GetString("gridColumn23.Summary2"))
		});
		this.gridColumn23.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn6, "gridColumn6");
		this.gridColumn6.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceCell.Font");
		this.gridColumn6.AppearanceCell.Options.UseFont = true;
		this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceHeader.Font");
		this.gridColumn6.AppearanceHeader.Options.UseFont = true;
		this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.ColumnEdit = this.SellPriceWithVat;
		this.gridColumn6.FieldName = "سعر البيع";
		this.gridColumn6.MinWidth = 24;
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.AllowFocus = false;
		this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn6.Summary"), resources.GetString("gridColumn6.Summary1"), resources.GetString("gridColumn6.Summary2"))
		});
		resources.ApplyResources(this.gridColumn20, "gridColumn20");
		this.gridColumn20.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn20.AppearanceCell.Font");
		this.gridColumn20.AppearanceCell.Options.UseFont = true;
		this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn20.AppearanceHeader.Font");
		this.gridColumn20.AppearanceHeader.Options.UseFont = true;
		this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.FieldName = "كمية البرنامج";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.OptionsColumn.AllowEdit = false;
		this.gridColumn20.OptionsColumn.AllowFocus = false;
		this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn5, "gridColumn5");
		this.gridColumn5.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceCell.Font");
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceHeader.Font");
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.FieldName = "كمية الجرد";
		this.gridColumn5.MinWidth = 23;
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn5.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn25, "gridColumn25");
		this.gridColumn25.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceCell.Font");
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceHeader.Font");
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.FieldName = "فرق الكميات";
		this.gridColumn25.MinWidth = 23;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.OptionsColumn.AllowEdit = false;
		this.gridColumn25.OptionsColumn.AllowFocus = false;
		this.gridColumn25.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn8, "gridColumn8");
		this.gridColumn8.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn8.AppearanceCell.Font");
		this.gridColumn8.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn8.AppearanceCell.FontStyleDelta");
		this.gridColumn8.AppearanceCell.Options.UseFont = true;
		this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn8.AppearanceHeader.Font");
		this.gridColumn8.AppearanceHeader.Options.UseFont = true;
		this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit6;
		this.gridColumn8.FieldName = "حذف";
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.OptionsColumn.AllowEdit = false;
		this.gridColumn8.OptionsColumn.AllowFocus = false;
		this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn7, "gridColumn7");
		this.gridColumn7.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceCell.Font");
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceHeader.Font");
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.FieldName = "ItemID";
		this.gridColumn7.MinWidth = 24;
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.OptionsColumn.AllowFocus = false;
		this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.cardView1, "cardView1");
		this.cardView1.CardWidth = 199;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		resources.ApplyResources(this.gridColumn10, "gridColumn10");
		this.gridColumn10.Name = "gridColumn10";
		resources.ApplyResources(this.gridColumn11, "gridColumn11");
		this.gridColumn11.Name = "gridColumn11";
		resources.ApplyResources(this.gridColumn12, "gridColumn12");
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn13, "gridColumn13");
		this.gridColumn13.Name = "gridColumn13";
		resources.ApplyResources(this.gridColumn14, "gridColumn14");
		this.gridColumn14.Name = "gridColumn14";
		resources.ApplyResources(this.gridColumn15, "gridColumn15");
		this.gridColumn15.Name = "gridColumn15";
		resources.ApplyResources(this.gridColumn16, "gridColumn16");
		this.gridColumn16.Name = "gridColumn16";
		resources.ApplyResources(this.gridColumn17, "gridColumn17");
		this.gridColumn17.Name = "gridColumn17";
		resources.ApplyResources(this.gridColumn18, "gridColumn18");
		this.gridColumn18.Name = "gridColumn18";
		resources.ApplyResources(this.BtnSearch, "BtnSearch");
		this.BtnSearch.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSearch.Appearance.Font");
		this.BtnSearch.Appearance.Options.UseFont = true;
		this.BtnSearch.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSearch.ImageOptions.Image");
		this.BtnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSearch.Name = "BtnSearch";
		this.BtnSearch.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.BtnPrint, "BtnPrint");
		this.BtnPrint.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrint.Appearance.Font");
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.TxtDate, "TxtDate");
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDate.Properties.Appearance.Font");
		this.TxtDate.Properties.Appearance.Options.UseFont = true;
		this.TxtDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate.Properties.Buttons"))
		});
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		resources.ApplyResources(this.label24, "label24");
		this.label24.Name = "label24";
		resources.ApplyResources(this.TxtNumNote, "TxtNumNote");
		this.TxtNumNote.Name = "TxtNumNote";
		this.TxtNumNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumNote.Properties.Appearance.Font");
		this.TxtNumNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNumNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions69");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions70");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"), resources.GetString("repositoryItemButtonEdit3.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit3.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("repositoryItemButtonEdit3.Buttons6"), resources.GetObject("repositoryItemButtonEdit3.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit3.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit3.Buttons9"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions7.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions7, "editorButtonImageOptions71");
		editorButtonImageOptions7.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.TxtBarcode);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.comboClass);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.RadioD);
		this.groupBox1.Controls.Add(this.BtnSearch);
		this.groupBox1.Controls.Add(this.RadioAll);
		this.groupBox1.Controls.Add(this.RadioA);
		this.groupBox1.Controls.Add(this.RadioC);
		this.groupBox1.Controls.Add(this.RadioB);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Controls.Add(this.comboCompany);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.comboGroup);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.comboItemType);
		this.groupBox1.Controls.Add(this.comboStore);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.TxtNumNote);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.comboClass, "comboClass");
		this.comboClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboClass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.comboClass.DropDownWidth = 256;
		this.comboClass.FormattingEnabled = true;
		this.comboClass.Name = "comboClass";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.RadioD, "RadioD");
		this.RadioD.Name = "RadioD";
		this.RadioD.TabStop = true;
		this.RadioD.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.RadioAll, "RadioAll");
		this.RadioAll.Name = "RadioAll";
		this.RadioAll.TabStop = true;
		this.RadioAll.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.RadioA, "RadioA");
		this.RadioA.Checked = true;
		this.RadioA.Name = "RadioA";
		this.RadioA.TabStop = true;
		this.RadioA.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.RadioC, "RadioC");
		this.RadioC.Name = "RadioC";
		this.RadioC.TabStop = true;
		this.RadioC.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.RadioB, "RadioB");
		this.RadioB.Name = "RadioB";
		this.RadioB.TabStop = true;
		this.RadioB.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.TxtCode, "TxtCode");
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCode.Properties.Appearance.Font");
		this.TxtCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCode.Properties.Appearance.Options.UseFont = true;
		this.TxtCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCode.Properties.ReadOnly = true;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		resources.ApplyResources(this.comboCompany, "comboCompany");
		this.comboCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.comboCompany.DropDownWidth = 256;
		this.comboCompany.FormattingEnabled = true;
		this.comboCompany.Name = "comboCompany";
		resources.ApplyResources(this.label17, "label17");
		this.label17.Name = "label17";
		resources.ApplyResources(this.comboGroup, "comboGroup");
		this.comboGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.comboGroup.FormattingEnabled = true;
		this.comboGroup.Name = "comboGroup";
		this.comboGroup.SelectedIndexChanged += new System.EventHandler(comboGroup_SelectedIndexChanged);
		resources.ApplyResources(this.label18, "label18");
		this.label18.Name = "label18";
		resources.ApplyResources(this.comboItemType, "comboItemType");
		this.comboItemType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboItemType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.comboItemType.FormattingEnabled = true;
		this.comboItemType.Name = "comboItemType";
		resources.ApplyResources(this.comboStore, "comboStore");
		this.comboStore.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboStore.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.comboStore.FormattingEnabled = true;
		this.comboStore.Name = "comboStore";
		resources.ApplyResources(this.label20, "label20");
		this.label20.Name = "label20";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.repositoryItemButtonEditCode, "repositoryItemButtonEditCode");
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions72");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCode.Buttons"), resources.GetString("repositoryItemButtonEditCode.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCode.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("repositoryItemButtonEditCode.Buttons6"), resources.GetObject("repositoryItemButtonEditCode.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCode.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCode.Buttons9"))
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
		resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		resources.ApplyResources(this.MenuSearchItem, "MenuSearchItem");
		this.MenuSearchItem.Name = "MenuSearchItem";
		resources.ApplyResources(this.RecoveredMenuItemFix9, "حركةصنفToolStripMenuItem");
		this.RecoveredMenuItemFix9.Name = "حركةصنفToolStripMenuItem";
		resources.ApplyResources(this.MenuNewItem, "MenuNewItem");
		this.MenuNewItem.Name = "MenuNewItem";
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.MenuPrintFatoura, "MenuPrintFatoura");
		this.MenuPrintFatoura.Name = "MenuPrintFatoura";
		resources.ApplyResources(this.MenuPreviewFatoura, "MenuPreviewFatoura");
		this.MenuPreviewFatoura.Name = "MenuPreviewFatoura";
		resources.ApplyResources(this.MenuPrintQued, "MenuPrintQued");
		this.MenuPrintQued.Name = "MenuPrintQued";
		resources.ApplyResources(this.RecoveredMenuItemFix10, "طباعةToolStripMenuItem1");
		this.RecoveredMenuItemFix10.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.MenuPrintQued, this.MenuPreviewFatoura, this.MenuPrintFatoura });
		this.RecoveredMenuItemFix10.Name = "طباعةToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix11, "استعلامToolStripMenuItem1");
		this.RecoveredMenuItemFix11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix2, this.RecoveredMenuItemFix1, this.RecoveredMenuItemFix3 });
		this.RecoveredMenuItemFix11.Name = "استعلامToolStripMenuItem1";
		resources.ApplyResources(this.MenuDelete, "MenuDelete");
		this.MenuDelete.Name = "MenuDelete";
		resources.ApplyResources(this.MenuSave, "MenuSave");
		this.MenuSave.Name = "MenuSave";
		resources.ApplyResources(this.MenuNew, "MenuNew");
		this.MenuNew.Name = "MenuNew";
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.RecoveredMenuItemFix10, this.toolStripSeparator1, this.MenuNewItem, this.RecoveredMenuItemFix9, this.MenuSearchItem, this.toolStripSeparator4,
			this.RecoveredMenuItemFix11, this.RecoveredMenuItemFix4, this.toolStripSeparator2, this.RecoveredMenuItemFix8, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		resources.ApplyResources(this.MenuEdit, "MenuEdit");
		this.MenuEdit.Name = "MenuEdit";
		this.ribbonPage2.Name = "ribbonPage2";
		resources.ApplyResources(this.ribbonPage2, "ribbonPage2");
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNew.Appearance.Font");
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		resources.ApplyResources(this.TxtBarcode, "TxtBarcode");
		this.TxtBarcode.Name = "TxtBarcode";
		this.TxtBarcode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcode.Properties.Appearance.Font");
		this.TxtBarcode.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.TxtBarcode.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcode.Properties.Appearance.Options.UseForeColor = true;
		this.TxtBarcode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		this.TxtBarcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcode.Properties.Buttons"))
		});
		this.TxtBarcode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcode_ButtonClick);
		this.TxtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtBarcode_KeyDown);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmStoreGardCat";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.SellPriceWithVat).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


