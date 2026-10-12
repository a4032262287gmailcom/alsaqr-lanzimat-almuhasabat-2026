using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Media;
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
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmCachMoney;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Frms.Reports.Stores;
using GTSErpSystem.Helper;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Orders;

public class FrmPurchases : XtraForm
{
	private Class_Purchases Pur = new Class_Purchases();

	private Class_Items ITM = new Class_Items();

	private Class_PurchaseOrder PurORD = new Class_PurchaseOrder();

	private Class_Orders ORD = new Class_Orders();

	private Class_Contract Contract = new Class_Contract();

	private Class_Suppliers SPR = new Class_Suppliers();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private Class_Bounce BNC = new Class_Bounce();

	private GTSdbEntities db = new GTSdbEntities();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private ClassProject Project = new ClassProject();

	private DataTable datatable = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	private int tafqitID = LoginDetails.TafqitID;

	public string txtWordAr;

	public string txtWordEn;

	public string CodeItm;

	private static int IsShow;

	private int intType = 1;

	public bool ISEVENTDISC = true;

	public bool ISEVENTDISCTotal = true;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	private string fileNameAR = Application.StartupPath + "\\Layout\\PurchesAR.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\PurchesEN.xml";

	private decimal ChargeAfterSale;

	private decimal ChargeMin = 0m;

	private int SN = 1;

	private int ItmID = 0;

	private int StorId = 0;

	private string ItmName = string.Empty;

	private string UnitName = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox2;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private NumericEditBox TxtNet;

	private TextEdit TxtNumVat;

	private ButtonEdit TxtSuppliers;

	private ButtonEdit TxtCode;

	private TextEdit TxtNumNote;

	private TextEdit TxtPhone;

	private System.Windows.Forms.ComboBox TxtType;

	private DateEdit TxtDate;

	private Label LblSup;

	private TextEdit TxtTafqit;

	private NumericEditBox TxtQuntity;

	private Label label9;

	private NumericEditBox TxtAllQuantity;

	private NumericEditBox TxtLastCost;

	private Label label10;

	private NumericEditBox TxtAvergCost;

	private NumericEditBox TxtProfit;

	private Label label7;

	private Label label8;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtSellPrice3;

	private NumericEditBox TxtTotals;

	private NumericEditBox TxtVat1;

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

	private Label label13;

	private Label label14;

	private Label label16;

	private Label label15;

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

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

	private ToolStripMenuItem MenuPurchOrder;

	private ToolStripMenuItem استعلامToolStripMenuItem1;

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

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private GridColumn gridColumn4;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private GridColumn gridColumn20;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private GridColumn gridColumn9;

	private RepositoryItemTextEdit repositoryItemTextEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit2;

	private RepositoryItemDateEdit repositoryItemDateEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit3;

	private RepositoryItemPictureEdit repositoryItemPictureEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

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

	private GridColumn gridColumn22;

	private GridColumn gridColumn5;

	private NumericEditBox txtdicoutprege;

	private Label label25;

	private TextEdit TxtLocation;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private Label LBlSuppAccount;

	private GridColumn gridColumn23;

	private GridColumn gridColumn21;

	private GridColumn gridColumn25;

	private RepositoryItemDateEdit repositoryItemDateEdit2;

	private GridColumn gridColumn26;

	private GridColumn gridColumn19;

	private GridColumn gridColumn24;

	private GridColumn gridColumn28;

	private RepositoryItemButtonEdit repositoryVatCalc;

	private GridColumn gridColumn29;

	private RepositoryItemButtonEdit repositoryVatCalcLastCost;

	private Label label27;

	private GridColumn gridColumn31;

	private GridColumn gridColumn30;

	private ToolStripMenuItem حركةالصنفكاملةToolStripMenuItem;

	private GroupBox groupBox5;

	private Label label48;

	private Label label36;

	private Label label31;

	private Label label33;

	private System.Windows.Forms.ComboBox Comp_Store;

	private System.Windows.Forms.ComboBox CBBounce;

	private Label label28;

	private Label label29;

	private ButtonEdit BtnOrderSearch;

	private Label label30;

	private Label label32;

	private Label label34;

	private Label label35;

	private GroupBox groupBox4;

	private ButtonEdit BtnSuppliersSearch;

	private SimpleButton BtnNewSuppliers;

	private Label label37;

	private Label label38;

	private Label label39;

	private Label label42;

	private Label label43;

	private TextEdit TxtAdditionalNum;

	private TextEdit TxtBuildingNum;

	private TextEdit TxtPostalCode;

	private TextEdit TxtAddress;

	private Label label41;

	private Label label46;

	private Label label45;

	private Label label47;

	private SimpleButton BtnORDPurchaseReturnID;

	private SimpleButton BtnPayment;

	private SimpleButton BtnCovertSales;

	private Label LblLastPrice;

	private System.Windows.Forms.ComboBox CBProject;

	private Label label17;

	private Label label18;

	private TextEdit TxtCharge;

	private SimpleButton BtnPrintBarcode;

	private GridColumn gridColumn32;

	private GridColumn gridColumn33;

	private GridColumn gridColumn34;

	private GridColumn gridColumn35;

	private GridColumn gridColumn36;

	private GridColumn gridColumn37;

	private GridColumn gridColumn38;

	private GridColumn gridColumn39;

	private GridColumn gridColumn40;

	private NumericEditBox TxtAllDiscount;

	private NumericEditBox TxtTotal;

	private Label label2;

	private GridColumn gridColumn27;

	public TextEdit TxtNote;

	public FrmPurchases()
	{
		InitializeComponent();
		TxtDigital();
		SetInitialRow();
		GroupPermission();
		BindComboBox();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
		BtnDefualte();
		if (LoginDetails.FrmOrderID > 0)
		{
			SearchOrder(Convert.ToInt32(LoginDetails.FrmOrderID));
		}
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
	}

	private void gridView1_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
	{
		e.ExceptionMode = ExceptionMode.NoAction;
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[0];
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

	private void gridView1_CellValueChanging(object sender, CellValueChangedEventArgs e)
	{
		if (!(e.Column.FieldName == "IsTax"))
		{
			return;
		}
		foreach (DataRow row in datatable.Rows)
		{
			int num = Convert.ToInt32(row["SN"]);
			if (datatable.Rows.Count == 1)
			{
				SN = num;
			}
			if (num != SN)
			{
				continue;
			}
			if (Convert.ToBoolean(row["IsTax"]))
			{
				row["IsTax"] = false;
				row["IsItemTax"] = false;
			}
			else
			{
				row["IsTax"] = true;
				row["IsItemTax"] = true;
			}
			if (row["IsTax"] != DBNull.Value && Convert.ToBoolean(row["IsTax"]))
			{
				string code = row["ItemCode"].ToString();
				Get_Item_ByItemCode_Result get_Item_ByItemCode_Result = ITM.GetItemByCode2(code).FirstOrDefault();
				row["Tax_Value"] = get_Item_ByItemCode_Result.Tax_Value;
				decimal num2 = SafeConvertToDecimal(row["Quantity"]) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
				decimal num3 = (SafeConvertToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
				if (Convert.ToInt32(row["ItemID"]) > 0)
				{
					row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]) * num2);
					row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
					if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
					{
						row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
						row["ItemVatDisc"] = row["ItemVat"];
						row["SumPriceDisc"] = row["SumPrice"];
						row["Net"] = row["Total"];
					}
					else
					{
						row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]) - Convert.ToDecimal(row["UnitPurchasePrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
						row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num2);
						if (Convert.ToBoolean(row["IsItemTax"]))
						{
							row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
						}
						else
						{
							row["ItemVatDisc"] = 0;
						}
						row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
					}
				}
			}
			else
			{
				decimal num4 = SafeConvertToDecimal(row["Quantity"]) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
				decimal num5 = (SafeConvertToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
				if (Convert.ToInt32(row["ItemID"]) > 0)
				{
					row["SumPrice"] = Convert.ToDecimal(row["UnitPurchasePrice"]) * num4;
					row["ItemVat"] = 0;
					row["Tax_Value"] = 0;
					row["Total"] = row["SumPrice"];
					if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
					{
						row["UnitPriceDisc"] = row["UnitPurchasePrice"];
						row["ItemVatDisc"] = row["ItemVat"];
						row["SumPriceDisc"] = row["SumPrice"];
						row["Net"] = row["Total"];
					}
					else
					{
						row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPurchasePrice"]) - Convert.ToDecimal(row["UnitPurchasePrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m);
						row["SumPriceDisc"] = Convert.ToDecimal(row["UnitPriceDisc"]) * num4;
						row["ItemVatDisc"] = row["ItemVat"];
						row["Net"] = row["SumPriceDisc"];
					}
				}
			}
			TxtDiscount.Text = "0.00";
			txtdicoutprege.Text = "0.00";
			TxtAllDiscount.Text = "0.00";
			decimal num6 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
			TxtTotal.Text = Convert.ToString(num6);
			decimal num7 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
			decimal num8 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
			TxtVat1.Text = num7.ToString();
			TxtTotalDiscount.Text = TxtTotal.Text;
			TxtVatDisc.Text = num8.ToString();
			TxtNet.Text = (num6 + num8).ToString();
			break;
		}
		gridView1.FocusedRowHandle = e.RowHandle;
		gridView1.FocusedColumn = gridView1.VisibleColumns[3];
		gridView1.ShowEditor();
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
		ArrangeDatatable();
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
				SN = 0;
			}
		}
		catch (Exception)
		{
		}
	}

	private void BtnDefualte()
	{
		TxtAllDiscount.ReadOnly = false;
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		TxtDate.DateTime = DateTime.Now;
		gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
		base.KeyPreview = true;
		txtdicoutprege.Controls[0].Visible = false;
		TxtCharge.Text = "0.00";
		TxtCharge.Text = "0.00";
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 13);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 13);
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
			BtnPayment.Visible = false;
			BtnORDPurchaseReturnID.Visible = false;
			BtnCovertSales.Visible = false;
		}
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
			FrmSearchPurchases frmSearchPurchases = new FrmSearchPurchases();
			frmSearchPurchases.ShowDialog();
			SearchOrder(frmSearchPurchases.CustCode);
		}
		catch (Exception)
		{
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
				GetCustomer(frmSearchSuppliers.CustCode);
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	public void InsertItemRow(CellValueChangedEventArgs e)
	{
		string text = Convert.ToString(e.Value);
		Get_Item_ByItemCode_Result get_Item_ByItemCode_Result = ITM.GetItemByCode2(text).FirstOrDefault();
		if (get_Item_ByItemCode_Result != null)
		{
			foreach (DataRow row in datatable.Rows)
			{
				int num = Convert.ToInt32(row["SN"]);
				if (datatable.Rows.Count == 1)
				{
					SN = num;
				}
				if (num == SN)
				{
					row["ItemID"] = get_Item_ByItemCode_Result.ItemId;
					row["ItemCode"] = get_Item_ByItemCode_Result.Item_code;
					row["ItemName"] = get_Item_ByItemCode_Result.item_Name;
					row["ItemNote"] = string.Empty;
					row["UnitType"] = "الصغرى";
					row["ItemUnitID"] = get_Item_ByItemCode_Result.UnitSmall;
					row["UnitName"] = get_Item_ByItemCode_Result.UnitNameSmall;
					row["Quantity"] = 1;
					row["BounceItem"] = 0;
					row["Height"] = 1;
					row["Width"] = 1;
					row["Long"] = 1;
					row["Weight"] = 1;
					row["UnitNum"] = 0;
					row["StoreName"] = Comp_Store.GetItemText(Comp_Store.SelectedItem);
					row["StoreID"] = Comp_Store.SelectedValue;
					row["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByItemCode_Result.SellPriceSmall));
					row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByItemCode_Result.LastCost));
					row["OrginalPrice"] = row["UnitPurchasePrice"];
					row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["PercentDisc"] = 0;
					row["AmountDisc"] = 0;
					row["UnitPriceDisc"] = row["UnitPurchasePrice"];
					row["SumPriceDisc"] = row["UnitPriceDisc"];
					row["IsItemTax"] = get_Item_ByItemCode_Result.Is_Tax;
					row["Tax_Value"] = get_Item_ByItemCode_Result.Tax_Value;
					if (!PurORD.IsVatActive())
					{
						row["ItemVat"] = 0;
						row["IsTax"] = false;
						row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
						row["ItemVatDisc"] = 0;
						row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
					}
					else if (Convert.ToBoolean(row["IsItemTax"]))
					{
						if (LoginDetails.ItemTotalWithVat)
						{
							row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
							row["SumPrice"] = Convert.ToDecimal(row["Total"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
							row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
							row["ItemVat"] = Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]);
							row["IsTax"] = true;
							row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
							row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
							row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
							row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
						}
						else
						{
							decimal value = Convert.ToDecimal(get_Item_ByItemCode_Result.Tax_Value) / 100m + 1m;
							row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByItemCode_Result.SellPriceSmall * (decimal?)value);
							row["IsTax"] = true;
							row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
							row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
							row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
							row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
						}
					}
					else
					{
						row["ItemVat"] = 0;
						row["IsTax"] = false;
						row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
						row["ItemVatDisc"] = 0;
						row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
					}
					row["DeleteItem"] = 0;
					row["Done"] = 1;
				}
			}
			return;
		}
		ItemMatchResult itemsOnly = ORD.GetItemsOnly(text);
		if (itemsOnly != null)
		{
			if (itemsOnly.MatchedField == "Item_code" || itemsOnly.MatchedField == "SmallBarCode1" || itemsOnly.MatchedField == "SmallBarCode2" || itemsOnly.MatchedField == "SmallBarCode3")
			{
				InsertNewItemUnitSmall(text);
			}
			else if (itemsOnly.MatchedField == "MediumBarCode1" || itemsOnly.MatchedField == "MediumBarCode2" || itemsOnly.MatchedField == "MediumBarCode3")
			{
				InsertNewItemMedium(text);
			}
			else if (itemsOnly.MatchedField == "BigBarCode1" || itemsOnly.MatchedField == "BigBarCode1" || itemsOnly.MatchedField == "BigBarCode1")
			{
				InsertNewItemBig(text);
			}
		}
		else
		{
			SoundPlayer soundPlayer = new SoundPlayer("SOUND38.WAV");
			soundPlayer.Play();
			MessageBox.Show("عفوا، لايوجد صنف بهذا الرقم \r\n الرجاء المراجعة والمحاولة مرة اخري.", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private decimal SafeDecimal(object value)
	{
		return (value == null || value == DBNull.Value) ? 1m : Convert.ToDecimal(value);
	}

	private decimal SafeDecimalBounceItem(object value)
	{
		return (value == null || value == DBNull.Value) ? 0m : Convert.ToDecimal(value);
	}

	public void UpdateItemRow(string TxtChange, CellValueChangedEventArgs e)
	{
		decimal num = Convert.ToDecimal(e.Value);
		bool flag = false;
		bool flag2 = false;
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToInt32(row["SN"]) != SN)
			{
				continue;
			}
			decimal quantity = ((TxtChange == "Quantity") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"])));
			decimal unitPurchasePrice = ((TxtChange == "UnitPurchasePrice") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"])));
			if (TxtChange == "UnitPurchasePrice")
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					row["OrginalPrice"] = num * (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
				}
				else
				{
					row["OrginalPrice"] = num;
				}
			}
			decimal num2 = ((TxtChange == "Height") ? num : FUNC_DecimalNum(SafeDecimal(row["Height"])));
			decimal num3 = ((TxtChange == "Width") ? num : FUNC_DecimalNum(SafeDecimal(row["Width"])));
			decimal num4 = ((TxtChange == "Long") ? num : FUNC_DecimalNum(SafeDecimal(row["Long"])));
			decimal weight = ((TxtChange == "Weight") ? num : FUNC_DecimalNum(SafeDecimal(row["Weight"])));
			decimal bounceItem = ((TxtChange == "BounceItem") ? num : FUNC_DecimalNum(SafeDecimalBounceItem(row["BounceItem"])));
			decimal num5 = ((TxtChange == "PercentDisc") ? FUNC_DecimalNum(num) : FUNC_DecimalNum(Convert.ToDecimal(row["PercentDisc"])));
			if (num5 > 100m)
			{
				break;
			}
			flag2 = TxtChange == "PercentDisc" || TxtChange == "AmountDisc";
			decimal amountDisc = ((TxtChange == "AmountDisc") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["AmountDisc"])));
			flag = TxtChange == "AmountDisc";
			UpdateRowFactionAccount(quantity, unitPurchasePrice, num2, num3, num4, weight, bounceItem, flag2, flag, num5, amountDisc);
		}
	}

	public void UpdateItemRowClcVat(string TxtChange, decimal ChangeValue)
	{
		bool flag = false;
		bool flag2 = false;
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToInt32(row["SN"]) == SN)
			{
				decimal quantity = ((TxtChange == "Quantity") ? ChangeValue : FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"])));
				decimal unitPurchasePrice = ((TxtChange == "UnitPurchasePrice") ? ChangeValue : FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"])));
				if (TxtChange == "UnitPurchasePrice" && LoginDetails.ItemTotalWithVat)
				{
					row["OrginalPrice"] = ChangeValue * (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
				}
				decimal num = ((TxtChange == "Height") ? ChangeValue : FUNC_DecimalNum(SafeDecimal(row["Height"])));
				decimal num2 = ((TxtChange == "Width") ? ChangeValue : FUNC_DecimalNum(SafeDecimal(row["Width"])));
				decimal num3 = ((TxtChange == "Long") ? ChangeValue : FUNC_DecimalNum(SafeDecimal(row["Long"])));
				decimal weight = ((TxtChange == "Weight") ? ChangeValue : FUNC_DecimalNum(SafeDecimal(row["Weight"])));
				decimal bounceItem = ((TxtChange == "BounceItem") ? ChangeValue : FUNC_DecimalNum(SafeDecimalBounceItem(row["BounceItem"])));
				decimal num4 = ((TxtChange == "PercentDisc") ? FUNC_DecimalNum(ChangeValue) : FUNC_DecimalNum(Convert.ToDecimal(row["PercentDisc"])));
				if (num4 > 100m)
				{
					break;
				}
				flag2 = TxtChange == "PercentDisc" || TxtChange == "AmountDisc";
				decimal amountDisc = ((TxtChange == "AmountDisc") ? ChangeValue : FUNC_DecimalNum(Convert.ToDecimal(row["AmountDisc"])));
				flag = TxtChange == "AmountDisc";
				UpdateRowFactionAccount(quantity, unitPurchasePrice, num, num2, num3, weight, bounceItem, flag2, flag, num4, amountDisc);
			}
		}
	}

	public void UpdateRowFactionAccount(decimal Quantity, decimal UnitPurchasePrice, decimal Height, decimal Width, decimal Long, decimal Weight, decimal BounceItem, bool IsDisc, bool IsDiscAmount, decimal PercentDisc, decimal AmountDisc)
	{
		decimal num = Quantity * Height * Width * Long * Weight;
		decimal num2 = (Quantity + BounceItem) * Height * Width * Long * Weight;
		decimal value = UnitPurchasePrice * num;
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToInt32(row["SN"]) != SN)
			{
				continue;
			}
			bool flag = row["IsItemTax"] != DBNull.Value && Convert.ToBoolean(row["IsItemTax"]);
			row["Quantity"] = Quantity;
			row["BounceItem"] = BounceItem;
			row["Height"] = Height;
			row["Width"] = Width;
			row["Long"] = Long;
			row["Weight"] = Weight;
			row["UnitPurchasePrice"] = FUNC_DecimalNum(UnitPurchasePrice);
			row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
			row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
			if (!LoginDetails.IsVat || !flag)
			{
				row["SumPrice"] = UnitPurchasePrice * num;
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
				row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * (UnitPurchasePrice * num) / 100m);
			}
			else
			{
				if (!flag)
				{
					continue;
				}
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num3 = Convert.ToDecimal(LoginDetails.PerVat) / 100m;
					decimal num4 = Convert.ToDecimal(row["OrginalPrice"]);
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["OrginalPrice"]) * num);
					row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) / (num3 + 1m));
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
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * num3);
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
					row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * (UnitPurchasePrice * num) / 100m);
				}
			}
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
				row["UnitPurchasePrice"] = FUNC_DecimalNum(num / Convert.ToDecimal(row["Quantity"]));
				row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPurchasePrice"]);
				row["ItemVat"] = 0;
				row["ItemVatDisc"] = 0;
				row["AmountDisc"] = 0;
				row["PercentDisc"] = 0;
			}
			else if (Convert.ToBoolean(row["IsItemTax"]))
			{
				decimal num3 = num / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
				decimal num4 = num3 / Convert.ToDecimal(row["Quantity"]);
				row["Net"] = num;
				row["ItemVat"] = num - num3;
				row["SumPrice"] = num3;
				if (LoginDetails.ItemTotalWithVat)
				{
					row["UnitPurchasePrice"] = num / Convert.ToDecimal(row["Quantity"]);
					row["OrginalPrice"] = row["UnitPurchasePrice"];
					row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["Total"] = num;
				}
				else
				{
					row["UnitPurchasePrice"] = num4;
					row["OrginalPrice"] = row["UnitPurchasePrice"];
					row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["Total"] = num3;
				}
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
				row["SumPriceDisc"] = row["SumPrice"];
				row["ItemVat"] = num - Convert.ToDecimal(row["UnitPurchasePrice"]);
				row["ItemVatDisc"] = row["ItemVat"];
				row["AmountDisc"] = 0;
				row["PercentDisc"] = 0;
			}
			else
			{
				row["Net"] = num;
				row["SumPrice"] = num;
				row["SumPriceDisc"] = num;
				row["Total"] = num;
				row["UnitPurchasePrice"] = FUNC_DecimalNum(num / Convert.ToDecimal(row["Quantity"]));
				row["OrginalPrice"] = num / Convert.ToDecimal(row["Quantity"]);
				row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPurchasePrice"]);
				row["ItemVat"] = 0;
				row["ItemVatDisc"] = 0;
				row["AmountDisc"] = 0;
				row["PercentDisc"] = 0;
			}
			break;
		}
	}

	private void GetCustomer(int id)
	{
		if (id > 0)
		{
			Account_CustSup account_CustSup = db.Account_CustSup.FirstOrDefault((Account_CustSup x) => x.ID == id && x.IsSuppliers == (bool?)true);
			if (account_CustSup != null)
			{
				BtnSuppliersSearch.Text = id.ToString();
				LblSup.Text = id.ToString();
				TxtNumVat.Text = account_CustSup.VatNum.ToString();
				LBlSuppAccount.Text = account_CustSup.AccountNo.ToString();
				TxtSuppliers.Text = account_CustSup.CustSuppName;
				TxtPhone.Text = account_CustSup.Phone;
				TxtAdditionalNum.Text = account_CustSup.AdditionalNum;
				TxtAddress.Text = account_CustSup.Address;
				TxtPostalCode.Text = account_CustSup.PostalCode;
				TxtBuildingNum.Text = account_CustSup.BuildingNum;
				Charge(account_CustSup.ID);
			}
			else
			{
				ClearSupp();
				MessageBox.Show("عفوا لا يوجد مورد بهذا الرقم");
			}
		}
		else
		{
			ClearSupp();
			MessageBox.Show("عفوا لا يوجد مورد بهذا الرقم");
		}
	}

	public void BindComboBox()
	{
		try
		{
			TxtType.DataSource = Pur.GetAccountType();
			TxtType.DisplayMember = "Name";
			TxtType.ValueMember = "ID";
			Comp_Store.DataSource = Contract.Get_Stores();
			Comp_Store.DisplayMember = "Store_Name";
			Comp_Store.ValueMember = "ID";
			CBProject.DataSource = Project.GetSearchAccountProjects();
			CBProject.DisplayMember = "ProjectName";
			CBProject.ValueMember = "ProjectID";
			CBBounce.DataSource = BNC.GetAll();
			CBBounce.DisplayMember = "Name";
			CBBounce.ValueMember = "ID";
			if (CBProject.Items.Count > 0)
			{
				CBCostCenter.DataSource = Cost.GetCostCentersByProject(Convert.ToInt32(CBProject.SelectedValue));
				CBCostCenter.DisplayMember = "CostCentersName";
				CBCostCenter.ValueMember = "CostCentersID";
				CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
			}
			TxtSuppliers.Text = SPR.GetFirstSuppliers();
			LblSup.Text = SPR.GetFirstSuppliersId().ToString();
			TxtDate.DateTime = DateTime.Now;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void CheckFfistRow()
	{
		if (datatable.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["ItemID"]);
			if (text == "0")
			{
				row.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
	}

	public void ArrangeDatatable()
	{
		int num = 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row.RowState != DataRowState.Deleted)
			{
				row["SN"] = num;
				num++;
			}
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
			GetAllitems_Alternatives_Result getAllitems_Alternatives_Result = Pur.GetAllitems_Alternatives(ItmID, StoreID, unitType).FirstOrDefault();
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
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num = getAllitems_Alternatives_Result.Tax_Value / 100m;
					if (num > 0m)
					{
						decimal num2 = Convert.ToDecimal(getAllitems_Alternatives_Result.SellPriceSmall) / (1m + num);
						TxtProfit.Text = (num2 - getAllitems_Alternatives_Result.Average_cost).ToString();
					}
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
		catch (Exception)
		{
		}
	}

	public void FilterCurrentTableSaved(DataTable ItemOrder)
	{
		int num = 1;
		dtCurrentTable.Clear();
		foreach (DataRow row in datatable.Rows)
		{
			string value = Convert.ToString(row["ItemCode"]);
			if (!string.IsNullOrEmpty(value))
			{
				bool flag = Pur.IsItemVat(Convert.ToInt32(row["ItemID"]));
				decimal num2 = ((row["UnitPrice"] != DBNull.Value) ? Convert.ToDecimal(row["UnitPrice"]) : 0m);
				if (!flag)
				{
					num2 *= 1.15m;
				}
				dtCurrentTable.Rows.Add(num, 0, (row["ItemID"] != DBNull.Value) ? Convert.ToInt32(row["ItemID"]) : 0, LoginDetails.BranchID, (row["StoreID"] != DBNull.Value) ? Convert.ToInt32(row["StoreID"]) : 0, (row["ItemUnitID"] != DBNull.Value) ? Convert.ToInt32(row["ItemUnitID"]) : 0, (row["Quantity"] != DBNull.Value) ? Convert.ToDecimal(row["Quantity"]) : 0m, (row["BounceItem"] != DBNull.Value) ? Convert.ToDecimal(row["BounceItem"]) : 0m, (row["UnitPurchasePrice"] != DBNull.Value) ? Convert.ToDecimal(row["UnitPurchasePrice"]) : 0m, (row["SumPrice"] != DBNull.Value) ? Convert.ToDecimal(row["SumPrice"]) : 0m, (row["ItemVat"] != DBNull.Value) ? Convert.ToDecimal(row["ItemVat"]) : 0m, (row["UnitPriceDisc"] != DBNull.Value) ? Convert.ToDecimal(row["UnitPriceDisc"]) : 0m, (row["Net"] != DBNull.Value) ? Convert.ToDecimal(row["Net"]) : 0m, (row["ItemVatDisc"] != DBNull.Value) ? Convert.ToDecimal(row["ItemVatDisc"]) : 0m, num2, Convert.ToString(row["UnitType"]), Convert.ToString(row["ItemNote"]), (row["UnitNum"] != DBNull.Value) ? Convert.ToDecimal(row["UnitNum"]) : 0m, (row["AmountDisc"] != DBNull.Value) ? Convert.ToDecimal(row["AmountDisc"]) : 0m, (row["PercentDisc"] != DBNull.Value) ? Convert.ToDecimal(row["PercentDisc"]) : 0m);
				num++;
			}
		}
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("ItemID", typeof(int));
		datatable.Columns.Add("ItemCode", typeof(string));
		datatable.Columns.Add("ItemName", typeof(string));
		datatable.Columns.Add("ItemNote", typeof(string));
		datatable.Columns.Add("UnitType", typeof(string));
		datatable.Columns.Add("ItemUnitID", typeof(int));
		datatable.Columns.Add("UnitName", typeof(string));
		datatable.Columns.Add("Quantity", typeof(decimal));
		datatable.Columns.Add("BounceItem", typeof(decimal));
		datatable.Columns.Add("Height", typeof(decimal));
		datatable.Columns.Add("Width", typeof(decimal));
		datatable.Columns.Add("Long", typeof(decimal));
		datatable.Columns.Add("Weight", typeof(decimal));
		datatable.Columns.Add("UnitNum", typeof(decimal));
		datatable.Columns.Add("StoreName", typeof(string));
		datatable.Columns.Add("StoreID", typeof(int));
		datatable.Columns.Add("UnitPrice", typeof(decimal));
		datatable.Columns.Add("UnitPurchasePrice", typeof(decimal));
		datatable.Columns.Add("OrginalPrice", typeof(decimal));
		datatable.Columns.Add("SumPrice", typeof(decimal));
		datatable.Columns.Add("ItemVat", typeof(decimal));
		datatable.Columns.Add("Total", typeof(decimal));
		datatable.Columns.Add("PercentDisc", typeof(decimal));
		datatable.Columns.Add("AmountDisc", typeof(decimal));
		datatable.Columns.Add("UnitPriceDisc", typeof(decimal));
		datatable.Columns.Add("SumPriceDisc", typeof(decimal));
		datatable.Columns.Add("ItemVatDisc", typeof(decimal));
		datatable.Columns.Add("Net", typeof(decimal));
		datatable.Columns.Add("IsItemTax", typeof(bool));
		datatable.Columns.Add("IsTax", typeof(bool));
		datatable.Columns.Add("Tax_Value", typeof(decimal));
		datatable.Columns.Add("DeleteItem", typeof(int));
		datatable.Columns.Add("SN", typeof(int));
		datatable.Columns.Add("Done", typeof(int));
		dtCurrentTable.Columns.Add("SN", typeof(int));
		dtCurrentTable.Columns.Add("Purchese_ID", typeof(int));
		dtCurrentTable.Columns.Add("ItemID", typeof(int));
		dtCurrentTable.Columns.Add("BranchID", typeof(int));
		dtCurrentTable.Columns.Add("StoreID", typeof(int));
		dtCurrentTable.Columns.Add("ItemUnitID", typeof(int));
		dtCurrentTable.Columns.Add("Quantity", typeof(decimal));
		dtCurrentTable.Columns.Add("BounceItem", typeof(decimal));
		dtCurrentTable.Columns.Add("UnitPurchasePrice", typeof(decimal));
		dtCurrentTable.Columns.Add("SumPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("ItemVat", typeof(decimal));
		dtCurrentTable.Columns.Add("UnitPriceDisc", typeof(decimal));
		dtCurrentTable.Columns.Add("Net", typeof(decimal));
		dtCurrentTable.Columns.Add("ItemVatDisc", typeof(decimal));
		dtCurrentTable.Columns.Add("UnitPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("UnitType", typeof(string));
		dtCurrentTable.Columns.Add("ItemNote", typeof(string));
		dtCurrentTable.Columns.Add("UnitNum", typeof(decimal));
		dtCurrentTable.Columns.Add("AmountDisc", typeof(decimal));
		dtCurrentTable.Columns.Add("PercentDisc", typeof(decimal));
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		gridView1.CustomDrawFooterCell += gridView1_CustomDrawFooterCell;
		gridView1.Columns["ItemVatDisc"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, string.Empty, "{0:0.###}", "0")
		});
		gridView1.Columns["SumPriceDisc"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, string.Empty, "{0:0.###}", "0")
		});
		gridView1.Columns["ItemVat"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, string.Empty, "{0:0.###}", "0")
		});
		gridView1.Columns["Total"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, string.Empty, "{0:0.###}", "0")
		});
		gridView1.Columns["SumPrice"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, string.Empty, "{0:0.###}", "0")
		});
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
		dataRow["ItemUnitID"] = 1;
		dataRow["UnitName"] = string.Empty;
		dataRow["Quantity"] = 1;
		dataRow["BounceItem"] = 0;
		dataRow["Height"] = 1;
		dataRow["Width"] = 1;
		dataRow["Long"] = 1;
		dataRow["Weight"] = 1;
		dataRow["UnitNum"] = 0;
		dataRow["StoreName"] = Comp_Store.GetItemText(Comp_Store.SelectedItem);
		if (Comp_Store.SelectedValue == null)
		{
			dataRow["StoreID"] = DBNull.Value;
		}
		else
		{
			dataRow["StoreID"] = Comp_Store.SelectedValue;
		}
		dataRow["UnitPrice"] = 0;
		dataRow["UnitPurchasePrice"] = 0;
		dataRow["SumPrice"] = 0;
		dataRow["ItemVat"] = 0;
		dataRow["Total"] = 0;
		dataRow["PercentDisc"] = 0;
		dataRow["AmountDisc"] = 0;
		dataRow["UnitPriceDisc"] = 0;
		dataRow["SumPriceDisc"] = 0;
		dataRow["ItemVatDisc"] = 0;
		dataRow["Net"] = 0;
		dataRow["IsItemTax"] = false;
		dataRow["IsTax"] = false;
		dataRow["Tax_Value"] = 0;
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
			if (Convert.ToDecimal(row["UnitPurchasePrice"]) <= 0m && text != string.Empty)
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ الفاتورة لوجود اصناف سعر الوحدة تساوي صفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				result = 0;
				break;
			}
		}
		return result;
	}

	private decimal SafeConvertToDecimal(object value)
	{
		return (value == DBNull.Value) ? 1m : Convert.ToDecimal(value);
	}

	private void Cal_FinalResultTotals(decimal Per, string DicountType)
	{
		try
		{
			if (datatable.Rows.Count < 1)
			{
				TxtTotal.Text = "0.00";
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtAllDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtVat1.Text = "0.000";
				TxtVat1.Text = "0.00";
			}
			else if (!PurORD.IsVatActive())
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
			decimal num = SafeConvertToDecimal(row["Quantity"]) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			decimal num2 = (SafeConvertToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = Convert.ToDecimal(row["UnitPurchasePrice"]) * num;
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = row["UnitPurchasePrice"];
					row["ItemVatDisc"] = row["ItemVat"];
					row["SumPriceDisc"] = row["SumPrice"];
					row["Net"] = row["Total"];
				}
				else
				{
					row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPurchasePrice"]) - Convert.ToDecimal(row["UnitPurchasePrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m);
					row["SumPriceDisc"] = Convert.ToDecimal(row["UnitPriceDisc"]) * num;
					row["ItemVatDisc"] = row["ItemVat"];
					row["Net"] = row["SumPriceDisc"];
				}
			}
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		TxtVat1.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotal.Text = Convert.ToString(num3);
		TxtTotalDiscount.Text = TxtTotal.Text;
		TxtNet.Text = TxtTotal.Text.ToString();
	}

	private void No_Discount_Vat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = SafeConvertToDecimal(row["Quantity"]) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			decimal num2 = (SafeConvertToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			decimal num3 = Convert.ToDecimal(row["ItemVat"]);
			if (num3 == 787898782592002m)
			{
				if (Convert.ToInt32(row["ItemID"]) <= 0)
				{
					continue;
				}
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]) * num);
				row["ItemVat"] = 0;
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["ItemVatDisc"] = row["ItemVat"];
					row["SumPriceDisc"] = row["SumPrice"];
					row["Net"] = row["Total"];
					continue;
				}
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]) - Convert.ToDecimal(row["UnitPurchasePrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
				if (Convert.ToBoolean(row["IsItemTax"]))
				{
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
				}
				else
				{
					row["ItemVatDisc"] = 0;
				}
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
				continue;
			}
			decimal num4 = Convert.ToDecimal(row["OrginalPrice"]);
			row["SumPrice"] = Convert.ToDecimal(row["OrginalPrice"]) * num;
			decimal num5 = Convert.ToDecimal(row["SumPrice"]);
			if (Convert.ToBoolean(row["IsItemTax"]))
			{
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
			}
			else
			{
				row["ItemVat"] = 0;
			}
			decimal num6 = Convert.ToDecimal(row["ItemVat"]);
			row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
			row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
			decimal num7 = Convert.ToDecimal(row["Total"]);
			if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
				row["ItemVatDisc"] = row["ItemVat"];
				row["SumPriceDisc"] = row["SumPrice"];
				row["Net"] = row["Total"];
				continue;
			}
			row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]) - Convert.ToDecimal(row["UnitPurchasePrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
			row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
			if (Convert.ToBoolean(row["IsItemTax"]))
			{
				row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
			}
			else
			{
				row["ItemVatDisc"] = 0;
			}
			row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num8 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotal.Text = Convert.ToString(num8);
		decimal num9 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		decimal num10 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat1.Text = num9.ToString();
		TxtTotalDiscount.Text = TxtTotal.Text;
		TxtVatDisc.Text = num10.ToString();
		TxtNet.Text = (num8 + num10).ToString();
	}

	private void No_Discount_PriceWithVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = SafeConvertToDecimal(row["Quantity"]) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			decimal num2 = (SafeConvertToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			decimal num3 = Convert.ToDecimal(row["ItemVat"]);
			if (!Convert.ToBoolean(row["IsItemTax"]) || num3 == 787898782592002m)
			{
				if (Convert.ToInt32(row["ItemID"]) > 0)
				{
					row["SumPrice"] = Convert.ToDecimal(row["UnitPurchasePrice"]) * num;
					row["ItemVat"] = 0;
					row["Total"] = row["SumPrice"];
					if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
					{
						row["UnitPriceDisc"] = row["UnitPurchasePrice"];
						row["ItemVatDisc"] = row["ItemVat"];
						row["SumPriceDisc"] = row["SumPrice"];
						row["Net"] = row["Total"];
					}
					else
					{
						row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPurchasePrice"]) - Convert.ToDecimal(row["UnitPurchasePrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m);
						row["SumPriceDisc"] = Convert.ToDecimal(row["UnitPriceDisc"]) * num;
						row["ItemVatDisc"] = row["ItemVat"];
						row["Net"] = row["SumPriceDisc"];
					}
				}
			}
			else
			{
				decimal num4 = ((row["Total"] != DBNull.Value) ? Convert.ToDecimal(row["Total"]) : 0m);
				decimal num5 = ((row["Tax_Value"] != DBNull.Value) ? Convert.ToDecimal(row["Tax_Value"]) : 0m);
				decimal value = num4 / (num5 / 100m + 1m);
				row["SumPrice"] = FUNC_DecimalNum(value);
				if (Convert.ToBoolean(row["IsItemTax"]))
				{
					row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]));
				}
				else
				{
					row["ItemVat"] = 0;
				}
				row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) / num);
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				}
				else
				{
					decimal num6 = Convert.ToDecimal(row["UnitPriceDisc"]);
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]) - Convert.ToDecimal(row["UnitPurchasePrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					decimal num7 = Convert.ToDecimal(row["UnitPriceDisc"]);
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					decimal num8 = Convert.ToDecimal(row["SumPriceDisc"]);
					if (Convert.ToBoolean(row["IsItemTax"]))
					{
						row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					}
					else
					{
						row["ItemVatDisc"] = 0;
					}
					decimal num9 = Convert.ToDecimal(row["ItemVatDisc"]);
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
				}
			}
			decimal num10 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
			decimal num11 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
			TxtDiscount.Text = "0.00";
			txtdicoutprege.Text = "0.00";
			TxtAllDiscount.Text = "0.00";
			decimal num12 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
			TxtTotal.Text = Convert.ToString(num12);
			TxtVat1.Text = num11.ToString();
			TxtTotalDiscount.Text = TxtTotal.Text;
			TxtVatDisc.Text = num10.ToString();
			TxtNet.Text = (num12 + num10).ToString();
		}
	}

	private void DiscountPercent_NoVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = SafeConvertToDecimal(row["Quantity"]) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			decimal num2 = (SafeConvertToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]) * num);
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				}
				else
				{
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]) - Convert.ToDecimal(row["UnitPurchasePrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
				}
			}
		}
		TxtVatDisc.Text = "0.00";
		TxtVat1.Text = "0.00";
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotal.Text = Convert.ToString(num3);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotal.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = (Convert.ToDecimal(TxtTotal.Text) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotal.Text)).ToString();
			TxtAllDiscount.Text = (Convert.ToDecimal(TxtTotal.Text) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / Convert.ToDecimal(TxtTotal.Text) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotal.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotal.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num4 = SafeConvertToDecimal(row2["Quantity"]) * SafeConvertToDecimal(row2["Height"]) * SafeConvertToDecimal(row2["Width"]) * SafeConvertToDecimal(row2["Long"]) * SafeConvertToDecimal(row2["Weight"]);
			decimal num5 = (SafeConvertToDecimal(row2["Quantity"]) + SafeConvertToDecimal(row2["BounceItem"])) * SafeConvertToDecimal(row2["Height"]) * SafeConvertToDecimal(row2["Width"]) * SafeConvertToDecimal(row2["Long"]) * SafeConvertToDecimal(row2["Weight"]);
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPurchasePrice"]) * num4);
				row2["ItemVat"] = 0;
				row2["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPrice"]));
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num4);
				row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["ItemVat"]));
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]));
			}
		}
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	private void DiscountPercent_Vat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = SafeConvertToDecimal(row["Quantity"]) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			decimal num2 = (SafeConvertToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]) * num);
			if (Convert.ToBoolean(row["IsItemTax"]))
			{
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
			}
			else
			{
				row["ItemVat"] = 0;
			}
			row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
			if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
				row["ItemVatDisc"] = row["ItemVat"];
				row["SumPriceDisc"] = row["SumPrice"];
				row["Net"] = row["Total"];
				continue;
			}
			row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]) - Convert.ToDecimal(row["UnitPurchasePrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
			row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
			if (Convert.ToBoolean(row["IsItemTax"]))
			{
				row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
			}
			else
			{
				row["ItemVatDisc"] = 0;
			}
			row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
		}
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat1.Text = Convert.ToString(num3);
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotal.Text = Convert.ToString(num4);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotal.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotal.Text) + num3) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotal.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotal.Text) + num3) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / Convert.ToDecimal(TxtTotal.Text) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotal.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotal.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num5 = SafeConvertToDecimal(row2["Quantity"]) * SafeConvertToDecimal(row2["Height"]) * SafeConvertToDecimal(row2["Width"]) * SafeConvertToDecimal(row2["Long"]) * SafeConvertToDecimal(row2["Weight"]);
			decimal num6 = (SafeConvertToDecimal(row2["Quantity"]) + SafeConvertToDecimal(row2["BounceItem"])) * SafeConvertToDecimal(row2["Height"]) * SafeConvertToDecimal(row2["Width"]) * SafeConvertToDecimal(row2["Long"]) * SafeConvertToDecimal(row2["Weight"]);
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				if (SafeConvertToDecimal(row2["Quantity"]) > 100000m)
				{
					row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) - Convert.ToDecimal(TxtDiscount.Text));
				}
				else
				{
					row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num5);
				}
				decimal num7 = Convert.ToDecimal(row2["UnitPriceDisc"]);
				decimal num8 = Convert.ToDecimal(row2["SumPriceDisc"]);
				if (Convert.ToBoolean(row2["IsItemTax"]))
				{
					row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m));
				}
				else
				{
					row2["ItemVatDisc"] = 0;
				}
				decimal num9 = Convert.ToDecimal(row2["ItemVatDisc"]);
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]));
			}
		}
		decimal num10 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVatDisc.Text = Convert.ToString(num10);
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	private void DiscountPercent_PriceWithVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = SafeConvertToDecimal(row["Quantity"]) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			decimal num2 = (SafeConvertToDecimal(row["Quantity"]) + SafeConvertToDecimal(row["BounceItem"])) * SafeConvertToDecimal(row["Height"]) * SafeConvertToDecimal(row["Width"]) * SafeConvertToDecimal(row["Long"]) * SafeConvertToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m));
			if (Convert.ToBoolean(row["IsItemTax"]))
			{
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]));
			}
			else
			{
				row["ItemVat"] = 0;
			}
			row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) / num);
			if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
				row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				continue;
			}
			row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]) - Convert.ToDecimal(row["UnitPurchasePrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
			row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num);
			if (Convert.ToBoolean(row["IsItemTax"]))
			{
				row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
			}
			else
			{
				row["ItemVatDisc"] = 0;
			}
			row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
		}
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat.Text = Convert.ToString(num3);
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotal.Text = Convert.ToString(num4);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotal.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotal.Text) + num3) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotal.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotal.Text) + num3) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(TxtTotal.Text) + num3) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotal.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotal.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num5 = SafeConvertToDecimal(row2["Quantity"]) * SafeConvertToDecimal(row2["Height"]) * SafeConvertToDecimal(row2["Width"]) * SafeConvertToDecimal(row2["Long"]) * SafeConvertToDecimal(row2["Weight"]);
			decimal num6 = (SafeConvertToDecimal(row2["Quantity"]) + SafeConvertToDecimal(row2["BounceItem"])) * SafeConvertToDecimal(row2["Height"]) * SafeConvertToDecimal(row2["Width"]) * SafeConvertToDecimal(row2["Long"]) * SafeConvertToDecimal(row2["Weight"]);
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num5);
				if (Convert.ToBoolean(row2["IsItemTax"]))
				{
					row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m));
				}
				else
				{
					row2["ItemVatDisc"] = 0;
				}
				row2["Net"] = Math.Round(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]), 1);
			}
		}
		decimal num7 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		decimal num8 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
		TxtVatDisc.Text = Convert.ToString(num7);
		TxtNet.Text = num8.ToString();
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
		InsertNewRow();
		gridControl1.DataSource = datatable;
		gridControl1.Enabled = true;
		gridView1.OptionsBehavior.Editable = true;
		BtnOrderSearch.Text = string.Empty;
		BtnSuppliersSearch.Text = string.Empty;
		TxtSuppliers.Text = SPR.GetFirstSuppliers();
		LblSup.Text = SPR.GetFirstSuppliersId().ToString();
		TxtNumVat.Text = string.Empty;
		TxtType.Text = string.Empty;
		TxtNote.Text = string.Empty;
		TxtAddress.Text = string.Empty;
		TxtCharge.Text = "0.00";
		TxtDiscount.Text = string.Empty;
		TxtAdditionalNum.Text = string.Empty;
		TxtBuildingNum.Text = string.Empty;
		TxtPostalCode.Text = string.Empty;
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtType.Text = "نقدى";
		TxtPhone.Text = string.Empty;
		TxtNumNote.Text = string.Empty;
		TxtVat1.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		TxtTafqit.Text = string.Empty;
		TxtTotal.Text = "0.00";
		TxtTotalDiscount.Text = "0.00";
		TxtNet.Text = "0.00";
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
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
		TxtDiscount.Enabled = true;
		TxtNote.Enabled = true;
		TxtType.Enabled = true;
		TxtDate.Enabled = true;
		BindComboBox();
	}

	private void NewFrm()
	{
		Clear();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		IsEnableTrue();
		TxtSuppliers.Focus();
	}

	private void Save()
	{
		if (TxtCode.Text != "")
		{
			SoundPlayer soundPlayer = new SoundPlayer("SOUND38.WAV");
			soundPlayer.Play();
			MessageBox.Show("عفوا،لايمكن حفظ الفاتوره مرتين \r\n الرجاء المراجعة والمحاولة مرة اخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		else if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 13))
		{
			int num = 0;
			int acc_Cash = 0;
			int acc_Bank = 0;
			decimal num2 = 0m;
			decimal num3 = 0m;
			if (datatable.Rows.Count > 1)
			{
				if (TxtSuppliers.Text != string.Empty)
				{
					if (TxtType.Text != string.Empty)
					{
						if (Convert.ToInt32(TxtType.SelectedValue) == 2 && string.IsNullOrWhiteSpace(LblSup.Text))
						{
							MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع حساب مورد نقدى", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							TxtSuppliers.Focus();
							return;
						}
						CheckFfistRow();
						if (IsItemPriceZero() == 0)
						{
							return;
						}
						if (Convert.ToInt32(TxtType.SelectedValue) == 1)
						{
							LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
							FrmMoney frmMoney = new FrmMoney();
							frmMoney.ShowDialog();
							if (!frmMoney.IsComplateMony)
							{
								InsertNewRow();
								return;
							}
							num2 = Convert.ToDecimal(frmMoney.TxtCash.Text);
							num3 = Convert.ToDecimal(frmMoney.TxtVisa.Text);
							acc_Cash = Convert.ToInt32(frmMoney.LblCash.Text);
							acc_Bank = Convert.ToInt32(frmMoney.LblVisa.Text);
							if (Convert.ToDecimal(TxtCharge.Text) > 0m)
							{
								ChargeMin = Convert.ToDecimal(TxtCharge.Text) * -1m;
							}
							ChargeAfterSale = Convert.ToDecimal(TxtCharge.Text) + num2 + num3 - Convert.ToDecimal(TxtNet.Text);
						}
						else
						{
							LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
							FrmMoneyAjil frmMoneyAjil = new FrmMoneyAjil();
							frmMoneyAjil.ShowDialog();
							if (!frmMoneyAjil.IsComplateMony)
							{
								return;
							}
							num2 = Convert.ToDecimal(frmMoneyAjil.TxtCash.Text);
							num3 = Convert.ToDecimal(frmMoneyAjil.TxtVisa.Text);
							if (Convert.ToDecimal(TxtCharge.Text) > 0m)
							{
								ChargeMin = Convert.ToDecimal(TxtCharge.Text) * -1m;
							}
							ChargeAfterSale = Convert.ToDecimal(TxtCharge.Text) + num2 + num3 - Convert.ToDecimal(TxtNet.Text);
						}
						if (CBCostCenter.SelectedValue == null)
						{
							CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
						}
						FilterCurrentTableSaved(datatable);
						int projectId = ((!int.TryParse(CBProject.SelectedValue?.ToString(), out var result)) ? 1 : result);
						num = Pur.InsertOrders(out var Code, Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, DateTime.Parse(TxtDate.Text), Convert.ToInt32(TxtType.SelectedValue), Convert.ToInt32(CBCostCenter.SelectedValue), TxtNote.Text, TxtNumNote.Text, Convert.ToDecimal(TxtVat1.Text), Convert.ToDecimal(TxtTotal.Text), Convert.ToDecimal(TxtTotal.Text) + Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtDiscount.Text), Convert.ToDecimal(txtdicoutprege.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtNet.Text), num2, num3, acc_Cash, acc_Bank, dtCurrentTable, ChargeAfterSale, projectId);
						if (num > 0)
						{
							BtnSave.Enabled = false;
							BtnEdit.Enabled = true;
							BtnDelete.Enabled = true;
							IsEnableFalse();
							TxtCode.Text = Code.ToString();
							gridView1.OptionsBehavior.Editable = false;
							DialogResult dialogResult = MessageBox.Show("تم حفظ بيانات فاتورة المشتريات بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							if (dialogResult == DialogResult.Yes)
							{
								Print();
							}
							Charge(Convert.ToInt32(LblSup.Text));
						}
						else
						{
							ChargeAfterSale = 0m;
							MessageBox.Show("تم حفظ بيانات فاتورة المشتريات رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
					MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة المشتريات بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 13))
		{
			if (TxtDate.ReadOnly)
			{
				IsEnableTrue();
				BtnEdit.Text = "F5 حفظ";
				InsertNewRow();
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
			int acc_Cash = 0;
			int acc_Bank = 0;
			decimal num2 = 0m;
			decimal num3 = 0m;
			if (!(TxtSuppliers.Text != string.Empty))
			{
				return;
			}
			if (Convert.ToInt32(TxtType.SelectedValue) == 2 && TxtSuppliers.Text == "مورد نقدى")
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع مورد نقدى", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtSuppliers.Focus();
			}
			else if (datatable.Rows.Count > 1)
			{
				CheckFfistRow();
				if (IsItemPriceZero() == 0)
				{
					return;
				}
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
				FilterCurrentTableSaved(datatable);
				int PurId = Convert.ToInt32(TxtCode.Text);
				Order_PurchasesReturn order_PurchasesReturn = db.Order_PurchasesReturn.Where((Order_PurchasesReturn x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
				if (order_PurchasesReturn == null)
				{
					if (CBCostCenter.SelectedValue == null)
					{
						CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
					}
					if (Convert.ToInt32(TxtType.SelectedValue) == 1)
					{
						LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
						FrmMoney frmMoney = new FrmMoney();
						frmMoney.ShowDialog();
						if (!frmMoney.IsComplateMony)
						{
							return;
						}
						num2 = Convert.ToDecimal(frmMoney.TxtCash.Text);
						num3 = Convert.ToDecimal(frmMoney.TxtVisa.Text);
						acc_Cash = Convert.ToInt32(frmMoney.LblCash.Text);
						acc_Bank = Convert.ToInt32(frmMoney.LblVisa.Text);
					}
					else
					{
						LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
						FrmMoneyAjil frmMoneyAjil = new FrmMoneyAjil();
						frmMoneyAjil.ShowDialog();
						if (!frmMoneyAjil.IsComplateMony)
						{
							return;
						}
						num2 = Convert.ToDecimal(frmMoneyAjil.TxtCash.Text);
						num3 = Convert.ToDecimal(frmMoneyAjil.TxtVisa.Text);
						if (Convert.ToDecimal(TxtCharge.Text) > 0m)
						{
							ChargeMin = Convert.ToDecimal(TxtCharge.Text) * -1m;
						}
						ChargeAfterSale = Convert.ToDecimal(TxtCharge.Text) + num2 + num3 - Convert.ToDecimal(TxtNet.Text);
					}
					int projectId = ((!int.TryParse(CBProject.SelectedValue?.ToString(), out var result)) ? 1 : result);
					num = Pur.UpdateOrders(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, DateTime.Parse(TxtDate.Text), Convert.ToInt32(TxtType.SelectedValue), Convert.ToInt32(CBCostCenter.SelectedValue), TxtNote.Text, TxtNumNote.Text, Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtTotal.Text), Convert.ToDecimal(TxtTotal.Text) + Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtDiscount.Text), Convert.ToDecimal(txtdicoutprege.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtNet.Text), num2, num3, acc_Cash, acc_Bank, dtCurrentTable, ChargeAfterSale, projectId);
					if (num > 0)
					{
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						IsEnableFalse();
						BtnEdit.Text = "Edit تعديل";
						gridView1.OptionsBehavior.Editable = false;
						DialogResult dialogResult = MessageBox.Show("تم تعديل فاتورة المشتريات بنجاح \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						if (dialogResult == DialogResult.Yes)
						{
							Print();
						}
						Charge(Convert.ToInt32(LblSup.Text));
					}
					else
					{
						ChargeAfterSale = 0m;
						MessageBox.Show("تم حفظ بيانات فاتورة المشتريات رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else
				{
					MessageBox.Show("عفوا\u064b لا يمكن التعديل على فاتورة مشتريات ليها مرتجع", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 13))
		{
			int num = 0;
			if (TxtCode.Text != string.Empty)
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة المشتريات رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
				{
					int PurId = Convert.ToInt32(TxtCode.Text);
					Order_PurchasesReturn order_PurchasesReturn = db.Order_PurchasesReturn.Where((Order_PurchasesReturn x) => x.FromPurchesID == (int?)PurId && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
					if (order_PurchasesReturn == null)
					{
						num = Pur.DeleteOrders(Convert.ToInt32(TxtCode.Text));
						if (num > 0)
						{
							MessageBox.Show("تم حذف بيانات فاتورة المشتريات رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							Clear();
							IsEnableTrue();
							BtnDelete.Enabled = false;
							BtnEdit.Enabled = false;
							BtnSave.Enabled = true;
							TxtSuppliers.Focus();
						}
						else
						{
							MessageBox.Show("لا يمكن حذف بيانات فاتورة المشتريات رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						}
					}
					else
					{
						MessageBox.Show("عفوا\u064b لا يمكن الحذف على فاتورة مشتريات ليها مرتجع", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة المشتريات بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 13))
		{
			if (TxtCode.Text != string.Empty)
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_Purchases", sqlConnection);
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
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintPurchase.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintPurchase.mrt");
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
					string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), Math.Round(Convert.ToDecimal(TxtNet.Text), 2).ToString(), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), 2).ToString());
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

	private void PrintBarcode(string PrntName)
	{
		try
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
			DataSetALL dataSetALL = new DataSetALL();
			foreach (DataRow row in datatable.Rows)
			{
				int num = Convert.ToInt32(row["ItemID"]);
				int num2 = Convert.ToInt32(row["Quantity"]);
				dataSetALL.Tables["BarcodeItem"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Select_BarCodePurchase", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@Purchese_ID", Convert.ToInt32(TxtCode.Text));
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlCommand.Parameters.AddWithValue("@ItemID", num);
				sqlDataAdapter.Fill(dataSetALL.BarcodeItem);
				if (dataSetALL.Tables["BarcodeItem"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					stiReport.Load(Application.StartupPath + "\\Reports\\RptBarcode.mrt");
					stiReport.RegData(dataSetALL.BarcodeItem);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["ItemName"] = row["ItemName"]?.ToString();
					stiReport["Barcode"] = row["ItemCode"]?.ToString();
					object obj = row["UnitPrice"];
					decimal result = 0m;
					if (obj != DBNull.Value && obj != null)
					{
						decimal.TryParse(obj.ToString(), out result);
					}
					stiReport["Price"] = result.ToString();
					stiReport.Render();
					PrinterSettings printerSettings = new PrinterSettings
					{
						PrinterName = PrntName,
						Copies = (short)num2
					};
					stiReport.Print(showPrintDialog: false, printerSettings);
				}
			}
			sqlConnection.Close();
		}
		catch (Exception)
		{
		}
	}

	public void IsEnableTrue()
	{
		TxtDiscount.ReadOnly = false;
		TxtNote.ReadOnly = false;
		TxtType.Enabled = true;
		TxtDate.ReadOnly = false;
		TxtBuildingNum.ReadOnly = false;
		TxtAddress.ReadOnly = false;
		TxtSuppliers.ReadOnly = false;
		TxtNumVat.ReadOnly = false;
		TxtPhone.ReadOnly = false;
		TxtNumNote.ReadOnly = false;
		TxtAdditionalNum.ReadOnly = false;
		TxtPostalCode.ReadOnly = false;
		txtdicoutprege.ReadOnly = false;
		CBCostCenter.Enabled = true;
		CBProject.Enabled = true;
		CBBounce.Enabled = true;
		Comp_Store.Enabled = true;
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = true;
		txtdicoutprege.ReadOnly = true;
		TxtTotalDiscount.ReadOnly = true;
		TxtNote.ReadOnly = true;
		TxtType.Enabled = false;
		TxtDate.ReadOnly = true;
		TxtSuppliers.ReadOnly = true;
		TxtNumVat.ReadOnly = true;
		TxtPhone.ReadOnly = true;
		TxtNumNote.ReadOnly = true;
		TxtAdditionalNum.ReadOnly = true;
		TxtPostalCode.ReadOnly = true;
		TxtBuildingNum.ReadOnly = true;
		TxtAddress.ReadOnly = true;
		CBCostCenter.Enabled = false;
		CBBounce.Enabled = false;
		CBProject.Enabled = false;
		TxtDiscount.BackColor = Color.White;
		CBProject.BackColor = Color.White;
		txtdicoutprege.BackColor = Color.White;
		TxtTotalDiscount.BackColor = Color.White;
		TxtNote.BackColor = Color.White;
		TxtType.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
		TxtSuppliers.BackColor = Color.White;
		TxtNumVat.BackColor = Color.White;
		TxtPhone.BackColor = Color.White;
		TxtNumNote.BackColor = Color.White;
		TxtAdditionalNum.BackColor = Color.White;
		TxtPostalCode.BackColor = Color.White;
		TxtBuildingNum.BackColor = Color.White;
		TxtAddress.BackColor = Color.White;
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
					if (row["SN"].ToString() == Convert.ToString(num) && row["ItemCode"].ToString() != string.Empty)
					{
						row.Delete();
						datatable.AcceptChanges();
						gridView1.FocusedColumn = gridView1.VisibleColumns[0];
						gridView1.ShowEditor();
						break;
					}
				}
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
				gridControl1.DataSource = datatable;
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
					dataRow2["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
					dataRow2["ItemCode"] = get_Item_ByCodeALL_Result.Item_code;
					dataRow2["ItemName"] = get_Item_ByCodeALL_Result.item_Name;
					dataRow2["UnitType"] = "الصغرى";
					dataRow2["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
					dataRow2["Quantity"] = 1;
					dataRow2["BounceItem"] = 0;
					dataRow2["Height"] = 1;
					dataRow2["Width"] = 1;
					dataRow2["Long"] = 1;
					dataRow2["Weight"] = 1;
					dataRow2["AmountDisc"] = 0;
					dataRow2["PercentDisc"] = 0;
					dataRow2["Done"] = 1;
					dataRow2["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
					dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
					dataRow2["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost);
					dataRow2["SumPrice"] = dataRow2["UnitPurchasePrice"];
					if (!PurORD.IsVatActive())
					{
						dataRow2["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost);
						dataRow2["Total"] = dataRow2["UnitPurchasePrice"];
						dataRow2["OrginalPrice"] = dataRow2["Total"];
						dataRow2["IsTax"] = false;
						dataRow2["ItemVat"] = 0;
					}
					else if (LoginDetails.ItemTotalWithVat)
					{
						decimal num2 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
						dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
						dataRow2["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost / (decimal?)num2);
						dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost), DecimalNum);
						dataRow2["OrginalPrice"] = dataRow2["Total"];
						dataRow2["Tax_Value"] = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value);
						dataRow2["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["Total"]) / num2);
						dataRow2["UnitPurchasePrice"] = dataRow2["SumPrice"];
						dataRow2["IsTax"] = true;
						dataRow2["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["Total"]) - Convert.ToDecimal(dataRow2["SumPrice"]));
						dataRow2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["ItemVat"]));
						dataRow2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["Total"]));
						dataRow2["UnitPriceDisc"] = dataRow2["UnitPurchasePrice"];
						dataRow2["SumPriceDisc"] = dataRow2["SumPrice"];
						dataRow2["IsItemTax"] = get_Item_ByCodeALL_Result.Is_Tax;
						dataRow2["Tax_Value"] = get_Item_ByCodeALL_Result.Tax_Value;
					}
					else
					{
						decimal value = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
						dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall * (decimal?)value);
						dataRow2["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost);
						dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost), DecimalNum);
						dataRow2["OrginalPrice"] = dataRow2["Total"];
						dataRow2["SumPrice"] = dataRow2["UnitPurchasePrice"];
						dataRow2["IsTax"] = true;
						DataRow dataRow3 = dataRow2;
						decimal value2 = Convert.ToDecimal(dataRow2["SumPrice"]);
						decimal? num3 = get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100;
						dataRow3["ItemVat"] = FUNC_DecimalNum((decimal?)value2 * num3);
						dataRow2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["SumPrice"]) + Convert.ToDecimal(dataRow2["ItemVat"]));
						dataRow2["IsItemTax"] = get_Item_ByCodeALL_Result.Is_Tax;
						dataRow2["Tax_Value"] = get_Item_ByCodeALL_Result.Tax_Value;
					}
					if (get_Item_ByCodeALL_Result.MediumBarCode1 == text || get_Item_ByCodeALL_Result.MediumBarCode2 == text || get_Item_ByCodeALL_Result.MediumBarCode3 == text)
					{
						dataRow2["UnitType"] = "المتوسطة";
						dataRow2["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitMediumID;
						dataRow2["UnitName"] = get_Item_ByCodeALL_Result.UnitMedium;
						dataRow2["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit;
						dataRow2["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit;
						dataRow2["UnitPrice"] = get_Item_ByCodeALL_Result.SellPriceMedium;
						dataRow2["UnitPurchasePrice"] = get_Item_ByCodeALL_Result.LastCost2;
						dataRow2["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost2);
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium / (decimal?)value3);
							dataRow2["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost2 / (decimal?)value3);
							dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost2), DecimalNum);
							dataRow2["OrginalPrice"] = dataRow2["Total"];
						}
						else
						{
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
							dataRow2["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost2);
							dataRow2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost2), DecimalNum);
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
						dataRow2["UnitPurchasePrice"] = get_Item_ByCodeALL_Result.LastCost3;
						dataRow2["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost3);
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value4 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge / (decimal?)value4);
							dataRow2["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost3 / (decimal?)value4);
							dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost3), DecimalNum);
							dataRow2["OrginalPrice"] = dataRow2["Total"];
						}
						else
						{
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
							dataRow2["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost3);
							dataRow2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost3), DecimalNum);
						}
					}
					dataRow2["StoreName"] = Comp_Store.Text;
					dataRow2["StoreID"] = Comp_Store.SelectedValue;
					dataRow2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["Total"]));
					datatable.Rows.Add(dataRow2);
				}
			}
			CheckFfistRow();
			InsertNewRow();
			ArrangeDatatable();
			frmSearchItems.Close();
			frmSearchItems.Hide();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
		}
		catch (Exception)
		{
		}
	}

	public void GetPurchesOrder()
	{
		try
		{
			FrmSearchPurchasesOrder frmSearchPurchasesOrder = new FrmSearchPurchasesOrder();
			frmSearchPurchasesOrder.ShowDialog();
			if (frmSearchPurchasesOrder.CustCode > 0)
			{
				int custCode = frmSearchPurchasesOrder.CustCode;
				Order_PurchasesOrder order_PurchasesOrder = PurORD.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<Order_PurchasesOrderDetails> itemsPurchesByID = PurORD.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (Order_PurchasesOrderDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = PurORD.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = item_Items.ItemId;
					dataRow["ItemUnitID"] = item_Items.UnitSmall;
					dataRow["ItemCode"] = item_Items.Item_code;
					dataRow["ItemName"] = item_Items.item_Name;
					dataRow["BounceItem"] = 0.0;
					dataRow["AmountDisc"] = 0.0;
					dataRow["PercentDisc"] = 0.0;
					if (item.ItemUnitType == "الصغرى")
					{
						dataRow["UnitName"] = db.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
					}
					if (item.ItemUnitType == "المتوسطة")
					{
						dataRow["UnitName"] = db.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
					}
					if (item.ItemUnitType == "الكبرى")
					{
						dataRow["UnitName"] = db.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.ItemUnitID).Name;
					}
					dataRow["UnitType"] = item.ItemUnitType;
					dataRow["StoreID"] = item.StoreID;
					dataRow["StoreName"] = db.Account_Stores.FirstOrDefault((Account_Stores x) => (int?)x.ID == item.StoreID).Store_Name;
					dataRow["Quantity"] = FUNC_DecimalNumQty(item.Quantity);
					dataRow["UnitNum"] = 0;
					dataRow["UnitPurchasePrice"] = FUNC_DecimalNum(item.UnitPrice);
					dataRow["UnitPriceDisc"] = FUNC_DecimalNum(item.NetUnitPrice);
					dataRow["UnitPrice"] = FUNC_DecimalNum(item_Items.SellPriceSmall);
					dataRow["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.UnitPrice) * Convert.ToDecimal(item.Quantity));
					dataRow["Total"] = FUNC_DecimalNum(item.TotalPrice + item.VAT);
					dataRow["ItemVatDisc"] = FUNC_DecimalNum(item.VAT_Discount);
					dataRow["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice) - Convert.ToDecimal(item.VAT_Discount));
					dataRow["OrginalPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetUnitPrice) + Convert.ToDecimal(item.VAT_Discount));
					dataRow["DeleteItem"] = 0;
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["ItemVat"] = FUNC_DecimalNum(item.VAT);
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice) + Convert.ToDecimal(item.VAT_Discount));
						dataRow["IsItemTax"] = 1;
						dataRow["Tax_Value"] = FUNC_DecimalNum(item_Items.Tax_Value);
					}
					else
					{
						dataRow["IsItemTax"] = 0;
						dataRow["Tax_Value"] = 0;
						dataRow["ItemVat"] = FUNC_DecimalNum(item.VAT);
						dataRow["Net"] = FUNC_DecimalNum(item.NetTotalPrice);
					}
					datatable.Rows.Add(dataRow);
					num++;
				}
				TxtType.SelectedValue = order_PurchasesOrder.Order_Paymant_Type;
				CBProject.SelectedValue = order_PurchasesOrder.ProjectId;
				CBCostCenter.SelectedValue = order_PurchasesOrder.CostCentersID;
				TxtCode.Text = string.Empty;
				LblSup.Text = order_PurchasesOrder.SupplierID.ToString();
				TxtSuppliers.Text = order_PurchasesOrder.SupplierName;
				Charge(Convert.ToInt32(order_PurchasesOrder.SupplierID));
				TxtPhone.Text = order_PurchasesOrder.SupplierPhone;
				TxtNumVat.Text = order_PurchasesOrder.SupplierVatNum;
				TxtDate.DateTime = Convert.ToDateTime(order_PurchasesOrder.Purchases_Date);
				TxtNote.Text = order_PurchasesOrder.Note;
				TxtNumNote.Text = order_PurchasesOrder.NoteNum;
				TxtTotal.Text = Convert.ToString(order_PurchasesOrder.TotalPrices);
				TxtVat.Text = Convert.ToString(order_PurchasesOrder.Tax);
				TxtTotal.Text = order_PurchasesOrder.Safy.ToString();
				TxtDiscount.Text = order_PurchasesOrder.DiscountNum.ToString();
				TxtVat1.Text = order_PurchasesOrder.TotalPrices_Discount.ToString();
				TxtVatDisc.Text = order_PurchasesOrder.Tax_Discount.ToString();
				TxtTotalDiscount.Text = order_PurchasesOrder.TotalPrices_Discount.ToString();
				TxtNet.Text = order_PurchasesOrder.Net.ToString();
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnDelete.Enabled = false;
				gridView1.OptionsBehavior.Editable = true;
				ArrangeDatatable();
				IsEnableTrue();
				InsertNewRow();
				gridControl1.DataSource = datatable;
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
		TxtVat1.DecimalDigits = DecimalNum;
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
		string text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ItemCode"));
		string text2 = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SN"));
		if (!(text != string.Empty))
		{
			return;
		}
		LoginDetails.ItemcodeForUnits = text;
		FrmSearchUnit frmSearchUnit = new FrmSearchUnit();
		frmSearchUnit.ShowDialog();
		if (frmSearchUnit.ItemCode != string.Empty)
		{
			string itemCode = frmSearchUnit.ItemCode;
			Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(itemCode).FirstOrDefault();
			if (frmSearchUnit.unitName == "الصغرى" && get_Item_ByCodeALL_Result != null)
			{
				int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				foreach (DataRow row in datatable.Rows)
				{
					if (row["SN"].ToString() == Convert.ToString(text2))
					{
						row["UnitType"] = "الصغرى";
						row["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
						row["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
						row["Quantity"] = 1;
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
							row["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost / (decimal?)value);
							row["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost), DecimalNum);
							row["OrginalPrice"] = row["Total"];
						}
						else
						{
							row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
							row["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost);
							row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost), DecimalNum);
							row["OrginalPrice"] = row["SumPrice"];
						}
					}
				}
				frmSearchUnit.Close();
			}
			if (frmSearchUnit.unitName == "المتوسطة")
			{
				if (get_Item_ByCodeALL_Result != null)
				{
					int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
					foreach (DataRow row2 in datatable.Rows)
					{
						if (row2["SN"].ToString() == Convert.ToString(text2))
						{
							row2["UnitType"] = "المتوسطة";
							row2["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitMediumID;
							row2["UnitName"] = get_Item_ByCodeALL_Result.UnitMedium;
							row2["Quantity"] = 1;
							if (LoginDetails.ItemTotalWithVat)
							{
								decimal value2 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
								row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
								row2["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost2 / (decimal?)value2);
								row2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost2), DecimalNum);
								row2["OrginalPrice"] = row2["Total"];
							}
							else
							{
								row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
								row2["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost2);
								row2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost2), DecimalNum);
								row2["OrginalPrice"] = row2["SumPrice"];
							}
						}
						frmSearchUnit.Close();
					}
				}
				frmSearchUnit.Close();
			}
			if (frmSearchUnit.unitName == "الكبرى" && get_Item_ByCodeALL_Result != null)
			{
				int num3 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				foreach (DataRow row3 in datatable.Rows)
				{
					if (row3["SN"].ToString() == Convert.ToString(text2))
					{
						row3["UnitType"] = "الكبرى";
						row3["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitLargeID;
						row3["UnitName"] = get_Item_ByCodeALL_Result.UnitLarge;
						row3["Quantity"] = 1;
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
							row3["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost3 / (decimal?)value3);
							row3["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost3), DecimalNum);
							row3["OrginalPrice"] = row3["Total"];
						}
						else
						{
							row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
							row3["UnitPurchasePrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.LastCost3);
							row3["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.LastCost3), DecimalNum);
							row3["OrginalPrice"] = row3["SumPrice"];
						}
					}
				}
				frmSearchUnit.Close();
			}
			gridControl1.DataSource = datatable;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
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
			if (row["SN"].ToString() == Convert.ToString(num) && row["ItemCode"].ToString() != string.Empty)
			{
				row.Delete();
				datatable.AcceptChanges();
				gridView1.FocusedColumn = gridView1.VisibleColumns[0];
				gridView1.ShowEditor();
				break;
			}
		}
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
		ArrangeDatatable();
		gridControl1.DataSource = datatable;
	}

	private void repositoryItemButtonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string Itemocde = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ItemCode"));
		if (Itemocde != string.Empty)
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
						if (row["SN"].ToString() == Convert.ToString(num))
						{
							row["StoreName"] = Comp_Store.Text;
							row["StoreID"] = Comp_Store.SelectedValue;
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
				if (row2["SN"].ToString() == Convert.ToString(num2))
				{
					row2["StoreName"] = value;
					row2["StoreID"] = StoreID;
				}
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[5];
			gridView1.ShowEditor();
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
		LoginDetails.ItemSearch = null;
	}

	private void repositoryVatCalc_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["SN"].ToString() == Convert.ToString(num))
			{
				FrmVAT frmVAT = new FrmVAT();
				frmVAT.ShowDialog();
				if (frmVAT.ISFrmClosing)
				{
					row["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(frmVAT.TxtNeed.Value));
					datatable.AcceptChanges();
					break;
				}
			}
		}
		gridView1.FocusedColumn = gridView1.VisibleColumns[12];
		gridView1.ShowEditor();
		ArrangeDatatable();
		gridControl1.DataSource = datatable;
	}

	public void UpdateUnitPriceInGrid(CellValueChangedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["SN"].ToString() == Convert.ToString(num))
			{
				row["UnitPrice"] = Convert.ToDecimal(e.Value);
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
				break;
			}
		}
	}

	private void repositoryVatCalcLastCost_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["SN"].ToString() == Convert.ToString(num))
			{
				FrmVAT frmVAT = new FrmVAT();
				frmVAT.ShowDialog();
				if (frmVAT.ISFrmClosing)
				{
					row["UnitPurchasePrice"] = Convert.ToDecimal(frmVAT.TxtNeed.Value);
					UpdateItemRowClcVat("UnitPurchasePrice", Convert.ToDecimal(row["UnitPurchasePrice"]));
					Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
					break;
				}
			}
		}
		gridView1.FocusedColumn = gridView1.VisibleColumns[12];
		gridView1.ShowEditor();
		ArrangeDatatable();
		gridControl1.DataSource = datatable;
	}

	private void InsertNewItemUnitSmall(string Barcode)
	{
		Get_Item_ByCodeALL_Result ItemDetails = ITM.GetItemByCode(Barcode).FirstOrDefault();
		Item_Items item_Items = (from x in ITM.GetItemByCode()
			where x.Item_code == ItemDetails.Item_code
			select x).FirstOrDefault();
		if (ItemDetails == null)
		{
			return;
		}
		foreach (DataRow row in datatable.Rows)
		{
			int num = Convert.ToInt32(row["SN"]);
			if (datatable.Rows.Count == 1)
			{
				SN = num;
			}
			if (num != SN)
			{
				continue;
			}
			row["ItemID"] = ItemDetails.ItemId;
			row["ItemCode"] = ItemDetails.Item_code;
			row["ItemName"] = ItemDetails.item_Name;
			row["ItemNote"] = string.Empty;
			row["UnitType"] = "الصغرى";
			row["ItemUnitID"] = ItemDetails.UnitSmallID;
			row["UnitName"] = ItemDetails.UnitSmall;
			row["Quantity"] = 1;
			row["BounceItem"] = 0;
			row["Height"] = 1;
			row["Width"] = 1;
			row["Long"] = 1;
			row["Weight"] = 1;
			row["UnitNum"] = 0;
			row["StoreName"] = Comp_Store.GetItemText(Comp_Store.SelectedItem);
			row["StoreID"] = Comp_Store.SelectedValue;
			row["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(ItemDetails.SellPriceSmall));
			row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(ItemDetails.LastCost));
			row["OrginalPrice"] = row["UnitPurchasePrice"];
			row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
			row["PercentDisc"] = 0;
			row["AmountDisc"] = 0;
			row["UnitPriceDisc"] = row["UnitPurchasePrice"];
			row["SumPriceDisc"] = row["UnitPriceDisc"];
			row["IsItemTax"] = ItemDetails.Is_Tax;
			row["Tax_Value"] = ItemDetails.Tax_Value;
			if (!PurORD.IsVatActive())
			{
				row["ItemVat"] = 0;
				row["IsTax"] = false;
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["ItemVatDisc"] = 0;
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
			}
			else if (Convert.ToBoolean(row["IsItemTax"]))
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["SumPrice"] = Convert.ToDecimal(row["Total"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
					row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["ItemVat"] = Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]);
					row["IsTax"] = true;
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				}
				else
				{
					decimal value = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceSmall * (decimal?)value);
					row["IsTax"] = true;
					row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				}
			}
			else
			{
				row["ItemVat"] = 0;
				row["IsTax"] = false;
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["ItemVatDisc"] = 0;
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
			}
			row["DeleteItem"] = 0;
			row["Done"] = 1;
		}
	}

	private void InsertNewItemMedium(string Barcode)
	{
		Get_Item_ByCodeALL_Result ItemDetails = ITM.GetItemByCode(Barcode).FirstOrDefault();
		Item_Items item_Items = (from x in ITM.GetItemByCode()
			where x.Item_code == ItemDetails.Item_code
			select x).FirstOrDefault();
		if (ItemDetails == null)
		{
			return;
		}
		foreach (DataRow row in datatable.Rows)
		{
			int num = Convert.ToInt32(row["SN"]);
			if (datatable.Rows.Count == 1)
			{
				SN = num;
			}
			if (num != SN)
			{
				continue;
			}
			row["ItemID"] = ItemDetails.ItemId;
			row["ItemCode"] = ItemDetails.Item_code;
			row["ItemName"] = ItemDetails.item_Name;
			row["ItemNote"] = string.Empty;
			row["UnitType"] = "المتوسطة";
			row["ItemUnitID"] = ItemDetails.UnitMediumID;
			row["UnitName"] = ItemDetails.UnitMedium;
			row["Quantity"] = 1;
			row["BounceItem"] = 0;
			row["Height"] = 1;
			row["Width"] = 1;
			row["Long"] = 1;
			row["Weight"] = 1;
			row["UnitNum"] = 0;
			row["StoreName"] = Comp_Store.GetItemText(Comp_Store.SelectedItem);
			row["StoreID"] = Comp_Store.SelectedValue;
			row["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(ItemDetails.SellPriceMedium));
			row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(ItemDetails.LastCost2));
			row["OrginalPrice"] = row["UnitPurchasePrice"];
			row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
			row["PercentDisc"] = 0;
			row["AmountDisc"] = 0;
			row["UnitPriceDisc"] = row["UnitPurchasePrice"];
			row["SumPriceDisc"] = row["UnitPriceDisc"];
			row["IsItemTax"] = ItemDetails.Is_Tax;
			row["Tax_Value"] = ItemDetails.Tax_Value;
			if (!PurORD.IsVatActive())
			{
				row["ItemVat"] = 0;
				row["IsTax"] = false;
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["ItemVatDisc"] = 0;
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
			}
			else if (Convert.ToBoolean(row["IsItemTax"]))
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["SumPrice"] = Convert.ToDecimal(row["Total"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
					row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["ItemVat"] = Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]);
					row["IsTax"] = true;
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				}
				else
				{
					decimal value = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellPriceMedium * (decimal?)value);
					row["IsTax"] = true;
					row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				}
			}
			else
			{
				row["ItemVat"] = 0;
				row["IsTax"] = false;
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["ItemVatDisc"] = 0;
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
			}
			row["DeleteItem"] = 0;
			row["Done"] = 1;
		}
	}

	private void InsertNewItemBig(string Barcode)
	{
		Get_Item_ByCodeALL_Result ItemDetails = ITM.GetItemByCode(Barcode).FirstOrDefault();
		Item_Items item_Items = (from x in ITM.GetItemByCode()
			where x.Item_code == ItemDetails.Item_code
			select x).FirstOrDefault();
		if (ItemDetails == null)
		{
			return;
		}
		foreach (DataRow row in datatable.Rows)
		{
			int num = Convert.ToInt32(row["SN"]);
			if (datatable.Rows.Count == 1)
			{
				SN = num;
			}
			if (num != SN)
			{
				continue;
			}
			row["ItemID"] = ItemDetails.ItemId;
			row["ItemCode"] = ItemDetails.Item_code;
			row["ItemName"] = ItemDetails.item_Name;
			row["ItemNote"] = string.Empty;
			row["UnitType"] = "الكبري";
			row["ItemUnitID"] = ItemDetails.UnitLargeID;
			row["UnitName"] = ItemDetails.UnitLarge;
			row["Quantity"] = 1;
			row["BounceItem"] = 0;
			row["Height"] = 1;
			row["Width"] = 1;
			row["Long"] = 1;
			row["Weight"] = 1;
			row["UnitNum"] = 0;
			row["StoreName"] = Comp_Store.GetItemText(Comp_Store.SelectedItem);
			row["StoreID"] = Comp_Store.SelectedValue;
			row["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(ItemDetails.SellpriceLarge));
			row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(ItemDetails.LastCost3));
			row["OrginalPrice"] = row["UnitPurchasePrice"];
			row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
			row["PercentDisc"] = 0;
			row["AmountDisc"] = 0;
			row["UnitPriceDisc"] = row["UnitPurchasePrice"];
			row["SumPriceDisc"] = row["UnitPriceDisc"];
			row["IsItemTax"] = ItemDetails.Is_Tax;
			row["Tax_Value"] = ItemDetails.Tax_Value;
			if (!PurORD.IsVatActive())
			{
				row["ItemVat"] = 0;
				row["IsTax"] = false;
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["ItemVatDisc"] = 0;
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
			}
			else if (Convert.ToBoolean(row["IsItemTax"]))
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["SumPrice"] = Convert.ToDecimal(row["Total"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
					row["UnitPurchasePrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
					row["ItemVat"] = Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]);
					row["IsTax"] = true;
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPurchasePrice"]));
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				}
				else
				{
					decimal value = Convert.ToDecimal(ItemDetails.Tax_Value) / 100m + 1m;
					row["UnitPrice"] = FUNC_DecimalNum(ItemDetails.SellpriceLarge * (decimal?)value);
					row["IsTax"] = true;
					row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				}
			}
			else
			{
				row["ItemVat"] = 0;
				row["IsTax"] = false;
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["ItemVatDisc"] = 0;
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
			}
			row["DeleteItem"] = 0;
			row["Done"] = 1;
		}
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		try
		{
			TxtPhone.Properties.MaxLength = 14;
			TxtNumVat.Properties.MaxLength = 15;
			if (LoginDetails.Lang == "ar-EG")
			{
				Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
			}
			else
			{
				Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
			}
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
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
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
			else
			{
				Edit();
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
		if (e.KeyCode.ToString() == "F11")
		{
			int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
			{
				foreach (DataRow row in datatable.Rows)
				{
					if (row["SN"].ToString() == Convert.ToString(num))
					{
						Class_Items class_Items = new Class_Items();
						int itemID = class_Items.GetItemID(row["ItemCode"].ToString());
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
			TxtSuppliers.Text = "مورد نقدى";
			TxtSuppliers.ForeColor = Color.Black;
			TxtType.ForeColor = Color.Black;
			TxtCharge.Text = "0.00";
		}
		if (TxtType.Text == "آجلة")
		{
			TxtSuppliers.Text = string.Empty;
			TxtSuppliers.ForeColor = Color.Red;
			TxtType.ForeColor = Color.Red;
			TxtCharge.Text = "0.00";
		}
		TxtSuppliers.Focus();
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
		if (e.Control && e.KeyCode == Keys.V)
		{
			e.SuppressKeyPress = true;
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
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		try
		{
			FrmSearchSuppliers frmSearchSuppliers = new FrmSearchSuppliers();
			frmSearchSuppliers.ShowDialog();
			if (frmSearchSuppliers.CustCode > 0)
			{
				GetCustomer(frmSearchSuppliers.CustCode);
			}
		}
		catch (Exception)
		{
			Clear();
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
		GetPurchesOrder();
	}

	private void LblSup_TextChanged(object sender, EventArgs e)
	{
		try
		{
			LBlSuppAccount.Text = SPR.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
		}
		catch
		{
		}
	}

	private void MenuPrintQued_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text != string.Empty)
		{
			Class_Print_DailyRestrictionsALL class_Print_DailyRestrictionsALL = new Class_Print_DailyRestrictionsALL();
			class_Print_DailyRestrictionsALL.Print_DailyRestrictionsALL(Convert.ToInt32(TxtCode.Text), 4);
		}
	}

	private void FrmPurchases_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason != CloseReason.UserClosing)
		{
			return;
		}
		DialogResult dialogResult = MessageBox.Show("هل انت متأكد من اغلاق شاشة المشتريات", "اغلاق المشتريات", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		if (dialogResult == DialogResult.Yes)
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
		else
		{
			e.Cancel = true;
		}
	}

	private void MenuExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void كلالكمياتبالمستودعاتToolStripMenuItem_Click(object sender, EventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
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
	}

	private void حركةصنفToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 102))
		{
			FrmRPMovementItemActive frmRPMovementItemActive = new FrmRPMovementItemActive();
			frmRPMovementItemActive.lblItemID.Text = ItmID.ToString();
			frmRPMovementItemActive.ItemActive.Text = ItmName.ToString();
			frmRPMovementItemActive.ShowDialog();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnNewSuppliers_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 24))
		{
			new FrmSuppliers().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Comp_Store_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void BtnOrderSearch_Click(object sender, EventArgs e)
	{
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

	private decimal FUNC_DecimalNumQty(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNumQty);
	}

	private void BtnPrintBarcode_Click(object sender, EventArgs e)
	{
		try
		{
			if (!(TxtCode.Text != string.Empty))
			{
				return;
			}
			FrmPrinter frmPrinter = new FrmPrinter();
			frmPrinter.ShowDialog();
			string PrntName = frmPrinter.comboPrint.Text;
			Thread thread = new Thread((ThreadStart)delegate
			{
				BeginInvoke((Action)delegate
				{
					PrintBarcode(PrntName);
				});
			});
			thread.Start();
		}
		catch (Exception)
		{
		}
	}

	private void حركةالصنفكاملةToolStripMenuItem_Click(object sender, EventArgs e)
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

	private void BtnOrderSearch_InvalidValue(object sender, InvalidValueExceptionEventArgs e)
	{
	}

	private void BtnOrderSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (string.IsNullOrWhiteSpace(BtnOrderSearch.Text))
			{
				MessageBox.Show("إدخل كود الفاتورة");
				BtnOrderSearch.Focus();
			}
			else
			{
				SearchOrder(Convert.ToInt32(BtnOrderSearch.Text));
			}
		}
	}

	private void Comp_Store_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void BtnOrderSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(BtnOrderSearch.Text))
		{
			MessageBox.Show("إدخل كود الفاتورة");
			BtnOrderSearch.Focus();
		}
		else
		{
			SearchOrder(Convert.ToInt32(BtnOrderSearch.Text));
		}
	}

	private void BtnSuppliersSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(BtnSuppliersSearch.Text))
		{
			MessageBox.Show("إدخل كود المورد");
			BtnOrderSearch.Focus();
		}
		else
		{
			GetCustomer(Convert.ToInt32(BtnSuppliersSearch.Text));
		}
	}

	private void BtnSuppliersSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (string.IsNullOrWhiteSpace(BtnSuppliersSearch.Text))
			{
				MessageBox.Show("إدخل كود المورد");
				BtnSuppliersSearch.Focus();
			}
			else
			{
				GetCustomer(Convert.ToInt32(BtnSuppliersSearch.Text));
			}
		}
	}

	private void SearchOrder(int PurchacheId)
	{
		if (PurchacheId <= 0)
		{
			return;
		}
		Order_Purchases order_Purchases = Pur.GetPurchases(PurchacheId).FirstOrDefault();
		if (order_Purchases != null)
		{
			datatable.Rows.Clear();
			List<Get_Item_BySearchPurchases_Result> list = Pur.Get_Item_BySearchPurchases(PurchacheId);
			foreach (Get_Item_BySearchPurchases_Result item in list)
			{
				DataRow dataRow = null;
				dataRow = datatable.NewRow();
				dataRow["ItemUnitID"] = item.ItemUnitID;
				dataRow["ItemID"] = item.ItemId;
				dataRow["ItemCode"] = item.Item_code;
				dataRow["ItemName"] = item.item_Name;
				dataRow["UnitType"] = item.ItemUnitType;
				dataRow["UnitName"] = PurORD.GetUnitNameByID(Convert.ToInt32(item.ItemUnitID));
				dataRow["StoreID"] = item.StoreID;
				dataRow["StoreName"] = item.StoreName;
				dataRow["Quantity"] = FUNC_DecimalNumQty(item.Quantity);
				dataRow["UnitNum"] = FUNC_DecimalNum(item.UnitNumber);
				dataRow["BounceItem"] = FUNC_DecimalNum(item.Bounce);
				dataRow["ItemNote"] = item.NoteItem;
				dataRow["UnitPurchasePrice"] = FUNC_DecimalNum(item.UnitPrice);
				dataRow["OrginalPrice"] = FUNC_DecimalNum(item.UnitPrice);
				dataRow["UnitPrice"] = FUNC_DecimalNum(item.SellPrice);
				dataRow["SumPrice"] = FUNC_DecimalNum(item.TotalPrice);
				dataRow["DeleteItem"] = 0;
				dataRow["ItemVat"] = FUNC_DecimalNum(item.VAT);
				decimal? vAT = item.VAT;
				if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
				{
					dataRow["IsTax"] = true;
					dataRow["IsItemTax"] = true;
				}
				else
				{
					dataRow["ItemVat"] = "787898782592002";
					dataRow["IsTax"] = false;
					dataRow["IsItemTax"] = false;
				}
				dataRow["Total"] = FUNC_DecimalNum(item.VAT + item.TotalPrice);
				dataRow["Net"] = FUNC_DecimalNum(item.NetTotalPrice);
				dataRow["Tax_Value"] = FUNC_DecimalNum(item.Tax_Value);
				dataRow["UnitPriceDisc"] = FUNC_DecimalNum(item.NetUnitPrice);
				dataRow["SumPriceDisc"] = FUNC_DecimalNum(item.NetTotalPrice - item.VAT_Discount);
				dataRow["ItemVatDisc"] = FUNC_DecimalNum(item.VAT_Discount);
				dataRow["AmountDisc"] = FUNC_DecimalNum(item.DiscNum);
				dataRow["PercentDisc"] = FUNC_DecimalNum(item.DiscPercent);
				datatable.Rows.Add(dataRow);
			}
			gridControl1.DataSource = datatable;
			GetCustomer(Convert.ToInt32(order_Purchases.SupplierID));
			CBProject.SelectedValue = order_Purchases.ProjectId;
			TxtType.SelectedValue = order_Purchases.Order_Paymant_Type;
			CBCostCenter.SelectedValue = order_Purchases.CostCentersID;
			TxtCode.Text = order_Purchases.PurBranchID.ToString();
			LblSup.Text = order_Purchases.SupplierID.ToString();
			TxtSuppliers.Text = order_Purchases.SupplierName;
			TxtPhone.Text = order_Purchases.SupplierPhone;
			TxtNumVat.Text = order_Purchases.SupplierVatNum;
			TxtDate.DateTime = Convert.ToDateTime(order_Purchases.Purchases_Date);
			TxtNote.Text = order_Purchases.Note;
			TxtNumNote.Text = order_Purchases.NoteNum;
			TxtTotals.Text = Convert.ToString(order_Purchases.TotalPrices);
			TxtVat.Text = Convert.ToString(order_Purchases.Tax);
			TxtTotal.Text = order_Purchases.TotalPrices_Discount.ToString();
			TxtDiscount.Text = order_Purchases.DiscountNum.ToString();
			txtdicoutprege.Text = order_Purchases.DiscountPerantage.ToString();
			TxtVat1.Text = order_Purchases.Tax.ToString();
			TxtVatDisc.Text = order_Purchases.Tax_Discount.ToString();
			TxtTotalDiscount.Text = order_Purchases.TotalPrices_Discount.ToString();
			TxtNet.Text = order_Purchases.Net.ToString();
			BtnSave.Enabled = false;
			BtnEdit.Enabled = true;
			BtnEdit.Text = "F6 تعديل";
			BtnDelete.Enabled = true;
			TxtSuppliers.Focus();
			gridView1.OptionsBehavior.Editable = false;
			ArrangeDatatable();
			IsEnableFalse();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			Charge(Convert.ToInt32(LblSup.Text));
		}
		else
		{
			MessageBox.Show("عفوا لا يوجد قاتورة مستريات بهذا الرقم");
		}
	}

	private void ClearSupp()
	{
		TxtSuppliers.Text = string.Empty;
		TxtAdditionalNum.Text = string.Empty;
		TxtAddress.Text = string.Empty;
		TxtPostalCode.Text = string.Empty;
		TxtPhone.Text = string.Empty;
		TxtBuildingNum.Text = string.Empty;
		TxtNumVat.Text = string.Empty;
	}

	private void BtnCovertSales_Click(object sender, EventArgs e)
	{
		if (datatable.Rows.Count > 0 && !string.IsNullOrWhiteSpace(datatable.Rows[0][0].ToString()))
		{
			datatable = (DataTable)gridControl1.DataSource;
			FrmOrders frmOrders = new FrmOrders(datatable);
			frmOrders.TxtNote.Text = TxtNote.Text;
			frmOrders.TxtNumNote.Text = TxtNumNote.Text;
			frmOrders.TxtType.SelectedValue = Convert.ToInt32(TxtType.SelectedValue);
			frmOrders.CBProject.SelectedValue = Convert.ToInt32(CBProject.SelectedValue);
			frmOrders.CBCostCenter.SelectedValue = Convert.ToInt32(CBCostCenter.SelectedValue);
			frmOrders.Show();
		}
		else
		{
			MessageBox.Show("لا يوجد اصناف");
		}
	}

	private void BtnPayment_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 32))
		{
			new FrmPayment().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnORDPurchaseReturnID_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 15))
		{
			new FrmPurchasesReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
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

	private void ZeroDisc()
	{
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		Cal_FinalResultTotals(0m, "DiscAmount");
	}

	private void TxtDiscount_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void txtdicoutprege_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
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

	private void TxtAllDiscount_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (TxtAllDiscount.Text != string.Empty && Convert.ToDecimal(TxtAllDiscount.Text) != 0m && Convert.ToDecimal(TxtAllDiscount.Text) < Convert.ToDecimal(TxtTotal.Text) + Convert.ToDecimal(TxtVat.Text))
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

	private void TxtNumVat_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void TxtPhone_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void TxtAdditionalNum_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void TxtAdditionalNum_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.V)
		{
			e.SuppressKeyPress = true;
		}
	}

	private void TxtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void TxtPostalCode_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.V)
		{
			e.SuppressKeyPress = true;
		}
	}

	private void TxtBuildingNum_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void TxtBuildingNum_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.V)
		{
			e.SuppressKeyPress = true;
		}
	}

	private void groupBox5_Enter(object sender, EventArgs e)
	{
	}

	private void TxtDiscount_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (TxtDiscount.Text != string.Empty && Convert.ToDecimal(TxtDiscount.Text) != 0m && Convert.ToDecimal(TxtDiscount.Text) < Convert.ToDecimal(TxtTotal.Text))
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmPurchases));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions imageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions5 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions6 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions7 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled8 = new DevExpress.Utils.SerializableAppearanceObject();
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.TxtNumVat = new DevExpress.XtraEditors.TextEdit();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtAllDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnPrintBarcode = new DevExpress.XtraEditors.SimpleButton();
		this.BtnORDPurchaseReturnID = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPayment = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCovertSales = new DevExpress.XtraEditors.SimpleButton();
		this.label27 = new System.Windows.Forms.Label();
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
		this.label7 = new System.Windows.Forms.Label();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label8 = new System.Windows.Forms.Label();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.TxtSellPrice3 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVat1 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
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
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
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
		this.حركةالصنفكاملةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPurchOrder = new System.Windows.Forms.ToolStripMenuItem();
		this.استعلامToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryVatCalcLastCost = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryVatCalc = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
		this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
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
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.label48 = new System.Windows.Forms.Label();
		this.CBProject = new System.Windows.Forms.ComboBox();
		this.label36 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.Comp_Store = new System.Windows.Forms.ComboBox();
		this.label28 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.BtnOrderSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.label30 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label34 = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.CBBounce = new System.Windows.Forms.ComboBox();
		this.LblLastPrice = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.BtnSuppliersSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnNewSuppliers = new DevExpress.XtraEditors.SimpleButton();
		this.label18 = new System.Windows.Forms.Label();
		this.label37 = new System.Windows.Forms.Label();
		this.label38 = new System.Windows.Forms.Label();
		this.label39 = new System.Windows.Forms.Label();
		this.label42 = new System.Windows.Forms.Label();
		this.label43 = new System.Windows.Forms.Label();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.TxtAdditionalNum = new DevExpress.XtraEditors.TextEdit();
		this.TxtBuildingNum = new DevExpress.XtraEditors.TextEdit();
		this.TxtPostalCode = new DevExpress.XtraEditors.TextEdit();
		this.TxtAddress = new DevExpress.XtraEditors.TextEdit();
		this.label41 = new System.Windows.Forms.Label();
		this.label46 = new System.Windows.Forms.Label();
		this.label45 = new System.Windows.Forms.Label();
		this.label47 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryVatCalcLastCost).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryVatCalc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).BeginInit();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnSuppliersSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAdditionalNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBuildingNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.repositoryItemButtonEditCode, "repositoryItemButtonEditCode");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCode.Buttons"))
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
		this.repositoryItemButtonEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		editorButtonImageOptions.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), imageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		resources.ApplyResources(this.LBlSuppAccount, "LBlSuppAccount");
		this.LBlSuppAccount.Name = "LBlSuppAccount";
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
		this.TxtDate.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtNumNote, "TxtNumNote");
		this.TxtNumNote.Name = "TxtNumNote";
		this.TxtNumNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumNote.Properties.Appearance.Font");
		this.TxtNumNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNumNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumNote.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNumNote.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNote.Properties.Appearance.Font");
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
		this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPhone_KeyPress);
		resources.ApplyResources(this.TxtType, "TxtType");
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Name = "TxtType";
		this.TxtType.SelectedIndexChanged += new System.EventHandler(TxtType_SelectedIndexChanged_1);
		this.TxtType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress);
		resources.ApplyResources(this.TxtNumVat, "TxtNumVat");
		this.TxtNumVat.Name = "TxtNumVat";
		this.TxtNumVat.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumVat.Properties.Appearance.Font");
		this.TxtNumVat.Properties.Appearance.Options.UseFont = true;
		this.TxtNumVat.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumVat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumVat.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNumVat.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtNumVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumVat_KeyPress);
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
		this.TxtSuppliers.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlTxtNumVat_KeyDown);
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
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.TxtTotal);
		this.groupBox2.Controls.Add(this.TxtAllDiscount);
		this.groupBox2.Controls.Add(this.BtnPrintBarcode);
		this.groupBox2.Controls.Add(this.BtnORDPurchaseReturnID);
		this.groupBox2.Controls.Add(this.BtnPayment);
		this.groupBox2.Controls.Add(this.BtnCovertSales);
		this.groupBox2.Controls.Add(this.label27);
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
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.TxtSellPrice3);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.TxtVat1);
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
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtTotal, "TxtTotal");
		this.TxtTotal.DecimalDigits = 3;
		this.TxtTotal.ForeColor = System.Drawing.Color.Black;
		this.TxtTotal.Name = "TxtTotal";
		this.TxtTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotal.Value = new decimal(new int[4]);
		this.TxtTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtAllDiscount, "TxtAllDiscount");
		this.TxtAllDiscount.DecimalDigits = 3;
		this.TxtAllDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtAllDiscount.Name = "TxtAllDiscount";
		this.TxtAllDiscount.ReadOnly = true;
		this.TxtAllDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllDiscount.Value = new decimal(new int[4]);
		this.TxtAllDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtAllDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtAllDiscount_KeyUp);
		resources.ApplyResources(this.BtnPrintBarcode, "BtnPrintBarcode");
		this.BtnPrintBarcode.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrintBarcode.Appearance.Font");
		this.BtnPrintBarcode.Appearance.Options.UseFont = true;
		this.BtnPrintBarcode.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrintBarcode.Name = "BtnPrintBarcode";
		this.BtnPrintBarcode.Click += new System.EventHandler(BtnPrintBarcode_Click);
		resources.ApplyResources(this.BtnORDPurchaseReturnID, "BtnORDPurchaseReturnID");
		this.BtnORDPurchaseReturnID.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnORDPurchaseReturnID.Appearance.Font");
		this.BtnORDPurchaseReturnID.Appearance.Options.UseFont = true;
		this.BtnORDPurchaseReturnID.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDPurchaseReturnID.ImageOptions.Image");
		this.BtnORDPurchaseReturnID.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnORDPurchaseReturnID.Name = "BtnORDPurchaseReturnID";
		this.BtnORDPurchaseReturnID.Click += new System.EventHandler(BtnORDPurchaseReturnID_Click);
		resources.ApplyResources(this.BtnPayment, "BtnPayment");
		this.BtnPayment.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPayment.Appearance.Font");
		this.BtnPayment.Appearance.Options.UseFont = true;
		this.BtnPayment.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPayment.ImageOptions.Image");
		this.BtnPayment.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPayment.Name = "BtnPayment";
		this.BtnPayment.Click += new System.EventHandler(BtnPayment_Click);
		resources.ApplyResources(this.BtnCovertSales, "BtnCovertSales");
		this.BtnCovertSales.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCovertSales.Appearance.Font");
		this.BtnCovertSales.Appearance.Options.UseFont = true;
		this.BtnCovertSales.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCovertSales.ImageOptions.Image");
		this.BtnCovertSales.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCovertSales.Name = "BtnCovertSales";
		this.BtnCovertSales.Click += new System.EventHandler(BtnCovertSales_Click);
		resources.ApplyResources(this.label27, "label27");
		this.label27.Name = "label27";
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
		this.txtdicoutprege.KeyDown += new System.Windows.Forms.KeyEventHandler(txtdicoutprege_KeyDown);
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
		this.TxtNet.TextChanged += new System.EventHandler(TxtNet_TextChanged);
		this.TxtNet.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
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
		resources.ApplyResources(this.BtnPrint, "BtnPrint");
		this.BtnPrint.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrint.Appearance.Font");
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
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
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDelete.Appearance.Font");
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
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
		resources.ApplyResources(this.TxtVat1, "TxtVat1");
		this.TxtVat1.DecimalDigits = 3;
		this.TxtVat1.ForeColor = System.Drawing.Color.Black;
		this.TxtVat1.Name = "TxtVat1";
		this.TxtVat1.ReadOnly = true;
		this.TxtVat1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat1.Value = new decimal(new int[4]);
		this.TxtVat1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.TxtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtDiscount_KeyDown);
		this.TxtDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtDiscount_KeyUp);
		resources.ApplyResources(this.TxtVatDisc, "TxtVatDisc");
		this.TxtVatDisc.DecimalDigits = 3;
		this.TxtVatDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatDisc.Name = "TxtVatDisc";
		this.TxtVatDisc.ReadOnly = true;
		this.TxtVatDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatDisc.Value = new decimal(new int[4]);
		this.TxtVatDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		this.ribbonPage2.Name = "ribbonPage2";
		resources.ApplyResources(this.ribbonPage2, "ribbonPage2");
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.طباعةToolStripMenuItem1, this.toolStripSeparator1, this.MenuNewItem, this.حركةصنفToolStripMenuItem, this.حركةالصنفكاملةToolStripMenuItem, this.MenuSearchItem,
			this.toolStripSeparator4, this.MenuPurchOrder, this.استعلامToolStripMenuItem1, this.toolStripSeparator2, this.تخصيصToolStripMenuItem1, this.toolStripSeparator3, this.MenuExit
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
		resources.ApplyResources(this.حركةالصنفكاملةToolStripMenuItem, "حركةالصنفكاملةToolStripMenuItem");
		this.حركةالصنفكاملةToolStripMenuItem.Name = "حركةالصنفكاملةToolStripMenuItem";
		this.حركةالصنفكاملةToolStripMenuItem.Click += new System.EventHandler(حركةالصنفكاملةToolStripMenuItem_Click);
		resources.ApplyResources(this.MenuSearchItem, "MenuSearchItem");
		this.MenuSearchItem.Name = "MenuSearchItem";
		this.MenuSearchItem.Click += new System.EventHandler(MenuSearchItem_Click);
		resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		resources.ApplyResources(this.MenuPurchOrder, "MenuPurchOrder");
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		this.MenuPurchOrder.Click += new System.EventHandler(MenuPurchOrder_Click);
		resources.ApplyResources(this.استعلامToolStripMenuItem1, "استعلامToolStripMenuItem1");
		this.استعلامToolStripMenuItem1.Name = "استعلامToolStripMenuItem1";
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
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[12]
		{
			this.repositoryItemButtonEdit4, this.repositoryItemButtonEdit6, this.repositoryItemTextEdit1, this.repositoryItemTextEdit2, this.repositoryItemDateEdit1, this.repositoryItemTextEdit3, this.repositoryItemPictureEdit1, this.repositoryItemButtonEdit5, this.repositoryItemButtonEditCodeSearch, this.repositoryItemDateEdit2,
			this.repositoryVatCalc, this.repositoryVatCalcLastCost
		});
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView1.Appearance.HeaderPanel.Font");
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		resources.ApplyResources(this.gridView1, "gridView1");
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[32]
		{
			this.gridColumn1, this.gridColumn2, this.gridColumn4, this.shearch, this.gridColumn20, this.gridColumn23, this.gridColumn21, this.gridColumn31, this.gridColumn30, this.gridColumn3,
			this.gridColumn6, this.gridColumn32, this.gridColumn7, this.gridColumn24, this.gridColumn26, this.gridColumn25, this.gridColumn8, this.gridColumn9, this.gridColumn22, this.gridColumn5,
			this.gridColumn19, this.gridColumn28, this.gridColumn29, this.gridColumn33, this.gridColumn34, this.gridColumn35, this.gridColumn36, this.gridColumn37, this.gridColumn38, this.gridColumn39,
			this.gridColumn40, this.gridColumn27
		});
		this.gridView1.DetailHeight = 431;
		this.gridView1.GridControl = this.gridControl1;
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
		this.gridView1.OptionsView.RowAutoHeight = true;
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridView1.OptionsView.ShowGroupPanel = false;
		this.gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanging);
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
		this.gridColumn1.MinWidth = 23;
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonEditCodeSearch, "repositoryItemButtonEditCodeSearch");
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions11");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons"), resources.GetString("repositoryItemButtonEditCodeSearch.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEditCodeSearch.Buttons6"), resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons9"))
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
		this.gridColumn2.MinWidth = 23;
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
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
		this.gridColumn4.MinWidth = 23;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions12");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"), resources.GetString("repositoryItemButtonEdit5.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit5.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonEdit5.Buttons6"), resources.GetObject("repositoryItemButtonEdit5.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit5.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit5.Buttons9"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEdit5.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
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
		this.shearch.FieldName = "UnitName";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.MinWidth = 23;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
		editorButtonImageOptions4.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions13");
		editorButtonImageOptions4.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemButtonEdit4.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
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
		this.gridColumn20.MinWidth = 23;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn20.Summary"), resources.GetString("gridColumn20.Summary1"), resources.GetString("gridColumn20.Summary2"))
		});
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn23, "gridColumn23");
		this.gridColumn23.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceCell.Font");
		this.gridColumn23.AppearanceCell.Options.UseFont = true;
		this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceHeader.Font");
		this.gridColumn23.AppearanceHeader.Options.UseFont = true;
		this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.FieldName = "BounceItem";
		this.gridColumn23.MinWidth = 28;
		this.gridColumn23.Name = "gridColumn23";
		resources.ApplyResources(this.gridColumn21, "gridColumn21");
		this.gridColumn21.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn21.AppearanceCell.Font");
		this.gridColumn21.AppearanceCell.Options.UseFont = true;
		this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn21.AppearanceHeader.Font");
		this.gridColumn21.AppearanceHeader.Options.UseFont = true;
		this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.ColumnEdit = this.repositoryVatCalcLastCost;
		this.gridColumn21.FieldName = "UnitPurchasePrice";
		this.gridColumn21.MinWidth = 28;
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn21.Summary"), resources.GetString("gridColumn21.Summary1"), resources.GetString("gridColumn21.Summary2"))
		});
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions14");
		this.repositoryVatCalcLastCost.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryVatCalcLastCost.Buttons"), resources.GetString("repositoryVatCalcLastCost.Buttons1"), (int)resources.GetObject("repositoryVatCalcLastCost.Buttons2"), (bool)resources.GetObject("repositoryVatCalcLastCost.Buttons3"), (bool)resources.GetObject("repositoryVatCalcLastCost.Buttons4"), (bool)resources.GetObject("repositoryVatCalcLastCost.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("repositoryVatCalcLastCost.Buttons6"), resources.GetObject("repositoryVatCalcLastCost.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryVatCalcLastCost.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryVatCalcLastCost.Buttons9"))
		});
		this.repositoryVatCalcLastCost.Name = "repositoryVatCalcLastCost";
		this.repositoryVatCalcLastCost.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryVatCalcLastCost_ButtonClick);
		resources.ApplyResources(this.gridColumn31, "gridColumn31");
		this.gridColumn31.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn31.AppearanceCell.Font");
		this.gridColumn31.AppearanceCell.Options.UseFont = true;
		this.gridColumn31.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn31.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn31.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn31.AppearanceHeader.Font");
		this.gridColumn31.AppearanceHeader.Options.UseFont = true;
		this.gridColumn31.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn31.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn31.FieldName = "AmountDisc";
		this.gridColumn31.MinWidth = 28;
		this.gridColumn31.Name = "gridColumn31";
		this.gridColumn31.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn30.FieldName = "PercentDisc";
		this.gridColumn30.MinWidth = 28;
		this.gridColumn30.Name = "gridColumn30";
		this.gridColumn30.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn3.MinWidth = 23;
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.OptionsColumn.AllowFocus = false;
		this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn3.Summary"), resources.GetString("gridColumn3.Summary1"), resources.GetString("gridColumn3.Summary2"))
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
		this.gridColumn6.MinWidth = 23;
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.AllowFocus = false;
		this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn6.OptionsColumn.ReadOnly = true;
		this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn6.Summary"), resources.GetString("gridColumn6.Summary1"), resources.GetString("gridColumn6.Summary2"))
		});
		this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn32, "gridColumn32");
		this.gridColumn32.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn32.AppearanceCell.Font");
		this.gridColumn32.AppearanceCell.Options.UseFont = true;
		this.gridColumn32.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn32.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn32.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn32.AppearanceHeader.Font");
		this.gridColumn32.AppearanceHeader.Options.UseFont = true;
		this.gridColumn32.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn32.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn32.FieldName = "Net";
		this.gridColumn32.MinWidth = 28;
		this.gridColumn32.Name = "gridColumn32";
		this.gridColumn32.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn32.Summary"), resources.GetString("gridColumn32.Summary1"), resources.GetString("gridColumn32.Summary2"))
		});
		this.gridColumn32.UnboundDataType = typeof(decimal);
		resources.ApplyResources(this.gridColumn7, "gridColumn7");
		this.gridColumn7.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceCell.Font");
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceHeader.Font");
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.FieldName = "Total";
		this.gridColumn7.MinWidth = 23;
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.OptionsColumn.AllowFocus = false;
		this.gridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn7.OptionsColumn.ReadOnly = true;
		this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn7.Summary"), resources.GetString("gridColumn7.Summary1"), resources.GetString("gridColumn7.Summary2"))
		});
		this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn24, "gridColumn24");
		this.gridColumn24.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceCell.Font");
		this.gridColumn24.AppearanceCell.Options.UseFont = true;
		this.gridColumn24.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceHeader.Font");
		this.gridColumn24.AppearanceHeader.Options.UseFont = true;
		this.gridColumn24.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.ColumnEdit = this.repositoryVatCalc;
		this.gridColumn24.FieldName = "UnitPrice";
		this.gridColumn24.MinWidth = 28;
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn24.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions15");
		this.repositoryVatCalc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryVatCalc.Buttons"), resources.GetString("repositoryVatCalc.Buttons1"), (int)resources.GetObject("repositoryVatCalc.Buttons2"), (bool)resources.GetObject("repositoryVatCalc.Buttons3"), (bool)resources.GetObject("repositoryVatCalc.Buttons4"), (bool)resources.GetObject("repositoryVatCalc.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, resources.GetString("repositoryVatCalc.Buttons6"), resources.GetObject("repositoryVatCalc.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryVatCalc.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryVatCalc.Buttons9"))
		});
		this.repositoryVatCalc.Name = "repositoryVatCalc";
		this.repositoryVatCalc.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryVatCalc_ButtonClick);
		resources.ApplyResources(this.gridColumn26, "gridColumn26");
		this.gridColumn26.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceCell.Font");
		this.gridColumn26.AppearanceCell.Options.UseFont = true;
		this.gridColumn26.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceHeader.Font");
		this.gridColumn26.AppearanceHeader.Options.UseFont = true;
		this.gridColumn26.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn26.FieldName = "UnitNum";
		this.gridColumn26.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn26.MinWidth = 28;
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.OptionsColumn.AllowFocus = false;
		resources.ApplyResources(this.gridColumn25, "gridColumn25");
		this.gridColumn25.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceCell.Font");
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceHeader.Font");
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.FieldName = "ItemNote";
		this.gridColumn25.MinWidth = 28;
		this.gridColumn25.Name = "gridColumn25";
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
		this.gridColumn8.MinWidth = 23;
		this.gridColumn8.Name = "gridColumn8";
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit6, "repositoryItemButtonEdit6");
		resources.ApplyResources(editorButtonImageOptions7, "editorButtonImageOptions16");
		editorButtonImageOptions7.ImageUri.Uri = "Delete;Size16x16";
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
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
		this.gridColumn9.MinWidth = 23;
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
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
		this.gridColumn22.MinWidth = 23;
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn22.OptionsColumn.AllowFocus = false;
		this.gridColumn22.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn22.Summary"), resources.GetString("gridColumn22.Summary1"), resources.GetString("gridColumn22.Summary2"))
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
		this.gridColumn5.MinWidth = 23;
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.OptionsColumn.AllowFocus = false;
		this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn5.Summary"), resources.GetString("gridColumn5.Summary1"), resources.GetString("gridColumn5.Summary2"))
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
		this.gridColumn19.FieldName = "UnitType";
		this.gridColumn19.MinWidth = 28;
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
		this.gridColumn28.FieldName = "ItemID";
		this.gridColumn28.MinWidth = 28;
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
		this.gridColumn29.FieldName = "StoreID";
		this.gridColumn29.MinWidth = 28;
		this.gridColumn29.Name = "gridColumn29";
		this.gridColumn29.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn33.FieldName = "UnitPriceDisc";
		this.gridColumn33.MinWidth = 29;
		this.gridColumn33.Name = "gridColumn33";
		this.gridColumn33.OptionsColumn.AllowEdit = false;
		this.gridColumn33.OptionsColumn.AllowFocus = false;
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
		this.gridColumn34.FieldName = "Height";
		this.gridColumn34.MinWidth = 29;
		this.gridColumn34.Name = "gridColumn34";
		this.gridColumn34.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn35.FieldName = "Width";
		this.gridColumn35.MinWidth = 29;
		this.gridColumn35.Name = "gridColumn35";
		this.gridColumn35.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn36.FieldName = "Long";
		this.gridColumn36.MinWidth = 29;
		this.gridColumn36.Name = "gridColumn36";
		this.gridColumn36.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn37.FieldName = "Weight";
		this.gridColumn37.MinWidth = 29;
		this.gridColumn37.Name = "gridColumn37";
		this.gridColumn37.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn38, "gridColumn38");
		this.gridColumn38.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn38.AppearanceCell.Font");
		this.gridColumn38.AppearanceCell.Options.UseFont = true;
		this.gridColumn38.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn38.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn38.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn38.AppearanceHeader.Font");
		this.gridColumn38.AppearanceHeader.Options.UseFont = true;
		this.gridColumn38.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn38.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn38.FieldName = "ItemUnitID";
		this.gridColumn38.MinWidth = 29;
		this.gridColumn38.Name = "gridColumn38";
		this.gridColumn38.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn39, "gridColumn39");
		this.gridColumn39.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn39.AppearanceCell.Font");
		this.gridColumn39.AppearanceCell.Options.UseFont = true;
		this.gridColumn39.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn39.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn39.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn39.AppearanceHeader.Font");
		this.gridColumn39.AppearanceHeader.Options.UseFont = true;
		this.gridColumn39.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn39.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn39.FieldName = "Tax_Value";
		this.gridColumn39.MinWidth = 29;
		this.gridColumn39.Name = "gridColumn39";
		this.gridColumn39.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn39.Summary"), resources.GetString("gridColumn39.Summary1"), resources.GetString("gridColumn39.Summary2"))
		});
		resources.ApplyResources(this.gridColumn40, "gridColumn40");
		this.gridColumn40.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn40.AppearanceCell.Font");
		this.gridColumn40.AppearanceCell.Options.UseFont = true;
		this.gridColumn40.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn40.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn40.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn40.AppearanceHeader.Font");
		this.gridColumn40.AppearanceHeader.Options.UseFont = true;
		this.gridColumn40.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn40.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn40.FieldName = "Done";
		this.gridColumn40.MinWidth = 29;
		this.gridColumn40.Name = "gridColumn40";
		resources.ApplyResources(this.gridColumn27, "gridColumn27");
		this.gridColumn27.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceCell.Font");
		this.gridColumn27.AppearanceCell.Options.UseFont = true;
		this.gridColumn27.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceHeader.Font");
		this.gridColumn27.AppearanceHeader.Options.UseFont = true;
		this.gridColumn27.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.FieldName = "IsTax";
		this.gridColumn27.MinWidth = 25;
		this.gridColumn27.Name = "gridColumn27";
		this.gridColumn27.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		resources.ApplyResources(this.repositoryItemDateEdit2, "repositoryItemDateEdit2");
		this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit2.Buttons"))
		});
		this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit2.CalendarTimeProperties.Buttons"))
		});
		this.repositoryItemDateEdit2.Mask.EditMask = resources.GetString("repositoryItemDateEdit2.Mask.EditMask");
		this.repositoryItemDateEdit2.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("repositoryItemDateEdit2.Mask.UseMaskAsDisplayFormat");
		this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
		resources.ApplyResources(this.cardView1, "cardView1");
		this.cardView1.CardWidth = 232;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.DetailHeight = 431;
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		resources.ApplyResources(this.gridColumn10, "gridColumn10");
		this.gridColumn10.MinWidth = 23;
		this.gridColumn10.Name = "gridColumn10";
		resources.ApplyResources(this.gridColumn11, "gridColumn11");
		this.gridColumn11.MinWidth = 23;
		this.gridColumn11.Name = "gridColumn11";
		resources.ApplyResources(this.gridColumn12, "gridColumn12");
		this.gridColumn12.MinWidth = 23;
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn13, "gridColumn13");
		this.gridColumn13.MinWidth = 23;
		this.gridColumn13.Name = "gridColumn13";
		resources.ApplyResources(this.gridColumn14, "gridColumn14");
		this.gridColumn14.MinWidth = 23;
		this.gridColumn14.Name = "gridColumn14";
		resources.ApplyResources(this.gridColumn15, "gridColumn15");
		this.gridColumn15.MinWidth = 23;
		this.gridColumn15.Name = "gridColumn15";
		resources.ApplyResources(this.gridColumn16, "gridColumn16");
		this.gridColumn16.MinWidth = 23;
		this.gridColumn16.Name = "gridColumn16";
		resources.ApplyResources(this.gridColumn17, "gridColumn17");
		this.gridColumn17.MinWidth = 23;
		this.gridColumn17.Name = "gridColumn17";
		resources.ApplyResources(this.gridColumn18, "gridColumn18");
		this.gridColumn18.MinWidth = 23;
		this.gridColumn18.Name = "gridColumn18";
		resources.ApplyResources(this.groupBox5, "groupBox5");
		this.groupBox5.Controls.Add(this.LBlSuppAccount);
		this.groupBox5.Controls.Add(this.label48);
		this.groupBox5.Controls.Add(this.CBProject);
		this.groupBox5.Controls.Add(this.CBCostCenter);
		this.groupBox5.Controls.Add(this.TxtDate);
		this.groupBox5.Controls.Add(this.label36);
		this.groupBox5.Controls.Add(this.label33);
		this.groupBox5.Controls.Add(this.Comp_Store);
		this.groupBox5.Controls.Add(this.TxtNumNote);
		this.groupBox5.Controls.Add(this.TxtNote);
		this.groupBox5.Controls.Add(this.label28);
		this.groupBox5.Controls.Add(this.label17);
		this.groupBox5.Controls.Add(this.TxtType);
		this.groupBox5.Controls.Add(this.label29);
		this.groupBox5.Controls.Add(this.BtnOrderSearch);
		this.groupBox5.Controls.Add(this.label30);
		this.groupBox5.Controls.Add(this.label32);
		this.groupBox5.Controls.Add(this.label34);
		this.groupBox5.Controls.Add(this.label35);
		this.groupBox5.Controls.Add(this.TxtCode);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.TabStop = false;
		this.groupBox5.Enter += new System.EventHandler(groupBox5_Enter);
		resources.ApplyResources(this.label48, "label48");
		this.label48.Name = "label48";
		resources.ApplyResources(this.CBProject, "CBProject");
		this.CBProject.FormattingEnabled = true;
		this.CBProject.Name = "CBProject";
		this.CBProject.SelectedIndexChanged += new System.EventHandler(CBProject_SelectedIndexChanged);
		this.CBProject.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		this.CBProject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
		resources.ApplyResources(this.label36, "label36");
		this.label36.Name = "label36";
		resources.ApplyResources(this.label33, "label33");
		this.label33.Name = "label33";
		resources.ApplyResources(this.Comp_Store, "Comp_Store");
		this.Comp_Store.FormattingEnabled = true;
		this.Comp_Store.Name = "Comp_Store";
		this.Comp_Store.SelectedIndexChanged += new System.EventHandler(Comp_Store_SelectedIndexChanged);
		this.Comp_Store.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Comp_Store_KeyPress);
		resources.ApplyResources(this.label28, "label28");
		this.label28.Name = "label28";
		resources.ApplyResources(this.label17, "label17");
		this.label17.Name = "label17";
		resources.ApplyResources(this.label29, "label29");
		this.label29.Name = "label29";
		resources.ApplyResources(this.BtnOrderSearch, "BtnOrderSearch");
		this.BtnOrderSearch.Name = "BtnOrderSearch";
		this.BtnOrderSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnOrderSearch.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnOrderSearch.Properties.Appearance.Font");
		this.BtnOrderSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnOrderSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnOrderSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnOrderSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnOrderSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnOrderSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnOrderSearch.Properties.Buttons"))
		});
		this.BtnOrderSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnOrderSearch.Properties.NullText = resources.GetString("BtnOrderSearch.Properties.NullText");
		this.BtnOrderSearch.Tag = "بحث بالكود";
		this.BtnOrderSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnOrderSearch_ButtonClick);
		this.BtnOrderSearch.InvalidValue += new DevExpress.XtraEditors.Controls.InvalidValueExceptionEventHandler(BtnOrderSearch_InvalidValue);
		this.BtnOrderSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnOrderSearch_KeyDown);
		resources.ApplyResources(this.label30, "label30");
		this.label30.Name = "label30";
		resources.ApplyResources(this.label32, "label32");
		this.label32.Name = "label32";
		resources.ApplyResources(this.label34, "label34");
		this.label34.Name = "label34";
		resources.ApplyResources(this.label35, "label35");
		this.label35.Name = "label35";
		resources.ApplyResources(this.label31, "label31");
		this.label31.Name = "label31";
		resources.ApplyResources(this.CBBounce, "CBBounce");
		this.CBBounce.FormattingEnabled = true;
		this.CBBounce.Name = "CBBounce";
		resources.ApplyResources(this.LblLastPrice, "LblLastPrice");
		this.LblLastPrice.ForeColor = System.Drawing.Color.Maroon;
		this.LblLastPrice.Name = "LblLastPrice";
		resources.ApplyResources(this.groupBox4, "groupBox4");
		this.groupBox4.Controls.Add(this.BtnSuppliersSearch);
		this.groupBox4.Controls.Add(this.BtnNewSuppliers);
		this.groupBox4.Controls.Add(this.LblSup);
		this.groupBox4.Controls.Add(this.label18);
		this.groupBox4.Controls.Add(this.label37);
		this.groupBox4.Controls.Add(this.label38);
		this.groupBox4.Controls.Add(this.label31);
		this.groupBox4.Controls.Add(this.label39);
		this.groupBox4.Controls.Add(this.LblLastPrice);
		this.groupBox4.Controls.Add(this.label42);
		this.groupBox4.Controls.Add(this.TxtPhone);
		this.groupBox4.Controls.Add(this.label43);
		this.groupBox4.Controls.Add(this.CBBounce);
		this.groupBox4.Controls.Add(this.TxtCharge);
		this.groupBox4.Controls.Add(this.TxtAdditionalNum);
		this.groupBox4.Controls.Add(this.TxtNumVat);
		this.groupBox4.Controls.Add(this.TxtBuildingNum);
		this.groupBox4.Controls.Add(this.TxtPostalCode);
		this.groupBox4.Controls.Add(this.TxtAddress);
		this.groupBox4.Controls.Add(this.TxtSuppliers);
		this.groupBox4.Controls.Add(this.label41);
		this.groupBox4.Controls.Add(this.label46);
		this.groupBox4.Controls.Add(this.label45);
		this.groupBox4.Controls.Add(this.label47);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.TabStop = false;
		resources.ApplyResources(this.BtnSuppliersSearch, "BtnSuppliersSearch");
		this.BtnSuppliersSearch.Name = "BtnSuppliersSearch";
		this.BtnSuppliersSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnSuppliersSearch.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSuppliersSearch.Properties.Appearance.Font");
		this.BtnSuppliersSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnSuppliersSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnSuppliersSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnSuppliersSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnSuppliersSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnSuppliersSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnSuppliersSearch.Properties.Buttons"))
		});
		this.BtnSuppliersSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnSuppliersSearch.Properties.NullText = resources.GetString("BtnSuppliersSearch.Properties.NullText");
		this.BtnSuppliersSearch.Tag = "بحث بالكود";
		this.BtnSuppliersSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnSuppliersSearch_ButtonClick);
		this.BtnSuppliersSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnSuppliersSearch_KeyDown);
		resources.ApplyResources(this.BtnNewSuppliers, "BtnNewSuppliers");
		this.BtnNewSuppliers.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNewSuppliers.Appearance.Font");
		this.BtnNewSuppliers.Appearance.Options.UseFont = true;
		this.BtnNewSuppliers.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewSuppliers.ImageOptions.Image");
		this.BtnNewSuppliers.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewSuppliers.Name = "BtnNewSuppliers";
		this.BtnNewSuppliers.Click += new System.EventHandler(BtnNewSuppliers_Click);
		resources.ApplyResources(this.label18, "label18");
		this.label18.Name = "label18";
		resources.ApplyResources(this.label37, "label37");
		this.label37.Name = "label37";
		resources.ApplyResources(this.label38, "label38");
		this.label38.Name = "label38";
		resources.ApplyResources(this.label39, "label39");
		this.label39.Name = "label39";
		resources.ApplyResources(this.label42, "label42");
		this.label42.Name = "label42";
		resources.ApplyResources(this.label43, "label43");
		this.label43.Name = "label43";
		resources.ApplyResources(this.TxtCharge, "TxtCharge");
		this.TxtCharge.Name = "TxtCharge";
		this.TxtCharge.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCharge.Properties.Appearance.Font");
		this.TxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCharge.Properties.ReadOnly = true;
		resources.ApplyResources(this.TxtAdditionalNum, "TxtAdditionalNum");
		this.TxtAdditionalNum.Name = "TxtAdditionalNum";
		this.TxtAdditionalNum.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAdditionalNum.Properties.Appearance.Font");
		this.TxtAdditionalNum.Properties.Appearance.Options.UseFont = true;
		this.TxtAdditionalNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAdditionalNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAdditionalNum.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtAdditionalNum_KeyDown);
		this.TxtAdditionalNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtAdditionalNum_KeyPress);
		resources.ApplyResources(this.TxtBuildingNum, "TxtBuildingNum");
		this.TxtBuildingNum.Name = "TxtBuildingNum";
		this.TxtBuildingNum.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBuildingNum.Properties.Appearance.Font");
		this.TxtBuildingNum.Properties.Appearance.Options.UseFont = true;
		this.TxtBuildingNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBuildingNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBuildingNum.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtBuildingNum_KeyDown);
		this.TxtBuildingNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtBuildingNum_KeyPress);
		resources.ApplyResources(this.TxtPostalCode, "TxtPostalCode");
		this.TxtPostalCode.Name = "TxtPostalCode";
		this.TxtPostalCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPostalCode.Properties.Appearance.Font");
		this.TxtPostalCode.Properties.Appearance.Options.UseFont = true;
		this.TxtPostalCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPostalCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPostalCode.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtPostalCode_KeyDown);
		this.TxtPostalCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPostalCode_KeyPress);
		resources.ApplyResources(this.TxtAddress, "TxtAddress");
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAddress.Properties.Appearance.Font");
		this.TxtAddress.Properties.Appearance.Options.UseFont = true;
		this.TxtAddress.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAddress.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.label41, "label41");
		this.label41.Name = "label41";
		resources.ApplyResources(this.label46, "label46");
		this.label46.Name = "label46";
		resources.ApplyResources(this.label45, "label45");
		this.label45.Name = "label45";
		resources.ApplyResources(this.label47, "label47");
		this.label47.Name = "label47";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.TxtTotals);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.TxtVat);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmPurchases";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmPurchases_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryVatCalcLastCost).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryVatCalc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnSuppliersSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAdditionalNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBuildingNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
