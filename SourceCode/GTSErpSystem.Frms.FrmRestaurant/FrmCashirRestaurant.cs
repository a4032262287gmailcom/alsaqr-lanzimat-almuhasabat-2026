using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Media;
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
using DevExpress.XtraTab;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Dto;
using GTSErpSystem.Frms.FrmRestaurant.LuckUp;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.Orders;
using GTSErpSystem.Frms.Orders.FrmLuckUpOrders;
using GTSErpSystem.Frms.Orders.FrmOpenDay;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Helper;
using GTSErpSystem.Properties;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;
using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.APIHelper;

namespace GTSErpSystem.Frms.FrmRestaurant;

public class FrmCashirRestaurant : XtraForm
{
	private GTSdbEntities db = new GTSdbEntities();

	private Class_Items ITM = new Class_Items();

	public DataTable datatable = new DataTable();

	public DataTable datatableDetails = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	private DataTable CashierPrint = new DataTable();

	private DataSetALL ST = new DataSetALL();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private Class_Orders ORD = new Class_Orders();

	private Class_PriceOffer PurORD = new Class_PriceOffer();

	private Class_Customer SPR = new Class_Customer();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private bool OpenClose = false;

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

	public string ReturantType;

	private static int IsShow;

	private static int RoomNom;

	private static int TableNom;

	private int intType = 1;

	public bool ISEVENTDISC = true;

	public bool ISEVENTDISCTotal = true;

	public bool PrintShik;

	public bool PrintFatora = false;

	public decimal Cash;

	public decimal Bank;

	public decimal MoneyNeed;

	public decimal MoneyPaying;

	public decimal MoneyNet;

	public decimal Hanger;

	private int DecimalNum = LoginDetails.DecimalNum;

	public decimal Disc;

	public decimal DiscPerc = 0m;

	private bool IsClosingOk = true;

	// True only while editing an existing paid takeaway invoice through the existing update path.
	private bool isEditingExistingTakeAway = false;

	private int CostID = 0;

	private int ItmID;

	private int ADDIdd;

	private int SN = 0;

	private int StorId = 0;

	private static int Code = 0;

	private string fileNameAR = Application.StartupPath + "\\Layout\\RestaurantCashirAR.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\RestaurantCashirEN.xml";

	private float fontSize = (float)LoginDetails.FontSize;

	private string UnitName;

	private Class_WaitFormFunc waitForm = new Class_WaitFormFunc();

	private Class_ElctroncInvoice Zakat = new Class_ElctroncInvoice();

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	private IContainer components = null;

	private NumericEditBox TxtNet22;

	private DateEdit TxtDate;

	private Label LBlSuppAccount;

	private ButtonEdit TxtCode;

	private NumericEditBox TxtTotalAll;

	private Label label3;

	private NumericEditBox TxtDiscount;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtTotals;

	private Label label5;

	private Label label6;

	private NumericEditBox TxtNet;

	private Label label7;

	private Label label8;

	private NumericEditBox TxtTotalBeforDisc;

	private NumericEditBox TxtVatBeforeDisc;

	private NumericEditBox txtdicoutprege;

	private Label label1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem MenuNew;

	private ToolStripMenuItem MenuSave;

	private ToolStripMenuItem MenuEdit;

	private ToolStripMenuItem MenuDelete;

	private ToolStripMenuItem RecoveredMenuItemFix1;

	private ToolStripMenuItem MenuPrintQued;

	private ToolStripMenuItem MenuPreviewFatoura;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuNewItem;

	private ToolStripMenuItem RecoveredMenuItemFix2;

	private ToolStripMenuItem MenuSearchItem;

	private ToolStripMenuItem MenupPriceItem;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem MenuPurchOrder;

	private ToolStripMenuItem RecoveredMenuItemFix3;

	private ToolStripMenuItem RecoveredMenuItemFix4;

	private ToolStripMenuItem RecoveredMenuItemFix5;

	private ToolStripMenuItem RecoveredMenuItemFix6;

	private ToolStripMenuItem RecoveredMenuItemFix7;

	private ToolStripMenuItem RecoveredMenuItemFix8;

	private ToolStripMenuItem RecoveredMenuItemFix9;

	private ToolStripMenuItem RecoveredMenuItemFix10;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem RecoveredMenuItemFix11;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem MenuExit;

	private TileControl tileControl1;

	private TileGroup tileGroup2;

	private TileControl tileControl2;

	private TileGroup tileGroup3;

	private NumericEditBox TxtCost;

	private Label label9;

	private Label label10;

	private NumericEditBox TxtAllVat;

	private NumericEditBox TxtTabocc;

	private Label label11;

	private NumericEditBox TxtAllTotal;

	private SimpleButton BtnExit;

	private SimpleButton BtnDelete;

	private SimpleButton BtnNew;

	private SimpleButton BtnSave;

	private SimpleButton BtnReturnCashier;

	private Label label14;

	private GroupBox groupBox2;

	private XtraTabControl xtraTabControl1;

	private XtraTabPage TabSafry;

	private XtraTabPage TabMahly;

	private SimpleButton BtnCollecting;

	private SimpleButton BtnEditRoom;

	private SimpleButton BtnNewMahly;

	private SimpleButton BtnDeleteMahly;

	private SimpleButton BtnSaveMahly;

	private XtraTabPage TabTawsel;

	private SimpleButton BtnReturnCashierTawsel;

	private SimpleButton BtnNewTawsel;

	private SimpleButton BtnDeleteTawsel;

	private SimpleButton BtnSaveTawsel;

	private GridView gridView1;

	private GridColumn gridColumn24;

	private GridColumn gridColumn23;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn gridColumn26;

	private GridColumn shearch;

	private GridColumn gridColumn19;

	private GridColumn gridColumn4;

	private GridColumn gridColumn25;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn22;

	private GridColumn gridColumn5;

	private GridColumn gridColumn7;

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

	private Label label4;

	private SimpleButton BtnPrintSafry;

	private SimpleButton BtnPrintMahly;

	private SimpleButton BtnPrintTawsel;

	private SimpleButton BtnCollectingTawsel;

	private Label label13;

	private NumericEditBox TxtDiscountPercent;

	private SimpleButton BtnDiscount;

	private GridColumn gridColumn28;

	private GridColumn gridColumn29;

	private GridColumn gridColumn30;

	private GridColumn gridColumn31;

	private GridColumn gridColumn32;

	private GridColumn gridColumn33;

	private Label label27;

	private NumericEditBox TxtDiscountALLTotal;

	private Label label12;

	private Label label43;

	private TextEdit TxtPhone;

	private GridColumn gridColumn34;

	private GridColumn ItemID;

	private GridColumn ADDId;

	private ToggleSwitch HoldOpenClose;

	private ToggleSwitch toggleSectoral;

	private GridColumn gridColumn35;

	private GridColumn gridColumn36;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4Units;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonADD;

	public GridControl gridControl1;

	private Label LValid;

	private Label LSucced;

	private SimpleButton BtnUpload;

	private GridColumn gridColumn37;

	private GridColumn gridColumn38;

	private GridColumn gridColumn39;

	private SimpleButton BtnTypeInvoic;

	private SimpleButton BtnOpenCashir;

	private ButtonEdit TxtBarcode;

	private Mode mode { get; set; }

	public FrmCashirRestaurant()
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		InitializeComponent();
		TxtDigital();
		SetInitialRow();
		SetInitialRowPrinter();
		BindComboBox();
		BtnDelete.Enabled = false;
		SetDateCasheir();
		IsRestaurantActive();
		CostID = Convert.ToInt32(Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID);
		IEnumerable<Item_Groups> enumerable = from x in gTSdbEntities.Item_Groups.ToList()
			where x.IsShow == true
			select x;
		tileGroup2.Items.Clear();
		tileGroup3.Items.Clear();
		tileControl1.ItemSize = LoginDetails.GroupSize;
		tileControl2.ItemSize = LoginDetails.ItemSize;
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		string groupColor = LoginDetails.GroupColor;
		string groupFontColor = LoginDetails.GroupFontColor;
		TxtDiscountPercent.Value = LoginDetails.DiscountForRestaurant;
		foreach (Item_Groups item in enumerable)
		{
			TileItem tileItem = new TileItem();
			tileGroup2.Items.Add(tileItem);
			tileItem.Text = item.Name;
			tileItem.Id = item.ID;
			tileItem.ItemSize = TileItemSize.Wide;
			tileItem.AllowGlyphSkinning = DefaultBoolean.True;
			tileItem.Appearance.Font = new Font("Tahoma", fontSize);
			tileItem.Appearance.BackColor = ColorTranslator.FromHtml(groupColor);
			tileItem.Appearance.ForeColor = ColorTranslator.FromHtml(groupFontColor);
			tileItem.Appearance.BorderColor = Color.WhiteSmoke;
			tileItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
			tileItem.ItemClick += tileBarItem1_ItemClick;
			TileItemElement element = new TileItemElement();
			tileItem.Elements.Add(element);
			if (LoginDetails.IsElectronicInvoice)
			{
				BtnDelete.Visible = false;
				BtnDeleteMahly.Visible = false;
				BtnDeleteTawsel.Visible = false;
			}
			if (!LoginDetails.BarcodeResturant)
			{
				TxtBarcode.Visible = false;
				gridControl1.Location = new Point(gridControl1.Location.X, TxtBarcode.Location.Y);
				int num = TxtNet.Location.Y;
				int num2 = num - gridControl1.Location.Y - 10;
				gridControl1.Height = num2;
			}
		}
		toggleSectoral.IsOn = true;
		Clear();
		if (!CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 157))
		{
			BtnDelete.Visible = false;
		}
		if (LoginDetails.FrmOrderClose)
		{
			xtraTabControl1.Visible = false;
		}
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnDelete);
		}
		if (!LoginDetails.ShowRoom)
		{
			BtnCollecting.Visible = false;
			BtnEditRoom.Visible = false;
		}
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "كود_الصنف"));
		string text2 = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "أسم الصنف"));
		decimal num = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "الكمية"));
		if (text == "" || text2 == "" || num <= 0m)
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
			string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(focusedRowCellDisplayText).FirstOrDefault();
				if (get_Item_ByCodeALL_Result != null)
				{
					foreach (DataRow row in datatable.Rows)
					{
						if (!(row["ظ…"].ToString() == focusedRowCellDisplayText4))
						{
							continue;
						}
						decimal value2 = Convert.ToDecimal(row["سعر الوحدة"].ToString());
						row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]), DecimalNum);
						row["TaxTobacco"] = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Tobacco) / 100m;
						decimal num = Convert.ToDecimal(row["TaxTobacco"]);
						if (num < LoginDetails.MinTobaccoTax && num != 0m)
						{
							row["TaxTobacco"] = LoginDetails.MinTobaccoTax;
						}
						row["TaxTobacco"] = Convert.ToDecimal(row["TaxTobacco"]) * Convert.ToDecimal(value);
						row["TaxTobaccoDis"] = row["TaxTobacco"];
						decimal value3 = Convert.ToDecimal(value2) * Convert.ToDecimal(value);
						row["الاجمالى"] = Convert.ToDecimal(value3) + Convert.ToDecimal(row["TaxTobacco"]);
						if (ORD.IsVatActive())
						{
							if (get_Item_ByCodeALL_Result.Is_Tax == true)
							{
								row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]) * (Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100) + 1m), 2);
								row["الضربية"] = Math.Round(Convert.ToDecimal(row["إجمالى المبلغ"]) - Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
								row["هل_ضريبة"] = 1;
								row["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
							}
							else
							{
								row["الضربية"] = 0;
								row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
								row["هل_ضريبة"] = 0;
								row["قيمة_الضريبة"] = 0;
							}
						}
						else
						{
							row["الضربية"] = 0;
							row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
							row["هل_ضريبة"] = 0;
							row["قيمة_الضريبة"] = 0;
						}
					}
					insertnewrow();
					CheckFfistRow();
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[3];
					gridView1.ShowEditor();
				}
			}
		}
		else if (e.Column.FieldName == "سعر الوحدة")
		{
			string value4 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText5 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText6 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText7 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText8 = gridView1.GetFocusedRowCellDisplayText("الكمية");
			string focusedRowCellDisplayText9 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText5 != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result2 = ITM.GetItemByCode(focusedRowCellDisplayText5).FirstOrDefault();
				if (get_Item_ByCodeALL_Result2 != null)
				{
					foreach (DataRow row2 in datatable.Rows)
					{
						if (!(row2["ظ…"].ToString() == focusedRowCellDisplayText9))
						{
							continue;
						}
						if (row2["أسم_الوحدة"].ToString() == "الصغرى")
						{
							decimal sellPermission = ORD.GetSellPermission(focusedRowCellDisplayText5, UserSellPrice, "الصغرى");
							if (UserSellPrice == 1)
							{
								if (Convert.ToDecimal(value4) < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 2)
							{
								if (Convert.ToDecimal(value4) < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 3)
							{
								if (Convert.ToDecimal(value4) < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 4 && Convert.ToDecimal(value4) < sellPermission)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row2["سعر الوحدة"] = sellPermission;
								return;
							}
							row2["سعر الوحدة"] = value4;
							row2["TaxTobacco"] = Convert.ToDecimal(value4) * Convert.ToDecimal(get_Item_ByCodeALL_Result2.Tax_Tobacco) / 100m;
							decimal num2 = Convert.ToDecimal(row2["TaxTobacco"]);
							if (num2 < LoginDetails.MinTobaccoTax && num2 != 0m)
							{
								row2["TaxTobacco"] = LoginDetails.MinTobaccoTax;
							}
							row2["TaxTobacco"] = Convert.ToDecimal(row2["TaxTobacco"]) * Convert.ToDecimal(focusedRowCellDisplayText8);
							row2["TaxTobaccoDis"] = row2["TaxTobacco"];
							decimal value5 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8);
							row2["الاجمالى"] = Convert.ToDecimal(value5) + Convert.ToDecimal(row2["TaxTobacco"]);
							if (ORD.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result2.Is_Tax == true)
								{
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]) * (Convert.ToDecimal(get_Item_ByCodeALL_Result2.Tax_Value / (decimal?)100) + 1m), 2);
									row2["الضربية"] = Math.Round(Convert.ToDecimal(row2["إجمالى المبلغ"]) - Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
									row2["هل_ضريبة"] = 1;
									row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result2.Tax_Value;
								}
								else
								{
									row2["الضربية"] = 0;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
									row2["هل_ضريبة"] = 0;
									row2["قيمة_الضريبة"] = 0;
								}
							}
							else
							{
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
							}
							break;
						}
						if (row2["أسم_الوحدة"].ToString() == "المتوسطة")
						{
							decimal sellPermission2 = ORD.GetSellPermission(focusedRowCellDisplayText5, UserSellPrice, "المتوسطة");
							if (UserSellPrice == 1)
							{
								if (Convert.ToDecimal(value4) < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 2)
							{
								if (Convert.ToDecimal(value4) < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 3)
							{
								if (Convert.ToDecimal(value4) < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row2["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 4 && Convert.ToDecimal(value4) < sellPermission2)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row2["سعر الوحدة"] = sellPermission2;
								return;
							}
							row2["سعر الوحدة"] = value4;
							row2["TaxTobacco"] = Convert.ToDecimal(value4) * Convert.ToDecimal(get_Item_ByCodeALL_Result2.Tax_Tobacco) / 100m;
							decimal num3 = Convert.ToDecimal(row2["TaxTobacco"]);
							if (num3 < LoginDetails.MinTobaccoTax && num3 != 0m)
							{
								row2["TaxTobacco"] = LoginDetails.MinTobaccoTax;
							}
							row2["TaxTobacco"] = Convert.ToDecimal(row2["TaxTobacco"]) * Convert.ToDecimal(focusedRowCellDisplayText8);
							row2["TaxTobaccoDis"] = row2["TaxTobacco"];
							decimal value6 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8);
							row2["الاجمالى"] = Convert.ToDecimal(value6) + Convert.ToDecimal(row2["TaxTobacco"]);
							if (ORD.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result2.Is_Tax == true)
								{
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]) * (Convert.ToDecimal(get_Item_ByCodeALL_Result2.Tax_Value / (decimal?)100) + 1m), 2);
									row2["الضربية"] = Math.Round(Convert.ToDecimal(row2["إجمالى المبلغ"]) - Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
									row2["هل_ضريبة"] = 1;
									row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result2.Tax_Value;
								}
								else
								{
									row2["الضربية"] = 0;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
									row2["هل_ضريبة"] = 0;
									row2["قيمة_الضريبة"] = 0;
								}
							}
							else
							{
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
							}
							break;
						}
						if (!(row2["أسم_الوحدة"].ToString() == "الكبرى"))
						{
							continue;
						}
						decimal sellPermission3 = ORD.GetSellPermission(focusedRowCellDisplayText5, UserSellPrice, "الكبرى");
						if (UserSellPrice == 1)
						{
							if (Convert.ToDecimal(value4) < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row2["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 2)
						{
							if (Convert.ToDecimal(value4) < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row2["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 3)
						{
							if (Convert.ToDecimal(value4) < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row2["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 4 && Convert.ToDecimal(value4) < sellPermission3)
						{
							MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							row2["سعر الوحدة"] = sellPermission3;
							return;
						}
						row2["سعر الوحدة"] = value4;
						row2["TaxTobacco"] = Convert.ToDecimal(value4) * Convert.ToDecimal(get_Item_ByCodeALL_Result2.Tax_Tobacco) / 100m;
						decimal num4 = Convert.ToDecimal(row2["TaxTobacco"]);
						if (num4 < LoginDetails.MinTobaccoTax && num4 != 0m)
						{
							row2["TaxTobacco"] = LoginDetails.MinTobaccoTax;
						}
						row2["TaxTobacco"] = Convert.ToDecimal(row2["TaxTobacco"]) * Convert.ToDecimal(focusedRowCellDisplayText8);
						row2["TaxTobaccoDis"] = row2["TaxTobacco"];
						decimal value7 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8);
						row2["الاجمالى"] = Convert.ToDecimal(value7) + Convert.ToDecimal(row2["TaxTobacco"]);
						if (ORD.IsVatActive())
						{
							if (get_Item_ByCodeALL_Result2.Is_Tax == true)
							{
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]) * (Convert.ToDecimal(get_Item_ByCodeALL_Result2.Tax_Value / (decimal?)100) + 1m), 2);
								row2["الضربية"] = Math.Round(Convert.ToDecimal(row2["إجمالى المبلغ"]) - Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
								row2["هل_ضريبة"] = 1;
								row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result2.Tax_Value;
							}
							else
							{
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
							}
						}
						else
						{
							row2["الضربية"] = 0;
							row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
							row2["هل_ضريبة"] = 0;
							row2["قيمة_الضريبة"] = 0;
						}
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
		else
		{
			if (!(e.Column.FieldName == "إجمالى المبلغ"))
			{
				return;
			}
			string text = Convert.ToString(e.Value);
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
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("ADDId");
			if (int.TryParse(focusedRowCellDisplayText2, out var _))
			{
			}
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (!int.TryParse(focusedRowCellDisplayText3, out SN))
			{
				SN = 0;
			}
			string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			UnitName = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string StoreName = gridView1.GetFocusedRowCellDisplayText("المخزن");
			StorId = (from x in db.Account_Stores
				where x.Store_Name == StoreName
				select x.ID).FirstOrDefault();
			CodeItm = focusedRowCellDisplayText4;
		}
		catch (Exception)
		{
		}
	}

	private void repositoryItemButtonEdit1_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		GridPlus();
	}

	private void repositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		GridDelete();
	}

	private void repositoryItemButtonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		GridMinus();
	}

	private void repositoryItemButtonEdit4Units_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string Itemocde = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "كود_الصنف"));
		if (Itemocde != "")
		{
			LoginDetails.ItemcodeForUnits = Itemocde;
			FrmSearchUnit frmSearchUnit = new FrmSearchUnit();
			Item_Items item_Items = (from x in ITM.GetItemByCode()
				where x.Item_code == Itemocde
				select x).FirstOrDefault();
			int itemId = item_Items.ItemId;
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
							if (row["ظ…"].ToString() == Convert.ToString(num))
							{
								row["الوحدة"] = get_Item_ByCodeALL_Result.UnitSmall;
								row["أسم_الوحدة"] = "الصغرى";
								row["المخزن"] = ORD.GetStoreName();
								row["رقم المخزن"] = ORD.GetStoreID();
								row["الكمية"] = 1;
								row["سعر الوحدة"] = get_Item_ByCodeALL_Result.SellPriceSmall;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1)), DecimalNum);
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
							if (row2["ظ…"].ToString() == Convert.ToString(num3))
							{
								row2["الوحدة"] = get_Item_ByCodeALL_Result2.UnitMedium;
								row2["أسم_الوحدة"] = "المتوسطة";
								row2["الكمية"] = 1;
								row2["المخزن"] = ORD.GetStoreName();
								row2["رقم المخزن"] = ORD.GetStoreID();
								row2["سعر الوحدة"] = get_Item_ByCodeALL_Result2.SellPriceMedium;
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium * (decimal?)Convert.ToInt32(1)), DecimalNum);
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
						}
						GetTotalCost();
						string StoreName2 = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int num4 = (from x in db.Account_Stores
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
						int num5 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row3 in datatable.Rows)
						{
							if (row3["ظ…"].ToString() == Convert.ToString(num5))
							{
								row3["الوحدة"] = get_Item_ByCodeALL_Result3.UnitLarge;
								row3["أسم_الوحدة"] = "الكبرى";
								row3["الكمية"] = 1;
								row3["المخزن"] = ORD.GetStoreName();
								row3["رقم المخزن"] = ORD.GetStoreID();
								row3["سعر الوحدة"] = get_Item_ByCodeALL_Result3.SellpriceLarge;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge * (decimal?)Convert.ToInt32(1)), DecimalNum);
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
						}
						GetTotalCost();
						string StoreName3 = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int num6 = (from x in db.Account_Stores
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

	private void repositoryItemButtonADD_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			decimal num2 = Convert.ToInt32(row["ItemID"]);
			decimal num3 = Convert.ToInt32(row["ADDId"]);
			decimal num4 = Convert.ToInt32(row["ظ…"]);
			if (Convert.ToInt32(row["ItemID"]) > 0 && Convert.ToInt32(row["ADDId"]) == 0 && Convert.ToInt32(row["ItemID"]) == ItmID && Convert.ToInt32(row["ADDId"]) == ADDIdd && Convert.ToInt32(row["ظ…"]) == SN)
			{
				row["RowId"] = row["ظ…"];
				row.AcceptChanges();
				SearchItems2(SN, ItmID);
				ArrangeDatatable();
				break;
			}
		}
	}

	public void SearchItems2(int RowId, int ItemIDADD)
	{
		try
		{
			LoginDetails.ItemSearch = CodeItm;
			LoginDetails.ISCheckSearchItem = false;
			LoginDetails.ISPurches = true;
			FrmSearchAdds frmSearchAdds = new FrmSearchAdds();
			frmSearchAdds.ShowDialog();
			if (LoginDetails.ItemSearchAll2 == null)
			{
				return;
			}
			if (LoginDetails.ItemSearchAll2.Count > 0)
			{
				foreach (string item in LoginDetails.ItemSearchAll2)
				{
					int ItemCodeRow = Convert.ToInt32(item);
					Item_Add item_Add = db.Item_Add.Where((Item_Add x) => x.ID == ItemCodeRow).FirstOrDefault();
					if (item_Add != null)
					{
						DataRow dataRow = null;
						dataRow = datatable.NewRow();
						dataRow["ظ…"] = RowId;
						dataRow["RowId"] = RowId;
						dataRow["ADDId"] = Convert.ToInt32(item_Add.ID);
						dataRow["ItemID"] = 0;
						dataRow["ItemIDADD"] = ItemIDADD;
						dataRow["ItemUnitID"] = 1;
						dataRow["كود_الصنف"] = item_Add.ID;
						dataRow["أسم الصنف"] = item_Add.Name;
						dataRow["المجموعة"] = item_Add.ID;
						dataRow["الوحدة"] = "حبة";
						dataRow["الكمية"] = 1;
						dataRow["متوسط_التكلفة"] = item_Add.CostPrice;
						dataRow["اجمالي_متوسط_التكلفة"] = item_Add.CostPrice;
						dataRow["المخزن"] = ORD.GetStoreName();
						dataRow["رقم المخزن"] = ORD.GetStoreID();
						dataRow["أسم_الوحدة"] = "الصغرى";
						dataRow["سعر الوحدة"] = item_Add.SellPrice;
						dataRow["الاجمالى"] = item_Add.SellPrice;
						if (ORD.IsVatActive())
						{
							if (item_Add.Is_Tax == true && !LoginDetails.ItemTotalWithVat)
							{
								dataRow["الضربية"] = Math.Round(Convert.ToDecimal(item_Add.SellPrice * (decimal?)Convert.ToInt32(1) * item_Add.Tax_Value / (decimal?)100), DecimalNum);
								dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Add.SellPrice * (decimal?)Convert.ToInt32(1) * item_Add.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Add.SellPrice * (decimal?)Convert.ToInt32(1))), DecimalNum);
								dataRow["هل_ضريبة"] = 1;
								dataRow["قيمة_الضريبة"] = item_Add.Tax_Value;
							}
							else
							{
								dataRow["هل_ضريبة"] = 0;
								dataRow["قيمة_الضريبة"] = 0;
								dataRow["الضربية"] = 0.0;
								dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Add.SellPrice * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
						}
						else
						{
							dataRow["هل_ضريبة"] = 0;
							dataRow["قيمة_الضريبة"] = 0;
							dataRow["الضربية"] = 0.0;
							dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Add.SellPrice * (decimal?)Convert.ToInt32(1))), DecimalNum);
						}
						object obj = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
						int num = Convert.ToInt32(obj.ToString());
						datatable.Rows.Add(dataRow);
					}
					IsShow = 1;
				}
				GetTotalCost();
				ArrangeDatatable();
				frmSearchAdds.Close();
				frmSearchAdds.Hide();
				LoginDetails.ItemSearchAll2.Clear();
				LoginDetails.ISPurches = false;
			}
			frmSearchAdds.Close();
			frmSearchAdds.Hide();
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
		}
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		if (isEditingExistingTakeAway && MessageBox.Show(
			"توجد تعديلات غير محفوظة. هل تريد إلغاء التعديلات والبدء بفاتورة جديدة؟",
			"إلغاء التعديل", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
			MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign) != DialogResult.Yes)
			return;

		isEditingExistingTakeAway = false;
		datatable.Rows.Clear();
		Clear();
	}

	private async void BtnSave_Click(object sender, EventArgs e)
	{
		await SaveOrEditAsync();
	}

	private async void BtnEdit_Click(object sender, EventArgs e)
	{
		if (!CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 157))
		{
			MessageBox.Show("عفواً، لا توجد لديك صلاحية تعديل فاتورة الكاشير.", "صلاحيات المستخدمين",
				MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3,
				MessageBoxOptions.RightAlign);
			return;
		}

		if (isEditingExistingTakeAway)
		{
			await SaveEditedTakeAwayAsync();
			return;
		}

		int orderId;
		Order_Orders order;
		if (!TryGetEditableTakeAwayOrder(out orderId, out order))
			return;

		SetTakeAwayEditMode(true);
		MessageBox.Show(
			"تم تفعيل وضع تعديل الفاتورة رقم (" + orderId + "). بعد تعديل البنود اضغط «حفظ التعديل» أو زر الحفظ. لن تُنشأ فاتورة جديدة.",
			"تعديل فاتورة الكاشير", MessageBoxButtons.OK, MessageBoxIcon.Information,
			MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
	}

	private async Task SaveOrEditAsync()
	{
		try
		{
			if (isEditingExistingTakeAway)
				await SaveEditedTakeAwayAsync();
			else
				await Save();
		}
		catch (Exception ex)
		{
			MessageBox.Show("تعذر تنفيذ أمر الحفظ: " + ex.Message, "خطأ في الحفظ",
				MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
				MessageBoxOptions.RightAlign);
		}
	}

	private bool TryGetEditableTakeAwayOrder(out int orderId, out Order_Orders order)
	{
		orderId = 0;
		order = null;

		if (!int.TryParse(TxtCode.Text, out orderId) || orderId <= 0)
		{
			MessageBox.Show("اختر فاتورة محفوظة أولاً ثم اضغط تعديل.", "تعديل فاتورة الكاشير",
				MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3,
				MessageBoxOptions.RightAlign);
			return false;
		}

		try
		{
			order = ORD.GetPurchases(orderId).FirstOrDefault();
			if (order == null ||
				order.PurBranchID.GetValueOrDefault() != orderId ||
				order.BranchID.GetValueOrDefault() != LoginDetails.BranchID)
			{
				MessageBox.Show("لم يتم العثور على الفاتورة في الفرع الحالي.", "تعديل فاتورة الكاشير",
					MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3,
					MessageBoxOptions.RightAlign);
				return false;
			}

			// The existing generic update procedure maps OrderCashierType=false to Restaurant_TypeID=1.
			// Do not use it for dine-in or delivery invoices, which have different workflows.
			if (order.Restaurant_TypeID.GetValueOrDefault() != 1)
			{
				MessageBox.Show(
					"التعديل من هذه الشاشة مدعوم حالياً لفواتير السفري المدفوعة فقط. لم يتم تعديل الفاتورة المحلية أو فاتورة التوصيل لأن لهما مسار تحديث مختلفاً.",
					"نوع الفاتورة غير مدعوم للتعديل", MessageBoxButtons.OK, MessageBoxIcon.Warning,
					MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return false;
			}

			if (order.Order_Paymant_Type.GetValueOrDefault() != 1 ||
				order.IsWaiting.GetValueOrDefault())
			{
				MessageBox.Show(
					"لا يمكن تعديل فاتورة معلقة أو غير مدفوعة من مسار تعديل فواتير السفري.",
					"حالة الفاتورة غير مدعومة", MessageBoxButtons.OK, MessageBoxIcon.Warning,
					MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return false;
			}

			// Do not rewrite an existing electronic invoice using the ordinary invoice update path.
			if (LoginDetails.IsElectronicInvoice)
			{
				MessageBox.Show(
					"الفوترة الإلكترونية مفعّلة. لا يمكن تعديل فاتورة موجودة من خلال مسار الحفظ العادي؛ استخدم مسار الإشعار الدائن/المدين المعتمد.",
					"حماية الفاتورة الإلكترونية", MessageBoxButtons.OK, MessageBoxIcon.Warning,
					MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return false;
			}

			if (Zakat.GetIdByOrderId(orderId).GetValueOrDefault() > 0)
			{
				MessageBox.Show(
					"للفواتورة سجل فوترة إلكترونية مرتبط؛ لا يجوز تعديلها من مسار الحفظ العادي.",
					"حماية الفاتورة الإلكترونية", MessageBoxButtons.OK, MessageBoxIcon.Warning,
					MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return false;
			}

			using (GTSdbEntities checkDb = new GTSdbEntities())
			{
				bool hasReturn = checkDb.Order_OrderReturn.Any(x =>
					x.FromPurchesID == (int?)orderId && x.BranchID == (int?)LoginDetails.BranchID);
				bool hasCheckout = checkDb.Order_CheckoutOrders.Any(x =>
					x.FromPurchesID == (int?)orderId && x.BranchID == (int?)LoginDetails.BranchID);
				if (hasReturn || hasCheckout)
				{
					MessageBox.Show(
						"لا يمكن تعديل الفاتورة لوجود حركة مرتجع أو تسوية/صرف مرتبطة بها.",
						"تعذر تعديل الفاتورة", MessageBoxButtons.OK, MessageBoxIcon.Warning,
						MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					return false;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(
				"تعذر التحقق من حالة الفاتورة قبل تعديلها؛ لم يتم فتح وضع التعديل. " + ex.Message,
				"تعذر التحقق", MessageBoxButtons.OK, MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			return false;
		}
	}

	private void SetTakeAwayEditMode(bool editing)
	{
		isEditingExistingTakeAway = editing;
		MenuEdit.Text = editing
			? (LoginDetails.Lang == "ar-EG" ? "حفظ التعديل F6" : "Save changes F6")
			: (LoginDetails.Lang == "ar-EG" ? "تعديل F6" : "Edit F6");

		if (editing)
		{
			BtnSave.Enabled = true;
			BtnSaveMahly.Enabled = false;
			BtnSaveTawsel.Enabled = false;
			BtnDelete.Enabled = false;
			gridControl1.Enabled = true;
			gridView1.OptionsBehavior.Editable = true;
			IsEnableTrue();
		}
		else if (!string.IsNullOrWhiteSpace(TxtCode.Text))
		{
			BtnSave.Enabled = false;
			BtnSaveMahly.Enabled = false;
			BtnSaveTawsel.Enabled = false;
			BtnDelete.Enabled = true;
			gridView1.OptionsBehavior.Editable = false;
			IsEnableFalse();
		}
	}

	private async Task SaveEditedTakeAwayAsync()
	{
		if (!isEditingExistingTakeAway)
			return;

		if (!CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 157))
		{
			MessageBox.Show("انتهت صلاحية التعديل أو لا توجد لديك صلاحية لهذه العملية.", "صلاحيات المستخدمين",
				MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3,
				MessageBoxOptions.RightAlign);
			SetTakeAwayEditMode(false);
			return;
		}

		int orderId;
		Order_Orders existingOrder;
		if (!TryGetEditableTakeAwayOrder(out orderId, out existingOrder))
		{
			SetTakeAwayEditMode(false);
			return;
		}

		try
		{
			if (SafeConvertToDecimal(TxtNet.Text) <= 0m)
			{
				MessageBox.Show("لا يمكن حفظ الفاتورة لأن الإجمالي يساوي صفراً.", "التحقق من الفاتورة",
					MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3,
					MessageBoxOptions.RightAlign);
				return;
			}

			int editableItemCount = datatable.Rows.Cast<DataRow>().Count(row =>
				row.RowState != DataRowState.Deleted &&
				row.RowState != DataRowState.Detached &&
				!string.IsNullOrWhiteSpace(Convert.ToString(row["كود_الصنف"])));
			if (editableItemCount == 0)
			{
				MessageBox.Show("أضف صنفاً واحداً على الأقل قبل حفظ التعديلات.", "التحقق من الفاتورة",
					MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3,
					MessageBoxOptions.RightAlign);
				return;
			}

			if (IsItemPriceZero() == 0)
				return;

			GetTotalCost();
			Cal_FinalResultTotals();
			if (PermissionSellPrice() == 0)
				return;

			LoginDetails.NetCash = Math.Round(SafeConvertToDecimal(TxtNet.Text), DecimalNum);
			LoginDetails.VatCash = Math.Round(SafeConvertToDecimal(TxtVatBeforeDisc.Text), DecimalNum);
			LoginDetails.TotalFatorPrice = SafeConvertToDecimal(TxtNet.Text);

			using (FrmMoneySafry payment = new FrmMoneySafry())
			{
				payment.ShowDialog(this);
				if (!payment.IsComplateMony)
					return;

				Cash = SafeConvertToDecimal(payment.TxtCash.Text);
				Bank = SafeConvertToDecimal(payment.TxtVisa.Text);
				MoneyNeed = SafeConvertToDecimal(payment.TxtCash.Text);
				MoneyPaying = SafeConvertToDecimal(payment.TxtVisa.Text);
				MoneyNet = SafeConvertToDecimal(payment.TxtMoneyNet.Text);
			}

			FilterDataTableSave(datatable, orderId, IsWait: false);
			int result = ORD.UpdateOrders(
				orderId,
				existingOrder.SupplierID.GetValueOrDefault(LoginDetails.Acc_CustomerCash),
				existingOrder.IsWaiting.GetValueOrDefault(false),
				existingOrder.SupplierName ?? "عميل نقدي",
				TxtPhone.Text ?? string.Empty,
				existingOrder.SupplierVatNum ?? string.Empty,
				TxtDate.DateTime,
				1,
				existingOrder.Restaurant_PayType ?? "مدفوعة",
				1,
				existingOrder.CostCentersID.GetValueOrDefault(),
				existingOrder.BounceID.GetValueOrDefault(),
				existingOrder.Note ?? string.Empty,
				existingOrder.NoteNum ?? string.Empty,
				Math.Round(SafeConvertToDecimal(TxtCost.Text), DecimalNum),
				SafeConvertToDecimal(TxtVat.Text),
				SafeConvertToDecimal(TxtTotals.Text),
				SafeConvertToDecimal(TxtTotalAll.Text),
				Math.Round(SafeConvertToDecimal(TxtDiscount.Text), DecimalNum),
				Math.Round(SafeConvertToDecimal(txtdicoutprege.Text), DecimalNum),
				Math.Round(SafeConvertToDecimal(TxtVatBeforeDisc.Text), DecimalNum),
				Math.Round(SafeConvertToDecimal(TxtTotalBeforDisc.Text), DecimalNum),
				Math.Round(SafeConvertToDecimal(TxtTabocc.Text), DecimalNum),
				Math.Round(SafeConvertToDecimal(TxtAllVat.Text), DecimalNum),
				Math.Round(SafeConvertToDecimal(TxtNet.Text), DecimalNum),
				Cash,
				Bank,
				false,
				existingOrder.RoomNum.GetValueOrDefault(),
				existingOrder.TableNum.GetValueOrDefault(),
				dtCurrentTable,
				existingOrder.SalesMan ?? LoginDetails.UserName,
				existingOrder.DateHold ?? DateTime.Now,
				existingOrder.BuildingNum ?? string.Empty,
				existingOrder.Street ?? string.Empty,
				existingOrder.District ?? string.Empty,
				existingOrder.City ?? string.Empty,
				existingOrder.Country ?? string.Empty,
				existingOrder.PostalCode ?? string.Empty,
				existingOrder.AdditionalNum ?? string.Empty,
				existingOrder.CommercialRecord ?? string.Empty,
				existingOrder.HasmPer.GetValueOrDefault(),
				existingOrder.HasmAmount.GetValueOrDefault(),
				existingOrder.Charge.GetValueOrDefault(),
				existingOrder.AllDiscount.GetValueOrDefault(),
				existingOrder.BounsAmount.GetValueOrDefault(),
				existingOrder.CarName ?? string.Empty,
				existingOrder.CarModel ?? string.Empty,
				existingOrder.PlateNumber ?? string.Empty,
				existingOrder.ChassisNum ?? string.Empty,
				existingOrder.CarColor ?? string.Empty,
				existingOrder.Counter ?? string.Empty,
				existingOrder.ProjectId.GetValueOrDefault(1));

			if (result <= 0)
			{
				MessageBox.Show("لم تُرجع طبقة البيانات نتيجة نجاح للتعديل. لم يتم عرض رسالة نجاح.", "تعذر التعديل",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3,
					MessageBoxOptions.RightAlign);
				return;
			}

			Order_Orders savedOrder = ORD.GetPurchases(orderId).FirstOrDefault();
			int expectedRows = datatable.Rows.Cast<DataRow>().Count(row =>
				row.RowState != DataRowState.Deleted &&
				row.RowState != DataRowState.Detached &&
				!string.IsNullOrWhiteSpace(Convert.ToString(row["كود_الصنف"])));
			List<Order_OrdersDetails> savedDetails = ORD.GetItemsPurchesByID(orderId);
			if (savedOrder == null ||
				savedOrder.BranchID.GetValueOrDefault() != LoginDetails.BranchID ||
				Math.Abs(savedOrder.Net.GetValueOrDefault() - Math.Round(SafeConvertToDecimal(TxtNet.Text), DecimalNum)) > 0.02m ||
				savedDetails == null || savedDetails.Count != expectedRows)
			{
				MessageBox.Show(
					"لم أتمكن من التحقق من تطابق رأس الفاتورة وتفاصيلها بعد التعديل؛ لم أعرض رسالة نجاح. أعد تحميل الفاتورة وتحقق من بياناتها قبل إعادة المحاولة.",
					"تعذر التحقق من التعديل", MessageBoxButtons.OK, MessageBoxIcon.Warning,
					MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return;
			}

			SetTakeAwayEditMode(false);
			GetElctornc();
			MessageBox.Show("تم تعديل فاتورة الكاشير رقم (" + orderId + ") والتحقق من إجماليها وعدد بنودها.",
				"نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
		}
		catch (Exception ex)
		{
			MessageBox.Show("تعذر تعديل الفاتورة رقم (" + orderId + "). " + ex.Message,
				"خطأ في تعديل الفاتورة", MessageBoxButtons.OK, MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnSaveMahly_Click(object sender, EventArgs e)
	{
		if (!LoginDetails.ShowRoom)
		{
			SaveM();
		}
		else
		{
			SaveMahly();
		}
	}

	private void BtnEditMahly_Click(object sender, EventArgs e)
	{
	}

	private void BtnSaveTawsel_Click(object sender, EventArgs e)
	{
		SaveDelivery();
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
		if (!(TxtCode.Text != ""))
		{
			return;
		}
		FrmPrint frmPrint = new FrmPrint();
		frmPrint.ShowDialog();
		if (frmPrint.CKCust.Checked)
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
		if (frmPrint.CKRest.Checked)
		{
			Thread thread2 = new Thread((ThreadStart)delegate
			{
				BeginInvoke((Action)delegate
				{
					PrintSecondCasheir(Code);
				});
			});
			thread2.Start();
		}
		if (!frmPrint.CKCustPrintSecond.Checked)
		{
			return;
		}
		Thread thread3 = new Thread((ThreadStart)delegate
		{
			BeginInvoke((Action)delegate
			{
				PrintSecondDefualt(Code);
			});
		});
		thread3.Start();
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (isEditingExistingTakeAway)
		{
			MessageBox.Show("احفظ التعديلات أو ألغِها قبل اختيار فاتورة أخرى.", "تعديلات غير محفوظة",
				MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3,
				MessageBoxOptions.RightAlign);
			return;
		}
		try
		{
			FrmSearchOrders frmSearchOrders = new FrmSearchOrders();
			frmSearchOrders.ShowDialog();
			if (frmSearchOrders.CustCode > 0)
			{
				int custCode = frmSearchOrders.CustCode;
				Order_Orders order_Orders = ORD.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<Order_OrdersDetails> itemsPurchesByID = ORD.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (Order_OrdersDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int ItemAdd = 0;
					int num2 = Convert.ToInt32(item.ItemID);
					if (num2 == 0)
					{
						num2 = Convert.ToInt32(item.ItemIDADD);
						ItemAdd = Convert.ToInt32(item.R_ADDId);
					}
					Item_Items item_Items = ORD.GetItemsByitemid(num2).FirstOrDefault();
					Printer printer = ORD.GetPrinter(num2);
					dataRow = datatable.NewRow();
					dataRow["ظ…"] = Convert.ToInt32(item.R_ItmSN);
					dataRow["RowId"] = Convert.ToInt32(item.R_RowId);
					dataRow["ADDId"] = Convert.ToInt32(item.R_ADDId);
					dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
					dataRow["ItemIDADD"] = Convert.ToInt32(item.ItemIDADD);
					dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
					dataRow["PrinterID"] = printer.ID;
					dataRow["PrinterName"] = printer.Name;
					dataRow["PrinterIDItem"] = item_Items.PrinterID;
					dataRow["PrinterNameItem"] = ORD.GetPrinterName(Convert.ToInt32(item_Items.PrinterID));
					dataRow["المجموعة"] = item_Items.Category_ID;
					if (ItemAdd > 0)
					{
						dataRow["كود_الصنف"] = ItemAdd;
						dataRow["أسم الصنف"] = db.Item_Add.Where((Item_Add x) => x.ID == ItemAdd).FirstOrDefault().Name;
						dataRow["الاسم الانجليزي"] = dataRow["أسم الصنف"];
					}
					else
					{
						dataRow["كود_الصنف"] = item_Items.Item_code;
						dataRow["أسم الصنف"] = item_Items.item_Name;
						dataRow["الاسم الانجليزي"] = item_Items.item_Name_English;
					}
					if (item.ItemUnitType == "الصغرى")
					{
						dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "المتوسطة")
					{
						dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "الكبرى")
					{
						dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					dataRow["أسم_الوحدة"] = item.ItemUnitType;
					dataRow["رقم المخزن"] = item.StoreID;
					dataRow["المخزن"] = (from x in db.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["الكمية"] = Math.Round(Convert.ToDecimal(item.Quantity), DecimalNum);
					dataRow["سعر الوحدة"] = item.UnitPrice;
					dataRow["متوسط_التكلفة"] = item.LastCost;
					dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
					dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
					dataRow["مسح"] = 0;
					dataRow["الطول"] = 1;
					dataRow["العرض"] = 1;
					dataRow["الإرتفاع"] = 1;
					dataRow["الوزن"] = 1;
					dataRow["قيمة الخصم"] = 0;
					dataRow["نسبةالخصم"] = 0;
					dataRow["TaxTobacco"] = Math.Round(Convert.ToDecimal(item.TobaccoTax), DecimalNum);
					dataRow["TaxTobaccoDis"] = Math.Round(Convert.ToDecimal(item.TobaccoTaxDis), DecimalNum);
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["الضربية"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item.NetTotalPrice), DecimalNum);
						dataRow["هل_ضريبة"] = 1;
						dataRow["قيمة_الضريبة"] = item_Items.Tax_Value;
					}
					else
					{
						dataRow["هل_ضريبة"] = 0;
						dataRow["قيمة_الضريبة"] = 0;
						dataRow["الضربية"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item.NetTotalPrice) + Convert.ToDecimal(item.VAT), DecimalNum);
					}
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				TxtCode.Text = order_Orders.PurBranchID.ToString();
				Code = custCode;
				isEditingExistingTakeAway = false;
				SetTakeAwayEditMode(false);
				TxtPhone.Text = order_Orders.SupplierPhone;
				TxtDate.DateTime = Convert.ToDateTime(order_Orders.Purchases_Date);
				TxtTotals.Text = Convert.ToString(order_Orders.TotalPrices);
				TxtVat.Text = Convert.ToString(order_Orders.Tax);
				TxtTotalAll.Text = order_Orders.Safy.ToString();
				TxtDiscount.Text = order_Orders.DiscountNum.ToString();
				TxtTotalBeforDisc.Text = order_Orders.TotalPrices_Discount.ToString();
				TxtVatBeforeDisc.Text = order_Orders.Tax_Discount.ToString();
				TxtNet.Text = order_Orders.Net.ToString();
				TxtNet22.Text = order_Orders.Safy.ToString();
				TxtCost.Text = order_Orders.CostOrder.ToString();
				Cash = Convert.ToDecimal(order_Orders.CashMoney);
				Bank = Convert.ToDecimal(order_Orders.CashBank);
				TxtTabocc.Text = order_Orders.TobaccoTax.ToString();
				TxtAllVat.Text = order_Orders.AllTax.ToString();
				LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
				LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum);
				if (order_Orders.Restaurant_TypeID == 1)
				{
					ReturantType = "طلب سفري";
				}
				if (order_Orders.Restaurant_TypeID == 2)
				{
					ReturantType = "طلب محلي";
				}
				if (order_Orders.Restaurant_TypeID == 3)
				{
					ReturantType = "طلب توصيل";
				}
				BtnSave.Enabled = false;
				BtnSaveMahly.Enabled = false;
				BtnSaveTawsel.Enabled = false;
				BtnDelete.Enabled = true;
				gridView1.OptionsBehavior.Editable = true;
				ArrangeDatatable();
				IsEnableFalse();
			}
			GetElctornc();
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void TxtCust_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustCode > 0)
			{
				int FrmID = frmSearchCustomer.CustCode;
				Account_CustSup account_CustSup = (from x in SPR.GetAll()
					where x.CustSuppCode == FrmID
					select x).FirstOrDefault();
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void tileBarItem2_ItemClick(object sender, TileItemEventArgs e)
	{
		int id = e.Item.Id;
		if (!LoginDetails.AlLItemInRow)
		{
			InsertNewItem(id);
		}
		else
		{
			InsertNewItemAllItemInRow(id);
		}
	}

	private void tileBarItem1_ItemClick(object sender, TileItemEventArgs e)
	{
		try
		{
			List<Item_Items> list = db.Item_Items.Where((Item_Items x) => x.Category_ID == (int?)e.Item.Id).ToList();
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
				tileItem.Appearance.BorderColor = Color.Black;
				tileItem.Appearance.ForeColor = ColorTranslator.FromHtml(itemFontColor);
				tileItem.Appearance.Font = new Font("Tahoma", fontSize);
				tileItem.ItemClick += tileBarItem2_ItemClick;
				TileItemElement tileItemElement = new TileItemElement();
				tileItem.Elements.Add(tileItemElement);
				TileItemElement tileItemElement2 = new TileItemElement();
				tileItem.Elements.Add(tileItemElement2);
				TileItemElement tileItemElement3 = new TileItemElement();
				tileItem.Elements.Add(tileItemElement3);
				TileItemElement tileItemElement4 = new TileItemElement();
				tileItem.Elements.Add(tileItemElement4);
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
					if (Convert.ToBoolean(item.Is_Tax))
					{
						tileItemElement3.Text = Math.Round(Convert.ToDecimal(item.SellPriceSmall), DecimalNum).ToString();
					}
					else
					{
						tileItemElement3.Text = Math.Round(Convert.ToDecimal(item.SellPriceSmall), DecimalNum).ToString();
					}
					if (LoginDetails.ItemShowPrice == "TopLeft")
					{
						tileItemElement3.TextAlignment = TileItemContentAlignment.TopLeft;
					}
					else if (LoginDetails.ItemShowPrice == "MiddleCenter")
					{
						tileItemElement3.TextAlignment = TileItemContentAlignment.MiddleCenter;
					}
					else if (LoginDetails.ItemShowPrice == "BottomRight")
					{
						tileItemElement3.TextAlignment = TileItemContentAlignment.BottomRight;
					}
				}
				if (LoginDetails.IsImage && !(item.Image == "") && item.Image != null)
				{
					tileItemElement4.Image = Base64ToImage(item.Image);
					tileItemElement4.ImageAlignment = TileItemContentAlignment.BottomRight;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void BtnReturnCashier_Click(object sender, EventArgs e)
	{
		FrmReturnOrderInCashier frmReturnOrderInCashier = new FrmReturnOrderInCashier();
		frmReturnOrderInCashier.TxtBarcode.Text = TxtCode.Text;
		frmReturnOrderInCashier.ShowDialog();
	}

	private void BtnCollectingTawsel_Click(object sender, EventArgs e)
	{
		if (!IsClosingOk)
		{
			MessageBox.Show("عفوا، يوجد عمليات جارية الان \r\n حاول مرة اخري عند الانتهاء .", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			return;
		}
		IsClosingOk = false;
		try
		{
			LoginDetails.IsFrmCollect = true;
			FrmDeliveryCollecting frmDeliveryCollecting = new FrmDeliveryCollecting();
			frmDeliveryCollecting.ShowDialog();
		}
		catch (Exception)
		{
		}
		IsClosingOk = true;
	}

	private void BtnCollecting_Click(object sender, EventArgs e)
	{
		if (!IsClosingOk)
		{
			MessageBox.Show("عفوا، يوجد عمليات جارية الان \r\n حاول مرة اخري عند الانتهاء .", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			return;
		}
		IsClosingOk = false;
		try
		{
			LoginDetails.IsFrmCollect = true;
			FrmMahlyCollecting frmMahlyCollecting = new FrmMahlyCollecting();
			frmMahlyCollecting.ShowDialog();
			if (!frmMahlyCollecting.IsBtnOpen)
			{
				if (frmMahlyCollecting.IsMoneyPay)
				{
					if (frmMahlyCollecting.LblType.Text == "1")
					{
						if (Convert.ToDecimal(frmMahlyCollecting.TxtMoneyOld.Text) > 0m)
						{
							ORD.UpdateMahly(Convert.ToInt32(frmMahlyCollecting.LblOrderID.Text), CostID, Convert.ToDecimal(frmMahlyCollecting.TxtCash.Text), Convert.ToDecimal(frmMahlyCollecting.TxtVisa.Text), IsPaymant: true, Convert.ToInt32(frmMahlyCollecting.LblRoomID.Text), 0);
							MessageBox.Show("تم سداد المبلغ لغرفة رقم ( " + frmMahlyCollecting.TxtRoom.Text + " ) بنجاح", " السداد ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						}
						else
						{
							ORD.UpdateMahly(Convert.ToInt32(frmMahlyCollecting.LblOrderID.Text), CostID, Convert.ToDecimal(frmMahlyCollecting.TxtCash.Text), Convert.ToDecimal(frmMahlyCollecting.TxtVisa.Text), IsPaymant: false, Convert.ToInt32(frmMahlyCollecting.LblRoomID.Text), 0);
							MessageBox.Show("تم سداد المبلغ لغرفة رقم ( " + frmMahlyCollecting.TxtRoom.Text + " ) بنجاح", " السداد ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						}
					}
					else if (Convert.ToDecimal(frmMahlyCollecting.TxtMoneyOld.Text) > 0m)
					{
						ORD.UpdateMahly(Convert.ToInt32(frmMahlyCollecting.LblOrderID.Text), CostID, Convert.ToDecimal(frmMahlyCollecting.TxtCash.Text), Convert.ToDecimal(frmMahlyCollecting.TxtVisa.Text), IsPaymant: true, 0, Convert.ToInt32(frmMahlyCollecting.LblRoomID.Text));
						MessageBox.Show("تم سداد المبلغ لطاولة رقم ( " + frmMahlyCollecting.TxtRoom.Text + " ) بنجاح", " السداد ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					else
					{
						ORD.UpdateMahly(Convert.ToInt32(frmMahlyCollecting.LblOrderID.Text), CostID, Convert.ToDecimal(frmMahlyCollecting.TxtCash.Text), Convert.ToDecimal(frmMahlyCollecting.TxtVisa.Text), IsPaymant: false, 0, Convert.ToInt32(frmMahlyCollecting.LblRoomID.Text));
						MessageBox.Show("تم سداد المبلغ لطاولة رقم ( " + frmMahlyCollecting.TxtRoom.Text + " ) بنجاح", " السداد ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					if (frmMahlyCollecting.CkPrintCheck.Checked)
					{
						Code = Convert.ToInt32(frmMahlyCollecting.LblOrderID.Text);
						if (frmMahlyCollecting.LblType.Text == "1")
						{
							RoomNom = Convert.ToInt32(frmMahlyCollecting.LblRoomID.Text);
							TableNom = 0;
						}
						else if (frmMahlyCollecting.LblType.Text == "2")
						{
							TableNom = Convert.ToInt32(frmMahlyCollecting.LblRoomID.Text);
							RoomNom = 0;
						}
						Thread thread = new Thread((ThreadStart)delegate
						{
							BeginInvoke((Action)delegate
							{
								PrintChik(Code);
							});
						});
						thread.Start();
						RoomNom = 0;
						TableNom = 0;
					}
				}
			}
			else
			{
				int rom = frmMahlyCollecting.Rom;
				GetOrderCashier(rom);
			}
		}
		catch (Exception)
		{
		}
		IsClosingOk = true;
	}

	private void GetOrderCashier(int OrdID)
	{
		Order_Orders order_Orders = ORD.GetPurchases(OrdID).FirstOrDefault();
		datatable.Rows.Clear();
		List<Order_OrdersDetails> itemsPurchesByID = ORD.GetItemsPurchesByID(OrdID);
		TxtCode.Text = order_Orders.PurBranchID.ToString();
		BtnSave.Enabled = false;
		BtnDelete.Enabled = true;
		gridView1.OptionsBehavior.Editable = true;
		ArrangeDatatable();
		IsEnableFalse();
	}

	public void UpdateUnitPriceInGrid(CellValueChangedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (!(row["ظ…"].ToString() == Convert.ToString(num)))
			{
				continue;
			}
			string value = Convert.ToString(e.Value);
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("الكمية");
			string focusedRowCellDisplayText5 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (!(focusedRowCellDisplayText != ""))
			{
				break;
			}
			Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(focusedRowCellDisplayText).FirstOrDefault();
			if (get_Item_ByCodeALL_Result == null)
			{
				break;
			}
			foreach (DataRow row2 in datatable.Rows)
			{
				if (!(row2["ظ…"].ToString() == focusedRowCellDisplayText5))
				{
					continue;
				}
				if (row2["أسم_الوحدة"].ToString() == "الصغرى")
				{
					decimal sellPermission = ORD.GetSellPermission(focusedRowCellDisplayText, UserSellPrice, "الصغرى");
					if (UserSellPrice == 1)
					{
						if (Convert.ToDecimal(value) < sellPermission)
						{
							MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							row2["سعر الوحدة"] = sellPermission;
							return;
						}
					}
					else if (UserSellPrice == 2)
					{
						if (Convert.ToDecimal(value) < sellPermission)
						{
							MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							row2["سعر الوحدة"] = sellPermission;
							return;
						}
					}
					else if (UserSellPrice == 3)
					{
						if (Convert.ToDecimal(value) < sellPermission)
						{
							MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							row2["سعر الوحدة"] = sellPermission;
							return;
						}
					}
					else if (UserSellPrice == 4 && Convert.ToDecimal(value) < sellPermission)
					{
						MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						row2["سعر الوحدة"] = sellPermission;
						return;
					}
					if (LoginDetails.ItemTotalWithVat)
					{
						row2["سعر الوحدة"] = Math.Round(Convert.ToDecimal(value) / (Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100) + 1m), 2);
					}
					else
					{
						row2["سعر الوحدة"] = value;
					}
					row2["TaxTobacco"] = Convert.ToDecimal(value) * Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Tobacco) / 100m;
					decimal num2 = Convert.ToDecimal(row2["TaxTobacco"]);
					if (num2 < LoginDetails.MinTobaccoTax && num2 != 0m)
					{
						row2["TaxTobacco"] = LoginDetails.MinTobaccoTax;
					}
					row2["TaxTobacco"] = Convert.ToDecimal(row2["TaxTobacco"]) * Convert.ToDecimal(focusedRowCellDisplayText4);
					row2["TaxTobaccoDis"] = row2["TaxTobacco"];
					decimal value2 = Convert.ToDecimal(value) * Convert.ToDecimal(focusedRowCellDisplayText4);
					row2["الاجمالى"] = Convert.ToDecimal(value2) + Convert.ToDecimal(row2["TaxTobacco"]);
					if (ORD.IsVatActive())
					{
						if (get_Item_ByCodeALL_Result.Is_Tax == true)
						{
							if (LoginDetails.ItemTotalWithVat)
							{
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), 2);
								row2["الضربية"] = Math.Round(Convert.ToDecimal(row2["إجمالى المبلغ"]) * Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100), DecimalNum);
								row2["هل_ضريبة"] = 1;
								row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
							}
							else
							{
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]) * (Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100) + 1m), 2);
								row2["الضربية"] = Math.Round(Convert.ToDecimal(row2["إجمالى المبلغ"]) - Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
								row2["هل_ضريبة"] = 1;
								row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
							}
						}
						else
						{
							row2["الضربية"] = 0;
							row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
							row2["هل_ضريبة"] = 0;
							row2["قيمة_الضريبة"] = 0;
						}
					}
					else
					{
						row2["الضربية"] = 0;
						row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
						row2["هل_ضريبة"] = 0;
						row2["قيمة_الضريبة"] = 0;
					}
					break;
				}
				if (row2["أسم_الوحدة"].ToString() == "المتوسطة")
				{
					decimal sellPermission2 = ORD.GetSellPermission(focusedRowCellDisplayText, UserSellPrice, "المتوسطة");
					if (UserSellPrice == 1)
					{
						if (Convert.ToDecimal(value) < sellPermission2)
						{
							MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							row2["سعر الوحدة"] = sellPermission2;
							return;
						}
					}
					else if (UserSellPrice == 2)
					{
						if (Convert.ToDecimal(value) < sellPermission2)
						{
							MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							row2["سعر الوحدة"] = sellPermission2;
							return;
						}
					}
					else if (UserSellPrice == 3)
					{
						if (Convert.ToDecimal(value) < sellPermission2)
						{
							MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							row2["سعر الوحدة"] = sellPermission2;
							return;
						}
					}
					else if (UserSellPrice == 4 && Convert.ToDecimal(value) < sellPermission2)
					{
						MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						row2["سعر الوحدة"] = sellPermission2;
						return;
					}
					row2["سعر الوحدة"] = value;
					row2["TaxTobacco"] = Convert.ToDecimal(value) * Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Tobacco) / 100m;
					decimal num3 = Convert.ToDecimal(row2["TaxTobacco"]);
					if (num3 < LoginDetails.MinTobaccoTax && num3 != 0m)
					{
						row2["TaxTobacco"] = LoginDetails.MinTobaccoTax;
					}
					row2["TaxTobacco"] = Convert.ToDecimal(row2["TaxTobacco"]) * Convert.ToDecimal(focusedRowCellDisplayText4);
					row2["TaxTobaccoDis"] = row2["TaxTobacco"];
					decimal value3 = Convert.ToDecimal(value) * Convert.ToDecimal(focusedRowCellDisplayText4);
					row2["الاجمالى"] = Convert.ToDecimal(value3) + Convert.ToDecimal(row2["TaxTobacco"]);
					if (ORD.IsVatActive())
					{
						if (get_Item_ByCodeALL_Result.Is_Tax == true)
						{
							row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]) * (Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100) + 1m), 2);
							row2["الضربية"] = Math.Round(Convert.ToDecimal(row2["إجمالى المبلغ"]) - Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
							row2["هل_ضريبة"] = 1;
							row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
						}
						else
						{
							row2["الضربية"] = 0;
							row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
							row2["هل_ضريبة"] = 0;
							row2["قيمة_الضريبة"] = 0;
						}
					}
					else
					{
						row2["الضربية"] = 0;
						row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
						row2["هل_ضريبة"] = 0;
						row2["قيمة_الضريبة"] = 0;
					}
					break;
				}
				if (!(row2["أسم_الوحدة"].ToString() == "الكبرى"))
				{
					continue;
				}
				decimal sellPermission3 = ORD.GetSellPermission(focusedRowCellDisplayText, UserSellPrice, "الكبرى");
				if (UserSellPrice == 1)
				{
					if (Convert.ToDecimal(value) < sellPermission3)
					{
						MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						row2["سعر الوحدة"] = sellPermission3;
						return;
					}
				}
				else if (UserSellPrice == 2)
				{
					if (Convert.ToDecimal(value) < sellPermission3)
					{
						MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						row2["سعر الوحدة"] = sellPermission3;
						return;
					}
				}
				else if (UserSellPrice == 3)
				{
					if (Convert.ToDecimal(value) < sellPermission3)
					{
						MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						row2["سعر الوحدة"] = sellPermission3;
						return;
					}
				}
				else if (UserSellPrice == 4 && Convert.ToDecimal(value) < sellPermission3)
				{
					MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					row2["سعر الوحدة"] = sellPermission3;
					return;
				}
				row2["سعر الوحدة"] = value;
				row2["TaxTobacco"] = Convert.ToDecimal(value) * Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Tobacco) / 100m;
				decimal num4 = Convert.ToDecimal(row2["TaxTobacco"]);
				if (num4 < LoginDetails.MinTobaccoTax && num4 != 0m)
				{
					row2["TaxTobacco"] = LoginDetails.MinTobaccoTax;
				}
				row2["TaxTobacco"] = Convert.ToDecimal(row2["TaxTobacco"]) * Convert.ToDecimal(focusedRowCellDisplayText4);
				row2["TaxTobaccoDis"] = row2["TaxTobacco"];
				decimal value4 = Convert.ToDecimal(value) * Convert.ToDecimal(focusedRowCellDisplayText4);
				row2["الاجمالى"] = Convert.ToDecimal(value4) + Convert.ToDecimal(row2["TaxTobacco"]);
				if (ORD.IsVatActive())
				{
					if (get_Item_ByCodeALL_Result.Is_Tax == true)
					{
						row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]) * (Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100) + 1m), 2);
						row2["الضربية"] = Math.Round(Convert.ToDecimal(row2["إجمالى المبلغ"]) - Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
						row2["هل_ضريبة"] = 1;
						row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
					}
					else
					{
						row2["الضربية"] = 0;
						row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
						row2["هل_ضريبة"] = 0;
						row2["قيمة_الضريبة"] = 0;
					}
				}
				else
				{
					row2["الضربية"] = 0;
					row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
					row2["هل_ضريبة"] = 0;
					row2["قيمة_الضريبة"] = 0;
				}
				break;
			}
			insertnewrow();
			CheckFfistRow();
			GetTotalCost();
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
			break;
		}
		insertnewrow();
		CheckFfistRow();
		GetTotalCost();
		gridView1.FocusedRowHandle = e.RowHandle;
		gridView1.FocusedColumn = gridView1.VisibleColumns[3];
		gridView1.ShowEditor();
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
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void GridPlusQty(decimal Qty)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (!(row["ظ…"].ToString() == Convert.ToString(num)))
			{
				continue;
			}
			row["الكمية"] = Qty;
			ItmID2 = row["كود_الصنف"].ToString();
			Item_Items itemsCode = ORD.GetItemsCode2(ItmID2);
			row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
			if (ORD.IsVatActive())
			{
				if (itemsCode.Is_Tax == true)
				{
					decimal value = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]);
					decimal? tax_Value = itemsCode.Tax_Value;
					row["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value * tax_Value / (decimal?)100), DecimalNum);
					string text = row["الضربية"].ToString();
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					decimal value2 = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]);
					decimal? tax_Value2 = itemsCode.Tax_Value;
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]))), DecimalNum);
					row["هل_ضريبة"] = 1;
					row["قيمة_الضريبة"] = itemsCode.Tax_Value;
				}
				else
				{
					row["الضربية"] = 0.0;
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					row["هل_ضريبة"] = 0;
					row["قيمة_الضريبة"] = 0;
				}
			}
			else
			{
				row["الضربية"] = 0.0;
				row["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"])), DecimalNum);
				row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"])), DecimalNum);
				row["هل_ضريبة"] = 0;
				row["قيمة_الضريبة"] = 0;
			}
			break;
		}
		ArrangeDatatable();
		GetTotalCost();
		Cal_FinalResultTotals();
		gridControl1.DataSource = datatable;
	}

	private void GridPlus()
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			decimal value = Convert.ToDecimal(row["سعر الوحدة"].ToString());
			if (!(row["ظ…"].ToString() == Convert.ToString(num)))
			{
				continue;
			}
			row["الكمية"] = Convert.ToDecimal(row["الكمية"]) + 1m;
			ItmID2 = row["كود_الصنف"].ToString();
			Item_Items itemsCode = ORD.GetItemsCode2(ItmID2);
			row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
			row["TaxTobacco"] = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(itemsCode.Tax_Tobacco) / 100m;
			decimal num2 = Convert.ToDecimal(row["TaxTobacco"]);
			if (num2 < LoginDetails.MinTobaccoTax && num2 != 0m)
			{
				row["TaxTobacco"] = LoginDetails.MinTobaccoTax;
			}
			row["TaxTobacco"] = Convert.ToDecimal(row["TaxTobacco"]) * Convert.ToDecimal(row["الكمية"]);
			row["TaxTobaccoDis"] = row["TaxTobacco"];
			decimal value2 = Convert.ToDecimal(value) * Convert.ToDecimal(row["الكمية"]);
			row["الاجمالى"] = Convert.ToDecimal(value2) + Convert.ToDecimal(row["TaxTobacco"]);
			if (ORD.IsVatActive())
			{
				if (itemsCode.Is_Tax == true)
				{
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]) * (Convert.ToDecimal(itemsCode.Tax_Value / (decimal?)100) + 1m), 2);
					row["الضربية"] = Math.Round(Convert.ToDecimal(row["إجمالى المبلغ"]) - Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
					row["هل_ضريبة"] = 1;
					row["قيمة_الضريبة"] = itemsCode.Tax_Value;
				}
				else
				{
					row["الضربية"] = 0;
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
					row["هل_ضريبة"] = 0;
					row["قيمة_الضريبة"] = 0;
				}
			}
			else
			{
				row["الضربية"] = 0;
				row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
				row["هل_ضريبة"] = 0;
				row["قيمة_الضريبة"] = 0;
			}
		}
		ArrangeDatatable();
		Cal_FinalResultTotals();
		gridControl1.DataSource = datatable;
	}

	private void GridMinus()
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			decimal value = Convert.ToDecimal(row["سعر الوحدة"].ToString());
			if (!(row["ظ…"].ToString() == Convert.ToString(num)))
			{
				continue;
			}
			row["الكمية"] = Convert.ToDecimal(row["الكمية"]) - 1m;
			if (Convert.ToDecimal(row["الكمية"]) <= 0m)
			{
				row["الكمية"] = 1;
			}
			row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
			ItmID2 = row["كود_الصنف"].ToString();
			Item_Items itemsCode = ORD.GetItemsCode2(ItmID2);
			row["TaxTobacco"] = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(itemsCode.Tax_Tobacco) / 100m;
			decimal num2 = Convert.ToDecimal(row["TaxTobacco"]);
			if (num2 < LoginDetails.MinTobaccoTax && num2 != 0m)
			{
				row["TaxTobacco"] = LoginDetails.MinTobaccoTax;
			}
			row["TaxTobacco"] = Convert.ToDecimal(row["TaxTobacco"]) * Convert.ToDecimal(row["الكمية"]);
			row["TaxTobaccoDis"] = row["TaxTobacco"];
			decimal value2 = Convert.ToDecimal(value) * Convert.ToDecimal(row["الكمية"]);
			row["الاجمالى"] = Convert.ToDecimal(value2) + Convert.ToDecimal(row["TaxTobacco"]);
			if (ORD.IsVatActive())
			{
				if (itemsCode.Is_Tax == true)
				{
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]) * (Convert.ToDecimal(itemsCode.Tax_Value / (decimal?)100) + 1m), 2);
					row["الضربية"] = Math.Round(Convert.ToDecimal(row["إجمالى المبلغ"]) - Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
					row["هل_ضريبة"] = 1;
					row["قيمة_الضريبة"] = itemsCode.Tax_Value;
				}
				else
				{
					row["الضربية"] = 0;
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
					row["هل_ضريبة"] = 0;
					row["قيمة_الضريبة"] = 0;
				}
			}
			else
			{
				row["الضربية"] = 0;
				row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
				row["هل_ضريبة"] = 0;
				row["قيمة_الضريبة"] = 0;
			}
		}
		ArrangeDatatable();
		GetTotalCost();
		Cal_FinalResultTotals();
		gridControl1.DataSource = datatable;
	}

	private void GridDelete()
	{
		try
		{
			int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
			SN = Convert.ToInt32(gridView1.GetFocusedRowCellDisplayText("ظ…"));
			ItmID = Convert.ToInt32(gridView1.GetFocusedRowCellDisplayText("ItemID"));
			ADDIdd = Convert.ToInt32(gridView1.GetFocusedRowCellDisplayText("ADDId"));
			foreach (DataRow row in datatable.Rows)
			{
				if (row["ظ…"].ToString() == Convert.ToString(SN) && row["ItemIDADD"].ToString() == Convert.ToString(ItmID) && row["ItemID"].ToString() == Convert.ToString(ItmID) && row["ADDId"].ToString() == Convert.ToString(ADDIdd))
				{
					row.Delete();
					break;
				}
				if (!(row["ظ…"].ToString() == Convert.ToString(SN)) || !(row["ItemID"].ToString() == Convert.ToString(ItmID)) || !(row["ADDId"].ToString() == Convert.ToString(ADDIdd)))
				{
					continue;
				}
				if (row["ظ…"].ToString() == Convert.ToString(SN) && row["ItemID"].ToString() == Convert.ToString(ItmID) && row["ADDId"].ToString() == "0")
				{
					for (int num2 = datatable.Rows.Count - 1; num2 >= 0; num2--)
					{
						DataRow dataRow2 = datatable.Rows[num2];
						if (dataRow2["ظ…"].ToString() == Convert.ToString(SN) && dataRow2["Rowid"].ToString() == Convert.ToString(SN))
						{
							dataRow2.Delete();
						}
						bool flag = true;
					}
					datatable.AcceptChanges();
				}
				else
				{
					row.Delete();
				}
				break;
			}
			GetTotalCost();
			ArrangeDatatable();
			Cal_FinalResultTotals();
			gridControl1.DataSource = datatable;
		}
		catch (Exception)
		{
		}
	}

	private void Clear()
	{
		Cash = 0m;
		Bank = 0m;
		MoneyNeed = 0m;
		MoneyPaying = 0m;
		MoneyNet = 0m;
		Hanger = 0m;
		RoomNom = 0;
		TableNom = 0;
		TblID = 1;
		isEditingExistingTakeAway = false;
		MenuEdit.Text = LoginDetails.Lang == "ar-EG" ? "تعديل F6" : "Edit F6";
		BtnSave.Enabled = true;
		BtnSaveMahly.Enabled = true;
		BtnSaveTawsel.Enabled = true;
		BtnDelete.Enabled = false;
		TxtCode.Text = "";
		datatable.Rows.Clear();
		gridControl1.DataSource = datatable;
		GetTotalCost();
		TxtDiscount.Text = "";
		TxtDiscountPercent.Value = LoginDetails.DiscountForRestaurant;
		TxtVat.Text = "0.00";
		TxtVatBeforeDisc.Text = "0.00";
		TxtTotals.Text = "0.00";
		TxtTotalBeforDisc.Text = "0.00";
		TxtTotalAll.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtNet22.Text = "0.00";
		TxtCost.Text = "0.00";
		TxtTabocc.Text = "0.00";
		TxtAllVat.Text = "0.00";
		TxtDate.DateTime = DateTime.Now;
		TxtDiscount.Enabled = true;
		TxtDate.Enabled = true;
		SetDateCasheir();
		TxtDiscountALLTotal.ReadOnly = false;
		TxtDiscount.ReadOnly = false;
		TxtDiscountPercent.ReadOnly = false;
		TxtDate.ReadOnly = false;
		TxtPhone.ReadOnly = false;
		TxtPhone.Text = string.Empty;
		LValid.Visible = false;
		LSucced.Visible = false;
		BtnUpload.Visible = false;
	}

	private async Task Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 157))
		{
			if (Convert.ToDecimal(TxtNet.Text) <= 0m)
			{
				MessageBox.Show("لا يمكن حفظ الفاتورة، اجمالي الفاتوره = 0", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return;
			}
			int Result = 0;
			int CostID = Convert.ToInt32(Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID);
			if (datatable.Rows.Count >= 1)
			{
				if (IsItemPriceZero() == 0)
				{
					return;
				}
				GetTotalCost();
				Cal_FinalResultTotals();
				LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
				LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum);
				LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
				FrmMoneySafry FrmShow = new FrmMoneySafry();
				FrmShow.ShowDialog();
				if (!FrmShow.IsComplateMony)
				{
					return;
				}
				Cash = Convert.ToDecimal(FrmShow.TxtCash.Text);
				Bank = Convert.ToDecimal(FrmShow.TxtVisa.Text);
				MoneyNeed = Convert.ToDecimal(FrmShow.TxtCash.Text);
				MoneyPaying = Convert.ToDecimal(FrmShow.TxtVisa.Text);
				MoneyNet = Convert.ToDecimal(FrmShow.TxtMoneyNet.Text);
				FilterDataTableSave(datatable, Result, IsWait: false);
				Result = ORD.SaveOrdersRestaurant_TakeAway(LoginDetails.Acc_CustomerCash, IsWaiting: false, "عميل نقدي", TxtPhone.Text, "", TxtDate.DateTime, 1, "مدفوعة", 1, 0, CostID, "", "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtTotals.Text), Convert.ToDecimal(TxtTotalAll.Text), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, OrderCashierType: false, 0, 0, dtCurrentTable, Sectoral: false, LoginDetails.UserName, DateTime.Now, "", "", "", "", "", "", "", "", 0m, 0m, 0m);
				Hanger = 0m;
				RoomNom = 0;
				TableNom = 0;
				if (Result > 0)
				{
					Code = Result;
					BtnSave.Enabled = false;
					BtnDelete.Enabled = true;
					TxtCode.Text = Result.ToString();
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
						await resultElectronicInvoiceXmlService.AddResult(LoginDetails.UserID, LoginDetails.CsIdForUser, res, LoginDetails.BranchID, isSuccess, inv.OrderId, OrderDto.Purchases_Date.Value, inv.InvoiceTypeName, "مبيعات كاشير المطعم");
					}
					BtnSave.Enabled = false;
					Thread workerThread = new Thread((ThreadStart)delegate
					{
						BeginInvoke((Action)delegate
						{
							Print(Code);
						});
					});
					workerThread.Start();
					if (LoginDetails.IsPrintItemCook)
					{
						Thread workerThread4 = new Thread((ThreadStart)delegate
						{
							BeginInvoke((Action)delegate
							{
								PrintSecondCasheir(Code);
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
					Clear();
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات فاتورة الكاشير رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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

	private async Task SaveM()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 157))
		{
			if (Convert.ToDecimal(TxtNet.Text) <= 0m)
			{
				MessageBox.Show("لا يمكن حفظ الفاتورة، اجمالي الفاتوره = 0", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return;
			}
			int Result = 0;
			int CostID = Convert.ToInt32(Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID);
			if (datatable.Rows.Count >= 1)
			{
				if (IsItemPriceZero() == 0)
				{
					return;
				}
				GetTotalCost();
				Cal_FinalResultTotals();
				LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
				LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum);
				LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
				FrmMoneySafry FrmShow = new FrmMoneySafry();
				FrmShow.ShowDialog();
				if (!FrmShow.IsComplateMony)
				{
					return;
				}
				Cash = Convert.ToDecimal(FrmShow.TxtCash.Text);
				Bank = Convert.ToDecimal(FrmShow.TxtVisa.Text);
				MoneyNeed = Convert.ToDecimal(FrmShow.TxtCash.Text);
				MoneyPaying = Convert.ToDecimal(FrmShow.TxtVisa.Text);
				MoneyNet = Convert.ToDecimal(FrmShow.TxtMoneyNet.Text);
				FilterDataTableSave(datatable, Result, IsWait: false);
				Result = ORD.SaveRestaurant(LoginDetails.Acc_Room, LoginDetails.Acc_RoomAccount, IsWaiting: false, "عميل محلي", TxtPhone.Text, "", TxtDate.DateTime, 2, "تعليق", 2, CostID, "", "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, OrderCashierType: false, RoomNom, TableNom, ISPasue: true, ISPayment: false, ISDelivery: false, 0, 0, DateTime.Now, dtCurrentTable);
				Hanger = 0m;
				RoomNom = 0;
				TableNom = 0;
				if (Result > 0)
				{
					Code = Result;
					BtnSave.Enabled = false;
					BtnDelete.Enabled = true;
					TxtCode.Text = Result.ToString();
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
						await resultElectronicInvoiceXmlService.AddResult(LoginDetails.UserID, LoginDetails.CsIdForUser, res, LoginDetails.BranchID, isSuccess, inv.OrderId, OrderDto.Purchases_Date.Value, inv.InvoiceTypeName, "مبيعات كاشير المطعم");
					}
					BtnSave.Enabled = false;
					Thread workerThread = new Thread((ThreadStart)delegate
					{
						BeginInvoke((Action)delegate
						{
							Print(Code);
						});
					});
					workerThread.Start();
					if (LoginDetails.IsPrintItemCook)
					{
						Thread workerThread4 = new Thread((ThreadStart)delegate
						{
							BeginInvoke((Action)delegate
							{
								PrintSecondCasheir(Code);
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
					Clear();
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات فاتورة الكاشير رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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

	private async Task SaveMahly()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 157))
		{
			int CostID = Convert.ToInt32(Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID);
			int Result = 0;
			if (datatable.Rows.Count >= 1)
			{
				GetTotalCost();
				Cal_FinalResultTotals();
				LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
				FrmMoneyMahly FrmShow = new FrmMoneyMahly();
				FrmShow.ShowDialog();
				if (!FrmShow.IsComplateMony)
				{
					return;
				}
				if (FrmShow.LblType.Text == "1")
				{
					RoomNom = Convert.ToInt32(FrmShow.LblRoomID.Text);
					TableNom = 0;
				}
				else
				{
					TableNom = Convert.ToInt32(FrmShow.LblRoomID.Text);
					RoomNom = 0;
				}
				if (FrmShow.TogglePause.IsOn)
				{
					if (!FrmShow.RadioPause.Checked)
					{
						if (FrmShow.LblType.Text == "1")
						{
							RoomNom = Convert.ToInt32(FrmShow.LblRoomID.Text);
							TableNom = 0;
						}
						else
						{
							TableNom = Convert.ToInt32(FrmShow.LblRoomID.Text);
							RoomNom = 0;
						}
						Hanger = 0m;
						string TimeB = Convert.ToDateTime(FrmShow.TxtDate1.Text).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(FrmShow.TxtTime1.Text).ToString("HH:mm:ss");
						DateTime TimeA = Convert.ToDateTime(TimeB);
						FilterDataTableSave(datatable, Result, IsWait: false);
						Result = ORD.SaveRestaurant(LoginDetails.Acc_Room, LoginDetails.Acc_RoomAccount, IsWaiting: false, "عميل محلي", TxtPhone.Text, "", TxtDate.DateTime, 2, "تعليق", 2, CostID, "", "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, OrderCashierType: false, RoomNom, TableNom, ISPasue: true, ISPayment: false, ISDelivery: false, 0, 0, TimeA, dtCurrentTable);
						TxtCode.Text = Result.ToString();
						Code = Result;
						Cash = 0m;
						Bank = 0m;
					}
					else
					{
						int OrdID = (Code = Convert.ToInt32(FrmShow.LblOrderID.Text));
						Hanger = 0m;
						if (FrmShow.LblType.Text == "1")
						{
							RoomNom = Convert.ToInt32(FrmShow.LblRoomID.Text);
							TableNom = 0;
						}
						else
						{
							TableNom = Convert.ToInt32(FrmShow.LblRoomID.Text);
							RoomNom = 0;
						}
						string TimeB2 = Convert.ToDateTime(FrmShow.TxtDate1.Text).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(FrmShow.TxtTime1.Text).ToString("HH:mm:ss");
						DateTime TimeA2 = Convert.ToDateTime(TimeB2);
						FilterDataTableSave(datatable, Result, IsWait: false);
						Result = ORD.UpdateRestaurant(OrdID, LoginDetails.Acc_Room, IsWaiting: false, "عميل محلي", TxtPhone.Text, "", TxtDate.DateTime, 2, "تعليق", 2, CostID, "", "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, OrderCashierType: false, RoomNom, TableNom, ISPasue: true, ISPayment: false, TimeA2, dtCurrentTable);
						TxtCode.Text = OrdID.ToString();
						Cash = 0m;
						Bank = 0m;
					}
				}
				else if (!FrmShow.RadioPause.Checked)
				{
					if (FrmShow.LblType.Text == "1")
					{
						RoomNom = Convert.ToInt32(FrmShow.LblRoomID.Text);
						TableNom = 0;
					}
					else
					{
						TableNom = Convert.ToInt32(FrmShow.LblRoomID.Text);
						RoomNom = 0;
					}
					Cash = Convert.ToDecimal(FrmShow.TxtCash.Text);
					Bank = Convert.ToDecimal(FrmShow.TxtVisa.Text);
					string TimeB3 = Convert.ToDateTime(FrmShow.TxtDate1.Text).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(FrmShow.TxtTime1.Text).ToString("HH:mm:ss");
					DateTime TimeA3 = Convert.ToDateTime(TimeB3);
					FilterDataTableSave(datatable, Result, IsWait: false);
					Result = ORD.SaveRestaurant(LoginDetails.Acc_Room, LoginDetails.Acc_RoomAccount, IsWaiting: false, "عميل محلي", TxtPhone.Text, "", TxtDate.DateTime, 1, "مدفوعة", 2, CostID, "", "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, OrderCashierType: false, RoomNom, TableNom, ISPasue: true, ISPayment: true, ISDelivery: false, 0, 0, TimeA3, dtCurrentTable);
					TxtCode.Text = Result.ToString();
					Code = Result;
					Hanger = 0m;
				}
				else
				{
					int OrdID2 = (Code = Convert.ToInt32(FrmShow.LblOrderID.Text));
					Cash = Convert.ToDecimal(FrmShow.TxtCash.Text);
					Bank = Convert.ToDecimal(FrmShow.TxtVisa.Text);
					if (FrmShow.LblType.Text == "1")
					{
						RoomNom = Convert.ToInt32(FrmShow.LblRoomID.Text);
						TableNom = 0;
					}
					else
					{
						TableNom = Convert.ToInt32(FrmShow.LblRoomID.Text);
						RoomNom = 0;
					}
					string TimeB4 = Convert.ToDateTime(FrmShow.TxtDate1.Text).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(FrmShow.TxtTime1.Text).ToString("HH:mm:ss");
					DateTime TimeA4 = Convert.ToDateTime(TimeB4);
					FilterDataTableSave(datatable, Result, IsWait: false);
					Result = ORD.UpdateRestaurant(OrdID2, LoginDetails.Acc_Room, IsWaiting: false, "عميل نقدي", TxtPhone.Text, "", TxtDate.DateTime, 1, "تعليق", 2, CostID, "", "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, OrderCashierType: false, RoomNom, TableNom, ISPasue: true, ISPayment: true, TimeA4, dtCurrentTable);
					TxtCode.Text = OrdID2.ToString();
					Hanger = 0m;
				}
				if (Result > 0)
				{
					Code = Result;
					BtnSave.Enabled = false;
					BtnDelete.Enabled = true;
					TxtCode.Text = Result.ToString();
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
						await resultElectronicInvoiceXmlService.AddResult(LoginDetails.UserID, LoginDetails.CsIdForUser, res, LoginDetails.BranchID, isSuccess, inv.OrderId, OrderDto.Purchases_Date.Value, inv.InvoiceTypeName, "مبيعات كاشير المطعم");
					}
					ReturantType = "طلب محلي";
					if (FrmShow.CkPrintCheck.Checked)
					{
						Thread workerThread = new Thread((ThreadStart)delegate
						{
							BeginInvoke((Action)delegate
							{
								Print(Code);
							});
						});
						workerThread.Start();
						if (LoginDetails.IsPrintSecondFatoraCasheir)
						{
							Thread workerThread2 = new Thread((ThreadStart)delegate
							{
								BeginInvoke((Action)delegate
								{
									PrintSecondDefualt(Code);
								});
							});
							workerThread2.Start();
						}
					}
					if (FrmShow.CkPrintCook.Checked)
					{
						Thread workerThread3 = new Thread((ThreadStart)delegate
						{
							BeginInvoke((Action)delegate
							{
								PrintSecondCasheir(Code);
							});
						});
						workerThread3.Start();
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
					}
					Clear();
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات فاتورة الكاشير رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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

	private async Task SaveDelivery()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 157))
		{
			int Result = 0;
			int CostID = Convert.ToInt32(Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID);
			if (datatable.Rows.Count >= 1)
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
				LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
				FrmMoneyDelivery FrmShow = new FrmMoneyDelivery();
				FrmShow.ShowDialog();
				if (!FrmShow.IsComplateMony)
				{
					return;
				}
				if (FrmShow.ToggelHanger.IsOn)
				{
					Hanger = Convert.ToDecimal(FrmShow.TxtHanger.Text);
					DateTime TimeA = DateTime.Now;
					FilterDataTableSave(datatable, Result, IsWait: false);
					Result = ORD.SaveRestaurant(Convert.ToInt32(FrmShow.lblComp.Text), Convert.ToInt32(FrmShow.LblCompAccount.Text), IsWaiting: false, FrmShow.TxtCustName.Text, FrmShow.TxtCustPhone.Text, "", TxtDate.DateTime, 2, "هنقرستيشن", 3, CostID, FrmShow.TxtCustPlace.Text, "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, OrderCashierType: false, 0, 0, ISPasue: false, ISPayment: true, ISDelivery: true, 1, Convert.ToInt32(FrmShow.LblDelivery.Text), TimeA, dtCurrentTable);
					Cash = 0m;
					Bank = 0m;
					MoneyNeed = 0m;
					MoneyPaying = 0m;
					MoneyNet = 0m;
					RoomNom = 0;
					TableNom = 0;
				}
				else if (FrmShow.ToggleDelivery.IsOn)
				{
					Hanger = Convert.ToDecimal(FrmShow.TxtDelivery.Text);
					DateTime TimeA2 = DateTime.Now;
					FilterDataTableSave(datatable, Result, IsWait: false);
					Result = ORD.SaveRestaurant(LoginDetails.Acc_Tawsel, LoginDetails.Acc_TawselAccount, IsWaiting: false, FrmShow.TxtCustName.Text, FrmShow.TxtCustPhone.Text, "", TxtDate.DateTime, 2, "غير مدفوعة", 3, CostID, FrmShow.TxtCustPlace.Text, "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, OrderCashierType: false, 0, 0, ISPasue: false, ISPayment: true, ISDelivery: true, 2, Convert.ToInt32(FrmShow.LblDelivery.Text), TimeA2, dtCurrentTable);
					Cash = 0m;
					Bank = 0m;
					MoneyNeed = 0m;
					MoneyPaying = 0m;
					MoneyNet = 0m;
					RoomNom = 0;
					TableNom = 0;
				}
				else
				{
					Cash = Convert.ToDecimal(FrmShow.TxtCash.Text);
					Bank = Convert.ToDecimal(FrmShow.TxtVisa.Text);
					MoneyNeed = Convert.ToDecimal(FrmShow.TxtCash.Text);
					MoneyPaying = Convert.ToDecimal(FrmShow.TxtVisa.Text);
					DateTime TimeA3 = DateTime.Now;
					FilterDataTableSave(datatable, Result, IsWait: false);
					Result = ORD.SaveRestaurant(LoginDetails.Acc_CustomerCash, LoginDetails.Acc_CashBox, IsWaiting: false, FrmShow.TxtCustName.Text, FrmShow.TxtCustPhone.Text, "", TxtDate.DateTime, 1, "مدفوعة", 3, CostID, FrmShow.TxtCustPlace.Text, "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, OrderCashierType: false, 0, 0, ISPasue: false, ISPayment: true, ISDelivery: true, 3, Convert.ToInt32(FrmShow.LblDelivery.Text), TimeA3, dtCurrentTable);
					Hanger = 0m;
					RoomNom = 0;
					TableNom = 0;
				}
				if (Result > 0)
				{
					Code = Result;
					BtnSave.Enabled = false;
					BtnDelete.Enabled = true;
					TxtCode.Text = Result.ToString();
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
						await resultElectronicInvoiceXmlService.AddResult(LoginDetails.UserID, LoginDetails.CsIdForUser, res, LoginDetails.BranchID, isSuccess, inv.OrderId, OrderDto.Purchases_Date.Value, inv.InvoiceTypeName, "مبيعات كاشير المطعم");
					}
					Thread workerThread = new Thread((ThreadStart)delegate
					{
						BeginInvoke((Action)delegate
						{
							Print(Code);
						});
					});
					workerThread.Start();
					if (LoginDetails.IsPrintItemCook)
					{
						Thread workerThread2 = new Thread((ThreadStart)delegate
						{
							BeginInvoke((Action)delegate
							{
								PrintItemCook(Code);
							});
						});
						workerThread2.Start();
					}
					if (LoginDetails.IsPrintSecondFatoraCasheir)
					{
						Thread workerThread3 = new Thread((ThreadStart)delegate
						{
							BeginInvoke((Action)delegate
							{
								PrintSecondDefualt(Code);
							});
						});
						workerThread3.Start();
					}
					Thread workerThread4 = new Thread((ThreadStart)delegate
					{
						BeginInvoke((Action)delegate
						{
							PrintSecondCasheir(Code);
						});
					});
					workerThread4.Start();
					Clear();
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات فاتورة الكاشير رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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

	private void Delete()
	{
		if (isEditingExistingTakeAway)
		{
			MessageBox.Show("احفظ التعديلات أو ألغِها قبل حذف الفاتورة.", "تعديلات غير محفوظة",
				MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3,
				MessageBoxOptions.RightAlign);
			return;
		}

		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 157))
		{
			int num = 0;
			int num2 = 0;
			if (TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة الكاشير رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
				{
					int ORDIDRES = Convert.ToInt32(TxtCode.Text);
					num = ORD.Delete(ORDIDRES);
					ORD.DeleteTranDetails(ORDIDRES);
					num2 = ORD.DeleteTran(ORDIDRES);
					GTSdbEntities gTSdbEntities = new GTSdbEntities();
					Restaurant_Room restaurant_Room = gTSdbEntities.Restaurant_Room.Where((Restaurant_Room x) => x.OrderID == (int?)ORDIDRES).FirstOrDefault();
					Restaurant_Table restaurant_Table = gTSdbEntities.Restaurant_Table.Where((Restaurant_Table x) => x.OrderID == (int?)ORDIDRES).FirstOrDefault();
					if (restaurant_Room != null)
					{
						ORD.UpdateRoom(ORDIDRES);
					}
					if (restaurant_Table != null)
					{
						ORD.UpdateTable(ORDIDRES);
					}
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات فاتورة الكاشير رقم  ( " + ORDIDRES + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						BtnDelete.Enabled = false;
						BtnSave.Enabled = true;
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات فاتورة الكاشير رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
							if (LoginDetails.IsElectronicInvoice)
							{
								stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirRestaurantZatca.mrt");
							}
							else
							{
								stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirRestaurant.mrt");
							}
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

	private void Print2()
	{
		try
		{
			if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 157))
			{
				if (TxtCode.Text != "")
				{
					ST.Tables["Order_PurchasesTablePrint"].Clear();
					foreach (DataRow row in datatable.Rows)
					{
						DataRow dataRow2 = ST.Order_PurchasesTablePrint.NewRow();
						dataRow2["Item_code"] = row["كود_الصنف"];
						dataRow2["item_Name"] = row["أسم الصنف"];
						dataRow2["item_NameEN"] = row["الاسم الانجليزي"];
						dataRow2["UnitsName"] = row["الوحدة"];
						dataRow2["Quantity"] = row["الكمية"];
						dataRow2["UnitPrice"] = row["سعر الوحدة"];
						dataRow2["VAT"] = row["الضربية"];
						dataRow2["SafyTotal"] = row["إجمالى المبلغ"];
						dataRow2["PrinterID"] = row["PrinterID"];
						dataRow2["PrinterName"] = row["PrinterName"];
						dataRow2["TotalPricesPurch"] = TxtTotalBeforDisc.Text;
						dataRow2["AllTax"] = TxtAllVat.Text;
						dataRow2["DiscountNumPurch"] = TxtDiscount.Text;
						dataRow2["Net"] = TxtNet.Text;
						ST.Order_PurchasesTablePrint.Rows.Add(dataRow2);
					}
					if (ST.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
					{
						StiReport stiReport = new StiReport();
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirRestaurant.mrt");
						stiReport.RegData(ST.Order_PurchasesTablePrint);
						stiReport.Dictionary.Synchronize();
						stiReport.Compile();
						stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
						stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
						stiReport["VatNumComp"] = LoginDetails.VatNum;
						stiReport["Date"] = TxtDate.DateTime;
						stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
						stiReport["Room"] = RoomNom;
						stiReport["Table"] = TableNom;
						stiReport["Tafqit"] = txtWordAr;
						stiReport["EmployeeName"] = LoginDetails.UserName;
						stiReport["CashMoney"] = Cash.ToString();
						stiReport["CashBank"] = Bank.ToString();
						stiReport["MoneyNeed"] = MoneyNeed.ToString();
						stiReport["MoneyPaying"] = MoneyPaying.ToString();
						stiReport["MoneyNet"] = MoneyNet.ToString();
						string returantType = ReturantType;
						stiReport["CashierType"] = returantType;
						stiReport["PrintComp"] = LoginDetails.PrintComp;
						stiReport["MobileComp"] = LoginDetails.Mobile;
						stiReport["AddressComp"] = LoginDetails.Address;
						Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
						string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString());
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

	private void PrintChik(int Code)
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

	private void PrintSecondCasheir(int Code)
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
								SqlCommand sqlCommand2 = new SqlCommand("Print_Order_Cashier", sqlConnection);
								sqlCommand2.CommandType = CommandType.StoredProcedure;
								SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2);
								sqlCommand2.Parameters.AddWithValue("@ID", Code);
								sqlCommand2.Parameters.AddWithValue("@PrinterID", Convert.ToInt32(item.ID));
								sqlCommand2.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
								sqlDataAdapter2.Fill(dataSetALL.Order_PurchasesTablePrint);
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
										PrinterName = item.Printer
									};
									try
									{
										stiReport.Print(showPrintDialog: false, printerSettings);
									}
									catch
									{
									}
								}
							}
						}
						sqlConnection.Close();
						ORD.UpdateOrderDetialsIsPrint(Code);
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

	private void PrintSecondCasheir2()
	{
		try
		{
			if (!LoginDetails.IsPrintSecondCasheir)
			{
				return;
			}
			if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 157))
			{
				if (TxtCode.Text != "")
				{
					SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
					ST.Tables["TapleItemGroupToCashier"].Clear();
					SqlCommand sqlCommand = new SqlCommand("Get_ItemGroupToPrintCashier", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
					sqlDataAdapter.Fill(ST.TapleItemGroupToCashier);
					ST.Tables["Order_PurchasesTablePrint"].Clear();
					foreach (DataRow row in datatable.Rows)
					{
						DataRow dataRow2 = ST.Order_PurchasesTablePrint.NewRow();
						dataRow2["Item_code"] = row["كود_الصنف"];
						dataRow2["item_Name"] = row["أسم الصنف"];
						dataRow2["item_NameEN"] = row["الاسم الانجليزي"];
						dataRow2["UnitsName"] = row["الوحدة"];
						dataRow2["Quantity"] = row["الكمية"];
						dataRow2["UnitPrice"] = row["سعر الوحدة"];
						dataRow2["VAT"] = row["الضربية"];
						dataRow2["SafyTotal"] = row["إجمالى المبلغ"];
						dataRow2["PrinterID"] = row["PrinterID"];
						dataRow2["PrinterName"] = row["PrinterName"];
						dataRow2["TotalPricesPurch"] = TxtTotalBeforDisc.Text;
						dataRow2["AllTax"] = TxtAllVat.Text;
						dataRow2["DiscountNumPurch"] = TxtDiscount.Text;
						dataRow2["Net"] = TxtNet.Text;
						ST.Order_PurchasesTablePrint.Rows.Add(dataRow2);
					}
					foreach (DataSetALL.TapleItemGroupToCashierRow item in ST.TapleItemGroupToCashier)
					{
						if (!(item.Printer != ""))
						{
							continue;
						}
						ST.Tables["Cashier_TablePrint"].Clear();
						foreach (DataSetALL.Order_PurchasesTablePrintRow item2 in ST.Order_PurchasesTablePrint.Where((DataSetALL.Order_PurchasesTablePrintRow x) => x.PrinterID == item.ID))
						{
							DataRow dataRow3 = ST.Cashier_TablePrint.NewRow();
							dataRow3["Item_code"] = item2["Item_code"];
							dataRow3["item_Name"] = item2["item_Name"];
							dataRow3["item_NameEN"] = item2["item_NameEN"];
							dataRow3["UnitsName"] = item2["UnitsName"];
							dataRow3["Quantity"] = item2["Quantity"];
							dataRow3["UnitPrice"] = item2["UnitPrice"];
							dataRow3["VAT"] = item2["VAT"];
							dataRow3["SafyTotal"] = item2["SafyTotal"];
							dataRow3["PrinterID"] = item2["PrinterID"];
							dataRow3["PrinterName"] = item2["PrinterName"];
							dataRow3["TotalPricesPurch"] = item2["TotalPricesPurch"];
							dataRow3["AllTax"] = item2["AllTax"];
							dataRow3["DiscountNumPurch"] = item2["DiscountNumPurch"];
							dataRow3["Net"] = item2["Net"];
							ST.Cashier_TablePrint.Rows.Add(dataRow3);
						}
						if (ST.Tables["Cashier_TablePrint"].Rows.Count > 0)
						{
							StiReport stiReport = new StiReport();
							stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirSecond.mrt");
							stiReport.RegData(ST.Cashier_TablePrint);
							stiReport.Dictionary.Synchronize();
							stiReport.Compile();
							stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
							stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
							stiReport["VatNumComp"] = LoginDetails.VatNum;
							stiReport["Date"] = TxtDate.DateTime;
							stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
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
							stiReport["CashierType"] = ReturantType;
							stiReport["Room"] = RoomNom;
							stiReport["Table"] = TableNom;
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
					sqlConnection.Close();
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

	private void PrintItemCook(int Code)
	{
		try
		{
			if (!LoginDetails.IsPrintItemCook)
			{
				return;
			}
			if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 157))
			{
				if (Code > 0)
				{
					using (SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer))
					{
						DataSetALL dataSetALL = new DataSetALL();
						dataSetALL.Tables["TapleItemGroupToCashier"].Clear();
						SqlCommand sqlCommand = new SqlCommand("Get_ItemGroupToPrintCashierItem", sqlConnection);
						sqlCommand.CommandType = CommandType.StoredProcedure;
						SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
						sqlCommand.Parameters.AddWithValue("@ID", Code);
						sqlDataAdapter.Fill(dataSetALL.TapleItemGroupToCashier);
						foreach (DataSetALL.TapleItemGroupToCashierRow item in dataSetALL.TapleItemGroupToCashier)
						{
							if (item.Printer != "")
							{
								dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
								SqlCommand sqlCommand2 = new SqlCommand("Print_Order_CashierByItemCook", sqlConnection);
								sqlCommand2.CommandType = CommandType.StoredProcedure;
								SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2);
								sqlCommand2.Parameters.AddWithValue("@ID", Code);
								sqlCommand2.Parameters.AddWithValue("@PrinterID", Convert.ToInt32(item.ID));
								sqlCommand2.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
								sqlDataAdapter2.Fill(dataSetALL.Order_PurchasesTablePrint);
								if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
								{
									StiReport stiReport = new StiReport();
									stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirItemCook.mrt");
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
										PrinterName = item.Printer
									};
									stiReport.Print(showPrintDialog: false, printerSettings);
								}
							}
						}
						sqlConnection.Close();
						ORD.UpdateOrderDetialsIsPrintCook(Code);
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

	private void PrintSecondCasheirNoPrint2()
	{
		try
		{
			if (!LoginDetails.IsPrintSecondCasheir)
			{
				return;
			}
			if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 157))
			{
				if (TxtCode.Text != "")
				{
					SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
					DataSetALL dataSetALL = new DataSetALL();
					dataSetALL.Tables["TapleItemGroupToCashier"].Clear();
					SqlCommand sqlCommand = new SqlCommand("Get_ItemGroupToPrintCashier", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
					sqlDataAdapter.Fill(dataSetALL.TapleItemGroupToCashier);
					foreach (DataSetALL.TapleItemGroupToCashierRow item in dataSetALL.TapleItemGroupToCashier)
					{
						if (item.Printer != "")
						{
							dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
							SqlCommand sqlCommand2 = new SqlCommand("Print_Order_CashierNoPrint", sqlConnection);
							sqlCommand2.CommandType = CommandType.StoredProcedure;
							SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2);
							sqlCommand2.Parameters.AddWithValue("@PrinterID", Convert.ToInt32(item.ID));
							sqlCommand2.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
							sqlDataAdapter2.Fill(dataSetALL.Order_PurchasesTablePrint);
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
								stiReport["Date"] = TxtDate.DateTime;
								stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
								stiReport["Room"] = RoomNom;
								stiReport["Table"] = TableNom;
								stiReport["Tafqit"] = txtWordAr;
								stiReport["EmployeeName"] = LoginDetails.UserName;
								stiReport["CashMoney"] = Cash.ToString();
								stiReport["CashBank"] = Bank.ToString();
								stiReport["MoneyNeed"] = MoneyNeed.ToString();
								stiReport["MoneyPaying"] = MoneyPaying.ToString();
								stiReport["MoneyNet"] = MoneyNet.ToString();
								string returantType = ReturantType;
								stiReport["CashierType"] = returantType;
								stiReport["PrintComp"] = LoginDetails.PrintComp;
								stiReport["MobileComp"] = LoginDetails.Mobile;
								stiReport["AddressComp"] = LoginDetails.Address;
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
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PrintSecondCasheirNoPrint()
	{
		try
		{
			if (!LoginDetails.IsPrintSecondCasheir)
			{
				return;
			}
			if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 157))
			{
				if (TxtCode.Text != "")
				{
					SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
					DataSetALL dataSetALL = new DataSetALL();
					dataSetALL.Tables["TapleItemGroupToCashier"].Clear();
					SqlCommand sqlCommand = new SqlCommand("Get_ItemGroupToPrintCashier", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
					sqlDataAdapter.Fill(dataSetALL.TapleItemGroupToCashier);
					foreach (DataSetALL.TapleItemGroupToCashierRow item in dataSetALL.TapleItemGroupToCashier)
					{
						if (item.Printer != "")
						{
							dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
							SqlCommand sqlCommand2 = new SqlCommand("Print_Order_CashierNoPrint", sqlConnection);
							sqlCommand2.CommandType = CommandType.StoredProcedure;
							SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2);
							sqlCommand2.Parameters.AddWithValue("@PrinterID", Convert.ToInt32(item.ID));
							sqlCommand2.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
							sqlDataAdapter2.Fill(dataSetALL.Order_PurchasesTablePrint);
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
								stiReport["Date"] = TxtDate.DateTime;
								stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
								stiReport["Room"] = RoomNom;
								stiReport["Table"] = TableNom;
								stiReport["Tafqit"] = txtWordAr;
								stiReport["EmployeeName"] = LoginDetails.UserName;
								stiReport["CashMoney"] = Cash.ToString();
								stiReport["CashBank"] = Bank.ToString();
								stiReport["MoneyNeed"] = MoneyNeed.ToString();
								stiReport["MoneyPaying"] = MoneyPaying.ToString();
								stiReport["MoneyNet"] = MoneyNet.ToString();
								string returantType = ReturantType;
								stiReport["CashierType"] = returantType;
								stiReport["PrintComp"] = LoginDetails.PrintComp;
								stiReport["MobileComp"] = LoginDetails.Mobile;
								stiReport["AddressComp"] = LoginDetails.Address;
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
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PreviewPrint(int Code)
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
							if (LoginDetails.IsElectronicInvoice)
							{
								stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirRestaurantZatca.mrt");
							}
							else
							{
								stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirRestaurant.mrt");
							}
							stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
							stiReport.Dictionary.Synchronize();
							stiReport.Compile();
							stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
							stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
							stiReport["VatNumComp"] = LoginDetails.VatNum;
							stiReport["Date"] = TxtDate.DateTime;
							stiReport["PurchID"] = Code;
							stiReport["CustVatNum"] = "";
							stiReport["Custphone"] = "";
							stiReport["Tafqit"] = txtWordAr;
							stiReport["EmployeeName"] = LoginDetails.UserName;
							stiReport["CashMoney"] = Cash.ToString();
							stiReport["CashBank"] = Bank.ToString();
							stiReport["MoneyNeed"] = MoneyNeed.ToString();
							stiReport["MoneyPaying"] = MoneyPaying.ToString();
							stiReport["MoneyNet"] = MoneyNet.ToString();
							stiReport["PrintComp"] = LoginDetails.PrintComp;
							stiReport["MobileComp"] = LoginDetails.Mobile;
							stiReport["AddressComp"] = LoginDetails.Address;
							Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
							string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString());
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
							string returantType = ReturantType;
							stiReport["CashierType"] = returantType;
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

	public void IsEnableTrue()
	{
		TxtDiscountALLTotal.ReadOnly = false;
		TxtDiscount.ReadOnly = false;
		TxtDiscountPercent.ReadOnly = false;
		TxtDate.ReadOnly = false;
		TxtPhone.ReadOnly = false;
	}

	public void IsEnableFalse()
	{
		TxtDiscountALLTotal.ReadOnly = true;
		TxtDiscount.ReadOnly = true;
		TxtDiscountPercent.ReadOnly = true;
		TxtDate.ReadOnly = true;
		TxtPhone.ReadOnly = true;
	}

	public void SetInitialRowPrinter()
	{
		CashierPrint.Columns.Add("ID", typeof(int));
		CashierPrint.Columns.Add("Name", typeof(string));
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("ظ…", typeof(int));
		datatable.Columns.Add("RowId", typeof(int));
		datatable.Columns.Add("ADDId", typeof(int));
		datatable.Columns.Add("ItemID", typeof(int));
		datatable.Columns.Add("ItemIDADD", typeof(int));
		datatable.Columns.Add("ItemUnitID", typeof(int));
		datatable.Columns.Add("كود_الصنف", typeof(string));
		datatable.Columns.Add("أسم الصنف", typeof(string));
		datatable.Columns.Add("الاسم الانجليزي", typeof(string));
		datatable.Columns.Add("المجموعة", typeof(string));
		datatable.Columns.Add("الوحدة", typeof(string));
		datatable.Columns.Add("المخزن", typeof(string));
		datatable.Columns.Add("رقم المخزن", typeof(int));
		datatable.Columns.Add("الكمية", typeof(decimal));
		datatable.Columns.Add("متوسط_التكلفة", typeof(decimal));
		datatable.Columns.Add("اجمالي_متوسط_التكلفة", typeof(decimal));
		datatable.Columns.Add("سعر الوحدة", typeof(decimal));
		datatable.Columns.Add("سعربعدالخصم", typeof(decimal));
		datatable.Columns.Add("نسبةالخصم", typeof(decimal));
		datatable.Columns.Add("إجمالى_بعدالخصم", typeof(decimal));
		datatable.Columns.Add("هل_ضريبة", typeof(int));
		datatable.Columns.Add("قيمة_الضريبة", typeof(decimal));
		datatable.Columns.Add("أسم_الوحدة", typeof(string));
		datatable.Columns.Add("الاجمالى", typeof(decimal));
		datatable.Columns.Add("الضربية", typeof(decimal));
		datatable.Columns.Add("الضريبه_بعد_الخصم", typeof(decimal));
		datatable.Columns.Add("إجمالى المبلغ", typeof(decimal));
		datatable.Columns.Add("حذف الصنف", typeof(int));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تم", typeof(int));
		datatable.Columns.Add("PrinterID", typeof(int));
		datatable.Columns.Add("PrinterName", typeof(string));
		datatable.Columns.Add("PrinterIDItem", typeof(int));
		datatable.Columns.Add("PrinterNameItem", typeof(string));
		datatable.Columns.Add("قيمة الخصم", typeof(decimal));
		datatable.Columns.Add("البونص", typeof(decimal));
		datatable.Columns.Add("الطول", typeof(decimal));
		datatable.Columns.Add("العرض", typeof(decimal));
		datatable.Columns.Add("الإرتفاع", typeof(decimal));
		datatable.Columns.Add("الوزن", typeof(decimal));
		datatable.Columns.Add("البيان", typeof(string));
		datatable.Columns.Add("TaxTobacco", typeof(decimal));
		datatable.Columns.Add("TaxTobaccoDis", typeof(decimal));
		datatable.Columns.Add("VatCode", typeof(string));
		datatable.Columns.Add("TaxExemptionReason", typeof(string));
		datatable.Columns.Add("TaxExemptionReasonCode", typeof(string));
		dtCurrentTable.Columns.Add("SN", typeof(int));
		dtCurrentTable.Columns.Add("Purchese_ID", typeof(int));
		dtCurrentTable.Columns.Add("R_ItmSN", typeof(int));
		dtCurrentTable.Columns.Add("R_RowId", typeof(int));
		dtCurrentTable.Columns.Add("R_ADDId", typeof(int));
		dtCurrentTable.Columns.Add("ItemID", typeof(int));
		dtCurrentTable.Columns.Add("ItemIDADD", typeof(int));
		dtCurrentTable.Columns.Add("BranchID", typeof(int));
		dtCurrentTable.Columns.Add("IsWaiting", typeof(bool));
		dtCurrentTable.Columns.Add("StoreID", typeof(int));
		dtCurrentTable.Columns.Add("ItemUnitID", typeof(string));
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
		dtCurrentTable.Columns.Add("TafqitUnitId", typeof(decimal));
		dtCurrentTable.Columns.Add("TafqitQunitity", typeof(decimal));
		dtCurrentTable.Columns.Add("TaxTobacco", typeof(decimal));
		dtCurrentTable.Columns.Add("TaxTobaccoDis", typeof(decimal));
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
		gridView1.Columns["الضريبه_بعد_الخصم"].Summary.AddRange(new GridSummaryItem[1]
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
		SetInitialDetails();
	}

	public void SetInitialDetails()
	{
		datatableDetails.Columns.Add("ظ…", typeof(int));
		datatableDetails.Columns.Add("RowId", typeof(int));
		datatableDetails.Columns.Add("ADDId", typeof(int));
		datatableDetails.Columns.Add("ItemID", typeof(int));
		datatableDetails.Columns.Add("ItemIDADD", typeof(int));
		datatableDetails.Columns.Add("ItemUnitID", typeof(int));
		datatableDetails.Columns.Add("كود_الصنف", typeof(string));
		datatableDetails.Columns.Add("أسم الصنف", typeof(string));
		datatableDetails.Columns.Add("الاسم الانجليزي", typeof(string));
		datatableDetails.Columns.Add("المجموعة", typeof(string));
		datatableDetails.Columns.Add("الوحدة", typeof(string));
		datatableDetails.Columns.Add("المخزن", typeof(string));
		datatableDetails.Columns.Add("رقم المخزن", typeof(int));
		datatableDetails.Columns.Add("الكمية", typeof(decimal));
		datatableDetails.Columns.Add("متوسط_التكلفة", typeof(decimal));
		datatableDetails.Columns.Add("اجمالي_متوسط_التكلفة", typeof(decimal));
		datatableDetails.Columns.Add("سعر الوحدة", typeof(decimal));
		datatableDetails.Columns.Add("سعربعدالخصم", typeof(decimal));
		datatableDetails.Columns.Add("نسبةالخصم", typeof(decimal));
		datatableDetails.Columns.Add("إجمالى_بعدالخصم", typeof(decimal));
		datatableDetails.Columns.Add("هل_ضريبة", typeof(int));
		datatableDetails.Columns.Add("قيمة_الضريبة", typeof(decimal));
		datatableDetails.Columns.Add("أسم_الوحدة", typeof(string));
		datatableDetails.Columns.Add("الاجمالى", typeof(decimal));
		datatableDetails.Columns.Add("الضربية", typeof(decimal));
		datatableDetails.Columns.Add("الضريبه_بعد_الخصم", typeof(decimal));
		datatableDetails.Columns.Add("إجمالى المبلغ", typeof(string));
		datatableDetails.Columns.Add("حذف الصنف", typeof(int));
		datatableDetails.Columns.Add("الطول", typeof(decimal));
		datatableDetails.Columns.Add("العرض", typeof(decimal));
		datatableDetails.Columns.Add("الإرتفاع", typeof(decimal));
		datatableDetails.Columns.Add("الوزن", typeof(decimal));
		datatableDetails.Columns.Add("قيمة الخصم", typeof(decimal));
		datatableDetails.Columns.Add("مسح", typeof(int));
		datatableDetails.Columns.Add("تم", typeof(int));
		datatableDetails.Columns.Add("PrinterID", typeof(int));
		datatableDetails.Columns.Add("PrinterName", typeof(string));
		datatableDetails.Columns.Add("PrinterIDItem", typeof(int));
		datatableDetails.Columns.Add("PrinterNameItem", typeof(string));
	}

	public int IsItemPriceZero()
	{
		return 1;
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
		object obj = datatable.Compute("SUM(الضربية)", "");
		TxtVat.Text = Convert.ToString(obj);
		object obj2 = datatable.Compute("SUM(الاجمالى)", "");
		TxtTotals.Text = Convert.ToString(obj2);
		foreach (DataRow row in datatable.Rows)
		{
			if (row["إجمالى المبلغ"] == DBNull.Value || !decimal.TryParse(row["إجمالى المبلغ"].ToString(), out var _))
			{
				row["إجمالى المبلغ"] = 0;
			}
		}
		object obj3 = datatable.Compute("SUM([إجمالى المبلغ])", "");
		TxtNet22.Text = Convert.ToString(obj3);
		TxtNet.Text = Convert.ToString(obj3);
	}

	public void insertnewrow()
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["ظ…"].ToString() == Convert.ToString(num))
			{
				DataRow dataRow2 = null;
				dataRow2 = datatableDetails.NewRow();
				dataRow2["كود_الصنف"] = "";
				dataRow2["أسم الصنف"] = string.Empty;
				dataRow2["المجموعة"] = 1;
				dataRow2["الوحدة"] = string.Empty;
				dataRow2["سعر الوحدة"] = 0;
				dataRow2["الكمية"] = 1;
				dataRow2["المخزن"] = ORD.GetStoreName();
				dataRow2["رقم المخزن"] = ORD.GetStoreID();
				dataRow2["تم"] = 0;
				dataRow2["الطول"] = 1;
				dataRow2["العرض"] = 1;
				dataRow2["الإرتفاع"] = 1;
				dataRow2["الوزن"] = 1;
				dataRow2["قيمة الخصم"] = 0;
				dataRow2["نسبةالخصم"] = 0;
				datatableDetails.Rows.Add(dataRow2);
				gridControl1.DataSource = datatableDetails;
				break;
			}
		}
	}

	public void InsertNewItem(int itemid)
	{
		bool flag = false;
		Item_Items items = ORD.GetItems(itemid);
		Printer printer = ORD.GetPrinter(itemid);
		decimal? sellPriceSmall = items.SellPriceSmall;
		if ((sellPriceSmall.GetValueOrDefault() >= 0m) & sellPriceSmall.HasValue)
		{
			DataRow dataRow = null;
			dataRow = datatable.NewRow();
			dataRow["ظ…"] = TblID;
			dataRow["RowId"] = 0;
			dataRow["ADDId"] = 0;
			dataRow["ItemID"] = Convert.ToInt32(items.ItemId);
			dataRow["ItemIDADD"] = dataRow["ItemID"];
			dataRow["ItemUnitID"] = Convert.ToInt32(items.UnitSmall);
			dataRow["PrinterID"] = printer.ID;
			dataRow["PrinterName"] = printer.Name;
			dataRow["PrinterIDItem"] = items.PrinterID;
			dataRow["PrinterNameItem"] = ORD.GetPrinterName(Convert.ToInt32(items.PrinterID));
			dataRow["كود_الصنف"] = items.Item_code;
			dataRow["أسم الصنف"] = items.item_Name;
			dataRow["الاسم الانجليزي"] = items.item_Name_English;
			dataRow["المجموعة"] = items.Category_ID;
			dataRow["الكمية"] = 1;
			dataRow["مسح"] = 0;
			dataRow["تم"] = 1;
			dataRow["أسم_الوحدة"] = "الصغرى";
			dataRow["متوسط_التكلفة"] = items.Average_cost;
			dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
			dataRow["المخزن"] = ORD.GetStoreName();
			dataRow["رقم المخزن"] = ORD.GetStoreID();
			dataRow["الطول"] = 1;
			dataRow["العرض"] = 1;
			dataRow["الإرتفاع"] = 1;
			dataRow["الوزن"] = 1;
			dataRow["قيمة الخصم"] = 0;
			dataRow["نسبةالخصم"] = 0;
			string unitNameById = ITM.GetUnitNameById(Convert.ToInt32(items.UnitSmall));
			dataRow["الوحدة"] = unitNameById;
			dataRow["VatCode"] = items.VatCode;
			dataRow["TaxExemptionReason"] = items.TaxExemptionReason;
			dataRow["TaxExemptionReasonCode"] = items.TaxExemptionReasonCode;
			if (ORD.IsVatActive())
			{
				if (items.Is_Tax == true)
				{
					if (LoginDetails.ItemTotalWithVat)
					{
						if (toggleSectoral.IsOn)
						{
							dataRow["سعر الوحدة"] = Convert.ToDecimal(items.SellPriceSmall) / (Convert.ToDecimal(items.Tax_Value) / 100m + 1m);
						}
						else
						{
							dataRow["سعر الوحدة"] = Convert.ToDecimal(items.SellPriceSmall2) / (Convert.ToDecimal(items.Tax_Value) / 100m + 1m);
						}
					}
					else if (toggleSectoral.IsOn)
					{
						dataRow["سعر الوحدة"] = Convert.ToDecimal(items.SellPriceSmall);
					}
					else
					{
						dataRow["سعر الوحدة"] = Convert.ToDecimal(items.SellPriceSmall2);
					}
					dataRow["TaxTobacco"] = Convert.ToDecimal(dataRow["سعر الوحدة"]) * Convert.ToDecimal(items.Tax_Tobacco) / 100m;
					decimal num = Convert.ToDecimal(dataRow["TaxTobacco"]);
					if (num < LoginDetails.MinTobaccoTax && num != 0m)
					{
						dataRow["TaxTobacco"] = LoginDetails.MinTobaccoTax;
					}
					dataRow["TaxTobaccoDis"] = dataRow["TaxTobacco"];
					dataRow["الاجمالى"] = Convert.ToDecimal(dataRow["سعر الوحدة"]) + Convert.ToDecimal(dataRow["TaxTobaccoDis"]);
					dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(dataRow["الاجمالى"]) * (Convert.ToDecimal(items.Tax_Value) / 100m + 1m), 2);
					dataRow["الضربية"] = Math.Round(Convert.ToDecimal(dataRow["إجمالى المبلغ"]) - Convert.ToDecimal(dataRow["الاجمالى"]), DecimalNum);
					dataRow["هل_ضريبة"] = 1;
					dataRow["قيمة_الضريبة"] = items.Tax_Value;
				}
				else
				{
					if (toggleSectoral.IsOn)
					{
						dataRow["سعر الوحدة"] = Convert.ToDecimal(items.SellPriceSmall);
					}
					else
					{
						dataRow["سعر الوحدة"] = Convert.ToDecimal(items.SellPriceSmall2);
					}
					dataRow["TaxTobacco"] = Convert.ToDecimal(dataRow["سعر الوحدة"]) * Convert.ToDecimal(items.Tax_Tobacco) / 100m;
					decimal num2 = Convert.ToDecimal(dataRow["TaxTobacco"]);
					if (num2 < LoginDetails.MinTobaccoTax && num2 != 0m)
					{
						dataRow["TaxTobacco"] = LoginDetails.MinTobaccoTax;
					}
					dataRow["TaxTobaccoDis"] = dataRow["TaxTobacco"];
					dataRow["الاجمالى"] = Convert.ToDecimal(dataRow["سعر الوحدة"]) + Convert.ToDecimal(dataRow["TaxTobaccoDis"]);
					dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(dataRow["الاجمالى"]), 2);
					dataRow["الضربية"] = 0;
					dataRow["هل_ضريبة"] = 0;
					dataRow["قيمة_الضريبة"] = 0;
				}
			}
			else
			{
				if (toggleSectoral.IsOn)
				{
					dataRow["سعر الوحدة"] = Convert.ToDecimal(items.SellPriceSmall);
				}
				else
				{
					dataRow["سعر الوحدة"] = Convert.ToDecimal(items.SellPriceSmall2);
				}
				dataRow["TaxTobacco"] = Convert.ToDecimal(dataRow["سعر الوحدة"]) * Convert.ToDecimal(items.Tax_Tobacco) / 100m;
				decimal num3 = Convert.ToDecimal(dataRow["TaxTobacco"]);
				if (num3 < LoginDetails.MinTobaccoTax && num3 != 0m)
				{
					dataRow["TaxTobacco"] = LoginDetails.MinTobaccoTax;
				}
				dataRow["TaxTobaccoDis"] = dataRow["TaxTobacco"];
				dataRow["الاجمالى"] = Convert.ToDecimal(dataRow["سعر الوحدة"]) + Convert.ToDecimal(dataRow["TaxTobaccoDis"]);
				dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(dataRow["الاجمالى"]), 2);
				dataRow["الضربية"] = 0;
				dataRow["هل_ضريبة"] = 0;
				dataRow["قيمة_الضريبة"] = 0;
			}
			dataRow["سعربعدالخصم"] = dataRow["سعر الوحدة"];
			dataRow["إجمالى_بعدالخصم"] = dataRow["الاجمالى"];
			dataRow["الضريبه_بعد_الخصم"] = dataRow["الضربية"];
			TblID++;
			datatable.Rows.Add(dataRow);
			ArrangeDatatable();
			gridControl1.DataSource = datatable;
			GetTotalCost();
			SoundPlayer soundPlayer = new SoundPlayer("MsgSent.WAV");
			soundPlayer.Play();
		}
		if (LoginDetails.DiscountForRestaurant > 0m)
		{
			DiscPerc = LoginDetails.DiscountForRestaurant;
			txtdicoutprege.Text = "0";
			txtdicoutprege.Text = DiscPerc.ToString();
			TxtDiscountPercent.Text = txtdicoutprege.Text;
		}
		Cal_FinalResultTotals();
		if (LoginDetails.DisPerCashier > 0m)
		{
			TxtDiscountPercent.Text = LoginDetails.DisPerCashier.ToString();
			TxtDiscountPercent_KeyUp(TxtDiscountPercent, new KeyEventArgs(Keys.None));
		}
	}

	public void InsertNewItemAllItemInRow(int itemid)
	{
		bool flag = false;
		Item_Items items = ORD.GetItems(itemid);
		Printer printer = ORD.GetPrinter(itemid);
		foreach (DataRow row in datatable.Rows)
		{
			string item_code = items.Item_code;
			string text = row["كود_الصنف"].ToString();
			if (item_code == text)
			{
				decimal value = Convert.ToDecimal(row["سعر الوحدة"].ToString());
				if (row["كود_الصنف"].ToString() == text)
				{
				}
				row["الكمية"] = Convert.ToDecimal(row["الكمية"]) + 1m;
				ItmID2 = row["كود_الصنف"].ToString();
				row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
				row["TaxTobacco"] = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(items.Tax_Tobacco) / 100m;
				decimal num = Convert.ToDecimal(row["TaxTobacco"]);
				if (num < LoginDetails.MinTobaccoTax && num != 0m)
				{
					row["TaxTobacco"] = LoginDetails.MinTobaccoTax;
				}
				row["TaxTobacco"] = Convert.ToDecimal(row["TaxTobacco"]) * Convert.ToDecimal(row["الكمية"]);
				row["TaxTobaccoDis"] = row["TaxTobacco"];
				decimal value2 = Convert.ToDecimal(value) * Convert.ToDecimal(row["الكمية"]);
				row["الاجمالى"] = Convert.ToDecimal(value2) + Convert.ToDecimal(row["TaxTobacco"]);
				if (ORD.IsVatActive())
				{
					if (items.Is_Tax == true)
					{
						row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]) * (Convert.ToDecimal(items.Tax_Value / (decimal?)100) + 1m), 2);
						row["الضربية"] = Math.Round(Convert.ToDecimal(row["إجمالى المبلغ"]) - Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
						row["هل_ضريبة"] = 1;
						row["قيمة_الضريبة"] = items.Tax_Value;
					}
					else
					{
						row["الضربية"] = 0;
						row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
						row["هل_ضريبة"] = 0;
						row["قيمة_الضريبة"] = 0;
					}
				}
				else
				{
					row["الضربية"] = 0;
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["الاجمالى"]), DecimalNum);
					row["هل_ضريبة"] = 0;
					row["قيمة_الضريبة"] = 0;
				}
				flag = true;
			}
			gridControl1.DataSource = datatable;
		}
		if (!flag)
		{
			InsertNewItem(itemid);
		}
		if (LoginDetails.DiscountForRestaurant > 0m)
		{
			DiscPerc = LoginDetails.DiscountForRestaurant;
			txtdicoutprege.Text = "0";
			txtdicoutprege.Text = DiscPerc.ToString();
			TxtDiscountPercent.Text = txtdicoutprege.Text;
		}
		Cal_FinalResultTotals();
		if (LoginDetails.DisPerCashier > 0m)
		{
			TxtDiscountPercent.Text = LoginDetails.DisPerCashier.ToString();
			TxtDiscountPercent_KeyUp(TxtDiscountPercent, new KeyEventArgs(Keys.None));
		}
	}

	private void Cal_FinalResultTotals()
	{
		try
		{
			if (LoginDetails.IsTobacc)
			{
				if (datatable.Rows.Count <= 0)
				{
					TxtVat.Text = "0.00";
					TxtNet.Text = "0.00";
					TxtNet22.Text = "0.00";
					TxtTotals.Text = "0.00";
					TxtDiscount.Text = "0.00";
					txtdicoutprege.Text = "0.00";
					TxtTotalBeforDisc.Text = "0.00";
					TxtVatBeforeDisc.Text = "0.00";
					TxtTotalAll.Text = "0.00";
					TxtCost.Text = "0.00";
					TxtTabocc.Text = "0.00";
					TxtAllVat.Text = "0.00";
					TxtAllTotal.Text = "0.00";
					return;
				}
				if (TxtDiscount.Text == "" || Convert.ToDecimal(TxtDiscount.Text) == 0m || Convert.ToDecimal(TxtDiscount.Text) > Convert.ToDecimal(TxtTotals.Text))
				{
					foreach (DataRow row in datatable.Rows)
					{
						if (row["أسم_الوحدة"].ToString() != "")
						{
							row["سعربعدالخصم"] = row["سعر الوحدة"];
							row["إجمالى_بعدالخصم"] = row["الاجمالى"];
							row["الضريبه_بعد_الخصم"] = row["الضربية"];
						}
					}
					TxtDiscount.Text = "0.00";
					txtdicoutprege.Text = "0.00";
					object obj = datatable.Compute("SUM(الاجمالى)", "");
					TxtTotals.Text = Convert.ToString(obj);
					object obj2 = datatable.Compute("SUM(الضربية)", "");
					object obj3 = datatable.Compute("SUM(TaxTobaccoDis)", "");
					TxtTabocc.Text = Convert.ToString(obj3);
					TxtVat.Text = Convert.ToString(obj2);
					object obj4 = datatable.Compute("SUM(إجمالى_بعدالخصم)", "");
					TxtTotalBeforDisc.Text = Convert.ToString(obj4);
					object obj5 = datatable.Compute("SUM(الضريبه_بعد_الخصم)", "");
					TxtVatBeforeDisc.Text = Convert.ToString(obj5);
					object obj6 = datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", "");
					TxtCost.Text = Convert.ToString(obj6);
					TxtAllTotal.Text = TxtTotalAll.Text;
					TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
					TxtTabocc.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
					TxtAllVat.Text = (Convert.ToDecimal(TxtVatBeforeDisc.Text) + Convert.ToDecimal(TxtTabocc.Text)).ToString();
					decimal num = Convert.ToDecimal(datatable.Compute("SUM([إجمالى المبلغ])", string.Empty));
					TxtNet.Text = num.ToString();
					TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text) + Convert.ToDecimal(TxtTabocc.Text), DecimalNum)).ToString();
					return;
				}
				decimal value = Convert.ToDecimal(TxtTotals.Text);
				decimal num2 = Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum) * 100m) / Math.Round(Convert.ToDecimal(value), DecimalNum);
				TxtDiscountPercent.Text = Math.Round(Convert.ToDecimal(num2), 2).ToString();
				foreach (DataRow row2 in datatable.Rows)
				{
					string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(focusedRowCellDisplayText).FirstOrDefault();
					if (row2["أسم_الوحدة"].ToString() != "")
					{
						row2["سعربعدالخصم"] = Convert.ToDecimal(row2["سعر الوحدة"].ToString()) - Convert.ToDecimal(num2 * Convert.ToDecimal(row2["سعر الوحدة"].ToString()) / 100m);
						row2["TaxTobaccoDis"] = Convert.ToDecimal(row2["TaxTobacco"].ToString()) - Convert.ToDecimal(num2 * Convert.ToDecimal(row2["TaxTobacco"].ToString()) / 100m);
						decimal num3 = Convert.ToDecimal(row2["TaxTobaccoDis"]);
						if (num3 < LoginDetails.MinTobaccoTax && num3 != 0m)
						{
							row2["TaxTobaccoDis"] = LoginDetails.MinTobaccoTax;
						}
						decimal value2 = Convert.ToDecimal(row2["سعربعدالخصم"]) * Convert.ToDecimal(row2["الكمية"]);
						row2["إجمالى_بعدالخصم"] = Convert.ToDecimal(value2) + Convert.ToDecimal(row2["TaxTobaccoDis"]);
						row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["إجمالى_بعدالخصم"]) * (Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100) + 1m), 2);
						row2["الضريبه_بعد_الخصم"] = Math.Round(Convert.ToDecimal(row2["إجمالى المبلغ"]) - Convert.ToDecimal(row2["إجمالى_بعدالخصم"]), DecimalNum);
					}
				}
				object obj7 = datatable.Compute("SUM(الاجمالى)", "");
				TxtTotals.Text = Convert.ToString(obj7);
				object obj8 = datatable.Compute("SUM(الضربية)", "");
				TxtVat.Text = Convert.ToString(obj8);
				object obj9 = datatable.Compute("SUM(إجمالى_بعدالخصم)", "");
				TxtTotalBeforDisc.Text = Convert.ToString(obj9);
				object obj10 = datatable.Compute("SUM(الضريبه_بعد_الخصم)", "");
				TxtVatBeforeDisc.Text = Convert.ToString(obj10);
				object obj11 = datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", "");
				TxtCost.Text = Convert.ToString(obj11);
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
				TxtAllVat.Text = TxtVatBeforeDisc.Text;
				TxtAllTotal.Text = TxtTotalAll.Text;
				TxtTabocc.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
				TxtAllVat.Text = (Convert.ToDecimal(TxtVatBeforeDisc.Text) + Convert.ToDecimal(TxtTabocc.Text)).ToString();
				decimal num4 = Convert.ToDecimal(datatable.Compute("SUM([إجمالى المبلغ])", string.Empty));
				TxtNet.Text = num4.ToString();
				TxtNet22.Text = TxtNet.Text;
				return;
			}
			if (datatable.Rows.Count <= 0)
			{
				TxtVat.Text = "0.00";
				TxtNet.Text = "0.00";
				TxtNet22.Text = "0.00";
				TxtTotals.Text = "0.00";
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtTotalBeforDisc.Text = "0.00";
				TxtVatBeforeDisc.Text = "0.00";
				TxtTotalAll.Text = "0.00";
				TxtCost.Text = "0.00";
				TxtTabocc.Text = "0.00";
				TxtAllVat.Text = "0.00";
				TxtAllTotal.Text = "0.00";
				return;
			}
			if (TxtDiscount.Text == "" || Convert.ToDecimal(TxtDiscount.Text) == 0m || Convert.ToDecimal(TxtDiscount.Text) > Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				TxtTabocc.Text = "0.00";
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				foreach (DataRow row3 in datatable.Rows)
				{
					if (row3["أسم_الوحدة"].ToString() != "")
					{
						row3["سعربعدالخصم"] = row3["سعر الوحدة"];
						row3["إجمالى_بعدالخصم"] = row3["الاجمالى"];
						row3["الضريبه_بعد_الخصم"] = row3["الضربية"];
					}
				}
				decimal num5 = Math.Round(Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", "")), 2);
				TxtTotals.Text = Convert.ToString(num5);
				decimal num6 = Math.Round(Convert.ToDecimal(datatable.Compute("SUM(الضربية)", "")), 2);
				TxtVat.Text = Convert.ToString(num6);
				object obj12 = datatable.Compute("SUM(إجمالى_بعدالخصم)", "");
				TxtTotalBeforDisc.Text = Convert.ToString(obj12);
				object obj13 = datatable.Compute("SUM(الضريبه_بعد_الخصم)", "");
				TxtVatBeforeDisc.Text = Convert.ToString(obj13);
				foreach (DataRow row4 in datatable.Rows)
				{
					if (row4["إجمالى المبلغ"] == DBNull.Value || !decimal.TryParse(row4["إجمالى المبلغ"].ToString(), out var _))
					{
						row4["إجمالى المبلغ"] = 0;
					}
				}
				object obj14 = datatable.Compute("SUM([إجمالى المبلغ])", "");
				TxtNet.Text = obj14.ToString();
				TxtNet22.Text = obj14.ToString();
				object obj15 = datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", "");
				TxtCost.Text = Convert.ToString(obj15);
				TxtAllVat.Text = TxtVatBeforeDisc.Text;
				TxtAllTotal.Text = TxtTotalAll.Text;
				TxtTabocc.Text = "0.00";
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
				return;
			}
			decimal value3 = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text));
			decimal num7;
			if (LoginDetails.DiscountForRestaurant > 0m)
			{
				num7 = Convert.ToDecimal(TxtDiscountPercent.Text);
				DiscPerc = Convert.ToDecimal(TxtDiscountPercent.Text);
				decimal num8 = Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text);
				Disc = num8 * DiscPerc / 100m;
				TxtDiscount.Text = Disc.ToString();
			}
			else
			{
				num7 = Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum) * 100m) / Math.Round(Convert.ToDecimal(value3), DecimalNum);
			}
			foreach (DataRow row5 in datatable.Rows)
			{
				if (row5["أسم_الوحدة"].ToString() != "")
				{
					row5["سعربعدالخصم"] = Convert.ToDecimal(row5["سعر الوحدة"].ToString()) - Convert.ToDecimal(num7 * Convert.ToDecimal(row5["سعر الوحدة"].ToString()) / 100m);
					row5["إجمالى_بعدالخصم"] = Convert.ToDecimal(row5["سعربعدالخصم"].ToString()) * Convert.ToDecimal(row5["الكمية"].ToString());
					row5["الضريبه_بعد_الخصم"] = Convert.ToDecimal(Convert.ToDecimal(row5["سعربعدالخصم"]) * Convert.ToDecimal(row5["الكمية"]) * Convert.ToDecimal(row5["قيمة_الضريبة"]) / 100m);
				}
			}
			object obj16 = datatable.Compute("SUM(الاجمالى)", "");
			TxtTotals.Text = Convert.ToString(obj16);
			object obj17 = datatable.Compute("SUM(الضربية)", "");
			TxtVat.Text = Convert.ToString(obj17);
			object obj18 = datatable.Compute("SUM(إجمالى_بعدالخصم)", "");
			TxtTotalBeforDisc.Text = Convert.ToString(obj18);
			object obj19 = datatable.Compute("SUM(الضريبه_بعد_الخصم)", "");
			TxtVatBeforeDisc.Text = Convert.ToString(obj19);
			object obj20 = datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", "");
			TxtCost.Text = Convert.ToString(obj20);
			TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text)) + Math.Round(Convert.ToDecimal(TxtVat.Text)) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
			TxtNet22.Text = TxtNet.Text;
			TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
			TxtAllVat.Text = TxtVatBeforeDisc.Text;
			TxtAllTotal.Text = TxtTotalAll.Text;
			TxtTabocc.Text = "0.00";
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
		}
	}

	public void DiscPersent()
	{
		DiscPerc = Convert.ToDecimal(txtdicoutprege.Text);
		decimal num = Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text);
		Disc = num * DiscPerc / 100m;
		TxtDiscount.Text = "0";
		TxtDiscount.Text = Disc.ToString();
	}

	public void ArrangeDatatable()
	{
		try
		{
			datatable.DefaultView.Sort = "ظ…";
			int num = 1;
			foreach (DataRow row in datatable.Rows)
			{
				if (Convert.ToInt32(row["RowId"]) > 0)
				{
					row["ظ…"] = row["RowId"];
				}
				else
				{
					row["ظ…"] = num;
				}
				num++;
			}
			datatable.DefaultView.Sort = "ظ…";
		}
		catch (Exception)
		{
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
					string code = item.ToString();
					Item_Items item_Items = ORD.GetItems(code).FirstOrDefault();
					if (item_Items != null)
					{
						DataRow dataRow = null;
						dataRow = datatable.NewRow();
						object obj = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
						int num = Convert.ToInt32(obj.ToString());
						dataRow["ظ…"] = num + 1;
						dataRow["RowId"] = 0;
						dataRow["ADDId"] = 0;
						dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
						dataRow["ItemIDADD"] = dataRow["ItemID"];
						dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
						dataRow["كود_الصنف"] = item_Items.Item_code;
						dataRow["أسم الصنف"] = item_Items.item_Name;
						dataRow["المجموعة"] = item_Items.Category_ID;
						dataRow["الوحدة"] = item_Items.Item_Unit.Name;
						dataRow["الكمية"] = 1;
						dataRow["متوسط_التكلفة"] = item_Items.Average_cost;
						dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
						dataRow["المخزن"] = ORD.GetStoreName();
						dataRow["رقم المخزن"] = ORD.GetStoreID();
						dataRow["أسم_الوحدة"] = "الصغرى";
						dataRow["سعر الوحدة"] = item_Items.SellPriceSmall;
						dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1)), DecimalNum);
						if (ORD.IsVatActive())
						{
							if (item_Items.Is_Tax == true)
							{
								dataRow["الضربية"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1) * item_Items.Tax_Value / (decimal?)100), DecimalNum);
								dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1) * item_Items.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
								dataRow["هل_ضريبة"] = 1;
								dataRow["قيمة_الضريبة"] = item_Items.Tax_Value;
							}
							else
							{
								dataRow["هل_ضريبة"] = 0;
								dataRow["قيمة_الضريبة"] = 0;
								dataRow["الضربية"] = 0.0;
								dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
						}
						else
						{
							dataRow["هل_ضريبة"] = 0;
							dataRow["قيمة_الضريبة"] = 0;
							dataRow["الضربية"] = 0.0;
							dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
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
		}
	}

	public void GetPurchesOrder()
	{
		try
		{
			FrmSearchPriceOffer frmSearchPriceOffer = new FrmSearchPriceOffer();
			frmSearchPriceOffer.ShowDialog();
			if (frmSearchPriceOffer.CustCode <= 0)
			{
				return;
			}
			int custCode = frmSearchPriceOffer.CustCode;
			Order_PriceOffer order_PriceOffer = PurORD.GetPurchases(custCode).FirstOrDefault();
			datatable.Rows.Clear();
			List<Order_PriceOfferDetails> itemsPurchesByID = PurORD.GetItemsPurchesByID(custCode);
			int num = 1;
			foreach (Order_PriceOfferDetails item in itemsPurchesByID)
			{
				DataRow dataRow = null;
				int itemID = Convert.ToInt32(item.ItemID);
				Item_Items item_Items = PurORD.GetItemsByitemid(itemID).FirstOrDefault();
				dataRow = datatable.NewRow();
				dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
				dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
				dataRow["كود_الصنف"] = item_Items.Item_code;
				dataRow["أسم الصنف"] = item_Items.item_Name;
				dataRow["المجموعة"] = item_Items.Category_ID;
				if (item.ItemUnitType == "الصغرى")
				{
					dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
				}
				if (item.ItemUnitType == "المتوسطة")
				{
					dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
				}
				if (item.ItemUnitType == "الكبرى")
				{
					dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
				}
				dataRow["أسم_الوحدة"] = item.ItemUnitType;
				dataRow["رقم المخزن"] = item.StoreID;
				dataRow["المخزن"] = (from x in db.Account_Stores
					where (int?)x.ID == item.StoreID
					select x.Store_Name).FirstOrDefault();
				dataRow["الكمية"] = item.Quantity;
				dataRow["متوسط_التكلفة"] = item_Items.Average_cost;
				dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
				dataRow["سعر الوحدة"] = item.UnitPrice;
				dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
				dataRow["مسح"] = 0;
				dataRow["الطول"] = 1;
				dataRow["العرض"] = 1;
				dataRow["الإرتفاع"] = 1;
				dataRow["الوزن"] = 1;
				dataRow["قيمة الخصم"] = 0;
				dataRow["نسبةالخصم"] = 0;
				decimal? vAT = item.VAT;
				if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
				{
					dataRow["الضربية"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
					dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT), DecimalNum);
					dataRow["هل_ضريبة"] = 1;
					dataRow["قيمة_الضريبة"] = item_Items.Tax_Value;
				}
				else
				{
					dataRow["هل_ضريبة"] = 0;
					dataRow["قيمة_الضريبة"] = 0;
					dataRow["الضربية"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
					dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT), DecimalNum);
				}
				datatable.Rows.Add(dataRow);
				num++;
			}
			gridControl1.DataSource = datatable;
			Code = custCode;
			TxtCode.Text = "";
			TxtDate.DateTime = Convert.ToDateTime(order_PriceOffer.Purchases_Date);
			TxtTotals.Text = Convert.ToString(order_PriceOffer.TotalPrices);
			TxtVat.Text = Convert.ToString(order_PriceOffer.Tax);
			TxtTotalAll.Text = order_PriceOffer.Safy.ToString();
			TxtDiscount.Text = order_PriceOffer.DiscountNum.ToString();
			TxtVatBeforeDisc.Text = order_PriceOffer.Tax_Discount.ToString();
			TxtNet.Text = order_PriceOffer.Net.ToString();
			TxtNet22.Text = order_PriceOffer.Net.ToString();
			BtnSave.Enabled = true;
			BtnDelete.Enabled = false;
			gridView1.OptionsBehavior.Editable = true;
			ArrangeDatatable();
			IsEnableTrue();
			insertnewrow();
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void gridControl1_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void SetDateCasheir()
	{
		TxtDate.DateTime = DateTime.Now;
	}

	private void IsRestaurantActive()
	{
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

	private void tileBarItem3_ItemClick(object sender, TileItemEventArgs e)
	{
		try
		{
			GTSdbEntities gTSdbEntities = new GTSdbEntities();
			Restaurant_Room restaurant_Room = gTSdbEntities.Restaurant_Room.Where((Restaurant_Room x) => x.ID == e.Item.Id).FirstOrDefault();
			if (restaurant_Room.OrderID > 0)
			{
				int num = Convert.ToInt32(restaurant_Room.OrderID);
				Order_Orders order_Orders = ORD.GetPurchases(num).FirstOrDefault();
				datatable.Rows.Clear();
				List<Order_OrdersDetails> itemsPurchesByID = ORD.GetItemsPurchesByID(num);
				int num2 = 1;
				foreach (Order_OrdersDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = ORD.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
					dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
					dataRow["كود_الصنف"] = item_Items.Item_code;
					dataRow["أسم الصنف"] = item_Items.item_Name;
					dataRow["المجموعة"] = item_Items.Category_ID;
					if (item.ItemUnitType == "الصغرى")
					{
						dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "المتوسطة")
					{
						dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "الكبرى")
					{
						dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					dataRow["أسم_الوحدة"] = item.ItemUnitType;
					dataRow["رقم المخزن"] = item.StoreID;
					dataRow["المخزن"] = (from x in db.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["الكمية"] = item.Quantity;
					dataRow["سعر الوحدة"] = item.UnitPrice;
					dataRow["متوسط_التكلفة"] = item.LastCost;
					dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
					dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
					dataRow["مسح"] = 0;
					dataRow["الطول"] = 1;
					dataRow["العرض"] = 1;
					dataRow["الإرتفاع"] = 1;
					dataRow["الوزن"] = 1;
					dataRow["قيمة الخصم"] = 0;
					dataRow["نسبةالخصم"] = 0;
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["الضربية"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["هل_ضريبة"] = 1;
						dataRow["قيمة_الضريبة"] = item_Items.Tax_Value;
					}
					else
					{
						dataRow["هل_ضريبة"] = 0;
						dataRow["قيمة_الضريبة"] = 0;
						dataRow["الضربية"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT), DecimalNum);
					}
					datatable.Rows.Add(dataRow);
					num2++;
				}
				gridControl1.DataSource = datatable;
				Code = num;
				TxtCode.Text = order_Orders.PurBranchID.ToString();
				TxtDate.DateTime = Convert.ToDateTime(order_Orders.Purchases_Date);
				TxtTotals.Text = Convert.ToString(order_Orders.TotalPrices);
				TxtVat.Text = Convert.ToString(order_Orders.Tax);
				TxtTotalAll.Text = order_Orders.Safy.ToString();
				TxtDiscount.Text = order_Orders.DiscountNum.ToString();
				TxtTotalBeforDisc.Text = order_Orders.TotalPrices_Discount.ToString();
				TxtVatBeforeDisc.Text = order_Orders.Tax_Discount.ToString();
				TxtNet.Text = order_Orders.Net.ToString();
				TxtNet22.Text = order_Orders.Safy.ToString();
				TxtCost.Text = order_Orders.CostOrder.ToString();
				Cash = Convert.ToDecimal(order_Orders.CashMoney);
				Bank = Convert.ToDecimal(order_Orders.CashBank);
				TxtTabocc.Text = order_Orders.TobaccoTax.ToString();
				TxtAllVat.Text = order_Orders.AllTax.ToString();
				BtnSave.Enabled = false;
				BtnSaveMahly.Enabled = false;
				BtnSaveTawsel.Enabled = false;
				BtnDelete.Enabled = true;
				gridView1.OptionsBehavior.Editable = true;
				ArrangeDatatable();
				IsEnableFalse();
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

	private decimal SafeConvertToDecimal(object value)
	{
		return (value == DBNull.Value || value == null) ? 0m : Convert.ToDecimal(value);
	}

	public void FilterDataTableSave(DataTable ItemOrder, int Purchese_ID, bool IsWait)
	{
		int num = 1;
		dtCurrentTable.Clear();
		foreach (DataRow row in ItemOrder.Rows)
		{
			if (row.RowState == DataRowState.Deleted || row.RowState == DataRowState.Detached)
				continue;

			if (row["كود_الصنف"].ToString() != "")
			{
				dtCurrentTable.Rows.Add(num, Purchese_ID, ToInt(row["ظ…"]), ToInt(row["RowId"]), ToInt(row["ADDId"]), ToInt(row["ItemID"]), ToInt(row["ItemIDADD"]), LoginDetails.BranchID, IsWait, ToInt(row["رقم المخزن"]), ToStr(row["ItemUnitID"]), ToDecimal(row["الكمية"]), Math.Round(ToDecimal(row["متوسط_التكلفة"]), DecimalNum), ToDecimal(row["سعر الوحدة"]), Math.Round(ToDecimal(row["سعر الوحدة"]), DecimalNum), Math.Round(ToDecimal(row["الاجمالى"]), DecimalNum), Math.Round(ToDecimal(row["الضربية"]), DecimalNum), Math.Round(ToDecimal(row["سعربعدالخصم"]), DecimalNum), Math.Round(ToDecimal(row["إجمالى المبلغ"]), DecimalNum), Math.Round(ToDecimal(row["الضريبه_بعد_الخصم"]), DecimalNum), ToStr(row["أسم_الوحدة"]), 0, "", 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, Math.Round(ToDecimal(row["TaxTobacco"]), DecimalNum), Math.Round(ToDecimal(row["TaxTobaccoDis"]), DecimalNum));
				num++;
			}
		}
	}

	private int ToInt(object value)
	{
		return (value != DBNull.Value) ? Convert.ToInt32(value) : 0;
	}

	private decimal ToDecimal(object value)
	{
		return (value == DBNull.Value) ? 0m : Convert.ToDecimal(value);
	}

	private string ToStr(object value)
	{
		return (value == DBNull.Value) ? string.Empty : value.ToString();
	}

	private void ZeroDisc()
	{
		foreach (DataRow row in datatable.Rows)
		{
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(focusedRowCellDisplayText).FirstOrDefault();
			if (row["أسم_الوحدة"].ToString() != "")
			{
				decimal value = Convert.ToDecimal(row["سعر الوحدة"].ToString());
				row["TaxTobaccoDis"] = row["TaxTobacco"];
				decimal value2 = Convert.ToDecimal(value) * Convert.ToDecimal(row["الكمية"]);
				row["الاجمالى"] = Convert.ToDecimal(value2) + Convert.ToDecimal(row["TaxTobacco"]);
				row["إجمالى_بعدالخصم"] = row["الاجمالى"];
				row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"]) * (Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100) + 1m), 2);
				row["الضريبه_بعد_الخصم"] = Math.Round(Convert.ToDecimal(row["إجمالى المبلغ"]) - Convert.ToDecimal(row["إجمالى_بعدالخصم"]), DecimalNum);
			}
		}
		TxtDiscountPercent.Text = "";
		TxtDiscount.Text = "";
		object obj = datatable.Compute("SUM(الاجمالى)", "");
		TxtTotals.Text = Convert.ToString(obj);
		object obj2 = datatable.Compute("SUM(الضربية)", "");
		TxtVat.Text = Convert.ToString(obj2);
		object obj3 = datatable.Compute("SUM(إجمالى_بعدالخصم)", "");
		TxtTotalBeforDisc.Text = Convert.ToString(obj3);
		object obj4 = datatable.Compute("SUM(الضريبه_بعد_الخصم)", "");
		TxtVatBeforeDisc.Text = Convert.ToString(obj4);
		object obj5 = datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", "");
		TxtCost.Text = Convert.ToString(obj5);
		TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
		TxtAllVat.Text = TxtVatBeforeDisc.Text;
		TxtAllTotal.Text = TxtTotalAll.Text;
		TxtAllVat.Text = (Convert.ToDecimal(TxtVatBeforeDisc.Text) + Convert.ToDecimal(TxtTabocc.Text)).ToString();
		decimal num = Convert.ToDecimal(datatable.Compute("SUM([إجمالى المبلغ])", string.Empty));
		TxtNet.Text = num.ToString();
		TxtNet22.Text = TxtNet.Text;
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
		if (e.KeyCode.ToString() == "F3")
		{
			if (gridView1.OptionsBehavior.Editable)
			{
				LoginDetails.ItemSearch = CodeItm;
				SearchItems();
			}
		}
		else if (e.KeyCode.ToString() == "F4")
		{
			datatable.Rows.Clear();
			Clear();
		}
		else
		{
			if (e.KeyCode.ToString() == "F5")
			{
				return;
			}
			if (e.KeyCode == Keys.F6)
			{
				BtnEdit_Click(this, EventArgs.Empty);
				return;
			}
			if (e.KeyCode.ToString() == "F8")
			{
				FrmOpenDay frmOpenDay = new FrmOpenDay();
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
			if (e.KeyCode.ToString() == "F11")
			{
				try
				{
					int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
					foreach (DataRow row in datatable.Rows)
					{
						if (row["ظ…"].ToString() == Convert.ToString(num))
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

	private void TxtNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
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
	}

	private void TxtDiscount_TextChanged(object sender, EventArgs e)
	{
	}

	private void TxtDiscount_KeyUp(object sender, KeyEventArgs e)
	{
		if (TxtDiscount.Text != string.Empty && Convert.ToDecimal(TxtDiscount.Text) != 0m && Convert.ToDecimal(TxtDiscount.Text) < Convert.ToDecimal(TxtTotals.Text))
		{
			decimal num = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), 2);
			decimal num2 = Convert.ToDecimal(TxtDiscount.Text);
			decimal num3 = Math.Round(num2 / num * 100m, 2);
			TxtDiscountPercent.Text = num3.ToString();
			Cal_FinalResultTotals();
		}
		else
		{
			ZeroDisc();
		}
	}

	private void TxtDiscountPercent_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			decimal num = 0m;
			decimal num2 = 0m;
			if (TxtDiscountPercent.Text != string.Empty && Convert.ToDecimal(TxtDiscountPercent.Text) != 0m)
			{
				num2 = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), 2);
				num = Convert.ToDecimal(TxtDiscountPercent.Text);
				decimal num3 = num2 * num / 100m;
				TxtDiscount.Text = num3.ToString("F2");
				Cal_FinalResultTotals();
			}
			else
			{
				ZeroDisc();
			}
		}
		catch (Exception)
		{
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

	private void TxtDiscountPercent_TextChanged(object sender, EventArgs e)
	{
	}

	private void TxtTotals_TextChanged(object sender, EventArgs e)
	{
		try
		{
			Cal_FinalResultTotals();
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
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
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
		// Keep the menu command consistent with the existing New toolbar action.
		BtnNew_Click(sender, e);
	}

	private async void MenuSave_Click(object sender, EventArgs e)
	{
		await SaveOrEditAsync();
	}

	private void MenuEdit_Click(object sender, EventArgs e)
	{
		BtnEdit_Click(sender, e);
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

	private void TxtNet22_TextChanged(object sender, EventArgs e)
	{
	}

	private void BtnDiscount_Click(object sender, EventArgs e)
	{
		using (new GTSdbEntities())
		{
			FrmPassWordSupervisor frmPassWordSupervisor = new FrmPassWordSupervisor();
			frmPassWordSupervisor.ShowDialog();
			if (frmPassWordSupervisor.ISClose)
			{
				TxtDiscount.ReadOnly = false;
				TxtDiscountPercent.ReadOnly = false;
			}
		}
	}

	private void gridControl1_Click(object sender, EventArgs e)
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
				Clear();
				if (frmCloseDay.IsClose)
				{
					return true;
				}
				return false;
			}
			FrmOpenDay frmOpenDay = new FrmOpenDay();
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

	private void TxtPhone_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void BtnEditRoom_Click(object sender, EventArgs e)
	{
		FrmEditRoom frmEditRoom = new FrmEditRoom();
		frmEditRoom.ShowDialog();
	}

	private void FrmCashirRestaurant_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (isEditingExistingTakeAway && e.CloseReason == CloseReason.UserClosing)
		{
			DialogResult discardChanges = MessageBox.Show(
				"توجد تعديلات غير محفوظة. هل تريد إغلاق الشاشة وإلغاء هذه التعديلات؟",
				"تعديلات غير محفوظة", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign);
			if (discardChanges != DialogResult.Yes)
			{
				e.Cancel = true;
				return;
			}
			isEditingExistingTakeAway = false;
		}
		if (e.CloseReason != CloseReason.UserClosing)
		{
			return;
		}
		DialogResult dialogResult = MessageBox.Show("هل انت متأكد من اغلاق شاشة المطعم", "اغلاق الكاشير", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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

	private void TxtAllTotal_TextChanged(object sender, EventArgs e)
	{
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
	}

	private void MenuPreviewFatoura_Click_1(object sender, EventArgs e)
	{
		PreviewPrint(Code);
	}

	private void MenuPrintFatoura_Click_1(object sender, EventArgs e)
	{
		Print(Code);
	}

	private void MenuNewItem_Click(object sender, EventArgs e)
	{
		openItmCard();
	}

	private void MenuSearchItem_Click(object sender, EventArgs e)
	{
		SearchItems();
	}

	private void MenuPriceItem_Click(object sender, EventArgs e)
	{
		try
		{
			int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
			foreach (DataRow row in datatable.Rows)
			{
				if (row["ظ…"].ToString() == Convert.ToString(num))
				{
					decimal currentQuantity = Convert.ToDecimal(row["الكمية"]);
					decimal unitPrice = Convert.ToDecimal(row["سعر الوحدة"]);
					int num2 = Convert.ToInt32(row["ItemID"]);
					if (num2 > 0)
					{
						LoginDetails.ItemcodeForStore = num2;
						FrmSearchTransferAllBranch frmSearchTransferAllBranch = new FrmSearchTransferAllBranch(num2, StorId, currentQuantity, unitPrice, UnitName, "")
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
		}
		catch (Exception)
		{
		}
	}

	private void toggleCashierType_Toggled(object sender, EventArgs e)
	{
	}

	private void MenuPurchOrder_Click(object sender, EventArgs e)
	{
		GetPurchesOrder();
	}

	private void BtnTypeInvoic_Click(object sender, EventArgs e)
	{
		new FrmInvoicType("Order").Show();
	}

	private void BtnUpload_Click_1(object sender, EventArgs e)
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

	private void MenuExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TxtType_KeyPress_1(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
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
			try
			{
				decimal num = row.Field<decimal>("الضريبه_بعد_الخصم");
				decimal num2 = row.Field<decimal>("سعر الوحدة");
				decimal num3 = row.Field<decimal>("الكمية");
				decimal num4 = Math.Round((num2 * num3 == 0m) ? 0m : (num / (num2 * num3) * 100m), 0);
				if (num4 != 15m && num4 != 5m)
				{
					num4 = 15m;
				}
				list.Add(new InvoiceItems
				{
					ProductName = row["أسم الصنف"]?.ToString(),
					DiscountValue = row.Field<decimal>("قيمة الخصم"),
					ProductPrice = num2,
					ProductQuantity = num3,
					TotalPrice = row.Field<decimal>("الاجمالى"),
					TotalWithVat = num + row.Field<decimal>("الاجمالى"),
					TotalPriceAfterDiscount = row.Field<decimal>("إجمالى المبلغ"),
					VatCode = row["VatCode"]?.ToString(),
					TaxExemptionReason = row["TaxExemptionReason"]?.ToString(),
					TaxExemptionReasonCode = row["TaxExemptionReasonCode"]?.ToString(),
					VatValue = num,
					VatPercentage = num4
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine("خطأ في الصف: " + row["أسم الصنف"]);
				Console.WriteLine("التفاصيل: " + ex.Message);
			}
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
		return new OrderOrdersDto
		{
			ID = ID,
			PurBranchID = ID,
			SupplierID = Convert.ToInt32(LBlSuppAccount.Text),
			SupplierPhone = TxtPhone.Text,
			Purchases_Date = Convert.ToDateTime(TxtDate.DateTime),
			Restaurant_PayType = 0,
			Restaurant_TypeID = 1,
			CostCentersID = 1,
			CostOrder = Math.Round(Convert.ToDecimal(TxtCost.Text), 2),
			Tax = Math.Round(Convert.ToDecimal(TxtVat.Text), 2),
			TotalPrices = Math.Round(Convert.ToDecimal(TxtTotals.Text), 2),
			Safy = Math.Round(Convert.ToDecimal(TxtTotals.Text), 2),
			DiscountNum = Math.Round(Convert.ToDecimal(TxtDiscount.Text), 2),
			DiscountPerantage = Math.Round(Convert.ToDecimal(txtdicoutprege.Text), 2),
			Tax_Discount = Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), 2),
			TotalPrices_Discount = Math.Round(Convert.ToDecimal(TxtAllTotal.Text), 2),
			TobaccoTax = 0m,
			AllTax = Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), 2),
			Net = Math.Round(Convert.ToDecimal(TxtNet.Text), 2),
			CashMoney = Cash,
			CashBank = Bank,
			RoomNum = 1,
			TableNum = 1,
			Sectoral = true,
			DateHold = DateTime.Now,
			BuildingNum = string.Empty,
			Country = "السعوديه",
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
			LoginDetails.EncHash = rowById.QRCode;
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

	public int InsertNewItemMizan(string BarcodeMizan)
	{
		if (TxtBarcode.Text.Length != 13 || !TxtBarcode.Text.All(char.IsDigit))
		{
			MessageBox.Show("الباركود خطأ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			TxtBarcode.Text = "";
			return 0;
		}
		int result = 0;
		try
		{
			bool flag = false;
			string text = BarcodeMizan.Substring(0, 7);
			string value = BarcodeMizan.Substring(BarcodeMizan.Length - 6, 6);
			decimal num = 0m;
			Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(text.ToString()).FirstOrDefault();
			Printer printer = ORD.GetPrinter(Convert.ToInt32(get_Item_ByCodeALL_Result.ItemId));
			if (get_Item_ByCodeALL_Result != null && get_Item_ByCodeALL_Result.Is_Item_mizan == true && !flag)
			{
				DataRow dataRow = null;
				dataRow = datatable.NewRow();
				dataRow["ظ…"] = TblID;
				dataRow["RowId"] = 0;
				dataRow["ADDId"] = 0;
				dataRow["ItemID"] = Convert.ToInt32(get_Item_ByCodeALL_Result.ItemId);
				dataRow["ItemIDADD"] = dataRow["ItemID"];
				dataRow["ItemUnitID"] = Convert.ToInt32(get_Item_ByCodeALL_Result.UnitSmallID);
				dataRow["PrinterID"] = printer.ID;
				dataRow["PrinterName"] = printer.Name;
				dataRow["كود_الصنف"] = get_Item_ByCodeALL_Result.Item_code;
				dataRow["أسم الصنف"] = get_Item_ByCodeALL_Result.item_Name;
				dataRow["الاسم الانجليزي"] = get_Item_ByCodeALL_Result.item_Name_English;
				dataRow["المجموعة"] = get_Item_ByCodeALL_Result.Category_ID;
				dataRow["مسح"] = 0;
				dataRow["تم"] = 1;
				dataRow["أسم_الوحدة"] = "الصغرى";
				dataRow["متوسط_التكلفة"] = get_Item_ByCodeALL_Result.Average_cost;
				dataRow["المخزن"] = ORD.GetStoreName();
				dataRow["رقم المخزن"] = ORD.GetStoreID();
				dataRow["الطول"] = 1;
				dataRow["العرض"] = 1;
				dataRow["الإرتفاع"] = 1;
				dataRow["الوزن"] = 1;
				dataRow["قيمة الخصم"] = 0;
				dataRow["نسبةالخصم"] = 0;
				string value2 = get_Item_ByCodeALL_Result.UnitSmall.ToString();
				dataRow["الوحدة"] = value2;
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
					dataRow["الكمية"] = FUNC_DecimalNumQty(num5);
					dataRow["سعر الوحدة"] = num4;
					dataRow["الاجمالى"] = FUNC_DecimalNum(num4 * num5);
					dataRow["الضربية"] = FUNC_DecimalNum(num * num5);
					decimal num7 = FUNC_DecimalNum(Convert.ToDecimal(dataRow["الاجمالى"]) + Convert.ToDecimal(dataRow["الضربية"]));
					dataRow["إجمالى المبلغ"] = num7;
				}
				else if (LoginDetails.ItemTotalWithVat)
				{
					decimal num8 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
					if (DecimalNumQty == 3 && LoginDetails.MizanRate == "2:4")
					{
						decimal num5 = GetMizanQuantity(Convert.ToDecimal(value), LoginDetails.MizanRate);
						num5 = Math.Floor(num5 * 1000m) / 1000m;
						dataRow["الكمية"] = FUNC_DecimalNumQty(Convert.ToDecimal(num5));
						decimal num6 = Math.Round(num5 * Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), 3);
						dataRow["سعر الوحدة"] = Math.Round(num6 / num8, 3);
						decimal num9 = Convert.ToDecimal(dataRow["سعر الوحدة"]);
						num = Convert.ToDecimal(num6) - Convert.ToDecimal(dataRow["سعر الوحدة"]);
						dataRow["الضربية"] = num;
						dataRow["الاجمالى"] = dataRow["سعر الوحدة"];
						decimal num10 = Math.Round(num6, 2, MidpointRounding.AwayFromZero);
						dataRow["إجمالى المبلغ"] = num10;
					}
					else
					{
						decimal num5 = FUNC_DecimalNumQty(GetMizanQuantity(Convert.ToDecimal(value), LoginDetails.MizanRate));
						dataRow["الكمية"] = FUNC_DecimalNumQty(Convert.ToDecimal(num5) / Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
						decimal num6 = Math.Round(num5 / num8, 3);
						dataRow["سعر الوحدة"] = FUNC_DecimalNum(num6 / Convert.ToDecimal(dataRow["الكمية"]));
						num = FUNC_DecimalNum(Convert.ToDecimal(num5) - Convert.ToDecimal(num6));
						dataRow["الضربية"] = num;
						dataRow["الاجمالى"] = FUNC_DecimalNum(num6);
						decimal num11 = FUNC_DecimalNum(Convert.ToDecimal(dataRow["الاجمالى"]) + Convert.ToDecimal(dataRow["الضربية"]));
						dataRow["إجمالى المبلغ"] = num11;
					}
				}
				else
				{
					decimal num12 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
					decimal num6 = FUNC_DecimalNumQty(GetMizanQuantity(Convert.ToDecimal(value), LoginDetails.MizanRate));
					dataRow["الكمية"] = FUNC_DecimalNumQty(Convert.ToDecimal(num6) / Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
					dataRow["سعر الوحدة"] = FUNC_DecimalNum(num6 / Convert.ToDecimal(dataRow["الكمية"]));
					dataRow["الاجمالى"] = FUNC_DecimalNum(num6);
					dataRow["الضربية"] = FUNC_DecimalNum(num6 * (Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m));
					dataRow["إجمالى المبلغ"] = num6;
				}
				dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
				TblID++;
				datatable.Rows.Add(dataRow);
				ArrangeDatatable();
				gridControl1.DataSource = datatable;
				GetTotalCost();
				SoundPlayer soundPlayer = new SoundPlayer("MsgSent.WAV");
				soundPlayer.Play();
			}
			if (LoginDetails.DiscountForRestaurant > 0m)
			{
				DiscPerc = LoginDetails.DiscountForRestaurant;
				txtdicoutprege.Text = "0";
				txtdicoutprege.Text = DiscPerc.ToString();
				TxtDiscountPercent.Text = txtdicoutprege.Text;
			}
			Cal_FinalResultTotals();
			if (LoginDetails.DisPerCashier > 0m)
			{
				TxtDiscountPercent.Text = LoginDetails.DisPerCashier.ToString();
				TxtDiscountPercent_KeyUp(TxtDiscountPercent, new KeyEventArgs(Keys.None));
			}
		}
		catch (Exception)
		{
			result = 0;
		}
		return result;
	}

	private void TxtBarcode_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.Return)
			{
				string text = TxtBarcode.Text;
				if (text != string.Empty)
				{
					int num = InsertNewItemMizan(text);
				}
				TxtBarcode.Text = string.Empty;
				TxtBarcode.Focus();
			}
		}
		catch (Exception)
		{
			TxtBarcode.Text = null;
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmRestaurant.FrmCashirRestaurant));
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
		DevExpress.XtraGrid.GridLevelNode gridLevelNode = new DevExpress.XtraGrid.GridLevelNode();
		DevExpress.Utils.SerializableAppearanceObject appearance4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled4 = new DevExpress.Utils.SerializableAppearanceObject();
		this.repositoryItemButtonEdit4Units = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.TxtNet22 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtAllTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtAllVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTabocc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotalBeforDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.TxtTotalAll = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatBeforeDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label1 = new System.Windows.Forms.Label();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix1 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintQued = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPreviewFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuNewItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix2 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MenupPriceItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPurchOrder = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix3 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix4 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix5 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix6 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix7 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix8 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix9 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix10 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.RecoveredMenuItemFix11 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.tileControl1 = new DevExpress.XtraEditors.TileControl();
		this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
		this.tileControl2 = new DevExpress.XtraEditors.TileControl();
		this.tileGroup3 = new DevExpress.XtraEditors.TileGroup();
		this.TxtCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label9 = new System.Windows.Forms.Label();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnReturnCashier = new DevExpress.XtraEditors.SimpleButton();
		this.label14 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnOpenCashir = new DevExpress.XtraEditors.SimpleButton();
		this.BtnTypeInvoic = new DevExpress.XtraEditors.SimpleButton();
		this.LValid = new System.Windows.Forms.Label();
		this.LSucced = new System.Windows.Forms.Label();
		this.BtnUpload = new DevExpress.XtraEditors.SimpleButton();
		this.toggleSectoral = new DevExpress.XtraEditors.ToggleSwitch();
		this.HoldOpenClose = new DevExpress.XtraEditors.ToggleSwitch();
		this.label43 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
		this.TabSafry = new DevExpress.XtraTab.XtraTabPage();
		this.BtnPrintSafry = new DevExpress.XtraEditors.SimpleButton();
		this.TabMahly = new DevExpress.XtraTab.XtraTabPage();
		this.BtnPrintMahly = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCollecting = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEditRoom = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNewMahly = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDeleteMahly = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSaveMahly = new DevExpress.XtraEditors.SimpleButton();
		this.TabTawsel = new DevExpress.XtraTab.XtraTabPage();
		this.BtnPrintTawsel = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCollectingTawsel = new DevExpress.XtraEditors.SimpleButton();
		this.BtnReturnCashierTawsel = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNewTawsel = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDeleteTawsel = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSaveTawsel = new DevExpress.XtraEditors.SimpleButton();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.ItemID = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.ADDId = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonADD = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.label4 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtDiscountPercent = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnDiscount = new DevExpress.XtraEditors.SimpleButton();
		this.label27 = new System.Windows.Forms.Label();
		this.TxtDiscountALLTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtBarcode = new DevExpress.XtraEditors.ButtonEdit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4Units).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.toggleSectoral.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.HoldOpenClose.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl1).BeginInit();
		this.xtraTabControl1.SuspendLayout();
		this.TabSafry.SuspendLayout();
		this.TabMahly.SuspendLayout();
		this.TabTawsel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonADD).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.repositoryItemButtonEdit4Units, "repositoryItemButtonEdit4Units");
		this.repositoryItemButtonEdit4Units.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4Units.Buttons"))
		});
		this.repositoryItemButtonEdit4Units.Name = "repositoryItemButtonEdit4Units";
		this.repositoryItemButtonEdit4Units.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit4Units_ButtonClick);
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		editorButtonImageOptions.Image = GTSErpSystem.Properties.Resources.rsz_1minus;
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"), resources.GetString("repositoryItemButtonEdit3.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit3.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEdit3.Buttons6"), resources.GetObject("repositoryItemButtonEdit3.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit3.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit3.Buttons9"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions2.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions5");
		editorButtonImageOptions2.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonPressed);
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions6");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonClick);
		resources.ApplyResources(this.TxtNet22, "TxtNet22");
		this.TxtNet22.DecimalDigits = 3;
		this.TxtNet22.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet22.Name = "TxtNet22";
		this.TxtNet22.ReadOnly = true;
		this.TxtNet22.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet22.UseCompatibleTextRendering = false;
		this.TxtNet22.Value = new decimal(new int[4]);
		this.TxtNet22.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet22.TextChanged += new System.EventHandler(TxtNet22_TextChanged);
		resources.ApplyResources(this.TxtAllTotal, "TxtAllTotal");
		this.TxtAllTotal.DecimalDigits = 3;
		this.TxtAllTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtAllTotal.Name = "TxtAllTotal";
		this.TxtAllTotal.ReadOnly = true;
		this.TxtAllTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllTotal.Value = new decimal(new int[4]);
		this.TxtAllTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtAllTotal.TextChanged += new System.EventHandler(TxtAllTotal_TextChanged);
		resources.ApplyResources(this.TxtAllVat, "TxtAllVat");
		this.TxtAllVat.DecimalDigits = 3;
		this.TxtAllVat.ForeColor = System.Drawing.Color.Black;
		this.TxtAllVat.Name = "TxtAllVat";
		this.TxtAllVat.ReadOnly = true;
		this.TxtAllVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllVat.Value = new decimal(new int[4]);
		this.TxtAllVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.TxtNet, "TxtNet");
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.ForeColor = System.Drawing.Color.Teal;
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet.TextChanged += new System.EventHandler(TxtNet_TextChanged);
		resources.ApplyResources(this.TxtTotalBeforDisc, "TxtTotalBeforDisc");
		this.TxtTotalBeforDisc.DecimalDigits = 3;
		this.TxtTotalBeforDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalBeforDisc.Name = "TxtTotalBeforDisc";
		this.TxtTotalBeforDisc.ReadOnly = true;
		this.TxtTotalBeforDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalBeforDisc.Value = new decimal(new int[4]);
		this.TxtTotalBeforDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalBeforDisc.TextChanged += new System.EventHandler(TxtTotalBeforDisc_TextChanged);
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.TxtDiscount, "TxtDiscount");
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.ForeColor = System.Drawing.Color.Red;
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.ReadOnly = true;
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscount.TextChanged += new System.EventHandler(TxtDiscount_TextChanged);
		this.TxtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtSuppliers_KeyDown);
		this.TxtDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtDiscount_KeyUp);
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
		this.TxtDate.Properties.EditFormat.FormatString = "yyyy/MM/dd HH:mm:ss tt";
		this.TxtDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		resources.ApplyResources(this.LBlSuppAccount, "LBlSuppAccount");
		this.LBlSuppAccount.Name = "LBlSuppAccount";
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
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
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
		this.TxtVat.ForeColor = System.Drawing.Color.Black;
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
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.txtdicoutprege, "txtdicoutprege");
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Black;
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.ReadOnly = true;
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[18]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.RecoveredMenuItemFix1, this.toolStripSeparator1, this.MenuNewItem, this.RecoveredMenuItemFix2, this.MenuSearchItem, this.MenupPriceItem,
			this.toolStripSeparator4, this.MenuPurchOrder, this.RecoveredMenuItemFix3, this.RecoveredMenuItemFix7, this.toolStripSeparator2, this.RecoveredMenuItemFix11, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.MenuNew.Name = "MenuNew";
		resources.ApplyResources(this.MenuNew, "MenuNew");
		this.MenuNew.Click += new System.EventHandler(MenuNew_Click);
		this.MenuSave.Name = "MenuSave";
		resources.ApplyResources(this.MenuSave, "MenuSave");
		this.MenuSave.Click += new System.EventHandler(BtnSave_Click);
		this.MenuEdit.Name = "MenuEdit";
		resources.ApplyResources(this.MenuEdit, "MenuEdit");
		this.MenuEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.MenuDelete.Name = "MenuDelete";
		resources.ApplyResources(this.MenuDelete, "MenuDelete");
		this.MenuDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.RecoveredMenuItemFix1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.MenuPrintQued, this.MenuPreviewFatoura, this.MenuPrintFatoura });
		this.RecoveredMenuItemFix1.Name = "طباعةToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix1, "طباعةToolStripMenuItem1");
		this.MenuPrintQued.Name = "MenuPrintQued";
		resources.ApplyResources(this.MenuPrintQued, "MenuPrintQued");
		this.MenuPrintQued.Click += new System.EventHandler(MenuPrintQued_Click);
		this.MenuPreviewFatoura.Name = "MenuPreviewFatoura";
		resources.ApplyResources(this.MenuPreviewFatoura, "MenuPreviewFatoura");
		this.MenuPreviewFatoura.Click += new System.EventHandler(MenuPreviewFatoura_Click_1);
		this.MenuPrintFatoura.Name = "MenuPrintFatoura";
		resources.ApplyResources(this.MenuPrintFatoura, "MenuPrintFatoura");
		this.MenuPrintFatoura.Click += new System.EventHandler(MenuPrintFatoura_Click_1);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.MenuNewItem.Name = "MenuNewItem";
		resources.ApplyResources(this.MenuNewItem, "MenuNewItem");
		this.MenuNewItem.Click += new System.EventHandler(MenuNewItem_Click);
		this.RecoveredMenuItemFix2.Name = "حركةصنفToolStripMenuItem";
		resources.ApplyResources(this.RecoveredMenuItemFix2, "حركةصنفToolStripMenuItem");
		this.MenuSearchItem.Name = "MenuSearchItem";
		resources.ApplyResources(this.MenuSearchItem, "MenuSearchItem");
		this.MenuSearchItem.Click += new System.EventHandler(MenuSearchItem_Click);
		this.MenupPriceItem.Name = "MenupPriceItem";
		resources.ApplyResources(this.MenupPriceItem, "MenupPriceItem");
		this.MenupPriceItem.Click += new System.EventHandler(MenuPriceItem_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		resources.ApplyResources(this.MenuPurchOrder, "MenuPurchOrder");
		this.MenuPurchOrder.Click += new System.EventHandler(MenuPurchOrder_Click);
		this.RecoveredMenuItemFix3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix4, this.RecoveredMenuItemFix5, this.RecoveredMenuItemFix6 });
		this.RecoveredMenuItemFix3.Name = "استعلامToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix3, "استعلامToolStripMenuItem1");
		this.RecoveredMenuItemFix4.Name = "رقمالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix4, "رقمالفاتورةToolStripMenuItem1");
		this.RecoveredMenuItemFix5.Name = "اسمالموردToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix5, "اسمالموردToolStripMenuItem1");
		this.RecoveredMenuItemFix6.Name = "نوعالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix6, "نوعالفاتورةToolStripMenuItem1");
		this.RecoveredMenuItemFix7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix8, this.RecoveredMenuItemFix9, this.RecoveredMenuItemFix10 });
		this.RecoveredMenuItemFix7.Name = "الكمياتToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix7, "الكمياتToolStripMenuItem1");
		this.RecoveredMenuItemFix8.Name = "الكميةالحاليةToolStripMenuItem";
		resources.ApplyResources(this.RecoveredMenuItemFix8, "الكميةالحاليةToolStripMenuItem");
		this.RecoveredMenuItemFix9.Name = "كلالكمياتبالمستودعاتToolStripMenuItem";
		resources.ApplyResources(this.RecoveredMenuItemFix9, "كلالكمياتبالمستودعاتToolStripMenuItem");
		this.RecoveredMenuItemFix10.Name = "كلالكمياتبالفروعToolStripMenuItem";
		resources.ApplyResources(this.RecoveredMenuItemFix10, "كلالكمياتبالفروعToolStripMenuItem");
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		this.RecoveredMenuItemFix11.Name = "تخصيصToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix11, "تخصيصToolStripMenuItem1");
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
		this.MenuExit.Name = "MenuExit";
		resources.ApplyResources(this.MenuExit, "MenuExit");
		this.MenuExit.Click += new System.EventHandler(MenuExit_Click);
		this.tileControl1.AllowDrag = false;
		this.tileControl1.AllowDragTilesBetweenGroups = false;
		resources.ApplyResources(this.tileControl1, "tileControl1");
		this.tileControl1.Groups.Add(this.tileGroup2);
		this.tileControl1.Name = "tileControl1";
		this.tileControl1.Orientation = System.Windows.Forms.Orientation.Vertical;
		this.tileControl1.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.tileGroup2.Name = "tileGroup2";
		this.tileControl2.AllowDrag = false;
		this.tileControl2.AllowDragTilesBetweenGroups = false;
		resources.ApplyResources(this.tileControl2, "tileControl2");
		this.tileControl2.Cursor = System.Windows.Forms.Cursors.Default;
		this.tileControl2.Groups.Add(this.tileGroup3);
		this.tileControl2.ItemSize = 90;
		this.tileControl2.MaxId = 26;
		this.tileControl2.Name = "tileControl2";
		this.tileControl2.Orientation = System.Windows.Forms.Orientation.Vertical;
		this.tileControl2.RowCount = 9999999;
		this.tileControl2.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.tileGroup3.Name = "tileGroup3";
		resources.ApplyResources(this.TxtCost, "TxtCost");
		this.TxtCost.DecimalDigits = 3;
		this.TxtCost.ForeColor = System.Drawing.Color.Black;
		this.TxtCost.Name = "TxtCost";
		this.TxtCost.ReadOnly = true;
		this.TxtCost.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCost.Value = new decimal(new int[4]);
		this.TxtCost.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDelete.Appearance.Font");
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNew.Appearance.Font");
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.BtnReturnCashier, "BtnReturnCashier");
		this.BtnReturnCashier.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnReturnCashier.Appearance.Font");
		this.BtnReturnCashier.Appearance.Options.UseFont = true;
		this.BtnReturnCashier.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReturnCashier.ImageOptions.Image");
		this.BtnReturnCashier.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnReturnCashier.Name = "BtnReturnCashier";
		this.BtnReturnCashier.Click += new System.EventHandler(BtnReturnCashier_Click);
		resources.ApplyResources(this.label14, "label14");
		this.label14.Name = "label14";
		this.groupBox2.Controls.Add(this.BtnOpenCashir);
		this.groupBox2.Controls.Add(this.BtnTypeInvoic);
		this.groupBox2.Controls.Add(this.LValid);
		this.groupBox2.Controls.Add(this.LSucced);
		this.groupBox2.Controls.Add(this.BtnUpload);
		this.groupBox2.Controls.Add(this.toggleSectoral);
		this.groupBox2.Controls.Add(this.HoldOpenClose);
		this.groupBox2.Controls.Add(this.label43);
		this.groupBox2.Controls.Add(this.TxtPhone);
		this.groupBox2.Controls.Add(this.TxtCode);
		this.groupBox2.Controls.Add(this.LBlSuppAccount);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.TxtDate);
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		this.BtnOpenCashir.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnOpenCashir.Appearance.Font");
		this.BtnOpenCashir.Appearance.Options.UseFont = true;
		this.BtnOpenCashir.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOpenCashir.ImageOptions.Image");
		this.BtnOpenCashir.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnOpenCashir, "BtnOpenCashir");
		this.BtnOpenCashir.Name = "BtnOpenCashir";
		this.BtnOpenCashir.Click += new System.EventHandler(BtnOpenCashir_Click);
		this.BtnTypeInvoic.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnTypeInvoic.Appearance.Font");
		this.BtnTypeInvoic.Appearance.Options.UseFont = true;
		this.BtnTypeInvoic.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnTypeInvoic.ImageOptions.Image");
		this.BtnTypeInvoic.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnTypeInvoic, "BtnTypeInvoic");
		this.BtnTypeInvoic.Name = "BtnTypeInvoic";
		this.BtnTypeInvoic.Click += new System.EventHandler(BtnTypeInvoic_Click);
		resources.ApplyResources(this.LValid, "LValid");
		this.LValid.BackColor = System.Drawing.Color.Green;
		this.LValid.ForeColor = System.Drawing.Color.White;
		this.LValid.Name = "LValid";
		resources.ApplyResources(this.LSucced, "LSucced");
		this.LSucced.BackColor = System.Drawing.Color.Green;
		this.LSucced.ForeColor = System.Drawing.Color.White;
		this.LSucced.Name = "LSucced";
		this.BtnUpload.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnUpload.Appearance.Font");
		this.BtnUpload.Appearance.Options.UseFont = true;
		this.BtnUpload.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUpload.ImageOptions.Image");
		this.BtnUpload.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnUpload, "BtnUpload");
		this.BtnUpload.Name = "BtnUpload";
		this.BtnUpload.Click += new System.EventHandler(BtnUpload_Click_1);
		resources.ApplyResources(this.toggleSectoral, "toggleSectoral");
		this.toggleSectoral.Name = "toggleSectoral";
		this.toggleSectoral.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleSectoral.Properties.Appearance.Font");
		this.toggleSectoral.Properties.Appearance.Options.UseFont = true;
		this.toggleSectoral.Properties.Appearance.Options.UseForeColor = true;
		this.toggleSectoral.Properties.OffText = resources.GetString("toggleSectoral.Properties.OffText");
		this.toggleSectoral.Properties.OnText = resources.GetString("toggleSectoral.Properties.OnText");
		resources.ApplyResources(this.HoldOpenClose, "HoldOpenClose");
		this.HoldOpenClose.Name = "HoldOpenClose";
		this.HoldOpenClose.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("HoldOpenClose.Properties.Appearance.Font");
		this.HoldOpenClose.Properties.Appearance.Options.UseFont = true;
		this.HoldOpenClose.Properties.Appearance.Options.UseForeColor = true;
		this.HoldOpenClose.Properties.OffText = resources.GetString("HoldOpenClose.Properties.OffText");
		this.HoldOpenClose.Properties.OnText = resources.GetString("HoldOpenClose.Properties.OnText");
		this.HoldOpenClose.Click += new System.EventHandler(HoldOpenClose_Click);
		resources.ApplyResources(this.label43, "label43");
		this.label43.Name = "label43";
		resources.ApplyResources(this.TxtPhone, "TxtPhone");
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPhone.Properties.Appearance.Font");
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPhone_KeyPress);
		resources.ApplyResources(this.xtraTabControl1, "xtraTabControl1");
		this.xtraTabControl1.Name = "xtraTabControl1";
		this.xtraTabControl1.SelectedTabPage = this.TabSafry;
		this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[3] { this.TabSafry, this.TabMahly, this.TabTawsel });
		this.TabSafry.Appearance.Header.Font = (System.Drawing.Font)resources.GetObject("TabSafry.Appearance.Header.Font");
		this.TabSafry.Appearance.Header.Options.UseFont = true;
		this.TabSafry.Appearance.Header.Options.UseTextOptions = true;
		this.TabSafry.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TabSafry.Controls.Add(this.BtnPrintSafry);
		this.TabSafry.Controls.Add(this.BtnExit);
		this.TabSafry.Controls.Add(this.BtnDelete);
		this.TabSafry.Controls.Add(this.BtnNew);
		this.TabSafry.Controls.Add(this.BtnSave);
		this.TabSafry.Controls.Add(this.BtnReturnCashier);
		resources.ApplyResources(this.TabSafry, "TabSafry");
		this.TabSafry.Name = "TabSafry";
		resources.ApplyResources(this.BtnPrintSafry, "BtnPrintSafry");
		this.BtnPrintSafry.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrintSafry.Appearance.Font");
		this.BtnPrintSafry.Appearance.Options.UseFont = true;
		this.BtnPrintSafry.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrintSafry.ImageOptions.Image");
		this.BtnPrintSafry.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrintSafry.Name = "BtnPrintSafry";
		this.BtnPrintSafry.Click += new System.EventHandler(BtnPrint_Click);
		this.TabMahly.Appearance.Header.Font = (System.Drawing.Font)resources.GetObject("TabMahly.Appearance.Header.Font");
		this.TabMahly.Appearance.Header.Options.UseFont = true;
		this.TabMahly.Appearance.Header.Options.UseTextOptions = true;
		this.TabMahly.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TabMahly.Controls.Add(this.BtnPrintMahly);
		this.TabMahly.Controls.Add(this.BtnCollecting);
		this.TabMahly.Controls.Add(this.BtnEditRoom);
		this.TabMahly.Controls.Add(this.BtnNewMahly);
		this.TabMahly.Controls.Add(this.BtnDeleteMahly);
		this.TabMahly.Controls.Add(this.BtnSaveMahly);
		resources.ApplyResources(this.TabMahly, "TabMahly");
		this.TabMahly.Name = "TabMahly";
		resources.ApplyResources(this.BtnPrintMahly, "BtnPrintMahly");
		this.BtnPrintMahly.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrintMahly.Appearance.Font");
		this.BtnPrintMahly.Appearance.Options.UseFont = true;
		this.BtnPrintMahly.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrintMahly.ImageOptions.Image");
		this.BtnPrintMahly.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrintMahly.Name = "BtnPrintMahly";
		this.BtnPrintMahly.Click += new System.EventHandler(BtnPrint_Click);
		resources.ApplyResources(this.BtnCollecting, "BtnCollecting");
		this.BtnCollecting.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCollecting.Appearance.Font");
		this.BtnCollecting.Appearance.Options.UseFont = true;
		this.BtnCollecting.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCollecting.ImageOptions.Image");
		this.BtnCollecting.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCollecting.Name = "BtnCollecting";
		this.BtnCollecting.Click += new System.EventHandler(BtnCollecting_Click);
		resources.ApplyResources(this.BtnEditRoom, "BtnEditRoom");
		this.BtnEditRoom.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnEditRoom.Appearance.Font");
		this.BtnEditRoom.Appearance.Options.UseFont = true;
		this.BtnEditRoom.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEditRoom.ImageOptions.Image");
		this.BtnEditRoom.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEditRoom.Name = "BtnEditRoom";
		this.BtnEditRoom.Click += new System.EventHandler(BtnEditRoom_Click);
		resources.ApplyResources(this.BtnNewMahly, "BtnNewMahly");
		this.BtnNewMahly.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNewMahly.Appearance.Font");
		this.BtnNewMahly.Appearance.Options.UseFont = true;
		this.BtnNewMahly.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewMahly.ImageOptions.Image");
		this.BtnNewMahly.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewMahly.Name = "BtnNewMahly";
		this.BtnNewMahly.Click += new System.EventHandler(BtnNew_Click);
		resources.ApplyResources(this.BtnDeleteMahly, "BtnDeleteMahly");
		this.BtnDeleteMahly.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDeleteMahly.Appearance.Font");
		this.BtnDeleteMahly.Appearance.Options.UseFont = true;
		this.BtnDeleteMahly.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDeleteMahly.ImageOptions.Image");
		this.BtnDeleteMahly.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDeleteMahly.Name = "BtnDeleteMahly";
		this.BtnDeleteMahly.Click += new System.EventHandler(BtnDelete_Click);
		resources.ApplyResources(this.BtnSaveMahly, "BtnSaveMahly");
		this.BtnSaveMahly.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSaveMahly.Appearance.Font");
		this.BtnSaveMahly.Appearance.Options.UseFont = true;
		this.BtnSaveMahly.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSaveMahly.ImageOptions.Image");
		this.BtnSaveMahly.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSaveMahly.Name = "BtnSaveMahly";
		this.BtnSaveMahly.Click += new System.EventHandler(BtnSaveMahly_Click);
		this.TabTawsel.AlwaysScrollActiveControlIntoView = false;
		this.TabTawsel.Appearance.Header.Font = (System.Drawing.Font)resources.GetObject("TabTawsel.Appearance.Header.Font");
		this.TabTawsel.Appearance.Header.Options.UseFont = true;
		this.TabTawsel.Appearance.Header.Options.UseTextOptions = true;
		this.TabTawsel.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TabTawsel.Controls.Add(this.BtnPrintTawsel);
		this.TabTawsel.Controls.Add(this.BtnCollectingTawsel);
		this.TabTawsel.Controls.Add(this.BtnReturnCashierTawsel);
		this.TabTawsel.Controls.Add(this.BtnNewTawsel);
		this.TabTawsel.Controls.Add(this.BtnDeleteTawsel);
		this.TabTawsel.Controls.Add(this.BtnSaveTawsel);
		resources.ApplyResources(this.TabTawsel, "TabTawsel");
		this.TabTawsel.Name = "TabTawsel";
		resources.ApplyResources(this.BtnPrintTawsel, "BtnPrintTawsel");
		this.BtnPrintTawsel.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrintTawsel.Appearance.Font");
		this.BtnPrintTawsel.Appearance.Options.UseFont = true;
		this.BtnPrintTawsel.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrintTawsel.ImageOptions.Image");
		this.BtnPrintTawsel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrintTawsel.Name = "BtnPrintTawsel";
		this.BtnPrintTawsel.Click += new System.EventHandler(BtnPrint_Click);
		resources.ApplyResources(this.BtnCollectingTawsel, "BtnCollectingTawsel");
		this.BtnCollectingTawsel.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCollectingTawsel.Appearance.Font");
		this.BtnCollectingTawsel.Appearance.Options.UseFont = true;
		this.BtnCollectingTawsel.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCollectingTawsel.ImageOptions.Image");
		this.BtnCollectingTawsel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCollectingTawsel.Name = "BtnCollectingTawsel";
		this.BtnCollectingTawsel.Click += new System.EventHandler(BtnCollectingTawsel_Click);
		resources.ApplyResources(this.BtnReturnCashierTawsel, "BtnReturnCashierTawsel");
		this.BtnReturnCashierTawsel.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnReturnCashierTawsel.Appearance.Font");
		this.BtnReturnCashierTawsel.Appearance.Options.UseFont = true;
		this.BtnReturnCashierTawsel.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReturnCashierTawsel.ImageOptions.Image");
		this.BtnReturnCashierTawsel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnReturnCashierTawsel.Name = "BtnReturnCashierTawsel";
		this.BtnReturnCashierTawsel.Click += new System.EventHandler(BtnReturnCashier_Click);
		resources.ApplyResources(this.BtnNewTawsel, "BtnNewTawsel");
		this.BtnNewTawsel.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNewTawsel.Appearance.Font");
		this.BtnNewTawsel.Appearance.Options.UseFont = true;
		this.BtnNewTawsel.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewTawsel.ImageOptions.Image");
		this.BtnNewTawsel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewTawsel.Name = "BtnNewTawsel";
		this.BtnNewTawsel.Click += new System.EventHandler(BtnNew_Click);
		resources.ApplyResources(this.BtnDeleteTawsel, "BtnDeleteTawsel");
		this.BtnDeleteTawsel.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDeleteTawsel.Appearance.Font");
		this.BtnDeleteTawsel.Appearance.Options.UseFont = true;
		this.BtnDeleteTawsel.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDeleteTawsel.ImageOptions.Image");
		this.BtnDeleteTawsel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDeleteTawsel.Name = "BtnDeleteTawsel";
		this.BtnDeleteTawsel.Click += new System.EventHandler(BtnDelete_Click);
		resources.ApplyResources(this.BtnSaveTawsel, "BtnSaveTawsel");
		this.BtnSaveTawsel.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSaveTawsel.Appearance.Font");
		this.BtnSaveTawsel.Appearance.Options.UseFont = true;
		this.BtnSaveTawsel.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSaveTawsel.ImageOptions.Image");
		this.BtnSaveTawsel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSaveTawsel.Name = "BtnSaveTawsel";
		this.BtnSaveTawsel.Click += new System.EventHandler(BtnSaveTawsel_Click);
		resources.ApplyResources(this.gridControl1, "gridControl1");
		this.gridControl1.EmbeddedNavigator.Margin = (System.Windows.Forms.Padding)resources.GetObject("gridControl1.EmbeddedNavigator.Margin");
		gridLevelNode.RelationName = "Level1";
		this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[1] { gridLevelNode });
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridControl1.Click += new System.EventHandler(gridControl1_Click);
		this.gridView1.Appearance.FilterPanel.BackColor = System.Drawing.Color.Gray;
		this.gridView1.Appearance.FilterPanel.Options.UseBackColor = true;
		this.gridView1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView1.Appearance.HeaderPanel.Font");
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[33]
		{
			this.ItemID, this.gridColumn24, this.gridColumn23, this.gridColumn1, this.gridColumn2, this.gridColumn26, this.shearch, this.gridColumn19, this.gridColumn4, this.gridColumn25,
			this.gridColumn20, this.gridColumn21, this.gridColumn3, this.gridColumn6, this.ADDId, this.gridColumn34, this.gridColumn22, this.gridColumn5, this.gridColumn7, this.gridColumn8,
			this.gridColumn9, this.gridColumn27, this.gridColumn28, this.gridColumn29, this.gridColumn30, this.gridColumn31, this.gridColumn32, this.gridColumn33, this.gridColumn35, this.gridColumn36,
			this.gridColumn37, this.gridColumn38, this.gridColumn39
		});
		this.gridView1.DetailHeight = 437;
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.Name = "gridView1";
		this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
		this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
		this.gridView1.OptionsPrint.EnableAppearanceOddRow = true;
		this.gridView1.OptionsPrint.PrintFilterInfo = true;
		this.gridView1.OptionsPrint.PrintPreview = true;
		this.gridView1.OptionsView.RowAutoHeight = true;
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridView1.OptionsView.ShowGroupPanel = false;
		resources.ApplyResources(this.ItemID, "ItemID");
		this.ItemID.FieldName = "ItemID";
		this.ItemID.MinWidth = 25;
		this.ItemID.Name = "ItemID";
		this.gridColumn24.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceHeader.Font");
		this.gridColumn24.AppearanceHeader.Options.UseFont = true;
		this.gridColumn24.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn24, "gridColumn24");
		this.gridColumn24.FieldName = "الضريبه_بعد_الخصم";
		this.gridColumn24.MinWidth = 24;
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn23.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceHeader.Font");
		this.gridColumn23.AppearanceHeader.Options.UseFont = true;
		this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn23, "gridColumn23");
		this.gridColumn23.FieldName = "إجمالى_بعدالخصم";
		this.gridColumn23.MinWidth = 24;
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn1.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceCell.Font");
		this.gridColumn1.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn1.AppearanceCell.FontStyleDelta");
		this.gridColumn1.AppearanceCell.Options.UseFont = true;
		this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceHeader.Font");
		this.gridColumn1.AppearanceHeader.Options.UseFont = true;
		this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn1, "gridColumn1");
		this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.FieldName = "كود_الصنف";
		this.gridColumn1.MinWidth = 24;
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.OptionsColumn.AllowEdit = false;
		this.gridColumn2.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn2.AppearanceCell.Font");
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn2.AppearanceHeader.Font");
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn2, "gridColumn2");
		this.gridColumn2.FieldName = "أسم الصنف";
		this.gridColumn2.MinWidth = 190;
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.gridColumn26.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceCell.Font");
		this.gridColumn26.AppearanceCell.Options.UseFont = true;
		this.gridColumn26.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceHeader.Font");
		this.gridColumn26.AppearanceHeader.Options.UseFont = true;
		this.gridColumn26.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn26, "gridColumn26");
		this.gridColumn26.FieldName = "المجموعة";
		this.gridColumn26.MinWidth = 24;
		this.gridColumn26.Name = "gridColumn26";
		this.shearch.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("shearch.AppearanceCell.Font");
		this.shearch.AppearanceCell.Options.UseFont = true;
		this.shearch.AppearanceCell.Options.UseTextOptions = true;
		this.shearch.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("shearch.AppearanceHeader.Font");
		this.shearch.AppearanceHeader.Options.UseFont = true;
		this.shearch.AppearanceHeader.Options.UseTextOptions = true;
		this.shearch.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.shearch, "shearch");
		this.shearch.ColumnEdit = this.repositoryItemButtonEdit4Units;
		this.shearch.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.FieldName = "الوحدة";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.MinWidth = 24;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.FieldName = "أسم_الوحدة";
		this.gridColumn19.MinWidth = 24;
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		this.gridColumn4.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceCell.Font");
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceHeader.Font");
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn4, "gridColumn4");
		this.gridColumn4.ColumnEdit = this.repositoryItemButtonEdit3;
		this.gridColumn4.FieldName = "المخزن";
		this.gridColumn4.MinWidth = 24;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn25.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceCell.Font");
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceHeader.Font");
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn25, "gridColumn25");
		this.gridColumn25.FieldName = "متوسط_التكلفة";
		this.gridColumn25.MinWidth = 24;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn20.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn20.AppearanceCell.Font");
		this.gridColumn20.AppearanceCell.Options.UseFont = true;
		this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn20.AppearanceHeader.Font");
		this.gridColumn20.AppearanceHeader.Options.UseFont = true;
		this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn20, "gridColumn20");
		this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.FieldName = "الكمية";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.MinWidth = 50;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn21.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn21.AppearanceCell.Font");
		this.gridColumn21.AppearanceCell.Options.UseFont = true;
		this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn21.AppearanceHeader.Font");
		this.gridColumn21.AppearanceHeader.Options.UseFont = true;
		this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		resources.ApplyResources(this.gridColumn21, "gridColumn21");
		this.gridColumn21.FieldName = "سعر الوحدة";
		this.gridColumn21.MinWidth = 80;
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn3.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
		this.gridColumn3.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceCell.Font");
		this.gridColumn3.AppearanceCell.Options.UseBackColor = true;
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceHeader.Font");
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn3, "gridColumn3");
		this.gridColumn3.FieldName = "الاجمالى";
		this.gridColumn3.MinWidth = 24;
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
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
		resources.ApplyResources(this.gridColumn6, "gridColumn6");
		this.gridColumn6.FieldName = "الضربية";
		this.gridColumn6.MinWidth = 24;
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.ReadOnly = true;
		this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.ADDId, "ADDId");
		this.ADDId.FieldName = "ADDId";
		this.ADDId.MinWidth = 25;
		this.ADDId.Name = "ADDId";
		this.gridColumn34.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn34.AppearanceCell.Font");
		this.gridColumn34.AppearanceCell.Options.UseFont = true;
		this.gridColumn34.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn34.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn34.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn34.AppearanceHeader.Font");
		this.gridColumn34.AppearanceHeader.Options.UseFont = true;
		this.gridColumn34.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn34.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn34, "gridColumn34");
		this.gridColumn34.ColumnEdit = this.repositoryItemButtonADD;
		this.gridColumn34.MinWidth = 42;
		this.gridColumn34.Name = "gridColumn34";
		resources.ApplyResources(this.repositoryItemButtonADD, "repositoryItemButtonADD");
		this.repositoryItemButtonADD.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonADD.Buttons"), resources.GetString("repositoryItemButtonADD.Buttons1"), (int)resources.GetObject("repositoryItemButtonADD.Buttons2"), (bool)resources.GetObject("repositoryItemButtonADD.Buttons3"), (bool)resources.GetObject("repositoryItemButtonADD.Buttons4"), (bool)resources.GetObject("repositoryItemButtonADD.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonADD.Buttons6"), resources.GetObject("repositoryItemButtonADD.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonADD.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonADD.Buttons9"))
		});
		this.repositoryItemButtonADD.Name = "repositoryItemButtonADD";
		this.repositoryItemButtonADD.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonADD.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonADD_ButtonClick);
		this.gridColumn22.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceCell.Font");
		this.gridColumn22.AppearanceCell.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceHeader.Font");
		this.gridColumn22.AppearanceHeader.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn22, "gridColumn22");
		this.gridColumn22.ColumnEdit = this.repositoryItemButtonEdit1;
		this.gridColumn22.FieldName = "زيادة";
		this.gridColumn22.MinWidth = 38;
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn5.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceCell.Font");
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceHeader.Font");
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn5, "gridColumn5");
		this.gridColumn5.ColumnEdit = this.repositoryItemButtonEdit3;
		this.gridColumn5.FieldName = "نقصان";
		this.gridColumn5.MinWidth = 38;
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn7.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceCell.Font");
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceHeader.Font");
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn7, "gridColumn7");
		this.gridColumn7.FieldName = "إجمالى المبلغ";
		this.gridColumn7.MinWidth = 70;
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.OptionsColumn.ReadOnly = true;
		this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn8.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn8.AppearanceCell.Font");
		this.gridColumn8.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn8.AppearanceCell.FontStyleDelta");
		this.gridColumn8.AppearanceCell.Options.UseFont = true;
		this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn8.AppearanceHeader.Font");
		this.gridColumn8.AppearanceHeader.Options.UseFont = true;
		this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn8, "gridColumn8");
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit2;
		this.gridColumn8.FieldName = "حذف";
		this.gridColumn8.MinWidth = 38;
		this.gridColumn8.Name = "gridColumn8";
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
		resources.ApplyResources(this.gridColumn9, "gridColumn9");
		this.gridColumn9.FieldName = "ظ…";
		this.gridColumn9.MinWidth = 24;
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		resources.ApplyResources(this.gridColumn27, "gridColumn27");
		this.gridColumn27.FieldName = "اجمالي_متوسط_التكلفة";
		this.gridColumn27.MinWidth = 24;
		this.gridColumn27.Name = "gridColumn27";
		this.gridColumn28.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn28.AppearanceHeader.Font");
		this.gridColumn28.AppearanceHeader.Options.UseFont = true;
		resources.ApplyResources(this.gridColumn28, "gridColumn28");
		this.gridColumn28.FieldName = "الطول";
		this.gridColumn28.MinWidth = 23;
		this.gridColumn28.Name = "gridColumn28";
		this.gridColumn29.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceHeader.Font");
		this.gridColumn29.AppearanceHeader.Options.UseFont = true;
		resources.ApplyResources(this.gridColumn29, "gridColumn29");
		this.gridColumn29.FieldName = "العرض";
		this.gridColumn29.MinWidth = 23;
		this.gridColumn29.Name = "gridColumn29";
		this.gridColumn30.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn30.AppearanceHeader.Font");
		this.gridColumn30.AppearanceHeader.Options.UseFont = true;
		resources.ApplyResources(this.gridColumn30, "gridColumn30");
		this.gridColumn30.FieldName = "الإرتفاع";
		this.gridColumn30.MinWidth = 23;
		this.gridColumn30.Name = "gridColumn30";
		this.gridColumn31.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn31.AppearanceHeader.Font");
		this.gridColumn31.AppearanceHeader.Options.UseFont = true;
		resources.ApplyResources(this.gridColumn31, "gridColumn31");
		this.gridColumn31.FieldName = "الوزن";
		this.gridColumn31.MinWidth = 23;
		this.gridColumn31.Name = "gridColumn31";
		this.gridColumn32.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn32.AppearanceHeader.Font");
		this.gridColumn32.AppearanceHeader.Options.UseFont = true;
		resources.ApplyResources(this.gridColumn32, "gridColumn32");
		this.gridColumn32.FieldName = "قيمة الخصم";
		this.gridColumn32.MinWidth = 23;
		this.gridColumn32.Name = "gridColumn32";
		this.gridColumn33.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn33.AppearanceHeader.Font");
		this.gridColumn33.AppearanceHeader.Options.UseFont = true;
		resources.ApplyResources(this.gridColumn33, "gridColumn33");
		this.gridColumn33.FieldName = "نسبة الخصم";
		this.gridColumn33.MinWidth = 23;
		this.gridColumn33.Name = "gridColumn33";
		this.gridColumn35.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn35.AppearanceCell.Font");
		this.gridColumn35.AppearanceCell.Options.UseFont = true;
		this.gridColumn35.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn35.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn35.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn35.AppearanceHeader.Font");
		this.gridColumn35.AppearanceHeader.Options.UseFont = true;
		this.gridColumn35.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn35.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn35, "gridColumn35");
		this.gridColumn35.FieldName = "TaxTobacco";
		this.gridColumn35.MinWidth = 25;
		this.gridColumn35.Name = "gridColumn35";
		this.gridColumn36.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceCell.Font");
		this.gridColumn36.AppearanceCell.Options.UseFont = true;
		this.gridColumn36.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn36.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn36.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceHeader.Font");
		this.gridColumn36.AppearanceHeader.Options.UseFont = true;
		this.gridColumn36.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn36.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn36, "gridColumn36");
		this.gridColumn36.FieldName = "TaxTobaccoDis";
		this.gridColumn36.MinWidth = 25;
		this.gridColumn36.Name = "gridColumn36";
		this.gridColumn37.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn37.AppearanceCell.Font");
		this.gridColumn37.AppearanceCell.Options.UseFont = true;
		this.gridColumn37.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn37.AppearanceHeader.Font");
		this.gridColumn37.AppearanceHeader.Options.UseFont = true;
		resources.ApplyResources(this.gridColumn37, "gridColumn37");
		this.gridColumn37.FieldName = "VatCode";
		this.gridColumn37.MinWidth = 25;
		this.gridColumn37.Name = "gridColumn37";
		this.gridColumn37.OptionsColumn.AllowEdit = false;
		this.gridColumn37.OptionsColumn.AllowFocus = false;
		this.gridColumn38.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn38.AppearanceCell.Font");
		this.gridColumn38.AppearanceCell.Options.UseFont = true;
		this.gridColumn38.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn38.AppearanceHeader.Font");
		this.gridColumn38.AppearanceHeader.Options.UseFont = true;
		resources.ApplyResources(this.gridColumn38, "gridColumn38");
		this.gridColumn38.FieldName = "TaxExemptionReason";
		this.gridColumn38.MinWidth = 25;
		this.gridColumn38.Name = "gridColumn38";
		this.gridColumn38.OptionsColumn.AllowEdit = false;
		this.gridColumn38.OptionsColumn.AllowFocus = false;
		this.gridColumn39.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn39.AppearanceCell.Font");
		this.gridColumn39.AppearanceCell.Options.UseFont = true;
		this.gridColumn39.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn39.AppearanceHeader.Font");
		this.gridColumn39.AppearanceHeader.Options.UseFont = true;
		resources.ApplyResources(this.gridColumn39, "gridColumn39");
		this.gridColumn39.FieldName = "TaxExemptionReasonCode";
		this.gridColumn39.MinWidth = 25;
		this.gridColumn39.Name = "gridColumn39";
		this.gridColumn39.OptionsColumn.AllowEdit = false;
		this.gridColumn39.OptionsColumn.AllowFocus = false;
		this.cardView1.CardWidth = 250;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.DetailHeight = 437;
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		resources.ApplyResources(this.gridColumn10, "gridColumn10");
		this.gridColumn10.MinWidth = 24;
		this.gridColumn10.Name = "gridColumn10";
		resources.ApplyResources(this.gridColumn11, "gridColumn11");
		this.gridColumn11.MinWidth = 24;
		this.gridColumn11.Name = "gridColumn11";
		resources.ApplyResources(this.gridColumn12, "gridColumn12");
		this.gridColumn12.MinWidth = 24;
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn13, "gridColumn13");
		this.gridColumn13.MinWidth = 24;
		this.gridColumn13.Name = "gridColumn13";
		resources.ApplyResources(this.gridColumn14, "gridColumn14");
		this.gridColumn14.MinWidth = 24;
		this.gridColumn14.Name = "gridColumn14";
		resources.ApplyResources(this.gridColumn15, "gridColumn15");
		this.gridColumn15.MinWidth = 24;
		this.gridColumn15.Name = "gridColumn15";
		resources.ApplyResources(this.gridColumn16, "gridColumn16");
		this.gridColumn16.MinWidth = 24;
		this.gridColumn16.Name = "gridColumn16";
		resources.ApplyResources(this.gridColumn17, "gridColumn17");
		this.gridColumn17.MinWidth = 24;
		this.gridColumn17.Name = "gridColumn17";
		resources.ApplyResources(this.gridColumn18, "gridColumn18");
		this.gridColumn18.MinWidth = 24;
		this.gridColumn18.Name = "gridColumn18";
		resources.ApplyResources(this.label4, "label4");
		this.label4.Cursor = System.Windows.Forms.Cursors.Default;
		this.label4.ForeColor = System.Drawing.Color.FromArgb(32, 31, 53);
		this.label4.Name = "label4";
		resources.ApplyResources(this.label13, "label13");
		this.label13.Name = "label13";
		resources.ApplyResources(this.TxtDiscountPercent, "TxtDiscountPercent");
		this.TxtDiscountPercent.DecimalDigits = 2;
		this.TxtDiscountPercent.ForeColor = System.Drawing.Color.Red;
		this.TxtDiscountPercent.MaxLength = 99;
		this.TxtDiscountPercent.Name = "TxtDiscountPercent";
		this.TxtDiscountPercent.ReadOnly = true;
		this.TxtDiscountPercent.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscountPercent.Value = new decimal(new int[4]);
		this.TxtDiscountPercent.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscountPercent.TextChanged += new System.EventHandler(TxtDiscountPercent_TextChanged);
		this.TxtDiscountPercent.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtDiscountPercent.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtDiscountPercent_KeyUp);
		resources.ApplyResources(this.BtnDiscount, "BtnDiscount");
		this.BtnDiscount.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDiscount.Appearance.Font");
		this.BtnDiscount.Appearance.Options.UseFont = true;
		this.BtnDiscount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDiscount.ImageOptions.Image");
		this.BtnDiscount.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDiscount.Name = "BtnDiscount";
		this.BtnDiscount.Click += new System.EventHandler(BtnDiscount_Click);
		resources.ApplyResources(this.label27, "label27");
		this.label27.Name = "label27";
		resources.ApplyResources(this.TxtDiscountALLTotal, "TxtDiscountALLTotal");
		this.TxtDiscountALLTotal.DecimalDigits = 3;
		this.TxtDiscountALLTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscountALLTotal.Name = "TxtDiscountALLTotal";
		this.TxtDiscountALLTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscountALLTotal.Value = new decimal(new int[4]);
		this.TxtDiscountALLTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
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
		this.TxtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtBarcode_KeyDown);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.TxtBarcode);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.label27);
		base.Controls.Add(this.TxtDiscountALLTotal);
		base.Controls.Add(this.BtnDiscount);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.TxtDiscountPercent);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.gridControl1);
		base.Controls.Add(this.TxtVatBeforeDisc);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.xtraTabControl1);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.TxtAllVat);
		base.Controls.Add(this.TxtTabocc);
		base.Controls.Add(this.TxtAllTotal);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.TxtCost);
		base.Controls.Add(this.label9);
		base.Controls.Add(this.tileControl2);
		base.Controls.Add(this.TxtVat);
		base.Controls.Add(this.TxtTotalBeforDisc);
		base.Controls.Add(this.TxtNet);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.tileControl1);
		base.Controls.Add(this.TxtDiscount);
		base.Controls.Add(this.TxtTotals);
		base.Controls.Add(this.txtdicoutprege);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.TxtTotalAll);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.TxtNet22);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmCashirRestaurant";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmCashirRestaurant_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4Units).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.toggleSectoral.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.HoldOpenClose.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl1).EndInit();
		this.xtraTabControl1.ResumeLayout(false);
		this.TabSafry.ResumeLayout(false);
		this.TabMahly.ResumeLayout(false);
		this.TabTawsel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonADD).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


