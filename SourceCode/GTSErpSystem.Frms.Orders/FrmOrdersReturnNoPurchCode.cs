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

public class FrmOrdersReturnNoPurchCode : XtraForm
{
	private Class_Items ITM = new Class_Items();

	private Class_Orders ORD = new Class_Orders();

	private Class_OrdersReturn orderTRN = new Class_OrdersReturn();

	private Class_Customer Cust = new Class_Customer();

	private Class_Suppliers Suppliers = new Class_Suppliers();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private GTSdbEntities db = new GTSdbEntities();

	private DataTable datatable = new DataTable();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private Class_Contract Contract = new Class_Contract();

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

	public bool ISEVENTDISC = true;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	private int SN = 1;

	private int ItmID = 0;

	private int StorId = 0;

	private string ItmName = string.Empty;

	private string UnitName = string.Empty;

	private IContainer components = null;

	private Label LBlSuppAccount;

	private Label label23;

	private TextEdit TxtNote;

	private Label label22;

	private TextEdit TxtPhone;

	private Label label21;

	private Label label20;

	private System.Windows.Forms.ComboBox TxtType;

	private Label label19;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private TextEdit TxtNumVat;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private Label label26;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private Label LblSup;

	private DateEdit TxtDate;

	private Label label24;

	private ToolStripMenuItem رقمالفاتورةToolStripMenuItem1;

	private GridColumn gridColumn18;

	private GridColumn gridColumn17;

	private GridColumn gridColumn16;

	private GridColumn gridColumn15;

	private GridColumn gridColumn14;

	private GridColumn gridColumn13;

	private TextEdit TxtNumNote;

	private GridColumn gridColumn11;

	private Label label18;

	private GroupBox groupBox2;

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

	private NumericEditBox TxtNet;

	private NumericEditBox TxtLastCost;

	private SimpleButton BtnEdit;

	private Label label10;

	private NumericEditBox TxtAvergCost;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private NumericEditBox TxtProfit;

	private SimpleButton BtnNew;

	private Label label8;

	private SimpleButton BtnDelete;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtSellPrice3;

	private NumericEditBox TxtTotals;

	private SimpleButton BtnExit;

	private Label label11;

	private NumericEditBox TxtTotalDiscount;

	private Label label12;

	private Label label1;

	private NumericEditBox TxtSellPrice2;

	private Label label3;

	private NumericEditBox TxtSellPrice1;

	private Label label4;

	private NumericEditBox TxtDiscount;

	private NumericEditBox TxtVatDisc;

	private Label label5;

	private Label label6;

	private RibbonPage ribbonPage2;

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

	private ToolStripMenuItem استعلامToolStripMenuItem1;

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

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private ButtonEdit TxtSuppliers;

	private Label label17;

	private ButtonEdit TxtCode;

	private GridColumn gridColumn12;

	private GroupBox groupBox1;

	private GridColumn gridColumn19;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn1;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private GridColumn gridColumn2;

	private GridColumn shearch;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

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

	private GridColumn gridColumn5;

	private GridColumn gridColumn9;

	private RepositoryItemTextEdit repositoryItemTextEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit2;

	private RepositoryItemDateEdit repositoryItemDateEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit3;

	private RepositoryItemPictureEdit repositoryItemPictureEdit1;

	private CardView cardView1;

	private GridColumn gridColumn10;

	private GroupBox groupBox3;

	private GridColumn gridColumn23;

	private NumericEditBox TxtCost;

	private NumericEditBox TxtTabocc;

	private Label label29;

	private NumericEditBox TxtAllVat;

	private Label label28;

	private NumericEditBox TxtAllTotal;

	private Label label27;

	private Label label30;

	private GridColumn gridColumn24;

	private Label label39;

	private System.Windows.Forms.ComboBox CBProject;

	private GridColumn gridColumn25;

	private GridColumn gridColumn26;

	private GridColumn gridColumn27;

	private GridColumn gridColumn28;

	private GridColumn gridColumn29;

	private GridColumn gridColumn30;

	private GridColumn gridColumn31;

	private NumericEditBox TxtAllDiscount;

	private Label label2;

	private GridColumn gridColumn32;

	private GridColumn gridColumn33;

	public FrmOrdersReturnNoPurchCode()
	{
		InitializeComponent();
		TxtDigital();
		SetInitialRow();
		Bind();
		GroupPermission();
		BtnDefalte();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "ItemCode"));
		string text2 = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "ItemName"));
		decimal num = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "Quantity"));
		decimal num2 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "UnitPrice"));
		if (!(text == "") && !(text2 == "") && !(num <= 0m) && !(num2 <= 0m))
		{
			return;
		}
		Item_Items item_Items = orderTRN.GetItems(text).FirstOrDefault();
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
		try
		{
			if (e.Column.FieldName == "ItemCode" || e.Column.FieldName == "ItemName")
			{
				InsertItemRow(e);
			}
			else
			{
				UpdateItemRow(e.Column.FieldName, e);
			}
			if (e.Column.FieldName == "UnitNum")
			{
				foreach (DataRow row in datatable.Rows)
				{
					int num = Convert.ToInt32(row["SN"]);
					if (num == SN)
					{
						row["UnitNum"] = Convert.ToDecimal(e.Value);
					}
				}
			}
			if (e.Column.FieldName == "Net")
			{
				UpdateItemRowByNet(e);
			}
			InsertNewRow();
			ArrangeDatatable();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			gridControl1.DataSource = datatable;
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[1];
			gridView1.ShowEditor();
		}
		catch (Exception)
		{
		}
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
			if (focusedRowCellDisplayText != string.Empty)
			{
				ItmID = Convert.ToInt32(gridView1.GetFocusedRowCellDisplayText("ItemID"));
			}
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("ItemName");
			if (focusedRowCellDisplayText2 != string.Empty)
			{
				ItmName = gridView1.GetFocusedRowCellDisplayText("ItemName");
			}
			string codeItm = Convert.ToString(gridView1.GetFocusedRowCellDisplayText("ItemCode"));
			UnitName = gridView1.GetFocusedRowCellDisplayText("UnitName");
			string StoreName = gridView1.GetFocusedRowCellDisplayText("StoreName");
			StorId = (from x in gTSdbEntities.Account_Stores
				where x.Store_Name == StoreName
				select x.ID).FirstOrDefault();
			GetAllitems_alternatives(ItmID, StorId, UnitName);
			CodeItm = codeItm;
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("SN");
			if (!int.TryParse(focusedRowCellDisplayText3, out SN))
			{
				SN = 1;
			}
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
		SN = 1;
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
			FrmSearchOrderReturnNoPurchCode frmSearchOrderReturnNoPurchCode = new FrmSearchOrderReturnNoPurchCode();
			frmSearchOrderReturnNoPurchCode.ShowDialog();
			if (frmSearchOrderReturnNoPurchCode.CustCode > 0)
			{
				int custCode = frmSearchOrderReturnNoPurchCode.CustCode;
				Order_OrderReturn order_OrderReturn = orderTRN.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<Order_OrderReturnDetails> itemsPurchesByID = orderTRN.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (Order_OrderReturnDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = orderTRN.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemCode"] = item_Items.Item_code;
					dataRow["ItemName"] = item_Items.item_Name;
					if (item.ItemUnitType == "الصغرى")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "المتوسطة")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "الكبرى")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					dataRow["ItemID"] = item.ItemID;
					dataRow["UnitType"] = item.ItemUnitType;
					dataRow["Storename"] = (from x in db.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["Quantity"] = FUNC_DecimalNumQty(item.Quantity);
					dataRow["AvergItem"] = item.CostAverg;
					dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
					dataRow["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.UnitPrice));
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetUnitPrice));
					dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
					dataRow["DeleteItem"] = 0;
					dataRow["AmountDisc"] = FUNC_DecimalNum(Convert.ToDecimal(item.AmountDisc));
					dataRow["PercentDisc"] = FUNC_DecimalNum(Convert.ToDecimal(item.PercentDisc));
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
						dataRow["Total"] = FUNC_DecimalNum(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT));
						dataRow["SumPriceDisc"] = Math.Round(Convert.ToDecimal(item.NetTotalPrice), DecimalNum);
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice));
						dataRow["IsItemTax"] = 1;
						dataRow["Tax_Value"] = item_Items.Tax_Value;
					}
					else
					{
						dataRow["IsItemTax"] = 0;
						dataRow["Tax_Value"] = 0;
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
						dataRow["Total"] = FUNC_DecimalNum(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT));
						dataRow["SumPriceDisc"] = Math.Round(Convert.ToDecimal(item.NetTotalPrice), DecimalNum);
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice) + Convert.ToDecimal(item.VAT_Discount));
					}
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				InsertNewRow();
				PrintID = custCode;
				TxtType.SelectedValue = order_OrderReturn.Order_Paymant_Type;
				CBCostCenter.SelectedValue = order_OrderReturn.CostCentersID;
				TxtCode.Text = order_OrderReturn.PurBranchID.ToString();
				LblSup.Text = order_OrderReturn.SupplierID.ToString();
				LBlSuppAccount.Text = Cust.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
				TxtSuppliers.Text = order_OrderReturn.SupplierName;
				TxtPhone.Text = order_OrderReturn.SupplierPhone;
				TxtNumVat.Text = order_OrderReturn.SupplierVatNum;
				TxtDate.DateTime = Convert.ToDateTime(order_OrderReturn.Purchases_Date);
				TxtNote.Text = order_OrderReturn.Note;
				TxtNumNote.Text = order_OrderReturn.NoteNum;
				TxtTotals.Text = Convert.ToString(order_OrderReturn.TotalPrices);
				TxtVat.Text = Convert.ToString(order_OrderReturn.Tax);
				TxtTotals.Text = order_OrderReturn.Safy.ToString();
				TxtDiscount.Text = order_OrderReturn.DiscountNum.ToString();
				txtdicoutprege.Text = order_OrderReturn.DiscountPerantage.ToString();
				TxtTotalDiscount.Text = order_OrderReturn.TotalPrices_Discount.ToString();
				TxtVatDisc.Text = order_OrderReturn.Tax_Discount.ToString();
				TxtNet.Text = order_OrderReturn.Net.ToString();
				TxtCost.Text = order_OrderReturn.CostAverg.ToString();
				TxtTabocc.Text = order_OrderReturn.TobaccoTax.ToString();
				TxtAllVat.Text = order_OrderReturn.AllTax.ToString();
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = false;
				FiterDatatableItemsUpdate();
				ArrangeDatatable();
				IsEnableFalse();
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
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
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustCode > 0)
			{
				int FrmID = frmSearchCustomer.CustID;
				Account_CustSup account_CustSup = gTSdbEntities.Account_CustSup.FirstOrDefault((Account_CustSup x) => x.ID == FrmID);
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

	private void BtnDefalte()
	{
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		TxtDate.DateTime = DateTime.Now;
		TxtSuppliers.Text = Suppliers.GetFirstSuppliers();
		TxtDate.Focus();
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		txtdicoutprege.Controls[0].Visible = false;
		if (LoginDetails.FrmOrderID != 0)
		{
			SearchFrm(LoginDetails.FrmOrderID);
		}
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 16);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 16);
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
	}

	public void InsertItemRow(CellValueChangedEventArgs e)
	{
		string code = Convert.ToString(e.Value);
		Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(code).FirstOrDefault();
		if (get_Item_ByCodeALL_Result == null)
		{
			return;
		}
		foreach (DataRow row in datatable.Rows)
		{
			int num = Convert.ToInt32(row["SN"]);
			if (num != SN)
			{
				continue;
			}
			row["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
			row["ItemCode"] = get_Item_ByCodeALL_Result.Item_code;
			row["ItemName"] = get_Item_ByCodeALL_Result.item_Name;
			row["ItemNote"] = string.Empty;
			row["UnitType"] = "الصغرى";
			row["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
			row["Quantity"] = 1;
			List<Account_Stores> stores = Contract.Get_Stores();
			Account_Stores account_Stores = stores.FirstOrDefault((Account_Stores store) => store.ID == LoginDetails.StoreID);
			if (account_Stores != null)
			{
				row["StoreName"] = account_Stores.Store_Name;
			}
			else
			{
				row["StoreName"] = string.Empty;
			}
			row["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
			row["OrginalPrice"] = row["UnitPrice"];
			row["SumPrice"] = row["UnitPrice"];
			row["PercentDisc"] = 0;
			row["AmountDisc"] = 0;
			row["UnitPriceDisc"] = row["UnitPrice"];
			row["SumPriceDisc"] = row["UnitPriceDisc"];
			row["IsItemTax"] = get_Item_ByCodeALL_Result.Is_Tax;
			row["Tax_Value"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.Tax_Value);
			if (!LoginDetails.IsVat)
			{
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				row["ItemVatDisc"] = 0;
				row["Net"] = row["SumPriceDisc"];
			}
			else if (Convert.ToBoolean(row["IsItemTax"]))
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
					row["SumPrice"] = Convert.ToDecimal(row["Total"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
					row["UnitPrice"] = row["SumPrice"];
					row["ItemVat"] = Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]);
					row["ItemVatDisc"] = row["ItemVat"];
					row["Net"] = row["Total"];
					row["UnitPriceDisc"] = row["UnitPrice"];
					row["SumPriceDisc"] = row["SumPrice"];
				}
				else
				{
					row["ItemVat"] = Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m);
					row["Total"] = Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]);
					row["ItemVatDisc"] = row["ItemVat"];
					row["Net"] = row["Total"];
				}
			}
			else
			{
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				row["ItemVatDisc"] = 0;
				row["Net"] = row["SumPriceDisc"];
			}
			row["AvergItem"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.Average_cost);
			row["SumAvergItem"] = row["AvergItem"];
			row["DeleteItem"] = 0;
			row["Done"] = 1;
		}
	}

	public void UpdateItemRow(string TxtChange, CellValueChangedEventArgs e)
	{
		decimal num = Convert.ToDecimal(e.Value);
		bool flag = false;
		bool flag2 = false;
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToInt32(row["SN"]) == SN)
			{
				decimal quantity = ((TxtChange == "Quantity") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"])));
				decimal unitPrice = ((TxtChange == "UnitPrice") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"])));
				if (TxtChange == "UnitPrice" && LoginDetails.ItemTotalWithVat)
				{
					row["OrginalPrice"] = num * (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
				}
				decimal num2 = ((TxtChange == "PercentDisc") ? FUNC_DecimalNum(num) : FUNC_DecimalNum(Convert.ToDecimal(row["PercentDisc"])));
				if (num2 > 100m)
				{
					break;
				}
				flag2 = TxtChange == "PercentDisc" || TxtChange == "AmountDisc";
				decimal amountDisc = ((TxtChange == "AmountDisc") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["AmountDisc"])));
				flag = TxtChange == "AmountDisc";
				decimal avergItem = ((TxtChange == "AvergItem") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"])));
				UpdateRowFactionAccount(quantity, unitPrice, flag2, flag, num2, amountDisc, avergItem);
			}
		}
	}

	public void UpdateRowFactionAccount(decimal Quantity, decimal UnitPrice, bool IsDisc, bool IsDiscAmount, decimal PercentDisc, decimal AmountDisc, decimal AvergItem)
	{
		decimal value = UnitPrice * Quantity;
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToInt32(row["SN"]) != SN)
			{
				continue;
			}
			bool flag = row["IsItemTax"] != DBNull.Value && Convert.ToBoolean(row["IsItemTax"]);
			row["Quantity"] = Quantity;
			row["UnitPrice"] = FUNC_DecimalNum(UnitPrice);
			row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
			row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
			row["AvergItem"] = AvergItem;
			if (!LoginDetails.IsVat)
			{
				row["SumPrice"] = UnitPrice * Quantity;
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (IsDisc && IsDiscAmount)
				{
					PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(row["SumPrice"]) * 100m);
					row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
				}
				else if (IsDisc && !IsDiscAmount)
				{
					AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(row["SumPrice"]));
					row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
				}
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["SumPrice"]) * (PercentDisc / 100m));
				row["ItemVatDisc"] = 0;
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
				row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * (UnitPrice * Quantity) / 100m);
			}
			else if (flag)
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num = Convert.ToDecimal(LoginDetails.PerVat) / 100m;
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["OrginalPrice"]) * Quantity);
					row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) / (num + 1m));
					row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]));
					if (IsDisc && IsDiscAmount)
					{
						PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(row["SumPrice"]) * 100m);
						row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
					}
					else if (IsDisc && !IsDiscAmount)
					{
						AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(row["SumPrice"]));
						row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
					}
					row["SumPriceDisc"] = Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(AmountDisc);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * num);
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
				}
				else
				{
					row["SumPrice"] = FUNC_DecimalNum(value);
					if (IsDisc && IsDiscAmount)
					{
						PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(row["SumPrice"]) * 100m);
						row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
					}
					else if (IsDisc && !IsDiscAmount)
					{
						AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(row["SumPrice"]));
						row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
					}
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["SumPrice"]) * (PercentDisc / 100m));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
					row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * (UnitPrice * Quantity) / 100m);
				}
			}
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * Quantity);
		}
	}

	public void UpdateItemRowByNet(CellValueChangedEventArgs e)
	{
		decimal num = Convert.ToDecimal(e.Value);
		foreach (DataRow row in datatable.Rows)
		{
			int num2 = Convert.ToInt32(row["SN"]);
			if (num2 != SN || !(row["ItemCode"].ToString() != string.Empty))
			{
				continue;
			}
			if (!LoginDetails.IsVat)
			{
				row["Net"] = num;
				row["SumPrice"] = num;
				row["SumPriceDisc"] = num;
				row["Total"] = num;
				row["UnitPrice"] = num / Convert.ToDecimal(row["Quantity"]);
				row["OrginalPrice"] = row["UnitPrice"];
				row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPrice"]);
				row["ItemVat"] = 0;
				row["ItemVatDisc"] = 0;
				row["AmountDisc"] = 0;
				row["PercentDisc"] = 0;
			}
			else if (Convert.ToBoolean(row["IsItemTax"]))
			{
				decimal num3 = num / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
				decimal value = num3 / Convert.ToDecimal(row["Quantity"]);
				row["Net"] = num;
				row["ItemVat"] = num - num3;
				row["SumPrice"] = num3;
				row["UnitPrice"] = FUNC_DecimalNum(value);
				row["UnitPriceDisc"] = row["UnitPrice"];
				row["Total"] = num;
				row["SumPriceDisc"] = row["SumPrice"];
				row["ItemVat"] = num - Convert.ToDecimal(row["UnitPrice"]);
				row["ItemVatDisc"] = row["ItemVat"];
				row["AmountDisc"] = 0;
				row["PercentDisc"] = 0;
				string text = row["OrginalPrice"].ToString();
				if (LoginDetails.ItemTotalWithVat)
				{
					row["OrginalPrice"] = Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
				}
				else
				{
					row["OrginalPrice"] = row["UnitPrice"];
				}
			}
			break;
		}
	}

	private void Cal_FinalResultTotals(decimal Per, string DicountType)
	{
		try
		{
			if (datatable.Rows.Count < 1)
			{
				TxtTotals.Text = "0.00";
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtAllDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtVat.Text = "0.000";
				TxtVatDisc.Text = "0.00";
				TxtTabocc.Text = "0.00";
				TxtCost.Text = "0.00";
			}
			else if (!ORD.IsVatActive())
			{
				if (Per == 0m)
				{
					No_Discount_NoVat();
				}
				else
				{
					DiscountPercent_NoVat(Per, DicountType);
				}
			}
			else if (LoginDetails.ItemTotalWithVat)
			{
				if (Per == 0m)
				{
					No_Discount_PriceWithVat();
				}
				else
				{
					DiscountPercent_PriceWithVat(Per, DicountType);
				}
			}
			else if (Per == 0m)
			{
				No_Discount_Vat();
			}
			else
			{
				DiscountPercent_Vat(Per, DicountType);
			}
			gridControl1.DataSource = datatable;
		}
		catch (Exception)
		{
		}
	}

	private void No_Discount_NoVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]);
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = Convert.ToDecimal(row["UnitPrice"]) * num;
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = row["UnitPrice"];
					row["ItemVatDisc"] = row["ItemVat"];
					row["SumPriceDisc"] = row["SumPrice"];
					row["Net"] = row["Total"];
				}
				else
				{
					row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m);
					row["SumPriceDisc"] = Convert.ToDecimal(row["UnitPriceDisc"]) * num;
					row["ItemVatDisc"] = row["ItemVat"];
					row["Net"] = row["SumPriceDisc"];
				}
				row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * num;
			}
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num2);
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtNet.Text = TxtTotals.Text.ToString();
	}

	private void No_Discount_Vat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]);
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = row["UnitPrice"];
					row["ItemVatDisc"] = row["ItemVat"];
					row["SumPriceDisc"] = row["SumPrice"];
					row["Net"] = row["Total"];
				}
				else
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Net"] = Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]);
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		TxtCost.Text = Convert.ToString(num2);
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num3);
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num4.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = num5.ToString();
		TxtNet.Text = (num3 + num5).ToString();
	}

	private void No_Discount_PriceWithVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]);
			decimal num2 = Convert.ToInt32(row["ItemID"]);
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				decimal num3 = ((row["Total"] != DBNull.Value) ? Convert.ToDecimal(row["Total"]) : 0m);
				decimal num4 = ((row["Tax_Value"] != DBNull.Value) ? Convert.ToDecimal(row["Tax_Value"]) : 0m);
				decimal value = num3 / (num4 / 100m + 1m);
				row["SumPrice"] = FUNC_DecimalNum(value);
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]));
				row["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) / num);
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				}
				else
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		TxtCost.Text = Convert.ToString(num5);
		decimal num6 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num6);
		decimal num7 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num7.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = TxtVat.Text;
		TxtNet.Text = (num6 + num7).ToString();
	}

	private void DiscountPercent_NoVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]);
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				}
				else
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num2);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num3 = Convert.ToDecimal(row2["Quantity"]);
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPrice"]) * num3);
				row2["ItemVat"] = 0;
				row2["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPrice"]));
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num3);
				row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["ItemVat"]));
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num3);
			}
		}
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	private void DiscountPercent_Vat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]);
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
					row["ItemVatDisc"] = row["ItemVat"];
					row["SumPriceDisc"] = row["SumPrice"];
					row["Net"] = row["Total"];
				}
				else
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat.Text = Convert.ToString(num2);
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num3);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(TxtTotals.Text) + num2) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num4 = Convert.ToDecimal(row2["Quantity"]);
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num4);
				row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m));
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num4);
			}
		}
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVatDisc.Text = Convert.ToString(num5);
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	private void DiscountPercent_PriceWithVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]);
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m));
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]));
				row["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) / num);
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				}
				else
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat.Text = Convert.ToString(num2);
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num3);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(TxtTotals.Text) + num2) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num4 = Convert.ToDecimal(row2["Quantity"]);
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num4);
				row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m));
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num4);
			}
		}
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVatDisc.Text = Convert.ToString(num5);
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	public void Bind()
	{
		try
		{
			TxtType.DataSource = orderTRN.GetAccountType();
			TxtType.DisplayMember = "Name";
			TxtType.ValueMember = "ID";
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
			if (dataRow["Quantity"].ToString() == "0" || dataRow["UnitPrice"].ToString() == "0")
			{
				dataRow.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
		gridControl1.DataSource = datatable;
	}

	public void ArrangeDatatable()
	{
		int num = 1;
		foreach (DataRow row in datatable.Rows)
		{
			row["SN"] = num;
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
				TxtLocation.Text = string.Empty;
			}
		}
		catch (Exception)
		{
		}
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("ItemID", typeof(int));
		datatable.Columns.Add("ItemCode", typeof(string));
		datatable.Columns.Add("ItemName", typeof(string));
		datatable.Columns.Add("ItemNote", typeof(string));
		datatable.Columns.Add("UnitType", typeof(string));
		datatable.Columns.Add("UnitName", typeof(string));
		datatable.Columns.Add("Quantity", typeof(decimal));
		datatable.Columns.Add("StoreName", typeof(string));
		datatable.Columns.Add("OrginalPrice", typeof(decimal));
		datatable.Columns.Add("UnitPrice", typeof(decimal));
		datatable.Columns.Add("SumPrice", typeof(decimal));
		datatable.Columns.Add("ItemVat", typeof(decimal));
		datatable.Columns.Add("Total", typeof(decimal));
		datatable.Columns.Add("PercentDisc", typeof(decimal));
		datatable.Columns.Add("AmountDisc", typeof(decimal));
		datatable.Columns.Add("UnitPriceDisc", typeof(decimal));
		datatable.Columns.Add("SumPriceDisc", typeof(decimal));
		datatable.Columns.Add("ItemVatDisc", typeof(decimal));
		datatable.Columns.Add("Net", typeof(decimal));
		datatable.Columns.Add("AvergItem", typeof(decimal));
		datatable.Columns.Add("SumAvergItem", typeof(decimal));
		datatable.Columns.Add("Tax_Value", typeof(decimal));
		datatable.Columns.Add("DeleteItem", typeof(int));
		datatable.Columns.Add("SN", typeof(int));
		datatable.Columns.Add("Done", typeof(int));
		datatable.Columns.Add("IsItemTax", typeof(bool));
		datatable.Columns.Add("SumPriceInstall", typeof(decimal));
		datatable.Columns.Add("SumPriceInstallDisc", typeof(decimal));
		datatable.Columns.Add("SellQuantity", typeof(decimal));
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridControl1.ProcessGridKey += gridControl1_ProcessGridKey;
		gridView1.RowCellClick += gridView1_RowCellClick;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		gridView1.CustomDrawFooterCell += gridView1_CustomDrawFooterCell;
		InsertNewRow();
	}

	public void InsertNewRow()
	{
		CheckFfistRow();
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["ItemID"] = 0;
		dataRow["ItemCode"] = string.Empty;
		dataRow["ItemName"] = string.Empty;
		dataRow["ItemNote"] = string.Empty;
		dataRow["UnitType"] = "الصغرى";
		dataRow["UnitName"] = string.Empty;
		dataRow["Quantity"] = 1;
		List<Account_Stores> stores = Contract.Get_Stores();
		Account_Stores account_Stores = stores.FirstOrDefault((Account_Stores store) => store.ID == LoginDetails.StoreID);
		if (account_Stores != null)
		{
			dataRow["StoreName"] = account_Stores.Store_Name;
		}
		else
		{
			dataRow["StoreName"] = string.Empty;
		}
		dataRow["OrginalPrice"] = 0;
		dataRow["UnitPrice"] = 0;
		dataRow["SumPrice"] = 0;
		dataRow["ItemVat"] = 0;
		dataRow["Total"] = 0;
		dataRow["PercentDisc"] = 0;
		dataRow["AmountDisc"] = 0;
		dataRow["UnitPriceDisc"] = 0;
		dataRow["SumPriceDisc"] = 0;
		dataRow["ItemVatDisc"] = 0;
		dataRow["Net"] = 0;
		dataRow["AvergItem"] = 0;
		dataRow["SumAvergItem"] = 0;
		dataRow["Tax_Value"] = 0;
		dataRow["IsItemTax"] = false;
		dataRow["DeleteItem"] = 1;
		dataRow["Done"] = 0;
		datatable.Rows.Add(dataRow);
		ArrangeDatatable();
		gridControl1.DataSource = datatable;
	}

	public int IsItemPriceZero()
	{
		int result = 1;
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["ItemCode"]);
			if (Convert.ToDecimal(row["UnitPrice"]) <= 0m && text != "")
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ الفاتورة لوجود اصناف سعر الوحدة تساوي صفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				result = 0;
				break;
			}
		}
		return result;
	}

	public void FiterDatatableItemsUpdate()
	{
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["ItemCode"]);
			if (text == "")
			{
				row.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
		gridControl1.DataSource = datatable;
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
		TxtNumVat.Text = "";
		TxtType.Text = "";
		TxtNote.Text = "";
		TxtDiscount.Text = "";
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtType.Text = "نقدى";
		TxtSuppliers.Text = Suppliers.GetFirstSuppliers();
		TxtNumNote.Text = "";
		TxtTotalDiscount.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		TxtTafqit.Text = "";
		TxtTotals.Text = "0.00";
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
		TxtCost.Text = "0.00";
		TxtDate.DateTime = DateTime.Now;
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
		TxtDiscount.Enabled = true;
		TxtNote.Enabled = true;
		TxtType.Enabled = true;
		TxtDate.Enabled = true;
		TxtPhone.Text = "";
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
		InsertNewRow();
		IsEnableTrue();
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtSuppliers.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 16))
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
							if (CBCostCenter.SelectedValue == null)
							{
								CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
							}
							FiterDatatableItemsUpdate();
							num = orderTRN.InsertPurch(out var Code, 0, Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), TxtNote.Text, TxtNumNote.Text, ISWait: false, Convert.ToDecimal(TxtCost.Text), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), ISPurReturnNum: false, Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), 0m, 0m, 0, 0m);
							orderTRN.InsertItemsNoPurchCode(datatable, num, 0, 0, IsWait: false);
							gridControl1.DataSource = datatable;
							gridView1.OptionsBehavior.Editable = false;
							orderTRN.InsertTran(out var TRNIDDS, Code, Convert.ToInt32(TxtType.SelectedValue), TxtSuppliers.Text, TxtNumNote.Text, TxtDate.DateTime);
							if (Convert.ToInt32(TxtType.SelectedValue) == 2)
							{
								orderTRN.InsertTranDetailsAjila(TRNIDDS, Code, Convert.ToInt32(LBlSuppAccount.Text), TxtSuppliers.Text, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtNet.Text), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue));
							}
							else
							{
								orderTRN.InsertTranDetailsCash(TRNIDDS, Code, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtNet.Text), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), Convert.ToDecimal(TxtNet.Text), 0m);
							}
							if (num > 0)
							{
								PrintID = num;
								BtnSave.Enabled = false;
								BtnEdit.Enabled = true;
								BtnDelete.Enabled = true;
								IsEnableFalse();
								TxtCode.Text = Code.ToString();
								switch (MessageBox.Show("تم حفظ بيانات فاتورة مرتجعات المبيعات بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
								{
								case DialogResult.Yes:
									Print();
									break;
								}
							}
							else
							{
								MessageBox.Show("تم حفظ بيانات فاتورة مرتجعات المبيعات رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
					MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة مرتجعات المبيعات بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 16))
		{
			if (TxtDate.ReadOnly)
			{
				IsEnableTrue();
				BtnEdit.Text = "F5 حفظ";
				InsertNewRow();
				gridControl1.Enabled = true;
				gridView1.OptionsBehavior.Editable = true;
				{
					foreach (DataRow row in datatable.Rows)
					{
						SN = Convert.ToInt32(row["SN"]);
					}
					return;
				}
			}
			int num = 0;
			int num2 = 0;
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
				if (CBCostCenter.SelectedValue == null)
				{
					CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
					CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
				}
				ArrangeDatatable();
				FiterDatatableItemsUpdate();
				num2 = orderTRN.DeleteTranDetails(Convert.ToInt32(TxtCode.Text), 13);
				num = orderTRN.UpdatePurch(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), TxtNote.Text, TxtNumNote.Text, ISWait: false, Convert.ToDecimal(TxtCost.Text), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), 0m, 0m);
				decimal num3 = Math.Round((Convert.ToDecimal(txtdicoutprege.Text) * Convert.ToDecimal(TxtVat.Text) / 100m - Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)) * -1m, DecimalNum);
				decimal num4 = Math.Round((Convert.ToDecimal(txtdicoutprege.Text) * Convert.ToDecimal(TxtTotals.Text) / 100m - Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum)) * -1m, DecimalNum);
				orderTRN.deleteItemsByPurcahesId(num);
				orderTRN.InsertItemsNoPurchCode(datatable, num, 0, 0, IsWait: false);
				orderTRN.UpdateTran(out var TRNIDDS, Convert.ToInt32(TxtCode.Text), Convert.ToInt32(TxtType.SelectedValue), TxtSuppliers.Text, TxtNumNote.Text, TxtDate.DateTime, 13);
				if (Convert.ToInt32(TxtType.SelectedValue) == 2)
				{
					orderTRN.UpdateTranDetailsAjila(TRNIDDS, Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LBlSuppAccount.Text), TxtSuppliers.Text, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtNet.Text), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue));
				}
				else
				{
					orderTRN.UpdateTranDetailsCash(TRNIDDS, Convert.ToInt32(TxtCode.Text), Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtNet.Text), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), 0m, 0m);
				}
				if (num > 0)
				{
					PrintID = num;
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					IsEnableFalse();
					BtnEdit.Text = "Edit تعديل";
					gridView1.OptionsBehavior.Editable = false;
					switch (MessageBox.Show("تم تعديل فاتورة مرتجعات المبيعات بنجاح \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						Print();
						break;
					}
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات فاتورة مرتجعات المبيعات رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 17))
		{
			int num = 0;
			int num2 = 0;
			if (TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة مرتجعات المبيعات رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = orderTRN.Delete(Convert.ToInt32(TxtCode.Text), datatable);
					orderTRN.DeleteTranDetails(Convert.ToInt32(TxtCode.Text), 13);
					num2 = orderTRN.DeleteTran(Convert.ToInt32(TxtCode.Text), 13);
					if (num > 0 && num2 > 0)
					{
						MessageBox.Show("تم حذف بيانات فاتورة مرتجعات المبيعات رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						gridView1.OptionsBehavior.Editable = true;
						IsEnableTrue();
						BtnDelete.Enabled = false;
						BtnEdit.Enabled = false;
						BtnSave.Enabled = true;
						InsertNewRow();
						TxtSuppliers.Focus();
						PrintID = 0;
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات فاتورة مرتجعات المبيعات رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة مرتجعات المبيعات بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 17))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_OrderReturn", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@ID", PrintID);
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlCommand.Parameters.AddWithValue("@CreditNote", 0);
				sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
				sqlConnection.Close();
				if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					if (LoginDetails.Lang == "ar-EG")
					{
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintOrdersReturn2.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintOrdersReturn2.mrt");
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
					string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), TxtTotalDiscount.Text, TxtVatDisc.Text);
					stiReport["EncHash"] = value;
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
		txtdicoutprege.ReadOnly = true;
		TxtNote.ReadOnly = true;
		TxtType.Enabled = false;
		TxtDate.ReadOnly = true;
		TxtSuppliers.ReadOnly = true;
		TxtNumVat.ReadOnly = true;
		TxtPhone.ReadOnly = true;
		TxtNumNote.ReadOnly = true;
		CBCostCenter.Enabled = false;
		TxtDiscount.BackColor = Color.White;
		txtdicoutprege.BackColor = Color.White;
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
				LoginDetails.ItemSearchID = orderTRN.GetItmID(CodeItm);
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
				int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				foreach (DataRow row in datatable.Rows)
				{
					if (row["SN"].ToString() == Convert.ToString(num))
					{
						if (row["ItemCode"].ToString() != "")
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
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
				foreach (string item in LoginDetails.ItemSearchAll2)
				{
					string text = item.ToString();
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(text).FirstOrDefault();
					if (get_Item_ByCodeALL_Result == null)
					{
						continue;
					}
					DataRow dataRow2 = null;
					dataRow2 = datatable.NewRow();
					dataRow2["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
					dataRow2["ItemCode"] = get_Item_ByCodeALL_Result.Item_code;
					dataRow2["ItemName"] = get_Item_ByCodeALL_Result.item_Name;
					dataRow2["UnitType"] = "الصغرى";
					dataRow2["Quantity"] = 1;
					dataRow2["AmountDisc"] = 0;
					dataRow2["PercentDisc"] = 0;
					dataRow2["Done"] = 1;
					dataRow2["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
					dataRow2["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost;
					dataRow2["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost;
					dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
					dataRow2["SumPrice"] = dataRow2["UnitPrice"];
					if (!LoginDetails.IsVat)
					{
						dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
						dataRow2["Total"] = dataRow2["UnitPrice"];
						dataRow2["OrginalPrice"] = dataRow2["Total"];
					}
					else if (LoginDetails.ItemTotalWithVat)
					{
						decimal num2 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
						dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall / (decimal?)num2);
						dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
						dataRow2["OrginalPrice"] = dataRow2["Total"];
						dataRow2["Tax_Value"] = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value);
						dataRow2["IsItemTax"] = get_Item_ByCodeALL_Result.Is_Tax;
						dataRow2["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["Total"]) / num2);
						dataRow2["UnitPrice"] = dataRow2["SumPrice"];
						dataRow2["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["Total"]) - Convert.ToDecimal(dataRow2["SumPrice"]));
						dataRow2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["ItemVat"]));
						dataRow2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["Total"]));
						dataRow2["UnitPriceDisc"] = dataRow2["UnitPrice"];
						dataRow2["SumPriceDisc"] = dataRow2["SumPrice"];
					}
					else
					{
						dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
						dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
						dataRow2["OrginalPrice"] = dataRow2["Total"];
						dataRow2["SumPrice"] = dataRow2["UnitPrice"];
						DataRow dataRow3 = dataRow2;
						decimal value = Convert.ToDecimal(dataRow2["SumPrice"]);
						decimal? num3 = get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100;
						dataRow3["ItemVat"] = FUNC_DecimalNum((decimal?)value * num3);
						dataRow2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["SumPrice"]) + Convert.ToDecimal(dataRow2["ItemVat"]));
						dataRow2["IsItemTax"] = 1;
						dataRow2["Tax_Value"] = get_Item_ByCodeALL_Result.Tax_Value;
					}
					if (get_Item_ByCodeALL_Result.MediumBarCode1 == text || get_Item_ByCodeALL_Result.MediumBarCode2 == text || get_Item_ByCodeALL_Result.MediumBarCode3 == text)
					{
						dataRow2["UnitType"] = "المتوسطة";
						dataRow2["UnitName"] = get_Item_ByCodeALL_Result.UnitMedium;
						dataRow2["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit;
						dataRow2["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit;
						dataRow2["UnitPrice"] = get_Item_ByCodeALL_Result.SellPriceMedium;
						dataRow2["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value2 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium / (decimal?)value2);
							dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
							dataRow2["OrginalPrice"] = dataRow2["Total"];
						}
						else
						{
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
							dataRow2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
						}
					}
					if (get_Item_ByCodeALL_Result.BigBarCode1 == text || get_Item_ByCodeALL_Result.BigBarCode2 == text || get_Item_ByCodeALL_Result.BigBarCode3 == text)
					{
						dataRow2["UnitType"] = "الكبرى";
						dataRow2["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitLargeID;
						dataRow2["UnitName"] = get_Item_ByCodeALL_Result.UnitLarge;
						dataRow2["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit * get_Item_ByCodeALL_Result.ConvertBigUnit;
						dataRow2["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit * get_Item_ByCodeALL_Result.ConvertBigUnit;
						dataRow2["UnitPrice"] = get_Item_ByCodeALL_Result.SellpriceLarge;
						dataRow2["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge / (decimal?)value3);
							dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
							dataRow2["OrginalPrice"] = dataRow2["Total"];
						}
						else
						{
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
							dataRow2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
						}
					}
					List<Account_Stores> stores = Contract.Get_Stores();
					Account_Stores account_Stores = stores.FirstOrDefault((Account_Stores store) => store.ID == LoginDetails.StoreID);
					if (account_Stores != null)
					{
						dataRow2["StoreName"] = account_Stores.Store_Name;
					}
					else
					{
						dataRow2["StoreName"] = string.Empty;
					}
					dataRow2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["Total"]));
					datatable.Rows.Add(dataRow2);
				}
				CheckFfistRow();
				InsertNewRow();
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
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
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
		TxtVatDisc.DecimalDigits = DecimalNum;
		TxtTotals.DecimalDigits = DecimalNum;
		TxtTotalDiscount.DecimalDigits = DecimalNum;
		TxtTotals.DecimalDigits = DecimalNum;
		TxtTotalDiscount.DecimalDigits = DecimalNum;
		TxtNet.DecimalDigits = DecimalNum;
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

	private void SearchFrm(int PurchacheId)
	{
		try
		{
			if (PurchacheId > 0)
			{
				Order_OrderReturn order_OrderReturn = orderTRN.GetPurchases(PurchacheId).FirstOrDefault();
				datatable.Rows.Clear();
				List<Order_OrderReturnDetails> itemsPurchesByID = orderTRN.GetItemsPurchesByID(PurchacheId);
				int num = 1;
				foreach (Order_OrderReturnDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = orderTRN.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemCode"] = item_Items.Item_code;
					dataRow["ItemName"] = item_Items.item_Name;
					if (item.ItemUnitType == "الصغرى")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "المتوسطة")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					if (item.ItemUnitType == "الكبرى")
					{
						dataRow["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
					}
					dataRow["ItemID"] = item.ItemID;
					dataRow["UnitType"] = item.ItemUnitType;
					dataRow["Storename"] = (from x in db.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["Quantity"] = FUNC_DecimalNumQty(item.Quantity);
					dataRow["AvergItem"] = item.CostAverg;
					dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
					dataRow["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.UnitPrice));
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetUnitPrice));
					dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
					dataRow["DeleteItem"] = 0;
					dataRow["AmountDisc"] = FUNC_DecimalNum(Convert.ToDecimal(item.AmountDisc));
					dataRow["PercentDisc"] = FUNC_DecimalNum(Convert.ToDecimal(item.PercentDisc));
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
						dataRow["Total"] = FUNC_DecimalNum(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT));
						dataRow["SumPriceDisc"] = Math.Round(Convert.ToDecimal(item.NetTotalPrice), DecimalNum);
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice) + Convert.ToDecimal(item.VAT_Discount));
						dataRow["IsItemTax"] = 1;
						dataRow["Tax_Value"] = item_Items.Tax_Value;
					}
					else
					{
						dataRow["IsItemTax"] = 0;
						dataRow["Tax_Value"] = 0;
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
						dataRow["Total"] = FUNC_DecimalNum(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT));
						dataRow["SumPriceDisc"] = Math.Round(Convert.ToDecimal(item.NetTotalPrice), DecimalNum);
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice) + Convert.ToDecimal(item.VAT_Discount));
					}
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				InsertNewRow();
				PrintID = PurchacheId;
				TxtType.SelectedValue = order_OrderReturn.Order_Paymant_Type;
				CBCostCenter.SelectedValue = order_OrderReturn.CostCentersID;
				TxtCode.Text = order_OrderReturn.PurBranchID.ToString();
				LblSup.Text = order_OrderReturn.SupplierID.ToString();
				LBlSuppAccount.Text = Cust.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
				TxtSuppliers.Text = order_OrderReturn.SupplierName;
				TxtPhone.Text = order_OrderReturn.SupplierPhone;
				TxtNumVat.Text = order_OrderReturn.SupplierVatNum;
				TxtDate.DateTime = Convert.ToDateTime(order_OrderReturn.Purchases_Date);
				TxtNote.Text = order_OrderReturn.Note;
				TxtNumNote.Text = order_OrderReturn.NoteNum;
				TxtTotals.Text = Convert.ToString(order_OrderReturn.TotalPrices);
				TxtVat.Text = Convert.ToString(order_OrderReturn.Tax);
				TxtTotals.Text = order_OrderReturn.Safy.ToString();
				TxtDiscount.Text = order_OrderReturn.DiscountNum.ToString();
				TxtTotalDiscount.Text = order_OrderReturn.TotalPrices_Discount.ToString();
				TxtVatDisc.Text = order_OrderReturn.Tax_Discount.ToString();
				TxtTotalDiscount.Text = order_OrderReturn.Net.ToString();
				TxtNet.Text = order_OrderReturn.Net.ToString();
				TxtCost.Text = order_OrderReturn.CostAverg.ToString();
				TxtTabocc.Text = order_OrderReturn.TobaccoTax.ToString();
				TxtAllVat.Text = order_OrderReturn.AllTax.ToString();
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = false;
				FiterDatatableItemsUpdate();
				ArrangeDatatable();
				IsEnableFalse();
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			}
			else
			{
				TxtDate.Focus();
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ItemCode"));
		string text2 = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SN"));
		if (text != string.Empty)
		{
			LoginDetails.ItemcodeForUnits = text;
			FrmSearchUnit frmSearchUnit = new FrmSearchUnit();
			frmSearchUnit.ShowDialog();
			if (frmSearchUnit.ItemCode != string.Empty)
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
							if (row["SN"].ToString() == Convert.ToString(text2))
							{
								row["UnitType"] = "الصغرى";
								row["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
								row["Quantity"] = 1;
								row["SumAvergItem"] = Math.Round(Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]), DecimalNumQty);
								if (LoginDetails.ItemTotalWithVat)
								{
									decimal value = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall / (decimal?)value);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
								}
								else
								{
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
								}
							}
						}
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "المتوسطة")
				{
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result2 = ITM.GetItemByCode(itemCode).FirstOrDefault();
					if (get_Item_ByCodeALL_Result2 != null)
					{
						int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row2 in datatable.Rows)
						{
							if (row2["SN"].ToString() == Convert.ToString(text2))
							{
								row2["Quantity"] = 1;
								decimal num3 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.ConvertMediumUnit);
								row2["SumAvergItem"] = Math.Round(Convert.ToDecimal(row2["AvergItem"]) * (Convert.ToDecimal(row2["Quantity"]) * num3), DecimalNumQty);
								row2["UnitType"] = "المتوسطة";
								row2["UnitName"] = get_Item_ByCodeALL_Result2.UnitMedium;
								row2["AvergItem"] = get_Item_ByCodeALL_Result2.Average_cost * get_Item_ByCodeALL_Result2.ConvertMediumUnit;
								row2["SumAvergItem"] = get_Item_ByCodeALL_Result2.Average_cost * get_Item_ByCodeALL_Result2.ConvertMediumUnit;
								row2["UnitPrice"] = get_Item_ByCodeALL_Result2.SellPriceMedium;
								row2["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium);
								if (LoginDetails.ItemTotalWithVat)
								{
									decimal value2 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.Tax_Value) / 100m + 1m;
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium / (decimal?)value2);
									row2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
								}
								else
								{
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium);
									row2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
								}
							}
						}
					}
				}
				if (frmSearchUnit.unitName == "الكبرى")
				{
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result3 = ITM.GetItemByCode(itemCode).FirstOrDefault();
					if (get_Item_ByCodeALL_Result3 != null)
					{
						int num4 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row3 in datatable.Rows)
						{
							if (row3["SN"].ToString() == Convert.ToString(text2))
							{
								row3["Quantity"] = 1;
								row3["UnitType"] = "الكبرى";
								row3["UnitName"] = get_Item_ByCodeALL_Result3.UnitLarge;
								row3["AvergItem"] = get_Item_ByCodeALL_Result3.Average_cost * get_Item_ByCodeALL_Result3.ConvertMediumUnit * get_Item_ByCodeALL_Result3.ConvertBigUnit;
								row3["SumAvergItem"] = get_Item_ByCodeALL_Result3.Average_cost * get_Item_ByCodeALL_Result3.ConvertMediumUnit * get_Item_ByCodeALL_Result3.ConvertBigUnit;
								if (LoginDetails.ItemTotalWithVat)
								{
									decimal value3 = Convert.ToDecimal(get_Item_ByCodeALL_Result3.Tax_Value) / 100m + 1m;
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge / (decimal?)value3);
									row3["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
								}
								else
								{
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge);
									row3["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
								}
							}
						}
						frmSearchUnit.Close();
					}
				}
				frmSearchUnit.Close();
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[4];
			gridView1.ShowEditor();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
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
			if (row["SN"].ToString() == Convert.ToString(num))
			{
				if (row["ItemCode"].ToString() != "")
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
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
	}

	private void repositoryItemButtonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string Itemocde = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ItemCode"));
		if (Itemocde != string.Empty)
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
						if (row["SN"].ToString() == Convert.ToString(num))
						{
							row["StoreName"] = value;
							break;
						}
					}
					gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[5];
					gridView1.ShowEditor();
					frmSearchTransfer.Close();
				}
				return;
			}
		}
		MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف المخازن", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}

	private void repositoryItemButtonEditCodeSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchItems();
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
		}
		gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
		base.KeyPreview = true;
		gridView1.FocusedRowHandle = 1;
		gridView1.FocusedColumn = gridView1.VisibleColumns[0];
		gridView1.ShowEditor();
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

	private void TxtNote_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Return)
		{
			groupBox2.Focus();
			gridView1.Focus();
		}
	}

	private void TxtType_SelectedIndexChanged_1(object sender, EventArgs e)
	{
		if (TxtType.Text == "نقدى")
		{
			TxtSuppliers.Text = Suppliers.GetFirstSuppliers();
			TxtSuppliers.ForeColor = Color.Black;
			TxtType.ForeColor = Color.Black;
		}
		if (TxtType.Text == "آجلة")
		{
			TxtSuppliers.Text = "";
			TxtNumVat.Text = "";
			TxtNumNote.Text = "";
			TxtPhone.Text = "";
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

	private void TxtNet_TextChanged(object sender, EventArgs e)
	{
		try
		{
			decimal number = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
			if (LoginDetails.Lang == "ar-EG")
			{
				ToWord toWord = new ToWord(number, currencies[tafqitID]);
				txtWordAr = toWord.ConvertToArabic();
				TxtTafqit.Text = txtWordAr;
			}
			else
			{
				ToWord toWord2 = new ToWord(number, currencies[tafqitID]);
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

	private void LblSup_TextChanged(object sender, EventArgs e)
	{
		LBlSuppAccount.Text = Cust.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
	}

	private void MenuPrintQued_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text != "")
		{
			Class_Print_DailyRestrictionsALL class_Print_DailyRestrictionsALL = new Class_Print_DailyRestrictionsALL();
			class_Print_DailyRestrictionsALL.Print_DailyRestrictionsALL(Convert.ToInt32(TxtCode.Text), 13);
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

	private void ZeroDisc()
	{
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		Cal_FinalResultTotals(0m, "DiscAmount");
	}

	private void TxtDiscount_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (TxtDiscount.Text != string.Empty && Convert.ToDecimal(TxtDiscount.Text) != 0m && Convert.ToDecimal(TxtDiscount.Text) < Convert.ToDecimal(TxtTotals.Text))
			{
				decimal per = Convert.ToDecimal(TxtDiscount.Text);
				Cal_FinalResultTotals(per, "DiscAmount");
			}
			else
			{
				ZeroDisc();
			}
		}
		catch (Exception)
		{
			ZeroDisc();
		}
	}

	private void TxtAllDiscount_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (TxtAllDiscount.Text != string.Empty && Convert.ToDecimal(TxtAllDiscount.Text) != 0m && Convert.ToDecimal(TxtAllDiscount.Text) < Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				decimal per = Convert.ToDecimal(TxtAllDiscount.Text);
				Cal_FinalResultTotals(per, "AllDiscount");
				gridControl1.DataSource = datatable;
			}
			else
			{
				ZeroDisc();
			}
		}
		catch (Exception)
		{
			ZeroDisc();
		}
	}

	private void txtdicoutprege_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (!decimal.TryParse(txtdicoutprege.Text, out var result))
			{
				result = 0m;
			}
			if (txtdicoutprege.Text != string.Empty && Convert.ToDecimal(txtdicoutprege.Text) != 0m && Convert.ToDecimal(txtdicoutprege.Text) <= 100m)
			{
				Cal_FinalResultTotals(result, "Percent");
			}
			else
			{
				ZeroDisc();
			}
		}
		catch (Exception)
		{
			ZeroDisc();
		}
	}

	private decimal FUNC_DecimalNum(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNum);
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

	private decimal FUNC_DecimalNumQty(decimal? Value)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmOrdersReturnNoPurchCode));
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
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.label19 = new System.Windows.Forms.Label();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.TxtNumVat = new DevExpress.XtraEditors.TextEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.label26 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label24 = new System.Windows.Forms.Label();
		this.رقمالفاتورةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.label18 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.TxtCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTabocc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label29 = new System.Windows.Forms.Label();
		this.TxtAllVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label28 = new System.Windows.Forms.Label();
		this.TxtAllTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label27 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
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
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtLastCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtAvergCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.TxtProfit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label8 = new System.Windows.Forms.Label();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtSellPrice3 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtTotalDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtSellPrice2 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtSellPrice1 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.TxtAllDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label2 = new System.Windows.Forms.Label();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
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
		this.MenuNewItem = new System.Windows.Forms.ToolStripMenuItem();
		this.حركةصنفToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.استعلامToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.اسمالموردToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.نوعالفاتورةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.الكمياتToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.الكميةالحاليةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.كلالكمياتبالمستودعاتToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.كلالكمياتبالفروعToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.تخصيصToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label39 = new System.Windows.Forms.Label();
		this.CBProject = new System.Windows.Forms.ComboBox();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
		this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
		this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.LBlSuppAccount, "LBlSuppAccount");
		this.LBlSuppAccount.Name = "LBlSuppAccount";
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
		this.TxtType.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
		resources.ApplyResources(this.label19, "label19");
		this.label19.Name = "label19";
		this.repositoryItemButtonEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		editorButtonImageOptions.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"), resources.GetString("repositoryItemButtonEdit3.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit3.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEdit3.Buttons6"), resources.GetObject("repositoryItemButtonEdit3.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit3.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit3.Buttons9"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		resources.ApplyResources(this.TxtNumVat, "TxtNumVat");
		this.TxtNumVat.Name = "TxtNumVat";
		this.TxtNumVat.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumVat.Properties.Appearance.Font");
		this.TxtNumVat.Properties.Appearance.Options.UseFont = true;
		this.TxtNumVat.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumVat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumVat.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNumVat.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtNumVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		resources.ApplyResources(this.label26, "label26");
		this.label26.Name = "label26";
		resources.ApplyResources(this.CBCostCenter, "CBCostCenter");
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		this.CBCostCenter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
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
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate.Properties.Mask.EditMask = resources.GetString("TxtDate.Properties.Mask.EditMask");
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label24, "label24");
		this.label24.Name = "label24";
		resources.ApplyResources(this.رقمالفاتورةToolStripMenuItem1, "رقمالفاتورةToolStripMenuItem1");
		this.رقمالفاتورةToolStripMenuItem1.Name = "رقمالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.gridColumn18, "gridColumn18");
		this.gridColumn18.Name = "gridColumn18";
		resources.ApplyResources(this.gridColumn17, "gridColumn17");
		this.gridColumn17.Name = "gridColumn17";
		resources.ApplyResources(this.gridColumn16, "gridColumn16");
		this.gridColumn16.Name = "gridColumn16";
		resources.ApplyResources(this.gridColumn15, "gridColumn15");
		this.gridColumn15.Name = "gridColumn15";
		resources.ApplyResources(this.gridColumn14, "gridColumn14");
		this.gridColumn14.Name = "gridColumn14";
		resources.ApplyResources(this.gridColumn13, "gridColumn13");
		this.gridColumn13.Name = "gridColumn13";
		resources.ApplyResources(this.TxtNumNote, "TxtNumNote");
		this.TxtNumNote.Name = "TxtNumNote";
		this.TxtNumNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumNote.Properties.Appearance.Font");
		this.TxtNumNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNumNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumNote.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNumNote.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.gridColumn11, "gridColumn11");
		this.gridColumn11.Name = "gridColumn11";
		resources.ApplyResources(this.label18, "label18");
		this.label18.Name = "label18";
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.TxtCost);
		this.groupBox2.Controls.Add(this.TxtTabocc);
		this.groupBox2.Controls.Add(this.label29);
		this.groupBox2.Controls.Add(this.TxtAllVat);
		this.groupBox2.Controls.Add(this.label28);
		this.groupBox2.Controls.Add(this.TxtAllTotal);
		this.groupBox2.Controls.Add(this.label27);
		this.groupBox2.Controls.Add(this.label30);
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
		this.groupBox2.Controls.Add(this.TxtTotals);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtTotalDiscount);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.TxtSellPrice2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtSellPrice1);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.TxtDiscount);
		this.groupBox2.Controls.Add(this.TxtVatDisc);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.TxtCost, "TxtCost");
		this.TxtCost.DecimalDigits = 3;
		this.TxtCost.ForeColor = System.Drawing.Color.Black;
		this.TxtCost.Name = "TxtCost";
		this.TxtCost.ReadOnly = true;
		this.TxtCost.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCost.Value = new decimal(new int[4]);
		this.TxtCost.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtTabocc, "TxtTabocc");
		this.TxtTabocc.DecimalDigits = 3;
		this.TxtTabocc.ForeColor = System.Drawing.Color.Black;
		this.TxtTabocc.Name = "TxtTabocc";
		this.TxtTabocc.ReadOnly = true;
		this.TxtTabocc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTabocc.Value = new decimal(new int[4]);
		this.TxtTabocc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label29, "label29");
		this.label29.Name = "label29";
		resources.ApplyResources(this.TxtAllVat, "TxtAllVat");
		this.TxtAllVat.DecimalDigits = 3;
		this.TxtAllVat.ForeColor = System.Drawing.Color.Black;
		this.TxtAllVat.Name = "TxtAllVat";
		this.TxtAllVat.ReadOnly = true;
		this.TxtAllVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllVat.Value = new decimal(new int[4]);
		this.TxtAllVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label28, "label28");
		this.label28.Name = "label28";
		resources.ApplyResources(this.TxtAllTotal, "TxtAllTotal");
		this.TxtAllTotal.DecimalDigits = 3;
		this.TxtAllTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtAllTotal.Name = "TxtAllTotal";
		this.TxtAllTotal.ReadOnly = true;
		this.TxtAllTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllTotal.Value = new decimal(new int[4]);
		this.TxtAllTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label27, "label27");
		this.label27.Name = "label27";
		resources.ApplyResources(this.label30, "label30");
		this.label30.Name = "label30";
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
		this.txtdicoutprege.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.txtdicoutprege.KeyUp += new System.Windows.Forms.KeyEventHandler(txtdicoutprege_KeyUp);
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
		resources.ApplyResources(this.TxtNet, "TxtNet");
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.TxtTotalDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
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
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtDiscount_KeyUp);
		resources.ApplyResources(this.TxtVatDisc, "TxtVatDisc");
		this.TxtVatDisc.DecimalDigits = 3;
		this.TxtVatDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatDisc.Name = "TxtVatDisc";
		this.TxtVatDisc.ReadOnly = true;
		this.TxtVatDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatDisc.Value = new decimal(new int[4]);
		this.TxtVatDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.TxtAllDiscount, "TxtAllDiscount");
		this.TxtAllDiscount.DecimalDigits = 3;
		this.TxtAllDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtAllDiscount.Name = "TxtAllDiscount";
		this.TxtAllDiscount.ReadOnly = true;
		this.TxtAllDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllDiscount.Value = new decimal(new int[4]);
		this.TxtAllDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtAllDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtAllDiscount_KeyUp);
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		this.ribbonPage2.Name = "ribbonPage2";
		resources.ApplyResources(this.ribbonPage2, "ribbonPage2");
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.طباعةToolStripMenuItem1, this.toolStripSeparator1, this.MenuNewItem, this.حركةصنفToolStripMenuItem, this.MenuSearchItem, this.toolStripSeparator4,
			this.استعلامToolStripMenuItem1, this.الكمياتToolStripMenuItem1, this.toolStripSeparator2, this.تخصيصToolStripMenuItem1, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		resources.ApplyResources(this.MenuNew, "MenuNew");
		this.MenuNew.Name = "MenuNew";
		this.MenuNew.Click += new System.EventHandler(MenuNew_Click);
		resources.ApplyResources(this.MenuSave, "MenuSave");
		this.MenuSave.Name = "MenuSave";
		this.MenuSave.Click += new System.EventHandler(MenuSave_Click);
		resources.ApplyResources(this.MenuEdit, "MenuEdit");
		this.MenuEdit.Name = "MenuEdit";
		this.MenuEdit.Click += new System.EventHandler(MenuEdit_Click);
		resources.ApplyResources(this.MenuDelete, "MenuDelete");
		this.MenuDelete.Name = "MenuDelete";
		this.MenuDelete.Click += new System.EventHandler(MenuDelete_Click);
		resources.ApplyResources(this.طباعةToolStripMenuItem1, "طباعةToolStripMenuItem1");
		this.طباعةToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.MenuPrintQued, this.MenuPreviewFatoura, this.MenuPrintFatoura });
		this.طباعةToolStripMenuItem1.Name = "طباعةToolStripMenuItem1";
		resources.ApplyResources(this.MenuPrintQued, "MenuPrintQued");
		this.MenuPrintQued.Name = "MenuPrintQued";
		this.MenuPrintQued.Click += new System.EventHandler(MenuPrintQued_Click);
		resources.ApplyResources(this.MenuPreviewFatoura, "MenuPreviewFatoura");
		this.MenuPreviewFatoura.Name = "MenuPreviewFatoura";
		this.MenuPreviewFatoura.Click += new System.EventHandler(MenuPreviewFatoura_Click);
		resources.ApplyResources(this.MenuPrintFatoura, "MenuPrintFatoura");
		this.MenuPrintFatoura.Name = "MenuPrintFatoura";
		this.MenuPrintFatoura.Click += new System.EventHandler(MenuPrintFatoura_Click);
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.MenuNewItem, "MenuNewItem");
		this.MenuNewItem.Name = "MenuNewItem";
		this.MenuNewItem.Click += new System.EventHandler(MenuNewItem_Click);
		resources.ApplyResources(this.حركةصنفToolStripMenuItem, "حركةصنفToolStripMenuItem");
		this.حركةصنفToolStripMenuItem.Name = "حركةصنفToolStripMenuItem";
		this.حركةصنفToolStripMenuItem.Click += new System.EventHandler(حركةصنفToolStripMenuItem_Click);
		resources.ApplyResources(this.MenuSearchItem, "MenuSearchItem");
		this.MenuSearchItem.Name = "MenuSearchItem";
		this.MenuSearchItem.Click += new System.EventHandler(MenuSearchItem_Click);
		resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		resources.ApplyResources(this.استعلامToolStripMenuItem1, "استعلامToolStripMenuItem1");
		this.استعلامToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.رقمالفاتورةToolStripMenuItem1, this.اسمالموردToolStripMenuItem1, this.نوعالفاتورةToolStripMenuItem1 });
		this.استعلامToolStripMenuItem1.Name = "استعلامToolStripMenuItem1";
		resources.ApplyResources(this.اسمالموردToolStripMenuItem1, "اسمالموردToolStripMenuItem1");
		this.اسمالموردToolStripMenuItem1.Name = "اسمالموردToolStripMenuItem1";
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
		this.MenuExit.Click += new System.EventHandler(MenuExit_Click);
		resources.ApplyResources(this.repositoryItemButtonEditCode, "repositoryItemButtonEditCode");
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCode.Buttons"), resources.GetString("repositoryItemButtonEditCode.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCode.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonEditCode.Buttons6"), resources.GetObject("repositoryItemButtonEditCode.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCode.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCode.Buttons9"))
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
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
		this.TxtSuppliers.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		this.TxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.gridColumn12, "gridColumn12");
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label39);
		this.groupBox1.Controls.Add(this.CBProject);
		this.groupBox1.Controls.Add(this.LBlSuppAccount);
		this.groupBox1.Controls.Add(this.label26);
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
		resources.ApplyResources(this.label39, "label39");
		this.label39.Name = "label39";
		resources.ApplyResources(this.CBProject, "CBProject");
		this.CBProject.FormattingEnabled = true;
		this.CBProject.Name = "CBProject";
		this.CBProject.SelectedIndexChanged += new System.EventHandler(CBProject_SelectedIndexChanged);
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceCell.Font");
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.FieldName = "UnitType";
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		this.gridColumn19.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
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
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[25]
		{
			this.gridColumn1, this.gridColumn2, this.shearch, this.gridColumn19, this.gridColumn4, this.gridColumn23, this.gridColumn20, this.gridColumn21, this.gridColumn3, this.gridColumn6,
			this.gridColumn7, this.gridColumn8, this.gridColumn22, this.gridColumn5, this.gridColumn9, this.gridColumn24, this.gridColumn25, this.gridColumn26, this.gridColumn27, this.gridColumn28,
			this.gridColumn29, this.gridColumn30, this.gridColumn31, this.gridColumn32, this.gridColumn33
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
		this.gridColumn1.FieldName = "ItemCode";
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonEditCodeSearch, "repositoryItemButtonEditCodeSearch");
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions5");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons"), resources.GetString("repositoryItemButtonEditCodeSearch.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("repositoryItemButtonEditCodeSearch.Buttons6"), resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons9"))
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
		this.gridColumn2.FieldName = "ItemName";
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.shearch.FieldName = "UnitName";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		this.shearch.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
		editorButtonImageOptions6.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions6");
		editorButtonImageOptions6.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemButtonEdit4.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
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
		this.gridColumn4.FieldName = "StoreName";
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		resources.ApplyResources(editorButtonImageOptions7, "editorButtonImageOptions7");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"), resources.GetString("repositoryItemButtonEdit5.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit5.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons5"), editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, resources.GetString("repositoryItemButtonEdit5.Buttons6"), resources.GetObject("repositoryItemButtonEdit5.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit5.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit5.Buttons9"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEdit5.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
		resources.ApplyResources(this.gridColumn23, "gridColumn23");
		this.gridColumn23.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceCell.Font");
		this.gridColumn23.AppearanceCell.Options.UseFont = true;
		this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceHeader.Font");
		this.gridColumn23.AppearanceHeader.Options.UseFont = true;
		this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.FieldName = "SumAvergItem";
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn20.FieldName = "Quantity";
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
		this.gridColumn21.FieldName = "UnitPrice";
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn3.FieldName = "SumPrice";
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
		this.gridColumn6.FieldName = "ItemVat";
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
		this.gridColumn7.FieldName = "Net";
		this.gridColumn7.Name = "gridColumn7";
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
		this.gridColumn8.FieldName = "DeleteItem";
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit6, "repositoryItemButtonEdit6");
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions8");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
		resources.ApplyResources(this.gridColumn22, "gridColumn22");
		this.gridColumn22.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceCell.Font");
		this.gridColumn22.AppearanceCell.Options.UseFont = true;
		this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceHeader.Font");
		this.gridColumn22.AppearanceHeader.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.FieldName = "SumPriceDisc";
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn22.OptionsColumn.AllowEdit = false;
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
		this.gridColumn5.FieldName = "ItemVatDisc";
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.OptionsColumn.AllowEdit = false;
		this.gridColumn5.OptionsColumn.AllowFocus = false;
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
		this.gridColumn9.FieldName = "SN";
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn24.FieldName = "ItemNote";
		this.gridColumn24.MinWidth = 27;
		this.gridColumn24.Name = "gridColumn24";
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
		this.gridColumn25.FieldName = "AvergItem";
		this.gridColumn25.MinWidth = 25;
		this.gridColumn25.Name = "gridColumn25";
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
		this.gridColumn26.FieldName = "Total";
		this.gridColumn26.MinWidth = 25;
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.OptionsColumn.AllowEdit = false;
		this.gridColumn26.OptionsColumn.AllowFocus = false;
		this.gridColumn26.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn27.FieldName = "AmountDisc";
		this.gridColumn27.MinWidth = 25;
		this.gridColumn27.Name = "gridColumn27";
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
		this.gridColumn28.FieldName = "PercentDisc";
		this.gridColumn28.MinWidth = 25;
		this.gridColumn28.Name = "gridColumn28";
		this.gridColumn28.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn29, "gridColumn29");
		this.gridColumn29.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceCell.Font");
		this.gridColumn29.AppearanceCell.Options.UseFont = true;
		this.gridColumn29.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceHeader.Font");
		this.gridColumn29.AppearanceHeader.Options.UseFont = true;
		this.gridColumn29.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.FieldName = "UnitPriceDisc";
		this.gridColumn29.MinWidth = 25;
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
		this.gridColumn30.FieldName = "OrginalPrice";
		this.gridColumn30.MinWidth = 25;
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
		this.gridColumn31.FieldName = "Tax_Value";
		this.gridColumn31.MinWidth = 25;
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
		this.gridColumn32.FieldName = "IsItemTax";
		this.gridColumn32.MinWidth = 25;
		this.gridColumn32.Name = "gridColumn32";
		this.gridColumn32.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn33.FieldName = "ItemID";
		this.gridColumn33.MinWidth = 25;
		this.gridColumn33.Name = "gridColumn33";
		this.gridColumn33.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
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
		resources.ApplyResources(this.groupBox3, "groupBox3");
		this.groupBox3.Controls.Add(this.TxtAllDiscount);
		this.groupBox3.Controls.Add(this.label2);
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.TabStop = false;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox3);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmOrdersReturnNoPurchCode";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		base.ResumeLayout(false);
	}
}
