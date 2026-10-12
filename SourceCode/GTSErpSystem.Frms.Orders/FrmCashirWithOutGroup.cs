using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
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
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmCachMoney;
using GTSErpSystem.Frms.Orders.FrmLuckUpOrders;
using GTSErpSystem.Frms.Orders.FrmOpenDay;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Helper;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Orders;

public class FrmCashirWithOutGroup : XtraForm
{
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

	public decimal Cash;

	public decimal Bank;

	public decimal MoneyNeed;

	public decimal MoneyPaying;

	public decimal MoneyNet;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	private bool IsClosingOk = true;

	public decimal FrmCash;

	public decimal FrmBank;

	public decimal Safy;

	private decimal? CreditLimit = 0m;

	private int ItmID = 0;

	private static int Code = 0;

	private string CurrentItemId = "0";

	private bool OpenClose = false;

	private int NumberDisc = 0;

	private decimal DiscountPercent = 0m;

	private decimal TotalHasm = 0m;

	private string fileNameAR = Application.StartupPath + "\\Layout\\CashirAR.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\CashirEN.xml";

	private decimal ChargeAfterSale = 0m;

	private IContainer components = null;

	private NumericEditBox TxtNet22;

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

	private NumericEditBox TxtTotalAll;

	private Label label2;

	private Label label3;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtTotals;

	private Label label5;

	private SimpleButton BtnMinus;

	private SimpleButton BtnPlus;

	private ButtonEdit TxtBarcode;

	private NumericEditBox TxtNet;

	private Label label7;

	private NumericEditBox TxtTotalBeforDisc;

	private NumericEditBox TxtVatBeforeDisc;

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

	private NumericEditBox TxtCost;

	private Label lblRoom;

	private Label lblTable;

	private NumericUpDown TxtTable;

	private NumericUpDown TxtRoom;

	private ToggleSwitch toggleCashierType;

	private SimpleButton BtnReturnCashier;

	private NumericEditBox TxtAllVat;

	private Label label12;

	private NumericEditBox TxtTabocc;

	private Label label11;

	private NumericEditBox TxtAllTotal;

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

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn24;

	private GridColumn gridColumn23;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn gridColumn26;

	private GridColumn shearch;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4Units;

	private GridColumn gridColumn19;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private GridColumn gridColumn25;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn22;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private GridColumn gridColumn5;

	private GridColumn gridColumn8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private GridColumn gridColumn9;

	private GridColumn gridColumn27;

	private RepositoryItemTextEdit repositoryItemTextEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit2;

	private RepositoryItemDateEdit repositoryItemDateEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit3;

	private RepositoryItemPictureEdit repositoryItemPictureEdit1;

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

	private GridColumn gridColumn32;

	private GridColumn gridColumn33;

	private NumericEditBox TxtDiscountALLTotal;

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

	private RepositoryItemButtonEdit repositoryItemButtonEditStore;

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

	private GridColumn gridColumn36;

	private ToggleSwitch HoldOpenClose;

	private ToolStripMenuItem صرفالحجزToolStripMenuItem;

	private SimpleButton BtnCheckOut;

	private GridColumn gridColumn37;

	private SimpleButton BtnMinNet;

	private SimpleButton BtnPlusNet;

	public FrmCashirWithOutGroup()
	{
		InitializeComponent();
		if (LoginDetails.Lang == "ar-EG")
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
		}
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		TxtDigital();
		SetInitialRow();
		SetInitialRowPrinter();
		BindComboBox();
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		SetDateCasheir();
		IsRestaurantActive();
		IEnumerable<Item_Groups> enumerable = from x in gTSdbEntities.Item_Groups.ToList()
			where x.IsShow == true
			select x;
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		string groupColor = LoginDetails.GroupColor;
		string groupFontColor = LoginDetails.GroupFontColor;
		TxtSuppliers.Text = "عميل نقدي";
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtBarcode.Focus();
		toggleSectoral.IsOn = true;
		if (LoginDetails.IsSecureCashier && !gTSdbEntities.User_Login.Select((User_Login x) => x.IsOpenDay == (bool?)true).FirstOrDefault())
		{
			base.Enabled = false;
			GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay frmOpenDay = new GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay();
			frmOpenDay.ShowDialog();
			if (!frmOpenDay.ISOpen)
			{
				return;
			}
			base.Enabled = true;
		}
		DateHold.DateTime = DateTime.Now;
		BtnMinNet.Visible = LoginDetails.Gabr;
		BtnPlusNet.Visible = LoginDetails.Gabr;
		User_Login user_Login = gTSdbEntities.User_Login.Where((User_Login x) => x.ID == LoginDetails.UserID).FirstOrDefault();
		if (user_Login != null)
		{
			OpenClose = true;
			HoldOpenClose.IsOn = user_Login.IsOpenDay.Value;
			OpenClose = false;
		}
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
			BtnReturnCashier.Visible = false;
			BtnCheckOut.Visible = false;
			BtnRecipt.Visible = false;
		}
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "كود_الصنف"));
		string text2 = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "أسم الصنف"));
		decimal num = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "الكمية"));
		decimal num2 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "سعر الوحدة"));
		if (text == "" || text2 == "" || num <= 0m || num2 <= 0m)
		{
			Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(text).FirstOrDefault();
			if (get_Item_ByCodeALL_Result != null)
			{
				e.Valid = false;
				if (text == "")
				{
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[0];
					gridView1.ShowEditor();
				}
				else if (text2 == "")
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
				else if (num2 <= 0m)
				{
					MessageBox.Show("برجاء إدخال سعر بيع أكبر من الصفر");
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[4];
					gridView1.ShowEditor();
				}
			}
		}
		Cal_FinalResultTotals();
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
		if (e.Column.FieldName == "الكمية")
		{
			string value = Convert.ToString(e.Value);
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("م");
			if (focusedRowCellDisplayText != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(focusedRowCellDisplayText).FirstOrDefault();
				if (get_Item_ByCodeALL_Result != null)
				{
					foreach (DataRow row in datatable.Rows)
					{
						if (row["م"].ToString() == focusedRowCellDisplayText4)
						{
							row["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitTafqit;
							row["كمية التفقيط"] = "1";
							decimal num = Convert.ToDecimal(row["سعر الوحدة"].ToString());
							if (row["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]), DecimalNumQty);
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(num * Convert.ToDecimal(value)), DecimalNum);
								Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
								break;
							}
							if (row["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								decimal num2 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertMediumUnit);
								row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * (Convert.ToDecimal(row["الكمية"]) * num2), DecimalNumQty);
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(num * Convert.ToDecimal(value)), DecimalNum);
								Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
								break;
							}
							if (row["أسم_الوحدة"].ToString() == "الكبرى")
							{
								decimal num3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertBigUnit);
								row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * (Convert.ToDecimal(row["الكمية"]) * num3), DecimalNumQty);
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(num * Convert.ToDecimal(value)), DecimalNum);
								Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
								break;
							}
						}
					}
					insertnewrow();
					CheckFfistRow();
					GetTotalCost();
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[3];
					gridView1.ShowEditor();
				}
			}
		}
		else if (e.Column.FieldName == "سعر الوحدة")
		{
			string value2 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText5 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText6 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText7 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText8 = gridView1.GetFocusedRowCellDisplayText("الكمية");
			string focusedRowCellDisplayText9 = gridView1.GetFocusedRowCellDisplayText("م");
			if (focusedRowCellDisplayText5 != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result2 = ITM.GetItemByCode(focusedRowCellDisplayText5).FirstOrDefault();
				if (get_Item_ByCodeALL_Result2 != null)
				{
					foreach (DataRow row2 in datatable.Rows)
					{
						if (!(row2["م"].ToString() == focusedRowCellDisplayText9))
						{
							continue;
						}
						if (row2["أسم_الوحدة"].ToString() == "الصغرى")
						{
							decimal sellPermission = ORD.GetSellPermission(focusedRowCellDisplayText5, UserSellPrice, "الصغرى");
							if (UserSellPrice == 1)
							{
								if (Convert.ToDecimal(value2) < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 2)
							{
								if (Convert.ToDecimal(value2) < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 3)
							{
								if (Convert.ToDecimal(value2) < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 4 && Convert.ToDecimal(value2) < sellPermission)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row2["سعر الوحدة"] = sellPermission;
								return;
							}
							row2["سعر الوحدة"] = value2;
							row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value2) * Convert.ToDecimal(focusedRowCellDisplayText8)), DecimalNum);
							Vat(get_Item_ByCodeALL_Result2.Is_Tax, get_Item_ByCodeALL_Result2.Tax_Value, Convert.ToDecimal(row2["سعر الوحدة"]), Convert.ToDecimal(row2["الكمية"]), row2, "سعر الوحدة");
							break;
						}
						if (row2["أسم_الوحدة"].ToString() == "المتوسطة")
						{
							decimal sellPermission2 = ORD.GetSellPermission(focusedRowCellDisplayText5, UserSellPrice, "المتوسطة");
							if (UserSellPrice == 1)
							{
								if (Convert.ToDecimal(value2) < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 2)
							{
								if (Convert.ToDecimal(value2) < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 3)
							{
								if (Convert.ToDecimal(value2) < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 4 && Convert.ToDecimal(value2) < sellPermission2)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row2["سعر الوحدة"] = sellPermission2;
								return;
							}
							row2["سعر الوحدة"] = value2;
							row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value2) * Convert.ToDecimal(focusedRowCellDisplayText8)), DecimalNum);
							Vat(get_Item_ByCodeALL_Result2.Is_Tax, get_Item_ByCodeALL_Result2.Tax_Value, Convert.ToDecimal(row2["سعر الوحدة"]), Convert.ToDecimal(row2["الكمية"]), row2, "سعر الوحدة");
							break;
						}
						if (!(row2["أسم_الوحدة"].ToString() == "الكبرى"))
						{
							continue;
						}
						decimal sellPermission3 = ORD.GetSellPermission(focusedRowCellDisplayText5, UserSellPrice, "الكبرى");
						if (UserSellPrice == 1)
						{
							if (Convert.ToDecimal(value2) < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row2["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 2)
						{
							if (Convert.ToDecimal(value2) < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row2["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 3)
						{
							if (Convert.ToDecimal(value2) < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row2["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 4 && Convert.ToDecimal(value2) < sellPermission3)
						{
							MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							row2["سعر الوحدة"] = sellPermission3;
							return;
						}
						row2["سعر الوحدة"] = value2;
						row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value2) * Convert.ToDecimal(focusedRowCellDisplayText8)), DecimalNum);
						Vat(get_Item_ByCodeALL_Result2.Is_Tax, get_Item_ByCodeALL_Result2.Tax_Value, Convert.ToDecimal(row2["سعر الوحدة"]), Convert.ToDecimal(row2["الكمية"]), row2, "سعر الوحدة");
						break;
					}
					insertnewrow();
					CheckFfistRow();
					GetTotalCost();
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[3];
					gridView1.ShowEditor();
				}
			}
		}
		else if (e.Column.FieldName == "الطول" && e.Value.ToString() != "0")
		{
			ValueChangeDGV("الطول", e);
		}
		else if (e.Column.FieldName == "الإرتفاع" && e.Value.ToString() != "0")
		{
			ValueChangeDGV("الإرتفاع", e);
		}
		else if (e.Column.FieldName == "العرض" && e.Value.ToString() != "0")
		{
			ValueChangeDGV("العرض", e);
		}
		else if (e.Column.FieldName == "البونص")
		{
			ValueChangeDGV("البونص", e);
		}
		else if (e.Column.FieldName == "الوزن" && e.Value.ToString() != "0")
		{
			decimal num4 = Convert.ToDecimal(e.Value);
			string focusedRowCellDisplayText10 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText11 = gridView1.GetFocusedRowCellDisplayText("م");
			if (focusedRowCellDisplayText10 != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result3 = ITM.GetItemByCode(focusedRowCellDisplayText10).FirstOrDefault();
				if (get_Item_ByCodeALL_Result3 != null)
				{
					foreach (DataRow row3 in datatable.Rows)
					{
						if (row3["م"].ToString() == focusedRowCellDisplayText11)
						{
							row3["الوزن"] = num4;
						}
					}
					CheckFfistRow();
					insertnewrow();
					GetTotalCost();
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[2];
					gridView1.ShowEditor();
				}
			}
		}
		else if (e.Column.FieldName == "قيمة الخصم")
		{
			ValueChangeDGV("قيمة الخصم", e);
		}
		else
		{
			if (!(e.Column.FieldName == "نسبة الخصم"))
			{
				return;
			}
			ValueChangeDGV("نسبة الخصم", e);
		}
		ArrangeDatatable();
		Cal_FinalResultTotals();
	}

	private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
	{
		try
		{
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("ItemID");
			if (focusedRowCellDisplayText != "")
			{
				ItmID = Convert.ToInt32(gridView1.GetFocusedRowCellDisplayText("ItemID"));
			}
			CurrentItemId = gridView1.GetFocusedRowCellDisplayText("ItemID");
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string StoreName = gridView1.GetFocusedRowCellDisplayText("المخزن");
			int storeID = (from x in db.Account_Stores
				where x.Store_Name == StoreName
				select x.ID).FirstOrDefault();
			GetAllitems_alternatives(ItmID, storeID, focusedRowCellDisplayText3);
			CodeItm = focusedRowCellDisplayText2;
		}
		catch
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
		if (Itemocde != "")
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
						if (row["م"].ToString() == Convert.ToString(num))
						{
							row["المخزن"] = value;
							row["رقم المخزن"] = StoreID;
							break;
						}
					}
					gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[5];
					gridView1.ShowEditor();
					GetTotalCost();
					frmSearchTransfer.Close();
					return;
				}
				int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				foreach (DataRow row2 in datatable.Rows)
				{
					if (row2["م"].ToString() == Convert.ToString(num2))
					{
						row2["المخزن"] = Comp_Store.Text;
						row2["رقم المخزن"] = Comp_Store.SelectedValue;
					}
				}
				return;
			}
		}
		MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف المخازن", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}

	private void repositoryItemButtonEdit4Units_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "كود_الصنف"));
		if (text != "")
		{
			LoginDetails.ItemcodeForUnits = text;
			FrmSearchUnit frmSearchUnit = new FrmSearchUnit();
			frmSearchUnit.ShowDialog();
			if (frmSearchUnit.ItemCode != "")
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
							if (!(row["كود_الصنف"].ToString() == Convert.ToString(itemCode)))
							{
								continue;
							}
							row["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitTafqit;
							row["كمية التفقيط"] = "1";
							row["الوحدة"] = get_Item_ByCodeALL_Result.UnitSmall;
							row["أسم_الوحدة"] = "الصغرى";
							if (LoginDetails.Lang == "ar-EG")
							{
								if (BtnEdit.Text == "تعديل F6")
								{
									row["المخزن"] = Comp_Store.Text;
									row["رقم المخزن"] = Comp_Store.SelectedValue;
								}
							}
							else if (BtnEdit.Text == "Edit F6")
							{
								row["المخزن"] = Comp_Store.Text;
								row["رقم المخزن"] = Comp_Store.SelectedValue;
							}
							row["الكمية"] = FUNC_DecimalNumQty(1);
							row["سعر الوحدة"] = get_Item_ByCodeALL_Result.SellPriceSmall;
							row["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1)), DecimalNum);
							row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]), DecimalNumQty);
							if (get_Item_ByCodeALL_Result.Is_Tax == true)
							{
								row["الضربية"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1) * get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100), DecimalNum);
								row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1) * get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
							else
							{
								row["الضربية"] = 0.0;
								row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
						}
						GetTotalCost();
						string StoreName = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int num2 = (from x in db.Account_Stores
							where x.Store_Name == StoreName
							select x.ID).FirstOrDefault();
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "المتوسطة")
				{
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result2 = ITM.GetItemByCode(itemCode).FirstOrDefault();
					if (get_Item_ByCodeALL_Result2 != null)
					{
						int num3 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row2 in datatable.Rows)
						{
							if (!(row2["كود_الصنف"].ToString() == Convert.ToString(itemCode)))
							{
								continue;
							}
							row2["الوحدة"] = get_Item_ByCodeALL_Result2.UnitMedium;
							row2["أسم_الوحدة"] = "المتوسطة";
							row2["الكمية"] = FUNC_DecimalNumQty(1);
							row2["وحدة التفقيط"] = get_Item_ByCodeALL_Result2.UnitTafqit;
							row2["كمية التفقيط"] = "1";
							if (LoginDetails.Lang == "ar-EG")
							{
								if (BtnEdit.Text == "تعديل F6")
								{
									row2["المخزن"] = Comp_Store.Text;
									row2["رقم المخزن"] = Comp_Store.SelectedValue;
								}
							}
							else if (BtnEdit.Text == "Edit F6")
							{
								row2["المخزن"] = Comp_Store.Text;
								row2["رقم المخزن"] = Comp_Store.SelectedValue;
							}
							row2["سعر الوحدة"] = get_Item_ByCodeALL_Result2.SellPriceMedium;
							row2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium * (decimal?)Convert.ToInt32(1)), DecimalNum);
							decimal num4 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.ConvertMediumUnit);
							row2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row2["متوسط_التكلفة"]) * (Convert.ToDecimal(row2["الكمية"]) * num4), DecimalNumQty);
							if (get_Item_ByCodeALL_Result2.Is_Tax == true)
							{
								row2["الضربية"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium * (decimal?)Convert.ToInt32(1) * get_Item_ByCodeALL_Result2.Tax_Value / (decimal?)100), DecimalNum);
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium * (decimal?)Convert.ToInt32(1) * get_Item_ByCodeALL_Result2.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
							else
							{
								row2["الضربية"] = 0.0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
						}
						GetTotalCost();
						string StoreName2 = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int num5 = (from x in db.Account_Stores
							where x.Store_Name == StoreName2
							select x.ID).FirstOrDefault();
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "الكبرى")
				{
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result3 = ITM.GetItemByCode(itemCode).FirstOrDefault();
					if (get_Item_ByCodeALL_Result3 != null)
					{
						int num6 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row3 in datatable.Rows)
						{
							if (!(row3["كود_الصنف"].ToString() == Convert.ToString(itemCode)))
							{
								continue;
							}
							row3["الوحدة"] = get_Item_ByCodeALL_Result3.UnitLarge;
							row3["أسم_الوحدة"] = "الكبرى";
							row3["الكمية"] = FUNC_DecimalNumQty(1);
							row3["وحدة التفقيط"] = get_Item_ByCodeALL_Result3.UnitTafqit;
							row3["كمية التفقيط"] = "1";
							if (LoginDetails.Lang == "ar-EG")
							{
								if (BtnEdit.Text == "تعديل F6")
								{
									row3["المخزن"] = Comp_Store.Text;
									row3["رقم المخزن"] = Comp_Store.SelectedValue;
								}
							}
							else if (BtnEdit.Text == "Edit F6")
							{
								row3["المخزن"] = Comp_Store.Text;
								row3["رقم المخزن"] = Comp_Store.SelectedValue;
							}
							row3["سعر الوحدة"] = get_Item_ByCodeALL_Result3.SellpriceLarge;
							row3["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge * (decimal?)Convert.ToInt32(1)), DecimalNum);
							decimal num7 = Convert.ToDecimal(get_Item_ByCodeALL_Result3.ConvertBigUnit);
							row3["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row3["متوسط_التكلفة"]) * (Convert.ToDecimal(row3["الكمية"]) * num7), DecimalNumQty);
							if (get_Item_ByCodeALL_Result3.Is_Tax == true)
							{
								row3["الضربية"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge * (decimal?)Convert.ToInt32(1) * get_Item_ByCodeALL_Result3.Tax_Value / (decimal?)100), DecimalNum);
								row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge * (decimal?)Convert.ToInt32(1) * get_Item_ByCodeALL_Result3.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
							else
							{
								row3["الضربية"] = 0.0;
								row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
						}
						GetTotalCost();
						string StoreName3 = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int num8 = (from x in db.Account_Stores
							where x.Store_Name == StoreName3
							select x.ID).FirstOrDefault();
						frmSearchUnit.Close();
					}
				}
				frmSearchUnit.Close();
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
			Cal_FinalResultTotals();
			frmSearchUnit.Close();
		}
		else
		{
			MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف الوحدات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void BtnPlus_Click(object sender, EventArgs e)
	{
		GridPlus();
	}

	private void BtnMinus_Click(object sender, EventArgs e)
	{
		GridPlus(1m, '-');
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
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		IsClosingOk = false;
		Save();
		IsClosingOk = true;
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		if (BtnEdit.Enabled)
		{
			IsClosingOk = false;
			Edit();
			IsClosingOk = true;
		}
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		IsClosingOk = false;
		Delete();
		IsClosingOk = true;
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
		IsClosingOk = false;
		if (TxtCode.Text != "")
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
		IsClosingOk = true;
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchOrders frmSearchOrders = new FrmSearchOrders();
		frmSearchOrders.ShowDialog();
		if (frmSearchOrders.CustCode > 0)
		{
			SearchOrderNum(frmSearchOrders.CustCode);
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
		InsertNewItem(id);
	}

	public Image Base64ToImage(string base64String)
	{
		byte[] array = Convert.FromBase64String(base64String);
		MemoryStream memoryStream = new MemoryStream(array, 0, array.Length);
		memoryStream.Write(array, 0, array.Length);
		return Image.FromStream(memoryStream, useEmbeddedColorManagement: true);
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
			if (!(row["م"].ToString() == Convert.ToString(num)))
			{
				continue;
			}
			ItmID2 = row["كود_الصنف"].ToString();
			Item_Items itemsCode = ORD.GetItemsCode2(ItmID2);
			if (star == '-' && Convert.ToDecimal(row["الكمية"]) > 1m)
			{
				row["الكمية"] = Convert.ToDecimal(row["الكمية"]) - count;
				row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num2 = Convert.ToDecimal(itemsCode.Tax_Value) / 100m + 1m;
					decimal num3 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(num3 * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(row["إجمالى المبلغ"]) / num2, DecimalNum);
					VatWithTotal(itemsCode.Is_Tax, num2, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["إجمالى المبلغ"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				else
				{
					decimal num4 = Convert.ToDecimal(row["سعر الوحدة"].ToString());
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(num4 * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					Vat(itemsCode.Is_Tax, itemsCode.Tax_Value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				break;
			}
			switch (star)
			{
			case '+':
				row["الكمية"] = Convert.ToDecimal(row["الكمية"]) + count;
				row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num11 = Convert.ToDecimal(itemsCode.Tax_Value) / 100m + 1m;
					decimal num12 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(num12 * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(row["إجمالى المبلغ"]) / num11, DecimalNum);
					VatWithTotal(itemsCode.Is_Tax, num11, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["إجمالى المبلغ"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				else
				{
					decimal num13 = Convert.ToDecimal(row["سعر الوحدة"].ToString());
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(num13 * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					Vat(itemsCode.Is_Tax, itemsCode.Tax_Value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				break;
			case '*':
				row["الكمية"] = Convert.ToDecimal(row["الكمية"]) * count;
				row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num8 = Convert.ToDecimal(itemsCode.Tax_Value) / 100m + 1m;
					decimal num9 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(num9 * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(row["إجمالى المبلغ"]) / num8, DecimalNum);
					VatWithTotal(itemsCode.Is_Tax, num8, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["إجمالى المبلغ"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				else
				{
					decimal num10 = Convert.ToDecimal(row["سعر الوحدة"].ToString());
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(num10 * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					Vat(itemsCode.Is_Tax, itemsCode.Tax_Value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				break;
			case '=':
				row["الكمية"] = count;
				row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num14 = Convert.ToDecimal(itemsCode.Tax_Value) / 100m + 1m;
					decimal num15 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(num15 * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(row["إجمالى المبلغ"]) / num14, DecimalNum);
					VatWithTotal(itemsCode.Is_Tax, num14, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["إجمالى المبلغ"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				else
				{
					decimal num16 = Convert.ToDecimal(row["سعر الوحدة"].ToString());
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(num16 * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					Vat(itemsCode.Is_Tax, itemsCode.Tax_Value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				break;
			case '2':
				row["الكمية"] = Convert.ToDecimal(row["الكمية"]) + count;
				row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num5 = Convert.ToDecimal(itemsCode.Tax_Value) / 100m + 1m;
					decimal num6 = Convert.ToDecimal(row["OrginalPrice"].ToString());
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(num6 * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(row["إجمالى المبلغ"]) / num5, DecimalNum);
					VatWithTotal(itemsCode.Is_Tax, num5, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["إجمالى المبلغ"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				else
				{
					decimal num7 = Convert.ToDecimal(row["سعر الوحدة"].ToString());
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(num7 * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					Vat(itemsCode.Is_Tax, itemsCode.Tax_Value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				break;
			}
			break;
		}
		ArrangeDatatable();
		GetTotalCost();
		Cal_FinalResultTotals();
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
					if (row["م"].ToString() == Convert.ToString(num))
					{
						row.Delete();
						datatable.AcceptChanges();
						break;
					}
				}
				GetTotalCost();
				ArrangeDatatable();
				Cal_FinalResultTotals();
				gridControl1.DataSource = datatable;
			}
			frmPassWordSupervisor.Close();
			TxtBarcode.Focus();
			return;
		}
		int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row2 in datatable.Rows)
		{
			if (row2["م"].ToString() == Convert.ToString(num2))
			{
				row2.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
		GetTotalCost();
		ArrangeDatatable();
		Cal_FinalResultTotals();
		gridControl1.DataSource = datatable;
	}

	private void TxtDigital()
	{
		TxtVat.DecimalDigits = DecimalNum;
		TxtVatBeforeDisc.DecimalDigits = DecimalNum;
		TxtTotals.DecimalDigits = DecimalNum;
		TxtTotalBeforDisc.DecimalDigits = DecimalNum;
		TxtTotalAll.DecimalDigits = DecimalNum;
		TxtNet.DecimalDigits = DecimalNum;
		TxtNet22.DecimalDigits = DecimalNum;
		TxtCost.DecimalDigits = DecimalNum;
		TxtTabocc.DecimalDigits = DecimalNum;
		TxtAllVat.DecimalDigits = DecimalNum;
		TxtAllTotal.DecimalDigits = DecimalNum;
		TxtDiscount.DecimalDigits = DecimalNum;
		txtdicoutprege.DecimalDigits = DecimalNum;
	}

	private void Clear()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtCode.Text = "";
		TxtPhone.Text = "";
		TxtAdditionalNum.Text = "";
		TxtCity.Text = "";
		TxtStreet.Text = "";
		TxtDistrict.Text = "";
		TxtCharge.Text = "";
		TxtPostalCode.Text = "";
		TxtCommercialRecord.Text = "";
		TxtSales.Text = "";
		TxtNumVat.Text = "";
		TxtCountry.Text = "";
		TxtNote.Text = "";
		TxtNumNote.Text = "";
		datatable.Rows.Clear();
		dtCurrentTable.Rows.Clear();
		gridControl1.DataSource = datatable;
		GetTotalCost();
		TxtDiscount.Text = "";
		TxtVat.Text = "0.00";
		TxtVatBeforeDisc.Text = "0.00";
		TxtTotals.Text = "0.00";
		TxtTotalBeforDisc.Text = "0.00";
		TxtType.Text = "نقدى";
		TxtSuppliers.Text = "عميل نقدى";
		TxtTotalAll.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtNet22.Text = "0.00";
		TxtCost.Text = "0.00";
		TxtTabocc.Text = "0.00";
		TxtAllVat.Text = "0.00";
		toggleCashierType.IsOn = false;
		TxtRoom.Value = 0m;
		TxtTable.Value = 0m;
		TxtDiscount.Enabled = true;
		TxtType.Enabled = true;
		TxtDate.Enabled = true;
		TxtDate.DateTime = DateTime.Now;
		TxtSuppliers.Focus();
		SetDateCasheir();
		TxtBarcode.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 12))
		{
			int purchese_ID = 0;
			bool isOn = toggleHold.IsOn;
			if (datatable.Rows.Count >= 1)
			{
				if (TxtSuppliers.Text != "")
				{
					if (TxtType.Text != "")
					{
						if (Convert.ToInt32(TxtType.SelectedValue) == 2 && LblSup.Text == "1")
						{
							MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع حساب مورد نقدى", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							TxtSuppliers.Focus();
						}
						else
						{
							if (IsItemPriceZero() == 0)
							{
								return;
							}
							GetTotalCost();
							Cal_FinalResultTotals();
							if (PermissionSellPrice() == 0)
							{
								return;
							}
							if (Convert.ToInt32(TxtType.SelectedValue) == 1)
							{
								LoginDetails.TotalFatorPrice = Convert.ToDecimal(TotalHasm);
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
								MoneyNet = Convert.ToDecimal(frmMoneyCashier.TxtMoneyNet.Text);
							}
							else
							{
								LoginDetails.TotalFatorPrice = Convert.ToDecimal(TotalHasm);
								FrmMoneyAjil frmMoneyAjil = new FrmMoneyAjil();
								frmMoneyAjil.ShowDialog();
								if (!frmMoneyAjil.IsComplateMony)
								{
									return;
								}
								decimal value = ((!(Convert.ToDecimal(TxtCharge.Text) < 0m)) ? Convert.ToDecimal(TxtCharge.Text) : (Convert.ToDecimal(TxtCharge.Text) * -1m));
								decimal num = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
								decimal? num2 = CreditLimit + (decimal?)value;
								if (!((num <= num2.GetValueOrDefault()) & num2.HasValue))
								{
									num2 = CreditLimit;
									if (!((num2.GetValueOrDefault() == 0m) & num2.HasValue))
									{
										MessageBox.Show("عفوا\u064b، رصيد العميل لا يكفي \r\n  الحد الائتماني = " + FUNC_DecimalNum(CreditLimit), "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
										return;
									}
								}
								FrmCash = Convert.ToDecimal(frmMoneyAjil.TxtCash.Text);
								FrmBank = Convert.ToDecimal(frmMoneyAjil.TxtVisa.Text);
								Safy = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
								TxtAllTotal.Clear();
								TxtAllTotal.Text = Safy.ToString();
								ChargeAfterSale = Convert.ToDecimal(TxtCharge.Text) + FrmCash + FrmBank;
							}
							int costCentersID = Convert.ToInt32(Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID);
							FilterDataTableSave(datatable, purchese_ID, isOn);
							LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
							LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum);
							purchese_ID = ((Convert.ToInt32(TxtType.SelectedValue) != 1) ? ORD.SaveOrders(Convert.ToInt32(LblSup.Text), isOn, TxtSuppliers.Text, TxtPhone.Text, "", Convert.ToDateTime(TxtDate.DateTime), Convert.ToInt32(TxtType.SelectedValue), "", 1, costCentersID, Convert.ToInt32(CBBounce.SelectedValue), TxtNote.Text, TxtNumNote.Text, Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllTotal.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TotalHasm), DecimalNum), FrmCash, FrmBank, toggleCashierType.IsOn, Convert.ToInt32(TxtRoom.Value), Convert.ToInt32(TxtTable.Value), dtCurrentTable, 1, TxtSales.Text, DateTime.Parse(DateHold.Text), "", TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, 0m, 0m, ChargeAfterSale, 1, 0m, 0m, "", 0m, 0m, "", "", "", "", "", "") : ORD.SaveOrders(Convert.ToInt32(LblSup.Text), isOn, TxtSuppliers.Text, TxtPhone.Text, "", Convert.ToDateTime(TxtDate.DateTime), Convert.ToInt32(TxtType.SelectedValue), "", 1, costCentersID, Convert.ToInt32(CBBounce.SelectedValue), TxtNote.Text, TxtNumNote.Text, Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllTotal.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TotalHasm), DecimalNum), Cash, Bank, toggleCashierType.IsOn, Convert.ToInt32(TxtRoom.Value), Convert.ToInt32(TxtTable.Value), dtCurrentTable, 1, TxtSales.Text, DateTime.Parse(DateHold.Text), "", TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, 0m, 0m, ChargeAfterSale, 1, 0m, 0m, "", 0m, 0m, "", "", "", "", "", ""));
							if (purchese_ID > 0)
							{
								PrintID = purchese_ID;
								Code = purchese_ID;
								BtnSave.Enabled = false;
								BtnEdit.Enabled = true;
								BtnDelete.Enabled = true;
								TxtCode.Text = purchese_ID.ToString();
								if (LoginDetails.IsPrintCashier)
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
								if (LoginDetails.IsPrintSecondFatoraCasheir)
								{
									Thread thread2 = new Thread((ThreadStart)delegate
									{
										BeginInvoke((Action)delegate
										{
											PrintSecondDefualt(Code);
										});
									});
									thread2.Start();
								}
								Clear();
								Charge(Convert.ToInt32(LblSup.Text));
							}
							else
							{
								MessageBox.Show("تم حفظ بيانات فاتورة الكاشير رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								Cash = 0m;
								Bank = 0m;
								FrmCash = 0m;
								FrmBank = 0m;
							}
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 12))
		{
			int purchese_ID = 0;
			if (!(TxtSuppliers.Text != ""))
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
					if (IsItemPriceZero() == 0)
					{
						return;
					}
					ArrangeDatatable();
					Cal_FinalResultTotals();
					if (PermissionSellPrice() == 0)
					{
						return;
					}
					if (Convert.ToInt32(TxtType.SelectedValue) == 1)
					{
						LoginDetails.TotalFatorPrice = Convert.ToDecimal(TotalHasm);
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
						MoneyNet = Convert.ToDecimal(frmMoneyCashier.TxtMoneyNet.Text);
					}
					else
					{
						LoginDetails.TotalFatorPrice = Convert.ToDecimal(TotalHasm);
						FrmMoneyAjil frmMoneyAjil = new FrmMoneyAjil();
						frmMoneyAjil.ShowDialog();
						if (!frmMoneyAjil.IsComplateMony)
						{
							return;
						}
						decimal num = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
						decimal? num2 = CreditLimit + (decimal?)Convert.ToDecimal(TxtCharge.Text);
						if (!((num <= num2.GetValueOrDefault()) & num2.HasValue))
						{
							MessageBox.Show("عفوا\u064b، رصيد العميل لا يكفي \r\n  الحد الائتماني = " + FUNC_DecimalNum(CreditLimit), "مستكشف البيانات", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							return;
						}
						FrmCash = Convert.ToDecimal(frmMoneyAjil.TxtCash.Text);
						FrmBank = Convert.ToDecimal(frmMoneyAjil.TxtVisa.Text);
						Safy = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
						TxtAllTotal.Clear();
						TxtAllTotal.Text = Safy.ToString();
					}
					int costCentersID = Convert.ToInt32(Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID);
					FilterDataTableSave(datatable, purchese_ID, toggleHold.IsOn);
					LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
					LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum);
					purchese_ID = ((Convert.ToInt32(TxtType.SelectedValue) != 1) ? ORD.UpdateOrders(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), toggleHold.IsOn, TxtSuppliers.Text, TxtPhone.Text, "", TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), "", 1, costCentersID, Convert.ToInt32(CBBounce.SelectedValue), "", "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllTotal.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TotalHasm), DecimalNum), FrmCash, FrmBank, toggleCashierType.IsOn, Convert.ToInt32(TxtRoom.Value), Convert.ToInt32(TxtTable.Value), dtCurrentTable, TxtSales.Text, Convert.ToDateTime(DateHold.Text), "", TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, 0m, 0m, ChargeAfterSale, 0m, 0m, "", "", "", "", "", "", 1) : ORD.UpdateOrders(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), toggleHold.IsOn, TxtSuppliers.Text, TxtPhone.Text, "", TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), "", 1, costCentersID, Convert.ToInt32(CBBounce.SelectedValue), "", "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllTotal.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TotalHasm), DecimalNum), Cash, Bank, toggleCashierType.IsOn, Convert.ToInt32(TxtRoom.Value), Convert.ToInt32(TxtTable.Value), dtCurrentTable, TxtSales.Text, Convert.ToDateTime(DateHold.Text), "", TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, 0m, 0m, ChargeAfterSale, 0m, 0m, "", "", "", "", "", "", 1));
					if (purchese_ID > 0)
					{
						PrintID = Convert.ToInt32(TxtCode.Text);
						Code = Convert.ToInt32(TxtCode.Text);
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						if (LoginDetails.IsPrintCashier)
						{
							Print(Code);
						}
						Clear();
						Charge(Convert.ToInt32(LblSup.Text));
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات فاتورة الكاشير رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Cash = 0m;
						Bank = 0m;
						FrmCash = 0m;
						FrmBank = 0m;
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 12))
		{
			int num = 0;
			if (TxtCode.Text != "")
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

	private void Print(int Code)
	{
		try
		{
			if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
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
					using SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
					DataSetALL dataSetALL = new DataSetALL();
					dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
					SqlCommand sqlCommand = new SqlCommand("Print_Order_Orders", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlCommand.Parameters.AddWithValue("@ID", Code);
					sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
					sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
					sqlConnection.Close();
					if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
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
						stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
						stiReport.Dictionary.Synchronize();
						stiReport.Compile();
						stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
						stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
						stiReport["VatNumComp"] = LoginDetails.VatNum;
						stiReport["CashMoney"] = Cash.ToString();
						stiReport["CashBank"] = Bank.ToString();
						stiReport["MoneyNeed"] = MoneyNeed.ToString();
						stiReport["MoneyPaying"] = MoneyPaying.ToString();
						stiReport["MoneyNet"] = MoneyNet.ToString();
						stiReport["MobileComp"] = LoginDetails.Mobile;
						stiReport["AddressComp"] = LoginDetails.Address;
						stiReport["PrintComp"] = LoginDetails.PrintComp;
						Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
						string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), (LoginDetails.NetCash + LoginDetails.VatCash).ToString(), LoginDetails.VatCash.ToString());
						stiReport["EncHash"] = value;
						PrinterSettings printerSettings = new PrinterSettings
						{
							PrinterName = LoginDetails.DefualtPrint
						};
						stiReport.Print(showPrintDialog: false, printerSettings);
					}
					else
					{
						MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					return;
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

	private void PrintSecondCasheir(int Code)
	{
		try
		{
			if (!LoginDetails.IsPrintSecondCasheir)
			{
				return;
			}
			if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
			{
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
							if (item.Printer != "")
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
									stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
									stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
									stiReport["VatNumComp"] = LoginDetails.VatNum;
									stiReport["MobileComp"] = LoginDetails.Mobile;
									stiReport["AddressComp"] = LoginDetails.Address;
									stiReport["PrintComp"] = LoginDetails.PrintComp;
									Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
									string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString());
									stiReport["EncHash"] = value;
									PrinterSettings printerSettings = new PrinterSettings
									{
										PrinterName = item.Printer
									};
									stiReport.Print(showPrintDialog: false, printerSettings);
								}
							}
						}
						sqlConnection.Close();
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

	private void PreviewPrint(int Code)
	{
		try
		{
			if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
			{
				if (Code > 0)
				{
					SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
					DataSetALL dataSetALL = new DataSetALL();
					dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
					SqlCommand sqlCommand = new SqlCommand("Print_Order_Orders", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlCommand.Parameters.AddWithValue("@ID", Code);
					sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
					sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
					sqlConnection.Close();
					if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
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
						stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
						stiReport.Dictionary.Synchronize();
						stiReport.Compile();
						stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
						stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
						stiReport["VatNumComp"] = LoginDetails.VatNum;
						stiReport["Type"] = TxtType.Text;
						stiReport["Date"] = TxtDate.DateTime;
						stiReport["PurchID"] = Code;
						stiReport["CustName"] = TxtSuppliers.Text;
						stiReport["CustVatNum"] = "";
						stiReport["Custphone"] = "";
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
						string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString());
						stiReport["EncHash"] = value;
						string value2 = (toggleCashierType.IsOn ? "طلب محلي" : "طلب سفري");
						stiReport["CashierType"] = value2;
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

	private void PrintSecondDefualt(int Code)
	{
		try
		{
			if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
			{
				if (Code > 0)
				{
					SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
					ST.Tables["Order_PurchasesTablePrint"].Clear();
					SqlCommand sqlCommand = new SqlCommand("Print_Order_OrdersAfterReturn", sqlConnection);
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
							stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashir.mrt");
						}
						else
						{
							stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCashir.mrt");
						}
						stiReport.RegData(ST.Order_PurchasesTablePrint);
						stiReport.Dictionary.Synchronize();
						stiReport.Compile();
						stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
						stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
						stiReport["VatNumComp"] = LoginDetails.VatNum;
						stiReport["Date"] = TxtDate.DateTime;
						stiReport["PurchID"] = Code;
						stiReport["Room"] = Convert.ToInt32(TxtRoom.Value);
						stiReport["Table"] = Convert.ToInt32(TxtTable.Value);
						stiReport["Tafqit"] = txtWordAr;
						stiReport["EmployeeName"] = LoginDetails.UserName;
						stiReport["CashMoney"] = Cash.ToString();
						stiReport["CashBank"] = Bank.ToString();
						stiReport["MoneyNeed"] = MoneyNeed.ToString();
						stiReport["MoneyPaying"] = MoneyPaying.ToString();
						stiReport["MoneyNet"] = MoneyNet.ToString();
						Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
						string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString());
						stiReport["EncHash"] = value;
						string value2 = (toggleCashierType.IsOn ? "طلب محلي" : "طلب سفري");
						stiReport["CashierType"] = value2;
						stiReport["PrintComp"] = LoginDetails.PrintComp;
						stiReport["MobileComp"] = LoginDetails.Mobile;
						stiReport["AddressComp"] = LoginDetails.Address;
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
		catch (Exception)
		{
			MessageBox.Show("عفوا، لم يتم الطباعة ", "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void IsEnableTrue()
	{
		TxtBarcode.Enabled = true;
		BtnPlus.Enabled = true;
		BtnMinus.Enabled = true;
		TxtDiscount.ReadOnly = false;
		TxtType.Enabled = true;
		TxtDate.ReadOnly = false;
		TxtSuppliers.ReadOnly = false;
		if (LoginDetails.Lang == "ar-EG")
		{
			BtnEdit.Text = "تعديل F6";
		}
		else
		{
			BtnEdit.Text = "Edit F6";
		}
	}

	public void IsEnableFalse()
	{
	}

	public void SetInitialRowPrinter()
	{
		CashierPrint.Columns.Add("ID", typeof(int));
		CashierPrint.Columns.Add("Name", typeof(string));
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("ItemID", typeof(int));
		datatable.Columns.Add("ItemUnitID", typeof(int));
		datatable.Columns.Add("ItemNote", typeof(string));
		datatable.Columns.Add("TafqitId", typeof(int));
		datatable.Columns.Add("كود_الصنف", typeof(string));
		datatable.Columns.Add("أسم الصنف", typeof(string));
		datatable.Columns.Add("الاسم الانجليزي", typeof(string));
		datatable.Columns.Add("المجموعة", typeof(string));
		datatable.Columns.Add("الوحدة", typeof(string));
		datatable.Columns.Add("رقم_الوحدة", typeof(int));
		datatable.Columns.Add("المخزن", typeof(string));
		datatable.Columns.Add("رقم المخزن", typeof(int));
		datatable.Columns.Add("الكمية", typeof(decimal));
		datatable.Columns.Add("وحدة التفقيط", typeof(string));
		datatable.Columns.Add("كمية التفقيط", typeof(decimal));
		datatable.Columns.Add("متوسط_التكلفة", typeof(decimal));
		datatable.Columns.Add("اجمالي_متوسط_التكلفة", typeof(decimal));
		datatable.Columns.Add("سعر الوحدة", typeof(decimal));
		datatable.Columns.Add("سعربعدالخصم", typeof(decimal));
		datatable.Columns.Add("نسبة الخصم", typeof(decimal));
		datatable.Columns.Add("إجمالى_بعدالخصم", typeof(decimal));
		datatable.Columns.Add("هل_ضريبة", typeof(int));
		datatable.Columns.Add("قيمة_الضريبة", typeof(decimal));
		datatable.Columns.Add("أسم_الوحدة", typeof(string));
		datatable.Columns.Add("الاجمالى", typeof(decimal));
		datatable.Columns.Add("الصافى", typeof(decimal));
		datatable.Columns.Add("الضربية", typeof(decimal));
		datatable.Columns.Add("الضريبه_بعدالخصم", typeof(decimal));
		datatable.Columns.Add("إجمالى المبلغ", typeof(string));
		datatable.Columns.Add("حذف الصنف", typeof(int));
		datatable.Columns.Add("م", typeof(int));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تم", typeof(int));
		datatable.Columns.Add("PrinterID", typeof(int));
		datatable.Columns.Add("PrinterName", typeof(string));
		datatable.Columns.Add("OrginalPrice", typeof(decimal));
		datatable.Columns.Add("OrginalTotalPrice", typeof(decimal));
		datatable.Columns.Add("قيمة الخصم", typeof(decimal));
		datatable.Columns.Add("البونص", typeof(decimal));
		datatable.Columns.Add("الطول", typeof(decimal));
		datatable.Columns.Add("العرض", typeof(decimal));
		datatable.Columns.Add("الإرتفاع", typeof(decimal));
		datatable.Columns.Add("الوزن", typeof(decimal));
		datatable.Columns.Add("البيان", typeof(string));
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
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridView1.RowCellClick += gridView1_RowCellClick;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		gridView1.Columns["متوسط_التكلفة"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.##}", "0")
		});
		gridView1.Columns["اجمالي_متوسط_التكلفة"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.##}", "0")
		});
		gridView1.Columns["الضريبه_بعدالخصم"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.##}", "0")
		});
		gridView1.Columns["إجمالى_بعدالخصم"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.##}", "0")
		});
		gridView1.Columns["الكمية"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.##}", "0")
		});
		gridView1.Columns["سعر الوحدة"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.##}", "0")
		});
		gridView1.Columns["الضربية"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.##}", "0")
		});
		gridView1.Columns["إجمالى المبلغ"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.##}", "0")
		});
		gridView1.Columns["الاجمالى"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.##}", "0")
		});
		GetTotalCost();
		Cal_FinalResultTotals();
	}

	public int IsItemPriceZero()
	{
		int result = 1;
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["كود_الصنف"]);
			if (Convert.ToDecimal(row["سعر الوحدة"]) <= 0m && text != "")
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ الفاتورة لوجود اصناف سعر الوحدة تساوي صفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				result = 0;
				break;
			}
			if (!LoginDetails.IsQuantityMinZero)
			{
				decimal num = Convert.ToDecimal(row["الكمية"]);
				string text2 = Convert.ToString(row["أسم الصنف"]);
				string type = Convert.ToString(row["أسم_الوحدة"]);
				int storeID = Convert.ToInt32(row["رقم المخزن"]);
				int itemID = Convert.ToInt32(ORD.GetItmID(text));
				decimal quantityPermission = ORD.GetQuantityPermission(itemID, storeID, type);
				if (quantityPermission <= 0m)
				{
					MessageBox.Show("نعتذر، لا يوجد كمية كافية من الصنف ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					result = 0;
					break;
				}
				if (num > quantityPermission)
				{
					MessageBox.Show("نعتذر، لا يوجد كمية كافية من الصنف ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					result = 0;
					break;
				}
			}
		}
		return result;
	}

	public void CheckFfistRow()
	{
		for (int num = datatable.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = datatable.Rows[num];
			if (dataRow["الكمية"].ToString() == "0" || dataRow["سعر الوحدة"].ToString() == "0")
			{
				dataRow.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
		gridControl1.DataSource = datatable;
	}

	public void GetTotalCost()
	{
		try
		{
			object obj = datatable.Compute("SUM(الضربية)", "");
			TxtVat.Text = Convert.ToString(obj);
			object obj2 = datatable.Compute("SUM(الاجمالى)", "");
			TxtTotals.Text = Convert.ToString(obj2);
			TxtTotalAll.Text = Convert.ToString(obj2);
			TxtNet.Text = (Convert.ToDecimal(TxtVatBeforeDisc.Text) + Convert.ToDecimal(TxtTotalAll.Text)).ToString();
		}
		catch
		{
		}
	}

	public void insertnewrow()
	{
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["المجموعة"] = 1;
		dataRow["الوحدة"] = string.Empty;
		dataRow["سعر الوحدة"] = 0;
		dataRow["الكمية"] = FUNC_DecimalNumQty(1);
		dataRow["المخزن"] = Comp_Store.Text;
		dataRow["رقم المخزن"] = Comp_Store.SelectedValue;
		dataRow["تم"] = 0;
		dataRow["الطول"] = 1;
		dataRow["العرض"] = 1;
		dataRow["الإرتفاع"] = 1;
		dataRow["الوزن"] = 1;
		dataRow["قيمة الخصم"] = 0;
		dataRow["نسبة الخصم"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
	}

	public void InsertNewItem(int itemid, string Barcode)
	{
		try
		{
			bool flag = false;
			string text = "";
			Get_Item_ByID_Result get_Item_ByID_Result = ITM.Get_Item_ByID(itemid).FirstOrDefault();
			Printer printer = ORD.GetPrinter(itemid);
			bool flag2 = true;
			foreach (DataRow row in datatable.Rows)
			{
				if (get_Item_ByID_Result.ItemId == itemid)
				{
					text = get_Item_ByID_Result.UnitSmall;
				}
				if (get_Item_ByID_Result.SmallBarCode1 == Barcode || get_Item_ByID_Result.SmallBarCode2 == Barcode || get_Item_ByID_Result.SmallBarCode3 == Barcode)
				{
					text = get_Item_ByID_Result.UnitSmall;
					if (toggleSectoral.IsOn)
					{
						row["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall);
					}
					else
					{
						row["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall2);
					}
				}
				if (get_Item_ByID_Result.MediumBarCode1 == Barcode || get_Item_ByID_Result.MediumBarCode2 == Barcode || get_Item_ByID_Result.MediumBarCode3 == Barcode)
				{
					text = get_Item_ByID_Result.UnitMedium;
					if (toggleSectoral.IsOn)
					{
						row["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceMedium);
					}
					else
					{
						row["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceMedium2);
					}
				}
				if (get_Item_ByID_Result.BigBarCode1 == Barcode || get_Item_ByID_Result.BigBarCode2 == Barcode || get_Item_ByID_Result.BigBarCode3 == Barcode)
				{
					text = get_Item_ByID_Result.UnitLarge;
					if (toggleSectoral.IsOn)
					{
						row["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellpriceLarge);
					}
					else
					{
						row["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellpriceLarge2);
					}
				}
				string item_code = get_Item_ByID_Result.Item_code;
				string text2 = row["كود_الصنف"].ToString();
				string text3 = row["الوحدة"].ToString();
				row["وحدة التفقيط"] = get_Item_ByID_Result.UnitTafqit;
				row["كمية التفقيط"] = "1";
				row["TafqitId"] = get_Item_ByID_Result.UnitTafqitID;
				if (!(item_code == text2) || !(text3 == text))
				{
					continue;
				}
				row["المخزن"] = Comp_Store.Text;
				row["رقم المخزن"] = Comp_Store.SelectedValue;
				row["الكمية"] = Convert.ToDecimal(row["الكمية"]) + 1m;
				ItmID2 = row["كود_الصنف"].ToString();
				row["وحدة التفقيط"] = get_Item_ByID_Result.UnitTafqit;
				row["كمية التفقيط"] = "1";
				decimal num = Math.Round((Convert.ToDecimal(row["الكمية"]) + Convert.ToDecimal(row["البونص"])) * (Convert.ToDecimal(row["الطول"]) * Convert.ToDecimal(row["العرض"]) * Convert.ToDecimal(row["الإرتفاع"])), DecimalNum);
				decimal num2 = Math.Round(Convert.ToDecimal(row["الكمية"]) * (Convert.ToDecimal(row["الطول"]) * Convert.ToDecimal(row["العرض"]) * Convert.ToDecimal(row["الإرتفاع"])), DecimalNum);
				decimal num3 = Convert.ToDecimal(row["سعر الوحدة"]) * num2;
				row["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row["متوسط_التكلفة"]) * num);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal value = Convert.ToDecimal(get_Item_ByID_Result.Tax_Value) / 100m + 1m;
					if (toggleSectoral.IsOn)
					{
						row["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall / (decimal?)value);
					}
					else
					{
						row["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall2 / (decimal?)value);
					}
					num3 = Convert.ToDecimal(get_Item_ByID_Result.SellPriceSmall) * num2;
					row["إجمالى المبلغ"] = Math.Round(num3, DecimalNum);
					row["إجمالى المبلغ"] = Math.Round(num3, DecimalNum);
					VatWithTotal(get_Item_ByID_Result.Is_Tax, value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["إجمالى المبلغ"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				else
				{
					if (toggleSectoral.IsOn)
					{
						row["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall);
					}
					else
					{
						row["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall2);
					}
					num3 = Convert.ToDecimal(row["سعر الوحدة"]) * num2;
					row["إجمالى المبلغ"] = Math.Round(num3, DecimalNum);
					Vat(get_Item_ByID_Result.Is_Tax, get_Item_ByID_Result.Tax_Value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
				}
				ArrangeDatatable();
				GetTotalCost();
				Cal_FinalResultTotals();
				ArrangeDatatable();
				gridControl1.DataSource = datatable;
				SoundPlayer soundPlayer = new SoundPlayer("MsgSent.WAV");
				soundPlayer.Play();
				flag = true;
				TxtBarcode.Focus();
				break;
			}
			if (!flag)
			{
				DataRow dataRow2 = null;
				dataRow2 = datatable.NewRow();
				dataRow2["ItemID"] = get_Item_ByID_Result.ItemId;
				dataRow2["ItemUnitID"] = get_Item_ByID_Result.UnitSmallID;
				dataRow2["كود_الصنف"] = get_Item_ByID_Result.Item_code;
				dataRow2["أسم الصنف"] = get_Item_ByID_Result.item_Name;
				dataRow2["الاسم الانجليزي"] = get_Item_ByID_Result.item_Name_English;
				dataRow2["المجموعة"] = get_Item_ByID_Result.Category_ID;
				dataRow2["PrinterID"] = printer.ID;
				dataRow2["PrinterName"] = printer.Name;
				dataRow2["الكمية"] = FUNC_DecimalNumQty(1);
				dataRow2["مسح"] = 0;
				dataRow2["تم"] = 1;
				dataRow2["الطول"] = 1;
				dataRow2["العرض"] = 1;
				dataRow2["الإرتفاع"] = 1;
				dataRow2["الوزن"] = 1;
				dataRow2["البونص"] = 0;
				dataRow2["قيمة الخصم"] = 0;
				dataRow2["نسبة الخصم"] = 0;
				dataRow2["المخزن"] = Comp_Store.Text;
				dataRow2["رقم المخزن"] = Comp_Store.SelectedValue;
				dataRow2["وحدة التفقيط"] = get_Item_ByID_Result.UnitTafqit;
				dataRow2["كمية التفقيط"] = "1";
				dataRow2["TafqitId"] = get_Item_ByID_Result.UnitTafqitID;
				dataRow2["أسم_الوحدة"] = "الصغرى";
				dataRow2["الوحدة"] = get_Item_ByID_Result.UnitSmall;
				dataRow2["متوسط_التكلفة"] = get_Item_ByID_Result.Average_cost;
				dataRow2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow2["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow2["الكمية"]), DecimalNum);
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal value2 = Convert.ToDecimal(get_Item_ByID_Result.Tax_Value) / 100m + 1m;
					if (toggleSectoral.IsOn)
					{
						dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall / (decimal?)value2);
						dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceSmall), DecimalNum);
						dataRow2["OrginalPrice"] = dataRow2["إجمالى المبلغ"];
					}
					else
					{
						dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall2);
						dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceSmall2), DecimalNum);
						dataRow2["OrginalPrice"] = dataRow2["إجمالى المبلغ"];
					}
					VatWithTotal(get_Item_ByID_Result.Is_Tax, value2, Convert.ToDecimal(dataRow2["سعر الوحدة"]), Convert.ToDecimal(dataRow2["إجمالى المبلغ"]), Convert.ToDecimal(dataRow2["الكمية"]), dataRow2, "كود_الصنف");
				}
				else
				{
					if (toggleSectoral.IsOn)
					{
						dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall);
						dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceSmall), DecimalNum);
					}
					else
					{
						dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall2);
						dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceSmall2), DecimalNum);
					}
					Vat(get_Item_ByID_Result.Is_Tax, get_Item_ByID_Result.Tax_Value, Convert.ToDecimal(dataRow2["سعر الوحدة"]), Convert.ToDecimal(dataRow2["الكمية"]), dataRow2, "كود_الصنف");
				}
				if (get_Item_ByID_Result.MediumBarCode1 == Barcode || get_Item_ByID_Result.MediumBarCode2 == Barcode || get_Item_ByID_Result.MediumBarCode3 == Barcode)
				{
					dataRow2["الوحدة"] = get_Item_ByID_Result.UnitMedium;
					dataRow2["رقم_الوحدة"] = get_Item_ByID_Result.UnitMediumID;
					dataRow2["أسم_الوحدة"] = "المتوسطة";
					dataRow2["متوسط_التكلفة"] = get_Item_ByID_Result.Average_cost * get_Item_ByID_Result.ConvertMediumUnit;
					dataRow2["اجمالي_متوسط_التكلفة"] = get_Item_ByID_Result.Average_cost * get_Item_ByID_Result.ConvertMediumUnit;
					dataRow2["سعر الوحدة"] = get_Item_ByID_Result.SellPriceMedium;
					dataRow2["الاجمالى"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceMedium);
					if (LoginDetails.ItemTotalWithVat)
					{
						decimal value3 = Convert.ToDecimal(get_Item_ByID_Result.Tax_Value) / 100m + 1m;
						if (toggleSectoral.IsOn)
						{
							dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceMedium / (decimal?)value3);
							dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceMedium), DecimalNum);
							dataRow2["OrginalPrice"] = dataRow2["إجمالى المبلغ"];
						}
						else
						{
							dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceMedium2);
							dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceMedium2), DecimalNum);
							dataRow2["OrginalPrice"] = dataRow2["إجمالى المبلغ"];
						}
						VatWithTotal(get_Item_ByID_Result.Is_Tax, value3, Convert.ToDecimal(dataRow2["سعر الوحدة"]), Convert.ToDecimal(dataRow2["إجمالى المبلغ"]), Convert.ToDecimal(dataRow2["الكمية"]), dataRow2, "كود_الصنف");
					}
					else
					{
						if (toggleSectoral.IsOn)
						{
							dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceMedium);
							dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceMedium), DecimalNum);
						}
						else
						{
							dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceMedium2);
							dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceMedium2), DecimalNum);
						}
						Vat(get_Item_ByID_Result.Is_Tax, get_Item_ByID_Result.Tax_Value, Convert.ToDecimal(dataRow2["سعر الوحدة"]), Convert.ToDecimal(dataRow2["الكمية"]), dataRow2, "كود_الصنف");
					}
				}
				else if (get_Item_ByID_Result.BigBarCode1 == Barcode || get_Item_ByID_Result.BigBarCode2 == Barcode || get_Item_ByID_Result.BigBarCode3 == Barcode)
				{
					dataRow2["الوحدة"] = get_Item_ByID_Result.UnitLarge;
					dataRow2["رقم_الوحدة"] = get_Item_ByID_Result.UnitLargeID;
					dataRow2["أسم_الوحدة"] = "الكبرى";
					dataRow2["متوسط_التكلفة"] = get_Item_ByID_Result.Average_cost * get_Item_ByID_Result.ConvertMediumUnit * get_Item_ByID_Result.ConvertBigUnit;
					dataRow2["اجمالي_متوسط_التكلفة"] = get_Item_ByID_Result.Average_cost * get_Item_ByID_Result.ConvertMediumUnit * get_Item_ByID_Result.ConvertBigUnit;
					if (LoginDetails.ItemTotalWithVat)
					{
						decimal value4 = Convert.ToDecimal(get_Item_ByID_Result.Tax_Value) / 100m + 1m;
						if (toggleSectoral.IsOn)
						{
							dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellpriceLarge / (decimal?)value4);
							dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellpriceLarge), DecimalNum);
							dataRow2["OrginalPrice"] = dataRow2["إجمالى المبلغ"];
						}
						else
						{
							dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellpriceLarge2);
							dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellpriceLarge2), DecimalNum);
							dataRow2["OrginalPrice"] = dataRow2["إجمالى المبلغ"];
						}
						VatWithTotal(get_Item_ByID_Result.Is_Tax, value4, Convert.ToDecimal(dataRow2["سعر الوحدة"]), Convert.ToDecimal(dataRow2["إجمالى المبلغ"]), Convert.ToDecimal(dataRow2["الكمية"]), dataRow2, "كود_الصنف");
					}
					else
					{
						if (toggleSectoral.IsOn)
						{
							dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellpriceLarge);
							dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellpriceLarge), DecimalNum);
						}
						else
						{
							dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellpriceLarge2);
							dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellpriceLarge2), DecimalNum);
						}
						Vat(get_Item_ByID_Result.Is_Tax, get_Item_ByID_Result.Tax_Value, Convert.ToDecimal(dataRow2["سعر الوحدة"]), Convert.ToDecimal(dataRow2["الكمية"]), dataRow2, "كود_الصنف");
					}
				}
				dataRow2["م"] = TblID;
				TblID++;
				ArrangeDatatable();
				datatable.Rows.Add(dataRow2);
				gridControl1.DataSource = datatable;
				GetTotalCost();
				Cal_FinalResultTotals();
				SoundPlayer soundPlayer2 = new SoundPlayer("MsgSent.WAV");
				soundPlayer2.Play();
			}
			ArrangeDatatable();
		}
		catch
		{
		}
	}

	private void VatWithTotal(bool? Is_Tax, decimal? Tax_Value, decimal? Unitprice, decimal? Totalprice, decimal AllQuatinty, DataRow dr, string ColumnField)
	{
		if (ORD.IsVatActive())
		{
			if (Is_Tax == true)
			{
				dr["إجمالى المبلغ"] = FUNC_DecimalNum(Totalprice);
				dr["الاجمالى"] = FUNC_DecimalNum(Totalprice / Tax_Value);
				dr["الضربية"] = FUNC_DecimalNum(Convert.ToDecimal(dr["إجمالى المبلغ"]) - Convert.ToDecimal(dr["الاجمالى"]));
				dr["هل_ضريبة"] = 1;
				dr["قيمة_الضريبة"] = Tax_Value;
			}
			else
			{
				dr["الضربية"] = 0;
				dr["إجمالى المبلغ"] = FUNC_DecimalNum(Totalprice);
				dr["الاجمالى"] = FUNC_DecimalNum(Totalprice);
				dr["هل_ضريبة"] = 0;
				dr["قيمة_الضريبة"] = 0;
			}
		}
		else
		{
			dr["الضربية"] = 0;
			dr["إجمالى المبلغ"] = FUNC_DecimalNum(Totalprice - (decimal?)Convert.ToDecimal(dr["قيمة الخصم"]));
			dr["الاجمالى"] = FUNC_DecimalNum(Totalprice);
			dr["هل_ضريبة"] = 0;
			dr["قيمة_الضريبة"] = 0;
		}
		decimal num = Convert.ToDecimal(dr["نسبة الخصم"]);
		if (ColumnField == "نسبة الخصم" || num != 0m)
		{
			if (num <= 100m && num != 0m)
			{
				decimal num2 = FUNC_DecimalNum(Convert.ToDecimal(dr["نسبة الخصم"]) * Convert.ToDecimal(dr["الاجمالى"]) / 100m);
				dr["قيمة الخصم"] = num2;
				dr["إجمالى_بعدالخصم"] = Math.Round(Convert.ToDecimal(dr["الاجمالى"]) - num2, DecimalNum);
				dr["الصافى"] = Math.Round(Convert.ToDecimal(dr["إجمالى المبلغ"]) - num2, DecimalNum);
			}
			else
			{
				dr["نسبة الخصم"] = "0.00";
				dr["قيمة الخصم"] = "0.00";
				dr["إجمالى_بعدالخصم"] = dr["الاجمالى"];
				dr["الصافى"] = dr["إجمالى المبلغ"];
			}
		}
		else if (Convert.ToDecimal(dr["قيمة الخصم"]) <= Convert.ToDecimal(dr["الاجمالى"]) && Convert.ToDecimal(dr["قيمة الخصم"]) != 0m)
		{
			decimal num3 = Convert.ToDecimal(dr["الاجمالى"]);
			dr["إجمالى_بعدالخصم"] = Math.Round(Convert.ToDecimal(dr["الاجمالى"]) - Convert.ToDecimal(dr["قيمة الخصم"]), DecimalNum).ToString();
			dr["الصافى"] = Math.Round(Convert.ToDecimal(dr["الاجمالى"]) - Convert.ToDecimal(dr["قيمة الخصم"]), DecimalNum);
			decimal num4 = Math.Round(Convert.ToDecimal(dr["قيمة الخصم"]) / Convert.ToDecimal(dr["الاجمالى"]) * 100m, DecimalNum);
			dr["نسبة الخصم"] = num4;
		}
		else
		{
			dr["نسبة الخصم"] = "0.00";
			dr["قيمة الخصم"] = "0.00";
			dr["إجمالى_بعدالخصم"] = dr["الاجمالى"];
			dr["الصافى"] = dr["إجمالى المبلغ"];
		}
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
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(text.ToString()).FirstOrDefault();
					Printer printer = ORD.GetPrinter(get_Item_ByCodeALL_Result.ItemId);
					if (get_Item_ByCodeALL_Result != null)
					{
						DataRow dataRow = null;
						dataRow = datatable.NewRow();
						dataRow["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
						dataRow["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
						dataRow["PrinterID"] = printer.ID;
						dataRow["PrinterName"] = printer.Name;
						dataRow["كود_الصنف"] = get_Item_ByCodeALL_Result.Item_code;
						dataRow["أسم الصنف"] = get_Item_ByCodeALL_Result.item_Name;
						dataRow["الاسم الانجليزي"] = get_Item_ByCodeALL_Result.item_Name_English;
						dataRow["المجموعة"] = get_Item_ByCodeALL_Result.Category_ID;
						dataRow["الوحدة"] = get_Item_ByCodeALL_Result.UnitSmall;
						dataRow["الكمية"] = 1;
						dataRow["متوسط_التكلفة"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.Average_cost);
						dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
						dataRow["المخزن"] = Comp_Store.Text;
						dataRow["رقم المخزن"] = Comp_Store.SelectedValue;
						dataRow["أسم_الوحدة"] = "الصغرى";
						dataRow["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitTafqit;
						dataRow["كمية التفقيط"] = "1";
						dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitTafqitID;
						dataRow["الطول"] = 1;
						dataRow["العرض"] = 1;
						dataRow["الإرتفاع"] = 1;
						dataRow["الوزن"] = 1;
						dataRow["قيمة الخصم"] = 0;
						dataRow["نسبة الخصم"] = 0;
						dataRow["البونص"] = 0;
						dataRow["نسبة الخصم"] = 0;
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							if (toggleSectoral.IsOn)
							{
								dataRow["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall / (decimal?)value);
								dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["إجمالى المبلغ"];
							}
							else
							{
								dataRow["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
								dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall2), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["إجمالى المبلغ"];
							}
							VatWithTotal(get_Item_ByCodeALL_Result.Is_Tax, value, Convert.ToDecimal(dataRow["سعر الوحدة"]), Convert.ToDecimal(dataRow["إجمالى المبلغ"]), Convert.ToDecimal(dataRow["الكمية"]), dataRow, "كود_الصنف");
						}
						else
						{
							if (toggleSectoral.IsOn)
							{
								dataRow["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
								dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
							}
							else
							{
								dataRow["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
								dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall2), DecimalNum);
							}
							Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(dataRow["سعر الوحدة"]), Convert.ToDecimal(dataRow["الكمية"]), dataRow, "كود_الصنف");
						}
						datatable.Rows.Add(dataRow);
					}
					IsShow = 1;
				}
				GetTotalCost();
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
			Cal_FinalResultTotals();
			gridControl1.DataSource = datatable;
		}
		catch (Exception)
		{
			Clear();
			Cal_FinalResultTotals();
			LoginDetails.ISCheckSearchItem = true;
			LoginDetails.ISPurches = false;
			TxtSuppliers.Focus();
		}
	}

	public void InsertNewItem22(int itemid, string Barcode)
	{
		try
		{
			bool flag = false;
			string text = "";
			Get_Item_ByID_Result get_Item_ByID_Result = ITM.Get_Item_ByID(itemid).FirstOrDefault();
			Printer printer = ORD.GetPrinter(itemid);
			decimal? sellPriceSmall = get_Item_ByID_Result.SellPriceSmall;
			if ((sellPriceSmall.GetValueOrDefault() > 0m) & sellPriceSmall.HasValue)
			{
				foreach (DataRow row in datatable.Rows)
				{
					if (get_Item_ByID_Result.ItemId == itemid)
					{
						text = get_Item_ByID_Result.UnitSmall;
					}
					if (get_Item_ByID_Result.SmallBarCode1 == Barcode || get_Item_ByID_Result.SmallBarCode2 == Barcode || get_Item_ByID_Result.SmallBarCode3 == Barcode)
					{
						text = get_Item_ByID_Result.UnitSmall;
					}
					if (get_Item_ByID_Result.MediumBarCode1 == Barcode || get_Item_ByID_Result.MediumBarCode2 == Barcode || get_Item_ByID_Result.MediumBarCode3 == Barcode)
					{
						text = get_Item_ByID_Result.UnitMedium;
					}
					if (get_Item_ByID_Result.BigBarCode1 == Barcode || get_Item_ByID_Result.BigBarCode2 == Barcode || get_Item_ByID_Result.BigBarCode3 == Barcode)
					{
						text = get_Item_ByID_Result.UnitLarge;
					}
					string item_code = get_Item_ByID_Result.Item_code;
					string text2 = row["كود_الصنف"].ToString();
					string text3 = row["الوحدة"].ToString();
					row["وحدة التفقيط"] = get_Item_ByID_Result.UnitTafqit;
					row["كمية التفقيط"] = "1";
					row["TafqitId"] = get_Item_ByID_Result.UnitTafqitID;
					if (item_code == text2 && text3 == text)
					{
						row["المخزن"] = Comp_Store.Text;
						row["رقم المخزن"] = Comp_Store.SelectedValue;
						if (toggleSectoral.IsOn)
						{
							row["سعر الوحدة"] = get_Item_ByID_Result.SellPriceSmall;
						}
						else
						{
							row["سعر الوحدة"] = get_Item_ByID_Result.SellPriceSmall2;
						}
						row["الكمية"] = Convert.ToDecimal(row["الكمية"]) + 1m;
						ItmID2 = row["كود_الصنف"].ToString();
						row["وحدة التفقيط"] = get_Item_ByID_Result.UnitTafqit;
						row["كمية التفقيط"] = "1";
						decimal num = Math.Round((Convert.ToDecimal(row["الكمية"]) + Convert.ToDecimal(row["البونص"])) * (Convert.ToDecimal(row["الطول"]) * Convert.ToDecimal(row["العرض"]) * Convert.ToDecimal(row["الإرتفاع"])), DecimalNum);
						decimal num2 = Math.Round(Convert.ToDecimal(row["الكمية"]) * (Convert.ToDecimal(row["الطول"]) * Convert.ToDecimal(row["العرض"]) * Convert.ToDecimal(row["الإرتفاع"])), DecimalNum);
						decimal d = Convert.ToDecimal(row["سعر الوحدة"]) * num2;
						row["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row["متوسط_التكلفة"]) * num);
						row["الاجمالى"] = Math.Round(d, DecimalNum);
						Vat(get_Item_ByID_Result.Is_Tax, get_Item_ByID_Result.Tax_Value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
						ArrangeDatatable();
						GetTotalCost();
						Cal_FinalResultTotals();
						ArrangeDatatable();
						gridControl1.DataSource = datatable;
						SoundPlayer soundPlayer = new SoundPlayer("MsgSent.WAV");
						soundPlayer.Play();
						flag = true;
						TxtBarcode.Focus();
						break;
					}
				}
				if (!flag)
				{
					DataRow dataRow2 = null;
					dataRow2 = datatable.NewRow();
					dataRow2["ItemID"] = get_Item_ByID_Result.ItemId;
					dataRow2["ItemUnitID"] = get_Item_ByID_Result.UnitSmallID;
					dataRow2["كود_الصنف"] = get_Item_ByID_Result.Item_code;
					dataRow2["أسم الصنف"] = get_Item_ByID_Result.item_Name;
					dataRow2["الاسم الانجليزي"] = get_Item_ByID_Result.item_Name_English;
					dataRow2["المجموعة"] = get_Item_ByID_Result.Category_ID;
					dataRow2["PrinterID"] = printer.ID;
					dataRow2["PrinterName"] = printer.Name;
					dataRow2["الكمية"] = FUNC_DecimalNumQty(1);
					dataRow2["مسح"] = 0;
					dataRow2["تم"] = 1;
					dataRow2["الطول"] = 1;
					dataRow2["العرض"] = 1;
					dataRow2["الإرتفاع"] = 1;
					dataRow2["الوزن"] = 1;
					dataRow2["البونص"] = 0;
					dataRow2["قيمة الخصم"] = 0;
					dataRow2["نسبة الخصم"] = 0;
					dataRow2["المخزن"] = Comp_Store.Text;
					dataRow2["رقم المخزن"] = Comp_Store.SelectedValue;
					dataRow2["وحدة التفقيط"] = get_Item_ByID_Result.UnitTafqit;
					dataRow2["كمية التفقيط"] = "1";
					dataRow2["TafqitId"] = get_Item_ByID_Result.UnitTafqitID;
					if (Barcode == get_Item_ByID_Result.MediumBarCode1 || Barcode == get_Item_ByID_Result.MediumBarCode2 || Barcode == get_Item_ByID_Result.MediumBarCode3)
					{
						dataRow2["أسم_الوحدة"] = "المتوسطة";
						dataRow2["سعر الوحدة"] = get_Item_ByID_Result.SellPriceMedium;
						dataRow2["الوحدة"] = get_Item_ByID_Result.UnitSmall;
						dataRow2["متوسط_التكلفة"] = get_Item_ByID_Result.Average_cost * get_Item_ByID_Result.ConvertMediumUnit;
						dataRow2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow2["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow2["الكمية"]), DecimalNum);
						dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceMedium * (decimal?)Convert.ToDecimal(1)), DecimalNum);
					}
					else if (Barcode == get_Item_ByID_Result.BigBarCode1 || Barcode == get_Item_ByID_Result.BigBarCode2 || Barcode == get_Item_ByID_Result.BigBarCode3)
					{
						dataRow2["أسم_الوحدة"] = "الكبرى";
						dataRow2["سعر الوحدة"] = get_Item_ByID_Result.SellpriceLarge;
						dataRow2["الوحدة"] = get_Item_ByID_Result.UnitSmall;
						dataRow2["متوسط_التكلفة"] = get_Item_ByID_Result.Average_cost * get_Item_ByID_Result.ConvertMediumUnit * get_Item_ByID_Result.ConvertBigUnit;
						dataRow2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow2["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow2["الكمية"]), DecimalNum);
						dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellpriceLarge * (decimal?)Convert.ToDecimal(1)), DecimalNum);
					}
					else
					{
						dataRow2["أسم_الوحدة"] = "الصغرى";
						if (toggleSectoral.IsOn)
						{
							dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall);
						}
						else
						{
							dataRow2["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByID_Result.SellPriceSmall2);
						}
						dataRow2["الوحدة"] = get_Item_ByID_Result.UnitSmall;
						dataRow2["متوسط_التكلفة"] = get_Item_ByID_Result.Average_cost;
						dataRow2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow2["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow2["الكمية"]), DecimalNum);
						dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceSmall * (decimal?)Convert.ToDecimal(1)), DecimalNum);
					}
					decimal num3 = Math.Round((Convert.ToDecimal(dataRow2["الكمية"]) + Convert.ToDecimal(dataRow2["البونص"])) * (Convert.ToDecimal(dataRow2["الطول"]) * Convert.ToDecimal(dataRow2["العرض"]) * Convert.ToDecimal(dataRow2["الإرتفاع"])), DecimalNum);
					decimal num4 = Math.Round(Convert.ToDecimal(dataRow2["الكمية"]) * (Convert.ToDecimal(dataRow2["الطول"]) * Convert.ToDecimal(dataRow2["العرض"]) * Convert.ToDecimal(dataRow2["الإرتفاع"])), DecimalNum);
					decimal d2 = Convert.ToDecimal(dataRow2["سعر الوحدة"]) * num4;
					dataRow2["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["متوسط_التكلفة"]) * num3);
					dataRow2["الاجمالى"] = Math.Round(d2, DecimalNum);
					Vat(get_Item_ByID_Result.Is_Tax, get_Item_ByID_Result.Tax_Value, Convert.ToDecimal(dataRow2["سعر الوحدة"]), Convert.ToDecimal(dataRow2["الكمية"]), dataRow2, "كود_الصنف");
					dataRow2["م"] = TblID;
					TblID++;
					ArrangeDatatable();
					datatable.Rows.Add(dataRow2);
					gridControl1.DataSource = datatable;
					GetTotalCost();
					Cal_FinalResultTotals();
					SoundPlayer soundPlayer2 = new SoundPlayer("MsgSent.WAV");
					soundPlayer2.Play();
				}
				ArrangeDatatable();
			}
			else
			{
				MessageBox.Show("عفوا لا يوجد سعر بيع", "مستكشف الاسعار", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.ServiceNotification);
			}
		}
		catch
		{
		}
	}

	public void InsertNewItem(int itemid)
	{
		bool flag = false;
		Get_Item_ByID_Result get_Item_ByID_Result = ITM.Get_Item_ByID(itemid).FirstOrDefault();
		Printer printer = ORD.GetPrinter(itemid);
		decimal? sellPriceSmall = get_Item_ByID_Result.SellPriceSmall;
		if ((sellPriceSmall.GetValueOrDefault() > 0m) & sellPriceSmall.HasValue)
		{
			foreach (DataRow row in datatable.Rows)
			{
				string item_code = get_Item_ByID_Result.Item_code;
				string text = row["كود_الصنف"].ToString();
				if (item_code == text)
				{
					row["المخزن"] = Comp_Store.Text;
					row["رقم المخزن"] = Comp_Store.SelectedValue;
					if (toggleSectoral.IsOn)
					{
						row["سعر الوحدة"] = get_Item_ByID_Result.SellPriceSmall;
					}
					else
					{
						row["سعر الوحدة"] = get_Item_ByID_Result.SellPriceSmall2;
					}
					row["وحدة التفقيط"] = get_Item_ByID_Result.UnitTafqit;
					row["كمية التفقيط"] = "1";
					row["TafqitId"] = get_Item_ByID_Result.UnitTafqitID;
					row["الكمية"] = FUNC_DecimalNumQty(Convert.ToDecimal(row["الكمية"]) + 1m);
					ItmID2 = row["كود_الصنف"].ToString();
					decimal num = Math.Round((Convert.ToDecimal(row["الكمية"]) + Convert.ToDecimal(row["البونص"])) * (Convert.ToDecimal(row["الطول"]) * Convert.ToDecimal(row["العرض"]) * Convert.ToDecimal(row["الإرتفاع"])), DecimalNum);
					decimal num2 = Math.Round(Convert.ToDecimal(row["الكمية"]) * (Convert.ToDecimal(row["الطول"]) * Convert.ToDecimal(row["العرض"]) * Convert.ToDecimal(row["الإرتفاع"])), DecimalNum);
					decimal d = Convert.ToDecimal(row["سعر الوحدة"]) * num2;
					row["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row["متوسط_التكلفة"]) * num);
					row["الاجمالى"] = Math.Round(d, DecimalNum);
					Vat(get_Item_ByID_Result.Is_Tax, get_Item_ByID_Result.Tax_Value, Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الكمية"]), row, "كود_الصنف");
					ArrangeDatatable();
					GetTotalCost();
					Cal_FinalResultTotals();
					gridControl1.DataSource = datatable;
					SoundPlayer soundPlayer = new SoundPlayer("MsgSent.WAV");
					soundPlayer.Play();
					flag = true;
					TxtBarcode.Focus();
					break;
				}
			}
			if (!flag)
			{
				DataRow dataRow2 = null;
				dataRow2 = datatable.NewRow();
				dataRow2["ItemID"] = get_Item_ByID_Result.ItemId;
				dataRow2["ItemUnitID"] = get_Item_ByID_Result.UnitSmallID;
				dataRow2["PrinterID"] = printer.ID;
				dataRow2["PrinterName"] = printer.Name;
				dataRow2["كود_الصنف"] = get_Item_ByID_Result.Item_code;
				dataRow2["أسم الصنف"] = get_Item_ByID_Result.item_Name;
				dataRow2["الاسم الانجليزي"] = get_Item_ByID_Result.item_Name_English;
				dataRow2["المجموعة"] = get_Item_ByID_Result.Category_ID;
				dataRow2["الوحدة"] = get_Item_ByID_Result.UnitSmall;
				dataRow2["الكمية"] = FUNC_DecimalNumQty(1);
				dataRow2["مسح"] = 0;
				dataRow2["تم"] = 1;
				dataRow2["أسم_الوحدة"] = "الصغرى";
				dataRow2["وحدة التفقيط"] = get_Item_ByID_Result.UnitTafqit;
				dataRow2["كمية التفقيط"] = "1";
				dataRow2["TafqitId"] = get_Item_ByID_Result.UnitTafqitID;
				dataRow2["متوسط_التكلفة"] = get_Item_ByID_Result.Average_cost;
				dataRow2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow2["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow2["الكمية"]), DecimalNum);
				if (toggleSectoral.IsOn)
				{
					dataRow2["سعر الوحدة"] = get_Item_ByID_Result.SellPriceSmall;
					dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceSmall), DecimalNum);
				}
				else
				{
					dataRow2["سعر الوحدة"] = get_Item_ByID_Result.SellPriceSmall2;
					dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByID_Result.SellPriceSmall2), DecimalNum);
				}
				dataRow2["المخزن"] = ORD.GetStoreName();
				dataRow2["رقم المخزن"] = ORD.GetStoreID();
				dataRow2["البونص"] = 0;
				dataRow2["الطول"] = 1;
				dataRow2["العرض"] = 1;
				dataRow2["الإرتفاع"] = 1;
				dataRow2["الوزن"] = 1;
				dataRow2["قيمة الخصم"] = 0;
				dataRow2["نسبة الخصم"] = 0;
				Vat(get_Item_ByID_Result.Is_Tax, get_Item_ByID_Result.Tax_Value, Convert.ToDecimal(dataRow2["سعر الوحدة"]), Convert.ToDecimal(dataRow2["الكمية"]), dataRow2, "كود_الصنف");
				datatable.Rows.Add(dataRow2);
				ArrangeDatatable();
				GetTotalCost();
				Cal_FinalResultTotals();
				gridControl1.DataSource = datatable;
				SoundPlayer soundPlayer2 = new SoundPlayer("MsgSent.wav");
				soundPlayer2.Play();
				TxtBarcode.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا لا يوجد سعر بيع", "مستكشف الاسعار", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.ServiceNotification);
		}
		Cal_FinalResultTotals();
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
				dataRow["كود_الصنف"] = get_Item_ByCodeALL_Result.Item_code;
				dataRow["أسم الصنف"] = get_Item_ByCodeALL_Result.item_Name;
				dataRow["الاسم الانجليزي"] = get_Item_ByCodeALL_Result.item_Name_English;
				dataRow["المجموعة"] = get_Item_ByCodeALL_Result.Category_ID;
				dataRow["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitTafqit;
				dataRow["كمية التفقيط"] = "1";
				if (LoginDetails.IsMizanWeight)
				{
					decimal num2 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
					decimal num3 = Convert.ToDecimal(value) / 10000m;
					decimal num4 = num3 * Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall);
					num = num3 - num4;
					dataRow["الكمية"] = FUNC_DecimalNumQty(num3);
					dataRow["سعر الوحدة"] = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
					dataRow["الاجمالى"] = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall) * num3);
				}
				else
				{
					decimal num5 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
					decimal num3 = Convert.ToDecimal(value) / 10000m;
					decimal num4 = num3 / num5;
					num = Convert.ToDecimal(num3) - Convert.ToDecimal(num4);
					dataRow["الكمية"] = FUNC_DecimalNumQty(Convert.ToDecimal(num4) / Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
					dataRow["سعر الوحدة"] = FUNC_DecimalNum(num4);
					dataRow["الاجمالى"] = FUNC_DecimalNum(num4);
				}
				dataRow["مسح"] = 0;
				dataRow["تم"] = 1;
				dataRow["المخزن"] = ORD.GetStoreName();
				dataRow["رقم المخزن"] = ORD.GetStoreID();
				dataRow["أسم_الوحدة"] = "الصغرى";
				dataRow["الوحدة"] = get_Item_ByCodeALL_Result.UnitSmall;
				dataRow["متوسط_التكلفة"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.Average_cost);
				dataRow["الطول"] = 1;
				dataRow["العرض"] = 1;
				dataRow["الإرتفاع"] = 1;
				dataRow["الوزن"] = 1;
				dataRow["قيمة الخصم"] = 0;
				dataRow["نسبة الخصم"] = 0;
				dataRow["البونص"] = 0;
				dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitTafqitID;
				dataRow["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]));
				Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(dataRow["سعر الوحدة"]), Convert.ToDecimal(dataRow["الكمية"]), dataRow, "MEZAN");
				dataRow["م"] = TblID;
				TblID++;
				datatable.Rows.Add(dataRow);
				gridControl1.DataSource = datatable;
				GetTotalCost();
				Cal_FinalResultTotals();
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

	private void Cal_FinalResultTotals()
	{
		try
		{
			if (LoginDetails.ItemTotalWithVat)
			{
				Cal_FinalResultTotalsItemWithVat();
				return;
			}
			if (LoginDetails.IsTobacc)
			{
				if (datatable.Rows.Count < 1)
				{
					TxtVat.Text = "0.000";
					TxtNet.Text = "0.00";
					TxtNet22.Text = "0.00";
					TxtTotals.Text = "0.00";
					TxtDiscount.Text = "0.00";
					txtdicoutprege.Text = "0.00";
					txtdicoutprege.Text = "0.00";
					TxtPhone.Text = "";
					TxtNumNote.Text = "";
					TxtTotalBeforDisc.Text = "0.00";
					TxtVatBeforeDisc.Text = "0.00";
					return;
				}
				if (TxtDiscount.Text == "" || TxtDiscount.Text == "0.00" || Convert.ToDecimal(TxtDiscount.Text) > Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
				{
					TxtDiscount.Text = "0.00";
					txtdicoutprege.Text = "0.00";
					TxtDiscountALLTotal.Text = "0.00";
					foreach (DataRow row in datatable.Rows)
					{
						if (row["أسم_الوحدة"].ToString() != "")
						{
							row["سعربعدالخصم"] = row["سعر الوحدة"];
							row["الضريبه_بعدالخصم"] = row["الضربية"];
							if (((Convert.ToDecimal(row["قيمة الخصم"]) == 0m || row["قيمة الخصم"].ToString() == "") && Convert.ToDecimal(row["نسبة الخصم"]) == 0m) || row["نسبة الخصم"].ToString() == "")
							{
								row["إجمالى_بعدالخصم"] = row["الاجمالى"];
							}
						}
					}
					decimal num = Convert.ToDecimal(datatable.Compute("SUM(الصافى)", ""));
					TxtTotals.Text = Convert.ToString(num);
					decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
					TxtVat.Text = Convert.ToString(num2);
					decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
					TxtTotalBeforDisc.Text = Convert.ToString(num3);
					decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعدالخصم)", ""));
					TxtVatBeforeDisc.Text = Convert.ToString(num4 + num4);
					decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
					TxtCost.Text = Convert.ToString(num5);
					TxtTotalAll.Text = TxtTotalBeforDisc.Text;
					TotalHasm = Convert.ToDecimal(TxtNet22.Text);
					TxtNet.Text = Math.Round(num - Convert.ToDecimal(TxtDiscount.Text), DecimalNum).ToString();
					decimal num6 = Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum);
					TxtTabocc.Text = num6.ToString();
					TxtAllVat.Text = (Convert.ToDecimal(TxtVatBeforeDisc.Text) + Convert.ToDecimal(TxtTabocc.Text)).ToString();
					TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text) + num6, DecimalNum)).ToString();
					return;
				}
				decimal num7 = Convert.ToDecimal(TxtDiscount.Text) * 100m / Convert.ToDecimal(TxtTotals.Text);
				foreach (DataRow row2 in datatable.Rows)
				{
					if (row2["أسم_الوحدة"].ToString() != "")
					{
						decimal num8 = Math.Round((Convert.ToDecimal(row2["الاجمالى"].ToString()) - Convert.ToDecimal(row2["قيمة الخصم"].ToString())) / Convert.ToDecimal(TxtTotalAll.Text) * Convert.ToDecimal(TxtDiscount.Text), DecimalNum);
						row2["سعربعدالخصم"] = Convert.ToDecimal(row2["سعر الوحدة"].ToString()) - Convert.ToDecimal(num7 * Convert.ToDecimal(row2["سعر الوحدة"].ToString()) / 100m);
						decimal num9 = Convert.ToDecimal(row2["سعربعدالخصم"].ToString());
						decimal num10 = Convert.ToDecimal(row2["قيمة الخصم"]);
						decimal num11 = Math.Round(Convert.ToDecimal(row2["سعر الوحدة"].ToString()) * (Convert.ToDecimal(row2["الكمية"]) * (Convert.ToDecimal(row2["الطول"]) * Convert.ToDecimal(row2["العرض"]) * Convert.ToDecimal(row2["الإرتفاع"]))) - Convert.ToDecimal(row2["قيمة الخصم"].ToString()) - num8, DecimalNum);
						row2["إجمالى_بعدالخصم"] = num11;
						row2["الضريبه_بعدالخصم"] = Convert.ToDecimal(Convert.ToDecimal(row2["إجمالى_بعدالخصم"]) * Convert.ToDecimal(row2["قيمة_الضريبة"])) / 100m;
					}
				}
				decimal num12 = Convert.ToDecimal(datatable.Compute("SUM(الصافى)", ""));
				TxtTotals.Text = Convert.ToString(num12);
				decimal num13 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
				TxtVat.Text = Convert.ToString(num13);
				decimal num14 = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
				TxtTotalBeforDisc.Text = Convert.ToString(num14);
				decimal num15 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعدالخصم)", ""));
				TxtVatBeforeDisc.Text = Convert.ToString(num15 + num15);
				decimal num16 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
				TxtCost.Text = Convert.ToString(num16);
				TxtTotalAll.Text = Math.Round(num12, DecimalNum).ToString();
				TotalHasm = Convert.ToDecimal(TxtNet22.Text);
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
				decimal num17 = Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum);
				TxtTabocc.Text = num17.ToString();
				TxtAllVat.Text = (Convert.ToDecimal(TxtVatBeforeDisc.Text) + Convert.ToDecimal(TxtTabocc.Text)).ToString();
				TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text) + num17, DecimalNum)).ToString();
				return;
			}
			if (datatable.Rows.Count < 1)
			{
				TxtVat.Text = "0.000";
				TxtNet.Text = "0.00";
				TxtNet22.Text = "0.00";
				TxtTotals.Text = "0.00";
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtPhone.Text = "";
				TxtNumNote.Text = "";
				TxtTotalBeforDisc.Text = "0.00";
				TxtVatBeforeDisc.Text = "0.00";
				DiscountPercent = 0m;
				return;
			}
			if (TxtDiscount.Text == "" || TxtDiscount.Text == "0.00" || Convert.ToDecimal(TxtDiscount.Text) > Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtDiscountALLTotal.Text = "0.00";
				DiscountPercent = 0m;
				if (LoginDetails.DiscountDecimalForCustomer)
				{
					NumberDisc = 1;
					foreach (DataRow row3 in datatable.Rows)
					{
						if (row3["أسم_الوحدة"].ToString() != "")
						{
							row3["سعربعدالخصم"] = decimal.Round(Convert.ToDecimal(row3["سعر الوحدة"]), NumberDisc);
							row3["الضريبه_بعدالخصم"] = decimal.Round(Convert.ToDecimal(row3["الضربية"]), NumberDisc);
							if (((Convert.ToDecimal(row3["قيمة الخصم"]) == 0m || row3["قيمة الخصم"].ToString() == "") && Convert.ToDecimal(row3["نسبة الخصم"]) == 0m) || row3["نسبة الخصم"].ToString() == "")
							{
								row3["إجمالى_بعدالخصم"] = decimal.Round(Convert.ToDecimal(row3["الاجمالى"]), NumberDisc);
							}
							row3["الاجمالى"] = decimal.Round(Convert.ToDecimal(row3["الاجمالى"]), NumberDisc);
							row3["الصافى"] = decimal.Round(Convert.ToDecimal(row3["الصافى"]), NumberDisc);
							row3["إجمالى المبلغ"] = decimal.Round(Convert.ToDecimal(row3["الاجمالى"]) + Convert.ToDecimal(row3["الضربية"]), NumberDisc);
						}
					}
				}
				if (LoginDetails.DiscountDecimal)
				{
					NumberDisc = 0;
					foreach (DataRow row4 in datatable.Rows)
					{
						if (row4["أسم_الوحدة"].ToString() != "")
						{
							row4["سعربعدالخصم"] = decimal.Round(Convert.ToDecimal(row4["سعر الوحدة"]), NumberDisc);
							row4["الضريبه_بعدالخصم"] = decimal.Round(Convert.ToDecimal(row4["الضربية"]), NumberDisc);
							if (((Convert.ToDecimal(row4["قيمة الخصم"]) == 0m || row4["قيمة الخصم"].ToString() == "") && Convert.ToDecimal(row4["نسبة الخصم"]) == 0m) || row4["نسبة الخصم"].ToString() == "")
							{
								row4["إجمالى_بعدالخصم"] = decimal.Round(Convert.ToDecimal(row4["الاجمالى"]), NumberDisc);
							}
							row4["الاجمالى"] = decimal.Round(Convert.ToDecimal(row4["الاجمالى"]), NumberDisc);
							row4["الصافى"] = decimal.Round(Convert.ToDecimal(row4["الصافى"]), NumberDisc);
							row4["إجمالى المبلغ"] = decimal.Round(Convert.ToDecimal(row4["الاجمالى"]) + Convert.ToDecimal(row4["الضربية"]), NumberDisc);
						}
					}
				}
				if (LoginDetails.NoDiscountDecimal)
				{
					NumberDisc = 2;
					foreach (DataRow row5 in datatable.Rows)
					{
						if (row5["أسم_الوحدة"].ToString() != "")
						{
							row5["سعربعدالخصم"] = decimal.Round(Convert.ToDecimal(row5["سعر الوحدة"]), NumberDisc);
							row5["الضريبه_بعدالخصم"] = decimal.Round(Convert.ToDecimal(row5["الضربية"]), NumberDisc);
							if (((Convert.ToDecimal(row5["قيمة الخصم"]) == 0m || row5["قيمة الخصم"].ToString() == "") && Convert.ToDecimal(row5["نسبة الخصم"]) == 0m) || row5["نسبة الخصم"].ToString() == "")
							{
								row5["إجمالى_بعدالخصم"] = decimal.Round(Convert.ToDecimal(row5["الاجمالى"]), NumberDisc);
							}
							row5["الاجمالى"] = decimal.Round(Convert.ToDecimal(row5["الاجمالى"]), NumberDisc);
							row5["الصافى"] = decimal.Round(Convert.ToDecimal(row5["الصافى"]), NumberDisc);
							row5["إجمالى المبلغ"] = decimal.Round(Convert.ToDecimal(row5["الاجمالى"]) + Convert.ToDecimal(row5["الضربية"]), NumberDisc);
						}
					}
				}
				decimal num18 = Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", ""));
				TxtTotals.Text = Convert.ToString(num18);
				decimal num19 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
				TxtVat.Text = Convert.ToString(num19);
				decimal num20 = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
				TxtTotalBeforDisc.Text = Convert.ToString(num20);
				decimal num21 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعدالخصم)", ""));
				TxtVatBeforeDisc.Text = Convert.ToString(num21);
				decimal num22 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
				TxtCost.Text = Convert.ToString(num22);
				TxtTotalAll.Text = TxtTotalBeforDisc.Text;
				TotalHasm = Convert.ToDecimal(TxtNet22.Text);
				TxtNet.Text = Math.Round(num18 - Convert.ToDecimal(TxtDiscount.Text), DecimalNum).ToString();
				TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum)).ToString();
				return;
			}
			decimal num23 = ((!(DiscountPercent > 0m)) ? (Convert.ToDecimal(TxtDiscount.Text) * 100m / Convert.ToDecimal(TxtTotals.Text)) : DiscountPercent);
			foreach (DataRow row6 in datatable.Rows)
			{
				if (LoginDetails.DiscountDecimalForCustomer)
				{
					NumberDisc = 1;
					if (row6["أسم_الوحدة"].ToString() != "")
					{
						decimal num24 = Math.Round((Convert.ToDecimal(row6["الاجمالى"].ToString()) - Convert.ToDecimal(row6["قيمة الخصم"].ToString())) / Convert.ToDecimal(TxtTotalAll.Text) * Convert.ToDecimal(TxtDiscount.Text), DecimalNum);
						row6["سعربعدالخصم"] = decimal.Round(Convert.ToDecimal(row6["سعر الوحدة"].ToString()) - Convert.ToDecimal(num23 * Convert.ToDecimal(row6["سعر الوحدة"].ToString()) / 100m), NumberDisc);
						decimal d = Math.Round(Convert.ToDecimal(row6["سعر الوحدة"].ToString()) * (Convert.ToDecimal(row6["الكمية"]) * (Convert.ToDecimal(row6["الطول"]) * Convert.ToDecimal(row6["العرض"]) * Convert.ToDecimal(row6["الإرتفاع"]))) - Convert.ToDecimal(row6["قيمة الخصم"].ToString()) - num24, DecimalNum);
						row6["إجمالى_بعدالخصم"] = decimal.Round(d, NumberDisc);
						row6["الضريبه_بعدالخصم"] = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(row6["إجمالى_بعدالخصم"]) * Convert.ToDecimal(row6["قيمة_الضريبة"])) / 100m, NumberDisc);
						row6["الاجمالى"] = decimal.Round(Convert.ToDecimal(row6["الاجمالى"]), NumberDisc);
						row6["الصافى"] = decimal.Round(Convert.ToDecimal(row6["الصافى"]), NumberDisc);
						row6["إجمالى المبلغ"] = decimal.Round(Convert.ToDecimal(row6["الاجمالى"]) + Convert.ToDecimal(row6["الضربية"]), NumberDisc);
					}
				}
				if (LoginDetails.DiscountDecimal)
				{
					NumberDisc = 0;
					if (row6["أسم_الوحدة"].ToString() != "")
					{
						decimal num25 = Math.Round((Convert.ToDecimal(row6["الاجمالى"].ToString()) - Convert.ToDecimal(row6["قيمة الخصم"].ToString())) / Convert.ToDecimal(TxtTotalAll.Text) * Convert.ToDecimal(TxtDiscount.Text), DecimalNum);
						row6["سعربعدالخصم"] = decimal.Round(Convert.ToDecimal(row6["سعر الوحدة"].ToString()) - Convert.ToDecimal(num23 * Convert.ToDecimal(row6["سعر الوحدة"].ToString()) / 100m), NumberDisc);
						decimal d2 = Math.Round(Convert.ToDecimal(row6["سعر الوحدة"].ToString()) * (Convert.ToDecimal(row6["الكمية"]) * (Convert.ToDecimal(row6["الطول"]) * Convert.ToDecimal(row6["العرض"]) * Convert.ToDecimal(row6["الإرتفاع"]))) - Convert.ToDecimal(row6["قيمة الخصم"].ToString()) - num25, DecimalNum);
						row6["إجمالى_بعدالخصم"] = decimal.Round(d2, NumberDisc);
						row6["الضريبه_بعدالخصم"] = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(row6["إجمالى_بعدالخصم"]) * Convert.ToDecimal(row6["قيمة_الضريبة"])) / 100m, NumberDisc);
						row6["الاجمالى"] = decimal.Round(Convert.ToDecimal(row6["الاجمالى"]), NumberDisc);
						row6["الصافى"] = decimal.Round(Convert.ToDecimal(row6["الصافى"]), NumberDisc);
						row6["إجمالى المبلغ"] = decimal.Round(Convert.ToDecimal(row6["الاجمالى"]) + Convert.ToDecimal(row6["الضربية"]), NumberDisc);
					}
				}
				if (LoginDetails.NoDiscountDecimal)
				{
					NumberDisc = 2;
					if (row6["أسم_الوحدة"].ToString() != "")
					{
						decimal num26 = Math.Round((Convert.ToDecimal(row6["الاجمالى"].ToString()) - Convert.ToDecimal(row6["قيمة الخصم"].ToString())) / Convert.ToDecimal(TxtTotalAll.Text) * Convert.ToDecimal(TxtDiscount.Text), DecimalNum);
						row6["سعربعدالخصم"] = decimal.Round(Convert.ToDecimal(row6["سعر الوحدة"].ToString()) - Convert.ToDecimal(num23 * Convert.ToDecimal(row6["سعر الوحدة"].ToString()) / 100m), NumberDisc);
						decimal d3 = Math.Round(Convert.ToDecimal(row6["سعر الوحدة"].ToString()) * (Convert.ToDecimal(row6["الكمية"]) * (Convert.ToDecimal(row6["الطول"]) * Convert.ToDecimal(row6["العرض"]) * Convert.ToDecimal(row6["الإرتفاع"]))) - Convert.ToDecimal(row6["قيمة الخصم"].ToString()) - num26, DecimalNum);
						row6["إجمالى_بعدالخصم"] = decimal.Round(d3, NumberDisc);
						row6["الضريبه_بعدالخصم"] = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(row6["إجمالى_بعدالخصم"]) * Convert.ToDecimal(row6["قيمة_الضريبة"])) / 100m, NumberDisc);
						row6["الاجمالى"] = decimal.Round(Convert.ToDecimal(row6["الاجمالى"]), NumberDisc);
						row6["الصافى"] = decimal.Round(Convert.ToDecimal(row6["الصافى"]), NumberDisc);
						row6["إجمالى المبلغ"] = decimal.Round(Convert.ToDecimal(row6["الاجمالى"]) + Convert.ToDecimal(row6["الضربية"]), NumberDisc);
					}
				}
			}
			decimal num27 = Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", ""));
			TxtTotals.Text = Convert.ToString(num27);
			decimal num28 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
			TxtVat.Text = Convert.ToString(num28);
			decimal num29 = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
			TxtTotalBeforDisc.Text = Convert.ToString(num29);
			decimal num30 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعدالخصم)", ""));
			TxtVatBeforeDisc.Text = Convert.ToString(num30);
			decimal num31 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
			TxtCost.Text = Convert.ToString(num31);
			TxtTotalAll.Text = Math.Round(num27, DecimalNum).ToString();
			TotalHasm = Convert.ToDecimal(TxtNet22.Text);
			TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
			TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum)).ToString();
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
		}
	}

	private void Cal_FinalResultTotalsItemWithVat()
	{
		try
		{
			if (datatable.Rows.Count < 1)
			{
				TxtVat.Text = "0.000";
				TxtNet.Text = "0.00";
				TxtNet22.Text = "0.00";
				TxtTotals.Text = "0.00";
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtPhone.Text = "";
				TxtNumNote.Text = "";
				TxtTotalBeforDisc.Text = "0.00";
				TxtVatBeforeDisc.Text = "0.00";
				return;
			}
			if (TxtDiscount.Text == "" || TxtDiscount.Text == "0.00" || Convert.ToDecimal(TxtDiscount.Text) > Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtDiscountALLTotal.Text = "0.00";
				foreach (DataRow row in datatable.Rows)
				{
					if (row["أسم_الوحدة"].ToString() != "")
					{
						row["سعربعدالخصم"] = row["سعر الوحدة"];
						row["الضريبه_بعدالخصم"] = row["الضربية"];
						row["إجمالى_بعدالخصم"] = row["الاجمالى"];
						row["الصافى"] = row["إجمالى المبلغ"];
					}
				}
				decimal num = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
				TxtVat.Text = Convert.ToString(num);
				decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", ""));
				TxtTotals.Text = Convert.ToString(num2);
				decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعدالخصم)", ""));
				TxtVatBeforeDisc.Text = Convert.ToString(num3);
				decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(الصافى)", ""));
				TxtTotalBeforDisc.Text = Convert.ToString(num4 - num3);
				decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
				TxtCost.Text = Convert.ToString(num5);
				TxtTotalAll.Text = TxtTotalBeforDisc.Text;
				TotalHasm = Convert.ToDecimal(TxtNet22.Text);
				TxtNet.Text = Math.Round(num2, DecimalNum).ToString();
				TxtNet22.Text = Math.Round(num4, DecimalNum).ToString();
				return;
			}
			decimal num6 = Convert.ToDecimal(TxtDiscount.Text) * 100m / Convert.ToDecimal(TxtTotals.Text);
			foreach (DataRow row2 in datatable.Rows)
			{
				if (row2["أسم_الوحدة"].ToString() != "")
				{
					decimal num7 = Math.Round((Convert.ToDecimal(row2["الاجمالى"].ToString()) - Convert.ToDecimal(row2["قيمة الخصم"].ToString())) / Convert.ToDecimal(TxtTotalAll.Text) * Convert.ToDecimal(TxtDiscount.Text), DecimalNum);
					row2["سعربعدالخصم"] = Convert.ToDecimal(row2["سعر الوحدة"].ToString()) - Convert.ToDecimal(num6 * Convert.ToDecimal(row2["سعر الوحدة"].ToString()) / 100m);
					decimal num8 = Math.Round(Convert.ToDecimal(row2["سعر الوحدة"].ToString()) * (Convert.ToDecimal(row2["الكمية"]) * (Convert.ToDecimal(row2["الطول"]) * Convert.ToDecimal(row2["العرض"]) * Convert.ToDecimal(row2["الإرتفاع"]))) - Convert.ToDecimal(row2["قيمة الخصم"].ToString()) - num7, DecimalNum);
					row2["إجمالى_بعدالخصم"] = num8;
					decimal num9 = Convert.ToDecimal(row2["قيمة_الضريبة"]);
					row2["الضريبه_بعدالخصم"] = num8 * (Convert.ToDecimal(row2["قيمة_الضريبة"]) - 1m);
					row2["الصافى"] = Convert.ToDecimal(row2["إجمالى_بعدالخصم"]) + Convert.ToDecimal(row2["الضريبه_بعدالخصم"]);
				}
			}
			decimal num10 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
			TxtVat.Text = Convert.ToString(num10);
			decimal num11 = Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", ""));
			TxtTotals.Text = Convert.ToString(num11);
			decimal num12 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعدالخصم)", ""));
			TxtVatBeforeDisc.Text = Convert.ToString(num12);
			decimal num13 = Convert.ToDecimal(datatable.Compute("SUM(الصافى)", ""));
			TxtTotalBeforDisc.Text = Convert.ToString(num13 - num12);
			decimal num14 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
			TxtCost.Text = Convert.ToString(num14);
			TxtTotalAll.Text = Math.Round(num11, DecimalNum).ToString();
			TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
			TxtNet22.Text = Math.Round(num13, DecimalNum).ToString();
			TotalHasm = Convert.ToDecimal(TxtNet22.Text);
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
		}
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
				TxtQuntity.Text = FUNC_DecimalNumQty(getAllitems_Alternatives_Result.CurrentBalance).ToString();
				TxtAllQuantity.Text = FUNC_DecimalNumQty(getAllitems_Alternatives_Result.AllQuantity).ToString();
			}
			else
			{
				TxtQuntity.Text = FUNC_DecimalNumQty(0m).ToString();
				TxtAllQuantity.Text = FUNC_DecimalNumQty(0m).ToString();
			}
		}
		catch (Exception)
		{
		}
	}

	public void Percentdecimal()
	{
	}

	public void ArrangeDatatable()
	{
		int num = 1;
		foreach (DataRow row in datatable.Rows)
		{
			row["م"] = num;
			num++;
		}
	}

	public void openItmCard()
	{
		if (gridControl1.Focus())
		{
			if (CodeItm != "")
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
			CodeItm = "";
		}
		FrmItems frmItems = new FrmItems();
		frmItems.ShowDialog();
		LoginDetails.ItemSearchID = 0;
	}

	private void SearchOrderNum(int OrderId)
	{
		try
		{
			if (OrderId <= 0)
			{
				return;
			}
			Order_Orders order_Orders = ORD.GetPurchases(OrderId).FirstOrDefault();
			if (order_Orders == null)
			{
				return;
			}
			datatable.Rows.Clear();
			List<Order_OrdersDetails> itemsPurchesByID = ORD.GetItemsPurchesByID(OrderId);
			int num = 1;
			Code = Convert.ToInt32(order_Orders.PurBranchID);
			foreach (Order_OrdersDetails item in itemsPurchesByID)
			{
				DataRow dataRow = null;
				int num2 = Convert.ToInt32(item.ItemID);
				Item_Items ItemDetails = ORD.GetItemsByitemid(num2).FirstOrDefault();
				Printer printer = ORD.GetPrinter(num2);
				dataRow = datatable.NewRow();
				dataRow["ItemID"] = Convert.ToInt32(ItemDetails.ItemId);
				dataRow["ItemUnitID"] = Convert.ToInt32(ItemDetails.UnitSmall);
				dataRow["PrinterID"] = printer.ID;
				dataRow["PrinterName"] = printer.Name;
				dataRow["كود_الصنف"] = ItemDetails.Item_code;
				dataRow["أسم الصنف"] = ItemDetails.item_Name;
				dataRow["الاسم الانجليزي"] = ItemDetails.item_Name_English;
				dataRow["الصافى"] = FUNC_DecimalNum(item.TotalPrice);
				dataRow["المجموعة"] = ItemDetails.Category_ID;
				dataRow["وحدة التفقيط"] = db.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == ItemDetails.UnitTafqitID).Name;
				dataRow["كمية التفقيط"] = "1";
				dataRow["TafqitId"] = ItemDetails.UnitTafqitID;
				dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
				dataRow["أسم_الوحدة"] = item.ItemUnitType;
				dataRow["رقم المخزن"] = item.StoreID;
				dataRow["المخزن"] = (from x in db.Account_Stores
					where (int?)x.ID == item.StoreID
					select x.Store_Name).FirstOrDefault();
				dataRow["الكمية"] = FUNC_DecimalNumQty(Convert.ToDecimal(item.Quantity));
				dataRow["سعر الوحدة"] = FUNC_DecimalNum(item.UnitPrice);
				decimal? vAT;
				if (LoginDetails.ItemTotalWithVat)
				{
					vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						decimal value = FUNC_DecimalNum(ItemDetails.Tax_Value / (decimal?)100 + (decimal?)1);
						dataRow["OrginalPrice"] = FUNC_DecimalNum(item.UnitPrice * (decimal?)value);
					}
				}
				else
				{
					dataRow["OrginalPrice"] = FUNC_DecimalNum(item.UnitPrice);
				}
				dataRow["إجمالى_بعدالخصم"] = FUNC_DecimalNum(item.NetTotalPrice);
				dataRow["متوسط_التكلفة"] = FUNC_DecimalNum(item.LastCost);
				dataRow["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]));
				dataRow["الاجمالى"] = FUNC_DecimalNum(item.TotalPrice);
				dataRow["مسح"] = 0;
				dataRow["الطول"] = FUNC_DecimalNum(item.Long);
				dataRow["العرض"] = FUNC_DecimalNum(item.width);
				dataRow["الإرتفاع"] = FUNC_DecimalNum(item.Height);
				dataRow["الوزن"] = FUNC_DecimalNum(item.width);
				dataRow["قيمة الخصم"] = FUNC_DecimalNum(item.Amount_Discount);
				dataRow["نسبة الخصم"] = FUNC_DecimalNum(item.Per_Discount);
				dataRow["البونص"] = FUNC_DecimalNum(item.Bounce);
				vAT = item.VAT;
				if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
				{
					dataRow["الضربية"] = FUNC_DecimalNum(item.VAT);
					dataRow["إجمالى المبلغ"] = FUNC_DecimalNum(item.TotalPrice + item.VAT);
					dataRow["هل_ضريبة"] = 1;
					dataRow["قيمة_الضريبة"] = FUNC_DecimalNum(ItemDetails.Tax_Value / (decimal?)100 + (decimal?)1);
				}
				else
				{
					dataRow["هل_ضريبة"] = 0;
					dataRow["قيمة_الضريبة"] = 0;
					dataRow["الضربية"] = FUNC_DecimalNum(item.VAT);
					dataRow["إجمالى المبلغ"] = FUNC_DecimalNum(item.TotalPrice + item.VAT);
				}
				datatable.Rows.Add(dataRow);
				num++;
			}
			gridControl1.DataSource = datatable;
			if (order_Orders.BounceID.HasValue)
			{
				CBBounce.SelectedValue = order_Orders.BounceID;
			}
			TxtStreet.Text = order_Orders.Street;
			TxtCity.Text = order_Orders.City;
			TxtCountry.Text = order_Orders.Country;
			TxtPostalCode.Text = order_Orders.PostalCode;
			TxtAdditionalNum.Text = order_Orders.AdditionalNum;
			TxtCommercialRecord.Text = order_Orders.CommercialRecord;
			TxtVat.Text = order_Orders.SupplierVatNum;
			TxtSales.Text = order_Orders.SalesMan;
			PrintID = OrderId;
			TxtType.SelectedValue = order_Orders.Order_Paymant_Type;
			TxtCode.Text = order_Orders.PurBranchID.ToString();
			LblSup.Text = order_Orders.SupplierID.ToString();
			toggleHold.IsOn = Convert.ToBoolean(order_Orders.IsWaiting);
			TxtSuppliers.Text = order_Orders.SupplierName;
			TxtPhone.Text = order_Orders.SupplierPhone;
			TxtNumVat.Text = order_Orders.SupplierVatNum;
			TxtCommercialRecord.Text = order_Orders.CommercialRecord;
			TxtNote.Text = order_Orders.Note;
			TxtDate.DateTime = Convert.ToDateTime(order_Orders.Purchases_Date);
			TxtTotals.Text = Convert.ToString(order_Orders.TotalPrices);
			TxtVat.Text = Convert.ToString(order_Orders.Tax);
			TxtTotalAll.Text = order_Orders.Safy.ToString();
			TxtDiscount.Text = Convert.ToString(order_Orders.DiscountNum);
			txtdicoutprege.Text = Convert.ToString(order_Orders.DiscountPerantage);
			TxtTotalBeforDisc.Text = order_Orders.TotalPrices_Discount.ToString();
			TxtVatBeforeDisc.Text = order_Orders.Tax_Discount.ToString();
			TxtNet.Text = (order_Orders.TotalPrices_Discount + order_Orders.Tax_Discount).ToString();
			TxtNet22.Text = order_Orders.Net.ToString();
			TxtCost.Text = order_Orders.CostOrder.ToString();
			Cash = Convert.ToDecimal(order_Orders.CashMoney);
			Bank = Convert.ToDecimal(order_Orders.CashBank);
			toggleCashierType.IsOn = Convert.ToBoolean(order_Orders.OrderCashierType);
			TxtRoom.Value = Convert.ToInt32(order_Orders.RoomNum);
			TxtTable.Value = Convert.ToInt32(order_Orders.TableNum);
			TxtTabocc.Text = order_Orders.TobaccoTax.ToString();
			TxtAllVat.Text = order_Orders.AllTax.ToString();
			LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
			LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum);
			BtnSave.Enabled = false;
			BtnEdit.Enabled = true;
			if (LoginDetails.Lang == "ar-EG")
			{
				BtnEdit.Text = "تعديل F6";
			}
			else
			{
				BtnEdit.Text = "Edit F6";
			}
			BtnDelete.Enabled = true;
			TxtSuppliers.Focus();
			gridView1.OptionsBehavior.Editable = true;
			ArrangeDatatable();
			IsEnableFalse();
			Cal_FinalResultTotals();
			Charge(Convert.ToInt32(LblSup.Text));
		}
		catch (Exception)
		{
			Clear();
		}
	}

	public void GetPurchesOrder()
	{
		try
		{
			FrmSearchPriceOffer frmSearchPriceOffer = new FrmSearchPriceOffer();
			frmSearchPriceOffer.ShowDialog();
			if (frmSearchPriceOffer.CustCode > 0)
			{
				int custCode = frmSearchPriceOffer.CustCode;
				Order_PriceOffer order_PriceOffer = PurORD.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<Order_PriceOfferDetails> itemsPurchesByID = PurORD.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (Order_PriceOfferDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items ItemDetails = PurORD.GetItemsByitemid(itemID).FirstOrDefault();
					Printer printer = ORD.GetPrinter(ItemDetails.ItemId);
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = Convert.ToInt32(ItemDetails.ItemId);
					dataRow["ItemUnitID"] = Convert.ToInt32(ItemDetails.UnitSmall);
					dataRow["PrinterID"] = printer.ID;
					dataRow["PrinterName"] = printer.Name;
					dataRow["كود_الصنف"] = ItemDetails.Item_code;
					dataRow["أسم الصنف"] = ItemDetails.item_Name;
					dataRow["الاسم الانجليزي"] = ItemDetails.item_Name_English;
					dataRow["المجموعة"] = ItemDetails.Category_ID;
					dataRow["وحدة التفقيط"] = db.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == ItemDetails.UnitTafqitID).Name;
					dataRow["كمية التفقيط"] = "1";
					dataRow["TafqitId"] = ItemDetails.UnitTafqitID;
					dataRow["الطول"] = 1;
					dataRow["العرض"] = 1;
					dataRow["الإرتفاع"] = 1;
					dataRow["الوزن"] = 1;
					dataRow["البونص"] = 0;
					dataRow["قيمة الخصم"] = 0;
					dataRow["نسبة الخصم"] = 0;
					dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					dataRow["أسم_الوحدة"] = item.ItemUnitType;
					dataRow["رقم المخزن"] = item.StoreID;
					dataRow["المخزن"] = (from x in db.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["الكمية"] = FUNC_DecimalNumQty(Convert.ToDecimal(item.Quantity));
					dataRow["متوسط_التكلفة"] = FUNC_DecimalNum(ItemDetails.Average_cost);
					dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
					dataRow["سعر الوحدة"] = FUNC_DecimalNum(item.UnitPrice);
					dataRow["الاجمالى"] = FUNC_DecimalNum(item.TotalPrice);
					dataRow["مسح"] = 0;
					decimal num2 = Math.Round((Convert.ToDecimal(dataRow["الكمية"]) + Convert.ToDecimal(dataRow["البونص"])) * (Convert.ToDecimal(dataRow["الطول"]) * Convert.ToDecimal(dataRow["العرض"]) * Convert.ToDecimal(dataRow["الإرتفاع"])), DecimalNum);
					decimal num3 = Math.Round(Convert.ToDecimal(dataRow["الكمية"]) * (Convert.ToDecimal(dataRow["الطول"]) * Convert.ToDecimal(dataRow["العرض"]) * Convert.ToDecimal(dataRow["الإرتفاع"])), DecimalNum);
					decimal d = Convert.ToDecimal(dataRow["سعر الوحدة"]) * num3;
					dataRow["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * num2);
					dataRow["الاجمالى"] = Math.Round(d, DecimalNum);
					Vat(ItemDetails.Is_Tax, ItemDetails.Tax_Value, Convert.ToDecimal(dataRow["سعر الوحدة"]), Convert.ToDecimal(dataRow["الكمية"]), dataRow, "عرض سعر");
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				PrintID = custCode;
				TxtType.SelectedValue = order_PriceOffer.Sectoral;
				TxtCode.Text = "";
				LblSup.Text = order_PriceOffer.SupplierID.ToString();
				TxtSuppliers.Text = order_PriceOffer.SupplierName;
				TxtPhone.Text = order_PriceOffer.SupplierPhone;
				if (order_PriceOffer.SupplierID.HasValue)
				{
					Charge(Convert.ToInt32(order_PriceOffer.SupplierID));
				}
				TxtDate.DateTime = Convert.ToDateTime(order_PriceOffer.Purchases_Date);
				TxtTotals.Text = Convert.ToString(order_PriceOffer.TotalPrices);
				TxtVat.Text = Convert.ToString(order_PriceOffer.Tax);
				TxtTotalAll.Text = order_PriceOffer.Safy.ToString();
				TxtDiscount.Text = order_PriceOffer.DiscountNum.ToString();
				TxtTotalBeforDisc.Text = order_PriceOffer.Safy.ToString();
				TxtVatBeforeDisc.Text = order_PriceOffer.Tax_Discount.ToString();
				TxtNet.Text = order_PriceOffer.Net.ToString();
				TxtNet22.Text = order_PriceOffer.Net.ToString();
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnDelete.Enabled = false;
				gridView1.OptionsBehavior.Editable = true;
				ArrangeDatatable();
				IsEnableTrue();
				TxtSuppliers.Text = order_PriceOffer.SupplierName;
				TxtSuppliers.Focus();
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
			else if (text != "" && InsertNewItemMizan(text) == 0)
			{
				ItemMatchResult itemsOnly = ORD.GetItemsOnly(text);
				if (itemsOnly != null)
				{
					InsertNewItem(itemsOnly.Item.ItemId, text);
				}
				else
				{
					SoundPlayer soundPlayer = new SoundPlayer("SOUND38.WAV");
					soundPlayer.Play();
				}
			}
			TxtBarcode.Text = "";
			TxtBarcode.Focus();
		}
		catch (Exception)
		{
			TxtBarcode.Text = null;
		}
	}

	private void SetDateCasheir()
	{
		TxtDate.DateTime = DateTime.Now;
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
			string item_code = row["كود_الصنف"].ToString();
			if (row["أسم_الوحدة"].ToString() == "الصغرى")
			{
				decimal sellPermission = ORD.GetSellPermission(item_code, UserSellPrice, "الصغرى");
				if (UserSellPrice != 5 && Convert.ToDecimal(row["سعربعدالخصم"]) < sellPermission)
				{
					MessageBox.Show("عفوا\u064b غير مصرح لك بخصم اكثر من السعر المحدد لك صلاحيته", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					result = 0;
					TxtDiscount.Focus();
					break;
				}
			}
			else if (row["أسم_الوحدة"].ToString() == "المتوسطة")
			{
				decimal sellPermission2 = ORD.GetSellPermission(item_code, UserSellPrice, "المتوسطة");
				if (UserSellPrice != 5 && Convert.ToDecimal(row["سعربعدالخصم"]) < sellPermission2)
				{
					MessageBox.Show("عفوا\u064b غير مصرح لك بخصم اكثر من السعر المحدد لك صلاحيته", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					result = 0;
					TxtDiscount.Focus();
					break;
				}
			}
			else if (row["أسم_الوحدة"].ToString() == "الكبرى")
			{
				decimal sellPermission3 = ORD.GetSellPermission(item_code, UserSellPrice, "الكبرى");
				if (UserSellPrice != 5 && Convert.ToDecimal(row["سعربعدالخصم"]) < sellPermission3)
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

	public void FilterDataTableSave(DataTable ItemOrder, int Purchese_ID, bool IsWait)
	{
		int num = 1;
		dtCurrentTable.Clear();
		foreach (DataRow row in ItemOrder.Rows)
		{
			if (row["كود_الصنف"].ToString() != "")
			{
				dtCurrentTable.Rows.Add(num, Purchese_ID, Convert.ToInt32(row["ItemID"]), LoginDetails.BranchID, IsWait, Convert.ToInt32(row["رقم المخزن"]), Convert.ToString(row["ItemUnitID"]), Convert.ToDecimal(row["الكمية"]), Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]), DecimalNum), Convert.ToDecimal(row["سعر الوحدة"]), Math.Round(Convert.ToDecimal(row["سعر الوحدة"]), DecimalNum), Math.Round(Convert.ToDecimal(row["الاجمالى"]), DecimalNum), Math.Round(Convert.ToDecimal(row["الضربية"]), DecimalNum), Math.Round(Convert.ToDecimal(row["سعربعدالخصم"]), DecimalNum), Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"]), DecimalNum), Math.Round(Convert.ToDecimal(row["الضريبه_بعدالخصم"]), DecimalNum), row["أسم_الوحدة"].ToString(), 1, null, 1, 1, 1, 1, 1, 1, 1, Convert.ToDecimal(row["البونص"]), Convert.ToDecimal(row["قيمة الخصم"]), Convert.ToDecimal(row["نسبة الخصم"]), Convert.ToInt32(row["TafqitId"]), 0);
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
			Account_CustSup account_CustSup = gTSdbEntities.Account_CustSup.FirstOrDefault((Account_CustSup x) => x.ID == FrmID && x.FrmCust == (bool?)true);
			if (account_CustSup != null)
			{
				LblSup.Text = FrmID.ToString();
				LblSup.Text = account_CustSup.ID.ToString();
				TxtNumVat.Text = account_CustSup.VatNum.ToString();
				TxtSuppliers.Text = account_CustSup.CustSuppName;
				TxtPhone.Text = account_CustSup.Phone;
				int cusID = Convert.ToInt32(account_CustSup.SalesManID);
				TxtSales.Text = Cust.GetSalesMan(cusID);
				TxtAdditionalNum.Text = account_CustSup.AdditionalNum;
				TxtPostalCode.Text = account_CustSup.PostalCode;
				TxtCommercialRecord.Text = account_CustSup.CommercialRecord;
				TxtCity.Text = account_CustSup.City;
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

	private void Frms_Load(object sender, EventArgs e)
	{
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
	}

	private void Frms_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F2")
		{
			TxtBarcode.Text = "";
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
			GetPurchesOrder();
			return;
		}
		if (e.KeyCode.ToString() == "F8")
		{
			GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay frmOpenDay = new GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay();
			frmOpenDay.ShowDialog();
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
			}
			return;
		}
		if (e.KeyCode.ToString() == "F11")
		{
			try
			{
				int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				foreach (DataRow row in datatable.Rows)
				{
					if (row["م"].ToString() == Convert.ToString(num))
					{
						int num2 = Convert.ToInt32(row["ItemID"]);
						if (num2 > 0)
						{
							LoginDetails.ItemcodeForStore = num2;
							FrmSearchTransferAllBranch frmSearchTransferAllBranch = new FrmSearchTransferAllBranch();
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
			GridPlus();
		}
		else if (e.KeyCode == Keys.Subtract && e.Modifiers == Keys.Control)
		{
			GridPlus(1m, '-');
		}
		else if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
		{
			new FrmItemReader().Show();
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

	private void CombName_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void TxtType_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void TxtDiscountPrecent_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (ISEVENTDISC)
			{
				DiscountPercent = Convert.ToDecimal(txtdicoutprege.Text);
				if (txtdicoutprege.Text != "" || Convert.ToDecimal(txtdicoutprege.Text) != 0m || Convert.ToDecimal(txtdicoutprege.Text) > 100m)
				{
					decimal num = Math.Round(Convert.ToDecimal(txtdicoutprege.Text) * Convert.ToDecimal(TxtTotalAll.Text) / 100m, DecimalNum);
					TxtDiscount.Text = num.ToString();
				}
				else
				{
					txtdicoutprege.Text = "0.00";
				}
			}
			ISEVENTDISC = true;
		}
		catch (Exception)
		{
			txtdicoutprege.Text = "0.00";
		}
	}

	private void TxtDiscount_TextChanged(object sender, EventArgs e)
	{
		try
		{
			ISEVENTDISC = false;
			Cal_FinalResultTotals();
			decimal num = Math.Round(Convert.ToDecimal(TxtTotalAll.Text) - Convert.ToDecimal(TxtDiscount.Text), DecimalNum);
			TxtNet.Value = num;
			decimal num2 = Math.Round(Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotalAll.Text) * 100m, DecimalNum);
			txtdicoutprege.Value = num2;
			ISEVENTDISC = false;
			if (ISEVENTDISCTotal)
			{
				TxtDiscountALLTotal.Text = (Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text) - (Convert.ToDecimal(TxtTotalBeforDisc.Text) + Convert.ToDecimal(TxtVatBeforeDisc.Text))).ToString();
			}
			ISEVENTDISC = true;
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
		}
	}

	private void TxtDiscountALLTotal_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (ISEVENTDISC)
			{
				if (TxtDiscountALLTotal.Text != "" || Convert.ToDecimal(TxtDiscountALLTotal.Text) != 0m || Convert.ToDecimal(TxtDiscountALLTotal.Text) >= Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
				{
					decimal num = (Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text) - Convert.ToDecimal(TxtDiscountALLTotal.Text)) / (Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text));
					decimal num2 = Convert.ToDecimal(TxtTotals.Text) - num * Convert.ToDecimal(TxtTotals.Text);
					ISEVENTDISCTotal = false;
					TxtDiscount.Text = num2.ToString();
					ISEVENTDISCTotal = true;
				}
				else
				{
					TxtDiscountALLTotal.Text = "0.00";
				}
			}
			ISEVENTDISC = true;
		}
		catch (Exception)
		{
			TxtDiscountALLTotal.Text = "0.00";
		}
	}

	private void TxtTotals_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (LoginDetails.DiscountDecimalForCustomer)
			{
				TxtDiscount.Text = "0.00";
				decimal num = decimal.Round(Convert.ToDecimal(TxtNet.Text), 1);
				if (Convert.ToDecimal(TxtNet.Text) - num >= 0m)
				{
					TxtDiscount.Text = (Convert.ToDecimal(TxtNet.Text) - num).ToString();
				}
				else
				{
					decimal num2 = Math.Round(Convert.ToDecimal(TxtNet.Text) - Convert.ToDecimal(0.1), 1);
					TxtDiscount.Text = (Convert.ToDecimal(TxtNet.Text) - num2).ToString();
				}
			}
			if (LoginDetails.DiscountDecimal)
			{
				TxtDiscount.Text = "0.00";
				decimal num3 = Math.Floor(Convert.ToDecimal(TxtNet.Text));
				TxtDiscount.Text = (Convert.ToDecimal(TxtNet.Text) - num3).ToString();
			}
			if (LoginDetails.NoDiscountDecimal)
			{
				Cal_FinalResultTotals();
			}
		}
		catch (Exception)
		{
			TxtVat.Text = "0.00";
			TxtNet.Text = "0.00";
			TxtNet22.Text = "0.00";
			TxtTotals.Text = "0.00";
		}
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

	private void TxtDiscount_Click(object sender, EventArgs e)
	{
		try
		{
			Cal_FinalResultTotals();
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
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
			TxtSuppliers.Text = "";
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

	private void TxtNet_TextChanged(object sender, EventArgs e)
	{
		try
		{
			ToWord toWord = new ToWord(Convert.ToDecimal(TxtNet.Text), currencies[tafqitID]);
			txtWordAr = toWord.ConvertToArabic();
		}
		catch (Exception)
		{
		}
	}

	private void TxtTotalBeforDisc_TextChanged(object sender, EventArgs e)
	{
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

	private void MenuPrintFatoura_Click(object sender, EventArgs e)
	{
		Print(Code);
	}

	private void MenuPrintQued_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text != "")
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
				PreviewPrint(Code);
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

	private void spinEdit1_EditValueChanged(object sender, EventArgs e)
	{
	}

	private void simpleButton1_Click(object sender, EventArgs e)
	{
		Percentdecimal();
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
			if (datatable.Rows.Count > 0)
			{
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
			if (!IsClosingOk)
			{
				MessageBox.Show("عفوا، يوجد عمليات جارية الان \r\n حاول مرة اخري عند الانتهاء .", "مستكشف الاخطاء", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				e.Cancel = true;
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
		if (!string.IsNullOrWhiteSpace(BtnOrderSearch.Text))
		{
			SearchOrderNum(Convert.ToInt32(LblSup.Text));
		}
	}

	private void MenuPurchOrder_Click(object sender, EventArgs e)
	{
		GetPurchesOrder();
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
			SearchOrderNum(Convert.ToInt32(BtnOrderSearch.Text));
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

	private void TxtType_KeyPress_1(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void TxtDiscountALLTotal_TextChanged_1(object sender, EventArgs e)
	{
		try
		{
			if (ISEVENTDISC)
			{
				if (TxtDiscountALLTotal.Text != "" || Convert.ToDecimal(TxtDiscountALLTotal.Text) != 0m || Convert.ToDecimal(TxtDiscountALLTotal.Text) >= Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
				{
					decimal num = Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text);
					decimal num2 = num - Convert.ToDecimal(TxtDiscountALLTotal.Text);
					decimal num3 = Convert.ToDecimal(TxtTotals.Text);
					decimal num4 = num2 / num;
					decimal num5 = num3 * num4;
					decimal num6 = num3 - num5;
					ISEVENTDISCTotal = false;
					TxtDiscount.Text = num6.ToString();
					ISEVENTDISCTotal = true;
					TxtNet22.Text = (num - Convert.ToDecimal(TxtDiscountALLTotal.Text)).ToString();
				}
				else
				{
					TxtDiscountALLTotal.Text = "0.00";
				}
			}
			ISEVENTDISC = true;
		}
		catch (Exception)
		{
			TxtDiscountALLTotal.Text = "0.00";
		}
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

	private void HoldOpenClose_Toggled(object sender, EventArgs e)
	{
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
				if (frmCloseDay.IsClose)
				{
					return true;
				}
				return false;
			}
			GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay frmOpenDay = new GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay();
			frmOpenDay.ShowDialog();
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
		if (Convert.ToDouble(TxtNet22.Text) > 0.0)
		{
			TxtNet22.Text = (Convert.ToDouble(TxtNet22.Text) + Convert.ToDouble(CountZero())).ToString();
		}
	}

	private void BtnMinNet_Click(object sender, EventArgs e)
	{
		if (Convert.ToDouble(TxtNet22.Text) > 0.0)
		{
			TxtNet22.Text = (Convert.ToDouble(TxtNet22.Text) - Convert.ToDouble(CountZero())).ToString();
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

	private void TxtDiscountALLTotal_Click(object sender, EventArgs e)
	{
	}

	private void tileControl1_Click(object sender, EventArgs e)
	{
	}

	private void TxtDiscount_ValueChanged(object sender, EventArgs e)
	{
		DiscountPercent = 0m;
	}

	private void Vat(bool? Is_Tax, decimal? Tax_Value, decimal? Unitprice, decimal AllQuatinty, DataRow dr, string ColumnField)
	{
		if (ORD.IsVatActive())
		{
			if (Is_Tax == true)
			{
				dr["الضربية"] = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty * Tax_Value / (decimal?)100);
				dr["إجمالى المبلغ"] = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty * Tax_Value / (decimal?)100 + Unitprice * (decimal?)AllQuatinty);
				dr["هل_ضريبة"] = 1;
				dr["قيمة_الضريبة"] = Tax_Value;
			}
			else
			{
				dr["الضربية"] = 0;
				dr["إجمالى المبلغ"] = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty);
				dr["هل_ضريبة"] = 0;
				dr["قيمة_الضريبة"] = 0;
			}
		}
		else
		{
			dr["الضربية"] = 0;
			dr["إجمالى المبلغ"] = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty - (decimal?)Convert.ToDecimal(dr["قيمة الخصم"]));
			dr["هل_ضريبة"] = 0;
			dr["قيمة_الضريبة"] = 0;
		}
		decimal num = Convert.ToDecimal(dr["نسبة الخصم"]);
		if (ColumnField == "نسبة الخصم" || num != 0m)
		{
			if (num <= 100m && num != 0m)
			{
				decimal num2 = FUNC_DecimalNum(Convert.ToDecimal(dr["نسبة الخصم"]) * Convert.ToDecimal(dr["الاجمالى"]) / 100m);
				dr["قيمة الخصم"] = num2;
				dr["إجمالى_بعدالخصم"] = Math.Round(Convert.ToDecimal(dr["الاجمالى"]) - num2, DecimalNum);
				dr["الصافى"] = Math.Round(Convert.ToDecimal(dr["الاجمالى"]) - num2, DecimalNum);
			}
			else
			{
				dr["نسبة الخصم"] = "0.00";
				dr["قيمة الخصم"] = "0.00";
				dr["إجمالى_بعدالخصم"] = dr["الاجمالى"];
				dr["الصافى"] = dr["الاجمالى"];
			}
		}
		else if (Convert.ToDecimal(dr["قيمة الخصم"]) <= Convert.ToDecimal(dr["الاجمالى"]) && Convert.ToDecimal(dr["قيمة الخصم"]) != 0m)
		{
			decimal num3 = Convert.ToDecimal(dr["الاجمالى"]);
			dr["إجمالى_بعدالخصم"] = Math.Round(Convert.ToDecimal(dr["الاجمالى"]) - Convert.ToDecimal(dr["قيمة الخصم"]), DecimalNum).ToString();
			dr["الصافى"] = Math.Round(Convert.ToDecimal(dr["الاجمالى"]) - Convert.ToDecimal(dr["قيمة الخصم"]), DecimalNum);
			decimal num4 = Math.Round(Convert.ToDecimal(dr["قيمة الخصم"]) / Convert.ToDecimal(dr["الاجمالى"]) * 100m, DecimalNum);
			dr["نسبة الخصم"] = num4;
		}
		else
		{
			dr["نسبة الخصم"] = "0.00";
			dr["قيمة الخصم"] = "0.00";
			dr["إجمالى_بعدالخصم"] = dr["الاجمالى"];
			dr["الصافى"] = dr["الاجمالى"];
		}
	}

	private void ValueChangeDGV(string TxtChange, CellValueChangedEventArgs e)
	{
		decimal num = Convert.ToDecimal(e.Value);
		string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
		string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
		string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
		string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("م");
		if (!(focusedRowCellDisplayText != ""))
		{
			return;
		}
		Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(focusedRowCellDisplayText).FirstOrDefault();
		if (get_Item_ByCodeALL_Result == null)
		{
			return;
		}
		foreach (DataRow row in datatable.Rows)
		{
			if (!(row["م"].ToString() == focusedRowCellDisplayText4))
			{
				continue;
			}
			decimal num2 = Convert.ToDecimal(row["سعر الوحدة"].ToString());
			row[TxtChange] = num;
			decimal num3 = Math.Round((Convert.ToDecimal(row["الكمية"]) + Convert.ToDecimal(row["البونص"])) * (Convert.ToDecimal(row["الطول"]) * Convert.ToDecimal(row["العرض"]) * Convert.ToDecimal(row["الإرتفاع"])), DecimalNum);
			decimal num4 = Math.Round(Convert.ToDecimal(row["الكمية"]) * (Convert.ToDecimal(row["الطول"]) * Convert.ToDecimal(row["العرض"]) * Convert.ToDecimal(row["الإرتفاع"])), DecimalNum);
			if (row["أسم_الوحدة"].ToString() == "الصغرى")
			{
				row["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row["متوسط_التكلفة"]) * num3);
				row["الاجمالى"] = Math.Round(num2 * num4, DecimalNum);
				if (TxtChange == "قيمة الخصم")
				{
					row["نسبة الخصم"] = "0";
				}
				Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, num2, num4, row, TxtChange);
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
				{
					row["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitSmallID);
					row["كمية التفقيط"] = FUNC_DecimalNum(row["الكمية"].ToString());
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
				{
					row["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitMediumID);
					decimal value = 1;
					decimal? convertMediumUnit = get_Item_ByCodeALL_Result.ConvertMediumUnit;
					row["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(row["الكمية"]));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
				{
					row["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitLargeID);
					decimal value = 1;
					decimal? convertMediumUnit = get_Item_ByCodeALL_Result.ConvertBigUnit;
					row["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(row["الكمية"]));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
				}
				break;
			}
			if (row["أسم_الوحدة"].ToString() == "المتوسطة")
			{
				decimal num5 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertMediumUnit);
				row["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row["متوسط_التكلفة"]) * (num3 * num5));
				row["الاجمالى"] = FUNC_DecimalNum(num2 * num4);
				if (TxtChange == "قيمة الخصم")
				{
					row["نسبة الخصم"] = "0";
				}
				Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, num2, num4, row, TxtChange);
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
				{
					int value2 = Convert.ToInt32(get_Item_ByCodeALL_Result.UnitSmallID);
					row["وحدة التفقيط"] = ITM.GetUnitName(value2);
					row["كمية التفقيط"] = FUNC_DecimalNum(row["الكمية"].ToString());
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
				{
					row["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitMedium;
					row["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.ConvertMediumUnit / get_Item_ByCodeALL_Result.ConvertMediumUnit * (decimal?)Convert.ToDecimal(row["الكمية"]));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
				{
					row["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitLarge;
					row["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.ConvertMediumUnit / get_Item_ByCodeALL_Result.ConvertBigUnit * (decimal?)Convert.ToDecimal(row["الكمية"]));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
				}
				break;
			}
			if (row["أسم_الوحدة"].ToString() == "الكبرى")
			{
				decimal num6 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertBigUnit);
				row["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row["متوسط_التكلفة"]) * (num3 * num6));
				row["الاجمالى"] = Math.Round(num2 * num4, DecimalNum);
				if (TxtChange == "قيمة الخصم")
				{
					row["نسبة الخصم"] = "0";
				}
				Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, num2, num4, row, TxtChange);
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
				{
					int value3 = Convert.ToInt32(get_Item_ByCodeALL_Result.UnitSmallID);
					row["وحدة التفقيط"] = ITM.GetUnitName(value3);
					row["كمية التفقيط"] = FUNC_DecimalNum(row["الكمية"].ToString());
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
				{
					row["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitMedium;
					row["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.ConvertBigUnit / get_Item_ByCodeALL_Result.ConvertMediumUnit * (decimal?)Convert.ToDecimal(row["الكمية"]));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
				{
					row["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitLarge;
					row["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.ConvertBigUnit / get_Item_ByCodeALL_Result.ConvertBigUnit * (decimal?)Convert.ToDecimal(row["الكمية"]));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
				}
				break;
			}
		}
		CheckFfistRow();
		insertnewrow();
		GetTotalCost();
		gridView1.FocusedRowHandle = e.RowHandle;
		gridView1.FocusedColumn = gridView1.VisibleColumns[2];
		gridView1.ShowEditor();
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
			}
			TxtBarcode.Focus();
		}
		catch (Exception)
		{
		}
	}

	private decimal FUNC_DecimalNum(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNum);
	}

	private decimal FUNC_DecimalNum(string Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNum);
	}

	private decimal FUNC_DecimalNumQty(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNumQty);
	}

	private decimal FUNC_DecimalNumQty(string Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNumQty);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmCashirWithOutGroup));
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
		this.TxtNet22 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
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
		this.TxtDiscountALLTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtAllTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTable = new System.Windows.Forms.NumericUpDown();
		this.toggleSectoral = new DevExpress.XtraEditors.ToggleSwitch();
		this.toggleCashierType = new DevExpress.XtraEditors.ToggleSwitch();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtRoom = new System.Windows.Forms.NumericUpDown();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label2 = new System.Windows.Forms.Label();
		this.lblRoom = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.lblTable = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.TxtAllVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtTabocc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtTotalBeforDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtTotalAll = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatBeforeDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnMinus = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPlus = new DevExpress.XtraEditors.SimpleButton();
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
		this.الكمياتToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.الكميةالحاليةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.كلالكمياتبالمستودعاتToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.كلالكمياتبالفروعToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.تخصيصToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
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
		this.repositoryItemButtonEdit4Units = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEditStore = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
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
		this.LblCustLast = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.DateHold = new DevExpress.XtraEditors.DateEdit();
		this.label28 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.BtnRecipt = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.BtnCustomerSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnNewCustomer = new DevExpress.XtraEditors.SimpleButton();
		this.label22 = new System.Windows.Forms.Label();
		this.label34 = new System.Windows.Forms.Label();
		this.label38 = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.label42 = new System.Windows.Forms.Label();
		this.label43 = new System.Windows.Forms.Label();
		this.TxtAdditionalNum = new DevExpress.XtraEditors.TextEdit();
		this.TxtStreet = new DevExpress.XtraEditors.TextEdit();
		this.TxtDistrict = new DevExpress.XtraEditors.TextEdit();
		this.TxtCommercialRecord = new DevExpress.XtraEditors.TextEdit();
		this.TxtPostalCode = new DevExpress.XtraEditors.TextEdit();
		this.TxtCountry = new DevExpress.XtraEditors.TextEdit();
		this.TxtCity = new DevExpress.XtraEditors.TextEdit();
		this.label6 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label37 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label46 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.TxtSales = new DevExpress.XtraEditors.TextEdit();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtNumVat = new DevExpress.XtraEditors.TextEdit();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtQuntity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label16 = new System.Windows.Forms.Label();
		this.LblNowQuintity = new System.Windows.Forms.Label();
		this.TxtAllQuantity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnCheckOut = new DevExpress.XtraEditors.SimpleButton();
		this.BtnMinNet = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPlusNet = new DevExpress.XtraEditors.SimpleButton();
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
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4Units).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
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
		((System.ComponentModel.ISupportInitialize)this.TxtStreet.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDistrict.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCountry.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.TxtNet22, "TxtNet22");
		this.TxtNet22.DecimalDigits = 3;
		this.TxtNet22.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet22.Name = "TxtNet22";
		this.TxtNet22.ReadOnly = true;
		this.TxtNet22.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet22.UseCompatibleTextRendering = false;
		this.TxtNet22.Value = new decimal(new int[4]);
		this.TxtNet22.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.txtdicoutprege.TextChanged += new System.EventHandler(TxtDiscountPrecent_TextChanged);
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
		this.TxtDiscount.ValueChanged += new System.EventHandler(TxtDiscount_ValueChanged);
		this.TxtDiscount.TextChanged += new System.EventHandler(TxtDiscount_TextChanged);
		resources.ApplyResources(this.TxtDiscountALLTotal, "TxtDiscountALLTotal");
		this.TxtDiscountALLTotal.DecimalDigits = 3;
		this.TxtDiscountALLTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscountALLTotal.Name = "TxtDiscountALLTotal";
		this.TxtDiscountALLTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscountALLTotal.Value = new decimal(new int[4]);
		this.TxtDiscountALLTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscountALLTotal.TextChanged += new System.EventHandler(TxtDiscountALLTotal_TextChanged_1);
		this.TxtDiscountALLTotal.Click += new System.EventHandler(TxtDiscountALLTotal_Click);
		resources.ApplyResources(this.TxtAllTotal, "TxtAllTotal");
		this.TxtAllTotal.DecimalDigits = 3;
		this.TxtAllTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtAllTotal.Name = "TxtAllTotal";
		this.TxtAllTotal.ReadOnly = true;
		this.TxtAllTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllTotal.Value = new decimal(new int[4]);
		this.TxtAllTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		resources.ApplyResources(this.TxtNet, "TxtNet");
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet.TextChanged += new System.EventHandler(TxtNet_TextChanged);
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
		resources.ApplyResources(this.TxtAllVat, "TxtAllVat");
		this.TxtAllVat.DecimalDigits = 3;
		this.TxtAllVat.ForeColor = System.Drawing.Color.Black;
		this.TxtAllVat.Name = "TxtAllVat";
		this.TxtAllVat.ReadOnly = true;
		this.TxtAllVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllVat.Value = new decimal(new int[4]);
		this.TxtAllVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		resources.ApplyResources(this.TxtTotalBeforDisc, "TxtTotalBeforDisc");
		this.TxtTotalBeforDisc.DecimalDigits = 3;
		this.TxtTotalBeforDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalBeforDisc.Name = "TxtTotalBeforDisc";
		this.TxtTotalBeforDisc.ReadOnly = true;
		this.TxtTotalBeforDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalBeforDisc.Value = new decimal(new int[4]);
		this.TxtTotalBeforDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalBeforDisc.TextChanged += new System.EventHandler(TxtTotalBeforDisc_TextChanged);
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.TxtTotalAll, "TxtTotalAll");
		this.TxtTotalAll.DecimalDigits = 3;
		this.TxtTotalAll.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalAll.Name = "TxtTotalAll";
		this.TxtTotalAll.ReadOnly = true;
		this.TxtTotalAll.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalAll.Value = new decimal(new int[4]);
		this.TxtTotalAll.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalAll.TextChanged += new System.EventHandler(TxtTotals_TextChanged);
		resources.ApplyResources(this.TxtVatBeforeDisc, "TxtVatBeforeDisc");
		this.TxtVatBeforeDisc.DecimalDigits = 3;
		this.TxtVatBeforeDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatBeforeDisc.Name = "TxtVatBeforeDisc";
		this.TxtVatBeforeDisc.ReadOnly = true;
		this.TxtVatBeforeDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatBeforeDisc.Value = new decimal(new int[4]);
		this.TxtVatBeforeDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtVat, "TxtVat");
		this.TxtVat.DecimalDigits = 3;
		this.TxtVat.ForeColor = System.Drawing.Color.Teal;
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.ReadOnly = true;
		this.TxtVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4]);
		this.TxtVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtTotals, "TxtTotals");
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.ReadOnly = true;
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.BtnMinus, "BtnMinus");
		this.BtnMinus.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnMinus.Appearance.Font");
		this.BtnMinus.Appearance.Options.UseFont = true;
		this.BtnMinus.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnMinus.ImageOptions.Image");
		this.BtnMinus.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnMinus.Name = "BtnMinus";
		this.BtnMinus.Click += new System.EventHandler(BtnMinus_Click);
		resources.ApplyResources(this.BtnPlus, "BtnPlus");
		this.BtnPlus.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPlus.Appearance.Font");
		this.BtnPlus.Appearance.Options.UseFont = true;
		this.BtnPlus.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPlus.ImageOptions.Image");
		this.BtnPlus.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPlus.Name = "BtnPlus";
		this.BtnPlus.Click += new System.EventHandler(BtnPlus_Click);
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
		this.استعلامToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[14]
		{
			this.رقمالفاتورةToolStripMenuItem1, this.اسمالموردToolStripMenuItem1, this.نوعالفاتورةToolStripMenuItem1, this.f5حفظالفاتورةToolStripMenuItem, this.f6تعديلالفاتورةToolStripMenuItem, this.f7حذفالفاتورةToolStripMenuItem, this.f9بحثفيعروضالاسعارToolStripMenuItem, this.f12فتحكرتالصنفToolStripMenuItem, this.ctrlDeleteحذفالسطرالحاليToolStripMenuItem, this.ctrlToolStripMenuItem,
			this.ctrlنقصانكميةالسطرالحاليToolStripMenuItem, this.ctrlPطباعةالفاتورةToolStripMenuItem, this.ctrlLقفلمؤقتToolStripMenuItem, this.f11اظهارالكمياتلكلالمخازنToolStripMenuItem
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
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[10] { this.repositoryItemButtonEdit1, this.repositoryItemButtonEdit2, this.repositoryItemTextEdit1, this.repositoryItemTextEdit2, this.repositoryItemDateEdit1, this.repositoryItemTextEdit3, this.repositoryItemPictureEdit1, this.repositoryItemButtonEdit3, this.repositoryItemButtonEdit4Units, this.repositoryItemButtonEditStore });
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridControl1.Click += new System.EventHandler(gridControl1_Click);
		this.gridView1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView1.Appearance.HeaderPanel.Font");
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		resources.ApplyResources(this.gridView1, "gridView1");
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[29]
		{
			this.gridColumn24, this.gridColumn23, this.gridColumn1, this.gridColumn2, this.gridColumn26, this.shearch, this.gridColumn19, this.gridColumn25, this.gridColumn20, this.gridColumn21,
			this.gridColumn3, this.gridColumn6, this.gridColumn7, this.gridColumn22, this.gridColumn5, this.gridColumn8, this.gridColumn9, this.gridColumn27, this.gridColumn28, this.gridColumn29,
			this.gridColumn30, this.gridColumn31, this.gridColumn32, this.gridColumn33, this.gridColumn4, this.gridColumn34, this.gridColumn35, this.gridColumn36, this.gridColumn37
		});
		this.gridView1.DetailHeight = 288;
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
		this.gridColumn24.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceHeader.Font");
		this.gridColumn24.AppearanceHeader.Options.UseFont = true;
		this.gridColumn24.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.FieldName = "الضريبه_بعدالخصم";
		this.gridColumn24.MinWidth = 17;
		this.gridColumn24.Name = "gridColumn24";
		resources.ApplyResources(this.gridColumn23, "gridColumn23");
		this.gridColumn23.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceCell.Font");
		this.gridColumn23.AppearanceCell.Options.UseFont = true;
		this.gridColumn23.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceHeader.Font");
		this.gridColumn23.AppearanceHeader.Options.UseFont = true;
		this.gridColumn23.FieldName = "إجمالى_بعدالخصم";
		this.gridColumn23.MinWidth = 17;
		this.gridColumn23.Name = "gridColumn23";
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
		this.gridColumn1.FieldName = "كود_الصنف";
		this.gridColumn1.MinWidth = 17;
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.OptionsColumn.AllowEdit = false;
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
		this.gridColumn2.MinWidth = 17;
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		resources.ApplyResources(this.gridColumn26, "gridColumn26");
		this.gridColumn26.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceCell.Font");
		this.gridColumn26.AppearanceCell.Options.UseFont = true;
		this.gridColumn26.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceHeader.Font");
		this.gridColumn26.AppearanceHeader.Options.UseFont = true;
		this.gridColumn26.FieldName = "المجموعة";
		this.gridColumn26.MinWidth = 17;
		this.gridColumn26.Name = "gridColumn26";
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
		this.shearch.FieldName = "الوحدة";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.MinWidth = 17;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.repositoryItemButtonEdit4Units, "repositoryItemButtonEdit4Units");
		this.repositoryItemButtonEdit4Units.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4Units.Buttons"))
		});
		this.repositoryItemButtonEdit4Units.Name = "repositoryItemButtonEdit4Units";
		this.repositoryItemButtonEdit4Units.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit4Units_ButtonClick);
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceCell.Font");
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.FieldName = "أسم_الوحدة";
		this.gridColumn19.MinWidth = 17;
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn25, "gridColumn25");
		this.gridColumn25.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceCell.Font");
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceHeader.Font");
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.FieldName = "متوسط_التكلفة";
		this.gridColumn25.MinWidth = 17;
		this.gridColumn25.Name = "gridColumn25";
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
		this.gridColumn20.FieldName = "الكمية";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.MinWidth = 17;
		this.gridColumn20.Name = "gridColumn20";
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
		this.gridColumn21.FieldName = "سعر الوحدة";
		this.gridColumn21.MinWidth = 17;
		this.gridColumn21.Name = "gridColumn21";
		resources.ApplyResources(this.gridColumn3, "gridColumn3");
		this.gridColumn3.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceCell.Font");
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceHeader.Font");
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.FieldName = "الاجمالى";
		this.gridColumn3.MinWidth = 17;
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
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
		this.gridColumn6.FieldName = "الضربية";
		this.gridColumn6.MinWidth = 17;
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.ReadOnly = true;
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
		this.gridColumn7.FieldName = "إجمالى المبلغ";
		this.gridColumn7.MinWidth = 17;
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.OptionsColumn.ReadOnly = true;
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
		this.gridColumn22.MinWidth = 17;
		this.gridColumn22.Name = "gridColumn22";
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		editorButtonImageOptions.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonPressed);
		resources.ApplyResources(this.gridColumn5, "gridColumn5");
		this.gridColumn5.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceCell.Font");
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceHeader.Font");
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.ColumnEdit = this.repositoryItemButtonEdit3;
		this.gridColumn5.FieldName = "نقصان";
		this.gridColumn5.MinWidth = 17;
		this.gridColumn5.Name = "gridColumn5";
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"), resources.GetString("repositoryItemButtonEdit3.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit3.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEdit3.Buttons6"), resources.GetObject("repositoryItemButtonEdit3.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit3.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit3.Buttons9"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
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
		this.gridColumn8.MinWidth = 17;
		this.gridColumn8.Name = "gridColumn8";
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonClick);
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
		this.gridColumn9.FieldName = "م";
		this.gridColumn9.MinWidth = 17;
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		resources.ApplyResources(this.gridColumn27, "gridColumn27");
		this.gridColumn27.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceCell.Font");
		this.gridColumn27.AppearanceCell.Options.UseFont = true;
		this.gridColumn27.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceHeader.Font");
		this.gridColumn27.AppearanceHeader.Options.UseFont = true;
		this.gridColumn27.FieldName = "اجمالي_متوسط_التكلفة";
		this.gridColumn27.MinWidth = 17;
		this.gridColumn27.Name = "gridColumn27";
		resources.ApplyResources(this.gridColumn28, "gridColumn28");
		this.gridColumn28.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn28.AppearanceCell.Font");
		this.gridColumn28.AppearanceCell.Options.UseFont = true;
		this.gridColumn28.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn28.AppearanceHeader.Font");
		this.gridColumn28.AppearanceHeader.Options.UseFont = true;
		this.gridColumn28.FieldName = "الطول";
		this.gridColumn28.MinWidth = 17;
		this.gridColumn28.Name = "gridColumn28";
		resources.ApplyResources(this.gridColumn29, "gridColumn29");
		this.gridColumn29.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceCell.Font");
		this.gridColumn29.AppearanceCell.Options.UseFont = true;
		this.gridColumn29.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceHeader.Font");
		this.gridColumn29.AppearanceHeader.Options.UseFont = true;
		this.gridColumn29.FieldName = "العرض";
		this.gridColumn29.MinWidth = 17;
		this.gridColumn29.Name = "gridColumn29";
		resources.ApplyResources(this.gridColumn30, "gridColumn30");
		this.gridColumn30.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn30.AppearanceCell.Font");
		this.gridColumn30.AppearanceCell.Options.UseFont = true;
		this.gridColumn30.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn30.AppearanceHeader.Font");
		this.gridColumn30.AppearanceHeader.Options.UseFont = true;
		this.gridColumn30.FieldName = "الإرتفاع";
		this.gridColumn30.MinWidth = 17;
		this.gridColumn30.Name = "gridColumn30";
		resources.ApplyResources(this.gridColumn31, "gridColumn31");
		this.gridColumn31.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn31.AppearanceCell.Font");
		this.gridColumn31.AppearanceCell.Options.UseFont = true;
		this.gridColumn31.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn31.AppearanceHeader.Font");
		this.gridColumn31.AppearanceHeader.Options.UseFont = true;
		this.gridColumn31.FieldName = "الوزن";
		this.gridColumn31.MinWidth = 17;
		this.gridColumn31.Name = "gridColumn31";
		resources.ApplyResources(this.gridColumn32, "gridColumn32");
		this.gridColumn32.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn32.AppearanceCell.Font");
		this.gridColumn32.AppearanceCell.Options.UseFont = true;
		this.gridColumn32.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn32.AppearanceHeader.Font");
		this.gridColumn32.AppearanceHeader.Options.UseFont = true;
		this.gridColumn32.FieldName = "قيمة الخصم";
		this.gridColumn32.MinWidth = 17;
		this.gridColumn32.Name = "gridColumn32";
		resources.ApplyResources(this.gridColumn33, "gridColumn33");
		this.gridColumn33.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn33.AppearanceCell.Font");
		this.gridColumn33.AppearanceCell.Options.UseFont = true;
		this.gridColumn33.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn33.AppearanceHeader.Font");
		this.gridColumn33.AppearanceHeader.Options.UseFont = true;
		this.gridColumn33.FieldName = "نسبة الخصم";
		this.gridColumn33.MinWidth = 17;
		this.gridColumn33.Name = "gridColumn33";
		resources.ApplyResources(this.gridColumn4, "gridColumn4");
		this.gridColumn4.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceCell.Font");
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceHeader.Font");
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.ColumnEdit = this.repositoryItemButtonEditStore;
		this.gridColumn4.FieldName = "المخزن";
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.repositoryItemButtonEditStore.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditStore.Buttons"), resources.GetString("repositoryItemButtonEditStore.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditStore.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditStore.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditStore.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditStore.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonEditStore.Buttons6"), resources.GetObject("repositoryItemButtonEditStore.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditStore.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditStore.Buttons9"))
		});
		this.repositoryItemButtonEditStore.Name = "repositoryItemButtonEditStore";
		this.repositoryItemButtonEditStore.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEditStore_ButtonClick);
		resources.ApplyResources(this.gridColumn34, "gridColumn34");
		this.gridColumn34.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn34.AppearanceCell.Font");
		this.gridColumn34.AppearanceCell.Options.UseFont = true;
		this.gridColumn34.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn34.AppearanceHeader.Font");
		this.gridColumn34.AppearanceHeader.Options.UseFont = true;
		this.gridColumn34.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn34.FieldName = "الصافى";
		this.gridColumn34.Name = "gridColumn34";
		this.gridColumn34.OptionsColumn.AllowEdit = false;
		this.gridColumn34.OptionsColumn.AllowFocus = false;
		resources.ApplyResources(this.gridColumn35, "gridColumn35");
		this.gridColumn35.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn35.AppearanceCell.Font");
		this.gridColumn35.AppearanceCell.Options.UseFont = true;
		this.gridColumn35.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn35.AppearanceHeader.Font");
		this.gridColumn35.AppearanceHeader.Options.UseFont = true;
		this.gridColumn35.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn35.FieldName = "البونص";
		this.gridColumn35.Name = "gridColumn35";
		resources.ApplyResources(this.gridColumn36, "gridColumn36");
		this.gridColumn36.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceCell.Font");
		this.gridColumn36.AppearanceCell.Options.UseFont = true;
		this.gridColumn36.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceHeader.Font");
		this.gridColumn36.AppearanceHeader.Options.UseFont = true;
		this.gridColumn36.FieldName = "ItemID";
		this.gridColumn36.Name = "gridColumn36";
		this.gridColumn36.OptionsColumn.AllowEdit = false;
		this.gridColumn36.OptionsColumn.AllowFocus = false;
		this.gridColumn36.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn37, "gridColumn37");
		this.gridColumn37.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn37.AppearanceCell.Font");
		this.gridColumn37.AppearanceCell.Options.UseFont = true;
		this.gridColumn37.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn37.AppearanceHeader.Font");
		this.gridColumn37.AppearanceHeader.Options.UseFont = true;
		this.gridColumn37.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn37.FieldName = "سعربعدالخصم";
		this.gridColumn37.MinWidth = 23;
		this.gridColumn37.Name = "gridColumn37";
		resources.ApplyResources(this.repositoryItemTextEdit1, "repositoryItemTextEdit1");
		this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
		resources.ApplyResources(this.repositoryItemTextEdit2, "repositoryItemTextEdit2");
		this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
		resources.ApplyResources(this.repositoryItemDateEdit1, "repositoryItemDateEdit1");
		this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit1.Buttons"))
		});
		this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit1.CalendarTimeProperties.Buttons"))
		});
		this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
		resources.ApplyResources(this.repositoryItemTextEdit3, "repositoryItemTextEdit3");
		this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
		this.repositoryItemPictureEdit1.Appearance.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceDisabled.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceReadOnly.Options.UseImage = true;
		this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
		this.repositoryItemPictureEdit1.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
		resources.ApplyResources(this.cardView1, "cardView1");
		this.cardView1.CardWidth = 183;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.DetailHeight = 288;
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		resources.ApplyResources(this.gridColumn10, "gridColumn10");
		this.gridColumn10.MinWidth = 17;
		this.gridColumn10.Name = "gridColumn10";
		resources.ApplyResources(this.gridColumn11, "gridColumn11");
		this.gridColumn11.MinWidth = 17;
		this.gridColumn11.Name = "gridColumn11";
		resources.ApplyResources(this.gridColumn12, "gridColumn12");
		this.gridColumn12.MinWidth = 17;
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn13, "gridColumn13");
		this.gridColumn13.MinWidth = 17;
		this.gridColumn13.Name = "gridColumn13";
		resources.ApplyResources(this.gridColumn14, "gridColumn14");
		this.gridColumn14.MinWidth = 17;
		this.gridColumn14.Name = "gridColumn14";
		resources.ApplyResources(this.gridColumn15, "gridColumn15");
		this.gridColumn15.MinWidth = 17;
		this.gridColumn15.Name = "gridColumn15";
		resources.ApplyResources(this.gridColumn16, "gridColumn16");
		this.gridColumn16.MinWidth = 17;
		this.gridColumn16.Name = "gridColumn16";
		resources.ApplyResources(this.gridColumn17, "gridColumn17");
		this.gridColumn17.MinWidth = 17;
		this.gridColumn17.Name = "gridColumn17";
		resources.ApplyResources(this.gridColumn18, "gridColumn18");
		this.gridColumn18.MinWidth = 17;
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
		this.groupBox5.Controls.Add(this.TxtRoom);
		this.groupBox5.Controls.Add(this.HoldOpenClose);
		this.groupBox5.Controls.Add(this.toggleCashierType);
		this.groupBox5.Controls.Add(this.LblSup);
		this.groupBox5.Controls.Add(this.Comp_Store);
		this.groupBox5.Controls.Add(this.toggleSectoral);
		this.groupBox5.Controls.Add(this.CBBounce);
		this.groupBox5.Controls.Add(this.LBlSuppAccount);
		this.groupBox5.Controls.Add(this.lblRoom);
		this.groupBox5.Controls.Add(this.TxtNumNote);
		this.groupBox5.Controls.Add(this.label18);
		this.groupBox5.Controls.Add(this.lblTable);
		this.groupBox5.Controls.Add(this.BtnOrderSearch);
		this.groupBox5.Controls.Add(this.LblCustLast);
		this.groupBox5.Controls.Add(this.label27);
		this.groupBox5.Controls.Add(this.DateHold);
		this.groupBox5.Controls.Add(this.TxtDate);
		this.groupBox5.Controls.Add(this.label28);
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
		this.HoldOpenClose.Toggled += new System.EventHandler(HoldOpenClose_Toggled);
		this.HoldOpenClose.Click += new System.EventHandler(HoldOpenClose_Click);
		resources.ApplyResources(this.Comp_Store, "Comp_Store");
		this.Comp_Store.FormattingEnabled = true;
		this.Comp_Store.Name = "Comp_Store";
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
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions5");
		this.BtnOrderSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnOrderSearch.Properties.Buttons"), resources.GetString("BtnOrderSearch.Properties.Buttons1"), (int)resources.GetObject("BtnOrderSearch.Properties.Buttons2"), (bool)resources.GetObject("BtnOrderSearch.Properties.Buttons3"), (bool)resources.GetObject("BtnOrderSearch.Properties.Buttons4"), (bool)resources.GetObject("BtnOrderSearch.Properties.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("BtnOrderSearch.Properties.Buttons6"), resources.GetObject("BtnOrderSearch.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("BtnOrderSearch.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("BtnOrderSearch.Properties.Buttons9"))
		});
		this.BtnOrderSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnOrderSearch.Properties.NullText = resources.GetString("BtnOrderSearch.Properties.NullText");
		this.BtnOrderSearch.Tag = "بحث بالكود";
		this.BtnOrderSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnOrderSearch_ButtonClick);
		this.BtnOrderSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnOrderSearch_KeyDown);
		this.BtnOrderSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BtnCustomerSearch_KeyPress);
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
		this.DateHold.Properties.Mask.EditMask = resources.GetString("DateHold.Properties.Mask.EditMask");
		this.DateHold.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("DateHold.Properties.Mask.UseMaskAsDisplayFormat");
		resources.ApplyResources(this.label28, "label28");
		this.label28.Name = "label28";
		resources.ApplyResources(this.label29, "label29");
		this.label29.Name = "label29";
		resources.ApplyResources(this.label30, "label30");
		this.label30.Name = "label30";
		resources.ApplyResources(this.BtnRecipt, "BtnRecipt");
		this.BtnRecipt.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnRecipt.Appearance.Font");
		this.BtnRecipt.Appearance.Options.UseFont = true;
		this.BtnRecipt.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRecipt.ImageOptions.Image");
		this.BtnRecipt.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnRecipt.Name = "BtnRecipt";
		this.BtnRecipt.Click += new System.EventHandler(BtnRecipt_Click);
		resources.ApplyResources(this.groupBox4, "groupBox4");
		this.groupBox4.Controls.Add(this.BtnCustomerSearch);
		this.groupBox4.Controls.Add(this.BtnNewCustomer);
		this.groupBox4.Controls.Add(this.label22);
		this.groupBox4.Controls.Add(this.label34);
		this.groupBox4.Controls.Add(this.label38);
		this.groupBox4.Controls.Add(this.label35);
		this.groupBox4.Controls.Add(this.label19);
		this.groupBox4.Controls.Add(this.label33);
		this.groupBox4.Controls.Add(this.label42);
		this.groupBox4.Controls.Add(this.label43);
		this.groupBox4.Controls.Add(this.TxtAdditionalNum);
		this.groupBox4.Controls.Add(this.TxtStreet);
		this.groupBox4.Controls.Add(this.TxtDistrict);
		this.groupBox4.Controls.Add(this.TxtCommercialRecord);
		this.groupBox4.Controls.Add(this.TxtPostalCode);
		this.groupBox4.Controls.Add(this.TxtCountry);
		this.groupBox4.Controls.Add(this.TxtCity);
		this.groupBox4.Controls.Add(this.label6);
		this.groupBox4.Controls.Add(this.label9);
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
		resources.ApplyResources(this.BtnCustomerSearch, "BtnCustomerSearch");
		this.BtnCustomerSearch.Name = "BtnCustomerSearch";
		this.BtnCustomerSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnCustomerSearch.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCustomerSearch.Properties.Appearance.Font");
		this.BtnCustomerSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnCustomerSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnCustomerSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnCustomerSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnCustomerSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions6");
		this.BtnCustomerSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnCustomerSearch.Properties.Buttons"), resources.GetString("BtnCustomerSearch.Properties.Buttons1"), (int)resources.GetObject("BtnCustomerSearch.Properties.Buttons2"), (bool)resources.GetObject("BtnCustomerSearch.Properties.Buttons3"), (bool)resources.GetObject("BtnCustomerSearch.Properties.Buttons4"), (bool)resources.GetObject("BtnCustomerSearch.Properties.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("BtnCustomerSearch.Properties.Buttons6"), resources.GetObject("BtnCustomerSearch.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("BtnCustomerSearch.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("BtnCustomerSearch.Properties.Buttons9"))
		});
		this.BtnCustomerSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCustomerSearch.Properties.NullText = resources.GetString("BtnCustomerSearch.Properties.NullText");
		this.BtnCustomerSearch.Tag = "بحث بالكود";
		this.BtnCustomerSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnCustomerSearch_ButtonClick);
		this.BtnCustomerSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnCustomerSearch_KeyDown);
		resources.ApplyResources(this.BtnNewCustomer, "BtnNewCustomer");
		this.BtnNewCustomer.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNewCustomer.Appearance.Font");
		this.BtnNewCustomer.Appearance.Options.UseFont = true;
		this.BtnNewCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewCustomer.ImageOptions.Image");
		this.BtnNewCustomer.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewCustomer.Name = "BtnNewCustomer";
		this.BtnNewCustomer.Click += new System.EventHandler(BtnNewCustomer_Click);
		resources.ApplyResources(this.label22, "label22");
		this.label22.Name = "label22";
		resources.ApplyResources(this.label34, "label34");
		this.label34.Name = "label34";
		resources.ApplyResources(this.label38, "label38");
		this.label38.Name = "label38";
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
		resources.ApplyResources(this.TxtCommercialRecord, "TxtCommercialRecord");
		this.TxtCommercialRecord.Name = "TxtCommercialRecord";
		this.TxtCommercialRecord.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCommercialRecord.Properties.Appearance.Font");
		this.TxtCommercialRecord.Properties.Appearance.Options.UseFont = true;
		this.TxtCommercialRecord.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCommercialRecord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtPostalCode, "TxtPostalCode");
		this.TxtPostalCode.Name = "TxtPostalCode";
		this.TxtPostalCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPostalCode.Properties.Appearance.Font");
		this.TxtPostalCode.Properties.Appearance.Options.UseFont = true;
		this.TxtPostalCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPostalCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
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
		resources.ApplyResources(this.label13, "label13");
		this.label13.Name = "label13";
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
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.TxtQuntity);
		base.Controls.Add(this.label16);
		base.Controls.Add(this.LblNowQuintity);
		base.Controls.Add(this.TxtAllQuantity);
		base.Controls.Add(this.gridControl1);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.BtnCheckOut);
		base.Controls.Add(this.BtnRecipt);
		base.Controls.Add(this.TxtTotalAll);
		base.Controls.Add(this.TxtNet);
		base.Controls.Add(this.TxtDiscountALLTotal);
		base.Controls.Add(this.TxtDiscount);
		base.Controls.Add(this.txtdicoutprege);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.TxtVat);
		base.Controls.Add(this.TxtNet22);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.TxtAllTotal);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.BtnReturnCashier);
		base.Controls.Add(this.TxtCost);
		base.Controls.Add(this.TxtAllVat);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.TxtTabocc);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.TxtTotals);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.TxtVatBeforeDisc);
		base.Controls.Add(this.TxtBarcode);
		base.Controls.Add(this.BtnMinus);
		base.Controls.Add(this.TxtTotalBeforDisc);
		base.Controls.Add(this.BtnPlus);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnPrint);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnPlusNet);
		base.Controls.Add(this.BtnMinNet);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnDelete);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmCashirWithOutGroup";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmCashir_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
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
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4Units).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
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
		((System.ComponentModel.ISupportInitialize)this.TxtStreet.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDistrict.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCountry.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
