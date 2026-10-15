using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
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
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.OrderRent;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.OrderRent.Search;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.OrderRent;

public class FrmRecipt : XtraForm
{
	private Class_Items ITM = new Class_Items();

	private Class_Recipt ORD = new Class_Recipt();

	private Class_Rent RNT = new Class_Rent();

	private Class_Customer Cust = new Class_Customer();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private GTSdbEntities db = new GTSdbEntities();

	private Calss_CostCenter Cost = new Calss_CostCenter();

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

	public int UserSellPrice = LoginDetails.UserSellPrice;

	public decimal Cash;

	public decimal Bank;

	public bool ISEVENTDISC = true;

	private int DecimalNum = LoginDetails.DecimalNum;

	private string fileName = Application.StartupPath + "\\Layout\\RentRecipt.xml";

	private IContainer components = null;

	private TextEdit TxtLocation;

	private Label label25;

	private NumericEditBox txtdicoutprege;

	private TextEdit TxtTafqit;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private ToolStripMenuItem RecoveredMenuItemFix1;

	private NumericEditBox TxtNet22;

	private GridColumn gridColumn4;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private ToolStripMenuItem RecoveredMenuItemFix2;

	private ToolStripMenuItem RecoveredMenuItemFix3;

	private ToolStripMenuItem RecoveredMenuItemFix4;

	private ToolStripMenuItem RecoveredMenuItemFix5;

	private ToolStripMenuItem RecoveredMenuItemFix6;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem RecoveredMenuItemFix7;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem MenuExit;

	private GroupBox groupBox3;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn1;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private GridColumn gridColumn2;

	private GridColumn shearch;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private GridColumn gridColumn19;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private GridColumn gridColumn22;

	private GridColumn gridColumn5;

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

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private NumericEditBox TxtDiscount;

	private NumericEditBox TxtVatBeforeDisc;

	private Label label5;

	private ToolStripMenuItem RecoveredMenuItemFix8;

	private Label label26;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private Label LblSup;

	private DateEdit TxtDate;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private Label label23;

	private TextEdit TxtParentName;

	private Label label22;

	private TextEdit TxtPhone;

	private Label label21;

	private Label label18;

	private GroupBox groupBox1;

	private ButtonEdit TxtSuppliers;

	private Label label17;

	private ButtonEdit TxtCode;

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private ToolStripMenuItem RecoveredMenuItemFix9;

	private ToolStripMenuItem MenuPurchOrder;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem MenuSearchItem;

	private ToolStripMenuItem RecoveredMenuItemFix10;

	private ToolStripMenuItem MenuNewItem;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripMenuItem MenuPreviewFatoura;

	private ToolStripMenuItem MenuPrintQued;

	private ToolStripMenuItem RecoveredMenuItemFix11;

	private ToolStripMenuItem MenuDelete;

	private ToolStripMenuItem MenuEdit;

	private ToolStripMenuItem MenuSave;

	private ToolStripMenuItem MenuNew;

	private ContextMenuStrip contextMenuStrip1;

	private RibbonPage ribbonPage2;

	private Label label6;

	private GroupBox groupBox2;

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

	private Label label11;

	private TextEdit TxtNote;

	private Label label10;

	private TextEdit TxtSuppIDNum;

	private Label label9;

	private TextEdit TxtAddress;

	private Label label12;

	private NumericEditBox TxtDeposit;

	private Label label31;

	private TextEdit TxtDateRentNum;

	private Label label13;

	private ButtonEdit TxtCodeRent;

	private DateEdit TxtDateRent;

	private Label label24;

	private GridColumn gridColumn28;

	private SimpleButton BtnMoney;

	public FrmRecipt()
	{
		InitializeComponent();
		TxtDigital();
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		TxtDate.DateTime = DateTime.Now;
		TxtDateRent.DateTime = DateTime.Now;
		SetInitialRow();
		gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
		base.KeyPreview = true;
		gridView1.Columns["الكمية"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["الكمية"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["سعر الوحدة"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["سعر الوحدة"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.FocusedRowHandle = 1;
		gridView1.FocusedColumn = gridView1.VisibleColumns[0];
		gridView1.ShowEditor();
		BindCBCostCenter();
		GroupPermission();
		TxtSuppliers.Text = "عميل نقدى";
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtDate.Focus();
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		txtdicoutprege.Controls[0].Visible = false;
		TxtDateRent.ReadOnly = true;
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
		int num = 0;
		if (e.Column.FieldName == "كود_الصنف")
		{
			string text = Convert.ToString(e.Value);
			Item_Items item_Items = ORD.GetItems(text).FirstOrDefault();
			if (item_Items != null)
			{
				if (0 == 0)
				{
					object rowCellValue = gridView1.GetRowCellValue(e.RowHandle, "تم");
					string text2 = rowCellValue.ToString();
					if (text2 == "0")
					{
						if (item_Items != null)
						{
							DataRow dataRow = null;
							dataRow = datatable.NewRow();
							dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
							dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
							dataRow["كود_الصنف"] = item_Items.Item_code;
							dataRow["أسم الصنف"] = item_Items.item_Name;
							dataRow["الوحدة"] = item_Items.Item_Unit.Name;
							dataRow["رقم_الوحدة"] = item_Items.Item_Unit.ID;
							dataRow["الكمية"] = 1;
							if (item_Items.UnitTafqitID == 1)
							{
								int value = Convert.ToInt32(item_Items.UnitSmall);
								dataRow["وحدة التفقيط"] = ITM.GetUnitName(value);
								dataRow["كمية التفقيط"] = dataRow["الكمية"];
							}
							if (item_Items.UnitTafqitID == 2)
							{
								int value2 = Convert.ToInt32(item_Items.UnitMedium);
								dataRow["وحدة التفقيط"] = ITM.GetUnitName(value2);
								dataRow["كمية التفقيط"] = Convert.ToDecimal(dataRow["الكمية"]) * Convert.ToDecimal(item_Items.ConvertMediumUnit);
							}
							if (item_Items.UnitTafqitID == 3)
							{
								int value3 = Convert.ToInt32(item_Items.UnitLarge);
								dataRow["وحدة التفقيط"] = ITM.GetUnitName(value3);
								dataRow["كمية التفقيط"] = Convert.ToDecimal(dataRow["الكمية"]) * Convert.ToDecimal(item_Items.ConvertMediumUnit) * Convert.ToDecimal(item_Items.ConvertBigUnit);
							}
							dataRow["مسح"] = 0;
							dataRow["تم"] = 1;
							dataRow["أسم_الوحدة"] = "الصغرى";
							dataRow["متوسط_التكلفة"] = item_Items.Average_cost;
							dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
							dataRow["سعر الوحدة"] = item_Items.SellPriceSmall;
							dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(1)), DecimalNum);
							dataRow["المخزن"] = ORD.GetStoreName();
							dataRow["رقم المخزن"] = ORD.GetStoreID();
							if (ORD.IsVatActive())
							{
								if (item_Items.Is_Tax == true)
								{
									dataRow["الضربية"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(1) * item_Items.Tax_Value / (decimal?)100), DecimalNum);
									dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(1) * item_Items.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(1))), DecimalNum);
									dataRow["هل_ضريبة"] = 1;
									dataRow["قيمة_الضريبة"] = item_Items.Tax_Value;
								}
								else
								{
									dataRow["الضربية"] = 0.0;
									dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(1))), DecimalNum);
									dataRow["هل_ضريبة"] = 0;
									dataRow["قيمة_الضريبة"] = 0;
								}
							}
							else
							{
								dataRow["الضربية"] = 0.0;
								dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(1))), DecimalNum);
								dataRow["هل_ضريبة"] = 0;
								dataRow["قيمة_الضريبة"] = 0;
							}
							CheckFfistRow();
							datatable.Rows.Add(dataRow);
							insertnewrow();
							GetTotalCost();
							gridView1.FocusedRowHandle = e.RowHandle;
							gridView1.FocusedColumn = gridView1.VisibleColumns[-1];
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
							string text3 = Convert.ToString(e.Value);
							string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
							string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
							string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
							string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("الكمية");
							string focusedRowCellDisplayText5 = gridView1.GetFocusedRowCellDisplayText("ظ…");
							if (!(row["ظ…"].ToString() == focusedRowCellDisplayText5))
							{
								continue;
							}
							if (row["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row["كود_الصنف"] = item_Items.Item_code;
								row["أسم الصنف"] = item_Items.item_Name;
								row["سعر الوحدة"] = item_Items.SellPriceSmall;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4)), DecimalNum);
								if (ORD.IsVatActive())
								{
									if (item_Items.Is_Tax == true)
									{
										row["الضربية"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4) * item_Items.Tax_Value / (decimal?)100), DecimalNum);
										row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4) * item_Items.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4))), DecimalNum);
										row["هل_ضريبة"] = 1;
										row["قيمة_الضريبة"] = item_Items.Tax_Value;
									}
									else
									{
										row["الضربية"] = 0;
										row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4))), DecimalNum);
										row["هل_ضريبة"] = 0;
										row["قيمة_الضريبة"] = 0;
									}
								}
								else
								{
									row["الضربية"] = 0;
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4))), DecimalNum);
									row["هل_ضريبة"] = 0;
									row["قيمة_الضريبة"] = 0;
								}
								break;
							}
							if (row["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								row["كود_الصنف"] = item_Items.Item_code;
								row["أسم الصنف"] = item_Items.item_Name;
								row["سعر الوحدة"] = item_Items.SellPriceMedium;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceMedium * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4)), DecimalNum);
								if (ORD.IsVatActive())
								{
									if (item_Items.Is_Tax == true)
									{
										row["الضربية"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceMedium * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4) * item_Items.Tax_Value / (decimal?)100), DecimalNum);
										row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceMedium * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4) * item_Items.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items.SellPriceMedium * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4))), DecimalNum);
										row["هل_ضريبة"] = 1;
										row["قيمة_الضريبة"] = item_Items.Tax_Value;
									}
									else
									{
										row["الضربية"] = 0;
										row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellPriceMedium * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4))), DecimalNum);
										row["هل_ضريبة"] = 0;
										row["قيمة_الضريبة"] = 0;
									}
								}
								else
								{
									row["الضربية"] = 0;
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellPriceMedium * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4))), DecimalNum);
									row["هل_ضريبة"] = 0;
									row["قيمة_الضريبة"] = 0;
								}
								break;
							}
							if (!(row["أسم_الوحدة"].ToString() == "الكبرى"))
							{
								continue;
							}
							row["كود_الصنف"] = item_Items.Item_code;
							row["أسم الصنف"] = item_Items.item_Name;
							row["سعر الوحدة"] = item_Items.SellpriceLarge;
							row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.SellpriceLarge * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4)), DecimalNum);
							if (ORD.IsVatActive())
							{
								if (item_Items.Is_Tax == true)
								{
									row["الضربية"] = Math.Round(Convert.ToDecimal(item_Items.SellpriceLarge * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4) * item_Items.Tax_Value / (decimal?)100), DecimalNum);
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items.SellpriceLarge * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4) * item_Items.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items.SellpriceLarge * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4))), DecimalNum);
									row["هل_ضريبة"] = 1;
									row["قيمة_الضريبة"] = item_Items.Tax_Value;
								}
								else
								{
									row["هل_ضريبة"] = 0;
									row["قيمة_الضريبة"] = 0;
									row["الضربية"] = 0;
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellpriceLarge * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4))), DecimalNum);
								}
							}
							else
							{
								row["هل_ضريبة"] = 0;
								row["قيمة_الضريبة"] = 0;
								row["الضربية"] = 0;
								row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellpriceLarge * (decimal?)Convert.ToDecimal(focusedRowCellDisplayText4))), DecimalNum);
							}
							break;
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
				CodeItm = text;
				SearchItems();
				LoginDetails.ItemSearch = "";
			}
		}
		else if (e.Column.FieldName == "الكمية")
		{
			string value4 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText6 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText7 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText8 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText9 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText6 != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(focusedRowCellDisplayText6).FirstOrDefault();
				if (get_Item_ByCodeALL_Result != null)
				{
					foreach (DataRow row2 in datatable.Rows)
					{
						if (!(row2["ظ…"].ToString() == focusedRowCellDisplayText9))
						{
							continue;
						}
						decimal num2 = Convert.ToDecimal(row2["سعر الوحدة"].ToString());
						if (row2["أسم_الوحدة"].ToString() == "الصغرى")
						{
							row2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row2["متوسط_التكلفة"]) * Convert.ToDecimal(row2["الكمية"]), DecimalNum);
							row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value4)), DecimalNum);
							if (ORD.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result.Is_Tax == true)
								{
									decimal value5 = num2 * Convert.ToDecimal(value4);
									decimal? tax_Value = get_Item_ByCodeALL_Result.Tax_Value;
									row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value5 * tax_Value / (decimal?)100), DecimalNum);
									decimal value6 = num2 * Convert.ToDecimal(value4);
									decimal? tax_Value2 = get_Item_ByCodeALL_Result.Tax_Value;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value6 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num2 * Convert.ToDecimal(value4))), DecimalNum);
									row2["هل_ضريبة"] = 1;
									row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
								}
								else
								{
									row2["الضربية"] = 0;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value4))), DecimalNum);
									row2["هل_ضريبة"] = 0;
									row2["قيمة_الضريبة"] = 0;
								}
							}
							else
							{
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value4))), DecimalNum);
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
							}
							break;
						}
						if (row2["أسم_الوحدة"].ToString() == "المتوسطة")
						{
							decimal num3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertMediumUnit);
							row2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row2["متوسط_التكلفة"]) * (Convert.ToDecimal(row2["الكمية"]) * num3), DecimalNum);
							row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value4)), DecimalNum);
							if (ORD.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result.Is_Tax == true)
								{
									decimal value5 = num2 * Convert.ToDecimal(value4);
									decimal? tax_Value = get_Item_ByCodeALL_Result.Tax_Value;
									row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value5 * tax_Value / (decimal?)100), DecimalNum);
									decimal value6 = num2 * Convert.ToDecimal(value4);
									decimal? tax_Value2 = get_Item_ByCodeALL_Result.Tax_Value;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value6 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num2 * Convert.ToDecimal(value4))), DecimalNum);
									row2["هل_ضريبة"] = 1;
									row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
								}
								else
								{
									row2["هل_ضريبة"] = 0;
									row2["قيمة_الضريبة"] = 0;
									row2["الضربية"] = 0;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value4))), DecimalNum);
								}
							}
							else
							{
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value4))), DecimalNum);
							}
							break;
						}
						if (!(row2["أسم_الوحدة"].ToString() == "الكبرى"))
						{
							continue;
						}
						decimal num4 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertBigUnit);
						row2["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(row2["متوسط_التكلفة"]) * (Convert.ToDecimal(row2["الكمية"]) * num4), DecimalNum);
						row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value4)), DecimalNum);
						if (ORD.IsVatActive())
						{
							if (get_Item_ByCodeALL_Result.Is_Tax == true)
							{
								decimal value5 = num2 * Convert.ToDecimal(value4);
								decimal? tax_Value = get_Item_ByCodeALL_Result.Tax_Value;
								row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value5 * tax_Value / (decimal?)100), DecimalNum);
								decimal value6 = num2 * Convert.ToDecimal(value4);
								decimal? tax_Value2 = get_Item_ByCodeALL_Result.Tax_Value;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value6 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num2 * Convert.ToDecimal(value4))), DecimalNum);
								row2["هل_ضريبة"] = 1;
								row2["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
							}
							else
							{
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value4))), DecimalNum);
							}
						}
						else
						{
							row2["هل_ضريبة"] = 0;
							row2["قيمة_الضريبة"] = 0;
							row2["الضربية"] = 0;
							row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value4))), DecimalNum);
						}
						break;
					}
					CheckFfistRow();
					insertnewrow();
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
			string value7 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText10 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText11 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText12 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText13 = gridView1.GetFocusedRowCellDisplayText("الكمية");
			string focusedRowCellDisplayText14 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText10 != "")
			{
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result2 = ITM.GetItemByCode(focusedRowCellDisplayText10).FirstOrDefault();
				if (get_Item_ByCodeALL_Result2 != null)
				{
					foreach (DataRow row3 in datatable.Rows)
					{
						if (!(row3["ظ…"].ToString() == focusedRowCellDisplayText14))
						{
							continue;
						}
						if (row3["أسم_الوحدة"].ToString() == "الصغرى")
						{
							decimal sellPermission = ORD.GetSellPermission(focusedRowCellDisplayText10, UserSellPrice, "الصغرى");
							if (UserSellPrice == 1)
							{
								if (Convert.ToDecimal(value7) < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row3["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 2)
							{
								if (Convert.ToDecimal(value7) < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row3["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 3)
							{
								if (Convert.ToDecimal(value7) < sellPermission)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row3["سعر الوحدة"] = sellPermission;
									return;
								}
							}
							else if (UserSellPrice == 4 && Convert.ToDecimal(value7) < sellPermission)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row3["سعر الوحدة"] = sellPermission;
								return;
							}
							row3["سعر الوحدة"] = value7;
							row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13)), DecimalNum);
							if (ORD.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result2.Is_Tax == true)
								{
									decimal value5 = Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13);
									decimal? tax_Value = get_Item_ByCodeALL_Result2.Tax_Value;
									row3["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value5 * tax_Value / (decimal?)100), DecimalNum);
									decimal value6 = Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13);
									decimal? tax_Value2 = get_Item_ByCodeALL_Result2.Tax_Value;
									row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value6 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13))), DecimalNum);
									row3["هل_ضريبة"] = 1;
									row3["قيمة_الضريبة"] = get_Item_ByCodeALL_Result2.Tax_Value;
								}
								else
								{
									row3["هل_ضريبة"] = 0;
									row3["قيمة_الضريبة"] = 0;
									row3["الضربية"] = 0;
									row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13))), DecimalNum);
								}
							}
							else
							{
								row3["هل_ضريبة"] = 0;
								row3["قيمة_الضريبة"] = 0;
								row3["الضربية"] = 0;
								row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13))), DecimalNum);
							}
							break;
						}
						if (row3["أسم_الوحدة"].ToString() == "المتوسطة")
						{
							decimal sellPermission2 = ORD.GetSellPermission(focusedRowCellDisplayText10, UserSellPrice, "المتوسطة");
							if (UserSellPrice == 1)
							{
								if (Convert.ToDecimal(value7) < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row3["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 2)
							{
								if (Convert.ToDecimal(value7) < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row3["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 3)
							{
								if (Convert.ToDecimal(value7) < sellPermission2)
								{
									MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									row3["سعر الوحدة"] = sellPermission2;
									return;
								}
							}
							else if (UserSellPrice == 4 && Convert.ToDecimal(value7) < sellPermission2)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row3["سعر الوحدة"] = sellPermission2;
								return;
							}
							row3["سعر الوحدة"] = value7;
							row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13)), DecimalNum);
							if (ORD.IsVatActive())
							{
								if (get_Item_ByCodeALL_Result2.Is_Tax == true)
								{
									decimal value5 = Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13);
									decimal? tax_Value = get_Item_ByCodeALL_Result2.Tax_Value;
									row3["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value5 * tax_Value / (decimal?)100), DecimalNum);
									decimal value6 = Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13);
									decimal? tax_Value2 = get_Item_ByCodeALL_Result2.Tax_Value;
									row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value6 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13))), DecimalNum);
									row3["هل_ضريبة"] = 1;
									row3["قيمة_الضريبة"] = get_Item_ByCodeALL_Result2.Tax_Value;
								}
								else
								{
									row3["هل_ضريبة"] = 0;
									row3["قيمة_الضريبة"] = 0;
									row3["الضربية"] = 0;
									row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13))), DecimalNum);
								}
							}
							else
							{
								row3["هل_ضريبة"] = 0;
								row3["قيمة_الضريبة"] = 0;
								row3["الضربية"] = 0;
								row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13))), DecimalNum);
							}
							break;
						}
						if (!(row3["أسم_الوحدة"].ToString() == "الكبرى"))
						{
							continue;
						}
						decimal sellPermission3 = ORD.GetSellPermission(focusedRowCellDisplayText10, UserSellPrice, "الكبرى");
						if (UserSellPrice == 1)
						{
							if (Convert.ToDecimal(value7) < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع القطاعي", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row3["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 2)
						{
							if (Convert.ToDecimal(value7) < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع الجملة", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row3["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 3)
						{
							if (Convert.ToDecimal(value7) < sellPermission3)
							{
								MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر بيع جملة الجملة", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								row3["سعر الوحدة"] = sellPermission3;
								return;
							}
						}
						else if (UserSellPrice == 4 && Convert.ToDecimal(value7) < sellPermission3)
						{
							MessageBox.Show("عفوا\u064b لا يمكن بيع هذا الصنف بسعر اقل من سعر الشراء", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							row3["سعر الوحدة"] = sellPermission3;
							return;
						}
						row3["سعر الوحدة"] = value7;
						row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13)), DecimalNum);
						if (ORD.IsVatActive())
						{
							if (get_Item_ByCodeALL_Result2.Is_Tax == true)
							{
								row3["هل_ضريبة"] = 1;
								row3["قيمة_الضريبة"] = get_Item_ByCodeALL_Result2.Tax_Value;
								decimal value5 = Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13);
								decimal? tax_Value = get_Item_ByCodeALL_Result2.Tax_Value;
								row3["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value5 * tax_Value / (decimal?)100), DecimalNum);
								decimal value6 = Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13);
								decimal? tax_Value2 = get_Item_ByCodeALL_Result2.Tax_Value;
								row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value6 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13))), DecimalNum);
							}
							else
							{
								row3["هل_ضريبة"] = 0;
								row3["قيمة_الضريبة"] = 0;
								row3["الضربية"] = 0;
								row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13))), DecimalNum);
							}
						}
						else
						{
							row3["هل_ضريبة"] = 0;
							row3["قيمة_الضريبة"] = 0;
							row3["الضربية"] = 0;
							row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value7) * Convert.ToDecimal(focusedRowCellDisplayText13))), DecimalNum);
						}
						break;
					}
					CheckFfistRow();
					insertnewrow();
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
		string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
		string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
		string StoreName = gridView1.GetFocusedRowCellDisplayText("المخزن");
		int num = (from x in db.Account_Stores
			where x.Store_Name == StoreName
			select x.ID).FirstOrDefault();
		CodeItm = focusedRowCellDisplayText;
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
			FrmSearchRecipt frmSearchRecipt = new FrmSearchRecipt();
			frmSearchRecipt.ShowDialog();
			if (frmSearchRecipt.CustCode > 0)
			{
				int custCode = frmSearchRecipt.CustCode;
				OrderRent_Recipt orderRent_Recipt = ORD.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<OrderRent_RecpitDetails> itemsPurchesByID = ORD.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (OrderRent_RecpitDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = ORD.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
					dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["عدد الايام"] = item.DayNum;
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
				CBCostCenter.SelectedValue = orderRent_Recipt.CostCentersID;
				TxtCode.Text = orderRent_Recipt.PurBranchID.ToString();
				LblSup.Text = orderRent_Recipt.SupplierID.ToString();
				TxtSuppliers.Text = orderRent_Recipt.SupplierName;
				TxtDate.DateTime = Convert.ToDateTime(orderRent_Recipt.Purchases_Date);
				TxtDateRent.DateTime = Convert.ToDateTime(orderRent_Recipt.Rent_Date);
				TxtDateRentNum.Text = orderRent_Recipt.DateNum.ToString();
				TxtParentName.Text = orderRent_Recipt.ParentName;
				TxtPhone.Text = orderRent_Recipt.SupplierPhone;
				TxtNote.Text = orderRent_Recipt.Note;
				TxtDeposit.Text = orderRent_Recipt.deposit.ToString();
				TxtAddress.Text = orderRent_Recipt.Address;
				TxtSuppIDNum.Text = orderRent_Recipt.SupplierIDNum;
				TxtTotals.Text = Convert.ToString(orderRent_Recipt.TotalPrices);
				TxtVat.Text = Convert.ToString(orderRent_Recipt.Tax);
				TxtTotalAll.Text = Convert.ToString(orderRent_Recipt.Safy);
				TxtDiscount.Text = orderRent_Recipt.DiscountNum.ToString();
				TxtTotalBeforDisc.Text = orderRent_Recipt.TotalPrices_Discount.ToString();
				TxtVatBeforeDisc.Text = orderRent_Recipt.Tax_Discount.ToString();
				TxtNet.Text = orderRent_Recipt.Net.ToString();
				TxtNet22.Text = orderRent_Recipt.Net.ToString();
				TxtCost.Text = orderRent_Recipt.CostOrder.ToString();
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

	private void TxtCust_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustCode > 0)
			{
				int FrmID = frmSearchCustomer.CustCode;
				List<Account_CustSup> all = Cust.GetAll();
				Account_CustSup account_CustSup = all.Where((Account_CustSup x) => x.ID == FrmID && x.IsCustomers == true).FirstOrDefault();
				LblSup.Text = FrmID.ToString();
				TxtSuppliers.Text = account_CustSup.CustSuppName;
				TxtPhone.Text = account_CustSup.Phone;
				TxtAddress.Text = account_CustSup.Address;
				TxtSuppIDNum.Text = account_CustSup.VatNum;
			}
		}
		catch (Exception)
		{
			Clear();
		}
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
				OrderRent_Rent orderRent_Rent = RNT.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<OrderRent_RentDetails> itemsPurchesByID = RNT.GetItemsPurchesByID(custCode);
				int num = 1;
				TxtDateRent.DateTime = Convert.ToDateTime(orderRent_Rent.Purchases_Date);
				TxtDate.DateTime = DateTime.Now;
				foreach (OrderRent_RentDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = RNT.GetItemsByitemid(itemID).FirstOrDefault();
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
					dataRow["عدد الايام"] = Convert.ToDecimal(TxtDateRentNum.Text);
					dataRow["متوسط_التكلفة"] = item.LastCost;
					dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]) * Convert.ToDecimal(dataRow["عدد الايام"]), DecimalNum);
					dataRow["سعر الوحدة"] = item.UnitPrice;
					dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(dataRow["سعر الوحدة"]) * Convert.ToDecimal(dataRow["الكمية"]) * Convert.ToDecimal(dataRow["عدد الايام"]), DecimalNum);
					dataRow["مسح"] = 0;
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
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
						dataRow["الضربية"] = 0;
						dataRow["إجمالى المبلغ"] = dataRow["الاجمالى"];
					}
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				insertnewrow();
				CBCostCenter.SelectedValue = orderRent_Rent.CostCentersID;
				TxtCodeRent.Text = orderRent_Rent.PurBranchID.ToString();
				LblSup.Text = orderRent_Rent.SupplierID.ToString();
				TxtSuppliers.Text = orderRent_Rent.SupplierName;
				TxtPhone.Text = orderRent_Rent.SupplierPhone;
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
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = false;
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = true;
				FiterDatatableItemsUpdate();
				Cal_FinalResultTotals();
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
			CBCostCenter.DataSource = Cost.GetSearchAccountCostCenter();
			CBCostCenter.DisplayMember = "CostCentersName";
			CBCostCenter.ValueMember = "CostCentersID";
			CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
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
		object summaryValue = gridView1.Columns["إجمالى المبلغ"].SummaryItem.SummaryValue;
		object summaryValue2 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
		TxtTotals.Text = Convert.ToString(summaryValue2);
		object summaryValue3 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
		TxtVat.Text = Convert.ToString(summaryValue3);
		decimal num = Convert.ToDecimal(TxtDiscount.Text);
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
		dataRow["المخزن"] = ORD.GetStoreName();
		dataRow["رقم المخزن"] = ORD.GetStoreID();
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
		datatable.Columns.Add("ItemID", typeof(int));
		datatable.Columns.Add("ItemUnitID", typeof(int));
		datatable.Columns.Add("ItemNote", typeof(string));
		datatable.Columns.Add("كود_الصنف", typeof(string));
		datatable.Columns.Add("أسم الصنف", typeof(string));
		datatable.Columns.Add("الوحدة", typeof(string));
		datatable.Columns.Add("رقم_الوحدة", typeof(int));
		datatable.Columns.Add("المخزن", typeof(string));
		datatable.Columns.Add("رقم المخزن", typeof(int));
		datatable.Columns.Add("الكمية", typeof(decimal));
		datatable.Columns.Add("عدد الايام", typeof(decimal));
		datatable.Columns.Add("وحدة التفقيط", typeof(string));
		datatable.Columns.Add("كمية التفقيط", typeof(decimal));
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
		datatable.Columns.Add("ظ…", typeof(int));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تم", typeof(int));
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
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["رقم_الوحدة"] = 1;
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
		dataRow["المخزن"] = ORD.GetStoreName();
		dataRow["ItemNote"] = "";
		dataRow["مسح"] = 0;
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridControl1.ProcessGridKey += gridControl_ProcessGridKey;
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
				if (num < quantityPermission)
				{
					MessageBox.Show("نعتذر، لا يوجد كمية كافية من الصنف ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					result = 0;
					break;
				}
			}
		}
		return result;
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
					decimal value = Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", ""));
					decimal value2 = Convert.ToDecimal(value);
					decimal value3 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
					decimal value4 = Convert.ToDecimal(value3);
					decimal num = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
					TxtAllTotal.Text = Convert.ToString(num);
					decimal value5 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعد_الخصم)", ""));
					decimal num2 = Convert.ToDecimal(value5);
					decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
					TxtCost.Text = Convert.ToString(num3);
					TxtAllTotal.Text = value2.ToString();
					TxtNet22.Text = Math.Round(Convert.ToDecimal(value2), DecimalNum).ToString();
					TxtTotalAll.Text = Math.Round(Convert.ToDecimal(value2) + Convert.ToDecimal(value4), DecimalNum).ToString();
					TxtTotalBeforDisc.Text = (Convert.ToDecimal(TxtAllTotal.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
					TxtTabocc.Text = TxtTotalBeforDisc.Text;
					TxtAllVat.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m * Convert.ToDecimal(1.05) - Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m).ToString();
					TxtNet.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m + Convert.ToDecimal(TxtAllVat.Text)).ToString();
					TxtVatBeforeDisc.Text = TxtVat.Text;
					TxtAllVat.Text = (Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m * Convert.ToDecimal(1.05) - Convert.ToDecimal(TxtTotalBeforDisc.Text) * 2m).ToString();
					TxtNet22.Text = TxtTotalBeforDisc.Text;
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
				decimal value6 = Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", ""));
				decimal value7 = Convert.ToDecimal(value6);
				decimal value8 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
				decimal value9 = Convert.ToDecimal(value8);
				decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
				TxtAllTotal.Text = Convert.ToString(num4);
				decimal value10 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعد_الخصم)", ""));
				decimal num5 = Convert.ToDecimal(value10);
				decimal num6 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
				TxtCost.Text = Convert.ToString(num6);
				TxtAllTotal.Text = value7.ToString();
				decimal value11 = Math.Round(Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)) / Math.Round(Convert.ToDecimal(TxtAllTotal.Text), DecimalNum) * 100m, DecimalNum);
				TxtNet22.Text = Math.Round(Convert.ToDecimal(value7), DecimalNum).ToString();
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(value7) + Convert.ToDecimal(value9), DecimalNum).ToString();
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
							row2["سعربعدالخصم"] = Convert.ToDecimal(row2["سعر الوحدة"].ToString()) - Convert.ToDecimal(value11) * Convert.ToDecimal(row2["سعر الوحدة"]) / 100m;
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
				foreach (DataRow row3 in datatable.Rows)
				{
					if (row3["أسم_الوحدة"].ToString() != "")
					{
						row3["سعربعدالخصم"] = row3["سعر الوحدة"];
						row3["إجمالى_بعدالخصم"] = row3["الاجمالى"];
						row3["الضريبه_بعد_الخصم"] = row3["الضربية"];
					}
				}
				decimal num7 = Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", ""));
				TxtTotals.Text = Convert.ToString(num7);
				decimal num8 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
				TxtVat.Text = Convert.ToString(num8);
				decimal num9 = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
				TxtTotalBeforDisc.Text = Convert.ToString(num9);
				decimal num10 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعد_الخصم)", ""));
				TxtVatBeforeDisc.Text = Convert.ToString(num10);
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)).ToString();
				TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)).ToString();
				decimal num11 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
				TxtCost.Text = Convert.ToString(num11);
				TxtAllVat.Text = TxtVatBeforeDisc.Text;
				TxtAllTotal.Text = TxtTotalAll.Text;
				TxtTabocc.Text = "0.00";
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
				return;
			}
			decimal value12 = Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text);
			decimal num12 = Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum) * 100m) / Math.Round(Convert.ToDecimal(value12), DecimalNum);
			foreach (DataRow row4 in datatable.Rows)
			{
				if (row4["أسم_الوحدة"].ToString() != "")
				{
					row4["سعربعدالخصم"] = Convert.ToDecimal(row4["سعر الوحدة"].ToString()) - Convert.ToDecimal(num12 * Convert.ToDecimal(row4["سعر الوحدة"].ToString()) / 100m);
					row4["إجمالى_بعدالخصم"] = Convert.ToDecimal(row4["سعربعدالخصم"].ToString()) * Convert.ToDecimal(row4["الكمية"].ToString());
					row4["الضريبه_بعد_الخصم"] = Convert.ToDecimal(Convert.ToDecimal(row4["سعربعدالخصم"]) * Convert.ToDecimal(row4["الكمية"]) * Convert.ToDecimal(row4["قيمة_الضريبة"]) / 100m);
				}
			}
			decimal num13 = Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", ""));
			TxtTotals.Text = Convert.ToString(num13);
			decimal num14 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
			TxtVat.Text = Convert.ToString(num14);
			decimal num15 = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
			TxtTotalBeforDisc.Text = Convert.ToString(num15);
			decimal num16 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعد_الخصم)", ""));
			TxtVatBeforeDisc.Text = Convert.ToString(num16);
			decimal num17 = Convert.ToDecimal(datatable.Compute("SUM(اجمالي_متوسط_التكلفة)", ""));
			TxtCost.Text = Convert.ToString(num17);
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

	private void Cal_FinalResultTotals2()
	{
		try
		{
			if (datatable.Rows.Count <= 1)
			{
				TxtVat.Text = "0.00";
				TxtNet.Text = "0.00";
				TxtNet22.Text = "0.00";
				TxtTotals.Text = "0.00";
				TxtDiscount.Text = "0.00";
				TxtVat.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtTotalAll.Text = "0.00";
				return;
			}
			if (txtdicoutprege.Text == "" || txtdicoutprege.Text == "0.00" || Convert.ToDecimal(txtdicoutprege.Text) > Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				decimal num = Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", ""));
				TxtTotals.Text = Convert.ToString(num);
				decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
				TxtVat.Text = Convert.ToString(num2);
				decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
				TxtTotalBeforDisc.Text = Convert.ToString(num3);
				decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعد_الخصم)", ""));
				TxtVatBeforeDisc.Text = Convert.ToString(num4);
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)).ToString();
				TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)).ToString();
				foreach (DataRow row in datatable.Rows)
				{
					if (row["أسم_الوحدة"].ToString() != "")
					{
						row["سعربعدالخصم"] = row["سعر الوحدة"];
						row["إجمالى_بعدالخصم"] = row["الاجمالى"];
						row["الضريبه_بعد_الخصم"] = row["الضربية"];
					}
				}
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
				return;
			}
			decimal value = Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text);
			txtdicoutprege.Text = Math.Round(Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum) * 100m) / Math.Round(Convert.ToDecimal(value), DecimalNum), DecimalNum).ToString();
			decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", ""));
			TxtTotals.Text = Convert.ToString(num5);
			decimal num6 = Convert.ToDecimal(datatable.Compute("SUM(الضربية)", ""));
			TxtVat.Text = Convert.ToString(num6);
			decimal num7 = Convert.ToDecimal(datatable.Compute("SUM(إجمالى_بعدالخصم)", ""));
			TxtTotalBeforDisc.Text = Convert.ToString(num7);
			decimal num8 = Convert.ToDecimal(datatable.Compute("SUM(الضريبه_بعد_الخصم)", ""));
			TxtVatBeforeDisc.Text = Convert.ToString(num8);
			TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
			TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
			TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
			foreach (DataRow row2 in datatable.Rows)
			{
				if (row2["أسم_الوحدة"].ToString() != "")
				{
					row2["سعربعدالخصم"] = Convert.ToDecimal(row2["سعر الوحدة"].ToString()) - Convert.ToDecimal(Convert.ToDecimal(txtdicoutprege.Text) * Convert.ToDecimal(row2["سعر الوحدة"].ToString()) / 100m);
					row2["إجمالى_بعدالخصم"] = Convert.ToDecimal(row2["سعربعدالخصم"].ToString()) * Convert.ToDecimal(row2["الكمية"].ToString());
					row2["الضريبه_بعد_الخصم"] = Convert.ToDecimal(Convert.ToDecimal(row2["سعربعدالخصم"]) * Convert.ToDecimal(row2["الكمية"]) * Convert.ToDecimal(row2["قيمة_الضريبة"]) / 100m);
				}
			}
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
		TxtAddress.Text = "";
		TxtSuppIDNum.Text = "";
		TxtParentName.Text = "";
		TxtDiscount.Text = "";
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtSuppliers.Text = "عميل نقدى";
		TxtPhone.Text = "";
		TxtNote.Text = "";
		TxtTotalBeforDisc.Text = "0.00";
		TxtVatBeforeDisc.Text = "0.00";
		TxtTafqit.Text = "";
		TxtTotalAll.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtNet22.Text = "0.00";
		TxtLocation.Text = "";
		TxtDate.DateTime = DateTime.Now;
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
		TxtDiscount.Enabled = true;
		TxtParentName.Enabled = true;
		TxtDate.Enabled = true;
		TxtDeposit.Text = "0.00";
		TxtDateRentNum.Text = "";
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
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["الكمية"] = 0;
		dataRow["سعر الوحدة"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
		dataRow["الضربية"] = 0.0;
		dataRow["إجمالى المبلغ"] = 0.0;
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		IsEnableTrue();
		TxtSuppliers.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 112))
		{
			int num = 0;
			if (datatable.Rows.Count >= 1)
			{
				if (TxtSuppliers.Text != "" && TxtCodeRent.Text != "")
				{
					bool flag = true;
					if (IsItemPriceZero() == 0)
					{
						return;
					}
					Cal_FinalResultTotals();
					if (PermissionSellPrice() == 0)
					{
						return;
					}
					if (CBCostCenter.SelectedValue == null)
					{
						CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
					}
					FiterDatatableItemsUpdate();
					num = ORD.InsertPurch(out var Code, Convert.ToInt32(TxtCodeRent.Text), Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtSuppIDNum.Text, TxtDate.DateTime, TxtDateRent.DateTime, Convert.ToInt32(TxtDateRentNum.Text), TxtParentName.Text, Convert.ToInt32(CBCostCenter.SelectedValue), TxtNote.Text, TxtAddress.Text, Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDeposit.Text), DecimalNum));
					ORD.InsertItems(datatable, num);
					ORD.InsertTran(out var TRNIDDS, Convert.ToInt32(LblSup.Text), 2, TxtSuppliers.Text, "", TxtDate.DateTime);
					ORD.InsertTranDetailsAjila(TRNIDDS, Code, Convert.ToInt32(LBlSuppAccount.Text), TxtSuppliers.Text, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtTotalBeforDisc.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtAllVat.Text), Convert.ToDecimal(TxtNet.Text), Convert.ToInt32(CBCostCenter.SelectedValue));
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
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات فاتورة التأجير رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else
				{
					MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة التأجير بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtCodeRent.Focus();
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 112))
		{
			if (TxtDate.ReadOnly)
			{
				IsEnableTrue();
				BtnEdit.Text = "F5 حفظ";
				insertnewrow();
				gridView1.OptionsBehavior.Editable = true;
				return;
			}
			int purchese_ID = 0;
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
					if (datatable.Rows.Count < 1 || IsItemPriceZero() == 0)
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
					FiterDatatableItemsUpdate();
					if (CBCostCenter.SelectedValue == null)
					{
						CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
					}
					FilterDataTableSave(datatable, purchese_ID);
					ORD.DeleteTranDetails(Convert.ToInt32(TxtCode.Text));
					ORD.deleteItemsByPurcahesId(Convert.ToInt32(TxtCode.Text));
					purchese_ID = ORD.UpdatePurch(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtSuppIDNum.Text, TxtDate.DateTime, TxtParentName.Text, Convert.ToInt32(CBCostCenter.SelectedValue), TxtNote.Text, TxtAddress.Text, Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDeposit.Text), DecimalNum));
					ORD.InsertItems(datatable, purchese_ID);
					ORD.UpdateTran(out var TRNIDDS, Convert.ToInt32(TxtCode.Text), 2, TxtSuppliers.Text, "", TxtDate.DateTime);
					ORD.UpdateTranDetailsAjila(TRNIDDS, Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LBlSuppAccount.Text), TxtSuppliers.Text, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtTotalBeforDisc.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtAllVat.Text), Convert.ToDecimal(TxtNet.Text), Convert.ToInt32(CBCostCenter.SelectedValue));
					if (purchese_ID > 0)
					{
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						IsEnableFalse();
						BtnEdit.Text = "تعديل F6";
						gridView1.OptionsBehavior.Editable = false;
						switch (MessageBox.Show("تم تعديل فاتورة التأجير بنجاح \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
						{
						case DialogResult.Yes:
							Print();
							break;
						}
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات فاتورة التأجير رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else
				{
					MessageBox.Show("عفوا\u064b لا يمكن التعديل على فاتورة التأجير ليها حركات مرتجعات او صرف ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 112))
		{
			int num = 0;
			if (TxtCode.Text != "")
			{
				int PurId = Convert.ToInt32(TxtCode.Text);
				Order_OrderReturn order_OrderReturn = db.Order_OrderReturn.Where((Order_OrderReturn x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
				Order_CheckoutOrders order_CheckoutOrders = db.Order_CheckoutOrders.Where((Order_CheckoutOrders x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
				if (order_OrderReturn == null && order_CheckoutOrders == null)
				{
					switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة التأجير رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						num = ORD.Delete(Convert.ToInt32(TxtCode.Text));
						if (num > 0)
						{
							MessageBox.Show("تم حذف بيانات فاتورة التأجير رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
							MessageBox.Show("لا يمكن حذف بيانات فاتورة التأجير رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						}
						break;
					}
				}
				else
				{
					MessageBox.Show("عفوا\u064b لا يمكن التعديل على فاتورة التأجير ليها حركات مرتجعات او صرف ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 112))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_Rent_Recipt", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
				sqlConnection.Close();
				if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintRentRecipt.mrt");
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
		TxtParentName.ReadOnly = false;
		TxtDate.ReadOnly = false;
		TxtSuppliers.ReadOnly = false;
		TxtPhone.ReadOnly = false;
		CBCostCenter.Enabled = true;
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = false;
		TxtParentName.ReadOnly = true;
		TxtDate.ReadOnly = true;
		TxtSuppliers.ReadOnly = true;
		TxtPhone.ReadOnly = true;
		CBCostCenter.Enabled = false;
		TxtDiscount.BackColor = Color.White;
		TxtParentName.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
		TxtSuppliers.BackColor = Color.White;
		TxtPhone.BackColor = Color.White;
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
					if (get_Item_ByCodeALL_Result == null)
					{
						continue;
					}
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = Convert.ToInt32(get_Item_ByCodeALL_Result.ItemId);
					dataRow["ItemUnitID"] = Convert.ToInt32(get_Item_ByCodeALL_Result.UnitSmallID);
					dataRow["كود_الصنف"] = get_Item_ByCodeALL_Result.Item_code;
					dataRow["أسم الصنف"] = get_Item_ByCodeALL_Result.item_Name;
					dataRow["الوحدة"] = get_Item_ByCodeALL_Result.UnitSmall;
					dataRow["الكمية"] = 1;
					dataRow["المخزن"] = ORD.GetStoreName();
					dataRow["متوسط_التكلفة"] = get_Item_ByCodeALL_Result.Average_cost;
					dataRow["اجمالي_متوسط_التكلفة"] = Math.Round(Convert.ToDecimal(dataRow["متوسط_التكلفة"]) * Convert.ToDecimal(dataRow["الكمية"]), DecimalNum);
					dataRow["رقم المخزن"] = ORD.GetStoreID();
					dataRow["أسم_الوحدة"] = "الصغرى";
					dataRow["سعر الوحدة"] = get_Item_ByCodeALL_Result.SellPriceSmall;
					dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1)), DecimalNum);
					if (ORD.IsVatActive())
					{
						if (get_Item_ByCodeALL_Result.Is_Tax == true)
						{
							dataRow["الضربية"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1) * get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100), DecimalNum);
							dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1) * get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
							dataRow["هل_ضريبة"] = 1;
							dataRow["قيمة_الضريبة"] = get_Item_ByCodeALL_Result.Tax_Value;
						}
						else
						{
							dataRow["هل_ضريبة"] = 0;
							dataRow["قيمة_الضريبة"] = 0;
							dataRow["الضربية"] = 0.0;
							dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
						}
					}
					else
					{
						dataRow["هل_ضريبة"] = 0;
						dataRow["قيمة_الضريبة"] = 0;
						dataRow["الضربية"] = 0.0;
						dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
					}
					object obj = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
					int num = Convert.ToInt32(obj.ToString());
					dataRow["ظ…"] = num + 1;
					datatable.Rows.Add(dataRow);
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

	private void GroupPermission()
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
					MessageBox.Show("عفوا\u064b غير مصرح لك بخصم اكثر من السعر المحدد لك صلاحيته", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
					MessageBox.Show("عفوا\u064b غير مصرح لك بخصم اكثر من السعر المحدد لك صلاحيته", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
					MessageBox.Show("عفوا\u064b غير مصرح لك بخصم اكثر من السعر المحدد لك صلاحيته", "صلاحيات التأجير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
	}

	public void FilterDataTableSave(DataTable ItemOrder, int Purchese_ID)
	{
		int num = 1;
		dtCurrentTable.Clear();
		foreach (DataRow row in ItemOrder.Rows)
		{
			if (row["كود_الصنف"].ToString() != "")
			{
				dtCurrentTable.Rows.Add(num, Purchese_ID, row["ItemNote"].ToString(), Convert.ToInt32(row["ItemID"]), LoginDetails.BranchID, 0, Convert.ToInt32(row["رقم المخزن"]), Convert.ToString(row["ItemUnitID"]), Convert.ToDecimal(row["الكمية"]), Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"]), DecimalNum), Convert.ToDecimal(row["سعر الوحدة"]), Math.Round(Convert.ToDecimal(row["سعر الوحدة"]), DecimalNum), Math.Round(Convert.ToDecimal(row["الاجمالى"]), DecimalNum), Math.Round(Convert.ToDecimal(row["الضربية"]), DecimalNum), Math.Round(Convert.ToDecimal(row["سعربعدالخصم"]), DecimalNum), Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"]), DecimalNum), Math.Round(Convert.ToDecimal(row["الضريبه_بعد_الخصم"]), DecimalNum), row["أسم_الوحدة"].ToString(), 1, 1);
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
							if (row2["ظ…"].ToString() == Convert.ToString(num3))
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
							if (row3["ظ…"].ToString() == Convert.ToString(num6))
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

	private void repositoryItemButtonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string Itemocde = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "كود_الصنف"));
		if (Itemocde != "")
		{
			Item_Items item_Items = (from x in ITM.GetItemByCode()
				where x.Item_code == Itemocde
				select x).FirstOrDefault();
			LoginDetails.ItemcodeForStore = item_Items.ItemId;
			FrmSearchTransfer frmSearchTransfer = new FrmSearchTransfer();
			frmSearchTransfer.ShowDialog();
			if (frmSearchTransfer.ItemIdStore <= 0)
			{
				int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				{
					foreach (DataRow row in datatable.Rows)
					{
						if (row["ظ…"].ToString() == Convert.ToString(num))
						{
							row["المخزن"] = ORD.GetStoreName();
							row["رقم المخزن"] = 1;
						}
					}
					return;
				}
			}
			int StoreID = LoginDetails.StoreID;
			if (StoreID == 0)
			{
				StoreID = 1;
			}
			int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
			string value = (from x in db.Account_Stores
				where x.ID == StoreID
				select x.Store_Name).FirstOrDefault();
			foreach (DataRow row2 in datatable.Rows)
			{
				if (row2["ظ…"].ToString() == Convert.ToString(num2))
				{
					row2["المخزن"] = value;
					row2["رقم المخزن"] = StoreID;
				}
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[5];
			gridView1.ShowEditor();
			GetTotalCost();
			frmSearchTransfer.Close();
		}
		else
		{
			MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف المخازن", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void repositoryItemButtonEditCodeSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchItems();
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		try
		{
			base.KeyPreview = true;
			gridControl1.ForceInitialize();
			gridControl1.MainView.RestoreLayoutFromXml(fileName);
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

	private void ControlTxtSuppliers_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtSuppliers.Focus();
		}
	}

	private void ControlTxtNumVat_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
		}
	}

	private void TxtNet_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (LoginDetails.Lang == "ar-EG")
			{
				ToWord toWord = new ToWord(Convert.ToDecimal(TxtNet.Text), currencies[tafqitID]);
				txtWordAr = toWord.ConvertToArabic();
				TxtTafqit.Text = txtWordAr;
			}
			else
			{
				ToWord toWord2 = new ToWord(Convert.ToDecimal(TxtNet.Text), currencies[tafqitID]);
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
		LBlSuppAccount.Text = Cust.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
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
		gridControl1.MainView.SaveLayoutToXml(fileName);
	}

	private void MenuPurchOrder_Click(object sender, EventArgs e)
	{
	}

	private void TxtDate_DateTimeChanged(object sender, EventArgs e)
	{
		TimeSpan timeSpan = TxtDate.DateTime - TxtDateRent.DateTime;
		int num = timeSpan.Days;
		if (timeSpan.Hours >= 1)
		{
			num++;
		}
		TxtDateRentNum.Text = num.ToString();
	}

	private void BtnMoney_Click(object sender, EventArgs e)
	{
		FrmReceipts frmReceipts = new FrmReceipts();
		frmReceipts.ShowDialog();
	}

	private void TxtDateRent_EditValueChanged(object sender, EventArgs e)
	{
		TimeSpan timeSpan = TxtDate.DateTime - TxtDateRent.DateTime;
		int num = timeSpan.Days;
		if (timeSpan.Hours >= 1)
		{
			num++;
		}
		TxtDateRentNum.Text = num.ToString();
	}

	private void TxtDateRent_TextChanged(object sender, EventArgs e)
	{
		TimeSpan timeSpan = TxtDate.DateTime - TxtDateRent.DateTime;
		int num = timeSpan.Days;
		if (timeSpan.Hours >= 1)
		{
			num++;
		}
		TxtDateRentNum.Text = num.ToString();
		int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			row["عدد الايام"] = num;
			string code = row["كود_الصنف"].ToString();
			Item_Items itemsCode = ORD.GetItemsCode2(code);
			row["اجمالي_متوسط_التكلفة"] = Convert.ToDecimal(row["متوسط_التكلفة"]) * Convert.ToDecimal(row["الكمية"]) * (decimal)num;
			if (ORD.IsVatActive())
			{
				if (itemsCode.Is_Tax == true)
				{
					decimal value = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]) * (decimal)num;
					decimal? tax_Value = itemsCode.Tax_Value;
					row["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value * tax_Value / (decimal?)100), DecimalNum);
					string text = row["الضربية"].ToString();
					row["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]) * (decimal)num), DecimalNum);
					decimal value2 = Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]) * (decimal)num;
					decimal? tax_Value2 = itemsCode.Tax_Value;
					row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(row["سعر الوحدة"]) * Convert.ToDecimal(row["الكمية"]) * (decimal)num)), DecimalNum);
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
		}
		ArrangeDatatable();
		GetTotalCost();
		Cal_FinalResultTotals();
		gridControl1.DataSource = datatable;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.OrderRent.FrmRecipt));
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
		this.TxtLocation = new DevExpress.XtraEditors.TextEdit();
		this.label25 = new System.Windows.Forms.Label();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.RecoveredMenuItemFix1 = new System.Windows.Forms.ToolStripMenuItem();
		this.TxtNet22 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.RecoveredMenuItemFix2 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix3 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix4 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix5 = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix6 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.RecoveredMenuItemFix7 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatBeforeDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.RecoveredMenuItemFix8 = new System.Windows.Forms.ToolStripMenuItem();
		this.label26 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtParentName = new DevExpress.XtraEditors.TextEdit();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label31 = new System.Windows.Forms.Label();
		this.TxtDateRentNum = new DevExpress.XtraEditors.TextEdit();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtCodeRent = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtDateRent = new DevExpress.XtraEditors.DateEdit();
		this.label24 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtSuppIDNum = new DevExpress.XtraEditors.TextEdit();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtAddress = new DevExpress.XtraEditors.TextEdit();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.RecoveredMenuItemFix9 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPurchOrder = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix10 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuNewItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPreviewFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintQued = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix11 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.label6 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnMoney = new DevExpress.XtraEditors.SimpleButton();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtDeposit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTabocc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label29 = new System.Windows.Forms.Label();
		this.TxtAllVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label28 = new System.Windows.Forms.Label();
		this.TxtAllTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label27 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtParentName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRentNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeRent.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRent.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRent.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppIDNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.TxtLocation.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtLocation.EditValue = "";
		this.TxtLocation.Location = new System.Drawing.Point(494, 158);
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
		this.label25.Location = new System.Drawing.Point(721, 162);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(51, 19);
		this.label25.TabIndex = 191;
		this.label25.Text = "الموقع";
		this.txtdicoutprege.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Black;
		this.txtdicoutprege.Location = new System.Drawing.Point(6, 90);
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtdicoutprege.Size = new System.Drawing.Size(107, 27);
		this.txtdicoutprege.TabIndex = 189;
		this.txtdicoutprege.Text = "0.000";
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.txtdicoutprege.TextChanged += new System.EventHandler(TxtDiscountPrecent_TextChanged);
		this.txtdicoutprege.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtTafqit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtTafqit.Location = new System.Drawing.Point(6, 158);
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
		this.BtnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(797, 97);
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
		this.BtnSave.Location = new System.Drawing.Point(930, 97);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(125, 40);
		this.BtnSave.TabIndex = 3;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.RecoveredMenuItemFix1.Name = "نوعالفاتورةToolStripMenuItem1";
		this.RecoveredMenuItemFix1.Size = new System.Drawing.Size(165, 26);
		this.RecoveredMenuItemFix1.Text = "نوع الفاتورة";
		this.TxtNet22.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNet22.DecimalDigits = 3;
		this.TxtNet22.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtNet22.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet22.Location = new System.Drawing.Point(797, 39);
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
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Visible = true;
		this.gridColumn4.VisibleIndex = 3;
		this.repositoryItemButtonEdit5.AutoHeight = false;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions1.Image");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEdit5.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
		this.RecoveredMenuItemFix2.Name = "اسمالموردToolStripMenuItem1";
		this.RecoveredMenuItemFix2.Size = new System.Drawing.Size(165, 26);
		this.RecoveredMenuItemFix2.Text = "اسم المورد";
		this.RecoveredMenuItemFix3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix4, this.RecoveredMenuItemFix5, this.RecoveredMenuItemFix6 });
		this.RecoveredMenuItemFix3.Name = "الكمياتToolStripMenuItem1";
		this.RecoveredMenuItemFix3.Size = new System.Drawing.Size(232, 24);
		this.RecoveredMenuItemFix3.Text = "الكميات";
		this.RecoveredMenuItemFix4.Name = "الكميةالحاليةToolStripMenuItem";
		this.RecoveredMenuItemFix4.Size = new System.Drawing.Size(251, 26);
		this.RecoveredMenuItemFix4.Text = "الكمية الحالية";
		this.RecoveredMenuItemFix5.Name = "كلالكمياتبالمستودعاتToolStripMenuItem";
		this.RecoveredMenuItemFix5.Size = new System.Drawing.Size(251, 26);
		this.RecoveredMenuItemFix5.Text = "كل الكميات بالمستودعات";
		this.RecoveredMenuItemFix6.Name = "كلالكمياتبالفروعToolStripMenuItem";
		this.RecoveredMenuItemFix6.Size = new System.Drawing.Size(251, 26);
		this.RecoveredMenuItemFix6.Text = "كل الكميات بالفروع";
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(229, 6);
		this.RecoveredMenuItemFix7.Name = "تخصيصToolStripMenuItem1";
		this.RecoveredMenuItemFix7.Size = new System.Drawing.Size(232, 24);
		this.RecoveredMenuItemFix7.Text = "تخصيص";
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(229, 6);
		this.MenuExit.Name = "MenuExit";
		this.MenuExit.Size = new System.Drawing.Size(232, 24);
		this.MenuExit.Text = "خروج";
		this.MenuExit.Click += new System.EventHandler(MenuExit_Click);
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Location = new System.Drawing.Point(12, 204);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1195, 283);
		this.groupBox3.TabIndex = 5;
		this.groupBox3.TabStop = false;
		this.gridControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridControl1.Location = new System.Drawing.Point(3, 18);
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[9] { this.repositoryItemButtonEdit4, this.repositoryItemButtonEdit6, this.repositoryItemTextEdit1, this.repositoryItemTextEdit2, this.repositoryItemDateEdit1, this.repositoryItemTextEdit3, this.repositoryItemPictureEdit1, this.repositoryItemButtonEdit5, this.repositoryItemButtonEditCodeSearch });
		this.gridControl1.Size = new System.Drawing.Size(1189, 262);
		this.gridControl1.TabIndex = 0;
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 20f);
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[20]
		{
			this.gridColumn1, this.gridColumn2, this.gridColumn27, this.shearch, this.gridColumn19, this.gridColumn4, this.gridColumn23, this.gridColumn24, this.gridColumn20, this.gridColumn28,
			this.gridColumn25, this.gridColumn26, this.gridColumn21, this.gridColumn3, this.gridColumn6, this.gridColumn7, this.gridColumn8, this.gridColumn22, this.gridColumn5, this.gridColumn9
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
		this.gridColumn1.Visible = true;
		this.gridColumn1.VisibleIndex = 0;
		this.gridColumn1.Width = 149;
		this.repositoryItemButtonEditCodeSearch.AutoHeight = false;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions2.Image");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEditCodeSearch.Name = "repositoryItemButtonEditCodeSearch";
		this.repositoryItemButtonEditCodeSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEditCodeSearch_ButtonClick);
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
		this.gridColumn27.Width = 200;
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
		this.shearch.OptionsColumn.ReadOnly = true;
		this.shearch.Visible = true;
		this.shearch.VisibleIndex = 2;
		this.shearch.Width = 61;
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.repositoryItemButtonEdit4.AutoHeight = false;
		editorButtonImageOptions3.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		editorButtonImageOptions3.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions3.Image");
		editorButtonImageOptions3.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemButtonEdit4.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
		this.gridColumn19.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Bold);
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.Caption = "أسم_الوحدة";
		this.gridColumn19.FieldName = "أسم_الوحدة";
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		this.gridColumn23.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Bold);
		this.gridColumn23.AppearanceCell.Options.UseFont = true;
		this.gridColumn23.Caption = "متوسط_التكلفة";
		this.gridColumn23.FieldName = "متوسط_التكلفة";
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn24.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Bold);
		this.gridColumn24.AppearanceCell.Options.UseFont = true;
		this.gridColumn24.Caption = "اجمالي_متوسط_التكلفة";
		this.gridColumn24.FieldName = "اجمالي_متوسط_التكلفة";
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn20.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn20.AppearanceCell.Options.UseFont = true;
		this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn20.AppearanceHeader.Options.UseFont = true;
		this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.Caption = "الكمية";
		this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.FieldName = "الكمية";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn20.Visible = true;
		this.gridColumn20.VisibleIndex = 4;
		this.gridColumn20.Width = 80;
		this.gridColumn28.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn28.AppearanceCell.Options.UseFont = true;
		this.gridColumn28.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn28.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn28.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn28.AppearanceHeader.Options.UseFont = true;
		this.gridColumn28.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn28.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn28.Caption = "عدد الايام";
		this.gridColumn28.FieldName = "عدد الايام";
		this.gridColumn28.MinWidth = 25;
		this.gridColumn28.Name = "gridColumn28";
		this.gridColumn28.OptionsColumn.AllowEdit = false;
		this.gridColumn28.Visible = true;
		this.gridColumn28.VisibleIndex = 5;
		this.gridColumn28.Width = 94;
		this.gridColumn25.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.Caption = "وحدة التفقيط";
		this.gridColumn25.FieldName = "وحدة التفقيط";
		this.gridColumn25.MinWidth = 25;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.Visible = true;
		this.gridColumn25.VisibleIndex = 6;
		this.gridColumn25.Width = 94;
		this.gridColumn26.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn26.AppearanceCell.Options.UseFont = true;
		this.gridColumn26.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn26.AppearanceHeader.Options.UseFont = true;
		this.gridColumn26.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.Caption = "كمية التفقيط";
		this.gridColumn26.FieldName = "كمية التفقيط";
		this.gridColumn26.MinWidth = 25;
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.Visible = true;
		this.gridColumn26.VisibleIndex = 7;
		this.gridColumn26.Width = 94;
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
		this.gridColumn21.VisibleIndex = 8;
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
		this.gridColumn3.FieldName = "الاجمالى";
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.OptionsColumn.AllowFocus = false;
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn3.Visible = true;
		this.gridColumn3.VisibleIndex = 9;
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
		this.gridColumn6.FieldName = "الضربية";
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.AllowFocus = false;
		this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn6.OptionsColumn.ReadOnly = true;
		this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn6.Visible = true;
		this.gridColumn6.VisibleIndex = 10;
		this.gridColumn6.Width = 80;
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
		this.gridColumn8.FieldName = "حذف";
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Visible = true;
		this.gridColumn8.VisibleIndex = 12;
		this.gridColumn8.Width = 27;
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit6.AutoHeight = false;
		editorButtonImageOptions4.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions4.Image");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "حذف", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
		this.gridColumn22.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Bold);
		this.gridColumn22.AppearanceCell.Options.UseFont = true;
		this.gridColumn22.Caption = "إجمالى_بعدالخصم";
		this.gridColumn22.FieldName = "إجمالى_بعدالخصم";
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Bold);
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.Caption = "الضريبه_بعد_الخصم";
		this.gridColumn5.FieldName = "الضريبه_بعد_الخصم";
		this.gridColumn5.Name = "gridColumn5";
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
		this.gridColumn9.Width = 40;
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
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(930, 145);
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
		this.label7.Location = new System.Drawing.Point(327, 98);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(119, 19);
		this.label7.TabIndex = 172;
		this.label7.Text = "المجموع بالخصم";
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(1063, 97);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(125, 40);
		this.BtnNew.TabIndex = 7;
		this.BtnNew.Text = "جديد F4";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label8.Location = new System.Drawing.Point(327, 130);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(109, 19);
		this.label8.TabIndex = 171;
		this.label8.Text = "الضريبة بالخصم";
		this.BtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(1063, 145);
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
		this.TxtVat.Location = new System.Drawing.Point(214, 57);
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
		this.TxtTotals.Location = new System.Drawing.Point(214, 22);
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
		this.BtnExit.Location = new System.Drawing.Point(797, 145);
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
		this.TxtTotalAll.Location = new System.Drawing.Point(6, 22);
		this.TxtTotalAll.Name = "TxtTotalAll";
		this.TxtTotalAll.ReadOnly = true;
		this.TxtTotalAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotalAll.Size = new System.Drawing.Size(107, 27);
		this.TxtTotalAll.TabIndex = 161;
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
		this.TxtTotalBeforDisc.Location = new System.Drawing.Point(214, 90);
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
		this.TxtNet.Location = new System.Drawing.Point(6, 124);
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNet.Size = new System.Drawing.Size(107, 27);
		this.TxtNet.TabIndex = 159;
		this.TxtNet.Text = "0.000";
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet.TextChanged += new System.EventHandler(TxtNet_TextChanged);
		this.TxtNet.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label1.Location = new System.Drawing.Point(119, 96);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(93, 19);
		this.label1.TabIndex = 163;
		this.label1.Text = "نسبة الخصم";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label2.Location = new System.Drawing.Point(119, 28);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(70, 19);
		this.label2.TabIndex = 164;
		this.label2.Text = "الاجمالي";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label3.Location = new System.Drawing.Point(119, 62);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(89, 19);
		this.label3.TabIndex = 165;
		this.label3.Text = "قيمة الخصم";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label4.Location = new System.Drawing.Point(119, 130);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(58, 19);
		this.label4.TabIndex = 166;
		this.label4.Text = "الصافي";
		this.TxtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscount.Location = new System.Drawing.Point(6, 57);
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDiscount.Size = new System.Drawing.Size(107, 27);
		this.TxtDiscount.TabIndex = 160;
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
		this.TxtVatBeforeDisc.Location = new System.Drawing.Point(214, 124);
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
		this.label5.Location = new System.Drawing.Point(327, 62);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(92, 19);
		this.label5.TabIndex = 167;
		this.label5.Text = "الضريبة VAT";
		this.RecoveredMenuItemFix8.Name = "رقمالفاتورةToolStripMenuItem1";
		this.RecoveredMenuItemFix8.Size = new System.Drawing.Size(165, 26);
		this.RecoveredMenuItemFix8.Text = "رقم الفاتورة";
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label26.Location = new System.Drawing.Point(229, 142);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(95, 21);
		this.label26.TabIndex = 127;
		this.label26.Text = "مركز التكلفة";
		this.CBCostCenter.Font = new System.Drawing.Font("Tahoma", 10f);
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Location = new System.Drawing.Point(6, 139);
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.Size = new System.Drawing.Size(220, 29);
		this.CBCostCenter.TabIndex = 8;
		this.CBCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		this.CBCostCenter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
		this.LblSup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblSup.AutoSize = true;
		this.LblSup.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblSup.Location = new System.Drawing.Point(1169, 65);
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
		this.TxtDate.DateTimeChanged += new System.EventHandler(TxtDate_DateTimeChanged);
		this.TxtDate.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit2.AutoHeight = false;
		editorButtonImageOptions5.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions5.Image");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "حذف", -1, true, true, false, editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit3.AutoHeight = false;
		editorButtonImageOptions6.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions6.Image");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
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
		this.TxtParentName.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtParentName.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label22.Location = new System.Drawing.Point(229, 103);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(84, 21);
		this.label22.TabIndex = 115;
		this.label22.Text = "رقم الجوال";
		this.TxtPhone.Location = new System.Drawing.Point(6, 100);
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.Size = new System.Drawing.Size(220, 28);
		this.TxtPhone.TabIndex = 5;
		this.TxtPhone.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label21.Location = new System.Drawing.Point(232, 25);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(53, 21);
		this.label21.TabIndex = 113;
		this.label21.Text = "التاريخ";
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label18.Location = new System.Drawing.Point(1074, 65);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(93, 21);
		this.label18.TabIndex = 107;
		this.label18.Text = "اسم العميل";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.label31);
		this.groupBox1.Controls.Add(this.TxtDateRentNum);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.TxtCodeRent);
		this.groupBox1.Controls.Add(this.TxtDateRent);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.TxtSuppIDNum);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.TxtAddress);
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
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.TxtSuppliers);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1195, 186);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "بيانات فاتورة التأجير";
		this.label31.AutoSize = true;
		this.label31.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label31.Location = new System.Drawing.Point(232, 65);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(76, 21);
		this.label31.TabIndex = 146;
		this.label31.Text = "عدد الايام";
		this.TxtDateRentNum.EditValue = "1";
		this.TxtDateRentNum.Location = new System.Drawing.Point(6, 62);
		this.TxtDateRentNum.Name = "TxtDateRentNum";
		this.TxtDateRentNum.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDateRentNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateRentNum.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDateRentNum.Properties.Appearance.Options.UseFont = true;
		this.TxtDateRentNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateRentNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateRentNum.Properties.ReadOnly = true;
		this.TxtDateRentNum.Size = new System.Drawing.Size(220, 28);
		this.TxtDateRentNum.TabIndex = 145;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label13.Location = new System.Drawing.Point(621, 25);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(82, 21);
		this.label13.TabIndex = 144;
		this.label13.Text = "رقم الايجار";
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
		this.TxtCodeRent.TabIndex = 143;
		this.TxtCodeRent.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCodeRent_ButtonClick);
		this.TxtDateRent.EditValue = null;
		this.TxtDateRent.Location = new System.Drawing.Point(335, 62);
		this.TxtDateRent.Name = "TxtDateRent";
		this.TxtDateRent.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateRent.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDateRent.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateRent.Properties.Appearance.Options.UseBackColor = true;
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
		this.TxtDateRent.TabIndex = 141;
		this.TxtDateRent.EditValueChanged += new System.EventHandler(TxtDateRent_EditValueChanged);
		this.TxtDateRent.TextChanged += new System.EventHandler(TxtDateRent_TextChanged);
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label24.Location = new System.Drawing.Point(621, 65);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(90, 21);
		this.label24.TabIndex = 142;
		this.label24.Text = "تاريخ الايجار";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label11.Location = new System.Drawing.Point(1074, 141);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(73, 21);
		this.label11.TabIndex = 134;
		this.label11.Text = "ملاحظات";
		this.TxtNote.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNote.Location = new System.Drawing.Point(728, 139);
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNote.Size = new System.Drawing.Size(340, 28);
		this.TxtNote.TabIndex = 133;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label10.Location = new System.Drawing.Point(621, 104);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(81, 21);
		this.label10.TabIndex = 132;
		this.label10.Text = "رقم الهوية";
		this.TxtSuppIDNum.Location = new System.Drawing.Point(335, 101);
		this.TxtSuppIDNum.Name = "TxtSuppIDNum";
		this.TxtSuppIDNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSuppIDNum.Properties.Appearance.Options.UseFont = true;
		this.TxtSuppIDNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSuppIDNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSuppIDNum.Size = new System.Drawing.Size(280, 28);
		this.TxtSuppIDNum.TabIndex = 131;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label9.Location = new System.Drawing.Point(621, 142);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(56, 21);
		this.label9.TabIndex = 130;
		this.label9.Text = "العنوان";
		this.TxtAddress.Location = new System.Drawing.Point(335, 139);
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtAddress.Properties.Appearance.Options.UseFont = true;
		this.TxtAddress.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAddress.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAddress.Size = new System.Drawing.Size(280, 28);
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
		this.TxtSuppliers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCust_ButtonClick);
		this.TxtSuppliers.Enter += new System.EventHandler(textBox1_Enter);
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
		this.TxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.repositoryItemButtonEditCode.AutoHeight = false;
		editorButtonImageOptions8.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions8.Image");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
		this.RecoveredMenuItemFix9.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix8, this.RecoveredMenuItemFix2, this.RecoveredMenuItemFix1 });
		this.RecoveredMenuItemFix9.Name = "استعلامToolStripMenuItem1";
		this.RecoveredMenuItemFix9.Size = new System.Drawing.Size(232, 24);
		this.RecoveredMenuItemFix9.Text = "استعلام";
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		this.MenuPurchOrder.Size = new System.Drawing.Size(232, 24);
		this.MenuPurchOrder.Text = "تحميل من عرض سعر  F9";
		this.MenuPurchOrder.Click += new System.EventHandler(MenuPurchOrder_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(229, 6);
		this.MenuSearchItem.Name = "MenuSearchItem";
		this.MenuSearchItem.Size = new System.Drawing.Size(232, 24);
		this.MenuSearchItem.Text = "بحث عن صنف        F3";
		this.MenuSearchItem.Click += new System.EventHandler(MenuSearchItem_Click);
		this.RecoveredMenuItemFix10.Name = "حركةصنفToolStripMenuItem";
		this.RecoveredMenuItemFix10.Size = new System.Drawing.Size(232, 24);
		this.RecoveredMenuItemFix10.Text = "حركة صنف";
		this.MenuNewItem.Name = "MenuNewItem";
		this.MenuNewItem.Size = new System.Drawing.Size(232, 24);
		this.MenuNewItem.Text = "صنف جديد            F12 ";
		this.MenuNewItem.Click += new System.EventHandler(MenuNewItem_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
		this.MenuPrintFatoura.Name = "MenuPrintFatoura";
		this.MenuPrintFatoura.Size = new System.Drawing.Size(259, 26);
		this.MenuPrintFatoura.Text = "طباعة الفاتورة       Ctrl + P";
		this.MenuPrintFatoura.Click += new System.EventHandler(MenuPrintFatoura_Click);
		this.MenuPreviewFatoura.Name = "MenuPreviewFatoura";
		this.MenuPreviewFatoura.Size = new System.Drawing.Size(259, 26);
		this.MenuPreviewFatoura.Text = "معاينة الفاتورة";
		this.MenuPreviewFatoura.Click += new System.EventHandler(MenuPreviewFatoura_Click);
		this.MenuPrintQued.Name = "MenuPrintQued";
		this.MenuPrintQued.Size = new System.Drawing.Size(259, 26);
		this.MenuPrintQued.Text = "معاينة القيد";
		this.MenuPrintQued.Click += new System.EventHandler(MenuPrintQued_Click);
		this.RecoveredMenuItemFix11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.MenuPrintQued, this.MenuPreviewFatoura, this.MenuPrintFatoura });
		this.RecoveredMenuItemFix11.Name = "طباعةToolStripMenuItem1";
		this.RecoveredMenuItemFix11.Size = new System.Drawing.Size(232, 24);
		this.RecoveredMenuItemFix11.Text = "طباعة ";
		this.MenuDelete.Name = "MenuDelete";
		this.MenuDelete.Size = new System.Drawing.Size(232, 24);
		this.MenuDelete.Text = "حذف                       F7";
		this.MenuDelete.Click += new System.EventHandler(MenuDelete_Click);
		this.MenuEdit.Name = "MenuEdit";
		this.MenuEdit.Size = new System.Drawing.Size(232, 24);
		this.MenuEdit.Text = "تعديل                      F6";
		this.MenuEdit.Click += new System.EventHandler(MenuEdit_Click);
		this.MenuSave.Name = "MenuSave";
		this.MenuSave.Size = new System.Drawing.Size(232, 24);
		this.MenuSave.Text = "حفظ                       F5";
		this.MenuSave.Click += new System.EventHandler(MenuSave_Click);
		this.MenuNew.Name = "MenuNew";
		this.MenuNew.Size = new System.Drawing.Size(232, 24);
		this.MenuNew.Text = "جديد                       F4";
		this.MenuNew.Click += new System.EventHandler(MenuNew_Click);
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.RecoveredMenuItemFix11, this.toolStripSeparator1, this.MenuNewItem, this.RecoveredMenuItemFix10, this.MenuSearchItem, this.toolStripSeparator4,
			this.MenuPurchOrder, this.RecoveredMenuItemFix9, this.RecoveredMenuItemFix3, this.toolStripSeparator2, this.RecoveredMenuItemFix7, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.contextMenuStrip1.Size = new System.Drawing.Size(233, 340);
		this.ribbonPage2.Name = "ribbonPage2";
		this.ribbonPage2.Text = "ribbonPage2";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label6.Location = new System.Drawing.Point(327, 27);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(107, 19);
		this.label6.TabIndex = 168;
		this.label6.Text = "المجموع Total";
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.BtnMoney);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.TxtDeposit);
		this.groupBox2.Controls.Add(this.TxtCost);
		this.groupBox2.Controls.Add(this.TxtTabocc);
		this.groupBox2.Controls.Add(this.label29);
		this.groupBox2.Controls.Add(this.TxtAllVat);
		this.groupBox2.Controls.Add(this.label28);
		this.groupBox2.Controls.Add(this.TxtAllTotal);
		this.groupBox2.Controls.Add(this.label27);
		this.groupBox2.Controls.Add(this.TxtLocation);
		this.groupBox2.Controls.Add(this.label25);
		this.groupBox2.Controls.Add(this.txtdicoutprege);
		this.groupBox2.Controls.Add(this.label30);
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
		this.groupBox2.Location = new System.Drawing.Point(12, 493);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1195, 192);
		this.groupBox2.TabIndex = 4;
		this.groupBox2.TabStop = false;
		this.BtnMoney.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnMoney.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnMoney.Appearance.Options.UseFont = true;
		this.BtnMoney.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnMoney.ImageOptions.Image");
		this.BtnMoney.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnMoney.Location = new System.Drawing.Point(1063, 39);
		this.BtnMoney.Margin = new System.Windows.Forms.Padding(4);
		this.BtnMoney.Name = "BtnMoney";
		this.BtnMoney.Size = new System.Drawing.Size(125, 48);
		this.BtnMoney.TabIndex = 212;
		this.BtnMoney.Text = "سند قبض";
		this.BtnMoney.Click += new System.EventHandler(BtnMoney_Click);
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label12.Location = new System.Drawing.Point(532, 82);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(146, 19);
		this.label12.TabIndex = 211;
		this.label12.Text = "المبلغ المدفوع تأمين";
		this.TxtDeposit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDeposit.DecimalDigits = 2;
		this.TxtDeposit.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtDeposit.ForeColor = System.Drawing.Color.Blue;
		this.TxtDeposit.Location = new System.Drawing.Point(494, 104);
		this.TxtDeposit.Name = "TxtDeposit";
		this.TxtDeposit.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDeposit.Size = new System.Drawing.Size(221, 48);
		this.TxtDeposit.TabIndex = 210;
		this.TxtDeposit.Text = "0.00";
		this.TxtDeposit.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDeposit.UseCompatibleTextRendering = false;
		this.TxtDeposit.Value = new decimal(new int[4]);
		this.TxtDeposit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtCost.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtCost.DecimalDigits = 3;
		this.TxtCost.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtCost.ForeColor = System.Drawing.Color.Black;
		this.TxtCost.Location = new System.Drawing.Point(996, 7);
		this.TxtCost.Name = "TxtCost";
		this.TxtCost.ReadOnly = true;
		this.TxtCost.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtCost.Size = new System.Drawing.Size(107, 27);
		this.TxtCost.TabIndex = 197;
		this.TxtCost.Text = "0.000";
		this.TxtCost.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCost.Value = new decimal(new int[4]);
		this.TxtCost.Visible = false;
		this.TxtCost.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTabocc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTabocc.DecimalDigits = 3;
		this.TxtTabocc.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTabocc.ForeColor = System.Drawing.Color.Black;
		this.TxtTabocc.Location = new System.Drawing.Point(335, 0);
		this.TxtTabocc.Name = "TxtTabocc";
		this.TxtTabocc.ReadOnly = true;
		this.TxtTabocc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTabocc.Size = new System.Drawing.Size(107, 27);
		this.TxtTabocc.TabIndex = 206;
		this.TxtTabocc.Text = "0.000";
		this.TxtTabocc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTabocc.Value = new decimal(new int[4]);
		this.TxtTabocc.Visible = false;
		this.TxtTabocc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label29.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label29.AutoSize = true;
		this.label29.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label29.Location = new System.Drawing.Point(448, 5);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(139, 19);
		this.label29.TabIndex = 205;
		this.label29.Text = "الضريبة التبغ 100%";
		this.label29.Visible = false;
		this.TxtAllVat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtAllVat.DecimalDigits = 3;
		this.TxtAllVat.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtAllVat.ForeColor = System.Drawing.Color.Black;
		this.TxtAllVat.Location = new System.Drawing.Point(587, 8);
		this.TxtAllVat.Name = "TxtAllVat";
		this.TxtAllVat.ReadOnly = true;
		this.TxtAllVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtAllVat.Size = new System.Drawing.Size(107, 27);
		this.TxtAllVat.TabIndex = 208;
		this.TxtAllVat.Text = "0.000";
		this.TxtAllVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllVat.Value = new decimal(new int[4]);
		this.TxtAllVat.Visible = false;
		this.TxtAllVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label28.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label28.AutoSize = true;
		this.label28.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label28.Location = new System.Drawing.Point(700, 13);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(113, 19);
		this.label28.TabIndex = 207;
		this.label28.Text = "الضريبة المضافة";
		this.label28.Visible = false;
		this.TxtAllTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtAllTotal.DecimalDigits = 3;
		this.TxtAllTotal.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtAllTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtAllTotal.Location = new System.Drawing.Point(815, 7);
		this.TxtAllTotal.Name = "TxtAllTotal";
		this.TxtAllTotal.ReadOnly = true;
		this.TxtAllTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtAllTotal.Size = new System.Drawing.Size(107, 28);
		this.TxtAllTotal.TabIndex = 209;
		this.TxtAllTotal.Text = "0.000";
		this.TxtAllTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllTotal.Value = new decimal(new int[4]);
		this.TxtAllTotal.Visible = false;
		this.TxtAllTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label27.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label27.Location = new System.Drawing.Point(1109, 13);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(97, 19);
		this.label27.TabIndex = 196;
		this.label27.Text = "تكلفة الفاتورة";
		this.label27.Visible = false;
		this.label30.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label30.AutoSize = true;
		this.label30.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label30.Location = new System.Drawing.Point(928, 15);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(66, 19);
		this.label30.TabIndex = 204;
		this.label30.Text = "المجموع";
		this.label30.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1219, 697);
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmRecipt";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "فاتورة استلام المعدات";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmOrders_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtParentName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRentNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeRent.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRent.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRent.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppIDNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}


