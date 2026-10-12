using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Threading;
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
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Reports;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmCachMoney;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Frms.Reports.Orders;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Orders;

public class FrmOrdersWedding : XtraForm
{
	private DataTable set = new DataTable();

	private Class_MoreHall Class_More = new Class_MoreHall();

	private Class_Items ITM = new Class_Items();

	private ClassProject Project = new ClassProject();

	private Class_Orders ORD = new Class_Orders();

	private Class_PriceOffer PRCOffer = new Class_PriceOffer();

	private Class_Customer Cust = new Class_Customer();

	private Class_Bounce BNC = new Class_Bounce();

	private Class_Contract Contract = new Class_Contract();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private DataTable datatable = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private int tafqitID = LoginDetails.TafqitID;

	private int ItmID = 0;

	public string txtWordAr;

	public string txtWordEn;

	public string CodeItm;

	public int UserSellPrice = LoginDetails.UserSellPrice;

	public decimal FrmCash;

	public decimal FrmBank;

	public decimal Safy;

	public bool ISEVENTDISC = true;

	public bool ISEVENTDISCTotal = true;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	private decimal ChargeAfterSale = 0m;

	private string fileNameAR = Application.StartupPath + "\\Layout\\OrderAR.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\OrderEN.xml";

	private string file1 = Application.StartupPath + "\\Layout\\Order1.xml";

	private decimal? CreditLimit = 0m;

	private IContainer components = null;

	private TextEdit TxtLocation;

	private Label label25;

	private NumericEditBox txtdicoutprege;

	private Label label13;

	private TextEdit TxtTafqit;

	private Label label14;

	private NumericEditBox TxtQuntity;

	private Label label16;

	private Label label9;

	private Label LblNowQuintity;

	private NumericEditBox TxtAllQuantity;

	private NumericEditBox TxtLastCost;

	private SimpleButton BtnEdit;

	private Label label10;

	private NumericEditBox TxtAvergCost;

	private SimpleButton BtnSave;

	private NumericEditBox TxtNet22;

	private GridColumn gridColumn4;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem تخصيصToolStripMenuItem1;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem MenuExit;

	private GroupBox groupBox3;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn shearch;

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

	private GridColumn gridColumn18;

	private SimpleButton BtnPrint;

	private NumericEditBox TxtProfit;

	private Label label7;

	private SimpleButton BtnNew;

	private Label label8;

	private SimpleButton BtnDelete;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtSellPrice3;

	private NumericEditBox TxtTotals;

	private SimpleButton BtnExit;

	private NumericEditBox TxtTotalAll;

	private NumericEditBox TxtTotalBeforDisc;

	private Label label11;

	private NumericEditBox TxtNet;

	private Label label1;

	private Label label2;

	private NumericEditBox TxtSellPrice2;

	private Label label3;

	private Label label12;

	private NumericEditBox TxtSellPrice1;

	private Label label4;

	private NumericEditBox TxtDiscount;

	private NumericEditBox TxtVatBeforeDisc;

	private Label label5;

	private Label label26;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private Label LblSup;

	private DateEdit TxtDate;

	private Label label24;

	private TextEdit TxtNumNote;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private Label label23;

	private TextEdit TxtNote;

	private TextEdit TxtPhone;

	private Label label21;

	private TextEdit TxtNumVat;

	private Label label18;

	private ButtonEdit TxtSuppliers;

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private ToolStripMenuItem StripMenuItem1;

	private ToolStripMenuItem MenuPurchOrder;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem MenuSearchItem;

	private ToolStripMenuItem حركةصنفToolStripMenuItem;

	private ToolStripMenuItem MenuNewItem;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripMenuItem MenuPreviewFatoura;

	private ToolStripMenuItem MenuPrintQued;

	private ToolStripMenuItem طباعةToolStripMenuItem1;

	private ToolStripMenuItem MenuDelete;

	private ToolStripMenuItem MenuEdit;

	private ToolStripMenuItem MenuSave;

	private ToolStripMenuItem MenuNew;

	private ContextMenuStrip contextMenuStrip1;

	private RibbonPage ribbonPage2;

	private Label label6;

	private GroupBox groupBox2;

	private ToggleSwitch toggleHold;

	private GridColumn gridColumn23;

	private NumericEditBox TxtCost;

	private Label label27;

	private Label LBlSuppAccount;

	private GridColumn gridColumn24;

	private NumericEditBox TxtAllVat;

	private Label label28;

	private NumericEditBox TxtAllTotal;

	private Label label30;

	private NumericEditBox TxtTabocc;

	private Label label29;

	private GridColumn gridColumn25;

	private GridColumn gridColumn26;

	private GridColumn gridColumn27;

	private Label label20;

	private System.Windows.Forms.ComboBox TxtType;

	private Label label31;

	private System.Windows.Forms.ComboBox CBBounce;

	private Label label17;

	private ButtonEdit TxtCode;

	private ButtonEdit BtnCustomerSearch;

	private GridColumn gridColumn19;

	private ButtonEdit BtnOrderSearch;

	private GridColumn gridColumn28;

	private GridColumn gridColumn29;

	private GridColumn gridColumn30;

	private GridColumn gridColumn31;

	private GridColumn gridColumn32;

	private GridColumn gridColumn33;

	private GridColumn gridColumn34;

	private Label label32;

	private NumericEditBox TxtDiscountALLTotal;

	private GridColumn gridColumn35;

	private ToolStripMenuItem كشفحسابToolStripMenuItem;

	private ToolStripMenuItem حركهصنففعليةToolStripMenuItem;

	private SimpleButton BtnNewCustomer;

	private SimpleButton BtnOrderReturn;

	private SimpleButton BtnRecipt;

	private SimpleButton BtnShowPrice;

	private ToggleSwitch toggleSectoral;

	private GroupBox groupBox4;

	private Label label42;

	private Label label43;

	private TextEdit TxtCity;

	private Label label15;

	private Label label46;

	private TextEdit TxtSales;

	private GroupBox groupBox5;

	private Label label48;

	private Label label22;

	private Label label35;

	private Label label19;

	private TextEdit TxtAdditionalNum;

	private TextEdit TxtCommercialRecord;

	private TextEdit TxtPostalCode;

	private Label label36;

	private System.Windows.Forms.ComboBox Comp_Store;

	private GridColumn gridColumn36;

	private Label LblCustLast;

	private DateEdit DateHold;

	private Label LblDataHold;

	private Label label38;

	private Label label33;

	private TextEdit TxtDistrict;

	private TextEdit TxtCountry;

	private Label label34;

	private TextEdit TxtStreet;

	private Label label39;

	private System.Windows.Forms.ComboBox CBProject;

	private NumericEditBox TxtHasmPer;

	private Label LblPerHasm;

	private Label LblHasm;

	private Label LblAmountHasm;

	private NumericEditBox TxtHasmAmount;

	private GridColumn gridColumn37;

	private SimpleButton simpleButton1;

	private GroupBox groupBox1;

	private Label label40;

	private Label label41;

	private Label label44;

	private Label label45;

	private TextBox TxtKosha;

	private TextBox TxtCountCaress;

	private TextBox TxtCookCarecass;

	private TextBox TxtCountWomenTable;

	private TextBox TxtCountWomen;

	private TextBox TxtCountCandles;

	private TextBox TxtCountTableMen;

	private TextBox TxtNumberOfMen;

	private Label label47;

	private TextBox TxtCountHeaters;

	private Label label49;

	private TextBox TxtTypeCaress;

	private Label label50;

	private Label label51;

	private Label label52;

	private TextBox TxtTrombah;

	private Label label53;

	private Label label54;

	private Label label55;

	private GroupBox groupBox6;

	private TextBox TxtAnotherGroom;

	private TextBox TxtAddCappuccino;

	private TextBox TxtLesar;

	private TextBox TxtScreenAdd;

	private TextBox TxtAddHeadPhones;

	private TextBox TxtTarab;

	private TextBox TxtWedding;

	private Label label56;

	private Label label57;

	private TextBox TxtCurains;

	private Label label60;

	private Label label58;

	private Label label61;

	private Label label62;

	private Label label63;

	private Label label64;

	private TextBox TxtBff;

	private NumericEditBox TxtPriceCaress;

	private SimpleButton BtnReportHold;

	private Label label59;

	private TextEdit TxtCharge;

	private Label label37;

	public FrmOrdersWedding(DataTable data)
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
		TxtDigital();
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		TxtDate.DateTime = DateTime.Now;
		DateHold.DateTime = DateTime.Now;
		SetInitialRow();
		gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
		base.KeyPreview = true;
		gridView1.Columns["الكمية"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["الكمية"].DisplayFormat.FormatString = "n" + DecimalNumQty;
		gridView1.Columns["سعر الوحدة"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["سعر الوحدة"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.FocusedRowHandle = 1;
		gridView1.FocusedColumn = gridView1.VisibleColumns[0];
		gridView1.ShowEditor();
		BindComboBox();
		BindCBCostCenter();
		GroupPermission();
		TxtSuppliers.Text = "عميل نقدى";
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtCode.Focus();
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		txtdicoutprege.Controls[0].Visible = false;
		toggleSectoral.IsOn = true;
		ConvertFromPur(data);
		if (LoginDetails.FrmOrderID != 0)
		{
			SearchOrdNum(LoginDetails.FrmOrderID);
		}
		TxtCharge.Text = "0.00";
		Hasm(LoginDetails.Hasm);
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
		decimal num2 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "الطول"));
		decimal num3 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "العرض"));
		decimal num4 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "الإرتفاع"));
		if (text == "" || text2 == "" || num <= 0m || num2 <= 0m || num3 <= 0m || num4 <= 0m)
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
					MessageBox.Show("برجاء إدخال  طول أكبر من الصفر");
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.ShowEditor();
				}
				else if (num3 <= 0m)
				{
					MessageBox.Show("برجاء إدخال عرض أكبر  من الصفر");
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.ShowEditor();
				}
				else if (num4 <= 0m)
				{
					MessageBox.Show("برجاء إدخال ارتفاع أكبر من الصفر");
					gridView1.FocusedRowHandle = e.RowHandle;
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

	private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
	{
		int num = 0;
		if (e.Column.FieldName == "كود_الصنف")
		{
			string text = Convert.ToString(e.Value);
			Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ORD.GetItemByCode(text).FirstOrDefault();
			if (get_Item_ByCodeALL_Result != null)
			{
				if (0 == 0)
				{
					object rowCellValue = gridView1.GetRowCellValue(e.RowHandle, "تم");
					string text2 = rowCellValue.ToString();
					if (text2 == "0")
					{
						if (get_Item_ByCodeALL_Result != null)
						{
							DataRow dataRow = null;
							dataRow = datatable.NewRow();
							dataRow["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
							dataRow["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
							dataRow["كود_الصنف"] = get_Item_ByCodeALL_Result.Item_code;
							dataRow["أسم الصنف"] = get_Item_ByCodeALL_Result.item_Name;
							dataRow["الوحدة"] = get_Item_ByCodeALL_Result.UnitSmall;
							dataRow["رقم_الوحدة"] = get_Item_ByCodeALL_Result.UnitSmallID;
							dataRow["الكمية"] = FUNC_DecimalNumQty(1);
							dataRow["البونص"] = 0;
							dataRow["الطول"] = 1;
							dataRow["العرض"] = 1;
							dataRow["الإرتفاع"] = 1;
							dataRow["الوزن"] = 1;
							dataRow["قيمة الخصم"] = 0;
							dataRow["نسبة الخصم"] = 0;
							dataRow["إجمالى_بعدالخصم"] = 0;
							if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
							{
								dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitSmallID);
								dataRow["كمية التفقيط"] = FUNC_DecimalNum(dataRow["الكمية"].ToString());
								dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
							}
							if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
							{
								dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitMediumID);
								DataRow dataRow2 = dataRow;
								decimal value = 1;
								decimal? convertMediumUnit = get_Item_ByCodeALL_Result.ConvertMediumUnit;
								dataRow2["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
								dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
							}
							if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
							{
								dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitLargeID);
								DataRow dataRow3 = dataRow;
								decimal value = 1;
								decimal? convertMediumUnit = get_Item_ByCodeALL_Result.ConvertBigUnit;
								dataRow3["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
								dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
							}
							dataRow["مسح"] = 0;
							dataRow["تم"] = 1;
							dataRow["أسم_الوحدة"] = "الصغرى";
							dataRow["متوسط_التكلفة"] = get_Item_ByCodeALL_Result.Average_cost;
							dataRow["اجمالي_متوسط_التكلفة"] = get_Item_ByCodeALL_Result.Average_cost;
							dataRow["سعر الوحدة"] = get_Item_ByCodeALL_Result.SellPriceSmall;
							dataRow["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
							dataRow["المخزن"] = Comp_Store.Text;
							dataRow["رقم المخزن"] = Comp_Store.SelectedValue;
							Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, get_Item_ByCodeALL_Result.SellPriceSmall, 1m, dataRow, "كود_الصنف");
							dataRow["الربح"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["إجمالى_بعدالخصم"]) - Convert.ToDecimal(dataRow["اجمالي_متوسط_التكلفة"]));
							CheckFfistRow();
							datatable.Rows.Add(dataRow);
							insertnewrow();
							GetTotalCost();
							gridView1.FocusedRowHandle = e.RowHandle;
							gridView1.FocusedColumn = gridView1.VisibleColumns[2];
							gridView1.ShowEditor();
						}
						else
						{
							switch (MessageBox.Show("عفوا، كود الصنف غير موجود هل تريد إضافة الآن؟", "مستكشف البيانات ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
							{
							case DialogResult.Yes:
							{
								LoginDetails.ItemcodeNewItem = text;
								gridView1.DeleteRow(gridView1.FocusedRowHandle);
								insertnewrow();
								FrmItems frmItems = new FrmItems();
								frmItems.TxtItemCode.Text = text.ToString();
								frmItems.TxtItemCode.Focus();
								frmItems.ShowDialog();
								break;
							}
							case DialogResult.No:
								gridView1.DeleteRow(gridView1.FocusedRowHandle);
								insertnewrow();
								break;
							}
						}
					}
					else
					{
						foreach (DataRow row in datatable.Rows)
						{
							decimal num2 = Convert.ToDecimal(e.Value);
							string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
							string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
							decimal num3 = Convert.ToDecimal(gridView1.GetFocusedRowCellDisplayText("الكمية"));
							string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("م");
							if (!(row["م"].ToString() == focusedRowCellDisplayText3))
							{
								continue;
							}
							if (row["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
								row["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmall;
								row["كود_الصنف"] = get_Item_ByCodeALL_Result.Item_code;
								row["أسم الصنف"] = get_Item_ByCodeALL_Result.item_Name;
								row["قيمة الخصم"] = "0";
								row["نسبة الخصم"] = "0";
								row["سعر الوحدة"] = get_Item_ByCodeALL_Result.SellPriceSmall;
								row["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)num3);
								Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, get_Item_ByCodeALL_Result.SellPriceSmall, num3, row, "كود_الصنف");
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
								row["كود_الصنف"] = get_Item_ByCodeALL_Result.Item_code;
								row["أسم الصنف"] = get_Item_ByCodeALL_Result.item_Name;
								row["سعر الوحدة"] = get_Item_ByCodeALL_Result.SellPriceMedium;
								row["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium * (decimal?)num3);
								Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, get_Item_ByCodeALL_Result.SellPriceMedium, num3, row, "كود_الصنف");
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
								row["كود_الصنف"] = get_Item_ByCodeALL_Result.Item_code;
								row["أسم الصنف"] = get_Item_ByCodeALL_Result.item_Name;
								row["سعر الوحدة"] = get_Item_ByCodeALL_Result.SellpriceLarge;
								row["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge * (decimal?)num3);
								Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, get_Item_ByCodeALL_Result.SellpriceLarge, num3, row, "كود_الصنف");
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
					}
				}
				else
				{
					MessageBox.Show("تم إدخال هذا الصنف من قبل");
				}
				StockLimit(get_Item_ByCodeALL_Result.ItemId, Convert.ToInt32(Comp_Store.SelectedValue));
			}
			else
			{
				CodeItm = text;
				SearchItems(CodeItm);
				LoginDetails.ItemSearch = "";
			}
		}
		else if (e.Column.FieldName == "الكمية")
		{
			string value4 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText5 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText6 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText7 = gridView1.GetFocusedRowCellDisplayText("م");
			if (focusedRowCellDisplayText4 != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result2 = ITM.GetItemByCode(focusedRowCellDisplayText4).FirstOrDefault();
				if (get_Item_ByCodeALL_Result2 != null)
				{
					foreach (DataRow row2 in datatable.Rows)
					{
						if (!(row2["م"].ToString() == focusedRowCellDisplayText7))
						{
							continue;
						}
						decimal num4 = Convert.ToDecimal(row2["سعر الوحدة"].ToString());
						row2["الكمية"] = FUNC_DecimalNumQty(Convert.ToDecimal(value4));
						decimal num5 = Math.Round((Convert.ToDecimal(row2["الكمية"]) + Convert.ToDecimal(row2["البونص"])) * (Convert.ToDecimal(row2["الطول"]) * Convert.ToDecimal(row2["العرض"]) * Convert.ToDecimal(row2["الإرتفاع"])), DecimalNum);
						decimal num6 = Math.Round(Convert.ToDecimal(row2["الكمية"]) * (Convert.ToDecimal(row2["الطول"]) * Convert.ToDecimal(row2["العرض"]) * Convert.ToDecimal(row2["الإرتفاع"])), DecimalNum);
						decimal d = num4 * num6;
						if (row2["أسم_الوحدة"].ToString() == "الصغرى")
						{
							row2["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row2["متوسط_التكلفة"]) * num5);
							row2["الاجمالى"] = Math.Round(d, DecimalNum);
							Vat(get_Item_ByCodeALL_Result2.Is_Tax, get_Item_ByCodeALL_Result2.Tax_Value, num4, num6, row2, "الكمية");
							if (get_Item_ByCodeALL_Result2.UnitTafqitID == 1)
							{
								row2["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result2.UnitSmallID);
								row2["كمية التفقيط"] = FUNC_DecimalNum(row2["الكمية"].ToString());
								row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitSmallID;
							}
							if (get_Item_ByCodeALL_Result2.UnitTafqitID == 2)
							{
								row2["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result2.UnitMediumID);
								decimal value = 1;
								decimal? convertMediumUnit = get_Item_ByCodeALL_Result2.ConvertMediumUnit;
								row2["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(row2["الكمية"]));
								row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitMediumID;
							}
							if (get_Item_ByCodeALL_Result2.UnitTafqitID == 3)
							{
								row2["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result2.UnitLargeID);
								decimal value = 1;
								decimal? convertMediumUnit = get_Item_ByCodeALL_Result2.ConvertBigUnit;
								row2["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(row2["الكمية"]));
								row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitLargeID;
							}
							break;
						}
						if (row2["أسم_الوحدة"].ToString() == "المتوسطة")
						{
							decimal num7 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.ConvertMediumUnit);
							row2["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row2["متوسط_التكلفة"]) * (num5 * num7));
							row2["الاجمالى"] = Math.Round(num4 * num6, DecimalNum);
							Vat(get_Item_ByCodeALL_Result2.Is_Tax, get_Item_ByCodeALL_Result2.Tax_Value, num4, num6, row2, "الكمية");
							if (get_Item_ByCodeALL_Result2.UnitTafqitID == 1)
							{
								int value5 = Convert.ToInt32(get_Item_ByCodeALL_Result2.UnitSmallID);
								row2["وحدة التفقيط"] = ITM.GetUnitName(value5);
								row2["كمية التفقيط"] = FUNC_DecimalNum(row2["الكمية"].ToString());
								row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitSmallID;
							}
							if (get_Item_ByCodeALL_Result2.UnitTafqitID == 2)
							{
								row2["وحدة التفقيط"] = get_Item_ByCodeALL_Result2.UnitMedium;
								row2["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.ConvertMediumUnit / get_Item_ByCodeALL_Result2.ConvertMediumUnit * (decimal?)Convert.ToDecimal(row2["الكمية"]));
								row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitMediumID;
							}
							if (get_Item_ByCodeALL_Result2.UnitTafqitID == 3)
							{
								row2["وحدة التفقيط"] = get_Item_ByCodeALL_Result2.UnitLarge;
								row2["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.ConvertMediumUnit / get_Item_ByCodeALL_Result2.ConvertBigUnit * (decimal?)Convert.ToDecimal(row2["الكمية"]));
								row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitLargeID;
							}
							break;
						}
						if (row2["أسم_الوحدة"].ToString() == "الكبرى")
						{
							decimal num8 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.ConvertBigUnit);
							row2["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row2["متوسط_التكلفة"]) * (num5 * num8));
							row2["الاجمالى"] = Math.Round(num4 * num6, DecimalNum);
							Vat(get_Item_ByCodeALL_Result2.Is_Tax, get_Item_ByCodeALL_Result2.Tax_Value, num4, num6, row2, "الكمية");
							if (get_Item_ByCodeALL_Result2.UnitTafqitID == 1)
							{
								int value6 = Convert.ToInt32(get_Item_ByCodeALL_Result2.UnitSmallID);
								row2["وحدة التفقيط"] = ITM.GetUnitName(value6);
								row2["كمية التفقيط"] = FUNC_DecimalNum(row2["الكمية"].ToString());
								row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitSmallID;
							}
							if (get_Item_ByCodeALL_Result2.UnitTafqitID == 2)
							{
								row2["وحدة التفقيط"] = get_Item_ByCodeALL_Result2.UnitMedium;
								row2["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.ConvertBigUnit / get_Item_ByCodeALL_Result2.ConvertMediumUnit * (decimal?)Convert.ToDecimal(row2["الكمية"]));
								row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitMediumID;
							}
							if (get_Item_ByCodeALL_Result2.UnitTafqitID == 3)
							{
								row2["وحدة التفقيط"] = get_Item_ByCodeALL_Result2.UnitLarge;
								row2["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.ConvertBigUnit / get_Item_ByCodeALL_Result2.ConvertBigUnit * (decimal?)Convert.ToDecimal(row2["الكمية"]));
								row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitLargeID;
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
			decimal num9 = Convert.ToDecimal(e.Value);
			string focusedRowCellDisplayText8 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText9 = gridView1.GetFocusedRowCellDisplayText("م");
			if (focusedRowCellDisplayText8 != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result3 = ITM.GetItemByCode(focusedRowCellDisplayText8).FirstOrDefault();
				if (get_Item_ByCodeALL_Result3 != null)
				{
					foreach (DataRow row3 in datatable.Rows)
					{
						if (row3["م"].ToString() == focusedRowCellDisplayText9)
						{
							row3["الوزن"] = num9;
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
		else if (e.Column.FieldName == "سعر الوحدة")
		{
			decimal num10 = Convert.ToDecimal(e.Value);
			string focusedRowCellDisplayText10 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText11 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			decimal num11 = Convert.ToDecimal(gridView1.GetFocusedRowCellDisplayText("الكمية"));
			string focusedRowCellDisplayText12 = gridView1.GetFocusedRowCellDisplayText("م");
			if (focusedRowCellDisplayText10 != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result4 = ITM.GetItemByCode(focusedRowCellDisplayText10).FirstOrDefault();
				if (get_Item_ByCodeALL_Result4 != null)
				{
					foreach (DataRow row4 in datatable.Rows)
					{
						if (!(row4["م"].ToString() == focusedRowCellDisplayText12))
						{
							continue;
						}
						if (row4["أسم_الوحدة"].ToString() == "الصغرى")
						{
							decimal sellPermission = ORD.GetSellPermission(focusedRowCellDisplayText10, UserSellPrice, "الصغرى");
							if (UserSellPrice == 1)
							{
								if (num10 < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row4["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 2)
							{
								if (num10 < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row4["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 3)
							{
								if (num10 < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row4["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 4 && num10 < sellPermission)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row4["سعر الوحدة"] = sellPermission;
								return;
							}
							row4["سعر الوحدة"] = num10;
							row4["الاجمالى"] = FUNC_DecimalNum(num10 * num11);
							Vat(get_Item_ByCodeALL_Result4.Is_Tax, get_Item_ByCodeALL_Result4.Tax_Value, num10, num11, row4, "سعر الوحدة");
							break;
						}
						if (row4["أسم_الوحدة"].ToString() == "المتوسطة")
						{
							decimal sellPermission2 = ORD.GetSellPermission(focusedRowCellDisplayText10, UserSellPrice, "المتوسطة");
							if (UserSellPrice == 1)
							{
								if (num10 < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row4["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 2)
							{
								if (num10 < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row4["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 3)
							{
								if (num10 < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row4["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 4 && num10 < sellPermission2)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row4["سعر الوحدة"] = sellPermission2;
								return;
							}
							row4["سعر الوحدة"] = num10;
							row4["الاجمالى"] = FUNC_DecimalNum(num10 * num11);
							Vat(get_Item_ByCodeALL_Result4.Is_Tax, get_Item_ByCodeALL_Result4.Tax_Value, num10, num11, row4, "سعر الوحدة");
							break;
						}
						if (!(row4["أسم_الوحدة"].ToString() == "الكبرى"))
						{
							continue;
						}
						decimal sellPermission3 = ORD.GetSellPermission(focusedRowCellDisplayText10, UserSellPrice, "الكبرى");
						if (UserSellPrice == 1)
						{
							if (num10 < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row4["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 2)
						{
							if (num10 < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row4["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 3)
						{
							if (num10 < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row4["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 4 && num10 < sellPermission3)
						{
							MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							row4["سعر الوحدة"] = sellPermission3;
							return;
						}
						row4["سعر الوحدة"] = num10;
						row4["الاجمالى"] = FUNC_DecimalNum(num10 * num11);
						Vat(get_Item_ByCodeALL_Result4.Is_Tax, get_Item_ByCodeALL_Result4.Tax_Value, num10, num11, row4, "سعر الوحدة");
						break;
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
		else if (e.Column.FieldName == "نسبة الخصم")
		{
			ValueChangeDGV("نسبة الخصم", e);
		}
		else if (e.Column.FieldName == "كمية التفقيط")
		{
			ValueChangeTafqit("كمية التفقيط", e);
		}
		else if (e.Column.FieldName == "أسم الصنف")
		{
			string text3 = Convert.ToString(e.Value);
			Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result5 = ORD.GetItemByCode(text3).FirstOrDefault();
			if (get_Item_ByCodeALL_Result5 != null)
			{
				if (0 == 0)
				{
					object rowCellValue2 = gridView1.GetRowCellValue(e.RowHandle, "تم");
					string text4 = rowCellValue2.ToString();
					if (text4 == "0")
					{
						if (get_Item_ByCodeALL_Result5 != null)
						{
							DataRow dataRow8 = null;
							dataRow8 = datatable.NewRow();
							dataRow8["ItemID"] = get_Item_ByCodeALL_Result5.ItemId;
							dataRow8["ItemUnitID"] = get_Item_ByCodeALL_Result5.UnitSmallID;
							dataRow8["كود_الصنف"] = get_Item_ByCodeALL_Result5.Item_code;
							dataRow8["أسم الصنف"] = get_Item_ByCodeALL_Result5.item_Name;
							dataRow8["الوحدة"] = get_Item_ByCodeALL_Result5.UnitSmall;
							dataRow8["رقم_الوحدة"] = get_Item_ByCodeALL_Result5.UnitSmallID;
							dataRow8["الكمية"] = FUNC_DecimalNumQty(1);
							dataRow8["البونص"] = 0;
							dataRow8["الطول"] = 1;
							dataRow8["العرض"] = 1;
							dataRow8["الإرتفاع"] = 1;
							dataRow8["الوزن"] = 1;
							dataRow8["قيمة الخصم"] = 0;
							dataRow8["نسبة الخصم"] = 0;
							dataRow8["إجمالى_بعدالخصم"] = 0;
							if (get_Item_ByCodeALL_Result5.UnitTafqitID == 1)
							{
								dataRow8["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result5.UnitSmallID);
								dataRow8["كمية التفقيط"] = FUNC_DecimalNum(dataRow8["الكمية"].ToString());
								dataRow8["TafqitId"] = get_Item_ByCodeALL_Result5.UnitSmallID;
							}
							if (get_Item_ByCodeALL_Result5.UnitTafqitID == 2)
							{
								dataRow8["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result5.UnitMediumID);
								DataRow dataRow9 = dataRow8;
								decimal value = 1;
								decimal? convertMediumUnit = get_Item_ByCodeALL_Result5.ConvertMediumUnit;
								dataRow9["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow8["الكمية"]));
								dataRow8["TafqitId"] = get_Item_ByCodeALL_Result5.UnitMediumID;
							}
							if (get_Item_ByCodeALL_Result5.UnitTafqitID == 3)
							{
								dataRow8["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result5.UnitLargeID);
								DataRow dataRow10 = dataRow8;
								decimal value = 1;
								decimal? convertMediumUnit = get_Item_ByCodeALL_Result5.ConvertBigUnit;
								dataRow10["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow8["الكمية"]));
								dataRow8["TafqitId"] = get_Item_ByCodeALL_Result5.UnitLargeID;
							}
							dataRow8["مسح"] = 0;
							dataRow8["تم"] = 1;
							dataRow8["أسم_الوحدة"] = "الصغرى";
							dataRow8["متوسط_التكلفة"] = get_Item_ByCodeALL_Result5.Average_cost;
							dataRow8["اجمالي_متوسط_التكلفة"] = get_Item_ByCodeALL_Result5.Average_cost;
							dataRow8["سعر الوحدة"] = get_Item_ByCodeALL_Result5.SellPriceSmall;
							dataRow8["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result5.SellPriceSmall);
							dataRow8["المخزن"] = Comp_Store.Text;
							dataRow8["رقم المخزن"] = Comp_Store.SelectedValue;
							Vat(get_Item_ByCodeALL_Result5.Is_Tax, get_Item_ByCodeALL_Result5.Tax_Value, get_Item_ByCodeALL_Result5.SellPriceSmall, 1m, dataRow8, "كود_الصنف");
							CheckFfistRow();
							datatable.Rows.Add(dataRow8);
							insertnewrow();
							GetTotalCost();
							gridView1.FocusedRowHandle = e.RowHandle;
							gridView1.FocusedColumn = gridView1.VisibleColumns[2];
							gridView1.ShowEditor();
						}
						else
						{
							switch (MessageBox.Show("عفوا، كود الصنف غير موجود هل تريد إضافة الآن؟", "مستكشف البيانات ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
							{
							case DialogResult.Yes:
							{
								LoginDetails.ItemcodeNewItem = text3;
								gridView1.DeleteRow(gridView1.FocusedRowHandle);
								insertnewrow();
								FrmItems frmItems2 = new FrmItems();
								frmItems2.TxtItemCode.Text = text3.ToString();
								frmItems2.TxtItemCode.Focus();
								frmItems2.ShowDialog();
								break;
							}
							case DialogResult.No:
								gridView1.DeleteRow(gridView1.FocusedRowHandle);
								insertnewrow();
								break;
							}
						}
					}
					else
					{
						foreach (DataRow row5 in datatable.Rows)
						{
							decimal num12 = Convert.ToDecimal(e.Value);
							string focusedRowCellDisplayText13 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
							string focusedRowCellDisplayText14 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
							decimal num13 = Convert.ToDecimal(gridView1.GetFocusedRowCellDisplayText("الكمية"));
							string focusedRowCellDisplayText15 = gridView1.GetFocusedRowCellDisplayText("م");
							if (!(row5["م"].ToString() == focusedRowCellDisplayText15))
							{
								continue;
							}
							if (row5["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row5["ItemID"] = get_Item_ByCodeALL_Result5.ItemId;
								row5["ItemUnitID"] = get_Item_ByCodeALL_Result5.UnitSmall;
								row5["كود_الصنف"] = get_Item_ByCodeALL_Result5.Item_code;
								row5["أسم الصنف"] = get_Item_ByCodeALL_Result5.item_Name;
								row5["قيمة الخصم"] = "0";
								row5["نسبة الخصم"] = "0";
								row5["سعر الوحدة"] = get_Item_ByCodeALL_Result5.SellPriceSmall;
								row5["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result5.SellPriceSmall * (decimal?)num13);
								Vat(get_Item_ByCodeALL_Result5.Is_Tax, get_Item_ByCodeALL_Result5.Tax_Value, get_Item_ByCodeALL_Result5.SellPriceSmall, num13, row5, "كود_الصنف");
								if (get_Item_ByCodeALL_Result5.UnitTafqitID == 1)
								{
									row5["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result5.UnitSmallID);
									row5["كمية التفقيط"] = FUNC_DecimalNum(row5["الكمية"].ToString());
									row5["TafqitId"] = get_Item_ByCodeALL_Result5.UnitSmallID;
								}
								if (get_Item_ByCodeALL_Result5.UnitTafqitID == 2)
								{
									row5["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result5.UnitMediumID);
									decimal value = 1;
									decimal? convertMediumUnit = get_Item_ByCodeALL_Result5.ConvertMediumUnit;
									row5["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(row5["الكمية"]));
									row5["TafqitId"] = get_Item_ByCodeALL_Result5.UnitMediumID;
								}
								if (get_Item_ByCodeALL_Result5.UnitTafqitID == 3)
								{
									row5["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result5.UnitLargeID);
									decimal value = 1;
									decimal? convertMediumUnit = get_Item_ByCodeALL_Result5.ConvertBigUnit;
									row5["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(row5["الكمية"]));
									row5["TafqitId"] = get_Item_ByCodeALL_Result5.UnitLargeID;
								}
								break;
							}
							if (row5["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								row5["كود_الصنف"] = get_Item_ByCodeALL_Result5.Item_code;
								row5["أسم الصنف"] = get_Item_ByCodeALL_Result5.item_Name;
								row5["سعر الوحدة"] = get_Item_ByCodeALL_Result5.SellPriceMedium;
								row5["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result5.SellPriceMedium * (decimal?)num13);
								Vat(get_Item_ByCodeALL_Result5.Is_Tax, get_Item_ByCodeALL_Result5.Tax_Value, get_Item_ByCodeALL_Result5.SellPriceMedium, num13, row5, "كود_الصنف");
								if (get_Item_ByCodeALL_Result5.UnitTafqitID == 1)
								{
									int value7 = Convert.ToInt32(get_Item_ByCodeALL_Result5.UnitSmallID);
									row5["وحدة التفقيط"] = ITM.GetUnitName(value7);
									row5["كمية التفقيط"] = FUNC_DecimalNum(row5["الكمية"].ToString());
									row5["TafqitId"] = get_Item_ByCodeALL_Result5.UnitSmallID;
								}
								if (get_Item_ByCodeALL_Result5.UnitTafqitID == 2)
								{
									row5["وحدة التفقيط"] = get_Item_ByCodeALL_Result5.UnitMedium;
									row5["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result5.ConvertMediumUnit / get_Item_ByCodeALL_Result5.ConvertMediumUnit * (decimal?)Convert.ToDecimal(row5["الكمية"]));
									row5["TafqitId"] = get_Item_ByCodeALL_Result5.UnitMediumID;
								}
								if (get_Item_ByCodeALL_Result5.UnitTafqitID == 3)
								{
									row5["وحدة التفقيط"] = get_Item_ByCodeALL_Result5.UnitLarge;
									row5["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result5.ConvertMediumUnit / get_Item_ByCodeALL_Result5.ConvertBigUnit * (decimal?)Convert.ToDecimal(row5["الكمية"]));
									row5["TafqitId"] = get_Item_ByCodeALL_Result5.UnitLargeID;
								}
								break;
							}
							if (row5["أسم_الوحدة"].ToString() == "الكبرى")
							{
								row5["كود_الصنف"] = get_Item_ByCodeALL_Result5.Item_code;
								row5["أسم الصنف"] = get_Item_ByCodeALL_Result5.item_Name;
								row5["سعر الوحدة"] = get_Item_ByCodeALL_Result5.SellpriceLarge;
								row5["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result5.SellpriceLarge * (decimal?)num13);
								Vat(get_Item_ByCodeALL_Result5.Is_Tax, get_Item_ByCodeALL_Result5.Tax_Value, get_Item_ByCodeALL_Result5.SellpriceLarge, num13, row5, "كود_الصنف");
								if (get_Item_ByCodeALL_Result5.UnitTafqitID == 1)
								{
									int value8 = Convert.ToInt32(get_Item_ByCodeALL_Result5.UnitSmallID);
									row5["وحدة التفقيط"] = ITM.GetUnitName(value8);
									row5["كمية التفقيط"] = FUNC_DecimalNum(row5["الكمية"].ToString());
									row5["TafqitId"] = get_Item_ByCodeALL_Result5.UnitSmallID;
								}
								if (get_Item_ByCodeALL_Result5.UnitTafqitID == 2)
								{
									row5["وحدة التفقيط"] = get_Item_ByCodeALL_Result5.UnitMedium;
									row5["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result5.ConvertBigUnit / get_Item_ByCodeALL_Result5.ConvertMediumUnit * (decimal?)Convert.ToDecimal(row5["الكمية"]));
									row5["TafqitId"] = get_Item_ByCodeALL_Result5.UnitMediumID;
								}
								if (get_Item_ByCodeALL_Result5.UnitTafqitID == 3)
								{
									row5["وحدة التفقيط"] = get_Item_ByCodeALL_Result5.UnitLarge;
									row5["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result5.ConvertBigUnit / get_Item_ByCodeALL_Result5.ConvertBigUnit * (decimal?)Convert.ToDecimal(row5["الكمية"]));
									row5["TafqitId"] = get_Item_ByCodeALL_Result5.UnitLargeID;
								}
								break;
							}
						}
					}
				}
				else
				{
					MessageBox.Show("تم إدخال هذا الصنف من قبل");
				}
			}
			else
			{
				CodeItm = text3;
				SearchItems(CodeItm);
				LoginDetails.ItemSearch = "";
			}
		}
		else if (e.Column.FieldName == "متوسط_التكلفة")
		{
			string value9 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText16 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText17 = gridView1.GetFocusedRowCellDisplayText("م");
			if (focusedRowCellDisplayText16 != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result6 = ITM.GetItemByCode(focusedRowCellDisplayText16).FirstOrDefault();
				if (get_Item_ByCodeALL_Result6 != null)
				{
					foreach (DataRow row6 in datatable.Rows)
					{
						if (row6["م"].ToString() == focusedRowCellDisplayText17)
						{
							row6["متوسط_التكلفة"] = FUNC_DecimalNumQty(Convert.ToDecimal(value9));
							decimal num14 = Math.Round((Convert.ToDecimal(row6["الكمية"]) + Convert.ToDecimal(row6["البونص"])) * (Convert.ToDecimal(row6["الطول"]) * Convert.ToDecimal(row6["العرض"]) * Convert.ToDecimal(row6["الإرتفاع"])), DecimalNum);
							row6["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row6["متوسط_التكلفة"]) * num14);
							row6["الربح"] = FUNC_DecimalNum(Convert.ToDecimal(row6["إجمالى_بعدالخصم"]) - Convert.ToDecimal(row6["اجمالي_متوسط_التكلفة"]));
						}
					}
				}
			}
		}
		else
		{
			if (!(e.Column.FieldName == "إجمالى المبلغ"))
			{
				return;
			}
			string value10 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText18 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText19 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText20 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText21 = gridView1.GetFocusedRowCellDisplayText("الكمية");
			string focusedRowCellDisplayText22 = gridView1.GetFocusedRowCellDisplayText("م");
			if (focusedRowCellDisplayText18 != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result7 = ITM.GetItemByCode(focusedRowCellDisplayText18).FirstOrDefault();
				if (get_Item_ByCodeALL_Result7 != null)
				{
					foreach (DataRow row7 in datatable.Rows)
					{
						if (row7["م"].ToString() == focusedRowCellDisplayText22)
						{
							row7["سعر الوحدة"] = Math.Round(Convert.ToDecimal(value10) / Convert.ToDecimal(1.15) / Convert.ToDecimal(row7["الكمية"]), DecimalNum);
							row7["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row7["سعر الوحدة"]) * Convert.ToDecimal(focusedRowCellDisplayText21)), DecimalNum);
							Vat(get_Item_ByCodeALL_Result7.Is_Tax, get_Item_ByCodeALL_Result7.Tax_Value, Convert.ToDecimal(row7["سعر الوحدة"]), Convert.ToDecimal(row7["الكمية"]), row7, "سعر الوحدة");
							row7["إجمالى المبلغ"] = value10;
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
		ArrangeDatatable();
		Cal_FinalResultTotals();
		gridControl1.DataSource = datatable;
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

	private void gridControl_ProcessGridKey(object sender, KeyEventArgs e)
	{
		GridControl gridControl = sender as GridControl;
		GridView gridView = gridControl.FocusedView as GridView;
		if (e.KeyData == Keys.Delete)
		{
			gridView.DeleteSelectedRows();
			e.Handled = true;
		}
	}

	private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
	{
		try
		{
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("ItemID");
			if (focusedRowCellDisplayText != "")
			{
				ItmID = Convert.ToInt32(gridView1.GetFocusedRowCellDisplayText("ItemID"));
			}
			string codeItm = Convert.ToString(gridView1.GetFocusedRowCellDisplayText("كود_الصنف"));
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string StoreName = gridView1.GetFocusedRowCellDisplayText("المخزن");
			int storeID = (from x in gTSdbEntities.Account_Stores
				where x.Store_Name == StoreName
				select x.ID).FirstOrDefault();
			GetAllitems_alternatives(ItmID, storeID, focusedRowCellDisplayText2);
			CodeItm = codeItm;
		}
		catch (Exception)
		{
		}
	}

	private void gridControl1_ProcessGridKey(object sender, KeyEventArgs e)
	{
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		NewFrm();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchOrders frmSearchOrders = new FrmSearchOrders();
		frmSearchOrders.ShowDialog();
		if (frmSearchOrders.CustCode > 0)
		{
			SearchOrdNum(frmSearchOrders.CustCode);
		}
	}

	private void TxtCust_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		using (new GTSdbEntities())
		{
			try
			{
				FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
				frmSearchCustomer.ShowDialog();
				if (frmSearchCustomer.CustID > 0)
				{
					SearchCustNum(frmSearchCustomer.CustID);
				}
			}
			catch (Exception)
			{
				Clear();
			}
		}
	}

	private void SearchCustNum(int ID, int codeCust = 0)
	{
		try
		{
			Account_CustSup account_CustSup = null;
			if (codeCust > 0)
			{
				account_CustSup = (from x in Cust.GetAll()
					where x.ID == ID && x.IsCustomers == true
					select x).FirstOrDefault();
				if (account_CustSup == null)
				{
					return;
				}
				ID = account_CustSup.ID;
			}
			if (ID > 0)
			{
				account_CustSup = (from x in Cust.GetAll()
					where x.ID == ID && x.IsCustomers == true
					select x).FirstOrDefault();
				if (account_CustSup != null)
				{
					LblSup.Text = ID.ToString();
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
					BtnCustomerSearch.Text = account_CustSup.ID.ToString();
					Charge(ID);
				}
				else
				{
					BtnCustomerSearch.Text = "";
				}
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void SearchOrdNum(int OrderSearch)
	{
		try
		{
			if (OrderSearch <= 0)
			{
				return;
			}
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
			Order_Orders order_Orders = ORD.GetPurchases(OrderSearch).FirstOrDefault();
			if (order_Orders != null)
			{
				datatable.Rows.Clear();
				List<Order_OrdersDetails> itemsPurchesByID = ORD.GetItemsPurchesByID(OrderSearch);
				int num = 1;
				foreach (Order_OrdersDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = ORD.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = item_Items.ItemId;
					dataRow["ItemUnitID"] = item.ItemUnitID;
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["كود_الصنف"] = item_Items.Item_code;
					dataRow["أسم الصنف"] = item_Items.item_Name;
					dataRow["البونص"] = FUNC_DecimalNum(item.Bounce);
					dataRow["الطول"] = FUNC_DecimalNum(item.Long);
					dataRow["العرض"] = FUNC_DecimalNum(item.width);
					dataRow["الإرتفاع"] = FUNC_DecimalNum(item.Height);
					dataRow["الوزن"] = FUNC_DecimalNum(item.Weight);
					dataRow["وحدة التفقيط"] = gTSdbEntities.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.TafqitUnitId).Name;
					dataRow["كمية التفقيط"] = FUNC_DecimalNum(item.TafqitQunitity);
					dataRow["TafqitId"] = item_Items.UnitTafqitID;
					dataRow["الوزن"] = FUNC_DecimalNum(item.Weight);
					dataRow["قيمة الخصم"] = FUNC_DecimalNum(item.Amount_Discount);
					dataRow["نسبة الخصم"] = FUNC_DecimalNum(item.Per_Discount);
					dataRow["إجمالى_بعدالخصم"] = FUNC_DecimalNum(item.NetTotalPrice);
					dataRow["الصافى"] = FUNC_DecimalNum(item.TotalPrice);
					dataRow["رقم_الوحدة"] = item.ItemUnitID;
					decimal value = FUNC_DecimalNum((item.Quantity + item.Bounce) * (item.Long * item.width * item.Height));
					decimal num2 = FUNC_DecimalNum(item.Quantity * (item.Long * item.width * item.Height));
					if (item.ItemUnitType == "الصغرى")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
					}
					if (item.ItemUnitType == "المتوسطة")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
					}
					if (item.ItemUnitType == "الكبرى")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
					}
					dataRow["أسم_الوحدة"] = item.ItemUnitType;
					dataRow["رقم المخزن"] = item.StoreID;
					dataRow["المخزن"] = (from x in gTSdbEntities.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["الكمية"] = FUNC_DecimalNumQty(item.Quantity);
					dataRow["متوسط_التكلفة"] = FUNC_DecimalNum(item.LastCost);
					dataRow["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(item.LastCost * (decimal?)value);
					dataRow["سعر الوحدة"] = FUNC_DecimalNum(item.UnitPrice);
					dataRow["الاجمالى"] = FUNC_DecimalNum(item.TotalPrice);
					dataRow["مسح"] = 0;
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["الضربية"] = FUNC_DecimalNum(item.VAT);
						dataRow["إجمالى المبلغ"] = FUNC_DecimalNum(item.TotalPrice + item.VAT);
						dataRow["هل_ضريبة"] = 1;
						dataRow["قيمة_الضريبة"] = FUNC_DecimalNum(item_Items.Tax_Value);
					}
					else
					{
						dataRow["هل_ضريبة"] = 0;
						dataRow["قيمة_الضريبة"] = 0;
						dataRow["الضربية"] = FUNC_DecimalNum(item.VAT);
						dataRow["إجمالى المبلغ"] = FUNC_DecimalNum(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT));
					}
					dataRow["الربح"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["إجمالى_بعدالخصم"]) - Convert.ToDecimal(dataRow["اجمالي_متوسط_التكلفة"]));
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				insertnewrow();
				TxtType.SelectedValue = order_Orders.Order_Paymant_Type;
				CBCostCenter.SelectedValue = order_Orders.CostCentersID;
				if (order_Orders.BounceID.HasValue)
				{
					CBBounce.SelectedValue = order_Orders.BounceID;
				}
				BtnCustomerSearch.Text = "";
				TxtCode.Text = order_Orders.PurBranchID.ToString();
				LblSup.Text = order_Orders.SupplierID.ToString();
				TxtSuppliers.Text = order_Orders.SupplierName;
				TxtPhone.Text = order_Orders.SupplierPhone;
				TxtAdditionalNum.Text = order_Orders.AdditionalNum;
				TxtPostalCode.Text = order_Orders.PostalCode;
				TxtCommercialRecord.Text = order_Orders.CommercialRecord;
				TxtSales.Text = order_Orders.SalesMan;
				TxtCity.Text = order_Orders.City;
				TxtStreet.Text = order_Orders.Street;
				TxtDistrict.Text = order_Orders.District;
				TxtCountry.Text = order_Orders.Country;
				TxtNumVat.Text = order_Orders.SupplierVatNum;
				TxtDate.DateTime = Convert.ToDateTime(order_Orders.Purchases_Date);
				TxtNote.Text = order_Orders.Note;
				TxtNumNote.Text = order_Orders.NoteNum;
				toggleHold.IsOn = Convert.ToBoolean(order_Orders.IsWaiting);
				TxtTotals.Text = Convert.ToString(order_Orders.TotalPrices);
				TxtVat.Text = Convert.ToString(order_Orders.Tax);
				TxtTotalAll.Text = Convert.ToString(order_Orders.TotalPrices);
				TxtDiscount.Text = order_Orders.DiscountNum.ToString();
				TxtTotalBeforDisc.Text = order_Orders.TotalPrices_Discount.ToString();
				TxtVatBeforeDisc.Text = order_Orders.Tax_Discount.ToString();
				TxtNet.Text = order_Orders.TotalPrices_Discount.ToString();
				TxtNet22.Text = order_Orders.Net.ToString();
				TxtCost.Text = order_Orders.CostOrder.ToString();
				TxtTabocc.Text = order_Orders.TobaccoTax.ToString();
				TxtAllVat.Text = order_Orders.AllTax.ToString();
				LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
				LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum);
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = true;
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = false;
				FiterDatatableItemsUpdate();
				ArrangeDatatable();
				IsEnableFalse();
				Charge(Convert.ToInt32(LblSup.Text));
				Order_MoveHall order_MoveHall = gTSdbEntities.Order_MoveHall.FirstOrDefault((Order_MoveHall x) => x.OrderId == (int?)OrderSearch);
				if (order_MoveHall != null)
				{
					TxtNumberOfMen.Text = order_MoveHall.NumberOfMen;
					TxtCountTableMen.Text = order_MoveHall.TheNumberOfMenslengths;
					TxtCountCaress.Text = order_MoveHall.TheNumberOfCarcasses;
					TxtCookCarecass.Text = order_MoveHall.CookingCarcassesOnItsBottom;
					TxtTypeCaress.Text = order_MoveHall.CarcassType;
					TxtPriceCaress.Text = order_MoveHall.ThePriceOfTheSacrifice.ToString();
					TxtTrombah.Text = order_MoveHall.Trombeh;
					TxtBff.Text = order_MoveHall.Bff;
					TxtCountWomen.Text = order_MoveHall.NumberOfWomen;
					TxtCountWomenTable.Text = order_MoveHall.TheNumberOfWomensTables;
					TxtCountCandles.Text = order_MoveHall.NumberOfWomensCandlesticks;
					TxtCountHeaters.Text = order_MoveHall.NumberOfHeaters;
					TxtKosha.Text = order_MoveHall.Kosha;
					TxtAddHeadPhones.Text = order_MoveHall.TurnOnHeadphones;
					TxtWedding.Text = order_MoveHall.AddAwedding;
					TxtCurains.Text = order_MoveHall.AddCurains;
					TxtAddCappuccino.Text = order_MoveHall.NescafeAndCappuccino;
					TxtLesar.Text = order_MoveHall.LaserDevices;
					TxtScreenAdd.Text = order_MoveHall.AddADisplay;
					TxtTarab.Text = order_MoveHall.TheAdditionOfMensRapture;
					TxtAnotherGroom.Text = order_MoveHall.AnotherGroom;
				}
			}
			else
			{
				BtnOrderSearch.Text = "";
				BtnOrderSearch.Focus();
			}
		}
		catch (Exception)
		{
		}
	}

	private void SearchCustomer()
	{
		using (new GTSdbEntities())
		{
			try
			{
				FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
				frmSearchCustomer.ShowDialog();
				if (frmSearchCustomer.CustID <= 0)
				{
				}
			}
			catch (Exception)
			{
				Clear();
			}
		}
	}

	private void SearchOrderNum()
	{
		try
		{
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
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
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = ORD.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = item_Items.ItemId;
					dataRow["ItemUnitID"] = item_Items.UnitSmall;
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["كود_الصنف"] = item_Items.Item_code;
					dataRow["أسم الصنف"] = item_Items.item_Name;
					dataRow["البونص"] = FUNC_DecimalNum(item.Bounce);
					dataRow["الطول"] = FUNC_DecimalNum(item.Long);
					dataRow["العرض"] = FUNC_DecimalNum(item.width);
					dataRow["الإرتفاع"] = FUNC_DecimalNum(item.Height);
					dataRow["الوزن"] = FUNC_DecimalNum(item.Weight);
					dataRow["قيمة الخصم"] = FUNC_DecimalNum(item.Amount_Discount);
					dataRow["نسبة الخصم"] = FUNC_DecimalNum(item.Per_Discount);
					dataRow["إجمالى_بعدالخصم"] = FUNC_DecimalNum(item.NetTotalPrice);
					dataRow["الصافى"] = FUNC_DecimalNum(item.TotalPrice);
					dataRow["رقم_الوحدة"] = item.ItemUnitID;
					decimal value = FUNC_DecimalNum((item.Quantity + item.Bounce) * (item.Long * item.width * item.Height));
					decimal num2 = FUNC_DecimalNum(item.Quantity * (item.Long * item.width * item.Height));
					if (item.ItemUnitType == "الصغرى")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
					}
					if (item.ItemUnitType == "المتوسطة")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
					}
					if (item.ItemUnitType == "الكبرى")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
					}
					dataRow["أسم_الوحدة"] = item.ItemUnitType;
					dataRow["رقم المخزن"] = item.StoreID;
					dataRow["المخزن"] = gTSdbEntities.Account_Stores.FirstOrDefault((Account_Stores x) => (int?)x.ID == item.StoreID).Store_Name;
					dataRow["الكمية"] = FUNC_DecimalNumQty(item.Quantity);
					dataRow["متوسط_التكلفة"] = item.LastCost;
					dataRow["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(item.LastCost * (decimal?)value);
					dataRow["سعر الوحدة"] = item.UnitPrice;
					dataRow["الاجمالى"] = FUNC_DecimalNum(item.TotalPrice);
					dataRow["مسح"] = 0;
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["الضربية"] = FUNC_DecimalNum(item.VAT);
						dataRow["إجمالى المبلغ"] = FUNC_DecimalNum(item.TotalPrice + item.VAT);
						dataRow["هل_ضريبة"] = 1;
						dataRow["قيمة_الضريبة"] = FUNC_DecimalNum(item_Items.Tax_Value);
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
				insertnewrow();
				TxtType.SelectedValue = order_Orders.Order_Paymant_Type;
				CBCostCenter.SelectedValue = order_Orders.CostCentersID;
				if (order_Orders.BounceID.HasValue)
				{
					CBBounce.SelectedValue = order_Orders.BounceID;
				}
				TxtCode.Text = order_Orders.PurBranchID.ToString();
				LblSup.Text = order_Orders.SupplierID.ToString();
				TxtSuppliers.Text = order_Orders.SupplierName;
				TxtPhone.Text = order_Orders.SupplierPhone;
				TxtNumVat.Text = order_Orders.SupplierVatNum;
				TxtDate.DateTime = Convert.ToDateTime(order_Orders.Purchases_Date);
				TxtNote.Text = order_Orders.Note;
				TxtNumNote.Text = order_Orders.NoteNum;
				toggleHold.IsOn = Convert.ToBoolean(order_Orders.IsWaiting);
				TxtTotals.Text = Convert.ToString(order_Orders.TotalPrices);
				TxtVat.Text = Convert.ToString(order_Orders.Tax);
				TxtTotalAll.Text = Convert.ToString(order_Orders.TotalPrices);
				TxtDiscount.Text = order_Orders.DiscountNum.ToString();
				TxtTotalBeforDisc.Text = order_Orders.TotalPrices_Discount.ToString();
				TxtVatBeforeDisc.Text = order_Orders.Tax_Discount.ToString();
				TxtNet.Text = order_Orders.TotalPrices_Discount.ToString();
				TxtNet22.Text = order_Orders.Net.ToString();
				TxtCost.Text = order_Orders.CostOrder.ToString();
				TxtTabocc.Text = order_Orders.TobaccoTax.ToString();
				TxtAllVat.Text = order_Orders.AllTax.ToString();
				int cusID = Convert.ToInt32(order_Orders.SupplierID);
				TxtSales.Text = Cust.GetSalesManByCusID(cusID);
				LoginDetails.NetCash = FUNC_DecimalNum(Convert.ToDecimal(TxtNet.Text));
				LoginDetails.VatCash = FUNC_DecimalNum(Convert.ToDecimal(TxtVatBeforeDisc.Text));
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = true;
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = false;
				FiterDatatableItemsUpdate();
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
	}

	public void BindComboBox()
	{
		try
		{
			TxtType.DataSource = ORD.GetAccountType();
			TxtType.DisplayMember = "Name";
			TxtType.ValueMember = "ID";
			CBBounce.DataSource = BNC.GetAll();
			CBBounce.DisplayMember = "Name";
			CBBounce.ValueMember = "ID";
			Comp_Store.DataSource = Contract.Get_Stores();
			Comp_Store.DisplayMember = "Store_Name";
			Comp_Store.ValueMember = "ID";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void BindCBCostCenter()
	{
		try
		{
			CBProject.DataSource = Project.GetSearchAccountProjects();
			CBProject.DisplayMember = "ProjectName";
			CBProject.ValueMember = "ProjectID";
			if (CBProject.Items.Count > 0)
			{
				CBCostCenter.DataSource = Cost.GetCostCentersByProject(Convert.ToInt32(CBProject.SelectedValue));
				CBCostCenter.DisplayMember = "CostCentersName";
				CBCostCenter.ValueMember = "CostCentersID";
				CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void CheckFfistRow()
	{
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToString(row["أسم الصنف"]) == "")
			{
				row.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
		gridControl1.DataSource = datatable;
	}

	public int CheckIfRowFound(string ItemCode, int Type)
	{
		int result = 0;
		if (Type == 1)
		{
			for (int num = datatable.Rows.Count - 1; num >= 0; num--)
			{
				DataRow dataRow = datatable.Rows[num];
				if (dataRow["كود_الصنف"].ToString() == ItemCode)
				{
					result = 1;
					break;
				}
			}
		}
		else
		{
			for (int num2 = datatable.Rows.Count - 1; num2 >= 0; num2--)
			{
				DataRow dataRow2 = datatable.Rows[num2];
				if (dataRow2["أسم الصنف"].ToString() == ItemCode)
				{
					result = 1;
					break;
				}
			}
		}
		return result;
	}

	public void GetTotalCost()
	{
		object obj = datatable.Compute("SUM(إجمالى_بعدالخصم)", "");
		TxtTotals.Text = Convert.ToString(obj);
		TxtTotalAll.Text = Convert.ToString(obj);
		object obj2 = datatable.Compute("SUM(الضربية)", "");
		TxtVat.Text = Convert.ToString(obj2);
	}

	public void StockLimit(int ItemID, int StoreID)
	{
		decimal num = 0m;
		decimal num2 = 0m;
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		Item_Items item_Items = gTSdbEntities.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault();
		if (item_Items != null)
		{
			num = Convert.ToDecimal(item_Items.SmallUnitQuantity);
		}
		if (num != 0m)
		{
			ItemQuantity itemQuantity = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
			if (itemQuantity != null)
			{
				num2 = Convert.ToDecimal(itemQuantity.CurrentBalance);
			}
			if (num2 <= num)
			{
				MessageBox.Show("تنبيه، حد الطلب ( " + FUNC_DecimalNumQty(num) + " ) بالبرنامج \r\n والكمية بالمخزن  ( " + FUNC_DecimalNumQty(num2) + " ) ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}

	private void ValueChangeTafqit(string TxtChange, CellValueChangedEventArgs e)
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
			if (row["أسم_الوحدة"].ToString() == "الصغرى")
			{
				row["كمية التفقيط"] = num;
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
				{
					row["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitSmallID);
					row["الكمية"] = FUNC_DecimalNumQty(Convert.ToDecimal(row["كمية التفقيط"].ToString()));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
				{
					row["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitMediumID);
					row["الكمية"] = FUNC_DecimalNumQty(get_Item_ByCodeALL_Result.ConvertMediumUnit * (decimal?)Convert.ToDecimal(row["كمية التفقيط"]));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
				{
					row["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitLargeID);
					decimal value = 1;
					decimal? convertBigUnit = get_Item_ByCodeALL_Result.ConvertBigUnit;
					row["الكمية"] = FUNC_DecimalNumQty((decimal?)value / convertBigUnit * (decimal?)Convert.ToDecimal(row["كمية التفقيط"]));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
				}
				row["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]));
				row["الاجمالى"] = FUNC_DecimalNum(num2 * Convert.ToDecimal(row["الكمية"]));
				Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, num2, num, row, "كمية التفقيط");
				break;
			}
			if (row["أسم_الوحدة"].ToString() == "المتوسطة")
			{
				decimal num3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertMediumUnit);
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
				{
					int value2 = Convert.ToInt32(get_Item_ByCodeALL_Result.UnitSmallID);
					row["وحدة التفقيط"] = ITM.GetUnitName(value2);
					row["الكمية"] = FUNC_DecimalNumQty(Convert.ToDecimal(row["كمية التفقيط"].ToString()));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
				{
					row["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitMedium;
					decimal value3 = Convert.ToDecimal(row["كمية التفقيط"]);
					decimal? convertMediumUnit = get_Item_ByCodeALL_Result.ConvertMediumUnit;
					row["الكمية"] = FUNC_DecimalNumQty((decimal?)value3 / convertMediumUnit);
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
				{
					row["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitLarge;
					row["الكمية"] = FUNC_DecimalNumQty(get_Item_ByCodeALL_Result.ConvertMediumUnit / get_Item_ByCodeALL_Result.ConvertBigUnit * (decimal?)Convert.ToDecimal(row["كمية التفقيط"]));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
				}
				Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, num2, num, row, "كمية التفقيط");
				row["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row["متوسط_التكلفة"]) * (Convert.ToDecimal(row["الكمية"]) * num3));
				row["الاجمالى"] = FUNC_DecimalNum(num2 * num);
				break;
			}
			if (row["أسم_الوحدة"].ToString() == "الكبرى")
			{
				decimal num4 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertBigUnit);
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
				{
					int value4 = Convert.ToInt32(get_Item_ByCodeALL_Result.UnitSmallID);
					row["وحدة التفقيط"] = ITM.GetUnitName(value4);
					row["الكمية"] = FUNC_DecimalNumQty(Convert.ToDecimal(row["كمية التفقيط"].ToString()));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
				{
					row["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitMedium;
					row["الكمية"] = FUNC_DecimalNumQty(get_Item_ByCodeALL_Result.ConvertBigUnit / get_Item_ByCodeALL_Result.ConvertMediumUnit);
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
				{
					row["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitLarge;
					row["الكمية"] = FUNC_DecimalNumQty(get_Item_ByCodeALL_Result.ConvertBigUnit / get_Item_ByCodeALL_Result.ConvertBigUnit * (decimal?)Convert.ToDecimal(row["كمية التفقيط"]));
					row["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
				}
				row["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row["متوسط_التكلفة"]) * (Convert.ToDecimal(row["الكمية"]) * num4));
				row["الاجمالى"] = FUNC_DecimalNum(num2 * num);
				Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, num2, num, row, "كمية التفقيط");
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

	public void insertnewrow()
	{
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["ItemID"] = "0";
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["الاجمالى"] = 0;
		dataRow["إجمالى المبلغ"] = 0;
		dataRow["سعر الوحدة"] = 0;
		dataRow["الكمية"] = FUNC_DecimalNumQty(1);
		dataRow["المخزن"] = Comp_Store.Text;
		dataRow["رقم المخزن"] = Comp_Store.SelectedValue;
		dataRow["تم"] = 0;
		dataRow["البونص"] = 0;
		dataRow["الطول"] = 1;
		dataRow["العرض"] = 1;
		dataRow["الإرتفاع"] = 1;
		dataRow["الوزن"] = 1;
		dataRow["قيمة الخصم"] = 0;
		dataRow["نسبة الخصم"] = 0;
		dataRow["الربح"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
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
				TxtAvergCost.Text = getAllitems_Alternatives_Result.Average_cost.ToString();
				TxtLastCost.Text = getAllitems_Alternatives_Result.LastCost.ToString();
				TxtSellPrice1.Text = getAllitems_Alternatives_Result.SellPriceSmall.ToString();
				TxtSellPrice2.Text = getAllitems_Alternatives_Result.SellPriceSmall2.ToString();
				TxtSellPrice3.Text = getAllitems_Alternatives_Result.SellPriceSmall3.ToString();
				TxtProfit.Text = getAllitems_Alternatives_Result.Profit.ToString();
				TxtLocation.Text = getAllitems_Alternatives_Result.LocationSmall.ToString();
			}
			else
			{
				TxtQuntity.Text = FUNC_DecimalNumQty(0m).ToString();
				TxtAllQuantity.Text = FUNC_DecimalNumQty(0m).ToString();
				TxtAvergCost.Text = "0.00";
				TxtLastCost.Text = "0.00";
				TxtSellPrice1.Text = "0.00";
				TxtSellPrice2.Text = "0.00";
				TxtSellPrice3.Text = "0.00";
				TxtProfit.Text = "0.00";
				TxtLocation.Text = "";
			}
		}
		catch (Exception)
		{
		}
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("ItemID", typeof(int));
		datatable.Columns.Add("ItemUnitID", typeof(int));
		datatable.Columns.Add("TafqitId", typeof(int));
		datatable.Columns.Add("ItemNote", typeof(string));
		datatable.Columns.Add("كود_الصنف", typeof(string));
		datatable.Columns.Add("أسم الصنف", typeof(string));
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
		datatable.Columns.Add("الضريبه_بعد_الخصم", typeof(decimal));
		datatable.Columns.Add("إجمالى المبلغ", typeof(string));
		datatable.Columns.Add("حذف الصنف", typeof(int));
		datatable.Columns.Add("م", typeof(int));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تم", typeof(int));
		datatable.Columns.Add("قيمة الخصم", typeof(decimal));
		datatable.Columns.Add("البونص", typeof(decimal));
		datatable.Columns.Add("الطول", typeof(decimal));
		datatable.Columns.Add("العرض", typeof(decimal));
		datatable.Columns.Add("الإرتفاع", typeof(decimal));
		datatable.Columns.Add("الوزن", typeof(decimal));
		datatable.Columns.Add("البيان", typeof(string));
		datatable.Columns.Add("الربح", typeof(decimal));
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
		set.Columns.Add("ItemID", typeof(int));
		set.Columns.Add("ItemUnitID", typeof(int));
		set.Columns.Add("TafqitId", typeof(int));
		set.Columns.Add("ItemNote", typeof(string));
		set.Columns.Add("كود_الصنف", typeof(string));
		set.Columns.Add("أسم الصنف", typeof(string));
		set.Columns.Add("الوحدة", typeof(string));
		set.Columns.Add("رقم_الوحدة", typeof(int));
		set.Columns.Add("المخزن", typeof(string));
		set.Columns.Add("رقم المخزن", typeof(int));
		set.Columns.Add("الكمية", typeof(decimal));
		set.Columns.Add("وحدة التفقيط", typeof(string));
		set.Columns.Add("كمية التفقيط", typeof(decimal));
		set.Columns.Add("متوسط_التكلفة", typeof(decimal));
		set.Columns.Add("اجمالي_متوسط_التكلفة", typeof(decimal));
		set.Columns.Add("سعر الوحدة", typeof(decimal));
		set.Columns.Add("سعربعدالخصم", typeof(decimal));
		set.Columns.Add("نسبة الخصم", typeof(decimal));
		set.Columns.Add("إجمالى_بعدالخصم", typeof(decimal));
		set.Columns.Add("هل_ضريبة", typeof(int));
		set.Columns.Add("قيمة_الضريبة", typeof(decimal));
		set.Columns.Add("أسم_الوحدة", typeof(string));
		set.Columns.Add("الاجمالى", typeof(decimal));
		set.Columns.Add("الصافى", typeof(decimal));
		set.Columns.Add("الضربية", typeof(decimal));
		set.Columns.Add("الضريبه_بعد_الخصم", typeof(decimal));
		set.Columns.Add("إجمالى المبلغ", typeof(string));
		set.Columns.Add("حذف الصنف", typeof(int));
		set.Columns.Add("م", typeof(int));
		set.Columns.Add("مسح", typeof(int));
		set.Columns.Add("تم", typeof(int));
		set.Columns.Add("قيمة الخصم", typeof(decimal));
		set.Columns.Add("البونص", typeof(decimal));
		set.Columns.Add("الطول", typeof(decimal));
		set.Columns.Add("العرض", typeof(decimal));
		set.Columns.Add("الإرتفاع", typeof(decimal));
		set.Columns.Add("الوزن", typeof(decimal));
		set.Columns.Add("البيان", typeof(string));
		set.Columns.Add("الربح", typeof(decimal));
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["ItemID"] = "0";
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["رقم_الوحدة"] = 1;
		dataRow["أسم_الوحدة"] = "الصغرى";
		dataRow["الكمية"] = FUNC_DecimalNumQty(0m);
		dataRow["متوسط_التكلفة"] = 0.0;
		dataRow["اجمالي_متوسط_التكلفة"] = 0.0;
		dataRow["سعر الوحدة"] = 0.0;
		dataRow["سعربعدالخصم"] = 0.0;
		dataRow["نسبة الخصم"] = 0.0;
		dataRow["إجمالى_بعدالخصم"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
		dataRow["الضربية"] = 0.0;
		dataRow["الضريبه_بعد_الخصم"] = 0.0;
		dataRow["إجمالى المبلغ"] = 0.0;
		dataRow["الصافى"] = 0.0;
		dataRow["هل_ضريبة"] = 0;
		dataRow["قيمة_الضريبة"] = 0.0;
		dataRow["المخزن"] = Comp_Store.Text;
		dataRow["ItemNote"] = "";
		dataRow["مسح"] = 0;
		dataRow["تم"] = 0;
		dataRow["البونص"] = 0;
		dataRow["الطول"] = 1;
		dataRow["العرض"] = 1;
		dataRow["الإرتفاع"] = 1;
		dataRow["الوزن"] = 1;
		dataRow["قيمة الخصم"] = 0;
		dataRow["الربح"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridControl1.ProcessGridKey += gridControl1_ProcessGridKey;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		gridView1.CustomDrawFooterCell += gridView1_CustomDrawFooterCell;
		gridView1.Columns["متوسط_التكلفة"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.###}", "0")
		});
		gridView1.Columns["اجمالي_متوسط_التكلفة"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.###}", "0")
		});
		gridView1.Columns["الضريبه_بعد_الخصم"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.###}", "0")
		});
		gridView1.Columns["إجمالى_بعدالخصم"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.###}", "0")
		});
		gridView1.Columns["الضربية"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.###}", "0")
		});
		gridView1.Columns["إجمالى المبلغ"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.###}", "0")
		});
		gridView1.Columns["الاجمالى"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.###}", "0")
		});
		GetTotalCost();
	}

	public decimal IsItemPriceZero()
	{
		decimal num = 1m;
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["كود_الصنف"]);
			if (!(text != ""))
			{
				continue;
			}
			int num2 = 0;
			num2 = Convert.ToInt32(row["ItemID"]);
			int itmIDType = ORD.GetItmIDType(num2);
			if (itmIDType != 2)
			{
				string value = Convert.ToString(row["سعر الوحدة"]);
				string text2 = Convert.ToString(row["أسم الصنف"]);
				if (string.IsNullOrWhiteSpace(value))
				{
					row["سعر الوحدة"] = "0";
				}
				if (LoginDetails.IsQuantityMinZero)
				{
					num = 1m;
					break;
				}
				decimal num3 = Convert.ToDecimal(row["الكمية"]);
				string type = Convert.ToString(row["أسم_الوحدة"]);
				int storeID = Convert.ToInt32(row["رقم المخزن"]);
				int itemID = Convert.ToInt32(ORD.GetItmID(text));
				num = ORD.GetQuantityPermission(itemID, storeID, type);
				if (num <= 0m)
				{
					MessageBox.Show("نعتذر، لا يوجد كمية كافية من الصنف ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					break;
				}
				if (num3 > num)
				{
					MessageBox.Show("نعتذر، الكمية المطلوبة اكبر من الكمية الموجودة  ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					break;
				}
			}
		}
		return num;
	}

	public decimal IsItemPriceZeroUpdate()
	{
		decimal num = 1m;
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["كود_الصنف"]);
			string value = Convert.ToString(row["سعر الوحدة"]);
			string text2 = Convert.ToString(row["أسم الصنف"]);
			if (string.IsNullOrWhiteSpace(value))
			{
				row["سعر الوحدة"] = "0";
			}
			if (Convert.ToDecimal(row["سعر الوحدة"]) <= 0m && text != "")
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ الفاتورة لوجود اصناف سعر الوحدة تساوي صفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				break;
			}
			if (text != "" && !LoginDetails.IsQuantityMinZero)
			{
				decimal num2 = Convert.ToDecimal(row["الكمية"]);
				string type = Convert.ToString(row["أسم_الوحدة"]);
				int storeID = Convert.ToInt32(row["رقم المخزن"]);
				int itemID = Convert.ToInt32(ORD.GetItmID(text));
				num = ORD.GetQuantityPermission(itemID, storeID, type) + ORD.GetQuantityPermissionReturn(itemID, Convert.ToInt32(TxtCode.Text));
				decimal quantityPermissionReturn = ORD.GetQuantityPermissionReturn(itemID, Convert.ToInt32(TxtCode.Text));
				if (num <= 0m)
				{
					MessageBox.Show("نعتذر، لا يوجد كمية كافية من الصنف ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					num = 0m;
					break;
				}
				if (num2 > num)
				{
					MessageBox.Show("نعتذر، الكمية المطلوبة اكبر من الكمية الموجودة  ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					num = 0m;
					break;
				}
			}
		}
		return num;
	}

	public void FiterDatatableItems()
	{
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["كود_الصنف"]);
			if (text != "")
			{
				DataRow[] array = dtCurrentTable.Select("كود_الصنف = '" + text + "'");
				if (array.Length == 0)
				{
					dtCurrentTable.Rows.Add(row.ItemArray);
				}
			}
			gridControl1.DataSource = dtCurrentTable;
			GetTotalCost();
		}
		datatable = dtCurrentTable;
	}

	public void FiterDatatableItemsUpdate()
	{
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["كود_الصنف"]);
			if (text == "")
			{
				row.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
		gridControl1.DataSource = datatable;
	}

	private void Cal_FinalResultTotals()
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
				TxtTafqit.Text = "";
				TxtTotalAll.Text = "0.00";
				TxtLocation.Text = "";
				TxtTafqit.Text = "";
				TxtSellPrice1.Text = "0.00";
				TxtSellPrice2.Text = "0.00";
				TxtSellPrice3.Text = "0.00";
				TxtProfit.Text = "0.00";
				TxtQuntity.Text = FUNC_DecimalNumQty(0m).ToString();
				TxtAllQuantity.Text = FUNC_DecimalNumQty(0m).ToString();
				TxtAvergCost.Text = "0.00";
				TxtLastCost.Text = "0.00";
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
						row["الضريبه_بعد_الخصم"] = row["الضربية"];
						if (((Convert.ToDecimal(row["قيمة الخصم"]) == 0m || row["قيمة الخصم"].ToString() == "") && Convert.ToDecimal(row["نسبة الخصم"]) == 0m) || row["نسبة الخصم"].ToString() == "")
						{
							row["إجمالى_بعدالخصم"] = row["الاجمالى"];
						}
					}
				}
				decimal num = Convert.ToDecimal(Convert.ToDecimal(datatable.Compute("SUM(الصافى)", "")));
				TxtTotals.Text = Convert.ToString(num);
				decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
				TxtVat.Text = Convert.ToString(num2);
				decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
				TxtTotalBeforDisc.Text = Convert.ToString(num3);
				decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعد_الخصم)", ""));
				TxtVatBeforeDisc.Text = Convert.ToString(num4);
				TxtAllVat.Text = TxtVatBeforeDisc.Text;
				decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
				TxtCost.Text = Convert.ToString(num5);
				TxtTotalAll.Text = Math.Round(num, DecimalNum).ToString();
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
				TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum)).ToString();
				return;
			}
			decimal num6 = Convert.ToDecimal(TxtDiscount.Text) * 100m / Convert.ToDecimal(TxtTotals.Text);
			foreach (DataRow row2 in datatable.Rows)
			{
				if (row2["أسم_الوحدة"].ToString() != "")
				{
					decimal num7 = Math.Round((Convert.ToDecimal(row2["الاجمالى"].ToString()) - Convert.ToDecimal(row2["قيمة الخصم"].ToString())) / Convert.ToDecimal(TxtTotalAll.Text) * Convert.ToDecimal(TxtDiscount.Text), DecimalNum);
					row2["سعربعدالخصم"] = Convert.ToDecimal(row2["سعر الوحدة"].ToString()) - Convert.ToDecimal(num6 * Convert.ToDecimal(row2["سعر الوحدة"].ToString()) / 100m);
					decimal num8 = Convert.ToDecimal(row2["سعربعدالخصم"].ToString());
					decimal num9 = Convert.ToDecimal(row2["قيمة الخصم"]);
					decimal num10 = Math.Round(Convert.ToDecimal(row2["سعر الوحدة"].ToString()) * (Convert.ToDecimal(row2["الكمية"]) * (Convert.ToDecimal(row2["الطول"]) * Convert.ToDecimal(row2["العرض"]) * Convert.ToDecimal(row2["الإرتفاع"]))) - Convert.ToDecimal(row2["قيمة الخصم"].ToString()) - num7, DecimalNum);
					row2["إجمالى_بعدالخصم"] = num10;
					row2["الضريبه_بعد_الخصم"] = Convert.ToDecimal(Convert.ToDecimal(row2["إجمالى_بعدالخصم"]) * Convert.ToDecimal(row2["قيمة_الضريبة"])) / 100m;
				}
			}
			decimal num11 = Convert.ToDecimal(datatable.Compute("SUM(الصافى)", ""));
			TxtTotals.Text = Convert.ToString(num11);
			decimal num12 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
			TxtVat.Text = Convert.ToString(num12);
			decimal num13 = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
			TxtTotalBeforDisc.Text = Convert.ToString(num13);
			decimal num14 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعد_الخصم)", ""));
			TxtVatBeforeDisc.Text = Convert.ToString(num14);
			TxtAllVat.Text = TxtVatBeforeDisc.Text;
			decimal num15 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
			TxtCost.Text = Convert.ToString(num15);
			TxtTotalAll.Text = Math.Round(num11, DecimalNum).ToString();
			TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
			TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum)).ToString();
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
		}
	}

	private void SetKeyboardLayout(InputLanguage layout)
	{
		InputLanguage.CurrentInputLanguage = layout;
	}

	private void Clear()
	{
		TxtSales.Text = "";
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtCode.Text = "";
		datatable.Rows.Clear();
		dtCurrentTable.Rows.Clear();
		gridControl1.DataSource = datatable;
		GetTotalCost();
		TxtSuppliers.Text = "";
		TxtNumVat.Text = "";
		TxtCity.Text = "";
		TxtStreet.Text = "";
		TxtDistrict.Text = "";
		TxtAdditionalNum.Text = "";
		TxtCommercialRecord.Text = "";
		TxtPostalCode.Text = "";
		TxtType.Text = "";
		TxtNote.Text = "";
		TxtDiscount.Text = "";
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtType.Text = "نقدى";
		TxtSuppliers.Text = "عميل نقدى";
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtPhone.Text = "";
		TxtNumNote.Text = "";
		TxtTotalBeforDisc.Text = "0.00";
		TxtVatBeforeDisc.Text = "0.00";
		TxtTafqit.Text = "";
		TxtTotalAll.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtNet22.Text = "0.00";
		TxtLocation.Text = "";
		TxtSellPrice1.Text = "0.00";
		TxtSellPrice2.Text = "0.00";
		TxtSellPrice3.Text = "0.00";
		TxtProfit.Text = "0.00";
		TxtQuntity.Text = "0.00";
		TxtAllQuantity.Text = FUNC_DecimalNumQty(0m).ToString();
		TxtAvergCost.Text = FUNC_DecimalNumQty(0m).ToString();
		TxtLastCost.Text = "0.00";
		TxtCharge.Text = "0.00";
		TxtDate.DateTime = DateTime.Now;
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
		TxtDiscount.Enabled = true;
		TxtNote.Enabled = true;
		TxtType.Enabled = true;
		TxtDate.Enabled = true;
		toggleHold.IsOn = false;
		BtnCustomerSearch.Focus();
		Comp_Store.SelectedValue = LoginDetails.StoreID;
		TxtNumberOfMen.Clear();
		TxtCountTableMen.Clear();
		TxtCountCaress.Clear();
		TxtCookCarecass.Clear();
		TxtTypeCaress.Clear();
		TxtPriceCaress.Clear();
		TxtTrombah.Clear();
		TxtBff.Clear();
		TxtCountWomen.Clear();
		TxtCountWomenTable.Clear();
		TxtCountCandles.Clear();
		TxtCountHeaters.Clear();
		TxtKosha.Clear();
		TxtAddHeadPhones.Clear();
		TxtWedding.Clear();
		TxtCurains.Clear();
		TxtAddCappuccino.Clear();
		TxtLesar.Clear();
		TxtScreenAdd.Clear();
		TxtTarab.Clear();
		TxtAnotherGroom.Clear();
	}

	private void NewFrm()
	{
		datatable.Rows.Clear();
		dtCurrentTable.Rows.Clear();
		gridControl1.Enabled = true;
		gridView1.OptionsBehavior.Editable = true;
		Clear();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["ItemID"] = "0";
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["الكمية"] = FUNC_DecimalNumQty(0m);
		dataRow["سعر الوحدة"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
		dataRow["الضربية"] = 0.0;
		dataRow["إجمالى المبلغ"] = 0.0;
		dataRow["البونص"] = 0;
		dataRow["الطول"] = 1;
		dataRow["العرض"] = 1;
		dataRow["الإرتفاع"] = 1;
		dataRow["الوزن"] = 1;
		dataRow["قيمة الخصم"] = 0;
		dataRow["نسبة الخصم"] = 0;
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		IsEnableTrue();
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtSuppliers.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 12))
		{
			int purchese_ID = 0;
			bool isOn = toggleHold.IsOn;
			if (datatable.Rows.Count > 1)
			{
				if (TxtSuppliers.Text != "")
				{
					if (TxtType.Text != "")
					{
						if (Convert.ToInt32(TxtType.SelectedValue) == 2 && string.IsNullOrWhiteSpace(LblSup.Text))
						{
							MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع حساب عميل نقدى", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							TxtSuppliers.Focus();
							return;
						}
						decimal num = IsItemPriceZero();
						if (num == 0m || PermissionSellPrice() == 0)
						{
							return;
						}
						if (Convert.ToInt32(TxtType.SelectedValue) == 1)
						{
							LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet22.Text);
							FrmMoney frmMoney = new FrmMoney();
							frmMoney.ShowDialog();
							if (!frmMoney.IsComplateMony)
							{
								return;
							}
							FrmCash = Convert.ToDecimal(frmMoney.TxtCash.Text);
							FrmBank = Convert.ToDecimal(frmMoney.TxtVisa.Text);
						}
						else
						{
							LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet22.Text);
							FrmMoneyAjil frmMoneyAjil = new FrmMoneyAjil();
							frmMoneyAjil.ShowDialog();
							if (!frmMoneyAjil.IsComplateMony)
							{
								return;
							}
							decimal value = ((!(Convert.ToDecimal(TxtCharge.Text) < 0m)) ? Convert.ToDecimal(TxtCharge.Text) : (Convert.ToDecimal(TxtCharge.Text) * -1m));
							decimal num2 = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
							decimal? num3 = CreditLimit + (decimal?)value;
							if (!((num2 <= num3.GetValueOrDefault()) & num3.HasValue))
							{
								num3 = CreditLimit;
								if (!((num3.GetValueOrDefault() == 0m) & num3.HasValue))
								{
									MessageBox.Show("عفوا\u064b، رصيد العميل لا يكفي \r\n  الحد الائتماني = " + FUNC_DecimalNum(CreditLimit), "مستكشف البيانات", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									return;
								}
							}
							FrmCash = Convert.ToDecimal(frmMoneyAjil.TxtCash.Text);
							FrmBank = Convert.ToDecimal(frmMoneyAjil.TxtVisa.Text);
							Safy = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
							TxtAllTotal.Clear();
							TxtAllTotal.Text = Safy.ToString();
							ChargeAfterSale = Convert.ToDecimal(TxtCharge.Text) + FrmCash + FrmBank + Convert.ToDecimal(TxtNet22.Text);
						}
						if (CBCostCenter.SelectedValue == null)
						{
							CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
						}
						FiterDatatableItemsUpdate();
						FilterDataTableSave(datatable, purchese_ID, isOn);
						LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
						LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum);
						purchese_ID = ORD.SaveOrders(Convert.ToInt32(LblSup.Text), isOn, TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, DateTime.Parse(TxtDate.Text), Convert.ToInt32(TxtType.SelectedValue), "", 1, Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBBounce.SelectedValue), TxtNote.Text, TxtNumNote.Text, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtTotals.Text), Convert.ToDecimal(TxtAllTotal.Text), Convert.ToDecimal(TxtDiscount.Text), Convert.ToDecimal(txtdicoutprege.Text), Convert.ToDecimal(TxtVatBeforeDisc.Text), Convert.ToDecimal(TxtTotalBeforDisc.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtAllVat.Text), Convert.ToDecimal(TxtNet22.Text), FrmCash, FrmBank, OrderCashierType: false, 0, 0, dtCurrentTable, 1, TxtSales.Text, DateTime.Parse(DateHold.Text), "", TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, Convert.ToDecimal(TxtHasmPer.Text), Convert.ToDecimal(TxtHasmAmount.Text), ChargeAfterSale, 1, 0m, 0m, "", 0m, 0m, "", "", "", "", "", "");
						gridControl1.DataSource = datatable;
						gridView1.OptionsBehavior.Editable = false;
						if (purchese_ID > 0)
						{
							BtnSave.Enabled = false;
							BtnEdit.Enabled = true;
							BtnDelete.Enabled = true;
							IsEnableFalse();
							TxtCode.Text = purchese_ID.ToString();
							SaveMore(purchese_ID);
							switch (MessageBox.Show("تم حفظ بيانات فاتورة المبيعات بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
							{
							case DialogResult.Yes:
								Print();
								break;
							}
							Charge(Convert.ToInt32(LblSup.Text));
						}
						else
						{
							ChargeAfterSale = 0m;
							MessageBox.Show("تم حفظ بيانات فاتورة المبيعات رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 12))
		{
			if (TxtDate.ReadOnly)
			{
				IsEnableTrue();
				BtnEdit.Text = "F5 حفظ";
				insertnewrow();
				gridView1.OptionsBehavior.Editable = true;
				return;
			}
			bool isOn = toggleHold.IsOn;
			int purchese_ID = 0;
			if (!(TxtSuppliers.Text != ""))
			{
				return;
			}
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
			if (Convert.ToInt32(TxtType.SelectedValue) == 2 && TxtSuppliers.Text == "عميل نقدى")
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع مورد نقدى", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtSuppliers.Focus();
			}
			else if (TxtCode.Text != "")
			{
				int PurId = Convert.ToInt32(TxtCode.Text);
				Order_OrderReturn order_OrderReturn = gTSdbEntities.Order_OrderReturn.Where((Order_OrderReturn x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
				Order_CheckoutOrders order_CheckoutOrders = gTSdbEntities.Order_CheckoutOrders.Where((Order_CheckoutOrders x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
				if (order_OrderReturn == null && order_CheckoutOrders == null)
				{
					if (datatable.Rows.Count <= 1)
					{
						return;
					}
					decimal num = IsItemPriceZeroUpdate();
					if (num == 0m)
					{
						return;
					}
					GetTotalCost();
					ArrangeDatatable();
					Cal_FinalResultTotals();
					if (PermissionSellPrice() == 0)
					{
						return;
					}
					if (Convert.ToInt32(TxtType.SelectedValue) == 1)
					{
						LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet22.Text);
						FrmMoney frmMoney = new FrmMoney();
						frmMoney.ShowDialog();
						if (!frmMoney.IsComplateMony)
						{
							return;
						}
						FrmCash = Convert.ToDecimal(frmMoney.TxtCash.Text);
						FrmBank = Convert.ToDecimal(frmMoney.TxtVisa.Text);
						TxtAllTotal.Clear();
						ChargeAfterSale = Convert.ToDecimal(TxtCharge.Text) + FrmCash + FrmBank + Convert.ToDecimal(TxtNet22.Text);
					}
					else
					{
						LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet22.Text);
						FrmMoneyAjil frmMoneyAjil = new FrmMoneyAjil();
						frmMoneyAjil.ShowDialog();
						if (!frmMoneyAjil.IsComplateMony)
						{
							return;
						}
						FrmCash = Convert.ToDecimal(frmMoneyAjil.TxtCash.Text);
						FrmBank = Convert.ToDecimal(frmMoneyAjil.TxtVisa.Text);
						Safy = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
						TxtAllTotal.Clear();
						TxtAllTotal.Text = Safy.ToString();
						ChargeAfterSale = Convert.ToDecimal(TxtCharge.Text) + FrmCash + FrmBank + Convert.ToDecimal(TxtNet22.Text);
					}
					FiterDatatableItemsUpdate();
					if (CBCostCenter.SelectedValue == null)
					{
						CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
					}
					LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
					LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum);
					FilterDataTableSave(datatable, purchese_ID, isOn);
					purchese_ID = ORD.UpdateOrders(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), isOn, TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, DateTime.Parse(TxtDate.Text), Convert.ToInt32(TxtType.SelectedValue), "", 1, Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBBounce.SelectedValue), TxtNote.Text, TxtNumNote.Text, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtTotals.Text), Convert.ToDecimal(TxtAllTotal.Text), Convert.ToDecimal(TxtDiscount.Text), Convert.ToDecimal(txtdicoutprege.Text), Convert.ToDecimal(TxtVatBeforeDisc.Text), Convert.ToDecimal(TxtTotalBeforDisc.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtAllVat.Text), Convert.ToDecimal(TxtNet22.Text), FrmCash, FrmBank, OrderCashierType: false, 0, 0, dtCurrentTable, TxtSales.Text, DateTime.Parse(DateHold.Text), "", "", "", "", "", TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, Convert.ToDecimal(TxtHasmPer.Text), Convert.ToDecimal(TxtHasmAmount.Text), 0m, 0m, 0m, "", "", "", "", "", "", 1);
					if (purchese_ID > 0)
					{
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						IsEnableFalse();
						BtnEdit.Text = "تعديل F6";
						gridView1.OptionsBehavior.Editable = false;
						SaveMore(Convert.ToInt32(TxtCode.Text));
						switch (MessageBox.Show("تم تعديل فاتورة المبيعات بنجاح \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
						{
						case DialogResult.Yes:
							Print();
							break;
						}
						Charge(Convert.ToInt32(LblSup.Text));
					}
					else
					{
						ChargeAfterSale = 0m;
						MessageBox.Show("تم حفظ بيانات فاتورة المبيعات رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else
				{
					MessageBox.Show("عفوا\u064b لا يمكن التعديل على فاتورة المبيعات ليها حركات مرتجعات او صرف ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
			return;
		}
		MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 12))
		{
			using (GTSdbEntities gTSdbEntities = new GTSdbEntities())
			{
				int num = 0;
				if (TxtCode.Text != "")
				{
					int PurId = Convert.ToInt32(TxtCode.Text);
					Order_OrderReturn order_OrderReturn = gTSdbEntities.Order_OrderReturn.Where((Order_OrderReturn x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
					Order_CheckoutOrders order_CheckoutOrders = gTSdbEntities.Order_CheckoutOrders.Where((Order_CheckoutOrders x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
					if (order_OrderReturn == null && order_CheckoutOrders == null)
					{
						switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة المبيعات رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
						{
						case DialogResult.Yes:
							num = ORD.DeleteOrders(Convert.ToInt32(TxtCode.Text));
							if (num > 0)
							{
								DeleteMore(Convert.ToInt32(TxtCode.Text));
								MessageBox.Show("تم حذف بيانات فاتورة المبيعات رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								Clear();
								gridView1.OptionsBehavior.Editable = true;
								IsEnableTrue();
								BtnDelete.Enabled = false;
								BtnEdit.Enabled = false;
								BtnSave.Enabled = true;
								insertnewrow();
								TxtSuppliers.Focus();
							}
							else
							{
								MessageBox.Show("لا يمكن حذف بيانات فاتورة المبيعات رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							}
							break;
						}
					}
					else
					{
						MessageBox.Show("عفوا\u064b لا يمكن التعديل على فاتورة المبيعات ليها حركات مرتجعات او صرف ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else
				{
					MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة المبيعات بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
				return;
			}
		}
		MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
	}

	private void SaveMore(int code)
	{
		if (string.IsNullOrEmpty(TxtPriceCaress.Text))
		{
			TxtPriceCaress.Text = "0";
		}
		Class_More.InsertUpdate(code, TxtNumberOfMen.Text, TxtCountTableMen.Text, TxtCountCaress.Text, TxtCookCarecass.Text, TxtTypeCaress.Text, Convert.ToDecimal(TxtPriceCaress.Text), TxtTrombah.Text, TxtBff.Text, TxtCountWomen.Text, TxtCountWomenTable.Text, TxtCountCandles.Text, TxtCountHeaters.Text, TxtKosha.Text, TxtAddHeadPhones.Text, TxtWedding.Text, TxtCurains.Text, TxtAddCappuccino.Text, TxtLesar.Text, TxtScreenAdd.Text, TxtTarab.Text, TxtAnotherGroom.Text);
	}

	private void DeleteMore(int code)
	{
		Class_More.Delete(code);
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_Orders", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
				sqlConnection.Close();
				if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
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
					stiReport["Type"] = TxtType.Text;
					stiReport["Date"] = TxtDate.DateTime;
					stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
					stiReport["CustName"] = TxtSuppliers.Text;
					stiReport["CustVatNum"] = TxtNumVat.Text;
					stiReport["Custphone"] = TxtPhone.Text;
					stiReport["Tafqit"] = txtWordAr;
					Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
					string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), (LoginDetails.NetCash + LoginDetails.VatCash).ToString(), LoginDetails.VatCash.ToString());
					stiReport["EncHash"] = value;
					stiReport.Render();
					stiReport.Show(dialogForm: true);
					if (LoginDetails.OrderStore)
					{
						PrintOrderStore();
					}
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

	private void PrintOrderStore()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_Orders", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
				sqlConnection.Close();
				if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					if (LoginDetails.Lang == "ar-EG")
					{
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintOrdersFromStore.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintOrdersFromStore.mrt");
					}
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
					stiReport["Type"] = TxtType.Text;
					stiReport["Date"] = TxtDate.DateTime;
					stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
					stiReport["CustName"] = TxtSuppliers.Text;
					stiReport["CustVatNum"] = TxtNumVat.Text;
					stiReport["Custphone"] = TxtPhone.Text;
					stiReport["Tafqit"] = txtWordAr;
					Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
					string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), TxtNet.Text, TxtVatBeforeDisc.Text);
					stiReport["EncHash"] = value;
					stiReport.Render();
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

	public void IsEnableTrue()
	{
		TxtDiscount.ReadOnly = false;
		TxtDiscountALLTotal.ReadOnly = false;
		TxtNote.ReadOnly = false;
		TxtType.Enabled = true;
		TxtType.Enabled = true;
		TxtDate.ReadOnly = false;
		TxtSuppliers.ReadOnly = false;
		TxtNumVat.ReadOnly = false;
		TxtPostalCode.ReadOnly = false;
		TxtSales.ReadOnly = false;
		TxtAdditionalNum.ReadOnly = false;
		TxtCity.ReadOnly = false;
		TxtStreet.ReadOnly = false;
		TxtDistrict.ReadOnly = false;
		TxtCommercialRecord.ReadOnly = false;
		TxtPhone.ReadOnly = false;
		TxtNumNote.ReadOnly = false;
		CBCostCenter.Enabled = true;
		toggleHold.ReadOnly = false;
		BtnEdit.Text = "تعديل F6";
		TxtNumberOfMen.ReadOnly = false;
		TxtCountTableMen.ReadOnly = false;
		TxtCountCaress.ReadOnly = false;
		TxtCookCarecass.ReadOnly = false;
		TxtTypeCaress.ReadOnly = false;
		TxtPriceCaress.ReadOnly = false;
		TxtTrombah.ReadOnly = false;
		TxtBff.ReadOnly = false;
		TxtCountWomen.ReadOnly = false;
		TxtCountWomenTable.ReadOnly = false;
		TxtCountCandles.ReadOnly = false;
		TxtCountHeaters.ReadOnly = false;
		TxtKosha.ReadOnly = false;
		TxtAddHeadPhones.ReadOnly = false;
		TxtWedding.ReadOnly = false;
		TxtCurains.ReadOnly = false;
		TxtAddCappuccino.ReadOnly = false;
		TxtLesar.ReadOnly = false;
		TxtScreenAdd.ReadOnly = false;
		TxtTarab.ReadOnly = false;
		TxtAnotherGroom.ReadOnly = false;
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = true;
		TxtDiscountALLTotal.ReadOnly = true;
		TxtNote.ReadOnly = true;
		TxtType.Enabled = false;
		TxtDate.ReadOnly = true;
		TxtSuppliers.ReadOnly = true;
		TxtNumVat.ReadOnly = true;
		TxtPhone.ReadOnly = true;
		TxtNumNote.ReadOnly = true;
		CBCostCenter.Enabled = false;
		toggleHold.ReadOnly = true;
		TxtPostalCode.ReadOnly = false;
		TxtSales.ReadOnly = false;
		TxtAdditionalNum.ReadOnly = false;
		TxtCity.ReadOnly = false;
		TxtStreet.ReadOnly = false;
		TxtDistrict.ReadOnly = false;
		TxtCommercialRecord.ReadOnly = false;
		TxtDiscount.BackColor = Color.White;
		TxtNote.BackColor = Color.White;
		TxtType.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
		TxtSuppliers.BackColor = Color.White;
		TxtNumVat.BackColor = Color.White;
		TxtPostalCode.BackColor = Color.White;
		TxtSales.BackColor = Color.White;
		TxtAdditionalNum.BackColor = Color.White;
		TxtCity.BackColor = Color.White;
		TxtStreet.BackColor = Color.White;
		TxtDistrict.BackColor = Color.White;
		TxtCommercialRecord.BackColor = Color.White;
		TxtPhone.BackColor = Color.White;
		TxtNumNote.BackColor = Color.White;
		TxtDiscountALLTotal.BackColor = Color.White;
		TxtNumberOfMen.ReadOnly = true;
		TxtCountTableMen.ReadOnly = true;
		TxtCountCaress.ReadOnly = true;
		TxtCookCarecass.ReadOnly = true;
		TxtTypeCaress.ReadOnly = true;
		TxtPriceCaress.ReadOnly = true;
		TxtTrombah.ReadOnly = true;
		TxtBff.ReadOnly = true;
		TxtCountWomen.ReadOnly = true;
		TxtCountWomenTable.ReadOnly = true;
		TxtCountCandles.ReadOnly = true;
		TxtCountHeaters.ReadOnly = true;
		TxtKosha.ReadOnly = true;
		TxtAddHeadPhones.ReadOnly = true;
		TxtWedding.ReadOnly = true;
		TxtCurains.ReadOnly = true;
		TxtAddCappuccino.ReadOnly = true;
		TxtLesar.ReadOnly = true;
		TxtScreenAdd.ReadOnly = true;
		TxtTarab.ReadOnly = true;
		TxtAnotherGroom.ReadOnly = true;
		TxtNumberOfMen.BackColor = Color.White;
		TxtCountTableMen.BackColor = Color.White;
		TxtCountCaress.BackColor = Color.White;
		TxtCookCarecass.BackColor = Color.White;
		TxtTypeCaress.BackColor = Color.White;
		TxtPriceCaress.BackColor = Color.White;
		TxtTrombah.BackColor = Color.White;
		TxtBff.BackColor = Color.White;
		TxtCountWomen.BackColor = Color.White;
		TxtCountWomenTable.BackColor = Color.White;
		TxtCountCandles.BackColor = Color.White;
		TxtCountHeaters.BackColor = Color.White;
		TxtKosha.BackColor = Color.White;
		TxtAddHeadPhones.BackColor = Color.White;
		TxtWedding.BackColor = Color.White;
		TxtCurains.BackColor = Color.White;
		TxtAddCappuccino.BackColor = Color.White;
		TxtLesar.BackColor = Color.White;
		TxtScreenAdd.BackColor = Color.White;
		TxtTarab.BackColor = Color.White;
		TxtAnotherGroom.BackColor = Color.White;
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
					string code = item.ToString();
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(code).FirstOrDefault();
					if (get_Item_ByCodeALL_Result != null)
					{
						DataRow dataRow = null;
						dataRow = datatable.NewRow();
						dataRow["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
						dataRow["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
						dataRow["كود_الصنف"] = get_Item_ByCodeALL_Result.Item_code;
						dataRow["أسم الصنف"] = get_Item_ByCodeALL_Result.item_Name;
						dataRow["الوحدة"] = get_Item_ByCodeALL_Result.UnitSmall;
						dataRow["رقم_الوحدة"] = get_Item_ByCodeALL_Result.UnitSmallID;
						dataRow["البونص"] = 0;
						dataRow["الطول"] = 1;
						dataRow["العرض"] = 1;
						dataRow["الإرتفاع"] = 1;
						dataRow["الوزن"] = 1;
						dataRow["قيمة الخصم"] = 0;
						dataRow["نسبة الخصم"] = 0;
						dataRow["الكمية"] = FUNC_DecimalNumQty(1);
						dataRow["متوسط_التكلفة"] = get_Item_ByCodeALL_Result.Average_cost;
						dataRow["اجمالي_متوسط_التكلفة"] = get_Item_ByCodeALL_Result.Average_cost;
						dataRow["المخزن"] = Comp_Store.Text;
						dataRow["رقم المخزن"] = Comp_Store.SelectedValue;
						dataRow["أسم_الوحدة"] = "الصغرى";
						if (toggleSectoral.IsOn)
						{
							dataRow["سعر الوحدة"] = get_Item_ByCodeALL_Result.SellPriceSmall;
							dataRow["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
							Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, get_Item_ByCodeALL_Result.SellPriceSmall, 1m, dataRow, null);
						}
						else
						{
							dataRow["سعر الوحدة"] = get_Item_ByCodeALL_Result.SellPriceSmall2;
							dataRow["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
							Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, get_Item_ByCodeALL_Result.SellPriceSmall2, 1m, dataRow, null);
						}
						if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
						{
							dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitSmallID);
							dataRow["كمية التفقيط"] = FUNC_DecimalNum(dataRow["الكمية"].ToString());
							dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
						}
						if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
						{
							dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitMediumID);
							DataRow dataRow2 = dataRow;
							decimal value = 1;
							decimal? convertMediumUnit = get_Item_ByCodeALL_Result.ConvertMediumUnit;
							dataRow2["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
							dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
						}
						if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
						{
							dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitLargeID);
							DataRow dataRow3 = dataRow;
							decimal value = 1;
							decimal? convertMediumUnit = get_Item_ByCodeALL_Result.ConvertBigUnit;
							dataRow3["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
							dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
						}
						object obj = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
						int num = Convert.ToInt32(obj.ToString());
						dataRow["م"] = num + 1;
						datatable.Rows.Add(dataRow);
						StockLimit(get_Item_ByCodeALL_Result.ItemId, Convert.ToInt32(Comp_Store.SelectedValue));
					}
				}
				CheckFfistRow();
				insertnewrow();
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
		}
		catch (Exception)
		{
		}
	}

	public void GetPricePffer()
	{
		try
		{
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
			FrmSearchPriceOffer frmSearchPriceOffer = new FrmSearchPriceOffer();
			frmSearchPriceOffer.ShowDialog();
			if (frmSearchPriceOffer.CustCode > 0)
			{
				int custCode = frmSearchPriceOffer.CustCode;
				Order_PriceOffer order_PriceOffer = PRCOffer.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<Order_PriceOfferDetails> itemsPurchesByID = PRCOffer.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (Order_PriceOfferDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = PRCOffer.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = item_Items.ItemId;
					dataRow["ItemUnitID"] = item_Items.UnitSmall;
					dataRow["كود_الصنف"] = item_Items.Item_code;
					dataRow["أسم الصنف"] = item_Items.item_Name;
					dataRow["البونص"] = 0;
					dataRow["الطول"] = 1;
					dataRow["العرض"] = 1;
					dataRow["الإرتفاع"] = 1;
					dataRow["الوزن"] = 1;
					dataRow["قيمة الخصم"] = 0;
					dataRow["نسبة الخصم"] = 0;
					dataRow["أسم_الوحدة"] = item.ItemUnitType;
					dataRow["رقم المخزن"] = item.StoreID;
					dataRow["المخزن"] = gTSdbEntities.Account_Stores.FirstOrDefault((Account_Stores x) => (int?)x.ID == item.StoreID).Store_Name;
					dataRow["الكمية"] = FUNC_DecimalNumQty(item.Quantity);
					dataRow["متوسط_التكلفة"] = ORD.GetAverageCostID(Convert.ToInt32(item.ItemID));
					dataRow["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["متوسط_التكلفة"]));
					dataRow["رقم_الوحدة"] = item.ItemUnitID;
					dataRow["وحدة التفقيط"] = ITM.GetUnitName(item_Items.UnitTafqitID);
					dataRow["كمية التفقيط"] = 0;
					dataRow["TafqitId"] = item_Items.UnitTafqitID;
					dataRow["الاجمالى"] = FUNC_DecimalNum(item.TotalPrice);
					dataRow["سعر الوحدة"] = FUNC_DecimalNum(item.UnitPrice);
					if (item.ItemUnitType == "الصغرى")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
						Vat(item_Items.Is_Tax, item_Items.Tax_Value, item_Items.SellPriceSmall, Convert.ToDecimal(item.Quantity), dataRow, "عرض سعر");
					}
					if (item.ItemUnitType == "المتوسطة")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
						Vat(item_Items.Is_Tax, item_Items.Tax_Value, item_Items.SellPriceMedium, Convert.ToDecimal(item.Quantity), dataRow, "عرض سعر");
					}
					if (item.ItemUnitType == "الكبرى")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
						Vat(item_Items.Is_Tax, item_Items.Tax_Value, item_Items.SellPriceMedium, Convert.ToDecimal(item.Quantity), dataRow, "عرض سعر");
					}
					dataRow["مسح"] = 0;
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["الضربية"] = FUNC_DecimalNum(item.VAT);
						dataRow["إجمالى المبلغ"] = FUNC_DecimalNum(item.TotalPrice + item.VAT);
						dataRow["هل_ضريبة"] = 1;
						dataRow["قيمة_الضريبة"] = FUNC_DecimalNum(item_Items.Tax_Value);
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
				TxtType.SelectedValue = order_PriceOffer.Sectoral;
				TxtCode.Text = "";
				LblSup.Text = order_PriceOffer.SupplierID.ToString();
				Charge(Convert.ToInt32(order_PriceOffer.SupplierID));
				SearchCustNum(Convert.ToInt32(order_PriceOffer.SupplierID));
				TxtSuppliers.Text = order_PriceOffer.SupplierName;
				TxtPhone.Text = order_PriceOffer.SupplierPhone;
				TxtNumVat.Text = order_PriceOffer.SupplierVatNum;
				TxtNote.Text = order_PriceOffer.Note;
				TxtNumNote.Text = order_PriceOffer.NoteNum;
				TxtTotals.Text = Convert.ToString(order_PriceOffer.TotalPrices);
				TxtVat.Text = Convert.ToString(order_PriceOffer.Tax);
				TxtTotalAll.Text = order_PriceOffer.Safy.ToString();
				TxtDiscount.Text = order_PriceOffer.DiscountNum.ToString();
				TxtTotalBeforDisc.Text = order_PriceOffer.Safy.ToString();
				TxtVatBeforeDisc.Text = order_PriceOffer.Tax_Discount.ToString();
				TxtNet.Text = order_PriceOffer.Net.ToString();
				TxtNet22.Text = order_PriceOffer.Net.ToString();
				TxtDate.DateTime = DateTime.Now;
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnDelete.Enabled = false;
				gridView1.OptionsBehavior.Editable = true;
				ArrangeDatatable();
				IsEnableTrue();
				insertnewrow();
				TxtSuppliers.Text = order_PriceOffer.SupplierName;
				GetTotalCost();
				Cal_FinalResultTotals();
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

	public void CopyOrders()
	{
		try
		{
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
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
					int itemId = Convert.ToInt32(item.ItemID);
					Get_Item_ByID_Result get_Item_ByID_Result = ITM.Get_Item_ByID(itemId).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = get_Item_ByID_Result.ItemId;
					dataRow["ItemUnitID"] = get_Item_ByID_Result.UnitSmallID;
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["رقم_الوحدة"] = item.ItemUnitID;
					dataRow["كود_الصنف"] = get_Item_ByID_Result.Item_code;
					dataRow["أسم الصنف"] = get_Item_ByID_Result.item_Name;
					dataRow["البونص"] = item.Bounce;
					dataRow["الطول"] = item.Long;
					dataRow["العرض"] = item.width;
					dataRow["الإرتفاع"] = item.Height;
					dataRow["الوزن"] = item.Weight;
					dataRow["قيمة الخصم"] = item.Amount_Discount;
					dataRow["نسبة الخصم"] = item.Per_Discount;
					decimal value = FUNC_DecimalNum((item.Quantity + item.Bounce) * (item.Long * item.width * item.Height));
					decimal num2 = FUNC_DecimalNum(item.Quantity * (item.Long * item.width * item.Height));
					if (item.ItemUnitType == "الصغرى")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "المتوسطة")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "الكبرى")
					{
						dataRow["الوحدة"] = gTSdbEntities.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					dataRow["أسم_الوحدة"] = item.ItemUnitType;
					dataRow["رقم المخزن"] = item.StoreID;
					dataRow["المخزن"] = (from x in gTSdbEntities.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["الكمية"] = FUNC_DecimalNumQty(item.Quantity);
					dataRow["متوسط_التكلفة"] = item.LastCost;
					dataRow["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(item.LastCost * (decimal?)value);
					dataRow["سعر الوحدة"] = item.UnitPrice;
					dataRow["الاجمالى"] = FUNC_DecimalNum(item.TotalPrice);
					dataRow["مسح"] = 0;
					Vat(get_Item_ByID_Result.Is_Tax, get_Item_ByID_Result.Tax_Value, get_Item_ByID_Result.SellPriceMedium, Convert.ToDecimal(dataRow["الكمية"]), dataRow, "");
					if (get_Item_ByID_Result.UnitTafqitID == 1)
					{
						dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByID_Result.UnitSmallID);
						dataRow["كمية التفقيط"] = FUNC_DecimalNum(dataRow["الكمية"].ToString());
						dataRow["TafqitId"] = get_Item_ByID_Result.UnitSmallID;
					}
					if (get_Item_ByID_Result.UnitTafqitID == 2)
					{
						dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByID_Result.UnitMediumID);
						DataRow dataRow2 = dataRow;
						decimal value2 = 1;
						decimal? convertMediumUnit = get_Item_ByID_Result.ConvertMediumUnit;
						dataRow2["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value2 / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
						dataRow["TafqitId"] = get_Item_ByID_Result.UnitMediumID;
					}
					if (get_Item_ByID_Result.UnitTafqitID == 3)
					{
						dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByID_Result.UnitLargeID);
						DataRow dataRow3 = dataRow;
						decimal value2 = 1;
						decimal? convertMediumUnit = get_Item_ByID_Result.ConvertBigUnit;
						dataRow3["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value2 / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
						dataRow["TafqitId"] = get_Item_ByID_Result.UnitLargeID;
					}
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				insertnewrow();
				TxtType.SelectedValue = order_Orders.Order_Paymant_Type;
				CBCostCenter.SelectedValue = order_Orders.CostCentersID;
				if (order_Orders.BounceID.HasValue)
				{
					CBBounce.SelectedValue = order_Orders.BounceID;
				}
				LblSup.Text = order_Orders.SupplierID.ToString();
				TxtSuppliers.Text = order_Orders.SupplierName;
				TxtPhone.Text = order_Orders.SupplierPhone;
				TxtNumVat.Text = order_Orders.SupplierVatNum;
				TxtDate.DateTime = Convert.ToDateTime(order_Orders.Purchases_Date);
				TxtNote.Text = order_Orders.Note;
				TxtNumNote.Text = order_Orders.NoteNum;
				toggleHold.IsOn = Convert.ToBoolean(order_Orders.IsWaiting);
				TxtTotals.Text = Convert.ToString(order_Orders.TotalPrices);
				TxtVat.Text = Convert.ToString(order_Orders.Tax);
				TxtTotalAll.Text = Convert.ToString(order_Orders.Safy);
				TxtDiscount.Text = order_Orders.DiscountNum.ToString();
				TxtTotalBeforDisc.Text = order_Orders.TotalPrices_Discount.ToString();
				TxtVatBeforeDisc.Text = order_Orders.Tax_Discount.ToString();
				TxtNet.Text = order_Orders.TotalPrices_Discount.ToString();
				TxtNet22.Text = order_Orders.Net.ToString();
				TxtCost.Text = order_Orders.CostOrder.ToString();
				TxtTabocc.Text = order_Orders.TobaccoTax.ToString();
				TxtAllVat.Text = order_Orders.AllTax.ToString();
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = false;
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = true;
				FiterDatatableItemsUpdate();
				insertnewrow();
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
	}

	private void GroupPermission()
	{
		TxtSellPrice2.Visible = LoginDetails.SellPrice2;
		label12.Visible = LoginDetails.SellPrice2;
		TxtSellPrice3.Visible = LoginDetails.SellPrice3;
		label9.Visible = LoginDetails.SellPrice3;
		TxtProfit.Visible = LoginDetails.ProvePrice;
		label10.Visible = LoginDetails.ProvePrice;
		TxtAvergCost.Visible = LoginDetails.AvergPrice;
		label13.Visible = LoginDetails.AvergPrice;
		TxtLastCost.Visible = LoginDetails.LastPrice;
		label14.Visible = LoginDetails.LastPrice;
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
		TxtSellPrice1.DecimalDigits = DecimalNum;
		TxtSellPrice2.DecimalDigits = DecimalNum;
		TxtSellPrice3.DecimalDigits = DecimalNum;
		TxtProfit.DecimalDigits = DecimalNum;
		TxtAvergCost.DecimalDigits = DecimalNum;
		TxtLastCost.DecimalDigits = DecimalNum;
		TxtQuntity.DecimalDigits = DecimalNumQty;
		TxtAllQuantity.DecimalDigits = DecimalNumQty;
	}

	public void FilterDataTableSave(DataTable ItemOrder, int Purchese_ID, bool IsWait)
	{
		int num = 1;
		dtCurrentTable.Clear();
		foreach (DataRow row in ItemOrder.Rows)
		{
			if (row["كود_الصنف"].ToString() != "")
			{
				int num2 = Convert.ToInt32(row["رقم_الوحدة"]);
				dtCurrentTable.Rows.Add(num, Purchese_ID, Convert.ToInt32(row["ItemID"]), LoginDetails.BranchID, IsWait, Convert.ToInt32(row["رقم المخزن"]), num2, Convert.ToDecimal(row["الكمية"]), Convert.ToDecimal(row["متوسط_التكلفة"]), Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["سعر الوحدة"]), Convert.ToDecimal(row["الاجمالى"]), Convert.ToDecimal(row["الضربية"]), Convert.ToDecimal(row["سعربعدالخصم"]), Convert.ToDecimal(row["إجمالى_بعدالخصم"]), Convert.ToDecimal(row["الضريبه_بعد_الخصم"]), row["أسم_الوحدة"].ToString(), 1, row["ItemNote"].ToString(), 1, 0, Convert.ToDecimal(row["الوزن"]), Convert.ToDecimal(row["الإرتفاع"]), "", Convert.ToDecimal(row["العرض"]), Convert.ToDecimal(row["الطول"]), Convert.ToDecimal(row["البونص"]), Convert.ToDecimal(row["قيمة الخصم"]), Convert.ToDecimal(row["نسبة الخصم"]), Convert.ToInt32(row["TafqitId"]), Convert.ToDecimal(row["كمية التفقيط"]));
				num++;
			}
		}
	}

	private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "كود_الصنف"));
		if (text != "")
		{
			LoginDetails.ItemcodeForUnits = text;
			FrmSearchUnit frmSearchUnit = new FrmSearchUnit();
			frmSearchUnit.ShowDialog();
			if (frmSearchUnit.ItemCode != "")
			{
				using GTSdbEntities gTSdbEntities = new GTSdbEntities();
				string itemCode = frmSearchUnit.ItemCode;
				if (frmSearchUnit.unitName == "الصغرى")
				{
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(itemCode).FirstOrDefault();
					if (get_Item_ByCodeALL_Result != null)
					{
						int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row in datatable.Rows)
						{
							if (row["م"].ToString() == Convert.ToString(num))
							{
								row["الوحدة"] = get_Item_ByCodeALL_Result.UnitSmall;
								row["أسم_الوحدة"] = "الصغرى";
								if (BtnEdit.Text == "تعديل F6")
								{
									row["المخزن"] = Comp_Store.Text;
									row["رقم المخزن"] = Comp_Store.SelectedValue;
								}
								row["الكمية"] = FUNC_DecimalNumQty(1);
								row["رقم_الوحدة"] = get_Item_ByCodeALL_Result.UnitSmallID;
								row["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
								row["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
								row["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row["متوسط_التكلفة"]) * ((Convert.ToDecimal(row["الكمية"]) + Convert.ToDecimal(row["البونص"])) * (Convert.ToDecimal(row["الطول"]) * Convert.ToDecimal(row["العرض"]) * Convert.ToDecimal(row["الإرتفاع"]))));
								if (get_Item_ByCodeALL_Result.Is_Tax == true)
								{
									row["الضربية"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall * get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100);
									row["إجمالى المبلغ"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall * get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100 + get_Item_ByCodeALL_Result.SellPriceSmall);
								}
								else
								{
									row["الضربية"] = 0.0;
									row["إجمالى المبلغ"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
								}
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
							}
							datatable.AcceptChanges();
						}
						GetTotalCost();
						string StoreName = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int num2 = (from x in gTSdbEntities.Account_Stores
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
							if (row2["م"].ToString() == Convert.ToString(num3))
							{
								row2["الوحدة"] = get_Item_ByCodeALL_Result2.UnitMedium;
								row2["أسم_الوحدة"] = "المتوسطة";
								row2["رقم_الوحدة"] = get_Item_ByCodeALL_Result2.UnitMediumID;
								if (BtnEdit.Text == "تعديل F6")
								{
									row2["المخزن"] = Comp_Store.Text;
									row2["رقم المخزن"] = Comp_Store.SelectedValue;
								}
								row2["سعر الوحدة"] = get_Item_ByCodeALL_Result2.SellPriceMedium;
								row2["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium * (decimal?)Convert.ToDecimal(row2["الكمية"]));
								decimal num4 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.ConvertMediumUnit);
								row2["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row2["متوسط_التكلفة"]) * ((Convert.ToDecimal(row2["الكمية"]) + Convert.ToDecimal(row2["البونص"])) * (Convert.ToDecimal(row2["الطول"]) * Convert.ToDecimal(row2["العرض"]) * Convert.ToDecimal(row2["الإرتفاع"])) * num4));
								Vat(get_Item_ByCodeALL_Result2.Is_Tax, get_Item_ByCodeALL_Result2.Tax_Value, get_Item_ByCodeALL_Result2.SellPriceMedium, Convert.ToDecimal(row2["الكمية"]), row2, "");
								if (get_Item_ByCodeALL_Result2.UnitTafqitID == 1)
								{
									int value2 = Convert.ToInt32(get_Item_ByCodeALL_Result2.UnitSmallID);
									row2["وحدة التفقيط"] = ITM.GetUnitName(value2);
									row2["كمية التفقيط"] = FUNC_DecimalNum(row2["الكمية"].ToString());
									row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitSmallID;
								}
								if (get_Item_ByCodeALL_Result2.UnitTafqitID == 2)
								{
									row2["وحدة التفقيط"] = get_Item_ByCodeALL_Result2.UnitMedium;
									row2["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.ConvertMediumUnit / get_Item_ByCodeALL_Result2.ConvertMediumUnit * (decimal?)Convert.ToDecimal(row2["الكمية"]));
									row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitLargeID;
								}
								if (get_Item_ByCodeALL_Result2.UnitTafqitID == 3)
								{
									row2["وحدة التفقيط"] = get_Item_ByCodeALL_Result2.UnitLarge;
									row2["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.ConvertMediumUnit / get_Item_ByCodeALL_Result2.ConvertBigUnit * (decimal?)Convert.ToDecimal(row2["الكمية"]));
									row2["TafqitId"] = get_Item_ByCodeALL_Result2.UnitLargeID;
								}
							}
							datatable.AcceptChanges();
						}
						GetTotalCost();
						string StoreName2 = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int num5 = (from x in gTSdbEntities.Account_Stores
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
							if (row3["م"].ToString() == Convert.ToString(num6))
							{
								row3["الوحدة"] = get_Item_ByCodeALL_Result3.UnitLarge;
								row3["أسم_الوحدة"] = "الكبرى";
								row3["رقم_الوحدة"] = get_Item_ByCodeALL_Result3.UnitLargeID;
								if (BtnEdit.Text == "تعديل F6")
								{
									row3["المخزن"] = Comp_Store.Text;
									row3["رقم المخزن"] = Comp_Store.SelectedValue;
								}
								row3["سعر الوحدة"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge);
								row3["الاجمالى"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge);
								decimal num7 = Convert.ToDecimal(get_Item_ByCodeALL_Result3.ConvertBigUnit);
								row3["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(row3["متوسط_التكلفة"]) * ((Convert.ToDecimal(row3["الكمية"]) + Convert.ToDecimal(row3["البونص"])) * (Convert.ToDecimal(row3["الطول"]) * Convert.ToDecimal(row3["العرض"]) * Convert.ToDecimal(row3["الإرتفاع"])) * num7));
								if (get_Item_ByCodeALL_Result3.Is_Tax == true)
								{
									row3["الضربية"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge * get_Item_ByCodeALL_Result3.Tax_Value / (decimal?)100);
									row3["إجمالى المبلغ"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge * get_Item_ByCodeALL_Result3.Tax_Value / (decimal?)100 + get_Item_ByCodeALL_Result3.SellpriceLarge);
								}
								else
								{
									row3["الضربية"] = 0.0;
									row3["إجمالى المبلغ"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge);
								}
								if (get_Item_ByCodeALL_Result3.UnitTafqitID == 1)
								{
									int value3 = Convert.ToInt32(get_Item_ByCodeALL_Result3.UnitSmallID);
									row3["وحدة التفقيط"] = ITM.GetUnitName(value3);
									row3["كمية التفقيط"] = FUNC_DecimalNum(row3["الكمية"].ToString());
									row3["TafqitId"] = get_Item_ByCodeALL_Result3.UnitSmallID;
								}
								if (get_Item_ByCodeALL_Result3.UnitTafqitID == 2)
								{
									row3["وحدة التفقيط"] = get_Item_ByCodeALL_Result3.UnitMedium;
									row3["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.ConvertBigUnit / get_Item_ByCodeALL_Result3.ConvertMediumUnit * (decimal?)Convert.ToDecimal(row3["الكمية"]));
									row3["TafqitId"] = get_Item_ByCodeALL_Result3.UnitMediumID;
								}
								if (get_Item_ByCodeALL_Result3.UnitTafqitID == 3)
								{
									row3["وحدة التفقيط"] = get_Item_ByCodeALL_Result3.UnitLarge;
									row3["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.ConvertBigUnit / get_Item_ByCodeALL_Result3.ConvertBigUnit * (decimal?)Convert.ToDecimal(row3["الكمية"]));
									row3["TafqitId"] = get_Item_ByCodeALL_Result3.UnitLargeID;
								}
							}
							datatable.AcceptChanges();
						}
						GetTotalCost();
						string StoreName3 = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int num8 = (from x in gTSdbEntities.Account_Stores
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

	private void repositoryItemButtonEdit2_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["م"].ToString() == Convert.ToString(num))
			{
				if (row["كود_الصنف"].ToString() != "")
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
		Cal_FinalResultTotals();
	}

	private void repositoryItemButtonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
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

	private void repositoryItemButtonEditCodeSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchItems();
		LoginDetails.ItemSearch = null;
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
		if (e.KeyCode.ToString() == "F9")
		{
			GetPricePffer();
			return;
		}
		if (e.KeyCode.ToString() == "F10")
		{
			CopyOrders();
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
		try
		{
			if (ISEVENTDISC)
			{
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
				TxtDiscount.Text = (Convert.ToDecimal(TxtNet.Text) - num).ToString();
			}
			if (LoginDetails.DiscountDecimal)
			{
				TxtDiscount.Text = "0.00";
				decimal num2 = Math.Floor(Convert.ToDecimal(TxtNet.Text));
				TxtDiscount.Text = (Convert.ToDecimal(TxtNet.Text) - num2).ToString();
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

	private void TxtNote_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Return)
		{
			groupBox2.Focus();
			gridView1.Focus();
		}
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
			TxtSuppliers.Text = "عميل نقدى";
			LblSup.Text = "0";
			TxtAdditionalNum.Text = "";
			TxtCity.Text = "";
			TxtStreet.Text = "";
			TxtDistrict.Text = "";
			TxtPostalCode.Text = "";
			TxtCharge.Text = "0.00";
			TxtCommercialRecord.Text = "";
			TxtSales.Text = "";
			TxtPhone.Text = "";
			TxtNumVat.Text = "";
			TxtSuppliers.ForeColor = Color.Black;
			TxtType.ForeColor = Color.Black;
		}
		if (TxtType.Text == "آجلة")
		{
			TxtSuppliers.Text = "";
			TxtAdditionalNum.Text = "";
			TxtCity.Text = "";
			TxtStreet.Text = "";
			TxtDistrict.Text = "";
			TxtPostalCode.Text = "";
			TxtCharge.Text = "0.00";
			TxtCommercialRecord.Text = "";
			TxtSales.Text = "";
			TxtPhone.Text = "";
			TxtNumVat.Text = "";
			TxtSuppliers.ForeColor = Color.Red;
			TxtType.ForeColor = Color.Red;
		}
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void ControlTxtSuppliers_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtSuppliers.Focus();
		}
	}

	private void ControlTxtNumVat_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtNumVat.Focus();
		}
	}

	private void TxtNet_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (LoginDetails.Lang == "ar-EG")
			{
				ToWord toWord = new ToWord(Convert.ToDecimal(TxtNet22.Text), currencies[tafqitID]);
				txtWordAr = toWord.ConvertToArabic();
				TxtTafqit.Text = txtWordAr;
			}
			else
			{
				ToWord toWord2 = new ToWord(Convert.ToDecimal(TxtNet22.Text), currencies[tafqitID]);
				txtWordAr = toWord2.ConvertToEnglish();
				TxtTafqit.Text = txtWordAr;
			}
		}
		catch (Exception)
		{
		}
	}

	private void LblSup_TextChanged(object sender, EventArgs e)
	{
		try
		{
			LBlSuppAccount.Text = Cust.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
		}
		catch
		{
		}
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
		openItmCard();
	}

	private void MenuSearchItem_Click(object sender, EventArgs e)
	{
		SearchItems();
	}

	private void MenuPrintQued_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text != "")
		{
			Class_Print_DailyRestrictionsALL class_Print_DailyRestrictionsALL = new Class_Print_DailyRestrictionsALL();
			class_Print_DailyRestrictionsALL.Print_DailyRestrictionsALL(Convert.ToInt32(TxtCode.Text), 5);
		}
	}

	private void FrmOrders_FormClosing(object sender, FormClosingEventArgs e)
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

	private void MenuPurchOrder_Click(object sender, EventArgs e)
	{
		GetPricePffer();
	}

	private void label23_Click(object sender, EventArgs e)
	{
	}

	private void TxtNote_EditValueChanged(object sender, EventArgs e)
	{
	}

	private void StripMenuItem1_Click(object sender, EventArgs e)
	{
		CopyOrders();
	}

	private void تخصيصToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		new FrmCustomer().ShowDialog();
	}

	private void BtnOrderSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SearchOrdNum(Convert.ToInt32(BtnOrderSearch.Text));
		}
	}

	private void BtnCustomerSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && !string.IsNullOrWhiteSpace(BtnCustomerSearch.Text))
		{
			SearchCustNum(Convert.ToInt32(BtnCustomerSearch.Text), 1);
		}
	}

	private void BtnOrderSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(BtnOrderSearch.Text))
		{
			SearchOrdNum(Convert.ToInt32(BtnOrderSearch.Text));
		}
	}

	private void BtnCustomerSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(BtnCustomerSearch.Text))
		{
			SearchCustNum(Convert.ToInt32(BtnCustomerSearch.Text), 1);
		}
	}

	private void MenuExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!(TxtSuppliers.Text != ""))
		{
			return;
		}
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
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

	private void حركهصنففعليةToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Class_MovementItem class_MovementItem = new Class_MovementItem();
		class_MovementItem.MovementItemActive(ItmID);
	}

	private void BtnNewCustomer_Click(object sender, EventArgs e)
	{
		new FrmCustomer().Show();
	}

	private void TxtType_KeyPress_1(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
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
				dr["الربح"] = FUNC_DecimalNum(Convert.ToDecimal(dr["إجمالى_بعدالخصم"]) - Convert.ToDecimal(dr["اجمالي_متوسط_التكلفة"]));
			}
			else
			{
				dr["نسبة الخصم"] = "0.00";
				dr["قيمة الخصم"] = "0.00";
				dr["إجمالى_بعدالخصم"] = dr["الاجمالى"];
				dr["الصافى"] = dr["الاجمالى"];
				dr["الربح"] = FUNC_DecimalNum(Convert.ToDecimal(dr["إجمالى_بعدالخصم"]) - Convert.ToDecimal(dr["اجمالي_متوسط_التكلفة"]));
			}
		}
		else if (Convert.ToDecimal(dr["قيمة الخصم"]) <= Convert.ToDecimal(dr["الاجمالى"]) && Convert.ToDecimal(dr["قيمة الخصم"]) != 0m)
		{
			decimal num3 = Convert.ToDecimal(dr["الاجمالى"]);
			dr["إجمالى_بعدالخصم"] = Math.Round(Convert.ToDecimal(dr["الاجمالى"]) - Convert.ToDecimal(dr["قيمة الخصم"]), DecimalNum).ToString();
			dr["الصافى"] = Math.Round(Convert.ToDecimal(dr["الاجمالى"]) - Convert.ToDecimal(dr["قيمة الخصم"]), DecimalNum);
			decimal num4 = Math.Round(Convert.ToDecimal(dr["قيمة الخصم"]) / Convert.ToDecimal(dr["الاجمالى"]) * 100m, DecimalNum);
			dr["نسبة الخصم"] = num4;
			dr["الربح"] = FUNC_DecimalNum(Convert.ToDecimal(dr["إجمالى_بعدالخصم"]) - Convert.ToDecimal(dr["اجمالي_متوسط_التكلفة"]));
		}
		else
		{
			dr["نسبة الخصم"] = "0.00";
			dr["قيمة الخصم"] = "0.00";
			dr["إجمالى_بعدالخصم"] = dr["الاجمالى"];
			dr["الصافى"] = dr["الاجمالى"];
			dr["الربح"] = FUNC_DecimalNum(Convert.ToDecimal(dr["إجمالى_بعدالخصم"]) - Convert.ToDecimal(dr["اجمالي_متوسط_التكلفة"]));
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

	private void Hasm(bool Action)
	{
		LblAmountHasm.Visible = Action;
		LblPerHasm.Visible = Action;
		LblHasm.Visible = Action;
		TxtHasmAmount.Visible = Action;
		TxtHasmPer.Visible = Action;
	}

	private void BtnOrderReturn_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 14))
		{
			new FrmOrderReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnShowPrice_Click(object sender, EventArgs e)
	{
		GetPricePffer();
	}

	private void BtnRecipt_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 31))
		{
			new FrmReceipts().Show();
			Charge(1);
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void LblNowQuintity_MouseEnter(object sender, EventArgs e)
	{
	}

	private void TxtNet22_TextChanged(object sender, EventArgs e)
	{
		TxtNet_TextChanged(null, null);
	}

	private void gridView1_RowClick(object sender, RowClickEventArgs e)
	{
	}

	private void TxtCode_EditValueChanged(object sender, EventArgs e)
	{
	}

	private void groupBox3_Enter(object sender, EventArgs e)
	{
	}

	private void TxtNumNote_EditValueChanged(object sender, EventArgs e)
	{
	}

	private void repositoryItemTextEditTafqit_Click(object sender, EventArgs e)
	{
		ValueChangeTafqit("كمية التفقيط", null);
	}

	private void toggleHold_Toggled(object sender, EventArgs e)
	{
		if (toggleHold.IsOn)
		{
			LblDataHold.Visible = true;
			DateHold.Visible = true;
		}
		else
		{
			LblDataHold.Visible = false;
			DateHold.Visible = false;
		}
	}

	private void groupBox4_Enter(object sender, EventArgs e)
	{
	}

	private void BtnOrderSearch_EditValueChanged(object sender, EventArgs e)
	{
	}

	private void groupBox5_Enter(object sender, EventArgs e)
	{
	}

	private void CBProject_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (CBProject.Items.Count > 0)
			{
				CBCostCenter.DataSource = Cost.GetCostCentersByProject(Convert.ToInt32(CBProject.SelectedValue));
				CBCostCenter.DisplayMember = "CostCentersName";
				CBCostCenter.ValueMember = "CostCentersID";
				CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
				CBCostCenter.SelectedIndex = 1;
			}
		}
		catch
		{
		}
	}

	private void label5_Click(object sender, EventArgs e)
	{
	}

	private void TxtHasmAmount_TextChanged(object sender, EventArgs e)
	{
		try
		{
			ISEVENTDISC = false;
			decimal num = Math.Round(Convert.ToDecimal(TxtNet.Text) - Convert.ToDecimal(TxtHasmAmount.Text), DecimalNum);
			TxtNet22.Value = num + Convert.ToDecimal(TxtVatBeforeDisc.Text);
			decimal num2 = Math.Round(Convert.ToDecimal(TxtHasmAmount.Text) / Convert.ToDecimal(TxtNet.Text) * 100m, DecimalNum);
			TxtHasmPer.Value = num2;
			ISEVENTDISC = false;
			ISEVENTDISC = true;
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
		}
	}

	private void TxtHasmPer_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (ISEVENTDISC)
			{
				if (TxtHasmPer.Text != "" || Convert.ToDecimal(TxtHasmPer.Text) != 0m || Convert.ToDecimal(TxtHasmPer.Text) > 100m)
				{
					decimal num = Math.Round(Convert.ToDecimal(TxtHasmPer.Text) * Convert.ToDecimal(TxtNet.Text) / 100m, DecimalNum);
					TxtHasmAmount.Text = num.ToString();
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

	private void toggleHold_Properties_Toggled(object sender, EventArgs e)
	{
		if (toggleHold.IsOn)
		{
			LblDataHold.Visible = true;
			DateHold.Visible = true;
		}
		else
		{
			LblDataHold.Visible = false;
			DateHold.Visible = false;
		}
	}

	private void حركةصنفToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Class_MovementItem class_MovementItem = new Class_MovementItem();
		class_MovementItem.MovementItemNormal(ItmID);
	}

	private void simpleButton1_Click(object sender, EventArgs e)
	{
		new FrmSearchWedding().Show();
	}

	private void BtnMore_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(TxtCode.Text))
		{
		}
	}

	private void BtnReportHold_Click(object sender, EventArgs e)
	{
		new FrmAllOrdersHold().Show();
	}

	private void DateHold_TextChanged(object sender, EventArgs e)
	{
		CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("ar-SA");
		int day = TxtDate.DateTime.Day;
		int month = TxtDate.DateTime.Month;
		int year = TxtDate.DateTime.Year;
		DateTime dateTime = new DateTime(year, month, day);
		TxtAddCappuccino.Text = dateTime.ToString("yyyy/MM/dd", cultureInfo);
	}

	private void label20_Click(object sender, EventArgs e)
	{
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
		}
		catch (Exception)
		{
		}
	}

	private void ConvertFromPur(DataTable data)
	{
		if (data == null)
		{
			return;
		}
		datatable.Clear();
		int num = 0;
		foreach (object row in data.Rows)
		{
			Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ORD.GetItemByCode(data.Rows[num][2].ToString()).FirstOrDefault();
			string value = data.Rows[num][0].ToString();
			if (get_Item_ByCodeALL_Result == null || string.IsNullOrWhiteSpace(value))
			{
				continue;
			}
			DataRow dataRow = null;
			dataRow = datatable.NewRow();
			dataRow["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
			dataRow["ItemUnitID"] = data.Rows[num][1];
			dataRow["كود_الصنف"] = get_Item_ByCodeALL_Result.Item_code;
			dataRow["أسم الصنف"] = get_Item_ByCodeALL_Result.item_Name;
			dataRow["الوحدة"] = data.Rows[num][4];
			dataRow["رقم_الوحدة"] = data.Rows[num][1];
			dataRow["البونص"] = data.Rows[num][9];
			dataRow["الطول"] = 1;
			dataRow["العرض"] = 1;
			dataRow["الإرتفاع"] = 1;
			dataRow["الوزن"] = 1;
			dataRow["قيمة الخصم"] = 0;
			dataRow["نسبة الخصم"] = 0;
			dataRow["الكمية"] = data.Rows[num][7];
			dataRow["المخزن"] = data.Rows[num][5];
			dataRow["رقم المخزن"] = data.Rows[num][6];
			dataRow["أسم_الوحدة"] = data.Rows[num][17];
			dataRow["سعر الوحدة"] = data.Rows[num][11];
			dataRow["البيان"] = data.Rows[num][24];
			dataRow["إجمالى المبلغ"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["الكمية"]) * Convert.ToDecimal(dataRow["سعر الوحدة"]));
			dataRow["إجمالى_بعدالخصم"] = dataRow["إجمالى المبلغ"];
			dataRow["الاجمالى"] = dataRow["إجمالى المبلغ"];
			dataRow["الضريبه_بعد_الخصم"] = data.Rows[num][21];
			dataRow["الصافى"] = data.Rows[num][18];
			decimal num2 = Convert.ToDecimal(data.Rows[num][11]);
			decimal num3 = FUNC_DecimalNum(Convert.ToDecimal(dataRow["الكمية"]) + Convert.ToDecimal(dataRow["البونص"]));
			decimal num4 = FUNC_DecimalNum(Convert.ToDecimal(dataRow["الكمية"]));
			decimal num5 = num2 * num4;
			if (dataRow["أسم_الوحدة"].ToString() == "الصغرى")
			{
				dataRow["متوسط_التكلفة"] = get_Item_ByCodeALL_Result.LastCost;
				dataRow["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * num3);
				Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(data.Rows[num][11]), num4, dataRow, null);
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
				{
					dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitSmallID);
					dataRow["كمية التفقيط"] = FUNC_DecimalNum(dataRow["الكمية"].ToString());
					dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
				{
					dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitMediumID);
					DataRow dataRow2 = dataRow;
					decimal value2 = 1;
					decimal? convertMediumUnit = get_Item_ByCodeALL_Result.ConvertMediumUnit;
					dataRow2["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value2 / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
					dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
				{
					dataRow["وحدة التفقيط"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitLargeID);
					DataRow dataRow3 = dataRow;
					decimal value2 = 1;
					decimal? convertMediumUnit = get_Item_ByCodeALL_Result.ConvertBigUnit;
					dataRow3["كمية التفقيط"] = FUNC_DecimalNum((decimal?)value2 / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
					dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
				}
			}
			if (dataRow["أسم_الوحدة"].ToString() == "المتوسطة")
			{
				Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(data.Rows[num][11]), num4, dataRow, null);
				dataRow["متوسط_التكلفة"] = get_Item_ByCodeALL_Result.LastCost2;
				decimal num6 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertMediumUnit);
				dataRow["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * (num3 * num6));
				dataRow["الاجمالى"] = Math.Round(num2 * num4, DecimalNum);
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
				{
					int value3 = Convert.ToInt32(get_Item_ByCodeALL_Result.UnitSmallID);
					dataRow["وحدة التفقيط"] = ITM.GetUnitName(value3);
					dataRow["كمية التفقيط"] = FUNC_DecimalNum(dataRow["الكمية"].ToString());
					dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
				{
					dataRow["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitMedium;
					dataRow["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.ConvertMediumUnit / get_Item_ByCodeALL_Result.ConvertMediumUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
					dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
				{
					dataRow["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitLarge;
					dataRow["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.ConvertMediumUnit / get_Item_ByCodeALL_Result.ConvertBigUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
					dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
				}
			}
			if (dataRow["أسم_الوحدة"].ToString() == "الكبرى")
			{
				Vat(get_Item_ByCodeALL_Result.Is_Tax, get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(data.Rows[num][11]), num4, dataRow, null);
				dataRow["متوسط_التكلفة"] = get_Item_ByCodeALL_Result.LastCost3;
				decimal num7 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertBigUnit);
				dataRow["اجمالي_متوسط_التكلفة"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * (num3 * num7));
				dataRow["الاجمالى"] = Math.Round(num2 * num4, DecimalNum);
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
				{
					int value4 = Convert.ToInt32(get_Item_ByCodeALL_Result.UnitSmallID);
					dataRow["وحدة التفقيط"] = ITM.GetUnitName(value4);
					dataRow["كمية التفقيط"] = FUNC_DecimalNum(dataRow["الكمية"].ToString());
					dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
				{
					dataRow["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitMedium;
					dataRow["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.ConvertBigUnit / get_Item_ByCodeALL_Result.ConvertMediumUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
					dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
				}
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
				{
					dataRow["وحدة التفقيط"] = get_Item_ByCodeALL_Result.UnitLarge;
					dataRow["كمية التفقيط"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.ConvertBigUnit / get_Item_ByCodeALL_Result.ConvertBigUnit * (decimal?)Convert.ToDecimal(dataRow["الكمية"]));
					dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
				}
			}
			datatable.Rows.Add(dataRow);
			num++;
		}
		insertnewrow();
		ArrangeDatatable();
		Cal_FinalResultTotals();
		gridControl1.DataSource = datatable;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmOrdersWedding));
		DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
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
		this.TxtLocation = new DevExpress.XtraEditors.TextEdit();
		this.label25 = new System.Windows.Forms.Label();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.label14 = new System.Windows.Forms.Label();
		this.TxtQuntity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.LblNowQuintity = new System.Windows.Forms.Label();
		this.TxtAllQuantity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtLastCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtAvergCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.TxtNet22 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.تخصيصToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.CBProject = new System.Windows.Forms.ComboBox();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.toggleSectoral = new DevExpress.XtraEditors.ToggleSwitch();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtAllVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label30 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.TxtCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtAllTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTabocc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label39 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.TxtProfit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label7 = new System.Windows.Forms.Label();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label8 = new System.Windows.Forms.Label();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.TxtSellPrice3 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTotalAll = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotalBeforDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtSellPrice2 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtSellPrice1 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatBeforeDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label24 = new System.Windows.Forms.Label();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.TxtNumVat = new DevExpress.XtraEditors.TextEdit();
		this.label18 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.CBBounce = new System.Windows.Forms.ComboBox();
		this.toggleHold = new DevExpress.XtraEditors.ToggleSwitch();
		this.BtnCustomerSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnNewCustomer = new DevExpress.XtraEditors.SimpleButton();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnOrderSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.StripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPurchOrder = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.حركةصنفToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuNewItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPreviewFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintQued = new System.Windows.Forms.ToolStripMenuItem();
		this.طباعةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.كشفحسابToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.حركهصنففعليةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.TxtAnotherGroom = new System.Windows.Forms.TextBox();
		this.TxtLesar = new System.Windows.Forms.TextBox();
		this.TxtScreenAdd = new System.Windows.Forms.TextBox();
		this.TxtAddHeadPhones = new System.Windows.Forms.TextBox();
		this.TxtTarab = new System.Windows.Forms.TextBox();
		this.TxtWedding = new System.Windows.Forms.TextBox();
		this.label56 = new System.Windows.Forms.Label();
		this.label57 = new System.Windows.Forms.Label();
		this.TxtCurains = new System.Windows.Forms.TextBox();
		this.label60 = new System.Windows.Forms.Label();
		this.label58 = new System.Windows.Forms.Label();
		this.label61 = new System.Windows.Forms.Label();
		this.label62 = new System.Windows.Forms.Label();
		this.label63 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label40 = new System.Windows.Forms.Label();
		this.label41 = new System.Windows.Forms.Label();
		this.label44 = new System.Windows.Forms.Label();
		this.label45 = new System.Windows.Forms.Label();
		this.TxtKosha = new System.Windows.Forms.TextBox();
		this.TxtCountCaress = new System.Windows.Forms.TextBox();
		this.TxtCookCarecass = new System.Windows.Forms.TextBox();
		this.TxtCountWomenTable = new System.Windows.Forms.TextBox();
		this.TxtCountWomen = new System.Windows.Forms.TextBox();
		this.TxtCountCandles = new System.Windows.Forms.TextBox();
		this.TxtCountTableMen = new System.Windows.Forms.TextBox();
		this.TxtNumberOfMen = new System.Windows.Forms.TextBox();
		this.label47 = new System.Windows.Forms.Label();
		this.TxtCountHeaters = new System.Windows.Forms.TextBox();
		this.label49 = new System.Windows.Forms.Label();
		this.TxtTypeCaress = new System.Windows.Forms.TextBox();
		this.label50 = new System.Windows.Forms.Label();
		this.label64 = new System.Windows.Forms.Label();
		this.label51 = new System.Windows.Forms.Label();
		this.label52 = new System.Windows.Forms.Label();
		this.TxtBff = new System.Windows.Forms.TextBox();
		this.TxtTrombah = new System.Windows.Forms.TextBox();
		this.label53 = new System.Windows.Forms.Label();
		this.label54 = new System.Windows.Forms.Label();
		this.label55 = new System.Windows.Forms.Label();
		this.TxtPriceCaress = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label32 = new System.Windows.Forms.Label();
		this.TxtDiscountALLTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnOrderReturn = new DevExpress.XtraEditors.SimpleButton();
		this.BtnRecipt = new DevExpress.XtraEditors.SimpleButton();
		this.BtnShowPrice = new DevExpress.XtraEditors.SimpleButton();
		this.TxtAddCappuccino = new System.Windows.Forms.TextBox();
		this.TxtHasmPer = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.LblPerHasm = new System.Windows.Forms.Label();
		this.LblHasm = new System.Windows.Forms.Label();
		this.LblAmountHasm = new System.Windows.Forms.Label();
		this.TxtHasmAmount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.label59 = new System.Windows.Forms.Label();
		this.label43 = new System.Windows.Forms.Label();
		this.label46 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label34 = new System.Windows.Forms.Label();
		this.label38 = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.label42 = new System.Windows.Forms.Label();
		this.TxtAdditionalNum = new DevExpress.XtraEditors.TextEdit();
		this.TxtStreet = new DevExpress.XtraEditors.TextEdit();
		this.TxtDistrict = new DevExpress.XtraEditors.TextEdit();
		this.TxtCommercialRecord = new DevExpress.XtraEditors.TextEdit();
		this.TxtPostalCode = new DevExpress.XtraEditors.TextEdit();
		this.TxtCountry = new DevExpress.XtraEditors.TextEdit();
		this.TxtCity = new DevExpress.XtraEditors.TextEdit();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtSales = new DevExpress.XtraEditors.TextEdit();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.BtnReportHold = new DevExpress.XtraEditors.SimpleButton();
		this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
		this.label48 = new System.Windows.Forms.Label();
		this.DateHold = new DevExpress.XtraEditors.DateEdit();
		this.LblDataHold = new System.Windows.Forms.Label();
		this.LblCustLast = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.Comp_Store = new System.Windows.Forms.ComboBox();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.label37 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)repositoryItemTextEdit).BeginInit();
		((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleSectoral.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleHold.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtAdditionalNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtStreet.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDistrict.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCountry.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales.Properties).BeginInit();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.TxtLocation, "TxtLocation");
		this.TxtLocation.Name = "TxtLocation";
		this.TxtLocation.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtLocation.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtLocation.Properties.Appearance.Font");
		this.TxtLocation.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
		this.TxtLocation.Properties.Appearance.Options.UseBackColor = true;
		this.TxtLocation.Properties.Appearance.Options.UseFont = true;
		this.TxtLocation.Properties.Appearance.Options.UseForeColor = true;
		this.TxtLocation.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLocation.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtLocation.Properties.ReadOnly = true;
		resources.ApplyResources(this.label25, "label25");
		this.label25.Name = "label25";
		resources.ApplyResources(this.txtdicoutprege, "txtdicoutprege");
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Black;
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.txtdicoutprege.TextChanged += new System.EventHandler(TxtDiscountPrecent_TextChanged);
		this.txtdicoutprege.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label13, "label13");
		this.label13.Name = "label13";
		resources.ApplyResources(this.TxtTafqit, "TxtTafqit");
		this.TxtTafqit.Name = "TxtTafqit";
		this.TxtTafqit.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtTafqit.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtTafqit.Properties.Appearance.Font");
		this.TxtTafqit.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
		this.TxtTafqit.Properties.Appearance.Options.UseBackColor = true;
		this.TxtTafqit.Properties.Appearance.Options.UseFont = true;
		this.TxtTafqit.Properties.Appearance.Options.UseForeColor = true;
		this.TxtTafqit.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTafqit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTafqit.Properties.ReadOnly = true;
		resources.ApplyResources(this.label14, "label14");
		this.label14.Name = "label14";
		resources.ApplyResources(this.TxtQuntity, "TxtQuntity");
		this.TxtQuntity.DecimalDigits = 3;
		this.TxtQuntity.ForeColor = System.Drawing.Color.Black;
		this.TxtQuntity.Name = "TxtQuntity";
		this.TxtQuntity.ReadOnly = true;
		this.TxtQuntity.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtQuntity.Value = new decimal(new int[4]);
		this.TxtQuntity.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label16, "label16");
		this.label16.Name = "label16";
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.LblNowQuintity, "LblNowQuintity");
		this.LblNowQuintity.Name = "LblNowQuintity";
		this.LblNowQuintity.MouseEnter += new System.EventHandler(LblNowQuintity_MouseEnter);
		resources.ApplyResources(this.TxtAllQuantity, "TxtAllQuantity");
		this.TxtAllQuantity.DecimalDigits = 3;
		this.TxtAllQuantity.ForeColor = System.Drawing.Color.Black;
		this.TxtAllQuantity.Name = "TxtAllQuantity";
		this.TxtAllQuantity.ReadOnly = true;
		this.TxtAllQuantity.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllQuantity.Value = new decimal(new int[4]);
		this.TxtAllQuantity.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtLastCost, "TxtLastCost");
		this.TxtLastCost.DecimalDigits = 3;
		this.TxtLastCost.ForeColor = System.Drawing.Color.Black;
		this.TxtLastCost.Name = "TxtLastCost";
		this.TxtLastCost.ReadOnly = true;
		this.TxtLastCost.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtLastCost.Value = new decimal(new int[4]);
		this.TxtLastCost.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnEdit.Appearance.Font");
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.TxtAvergCost, "TxtAvergCost");
		this.TxtAvergCost.DecimalDigits = 3;
		this.TxtAvergCost.ForeColor = System.Drawing.Color.Black;
		this.TxtAvergCost.Name = "TxtAvergCost";
		this.TxtAvergCost.ReadOnly = true;
		this.TxtAvergCost.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAvergCost.Value = new decimal(new int[4]);
		this.TxtAvergCost.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		resources.ApplyResources(this.gridColumn4, "gridColumn4");
		this.gridColumn4.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceCell.Font");
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceHeader.Font");
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(repositoryItemButtonEdit, "repositoryItemButtonEdit4");
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		repositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
		});
		repositoryItemButtonEdit.Name = "repositoryItemButtonEdit5";
		this.gridColumn4.ColumnEdit = repositoryItemButtonEdit;
		this.gridColumn4.FieldName = "المخزن";
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.تخصيصToolStripMenuItem1, "تخصيصToolStripMenuItem1");
		this.تخصيصToolStripMenuItem1.Name = "تخصيصToolStripMenuItem1";
		this.تخصيصToolStripMenuItem1.Click += new System.EventHandler(تخصيصToolStripMenuItem1_Click);
		resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		resources.ApplyResources(this.MenuExit, "MenuExit");
		this.MenuExit.Name = "MenuExit";
		this.MenuExit.Click += new System.EventHandler(MenuExit_Click);
		resources.ApplyResources(this.groupBox3, "groupBox3");
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Controls.Add(this.CBProject);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.TabStop = false;
		this.groupBox3.Enter += new System.EventHandler(groupBox3_Enter);
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
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[29]
		{
			this.gridColumn1, this.gridColumn2, this.gridColumn20, this.gridColumn21, this.gridColumn27, this.shearch, this.gridColumn4, this.gridColumn23, this.gridColumn24, this.gridColumn25,
			this.gridColumn26, this.gridColumn36, this.gridColumn3, this.gridColumn6, this.gridColumn7, this.gridColumn8, this.gridColumn22, this.gridColumn5, this.gridColumn9, this.gridColumn19,
			this.gridColumn28, this.gridColumn29, this.gridColumn30, this.gridColumn31, this.gridColumn32, this.gridColumn37, this.gridColumn33, this.gridColumn34, this.gridColumn35
		});
		this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(831, 81, 252, 466);
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.HorzScrollStep = 2;
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
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridView1.OptionsView.ShowGroupPanel = false;
		this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridView1_RowClick);
		this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
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
		resources.ApplyResources(repositoryItemButtonEdit2, "repositoryItemButtonEdit5");
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"), resources.GetString("repositoryItemButtonEdit5.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit5.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEdit5.Buttons6"), resources.GetObject("repositoryItemButtonEdit5.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit5.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit5.Buttons9"))
		});
		repositoryItemButtonEdit2.Name = "repositoryItemButtonEditCodeSearch";
		this.gridColumn1.ColumnEdit = repositoryItemButtonEdit2;
		this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.FieldName = "كود_الصنف";
		this.gridColumn1.Name = "gridColumn1";
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
		this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn20.FieldName = "الكمية";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
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
		this.gridColumn21.FieldName = "سعر الوحدة";
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn27, "gridColumn27");
		this.gridColumn27.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceCell.Font");
		this.gridColumn27.AppearanceCell.Options.UseFont = true;
		this.gridColumn27.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceHeader.Font");
		this.gridColumn27.AppearanceHeader.Options.UseFont = true;
		this.gridColumn27.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.FieldName = "ItemNote";
		this.gridColumn27.MinWidth = 24;
		this.gridColumn27.Name = "gridColumn27";
		this.gridColumn27.OptionsColumn.FixedWidth = true;
		resources.ApplyResources(this.shearch, "shearch");
		this.shearch.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("shearch.AppearanceCell.Font");
		this.shearch.AppearanceCell.Options.UseFont = true;
		this.shearch.AppearanceCell.Options.UseTextOptions = true;
		this.shearch.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("shearch.AppearanceHeader.Font");
		this.shearch.AppearanceHeader.Options.UseFont = true;
		this.shearch.AppearanceHeader.Options.UseTextOptions = true;
		this.shearch.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		repositoryItemButtonEdit3.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(repositoryItemButtonEdit3, "repositoryItemButtonEdit6");
		editorButtonImageOptions3.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		editorButtonImageOptions3.ImageUri.Uri = "Zoom100";
		repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit4";
		this.shearch.ColumnEdit = repositoryItemButtonEdit3;
		this.shearch.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.FieldName = "الوحدة";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn23, "gridColumn23");
		this.gridColumn23.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceCell.Font");
		this.gridColumn23.AppearanceCell.Options.UseFont = true;
		this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceHeader.Font");
		this.gridColumn23.AppearanceHeader.Options.UseFont = true;
		this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.FieldName = "متوسط_التكلفة";
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn24, "gridColumn24");
		this.gridColumn24.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceCell.Font");
		this.gridColumn24.AppearanceCell.Options.UseFont = true;
		this.gridColumn24.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceHeader.Font");
		this.gridColumn24.AppearanceHeader.Options.UseFont = true;
		this.gridColumn24.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.FieldName = "اجمالي_متوسط_التكلفة";
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn24.OptionsColumn.AllowFocus = false;
		this.gridColumn24.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn25, "gridColumn25");
		this.gridColumn25.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceCell.Font");
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceHeader.Font");
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.FieldName = "وحدة التفقيط";
		this.gridColumn25.MinWidth = 24;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.OptionsColumn.AllowFocus = false;
		this.gridColumn25.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn26, "gridColumn26");
		this.gridColumn26.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceCell.Font");
		this.gridColumn26.AppearanceCell.Options.UseFont = true;
		this.gridColumn26.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceHeader.Font");
		this.gridColumn26.AppearanceHeader.Options.UseFont = true;
		this.gridColumn26.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		repositoryItemTextEdit.Name = "repositoryItemTextEditTafqit";
		this.gridColumn26.ColumnEdit = repositoryItemTextEdit;
		this.gridColumn26.FieldName = "كمية التفقيط";
		this.gridColumn26.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn26.MinWidth = 24;
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
		this.gridColumn26.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn26.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn36, "gridColumn36");
		this.gridColumn36.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceCell.Font");
		this.gridColumn36.AppearanceCell.Options.UseFont = true;
		this.gridColumn36.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn36.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn36.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceHeader.Font");
		this.gridColumn36.AppearanceHeader.Options.UseFont = true;
		this.gridColumn36.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn36.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn36.FieldName = "الصافى";
		this.gridColumn36.MinWidth = 24;
		this.gridColumn36.Name = "gridColumn36";
		this.gridColumn36.OptionsColumn.AllowEdit = false;
		this.gridColumn36.OptionsColumn.AllowFocus = false;
		this.gridColumn36.OptionsColumn.ReadOnly = true;
		this.gridColumn36.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn3.FieldName = "الاجمالى";
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.OptionsColumn.AllowFocus = false;
		this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn6, "gridColumn6");
		this.gridColumn6.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceCell.Font");
		this.gridColumn6.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn6.AppearanceCell.FontStyleDelta");
		this.gridColumn6.AppearanceCell.Options.UseFont = true;
		this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceHeader.Font");
		this.gridColumn6.AppearanceHeader.Options.UseFont = true;
		this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.FieldName = "الضربية";
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.AllowFocus = false;
		this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
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
		this.gridColumn7.FieldName = "إجمالى المبلغ";
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
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
		repositoryItemButtonEdit4.Appearance.Options.UseImage = true;
		resources.ApplyResources(repositoryItemButtonEdit4, "repositoryItemButtonEdit7");
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit7.Buttons"), resources.GetString("repositoryItemButtonEdit7.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit7.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonEdit7.Buttons6"), resources.GetObject("repositoryItemButtonEdit7.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit7.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit7.Buttons9"))
		});
		repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit6";
		repositoryItemButtonEdit4.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.gridColumn8.ColumnEdit = repositoryItemButtonEdit4;
		this.gridColumn8.FieldName = "حذف";
		this.gridColumn8.Name = "gridColumn8";
		resources.ApplyResources(this.gridColumn22, "gridColumn22");
		this.gridColumn22.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceCell.Font");
		this.gridColumn22.AppearanceCell.Options.UseFont = true;
		this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceHeader.Font");
		this.gridColumn22.AppearanceHeader.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.FieldName = "إجمالى_بعدالخصم";
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn22.OptionsColumn.AllowFocus = false;
		this.gridColumn22.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn5, "gridColumn5");
		this.gridColumn5.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceCell.Font");
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceHeader.Font");
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.FieldName = "الضريبه_بعد_الخصم";
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.OptionsColumn.AllowFocus = false;
		resources.ApplyResources(this.gridColumn9, "gridColumn9");
		this.gridColumn9.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceCell.Font");
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceHeader.Font");
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.FieldName = "م";
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceCell.Font");
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.FieldName = "أسم_الوحدة";
		this.gridColumn19.MinWidth = 24;
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowFocus = false;
		resources.ApplyResources(this.gridColumn28, "gridColumn28");
		this.gridColumn28.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn28.AppearanceCell.Font");
		this.gridColumn28.AppearanceCell.Options.UseFont = true;
		this.gridColumn28.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn28.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn28.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn28.AppearanceHeader.Font");
		this.gridColumn28.AppearanceHeader.Options.UseFont = true;
		this.gridColumn28.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn28.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn28.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn28.FieldName = "قيمة الخصم";
		this.gridColumn28.Name = "gridColumn28";
		resources.ApplyResources(this.gridColumn29, "gridColumn29");
		this.gridColumn29.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceCell.Font");
		this.gridColumn29.AppearanceCell.Options.UseFont = true;
		this.gridColumn29.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceHeader.Font");
		this.gridColumn29.AppearanceHeader.Options.UseFont = true;
		this.gridColumn29.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn29.FieldName = "نسبة الخصم";
		this.gridColumn29.Name = "gridColumn29";
		this.gridColumn29.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn30, "gridColumn30");
		this.gridColumn30.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn30.AppearanceCell.Font");
		this.gridColumn30.AppearanceCell.Options.UseFont = true;
		this.gridColumn30.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn30.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn30.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn30.AppearanceHeader.Font");
		this.gridColumn30.AppearanceHeader.Options.UseFont = true;
		this.gridColumn30.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn30.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn30.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn30.FieldName = "البونص";
		this.gridColumn30.Name = "gridColumn30";
		this.gridColumn30.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn31, "gridColumn31");
		this.gridColumn31.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn31.AppearanceCell.Font");
		this.gridColumn31.AppearanceCell.Options.UseFont = true;
		this.gridColumn31.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn31.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn31.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn31.AppearanceHeader.Font");
		this.gridColumn31.AppearanceHeader.Options.UseFont = true;
		this.gridColumn31.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn31.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn31.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn31.FieldName = "الطول";
		this.gridColumn31.Name = "gridColumn31";
		this.gridColumn31.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn32.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn32.FieldName = "العرض";
		this.gridColumn32.Name = "gridColumn32";
		this.gridColumn32.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn37.FieldName = "الربح";
		this.gridColumn37.MinWidth = 24;
		this.gridColumn37.Name = "gridColumn37";
		this.gridColumn37.OptionsColumn.AllowEdit = false;
		this.gridColumn37.OptionsColumn.AllowFocus = false;
		this.gridColumn37.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn33.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn33.FieldName = "الإرتفاع";
		this.gridColumn33.Name = "gridColumn33";
		this.gridColumn33.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn34.FieldName = "الوزن";
		this.gridColumn34.Name = "gridColumn34";
		this.gridColumn34.OptionsColumn.FixedWidth = true;
		this.gridColumn34.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn35, "gridColumn35");
		this.gridColumn35.FieldName = "ItemID";
		this.gridColumn35.Name = "gridColumn35";
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
		resources.ApplyResources(this.CBProject, "CBProject");
		this.CBProject.FormattingEnabled = true;
		this.CBProject.Name = "CBProject";
		this.CBProject.SelectedIndexChanged += new System.EventHandler(CBProject_SelectedIndexChanged);
		this.CBProject.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		this.CBProject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
		resources.ApplyResources(this.TxtVat, "TxtVat");
		this.TxtVat.DecimalDigits = 3;
		this.TxtVat.ForeColor = System.Drawing.Color.Black;
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.ReadOnly = true;
		this.TxtVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4]);
		this.TxtVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.toggleSectoral, "toggleSectoral");
		this.toggleSectoral.Name = "toggleSectoral";
		this.toggleSectoral.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleSectoral.Properties.Appearance.Font");
		this.toggleSectoral.Properties.Appearance.Options.UseFont = true;
		this.toggleSectoral.Properties.Appearance.Options.UseForeColor = true;
		this.toggleSectoral.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleSectoral.Properties.GlyphVerticalAlignment");
		this.toggleSectoral.Properties.OffText = resources.GetString("toggleSectoral.Properties.OffText");
		this.toggleSectoral.Properties.OnText = resources.GetString("toggleSectoral.Properties.OnText");
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		this.label5.Click += new System.EventHandler(label5_Click);
		resources.ApplyResources(this.TxtAllVat, "TxtAllVat");
		this.TxtAllVat.DecimalDigits = 3;
		this.TxtAllVat.ForeColor = System.Drawing.Color.Black;
		this.TxtAllVat.Name = "TxtAllVat";
		this.TxtAllVat.ReadOnly = true;
		this.TxtAllVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllVat.Value = new decimal(new int[4]);
		this.TxtAllVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtTotals, "TxtTotals");
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label30, "label30");
		this.label30.Name = "label30";
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.label27, "label27");
		this.label27.Name = "label27";
		resources.ApplyResources(this.TxtCost, "TxtCost");
		this.TxtCost.DecimalDigits = 3;
		this.TxtCost.ForeColor = System.Drawing.Color.Black;
		this.TxtCost.Name = "TxtCost";
		this.TxtCost.ReadOnly = true;
		this.TxtCost.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCost.Value = new decimal(new int[4]);
		this.TxtCost.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtAllTotal, "TxtAllTotal");
		this.TxtAllTotal.DecimalDigits = 3;
		this.TxtAllTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtAllTotal.Name = "TxtAllTotal";
		this.TxtAllTotal.ReadOnly = true;
		this.TxtAllTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllTotal.Value = new decimal(new int[4]);
		this.TxtAllTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtTabocc, "TxtTabocc");
		this.TxtTabocc.DecimalDigits = 3;
		this.TxtTabocc.ForeColor = System.Drawing.Color.Black;
		this.TxtTabocc.Name = "TxtTabocc";
		this.TxtTabocc.ReadOnly = true;
		this.TxtTabocc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTabocc.Value = new decimal(new int[4]);
		this.TxtTabocc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label39, "label39");
		this.label39.Name = "label39";
		resources.ApplyResources(this.label28, "label28");
		this.label28.Name = "label28";
		resources.ApplyResources(this.label26, "label26");
		this.label26.Name = "label26";
		resources.ApplyResources(this.label29, "label29");
		this.label29.Name = "label29";
		resources.ApplyResources(this.CBCostCenter, "CBCostCenter");
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		this.CBCostCenter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
		resources.ApplyResources(this.BtnPrint, "BtnPrint");
		this.BtnPrint.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrint.Appearance.Font");
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtProfit, "TxtProfit");
		this.TxtProfit.DecimalDigits = 3;
		this.TxtProfit.ForeColor = System.Drawing.Color.Black;
		this.TxtProfit.Name = "TxtProfit";
		this.TxtProfit.ReadOnly = true;
		this.TxtProfit.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtProfit.Value = new decimal(new int[4]);
		this.TxtProfit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNew.Appearance.Font");
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDelete.Appearance.Font");
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtSellPrice3, "TxtSellPrice3");
		this.TxtSellPrice3.DecimalDigits = 3;
		this.TxtSellPrice3.ForeColor = System.Drawing.Color.Black;
		this.TxtSellPrice3.Name = "TxtSellPrice3";
		this.TxtSellPrice3.ReadOnly = true;
		this.TxtSellPrice3.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtSellPrice3.Value = new decimal(new int[4]);
		this.TxtSellPrice3.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtTotalAll, "TxtTotalAll");
		this.TxtTotalAll.DecimalDigits = 3;
		this.TxtTotalAll.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalAll.Name = "TxtTotalAll";
		this.TxtTotalAll.ReadOnly = true;
		this.TxtTotalAll.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalAll.Value = new decimal(new int[4]);
		this.TxtTotalAll.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalAll.TextChanged += new System.EventHandler(TxtTotals_TextChanged);
		this.TxtTotalAll.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtTotalBeforDisc, "TxtTotalBeforDisc");
		this.TxtTotalBeforDisc.DecimalDigits = 3;
		this.TxtTotalBeforDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalBeforDisc.Name = "TxtTotalBeforDisc";
		this.TxtTotalBeforDisc.ReadOnly = true;
		this.TxtTotalBeforDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalBeforDisc.Value = new decimal(new int[4]);
		this.TxtTotalBeforDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label11, "label11");
		this.label11.Name = "label11";
		resources.ApplyResources(this.TxtNet, "TxtNet");
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.ForeColor = System.Drawing.Color.Black;
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet.TextChanged += new System.EventHandler(TxtNet_TextChanged);
		this.TxtNet.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtSellPrice2, "TxtSellPrice2");
		this.TxtSellPrice2.DecimalDigits = 3;
		this.TxtSellPrice2.ForeColor = System.Drawing.Color.Black;
		this.TxtSellPrice2.Name = "TxtSellPrice2";
		this.TxtSellPrice2.ReadOnly = true;
		this.TxtSellPrice2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtSellPrice2.Value = new decimal(new int[4]);
		this.TxtSellPrice2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.TxtSellPrice1, "TxtSellPrice1");
		this.TxtSellPrice1.DecimalDigits = 3;
		this.TxtSellPrice1.ForeColor = System.Drawing.Color.Black;
		this.TxtSellPrice1.Name = "TxtSellPrice1";
		this.TxtSellPrice1.ReadOnly = true;
		this.TxtSellPrice1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtSellPrice1.Value = new decimal(new int[4]);
		this.TxtSellPrice1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.TxtDiscount, "TxtDiscount");
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscount.TextChanged += new System.EventHandler(TxtDiscount_TextChanged);
		this.TxtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtVatBeforeDisc, "TxtVatBeforeDisc");
		this.TxtVatBeforeDisc.DecimalDigits = 3;
		this.TxtVatBeforeDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatBeforeDisc.Name = "TxtVatBeforeDisc";
		this.TxtVatBeforeDisc.ReadOnly = true;
		this.TxtVatBeforeDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatBeforeDisc.Value = new decimal(new int[4]);
		this.TxtVatBeforeDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.LblSup, "LblSup");
		this.LblSup.Name = "LblSup";
		this.LblSup.TextChanged += new System.EventHandler(LblSup_TextChanged);
		resources.ApplyResources(this.TxtDate, "TxtDate");
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDate.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDate.Properties.Appearance.Font");
		this.TxtDate.Properties.Appearance.Options.UseBackColor = true;
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
		this.TxtDate.Properties.Mask.EditMask = resources.GetString("TxtDate.Properties.Mask.EditMask");
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate.TextChanged += new System.EventHandler(DateHold_TextChanged);
		this.TxtDate.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label24, "label24");
		this.label24.Name = "label24";
		resources.ApplyResources(this.TxtNumNote, "TxtNumNote");
		this.TxtNumNote.Name = "TxtNumNote";
		this.TxtNumNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumNote.Properties.Appearance.Font");
		this.TxtNumNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNumNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumNote.EditValueChanged += new System.EventHandler(TxtNumNote_EditValueChanged);
		this.TxtNumNote.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNumNote.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions5");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions6");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"), resources.GetString("repositoryItemButtonEdit3.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit3.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("repositoryItemButtonEdit3.Buttons6"), resources.GetObject("repositoryItemButtonEdit3.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit3.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit3.Buttons9"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions7.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions7, "editorButtonImageOptions7");
		editorButtonImageOptions7.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		resources.ApplyResources(this.label23, "label23");
		this.label23.Name = "label23";
		this.label23.Click += new System.EventHandler(label23_Click);
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNote.Properties.Appearance.Font");
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNote.EditValueChanged += new System.EventHandler(TxtNote_EditValueChanged);
		this.TxtNote.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNote.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtPhone, "TxtPhone");
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPhone.Properties.Appearance.Font");
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.TxtNumVat, "TxtNumVat");
		this.TxtNumVat.Name = "TxtNumVat";
		this.TxtNumVat.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumVat.Properties.Appearance.Font");
		this.TxtNumVat.Properties.Appearance.Options.UseFont = true;
		this.TxtNumVat.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumVat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumVat.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNumVat.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtNumVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.label18, "label18");
		this.label18.Name = "label18";
		resources.ApplyResources(this.label31, "label31");
		this.label31.Name = "label31";
		resources.ApplyResources(this.CBBounce, "CBBounce");
		this.CBBounce.FormattingEnabled = true;
		this.CBBounce.Name = "CBBounce";
		resources.ApplyResources(this.toggleHold, "toggleHold");
		this.toggleHold.Name = "toggleHold";
		this.toggleHold.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleHold.Properties.Appearance.Font");
		this.toggleHold.Properties.Appearance.Options.UseFont = true;
		this.toggleHold.Properties.Appearance.Options.UseForeColor = true;
		this.toggleHold.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleHold.Properties.GlyphVerticalAlignment");
		this.toggleHold.Properties.OffText = resources.GetString("toggleHold.Properties.OffText");
		this.toggleHold.Properties.OnText = resources.GetString("toggleHold.Properties.OnText");
		this.toggleHold.Properties.Toggled += new System.EventHandler(toggleHold_Properties_Toggled);
		this.toggleHold.Toggled += new System.EventHandler(toggleHold_Toggled);
		this.toggleHold.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlTxtSuppliers_KeyDown);
		resources.ApplyResources(this.BtnCustomerSearch, "BtnCustomerSearch");
		this.BtnCustomerSearch.Name = "BtnCustomerSearch";
		this.BtnCustomerSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnCustomerSearch.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCustomerSearch.Properties.Appearance.Font");
		this.BtnCustomerSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnCustomerSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnCustomerSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnCustomerSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnCustomerSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions8");
		this.BtnCustomerSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnCustomerSearch.Properties.Buttons"), resources.GetString("BtnCustomerSearch.Properties.Buttons1"), (int)resources.GetObject("BtnCustomerSearch.Properties.Buttons2"), (bool)resources.GetObject("BtnCustomerSearch.Properties.Buttons3"), (bool)resources.GetObject("BtnCustomerSearch.Properties.Buttons4"), (bool)resources.GetObject("BtnCustomerSearch.Properties.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("BtnCustomerSearch.Properties.Buttons6"), resources.GetObject("BtnCustomerSearch.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("BtnCustomerSearch.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("BtnCustomerSearch.Properties.Buttons9"))
		});
		this.BtnCustomerSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCustomerSearch.Properties.NullText = resources.GetString("BtnCustomerSearch.Properties.NullText");
		this.BtnCustomerSearch.Tag = "بحث بالكود";
		this.BtnCustomerSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnCustomerSearch_ButtonClick);
		this.BtnCustomerSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnCustomerSearch_KeyDown);
		this.BtnCustomerSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.BtnNewCustomer, "BtnNewCustomer");
		this.BtnNewCustomer.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNewCustomer.Appearance.Font");
		this.BtnNewCustomer.Appearance.Options.UseFont = true;
		this.BtnNewCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewCustomer.ImageOptions.Image");
		this.BtnNewCustomer.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewCustomer.Name = "BtnNewCustomer";
		this.BtnNewCustomer.Click += new System.EventHandler(BtnNewCustomer_Click);
		this.BtnNewCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.LBlSuppAccount, "LBlSuppAccount");
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		resources.ApplyResources(this.label20, "label20");
		this.label20.Name = "label20";
		this.label20.Click += new System.EventHandler(label20_Click);
		resources.ApplyResources(this.TxtType, "TxtType");
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Name = "TxtType";
		this.TxtType.SelectedIndexChanged += new System.EventHandler(TxtType_SelectedIndexChanged_1);
		this.TxtType.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
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
		this.TxtSuppliers.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlTxtNumVat_KeyDown);
		resources.ApplyResources(this.label17, "label17");
		this.label17.Name = "label17";
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
		this.TxtCode.EditValueChanged += new System.EventHandler(TxtCode_EditValueChanged);
		this.TxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnOrderSearch, "BtnOrderSearch");
		this.BtnOrderSearch.Name = "BtnOrderSearch";
		this.BtnOrderSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnOrderSearch.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnOrderSearch.Properties.Appearance.Font");
		this.BtnOrderSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnOrderSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnOrderSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnOrderSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnOrderSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		resources.ApplyResources(editorButtonImageOptions9, "editorButtonImageOptions9");
		this.BtnOrderSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnOrderSearch.Properties.Buttons"), resources.GetString("BtnOrderSearch.Properties.Buttons1"), (int)resources.GetObject("BtnOrderSearch.Properties.Buttons2"), (bool)resources.GetObject("BtnOrderSearch.Properties.Buttons3"), (bool)resources.GetObject("BtnOrderSearch.Properties.Buttons4"), (bool)resources.GetObject("BtnOrderSearch.Properties.Buttons5"), editorButtonImageOptions9, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance9, appearanceHovered9, appearancePressed9, appearanceDisabled9, resources.GetString("BtnOrderSearch.Properties.Buttons6"), resources.GetObject("BtnOrderSearch.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("BtnOrderSearch.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("BtnOrderSearch.Properties.Buttons9"))
		});
		this.BtnOrderSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnOrderSearch.Properties.NullText = resources.GetString("BtnOrderSearch.Properties.NullText");
		this.BtnOrderSearch.Tag = "بحث بالكود";
		this.BtnOrderSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnOrderSearch_ButtonClick);
		this.BtnOrderSearch.EditValueChanged += new System.EventHandler(BtnOrderSearch_EditValueChanged);
		this.BtnOrderSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnOrderSearch_KeyDown);
		this.BtnOrderSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.repositoryItemButtonEditCode, "repositoryItemButtonEditCode");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCode.Buttons"))
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
		resources.ApplyResources(this.StripMenuItem1, "StripMenuItem1");
		this.StripMenuItem1.Name = "StripMenuItem1";
		this.StripMenuItem1.Click += new System.EventHandler(StripMenuItem1_Click);
		resources.ApplyResources(this.MenuPurchOrder, "MenuPurchOrder");
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		this.MenuPurchOrder.Click += new System.EventHandler(MenuPurchOrder_Click);
		resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		resources.ApplyResources(this.MenuSearchItem, "MenuSearchItem");
		this.MenuSearchItem.Name = "MenuSearchItem";
		this.MenuSearchItem.Click += new System.EventHandler(MenuSearchItem_Click);
		resources.ApplyResources(this.حركةصنفToolStripMenuItem, "حركةصنفToolStripMenuItem");
		this.حركةصنفToolStripMenuItem.Name = "حركةصنفToolStripMenuItem";
		this.حركةصنفToolStripMenuItem.Click += new System.EventHandler(حركةصنفToolStripMenuItem_Click);
		resources.ApplyResources(this.MenuNewItem, "MenuNewItem");
		this.MenuNewItem.Name = "MenuNewItem";
		this.MenuNewItem.Click += new System.EventHandler(MenuNewItem_Click);
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.MenuPrintFatoura, "MenuPrintFatoura");
		this.MenuPrintFatoura.Name = "MenuPrintFatoura";
		this.MenuPrintFatoura.Click += new System.EventHandler(MenuPrintFatoura_Click);
		resources.ApplyResources(this.MenuPreviewFatoura, "MenuPreviewFatoura");
		this.MenuPreviewFatoura.Name = "MenuPreviewFatoura";
		this.MenuPreviewFatoura.Click += new System.EventHandler(MenuPreviewFatoura_Click);
		resources.ApplyResources(this.MenuPrintQued, "MenuPrintQued");
		this.MenuPrintQued.Name = "MenuPrintQued";
		this.MenuPrintQued.Click += new System.EventHandler(MenuPrintQued_Click);
		resources.ApplyResources(this.طباعةToolStripMenuItem1, "طباعةToolStripMenuItem1");
		this.طباعةToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.MenuPrintQued, this.MenuPreviewFatoura, this.MenuPrintFatoura });
		this.طباعةToolStripMenuItem1.Name = "طباعةToolStripMenuItem1";
		resources.ApplyResources(this.MenuDelete, "MenuDelete");
		this.MenuDelete.Name = "MenuDelete";
		this.MenuDelete.Click += new System.EventHandler(MenuDelete_Click);
		resources.ApplyResources(this.MenuEdit, "MenuEdit");
		this.MenuEdit.Name = "MenuEdit";
		this.MenuEdit.Click += new System.EventHandler(MenuEdit_Click);
		resources.ApplyResources(this.MenuSave, "MenuSave");
		this.MenuSave.Name = "MenuSave";
		this.MenuSave.Click += new System.EventHandler(MenuSave_Click);
		resources.ApplyResources(this.MenuNew, "MenuNew");
		this.MenuNew.Name = "MenuNew";
		this.MenuNew.Click += new System.EventHandler(MenuNew_Click);
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[18]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.طباعةToolStripMenuItem1, this.toolStripSeparator1, this.كشفحسابToolStripMenuItem, this.حركةصنفToolStripMenuItem, this.حركهصنففعليةToolStripMenuItem, this.MenuNewItem,
			this.MenuSearchItem, this.toolStripSeparator4, this.MenuPurchOrder, this.StripMenuItem1, this.toolStripSeparator2, this.تخصيصToolStripMenuItem1, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		resources.ApplyResources(this.كشفحسابToolStripMenuItem, "كشفحسابToolStripMenuItem");
		this.كشفحسابToolStripMenuItem.Name = "كشفحسابToolStripMenuItem";
		this.كشفحسابToolStripMenuItem.Click += new System.EventHandler(كشفحسابToolStripMenuItem_Click);
		resources.ApplyResources(this.حركهصنففعليةToolStripMenuItem, "حركهصنففعليةToolStripMenuItem");
		this.حركهصنففعليةToolStripMenuItem.Name = "حركهصنففعليةToolStripMenuItem";
		this.حركهصنففعليةToolStripMenuItem.Click += new System.EventHandler(حركهصنففعليةToolStripMenuItem_Click);
		this.ribbonPage2.Name = "ribbonPage2";
		resources.ApplyResources(this.ribbonPage2, "ribbonPage2");
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.groupBox6);
		this.groupBox2.Controls.Add(this.groupBox1);
		this.groupBox2.Controls.Add(this.label32);
		this.groupBox2.Controls.Add(this.TxtDiscountALLTotal);
		this.groupBox2.Controls.Add(this.TxtLocation);
		this.groupBox2.Controls.Add(this.label25);
		this.groupBox2.Controls.Add(this.txtdicoutprege);
		this.groupBox2.Controls.Add(this.TxtTafqit);
		this.groupBox2.Controls.Add(this.TxtNet22);
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnOrderReturn);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.BtnRecipt);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.BtnShowPrice);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.TxtTotalAll);
		this.groupBox2.Controls.Add(this.TxtTotalBeforDisc);
		this.groupBox2.Controls.Add(this.TxtNet);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.TxtDiscount);
		this.groupBox2.Controls.Add(this.TxtVatBeforeDisc);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.groupBox6, "groupBox6");
		this.groupBox6.Controls.Add(this.TxtAnotherGroom);
		this.groupBox6.Controls.Add(this.TxtLesar);
		this.groupBox6.Controls.Add(this.TxtScreenAdd);
		this.groupBox6.Controls.Add(this.TxtAddHeadPhones);
		this.groupBox6.Controls.Add(this.TxtTarab);
		this.groupBox6.Controls.Add(this.TxtWedding);
		this.groupBox6.Controls.Add(this.label56);
		this.groupBox6.Controls.Add(this.label57);
		this.groupBox6.Controls.Add(this.TxtCurains);
		this.groupBox6.Controls.Add(this.label60);
		this.groupBox6.Controls.Add(this.label58);
		this.groupBox6.Controls.Add(this.label61);
		this.groupBox6.Controls.Add(this.label62);
		this.groupBox6.Controls.Add(this.label63);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.TabStop = false;
		resources.ApplyResources(this.TxtAnotherGroom, "TxtAnotherGroom");
		this.TxtAnotherGroom.Name = "TxtAnotherGroom";
		resources.ApplyResources(this.TxtLesar, "TxtLesar");
		this.TxtLesar.Name = "TxtLesar";
		resources.ApplyResources(this.TxtScreenAdd, "TxtScreenAdd");
		this.TxtScreenAdd.Name = "TxtScreenAdd";
		resources.ApplyResources(this.TxtAddHeadPhones, "TxtAddHeadPhones");
		this.TxtAddHeadPhones.Name = "TxtAddHeadPhones";
		resources.ApplyResources(this.TxtTarab, "TxtTarab");
		this.TxtTarab.Name = "TxtTarab";
		resources.ApplyResources(this.TxtWedding, "TxtWedding");
		this.TxtWedding.Name = "TxtWedding";
		resources.ApplyResources(this.label56, "label56");
		this.label56.Name = "label56";
		resources.ApplyResources(this.label57, "label57");
		this.label57.Name = "label57";
		resources.ApplyResources(this.TxtCurains, "TxtCurains");
		this.TxtCurains.Name = "TxtCurains";
		resources.ApplyResources(this.label60, "label60");
		this.label60.Name = "label60";
		resources.ApplyResources(this.label58, "label58");
		this.label58.Name = "label58";
		resources.ApplyResources(this.label61, "label61");
		this.label61.Name = "label61";
		resources.ApplyResources(this.label62, "label62");
		this.label62.Name = "label62";
		resources.ApplyResources(this.label63, "label63");
		this.label63.Name = "label63";
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label40);
		this.groupBox1.Controls.Add(this.label41);
		this.groupBox1.Controls.Add(this.label44);
		this.groupBox1.Controls.Add(this.label45);
		this.groupBox1.Controls.Add(this.TxtKosha);
		this.groupBox1.Controls.Add(this.TxtCountCaress);
		this.groupBox1.Controls.Add(this.TxtCookCarecass);
		this.groupBox1.Controls.Add(this.TxtCountWomenTable);
		this.groupBox1.Controls.Add(this.TxtCountWomen);
		this.groupBox1.Controls.Add(this.TxtCountCandles);
		this.groupBox1.Controls.Add(this.TxtCountTableMen);
		this.groupBox1.Controls.Add(this.TxtNumberOfMen);
		this.groupBox1.Controls.Add(this.label47);
		this.groupBox1.Controls.Add(this.TxtCountHeaters);
		this.groupBox1.Controls.Add(this.label49);
		this.groupBox1.Controls.Add(this.TxtTypeCaress);
		this.groupBox1.Controls.Add(this.label50);
		this.groupBox1.Controls.Add(this.label64);
		this.groupBox1.Controls.Add(this.label51);
		this.groupBox1.Controls.Add(this.label52);
		this.groupBox1.Controls.Add(this.TxtBff);
		this.groupBox1.Controls.Add(this.TxtTrombah);
		this.groupBox1.Controls.Add(this.label53);
		this.groupBox1.Controls.Add(this.label54);
		this.groupBox1.Controls.Add(this.label55);
		this.groupBox1.Controls.Add(this.TxtPriceCaress);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.label40, "label40");
		this.label40.Name = "label40";
		resources.ApplyResources(this.label41, "label41");
		this.label41.Name = "label41";
		resources.ApplyResources(this.label44, "label44");
		this.label44.Name = "label44";
		resources.ApplyResources(this.label45, "label45");
		this.label45.Name = "label45";
		resources.ApplyResources(this.TxtKosha, "TxtKosha");
		this.TxtKosha.Name = "TxtKosha";
		resources.ApplyResources(this.TxtCountCaress, "TxtCountCaress");
		this.TxtCountCaress.Name = "TxtCountCaress";
		resources.ApplyResources(this.TxtCookCarecass, "TxtCookCarecass");
		this.TxtCookCarecass.Name = "TxtCookCarecass";
		resources.ApplyResources(this.TxtCountWomenTable, "TxtCountWomenTable");
		this.TxtCountWomenTable.Name = "TxtCountWomenTable";
		resources.ApplyResources(this.TxtCountWomen, "TxtCountWomen");
		this.TxtCountWomen.Name = "TxtCountWomen";
		resources.ApplyResources(this.TxtCountCandles, "TxtCountCandles");
		this.TxtCountCandles.Name = "TxtCountCandles";
		resources.ApplyResources(this.TxtCountTableMen, "TxtCountTableMen");
		this.TxtCountTableMen.Name = "TxtCountTableMen";
		resources.ApplyResources(this.TxtNumberOfMen, "TxtNumberOfMen");
		this.TxtNumberOfMen.Name = "TxtNumberOfMen";
		resources.ApplyResources(this.label47, "label47");
		this.label47.Name = "label47";
		resources.ApplyResources(this.TxtCountHeaters, "TxtCountHeaters");
		this.TxtCountHeaters.Name = "TxtCountHeaters";
		resources.ApplyResources(this.label49, "label49");
		this.label49.Name = "label49";
		resources.ApplyResources(this.TxtTypeCaress, "TxtTypeCaress");
		this.TxtTypeCaress.Name = "TxtTypeCaress";
		resources.ApplyResources(this.label50, "label50");
		this.label50.Name = "label50";
		resources.ApplyResources(this.label64, "label64");
		this.label64.Name = "label64";
		resources.ApplyResources(this.label51, "label51");
		this.label51.Name = "label51";
		resources.ApplyResources(this.label52, "label52");
		this.label52.Name = "label52";
		resources.ApplyResources(this.TxtBff, "TxtBff");
		this.TxtBff.Name = "TxtBff";
		resources.ApplyResources(this.TxtTrombah, "TxtTrombah");
		this.TxtTrombah.Name = "TxtTrombah";
		resources.ApplyResources(this.label53, "label53");
		this.label53.Name = "label53";
		resources.ApplyResources(this.label54, "label54");
		this.label54.Name = "label54";
		resources.ApplyResources(this.label55, "label55");
		this.label55.Name = "label55";
		resources.ApplyResources(this.TxtPriceCaress, "TxtPriceCaress");
		this.TxtPriceCaress.ControlStyle.HoverBaseColor = System.Drawing.SystemColors.ControlDark;
		this.TxtPriceCaress.DecimalDigits = 3;
		this.TxtPriceCaress.FlatBorderColor = System.Drawing.SystemColors.ScrollBar;
		this.TxtPriceCaress.ForeColor = System.Drawing.Color.Black;
		this.TxtPriceCaress.Name = "TxtPriceCaress";
		this.TxtPriceCaress.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtPriceCaress.Value = new decimal(new int[4]);
		this.TxtPriceCaress.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtPriceCaress.TextChanged += new System.EventHandler(TxtDiscount_TextChanged);
		this.TxtPriceCaress.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label32, "label32");
		this.label32.Name = "label32";
		resources.ApplyResources(this.TxtDiscountALLTotal, "TxtDiscountALLTotal");
		this.TxtDiscountALLTotal.DecimalDigits = 3;
		this.TxtDiscountALLTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscountALLTotal.Name = "TxtDiscountALLTotal";
		this.TxtDiscountALLTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscountALLTotal.Value = new decimal(new int[4]);
		this.TxtDiscountALLTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscountALLTotal.TextChanged += new System.EventHandler(TxtDiscountALLTotal_TextChanged);
		resources.ApplyResources(this.BtnOrderReturn, "BtnOrderReturn");
		this.BtnOrderReturn.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnOrderReturn.Appearance.Font");
		this.BtnOrderReturn.Appearance.Options.UseFont = true;
		this.BtnOrderReturn.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOrderReturn.ImageOptions.Image");
		this.BtnOrderReturn.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnOrderReturn.Name = "BtnOrderReturn";
		this.BtnOrderReturn.Click += new System.EventHandler(BtnOrderReturn_Click);
		this.BtnOrderReturn.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnRecipt, "BtnRecipt");
		this.BtnRecipt.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnRecipt.Appearance.Font");
		this.BtnRecipt.Appearance.Options.UseFont = true;
		this.BtnRecipt.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRecipt.ImageOptions.Image");
		this.BtnRecipt.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnRecipt.Name = "BtnRecipt";
		this.BtnRecipt.Click += new System.EventHandler(BtnRecipt_Click);
		this.BtnRecipt.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnShowPrice, "BtnShowPrice");
		this.BtnShowPrice.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnShowPrice.Appearance.Font");
		this.BtnShowPrice.Appearance.Options.UseFont = true;
		this.BtnShowPrice.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnShowPrice.ImageOptions.Image");
		this.BtnShowPrice.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnShowPrice.Name = "BtnShowPrice";
		this.BtnShowPrice.Click += new System.EventHandler(BtnShowPrice_Click);
		this.BtnShowPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtAddCappuccino, "TxtAddCappuccino");
		this.TxtAddCappuccino.BackColor = System.Drawing.Color.White;
		this.TxtAddCappuccino.Name = "TxtAddCappuccino";
		resources.ApplyResources(this.TxtHasmPer, "TxtHasmPer");
		this.TxtHasmPer.DecimalDigits = 3;
		this.TxtHasmPer.ForeColor = System.Drawing.Color.Black;
		this.TxtHasmPer.Name = "TxtHasmPer";
		this.TxtHasmPer.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtHasmPer.Value = new decimal(new int[4]);
		this.TxtHasmPer.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtHasmPer.TextChanged += new System.EventHandler(TxtHasmPer_TextChanged);
		this.TxtHasmPer.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.LblPerHasm, "LblPerHasm");
		this.LblPerHasm.Name = "LblPerHasm";
		resources.ApplyResources(this.LblHasm, "LblHasm");
		this.LblHasm.ForeColor = System.Drawing.Color.Brown;
		this.LblHasm.Name = "LblHasm";
		resources.ApplyResources(this.LblAmountHasm, "LblAmountHasm");
		this.LblAmountHasm.Name = "LblAmountHasm";
		resources.ApplyResources(this.TxtHasmAmount, "TxtHasmAmount");
		this.TxtHasmAmount.DecimalDigits = 3;
		this.TxtHasmAmount.ForeColor = System.Drawing.Color.Black;
		this.TxtHasmAmount.Name = "TxtHasmAmount";
		this.TxtHasmAmount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtHasmAmount.Value = new decimal(new int[4]);
		this.TxtHasmAmount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtHasmAmount.TextChanged += new System.EventHandler(TxtHasmAmount_TextChanged);
		this.TxtHasmAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.groupBox4, "groupBox4");
		this.groupBox4.Controls.Add(this.label59);
		this.groupBox4.Controls.Add(this.BtnCustomerSearch);
		this.groupBox4.Controls.Add(this.TxtAddCappuccino);
		this.groupBox4.Controls.Add(this.BtnNewCustomer);
		this.groupBox4.Controls.Add(this.label43);
		this.groupBox4.Controls.Add(this.TxtNote);
		this.groupBox4.Controls.Add(this.LBlSuppAccount);
		this.groupBox4.Controls.Add(this.LblSup);
		this.groupBox4.Controls.Add(this.label46);
		this.groupBox4.Controls.Add(this.TxtPhone);
		this.groupBox4.Controls.Add(this.TxtSuppliers);
		this.groupBox4.Controls.Add(this.TxtNumVat);
		this.groupBox4.Controls.Add(this.label18);
		this.groupBox4.Controls.Add(this.label23);
		this.groupBox4.Controls.Add(this.TxtCharge);
		this.groupBox4.Controls.Add(this.label37);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.TabStop = false;
		this.groupBox4.Enter += new System.EventHandler(groupBox4_Enter);
		resources.ApplyResources(this.label59, "label59");
		this.label59.Name = "label59";
		resources.ApplyResources(this.label43, "label43");
		this.label43.Name = "label43";
		resources.ApplyResources(this.label46, "label46");
		this.label46.Name = "label46";
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
		resources.ApplyResources(this.TxtAdditionalNum, "TxtAdditionalNum");
		this.TxtAdditionalNum.Name = "TxtAdditionalNum";
		this.TxtAdditionalNum.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAdditionalNum.Properties.Appearance.Font");
		this.TxtAdditionalNum.Properties.Appearance.Options.UseFont = true;
		this.TxtAdditionalNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAdditionalNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAdditionalNum.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtAdditionalNum.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtAdditionalNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtStreet, "TxtStreet");
		this.TxtStreet.Name = "TxtStreet";
		this.TxtStreet.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtStreet.Properties.Appearance.Font");
		this.TxtStreet.Properties.Appearance.Options.UseFont = true;
		this.TxtStreet.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtStreet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtStreet.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtStreet.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtStreet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtDistrict, "TxtDistrict");
		this.TxtDistrict.Name = "TxtDistrict";
		this.TxtDistrict.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDistrict.Properties.Appearance.Font");
		this.TxtDistrict.Properties.Appearance.Options.UseFont = true;
		this.TxtDistrict.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDistrict.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDistrict.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtDistrict.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtDistrict.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtCommercialRecord, "TxtCommercialRecord");
		this.TxtCommercialRecord.Name = "TxtCommercialRecord";
		this.TxtCommercialRecord.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCommercialRecord.Properties.Appearance.Font");
		this.TxtCommercialRecord.Properties.Appearance.Options.UseFont = true;
		this.TxtCommercialRecord.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCommercialRecord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCommercialRecord.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtCommercialRecord.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtCommercialRecord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtPostalCode, "TxtPostalCode");
		this.TxtPostalCode.Name = "TxtPostalCode";
		this.TxtPostalCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPostalCode.Properties.Appearance.Font");
		this.TxtPostalCode.Properties.Appearance.Options.UseFont = true;
		this.TxtPostalCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPostalCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPostalCode.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtPostalCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtPostalCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtCountry, "TxtCountry");
		this.TxtCountry.Name = "TxtCountry";
		this.TxtCountry.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCountry.Properties.Appearance.Font");
		this.TxtCountry.Properties.Appearance.Options.UseFont = true;
		this.TxtCountry.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCountry.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCountry.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtCountry.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtCountry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtCity, "TxtCity");
		this.TxtCity.Name = "TxtCity";
		this.TxtCity.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCity.Properties.Appearance.Font");
		this.TxtCity.Properties.Appearance.Options.UseFont = true;
		this.TxtCity.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCity.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtCity.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtCity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.label15, "label15");
		this.label15.Name = "label15";
		resources.ApplyResources(this.TxtSales, "TxtSales");
		this.TxtSales.Name = "TxtSales";
		this.TxtSales.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtSales.Properties.Appearance.Font");
		this.TxtSales.Properties.Appearance.Options.UseFont = true;
		this.TxtSales.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSales.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSales.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtSales.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtSales.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.groupBox5, "groupBox5");
		this.groupBox5.Controls.Add(this.BtnReportHold);
		this.groupBox5.Controls.Add(this.simpleButton1);
		this.groupBox5.Controls.Add(this.label48);
		this.groupBox5.Controls.Add(this.TxtNumNote);
		this.groupBox5.Controls.Add(this.toggleHold);
		this.groupBox5.Controls.Add(this.label17);
		this.groupBox5.Controls.Add(this.TxtCode);
		this.groupBox5.Controls.Add(this.BtnOrderSearch);
		this.groupBox5.Controls.Add(this.DateHold);
		this.groupBox5.Controls.Add(this.TxtDate);
		this.groupBox5.Controls.Add(this.LblDataHold);
		this.groupBox5.Controls.Add(this.LblCustLast);
		this.groupBox5.Controls.Add(this.label21);
		this.groupBox5.Controls.Add(this.TxtType);
		this.groupBox5.Controls.Add(this.label20);
		this.groupBox5.Controls.Add(this.label24);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.TabStop = false;
		this.groupBox5.Enter += new System.EventHandler(groupBox5_Enter);
		resources.ApplyResources(this.BtnReportHold, "BtnReportHold");
		this.BtnReportHold.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnReportHold.Appearance.Font");
		this.BtnReportHold.Appearance.Options.UseFont = true;
		this.BtnReportHold.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnReportHold.Name = "BtnReportHold";
		this.BtnReportHold.Click += new System.EventHandler(BtnReportHold_Click);
		resources.ApplyResources(this.simpleButton1, "simpleButton1");
		this.simpleButton1.Appearance.Font = (System.Drawing.Font)resources.GetObject("simpleButton1.Appearance.Font");
		this.simpleButton1.Appearance.Options.UseFont = true;
		this.simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.simpleButton1.Name = "simpleButton1";
		this.simpleButton1.Click += new System.EventHandler(simpleButton1_Click);
		resources.ApplyResources(this.label48, "label48");
		this.label48.Name = "label48";
		this.label48.TextChanged += new System.EventHandler(LblSup_TextChanged);
		resources.ApplyResources(this.DateHold, "DateHold");
		this.DateHold.Name = "DateHold";
		this.DateHold.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.DateHold.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.DateHold.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("DateHold.Properties.Appearance.Font");
		this.DateHold.Properties.Appearance.Options.UseBackColor = true;
		this.DateHold.Properties.Appearance.Options.UseFont = true;
		this.DateHold.Properties.Appearance.Options.UseTextOptions = true;
		this.DateHold.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.DateHold.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("DateHold.Properties.Buttons"))
		});
		this.DateHold.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("DateHold.Properties.CalendarTimeProperties.Buttons"))
		});
		this.DateHold.Properties.Mask.EditMask = resources.GetString("DateHold.Properties.Mask.EditMask");
		this.DateHold.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("DateHold.Properties.Mask.UseMaskAsDisplayFormat");
		this.DateHold.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.LblDataHold, "LblDataHold");
		this.LblDataHold.Name = "LblDataHold";
		resources.ApplyResources(this.LblCustLast, "LblCustLast");
		this.LblCustLast.Name = "LblCustLast";
		resources.ApplyResources(this.label36, "label36");
		this.label36.Name = "label36";
		resources.ApplyResources(this.Comp_Store, "Comp_Store");
		this.Comp_Store.FormattingEnabled = true;
		this.Comp_Store.Name = "Comp_Store";
		resources.ApplyResources(this.TxtCharge, "TxtCharge");
		this.TxtCharge.Name = "TxtCharge";
		this.TxtCharge.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCharge.Properties.Appearance.Font");
		this.TxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCharge.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtCharge.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtCharge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.label37, "label37");
		this.label37.Name = "label37";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.label31);
		base.Controls.Add(this.TxtSales);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.Comp_Store);
		base.Controls.Add(this.label30);
		base.Controls.Add(this.label36);
		base.Controls.Add(this.CBBounce);
		base.Controls.Add(this.TxtHasmPer);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.LblNowQuintity);
		base.Controls.Add(this.label27);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.TxtCost);
		base.Controls.Add(this.LblHasm);
		base.Controls.Add(this.TxtAllTotal);
		base.Controls.Add(this.TxtHasmAmount);
		base.Controls.Add(this.TxtTabocc);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.label28);
		base.Controls.Add(this.TxtSellPrice1);
		base.Controls.Add(this.TxtQuntity);
		base.Controls.Add(this.label29);
		base.Controls.Add(this.LblAmountHasm);
		base.Controls.Add(this.label16);
		base.Controls.Add(this.TxtSellPrice2);
		base.Controls.Add(this.label9);
		base.Controls.Add(this.LblPerHasm);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.TxtAllQuantity);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.TxtSellPrice3);
		base.Controls.Add(this.TxtLastCost);
		base.Controls.Add(this.TxtProfit);
		base.Controls.Add(this.TxtAvergCost);
		base.Controls.Add(this.label10);
		base.Controls.Add(this.label26);
		base.Controls.Add(this.label39);
		base.Controls.Add(this.CBCostCenter);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.TxtVat);
		base.Controls.Add(this.TxtTotals);
		base.Controls.Add(this.TxtAllVat);
		base.Controls.Add(this.label22);
		base.Controls.Add(this.label34);
		base.Controls.Add(this.label38);
		base.Controls.Add(this.label35);
		base.Controls.Add(this.label19);
		base.Controls.Add(this.TxtAdditionalNum);
		base.Controls.Add(this.TxtStreet);
		base.Controls.Add(this.TxtDistrict);
		base.Controls.Add(this.TxtCommercialRecord);
		base.Controls.Add(this.TxtPostalCode);
		base.Controls.Add(this.label33);
		base.Controls.Add(this.label42);
		base.Controls.Add(this.TxtCountry);
		base.Controls.Add(this.TxtCity);
		base.Controls.Add(this.toggleSectoral);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmOrdersWedding";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmOrders_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)repositoryItemTextEdit).EndInit();
		((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleSectoral.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleHold.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtAdditionalNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtStreet.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDistrict.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCountry.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales.Properties).EndInit();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
