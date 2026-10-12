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
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmReservations;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Frms.Reports.Stores;
using Humanizer;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Orders;

public class FrmPriceOffer : XtraForm
{
	private DataTable set = new DataTable();

	private Class_PriceOffer POFR = new Class_PriceOffer();

	private Class_Orders ORD = new Class_Orders();

	private Class_Bounce BNC = new Class_Bounce();

	private Class_Contract Contract = new Class_Contract();

	private Class_Customer Cus = new Class_Customer();

	private Class_Items ITM = new Class_Items();

	private Class_Customer Cust = new Class_Customer();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private GTSdbEntities db = new GTSdbEntities();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private DataTable datatable = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	public string txtWordAr;

	public string txtWordEn;

	public string CodeItm;

	private static int IsShow;

	private static int PrintID;

	private int intType = 1;

	public bool ISEVENTDISC = true;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	private string fileNameAR = Application.StartupPath + "\\Layout\\PriceOfferAR.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\PriceOfferEN.xml";

	private decimal? CreditLimit = 0m;

	private int SN = 1;

	private int StorId = 0;

	private int ItmID = 0;

	private string ItmName = string.Empty;

	private string UnitName = string.Empty;

	public int UserSellPrice = LoginDetails.UserSellPrice;

	private DateTime selectedBookingDate;

	private DateTime selectedInstallationDate;

	private DateTime selectedJawDate;

	private int selectedHallsID;

	private IContainer components = null;

	private GridColumn gridColumn10;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private ToolStripMenuItem رقمالفاتورةToolStripMenuItem1;

	private GridColumn gridColumn18;

	private CardView cardView1;

	private GridColumn gridColumn11;

	private GridColumn gridColumn12;

	private GridColumn gridColumn13;

	private GridColumn gridColumn14;

	private GridColumn gridColumn15;

	private GridColumn gridColumn16;

	private GridColumn gridColumn17;

	private GroupBox groupBox2;

	private TextEdit TxtLocation;

	private Label label25;

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

	private SimpleButton BtnExit;

	private NumericEditBox TxtTotals;

	private Label label11;

	private NumericEditBox TxtTotalDiscount;

	private Label label12;

	private Label label1;

	private Label label2;

	private NumericEditBox TxtSellPrice2;

	private Label label3;

	private NumericEditBox TxtSellPrice1;

	private Label label4;

	private NumericEditBox TxtDiscount;

	private NumericEditBox TxtVatDisc;

	private Label label5;

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

	private GroupBox groupBox3;

	private GroupBox groupBox5;

	private Label label17;

	private System.Windows.Forms.ComboBox CBSaleTybe;

	private Label label48;

	private TextEdit TxtNote;

	private Label label31;

	private System.Windows.Forms.ComboBox Comp_Store;

	private System.Windows.Forms.ComboBox CBBounce;

	private TextEdit TxtNumNote;

	private Label label18;

	private Label label26;

	private ButtonEdit TxtCode;

	private System.Windows.Forms.ComboBox CBProject;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private ButtonEdit BtnOrderSearch;

	private DateEdit TxtDate;

	private Label LblCustLast;

	private Label label21;

	private System.Windows.Forms.ComboBox TxtType;

	private Label label23;

	private Label label20;

	private Label label24;

	private GroupBox groupBox4;

	private SimpleButton BtnNewCustomer;

	private Label label22;

	private Label label34;

	private Label label35;

	private Label label19;

	private Label label33;

	private Label label42;

	private Label label43;

	private TextEdit TxtAdditionalNum;

	private TextEdit TxtStreet;

	private TextEdit TxtDistrict;

	private TextEdit TxtCommercialRecord;

	private TextEdit TxtPostalCode;

	private TextEdit TxtCountry;

	private TextEdit TxtCity;

	private Label LBlSuppAccount;

	private Label LblSup;

	private Label label37;

	private Label label32;

	private Label label46;

	private TextEdit TxtPhone;

	private TextEdit TxtCharge;

	private TextEdit TxtSales;

	private ButtonEdit TxtSuppliers;

	private TextEdit TxtNumVat;

	private Label label40;

	private Label label7;

	private NumericEditBox TxtAllDiscount;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	public GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn shearch;

	private GridColumn gridColumn23;

	private GridColumn gridColumn19;

	private GridColumn gridColumn4;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private GridColumn gridColumn22;

	private GridColumn gridColumn5;

	private GridColumn gridColumn9;

	private GridColumn gridColumn24;

	private GridColumn gridColumn25;

	private GridColumn gridColumn26;

	private GridColumn gridColumn27;

	private GridColumn gridColumn28;

	private GridColumn gridColumn29;

	private GridColumn gridColumn30;

	private GridColumn gridColumn31;

	private GridColumn gridColumn32;

	private GridColumn gridColumn33;

	private GridColumn gridColumn34;

	private GridColumn gridColumn35;

	private GridColumn gridColumn36;

	private GridColumn gridColumn37;

	private GridColumn gridColumn38;

	private GridColumn gridColumn39;

	private GridColumn gridColumn40;

	private GridColumn gridColumn41;

	private GridColumn gridColumn42;

	private GridColumn gridColumn43;

	private GridColumn gridColumn44;

	private GridColumn gridColumn45;

	private GridColumn gridColumn46;

	private GridColumn gridColumn47;

	private ButtonEdit BtnCustomerSearch2;

	private Label label6;

	private Label label27;

	private Label label29;

	private Label label28;

	private Label label30;

	private Label label36;

	private Label label39;

	private Label label38;

	private Label label41;

	private Label label47;

	private Label label45;

	private Label label44;

	private Label label50;

	private Label label49;

	private Label label51;

	private NumericEditBox TxtDisprege;

	public FrmPriceOffer()
	{
		InitializeComponent();
		TxtDigital();
		SetInitialRow();
		BindGrid();
		BtnDefault();
		GroupPermission();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
		TxtDisprege.ReadOnly = false;
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "ItemCode"));
		string text2 = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "ItemName"));
		decimal num = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "Quantity"));
		if (text == string.Empty || text2 == string.Empty || num <= 0m)
		{
			Item_Items item_Items = POFR.GetItems(text).FirstOrDefault();
			if (item_Items != null)
			{
				e.Valid = false;
				if (text == string.Empty)
				{
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
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
		gridControl1.DataSource = datatable;
	}

	private void gridView1_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
	{
		e.ExceptionMode = ExceptionMode.NoAction;
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[0];
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
				SN = 0;
			}
		}
		catch (Exception)
		{
		}
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
			gridView1.FocusedColumn = gridView1.VisibleColumns[0];
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

	private void BtnNewCustomer_Click(object sender, EventArgs e)
	{
		new FrmCustomer().Show();
	}

	private void TxtSuppliers_ButtonClick(object sender, ButtonPressedEventArgs e)
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

	private void BtnCustomerSearch2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(BtnCustomerSearch2.Text))
		{
			SearchCustNum(Convert.ToInt32(BtnCustomerSearch2.Text), 1);
		}
	}

	private void TxtCode_ButtonClick_1(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchPriceOffer frmSearchPriceOffer = new FrmSearchPriceOffer();
		frmSearchPriceOffer.ShowDialog();
		if (frmSearchPriceOffer.CustCode > 0)
		{
			SearchOrdNum(frmSearchPriceOffer.CustCode);
			Tafqit();
		}
	}

	private void BtnOrderSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(BtnOrderSearch.Text))
		{
			SearchOrdNum(Convert.ToInt32(BtnOrderSearch.Text));
		}
	}

	private void BtnOrderSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SearchOrdNum(Convert.ToInt32(BtnOrderSearch.Text));
		}
	}

	private void BtnOrderSearch_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void BtnCustomerSearch2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && !string.IsNullOrWhiteSpace(BtnCustomerSearch2.Text))
		{
			SearchCustNum(Convert.ToInt32(BtnCustomerSearch2.Text), 1);
		}
	}

	private void BtnDefault()
	{
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 10);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 10);
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
		CBCostCenter.Visible = true;
		label26.Visible = true;
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		TxtDate.DateTime = DateTime.Now;
		TxtDate.Focus();
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
			row["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
			row["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
			row["TafqitID"] = get_Item_ByCodeALL_Result.UnitTafqitID;
			row["UnitNameTF"] = get_Item_ByCodeALL_Result.UnitTafqit;
			row["QuantityTF"] = 1;
			row["Quantity"] = 1;
			row["BounceItem"] = 0;
			row["Height"] = 1;
			row["Width"] = 1;
			row["Long"] = 1;
			row["Weight"] = 1;
			row["UnitNum"] = 0;
			row["StoreName"] = Comp_Store.GetItemText(Comp_Store.SelectedItem);
			row["StoreID"] = Comp_Store.SelectedValue;
			decimal num2 = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
			switch (Convert.ToInt32(CBSaleTybe.SelectedValue))
			{
			case 1:
				num2 = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall));
				break;
			case 2:
				num2 = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall2));
				break;
			case 3:
				num2 = FUNC_DecimalNum(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall3));
				break;
			}
			row["UnitPrice"] = num2;
			row["OrginalPrice"] = row["UnitPrice"];
			row["SumPrice"] = row["UnitPrice"];
			row["PercentDisc"] = 0;
			row["AmountDisc"] = 0;
			row["UnitPriceDisc"] = row["UnitPrice"];
			row["SumPriceDisc"] = row["UnitPriceDisc"];
			row["IsItemTax"] = get_Item_ByCodeALL_Result.Is_Tax;
			row["Tax_Value"] = get_Item_ByCodeALL_Result.Tax_Value;
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
					row["Total"] = FUNC_DecimalNum(num2);
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
				decimal num3 = Convert.ToDecimal(row["SumPrice"]);
				row["Net"] = row["SumPriceDisc"];
			}
			row["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost;
			row["SumAvergItem"] = row["AvergItem"];
			row["Profit"] = Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]);
			row["DeleteItem"] = 0;
			row["Done"] = 1;
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
				TxtDisprege.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtVat.Text = "0.000";
				TxtVatDisc.Text = "0.00";
				TxtPhone.Text = string.Empty;
				TxtNumNote.Text = string.Empty;
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
			Tafqit();
		}
		catch (Exception)
		{
		}
	}

	private void No_Discount_NoVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			decimal num2 = (Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["BounceItem"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
				row["ItemVat"] = 0;
				row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
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
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
				row["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]));
			}
		}
		TxtDiscount.Text = "0.00";
		TxtDisprege.Text = "0.00";
		TxtTotalDiscount.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num3);
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtNet.Text = TxtTotals.Text.ToString();
	}

	private void No_Discount_Vat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			decimal num2 = (Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["BounceItem"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
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
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
				row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
			}
			else
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
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
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
			row["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]));
		}
		TxtDiscount.Text = "0.00";
		TxtDisprege.Text = "0.00";
		TxtTotalDiscount.Text = "0.00";
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num4);
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		decimal num6 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num5.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = num6.ToString();
		TxtNet.Text = (num4 + num6).ToString();
	}

	private void No_Discount_PriceWithVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			decimal num2 = (Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["BounceItem"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			if (Convert.ToBoolean(row["IsItemTax"]))
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m));
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]));
			}
			else
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				row["ItemVat"] = 0;
			}
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
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
			row["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]));
		}
		TxtDiscount.Text = "0.00";
		TxtDisprege.Text = "0.00";
		TxtTotalDiscount.Text = "0.00";
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num4);
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num5.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = TxtVat.Text;
		TxtNet.Text = (num4 + num5).ToString();
	}

	private void DiscountPercent_NoVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			decimal num2 = (Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["BounceItem"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
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
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
				row["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]));
			}
		}
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num3);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			TxtDisprege.Text = PerNum.ToString();
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
			TxtDisprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num4 = Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
			decimal num5 = (Convert.ToDecimal(row2["Quantity"]) + Convert.ToDecimal(row2["BounceItem"])) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPrice"]) * num4);
				row2["ItemVat"] = 0;
				row2["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPrice"]));
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num4);
				row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["ItemVat"]));
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num5);
				row2["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) - Convert.ToDecimal(row2["SumAvergItem"]));
			}
		}
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	private void DiscountPercent_Vat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			decimal num2 = (Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["BounceItem"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
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
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
				row["ItemVatDisc"] = row["ItemVat"];
				row["SumPriceDisc"] = row["SumPrice"];
				row["Net"] = row["Total"];
			}
			else
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
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
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
			row["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]));
		}
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat.Text = Convert.ToString(num3);
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num4);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			TxtDisprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num3) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num3) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(TxtTotals.Text) + num3) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtDisprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num5 = Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
			decimal num6 = (Convert.ToDecimal(row2["Quantity"]) + Convert.ToDecimal(row2["BounceItem"])) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
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
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num6);
				row2["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) - Convert.ToDecimal(row2["SumAvergItem"]));
			}
		}
		decimal num7 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVatDisc.Text = Convert.ToString(num7);
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text)).ToString();
	}

	private void DiscountPercent_PriceWithVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
			decimal num2 = (Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["BounceItem"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Width"]) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Weight"]);
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
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
			row["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]));
		}
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat.Text = Convert.ToString(num3);
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num4);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			TxtDisprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num3) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num3) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(TxtTotals.Text) + num3) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtDisprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num5 = Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
			decimal num6 = (Convert.ToDecimal(row2["Quantity"]) + Convert.ToDecimal(row2["BounceItem"])) * Convert.ToDecimal(row2["Height"]) * Convert.ToDecimal(row2["Width"]) * Convert.ToDecimal(row2["Long"]) * Convert.ToDecimal(row2["Weight"]);
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				decimal num7 = Convert.ToDecimal(row2["Tax_Value"]) / 100m;
				decimal num8 = Convert.ToDecimal(row2["PercentDisc"]) / 100m;
				decimal num9 = Convert.ToDecimal(row2["OrginalPrice"]) / (1m + num7);
				decimal num10 = num9 * (1m - num8);
				row2["UnitPriceDisc"] = num10 - num10 * (PerNum / 100m);
				row2["SumPriceDisc"] = Convert.ToDecimal(row2["UnitPriceDisc"]) * num5;
				decimal num11 = Convert.ToDecimal(row2["UnitPriceDisc"]);
				decimal num12 = Convert.ToDecimal(row2["SumPriceDisc"]);
				if (Convert.ToBoolean(row2["IsItemTax"]))
				{
					row2["ItemVatDisc"] = Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m);
				}
				else
				{
					row2["ItemVatDisc"] = 0;
				}
				decimal num13 = Convert.ToDecimal(row2["ItemVatDisc"]);
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]));
				decimal num14 = Convert.ToDecimal(row2["Net"]);
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]));
				row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["ItemVatDisc"]));
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num6);
				row2["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) - Convert.ToDecimal(row2["SumAvergItem"]));
			}
		}
		decimal num15 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		decimal num16 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
		TxtVatDisc.Text = Convert.ToString(num15);
		TxtNet.Text = num16.ToString();
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
				decimal num2 = ((TxtChange == "Height") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Height"])));
				decimal num3 = ((TxtChange == "Width") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Width"])));
				decimal num4 = ((TxtChange == "Long") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Long"])));
				decimal weight = ((TxtChange == "Weight") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Weight"])));
				decimal bounceItem = ((TxtChange == "BounceItem") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["BounceItem"])));
				decimal num5 = ((TxtChange == "PercentDisc") ? FUNC_DecimalNum(num) : FUNC_DecimalNum(Convert.ToDecimal(row["PercentDisc"])));
				if (num5 > 100m)
				{
					break;
				}
				flag2 = TxtChange == "PercentDisc" || TxtChange == "AmountDisc";
				decimal amountDisc = ((TxtChange == "AmountDisc") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["AmountDisc"])));
				flag = TxtChange == "AmountDisc";
				decimal avergItem = ((TxtChange == "AvergItem") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"])));
				UpdateRowFactionAccount(quantity, unitPrice, num2, num3, num4, weight, bounceItem, flag2, flag, num5, amountDisc, avergItem);
			}
		}
	}

	public void UpdateRowFactionAccount(decimal Quantity, decimal UnitPrice, decimal Height, decimal Width, decimal Long, decimal Weight, decimal BounceItem, bool IsDisc, bool IsDiscAmount, decimal PercentDisc, decimal AmountDisc, decimal AvergItem)
	{
		decimal num = Quantity * Height * Width * Long * Weight;
		decimal num2 = (Quantity + BounceItem) * Height * Width * Long * Weight;
		decimal value = UnitPrice * num;
		foreach (DataRow row in datatable.Rows)
		{
			int num3 = Convert.ToInt32(row["SN"]);
			if (Convert.ToInt32(row["SN"]) != SN)
			{
				continue;
			}
			bool flag = Convert.ToBoolean(row["IsItemTax"]);
			row["Quantity"] = Quantity;
			row["BounceItem"] = BounceItem;
			row["Height"] = Height;
			row["Width"] = Width;
			row["Long"] = Long;
			row["Weight"] = Weight;
			row["UnitPrice"] = FUNC_DecimalNum(UnitPrice);
			row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
			row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
			row["AvergItem"] = AvergItem;
			if (!LoginDetails.IsVat)
			{
				row["SumPrice"] = UnitPrice * num;
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
				row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * (UnitPrice * num) / 100m);
			}
			else if (flag)
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num4 = Convert.ToDecimal(LoginDetails.PerVat) / 100m;
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["OrginalPrice"]) * num);
					row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) / (num4 + 1m));
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
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * num4);
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
					row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * (UnitPrice * num) / 100m);
				}
			}
			else
			{
				row["SumPrice"] = UnitPrice * num;
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
				row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * (UnitPrice * num) / 100m);
			}
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
			row["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]));
		}
	}

	public void UpdateItemRowByNet(CellValueChangedEventArgs e)
	{
		decimal num = Convert.ToDecimal(e.Value);
		foreach (DataRow row in datatable.Rows)
		{
			int num2 = Convert.ToInt32(row["SN"]);
			if (num2 == SN && row["ItemCode"].ToString() != string.Empty)
			{
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
					decimal num4 = num3 / Convert.ToDecimal(row["Quantity"]);
					row["Net"] = num;
					row["ItemVat"] = num - num3;
					row["SumPrice"] = num3;
					row["UnitPrice"] = num4;
					row["OrginalPrice"] = row["UnitPrice"];
					row["UnitPriceDisc"] = row["UnitPrice"];
					row["Total"] = num;
					row["SumPriceDisc"] = row["SumPrice"];
					row["ItemVat"] = num - Convert.ToDecimal(row["UnitPrice"]);
					row["ItemVatDisc"] = row["ItemVat"];
					row["AmountDisc"] = 0;
					row["PercentDisc"] = 0;
				}
				break;
			}
		}
	}

	private void Item_No_Vat(decimal? Unitprice, decimal AllQuatinty, DataRow dr)
	{
		dr["SumPrice"] = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty);
		dr["Net"] = dr["SumPrice"];
		dr["ItemVat"] = 0;
		dr["SumPriceDisc"] = dr["ItemVat"];
		dr["IsItemTax"] = 0;
		dr["Tax_Value"] = 0;
		if (Convert.ToDecimal(dr["PercentDisc"]) <= Convert.ToDecimal(100) && Convert.ToDecimal(dr["PercentDisc"]) > 0m)
		{
			dr["Total"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]) - Convert.ToDecimal(dr["SumPrice"]) * (Convert.ToDecimal(dr["PercentDisc"]) / 100m));
			dr["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["Total"]));
			dr["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPriceDisc"]) / AllQuatinty);
			dr["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]) * (Convert.ToDecimal(dr["PercentDisc"]) / 100m));
		}
		else if (dr["UnitPriceDisc"] != DBNull.Value && dr["SumPrice"] != DBNull.Value && Convert.ToDecimal(dr["UnitPriceDisc"]) <= Convert.ToDecimal(dr["SumPrice"]) && Convert.ToDecimal(dr["UnitPriceDisc"]) > 0m)
		{
			dr["Total"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]) - Convert.ToDecimal(dr["UnitPriceDisc"]));
			dr["SumPriceDisc"] = dr["Total"];
			dr["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPriceDisc"]) / AllQuatinty);
			dr["PercentDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["UnitPriceDisc"]) / Convert.ToDecimal(dr["SumPrice"]) * 100m);
		}
		else
		{
			dr["UnitPriceDisc"] = dr["UnitPrice"];
			dr["SumPriceDisc"] = dr["SumPrice"];
			dr["Total"] = dr["Net"];
			dr["PercentDisc"] = "0.00";
			dr["AmountDisc"] = "0.00";
			dr["ItemVatDisc"] = "0";
		}
	}

	private void Vat(decimal? Tax_Value, decimal? Unitprice, decimal AllQuatinty, DataRow dr)
	{
		dr["SumPrice"] = FUNC_DecimalNum(Unitprice * (decimal?)AllQuatinty);
		decimal value = Convert.ToDecimal(dr["SumPrice"]);
		decimal? num = Tax_Value / (decimal?)100;
		dr["ItemVat"] = FUNC_DecimalNum((decimal?)value * num);
		dr["Net"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]) + Convert.ToDecimal(dr["ItemVat"]));
		dr["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]) / AllQuatinty);
		dr["IsItemTax"] = 1;
		dr["Tax_Value"] = Tax_Value;
		if (Convert.ToDecimal(dr["PercentDisc"]) <= Convert.ToDecimal(dr["SumPrice"]) && Convert.ToDecimal(dr["PercentDisc"]) > 0m)
		{
			dr["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]) - Convert.ToDecimal(dr["SumPrice"]) * (Convert.ToDecimal(dr["PercentDisc"]) / 100m));
			value = Convert.ToDecimal(dr["SumPriceDisc"]);
			decimal? num2 = Tax_Value - (decimal?)1;
			dr["ItemVatDisc"] = FUNC_DecimalNum((decimal?)value * num2);
			dr["Total"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPriceDisc"]) + Convert.ToDecimal(dr["ItemVatDisc"]));
			dr["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPriceDisc"]) / AllQuatinty);
		}
		else if (Convert.ToDecimal(dr["AmountDisc"]) <= Convert.ToDecimal(dr["SumPrice"]) && Convert.ToDecimal(dr["AmountDisc"]) > 0m)
		{
			dr["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]) - Convert.ToDecimal(dr["AmountDisc"]));
			value = Convert.ToDecimal(dr["SumPriceDisc"]);
			decimal? num3 = Tax_Value - (decimal?)1;
			dr["ItemVatDisc"] = FUNC_DecimalNum((decimal?)value * num3);
			dr["Total"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPriceDisc"]) + Convert.ToDecimal(dr["ItemVatDisc"]));
			dr["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPriceDisc"]) / AllQuatinty);
			dr["PercentDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["AmountDisc"]) / (Convert.ToDecimal(dr["Net"]) - Convert.ToDecimal(dr["Total"])));
		}
		else
		{
			dr["Total"] = FUNC_DecimalNum(Convert.ToDecimal(dr["Net"]));
			dr["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]));
			dr["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["ItemVat"]));
			dr["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["UnitPrice"]));
			dr["PercentDisc"] = "0.00";
			dr["AmountDisc"] = "0.00";
		}
	}

	private void VatWithTotal(decimal? Tax_Value, decimal? UnitPrice, decimal AllQuatinty, DataRow dr)
	{
		dr["SumPrice"] = FUNC_DecimalNum(UnitPrice * (decimal?)AllQuatinty);
		dr["Net"] = FUNC_DecimalNum((decimal?)Convert.ToDecimal(dr["SumPrice"]) * Tax_Value);
		dr["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(dr["Net"]) - Convert.ToDecimal(dr["SumPrice"]));
		decimal num = FUNC_DecimalNum(UnitPrice * (decimal?)AllQuatinty);
		decimal num2 = FUNC_DecimalNum((decimal?)Convert.ToDecimal(dr["SumPrice"]) * Tax_Value);
		decimal num3 = FUNC_DecimalNum(Convert.ToDecimal(dr["Net"]) - Convert.ToDecimal(dr["SumPrice"]));
		dr["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]) / AllQuatinty);
		dr["IsItemTax"] = 1;
		dr["Tax_Value"] = (Tax_Value - (decimal?)1) * (decimal?)100;
		if (Convert.ToDecimal(dr["PercentDisc"]) <= Convert.ToDecimal(dr["SumPrice"]) && Convert.ToDecimal(dr["PercentDisc"]) > 0m)
		{
			dr["Total"] = FUNC_DecimalNum(Convert.ToDecimal(dr["Net"]) - Convert.ToDecimal(dr["Net"]) * (Convert.ToDecimal(dr["PercentDisc"]) / 100m));
			dr["SumPriceDisc"] = FUNC_DecimalNum((decimal?)Convert.ToDecimal(dr["Total"]) / Tax_Value);
			dr["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["Total"]) - Convert.ToDecimal(dr["SumPriceDisc"]));
			dr["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPriceDisc"]) / AllQuatinty);
			dr["AmountDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["Net"]) - Convert.ToDecimal(dr["Total"]));
		}
		else if (Convert.ToDecimal(dr["AmountDisc"]) <= Convert.ToDecimal(dr["SumPrice"]) && Convert.ToDecimal(dr["AmountDisc"]) > 0m)
		{
			dr["Total"] = FUNC_DecimalNum(Convert.ToDecimal(dr["Net"]) - Convert.ToDecimal(dr["AmountDisc"]));
			dr["SumPriceDisc"] = FUNC_DecimalNum((decimal?)Convert.ToDecimal(dr["Total"]) / Tax_Value);
			dr["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["Total"]) - Convert.ToDecimal(dr["SumPriceDisc"]));
			dr["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPriceDisc"]) / AllQuatinty);
			dr["PercentDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["AmountDisc"]) / (Convert.ToDecimal(dr["Net"]) - Convert.ToDecimal(dr["Total"])));
		}
		else
		{
			dr["Total"] = FUNC_DecimalNum(Convert.ToDecimal(dr["Net"]));
			dr["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["SumPrice"]));
			dr["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["ItemVat"]));
			dr["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dr["UnitPrice"]));
			dr["PercentDisc"] = "0.00";
			dr["AmountDisc"] = "0.00";
		}
	}

	public void BindGrid()
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
			}
			TxtType.DataSource = ORD.GetAccountType();
			TxtType.DisplayMember = "Name";
			TxtType.ValueMember = "ID";
			CBBounce.DataSource = BNC.GetAll();
			CBBounce.DisplayMember = "Name";
			CBBounce.ValueMember = "ID";
			Comp_Store.DataSource = Contract.Get_Stores();
			Comp_Store.DisplayMember = "Store_Name";
			Comp_Store.ValueMember = "ID";
			Comp_Store.SelectedValue = LoginDetails.StoreID;
			CBSaleTybe.DataSource = ORD.GetUser_SellPrice();
			CBSaleTybe.DisplayMember = "Name";
			CBSaleTybe.ValueMember = "ID";
			CBSaleTybe.SelectedValue = 1;
			CBSaleTybe.DataSource = ORD.GetUser_SellPrice();
			CBSaleTybe.DisplayMember = "Name";
			CBSaleTybe.ValueMember = "ID";
			CBSaleTybe.SelectedValue = 1;
			TxtSuppliers.Text = Cust.GetFirstCustomer();
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
		dataRow["TafqitID"] = 1;
		dataRow["UnitNameTF"] = string.Empty;
		dataRow["QuantityTF"] = 1;
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
		dataRow["Profit"] = 0;
		dataRow["IsItemTax"] = false;
		dataRow["Tax_Value"] = 0;
		dataRow["DeleteItem"] = 0;
		dataRow["Done"] = 0;
		datatable.Rows.Add(dataRow);
		ArrangeDatatable();
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
		datatable.Columns.Add("ItemUnitID", typeof(int));
		datatable.Columns.Add("UnitName", typeof(string));
		datatable.Columns.Add("TafqitID", typeof(int));
		datatable.Columns.Add("UnitNameTF", typeof(string));
		datatable.Columns.Add("QuantityTF", typeof(decimal));
		datatable.Columns.Add("Quantity", typeof(decimal));
		datatable.Columns.Add("BounceItem", typeof(decimal));
		datatable.Columns.Add("Height", typeof(decimal));
		datatable.Columns.Add("Width", typeof(decimal));
		datatable.Columns.Add("Long", typeof(decimal));
		datatable.Columns.Add("Weight", typeof(decimal));
		datatable.Columns.Add("UnitNum", typeof(decimal));
		datatable.Columns.Add("StoreName", typeof(string));
		datatable.Columns.Add("StoreID", typeof(int));
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
		datatable.Columns.Add("Profit", typeof(decimal));
		datatable.Columns.Add("IsItemTax", typeof(bool));
		datatable.Columns.Add("Tax_Value", typeof(decimal));
		datatable.Columns.Add("DeleteItem", typeof(int));
		datatable.Columns.Add("SN", typeof(int));
		datatable.Columns.Add("Done", typeof(int));
		datatable.Columns.Add("TaxTobacco", typeof(decimal));
		datatable.Columns.Add("TotalWithTaxTobacco", typeof(decimal));
		datatable.Columns.Add("TaxTobaccoDis", typeof(decimal));
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
		dtCurrentTable.Columns.Add("TaxTobacco", typeof(decimal));
		dtCurrentTable.Columns.Add("TotalWithTaxTobacco", typeof(decimal));
		dtCurrentTable.Columns.Add("TaxTobaccoDis", typeof(decimal));
		set.Columns.Add("ItemID", typeof(int));
		set.Columns.Add("ItemUnitID", typeof(int));
		set.Columns.Add("TafqitId", typeof(int));
		set.Columns.Add("ItemNote", typeof(string));
		set.Columns.Add("ItemCode", typeof(string));
		set.Columns.Add("ItemName", typeof(string));
		set.Columns.Add("UnitType", typeof(string));
		set.Columns.Add("UnitID", typeof(int));
		set.Columns.Add("StoreName", typeof(string));
		set.Columns.Add("StoreID", typeof(int));
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
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridControl1.ProcessGridKey += gridControl1_ProcessGridKey;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		gridView1.CustomDrawFooterCell += gridView1_CustomDrawFooterCell;
		gridView1.Columns["AvergItem"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, string.Empty, "{0:0.###}", "0")
		});
		gridView1.Columns["SumAvergItem"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, string.Empty, "{0:0.###}", "0")
		});
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

	public int IsItemPriceZero()
	{
		int result = 1;
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["ItemCode"]);
			if (Convert.ToDecimal(row["UnitPrice"]) <= 0m && text != string.Empty)
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
			if (text == string.Empty)
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
		CBSaleTybe.SelectedValue = 1;
		TxtSales.Text = string.Empty;
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtCode.Text = string.Empty;
		datatable.Rows.Clear();
		dtCurrentTable.Rows.Clear();
		gridControl1.DataSource = datatable;
		TxtSuppliers.Text = string.Empty;
		TxtNumVat.Text = string.Empty;
		TxtCity.Text = string.Empty;
		TxtStreet.Text = string.Empty;
		TxtDistrict.Text = string.Empty;
		TxtAdditionalNum.Text = string.Empty;
		TxtCommercialRecord.Text = string.Empty;
		TxtPostalCode.Text = string.Empty;
		TxtType.Text = string.Empty;
		TxtNote.Text = string.Empty;
		TxtDiscount.Text = string.Empty;
		TxtCountry.Text = string.Empty;
		TxtVat.Text = "0.00";
		TxtDisprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtType.Text = "نقدى";
		TxtSuppliers.Text = Cust.GetFirstCustomer();
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtPhone.Text = string.Empty;
		TxtNumNote.Text = string.Empty;
		TxtVatDisc.Text = "0.00";
		TxtTafqit.Text = string.Empty;
		BtnCustomerSearch2.Text = string.Empty;
		TxtTotals.Text = "0.00";
		TxtTotalDiscount.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtLocation.Text = string.Empty;
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
		BtnCustomerSearch2.Focus();
		Comp_Store.SelectedValue = LoginDetails.StoreID;
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
		BtnOrderSearch.Text = string.Empty;
		IsEnableTrue();
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtSuppliers.Focus();
		BindGrid();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 10))
		{
			int num = 0;
			if (datatable.Rows.Count > 1)
			{
				if (TxtSuppliers.Text != string.Empty)
				{
					if (TxtType.Text != string.Empty)
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
							Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
							if (CBCostCenter.SelectedValue == null)
							{
								CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
							}
							FiterDatatableItemsUpdate();
							int projectId = ((!int.TryParse(CBProject.SelectedValue?.ToString(), out var result)) ? 1 : result);
							num = POFR.InsertPurch(out var Code, Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, TxtDate.DateTime, Convert.ToInt32(CBCostCenter.SelectedValue), string.Empty, TxtNote.Text, TxtNumNote.Text, Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDisprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), 0m, Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, Convert.ToInt32(CBSaleTybe.SelectedValue), Math.Round(Convert.ToDecimal(TxtAvergCost.Text), DecimalNum), Convert.ToInt32(TxtType.SelectedValue), projectId);
							POFR.InsertItems(datatable, num);
							if (LoginDetails.Active_Virgin == 9)
							{
								POFR.InsertRes(Code, selectedHallsID, selectedBookingDate, selectedInstallationDate, selectedJawDate);
							}
							gridControl1.DataSource = datatable;
							gridView1.OptionsBehavior.Editable = false;
							if (num > 0)
							{
								PrintID = num;
								BtnSave.Enabled = false;
								BtnEdit.Enabled = true;
								BtnDelete.Enabled = true;
								TxtCode.Text = num.ToString();
								IsEnableFalse();
								switch (MessageBox.Show("تم حفظ بيانات فاتورة عرض السعر بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
								{
								case DialogResult.Yes:
									Print();
									break;
								}
							}
							else
							{
								MessageBox.Show("تم حفظ بيانات فاتورة عرض السعر رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
					MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة عرض السعر بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 10))
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
			if (!(TxtSuppliers.Text != string.Empty))
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
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
				if (CBCostCenter.SelectedValue == null)
				{
					CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
				}
				FiterDatatableItemsUpdate();
				int projectId = ((!int.TryParse(CBProject.SelectedValue?.ToString(), out var result)) ? 1 : result);
				num = POFR.UpdatePurch(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, TxtDate.DateTime, Convert.ToInt32(CBCostCenter.SelectedValue), string.Empty, TxtNote.Text, TxtNumNote.Text, Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDisprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), 0m, Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, Convert.ToInt32(CBSaleTybe.SelectedValue), Math.Round(Convert.ToDecimal(TxtAvergCost.Text), DecimalNum), Convert.ToInt32(TxtType.SelectedValue), projectId);
				decimal num2 = Math.Round((Convert.ToDecimal(TxtDisprege.Text) * Convert.ToDecimal(TxtVat.Text) / 100m - Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)) * -1m, DecimalNum);
				decimal num3 = Math.Round((Convert.ToDecimal(TxtDisprege.Text) * Convert.ToDecimal(TxtTotals.Text) / 100m - Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum)) * -1m, DecimalNum);
				POFR.deleteItemsByPurcahesId(num);
				POFR.InsertItems(datatable, num);
				if (LoginDetails.Active_Virgin == 9)
				{
					POFR.UpdateRes(Convert.ToInt32(TxtCode.Text), selectedHallsID, selectedBookingDate, selectedInstallationDate, selectedJawDate);
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
					switch (MessageBox.Show("تم تعديل فاتورة عرض السعر بنجاح \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						Print();
						break;
					}
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات فاتورة عرض السعر رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 10))
		{
			int num = 0;
			if (TxtCode.Text != string.Empty)
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة عرض السعر رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = POFR.Delete(Convert.ToInt32(TxtCode.Text));
					if (LoginDetails.Active_Virgin == 9)
					{
						POFR.DeleteRes(Convert.ToInt32(TxtCode.Text));
					}
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات فاتورة عرض السعر رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
						MessageBox.Show("لا يمكن حذف بيانات فاتورة عرض السعر رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة عرض السعر بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 10))
		{
			if (TxtCode.Text != string.Empty)
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_PriceOffer", sqlConnection);
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
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintPriceOffer.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintPriceOffer.mrt");
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
					stiReport["Tafqit"] = TxtTafqit.Text;
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
		TxtSales.ReadOnly = false;
		TxtCity.ReadOnly = false;
		TxtCountry.ReadOnly = false;
		TxtAdditionalNum.ReadOnly = false;
		TxtPostalCode.ReadOnly = false;
		TxtCommercialRecord.ReadOnly = false;
		TxtDistrict.ReadOnly = false;
		TxtStreet.ReadOnly = false;
		TxtAllDiscount.ReadOnly = false;
		TxtDisprege.ReadOnly = false;
		BtnCustomerSearch2.ReadOnly = false;
		CBCostCenter.Enabled = true;
		CBSaleTybe.Enabled = true;
		CBProject.Enabled = true;
		CBBounce.Enabled = true;
		Comp_Store.Enabled = true;
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = true;
		TxtType.Enabled = false;
		Comp_Store.Enabled = false;
		CBSaleTybe.Enabled = false;
		CBBounce.Enabled = false;
		TxtDate.ReadOnly = true;
		TxtSuppliers.ReadOnly = true;
		TxtNumVat.ReadOnly = true;
		TxtPhone.ReadOnly = true;
		TxtNumNote.ReadOnly = true;
		TxtSales.ReadOnly = true;
		TxtCity.ReadOnly = true;
		TxtCountry.ReadOnly = true;
		TxtAdditionalNum.ReadOnly = true;
		TxtPostalCode.ReadOnly = true;
		TxtCommercialRecord.ReadOnly = true;
		TxtDistrict.ReadOnly = true;
		TxtStreet.ReadOnly = true;
		TxtNumNote.ReadOnly = true;
		TxtNote.ReadOnly = true;
		TxtDiscount.ReadOnly = true;
		TxtAllDiscount.ReadOnly = true;
		TxtDisprege.ReadOnly = true;
		BtnCustomerSearch2.ReadOnly = true;
		CBCostCenter.Enabled = false;
		CBProject.Enabled = false;
		TxtDiscount.BackColor = Color.White;
		TxtSales.BackColor = Color.White;
		TxtCity.BackColor = Color.White;
		TxtCommercialRecord.BackColor = Color.White;
		TxtAdditionalNum.BackColor = Color.White;
		TxtPostalCode.BackColor = Color.White;
		TxtDistrict.BackColor = Color.White;
		TxtStreet.BackColor = Color.White;
		TxtCountry.BackColor = Color.White;
		TxtNote.BackColor = Color.White;
		TxtType.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
		TxtSuppliers.BackColor = Color.White;
		TxtNumVat.BackColor = Color.White;
		TxtPhone.BackColor = Color.White;
		TxtNumNote.BackColor = Color.White;
		CBBounce.BackColor = Color.White;
		Comp_Store.BackColor = Color.White;
		BtnCustomerSearch2.BackColor = Color.White;
	}

	public void openItmCard()
	{
		if (gridControl1.Focus())
		{
			if (CodeItm != string.Empty)
			{
				LoginDetails.ItemSearchID = POFR.GetItmID(CodeItm);
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
					if (row["SN"].ToString() == Convert.ToString(num))
					{
						if (row["ItemCode"].ToString() != string.Empty)
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
					dataRow2["UnitNum"] = 0;
					if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
					{
						dataRow2["UnitNameTF"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitSmallID);
						dataRow2["QuantityTF"] = FUNC_DecimalNum(dataRow2["Quantity"].ToString());
						dataRow2["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
					}
					if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
					{
						dataRow2["UnitNameTF"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitMediumID);
						DataRow dataRow3 = dataRow2;
						decimal value = 1;
						decimal? convertMediumUnit = get_Item_ByCodeALL_Result.ConvertMediumUnit;
						dataRow3["QuantityTF"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow2["Quantity"]));
						dataRow2["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
					}
					if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
					{
						dataRow2["UnitNameTF"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitLargeID);
						DataRow dataRow4 = dataRow2;
						decimal value = 1;
						decimal? convertMediumUnit = get_Item_ByCodeALL_Result.ConvertBigUnit;
						dataRow4["QuantityTF"] = FUNC_DecimalNum((decimal?)value / convertMediumUnit * (decimal?)Convert.ToDecimal(dataRow2["Quantity"]));
						dataRow2["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
					}
					dataRow2["Done"] = 1;
					dataRow2["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
					dataRow2["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost;
					dataRow2["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost;
					dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
					dataRow2["SumPrice"] = dataRow2["UnitPrice"];
					int num2 = Convert.ToInt32(CBSaleTybe.SelectedValue);
					if (!LoginDetails.IsVat)
					{
						switch (num2)
						{
						case 1:
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
							dataRow2["Total"] = dataRow2["UnitPrice"];
							dataRow2["OrginalPrice"] = dataRow2["Total"];
							break;
						case 2:
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
							dataRow2["Total"] = dataRow2["UnitPrice"];
							dataRow2["OrginalPrice"] = dataRow2["Total"];
							break;
						case 3:
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall3);
							dataRow2["Total"] = dataRow2["UnitPrice"];
							dataRow2["OrginalPrice"] = dataRow2["Total"];
							break;
						default:
							dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
							dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
							dataRow2["OrginalPrice"] = dataRow2["Total"];
							break;
						}
						Item_No_Vat(Convert.ToDecimal(dataRow2["UnitPrice"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2);
					}
					else
					{
						bool flag = Convert.ToBoolean(get_Item_ByCodeALL_Result.Is_Tax);
						dataRow2["IsItemTax"] = flag;
						if (flag)
						{
							if (LoginDetails.ItemTotalWithVat)
							{
								decimal value2 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
								switch (num2)
								{
								case 1:
									dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall / (decimal?)value2);
									dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
									dataRow2["OrginalPrice"] = dataRow2["Total"];
									break;
								case 2:
									dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
									dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall2), DecimalNum);
									dataRow2["OrginalPrice"] = dataRow2["Total"];
									break;
								case 3:
									dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall3);
									dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall3), DecimalNum);
									dataRow2["OrginalPrice"] = dataRow2["Total"];
									break;
								default:
									dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
									dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
									dataRow2["OrginalPrice"] = dataRow2["Total"];
									break;
								}
								VatWithTotal(value2, Convert.ToDecimal(dataRow2["UnitPrice"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2);
							}
							else
							{
								switch (num2)
								{
								case 1:
									dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
									dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
									dataRow2["OrginalPrice"] = dataRow2["Total"];
									break;
								case 2:
									dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
									dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall2), DecimalNum);
									dataRow2["OrginalPrice"] = dataRow2["Total"];
									break;
								case 3:
									dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall3);
									dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall3), DecimalNum);
									dataRow2["OrginalPrice"] = dataRow2["Total"];
									break;
								default:
									dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
									dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
									dataRow2["OrginalPrice"] = dataRow2["Total"];
									break;
								}
								Vat(get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(dataRow2["UnitPrice"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2);
							}
						}
						else
						{
							switch (num2)
							{
							case 1:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
								dataRow2["Total"] = dataRow2["UnitPrice"];
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							case 2:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
								dataRow2["Total"] = dataRow2["UnitPrice"];
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							case 3:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall3);
								dataRow2["Total"] = dataRow2["UnitPrice"];
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							default:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
								dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							}
							Item_No_Vat(Convert.ToDecimal(dataRow2["UnitPrice"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2);
						}
					}
					if (get_Item_ByCodeALL_Result.MediumBarCode1 == text || get_Item_ByCodeALL_Result.MediumBarCode2 == text || get_Item_ByCodeALL_Result.MediumBarCode3 == text)
					{
						dataRow2["UnitType"] = "المتوسطة";
						dataRow2["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitMediumID;
						dataRow2["UnitName"] = get_Item_ByCodeALL_Result.UnitMedium;
						dataRow2["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit;
						dataRow2["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit;
						dataRow2["UnitPrice"] = get_Item_ByCodeALL_Result.SellPriceMedium;
						dataRow2["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							switch (num2)
							{
							case 1:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium / (decimal?)value3);
								dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							case 2:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium2);
								dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium2), DecimalNum);
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							case 3:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium3);
								dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium3), DecimalNum);
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							default:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
								dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							}
							VatWithTotal(value3, Convert.ToDecimal(dataRow2["UnitPrice"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2);
						}
						else
						{
							switch (num2)
							{
							case 1:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
								dataRow2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
								break;
							case 2:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium2);
								dataRow2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium2), DecimalNum);
								break;
							case 3:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium3);
								dataRow2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium3), DecimalNum);
								break;
							default:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
								dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							}
							Vat(get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(dataRow2["UnitPrice"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2);
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
							decimal value4 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							switch (num2)
							{
							case 1:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge / (decimal?)value4);
								dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							case 2:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge2);
								dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge2), DecimalNum);
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							case 3:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge3);
								dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge3), DecimalNum);
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							default:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge / (decimal?)value4);
								dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							}
							VatWithTotal(value4, Convert.ToDecimal(dataRow2["UnitPrice"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2);
						}
						else
						{
							switch (num2)
							{
							case 1:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
								dataRow2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
								break;
							case 2:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge2);
								dataRow2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge2), DecimalNum);
								break;
							case 3:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge3);
								dataRow2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge3), DecimalNum);
								break;
							default:
								dataRow2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
								dataRow2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
								dataRow2["OrginalPrice"] = dataRow2["Total"];
								break;
							}
							Vat(get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(dataRow2["UnitPrice"]), Convert.ToDecimal(dataRow2["Quantity"]), dataRow2);
						}
					}
					dataRow2["StoreName"] = Comp_Store.Text;
					dataRow2["StoreID"] = Comp_Store.SelectedValue;
					dataRow2["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow2["SumPriceDisc"]) - Convert.ToDecimal(dataRow2["SumAvergItem"]));
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

	public int PermissionSellPrice()
	{
		int result = 1;
		foreach (DataRow row in datatable.Rows)
		{
			string item_code = row["ItemCode"].ToString();
			if (row["UnitType"].ToString() == "الصغرى")
			{
				decimal sellPermission = POFR.GetSellPermission(item_code, UserSellPrice, "الصغرى");
				if (UserSellPrice != 5 && Convert.ToDecimal(row["UnitPriceDisc"]) < sellPermission)
				{
					MessageBox.Show("عفوا\u064b غير مصرح لك بخصم اكثر من السعر المحدد لك صلاحيته", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					result = 0;
					TxtDiscount.Focus();
					break;
				}
			}
			else if (row["UnitType"].ToString() == "المتوسطة")
			{
				decimal sellPermission2 = POFR.GetSellPermission(item_code, UserSellPrice, "المتوسطة");
				if (UserSellPrice != 5 && Convert.ToDecimal(row["UnitPriceDisc"]) < sellPermission2)
				{
					MessageBox.Show("عفوا\u064b غير مصرح لك بخصم اكثر من السعر المحدد لك صلاحيته", "صلاحيات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					result = 0;
					TxtDiscount.Focus();
					break;
				}
			}
			else if (row["UnitType"].ToString() == "الكبرى")
			{
				decimal sellPermission3 = POFR.GetSellPermission(item_code, UserSellPrice, "الكبرى");
				if (UserSellPrice != 5 && Convert.ToDecimal(row["UnitPriceDisc"]) < sellPermission3)
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
		TxtVatDisc.DecimalDigits = DecimalNum;
		TxtAllDiscount.DecimalDigits = DecimalNum;
		TxtTotals.DecimalDigits = DecimalNum;
		TxtTotalDiscount.DecimalDigits = DecimalNum;
		TxtNet.DecimalDigits = DecimalNum;
		TxtDisprege.DecimalDigits = DecimalNum;
		TxtSellPrice1.DecimalDigits = DecimalNum;
		TxtSellPrice2.DecimalDigits = DecimalNum;
		TxtSellPrice3.DecimalDigits = DecimalNum;
		TxtProfit.DecimalDigits = DecimalNum;
		TxtAvergCost.DecimalDigits = DecimalNum;
		TxtLastCost.DecimalDigits = DecimalNum;
		TxtQuntity.DecimalDigits = DecimalNumQty;
		TxtAllQuantity.DecimalDigits = DecimalNumQty;
	}

	public void UpdateUnitPriceInGrid(CellValueChangedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["SN"].ToString() == Convert.ToString(num))
			{
				row["UnitPrice"] = Convert.ToDecimal(e.Value);
				UpdateItemRow("UnitPrice", e);
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
				gridControl1.DataSource = datatable;
				break;
			}
		}
	}

	private void SearchOrdNum(int PriceShowSearch)
	{
		try
		{
			if (PriceShowSearch <= 0)
			{
				return;
			}
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
			Order_PriceOffer order_PriceOffer = POFR.GetPurchases(PriceShowSearch).FirstOrDefault();
			datatable.Rows.Clear();
			List<Order_PriceOfferDetails> itemsPurchesByID = POFR.GetItemsPurchesByID(PriceShowSearch);
			int num = 1;
			if (order_PriceOffer != null)
			{
				foreach (Order_PriceOfferDetails item in itemsPurchesByID)
				{
					DataRow dataRow = datatable.NewRow();
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = POFR.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow["ItemID"] = item_Items?.ItemId ?? 0;
					dataRow["ItemUnitID"] = item.ItemUnitID;
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["ItemCode"] = item_Items?.Item_code ?? string.Empty;
					dataRow["ItemName"] = item_Items?.item_Name ?? string.Empty;
					dataRow["BounceItem"] = FUNC_DecimalNum(item.Bounce);
					dataRow["Height"] = FUNC_DecimalNum(item.Long);
					dataRow["Width"] = FUNC_DecimalNum(item.width);
					dataRow["Long"] = FUNC_DecimalNum(item.Height);
					dataRow["Weight"] = (Convert.IsDBNull(item.Weight) ? 1m : FUNC_DecimalNum(item.Weight));
					dataRow["UnitNameTF"] = gTSdbEntities.Item_Unit.FirstOrDefault((Item_Unit x) => (int?)x.ID == item.TafqitUnitId)?.Name ?? string.Empty;
					dataRow["QuantityTF"] = FUNC_DecimalNum(item.TafqitQunitity);
					dataRow["TafqitId"] = (item_Items?.UnitTafqitID).GetValueOrDefault();
					dataRow["AmountDisc"] = FUNC_DecimalNum(item.Amount_Discount);
					dataRow["PercentDisc"] = FUNC_DecimalNum(item.Per_Discount);
					dataRow["SumPriceDisc"] = FUNC_DecimalNum(item.NetTotalPrice);
					dataRow["Net"] = FUNC_DecimalNum(item.NetTotalPrice);
					dataRow["ItemUnitID"] = item.ItemUnitID;
					dataRow["UnitNum"] = item.UnitNumber;
					decimal value = FUNC_DecimalNum((item.Quantity + item.Bounce) * (item.Long * item.width * item.Height));
					decimal num2 = FUNC_DecimalNum(item.Quantity * (item.Long * item.width * item.Height));
					dataRow["UnitType"] = item.ItemUnitType;
					dataRow["UnitName"] = ORD.GetUnitNameByID(Convert.ToInt32(item.ItemUnitID));
					dataRow["StoreID"] = item.StoreID;
					dataRow["StoreName"] = (from x in gTSdbEntities.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault() ?? string.Empty;
					dataRow["Quantity"] = FUNC_DecimalNumQty(item.Quantity);
					dataRow["AvergItem"] = FUNC_DecimalNum(item.LastCost);
					dataRow["SumAvergItem"] = FUNC_DecimalNum(item.LastCost * (decimal?)value);
					dataRow["UnitPrice"] = FUNC_DecimalNum(item.UnitPrice);
					dataRow["UnitPriceDisc"] = FUNC_DecimalNum((item?.NetUnitPrice).GetValueOrDefault());
					decimal? vAT;
					if (LoginDetails.ItemTotalWithVat)
					{
						vAT = item.VAT;
						if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
						{
							decimal value2 = FUNC_DecimalNum(item_Items.Tax_Value / (decimal?)100 + (decimal?)1);
							dataRow["OrginalPrice"] = FUNC_DecimalNum(item.UnitPrice * (decimal?)value2);
						}
					}
					else
					{
						dataRow["OrginalPrice"] = FUNC_DecimalNum(item.UnitPrice);
					}
					vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["ItemVat"] = FUNC_DecimalNum(item.VAT);
						dataRow["Total"] = FUNC_DecimalNum(item.TotalPrice + item.VAT);
						dataRow["IsItemTax"] = 1;
						dataRow["Tax_Value"] = FUNC_DecimalNum(item_Items.Tax_Value);
						dataRow["ItemVatDisc"] = FUNC_DecimalNum(item.VAT_Discount);
					}
					else
					{
						dataRow["IsItemTax"] = 0;
						dataRow["ItemVatDisc"] = 0;
						dataRow["ItemVat"] = FUNC_DecimalNum(item.VAT);
						dataRow["Total"] = FUNC_DecimalNum(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT));
					}
					if (LoginDetails.ItemTotalWithVat)
					{
						dataRow["SumPrice"] = FUNC_DecimalNum(item.TotalPrice - item.VAT);
						dataRow["Total"] = FUNC_DecimalNum(item.TotalPrice);
					}
					else
					{
						dataRow["SumPrice"] = FUNC_DecimalNum(item.TotalPrice);
						dataRow["Total"] = FUNC_DecimalNum(item.TotalPrice + item.VAT);
					}
					dataRow["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["SumPriceDisc"]) - Convert.ToDecimal(dataRow["SumAvergItem"]));
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				InsertNewRow();
				CBCostCenter.SelectedValue = order_PriceOffer?.CostCentersID ?? 1;
				CBProject.SelectedValue = order_PriceOffer?.ProjectId ?? 1;
				BtnCustomerSearch2.Text = order_PriceOffer.SupplierID.ToString();
				TxtCode.Text = order_PriceOffer.PurBranchID?.ToString() ?? string.Empty;
				LblSup.Text = order_PriceOffer.SupplierID.ToString();
				TxtSuppliers.Text = order_PriceOffer.SupplierName;
				TxtPhone.Text = order_PriceOffer.SupplierPhone;
				TxtAdditionalNum.Text = order_PriceOffer.AdditionalNum;
				TxtPostalCode.Text = order_PriceOffer.PostalCode;
				TxtCommercialRecord.Text = order_PriceOffer.CommercialRecord;
				TxtSales.Text = order_PriceOffer.SalesMan;
				TxtCity.Text = order_PriceOffer.City;
				TxtStreet.Text = order_PriceOffer.Street;
				TxtDistrict.Text = order_PriceOffer.District;
				TxtCountry.Text = order_PriceOffer.Country;
				TxtNumVat.Text = order_PriceOffer.SupplierVatNum;
				TxtDate.DateTime = Convert.ToDateTime(order_PriceOffer.Purchases_Date);
				TxtNote.Text = order_PriceOffer.Note;
				TxtNumNote.Text = order_PriceOffer.NoteNum;
				TxtTotals.Text = Convert.ToString(order_PriceOffer.TotalPrices);
				TxtVat.Text = Convert.ToString(order_PriceOffer.Tax);
				TxtDiscount.Text = order_PriceOffer.DiscountNum.ToString();
				TxtDisprege.Text = order_PriceOffer.DiscountPerantage.ToString();
				TxtAllDiscount.Text = order_PriceOffer.AllDiscount.ToString();
				TxtVatDisc.Text = order_PriceOffer.Tax_Discount.ToString();
				TxtTotalDiscount.Text = order_PriceOffer.Safy.ToString();
				CBSaleTybe.SelectedValue = order_PriceOffer.Sectoral;
				TxtNet.Text = order_PriceOffer.Net.ToString();
				TxtType.Text = ORD.GetAccountNameById(Convert.ToInt32(order_PriceOffer.OrderType));
				LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum);
				LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum);
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
			}
			else
			{
				BtnOrderSearch.Text = string.Empty;
				BtnOrderSearch.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void Tafqit()
	{
		try
		{
			decimal d = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
			decimal num = Math.Round(d, 2);
			decimal num2 = num;
			int num3 = (int)Math.Floor(num2);
			int num4 = (int)Math.Round((num2 - (decimal)num3) * 100m);
			string empty = string.Empty;
			if (LoginDetails.Lang == "ar-EG")
			{
				string text = num3.ToWords(new CultureInfo("ar"));
				string text2 = ((num4 > 0) ? ("و" + num4.ToWords(new CultureInfo("ar")) + " هللة") : string.Empty);
				empty = text + " ريالا\u064b " + text2 + " فقط لا غير";
			}
			else
			{
				string text3 = num3.ToWords(new CultureInfo("en"));
				string text4 = ((num4 > 0) ? ("and " + num4.ToWords(new CultureInfo("en")) + " Halalah") : string.Empty);
				empty = text3 + " Saudi Riyal " + text4 + " only";
			}
			TxtTafqit.Text = empty;
		}
		catch (Exception)
		{
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
							if (!(row["SN"].ToString() == Convert.ToString(text2)))
							{
								continue;
							}
							row["UnitNameTF"] = get_Item_ByCodeALL_Result.UnitTafqit;
							row["QuantityTF"] = "1";
							row["UnitType"] = "الصغرى";
							row["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
							row["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
							row["Quantity"] = 1;
							row["SumAvergItem"] = Math.Round(Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]), DecimalNumQty);
							int num2 = Convert.ToInt32(CBSaleTybe.SelectedValue);
							if (LoginDetails.ItemTotalWithVat)
							{
								decimal value = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
								switch (num2)
								{
								case 1:
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall / (decimal?)value);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
									break;
								case 2:
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall2), DecimalNum);
									row["OrginalPrice"] = row["SumPrice"];
									break;
								case 3:
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall3);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall3), DecimalNum);
									row["OrginalPrice"] = row["SumPrice"];
									break;
								default:
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall / (decimal?)value);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
									break;
								}
								VatWithTotal(value, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Quantity"]), row);
							}
							else
							{
								switch (num2)
								{
								case 1:
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
									break;
								case 2:
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall2), DecimalNum);
									break;
								case 3:
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall3);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall3), DecimalNum);
									break;
								default:
									row["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
									row["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
									break;
								}
								Vat(get_Item_ByCodeALL_Result.Tax_Value, Convert.ToDecimal(row["UnitPrice"]), Convert.ToDecimal(row["Quantity"]), row);
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
						int num3 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row2 in datatable.Rows)
						{
							if (!(row2["SN"].ToString() == Convert.ToString(text2)))
							{
								continue;
							}
							row2["Quantity"] = 1;
							row2["UnitNameTF"] = get_Item_ByCodeALL_Result2.UnitTafqit;
							row2["QuantityTF"] = "1";
							decimal num4 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.ConvertMediumUnit);
							row2["SumAvergItem"] = Math.Round(Convert.ToDecimal(row2["AvergItem"]) * (Convert.ToDecimal(row2["Quantity"]) * num4), DecimalNumQty);
							row2["UnitType"] = "المتوسطة";
							row2["ItemUnitID"] = get_Item_ByCodeALL_Result2.UnitMediumID;
							row2["UnitName"] = get_Item_ByCodeALL_Result2.UnitMedium;
							row2["AvergItem"] = get_Item_ByCodeALL_Result2.Average_cost * get_Item_ByCodeALL_Result2.ConvertMediumUnit;
							row2["SumAvergItem"] = get_Item_ByCodeALL_Result2.Average_cost * get_Item_ByCodeALL_Result2.ConvertMediumUnit;
							row2["UnitPrice"] = get_Item_ByCodeALL_Result2.SellPriceMedium;
							row2["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium);
							int num5 = Convert.ToInt32(CBSaleTybe.SelectedValue);
							if (LoginDetails.ItemTotalWithVat)
							{
								decimal value2 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.Tax_Value) / 100m + 1m;
								switch (num5)
								{
								case 1:
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium / (decimal?)value2);
									row2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
									break;
								case 2:
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium2);
									row2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium2), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
									break;
								case 3:
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium3);
									row2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium3), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
									break;
								default:
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium / (decimal?)value2);
									row2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
									break;
								}
								VatWithTotal(value2, Convert.ToDecimal(row2["UnitPrice"]), Convert.ToDecimal(row2["Quantity"]), row2);
							}
							else
							{
								switch (num5)
								{
								case 1:
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium);
									row2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
									break;
								case 2:
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium2);
									row2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium2), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
									break;
								case 3:
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium3);
									row2["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium3), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
									break;
								default:
									row2["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result2.SellPriceMedium);
									row2["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result2.SellPriceMedium), DecimalNum);
									row2["OrginalPrice"] = row2["Total"];
									break;
								}
								Vat(get_Item_ByCodeALL_Result2.Tax_Value, Convert.ToDecimal(row2["UnitPrice"]), Convert.ToDecimal(row2["Quantity"]), row2);
							}
						}
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
							if (!(row3["SN"].ToString() == Convert.ToString(text2)))
							{
								continue;
							}
							row3["Quantity"] = 1;
							row3["UnitType"] = get_Item_ByCodeALL_Result3.UnitTafqit;
							row3["QuantityTF"] = "1";
							row3["UnitType"] = "الكبرى";
							row3["ItemUnitID"] = get_Item_ByCodeALL_Result3.UnitLargeID;
							row3["UnitName"] = get_Item_ByCodeALL_Result3.UnitLarge;
							row3["AvergItem"] = get_Item_ByCodeALL_Result3.Average_cost * get_Item_ByCodeALL_Result3.ConvertMediumUnit * get_Item_ByCodeALL_Result3.ConvertBigUnit;
							row3["SumAvergItem"] = get_Item_ByCodeALL_Result3.Average_cost * get_Item_ByCodeALL_Result3.ConvertMediumUnit * get_Item_ByCodeALL_Result3.ConvertBigUnit;
							int num7 = Convert.ToInt32(CBSaleTybe.SelectedValue);
							if (LoginDetails.ItemTotalWithVat)
							{
								decimal value3 = Convert.ToDecimal(get_Item_ByCodeALL_Result3.Tax_Value) / 100m + 1m;
								switch (num7)
								{
								case 1:
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge / (decimal?)value3);
									row3["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
									break;
								case 2:
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge2);
									row3["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge2), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
									break;
								case 3:
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge3);
									row3["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge3), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
									break;
								default:
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge / (decimal?)value3);
									row3["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
									break;
								}
								VatWithTotal(value3, Convert.ToDecimal(row3["UnitPrice"]), Convert.ToDecimal(row3["Quantity"]), row3);
							}
							else
							{
								switch (num7)
								{
								case 1:
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge);
									row3["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
									break;
								case 2:
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge2);
									row3["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge2), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
									break;
								case 3:
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge3);
									row3["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge3), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
									break;
								default:
									row3["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result3.SellpriceLarge);
									row3["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result3.SellpriceLarge), DecimalNum);
									row3["OrginalPrice"] = row3["Total"];
									break;
								}
								Vat(get_Item_ByCodeALL_Result3.Tax_Value, Convert.ToDecimal(row3["UnitPrice"]), Convert.ToDecimal(row3["Quantity"]), row3);
							}
						}
						frmSearchUnit.Close();
					}
				}
				frmSearchUnit.Close();
			}
			gridControl1.DataSource = datatable;
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[5];
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
				if (row["ItemCode"].ToString() != string.Empty)
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
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
		gridControl1.DataSource = datatable;
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
							row["StoreID"] = StoreID;
							break;
						}
					}
					gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[5];
					gridView1.ShowEditor();
					frmSearchTransfer.Close();
					gridControl1.DataSource = datatable;
					return;
				}
				int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				foreach (DataRow row2 in datatable.Rows)
				{
					if (row2["SN"].ToString() == Convert.ToString(num2))
					{
						row2["StoreName"] = Comp_Store.Text;
						row2["StoreID"] = Comp_Store.SelectedValue;
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
		if (LoginDetails.Lang == "ar-EG")
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
		}
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
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		TxtDisprege.ReadOnly = false;
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
		else if (e.KeyCode.ToString() == "F2")
		{
			if (LoginDetails.Active_Virgin == 9)
			{
				GTSErpSystem.Frms.Orders.FrmReservations.FrmReservations frmReservations = new GTSErpSystem.Frms.Orders.FrmReservations.FrmReservations();
				if (frmReservations.ShowDialog() == DialogResult.OK)
				{
					selectedBookingDate = frmReservations.BookingDate;
					selectedInstallationDate = frmReservations.InstallationDate;
					selectedJawDate = frmReservations.JawDate;
					selectedHallsID = frmReservations.HallsID;
				}
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
			else
			{
				Edit();
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
				string custName = TxtSuppliers.Text;
				try
				{
					int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
					foreach (DataRow row in datatable.Rows)
					{
						if (row["SN"].ToString() == Convert.ToString(num))
						{
							decimal currentQuantity = Convert.ToDecimal(row["Quantity"]);
							decimal unitPrice = Convert.ToDecimal(row["UnitPriceDisc"]);
							int num2 = Convert.ToInt32(row["ItemID"]);
							if (num2 > 0)
							{
								LoginDetails.ItemcodeForStore = num2;
								FrmSearchTransferAllBranch frmSearchTransferAllBranch = new FrmSearchTransferAllBranch(num2, StorId, currentQuantity, unitPrice, UnitName, custName)
								{
									Owner = this
								};
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
		}
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

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void TxtNet_TextChanged(object sender, EventArgs e)
	{
		Tafqit();
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

	private void MenuExit_Click(object sender, EventArgs e)
	{
		Close();
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

	private void FrmPriceOffer_FormClosing(object sender, FormClosingEventArgs e)
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
					TxtCountry.Text = account_CustSup.Country;
					TxtStreet.Text = account_CustSup.Street;
					TxtDistrict.Text = account_CustSup.District;
					CreditLimit = account_CustSup.CreditLimit;
					BtnCustomerSearch2.Text = account_CustSup.ID.ToString();
					Charge(ID);
				}
				else
				{
					BtnCustomerSearch2.Text = string.Empty;
				}
			}
		}
		catch (Exception)
		{
			Clear();
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
				TxtCharge.Text = FUNC_DecimalNum(item.HasValue ? item.Value : 0m).ToString();
			}
		}
		catch (Exception)
		{
		}
	}

	private decimal FUNC_DecimalNumQty(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNumQty);
	}

	private void CBProject_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Return)
		{
			groupBox2.Focus();
			gridView1.Focus();
		}
	}

	private void CBProject_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
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

	private void ZeroDisc()
	{
		TxtDiscount.Text = "0.00";
		TxtDisprege.Text = "0.00";
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

	private void TxtPhone_KeyDown_1(object sender, KeyEventArgs e)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		Account_CustSup account_CustSup = gTSdbEntities.Account_CustSup.Where((Account_CustSup x) => x.Phone == TxtPhone.Text).FirstOrDefault();
		if (account_CustSup != null)
		{
			SearchCustNum(Convert.ToInt32(account_CustSup.ID));
		}
	}

	private void TxtPhone_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("ar"));
	}

	private void TxtNumNote_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void label39_Click(object sender, EventArgs e)
	{
	}

	private void TxtDisprege_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (!decimal.TryParse(TxtDisprege.Text, out var result))
			{
				result = 0m;
			}
			if (TxtDisprege.Text != string.Empty && Convert.ToDecimal(TxtDisprege.Text) != 0m && Convert.ToDecimal(TxtDisprege.Text) <= 100m)
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
			if (TxtAllDiscount.Text != string.Empty && Convert.ToDecimal(TxtAllDiscount.Text) != 0m && Convert.ToDecimal(TxtAllDiscount.Text) < Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text))
			{
				decimal per = Convert.ToDecimal(TxtAllDiscount.Text);
				Cal_FinalResultTotals(per, "AllDiscount");
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
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void TxtPhone_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void TxtAdditionalNum_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void TxtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void TxtCommercialRecord_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmPriceOffer));
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
		DevExpress.XtraGrid.GridLevelNode gridLevelNode = new DevExpress.XtraGrid.GridLevelNode();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions8 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled8 = new DevExpress.Utils.SerializableAppearanceObject();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.رقمالفاتورةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
		this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn47 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.TxtDisprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label47 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.TxtSellPrice1 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.TxtSellPrice2 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label13 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.TxtSellPrice3 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtProfit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtAllDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtLocation = new DevExpress.XtraEditors.TextEdit();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label25 = new System.Windows.Forms.Label();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.TxtQuntity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtAllQuantity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtLastCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtAvergCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtTotalDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
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
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.TxtCountry = new DevExpress.XtraEditors.TextEdit();
		this.label33 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.label45 = new System.Windows.Forms.Label();
		this.label44 = new System.Windows.Forms.Label();
		this.label41 = new System.Windows.Forms.Label();
		this.label39 = new System.Windows.Forms.Label();
		this.label38 = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.BtnCustomerSearch2 = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnNewCustomer = new DevExpress.XtraEditors.SimpleButton();
		this.label22 = new System.Windows.Forms.Label();
		this.label34 = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label42 = new System.Windows.Forms.Label();
		this.label43 = new System.Windows.Forms.Label();
		this.TxtAdditionalNum = new DevExpress.XtraEditors.TextEdit();
		this.TxtStreet = new DevExpress.XtraEditors.TextEdit();
		this.TxtDistrict = new DevExpress.XtraEditors.TextEdit();
		this.TxtCommercialRecord = new DevExpress.XtraEditors.TextEdit();
		this.TxtPostalCode = new DevExpress.XtraEditors.TextEdit();
		this.TxtCity = new DevExpress.XtraEditors.TextEdit();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.LblSup = new System.Windows.Forms.Label();
		this.label37 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label46 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.TxtNumVat = new DevExpress.XtraEditors.TextEdit();
		this.label40 = new System.Windows.Forms.Label();
		this.TxtSales = new DevExpress.XtraEditors.TextEdit();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.label51 = new System.Windows.Forms.Label();
		this.label50 = new System.Windows.Forms.Label();
		this.label49 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.LblCustLast = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.CBSaleTybe = new System.Windows.Forms.ComboBox();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label48 = new System.Windows.Forms.Label();
		this.CBBounce = new System.Windows.Forms.ComboBox();
		this.BtnOrderSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.label18 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.CBProject = new System.Windows.Forms.ComboBox();
		this.Comp_Store = new System.Windows.Forms.ComboBox();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.label31 = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCountry.Properties).BeginInit();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAdditionalNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtStreet.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDistrict.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.repositoryItemButtonEditCodeSearch, "repositoryItemButtonEditCodeSearch");
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions33");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons"), resources.GetString("repositoryItemButtonEditCodeSearch.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEditCodeSearch.Buttons6"), resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons9"))
		});
		this.repositoryItemButtonEditCodeSearch.Name = "repositoryItemButtonEditCodeSearch";
		this.repositoryItemButtonEditCodeSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEditCodeSearch_ButtonClick);
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
		editorButtonImageOptions2.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions34");
		editorButtonImageOptions2.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemButtonEdit4.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions35");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"), resources.GetString("repositoryItemButtonEdit5.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit5.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit5.Buttons6"), resources.GetObject("repositoryItemButtonEdit5.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit5.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit5.Buttons9"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEdit5.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit6, "repositoryItemButtonEdit6");
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions36");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
		resources.ApplyResources(this.gridColumn10, "gridColumn10");
		this.gridColumn10.Name = "gridColumn10";
		this.repositoryItemButtonEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions5.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions37");
		editorButtonImageOptions5.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions38");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"), resources.GetString("repositoryItemButtonEdit3.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit3.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("repositoryItemButtonEdit3.Buttons6"), resources.GetObject("repositoryItemButtonEdit3.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit3.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit3.Buttons9"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		resources.ApplyResources(editorButtonImageOptions7, "editorButtonImageOptions39");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		resources.ApplyResources(this.رقمالفاتورةToolStripMenuItem1, "رقمالفاتورةToolStripMenuItem1");
		this.رقمالفاتورةToolStripMenuItem1.Name = "رقمالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.gridColumn18, "gridColumn18");
		this.gridColumn18.Name = "gridColumn18";
		resources.ApplyResources(this.cardView1, "cardView1");
		this.cardView1.CardWidth = 199;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
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
		gridLevelNode.RelationName = "Level1";
		this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[1] { gridLevelNode });
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView1.Appearance.HeaderPanel.Font");
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		resources.ApplyResources(this.gridView1, "gridView1");
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[39]
		{
			this.gridColumn1, this.gridColumn2, this.shearch, this.gridColumn23, this.gridColumn19, this.gridColumn4, this.gridColumn20, this.gridColumn21, this.gridColumn3, this.gridColumn6,
			this.gridColumn7, this.gridColumn8, this.gridColumn22, this.gridColumn5, this.gridColumn9, this.gridColumn24, this.gridColumn25, this.gridColumn26, this.gridColumn27, this.gridColumn28,
			this.gridColumn29, this.gridColumn30, this.gridColumn31, this.gridColumn32, this.gridColumn33, this.gridColumn34, this.gridColumn35, this.gridColumn36, this.gridColumn37, this.gridColumn38,
			this.gridColumn39, this.gridColumn40, this.gridColumn41, this.gridColumn42, this.gridColumn43, this.gridColumn44, this.gridColumn45, this.gridColumn46, this.gridColumn47
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
		this.gridColumn23.FieldName = "ItemNote";
		this.gridColumn23.MinWidth = 27;
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceCell.Font");
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.FieldName = "UnitName";
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
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
		resources.ApplyResources(this.gridColumn3, "gridColumn3");
		this.gridColumn3.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceCell.Font");
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceHeader.Font");
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.FieldName = "Total";
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.OptionsColumn.AllowFocus = false;
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
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn7.Summary"), resources.GetString("gridColumn7.Summary1"), resources.GetString("gridColumn7.Summary2"))
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
		resources.ApplyResources(this.gridColumn24, "gridColumn24");
		this.gridColumn24.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceCell.Font");
		this.gridColumn24.AppearanceCell.Options.UseFont = true;
		this.gridColumn24.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceHeader.Font");
		this.gridColumn24.AppearanceHeader.Options.UseFont = true;
		this.gridColumn24.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.FieldName = "ItemID";
		this.gridColumn24.MinWidth = 29;
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn24.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn25, "gridColumn25");
		this.gridColumn25.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceCell.Font");
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
		this.gridColumn25.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceHeader.Font");
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.FieldName = "ItemUnitID";
		this.gridColumn25.MinWidth = 29;
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
		this.gridColumn26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
		this.gridColumn26.FieldName = "TafqitID";
		this.gridColumn26.MinWidth = 29;
		this.gridColumn26.Name = "gridColumn26";
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
		this.gridColumn27.FieldName = "UnitNameTF";
		this.gridColumn27.MinWidth = 29;
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
		this.gridColumn28.FieldName = "QuantityTF";
		this.gridColumn28.MinWidth = 29;
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
		this.gridColumn29.FieldName = "BounceItem";
		this.gridColumn29.MinWidth = 29;
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
		this.gridColumn30.FieldName = "Height";
		this.gridColumn30.MinWidth = 29;
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
		this.gridColumn31.FieldName = "Width";
		this.gridColumn31.MinWidth = 29;
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
		this.gridColumn32.FieldName = "Long";
		this.gridColumn32.MinWidth = 29;
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
		this.gridColumn33.FieldName = "Weight";
		this.gridColumn33.MinWidth = 29;
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
		this.gridColumn34.FieldName = "UnitNum";
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
		this.gridColumn35.FieldName = "StoreID";
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
		this.gridColumn36.FieldName = "OrginalPrice";
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
		this.gridColumn37.FieldName = "SumPrice";
		this.gridColumn37.MinWidth = 29;
		this.gridColumn37.Name = "gridColumn37";
		this.gridColumn37.OptionsColumn.AllowEdit = false;
		this.gridColumn37.OptionsColumn.AllowFocus = false;
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
		this.gridColumn38.FieldName = "PercentDisc";
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
		this.gridColumn39.FieldName = "AmountDisc";
		this.gridColumn39.MinWidth = 29;
		this.gridColumn39.Name = "gridColumn39";
		this.gridColumn39.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
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
		this.gridColumn40.FieldName = "UnitPriceDisc";
		this.gridColumn40.MinWidth = 29;
		this.gridColumn40.Name = "gridColumn40";
		this.gridColumn40.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn41, "gridColumn41");
		this.gridColumn41.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn41.AppearanceCell.Font");
		this.gridColumn41.AppearanceCell.Options.UseFont = true;
		this.gridColumn41.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn41.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn41.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn41.AppearanceHeader.Font");
		this.gridColumn41.AppearanceHeader.Options.UseFont = true;
		this.gridColumn41.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn41.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn41.FieldName = "AvergItem";
		this.gridColumn41.MinWidth = 29;
		this.gridColumn41.Name = "gridColumn41";
		this.gridColumn41.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn42, "gridColumn42");
		this.gridColumn42.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn42.AppearanceCell.Font");
		this.gridColumn42.AppearanceCell.Options.UseFont = true;
		this.gridColumn42.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn42.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn42.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn42.AppearanceHeader.Font");
		this.gridColumn42.AppearanceHeader.Options.UseFont = true;
		this.gridColumn42.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn42.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn42.FieldName = "SumAvergItem";
		this.gridColumn42.MinWidth = 29;
		this.gridColumn42.Name = "gridColumn42";
		this.gridColumn42.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn43, "gridColumn43");
		this.gridColumn43.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn43.AppearanceCell.Font");
		this.gridColumn43.AppearanceCell.Options.UseFont = true;
		this.gridColumn43.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn43.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn43.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn43.AppearanceHeader.Font");
		this.gridColumn43.AppearanceHeader.Options.UseFont = true;
		this.gridColumn43.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn43.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn43.FieldName = "Profit";
		this.gridColumn43.MinWidth = 29;
		this.gridColumn43.Name = "gridColumn43";
		this.gridColumn43.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn44, "gridColumn44");
		this.gridColumn44.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn44.AppearanceCell.Font");
		this.gridColumn44.AppearanceCell.Options.UseFont = true;
		this.gridColumn44.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn44.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn44.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn44.AppearanceHeader.Font");
		this.gridColumn44.AppearanceHeader.Options.UseFont = true;
		this.gridColumn44.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn44.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn44.FieldName = "IsItemTax";
		this.gridColumn44.MinWidth = 29;
		this.gridColumn44.Name = "gridColumn44";
		this.gridColumn44.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn45, "gridColumn45");
		this.gridColumn45.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn45.AppearanceCell.Font");
		this.gridColumn45.AppearanceCell.Options.UseFont = true;
		this.gridColumn45.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn45.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn45.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn45.AppearanceHeader.Font");
		this.gridColumn45.AppearanceHeader.Options.UseFont = true;
		this.gridColumn45.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn45.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn45.FieldName = "Tax_Value";
		this.gridColumn45.MinWidth = 29;
		this.gridColumn45.Name = "gridColumn45";
		this.gridColumn45.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn46, "gridColumn46");
		this.gridColumn46.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn46.AppearanceCell.Font");
		this.gridColumn46.AppearanceCell.Options.UseFont = true;
		this.gridColumn46.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn46.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn46.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn46.AppearanceHeader.Font");
		this.gridColumn46.AppearanceHeader.Options.UseFont = true;
		this.gridColumn46.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn46.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn46.FieldName = "Done";
		this.gridColumn46.MinWidth = 29;
		this.gridColumn46.Name = "gridColumn46";
		this.gridColumn46.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn47, "gridColumn47");
		this.gridColumn47.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn47.AppearanceCell.Font");
		this.gridColumn47.AppearanceCell.Options.UseFont = true;
		this.gridColumn47.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn47.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn47.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn47.AppearanceHeader.Font");
		this.gridColumn47.AppearanceHeader.Options.UseFont = true;
		this.gridColumn47.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn47.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn47.FieldName = "UnitType";
		this.gridColumn47.MinWidth = 29;
		this.gridColumn47.Name = "gridColumn47";
		this.gridColumn47.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.TxtDisprege);
		this.groupBox2.Controls.Add(this.label47);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.label15);
		this.groupBox2.Controls.Add(this.label30);
		this.groupBox2.Controls.Add(this.label16);
		this.groupBox2.Controls.Add(this.TxtSellPrice1);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.label28);
		this.groupBox2.Controls.Add(this.label29);
		this.groupBox2.Controls.Add(this.TxtSellPrice2);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label27);
		this.groupBox2.Controls.Add(this.TxtSellPrice3);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.TxtProfit);
		this.groupBox2.Controls.Add(this.TxtAllDiscount);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.TxtNet);
		this.groupBox2.Controls.Add(this.TxtVatDisc);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.TxtVat);
		this.groupBox2.Controls.Add(this.TxtLocation);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.label25);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.TxtTafqit);
		this.groupBox2.Controls.Add(this.TxtQuntity);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.TxtAllQuantity);
		this.groupBox2.Controls.Add(this.TxtLastCost);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.TxtAvergCost);
		this.groupBox2.Controls.Add(this.TxtTotals);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtTotalDiscount);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtDiscount);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.TxtDisprege, "TxtDisprege");
		this.TxtDisprege.DecimalDigits = 3;
		this.TxtDisprege.ForeColor = System.Drawing.Color.Black;
		this.TxtDisprege.Name = "TxtDisprege";
		this.TxtDisprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDisprege.Value = new decimal(new int[4]);
		this.TxtDisprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDisprege.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtDisprege_KeyUp);
		resources.ApplyResources(this.label47, "label47");
		this.label47.Name = "label47";
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.label15, "label15");
		this.label15.Name = "label15";
		resources.ApplyResources(this.label30, "label30");
		this.label30.Name = "label30";
		resources.ApplyResources(this.label16, "label16");
		this.label16.Name = "label16";
		resources.ApplyResources(this.TxtSellPrice1, "TxtSellPrice1");
		this.TxtSellPrice1.DecimalDigits = 3;
		this.TxtSellPrice1.ForeColor = System.Drawing.Color.Black;
		this.TxtSellPrice1.Name = "TxtSellPrice1";
		this.TxtSellPrice1.ReadOnly = true;
		this.TxtSellPrice1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtSellPrice1.Value = new decimal(new int[4]);
		this.TxtSellPrice1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label14, "label14");
		this.label14.Name = "label14";
		resources.ApplyResources(this.label28, "label28");
		this.label28.Name = "label28";
		resources.ApplyResources(this.label29, "label29");
		this.label29.Name = "label29";
		resources.ApplyResources(this.TxtSellPrice2, "TxtSellPrice2");
		this.TxtSellPrice2.DecimalDigits = 3;
		this.TxtSellPrice2.ForeColor = System.Drawing.Color.Black;
		this.TxtSellPrice2.Name = "TxtSellPrice2";
		this.TxtSellPrice2.ReadOnly = true;
		this.TxtSellPrice2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtSellPrice2.Value = new decimal(new int[4]);
		this.TxtSellPrice2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label13, "label13");
		this.label13.Name = "label13";
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.label27, "label27");
		this.label27.Name = "label27";
		resources.ApplyResources(this.TxtSellPrice3, "TxtSellPrice3");
		this.TxtSellPrice3.DecimalDigits = 3;
		this.TxtSellPrice3.ForeColor = System.Drawing.Color.Black;
		this.TxtSellPrice3.Name = "TxtSellPrice3";
		this.TxtSellPrice3.ReadOnly = true;
		this.TxtSellPrice3.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtSellPrice3.Value = new decimal(new int[4]);
		this.TxtSellPrice3.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.TxtProfit, "TxtProfit");
		this.TxtProfit.DecimalDigits = 3;
		this.TxtProfit.ForeColor = System.Drawing.Color.Black;
		this.TxtProfit.Name = "TxtProfit";
		this.TxtProfit.ReadOnly = true;
		this.TxtProfit.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtProfit.Value = new decimal(new int[4]);
		this.TxtProfit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtAllDiscount, "TxtAllDiscount");
		this.TxtAllDiscount.DecimalDigits = 3;
		this.TxtAllDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtAllDiscount.Name = "TxtAllDiscount";
		this.TxtAllDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllDiscount.Value = new decimal(new int[4]);
		this.TxtAllDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtAllDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtAllDiscount_KeyUp);
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
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
		resources.ApplyResources(this.TxtVatDisc, "TxtVatDisc");
		this.TxtVatDisc.DecimalDigits = 3;
		this.TxtVatDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatDisc.Name = "TxtVatDisc";
		this.TxtVatDisc.ReadOnly = true;
		this.TxtVatDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatDisc.Value = new decimal(new int[4]);
		this.TxtVatDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNew.Appearance.Font");
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label25, "label25");
		this.label25.Name = "label25";
		resources.ApplyResources(this.BtnPrint, "BtnPrint");
		this.BtnPrint.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrint.Appearance.Font");
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnEdit.Appearance.Font");
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		resources.ApplyResources(this.TxtQuntity, "TxtQuntity");
		this.TxtQuntity.DecimalDigits = 3;
		this.TxtQuntity.ForeColor = System.Drawing.Color.Black;
		this.TxtQuntity.Name = "TxtQuntity";
		this.TxtQuntity.ReadOnly = true;
		this.TxtQuntity.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtQuntity.Value = new decimal(new int[4]);
		this.TxtQuntity.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
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
		resources.ApplyResources(this.TxtTotals, "TxtTotals");
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.ReadOnly = true;
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotals.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		this.TxtTotalDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.TxtDiscount, "TxtDiscount");
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtDiscount_KeyUp);
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
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
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions40");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCode.Buttons"), resources.GetString("repositoryItemButtonEditCode.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCode.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("repositoryItemButtonEditCode.Buttons6"), resources.GetObject("repositoryItemButtonEditCode.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCode.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCode.Buttons9"))
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
		resources.ApplyResources(this.groupBox3, "groupBox3");
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.TabStop = false;
		resources.ApplyResources(this.TxtCountry, "TxtCountry");
		this.TxtCountry.Name = "TxtCountry";
		this.TxtCountry.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCountry.Properties.Appearance.Font");
		this.TxtCountry.Properties.Appearance.Options.UseFont = true;
		this.TxtCountry.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCountry.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.label33, "label33");
		this.label33.Name = "label33";
		resources.ApplyResources(this.groupBox4, "groupBox4");
		this.groupBox4.Controls.Add(this.label45);
		this.groupBox4.Controls.Add(this.label44);
		this.groupBox4.Controls.Add(this.label41);
		this.groupBox4.Controls.Add(this.TxtCountry);
		this.groupBox4.Controls.Add(this.label33);
		this.groupBox4.Controls.Add(this.label39);
		this.groupBox4.Controls.Add(this.label38);
		this.groupBox4.Controls.Add(this.label36);
		this.groupBox4.Controls.Add(this.BtnCustomerSearch2);
		this.groupBox4.Controls.Add(this.BtnNewCustomer);
		this.groupBox4.Controls.Add(this.label22);
		this.groupBox4.Controls.Add(this.label34);
		this.groupBox4.Controls.Add(this.label35);
		this.groupBox4.Controls.Add(this.label19);
		this.groupBox4.Controls.Add(this.label42);
		this.groupBox4.Controls.Add(this.label43);
		this.groupBox4.Controls.Add(this.TxtAdditionalNum);
		this.groupBox4.Controls.Add(this.TxtStreet);
		this.groupBox4.Controls.Add(this.TxtDistrict);
		this.groupBox4.Controls.Add(this.TxtCommercialRecord);
		this.groupBox4.Controls.Add(this.TxtPostalCode);
		this.groupBox4.Controls.Add(this.TxtCity);
		this.groupBox4.Controls.Add(this.LBlSuppAccount);
		this.groupBox4.Controls.Add(this.LblSup);
		this.groupBox4.Controls.Add(this.label37);
		this.groupBox4.Controls.Add(this.label32);
		this.groupBox4.Controls.Add(this.label46);
		this.groupBox4.Controls.Add(this.TxtPhone);
		this.groupBox4.Controls.Add(this.TxtCharge);
		this.groupBox4.Controls.Add(this.TxtNumVat);
		this.groupBox4.Controls.Add(this.label40);
		this.groupBox4.Controls.Add(this.TxtSales);
		this.groupBox4.Controls.Add(this.TxtSuppliers);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.TabStop = false;
		resources.ApplyResources(this.label45, "label45");
		this.label45.Name = "label45";
		resources.ApplyResources(this.label44, "label44");
		this.label44.Name = "label44";
		resources.ApplyResources(this.label41, "label41");
		this.label41.Name = "label41";
		resources.ApplyResources(this.label39, "label39");
		this.label39.Name = "label39";
		this.label39.Click += new System.EventHandler(label39_Click);
		resources.ApplyResources(this.label38, "label38");
		this.label38.Name = "label38";
		resources.ApplyResources(this.label36, "label36");
		this.label36.Name = "label36";
		resources.ApplyResources(this.BtnCustomerSearch2, "BtnCustomerSearch2");
		this.BtnCustomerSearch2.Name = "BtnCustomerSearch2";
		this.BtnCustomerSearch2.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnCustomerSearch2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCustomerSearch2.Properties.Appearance.Font");
		this.BtnCustomerSearch2.Properties.Appearance.Options.UseBackColor = true;
		this.BtnCustomerSearch2.Properties.Appearance.Options.UseFont = true;
		this.BtnCustomerSearch2.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnCustomerSearch2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnCustomerSearch2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCustomerSearch2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnCustomerSearch2.Properties.Buttons"))
		});
		this.BtnCustomerSearch2.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCustomerSearch2.Properties.NullText = resources.GetString("BtnCustomerSearch2.Properties.NullText");
		this.BtnCustomerSearch2.Tag = "Search";
		this.BtnCustomerSearch2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnCustomerSearch2_ButtonClick);
		this.BtnCustomerSearch2.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnCustomerSearch2_KeyDown);
		resources.ApplyResources(this.BtnNewCustomer, "BtnNewCustomer");
		this.BtnNewCustomer.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNewCustomer.Appearance.Font");
		this.BtnNewCustomer.Appearance.Options.UseFont = true;
		this.BtnNewCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewCustomer.ImageOptions.Image");
		this.BtnNewCustomer.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewCustomer.Name = "BtnNewCustomer";
		this.BtnNewCustomer.Click += new System.EventHandler(BtnNewCustomer_Click);
		resources.ApplyResources(this.label22, "label22");
		this.label22.Name = "label22";
		resources.ApplyResources(this.label34, "label34");
		this.label34.Name = "label34";
		resources.ApplyResources(this.label35, "label35");
		this.label35.Name = "label35";
		resources.ApplyResources(this.label19, "label19");
		this.label19.Name = "label19";
		resources.ApplyResources(this.label42, "label42");
		this.label42.Name = "label42";
		resources.ApplyResources(this.label43, "label43");
		this.label43.Name = "label43";
		resources.ApplyResources(this.TxtAdditionalNum, "TxtAdditionalNum");
		this.TxtAdditionalNum.Name = "TxtAdditionalNum";
		this.TxtAdditionalNum.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAdditionalNum.Properties.Appearance.Font");
		this.TxtAdditionalNum.Properties.Appearance.Options.UseFont = true;
		this.TxtAdditionalNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAdditionalNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAdditionalNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtAdditionalNum_KeyPress);
		resources.ApplyResources(this.TxtStreet, "TxtStreet");
		this.TxtStreet.Name = "TxtStreet";
		this.TxtStreet.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtStreet.Properties.Appearance.Font");
		this.TxtStreet.Properties.Appearance.Options.UseFont = true;
		this.TxtStreet.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtStreet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtDistrict, "TxtDistrict");
		this.TxtDistrict.Name = "TxtDistrict";
		this.TxtDistrict.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDistrict.Properties.Appearance.Font");
		this.TxtDistrict.Properties.Appearance.Options.UseFont = true;
		this.TxtDistrict.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDistrict.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtCommercialRecord, "TxtCommercialRecord");
		this.TxtCommercialRecord.Name = "TxtCommercialRecord";
		this.TxtCommercialRecord.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCommercialRecord.Properties.Appearance.Font");
		this.TxtCommercialRecord.Properties.Appearance.Options.UseFont = true;
		this.TxtCommercialRecord.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCommercialRecord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCommercialRecord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtCommercialRecord_KeyPress);
		resources.ApplyResources(this.TxtPostalCode, "TxtPostalCode");
		this.TxtPostalCode.Name = "TxtPostalCode";
		this.TxtPostalCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPostalCode.Properties.Appearance.Font");
		this.TxtPostalCode.Properties.Appearance.Options.UseFont = true;
		this.TxtPostalCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPostalCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPostalCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPostalCode_KeyPress);
		resources.ApplyResources(this.TxtCity, "TxtCity");
		this.TxtCity.Name = "TxtCity";
		this.TxtCity.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCity.Properties.Appearance.Font");
		this.TxtCity.Properties.Appearance.Options.UseFont = true;
		this.TxtCity.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.LBlSuppAccount, "LBlSuppAccount");
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		resources.ApplyResources(this.LblSup, "LblSup");
		this.LblSup.Name = "LblSup";
		resources.ApplyResources(this.label37, "label37");
		this.label37.Name = "label37";
		resources.ApplyResources(this.label32, "label32");
		this.label32.Name = "label32";
		resources.ApplyResources(this.label46, "label46");
		this.label46.Name = "label46";
		resources.ApplyResources(this.TxtPhone, "TxtPhone");
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPhone.Properties.Appearance.Font");
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.Enter += new System.EventHandler(TxtPhone_Enter);
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtPhone_KeyDown_1);
		this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPhone_KeyPress);
		resources.ApplyResources(this.TxtCharge, "TxtCharge");
		this.TxtCharge.Name = "TxtCharge";
		this.TxtCharge.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCharge.Properties.Appearance.Font");
		this.TxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.TxtNumVat, "TxtNumVat");
		this.TxtNumVat.Name = "TxtNumVat";
		this.TxtNumVat.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumVat.Properties.Appearance.Font");
		this.TxtNumVat.Properties.Appearance.Options.UseFont = true;
		this.TxtNumVat.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumVat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumVat_KeyPress);
		resources.ApplyResources(this.label40, "label40");
		this.label40.Name = "label40";
		resources.ApplyResources(this.TxtSales, "TxtSales");
		this.TxtSales.Name = "TxtSales";
		this.TxtSales.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtSales.Properties.Appearance.Font");
		this.TxtSales.Properties.Appearance.Options.UseFont = true;
		this.TxtSales.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSales.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
		this.TxtSuppliers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtSuppliers_ButtonClick);
		resources.ApplyResources(this.groupBox5, "groupBox5");
		this.groupBox5.Controls.Add(this.label51);
		this.groupBox5.Controls.Add(this.label50);
		this.groupBox5.Controls.Add(this.label49);
		this.groupBox5.Controls.Add(this.TxtCode);
		this.groupBox5.Controls.Add(this.label21);
		this.groupBox5.Controls.Add(this.LblCustLast);
		this.groupBox5.Controls.Add(this.label17);
		this.groupBox5.Controls.Add(this.CBSaleTybe);
		this.groupBox5.Controls.Add(this.TxtDate);
		this.groupBox5.Controls.Add(this.label48);
		this.groupBox5.Controls.Add(this.CBBounce);
		this.groupBox5.Controls.Add(this.BtnOrderSearch);
		this.groupBox5.Controls.Add(this.label18);
		this.groupBox5.Controls.Add(this.label6);
		this.groupBox5.Controls.Add(this.label26);
		this.groupBox5.Controls.Add(this.TxtNumNote);
		this.groupBox5.Controls.Add(this.CBProject);
		this.groupBox5.Controls.Add(this.Comp_Store);
		this.groupBox5.Controls.Add(this.CBCostCenter);
		this.groupBox5.Controls.Add(this.label31);
		this.groupBox5.Controls.Add(this.TxtType);
		this.groupBox5.Controls.Add(this.TxtNote);
		this.groupBox5.Controls.Add(this.label23);
		this.groupBox5.Controls.Add(this.label20);
		this.groupBox5.Controls.Add(this.label24);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.TabStop = false;
		resources.ApplyResources(this.label51, "label51");
		this.label51.Name = "label51";
		resources.ApplyResources(this.label50, "label50");
		this.label50.Name = "label50";
		resources.ApplyResources(this.label49, "label49");
		this.label49.Name = "label49";
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
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick_1);
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.LblCustLast, "LblCustLast");
		this.LblCustLast.Name = "LblCustLast";
		resources.ApplyResources(this.label17, "label17");
		this.label17.Name = "label17";
		resources.ApplyResources(this.CBSaleTybe, "CBSaleTybe");
		this.CBSaleTybe.FormattingEnabled = true;
		this.CBSaleTybe.Name = "CBSaleTybe";
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
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		resources.ApplyResources(this.label48, "label48");
		this.label48.Name = "label48";
		resources.ApplyResources(this.CBBounce, "CBBounce");
		this.CBBounce.FormattingEnabled = true;
		this.CBBounce.Name = "CBBounce";
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
		this.BtnOrderSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnOrderSearch_KeyDown);
		this.BtnOrderSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BtnOrderSearch_KeyPress);
		resources.ApplyResources(this.label18, "label18");
		this.label18.Name = "label18";
		resources.ApplyResources(this.label26, "label26");
		this.label26.Name = "label26";
		resources.ApplyResources(this.TxtNumNote, "TxtNumNote");
		this.TxtNumNote.Name = "TxtNumNote";
		this.TxtNumNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumNote.Properties.Appearance.Font");
		this.TxtNumNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNumNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumNote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumNote_KeyPress);
		resources.ApplyResources(this.CBProject, "CBProject");
		this.CBProject.FormattingEnabled = true;
		this.CBProject.Name = "CBProject";
		this.CBProject.SelectedIndexChanged += new System.EventHandler(CBProject_SelectedIndexChanged);
		this.CBProject.KeyDown += new System.Windows.Forms.KeyEventHandler(CBProject_KeyDown);
		this.CBProject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CBProject_KeyPress);
		resources.ApplyResources(this.Comp_Store, "Comp_Store");
		this.Comp_Store.FormattingEnabled = true;
		this.Comp_Store.Name = "Comp_Store";
		resources.ApplyResources(this.CBCostCenter, "CBCostCenter");
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Name = "CBCostCenter";
		resources.ApplyResources(this.label31, "label31");
		this.label31.Name = "label31";
		resources.ApplyResources(this.TxtType, "TxtType");
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Name = "TxtType";
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNote.Properties.Appearance.Font");
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.label23, "label23");
		this.label23.Name = "label23";
		resources.ApplyResources(this.label20, "label20");
		this.label20.Name = "label20";
		resources.ApplyResources(this.label24, "label24");
		this.label24.Name = "label24";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmPriceOffer";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmPriceOffer_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TxtCountry.Properties).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAdditionalNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtStreet.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDistrict.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		base.ResumeLayout(false);
	}
}
