using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
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
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DAL.Models;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Dto;
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmCachMoney;
using GTSErpSystem.Frms.Orders.FrmLuckUpOrders;
using GTSErpSystem.Frms.Orders.FrmOpenDay;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Frms.Reports.Stores;
using GTSErpSystem.Helper;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;
using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.APIHelper;

namespace GTSErpSystem.Frms.Orders;

public class FrmCashir : XtraForm
{
	private ApplicationDbContext context;

	private DiscountCodesManager DisCode;

	private Class_WaitFormFunc waitForm = new Class_WaitFormFunc();

	private Class_ElctroncInvoice Zakat = new Class_ElctroncInvoice();

	private GTSdbEntities db = new GTSdbEntities();

	private Class_Items ITM = new Class_Items();

	private DataTable datatable = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	private DataTable CashierPrint = new DataTable();

	private Class_Customer Cust = new Class_Customer();

	private Class_Contract Contract = new Class_Contract();

	private DataSetALL ST = new DataSetALL();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private Class_Bounce BNC = new Class_Bounce();

	private Class_Orders ORD = new Class_Orders();

	private Class_PriceOffer PurORD = new Class_PriceOffer();

	private Class_Customer SPR = new Class_Customer();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	public static int TblID = 1;

	public string ItmID2;

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private int tafqitID = LoginDetails.TafqitID;

	public string txtWordAr;

	public string txtWordEn;

	public int UserSellPrice = LoginDetails.UserSellPrice;

	public string CodeItm;

	private static int IsShow;

	private static int PrintID = 0;

	private int intType = 1;

	public bool ISEVENTDISC = true;

	public bool ISEVENTDISCTotal = true;

	public int EVENTDISC = 1;

	public decimal Cash;

	public decimal Bank;

	public decimal MoneyNeed;

	public decimal MoneyPaying;

	public decimal MoneyNet;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	public decimal FrmCash22;

	public decimal FrmBank22;

	public decimal Safy;

	private decimal? CreditLimit = 0m;

	private int ItmID = 0;

	private static int Code = 0;

	private string CurrentItemId = "0";

	private string ItmName = "";

	private bool OpenClose = false;

	private string TxtVatNumCustomer;

	private decimal TotalHasm = 0m;

	private int NumberDisc = 0;

	private int StorId = 0;

	private string UnitName = string.Empty;

	private string fileNameAR = Application.StartupPath + "\\Layout\\CashirAR.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\CashirEN.xml";

	private decimal ChargeAfterSale = 0m;

	private int SN = 1;

	private IContainer components = null;

	private NumericEditBox TxtNet;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private Label LblSup;

	private DateEdit TxtDate;

	private System.Windows.Forms.ComboBox TxtType;

	private Label LBlSuppAccount;

	private NumericEditBox TxtTotals;

	private Label label2;

	private Label label3;

	private NumericEditBox TxtVat;

	private ButtonEdit TxtBarcode;

	private NumericEditBox TxtTotalDiscount;

	private Label label7;

	private NumericEditBox TxtVatDisc;

	private Label label1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem MenuNew;

	private ToolStripMenuItem MenuSave;

	private ToolStripMenuItem MenuEdit;

	private ToolStripMenuItem MenuDelete;

	private ToolStripMenuItem طباعةToolStripMenuItem1;

	private ToolStripMenuItem MenuPrintQued;

	private ToolStripMenuItem MenuPreviewFatoura;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuNewItem;

	private ToolStripMenuItem حركةصنفToolStripMenuItem;

	private ToolStripMenuItem MenuSearchItem;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem MenuPurchOrder;

	private ToolStripMenuItem استعلامToolStripMenuItem1;

	private ToolStripMenuItem رقمالفاتورةToolStripMenuItem1;

	private ToolStripMenuItem اسمالموردToolStripMenuItem1;

	private ToolStripMenuItem نوعالفاتورةToolStripMenuItem1;

	private ToolStripMenuItem الكمياتToolStripMenuItem1;

	private ToolStripMenuItem الكميةالحاليةToolStripMenuItem;

	private ToolStripMenuItem كلالكمياتبالمستودعاتToolStripMenuItem;

	private ToolStripMenuItem كلالكمياتبالفروعToolStripMenuItem;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem تخصيصToolStripMenuItem1;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem MenuExit;

	private TileControl tileControl1;

	private TileGroup tileGroup2;

	private TileControl tileControl2;

	private TileGroup tileGroup3;

	private NumericEditBox TxtCost;

	private Label lblRoom;

	private Label lblTable;

	private NumericUpDown TxtTable;

	private NumericUpDown TxtRoom;

	private ToggleSwitch toggleCashierType;

	private SimpleButton BtnReturnCashier;

	private Label label12;

	private NumericEditBox TxtTabocc;

	private Label label11;

	private NumericEditBox TxtDiscount;

	private ButtonEdit TxtCode;

	private NumericEditBox txtdicoutprege;

	private ToolStripMenuItem f5حفظالفاتورةToolStripMenuItem;

	private ToolStripMenuItem f6تعديلالفاتورةToolStripMenuItem;

	private ToolStripMenuItem f7حذفالفاتورةToolStripMenuItem;

	private ToolStripMenuItem f9بحثفيعروضالاسعارToolStripMenuItem;

	private ToolStripMenuItem f12فتحكرتالصنفToolStripMenuItem;

	private ToolStripMenuItem ctrlDeleteحذفالسطرالحاليToolStripMenuItem;

	private ToolStripMenuItem ctrlToolStripMenuItem;

	private ToolStripMenuItem ctrlنقصانكميةالسطرالحاليToolStripMenuItem;

	private ToolStripMenuItem ctrlPطباعةالفاتورةToolStripMenuItem;

	private ToolStripMenuItem ctrlLقفلمؤقتToolStripMenuItem;

	private Label label14;

	private Label label15;

	private ToolStripMenuItem f11اظهارالكمياتلكلالمخازنToolStripMenuItem;

	private GridView gridView1;

	private GridColumn gridColumn24;

	private GridColumn gridColumn23;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn gridColumn26;

	private GridColumn shearch;

	private GridColumn gridColumn19;

	private GridColumn gridColumn25;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn22;

	private GridColumn gridColumn5;

	private GridColumn gridColumn8;

	private GridColumn gridColumn9;

	private GridColumn gridColumn27;

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

	private GridColumn gridColumn28;

	private GridColumn gridColumn29;

	private GridColumn gridColumn30;

	private GridColumn gridColumn31;

	private NumericEditBox TxtAllDiscount;

	private Label label8;

	private ToggleSwitch toggleSectoral;

	private GroupBox groupBox5;

	private Label label48;

	private TextEdit TxtNote;

	private Label label36;

	private Label label31;

	private System.Windows.Forms.ComboBox Comp_Store;

	private System.Windows.Forms.ComboBox CBBounce;

	private TextEdit TxtNumNote;

	private ToggleSwitch toggleHold;

	private Label label18;

	private ButtonEdit BtnOrderSearch;

	private Label LblCustLast;

	private Label label27;

	private Label label28;

	private Label label29;

	private Label label30;

	private ToolStripMenuItem كشفحسابToolStripMenuItem;

	private GridColumn gridColumn4;

	private DateEdit DateHold;

	private GridColumn gridColumn34;

	private GridColumn gridColumn35;

	private Label LblDateHold;

	private SimpleButton BtnRecipt;

	private GroupBox groupBox4;

	private ButtonEdit BtnCustomerSearch;

	private SimpleButton BtnNewCustomer;

	private Label label22;

	private Label label34;

	private Label label38;

	private Label label35;

	private Label label19;

	private Label label33;

	private Label label42;

	private Label label43;

	private TextEdit TxtAdditionalNum;

	private TextEdit TxtStreet;

	private TextEdit TxtDistrict;

	private TextEdit TxtCommercialRecord;

	private TextEdit TxtPostalCode;

	private TextEdit TxtCountry;

	private TextEdit TxtCity;

	private Label label6;

	private Label label9;

	private Label label37;

	private Label label10;

	private Label label46;

	private TextEdit TxtPhone;

	private TextEdit TxtCharge;

	private TextEdit TxtSales;

	private ButtonEdit TxtSuppliers;

	private TextEdit TxtNumVat;

	private Label label13;

	private NumericEditBox TxtQuntity;

	private Label label16;

	private Label LblNowQuintity;

	private NumericEditBox TxtAllQuantity;

	private GridColumn ItemID;

	private ToggleSwitch HoldOpenClose;

	private ToolStripMenuItem صرفالحجزToolStripMenuItem;

	private SimpleButton BtnCheckOut;

	private GridColumn gridColumn37;

	private SimpleButton BtnMinNet;

	private SimpleButton BtnPlusNet;

	private SimpleButton BtnTailor;

	private ToolStripMenuItem وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem;

	private ToolStripMenuItem وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem1;

	private ToolStripMenuItem وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem2;

	private GridColumn gridColumn36;

	private GridColumn gridColumn38;

	private GridColumn gridColumn32;

	private GridColumn gridColumn33;

	private GridColumn gridColumn39;

	private GridColumn gridColumn40;

	private GridColumn gridColumn41;

	private GridColumn gridColumn42;

	private GridColumn gridColumn43;

	public GridControl gridControl1;

	private GridColumn gridColumn44;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4Units;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonEditStore;

	private TileGroup tileGroup1;

	private TileGroup tileGroup4;

	private TileGroup tileGroup5;

	private TileGroup tileGroup6;

	private TileGroup tileGroup7;

	private TileGroup tileGroup9;

	private TileGroup tileGroup10;

	private TileGroup tileGroup11;

	private TileGroup tileGroup12;

	private TileGroup tileGroup13;

	private TileGroup tileGroup14;

	private ButtonEdit BtnDisCode;

	private GroupBox GropGlasses;

	private ButtonEdit BtnCustomerSearch2;

	private SimpleButton BtnNewCustomer2;

	private Label label21;

	private TextEdit TxtLeftAdd;

	private TextEdit TxtLeftAxis;

	private TextEdit TxtLeftCYL;

	private TextEdit TxtLeftSph;

	private TextEdit TxtRightAdd;

	private TextEdit TxtRightAxis;

	private Label label26;

	private Label label39;

	private Label label40;

	private TextEdit TxtRightCYL;

	private TextEdit TxtCharge2;

	private TextEdit TxtSales2;

	private ButtonEdit TxtSuppliers2;

	private TextEdit TxtRightSph;

	private Label label44;

	private TextEdit TxtNoteGlass;

	private Label lNote;

	private TextEdit _TxtPhone;

	private TextEdit _TxtVatNum;

	private Label label32;

	private Label label51;

	private Label label50;

	private Label label49;

	private Label label47;

	private Label label45;

	private Label label4;

	private Label LValid;

	private Label LSucced;

	private SimpleButton BtnUpload;

	private GridColumn gridColumn45;

	private GridColumn gridColumn46;

	private GridColumn gridColumn47;

	private SimpleButton BtnTypeInvoic;

	private SimpleButton BtnOpenCashir;

	private Mode mode { get; set; }

	public FrmCashir()
	{
		InitializeComponent();
		TxtDigital();
		SetInitialRow();
		SetInitialRowPrinter();
		BindComboBox();
		IsRestaurantActive();
		BtnDefault();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
		context = new ApplicationDbContext();
		DisCode = new DiscountCodesManager(context);
		if (!LoginDetails.DisCode)
		{
			BtnDisCode.Visible = false;
		}
		if (LoginDetails.Glasses)
		{
			groupBox4.Visible = false;
			GropGlasses.Location = new Point(577, -1);
		}
		else
		{
			GropGlasses.Visible = false;
		}
		if (LoginDetails.IsElectronicInvoice)
		{
			BtnDelete.Visible = false;
			BtnEdit.Visible = false;
		}
		Setting();
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "ItemCode"));
		string text2 = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "ItemName"));
		decimal num = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "Quantity"));
		if (!(text == string.Empty) && !(text2 == string.Empty) && !(num <= 0m))
		{
			return;
		}
		Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(text).FirstOrDefault();
		if (get_Item_ByCodeALL_Result != null)
		{
			e.Valid = false;
			if (text == string.Empty)
			{
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[0];
				gridView1.ShowEditor();
			}
			else if (text2 == string.Empty)
			{
				MessageBox.Show("برجاء إدخال أسم صنف");
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[0];
				gridView1.ShowEditor();
			}
			else if (num <= 0m)
			{
				MessageBox.Show("برجاء إدخال كمية أكبر من الصفر");
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[3];
				gridView1.ShowEditor();
			}
		}
	}

	private void gridView1_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
	{
		e.ExceptionMode = ExceptionMode.NoAction;
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[0];
	}

	private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
	{
	}

	private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
	{
		try
		{
			if (e.Column.FieldName == "Net" || e.Column.FieldName == "Total")
			{
				UpdateItemRowByNet(e);
			}
			else
			{
				UpdateItemRow(e.Column.FieldName, e);
			}
			if (e.Column.FieldName == "Item_Name")
			{
				string value = gridView1.GetRowCellValue(e.RowHandle, e.Column).ToString();
				gridView1.SetRowCellValue(e.RowHandle, e.Column, value);
			}
			ArrangeDatatable();
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

	private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
	{
		try
		{
			ArrangeDatatable();
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("ItemID");
			if (focusedRowCellDisplayText != string.Empty)
			{
				ItmID = Convert.ToInt32(gridView1.GetFocusedRowCellDisplayText("ItemID"));
			}
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("ItemName");
			if (focusedRowCellDisplayText2 != string.Empty)
			{
				ItmName = gridView1.GetFocusedRowCellDisplayText("ItemName");
			}
			string codeItm = Convert.ToString(gridView1.GetFocusedRowCellDisplayText("ItemCode"));
			UnitName = gridView1.GetFocusedRowCellDisplayText("UnitName");
			string StoreName = gridView1.GetFocusedRowCellDisplayText("StoreName");
			StorId = (from x in gTSdbEntities.Account_Stores
				where x.Store_Name == StoreName
				select x.ID).FirstOrDefault();
			GetAllitems_alternatives(ItmID, StorId, UnitName);
			CodeItm = codeItm;
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("SN");
			if (!int.TryParse(focusedRowCellDisplayText3, out SN))
			{
				SN = 0;
			}
		}
		catch (Exception)
		{
		}
	}

	private void repositoryItemButtonEdit1_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		GridPlus(1m, '2');
	}

	private void repositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		GridDelete();
	}

	private void repositoryItemButtonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		GridPlus(1m, '-');
	}

	private void repositoryItemButtonEditStore_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string Itemocde = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "كود_الصنف"));
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
							row["StoreID"] = StoreID;
							break;
						}
					}
					gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[5];
					gridView1.ShowEditor();
					frmSearchTransfer.Close();
					return;
				}
				int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				foreach (DataRow row2 in datatable.Rows)
				{
					if (row2["SN"].ToString() == Convert.ToString(num2))
					{
						row2["StoreName"] = Comp_Store.Text;
						row2["StoreID"] = Comp_Store.SelectedValue;
					}
				}
				return;
			}
		}
		MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف المخازن", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}

	private void repositoryItemButtonEdit4Units_ButtonClick(object sender, ButtonPressedEventArgs e)
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
							if (!(row["SN"].ToString() == Convert.ToString(text2)))
							{
								continue;
							}
							row["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
							row["UnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
							row["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
							row["UnitType"] = "الصغرى";
							row["Quantity"] = 1;
							row["AvergItem"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.Average_cost);
							row["SumAvergItem"] = Math.Round(Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]), DecimalNumQty);
							if (LoginDetails.ItemTotalWithVat)
							{
								decimal value = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
								if (toggleSectoral.IsOn)
								{
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall / (decimal?)value);
									row["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
									row["OrginalPrice"] = row["UnitPrice"];
								}
								else
								{
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
									row["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall2), DecimalNum);
									row["OrginalPrice"] = row["Total"];
								}
								VatWithTotal(get_Item_ByCodeALL_Result.Is_Tax, value, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
							}
							else
							{
								if (toggleSectoral.IsOn)
								{
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
								}
								else
								{
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
									row["SumPric"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall2), DecimalNum);
								}
								Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, get_Item_ByCodeALL_Result.Tax_Tobacco, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
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
							if (!(row2["SN"].ToString() == Convert.ToString(text2)))
							{
								continue;
							}
							row2["Quantity"] = 1;
							decimal num3 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.ConvertMediumUnit);
							row2["SumAvergItem"] = Math.Round(Convert.ToDecimal(row2["AvergItem"]) * (Convert.ToDecimal(row2["Quantity"]) * num3), DecimalNumQty);
							row2["UnitName"] = get_Item_ByCodeALL_Result2.UnitMedium;
							row2["ItemUnitID"] = get_Item_ByCodeALL_Result2.UnitMediumID;
							row2["UnitType"] = "المتوسطة";
							row2["AvergItem"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.Average_cost * get_Item_ByCodeALL_Result2.ConvertMediumUnit);
							row2["SumAvergItem"] = get_Item_ByCodeALL_Result2.Average_cost * get_Item_ByCodeALL_Result2.ConvertMediumUnit;
							row2["UnitPrice"] = get_Item_ByCodeALL_Result2.SellPriceMedium;
							row2["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium);
							if (LoginDetails.ItemTotalWithVat)
							{
								decimal value2 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.Tax_Value) / 100m + 1m;
								if (toggleSectoral.IsOn)
								{
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium / (decimal?)value2);
									row2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium), DecimalNum);
									row2["OrginalPrice"] = row2["UnitPrice"];
								}
								else
								{
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium2);
									row2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium2), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
								}
								VatWithTotal(get_Item_ByCodeALL_Result2.Is_Tax, value2, Convert.ToDecimal(row2["UnitPrice"]), Convert.ToDecimal(row2["UnitPrice"]), Convert.ToDecimal(row2["Quantity"]), row2, "ItemCode");
							}
							else
							{
								if (toggleSectoral.IsOn)
								{
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium);
									row2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium), DecimalNum);
								}
								else
								{
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium2);
									row2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium2), DecimalNum);
								}
								Vat(get_Item_ByCodeALL_Result2.Is_Tax, get_Item_ByCodeALL_Result2.Tax_Value, get_Item_ByCodeALL_Result2.Tax_Tobacco, Convert.ToDecimal(row2["UnitPrice"]), Convert.ToDecimal(row2["Quantity"]), row2, "ItemCode");
							}
						}
						frmSearchUnit.Close();
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
							if (!(row3["SN"].ToString() == Convert.ToString(text2)))
							{
								continue;
							}
							row3["Quantity"] = 1;
							row3["UnitName"] = get_Item_ByCodeALL_Result3.UnitLarge;
							row3["ItemUnitID"] = get_Item_ByCodeALL_Result3.UnitLargeID;
							row3["UnitType"] = "الكبرى";
							row3["AvergItem"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.Average_cost * get_Item_ByCodeALL_Result3.ConvertMediumUnit * get_Item_ByCodeALL_Result3.ConvertBigUnit);
							row3["SumAvergItem"] = get_Item_ByCodeALL_Result3.Average_cost * get_Item_ByCodeALL_Result3.ConvertMediumUnit * get_Item_ByCodeALL_Result3.ConvertBigUnit;
							if (LoginDetails.ItemTotalWithVat)
							{
								decimal value3 = Convert.ToDecimal(get_Item_ByCodeALL_Result3.Tax_Value) / 100m + 1m;
								if (toggleSectoral.IsOn)
								{
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge / (decimal?)value3);
									row3["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge), DecimalNum);
									row3["OrginalPrice"] = row3["UnitPrice"];
								}
								else
								{
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge2);
									row3["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge2), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
								}
								VatWithTotal(get_Item_ByCodeALL_Result3.Is_Tax, value3, Convert.ToDecimal(row3["UnitPrice"]), Convert.ToDecimal(row3["UnitPrice"]), Convert.ToDecimal(row3["Quantity"]), row3, "ItemCode");
							}
							else
							{
								if (toggleSectoral.IsOn)
								{
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge);
									row3["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge), DecimalNum);
								}
								else
								{
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge2);
									row3["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge2), DecimalNum);
								}
								Vat(get_Item_ByCodeALL_Result3.Is_Tax, get_Item_ByCodeALL_Result3.Tax_Value, get_Item_ByCodeALL_Result3.Tax_Tobacco, Convert.ToDecimal(row3["UnitPrice"]), Convert.ToDecimal(row3["Quantity"]), row3, "ItemCode");
							}
						}
						frmSearchUnit.Close();
					}
				}
				frmSearchUnit.Close();
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			frmSearchUnit.Close();
		}
		else
		{
			MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف الوحدات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void BtnOpenCashir_Click(object sender, EventArgs e)
	{
		try
		{
			string defualtPrintA = LoginDetails.DefualtPrintA4;
			byte[] bytes = new byte[5] { 27, 112, 0, 100, 250 };
			RawPrinterHelper.SendBytesToPrinter(defualtPrintA, bytes);
		}
		catch (Exception ex)
		{
			MessageBox.Show("فشل في فتح درج الكاشير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		if (LoginDetails.IsSecureCashier)
		{
			FrmPassWordSupervisor frmPassWordSupervisor = new FrmPassWordSupervisor();
			frmPassWordSupervisor.ShowDialog();
			if (frmPassWordSupervisor.ISClose)
			{
				datatable.Rows.Clear();
				Clear();
			}
			frmPassWordSupervisor.Close();
			TxtBarcode.Focus();
		}
		else
		{
			datatable.Rows.Clear();
			Clear();
		}
		SN = 1;
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		if (BtnEdit.Enabled)
		{
			Edit();
		}
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameAR);
		}
		else
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameEN);
		}
		Close();
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		if (!(TxtCode.Text != string.Empty))
		{
			return;
		}
		DialogResult dialogResult = MessageBox.Show("هل تريد طباعة الفاتورة الحراري رقم ( " + TxtCode.Text + " ) ؟", "مستكشف الطباعه الحراري", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		if (dialogResult == DialogResult.Yes)
		{
			Thread thread = new Thread((ThreadStart)delegate
			{
				BeginInvoke((Action)delegate
				{
					Print(Convert.ToInt32(TxtCode.Text));
				});
			});
			thread.Start();
		}
		DialogResult dialogResult2 = MessageBox.Show("هل تريد طباعة الفاتورة A4 رقم ( " + TxtCode.Text + " ) ؟", "مستكشف الطباعه A4", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		if (dialogResult2 != DialogResult.Yes)
		{
			return;
		}
		Thread thread2 = new Thread((ThreadStart)delegate
		{
			BeginInvoke((Action)delegate
			{
				PrintA4(Convert.ToInt32(TxtCode.Text));
			});
		});
		thread2.Start();
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchOrders frmSearchOrders = new FrmSearchOrders();
		frmSearchOrders.ShowDialog();
		if (frmSearchOrders.CustCode > 0)
		{
			SearchOrderNum(frmSearchOrders.CustCode);
			GetElctornc();
			Code = Convert.ToInt32(TxtCode.Text);
		}
	}

	private void TxtCust_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
		frmSearchCustomer.ShowDialog();
		if (frmSearchCustomer.CustCode > 0)
		{
			SearchCustomer(frmSearchCustomer.CustID);
		}
	}

	private void tileBarItem2_ItemClick(object sender, TileItemEventArgs e)
	{
		int id = e.Item.Id;
		Get_Item_ByID_Result get_Item_ByID_Result = ITM.Get_Item_ByID(id).FirstOrDefault();
		InsertNewItem(get_Item_ByID_Result.Item_code);
	}

	private void tileBarItem1_ItemClick(object sender, TileItemEventArgs e)
	{
		bool isVatActive = ORD.IsVatActive();
		try
		{
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
			List<Item_Items> list = gTSdbEntities.Item_Items.Where((Item_Items x) => x.Category_ID == (int?)e.Item.Id).ToList();
			tileGroup3.Items.Clear();
			string itemColor = LoginDetails.ItemColor;
			string itemFontColor = LoginDetails.ItemFontColor;
			foreach (Item_Items item in list)
			{
				TileItem tileItem = new TileItem();
				tileGroup3.Items.Add(tileItem);
				tileItem.Id = item.ItemId;
				tileItem.ItemSize = TileItemSize.Wide;
				tileItem.Appearance.BackColor = ColorTranslator.FromHtml(itemColor);
				tileItem.Appearance.ForeColor = ColorTranslator.FromHtml(itemFontColor);
				tileItem.Appearance.Font = new Font("Tahoma", Convert.ToSingle(LoginDetails.FontSize));
				tileItem.ItemClick += tileBarItem2_ItemClick;
				TileItemElement tileItemElement = new TileItemElement();
				tileItem.Elements.Add(tileItemElement);
				TileItemElement tileItemElement2 = new TileItemElement();
				tileItem.Elements.Add(tileItemElement2);
				TileItemElement SellPrice = new TileItemElement();
				tileItem.Elements.Add(SellPrice);
				TileItemElement tileItemElement3 = new TileItemElement();
				tileItem.Elements.Add(tileItemElement3);
				if (LoginDetails.IsItemNameAR)
				{
					tileItemElement.Text = item.item_Name;
					if (LoginDetails.ItemNameAR == "TopLeft")
					{
						tileItemElement.TextAlignment = TileItemContentAlignment.TopLeft;
					}
					else if (LoginDetails.ItemNameAR == "MiddleCenter")
					{
						tileItemElement.TextAlignment = TileItemContentAlignment.MiddleCenter;
					}
					else if (LoginDetails.ItemNameAR == "BottomRight")
					{
						tileItemElement.TextAlignment = TileItemContentAlignment.BottomRight;
					}
				}
				if (LoginDetails.IsItemNameEN)
				{
					tileItemElement2.Text = item.item_Name_English.ToString();
					if (LoginDetails.ItemNameEN == "TopLeft")
					{
						tileItemElement2.TextAlignment = TileItemContentAlignment.TopLeft;
					}
					else if (LoginDetails.ItemNameEN == "MiddleCenter")
					{
						tileItemElement2.TextAlignment = TileItemContentAlignment.MiddleCenter;
					}
					else if (LoginDetails.ItemNameEN == "BottomRight")
					{
						tileItemElement2.TextAlignment = TileItemContentAlignment.BottomRight;
					}
				}
				if (LoginDetails.IsItemShowPrice)
				{
					Task.Run(delegate
					{
						decimal num = Convert.ToDecimal(item.SellPriceSmall);
						decimal num2 = Convert.ToDecimal(item.Tax_Value);
						decimal num3 = Math.Round(num * (num2 / 100m + 1m), DecimalNum);
						decimal num4 = Math.Round(num, DecimalNum);
						bool valueOrDefault = item.Is_Tax == true;
						decimal finalPrice = ((!isVatActive || LoginDetails.ItemTotalWithVat) ? num4 : ((isVatActive & valueOrDefault) ? num3 : num4));
						Invoke((MethodInvoker)delegate
						{
							if (SellPrice.Text != finalPrice.ToString())
							{
								SellPrice.Text = finalPrice.ToString();
							}
						});
					});
					if (LoginDetails.ItemShowPrice == "TopLeft")
					{
						SellPrice.TextAlignment = TileItemContentAlignment.TopLeft;
					}
					else if (LoginDetails.ItemShowPrice == "MiddleCenter")
					{
						SellPrice.TextAlignment = TileItemContentAlignment.MiddleCenter;
					}
					else if (LoginDetails.ItemShowPrice == "BottomRight")
					{
						SellPrice.TextAlignment = TileItemContentAlignment.BottomRight;
					}
				}
				if (LoginDetails.IsImage && !string.IsNullOrEmpty(item.Image))
				{
					Image img = (tileItemElement3.Image = Base64ToImage(item.Image));
					tileItemElement3.ImageAlignment = TileItemContentAlignment.MiddleLeft;
					tileItemElement3.ImageScaleMode = TileItemImageScaleMode.Stretch;
					if (LoginDetails.ImgShape)
					{
						tileItemElement3.Image = MakeSquareImage(img);
						tileItemElement3.ImageAlignment = TileItemContentAlignment.MiddleRight;
						tileItemElement3.ImageScaleMode = TileItemImageScaleMode.ZoomInside;
						tileItemElement.TextAlignment = TileItemContentAlignment.TopLeft;
						tileItemElement.Appearance.Normal.TextOptions.WordWrap = WordWrap.Wrap;
						tileItemElement.MaxWidth = 100;
						tileItemElement2.TextAlignment = TileItemContentAlignment.TopLeft;
						tileItemElement2.Appearance.Normal.TextOptions.WordWrap = WordWrap.Wrap;
						tileItemElement2.MaxWidth = 100;
						SellPrice.TextAlignment = TileItemContentAlignment.BottomLeft;
					}
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void BtnDisCode_ButtonClick_1(object sender, ButtonPressedEventArgs e)
	{
		DiscountCode();
	}

	public void insertnewrow()
	{
		CheckFfistRow();
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["ItemID"] = "0";
		dataRow["ItemCode"] = string.Empty;
		dataRow["ItemName"] = string.Empty;
		dataRow["Group"] = 1;
		dataRow["UnitName"] = string.Empty;
		dataRow["UnitPrice"] = 0;
		dataRow["Quantity"] = FUNC_DecimalNumQty(1);
		dataRow["StoreName"] = Comp_Store.Text;
		dataRow["StoreID"] = Comp_Store.SelectedValue;
		dataRow["Done"] = 0;
		dataRow["Height"] = 1;
		dataRow["Width"] = 1;
		dataRow["Long"] = 1;
		dataRow["Weight"] = 1;
		dataRow["AmountDisc"] = 0;
		dataRow["PercentDisc"] = 0;
		dataRow["InstallationPrice"] = 0;
		dataRow["InstallPriceDis"] = 0;
		dataRow["StoreName"] = Comp_Store.GetItemText(Comp_Store.SelectedItem);
		if (Comp_Store.SelectedValue == null)
		{
			dataRow["StoreID"] = DBNull.Value;
		}
		else
		{
			dataRow["StoreID"] = Comp_Store.SelectedValue;
		}
		dataRow["OrginalPrice"] = 0;
		dataRow["SumPrice"] = 0;
		dataRow["ItemVat"] = 0;
		dataRow["Total"] = 0;
		dataRow["PercentDisc"] = 0;
		dataRow["SumPriceDisc"] = 0;
		dataRow["ItemVatDisc"] = 0;
		dataRow["Net"] = 0;
		dataRow["AvergItem"] = 0;
		dataRow["SumAvergItem"] = 0;
		dataRow["Done"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
	}

	public void InsertNewItem(string itemCode)
	{
		bool flag = false;
		Get_Item_ByCodeALL_Result ItemDetails = ITM.GetItemByCode(itemCode).FirstOrDefault();
		Printer printer = ORD.GetPrinter(ItemDetails.ItemId);
		foreach (DataRow row in datatable.Rows)
		{
			string item_code = ItemDetails.Item_code;
			string text = row["ItemCode"].ToString();
			if (!(item_code == text) || !LoginDetails.AlLItemInRow || row["UnitType"] != "الصغرى")
			{
				continue;
			}
			row["VatCode"] = ItemDetails.VatCode;
			row["TaxExemptionReason"] = ItemDetails.TaxExemptionReason;
			row["TaxExemptionReasonCode"] = ItemDetails.TaxExemptionReasonCode;
			row["StoreName"] = Comp_Store.Text;
			row["StoreID"] = Comp_Store.SelectedValue;
			row["Quantity"] = FUNC_DecimalNumQty(Convert.ToDecimal(row["Quantity"]) + 1m);
			ItmID2 = row["ItemCode"].ToString();
			decimal num = Math.Round((Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["BounceItem"])) * (Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"])), DecimalNum);
			decimal num2 = Math.Round(Convert.ToDecimal(row["Quantity"]) * (Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"])), DecimalNum);
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			if (LoginDetails.ItemTotalWithVat)
			{
				decimal num3 = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
				if (toggleSectoral.IsOn)
				{
					decimal num4 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["Total"] = Math.Round(Convert.ToDecimal(num4 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["SumPrice"] = Math.Round(Convert.ToDecimal(row["Total"]) / num3, DecimalNum);
				}
				else
				{
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 / (decimal?)num3);
					decimal num5 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["Total"] = Math.Round(Convert.ToDecimal(num5 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["SumPrice"] = Math.Round(Convert.ToDecimal(row["Total"]) / num3, DecimalNum);
				}
				VatWithTotal(ItemDetails.Is_Tax, num3, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Total"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
			}
			else
			{
				decimal num6 = Convert.ToDecimal(row["UnitPrice"]) + Convert.ToDecimal(row["InstallationPrice"]);
				if (toggleSectoral.IsOn)
				{
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall);
				}
				else
				{
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2);
				}
				decimal d = num6 * num2;
				row["Total"] = Math.Round(d, DecimalNum);
				row["OrginalPrice"] = row["Total"];
				Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num6, Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
			}
			ArrangeDatatable();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			gridControl1.DataSource = datatable;
			SoundPlayer soundPlayer = new SoundPlayer("MsgSent.WAV");
			soundPlayer.Play();
			flag = true;
			TxtBarcode.Focus();
			break;
		}
		if (!LoginDetails.AlLItemInRow)
		{
			Item_Items item_Items = (from x in ITM.GetItemByCode()
				where x.Item_code == ItemDetails.Item_code
				select x).FirstOrDefault();
			DataRow dataRow2 = null;
			dataRow2 = datatable.NewRow();
			dataRow2["ItemID"] = ItemDetails.ItemId;
			dataRow2["ItemUnitID"] = ItemDetails.UnitSmallID;
			dataRow2["PrinterID"] = printer.ID;
			dataRow2["PrinterName"] = printer.Name;
			dataRow2["ItemCode"] = ItemDetails.Item_code;
			dataRow2["ItemName"] = ItemDetails.item_Name;
			dataRow2["Group"] = ItemDetails.Category_ID;
			dataRow2["UnitName"] = ItemDetails.UnitSmall;
			dataRow2["Quantity"] = FUNC_DecimalNumQty(1);
			dataRow2["InstallationPrice"] = FUNC_DecimalNum(item_Items.PriceExportunit1);
			dataRow2["InstallPriceDis"] = dataRow2["InstallationPrice"];
			dataRow2["Delete"] = 0;
			dataRow2["Done"] = 1;
			dataRow2["UnitType"] = "الصغرى";
			dataRow2["AvergItem"] = ItemDetails.Average_cost.GetValueOrDefault();
			dataRow2["AvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["AvergItem"]));
			dataRow2["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow2["AvergItem"]) * Convert.ToDecimal(dataRow2["Quantity"]), DecimalNum);
			dataRow2["StoreName"] = ORD.GetStoreName();
			dataRow2["StoreID"] = ORD.GetStoreID();
			dataRow2["StoreName"] = Comp_Store.Text;
			dataRow2["StoreID"] = Comp_Store.SelectedValue;
			dataRow2["BounceItem"] = 0;
			dataRow2["Height"] = 1;
			dataRow2["Width"] = 1;
			dataRow2["Long"] = 1;
			dataRow2["Weight"] = 1;
			dataRow2["AmountDisc"] = 0;
			dataRow2["PercentDisc"] = 0;
			dataRow2["VatCode"] = ItemDetails.VatCode;
			dataRow2["TaxExemptionReason"] = ItemDetails.TaxExemptionReason;
			dataRow2["TaxExemptionReasonCode"] = ItemDetails.TaxExemptionReasonCode;
			decimal num7 = FUNC_DecimalNum(ItemDetails.SellPriceSmall + item_Items.PriceExportunit1);
			if (LoginDetails.ItemTotalWithVat)
			{
				decimal num8;
				decimal num9;
				if (ItemDetails.Is_Tax == true)
				{
					num8 = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
					num9 = ItemDetails.SellPriceSmall.GetValueOrDefault() / num8 + item_Items.PriceExportunit1.GetValueOrDefault();
				}
				else
				{
					num8 = 0m;
					num9 = ItemDetails.SellPriceSmall.GetValueOrDefault() + item_Items.PriceExportunit1.GetValueOrDefault();
				}
				if (toggleSectoral.IsOn)
				{
					if (ItemDetails.Is_Tax == true)
					{
						dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall / (decimal?)num8);
					}
					else
					{
						dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall);
					}
					dataRow2["Total"] = num9;
					dataRow2["OrginalPrice"] = dataRow2["Total"];
					VatWithTotal(ItemDetails.Is_Tax, num8, num9, Convert.ToDecimal(dataRow2["Total"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
				else
				{
					decimal num10 = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 / (decimal?)num8 + item_Items.PriceExportunit1);
					dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 / (decimal?)num8);
					dataRow2["Total"] = num10;
					dataRow2["OrginalPrice"] = dataRow2["Total"];
					VatWithTotal(ItemDetails.Is_Tax, num8, num10, Convert.ToDecimal(dataRow2["Total"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
			}
			else
			{
				if (toggleSectoral.IsOn)
				{
					dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall);
					dataRow2["SumPrice"] = num7;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num7, Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
				else
				{
					decimal num11 = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 + item_Items.PriceExportunit1);
					dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2);
					dataRow2["SumPrice"] = num11;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num11, Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
				dataRow2["OrginalPrice"] = dataRow2["SumPrice"];
			}
			datatable.Rows.Add(dataRow2);
			ArrangeDatatable();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			gridControl1.DataSource = datatable;
			SoundPlayer soundPlayer2 = new SoundPlayer("MsgSent.wav");
			soundPlayer2.Play();
			TxtBarcode.Focus();
			flag = true;
		}
		if (!flag)
		{
			Item_Items item_Items2 = (from x in ITM.GetItemByCode()
				where x.Item_code == ItemDetails.Item_code
				select x).FirstOrDefault();
			DataRow dataRow3 = null;
			dataRow3 = datatable.NewRow();
			dataRow3["ItemID"] = ItemDetails.ItemId;
			dataRow3["ItemUnitID"] = ItemDetails.UnitSmallID;
			dataRow3["PrinterID"] = printer.ID;
			dataRow3["PrinterName"] = printer.Name;
			dataRow3["ItemCode"] = ItemDetails.Item_code;
			dataRow3["ItemName"] = ItemDetails.item_Name;
			dataRow3["Group"] = ItemDetails.Category_ID;
			dataRow3["UnitName"] = ItemDetails.UnitSmall;
			dataRow3["Quantity"] = FUNC_DecimalNumQty(1);
			dataRow3["InstallationPrice"] = FUNC_DecimalNum(item_Items2.PriceExportunit1);
			dataRow3["InstallPriceDis"] = dataRow3["InstallationPrice"];
			dataRow3["Delete"] = 0;
			dataRow3["Done"] = 1;
			dataRow3["UnitType"] = "الصغرى";
			dataRow3["AvergItem"] = ItemDetails.Average_cost.GetValueOrDefault();
			dataRow3["AvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow3["AvergItem"]));
			dataRow3["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow3["AvergItem"]) * Convert.ToDecimal(dataRow3["Quantity"]), DecimalNum);
			dataRow3["StoreName"] = ORD.GetStoreName();
			dataRow3["StoreID"] = ORD.GetStoreID();
			dataRow3["StoreName"] = Comp_Store.Text;
			dataRow3["StoreID"] = Comp_Store.SelectedValue;
			dataRow3["BounceItem"] = 0;
			dataRow3["Height"] = 1;
			dataRow3["Width"] = 1;
			dataRow3["Long"] = 1;
			dataRow3["Weight"] = 1;
			dataRow3["AmountDisc"] = 0;
			dataRow3["PercentDisc"] = 0;
			dataRow3["VatCode"] = ItemDetails.VatCode;
			dataRow3["TaxExemptionReason"] = ItemDetails.TaxExemptionReason;
			dataRow3["TaxExemptionReasonCode"] = ItemDetails.TaxExemptionReasonCode;
			decimal num12 = FUNC_DecimalNum(ItemDetails.SellPriceSmall + item_Items2.PriceExportunit1);
			if (LoginDetails.ItemTotalWithVat)
			{
				decimal num13;
				decimal num14;
				if (ItemDetails.Is_Tax == true)
				{
					num13 = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
					num14 = ItemDetails.SellPriceSmall.GetValueOrDefault() / num13 + item_Items2.PriceExportunit1.GetValueOrDefault();
				}
				else
				{
					num13 = 0m;
					num14 = ItemDetails.SellPriceSmall.GetValueOrDefault() + item_Items2.PriceExportunit1.GetValueOrDefault();
				}
				if (toggleSectoral.IsOn)
				{
					if (ItemDetails.Is_Tax == true)
					{
						dataRow3["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall / (decimal?)num13);
					}
					else
					{
						dataRow3["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall);
					}
					dataRow3["Total"] = num14;
					dataRow3["OrginalPrice"] = dataRow3["Total"];
					VatWithTotal(ItemDetails.Is_Tax, num13, num14, Convert.ToDecimal(dataRow3["Total"]), Convert.ToDecimal(dataRow3["Quantity"]), dataRow3, "ItemCode");
				}
				else
				{
					decimal num15 = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 / (decimal?)num13 + item_Items2.PriceExportunit1);
					dataRow3["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 / (decimal?)num13);
					dataRow3["Total"] = num15;
					dataRow3["OrginalPrice"] = dataRow3["Total"];
					VatWithTotal(ItemDetails.Is_Tax, num13, num15, Convert.ToDecimal(dataRow3["Total"]), Convert.ToDecimal(dataRow3["Quantity"]), dataRow3, "ItemCode");
				}
			}
			else
			{
				if (toggleSectoral.IsOn)
				{
					dataRow3["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall);
					dataRow3["SumPrice"] = num12;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num12, Convert.ToDecimal(dataRow3["Quantity"]), dataRow3, "ItemCode");
				}
				else
				{
					decimal num16 = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 + item_Items2.PriceExportunit1);
					dataRow3["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2);
					dataRow3["SumPrice"] = num16;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num16, Convert.ToDecimal(dataRow3["Quantity"]), dataRow3, "ItemCode");
				}
				dataRow3["OrginalPrice"] = dataRow3["SumPrice"];
			}
			datatable.Rows.Add(dataRow3);
			ArrangeDatatable();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			gridControl1.DataSource = datatable;
			SoundPlayer soundPlayer3 = new SoundPlayer("MsgSent.wav");
			soundPlayer3.Play();
			TxtBarcode.Focus();
		}
		if (LoginDetails.DisPerCashier > 0m)
		{
			txtdicoutprege.Text = LoginDetails.DisPerCashier.ToString();
			txtdicoutprege_KeyUp(txtdicoutprege, new KeyEventArgs(Keys.None));
		}
	}

	private void Insert()
	{
	}

	public void InsertNewItemUnitMedium(string itemCode)
	{
		bool flag = false;
		Get_Item_ByCodeALL_Result ItemDetails = ITM.GetItemByCode(itemCode).FirstOrDefault();
		Printer printer = ORD.GetPrinter(ItemDetails.ItemId);
		foreach (DataRow row in datatable.Rows)
		{
			if (row["UnitType"] != "المتوسطة")
			{
				continue;
			}
			string item_code = ItemDetails.Item_code;
			string text = row["ItemCode"].ToString();
			if (!(item_code == text))
			{
				continue;
			}
			row["VatCode"] = ItemDetails.VatCode;
			row["TaxExemptionReason"] = ItemDetails.TaxExemptionReason;
			row["TaxExemptionReasonCode"] = ItemDetails.TaxExemptionReasonCode;
			row["StoreName"] = Comp_Store.Text;
			row["StoreID"] = Comp_Store.SelectedValue;
			row["Quantity"] = FUNC_DecimalNumQty(Convert.ToDecimal(row["Quantity"]) + 1m);
			ItmID2 = row["ItemCode"].ToString();
			decimal num = Math.Round((Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["BounceItem"])) * (Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"])), DecimalNum);
			decimal num2 = Math.Round(Convert.ToDecimal(row["Quantity"]) * (Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"])), DecimalNum);
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			if (LoginDetails.ItemTotalWithVat)
			{
				decimal num3 = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
				if (toggleSectoral.IsOn)
				{
					decimal num4 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["Total"] = Math.Round(Convert.ToDecimal(num4 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["SumPrice"] = Math.Round(Convert.ToDecimal(row["Total"]) / num3, DecimalNum);
				}
				else
				{
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 / (decimal?)num3);
					decimal num5 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["Total"] = Math.Round(Convert.ToDecimal(num5 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["SumPrice"] = Math.Round(Convert.ToDecimal(row["Total"]) / num3, DecimalNum);
				}
				VatWithTotal(ItemDetails.Is_Tax, num3, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Total"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
			}
			else
			{
				decimal num6 = Convert.ToDecimal(row["UnitPrice"]) + Convert.ToDecimal(row["InstallationPrice"]);
				if (toggleSectoral.IsOn)
				{
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall);
				}
				else
				{
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2);
				}
				decimal d = num6 * num2;
				row["Total"] = Math.Round(d, DecimalNum);
				row["OrginalPrice"] = row["Total"];
				Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num6, Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
			}
			ArrangeDatatable();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			gridControl1.DataSource = datatable;
			SoundPlayer soundPlayer = new SoundPlayer("MsgSent.WAV");
			soundPlayer.Play();
			flag = true;
			TxtBarcode.Focus();
			break;
		}
		if (!flag)
		{
			Item_Items item_Items = (from x in ITM.GetItemByCode()
				where x.Item_code == ItemDetails.Item_code
				select x).FirstOrDefault();
			DataRow dataRow2 = null;
			dataRow2 = datatable.NewRow();
			dataRow2["ItemID"] = ItemDetails.ItemId;
			dataRow2["ItemUnitID"] = ItemDetails.UnitMediumID;
			dataRow2["PrinterID"] = printer.ID;
			dataRow2["PrinterName"] = printer.Name;
			dataRow2["ItemCode"] = ItemDetails.Item_code;
			dataRow2["ItemName"] = ItemDetails.item_Name;
			dataRow2["Group"] = ItemDetails.Category_ID;
			dataRow2["UnitName"] = ItemDetails.UnitMedium;
			dataRow2["Quantity"] = FUNC_DecimalNumQty(1);
			dataRow2["InstallationPrice"] = FUNC_DecimalNum(item_Items.PriceExportunit1);
			dataRow2["InstallPriceDis"] = dataRow2["InstallationPrice"];
			dataRow2["Delete"] = 0;
			dataRow2["Done"] = 1;
			dataRow2["UnitType"] = "المتوسطة";
			dataRow2["AvergItem"] = ItemDetails.Average_cost.GetValueOrDefault();
			dataRow2["AvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["AvergItem"]));
			dataRow2["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow2["AvergItem"]) * Convert.ToDecimal(dataRow2["Quantity"]), DecimalNum);
			dataRow2["StoreName"] = ORD.GetStoreName();
			dataRow2["StoreID"] = ORD.GetStoreID();
			dataRow2["StoreName"] = Comp_Store.Text;
			dataRow2["StoreID"] = Comp_Store.SelectedValue;
			dataRow2["BounceItem"] = 0;
			dataRow2["Height"] = 1;
			dataRow2["Width"] = 1;
			dataRow2["Long"] = 1;
			dataRow2["Weight"] = 1;
			dataRow2["AmountDisc"] = 0;
			dataRow2["PercentDisc"] = 0;
			dataRow2["VatCode"] = ItemDetails.VatCode;
			dataRow2["TaxExemptionReason"] = ItemDetails.TaxExemptionReason;
			dataRow2["TaxExemptionReasonCode"] = ItemDetails.TaxExemptionReasonCode;
			decimal num7 = Convert.ToDecimal(ItemDetails.ConvertMediumUnit);
			dataRow2["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow2["AvergItem"]) * (Convert.ToDecimal(dataRow2["Quantity"]) * num7), DecimalNumQty);
			dataRow2["AvergItem"] = FUNC_DecimalNum(ItemDetails.Average_cost * ItemDetails.ConvertMediumUnit);
			dataRow2["SumAvergItem"] = ItemDetails.Average_cost * ItemDetails.ConvertMediumUnit;
			decimal num8 = FUNC_DecimalNum(ItemDetails.SellPriceMedium + item_Items.PriceExportunit1);
			if (LoginDetails.ItemTotalWithVat)
			{
				decimal num9;
				decimal num10;
				if (ItemDetails.Is_Tax == true)
				{
					num9 = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
					num10 = ItemDetails.SellPriceMedium.GetValueOrDefault() / num9 + item_Items.PriceExportunit1.GetValueOrDefault();
				}
				else
				{
					num9 = 0m;
					num10 = ItemDetails.SellPriceMedium.GetValueOrDefault() + item_Items.PriceExportunit1.GetValueOrDefault();
				}
				if (toggleSectoral.IsOn)
				{
					if (ItemDetails.Is_Tax == true)
					{
						dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceMedium / (decimal?)num9);
					}
					else
					{
						dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceMedium);
					}
					dataRow2["Total"] = num10;
					dataRow2["OrginalPrice"] = dataRow2["Total"];
					VatWithTotal(ItemDetails.Is_Tax, num9, num10, Convert.ToDecimal(dataRow2["Total"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
				else
				{
					decimal num11 = FUNC_DecimalNum(ItemDetails.SellPriceMedium2 / (decimal?)num9 + item_Items.PriceExportunit1);
					dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceMedium2 / (decimal?)num9);
					dataRow2["Total"] = num11;
					dataRow2["OrginalPrice"] = dataRow2["Total"];
					VatWithTotal(ItemDetails.Is_Tax, num9, num11, Convert.ToDecimal(dataRow2["Total"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
			}
			else
			{
				if (toggleSectoral.IsOn)
				{
					dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceMedium);
					dataRow2["SumPrice"] = num8;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num8, Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
				else
				{
					decimal num12 = FUNC_DecimalNum(ItemDetails.SellPriceMedium2 + item_Items.PriceExportunit1);
					dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceMedium2);
					dataRow2["SumPrice"] = num12;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num12, Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
				dataRow2["OrginalPrice"] = dataRow2["SumPrice"];
			}
			datatable.Rows.Add(dataRow2);
			ArrangeDatatable();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			gridControl1.DataSource = datatable;
			SoundPlayer soundPlayer2 = new SoundPlayer("MsgSent.wav");
			soundPlayer2.Play();
			TxtBarcode.Focus();
		}
		if (LoginDetails.DisPerCashier > 0m)
		{
			txtdicoutprege.Text = LoginDetails.DisPerCashier.ToString();
			txtdicoutprege_KeyUp(txtdicoutprege, new KeyEventArgs(Keys.None));
		}
	}

	public void InsertNewItemUnitLarg(string itemCode)
	{
		bool flag = false;
		Get_Item_ByCodeALL_Result ItemDetails = ITM.GetItemByCode(itemCode).FirstOrDefault();
		Printer printer = ORD.GetPrinter(ItemDetails.ItemId);
		foreach (DataRow row in datatable.Rows)
		{
			if (row["UnitType"] != "الكبرى")
			{
				continue;
			}
			string item_code = ItemDetails.Item_code;
			string text = row["ItemCode"].ToString();
			if (!(item_code == text))
			{
				continue;
			}
			row["VatCode"] = ItemDetails.VatCode;
			row["TaxExemptionReason"] = ItemDetails.TaxExemptionReason;
			row["TaxExemptionReasonCode"] = ItemDetails.TaxExemptionReasonCode;
			row["StoreName"] = Comp_Store.Text;
			row["StoreID"] = Comp_Store.SelectedValue;
			row["Quantity"] = FUNC_DecimalNumQty(Convert.ToDecimal(row["Quantity"]) + 1m);
			ItmID2 = row["ItemCode"].ToString();
			decimal num = Math.Round((Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["BounceItem"])) * (Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"])), DecimalNum);
			decimal num2 = Math.Round(Convert.ToDecimal(row["Quantity"]) * (Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"])), DecimalNum);
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			if (LoginDetails.ItemTotalWithVat)
			{
				decimal num3 = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
				if (toggleSectoral.IsOn)
				{
					decimal num4 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["Total"] = Math.Round(Convert.ToDecimal(num4 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["SumPrice"] = Math.Round(Convert.ToDecimal(row["Total"]) / num3, DecimalNum);
				}
				else
				{
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 / (decimal?)num3);
					decimal num5 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["Total"] = Math.Round(Convert.ToDecimal(num5 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["SumPrice"] = Math.Round(Convert.ToDecimal(row["Total"]) / num3, DecimalNum);
				}
				VatWithTotal(ItemDetails.Is_Tax, num3, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Total"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
			}
			else
			{
				decimal num6 = Convert.ToDecimal(row["UnitPrice"]) + Convert.ToDecimal(row["InstallationPrice"]);
				if (toggleSectoral.IsOn)
				{
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall);
				}
				else
				{
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2);
				}
				decimal d = num6 * num2;
				row["Total"] = Math.Round(d, DecimalNum);
				row["OrginalPrice"] = row["Total"];
				Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num6, Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
			}
			ArrangeDatatable();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			gridControl1.DataSource = datatable;
			SoundPlayer soundPlayer = new SoundPlayer("MsgSent.WAV");
			soundPlayer.Play();
			flag = true;
			TxtBarcode.Focus();
			break;
		}
		if (!flag)
		{
			Item_Items item_Items = (from x in ITM.GetItemByCode()
				where x.Item_code == ItemDetails.Item_code
				select x).FirstOrDefault();
			DataRow dataRow2 = null;
			dataRow2 = datatable.NewRow();
			dataRow2["ItemID"] = ItemDetails.ItemId;
			dataRow2["ItemUnitID"] = ItemDetails.UnitLargeID;
			dataRow2["PrinterID"] = printer.ID;
			dataRow2["PrinterName"] = printer.Name;
			dataRow2["ItemCode"] = ItemDetails.Item_code;
			dataRow2["ItemName"] = ItemDetails.item_Name;
			dataRow2["Group"] = ItemDetails.Category_ID;
			dataRow2["UnitName"] = ItemDetails.UnitLarge;
			dataRow2["Quantity"] = FUNC_DecimalNumQty(1);
			dataRow2["InstallationPrice"] = FUNC_DecimalNum(item_Items.PriceExportunit1);
			dataRow2["InstallPriceDis"] = dataRow2["InstallationPrice"];
			dataRow2["Delete"] = 0;
			dataRow2["Done"] = 1;
			dataRow2["UnitType"] = "الكبرى";
			dataRow2["AvergItem"] = ItemDetails.Average_cost.GetValueOrDefault();
			dataRow2["AvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["AvergItem"]));
			dataRow2["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow2["AvergItem"]) * Convert.ToDecimal(dataRow2["Quantity"]), DecimalNum);
			dataRow2["StoreName"] = ORD.GetStoreName();
			dataRow2["StoreID"] = ORD.GetStoreID();
			dataRow2["StoreName"] = Comp_Store.Text;
			dataRow2["StoreID"] = Comp_Store.SelectedValue;
			dataRow2["BounceItem"] = 0;
			dataRow2["Height"] = 1;
			dataRow2["Width"] = 1;
			dataRow2["Long"] = 1;
			dataRow2["Weight"] = 1;
			dataRow2["AmountDisc"] = 0;
			dataRow2["PercentDisc"] = 0;
			dataRow2["VatCode"] = ItemDetails.VatCode;
			dataRow2["TaxExemptionReason"] = ItemDetails.TaxExemptionReason;
			dataRow2["TaxExemptionReasonCode"] = ItemDetails.TaxExemptionReasonCode;
			decimal num7 = Convert.ToDecimal(ItemDetails.ConvertMediumUnit * ItemDetails.ConvertBigUnit);
			dataRow2["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow2["AvergItem"]) * (Convert.ToDecimal(dataRow2["Quantity"]) * num7), DecimalNumQty);
			dataRow2["AvergItem"] = FUNC_DecimalNum(ItemDetails.Average_cost * ItemDetails.ConvertMediumUnit * ItemDetails.ConvertBigUnit);
			dataRow2["SumAvergItem"] = ItemDetails.Average_cost * ItemDetails.ConvertMediumUnit * ItemDetails.ConvertBigUnit;
			decimal num8 = FUNC_DecimalNum(ItemDetails.SellpriceLarge + item_Items.PriceExportunit1);
			if (LoginDetails.ItemTotalWithVat)
			{
				decimal num9;
				decimal num10;
				if (ItemDetails.Is_Tax == true)
				{
					num9 = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
					num10 = ItemDetails.SellpriceLarge.GetValueOrDefault() / num9 + item_Items.PriceExportunit1.GetValueOrDefault();
				}
				else
				{
					num9 = 0m;
					num10 = ItemDetails.SellpriceLarge.GetValueOrDefault() + item_Items.PriceExportunit1.GetValueOrDefault();
				}
				if (toggleSectoral.IsOn)
				{
					if (ItemDetails.Is_Tax == true)
					{
						dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellpriceLarge / (decimal?)num9);
					}
					else
					{
						dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellpriceLarge);
					}
					dataRow2["Total"] = num10;
					dataRow2["OrginalPrice"] = dataRow2["Total"];
					VatWithTotal(ItemDetails.Is_Tax, num9, num10, Convert.ToDecimal(dataRow2["Total"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
				else
				{
					decimal num11 = FUNC_DecimalNum(ItemDetails.SellpriceLarge2 / (decimal?)num9 + item_Items.PriceExportunit1);
					dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellpriceLarge2 / (decimal?)num9);
					dataRow2["Total"] = num11;
					dataRow2["OrginalPrice"] = dataRow2["Total"];
					VatWithTotal(ItemDetails.Is_Tax, num9, num11, Convert.ToDecimal(dataRow2["Total"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
			}
			else
			{
				if (toggleSectoral.IsOn)
				{
					dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellpriceLarge);
					dataRow2["SumPrice"] = num8;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num8, Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
				else
				{
					decimal num12 = FUNC_DecimalNum(ItemDetails.SellpriceLarge2 + item_Items.PriceExportunit1);
					dataRow2["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellpriceLarge2);
					dataRow2["SumPrice"] = num12;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num12, Convert.ToDecimal(dataRow2["Quantity"]), dataRow2, "ItemCode");
				}
				dataRow2["OrginalPrice"] = dataRow2["SumPrice"];
			}
			datatable.Rows.Add(dataRow2);
			ArrangeDatatable();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			gridControl1.DataSource = datatable;
			SoundPlayer soundPlayer2 = new SoundPlayer("MsgSent.wav");
			soundPlayer2.Play();
			TxtBarcode.Focus();
		}
		if (LoginDetails.DisPerCashier > 0m)
		{
			txtdicoutprege.Text = LoginDetails.DisPerCashier.ToString();
			txtdicoutprege_KeyUp(txtdicoutprege, new KeyEventArgs(Keys.None));
		}
	}

	public void InsertNewItemUnitSmall(string itemCode)
	{
		Get_Item_ByCodeALL_Result ItemDetails = ITM.GetItemByCode(itemCode).FirstOrDefault();
		Item_Items item_Items = (from x in ITM.GetItemByCode()
			where x.Item_code == ItemDetails.Item_code
			select x).FirstOrDefault();
		Printer printer = ORD.GetPrinter(ItemDetails.ItemId);
		if (!LoginDetails.AlLItemInRow)
		{
			DataRow dataRow = datatable.NewRow();
			dataRow["ItemID"] = ItemDetails.ItemId;
			dataRow["ItemUnitID"] = ItemDetails.UnitSmallID;
			dataRow["PrinterID"] = printer.ID;
			dataRow["PrinterName"] = printer.Name;
			dataRow["ItemCode"] = ItemDetails.Item_code;
			dataRow["ItemName"] = ItemDetails.item_Name;
			dataRow["Group"] = ItemDetails.Category_ID;
			dataRow["UnitName"] = ItemDetails.UnitSmall;
			dataRow["Tax_Value"] = ItemDetails.Tax_Value;
			dataRow["Quantity"] = FUNC_DecimalNumQty(1);
			dataRow["InstallationPrice"] = FUNC_DecimalNum(item_Items.PriceExportunit1);
			dataRow["InstallPriceDis"] = dataRow["InstallationPrice"];
			dataRow["Delete"] = 0;
			dataRow["Done"] = 1;
			dataRow["UnitType"] = "الصغرى";
			dataRow["AvergItem"] = ItemDetails.Average_cost.GetValueOrDefault();
			dataRow["AvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["AvergItem"]));
			dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
			dataRow["StoreName"] = ORD.GetStoreName();
			dataRow["StoreID"] = ORD.GetStoreID();
			dataRow["StoreName"] = Comp_Store.Text;
			dataRow["StoreID"] = Comp_Store.SelectedValue;
			dataRow["BounceItem"] = 0;
			dataRow["Height"] = 1;
			dataRow["Width"] = 1;
			dataRow["Long"] = 1;
			dataRow["Weight"] = 1;
			dataRow["AmountDisc"] = 0;
			dataRow["PercentDisc"] = 0;
			dataRow["VatCode"] = ItemDetails.VatCode;
			dataRow["TaxExemptionReason"] = ItemDetails.TaxExemptionReason;
			dataRow["TaxExemptionReasonCode"] = ItemDetails.TaxExemptionReasonCode;
			decimal num = FUNC_DecimalNum(ItemDetails.SellPriceSmall + item_Items.PriceExportunit1);
			if (LoginDetails.ItemTotalWithVat)
			{
				decimal value = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
				decimal num2 = ItemDetails.SellPriceSmall.GetValueOrDefault() + item_Items.PriceExportunit1.GetValueOrDefault();
				if (toggleSectoral.IsOn)
				{
					dataRow["IsItemTax"] = true;
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall / (decimal?)value);
					dataRow["Total"] = num2;
					dataRow["OrginalPrice"] = ItemDetails.SellPriceSmall / (decimal?)value;
				}
				else
				{
					dataRow["IsItemTax"] = true;
					decimal num3 = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 / (decimal?)value + item_Items.PriceExportunit1);
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 / (decimal?)value);
					dataRow["Total"] = num3;
					dataRow["OrginalPrice"] = ItemDetails.SellPriceSmall2 / (decimal?)value;
				}
			}
			else
			{
				if (toggleSectoral.IsOn)
				{
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall);
					dataRow["SumPrice"] = num;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num, Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
				}
				else
				{
					decimal num4 = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 + item_Items.PriceExportunit1);
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2);
					dataRow["SumPrice"] = num4;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num4, Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
				}
				dataRow["OrginalPrice"] = dataRow["SumPrice"];
			}
			datatable.Rows.Add(dataRow);
		}
		else
		{
			InsertNewItem(ItemDetails.Item_code);
		}
		ArrangeDatatable();
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
		gridControl1.DataSource = datatable;
		SoundPlayer soundPlayer = new SoundPlayer("MsgSent.wav");
		soundPlayer.Play();
		TxtBarcode.Focus();
		if (LoginDetails.DisPerCashier > 0m)
		{
			txtdicoutprege.Text = LoginDetails.DisPerCashier.ToString();
			txtdicoutprege_KeyUp(txtdicoutprege, new KeyEventArgs(Keys.None));
		}
	}

	public void InsertNewItemMedium(string itemCode)
	{
		Get_Item_ByCodeALL_Result ItemDetails = ITM.GetItemByCode(itemCode).FirstOrDefault();
		Item_Items item_Items = (from x in ITM.GetItemByCode()
			where x.Item_code == ItemDetails.Item_code
			select x).FirstOrDefault();
		string unitNameById = ITM.GetUnitNameById(Convert.ToInt32(item_Items.UnitMedium));
		Printer printer = ORD.GetPrinter(ItemDetails.ItemId);
		if (!LoginDetails.AlLItemInRow)
		{
			DataRow dataRow = datatable.NewRow();
			dataRow["ItemID"] = ItemDetails.ItemId;
			dataRow["ItemUnitID"] = ItemDetails.UnitMediumID;
			dataRow["PrinterID"] = printer.ID;
			dataRow["PrinterName"] = printer.Name;
			dataRow["ItemCode"] = ItemDetails.Item_code;
			dataRow["ItemName"] = ItemDetails.item_Name;
			dataRow["Group"] = ItemDetails.Category_ID;
			dataRow["UnitName"] = unitNameById;
			dataRow["Quantity"] = FUNC_DecimalNumQty(1);
			dataRow["InstallationPrice"] = FUNC_DecimalNum(ItemDetails.PriceExportunit1);
			dataRow["InstallPriceDis"] = dataRow["InstallationPrice"];
			dataRow["Delete"] = 0;
			dataRow["Done"] = 1;
			dataRow["UnitType"] = "المتوسطة";
			dataRow["AvergItem"] = ItemDetails.Average_cost.GetValueOrDefault();
			dataRow["AvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["AvergItem"]));
			dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
			dataRow["StoreName"] = ORD.GetStoreName();
			dataRow["StoreID"] = ORD.GetStoreID();
			dataRow["StoreName"] = Comp_Store.Text;
			dataRow["StoreID"] = Comp_Store.SelectedValue;
			dataRow["BounceItem"] = 0;
			dataRow["Height"] = 1;
			dataRow["Width"] = 1;
			dataRow["Long"] = 1;
			dataRow["Weight"] = 1;
			dataRow["AmountDisc"] = 0;
			dataRow["PercentDisc"] = 0;
			dataRow["VatCode"] = ItemDetails.VatCode;
			dataRow["TaxExemptionReason"] = ItemDetails.TaxExemptionReason;
			dataRow["TaxExemptionReasonCode"] = ItemDetails.TaxExemptionReasonCode;
			decimal num = Convert.ToDecimal(ItemDetails.ConvertMediumUnit);
			dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * (Convert.ToDecimal(dataRow["Quantity"]) * num), DecimalNumQty);
			dataRow["AvergItem"] = FUNC_DecimalNum(ItemDetails.Average_cost * ItemDetails.ConvertMediumUnit);
			dataRow["SumAvergItem"] = ItemDetails.Average_cost * ItemDetails.ConvertMediumUnit;
			decimal num2 = FUNC_DecimalNum(ItemDetails.SellPriceMedium + item_Items.PriceExportunit1);
			if (LoginDetails.ItemTotalWithVat)
			{
				decimal value = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
				decimal num3 = FUNC_DecimalNum(ItemDetails.SellPriceMedium / (decimal?)value + item_Items.PriceExportunit1);
				if (toggleSectoral.IsOn)
				{
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceMedium / (decimal?)value);
					dataRow["Total"] = num3;
					dataRow["OrginalPrice"] = dataRow["Total"];
					VatWithTotal(ItemDetails.Is_Tax, value, num3, Convert.ToDecimal(dataRow["Total"]), Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
				}
				else
				{
					decimal num4 = FUNC_DecimalNum(ItemDetails.SellPriceMedium2 / (decimal?)value + item_Items.PriceExportunit1);
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceMedium2 / (decimal?)value);
					dataRow["Total"] = num4;
					dataRow["OrginalPrice"] = dataRow["Total"];
					VatWithTotal(ItemDetails.Is_Tax, value, num4, Convert.ToDecimal(dataRow["Total"]), Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
				}
			}
			else
			{
				if (toggleSectoral.IsOn)
				{
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceMedium);
					dataRow["SumPrice"] = num2;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num2, Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
				}
				else
				{
					decimal num5 = FUNC_DecimalNum(ItemDetails.SellPriceMedium2 + item_Items.PriceExportunit1);
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceMedium2);
					dataRow["SumPrice"] = num5;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num5, Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
				}
				dataRow["OrginalPrice"] = dataRow["SumPrice"];
			}
			datatable.Rows.Add(dataRow);
		}
		else
		{
			InsertNewItemUnitMedium(ItemDetails.Item_code);
		}
		ArrangeDatatable();
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
		gridControl1.DataSource = datatable;
		SoundPlayer soundPlayer = new SoundPlayer("MsgSent.wav");
		soundPlayer.Play();
		TxtBarcode.Focus();
	}

	public void InsertNewItemBig(string itemCode)
	{
		Get_Item_ByCodeALL_Result ItemDetails = ITM.GetItemByCode(itemCode).FirstOrDefault();
		Item_Items item_Items = (from x in ITM.GetItemByCode()
			where x.Item_code == ItemDetails.Item_code
			select x).FirstOrDefault();
		string unitNameById = ITM.GetUnitNameById(Convert.ToInt32(item_Items.UnitLarge));
		Printer printer = ORD.GetPrinter(ItemDetails.ItemId);
		if (!LoginDetails.AlLItemInRow)
		{
			DataRow dataRow = datatable.NewRow();
			dataRow["ItemID"] = ItemDetails.ItemId;
			dataRow["ItemUnitID"] = ItemDetails.UnitLargeID;
			dataRow["PrinterID"] = printer.ID;
			dataRow["PrinterName"] = printer.Name;
			dataRow["ItemCode"] = ItemDetails.Item_code;
			dataRow["ItemName"] = ItemDetails.item_Name;
			dataRow["Group"] = ItemDetails.Category_ID;
			dataRow["UnitName"] = unitNameById;
			dataRow["Quantity"] = FUNC_DecimalNumQty(1);
			dataRow["InstallationPrice"] = FUNC_DecimalNum(ItemDetails.PriceExportunit1);
			dataRow["InstallPriceDis"] = dataRow["InstallationPrice"];
			dataRow["Delete"] = 0;
			dataRow["Done"] = 1;
			dataRow["UnitType"] = "الكبرى";
			dataRow["AvergItem"] = ItemDetails.Average_cost.GetValueOrDefault();
			dataRow["AvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["AvergItem"]));
			dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
			dataRow["StoreName"] = ORD.GetStoreName();
			dataRow["StoreID"] = ORD.GetStoreID();
			dataRow["StoreName"] = Comp_Store.Text;
			dataRow["StoreID"] = Comp_Store.SelectedValue;
			dataRow["BounceItem"] = 0;
			dataRow["Height"] = 1;
			dataRow["Width"] = 1;
			dataRow["Long"] = 1;
			dataRow["Weight"] = 1;
			dataRow["AmountDisc"] = 0;
			dataRow["PercentDisc"] = 0;
			dataRow["VatCode"] = ItemDetails.VatCode;
			dataRow["TaxExemptionReason"] = ItemDetails.TaxExemptionReason;
			dataRow["TaxExemptionReasonCode"] = ItemDetails.TaxExemptionReasonCode;
			decimal num = Convert.ToDecimal(ItemDetails.ConvertMediumUnit * ItemDetails.ConvertBigUnit);
			dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * (Convert.ToDecimal(dataRow["Quantity"]) * num), DecimalNumQty);
			dataRow["AvergItem"] = FUNC_DecimalNum(ItemDetails.Average_cost * ItemDetails.ConvertMediumUnit * ItemDetails.ConvertBigUnit);
			dataRow["SumAvergItem"] = ItemDetails.Average_cost * ItemDetails.ConvertMediumUnit * ItemDetails.ConvertBigUnit;
			decimal num2 = FUNC_DecimalNum(ItemDetails.SellpriceLarge + item_Items.PriceExportunit1);
			if (LoginDetails.ItemTotalWithVat)
			{
				decimal value = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
				decimal num3 = FUNC_DecimalNum(ItemDetails.SellpriceLarge / (decimal?)value + item_Items.PriceExportunit1);
				if (toggleSectoral.IsOn)
				{
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellpriceLarge / (decimal?)value);
					dataRow["Total"] = num3;
					dataRow["OrginalPrice"] = dataRow["Total"];
					VatWithTotal(ItemDetails.Is_Tax, value, num3, Convert.ToDecimal(dataRow["Total"]), Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
				}
				else
				{
					decimal num4 = FUNC_DecimalNum(ItemDetails.SellpriceLarge2 / (decimal?)value + item_Items.PriceExportunit1);
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellpriceLarge2 / (decimal?)value);
					dataRow["Total"] = num4;
					dataRow["OrginalPrice"] = dataRow["Total"];
					VatWithTotal(ItemDetails.Is_Tax, value, num4, Convert.ToDecimal(dataRow["Total"]), Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
				}
			}
			else
			{
				if (toggleSectoral.IsOn)
				{
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellpriceLarge);
					dataRow["SumPrice"] = num2;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num2, Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
				}
				else
				{
					decimal num5 = FUNC_DecimalNum(ItemDetails.SellpriceLarge2 + item_Items.PriceExportunit1);
					dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellpriceLarge2);
					dataRow["SumPrice"] = num5;
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num5, Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
				}
				dataRow["OrginalPrice"] = dataRow["SumPrice"];
			}
			datatable.Rows.Add(dataRow);
			ArrangeDatatable();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			gridControl1.DataSource = datatable;
			SoundPlayer soundPlayer = new SoundPlayer("MsgSent.wav");
			soundPlayer.Play();
			TxtBarcode.Focus();
		}
		else
		{
			InsertNewItemUnitLarg(ItemDetails.Item_code);
		}
	}

	public decimal GetMizanQuantity(decimal PriceItem, string MizanRate)
	{
		decimal num = 0m;
		return MizanRate switch
		{
			"3:3" => PriceItem / 1000m, 
			"1:5" => PriceItem / 100000m, 
			"2:4" => PriceItem / 10000m, 
			"4:2" => PriceItem / 100m, 
			"5:1" => PriceItem / 10m, 
			_ => PriceItem / 1000m, 
		};
	}

	public int InsertNewItemMizan(string BarcodeMizan)
	{
		int result = 0;
		try
		{
			bool flag = false;
			string text = BarcodeMizan.Substring(0, 7);
			string value = BarcodeMizan.Substring(BarcodeMizan.Length - 6, 6);
			decimal num = 0m;
			if (LoginDetails.IsMizanWeight)
			{
			}
			Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(text.ToString()).FirstOrDefault();
			if (get_Item_ByCodeALL_Result != null && get_Item_ByCodeALL_Result.Is_Item_mizan == true && !flag)
			{
				DataRow dataRow = null;
				dataRow = datatable.NewRow();
				dataRow["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
				dataRow["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
				dataRow["ItemCode"] = get_Item_ByCodeALL_Result.Item_code;
				dataRow["ItemName"] = get_Item_ByCodeALL_Result.item_Name;
				dataRow["Group"] = get_Item_ByCodeALL_Result.Category_ID;
				dataRow["VatCode"] = get_Item_ByCodeALL_Result.VatCode;
				dataRow["TaxExemptionReason"] = get_Item_ByCodeALL_Result.TaxExemptionReason;
				dataRow["TaxExemptionReasonCode"] = get_Item_ByCodeALL_Result.TaxExemptionReasonCode;
				decimal num2 = Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall);
				if (LoginDetails.IsMizanWeight)
				{
					decimal num3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
					decimal num4 = (LoginDetails.ItemTotalWithVat ? FUNC_DecimalNumQty(num2 / num3) : FUNC_DecimalNumQty(num2));
					decimal num5 = FUNC_DecimalNumQty(GetMizanQuantity(Convert.ToDecimal(value), LoginDetails.MizanRate));
					decimal num6 = FUNC_DecimalNumQty(num5 * num4);
					num = FUNC_DecimalNum(num2 - num2 / num3);
					dataRow["Quantity"] = FUNC_DecimalNumQty(num5);
					dataRow["UnitPrice"] = num4;
					dataRow["SumPrice"] = FUNC_DecimalNum(num4 * num5);
					decimal num7 = Convert.ToDecimal(dataRow["SumPrice"]);
					dataRow["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["SumPrice"]) * (Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m));
					decimal num8 = FUNC_DecimalNum(Convert.ToDecimal(dataRow["SumPrice"]) + Convert.ToDecimal(dataRow["ItemVat"]));
					dataRow["Total"] = num8;
				}
				else if (LoginDetails.ItemTotalWithVat)
				{
					decimal num9 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
					if (DecimalNumQty == 3 && LoginDetails.MizanRate == "2:4")
					{
						decimal num5 = GetMizanQuantity(Convert.ToDecimal(value), LoginDetails.MizanRate);
						num5 = Math.Floor(num5 * 1000m) / 1000m;
						dataRow["Quantity"] = FUNC_DecimalNumQty(Convert.ToDecimal(num5));
						decimal num6 = Math.Round(num5 * Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), 3);
						dataRow["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
						decimal num10 = Math.Round(num6 / num9, 3);
						num = Convert.ToDecimal(num6) - Convert.ToDecimal(num10);
						dataRow["OrginalPrice"] = FUNC_DecimalNum(num6 / Convert.ToDecimal(dataRow["Quantity"]));
						dataRow["ItemVat"] = num;
						dataRow["SumPrice"] = num10;
						decimal num11 = Math.Round(num6, 2, MidpointRounding.AwayFromZero);
						dataRow["Total"] = num11;
					}
					else
					{
						decimal num5 = FUNC_DecimalNumQty(GetMizanQuantity(Convert.ToDecimal(value), LoginDetails.MizanRate));
						dataRow["Quantity"] = FUNC_DecimalNumQty(Convert.ToDecimal(num5) / Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
						decimal num6 = Math.Round(num5 / num9, 3);
						dataRow["UnitPrice"] = FUNC_DecimalNum(num6 / Convert.ToDecimal(dataRow["Quantity"]));
						num = FUNC_DecimalNum(Convert.ToDecimal(num5) - Convert.ToDecimal(num6));
						dataRow["OrginalPrice"] = FUNC_DecimalNum(num6 / Convert.ToDecimal(dataRow["Quantity"]));
						dataRow["ItemVat"] = num;
						dataRow["SumPrice"] = FUNC_DecimalNum(num6);
						decimal num12 = FUNC_DecimalNum(Convert.ToDecimal(dataRow["SumPrice"]) + Convert.ToDecimal(dataRow["ItemVat"]));
						dataRow["Total"] = num12;
					}
				}
				else
				{
					decimal num13 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
					decimal num6 = FUNC_DecimalNumQty(GetMizanQuantity(Convert.ToDecimal(value), LoginDetails.MizanRate));
					dataRow["Quantity"] = FUNC_DecimalNumQty(Convert.ToDecimal(num6) / Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
					dataRow["UnitPrice"] = FUNC_DecimalNum(num6 / Convert.ToDecimal(dataRow["Quantity"]));
					dataRow["OrginalPrice"] = FUNC_DecimalNum(num6 / Convert.ToDecimal(dataRow["Quantity"]));
					dataRow["SumPrice"] = FUNC_DecimalNum(num6);
					dataRow["ItemVat"] = FUNC_DecimalNum(num6 * (Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m));
				}
				dataRow["Delete"] = 0;
				dataRow["IsItemTax"] = get_Item_ByCodeALL_Result.Is_Tax;
				dataRow["Tax_Value"] = get_Item_ByCodeALL_Result.Tax_Value.GetValueOrDefault();
				dataRow["Done"] = 1;
				dataRow["StoreName"] = Comp_Store.Text;
				dataRow["StoreID"] = Comp_Store.SelectedValue;
				dataRow["UnitType"] = "الصغرى";
				dataRow["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
				dataRow["AvergItem"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.Average_cost);
				dataRow["Height"] = 1;
				dataRow["Width"] = 1;
				dataRow["Long"] = 1;
				dataRow["Weight"] = 1;
				dataRow["AmountDisc"] = 0;
				dataRow["PercentDisc"] = 0;
				dataRow["BounceItem"] = 0;
				dataRow["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]));
				dataRow["SN"] = TblID;
				TblID++;
				datatable.Rows.Add(dataRow);
				gridControl1.DataSource = datatable;
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
				if (LoginDetails.DisPerCashier > 0m)
				{
					txtdicoutprege.Text = LoginDetails.DisPerCashier.ToString();
					txtdicoutprege_KeyUp(txtdicoutprege, new KeyEventArgs(Keys.None));
				}
				SoundPlayer soundPlayer = new SoundPlayer("MsgSent.WAV");
				soundPlayer.Play();
				result = 1;
			}
		}
		catch (Exception)
		{
			result = 0;
		}
		return result;
	}

	public void GetAllitems_alternatives(int ItmID, int StoreID, string Type)
	{
		try
		{
			int unitType = 1;
			if (Type == "المتوسطة")
			{
				unitType = 2;
			}
			if (Type == "الكبرى")
			{
				unitType = 3;
			}
			GetAllitems_Alternatives_Result getAllitems_Alternatives_Result = ORD.GetAllitems_Alternatives(ItmID, StoreID, unitType).FirstOrDefault();
			if (getAllitems_Alternatives_Result != null)
			{
				TxtAllQuantity.Text = FUNC_DecimalNumQty(getAllitems_Alternatives_Result.AllQuantity).ToString();
			}
			else
			{
				TxtAllQuantity.Text = FUNC_DecimalNumQty(0m).ToString();
			}
			TxtQuntity.Text = ORD.GetCurrentBalance(ItmID, Convert.ToInt32(Comp_Store.SelectedValue)).ToString();
		}
		catch (Exception)
		{
		}
	}

	public void UpdateItemRowByNet(CellValueChangedEventArgs e)
	{
		decimal num = Convert.ToDecimal(e.Value);
		foreach (DataRow row in datatable.Rows)
		{
			int num2 = Convert.ToInt32(row["SN"]);
			if (num2 == SN && row["ItemCode"].ToString() != string.Empty)
			{
				decimal num3 = 0m;
				decimal num4 = 0m;
				if (row["InstallationPrice"] != DBNull.Value && row["InstallationPrice"] != null)
				{
					num3 = Convert.ToDecimal(row["InstallationPrice"]);
				}
				if (row["Quantity"] != DBNull.Value && row["Quantity"] != null)
				{
					num4 = Convert.ToDecimal(row["Quantity"]);
				}
				decimal num5 = FUNC_DecimalNum(num3 * num4);
				if (!LoginDetails.IsVat)
				{
					row["Net"] = num;
					row["SumPrice"] = num;
					row["SumPriceDisc"] = num;
					row["Total"] = num;
					row["UnitPrice"] = (num - num5) / Convert.ToDecimal(row["Quantity"]);
					row["OrginalPrice"] = row["UnitPrice"];
					row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPrice"]);
					row["ItemVat"] = 0;
					row["ItemVatDisc"] = 0;
					row["AmountDisc"] = 0;
					row["PercentDisc"] = 0;
				}
				else if (Convert.ToBoolean(row["IsItemTax"]))
				{
					decimal num6 = FUNC_DecimalNum(Convert.ToDecimal(row["Tax_Value"]));
					row["Net"] = FUNC_DecimalNum(num);
					row["Sumprice"] = FUNC_DecimalNum(Convert.ToDecimal(num) / (num6 / 100m + 1m));
					row["ItemVat"] = FUNC_DecimalNum(num - Convert.ToDecimal(row["Sumprice"]));
					decimal num7 = FUNC_DecimalNum(Convert.ToDecimal(row["Sumprice"]) - num5);
					row["UnitPrice"] = FUNC_DecimalNum(num7 / Convert.ToDecimal(row["Quantity"]));
					row["UnitPriceDisc"] = row["UnitPrice"];
					row["Total"] = FUNC_DecimalNum(num);
					row["SumPriceDisc"] = row["SumPrice"];
					row["ItemVatDisc"] = row["ItemVat"];
					row["AmountDisc"] = 0;
					row["PercentDisc"] = 0;
					decimal num8 = Convert.ToDecimal(num) / (num6 / 100m + 1m);
					decimal num9 = num8 - num5;
					row["OrginalPrice"] = num9 / Convert.ToDecimal(row["Quantity"]);
					string text = row["OrginalPrice"].ToString();
				}
				break;
			}
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
				decimal num2 = ((row["OrginalPrice"] != DBNull.Value) ? Convert.ToDecimal(row["OrginalPrice"]) : 0m);
				decimal quantity = ((TxtChange == "Quantity") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"])));
				decimal unitPrice = ((TxtChange == "OrginalPrice") ? num : Convert.ToDecimal(row["OrginalPrice"]));
				if (TxtChange == "UnitPrice")
				{
					row["OrginalPrice"] = num;
					unitPrice = num;
				}
				decimal num3 = ((TxtChange == "Height") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Height"])));
				decimal num4 = ((TxtChange == "Width") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Width"])));
				decimal num5 = ((TxtChange == "Long") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Long"])));
				decimal weight = ((TxtChange == "Weight") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Weight"])));
				decimal bounceItem = ((TxtChange == "BounceItem") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["BounceItem"])));
				decimal num6 = ((TxtChange == "PercentDisc") ? FUNC_DecimalNum(num) : FUNC_DecimalNum(Convert.ToDecimal(row["PercentDisc"])));
				if (num6 > 100m)
				{
					row["PercentDisc"] = 0;
					row["AmountDisc"] = 0;
					break;
				}
				flag2 = TxtChange == "PercentDisc" || TxtChange == "AmountDisc";
				decimal num7 = ((TxtChange == "AmountDisc") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["AmountDisc"])));
				if (num7 > Convert.ToDecimal(row["SumPrice"]))
				{
					row["AmountDisc"] = 0;
					row["PercentDisc"] = 0;
					break;
				}
				flag = TxtChange == "AmountDisc";
				decimal avergItem = ((TxtChange == "AvergItem") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"])));
				decimal installationPrice = ((TxtChange == "InstallationPrice") ? num : FUNC_DecimalNum((row["InstallPriceDis"] == DBNull.Value || row["InstallPriceDis"] == null) ? 0m : Convert.ToDecimal(row["InstallPriceDis"])));
				UpdateRowFactionAccount(quantity, unitPrice, num3, num4, num5, weight, bounceItem, flag2, flag, num6, num7, avergItem, installationPrice);
			}
		}
	}

	public void UpdateRowFactionAccount(decimal Quantity, decimal UnitPrice, decimal Height, decimal Width, decimal Long, decimal Weight, decimal BounceItem, bool IsDisc, bool IsDiscAmount, decimal PercentDisc, decimal AmountDisc, decimal AvergItem, decimal InstallationPrice)
	{
		decimal num = Quantity * Height * Width * Long * Weight;
		decimal num2 = (Quantity + BounceItem) * Height * Width * Long * Weight;
		decimal num3 = UnitPrice * num;
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToInt32(row["SN"]) != SN)
			{
				continue;
			}
			bool flag = Convert.ToBoolean(row["IsItemTax"]);
			row["Quantity"] = Quantity;
			row["BounceItem"] = BounceItem;
			row["Height"] = Height;
			row["Width"] = Width;
			row["Long"] = Long;
			row["Weight"] = Weight;
			row["UnitPrice"] = FUNC_DecimalNum(UnitPrice);
			row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
			row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
			row["AvergItem"] = FUNC_DecimalNum(AvergItem);
			row["InstallationPrice"] = InstallationPrice;
			if (PercentDisc > 0m)
			{
				IsDisc = true;
			}
			if (!LoginDetails.IsVat)
			{
				row["SumPrice"] = (UnitPrice + InstallationPrice) * num;
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
					row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * ((UnitPrice + InstallationPrice) * num) / 100m);
				}
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["AmountDisc"]));
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (PercentDisc / 100m));
				row["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]) - Convert.ToDecimal(row["InstallationPrice"]) * (PercentDisc / 100m));
				row["ItemVatDisc"] = 0;
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
			}
			else if (flag)
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					UnitPrice = Convert.ToDecimal(row["OrginalPrice"]);
					decimal num4 = FUNC_DecimalNum(Convert.ToDecimal(row["Tax_Value"]) / 100m);
					row["SumPrice"] = (UnitPrice + InstallationPrice) * num;
					decimal num5 = Convert.ToDecimal(row["SumPrice"]);
					row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * num4);
					if (IsDisc && IsDiscAmount)
					{
						PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(row["SumPrice"]) * 100m);
						row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
					}
					else if (IsDisc && !IsDiscAmount)
					{
						AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(row["SumPrice"]));
						row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
						row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * ((UnitPrice + InstallationPrice) * num) / 100m);
					}
					else if (!IsDisc && !IsDiscAmount)
					{
						row["UnitPriceDisc"] = row["UnitPrice"];
					}
					row["SumPriceDisc"] = Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["AmountDisc"]);
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) - Convert.ToDecimal(row["UnitPriceDisc"]) * (PercentDisc / 100m));
					row["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]) - Convert.ToDecimal(row["InstallationPrice"]) * (PercentDisc / 100m));
					row["ItemVatDisc"] = Convert.ToDecimal(row["SumPriceDisc"]) * num4;
					decimal value = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
					row["Total"] = Convert.ToDecimal(value);
					row["Net"] = row["Total"];
				}
				else
				{
					row["SumPrice"] = (UnitPrice + InstallationPrice) * num;
					row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					if (IsDisc && IsDiscAmount)
					{
						PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(row["SumPrice"]) * 100m);
						row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
					}
					else if (IsDisc && !IsDiscAmount)
					{
						AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(row["SumPrice"]));
						row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
						row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * ((UnitPrice + InstallationPrice) * num) / 100m);
					}
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["AmountDisc"]));
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) - Convert.ToDecimal(row["UnitPriceDisc"]) * (PercentDisc / 100m));
					row["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]) - Convert.ToDecimal(row["InstallationPrice"]) * (PercentDisc / 100m));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
				}
			}
			else
			{
				row["SumPrice"] = (UnitPrice + InstallationPrice) * num;
				row["ItemVat"] = 0;
				if (IsDisc && IsDiscAmount)
				{
					PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(row["SumPrice"]) * 100m);
					row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
				}
				else if (IsDisc && !IsDiscAmount)
				{
					AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(row["SumPrice"]));
					row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
					row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * ((UnitPrice + InstallationPrice) * num) / 100m);
				}
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["AmountDisc"]));
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) - Convert.ToDecimal(row["UnitPriceDisc"]) * (PercentDisc / 100m));
				row["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]) - Convert.ToDecimal(row["InstallationPrice"]) * (PercentDisc / 100m));
				row["ItemVatDisc"] = 0;
				decimal num6 = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
				row["Total"] = num6;
				row["Net"] = row["Total"];
			}
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
		}
	}

	private void BtnDefault()
	{
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 157);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 157);
		tileGroup2.Items.Clear();
		tileGroup3.Items.Clear();
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		TxtDate.DateTime = DateTime.Now;
		if (!flag)
		{
			BtnEdit.Visible = false;
		}
		if (!flag2)
		{
			BtnDelete.Visible = false;
		}
		TxtVatDisc.Visible = true;
		TxtCost.Visible = false;
		TxtVat.Visible = false;
		TxtTotals.Visible = true;
		TxtTabocc.Visible = true;
		label12.Visible = false;
		label11.Visible = true;
		label14.Visible = true;
		label7.Visible = false;
		if (LoginDetails.FrmOrderClose)
		{
			BtnNew.Visible = false;
			BtnSave.Visible = false;
			BtnEdit.Visible = false;
			BtnDelete.Visible = false;
			BtnExit.Visible = false;
			BtnPrint.Visible = false;
			BtnReturnCashier.Visible = false;
			BtnCheckOut.Visible = false;
			BtnRecipt.Visible = false;
		}
	}

	public Image Base64ToImage(string base64String)
	{
		byte[] array = Convert.FromBase64String(base64String);
		MemoryStream memoryStream = new MemoryStream(array, 0, array.Length);
		memoryStream.Write(array, 0, array.Length);
		return Image.FromStream(memoryStream, useEmbeddedColorManagement: true);
	}

	private Image Base64ToImageGrop(string base64String)
	{
		byte[] buffer = Convert.FromBase64String(base64String);
		using MemoryStream stream = new MemoryStream(buffer);
		return Image.FromStream(stream);
	}

	public void BindComboBox()
	{
		try
		{
			TxtType.DataSource = ORD.GetAccountType();
			TxtType.DisplayMember = "Name";
			TxtType.ValueMember = "ID";
			Comp_Store.DataSource = Contract.Get_Stores();
			Comp_Store.DisplayMember = "Store_Name";
			Comp_Store.ValueMember = "ID";
			CBBounce.DataSource = BNC.GetAll();
			CBBounce.DisplayMember = "Name";
			CBBounce.ValueMember = "ID";
			TxtSuppliers.Text = Cust.GetFirstCustomer();
			TxtSuppliers2.Text = Cust.GetFirstCustomer();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void GridPlus(decimal count = 1m, char star = '1')
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (!(row["SN"].ToString() == Convert.ToString(num)))
			{
				continue;
			}
			ItmID2 = row["ItemCode"].ToString();
			Item_Items itemsCode = ORD.GetItemsCode2(ItmID2);
			if (star == '-' && Convert.ToDecimal(row["Quantity"]) > 1m)
			{
				row["Quantity"] = Convert.ToDecimal(row["Quantity"]) - count;
				row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num2 = Convert.ToDecimal(itemsCode.Tax_Value) / 100m + 1m;
					decimal num3 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["Total"] = Math.Round(Convert.ToDecimal(num3 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["SumPrice"] = Math.Round(Convert.ToDecimal(row["Total"]) / num2, DecimalNum);
					VatWithTotal(itemsCode.Is_Tax, num2, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Total"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
				}
				else
				{
					decimal num4 = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) + Convert.ToDecimal(row["InstallationPrice"]));
					row["SumPrice"] = Math.Round(Convert.ToDecimal(num4 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					Vat(itemsCode.Is_Tax, itemsCode.Tax_Value, itemsCode.Tax_Tobacco, num4, Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
				}
				break;
			}
			switch (star)
			{
			case '+':
				row["Quantity"] = Convert.ToDecimal(row["Quantity"]) + count;
				row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num12 = Convert.ToDecimal(itemsCode.Tax_Value) / 100m + 1m;
					decimal num13 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["Total"] = Math.Round(Convert.ToDecimal(num13 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["SumPrice"] = Math.Round(Convert.ToDecimal(row["Total"]) / num12, DecimalNum);
					VatWithTotal(itemsCode.Is_Tax, num12, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Total"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
				}
				else
				{
					decimal num14 = Convert.ToDecimal(row["UnitPrice"].ToString());
					row["SumPrice"] = Math.Round(Convert.ToDecimal(num14 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					Vat(itemsCode.Is_Tax, itemsCode.Tax_Value, itemsCode.Tax_Tobacco, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
				}
				break;
			case '*':
				row["Quantity"] = Convert.ToDecimal(row["Quantity"]) * count;
				row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num9 = Convert.ToDecimal(itemsCode.Tax_Value) / 100m + 1m;
					decimal num10 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["Total"] = Math.Round(Convert.ToDecimal(num10 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["SumPrice"] = Math.Round(Convert.ToDecimal(row["Total"]) / num9, DecimalNum);
					VatWithTotal(itemsCode.Is_Tax, num9, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Total"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
				}
				else
				{
					decimal num11 = Convert.ToDecimal(row["UnitPrice"].ToString());
					row["SumPrice"] = Math.Round(Convert.ToDecimal(num11 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					Vat(itemsCode.Is_Tax, itemsCode.Tax_Value, itemsCode.Tax_Tobacco, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
				}
				break;
			case '=':
				row["Quantity"] = count;
				row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num15 = Convert.ToDecimal(itemsCode.Tax_Value) / 100m + 1m;
					decimal num16 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["Total"] = Math.Round(Convert.ToDecimal(num16 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["SumPrice"] = Math.Round(Convert.ToDecimal(row["Total"]) / num15, DecimalNum);
					VatWithTotal(itemsCode.Is_Tax, num15, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Total"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
				}
				else
				{
					decimal num17 = Convert.ToDecimal(row["UnitPrice"].ToString());
					row["SumPrice"] = Math.Round(Convert.ToDecimal(num17 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					Vat(itemsCode.Is_Tax, itemsCode.Tax_Value, itemsCode.Tax_Tobacco, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
				}
				break;
			case '2':
				row["Quantity"] = Convert.ToDecimal(row["Quantity"]) + count;
				row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num5 = Convert.ToDecimal(itemsCode.Tax_Value) / 100m + 1m;
					decimal num6 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["Total"] = Math.Round(Convert.ToDecimal(num6 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["SumPrice"] = Math.Round(Convert.ToDecimal(row["Total"]) / num5, DecimalNum);
					decimal num7 = Convert.ToDecimal(row["SumPrice"]);
					VatWithTotal(itemsCode.Is_Tax, num5, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Total"]), Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
				}
				else
				{
					decimal num8 = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) + Convert.ToDecimal(row["InstallationPrice"]));
					row["SumPrice"] = Math.Round(Convert.ToDecimal(num8 * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					Vat(itemsCode.Is_Tax, itemsCode.Tax_Value, itemsCode.Tax_Tobacco, num8, Convert.ToDecimal(row["Quantity"]), row, "ItemCode");
				}
				break;
			}
			break;
		}
		ArrangeDatatable();
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
		gridControl1.DataSource = datatable;
		TxtBarcode.Focus();
	}

	private void GridDelete()
	{
		if (LoginDetails.IsSecureCashier)
		{
			FrmPassWordSupervisor frmPassWordSupervisor = new FrmPassWordSupervisor();
			frmPassWordSupervisor.ShowDialog();
			if (frmPassWordSupervisor.ISClose)
			{
				int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				foreach (DataRow row in datatable.Rows)
				{
					if (row["SN"].ToString() == Convert.ToString(num))
					{
						row.Delete();
						datatable.AcceptChanges();
						break;
					}
				}
				ArrangeDatatable();
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
				gridControl1.DataSource = datatable;
			}
			frmPassWordSupervisor.Close();
			TxtBarcode.Focus();
			return;
		}
		int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row2 in datatable.Rows)
		{
			if (row2["SN"].ToString() == Convert.ToString(num2))
			{
				row2.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
		ArrangeDatatable();
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
		gridControl1.DataSource = datatable;
	}

	private void TxtDigital()
	{
		TxtVat.DecimalDigits = DecimalNum;
		TxtVatDisc.DecimalDigits = DecimalNum;
		TxtTotals.DecimalDigits = DecimalNum;
		TxtTotalDiscount.DecimalDigits = DecimalNum;
		TxtNet.DecimalDigits = DecimalNum;
		TxtCost.DecimalDigits = DecimalNum;
		TxtTabocc.DecimalDigits = DecimalNum;
		TxtDiscount.DecimalDigits = DecimalNum;
		txtdicoutprege.DecimalDigits = DecimalNum;
	}

	private void Clear()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtCode.Text = string.Empty;
		BtnCustomerSearch.Text = string.Empty;
		BtnOrderSearch.Text = string.Empty;
		TxtPhone.Text = string.Empty;
		TxtAdditionalNum.Text = string.Empty;
		TxtCity.Text = string.Empty;
		TxtStreet.Text = string.Empty;
		TxtDistrict.Text = string.Empty;
		TxtCharge.Text = string.Empty;
		TxtPostalCode.Text = string.Empty;
		TxtCommercialRecord.Text = string.Empty;
		TxtSales.Text = string.Empty;
		TxtNumVat.Text = string.Empty;
		TxtCountry.Text = string.Empty;
		TxtNote.Text = string.Empty;
		TxtNumNote.Text = string.Empty;
		TxtBarcode.Text = string.Empty;
		BtnDisCode.Text = string.Empty;
		TxtSuppliers2.Text = string.Empty;
		BtnCustomerSearch2.Text = string.Empty;
		TxtSales2.Text = string.Empty;
		TxtRightSph.Text = string.Empty;
		TxtRightCYL.Text = string.Empty;
		TxtRightAxis.Text = string.Empty;
		TxtRightAdd.Text = string.Empty;
		TxtLeftSph.Text = string.Empty;
		TxtLeftCYL.Text = string.Empty;
		TxtLeftAxis.Text = string.Empty;
		TxtLeftAdd.Text = string.Empty;
		TxtNoteGlass.Text = string.Empty;
		_TxtVatNum.Text = string.Empty;
		_TxtPhone.Text = string.Empty;
		TxtCharge2.Text = "0.00";
		BtnDisCode.Enabled = true;
		datatable.Rows.Clear();
		dtCurrentTable.Rows.Clear();
		gridControl1.DataSource = datatable;
		TxtAllDiscount.Text = "0.00";
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		TxtTotals.Text = "0.00";
		TxtType.Text = "نقدى";
		TxtSuppliers.Text = Cust.GetFirstCustomer();
		TxtSuppliers2.Text = Cust.GetFirstCustomer();
		TxtTotals.Text = "0.00";
		TxtTotalDiscount.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtCost.Text = "0.00";
		TxtTabocc.Text = "0.00";
		toggleCashierType.IsOn = false;
		TxtRoom.Value = 0m;
		TxtTable.Value = 0m;
		TxtDiscount.Enabled = true;
		txtdicoutprege.Enabled = true;
		TxtType.Enabled = true;
		TxtDate.Enabled = true;
		TxtDate.DateTime = DateTime.Now;
		TxtSuppliers.Focus();
		TxtBarcode.Focus();
		LValid.Visible = false;
		LSucced.Visible = false;
		BtnUpload.Visible = false;
		toggleHold.IsOn = false;
		DateHold.DateTime = DateTime.Now;
		DateHold.Visible = false;
		BindComboBox();
	}

	private async Task Save()
	{
		if (TxtCode.Text != "")
		{
			SoundPlayer player = new SoundPlayer("SOUND38.WAV");
			player.Play();
			MessageBox.Show("عفوا،لايمكن حفظ الفاتوره مرتين \r\n الرجاء المراجعة والمحاولة مرة اخري.", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		else if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 157))
		{
			int Result = 0;
			bool IsWait = toggleHold.IsOn;
			if (datatable.Rows.Count >= 1)
			{
				if (TxtSuppliers.Text != string.Empty)
				{
					if (TxtType.Text != string.Empty)
					{
						if (Convert.ToInt32(TxtType.SelectedValue) == 2 && LblSup.Text == "1")
						{
							MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع حساب عميل نقدى", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							TxtSuppliers.Focus();
							return;
						}
						decimal isok = IsItemPriceZero();
						if (isok == 0m || PermissionSellPrice() == 0)
						{
							return;
						}
						if (Convert.ToInt32(TxtType.SelectedValue) == 1)
						{
							LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
							FrmMoneyCashier FrmShow = new FrmMoneyCashier();
							FrmShow.ShowDialog();
							if (!FrmShow.IsComplateMony)
							{
								return;
							}
							Cash = Convert.ToDecimal(FrmShow.TxtCash.Text);
							Bank = Convert.ToDecimal(FrmShow.TxtVisa.Text);
							MoneyNeed = Convert.ToDecimal(FrmShow.TxtMoneyNeed.Text);
							MoneyPaying = Convert.ToDecimal(FrmShow.TxtMoneyPaying.Text);
						}
						else
						{
							LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
							FrmMoneyAjil FrmShow2 = new FrmMoneyAjil();
							FrmShow2.ShowDialog();
							if (!FrmShow2.IsComplateMony)
							{
								return;
							}
							decimal Charg = ((!(Convert.ToDecimal(TxtCharge.Text) < 0m)) ? Convert.ToDecimal(TxtCharge.Text) : (Convert.ToDecimal(TxtCharge.Text) * -1m));
							decimal num = Convert.ToDecimal(FrmShow2.TxtNet.Text) + Charg;
							decimal? creditLimit = CreditLimit;
							int num2;
							if (!((num <= creditLimit.GetValueOrDefault()) & creditLimit.HasValue))
							{
								creditLimit = CreditLimit;
								num2 = (((creditLimit.GetValueOrDefault() == 0m) & creditLimit.HasValue) ? 1 : 0);
							}
							else
							{
								num2 = 1;
							}
							if (num2 == 0)
							{
								MessageBox.Show("عفوا\u064b، رصيد العميل لا يكفي \r\n  الحد الائتماني = " + FUNC_DecimalNum(CreditLimit), "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								return;
							}
							Cash = Convert.ToDecimal(FrmShow2.TxtCash.Text);
							Bank = Convert.ToDecimal(FrmShow2.TxtVisa.Text);
							Safy = Convert.ToDecimal(FrmShow2.TxtNet.Text);
							ChargeAfterSale = Convert.ToDecimal(TxtCharge.Text) + Cash + Bank;
						}
						int CostID = Convert.ToInt32(Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID);
						if (!FilterDataTableSave(datatable, Result, IsWait))
						{
							return;
						}
						LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum);
						LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum);
						string VatNum;
						string Phone;
						string SuplierName;
						if (LoginDetails.Glasses)
						{
							VatNum = _TxtVatNum.Text;
							Phone = _TxtPhone.Text;
							SuplierName = TxtSuppliers2.Text;
						}
						else
						{
							VatNum = TxtNumVat.Text;
							Phone = TxtPhone.Text;
							SuplierName = TxtSuppliers.Text;
						}
						Result = ((Convert.ToInt32(TxtType.SelectedValue) != 1) ? ORD.SaveOrders(Convert.ToInt32(LblSup.Text), IsWait, SuplierName, Phone, VatNum, Convert.ToDateTime(TxtDate.DateTime), Convert.ToInt32(TxtType.SelectedValue), string.Empty, 1, CostID, Convert.ToInt32(CBBounce.SelectedValue), TxtNote.Text, TxtNumNote.Text, Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, toggleCashierType.IsOn, Convert.ToInt32(TxtRoom.Value), Convert.ToInt32(TxtTable.Value), dtCurrentTable, 1, TxtSales.Text, DateTime.Parse(DateHold.Text), "372", TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, 0m, 0m, ChargeAfterSale, 1, Convert.ToDecimal(TxtAllDiscount.Text), 0m, BtnDisCode.Text, LoginDetails.AmountPaid, LoginDetails.Rest, "", "", "", "", "", "") : ORD.SaveOrders(Convert.ToInt32(LblSup.Text), IsWait, SuplierName, Phone, VatNum, Convert.ToDateTime(TxtDate.DateTime), Convert.ToInt32(TxtType.SelectedValue), string.Empty, 1, CostID, Convert.ToInt32(CBBounce.SelectedValue), TxtNote.Text, TxtNumNote.Text, Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, toggleCashierType.IsOn, Convert.ToInt32(TxtRoom.Value), Convert.ToInt32(TxtTable.Value), dtCurrentTable, 1, TxtSales.Text, DateTime.Parse(DateHold.Text), "372", TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, 0m, 0m, ChargeAfterSale, 1, Convert.ToDecimal(TxtAllDiscount.Text), 0m, BtnDisCode.Text, LoginDetails.AmountPaid, LoginDetails.Rest, "", "", "", "", "", ""));
						if (Result > 0)
						{
							ORD.MangeGlassess(Result, TxtRightSph.Text, TxtRightCYL.Text, TxtRightAxis.Text, TxtRightAdd.Text, TxtLeftSph.Text, TxtLeftCYL.Text, TxtLeftAxis.Text, TxtLeftAdd.Text, TxtNoteGlass.Text);
							DisCode.NumberUsedAsync(BtnDisCode.Text);
							OrderOrdersDto OrderDto = await GenerateOrder(Result);
							bool isSuccess = false;
							if (LoginDetails.IsElectronicInvoice)
							{
								if (LoginDetails.ModeInvoice == "developer")
								{
									mode = Mode.developer;
								}
								else if (LoginDetails.ModeInvoice == "Simulation")
								{
									mode = Mode.Simulation;
								}
								else if (LoginDetails.ModeInvoice == "Production")
								{
									mode = Mode.Production;
								}
								ElectronicInvoice invoiceZakat = new ElectronicInvoice();
								InvoiceDto inv = invoiceDto(OrderDto);
								Result res = await invoiceZakat.CreateXml(inv, LoginDetails.InvoiceHash, supplierPartyDto(OrderDto), CustomerPartyDto(OrderDto), invoiceItems(datatable), CSIDInfoDto());
								if (!res.IsValid)
								{
									ORD.DeleteOrders(Result);
									MessageBox.Show(res.ErrorMessage, "الفاتورة غير صالحة");
									LValid.Visible = true;
									LValid.BackColor = Color.Red;
									LValid.Text = "NotValid";
									LSucced.Visible = true;
									LSucced.BackColor = Color.Red;
									LSucced.Text = "NotSuccess";
									return;
								}
								if (LoginDetails.SentXmlZakateNow && res.IsValid)
								{
									isSuccess = (await invoiceZakat.SentInvoiceToZakat(res, CSIDInfoDto(), inv.InvoiceTypeName, mode)).IsSuccess;
									LValid.Visible = true;
									LValid.Text = "Valid";
									LValid.BackColor = Color.Green;
									LSucced.Visible = true;
									LSucced.Text = "Success";
									LSucced.BackColor = Color.Green;
								}
								else
								{
									LValid.Visible = true;
									LValid.Text = "Valid";
									LValid.BackColor = Color.Green;
									LSucced.Visible = true;
									LSucced.BackColor = Color.Red;
									LSucced.Text = "NotSuccess";
									BtnUpload.Visible = true;
								}
								LoginDetails.EncHash = res.QRCode;
								string OrderTyprId = ((LoginDetails.OrderTypeElectronicInvoiceId == 0) ? "388" : LoginDetails.OrderTypeElectronicInvoiceId.ToString());
								string NormalOrSimpleInvoice = (string.IsNullOrEmpty(LoginDetails.NormalOrSimpleInvoice) ? "02" : LoginDetails.NormalOrSimpleInvoice);
								ORD.UpdateOrderAFterElctorncFatora(Result, res.QRCode, OrderTyprId, NormalOrSimpleInvoice, LoginDetails.ThirdParty, LoginDetails.NominalInvoice, LoginDetails.ExportInvoice, LoginDetails.SummaryInvoice, LoginDetails.SelfInvoice);
								ResultElectronicInvoiceXmlService resultElectronicInvoiceXmlService = new ResultElectronicInvoiceXmlService();
								await resultElectronicInvoiceXmlService.AddResult(LoginDetails.UserID, LoginDetails.CsIdForUser, res, LoginDetails.BranchID, isSuccess, inv.OrderId, OrderDto.Purchases_Date.Value, inv.InvoiceTypeName, "مبيعات كاشير");
							}
							BtnSave.Enabled = false;
							if (!LoginDetails.IsElectronicInvoice)
							{
								BtnEdit.Enabled = true;
								BtnDelete.Enabled = true;
							}
							PrintID = Result;
							Code = Result;
							TxtCode.Text = Result.ToString();
							TxtVatNumCustomer = TxtNumVat.Text;
							FilterDataTablePrint(datatable, Code);
							if (LoginDetails.FrmOpenDayIsPrint)
							{
								Thread workerThread = new Thread((ThreadStart)delegate
								{
									BeginInvoke((Action)delegate
									{
										Print(Code);
									});
								});
								workerThread.Start();
							}
							if (LoginDetails.FrmOpenDayIsPrintA4)
							{
								Thread workerThread2 = new Thread((ThreadStart)delegate
								{
									BeginInvoke((Action)delegate
									{
										PrintA4(Code);
									});
								});
								workerThread2.Start();
							}
							if (LoginDetails.IsPrintItemCook)
							{
								Thread workerThread4 = new Thread((ThreadStart)delegate
								{
									BeginInvoke((Action)delegate
									{
										PrintItemCook(Code);
									});
								});
								workerThread4.Start();
							}
							if (LoginDetails.IsPrintSecondFatoraCasheir)
							{
								Thread workerThread5 = new Thread((ThreadStart)delegate
								{
									BeginInvoke((Action)delegate
									{
										PrintSecondDefualt(Code);
									});
								});
								workerThread5.Start();
							}
							if (LoginDetails.SentInvoice)
							{
								string pdfUrl = ExportInvoiceAndUploadToServer();
								if (!string.IsNullOrWhiteSpace(pdfUrl))
								{
									TwilioHelper.SendPdf(TxtPhone.Text, pdfUrl, Result, Convert.ToDecimal(TxtNet.Text), "فاتوره", out var _);
								}
							}
							Clear();
						}
						else
						{
							MessageBox.Show("تم حفظ بيانات فاتورة الكاشير رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							Cash = 0m;
							Bank = 0m;
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
					MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة الكاشير بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 157))
		{
			int purchese_ID = 0;
			if (!(TxtSuppliers.Text != string.Empty))
			{
				return;
			}
			if (Convert.ToInt32(TxtType.SelectedValue) == 2 && LblSup.Text == "1")
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع مورد نقدى", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtSuppliers.Focus();
				return;
			}
			int PurId = Convert.ToInt32(TxtCode.Text);
			Order_OrderReturn order_OrderReturn = db.Order_OrderReturn.Where((Order_OrderReturn x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
			Order_CheckoutOrders order_CheckoutOrders = db.Order_CheckoutOrders.Where((Order_CheckoutOrders x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
			if (order_OrderReturn == null && order_CheckoutOrders == null)
			{
				if (datatable.Rows.Count > 0)
				{
					decimal num = IsItemPriceZero();
					if (num == 0m)
					{
						return;
					}
					ArrangeDatatable();
					if (PermissionSellPrice() == 0)
					{
						return;
					}
					if (Convert.ToInt32(TxtType.SelectedValue) == 1)
					{
						LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
						FrmMoneyCashier frmMoneyCashier = new FrmMoneyCashier();
						frmMoneyCashier.ShowDialog();
						if (!frmMoneyCashier.IsComplateMony)
						{
							return;
						}
						Cash = Convert.ToDecimal(frmMoneyCashier.TxtCash.Text);
						Bank = Convert.ToDecimal(frmMoneyCashier.TxtVisa.Text);
						MoneyNeed = Convert.ToDecimal(frmMoneyCashier.TxtMoneyNeed.Text);
						MoneyPaying = Convert.ToDecimal(frmMoneyCashier.TxtMoneyPaying.Text);
					}
					else
					{
						LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
						LoginDetails.Cach = Math.Round(FrmCash22, DecimalNum);
						LoginDetails.Bank = Math.Round(FrmBank22, DecimalNum);
						FrmMoneyAjil frmMoneyAjil = new FrmMoneyAjil();
						frmMoneyAjil.ShowDialog();
						if (!frmMoneyAjil.IsComplateMony)
						{
							return;
						}
						decimal num2 = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
						decimal? num3 = CreditLimit + (decimal?)Convert.ToDecimal(TxtCharge.Text);
						if ((num2 <= num3.GetValueOrDefault()) & num3.HasValue)
						{
							Cash = Convert.ToDecimal(frmMoneyAjil.TxtCash.Text);
							Bank = Convert.ToDecimal(frmMoneyAjil.TxtVisa.Text);
							Safy = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
							TxtTotals.Clear();
							TxtTotals.Text = Safy.ToString();
						}
					}
					int costCentersID = Convert.ToInt32(Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID);
					if (!FilterDataTableSave(datatable, purchese_ID, toggleHold.IsOn))
					{
						return;
					}
					LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum);
					LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum);
					string supplierPhone;
					if (LoginDetails.Glasses)
					{
						string text = _TxtVatNum.Text;
						supplierPhone = _TxtPhone.Text;
					}
					else
					{
						string text = TxtNumVat.Text;
						supplierPhone = TxtPhone.Text;
					}
					purchese_ID = ((Convert.ToInt32(TxtType.SelectedValue) != 1) ? ORD.UpdateOrders(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), toggleHold.IsOn, TxtSuppliers.Text, supplierPhone, string.Empty, TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), string.Empty, 1, costCentersID, Convert.ToInt32(CBBounce.SelectedValue), TxtNote.Text, string.Empty, Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, toggleCashierType.IsOn, Convert.ToInt32(TxtRoom.Value), Convert.ToInt32(TxtTable.Value), dtCurrentTable, TxtSales.Text, Convert.ToDateTime(DateHold.Text), "372", TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, 0m, 0m, ChargeAfterSale, Convert.ToDecimal(TxtAllDiscount.Text), 0m, "", "", "", "", "", "", 1) : ORD.UpdateOrders(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), toggleHold.IsOn, TxtSuppliers.Text, supplierPhone, TxtNumVat.Text, TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), string.Empty, 1, costCentersID, Convert.ToInt32(CBBounce.SelectedValue), TxtNote.Text, TxtNumNote.Text, Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, toggleCashierType.IsOn, Convert.ToInt32(TxtRoom.Value), Convert.ToInt32(TxtTable.Value), dtCurrentTable, TxtSales.Text, Convert.ToDateTime(DateHold.Text), "372", TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, 0m, 0m, ChargeAfterSale, Convert.ToDecimal(TxtAllDiscount.Text), 0m, "", "", "", "", "", "", 1));
					if (purchese_ID > 0)
					{
						ORD.MangeGlassess(Convert.ToInt32(TxtCode.Text), TxtRightSph.Text, TxtRightCYL.Text, TxtRightAxis.Text, TxtRightAdd.Text, TxtLeftSph.Text, TxtLeftCYL.Text, TxtLeftAxis.Text, TxtLeftAdd.Text, TxtNoteGlass.Text);
						PrintID = Convert.ToInt32(TxtCode.Text);
						Code = Convert.ToInt32(TxtCode.Text);
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						FilterDataTablePrint(datatable, Code);
						if (LoginDetails.FrmOpenDayIsPrint)
						{
							Thread thread = new Thread((ThreadStart)delegate
							{
								BeginInvoke((Action)delegate
								{
									Print(Code);
								});
							});
							thread.Start();
						}
						if (LoginDetails.FrmOpenDayIsPrintA4)
						{
							Thread thread2 = new Thread((ThreadStart)delegate
							{
								BeginInvoke((Action)delegate
								{
									PrintA4(Code);
								});
							});
							thread2.Start();
						}
						Clear();
						Charge(Convert.ToInt32(LblSup.Text));
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات فاتورة الكاشير رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Cash = 0m;
						Bank = 0m;
					}
				}
				else
				{
					MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b لا يمكن التعديل على فاتورة المبيعات ليها حركات مرتجعات او صرف ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (LoginDetails.IsElectronicInvoice)
		{
			return;
		}
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 157))
		{
			int num = 0;
			if (TxtCode.Text != string.Empty)
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة الكاشير رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
				{
					int PurId = Convert.ToInt32(TxtCode.Text);
					Order_OrderReturn order_OrderReturn = db.Order_OrderReturn.Where((Order_OrderReturn x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
					Order_CheckoutOrders order_CheckoutOrders = db.Order_CheckoutOrders.Where((Order_CheckoutOrders x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
					if (order_OrderReturn == null && order_CheckoutOrders == null)
					{
						num = ORD.DeleteOrders(Convert.ToInt32(TxtCode.Text));
						if (num > 0)
						{
							ORD.DeleteGlassess(Convert.ToInt32(TxtCode.Text));
							MessageBox.Show("تم حذف بيانات فاتورة الكاشير رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							Clear();
							BtnDelete.Enabled = false;
							BtnEdit.Enabled = false;
							BtnSave.Enabled = true;
							PrintID = 0;
							TxtBarcode.Focus();
						}
						else
						{
							MessageBox.Show("لا يمكن حذف بيانات فاتورة الكاشير رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						}
					}
					else
					{
						MessageBox.Show("عفوا\u064b لا يمكن الحذف على فاتورة المبيعات ليها حركات مرتجعات او صرف ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة الكاشير بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void PrintDataTable(int Code)
	{
		try
		{
			if (Code > 0)
			{
				Thread thread = new Thread((ThreadStart)delegate
				{
					BeginInvoke((Action)delegate
					{
						PrintSecondCasheir(Code);
					});
				});
				thread.Start();
				if (ST.TableGetLedger_Employee.Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					if (LoginDetails.Lang == "ar-EG")
					{
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashir.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCashir.mrt");
					}
					stiReport.RegData(ST.TableGetLedger_Employee);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					if (TxtVatNumCustomer != string.Empty)
					{
						stiReport["Title"] = "فاتورة ضريبية";
					}
					else
					{
						stiReport["Title"] = "فاتورة ضريبية مبسطة";
					}
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
					stiReport["Address"] = LoginDetails.Address;
					stiReport["Phone"] = LoginDetails.Phone;
					stiReport["MobileComp"] = LoginDetails.Mobile;
					stiReport["Bank"] = LoginDetails.CompanyBank;
					stiReport["CashMoney"] = Cash.ToString();
					stiReport["CashBank"] = Bank.ToString();
					stiReport["MoneyNeed"] = MoneyNeed.ToString();
					stiReport["MoneyPaying"] = MoneyPaying.ToString();
					stiReport["MoneyNet"] = MoneyNet.ToString();
					stiReport["MobileComp"] = LoginDetails.Mobile;
					stiReport["AddressComp"] = LoginDetails.Address;
					stiReport["PrintComp"] = LoginDetails.PrintComp;
					stiReport["CustName"] = TxtSuppliers.Text;
					stiReport["CustVatNum"] = TxtNumVat.Text;
					stiReport["Custphone"] = TxtPhone.Text;
					stiReport["Tafqit"] = txtWordAr;
					stiReport["EmployeeName"] = LoginDetails.UserName;
					Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
					string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), Math.Round(LoginDetails.NetCash + LoginDetails.VatCash, 2).ToString(), LoginDetails.VatCash.ToString());
					stiReport["EncHash"] = value;
					PrinterSettings printerSettings = new PrinterSettings();
					printerSettings.PrinterName = LoginDetails.DefualtPrint;
					printerSettings.DefaultPageSettings.PaperSize = new PaperSize("Custom", 80, 3276);
					stiReport.Print(showPrintDialog: false, printerSettings);
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
		catch (Exception)
		{
			MessageBox.Show("عفوا، لم يتم الطباعة ", "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PrintA4DataTable(int Code)
	{
		try
		{
			if (Code > 0)
			{
				using (new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer))
				{
					if (ST.TableGetLedger_Employee.Rows.Count > 0)
					{
						StiReport stiReport = new StiReport();
						if (LoginDetails.Lang == "ar-EG")
						{
							stiReport.Load(Application.StartupPath + "\\Reports\\PrintOrders.mrt");
						}
						else
						{
							stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintOrders.mrt");
						}
						stiReport.RegData(ST.TableGetLedger_Employee);
						stiReport.Dictionary.Synchronize();
						stiReport.Compile();
						if (TxtVatNumCustomer != string.Empty)
						{
							stiReport["Title"] = "فاتورة ضريبية";
						}
						else
						{
							stiReport["Title"] = "فاتورة ضريبية مبسطة";
						}
						stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
						stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
						stiReport["VatNumComp"] = LoginDetails.VatNum;
						stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
						stiReport["Address"] = LoginDetails.Address;
						stiReport["Phone"] = LoginDetails.Phone;
						stiReport["MobileComp"] = LoginDetails.Mobile;
						stiReport["Bank"] = LoginDetails.CompanyBank;
						stiReport["CashMoney"] = Cash.ToString();
						stiReport["CashBank"] = Bank.ToString();
						stiReport["MoneyNeed"] = MoneyNeed.ToString();
						stiReport["MoneyPaying"] = MoneyPaying.ToString();
						stiReport["MoneyNet"] = MoneyNet.ToString();
						stiReport["MobileComp"] = LoginDetails.Mobile;
						stiReport["AddressComp"] = LoginDetails.Address;
						stiReport["PrintComp"] = LoginDetails.PrintComp;
						stiReport["CustName"] = TxtSuppliers.Text;
						stiReport["CustVatNum"] = TxtNumVat.Text;
						stiReport["Custphone"] = TxtPhone.Text;
						stiReport["Tafqit"] = txtWordAr;
						stiReport["EmployeeName"] = LoginDetails.UserName;
						Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
						string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), Math.Round(LoginDetails.NetCash + LoginDetails.VatCash, 2).ToString(), LoginDetails.VatCash.ToString());
						stiReport["EncHash"] = value;
						PrinterSettings printerSettings = new PrinterSettings();
						printerSettings.PrinterName = LoginDetails.DefualtPrintA4;
						stiReport.Print(showPrintDialog: false, printerSettings);
						LoginDetails.FrmOpenDayIsPrintA4 = false;
					}
					else
					{
						MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					return;
				}
			}
			MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception)
		{
			MessageBox.Show("PrintA4DataTable عفوا، لم يتم الطباعة ", "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PreviewPrintDataTable(int Code)
	{
		try
		{
			if (Code > 0)
			{
				if (ST.TableGetLedger_Employee.Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					if (LoginDetails.Lang == "ar-EG")
					{
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashir.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCashir.mrt");
					}
					stiReport.RegData(ST.TableGetLedger_Employee);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					if (TxtVatNumCustomer != string.Empty)
					{
						stiReport["Title"] = "فاتورة ضريبية";
					}
					else
					{
						stiReport["Title"] = "فاتورة ضريبية مبسطة";
					}
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
					stiReport["Address"] = LoginDetails.Address;
					stiReport["Phone"] = LoginDetails.Phone;
					stiReport["MobileComp"] = LoginDetails.Mobile;
					stiReport["Bank"] = LoginDetails.CompanyBank;
					stiReport["CashMoney"] = Cash.ToString();
					stiReport["CashBank"] = Bank.ToString();
					stiReport["MoneyNeed"] = MoneyNeed.ToString();
					stiReport["MoneyPaying"] = MoneyPaying.ToString();
					stiReport["MoneyNet"] = MoneyNet.ToString();
					stiReport["MobileComp"] = LoginDetails.Mobile;
					stiReport["AddressComp"] = LoginDetails.Address;
					stiReport["PrintComp"] = LoginDetails.PrintComp;
					stiReport["CustName"] = TxtSuppliers.Text;
					stiReport["CustVatNum"] = TxtNumVat.Text;
					stiReport["Custphone"] = TxtPhone.Text;
					stiReport["Tafqit"] = txtWordAr;
					stiReport["EmployeeName"] = LoginDetails.UserName;
					Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
					string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), Math.Round(LoginDetails.NetCash + LoginDetails.VatCash, 2).ToString(), LoginDetails.VatCash.ToString());
					stiReport["EncHash"] = value;
					stiReport.Show(dialogForm: true);
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
		catch (Exception)
		{
			MessageBox.Show("عفوا، لم يتم الطباعة ", "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Print(int Code)
	{
		try
		{
			if (LoginDetails.IsPrintOrder)
			{
				Thread thread = new Thread((ThreadStart)delegate
				{
					BeginInvoke((Action)delegate
					{
						PrintDataTable(Code);
					});
				});
				thread.Start();
				return;
			}
			if (Code > 0)
			{
				using (SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer))
				{
					ST.Tables["Order_PurchasesTablePrint"].Clear();
					SqlCommand sqlCommand = new SqlCommand("Print_Order_Orders", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlCommand.Parameters.AddWithValue("@ID", Code);
					sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
					sqlDataAdapter.Fill(ST.Order_PurchasesTablePrint);
					sqlConnection.Close();
					if (ST.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
					{
						StiReport stiReport = new StiReport();
						if (LoginDetails.Lang == "ar-EG")
						{
							if (LoginDetails.IsElectronicInvoice)
							{
								stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirZatca.mrt");
							}
							else
							{
								stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashir.mrt");
							}
						}
						else
						{
							stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCashir.mrt");
						}
						stiReport.RegData(ST.Order_PurchasesTablePrint);
						stiReport.Dictionary.Synchronize();
						stiReport.Compile();
						if (TxtVatNumCustomer != string.Empty)
						{
							stiReport["Title"] = "فاتورة ضريبية";
						}
						else
						{
							stiReport["Title"] = "فاتورة ضريبية مبسطة";
						}
						stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
						stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
						stiReport["VatNumComp"] = LoginDetails.VatNum;
						stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
						stiReport["Address"] = LoginDetails.Address;
						stiReport["Phone"] = LoginDetails.Phone;
						stiReport["MobileComp"] = LoginDetails.Mobile;
						stiReport["Bank"] = LoginDetails.CompanyBank;
						stiReport["CashMoney"] = Cash.ToString();
						stiReport["CashBank"] = Bank.ToString();
						stiReport["MoneyNeed"] = MoneyNeed.ToString();
						stiReport["MoneyPaying"] = MoneyPaying.ToString();
						stiReport["MoneyNet"] = MoneyNet.ToString();
						stiReport["MobileComp"] = LoginDetails.Mobile;
						stiReport["AddressComp"] = LoginDetails.Address;
						stiReport["PrintComp"] = LoginDetails.PrintComp;
						stiReport["CustName"] = TxtSuppliers.Text;
						stiReport["CustVatNum"] = TxtNumVat.Text;
						stiReport["Custphone"] = TxtPhone.Text;
						stiReport["Tafqit"] = txtWordAr;
						stiReport["EmployeeName"] = LoginDetails.UserName;
						Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
						string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), Math.Round(LoginDetails.NetCash + LoginDetails.VatCash, 2).ToString(), LoginDetails.VatCash.ToString());
						if (LoginDetails.IsElectronicInvoice)
						{
							string text = LoginDetails.EncHash.Trim().Replace("\r", "").Replace("\n", "");
							Image value2 = QRHelper.GenerateQRCode(text);
							stiReport["QrZatca"] = value2;
							stiReport["EncHash"] = text;
						}
						else
						{
							stiReport["EncHash"] = value;
						}
						PrinterSettings printerSettings = new PrinterSettings();
						printerSettings.PrinterName = LoginDetails.DefualtPrint;
						printerSettings.DefaultPageSettings.PaperSize = new PaperSize("Custom", 80, 3276);
						stiReport.Print(showPrintDialog: false, printerSettings);
					}
					else
					{
						MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					return;
				}
			}
			MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception)
		{
			MessageBox.Show("Print1 عفوا، لم يتم الطباعة ", "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PrintA4(int Code)
	{
		try
		{
			if (LoginDetails.IsPrintOrder)
			{
				Thread thread = new Thread((ThreadStart)delegate
				{
					BeginInvoke((Action)delegate
					{
						PrintA4DataTable(Code);
					});
				});
				thread.Start();
				return;
			}
			if (Code > 0)
			{
				using (SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer))
				{
					ST.Tables["Order_PurchasesTablePrint"].Clear();
					SqlCommand sqlCommand = new SqlCommand("Print_Order_Orders", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlCommand.Parameters.AddWithValue("@ID", Code);
					sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
					sqlDataAdapter.Fill(ST.Order_PurchasesTablePrint);
					sqlConnection.Close();
					if (ST.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
					{
						StiReport stiReport = new StiReport();
						if (LoginDetails.Lang == "ar-EG")
						{
							if (LoginDetails.IsElectronicInvoice)
							{
								stiReport.Load(Application.StartupPath + "\\Reports\\PrintOrdersZatca.mrt");
							}
							else
							{
								stiReport.Load(Application.StartupPath + "\\Reports\\PrintOrders.mrt");
							}
						}
						else
						{
							stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintOrders.mrt");
						}
						stiReport.RegData(ST.Order_PurchasesTablePrint);
						stiReport.Dictionary.Synchronize();
						stiReport.Compile();
						if (TxtVatNumCustomer != string.Empty)
						{
							stiReport["Title"] = "فاتورة ضريبية";
						}
						else
						{
							stiReport["Title"] = "فاتورة ضريبية مبسطة";
						}
						stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
						stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
						stiReport["VatNumComp"] = LoginDetails.VatNum;
						stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
						stiReport["Address"] = LoginDetails.Address;
						stiReport["Phone"] = LoginDetails.Phone;
						stiReport["MobileComp"] = LoginDetails.Mobile;
						stiReport["Bank"] = LoginDetails.CompanyBank;
						stiReport["CustName"] = TxtSuppliers.Text;
						stiReport["CustVatNum"] = TxtNumVat.Text;
						stiReport["Custphone"] = TxtPhone.Text;
						stiReport["PrintComp"] = LoginDetails.PrintComp;
						stiReport["Tafqit"] = txtWordAr;
						stiReport["EmployeeName"] = LoginDetails.UserName;
						stiReport["CashMoney"] = Cash.ToString();
						stiReport["CashBank"] = Bank.ToString();
						stiReport["MoneyNeed"] = MoneyNeed.ToString();
						stiReport["MoneyPaying"] = MoneyPaying.ToString();
						stiReport["MoneyNet"] = MoneyNet.ToString();
						Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
						string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), Math.Round(LoginDetails.NetCash + LoginDetails.VatCash, 2).ToString(), LoginDetails.VatCash.ToString());
						if (LoginDetails.IsElectronicInvoice)
						{
							string qrText = LoginDetails.EncHash.Trim().Replace("\r", "").Replace("\n", "");
							Image value2 = QRHelper.GenerateQRCode(qrText);
							stiReport["QrZatca"] = value2;
						}
						else
						{
							stiReport["EncHash"] = value;
						}
						PrinterSettings printerSettings = new PrinterSettings();
						printerSettings.PrinterName = LoginDetails.DefualtPrintA4;
						stiReport.Print(showPrintDialog: false, printerSettings);
						LoginDetails.FrmOpenDayIsPrintA4 = false;
					}
					else
					{
						MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					return;
				}
			}
			MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception)
		{
			MessageBox.Show(" PrintA4 عفوا، لم يتم الطباعة ", "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PrintSecondCasheir(int Code)
	{
		try
		{
			if (!LoginDetails.IsPrintSecondCasheir)
			{
				return;
			}
			if (Code > 0)
			{
				using (SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer))
				{
					DataSetALL dataSetALL = new DataSetALL();
					dataSetALL.Tables["TapleItemGroupToCashier"].Clear();
					SqlCommand sqlCommand = new SqlCommand("Get_ItemGroupToPrintCashier", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlCommand.Parameters.AddWithValue("@ID", Code);
					sqlDataAdapter.Fill(dataSetALL.TapleItemGroupToCashier);
					foreach (DataSetALL.TapleItemGroupToCashierRow item in dataSetALL.TapleItemGroupToCashier)
					{
						if (item.Printer != string.Empty)
						{
							dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
							SqlCommand sqlCommand2 = new SqlCommand("Print_Order_CashierRes", sqlConnection);
							sqlCommand2.CommandType = CommandType.StoredProcedure;
							SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2);
							sqlCommand2.Parameters.AddWithValue("@ID", PrintID);
							sqlCommand2.Parameters.AddWithValue("@PrinterID", Convert.ToInt32(item.ID));
							sqlCommand2.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
							sqlDataAdapter2.Fill(dataSetALL.Order_PurchasesTablePrint);
							if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
							{
								StiReport stiReport = new StiReport();
								if (LoginDetails.Lang == "ar-EG")
								{
									stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirSecond.mrt");
								}
								else
								{
									stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCashirSecond.mrt");
								}
								stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
								stiReport.Dictionary.Synchronize();
								stiReport.Compile();
								if (TxtVatNumCustomer != string.Empty)
								{
									stiReport["Title"] = "فاتورة ضريبية";
								}
								else
								{
									stiReport["Title"] = "فاتورة ضريبية مبسطة";
								}
								stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
								stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
								stiReport["VatNumComp"] = LoginDetails.VatNum;
								stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
								stiReport["Address"] = LoginDetails.Address;
								stiReport["Phone"] = LoginDetails.Phone;
								stiReport["MobileComp"] = LoginDetails.Mobile;
								stiReport["Bank"] = LoginDetails.CompanyBank;
								stiReport["CashMoney"] = Cash.ToString();
								stiReport["CashBank"] = Bank.ToString();
								stiReport["MoneyNeed"] = MoneyNeed.ToString();
								stiReport["MoneyPaying"] = MoneyPaying.ToString();
								stiReport["MoneyNet"] = MoneyNet.ToString();
								stiReport["MobileComp"] = LoginDetails.Mobile;
								stiReport["AddressComp"] = LoginDetails.Address;
								stiReport["PrintComp"] = LoginDetails.PrintComp;
								stiReport["CustName"] = TxtSuppliers.Text;
								stiReport["CustVatNum"] = TxtNumVat.Text;
								stiReport["Custphone"] = TxtPhone.Text;
								stiReport["Tafqit"] = txtWordAr;
								stiReport["EmployeeName"] = LoginDetails.UserName;
								Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
								string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), Math.Round(LoginDetails.NetCash + LoginDetails.VatCash, 2).ToString(), LoginDetails.VatCash.ToString());
								stiReport["EncHash"] = value;
								PrinterSettings printerSettings = new PrinterSettings
								{
									PrinterName = item.Printer
								};
								stiReport.Print(showPrintDialog: false, printerSettings);
								stiReport.Show(dialogForm: false);
							}
						}
					}
					sqlConnection.Close();
					return;
				}
			}
			MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception)
		{
			MessageBox.Show("عفوا، لم يتم الطباعة PrintSecondCasheir22 ", "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PreviewPrint(int Code)
	{
		try
		{
			if (LoginDetails.IsPrintOrder)
			{
				PreviewPrintDataTable(Code);
			}
			else if (Code > 0)
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				ST.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_Orders", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@ID", Code);
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlDataAdapter.Fill(ST.Order_PurchasesTablePrint);
				sqlConnection.Close();
				if (ST.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					if (LoginDetails.Lang == "ar-EG")
					{
						if (LoginDetails.IsElectronicInvoice)
						{
							stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirZatca.mrt");
						}
						else
						{
							stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashir.mrt");
						}
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCashir.mrt");
					}
					stiReport.RegData(ST.Order_PurchasesTablePrint);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					if (TxtVatNumCustomer != string.Empty)
					{
						stiReport["Title"] = "فاتورة ضريبية";
					}
					else
					{
						stiReport["Title"] = "فاتورة ضريبية مبسطة";
					}
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["Type"] = TxtType.Text;
					stiReport["Date"] = TxtDate.DateTime;
					stiReport["PurchID"] = Code;
					stiReport["CustName"] = TxtSuppliers.Text;
					stiReport["CustVatNum"] = string.Empty;
					stiReport["Custphone"] = string.Empty;
					stiReport["Tafqit"] = txtWordAr;
					stiReport["EmployeeName"] = LoginDetails.UserName;
					stiReport["CashMoney"] = Cash.ToString();
					stiReport["CashBank"] = Bank.ToString();
					stiReport["MoneyNeed"] = MoneyNeed.ToString();
					stiReport["MoneyPaying"] = MoneyPaying.ToString();
					stiReport["MoneyNet"] = MoneyNet.ToString();
					stiReport["MobileComp"] = LoginDetails.Mobile;
					stiReport["AddressComp"] = LoginDetails.Address;
					stiReport["PrintComp"] = LoginDetails.PrintComp;
					Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
					string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), Math.Round(LoginDetails.NetCash + LoginDetails.VatCash, 1).ToString(), LoginDetails.VatCash.ToString());
					if (LoginDetails.IsElectronicInvoice)
					{
						string text = LoginDetails.EncHash.Trim().Replace("\r", "").Replace("\n", "");
						Image value2 = QRHelper.GenerateQRCode(text);
						stiReport["QrZatca"] = value2;
						stiReport["EncHash"] = text;
					}
					else
					{
						stiReport["EncHash"] = value;
					}
					string value3 = (toggleCashierType.IsOn ? "طلب محلي" : "طلب سفري");
					stiReport["CashierType"] = value3;
					stiReport["Room"] = Convert.ToInt32(TxtRoom.Value);
					stiReport["Table"] = Convert.ToInt32(TxtTable.Value);
					stiReport.Show(dialogForm: true);
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
		catch (Exception)
		{
			MessageBox.Show("عفوا، لم يتم الطباعة ", "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PrintSecondDefualt(int Code)
	{
		try
		{
			if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 157))
			{
				if (Code > 0)
				{
					using (SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer))
					{
						DataSetALL dataSetALL = new DataSetALL();
						dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
						SqlCommand sqlCommand = new SqlCommand("Print_Order_OrdersAfterReturn", sqlConnection);
						sqlCommand.CommandType = CommandType.StoredProcedure;
						SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
						sqlCommand.Parameters.AddWithValue("@ID", Code);
						sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
						sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
						sqlConnection.Close();
						if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
						{
							StiReport stiReport = new StiReport();
							stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirRestaurant.mrt");
							stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
							stiReport.Dictionary.Synchronize();
							stiReport.Compile();
							stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
							stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
							stiReport["VatNumComp"] = LoginDetails.VatNum;
							stiReport["PrintComp"] = LoginDetails.PrintComp;
							stiReport["MobileComp"] = LoginDetails.Mobile;
							stiReport["AddressComp"] = LoginDetails.Address;
							Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
							string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString());
							stiReport["EncHash"] = value;
							PrinterSettings printerSettings = new PrinterSettings
							{
								PrinterName = LoginDetails.SecondPrintDefualt
							};
							stiReport.Print(showPrintDialog: false, printerSettings);
							stiReport.Show(dialogForm: true);
						}
						else
						{
							MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						return;
					}
				}
				MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception)
		{
			MessageBox.Show("عفوا، لم يتم الطباعة ", "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PrintItemCook(int Code)
	{
		try
		{
			if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 157))
			{
				if (Code > 0)
				{
					using (SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer))
					{
						DataSetALL dataSetALL = new DataSetALL();
						dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
						SqlCommand sqlCommand = new SqlCommand("Print_Order_OrdersAfterReturn", sqlConnection);
						sqlCommand.CommandType = CommandType.StoredProcedure;
						SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
						sqlCommand.Parameters.AddWithValue("@ID", Code);
						sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
						sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
						sqlConnection.Close();
						if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
						{
							StiReport stiReport = new StiReport();
							stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirSecond.mrt");
							stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
							stiReport.Dictionary.Synchronize();
							stiReport.Compile();
							stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
							stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
							stiReport["VatNumComp"] = LoginDetails.VatNum;
							stiReport["PrintComp"] = LoginDetails.PrintComp;
							stiReport["MobileComp"] = LoginDetails.Mobile;
							stiReport["AddressComp"] = LoginDetails.Address;
							Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
							string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString());
							stiReport["EncHash"] = value;
							PrinterSettings printerSettings = new PrinterSettings
							{
								PrinterName = LoginDetails.SecondPrintDefualt
							};
							stiReport.Print(showPrintDialog: false, printerSettings);
						}
						else
						{
							MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						return;
					}
				}
				MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception)
		{
			MessageBox.Show("عفوا، لم يتم الطباعة ", "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void IsEnableTrue()
	{
		TxtBarcode.Enabled = true;
		TxtDiscount.ReadOnly = false;
		TxtType.Enabled = true;
		TxtDate.ReadOnly = false;
		TxtSuppliers.ReadOnly = false;
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		BtnSave.Enabled = false;
		BtnEdit.Enabled = true;
		BtnDelete.Enabled = true;
		TxtSuppliers.Focus();
		gridView1.OptionsBehavior.Editable = true;
	}

	public void SetInitialRowPrinter()
	{
		CashierPrint.Columns.Add("ID", typeof(int));
		CashierPrint.Columns.Add("Name", typeof(string));
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

	public void SetInitialRow()
	{
		datatable.Columns.Add("ItemID", typeof(int));
		datatable.Columns.Add("ItemUnitID", typeof(int));
		datatable.Columns.Add("ItemNote", typeof(string));
		datatable.Columns.Add("ItemCode", typeof(string));
		datatable.Columns.Add("ItemName", typeof(string));
		datatable.Columns.Add("Group", typeof(string));
		datatable.Columns.Add("UnitType", typeof(string));
		datatable.Columns.Add("UnitID", typeof(int));
		datatable.Columns.Add("StoreName", typeof(string));
		datatable.Columns.Add("StoreID", typeof(int));
		datatable.Columns.Add("Quantity", typeof(decimal));
		datatable.Columns.Add("AvergItem", typeof(decimal));
		datatable.Columns.Add("SumAvergItem", typeof(decimal));
		datatable.Columns.Add("UnitPrice", typeof(decimal));
		datatable.Columns.Add("UnitPriceDisc", typeof(decimal));
		datatable.Columns.Add("PercentDisc", typeof(decimal));
		datatable.Columns.Add("SumPriceDisc", typeof(decimal));
		datatable.Columns.Add("IsItemTax", typeof(int));
		datatable.Columns.Add("Tax_Value", typeof(decimal));
		datatable.Columns.Add("UnitName", typeof(string));
		datatable.Columns.Add("SumPrice", typeof(decimal));
		datatable.Columns.Add("Net", typeof(decimal));
		datatable.Columns.Add("ItemVat", typeof(decimal));
		datatable.Columns.Add("ItemVatDisc", typeof(decimal));
		datatable.Columns.Add("Total", typeof(decimal));
		datatable.Columns.Add("حذف الصنف", typeof(int));
		datatable.Columns.Add("SN", typeof(int));
		datatable.Columns.Add("Delete", typeof(int));
		datatable.Columns.Add("Done", typeof(int));
		datatable.Columns.Add("PrinterID", typeof(int));
		datatable.Columns.Add("PrinterName", typeof(string));
		datatable.Columns.Add("OrginalPrice", typeof(decimal));
		datatable.Columns.Add("OrginalTotalPrice", typeof(decimal));
		datatable.Columns.Add("VatCode", typeof(string));
		datatable.Columns.Add("TaxExemptionReason", typeof(string));
		datatable.Columns.Add("TaxExemptionReasonCode", typeof(string));
		datatable.Columns.Add("AmountDisc", typeof(decimal));
		datatable.Columns.Add("BounceItem", typeof(decimal));
		datatable.Columns.Add("Height", typeof(decimal));
		datatable.Columns.Add("Width", typeof(decimal));
		datatable.Columns.Add("Long", typeof(decimal));
		datatable.Columns.Add("Weight", typeof(decimal));
		datatable.Columns.Add("TaxTobacco", typeof(decimal));
		datatable.Columns.Add("TaxTobaccoDis", typeof(decimal));
		datatable.Columns.Add("TotalWithTaxTobacco", typeof(decimal));
		datatable.Columns.Add("InstallationPrice", typeof(decimal));
		datatable.Columns.Add("InstallPriceDis", typeof(decimal));
		dtCurrentTable.Columns.Add("SN", typeof(int));
		dtCurrentTable.Columns.Add("Purchese_ID", typeof(int));
		dtCurrentTable.Columns.Add("ItemID", typeof(int));
		dtCurrentTable.Columns.Add("BranchID", typeof(int));
		dtCurrentTable.Columns.Add("IsWaiting", typeof(bool));
		dtCurrentTable.Columns.Add("StoreID", typeof(int));
		dtCurrentTable.Columns.Add("ItemUnitID", typeof(int));
		dtCurrentTable.Columns.Add("Quantity", typeof(decimal));
		dtCurrentTable.Columns.Add("LastCost", typeof(decimal));
		dtCurrentTable.Columns.Add("SmallUnitPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("UnitPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("TotalPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("VAT", typeof(decimal));
		dtCurrentTable.Columns.Add("NetUnitPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("NetTotalPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("VAT_Discount", typeof(decimal));
		dtCurrentTable.Columns.Add("ItemUnitType", typeof(string));
		dtCurrentTable.Columns.Add("IsPrint", typeof(bool));
		dtCurrentTable.Columns.Add("ItemNote", typeof(string));
		dtCurrentTable.Columns.Add("IsPrintCook", typeof(bool));
		dtCurrentTable.Columns.Add("UnitNumber", typeof(decimal));
		dtCurrentTable.Columns.Add("Weight", typeof(decimal));
		dtCurrentTable.Columns.Add("Height", typeof(decimal));
		dtCurrentTable.Columns.Add("DetailsData", typeof(string));
		dtCurrentTable.Columns.Add("width", typeof(decimal));
		dtCurrentTable.Columns.Add("Long", typeof(decimal));
		dtCurrentTable.Columns.Add("Bounce", typeof(decimal));
		dtCurrentTable.Columns.Add("Amount_Discount", typeof(decimal));
		dtCurrentTable.Columns.Add("Per_Discount", typeof(decimal));
		dtCurrentTable.Columns.Add("TafqitId", typeof(int));
		dtCurrentTable.Columns.Add("TafqitQuintity", typeof(decimal));
		dtCurrentTable.Columns.Add("TaxTobacco", typeof(decimal));
		dtCurrentTable.Columns.Add("TotalWithTaxTobacco", typeof(decimal));
		dtCurrentTable.Columns.Add("TaxTobaccoDis", typeof(decimal));
		dtCurrentTable.Columns.Add("InstallationPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("InstallPriceDis", typeof(decimal));
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridView1.RowCellClick += gridView1_RowCellClick;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
	}

	public decimal ConvertToDecimalOrDefault(object value)
	{
		return (value != DBNull.Value) ? Convert.ToDecimal(value) : 1m;
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
				TxtTabocc.Text = "0.00";
				TxtCost.Text = "0.00";
				TxtPhone.Text = string.Empty;
				TxtNumNote.Text = string.Empty;
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
		}
		catch (Exception)
		{
		}
	}

	private decimal SafeConvertToDecimal(object value)
	{
		return (value == DBNull.Value || value == null) ? 0m : Convert.ToDecimal(value);
	}

	private void No_Discount_NoVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]));
			decimal num2 = (Convert.ToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
				row["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallPriceDis"]) * num);
				row["ItemVatDisc"] = "0";
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
			}
			else
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
				row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
				row["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallPriceDis"]) * num);
			}
		}
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		TxtCost.Text = Convert.ToString(num3);
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
		TxtTotals.Text = Convert.ToString(num4);
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(TaxTobacco)", string.Empty));
		if (LoginDetails.IsTobacc)
		{
			TxtTabocc.Text = Convert.ToString(num5);
		}
		else
		{
			TxtTabocc.Text = "0.00";
		}
		TxtVat.Text = "0.00";
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = "0.00";
		TxtNet.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtTabocc.Text), DecimalNum).ToString();
	}

	private void No_Discount_Vat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			row["Total"] = Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]);
			if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
				decimal num2 = ((row["InstallPriceDis"] == DBNull.Value || row["InstallPriceDis"] == null) ? 0m : Convert.ToDecimal(row["InstallPriceDis"]));
				row["InstallPriceDis"] = FUNC_DecimalNum(num2 * num);
				if (Convert.ToBoolean(row["IsItemTax"]))
				{
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
				}
				else
				{
					row["ItemVatDisc"] = 0;
				}
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
			}
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		TxtCost.Text = Convert.ToString(num3);
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		decimal num5 = ((datatable.Compute("SUM(TaxTobacco)", string.Empty) is DBNull) ? 0m : Convert.ToDecimal(datatable.Compute("SUM(TaxTobacco)", string.Empty)));
		TxtTotals.Text = Convert.ToString(num4);
		if (LoginDetails.IsTobacc)
		{
			TxtTabocc.Text = Convert.ToString(num5);
		}
		else
		{
			TxtTabocc.Text = "0.00";
		}
		decimal num6 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		decimal num7 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num6.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = num7.ToString();
		TxtNet.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVatDisc.Text) + Convert.ToDecimal(TxtTabocc.Text), DecimalNum).ToString();
	}

	private void No_Discount_PriceWithVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = ConvertToDecimalOrDefault(row["Quantity"]);
			decimal num2 = ConvertToDecimalOrDefault(row["Height"]);
			decimal num3 = ConvertToDecimalOrDefault(row["Width"]);
			decimal num4 = ConvertToDecimalOrDefault(row["Long"]);
			decimal num5 = ConvertToDecimalOrDefault(row["Weight"]);
			decimal num6 = ConvertToDecimalOrDefault(row["BounceItem"]);
			decimal num7 = num * num2 * num3 * num4 * num5;
			decimal num8 = (num + num6) * num2 * num3 * num4 * num5;
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			row["UnitPrice"] = Math.Round(Convert.ToDecimal(row["UnitPrice"]), 2);
			decimal num9 = ((row["InstallationPrice"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["InstallationPrice"]));
			if (LoginDetails.InstallAll == 2)
			{
				row["InstallPriceDis"] = FUNC_DecimalNum(num9);
			}
			else if (LoginDetails.InstallAll == 1)
			{
				row["InstallPriceDis"] = FUNC_DecimalNum(num9 * num8);
			}
			else
			{
				row["InstallPriceDis"] = 0;
			}
			decimal num10 = ((row["Total"] != DBNull.Value) ? Convert.ToDecimal(row["Total"]) : 0m);
			decimal num11 = ((row["Tax_Value"] != DBNull.Value) ? Convert.ToDecimal(row["Tax_Value"]) : 0m);
			decimal value = num10 / (num11 / 100m + 1m);
			row["SumPrice"] = FUNC_DecimalNum(value);
			if (Convert.ToBoolean(row["IsItemTax"]))
			{
				row["ItemVat"] = FUNC_DecimalNum(num10 - Convert.ToDecimal(row["SumPrice"]));
			}
			else
			{
				row["ItemVat"] = 0;
			}
			if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
				row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				decimal num12 = Convert.ToDecimal(row["Net"]);
			}
			else
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
				decimal num13 = 0m;
				if (row.Table.Columns.Contains("SumInstallationDis") && row["SumInstallationDis"] != DBNull.Value)
				{
					num13 = Convert.ToDecimal(row["SumInstallationDis"]);
				}
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num7 + num13);
				if (Convert.ToBoolean(row["IsItemTax"]))
				{
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
				}
				else
				{
					row["ItemVatDisc"] = 0;
				}
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
			}
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num8);
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num14 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		TxtCost.Text = Convert.ToString(num14);
		decimal num15 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num15);
		decimal num16 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num16.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = TxtVat.Text;
		decimal num17 = FUNC_DecimalNum(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text));
		decimal num18 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
		TxtNet.Text = num18.ToString();
	}

	private void DiscountPercent_NoVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			decimal num2 = (Convert.ToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
					row["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) + Convert.ToDecimal(row["InstallationPrice"]));
				}
				else
				{
					decimal num3 = FUNC_DecimalNum(Convert.ToDecimal(row["InstallPriceDis"]) * Convert.ToDecimal(row["Quantity"]));
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) - Convert.ToDecimal(row["UnitPriceDisc"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["InstallPriceDis"] = FUNC_DecimalNum(num3 - num3 * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num + Convert.ToDecimal(row["InstallPriceDis"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
			}
		}
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num4);
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
			if (((Convert.ToDecimal(row2["AmountDisc"]) == 0m || row2["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row2["PercentDisc"]) == 0m) || row2["PercentDisc"].ToString() == string.Empty)
			{
				decimal num5 = Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
				decimal num6 = (Convert.ToDecimal(row2["Quantity"]) + SafeConvertToDecimal(row2["BounceItem"])) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
				if (Convert.ToInt32(row2["ItemID"]) > 0)
				{
					decimal value = FUNC_DecimalNum(Convert.ToDecimal(row2["InstallPriceDis"]) * Convert.ToDecimal(row2["Quantity"]));
					row2["ItemVat"] = 0;
					row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
					row2["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(value) - Convert.ToDecimal(value) * (PerNum / 100m));
					row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num5 + Convert.ToDecimal(row2["InstallPriceDis"]));
					row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["ItemVat"]));
					row2["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]));
					row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["Total"]));
					row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num6);
				}
			}
		}
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	private void DiscountPercent_Vat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			decimal num2 = (Convert.ToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
				row["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]));
				row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) + Convert.ToDecimal(row["InstallationPrice"]));
			}
			else
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) - Convert.ToDecimal(row["UnitPriceDisc"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
				row["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallPriceDis"]) - Convert.ToDecimal(row["InstallPriceDis"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
				row["SumPriceDisc"] = FUNC_DecimalNum((Convert.ToDecimal(row["UnitPriceDisc"]) + Convert.ToDecimal(row["UnitPriceDisc"])) * num);
				if (Convert.ToBoolean(row["IsItemTax"]))
				{
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
				}
				else
				{
					row["ItemVatDisc"] = 0;
				}
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
			}
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
		}
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat.Text = Convert.ToString(num3);
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num4);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num3) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num3) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(TxtTotals.Text) + num3) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal value = FUNC_DecimalNum(Convert.ToDecimal(row2["InstallPriceDis"]) * Convert.ToDecimal(row2["Quantity"]));
			decimal num5 = Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
			decimal num6 = (Convert.ToDecimal(row2["Quantity"]) + SafeConvertToDecimal(row2["BounceItem"])) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(value) - Convert.ToDecimal(value) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num5 + Convert.ToDecimal(row2["InstallPriceDis"]));
				if (Convert.ToBoolean(row2["IsItemTax"]))
				{
					row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m));
				}
				else
				{
					row2["ItemVatDisc"] = 0;
				}
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num6);
			}
		}
		decimal num7 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVatDisc.Text = Convert.ToString(num7);
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	private void DiscountPercent_PriceWithVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			decimal num2 = (Convert.ToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			decimal num3 = FUNC_DecimalNum((row["InstallationPrice"] == null || Convert.IsDBNull(row["InstallationPrice"])) ? 0m : Convert.ToDecimal(row["InstallationPrice"]));
			if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
				row["InstallPriceDis"] = num3;
				row["ItemVatDisc"] = row["ItemVat"];
				row["SumPriceDisc"] = row["SumPrice"];
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]) + num3);
				decimal num4 = Convert.ToDecimal(row["Net"]);
			}
			else
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
				if (Convert.ToBoolean(row["IsItemTax"]))
				{
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
				}
				else
				{
					row["ItemVatDisc"] = 0;
				}
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]) + num3);
			}
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
		}
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat.Text = Convert.ToString(num5);
		decimal num6 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num6);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = Math.Round((Convert.ToDecimal(TxtTotals.Text) + num5) * (PerNum / 100m), 2).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num5) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(TxtTotals.Text) + num5) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num7 = Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
			decimal num8 = (Convert.ToDecimal(row2["Quantity"]) + SafeConvertToDecimal(row2["BounceItem"])) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * Convert.ToDecimal(num7));
				if (Convert.ToBoolean(row2["IsItemTax"]))
				{
					row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m));
				}
				else
				{
					row2["ItemVatDisc"] = 0;
				}
				decimal num9 = Convert.ToDecimal(row2["SumPriceDisc"]);
				decimal num10 = Convert.ToDecimal(row2["ItemVatDisc"]);
				row2["InstallPriceDis"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * num7);
				row2["Net"] = Math.Round(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]), 1);
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num8);
			}
		}
		decimal num11 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVatDisc.Text = Convert.ToString(num11);
		TxtNet.Text = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text), 1).ToString();
	}

	private void Vat(bool? Is_Tax, decimal? Tax_Value, decimal? Tax_Tobacco, decimal? Unitprice, decimal AllQuatinty, DataRow dr, string ColumnField)
	{
		decimal? num = Tax_Tobacco;
		if ((num.GetValueOrDefault() > 0m) & num.HasValue)
		{
			if (ORD.IsVatActive())
			{
				if (Is_Tax == true)
				{
					decimal num2 = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty);
					dr["Sumprice"] = num2;
					decimal num3 = num2 * Convert.ToDecimal(Tax_Tobacco / (decimal?)100);
					num = Unitprice;
					decimal minTobaccoTax = LoginDetails.MinTobaccoTax;
					num3 = (((num.GetValueOrDefault() <= minTobaccoTax) & num.HasValue) ? (LoginDetails.MinTobaccoTax * AllQuatinty) : num3);
					dr["TaxTobacco"] = FUNC_DecimalNum(num3);
					dr["TotalWithTaxTobacco"] = FUNC_DecimalNum(num2 + num3);
					dr["ItemVat"] = FUNC_DecimalNum((decimal?)(num2 + num3) * Tax_Value / (decimal?)100);
					dr["Total"] = FUNC_DecimalNum(Convert.ToDecimal(dr["TotalWithTaxTobacco"]) + Convert.ToDecimal(dr["ItemVat"]));
					dr["IsItemTax"] = 1;
					dr["Tax_Value"] = Tax_Value;
				}
				else
				{
					decimal num4 = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty);
					dr["Total"] = num4;
					decimal num5 = num4 * Convert.ToDecimal(Tax_Tobacco / (decimal?)100);
					num = Unitprice;
					decimal minTobaccoTax = LoginDetails.MinTobaccoTax;
					num5 = (((num.GetValueOrDefault() <= minTobaccoTax) & num.HasValue) ? (LoginDetails.MinTobaccoTax * AllQuatinty) : num5);
					dr["TaxTobacco"] = FUNC_DecimalNum(num5);
					dr["TotalWithTaxTobacco"] = FUNC_DecimalNum(num4 + num5);
					dr["ItemVat"] = 0;
					dr["Total"] = dr["TotalWithTaxTobacco"];
					dr["IsItemTax"] = 0;
					dr["Tax_Value"] = 0;
				}
			}
			else
			{
				decimal num6 = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty);
				dr["SumPrice"] = num6;
				decimal num7 = num6 * Convert.ToDecimal(Tax_Tobacco / (decimal?)100);
				num = Unitprice;
				decimal minTobaccoTax = LoginDetails.MinTobaccoTax;
				num7 = (((num.GetValueOrDefault() <= minTobaccoTax) & num.HasValue) ? (LoginDetails.MinTobaccoTax * AllQuatinty) : num7);
				dr["TaxTobacco"] = FUNC_DecimalNum(num7);
				dr["TotalWithTaxTobacco"] = FUNC_DecimalNum(num6 + num7);
				dr["ItemVat"] = 0;
				dr["Total"] = dr["TotalWithTaxTobacco"];
				dr["IsItemTax"] = 0;
				dr["Tax_Value"] = 0;
			}
		}
		else if (ORD.IsVatActive())
		{
			if (Is_Tax == true)
			{
				decimal num8 = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty);
				dr["SumPrice"] = num8;
				dr["TaxTobacco"] = "0";
				dr["TotalWithTaxTobacco"] = FUNC_DecimalNum(num8);
				decimal num9 = Math.Round(num8 * Convert.ToDecimal(Tax_Value / (decimal?)100), 2);
				dr["ItemVat"] = num9;
				decimal num10 = FUNC_DecimalNum(Convert.ToDecimal(num8) + Convert.ToDecimal(dr["ItemVat"]));
				dr["Total"] = num10;
				dr["IsItemTax"] = 1;
				dr["Tax_Value"] = Tax_Value;
			}
			else
			{
				dr["SumPrice"] = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty);
				dr["ItemVat"] = 0;
				dr["TaxTobacco"] = "0";
				dr["Total"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]));
				dr["TotalWithTaxTobacco"] = dr["Total"];
				dr["IsItemTax"] = 0;
				dr["Tax_Value"] = 0;
			}
		}
		else
		{
			dr["SumPrice"] = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty);
			dr["ItemVat"] = 0;
			dr["TaxTobacco"] = "0";
			dr["Total"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]));
			dr["TotalWithTaxTobacco"] = dr["Total"];
			dr["IsItemTax"] = 0;
			dr["Tax_Value"] = 0;
		}
	}

	private void VatWithTotal(bool? Is_Tax, decimal? Tax_Value, decimal? SumPrice, decimal? Totalprice, decimal AllQuatinty, DataRow dr, string ColumnField)
	{
		if (ORD.IsVatActive())
		{
			if (ColumnField == "InstallationPrice")
			{
				dr["Total"] = FUNC_DecimalNum(Totalprice);
				dr["SumPrice"] = FUNC_DecimalNum(SumPrice);
				dr["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(dr["Total"]) - Convert.ToDecimal(dr["SumPrice"]));
				dr["IsItemTax"] = 1;
				dr["Tax_Value"] = (Tax_Value - (decimal?)1) * (decimal?)100;
			}
			else if (Is_Tax == true)
			{
				decimal num = FUNC_DecimalNum(Convert.ToDecimal(Totalprice) * Convert.ToDecimal(Tax_Value));
				if (num >= 100m)
				{
					dr["Total"] = Math.Round(Convert.ToDecimal(num), 1);
				}
				else
				{
					dr["Total"] = Math.Round(Convert.ToDecimal(num), 1);
				}
				decimal num2 = Convert.ToDecimal(dr["Total"]);
				dr["SumPrice"] = FUNC_DecimalNum(Totalprice);
				dr["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(dr["Total"]) - Convert.ToDecimal(dr["SumPrice"]));
				dr["IsItemTax"] = 1;
				dr["Tax_Value"] = (Tax_Value - (decimal?)1) * (decimal?)100;
			}
			else
			{
				decimal value = FUNC_DecimalNum(Convert.ToDecimal(Totalprice));
				dr["Total"] = Math.Round(Convert.ToDecimal(value), 1);
				dr["SumPrice"] = FUNC_DecimalNum(Totalprice);
				dr["ItemVat"] = 0;
				dr["IsItemTax"] = 0;
				dr["Tax_Value"] = 0;
			}
		}
		else
		{
			dr["ItemVat"] = 0;
			dr["Total"] = FUNC_DecimalNum(Totalprice - (decimal?)Convert.ToDecimal(dr["AmountDisc"]));
			dr["SumPrice"] = FUNC_DecimalNum(Totalprice);
			dr["IsItemTax"] = 0;
			dr["Tax_Value"] = 0;
		}
	}

	public void GetData(int orderId, int BranchID, string storedProcedureName)
	{
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
		sqlConnection.Open();
		try
		{
			using SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.AddWithValue("@PurBranchID", orderId);
			sqlCommand.Parameters.AddWithValue("@BranchID", BranchID);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);
			if (dataSet.Tables.Count > 0)
			{
				DataTable dataTable = dataSet.Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					TxtType.Text = ORD.GetAccountNameById(Convert.ToInt32(dataRow["Order_Paymant_Type"]));
					BtnCustomerSearch.Text = dataRow["SupplierID"].ToString();
					TxtCode.Text = dataRow["ID"].ToString();
					LblSup.Text = dataRow["SupplierID"].ToString();
					TxtSuppliers.Text = dataRow["SupplierName"].ToString();
					TxtSuppliers2.Text = dataRow["SupplierName"].ToString();
					TxtPhone.Text = dataRow["SupplierPhone"].ToString();
					_TxtPhone.Text = dataRow["SupplierPhone"].ToString();
					TxtAdditionalNum.Text = dataRow["AdditionalNum"].ToString();
					TxtPostalCode.Text = dataRow["PostalCode"].ToString();
					TxtCommercialRecord.Text = dataRow["CommercialRecord"].ToString();
					TxtSales.Text = dataRow["SalesMan"].ToString();
					TxtSales2.Text = dataRow["SalesMan"].ToString();
					TxtCity.Text = dataRow["City"].ToString();
					TxtStreet.Text = dataRow["Street"].ToString();
					TxtDistrict.Text = dataRow["District"].ToString();
					TxtCountry.Text = dataRow["Country"].ToString();
					TxtNumVat.Text = dataRow["SupplierVatNum"].ToString();
					_TxtVatNum.Text = dataRow["SupplierVatNum"].ToString();
					TxtNote.Text = dataRow["Note"].ToString();
					TxtNumNote.Text = dataRow["NoteNum"].ToString();
					TxtTotals.Text = dataRow["TotalPrices"].ToString();
					TxtVat.Text = dataRow["Tax"].ToString();
					TxtDiscount.Text = dataRow["DiscountNum"].ToString();
					txtdicoutprege.Text = dataRow["DiscountPerantage"].ToString();
					TxtVatDisc.Text = dataRow["Tax_Discount"].ToString();
					if (storedProcedureName == "GetPriceOffersWithDetails")
					{
						TxtTotalDiscount.Text = dataRow["Safy"].ToString();
					}
					else
					{
						TxtTotalDiscount.Text = dataRow["TotalPrices_Discount"].ToString();
						TxtAllDiscount.Text = dataRow["AllDiscount"].ToString();
						BtnDisCode.Text = dataRow["DisCode"].ToString();
						BtnDisCode.Enabled = false;
						TxtDate.DateTime = Convert.ToDateTime(dataRow["Purchases_Date"]);
						toggleHold.IsOn = Convert.ToBoolean(dataRow["IsWaiting"]);
						DateHold.DateTime = Convert.ToDateTime(dataRow["DateHold"]);
					}
					TxtNet.Text = dataRow["Net"].ToString();
					TxtCost.Text = dataRow["CostOrder"].ToString();
					TxtTabocc.Text = dataRow["TobaccoTax"].ToString();
					LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum);
					LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum);
					FrmCash22 = (dataRow.IsNull("CashMoney") ? 0m : Convert.ToDecimal(dataRow["CashMoney"]));
					FrmBank22 = (dataRow.IsNull("CashBank") ? 0m : Convert.ToDecimal(dataRow["CashBank"]));
				}
				if (dataSet.Tables.Count >= 1)
				{
					DataTable dataTable2 = dataSet.Tables[0];
					datatable.Rows.Clear();
					foreach (DataRow row in dataTable2.Rows)
					{
						DataRow dataRow3 = datatable.NewRow();
						dataRow3["ItemID"] = row["ItemID"] ?? ((object)0);
						dataRow3["ItemUnitID"] = row["ItemUnitID"] ?? ((object)0);
						dataRow3["ItemNote"] = row["ItemNote"] ?? "";
						dataRow3["ItemCode"] = row["item_Code"] ?? "";
						dataRow3["ItemName"] = row["item_Name"] ?? "";
						dataRow3["BounceItem"] = row["Bounce"] ?? ((object)0);
						dataRow3["Height"] = row["Height"] ?? ((object)1);
						dataRow3["Width"] = row["Width"] ?? ((object)1);
						dataRow3["Long"] = row["Long"] ?? ((object)1);
						dataRow3["Weight"] = row["Weight"] ?? ((object)1);
						dataRow3["AmountDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["Amount_Discount"]));
						dataRow3["PercentDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["Per_Discount"]));
						dataRow3["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["NetTotalPrice"]) - Convert.ToDecimal(row["VAT_Discount"]));
						dataRow3["ItemUnitID"] = row["ItemUnitID"] ?? ((object)1);
						if (storedProcedureName == "GetOrdersWithDetails")
						{
							dataRow3["InstallationPrice"] = FUNC_DecimalNum((row["PriceInstall"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["PriceInstall"]));
							dataRow3["InstallPriceDis"] = FUNC_DecimalNum((row["PriceInstallDise"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["PriceInstallDise"]));
							dataRow3["Net"] = FUNC_DecimalNum((row["NetTotalPrice"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["NetTotalPrice"]));
						}
						else
						{
							dataRow3["InstallationPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["PriceExportunit1"]));
							dataRow3["InstallPriceDis"] = 0;
							dataRow3["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["NetTotalPrice"]));
						}
						decimal num = FUNC_DecimalNum((Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["Bounce"])) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Width"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Weight"]);
						decimal num2 = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * (Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Width"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Weight"]));
						decimal num3 = FUNC_DecimalNum(Convert.IsDBNull(row["LastCost"]) ? 0m : Convert.ToDecimal(row["LastCost"]));
						dataRow3["UnitType"] = row["ItemUnitType"] ?? "";
						dataRow3["UnitName"] = row["UnitName"] ?? "";
						dataRow3["StoreID"] = row["StoreID"] ?? ((object)1);
						dataRow3["StoreName"] = row["Store_Name"] ?? "";
						dataRow3["Quantity"] = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]));
						dataRow3["AvergItem"] = num3;
						dataRow3["SumAvergItem"] = FUNC_DecimalNum(num3 * num);
						dataRow3["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"] ?? ((object)0)));
						dataRow3["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["NetUnitPrice"] ?? ((object)0)));
						decimal num4 = Convert.ToDecimal(row["VAT"]);
						decimal value = Convert.ToDecimal(row["Tax_Value"]);
						decimal num5 = Convert.ToDecimal(row["UnitPrice"]);
						decimal num6 = Convert.ToDecimal(row["TotalPrice"]);
						if (storedProcedureName == "GetOrdersWithDetails")
						{
							if (LoginDetails.ItemTotalWithVat)
							{
								dataRow3["SumPrice"] = FUNC_DecimalNum(num6);
							}
							else
							{
								dataRow3["SumPrice"] = FUNC_DecimalNum(num6 - num4);
							}
						}
						else
						{
							dataRow3["SumPrice"] = FUNC_DecimalNum(num5 * (decimal)Convert.ToInt32(dataRow3["Quantity"]));
						}
						dataRow3["OrginalPrice"] = FUNC_DecimalNum(num5);
						if (num4 > 0m)
						{
							dataRow3["ItemVat"] = FUNC_DecimalNum(num4);
							if (storedProcedureName == "GetOrdersWithDetails")
							{
								dataRow3["Total"] = FUNC_DecimalNum(num6);
							}
							else
							{
								dataRow3["Total"] = FUNC_DecimalNum(num6);
							}
							dataRow3["IsItemTax"] = 1;
							dataRow3["Tax_Value"] = FUNC_DecimalNum(value);
							dataRow3["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["VAT_Discount"]));
						}
						else
						{
							dataRow3["IsItemTax"] = 0;
							dataRow3["ItemVatDisc"] = 0;
							dataRow3["ItemVat"] = FUNC_DecimalNum(num4);
							dataRow3["Total"] = FUNC_DecimalNum(Convert.ToDecimal(num6) + Convert.ToDecimal(num4));
						}
						datatable.Rows.Add(dataRow3);
					}
					gridControl1.DataSource = datatable;
					decimal num7 = Convert.ToDecimal(datatable.Compute("SUM(InstallationPrice)", string.Empty));
					decimal num8 = FUNC_DecimalNum(Convert.ToDecimal(TxtNet.Text) + num7);
					if (storedProcedureName == "GetPriceOffersWithDetails")
					{
						TxtNet.Text = num8.ToString();
					}
				}
				BtnEdit.Text = "F6 تعديل";
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = false;
				CheckFfistRow();
				ArrangeDatatable();
				IsEnableFalse();
				if (Convert.ToInt32(LblSup.Text) == 0)
				{
					return;
				}
				Charge(Convert.ToInt32(LblSup.Text));
				BtnSave.Enabled = false;
				if (!LoginDetails.IsElectronicInvoice)
				{
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
				}
				else
				{
					BtnEdit.Enabled = false;
					BtnDelete.Enabled = false;
				}
			}
			else
			{
				MessageBox.Show("لا يوجد فاتوره بهذا الرقم");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message);
		}
		sqlConnection.Close();
	}

	public void GetPricePffer()
	{
		try
		{
			using (new GTSdbEntities())
			{
				FrmSearchPriceOffer frmSearchPriceOffer = new FrmSearchPriceOffer();
				frmSearchPriceOffer.ShowDialog();
				if (frmSearchPriceOffer.CustCode > 0)
				{
					int custCode = frmSearchPriceOffer.CustCode;
					GetData(custCode, LoginDetails.BranchID, "GetPriceOffersWithDetails");
				}
			}
			BtnSave.Enabled = true;
			BtnEdit.Enabled = false;
			BtnDelete.Enabled = false;
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void SearchOrderNum(int OrderSearch)
	{
		try
		{
			GetData(OrderSearch, LoginDetails.BranchID, "GetOrdersWithDetails");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		if (LoginDetails.Glasses)
		{
			Order_Extension glassess = ORD.GetGlassess(OrderSearch);
			if (glassess != null)
			{
				TxtRightSph.Text = glassess.RightSph.ToString();
				TxtRightCYL.Text = glassess.RightCYL.ToString();
				TxtRightAxis.Text = glassess.RightAxis.ToString();
				TxtRightAdd.Text = glassess.RightAdd.ToString();
				TxtLeftSph.Text = glassess.LeftSph.ToString();
				TxtLeftCYL.Text = glassess.LeftCYL.ToString();
				TxtLeftAxis.Text = glassess.LeftAxis.ToString();
				TxtLeftAdd.Text = glassess.LeftAdd.ToString();
				TxtNoteGlass.Text = glassess.Note.ToString();
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

	public void openItmCard()
	{
		if (gridControl1.Focus())
		{
			if (CodeItm != string.Empty)
			{
				LoginDetails.ItemSearchID = ORD.GetItmID(CodeItm);
			}
			else
			{
				LoginDetails.ItemSearchID = 0;
			}
		}
		else
		{
			LoginDetails.ItemSearchID = 0;
			CodeItm = string.Empty;
		}
		FrmItems frmItems = new FrmItems();
		frmItems.ShowDialog();
		LoginDetails.ItemSearchID = 0;
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
				foreach (string item in LoginDetails.ItemSearchAll2)
				{
					string text = item.ToString();
					Get_Item_ByCodeALL_Result ItemDetails = ITM.GetItemByCode(text.ToString()).FirstOrDefault();
					Printer printer = ORD.GetPrinter(ItemDetails.ItemId);
					Item_Items item_Items = (from x in ITM.GetItemByCode()
						where x.Item_code == ItemDetails.Item_code
						select x).FirstOrDefault();
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = ItemDetails.ItemId;
					dataRow["ItemUnitID"] = ItemDetails.UnitSmallID;
					dataRow["PrinterID"] = printer.ID;
					dataRow["PrinterName"] = printer.Name;
					dataRow["ItemCode"] = ItemDetails.Item_code;
					dataRow["ItemName"] = ItemDetails.item_Name;
					dataRow["Group"] = ItemDetails.Category_ID;
					dataRow["UnitName"] = ItemDetails.UnitSmall;
					dataRow["Quantity"] = FUNC_DecimalNumQty(1);
					dataRow["InstallationPrice"] = FUNC_DecimalNum(item_Items.PriceExportunit1);
					dataRow["InstallPriceDis"] = FUNC_DecimalNum(item_Items.PriceExportunit1);
					dataRow["Delete"] = 0;
					dataRow["Done"] = 1;
					dataRow["UnitType"] = "الصغرى";
					dataRow["AvergItem"] = ItemDetails.Average_cost;
					dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
					dataRow["StoreName"] = ORD.GetStoreName();
					dataRow["StoreID"] = ORD.GetStoreID();
					dataRow["StoreName"] = Comp_Store.Text;
					dataRow["StoreID"] = Comp_Store.SelectedValue;
					dataRow["BounceItem"] = 0;
					dataRow["Height"] = 1;
					dataRow["Width"] = 1;
					dataRow["Long"] = 1;
					dataRow["Weight"] = 1;
					dataRow["AmountDisc"] = 0;
					dataRow["PercentDisc"] = 0;
					dataRow["VatCode"] = ItemDetails.VatCode;
					dataRow["TaxExemptionReason"] = ItemDetails.TaxExemptionReason;
					dataRow["TaxExemptionReasonCode"] = ItemDetails.TaxExemptionReasonCode;
					decimal num = FUNC_DecimalNum(ItemDetails.SellPriceSmall + item_Items.PriceExportunit1);
					if (LoginDetails.ItemTotalWithVat)
					{
						decimal num2 = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
						decimal num3 = ItemDetails.SellPriceSmall.GetValueOrDefault() / num2 + item_Items.PriceExportunit1.GetValueOrDefault();
						if (toggleSectoral.IsOn)
						{
							dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall / (decimal?)num2);
							dataRow["Total"] = num3;
							dataRow["OrginalPrice"] = dataRow["Total"];
							VatWithTotal(ItemDetails.Is_Tax, num2, num3, Convert.ToDecimal(dataRow["Total"]), Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
						}
						else
						{
							decimal num4 = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 / (decimal?)num2 + item_Items.PriceExportunit1);
							dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 / (decimal?)num2);
							dataRow["Total"] = num4;
							dataRow["OrginalPrice"] = dataRow["Total"];
							VatWithTotal(ItemDetails.Is_Tax, num2, num4, Convert.ToDecimal(dataRow["Total"]), Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
						}
					}
					else
					{
						if (toggleSectoral.IsOn)
						{
							dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall);
							dataRow["SumPrice"] = num;
							Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num, Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
						}
						else
						{
							decimal num5 = FUNC_DecimalNum(ItemDetails.SellPriceSmall2 + item_Items.PriceExportunit1);
							dataRow["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall2);
							dataRow["SumPrice"] = num5;
							Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, ItemDetails.Tax_Tobacco, num5, Convert.ToDecimal(dataRow["Quantity"]), dataRow, "ItemCode");
						}
						dataRow["OrginalPrice"] = dataRow["SumPrice"];
					}
					datatable.Rows.Add(dataRow);
					IsShow = 1;
				}
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
			gridControl1.DataSource = datatable;
		}
		catch (Exception)
		{
			Clear();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			LoginDetails.ISCheckSearchItem = true;
			LoginDetails.ISPurches = false;
			TxtSuppliers.Focus();
		}
		if (LoginDetails.DisPerCashier > 0m)
		{
			txtdicoutprege.Text = LoginDetails.DisPerCashier.ToString();
			txtdicoutprege_KeyUp(txtdicoutprege, new KeyEventArgs(Keys.None));
		}
	}

	private void gridControl1_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode != Keys.Return)
			{
				return;
			}
			string text = TxtBarcode.Text;
			if (text.StartsWith("+"))
			{
				string value = new string(text.Where(char.IsDigit).ToArray());
				GridPlus(Convert.ToInt32(value), '+');
			}
			else if (text.StartsWith("-"))
			{
				string value2 = new string(text.Where(char.IsDigit).ToArray());
				GridPlus(Convert.ToInt32(value2), '-');
			}
			else if (text.StartsWith("*"))
			{
				string value3 = new string(text.Where(char.IsDigit).ToArray());
				GridPlus(Convert.ToInt32(value3), '*');
			}
			else if (text.StartsWith("="))
			{
				string value4 = new string(text.Where(char.IsDigit).ToArray());
				GridPlus(Convert.ToInt32(value4), '=');
			}
			else if (text != string.Empty && InsertNewItemMizan(text) == 0)
			{
				ItemMatchResult itemsOnly = ORD.GetItemsOnly(text);
				if (itemsOnly != null)
				{
					string itemCode = TxtBarcode.Text;
					if (itemsOnly.MatchedField == "Item_code" || itemsOnly.MatchedField == "SmallBarCode1" || itemsOnly.MatchedField == "SmallBarCode2" || itemsOnly.MatchedField == "SmallBarCode3")
					{
						InsertNewItemUnitSmall(itemCode);
					}
					else if (itemsOnly.MatchedField == "MediumBarCode1" || itemsOnly.MatchedField == "MediumBarCode2" || itemsOnly.MatchedField == "MediumBarCode3")
					{
						InsertNewItemMedium(itemCode);
					}
					else if (itemsOnly.MatchedField == "BigBarCode1" || itemsOnly.MatchedField == "BigBarCode1" || itemsOnly.MatchedField == "BigBarCode1")
					{
						InsertNewItemBig(itemCode);
					}
				}
				else
				{
					SoundPlayer soundPlayer = new SoundPlayer("SOUND38.WAV");
					soundPlayer.Play();
					MessageBox.Show("عفوا، لايوجد صنف بهذا الرقم \r\n الرجاء المراجعة والمحاولة مرة اخري.", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			TxtBarcode.Text = string.Empty;
			TxtBarcode.Focus();
		}
		catch (Exception)
		{
			TxtBarcode.Text = null;
		}
	}

	private void IsRestaurantActive()
	{
		if (LoginDetails.IsRestaurant)
		{
			toggleCashierType.Visible = true;
			TxtRoom.Visible = true;
			TxtTable.Visible = true;
			lblRoom.Visible = true;
			lblTable.Visible = true;
		}
		else
		{
			toggleCashierType.Visible = false;
			TxtRoom.Visible = false;
			TxtTable.Visible = false;
			lblRoom.Visible = false;
			lblTable.Visible = false;
		}
	}

	public int PermissionSellPrice()
	{
		int result = 1;
		foreach (DataRow row in datatable.Rows)
		{
			string text = row["ItemCode"].ToString();
			if (text == string.Empty)
			{
				break;
			}
			if (row["UnitType"].ToString() == "الصغرى")
			{
				decimal sellPermission = ORD.GetSellPermission(text, UserSellPrice, "الصغرى");
				if (UserSellPrice != 5 && Convert.ToDecimal(row["UnitPriceDisc"]) < sellPermission)
				{
					MessageBox.Show("عفوا\u064b غير مصرح لك بخصم اكثر من السعر المحدد لك صلاحيته", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					result = 0;
					TxtDiscount.Focus();
					break;
				}
			}
			else if (row["UnitType"].ToString() == "المتوسطة")
			{
				decimal sellPermission2 = ORD.GetSellPermission(text, UserSellPrice, "المتوسطة");
				if (UserSellPrice != 5 && Convert.ToDecimal(row["UnitPriceDisc"]) < sellPermission2)
				{
					MessageBox.Show("عفوا\u064b غير مصرح لك بخصم اكثر من السعر المحدد لك صلاحيته", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					result = 0;
					TxtDiscount.Focus();
					break;
				}
			}
			else if (row["UnitType"].ToString() == "الكبرى")
			{
				decimal sellPermission3 = ORD.GetSellPermission(text, UserSellPrice, "الكبرى");
				if (UserSellPrice != 5 && Convert.ToDecimal(row["UnitPriceDisc"]) < sellPermission3)
				{
					MessageBox.Show("عفوا\u064b غير مصرح لك بخصم اكثر من السعر المحدد لك صلاحيته", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					result = 0;
					TxtDiscount.Focus();
					break;
				}
			}
		}
		return result;
	}

	public bool FilterDataTableSave(DataTable ItemOrder, int Purchese_ID, bool IsWait)
	{
		int num = 1;
		dtCurrentTable.Clear();
		foreach (DataRow row in ItemOrder.Rows)
		{
			if (row["ItemCode"].ToString() != string.Empty)
			{
				decimal num2 = SafeConvertToDecimal(row["Net"]);
				if (num2 == 0m)
				{
					MessageBox.Show("لا يمكن حفظ الفاتورة، يوجد أصناف بصافي قيمة = 0", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					return false;
				}
				dtCurrentTable.Rows.Add(num, Purchese_ID, Convert.ToInt32(row["ItemID"]), LoginDetails.BranchID, IsWait, Convert.ToInt32(row["StoreID"]), Convert.ToString(row["ItemUnitID"]), SafeConvertToDecimal(row["Quantity"]), Math.Round(SafeConvertToDecimal(row["AvergItem"]), DecimalNum), SafeConvertToDecimal(row["UnitPrice"]), Math.Round(SafeConvertToDecimal(row["UnitPrice"]), DecimalNum), Math.Round(SafeConvertToDecimal(row["Total"]), DecimalNum), Math.Round(SafeConvertToDecimal(row["ItemVat"]), DecimalNum), Math.Round(SafeConvertToDecimal(row["UnitPriceDisc"]), DecimalNum), Math.Round(SafeConvertToDecimal(row["Net"]), DecimalNum), Math.Round(SafeConvertToDecimal(row["ItemVatDisc"]), DecimalNum), row["UnitType"].ToString(), 1, null, 1, 1, 1, 1, 1, 1, 1, SafeConvertToDecimal(row["BounceItem"]), SafeConvertToDecimal(row["AmountDisc"]), SafeConvertToDecimal(row["PercentDisc"]), 1, 0, SafeConvertToDecimal(row["TaxTobacco"]), SafeConvertToDecimal(row["TaxTobaccoDis"]), SafeConvertToDecimal(row["TotalWithTaxTobacco"]), SafeConvertToDecimal(row["InstallationPrice"]), SafeConvertToDecimal(row["InstallPriceDis"]));
				num++;
			}
		}
		return true;
	}

	private void FilterDataTablePrint(DataTable ItemOrder, int Purchese_ID)
	{
		ST.Tables["TableGetLedger_Employee"].Clear();
		int num = 1;
		string text = (toggleCashierType.IsOn ? "طلب محلي" : "طلب سفري");
		foreach (DataRow row in ItemOrder.Rows)
		{
			if (row["ItemCode"].ToString() != string.Empty)
			{
				ST.TableGetLedger_Employee.Rows.Add(num, num, Purchese_ID, TxtDate.DateTime, TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, TxtCity.Text, text, row["ItemCode"], row["ItemName"], "Item", row["ItemNote"], row["UnitName"], TxtNumNote.Text, row["Quantity"], row["UnitPrice"], row["SumPrice"], row["Total"], row["SumPriceDisc"], row["ItemVat"], TxtTotals.Text, TxtTotalDiscount.Text, TxtVat.Text, TxtVatDisc.Text, TxtDiscount.Text, TxtNet.Text, TxtNet.Text, TxtNote.Text, TxtType.Text, TxtTable.Value, TxtRoom.Value, LoginDetails.UserName, LoginDetails.UserName, Cash, Bank, Cash + Bank, 0, 0, 0, LblSup.Text);
				num++;
			}
		}
	}

	private void SearchCustomer(int CustomerId)
	{
		if (CustomerId <= 0)
		{
			return;
		}
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		try
		{
			int FrmID = CustomerId;
			Account_CustSup account_CustSup = gTSdbEntities.Account_CustSup.FirstOrDefault((Account_CustSup x) => x.ID == FrmID && x.IsCustomers == (bool?)true);
			if (account_CustSup != null)
			{
				LblSup.Text = FrmID.ToString();
				LblSup.Text = account_CustSup.ID.ToString();
				TxtNumVat.Text = account_CustSup.VatNum.ToString();
				_TxtVatNum.Text = account_CustSup.VatNum.ToString();
				TxtVatNumCustomer = account_CustSup.VatNum.ToString();
				TxtSuppliers.Text = account_CustSup.CustSuppName;
				TxtSuppliers2.Text = account_CustSup.CustSuppName;
				TxtPhone.Text = account_CustSup.Phone;
				_TxtPhone.Text = account_CustSup.Phone;
				int cusID = Convert.ToInt32(account_CustSup.SalesManID);
				TxtSales.Text = Cust.GetSalesMan(cusID);
				TxtSales2.Text = Cust.GetSalesMan(cusID);
				TxtAdditionalNum.Text = account_CustSup.AdditionalNum;
				TxtPostalCode.Text = account_CustSup.PostalCode;
				TxtCommercialRecord.Text = account_CustSup.CommercialRecord;
				TxtCity.Text = account_CustSup.City;
				TxtCountry.Text = account_CustSup.Country;
				TxtStreet.Text = account_CustSup.Street;
				TxtDistrict.Text = account_CustSup.District;
				CreditLimit = account_CustSup.CreditLimit;
				Charge(FrmID);
			}
			else
			{
				Clear();
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	public void UpdateUnitPriceInGrid(CellValueChangedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["SN"].ToString() == Convert.ToString(num))
			{
				row["UnitPrice"] = Convert.ToDecimal(e.Value);
				UpdateItemRow("UnitPrice", e);
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
				break;
			}
		}
	}

	private void ZeroDisc()
	{
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		Cal_FinalResultTotals(0m, "DiscAmount");
	}

	private async void DiscountCode()
	{
		try
		{
			string disCode = BtnDisCode.Text.Trim();
			string companyName = LoginDetails.CompanyNameAR ?? "";
			string companyVatNum = LoginDetails.VatNum ?? "";
			if (string.IsNullOrWhiteSpace(disCode))
			{
				MessageBox.Show("يرجى إدخال كود الخصم", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			(decimal DiscountValue, decimal DiscountPercentage)? discountDetails = await DisCode.GetDisAsync(disCode, companyName, companyVatNum);
			if (discountDetails.HasValue)
			{
				decimal DisAmount = discountDetails.Value.DiscountValue;
				decimal Disper = discountDetails.Value.DiscountPercentage;
				TxtDiscount.Text = DisAmount.ToString();
				txtdicoutprege.Text = Disper.ToString();
				if (Convert.ToDecimal(DisAmount) > 0m)
				{
					Cal_FinalResultTotals(DisAmount, "DiscAmount");
				}
				else if (Convert.ToDecimal(Disper) > 0m)
				{
					Cal_FinalResultTotals(Disper, "Percent");
				}
				else
				{
					ZeroDisc();
				}
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show("حدث خطأ: " + ex2.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		TxtPhone.Properties.MaxLength = 14;
		TxtNumVat.Properties.MaxLength = 15;
		try
		{
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
		IEnumerable<Item_Groups> enumerable = from x in db.Item_Groups.ToList()
			where x.IsShow == true
			select x;
		tileControl1.ItemSize = LoginDetails.GroupSize;
		tileControl2.ItemSize = LoginDetails.ItemSize;
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		string groupColor = LoginDetails.GroupColor;
		string groupFontColor = LoginDetails.GroupFontColor;
		foreach (Item_Groups item in enumerable)
		{
			TileItem tileItem = new TileItem();
			tileGroup2.Items.Add(tileItem);
			tileItem.Id = item.ID;
			tileItem.ItemSize = TileItemSize.Wide;
			tileItem.AllowGlyphSkinning = DefaultBoolean.True;
			tileItem.Appearance.Font = new Font("Tahoma", 12.35f);
			tileItem.Appearance.BackColor = ColorTranslator.FromHtml(groupColor);
			tileItem.Appearance.ForeColor = ColorTranslator.FromHtml(groupFontColor);
			tileItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
			tileItem.ItemClick += tileBarItem1_ItemClick;
			TileItemElement tileItemElement = new TileItemElement();
			tileItemElement.Text = item.Name;
			tileItemElement.TextAlignment = TileItemContentAlignment.BottomCenter;
			tileItem.Elements.Add(tileItemElement);
			if (LoginDetails.IsImage && !string.IsNullOrEmpty(item.Image))
			{
				Image image = Base64ToImageGrop(item.Image);
				if (LoginDetails.ImgShape)
				{
					image = MakeSquareImage(image);
				}
				tileItem.BackgroundImage = image;
				tileItem.BackgroundImageAlignment = TileItemContentAlignment.MiddleCenter;
				tileItem.BackgroundImageScaleMode = TileItemImageScaleMode.Stretch;
			}
		}
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtBarcode.Focus();
		toggleSectoral.IsOn = true;
		if (LoginDetails.IsSecureCashier && !db.User_Login.Select((User_Login x) => x.IsOpenDay == (bool?)true).FirstOrDefault())
		{
			base.Enabled = false;
			GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay frmOpenDay = new GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay();
			frmOpenDay.ShowDialog();
			Clear();
			if (!frmOpenDay.ISOpen)
			{
				return;
			}
			base.Enabled = true;
		}
		DateHold.DateTime = DateTime.Now.AddDays(1.0);
		BtnMinNet.Visible = LoginDetails.Gabr;
		BtnPlusNet.Visible = LoginDetails.Gabr;
		User_Login user_Login = db.User_Login.Where((User_Login x) => x.ID == LoginDetails.UserID).FirstOrDefault();
		if (user_Login != null)
		{
			OpenClose = true;
			HoldOpenClose.IsOn = user_Login.IsOpenDay.Value;
			OpenClose = false;
		}
	}

	private void Frms_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F2")
		{
			TxtBarcode.Text = string.Empty;
			TxtBarcode.Focus();
		}
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
			if (LoginDetails.IsSecureCashier)
			{
				FrmPassWordSupervisor frmPassWordSupervisor = new FrmPassWordSupervisor();
				frmPassWordSupervisor.ShowDialog();
				if (frmPassWordSupervisor.ISClose)
				{
					datatable.Rows.Clear();
					Clear();
				}
				frmPassWordSupervisor.Close();
				TxtBarcode.Focus();
			}
			else
			{
				datatable.Rows.Clear();
				Clear();
			}
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
		if (e.KeyCode.ToString() == "F9")
		{
			GetPricePffer();
			return;
		}
		if (e.KeyCode.ToString() == "F8")
		{
			GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay frmOpenDay = new GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay();
			frmOpenDay.ShowDialog();
			Clear();
			return;
		}
		if (e.KeyCode.ToString() == "F10")
		{
			FrmCloseDay frmCloseDay = new FrmCloseDay();
			frmCloseDay.ShowDialog();
			if (!frmCloseDay.IsClose)
			{
				base.Enabled = false;
				Close();
				Clear();
			}
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
		if (e.KeyCode.ToString() == "F12")
		{
			openItmCard();
		}
		else if (e.KeyCode == Keys.P && e.Modifiers == Keys.Control)
		{
			if (BtnDelete.Enabled)
			{
				Print(Code);
			}
		}
		else if (e.KeyCode == Keys.L && e.Modifiers == Keys.Control)
		{
			new FrmLock().ShowDialog();
		}
		else if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
		{
			GridDelete();
		}
		else if (e.KeyCode == Keys.Add && e.Modifiers == Keys.Control)
		{
			GridPlus(1m, '+');
		}
		else if (e.KeyCode == Keys.Subtract && e.Modifiers == Keys.Control)
		{
			GridPlus(1m, '-');
		}
		else if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
		{
			new FrmItemReader().Show();
		}
		else if (e.KeyCode == Keys.R && e.Modifiers == Keys.Control)
		{
			new FrmReservation().Show();
		}
	}

	private void SetKeyboardLayout(InputLanguage layout)
	{
		InputLanguage.CurrentInputLanguage = layout;
	}

	private void textBox1_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("ar"));
	}

	public static InputLanguage GetInputLanguageByName(string inputName)
	{
		foreach (InputLanguage installedInputLanguage in InputLanguage.InstalledInputLanguages)
		{
			if (installedInputLanguage.Culture.EnglishName.ToLower().StartsWith(inputName))
			{
				return installedInputLanguage;
			}
		}
		return null;
	}

	private void TxtSuppliers_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("ar"));
	}

	private void TxtNote_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("ar"));
	}

	private void TxtNumVat_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("ar"));
	}

	private void TxtSuppliers_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void TxtType_SelectedIndexChanged_1(object sender, EventArgs e)
	{
		if (TxtType.Text == "نقدى")
		{
			TxtSuppliers.ForeColor = Color.Black;
			TxtType.ForeColor = Color.Black;
		}
		if (TxtType.Text == "آجلة")
		{
			TxtSuppliers.Text = string.Empty;
			TxtSales.Text = string.Empty;
			TxtNumVat.Text = string.Empty;
			TxtPhone.Text = string.Empty;
			TxtCity.Text = string.Empty;
			TxtCountry.Text = string.Empty;
			TxtPostalCode.Text = string.Empty;
			TxtCommercialRecord.Text = string.Empty;
			TxtCharge.Text = "0.00";
			TxtSuppliers.ForeColor = Color.Red;
			TxtType.ForeColor = Color.Red;
		}
	}

	private void ControlSave_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnSave.Focus();
		}
	}

	private void TxtType_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void TxtDiscount_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (TxtDiscount.Text != string.Empty && Convert.ToDecimal(TxtDiscount.Text) != 0m && Convert.ToDecimal(TxtDiscount.Text) <= Convert.ToDecimal(TxtTotals.Text))
			{
				decimal per = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
				Cal_FinalResultTotals(per, "DiscAmount");
				txtdicoutprege.Text = per.ToString();
			}
			else
			{
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtAllDiscount.Text = "0.00";
				Cal_FinalResultTotals(0m, "DiscAmount");
			}
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
			txtdicoutprege.Text = "0.00";
			TxtAllDiscount.Text = "0.00";
			Cal_FinalResultTotals(0m, "DiscAmount");
		}
	}

	private void txtdicoutprege_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (decimal.TryParse(txtdicoutprege.Text, out var result))
			{
				decimal num = result;
			}
			if (txtdicoutprege.Text != string.Empty && Convert.ToDecimal(txtdicoutprege.Text) != 0m && Convert.ToDecimal(txtdicoutprege.Text) <= 100m)
			{
				Cal_FinalResultTotals(result, "Percent");
				return;
			}
			TxtDiscount.Text = "0.00";
			txtdicoutprege.Text = "0.00";
			TxtAllDiscount.Text = "0.00";
			Cal_FinalResultTotals(0m, "DiscAmount");
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
			txtdicoutprege.Text = "0.00";
			TxtAllDiscount.Text = "0.00";
			Cal_FinalResultTotals(0m, "DiscAmount");
		}
	}

	private void TxtAllDiscount_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (TxtAllDiscount.Text != string.Empty && Convert.ToDecimal(TxtAllDiscount.Text) != 0m && Convert.ToDecimal(TxtAllDiscount.Text) < Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				decimal num = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), 1);
				decimal per = Convert.ToDecimal(TxtAllDiscount.Text);
				Cal_FinalResultTotals(per, "AllDiscount");
				TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
				TxtNet.Text = (num - Convert.ToDecimal(TxtAllDiscount.Text) + Convert.ToDecimal(TxtTabocc.Text)).ToString();
			}
			else
			{
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtAllDiscount.Text = "0.00";
				Cal_FinalResultTotals(0m, "DiscAmount");
			}
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
			txtdicoutprege.Text = "0.00";
			TxtAllDiscount.Text = "0.00";
			Cal_FinalResultTotals(0m, "DiscAmount");
		}
	}

	private void BtnDisCode_KeyUp_1(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			DiscountCode();
		}
	}

	private void BtnTypeInvoic_Click(object sender, EventArgs e)
	{
		new FrmInvoicType("Order").Show();
	}

	private void TxtNumVat_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void TxtPhone_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void TxtCommercialRecord_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void TxtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void MenuNew_Click(object sender, EventArgs e)
	{
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
		Print(Code);
	}

	private void حركةصنفToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 60))
		{
			FrmRPMovementItem frmRPMovementItem = new FrmRPMovementItem();
			frmRPMovementItem.lblItemID.Text = ItmID.ToString();
			frmRPMovementItem.TxtItemCode.Text = ItmName.ToString();
			frmRPMovementItem.ShowDialog();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void MenuPrintFatoura_Click(object sender, EventArgs e)
	{
		Print(Code);
	}

	private void MenuPrintQued_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text != string.Empty)
		{
			Class_Print_DailyRestrictionsALL class_Print_DailyRestrictionsALL = new Class_Print_DailyRestrictionsALL();
			class_Print_DailyRestrictionsALL.Print_DailyRestrictionsALL(Convert.ToInt32(TxtCode.Text), 5);
		}
	}

	private void LblSup_TextChanged(object sender, EventArgs e)
	{
		LBlSuppAccount.Text = SPR.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
	}

	private void BtnReturnCashier_Click(object sender, EventArgs e)
	{
		FrmReturnOrderInCashier frmReturnOrderInCashier = new FrmReturnOrderInCashier();
		frmReturnOrderInCashier.TxtBarcode.Text = TxtCode.Text;
		frmReturnOrderInCashier.ShowDialog();
	}

	private void MenuPreviewFatoura_Click_1(object sender, EventArgs e)
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			BeginInvoke((Action)delegate
			{
				PreviewPrint(Convert.ToInt32(TxtCode.Text));
			});
		});
		thread.Start();
	}

	private void MenuPrintFatoura_Click_1(object sender, EventArgs e)
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			BeginInvoke((Action)delegate
			{
				Print(Code);
			});
		});
		thread.Start();
	}

	private void FrmCashir_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason != CloseReason.UserClosing)
		{
			return;
		}
		DialogResult dialogResult = MessageBox.Show("هل انت متأكد من اغلاق شاشة الكاشير", "اغلاق الكاشير", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		if (dialogResult == DialogResult.Yes)
		{
			if (datatable.Rows.Count <= 0)
			{
				return;
			}
			if (LoginDetails.IsSecureCashier)
			{
				FrmPassWordSupervisor frmPassWordSupervisor = new FrmPassWordSupervisor();
				frmPassWordSupervisor.ShowDialog();
				if (!frmPassWordSupervisor.ISClose)
				{
					e.Cancel = true;
					return;
				}
			}
			if (LoginDetails.Lang == "ar-EG")
			{
				gridControl1.MainView.SaveLayoutToXml(fileNameAR);
			}
			else
			{
				gridControl1.MainView.SaveLayoutToXml(fileNameEN);
			}
		}
		else
		{
			e.Cancel = true;
		}
	}

	private void gridControl1_Click(object sender, EventArgs e)
	{
	}

	private void BtnNewCustomer_Click(object sender, EventArgs e)
	{
		new FrmCustomer().Show();
	}

	private void lblRoom_Click(object sender, EventArgs e)
	{
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void TxtBarcode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchItems();
	}

	private void toggleHold_Toggled(object sender, EventArgs e)
	{
		if (toggleHold.IsOn)
		{
			LblDateHold.Visible = true;
			DateHold.Visible = true;
		}
		else
		{
			LblDateHold.Visible = false;
			DateHold.Visible = false;
		}
	}

	private void CBBounce_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void MenuNewItem_Click(object sender, EventArgs e)
	{
		openItmCard();
	}

	private void MenuSearchItem_Click(object sender, EventArgs e)
	{
		SearchItems();
	}

	private void toggleCashierType_Toggled(object sender, EventArgs e)
	{
		if (!toggleCashierType.IsOn)
		{
			TxtRoom.Value = 0m;
			TxtTable.Value = 0m;
			TxtRoom.Enabled = false;
			TxtTable.Enabled = false;
		}
		else
		{
			TxtRoom.Enabled = true;
			TxtTable.Enabled = true;
		}
	}

	private void BtnCustomerSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(BtnCustomerSearch.Text))
		{
			SearchCustomer(Convert.ToInt32(LblSup.Text));
		}
	}

	private void BtnOrderSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (!int.TryParse(BtnOrderSearch.Text.Trim(), out var _))
		{
			SoundPlayer soundPlayer = new SoundPlayer("SOUND38.WAV");
			soundPlayer.Play();
			MessageBox.Show("تنبيه،من فضلك أدخل رقم طلب صحيح \r\n الرجاء المراجعة والمحاولة مرة اخري.", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			BtnOrderSearch.Focus();
			BtnOrderSearch.SelectAll();
		}
		else if (!string.IsNullOrWhiteSpace(BtnOrderSearch.Text))
		{
			SearchOrderNum(Convert.ToInt32(BtnOrderSearch.Text));
			GetElctornc();
		}
	}

	private void MenuPurchOrder_Click(object sender, EventArgs e)
	{
		GetPricePffer();
	}

	private void BtnCustomerSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && !string.IsNullOrWhiteSpace(BtnCustomerSearch.Text))
		{
			SearchCustomer(Convert.ToInt32(BtnCustomerSearch.Text));
		}
	}

	private void BtnCustomerSearch_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void BtnOrderSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (!int.TryParse(BtnOrderSearch.Text.Trim(), out var _))
			{
				SoundPlayer soundPlayer = new SoundPlayer("SOUND38.WAV");
				soundPlayer.Play();
				MessageBox.Show("تنبيه،من فضلك أدخل رقم طلب صحيح \r\n الرجاء المراجعة والمحاولة مرة اخري.", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				BtnOrderSearch.Focus();
				BtnOrderSearch.SelectAll();
			}
			else
			{
				SearchOrderNum(Convert.ToInt32(BtnOrderSearch.Text));
				GetElctornc();
			}
		}
	}

	private void toggleHold_Properties_Click(object sender, EventArgs e)
	{
		if (!toggleHold.IsOn)
		{
			LblDateHold.Visible = false;
			DateHold.Visible = false;
		}
		else
		{
			LblDateHold.Visible = true;
			DateHold.Visible = true;
		}
	}

	private void MenuExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnRecipt_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 31))
		{
			new FrmReceipts().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private bool openclose(bool Ck)
	{
		User_Login user_Login = db.User_Login.Where((User_Login x) => x.ID == LoginDetails.UserID).FirstOrDefault();
		if (user_Login != null)
		{
			if (!Ck)
			{
				FrmCloseDay frmCloseDay = new FrmCloseDay();
				frmCloseDay.ShowDialog();
				Clear();
				if (frmCloseDay.IsClose)
				{
					return true;
				}
				return false;
			}
			GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay frmOpenDay = new GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay();
			frmOpenDay.ShowDialog();
			Clear();
			if (frmOpenDay.ISOpen)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	private void صرفالحجزToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 59))
		{
			new FrmCheckOut().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCheckOut_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 59))
		{
			new FrmCheckOut().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private string CountZero()
	{
		string text = "0.";
		for (int i = 0; i < DecimalNum - 1; i++)
		{
			text += "0";
		}
		return text + "1";
	}

	private void BtnPlusNet_Click(object sender, EventArgs e)
	{
		if (Convert.ToDouble(TxtNet.Text) > 0.0)
		{
			TxtNet.Text = (Convert.ToDouble(TxtNet.Text) + Convert.ToDouble(CountZero())).ToString();
		}
	}

	private void TxtSuppliers2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
		frmSearchCustomer.ShowDialog();
		if (frmSearchCustomer.CustCode > 0)
		{
			SearchCustomer(frmSearchCustomer.CustID);
		}
	}

	private void TxtSuppliers2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void TxtSuppliers2_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("ar"));
	}

	private void BtnCustomerSearch2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(BtnCustomerSearch2.Text))
		{
			SearchCustomer(Convert.ToInt32(LblSup.Text));
		}
	}

	private void BtnCustomerSearch2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && !string.IsNullOrWhiteSpace(BtnCustomerSearch2.Text))
		{
			SearchCustomer(Convert.ToInt32(BtnCustomerSearch2.Text));
		}
	}

	private void BtnNewCustomer2_Click(object sender, EventArgs e)
	{
		new FrmCustomer().Show();
	}

	private void DateHold_EditValueChanged(object sender, EventArgs e)
	{
	}

	private void BtnMinNet_Click(object sender, EventArgs e)
	{
		if (Convert.ToDouble(TxtNet.Text) > 0.0)
		{
			TxtNet.Text = (Convert.ToDouble(TxtNet.Text) - Convert.ToDouble(CountZero())).ToString();
		}
	}

	private void HoldOpenClose_Click(object sender, EventArgs e)
	{
		if (HoldOpenClose.IsOn)
		{
			HoldOpenClose.IsOn = false;
		}
		else
		{
			HoldOpenClose.IsOn = true;
		}
		if (!OpenClose)
		{
			HoldOpenClose.IsOn = openclose(HoldOpenClose.IsOn);
		}
	}

	private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.Tables["TableLadger"].Clear();
		SqlCommand sqlCommand = new SqlCommand("Ledger_Account", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlCommand.Parameters.AddWithValue("@Date1", DateTime.Now.AddMonths(-1));
		sqlCommand.Parameters.AddWithValue("@Date2", DateTime.Now);
		sqlCommand.Parameters.AddWithValue("@AccountID", Convert.ToInt32(LBlSuppAccount.Text));
		sqlCommand.Parameters.AddWithValue("@TypeID", 1);
		sqlDataAdapter.Fill(dataSetALL.TableLadger);
		sqlConnection.Close();
		if (dataSetALL.Tables["TableLadger"].Rows.Count > 0)
		{
			StiReport stiReport = new StiReport();
			if (LoginDetails.Lang == "ar-EG")
			{
				stiReport.Load(Application.StartupPath + "\\Reports\\ReportLedger.mrt");
			}
			else
			{
				stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportLedger.mrt");
			}
			stiReport.RegData(dataSetALL.TableLadger);
			stiReport.Dictionary.Synchronize();
			stiReport.Compile();
			stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
			stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
			stiReport["VatNumComp"] = LoginDetails.VatNum;
			stiReport["Date1"] = DateTime.Now.AddMonths(-1);
			stiReport["Date2"] = DateTime.Now;
			stiReport.Render();
			stiReport.Show(dialogForm: true);
			FrmReportGetOrder frmReportGetOrder = new FrmReportGetOrder();
			frmReportGetOrder.stiViewerControl1.Report = stiReport;
		}
		else
		{
			MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnTailor_Click(object sender, EventArgs e)
	{
		FrmTailor frmTailor = new FrmTailor();
		frmTailor.Show();
	}

	private decimal FUNC_DecimalNum(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNum);
	}

	private decimal FUNC_DecimalNumQty(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNumQty);
	}

	private void Charge(int ID)
	{
		try
		{
			GTSdbEntities gTSdbEntities = new GTSdbEntities();
			List<decimal?> list = gTSdbEntities.GetAccountBalance(ID).ToList();
			foreach (decimal? item in list)
			{
				TxtCharge.Text = FUNC_DecimalNum(item.Value).ToString();
				TxtCharge2.Text = FUNC_DecimalNum(item.Value).ToString();
			}
			TxtBarcode.Focus();
		}
		catch (Exception)
		{
		}
	}

	public static Image MakeSquareImage(Image img)
	{
		int num = Math.Min(img.Width, img.Height);
		if (num < 50)
		{
			num = 50;
		}
		Bitmap bitmap = new Bitmap(num, num);
		using (Graphics graphics = Graphics.FromImage(bitmap))
		{
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			int num2 = (img.Width - num) / 2;
			int num3 = (img.Height - num) / 2;
			graphics.DrawImage(img, new Rectangle(0, 0, num, num), new Rectangle(num2, num3, num, num), GraphicsUnit.Pixel);
		}
		return bitmap;
	}

	private string ExportInvoiceAndUploadToServer()
	{
		try
		{
			if (string.IsNullOrWhiteSpace(TxtCode.Text))
			{
				MessageBox.Show("اختر رقم الفاتورة أولا\u064b", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return null;
			}
			SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Print_Order_Orders", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
			sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
			sqlConnection.Close();
			if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count == 0)
			{
				MessageBox.Show("لا يوجد بيانات", "طباعة", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return null;
			}
			StiReport stiReport = new StiReport();
			if (LoginDetails.Lang == "ar-EG")
			{
				stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashir.mrt");
			}
			else
			{
				stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCashir.mrt");
			}
			stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
			stiReport.Dictionary.Synchronize();
			stiReport.Compile();
			if (TxtVatNumCustomer != string.Empty)
			{
				stiReport["Title"] = "فاتورة ضريبية";
			}
			else
			{
				stiReport["Title"] = "فاتورة ضريبية مبسطة";
			}
			stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
			stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
			stiReport["VatNumComp"] = LoginDetails.VatNum;
			stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
			stiReport["Address"] = LoginDetails.Address;
			stiReport["Phone"] = LoginDetails.Phone;
			stiReport["MobileComp"] = LoginDetails.Mobile;
			stiReport["Bank"] = LoginDetails.CompanyBank;
			stiReport["CashMoney"] = Cash.ToString();
			stiReport["CashBank"] = Bank.ToString();
			stiReport["MoneyNeed"] = MoneyNeed.ToString();
			stiReport["MoneyPaying"] = MoneyPaying.ToString();
			stiReport["MoneyNet"] = MoneyNet.ToString();
			stiReport["MobileComp"] = LoginDetails.Mobile;
			stiReport["AddressComp"] = LoginDetails.Address;
			stiReport["PrintComp"] = LoginDetails.PrintComp;
			stiReport["CustName"] = TxtSuppliers.Text;
			stiReport["CustVatNum"] = TxtNumVat.Text;
			stiReport["Custphone"] = TxtPhone.Text;
			stiReport["Tafqit"] = txtWordAr;
			stiReport["EmployeeName"] = LoginDetails.UserName;
			Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
			string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), Math.Round(LoginDetails.NetCash + LoginDetails.VatCash, 2).ToString(), LoginDetails.VatCash.ToString());
			stiReport["EncHash"] = value;
			string text = $"Invoice_{TxtCode.Text}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
			string text2 = Path.Combine(Path.GetTempPath(), text);
			stiReport.Render(showProgress: false);
			stiReport.ExportDocument(StiExportFormat.Pdf, text2);
			string requestUriString = "ftp://win1135.site4now.net/" + text;
			string userName = "moelsayed";
			string password = "78789878";
			FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUriString);
			ftpWebRequest.Method = "STOR";
			ftpWebRequest.Credentials = new NetworkCredential(userName, password);
			byte[] array = File.ReadAllBytes(text2);
			ftpWebRequest.ContentLength = array.Length;
			using (Stream stream = ftpWebRequest.GetRequestStream())
			{
				stream.Write(array, 0, array.Length);
			}
			using ((FtpWebResponse)ftpWebRequest.GetResponse())
			{
			}
			File.Delete(text2);
			return "http://gtsdev-001-site28.atempurl.com/sentinvoices/" + text;
		}
		catch (Exception ex)
		{
			MessageBox.Show("⚠\ufe0f خطأ أثناء حفظ الفاتورة:\n" + ex.Message);
			return null;
		}
	}

	private CSIDInfoDto CSIDInfoDto()
	{
		return new CSIDInfoDto
		{
			CertPem = LoginDetails.PublicKey,
			CSR = LoginDetails.Crs,
			PrivateKey = LoginDetails.PrivateKey,
			Secretkey = LoginDetails.SecretKey
		};
	}

	private InvoiceDto invoiceDto(OrderOrdersDto model)
	{
		return new InvoiceDto
		{
			ID = "Inv-" + model.PurBranchID,
			OrderId = model.PurBranchID.ToString(),
			IssueDate = model.Purchases_Date.Value.ToString("yyyy-MM-dd"),
			IssueTime = model.Purchases_Date.Value.ToString("HH:mm:ss"),
			InvoiceType = model.OrderTypeElectronicInvoiceId.Value,
			InvoiceTypeName = GenereateInvoiceTypeName(model),
			DocumentCurrencyCode = "SAR",
			TaxCurrencyCode = "SAR",
			UUID = model.PurBranchID.ToString(),
			PaymentCode = GetPaymentMethod(model),
			PayableAmount = 0m,
			InstructionNote = model.FinCash.ToString()
		};
	}

	private string GenereateInvoiceTypeName(OrderOrdersDto order)
	{
		return order.NormalOrSimpleInvoice + order.ThirdParty + order.NominalInvoice + order.ExportInvoice + order.SummaryInvoice + order.SelfInvoice;
	}

	private void BtnUpload_Click(object sender, EventArgs e)
	{
		if (LSucced.Text == "Success")
		{
			MessageBox.Show("تم ارسال الفاتوره بالفعل", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			return;
		}
		if (TxtCode.Text == "" || TxtCode.Text == string.Empty)
		{
			MessageBox.Show("يرجعي تحديد فاتوره ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			return;
		}
		int orderId = Convert.ToInt32(TxtCode.Text);
		int valueOrDefault = Zakat.GetIdByOrderId(orderId).GetValueOrDefault();
		SentToSazakt(valueOrDefault);
	}

	private void Comp_Store_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (Comp_Store.SelectedValue != null && Comp_Store.SelectedValue is int)
		{
			int storeId = Convert.ToInt32(Comp_Store.SelectedValue);
			TxtQuntity.Text = ORD.GetCurrentBalance(ItmID, storeId).ToString();
		}
	}

	private void TxtNumVat_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.V)
		{
			e.SuppressKeyPress = true;
		}
	}

	private void TxtPostalCode_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.V)
		{
			e.SuppressKeyPress = true;
		}
	}

	private void TxtCommercialRecord_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.V)
		{
			e.SuppressKeyPress = true;
		}
	}

	private void TxtCommercialRecord_MouseDown(object sender, MouseEventArgs e)
	{
	}

	private void TxtPostalCode_MouseDown(object sender, MouseEventArgs e)
	{
	}

	private void TxtPhone_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.V)
		{
			e.SuppressKeyPress = true;
		}
	}

	private void TxtNumNote_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private List<string> GetPaymentMethod(OrderOrdersDto order)
	{
		List<string> list = new List<string>();
		decimal? cashMoney = order.CashMoney;
		if ((cashMoney.GetValueOrDefault() > 0m) & cashMoney.HasValue)
		{
			list.Add("10");
		}
		else
		{
			cashMoney = order.CashBank;
			if ((cashMoney.GetValueOrDefault() > 0m) & cashMoney.HasValue)
			{
				list.Add("30");
			}
			else
			{
				cashMoney = order.BankTransfare;
				if ((cashMoney.GetValueOrDefault() > 0m) & cashMoney.HasValue)
				{
					list.Add("42");
				}
				else
				{
					list.Add("");
				}
			}
		}
		return list;
	}

	private List<InvoiceItems> invoiceItems(DataTable orderDetails)
	{
		List<InvoiceItems> list = new List<InvoiceItems>();
		foreach (DataRow row in orderDetails.Rows)
		{
			decimal num = row.Field<decimal>("ItemVatDisc");
			decimal num2 = row.Field<decimal>("UnitPriceDisc");
			decimal num3 = row.Field<decimal>("Quantity");
			decimal num4 = row.Field<decimal>("ItemVatDisc") / (row.Field<decimal>("UnitPriceDisc") * row.Field<decimal>("Quantity")) * 100m;
			decimal value = Math.Round((row.Field<decimal>("Net") == 0m) ? 0m : (row.Field<decimal>("ItemVatDisc") / (row.Field<decimal>("UnitPriceDisc") * row.Field<decimal>("Quantity")) * 100m), 0);
			string text = row["VatCode"].ToString();
			if (LoginDetails.IsVat && text != "E")
			{
				value = 15m;
			}
			list.Add(new InvoiceItems
			{
				ProductName = row["ItemName"]?.ToString(),
				DiscountValue = row.Field<decimal>("AmountDisc"),
				ProductPrice = row.Field<decimal>("UnitPriceDisc"),
				ProductQuantity = row.Field<decimal>("Quantity"),
				TotalPrice = row.Field<decimal>("SumPrice"),
				TotalWithVat = row.Field<decimal>("ItemVatDisc") + row.Field<decimal>("SumPrice"),
				TotalPriceAfterDiscount = row.Field<decimal>("Net"),
				VatCode = row["VatCode"]?.ToString(),
				TaxExemptionReason = row["TaxExemptionReason"]?.ToString(),
				TaxExemptionReasonCode = row["TaxExemptionReasonCode"]?.ToString(),
				VatValue = row.Field<decimal>("ItemVatDisc"),
				VatPercentage = value
			});
		}
		return list;
	}

	private CustomerPartyDto CustomerPartyDto(OrderOrdersDto order)
	{
		return new CustomerPartyDto
		{
			StreetName = order.Street,
			BuildingNumber = order.BuildingNum,
			CityName = order.City,
			PostalZone = order.PostalCode,
			CitySubdivisionName = order.District,
			IdentificationCode = "SA",
			RegistrationName = order.SupplierName,
			CompanyID = order.SupplierVatNum,
			Name = order.SupplierName
		};
	}

	private SupplierPartyDto supplierPartyDto(OrderOrdersDto order)
	{
		return new SupplierPartyDto
		{
			ID = LoginDetails.SupplierPartyIdentificationID,
			schemeID = LoginDetails.SupplierPartyIdentificationschemeID,
			StreetName = LoginDetails.Street,
			BuildingNumber = LoginDetails.BuildingNum,
			PostalZone = LoginDetails.PostalCode,
			CitySubdivisionName = LoginDetails.District,
			RegistrationName = LoginDetails.CompanyNameAR,
			CompanyID = LoginDetails.VatNum,
			CityName = LoginDetails.City,
			IdentificationCode = "SA"
		};
	}

	private async Task<OrderOrdersDto> GenerateOrder(int ID)
	{
		bool IsWait = toggleHold.IsOn;
		return new OrderOrdersDto
		{
			ID = ID,
			PurBranchID = ID,
			SupplierID = Convert.ToInt32(LBlSuppAccount.Text),
			IsWaiting = IsWait,
			SupplierName = TxtSuppliers.Text,
			SupplierPhone = TxtPhone.Text,
			SupplierVatNum = TxtNumVat.Text,
			Purchases_Date = Convert.ToDateTime(TxtDate.DateTime),
			Order_Paymant_Type = Convert.ToInt32(TxtType.SelectedValue),
			Restaurant_PayType = 0,
			Restaurant_TypeID = 1,
			CostCentersID = 1,
			BounceID = Convert.ToInt32(CBBounce.SelectedValue),
			Note = TxtNote.Text,
			NoteNum = TxtNumNote.Text,
			CostOrder = Math.Round(Convert.ToDecimal(TxtCost.Text), 2),
			Tax = Math.Round(Convert.ToDecimal(TxtVat.Text), 2),
			TotalPrices = Math.Round(Convert.ToDecimal(TxtTotals.Text), 2),
			Safy = Math.Round(Convert.ToDecimal(TxtTotals.Text), 2),
			DiscountNum = Math.Round(Convert.ToDecimal(TxtAllDiscount.Text), 2),
			DiscountPerantage = Math.Round(Convert.ToDecimal(txtdicoutprege.Text), 2),
			Tax_Discount = Math.Round(Convert.ToDecimal(TxtVatDisc.Text), 2),
			TotalPrices_Discount = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), 2),
			TobaccoTax = 0m,
			AllTax = Math.Round(Convert.ToDecimal(TxtVatDisc.Text), 2),
			Net = Math.Round(Convert.ToDecimal(TxtNet.Text), 2),
			CashMoney = Cash,
			CashBank = Bank,
			RoomNum = 1,
			TableNum = 1,
			Sectoral = true,
			SalesMan = TxtSales.Text,
			DateHold = DateTime.Now,
			BuildingNum = string.Empty,
			Street = TxtStreet.Text,
			District = TxtDistrict.Text,
			City = TxtCity.Text,
			Country = "السعوديه",
			PostalCode = TxtPostalCode.Text,
			CommercialRecord = TxtCommercialRecord.Text,
			OrderTypeElectronicInvoiceId = ((LoginDetails.OrderTypeElectronicInvoiceId == 0) ? 388 : LoginDetails.OrderTypeElectronicInvoiceId),
			NormalOrSimpleInvoice = (string.IsNullOrEmpty(LoginDetails.NormalOrSimpleInvoice) ? "02" : LoginDetails.NormalOrSimpleInvoice),
			ThirdParty = (LoginDetails.ThirdParty ? "1" : "0"),
			NominalInvoice = (LoginDetails.NominalInvoice ? "1" : "0"),
			ExportInvoice = (LoginDetails.ExportInvoice ? "1" : "0"),
			SummaryInvoice = (LoginDetails.SummaryInvoice ? "1" : "0"),
			SelfInvoice = (LoginDetails.SelfInvoice ? "1" : "0")
		};
	}

	public async Task SentToSazakt(int id)
	{
		waitForm.Show();
		if (LoginDetails.ModeInvoice == "developer")
		{
			mode = Mode.developer;
		}
		else if (LoginDetails.ModeInvoice == "Simulation")
		{
			mode = Mode.Simulation;
		}
		else if (LoginDetails.ModeInvoice == "Production")
		{
			mode = Mode.Production;
		}
		ElectronicInvoice invoiceZakat = new ElectronicInvoice();
		ResultElectronicInvoiceXml res = db.ResultElectronicInvoiceXmls.FirstOrDefault((ResultElectronicInvoiceXml s) => s.Id == id);
		if ((res?.IsValid ?? false) && (await invoiceZakat.SentInvoiceToZakat(GenerateResult(res), CSIDInfoDto(), res.InvoiceTypeName, mode)).IsSuccess)
		{
			Zakat.UpdateZakatById(res.Id, res.QRCode);
			LSucced.Visible = true;
			LSucced.Text = "Success";
			LSucced.BackColor = Color.Green;
		}
		waitForm.Close();
	}

	private Result GenerateResult(ResultElectronicInvoiceXml resultElectronic)
	{
		return new Result
		{
			QRCode = resultElectronic.QRCode,
			AllowanceTotalAmount = resultElectronic.AllowanceTotalAmount,
			ChargeTotalAmount = resultElectronic.ChargeTotalAmount,
			EncodedInvoice = resultElectronic.EncodedInvoice,
			InvoiceHash = resultElectronic.InvoiceHash,
			IsValid = resultElectronic.IsValid,
			LineExtensionAmount = resultElectronic.LineExtensionAmount,
			SingedXMLFileNameFullPath = resultElectronic.SingedXMLFileNameFullPath,
			UUID = resultElectronic.UUID,
			TaxInclusiveAmount = resultElectronic.TaxInclusiveAmount,
			PayableAmount = resultElectronic.PayableAmount,
			TaxAmount = resultElectronic.TaxAmount,
			TaxExclusiveAmount = resultElectronic.TaxExclusiveAmount,
			ErrorMessage = resultElectronic.ErrorMessage,
			SingedXMLFileName = resultElectronic.SingedXMLFileName,
			SingedXML = resultElectronic.SingedXML,
			ResultedValue = resultElectronic.ResultedValue,
			PrepaidAmount = resultElectronic.PrepaidAmount,
			SingedXMLFileNameShortPath = resultElectronic.SingedXMLFileNameShortPath,
			PIH = resultElectronic.PIH,
			Operation = resultElectronic.Operation,
			NormalXMLFileNameShortPath = resultElectronic.NormalXMLFileNameShortPath,
			NormalXMLFileNameFullPath = resultElectronic.NormalXMLFileNameFullPath
		};
	}

	private void GetElctornc()
	{
		if (!LoginDetails.IsElectronicInvoice)
		{
			return;
		}
		int orderId = Convert.ToInt32(TxtCode.Text);
		int valueOrDefault = Zakat.GetIdByOrderId(orderId).GetValueOrDefault();
		if (valueOrDefault != 0)
		{
			ResultElectronicInvoiceXml rowById = Zakat.GetRowById(valueOrDefault);
			LoginDetails.EncHash = rowById.QRCode;
			bool isValid = rowById.IsValid;
			bool valueOrDefault2 = rowById.Uploaded == true;
			if (isValid)
			{
				LValid.Visible = true;
				LValid.Text = "Valid";
				LValid.BackColor = Color.Green;
			}
			if (valueOrDefault2)
			{
				LSucced.Visible = true;
				LSucced.Text = "Success";
				LSucced.BackColor = Color.Green;
			}
			else
			{
				LSucced.Visible = true;
				LSucced.BackColor = Color.Red;
				LSucced.Text = "NotSuccess";
				BtnUpload.Visible = true;
			}
		}
	}

	private void Setting()
	{
		_ = LoginDetails.PerGrid;
		double num = ((Convert.ToDouble(LoginDetails.PerGrid) / 100.0 <= 0.1) ? 0.475 : (Convert.ToDouble(LoginDetails.PerGrid) / 100.0));
		int num2 = 7;
		gridControl1.Width = (int)((double)base.ClientSize.Width * num);
		gridControl1.Left = base.ClientSize.Width - gridControl1.Width - num2;
		tileControl2.Width = gridControl1.Left;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmCashir));
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
		this.repositoryItemButtonEdit4Units = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEditStore = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.label8 = new System.Windows.Forms.Label();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtAllDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTable = new System.Windows.Forms.NumericUpDown();
		this.toggleSectoral = new DevExpress.XtraEditors.ToggleSwitch();
		this.toggleCashierType = new DevExpress.XtraEditors.ToggleSwitch();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtRoom = new System.Windows.Forms.NumericUpDown();
		this.TxtTotalDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label2 = new System.Windows.Forms.Label();
		this.lblRoom = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.lblTable = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtTabocc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtBarcode = new DevExpress.XtraEditors.ButtonEdit();
		this.label7 = new System.Windows.Forms.Label();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.طباعةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintQued = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPreviewFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.كشفحسابToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuNewItem = new System.Windows.Forms.ToolStripMenuItem();
		this.حركةصنفToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.صرفالحجزToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPurchOrder = new System.Windows.Forms.ToolStripMenuItem();
		this.استعلامToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.رقمالفاتورةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.اسمالموردToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.نوعالفاتورةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.f5حفظالفاتورةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.f6تعديلالفاتورةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.f7حذفالفاتورةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.f9بحثفيعروضالاسعارToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.f12فتحكرتالصنفToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ctrlDeleteحذفالسطرالحاليToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ctrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ctrlنقصانكميةالسطرالحاليToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ctrlPطباعةالفاتورةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ctrlLقفلمؤقتToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.f11اظهارالكمياتلكلالمخازنToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.الكمياتToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.الكميةالحاليةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.كلالكمياتبالمستودعاتToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.كلالكمياتبالفروعToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.تخصيصToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.tileControl1 = new DevExpress.XtraEditors.TileControl();
		this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
		this.tileControl2 = new DevExpress.XtraEditors.TileControl();
		this.tileGroup3 = new DevExpress.XtraEditors.TileGroup();
		this.tileGroup9 = new DevExpress.XtraEditors.TileGroup();
		this.tileGroup10 = new DevExpress.XtraEditors.TileGroup();
		this.tileGroup11 = new DevExpress.XtraEditors.TileGroup();
		this.tileGroup12 = new DevExpress.XtraEditors.TileGroup();
		this.tileGroup13 = new DevExpress.XtraEditors.TileGroup();
		this.tileGroup14 = new DevExpress.XtraEditors.TileGroup();
		this.TxtCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnReturnCashier = new DevExpress.XtraEditors.SimpleButton();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.ItemID = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn47 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.label48 = new System.Windows.Forms.Label();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.LblDateHold = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.toggleHold = new DevExpress.XtraEditors.ToggleSwitch();
		this.label31 = new System.Windows.Forms.Label();
		this.HoldOpenClose = new DevExpress.XtraEditors.ToggleSwitch();
		this.Comp_Store = new System.Windows.Forms.ComboBox();
		this.CBBounce = new System.Windows.Forms.ComboBox();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.label18 = new System.Windows.Forms.Label();
		this.BtnOrderSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.label9 = new System.Windows.Forms.Label();
		this.LblCustLast = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.DateHold = new DevExpress.XtraEditors.DateEdit();
		this.label28 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.BtnTypeInvoic = new DevExpress.XtraEditors.SimpleButton();
		this.BtnRecipt = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.LValid = new System.Windows.Forms.Label();
		this.LSucced = new System.Windows.Forms.Label();
		this.BtnUpload = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCustomerSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnTailor = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNewCustomer = new DevExpress.XtraEditors.SimpleButton();
		this.label22 = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.label42 = new System.Windows.Forms.Label();
		this.label43 = new System.Windows.Forms.Label();
		this.TxtAdditionalNum = new DevExpress.XtraEditors.TextEdit();
		this.TxtCommercialRecord = new DevExpress.XtraEditors.TextEdit();
		this.TxtPostalCode = new DevExpress.XtraEditors.TextEdit();
		this.TxtCountry = new DevExpress.XtraEditors.TextEdit();
		this.TxtCity = new DevExpress.XtraEditors.TextEdit();
		this.label6 = new System.Windows.Forms.Label();
		this.label37 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label46 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.TxtSales = new DevExpress.XtraEditors.TextEdit();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtNumVat = new DevExpress.XtraEditors.TextEdit();
		this.label13 = new System.Windows.Forms.Label();
		this.label34 = new System.Windows.Forms.Label();
		this.label38 = new System.Windows.Forms.Label();
		this.TxtStreet = new DevExpress.XtraEditors.TextEdit();
		this.TxtDistrict = new DevExpress.XtraEditors.TextEdit();
		this.TxtQuntity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label16 = new System.Windows.Forms.Label();
		this.LblNowQuintity = new System.Windows.Forms.Label();
		this.TxtAllQuantity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnCheckOut = new DevExpress.XtraEditors.SimpleButton();
		this.BtnMinNet = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPlusNet = new DevExpress.XtraEditors.SimpleButton();
		this.tileGroup1 = new DevExpress.XtraEditors.TileGroup();
		this.tileGroup4 = new DevExpress.XtraEditors.TileGroup();
		this.tileGroup5 = new DevExpress.XtraEditors.TileGroup();
		this.tileGroup6 = new DevExpress.XtraEditors.TileGroup();
		this.tileGroup7 = new DevExpress.XtraEditors.TileGroup();
		this.BtnDisCode = new DevExpress.XtraEditors.ButtonEdit();
		this.GropGlasses = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.lNote = new System.Windows.Forms.Label();
		this.TxtNoteGlass = new DevExpress.XtraEditors.TextEdit();
		this.BtnCustomerSearch2 = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnNewCustomer2 = new DevExpress.XtraEditors.SimpleButton();
		this.label26 = new System.Windows.Forms.Label();
		this.TxtSuppliers2 = new DevExpress.XtraEditors.ButtonEdit();
		this.label44 = new System.Windows.Forms.Label();
		this._TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this._TxtVatNum = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label51 = new System.Windows.Forms.Label();
		this.label50 = new System.Windows.Forms.Label();
		this.label49 = new System.Windows.Forms.Label();
		this.label47 = new System.Windows.Forms.Label();
		this.label45 = new System.Windows.Forms.Label();
		this.TxtLeftAdd = new DevExpress.XtraEditors.TextEdit();
		this.TxtRightSph = new DevExpress.XtraEditors.TextEdit();
		this.TxtRightCYL = new DevExpress.XtraEditors.TextEdit();
		this.TxtLeftAxis = new DevExpress.XtraEditors.TextEdit();
		this.TxtRightAxis = new DevExpress.XtraEditors.TextEdit();
		this.TxtRightAdd = new DevExpress.XtraEditors.TextEdit();
		this.TxtLeftCYL = new DevExpress.XtraEditors.TextEdit();
		this.TxtLeftSph = new DevExpress.XtraEditors.TextEdit();
		this.label39 = new System.Windows.Forms.Label();
		this.label40 = new System.Windows.Forms.Label();
		this.TxtCharge2 = new DevExpress.XtraEditors.TextEdit();
		this.TxtSales2 = new DevExpress.XtraEditors.TextEdit();
		this.BtnOpenCashir = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4Units).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleSectoral.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleCashierType.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRoom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleHold.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.HoldOpenClose.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties).BeginInit();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAdditionalNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCountry.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtStreet.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDistrict.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnDisCode.Properties).BeginInit();
		this.GropGlasses.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNoteGlass.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this._TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this._TxtVatNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLeftAdd.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRightSph.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRightCYL.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLeftAxis.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRightAxis.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRightAdd.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLeftCYL.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLeftSph.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales2.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.repositoryItemButtonEdit4Units, "repositoryItemButtonEdit4Units");
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.repositoryItemButtonEdit4Units.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4Units.Buttons"), resources.GetString("repositoryItemButtonEdit4Units.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4Units.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4Units.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4Units.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4Units.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEdit4Units.Buttons6"), resources.GetObject("repositoryItemButtonEdit4Units.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4Units.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4Units.Buttons9"))
		});
		this.repositoryItemButtonEdit4Units.Name = "repositoryItemButtonEdit4Units";
		this.repositoryItemButtonEdit4Units.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit4Units_ButtonClick);
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions2.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		editorButtonImageOptions2.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonPressed);
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"), resources.GetString("repositoryItemButtonEdit3.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit3.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit3.Buttons6"), resources.GetObject("repositoryItemButtonEdit3.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit3.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit3.Buttons9"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonClick);
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions5");
		this.repositoryItemButtonEditStore.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditStore.Buttons"), resources.GetString("repositoryItemButtonEditStore.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditStore.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditStore.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditStore.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditStore.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("repositoryItemButtonEditStore.Buttons6"), resources.GetObject("repositoryItemButtonEditStore.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditStore.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditStore.Buttons9"))
		});
		this.repositoryItemButtonEditStore.Name = "repositoryItemButtonEditStore";
		this.repositoryItemButtonEditStore.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEditStore_ButtonClick);
		resources.ApplyResources(this.TxtNet, "TxtNet");
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnEdit.Appearance.Font");
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.BtnPrint, "BtnPrint");
		this.BtnPrint.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrint.Appearance.Font");
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNew.Appearance.Font");
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDelete.Appearance.Font");
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.txtdicoutprege, "txtdicoutprege");
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Red;
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.txtdicoutprege.KeyUp += new System.Windows.Forms.KeyEventHandler(txtdicoutprege_KeyUp);
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
		resources.ApplyResources(this.TxtDiscount, "TxtDiscount");
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.ForeColor = System.Drawing.Color.Red;
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtDiscount_KeyUp);
		resources.ApplyResources(this.TxtAllDiscount, "TxtAllDiscount");
		this.TxtAllDiscount.DecimalDigits = 3;
		this.TxtAllDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtAllDiscount.Name = "TxtAllDiscount";
		this.TxtAllDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllDiscount.Value = new decimal(new int[4]);
		this.TxtAllDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtAllDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtAllDiscount_KeyUp);
		resources.ApplyResources(this.TxtTable, "TxtTable");
		this.TxtTable.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtTable.Name = "TxtTable";
		resources.ApplyResources(this.toggleSectoral, "toggleSectoral");
		this.toggleSectoral.Name = "toggleSectoral";
		this.toggleSectoral.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleSectoral.Properties.Appearance.Font");
		this.toggleSectoral.Properties.Appearance.Options.UseFont = true;
		this.toggleSectoral.Properties.Appearance.Options.UseForeColor = true;
		this.toggleSectoral.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleSectoral.Properties.GlyphVerticalAlignment");
		this.toggleSectoral.Properties.OffText = resources.GetString("toggleSectoral.Properties.OffText");
		this.toggleSectoral.Properties.OnText = resources.GetString("toggleSectoral.Properties.OnText");
		this.toggleSectoral.Toggled += new System.EventHandler(toggleCashierType_Toggled);
		resources.ApplyResources(this.toggleCashierType, "toggleCashierType");
		this.toggleCashierType.Name = "toggleCashierType";
		this.toggleCashierType.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleCashierType.Properties.Appearance.Font");
		this.toggleCashierType.Properties.Appearance.Options.UseFont = true;
		this.toggleCashierType.Properties.Appearance.Options.UseForeColor = true;
		this.toggleCashierType.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleCashierType.Properties.GlyphVerticalAlignment");
		this.toggleCashierType.Properties.OffText = resources.GetString("toggleCashierType.Properties.OffText");
		this.toggleCashierType.Properties.OnText = resources.GetString("toggleCashierType.Properties.OnText");
		this.toggleCashierType.Toggled += new System.EventHandler(toggleCashierType_Toggled);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.TxtRoom, "TxtRoom");
		this.TxtRoom.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtRoom.Name = "TxtRoom";
		resources.ApplyResources(this.TxtTotalDiscount, "TxtTotalDiscount");
		this.TxtTotalDiscount.DecimalDigits = 3;
		this.TxtTotalDiscount.ForeColor = System.Drawing.Color.Blue;
		this.TxtTotalDiscount.Name = "TxtTotalDiscount";
		this.TxtTotalDiscount.ReadOnly = true;
		this.TxtTotalDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalDiscount.UseCompatibleTextRendering = false;
		this.TxtTotalDiscount.Value = new decimal(new int[4]);
		this.TxtTotalDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.lblRoom, "lblRoom");
		this.lblRoom.Name = "lblRoom";
		this.lblRoom.Click += new System.EventHandler(lblRoom_Click);
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.LblSup, "LblSup");
		this.LblSup.Name = "LblSup";
		this.LblSup.TextChanged += new System.EventHandler(LblSup_TextChanged);
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
		this.TxtDate.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate.Properties.DisplayFormat.FormatString = "";
		this.TxtDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDate.Properties.EditFormat.FormatString = "";
		this.TxtDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDate.Properties.Mask.EditMask = resources.GetString("TxtDate.Properties.Mask.EditMask");
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate.Properties.Mask.UseMaskAsDisplayFormat");
		resources.ApplyResources(this.lblTable, "lblTable");
		this.lblTable.Name = "lblTable";
		resources.ApplyResources(this.TxtType, "TxtType");
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Name = "TxtType";
		this.TxtType.SelectedIndexChanged += new System.EventHandler(TxtType_SelectedIndexChanged_1);
		this.TxtType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress);
		resources.ApplyResources(this.LBlSuppAccount, "LBlSuppAccount");
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.TxtTabocc, "TxtTabocc");
		this.TxtTabocc.DecimalDigits = 3;
		this.TxtTabocc.ForeColor = System.Drawing.Color.Black;
		this.TxtTabocc.Name = "TxtTabocc";
		this.TxtTabocc.ReadOnly = true;
		this.TxtTabocc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTabocc.Value = new decimal(new int[4]);
		this.TxtTabocc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label11, "label11");
		this.label11.Name = "label11";
		resources.ApplyResources(this.TxtTotals, "TxtTotals");
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.ReadOnly = true;
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtVatDisc, "TxtVatDisc");
		this.TxtVatDisc.DecimalDigits = 3;
		this.TxtVatDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatDisc.Name = "TxtVatDisc";
		this.TxtVatDisc.ReadOnly = true;
		this.TxtVatDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatDisc.Value = new decimal(new int[4]);
		this.TxtVatDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtVat, "TxtVat");
		this.TxtVat.DecimalDigits = 3;
		this.TxtVat.ForeColor = System.Drawing.Color.Teal;
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.ReadOnly = true;
		this.TxtVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4]);
		this.TxtVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.TxtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(gridControl1_KeyDown);
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[19]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.طباعةToolStripMenuItem1, this.toolStripSeparator1, this.كشفحسابToolStripMenuItem, this.MenuNewItem, this.حركةصنفToolStripMenuItem, this.MenuSearchItem,
			this.صرفالحجزToolStripMenuItem, this.toolStripSeparator4, this.MenuPurchOrder, this.استعلامToolStripMenuItem1, this.الكمياتToolStripMenuItem1, this.toolStripSeparator2, this.تخصيصToolStripMenuItem1, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		resources.ApplyResources(this.MenuNew, "MenuNew");
		this.MenuNew.Name = "MenuNew";
		this.MenuNew.Click += new System.EventHandler(MenuNew_Click);
		resources.ApplyResources(this.MenuSave, "MenuSave");
		this.MenuSave.Name = "MenuSave";
		this.MenuSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.MenuEdit, "MenuEdit");
		this.MenuEdit.Name = "MenuEdit";
		this.MenuEdit.Click += new System.EventHandler(BtnEdit_Click);
		resources.ApplyResources(this.MenuDelete, "MenuDelete");
		this.MenuDelete.Name = "MenuDelete";
		this.MenuDelete.Click += new System.EventHandler(BtnDelete_Click);
		resources.ApplyResources(this.طباعةToolStripMenuItem1, "طباعةToolStripMenuItem1");
		this.طباعةToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.MenuPrintQued, this.MenuPreviewFatoura, this.MenuPrintFatoura });
		this.طباعةToolStripMenuItem1.Name = "طباعةToolStripMenuItem1";
		resources.ApplyResources(this.MenuPrintQued, "MenuPrintQued");
		this.MenuPrintQued.Name = "MenuPrintQued";
		this.MenuPrintQued.Click += new System.EventHandler(MenuPrintQued_Click);
		resources.ApplyResources(this.MenuPreviewFatoura, "MenuPreviewFatoura");
		this.MenuPreviewFatoura.Name = "MenuPreviewFatoura";
		this.MenuPreviewFatoura.Click += new System.EventHandler(MenuPreviewFatoura_Click_1);
		resources.ApplyResources(this.MenuPrintFatoura, "MenuPrintFatoura");
		this.MenuPrintFatoura.Name = "MenuPrintFatoura";
		this.MenuPrintFatoura.Click += new System.EventHandler(MenuPrintFatoura_Click_1);
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.كشفحسابToolStripMenuItem, "كشفحسابToolStripMenuItem");
		this.كشفحسابToolStripMenuItem.Name = "كشفحسابToolStripMenuItem";
		this.كشفحسابToolStripMenuItem.Click += new System.EventHandler(كشفحسابToolStripMenuItem_Click);
		resources.ApplyResources(this.MenuNewItem, "MenuNewItem");
		this.MenuNewItem.Name = "MenuNewItem";
		this.MenuNewItem.Click += new System.EventHandler(MenuNewItem_Click);
		resources.ApplyResources(this.حركةصنفToolStripMenuItem, "حركةصنفToolStripMenuItem");
		this.حركةصنفToolStripMenuItem.Name = "حركةصنفToolStripMenuItem";
		this.حركةصنفToolStripMenuItem.Click += new System.EventHandler(حركةصنفToolStripMenuItem_Click);
		resources.ApplyResources(this.MenuSearchItem, "MenuSearchItem");
		this.MenuSearchItem.Name = "MenuSearchItem";
		this.MenuSearchItem.Click += new System.EventHandler(MenuSearchItem_Click);
		resources.ApplyResources(this.صرفالحجزToolStripMenuItem, "صرفالحجزToolStripMenuItem");
		this.صرفالحجزToolStripMenuItem.Name = "صرفالحجزToolStripMenuItem";
		this.صرفالحجزToolStripMenuItem.Click += new System.EventHandler(صرفالحجزToolStripMenuItem_Click);
		resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		resources.ApplyResources(this.MenuPurchOrder, "MenuPurchOrder");
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		this.MenuPurchOrder.Click += new System.EventHandler(MenuPurchOrder_Click);
		resources.ApplyResources(this.استعلامToolStripMenuItem1, "استعلامToolStripMenuItem1");
		this.استعلامToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this.رقمالفاتورةToolStripMenuItem1, this.اسمالموردToolStripMenuItem1, this.نوعالفاتورةToolStripMenuItem1, this.f5حفظالفاتورةToolStripMenuItem, this.f6تعديلالفاتورةToolStripMenuItem, this.f7حذفالفاتورةToolStripMenuItem, this.f9بحثفيعروضالاسعارToolStripMenuItem, this.f12فتحكرتالصنفToolStripMenuItem, this.ctrlDeleteحذفالسطرالحاليToolStripMenuItem, this.ctrlToolStripMenuItem,
			this.ctrlنقصانكميةالسطرالحاليToolStripMenuItem, this.ctrlPطباعةالفاتورةToolStripMenuItem, this.ctrlLقفلمؤقتToolStripMenuItem, this.f11اظهارالكمياتلكلالمخازنToolStripMenuItem, this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem, this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem1, this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem2
		});
		this.استعلامToolStripMenuItem1.Name = "استعلامToolStripMenuItem1";
		resources.ApplyResources(this.رقمالفاتورةToolStripMenuItem1, "رقمالفاتورةToolStripMenuItem1");
		this.رقمالفاتورةToolStripMenuItem1.Name = "رقمالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.اسمالموردToolStripMenuItem1, "اسمالموردToolStripMenuItem1");
		this.اسمالموردToolStripMenuItem1.Name = "اسمالموردToolStripMenuItem1";
		resources.ApplyResources(this.نوعالفاتورةToolStripMenuItem1, "نوعالفاتورةToolStripMenuItem1");
		this.نوعالفاتورةToolStripMenuItem1.Name = "نوعالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.f5حفظالفاتورةToolStripMenuItem, "f5حفظالفاتورةToolStripMenuItem");
		this.f5حفظالفاتورةToolStripMenuItem.Name = "f5حفظالفاتورةToolStripMenuItem";
		resources.ApplyResources(this.f6تعديلالفاتورةToolStripMenuItem, "f6تعديلالفاتورةToolStripMenuItem");
		this.f6تعديلالفاتورةToolStripMenuItem.Name = "f6تعديلالفاتورةToolStripMenuItem";
		resources.ApplyResources(this.f7حذفالفاتورةToolStripMenuItem, "f7حذفالفاتورةToolStripMenuItem");
		this.f7حذفالفاتورةToolStripMenuItem.Name = "f7حذفالفاتورةToolStripMenuItem";
		resources.ApplyResources(this.f9بحثفيعروضالاسعارToolStripMenuItem, "f9بحثفيعروضالاسعارToolStripMenuItem");
		this.f9بحثفيعروضالاسعارToolStripMenuItem.Name = "f9بحثفيعروضالاسعارToolStripMenuItem";
		resources.ApplyResources(this.f12فتحكرتالصنفToolStripMenuItem, "f12فتحكرتالصنفToolStripMenuItem");
		this.f12فتحكرتالصنفToolStripMenuItem.Name = "f12فتحكرتالصنفToolStripMenuItem";
		resources.ApplyResources(this.ctrlDeleteحذفالسطرالحاليToolStripMenuItem, "ctrlDeleteحذفالسطرالحاليToolStripMenuItem");
		this.ctrlDeleteحذفالسطرالحاليToolStripMenuItem.Name = "ctrlDeleteحذفالسطرالحاليToolStripMenuItem";
		resources.ApplyResources(this.ctrlToolStripMenuItem, "ctrlToolStripMenuItem");
		this.ctrlToolStripMenuItem.Name = "ctrlToolStripMenuItem";
		resources.ApplyResources(this.ctrlنقصانكميةالسطرالحاليToolStripMenuItem, "ctrlنقصانكميةالسطرالحاليToolStripMenuItem");
		this.ctrlنقصانكميةالسطرالحاليToolStripMenuItem.Name = "ctrlنقصانكميةالسطرالحاليToolStripMenuItem";
		resources.ApplyResources(this.ctrlPطباعةالفاتورةToolStripMenuItem, "ctrlPطباعةالفاتورةToolStripMenuItem");
		this.ctrlPطباعةالفاتورةToolStripMenuItem.Name = "ctrlPطباعةالفاتورةToolStripMenuItem";
		resources.ApplyResources(this.ctrlLقفلمؤقتToolStripMenuItem, "ctrlLقفلمؤقتToolStripMenuItem");
		this.ctrlLقفلمؤقتToolStripMenuItem.Name = "ctrlLقفلمؤقتToolStripMenuItem";
		resources.ApplyResources(this.f11اظهارالكمياتلكلالمخازنToolStripMenuItem, "f11اظهارالكمياتلكلالمخازنToolStripMenuItem");
		this.f11اظهارالكمياتلكلالمخازنToolStripMenuItem.Name = "f11اظهارالكمياتلكلالمخازنToolStripMenuItem";
		resources.ApplyResources(this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem, "وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem");
		this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem.Name = "وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem";
		resources.ApplyResources(this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem1, "وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem1");
		this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem1.Name = "وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem1";
		resources.ApplyResources(this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem2, "وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem2");
		this.وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem2.Name = "وبعدهاالرقمفيخانةالبحثبالباركودلزيادةالكميةعليالصنفToolStripMenuItem2";
		resources.ApplyResources(this.الكمياتToolStripMenuItem1, "الكمياتToolStripMenuItem1");
		this.الكمياتToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.الكميةالحاليةToolStripMenuItem, this.كلالكمياتبالمستودعاتToolStripMenuItem, this.كلالكمياتبالفروعToolStripMenuItem });
		this.الكمياتToolStripMenuItem1.Name = "الكمياتToolStripMenuItem1";
		resources.ApplyResources(this.الكميةالحاليةToolStripMenuItem, "الكميةالحاليةToolStripMenuItem");
		this.الكميةالحاليةToolStripMenuItem.Name = "الكميةالحاليةToolStripMenuItem";
		resources.ApplyResources(this.كلالكمياتبالمستودعاتToolStripMenuItem, "كلالكمياتبالمستودعاتToolStripMenuItem");
		this.كلالكمياتبالمستودعاتToolStripMenuItem.Name = "كلالكمياتبالمستودعاتToolStripMenuItem";
		resources.ApplyResources(this.كلالكمياتبالفروعToolStripMenuItem, "كلالكمياتبالفروعToolStripMenuItem");
		this.كلالكمياتبالفروعToolStripMenuItem.Name = "كلالكمياتبالفروعToolStripMenuItem";
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.تخصيصToolStripMenuItem1, "تخصيصToolStripMenuItem1");
		this.تخصيصToolStripMenuItem1.Name = "تخصيصToolStripMenuItem1";
		resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		resources.ApplyResources(this.MenuExit, "MenuExit");
		this.MenuExit.Name = "MenuExit";
		this.MenuExit.Click += new System.EventHandler(MenuExit_Click);
		resources.ApplyResources(this.tileControl1, "tileControl1");
		this.tileControl1.AllowDrag = false;
		this.tileControl1.AllowDragTilesBetweenGroups = false;
		this.tileControl1.Groups.Add(this.tileGroup2);
		this.tileControl1.Name = "tileControl1";
		this.tileControl1.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.tileGroup2.Name = "tileGroup2";
		resources.ApplyResources(this.tileGroup2, "tileGroup2");
		resources.ApplyResources(this.tileControl2, "tileControl2");
		this.tileControl2.AllowDrag = false;
		this.tileControl2.AllowDragTilesBetweenGroups = false;
		this.tileControl2.Cursor = System.Windows.Forms.Cursors.Default;
		this.tileControl2.Groups.Add(this.tileGroup3);
		this.tileControl2.Groups.Add(this.tileGroup9);
		this.tileControl2.Groups.Add(this.tileGroup10);
		this.tileControl2.Groups.Add(this.tileGroup11);
		this.tileControl2.Groups.Add(this.tileGroup12);
		this.tileControl2.Groups.Add(this.tileGroup13);
		this.tileControl2.Groups.Add(this.tileGroup14);
		this.tileControl2.ItemSize = 90;
		this.tileControl2.MaxId = 28;
		this.tileControl2.Name = "tileControl2";
		this.tileControl2.Orientation = System.Windows.Forms.Orientation.Vertical;
		this.tileControl2.RowCount = 9999999;
		this.tileControl2.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.tileGroup3.Name = "tileGroup3";
		resources.ApplyResources(this.tileGroup3, "tileGroup3");
		this.tileGroup9.Name = "tileGroup9";
		resources.ApplyResources(this.tileGroup9, "tileGroup9");
		this.tileGroup10.Name = "tileGroup10";
		resources.ApplyResources(this.tileGroup10, "tileGroup10");
		this.tileGroup11.Name = "tileGroup11";
		resources.ApplyResources(this.tileGroup11, "tileGroup11");
		this.tileGroup12.Name = "tileGroup12";
		resources.ApplyResources(this.tileGroup12, "tileGroup12");
		this.tileGroup13.Name = "tileGroup13";
		resources.ApplyResources(this.tileGroup13, "tileGroup13");
		this.tileGroup14.Name = "tileGroup14";
		resources.ApplyResources(this.tileGroup14, "tileGroup14");
		resources.ApplyResources(this.TxtCost, "TxtCost");
		this.TxtCost.DecimalDigits = 3;
		this.TxtCost.ForeColor = System.Drawing.Color.Black;
		this.TxtCost.Name = "TxtCost";
		this.TxtCost.ReadOnly = true;
		this.TxtCost.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCost.Value = new decimal(new int[4]);
		this.TxtCost.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.BtnReturnCashier, "BtnReturnCashier");
		this.BtnReturnCashier.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnReturnCashier.Appearance.Font");
		this.BtnReturnCashier.Appearance.Options.UseFont = true;
		this.BtnReturnCashier.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReturnCashier.ImageOptions.Image");
		this.BtnReturnCashier.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnReturnCashier.Name = "BtnReturnCashier";
		this.BtnReturnCashier.Click += new System.EventHandler(BtnReturnCashier_Click);
		resources.ApplyResources(this.label14, "label14");
		this.label14.Name = "label14";
		resources.ApplyResources(this.label15, "label15");
		this.label15.Name = "label15";
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
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[40]
		{
			this.gridColumn24, this.gridColumn23, this.gridColumn1, this.gridColumn2, this.gridColumn26, this.shearch, this.gridColumn19, this.gridColumn25, this.gridColumn20, this.gridColumn21,
			this.gridColumn3, this.gridColumn6, this.gridColumn7, this.gridColumn22, this.gridColumn5, this.gridColumn8, this.gridColumn9, this.gridColumn27, this.gridColumn28, this.gridColumn29,
			this.gridColumn30, this.gridColumn31, this.gridColumn4, this.gridColumn34, this.gridColumn35, this.ItemID, this.gridColumn37, this.gridColumn36, this.gridColumn38, this.gridColumn32,
			this.gridColumn33, this.gridColumn39, this.gridColumn40, this.gridColumn41, this.gridColumn42, this.gridColumn43, this.gridColumn44, this.gridColumn45, this.gridColumn46, this.gridColumn47
		});
		this.gridView1.DetailHeight = 354;
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
		this.gridView1.Name = "gridView1";
		this.gridView1.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.False;
		this.gridView1.OptionsBehavior.KeepGroupExpandedOnSorting = false;
		this.gridView1.OptionsCustomization.AllowSort = false;
		this.gridView1.OptionsDetail.EnableMasterViewMode = false;
		this.gridView1.OptionsMenu.ShowGroupSortSummaryItems = false;
		this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
		this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
		this.gridView1.OptionsPrint.EnableAppearanceOddRow = true;
		this.gridView1.OptionsPrint.PrintFilterInfo = true;
		this.gridView1.OptionsPrint.PrintPreview = true;
		this.gridView1.OptionsView.RowAutoHeight = true;
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridView1.OptionsView.ShowGroupPanel = false;
		this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
		resources.ApplyResources(this.gridColumn24, "gridColumn24");
		this.gridColumn24.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceCell.Font");
		this.gridColumn24.AppearanceCell.Options.UseFont = true;
		this.gridColumn24.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceHeader.Font");
		this.gridColumn24.AppearanceHeader.Options.UseFont = true;
		this.gridColumn24.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.FieldName = "ItemVatDisc";
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn24.OptionsColumn.AllowEdit = false;
		this.gridColumn24.OptionsColumn.AllowFocus = false;
		this.gridColumn24.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
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
		this.gridColumn23.FieldName = "SumPriceDisc";
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.OptionsColumn.AllowEdit = false;
		this.gridColumn23.OptionsColumn.AllowFocus = false;
		this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.FieldName = "ItemCode";
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.OptionsColumn.AllowEdit = false;
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
		this.gridColumn2.FieldName = "ItemName";
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn26, "gridColumn26");
		this.gridColumn26.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceCell.Font");
		this.gridColumn26.AppearanceCell.Options.UseFont = true;
		this.gridColumn26.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceHeader.Font");
		this.gridColumn26.AppearanceHeader.Options.UseFont = true;
		this.gridColumn26.FieldName = "Group";
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.shearch, "shearch");
		this.shearch.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("shearch.AppearanceCell.Font");
		this.shearch.AppearanceCell.Options.UseFont = true;
		this.shearch.AppearanceCell.Options.UseTextOptions = true;
		this.shearch.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("shearch.AppearanceHeader.Font");
		this.shearch.AppearanceHeader.Options.UseFont = true;
		this.shearch.AppearanceHeader.Options.UseTextOptions = true;
		this.shearch.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.ColumnEdit = this.repositoryItemButtonEdit4Units;
		this.shearch.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.FieldName = "UnitType";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		this.shearch.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceCell.Font");
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.FieldName = "UnitName";
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		this.gridColumn19.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn25, "gridColumn25");
		this.gridColumn25.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceCell.Font");
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceHeader.Font");
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.FieldName = "AvergItem";
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
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
		this.gridColumn20.FieldName = "Quantity";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn20.Summary"), resources.GetString("gridColumn20.Summary1"), resources.GetString("gridColumn20.Summary2"))
		});
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
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
		this.gridColumn21.FieldName = "UnitPrice";
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn3, "gridColumn3");
		this.gridColumn3.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceCell.Font");
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceHeader.Font");
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.FieldName = "SumPrice";
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn3.Summary"), resources.GetString("gridColumn3.Summary1"), resources.GetString("gridColumn3.Summary2"))
		});
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn6, "gridColumn6");
		this.gridColumn6.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
		this.gridColumn6.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceCell.Font");
		this.gridColumn6.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn6.AppearanceCell.FontStyleDelta");
		this.gridColumn6.AppearanceCell.Options.UseBackColor = true;
		this.gridColumn6.AppearanceCell.Options.UseFont = true;
		this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceHeader.Font");
		this.gridColumn6.AppearanceHeader.Options.UseFont = true;
		this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.FieldName = "ItemVat";
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.ReadOnly = true;
		this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn7, "gridColumn7");
		this.gridColumn7.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceCell.Font");
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceHeader.Font");
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.FieldName = "Total";
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn7.Summary"), resources.GetString("gridColumn7.Summary1"), resources.GetString("gridColumn7.Summary2"))
		});
		this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn22, "gridColumn22");
		this.gridColumn22.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceCell.Font");
		this.gridColumn22.AppearanceCell.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceHeader.Font");
		this.gridColumn22.AppearanceHeader.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.ColumnEdit = this.repositoryItemButtonEdit1;
		this.gridColumn22.FieldName = "زيادة";
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn22.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn5, "gridColumn5");
		this.gridColumn5.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceCell.Font");
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceHeader.Font");
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.ColumnEdit = this.repositoryItemButtonEdit3;
		this.gridColumn5.FieldName = "نقصان";
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit2;
		this.gridColumn8.FieldName = "حذف";
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn9, "gridColumn9");
		this.gridColumn9.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
		this.gridColumn9.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceCell.Font");
		this.gridColumn9.AppearanceCell.Options.UseBackColor = true;
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceHeader.Font");
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.FieldName = "SN";
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn27, "gridColumn27");
		this.gridColumn27.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceCell.Font");
		this.gridColumn27.AppearanceCell.Options.UseFont = true;
		this.gridColumn27.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceHeader.Font");
		this.gridColumn27.AppearanceHeader.Options.UseFont = true;
		this.gridColumn27.FieldName = "SumAvergItem";
		this.gridColumn27.Name = "gridColumn27";
		this.gridColumn27.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn28, "gridColumn28");
		this.gridColumn28.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn28.AppearanceCell.Font");
		this.gridColumn28.AppearanceCell.Options.UseFont = true;
		this.gridColumn28.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn28.AppearanceHeader.Font");
		this.gridColumn28.AppearanceHeader.Options.UseFont = true;
		this.gridColumn28.FieldName = "Height";
		this.gridColumn28.Name = "gridColumn28";
		this.gridColumn28.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn29, "gridColumn29");
		this.gridColumn29.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceCell.Font");
		this.gridColumn29.AppearanceCell.Options.UseFont = true;
		this.gridColumn29.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceHeader.Font");
		this.gridColumn29.AppearanceHeader.Options.UseFont = true;
		this.gridColumn29.FieldName = "Width";
		this.gridColumn29.Name = "gridColumn29";
		this.gridColumn29.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn30, "gridColumn30");
		this.gridColumn30.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn30.AppearanceCell.Font");
		this.gridColumn30.AppearanceCell.Options.UseFont = true;
		this.gridColumn30.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn30.AppearanceHeader.Font");
		this.gridColumn30.AppearanceHeader.Options.UseFont = true;
		this.gridColumn30.FieldName = "Long";
		this.gridColumn30.Name = "gridColumn30";
		this.gridColumn30.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn31, "gridColumn31");
		this.gridColumn31.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn31.AppearanceCell.Font");
		this.gridColumn31.AppearanceCell.Options.UseFont = true;
		this.gridColumn31.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn31.AppearanceHeader.Font");
		this.gridColumn31.AppearanceHeader.Options.UseFont = true;
		this.gridColumn31.FieldName = "Weight";
		this.gridColumn31.Name = "gridColumn31";
		this.gridColumn31.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn4, "gridColumn4");
		this.gridColumn4.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceCell.Font");
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceHeader.Font");
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.ColumnEdit = this.repositoryItemButtonEditStore;
		this.gridColumn4.FieldName = "StoreName";
		this.gridColumn4.MinWidth = 23;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn34, "gridColumn34");
		this.gridColumn34.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn34.AppearanceCell.Font");
		this.gridColumn34.AppearanceCell.Options.UseFont = true;
		this.gridColumn34.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn34.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn34.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn34.AppearanceHeader.Font");
		this.gridColumn34.AppearanceHeader.Options.UseFont = true;
		this.gridColumn34.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn34.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn34.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn34.FieldName = "Net";
		this.gridColumn34.MinWidth = 23;
		this.gridColumn34.Name = "gridColumn34";
		this.gridColumn34.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn34.Summary"), resources.GetString("gridColumn34.Summary1"), resources.GetString("gridColumn34.Summary2"))
		});
		resources.ApplyResources(this.gridColumn35, "gridColumn35");
		this.gridColumn35.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn35.AppearanceCell.Font");
		this.gridColumn35.AppearanceCell.Options.UseFont = true;
		this.gridColumn35.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn35.AppearanceHeader.Font");
		this.gridColumn35.AppearanceHeader.Options.UseFont = true;
		this.gridColumn35.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn35.FieldName = "BounceItem";
		this.gridColumn35.MinWidth = 23;
		this.gridColumn35.Name = "gridColumn35";
		this.gridColumn35.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.ItemID, "ItemID");
		this.ItemID.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("ItemID.AppearanceCell.Font");
		this.ItemID.AppearanceCell.Options.UseFont = true;
		this.ItemID.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("ItemID.AppearanceHeader.Font");
		this.ItemID.AppearanceHeader.Options.UseFont = true;
		this.ItemID.FieldName = "ItemID";
		this.ItemID.MinWidth = 23;
		this.ItemID.Name = "ItemID";
		this.ItemID.OptionsColumn.AllowEdit = false;
		this.ItemID.OptionsColumn.AllowFocus = false;
		this.ItemID.OptionsColumn.ReadOnly = true;
		this.ItemID.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn37, "gridColumn37");
		this.gridColumn37.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn37.AppearanceCell.Font");
		this.gridColumn37.AppearanceCell.Options.UseFont = true;
		this.gridColumn37.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn37.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn37.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn37.AppearanceHeader.Font");
		this.gridColumn37.AppearanceHeader.Options.UseFont = true;
		this.gridColumn37.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn37.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn37.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn37.FieldName = "UnitPriceDisc";
		this.gridColumn37.MinWidth = 27;
		this.gridColumn37.Name = "gridColumn37";
		this.gridColumn37.OptionsColumn.AllowEdit = false;
		this.gridColumn37.OptionsColumn.AllowFocus = false;
		this.gridColumn37.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn36, "gridColumn36");
		this.gridColumn36.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceCell.Font");
		this.gridColumn36.AppearanceCell.Options.UseFont = true;
		this.gridColumn36.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn36.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn36.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceHeader.Font");
		this.gridColumn36.AppearanceHeader.Options.UseFont = true;
		this.gridColumn36.AppearanceHeader.Options.UseImage = true;
		this.gridColumn36.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn36.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn36.FieldName = "TaxTobacco";
		this.gridColumn36.MinWidth = 25;
		this.gridColumn36.Name = "gridColumn36";
		this.gridColumn36.OptionsColumn.AllowEdit = false;
		this.gridColumn36.OptionsColumn.AllowFocus = false;
		this.gridColumn36.OptionsColumn.ReadOnly = true;
		this.gridColumn36.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn38, "gridColumn38");
		this.gridColumn38.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn38.AppearanceCell.Font");
		this.gridColumn38.AppearanceCell.Options.UseFont = true;
		this.gridColumn38.AppearanceCell.Options.UseImage = true;
		this.gridColumn38.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn38.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn38.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn38.AppearanceHeader.Font");
		this.gridColumn38.AppearanceHeader.Options.UseFont = true;
		this.gridColumn38.AppearanceHeader.Options.UseImage = true;
		this.gridColumn38.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn38.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn38.FieldName = "TotalWithTaxTobacco";
		this.gridColumn38.MinWidth = 25;
		this.gridColumn38.Name = "gridColumn38";
		this.gridColumn38.OptionsColumn.AllowEdit = false;
		this.gridColumn38.OptionsColumn.AllowFocus = false;
		this.gridColumn38.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn32, "gridColumn32");
		this.gridColumn32.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn32.AppearanceCell.Font");
		this.gridColumn32.AppearanceCell.Options.UseFont = true;
		this.gridColumn32.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn32.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn32.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn32.AppearanceHeader.Font");
		this.gridColumn32.AppearanceHeader.Options.UseFont = true;
		this.gridColumn32.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn32.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn32.FieldName = "TaxTobaccoDis";
		this.gridColumn32.MinWidth = 25;
		this.gridColumn32.Name = "gridColumn32";
		this.gridColumn32.OptionsColumn.AllowEdit = false;
		this.gridColumn32.OptionsColumn.AllowFocus = false;
		this.gridColumn32.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn33, "gridColumn33");
		this.gridColumn33.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn33.AppearanceCell.Font");
		this.gridColumn33.AppearanceCell.Options.UseFont = true;
		this.gridColumn33.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn33.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn33.AppearanceHeader.Font");
		this.gridColumn33.AppearanceHeader.Options.UseFont = true;
		this.gridColumn33.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn33.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn33.FieldName = "InstallationPrice";
		this.gridColumn33.MinWidth = 25;
		this.gridColumn33.Name = "gridColumn33";
		this.gridColumn33.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn39, "gridColumn39");
		this.gridColumn39.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn39.AppearanceCell.Font");
		this.gridColumn39.AppearanceCell.Options.UseFont = true;
		this.gridColumn39.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn39.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn39.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn39.AppearanceHeader.Font");
		this.gridColumn39.AppearanceHeader.Options.UseFont = true;
		this.gridColumn39.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn39.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn39.FieldName = "UnitID";
		this.gridColumn39.MinWidth = 25;
		this.gridColumn39.Name = "gridColumn39";
		this.gridColumn39.OptionsFilter.AllowAutoFilter = false;
		this.gridColumn39.OptionsFilter.AllowFilter = false;
		this.gridColumn39.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn40, "gridColumn40");
		this.gridColumn40.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn40.AppearanceCell.Font");
		this.gridColumn40.AppearanceCell.Options.UseFont = true;
		this.gridColumn40.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn40.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn40.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn40.AppearanceHeader.Font");
		this.gridColumn40.AppearanceHeader.Options.UseFont = true;
		this.gridColumn40.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn40.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn40.FieldName = "StoreID";
		this.gridColumn40.MinWidth = 25;
		this.gridColumn40.Name = "gridColumn40";
		this.gridColumn40.OptionsFilter.AllowAutoFilter = false;
		this.gridColumn40.OptionsFilter.AllowFilter = false;
		this.gridColumn40.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn41, "gridColumn41");
		this.gridColumn41.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn41.AppearanceCell.Font");
		this.gridColumn41.AppearanceCell.Options.UseFont = true;
		this.gridColumn41.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn41.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn41.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn41.AppearanceHeader.Font");
		this.gridColumn41.AppearanceHeader.Options.UseFont = true;
		this.gridColumn41.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn41.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn41.FieldName = "AmountDisc";
		this.gridColumn41.MinWidth = 25;
		this.gridColumn41.Name = "gridColumn41";
		this.gridColumn41.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn42, "gridColumn42");
		this.gridColumn42.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn42.AppearanceCell.Font");
		this.gridColumn42.AppearanceCell.Options.UseFont = true;
		this.gridColumn42.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn42.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn42.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn42.AppearanceHeader.Font");
		this.gridColumn42.AppearanceHeader.Options.UseFont = true;
		this.gridColumn42.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn42.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn42.FieldName = "PercentDisc";
		this.gridColumn42.MinWidth = 25;
		this.gridColumn42.Name = "gridColumn42";
		this.gridColumn42.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn43, "gridColumn43");
		this.gridColumn43.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn43.AppearanceCell.Font");
		this.gridColumn43.AppearanceCell.Options.UseFont = true;
		this.gridColumn43.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn43.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn43.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn43.AppearanceHeader.Font");
		this.gridColumn43.AppearanceHeader.Options.UseFont = true;
		this.gridColumn43.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn43.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn43.FieldName = "Tax_Value";
		this.gridColumn43.MinWidth = 25;
		this.gridColumn43.Name = "gridColumn43";
		this.gridColumn43.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn44, "gridColumn44");
		this.gridColumn44.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn44.AppearanceCell.Font");
		this.gridColumn44.AppearanceCell.Options.UseFont = true;
		this.gridColumn44.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn44.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn44.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn44.AppearanceHeader.Font");
		this.gridColumn44.AppearanceHeader.Options.UseFont = true;
		this.gridColumn44.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn44.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn44.FieldName = "InstallPriceDis";
		this.gridColumn44.MinWidth = 25;
		this.gridColumn44.Name = "gridColumn44";
		this.gridColumn44.OptionsColumn.AllowEdit = false;
		this.gridColumn44.OptionsColumn.AllowFocus = false;
		this.gridColumn44.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn44.Summary"), resources.GetString("gridColumn44.Summary1"), resources.GetString("gridColumn44.Summary2"))
		});
		resources.ApplyResources(this.gridColumn45, "gridColumn45");
		this.gridColumn45.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn45.AppearanceCell.Font");
		this.gridColumn45.AppearanceCell.Options.UseFont = true;
		this.gridColumn45.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn45.AppearanceHeader.Font");
		this.gridColumn45.AppearanceHeader.Options.UseFont = true;
		this.gridColumn45.FieldName = "VatCode";
		this.gridColumn45.MinWidth = 25;
		this.gridColumn45.Name = "gridColumn45";
		this.gridColumn45.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn46, "gridColumn46");
		this.gridColumn46.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn46.AppearanceCell.Font");
		this.gridColumn46.AppearanceCell.Options.UseFont = true;
		this.gridColumn46.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn46.AppearanceHeader.Font");
		this.gridColumn46.AppearanceHeader.Options.UseFont = true;
		this.gridColumn46.FieldName = "TaxExemptionReason";
		this.gridColumn46.MinWidth = 25;
		this.gridColumn46.Name = "gridColumn46";
		this.gridColumn46.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn47, "gridColumn47");
		this.gridColumn47.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn47.AppearanceCell.Font");
		this.gridColumn47.AppearanceCell.Options.UseFont = true;
		this.gridColumn47.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn47.AppearanceHeader.Font");
		this.gridColumn47.AppearanceHeader.Options.UseFont = true;
		this.gridColumn47.FieldName = "TaxExemptionReasonCode";
		this.gridColumn47.MinWidth = 25;
		this.gridColumn47.Name = "gridColumn47";
		this.gridColumn47.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.cardView1, "cardView1");
		this.cardView1.CardWidth = 213;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.DetailHeight = 354;
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
		resources.ApplyResources(this.groupBox5, "groupBox5");
		this.groupBox5.Controls.Add(this.label48);
		this.groupBox5.Controls.Add(this.TxtNote);
		this.groupBox5.Controls.Add(this.LblDateHold);
		this.groupBox5.Controls.Add(this.label36);
		this.groupBox5.Controls.Add(this.TxtCode);
		this.groupBox5.Controls.Add(this.toggleHold);
		this.groupBox5.Controls.Add(this.label31);
		this.groupBox5.Controls.Add(this.TxtTable);
		this.groupBox5.Controls.Add(this.HoldOpenClose);
		this.groupBox5.Controls.Add(this.TxtRoom);
		this.groupBox5.Controls.Add(this.toggleCashierType);
		this.groupBox5.Controls.Add(this.LblSup);
		this.groupBox5.Controls.Add(this.Comp_Store);
		this.groupBox5.Controls.Add(this.toggleSectoral);
		this.groupBox5.Controls.Add(this.CBBounce);
		this.groupBox5.Controls.Add(this.LBlSuppAccount);
		this.groupBox5.Controls.Add(this.TxtNumNote);
		this.groupBox5.Controls.Add(this.label18);
		this.groupBox5.Controls.Add(this.BtnOrderSearch);
		this.groupBox5.Controls.Add(this.label9);
		this.groupBox5.Controls.Add(this.LblCustLast);
		this.groupBox5.Controls.Add(this.lblRoom);
		this.groupBox5.Controls.Add(this.label27);
		this.groupBox5.Controls.Add(this.DateHold);
		this.groupBox5.Controls.Add(this.TxtDate);
		this.groupBox5.Controls.Add(this.label28);
		this.groupBox5.Controls.Add(this.lblTable);
		this.groupBox5.Controls.Add(this.label29);
		this.groupBox5.Controls.Add(this.label30);
		this.groupBox5.Controls.Add(this.TxtType);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.TabStop = false;
		resources.ApplyResources(this.label48, "label48");
		this.label48.Name = "label48";
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNote.Properties.Appearance.Font");
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.LblDateHold, "LblDateHold");
		this.LblDateHold.Name = "LblDateHold";
		resources.ApplyResources(this.label36, "label36");
		this.label36.Name = "label36";
		resources.ApplyResources(this.toggleHold, "toggleHold");
		this.toggleHold.Name = "toggleHold";
		this.toggleHold.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleHold.Properties.Appearance.Font");
		this.toggleHold.Properties.Appearance.Options.UseFont = true;
		this.toggleHold.Properties.Appearance.Options.UseForeColor = true;
		this.toggleHold.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleHold.Properties.GlyphVerticalAlignment");
		this.toggleHold.Properties.OffText = resources.GetString("toggleHold.Properties.OffText");
		this.toggleHold.Properties.OnText = resources.GetString("toggleHold.Properties.OnText");
		this.toggleHold.Properties.Click += new System.EventHandler(toggleHold_Properties_Click);
		this.toggleHold.Toggled += new System.EventHandler(toggleHold_Toggled);
		resources.ApplyResources(this.label31, "label31");
		this.label31.Name = "label31";
		resources.ApplyResources(this.HoldOpenClose, "HoldOpenClose");
		this.HoldOpenClose.Name = "HoldOpenClose";
		this.HoldOpenClose.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("HoldOpenClose.Properties.Appearance.Font");
		this.HoldOpenClose.Properties.Appearance.Options.UseFont = true;
		this.HoldOpenClose.Properties.Appearance.Options.UseForeColor = true;
		this.HoldOpenClose.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("HoldOpenClose.Properties.GlyphVerticalAlignment");
		this.HoldOpenClose.Properties.OffText = resources.GetString("HoldOpenClose.Properties.OffText");
		this.HoldOpenClose.Properties.OnText = resources.GetString("HoldOpenClose.Properties.OnText");
		this.HoldOpenClose.Click += new System.EventHandler(HoldOpenClose_Click);
		resources.ApplyResources(this.Comp_Store, "Comp_Store");
		this.Comp_Store.FormattingEnabled = true;
		this.Comp_Store.Name = "Comp_Store";
		this.Comp_Store.SelectedIndexChanged += new System.EventHandler(Comp_Store_SelectedIndexChanged);
		resources.ApplyResources(this.CBBounce, "CBBounce");
		this.CBBounce.FormattingEnabled = true;
		this.CBBounce.Name = "CBBounce";
		this.CBBounce.SelectedIndexChanged += new System.EventHandler(CBBounce_SelectedIndexChanged);
		resources.ApplyResources(this.TxtNumNote, "TxtNumNote");
		this.TxtNumNote.Name = "TxtNumNote";
		this.TxtNumNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumNote.Properties.Appearance.Font");
		this.TxtNumNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNumNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumNote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumNote_KeyPress);
		resources.ApplyResources(this.label18, "label18");
		this.label18.Name = "label18";
		resources.ApplyResources(this.BtnOrderSearch, "BtnOrderSearch");
		this.BtnOrderSearch.Name = "BtnOrderSearch";
		this.BtnOrderSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnOrderSearch.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnOrderSearch.Properties.Appearance.Font");
		this.BtnOrderSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnOrderSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnOrderSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnOrderSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnOrderSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions6");
		this.BtnOrderSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnOrderSearch.Properties.Buttons"), resources.GetString("BtnOrderSearch.Properties.Buttons1"), (int)resources.GetObject("BtnOrderSearch.Properties.Buttons2"), (bool)resources.GetObject("BtnOrderSearch.Properties.Buttons3"), (bool)resources.GetObject("BtnOrderSearch.Properties.Buttons4"), (bool)resources.GetObject("BtnOrderSearch.Properties.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("BtnOrderSearch.Properties.Buttons6"), resources.GetObject("BtnOrderSearch.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("BtnOrderSearch.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("BtnOrderSearch.Properties.Buttons9"))
		});
		this.BtnOrderSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnOrderSearch.Properties.NullText = resources.GetString("BtnOrderSearch.Properties.NullText");
		this.BtnOrderSearch.Tag = "بحث بالكود";
		this.BtnOrderSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnOrderSearch_ButtonClick);
		this.BtnOrderSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnOrderSearch_KeyDown);
		this.BtnOrderSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BtnCustomerSearch_KeyPress);
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.LblCustLast, "LblCustLast");
		this.LblCustLast.Name = "LblCustLast";
		resources.ApplyResources(this.label27, "label27");
		this.label27.Name = "label27";
		resources.ApplyResources(this.DateHold, "DateHold");
		this.DateHold.Name = "DateHold";
		this.DateHold.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.DateHold.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("DateHold.Properties.Appearance.Font");
		this.DateHold.Properties.Appearance.Options.UseFont = true;
		this.DateHold.Properties.Appearance.Options.UseTextOptions = true;
		this.DateHold.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.DateHold.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("DateHold.Properties.Buttons"))
		});
		this.DateHold.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.DateHold.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("DateHold.Properties.CalendarTimeProperties.Buttons"))
		});
		this.DateHold.Properties.DisplayFormat.FormatString = "";
		this.DateHold.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.DateHold.Properties.EditFormat.FormatString = "";
		this.DateHold.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.DateHold.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("DateHold.Properties.Mask.UseMaskAsDisplayFormat");
		this.DateHold.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.DateHold.EditValueChanged += new System.EventHandler(DateHold_EditValueChanged);
		resources.ApplyResources(this.label28, "label28");
		this.label28.Name = "label28";
		resources.ApplyResources(this.label29, "label29");
		this.label29.Name = "label29";
		resources.ApplyResources(this.label30, "label30");
		this.label30.Name = "label30";
		resources.ApplyResources(this.BtnTypeInvoic, "BtnTypeInvoic");
		this.BtnTypeInvoic.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnTypeInvoic.Appearance.Font");
		this.BtnTypeInvoic.Appearance.Options.UseFont = true;
		this.BtnTypeInvoic.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnTypeInvoic.ImageOptions.Image");
		this.BtnTypeInvoic.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnTypeInvoic.Name = "BtnTypeInvoic";
		this.BtnTypeInvoic.Click += new System.EventHandler(BtnTypeInvoic_Click);
		resources.ApplyResources(this.BtnRecipt, "BtnRecipt");
		this.BtnRecipt.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnRecipt.Appearance.Font");
		this.BtnRecipt.Appearance.Options.UseFont = true;
		this.BtnRecipt.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRecipt.ImageOptions.Image");
		this.BtnRecipt.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnRecipt.Name = "BtnRecipt";
		this.BtnRecipt.Click += new System.EventHandler(BtnRecipt_Click);
		resources.ApplyResources(this.groupBox4, "groupBox4");
		this.groupBox4.Controls.Add(this.LValid);
		this.groupBox4.Controls.Add(this.BtnTypeInvoic);
		this.groupBox4.Controls.Add(this.LSucced);
		this.groupBox4.Controls.Add(this.BtnUpload);
		this.groupBox4.Controls.Add(this.BtnCustomerSearch);
		this.groupBox4.Controls.Add(this.BtnTailor);
		this.groupBox4.Controls.Add(this.BtnNewCustomer);
		this.groupBox4.Controls.Add(this.label22);
		this.groupBox4.Controls.Add(this.label35);
		this.groupBox4.Controls.Add(this.label19);
		this.groupBox4.Controls.Add(this.label33);
		this.groupBox4.Controls.Add(this.label42);
		this.groupBox4.Controls.Add(this.label43);
		this.groupBox4.Controls.Add(this.TxtAdditionalNum);
		this.groupBox4.Controls.Add(this.TxtCommercialRecord);
		this.groupBox4.Controls.Add(this.TxtPostalCode);
		this.groupBox4.Controls.Add(this.TxtCountry);
		this.groupBox4.Controls.Add(this.TxtCity);
		this.groupBox4.Controls.Add(this.label6);
		this.groupBox4.Controls.Add(this.label37);
		this.groupBox4.Controls.Add(this.label10);
		this.groupBox4.Controls.Add(this.label46);
		this.groupBox4.Controls.Add(this.TxtPhone);
		this.groupBox4.Controls.Add(this.TxtCharge);
		this.groupBox4.Controls.Add(this.TxtSales);
		this.groupBox4.Controls.Add(this.TxtSuppliers);
		this.groupBox4.Controls.Add(this.TxtNumVat);
		this.groupBox4.Controls.Add(this.label13);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.TabStop = false;
		resources.ApplyResources(this.LValid, "LValid");
		this.LValid.BackColor = System.Drawing.Color.Green;
		this.LValid.ForeColor = System.Drawing.Color.White;
		this.LValid.Name = "LValid";
		resources.ApplyResources(this.LSucced, "LSucced");
		this.LSucced.BackColor = System.Drawing.Color.Green;
		this.LSucced.ForeColor = System.Drawing.Color.White;
		this.LSucced.Name = "LSucced";
		resources.ApplyResources(this.BtnUpload, "BtnUpload");
		this.BtnUpload.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnUpload.Appearance.Font");
		this.BtnUpload.Appearance.Options.UseFont = true;
		this.BtnUpload.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUpload.ImageOptions.Image");
		this.BtnUpload.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUpload.Name = "BtnUpload";
		this.BtnUpload.Click += new System.EventHandler(BtnUpload_Click);
		resources.ApplyResources(this.BtnCustomerSearch, "BtnCustomerSearch");
		this.BtnCustomerSearch.Name = "BtnCustomerSearch";
		this.BtnCustomerSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnCustomerSearch.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCustomerSearch.Properties.Appearance.Font");
		this.BtnCustomerSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnCustomerSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnCustomerSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnCustomerSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnCustomerSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCustomerSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnCustomerSearch.Properties.Buttons"))
		});
		this.BtnCustomerSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCustomerSearch.Properties.NullText = resources.GetString("BtnCustomerSearch.Properties.NullText");
		this.BtnCustomerSearch.Tag = "بحث بالكود";
		this.BtnCustomerSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnCustomerSearch_ButtonClick);
		this.BtnCustomerSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnCustomerSearch_KeyDown);
		resources.ApplyResources(this.BtnTailor, "BtnTailor");
		this.BtnTailor.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnTailor.Appearance.Font");
		this.BtnTailor.Appearance.Options.UseFont = true;
		this.BtnTailor.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnTailor.ImageOptions.Image");
		this.BtnTailor.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnTailor.Name = "BtnTailor";
		this.BtnTailor.Click += new System.EventHandler(BtnTailor_Click);
		resources.ApplyResources(this.BtnNewCustomer, "BtnNewCustomer");
		this.BtnNewCustomer.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNewCustomer.Appearance.Font");
		this.BtnNewCustomer.Appearance.Options.UseFont = true;
		this.BtnNewCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewCustomer.ImageOptions.Image");
		this.BtnNewCustomer.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewCustomer.Name = "BtnNewCustomer";
		this.BtnNewCustomer.Click += new System.EventHandler(BtnNewCustomer_Click);
		resources.ApplyResources(this.label22, "label22");
		this.label22.Name = "label22";
		resources.ApplyResources(this.label35, "label35");
		this.label35.Name = "label35";
		resources.ApplyResources(this.label19, "label19");
		this.label19.Name = "label19";
		resources.ApplyResources(this.label33, "label33");
		this.label33.Name = "label33";
		resources.ApplyResources(this.label42, "label42");
		this.label42.Name = "label42";
		resources.ApplyResources(this.label43, "label43");
		this.label43.Name = "label43";
		resources.ApplyResources(this.TxtAdditionalNum, "TxtAdditionalNum");
		this.TxtAdditionalNum.Name = "TxtAdditionalNum";
		this.TxtAdditionalNum.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAdditionalNum.Properties.Appearance.Font");
		this.TxtAdditionalNum.Properties.Appearance.Options.UseFont = true;
		this.TxtAdditionalNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAdditionalNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtCommercialRecord, "TxtCommercialRecord");
		this.TxtCommercialRecord.Name = "TxtCommercialRecord";
		this.TxtCommercialRecord.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCommercialRecord.Properties.Appearance.Font");
		this.TxtCommercialRecord.Properties.Appearance.Options.UseFont = true;
		this.TxtCommercialRecord.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCommercialRecord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCommercialRecord.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtCommercialRecord_KeyDown);
		this.TxtCommercialRecord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtCommercialRecord_KeyPress);
		this.TxtCommercialRecord.MouseDown += new System.Windows.Forms.MouseEventHandler(TxtCommercialRecord_MouseDown);
		resources.ApplyResources(this.TxtPostalCode, "TxtPostalCode");
		this.TxtPostalCode.Name = "TxtPostalCode";
		this.TxtPostalCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPostalCode.Properties.Appearance.Font");
		this.TxtPostalCode.Properties.Appearance.Options.UseFont = true;
		this.TxtPostalCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPostalCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPostalCode.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtPostalCode_KeyDown);
		this.TxtPostalCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPostalCode_KeyPress);
		this.TxtPostalCode.MouseDown += new System.Windows.Forms.MouseEventHandler(TxtPostalCode_MouseDown);
		resources.ApplyResources(this.TxtCountry, "TxtCountry");
		this.TxtCountry.Name = "TxtCountry";
		this.TxtCountry.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCountry.Properties.Appearance.Font");
		this.TxtCountry.Properties.Appearance.Options.UseFont = true;
		this.TxtCountry.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCountry.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtCity, "TxtCity");
		this.TxtCity.Name = "TxtCity";
		this.TxtCity.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCity.Properties.Appearance.Font");
		this.TxtCity.Properties.Appearance.Options.UseFont = true;
		this.TxtCity.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.label37, "label37");
		this.label37.Name = "label37";
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.label46, "label46");
		this.label46.Name = "label46";
		resources.ApplyResources(this.TxtPhone, "TxtPhone");
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPhone.Properties.Appearance.Font");
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtPhone_KeyDown);
		this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPhone_KeyPress);
		resources.ApplyResources(this.TxtCharge, "TxtCharge");
		this.TxtCharge.Name = "TxtCharge";
		this.TxtCharge.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCharge.Properties.Appearance.Font");
		this.TxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtSales, "TxtSales");
		this.TxtSales.Name = "TxtSales";
		this.TxtSales.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtSales.Properties.Appearance.Font");
		this.TxtSales.Properties.Appearance.Options.UseFont = true;
		this.TxtSales.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSales.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtSuppliers, "TxtSuppliers");
		this.TxtSuppliers.Name = "TxtSuppliers";
		this.TxtSuppliers.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtSuppliers.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtSuppliers.Properties.Appearance.Font");
		this.TxtSuppliers.Properties.Appearance.Options.UseBackColor = true;
		this.TxtSuppliers.Properties.Appearance.Options.UseFont = true;
		this.TxtSuppliers.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSuppliers.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSuppliers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtSuppliers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCust_ButtonClick);
		this.TxtSuppliers.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtSuppliers.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtSuppliers_KeyDown);
		resources.ApplyResources(this.TxtNumVat, "TxtNumVat");
		this.TxtNumVat.Name = "TxtNumVat";
		this.TxtNumVat.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumVat.Properties.Appearance.Font");
		this.TxtNumVat.Properties.Appearance.Options.UseFont = true;
		this.TxtNumVat.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumVat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumVat.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNumVat_KeyDown);
		this.TxtNumVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumVat_KeyPress);
		resources.ApplyResources(this.label13, "label13");
		this.label13.Name = "label13";
		resources.ApplyResources(this.label34, "label34");
		this.label34.Name = "label34";
		resources.ApplyResources(this.label38, "label38");
		this.label38.Name = "label38";
		resources.ApplyResources(this.TxtStreet, "TxtStreet");
		this.TxtStreet.Name = "TxtStreet";
		this.TxtStreet.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtStreet.Properties.Appearance.Font");
		this.TxtStreet.Properties.Appearance.Options.UseFont = true;
		this.TxtStreet.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtStreet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtDistrict, "TxtDistrict");
		this.TxtDistrict.Name = "TxtDistrict";
		this.TxtDistrict.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDistrict.Properties.Appearance.Font");
		this.TxtDistrict.Properties.Appearance.Options.UseFont = true;
		this.TxtDistrict.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDistrict.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtQuntity, "TxtQuntity");
		this.TxtQuntity.DecimalDigits = 3;
		this.TxtQuntity.ForeColor = System.Drawing.Color.FromArgb(0, 64, 64);
		this.TxtQuntity.Name = "TxtQuntity";
		this.TxtQuntity.ReadOnly = true;
		this.TxtQuntity.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtQuntity.Value = new decimal(new int[4]);
		this.TxtQuntity.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label16, "label16");
		this.label16.Name = "label16";
		resources.ApplyResources(this.LblNowQuintity, "LblNowQuintity");
		this.LblNowQuintity.Name = "LblNowQuintity";
		resources.ApplyResources(this.TxtAllQuantity, "TxtAllQuantity");
		this.TxtAllQuantity.DecimalDigits = 3;
		this.TxtAllQuantity.ForeColor = System.Drawing.Color.FromArgb(0, 64, 64);
		this.TxtAllQuantity.Name = "TxtAllQuantity";
		this.TxtAllQuantity.ReadOnly = true;
		this.TxtAllQuantity.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllQuantity.UseWaitCursor = true;
		this.TxtAllQuantity.Value = new decimal(new int[4]);
		this.TxtAllQuantity.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.BtnCheckOut, "BtnCheckOut");
		this.BtnCheckOut.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCheckOut.Appearance.Font");
		this.BtnCheckOut.Appearance.Options.UseFont = true;
		this.BtnCheckOut.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCheckOut.ImageOptions.Image");
		this.BtnCheckOut.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCheckOut.Name = "BtnCheckOut";
		this.BtnCheckOut.Click += new System.EventHandler(BtnCheckOut_Click);
		resources.ApplyResources(this.BtnMinNet, "BtnMinNet");
		this.BtnMinNet.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnMinNet.Appearance.Font");
		this.BtnMinNet.Appearance.Options.UseFont = true;
		this.BtnMinNet.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnMinNet.ImageOptions.Image");
		this.BtnMinNet.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnMinNet.Name = "BtnMinNet";
		this.BtnMinNet.Click += new System.EventHandler(BtnMinNet_Click);
		resources.ApplyResources(this.BtnPlusNet, "BtnPlusNet");
		this.BtnPlusNet.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPlusNet.Appearance.Font");
		this.BtnPlusNet.Appearance.Options.UseFont = true;
		this.BtnPlusNet.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPlusNet.ImageOptions.Image");
		this.BtnPlusNet.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPlusNet.Name = "BtnPlusNet";
		this.BtnPlusNet.Click += new System.EventHandler(BtnPlusNet_Click);
		this.tileGroup1.Name = "tileGroup1";
		resources.ApplyResources(this.tileGroup1, "tileGroup1");
		this.tileGroup4.Name = "tileGroup4";
		resources.ApplyResources(this.tileGroup4, "tileGroup4");
		this.tileGroup5.Name = "tileGroup5";
		resources.ApplyResources(this.tileGroup5, "tileGroup5");
		this.tileGroup6.Name = "tileGroup6";
		resources.ApplyResources(this.tileGroup6, "tileGroup6");
		this.tileGroup7.Name = "tileGroup7";
		resources.ApplyResources(this.tileGroup7, "tileGroup7");
		resources.ApplyResources(this.BtnDisCode, "BtnDisCode");
		this.BtnDisCode.Name = "BtnDisCode";
		this.BtnDisCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnDisCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDisCode.Properties.Appearance.Font");
		this.BtnDisCode.Properties.Appearance.Options.UseBackColor = true;
		this.BtnDisCode.Properties.Appearance.Options.UseFont = true;
		this.BtnDisCode.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnDisCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnDisCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnDisCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnDisCode.Properties.Buttons"))
		});
		this.BtnDisCode.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnDisCode.Properties.NullText = resources.GetString("BtnDisCode.Properties.NullText");
		this.BtnDisCode.Tag = "";
		this.BtnDisCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnDisCode_ButtonClick_1);
		this.BtnDisCode.KeyUp += new System.Windows.Forms.KeyEventHandler(BtnDisCode_KeyUp_1);
		resources.ApplyResources(this.GropGlasses, "GropGlasses");
		this.GropGlasses.Controls.Add(this.label4);
		this.GropGlasses.Controls.Add(this.lNote);
		this.GropGlasses.Controls.Add(this.TxtNoteGlass);
		this.GropGlasses.Controls.Add(this.BtnCustomerSearch2);
		this.GropGlasses.Controls.Add(this.BtnNewCustomer2);
		this.GropGlasses.Controls.Add(this.label26);
		this.GropGlasses.Controls.Add(this.TxtSuppliers2);
		this.GropGlasses.Controls.Add(this.label44);
		this.GropGlasses.Controls.Add(this._TxtPhone);
		this.GropGlasses.Controls.Add(this._TxtVatNum);
		this.GropGlasses.Controls.Add(this.label21);
		this.GropGlasses.Controls.Add(this.label32);
		this.GropGlasses.Controls.Add(this.label51);
		this.GropGlasses.Controls.Add(this.label50);
		this.GropGlasses.Controls.Add(this.label49);
		this.GropGlasses.Controls.Add(this.label47);
		this.GropGlasses.Controls.Add(this.label45);
		this.GropGlasses.Controls.Add(this.TxtLeftAdd);
		this.GropGlasses.Controls.Add(this.TxtRightSph);
		this.GropGlasses.Controls.Add(this.TxtRightCYL);
		this.GropGlasses.Controls.Add(this.TxtLeftAxis);
		this.GropGlasses.Controls.Add(this.TxtRightAxis);
		this.GropGlasses.Controls.Add(this.TxtRightAdd);
		this.GropGlasses.Controls.Add(this.TxtLeftCYL);
		this.GropGlasses.Controls.Add(this.TxtLeftSph);
		this.GropGlasses.Name = "GropGlasses";
		this.GropGlasses.TabStop = false;
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.lNote, "lNote");
		this.lNote.Name = "lNote";
		resources.ApplyResources(this.TxtNoteGlass, "TxtNoteGlass");
		this.TxtNoteGlass.Name = "TxtNoteGlass";
		this.TxtNoteGlass.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNoteGlass.Properties.Appearance.Font");
		this.TxtNoteGlass.Properties.Appearance.Options.UseFont = true;
		this.TxtNoteGlass.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNoteGlass.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.BtnCustomerSearch2, "BtnCustomerSearch2");
		this.BtnCustomerSearch2.Name = "BtnCustomerSearch2";
		this.BtnCustomerSearch2.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnCustomerSearch2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCustomerSearch2.Properties.Appearance.Font");
		this.BtnCustomerSearch2.Properties.Appearance.Options.UseBackColor = true;
		this.BtnCustomerSearch2.Properties.Appearance.Options.UseFont = true;
		this.BtnCustomerSearch2.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnCustomerSearch2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnCustomerSearch2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCustomerSearch2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnCustomerSearch2.Properties.Buttons"))
		});
		this.BtnCustomerSearch2.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCustomerSearch2.Properties.NullText = resources.GetString("BtnCustomerSearch2.Properties.NullText");
		this.BtnCustomerSearch2.Tag = "بحث بالكود";
		this.BtnCustomerSearch2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnCustomerSearch2_ButtonClick);
		this.BtnCustomerSearch2.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnCustomerSearch2_KeyDown);
		resources.ApplyResources(this.BtnNewCustomer2, "BtnNewCustomer2");
		this.BtnNewCustomer2.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNewCustomer2.Appearance.Font");
		this.BtnNewCustomer2.Appearance.Options.UseFont = true;
		this.BtnNewCustomer2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewCustomer2.ImageOptions.Image");
		this.BtnNewCustomer2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewCustomer2.Name = "BtnNewCustomer2";
		this.BtnNewCustomer2.Click += new System.EventHandler(BtnNewCustomer2_Click);
		resources.ApplyResources(this.label26, "label26");
		this.label26.Name = "label26";
		resources.ApplyResources(this.TxtSuppliers2, "TxtSuppliers2");
		this.TxtSuppliers2.Name = "TxtSuppliers2";
		this.TxtSuppliers2.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtSuppliers2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtSuppliers2.Properties.Appearance.Font");
		this.TxtSuppliers2.Properties.Appearance.Options.UseBackColor = true;
		this.TxtSuppliers2.Properties.Appearance.Options.UseFont = true;
		this.TxtSuppliers2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSuppliers2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSuppliers2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtSuppliers2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtSuppliers2_ButtonClick);
		this.TxtSuppliers2.Enter += new System.EventHandler(TxtSuppliers2_Enter);
		this.TxtSuppliers2.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtSuppliers2_KeyDown);
		resources.ApplyResources(this.label44, "label44");
		this.label44.Name = "label44";
		resources.ApplyResources(this._TxtPhone, "_TxtPhone");
		this._TxtPhone.Name = "_TxtPhone";
		this._TxtPhone.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("_TxtPhone.Properties.Appearance.Font");
		this._TxtPhone.Properties.Appearance.Options.UseFont = true;
		this._TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this._TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this._TxtVatNum, "_TxtVatNum");
		this._TxtVatNum.Name = "_TxtVatNum";
		this._TxtVatNum.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("_TxtVatNum.Properties.Appearance.Font");
		this._TxtVatNum.Properties.Appearance.Options.UseFont = true;
		this._TxtVatNum.Properties.Appearance.Options.UseTextOptions = true;
		this._TxtVatNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.label32, "label32");
		this.label32.Name = "label32";
		resources.ApplyResources(this.label51, "label51");
		this.label51.Name = "label51";
		resources.ApplyResources(this.label50, "label50");
		this.label50.Name = "label50";
		resources.ApplyResources(this.label49, "label49");
		this.label49.Name = "label49";
		resources.ApplyResources(this.label47, "label47");
		this.label47.Name = "label47";
		resources.ApplyResources(this.label45, "label45");
		this.label45.Name = "label45";
		resources.ApplyResources(this.TxtLeftAdd, "TxtLeftAdd");
		this.TxtLeftAdd.Name = "TxtLeftAdd";
		this.TxtLeftAdd.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtLeftAdd.Properties.Appearance.Font");
		this.TxtLeftAdd.Properties.Appearance.Options.UseFont = true;
		this.TxtLeftAdd.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLeftAdd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtRightSph, "TxtRightSph");
		this.TxtRightSph.Name = "TxtRightSph";
		this.TxtRightSph.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtRightSph.Properties.Appearance.Font");
		this.TxtRightSph.Properties.Appearance.Options.UseFont = true;
		this.TxtRightSph.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtRightSph.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtRightCYL, "TxtRightCYL");
		this.TxtRightCYL.Name = "TxtRightCYL";
		this.TxtRightCYL.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtRightCYL.Properties.Appearance.Font");
		this.TxtRightCYL.Properties.Appearance.Options.UseFont = true;
		this.TxtRightCYL.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtRightCYL.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtLeftAxis, "TxtLeftAxis");
		this.TxtLeftAxis.Name = "TxtLeftAxis";
		this.TxtLeftAxis.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtLeftAxis.Properties.Appearance.Font");
		this.TxtLeftAxis.Properties.Appearance.Options.UseFont = true;
		this.TxtLeftAxis.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLeftAxis.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtRightAxis, "TxtRightAxis");
		this.TxtRightAxis.Name = "TxtRightAxis";
		this.TxtRightAxis.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtRightAxis.Properties.Appearance.Font");
		this.TxtRightAxis.Properties.Appearance.Options.UseFont = true;
		this.TxtRightAxis.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtRightAxis.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtRightAdd, "TxtRightAdd");
		this.TxtRightAdd.Name = "TxtRightAdd";
		this.TxtRightAdd.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtRightAdd.Properties.Appearance.Font");
		this.TxtRightAdd.Properties.Appearance.Options.UseFont = true;
		this.TxtRightAdd.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtRightAdd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtLeftCYL, "TxtLeftCYL");
		this.TxtLeftCYL.Name = "TxtLeftCYL";
		this.TxtLeftCYL.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtLeftCYL.Properties.Appearance.Font");
		this.TxtLeftCYL.Properties.Appearance.Options.UseFont = true;
		this.TxtLeftCYL.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLeftCYL.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtLeftSph, "TxtLeftSph");
		this.TxtLeftSph.Name = "TxtLeftSph";
		this.TxtLeftSph.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtLeftSph.Properties.Appearance.Font");
		this.TxtLeftSph.Properties.Appearance.Options.UseFont = true;
		this.TxtLeftSph.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLeftSph.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.label39, "label39");
		this.label39.Name = "label39";
		resources.ApplyResources(this.label40, "label40");
		this.label40.Name = "label40";
		resources.ApplyResources(this.TxtCharge2, "TxtCharge2");
		this.TxtCharge2.Name = "TxtCharge2";
		this.TxtCharge2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCharge2.Properties.Appearance.Font");
		this.TxtCharge2.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge2.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge2.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtSales2, "TxtSales2");
		this.TxtSales2.Name = "TxtSales2";
		this.TxtSales2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtSales2.Properties.Appearance.Font");
		this.TxtSales2.Properties.Appearance.Options.UseFont = true;
		this.TxtSales2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSales2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.BtnOpenCashir, "BtnOpenCashir");
		this.BtnOpenCashir.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnOpenCashir.Appearance.Font");
		this.BtnOpenCashir.Appearance.Options.UseFont = true;
		this.BtnOpenCashir.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOpenCashir.ImageOptions.Image");
		this.BtnOpenCashir.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnOpenCashir.Name = "BtnOpenCashir";
		this.BtnOpenCashir.Click += new System.EventHandler(BtnOpenCashir_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.BtnOpenCashir);
		base.Controls.Add(this.GropGlasses);
		base.Controls.Add(this.BtnDisCode);
		base.Controls.Add(this.TxtQuntity);
		base.Controls.Add(this.label34);
		base.Controls.Add(this.label16);
		base.Controls.Add(this.label38);
		base.Controls.Add(this.LblNowQuintity);
		base.Controls.Add(this.TxtAllQuantity);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.BtnCheckOut);
		base.Controls.Add(this.BtnRecipt);
		base.Controls.Add(this.TxtTotals);
		base.Controls.Add(this.TxtTotalDiscount);
		base.Controls.Add(this.TxtStreet);
		base.Controls.Add(this.TxtAllDiscount);
		base.Controls.Add(this.TxtDistrict);
		base.Controls.Add(this.TxtDiscount);
		base.Controls.Add(this.txtdicoutprege);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label39);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.label40);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.TxtVat);
		base.Controls.Add(this.TxtNet);
		base.Controls.Add(this.TxtCharge2);
		base.Controls.Add(this.TxtSales2);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.gridControl1);
		base.Controls.Add(this.BtnReturnCashier);
		base.Controls.Add(this.TxtCost);
		base.Controls.Add(this.tileControl2);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.tileControl1);
		base.Controls.Add(this.TxtTabocc);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.TxtVatDisc);
		base.Controls.Add(this.TxtBarcode);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnPrint);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnPlusNet);
		base.Controls.Add(this.BtnMinNet);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnDelete);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmCashir";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmCashir_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4Units).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTable).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleSectoral.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleCashierType.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRoom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleHold.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.HoldOpenClose.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAdditionalNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCountry.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtStreet.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDistrict.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnDisCode.Properties).EndInit();
		this.GropGlasses.ResumeLayout(false);
		this.GropGlasses.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNoteGlass.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this._TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this._TxtVatNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLeftAdd.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRightSph.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRightCYL.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLeftAxis.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRightAxis.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRightAdd.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLeftCYL.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLeftSph.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales2.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
