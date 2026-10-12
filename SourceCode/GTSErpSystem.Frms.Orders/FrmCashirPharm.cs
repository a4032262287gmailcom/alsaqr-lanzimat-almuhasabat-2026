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
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmCachMoney;
using GTSErpSystem.Frms.Orders.FrmLuckUpOrders;
using GTSErpSystem.Frms.Orders.FrmOpenDay;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Helper;
using GTSErpSystem.Properties;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Orders;

public class FrmCashirPharm : XtraForm
{
	private GTSdbEntities db = new GTSdbEntities();

	private Class_Items ITM = new Class_Items();

	private DataTable datatable = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	private DataTable CashierPrint = new DataTable();

	private DataSetALL ST = new DataSetALL();

	private Calss_CostCenter Cost = new Calss_CostCenter();

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

	public decimal Cash;

	public decimal Bank;

	public decimal MoneyNeed;

	public decimal MoneyPaying;

	public decimal MoneyNet;

	private int DecimalNum = LoginDetails.DecimalNum;

	private bool IsClosingOk = true;

	private static int Code = 0;

	private IContainer components = null;

	private NumericEditBox TxtNet22;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn shearch;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private GridColumn gridColumn19;

	private GridColumn gridColumn4;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private GridColumn gridColumn9;

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

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private GroupBox groupBox1;

	private Label LblSup;

	private DateEdit TxtDate;

	private Label label21;

	private Label label20;

	private System.Windows.Forms.ComboBox TxtType;

	private Label label18;

	private ButtonEdit TxtSuppliers;

	private Label LBlSuppAccount;

	private NumericEditBox TxtTotalAll;

	private Label label2;

	private Label label3;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtTotals;

	private Label label5;

	private Label label6;

	private SimpleButton BtnMinus;

	private SimpleButton BtnPlus;

	private ButtonEdit TxtBarcode;

	private GridColumn gridColumn22;

	private GridColumn gridColumn5;

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

	private ToolStripMenuItem RecoveredMenuItemFix1;

	private ToolStripMenuItem MenuPrintQued;

	private ToolStripMenuItem MenuPreviewFatoura;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuNewItem;

	private ToolStripMenuItem RecoveredMenuItemFix2;

	private ToolStripMenuItem MenuSearchItem;

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

	private GridColumn gridColumn23;

	private GridColumn gridColumn24;

	private Label label4;

	private GridColumn gridColumn25;

	private NumericEditBox TxtCost;

	private Label label9;

	private Label label10;

	private GridColumn gridColumn26;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4Units;

	private SimpleButton BtnReturnCashier;

	private GridColumn gridColumn27;

	private NumericEditBox TxtAllVat;

	private Label label12;

	private NumericEditBox TxtTabocc;

	private Label label11;

	private Label label13;

	private NumericEditBox TxtAllTotal;

	private NumericEditBox TxtDiscount;

	private ButtonEdit TxtCode;

	private NumericEditBox txtdicoutprege;

	private ToolStripMenuItem RecoveredMenuItemFix12;

	private ToolStripMenuItem RecoveredMenuItemFix13;

	private ToolStripMenuItem RecoveredMenuItemFix14;

	private ToolStripMenuItem RecoveredMenuItemFix15;

	private ToolStripMenuItem RecoveredMenuItemFix16;

	private ToolStripMenuItem RecoveredMenuItemFix17;

	private ToolStripMenuItem ctrlToolStripMenuItem;

	private ToolStripMenuItem RecoveredMenuItemFix18;

	private ToolStripMenuItem RecoveredMenuItemFix19;

	private ToolStripMenuItem RecoveredMenuItemFix20;

	private Label label14;

	private Label label15;

	private ToolStripMenuItem RecoveredMenuItemFix21;

	public FrmCashirPharm()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
		}
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		InitializeComponent();
		TxtDigital();
		SetInitialRow();
		SetInitialRowPrinter();
		BindComboBox();
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		SetDateCasheir();
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		TxtSuppliers.Text = "عميل نقدي";
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtBarcode.Focus();
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
						decimal num = Convert.ToDecimal(row["سعر الوحدة"].ToString());
						if (row["أسم_الوحدة"].ToString() == "الصغرى")
						{
							row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]), DecimalNum);
							row["الاجمالى"] = Math.Round(Convert.ToDecimal(num * Convert.ToDecimal(value)), DecimalNum);
							if (ORD.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result.Is_Tax == true)
								{
									decimal value2 = num * Convert.ToDecimal(value);
									decimal? tax_Value = get_Item_ByCodeALL_Result.Tax_Value;
									row["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
									decimal value3 = num * Convert.ToDecimal(value);
									decimal? tax_Value2 = get_Item_ByCodeALL_Result.Tax_Value;
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
									row["هل_ضريبة"] = 1;
									row["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
								}
								else
								{
									row["الضربية"] = 0;
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
									row["هل_ضريبة"] = 0;
									row["قيمة_الضريبة"] = 0;
								}
							}
							else
							{
								row["الضربية"] = 0;
								row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
								row["هل_ضريبة"] = 0;
								row["قيمة_الضريبة"] = 0;
							}
							break;
						}
						if (row["أسم_الوحدة"].ToString() == "المتوسطة")
						{
							decimal num2 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertMediumUnit);
							row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * (Convert.ToDecimal(row["الكمية"]) * num2), DecimalNum);
							row["الاجمالى"] = Math.Round(Convert.ToDecimal(num * Convert.ToDecimal(value)), DecimalNum);
							if (ORD.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result.Is_Tax == true)
								{
									decimal value2 = num * Convert.ToDecimal(value);
									decimal? tax_Value = get_Item_ByCodeALL_Result.Tax_Value;
									row["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
									decimal value3 = num * Convert.ToDecimal(value);
									decimal? tax_Value2 = get_Item_ByCodeALL_Result.Tax_Value;
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
									row["هل_ضريبة"] = 1;
									row["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
								}
								else
								{
									row["هل_ضريبة"] = 0;
									row["قيمة_الضريبة"] = 0;
									row["الضربية"] = 0;
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
								}
							}
							else
							{
								row["هل_ضريبة"] = 0;
								row["قيمة_الضريبة"] = 0;
								row["الضربية"] = 0;
								row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
							}
							break;
						}
						if (!(row["أسم_الوحدة"].ToString() == "الكبرى"))
						{
							continue;
						}
						decimal num3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertBigUnit);
						row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * (Convert.ToDecimal(row["الكمية"]) * num3), DecimalNum);
						row["الاجمالى"] = Math.Round(Convert.ToDecimal(num * Convert.ToDecimal(value)), DecimalNum);
						if (ORD.IsVatActive())
						{
							if (get_Item_ByCodeALL_Result.Is_Tax == true)
							{
								decimal value2 = num * Convert.ToDecimal(value);
								decimal? tax_Value = get_Item_ByCodeALL_Result.Tax_Value;
								row["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
								decimal value3 = num * Convert.ToDecimal(value);
								decimal? tax_Value2 = get_Item_ByCodeALL_Result.Tax_Value;
								row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
								row["هل_ضريبة"] = 1;
								row["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
							}
							else
							{
								row["هل_ضريبة"] = 0;
								row["قيمة_الضريبة"] = 0;
								row["الضربية"] = 0;
								row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
							}
						}
						else
						{
							row["هل_ضريبة"] = 0;
							row["قيمة_الضريبة"] = 0;
							row["الضربية"] = 0;
							row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
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
			if (!(e.Column.FieldName == "سعر الوحدة"))
			{
				return;
			}
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
							row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8)), DecimalNum);
							if (ORD.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result2.Is_Tax == true)
								{
									decimal value2 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8);
									decimal? tax_Value = get_Item_ByCodeALL_Result2.Tax_Value;
									row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
									decimal value3 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8);
									decimal? tax_Value2 = get_Item_ByCodeALL_Result2.Tax_Value;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8))), DecimalNum);
									row2["هل_ضريبة"] = 1;
									row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result2.Tax_Value;
								}
								else
								{
									row2["هل_ضريبة"] = 0;
									row2["قيمة_الضريبة"] = 0;
									row2["الضربية"] = 0;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8))), DecimalNum);
								}
							}
							else
							{
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8))), DecimalNum);
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
							row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8)), DecimalNum);
							if (ORD.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result2.Is_Tax == true)
								{
									decimal value2 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8);
									decimal? tax_Value = get_Item_ByCodeALL_Result2.Tax_Value;
									row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
									decimal value3 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8);
									decimal? tax_Value2 = get_Item_ByCodeALL_Result2.Tax_Value;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8))), DecimalNum);
									row2["هل_ضريبة"] = 1;
									row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result2.Tax_Value;
								}
								else
								{
									row2["هل_ضريبة"] = 0;
									row2["قيمة_الضريبة"] = 0;
									row2["الضربية"] = 0;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8))), DecimalNum);
								}
							}
							else
							{
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8))), DecimalNum);
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
						row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8)), DecimalNum);
						if (ORD.IsVatActive())
						{
							if (get_Item_ByCodeALL_Result2.Is_Tax == true)
							{
								row2["هل_ضريبة"] = 1;
								row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result2.Tax_Value;
								decimal value2 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8);
								decimal? tax_Value = get_Item_ByCodeALL_Result2.Tax_Value;
								row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
								decimal value3 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8);
								decimal? tax_Value2 = get_Item_ByCodeALL_Result2.Tax_Value;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8))), DecimalNum);
							}
							else
							{
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8))), DecimalNum);
							}
						}
						else
						{
							row2["هل_ضريبة"] = 0;
							row2["قيمة_الضريبة"] = 0;
							row2["الضربية"] = 0;
							row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText8))), DecimalNum);
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
		ArrangeDatatable();
		Cal_FinalResultTotals();
	}

	private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
	{
		string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
		string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
		string StoreName = gridView1.GetFocusedRowCellDisplayText("المخزن");
		int num = (from x in db.Account_Stores
			where x.Store_Name == StoreName
			select x.ID).FirstOrDefault();
		CodeItm = focusedRowCellDisplayText;
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
							if (row["كود_الصنف"].ToString() == Convert.ToString(itemCode))
							{
								row["الوحدة"] = get_Item_ByCodeALL_Result.UnitSmall;
								row["أسم_الوحدة"] = "الصغرى";
								row["المخزن"] = ORD.GetStoreName();
								row["رقم المخزن"] = ORD.GetStoreID();
								row["الكمية"] = 1;
								row["سعر الوحدة"] = get_Item_ByCodeALL_Result.SellPriceSmall;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1)), DecimalNum);
								row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]), DecimalNum);
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
							if (row2["كود_الصنف"].ToString() == Convert.ToString(itemCode))
							{
								row2["الوحدة"] = get_Item_ByCodeALL_Result2.UnitMedium;
								row2["أسم_الوحدة"] = "المتوسطة";
								row2["الكمية"] = 1;
								row2["المخزن"] = ORD.GetStoreName();
								row2["رقم المخزن"] = ORD.GetStoreID();
								row2["سعر الوحدة"] = get_Item_ByCodeALL_Result2.SellPriceMedium;
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium * (decimal?)Convert.ToInt32(1)), DecimalNum);
								decimal num4 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.ConvertMediumUnit);
								row2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row2["متوسط_التكلفة"]) * (Convert.ToDecimal(row2["الكمية"]) * num4), DecimalNum);
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
							if (row3["كود_الصنف"].ToString() == Convert.ToString(itemCode))
							{
								row3["الوحدة"] = get_Item_ByCodeALL_Result3.UnitLarge;
								row3["أسم_الوحدة"] = "الكبرى";
								row3["الكمية"] = 1;
								row3["المخزن"] = ORD.GetStoreName();
								row3["رقم المخزن"] = ORD.GetStoreID();
								row3["سعر الوحدة"] = get_Item_ByCodeALL_Result3.SellpriceLarge;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge * (decimal?)Convert.ToInt32(1)), DecimalNum);
								decimal num7 = Convert.ToDecimal(get_Item_ByCodeALL_Result3.ConvertBigUnit);
								row3["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row3["متوسط_التكلفة"]) * (Convert.ToDecimal(row3["الكمية"]) * num7), DecimalNum);
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
		GridMinus();
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
					int num2 = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = ORD.GetItemsByitemid(num2).FirstOrDefault();
					Printer printer = ORD.GetPrinter(num2);
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
					dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
					dataRow["PrinterID"] = printer.ID;
					dataRow["PrinterName"] = printer.Name;
					dataRow["كود_الصنف"] = item_Items.Item_code;
					dataRow["أسم الصنف"] = item_Items.item_Name;
					dataRow["الاسم الانجليزي"] = item_Items.item_Name_English;
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
				PrintID = custCode;
				TxtType.SelectedValue = order_Orders.Order_Paymant_Type;
				TxtCode.Text = order_Orders.PurBranchID.ToString();
				Code = Convert.ToInt32(order_Orders.PurBranchID);
				LblSup.Text = order_Orders.SupplierID.ToString();
				TxtSuppliers.Text = order_Orders.SupplierName;
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
				BtnEdit.Enabled = true;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = true;
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = true;
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
					where x.CustSuppCode == FrmID && x.FrmCust == true
					select x).FirstOrDefault();
				LblSup.Text = FrmID.ToString();
				TxtSuppliers.Text = account_CustSup.CustSuppName;
			}
		}
		catch (Exception)
		{
			Clear();
		}
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
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void GridPlus()
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (!(row["ظ…"].ToString() == Convert.ToString(num)))
			{
				continue;
			}
			row["الكمية"] = Convert.ToDecimal(row["الكمية"]) + 1m;
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
		TxtBarcode.Focus();
	}

	private void GridMinus()
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (!(row["ظ…"].ToString() == Convert.ToString(num)))
			{
				continue;
			}
			row["الكمية"] = Convert.ToDecimal(row["الكمية"]) - 1m;
			if (Convert.ToDecimal(row["الكمية"]) <= 0m)
			{
				row["الكمية"] = 1;
			}
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
					if (row["ظ…"].ToString() == Convert.ToString(num))
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
			if (row2["ظ…"].ToString() == Convert.ToString(num2))
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
		datatable.Rows.Clear();
		dtCurrentTable.Rows.Clear();
		gridControl1.DataSource = datatable;
		GetTotalCost();
		TxtSuppliers.Text = "";
		TxtType.Text = "";
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
		TxtDiscount.Enabled = true;
		TxtType.Enabled = true;
		TxtDate.Enabled = true;
		TxtDate.DateTime = DateTime.Now;
		TxtBarcode.Focus();
		SetDateCasheir();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 12))
		{
			int purchese_ID = 0;
			if (datatable.Rows.Count >= 1)
			{
				if (TxtSuppliers.Text != "")
				{
					if (TxtType.Text != "")
					{
						if (TxtType.Text == "آجلة" && LblSup.Text == "1")
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
								MoneyNet = Convert.ToDecimal(frmMoneyCashier.TxtMoneyNet.Text);
							}
							else
							{
								Cash = 0m;
								Bank = 0m;
								MoneyNeed = 0m;
								MoneyPaying = 0m;
								MoneyNet = 0m;
							}
							int costCentersID = Convert.ToInt32(Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID);
							FilterDataTableSave(datatable, purchese_ID, IsWait: false);
							purchese_ID = ORD.SaveOrders(Convert.ToInt32(LblSup.Text), IsWaiting: false, TxtSuppliers.Text, "", "", TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), "", 1, costCentersID, 0, "", "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, OrderCashierType: false, 0, 0, dtCurrentTable, 1, LoginDetails.UserName, Convert.ToDateTime("01-01-1970"), "", "", "", "", "", "", "", "", 0m, 0m, 0m, 1, 0m, 0m, "", 0m, 0m, "", "", "", "", "", "");
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
							}
							else
							{
								MessageBox.Show("تم حفظ بيانات فاتورة الكاشير رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
			if (TxtType.Text == "آجلة" && LblSup.Text == "1")
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
						MoneyNet = Convert.ToDecimal(frmMoneyCashier.TxtMoneyNet.Text);
					}
					else
					{
						Cash = 0m;
						Bank = 0m;
						MoneyNeed = 0m;
						MoneyPaying = 0m;
						MoneyNet = 0m;
					}
					int costCentersID = Convert.ToInt32(Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID);
					FilterDataTableSave(datatable, purchese_ID, IsWait: false);
					purchese_ID = ORD.UpdateOrders(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), IsWaiting: false, TxtSuppliers.Text, "", "", TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), "", 1, costCentersID, 0, "", "", Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Cash, Bank, OrderCashierType: false, 0, 0, dtCurrentTable, LoginDetails.UserName, Convert.ToDateTime("01-01-1970"), "", "", "", "", "", "", "", "", 0m, 0m, 0m, 0m, 0m, "", "", "", "", "", "", 1);
					if (purchese_ID > 0)
					{
						PrintID = purchese_ID;
						Code = purchese_ID;
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
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
						Clear();
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات فاتورة الكاشير رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
					break;
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
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashir.mrt");
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
									stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirSecond.mrt");
									stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
									stiReport.Dictionary.Synchronize();
									stiReport.Compile();
									stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
									stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
									stiReport["VatNumComp"] = LoginDetails.VatNum;
									stiReport["MobileComp"] = LoginDetails.Mobile;
									stiReport["AddressComp"] = LoginDetails.Address;
									stiReport["PrintComp"] = LoginDetails.PrintComp;
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
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashir.mrt");
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
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashir.mrt");
						stiReport.RegData(ST.Order_PurchasesTablePrint);
						stiReport.Dictionary.Synchronize();
						stiReport.Compile();
						stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
						stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
						stiReport["VatNumComp"] = LoginDetails.VatNum;
						stiReport["Date"] = TxtDate.DateTime;
						stiReport["PurchID"] = Code;
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
		BtnEdit.Text = "تعديل F6";
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
		datatable.Columns.Add("الاجمالى", typeof(string));
		datatable.Columns.Add("الضربية", typeof(decimal));
		datatable.Columns.Add("الضريبه_بعدالخصم", typeof(decimal));
		datatable.Columns.Add("إجمالى المبلغ", typeof(string));
		datatable.Columns.Add("حذف الصنف", typeof(int));
		datatable.Columns.Add("ظ…", typeof(int));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تم", typeof(int));
		datatable.Columns.Add("PrinterID", typeof(int));
		datatable.Columns.Add("PrinterName", typeof(string));
		dtCurrentTable.Columns.Add("SN", typeof(int));
		dtCurrentTable.Columns.Add("Purchese_ID", typeof(int));
		dtCurrentTable.Columns.Add("ItemNote", typeof(string));
		dtCurrentTable.Columns.Add("ItemID", typeof(string));
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
		dtCurrentTable.Columns.Add("IsPrintCook", typeof(bool));
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
		object summaryValue = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
		TxtVat.Text = Convert.ToString(summaryValue);
		object summaryValue2 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
		TxtTotals.Text = Convert.ToString(summaryValue2);
		TxtNet22.Text = (Convert.ToDecimal(TxtVat.Text) + Convert.ToDecimal(TxtTotals.Text)).ToString();
		TxtNet.Text = (Convert.ToDecimal(TxtVatBeforeDisc.Text) + Convert.ToDecimal(TxtTotalAll.Text)).ToString();
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
		dataRow["الكمية"] = 1;
		dataRow["المخزن"] = ORD.GetStoreName();
		dataRow["رقم المخزن"] = ORD.GetStoreID();
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
	}

	public void InsertNewItem(int itemid, string Barcode)
	{
		bool flag = false;
		string text = "";
		Item_Items ItemDetails = ORD.GetItems(itemid);
		Printer printer = ORD.GetPrinter(itemid);
		decimal? sellPriceSmall = ItemDetails.SellPriceSmall;
		if ((sellPriceSmall.GetValueOrDefault() > 0m) & sellPriceSmall.HasValue)
		{
			foreach (DataRow row in datatable.Rows)
			{
				if (ItemDetails.ItemId == itemid)
				{
					text = db.Item_Unit.FirstOrDefault((Item_Unit s) => (int?)s.ID == ItemDetails.UnitSmall).Name;
				}
				if (ItemDetails.SmallBarCode1 == Barcode || ItemDetails.SmallBarCode2 == Barcode || ItemDetails.SmallBarCode3 == Barcode)
				{
					text = db.Item_Unit.FirstOrDefault((Item_Unit s) => (int?)s.ID == ItemDetails.UnitSmall).Name;
				}
				if (ItemDetails.MediumBarCode1 == Barcode || ItemDetails.MediumBarCode2 == Barcode || ItemDetails.MediumBarCode3 == Barcode)
				{
					text = db.Item_Unit.FirstOrDefault((Item_Unit s) => (int?)s.ID == ItemDetails.UnitSmall).Name;
				}
				if (ItemDetails.BigBarCode1 == Barcode || ItemDetails.BigBarCode2 == Barcode || ItemDetails.BigBarCode3 == Barcode)
				{
					text = db.Item_Unit.FirstOrDefault((Item_Unit s) => (int?)s.ID == ItemDetails.UnitSmall).Name;
				}
				string item_code = ItemDetails.Item_code;
				string text2 = row["كود_الصنف"].ToString();
				string text3 = row["الوحدة"].ToString();
				if (!(item_code == text2) || !(text3 == text))
				{
					continue;
				}
				row["الكمية"] = Convert.ToDecimal(row["الكمية"]) + 1m;
				ItmID2 = row["كود_الصنف"].ToString();
				row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
				if (ORD.IsVatActive())
				{
					if (ItemDetails.Is_Tax == true)
					{
						decimal value = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]);
						decimal? tax_Value = ItemDetails.Tax_Value;
						row["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value * tax_Value / (decimal?)100), DecimalNum);
						string text4 = row["الضربية"].ToString();
						row["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"])), DecimalNum);
						decimal value2 = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]);
						decimal? tax_Value2 = ItemDetails.Tax_Value;
						row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]))), DecimalNum);
						row["هل_ضريبة"] = 1;
						row["قيمة_الضريبة"] = ItemDetails.Tax_Value;
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
			if (flag)
			{
				return;
			}
			DataRow dataRow2 = null;
			dataRow2 = datatable.NewRow();
			dataRow2["ItemID"] = Convert.ToInt32(ItemDetails.ItemId);
			dataRow2["ItemUnitID"] = Convert.ToInt32(ItemDetails.UnitSmall);
			dataRow2["كود_الصنف"] = ItemDetails.Item_code;
			dataRow2["أسم الصنف"] = ItemDetails.item_Name;
			dataRow2["الاسم الانجليزي"] = ItemDetails.item_Name_English;
			dataRow2["المجموعة"] = ItemDetails.Category_ID;
			dataRow2["PrinterID"] = printer.ID;
			dataRow2["PrinterName"] = printer.Name;
			dataRow2["الكمية"] = 1;
			dataRow2["مسح"] = 0;
			dataRow2["تم"] = 1;
			dataRow2["المخزن"] = ORD.GetStoreName();
			dataRow2["رقم المخزن"] = ORD.GetStoreID();
			if (Barcode == ItemDetails.MediumBarCode1 || Barcode == ItemDetails.MediumBarCode2 || Barcode == ItemDetails.MediumBarCode3)
			{
				dataRow2["أسم_الوحدة"] = "المتوسطة";
				dataRow2["سعر الوحدة"] = ItemDetails.SellPriceMedium;
				dataRow2["الوحدة"] = db.Item_Unit.FirstOrDefault((Item_Unit s) => (int?)s.ID == ItemDetails.UnitMedium).Name;
				dataRow2["متوسط_التكلفة"] = ItemDetails.Average_cost * ItemDetails.ConvertMediumUnit;
				dataRow2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow2["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow2["الكمية"]), DecimalNum);
				dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(ItemDetails.SellPriceMedium * (decimal?)Convert.ToDecimal(1)), DecimalNum);
			}
			else if (Barcode == ItemDetails.BigBarCode1 || Barcode == ItemDetails.BigBarCode2 || Barcode == ItemDetails.BigBarCode3)
			{
				dataRow2["أسم_الوحدة"] = "الكبرى";
				dataRow2["سعر الوحدة"] = ItemDetails.SellpriceLarge;
				dataRow2["الوحدة"] = db.Item_Unit.FirstOrDefault((Item_Unit s) => (int?)s.ID == ItemDetails.UnitLarge).Name;
				dataRow2["متوسط_التكلفة"] = ItemDetails.Average_cost * ItemDetails.ConvertMediumUnit * ItemDetails.ConvertBigUnit;
				dataRow2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow2["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow2["الكمية"]), DecimalNum);
				dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(ItemDetails.SellpriceLarge * (decimal?)Convert.ToDecimal(1)), DecimalNum);
			}
			else
			{
				dataRow2["أسم_الوحدة"] = "الصغرى";
				dataRow2["سعر الوحدة"] = ItemDetails.SellPriceSmall;
				dataRow2["الوحدة"] = db.Item_Unit.FirstOrDefault((Item_Unit s) => (int?)s.ID == ItemDetails.UnitSmall).Name;
				dataRow2["متوسط_التكلفة"] = ItemDetails.Average_cost;
				dataRow2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow2["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow2["الكمية"]), DecimalNum);
				dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToDecimal(1)), DecimalNum);
			}
			if (ORD.IsVatActive())
			{
				if (ItemDetails.Is_Tax == true)
				{
					DataRow dataRow3 = dataRow2;
					decimal value = Convert.ToDecimal(dataRow2["سعر الوحدة"]) * Convert.ToDecimal(1);
					decimal? tax_Value = ItemDetails.Tax_Value;
					dataRow3["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value * tax_Value / (decimal?)100), DecimalNum);
					DataRow dataRow4 = dataRow2;
					decimal value2 = Convert.ToDecimal(dataRow2["سعر الوحدة"]) * Convert.ToDecimal(1);
					decimal? tax_Value3 = ItemDetails.Tax_Value;
					dataRow4["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value3 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(dataRow2["سعر الوحدة"]) * Convert.ToDecimal(1))), DecimalNum);
					dataRow2["هل_ضريبة"] = 1;
					dataRow2["قيمة_الضريبة"] = ItemDetails.Tax_Value;
					decimal num = Convert.ToDecimal(dataRow2["الضربية"]);
					decimal num2 = Convert.ToDecimal(dataRow2["إجمالى المبلغ"]);
					decimal num3 = Convert.ToDecimal(dataRow2["قيمة_الضريبة"]);
					decimal num4 = Convert.ToDecimal(dataRow2["إجمالى المبلغ"]);
				}
				else
				{
					dataRow2["الضربية"] = 0.0;
					dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataRow2["سعر الوحدة"]) * Convert.ToDecimal(1))), DecimalNum);
					dataRow2["هل_ضريبة"] = 0;
					dataRow2["قيمة_الضريبة"] = 0;
				}
			}
			else
			{
				dataRow2["الضربية"] = 0.0;
				dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataRow2["سعر الوحدة"]) * Convert.ToDecimal(1))), DecimalNum);
				dataRow2["هل_ضريبة"] = 0;
				dataRow2["قيمة_الضريبة"] = 0;
			}
			dataRow2["ظ…"] = TblID;
			TblID++;
			datatable.Rows.Add(dataRow2);
			gridControl1.DataSource = datatable;
			GetTotalCost();
			Cal_FinalResultTotals();
			SoundPlayer soundPlayer2 = new SoundPlayer("MsgSent.WAV");
			soundPlayer2.Play();
		}
		else
		{
			MessageBox.Show("عفوا لا يوجد سعر بيع", "مستكشف الاسعار", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.ServiceNotification);
		}
	}

	public void InsertNewItem(int itemid)
	{
		bool flag = false;
		Item_Items ItemDetails = ORD.GetItems(itemid);
		Printer printer = ORD.GetPrinter(itemid);
		decimal? sellPriceSmall = ItemDetails.SellPriceSmall;
		if ((sellPriceSmall.GetValueOrDefault() > 0m) & sellPriceSmall.HasValue)
		{
			foreach (DataRow row in datatable.Rows)
			{
				string item_code = ItemDetails.Item_code;
				string text = row["كود_الصنف"].ToString();
				if (!(item_code == text))
				{
					continue;
				}
				row["الكمية"] = Convert.ToDecimal(row["الكمية"]) + 1m;
				ItmID2 = row["كود_الصنف"].ToString();
				row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
				if (ORD.IsVatActive())
				{
					if (ItemDetails.Is_Tax == true)
					{
						decimal value = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]);
						decimal? tax_Value = ItemDetails.Tax_Value;
						row["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value * tax_Value / (decimal?)100), DecimalNum);
						string text2 = row["الضربية"].ToString();
						row["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"])), DecimalNum);
						decimal value2 = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]);
						decimal? tax_Value2 = ItemDetails.Tax_Value;
						row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]))), DecimalNum);
						row["هل_ضريبة"] = 1;
						row["قيمة_الضريبة"] = ItemDetails.Tax_Value;
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
			if (!flag)
			{
				DataRow dataRow2 = null;
				dataRow2 = datatable.NewRow();
				dataRow2["ItemID"] = Convert.ToInt32(ItemDetails.ItemId);
				dataRow2["ItemUnitID"] = Convert.ToInt32(ItemDetails.UnitSmall);
				dataRow2["PrinterID"] = printer.ID;
				dataRow2["PrinterName"] = printer.Name;
				dataRow2["كود_الصنف"] = ItemDetails.Item_code;
				dataRow2["أسم الصنف"] = ItemDetails.item_Name;
				dataRow2["الاسم الانجليزي"] = ItemDetails.item_Name_English;
				dataRow2["المجموعة"] = ItemDetails.Category_ID;
				dataRow2["الوحدة"] = db.Item_Unit.FirstOrDefault((Item_Unit s) => (int?)s.ID == ItemDetails.UnitSmall).Name;
				dataRow2["الكمية"] = 1;
				dataRow2["مسح"] = 0;
				dataRow2["تم"] = 1;
				dataRow2["أسم_الوحدة"] = "الصغرى";
				dataRow2["متوسط_التكلفة"] = ItemDetails.Average_cost;
				dataRow2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow2["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow2["الكمية"]), DecimalNum);
				dataRow2["سعر الوحدة"] = ItemDetails.SellPriceSmall;
				dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToDecimal(1)), DecimalNum);
				dataRow2["المخزن"] = ORD.GetStoreName();
				dataRow2["رقم المخزن"] = ORD.GetStoreID();
				if (ORD.IsVatActive())
				{
					if (ItemDetails.Is_Tax == true)
					{
						dataRow2["الضربية"] = Math.Round(Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToDecimal(1) * ItemDetails.Tax_Value / (decimal?)100), DecimalNum);
						dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToDecimal(1) * ItemDetails.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToDecimal(1))), DecimalNum);
						dataRow2["هل_ضريبة"] = 1;
						dataRow2["قيمة_الضريبة"] = ItemDetails.Tax_Value;
					}
					else
					{
						dataRow2["الضربية"] = 0.0;
						dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToDecimal(1))), DecimalNum);
						dataRow2["هل_ضريبة"] = 0;
						dataRow2["قيمة_الضريبة"] = 0;
					}
				}
				else
				{
					dataRow2["الضربية"] = 0.0;
					dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToDecimal(1))), DecimalNum);
					dataRow2["هل_ضريبة"] = 0;
					dataRow2["قيمة_الضريبة"] = 0;
				}
				dataRow2["ظ…"] = TblID;
				TblID++;
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
			string barcodeMizan = BarcodeMizan.Substring(3, 13);
			string text = BarcodeMizan.Substring(BarcodeMizan.Length - 13, 13);
			string value2;
			if (LoginDetails.IsMizanWeight)
			{
				string value = text.Substring(0, 2);
				string text2 = text.Substring(text.Length - 4, 4);
				value2 = (Convert.ToDecimal(value) + Convert.ToDecimal("." + text2)).ToString();
			}
			else
			{
				string value = text.Substring(0, 3);
				string text2 = text.Substring(text.Length - 3, 3);
				value2 = (Convert.ToDecimal(value) + Convert.ToDecimal("." + text2)).ToString();
			}
			Item_Items ItemDetails = ORD.GetItemsMizan(barcodeMizan);
			if (ItemDetails != null && ItemDetails.Is_Item_mizan == true)
			{
				bool flag2 = true;
				if (!flag)
				{
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = Convert.ToInt32(ItemDetails.ItemId);
					dataRow["ItemUnitID"] = Convert.ToInt32(ItemDetails.UnitSmall);
					dataRow["كود_الصنف"] = ItemDetails.Item_code;
					dataRow["أسم الصنف"] = ItemDetails.item_Name;
					dataRow["الاسم الانجليزي"] = ItemDetails.item_Name_English;
					dataRow["المجموعة"] = ItemDetails.Category_ID;
					if (LoginDetails.IsMizanWeight)
					{
						dataRow["الكمية"] = Convert.ToDecimal(value2);
						dataRow["سعر الوحدة"] = ItemDetails.SellPriceSmall;
						dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(dataRow["سعر الوحدة"]) * Convert.ToDecimal(dataRow["الكمية"]), 3);
					}
					else
					{
						dataRow["الكمية"] = Math.Round(Convert.ToDecimal(value2) / Convert.ToDecimal(ItemDetails.SellPriceSmall), 3);
						dataRow["سعر الوحدة"] = ItemDetails.SellPriceSmall;
						dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(value2), 3);
					}
					dataRow["مسح"] = 0;
					dataRow["تم"] = 1;
					dataRow["المخزن"] = ORD.GetStoreName();
					dataRow["رقم المخزن"] = ORD.GetStoreID();
					dataRow["أسم_الوحدة"] = "الصغرى";
					dataRow["الوحدة"] = db.Item_Unit.FirstOrDefault((Item_Unit s) => (int?)s.ID == ItemDetails.UnitSmall).Name;
					dataRow["متوسط_التكلفة"] = ItemDetails.Average_cost;
					dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
					if (ORD.IsVatActive())
					{
						if (ItemDetails.Is_Tax == true)
						{
							DataRow dataRow2 = dataRow;
							decimal value3 = Convert.ToDecimal(dataRow["الاجمالى"]);
							decimal? tax_Value = ItemDetails.Tax_Value;
							dataRow2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value / (decimal?)100), DecimalNum);
							dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(dataRow["الضربية"]) + Convert.ToDecimal(Convert.ToDecimal(dataRow["الاجمالى"])), DecimalNum);
							dataRow["هل_ضريبة"] = 1;
							dataRow["قيمة_الضريبة"] = ItemDetails.Tax_Value;
						}
						else
						{
							dataRow["الضربية"] = 0.0;
							dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(dataRow["سعر الوحدة"]), DecimalNum);
							dataRow["هل_ضريبة"] = 0;
							dataRow["قيمة_الضريبة"] = 0;
						}
					}
					else
					{
						dataRow["الضربية"] = 0.0;
						dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(dataRow["سعر الوحدة"]), DecimalNum);
						dataRow["هل_ضريبة"] = 0;
						dataRow["قيمة_الضريبة"] = 0;
					}
					dataRow["ظ…"] = TblID;
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
					TxtDiscount.Text = "0.00";
					foreach (DataRow row in datatable.Rows)
					{
						if (row["أسم_الوحدة"].ToString() != "")
						{
							row["الكمية"] = Math.Round(Convert.ToDecimal(row["الكمية"]), 3).ToString();
							row["سعر الوحدة"] = Math.Round(Convert.ToDecimal(row["سعر الوحدة"]), DecimalNum).ToString();
							row["متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]), DecimalNum).ToString();
							row["قيمة_الضريبة"] = Math.Round(Convert.ToDecimal(row["قيمة_الضريبة"]), DecimalNum).ToString();
							row["سعربعدالخصم"] = row["سعر الوحدة"];
							row["إجمالى_بعدالخصم"] = row["الاجمالى"];
							row["الضريبه_بعدالخصم"] = row["الضربية"];
						}
					}
					object summaryValue = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
					decimal value = Convert.ToDecimal(summaryValue);
					object summaryValue2 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
					decimal value2 = Convert.ToDecimal(summaryValue2);
					object summaryValue3 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
					TxtAllTotal.Text = Convert.ToString(summaryValue3);
					object summaryValue4 = gridView1.Columns["الضريبه_بعدالخصم"].SummaryItem.SummaryValue;
					decimal num = Convert.ToDecimal(summaryValue4);
					object summaryValue5 = gridView1.Columns["اجمالي_متوسط_التكلفة"].SummaryItem.SummaryValue;
					TxtCost.Text = Convert.ToString(summaryValue5);
					TxtAllTotal.Text = value.ToString();
					TxtVatBeforeDisc.Text = num.ToString();
					TxtNet22.Text = Math.Round(Convert.ToDecimal(value), DecimalNum).ToString();
					TxtTotalAll.Text = Math.Round(Convert.ToDecimal(value) + Convert.ToDecimal(value2), DecimalNum).ToString();
					TxtTotalBeforDisc.Text = (Convert.ToDecimal(TxtAllTotal.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
					TxtTabocc.Text = TxtTotalBeforDisc.Text;
					TxtAllVat.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m * Convert.ToDecimal(1.05) - Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m).ToString();
					TxtNet.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m + Convert.ToDecimal(TxtAllVat.Text)).ToString();
					return;
				}
				object summaryValue6 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
				decimal value3 = Convert.ToDecimal(summaryValue6);
				object summaryValue7 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
				decimal value4 = Convert.ToDecimal(summaryValue7);
				object summaryValue8 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
				TxtAllTotal.Text = Convert.ToString(summaryValue8);
				object summaryValue9 = gridView1.Columns["الضريبه_بعدالخصم"].SummaryItem.SummaryValue;
				decimal num2 = Convert.ToDecimal(summaryValue9);
				object summaryValue10 = gridView1.Columns["اجمالي_متوسط_التكلفة"].SummaryItem.SummaryValue;
				TxtCost.Text = Convert.ToString(summaryValue10);
				TxtAllTotal.Text = value3.ToString();
				decimal value5 = Math.Round(Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)) / Math.Round(Convert.ToDecimal(TxtAllTotal.Text), DecimalNum) * 100m, DecimalNum);
				TxtNet22.Text = Math.Round(Convert.ToDecimal(value3), DecimalNum).ToString();
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(value3) + Convert.ToDecimal(value4), DecimalNum).ToString();
				TxtVatBeforeDisc.Text = num2.ToString();
				TxtTotalBeforDisc.Text = (Convert.ToDecimal(TxtAllTotal.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
				TxtTabocc.Text = TxtTotalBeforDisc.Text;
				TxtAllVat.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m * Convert.ToDecimal(1.05) - Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m).ToString();
				TxtNet.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m + Convert.ToDecimal(TxtAllVat.Text)).ToString();
				{
					foreach (DataRow row2 in datatable.Rows)
					{
						if (row2["أسم_الوحدة"].ToString() != "")
						{
							row2["الكمية"] = Math.Round(Convert.ToDecimal(row2["الكمية"]), 3).ToString();
							row2["سعر الوحدة"] = Math.Round(Convert.ToDecimal(row2["سعر الوحدة"]), DecimalNum).ToString();
							row2["متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row2["متوسط_التكلفة"]), DecimalNum).ToString();
							row2["قيمة_الضريبة"] = Math.Round(Convert.ToDecimal(row2["قيمة_الضريبة"]), DecimalNum).ToString();
							row2["سعربعدالخصم"] = Convert.ToDecimal(row2["سعر الوحدة"].ToString()) - Convert.ToDecimal(value5) * Convert.ToDecimal(row2["سعر الوحدة"]) / 100m;
							row2["إجمالى_بعدالخصم"] = Convert.ToDecimal(row2["سعربعدالخصم"].ToString()) * Convert.ToDecimal(row2["الكمية"].ToString());
							row2["الضريبه_بعدالخصم"] = Convert.ToDecimal(Convert.ToDecimal(row2["سعربعدالخصم"]) * Convert.ToDecimal(row2["الكمية"]) * Convert.ToDecimal(row2["قيمة_الضريبة"]) / 100m);
						}
					}
					return;
				}
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
						row3["الكمية"] = Math.Round(Convert.ToDecimal(row3["الكمية"]), 3).ToString();
						row3["سعر الوحدة"] = Math.Round(Convert.ToDecimal(row3["سعر الوحدة"]), DecimalNum).ToString();
						row3["متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row3["متوسط_التكلفة"]), DecimalNum).ToString();
						row3["قيمة_الضريبة"] = Math.Round(Convert.ToDecimal(row3["قيمة_الضريبة"]), DecimalNum).ToString();
						row3["سعربعدالخصم"] = row3["سعر الوحدة"];
						row3["إجمالى_بعدالخصم"] = row3["الاجمالى"];
						row3["الضريبه_بعدالخصم"] = row3["الضربية"];
					}
				}
				object summaryValue11 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
				TxtTotals.Text = Convert.ToString(summaryValue11);
				object summaryValue12 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
				TxtVat.Text = Convert.ToString(summaryValue12);
				object summaryValue13 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
				TxtTotalBeforDisc.Text = Convert.ToString(summaryValue13);
				object summaryValue14 = gridView1.Columns["الضريبه_بعدالخصم"].SummaryItem.SummaryValue;
				TxtVatBeforeDisc.Text = Convert.ToString(summaryValue14);
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)).ToString();
				TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)).ToString();
				object summaryValue15 = gridView1.Columns["اجمالي_متوسط_التكلفة"].SummaryItem.SummaryValue;
				TxtCost.Text = Convert.ToString(summaryValue15);
				TxtAllVat.Text = TxtVatBeforeDisc.Text;
				TxtAllTotal.Text = TxtTotalAll.Text;
				TxtTabocc.Text = "0.00";
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
				return;
			}
			decimal value6 = Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text);
			decimal num3 = Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum) * 100m) / Math.Round(Convert.ToDecimal(value6), DecimalNum);
			object summaryValue16 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
			TxtTotals.Text = Convert.ToString(summaryValue16);
			object summaryValue17 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
			TxtVat.Text = Convert.ToString(summaryValue17);
			object summaryValue18 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
			TxtTotalBeforDisc.Text = Convert.ToString(summaryValue18);
			object summaryValue19 = gridView1.Columns["الضريبه_بعدالخصم"].SummaryItem.SummaryValue;
			TxtVatBeforeDisc.Text = Convert.ToString(summaryValue19);
			object summaryValue20 = gridView1.Columns["اجمالي_متوسط_التكلفة"].SummaryItem.SummaryValue;
			TxtCost.Text = Convert.ToString(summaryValue20);
			TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
			TxtNet22.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
			TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
			TxtAllVat.Text = TxtVatBeforeDisc.Text;
			TxtAllTotal.Text = TxtTotalAll.Text;
			TxtTabocc.Text = "0.00";
			foreach (DataRow row4 in datatable.Rows)
			{
				if (row4["أسم_الوحدة"].ToString() != "")
				{
					row4["الكمية"] = Math.Round(Convert.ToDecimal(row4["الكمية"]), 3).ToString();
					row4["سعر الوحدة"] = Math.Round(Convert.ToDecimal(row4["سعر الوحدة"]), DecimalNum).ToString();
					row4["متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row4["متوسط_التكلفة"]), DecimalNum).ToString();
					row4["قيمة_الضريبة"] = Math.Round(Convert.ToDecimal(row4["قيمة_الضريبة"]), DecimalNum).ToString();
					row4["سعربعدالخصم"] = Convert.ToDecimal(row4["سعر الوحدة"].ToString()) - Convert.ToDecimal(num3 * Convert.ToDecimal(row4["سعر الوحدة"].ToString()) / 100m);
					row4["إجمالى_بعدالخصم"] = Convert.ToDecimal(row4["سعربعدالخصم"].ToString()) * Convert.ToDecimal(row4["الكمية"].ToString());
					row4["الضريبه_بعدالخصم"] = Convert.ToDecimal(Convert.ToDecimal(row4["سعربعدالخصم"]) * Convert.ToDecimal(row4["الكمية"]) * Convert.ToDecimal(row4["قيمة_الضريبة"]) / 100m);
				}
			}
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
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
			row["ظ…"] = num;
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
					Item_Items ItemDetails = ORD.GetItems(code).FirstOrDefault();
					Printer printer = ORD.GetPrinter(ItemDetails.ItemId);
					if (ItemDetails != null)
					{
						DataRow dataRow = null;
						dataRow = datatable.NewRow();
						dataRow["ItemID"] = Convert.ToInt32(ItemDetails.ItemId);
						dataRow["ItemUnitID"] = Convert.ToInt32(ItemDetails.UnitSmall);
						dataRow["PrinterID"] = printer.ID;
						dataRow["PrinterName"] = printer.Name;
						dataRow["كود_الصنف"] = ItemDetails.Item_code;
						dataRow["أسم الصنف"] = ItemDetails.item_Name;
						dataRow["الاسم الانجليزي"] = ItemDetails.item_Name_English;
						dataRow["المجموعة"] = ItemDetails.Category_ID;
						dataRow["الوحدة"] = db.Item_Unit.FirstOrDefault((Item_Unit s) => (int?)s.ID == ItemDetails.UnitSmall).Name;
						dataRow["الكمية"] = 1;
						dataRow["متوسط_التكلفة"] = ItemDetails.Average_cost;
						dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
						dataRow["المخزن"] = ORD.GetStoreName();
						dataRow["رقم المخزن"] = ORD.GetStoreID();
						dataRow["أسم_الوحدة"] = "الصغرى";
						dataRow["سعر الوحدة"] = ItemDetails.SellPriceSmall;
						dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToInt32(1)), DecimalNum);
						if (ORD.IsVatActive())
						{
							if (ItemDetails.Is_Tax == true)
							{
								dataRow["الضربية"] = Math.Round(Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToInt32(1) * ItemDetails.Tax_Value / (decimal?)100), DecimalNum);
								dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToInt32(1) * ItemDetails.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
								dataRow["هل_ضريبة"] = 1;
								dataRow["قيمة_الضريبة"] = ItemDetails.Tax_Value;
							}
							else
							{
								dataRow["هل_ضريبة"] = 0;
								dataRow["قيمة_الضريبة"] = 0;
								dataRow["الضربية"] = 0.0;
								dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
						}
						else
						{
							dataRow["هل_ضريبة"] = 0;
							dataRow["قيمة_الضريبة"] = 0;
							dataRow["الضربية"] = 0.0;
							dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(ItemDetails.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
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
					Item_Items item_Items = PurORD.GetItemsByitemid(itemID).FirstOrDefault();
					Printer printer = ORD.GetPrinter(item_Items.ItemId);
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
					dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
					dataRow["PrinterID"] = printer.ID;
					dataRow["PrinterName"] = printer.Name;
					dataRow["كود_الصنف"] = item_Items.Item_code;
					dataRow["أسم الصنف"] = item_Items.item_Name;
					dataRow["الاسم الانجليزي"] = item_Items.item_Name_English;
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
				PrintID = custCode;
				TxtType.SelectedValue = order_PriceOffer.Sectoral;
				TxtCode.Text = "";
				LblSup.Text = order_PriceOffer.SupplierID.ToString();
				TxtSuppliers.Text = order_PriceOffer.SupplierName;
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
				insertnewrow();
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
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		string text = TxtBarcode.Text;
		if (text != "")
		{
			ItemMatchResult itemsOnly = ORD.GetItemsOnly(text);
			if (itemsOnly == null)
			{
				string code = text.Substring(3, 13);
				ItemMatchResult itemsOnly2 = ORD.GetItemsOnly(code);
				if (itemsOnly2 == null)
				{
					SoundPlayer soundPlayer = new SoundPlayer("SOUND38.WAV");
					soundPlayer.Play();
				}
				else
				{
					InsertNewItem(itemsOnly2.Item.ItemId, text);
				}
			}
			else
			{
				InsertNewItem(itemsOnly.Item.ItemId, text);
			}
		}
		TxtBarcode.Text = "";
		TxtBarcode.Focus();
	}

	private void SetDateCasheir()
	{
		TxtDate.DateTime = DateTime.Now;
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
				dtCurrentTable.Rows.Add(num, Purchese_ID, Convert.ToInt32(row["ItemID"]), LoginDetails.BranchID, IsWait, Convert.ToInt32(row["رقم المخزن"]), Convert.ToString(row["ItemUnitID"]), Convert.ToDecimal(row["الكمية"]), Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]), DecimalNum), Convert.ToDecimal(row["سعر الوحدة"]), Math.Round(Convert.ToDecimal(row["سعر الوحدة"]), DecimalNum), Math.Round(Convert.ToDecimal(row["الاجمالى"]), DecimalNum), Math.Round(Convert.ToDecimal(row["الضربية"]), DecimalNum), Math.Round(Convert.ToDecimal(row["سعربعدالخصم"]), DecimalNum), Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"]), DecimalNum), Math.Round(Convert.ToDecimal(row["الضريبه_بعدالخصم"]), DecimalNum), row["أسم_الوحدة"].ToString(), 1, null, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
				num++;
			}
		}
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
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
			GridMinus();
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
			decimal num = Math.Round(Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotalAll.Text) * 100m, DecimalNum);
			txtdicoutprege.Value = num;
			ISEVENTDISC = true;
		}
		catch (Exception)
		{
			TxtDiscount.Text = "0.00";
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
			TxtSuppliers.Text = "مورد نقدى";
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

	private void ControlTxtSuppliers_KeyDown(object sender, KeyEventArgs e)
	{
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
			if (datatable.Rows.Count > 0 && LoginDetails.IsSecureCashier)
			{
				FrmPassWordSupervisor frmPassWordSupervisor = new FrmPassWordSupervisor();
				frmPassWordSupervisor.ShowDialog();
				if (!frmPassWordSupervisor.ISClose)
				{
					e.Cancel = true;
					return;
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

	private void MenuNewItem_Click(object sender, EventArgs e)
	{
		openItmCard();
	}

	private void MenuSearchItem_Click(object sender, EventArgs e)
	{
		SearchItems();
	}

	private void MenuPurchOrder_Click(object sender, EventArgs e)
	{
		GetPurchesOrder();
	}

	private void MenuExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TxtType_KeyPress_1(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
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
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmCashirPharm));
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions imageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled4 = new DevExpress.Utils.SerializableAppearanceObject();
		this.TxtNet22 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
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
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtAllTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.label18 = new System.Windows.Forms.Label();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtAllVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtTabocc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtTotalBeforDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
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
		this.RecoveredMenuItemFix1 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintQued = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPreviewFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuNewItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix2 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPurchOrder = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix3 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix4 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix5 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix6 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix12 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix13 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix14 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix15 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix16 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix17 = new System.Windows.Forms.ToolStripMenuItem();
		this.ctrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix18 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix19 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix20 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix21 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix7 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix8 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix9 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix10 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.RecoveredMenuItemFix11 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.TxtCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label9 = new System.Windows.Forms.Label();
		this.BtnReturnCashier = new DevExpress.XtraEditors.SimpleButton();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4Units).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.TxtNet22.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNet22.DecimalDigits = 3;
		this.TxtNet22.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtNet22.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet22.Location = new System.Drawing.Point(957, 526);
		this.TxtNet22.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtNet22.Name = "TxtNet22";
		this.TxtNet22.ReadOnly = true;
		this.TxtNet22.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNet22.Size = new System.Drawing.Size(144, 32);
		this.TxtNet22.TabIndex = 100;
		this.TxtNet22.Text = "0.000";
		this.TxtNet22.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet22.UseCompatibleTextRendering = false;
		this.TxtNet22.Value = new decimal(new int[4]);
		this.TxtNet22.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.gridControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridControl1.Location = new System.Drawing.Point(10, 214);
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[9] { this.repositoryItemButtonEdit1, this.repositoryItemButtonEdit2, this.repositoryItemTextEdit1, this.repositoryItemTextEdit2, this.repositoryItemDateEdit1, this.repositoryItemTextEdit3, this.repositoryItemPictureEdit1, this.repositoryItemButtonEdit3, this.repositoryItemButtonEdit4Units });
		this.gridControl1.Size = new System.Drawing.Size(1091, 306);
		this.gridControl1.TabIndex = 101;
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(gridControl1_KeyDown);
		this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 20f);
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[19]
		{
			this.gridColumn24, this.gridColumn23, this.gridColumn1, this.gridColumn2, this.gridColumn26, this.shearch, this.gridColumn19, this.gridColumn4, this.gridColumn25, this.gridColumn20,
			this.gridColumn21, this.gridColumn3, this.gridColumn6, this.gridColumn7, this.gridColumn22, this.gridColumn5, this.gridColumn8, this.gridColumn9, this.gridColumn27
		});
		this.gridView1.DetailHeight = 355;
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
		this.gridColumn24.Caption = "الضريبه_بعدالخصم";
		this.gridColumn24.FieldName = "الضريبه_بعدالخصم";
		this.gridColumn24.MinWidth = 21;
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn24.Width = 81;
		this.gridColumn23.Caption = "إجمالى_بعدالخصم";
		this.gridColumn23.FieldName = "إجمالى_بعدالخصم";
		this.gridColumn23.MinWidth = 21;
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.Width = 81;
		this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Bold);
		this.gridColumn1.AppearanceCell.FontStyleDelta = System.Drawing.FontStyle.Bold;
		this.gridColumn1.AppearanceCell.Options.UseFont = true;
		this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn1.AppearanceHeader.Options.UseFont = true;
		this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.Caption = "كود_الصنف";
		this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.FieldName = "كود_الصنف";
		this.gridColumn1.MinWidth = 21;
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.OptionsColumn.AllowEdit = false;
		this.gridColumn1.Width = 188;
		this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.Caption = "أسم الصنف ItemName";
		this.gridColumn2.FieldName = "أسم الصنف";
		this.gridColumn2.MinWidth = 21;
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.gridColumn2.Visible = true;
		this.gridColumn2.VisibleIndex = 0;
		this.gridColumn2.Width = 171;
		this.gridColumn26.Caption = "المجموعة";
		this.gridColumn26.FieldName = "المجموعة";
		this.gridColumn26.MinWidth = 21;
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.Width = 81;
		this.shearch.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.shearch.AppearanceCell.Options.UseFont = true;
		this.shearch.AppearanceCell.Options.UseTextOptions = true;
		this.shearch.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.shearch.AppearanceHeader.Options.UseFont = true;
		this.shearch.AppearanceHeader.Options.UseTextOptions = true;
		this.shearch.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.Caption = "Unit";
		this.shearch.ColumnEdit = this.repositoryItemButtonEdit4Units;
		this.shearch.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.FieldName = "الوحدة";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.MinWidth = 21;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		this.shearch.Visible = true;
		this.shearch.VisibleIndex = 1;
		this.shearch.Width = 51;
		this.repositoryItemButtonEdit4Units.AutoHeight = false;
		this.repositoryItemButtonEdit4Units.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
		});
		this.repositoryItemButtonEdit4Units.Name = "repositoryItemButtonEdit4Units";
		this.repositoryItemButtonEdit4Units.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit4Units_ButtonClick);
		this.gridColumn19.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.Caption = "أسم_الوحدة";
		this.gridColumn19.FieldName = "أسم_الوحدة";
		this.gridColumn19.MinWidth = 21;
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		this.gridColumn19.Width = 81;
		this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.Caption = "المخزن";
		this.gridColumn4.ColumnEdit = this.repositoryItemButtonEdit3;
		this.gridColumn4.FieldName = "المخزن";
		this.gridColumn4.MinWidth = 21;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Width = 194;
		this.repositoryItemButtonEdit3.AutoHeight = false;
		editorButtonImageOptions.Image = GTSErpSystem.Properties.Resources.rsz_1minus;
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
		this.gridColumn25.Caption = "متوسط_التكلفة";
		this.gridColumn25.FieldName = "متوسط_التكلفة";
		this.gridColumn25.MinWidth = 21;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.Width = 81;
		this.gridColumn20.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn20.AppearanceCell.Options.UseFont = true;
		this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn20.AppearanceHeader.Options.UseFont = true;
		this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.Caption = ".Qty";
		this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.FieldName = "الكمية";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.MinWidth = 21;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn20.Visible = true;
		this.gridColumn20.VisibleIndex = 2;
		this.gridColumn20.Width = 64;
		this.gridColumn21.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn21.AppearanceCell.Options.UseFont = true;
		this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn21.AppearanceHeader.Options.UseFont = true;
		this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.gridColumn21.Caption = "Price";
		this.gridColumn21.FieldName = "سعر الوحدة";
		this.gridColumn21.MinWidth = 21;
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.Visible = true;
		this.gridColumn21.VisibleIndex = 3;
		this.gridColumn21.Width = 64;
		this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.Caption = "Total";
		this.gridColumn3.FieldName = "الاجمالى";
		this.gridColumn3.MinWidth = 21;
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn3.Width = 83;
		this.gridColumn6.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
		this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn6.AppearanceCell.FontStyleDelta = System.Drawing.FontStyle.Bold;
		this.gridColumn6.AppearanceCell.Options.UseBackColor = true;
		this.gridColumn6.AppearanceCell.Options.UseFont = true;
		this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn6.AppearanceHeader.Options.UseFont = true;
		this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.Caption = "الضريبة";
		this.gridColumn6.FieldName = "الضربية";
		this.gridColumn6.MinWidth = 21;
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.ReadOnly = true;
		this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn6.Width = 169;
		this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.Caption = "Totals";
		this.gridColumn7.FieldName = "إجمالى المبلغ";
		this.gridColumn7.MinWidth = 21;
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.OptionsColumn.ReadOnly = true;
		this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn7.Visible = true;
		this.gridColumn7.VisibleIndex = 4;
		this.gridColumn7.Width = 64;
		this.gridColumn22.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn22.AppearanceCell.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn22.AppearanceHeader.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.Caption = "+P";
		this.gridColumn22.ColumnEdit = this.repositoryItemButtonEdit1;
		this.gridColumn22.FieldName = "زيادة";
		this.gridColumn22.MinWidth = 21;
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn22.Visible = true;
		this.gridColumn22.VisibleIndex = 5;
		this.gridColumn22.Width = 21;
		this.repositoryItemButtonEdit1.AutoHeight = false;
		editorButtonImageOptions2.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions2.Image");
		editorButtonImageOptions2.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit1.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonPressed);
		this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.Caption = "-M";
		this.gridColumn5.ColumnEdit = this.repositoryItemButtonEdit3;
		this.gridColumn5.FieldName = "نقصان";
		this.gridColumn5.MinWidth = 21;
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.Visible = true;
		this.gridColumn5.VisibleIndex = 6;
		this.gridColumn5.Width = 21;
		this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn8.AppearanceCell.FontStyleDelta = System.Drawing.FontStyle.Bold;
		this.gridColumn8.AppearanceCell.Options.UseFont = true;
		this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn8.AppearanceHeader.Options.UseFont = true;
		this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.Caption = ".Del";
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit2;
		this.gridColumn8.CustomizationCaption = "ط­1ظپ";
		this.gridColumn8.FieldName = "حذف";
		this.gridColumn8.MinWidth = 21;
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Visible = true;
		this.gridColumn8.VisibleIndex = 7;
		this.gridColumn8.Width = 21;
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit2.AutoHeight = false;
		editorButtonImageOptions3.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions3.Image");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "حذف", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonClick);
		this.gridColumn9.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
		this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceCell.Options.UseBackColor = true;
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.Caption = "ظ…";
		this.gridColumn9.FieldName = "ظ…";
		this.gridColumn9.MinWidth = 21;
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.Width = 69;
		this.gridColumn27.Caption = "اجمالي_متوسط_التكلفة";
		this.gridColumn27.FieldName = "اجمالي_متوسط_التكلفة";
		this.gridColumn27.MinWidth = 21;
		this.gridColumn27.Name = "gridColumn27";
		this.gridColumn27.Width = 81;
		this.repositoryItemTextEdit1.AutoHeight = false;
		this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
		this.repositoryItemTextEdit2.AutoHeight = false;
		this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
		this.repositoryItemDateEdit1.AutoHeight = false;
		this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
		this.repositoryItemTextEdit3.AutoHeight = false;
		this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
		this.repositoryItemPictureEdit1.Appearance.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceDisabled.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceReadOnly.Options.UseImage = true;
		this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
		this.repositoryItemPictureEdit1.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
		this.cardView1.CardWidth = 214;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.DetailHeight = 355;
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		this.gridColumn10.Caption = "كود_الصنف";
		this.gridColumn10.MinWidth = 21;
		this.gridColumn10.Name = "gridColumn10";
		this.gridColumn10.Visible = true;
		this.gridColumn10.VisibleIndex = 0;
		this.gridColumn10.Width = 81;
		this.gridColumn11.Caption = "أسم الصنف";
		this.gridColumn11.MinWidth = 21;
		this.gridColumn11.Name = "gridColumn11";
		this.gridColumn11.Visible = true;
		this.gridColumn11.VisibleIndex = 1;
		this.gridColumn11.Width = 81;
		this.gridColumn12.Caption = "الوحدة";
		this.gridColumn12.MinWidth = 21;
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		this.gridColumn12.Visible = true;
		this.gridColumn12.VisibleIndex = 2;
		this.gridColumn12.Width = 81;
		this.gridColumn13.Caption = "الكمية";
		this.gridColumn13.MinWidth = 21;
		this.gridColumn13.Name = "gridColumn13";
		this.gridColumn13.Visible = true;
		this.gridColumn13.VisibleIndex = 3;
		this.gridColumn13.Width = 81;
		this.gridColumn14.Caption = "سعر الوحدة";
		this.gridColumn14.MinWidth = 21;
		this.gridColumn14.Name = "gridColumn14";
		this.gridColumn14.Visible = true;
		this.gridColumn14.VisibleIndex = 4;
		this.gridColumn14.Width = 81;
		this.gridColumn15.Caption = "الاجمالى";
		this.gridColumn15.MinWidth = 21;
		this.gridColumn15.Name = "gridColumn15";
		this.gridColumn15.Visible = true;
		this.gridColumn15.VisibleIndex = 5;
		this.gridColumn15.Width = 81;
		this.gridColumn16.Caption = "الضربية";
		this.gridColumn16.MinWidth = 21;
		this.gridColumn16.Name = "gridColumn16";
		this.gridColumn16.Visible = true;
		this.gridColumn16.VisibleIndex = 6;
		this.gridColumn16.Width = 81;
		this.gridColumn17.Caption = "إجمالى المبلغ";
		this.gridColumn17.MinWidth = 21;
		this.gridColumn17.Name = "gridColumn17";
		this.gridColumn17.Visible = true;
		this.gridColumn17.VisibleIndex = 7;
		this.gridColumn17.Width = 81;
		this.gridColumn18.Caption = "حذف الصنف";
		this.gridColumn18.MinWidth = 21;
		this.gridColumn18.Name = "gridColumn18";
		this.gridColumn18.Visible = true;
		this.gridColumn18.VisibleIndex = 8;
		this.gridColumn18.Width = 81;
		this.BtnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(243, 134);
		this.BtnEdit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Size = new System.Drawing.Size(107, 32);
		this.BtnEdit.TabIndex = 106;
		this.BtnEdit.Text = "تعديل Edit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(128, 134);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(107, 32);
		this.BtnSave.TabIndex = 105;
		this.BtnSave.Text = "حفظ Save";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(128, 175);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(107, 32);
		this.BtnPrint.TabIndex = 108;
		this.BtnPrint.Text = "طباعة Print";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(12, 134);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(107, 32);
		this.BtnNew.TabIndex = 109;
		this.BtnNew.Text = "جديد New";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(12, 175);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(107, 32);
		this.BtnDelete.TabIndex = 107;
		this.BtnDelete.Text = "حذف Delete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(243, 175);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(107, 32);
		this.BtnExit.TabIndex = 110;
		this.BtnExit.Text = "خروج Exit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.txtdicoutprege);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Controls.Add(this.TxtDiscount);
		this.groupBox1.Controls.Add(this.TxtAllTotal);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.TxtNet);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.LblSup);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.TxtType);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.TxtSuppliers);
		this.groupBox1.Controls.Add(this.LBlSuppAccount);
		this.groupBox1.Location = new System.Drawing.Point(10, 10);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Size = new System.Drawing.Size(1140, 118);
		this.groupBox1.TabIndex = 111;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "بيانات فاتورة الكاشير";
		this.txtdicoutprege.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Red;
		this.txtdicoutprege.Location = new System.Drawing.Point(177, 80);
		this.txtdicoutprege.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtdicoutprege.Size = new System.Drawing.Size(92, 23);
		this.txtdicoutprege.TabIndex = 208;
		this.txtdicoutprege.Text = "0.000";
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.txtdicoutprege.TextChanged += new System.EventHandler(TxtDiscountPrecent_TextChanged);
		this.TxtCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCode.Location = new System.Drawing.Point(676, 23);
		this.TxtCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
		this.TxtCode.Size = new System.Drawing.Size(295, 22);
		this.TxtCode.TabIndex = 207;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.TxtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDiscount.ForeColor = System.Drawing.Color.Red;
		this.TxtDiscount.Location = new System.Drawing.Point(177, 51);
		this.TxtDiscount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDiscount.Size = new System.Drawing.Size(92, 23);
		this.TxtDiscount.TabIndex = 205;
		this.TxtDiscount.Text = "0.000";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscount.TextChanged += new System.EventHandler(TxtDiscount_TextChanged);
		this.TxtAllTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtAllTotal.DecimalDigits = 3;
		this.TxtAllTotal.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtAllTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtAllTotal.Location = new System.Drawing.Point(177, 20);
		this.TxtAllTotal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtAllTotal.Name = "TxtAllTotal";
		this.TxtAllTotal.ReadOnly = true;
		this.TxtAllTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtAllTotal.Size = new System.Drawing.Size(92, 24);
		this.TxtAllTotal.TabIndex = 203;
		this.TxtAllTotal.Text = "0.000";
		this.TxtAllTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllTotal.Value = new decimal(new int[4]);
		this.TxtAllTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label1.Location = new System.Drawing.Point(274, 85);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(93, 16);
		this.label1.TabIndex = 190;
		this.label1.Text = "النسبة Percent";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label10.Location = new System.Drawing.Point(976, 25);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(145, 17);
		this.label10.TabIndex = 180;
		this.label10.Text = "رقم الفاتورة Order Num";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 11f);
		this.label4.ForeColor = System.Drawing.Color.Blue;
		this.label4.Location = new System.Drawing.Point(53, 20);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(82, 18);
		this.label4.TabIndex = 175;
		this.label4.Text = "الصافي Net";
		this.TxtNet.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.Font = new System.Drawing.Font("Tahoma", 25f);
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Location = new System.Drawing.Point(49, 55);
		this.TxtNet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNet.Size = new System.Drawing.Size(121, 48);
		this.TxtNet.TabIndex = 112;
		this.TxtNet.Text = "0.000";
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet.TextChanged += new System.EventHandler(TxtNet_TextChanged);
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label2.Location = new System.Drawing.Point(274, 27);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(87, 16);
		this.label2.TabIndex = 168;
		this.label2.Text = "المجموع Total";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label3.Location = new System.Drawing.Point(274, 55);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(95, 16);
		this.label3.TabIndex = 169;
		this.label3.Text = "الخصم Discount";
		this.LblSup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblSup.AutoSize = true;
		this.LblSup.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblSup.Location = new System.Drawing.Point(1118, 54);
		this.LblSup.Name = "LblSup";
		this.LblSup.Size = new System.Drawing.Size(16, 17);
		this.LblSup.TabIndex = 125;
		this.LblSup.Text = "1";
		this.LblSup.Visible = false;
		this.LblSup.TextChanged += new System.EventHandler(LblSup_TextChanged);
		this.TxtDate.EditValue = null;
		this.TxtDate.Location = new System.Drawing.Point(390, 53);
		this.TxtDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDate.Properties.Appearance.Options.UseFont = true;
		this.TxtDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate.Properties.DisplayFormat.FormatString = "";
		this.TxtDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDate.Properties.EditFormat.FormatString = "";
		this.TxtDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDate.Properties.Mask.EditMask = "yyyy/MM/dd HH:mm:ss tt";
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate.Size = new System.Drawing.Size(189, 22);
		this.TxtDate.TabIndex = 0;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label21.Location = new System.Drawing.Point(585, 55);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(76, 17);
		this.label21.TabIndex = 113;
		this.label21.Text = "التاريخ Date";
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label20.Location = new System.Drawing.Point(585, 24);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(69, 17);
		this.label20.TabIndex = 112;
		this.label20.Text = "النوع Type";
		this.TxtType.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Location = new System.Drawing.Point(390, 22);
		this.TxtType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtType.Name = "TxtType";
		this.TxtType.Size = new System.Drawing.Size(190, 24);
		this.TxtType.TabIndex = 1;
		this.TxtType.SelectedIndexChanged += new System.EventHandler(TxtType_SelectedIndexChanged_1);
		this.TxtType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress);
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label18.Location = new System.Drawing.Point(979, 56);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(143, 17);
		this.label18.TabIndex = 107;
		this.label18.Text = "اسم العميل Cus Name";
		this.TxtSuppliers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSuppliers.Location = new System.Drawing.Point(676, 53);
		this.TxtSuppliers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
		this.TxtSuppliers.Size = new System.Drawing.Size(295, 22);
		this.TxtSuppliers.TabIndex = 3;
		this.TxtSuppliers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCust_ButtonClick);
		this.LBlSuppAccount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LBlSuppAccount.AutoSize = true;
		this.LBlSuppAccount.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LBlSuppAccount.Location = new System.Drawing.Point(1118, 29);
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		this.LBlSuppAccount.Size = new System.Drawing.Size(16, 17);
		this.LBlSuppAccount.TabIndex = 105;
		this.LBlSuppAccount.Text = "1";
		this.LBlSuppAccount.Visible = false;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label13.Location = new System.Drawing.Point(276, 487);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(137, 16);
		this.label13.TabIndex = 204;
		this.label13.Text = "المجموع بعد الخصم Net";
		this.label13.Visible = false;
		this.TxtAllVat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtAllVat.DecimalDigits = 3;
		this.TxtAllVat.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtAllVat.ForeColor = System.Drawing.Color.Black;
		this.TxtAllVat.Location = new System.Drawing.Point(179, 454);
		this.TxtAllVat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtAllVat.Name = "TxtAllVat";
		this.TxtAllVat.ReadOnly = true;
		this.TxtAllVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtAllVat.Size = new System.Drawing.Size(92, 23);
		this.TxtAllVat.TabIndex = 202;
		this.TxtAllVat.Text = "0.000";
		this.TxtAllVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllVat.Value = new decimal(new int[4]);
		this.TxtAllVat.Visible = false;
		this.TxtAllVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label12.Location = new System.Drawing.Point(276, 458);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(119, 16);
		this.label12.TabIndex = 201;
		this.label12.Text = "الضريبة المضافة VAT";
		this.label12.Visible = false;
		this.TxtTabocc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTabocc.DecimalDigits = 3;
		this.TxtTabocc.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTabocc.ForeColor = System.Drawing.Color.Black;
		this.TxtTabocc.Location = new System.Drawing.Point(179, 426);
		this.TxtTabocc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtTabocc.Name = "TxtTabocc";
		this.TxtTabocc.ReadOnly = true;
		this.TxtTabocc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTabocc.Size = new System.Drawing.Size(92, 23);
		this.TxtTabocc.TabIndex = 200;
		this.TxtTabocc.Text = "0.000";
		this.TxtTabocc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTabocc.Value = new decimal(new int[4]);
		this.TxtTabocc.Visible = false;
		this.TxtTabocc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label11.Location = new System.Drawing.Point(276, 430);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(106, 16);
		this.label11.TabIndex = 199;
		this.label11.Text = "رسوم التبغ 100%";
		this.label11.Visible = false;
		this.TxtTotalBeforDisc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTotalBeforDisc.DecimalDigits = 3;
		this.TxtTotalBeforDisc.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtTotalBeforDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalBeforDisc.Location = new System.Drawing.Point(179, 483);
		this.TxtTotalBeforDisc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtTotalBeforDisc.Name = "TxtTotalBeforDisc";
		this.TxtTotalBeforDisc.ReadOnly = true;
		this.TxtTotalBeforDisc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotalBeforDisc.Size = new System.Drawing.Size(92, 24);
		this.TxtTotalBeforDisc.TabIndex = 178;
		this.TxtTotalBeforDisc.Text = "0.000";
		this.TxtTotalBeforDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalBeforDisc.Value = new decimal(new int[4]);
		this.TxtTotalBeforDisc.Visible = false;
		this.TxtTotalBeforDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalBeforDisc.TextChanged += new System.EventHandler(TxtTotalBeforDisc_TextChanged);
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label5.Location = new System.Drawing.Point(409, 522);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(46, 16);
		this.label5.TabIndex = 171;
		this.label5.Text = "الضريبة";
		this.label5.Visible = false;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label6.Location = new System.Drawing.Point(521, 412);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(54, 16);
		this.label6.TabIndex = 172;
		this.label6.Text = "المجموع";
		this.label6.Visible = false;
		this.TxtTotalAll.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.TxtTotalAll.DecimalDigits = 3;
		this.TxtTotalAll.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotalAll.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalAll.Location = new System.Drawing.Point(424, 407);
		this.TxtTotalAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtTotalAll.Name = "TxtTotalAll";
		this.TxtTotalAll.ReadOnly = true;
		this.TxtTotalAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotalAll.Size = new System.Drawing.Size(92, 23);
		this.TxtTotalAll.TabIndex = 167;
		this.TxtTotalAll.Text = "0.000";
		this.TxtTotalAll.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalAll.Value = new decimal(new int[4]);
		this.TxtTotalAll.Visible = false;
		this.TxtTotalAll.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalAll.TextChanged += new System.EventHandler(TxtTotals_TextChanged);
		this.TxtVatBeforeDisc.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.TxtVatBeforeDisc.DecimalDigits = 3;
		this.TxtVatBeforeDisc.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtVatBeforeDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatBeforeDisc.Location = new System.Drawing.Point(312, 517);
		this.TxtVatBeforeDisc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtVatBeforeDisc.Name = "TxtVatBeforeDisc";
		this.TxtVatBeforeDisc.ReadOnly = true;
		this.TxtVatBeforeDisc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVatBeforeDisc.Size = new System.Drawing.Size(92, 23);
		this.TxtVatBeforeDisc.TabIndex = 179;
		this.TxtVatBeforeDisc.Text = "0.000";
		this.TxtVatBeforeDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatBeforeDisc.Value = new decimal(new int[4]);
		this.TxtVatBeforeDisc.Visible = false;
		this.TxtVatBeforeDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtVat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtVat.DecimalDigits = 3;
		this.TxtVat.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtVat.ForeColor = System.Drawing.Color.Teal;
		this.TxtVat.Location = new System.Drawing.Point(711, 526);
		this.TxtVat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.ReadOnly = true;
		this.TxtVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVat.Size = new System.Drawing.Size(151, 32);
		this.TxtVat.TabIndex = 174;
		this.TxtVat.Text = "0.000";
		this.TxtVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4]);
		this.TxtVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotals.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Location = new System.Drawing.Point(423, 366);
		this.TxtTotals.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.ReadOnly = true;
		this.TxtTotals.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotals.Size = new System.Drawing.Size(92, 23);
		this.TxtTotals.TabIndex = 173;
		this.TxtTotals.Text = "0.000";
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.Visible = false;
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.BtnMinus.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnMinus.Appearance.Options.UseFont = true;
		this.BtnMinus.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnMinus.ImageOptions.Image");
		this.BtnMinus.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnMinus.Location = new System.Drawing.Point(525, 134);
		this.BtnMinus.Margin = new System.Windows.Forms.Padding(4);
		this.BtnMinus.Name = "BtnMinus";
		this.BtnMinus.Size = new System.Drawing.Size(136, 32);
		this.BtnMinus.TabIndex = 176;
		this.BtnMinus.Text = "نقصان";
		this.BtnMinus.Click += new System.EventHandler(BtnMinus_Click);
		this.BtnPlus.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPlus.Appearance.Options.UseFont = true;
		this.BtnPlus.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPlus.ImageOptions.Image");
		this.BtnPlus.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPlus.Location = new System.Drawing.Point(523, 175);
		this.BtnPlus.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPlus.Name = "BtnPlus";
		this.BtnPlus.Size = new System.Drawing.Size(136, 32);
		this.BtnPlus.TabIndex = 175;
		this.BtnPlus.Text = "زيادة";
		this.BtnPlus.Click += new System.EventHandler(BtnPlus_Click);
		this.TxtBarcode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtBarcode.Location = new System.Drawing.Point(680, 141);
		this.TxtBarcode.Name = "TxtBarcode";
		this.TxtBarcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtBarcode.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		this.TxtBarcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, imageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.TxtBarcode.Size = new System.Drawing.Size(421, 30);
		this.TxtBarcode.TabIndex = 0;
		this.TxtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(gridControl1_KeyDown);
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label7.Location = new System.Drawing.Point(519, 370);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(98, 16);
		this.label7.TabIndex = 181;
		this.label7.Text = "المجموع بالخصم";
		this.label7.Visible = false;
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.RecoveredMenuItemFix1, this.toolStripSeparator1, this.MenuNewItem, this.RecoveredMenuItemFix2, this.MenuSearchItem, this.toolStripSeparator4,
			this.MenuPurchOrder, this.RecoveredMenuItemFix3, this.RecoveredMenuItemFix7, this.toolStripSeparator2, this.RecoveredMenuItemFix11, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.contextMenuStrip1.Size = new System.Drawing.Size(196, 314);
		this.MenuNew.Name = "MenuNew";
		this.MenuNew.Size = new System.Drawing.Size(195, 22);
		this.MenuNew.Text = "جديد                       F4";
		this.MenuNew.Click += new System.EventHandler(MenuNew_Click);
		this.MenuSave.Name = "MenuSave";
		this.MenuSave.Size = new System.Drawing.Size(195, 22);
		this.MenuSave.Text = "حفظ                       F5";
		this.MenuSave.Click += new System.EventHandler(BtnSave_Click);
		this.MenuEdit.Name = "MenuEdit";
		this.MenuEdit.Size = new System.Drawing.Size(195, 22);
		this.MenuEdit.Text = "تعديل                      F6";
		this.MenuEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.MenuDelete.Name = "MenuDelete";
		this.MenuDelete.Size = new System.Drawing.Size(195, 22);
		this.MenuDelete.Text = "حذف                       F7";
		this.MenuDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.RecoveredMenuItemFix1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.MenuPrintQued, this.MenuPreviewFatoura, this.MenuPrintFatoura });
		this.RecoveredMenuItemFix1.Name = "طباعةToolStripMenuItem1";
		this.RecoveredMenuItemFix1.Size = new System.Drawing.Size(195, 22);
		this.RecoveredMenuItemFix1.Text = "طباعة ";
		this.MenuPrintQued.Name = "MenuPrintQued";
		this.MenuPrintQued.Size = new System.Drawing.Size(204, 22);
		this.MenuPrintQued.Text = "معاينة القيد";
		this.MenuPrintQued.Click += new System.EventHandler(MenuPrintQued_Click);
		this.MenuPreviewFatoura.Name = "MenuPreviewFatoura";
		this.MenuPreviewFatoura.Size = new System.Drawing.Size(204, 22);
		this.MenuPreviewFatoura.Text = "معاينة الفاتورة";
		this.MenuPreviewFatoura.Click += new System.EventHandler(MenuPreviewFatoura_Click_1);
		this.MenuPrintFatoura.Name = "MenuPrintFatoura";
		this.MenuPrintFatoura.Size = new System.Drawing.Size(204, 22);
		this.MenuPrintFatoura.Text = "طباعة الفاتورة       Ctrl + P";
		this.MenuPrintFatoura.Click += new System.EventHandler(MenuPrintFatoura_Click_1);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
		this.MenuNewItem.Name = "MenuNewItem";
		this.MenuNewItem.Size = new System.Drawing.Size(195, 22);
		this.MenuNewItem.Text = "صنف جديد            F12 ";
		this.MenuNewItem.Click += new System.EventHandler(MenuNewItem_Click);
		this.RecoveredMenuItemFix2.Name = "حركةصنفToolStripMenuItem";
		this.RecoveredMenuItemFix2.Size = new System.Drawing.Size(195, 22);
		this.RecoveredMenuItemFix2.Text = "حركة صنف";
		this.MenuSearchItem.Name = "MenuSearchItem";
		this.MenuSearchItem.Size = new System.Drawing.Size(195, 22);
		this.MenuSearchItem.Text = "بحث عن صنف        F3";
		this.MenuSearchItem.Click += new System.EventHandler(MenuSearchItem_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(192, 6);
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		this.MenuPurchOrder.Size = new System.Drawing.Size(195, 22);
		this.MenuPurchOrder.Text = "تحميل من عرض سعر  F9";
		this.MenuPurchOrder.Click += new System.EventHandler(MenuPurchOrder_Click);
		this.RecoveredMenuItemFix3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[14]
		{
			this.RecoveredMenuItemFix4, this.RecoveredMenuItemFix5, this.RecoveredMenuItemFix6, this.RecoveredMenuItemFix12, this.RecoveredMenuItemFix13, this.RecoveredMenuItemFix14, this.RecoveredMenuItemFix15, this.RecoveredMenuItemFix16, this.RecoveredMenuItemFix17, this.ctrlToolStripMenuItem,
			this.RecoveredMenuItemFix18, this.RecoveredMenuItemFix19, this.RecoveredMenuItemFix20, this.RecoveredMenuItemFix21
		});
		this.RecoveredMenuItemFix3.Name = "استعلامToolStripMenuItem1";
		this.RecoveredMenuItemFix3.Size = new System.Drawing.Size(195, 22);
		this.RecoveredMenuItemFix3.Text = "دليل الاختصارات";
		this.RecoveredMenuItemFix4.Name = "رقمالفاتورةToolStripMenuItem1";
		this.RecoveredMenuItemFix4.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix4.Text = "F2 الباركود";
		this.RecoveredMenuItemFix5.Name = "اسمالموردToolStripMenuItem1";
		this.RecoveredMenuItemFix5.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix5.Text = "F3 بحث عن صنف";
		this.RecoveredMenuItemFix6.Name = "نوعالفاتورةToolStripMenuItem1";
		this.RecoveredMenuItemFix6.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix6.Text = "F4 فاتورة جديدة";
		this.RecoveredMenuItemFix12.Name = "f5حفظالفاتورةToolStripMenuItem";
		this.RecoveredMenuItemFix12.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix12.Text = "F5 حفظ الفاتورة";
		this.RecoveredMenuItemFix13.Name = "f6تعديلالفاتورةToolStripMenuItem";
		this.RecoveredMenuItemFix13.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix13.Text = "F6 تعديل الفاتورة";
		this.RecoveredMenuItemFix14.Name = "f7حذفالفاتورةToolStripMenuItem";
		this.RecoveredMenuItemFix14.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix14.Text = "F7 حذف الفاتورة";
		this.RecoveredMenuItemFix15.Name = "f9بحثفيعروضالاسعارToolStripMenuItem";
		this.RecoveredMenuItemFix15.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix15.Text = "F9 بحث في عروض الاسعار";
		this.RecoveredMenuItemFix16.Name = "f12فتحكرتالصنفToolStripMenuItem";
		this.RecoveredMenuItemFix16.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix16.Text = "F12 فتح كرت الصنف";
		this.RecoveredMenuItemFix17.Name = "ctrlDeleteحذفالسطرالحاليToolStripMenuItem";
		this.RecoveredMenuItemFix17.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix17.Text = "Ctrl && Delete  حذف السطر الحالي";
		this.ctrlToolStripMenuItem.Name = "ctrlToolStripMenuItem";
		this.ctrlToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
		this.ctrlToolStripMenuItem.Text = "Ctrl && + زيادة كمية السطر الحالي";
		this.RecoveredMenuItemFix18.Name = "ctrlنقصانكميةالسطرالحاليToolStripMenuItem";
		this.RecoveredMenuItemFix18.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix18.Text = "Ctrl && - نقصان كمية السطر الحالي";
		this.RecoveredMenuItemFix19.Name = "ctrlPطباعةالفاتورةToolStripMenuItem";
		this.RecoveredMenuItemFix19.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix19.Text = "Ctrl && P طباعة الفاتورة";
		this.RecoveredMenuItemFix20.Name = "ctrlLقفلمؤقتToolStripMenuItem";
		this.RecoveredMenuItemFix20.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix20.Text = "Ctrl && L قفل مؤقت";
		this.RecoveredMenuItemFix21.Name = "f11اظهارالكمياتلكلالمخازنToolStripMenuItem";
		this.RecoveredMenuItemFix21.Size = new System.Drawing.Size(245, 22);
		this.RecoveredMenuItemFix21.Text = "F11  اظهار الكميات لكل المخازن";
		this.RecoveredMenuItemFix7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix8, this.RecoveredMenuItemFix9, this.RecoveredMenuItemFix10 });
		this.RecoveredMenuItemFix7.Name = "الكمياتToolStripMenuItem1";
		this.RecoveredMenuItemFix7.Size = new System.Drawing.Size(195, 22);
		this.RecoveredMenuItemFix7.Text = "الكميات";
		this.RecoveredMenuItemFix8.Name = "الكميةالحاليةToolStripMenuItem";
		this.RecoveredMenuItemFix8.Size = new System.Drawing.Size(197, 22);
		this.RecoveredMenuItemFix8.Text = "الكمية الحالية";
		this.RecoveredMenuItemFix9.Name = "كلالكمياتبالمستودعاتToolStripMenuItem";
		this.RecoveredMenuItemFix9.Size = new System.Drawing.Size(197, 22);
		this.RecoveredMenuItemFix9.Text = "كل الكميات بالمستودعات";
		this.RecoveredMenuItemFix10.Name = "كلالكمياتبالفروعToolStripMenuItem";
		this.RecoveredMenuItemFix10.Size = new System.Drawing.Size(197, 22);
		this.RecoveredMenuItemFix10.Text = "كل الكميات بالفروع";
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
		this.RecoveredMenuItemFix11.Name = "تخصيصToolStripMenuItem1";
		this.RecoveredMenuItemFix11.Size = new System.Drawing.Size(195, 22);
		this.RecoveredMenuItemFix11.Text = "تخصيص";
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(192, 6);
		this.MenuExit.Name = "MenuExit";
		this.MenuExit.Size = new System.Drawing.Size(195, 22);
		this.MenuExit.Text = "خروج";
		this.MenuExit.Click += new System.EventHandler(MenuExit_Click);
		this.TxtCost.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtCost.DecimalDigits = 3;
		this.TxtCost.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtCost.ForeColor = System.Drawing.Color.Black;
		this.TxtCost.Location = new System.Drawing.Point(423, 445);
		this.TxtCost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtCost.Name = "TxtCost";
		this.TxtCost.ReadOnly = true;
		this.TxtCost.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtCost.Size = new System.Drawing.Size(92, 23);
		this.TxtCost.TabIndex = 195;
		this.TxtCost.Text = "0.000";
		this.TxtCost.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCost.Value = new decimal(new int[4]);
		this.TxtCost.Visible = false;
		this.TxtCost.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label9.Location = new System.Drawing.Point(519, 450);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(81, 16);
		this.label9.TabIndex = 194;
		this.label9.Text = "تكلفة الفاتورة";
		this.label9.Visible = false;
		this.BtnReturnCashier.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnReturnCashier.Appearance.Options.UseFont = true;
		this.BtnReturnCashier.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReturnCashier.ImageOptions.Image");
		this.BtnReturnCashier.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnReturnCashier.Location = new System.Drawing.Point(359, 134);
		this.BtnReturnCashier.Margin = new System.Windows.Forms.Padding(4);
		this.BtnReturnCashier.Name = "BtnReturnCashier";
		this.BtnReturnCashier.Size = new System.Drawing.Size(157, 73);
		this.BtnReturnCashier.TabIndex = 196;
		this.BtnReturnCashier.Text = "مرتجعات .Rtn";
		this.BtnReturnCashier.Click += new System.EventHandler(BtnReturnCashier_Click);
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label14.Location = new System.Drawing.Point(658, 534);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(46, 16);
		this.label14.TabIndex = 205;
		this.label14.Text = "الضريبة";
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label15.Location = new System.Drawing.Point(895, 534);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(57, 16);
		this.label15.TabIndex = 206;
		this.label15.Text = "الاجمالي";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1113, 566);
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.label15);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.BtnReturnCashier);
		base.Controls.Add(this.TxtCost);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.label9);
		base.Controls.Add(this.TxtAllVat);
		base.Controls.Add(this.TxtVat);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.TxtTabocc);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.TxtTotals);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.TxtVatBeforeDisc);
		base.Controls.Add(this.TxtTotalAll);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.TxtBarcode);
		base.Controls.Add(this.BtnMinus);
		base.Controls.Add(this.TxtTotalBeforDisc);
		base.Controls.Add(this.BtnPlus);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnPrint);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.gridControl1);
		base.Controls.Add(this.TxtNet22);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		base.Name = "FrmCashirPharm";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "الكاشير ";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmCashir_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4Units).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


