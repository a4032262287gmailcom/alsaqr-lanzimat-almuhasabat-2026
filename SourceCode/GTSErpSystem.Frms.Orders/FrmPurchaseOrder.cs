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
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Frms.Reports.Stores;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Orders;

public class FrmPurchaseOrder : XtraForm
{
	private Class_PurchaseOrder Pur = new Class_PurchaseOrder();

	private ClassProject Project = new ClassProject();

	private Class_Suppliers SPR = new Class_Suppliers();

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

	private static int IsShow;

	private static int PrintID;

	private int intType = 1;

	private string ItmName = "";

	private int ItmID = 0;

	public bool ISEVENTDISC = true;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	private string fileNameAR = Application.StartupPath + "\\Layout\\PurchaseOrderAR.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\PurchaseOrderEN.xml";

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

	private Label label15;

	private NumericEditBox TxtAllQuantity;

	private NumericEditBox TxtLastCost;

	private SimpleButton BtnEdit;

	private Label label10;

	private NumericEditBox TxtAvergCost;

	private SimpleButton BtnSave;

	private ToolStripMenuItem نوعالفاتورةToolStripMenuItem1;

	private NumericEditBox TxtNet;

	private GridColumn gridColumn4;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private ToolStripMenuItem اسمالموردToolStripMenuItem1;

	private ToolStripMenuItem الكمياتToolStripMenuItem1;

	private ToolStripMenuItem الكميةالحاليةToolStripMenuItem;

	private ToolStripMenuItem كلالكمياتبالمستودعاتToolStripMenuItem;

	private ToolStripMenuItem كلالكمياتبالفروعToolStripMenuItem;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem تخصيصToolStripMenuItem1;

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

	private NumericEditBox TxtProfit;

	private SimpleButton BtnNew;

	private Label label8;

	private SimpleButton BtnDelete;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtSellPrice3;

	private SimpleButton BtnExit;

	private NumericEditBox TxtTotal;

	private Label label11;

	private NumericEditBox TxtTotalDiscount;

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

	private ToolStripMenuItem رقمالفاتورةToolStripMenuItem1;

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

	private Label label22;

	private TextEdit TxtPhone;

	private Label label21;

	private Label label20;

	private System.Windows.Forms.ComboBox TxtType;

	private Label label19;

	private TextEdit TxtNumVat;

	private Label label18;

	private GroupBox groupBox1;

	private ButtonEdit TxtSuppliers;

	private Label label17;

	private ButtonEdit TxtCode;

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private ToolStripMenuItem استعلامToolStripMenuItem1;

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

	private GroupBox groupBox2;

	private Label label27;

	private System.Windows.Forms.ComboBox CBProject;

	private GridColumn gridColumn23;

	private NumericEditBox TxtAllDiscount;

	private Label label6;

	public FrmPurchaseOrder()
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
		SetInitialRow();
		gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
		base.KeyPreview = true;
		gridView1.Columns["الكمية"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["الكمية"].DisplayFormat.FormatString = "n" + DecimalNumQty;
		gridView1.Columns["سعر الوحدة"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["سعر الوحدة"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["الاجمالى"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["الاجمالى"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["الضربية"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["الضربية"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["إجمالى المبلغ"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["إجمالى المبلغ"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.FocusedRowHandle = 1;
		gridView1.FocusedColumn = gridView1.VisibleColumns[0];
		gridView1.ShowEditor();
		BindComboBox();
		BindCBCostCenter();
		GroupPermission();
		TxtSuppliers.Text = "مورد نقدى";
		TxtDate.Focus();
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		txtdicoutprege.Controls[0].Visible = false;
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 11);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 11);
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
		TxtPhone.Properties.MaxLength = 14;
		TxtNumVat.Properties.MaxLength = 15;
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "كود_الصنف"));
		string text2 = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "أسم الصنف"));
		decimal num = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "الكمية"));
		decimal num2 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "سعر الوحدة"));
		if (text == "" || text2 == "" || num <= 0m || num2 <= 0m)
		{
			Item_Items item_Items = Pur.GetItems(text).FirstOrDefault();
			if (item_Items != null)
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
			Item_Items item_Items = Pur.GetItems(text).FirstOrDefault();
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
							if (item_Items.item_Type == 2)
							{
								MessageBox.Show("عفوا ،لايمكن عمل فاتورة شراء لاصناف خدمية", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
								gridView1.FocusedRowHandle = e.RowHandle;
								gridView1.FocusedColumn = gridView1.VisibleColumns[-1];
								gridView1.ShowEditor();
							}
							else if (item_Items.item_Type == 3)
							{
								MessageBox.Show("عفوا ،لايمكن عمل فاتورة شراء لاصناف مكونة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
								gridView1.FocusedRowHandle = e.RowHandle;
								gridView1.FocusedColumn = gridView1.VisibleColumns[-1];
								gridView1.ShowEditor();
							}
							else
							{
								DataRow dataRow = null;
								dataRow = datatable.NewRow();
								dataRow["كود_الصنف"] = item_Items.Item_code;
								dataRow["أسم الصنف"] = item_Items.item_Name;
								dataRow["الوحدة"] = item_Items.Item_Unit.Name;
								dataRow["الكمية"] = FUNC_DecimalNumQty(1);
								dataRow["مسح"] = 0;
								dataRow["تم"] = 1;
								dataRow["أسم_الوحدة"] = "الصغرى";
								dataRow["سعر الوحدة"] = item_Items.LastCost;
								dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(1)), DecimalNum);
								dataRow["المخزن"] = Pur.GetStoreName();
								dataRow["رقم المخزن"] = Pur.GetStoreID();
								if (Pur.IsVatActive())
								{
									if (LoginDetails.ItemTotalWithVat)
									{
										if (item_Items.Is_Tax == true)
										{
											dataRow["سعر الوحدة"] = Math.Round(Convert.ToDecimal(item_Items.LastCost / (item_Items.Tax_Value / (decimal?)100 + (decimal?)1)), DecimalNum);
											dataRow["الاجمالى"] = dataRow["سعر الوحدة"];
											dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items.LastCost), DecimalNum);
											dataRow["الضربية"] = Math.Round(Convert.ToDecimal(dataRow["إجمالى المبلغ"]) - Convert.ToDecimal(dataRow["الاجمالى"]), DecimalNum);
											dataRow["هل_ضريبة"] = 1;
											dataRow["قيمة_الضريبة"] = item_Items.Tax_Value;
										}
										else
										{
											dataRow["الضربية"] = 0.0;
											dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(1))), DecimalNum);
											dataRow["هل_ضريبة"] = 0;
											dataRow["قيمة_الضريبة"] = 0;
										}
									}
									else if (item_Items.Is_Tax == true)
									{
										dataRow["الضربية"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(1) * item_Items.Tax_Value / (decimal?)100), DecimalNum);
										dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(1) * item_Items.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(1))), DecimalNum);
										dataRow["هل_ضريبة"] = 1;
										dataRow["قيمة_الضريبة"] = item_Items.Tax_Value;
									}
									else
									{
										dataRow["الضربية"] = 0.0;
										dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(1))), DecimalNum);
										dataRow["هل_ضريبة"] = 0;
										dataRow["قيمة_الضريبة"] = 0;
									}
								}
								else
								{
									dataRow["الضربية"] = 0.0;
									dataRow["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(1))), DecimalNum);
									dataRow["هل_ضريبة"] = 0;
									dataRow["قيمة_الضريبة"] = 0;
								}
								object rowCellValue2 = gridView1.GetRowCellValue(e.RowHandle, "م");
								string text3 = rowCellValue2.ToString();
								if (text3 == "")
								{
									dataRow["م"] = e.RowHandle + 1;
								}
								else
								{
									dataRow["م"] = text3;
								}
								CheckFfistRow();
								datatable.Rows.Add(dataRow);
								insertnewrow();
								GetAllitems_alternatives(text, dataRow["أسم_الوحدة"].ToString(), 1);
								gridView1.FocusedRowHandle = e.RowHandle;
								gridView1.FocusedColumn = gridView1.VisibleColumns[-1];
								gridView1.ShowEditor();
							}
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
							string value = Convert.ToString(e.Value);
							string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
							string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
							string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
							string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("م");
							if (!(row["م"].ToString() == focusedRowCellDisplayText4))
							{
								continue;
							}
							if (row["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row["كود_الصنف"] = item_Items.Item_code;
								row["أسم الصنف"] = item_Items.item_Name;
								row["سعر الوحدة"] = item_Items.LastCost;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(value)), DecimalNum);
								if (Pur.IsVatActive())
								{
									if (item_Items.Is_Tax == true)
									{
										row["الضربية"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(value) * item_Items.Tax_Value / (decimal?)100), DecimalNum);
										row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(value) * item_Items.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(value))), DecimalNum);
										row["هل_ضريبة"] = 1;
										row["قيمة_الضريبة"] = item_Items.Tax_Value;
									}
									else
									{
										row["الضربية"] = 0;
										row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(value))), DecimalNum);
										row["هل_ضريبة"] = 0;
										row["قيمة_الضريبة"] = 0;
									}
								}
								else
								{
									row["الضربية"] = 0;
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(value))), DecimalNum);
									row["هل_ضريبة"] = 0;
									row["قيمة_الضريبة"] = 0;
								}
								break;
							}
							if (row["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								row["كود_الصنف"] = item_Items.Item_code;
								row["أسم الصنف"] = item_Items.item_Name;
								row["سعر الوحدة"] = item_Items.LastCost2;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost2 * (decimal?)Convert.ToDecimal(value)), DecimalNum);
								if (Pur.IsVatActive())
								{
									if (item_Items.Is_Tax == true)
									{
										row["الضربية"] = Math.Round(Convert.ToDecimal(item_Items.LastCost2 * (decimal?)Convert.ToDecimal(value) * item_Items.Tax_Value / (decimal?)100), DecimalNum);
										row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items.LastCost2 * (decimal?)Convert.ToDecimal(value) * item_Items.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items.LastCost2 * (decimal?)Convert.ToDecimal(value))), DecimalNum);
										row["هل_ضريبة"] = 1;
										row["قيمة_الضريبة"] = item_Items.Tax_Value;
									}
									else
									{
										row["الضربية"] = 0;
										row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.LastCost2 * (decimal?)Convert.ToDecimal(value))), DecimalNum);
										row["هل_ضريبة"] = 0;
										row["قيمة_الضريبة"] = 0;
									}
								}
								else
								{
									row["الضربية"] = 0;
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.LastCost2 * (decimal?)Convert.ToDecimal(value))), DecimalNum);
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
							row["سعر الوحدة"] = item_Items.LastCost3;
							row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost3 * (decimal?)Convert.ToDecimal(value)), DecimalNum);
							if (Pur.IsVatActive())
							{
								if (item_Items.Is_Tax == true)
								{
									row["الضربية"] = Math.Round(Convert.ToDecimal(item_Items.LastCost3 * (decimal?)Convert.ToDecimal(value) * item_Items.Tax_Value / (decimal?)100), DecimalNum);
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items.LastCost3 * (decimal?)Convert.ToDecimal(value) * item_Items.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items.LastCost3 * (decimal?)Convert.ToDecimal(value))), DecimalNum);
									row["هل_ضريبة"] = 1;
									row["قيمة_الضريبة"] = item_Items.Tax_Value;
								}
								else
								{
									row["هل_ضريبة"] = 0;
									row["قيمة_الضريبة"] = 0;
									row["الضربية"] = 0;
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.LastCost3 * (decimal?)Convert.ToDecimal(value))), DecimalNum);
								}
							}
							else
							{
								row["هل_ضريبة"] = 0;
								row["قيمة_الضريبة"] = 0;
								row["الضربية"] = 0;
								row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.LastCost3 * (decimal?)Convert.ToDecimal(value))), DecimalNum);
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
			string value2 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText5 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText6 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText7 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText8 = gridView1.GetFocusedRowCellDisplayText("م");
			if (focusedRowCellDisplayText5 != "")
			{
				Item_Items item_Items2 = Pur.GetItems(focusedRowCellDisplayText5).FirstOrDefault();
				if (item_Items2 != null)
				{
					foreach (DataRow row2 in datatable.Rows)
					{
						if (!(row2["م"].ToString() == focusedRowCellDisplayText8))
						{
							continue;
						}
						decimal num2 = Convert.ToDecimal(row2["سعر الوحدة"].ToString());
						if (row2["أسم_الوحدة"].ToString() == "الصغرى")
						{
							row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value2)), DecimalNum);
							if (Pur.IsVatActive())
							{
								if (LoginDetails.ItemTotalWithVat)
								{
									if (item_Items2.Is_Tax == true)
									{
										row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(row2["الاجمالى"]) * Convert.ToDecimal(item_Items2.Tax_Value / (decimal?)100 + (decimal?)1), 1);
										row2["الضربية"] = Math.Round(Convert.ToDecimal(row2["إجمالى المبلغ"]) - Convert.ToDecimal(row2["الاجمالى"]), DecimalNum);
										row2["هل_ضريبة"] = 1;
										row2["قيمة_الضريبة"] = item_Items2.Tax_Value;
									}
									else
									{
										row2["الضربية"] = 0;
										row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value2))), DecimalNum);
										row2["هل_ضريبة"] = 0;
										row2["قيمة_الضريبة"] = 0;
									}
								}
								else if (item_Items2.Is_Tax == true)
								{
									decimal value3 = num2 * Convert.ToDecimal(value2);
									decimal? tax_Value = item_Items2.Tax_Value;
									row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value / (decimal?)100), DecimalNum);
									decimal value4 = num2 * Convert.ToDecimal(value2);
									decimal? tax_Value2 = item_Items2.Tax_Value;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value4 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num2 * Convert.ToDecimal(value2))), DecimalNum);
									row2["هل_ضريبة"] = 1;
									row2["قيمة_الضريبة"] = item_Items2.Tax_Value;
								}
								else
								{
									row2["الضربية"] = 0;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value2))), DecimalNum);
									row2["هل_ضريبة"] = 0;
									row2["قيمة_الضريبة"] = 0;
								}
							}
							else
							{
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value2))), DecimalNum);
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
							}
							break;
						}
						if (row2["أسم_الوحدة"].ToString() == "المتوسطة")
						{
							row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value2)), DecimalNum);
							if (Pur.IsVatActive())
							{
								if (item_Items2.Is_Tax == true)
								{
									decimal value3 = num2 * Convert.ToDecimal(value2);
									decimal? tax_Value = item_Items2.Tax_Value;
									row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value / (decimal?)100), DecimalNum);
									decimal value4 = num2 * Convert.ToDecimal(value2);
									decimal? tax_Value2 = item_Items2.Tax_Value;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value4 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num2 * Convert.ToDecimal(value2))), DecimalNum);
									row2["هل_ضريبة"] = 1;
									row2["قيمة_الضريبة"] = item_Items2.Tax_Value;
								}
								else
								{
									row2["هل_ضريبة"] = 0;
									row2["قيمة_الضريبة"] = 0;
									row2["الضربية"] = 0;
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value2))), DecimalNum);
								}
							}
							else
							{
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value2))), DecimalNum);
							}
							break;
						}
						if (!(row2["أسم_الوحدة"].ToString() == "الكبرى"))
						{
							continue;
						}
						row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value2)), DecimalNum);
						if (Pur.IsVatActive())
						{
							if (item_Items2.Is_Tax == true)
							{
								decimal value3 = num2 * Convert.ToDecimal(value2);
								decimal? tax_Value = item_Items2.Tax_Value;
								row2["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value / (decimal?)100), DecimalNum);
								decimal value4 = num2 * Convert.ToDecimal(value2);
								decimal? tax_Value2 = item_Items2.Tax_Value;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value4 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num2 * Convert.ToDecimal(value2))), DecimalNum);
								row2["هل_ضريبة"] = 1;
								row2["قيمة_الضريبة"] = item_Items2.Tax_Value;
							}
							else
							{
								row2["هل_ضريبة"] = 0;
								row2["قيمة_الضريبة"] = 0;
								row2["الضربية"] = 0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value2))), DecimalNum);
							}
						}
						else
						{
							row2["هل_ضريبة"] = 0;
							row2["قيمة_الضريبة"] = 0;
							row2["الضربية"] = 0;
							row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(value2))), DecimalNum);
						}
						break;
					}
					CheckFfistRow();
					insertnewrow();
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
			string value5 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText9 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText10 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText11 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText12 = gridView1.GetFocusedRowCellDisplayText("الكمية");
			string focusedRowCellDisplayText13 = gridView1.GetFocusedRowCellDisplayText("م");
			if (focusedRowCellDisplayText9 != "")
			{
				Item_Items item_Items3 = Pur.GetItems(focusedRowCellDisplayText9).FirstOrDefault();
				if (item_Items3 != null)
				{
					foreach (DataRow row3 in datatable.Rows)
					{
						if (!(row3["م"].ToString() == focusedRowCellDisplayText13))
						{
							continue;
						}
						if (row3["أسم_الوحدة"].ToString() == "الصغرى")
						{
							row3["سعر الوحدة"] = value5;
							row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12)), DecimalNum);
							if (Pur.IsVatActive())
							{
								if (item_Items3.Is_Tax == true)
								{
									decimal value3 = Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12);
									decimal? tax_Value = item_Items3.Tax_Value;
									row3["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value / (decimal?)100), DecimalNum);
									decimal value4 = Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12);
									decimal? tax_Value2 = item_Items3.Tax_Value;
									row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value4 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12))), DecimalNum);
									row3["هل_ضريبة"] = 1;
									row3["قيمة_الضريبة"] = item_Items3.Tax_Value;
								}
								else
								{
									row3["هل_ضريبة"] = 0;
									row3["قيمة_الضريبة"] = 0;
									row3["الضربية"] = 0;
									row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12))), DecimalNum);
								}
							}
							else
							{
								row3["هل_ضريبة"] = 0;
								row3["قيمة_الضريبة"] = 0;
								row3["الضربية"] = 0;
								row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12))), DecimalNum);
							}
							break;
						}
						if (row3["أسم_الوحدة"].ToString() == "المتوسطة")
						{
							row3["سعر الوحدة"] = value5;
							row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12)), DecimalNum);
							if (Pur.IsVatActive())
							{
								if (item_Items3.Is_Tax == true)
								{
									decimal value3 = Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12);
									decimal? tax_Value = item_Items3.Tax_Value;
									row3["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value / (decimal?)100), DecimalNum);
									decimal value4 = Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12);
									decimal? tax_Value2 = item_Items3.Tax_Value;
									row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value4 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12))), DecimalNum);
									row3["هل_ضريبة"] = 1;
									row3["قيمة_الضريبة"] = item_Items3.Tax_Value;
								}
								else
								{
									row3["هل_ضريبة"] = 0;
									row3["قيمة_الضريبة"] = 0;
									row3["الضربية"] = 0;
									row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12))), DecimalNum);
								}
							}
							else
							{
								row3["هل_ضريبة"] = 0;
								row3["قيمة_الضريبة"] = 0;
								row3["الضربية"] = 0;
								row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12))), DecimalNum);
							}
							break;
						}
						if (!(row3["أسم_الوحدة"].ToString() == "الكبرى"))
						{
							continue;
						}
						row3["سعر الوحدة"] = value5;
						row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12)), DecimalNum);
						if (Pur.IsVatActive())
						{
							if (item_Items3.Is_Tax == true)
							{
								row3["هل_ضريبة"] = 1;
								row3["قيمة_الضريبة"] = item_Items3.Tax_Value;
								decimal value3 = Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12);
								decimal? tax_Value = item_Items3.Tax_Value;
								row3["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value3 * tax_Value / (decimal?)100), DecimalNum);
								decimal value4 = Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12);
								decimal? tax_Value2 = item_Items3.Tax_Value;
								row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value4 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12))), DecimalNum);
							}
							else
							{
								row3["هل_ضريبة"] = 0;
								row3["قيمة_الضريبة"] = 0;
								row3["الضربية"] = 0;
								row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12))), DecimalNum);
							}
						}
						else
						{
							row3["هل_ضريبة"] = 0;
							row3["قيمة_الضريبة"] = 0;
							row3["الضربية"] = 0;
							row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(value5) * Convert.ToDecimal(focusedRowCellDisplayText12))), DecimalNum);
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
		try
		{
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
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string StoreName = gridView1.GetFocusedRowCellDisplayText("المخزن");
			int storeID = (from x in db.Account_Stores
				where x.Store_Name == StoreName
				select x.ID).FirstOrDefault();
			GetAllitems_alternatives(focusedRowCellDisplayText3, focusedRowCellDisplayText4, storeID);
			CodeItm = focusedRowCellDisplayText3;
		}
		catch
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
		try
		{
			FrmSearchPurchasesOrder frmSearchPurchasesOrder = new FrmSearchPurchasesOrder();
			frmSearchPurchasesOrder.ShowDialog();
			if (frmSearchPurchasesOrder.CustCode > 0)
			{
				int custCode = frmSearchPurchasesOrder.CustCode;
				Order_PurchasesOrder order_PurchasesOrder = Pur.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<Order_PurchasesOrderDetails> itemsPurchesByID = Pur.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (Order_PurchasesOrderDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = Pur.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["كود_الصنف"] = item_Items.Item_code;
					dataRow["أسم الصنف"] = item_Items.item_Name;
					dataRow["الشرح"] = item.ItemNote;
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
					dataRow["الكمية"] = FUNC_DecimalNumQty(item.Quantity);
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
				PrintID = custCode;
				TxtType.SelectedValue = order_PurchasesOrder.Order_Paymant_Type;
				CBCostCenter.SelectedValue = order_PurchasesOrder.CostCentersID;
				CBProject.SelectedValue = order_PurchasesOrder.ProjectId;
				TxtCode.Text = order_PurchasesOrder.PurBranchID.ToString();
				LblSup.Text = order_PurchasesOrder.SupplierID.ToString();
				TxtSuppliers.Text = order_PurchasesOrder.SupplierName;
				TxtPhone.Text = order_PurchasesOrder.SupplierPhone;
				TxtNumVat.Text = order_PurchasesOrder.SupplierVatNum;
				TxtDate.DateTime = Convert.ToDateTime(order_PurchasesOrder.Purchases_Date);
				TxtNote.Text = order_PurchasesOrder.Note;
				TxtNumNote.Text = order_PurchasesOrder.NoteNum;
				TxtVat.Text = Convert.ToString(order_PurchasesOrder.Tax);
				TxtTotal.Text = order_PurchasesOrder.Safy.ToString();
				TxtDiscount.Text = order_PurchasesOrder.DiscountNum.ToString();
				TxtVatBeforeDisc.Text = order_PurchasesOrder.Tax_Discount.ToString();
				TxtTotalDiscount.Text = order_PurchasesOrder.Net.ToString();
				TxtNet.Text = order_PurchasesOrder.Net.ToString();
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
			FrmSearchSuppliers frmSearchSuppliers = new FrmSearchSuppliers();
			frmSearchSuppliers.ShowDialog();
			if (frmSearchSuppliers.CustCode > 0)
			{
				int FrmID = frmSearchSuppliers.CustCode;
				Account_CustSup account_CustSup = (from x in SPR.GetAll()
					where x.ID == FrmID && x.IsSuppliers == true
					select x).FirstOrDefault();
				LblSup.Text = FrmID.ToString();
				TxtNumVat.Text = account_CustSup.VatNum.ToString();
				TxtSuppliers.Text = account_CustSup.CustSuppName;
				TxtPhone.Text = account_CustSup.Phone;
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
			CBProject.DataSource = Project.GetSearchAccountProjects();
			CBProject.DisplayMember = "ProjectName";
			CBProject.ValueMember = "ProjectID";
			if (CBProject.Items.Count > 0)
			{
				CBCostCenter.DataSource = Pur.GetCostCentersByProject(Convert.ToInt32(CBProject.SelectedValue));
				CBCostCenter.DisplayMember = "CostCentersName";
				CBCostCenter.ValueMember = "CostCentersID";
			}
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
		dataRow["سعر الوحدة"] = 0.0;
		dataRow["الكمية"] = FUNC_DecimalNumQty(1);
		dataRow["المخزن"] = Pur.GetStoreName();
		dataRow["رقم المخزن"] = Pur.GetStoreID();
		dataRow["تم"] = 0;
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

	public void GetAllitems_alternatives(string Itmcode, string unit, int StoreID)
	{
		if (Itmcode != "")
		{
			TxtQuntity.Text = FUNC_DecimalNumQty(Convert.ToDecimal(Pur.GetQantityForItems(Itmcode, unit, StoreID))).ToString();
			TxtAllQuantity.Text = FUNC_DecimalNumQty(Convert.ToDecimal(Pur.GetQantityForItems(Itmcode, unit))).ToString();
			TxtAvergCost.Text = Math.Round(Convert.ToDecimal(Pur.GetAvargeCost(Itmcode)), DecimalNum).ToString();
			TxtSellPrice1.Text = Math.Round(Convert.ToDecimal(Pur.GetSellprice1(Itmcode)), DecimalNum).ToString();
			TxtSellPrice2.Text = Math.Round(Convert.ToDecimal(Pur.GetSellprice2(Itmcode)), DecimalNum).ToString();
			TxtSellPrice3.Text = Math.Round(Convert.ToDecimal(Pur.GetSellprice3(Itmcode)), DecimalNum).ToString();
			TxtProfit.Text = Math.Round(Convert.ToDecimal(TxtSellPrice1.Text) - Convert.ToDecimal(TxtAvergCost.Text)).ToString();
			Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.Item_code == Itmcode || x.item_Name == Itmcode || x.item_Name_English == Itmcode || x.SmallBarCode1 == Itmcode || x.SmallBarCode2 == Itmcode || x.SmallBarCode3 == Itmcode || x.MediumBarCode1 == Itmcode || x.MediumBarCode2 == Itmcode || x.MediumBarCode3 == Itmcode || x.BigBarCode1 == Itmcode || x.BigBarCode2 == Itmcode || x.BigBarCode3 == Itmcode).FirstOrDefault();
			if (unit == "الصغرى")
			{
				TxtLastCost.Text = item_Items.LastCost.ToString();
				TxtAvergCost.Text = item_Items.Average_cost.ToString();
				TxtSellPrice1.Text = item_Items.SellPriceSmall.ToString();
				TxtSellPrice2.Text = item_Items.SellPriceSmall2.ToString();
				TxtSellPrice3.Text = item_Items.SellPriceSmall3.ToString();
				TxtProfit.Text = (Convert.ToDecimal(TxtSellPrice1.Text) - Convert.ToDecimal(TxtAvergCost.Text)).ToString();
				if (item_Items.LocationSmall != null)
				{
					TxtLocation.Text = item_Items.LocationSmall.ToString();
				}
				else
				{
					TxtLocation.Text = "";
				}
			}
			if (unit == "المتوسطة")
			{
				TxtLastCost.Text = item_Items.LastCost2.ToString();
				TxtAvergCost.Text = (Convert.ToDecimal(item_Items.Average_cost) * Convert.ToDecimal(item_Items.ConvertMediumUnit)).ToString();
				TxtSellPrice1.Text = item_Items.SellPriceMedium.ToString();
				TxtSellPrice2.Text = item_Items.SellPriceMedium2.ToString();
				TxtSellPrice3.Text = item_Items.SellPriceMedium3.ToString();
				TxtProfit.Text = (Convert.ToDecimal(TxtSellPrice1.Text) - Convert.ToDecimal(TxtAvergCost.Text)).ToString();
				if (item_Items.LocationMedium != null)
				{
					TxtLocation.Text = item_Items.LocationMedium.ToString();
				}
				else
				{
					TxtLocation.Text = "";
				}
			}
			if (unit == "الكبرى")
			{
				TxtLastCost.Text = item_Items.LastCost3.ToString();
				TxtAvergCost.Text = (Convert.ToDecimal(item_Items.Average_cost) * Convert.ToDecimal(item_Items.ConvertMediumUnit) * Convert.ToDecimal(item_Items.ConvertBigUnit)).ToString();
				TxtSellPrice1.Text = item_Items.SellpriceLarge.ToString();
				TxtSellPrice2.Text = item_Items.SellpriceLarge2.ToString();
				TxtSellPrice3.Text = item_Items.SellpriceLarge3.ToString();
				TxtProfit.Text = (Convert.ToDecimal(TxtSellPrice1.Text) - Convert.ToDecimal(TxtAvergCost.Text)).ToString();
				if (item_Items.LocationLarge != null)
				{
					TxtLocation.Text = item_Items.LocationLarge.ToString();
				}
				else
				{
					TxtLocation.Text = "";
				}
			}
			if (TxtLastCost.Text == "" || unit == "")
			{
				TxtLastCost.Text = "0.00";
			}
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

	public void SetInitialRow()
	{
		datatable.Columns.Add("كود_الصنف", typeof(string));
		datatable.Columns.Add("أسم الصنف", typeof(string));
		datatable.Columns.Add("الوحدة", typeof(string));
		datatable.Columns.Add("المخزن", typeof(string));
		datatable.Columns.Add("رقم المخزن", typeof(int));
		datatable.Columns.Add("الكمية", typeof(decimal));
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
		datatable.Columns.Add("م", typeof(int));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تم", typeof(int));
		datatable.Columns.Add("الشرح", typeof(string));
		dtCurrentTable.Columns.Add("كود_الصنف", typeof(string));
		dtCurrentTable.Columns.Add("أسم الصنف", typeof(string));
		dtCurrentTable.Columns.Add("الوحدة", typeof(string));
		dtCurrentTable.Columns.Add("المخزن", typeof(string));
		dtCurrentTable.Columns.Add("رقم المخزن", typeof(int));
		dtCurrentTable.Columns.Add("الكمية", typeof(decimal));
		dtCurrentTable.Columns.Add("سعر الوحدة", typeof(decimal));
		dtCurrentTable.Columns.Add("سعربعدالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("نسبةالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("إجمالى_بعدالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("هل_ضريبة", typeof(int));
		dtCurrentTable.Columns.Add("قيمة_الضريبة", typeof(decimal));
		dtCurrentTable.Columns.Add("أسم_الوحدة", typeof(string));
		dtCurrentTable.Columns.Add("الاجمالى", typeof(decimal));
		dtCurrentTable.Columns.Add("الضربية", typeof(decimal));
		dtCurrentTable.Columns.Add("الضريبه_بعد_الخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("إجمالى المبلغ", typeof(decimal));
		dtCurrentTable.Columns.Add("حذف الصنف", typeof(int));
		dtCurrentTable.Columns.Add("م", typeof(int));
		dtCurrentTable.Columns.Add("مسح", typeof(int));
		dtCurrentTable.Columns.Add("تم", typeof(int));
		dtCurrentTable.Columns.Add("الشرح", typeof(string));
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["أسم_الوحدة"] = "الصغرى";
		dataRow["الكمية"] = FUNC_DecimalNumQty(0m);
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
		dataRow["المخزن"] = Pur.GetStoreName();
		dataRow["مسح"] = 0;
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridControl1.ProcessGridKey += gridControl1_ProcessGridKey;
		gridView1.RowCellClick += gridView1_RowCellClick;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		gridView1.CustomDrawFooterCell += gridView1_CustomDrawFooterCell;
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
			if (datatable.Rows.Count <= 1)
			{
				TxtVat.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtNet.Text = "0.00";
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtPhone.Text = "";
				TxtNumNote.Text = "";
				TxtVatBeforeDisc.Text = "0.00";
				TxtTafqit.Text = "";
				TxtTotal.Text = "0.00";
				TxtLocation.Text = "";
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
			if (TxtDiscount.Text == "" || TxtDiscount.Text == "0.00" || Convert.ToDecimal(TxtDiscount.Text) > Convert.ToDecimal(TxtTotal.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				decimal num = Convert.ToDecimal(datatable.Compute("SUM(الاجمالى)", string.Empty));
				TxtTotal.Text = Convert.ToString(num);
				decimal num2 = 0m;
				object obj = datatable.Compute("SUM(الضربية)", string.Empty);
				if (obj != DBNull.Value)
				{
					num2 = Convert.ToDecimal(obj);
				}
				TxtVat.Text = num2.ToString();
				decimal num3 = 0m;
				object obj2 = datatable.Compute("SUM(إجمالى_بعدالخصم)", string.Empty);
				if (obj2 != DBNull.Value)
				{
					num3 = Convert.ToDecimal(obj2);
				}
				decimal num4 = 0m;
				object obj3 = datatable.Compute("SUM(الضريبه_بعد_الخصم)", string.Empty);
				if (obj3 != DBNull.Value)
				{
					num4 = Convert.ToDecimal(obj3);
				}
				TxtVatBeforeDisc.Text = Convert.ToString(num4);
				TxtTotalDiscount.Text = Math.Round(Convert.ToDecimal(TxtTotal.Text), DecimalNum).ToString();
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotal.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)).ToString();
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
			decimal value = Convert.ToDecimal(TxtTotal.Text) + Convert.ToDecimal(TxtVat.Text);
			decimal num5 = Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum) * 100m) / Math.Round(Convert.ToDecimal(value), DecimalNum);
			object summaryValue = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
			object summaryValue2 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
			TxtVat.Text = Convert.ToString(summaryValue2);
			object summaryValue3 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
			object summaryValue4 = gridView1.Columns["الضريبه_بعد_الخصم"].SummaryItem.SummaryValue;
			TxtVatBeforeDisc.Text = Convert.ToString(summaryValue4);
			TxtTotalDiscount.Text = (Math.Round(Convert.ToDecimal(TxtTotal.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
			TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotal.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
			TxtTotal.Text = Math.Round(Convert.ToDecimal(TxtTotal.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
			foreach (DataRow row2 in datatable.Rows)
			{
				if (row2["أسم_الوحدة"].ToString() != "")
				{
					row2["سعربعدالخصم"] = Convert.ToDecimal(row2["سعر الوحدة"].ToString()) - Convert.ToDecimal(num5 * Convert.ToDecimal(row2["سعر الوحدة"].ToString()) / 100m);
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

	private void Cal_FinalResultTotals2()
	{
		try
		{
			if (datatable.Rows.Count <= 1)
			{
				TxtVat.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtNet.Text = "0.00";
				TxtDiscount.Text = "0.00";
				TxtVat.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtTotal.Text = "0.00";
				return;
			}
			if (txtdicoutprege.Text == "" || txtdicoutprege.Text == "0.00" || Convert.ToDecimal(txtdicoutprege.Text) > Convert.ToDecimal(TxtTotal.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				object summaryValue = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
				TxtTotal.Text = Convert.ToString(summaryValue);
				object summaryValue2 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
				TxtVat.Text = Convert.ToString(summaryValue2);
				object summaryValue3 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
				object summaryValue4 = gridView1.Columns["الضريبه_بعد_الخصم"].SummaryItem.SummaryValue;
				TxtVatBeforeDisc.Text = Convert.ToString(summaryValue4);
				TxtTotalDiscount.Text = (Math.Round(Convert.ToDecimal(TxtTotal.Text), 2) + Math.Round(Convert.ToDecimal(TxtVat.Text), 2)).ToString();
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotal.Text), 2) + Math.Round(Convert.ToDecimal(TxtVat.Text), 2)).ToString();
				foreach (DataRow row in datatable.Rows)
				{
					if (row["أسم_الوحدة"].ToString() != "")
					{
						row["سعربعدالخصم"] = row["سعر الوحدة"];
						row["إجمالى_بعدالخصم"] = row["الاجمالى"];
						row["الضريبه_بعد_الخصم"] = row["الضربية"];
					}
				}
				TxtTotal.Text = Math.Round(Convert.ToDecimal(TxtTotal.Text) + Convert.ToDecimal(TxtVat.Text), 2).ToString();
				return;
			}
			decimal value = Convert.ToDecimal(TxtTotal.Text) + Convert.ToDecimal(TxtVat.Text);
			txtdicoutprege.Text = Math.Round(Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), 2) * 100m) / Math.Round(Convert.ToDecimal(value), 2), 2).ToString();
			object summaryValue5 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
			object summaryValue6 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
			TxtVat.Text = Convert.ToString(summaryValue6);
			object summaryValue7 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
			object summaryValue8 = gridView1.Columns["الضريبه_بعد_الخصم"].SummaryItem.SummaryValue;
			TxtVatBeforeDisc.Text = Convert.ToString(summaryValue8);
			TxtTotalDiscount.Text = (Math.Round(Convert.ToDecimal(TxtTotal.Text), 2) + Math.Round(Convert.ToDecimal(TxtVat.Text), 2) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), 2)).ToString();
			TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotal.Text), 2) + Math.Round(Convert.ToDecimal(TxtVat.Text), 2) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), 2)).ToString();
			TxtTotal.Text = Math.Round(Convert.ToDecimal(TxtTotal.Text) + Convert.ToDecimal(TxtVat.Text), 2).ToString();
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
		TxtNumVat.Text = "";
		TxtType.Text = "";
		TxtNote.Text = "";
		TxtDiscount.Text = "";
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtType.Text = "نقدى";
		TxtSuppliers.Text = "مورد نقدى";
		TxtPhone.Text = "";
		TxtNumNote.Text = "";
		TxtVatBeforeDisc.Text = "0.00";
		TxtTafqit.Text = "";
		TxtTotal.Text = "0.00";
		TxtTotalDiscount.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtLocation.Text = "";
		TxtSellPrice1.Text = "0.00";
		TxtSellPrice2.Text = "0.00";
		TxtSellPrice3.Text = "0.00";
		TxtProfit.Text = "0.00";
		TxtQuntity.Text = FUNC_DecimalNumQty(0m).ToString();
		TxtAllQuantity.Text = FUNC_DecimalNumQty(0m).ToString();
		TxtAvergCost.Text = "0.00";
		TxtLastCost.Text = "0.00";
		TxtDate.DateTime = DateTime.Now;
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
		TxtDiscount.Enabled = true;
		TxtNote.Enabled = true;
		TxtType.Enabled = true;
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
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["الكمية"] = FUNC_DecimalNumQty(0m);
		dataRow["سعر الوحدة"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
		dataRow["الضربية"] = 0.0;
		dataRow["إجمالى المبلغ"] = 0.0;
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		IsEnableTrue();
		TxtSuppliers.Focus();
		BindCBCostCenter();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 11))
		{
			int num = 0;
			if (datatable.Rows.Count > 1)
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
							FiterDatatableItemsUpdate();
							if (CBCostCenter.SelectedValue == null)
							{
								CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
							}
							int projectId = ((!int.TryParse(CBProject.SelectedValue?.ToString(), out var result)) ? 1 : result);
							num = Pur.InsertPurch(out var Code, Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), Convert.ToInt32(CBCostCenter.SelectedValue), TxtNote.Text, TxtNumNote.Text, Math.Round(Convert.ToDecimal(TxtTotal.Text), 2), Math.Round(Convert.ToDecimal(TxtVat.Text), 2), Math.Round(Convert.ToDecimal(TxtTotal.Text), 2), Math.Round(Convert.ToDecimal(TxtDiscount.Text), 2), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), 2), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), 2), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), 2), Math.Round(Convert.ToDecimal(TxtNet.Text), 2), projectId);
							Pur.InsertItems(datatable, num);
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
								switch (MessageBox.Show("تم حفظ بيانات فاتورة أمر الشراء بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
								{
								case DialogResult.Yes:
									Print();
									break;
								}
							}
							else
							{
								MessageBox.Show("تم حفظ بيانات فاتورة أمر الشراء رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
					MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة أمر الشراء بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 11))
		{
			if (TxtDate.ReadOnly)
			{
				IsEnableTrue();
				BtnEdit.Text = "F5 حفظ";
				insertnewrow();
				gridView1.OptionsBehavior.Editable = true;
				return;
			}
			int num = 0;
			if (!(TxtSuppliers.Text != ""))
			{
				return;
			}
			if (Convert.ToInt32(TxtType.SelectedValue) == 2 && LblSup.Text == "1")
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع مورد نقدى", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtSuppliers.Focus();
			}
			else if (datatable.Rows.Count > 1)
			{
				if (IsItemPriceZero() == 0)
				{
					return;
				}
				ArrangeDatatable();
				Cal_FinalResultTotals();
				GetTotalCost();
				FiterDatatableItemsUpdate();
				if (CBCostCenter.SelectedValue == null)
				{
					CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
				}
				int projectId = ((!int.TryParse(CBProject.SelectedValue?.ToString(), out var result)) ? 1 : result);
				num = Pur.UpdatePurch(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), Convert.ToInt32(CBCostCenter.SelectedValue), TxtNote.Text, TxtNumNote.Text, Math.Round(Convert.ToDecimal(TxtTotal.Text), 2), Math.Round(Convert.ToDecimal(TxtVat.Text), 2), Math.Round(Convert.ToDecimal(TxtTotal.Text), 2), Math.Round(Convert.ToDecimal(TxtDiscount.Text), 2), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), 2), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), 2), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), 2), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), 2), projectId);
				decimal num2 = Math.Round((Convert.ToDecimal(txtdicoutprege.Text) * Convert.ToDecimal(TxtVat.Text) / 100m - Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)) * -1m, DecimalNum);
				decimal num3 = Math.Round((Convert.ToDecimal(txtdicoutprege.Text) * Convert.ToDecimal(TxtTotal.Text) / 100m - Math.Round(Convert.ToDecimal(TxtTotal.Text), DecimalNum)) * -1m, DecimalNum);
				Pur.deleteItemsByPurcahesId(num);
				Pur.InsertItems(datatable, num);
				if (num > 0)
				{
					PrintID = num;
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					IsEnableFalse();
					BtnEdit.Text = "Edit تعديل";
					gridView1.OptionsBehavior.Editable = false;
					switch (MessageBox.Show("تم تعديل فاتورة أمر الشراء بنجاح \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						Print();
						break;
					}
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات فاتورة أمر الشراء رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 11))
		{
			int num = 0;
			if (TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة أمر الشراء رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = Pur.Delete(Convert.ToInt32(TxtCode.Text));
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات فاتورة أمر الشراء رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						gridView1.OptionsBehavior.Editable = true;
						IsEnableTrue();
						BtnDelete.Enabled = false;
						BtnEdit.Enabled = false;
						BtnSave.Enabled = true;
						insertnewrow();
						TxtSuppliers.Focus();
						PrintID = 0;
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات فاتورة أمر الشراء رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة أمر الشراء بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 11))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_PurchasesOrder", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@ID", PrintID);
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
				sqlConnection.Close();
				if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					if (LoginDetails.Lang == "ar-EG")
					{
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintPurchaseOrder.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintPurchaseOrder.mrt");
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
		TxtNote.ReadOnly = false;
		TxtType.Enabled = true;
		TxtDate.ReadOnly = false;
		TxtSuppliers.ReadOnly = false;
		TxtNumVat.ReadOnly = false;
		TxtPhone.ReadOnly = false;
		TxtNumNote.ReadOnly = false;
		CBCostCenter.Enabled = true;
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = true;
		TxtNote.ReadOnly = true;
		TxtType.Enabled = false;
		TxtDate.ReadOnly = true;
		TxtSuppliers.ReadOnly = true;
		TxtNumVat.ReadOnly = true;
		TxtPhone.ReadOnly = true;
		TxtNumNote.ReadOnly = true;
		CBCostCenter.Enabled = false;
		TxtDiscount.BackColor = Color.White;
		TxtNote.BackColor = Color.White;
		TxtType.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
		TxtSuppliers.BackColor = Color.White;
		TxtNumVat.BackColor = Color.White;
		TxtPhone.BackColor = Color.White;
		TxtNumNote.BackColor = Color.White;
	}

	public void openItmCard()
	{
		if (gridControl1.Focus())
		{
			if (CodeItm != "")
			{
				LoginDetails.ItemSearchID = Pur.GetItmID(CodeItm);
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
				foreach (string item in LoginDetails.ItemSearchAll2)
				{
					string code = item.ToString();
					Item_Items item_Items = Pur.GetItems(code).FirstOrDefault();
					if (item_Items != null)
					{
						DataRow dataRow2 = null;
						dataRow2 = datatable.NewRow();
						dataRow2["كود_الصنف"] = item_Items.Item_code;
						dataRow2["أسم الصنف"] = item_Items.item_Name;
						dataRow2["الوحدة"] = item_Items.Item_Unit.Name;
						dataRow2["الكمية"] = FUNC_DecimalNumQty(1);
						dataRow2["المخزن"] = Pur.GetStoreName();
						dataRow2["رقم المخزن"] = Pur.GetStoreID();
						dataRow2["أسم_الوحدة"] = "الصغرى";
						dataRow2["سعر الوحدة"] = Math.Round(Convert.ToDecimal(item_Items.LastCost), DecimalNum);
						decimal num2 = Convert.ToDecimal(dataRow2["سعر الوحدة"]);
						dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost), DecimalNum);
						if (Pur.IsVatActive())
						{
							if (LoginDetails.ItemTotalWithVat)
							{
								if (item_Items.Is_Tax == true)
								{
									dataRow2["سعر الوحدة"] = Math.Round(Convert.ToDecimal(item_Items.LastCost / (item_Items.Tax_Value / (decimal?)100 + (decimal?)1)), DecimalNum);
									dataRow2["الاجمالى"] = dataRow2["سعر الوحدة"];
									dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(dataRow2["الاجمالى"]) * Convert.ToDecimal(item_Items.Tax_Value / (decimal?)100 + (decimal?)1), 1);
									dataRow2["الضربية"] = Math.Round(Convert.ToDecimal(dataRow2["إجمالى المبلغ"]) - Convert.ToDecimal(dataRow2["الاجمالى"]), DecimalNum);
									dataRow2["هل_ضريبة"] = 1;
									dataRow2["قيمة_الضريبة"] = item_Items.Tax_Value;
								}
								else
								{
									dataRow2["الضربية"] = 0;
									dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(dataRow2["الكمية"]))), DecimalNum);
									dataRow2["هل_ضريبة"] = 0;
									dataRow2["قيمة_الضريبة"] = 0;
								}
							}
							else if (item_Items.Is_Tax == true)
							{
								DataRow dataRow3 = dataRow2;
								decimal value = num2 * Convert.ToDecimal(dataRow2["الكمية"]);
								decimal? tax_Value = item_Items.Tax_Value;
								dataRow3["الضربية"] = Math.Round(Convert.ToDecimal((decimal?)value * tax_Value / (decimal?)100), DecimalNum);
								DataRow dataRow4 = dataRow2;
								decimal value2 = num2 * Convert.ToDecimal(dataRow2["الكمية"]);
								decimal? tax_Value2 = item_Items.Tax_Value;
								dataRow4["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(num2 * Convert.ToDecimal(dataRow2["الكمية"]))), DecimalNum);
								dataRow2["هل_ضريبة"] = 1;
								dataRow2["قيمة_الضريبة"] = item_Items.Tax_Value;
							}
							else
							{
								dataRow2["الضربية"] = 0;
								dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(num2 * Convert.ToDecimal(dataRow2["الكمية"]))), DecimalNum);
								dataRow2["هل_ضريبة"] = 0;
								dataRow2["قيمة_الضريبة"] = 0;
							}
						}
						else
						{
							dataRow2["هل_ضريبة"] = 0;
							dataRow2["قيمة_الضريبة"] = 0;
							dataRow2["الضربية"] = 0.0;
							dataRow2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToInt32(1))), DecimalNum);
						}
						object obj = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
						int num3 = Convert.ToInt32(obj.ToString());
						dataRow2["م"] = num3 + 1;
						datatable.Rows.Add(dataRow2);
					}
					IsShow = 1;
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

	private void TxtDigital()
	{
		TxtVat.DecimalDigits = DecimalNum;
		TxtVatBeforeDisc.DecimalDigits = DecimalNum;
		TxtTotal.DecimalDigits = DecimalNum;
		TxtTotalDiscount.DecimalDigits = DecimalNum;
		TxtNet.DecimalDigits = DecimalNum;
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
					Item_Items item_Items = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items != null)
					{
						int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row in datatable.Rows)
						{
							if (row["م"].ToString() == Convert.ToString(num))
							{
								row["الوحدة"] = item_Items.Item_Unit.Name;
								row["أسم_الوحدة"] = "الصغرى";
								row["المخزن"] = Pur.GetStoreName();
								row["رقم المخزن"] = Pur.GetStoreID();
								row["الكمية"] = FUNC_DecimalNumQty(1);
								row["سعر الوحدة"] = item_Items.LastCost;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToInt32(1)), DecimalNum);
								if (item_Items.Is_Tax == true)
								{
									row["الضربية"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToInt32(1) * item_Items.Tax_Value / (decimal?)100), DecimalNum);
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToInt32(1) * item_Items.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToInt32(1))), DecimalNum);
								}
								else
								{
									row["الضربية"] = 0.0;
									row["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToInt32(1))), DecimalNum);
								}
							}
						}
						GetTotalCost();
						string StoreName = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int storeID = (from x in db.Account_Stores
							where x.Store_Name == StoreName
							select x.ID).FirstOrDefault();
						GetAllitems_alternatives(itemCode, "الصغرى", storeID);
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "المتوسطة")
				{
					Item_Items item_Items2 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items2 != null)
					{
						int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row2 in datatable.Rows)
						{
							if (!(row2["م"].ToString() == Convert.ToString(num2)))
							{
								continue;
							}
							row2["الوحدة"] = item_Items2.Item_Unit1.Name;
							row2["أسم_الوحدة"] = "المتوسطة";
							row2["الكمية"] = FUNC_DecimalNumQty(1);
							row2["المخزن"] = Pur.GetStoreName();
							row2["رقم المخزن"] = Pur.GetStoreID();
							if (LoginDetails.ItemTotalWithVat)
							{
								row2["سعر الوحدة"] = Math.Round(Convert.ToDecimal(item_Items2.LastCost2) / (Convert.ToDecimal(item_Items2.Tax_Value / (decimal?)100) + 1m), 2);
								row2["الاجمالى"] = row2["سعر الوحدة"];
							}
							else
							{
								row2["سعر الوحدة"] = item_Items2.LastCost2;
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items2.LastCost2 * (decimal?)Convert.ToInt32(1)), DecimalNum);
							}
							if (item_Items2.Is_Tax == true)
							{
								if (LoginDetails.ItemTotalWithVat)
								{
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items2.LastCost2 * (decimal?)Convert.ToInt32(1)), DecimalNum);
									row2["الضربية"] = Math.Round(Convert.ToDecimal(row2["إجمالى المبلغ"]) - Convert.ToDecimal(row2["الاجمالى"]), 2);
								}
								else
								{
									row2["الضربية"] = Math.Round(Convert.ToDecimal(item_Items2.LastCost2 * (decimal?)Convert.ToInt32(1) * item_Items2.Tax_Value / (decimal?)100), DecimalNum);
									row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items2.LastCost2 * (decimal?)Convert.ToInt32(1) * item_Items2.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items2.LastCost2 * (decimal?)Convert.ToInt32(1))), DecimalNum);
								}
							}
							else
							{
								row2["الضربية"] = 0.0;
								row2["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items2.LastCost2 * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
						}
						GetTotalCost();
						string StoreName2 = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int storeID2 = (from x in db.Account_Stores
							where x.Store_Name == StoreName2
							select x.ID).FirstOrDefault();
						GetAllitems_alternatives(itemCode, "المتوسطة", storeID2);
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "الكبرى")
				{
					Item_Items item_Items3 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items3 != null)
					{
						int num3 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row3 in datatable.Rows)
						{
							if (!(row3["م"].ToString() == Convert.ToString(num3)))
							{
								continue;
							}
							row3["الوحدة"] = item_Items3.Item_Unit2.Name;
							row3["أسم_الوحدة"] = "الكبرى";
							row3["الكمية"] = FUNC_DecimalNumQty(1);
							row3["المخزن"] = Pur.GetStoreName();
							row3["رقم المخزن"] = Pur.GetStoreID();
							if (LoginDetails.ItemTotalWithVat)
							{
								row3["سعر الوحدة"] = Math.Round(Convert.ToDecimal(item_Items3.LastCost3) / (Convert.ToDecimal(item_Items3.Tax_Value / (decimal?)100) + 1m), 2);
								row3["الاجمالى"] = row3["سعر الوحدة"];
							}
							else
							{
								row3["سعر الوحدة"] = item_Items3.LastCost3;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items3.LastCost3 * (decimal?)Convert.ToInt32(1)), DecimalNum);
							}
							if (item_Items3.Is_Tax == true)
							{
								if (LoginDetails.ItemTotalWithVat)
								{
									row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items3.LastCost3 * (decimal?)Convert.ToInt32(1)), DecimalNum);
									row3["الضربية"] = Math.Round(Convert.ToDecimal(row3["إجمالى المبلغ"]) - Convert.ToDecimal(row3["الاجمالى"]), 2);
								}
								else
								{
									row3["الضربية"] = Math.Round(Convert.ToDecimal(item_Items3.LastCost3 * (decimal?)Convert.ToInt32(1) * item_Items3.Tax_Value / (decimal?)100), DecimalNum);
									row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(item_Items3.LastCost3 * (decimal?)Convert.ToInt32(1) * item_Items3.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items3.LastCost3 * (decimal?)Convert.ToInt32(1))), DecimalNum);
								}
							}
							else
							{
								row3["الضربية"] = 0.0;
								row3["إجمالى المبلغ"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items3.LastCost3 * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
						}
						GetTotalCost();
						string StoreName3 = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int storeID3 = (from x in db.Account_Stores
							where x.Store_Name == StoreName3
							select x.ID).FirstOrDefault();
						GetAllitems_alternatives(itemCode, "الكبرى", storeID3);
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
			Item_Items item_Items = (from x in Pur.GetItems()
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
						if (row["م"].ToString() == Convert.ToString(num))
						{
							row["المخزن"] = Pur.GetStoreName();
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
				if (row2["م"].ToString() == Convert.ToString(num2))
				{
					row2["المخزن"] = value;
					row2["رقم المخزن"] = StoreID;
				}
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[5];
			gridView1.ShowEditor();
			GetTotalCost();
			GetAllitems_alternatives(Itemocde, "الصغرى", StoreID);
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
			if (e.KeyCode.ToString() == "F11")
			{
				int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				{
					foreach (DataRow row in datatable.Rows)
					{
						if (row["م"].ToString() == Convert.ToString(num))
						{
							Class_Items class_Items = new Class_Items();
							int itemID = class_Items.GetItemID(row["كود_الصنف"].ToString());
							if (itemID > 0)
							{
								LoginDetails.ItemcodeForStore = itemID;
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
					decimal num = Math.Round(Convert.ToDecimal(txtdicoutprege.Text) * Convert.ToDecimal(TxtTotal.Text) / 100m, DecimalNum);
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
			decimal num = Math.Round(Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotal.Text) * 100m, DecimalNum);
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
			Cal_FinalResultTotals();
		}
		catch (Exception)
		{
			TxtVat.Text = "0.00";
			TxtTotalDiscount.Text = "0.00";
			TxtNet.Text = "0.00";
			TxtTotal.Text = "0.00";
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
				ToWord toWord = new ToWord(Convert.ToDecimal(TxtTotalDiscount.Text), currencies[tafqitID]);
				txtWordAr = toWord.ConvertToArabic();
				TxtTafqit.Text = txtWordAr;
			}
			else
			{
				ToWord toWord2 = new ToWord(Convert.ToDecimal(TxtTotalDiscount.Text), currencies[tafqitID]);
				txtWordAr = toWord2.ConvertToEnglish();
				TxtTafqit.Text = txtWordAr;
			}
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

	private void MenuExit_Click(object sender, EventArgs e)
	{
		Close();
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

	private void FrmPurchaseOrder_FormClosing(object sender, FormClosingEventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmPurchaseOrder));
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
		this.label13 = new System.Windows.Forms.Label();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.label14 = new System.Windows.Forms.Label();
		this.TxtQuntity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtAllQuantity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtLastCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtAvergCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.نوعالفاتورةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.اسمالموردToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.الكمياتToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.الكميةالحاليةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.كلالكمياتبالمستودعاتToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.كلالكمياتبالفروعToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.تخصيصToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.TxtProfit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label8 = new System.Windows.Forms.Label();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtSellPrice3 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtTotalDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtSellPrice2 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtSellPrice1 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatBeforeDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.رقمالفاتورةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.label26 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label24 = new System.Windows.Forms.Label();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtNumVat = new DevExpress.XtraEditors.TextEdit();
		this.label18 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label27 = new System.Windows.Forms.Label();
		this.CBProject = new System.Windows.Forms.ComboBox();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.استعلامToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.TxtAllDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label6 = new System.Windows.Forms.Label();
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
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox2.SuspendLayout();
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
		resources.ApplyResources(this.label15, "label15");
		this.label15.Name = "label15";
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
		resources.ApplyResources(this.نوعالفاتورةToolStripMenuItem1, "نوعالفاتورةToolStripMenuItem1");
		this.نوعالفاتورةToolStripMenuItem1.Name = "نوعالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.TxtNet, "TxtNet");
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.gridColumn4.MinWidth = 17;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"), resources.GetString("repositoryItemButtonEdit5.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit5.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEdit5.Buttons6"), resources.GetObject("repositoryItemButtonEdit5.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit5.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit5.Buttons9"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEdit5.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
		resources.ApplyResources(this.اسمالموردToolStripMenuItem1, "اسمالموردToolStripMenuItem1");
		this.اسمالموردToolStripMenuItem1.Name = "اسمالموردToolStripMenuItem1";
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
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[9] { this.repositoryItemButtonEdit4, this.repositoryItemButtonEdit6, this.repositoryItemTextEdit1, this.repositoryItemTextEdit2, this.repositoryItemDateEdit1, this.repositoryItemTextEdit3, this.repositoryItemPictureEdit1, this.repositoryItemButtonEdit5, this.repositoryItemButtonEditCodeSearch });
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView1.Appearance.HeaderPanel.Font");
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		resources.ApplyResources(this.gridView1, "gridView1");
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[15]
		{
			this.gridColumn1, this.gridColumn2, this.gridColumn23, this.shearch, this.gridColumn19, this.gridColumn4, this.gridColumn20, this.gridColumn21, this.gridColumn3, this.gridColumn6,
			this.gridColumn7, this.gridColumn8, this.gridColumn22, this.gridColumn5, this.gridColumn9
		});
		this.gridView1.DetailHeight = 284;
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
		this.gridColumn1.MinWidth = 17;
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonEditCodeSearch, "repositoryItemButtonEditCodeSearch");
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons"), resources.GetString("repositoryItemButtonEditCodeSearch.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEditCodeSearch.Buttons6"), resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons9"))
		});
		this.repositoryItemButtonEditCodeSearch.Name = "repositoryItemButtonEditCodeSearch";
		this.repositoryItemButtonEditCodeSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEditCodeSearch_ButtonClick);
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
		this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn23.FieldName = "الشرح";
		this.gridColumn23.MinWidth = 25;
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.shearch.ColumnEdit = this.repositoryItemButtonEdit4;
		this.shearch.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.FieldName = "الوحدة";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.MinWidth = 17;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		this.shearch.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
		editorButtonImageOptions3.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		editorButtonImageOptions3.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemButtonEdit4.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.FieldName = "أسم_الوحدة";
		this.gridColumn19.MinWidth = 17;
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
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
		this.gridColumn6.MinWidth = 17;
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
		this.gridColumn7.MinWidth = 17;
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.OptionsColumn.AllowFocus = false;
		this.gridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn7.OptionsColumn.ReadOnly = true;
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
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit6;
		this.gridColumn8.FieldName = "حذف";
		this.gridColumn8.MinWidth = 17;
		this.gridColumn8.Name = "gridColumn8";
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit6, "repositoryItemButtonEdit6");
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
		resources.ApplyResources(this.gridColumn22, "gridColumn22");
		this.gridColumn22.FieldName = "إجمالى_بعدالخصم";
		this.gridColumn22.MinWidth = 17;
		this.gridColumn22.Name = "gridColumn22";
		resources.ApplyResources(this.gridColumn5, "gridColumn5");
		this.gridColumn5.FieldName = "الضريبه_بعد_الخصم";
		this.gridColumn5.MinWidth = 17;
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
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
		this.gridColumn9.MinWidth = 17;
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
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
		this.cardView1.CardWidth = 171;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.DetailHeight = 284;
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
		resources.ApplyResources(this.TxtVat, "TxtVat");
		this.TxtVat.DecimalDigits = 3;
		this.TxtVat.ForeColor = System.Drawing.Color.Black;
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.ReadOnly = true;
		this.TxtVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4]);
		this.TxtVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		resources.ApplyResources(this.TxtTotal, "TxtTotal");
		this.TxtTotal.DecimalDigits = 3;
		this.TxtTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtTotal.Name = "TxtTotal";
		this.TxtTotal.ReadOnly = true;
		this.TxtTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotal.Value = new decimal(new int[4]);
		this.TxtTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotal.TextChanged += new System.EventHandler(TxtTotals_TextChanged);
		resources.ApplyResources(this.label11, "label11");
		this.label11.Name = "label11";
		resources.ApplyResources(this.TxtTotalDiscount, "TxtTotalDiscount");
		this.TxtTotalDiscount.DecimalDigits = 3;
		this.TxtTotalDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalDiscount.Name = "TxtTotalDiscount";
		this.TxtTotalDiscount.ReadOnly = true;
		this.TxtTotalDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalDiscount.Value = new decimal(new int[4]);
		this.TxtTotalDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalDiscount.TextChanged += new System.EventHandler(TxtNet_TextChanged);
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
		resources.ApplyResources(this.TxtVatBeforeDisc, "TxtVatBeforeDisc");
		this.TxtVatBeforeDisc.DecimalDigits = 3;
		this.TxtVatBeforeDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatBeforeDisc.Name = "TxtVatBeforeDisc";
		this.TxtVatBeforeDisc.ReadOnly = true;
		this.TxtVatBeforeDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatBeforeDisc.Value = new decimal(new int[4]);
		this.TxtVatBeforeDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.رقمالفاتورةToolStripMenuItem1, "رقمالفاتورةToolStripMenuItem1");
		this.رقمالفاتورةToolStripMenuItem1.Name = "رقمالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.label26, "label26");
		this.label26.Name = "label26";
		resources.ApplyResources(this.CBCostCenter, "CBCostCenter");
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		this.CBCostCenter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
		resources.ApplyResources(this.LblSup, "LblSup");
		this.LblSup.Name = "LblSup";
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
		this.TxtDate.Properties.Mask.EditMask = resources.GetString("TxtDate.Properties.Mask.EditMask");
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label24, "label24");
		this.label24.Name = "label24";
		resources.ApplyResources(this.TxtNumNote, "TxtNumNote");
		this.TxtNumNote.Name = "TxtNumNote";
		this.TxtNumNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumNote.Properties.Appearance.Font");
		this.TxtNumNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNumNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNote.Properties.Appearance.Font");
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNote.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNote.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label22, "label22");
		this.label22.Name = "label22";
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
		resources.ApplyResources(this.label20, "label20");
		this.label20.Name = "label20";
		resources.ApplyResources(this.TxtType, "TxtType");
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Name = "TxtType";
		this.TxtType.SelectedIndexChanged += new System.EventHandler(TxtType_SelectedIndexChanged_1);
		this.TxtType.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlTxtSuppliers_KeyDown);
		this.TxtType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
		resources.ApplyResources(this.label19, "label19");
		this.label19.Name = "label19";
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
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label27);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Controls.Add(this.CBProject);
		this.groupBox1.Controls.Add(this.CBCostCenter);
		this.groupBox1.Controls.Add(this.LblSup);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.TxtNumNote);
		this.groupBox1.Controls.Add(this.label23);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.TxtPhone);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.TxtType);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.TxtNumVat);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.TxtSuppliers);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		this.groupBox1.Enter += new System.EventHandler(textBox1_Enter);
		resources.ApplyResources(this.label27, "label27");
		this.label27.Name = "label27";
		resources.ApplyResources(this.CBProject, "CBProject");
		this.CBProject.FormattingEnabled = true;
		this.CBProject.Name = "CBProject";
		this.CBProject.SelectedIndexChanged += new System.EventHandler(CBProject_SelectedIndexChanged);
		this.CBProject.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		this.CBProject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
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
		resources.ApplyResources(this.repositoryItemButtonEditCode, "repositoryItemButtonEditCode");
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions8");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCode.Buttons"), resources.GetString("repositoryItemButtonEditCode.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCode.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("repositoryItemButtonEditCode.Buttons6"), resources.GetObject("repositoryItemButtonEditCode.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCode.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCode.Buttons9"))
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
		resources.ApplyResources(this.استعلامToolStripMenuItem1, "استعلامToolStripMenuItem1");
		this.استعلامToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.رقمالفاتورةToolStripMenuItem1, this.اسمالموردToolStripMenuItem1, this.نوعالفاتورةToolStripMenuItem1 });
		this.استعلامToolStripMenuItem1.Name = "استعلامToolStripMenuItem1";
		resources.ApplyResources(this.MenuPurchOrder, "MenuPurchOrder");
		this.MenuPurchOrder.Name = "MenuPurchOrder";
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
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.طباعةToolStripMenuItem1, this.toolStripSeparator1, this.MenuNewItem, this.حركةصنفToolStripMenuItem, this.MenuSearchItem, this.toolStripSeparator4,
			this.MenuPurchOrder, this.استعلامToolStripMenuItem1, this.الكمياتToolStripMenuItem1, this.toolStripSeparator2, this.تخصيصToolStripMenuItem1, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.ribbonPage2.Name = "ribbonPage2";
		resources.ApplyResources(this.ribbonPage2, "ribbonPage2");
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.TxtAllDiscount);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.TxtLocation);
		this.groupBox2.Controls.Add(this.label25);
		this.groupBox2.Controls.Add(this.txtdicoutprege);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.TxtTafqit);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.TxtQuntity);
		this.groupBox2.Controls.Add(this.label16);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.label15);
		this.groupBox2.Controls.Add(this.TxtAllQuantity);
		this.groupBox2.Controls.Add(this.TxtNet);
		this.groupBox2.Controls.Add(this.TxtLastCost);
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.TxtAvergCost);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.TxtProfit);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.TxtVat);
		this.groupBox2.Controls.Add(this.TxtSellPrice3);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.TxtTotal);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtTotalDiscount);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.TxtSellPrice2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtSellPrice1);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.TxtDiscount);
		this.groupBox2.Controls.Add(this.TxtVatBeforeDisc);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.TxtAllDiscount, "TxtAllDiscount");
		this.TxtAllDiscount.DecimalDigits = 3;
		this.TxtAllDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtAllDiscount.Name = "TxtAllDiscount";
		this.TxtAllDiscount.ReadOnly = true;
		this.TxtAllDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllDiscount.Value = new decimal(new int[4]);
		this.TxtAllDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmPurchaseOrder";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmPurchaseOrder_FormClosing);
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
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
