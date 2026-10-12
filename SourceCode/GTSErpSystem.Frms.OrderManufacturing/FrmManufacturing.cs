using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using GTSErpSystem.BLL.OrderManufacturing;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.OrderManufacturing.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.OrderManufacturing;

public class FrmManufacturing : XtraForm
{
	private Class_Orders ORD = new Class_Orders();

	private Class_Manufacturing Pur = new Class_Manufacturing();

	private Class_Suppliers SPR = new Class_Suppliers();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private GTSdbEntities db = new GTSdbEntities();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private DataTable datatable = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	private DataTable datatableManfc = new DataTable();

	private DataTable dtCurrentTableManfc = new DataTable();

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

	public bool ISEVENTDISC = true;

	private int DecimalNum = LoginDetails.DecimalNum;

	private string fileNameAR1 = Application.StartupPath + "\\Layout\\OrderAR1.xml";

	private string fileNameEN1 = Application.StartupPath + "\\Layout\\OrderEN1.xml";

	private string fileNameAR2 = Application.StartupPath + "\\Layout\\OrderAR2.xml";

	private string fileNameEN2 = Application.StartupPath + "\\Layout\\OrderEN2.xml";

	private IContainer components = null;

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

	private GridColumn gridColumn8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private GridColumn gridColumn22;

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

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private ToolStripMenuItem RecoveredMenuItemFix8;

	private Label label26;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private DateEdit TxtDate;

	private Label label24;

	private TextEdit TxtNumNote;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private Label label23;

	private TextEdit TxtNote;

	private Label label21;

	private GroupBox groupBox1;

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

	private GroupBox groupBox2;

	private NumericEditBox txtdicoutprege;

	private Label label7;

	private Label label8;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtTotals;

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

	private Label label6;

	private SimpleButton BtnManufacturingOrder;

	private SimpleButton BtnPlus;

	private GridControl gridControl2;

	private GridView gridView2;

	private GridColumn gridColumn23;

	private GridColumn gridColumn24;

	private GridColumn gridColumn25;

	private GridColumn gridColumn26;

	private GridColumn gridColumn27;

	private GridColumn gridColumn28;

	private GridColumn gridColumn29;

	private GridColumn gridColumn30;

	private GridColumn gridColumn33;

	private GridColumn gridColumn34;

	private GridColumn gridColumn36;

	private CardView cardView2;

	private GridColumn gridColumn37;

	private GridColumn gridColumn38;

	private GridColumn gridColumn39;

	private GridColumn gridColumn40;

	private GridColumn gridColumn41;

	private GridColumn gridColumn42;

	private GridColumn gridColumn43;

	private GridColumn gridColumn44;

	private GridColumn gridColumn45;

	private GridColumn gridColumn5;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn31;

	private GridColumn gridColumn32;

	private GridColumn gridColumn35;

	private Label label39;

	private System.Windows.Forms.ComboBox CBProject;

	private RepositoryItemButtonEdit repositoryItemButtonEdit10;

	private RepositoryItemButtonEdit repositoryItemButtonEdit7;

	private RepositoryItemButtonEdit repositoryItemButtonEdit9;

	private RepositoryItemButtonEdit repositoryItemButtonEdit11;

	private RepositoryItemButtonEdit repositoryItemButtonEdit8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit12;

	public FrmManufacturing()
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
		SetInitialRowManfc();
		BindCBCostCenter();
		TxtDate.Focus();
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		txtdicoutprege.Controls[0].Visible = false;
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 119);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 119);
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
							if (item_Items.item_Type == 3)
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
								dataRow["الكمية"] = 1.0;
								dataRow["عدد الوحدة"] = 0;
								dataRow["مسح"] = 0;
								dataRow["تم"] = 1;
								dataRow["أسم_الوحدة"] = "الصغرى";
								dataRow["سعر الوحدة"] = item_Items.LastCost;
								dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(1)), DecimalNum);
								dataRow["المخزن"] = Pur.GetStoreName();
								dataRow["رقم المخزن"] = Pur.GetStoreID();
								object rowCellValue2 = gridView1.GetRowCellValue(e.RowHandle, "ظ…");
								string text3 = rowCellValue2.ToString();
								if (text3 == "")
								{
									dataRow["ظ…"] = e.RowHandle + 1;
								}
								else
								{
									dataRow["ظ…"] = text3;
								}
								CheckFfistRow();
								datatable.Rows.Add(dataRow);
								insertnewrow();
								GetTotalCost();
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
							string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("ظ…");
							if (row["ظ…"].ToString() == focusedRowCellDisplayText4)
							{
								if (row["أسم_الوحدة"].ToString() == "الصغرى")
								{
									row["كود_الصنف"] = item_Items.Item_code;
									row["أسم الصنف"] = item_Items.item_Name;
									row["سعر الوحدة"] = item_Items.LastCost;
									row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(value)), DecimalNum);
									break;
								}
								if (row["أسم_الوحدة"].ToString() == "المتوسطة")
								{
									row["كود_الصنف"] = item_Items.Item_code;
									row["أسم الصنف"] = item_Items.item_Name;
									row["سعر الوحدة"] = item_Items.LastCost2;
									row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost2 * (decimal?)Convert.ToDecimal(value)), DecimalNum);
									break;
								}
								if (row["أسم_الوحدة"].ToString() == "الكبرى")
								{
									row["كود_الصنف"] = item_Items.Item_code;
									row["أسم الصنف"] = item_Items.item_Name;
									row["سعر الوحدة"] = item_Items.LastCost3;
									row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost3 * (decimal?)Convert.ToDecimal(value)), DecimalNum);
									break;
								}
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
			string focusedRowCellDisplayText8 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText5 != "")
			{
				Item_Items item_Items2 = Pur.GetItems(focusedRowCellDisplayText5).FirstOrDefault();
				if (item_Items2 != null)
				{
					foreach (DataRow row2 in datatable.Rows)
					{
						if (row2["ظ…"].ToString() == focusedRowCellDisplayText8)
						{
							decimal num2 = Convert.ToDecimal(row2["سعر الوحدة"].ToString());
							if (row2["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value2)), DecimalNum);
								break;
							}
							if (row2["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value2)), DecimalNum);
								break;
							}
							if (row2["أسم_الوحدة"].ToString() == "الكبرى")
							{
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value2)), DecimalNum);
								break;
							}
						}
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
			string value3 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText9 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText10 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText11 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText12 = gridView1.GetFocusedRowCellDisplayText("الكمية");
			string focusedRowCellDisplayText13 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText9 != "")
			{
				Item_Items item_Items3 = Pur.GetItems(focusedRowCellDisplayText9).FirstOrDefault();
				if (item_Items3 != null)
				{
					foreach (DataRow row3 in datatable.Rows)
					{
						if (row3["ظ…"].ToString() == focusedRowCellDisplayText13)
						{
							if (row3["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row3["سعر الوحدة"] = value3;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value3) * Convert.ToDecimal(focusedRowCellDisplayText12)), DecimalNum);
								break;
							}
							if (row3["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								row3["سعر الوحدة"] = value3;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value3) * Convert.ToDecimal(focusedRowCellDisplayText12)), DecimalNum);
								break;
							}
							if (row3["أسم_الوحدة"].ToString() == "الكبرى")
							{
								row3["سعر الوحدة"] = value3;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value3) * Convert.ToDecimal(focusedRowCellDisplayText12)), DecimalNum);
								break;
							}
						}
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
		gridControl1.DataSource = datatable;
		gridControl2.DataSource = datatableManfc;
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

	private void gridControl1_ProcessGridKey(object sender, KeyEventArgs e)
	{
	}

	private void gridView2_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string text = Convert.ToString(gridView2.GetRowCellValue(e.RowHandle, "كود_الصنف"));
		string text2 = Convert.ToString(gridView2.GetRowCellValue(e.RowHandle, "أسم الصنف"));
		decimal num = Convert.ToDecimal(gridView2.GetRowCellValue(e.RowHandle, "الكمية"));
		decimal num2 = Convert.ToDecimal(gridView2.GetRowCellValue(e.RowHandle, "النسبة"));
		if (text == "" || text2 == "" || num <= 0m || num2 <= 0m)
		{
			Item_Items item_Items = Pur.GetItems(text).FirstOrDefault();
			if (item_Items != null)
			{
				e.Valid = false;
				if (text == "")
				{
					gridView2.FocusedRowHandle = e.RowHandle;
					gridView2.FocusedColumn = gridView2.VisibleColumns[0];
					gridView2.ShowEditor();
				}
				else if (text2 == "")
				{
					MessageBox.Show("برجاء إدخال أسم صنف");
					gridView2.FocusedRowHandle = e.RowHandle;
					gridView2.FocusedColumn = gridView2.VisibleColumns[0];
					gridView2.ShowEditor();
				}
				else if (num <= 0m)
				{
					MessageBox.Show("برجاء إدخال كمية أكبر من الصفر");
					gridView2.FocusedRowHandle = e.RowHandle;
					gridView2.FocusedColumn = gridView2.VisibleColumns[3];
					gridView2.ShowEditor();
				}
				else if (num2 <= 0m)
				{
					MessageBox.Show("برجاء إدخال نسبة مئوية أكبر من الصفر");
					gridView2.FocusedRowHandle = e.RowHandle;
					gridView2.FocusedColumn = gridView2.VisibleColumns[5];
					gridView2.ShowEditor();
				}
			}
		}
		Cal_FinalResultTotals();
	}

	private void gridView2_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
	{
		e.ExceptionMode = ExceptionMode.NoAction;
		gridControl2.DataSource = datatableManfc;
		gridView2.FocusedColumn = gridView2.VisibleColumns[0];
	}

	private void gridView2_RowCellClick(object sender, RowCellClickEventArgs e)
	{
	}

	private void gridView2_CellValueChanged(object sender, CellValueChangedEventArgs e)
	{
		int num = 0;
		decimal num2 = Convert.ToDecimal(gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue);
		decimal num3 = num2 / 100m;
		if (e.Column.FieldName == "كود_الصنف")
		{
			string text = Convert.ToString(e.Value);
			Item_Items item_Items = Pur.GetItems(text).FirstOrDefault();
			if (item_Items != null)
			{
				if (0 == 0)
				{
					object rowCellValue = gridView2.GetRowCellValue(e.RowHandle, "تم");
					string text2 = rowCellValue.ToString();
					if (text2 == "0")
					{
						if (item_Items != null)
						{
							if (item_Items.item_Type == 3)
							{
								MessageBox.Show("عفوا ،لايمكن عمل فاتورة شراء لاصناف مكونة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
								gridView2.FocusedRowHandle = e.RowHandle;
								gridView2.FocusedColumn = gridView2.VisibleColumns[-1];
								gridView2.ShowEditor();
							}
							else
							{
								DataRow dataRow = null;
								dataRow = datatableManfc.NewRow();
								dataRow["كود_الصنف"] = item_Items.Item_code;
								dataRow["أسم الصنف"] = item_Items.item_Name;
								dataRow["الوحدة"] = item_Items.Item_Unit.Name;
								dataRow["الكمية"] = 1.0;
								dataRow["النسبة"] = 100;
								dataRow["قيمة الربح"] = 0.0;
								dataRow["نسبة الربح"] = 0.0;
								dataRow["سعر البيع"] = item_Items.LastCost;
								dataRow["اجمالى البيع"] = Math.Round(Convert.ToDecimal(item_Items.LastCost), DecimalNum);
								dataRow["مسح"] = 0;
								dataRow["تم"] = 1;
								dataRow["أسم_الوحدة"] = "الصغرى";
								dataRow["سعر التكلفة"] = item_Items.LastCost;
								dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost), DecimalNum);
								dataRow["المخزن"] = Pur.GetStoreName();
								dataRow["رقم المخزن"] = Pur.GetStoreID();
								object rowCellValue2 = gridView2.GetRowCellValue(e.RowHandle, "ظ…");
								string text3 = rowCellValue2.ToString();
								if (text3 == "")
								{
									dataRow["ظ…"] = e.RowHandle + 1;
								}
								else
								{
									dataRow["ظ…"] = text3;
								}
								Profit(dataRow, "code");
								CheckFfistRowManfc();
								datatableManfc.Rows.Add(dataRow);
								insertnewrowManfc();
								GetTotalCostManfc();
								gridView2.FocusedRowHandle = e.RowHandle;
								gridView2.FocusedColumn = gridView2.VisibleColumns[-1];
								gridView2.ShowEditor();
							}
						}
						else
						{
							switch (MessageBox.Show("عفوا، كود الصنف غير موجود هل تريد إضافة الآن؟", "مستكشف البيانات ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
							{
							case DialogResult.Yes:
							{
								LoginDetails.ItemcodeNewItem = text;
								gridView2.DeleteRow(gridView2.FocusedRowHandle);
								insertnewrowManfc();
								FrmItems frmItems = new FrmItems();
								frmItems.TxtItemCode.Text = text.ToString();
								frmItems.TxtItemCode.Focus();
								frmItems.ShowDialog();
								break;
							}
							case DialogResult.No:
								gridView2.DeleteRow(gridView2.FocusedRowHandle);
								insertnewrowManfc();
								break;
							}
						}
					}
					else
					{
						foreach (DataRow row in datatableManfc.Rows)
						{
							string value = Convert.ToString(e.Value);
							string focusedRowCellDisplayText = gridView2.GetFocusedRowCellDisplayText("كود_الصنف");
							string focusedRowCellDisplayText2 = gridView2.GetFocusedRowCellDisplayText("الوحدة");
							string focusedRowCellDisplayText3 = gridView2.GetFocusedRowCellDisplayText("أسم_الوحدة");
							string focusedRowCellDisplayText4 = gridView2.GetFocusedRowCellDisplayText("ظ…");
							if (row["ظ…"].ToString() == focusedRowCellDisplayText4)
							{
								if (row["أسم_الوحدة"].ToString() == "الصغرى")
								{
									row["كود_الصنف"] = item_Items.Item_code;
									row["أسم الصنف"] = item_Items.item_Name;
									row["سعر التكلفة"] = item_Items.LastCost;
									row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(value)), DecimalNum);
									break;
								}
								if (row["أسم_الوحدة"].ToString() == "المتوسطة")
								{
									row["كود_الصنف"] = item_Items.Item_code;
									row["أسم الصنف"] = item_Items.item_Name;
									row["سعر التكلفة"] = item_Items.LastCost2;
									row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost2 * (decimal?)Convert.ToDecimal(value)), DecimalNum);
									break;
								}
								if (row["أسم_الوحدة"].ToString() == "الكبرى")
								{
									row["كود_الصنف"] = item_Items.Item_code;
									row["أسم الصنف"] = item_Items.item_Name;
									row["سعر التكلفة"] = item_Items.LastCost3;
									row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost3 * (decimal?)Convert.ToDecimal(value)), DecimalNum);
									break;
								}
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
				CodeItm = text;
				SearchItems();
				LoginDetails.ItemSearch = "";
			}
		}
		else if (e.Column.FieldName == "الكمية")
		{
			string value2 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText5 = gridView2.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText6 = gridView2.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText7 = gridView2.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText8 = gridView2.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText5 != "")
			{
				Item_Items item_Items2 = Pur.GetItems(focusedRowCellDisplayText5).FirstOrDefault();
				if (item_Items2 != null)
				{
					foreach (DataRow row2 in datatableManfc.Rows)
					{
						if (row2["ظ…"].ToString() == focusedRowCellDisplayText8)
						{
							decimal num4 = Convert.ToDecimal(row2["سعر التكلفة"].ToString());
							decimal num5 = Convert.ToDecimal(row2["سعر البيع"].ToString());
							if (row2["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num4 * Convert.ToDecimal(value2)), DecimalNum);
								row2["اجمالى البيع"] = Math.Round(Convert.ToDecimal(num5 * Convert.ToDecimal(value2)), DecimalNum);
								break;
							}
							if (row2["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num4 * Convert.ToDecimal(value2)), DecimalNum);
								row2["اجمالى البيع"] = Math.Round(Convert.ToDecimal(num5 * Convert.ToDecimal(value2)), DecimalNum);
								break;
							}
							if (row2["أسم_الوحدة"].ToString() == "الكبرى")
							{
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num4 * Convert.ToDecimal(value2)), DecimalNum);
								row2["اجمالى البيع"] = Math.Round(Convert.ToDecimal(num5 * Convert.ToDecimal(value2)), DecimalNum);
								break;
							}
						}
					}
					CheckFfistRowManfc();
					insertnewrowManfc();
					GetTotalCostManfc();
					gridView2.FocusedRowHandle = e.RowHandle;
					gridView2.FocusedColumn = gridView2.VisibleColumns[3];
					gridView2.ShowEditor();
				}
			}
		}
		else if (e.Column.FieldName == "سعر التكلفة")
		{
			string value3 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText9 = gridView2.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText10 = gridView2.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText11 = gridView2.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText12 = gridView2.GetFocusedRowCellDisplayText("الكمية");
			string focusedRowCellDisplayText13 = gridView2.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText9 != "")
			{
				Item_Items item_Items3 = Pur.GetItems(focusedRowCellDisplayText9).FirstOrDefault();
				if (item_Items3 != null)
				{
					foreach (DataRow row3 in datatableManfc.Rows)
					{
						if (row3["ظ…"].ToString() == focusedRowCellDisplayText13)
						{
							if (row3["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row3["سعر التكلفة"] = value3;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value3) * Convert.ToDecimal(focusedRowCellDisplayText12)), DecimalNum);
								break;
							}
							if (row3["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								row3["سعر التكلفة"] = value3;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value3) * Convert.ToDecimal(focusedRowCellDisplayText12)), DecimalNum);
								break;
							}
							if (row3["أسم_الوحدة"].ToString() == "الكبرى")
							{
								row3["سعر التكلفة"] = value3;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value3) * Convert.ToDecimal(focusedRowCellDisplayText12)), DecimalNum);
								break;
							}
						}
						Profit(row3, "price");
					}
					CheckFfistRowManfc();
					insertnewrowManfc();
					GetTotalCostManfc();
					gridView2.FocusedRowHandle = e.RowHandle;
					gridView2.FocusedColumn = gridView2.VisibleColumns[4];
					gridView2.ShowEditor();
				}
			}
		}
		else if (e.Column.FieldName == "نسبة الربح")
		{
			string text4 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText14 = gridView2.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText15 = gridView2.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText16 = gridView2.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText17 = gridView2.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText14 != "")
			{
				Item_Items item_Items4 = Pur.GetItems(focusedRowCellDisplayText14).FirstOrDefault();
				if (item_Items4 != null)
				{
					foreach (DataRow row4 in datatableManfc.Rows)
					{
						if (row4["ظ…"].ToString() == focusedRowCellDisplayText17)
						{
							row4["قيمة الربح"] = "0.00";
							Profit(row4, "نسبة الربح");
						}
					}
					CheckFfistRowManfc();
					insertnewrowManfc();
					GetTotalCostManfc();
					gridView2.FocusedRowHandle = e.RowHandle;
					gridView2.FocusedColumn = gridView2.VisibleColumns[3];
					gridView2.ShowEditor();
				}
			}
		}
		else if (e.Column.FieldName == "قيمة الربح")
		{
			string text5 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText18 = gridView2.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText19 = gridView2.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText20 = gridView2.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText21 = gridView2.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText18 != "")
			{
				Item_Items item_Items5 = Pur.GetItems(focusedRowCellDisplayText18).FirstOrDefault();
				if (item_Items5 != null)
				{
					foreach (DataRow row5 in datatableManfc.Rows)
					{
						if (row5["ظ…"].ToString() == focusedRowCellDisplayText21)
						{
							row5["نسبة الربح"] = "0.00";
							Profit(row5, "قيمة الربح");
						}
					}
					CheckFfistRowManfc();
					insertnewrowManfc();
					GetTotalCostManfc();
					gridView2.FocusedRowHandle = e.RowHandle;
					gridView2.FocusedColumn = gridView2.VisibleColumns[3];
					gridView2.ShowEditor();
				}
			}
		}
		else if (e.Column.FieldName == "سعر البيع")
		{
			string text6 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText22 = gridView2.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText23 = gridView2.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText24 = gridView2.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText25 = gridView2.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText22 != "")
			{
				Item_Items item_Items6 = Pur.GetItems(focusedRowCellDisplayText22).FirstOrDefault();
				if (item_Items6 != null)
				{
					foreach (DataRow row6 in datatableManfc.Rows)
					{
						if (row6["ظ…"].ToString() == focusedRowCellDisplayText25)
						{
							row6["قيمة الربح"] = Convert.ToDecimal(row6["سعر البيع"]) - Convert.ToDecimal(row6["سعر التكلفة"]);
							row6["نسبة الربح"] = Math.Round(Convert.ToDecimal(row6["قيمة الربح"]) / Convert.ToDecimal(row6["سعر التكلفة"]) * 100m, DecimalNum);
							row6["اجمالى البيع"] = Math.Round(Convert.ToDecimal(row6["سعر البيع"]) * Convert.ToDecimal(row6["الكمية"]), DecimalNum);
						}
					}
					CheckFfistRowManfc();
					insertnewrowManfc();
					GetTotalCostManfc();
					gridView2.FocusedRowHandle = e.RowHandle;
					gridView2.FocusedColumn = gridView2.VisibleColumns[3];
					gridView2.ShowEditor();
				}
			}
		}
		else
		{
			if (!(e.Column.FieldName == "النسبة"))
			{
				return;
			}
			string text7 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText26 = gridView2.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText27 = gridView2.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText28 = gridView2.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText29 = gridView2.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText26 != "")
			{
				Item_Items item_Items7 = Pur.GetItems(focusedRowCellDisplayText26).FirstOrDefault();
				if (item_Items7 != null)
				{
					foreach (DataRow row7 in datatableManfc.Rows)
					{
						if (row7["ظ…"].ToString() == focusedRowCellDisplayText29)
						{
							decimal num6 = Convert.ToDecimal(row7["النسبة"]);
							row7["الاجمالى"] = Math.Round(Convert.ToDecimal(num3 * num6), DecimalNum);
							row7["سعر التكلفة"] = Math.Round(Convert.ToDecimal(row7["الاجمالى"]) / Convert.ToDecimal(row7["الكمية"]), DecimalNum);
							decimal num7 = Convert.ToDecimal(row7["نسبة الربح"]) * Convert.ToDecimal(row7["سعر التكلفة"]) / 100m;
							row7["قيمة الربح"] = num7;
							row7["سعر البيع"] = Math.Round(Convert.ToDecimal(row7["سعر التكلفة"]) + num7, DecimalNum);
							row7["نسبة الربح"] = Math.Round(Convert.ToDecimal(row7["قيمة الربح"]) / Convert.ToDecimal(row7["سعر التكلفة"]) * 100m, DecimalNum);
							row7["اجمالى البيع"] = Math.Round(Convert.ToDecimal(row7["سعر البيع"]) * Convert.ToDecimal(row7["الكمية"]), DecimalNum);
						}
					}
					CheckFfistRowManfc();
					insertnewrowManfc();
					GetTotalCostManfc();
					gridView2.FocusedRowHandle = e.RowHandle;
					gridView2.FocusedColumn = gridView2.VisibleColumns[3];
					gridView2.ShowEditor();
				}
			}
		}
		ArrangeDatatableManfc();
		gridControl1.DataSource = datatable;
		gridControl2.DataSource = datatableManfc;
		Cal_FinalResultTotals();
	}

	private void gridView2_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
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

	private void gridContro2_ProcessGridKey(object sender, KeyEventArgs e)
	{
		GridControl gridControl = sender as GridControl;
		GridView gridView = gridControl.FocusedView as GridView;
		if (e.KeyData == Keys.Delete)
		{
			gridView.DeleteSelectedRows();
			e.Handled = true;
		}
	}

	private void gridView2_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
	{
		string focusedRowCellDisplayText = gridView2.GetFocusedRowCellDisplayText("كود_الصنف");
		string focusedRowCellDisplayText2 = gridView2.GetFocusedRowCellDisplayText("أسم_الوحدة");
		string StoreName = gridView2.GetFocusedRowCellDisplayText("المخزن");
		int num = (from x in db.Account_Stores
			where x.Store_Name == StoreName
			select x.ID).FirstOrDefault();
		CodeItm = focusedRowCellDisplayText;
	}

	private void gridControl2_ProcessGridKey(object sender, KeyEventArgs e)
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
		if (LoginDetails.Lang == "ar-EG")
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameAR1);
			gridControl2.MainView.SaveLayoutToXml(fileNameAR2);
		}
		else
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameEN1);
			gridControl2.MainView.SaveLayoutToXml(fileNameEN2);
		}
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
			FrmSearchManufacturing frmSearchManufacturing = new FrmSearchManufacturing();
			frmSearchManufacturing.ShowDialog();
			if (frmSearchManufacturing.CustCode <= 0)
			{
				return;
			}
			int custCode = frmSearchManufacturing.CustCode;
			Order_Manufacturing order_Manufacturing = Pur.GetPurchases(custCode).FirstOrDefault();
			datatable.Rows.Clear();
			datatableManfc.Rows.Clear();
			List<Order_ManufacturingDetailsItems> itemsPurchesDetailsItems = Pur.GetItemsPurchesDetailsItems(custCode);
			List<Order_ManufacturingDetailsNew> itemsPurchesDetailsNew = Pur.GetItemsPurchesDetailsNew(custCode);
			int num = 1;
			foreach (Order_ManufacturingDetailsItems item in itemsPurchesDetailsItems)
			{
				DataRow dataRow = null;
				int itemID = Convert.ToInt32(item.ItemID);
				Item_Items item_Items = Pur.GetItemsByitemid(itemID).FirstOrDefault();
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
				dataRow["الكمية"] = item.Quantity;
				dataRow["عدد الوحدة"] = item.QtyNumber;
				dataRow["سعر الوحدة"] = item.UnitPrice;
				dataRow["سعربعدالخصم"] = item.NetUnitPrice;
				dataRow["إجمالى_بعدالخصم"] = item.NetTotalPrice;
				dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
				dataRow["مسح"] = 0;
				datatable.Rows.Add(dataRow);
				num++;
			}
			gridControl1.DataSource = datatable;
			insertnewrow();
			int num2 = 1;
			foreach (Order_ManufacturingDetailsNew item2 in itemsPurchesDetailsNew)
			{
				DataRow dataRow2 = null;
				int itemID2 = Convert.ToInt32(item2.ItemID);
				Item_Items item_Items2 = Pur.GetItemsByitemid(itemID2).FirstOrDefault();
				dataRow2 = datatableManfc.NewRow();
				dataRow2["كود_الصنف"] = item_Items2.Item_code;
				dataRow2["أسم الصنف"] = item_Items2.item_Name;
				if (item2.ItemUnitType == "الصغرى")
				{
					dataRow2["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item2.ItemUnitID).FirstOrDefault().Name;
				}
				if (item2.ItemUnitType == "المتوسطة")
				{
					dataRow2["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item2.ItemUnitID).FirstOrDefault().Name;
				}
				if (item2.ItemUnitType == "الكبرى")
				{
					dataRow2["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item2.ItemUnitID).FirstOrDefault().Name;
				}
				dataRow2["أسم_الوحدة"] = item2.ItemUnitType;
				dataRow2["رقم المخزن"] = item2.StoreID;
				dataRow2["المخزن"] = (from x in db.Account_Stores
					where (int?)x.ID == item2.StoreID
					select x.Store_Name).FirstOrDefault();
				dataRow2["الكمية"] = item2.Quantity;
				dataRow2["النسبة"] = item2.Percentage;
				dataRow2["قيمة الربح"] = item2.AmountProfit;
				dataRow2["نسبة الربح"] = item2.PerProfit;
				dataRow2["سعر البيع"] = item2.SalePrice;
				dataRow2["اجمالى البيع"] = item2.TotalSalePrice;
				dataRow2["سعر التكلفة"] = item2.UnitPrice;
				dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(item2.TotalPrice), DecimalNum);
				dataRow2["مسح"] = 0;
				datatableManfc.Rows.Add(dataRow2);
				num2++;
			}
			gridControl2.DataSource = datatableManfc;
			insertnewrowManfc();
			PrintID = custCode;
			CBCostCenter.SelectedValue = order_Manufacturing.CostCentersID;
			TxtCode.Text = order_Manufacturing.ID.ToString();
			TxtDate.DateTime = Convert.ToDateTime(order_Manufacturing.Purchases_Date);
			TxtNote.Text = order_Manufacturing.Note;
			TxtNumNote.Text = order_Manufacturing.NoteNum;
			TxtTotals.Text = Convert.ToString(order_Manufacturing.TotalPrices);
			TxtVat.Text = Convert.ToString(order_Manufacturing.Tax);
			TxtTotalAll.Text = order_Manufacturing.Safy.ToString();
			TxtDiscount.Text = order_Manufacturing.DiscountNum.ToString();
			TxtTotalBeforDisc.Text = order_Manufacturing.TotalPrices_Discount.ToString();
			TxtVatBeforeDisc.Text = order_Manufacturing.Tax_Discount.ToString();
			TxtNet.Text = order_Manufacturing.Net.ToString();
			TxtNet22.Text = order_Manufacturing.Net.ToString();
			BtnSave.Enabled = false;
			BtnEdit.Enabled = true;
			BtnEdit.Text = "F6 تعديل";
			BtnDelete.Enabled = true;
			gridView1.OptionsBehavior.Editable = true;
			gridView2.OptionsBehavior.Editable = true;
			FiterDatatableItemsUpdate();
			insertnewrow();
			ArrangeDatatable();
			IsEnableTrue();
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void BtnManufacturingOrder_Click(object sender, EventArgs e)
	{
		try
		{
			FrmSearchManufacturingOrder frmSearchManufacturingOrder = new FrmSearchManufacturingOrder();
			frmSearchManufacturingOrder.ShowDialog();
			if (frmSearchManufacturingOrder.CustCode <= 0)
			{
				return;
			}
			NewFrm();
			int custCode = frmSearchManufacturingOrder.CustCode;
			Order_ManufacturingOrder order_ManufacturingOrder = Pur.GetPurchasesOrder(custCode).FirstOrDefault();
			datatable.Rows.Clear();
			List<Order_ManufacturingOrderDetails> itemsPurchesByID = Pur.GetItemsPurchesByID(custCode);
			int num = 1;
			foreach (Order_ManufacturingOrderDetails item in itemsPurchesByID)
			{
				DataRow dataRow = null;
				int itemID = Convert.ToInt32(item.ItemID);
				Item_Items item_Items = Pur.GetItemsByitemid(itemID).FirstOrDefault();
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
				dataRow["الكمية"] = item.Quantity;
				dataRow["عدد الوحدة"] = 0;
				dataRow["سعر الوحدة"] = item.UnitPrice;
				dataRow["سعربعدالخصم"] = item.NetUnitPrice;
				dataRow["إجمالى_بعدالخصم"] = item.NetTotalPrice;
				dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
				dataRow["مسح"] = 0;
				datatable.Rows.Add(dataRow);
				num++;
			}
			gridControl1.DataSource = datatable;
			insertnewrow();
			PrintID = custCode;
			CBCostCenter.SelectedValue = order_ManufacturingOrder.CostCentersID;
			TxtCode.Text = "";
			TxtDate.DateTime = DateTime.Now;
			TxtNote.Text = order_ManufacturingOrder.Note;
			TxtNumNote.Text = order_ManufacturingOrder.NoteNum;
			TxtTotals.Text = Convert.ToString(order_ManufacturingOrder.TotalPrices);
			TxtVat.Text = Convert.ToString(order_ManufacturingOrder.Tax);
			TxtTotalAll.Text = order_ManufacturingOrder.Safy.ToString();
			TxtDiscount.Text = order_ManufacturingOrder.DiscountNum.ToString();
			TxtTotalBeforDisc.Text = order_ManufacturingOrder.TotalPrices_Discount.ToString();
			TxtVatBeforeDisc.Text = order_ManufacturingOrder.Tax_Discount.ToString();
			TxtNet.Text = order_ManufacturingOrder.Net.ToString();
			TxtNet22.Text = order_ManufacturingOrder.Net.ToString();
			BtnSave.Enabled = true;
			BtnEdit.Enabled = false;
			BtnEdit.Text = "F6 تعديل";
			BtnDelete.Enabled = false;
			gridView1.OptionsBehavior.Editable = true;
			FiterDatatableItemsUpdate();
			insertnewrow();
			ArrangeDatatable();
			IsEnableTrue();
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void BtnPlus_Click(object sender, EventArgs e)
	{
		GetTotalManfactory();
	}

	public void BindCBCostCenter()
	{
		try
		{
			ClassProject classProject = new ClassProject();
			CBProject.DataSource = classProject.GetSearchAccountProjects();
			CBProject.DisplayMember = "ProjectName";
			CBProject.ValueMember = "ProjectID";
			if (CBProject.Items.Count > 0)
			{
				CBCostCenter.DataSource = Cost.GetCostCentersByProject(Convert.ToInt32(CBProject.SelectedValue));
				CBCostCenter.DisplayMember = "CostCentersName";
				CBCostCenter.ValueMember = "CostCentersID";
				CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
				CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
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

	public void CheckFfistRowManfc()
	{
		for (int num = datatableManfc.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = datatableManfc.Rows[num];
			if (dataRow["الكمية"].ToString() == "0" || dataRow["سعر التكلفة"].ToString() == "0")
			{
				dataRow.Delete();
				datatableManfc.AcceptChanges();
				break;
			}
		}
		gridControl1.DataSource = datatableManfc;
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

	public int GetTotalManfactory()
	{
		int result = 0;
		if (datatableManfc.Rows.Count <= 1)
		{
			MessageBox.Show("عفوا، يجب ادخال صنف واحد علي الاقل في التصنيع", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			gridControl2.Focus();
			return result;
		}
		if (datatable.Rows.Count <= 1)
		{
			MessageBox.Show("عفوا، يجب ادخال صنف واحد علي الاقل في الموادر الخام", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			gridControl1.Focus();
			return result;
		}
		object summaryValue = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
		decimal num = Convert.ToDecimal(summaryValue);
		object summaryValue2 = gridView2.Columns["النسبة"].SummaryItem.SummaryValue;
		decimal num2 = Convert.ToDecimal(summaryValue2);
		if (num2 != 100m)
		{
			MessageBox.Show("عفوا، يجب ان يكون اجمالي النسبة المئوية تساوي 100طŒ الرجاء مراجعة النسبة المئوية والمحاولة مرة أخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			gridControl2.Focus();
			gridView2.FocusedColumn = gridView2.VisibleColumns[5];
			gridView2.ShowEditor();
			return result;
		}
		CheckFfistRowManfc();
		decimal num3 = num / 100m;
		foreach (DataRow row in datatableManfc.Rows)
		{
			decimal num4 = Convert.ToDecimal(row["النسبة"]);
			row["الاجمالى"] = Math.Round(Convert.ToDecimal(num3 * num4), DecimalNum);
			decimal num5 = Convert.ToDecimal(row["الكمية"]);
			decimal num6 = Convert.ToDecimal(row["الاجمالى"]);
			row["سعر التكلفة"] = Convert.ToDecimal(row["الاجمالى"]) / Convert.ToDecimal(row["الكمية"]);
			row["قيمة الربح"] = Convert.ToDecimal(row["سعر البيع"]) - Convert.ToDecimal(row["سعر التكلفة"]);
			row["نسبة الربح"] = Math.Round(Convert.ToDecimal(row["قيمة الربح"]) / Convert.ToDecimal(row["سعر التكلفة"]) * 100m, DecimalNum);
			row["اجمالى البيع"] = Math.Round(Convert.ToDecimal(row["سعر البيع"]) * Convert.ToDecimal(row["الكمية"]), DecimalNum);
			datatableManfc.AcceptChanges();
		}
		gridControl1.DataSource = datatable;
		gridControl2.DataSource = datatableManfc;
		return 1;
	}

	public void GetTotalCost()
	{
	}

	public void GetTotalCostManfc()
	{
	}

	public void insertnewrow()
	{
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["سعر الوحدة"] = 0.0;
		dataRow["الكمية"] = 1;
		dataRow["عدد الوحدة"] = 0;
		dataRow["المخزن"] = Pur.GetStoreName();
		dataRow["رقم المخزن"] = Pur.GetStoreID();
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
	}

	public void insertnewrowManfc()
	{
		DataRow dataRow = null;
		dataRow = datatableManfc.NewRow();
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["سعر التكلفة"] = 0.0;
		dataRow["الكمية"] = 1;
		dataRow["النسبة"] = 0;
		dataRow["قيمة الربح"] = 0.0;
		dataRow["نسبة الربح"] = 0.0;
		dataRow["سعر البيع"] = 0.0;
		dataRow["اجمالى البيع"] = 0.0;
		dataRow["المخزن"] = Pur.GetStoreName();
		dataRow["رقم المخزن"] = Pur.GetStoreID();
		dataRow["تم"] = 0;
		datatableManfc.Rows.Add(dataRow);
		gridControl2.DataSource = datatableManfc;
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

	public void ArrangeDatatableManfc()
	{
		int num = 1;
		foreach (DataRow row in datatableManfc.Rows)
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
		datatable.Columns.Add("الكمية", typeof(decimal));
		datatable.Columns.Add("عدد الوحدة", typeof(decimal));
		datatable.Columns.Add("سعر الوحدة", typeof(decimal));
		datatable.Columns.Add("سعربعدالخصم", typeof(decimal));
		datatable.Columns.Add("نسبةالخصم", typeof(decimal));
		datatable.Columns.Add("إجمالى_بعدالخصم", typeof(decimal));
		datatable.Columns.Add("أسم_الوحدة", typeof(string));
		datatable.Columns.Add("الاجمالى", typeof(string));
		datatable.Columns.Add("حذف الصنف", typeof(int));
		datatable.Columns.Add("ظ…", typeof(int));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تم", typeof(int));
		dtCurrentTable.Columns.Add("كود_الصنف", typeof(string));
		dtCurrentTable.Columns.Add("أسم الصنف", typeof(string));
		dtCurrentTable.Columns.Add("الوحدة", typeof(string));
		dtCurrentTable.Columns.Add("المخزن", typeof(string));
		dtCurrentTable.Columns.Add("رقم المخزن", typeof(int));
		dtCurrentTable.Columns.Add("الكمية", typeof(decimal));
		dtCurrentTable.Columns.Add("عدد الوحدة", typeof(decimal));
		dtCurrentTable.Columns.Add("سعر الوحدة", typeof(decimal));
		dtCurrentTable.Columns.Add("سعربعدالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("نسبةالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("إجمالى_بعدالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("أسم_الوحدة", typeof(string));
		dtCurrentTable.Columns.Add("الاجمالى", typeof(string));
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
		dataRow["عدد الوحدة"] = 0;
		dataRow["سعر الوحدة"] = 0.0;
		dataRow["سعربعدالخصم"] = 0.0;
		dataRow["نسبةالخصم"] = 0.0;
		dataRow["إجمالى_بعدالخصم"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
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
		gridView1.Columns["إجمالى_بعدالخصم"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.###}", "0")
		});
		gridView1.Columns["الاجمالى"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.###}", "0")
		});
		GetTotalCost();
		gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
		base.KeyPreview = true;
		gridView1.Columns["الكمية"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["الكمية"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["سعر الوحدة"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["سعر الوحدة"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["الاجمالى"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["الاجمالى"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.FocusedRowHandle = 1;
		gridView1.FocusedColumn = gridView1.VisibleColumns[0];
		gridView1.ShowEditor();
	}

	public void SetInitialRowManfc()
	{
		datatableManfc.Columns.Add("كود_الصنف", typeof(string));
		datatableManfc.Columns.Add("أسم الصنف", typeof(string));
		datatableManfc.Columns.Add("الوحدة", typeof(string));
		datatableManfc.Columns.Add("المخزن", typeof(string));
		datatableManfc.Columns.Add("رقم المخزن", typeof(int));
		datatableManfc.Columns.Add("الكمية", typeof(decimal));
		datatableManfc.Columns.Add("النسبة", typeof(decimal));
		datatableManfc.Columns.Add("قيمة الربح", typeof(decimal));
		datatableManfc.Columns.Add("نسبة الربح", typeof(decimal));
		datatableManfc.Columns.Add("سعر البيع", typeof(decimal));
		datatableManfc.Columns.Add("اجمالى البيع", typeof(decimal));
		datatableManfc.Columns.Add("سعر التكلفة", typeof(decimal));
		datatableManfc.Columns.Add("أسم_الوحدة", typeof(string));
		datatableManfc.Columns.Add("الاجمالى", typeof(string));
		datatableManfc.Columns.Add("حذف الصنف", typeof(int));
		datatableManfc.Columns.Add("ظ…", typeof(int));
		datatableManfc.Columns.Add("مسح", typeof(int));
		datatableManfc.Columns.Add("تم", typeof(int));
		dtCurrentTableManfc.Columns.Add("كود_الصنف", typeof(string));
		dtCurrentTableManfc.Columns.Add("أسم الصنف", typeof(string));
		dtCurrentTableManfc.Columns.Add("الوحدة", typeof(string));
		dtCurrentTableManfc.Columns.Add("المخزن", typeof(string));
		dtCurrentTableManfc.Columns.Add("رقم المخزن", typeof(int));
		dtCurrentTableManfc.Columns.Add("الكمية", typeof(decimal));
		dtCurrentTableManfc.Columns.Add("النسبة", typeof(decimal));
		dtCurrentTableManfc.Columns.Add("سعر الوحدة", typeof(decimal));
		dtCurrentTableManfc.Columns.Add("أسم_الوحدة", typeof(string));
		dtCurrentTableManfc.Columns.Add("الاجمالى", typeof(string));
		dtCurrentTableManfc.Columns.Add("حذف الصنف", typeof(int));
		dtCurrentTableManfc.Columns.Add("ظ…", typeof(int));
		dtCurrentTableManfc.Columns.Add("مسح", typeof(int));
		dtCurrentTableManfc.Columns.Add("تم", typeof(int));
		DataRow dataRow = null;
		dataRow = datatableManfc.NewRow();
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["أسم_الوحدة"] = "الصغرى";
		dataRow["الكمية"] = 0;
		dataRow["النسبة"] = 100;
		dataRow["قيمة الربح"] = 0.0;
		dataRow["نسبة الربح"] = 0.0;
		dataRow["سعر البيع"] = 0.0;
		dataRow["اجمالى البيع"] = 0.0;
		dataRow["سعر التكلفة"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
		dataRow["المخزن"] = Pur.GetStoreName();
		dataRow["مسح"] = 0;
		dataRow["تم"] = 0;
		datatableManfc.Rows.Add(dataRow);
		gridControl2.DataSource = datatableManfc;
		gridView2.ValidateRow += gridView2_ValidateRow;
		gridView2.CellValueChanged += gridView2_CellValueChanged;
		gridControl1.ProcessGridKey += gridControl2_ProcessGridKey;
		gridView2.RowCellClick += gridView2_RowCellClick;
		gridView2.FocusedRowChanged += gridView2_FocusedRowChanged;
		gridView2.InvalidRowException += gridView2_InvalidRowException;
		gridView2.CustomDrawFooterCell += gridView2_CustomDrawFooterCell;
		gridView2.Columns["الاجمالى"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.###}", "0")
		});
		gridView2.Columns["النسبة"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.###}", "0")
		});
		gridView2.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
		base.KeyPreview = true;
		gridView2.Columns["الكمية"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView2.Columns["الكمية"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView2.Columns["سعر التكلفة"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView2.Columns["سعر التكلفة"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView2.Columns["الاجمالى"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView2.Columns["الاجمالى"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView2.Columns["النسبة"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView2.Columns["النسبة"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView2.FocusedRowHandle = 1;
		gridView2.FocusedColumn = gridView2.VisibleColumns[0];
		gridView2.ShowEditor();
	}

	public decimal IsItemPriceZero()
	{
		decimal num = 1m;
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["كود_الصنف"]);
			if (Convert.ToDecimal(row["سعر الوحدة"]) <= 0m && text != "")
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ الفاتورة لوجود اصناف سعر الوحدة تساوي صفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				num = 0m;
				break;
			}
		}
		foreach (DataRow row2 in datatable.Rows)
		{
			string text2 = Convert.ToString(row2["كود_الصنف"]);
			if (!(text2 != string.Empty))
			{
				continue;
			}
			string code = row2["كود_الصنف"].ToString();
			int itmCodeType = ORD.GetItmCodeType(code);
			if (itmCodeType != 2)
			{
				string value = Convert.ToString(row2["سعر الوحدة"]);
				string text3 = Convert.ToString(row2["الوحدة"]);
				if (string.IsNullOrWhiteSpace(value))
				{
					row2["سعر الوحدة"] = 0;
				}
				decimal num2 = Convert.ToDecimal(row2["الكمية"]);
				string type = Convert.ToString(row2["أسم_الوحدة"]);
				int storeIdByName = ORD.GetStoreIdByName(row2["المخزن"].ToString());
				int itemID = Convert.ToInt32(ORD.GetItmID(text2));
				num = ORD.GetQuantityPermission(itemID, storeIdByName, type);
				if (LoginDetails.IsQuantityMinZero)
				{
					num = 1m;
					break;
				}
				if (num <= 0m)
				{
					MessageBox.Show("نعتذر، لا يوجد كمية كافية من الصنف ( " + text3 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					num = 0m;
					break;
				}
				if (num2 > num)
				{
					MessageBox.Show("نعتذر، الكمية المطلوبة اكبر من الكمية الموجودة  ( " + text3 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					num = 0m;
					break;
				}
			}
		}
		return num;
	}

	public int IsItemPriceZeroManfc()
	{
		int result = 1;
		foreach (DataRow row in datatableManfc.Rows)
		{
			string text = Convert.ToString(row["كود_الصنف"]);
			if (Convert.ToDecimal(row["سعر التكلفة"]) <= 0m && text != "")
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ الفاتورة لوجود اصناف سعر التكلفة تساوي صفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
			if (datatable.Rows.Count < 1)
			{
				TxtVat.Text = "0.00";
				TxtNet.Text = "0.00";
				TxtNet22.Text = "0.00";
				TxtTotals.Text = "0.00";
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtNumNote.Text = "";
				TxtTotalBeforDisc.Text = "0.00";
				TxtVatBeforeDisc.Text = "0.00";
				TxtTotalAll.Text = "0.00";
				return;
			}
			foreach (DataRow row in datatable.Rows)
			{
				if (row["أسم_الوحدة"].ToString() != "")
				{
					row["سعربعدالخصم"] = Convert.ToDecimal(row["سعر الوحدة"].ToString());
					row["إجمالى_بعدالخصم"] = Convert.ToDecimal(row["سعربعدالخصم"].ToString()) * Convert.ToDecimal(row["الكمية"].ToString());
				}
			}
			object summaryValue = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
			TxtTotals.Text = Convert.ToString(summaryValue);
			object summaryValue2 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
			TxtTotalBeforDisc.Text = Convert.ToString(summaryValue2);
			TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
			TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
			TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
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
				object summaryValue = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
				TxtTotals.Text = Convert.ToString(summaryValue);
				object summaryValue2 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
				TxtVat.Text = Convert.ToString(summaryValue2);
				object summaryValue3 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
				TxtTotalBeforDisc.Text = Convert.ToString(summaryValue3);
				object summaryValue4 = gridView1.Columns["الضريبه_بعد_الخصم"].SummaryItem.SummaryValue;
				TxtVatBeforeDisc.Text = Convert.ToString(summaryValue4);
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), 2) + Math.Round(Convert.ToDecimal(TxtVat.Text), 2)).ToString();
				TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), 2) + Math.Round(Convert.ToDecimal(TxtVat.Text), 2)).ToString();
				foreach (DataRow row in datatable.Rows)
				{
					if (row["أسم_الوحدة"].ToString() != "")
					{
						row["سعربعدالخصم"] = row["سعر الوحدة"];
						row["إجمالى_بعدالخصم"] = row["الاجمالى"];
						row["الضريبه_بعد_الخصم"] = row["الضربية"];
					}
				}
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), 2).ToString();
				return;
			}
			decimal value = Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text);
			txtdicoutprege.Text = Math.Round(Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), 2) * 100m) / Math.Round(Convert.ToDecimal(value), 2), 2).ToString();
			object summaryValue5 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
			TxtTotals.Text = Convert.ToString(summaryValue5);
			object summaryValue6 = gridView1.Columns["الضربية"].SummaryItem.SummaryValue;
			TxtVat.Text = Convert.ToString(summaryValue6);
			object summaryValue7 = gridView1.Columns["إجمالى_بعدالخصم"].SummaryItem.SummaryValue;
			TxtTotalBeforDisc.Text = Convert.ToString(summaryValue7);
			object summaryValue8 = gridView1.Columns["الضريبه_بعد_الخصم"].SummaryItem.SummaryValue;
			TxtVatBeforeDisc.Text = Convert.ToString(summaryValue8);
			TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), 2) + Math.Round(Convert.ToDecimal(TxtVat.Text), 2) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), 2)).ToString();
			TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), 2) + Math.Round(Convert.ToDecimal(TxtVat.Text), 2) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), 2)).ToString();
			TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), 2).ToString();
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
		datatableManfc.Rows.Clear();
		dtCurrentTableManfc.Rows.Clear();
		gridControl1.DataSource = datatable;
		gridControl2.DataSource = datatableManfc;
		GetTotalCost();
		TxtNote.Text = "";
		TxtDiscount.Text = "";
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtNumNote.Text = "";
		TxtTotalBeforDisc.Text = "0.00";
		TxtVatBeforeDisc.Text = "0.00";
		TxtTotalAll.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtNet22.Text = "0.00";
		TxtDate.DateTime = DateTime.Now;
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
		TxtDiscount.Enabled = true;
		TxtNote.Enabled = true;
		TxtDate.Enabled = true;
	}

	private void NewFrm()
	{
		datatable.Rows.Clear();
		dtCurrentTable.Rows.Clear();
		datatableManfc.Rows.Clear();
		dtCurrentTableManfc.Rows.Clear();
		gridControl1.Enabled = true;
		gridControl2.Enabled = true;
		gridView1.OptionsBehavior.Editable = true;
		gridView2.OptionsBehavior.Editable = true;
		Clear();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = "";
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["الكمية"] = 0.0;
		dataRow["عدد الوحدة"] = 0;
		dataRow["سعر الوحدة"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		DataRow dataRow2 = null;
		dataRow2 = datatableManfc.NewRow();
		dataRow2["كود_الصنف"] = "";
		dataRow2["أسم الصنف"] = string.Empty;
		dataRow2["الوحدة"] = string.Empty;
		dataRow2["الكمية"] = 0.0;
		dataRow2["النسبة"] = 100;
		dataRow2["قيمة الربح"] = 0.0;
		dataRow2["نسبة الربح"] = 0.0;
		dataRow2["سعر البيع"] = 0.0;
		dataRow2["اجمالى البيع"] = 0.0;
		dataRow2["سعر التكلفة"] = 0.0;
		dataRow2["الاجمالى"] = 0.0;
		dataRow2["تم"] = 0;
		datatableManfc.Rows.Add(dataRow2);
		IsEnableTrue();
		BindCBCostCenter();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 119))
		{
			int num = 0;
			if (datatable.Rows.Count > 1)
			{
				decimal num2 = IsItemPriceZero();
				if (num2 == 0m)
				{
					return;
				}
				GetTotalCost();
				Cal_FinalResultTotals();
				if (GetTotalManfactory() == 0)
				{
					return;
				}
				FiterDatatableItemsUpdate();
				if (CBCostCenter.SelectedValue == null)
				{
					CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
					CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
				}
				num = Pur.InsertPurch(out var Code, TxtDate.DateTime, Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), TxtNote.Text, TxtNumNote.Text, Math.Round(Convert.ToDecimal(TxtTotals.Text), 2), Math.Round(Convert.ToDecimal(TxtVat.Text), 2), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), 2), Math.Round(Convert.ToDecimal(TxtDiscount.Text), 2), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), 2), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), 2), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), 2), Math.Round(Convert.ToDecimal(TxtNet.Text), 2));
				Pur.InsertItems(datatable, num);
				Pur.InsertItemsNew(datatableManfc, num);
				gridControl1.DataSource = datatable;
				gridControl2.DataSource = datatableManfc;
				gridView1.OptionsBehavior.Editable = false;
				gridView2.OptionsBehavior.Editable = false;
				if (num > 0)
				{
					PrintID = num;
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					IsEnableFalse();
					TxtCode.Text = Code.ToString();
					DialogResult dialogResult = MessageBox.Show("تم حفظ بيانات فاتورة طلب أمر المواد بنجاح ", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					if (dialogResult != DialogResult.OK)
					{
					}
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات فاتورة طلب أمر المواد رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل صنف واحد على الاقل", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 119))
		{
			if (TxtDate.ReadOnly)
			{
				IsEnableTrue();
				BtnEdit.Text = "F5 حفظ";
				insertnewrow();
				insertnewrowManfc();
				gridView1.OptionsBehavior.Editable = true;
				gridView2.OptionsBehavior.Editable = true;
				return;
			}
			int num = 0;
			if (datatable.Rows.Count <= 1)
			{
				return;
			}
			decimal num2 = IsItemPriceZero();
			if (num2 == 0m)
			{
				return;
			}
			ArrangeDatatable();
			Cal_FinalResultTotals();
			GetTotalCost();
			if (GetTotalManfactory() == 0)
			{
				return;
			}
			FiterDatatableItemsUpdate();
			if (CBCostCenter.SelectedValue == null)
			{
				CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
				CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
			}
			num = Pur.UpdatePurch(Convert.ToInt32(TxtCode.Text), TxtDate.DateTime, Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), TxtNote.Text, TxtNumNote.Text, Math.Round(Convert.ToDecimal(TxtTotals.Text), 2), Math.Round(Convert.ToDecimal(TxtVat.Text), 2), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), 2), Math.Round(Convert.ToDecimal(TxtDiscount.Text), 2), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), 2), Math.Round(Convert.ToDecimal(TxtVatBeforeDisc.Text), 2), Math.Round(Convert.ToDecimal(TxtTotalBeforDisc.Text), 2), Math.Round(Convert.ToDecimal(TxtNet.Text), 2));
			Pur.deleteItemsByPurcahesId(num);
			Pur.InsertItems(datatable, num);
			Pur.InsertItemsNew(datatableManfc, num);
			if (num > 0)
			{
				PrintID = num;
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
				IsEnableFalse();
				BtnEdit.Text = "Edit تعديل";
				gridView1.OptionsBehavior.Editable = false;
				gridView2.OptionsBehavior.Editable = false;
				DialogResult dialogResult = MessageBox.Show("تم تعديل فاتورة طلب أمر المواد بنجاح ", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				if (dialogResult != DialogResult.OK)
				{
				}
			}
			else
			{
				MessageBox.Show("تم حفظ بيانات فاتورة طلب أمر المواد رقم(" + TxtCode.Text + ") بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 119))
		{
			int num = 0;
			if (TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة طلب أمر المواد رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = Pur.Delete(Convert.ToInt32(TxtCode.Text), datatable);
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات فاتورة طلب أمر المواد رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						gridView1.OptionsBehavior.Editable = true;
						IsEnableTrue();
						BtnDelete.Enabled = false;
						BtnEdit.Enabled = false;
						BtnSave.Enabled = true;
						insertnewrow();
						PrintID = 0;
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات فاتورة طلب أمر المواد رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة طلب أمر المواد بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		TxtNote.ReadOnly = false;
		TxtDate.ReadOnly = false;
		TxtNumNote.ReadOnly = false;
		CBCostCenter.Enabled = true;
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = true;
		TxtNote.ReadOnly = true;
		TxtDate.ReadOnly = true;
		TxtNumNote.ReadOnly = true;
		CBCostCenter.Enabled = false;
		TxtDiscount.BackColor = Color.White;
		TxtNote.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
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
				foreach (string item in LoginDetails.ItemSearchAll2)
				{
					string code = item.ToString();
					Item_Items item_Items = Pur.GetItems(code).FirstOrDefault();
					if (item_Items != null)
					{
						DataRow dataRow = null;
						dataRow = datatable.NewRow();
						dataRow["كود_الصنف"] = item_Items.Item_code;
						dataRow["أسم الصنف"] = item_Items.item_Name;
						dataRow["الوحدة"] = item_Items.Item_Unit.Name;
						dataRow["الكمية"] = 1;
						dataRow["عدد الوحدة"] = 0;
						dataRow["المخزن"] = Pur.GetStoreName();
						dataRow["رقم المخزن"] = Pur.GetStoreID();
						dataRow["أسم_الوحدة"] = "الصغرى";
						dataRow["سعر الوحدة"] = Math.Round(Convert.ToDecimal(item_Items.LastCost), DecimalNum);
						dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost), DecimalNum);
						object obj = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
						int num = Convert.ToInt32(obj.ToString());
						dataRow["ظ…"] = num + 1;
						datatable.Rows.Add(dataRow);
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
			gridControl1.DataSource = datatable;
			gridControl2.DataSource = datatableManfc;
			Cal_FinalResultTotals();
		}
		catch (Exception)
		{
		}
	}

	public void SearchItemsManfc()
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
					Item_Items item_Items = Pur.GetItems(code).FirstOrDefault();
					if (item_Items != null)
					{
						DataRow dataRow = null;
						dataRow = datatableManfc.NewRow();
						dataRow["كود_الصنف"] = item_Items.Item_code;
						dataRow["أسم الصنف"] = item_Items.item_Name;
						dataRow["الوحدة"] = item_Items.Item_Unit.Name;
						dataRow["الكمية"] = 1;
						dataRow["قيمة الربح"] = 0.0;
						dataRow["نسبة الربح"] = 0.0;
						dataRow["سعر البيع"] = Math.Round(Convert.ToDecimal(item_Items.LastCost), DecimalNum);
						dataRow["اجمالى البيع"] = Math.Round(Convert.ToDecimal(item_Items.LastCost), DecimalNum);
						dataRow["النسبة"] = 100;
						dataRow["المخزن"] = Pur.GetStoreName();
						dataRow["رقم المخزن"] = Pur.GetStoreID();
						dataRow["أسم_الوحدة"] = "الصغرى";
						dataRow["سعر التكلفة"] = Math.Round(Convert.ToDecimal(item_Items.LastCost), DecimalNum);
						dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost), DecimalNum);
						object obj = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
						int num = Convert.ToInt32(obj.ToString());
						dataRow["ظ…"] = num + 1;
						datatableManfc.Rows.Add(dataRow);
					}
					IsShow = 1;
				}
				CheckFfistRowManfc();
				insertnewrowManfc();
				GetTotalCostManfc();
				ArrangeDatatableManfc();
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
			gridControl1.DataSource = datatable;
			gridControl2.DataSource = datatableManfc;
			Cal_FinalResultTotals();
		}
		catch (Exception)
		{
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
		TxtDiscount.DecimalDigits = DecimalNum;
		txtdicoutprege.DecimalDigits = DecimalNum;
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
							if (row["ظ…"].ToString() == Convert.ToString(num))
							{
								row["الوحدة"] = item_Items.Item_Unit.Name;
								row["أسم_الوحدة"] = "الصغرى";
								row["المخزن"] = Pur.GetStoreName();
								row["رقم المخزن"] = Pur.GetStoreID();
								row["الكمية"] = 1;
								row["سعر الوحدة"] = item_Items.LastCost;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToInt32(1)), DecimalNum);
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
					Item_Items item_Items2 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items2 != null)
					{
						int num3 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row2 in datatable.Rows)
						{
							if (row2["ظ…"].ToString() == Convert.ToString(num3))
							{
								row2["الوحدة"] = item_Items2.Item_Unit1.Name;
								row2["أسم_الوحدة"] = "المتوسطة";
								row2["الكمية"] = 1;
								row2["المخزن"] = Pur.GetStoreName();
								row2["رقم المخزن"] = Pur.GetStoreID();
								row2["سعر الوحدة"] = item_Items2.LastCost2;
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items2.LastCost2 * (decimal?)Convert.ToInt32(1)), DecimalNum);
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
					Item_Items item_Items3 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items3 != null)
					{
						int num5 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row3 in datatable.Rows)
						{
							if (row3["ظ…"].ToString() == Convert.ToString(num5))
							{
								row3["الوحدة"] = item_Items3.Item_Unit2.Name;
								row3["أسم_الوحدة"] = "الكبرى";
								row3["الكمية"] = 1;
								row3["المخزن"] = Pur.GetStoreName();
								row3["رقم المخزن"] = Pur.GetStoreID();
								row3["سعر الوحدة"] = item_Items3.LastCost3;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items3.LastCost3 * (decimal?)Convert.ToInt32(1)), DecimalNum);
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
						if (row["ظ…"].ToString() == Convert.ToString(num))
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

	private void repositoryItemButtonEdit11_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchItemsManfc();
	}

	private void repositoryItemButtonEdit12_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView2.GetDataSourceRowIndex(gridView2.FocusedRowHandle) + 1;
		foreach (DataRow row in datatableManfc.Rows)
		{
			if (row["ظ…"].ToString() == Convert.ToString(num))
			{
				if (row["كود_الصنف"].ToString() != "")
				{
					row.Delete();
					datatableManfc.AcceptChanges();
					gridView2.FocusedColumn = gridView2.VisibleColumns[0];
					gridView2.ShowEditor();
				}
				else
				{
					gridView2.FocusedColumn = gridView2.VisibleColumns[0];
					gridView2.ShowEditor();
				}
				break;
			}
		}
		ArrangeDatatableManfc();
		gridControl2.DataSource = datatableManfc;
		Cal_FinalResultTotals();
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		try
		{
			base.KeyPreview = true;
			gridControl1.ForceInitialize();
			if (LoginDetails.Lang == "ar-EG")
			{
				gridControl1.MainView.RestoreLayoutFromXml(fileNameAR1);
				gridControl2.MainView.RestoreLayoutFromXml(fileNameAR2);
			}
			else
			{
				gridControl1.MainView.RestoreLayoutFromXml(fileNameEN1);
				gridControl2.MainView.RestoreLayoutFromXml(fileNameEN2);
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
						if (row["ظ…"].ToString() == Convert.ToString(num))
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

	private void TxtNet_TextChanged(object sender, EventArgs e)
	{
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

	private void repositoryItemButtonEdit8_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView2.GetDataSourceRowIndex(gridView2.FocusedRowHandle) + 1;
		foreach (DataRow row in datatableManfc.Rows)
		{
			if (row["ظ…"].ToString() == Convert.ToString(num))
			{
				if (row["كود_الصنف"].ToString() != "")
				{
					row.Delete();
					datatableManfc.AcceptChanges();
					gridView2.FocusedColumn = gridView2.VisibleColumns[0];
					gridView2.ShowEditor();
				}
				else
				{
					gridView2.FocusedColumn = gridView2.VisibleColumns[0];
					gridView2.ShowEditor();
				}
				break;
			}
		}
		ArrangeDatatableManfc();
		gridControl2.DataSource = datatableManfc;
		Cal_FinalResultTotals();
	}

	private void FrmManufacturing_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameAR1);
			gridControl2.MainView.SaveLayoutToXml(fileNameAR2);
		}
		else
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameEN1);
			gridControl2.MainView.SaveLayoutToXml(fileNameEN2);
		}
	}

	private void repositoryItemButtonEdit9_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string Itemocde = Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "كود_الصنف"));
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
				int num = gridView2.GetDataSourceRowIndex(gridView2.FocusedRowHandle) + 1;
				{
					foreach (DataRow row in datatableManfc.Rows)
					{
						if (row["ظ…"].ToString() == Convert.ToString(num))
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
			int num2 = gridView2.GetDataSourceRowIndex(gridView2.FocusedRowHandle) + 1;
			string value = (from x in db.Account_Stores
				where x.ID == StoreID
				select x.Store_Name).FirstOrDefault();
			foreach (DataRow row2 in datatableManfc.Rows)
			{
				if (row2["ظ…"].ToString() == Convert.ToString(num2))
				{
					row2["المخزن"] = value;
					row2["رقم المخزن"] = StoreID;
				}
			}
			gridView2.FocusedRowHandle = gridView2.FocusedRowHandle;
			gridView2.FocusedColumn = gridView2.VisibleColumns[5];
			gridView2.ShowEditor();
			GetTotalCost();
			frmSearchTransfer.Close();
		}
		else
		{
			MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف المخازن", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void repositoryItemButtonEdit7_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string text = Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "كود_الصنف"));
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
						int num = gridView2.GetDataSourceRowIndex(gridView2.FocusedRowHandle) + 1;
						foreach (DataRow row in datatable.Rows)
						{
							if (row["ظ…"].ToString() == Convert.ToString(num))
							{
								row["الوحدة"] = item_Items.Item_Unit.Name;
								row["أسم_الوحدة"] = "الصغرى";
								row["المخزن"] = Pur.GetStoreName();
								row["رقم المخزن"] = Pur.GetStoreID();
								row["الكمية"] = 1;
								row["سعر الوحدة"] = item_Items.LastCost;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToInt32(1)), DecimalNum);
							}
						}
						GetTotalCost();
						string StoreName = gridView2.GetFocusedRowCellDisplayText("المخزن");
						int num2 = (from x in db.Account_Stores
							where x.Store_Name == StoreName
							select x.ID).FirstOrDefault();
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "المتوسطة")
				{
					Item_Items item_Items2 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items2 != null)
					{
						int num3 = gridView2.GetDataSourceRowIndex(gridView2.FocusedRowHandle) + 1;
						foreach (DataRow row2 in datatable.Rows)
						{
							if (row2["ظ…"].ToString() == Convert.ToString(num3))
							{
								row2["الوحدة"] = item_Items2.Item_Unit1.Name;
								row2["أسم_الوحدة"] = "المتوسطة";
								row2["الكمية"] = 1;
								row2["المخزن"] = Pur.GetStoreName();
								row2["رقم المخزن"] = Pur.GetStoreID();
								row2["سعر الوحدة"] = item_Items2.LastCost2;
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items2.LastCost2 * (decimal?)Convert.ToInt32(1)), DecimalNum);
							}
						}
						GetTotalCost();
						string StoreName2 = gridView2.GetFocusedRowCellDisplayText("المخزن");
						int num4 = (from x in db.Account_Stores
							where x.Store_Name == StoreName2
							select x.ID).FirstOrDefault();
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "الكبرى")
				{
					Item_Items item_Items3 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items3 != null)
					{
						int num5 = gridView2.GetDataSourceRowIndex(gridView2.FocusedRowHandle) + 1;
						foreach (DataRow row3 in datatable.Rows)
						{
							if (row3["ظ…"].ToString() == Convert.ToString(num5))
							{
								row3["الوحدة"] = item_Items3.Item_Unit2.Name;
								row3["أسم_الوحدة"] = "الكبرى";
								row3["الكمية"] = 1;
								row3["المخزن"] = Pur.GetStoreName();
								row3["رقم المخزن"] = Pur.GetStoreID();
								row3["سعر الوحدة"] = item_Items3.LastCost3;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items3.LastCost3 * (decimal?)Convert.ToInt32(1)), DecimalNum);
							}
						}
						GetTotalCost();
						string StoreName3 = gridView2.GetFocusedRowCellDisplayText("المخزن");
						int num6 = (from x in db.Account_Stores
							where x.Store_Name == StoreName3
							select x.ID).FirstOrDefault();
						frmSearchUnit.Close();
					}
				}
				frmSearchUnit.Close();
			}
			gridView2.FocusedRowHandle = gridView2.FocusedRowHandle;
			gridView2.FocusedColumn = gridView2.VisibleColumns[3];
			gridView2.ShowEditor();
			Cal_FinalResultTotals();
			frmSearchUnit.Close();
		}
		else
		{
			MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف الوحدات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void MenuExit_Click(object sender, EventArgs e)
	{
		Close();
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
				CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
			}
		}
		catch
		{
		}
	}

	private void TxtType_KeyPress_1(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void Profit(DataRow dr, string ColumnField)
	{
		decimal num = Convert.ToDecimal(dr["نسبة الربح"]);
		if (ColumnField == "نسبة الربح" || num != 0m)
		{
			if (num <= 100m && num != 0m)
			{
				decimal num2 = Convert.ToDecimal(dr["نسبة الربح"]) * Convert.ToDecimal(dr["سعر التكلفة"]) / 100m;
				dr["قيمة الربح"] = num2;
				dr["سعر البيع"] = Math.Round(Convert.ToDecimal(dr["سعر التكلفة"]) + num2, DecimalNum);
				dr["اجمالى البيع"] = Math.Round(num2 * Convert.ToDecimal(dr["الكمية"]), DecimalNum);
			}
			else
			{
				dr["نسبة الربح"] = "0.00";
				dr["قيمة الربح"] = "0.00";
				dr["اجمالى البيع"] = dr["الاجمالى"];
				dr["سعر البيع"] = dr["سعر التكلفة"];
			}
		}
		else if (Convert.ToDecimal(dr["قيمة الربح"]) != 0m)
		{
			decimal num3 = Convert.ToDecimal(dr["سعر التكلفة"]);
			decimal num4 = Math.Round(Convert.ToDecimal(dr["سعر التكلفة"]) + Convert.ToDecimal(dr["قيمة الربح"]), DecimalNum);
			dr["سعر البيع"] = num4.ToString();
			dr["اجمالى البيع"] = Math.Round(num4 * Convert.ToDecimal(dr["الكمية"]), DecimalNum);
			decimal num5 = Math.Round(Convert.ToDecimal(dr["قيمة الربح"]) / Convert.ToDecimal(dr["سعر التكلفة"]) * 100m, DecimalNum);
			dr["نسبة الربح"] = num5;
		}
		else
		{
			dr["نسبة الربح"] = "0.00";
			dr["قيمة الربح"] = "0.00";
			dr["اجمالى البيع"] = dr["الاجمالى"];
			dr["سعر البيع"] = dr["سعر التكلفة"];
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.OrderManufacturing.FrmManufacturing));
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions9 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions10 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions11 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions12 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance12 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered12 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed12 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled12 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions13 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance13 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered13 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed13 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled13 = new DevExpress.Utils.SerializableAppearanceObject();
		this.repositoryItemButtonEdit10 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
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
		this.gridControl2 = new DevExpress.XtraGrid.GridControl();
		this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit11 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit12 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.cardView2 = new DevExpress.XtraGrid.Views.Card.CardView();
		this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.RecoveredMenuItemFix8 = new System.Windows.Forms.ToolStripMenuItem();
		this.label26 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label24 = new System.Windows.Forms.Label();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label39 = new System.Windows.Forms.Label();
		this.CBProject = new System.Windows.Forms.ComboBox();
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
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnPlus = new DevExpress.XtraEditors.SimpleButton();
		this.BtnManufacturingOrder = new DevExpress.XtraEditors.SimpleButton();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
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
		this.label6 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit10).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit9).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit11).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit12).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit8).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView2).BeginInit();
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
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.repositoryItemButtonEdit10, "repositoryItemButtonEdit10");
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.repositoryItemButtonEdit10.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit10.Buttons"), resources.GetString("repositoryItemButtonEdit10.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit10.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit10.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit10.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit10.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEdit10.Buttons6"), resources.GetObject("repositoryItemButtonEdit10.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit10.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit10.Buttons9"))
		});
		this.repositoryItemButtonEdit10.Name = "repositoryItemButtonEdit10";
		this.repositoryItemButtonEdit7.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit7, "repositoryItemButtonEdit7");
		editorButtonImageOptions2.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		editorButtonImageOptions2.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit7.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit7.Buttons"), resources.GetString("repositoryItemButtonEdit7.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit7.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEdit7.Buttons6"), resources.GetObject("repositoryItemButtonEdit7.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit7.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit7.Buttons9"))
		});
		this.repositoryItemButtonEdit7.Name = "repositoryItemButtonEdit7";
		this.repositoryItemButtonEdit7.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit7_ButtonClick);
		resources.ApplyResources(this.repositoryItemButtonEdit9, "repositoryItemButtonEdit9");
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		this.repositoryItemButtonEdit9.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit9.Buttons"), resources.GetString("repositoryItemButtonEdit9.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit9.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit9.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit9.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit9.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit9.Buttons6"), resources.GetObject("repositoryItemButtonEdit9.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit9.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit9.Buttons9"))
		});
		this.repositoryItemButtonEdit9.Name = "repositoryItemButtonEdit9";
		this.repositoryItemButtonEdit9.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit9_ButtonClick);
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnEdit.Appearance.Font");
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.RecoveredMenuItemFix1, "نوعالفاتورةToolStripMenuItem1");
		this.RecoveredMenuItemFix1.Name = "نوعالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.TxtNet22, "TxtNet22");
		this.TxtNet22.DecimalDigits = 3;
		this.TxtNet22.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet22.Name = "TxtNet22";
		this.TxtNet22.ReadOnly = true;
		this.TxtNet22.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet22.UseCompatibleTextRendering = false;
		this.TxtNet22.Value = new decimal(new int[4]);
		this.TxtNet22.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"), resources.GetString("repositoryItemButtonEdit5.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit5.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonEdit5.Buttons6"), resources.GetObject("repositoryItemButtonEdit5.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit5.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit5.Buttons9"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEdit5.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
		resources.ApplyResources(this.RecoveredMenuItemFix2, "اسمالموردToolStripMenuItem1");
		this.RecoveredMenuItemFix2.Name = "اسمالموردToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix3, "الكمياتToolStripMenuItem1");
		this.RecoveredMenuItemFix3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix4, this.RecoveredMenuItemFix5, this.RecoveredMenuItemFix6 });
		this.RecoveredMenuItemFix3.Name = "الكمياتToolStripMenuItem1";
		resources.ApplyResources(this.RecoveredMenuItemFix4, "الكميةالحاليةToolStripMenuItem");
		this.RecoveredMenuItemFix4.Name = "الكميةالحاليةToolStripMenuItem";
		resources.ApplyResources(this.RecoveredMenuItemFix5, "كلالكمياتبالمستودعاتToolStripMenuItem");
		this.RecoveredMenuItemFix5.Name = "كلالكمياتبالمستودعاتToolStripMenuItem";
		resources.ApplyResources(this.RecoveredMenuItemFix6, "كلالكمياتبالفروعToolStripMenuItem");
		this.RecoveredMenuItemFix6.Name = "كلالكمياتبالفروعToolStripMenuItem";
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.RecoveredMenuItemFix7, "تخصيصToolStripMenuItem1");
		this.RecoveredMenuItemFix7.Name = "تخصيصToolStripMenuItem1";
		resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		resources.ApplyResources(this.MenuExit, "MenuExit");
		this.MenuExit.Name = "MenuExit";
		this.MenuExit.Click += new System.EventHandler(MenuExit_Click);
		resources.ApplyResources(this.groupBox3, "groupBox3");
		this.groupBox3.Controls.Add(this.gridControl2);
		this.groupBox3.Controls.Add(this.TxtNet22);
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.TabStop = false;
		resources.ApplyResources(this.gridControl2, "gridControl2");
		this.gridControl2.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControl2.EmbeddedNavigator.AccessibleDescription");
		this.gridControl2.EmbeddedNavigator.AccessibleName = resources.GetString("gridControl2.EmbeddedNavigator.AccessibleName");
		this.gridControl2.EmbeddedNavigator.AllowHtmlTextInToolTip = (DevExpress.Utils.DefaultBoolean)resources.GetObject("gridControl2.EmbeddedNavigator.AllowHtmlTextInToolTip");
		this.gridControl2.EmbeddedNavigator.Anchor = (System.Windows.Forms.AnchorStyles)resources.GetObject("gridControl2.EmbeddedNavigator.Anchor");
		this.gridControl2.EmbeddedNavigator.BackgroundImage = (System.Drawing.Image)resources.GetObject("gridControl2.EmbeddedNavigator.BackgroundImage");
		this.gridControl2.EmbeddedNavigator.BackgroundImageLayout = (System.Windows.Forms.ImageLayout)resources.GetObject("gridControl2.EmbeddedNavigator.BackgroundImageLayout");
		this.gridControl2.EmbeddedNavigator.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("gridControl2.EmbeddedNavigator.ImeMode");
		this.gridControl2.EmbeddedNavigator.Margin = (System.Windows.Forms.Padding)resources.GetObject("gridControl2.EmbeddedNavigator.Margin");
		this.gridControl2.EmbeddedNavigator.MaximumSize = (System.Drawing.Size)resources.GetObject("gridControl2.EmbeddedNavigator.MaximumSize");
		this.gridControl2.EmbeddedNavigator.TextLocation = (DevExpress.XtraEditors.NavigatorButtonsTextLocation)resources.GetObject("gridControl2.EmbeddedNavigator.TextLocation");
		this.gridControl2.EmbeddedNavigator.ToolTip = resources.GetString("gridControl2.EmbeddedNavigator.ToolTip");
		this.gridControl2.EmbeddedNavigator.ToolTipIconType = (DevExpress.Utils.ToolTipIconType)resources.GetObject("gridControl2.EmbeddedNavigator.ToolTipIconType");
		this.gridControl2.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControl2.EmbeddedNavigator.ToolTipTitle");
		this.gridControl2.MainView = this.gridView2;
		this.gridControl2.Name = "gridControl2";
		this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[3] { this.repositoryItemButtonEdit8, this.repositoryItemButtonEdit11, this.repositoryItemButtonEdit12 });
		this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView2, this.cardView2 });
		this.gridView2.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView2.Appearance.HeaderPanel.Font");
		this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
		resources.ApplyResources(this.gridView2, "gridView2");
		this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[16]
		{
			this.gridColumn23, this.gridColumn24, this.gridColumn25, this.gridColumn26, this.gridColumn27, this.gridColumn28, this.gridColumn6, this.gridColumn29, this.gridColumn30, this.gridColumn33,
			this.gridColumn34, this.gridColumn36, this.gridColumn7, this.gridColumn31, this.gridColumn32, this.gridColumn35
		});
		this.gridView2.GridControl = this.gridControl2;
		this.gridView2.Name = "gridView2";
		this.gridView2.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.False;
		this.gridView2.OptionsBehavior.KeepGroupExpandedOnSorting = false;
		this.gridView2.OptionsCustomization.AllowSort = false;
		this.gridView2.OptionsMenu.ShowGroupSortSummaryItems = false;
		this.gridView2.OptionsNavigation.AutoFocusNewRow = true;
		this.gridView2.OptionsNavigation.EnterMoveNextColumn = true;
		this.gridView2.OptionsPrint.EnableAppearanceOddRow = true;
		this.gridView2.OptionsPrint.PrintFilterInfo = true;
		this.gridView2.OptionsPrint.PrintPreview = true;
		this.gridView2.OptionsView.ShowFooter = true;
		this.gridView2.OptionsView.ShowGroupPanel = false;
		resources.ApplyResources(this.gridColumn23, "gridColumn23");
		this.gridColumn23.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceCell.Font");
		this.gridColumn23.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn23.AppearanceCell.FontStyleDelta");
		this.gridColumn23.AppearanceCell.Options.UseFont = true;
		this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceHeader.Font");
		this.gridColumn23.AppearanceHeader.Options.UseFont = true;
		this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.ColumnEdit = this.repositoryItemButtonEdit11;
		this.gridColumn23.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn23.FieldName = "كود_الصنف";
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonEdit11, "repositoryItemButtonEdit11");
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions5");
		this.repositoryItemButtonEdit11.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit11.Buttons"), resources.GetString("repositoryItemButtonEdit11.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit11.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit11.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit11.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit11.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("repositoryItemButtonEdit11.Buttons6"), resources.GetObject("repositoryItemButtonEdit11.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit11.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit11.Buttons9"))
		});
		this.repositoryItemButtonEdit11.Name = "repositoryItemButtonEdit11";
		this.repositoryItemButtonEdit11.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit11_ButtonClick);
		resources.ApplyResources(this.gridColumn24, "gridColumn24");
		this.gridColumn24.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceCell.Font");
		this.gridColumn24.AppearanceCell.Options.UseFont = true;
		this.gridColumn24.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceHeader.Font");
		this.gridColumn24.AppearanceHeader.Options.UseFont = true;
		this.gridColumn24.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.FieldName = "أسم الصنف";
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn24.OptionsColumn.AllowEdit = false;
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
		this.gridColumn25.ColumnEdit = this.repositoryItemButtonEdit7;
		this.gridColumn25.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.gridColumn25.FieldName = "الوحدة";
		this.gridColumn25.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.OptionsColumn.ReadOnly = true;
		this.gridColumn25.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn26, "gridColumn26");
		this.gridColumn26.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceHeader.Font");
		this.gridColumn26.AppearanceHeader.Options.UseFont = true;
		this.gridColumn26.FieldName = "أسم_الوحدة";
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.OptionsColumn.AllowEdit = false;
		this.gridColumn26.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn27, "gridColumn27");
		this.gridColumn27.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceCell.Font");
		this.gridColumn27.AppearanceCell.Options.UseFont = true;
		this.gridColumn27.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceHeader.Font");
		this.gridColumn27.AppearanceHeader.Options.UseFont = true;
		this.gridColumn27.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.ColumnEdit = this.repositoryItemButtonEdit9;
		this.gridColumn27.FieldName = "المخزن";
		this.gridColumn27.Name = "gridColumn27";
		this.gridColumn27.OptionsColumn.ReadOnly = true;
		this.gridColumn27.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
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
		this.gridColumn28.FieldName = "الكمية";
		this.gridColumn28.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn28.Name = "gridColumn28";
		this.gridColumn28.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn28.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn6, "gridColumn6");
		this.gridColumn6.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceCell.Font");
		this.gridColumn6.AppearanceCell.Options.UseFont = true;
		this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceHeader.Font");
		this.gridColumn6.AppearanceHeader.Options.UseFont = true;
		this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn6.FieldName = "النسبة";
		this.gridColumn6.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn6.MinWidth = 24;
		this.gridColumn6.Name = "gridColumn6";
		resources.ApplyResources(this.gridColumn29, "gridColumn29");
		this.gridColumn29.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceCell.Font");
		this.gridColumn29.AppearanceCell.Options.UseFont = true;
		this.gridColumn29.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceHeader.Font");
		this.gridColumn29.AppearanceHeader.Options.UseFont = true;
		this.gridColumn29.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.gridColumn29.FieldName = "سعر التكلفة";
		this.gridColumn29.Name = "gridColumn29";
		this.gridColumn29.OptionsColumn.AllowEdit = false;
		this.gridColumn29.OptionsColumn.AllowFocus = false;
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
		this.gridColumn30.FieldName = "الاجمالى";
		this.gridColumn30.Name = "gridColumn30";
		this.gridColumn30.OptionsColumn.AllowEdit = false;
		this.gridColumn30.OptionsColumn.AllowFocus = false;
		this.gridColumn30.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn30.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn33, "gridColumn33");
		this.gridColumn33.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn33.AppearanceCell.Font");
		this.gridColumn33.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn33.AppearanceCell.FontStyleDelta");
		this.gridColumn33.AppearanceCell.Options.UseFont = true;
		this.gridColumn33.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn33.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn33.AppearanceHeader.Font");
		this.gridColumn33.AppearanceHeader.Options.UseFont = true;
		this.gridColumn33.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn33.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn33.ColumnEdit = this.repositoryItemButtonEdit12;
		this.gridColumn33.FieldName = "حذف";
		this.gridColumn33.Name = "gridColumn33";
		this.gridColumn33.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonEdit12, "repositoryItemButtonEdit12");
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions6");
		this.repositoryItemButtonEdit12.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit12.Buttons"), resources.GetString("repositoryItemButtonEdit12.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit12.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit12.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit12.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit12.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("repositoryItemButtonEdit12.Buttons6"), resources.GetObject("repositoryItemButtonEdit12.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit12.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit12.Buttons9"))
		});
		this.repositoryItemButtonEdit12.Name = "repositoryItemButtonEdit12";
		this.repositoryItemButtonEdit12.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit12.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit12_ButtonPressed);
		resources.ApplyResources(this.gridColumn34, "gridColumn34");
		this.gridColumn34.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn34.AppearanceCell.Font");
		this.gridColumn34.AppearanceCell.Options.UseFont = true;
		this.gridColumn34.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn34.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn34.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn34.AppearanceHeader.Font");
		this.gridColumn34.AppearanceHeader.Options.UseFont = true;
		this.gridColumn34.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn34.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn34.FieldName = "إجمالى_بعدالخصم";
		this.gridColumn34.Name = "gridColumn34";
		this.gridColumn34.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn36.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn36.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn36.FieldName = "ظ…";
		this.gridColumn36.Name = "gridColumn36";
		this.gridColumn36.OptionsColumn.AllowEdit = false;
		this.gridColumn36.OptionsColumn.AllowFocus = false;
		this.gridColumn36.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn7, "gridColumn7");
		this.gridColumn7.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceCell.Font");
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceHeader.Font");
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn7.FieldName = "قيمة الربح";
		this.gridColumn7.MinWidth = 23;
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn31.FieldName = "نسبة الربح";
		this.gridColumn31.MinWidth = 23;
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
		this.gridColumn32.FieldName = "سعر البيع";
		this.gridColumn32.MinWidth = 23;
		this.gridColumn32.Name = "gridColumn32";
		this.gridColumn32.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn35, "gridColumn35");
		this.gridColumn35.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn35.AppearanceCell.Font");
		this.gridColumn35.AppearanceCell.Options.UseFont = true;
		this.gridColumn35.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn35.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn35.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn35.AppearanceHeader.Font");
		this.gridColumn35.AppearanceHeader.Options.UseFont = true;
		this.gridColumn35.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn35.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn35.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn35.FieldName = "اجمالى البيع";
		this.gridColumn35.MinWidth = 23;
		this.gridColumn35.Name = "gridColumn35";
		this.gridColumn35.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonEdit8, "repositoryItemButtonEdit8");
		this.repositoryItemButtonEdit8.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.repositoryItemButtonEdit8.Name = "repositoryItemButtonEdit8";
		resources.ApplyResources(this.cardView2, "cardView2");
		this.cardView2.CardWidth = 199;
		this.cardView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn37, this.gridColumn38, this.gridColumn39, this.gridColumn40, this.gridColumn41, this.gridColumn42, this.gridColumn43, this.gridColumn44, this.gridColumn45 });
		this.cardView2.GridControl = this.gridControl2;
		this.cardView2.Name = "cardView2";
		this.cardView2.OptionsPrint.PrintFilterInfo = true;
		this.cardView2.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		resources.ApplyResources(this.gridColumn37, "gridColumn37");
		this.gridColumn37.Name = "gridColumn37";
		resources.ApplyResources(this.gridColumn38, "gridColumn38");
		this.gridColumn38.Name = "gridColumn38";
		resources.ApplyResources(this.gridColumn39, "gridColumn39");
		this.gridColumn39.Name = "gridColumn39";
		this.gridColumn39.OptionsColumn.AllowEdit = false;
		this.gridColumn39.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn40, "gridColumn40");
		this.gridColumn40.Name = "gridColumn40";
		resources.ApplyResources(this.gridColumn41, "gridColumn41");
		this.gridColumn41.Name = "gridColumn41";
		resources.ApplyResources(this.gridColumn42, "gridColumn42");
		this.gridColumn42.Name = "gridColumn42";
		resources.ApplyResources(this.gridColumn43, "gridColumn43");
		this.gridColumn43.Name = "gridColumn43";
		resources.ApplyResources(this.gridColumn44, "gridColumn44");
		this.gridColumn44.Name = "gridColumn44";
		resources.ApplyResources(this.gridColumn45, "gridColumn45");
		this.gridColumn45.Name = "gridColumn45";
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
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[12]
		{
			this.gridColumn1, this.gridColumn2, this.shearch, this.gridColumn19, this.gridColumn4, this.gridColumn20, this.gridColumn5, this.gridColumn21, this.gridColumn3, this.gridColumn8,
			this.gridColumn22, this.gridColumn9
		});
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
		this.gridColumn1.Name = "gridColumn1";
		resources.ApplyResources(this.repositoryItemButtonEditCodeSearch, "repositoryItemButtonEditCodeSearch");
		resources.ApplyResources(editorButtonImageOptions7, "editorButtonImageOptions7");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons"), resources.GetString("repositoryItemButtonEditCodeSearch.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons5"), editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, resources.GetString("repositoryItemButtonEditCodeSearch.Buttons6"), resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons9"))
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
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
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
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
		editorButtonImageOptions8.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions8");
		editorButtonImageOptions8.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemButtonEdit4.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.FieldName = "أسم_الوحدة";
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
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn5, "gridColumn5");
		this.gridColumn5.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceCell.Font");
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceHeader.Font");
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn5.FieldName = "عدد الوحدة";
		this.gridColumn5.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn5.MinWidth = 24;
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
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
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
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
		this.gridColumn8.Name = "gridColumn8";
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit6, "repositoryItemButtonEdit6");
		resources.ApplyResources(editorButtonImageOptions9, "editorButtonImageOptions9");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions9, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance9, appearanceHovered9, appearancePressed9, appearanceDisabled9, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
		resources.ApplyResources(this.gridColumn22, "gridColumn22");
		this.gridColumn22.FieldName = "إجمالى_بعدالخصم";
		this.gridColumn22.Name = "gridColumn22";
		resources.ApplyResources(this.gridColumn9, "gridColumn9");
		this.gridColumn9.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceCell.Font");
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceHeader.Font");
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.FieldName = "ظ…";
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
		resources.ApplyResources(this.BtnPrint, "BtnPrint");
		this.BtnPrint.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrint.Appearance.Font");
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNew.Appearance.Font");
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDelete.Appearance.Font");
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.RecoveredMenuItemFix8, "رقمالفاتورةToolStripMenuItem1");
		this.RecoveredMenuItemFix8.Name = "رقمالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.label26, "label26");
		this.label26.Name = "label26";
		resources.ApplyResources(this.CBCostCenter, "CBCostCenter");
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		this.CBCostCenter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
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
		resources.ApplyResources(editorButtonImageOptions10, "editorButtonImageOptions10");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), editorButtonImageOptions10, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance10, appearanceHovered10, appearancePressed10, appearanceDisabled10, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		resources.ApplyResources(editorButtonImageOptions11, "editorButtonImageOptions11");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"), resources.GetString("repositoryItemButtonEdit3.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit3.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons5"), editorButtonImageOptions11, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance11, appearanceHovered11, appearancePressed11, appearanceDisabled11, resources.GetString("repositoryItemButtonEdit3.Buttons6"), resources.GetObject("repositoryItemButtonEdit3.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit3.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit3.Buttons9"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions12.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions12, "editorButtonImageOptions12");
		editorButtonImageOptions12.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions12, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance12, appearanceHovered12, appearancePressed12, appearanceDisabled12, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
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
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label39);
		this.groupBox1.Controls.Add(this.CBProject);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Controls.Add(this.CBCostCenter);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.TxtNumNote);
		this.groupBox1.Controls.Add(this.label23);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		this.groupBox1.Enter += new System.EventHandler(textBox1_Enter);
		resources.ApplyResources(this.label39, "label39");
		this.label39.Name = "label39";
		resources.ApplyResources(this.CBProject, "CBProject");
		this.CBProject.FormattingEnabled = true;
		this.CBProject.Name = "CBProject";
		this.CBProject.SelectedIndexChanged += new System.EventHandler(CBProject_SelectedIndexChanged);
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
		resources.ApplyResources(editorButtonImageOptions13, "editorButtonImageOptions13");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCode.Buttons"), resources.GetString("repositoryItemButtonEditCode.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCode.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons5"), editorButtonImageOptions13, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance13, appearanceHovered13, appearancePressed13, appearanceDisabled13, resources.GetString("repositoryItemButtonEditCode.Buttons6"), resources.GetObject("repositoryItemButtonEditCode.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCode.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCode.Buttons9"))
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
		resources.ApplyResources(this.RecoveredMenuItemFix9, "استعلامToolStripMenuItem1");
		this.RecoveredMenuItemFix9.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.RecoveredMenuItemFix8, this.RecoveredMenuItemFix2, this.RecoveredMenuItemFix1 });
		this.RecoveredMenuItemFix9.Name = "استعلامToolStripMenuItem1";
		resources.ApplyResources(this.MenuPurchOrder, "MenuPurchOrder");
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		resources.ApplyResources(this.MenuSearchItem, "MenuSearchItem");
		this.MenuSearchItem.Name = "MenuSearchItem";
		this.MenuSearchItem.Click += new System.EventHandler(MenuSearchItem_Click);
		resources.ApplyResources(this.RecoveredMenuItemFix10, "حركةصنفToolStripMenuItem");
		this.RecoveredMenuItemFix10.Name = "حركةصنفToolStripMenuItem";
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
		resources.ApplyResources(this.RecoveredMenuItemFix11, "طباعةToolStripMenuItem1");
		this.RecoveredMenuItemFix11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.MenuPrintQued, this.MenuPreviewFatoura, this.MenuPrintFatoura });
		this.RecoveredMenuItemFix11.Name = "طباعةToolStripMenuItem1";
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
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.RecoveredMenuItemFix11, this.toolStripSeparator1, this.MenuNewItem, this.RecoveredMenuItemFix10, this.MenuSearchItem, this.toolStripSeparator4,
			this.MenuPurchOrder, this.RecoveredMenuItemFix9, this.RecoveredMenuItemFix3, this.toolStripSeparator2, this.RecoveredMenuItemFix7, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.ribbonPage2.Name = "ribbonPage2";
		resources.ApplyResources(this.ribbonPage2, "ribbonPage2");
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.BtnPlus);
		this.groupBox2.Controls.Add(this.BtnManufacturingOrder);
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.BtnPlus, "BtnPlus");
		this.BtnPlus.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPlus.Appearance.Font");
		this.BtnPlus.Appearance.Options.UseFont = true;
		this.BtnPlus.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPlus.ImageOptions.Image");
		this.BtnPlus.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPlus.Name = "BtnPlus";
		this.BtnPlus.Click += new System.EventHandler(BtnPlus_Click);
		resources.ApplyResources(this.BtnManufacturingOrder, "BtnManufacturingOrder");
		this.BtnManufacturingOrder.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnManufacturingOrder.Appearance.Font");
		this.BtnManufacturingOrder.Appearance.Options.UseFont = true;
		this.BtnManufacturingOrder.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnManufacturingOrder.ImageOptions.Image");
		this.BtnManufacturingOrder.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnManufacturingOrder.Name = "BtnManufacturingOrder";
		this.BtnManufacturingOrder.Click += new System.EventHandler(BtnManufacturingOrder_Click);
		this.BtnManufacturingOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.txtdicoutprege, "txtdicoutprege");
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Black;
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
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
		resources.ApplyResources(this.TxtTotalAll, "TxtTotalAll");
		this.TxtTotalAll.DecimalDigits = 3;
		this.TxtTotalAll.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalAll.Name = "TxtTotalAll";
		this.TxtTotalAll.ReadOnly = true;
		this.TxtTotalAll.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalAll.Value = new decimal(new int[4]);
		this.TxtTotalAll.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtTotalBeforDisc, "TxtTotalBeforDisc");
		this.TxtTotalBeforDisc.DecimalDigits = 3;
		this.TxtTotalBeforDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalBeforDisc.Name = "TxtTotalBeforDisc";
		this.TxtTotalBeforDisc.ReadOnly = true;
		this.TxtTotalBeforDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalBeforDisc.Value = new decimal(new int[4]);
		this.TxtTotalBeforDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtNet, "TxtNet");
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.ForeColor = System.Drawing.Color.Black;
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.TxtDiscount, "TxtDiscount");
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.txtdicoutprege);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.TxtVat);
		base.Controls.Add(this.TxtTotals);
		base.Controls.Add(this.TxtTotalAll);
		base.Controls.Add(this.TxtTotalBeforDisc);
		base.Controls.Add(this.TxtNet);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.TxtDiscount);
		base.Controls.Add(this.TxtVatBeforeDisc);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmManufacturing";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmManufacturing_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit10).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit9).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit11).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit12).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit8).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView2).EndInit();
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
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


