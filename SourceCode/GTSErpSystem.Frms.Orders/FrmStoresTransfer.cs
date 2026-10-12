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
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Orders;

public class FrmStoresTransfer : XtraForm
{
	private Class_StoresTransfer Pur = new Class_StoresTransfer();

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

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	public bool ISEVENTDISC = true;

	private string fileNameAR = Application.StartupPath + "\\Layout\\StoresTransferAR.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\StoresTransferEN.xml";

	private IContainer components = null;

	private ToolStripMenuItem اسمالموردToolStripMenuItem1;

	private GridColumn gridColumn19;

	private TextEdit TxtLocation;

	private Label label25;

	private NumericEditBox txtdicoutprege;

	private Label label13;

	private TextEdit TxtTafqit;

	private Label label14;

	private NumericEditBox TxtQuntity;

	private Label label16;

	private Label label15;

	private NumericEditBox TxtAllQuantity;

	private NumericEditBox TxtNet22;

	private NumericEditBox TxtLastCost;

	private SimpleButton BtnEdit;

	private Label label10;

	private NumericEditBox TxtAvergCost;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private ToolStripMenuItem رقمالفاتورةToolStripMenuItem1;

	private ToolStripMenuItem نوعالفاتورةToolStripMenuItem1;

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

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

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

	private Label label9;

	private SimpleButton BtnSave;

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

	private Label label11;

	private NumericEditBox TxtNet;

	private Label label12;

	private Label label1;

	private Label label2;

	private NumericEditBox TxtTotalBeforDisc;

	private NumericEditBox TxtSellPrice2;

	private Label label3;

	private NumericEditBox TxtSellPrice1;

	private Label label4;

	private NumericEditBox TxtDiscount;

	private NumericEditBox TxtVatBeforeDisc;

	private Label LBlSuppAccount;

	private Label label24;

	private TextEdit TxtNumNote;

	private Label label21;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private GroupBox groupBox1;

	private Label label17;

	private ButtonEdit TxtCode;

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem MenuSearchItem;

	private ToolStripMenuItem حركةصنفToolStripMenuItem;

	private ToolStripMenuItem MenuNewItem;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripMenuItem MenuPreviewFatoura;

	private ToolStripMenuItem MenuPrintQued;

	private ToolStripMenuItem طباعةToolStripMenuItem1;

	private ToolStripMenuItem استعلامToolStripMenuItem1;

	private ToolStripMenuItem MenuDelete;

	private ToolStripMenuItem MenuSave;

	private ToolStripMenuItem MenuNew;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem MenuEdit;

	private RibbonPage ribbonPage2;

	private Label label5;

	private Label label6;

	private GroupBox groupBox2;

	private Label LblToStore;

	private Label label22;

	private ButtonEdit TxtToStore;

	private Label LblFromStore;

	private Label label19;

	private ButtonEdit TxtFromStore;

	private DateEdit TxtDate;

	public FrmStoresTransfer()
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
		gridView1.FocusedRowHandle = 1;
		gridView1.FocusedColumn = gridView1.VisibleColumns[0];
		gridView1.ShowEditor();
		GroupPermission();
		TxtDate.Focus();
		LblFromStore.Text = LoginDetails.StoreID.ToString();
		TxtFromStore.Text = Pur.SelectStore(LoginDetails.StoreID);
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		txtdicoutprege.Controls[0].Visible = false;
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 25);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 25);
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
		if (text == string.Empty || text2 == string.Empty || num <= 0m)
		{
			Item_Items item_Items = Pur.GetItems(text).FirstOrDefault();
			if (item_Items != null)
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
								MessageBox.Show("عفوا ،لايمكن عمل فاتورة لاصناف خدمية", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
								gridView1.FocusedRowHandle = e.RowHandle;
								gridView1.FocusedColumn = gridView1.VisibleColumns[-1];
								gridView1.ShowEditor();
							}
							else if (item_Items.item_Type == 3)
							{
								MessageBox.Show("عفوا ،لايمكن عمل فاتورة لاصناف مكونة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
								gridView1.FocusedRowHandle = e.RowHandle;
								gridView1.FocusedColumn = gridView1.VisibleColumns[-1];
								gridView1.ShowEditor();
							}
							else
							{
								decimal num2 = Convert.ToDecimal(item_Items.Tax_Value / (decimal?)100 + (decimal?)1);
								DataRow dataRow = null;
								dataRow = datatable.NewRow();
								dataRow["كود_الصنف"] = item_Items.Item_code;
								dataRow["أسم الصنف"] = item_Items.item_Name;
								dataRow["الوحدة"] = item_Items.Item_Unit.Name;
								dataRow["الكمية"] = FUNC_DecimalNumQty(1);
								dataRow["مسح"] = 0;
								dataRow["تم"] = 1;
								dataRow["أسم_الوحدة"] = "الصغرى";
								if (LoginDetails.ItemTotalWithVat)
								{
									dataRow["سعر الوحدة"] = Math.Round(Convert.ToDecimal(item_Items.LastCost) / num2, 2);
									dataRow["الاجمالى"] = dataRow["سعر الوحدة"];
								}
								else
								{
									dataRow["سعر الوحدة"] = item_Items.LastCost;
									dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(1)), DecimalNum);
								}
								object rowCellValue2 = gridView1.GetRowCellValue(e.RowHandle, "م");
								string text3 = rowCellValue2.ToString();
								if (text3 == string.Empty)
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
								GetTotalCost();
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
							string text4 = Convert.ToString(e.Value);
							string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
							string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
							string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
							string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("م");
							decimal num3 = Convert.ToDecimal(item_Items.Tax_Value / (decimal?)100 + (decimal?)1);
							if (!(row["م"].ToString() == focusedRowCellDisplayText4))
							{
								continue;
							}
							if (row["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row["كود_الصنف"] = item_Items.Item_code;
								row["أسم الصنف"] = item_Items.item_Name;
								if (LoginDetails.ItemTotalWithVat)
								{
									row["سعر الوحدة"] = Math.Round(Convert.ToDecimal(item_Items.LastCost) / num3, 2);
									row["الاجمالى"] = row["سعر الوحدة"];
								}
								else
								{
									row["سعر الوحدة"] = item_Items.LastCost;
									row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(1)), DecimalNum);
								}
								break;
							}
							if (row["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								row["كود_الصنف"] = item_Items.Item_code;
								row["أسم الصنف"] = item_Items.item_Name;
								if (LoginDetails.ItemTotalWithVat)
								{
									row["سعر الوحدة"] = Math.Round(Convert.ToDecimal(item_Items.LastCost2) / num3, 2);
									row["الاجمالى"] = row["سعر الوحدة"];
								}
								else
								{
									row["سعر الوحدة"] = item_Items.LastCost2;
									row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost2 * (decimal?)Convert.ToDecimal(1)), DecimalNum);
								}
								break;
							}
							if (row["أسم_الوحدة"].ToString() == "الكبرى")
							{
								row["كود_الصنف"] = item_Items.Item_code;
								row["أسم الصنف"] = item_Items.item_Name;
								if (LoginDetails.ItemTotalWithVat)
								{
									row["سعر الوحدة"] = Math.Round(Convert.ToDecimal(item_Items.LastCost3) / num3, 2);
									row["الاجمالى"] = row["سعر الوحدة"];
								}
								else
								{
									row["سعر الوحدة"] = item_Items.LastCost3;
									row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost3 * (decimal?)Convert.ToDecimal(1)), DecimalNum);
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
				CodeItm = text;
				SearchItems();
				LoginDetails.ItemSearch = string.Empty;
			}
		}
		else
		{
			if (!(e.Column.FieldName == "الكمية"))
			{
				return;
			}
			string value = Convert.ToString(e.Value);
			string focusedRowCellDisplayText5 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText6 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText7 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText8 = gridView1.GetFocusedRowCellDisplayText("م");
			if (focusedRowCellDisplayText5 != string.Empty)
			{
				Item_Items item_Items2 = Pur.GetItems(focusedRowCellDisplayText5).FirstOrDefault();
				if (item_Items2 != null)
				{
					foreach (DataRow row2 in datatable.Rows)
					{
						if (row2["م"].ToString() == focusedRowCellDisplayText8)
						{
							decimal num4 = Convert.ToDecimal(row2["سعر الوحدة"].ToString());
							if (row2["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num4 * Convert.ToDecimal(value)), DecimalNum);
								break;
							}
							if (row2["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num4 * Convert.ToDecimal(value)), DecimalNum);
								break;
							}
							if (row2["أسم_الوحدة"].ToString() == "الكبرى")
							{
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(num4 * Convert.ToDecimal(value)), DecimalNum);
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
		IsEnableTrue();
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
		GetAllitems_alternatives(focusedRowCellDisplayText, focusedRowCellDisplayText2, Convert.ToInt32(LblFromStore.Text));
		CodeItm = focusedRowCellDisplayText;
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
			FrmSearchTransferStore frmSearchTransferStore = new FrmSearchTransferStore();
			frmSearchTransferStore.ShowDialog();
			if (frmSearchTransferStore.CustCode > 0)
			{
				int custCode = frmSearchTransferStore.CustCode;
				Order_StoreTransfer order_StoreTransfer = Pur.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<Order_StoreTransferDetails> itemsPurchesByID = Pur.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (Order_StoreTransferDetails item in itemsPurchesByID)
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
					dataRow["الكمية"] = FUNC_DecimalNumQty(item.Quantity);
					dataRow["سعر الوحدة"] = item.UnitPrice;
					dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
					dataRow["مسح"] = 0;
					datatable.Rows.Add(dataRow);
					num++;
				}
				TxtCode.Text = order_StoreTransfer.ID.ToString();
				TxtFromStore.Text = Pur.SelectStore(Convert.ToInt32(order_StoreTransfer.FromStoreID));
				TxtToStore.Text = Pur.SelectStore(Convert.ToInt32(order_StoreTransfer.ToStoreID));
				TxtDate.Text = Convert.ToDateTime(order_StoreTransfer.DateTransfer).ToString("yyyy/MM/dd HH:mm:ss tt");
				TxtNumNote.Text = order_StoreTransfer.Note;
				LblFromStore.Text = order_StoreTransfer.FromStoreID.ToString();
				LblToStore.Text = order_StoreTransfer.FromStoreID.ToString();
				gridControl1.DataSource = datatable;
				PrintID = custCode;
				Cal_FinalResultTotals();
				FiterDatatableItems();
				FiterDatatableItemsUpdate();
				GetTotalCost();
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = true;
				gridView1.OptionsBehavior.Editable = false;
				ArrangeDatatable();
				IsEnableFalse();
			}
			else
			{
				TxtCode.Focus();
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void TxtFromStore_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchTransfer2 frmSearchTransfer = new FrmSearchTransfer2();
		frmSearchTransfer.ShowDialog();
		if (frmSearchTransfer.CustCode > 0)
		{
			LblFromStore.Text = frmSearchTransfer.StorID.ToString();
			TxtFromStore.Text = frmSearchTransfer.StoreName.ToString();
			if (TxtFromStore.Text == TxtToStore.Text)
			{
				MessageBox.Show("نعتذر، لايمكن ان يكون اسم تحويل من مستودع هو نفس اسم تحويل الي مستودع", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				LblFromStore.Text = "0";
				TxtFromStore.Text = string.Empty;
				TxtFromStore.Focus();
			}
		}
	}

	private void TxtToStore_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchTransfer2 frmSearchTransfer = new FrmSearchTransfer2();
		frmSearchTransfer.ShowDialog();
		if (frmSearchTransfer.CustCode > 0)
		{
			LblToStore.Text = frmSearchTransfer.StorID.ToString();
			TxtToStore.Text = frmSearchTransfer.StoreName.ToString();
			if (TxtFromStore.Text == TxtToStore.Text)
			{
				MessageBox.Show("نعتذر، لايمكن ان يكون اسم تحويل من مستودع هو نفس اسم تحويل الي مستودع", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				LblToStore.Text = "0";
				TxtToStore.Text = string.Empty;
				TxtToStore.Focus();
			}
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
		object summaryValue = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
		TxtTotals.Text = Convert.ToString(summaryValue);
	}

	public void insertnewrow()
	{
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = string.Empty;
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["سعر الوحدة"] = 0;
		dataRow["الكمية"] = FUNC_DecimalNumQty(1);
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
		if (Itmcode != string.Empty)
		{
			TxtQuntity.Text = FUNC_DecimalNumQty(Convert.ToDecimal(Pur.GetQantityForItems(Itmcode, unit, StoreID))).ToString();
			TxtAllQuantity.Text = FUNC_DecimalNumQty(Convert.ToDecimal(Pur.GetQantityForItems(Itmcode, unit))).ToString();
			TxtAvergCost.Text = Math.Round(Convert.ToDecimal(Pur.GetAvargeCost(Itmcode)), DecimalNum).ToString();
			TxtSellPrice1.Text = Math.Round(Convert.ToDecimal(Pur.GetSellprice1(Itmcode)), DecimalNum).ToString();
			TxtSellPrice2.Text = Math.Round(Convert.ToDecimal(Pur.GetSellprice2(Itmcode)), DecimalNum).ToString();
			TxtSellPrice3.Text = Math.Round(Convert.ToDecimal(Pur.GetSellprice3(Itmcode)), DecimalNum).ToString();
			TxtProfit.Text = Math.Round(Convert.ToDecimal(TxtSellPrice1.Text) - Convert.ToDecimal(TxtAvergCost.Text)).ToString();
			GTSdbEntities gTSdbEntities = new GTSdbEntities();
			Item_Items item_Items = gTSdbEntities.Item_Items.Where((Item_Items x) => x.Item_code == Itmcode || x.item_Name == Itmcode || x.item_Name_English == Itmcode || x.SmallBarCode1 == Itmcode || x.SmallBarCode2 == Itmcode || x.SmallBarCode3 == Itmcode || x.MediumBarCode1 == Itmcode || x.MediumBarCode2 == Itmcode || x.MediumBarCode3 == Itmcode || x.BigBarCode1 == Itmcode || x.BigBarCode2 == Itmcode || x.BigBarCode3 == Itmcode).FirstOrDefault();
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
					TxtLocation.Text = string.Empty;
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
					TxtLocation.Text = string.Empty;
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
					TxtLocation.Text = string.Empty;
				}
			}
			if (TxtLastCost.Text == string.Empty || unit == string.Empty)
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
			TxtLocation.Text = string.Empty;
		}
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("كود_الصنف", typeof(string));
		datatable.Columns.Add("أسم الصنف", typeof(string));
		datatable.Columns.Add("الوحدة", typeof(string));
		datatable.Columns.Add("الكمية", typeof(decimal));
		datatable.Columns.Add("سعر الوحدة", typeof(decimal));
		datatable.Columns.Add("أسم_الوحدة", typeof(string));
		datatable.Columns.Add("الاجمالى", typeof(string));
		datatable.Columns.Add("حذف الصنف", typeof(int));
		datatable.Columns.Add("م", typeof(int));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تم", typeof(int));
		dtCurrentTable.Columns.Add("كود_الصنف", typeof(string));
		dtCurrentTable.Columns.Add("أسم الصنف", typeof(string));
		dtCurrentTable.Columns.Add("الوحدة", typeof(string));
		dtCurrentTable.Columns.Add("الكمية", typeof(decimal));
		dtCurrentTable.Columns.Add("سعر الوحدة", typeof(decimal));
		dtCurrentTable.Columns.Add("أسم_الوحدة", typeof(string));
		dtCurrentTable.Columns.Add("الاجمالى", typeof(string));
		dtCurrentTable.Columns.Add("حذف الصنف", typeof(int));
		dtCurrentTable.Columns.Add("م", typeof(int));
		dtCurrentTable.Columns.Add("مسح", typeof(int));
		dtCurrentTable.Columns.Add("تم", typeof(int));
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = string.Empty;
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["أسم_الوحدة"] = "الصغرى";
		dataRow["الكمية"] = FUNC_DecimalNumQty(1);
		dataRow["سعر الوحدة"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
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
		gridView1.Columns["الاجمالى"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, string.Empty, "{0:0.##}", "0")
		});
		GetTotalCost();
	}

	public int IsItemPriceZero()
	{
		int result = 1;
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["كود_الصنف"]);
			if (text != string.Empty && !LoginDetails.IsQuantityMinZero)
			{
				decimal num = Convert.ToDecimal(row["الكمية"]);
				string text2 = Convert.ToString(row["أسم الصنف"]);
				string type = Convert.ToString(row["أسم_الوحدة"]);
				int storeID = Convert.ToInt32(LblFromStore.Text);
				int itemID = Convert.ToInt32(Pur.GetItmID(text));
				decimal quantityPermission = Pur.GetQuantityPermission(itemID, storeID, type);
				if (quantityPermission <= 0m)
				{
					MessageBox.Show("نعتذر، لا يوجد كمية كافية من الصنف ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					result = 0;
					break;
				}
				if (num > quantityPermission)
				{
					MessageBox.Show("نعتذر، الكمية المطلوبة اكبر من الكمية الموجودة  ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
			if (text != string.Empty)
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
			if (text == string.Empty)
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
				TxtNet.Text = "0.00";
				TxtNet22.Text = "0.00";
				TxtTotals.Text = "0.00";
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtNumNote.Text = string.Empty;
				TxtTotalBeforDisc.Text = "0.00";
				TxtVatBeforeDisc.Text = "0.00";
				TxtTafqit.Text = string.Empty;
				TxtTotalAll.Text = "0.00";
				TxtLocation.Text = string.Empty;
				TxtTafqit.Text = string.Empty;
				TxtSellPrice1.Text = "0.00";
				TxtSellPrice2.Text = "0.00";
				TxtSellPrice3.Text = "0.00";
				TxtProfit.Text = "0.00";
				TxtQuntity.Text = FUNC_DecimalNumQty(0m).ToString();
				TxtAllQuantity.Text = FUNC_DecimalNumQty(0m).ToString();
				TxtAvergCost.Text = "0.00";
				TxtLastCost.Text = "0.00";
			}
			else if (TxtDiscount.Text == string.Empty || TxtDiscount.Text == "0.00" || Convert.ToDecimal(TxtDiscount.Text) > Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				object summaryValue = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
				TxtTotals.Text = Convert.ToString(summaryValue);
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)).ToString();
				TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)).ToString();
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
			}
			else
			{
				decimal value = Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text);
				txtdicoutprege.Text = Math.Round(Convert.ToDecimal(Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum) * 100m) / Math.Round(Convert.ToDecimal(value), DecimalNum), DecimalNum).ToString();
				object summaryValue2 = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
				TxtTotals.Text = Convert.ToString(summaryValue2);
				TxtNet.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
				TxtNet22.Text = (Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum) + Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum) - Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum)).ToString();
				TxtTotalAll.Text = Math.Round(Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), DecimalNum).ToString();
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
		TxtCode.Text = string.Empty;
		datatable.Rows.Clear();
		dtCurrentTable.Rows.Clear();
		gridControl1.DataSource = datatable;
		GetTotalCost();
		LblFromStore.Text = LoginDetails.StoreID.ToString();
		TxtFromStore.Text = Pur.SelectStore(LoginDetails.StoreID);
		TxtToStore.Text = string.Empty;
		LblToStore.Text = "0";
		TxtDiscount.Text = string.Empty;
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtNumNote.Text = string.Empty;
		TxtTotalBeforDisc.Text = "0.00";
		TxtVatBeforeDisc.Text = "0.00";
		TxtTafqit.Text = string.Empty;
		TxtTotalAll.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtNet22.Text = "0.00";
		TxtLocation.Text = string.Empty;
		TxtSellPrice1.Text = "0.00";
		TxtSellPrice2.Text = "0.00";
		TxtSellPrice3.Text = "0.00";
		TxtProfit.Text = "0.00";
		TxtQuntity.Text = FUNC_DecimalNumQty(0m).ToString();
		TxtAllQuantity.Text = FUNC_DecimalNumQty(0m).ToString();
		TxtAvergCost.Text = "0.00";
		TxtLastCost.Text = "0.00";
		TxtDate.DateTime = DateTime.Now;
		TxtDiscount.Enabled = true;
		txtdicoutprege.Enabled = true;
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
		dataRow["كود_الصنف"] = string.Empty;
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["الكمية"] = FUNC_DecimalNumQty(1);
		dataRow["سعر الوحدة"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		IsEnableTrue();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 25))
		{
			if (CheckStoreName() == 0)
			{
				return;
			}
			int num = 0;
			if (datatable.Rows.Count > 1)
			{
				Cal_FinalResultTotals();
				FiterDatatableItemsUpdate();
				GetTotalCost();
				FiterDatatableItems();
				if (IsItemPriceZero() == 0)
				{
					return;
				}
				num = Pur.InsertStor(out var Code, Convert.ToInt32(LblFromStore.Text), Convert.ToInt32(LblToStore.Text), TxtDate.DateTime, TxtNumNote.Text);
				Pur.InsertItems(datatable, Code, Convert.ToInt32(LblFromStore.Text), Convert.ToInt32(LblToStore.Text));
				gridControl1.DataSource = datatable;
				gridView1.OptionsBehavior.Editable = false;
				if (num > 0)
				{
					PrintID = Code;
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					TxtCode.Text = num.ToString();
					switch (MessageBox.Show("تم حفظ بيانات تحويل الاصناف بين المستودعات بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						Print();
						break;
					}
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات تحويل الاصناف بين المستودعات رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 25))
		{
			if (CheckStoreName() == 0)
			{
				return;
			}
			if (TxtDate.ReadOnly)
			{
				IsEnableTrue();
				BtnEdit.Text = "F5 حفظ";
				insertnewrow();
				gridView1.OptionsBehavior.Editable = true;
				return;
			}
			int num = 0;
			if (datatable.Rows.Count > 1)
			{
				ArrangeDatatable();
				Cal_FinalResultTotals();
				FiterDatatableItemsUpdate();
				GetTotalCost();
				if (IsItemPriceZero() == 0)
				{
					return;
				}
				int num2 = Convert.ToInt32(TxtCode.Text);
				num = Pur.UpdateStor(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblFromStore.Text), Convert.ToInt32(LblToStore.Text), TxtDate.DateTime, TxtNumNote.Text);
				Pur.deleteItemsByOrderId(num, Convert.ToInt32(LblFromStore.Text), Convert.ToInt32(LblToStore.Text));
				Pur.InsertItems(datatable, num, Convert.ToInt32(LblFromStore.Text), Convert.ToInt32(LblToStore.Text));
				if (num > 0)
				{
					PrintID = num;
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					IsEnableFalse();
					BtnEdit.Text = "Edit تعديل";
					gridView1.OptionsBehavior.Editable = false;
					switch (MessageBox.Show("تم تعديل تحويل الاصناف بين المستودعات بنجاح \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						Print();
						break;
					}
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات تحويل الاصناف بين المستودعات رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 18))
		{
			int num = 0;
			if (TxtCode.Text != string.Empty)
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف تحويل الاصناف بين المستودعات رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = Pur.DeleteStor(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblFromStore.Text), Convert.ToInt32(LblToStore.Text));
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات تحويل الاصناف بين المستودعات رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						gridView1.OptionsBehavior.Editable = true;
						IsEnableTrue();
						BtnDelete.Enabled = false;
						BtnEdit.Enabled = false;
						BtnSave.Enabled = true;
						insertnewrow();
						TxtCode.Focus();
						PrintID = 0;
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات تحويل الاصناف بين المستودعات رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات تحويل الاصناف بين المستودعات بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 18))
		{
			if (TxtCode.Text != string.Empty)
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_StoreTransfer", sqlConnection);
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
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintStoresTransfer.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintStoresTransfer.mrt");
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
					stiReport["Date"] = TxtDate.DateTime;
					stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
					stiReport["Tafqit"] = txtWordAr;
					stiReport["FromStore"] = TxtFromStore.Text;
					stiReport["ToStore"] = TxtToStore.Text;
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
		txtdicoutprege.ReadOnly = false;
		TxtDate.ReadOnly = false;
		TxtNumNote.ReadOnly = false;
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = true;
		txtdicoutprege.ReadOnly = true;
		TxtDate.ReadOnly = true;
		TxtNumNote.ReadOnly = true;
		TxtDiscount.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
		TxtNumNote.BackColor = Color.White;
	}

	public void openItmCard()
	{
		if (gridControl1.Focus())
		{
			if (CodeItm != string.Empty)
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
						if (row["كود_الصنف"].ToString() != string.Empty)
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
					decimal num2 = Convert.ToDecimal(item_Items.Tax_Value / (decimal?)100 + (decimal?)1);
					if (item_Items != null)
					{
						DataRow dataRow2 = null;
						dataRow2 = datatable.NewRow();
						dataRow2["كود_الصنف"] = item_Items.Item_code;
						dataRow2["أسم الصنف"] = item_Items.item_Name;
						dataRow2["الوحدة"] = item_Items.Item_Unit.Name;
						dataRow2["الكمية"] = FUNC_DecimalNumQty(1);
						dataRow2["أسم_الوحدة"] = "الصغرى";
						if (LoginDetails.ItemTotalWithVat)
						{
							dataRow2["سعر الوحدة"] = Math.Round(Convert.ToDecimal(item_Items.LastCost) / num2, 2);
							dataRow2["الاجمالى"] = dataRow2["سعر الوحدة"];
						}
						else
						{
							dataRow2["سعر الوحدة"] = item_Items.LastCost;
							dataRow2["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.LastCost * (decimal?)Convert.ToDecimal(1)), DecimalNum);
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

	private int CheckStoreName()
	{
		int result = 1;
		if (TxtFromStore.Text == TxtToStore.Text)
		{
			MessageBox.Show("نعتذر، لايمكن ان يكون اسم تحويل من مستودع هو نفس اسم تحويل الي مستودع", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			LblFromStore.Text = "0";
			TxtFromStore.Text = string.Empty;
			result = 0;
			TxtFromStore.Focus();
		}
		if (TxtFromStore.Text == string.Empty)
		{
			MessageBox.Show("فضلا، ادخل اسم المستودع ثم حاول مرة اخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			result = 0;
			TxtFromStore.Focus();
		}
		if (TxtToStore.Text == string.Empty)
		{
			MessageBox.Show("فضلا، ادخل اسم المستودع ثم حاول مرة اخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			result = 0;
			TxtToStore.Focus();
		}
		return result;
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
		TxtTotals.DecimalDigits = DecimalNum;
		TxtTotalBeforDisc.DecimalDigits = DecimalNum;
		TxtTotalAll.DecimalDigits = DecimalNum;
		TxtNet.DecimalDigits = DecimalNum;
		TxtNet22.DecimalDigits = DecimalNum;
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
		string Itemocde = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "كود_الصنف"));
		if (Itemocde != string.Empty)
		{
			LoginDetails.ItemcodeForUnits = Itemocde;
			FrmSearchUnit frmSearchUnit = new FrmSearchUnit();
			Item_Items item_Items = (from x in Pur.GetItems()
				where x.Item_code == Itemocde
				select x).FirstOrDefault();
			int itemId = item_Items.ItemId;
			frmSearchUnit.ShowDialog();
			if (frmSearchUnit.ItemCode != string.Empty)
			{
				string itemCode = frmSearchUnit.ItemCode;
				if (frmSearchUnit.unitName == "الصغرى")
				{
					Item_Items item_Items2 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items2 != null)
					{
						int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row in datatable.Rows)
						{
							if (row["م"].ToString() == Convert.ToString(num))
							{
								row["الوحدة"] = item_Items2.Item_Unit.Name;
								row["أسم_الوحدة"] = "الصغرى";
								row["الكمية"] = FUNC_DecimalNumQty(1);
								row["سعر الوحدة"] = item_Items2.LastCost;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items2.LastCost * (decimal?)Convert.ToInt32(1)), DecimalNum);
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
					Item_Items item_Items3 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items3 != null)
					{
						int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row2 in datatable.Rows)
						{
							if (row2["م"].ToString() == Convert.ToString(num2))
							{
								row2["الوحدة"] = item_Items3.Item_Unit1.Name;
								row2["أسم_الوحدة"] = "المتوسطة";
								row2["الكمية"] = FUNC_DecimalNumQty(1);
								row2["سعر الوحدة"] = item_Items3.LastCost2;
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items3.LastCost2 * (decimal?)Convert.ToInt32(1)), DecimalNum);
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
					Item_Items item_Items4 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items4 != null)
					{
						int num3 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row3 in datatable.Rows)
						{
							if (row3["م"].ToString() == Convert.ToString(num3))
							{
								row3["الوحدة"] = item_Items4.Item_Unit2.Name;
								row3["أسم_الوحدة"] = "الكبرى";
								row3["الكمية"] = FUNC_DecimalNumQty(1);
								row3["سعر الوحدة"] = item_Items4.LastCost3;
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items4.LastCost3 * (decimal?)Convert.ToInt32(1)), DecimalNum);
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
				if (row["كود_الصنف"].ToString() != string.Empty)
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

	private void MenuExit_Click(object sender, EventArgs e)
	{
		Close();
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

	private void حركةصنفToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void FrmStoresTransfer_FormClosing(object sender, FormClosingEventArgs e)
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

	private void TxtDiscount_TextChanged(object sender, EventArgs e)
	{
		if (ISEVENTDISC)
		{
			decimal result = 0m;
			decimal result2 = 0m;
			decimal.TryParse(TxtDiscount.Text, out result);
			decimal.TryParse(TxtTotalAll.Text, out result2);
			if (result2 <= 0m)
			{
				result2 = 1m;
			}
			ISEVENTDISC = false;
			Cal_FinalResultTotals();
			decimal num = Math.Round(result / result2 * 100m, DecimalNum);
			txtdicoutprege.Value = num;
			ISEVENTDISC = true;
		}
	}

	private void txtdicoutprege_TextChanged(object sender, EventArgs e)
	{
		if (ISEVENTDISC)
		{
			decimal result = 0m;
			decimal result2 = 0m;
			decimal.TryParse(txtdicoutprege.Text, out result);
			decimal.TryParse(TxtTotalAll.Text, out result2);
			if (result < 0m)
			{
				result = 0m;
			}
			if (result > 100m)
			{
				result = 100m;
			}
			if (result2 <= 0m)
			{
				result2 = 1m;
			}
			ISEVENTDISC = false;
			Cal_FinalResultTotals();
			decimal num = Math.Round(result * result2 / 100m, DecimalNum);
			TxtDiscount.Text = num.ToString();
			txtdicoutprege.Text = result.ToString();
			ISEVENTDISC = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmStoresTransfer));
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
		this.اسمالموردToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.TxtLocation = new DevExpress.XtraEditors.TextEdit();
		this.label25 = new System.Windows.Forms.Label();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.label14 = new System.Windows.Forms.Label();
		this.TxtQuntity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtAllQuantity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtNet22 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtLastCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtAvergCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.رقمالفاتورةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.نوعالفاتورةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
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
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.TxtProfit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label7 = new System.Windows.Forms.Label();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label8 = new System.Windows.Forms.Label();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.TxtSellPrice3 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTotalAll = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtTotalBeforDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtSellPrice2 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtSellPrice1 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatBeforeDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.LblToStore = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtToStore = new DevExpress.XtraEditors.ButtonEdit();
		this.LblFromStore = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtFromStore = new DevExpress.XtraEditors.ButtonEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.حركةصنفToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuNewItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPreviewFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintQued = new System.Windows.Forms.ToolStripMenuItem();
		this.طباعةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.استعلامToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.label6 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtToStore.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtFromStore.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.اسمالموردToolStripMenuItem1, "اسمالموردToolStripMenuItem1");
		this.اسمالموردToolStripMenuItem1.Name = "اسمالموردToolStripMenuItem1";
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.FieldName = "أسم_الوحدة";
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
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
		this.txtdicoutprege.ReadOnly = true;
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.txtdicoutprege.TextChanged += new System.EventHandler(txtdicoutprege_TextChanged);
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
		resources.ApplyResources(this.TxtNet22, "TxtNet22");
		this.TxtNet22.DecimalDigits = 3;
		this.TxtNet22.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet22.Name = "TxtNet22";
		this.TxtNet22.ReadOnly = true;
		this.TxtNet22.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet22.UseCompatibleTextRendering = false;
		this.TxtNet22.Value = new decimal(new int[4]);
		this.TxtNet22.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet22.TextChanged += new System.EventHandler(TxtNet_TextChanged);
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
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
		editorButtonImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions9");
		editorButtonImageOptions.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemButtonEdit4.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
		resources.ApplyResources(this.رقمالفاتورةToolStripMenuItem1, "رقمالفاتورةToolStripMenuItem1");
		this.رقمالفاتورةToolStripMenuItem1.Name = "رقمالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.نوعالفاتورةToolStripMenuItem1, "نوعالفاتورةToolStripMenuItem1");
		this.نوعالفاتورةToolStripMenuItem1.Name = "نوعالفاتورةToolStripMenuItem1";
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
		resources.ApplyResources(this.groupBox3, "groupBox3");
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Controls.Add(this.TxtVat);
		this.groupBox3.Controls.Add(this.label5);
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
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn1, this.gridColumn2, this.shearch, this.gridColumn19, this.gridColumn20, this.gridColumn21, this.gridColumn3, this.gridColumn8, this.gridColumn9 });
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
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions10");
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
		this.gridColumn21.OptionsColumn.AllowEdit = false;
		this.gridColumn21.OptionsColumn.AllowFocus = false;
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
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions11");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
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
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions12");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"), resources.GetString("repositoryItemButtonEdit5.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit5.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonEdit5.Buttons6"), resources.GetObject("repositoryItemButtonEdit5.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit5.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit5.Buttons9"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEdit5.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
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
		resources.ApplyResources(this.TxtVat, "TxtVat");
		this.TxtVat.DecimalDigits = 3;
		this.TxtVat.ForeColor = System.Drawing.Color.Black;
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.ReadOnly = true;
		this.TxtVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4]);
		this.TxtVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		resources.ApplyResources(this.TxtTotals, "TxtTotals");
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.ReadOnly = true;
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.TxtNet.TextChanged += new System.EventHandler(TxtTotals_TextChanged);
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtTotalBeforDisc, "TxtTotalBeforDisc");
		this.TxtTotalBeforDisc.DecimalDigits = 3;
		this.TxtTotalBeforDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalBeforDisc.Name = "TxtTotalBeforDisc";
		this.TxtTotalBeforDisc.ReadOnly = true;
		this.TxtTotalBeforDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalBeforDisc.Value = new decimal(new int[4]);
		this.TxtTotalBeforDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.TxtDiscount.ReadOnly = true;
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
		resources.ApplyResources(this.LBlSuppAccount, "LBlSuppAccount");
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		resources.ApplyResources(this.label24, "label24");
		this.label24.Name = "label24";
		resources.ApplyResources(this.TxtNumNote, "TxtNumNote");
		this.TxtNumNote.Name = "TxtNumNote";
		this.TxtNumNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumNote.Properties.Appearance.Font");
		this.TxtNumNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNumNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumNote.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions13");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions14");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"), resources.GetString("repositoryItemButtonEdit3.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit3.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("repositoryItemButtonEdit3.Buttons6"), resources.GetObject("repositoryItemButtonEdit3.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit3.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit3.Buttons9"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions7.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions7, "editorButtonImageOptions15");
		editorButtonImageOptions7.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.LblToStore);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.TxtToStore);
		this.groupBox1.Controls.Add(this.LblFromStore);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.TxtFromStore);
		this.groupBox1.Controls.Add(this.LBlSuppAccount);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.TxtNumNote);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
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
		resources.ApplyResources(this.LblToStore, "LblToStore");
		this.LblToStore.Name = "LblToStore";
		resources.ApplyResources(this.label22, "label22");
		this.label22.Name = "label22";
		resources.ApplyResources(this.TxtToStore, "TxtToStore");
		this.TxtToStore.Name = "TxtToStore";
		this.TxtToStore.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtToStore.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtToStore.Properties.Appearance.Font");
		this.TxtToStore.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtToStore.Properties.Appearance.Options.UseBackColor = true;
		this.TxtToStore.Properties.Appearance.Options.UseFont = true;
		this.TxtToStore.Properties.Appearance.Options.UseForeColor = true;
		this.TxtToStore.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtToStore.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtToStore.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtToStore.Properties.ReadOnly = true;
		this.TxtToStore.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtToStore_ButtonClick);
		resources.ApplyResources(this.LblFromStore, "LblFromStore");
		this.LblFromStore.Name = "LblFromStore";
		resources.ApplyResources(this.label19, "label19");
		this.label19.Name = "label19";
		resources.ApplyResources(this.TxtFromStore, "TxtFromStore");
		this.TxtFromStore.Name = "TxtFromStore";
		this.TxtFromStore.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtFromStore.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtFromStore.Properties.Appearance.Font");
		this.TxtFromStore.Properties.Appearance.ForeColor = System.Drawing.Color.Green;
		this.TxtFromStore.Properties.Appearance.Options.UseBackColor = true;
		this.TxtFromStore.Properties.Appearance.Options.UseFont = true;
		this.TxtFromStore.Properties.Appearance.Options.UseForeColor = true;
		this.TxtFromStore.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtFromStore.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtFromStore.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtFromStore.Properties.ReadOnly = true;
		this.TxtFromStore.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtFromStore_ButtonClick);
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
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions16");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCode.Buttons"), resources.GetString("repositoryItemButtonEditCode.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCode.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("repositoryItemButtonEditCode.Buttons6"), resources.GetObject("repositoryItemButtonEditCode.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCode.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCode.Buttons9"))
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
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
		this.MenuPrintFatoura.Click += new System.EventHandler(BtnPrint_Click);
		resources.ApplyResources(this.MenuPreviewFatoura, "MenuPreviewFatoura");
		this.MenuPreviewFatoura.Name = "MenuPreviewFatoura";
		resources.ApplyResources(this.MenuPrintQued, "MenuPrintQued");
		this.MenuPrintQued.Name = "MenuPrintQued";
		resources.ApplyResources(this.طباعةToolStripMenuItem1, "طباعةToolStripMenuItem1");
		this.طباعةToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.MenuPrintQued, this.MenuPreviewFatoura, this.MenuPrintFatoura });
		this.طباعةToolStripMenuItem1.Name = "طباعةToolStripMenuItem1";
		resources.ApplyResources(this.استعلامToolStripMenuItem1, "استعلامToolStripMenuItem1");
		this.استعلامToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.رقمالفاتورةToolStripMenuItem1, this.اسمالموردToolStripMenuItem1, this.نوعالفاتورةToolStripMenuItem1 });
		this.استعلامToolStripMenuItem1.Name = "استعلامToolStripMenuItem1";
		resources.ApplyResources(this.MenuDelete, "MenuDelete");
		this.MenuDelete.Name = "MenuDelete";
		this.MenuDelete.Click += new System.EventHandler(BtnDelete_Click);
		resources.ApplyResources(this.MenuSave, "MenuSave");
		this.MenuSave.Name = "MenuSave";
		this.MenuSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.MenuNew, "MenuNew");
		this.MenuNew.Name = "MenuNew";
		this.MenuNew.Click += new System.EventHandler(BtnNew_Click);
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.طباعةToolStripMenuItem1, this.toolStripSeparator1, this.MenuNewItem, this.حركةصنفToolStripMenuItem, this.MenuSearchItem, this.toolStripSeparator4,
			this.استعلامToolStripMenuItem1, this.الكمياتToolStripMenuItem1, this.toolStripSeparator2, this.تخصيصToolStripMenuItem1, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		resources.ApplyResources(this.MenuEdit, "MenuEdit");
		this.MenuEdit.Name = "MenuEdit";
		this.MenuEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.ribbonPage2.Name = "ribbonPage2";
		resources.ApplyResources(this.ribbonPage2, "ribbonPage2");
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.groupBox2, "groupBox2");
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
		this.groupBox2.Controls.Add(this.TxtNet22);
		this.groupBox2.Controls.Add(this.TxtLastCost);
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.TxtAvergCost);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.TxtProfit);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.TxtSellPrice3);
		this.groupBox2.Controls.Add(this.TxtTotals);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.TxtTotalAll);
		this.groupBox2.Controls.Add(this.TxtTotalBeforDisc);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtNet);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.TxtSellPrice2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtSellPrice1);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.TxtDiscount);
		this.groupBox2.Controls.Add(this.TxtVatBeforeDisc);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmStoresTransfer";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmStoresTransfer_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtToStore.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtFromStore.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
