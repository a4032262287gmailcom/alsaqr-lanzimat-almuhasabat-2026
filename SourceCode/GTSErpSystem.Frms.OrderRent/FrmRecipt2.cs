using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using GTSErpSystem.BLL.OrderRent;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.OrderRent.Search;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.OrderRent;

public class FrmRecipt2 : XtraForm
{
	private Class_Dalala PurRTN = new Class_Dalala();

	private Class_Sending Pur = new Class_Sending();

	private Class_Rent ORD = new Class_Rent();

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

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private RibbonPage ribbonPage2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

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

	private GridColumn gridColumn26;

	private GridColumn gridColumn27;

	private GroupBox groupBox1;

	private DateEdit TxtDateRent;

	private Label label24;

	private Label label16;

	private TextEdit TxtNote;

	private Label label17;

	private TextEdit TxtSuppIDNum;

	private Label LBlSuppAccount;

	private Label label26;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private Label LblSup;

	private DateEdit TxtDate;

	private Label label23;

	private TextEdit TxtParentName;

	private Label label22;

	private TextEdit TxtPhone;

	private Label label21;

	private Label label19;

	private ButtonEdit TxtSuppliers;

	private Label label20;

	private ButtonEdit TxtCode;

	private Label label31;

	private TextEdit TxtDateRentNum;

	private Label label25;

	private ButtonEdit TxtCodeRent;

	private GroupBox groupBox2;

	private Label label12;

	private NumericEditBox TxtDeposit;

	private NumericEditBox numericEditBox1;

	private NumericEditBox numericEditBox2;

	private Label label1;

	private NumericEditBox numericEditBox3;

	private Label label2;

	private NumericEditBox numericEditBox4;

	private Label label3;

	private TextEdit TxtLocation;

	private Label label4;

	private NumericEditBox txtdicoutprege;

	private Label label5;

	private TextEdit TxtTafqit;

	private NumericEditBox TxtNet22;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private Label label7;

	private SimpleButton BtnNew;

	private Label label8;

	private SimpleButton BtnDelete;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtTotals;

	private SimpleButton BtnExit;

	private NumericEditBox TxtTotalAll;

	private NumericEditBox TxtTotalBeforDisc;

	private NumericEditBox TxtNet;

	private Label label6;

	private Label label9;

	private Label label10;

	private Label label11;

	private NumericEditBox TxtDiscount;

	private NumericEditBox TxtVatBeforeDisc;

	private Label label13;

	private Label label14;

	private Label label18;

	private TextEdit TxtAddress;

	public FrmRecipt2()
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
		TxtDate.Focus();
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
	}

	private void TxtCodeRent_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchRent frmSearchRent = new FrmSearchRent();
			frmSearchRent.ShowDialog();
			if (frmSearchRent.CustCode > 0)
			{
				int custCode = frmSearchRent.CustCode;
				OrderRent_Rent orderRent_Rent = ORD.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<OrderRent_RentDetails> itemsPurchesByID = ORD.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (OrderRent_RentDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = ORD.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
					dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
					dataRow["ItemNote"] = item.ItemNote;
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
					dataRow["الكمية"] = item.Quantity;
					dataRow["متوسط_التكلفة"] = item.LastCost;
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
				insertnewrow();
				CBCostCenter.SelectedValue = orderRent_Rent.CostCentersID;
				TxtCode.Text = orderRent_Rent.PurBranchID.ToString();
				LblSup.Text = orderRent_Rent.SupplierID.ToString();
				TxtSuppliers.Text = orderRent_Rent.SupplierName;
				TxtPhone.Text = orderRent_Rent.SupplierPhone;
				TxtDate.DateTime = Convert.ToDateTime(orderRent_Rent.Purchases_Date);
				TxtDateRent.DateTime = Convert.ToDateTime(orderRent_Rent.Purchases_Date);
				TxtParentName.Text = orderRent_Rent.ParentName;
				TxtPhone.Text = orderRent_Rent.SupplierPhone;
				TxtNote.Text = orderRent_Rent.Note;
				TxtDeposit.Text = orderRent_Rent.deposit.ToString();
				TxtAddress.Text = orderRent_Rent.Address;
				TxtSuppIDNum.Text = orderRent_Rent.SupplierIDNum;
				TxtTotals.Text = Convert.ToString(orderRent_Rent.TotalPrices);
				TxtVat.Text = Convert.ToString(orderRent_Rent.Tax);
				TxtTotalAll.Text = Convert.ToString(orderRent_Rent.Safy);
				TxtDiscount.Text = orderRent_Rent.DiscountNum.ToString();
				TxtTotalBeforDisc.Text = orderRent_Rent.TotalPrices_Discount.ToString();
				TxtVatBeforeDisc.Text = orderRent_Rent.Tax_Discount.ToString();
				TxtNet.Text = orderRent_Rent.Net.ToString();
				TxtNet22.Text = orderRent_Rent.Net.ToString();
				TxtCost.Text = orderRent_Rent.CostOrder.ToString();
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
					return;
				}
				if (TxtDiscount.Text == "" || Convert.ToDecimal(TxtDiscount.Text) == 0m || Convert.ToDecimal(TxtDiscount.Text) > Convert.ToDecimal(TxtTotals.Text))
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
						return;
					}
				}
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
				{
					foreach (DataRow row2 in datatable.Rows)
					{
						if (row2["أسم_الوحدة"].ToString() != "")
						{
							row2["سعربعدالخصم"] = Convert.ToDecimal(row2["سعر الوحدة"].ToString()) - num3 * Convert.ToDecimal(row2["سعر الوحدة"]) / 100m;
							row2["إجمالى_بعدالخصم"] = Convert.ToDecimal(row2["سعربعدالخصم"].ToString()) * Convert.ToDecimal(row2["الكمية"].ToString());
							row2["الضريبه_بعد_الخصم"] = Convert.ToDecimal(Convert.ToDecimal(row2["سعربعدالخصم"]) * Convert.ToDecimal(row2["الكمية"]) * Convert.ToDecimal(row2["قيمة_الضريبة"]) / 100m);
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
				return;
			}
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
		TxtDate.DateTime = DateTime.Now;
		CBCostCenter.SelectedValue = 0;
		TxtDiscount.Enabled = true;
		TxtDate.Enabled = true;
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
	}

	private void Edit()
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.OrderRent.FrmRecipt2));
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
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.TxtCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTabocc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label29 = new System.Windows.Forms.Label();
		this.TxtAllVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label28 = new System.Windows.Forms.Label();
		this.TxtAllTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label27 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label18 = new System.Windows.Forms.Label();
		this.TxtAddress = new DevExpress.XtraEditors.TextEdit();
		this.label31 = new System.Windows.Forms.Label();
		this.TxtDateRentNum = new DevExpress.XtraEditors.TextEdit();
		this.label25 = new System.Windows.Forms.Label();
		this.TxtCodeRent = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtDateRent = new DevExpress.XtraEditors.DateEdit();
		this.label24 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtSuppIDNum = new DevExpress.XtraEditors.TextEdit();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtParentName = new DevExpress.XtraEditors.TextEdit();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.label20 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtDeposit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.numericEditBox1 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.numericEditBox2 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label1 = new System.Windows.Forms.Label();
		this.numericEditBox3 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label2 = new System.Windows.Forms.Label();
		this.numericEditBox4 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtLocation = new DevExpress.XtraEditors.TextEdit();
		this.label4 = new System.Windows.Forms.Label();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.TxtNet22 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.label7 = new System.Windows.Forms.Label();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label8 = new System.Windows.Forms.Label();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTotalAll = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotalBeforDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatBeforeDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label13 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
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
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRentNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeRent.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRent.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRent.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppIDNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtParentName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
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
		this.repositoryItemButtonEditCode.AutoHeight = false;
		editorButtonImageOptions6.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions6.Image");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
		this.ribbonPage2.Name = "ribbonPage2";
		this.ribbonPage2.Text = "ribbonPage2";
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
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.TxtAddress);
		this.groupBox1.Controls.Add(this.label31);
		this.groupBox1.Controls.Add(this.TxtDateRentNum);
		this.groupBox1.Controls.Add(this.label25);
		this.groupBox1.Controls.Add(this.TxtCodeRent);
		this.groupBox1.Controls.Add(this.TxtDateRent);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtSuppIDNum);
		this.groupBox1.Controls.Add(this.LBlSuppAccount);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Controls.Add(this.CBCostCenter);
		this.groupBox1.Controls.Add(this.LblSup);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label23);
		this.groupBox1.Controls.Add(this.TxtParentName);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.TxtPhone);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.TxtSuppliers);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1195, 185);
		this.groupBox1.TabIndex = 218;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "بيانات فاتورة التأجير";
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label18.Location = new System.Drawing.Point(621, 142);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(56, 21);
		this.label18.TabIndex = 142;
		this.label18.Text = "العنوان";
		this.TxtAddress.Location = new System.Drawing.Point(335, 139);
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtAddress.Properties.Appearance.Options.UseFont = true;
		this.TxtAddress.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAddress.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAddress.Size = new System.Drawing.Size(280, 28);
		this.TxtAddress.TabIndex = 141;
		this.label31.AutoSize = true;
		this.label31.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label31.Location = new System.Drawing.Point(232, 65);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(76, 21);
		this.label31.TabIndex = 140;
		this.label31.Text = "عدد الايام";
		this.TxtDateRentNum.Location = new System.Drawing.Point(6, 62);
		this.TxtDateRentNum.Name = "TxtDateRentNum";
		this.TxtDateRentNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateRentNum.Properties.Appearance.Options.UseFont = true;
		this.TxtDateRentNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateRentNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateRentNum.Size = new System.Drawing.Size(220, 28);
		this.TxtDateRentNum.TabIndex = 139;
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label25.Location = new System.Drawing.Point(621, 25);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(82, 21);
		this.label25.TabIndex = 138;
		this.label25.Text = "رقم الايجار";
		this.TxtCodeRent.Location = new System.Drawing.Point(335, 22);
		this.TxtCodeRent.Name = "TxtCodeRent";
		this.TxtCodeRent.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCodeRent.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCodeRent.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCodeRent.Properties.Appearance.Options.UseFont = true;
		this.TxtCodeRent.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCodeRent.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCodeRent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCodeRent.Properties.ReadOnly = true;
		this.TxtCodeRent.Size = new System.Drawing.Size(280, 28);
		this.TxtCodeRent.TabIndex = 137;
		this.TxtCodeRent.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCodeRent_ButtonClick);
		this.TxtDateRent.EditValue = null;
		this.TxtDateRent.Location = new System.Drawing.Point(335, 62);
		this.TxtDateRent.Name = "TxtDateRent";
		this.TxtDateRent.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateRent.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateRent.Properties.Appearance.Options.UseFont = true;
		this.TxtDateRent.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateRent.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateRent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateRent.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateRent.Properties.Mask.EditMask = "yyyy/MM/dd HH:mm:ss tt";
		this.TxtDateRent.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDateRent.Size = new System.Drawing.Size(280, 28);
		this.TxtDateRent.TabIndex = 135;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label24.Location = new System.Drawing.Point(621, 65);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(90, 21);
		this.label24.TabIndex = 136;
		this.label24.Text = "تاريخ الايجار";
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label16.Location = new System.Drawing.Point(1074, 142);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(73, 21);
		this.label16.TabIndex = 134;
		this.label16.Text = "ملاحظات";
		this.TxtNote.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNote.Location = new System.Drawing.Point(728, 140);
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNote.Size = new System.Drawing.Size(340, 28);
		this.TxtNote.TabIndex = 133;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label17.Location = new System.Drawing.Point(621, 104);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(81, 21);
		this.label17.TabIndex = 132;
		this.label17.Text = "رقم الهوية";
		this.TxtSuppIDNum.Location = new System.Drawing.Point(335, 101);
		this.TxtSuppIDNum.Name = "TxtSuppIDNum";
		this.TxtSuppIDNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSuppIDNum.Properties.Appearance.Options.UseFont = true;
		this.TxtSuppIDNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSuppIDNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSuppIDNum.Size = new System.Drawing.Size(280, 28);
		this.TxtSuppIDNum.TabIndex = 131;
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
		this.label26.Location = new System.Drawing.Point(232, 143);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(95, 21);
		this.label26.TabIndex = 127;
		this.label26.Text = "مركز التكلفة";
		this.CBCostCenter.Font = new System.Drawing.Font("Tahoma", 10f);
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Location = new System.Drawing.Point(6, 140);
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.Size = new System.Drawing.Size(220, 29);
		this.CBCostCenter.TabIndex = 8;
		this.LblSup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblSup.AutoSize = true;
		this.LblSup.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblSup.Location = new System.Drawing.Point(1169, 65);
		this.LblSup.Name = "LblSup";
		this.LblSup.Size = new System.Drawing.Size(19, 21);
		this.LblSup.TabIndex = 125;
		this.LblSup.Text = "1";
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
		this.label23.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label23.Location = new System.Drawing.Point(1074, 104);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(93, 21);
		this.label23.TabIndex = 117;
		this.label23.Text = "اسم الكفيل";
		this.TxtParentName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtParentName.Location = new System.Drawing.Point(728, 101);
		this.TxtParentName.Name = "TxtParentName";
		this.TxtParentName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtParentName.Properties.Appearance.Options.UseFont = true;
		this.TxtParentName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtParentName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtParentName.Size = new System.Drawing.Size(340, 28);
		this.TxtParentName.TabIndex = 6;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label22.Location = new System.Drawing.Point(232, 104);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(84, 21);
		this.label22.TabIndex = 115;
		this.label22.Text = "رقم الجوال";
		this.TxtPhone.Location = new System.Drawing.Point(6, 101);
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.Size = new System.Drawing.Size(220, 28);
		this.TxtPhone.TabIndex = 5;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label21.Location = new System.Drawing.Point(232, 25);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(53, 21);
		this.label21.TabIndex = 113;
		this.label21.Text = "التاريخ";
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label19.Location = new System.Drawing.Point(1074, 65);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(93, 21);
		this.label19.TabIndex = 107;
		this.label19.Text = "اسم العميل";
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
		this.TxtSuppliers.Size = new System.Drawing.Size(340, 28);
		this.TxtSuppliers.TabIndex = 3;
		this.label20.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label20.Location = new System.Drawing.Point(1074, 25);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(90, 21);
		this.label20.TabIndex = 105;
		this.label20.Text = "رقم الفاتورة";
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
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.TxtDeposit);
		this.groupBox2.Controls.Add(this.numericEditBox1);
		this.groupBox2.Controls.Add(this.numericEditBox2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.numericEditBox3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.numericEditBox4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtLocation);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.txtdicoutprege);
		this.groupBox2.Controls.Add(this.label5);
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
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtDiscount);
		this.groupBox2.Controls.Add(this.TxtVatBeforeDisc);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Location = new System.Drawing.Point(12, 496);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1195, 189);
		this.groupBox2.TabIndex = 219;
		this.groupBox2.TabStop = false;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label12.Location = new System.Drawing.Point(532, 79);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(146, 19);
		this.label12.TabIndex = 211;
		this.label12.Text = "المبلغ المدفوع تأمين";
		this.TxtDeposit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDeposit.DecimalDigits = 2;
		this.TxtDeposit.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtDeposit.ForeColor = System.Drawing.Color.Blue;
		this.TxtDeposit.Location = new System.Drawing.Point(494, 101);
		this.TxtDeposit.Name = "TxtDeposit";
		this.TxtDeposit.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDeposit.Size = new System.Drawing.Size(221, 48);
		this.TxtDeposit.TabIndex = 210;
		this.TxtDeposit.Text = "0.00";
		this.TxtDeposit.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDeposit.UseCompatibleTextRendering = false;
		this.TxtDeposit.Value = new decimal(new int[4]);
		this.TxtDeposit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.numericEditBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.numericEditBox1.DecimalDigits = 3;
		this.numericEditBox1.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.numericEditBox1.ForeColor = System.Drawing.Color.Black;
		this.numericEditBox1.Location = new System.Drawing.Point(996, 4);
		this.numericEditBox1.Name = "numericEditBox1";
		this.numericEditBox1.ReadOnly = true;
		this.numericEditBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.numericEditBox1.Size = new System.Drawing.Size(107, 27);
		this.numericEditBox1.TabIndex = 197;
		this.numericEditBox1.Text = "0.000";
		this.numericEditBox1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.numericEditBox1.Value = new decimal(new int[4]);
		this.numericEditBox1.Visible = false;
		this.numericEditBox1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.numericEditBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.numericEditBox2.DecimalDigits = 3;
		this.numericEditBox2.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.numericEditBox2.ForeColor = System.Drawing.Color.Black;
		this.numericEditBox2.Location = new System.Drawing.Point(335, -3);
		this.numericEditBox2.Name = "numericEditBox2";
		this.numericEditBox2.ReadOnly = true;
		this.numericEditBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.numericEditBox2.Size = new System.Drawing.Size(107, 27);
		this.numericEditBox2.TabIndex = 206;
		this.numericEditBox2.Text = "0.000";
		this.numericEditBox2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.numericEditBox2.Value = new decimal(new int[4]);
		this.numericEditBox2.Visible = false;
		this.numericEditBox2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label1.Location = new System.Drawing.Point(448, 2);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(139, 19);
		this.label1.TabIndex = 205;
		this.label1.Text = "الضريبة التبغ 100%";
		this.label1.Visible = false;
		this.numericEditBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.numericEditBox3.DecimalDigits = 3;
		this.numericEditBox3.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.numericEditBox3.ForeColor = System.Drawing.Color.Black;
		this.numericEditBox3.Location = new System.Drawing.Point(587, 5);
		this.numericEditBox3.Name = "numericEditBox3";
		this.numericEditBox3.ReadOnly = true;
		this.numericEditBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.numericEditBox3.Size = new System.Drawing.Size(107, 27);
		this.numericEditBox3.TabIndex = 208;
		this.numericEditBox3.Text = "0.000";
		this.numericEditBox3.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.numericEditBox3.Value = new decimal(new int[4]);
		this.numericEditBox3.Visible = false;
		this.numericEditBox3.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label2.Location = new System.Drawing.Point(700, 10);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(113, 19);
		this.label2.TabIndex = 207;
		this.label2.Text = "الضريبة المضافة";
		this.label2.Visible = false;
		this.numericEditBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.numericEditBox4.DecimalDigits = 3;
		this.numericEditBox4.Font = new System.Drawing.Font("Tahoma", 10f);
		this.numericEditBox4.ForeColor = System.Drawing.Color.Black;
		this.numericEditBox4.Location = new System.Drawing.Point(815, 4);
		this.numericEditBox4.Name = "numericEditBox4";
		this.numericEditBox4.ReadOnly = true;
		this.numericEditBox4.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.numericEditBox4.Size = new System.Drawing.Size(107, 28);
		this.numericEditBox4.TabIndex = 209;
		this.numericEditBox4.Text = "0.000";
		this.numericEditBox4.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.numericEditBox4.Value = new decimal(new int[4]);
		this.numericEditBox4.Visible = false;
		this.numericEditBox4.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label3.Location = new System.Drawing.Point(1109, 10);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(97, 19);
		this.label3.TabIndex = 196;
		this.label3.Text = "تكلفة الفاتورة";
		this.label3.Visible = false;
		this.TxtLocation.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtLocation.EditValue = "";
		this.TxtLocation.Location = new System.Drawing.Point(494, 155);
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
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label4.Location = new System.Drawing.Point(721, 159);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(51, 19);
		this.label4.TabIndex = 191;
		this.label4.Text = "الموقع";
		this.txtdicoutprege.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Black;
		this.txtdicoutprege.Location = new System.Drawing.Point(6, 87);
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtdicoutprege.Size = new System.Drawing.Size(107, 27);
		this.txtdicoutprege.TabIndex = 189;
		this.txtdicoutprege.Text = "0.000";
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label5.Location = new System.Drawing.Point(928, 12);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(66, 19);
		this.label5.TabIndex = 204;
		this.label5.Text = "المجموع";
		this.label5.Visible = false;
		this.TxtTafqit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
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
		this.TxtNet22.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNet22.DecimalDigits = 3;
		this.TxtNet22.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtNet22.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet22.Location = new System.Drawing.Point(797, 36);
		this.TxtNet22.Name = "TxtNet22";
		this.TxtNet22.ReadOnly = true;
		this.TxtNet22.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNet22.Size = new System.Drawing.Size(392, 48);
		this.TxtNet22.TabIndex = 92;
		this.TxtNet22.Text = "0.000";
		this.TxtNet22.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet22.UseCompatibleTextRendering = false;
		this.TxtNet22.Value = new decimal(new int[4]);
		this.TxtNet22.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label8.Location = new System.Drawing.Point(327, 127);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(109, 19);
		this.label8.TabIndex = 171;
		this.label8.Text = "الضريبة بالخصم";
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
		this.TxtTotals.Location = new System.Drawing.Point(214, 19);
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
		this.TxtTotalAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTotalAll.DecimalDigits = 3;
		this.TxtTotalAll.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotalAll.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalAll.Location = new System.Drawing.Point(6, 19);
		this.TxtTotalAll.Name = "TxtTotalAll";
		this.TxtTotalAll.ReadOnly = true;
		this.TxtTotalAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotalAll.Size = new System.Drawing.Size(107, 27);
		this.TxtTotalAll.TabIndex = 161;
		this.TxtTotalAll.Text = "0.000";
		this.TxtTotalAll.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalAll.Value = new decimal(new int[4]);
		this.TxtTotalAll.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.TxtNet.TabIndex = 159;
		this.TxtNet.Text = "0.000";
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label6.Location = new System.Drawing.Point(119, 93);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(93, 19);
		this.label6.TabIndex = 163;
		this.label6.Text = "نسبة الخصم";
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label9.Location = new System.Drawing.Point(119, 25);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(70, 19);
		this.label9.TabIndex = 164;
		this.label9.Text = "الاجمالي";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label10.Location = new System.Drawing.Point(119, 59);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(89, 19);
		this.label10.TabIndex = 165;
		this.label10.Text = "قيمة الخصم";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label11.Location = new System.Drawing.Point(119, 127);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(58, 19);
		this.label11.TabIndex = 166;
		this.label11.Text = "الصافي";
		this.TxtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscount.Location = new System.Drawing.Point(6, 54);
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDiscount.Size = new System.Drawing.Size(107, 27);
		this.TxtDiscount.TabIndex = 160;
		this.TxtDiscount.Text = "0.000";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label13.Location = new System.Drawing.Point(327, 59);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(92, 19);
		this.label13.TabIndex = 167;
		this.label13.Text = "الضريبة VAT";
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label14.Location = new System.Drawing.Point(327, 24);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(107, 19);
		this.label14.TabIndex = 168;
		this.label14.Text = "المجموع Total";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1219, 697);
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.TxtCost);
		base.Controls.Add(this.TxtTabocc);
		base.Controls.Add(this.label29);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.TxtAllVat);
		base.Controls.Add(this.label28);
		base.Controls.Add(this.TxtAllTotal);
		base.Controls.Add(this.label30);
		base.Controls.Add(this.label27);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmRecipt";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "فاتورة استلام الايجار";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
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
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRentNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeRent.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRent.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRent.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppIDNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtParentName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


