using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
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
using GTSErpSystem.BLL.OrderRent;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.OrderRent.Search;
using GTSErpSystem.Frms.Orders.FrmCachMoney;
using GTSErpSystem.Frms.Orders.FrmReservations;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using Humanizer;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.OrderRent;

public class FrmRentalInvoice : XtraForm
{
	private Calss_CostCenter Cost = new Calss_CostCenter();

	private Class_Orders ORD = new Class_Orders();

	private Class_Customer Cust = new Class_Customer();

	private Class_Contract Contract = new Class_Contract();

	private Class_Rent Rent = new Class_Rent();

	private Class_PriceShowRent PSR = new Class_PriceShowRent();

	private DataTable datatable = new DataTable();

	private GTSdbEntities db = new GTSdbEntities();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_Items ITM = new Class_Items();

	private string fileNameAR = Application.StartupPath + "\\Layout\\FrmRentalInvoice.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\FrmRentalInvoice.xml";

	private int SN = 1;

	private int ItmID = 0;

	private int StorId = 0;

	private string UnitName = string.Empty;

	public string CodeItm;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	public string words;

	public decimal FrmCash;

	public decimal FrmBank;

	public decimal Paid;

	public decimal Rest;

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private IContainer components = null;

	private Label label11;

	private TextEdit TxtNote;

	private Label label10;

	private TextEdit TxtSuppIDNum;

	private Label label9;

	private TextEdit TxtAddress;

	private Label LBlSuppAccount;

	private Label label26;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private Label LblSup;

	private DateEdit TxtRentalDate;

	private Label label23;

	private TextEdit TxtParentName;

	private Label label22;

	private TextEdit TxtPhone;

	private Label label21;

	private Label label18;

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private ToolStripMenuItem RecoveredMenuItemFix1;

	private ButtonEdit TxtSuppliers;

	private Label label17;

	private ButtonEdit TxtCode;

	private GroupBox groupBox1;

	private GridColumn gridColumn18;

	private ToolStripMenuItem RecoveredMenuItemFix2;

	private ToolStripMenuItem RecoveredMenuItemFix3;

	private ToolStripMenuItem RecoveredMenuItemFix4;

	private Label label12;

	private NumericEditBox TxtDeposit;

	private TextEdit TxtLocation;

	private Label label25;

	private NumericEditBox txtdicoutprege;

	private GroupBox groupBox2;

	private TextEdit TxtTafqit;

	private NumericEditBox TxtNet;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private SimpleButton BtnNew;

	private Label label8;

	private SimpleButton BtnDelete;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtAllDiscount;

	private SimpleButton BtnExit;

	private NumericEditBox TxtTotals;

	private NumericEditBox TxtTotalDiscount;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private NumericEditBox TxtDiscount;

	private NumericEditBox TxtVatDisc;

	private Label label5;

	private Label label6;

	private RibbonPage ribbonPage2;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem MenuNew;

	private ToolStripMenuItem MenuSave;

	private ToolStripMenuItem MenuEdit;

	private ToolStripMenuItem MenuDelete;

	private ToolStripMenuItem RecoveredMenuItemFix5;

	private ToolStripMenuItem MenuPrintQued;

	private ToolStripMenuItem MenuPreviewFatoura;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuNewItem;

	private ToolStripMenuItem RecoveredMenuItemFix6;

	private ToolStripMenuItem MenuSearchItem;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem MenuPurchOrder;

	private ToolStripMenuItem RecoveredMenuItemFix7;

	private ToolStripMenuItem RecoveredMenuItemFix8;

	private ToolStripMenuItem RecoveredMenuItemFix9;

	private ToolStripMenuItem RecoveredMenuItemFix10;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem RecoveredMenuItemFix11;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem MenuExit;

	private GroupBox groupBox3;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn gridColumn27;

	private GridColumn shearch;

	private GridColumn gridColumn19;

	private GridColumn gridColumn4;

	private GridColumn gridColumn23;

	private GridColumn gridColumn24;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private GridColumn gridColumn22;

	private GridColumn gridColumn5;

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

	private DateEdit TxtDateReceipt;

	private Label label13;

	private Label label14;

	private TextEdit TxtDayNum;

	private GridColumn gridColumn26;

	private GridColumn gridColumn28;

	private GridColumn gridColumn29;

	private GridColumn gridColumn30;

	private GridColumn gridColumn31;

	private GridColumn gridColumn32;

	private GridColumn gridColumn33;

	private GridColumn gridColumn34;

	private GridColumn gridColumn35;

	private GridColumn gridColumn36;

	private GridColumn gridColumn25;

	private GridColumn gridColumn37;

	private System.Windows.Forms.ComboBox TxtType;

	private Label label20;

	private Label label37;

	private TextEdit TxtCharge;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private RepositoryItemButtonEdit repositoryItemButtonEdit7;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private SimpleButton BtnShowPrice;

	private SimpleButton BtnNewCustomer;

	public FrmRentalInvoice()
	{
		InitializeComponent();
		Bind();
		SetInitialRow();
		BtnDefault();
	}

	private void gridView1_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
	{
		e.ExceptionMode = ExceptionMode.NoAction;
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[1];
	}

	private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
	{
		try
		{
			if (e.Column.FieldName == "ItemCode" || e.Column.FieldName == "ItemName")
			{
				InsertItemRow(e);
			}
			else
			{
				UpdateItemRow(e.Column.FieldName, e);
			}
			if (e.Column.FieldName == "UnitNum")
			{
				foreach (DataRow row in datatable.Rows)
				{
					int num = Convert.ToInt32(row["SN"]);
					if (num == SN)
					{
						row["UnitNum"] = Convert.ToDecimal(e.Value);
					}
				}
			}
			if (e.Column.FieldName == "Net")
			{
				UpdateItemRowByNet(e);
			}
			InsertNewRow();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			gridControl1.DataSource = datatable;
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[1];
			gridView1.ShowEditor();
		}
		catch (Exception)
		{
		}
	}

	private void gridView1_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
	{
		int num = e.Bounds.Height;
		Brush gradientBrush = e.Cache.GetGradientBrush(e.Bounds, Color.Wheat, Color.FloralWhite, LinearGradientMode.Vertical);
		Rectangle bounds = e.Bounds;
		BorderPainter borderPainter = BorderHelper.GetPainter(BorderStyles.Style3D);
		AppearanceObject appearanceObject = new AppearanceObject(e.Appearance);
		appearanceObject.BorderColor = Color.DarkGray;
		appearanceObject.ParentAppearance.Options.UseTextOptions = true;
		appearanceObject.ParentAppearance.TextOptions.HAlignment = HorzAlignment.Center;
		appearanceObject.ParentAppearance.Options.UseFont = true;
		appearanceObject.ParentAppearance.ForeColor = Color.Red;
		borderPainter.DrawObject(new BorderObjectInfoArgs(e.Cache, appearanceObject, bounds));
		bounds.Inflate(-1, -1);
		e.Cache.FillRectangle(gradientBrush, bounds);
		bounds.Inflate(-2, 0);
		e.Appearance.DrawString(e.Cache, e.Info.DisplayText, bounds);
		e.Handled = true;
	}

	private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
	{
		try
		{
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("ItemID");
			if (focusedRowCellDisplayText != string.Empty)
			{
				ItmID = Convert.ToInt32(gridView1.GetFocusedRowCellDisplayText("ItemID"));
			}
			string codeItm = Convert.ToString(gridView1.GetFocusedRowCellDisplayText("ItemCode"));
			UnitName = gridView1.GetFocusedRowCellDisplayText("UnitName");
			string StoreName = gridView1.GetFocusedRowCellDisplayText("StoreName");
			StorId = (from x in gTSdbEntities.Account_Stores
				where x.Store_Name == StoreName
				select x.ID).FirstOrDefault();
			CodeItm = codeItm;
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("SN");
			if (!int.TryParse(focusedRowCellDisplayText2, out SN))
			{
				SN = 0;
			}
		}
		catch (Exception)
		{
		}
	}

	private void TxtSuppliers_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustCode > 0)
			{
				int FrmID = frmSearchCustomer.CustCode;
				Account_CustSup account_CustSup = (from x in Cust.GetAll()
					where x.ID == FrmID && x.IsCustomers == true
					select x).FirstOrDefault();
				LblSup.Text = FrmID.ToString();
				TxtSuppliers.Text = account_CustSup.CustSuppName;
				TxtPhone.Text = account_CustSup.Phone;
				TxtAddress.Text = account_CustSup.Address;
				TxtSuppIDNum.Text = account_CustSup.CommercialRecord;
				Charge(FrmID);
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		NewFrm();
		SN = 1;
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchRentalInvoice frmSearchRentalInvoice = new FrmSearchRentalInvoice();
			frmSearchRentalInvoice.ShowDialog();
			if (frmSearchRentalInvoice.CustCode > 0)
			{
				int custCode = frmSearchRentalInvoice.CustCode;
				Orders_RentalInvoice orders_RentalInvoice = Rent.GetPurchasesRental(custCode).FirstOrDefault();
				CBCostCenter.SelectedValue = orders_RentalInvoice.CostCentersID;
				TxtCode.Text = orders_RentalInvoice.PurBranchID.ToString();
				LblSup.Text = orders_RentalInvoice.SupplierID.ToString();
				TxtSuppliers.Text = orders_RentalInvoice.SupplierName;
				TxtRentalDate.DateTime = Convert.ToDateTime(orders_RentalInvoice.RentalDate);
				TxtDateReceipt.DateTime = Convert.ToDateTime(orders_RentalInvoice.ReturnDate);
				TxtParentName.Text = orders_RentalInvoice.ParentName;
				TxtPhone.Text = orders_RentalInvoice.SupplierPhone;
				TxtNote.Text = orders_RentalInvoice.Note;
				TxtDeposit.Text = orders_RentalInvoice.deposit.ToString();
				TxtAddress.Text = orders_RentalInvoice.Address;
				TxtSuppIDNum.Text = orders_RentalInvoice.SupplierIDNum;
				TxtTotals.Text = Convert.ToString(orders_RentalInvoice.TotalPrices);
				TxtVat.Text = Convert.ToString(orders_RentalInvoice.Tax);
				TxtTotalDiscount.Text = Convert.ToString(orders_RentalInvoice.TotalPrices_Discount);
				TxtDiscount.Text = orders_RentalInvoice.DiscountNum.ToString();
				txtdicoutprege.Text = orders_RentalInvoice.DiscountPerantage.ToString();
				TxtAllDiscount.Text = orders_RentalInvoice.AllDiscount.ToString();
				TxtVatDisc.Text = orders_RentalInvoice.Tax_Discount.ToString();
				TxtNet.Text = orders_RentalInvoice.Net.ToString();
				TxtType.Text = ORD.GetAccountNameById(Convert.ToInt32(orders_RentalInvoice.Order_Paymant_Type));
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = true;
				Charge(Convert.ToInt32(LblSup.Text));
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = false;
				datatable.Rows.Clear();
				List<Ordert_RentalInvoiceDetails> purchasesRentalDetails = Rent.GetPurchasesRentalDetails(custCode);
				int num = 1;
				foreach (Ordert_RentalInvoiceDetails item in purchasesRentalDetails)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = ORD.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
					dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["DayNum"] = FUNC_DecimalNum(Convert.ToDecimal(item.DaysNum));
					dataRow["ItemCode"] = item_Items.Item_code;
					dataRow["ItemName"] = item_Items.item_Name;
					if (item.ItemUnitType == "الصغرى")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "المتوسطة")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "الكبرى")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					dataRow["UnitType"] = item.ItemUnitType;
					dataRow["StoreID"] = item.StoreID;
					dataRow["StoreName"] = (from x in db.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["Quantity"] = FUNC_DecimalNum(Convert.ToDecimal(item.Quantity));
					dataRow["AvergItem"] = item.LastCost;
					dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
					dataRow["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.UnitPrice));
					dataRow["OrginalPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.Total) / Convert.ToDecimal(item.Quantity));
					dataRow["UnitPriceDisc"] = Math.Round(Convert.ToDecimal(item.Total), DecimalNum);
					dataRow["PercentDisc"] = Math.Round(Convert.ToDecimal(item.PercentDisc), DecimalNum);
					dataRow["AmountDisc"] = Math.Round(Convert.ToDecimal(item.AmountDisc), DecimalNum);
					dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(item.SumPrice), DecimalNum);
					dataRow["Total"] = Math.Round(Convert.ToDecimal(item.Total), DecimalNum);
					dataRow["SumPriceDisc"] = Math.Round(Convert.ToDecimal(item.SumPriceDisc), DecimalNum);
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice));
						dataRow["IsItemTax"] = 1;
						dataRow["Tax_Value"] = item_Items.Tax_Value;
					}
					else
					{
						dataRow["IsItemTax"] = 0;
						dataRow["Tax_Value"] = 0;
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice));
					}
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				ArrangeDatatable();
				IsEnableFalse();
			}
			else
			{
				TxtSuppliers.Focus();
			}
		}
		catch (Exception)
		{
			Clear();
		}
		Tafqit();
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		Delete();
		SN = 1;
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnShowPrice_Click(object sender, EventArgs e)
	{
		GetPricePffer();
	}

	private void BtnDefault()
	{
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 12);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 12);
		if (!flag)
		{
			BtnEdit.Visible = false;
		}
		if (!flag2)
		{
			BtnDelete.Visible = false;
		}
		if (LoginDetails.FrmOrderClose)
		{
			BtnNew.Visible = false;
			BtnSave.Visible = false;
			BtnEdit.Visible = false;
			BtnDelete.Visible = false;
			BtnExit.Visible = false;
			BtnPrint.Visible = false;
		}
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtRentalDate.DateTime = DateTime.Now;
		TxtDateReceipt.DateTime = DateTime.Now;
		TxtCode.Focus();
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("ItemID", typeof(int));
		datatable.Columns.Add("ItemCode", typeof(string));
		datatable.Columns.Add("ItemName", typeof(string));
		datatable.Columns.Add("ItemNote", typeof(string));
		datatable.Columns.Add("UnitType", typeof(string));
		datatable.Columns.Add("ItemUnitID", typeof(int));
		datatable.Columns.Add("UnitName", typeof(string));
		datatable.Columns.Add("Quantity", typeof(decimal));
		datatable.Columns.Add("DayNum", typeof(decimal));
		datatable.Columns.Add("StoreName", typeof(string));
		datatable.Columns.Add("StoreID", typeof(int));
		datatable.Columns.Add("OrginalPrice", typeof(decimal));
		datatable.Columns.Add("UnitPrice", typeof(decimal));
		datatable.Columns.Add("SumPrice", typeof(decimal));
		datatable.Columns.Add("ItemVat", typeof(decimal));
		datatable.Columns.Add("Total", typeof(decimal));
		datatable.Columns.Add("PercentDisc", typeof(decimal));
		datatable.Columns.Add("AmountDisc", typeof(decimal));
		datatable.Columns.Add("UnitPriceDisc", typeof(decimal));
		datatable.Columns.Add("SumPriceDisc", typeof(decimal));
		datatable.Columns.Add("ItemVatDisc", typeof(decimal));
		datatable.Columns.Add("Net", typeof(decimal));
		datatable.Columns.Add("AvergItem", typeof(decimal));
		datatable.Columns.Add("SumAvergItem", typeof(decimal));
		datatable.Columns.Add("IsItemTax", typeof(bool));
		datatable.Columns.Add("Tax_Value", typeof(decimal));
		datatable.Columns.Add("DeleteItem", typeof(int));
		datatable.Columns.Add("SN", typeof(int));
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		gridView1.CustomDrawFooterCell += gridView1_CustomDrawFooterCell;
		InsertNewRow();
	}

	public void Bind()
	{
		try
		{
			CBCostCenter.DataSource = Cost.GetSearchAccountCostCenter();
			CBCostCenter.DisplayMember = "CostCentersName";
			CBCostCenter.ValueMember = "CostCentersID";
			CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
			TxtSuppliers.Text = Cust.GetFirstCustomer();
			TxtType.DataSource = ORD.GetAccountType();
			TxtType.DisplayMember = "Name";
			TxtType.ValueMember = "ID";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Clear()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtCode.Text = "";
		datatable.Rows.Clear();
		gridControl1.DataSource = datatable;
		TxtSuppliers.Text = "";
		TxtAddress.Text = "";
		TxtSuppIDNum.Text = "";
		TxtParentName.Text = "";
		TxtDiscount.Text = "";
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtCharge.Text = "0.00";
		TxtSuppliers.Text = Cust.GetFirstCustomer();
		TxtPhone.Text = "";
		TxtNote.Text = "";
		TxtVatDisc.Text = "0.00";
		TxtTafqit.Text = "";
		TxtTotals.Text = "0.00";
		TxtDeposit.Text = "0.00";
		TxtTotalDiscount.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtLocation.Text = "";
		TxtRentalDate.DateTime = DateTime.Now;
		TxtDateReceipt.DateTime = DateTime.Now;
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
		TxtDiscount.Enabled = true;
		TxtParentName.Enabled = true;
		TxtRentalDate.Enabled = true;
		TxtDateReceipt.Enabled = true;
	}

	public void CheckFfistRow()
	{
		if (datatable.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["ItemID"]);
			if (text == "0")
			{
				row.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
	}

	public void ArrangeDatatable()
	{
		int num = 1;
		foreach (DataRow row in datatable.Rows)
		{
			row["SN"] = num;
			num++;
		}
	}

	public void InsertNewRow()
	{
		CheckFfistRow();
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["ItemID"] = 0;
		dataRow["ItemCode"] = string.Empty;
		dataRow["ItemName"] = string.Empty;
		dataRow["ItemNote"] = string.Empty;
		dataRow["UnitType"] = "الصغرى";
		dataRow["ItemUnitID"] = 1;
		dataRow["UnitName"] = string.Empty;
		dataRow["Quantity"] = 1;
		dataRow["DayNum"] = 0;
		List<Account_Stores> stores = Contract.Get_Stores();
		Account_Stores account_Stores = stores.FirstOrDefault((Account_Stores store) => store.ID == LoginDetails.StoreID);
		if (account_Stores != null)
		{
			dataRow["StoreName"] = account_Stores.Store_Name;
		}
		else
		{
			dataRow["StoreName"] = string.Empty;
		}
		dataRow["StoreID"] = Contract.Get_StoresIDByName(dataRow["StoreName"].ToString());
		dataRow["OrginalPrice"] = 0;
		dataRow["UnitPrice"] = 0;
		dataRow["SumPrice"] = 0;
		dataRow["ItemVat"] = 0;
		dataRow["Total"] = 0;
		dataRow["PercentDisc"] = 0;
		dataRow["AmountDisc"] = 0;
		dataRow["UnitPriceDisc"] = 0;
		dataRow["SumPriceDisc"] = 0;
		dataRow["ItemVatDisc"] = 0;
		dataRow["Net"] = 0;
		dataRow["AvergItem"] = 0;
		dataRow["SumAvergItem"] = 0;
		dataRow["IsItemTax"] = false;
		dataRow["Tax_Value"] = 0;
		dataRow["DeleteItem"] = 1;
		datatable.Rows.Add(dataRow);
		ArrangeDatatable();
		gridControl1.DataSource = datatable;
	}

	public void InsertItemRow(CellValueChangedEventArgs e)
	{
		string code = Convert.ToString(e.Value);
		Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(code).FirstOrDefault();
		if (get_Item_ByCodeALL_Result == null)
		{
			return;
		}
		foreach (DataRow row in datatable.Rows)
		{
			int num = Convert.ToInt32(row["SN"]);
			if (num != SN)
			{
				continue;
			}
			row["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
			row["ItemCode"] = get_Item_ByCodeALL_Result.Item_code;
			row["ItemName"] = get_Item_ByCodeALL_Result.item_Name;
			row["ItemNote"] = string.Empty;
			row["UnitType"] = "الصغرى";
			row["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
			row["Quantity"] = 1;
			if (Convert.ToDecimal(TxtDayNum.Text) <= 0m)
			{
				row["DayNum"] = 1;
				TxtDayNum.Text = "1";
			}
			else
			{
				row["DayNum"] = TxtDayNum.Text;
			}
			List<Account_Stores> stores = Contract.Get_Stores();
			Account_Stores account_Stores = stores.FirstOrDefault((Account_Stores store) => store.ID == LoginDetails.StoreID);
			if (account_Stores != null)
			{
				row["StoreName"] = account_Stores.Store_Name;
			}
			else
			{
				row["StoreName"] = string.Empty;
			}
			row["StoreID"] = Contract.Get_StoresIDByName(row["StoreName"].ToString());
			row["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
			row["OrginalPrice"] = row["UnitPrice"];
			row["SumPrice"] = row["UnitPrice"];
			row["PercentDisc"] = 0;
			row["AmountDisc"] = 0;
			row["UnitPriceDisc"] = row["UnitPrice"];
			row["SumPriceDisc"] = row["UnitPriceDisc"];
			row["IsItemTax"] = get_Item_ByCodeALL_Result.Is_Tax;
			row["Tax_Value"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.Tax_Value);
			if (!LoginDetails.IsVat)
			{
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				row["ItemVatDisc"] = 0;
				row["Net"] = row["SumPriceDisc"];
			}
			else if (Convert.ToBoolean(row["IsItemTax"]))
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
					row["SumPrice"] = Convert.ToDecimal(row["Total"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
					row["UnitPrice"] = row["SumPrice"];
					row["ItemVat"] = Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]);
					row["ItemVatDisc"] = row["ItemVat"];
					row["Net"] = row["Total"];
					row["UnitPriceDisc"] = row["UnitPrice"];
					row["SumPriceDisc"] = row["SumPrice"];
				}
				else
				{
					row["ItemVat"] = Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m);
					row["Total"] = Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]);
					row["ItemVatDisc"] = row["ItemVat"];
					row["Net"] = row["Total"];
				}
			}
			else
			{
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				row["ItemVatDisc"] = 0;
				row["Net"] = row["SumPriceDisc"];
			}
			row["AvergItem"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.Average_cost);
			row["SumAvergItem"] = row["AvergItem"];
			row["DeleteItem"] = 0;
		}
	}

	public void UpdateItemRow(string TxtChange, CellValueChangedEventArgs e)
	{
		decimal num = Convert.ToDecimal(e.Value);
		bool flag = false;
		bool flag2 = false;
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToInt32(row["SN"]) == SN)
			{
				decimal quantity = ((TxtChange == "Quantity") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"])));
				decimal unitPrice = ((TxtChange == "UnitPrice") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"])));
				if (TxtChange == "UnitPrice" && LoginDetails.ItemTotalWithVat)
				{
					row["OrginalPrice"] = num * (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
				}
				decimal num2 = ((TxtChange == "PercentDisc") ? FUNC_DecimalNum(num) : FUNC_DecimalNum(Convert.ToDecimal(row["PercentDisc"])));
				if (num2 > 100m)
				{
					break;
				}
				flag2 = TxtChange == "PercentDisc" || TxtChange == "AmountDisc";
				decimal amountDisc = ((TxtChange == "AmountDisc") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["AmountDisc"])));
				flag = TxtChange == "AmountDisc";
				decimal avergItem = ((TxtChange == "AvergItem") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"])));
				decimal daysNum = ((TxtChange == "DayNum") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["DayNum"])));
				UpdateRowFactionAccount(quantity, unitPrice, flag2, flag, num2, amountDisc, avergItem, daysNum);
			}
		}
	}

	public void UpdateRowFactionAccount(decimal Quantity, decimal UnitPrice, bool IsDisc, bool IsDiscAmount, decimal PercentDisc, decimal AmountDisc, decimal AvergItem, decimal DaysNum)
	{
		decimal num = Quantity * DaysNum;
		decimal value = UnitPrice * num;
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToInt32(row["SN"]) != SN)
			{
				continue;
			}
			bool flag = row["IsItemTax"] != DBNull.Value && Convert.ToBoolean(row["IsItemTax"]);
			row["Quantity"] = Quantity;
			row["UnitPrice"] = FUNC_DecimalNum(UnitPrice);
			row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
			row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
			row["AvergItem"] = AvergItem;
			if (!LoginDetails.IsVat)
			{
				row["SumPrice"] = UnitPrice * num;
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (IsDisc && IsDiscAmount)
				{
					PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(row["SumPrice"]) * 100m);
					row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
				}
				else if (IsDisc && !IsDiscAmount)
				{
					AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(row["SumPrice"]));
					row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
				}
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["SumPrice"]) * (PercentDisc / 100m));
				row["ItemVatDisc"] = 0;
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
				row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * (UnitPrice * num) / 100m);
			}
			else if (flag)
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num2 = Convert.ToDecimal(LoginDetails.PerVat) / 100m;
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["OrginalPrice"]) * num);
					row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) / (num2 + 1m));
					row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]));
					if (IsDisc && IsDiscAmount)
					{
						PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(row["SumPrice"]) * 100m);
						row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
					}
					else if (IsDisc && !IsDiscAmount)
					{
						AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(row["SumPrice"]));
						row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
					}
					row["SumPriceDisc"] = Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(AmountDisc);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * num2);
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
				}
				else
				{
					row["SumPrice"] = FUNC_DecimalNum(value);
					if (IsDisc && IsDiscAmount)
					{
						PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(row["SumPrice"]) * 100m);
						row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
					}
					else if (IsDisc && !IsDiscAmount)
					{
						AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(row["SumPrice"]));
						row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
					}
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["SumPrice"]) * (PercentDisc / 100m));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
					row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * (UnitPrice * num) / 100m);
				}
			}
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
		}
	}

	public void UpdateItemRowByNet(CellValueChangedEventArgs e)
	{
		decimal num = Convert.ToDecimal(e.Value);
		foreach (DataRow row in datatable.Rows)
		{
			int num2 = Convert.ToInt32(row["SN"]);
			if (num2 != SN || !(row["ItemCode"].ToString() != string.Empty))
			{
				continue;
			}
			row["DayNum"] = 1;
			TxtDayNum.Text = "1";
			if (!LoginDetails.IsVat)
			{
				row["Net"] = num;
				row["SumPrice"] = num;
				row["SumPriceDisc"] = num;
				row["Total"] = num;
				row["UnitPrice"] = FUNC_DecimalNum(num / Convert.ToDecimal(row["Quantity"]));
				row["OrginalPrice"] = row["UnitPrice"];
				row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPrice"]);
				row["ItemVat"] = 0;
				row["ItemVatDisc"] = 0;
				row["AmountDisc"] = 0;
				row["PercentDisc"] = 0;
			}
			else if (Convert.ToBoolean(row["IsItemTax"]))
			{
				decimal num3 = num / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
				decimal value = num3 / Convert.ToDecimal(row["Quantity"]);
				row["Net"] = num;
				row["ItemVat"] = num - num3;
				row["SumPrice"] = num3;
				row["UnitPrice"] = FUNC_DecimalNum(value);
				row["UnitPriceDisc"] = row["UnitPrice"];
				row["Total"] = num;
				row["SumPriceDisc"] = row["SumPrice"];
				row["ItemVat"] = num - Convert.ToDecimal(row["UnitPrice"]);
				row["ItemVatDisc"] = row["ItemVat"];
				row["AmountDisc"] = 0;
				row["PercentDisc"] = 0;
				string text = row["OrginalPrice"].ToString();
				if (LoginDetails.ItemTotalWithVat)
				{
					row["OrginalPrice"] = Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
				}
				else
				{
					row["OrginalPrice"] = row["UnitPrice"];
				}
			}
			break;
		}
	}

	private void Cal_FinalResultTotals(decimal Per, string DicountType)
	{
		try
		{
			if (datatable.Rows.Count < 1)
			{
				TxtTotals.Text = "0.00";
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtAllDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtVat.Text = "0.000";
				TxtVatDisc.Text = "0.00";
			}
			else if (!ORD.IsVatActive())
			{
				if (Per == 0m)
				{
					No_Discount_NoVat();
				}
				else
				{
					DiscountPercent_NoVat(Per, DicountType);
				}
			}
			else if (LoginDetails.ItemTotalWithVat)
			{
				if (Per == 0m)
				{
					No_Discount_PriceWithVat();
				}
				else
				{
					DiscountPercent_PriceWithVat(Per, DicountType);
				}
			}
			else if (Per == 0m)
			{
				No_Discount_Vat();
			}
			else
			{
				DiscountPercent_Vat(Per, DicountType);
			}
			gridControl1.DataSource = datatable;
		}
		catch (Exception)
		{
		}
	}

	private void No_Discount_NoVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = Convert.ToDecimal(row["UnitPrice"]) * num;
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = row["UnitPrice"];
					row["ItemVatDisc"] = row["ItemVat"];
					row["SumPriceDisc"] = row["SumPrice"];
					row["Net"] = row["Total"];
				}
				else
				{
					row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m);
					row["SumPriceDisc"] = Convert.ToDecimal(row["UnitPriceDisc"]) * num;
					row["ItemVatDisc"] = row["ItemVat"];
					row["Net"] = row["SumPriceDisc"];
				}
				row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * num;
			}
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num2);
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtNet.Text = TxtTotals.Text.ToString();
	}

	private void No_Discount_Vat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = row["UnitPrice"];
					row["ItemVatDisc"] = row["ItemVat"];
					row["SumPriceDisc"] = row["SumPrice"];
					row["Net"] = row["Total"];
				}
				else
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Net"] = Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]);
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num2);
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num3.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = num4.ToString();
		TxtNet.Text = (num2 + num4).ToString();
	}

	private void No_Discount_PriceWithVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
				decimal num2 = ((row["Tax_Value"] != DBNull.Value) ? Convert.ToDecimal(row["Tax_Value"]) : 0m);
				row["Total"] = Math.Round(Convert.ToDecimal(row["SumPrice"]) * (num2 / 100m + 1m), 1);
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]));
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				}
				else
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num3);
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num4.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = TxtVat.Text;
		TxtNet.Text = (num3 + num4).ToString();
	}

	private void DiscountPercent_NoVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				}
				else
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num2);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num3 = FUNC_DecimalNum(Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["DayNum"]));
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPrice"]) * num3);
				row2["ItemVat"] = 0;
				row2["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPrice"]));
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num3);
				row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["ItemVat"]));
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num3);
			}
		}
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	private void DiscountPercent_Vat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
					row["ItemVatDisc"] = row["ItemVat"];
					row["SumPriceDisc"] = row["SumPrice"];
					row["Net"] = row["Total"];
				}
				else
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat.Text = Convert.ToString(num2);
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num3);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(TxtTotals.Text) + num2) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num4 = FUNC_DecimalNum(Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["DayNum"]));
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num4);
				row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m));
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num4);
			}
		}
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVatDisc.Text = Convert.ToString(num5);
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	private void DiscountPercent_PriceWithVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m));
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]));
				row["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) / num);
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				}
				else
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat.Text = Convert.ToString(num2);
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num3);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(TxtTotals.Text) + num2) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num4 = FUNC_DecimalNum(Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["DayNum"]));
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num4);
				row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m));
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num4);
			}
		}
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVatDisc.Text = Convert.ToString(num5);
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	public void SearchItems(string Item = null)
	{
		try
		{
			LoginDetails.ItemSearch = CodeItm;
			LoginDetails.ISCheckSearchItem = false;
			LoginDetails.ISPurches = true;
			FrmSearchItems1 frmSearchItems = new FrmSearchItems1(Item);
			frmSearchItems.ShowDialog();
			if (LoginDetails.ItemSearchAll2 == null)
			{
				return;
			}
			if (LoginDetails.ItemSearchAll2.Count > 0)
			{
				foreach (string item in LoginDetails.ItemSearchAll2)
				{
					string text = item.ToString();
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(text).FirstOrDefault();
					if (get_Item_ByCodeALL_Result == null)
					{
						continue;
					}
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
					dataRow["ItemCode"] = get_Item_ByCodeALL_Result.Item_code;
					dataRow["ItemName"] = get_Item_ByCodeALL_Result.item_Name;
					dataRow["UnitType"] = "الصغرى";
					dataRow["Quantity"] = 1;
					dataRow["AmountDisc"] = 0;
					dataRow["PercentDisc"] = 0;
					dataRow["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
					dataRow["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost;
					dataRow["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost;
					dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
					dataRow["SumPrice"] = dataRow["UnitPrice"];
					if (Convert.ToDecimal(TxtDayNum.Text) <= 0m)
					{
						dataRow["DayNum"] = 1;
						TxtDayNum.Text = "1";
					}
					else
					{
						dataRow["DayNum"] = TxtDayNum.Text;
					}
					if (!LoginDetails.IsVat)
					{
						dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
						dataRow["Total"] = dataRow["UnitPrice"];
						dataRow["OrginalPrice"] = dataRow["Total"];
					}
					else if (LoginDetails.ItemTotalWithVat)
					{
						decimal num = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
						dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall / (decimal?)num);
						dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
						dataRow["OrginalPrice"] = dataRow["Total"];
						dataRow["Tax_Value"] = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value);
						dataRow["IsItemTax"] = get_Item_ByCodeALL_Result.Is_Tax;
						dataRow["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["Total"]) / num);
						dataRow["UnitPrice"] = dataRow["SumPrice"];
						dataRow["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["Total"]) - Convert.ToDecimal(dataRow["SumPrice"]));
						dataRow["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["ItemVat"]));
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["Total"]));
						dataRow["UnitPriceDisc"] = dataRow["UnitPrice"];
						dataRow["SumPriceDisc"] = dataRow["SumPrice"];
					}
					else
					{
						dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
						dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
						dataRow["OrginalPrice"] = dataRow["Total"];
						dataRow["SumPrice"] = dataRow["UnitPrice"];
						DataRow dataRow2 = dataRow;
						decimal value = Convert.ToDecimal(dataRow["SumPrice"]);
						decimal? num2 = get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100;
						dataRow2["ItemVat"] = FUNC_DecimalNum((decimal?)value * num2);
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["SumPrice"]) + Convert.ToDecimal(dataRow["ItemVat"]));
						dataRow["IsItemTax"] = 1;
						dataRow["Tax_Value"] = get_Item_ByCodeALL_Result.Tax_Value;
					}
					if (get_Item_ByCodeALL_Result.MediumBarCode1 == text || get_Item_ByCodeALL_Result.MediumBarCode2 == text || get_Item_ByCodeALL_Result.MediumBarCode3 == text)
					{
						dataRow["UnitType"] = "المتوسطة";
						dataRow["UnitName"] = get_Item_ByCodeALL_Result.UnitMedium;
						dataRow["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit;
						dataRow["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit;
						dataRow["UnitPrice"] = get_Item_ByCodeALL_Result.SellPriceMedium;
						dataRow["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value2 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium / (decimal?)value2);
							dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
							dataRow["OrginalPrice"] = dataRow["Total"];
						}
						else
						{
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
							dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
						}
					}
					if (get_Item_ByCodeALL_Result.BigBarCode1 == text || get_Item_ByCodeALL_Result.BigBarCode2 == text || get_Item_ByCodeALL_Result.BigBarCode3 == text)
					{
						dataRow["UnitType"] = "الكبرى";
						dataRow["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitLargeID;
						dataRow["UnitName"] = get_Item_ByCodeALL_Result.UnitLarge;
						dataRow["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit * get_Item_ByCodeALL_Result.ConvertBigUnit;
						dataRow["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit * get_Item_ByCodeALL_Result.ConvertBigUnit;
						dataRow["UnitPrice"] = get_Item_ByCodeALL_Result.SellpriceLarge;
						dataRow["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge / (decimal?)value3);
							dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
							dataRow["OrginalPrice"] = dataRow["Total"];
						}
						else
						{
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
							dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
						}
					}
					List<Account_Stores> stores = Contract.Get_Stores();
					Account_Stores account_Stores = stores.FirstOrDefault((Account_Stores store) => store.ID == LoginDetails.StoreID);
					if (account_Stores != null)
					{
						dataRow["StoreName"] = account_Stores.Store_Name;
						dataRow["StoreID"] = Contract.Get_StoresIDByName(dataRow["StoreName"].ToString());
					}
					else
					{
						dataRow["StoreName"] = string.Empty;
					}
					dataRow["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["Total"]));
					datatable.Rows.Add(dataRow);
				}
				CheckFfistRow();
				InsertNewRow();
				ArrangeDatatable();
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
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
		}
		catch (Exception)
		{
		}
	}

	private void NewFrm()
	{
		datatable.Rows.Clear();
		gridControl1.Enabled = true;
		gridView1.OptionsBehavior.Editable = true;
		Clear();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		InsertNewRow();
		TxtCode.Text = string.Empty;
		IsEnableTrue();
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtSuppliers.Focus();
	}

	public void IsEnableTrue()
	{
		TxtDiscount.ReadOnly = false;
		TxtRentalDate.ReadOnly = false;
		TxtDayNum.ReadOnly = false;
		TxtDateReceipt.ReadOnly = false;
		TxtAddress.ReadOnly = false;
		TxtSuppIDNum.ReadOnly = false;
		TxtParentName.ReadOnly = false;
		TxtType.Enabled = true;
		TxtSuppliers.ReadOnly = false;
		TxtPhone.ReadOnly = false;
		TxtNote.ReadOnly = false;
		TxtDiscount.ReadOnly = false;
		TxtAllDiscount.ReadOnly = false;
		txtdicoutprege.ReadOnly = false;
		CBCostCenter.Enabled = true;
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = true;
		TxtRentalDate.ReadOnly = true;
		TxtDayNum.ReadOnly = true;
		TxtDateReceipt.ReadOnly = true;
		TxtAddress.ReadOnly = true;
		TxtSuppIDNum.ReadOnly = true;
		TxtParentName.ReadOnly = true;
		TxtType.Enabled = false;
		TxtSuppliers.ReadOnly = true;
		TxtPhone.ReadOnly = true;
		TxtNote.ReadOnly = true;
		TxtDiscount.ReadOnly = true;
		TxtAllDiscount.ReadOnly = true;
		txtdicoutprege.ReadOnly = true;
		CBCostCenter.Enabled = false;
		TxtDiscount.BackColor = Color.White;
		TxtRentalDate.BackColor = Color.White;
		TxtDayNum.BackColor = Color.White;
		TxtDateReceipt.BackColor = Color.White;
		TxtNote.BackColor = Color.White;
		TxtType.BackColor = Color.White;
		TxtSuppliers.BackColor = Color.White;
		TxtAddress.BackColor = Color.White;
		TxtParentName.BackColor = Color.White;
		TxtSuppIDNum.BackColor = Color.White;
		TxtPhone.BackColor = Color.White;
	}

	public decimal IsItemPriceZero()
	{
		decimal num = 1m;
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["ItemCode"]);
			if (!(text != string.Empty))
			{
				continue;
			}
			int num2 = 0;
			num2 = Convert.ToInt32(row["ItemID"]);
			int itmIDType = ORD.GetItmIDType(num2);
			if (itmIDType != 2)
			{
				string value = Convert.ToString(row["UnitPrice"]);
				string text2 = Convert.ToString(row["ItemName"]);
				if (string.IsNullOrWhiteSpace(value))
				{
					row["UnitPrice"] = 0;
				}
				decimal num3 = Convert.ToDecimal(row["Quantity"]);
				string type = Convert.ToString(row["UnitType"]);
				int storeID = Convert.ToInt32(row["StoreID"]);
				int itemID = Convert.ToInt32(ORD.GetItmID(text));
				num = ORD.GetQuantityPermission(itemID, storeID, type);
				if (LoginDetails.IsQuantityMinZero)
				{
					num = 1m;
					break;
				}
				if (num <= 0m)
				{
					MessageBox.Show("نعتذر، لا يوجد كمية كافية من الصنف ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					num = 0m;
					break;
				}
				if (num3 > num)
				{
					MessageBox.Show("نعتذر، الكمية المطلوبة اكبر من الكمية الموجودة  ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					num = 0m;
					break;
				}
			}
		}
		return num;
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 111))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_RentalInvoice", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
				sqlConnection.Close();
				if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintRent2.mrt");
					stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
					stiReport["Address"] = LoginDetails.Address;
					stiReport["Phone"] = LoginDetails.Phone;
					stiReport["Bank"] = LoginDetails.CompanyBank;
					stiReport["Tafqit"] = words;
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
				MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private decimal SafeConvertToDecimal(object value)
	{
		return (value == DBNull.Value || value == null) ? 0m : Convert.ToDecimal(value);
	}

	public bool FilterDataTableSave(DataTable ItemOrder)
	{
		int num = 1;
		foreach (DataRow row in ItemOrder.Rows)
		{
			if (row["ItemCode"].ToString() != string.Empty)
			{
				decimal num2 = SafeConvertToDecimal(row["Net"]);
				if (num2 == 0m)
				{
					MessageBox.Show("لا يمكن حفظ الفاتورة، يوجد معدات بصافي قيمة = 0", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					return false;
				}
			}
		}
		return true;
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 111))
		{
			int num = 0;
			if (datatable.Rows.Count > 1)
			{
				if (!FilterDataTableSave(datatable))
				{
					return;
				}
				if (TxtSuppliers.Text != "")
				{
					if (TxtType.Text != string.Empty)
					{
						if (Convert.ToInt32(TxtType.SelectedValue) == 2 && string.IsNullOrWhiteSpace(LblSup.Text))
						{
							MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع حساب عميل نقدى", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							TxtSuppliers.Focus();
							return;
						}
						decimal num2 = IsItemPriceZero();
						if (num2 == 0m)
						{
							return;
						}
						if (Convert.ToInt32(TxtType.SelectedValue) == 1)
						{
							LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
							FrmMoney frmMoney = new FrmMoney();
							frmMoney.ShowDialog();
							if (!frmMoney.IsComplateMony)
							{
								return;
							}
							FrmCash = Convert.ToDecimal(frmMoney.TxtCash.Text);
							FrmBank = Convert.ToDecimal(frmMoney.TxtVisa.Text);
							Paid = FrmCash + FrmBank;
							Rest = 0m;
						}
						else
						{
							LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
							FrmMoneyAjil frmMoneyAjil = new FrmMoneyAjil();
							frmMoneyAjil.TxtCash.Text = "0.00";
							frmMoneyAjil.TxtVisa.Text = "0.00";
							frmMoneyAjil.ShowDialog();
							if (!frmMoneyAjil.IsComplateMony)
							{
								return;
							}
							FrmCash = Convert.ToDecimal(frmMoneyAjil.TxtCash.Text);
							FrmBank = Convert.ToDecimal(frmMoneyAjil.TxtVisa.Text);
							Paid = FrmCash + FrmBank;
							Rest = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
						}
						if (CBCostCenter.SelectedValue == null)
						{
							CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
						}
						num = Rent.InsertRental(out var _, Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtSuppIDNum.Text, TxtRentalDate.DateTime, TxtDateReceipt.DateTime, TxtParentName.Text, Convert.ToInt32(CBCostCenter.SelectedValue), TxtNote.Text, TxtAddress.Text, Convert.ToDecimal(TxtDayNum.Text), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtAllDiscount.Text), Convert.ToDecimal(TxtDiscount.Text), Convert.ToDecimal(txtdicoutprege.Text), Convert.ToDecimal(TxtVatDisc.Text), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDeposit.Text), DecimalNum), IsRent: true, Convert.ToInt32(TxtType.SelectedValue), FrmCash, FrmBank, Paid, Rest);
						Rent.InsertItemsRent(datatable, num);
						gridControl1.DataSource = datatable;
						gridView1.OptionsBehavior.Editable = false;
						if (num > 0)
						{
							BtnSave.Enabled = false;
							BtnEdit.Enabled = true;
							BtnDelete.Enabled = true;
							IsEnableFalse();
							TxtCode.Text = num.ToString();
							switch (MessageBox.Show("تم حفظ بيانات فاتورة التأجير بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
							{
							case DialogResult.Yes:
								Print();
								break;
							}
							Charge(Convert.ToInt32(LblSup.Text));
						}
						else
						{
							MessageBox.Show("تم حفظ بيانات فاتورة التأجير رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						}
					}
					else
					{
						MessageBox.Show("عفوا\u064b من فضلك اختر نوع الفاتورة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						TxtType.Focus();
					}
				}
				else
				{
					MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة المبيعات بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtSuppliers.Focus();
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل صنف واحد على الاقل", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				gridControl1.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 111))
		{
			if (TxtRentalDate.ReadOnly)
			{
				IsEnableTrue();
				BtnEdit.Text = "F5 حفظ";
				InsertNewRow();
				gridView1.OptionsBehavior.Editable = true;
				return;
			}
			int num = 0;
			if (!(TxtSuppliers.Text != ""))
			{
				return;
			}
			if (TxtCode.Text != "")
			{
				int PurId = Convert.ToInt32(TxtCode.Text);
				Order_OrderReturn order_OrderReturn = db.Order_OrderReturn.Where((Order_OrderReturn x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
				Order_CheckoutOrders order_CheckoutOrders = db.Order_CheckoutOrders.Where((Order_CheckoutOrders x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
				if (order_OrderReturn == null && order_CheckoutOrders == null)
				{
					if (datatable.Rows.Count <= 1 || !FilterDataTableSave(datatable))
					{
						return;
					}
					decimal num2 = IsItemPriceZero();
					if (num2 == 0m)
					{
						return;
					}
					ArrangeDatatable();
					if (Convert.ToInt32(TxtType.SelectedValue) == 1)
					{
						LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
						FrmMoney frmMoney = new FrmMoney();
						frmMoney.ShowDialog();
						if (!frmMoney.IsComplateMony)
						{
							return;
						}
						FrmCash = Convert.ToDecimal(frmMoney.TxtCash.Text);
						FrmBank = Convert.ToDecimal(frmMoney.TxtVisa.Text);
						Paid = FrmCash + FrmBank;
						Rest = 0m;
					}
					else
					{
						LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
						FrmMoneyAjil frmMoneyAjil = new FrmMoneyAjil();
						frmMoneyAjil.ShowDialog();
						if (!frmMoneyAjil.IsComplateMony)
						{
							return;
						}
						FrmCash = Convert.ToDecimal(frmMoneyAjil.TxtCash.Text);
						FrmBank = Convert.ToDecimal(frmMoneyAjil.TxtVisa.Text);
						Paid = FrmCash + FrmBank;
						Rest = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
					}
					if (CBCostCenter.SelectedValue == null)
					{
						CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
					}
					num = Rent.UpdateRental(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtSuppIDNum.Text, TxtRentalDate.DateTime, TxtDateReceipt.DateTime, TxtParentName.Text, Convert.ToInt32(CBCostCenter.SelectedValue), TxtNote.Text, TxtAddress.Text, Convert.ToDecimal(TxtDayNum.Text), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtAllDiscount.Text), Convert.ToDecimal(TxtDiscount.Text), Convert.ToDecimal(txtdicoutprege.Text), Convert.ToDecimal(TxtVatDisc.Text), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDeposit.Text), DecimalNum), IsRent: true, Convert.ToInt32(TxtType.SelectedValue), FrmCash, FrmBank, Paid, Rest);
					Rent.deleteItemsRentalByPurcahesId(Convert.ToInt32(TxtCode.Text));
					Rent.InsertItemsRent(datatable, num);
					gridControl1.DataSource = datatable;
					gridView1.OptionsBehavior.Editable = false;
					if (num > 0)
					{
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						IsEnableFalse();
						BtnEdit.Text = "تعديل F6";
						gridView1.OptionsBehavior.Editable = false;
						switch (MessageBox.Show("تم تعديل فاتورة التاجير بنجاح \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
						{
						}
						Charge(Convert.ToInt32(LblSup.Text));
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات فاتورة التاجير رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else
				{
					MessageBox.Show("عفوا\u064b لا يمكن التعديل على فاتورة التاجير  ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 111))
		{
			int num = 0;
			if (TxtCode.Text != "")
			{
				int num2 = Convert.ToInt32(TxtCode.Text);
				switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة التأجير رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = Rent.DeleteRental(Convert.ToInt32(TxtCode.Text));
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات فاتورة التأجير رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						gridView1.OptionsBehavior.Editable = true;
						IsEnableTrue();
						InsertNewRow();
						TxtSuppliers.Focus();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات فاتورة التأجير رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					break;
				default:
					MessageBox.Show("عفوا\u064b لا يمكن التعديل على فاتورة المبيعات ليها حركات مرتجعات او صرف ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة التأجير بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Tafqit()
	{
		try
		{
			decimal num = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
			int num2 = (int)Math.Floor(num);
			int num3 = (int)((num - (decimal)num2) * 100m);
			if (LoginDetails.Lang == "ar-EG")
			{
				words = num2.ToWords(new CultureInfo("ar")) + ((num3 > 0) ? (" فاصلة " + num3.ToWords(new CultureInfo("ar"))) : "") + " ريال سعودي";
			}
			else
			{
				words = num2.ToWords(new CultureInfo("en")) + ((num3 > 0) ? (" point " + num3.ToWords(new CultureInfo("en"))) : "Saudi riyal") + "Saudi riyal";
			}
			TxtTafqit.Text = words;
		}
		catch (Exception)
		{
		}
	}

	private void Charge(int ID)
	{
		try
		{
			GTSdbEntities gTSdbEntities = new GTSdbEntities();
			List<decimal?> list = gTSdbEntities.GetAccountBalance(ID).ToList();
			foreach (decimal? item in list)
			{
				TxtCharge.Text = FUNC_DecimalNum(item.HasValue ? item.Value : 0m).ToString();
			}
		}
		catch (Exception)
		{
		}
	}

	public void GetPricePffer()
	{
		try
		{
			FrmSearchPriceShowRent frmSearchPriceShowRent = new FrmSearchPriceShowRent();
			frmSearchPriceShowRent.ShowDialog();
			if (frmSearchPriceShowRent.CustCode > 0)
			{
				int custCode = frmSearchPriceShowRent.CustCode;
				Orders_PriceShowRental orders_PriceShowRental = PSR.GetPurchasesRental(custCode).FirstOrDefault();
				CBCostCenter.SelectedValue = orders_PriceShowRental.CostCentersID;
				TxtCode.Text = orders_PriceShowRental.PurBranchID.ToString();
				LblSup.Text = orders_PriceShowRental.SupplierID.ToString();
				TxtSuppliers.Text = orders_PriceShowRental.SupplierName;
				TxtRentalDate.DateTime = Convert.ToDateTime(orders_PriceShowRental.RentalDate);
				TxtDateReceipt.DateTime = Convert.ToDateTime(orders_PriceShowRental.ReturnDate);
				TxtParentName.Text = orders_PriceShowRental.ParentName;
				TxtPhone.Text = orders_PriceShowRental.SupplierPhone;
				TxtNote.Text = orders_PriceShowRental.Note;
				TxtDeposit.Text = orders_PriceShowRental.deposit.ToString();
				TxtAddress.Text = orders_PriceShowRental.Address;
				TxtSuppIDNum.Text = orders_PriceShowRental.SupplierIDNum;
				TxtTotals.Text = Convert.ToString(orders_PriceShowRental.TotalPrices);
				TxtVat.Text = Convert.ToString(orders_PriceShowRental.Tax);
				TxtTotalDiscount.Text = Convert.ToString(orders_PriceShowRental.TotalPrices_Discount);
				TxtDiscount.Text = orders_PriceShowRental.DiscountNum.ToString();
				txtdicoutprege.Text = orders_PriceShowRental.DiscountPerantage.ToString();
				TxtAllDiscount.Text = orders_PriceShowRental.AllDiscount.ToString();
				TxtVatDisc.Text = orders_PriceShowRental.Tax_Discount.ToString();
				TxtNet.Text = orders_PriceShowRental.Net.ToString();
				TxtType.Text = ORD.GetAccountNameById(Convert.ToInt32(orders_PriceShowRental.Order_Paymant_Type));
				Charge(Convert.ToInt32(LblSup.Text));
				TxtSuppliers.Focus();
				datatable.Rows.Clear();
				List<Ordert_PriceShowRentalDetails> purchasesRentalDetails = PSR.GetPurchasesRentalDetails(custCode);
				int num = 1;
				foreach (Ordert_PriceShowRentalDetails item in purchasesRentalDetails)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = ORD.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
					dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["DayNum"] = FUNC_DecimalNum(Convert.ToDecimal(item.DaysNum));
					dataRow["ItemCode"] = item_Items.Item_code;
					dataRow["ItemName"] = item_Items.item_Name;
					if (item.ItemUnitType == "الصغرى")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "المتوسطة")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "الكبرى")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					dataRow["UnitType"] = item.ItemUnitType;
					dataRow["StoreID"] = item.StoreID;
					dataRow["StoreName"] = (from x in db.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["Quantity"] = FUNC_DecimalNum(Convert.ToDecimal(item.Quantity));
					dataRow["AvergItem"] = item.LastCost;
					dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
					dataRow["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.UnitPrice));
					dataRow["UnitPriceDisc"] = Math.Round(Convert.ToDecimal(item.NetUnitPrice), DecimalNum);
					dataRow["PercentDisc"] = Math.Round(Convert.ToDecimal(item.PercentDisc), DecimalNum);
					dataRow["AmountDisc"] = Math.Round(Convert.ToDecimal(item.AmountDisc), DecimalNum);
					dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(item.SumPrice), DecimalNum);
					dataRow["Total"] = Math.Round(Convert.ToDecimal(item.Total), DecimalNum);
					dataRow["SumPriceDisc"] = Math.Round(Convert.ToDecimal(item.SumPriceDisc), DecimalNum);
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice));
						dataRow["IsItemTax"] = 1;
						dataRow["Tax_Value"] = item_Items.Tax_Value;
					}
					else
					{
						dataRow["IsItemTax"] = 0;
						dataRow["Tax_Value"] = 0;
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice));
					}
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				InsertNewRow();
				int num2 = 1;
				foreach (DataRow row in datatable.Rows)
				{
					row["SN"] = num2;
					num2++;
					SN = Convert.ToInt32(row["SN"]);
				}
			}
			else
			{
				TxtSuppliers.Focus();
			}
		}
		catch (Exception)
		{
			Clear();
		}
		Tafqit();
	}

	private void repositoryItemButtonEdit7_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ItemCode"));
		string text2 = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SN"));
		if (text != string.Empty)
		{
			LoginDetails.ItemcodeForUnits = text;
			FrmSearchUnit frmSearchUnit = new FrmSearchUnit();
			frmSearchUnit.ShowDialog();
			if (frmSearchUnit.ItemCode != string.Empty)
			{
				string itemCode = frmSearchUnit.ItemCode;
				if (frmSearchUnit.unitName == "الصغرى")
				{
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(itemCode).FirstOrDefault();
					if (get_Item_ByCodeALL_Result != null)
					{
						int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row in datatable.Rows)
						{
							if (row["SN"].ToString() == Convert.ToString(text2))
							{
								row["UnitType"] = "الصغرى";
								row["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
								row["Quantity"] = 1;
								row["SumAvergItem"] = Math.Round(Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]), DecimalNumQty);
								if (LoginDetails.ItemTotalWithVat)
								{
									decimal value = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall / (decimal?)value);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
								}
								else
								{
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
								}
							}
						}
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "المتوسطة")
				{
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result2 = ITM.GetItemByCode(itemCode).FirstOrDefault();
					if (get_Item_ByCodeALL_Result2 != null)
					{
						int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row2 in datatable.Rows)
						{
							if (row2["SN"].ToString() == Convert.ToString(text2))
							{
								row2["Quantity"] = 1;
								decimal num3 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.ConvertMediumUnit);
								row2["SumAvergItem"] = Math.Round(Convert.ToDecimal(row2["AvergItem"]) * (Convert.ToDecimal(row2["Quantity"]) * num3), DecimalNumQty);
								row2["UnitType"] = "المتوسطة";
								row2["UnitName"] = get_Item_ByCodeALL_Result2.UnitMedium;
								row2["AvergItem"] = get_Item_ByCodeALL_Result2.Average_cost * get_Item_ByCodeALL_Result2.ConvertMediumUnit;
								row2["SumAvergItem"] = get_Item_ByCodeALL_Result2.Average_cost * get_Item_ByCodeALL_Result2.ConvertMediumUnit;
								row2["UnitPrice"] = get_Item_ByCodeALL_Result2.SellPriceMedium;
								row2["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium);
								if (LoginDetails.ItemTotalWithVat)
								{
									decimal value2 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.Tax_Value) / 100m + 1m;
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium / (decimal?)value2);
									row2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
								}
								else
								{
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium);
									row2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
								}
							}
						}
					}
				}
				if (frmSearchUnit.unitName == "الكبرى")
				{
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result3 = ITM.GetItemByCode(itemCode).FirstOrDefault();
					if (get_Item_ByCodeALL_Result3 != null)
					{
						int num4 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row3 in datatable.Rows)
						{
							if (row3["SN"].ToString() == Convert.ToString(text2))
							{
								row3["Quantity"] = 1;
								row3["UnitType"] = "الكبرى";
								row3["UnitName"] = get_Item_ByCodeALL_Result3.UnitLarge;
								row3["AvergItem"] = get_Item_ByCodeALL_Result3.Average_cost * get_Item_ByCodeALL_Result3.ConvertMediumUnit * get_Item_ByCodeALL_Result3.ConvertBigUnit;
								row3["SumAvergItem"] = get_Item_ByCodeALL_Result3.Average_cost * get_Item_ByCodeALL_Result3.ConvertMediumUnit * get_Item_ByCodeALL_Result3.ConvertBigUnit;
								if (LoginDetails.ItemTotalWithVat)
								{
									decimal value3 = Convert.ToDecimal(get_Item_ByCodeALL_Result3.Tax_Value) / 100m + 1m;
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge / (decimal?)value3);
									row3["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
								}
								else
								{
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge);
									row3["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
								}
							}
						}
						frmSearchUnit.Close();
					}
				}
				frmSearchUnit.Close();
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[4];
			gridView1.ShowEditor();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			frmSearchUnit.Close();
		}
		else
		{
			MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف الوحدات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void repositoryItemButtonEdit2_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["SN"].ToString() == Convert.ToString(num))
			{
				if (row["ItemCode"].ToString() != "")
				{
					row.Delete();
					datatable.AcceptChanges();
					gridView1.FocusedColumn = gridView1.VisibleColumns[0];
					gridView1.ShowEditor();
				}
				else
				{
					gridView1.FocusedColumn = gridView1.VisibleColumns[0];
					gridView1.ShowEditor();
				}
				break;
			}
		}
		ArrangeDatatable();
		gridControl1.DataSource = datatable;
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
	}

	private void repositoryItemButtonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string Itemocde = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ItemCode"));
		if (Itemocde != string.Empty)
		{
			using (GTSdbEntities gTSdbEntities = new GTSdbEntities())
			{
				Item_Items item_Items = (from x in ITM.GetItemByCode()
					where x.Item_code == Itemocde
					select x).FirstOrDefault();
				LoginDetails.ItemcodeForStore = item_Items.ItemId;
				FrmSearchTransfer frmSearchTransfer = new FrmSearchTransfer();
				frmSearchTransfer.ShowDialog();
				if (frmSearchTransfer.ItemIdStore > 0)
				{
					int StoreID = LoginDetails.StoreID;
					if (StoreID == 0)
					{
						StoreID = 1;
					}
					int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
					string value = (from x in gTSdbEntities.Account_Stores
						where x.ID == StoreID
						select x.Store_Name).FirstOrDefault();
					foreach (DataRow row in datatable.Rows)
					{
						if (row["SN"].ToString() == Convert.ToString(num))
						{
							row["StoreName"] = value;
							break;
						}
					}
					gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[5];
					gridView1.ShowEditor();
					frmSearchTransfer.Close();
				}
				return;
			}
		}
		MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف المخازن", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}

	private void repositoryItemButtonEditCodeSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchItems();
	}

	private void repositoryItemButtonEdit4_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int itemID = 0;
		int storeID = 0;
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToInt32(row["SN"]) == SN)
			{
				itemID = Convert.ToInt32(row["ItemID"]);
				storeID = Convert.ToInt32(row["StoreID"]);
			}
		}
		FrmItemRentDeatils frmItemRentDeatils = new FrmItemRentDeatils(itemID, LoginDetails.BranchID, storeID);
		frmItemRentDeatils.ShowDialog();
	}

	private void FrmRentalInvoice_Load(object sender, EventArgs e)
	{
		try
		{
			TxtType.SelectedIndex = 1;
			base.KeyPreview = true;
			gridControl1.ForceInitialize();
			if (LoginDetails.Lang == "ar-EG")
			{
				gridControl1.MainView.RestoreLayoutFromXml(fileNameAR);
			}
			else
			{
				gridControl1.MainView.RestoreLayoutFromXml(fileNameEN);
			}
		}
		catch (Exception)
		{
		}
	}

	private void FrmRentalInvoice_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameAR);
		}
		else
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameEN);
		}
	}

	private void TxtRentalDate_DateTimeChanged(object sender, EventArgs e)
	{
		TimeSpan timeSpan = TxtDateReceipt.DateTime - TxtRentalDate.DateTime;
		int num = timeSpan.Days;
		if (timeSpan.Hours >= 1)
		{
			num++;
		}
		TxtDayNum.Text = num.ToString();
	}

	private void TxtDateReceipt_DateTimeChanged(object sender, EventArgs e)
	{
		TimeSpan timeSpan = TxtDateReceipt.DateTime - TxtRentalDate.DateTime;
		int num = timeSpan.Days;
		if (timeSpan.Hours >= 1)
		{
			num++;
		}
		TxtDayNum.Text = num.ToString();
	}

	private void TxtDayNum_EditValueChanged(object sender, EventArgs e)
	{
		if (int.TryParse(TxtDayNum.Text, out var result))
		{
			if (DateTime.TryParse(TxtRentalDate.Text, out var result2))
			{
				DateTime dateTime = result2.AddDays(result);
				TxtDateReceipt.Text = dateTime.ToString("yyyy-MM-dd");
			}
			else
			{
				MessageBox.Show("تأكد من صحة تاريخ التاجير", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		else
		{
			TxtDateReceipt.DateTime = TxtRentalDate.DateTime;
		}
		foreach (DataRow row in datatable.Rows)
		{
			row["DayNum"] = result;
		}
		int rowHandle = 0;
		GridColumn column = gridView1.Columns["DayNum"];
		CellValueChangedEventArgs e2 = new CellValueChangedEventArgs(rowHandle, column, result);
		UpdateItemRow("DayNum", e2);
		gridControl1.DataSource = datatable;
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
	}

	private void TxtNet_TextChanged(object sender, EventArgs e)
	{
		Tafqit();
	}

	private void ZeroDisc()
	{
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		Cal_FinalResultTotals(0m, "DiscAmount");
	}

	private void TxtDiscount_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (TxtDiscount.Text != string.Empty && Convert.ToDecimal(TxtDiscount.Text) != 0m && Convert.ToDecimal(TxtDiscount.Text) < Convert.ToDecimal(TxtTotals.Text))
			{
				decimal per = Convert.ToDecimal(TxtDiscount.Text);
				Cal_FinalResultTotals(per, "DiscAmount");
			}
			else
			{
				ZeroDisc();
			}
		}
		catch (Exception)
		{
			ZeroDisc();
		}
	}

	private void txtdicoutprege_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (!decimal.TryParse(txtdicoutprege.Text, out var result))
			{
				result = 0m;
			}
			if (txtdicoutprege.Text != string.Empty && Convert.ToDecimal(txtdicoutprege.Text) != 0m && Convert.ToDecimal(txtdicoutprege.Text) <= 100m)
			{
				Cal_FinalResultTotals(result, "Percent");
			}
			else
			{
				ZeroDisc();
			}
		}
		catch (Exception)
		{
			ZeroDisc();
		}
	}

	private void TxtAllDiscount_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (TxtAllDiscount.Text != string.Empty && Convert.ToDecimal(TxtAllDiscount.Text) != 0m && Convert.ToDecimal(TxtAllDiscount.Text) < Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				decimal per = Convert.ToDecimal(TxtAllDiscount.Text);
				Cal_FinalResultTotals(per, "AllDiscount");
				gridControl1.DataSource = datatable;
			}
			else
			{
				ZeroDisc();
			}
		}
		catch (Exception)
		{
			ZeroDisc();
		}
	}

	private void TxtDiscount_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void txtdicoutprege_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private decimal FUNC_DecimalNum(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNum);
	}

	private void MenuNew_Click(object sender, EventArgs e)
	{
		NewFrm();
	}

	private void MenuSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void MenuEdit_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void MenuDelete_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void MenuPreviewFatoura_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void MenuPrintFatoura_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void MenuNewItem_Click(object sender, EventArgs e)
	{
	}

	private void TxtDayNum_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void TxtCharge_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void FrmRentalInvoice_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F3")
		{
			if (gridView1.OptionsBehavior.Editable)
			{
				LoginDetails.ItemSearch = CodeItm;
				SearchItems();
			}
			return;
		}
		if (e.KeyCode.ToString() == "F4")
		{
			NewFrm();
			return;
		}
		if (e.KeyCode.ToString() == "F5")
		{
			if (BtnSave.Enabled)
			{
				Save();
			}
			return;
		}
		if (e.KeyCode.ToString() == "F6")
		{
			if (BtnEdit.Enabled)
			{
				Edit();
			}
			return;
		}
		if (e.KeyCode.ToString() == "F7")
		{
			if (BtnDelete.Enabled)
			{
				Delete();
			}
			return;
		}
		if (e.KeyCode.ToString() == "F8")
		{
			if (LoginDetails.Active_Virgin == 9)
			{
				new FrmReportReser().Show();
			}
			return;
		}
		if (e.KeyCode.ToString() == "F9")
		{
			GetPricePffer();
			return;
		}
		if (e.KeyCode.ToString() == "F11")
		{
			string custName = TxtSuppliers.Text;
			try
			{
				int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				foreach (DataRow row in datatable.Rows)
				{
					if (row["SN"].ToString() == Convert.ToString(num))
					{
						decimal currentQuantity = Convert.ToDecimal(row["Quantity"]);
						decimal unitPrice = Convert.ToDecimal(row["UnitPriceDisc"]);
						int num2 = Convert.ToInt32(row["ItemID"]);
						if (num2 > 0)
						{
							LoginDetails.ItemcodeForStore = num2;
							FrmSearchTransferAllBranch frmSearchTransferAllBranch = new FrmSearchTransferAllBranch(num2, StorId, currentQuantity, unitPrice, UnitName, custName)
							{
								Owner = this
							};
							frmSearchTransferAllBranch.ShowDialog();
							frmSearchTransferAllBranch.Close();
							LoginDetails.ItemcodeForStore = 0;
							break;
						}
					}
				}
				return;
			}
			catch (Exception)
			{
				return;
			}
		}
		if (e.KeyCode == Keys.P && e.Modifiers == Keys.Control)
		{
			if (BtnDelete.Enabled)
			{
				Print();
			}
		}
		else if (e.KeyCode == Keys.L && e.Modifiers == Keys.Control)
		{
			new FrmLock().ShowDialog();
		}
		else if (e.KeyCode == Keys.M && e.Modifiers == Keys.Control)
		{
			new FrmCustomer().ShowDialog();
		}
		else if (e.KeyCode == Keys.R && e.Modifiers == Keys.Control)
		{
			new FrmReservation().Show();
		}
	}

	private void BtnNewCustomer_Click(object sender, EventArgs e)
	{
		new FrmCustomer().Show();
	}

	private void MenuSearchItem_Click(object sender, EventArgs e)
	{
		SearchItems();
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void MenuPrintQued_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text != "")
		{
			Class_Print_DailyRestrictionsALL class_Print_DailyRestrictionsALL = new Class_Print_DailyRestrictionsALL();
			class_Print_DailyRestrictionsALL.Print_DailyRestrictionsALL(Convert.ToInt32(TxtCode.Text), 5);
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.OrderRent.FrmRentalInvoice));
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions9 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled9 = new DevExpress.Utils.SerializableAppearanceObject();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtSuppIDNum = new DevExpress.XtraEditors.TextEdit();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtAddress = new DevExpress.XtraEditors.TextEdit();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtRentalDate = new DevExpress.XtraEditors.DateEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtParentName = new DevExpress.XtraEditors.TextEdit();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.RecoveredMenuItemFix1 = new System.Windows.Forms.ToolStripMenuItem();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label37 = new System.Windows.Forms.Label();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.label14 = new System.Windows.Forms.Label();
		this.TxtDayNum = new DevExpress.XtraEditors.TextEdit();
		this.TxtDateReceipt = new DevExpress.XtraEditors.DateEdit();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.label20 = new System.Windows.Forms.Label();
		this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.RecoveredMenuItemFix2 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix3 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix4 = new System.Windows.Forms.ToolStripMenuItem();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtDeposit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtLocation = new DevExpress.XtraEditors.TextEdit();
		this.label25 = new System.Windows.Forms.Label();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnShowPrice = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label8 = new System.Windows.Forms.Label();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtAllDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotalDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix5 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintQued = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPreviewFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuNewItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix6 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPurchOrder = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix7 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix8 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix9 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix10 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.RecoveredMenuItemFix11 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
		this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.BtnNewCustomer = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppIDNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtParentName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDayNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		base.SuspendLayout();
		this.repositoryItemButtonEditCodeSearch.AutoHeight = false;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions10.Image");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEditCodeSearch.Name = "repositoryItemButtonEditCodeSearch";
		this.repositoryItemButtonEditCodeSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEditCodeSearch_ButtonClick);
		this.repositoryItemButtonEdit7.AutoHeight = false;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions11.Image");
		this.repositoryItemButtonEdit7.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit7.Name = "repositoryItemButtonEdit7";
		this.repositoryItemButtonEdit7.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit7_ButtonClick);
		this.repositoryItemButtonEdit5.AutoHeight = false;
		editorButtonImageOptions3.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions12.Image");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEdit5.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit6.AutoHeight = false;
		editorButtonImageOptions4.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions13.Image");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "حذف", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
		this.repositoryItemButtonEdit4.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit4.AutoHeight = false;
		editorButtonImageOptions5.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions14.Image");
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemButtonEdit4.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit4.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit4_ButtonPressed);
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label11.Location = new System.Drawing.Point(721, 155);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(73, 21);
		this.label11.TabIndex = 134;
		this.label11.Text = "ملاحظات";
		this.TxtNote.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNote.Location = new System.Drawing.Point(6, 152);
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNote.Size = new System.Drawing.Size(709, 28);
		this.TxtNote.TabIndex = 133;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label10.Location = new System.Drawing.Point(627, 25);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(81, 21);
		this.label10.TabIndex = 132;
		this.label10.Text = "رقم الهوية";
		this.TxtSuppIDNum.Location = new System.Drawing.Point(341, 22);
		this.TxtSuppIDNum.Name = "TxtSuppIDNum";
		this.TxtSuppIDNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSuppIDNum.Properties.Appearance.Options.UseFont = true;
		this.TxtSuppIDNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSuppIDNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSuppIDNum.Size = new System.Drawing.Size(280, 28);
		this.TxtSuppIDNum.TabIndex = 131;
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label9.Location = new System.Drawing.Point(1089, 126);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(56, 21);
		this.label9.TabIndex = 130;
		this.label9.Text = "العنوان";
		this.TxtAddress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtAddress.Location = new System.Drawing.Point(728, 124);
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtAddress.Properties.Appearance.Options.UseFont = true;
		this.TxtAddress.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAddress.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAddress.Size = new System.Drawing.Size(340, 28);
		this.TxtAddress.TabIndex = 129;
		this.LBlSuppAccount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LBlSuppAccount.AutoSize = true;
		this.LBlSuppAccount.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LBlSuppAccount.Location = new System.Drawing.Point(1169, 29);
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		this.LBlSuppAccount.Size = new System.Drawing.Size(19, 21);
		this.LBlSuppAccount.TabIndex = 128;
		this.LBlSuppAccount.Text = "1";
		this.LBlSuppAccount.Visible = false;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label26.Location = new System.Drawing.Point(627, 59);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(95, 21);
		this.label26.TabIndex = 127;
		this.label26.Text = "مركز التكلفة";
		this.CBCostCenter.Font = new System.Drawing.Font("Tahoma", 10f);
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Location = new System.Drawing.Point(341, 56);
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.Size = new System.Drawing.Size(280, 29);
		this.CBCostCenter.TabIndex = 8;
		this.LblSup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblSup.AutoSize = true;
		this.LblSup.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblSup.Location = new System.Drawing.Point(1169, 59);
		this.LblSup.Name = "LblSup";
		this.LblSup.Size = new System.Drawing.Size(19, 21);
		this.LblSup.TabIndex = 125;
		this.LblSup.Text = "1";
		this.TxtRentalDate.EditValue = null;
		this.TxtRentalDate.Location = new System.Drawing.Point(6, 56);
		this.TxtRentalDate.Name = "TxtRentalDate";
		this.TxtRentalDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtRentalDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtRentalDate.Properties.Appearance.Options.UseFont = true;
		this.TxtRentalDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtRentalDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtRentalDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtRentalDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtRentalDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtRentalDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtRentalDate.Size = new System.Drawing.Size(214, 28);
		this.TxtRentalDate.TabIndex = 0;
		this.TxtRentalDate.DateTimeChanged += new System.EventHandler(TxtRentalDate_DateTimeChanged);
		this.label23.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label23.Location = new System.Drawing.Point(1074, 93);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(93, 21);
		this.label23.TabIndex = 117;
		this.label23.Text = "اسم الكفيل";
		this.TxtParentName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtParentName.Location = new System.Drawing.Point(728, 90);
		this.TxtParentName.Name = "TxtParentName";
		this.TxtParentName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtParentName.Properties.Appearance.Options.UseFont = true;
		this.TxtParentName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtParentName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtParentName.Size = new System.Drawing.Size(340, 28);
		this.TxtParentName.TabIndex = 6;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label22.Location = new System.Drawing.Point(627, 93);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(84, 21);
		this.label22.TabIndex = 115;
		this.label22.Text = "رقم الجوال";
		this.TxtPhone.Location = new System.Drawing.Point(341, 90);
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.Size = new System.Drawing.Size(280, 28);
		this.TxtPhone.TabIndex = 5;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label21.Location = new System.Drawing.Point(226, 56);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(92, 21);
		this.label21.TabIndex = 113;
		this.label21.Text = "تاريخ التأجير";
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label18.Location = new System.Drawing.Point(1074, 59);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(93, 21);
		this.label18.TabIndex = 107;
		this.label18.Text = "اسم العميل";
		this.repositoryItemButtonEditCode.AutoHeight = false;
		editorButtonImageOptions6.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions15.Image");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
		this.repositoryItemButtonEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.repositoryItemButtonEdit1.AutoHeight = false;
		editorButtonImageOptions7.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		editorButtonImageOptions7.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions16.Image");
		editorButtonImageOptions7.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.repositoryItemButtonEdit3.AutoHeight = false;
		editorButtonImageOptions8.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions17.Image");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit2.AutoHeight = false;
		editorButtonImageOptions9.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions18.Image");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "حذف", -1, true, true, false, editorButtonImageOptions9, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance9, appearanceHovered9, appearancePressed9, appearanceDisabled9, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.RecoveredMenuItemFix1.Name = "رقمالفاتورةToolStripMenuItem1";
		this.RecoveredMenuItemFix1.Size = new System.Drawing.Size(165, 26);
		this.RecoveredMenuItemFix1.Text = "رقم الفاتورة";
		this.TxtSuppliers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSuppliers.Location = new System.Drawing.Point(762, 56);
		this.TxtSuppliers.Name = "TxtSuppliers";
		this.TxtSuppliers.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtSuppliers.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSuppliers.Properties.Appearance.Options.UseBackColor = true;
		this.TxtSuppliers.Properties.Appearance.Options.UseFont = true;
		this.TxtSuppliers.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSuppliers.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSuppliers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtSuppliers.Size = new System.Drawing.Size(306, 28);
		this.TxtSuppliers.TabIndex = 3;
		this.TxtSuppliers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtSuppliers_ButtonClick);
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label17.Location = new System.Drawing.Point(1074, 25);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(90, 21);
		this.label17.TabIndex = 105;
		this.label17.Text = "رقم الفاتورة";
		this.TxtCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCode.Location = new System.Drawing.Point(728, 22);
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCode.Properties.Appearance.Options.UseFont = true;
		this.TxtCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCode.Properties.ReadOnly = true;
		this.TxtCode.Size = new System.Drawing.Size(340, 28);
		this.TxtCode.TabIndex = 50;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.BtnNewCustomer);
		this.groupBox1.Controls.Add(this.label37);
		this.groupBox1.Controls.Add(this.TxtCharge);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.TxtDayNum);
		this.groupBox1.Controls.Add(this.TxtDateReceipt);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.TxtSuppIDNum);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.LBlSuppAccount);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Controls.Add(this.TxtAddress);
		this.groupBox1.Controls.Add(this.CBCostCenter);
		this.groupBox1.Controls.Add(this.LblSup);
		this.groupBox1.Controls.Add(this.TxtRentalDate);
		this.groupBox1.Controls.Add(this.label23);
		this.groupBox1.Controls.Add(this.TxtParentName);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.TxtPhone);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.TxtSuppliers);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1195, 162);
		this.groupBox1.TabIndex = 6;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "بيانات فاتورة التأجير";
		this.label37.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label37.Location = new System.Drawing.Point(226, 25);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(94, 21);
		this.label37.TabIndex = 142;
		this.label37.Text = "رصيد العميل";
		this.TxtCharge.Enabled = false;
		this.TxtCharge.Location = new System.Drawing.Point(6, 22);
		this.TxtCharge.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCharge.Name = "TxtCharge";
		this.TxtCharge.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCharge.Size = new System.Drawing.Size(214, 28);
		this.TxtCharge.TabIndex = 141;
		this.TxtCharge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtCharge_KeyPress);
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label14.Location = new System.Drawing.Point(627, 126);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(76, 21);
		this.label14.TabIndex = 138;
		this.label14.Text = "عدد الايام";
		this.TxtDayNum.Location = new System.Drawing.Point(6, 124);
		this.TxtDayNum.Name = "TxtDayNum";
		this.TxtDayNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDayNum.Properties.Appearance.Options.UseFont = true;
		this.TxtDayNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDayNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDayNum.Size = new System.Drawing.Size(615, 28);
		this.TxtDayNum.TabIndex = 137;
		this.TxtDayNum.EditValueChanged += new System.EventHandler(TxtDayNum_EditValueChanged);
		this.TxtDayNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtDayNum_KeyPress);
		this.TxtDateReceipt.EditValue = null;
		this.TxtDateReceipt.Location = new System.Drawing.Point(6, 90);
		this.TxtDateReceipt.Name = "TxtDateReceipt";
		this.TxtDateReceipt.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateReceipt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateReceipt.Properties.Appearance.Options.UseFont = true;
		this.TxtDateReceipt.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateReceipt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateReceipt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateReceipt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateReceipt.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDateReceipt.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDateReceipt.Size = new System.Drawing.Size(214, 28);
		this.TxtDateReceipt.TabIndex = 135;
		this.TxtDateReceipt.DateTimeChanged += new System.EventHandler(TxtDateReceipt_DateTimeChanged);
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label13.Location = new System.Drawing.Point(226, 93);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(107, 21);
		this.label13.TabIndex = 136;
		this.label13.Text = "تاريخ الاستلام";
		this.TxtType.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Location = new System.Drawing.Point(391, 230);
		this.TxtType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtType.Name = "TxtType";
		this.TxtType.Size = new System.Drawing.Size(280, 29);
		this.TxtType.TabIndex = 139;
		this.TxtType.Visible = false;
		this.label20.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label20.Location = new System.Drawing.Point(679, 233);
		this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(43, 21);
		this.label20.TabIndex = 140;
		this.label20.Text = "النوع";
		this.label20.Visible = false;
		this.gridColumn18.Caption = "حذف الصنف";
		this.gridColumn18.Name = "gridColumn18";
		this.gridColumn18.Visible = true;
		this.gridColumn18.VisibleIndex = 8;
		this.RecoveredMenuItemFix2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix1, this.RecoveredMenuItemFix3, this.RecoveredMenuItemFix4 });
		this.RecoveredMenuItemFix2.Name = "استعلامToolStripMenuItem1";
		this.RecoveredMenuItemFix2.Size = new System.Drawing.Size(232, 24);
		this.RecoveredMenuItemFix2.Text = "استعلام";
		this.RecoveredMenuItemFix3.Name = "اسمالموردToolStripMenuItem1";
		this.RecoveredMenuItemFix3.Size = new System.Drawing.Size(165, 26);
		this.RecoveredMenuItemFix3.Text = "اسم المورد";
		this.RecoveredMenuItemFix4.Name = "نوعالفاتورةToolStripMenuItem1";
		this.RecoveredMenuItemFix4.Size = new System.Drawing.Size(165, 26);
		this.RecoveredMenuItemFix4.Text = "نوع الفاتورة";
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label12.Location = new System.Drawing.Point(529, 76);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(146, 19);
		this.label12.TabIndex = 211;
		this.label12.Text = "المبلغ المدفوع تأمين";
		this.TxtDeposit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDeposit.DecimalDigits = 2;
		this.TxtDeposit.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtDeposit.ForeColor = System.Drawing.Color.Blue;
		this.TxtDeposit.Location = new System.Drawing.Point(494, 98);
		this.TxtDeposit.Name = "TxtDeposit";
		this.TxtDeposit.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDeposit.Size = new System.Drawing.Size(221, 48);
		this.TxtDeposit.TabIndex = 210;
		this.TxtDeposit.Text = "0.00";
		this.TxtDeposit.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDeposit.UseCompatibleTextRendering = false;
		this.TxtDeposit.Value = new decimal(new int[4]);
		this.TxtDeposit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtLocation.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtLocation.EditValue = "";
		this.TxtLocation.Location = new System.Drawing.Point(494, 185);
		this.TxtLocation.Name = "TxtLocation";
		this.TxtLocation.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtLocation.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f, System.Drawing.FontStyle.Bold);
		this.TxtLocation.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
		this.TxtLocation.Properties.Appearance.Options.UseBackColor = true;
		this.TxtLocation.Properties.Appearance.Options.UseFont = true;
		this.TxtLocation.Properties.Appearance.Options.UseForeColor = true;
		this.TxtLocation.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLocation.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtLocation.Properties.ReadOnly = true;
		this.TxtLocation.Size = new System.Drawing.Size(221, 28);
		this.TxtLocation.TabIndex = 126;
		this.label25.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label25.Location = new System.Drawing.Point(721, 189);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(51, 19);
		this.label25.TabIndex = 191;
		this.label25.Text = "الموقع";
		this.txtdicoutprege.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Black;
		this.txtdicoutprege.Location = new System.Drawing.Point(6, 81);
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtdicoutprege.Size = new System.Drawing.Size(107, 27);
		this.txtdicoutprege.TabIndex = 189;
		this.txtdicoutprege.Text = "0.000";
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.txtdicoutprege.KeyDown += new System.Windows.Forms.KeyEventHandler(txtdicoutprege_KeyDown);
		this.txtdicoutprege.KeyUp += new System.Windows.Forms.KeyEventHandler(txtdicoutprege_KeyUp);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.BtnShowPrice);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.TxtDeposit);
		this.groupBox2.Controls.Add(this.TxtLocation);
		this.groupBox2.Controls.Add(this.label25);
		this.groupBox2.Controls.Add(this.txtdicoutprege);
		this.groupBox2.Controls.Add(this.TxtTafqit);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtNet);
		this.groupBox2.Controls.Add(this.TxtNote);
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.TxtVat);
		this.groupBox2.Controls.Add(this.TxtAllDiscount);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.TxtTotals);
		this.groupBox2.Controls.Add(this.TxtTotalDiscount);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.TxtDiscount);
		this.groupBox2.Controls.Add(this.TxtVatDisc);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Location = new System.Drawing.Point(12, 518);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1195, 219);
		this.groupBox2.TabIndex = 7;
		this.groupBox2.TabStop = false;
		this.BtnShowPrice.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnShowPrice.Appearance.Options.UseFont = true;
		this.BtnShowPrice.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnShowPrice.ImageOptions.Image");
		this.BtnShowPrice.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnShowPrice.Location = new System.Drawing.Point(798, 79);
		this.BtnShowPrice.Margin = new System.Windows.Forms.Padding(5);
		this.BtnShowPrice.Name = "BtnShowPrice";
		this.BtnShowPrice.Size = new System.Drawing.Size(391, 39);
		this.BtnShowPrice.TabIndex = 212;
		this.BtnShowPrice.Text = "تحميل من\r\n عرض سعرF9";
		this.BtnShowPrice.Click += new System.EventHandler(BtnShowPrice_Click);
		this.TxtTafqit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtTafqit.Location = new System.Drawing.Point(6, 185);
		this.TxtTafqit.Name = "TxtTafqit";
		this.TxtTafqit.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtTafqit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtTafqit.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
		this.TxtTafqit.Properties.Appearance.Options.UseBackColor = true;
		this.TxtTafqit.Properties.Appearance.Options.UseFont = true;
		this.TxtTafqit.Properties.Appearance.Options.UseForeColor = true;
		this.TxtTafqit.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTafqit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTafqit.Properties.ReadOnly = true;
		this.TxtTafqit.Size = new System.Drawing.Size(482, 28);
		this.TxtTafqit.TabIndex = 93;
		this.TxtNet.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Location = new System.Drawing.Point(798, 26);
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNet.Size = new System.Drawing.Size(392, 48);
		this.TxtNet.TabIndex = 92;
		this.TxtNet.Text = "0.000";
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet.TextChanged += new System.EventHandler(TxtNet_TextChanged);
		this.BtnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(798, 121);
		this.BtnEdit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Size = new System.Drawing.Size(125, 40);
		this.BtnEdit.TabIndex = 4;
		this.BtnEdit.Text = "تعديل F6";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(931, 121);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(125, 40);
		this.BtnSave.TabIndex = 3;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(931, 169);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(125, 40);
		this.BtnPrint.TabIndex = 6;
		this.BtnPrint.Text = "طباعة Ctrl P";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(1064, 121);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(125, 40);
		this.BtnNew.TabIndex = 7;
		this.BtnNew.Text = "جديد F4";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label8.Location = new System.Drawing.Point(349, 121);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(109, 19);
		this.label8.TabIndex = 171;
		this.label8.Text = "الضريبة بالخصم";
		this.BtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(1064, 169);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(125, 40);
		this.BtnDelete.TabIndex = 5;
		this.BtnDelete.Text = "حذف F7";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.TxtVat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtVat.DecimalDigits = 3;
		this.TxtVat.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtVat.ForeColor = System.Drawing.Color.Black;
		this.TxtVat.Location = new System.Drawing.Point(236, 84);
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.ReadOnly = true;
		this.TxtVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVat.Size = new System.Drawing.Size(107, 27);
		this.TxtVat.TabIndex = 170;
		this.TxtVat.Text = "0.000";
		this.TxtVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4]);
		this.TxtVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtAllDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtAllDiscount.DecimalDigits = 3;
		this.TxtAllDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtAllDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtAllDiscount.Location = new System.Drawing.Point(236, 53);
		this.TxtAllDiscount.Name = "TxtAllDiscount";
		this.TxtAllDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtAllDiscount.Size = new System.Drawing.Size(107, 27);
		this.TxtAllDiscount.TabIndex = 169;
		this.TxtAllDiscount.Text = "0.000";
		this.TxtAllDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllDiscount.Value = new decimal(new int[4]);
		this.TxtAllDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtAllDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtAllDiscount_KeyUp);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(798, 169);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(125, 40);
		this.BtnExit.TabIndex = 8;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.TxtTotals.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Location = new System.Drawing.Point(6, 13);
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.ReadOnly = true;
		this.TxtTotals.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotals.Size = new System.Drawing.Size(107, 27);
		this.TxtTotals.TabIndex = 161;
		this.TxtTotals.Text = "0.000";
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTotalDiscount.DecimalDigits = 3;
		this.TxtTotalDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotalDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalDiscount.Location = new System.Drawing.Point(6, 115);
		this.TxtTotalDiscount.Name = "TxtTotalDiscount";
		this.TxtTotalDiscount.ReadOnly = true;
		this.TxtTotalDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotalDiscount.Size = new System.Drawing.Size(107, 27);
		this.TxtTotalDiscount.TabIndex = 159;
		this.TxtTotalDiscount.Text = "0.000";
		this.TxtTotalDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalDiscount.Value = new decimal(new int[4]);
		this.TxtTotalDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label1.Location = new System.Drawing.Point(119, 87);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(93, 19);
		this.label1.TabIndex = 163;
		this.label1.Text = "نسبة الخصم";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label2.Location = new System.Drawing.Point(119, 19);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(70, 19);
		this.label2.TabIndex = 164;
		this.label2.Text = "الاجمالي";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label3.Location = new System.Drawing.Point(119, 53);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(89, 19);
		this.label3.TabIndex = 165;
		this.label3.Text = "قيمة الخصم";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label4.Location = new System.Drawing.Point(119, 121);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(58, 19);
		this.label4.TabIndex = 166;
		this.label4.Text = "الصافي";
		this.TxtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscount.Location = new System.Drawing.Point(6, 48);
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDiscount.Size = new System.Drawing.Size(107, 27);
		this.TxtDiscount.TabIndex = 160;
		this.TxtDiscount.Text = "0.000";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtDiscount_KeyDown);
		this.TxtDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtDiscount_KeyUp);
		this.TxtVatDisc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtVatDisc.DecimalDigits = 3;
		this.TxtVatDisc.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtVatDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatDisc.Location = new System.Drawing.Point(236, 115);
		this.TxtVatDisc.Name = "TxtVatDisc";
		this.TxtVatDisc.ReadOnly = true;
		this.TxtVatDisc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVatDisc.Size = new System.Drawing.Size(107, 27);
		this.TxtVatDisc.TabIndex = 162;
		this.TxtVatDisc.Text = "0.000";
		this.TxtVatDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatDisc.Value = new decimal(new int[4]);
		this.TxtVatDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label5.Location = new System.Drawing.Point(349, 89);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(92, 19);
		this.label5.TabIndex = 167;
		this.label5.Text = "الضريبة VAT";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label6.Location = new System.Drawing.Point(349, 58);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(118, 19);
		this.label6.TabIndex = 168;
		this.label6.Text = "الخصم الاجمالي";
		this.ribbonPage2.Name = "ribbonPage2";
		this.ribbonPage2.Text = "ribbonPage2";
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.RecoveredMenuItemFix5, this.toolStripSeparator1, this.MenuNewItem, this.RecoveredMenuItemFix6, this.MenuSearchItem, this.toolStripSeparator4,
			this.MenuPurchOrder, this.RecoveredMenuItemFix2, this.RecoveredMenuItemFix7, this.toolStripSeparator2, this.RecoveredMenuItemFix11, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.contextMenuStrip1.Size = new System.Drawing.Size(233, 340);
		this.MenuNew.Name = "MenuNew";
		this.MenuNew.Size = new System.Drawing.Size(232, 24);
		this.MenuNew.Text = "جديد                       F4";
		this.MenuNew.Click += new System.EventHandler(MenuNew_Click);
		this.MenuSave.Name = "MenuSave";
		this.MenuSave.Size = new System.Drawing.Size(232, 24);
		this.MenuSave.Text = "حفظ                       F5";
		this.MenuSave.Click += new System.EventHandler(MenuSave_Click);
		this.MenuEdit.Name = "MenuEdit";
		this.MenuEdit.Size = new System.Drawing.Size(232, 24);
		this.MenuEdit.Text = "تعديل                      F6";
		this.MenuEdit.Click += new System.EventHandler(MenuEdit_Click);
		this.MenuDelete.Name = "MenuDelete";
		this.MenuDelete.Size = new System.Drawing.Size(232, 24);
		this.MenuDelete.Text = "حذف                       F7";
		this.MenuDelete.Click += new System.EventHandler(MenuDelete_Click);
		this.RecoveredMenuItemFix5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.MenuPrintQued, this.MenuPreviewFatoura, this.MenuPrintFatoura });
		this.RecoveredMenuItemFix5.Name = "طباعةToolStripMenuItem1";
		this.RecoveredMenuItemFix5.Size = new System.Drawing.Size(232, 24);
		this.RecoveredMenuItemFix5.Text = "طباعة ";
		this.MenuPrintQued.Name = "MenuPrintQued";
		this.MenuPrintQued.Size = new System.Drawing.Size(259, 26);
		this.MenuPrintQued.Text = "معاينة القيد";
		this.MenuPrintQued.Click += new System.EventHandler(MenuPrintQued_Click);
		this.MenuPreviewFatoura.Name = "MenuPreviewFatoura";
		this.MenuPreviewFatoura.Size = new System.Drawing.Size(259, 26);
		this.MenuPreviewFatoura.Text = "معاينة الفاتورة";
		this.MenuPreviewFatoura.Click += new System.EventHandler(MenuPreviewFatoura_Click);
		this.MenuPrintFatoura.Name = "MenuPrintFatoura";
		this.MenuPrintFatoura.Size = new System.Drawing.Size(259, 26);
		this.MenuPrintFatoura.Text = "طباعة الفاتورة       Ctrl + P";
		this.MenuPrintFatoura.Click += new System.EventHandler(MenuPrintFatoura_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
		this.MenuNewItem.Name = "MenuNewItem";
		this.MenuNewItem.Size = new System.Drawing.Size(232, 24);
		this.MenuNewItem.Text = "صنف جديد            F12 ";
		this.MenuNewItem.Click += new System.EventHandler(MenuNewItem_Click);
		this.RecoveredMenuItemFix6.Name = "حركةصنفToolStripMenuItem";
		this.RecoveredMenuItemFix6.Size = new System.Drawing.Size(232, 24);
		this.RecoveredMenuItemFix6.Text = "حركة صنف";
		this.MenuSearchItem.Name = "MenuSearchItem";
		this.MenuSearchItem.Size = new System.Drawing.Size(232, 24);
		this.MenuSearchItem.Text = "بحث عن صنف        F3";
		this.MenuSearchItem.Click += new System.EventHandler(MenuSearchItem_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(229, 6);
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		this.MenuPurchOrder.Size = new System.Drawing.Size(232, 24);
		this.MenuPurchOrder.Text = "تحميل من عرض سعر  F9";
		this.RecoveredMenuItemFix7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix8, this.RecoveredMenuItemFix9, this.RecoveredMenuItemFix10 });
		this.RecoveredMenuItemFix7.Name = "الكمياتToolStripMenuItem1";
		this.RecoveredMenuItemFix7.Size = new System.Drawing.Size(232, 24);
		this.RecoveredMenuItemFix7.Text = "الكميات";
		this.RecoveredMenuItemFix8.Name = "الكميةالحاليةToolStripMenuItem";
		this.RecoveredMenuItemFix8.Size = new System.Drawing.Size(251, 26);
		this.RecoveredMenuItemFix8.Text = "الكمية الحالية";
		this.RecoveredMenuItemFix9.Name = "كلالكمياتبالمستودعاتToolStripMenuItem";
		this.RecoveredMenuItemFix9.Size = new System.Drawing.Size(251, 26);
		this.RecoveredMenuItemFix9.Text = "كل الكميات بالمستودعات";
		this.RecoveredMenuItemFix10.Name = "كلالكمياتبالفروعToolStripMenuItem";
		this.RecoveredMenuItemFix10.Size = new System.Drawing.Size(251, 26);
		this.RecoveredMenuItemFix10.Text = "كل الكميات بالفروع";
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(229, 6);
		this.RecoveredMenuItemFix11.Name = "تخصيصToolStripMenuItem1";
		this.RecoveredMenuItemFix11.Size = new System.Drawing.Size(232, 24);
		this.RecoveredMenuItemFix11.Text = "تخصيص";
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(229, 6);
		this.MenuExit.Name = "MenuExit";
		this.MenuExit.Size = new System.Drawing.Size(232, 24);
		this.MenuExit.Text = "خروج";
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Controls.Add(this.TxtType);
		this.groupBox3.Controls.Add(this.label20);
		this.groupBox3.Location = new System.Drawing.Point(12, 170);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1195, 342);
		this.groupBox3.TabIndex = 8;
		this.groupBox3.TabStop = false;
		this.gridControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridControl1.Location = new System.Drawing.Point(3, 16);
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.Size = new System.Drawing.Size(1189, 320);
		this.gridControl1.TabIndex = 0;
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 20f);
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[29]
		{
			this.gridColumn1, this.gridColumn2, this.gridColumn27, this.shearch, this.gridColumn19, this.gridColumn4, this.gridColumn23, this.gridColumn24, this.gridColumn20, this.gridColumn21,
			this.gridColumn3, this.gridColumn6, this.gridColumn7, this.gridColumn8, this.gridColumn22, this.gridColumn5, this.gridColumn9, this.gridColumn26, this.gridColumn28, this.gridColumn29,
			this.gridColumn30, this.gridColumn31, this.gridColumn32, this.gridColumn33, this.gridColumn34, this.gridColumn35, this.gridColumn36, this.gridColumn25, this.gridColumn37
		});
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "", null, "")
		});
		this.gridView1.Name = "gridView1";
		this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
		this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
		this.gridView1.OptionsPrint.EnableAppearanceOddRow = true;
		this.gridView1.OptionsPrint.PrintFilterInfo = true;
		this.gridView1.OptionsPrint.PrintPreview = true;
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridView1.OptionsView.ShowGroupPanel = false;
		this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Bold);
		this.gridColumn1.AppearanceCell.FontStyleDelta = System.Drawing.FontStyle.Bold;
		this.gridColumn1.AppearanceCell.Options.UseFont = true;
		this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn1.AppearanceHeader.Options.UseFont = true;
		this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.Caption = "كود الصنف";
		this.gridColumn1.ColumnEdit = this.repositoryItemButtonEditCodeSearch;
		this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.FieldName = "ItemCode";
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.Visible = true;
		this.gridColumn1.VisibleIndex = 0;
		this.gridColumn1.Width = 149;
		this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.Caption = "أسم الصنف";
		this.gridColumn2.FieldName = "ItemName";
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.gridColumn2.OptionsColumn.AllowFocus = false;
		this.gridColumn2.Visible = true;
		this.gridColumn2.VisibleIndex = 1;
		this.gridColumn2.Width = 250;
		this.gridColumn27.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn27.AppearanceCell.Options.UseFont = true;
		this.gridColumn27.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn27.AppearanceHeader.Options.UseFont = true;
		this.gridColumn27.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.Caption = "الشرح";
		this.gridColumn27.FieldName = "ItemNote";
		this.gridColumn27.MinWidth = 25;
		this.gridColumn27.Name = "gridColumn27";
		this.gridColumn27.Visible = true;
		this.gridColumn27.VisibleIndex = 2;
		this.gridColumn27.Width = 200;
		this.shearch.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.shearch.AppearanceCell.Options.UseFont = true;
		this.shearch.AppearanceCell.Options.UseTextOptions = true;
		this.shearch.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.shearch.AppearanceHeader.Options.UseFont = true;
		this.shearch.AppearanceHeader.Options.UseTextOptions = true;
		this.shearch.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.Caption = "اسم الوحده";
		this.shearch.ColumnEdit = this.repositoryItemButtonEdit7;
		this.shearch.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.FieldName = "UnitName";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		this.shearch.Visible = true;
		this.shearch.VisibleIndex = 3;
		this.shearch.Width = 61;
		this.gridColumn19.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.Caption = "نوع الوحده";
		this.gridColumn19.FieldName = "UnitType";
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.AllowFocus = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.Caption = "المخزن";
		this.gridColumn4.ColumnEdit = this.repositoryItemButtonEdit5;
		this.gridColumn4.FieldName = "StoreName";
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Visible = true;
		this.gridColumn4.VisibleIndex = 4;
		this.gridColumn23.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn23.AppearanceCell.Options.UseFont = true;
		this.gridColumn23.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn23.AppearanceHeader.Options.UseFont = true;
		this.gridColumn23.Caption = "متوسط التكلفة";
		this.gridColumn23.FieldName = "AvergItem";
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.OptionsColumn.AllowEdit = false;
		this.gridColumn23.OptionsColumn.AllowFocus = false;
		this.gridColumn24.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn24.AppearanceCell.Options.UseFont = true;
		this.gridColumn24.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn24.AppearanceHeader.Options.UseFont = true;
		this.gridColumn24.Caption = "اجمالي متوسط التكلفة";
		this.gridColumn24.FieldName = "SumAvergItem";
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn24.OptionsColumn.AllowEdit = false;
		this.gridColumn24.OptionsColumn.AllowFocus = false;
		this.gridColumn20.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn20.AppearanceCell.Options.UseFont = true;
		this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn20.AppearanceHeader.Options.UseFont = true;
		this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.Caption = "الكمية";
		this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.FieldName = "Quantity";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", "SUM={0:0.##}")
		});
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn20.Visible = true;
		this.gridColumn20.VisibleIndex = 5;
		this.gridColumn20.Width = 80;
		this.gridColumn21.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn21.AppearanceCell.Options.UseFont = true;
		this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn21.AppearanceHeader.Options.UseFont = true;
		this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.gridColumn21.Caption = "سعر الوحدة";
		this.gridColumn21.FieldName = "UnitPrice";
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "UnitPrice", "SUM={0:0.##}")
		});
		this.gridColumn21.Visible = true;
		this.gridColumn21.VisibleIndex = 6;
		this.gridColumn21.Width = 80;
		this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.Caption = "الاجمالى";
		this.gridColumn3.FieldName = "Total";
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.OptionsColumn.AllowFocus = false;
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn3.Visible = true;
		this.gridColumn3.VisibleIndex = 8;
		this.gridColumn3.Width = 80;
		this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn6.AppearanceCell.FontStyleDelta = System.Drawing.FontStyle.Bold;
		this.gridColumn6.AppearanceCell.Options.UseFont = true;
		this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn6.AppearanceHeader.Options.UseFont = true;
		this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.Caption = "الضريبة";
		this.gridColumn6.FieldName = "ItemVat";
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.AllowFocus = false;
		this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn6.OptionsColumn.ReadOnly = true;
		this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ItemVat", "SUM={0:0.##}")
		});
		this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn6.Visible = true;
		this.gridColumn6.VisibleIndex = 7;
		this.gridColumn6.Width = 80;
		this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.Caption = "الاجمالي بالضريبه";
		this.gridColumn7.FieldName = "Net";
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Net", "SUM={0:0.##}")
		});
		this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn7.Visible = true;
		this.gridColumn7.VisibleIndex = 9;
		this.gridColumn7.Width = 80;
		this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn8.AppearanceCell.FontStyleDelta = System.Drawing.FontStyle.Bold;
		this.gridColumn8.AppearanceCell.Options.UseFont = true;
		this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn8.AppearanceHeader.Options.UseFont = true;
		this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.Caption = "حذف";
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit6;
		this.gridColumn8.CustomizationCaption = "ط­1ظپ";
		this.gridColumn8.FieldName = "DeleteItem";
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Visible = true;
		this.gridColumn8.VisibleIndex = 10;
		this.gridColumn8.Width = 27;
		this.gridColumn22.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn22.AppearanceCell.Options.UseFont = true;
		this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn22.AppearanceHeader.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.Caption = "إجمالى بعد الخصم";
		this.gridColumn22.FieldName = "SumPriceDisc";
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn22.OptionsColumn.AllowEdit = false;
		this.gridColumn22.OptionsColumn.AllowFocus = false;
		this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.Caption = "الضريبه بعد الخصم";
		this.gridColumn5.FieldName = "ItemVatDisc";
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.OptionsColumn.AllowEdit = false;
		this.gridColumn5.OptionsColumn.AllowFocus = false;
		this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.Caption = "ظ…";
		this.gridColumn9.FieldName = "SN";
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn9.Visible = true;
		this.gridColumn9.VisibleIndex = 11;
		this.gridColumn9.Width = 40;
		this.gridColumn26.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn26.AppearanceCell.Options.UseFont = true;
		this.gridColumn26.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn26.AppearanceHeader.Options.UseFont = true;
		this.gridColumn26.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.Caption = "المجموع";
		this.gridColumn26.FieldName = "SumPrice";
		this.gridColumn26.MinWidth = 25;
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.Visible = true;
		this.gridColumn26.VisibleIndex = 12;
		this.gridColumn26.Width = 94;
		this.gridColumn28.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn28.AppearanceCell.Options.UseFont = true;
		this.gridColumn28.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn28.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn28.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn28.AppearanceHeader.Options.UseFont = true;
		this.gridColumn28.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn28.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn28.Caption = "رقم المخزن";
		this.gridColumn28.FieldName = "StoreID";
		this.gridColumn28.MinWidth = 25;
		this.gridColumn28.Name = "gridColumn28";
		this.gridColumn28.OptionsColumn.AllowEdit = false;
		this.gridColumn28.OptionsColumn.AllowFocus = false;
		this.gridColumn28.Visible = true;
		this.gridColumn28.VisibleIndex = 13;
		this.gridColumn28.Width = 94;
		this.gridColumn29.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn29.AppearanceCell.Options.UseFont = true;
		this.gridColumn29.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn29.AppearanceHeader.Options.UseFont = true;
		this.gridColumn29.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.Caption = "قيمه الخصم";
		this.gridColumn29.FieldName = "AmountDisc";
		this.gridColumn29.MinWidth = 25;
		this.gridColumn29.Name = "gridColumn29";
		this.gridColumn29.Visible = true;
		this.gridColumn29.VisibleIndex = 14;
		this.gridColumn29.Width = 94;
		this.gridColumn30.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn30.AppearanceCell.Options.UseFont = true;
		this.gridColumn30.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn30.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn30.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn30.AppearanceHeader.Options.UseFont = true;
		this.gridColumn30.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn30.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn30.Caption = "نسبه الخصم";
		this.gridColumn30.FieldName = "PercentDisc";
		this.gridColumn30.MinWidth = 25;
		this.gridColumn30.Name = "gridColumn30";
		this.gridColumn30.Visible = true;
		this.gridColumn30.VisibleIndex = 15;
		this.gridColumn30.Width = 94;
		this.gridColumn31.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn31.AppearanceCell.Options.UseFont = true;
		this.gridColumn31.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn31.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn31.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn31.AppearanceHeader.Options.UseFont = true;
		this.gridColumn31.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn31.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn31.Caption = "هل ضريبه";
		this.gridColumn31.FieldName = "IsItemTax";
		this.gridColumn31.MinWidth = 25;
		this.gridColumn31.Name = "gridColumn31";
		this.gridColumn31.OptionsColumn.AllowEdit = false;
		this.gridColumn31.OptionsColumn.AllowFocus = false;
		this.gridColumn31.Width = 94;
		this.gridColumn32.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn32.AppearanceCell.Options.UseFont = true;
		this.gridColumn32.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn32.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn32.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn32.AppearanceHeader.Options.UseFont = true;
		this.gridColumn32.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn32.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn32.Caption = "قيمه الضريبه";
		this.gridColumn32.FieldName = "Tax_Value";
		this.gridColumn32.MinWidth = 25;
		this.gridColumn32.Name = "gridColumn32";
		this.gridColumn32.OptionsColumn.AllowEdit = false;
		this.gridColumn32.OptionsColumn.AllowFocus = false;
		this.gridColumn32.Width = 94;
		this.gridColumn33.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn33.AppearanceCell.Options.UseFont = true;
		this.gridColumn33.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn33.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn33.AppearanceHeader.Options.UseFont = true;
		this.gridColumn33.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn33.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn33.Caption = "ItemID";
		this.gridColumn33.FieldName = "ItemID";
		this.gridColumn33.MinWidth = 25;
		this.gridColumn33.Name = "gridColumn33";
		this.gridColumn33.OptionsColumn.AllowEdit = false;
		this.gridColumn33.OptionsColumn.AllowFocus = false;
		this.gridColumn33.Width = 94;
		this.gridColumn34.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn34.AppearanceCell.Options.UseFont = true;
		this.gridColumn34.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn34.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn34.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn34.AppearanceHeader.Options.UseFont = true;
		this.gridColumn34.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn34.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn34.Caption = "سعر بعد الخصم";
		this.gridColumn34.FieldName = "UnitPriceDisc";
		this.gridColumn34.MinWidth = 25;
		this.gridColumn34.Name = "gridColumn34";
		this.gridColumn34.OptionsColumn.AllowEdit = false;
		this.gridColumn34.OptionsColumn.AllowFocus = false;
		this.gridColumn34.Visible = true;
		this.gridColumn34.VisibleIndex = 16;
		this.gridColumn34.Width = 94;
		this.gridColumn35.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn35.AppearanceCell.Options.UseFont = true;
		this.gridColumn35.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn35.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn35.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn35.AppearanceHeader.Options.UseFont = true;
		this.gridColumn35.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn35.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn35.Caption = "رقم الوحده";
		this.gridColumn35.FieldName = "ItemUnitID";
		this.gridColumn35.MinWidth = 25;
		this.gridColumn35.Name = "gridColumn35";
		this.gridColumn35.OptionsColumn.AllowEdit = false;
		this.gridColumn35.OptionsColumn.AllowFocus = false;
		this.gridColumn35.Visible = true;
		this.gridColumn35.VisibleIndex = 17;
		this.gridColumn35.Width = 94;
		this.gridColumn36.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn36.AppearanceCell.Options.UseFont = true;
		this.gridColumn36.Caption = "OrginalPrice";
		this.gridColumn36.FieldName = "OrginalPrice";
		this.gridColumn36.MinWidth = 25;
		this.gridColumn36.Name = "gridColumn36";
		this.gridColumn36.OptionsColumn.AllowEdit = false;
		this.gridColumn36.OptionsColumn.AllowFocus = false;
		this.gridColumn36.Width = 94;
		this.gridColumn25.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.Caption = "عدد الايام";
		this.gridColumn25.FieldName = "DayNum";
		this.gridColumn25.MinWidth = 25;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.OptionsColumn.AllowEdit = false;
		this.gridColumn25.OptionsColumn.AllowFocus = false;
		this.gridColumn25.OptionsFilter.AllowAutoFilter = false;
		this.gridColumn25.OptionsFilter.AllowFilter = false;
		this.gridColumn25.Visible = true;
		this.gridColumn25.VisibleIndex = 18;
		this.gridColumn25.Width = 94;
		this.gridColumn37.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn37.AppearanceCell.Options.UseFont = true;
		this.gridColumn37.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn37.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn37.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn37.AppearanceHeader.Options.UseFont = true;
		this.gridColumn37.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn37.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn37.Caption = "تفاصيل الصنف";
		this.gridColumn37.ColumnEdit = this.repositoryItemButtonEdit4;
		this.gridColumn37.FieldName = "Info";
		this.gridColumn37.MinWidth = 25;
		this.gridColumn37.Name = "gridColumn37";
		this.gridColumn37.Visible = true;
		this.gridColumn37.VisibleIndex = 19;
		this.gridColumn37.Width = 94;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		this.gridColumn10.Caption = "كود_الصنف";
		this.gridColumn10.Name = "gridColumn10";
		this.gridColumn10.Visible = true;
		this.gridColumn10.VisibleIndex = 0;
		this.gridColumn11.Caption = "أسم الصنف";
		this.gridColumn11.Name = "gridColumn11";
		this.gridColumn11.Visible = true;
		this.gridColumn11.VisibleIndex = 1;
		this.gridColumn12.Caption = "الوحدة";
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		this.gridColumn12.Visible = true;
		this.gridColumn12.VisibleIndex = 2;
		this.gridColumn13.Caption = "الكمية";
		this.gridColumn13.Name = "gridColumn13";
		this.gridColumn13.Visible = true;
		this.gridColumn13.VisibleIndex = 3;
		this.gridColumn14.Caption = "سعر الوحدة";
		this.gridColumn14.Name = "gridColumn14";
		this.gridColumn14.Visible = true;
		this.gridColumn14.VisibleIndex = 4;
		this.gridColumn15.Caption = "الاجمالى";
		this.gridColumn15.Name = "gridColumn15";
		this.gridColumn15.Visible = true;
		this.gridColumn15.VisibleIndex = 5;
		this.gridColumn16.Caption = "الضربية";
		this.gridColumn16.Name = "gridColumn16";
		this.gridColumn16.Visible = true;
		this.gridColumn16.VisibleIndex = 6;
		this.gridColumn17.Caption = "إجمالى المبلغ";
		this.gridColumn17.Name = "gridColumn17";
		this.gridColumn17.Visible = true;
		this.gridColumn17.VisibleIndex = 7;
		this.BtnNewCustomer.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNewCustomer.Appearance.Options.UseFont = true;
		this.BtnNewCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewCustomer.ImageOptions.Image");
		this.BtnNewCustomer.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewCustomer.Location = new System.Drawing.Point(728, 56);
		this.BtnNewCustomer.Margin = new System.Windows.Forms.Padding(0);
		this.BtnNewCustomer.Name = "BtnNewCustomer";
		this.BtnNewCustomer.Size = new System.Drawing.Size(31, 28);
		this.BtnNewCustomer.TabIndex = 143;
		this.BtnNewCustomer.Click += new System.EventHandler(BtnNewCustomer_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1219, 749);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmRentalInvoice";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "فاتوره تأجير";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmRentalInvoice_FormClosing);
		base.Load += new System.EventHandler(FrmRentalInvoice_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FrmRentalInvoice_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppIDNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtParentName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDayNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		base.ResumeLayout(false);
	}
}


