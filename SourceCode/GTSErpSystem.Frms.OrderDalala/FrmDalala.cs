using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.OrderDalala;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.OrderDalala.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.OrderDalala;

public class FrmDalala : XtraForm
{
	private Class_Dalala PurRTN = new Class_Dalala();

	private Class_Sending Pur = new Class_Sending();

	private Class_Items ITM = new Class_Items();

	private Class_Customer Cust = new Class_Customer();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private GTSdbEntities db = new GTSdbEntities();

	private DataTable datatable = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private int tafqitID = LoginDetails.TafqitID;

	public string txtWordAr;

	public string txtWordEn;

	public string CodeItm;

	private static int IsShow;

	private static int PrintID;

	private int intType = 1;

	public decimal QuanITM;

	public bool ISEVENTDISC = true;

	private int DecimalNum = LoginDetails.DecimalNum;

	public int UserSellPrice = LoginDetails.UserSellPrice;

	private IContainer components = null;

	private GridColumn gridColumn2;

	private GridColumn gridColumn1;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private GridColumn gridColumn5;

	private ToolStripMenuItem MenuExit;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem RecoveredMenuItemFix1;

	private NumericEditBox txtdicoutprege;

	private TextEdit TxtTafqit;

	private ToolStripMenuItem RecoveredMenuItemFix2;

	private ToolStripMenuItem MenuPrintQued;

	private ToolStripMenuItem MenuPreviewFatoura;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem RecoveredMenuItemFix3;

	private ToolStripMenuItem RecoveredMenuItemFix4;

	private ToolStripMenuItem RecoveredMenuItemFix5;

	private ToolStripMenuItem RecoveredMenuItemFix6;

	private ToolStripMenuItem MenuSearchItem;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem MenuNew;

	private ToolStripMenuItem MenuSave;

	private ToolStripMenuItem MenuEdit;

	private ToolStripMenuItem MenuDelete;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuNewItem;

	private ToolStripMenuItem RecoveredMenuItemFix7;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem MenuPurchOrder;

	private ToolStripMenuItem RecoveredMenuItemFix8;

	private ToolStripMenuItem RecoveredMenuItemFix9;

	private ToolStripMenuItem RecoveredMenuItemFix10;

	private ToolStripMenuItem RecoveredMenuItemFix11;

	private GridColumn gridColumn9;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn shearch;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private GridColumn gridColumn19;

	private GridColumn gridColumn4;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private GridColumn gridColumn22;

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

	private GroupBox groupBox3;

	private NumericEditBox TxtNet22;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private Label label7;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtTotals;

	private SimpleButton BtnExit;

	private NumericEditBox TxtTotalAll;

	private NumericEditBox TxtTotalBeforDisc;

	private NumericEditBox TxtNet;

	private Label label8;

	private Label LBlSuppAccount;

	private ButtonEdit TxtPurCode;

	private Label label26;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private Label LblSup;

	private DateEdit TxtDate;

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private Label label21;

	private Label label20;

	private Label label18;

	private ButtonEdit TxtSuppliers;

	private Label label17;

	private ButtonEdit TxtCode;

	private GroupBox groupBox1;

	private RibbonPage ribbonPage2;

	private Label label1;

	private Label label2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private Label label3;

	private Label label4;

	private NumericEditBox TxtDiscount;

	private NumericEditBox TxtVatBeforeDisc;

	private Label label5;

	private Label label6;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private GroupBox groupBox2;

	private GridColumn gridColumn23;

	private GridColumn gridColumn24;

	private NumericEditBox TxtCost;

	private NumericEditBox TxtTabocc;

	private Label label29;

	private NumericEditBox TxtAllVat;

	private Label label28;

	private NumericEditBox TxtAllTotal;

	private Label label27;

	private Label label30;

	private GridColumn gridColumn25;

	private RepositoryItemButtonEdit BtnPlus;

	private Label label24;

	private TextEdit TxtCity;

	private Label label23;

	private TextEdit TxtNameDriver;

	private Label label19;

	private TextEdit TxtDriverPhone;

	private Label label31;

	private TextEdit TxtCarNum;

	private NumericEditBox TxtExpenses;

	private Label label13;

	private NumericEditBox TxtEmployee;

	private Label label12;

	private NumericEditBox TxtMarket;

	private Label label11;

	private NumericEditBox TxtCareRent;

	private Label label10;

	private Label label9;

	private NumericEditBox TxtBounce;

	private Label label14;

	private NumericUpDown TxtBouncePerc;

	private Label label15;

	private TextEdit TxtExpensesText;

	private SimpleButton BtnPayment;

	private GridColumn gridColumn26;

	private GridColumn gridColumn27;

	private Label LblCust;

	private Label label22;

	private ButtonEdit TxtCustomer;

	private Label label25;

	private System.Windows.Forms.ComboBox TxtType;

	private Label LblCustAccount;

	public FrmDalala()
	{
		InitializeComponent();
		TxtDigital();
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		TxtDate.DateTime = DateTime.Now;
		SetInitialRow();
		gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
		base.KeyPreview = true;
		gridView1.Columns["الكمية"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["الكمية"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["سعر الوحدة"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["سعر الوحدة"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.FocusedRowHandle = 1;
		gridView1.FocusedColumn = gridView1.VisibleColumns[3];
		gridView1.ShowEditor();
		GroupPermission();
		BindCBCostCenter();
		BindComboBox();
		TxtDate.Focus();
		LblCust.Text = LoginDetails.Acc_CustomerCash.ToString();
		LblCustAccount.Text = PurRTN.GetIDAccount(Convert.ToInt32(LblCust.Text)).ToString();
		TxtCustomer.Text = "عميل نقدى";
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		gridView1.OptionsBehavior.Editable = false;
		Clear();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
	}

	private void gridView1_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
	{
	}

	private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
	{
	}

	private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
	{
		if (e.Column.FieldName == "الكمية")
		{
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كمية الارسالية");
			string value = Convert.ToString(e.Value);
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText5 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText2 != "")
			{
				Item_Items item_Items = PurRTN.GetItems(focusedRowCellDisplayText2).FirstOrDefault();
				if (item_Items != null)
				{
					foreach (DataRow row in datatable.Rows)
					{
						if (row["ظ…"].ToString() == focusedRowCellDisplayText5)
						{
							decimal num = Convert.ToDecimal(row["سعر الوحدة"].ToString());
							if (row["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]), DecimalNum);
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(num * Convert.ToDecimal(value)), DecimalNum);
								if (PurRTN.IsVatActive())
								{
									if (item_Items.Is_Tax == true)
									{
										decimal value2 = num * Convert.ToDecimal(value);
										decimal? tax_Value = item_Items.Tax_Value;
										row["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
										decimal value3 = num * Convert.ToDecimal(value);
										decimal? tax_Value2 = item_Items.Tax_Value;
										row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
										row["هل_ضريبة"] = 1;
										row["قيمة_الضريبة"] = item_Items.Tax_Value;
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
								decimal num2 = Convert.ToDecimal(item_Items.ConvertMediumUnit);
								row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * (Convert.ToDecimal(row["الكمية"]) * num2), DecimalNum);
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(num * Convert.ToDecimal(value)), DecimalNum);
								if (PurRTN.IsVatActive())
								{
									if (item_Items.Is_Tax == true)
									{
										decimal value2 = num * Convert.ToDecimal(value);
										decimal? tax_Value = item_Items.Tax_Value;
										row["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
										decimal value3 = num * Convert.ToDecimal(value);
										decimal? tax_Value2 = item_Items.Tax_Value;
										row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
										row["هل_ضريبة"] = 1;
										row["قيمة_الضريبة"] = item_Items.Tax_Value;
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
							if (row["أسم_الوحدة"].ToString() == "الكبرى")
							{
								decimal num3 = Convert.ToDecimal(item_Items.ConvertBigUnit);
								row["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]) * (Convert.ToDecimal(row["الكمية"]) * num3), DecimalNum);
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(num * Convert.ToDecimal(value)), DecimalNum);
								if (PurRTN.IsVatActive())
								{
									if (item_Items.Is_Tax == true)
									{
										decimal value2 = num * Convert.ToDecimal(value);
										decimal? tax_Value = item_Items.Tax_Value;
										row["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
										decimal value3 = num * Convert.ToDecimal(value);
										decimal? tax_Value2 = item_Items.Tax_Value;
										row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num * Convert.ToDecimal(value))), DecimalNum);
										row["هل_ضريبة"] = 1;
										row["قيمة_الضريبة"] = item_Items.Tax_Value;
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
							}
						}
						ArrangeDatatable();
						Cal_FinalResultTotals();
					}
					insertnewrow();
					CheckName();
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
			string focusedRowCellDisplayText6 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText7 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText8 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText9 = gridView1.GetFocusedRowCellDisplayText("الكمية");
			string focusedRowCellDisplayText10 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText6 != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(focusedRowCellDisplayText6).FirstOrDefault();
				if (get_Item_ByCodeALL_Result != null)
				{
					foreach (DataRow row2 in datatable.Rows)
					{
						if (!(row2["ظ…"].ToString() == focusedRowCellDisplayText10))
						{
							continue;
						}
						if (row2["أسم_الوحدة"].ToString() == "الصغرى")
						{
							decimal sellPermission = PurRTN.GetSellPermission(focusedRowCellDisplayText6, UserSellPrice, "الصغرى");
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
							row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9)), DecimalNum);
							if (PurRTN.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result.Is_Tax == true)
								{
									decimal value2 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9);
									decimal? tax_Value = get_Item_ByCodeALL_Result.Tax_Value;
									row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
									decimal value3 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9);
									decimal? tax_Value2 = get_Item_ByCodeALL_Result.Tax_Value;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9))), DecimalNum);
									row2["هل_ضريبة"] = 1;
									row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
								}
								else
								{
									row2["هل_ضريبة"] = 0;
									row2["قيمة_الضريبة"] = 0;
									row2["الضربية"] = 0;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9))), DecimalNum);
								}
							}
							else
							{
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9))), DecimalNum);
							}
							break;
						}
						if (row2["أسم_الوحدة"].ToString() == "المتوسطة")
						{
							decimal sellPermission2 = PurRTN.GetSellPermission(focusedRowCellDisplayText6, UserSellPrice, "المتوسطة");
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
							row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9)), DecimalNum);
							if (PurRTN.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result.Is_Tax == true)
								{
									decimal value2 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9);
									decimal? tax_Value = get_Item_ByCodeALL_Result.Tax_Value;
									row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
									decimal value3 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9);
									decimal? tax_Value2 = get_Item_ByCodeALL_Result.Tax_Value;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9))), DecimalNum);
									row2["هل_ضريبة"] = 1;
									row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
								}
								else
								{
									row2["هل_ضريبة"] = 0;
									row2["قيمة_الضريبة"] = 0;
									row2["الضربية"] = 0;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9))), DecimalNum);
								}
							}
							else
							{
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9))), DecimalNum);
							}
							break;
						}
						if (!(row2["أسم_الوحدة"].ToString() == "الكبرى"))
						{
							continue;
						}
						decimal sellPermission3 = PurRTN.GetSellPermission(focusedRowCellDisplayText6, UserSellPrice, "الكبرى");
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
						row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9)), DecimalNum);
						if (PurRTN.IsVatActive())
						{
							if (get_Item_ByCodeALL_Result.Is_Tax == true)
							{
								row2["هل_ضريبة"] = 1;
								row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
								decimal value2 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9);
								decimal? tax_Value = get_Item_ByCodeALL_Result.Tax_Value;
								row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value / (decimal?)100), DecimalNum);
								decimal value3 = Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9);
								decimal? tax_Value2 = get_Item_ByCodeALL_Result.Tax_Value;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9))), DecimalNum);
							}
							else
							{
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9))), DecimalNum);
							}
						}
						else
						{
							row2["هل_ضريبة"] = 0;
							row2["قيمة_الضريبة"] = 0;
							row2["الضربية"] = 0;
							row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value4) * Convert.ToDecimal(focusedRowCellDisplayText9))), DecimalNum);
						}
						break;
					}
					CheckName();
					GetTotalCost();
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[4];
					gridView1.ShowEditor();
				}
			}
		}
		ArrangeDatatable();
		Cal_FinalResultTotals();
	}

	private void gridView1_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
	{
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
		string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
		string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
		string StoreName = gridView1.GetFocusedRowCellDisplayText("المخزن");
		string text = gridView1.GetFocusedRowCellDisplayText("كمية الارسالية");
		int num = (from x in db.Account_Stores
			where x.Store_Name == StoreName
			select x.ID).FirstOrDefault();
		CodeItm = focusedRowCellDisplayText;
		if (text == "")
		{
			text = "0";
		}
		QuanITM = Convert.ToDecimal(text);
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
		try
		{
			FrmSearchDalala frmSearchDalala = new FrmSearchDalala();
			frmSearchDalala.ShowDialog();
			if (frmSearchDalala.CustCode > 0)
			{
				int custCode = frmSearchDalala.CustCode;
				OrderDalala_Dalala orderDalala_Dalala = PurRTN.GetDalala(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<OrderDalala_DalalaDetails> itemsPurchesByID = PurRTN.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (OrderDalala_DalalaDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int num2 = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = PurRTN.GetItemsByitemid(num2).FirstOrDefault();
					decimal totalQantitysending = PurRTN.GetTotalQantitysending(Convert.ToInt32(orderDalala_Dalala.FromSendingID), num2);
					dataRow = datatable.NewRow();
					dataRow["كود_الصنف"] = item_Items.Item_code;
					dataRow["أسم الصنف"] = item_Items.item_Name;
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
					dataRow["كمية الارسالية"] = totalQantitysending;
					dataRow["الكمية"] = item.Quantity;
					dataRow["كمية الهدر"] = item.QuantityMinus;
					dataRow["سعر الوحدة"] = item.UnitPrice;
					dataRow["متوسط_التكلفة"] = item.LastCost;
					dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
					dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
					dataRow["الشرح"] = item.ItemNote;
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
				TxtCode.Text = orderDalala_Dalala.PurBranchID.ToString();
				TxtPurCode.Text = orderDalala_Dalala.FromSendingID.ToString();
				TxtType.SelectedValue = orderDalala_Dalala.TypeID;
				LblCust.Text = orderDalala_Dalala.CustID.ToString();
				LblCustAccount.Text = PurRTN.GetIDAccount(Convert.ToInt32(LblCust.Text)).ToString();
				TxtCustomer.Text = orderDalala_Dalala.CustName;
				LblSup.Text = orderDalala_Dalala.SupplierID.ToString();
				LBlSuppAccount.Text = PurRTN.GetIDAccountSupp(Convert.ToInt32(LblSup.Text)).ToString();
				TxtSuppliers.Text = orderDalala_Dalala.SupplierName;
				TxtNameDriver.Text = orderDalala_Dalala.DriverName;
				TxtDriverPhone.Text = orderDalala_Dalala.DriverPhone;
				TxtCarNum.Text = orderDalala_Dalala.CarNum;
				TxtCity.Text = orderDalala_Dalala.City;
				TxtDate.DateTime = Convert.ToDateTime(orderDalala_Dalala.Purchases_Date);
				TxtBouncePerc.Text = orderDalala_Dalala.BouncePerc.ToString();
				TxtBounce.Text = orderDalala_Dalala.Bounce.ToString();
				TxtExpenses.Text = orderDalala_Dalala.Expenses.ToString();
				TxtExpensesText.Text = orderDalala_Dalala.ExpensesText;
				TxtEmployee.Text = orderDalala_Dalala.Employee.ToString();
				TxtCareRent.Text = orderDalala_Dalala.CareRent.ToString();
				TxtMarket.Text = orderDalala_Dalala.Market.ToString();
				TxtTotals.Text = Convert.ToString(orderDalala_Dalala.TotalPrices);
				TxtVat.Text = Convert.ToString(orderDalala_Dalala.Tax);
				TxtTotalAll.Text = orderDalala_Dalala.Safy.ToString();
				TxtDiscount.Text = orderDalala_Dalala.DiscountNum.ToString();
				TxtTotalBeforDisc.Text = orderDalala_Dalala.TotalPrices_Discount.ToString();
				TxtVatBeforeDisc.Text = orderDalala_Dalala.Tax_Discount.ToString();
				TxtNet.Text = orderDalala_Dalala.Net.ToString();
				TxtNet22.Text = orderDalala_Dalala.Net.ToString();
				TxtTabocc.Text = orderDalala_Dalala.TobaccoTax.ToString();
				TxtAllVat.Text = orderDalala_Dalala.AllTax.ToString();
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = true;
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = false;
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
			FrmSearchSending frmSearchSending = new FrmSearchSending();
			frmSearchSending.ShowDialog();
			if (frmSearchSending.CustCode > 0)
			{
				int custCode = frmSearchSending.CustCode;
				OrderDalala_Sending orderDalala_Sending = Pur.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<OrderDalala_SendingDetails> itemsPurchesByID = Pur.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (OrderDalala_SendingDetails item in itemsPurchesByID)
				{
					int num2 = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = Pur.GetItemsByitemid(num2).FirstOrDefault();
					decimal totalQantityDalala = PurRTN.GetTotalQantityDalala(custCode, num2);
					decimal? quantity = item.Quantity;
					if (!((totalQantityDalala < quantity.GetValueOrDefault()) & quantity.HasValue))
					{
						continue;
					}
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					dataRow["كود_الصنف"] = item_Items.Item_code;
					dataRow["أسم الصنف"] = item_Items.item_Name;
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
					dataRow["كمية الارسالية"] = item.Quantity - (decimal?)totalQantityDalala;
					dataRow["الكمية"] = 0.0;
					dataRow["كمية الهدر"] = 0.0;
					dataRow["متوسط_التكلفة"] = item_Items.LastCost;
					dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
					dataRow["سعر الوحدة"] = item_Items.SellPriceSmall;
					dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(dataRow["سعر الوحدة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
					dataRow["مسح"] = 0;
					if (item_Items.Is_Tax == true)
					{
						dataRow["الضربية"] = Math.Round(Convert.ToDecimal(dataRow["الاجمالى"]) * Convert.ToDecimal(item_Items.Tax_Value) / 100m, DecimalNum);
						dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(dataRow["الاجمالى"]) + Convert.ToDecimal(dataRow["الضربية"]), DecimalNum);
						dataRow["هل_ضريبة"] = 1;
						dataRow["قيمة_الضريبة"] = item_Items.Tax_Value;
					}
					else
					{
						dataRow["هل_ضريبة"] = 0;
						dataRow["قيمة_الضريبة"] = 0;
						dataRow["الضربية"] = Math.Round(Convert.ToDecimal(item_Items.Tax_Value), DecimalNum);
						dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(dataRow["الاجمالى"]) + Convert.ToDecimal(dataRow["الضربية"]), DecimalNum);
					}
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				PrintID = custCode;
				TxtCode.Text = "";
				CBCostCenter.SelectedValue = 0;
				TxtPurCode.Text = orderDalala_Sending.PurBranchID.ToString();
				LblSup.Text = orderDalala_Sending.SupplierID.ToString();
				LBlSuppAccount.Text = PurRTN.GetIDAccountSupp(Convert.ToInt32(LblSup.Text)).ToString();
				TxtSuppliers.Text = orderDalala_Sending.SupplierName;
				TxtDate.DateTime = DateTime.Now;
				TxtNameDriver.Text = orderDalala_Sending.DriverName;
				TxtDriverPhone.Text = orderDalala_Sending.DriverPhone;
				TxtCarNum.Text = orderDalala_Sending.CarNum;
				TxtCity.Text = orderDalala_Sending.City;
				TxtNameDriver.Text = orderDalala_Sending.DriverName;
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnDelete.Enabled = false;
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = true;
				ArrangeDatatable();
				IsEnableTrue();
				Cal_FinalResultTotals();
				if (datatable.Rows.Count == 0)
				{
					MessageBox.Show("تم عمل مبيعات لهذه الفاتورة الارسالية ( " + TxtPurCode.Text + " ) ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
	}

	private void TxtCustomer_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustCode > 0)
			{
				int FrmID = frmSearchCustomer.CustCode;
				Account_CustSup account_CustSup = (from x in Cust.GetAll()
					where x.CustSuppCode == FrmID && x.FrmCust == true
					select x).FirstOrDefault();
				LblCust.Text = FrmID.ToString();
				TxtCustomer.Text = account_CustSup.CustSuppName;
				LblCustAccount.Text = PurRTN.GetIDAccount(Convert.ToInt32(LblCust.Text)).ToString();
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
			TxtType.DataSource = Pur.GetAccountType();
			TxtType.DisplayMember = "Name";
			TxtType.ValueMember = "ID";
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
			CBCostCenter.DataSource = PurRTN.GetCostCenters();
			CBCostCenter.DisplayMember = "CostCentersName";
			CBCostCenter.ValueMember = "CostCentersID";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
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
			}
		}
		gridControl1.DataSource = datatable;
	}

	public void CheckName()
	{
		for (int num = datatable.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = datatable.Rows[num];
			if (dataRow["أسم الصنف"].ToString() == "")
			{
				dataRow.Delete();
				datatable.AcceptChanges();
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
		object summaryValue = gridView1.Columns["إجمالى المبلغ"].SummaryItem.SummaryValue;
		object summaryValue2 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
		TxtTotals.Text = Convert.ToString(summaryValue2);
		object summaryValue3 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
		TxtVat.Text = Convert.ToString(summaryValue3);
	}

	public void insertnewrow()
	{
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["سعر الوحدة"] = 0;
		dataRow["الكمية"] = 1;
		dataRow["المخزن"] = PurRTN.GetStoreName();
		dataRow["رقم المخزن"] = PurRTN.GetStoreID();
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
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

	public void SetInitialRow()
	{
		datatable.Columns.Add("كود_الصنف", typeof(string));
		datatable.Columns.Add("أسم الصنف", typeof(string));
		datatable.Columns.Add("الوحدة", typeof(string));
		datatable.Columns.Add("المخزن", typeof(string));
		datatable.Columns.Add("رقم المخزن", typeof(int));
		datatable.Columns.Add("كمية الارسالية", typeof(decimal));
		datatable.Columns.Add("الكمية", typeof(decimal));
		datatable.Columns.Add("كمية الهدر", typeof(decimal));
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
		datatable.Columns.Add("الضريبه_بعد_الخصم", typeof(decimal));
		datatable.Columns.Add("إجمالى المبلغ", typeof(string));
		datatable.Columns.Add("الشرح", typeof(string));
		datatable.Columns.Add("حذف الصنف", typeof(int));
		datatable.Columns.Add("ظ…", typeof(int));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تم", typeof(int));
		dtCurrentTable.Columns.Add("كود_الصنف", typeof(string));
		dtCurrentTable.Columns.Add("أسم الصنف", typeof(string));
		dtCurrentTable.Columns.Add("الوحدة", typeof(string));
		dtCurrentTable.Columns.Add("المخزن", typeof(string));
		dtCurrentTable.Columns.Add("رقم المخزن", typeof(int));
		dtCurrentTable.Columns.Add("كمية الارسالية", typeof(decimal));
		dtCurrentTable.Columns.Add("الكمية", typeof(decimal));
		dtCurrentTable.Columns.Add("كمية الهدر", typeof(decimal));
		dtCurrentTable.Columns.Add("متوسط_التكلفة", typeof(decimal));
		dtCurrentTable.Columns.Add("اجمالي_متوسط_التكلفة", typeof(decimal));
		dtCurrentTable.Columns.Add("سعر الوحدة", typeof(decimal));
		dtCurrentTable.Columns.Add("سعربعدالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("نسبةالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("إجمالى_بعدالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("هل_ضريبة", typeof(int));
		dtCurrentTable.Columns.Add("قيمة_الضريبة", typeof(decimal));
		dtCurrentTable.Columns.Add("أسم_الوحدة", typeof(string));
		dtCurrentTable.Columns.Add("الاجمالى", typeof(string));
		dtCurrentTable.Columns.Add("الضربية", typeof(decimal));
		dtCurrentTable.Columns.Add("الضريبه_بعد_الخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("إجمالى المبلغ", typeof(string));
		dtCurrentTable.Columns.Add("الشرح", typeof(string));
		dtCurrentTable.Columns.Add("حذف الصنف", typeof(int));
		dtCurrentTable.Columns.Add("ظ…", typeof(int));
		dtCurrentTable.Columns.Add("مسح", typeof(int));
		dtCurrentTable.Columns.Add("تم", typeof(int));
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["أسم_الوحدة"] = "الصغرى";
		dataRow["الكمية"] = 0;
		dataRow["متوسط_التكلفة"] = 0.0;
		dataRow["اجمالي_متوسط_التكلفة"] = 0.0;
		dataRow["سعر الوحدة"] = 0.0;
		dataRow["سعربعدالخصم"] = 0.0;
		dataRow["نسبةالخصم"] = 0.0;
		dataRow["إجمالى_بعدالخصم"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
		dataRow["الضربية"] = 0.0;
		dataRow["الضريبه_بعد_الخصم"] = 0.0;
		dataRow["إجمالى المبلغ"] = 0.0;
		dataRow["هل_ضريبة"] = 0;
		dataRow["قيمة_الضريبة"] = 0.0;
		dataRow["المخزن"] = PurRTN.GetStoreName();
		dataRow["مسح"] = 0;
		dataRow["تم"] = 0;
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridControl1.ProcessGridKey += gridControl1_ProcessGridKey;
		gridView1.RowCellClick += gridView1_RowCellClick;
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
				}
				else if (TxtDiscount.Text == "" || Convert.ToDecimal(TxtDiscount.Text) == 0m || Convert.ToDecimal(TxtDiscount.Text) > Convert.ToDecimal(TxtTotals.Text))
				{
					TxtDiscount.Text = "0.00";
					object summaryValue = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
					decimal value = Convert.ToDecimal(summaryValue);
					object summaryValue2 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
					decimal value2 = Convert.ToDecimal(summaryValue2);
					object summaryValue3 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
					TxtAllTotal.Text = Convert.ToString(summaryValue3);
					object summaryValue4 = gridView1.Columns["الضريبه_بعد_الخصم"].SummaryItem.SummaryValue;
					decimal num = Convert.ToDecimal(summaryValue4);
					object summaryValue5 = gridView1.Columns["اجمالي_متوسط_التكلفة"].SummaryItem.SummaryValue;
					TxtCost.Text = Convert.ToString(summaryValue5);
					TxtAllTotal.Text = value.ToString();
					TxtNet22.Text = Math.Round(Convert.ToDecimal(value), DecimalNum).ToString();
					TxtTotalAll.Text = Math.Round(Convert.ToDecimal(value) + Convert.ToDecimal(value2), DecimalNum).ToString();
					TxtTotalBeforDisc.Text = (Convert.ToDecimal(TxtAllTotal.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
					TxtTabocc.Text = TxtTotalBeforDisc.Text;
					TxtAllVat.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m * Convert.ToDecimal(1.05) - Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m).ToString();
					TxtNet.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m + Convert.ToDecimal(TxtAllVat.Text)).ToString();
					TxtVatBeforeDisc.Text = TxtVat.Text;
					TxtAllVat.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m * Convert.ToDecimal(1.05) - Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m).ToString();
					TxtNet22.Text = TxtNet.Text;
					foreach (DataRow row in datatable.Rows)
					{
						if (row["أسم_الوحدة"].ToString() != "")
						{
							row["سعربعدالخصم"] = row["سعر الوحدة"];
							row["إجمالى_بعدالخصم"] = row["الاجمالى"];
							row["الضريبه_بعد_الخصم"] = row["الضربية"];
						}
					}
				}
				else
				{
					object summaryValue6 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
					decimal value3 = Convert.ToDecimal(summaryValue6);
					object summaryValue7 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
					decimal value4 = Convert.ToDecimal(summaryValue7);
					object summaryValue8 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
					TxtAllTotal.Text = Convert.ToString(summaryValue8);
					object summaryValue9 = gridView1.Columns["الضريبه_بعد_الخصم"].SummaryItem.SummaryValue;
					decimal num2 = Convert.ToDecimal(summaryValue9);
					object summaryValue10 = gridView1.Columns["اجمالي_متوسط_التكلفة"].SummaryItem.SummaryValue;
					TxtCost.Text = Convert.ToString(summaryValue10);
					TxtAllTotal.Text = value3.ToString();
					decimal num3 = Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)) / Math.Round(Convert.ToDecimal(TxtAllTotal.Text), DecimalNum) * 100m;
					TxtNet22.Text = Math.Round(Convert.ToDecimal(value3), DecimalNum).ToString();
					TxtTotalAll.Text = Math.Round(Convert.ToDecimal(value3) + Convert.ToDecimal(value4), DecimalNum).ToString();
					TxtVatBeforeDisc.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * Convert.ToDecimal(1.05) - Convert.ToDecimal(TxtTotalBeforDisc.Text)).ToString();
					TxtTotalBeforDisc.Text = (Convert.ToDecimal(TxtAllTotal.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
					TxtTabocc.Text = TxtTotalBeforDisc.Text;
					TxtAllVat.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m * Convert.ToDecimal(1.05) - Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m).ToString();
					TxtNet.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m + Convert.ToDecimal(TxtAllVat.Text)).ToString();
					TxtNet22.Text = TxtNet.Text;
					foreach (DataRow row2 in datatable.Rows)
					{
						if (row2["أسم_الوحدة"].ToString() != "")
						{
							row2["سعربعدالخصم"] = Convert.ToDecimal(row2["سعر الوحدة"].ToString()) - num3 * Convert.ToDecimal(row2["سعر الوحدة"]) / 100m;
							row2["إجمالى_بعدالخصم"] = Convert.ToDecimal(row2["سعربعدالخصم"].ToString()) * Convert.ToDecimal(row2["الكمية"].ToString());
							row2["الضريبه_بعد_الخصم"] = Convert.ToDecimal(Convert.ToDecimal(row2["سعربعدالخصم"]) * Convert.ToDecimal(row2["الكمية"]) * Convert.ToDecimal(row2["قيمة_الضريبة"]) / 100m);
						}
					}
				}
			}
			else if (datatable.Rows.Count <= 0)
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
			}
			else if (TxtDiscount.Text == "" || Convert.ToDecimal(TxtDiscount.Text) == 0m || Convert.ToDecimal(TxtDiscount.Text) > Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				TxtTabocc.Text = "0.00";
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				object summaryValue11 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
				TxtTotals.Text = Convert.ToString(summaryValue11);
				object summaryValue12 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
				TxtVat.Text = Convert.ToString(summaryValue12);
				object summaryValue13 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
				TxtTotalBeforDisc.Text = Convert.ToString(summaryValue13);
				object summaryValue14 = gridView1.Columns["الضريبه_بعد_الخصم"].SummaryItem.SummaryValue;
				TxtVatBeforeDisc.Text = Convert.ToString(summaryValue14);
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)).ToString();
				TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)).ToString();
				object summaryValue15 = gridView1.Columns["اجمالي_متوسط_التكلفة"].SummaryItem.SummaryValue;
				TxtCost.Text = Convert.ToString(summaryValue15);
				TxtAllVat.Text = TxtVatBeforeDisc.Text;
				TxtAllTotal.Text = TxtTotalAll.Text;
				TxtTabocc.Text = "0.00";
				foreach (DataRow row3 in datatable.Rows)
				{
					if (row3["أسم_الوحدة"].ToString() != "")
					{
						row3["سعربعدالخصم"] = row3["سعر الوحدة"];
						row3["إجمالى_بعدالخصم"] = row3["الاجمالى"];
						row3["الضريبه_بعد_الخصم"] = row3["الضربية"];
					}
				}
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
			}
			else
			{
				decimal value5 = Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text);
				decimal num4 = Math.Round(Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum) * 100m) / Math.Round(Convert.ToDecimal(value5), DecimalNum), DecimalNum);
				foreach (DataRow row4 in datatable.Rows)
				{
					if (row4["أسم_الوحدة"].ToString() != "")
					{
						row4["سعربعدالخصم"] = Convert.ToDecimal(row4["سعر الوحدة"].ToString()) - Convert.ToDecimal(num4 * Convert.ToDecimal(row4["سعر الوحدة"].ToString()) / 100m);
						row4["إجمالى_بعدالخصم"] = Convert.ToDecimal(row4["سعربعدالخصم"].ToString()) * Convert.ToDecimal(row4["الكمية"].ToString());
						row4["الضريبه_بعد_الخصم"] = Convert.ToDecimal(Convert.ToDecimal(row4["سعربعدالخصم"]) * Convert.ToDecimal(row4["الكمية"]) * Convert.ToDecimal(row4["قيمة_الضريبة"]) / 100m);
					}
				}
				object summaryValue16 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
				TxtTotals.Text = Convert.ToString(summaryValue16);
				object summaryValue17 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
				TxtVat.Text = Convert.ToString(summaryValue17);
				object summaryValue18 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
				TxtTotalBeforDisc.Text = Convert.ToString(summaryValue18);
				object summaryValue19 = gridView1.Columns["الضريبه_بعد_الخصم"].SummaryItem.SummaryValue;
				TxtVatBeforeDisc.Text = Convert.ToString(summaryValue19);
				object summaryValue20 = gridView1.Columns["اجمالي_متوسط_التكلفة"].SummaryItem.SummaryValue;
				TxtCost.Text = Convert.ToString(summaryValue20);
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
				TxtNet22.Text = TxtNet.Text;
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
				TxtAllVat.Text = TxtVatBeforeDisc.Text;
				TxtAllTotal.Text = TxtTotalAll.Text;
				TxtTabocc.Text = "0.00";
			}
			Bounce();
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
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtCode.Text = "";
		datatable.Rows.Clear();
		dtCurrentTable.Rows.Clear();
		gridControl1.DataSource = datatable;
		GetTotalCost();
		TxtType.Text = "نقدى";
		TxtCustomer.Text = "عميل نقدي";
		TxtSuppliers.Text = "";
		TxtDiscount.Text = "";
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtSuppliers.Text = "";
		TxtTotalBeforDisc.Text = "0.00";
		TxtVatBeforeDisc.Text = "0.00";
		TxtTafqit.Text = "";
		TxtTotalAll.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtNet22.Text = "0.00";
		TxtPurCode.Text = "";
		TxtExpensesText.Text = "";
		TxtDate.DateTime = DateTime.Now;
		CBCostCenter.SelectedValue = 0;
		TxtDiscount.Enabled = true;
		TxtDate.Enabled = true;
		TxtNameDriver.Text = "";
		TxtDriverPhone.Text = "";
		TxtCarNum.Text = "";
		TxtCity.Text = "";
		TxtBouncePerc.Text = "7.00";
		TxtBounce.Text = "0.00";
		TxtExpenses.Text = "0.00";
		TxtEmployee.Text = "0.00";
		TxtCareRent.Text = "0.00";
		TxtMarket.Text = "0.00";
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
		IsEnableTrue();
		TxtSuppliers.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 107))
		{
			int num = 0;
			CheckFfistRow();
			if (datatable.Rows.Count > 0)
			{
				if (TxtSuppliers.Text != "")
				{
					Cal_FinalResultTotals();
					GetTotalCost();
					FiterDatatableItemsUpdate();
					num = PurRTN.InsertPurch(out var Code, Convert.ToInt32(TxtPurCode.Text), Convert.ToInt32(TxtType.SelectedValue), Convert.ToInt32(LblCust.Text), TxtCustomer.Text, Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtNameDriver.Text, TxtDriverPhone.Text, TxtDate.DateTime, TxtCarNum.Text, TxtCity.Text, TxtBouncePerc.Value, Convert.ToDecimal(TxtBounce.Value), TxtExpensesText.Text, Convert.ToDecimal(TxtExpenses.Value), Convert.ToDecimal(TxtEmployee.Value), Convert.ToDecimal(TxtCareRent.Value), Convert.ToDecimal(TxtMarket.Value), Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Convert.ToInt32(CBCostCenter.SelectedValue));
					PurRTN.InsertItems(datatable, num, Convert.ToInt32(TxtPurCode.Text));
					decimal totalSup = Convert.ToDecimal(TxtTotals.Text) - (Convert.ToDecimal(TxtBounce.Text) + Convert.ToDecimal(TxtEmployee.Text) + Convert.ToDecimal(TxtCareRent.Text) + Convert.ToDecimal(TxtMarket.Text));
					decimal num2 = Convert.ToDecimal(TxtEmployee.Text) + Convert.ToDecimal(TxtCareRent.Text) + Convert.ToDecimal(TxtMarket.Text) + Convert.ToDecimal(TxtExpenses.Text);
					decimal totalSell = Convert.ToDecimal(TxtBounce.Text) + Convert.ToDecimal(TxtEmployee.Text) + Convert.ToDecimal(TxtCareRent.Text) + Convert.ToDecimal(TxtMarket.Text);
					decimal totalCash = Convert.ToDecimal(TxtNet.Text) - num2;
					PurRTN.InsertTranNum(out var TRNIDDS, Code, TxtSuppliers.Text, TxtDate.DateTime);
					if (Convert.ToInt32(TxtType.SelectedValue) == 2)
					{
						PurRTN.InsertTranDetailsAjil(TRNIDDS, Code, Convert.ToInt32(LblCustAccount.Text), Convert.ToInt32(LBlSuppAccount.Text), TxtSuppliers.Text, totalSup, totalSell, Convert.ToDecimal(TxtAllVat.Text), totalCash, Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToDecimal(TxtExpenses.Text), TxtExpensesText.Text, Convert.ToDecimal(TxtEmployee.Text), Convert.ToDecimal(TxtCareRent.Text), Convert.ToDecimal(TxtMarket.Text));
					}
					else
					{
						PurRTN.InsertTranDetailsCash(TRNIDDS, Code, Convert.ToInt32(LBlSuppAccount.Text), TxtSuppliers.Text, totalSup, totalSell, Convert.ToDecimal(TxtAllVat.Text), totalCash, Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToDecimal(TxtExpenses.Text), TxtExpensesText.Text, Convert.ToDecimal(TxtEmployee.Text), Convert.ToDecimal(TxtCareRent.Text), Convert.ToDecimal(TxtMarket.Text));
					}
					gridControl1.DataSource = datatable;
					gridView1.OptionsBehavior.Editable = false;
					if (num > 0)
					{
						PrintID = num;
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						IsEnableFalse();
						TxtCode.Text = Code.ToString();
						switch (MessageBox.Show("تم حفظ بيانات فاتورة الدلالة بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
						{
						case DialogResult.Yes:
							Print();
							break;
						}
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات فاتورة الدلالة رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else
				{
					MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة الدلالة بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 107))
		{
			if (TxtDate.ReadOnly)
			{
				IsEnableTrue();
				BtnEdit.Text = "F6 حفظ";
				gridView1.OptionsBehavior.Editable = true;
				return;
			}
			int num = 0;
			int num2 = 0;
			if (!(TxtSuppliers.Text != ""))
			{
				return;
			}
			CheckFfistRow();
			if (datatable.Rows.Count > 0)
			{
				ArrangeDatatable();
				Cal_FinalResultTotals();
				FiterDatatableItemsUpdate();
				GetTotalCost();
				num2 = PurRTN.DeleteTranDetailsNum(Convert.ToInt32(TxtCode.Text), 39);
				num = PurRTN.UpdatePurch(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(TxtType.SelectedValue), Convert.ToInt32(LblCust.Text), TxtCustomer.Text, Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtNameDriver.Text, TxtDriverPhone.Text, TxtDate.DateTime, TxtCarNum.Text, TxtCity.Text, TxtBouncePerc.Value, Convert.ToDecimal(TxtBounce.Value), TxtExpensesText.Text, Convert.ToDecimal(TxtExpenses.Value), Convert.ToDecimal(TxtEmployee.Value), Convert.ToDecimal(TxtCareRent.Value), Convert.ToDecimal(TxtMarket.Value), Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Convert.ToInt32(CBCostCenter.SelectedValue));
				PurRTN.deleteItemsByPurcahesId(num);
				PurRTN.InsertItems(datatable, num, Convert.ToInt32(TxtPurCode.Text));
				decimal totalSup = Convert.ToDecimal(TxtTotals.Text) - (Convert.ToDecimal(TxtBounce.Text) + Convert.ToDecimal(TxtEmployee.Text) + Convert.ToDecimal(TxtCareRent.Text) + Convert.ToDecimal(TxtMarket.Text));
				decimal num3 = Convert.ToDecimal(TxtEmployee.Text) + Convert.ToDecimal(TxtCareRent.Text) + Convert.ToDecimal(TxtMarket.Text) + Convert.ToDecimal(TxtExpenses.Text);
				decimal totalSell = Convert.ToDecimal(TxtBounce.Text) + Convert.ToDecimal(TxtEmployee.Text) + Convert.ToDecimal(TxtCareRent.Text) + Convert.ToDecimal(TxtMarket.Text);
				decimal totalCash = Convert.ToDecimal(TxtNet.Text) - num3;
				PurRTN.UpdateTranNUM(out var TRNIDDS, Convert.ToInt32(TxtCode.Text), TxtSuppliers.Text, TxtDate.DateTime);
				if (Convert.ToInt32(TxtType.SelectedValue) == 2)
				{
					PurRTN.InsertTranDetailsAjil(TRNIDDS, Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblCustAccount.Text), Convert.ToInt32(LBlSuppAccount.Text), TxtSuppliers.Text, totalSup, totalSell, Convert.ToDecimal(TxtAllVat.Text), totalCash, Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToDecimal(TxtExpenses.Text), TxtExpensesText.Text, Convert.ToDecimal(TxtEmployee.Text), Convert.ToDecimal(TxtCareRent.Text), Convert.ToDecimal(TxtMarket.Text));
				}
				else
				{
					PurRTN.InsertTranDetailsCash(TRNIDDS, Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LBlSuppAccount.Text), TxtSuppliers.Text, totalSup, totalSell, Convert.ToDecimal(TxtAllVat.Text), totalCash, Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToDecimal(TxtExpenses.Text), TxtExpensesText.Text, Convert.ToDecimal(TxtEmployee.Text), Convert.ToDecimal(TxtCareRent.Text), Convert.ToDecimal(TxtMarket.Text));
				}
				if (num > 0)
				{
					PrintID = num;
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					IsEnableFalse();
					BtnEdit.Text = "تعديل F6";
					gridView1.OptionsBehavior.Editable = false;
					switch (MessageBox.Show("تم تعديل فاتورة الدلالة بنجاح \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						Print();
						break;
					}
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات فاتورة الدلالة رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 107))
		{
			int num = 0;
			int num2 = 0;
			if (TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة الدلالة رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = PurRTN.Delete(Convert.ToInt32(TxtCode.Text));
					PurRTN.DeleteTranDetailsNum(Convert.ToInt32(TxtCode.Text), 39);
					num2 = PurRTN.DeleteTranNum(Convert.ToInt32(TxtCode.Text), 39);
					if (num > 0 && num2 > 0)
					{
						MessageBox.Show("تم حذف بيانات فاتورة الدلالة رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						gridView1.OptionsBehavior.Editable = true;
						IsEnableTrue();
						BtnDelete.Enabled = false;
						BtnEdit.Enabled = false;
						BtnSave.Enabled = true;
						TxtSuppliers.Focus();
						PrintID = 0;
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات فاتورة الدلالة رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة الدلالة بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 107))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_OrderDalala_Dalala", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@ID", PrintID);
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
				sqlConnection.Close();
				if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintOrderDalala.mrt");
					stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["Date"] = TxtDate.DateTime;
					stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
					stiReport["CustName"] = TxtSuppliers.Text;
					stiReport["Tafqit"] = txtWordAr;
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

	public void IsEnableTrue()
	{
		TxtDiscount.ReadOnly = false;
		TxtDate.ReadOnly = false;
		BtnEdit.Text = "F6 تعديل";
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = true;
		TxtDate.ReadOnly = true;
		TxtDiscount.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
	}

	public void openItmCard()
	{
		if (gridControl1.Focus())
		{
			if (CodeItm != "")
			{
				LoginDetails.ItemSearchID = PurRTN.GetItmID(CodeItm);
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
					Item_Items item_Items = PurRTN.GetItems(code).FirstOrDefault();
					if (item_Items != null)
					{
						DataRow dataRow = null;
						dataRow = datatable.NewRow();
						dataRow["كود_الصنف"] = item_Items.Item_code;
						dataRow["أسم الصنف"] = item_Items.item_Name;
						dataRow["الوحدة"] = item_Items.Item_Unit.Name;
						dataRow["الكمية"] = 1;
						dataRow["كمية الارسالية"] = 0;
						dataRow["كمية الهدر"] = 0;
						dataRow["المخزن"] = PurRTN.GetStoreName();
						dataRow["رقم المخزن"] = PurRTN.GetStoreID();
						dataRow["أسم_الوحدة"] = "الصغرى";
						dataRow["متوسط_التكلفة"] = item_Items.Average_cost;
						dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
						dataRow["سعر الوحدة"] = item_Items.SellPriceSmall;
						dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1)), DecimalNum);
						if (PurRTN.IsVatActive())
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
						object obj = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
						int num = Convert.ToInt32(obj.ToString());
						dataRow["ظ…"] = num + 1;
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

	private void GroupPermission()
	{
		label12.Visible = LoginDetails.SellPrice2;
		label9.Visible = LoginDetails.SellPrice3;
		label10.Visible = LoginDetails.ProvePrice;
		label13.Visible = LoginDetails.AvergPrice;
		label14.Visible = LoginDetails.LastPrice;
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

	private void GridPlus()
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (!(row["ظ…"].ToString() == Convert.ToString(num)) || Convert.ToInt32(row["الكمية"]) >= Convert.ToInt32(row["كمية الارسالية"]))
			{
				continue;
			}
			row["الكمية"] = Convert.ToDecimal(row["الكمية"]) + 1m;
			string code = row["كود_الصنف"].ToString();
			Item_Items item_Items = Pur.GetItems(code).FirstOrDefault();
			row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]);
			if (PurRTN.IsVatActive())
			{
				if (item_Items.Is_Tax == true)
				{
					decimal value = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]);
					decimal? tax_Value = item_Items.Tax_Value;
					row["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value * tax_Value / (decimal?)100), DecimalNum);
					string text = row["الضربية"].ToString();
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"])), DecimalNum);
					decimal value2 = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]);
					decimal? tax_Value2 = item_Items.Tax_Value;
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]))), DecimalNum);
					row["هل_ضريبة"] = 1;
					row["قيمة_الضريبة"] = item_Items.Tax_Value;
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
			if (Convert.ToDecimal(row["كمية الارسالية"]) - (Convert.ToDecimal(row["الكمية"]) + Convert.ToDecimal(row["كمية الهدر"])) < 0m)
			{
				row["الكمية"] = "0.00";
				row["كمية الهدر"] = "0.00";
			}
			break;
		}
		ArrangeDatatable();
		GetTotalCost();
		Cal_FinalResultTotals();
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[6];
		gridView1.ShowEditor();
	}

	private void Bounce()
	{
		TxtBounce.Text = (Convert.ToDecimal(TxtTotalAll.Text) * TxtBouncePerc.Value / 100m).ToString();
	}

	private void repositoryItemButtonEdit2_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["ظ…"].ToString() == Convert.ToString(num))
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

	private void Frms_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
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
			NewFrm();
		}
		else if (e.KeyCode.ToString() == "F5")
		{
			if (BtnSave.Enabled)
			{
				Save();
			}
		}
		else if (e.KeyCode.ToString() == "F6")
		{
			if (BtnEdit.Enabled)
			{
				Edit();
			}
		}
		else if (e.KeyCode.ToString() == "F7")
		{
			if (BtnDelete.Enabled)
			{
				Delete();
			}
		}
		else
		{
			if (e.KeyCode.ToString() == "F9")
			{
				return;
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

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void ControlSave_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnSave.Focus();
		}
	}

	private void ControlTxtNumVat_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void TxtNet_TextChanged(object sender, EventArgs e)
	{
		try
		{
			decimal number = Convert.ToDecimal(TxtTotals.Text) - (Convert.ToDecimal(TxtEmployee.Text) + Convert.ToDecimal(TxtCareRent.Text) + Convert.ToDecimal(TxtMarket.Text) + Convert.ToDecimal(TxtBounce.Text));
			ToWord toWord = new ToWord(number, currencies[tafqitID]);
			txtWordAr = toWord.ConvertToArabic();
			TxtTafqit.Text = txtWordAr;
		}
		catch (Exception)
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

	private void MenuPurchOrder_Click(object sender, EventArgs e)
	{
	}

	private void LblSup_TextChanged(object sender, EventArgs e)
	{
		LBlSuppAccount.Text = Cust.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
	}

	private void MenuPrintQued_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text != "")
		{
			Class_Print_DailyRestrictionsALL class_Print_DailyRestrictionsALL = new Class_Print_DailyRestrictionsALL();
			class_Print_DailyRestrictionsALL.Print_DailyRestrictionsALL(Convert.ToInt32(TxtCode.Text), 39);
		}
	}

	private void BtnPlus_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		GridPlus();
	}

	private void TxtBouncePerc_ValueChanged(object sender, EventArgs e)
	{
		Bounce();
	}

	private void BtnPayment_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 32))
		{
			new FrmPayment().ShowDialog();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TxtExpenses_TextChanged(object sender, EventArgs e)
	{
		if (Convert.ToDecimal(TxtExpenses.Text) > Convert.ToDecimal(TxtBounce.Text))
		{
			TxtExpenses.Text = "0.00";
		}
	}

	private void TxtType_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (TxtType.Text == "نقدى")
		{
			TxtCustomer.Text = "عميل نقدى";
			TxtCustomer.ForeColor = Color.Black;
			TxtType.ForeColor = Color.Black;
		}
		if (TxtType.Text == "آجلة")
		{
			TxtCustomer.Text = "";
			TxtCustomer.ForeColor = Color.Red;
			TxtType.ForeColor = Color.Red;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.OrderDalala.FrmDalala));
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
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.RecoveredMenuItemFix1 = new System.Windows.Forms.ToolStripMenuItem();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.RecoveredMenuItemFix2 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintQued = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPreviewFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.RecoveredMenuItemFix3 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix4 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix5 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix6 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuNewItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix7 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPurchOrder = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix8 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix9 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix10 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix11 = new System.Windows.Forms.ToolStripMenuItem();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.BtnPlus = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.TxtNet22 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.label7 = new System.Windows.Forms.Label();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTotalAll = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotalBeforDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label8 = new System.Windows.Forms.Label();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.TxtPurCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label26 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.LblCustAccount = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.LblCust = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtCustomer = new DevExpress.XtraEditors.ButtonEdit();
		this.label24 = new System.Windows.Forms.Label();
		this.TxtCity = new DevExpress.XtraEditors.TextEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtNameDriver = new DevExpress.XtraEditors.TextEdit();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtDriverPhone = new DevExpress.XtraEditors.TextEdit();
		this.label31 = new System.Windows.Forms.Label();
		this.TxtCarNum = new DevExpress.XtraEditors.TextEdit();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatBeforeDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnPayment = new DevExpress.XtraEditors.SimpleButton();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtExpensesText = new DevExpress.XtraEditors.TextEdit();
		this.TxtBounce = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label14 = new System.Windows.Forms.Label();
		this.TxtBouncePerc = new System.Windows.Forms.NumericUpDown();
		this.TxtExpenses = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtEmployee = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtMarket = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtCareRent = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTabocc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label29 = new System.Windows.Forms.Label();
		this.TxtAllVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label28 = new System.Windows.Forms.Label();
		this.TxtAllTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label27 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnPlus).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtPurCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCustomer.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNameDriver.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDriverPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCarNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtExpensesText.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBouncePerc).BeginInit();
		base.SuspendLayout();
		this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.Caption = "أسم الصنف";
		this.gridColumn2.FieldName = "أسم الصنف";
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.gridColumn2.OptionsColumn.AllowFocus = false;
		this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn2.OptionsColumn.ReadOnly = true;
		this.gridColumn2.Visible = true;
		this.gridColumn2.VisibleIndex = 1;
		this.gridColumn2.Width = 270;
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
		this.gridColumn1.ColumnEdit = this.repositoryItemButtonEditCodeSearch;
		this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.FieldName = "كود_الصنف";
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.OptionsColumn.AllowEdit = false;
		this.gridColumn1.OptionsColumn.AllowFocus = false;
		this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn1.OptionsColumn.ReadOnly = true;
		this.gridColumn1.Visible = true;
		this.gridColumn1.VisibleIndex = 0;
		this.gridColumn1.Width = 194;
		this.repositoryItemButtonEditCodeSearch.AutoHeight = false;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions1.Image");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEditCodeSearch.Name = "repositoryItemButtonEditCodeSearch";
		this.gridColumn5.Caption = "الضريبه_بعد_الخصم";
		this.gridColumn5.FieldName = "الضريبه_بعد_الخصم";
		this.gridColumn5.Name = "gridColumn5";
		this.MenuExit.Name = "MenuExit";
		this.MenuExit.Size = new System.Drawing.Size(222, 24);
		this.MenuExit.Text = "خروج";
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(219, 6);
		this.RecoveredMenuItemFix1.Name = "تخصيصToolStripMenuItem1";
		this.RecoveredMenuItemFix1.Size = new System.Drawing.Size(222, 24);
		this.RecoveredMenuItemFix1.Text = "تخصيص";
		this.txtdicoutprege.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Black;
		this.txtdicoutprege.Location = new System.Drawing.Point(6, 87);
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.ReadOnly = true;
		this.txtdicoutprege.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtdicoutprege.Size = new System.Drawing.Size(107, 27);
		this.txtdicoutprege.TabIndex = 102;
		this.txtdicoutprege.Text = "0.000";
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.txtdicoutprege.TextChanged += new System.EventHandler(TxtDiscountPrecent_TextChanged);
		this.txtdicoutprege.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtTafqit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTafqit.Location = new System.Drawing.Point(6, 155);
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
		this.RecoveredMenuItemFix2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.MenuPrintQued, this.MenuPreviewFatoura, this.MenuPrintFatoura });
		this.RecoveredMenuItemFix2.Name = "طباعةToolStripMenuItem1";
		this.RecoveredMenuItemFix2.Size = new System.Drawing.Size(222, 24);
		this.RecoveredMenuItemFix2.Text = "طباعة ";
		this.MenuPrintQued.Name = "MenuPrintQued";
		this.MenuPrintQued.Size = new System.Drawing.Size(259, 26);
		this.MenuPrintQued.Text = "معاينة القيد";
		this.MenuPrintQued.Click += new System.EventHandler(MenuPrintQued_Click);
		this.MenuPreviewFatoura.Name = "MenuPreviewFatoura";
		this.MenuPreviewFatoura.Size = new System.Drawing.Size(259, 26);
		this.MenuPreviewFatoura.Text = "معاينة الفاتورة";
		this.MenuPrintFatoura.Name = "MenuPrintFatoura";
		this.MenuPrintFatoura.Size = new System.Drawing.Size(259, 26);
		this.MenuPrintFatoura.Text = "طباعة الفاتورة       Ctrl + P";
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(219, 6);
		this.RecoveredMenuItemFix3.Name = "كلالكمياتبالمستودعاتToolStripMenuItem";
		this.RecoveredMenuItemFix3.Size = new System.Drawing.Size(251, 26);
		this.RecoveredMenuItemFix3.Text = "كل الكميات بالمستودعات";
		this.RecoveredMenuItemFix4.Name = "الكميةالحاليةToolStripMenuItem";
		this.RecoveredMenuItemFix4.Size = new System.Drawing.Size(251, 26);
		this.RecoveredMenuItemFix4.Text = "الكمية الحالية";
		this.RecoveredMenuItemFix5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix4, this.RecoveredMenuItemFix3, this.RecoveredMenuItemFix6 });
		this.RecoveredMenuItemFix5.Name = "الكمياتToolStripMenuItem1";
		this.RecoveredMenuItemFix5.Size = new System.Drawing.Size(222, 24);
		this.RecoveredMenuItemFix5.Text = "الكميات";
		this.RecoveredMenuItemFix6.Name = "كلالكمياتبالفروعToolStripMenuItem";
		this.RecoveredMenuItemFix6.Size = new System.Drawing.Size(251, 26);
		this.RecoveredMenuItemFix6.Text = "كل الكميات بالفروع";
		this.MenuSearchItem.Name = "MenuSearchItem";
		this.MenuSearchItem.Size = new System.Drawing.Size(222, 24);
		this.MenuSearchItem.Text = "بحث عن صنف        F3";
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.RecoveredMenuItemFix2, this.toolStripSeparator1, this.MenuNewItem, this.RecoveredMenuItemFix7, this.MenuSearchItem, this.toolStripSeparator4,
			this.MenuPurchOrder, this.RecoveredMenuItemFix8, this.RecoveredMenuItemFix5, this.toolStripSeparator2, this.RecoveredMenuItemFix1, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.contextMenuStrip1.Size = new System.Drawing.Size(223, 340);
		this.MenuNew.Name = "MenuNew";
		this.MenuNew.Size = new System.Drawing.Size(222, 24);
		this.MenuNew.Text = "جديد                       F4";
		this.MenuSave.Name = "MenuSave";
		this.MenuSave.Size = new System.Drawing.Size(222, 24);
		this.MenuSave.Text = "حفظ                       F5";
		this.MenuEdit.Name = "MenuEdit";
		this.MenuEdit.Size = new System.Drawing.Size(222, 24);
		this.MenuEdit.Text = "تعديل                      F6";
		this.MenuDelete.Name = "MenuDelete";
		this.MenuDelete.Size = new System.Drawing.Size(222, 24);
		this.MenuDelete.Text = "حذف                       F7";
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(219, 6);
		this.MenuNewItem.Name = "MenuNewItem";
		this.MenuNewItem.Size = new System.Drawing.Size(222, 24);
		this.MenuNewItem.Text = "صنف جديد            F12 ";
		this.RecoveredMenuItemFix7.Name = "حركةصنفToolStripMenuItem";
		this.RecoveredMenuItemFix7.Size = new System.Drawing.Size(222, 24);
		this.RecoveredMenuItemFix7.Text = "حركة صنف";
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(219, 6);
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		this.MenuPurchOrder.Size = new System.Drawing.Size(222, 24);
		this.MenuPurchOrder.Text = "تحميل من امر شراء  F9";
		this.MenuPurchOrder.Visible = false;
		this.RecoveredMenuItemFix8.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix9, this.RecoveredMenuItemFix10, this.RecoveredMenuItemFix11 });
		this.RecoveredMenuItemFix8.Name = "استعلامToolStripMenuItem1";
		this.RecoveredMenuItemFix8.Size = new System.Drawing.Size(222, 24);
		this.RecoveredMenuItemFix8.Text = "استعلام";
		this.RecoveredMenuItemFix9.Name = "رقمالفاتورةToolStripMenuItem1";
		this.RecoveredMenuItemFix9.Size = new System.Drawing.Size(165, 26);
		this.RecoveredMenuItemFix9.Text = "رقم الفاتورة";
		this.RecoveredMenuItemFix10.Name = "اسمالموردToolStripMenuItem1";
		this.RecoveredMenuItemFix10.Size = new System.Drawing.Size(165, 26);
		this.RecoveredMenuItemFix10.Text = "اسم المورد";
		this.RecoveredMenuItemFix11.Name = "نوعالفاتورةToolStripMenuItem1";
		this.RecoveredMenuItemFix11.Size = new System.Drawing.Size(165, 26);
		this.RecoveredMenuItemFix11.Text = "نوع الفاتورة";
		this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.Caption = "ظ…";
		this.gridColumn9.FieldName = "ظ…";
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn9.Visible = true;
		this.gridColumn9.VisibleIndex = 13;
		this.gridColumn9.Width = 49;
		this.gridControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridControl1.Location = new System.Drawing.Point(3, 18);
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[10] { this.repositoryItemButtonEdit4, this.repositoryItemButtonEdit6, this.repositoryItemTextEdit1, this.repositoryItemTextEdit2, this.repositoryItemDateEdit1, this.repositoryItemTextEdit3, this.repositoryItemPictureEdit1, this.repositoryItemButtonEdit5, this.repositoryItemButtonEditCodeSearch, this.BtnPlus });
		this.gridControl1.Size = new System.Drawing.Size(1189, 266);
		this.gridControl1.TabIndex = 0;
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 20f);
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[19]
		{
			this.gridColumn1, this.gridColumn2, this.shearch, this.gridColumn19, this.gridColumn4, this.gridColumn23, this.gridColumn25, this.gridColumn20, this.gridColumn26, this.gridColumn27,
			this.gridColumn21, this.gridColumn3, this.gridColumn6, this.gridColumn7, this.gridColumn8, this.gridColumn22, this.gridColumn5, this.gridColumn9, this.gridColumn24
		});
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.Name = "gridView1";
		this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
		this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
		this.gridView1.OptionsPrint.EnableAppearanceOddRow = true;
		this.gridView1.OptionsPrint.PrintFilterInfo = true;
		this.gridView1.OptionsPrint.PrintPreview = true;
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridView1.OptionsView.ShowGroupPanel = false;
		this.shearch.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.shearch.AppearanceCell.Options.UseFont = true;
		this.shearch.AppearanceCell.Options.UseTextOptions = true;
		this.shearch.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.shearch.AppearanceHeader.Options.UseFont = true;
		this.shearch.AppearanceHeader.Options.UseTextOptions = true;
		this.shearch.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.Caption = "الوحدة";
		this.shearch.ColumnEdit = this.repositoryItemButtonEdit4;
		this.shearch.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.FieldName = "الوحدة";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.AllowEdit = false;
		this.shearch.OptionsColumn.AllowFocus = false;
		this.shearch.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.shearch.OptionsColumn.ReadOnly = true;
		this.shearch.Visible = true;
		this.shearch.VisibleIndex = 2;
		this.shearch.Width = 86;
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.repositoryItemButtonEdit4.AutoHeight = false;
		editorButtonImageOptions2.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions2.Image");
		editorButtonImageOptions2.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.gridColumn19.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.Caption = "أسم_الوحدة";
		this.gridColumn19.FieldName = "أسم_الوحدة";
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.AllowFocus = false;
		this.gridColumn19.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.Caption = "المخزن";
		this.gridColumn4.ColumnEdit = this.repositoryItemButtonEdit5;
		this.gridColumn4.FieldName = "المخزن";
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.AllowEdit = false;
		this.gridColumn4.OptionsColumn.AllowFocus = false;
		this.gridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Visible = true;
		this.gridColumn4.VisibleIndex = 3;
		this.gridColumn4.Width = 110;
		this.repositoryItemButtonEdit5.AutoHeight = false;
		editorButtonImageOptions3.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions3.Image");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.gridColumn23.Caption = "متوسط_التكلفة";
		this.gridColumn23.FieldName = "متوسط_التكلفة";
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn25.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Bold);
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.Caption = "كمية الارسالية";
		this.gridColumn25.ColumnEdit = this.BtnPlus;
		this.gridColumn25.FieldName = "كمية الارسالية";
		this.gridColumn25.MinWidth = 25;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.OptionsColumn.AllowEdit = false;
		this.gridColumn25.OptionsColumn.AllowFocus = false;
		this.gridColumn25.Visible = true;
		this.gridColumn25.VisibleIndex = 4;
		this.gridColumn25.Width = 94;
		this.BtnPlus.AutoHeight = false;
		editorButtonImageOptions4.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions4.Image");
		this.BtnPlus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.BtnPlus.Name = "BtnPlus";
		this.BtnPlus.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnPlus_ButtonPressed);
		this.gridColumn20.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn20.AppearanceCell.Options.UseFont = true;
		this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn20.AppearanceHeader.Options.UseFont = true;
		this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.Caption = "الكمية";
		this.gridColumn20.ColumnEdit = this.BtnPlus;
		this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.FieldName = "الكمية";
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.Visible = true;
		this.gridColumn20.VisibleIndex = 5;
		this.gridColumn20.Width = 86;
		this.gridColumn26.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn26.AppearanceCell.Options.UseFont = true;
		this.gridColumn26.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn26.AppearanceHeader.Options.UseFont = true;
		this.gridColumn26.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.Caption = "كمية الهدر";
		this.gridColumn26.FieldName = "كمية الهدر";
		this.gridColumn26.MinWidth = 25;
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.Visible = true;
		this.gridColumn26.VisibleIndex = 8;
		this.gridColumn26.Width = 94;
		this.gridColumn27.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn27.AppearanceCell.Options.UseFont = true;
		this.gridColumn27.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn27.AppearanceHeader.Options.UseFont = true;
		this.gridColumn27.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.Caption = "الشرح";
		this.gridColumn27.FieldName = "الشرح";
		this.gridColumn27.MinWidth = 25;
		this.gridColumn27.Name = "gridColumn27";
		this.gridColumn27.Visible = true;
		this.gridColumn27.VisibleIndex = 9;
		this.gridColumn27.Width = 94;
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
		this.gridColumn21.FieldName = "سعر الوحدة";
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.Visible = true;
		this.gridColumn21.VisibleIndex = 6;
		this.gridColumn21.Width = 81;
		this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.Caption = "الاجمالى";
		this.gridColumn3.FieldName = "الاجمالى";
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.OptionsColumn.AllowFocus = false;
		this.gridColumn3.OptionsColumn.ReadOnly = true;
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn3.Visible = true;
		this.gridColumn3.VisibleIndex = 10;
		this.gridColumn3.Width = 81;
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
		this.gridColumn6.FieldName = "الضربية";
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.AllowFocus = false;
		this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn6.OptionsColumn.ReadOnly = true;
		this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn6.Visible = true;
		this.gridColumn6.VisibleIndex = 7;
		this.gridColumn6.Width = 81;
		this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.Caption = "إجمالى المبلغ";
		this.gridColumn7.FieldName = "إجمالى المبلغ";
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.OptionsColumn.AllowFocus = false;
		this.gridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn7.OptionsColumn.ReadOnly = true;
		this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn7.Visible = true;
		this.gridColumn7.VisibleIndex = 11;
		this.gridColumn7.Width = 81;
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
		this.gridColumn8.FieldName = "حذف";
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Visible = true;
		this.gridColumn8.VisibleIndex = 12;
		this.gridColumn8.Width = 40;
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit6.AutoHeight = false;
		editorButtonImageOptions5.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions5.Image");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "حذف", -1, true, true, false, editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
		this.gridColumn22.Caption = "إجمالى_بعدالخصم";
		this.gridColumn22.FieldName = "إجمالى_بعدالخصم";
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn24.Caption = "اجمالي_متوسط_التكلفة";
		this.gridColumn24.FieldName = "اجمالي_متوسط_التكلفة";
		this.gridColumn24.Name = "gridColumn24";
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
		this.gridColumn18.Caption = "حذف الصنف";
		this.gridColumn18.Name = "gridColumn18";
		this.gridColumn18.Visible = true;
		this.gridColumn18.VisibleIndex = 8;
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Location = new System.Drawing.Point(12, 203);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1195, 287);
		this.groupBox3.TabIndex = 11;
		this.groupBox3.TabStop = false;
		this.TxtNet22.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNet22.DecimalDigits = 3;
		this.TxtNet22.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtNet22.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet22.Location = new System.Drawing.Point(797, 36);
		this.TxtNet22.Name = "TxtNet22";
		this.TxtNet22.ReadOnly = true;
		this.TxtNet22.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNet22.Size = new System.Drawing.Size(258, 48);
		this.TxtNet22.TabIndex = 92;
		this.TxtNet22.Text = "0.000";
		this.TxtNet22.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet22.UseCompatibleTextRendering = false;
		this.TxtNet22.Value = new decimal(new int[4]);
		this.TxtNet22.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet22.TextChanged += new System.EventHandler(TxtNet_TextChanged);
		this.BtnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(797, 94);
		this.BtnEdit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Size = new System.Drawing.Size(125, 40);
		this.BtnEdit.TabIndex = 4;
		this.BtnEdit.Text = "تعديل F6";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(930, 94);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(125, 40);
		this.BtnSave.TabIndex = 3;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(930, 142);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(125, 40);
		this.BtnPrint.TabIndex = 6;
		this.BtnPrint.Text = "طباعة Ctrl P";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label7.Location = new System.Drawing.Point(327, 95);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(119, 19);
		this.label7.TabIndex = 172;
		this.label7.Text = "المجموع بالخصم";
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(1063, 94);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(125, 40);
		this.BtnNew.TabIndex = 7;
		this.BtnNew.Text = "جديد F4";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(1063, 142);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(125, 40);
		this.BtnDelete.TabIndex = 5;
		this.BtnDelete.Text = "حذف F7";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtVat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtVat.DecimalDigits = 3;
		this.TxtVat.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtVat.ForeColor = System.Drawing.Color.Black;
		this.TxtVat.Location = new System.Drawing.Point(214, 54);
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.ReadOnly = true;
		this.TxtVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVat.Size = new System.Drawing.Size(107, 27);
		this.TxtVat.TabIndex = 170;
		this.TxtVat.Text = "0.000";
		this.TxtVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4]);
		this.TxtVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotals.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Location = new System.Drawing.Point(214, 20);
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.ReadOnly = true;
		this.TxtTotals.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotals.Size = new System.Drawing.Size(107, 27);
		this.TxtTotals.TabIndex = 169;
		this.TxtTotals.Text = "0.000";
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(797, 142);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(125, 40);
		this.BtnExit.TabIndex = 8;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtTotalAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTotalAll.DecimalDigits = 3;
		this.TxtTotalAll.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotalAll.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalAll.Location = new System.Drawing.Point(6, 19);
		this.TxtTotalAll.Name = "TxtTotalAll";
		this.TxtTotalAll.ReadOnly = true;
		this.TxtTotalAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotalAll.Size = new System.Drawing.Size(107, 27);
		this.TxtTotalAll.TabIndex = 100;
		this.TxtTotalAll.Text = "0.000";
		this.TxtTotalAll.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalAll.Value = new decimal(new int[4]);
		this.TxtTotalAll.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalAll.TextChanged += new System.EventHandler(TxtTotals_TextChanged);
		this.TxtTotalAll.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtTotalBeforDisc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTotalBeforDisc.DecimalDigits = 3;
		this.TxtTotalBeforDisc.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotalBeforDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalBeforDisc.Location = new System.Drawing.Point(214, 87);
		this.TxtTotalBeforDisc.Name = "TxtTotalBeforDisc";
		this.TxtTotalBeforDisc.ReadOnly = true;
		this.TxtTotalBeforDisc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotalBeforDisc.Size = new System.Drawing.Size(107, 27);
		this.TxtTotalBeforDisc.TabIndex = 157;
		this.TxtTotalBeforDisc.Text = "0.000";
		this.TxtTotalBeforDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalBeforDisc.Value = new decimal(new int[4]);
		this.TxtTotalBeforDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtNet.ForeColor = System.Drawing.Color.Black;
		this.TxtNet.Location = new System.Drawing.Point(6, 121);
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNet.Size = new System.Drawing.Size(107, 27);
		this.TxtNet.TabIndex = 103;
		this.TxtNet.Text = "0.000";
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet.TextChanged += new System.EventHandler(TxtNet_TextChanged);
		this.TxtNet.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label8.Location = new System.Drawing.Point(327, 127);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(109, 19);
		this.label8.TabIndex = 171;
		this.label8.Text = "الضريبة بالخصم";
		this.LBlSuppAccount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LBlSuppAccount.AutoSize = true;
		this.LBlSuppAccount.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LBlSuppAccount.Location = new System.Drawing.Point(1098, 46);
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		this.LBlSuppAccount.Size = new System.Drawing.Size(19, 21);
		this.LBlSuppAccount.TabIndex = 129;
		this.LBlSuppAccount.Text = "1";
		this.TxtPurCode.Location = new System.Drawing.Point(369, 22);
		this.TxtPurCode.Name = "TxtPurCode";
		this.TxtPurCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtPurCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPurCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtPurCode.Properties.Appearance.Options.UseFont = true;
		this.TxtPurCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPurCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPurCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtPurCode.Properties.ReadOnly = true;
		this.TxtPurCode.Size = new System.Drawing.Size(220, 28);
		this.TxtPurCode.TabIndex = 1;
		this.TxtPurCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCust_ButtonClick);
		this.TxtPurCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label26.Location = new System.Drawing.Point(232, 104);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(95, 21);
		this.label26.TabIndex = 127;
		this.label26.Text = "مركز التكلفة";
		this.CBCostCenter.Enabled = false;
		this.CBCostCenter.Font = new System.Drawing.Font("Tahoma", 10f);
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Location = new System.Drawing.Point(6, 101);
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.Size = new System.Drawing.Size(220, 29);
		this.CBCostCenter.TabIndex = 7;
		this.CBCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		this.CBCostCenter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
		this.LblSup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblSup.AutoSize = true;
		this.LblSup.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblSup.Location = new System.Drawing.Point(1098, 86);
		this.LblSup.Name = "LblSup";
		this.LblSup.Size = new System.Drawing.Size(19, 21);
		this.LblSup.TabIndex = 125;
		this.LblSup.Text = "1";
		this.LblSup.TextChanged += new System.EventHandler(LblSup_TextChanged);
		this.TxtDate.EditValue = null;
		this.TxtDate.Location = new System.Drawing.Point(6, 22);
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
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate.Properties.Mask.EditMask = "yyyy/MM/dd HH:mm:ss tt";
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate.Size = new System.Drawing.Size(220, 28);
		this.TxtDate.TabIndex = 0;
		this.TxtDate.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.repositoryItemButtonEditCode.AutoHeight = false;
		editorButtonImageOptions6.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions6.Image");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label21.Location = new System.Drawing.Point(232, 25);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(53, 21);
		this.label21.TabIndex = 113;
		this.label21.Text = "التاريخ";
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label20.Location = new System.Drawing.Point(595, 25);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(103, 21);
		this.label20.TabIndex = 112;
		this.label20.Text = "رقم الارسالية";
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label18.Location = new System.Drawing.Point(1074, 65);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(89, 21);
		this.label18.TabIndex = 107;
		this.label18.Text = "اسم المورد";
		this.TxtSuppliers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSuppliers.Location = new System.Drawing.Point(728, 62);
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
		this.TxtSuppliers.Properties.ReadOnly = true;
		this.TxtSuppliers.Size = new System.Drawing.Size(340, 28);
		this.TxtSuppliers.TabIndex = 2;
		this.TxtSuppliers.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlTxtNumVat_KeyDown);
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
		this.groupBox1.Controls.Add(this.LblCustAccount);
		this.groupBox1.Controls.Add(this.label25);
		this.groupBox1.Controls.Add(this.TxtType);
		this.groupBox1.Controls.Add(this.LblCust);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.TxtCustomer);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.TxtCity);
		this.groupBox1.Controls.Add(this.label23);
		this.groupBox1.Controls.Add(this.LBlSuppAccount);
		this.groupBox1.Controls.Add(this.TxtNameDriver);
		this.groupBox1.Controls.Add(this.TxtPurCode);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Controls.Add(this.TxtDriverPhone);
		this.groupBox1.Controls.Add(this.CBCostCenter);
		this.groupBox1.Controls.Add(this.label31);
		this.groupBox1.Controls.Add(this.LblSup);
		this.groupBox1.Controls.Add(this.TxtCarNum);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.TxtSuppliers);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1195, 185);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "بيانات فاتورة الدلالة";
		this.LblCustAccount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblCustAccount.AutoSize = true;
		this.LblCustAccount.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblCustAccount.Location = new System.Drawing.Point(1098, 123);
		this.LblCustAccount.Name = "LblCustAccount";
		this.LblCustAccount.Size = new System.Drawing.Size(19, 21);
		this.LblCustAccount.TabIndex = 135;
		this.LblCustAccount.Text = "1";
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label25.Location = new System.Drawing.Point(232, 144);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(43, 21);
		this.label25.TabIndex = 134;
		this.label25.Text = "النوع";
		this.TxtType.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Location = new System.Drawing.Point(6, 141);
		this.TxtType.Name = "TxtType";
		this.TxtType.Size = new System.Drawing.Size(220, 29);
		this.TxtType.TabIndex = 133;
		this.TxtType.SelectedIndexChanged += new System.EventHandler(TxtType_SelectedIndexChanged);
		this.TxtType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress);
		this.LblCust.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblCust.AutoSize = true;
		this.LblCust.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblCust.Location = new System.Drawing.Point(1098, 161);
		this.LblCust.Name = "LblCust";
		this.LblCust.Size = new System.Drawing.Size(19, 21);
		this.LblCust.TabIndex = 132;
		this.LblCust.Text = "1";
		this.label22.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label22.Location = new System.Drawing.Point(1074, 144);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(93, 21);
		this.label22.TabIndex = 131;
		this.label22.Text = "اسم العميل";
		this.TxtCustomer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCustomer.Location = new System.Drawing.Point(369, 141);
		this.TxtCustomer.Name = "TxtCustomer";
		this.TxtCustomer.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCustomer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCustomer.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCustomer.Properties.Appearance.Options.UseFont = true;
		this.TxtCustomer.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCustomer.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCustomer.Size = new System.Drawing.Size(699, 28);
		this.TxtCustomer.TabIndex = 130;
		this.TxtCustomer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCustomer_ButtonClick);
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label24.Location = new System.Drawing.Point(595, 65);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(59, 21);
		this.label24.TabIndex = 127;
		this.label24.Text = "المدينة";
		this.TxtCity.Location = new System.Drawing.Point(369, 62);
		this.TxtCity.Name = "TxtCity";
		this.TxtCity.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCity.Properties.Appearance.Options.UseFont = true;
		this.TxtCity.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCity.Size = new System.Drawing.Size(220, 28);
		this.TxtCity.TabIndex = 123;
		this.label23.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label23.Location = new System.Drawing.Point(1074, 104);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(102, 21);
		this.label23.TabIndex = 126;
		this.label23.Text = "اسم السواق";
		this.TxtNameDriver.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNameDriver.Location = new System.Drawing.Point(728, 101);
		this.TxtNameDriver.Name = "TxtNameDriver";
		this.TxtNameDriver.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNameDriver.Properties.Appearance.Options.UseFont = true;
		this.TxtNameDriver.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNameDriver.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNameDriver.Size = new System.Drawing.Size(340, 28);
		this.TxtNameDriver.TabIndex = 122;
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label19.Location = new System.Drawing.Point(232, 65);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(84, 21);
		this.label19.TabIndex = 125;
		this.label19.Text = "رقم الجوال";
		this.TxtDriverPhone.Location = new System.Drawing.Point(6, 62);
		this.TxtDriverPhone.Name = "TxtDriverPhone";
		this.TxtDriverPhone.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDriverPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtDriverPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDriverPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDriverPhone.Size = new System.Drawing.Size(220, 28);
		this.TxtDriverPhone.TabIndex = 121;
		this.label31.AutoSize = true;
		this.label31.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label31.Location = new System.Drawing.Point(595, 104);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(101, 21);
		this.label31.TabIndex = 124;
		this.label31.Text = "لوحة السيارة";
		this.TxtCarNum.Location = new System.Drawing.Point(369, 101);
		this.TxtCarNum.Name = "TxtCarNum";
		this.TxtCarNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCarNum.Properties.Appearance.Options.UseFont = true;
		this.TxtCarNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCarNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCarNum.Size = new System.Drawing.Size(220, 28);
		this.TxtCarNum.TabIndex = 120;
		this.ribbonPage2.Name = "ribbonPage2";
		this.ribbonPage2.Text = "ribbonPage2";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label1.Location = new System.Drawing.Point(119, 93);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(93, 19);
		this.label1.TabIndex = 163;
		this.label1.Text = "نسبة الخصم";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label2.Location = new System.Drawing.Point(119, 25);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(70, 19);
		this.label2.TabIndex = 164;
		this.label2.Text = "الاجمالي";
		this.repositoryItemButtonEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.repositoryItemButtonEdit1.AutoHeight = false;
		editorButtonImageOptions7.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		editorButtonImageOptions7.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions7.Image");
		editorButtonImageOptions7.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label3.Location = new System.Drawing.Point(119, 59);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(89, 19);
		this.label3.TabIndex = 165;
		this.label3.Text = "قيمة الخصم";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label4.Location = new System.Drawing.Point(119, 127);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(58, 19);
		this.label4.TabIndex = 166;
		this.label4.Text = "الصافي";
		this.TxtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscount.Location = new System.Drawing.Point(6, 54);
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.ReadOnly = true;
		this.TxtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDiscount.Size = new System.Drawing.Size(107, 27);
		this.TxtDiscount.TabIndex = 101;
		this.TxtDiscount.Text = "0.000";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscount.TextChanged += new System.EventHandler(TxtDiscount_TextChanged);
		this.TxtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtVatBeforeDisc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtVatBeforeDisc.DecimalDigits = 3;
		this.TxtVatBeforeDisc.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtVatBeforeDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatBeforeDisc.Location = new System.Drawing.Point(214, 121);
		this.TxtVatBeforeDisc.Name = "TxtVatBeforeDisc";
		this.TxtVatBeforeDisc.ReadOnly = true;
		this.TxtVatBeforeDisc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVatBeforeDisc.Size = new System.Drawing.Size(107, 27);
		this.TxtVatBeforeDisc.TabIndex = 162;
		this.TxtVatBeforeDisc.Text = "0.000";
		this.TxtVatBeforeDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatBeforeDisc.Value = new decimal(new int[4]);
		this.TxtVatBeforeDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label5.Location = new System.Drawing.Point(327, 59);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(92, 19);
		this.label5.TabIndex = 167;
		this.label5.Text = "الضريبة VAT";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label6.Location = new System.Drawing.Point(327, 25);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(107, 19);
		this.label6.TabIndex = 168;
		this.label6.Text = "المجموع Total";
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit2.AutoHeight = false;
		editorButtonImageOptions8.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions8.Image");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "حذف", -1, true, true, false, editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit3.AutoHeight = false;
		editorButtonImageOptions9.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions9.Image");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions9, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance9, appearanceHovered9, appearancePressed9, appearanceDisabled9, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.BtnPayment);
		this.groupBox2.Controls.Add(this.label15);
		this.groupBox2.Controls.Add(this.TxtExpensesText);
		this.groupBox2.Controls.Add(this.TxtBounce);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.TxtBouncePerc);
		this.groupBox2.Controls.Add(this.TxtExpenses);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.TxtEmployee);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.TxtMarket);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtCareRent);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.txtdicoutprege);
		this.groupBox2.Controls.Add(this.TxtTafqit);
		this.groupBox2.Controls.Add(this.TxtNet22);
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.TxtVat);
		this.groupBox2.Controls.Add(this.TxtTotals);
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
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Location = new System.Drawing.Point(12, 496);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1195, 189);
		this.groupBox2.TabIndex = 10;
		this.groupBox2.TabStop = false;
		this.BtnPayment.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPayment.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPayment.Appearance.Options.UseFont = true;
		this.BtnPayment.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPayment.ImageOptions.Image");
		this.BtnPayment.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPayment.Location = new System.Drawing.Point(1063, 36);
		this.BtnPayment.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPayment.Name = "BtnPayment";
		this.BtnPayment.Size = new System.Drawing.Size(125, 48);
		this.BtnPayment.TabIndex = 206;
		this.BtnPayment.Text = "ايصال صرف";
		this.BtnPayment.Click += new System.EventHandler(BtnPayment_Click);
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label15.Location = new System.Drawing.Point(670, 160);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(109, 19);
		this.label15.TabIndex = 205;
		this.label15.Text = "مصروفات أخري";
		this.TxtExpensesText.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtExpensesText.Location = new System.Drawing.Point(494, 156);
		this.TxtExpensesText.Name = "TxtExpensesText";
		this.TxtExpensesText.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtExpensesText.Properties.Appearance.Options.UseFont = true;
		this.TxtExpensesText.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtExpensesText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtExpensesText.Size = new System.Drawing.Size(170, 28);
		this.TxtExpensesText.TabIndex = 130;
		this.TxtBounce.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtBounce.DecimalDigits = 2;
		this.TxtBounce.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtBounce.ForeColor = System.Drawing.Color.Black;
		this.TxtBounce.Location = new System.Drawing.Point(704, 88);
		this.TxtBounce.Name = "TxtBounce";
		this.TxtBounce.ReadOnly = true;
		this.TxtBounce.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtBounce.Size = new System.Drawing.Size(151, 27);
		this.TxtBounce.TabIndex = 204;
		this.TxtBounce.Text = "0.00";
		this.TxtBounce.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtBounce.Value = new decimal(new int[4]);
		this.TxtBounce.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label14.Location = new System.Drawing.Point(861, 93);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(94, 19);
		this.label14.TabIndex = 203;
		this.label14.Text = "قيمة العمولة";
		this.TxtBouncePerc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtBouncePerc.DecimalPlaces = 2;
		this.TxtBouncePerc.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtBouncePerc.Location = new System.Drawing.Point(704, 55);
		this.TxtBouncePerc.Maximum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtBouncePerc.Minimum = new decimal(new int[4] { 5, 0, 0, 0 });
		this.TxtBouncePerc.Name = "TxtBouncePerc";
		this.TxtBouncePerc.Size = new System.Drawing.Size(151, 26);
		this.TxtBouncePerc.TabIndex = 202;
		this.TxtBouncePerc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtBouncePerc.Value = new decimal(new int[4] { 7, 0, 0, 0 });
		this.TxtBouncePerc.ValueChanged += new System.EventHandler(TxtBouncePerc_ValueChanged);
		this.TxtExpenses.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtExpenses.DecimalDigits = 2;
		this.TxtExpenses.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtExpenses.ForeColor = System.Drawing.Color.Black;
		this.TxtExpenses.Location = new System.Drawing.Point(704, 121);
		this.TxtExpenses.Name = "TxtExpenses";
		this.TxtExpenses.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtExpenses.Size = new System.Drawing.Size(151, 27);
		this.TxtExpenses.TabIndex = 201;
		this.TxtExpenses.Text = "0.00";
		this.TxtExpenses.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtExpenses.Value = new decimal(new int[4]);
		this.TxtExpenses.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtExpenses.TextChanged += new System.EventHandler(TxtExpenses_TextChanged);
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label13.Location = new System.Drawing.Point(873, 127);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(109, 19);
		this.label13.TabIndex = 200;
		this.label13.Text = "مصروفات اخري";
		this.TxtEmployee.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtEmployee.DecimalDigits = 2;
		this.TxtEmployee.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtEmployee.ForeColor = System.Drawing.Color.Black;
		this.TxtEmployee.Location = new System.Drawing.Point(451, 53);
		this.TxtEmployee.Name = "TxtEmployee";
		this.TxtEmployee.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtEmployee.Size = new System.Drawing.Size(138, 27);
		this.TxtEmployee.TabIndex = 199;
		this.TxtEmployee.Text = "0.00";
		this.TxtEmployee.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtEmployee.Value = new decimal(new int[4]);
		this.TxtEmployee.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label12.Location = new System.Drawing.Point(595, 58);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(84, 19);
		this.label12.TabIndex = 198;
		this.label12.Text = "أجور العمال";
		this.TxtMarket.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtMarket.DecimalDigits = 2;
		this.TxtMarket.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtMarket.ForeColor = System.Drawing.Color.Black;
		this.TxtMarket.Location = new System.Drawing.Point(451, 123);
		this.TxtMarket.Name = "TxtMarket";
		this.TxtMarket.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMarket.Size = new System.Drawing.Size(138, 27);
		this.TxtMarket.TabIndex = 197;
		this.TxtMarket.Text = "0.00";
		this.TxtMarket.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMarket.Value = new decimal(new int[4]);
		this.TxtMarket.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label11.Location = new System.Drawing.Point(595, 128);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(89, 19);
		this.label11.TabIndex = 196;
		this.label11.Text = "كرت السوق";
		this.TxtCareRent.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtCareRent.DecimalDigits = 2;
		this.TxtCareRent.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtCareRent.ForeColor = System.Drawing.Color.Black;
		this.TxtCareRent.Location = new System.Drawing.Point(451, 90);
		this.TxtCareRent.Name = "TxtCareRent";
		this.TxtCareRent.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtCareRent.Size = new System.Drawing.Size(138, 27);
		this.TxtCareRent.TabIndex = 195;
		this.TxtCareRent.Text = "0.00";
		this.TxtCareRent.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCareRent.Value = new decimal(new int[4]);
		this.TxtCareRent.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label10.Location = new System.Drawing.Point(595, 95);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(95, 19);
		this.label10.TabIndex = 194;
		this.label10.Text = "ايجار السيارة";
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label9.Location = new System.Drawing.Point(861, 59);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(57, 19);
		this.label9.TabIndex = 192;
		this.label9.Text = "العمولة";
		this.TxtCost.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtCost.DecimalDigits = 3;
		this.TxtCost.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtCost.ForeColor = System.Drawing.Color.Black;
		this.TxtCost.Location = new System.Drawing.Point(744, 359);
		this.TxtCost.Name = "TxtCost";
		this.TxtCost.ReadOnly = true;
		this.TxtCost.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtCost.Size = new System.Drawing.Size(107, 27);
		this.TxtCost.TabIndex = 211;
		this.TxtCost.Text = "0.000";
		this.TxtCost.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCost.Value = new decimal(new int[4]);
		this.TxtCost.Visible = false;
		this.TxtCost.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTabocc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTabocc.DecimalDigits = 3;
		this.TxtTabocc.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTabocc.ForeColor = System.Drawing.Color.Black;
		this.TxtTabocc.Location = new System.Drawing.Point(83, 352);
		this.TxtTabocc.Name = "TxtTabocc";
		this.TxtTabocc.ReadOnly = true;
		this.TxtTabocc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTabocc.Size = new System.Drawing.Size(107, 27);
		this.TxtTabocc.TabIndex = 214;
		this.TxtTabocc.Text = "0.000";
		this.TxtTabocc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTabocc.Value = new decimal(new int[4]);
		this.TxtTabocc.Visible = false;
		this.TxtTabocc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label29.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label29.AutoSize = true;
		this.label29.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label29.Location = new System.Drawing.Point(196, 357);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(139, 19);
		this.label29.TabIndex = 213;
		this.label29.Text = "الضريبة التبغ 100%";
		this.label29.Visible = false;
		this.TxtAllVat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtAllVat.DecimalDigits = 3;
		this.TxtAllVat.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtAllVat.ForeColor = System.Drawing.Color.Black;
		this.TxtAllVat.Location = new System.Drawing.Point(335, 360);
		this.TxtAllVat.Name = "TxtAllVat";
		this.TxtAllVat.ReadOnly = true;
		this.TxtAllVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtAllVat.Size = new System.Drawing.Size(107, 27);
		this.TxtAllVat.TabIndex = 216;
		this.TxtAllVat.Text = "0.000";
		this.TxtAllVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllVat.Value = new decimal(new int[4]);
		this.TxtAllVat.Visible = false;
		this.TxtAllVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label28.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label28.AutoSize = true;
		this.label28.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label28.Location = new System.Drawing.Point(448, 365);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(113, 19);
		this.label28.TabIndex = 215;
		this.label28.Text = "الضريبة المضافة";
		this.label28.Visible = false;
		this.TxtAllTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtAllTotal.DecimalDigits = 3;
		this.TxtAllTotal.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtAllTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtAllTotal.Location = new System.Drawing.Point(563, 359);
		this.TxtAllTotal.Name = "TxtAllTotal";
		this.TxtAllTotal.ReadOnly = true;
		this.TxtAllTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtAllTotal.Size = new System.Drawing.Size(107, 28);
		this.TxtAllTotal.TabIndex = 217;
		this.TxtAllTotal.Text = "0.000";
		this.TxtAllTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllTotal.Value = new decimal(new int[4]);
		this.TxtAllTotal.Visible = false;
		this.TxtAllTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label27.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label27.Location = new System.Drawing.Point(857, 365);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(97, 19);
		this.label27.TabIndex = 210;
		this.label27.Text = "تكلفة الفاتورة";
		this.label27.Visible = false;
		this.label30.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label30.AutoSize = true;
		this.label30.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label30.Location = new System.Drawing.Point(676, 367);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(66, 19);
		this.label30.TabIndex = 212;
		this.label30.Text = "المجموع";
		this.label30.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1219, 697);
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.TxtCost);
		base.Controls.Add(this.TxtTabocc);
		base.Controls.Add(this.label29);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.TxtAllVat);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.label28);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.TxtAllTotal);
		base.Controls.Add(this.label30);
		base.Controls.Add(this.label27);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmDalala";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "فاتورة الدلالة";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnPlus).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TxtPurCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCustomer.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNameDriver.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDriverPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCarNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtExpensesText.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBouncePerc).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


