using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
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
using GTSErpSystem.BLL.Reports;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DAL.Models;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Dto;
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmCachMoney;
using GTSErpSystem.Frms.Orders.FrmLuckUpOrders;
using GTSErpSystem.Frms.Orders.FrmReservations;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Helper;
using Humanizer;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;
using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.APIHelper;

namespace GTSErpSystem.Frms.Orders;

public class FrmOrdersDraft : XtraForm
{
	private ApplicationDbContext context;

	private DiscountCodesManager DisCode;

	private Class_WaitFormFunc waitForm = new Class_WaitFormFunc();

	private Class_ElctroncInvoice Zakat = new Class_ElctroncInvoice();

	private GTSdbEntities db = new GTSdbEntities();

	private Class_Items ITM = new Class_Items();

	private Class_Orders ORD = new Class_Orders();

	private Class_PriceOffer PRCOffer = new Class_PriceOffer();

	private Class_Customer Cust = new Class_Customer();

	private Class_Bounce BNC = new Class_Bounce();

	private Class_Contract Contract = new Class_Contract();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private DataTable datatable = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	private int tafqitID = LoginDetails.TafqitID;

	public string txtWordAr;

	public string txtWordEn;

	public string CodeItm;

	public decimal FrmCash;

	public decimal FrmBank;

	public decimal Safy;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	private decimal ChargeAfterSale = 0m;

	private string fileNameAR = Application.StartupPath + "\\Layout\\OrderAR.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\OrderEN.xml";

	private decimal? CreditLimit = 0m;

	private int SN = 1;

	private int ItmID = 0;

	private int StorId = 0;

	private string UnitName = string.Empty;

	private decimal BounnsPre = 0m;

	private int AccountSN = 0;

	private FrmCars frmCars = new FrmCars();

	public int UserSellPrice = LoginDetails.UserSellPrice;

	private DateTime selectedBookingDate;

	private DateTime selectedInstallationDate;

	private DateTime selectedJawDate;

	private int selectedHallsID;

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

	private Label LblNowQuintity;

	private NumericEditBox TxtAllQuantity;

	private NumericEditBox TxtLastCost;

	private SimpleButton BtnEdit;

	private Label label10;

	private NumericEditBox TxtAvergCost;

	private SimpleButton BtnSave;

	private NumericEditBox TxtNet;

	private GridColumn gridColumn4;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem تخصيصToolStripMenuItem1;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem MenuExit;

	private GroupBox groupBox3;

	private GridView gridView1;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn shearch;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private GridColumn gridColumn22;

	private GridColumn gridColumn5;

	private GridColumn gridColumn9;

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

	private NumericEditBox TxtTotals;

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

	private NumericEditBox TxtVatDisc;

	private Label label5;

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

	private TextEdit TxtPhone;

	private Label label21;

	private TextEdit TxtNumVat;

	private Label label18;

	private ButtonEdit TxtSuppliers;

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private ToolStripMenuItem StripMenuItem1;

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

	private ToggleSwitch toggleHold;

	private GridColumn gridColumn23;

	private NumericEditBox TxtCost;

	private Label label27;

	private Label LBlSuppAccount;

	private GridColumn gridColumn24;

	private NumericEditBox TxtTabocc;

	private Label label29;

	private GridColumn gridColumn25;

	private GridColumn gridColumn26;

	private GridColumn gridColumn27;

	private Label label20;

	private System.Windows.Forms.ComboBox TxtType;

	private Label label31;

	private System.Windows.Forms.ComboBox CBBounce;

	private Label label17;

	private ButtonEdit TxtCode;

	private ButtonEdit BtnCustomerSearch;

	private ButtonEdit BtnOrderSearch;

	private GridColumn gridColumn28;

	private GridColumn gridColumn29;

	private GridColumn gridColumn30;

	private GridColumn gridColumn31;

	private GridColumn gridColumn32;

	private GridColumn gridColumn33;

	private GridColumn gridColumn34;

	private Label label32;

	private NumericEditBox TxtAllDiscount;

	private GridColumn ItemID;

	private ToolStripMenuItem كشفحسابToolStripMenuItem;

	private ToolStripMenuItem حركهصنففعليةToolStripMenuItem;

	private SimpleButton BtnNewCustomer;

	private SimpleButton BtnOrderReturn;

	private SimpleButton BtnRecipt;

	private SimpleButton BtnShowPrice;

	private ToggleSwitch toggleSectoral;

	private GroupBox groupBox4;

	private Label label42;

	private Label label43;

	private TextEdit TxtCity;

	private Label label15;

	private Label label46;

	private TextEdit TxtSales;

	private GroupBox groupBox5;

	private Label label48;

	private Label label22;

	private Label label35;

	private Label label19;

	private TextEdit TxtAdditionalNum;

	private TextEdit TxtCommercialRecord;

	private TextEdit TxtPostalCode;

	private Label label36;

	private System.Windows.Forms.ComboBox Comp_Store;

	private GridColumn gridColumn36;

	private Label LblCustLast;

	private Label label37;

	private TextEdit TxtCharge;

	private DateEdit DateHold;

	private Label LblDataHold;

	private Label label38;

	private Label label33;

	private TextEdit TxtDistrict;

	private TextEdit TxtAddress;

	private Label label34;

	private TextEdit TxtStreet;

	private Label label39;

	private System.Windows.Forms.ComboBox CBProject;

	private NumericEditBox TxtHasmPer;

	private Label LblPerHasm;

	private Label LblHasm;

	private Label LblAmountHasm;

	private NumericEditBox TxtHasmAmount;

	private GridColumn gridColumn37;

	private GridColumn gridColumn35;

	private GridColumn gridColumn38;

	private GridColumn gridColumn39;

	private GridColumn gridColumn40;

	private GridColumn gridColumn41;

	private GridColumn gridColumn42;

	private GridColumn gridColumn43;

	private GridColumn gridColumn19;

	private GridColumn gridColumn44;

	private Label label6;

	private System.Windows.Forms.ComboBox CBSaleTybe;

	public GridControl gridControl1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private RepositoryItemTextEdit repositoryItemTextEditTafqit;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private Label label7;

	private RepositoryItemButtonEdit repositoryItemButtonEdit7;

	private GridColumn gridColumn45;

	private GridColumn gridColumn46;

	private NumericEditBox TxtBounce;

	private Label LableBouns;

	private RepositoryItemButtonEdit repositoryItemButtonStores;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private RepositoryItemButtonEdit repositoryItemButtonEditUnit;

	private RepositoryItemButtonEdit repositoryItemButtonDelete;

	private ButtonEdit BtnDisCode;

	private Label label28;

	private GridColumn gridColumn47;

	private GridColumn gridColumn48;

	private GridColumn gridColumn49;

	private SimpleButton BtnTypeInvoic;

	private Label LValid;

	private Label LSucced;

	private SimpleButton BtnUpload;

	public CheckEdit CkSentInvoice;

	private Mode mode { get; set; }

	public FrmOrdersDraft(DataTable data)
	{
		InitializeComponent();
		Bind();
		SetInitialRow();
		TxtDigital();
		BtnDefault();
		GroupPermission();
		HusmAndBouns();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
		if (data != null && data.Rows.Count > 0)
		{
			if (data.Columns.Contains("UnitPurchasePrice"))
			{
				datatable = data;
				GetTotal2();
			}
			else
			{
				datatable = data;
				GetTotal();
			}
		}
		context = new ApplicationDbContext();
		DisCode = new DiscountCodesManager(context);
		if (LoginDetails.FrmOrderID > 0)
		{
			SearchOrdNum(Convert.ToInt32(LoginDetails.FrmOrderID));
		}
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "ItemCode"));
		decimal num = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "Quantity"));
		decimal num2 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "Height"));
		decimal num3 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "Width"));
		decimal num4 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "Long"));
		decimal num5 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "Weight"));
		if (text == string.Empty || num <= 0m || num4 <= 0m || num3 <= 0m || num2 <= 0m || num5 <= 0m)
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
			else if (num4 <= 0m)
			{
				MessageBox.Show("برجاء إدخال  ارتفاع أكبر من الصفر");
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.ShowEditor();
			}
			else if (num3 <= 0m)
			{
				MessageBox.Show("برجاء إدخال عرض أكبر  من الصفر");
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.ShowEditor();
			}
			else if (num2 <= 0m)
			{
				MessageBox.Show("برجاء إدخال طول أكبر من الصفر");
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.ShowEditor();
			}
			else if (num5 <= 0m)
			{
				MessageBox.Show("برجاء إدخال وزن أكبر من الصفر");
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.ShowEditor();
			}
		}
	}

	private void gridView1_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
	{
		e.ExceptionMode = ExceptionMode.NoAction;
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[1];
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
			if (e.Column.FieldName == "Net" || e.Column.FieldName == "Total")
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
			string codeItm = Convert.ToString(gridView1.GetFocusedRowCellDisplayText("ItemCode"));
			UnitName = gridView1.GetFocusedRowCellDisplayText("UnitName");
			string StoreName = gridView1.GetFocusedRowCellDisplayText("StoreName");
			StorId = (from x in gTSdbEntities.Account_Stores
				where x.Store_Name == StoreName
				select x.ID).FirstOrDefault();
			GetAllitems_alternatives(ItmID, StorId, UnitName);
			CodeItm = codeItm;
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("SN");
			if (!int.TryParse(focusedRowCellDisplayText2, out SN))
			{
				SN = 0;
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
		PrintAfterReturn();
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchOrdersDraft frmSearchOrdersDraft = new FrmSearchOrdersDraft();
		frmSearchOrdersDraft.ShowDialog();
		if (frmSearchOrdersDraft.CustCode > 0)
		{
			SearchOrdNum(frmSearchOrdersDraft.CustCode);
			Tafqit();
		}
	}

	private void TxtCust_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		using (new GTSdbEntities())
		{
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustID > 0)
			{
				SearchCustNum(frmSearchCustomer.CustID);
			}
		}
	}

	private void BtnOrderReturn_Click(object sender, EventArgs e)
	{
		FrmOrderInCashier frmOrderInCashier = new FrmOrderInCashier();
		frmOrderInCashier.TxtBarcode.Text = TxtCode.Text;
		frmOrderInCashier.ShowDialog();
	}

	private void BtnShowPrice_Click(object sender, EventArgs e)
	{
		GetPricePffer();
	}

	private void BtnRecipt_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 31))
		{
			new FrmReceipts().Show();
			Charge(Convert.ToInt32(LblSup.Text));
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnDisCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		DiscountCode();
	}

	private void BtnTypeInvoic_Click(object sender, EventArgs e)
	{
		new FrmInvoicType("Order").Show();
	}

	private void BtnUpload_Click(object sender, EventArgs e)
	{
		if (LSucced.Text == "Success")
		{
			MessageBox.Show("تم ارسال الفاتوره بالفعل", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			return;
		}
		int orderId = Convert.ToInt32(TxtCode.Text);
		int valueOrDefault = Zakat.GetIdByOrderId(orderId).GetValueOrDefault();
		SentToSazakt(valueOrDefault);
	}

	private void GetTotal2()
	{
		if (ORD.IsVatActive())
		{
			if (LoginDetails.ItemTotalWithVat)
			{
				DataTable dataTable = datatable.Copy();
				foreach (DataRow row in dataTable.Rows)
				{
					decimal num = ConvertToDecimalOrDefault(row["Quantity"]);
					decimal num2 = ConvertToDecimalOrDefault(row["Height"]);
					decimal num3 = ConvertToDecimalOrDefault(row["Width"]);
					decimal num4 = ConvertToDecimalOrDefault(row["Long"]);
					decimal num5 = ConvertToDecimalOrDefault(row["Weight"]);
					decimal num6 = ConvertToDecimalOrDefault(row["BounceItem"]);
					decimal num7 = num * num2 * num3 * num4 * num5;
					decimal num8 = (num + num6) * num2 * num3 * num4 * num5;
					decimal num9 = Convert.ToDecimal(row["UnitPrice"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
					decimal num10 = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(num9));
					if (Convert.ToInt32(row["ItemID"]) > 0)
					{
						row["UnitPrice"] = num9;
						row["ItemVat"] = FUNC_DecimalNum(num10 * num7);
						row["SumPrice"] = Convert.ToDecimal(row["UnitPrice"]) * num7;
						row["Total"] = Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]);
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
							row["SumPriceDisc"] = Convert.ToDecimal(row["UnitPriceDisc"]) * num7;
							row["ItemVatDisc"] = Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m);
							row["Net"] = Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]);
						}
					}
				}
				ApplyDecimalFunctionToDataTable(dataTable);
				datatable = dataTable;
			}
			else
			{
				DataTable dataTable2 = datatable.Copy();
				foreach (DataRow row2 in dataTable2.Rows)
				{
					decimal num11 = ConvertToDecimalOrDefault(row2["Quantity"]);
					decimal num12 = ConvertToDecimalOrDefault(row2["Height"]);
					decimal num13 = ConvertToDecimalOrDefault(row2["Width"]);
					decimal num14 = ConvertToDecimalOrDefault(row2["Long"]);
					decimal num15 = ConvertToDecimalOrDefault(row2["Weight"]);
					decimal num16 = ConvertToDecimalOrDefault(row2["BounceItem"]);
					decimal num17 = num11 * num12 * num13 * num14 * num15;
					decimal num18 = (num11 + num16) * num12 * num13 * num14 * num15;
					if (Convert.ToInt32(row2["ItemID"]) > 0)
					{
						row2["SumPrice"] = Convert.ToDecimal(row2["UnitPrice"]) * num17;
						row2["ItemVat"] = Convert.ToDecimal(row2["SumPrice"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m);
						row2["Total"] = Convert.ToDecimal(row2["SumPrice"]) + Convert.ToDecimal(row2["ItemVat"]);
						if (((Convert.ToDecimal(row2["AmountDisc"]) == 0m || row2["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row2["PercentDisc"]) == 0m) || row2["PercentDisc"].ToString() == string.Empty)
						{
							row2["UnitPriceDisc"] = row2["UnitPrice"];
							row2["ItemVatDisc"] = row2["ItemVat"];
							row2["SumPriceDisc"] = row2["SumPrice"];
							row2["Net"] = row2["Total"];
						}
						else
						{
							row2["UnitPriceDisc"] = Convert.ToDecimal(row2["UnitPrice"]) - Convert.ToDecimal(row2["UnitPrice"]) * (Convert.ToDecimal(row2["PercentDisc"]) / 100m);
							row2["SumPriceDisc"] = Convert.ToDecimal(row2["UnitPriceDisc"]) * num17;
							row2["ItemVatDisc"] = Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m);
							row2["Net"] = Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]);
						}
					}
				}
				ApplyDecimalFunctionToDataTable(dataTable2);
				datatable = dataTable2;
			}
		}
		else
		{
			DataTable dataTable3 = datatable.Copy();
			foreach (DataRow row3 in dataTable3.Rows)
			{
				decimal num19 = ConvertToDecimalOrDefault(row3["Quantity"]);
				decimal num20 = ConvertToDecimalOrDefault(row3["Height"]);
				decimal num21 = ConvertToDecimalOrDefault(row3["Width"]);
				decimal num22 = ConvertToDecimalOrDefault(row3["Long"]);
				decimal num23 = ConvertToDecimalOrDefault(row3["Weight"]);
				decimal num24 = ConvertToDecimalOrDefault(row3["BounceItem"]);
				decimal num25 = num19 * num20 * num21 * num22 * num23;
				decimal num26 = (num19 + num24) * num20 * num21 * num22 * num23;
				if (Convert.ToInt32(row3["ItemID"]) > 0)
				{
					row3["SumPrice"] = Convert.ToDecimal(row3["UnitPrice"]) * num25;
					row3["ItemVat"] = 0;
					row3["Total"] = row3["SumPrice"];
					if (((Convert.ToDecimal(row3["AmountDisc"]) == 0m || row3["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row3["PercentDisc"]) == 0m) || row3["PercentDisc"].ToString() == string.Empty)
					{
						row3["UnitPriceDisc"] = row3["UnitPrice"];
						row3["ItemVatDisc"] = row3["ItemVat"];
						row3["SumPriceDisc"] = row3["SumPrice"];
						row3["Net"] = row3["Total"];
					}
					else
					{
						row3["UnitPriceDisc"] = Convert.ToDecimal(row3["UnitPrice"]) - Convert.ToDecimal(row3["UnitPrice"]) * (Convert.ToDecimal(row3["PercentDisc"]) / 100m);
						row3["SumPriceDisc"] = Convert.ToDecimal(row3["UnitPriceDisc"]) * num25;
						row3["ItemVatDisc"] = row3["ItemVat"];
						row3["Net"] = row3["SumPriceDisc"];
					}
				}
			}
			ApplyDecimalFunctionToDataTable(dataTable3);
			datatable = dataTable3;
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num27 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num27);
		decimal num28 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		decimal num29 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num28.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = num29.ToString();
		TxtNet.Text = (num27 + num29).ToString();
		gridControl1.DataSource = datatable;
		IsEnableFalse();
	}

	private void GetTotal()
	{
		gridControl1.DataSource = datatable;
		decimal num = Convert.ToDecimal(datatable.Compute("SUM(SumPrice)", string.Empty));
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num);
		TxtTotalDiscount.Text = Convert.ToString(num);
		TxtNet.Text = Convert.ToString(num2);
		TxtVat.Text = Convert.ToString(num3);
		TxtVatDisc.Text = Convert.ToString(num4);
	}

	private void BtnDefault()
	{
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 12);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 12);
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
			BtnOrderReturn.Visible = false;
			BtnShowPrice.Visible = false;
			BtnRecipt.Visible = false;
		}
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtDate.DateTime = DateTime.Now;
		DateHold.DateTime = DateTime.Now;
		TxtCode.Focus();
		CkSentInvoice.Checked = LoginDetails.SentInvoice;
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
					TxtAddress.Text = account_CustSup.Address;
					TxtStreet.Text = account_CustSup.Street;
					TxtDistrict.Text = account_CustSup.District;
					CreditLimit = account_CustSup.CreditLimit;
					BtnCustomerSearch.Text = account_CustSup.ID.ToString();
					Charge(ID);
				}
				else
				{
					BtnCustomerSearch.Text = string.Empty;
				}
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	public void Bind()
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

	public void StockLimit(int ItemID, int StoreID)
	{
		decimal num = 0m;
		decimal num2 = 0m;
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		Item_Items item_Items = gTSdbEntities.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault();
		if (item_Items != null)
		{
			num = Convert.ToDecimal(item_Items.SmallUnitQuantity);
		}
		if (num != 0m)
		{
			ItemQuantity itemQuantity = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
			if (itemQuantity != null)
			{
				num2 = Convert.ToDecimal(itemQuantity.CurrentBalance);
			}
			if (num2 <= num)
			{
				MessageBox.Show("تنبيه، حد الطلب ( " + FUNC_DecimalNumQty(num) + " ) بالبرنامج \r\n والكمية بالمخزن  ( " + FUNC_DecimalNumQty(num2) + " ) ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

	private void ApplyDecimalFunctionToDataTable(DataTable table)
	{
		for (int i = 0; i < table.Rows.Count; i++)
		{
			DataRow dataRow = table.Rows[i];
			for (int j = 0; j < table.Columns.Count; j++)
			{
				DataColumn dataColumn = table.Columns[j];
				if ((dataColumn.DataType == typeof(decimal) || dataColumn.DataType == typeof(double)) && dataRow[dataColumn] != DBNull.Value)
				{
					dataRow[dataColumn] = FUNC_DecimalNum(Convert.ToDecimal(dataRow[dataColumn]));
				}
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
		if (dataRow.Table.Columns.Contains("TafqitID"))
		{
			dataRow["TafqitID"] = 1;
		}
		if (dataRow.Table.Columns.Contains("UnitNameTF"))
		{
			dataRow["UnitNameTF"] = string.Empty;
		}
		if (dataRow.Table.Columns.Contains("QuantityTF"))
		{
			dataRow["QuantityTF"] = 1;
		}
		dataRow["Quantity"] = 1;
		dataRow["BounceItem"] = 0;
		dataRow["Height"] = 1;
		dataRow["Width"] = 1;
		dataRow["Long"] = 1;
		dataRow["Weight"] = 1;
		dataRow["UnitNum"] = 0;
		if (dataRow.Table.Columns.Contains("SumInstallationDis") && dataRow.Table.Columns.Contains("InstallationPrice"))
		{
			dataRow["SumInstallationDis"] = 0;
			dataRow["InstallationPrice"] = 0;
		}
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
		if (dataRow.Table.Columns.Contains("AvergItem"))
		{
			dataRow["AvergItem"] = 0;
		}
		if (dataRow.Table.Columns.Contains("SumAvergItem"))
		{
			dataRow["SumAvergItem"] = 0;
		}
		if (dataRow.Table.Columns.Contains("Profit"))
		{
			dataRow["Profit"] = 0;
		}
		if (dataRow.Table.Columns.Contains("IsItemTax"))
		{
			dataRow["IsItemTax"] = false;
		}
		dataRow["Tax_Value"] = 0;
		if (dataRow.Table.Columns.Contains("DeleteItem"))
		{
			dataRow["DeleteItem"] = 1;
		}
		if (dataRow.Table.Columns.Contains("Done"))
		{
			dataRow["Done"] = 0;
		}
		datatable.Rows.Add(dataRow);
		ArrangeDatatable();
		gridControl1.DataSource = datatable;
	}

	private void Taffit(string Code)
	{
		Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(Code).FirstOrDefault();
		DataTable dataTable = datatable.Copy();
		foreach (DataRow row in dataTable.Rows)
		{
			int num = ConvertToDecimalOrDefaultInt(row["SN"]);
			if (SN < 1)
			{
				SN = 1;
			}
			if (num == SN)
			{
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
				{
					row["TafqitID"] = get_Item_ByCodeALL_Result.UnitMediumID;
					row["UnitNameTF"] = get_Item_ByCodeALL_Result.UnitMedium;
					row["QuantityTF"] = 1;
					row["Quantity"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.ConvertMediumUnit);
				}
				else if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
				{
					row["TafqitID"] = get_Item_ByCodeALL_Result.UnitLargeID;
					row["UnitNameTF"] = get_Item_ByCodeALL_Result.UnitLarge;
					row["QuantityTF"] = 1;
					row["Quantity"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.ConvertBigUnit);
				}
				decimal unitPrice = ((!LoginDetails.ItemTotalWithVat) ? Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall) : (Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall) / Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100 + (decimal?)1)));
				row["IsItemTax"] = true;
				UpdateRowFactionAccount(Convert.ToDecimal(row["Quantity"]), unitPrice, 1m, 1m, 1m, 1m, 0m, IsDisc: false, IsDiscAmount: false, 0m, 0m, Convert.ToDecimal(get_Item_ByCodeALL_Result.Average_cost), 0m, 1m);
			}
		}
	}

	private decimal GetUnitSellPrice(dynamic item, string matchedField, int saleType)
	{
		string sizeType = GetSizeType(matchedField);
		decimal result = 0m;
		switch (sizeType)
		{
		case "Small":
			switch (saleType)
			{
			case 1:
				return Convert.ToDecimal(item.SellPriceSmall);
			case 2:
				return Convert.ToDecimal(item.SellPriceSmall2);
			case 3:
				return Convert.ToDecimal(item.SellPriceSmall3);
			}
			break;
		case "Medium":
			switch (saleType)
			{
			case 1:
				return Convert.ToDecimal(item.SellPriceMedium);
			case 2:
				return Convert.ToDecimal(item.SellPriceMedium2);
			case 3:
				return Convert.ToDecimal(item.SellPriceMedium3);
			}
			break;
		case "Large":
			switch (saleType)
			{
			case 1:
				return Convert.ToDecimal(item.SellpriceLarge);
			case 2:
				return Convert.ToDecimal(item.SellpriceLarge2);
			case 3:
				return Convert.ToDecimal(item.SellpriceLarge3);
			}
			break;
		}
		return result;
	}

	private string GetSizeType(string field)
	{
		switch (field)
		{
		default:
			if (!(field == "SmallBarCode3"))
			{
				if (field == "MediumBarCode1" || field == "MediumBarCode2" || field == "MediumBarCode3")
				{
					return "Medium";
				}
				if (field == "BigBarCode1" || field == "BigBarCode2" || field == "BigBarCode3")
				{
					return "Large";
				}
				return string.Empty;
			}
			goto case "Item_code";
		case "Item_code":
		case "SmallBarCode1":
		case "SmallBarCode2":
			return "Small";
		}
	}

	public void InsertItemRow(CellValueChangedEventArgs e)
	{
		string code = Convert.ToString(e.Value);
		ItemMatchResult itemsOnly = ORD.GetItemsOnly(code);
		Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(code).FirstOrDefault();
		if (get_Item_ByCodeALL_Result != null)
		{
			DataTable dataTable = datatable.Copy();
			foreach (DataRow row in dataTable.Rows)
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
				row["VatCode"] = get_Item_ByCodeALL_Result.VatCode;
				row["TaxExemptionReason"] = get_Item_ByCodeALL_Result.TaxExemptionReason;
				row["TaxExemptionReasonCode"] = get_Item_ByCodeALL_Result.TaxExemptionReasonCode;
				row["QuantityTF"] = 1;
				row["Quantity"] = 1;
				row["BounceItem"] = 0;
				row["Height"] = 1;
				row["Width"] = 1;
				row["Long"] = 1;
				row["Weight"] = 1;
				row["UnitNum"] = 0;
				if (LoginDetails.InstallAll == 0)
				{
					row["InstallationPrice"] = 0;
					row["SumInstallationDis"] = 0;
				}
				else
				{
					row["InstallationPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.PriceExportunit1);
					row["SumInstallationDis"] = row["InstallationPrice"];
				}
				row["StoreName"] = Comp_Store.GetItemText(Comp_Store.SelectedItem);
				row["StoreID"] = Comp_Store.SelectedValue;
				string matchedField = itemsOnly.MatchedField;
				int saleType = Convert.ToInt32(CBSaleTybe.SelectedValue);
				decimal unitSellPrice = GetUnitSellPrice(get_Item_ByCodeALL_Result, matchedField, saleType);
				row["UnitPrice"] = unitSellPrice;
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
						row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(unitSellPrice) + Convert.ToDecimal(row["SumInstallationDis"]));
						row["SumPrice"] = Convert.ToDecimal(unitSellPrice) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
						row["UnitPrice"] = row["SumPrice"];
						row["ItemVat"] = Convert.ToDecimal(unitSellPrice) - Convert.ToDecimal(row["SumPrice"]);
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
				row["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost;
				row["SumAvergItem"] = row["AvergItem"];
				row["Profit"] = Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]);
				row["DeleteItem"] = 0;
				row["Done"] = 1;
			}
			ApplyDecimalFunctionToDataTable(dataTable);
			datatable = dataTable;
		}
		if (get_Item_ByCodeALL_Result.UnitTafqitID > 1)
		{
			Taffit(code);
		}
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
			if (TxtChange == "UnitNum")
			{
				break;
			}
			decimal num2 = ((TxtChange == "Quantity") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"])));
			decimal num4;
			if (TxtChange == "Quantity")
			{
				decimal num3 = 1m;
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(row["ItemCode"].ToString()).FirstOrDefault();
				if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
				{
					num3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertMediumUnit);
				}
				else if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
				{
					num3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.ConvertBigUnit);
				}
				num4 = FUNC_DecimalNum(num2 / num3);
				row["QuantityTF"] = num4;
			}
			decimal unitPrice = ((TxtChange == "UnitPrice") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"])));
			if (TxtChange == "UnitPrice" && LoginDetails.ItemTotalWithVat)
			{
				row["OrginalPrice"] = num * (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
			}
			decimal num5 = ((TxtChange == "Height") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Height"])));
			decimal num6 = ((TxtChange == "Width") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Width"])));
			decimal num7 = ((TxtChange == "Long") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Long"])));
			decimal weight = ((TxtChange == "Weight") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["Weight"])));
			decimal bounceItem = ((TxtChange == "BounceItem") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["BounceItem"])));
			decimal num8 = ((TxtChange == "PercentDisc") ? FUNC_DecimalNum(num) : FUNC_DecimalNum(Convert.ToDecimal(row["PercentDisc"])));
			if (num8 > 100m)
			{
				break;
			}
			flag2 = TxtChange == "PercentDisc" || TxtChange == "AmountDisc";
			decimal amountDisc = ((TxtChange == "AmountDisc") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["AmountDisc"])));
			flag = TxtChange == "AmountDisc";
			decimal avergItem = ((TxtChange == "AvergItem") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"])));
			decimal installationPrice = ((TxtChange == "InstallationPrice") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"])));
			num4 = ((TxtChange == "QuantityTF") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["QuantityTF"])));
			if (TxtChange == "QuantityTF")
			{
				decimal num9 = 1m;
				Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result2 = ITM.GetItemByCode(row["ItemCode"].ToString()).FirstOrDefault();
				if (get_Item_ByCodeALL_Result2.UnitTafqitID == 2)
				{
					num9 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.ConvertMediumUnit);
				}
				else if (get_Item_ByCodeALL_Result2.UnitTafqitID == 3)
				{
					num9 = Convert.ToDecimal(get_Item_ByCodeALL_Result2.ConvertBigUnit);
				}
				row["Quantity"] = FUNC_DecimalNum(num9 * num);
				num2 = Convert.ToDecimal(row["Quantity"]);
			}
			UpdateRowFactionAccount(num2, unitPrice, num5, num6, num7, weight, bounceItem, flag2, flag, num8, amountDisc, avergItem, installationPrice, num4);
		}
	}

	public void UpdateRowFactionAccount(decimal Quantity, decimal UnitPrice, decimal Height, decimal Width, decimal Long, decimal Weight, decimal BounceItem, bool IsDisc, bool IsDiscAmount, decimal PercentDisc, decimal AmountDisc, decimal AvergItem, decimal InstallationPrice, decimal QuantityTF)
	{
		decimal num = Quantity * Height * Width * Long * Weight;
		decimal num2 = (Quantity + BounceItem) * Height * Width * Long * Weight;
		decimal num3 = UnitPrice * num;
		foreach (DataRow row in datatable.Rows)
		{
			int num4 = ConvertToDecimalOrDefaultInt(row["SN"]);
			if (num4 != SN)
			{
				continue;
			}
			bool flag = Convert.ToBoolean(row["IsItemTax"]);
			row["Quantity"] = Quantity;
			row["QuantityTF"] = QuantityTF;
			row["BounceItem"] = BounceItem;
			row["Height"] = Height;
			row["Width"] = Width;
			row["Long"] = Long;
			row["Weight"] = Weight;
			row["UnitPrice"] = FUNC_DecimalNum(UnitPrice);
			row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
			row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
			row["AvergItem"] = AvergItem;
			row["InstallationPrice"] = InstallationPrice;
			if (PercentDisc > 0m)
			{
				IsDisc = true;
			}
			if (!LoginDetails.IsVat)
			{
				row["SumPrice"] = (UnitPrice + InstallationPrice) * num;
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (IsDisc && IsDiscAmount)
				{
					PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(num3) * 100m);
					row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
				}
				else if (IsDisc && !IsDiscAmount)
				{
					AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(num3));
					row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
					row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * num3 / 100m);
				}
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["AmountDisc"]));
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (PercentDisc / 100m));
				row["SumInstallationDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]) - Convert.ToDecimal(row["InstallationPrice"]) * (PercentDisc / 100m));
				row["ItemVatDisc"] = 0;
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
			}
			else if (flag)
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num5 = FUNC_DecimalNum(Convert.ToDecimal(row["Tax_Value"]) / 100m);
					row["SumPrice"] = Math.Round((UnitPrice + InstallationPrice) * num, DecimalNum);
					decimal num6 = Convert.ToDecimal(row["SumPrice"]);
					row["ItemVat"] = Convert.ToDecimal(row["SumPrice"]) * num5;
					if (IsDisc && IsDiscAmount)
					{
						PercentDisc = FUNC_DecimalNum(AmountDisc / (UnitPrice * num) * 100m);
						row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
					}
					else if (IsDisc && !IsDiscAmount)
					{
						AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(row["SumPrice"]));
						row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
						row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * (UnitPrice * num) / 100m);
					}
					if (IsDisc)
					{
						row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["AmountDisc"]));
						row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) - Convert.ToDecimal(row["UnitPriceDisc"]) * (PercentDisc / 100m));
						row["ItemVatDisc"] = Math.Truncate(Convert.ToDecimal(row["SumPriceDisc"]) * num5);
						row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
					}
					else
					{
						row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["AmountDisc"]));
						row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) - Convert.ToDecimal(row["UnitPriceDisc"]) * (PercentDisc / 100m));
						if (Convert.ToDecimal(row["SumPriceDisc"]) < 10m)
						{
							row["ItemVatDisc"] = Math.Round(Convert.ToDecimal(row["SumPriceDisc"]) * num5, 2);
							row["Total"] = Math.Round(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]), 21);
						}
						else
						{
							row["ItemVatDisc"] = Math.Round(Convert.ToDecimal(row["SumPriceDisc"]) * num5, 2);
							decimal num7 = Convert.ToDecimal(row["ItemVatDisc"]);
							decimal num8 = Convert.ToDecimal(row["SumPriceDisc"]);
							decimal num9 = Math.Round(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]), 2);
							if (num9 - Math.Floor(num9) < 0.1m)
							{
								num9 = Math.Floor(num9);
							}
							decimal num10 = Convert.ToDecimal(row["AmountDisc"]);
							if (num10 <= 0m)
							{
								decimal num11 = FUNC_DecimalNum(UnitPrice * (1m + num5));
								num9 = num11 * Quantity;
							}
							row["Total"] = num9;
						}
						row["Net"] = Math.Round(Convert.ToDecimal(row["Total"]), 2);
						decimal num12 = Convert.ToDecimal(row["Net"]);
					}
				}
				else
				{
					row["SumPrice"] = (UnitPrice + InstallationPrice) * num;
					row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					if (IsDisc && IsDiscAmount)
					{
						PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(num3) * 100m);
						row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
					}
					else if (IsDisc && !IsDiscAmount)
					{
						AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(num3));
						row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
						row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * (UnitPrice * num) / 100m);
					}
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["AmountDisc"]));
					decimal num13 = Convert.ToDecimal(row["SumPriceDisc"]);
					row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) - Convert.ToDecimal(row["UnitPriceDisc"]) * (PercentDisc / 100m));
					row["SumInstallationDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]) - Convert.ToDecimal(row["InstallationPrice"]) * (PercentDisc / 100m));
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					decimal num14 = Convert.ToDecimal(row["ItemVatDisc"]);
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]));
					decimal num15 = Convert.ToDecimal(row["Net"]);
				}
			}
			else
			{
				row["SumPrice"] = (UnitPrice + InstallationPrice) * num;
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (IsDisc && IsDiscAmount)
				{
					PercentDisc = FUNC_DecimalNum(AmountDisc / Convert.ToDecimal(num3) * 100m);
					row["PercentDisc"] = FUNC_DecimalNum(PercentDisc);
				}
				else if (IsDisc && !IsDiscAmount)
				{
					AmountDisc = FUNC_DecimalNum(PercentDisc / 100m * Convert.ToDecimal(num3));
					row["AmountDisc"] = FUNC_DecimalNum(AmountDisc);
					row["AmountDisc"] = FUNC_DecimalNum(PercentDisc * num3 / 100m);
				}
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["AmountDisc"]));
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (PercentDisc / 100m));
				row["SumInstallationDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]) - Convert.ToDecimal(row["InstallationPrice"]) * (PercentDisc / 100m));
				row["ItemVatDisc"] = 0;
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
			}
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num2);
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
			decimal num3 = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]) * Convert.ToDecimal(row["quantity"]));
			decimal num4 = Convert.ToDecimal(row["InstallationPrice"]);
			if (num <= num3)
			{
				break;
			}
			if (!LoginDetails.IsVat)
			{
				row["Net"] = num;
				row["SumPrice"] = num;
				row["SumPriceDisc"] = num;
				row["Total"] = num;
				row["UnitPrice"] = (num - num3) / Convert.ToDecimal(row["Quantity"]);
				row["OrginalPrice"] = row["UnitPrice"];
				row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPrice"]);
				row["ItemVat"] = 0;
				row["ItemVatDisc"] = 0;
				row["AmountDisc"] = 0;
				row["PercentDisc"] = 0;
				row["SumInstallationDis"] = row["InstallationPrice"];
				TxtDiscount.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtAllDiscount.Text = "0.00";
				TxtVat.Text = "0.00";
				TxtVatDisc.Text = "0.00";
				decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
				TxtTotals.Text = Convert.ToString(num5);
				TxtTotalDiscount.Text = TxtTotals.Text;
				TxtNet.Text = TxtTotals.Text.ToString();
			}
			else if (Convert.ToBoolean(row["IsItemTax"]))
			{
				decimal num6 = num / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m);
				decimal num7 = (num6 - num3) / Convert.ToDecimal(row["Quantity"]);
				row["Net"] = num;
				row["ItemVat"] = num - num6;
				row["SumPrice"] = num6;
				row["UnitPrice"] = num7;
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

	public void ArrangeDatatable()
	{
		int num = 1;
		foreach (DataRow row in datatable.Rows)
		{
			row["SN"] = num;
			num++;
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
		datatable.Columns.Add("InstallationPrice", typeof(decimal));
		datatable.Columns.Add("SumInstallationDis", typeof(decimal));
		datatable.Columns.Add("VatCode", typeof(string));
		datatable.Columns.Add("TaxExemptionReason", typeof(string));
		datatable.Columns.Add("TaxExemptionReasonCode", typeof(string));
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
		dtCurrentTable.Columns.Add("InstallationPrice", typeof(decimal));
		dtCurrentTable.Columns.Add("SumInstallationDis", typeof(decimal));
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

	public decimal IsItemPriceZero()
	{
		decimal num = 1m;
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["ItemCode"]);
			if (!(text != string.Empty))
			{
				continue;
			}
			int num2 = 0;
			num2 = Convert.ToInt32(row["ItemID"]);
			int itmIDType = ORD.GetItmIDType(num2);
			if (itmIDType != 2)
			{
				string value = Convert.ToString(row["UnitPrice"]);
				string text2 = Convert.ToString(row["ItemName"]);
				if (string.IsNullOrWhiteSpace(value))
				{
					row["UnitPrice"] = 0;
				}
				decimal num3 = Convert.ToDecimal(row["Quantity"]);
				string type = Convert.ToString(row["UnitType"]);
				int storeID = Convert.ToInt32(row["StoreID"]);
				int itemID = Convert.ToInt32(ORD.GetItmID(text));
				num = ORD.GetQuantityPermission(itemID, storeID, type);
				if (LoginDetails.IsQuantityMinZero)
				{
					num = 1m;
					break;
				}
				if (num <= 0m)
				{
					MessageBox.Show("نعتذر، لا يوجد كمية كافية من الصنف ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					num = 0m;
					break;
				}
				if (num3 > num)
				{
					MessageBox.Show("نعتذر، الكمية المطلوبة اكبر من الكمية الموجودة  ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					num = 0m;
					break;
				}
			}
		}
		return num;
	}

	public decimal IsItemPriceZeroUpdate()
	{
		decimal num = 1m;
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["ItemCode"]);
			string value = Convert.ToString(row["UnitPrice"]);
			string text2 = Convert.ToString(row["ItemName"]);
			if (string.IsNullOrWhiteSpace(value))
			{
				row["UnitPrice"] = "0";
			}
			if (Convert.ToDecimal(row["UnitPrice"]) <= 0m && text != string.Empty)
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ الفاتورة لوجود اصناف سعر الوحدة تساوي صفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				break;
			}
			if (text != string.Empty && !LoginDetails.IsQuantityMinZero)
			{
				decimal num2 = Convert.ToDecimal(row["Quantity"]);
				string type = Convert.ToString(row["UnitType"]);
				int storeID = Convert.ToInt32(row["StoreID"]);
				int itemID = Convert.ToInt32(ORD.GetItmID(text));
				num = ORD.GetQuantityPermission(itemID, storeID, type) + ORD.GetQuantityPermissionReturn(itemID, Convert.ToInt32(TxtCode.Text));
				if (num <= 0m)
				{
					MessageBox.Show("نعتذر، لا يوجد كمية كافية من الصنف ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					num = 0m;
					break;
				}
				if (num2 > num)
				{
					MessageBox.Show("نعتذر، الكمية المطلوبة اكبر من الكمية الموجودة  ( " + text2 + " ) في المخزن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					num = 0m;
					break;
				}
			}
		}
		return num;
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
			Tafqit();
		}
		catch (Exception)
		{
		}
	}

	public decimal ConvertToDecimalOrDefault(object value)
	{
		return (value != DBNull.Value) ? Convert.ToDecimal(value) : 1m;
	}

	public int ConvertToDecimalOrDefaultInt(object value)
	{
		return (value != DBNull.Value) ? Convert.ToInt32(value) : 0;
	}

	private void No_Discount_NoVat()
	{
		DataTable dataTable = datatable.Copy();
		foreach (DataRow row in dataTable.Rows)
		{
			decimal num = ConvertToDecimalOrDefault(row["Quantity"]);
			decimal num2 = ConvertToDecimalOrDefault(row["Height"]);
			decimal num3 = ConvertToDecimalOrDefault(row["Width"]);
			decimal num4 = ConvertToDecimalOrDefault(row["Long"]);
			decimal num5 = ConvertToDecimalOrDefault(row["Weight"]);
			decimal num6 = ConvertToDecimalOrDefault(row["BounceItem"]);
			decimal num7 = num * num2 * num3 * num4 * num5;
			decimal num8 = (num + num6) * num2 * num3 * num4 * num5;
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				if (LoginDetails.InstallAll == 2)
				{
					row["SumInstallationDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]));
				}
				else if (LoginDetails.InstallAll == 1)
				{
					row["SumInstallationDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]) * num8);
				}
				else
				{
					row["SumInstallationDis"] = 0;
				}
				row["SumPrice"] = Convert.ToDecimal(row["UnitPrice"]) * num7 + Convert.ToDecimal(row["SumInstallationDis"]);
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
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num7 + Convert.ToDecimal(row["SumInstallationDis"]));
					row["ItemVatDisc"] = row["ItemVat"];
					row["Net"] = row["SumPriceDisc"];
				}
				row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * num8;
				row["Profit"] = Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]);
			}
		}
		ApplyDecimalFunctionToDataTable(dataTable);
		datatable = dataTable;
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num9 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num9);
		TxtTotalDiscount.Text = TxtTotals.Text;
		decimal num10 = FUNC_DecimalNum(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text));
		decimal num11 = num10 * (BounnsPre / 100m);
		TxtBounce.Text = num11.ToString();
		decimal num12 = num9 + num11;
		TxtNet.Text = num12.ToString();
	}

	private void No_Discount_Vat()
	{
		DataTable dataTable = datatable.Copy();
		foreach (DataRow row in dataTable.Rows)
		{
			decimal num = ConvertToDecimalOrDefault(row["Quantity"]);
			decimal num2 = ConvertToDecimalOrDefault(row["Height"]);
			decimal num3 = ConvertToDecimalOrDefault(row["Width"]);
			decimal num4 = ConvertToDecimalOrDefault(row["Long"]);
			decimal num5 = ConvertToDecimalOrDefault(row["Weight"]);
			decimal num6 = ConvertToDecimalOrDefault(row["BounceItem"]);
			decimal num7 = num * num2 * num3 * num4 * num5;
			decimal num8 = (num + num6) * num2 * num3 * num4 * num5;
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			if (LoginDetails.InstallAll == 2)
			{
				row["SumInstallationDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]));
			}
			else if (LoginDetails.InstallAll == 1)
			{
				row["SumInstallationDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]) * num8);
			}
			else
			{
				row["SumInstallationDis"] = 0;
			}
			row["SumPrice"] = Convert.ToDecimal(row["UnitPrice"]) * num7 + Convert.ToDecimal(row["SumInstallationDis"]);
			decimal num9 = Convert.ToDecimal(row["SumPrice"]);
			if (Convert.ToBoolean(row["IsItemTax"]))
			{
				row["ItemVat"] = Convert.ToDecimal(row["SumPrice"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m);
			}
			else
			{
				row["ItemVat"] = 0;
			}
			decimal num10 = Convert.ToDecimal(row["ItemVat"]);
			row["Total"] = Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["ItemVat"]);
			decimal num11 = Convert.ToDecimal(row["Total"]);
			if (num11 > 100m)
			{
				row["Total"] = Math.Round(num11).ToString("F2");
			}
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
				row["SumPriceDisc"] = Convert.ToDecimal(row["UnitPriceDisc"]) * num7 + Convert.ToDecimal(row["SumInstallationDis"]);
				if (Convert.ToBoolean(row["IsItemTax"]))
				{
					row["ItemVatDisc"] = Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m);
				}
				else
				{
					row["ItemVatDisc"] = 0;
				}
				row["Net"] = Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]);
			}
			row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * num8;
			row["Profit"] = Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]);
		}
		ApplyDecimalFunctionToDataTable(dataTable);
		datatable = dataTable;
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num12 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		TxtCost.Text = Convert.ToString(num12);
		decimal num13 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num13);
		decimal num14 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		decimal num15 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num14.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = num15.ToString();
		decimal num16 = FUNC_DecimalNum(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text));
		decimal num17 = num16 * (BounnsPre / 100m);
		TxtBounce.Text = num17.ToString();
		decimal num18 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
		TxtNet.Text = (num18 + num17).ToString();
	}

	private void No_Discount_PriceWithVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = ConvertToDecimalOrDefault(row["Quantity"]);
			decimal num2 = ConvertToDecimalOrDefault(row["Height"]);
			decimal num3 = ConvertToDecimalOrDefault(row["Width"]);
			decimal num4 = ConvertToDecimalOrDefault(row["Long"]);
			decimal num5 = ConvertToDecimalOrDefault(row["Weight"]);
			decimal num6 = ConvertToDecimalOrDefault(row["BounceItem"]);
			decimal num7 = num * num2 * num3 * num4 * num5;
			decimal num8 = (num + num6) * num2 * num3 * num4 * num5;
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			row["UnitPrice"] = Math.Round(Convert.ToDecimal(row["UnitPrice"]), 2);
			if (LoginDetails.InstallAll == 2)
			{
				row["SumInstallationDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]));
			}
			else if (LoginDetails.InstallAll == 1)
			{
				row["SumInstallationDis"] = FUNC_DecimalNum(Convert.ToDecimal(row["InstallationPrice"]) * num8);
			}
			else
			{
				row["SumInstallationDis"] = 0;
			}
			decimal num9 = ((row["Total"] != DBNull.Value) ? Convert.ToDecimal(row["Total"]) : 0m);
			decimal num10 = ((row["Tax_Value"] != DBNull.Value) ? Convert.ToDecimal(row["Tax_Value"]) : 0m);
			decimal value = num9 / (num10 / 100m + 1m);
			row["SumPrice"] = FUNC_DecimalNum(value);
			if (Convert.ToBoolean(row["IsItemTax"]))
			{
				row["ItemVat"] = FUNC_DecimalNum(num9 - Convert.ToDecimal(row["SumPrice"]));
			}
			else
			{
				row["ItemVat"] = 0;
			}
			if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]));
				row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]));
				row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]));
				decimal num11 = Convert.ToDecimal(row["Net"]);
			}
			else
			{
				row["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m));
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num7 + Convert.ToDecimal(row["SumInstallationDis"]));
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
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num8);
			row["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]));
		}
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num12 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		TxtCost.Text = Convert.ToString(num12);
		decimal num13 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num13);
		decimal num14 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num14.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = TxtVat.Text;
		decimal num15 = FUNC_DecimalNum(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text));
		decimal num16 = num15 * (BounnsPre / 100m);
		TxtBounce.Text = num16.ToString();
		decimal num17 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
		TxtNet.Text = (num17 + num16).ToString();
	}

	private void DiscountPercent_NoVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = ConvertToDecimalOrDefault(row["Quantity"]);
			decimal num2 = ConvertToDecimalOrDefault(row["Height"]);
			decimal num3 = ConvertToDecimalOrDefault(row["Width"]);
			decimal num4 = ConvertToDecimalOrDefault(row["Long"]);
			decimal num5 = ConvertToDecimalOrDefault(row["Weight"]);
			decimal num6 = ConvertToDecimalOrDefault(row["BounceItem"]);
			decimal num7 = num * num2 * num3 * num4 * num5;
			decimal num8 = (num + num6) * num2 * num3 * num4 * num5;
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num7);
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
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num7);
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]));
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num8);
				row["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]));
			}
		}
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num9 = Convert.ToDecimal(datatable.Compute("SUM(SumInstallationDis)", string.Empty));
		decimal num10 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num10 + num9);
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
			decimal num11 = ConvertToDecimalOrDefault(row2["Quantity"]);
			decimal num12 = ConvertToDecimalOrDefault(row2["Height"]);
			decimal num13 = ConvertToDecimalOrDefault(row2["Width"]);
			decimal num14 = ConvertToDecimalOrDefault(row2["Long"]);
			decimal num15 = ConvertToDecimalOrDefault(row2["Weight"]);
			decimal num16 = ConvertToDecimalOrDefault(row2["BounceItem"]);
			decimal num17 = num11 * num12 * num13 * num14 * num15;
			decimal num18 = (num11 + num16) * num12 * num13 * num14 * num15;
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPrice"]) * num17);
				row2["ItemVat"] = 0;
				row2["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPrice"]));
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num17);
				row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["ItemVat"]));
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num18);
				row2["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) - Convert.ToDecimal(row2["SumAvergItem"]));
			}
		}
		if (Convert.ToDecimal(TxtBounce.Text) > 0m)
		{
			decimal num19 = FUNC_DecimalNum(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text));
			decimal num20 = num19 * (BounnsPre / 100m);
			TxtBounce.Text = num20.ToString();
		}
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text) + Convert.ToDecimal(TxtBounce.Text)).ToString();
	}

	private void DiscountPercent_Vat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = ConvertToDecimalOrDefault(row["Quantity"]);
			decimal num2 = ConvertToDecimalOrDefault(row["Height"]);
			decimal num3 = ConvertToDecimalOrDefault(row["Width"]);
			decimal num4 = ConvertToDecimalOrDefault(row["Long"]);
			decimal num5 = ConvertToDecimalOrDefault(row["Weight"]);
			decimal num6 = ConvertToDecimalOrDefault(row["BounceItem"]);
			decimal num7 = num * num2 * num3 * num4 * num5;
			decimal num8 = (num + num6) * num2 * num3 * num4 * num5;
			if (Convert.ToInt32(row["ItemID"]) <= 0)
			{
				continue;
			}
			row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num7);
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
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num7);
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
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num8);
			row["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]));
		}
		decimal num9 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat.Text = Convert.ToString(num9);
		decimal num10 = Convert.ToDecimal(datatable.Compute("SUM(SumInstallationDis)", string.Empty));
		decimal num11 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num11 + num10);
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(num11) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(num11) + num9) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(num11)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(num11) + num9) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(num11) + num9) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(num11)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num12 = ConvertToDecimalOrDefault(row2["Quantity"]);
			decimal num13 = ConvertToDecimalOrDefault(row2["Height"]);
			decimal num14 = ConvertToDecimalOrDefault(row2["Width"]);
			decimal num15 = ConvertToDecimalOrDefault(row2["Long"]);
			decimal num16 = ConvertToDecimalOrDefault(row2["Weight"]);
			decimal num17 = ConvertToDecimalOrDefault(row2["BounceItem"]);
			decimal num18 = num12 * num13 * num14 * num15 * num16;
			decimal num19 = (num12 + num17) * num13 * num14 * num15 * num16;
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				row2["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) * num18) + Convert.ToDecimal(row2["SumInstallationDis"]);
				if (Convert.ToBoolean(row2["IsItemTax"]))
				{
					row2["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m));
				}
				else
				{
					row2["ItemVatDisc"] = 0;
				}
				row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]));
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num19);
				row2["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) - Convert.ToDecimal(row2["SumAvergItem"]));
			}
		}
		decimal num20 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVatDisc.Text = Convert.ToString(num20);
		if (Convert.ToDecimal(TxtBounce.Text) > 0m)
		{
			decimal num21 = FUNC_DecimalNum(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text));
			decimal num22 = num21 * (BounnsPre / 100m);
			TxtBounce.Text = num22.ToString();
		}
		TxtNet.Text = (Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text) + Convert.ToDecimal(TxtBounce.Text)).ToString();
	}

	private void DiscountPercent_PriceWithVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = ConvertToDecimalOrDefault(row["Quantity"]);
			decimal num2 = ConvertToDecimalOrDefault(row["Height"]);
			decimal num3 = ConvertToDecimalOrDefault(row["Width"]);
			decimal num4 = ConvertToDecimalOrDefault(row["Long"]);
			decimal num5 = ConvertToDecimalOrDefault(row["Weight"]);
			decimal num6 = ConvertToDecimalOrDefault(row["BounceItem"]);
			decimal num7 = num * num2 * num3 * num4 * num5;
			decimal num8 = (num + num6) * num2 * num3 * num4 * num5;
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
			row["UnitPrice"] = FUNC_DecimalNum((Convert.ToDecimal(row["SumPrice"]) - Convert.ToDecimal(row["SumInstallationDis"])) / num7);
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
				row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num7) + Convert.ToDecimal(row["SumInstallationDis"]);
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
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num8);
			row["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) - Convert.ToDecimal(row["SumAvergItem"]));
		}
		decimal num9 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		TxtVat.Text = Convert.ToString(num9);
		decimal num10 = Convert.ToDecimal(datatable.Compute("SUM(SumInstallationDis)", string.Empty));
		decimal num11 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num11);
		decimal value = num11 - num10;
		if (DicountType == "DiscAmount")
		{
			PerNum = Convert.ToDecimal(TxtDiscount.Text) / Convert.ToDecimal(value) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(value) + num9) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(value)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(value) + num9) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(value) + num9) * 100m;
			TxtDiscount.Text = (PerNum / 100m * Convert.ToDecimal(value)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num12 = ConvertToDecimalOrDefault(row2["Quantity"]);
			decimal num13 = ConvertToDecimalOrDefault(row2["Height"]);
			decimal num14 = ConvertToDecimalOrDefault(row2["Width"]);
			decimal num15 = ConvertToDecimalOrDefault(row2["Long"]);
			decimal num16 = ConvertToDecimalOrDefault(row2["Weight"]);
			decimal num17 = ConvertToDecimalOrDefault(row2["BounceItem"]);
			decimal num18 = num12 * num13 * num14 * num15 * num16;
			decimal num19 = (num12 + num17) * num13 * num14 * num15 * num16;
			if (Convert.ToInt32(row2["ItemID"]) > 0)
			{
				row2["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row2["UnitPriceDisc"]) - Convert.ToDecimal(row2["UnitPriceDisc"]) * (PerNum / 100m));
				decimal num20 = Convert.ToDecimal(row2["UnitPriceDisc"]);
				row2["SumPriceDisc"] = Convert.ToDecimal(row2["UnitPriceDisc"]) * num18 + Convert.ToDecimal(row2["SumInstallationDis"]);
				decimal num21 = Convert.ToDecimal(row2["SumPriceDisc"]);
				if (Convert.ToBoolean(row2["IsItemTax"]))
				{
					row2["ItemVatDisc"] = Convert.ToDecimal(row2["SumPriceDisc"]) * (Convert.ToDecimal(row2["Tax_Value"]) / 100m);
				}
				else
				{
					row2["ItemVatDisc"] = 0;
				}
				decimal num22 = Convert.ToDecimal(row2["ItemVatDisc"]);
				row2["Net"] = Math.Round(Convert.ToDecimal(row2["SumPriceDisc"]) + Convert.ToDecimal(row2["ItemVatDisc"]), 1);
				decimal num23 = Convert.ToDecimal(row2["Net"]);
				row2["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row2["AvergItem"]) * num19);
				row2["Profit"] = FUNC_DecimalNum(Convert.ToDecimal(row2["SumPriceDisc"]) - Convert.ToDecimal(row2["SumAvergItem"]));
			}
		}
		decimal num24 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		decimal num25 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
		if (num25 > 1000m)
		{
			num25 = Math.Round(num25);
		}
		TxtVatDisc.Text = Convert.ToString(num24);
		if (Convert.ToDecimal(TxtBounce.Text) > 0m)
		{
			decimal num26 = FUNC_DecimalNum(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text));
			decimal num27 = num26 * (BounnsPre / 100m);
			TxtBounce.Text = num27.ToString();
			TxtNet.Text = (num25 + num27).ToString();
		}
		else
		{
			TxtNet.Text = num25.ToString();
		}
	}

	private void SetKeyboardLayout(InputLanguage layout)
	{
		InputLanguage.CurrentInputLanguage = layout;
	}

	private void Clear()
	{
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
		TxtAddress.Text = string.Empty;
		TxtDiscount.Text = string.Empty;
		BtnDisCode.Text = string.Empty;
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtType.Text = "نقدى";
		TxtSuppliers.Text = Cust.GetFirstCustomer();
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtPhone.Text = string.Empty;
		TxtNumNote.Text = string.Empty;
		TxtVatDisc.Text = "0.00";
		TxtTafqit.Text = string.Empty;
		BtnCustomerSearch.Text = string.Empty;
		TxtTotals.Text = "0.00";
		TxtTotalDiscount.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtBounce.Text = "0.00";
		TxtLocation.Text = string.Empty;
		TxtSellPrice1.Text = "0.00";
		TxtSellPrice2.Text = "0.00";
		TxtSellPrice3.Text = "0.00";
		TxtProfit.Text = "0.00";
		TxtQuntity.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		TxtAllQuantity.Text = FUNC_DecimalNumQty(0m).ToString();
		TxtAvergCost.Text = FUNC_DecimalNumQty(0m).ToString();
		TxtLastCost.Text = "0.00";
		TxtCharge.Text = "0.00";
		TxtHasmAmount.Text = "0.00";
		TxtHasmPer.Text = "0.00";
		TxtDate.DateTime = DateTime.Now;
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
		TxtDiscount.Enabled = true;
		TxtNote.Enabled = true;
		TxtType.Enabled = true;
		TxtDate.Enabled = true;
		toggleHold.IsOn = false;
		BtnCustomerSearch.Focus();
		Comp_Store.SelectedValue = LoginDetails.StoreID;
		CBSaleTybe.SelectedValue = 1;
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
		BtnDisCode.Enabled = true;
		InsertNewRow();
		BtnOrderSearch.Text = string.Empty;
		IsEnableTrue();
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtSuppliers.Focus();
		LValid.Visible = false;
		LSucced.Visible = false;
		BtnUpload.Visible = false;
		Cars.CarName = "";
		Cars.CarModel = "";
		Cars.PlateNum = "";
		Cars.ChassisNum = "";
		Cars.Color = "";
		Cars.Counter = "";
	}

	private async Task Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 12))
		{
			if (Convert.ToDecimal(TxtNet.Text) > 0m)
			{
				int Result = 0;
				bool IsWait = toggleHold.IsOn;
				if (datatable.Rows.Count >= 1)
				{
					if (TxtSuppliers.Text != string.Empty)
					{
						if (TxtType.Text != string.Empty)
						{
							if (Convert.ToInt32(TxtType.SelectedValue) == 2 && string.IsNullOrWhiteSpace(LblSup.Text))
							{
								MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع حساب عميل نقدى", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								TxtSuppliers.Focus();
							}
							else
							{
								if (PermissionSellPrice() == 0)
								{
									return;
								}
								decimal isok = IsItemPriceZero();
								if (isok == 0m)
								{
									return;
								}
								if (Convert.ToInt32(TxtType.SelectedValue) == 1)
								{
									LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
									FrmMoney FrmShow = new FrmMoney();
									FrmShow.ShowDialog();
									if (!FrmShow.IsComplateMony)
									{
										return;
									}
									FrmCash = Convert.ToDecimal(FrmShow.TxtCash.Text);
									FrmBank = Convert.ToDecimal(FrmShow.TxtVisa.Text);
									AccountSN = Convert.ToInt32(FrmShow.LblVisa.Text);
									Safy = 0m;
								}
								else
								{
									LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
									FrmMoneyAjil FrmShow2 = new FrmMoneyAjil();
									FrmShow2.ShowDialog();
									if (!FrmShow2.IsComplateMony)
									{
										return;
									}
									decimal Charg = ((!(Convert.ToDecimal(TxtCharge.Text) < 0m)) ? Convert.ToDecimal(TxtCharge.Text) : (Convert.ToDecimal(TxtCharge.Text) * -1m));
									decimal num = Convert.ToDecimal(FrmShow2.TxtNet.Text) + Charg;
									decimal? creditLimit = CreditLimit;
									int num2;
									if (!((num <= creditLimit.GetValueOrDefault()) & creditLimit.HasValue))
									{
										creditLimit = CreditLimit;
										num2 = (((creditLimit.GetValueOrDefault() == 0m) & creditLimit.HasValue) ? 1 : 0);
									}
									else
									{
										num2 = 1;
									}
									if (num2 == 0)
									{
										MessageBox.Show("عفوا\u064b، رصيد العميل لا يكفي \r\n  الحد الائتماني = " + FUNC_DecimalNum(CreditLimit), "مستكشف البيانات", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
										return;
									}
									FrmCash = Convert.ToDecimal(FrmShow2.TxtCash.Text);
									FrmBank = Convert.ToDecimal(FrmShow2.TxtVisa.Text);
									Safy = Convert.ToDecimal(FrmShow2.TxtNet.Text);
									ChargeAfterSale = Convert.ToDecimal(TxtCharge.Text) + FrmCash + FrmBank + Convert.ToDecimal(TxtNet.Text);
									AccountSN = Convert.ToInt32(FrmShow2.LblVisa.Text);
								}
								if (CBCostCenter.SelectedValue == null)
								{
									CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
								}
								CheckFfistRow();
								if (!FilterDataTableSave(datatable, Result, IsWait))
								{
									return;
								}
								LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
								LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum);
								Result = ORD.SaveOrdersDraft(Convert.ToInt32(LblSup.Text), IsWait, TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, DateTime.Parse(TxtDate.Text), Convert.ToInt32(TxtType.SelectedValue), string.Empty, 1, Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBBounce.SelectedValue), TxtNote.Text, TxtNumNote.Text, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtTotals.Text), Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtDiscount.Text), Convert.ToDecimal(txtdicoutprege.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtNet.Text), FrmCash, FrmBank, OrderCashierType: false, 0, 0, dtCurrentTable, Convert.ToInt32(CBSaleTybe.SelectedValue), TxtSales.Text, DateTime.Parse(DateHold.Text), AccountSN.ToString(), TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtAddress.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, Convert.ToDecimal(TxtHasmPer.Text), Convert.ToDecimal(TxtHasmAmount.Text), ChargeAfterSale, Convert.ToInt32(CBProject.SelectedValue), Convert.ToDecimal(TxtAllDiscount.Text), Convert.ToDecimal(TxtBounce.Text), BtnDisCode.Text, 0m, 0m, Cars.CarName, Cars.CarModel, Cars.PlateNum, Cars.ChassisNum, Cars.Color, Cars.Counter);
								if (LoginDetails.Active_Virgin == 9)
								{
									ORD.InsertRes(Result, selectedHallsID, selectedBookingDate, selectedInstallationDate, selectedJawDate);
								}
								gridControl1.DataSource = datatable;
								gridView1.OptionsBehavior.Editable = false;
								if (Result > 0)
								{
									BtnSave.Enabled = false;
									IsEnableFalse();
									TxtCode.Text = Result.ToString();
									DisCode.NumberUsedAsync(BtnDisCode.Text);
									DialogResult dialogResult = MessageBox.Show("تم حفظ بيانات فاتورة المبيعات بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
									if (dialogResult == DialogResult.Yes)
									{
										Print();
									}
									Charge(Convert.ToInt32(LblSup.Text));
								}
								else
								{
									ChargeAfterSale = 0m;
									MessageBox.Show("تم حفظ بيانات فاتورة المبيعات رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
						MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة المبيعات بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
				MessageBox.Show("عفوا\u064b لايمكن حفظ الفاتوره واجمالي الفاتوره يساوي الصفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
			bool isOn = toggleHold.IsOn;
			int purchese_ID = 0;
			if (!(TxtSuppliers.Text != string.Empty))
			{
				return;
			}
			using (new GTSdbEntities())
			{
				if (Convert.ToInt32(TxtType.SelectedValue) == 2 && TxtSuppliers.Text == "عميل نقدى")
				{
					MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع مورد نقدى", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtSuppliers.Focus();
				}
				else if (TxtCode.Text != string.Empty)
				{
					int num = Convert.ToInt32(TxtCode.Text);
					if (datatable.Rows.Count <= 1)
					{
						return;
					}
					decimal num2 = IsItemPriceZeroUpdate();
					if (num2 == 0m)
					{
						return;
					}
					ArrangeDatatable();
					Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
					if (!FilterDataTableSave(datatable, purchese_ID, isOn))
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
							return;
						}
						FrmCash = Convert.ToDecimal(frmMoney.TxtCash.Text);
						FrmBank = Convert.ToDecimal(frmMoney.TxtVisa.Text);
						Safy = 0m;
						ChargeAfterSale = Convert.ToDecimal(TxtCharge.Text) + FrmCash + FrmBank + Convert.ToDecimal(TxtNet.Text);
						AccountSN = Convert.ToInt32(frmMoney.LblVisa.Text);
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
						decimal value = ((!(Convert.ToDecimal(TxtCharge.Text) < 0m)) ? Convert.ToDecimal(TxtCharge.Text) : (Convert.ToDecimal(TxtCharge.Text) * -1m));
						decimal num3 = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
						decimal? num4 = CreditLimit + (decimal?)value;
						if (!((num3 <= num4.GetValueOrDefault()) & num4.HasValue))
						{
							num4 = CreditLimit;
							if (!((num4.GetValueOrDefault() == 0m) & num4.HasValue))
							{
								MessageBox.Show("عفوا\u064b، رصيد العميل لا يكفي \r\n  الحد الائتماني = " + FUNC_DecimalNum(CreditLimit), "مستكشف البيانات", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
								return;
							}
						}
						FrmCash = Convert.ToDecimal(frmMoneyAjil.TxtCash.Text);
						FrmBank = Convert.ToDecimal(frmMoneyAjil.TxtVisa.Text);
						Safy = Convert.ToDecimal(frmMoneyAjil.TxtNet.Text);
						ChargeAfterSale = Convert.ToDecimal(TxtCharge.Text) + FrmCash + FrmBank + Convert.ToDecimal(TxtNet.Text);
						AccountSN = Convert.ToInt32(frmMoneyAjil.LblVisa.Text);
					}
					CheckFfistRow();
					if (CBCostCenter.SelectedValue == null)
					{
						CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
					}
					LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
					LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum);
					purchese_ID = ORD.UpdateOrdersDraft(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), isOn, TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, DateTime.Parse(TxtDate.Text), Convert.ToInt32(TxtType.SelectedValue), string.Empty, 1, Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBBounce.SelectedValue), TxtNote.Text, TxtNumNote.Text, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtTotals.Text), Convert.ToDecimal(TxtTotals.Text) + Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtDiscount.Text), Convert.ToDecimal(txtdicoutprege.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtNet.Text), FrmCash, FrmBank, OrderCashierType: false, 0, 0, dtCurrentTable, TxtSales.Text, DateTime.Parse(DateHold.Text), AccountSN.ToString(), TxtStreet.Text, TxtDistrict.Text, TxtCity.Text, TxtAddress.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, Convert.ToDecimal(TxtHasmPer.Text), Convert.ToDecimal(TxtHasmAmount.Text), 0m, Convert.ToDecimal(TxtAllDiscount.Text), Convert.ToDecimal(TxtBounce.Text), Cars.CarName, Cars.CarModel, Cars.PlateNum, Cars.ChassisNum, Cars.Color, Cars.Counter);
					if (LoginDetails.Active_Virgin == 9)
					{
						ORD.UpdateRes(Convert.ToInt32(TxtCode.Text), selectedHallsID, selectedBookingDate, selectedInstallationDate, selectedJawDate);
					}
					if (purchese_ID > 0)
					{
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						IsEnableFalse();
						BtnEdit.Text = "تعديل F6";
						gridView1.OptionsBehavior.Editable = false;
						switch (MessageBox.Show("تم تعديل فاتورة المبيعات بنجاح \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
						{
						case DialogResult.Yes:
							Print();
							break;
						}
						Charge(Convert.ToInt32(LblSup.Text));
					}
					else
					{
						ChargeAfterSale = 0m;
						MessageBox.Show("تم حفظ بيانات فاتورة المبيعات رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else
				{
					MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
				return;
			}
		}
		MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 12))
		{
			using (new GTSdbEntities())
			{
				int num = 0;
				if (TxtCode.Text != string.Empty)
				{
					int num2 = Convert.ToInt32(TxtCode.Text);
					if (LoginDetails.Active_Virgin == 9)
					{
						ORD.DeleteRes(Convert.ToInt32(TxtCode.Text));
					}
					switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة المبيعات رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						num = ORD.DeleteOrdersDraft(Convert.ToInt32(TxtCode.Text));
						if (num > 0)
						{
							MessageBox.Show("تم حذف بيانات فاتورة المبيعات رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							Clear();
							gridView1.OptionsBehavior.Editable = true;
							IsEnableTrue();
							BtnDelete.Enabled = false;
							BtnEdit.Enabled = false;
							BtnSave.Enabled = true;
							InsertNewRow();
							TxtSuppliers.Focus();
						}
						else
						{
							MessageBox.Show("لا يمكن حذف بيانات فاتورة المبيعات رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						}
						break;
					}
				}
				else
				{
					MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة المبيعات بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
				return;
			}
		}
		MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			if (TxtCode.Text != string.Empty)
			{
				SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_OrdersDraft", sqlConnection);
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
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintOrders.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintOrders.mrt");
					}
					stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					if (TxtNumVat.Text != string.Empty)
					{
						stiReport["Title"] = "فاتورة ضريبية";
					}
					else
					{
						stiReport["Title"] = "فاتورة ضريبية مبسطة";
					}
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
					Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
					string value = ((!LoginDetails.ItemTotalWithVat) ? class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString()) : class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString()));
					stiReport["EncHash"] = value;
					stiReport.Render();
					stiReport.Show(dialogForm: true);
					if (LoginDetails.OrderStore)
					{
						PrintOrderStore();
					}
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

	private void PrintAfterReturn()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			if (TxtCode.Text != string.Empty)
			{
				SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_OrdersAfterReturn", sqlConnection);
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
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintOrders2.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintOrders2.mrt");
					}
					stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					if (TxtNumVat.Text != string.Empty)
					{
						stiReport["Title"] = "فاتورة ضريبية";
					}
					else
					{
						stiReport["Title"] = "فاتورة ضريبية مبسطة";
					}
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
					Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
					string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString());
					stiReport["EncHash"] = value;
					stiReport.Render();
					stiReport.Show(dialogForm: true);
					if (LoginDetails.OrderStore)
					{
						PrintOrderStore();
					}
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

	private void PrintOrderStore()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			if (TxtCode.Text != string.Empty)
			{
				SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Order_OrdersDraft", sqlConnection);
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
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintOrdersFromStore.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintOrdersFromStore.mrt");
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
					Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
					string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), TxtTotalDiscount.Text, TxtVatDisc.Text);
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
		TxtBounce.ReadOnly = false;
		TxtCity.ReadOnly = false;
		TxtAddress.ReadOnly = false;
		TxtAdditionalNum.ReadOnly = false;
		TxtPostalCode.ReadOnly = false;
		DateHold.ReadOnly = false;
		TxtCommercialRecord.ReadOnly = false;
		TxtDistrict.ReadOnly = false;
		TxtStreet.ReadOnly = false;
		TxtAllDiscount.ReadOnly = false;
		txtdicoutprege.ReadOnly = false;
		BtnCustomerSearch.ReadOnly = false;
		TxtHasmAmount.ReadOnly = false;
		TxtHasmPer.ReadOnly = false;
		CBCostCenter.Enabled = true;
		CBSaleTybe.Enabled = true;
		CBProject.Enabled = true;
		CBBounce.Enabled = true;
		Comp_Store.Enabled = true;
		toggleHold.Enabled = true;
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = true;
		TxtBounce.ReadOnly = true;
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
		TxtAddress.ReadOnly = true;
		TxtAdditionalNum.ReadOnly = true;
		TxtPostalCode.ReadOnly = true;
		TxtCommercialRecord.ReadOnly = true;
		TxtDistrict.ReadOnly = true;
		TxtStreet.ReadOnly = true;
		TxtNumNote.ReadOnly = true;
		TxtNote.ReadOnly = true;
		TxtDiscount.ReadOnly = true;
		TxtAllDiscount.ReadOnly = true;
		txtdicoutprege.ReadOnly = true;
		BtnCustomerSearch.ReadOnly = true;
		TxtHasmAmount.ReadOnly = true;
		TxtHasmPer.ReadOnly = true;
		CBCostCenter.Enabled = false;
		CBProject.Enabled = false;
		toggleHold.Enabled = false;
		TxtDiscount.BackColor = Color.White;
		TxtSales.BackColor = Color.White;
		TxtCity.BackColor = Color.White;
		TxtCommercialRecord.BackColor = Color.White;
		TxtAdditionalNum.BackColor = Color.White;
		TxtPostalCode.BackColor = Color.White;
		TxtDistrict.BackColor = Color.White;
		TxtStreet.BackColor = Color.White;
		TxtAddress.BackColor = Color.White;
		TxtBounce.BackColor = Color.White;
		TxtNote.BackColor = Color.White;
		TxtType.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
		TxtSuppliers.BackColor = Color.White;
		TxtNumVat.BackColor = Color.White;
		TxtPhone.BackColor = Color.White;
		TxtNumNote.BackColor = Color.White;
		CBBounce.BackColor = Color.White;
		Comp_Store.BackColor = Color.White;
		BtnCustomerSearch.BackColor = Color.White;
	}

	public void openItmCard()
	{
		if (gridControl1.Focus())
		{
			if (CodeItm != string.Empty)
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
			CodeItm = string.Empty;
		}
		FrmItems frmItems = new FrmItems();
		frmItems.ShowDialog();
		LoginDetails.ItemSearchID = 0;
	}

	private void TxtDigital()
	{
		TxtVat.DecimalDigits = DecimalNum;
		TxtVatDisc.DecimalDigits = DecimalNum;
		TxtTotals.DecimalDigits = DecimalNum;
		TxtTotalDiscount.DecimalDigits = DecimalNum;
		TxtNet.DecimalDigits = DecimalNum;
		TxtCost.DecimalDigits = DecimalNum;
		TxtTabocc.DecimalDigits = DecimalNum;
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
				string text = "";
				int num = 1;
				foreach (string item in LoginDetails.ItemSearchAll2)
				{
					text = item.ToString();
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(text).FirstOrDefault();
					if (get_Item_ByCodeALL_Result == null)
					{
						continue;
					}
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = get_Item_ByCodeALL_Result.ItemId;
					dataRow["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
					dataRow["ItemCode"] = get_Item_ByCodeALL_Result.Item_code;
					dataRow["ItemName"] = get_Item_ByCodeALL_Result.item_Name;
					dataRow["UnitType"] = "الصغرى";
					dataRow["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitSmallID;
					dataRow["VatCode"] = get_Item_ByCodeALL_Result.VatCode;
					dataRow["TaxExemptionReason"] = get_Item_ByCodeALL_Result.TaxExemptionReason;
					dataRow["TaxExemptionReasonCode"] = get_Item_ByCodeALL_Result.TaxExemptionReasonCode;
					dataRow["Quantity"] = 1;
					dataRow["BounceItem"] = 0;
					dataRow["Height"] = 1;
					dataRow["Width"] = 1;
					dataRow["Long"] = 1;
					dataRow["Weight"] = 1;
					dataRow["AmountDisc"] = 0;
					dataRow["PercentDisc"] = 0;
					dataRow["UnitNum"] = 0;
					num = Convert.ToInt32(get_Item_ByCodeALL_Result.UnitTafqitID);
					if (LoginDetails.InstallAll == 0)
					{
						dataRow["InstallationPrice"] = 0;
						dataRow["SumInstallationDis"] = 0;
					}
					else
					{
						dataRow["InstallationPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.PriceExportunit1);
						dataRow["SumInstallationDis"] = dataRow["InstallationPrice"];
					}
					if (get_Item_ByCodeALL_Result.UnitTafqitID == 1)
					{
						dataRow["UnitNameTF"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitSmallID);
						dataRow["QuantityTF"] = FUNC_DecimalNum(dataRow["Quantity"].ToString());
						dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitSmallID;
					}
					if (get_Item_ByCodeALL_Result.UnitTafqitID == 2)
					{
						dataRow["UnitNameTF"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitMediumID);
						dataRow["QuantityTF"] = 1;
						dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitMediumID;
					}
					if (get_Item_ByCodeALL_Result.UnitTafqitID == 3)
					{
						dataRow["UnitNameTF"] = ITM.GetUnitName(get_Item_ByCodeALL_Result.UnitLargeID);
						dataRow["QuantityTF"] = 1;
						dataRow["TafqitId"] = get_Item_ByCodeALL_Result.UnitLargeID;
					}
					dataRow["Done"] = 1;
					dataRow["UnitName"] = get_Item_ByCodeALL_Result.UnitSmall;
					dataRow["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost;
					dataRow["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost;
					dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
					dataRow["SumPrice"] = dataRow["UnitPrice"];
					int num2 = Convert.ToInt32(CBSaleTybe.SelectedValue);
					if (!LoginDetails.IsVat)
					{
						switch (num2)
						{
						case 1:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
							dataRow["Total"] = dataRow["UnitPrice"];
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						case 2:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
							dataRow["Total"] = dataRow["UnitPrice"];
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						case 3:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall3);
							dataRow["Total"] = dataRow["UnitPrice"];
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						default:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
							dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						}
						dataRow["IsItemTax"] = 0;
					}
					else if (LoginDetails.ItemTotalWithVat)
					{
						decimal num3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
						switch (num2)
						{
						case 1:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall / (decimal?)num3);
							dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall) + Convert.ToDecimal(dataRow["InstallationPrice"]), DecimalNum);
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						case 2:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
							dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall2), DecimalNum);
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						case 3:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall3);
							dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall3), DecimalNum);
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						default:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
							dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						}
						dataRow["Tax_Value"] = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value);
						dataRow["IsItemTax"] = get_Item_ByCodeALL_Result.Is_Tax;
						dataRow["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["Total"]) / num3);
						dataRow["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["Total"]) - Convert.ToDecimal(dataRow["SumPrice"]));
						dataRow["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["ItemVat"]));
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["Total"]));
						dataRow["UnitPriceDisc"] = dataRow["UnitPrice"];
						dataRow["SumPriceDisc"] = dataRow["SumPrice"];
					}
					else
					{
						switch (num2)
						{
						case 1:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
							dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						case 2:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall2);
							dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall2), DecimalNum);
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						case 3:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall3);
							dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall3), DecimalNum);
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						default:
							dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceSmall);
							dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceSmall), DecimalNum);
							dataRow["OrginalPrice"] = dataRow["Total"];
							break;
						}
						dataRow["SumPrice"] = dataRow["UnitPrice"];
						DataRow dataRow2 = dataRow;
						decimal value = Convert.ToDecimal(dataRow["SumPrice"]);
						decimal? num4 = get_Item_ByCodeALL_Result.Tax_Value / (decimal?)100;
						dataRow2["ItemVat"] = FUNC_DecimalNum((decimal?)value * num4);
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["SumPrice"]) + Convert.ToDecimal(dataRow["ItemVat"]));
						dataRow["IsItemTax"] = 1;
						dataRow["Tax_Value"] = get_Item_ByCodeALL_Result.Tax_Value;
					}
					if (get_Item_ByCodeALL_Result.MediumBarCode1 == text || get_Item_ByCodeALL_Result.MediumBarCode2 == text || get_Item_ByCodeALL_Result.MediumBarCode3 == text)
					{
						dataRow["UnitType"] = "المتوسطة";
						dataRow["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitMediumID;
						dataRow["UnitName"] = get_Item_ByCodeALL_Result.UnitMedium;
						dataRow["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit;
						dataRow["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit;
						dataRow["UnitPrice"] = get_Item_ByCodeALL_Result.SellPriceMedium;
						dataRow["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value2 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							switch (num2)
							{
							case 1:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium / (decimal?)value2);
								dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["Total"];
								break;
							case 2:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium2);
								dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium2), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["Total"];
								break;
							case 3:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium3);
								dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium3), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["Total"];
								break;
							default:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
								dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["Total"];
								break;
							}
						}
						else
						{
							switch (num2)
							{
							case 1:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
								dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
								break;
							case 2:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium2);
								dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium2), DecimalNum);
								break;
							case 3:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium3);
								dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium3), DecimalNum);
								break;
							default:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellPriceMedium);
								dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellPriceMedium), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["Total"];
								break;
							}
						}
					}
					if (get_Item_ByCodeALL_Result.BigBarCode1 == text || get_Item_ByCodeALL_Result.BigBarCode2 == text || get_Item_ByCodeALL_Result.BigBarCode3 == text)
					{
						dataRow["UnitType"] = "الكبرى";
						dataRow["ItemUnitID"] = get_Item_ByCodeALL_Result.UnitLargeID;
						dataRow["UnitName"] = get_Item_ByCodeALL_Result.UnitLarge;
						dataRow["AvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit * get_Item_ByCodeALL_Result.ConvertBigUnit;
						dataRow["SumAvergItem"] = get_Item_ByCodeALL_Result.Average_cost * get_Item_ByCodeALL_Result.ConvertMediumUnit * get_Item_ByCodeALL_Result.ConvertBigUnit;
						dataRow["UnitPrice"] = get_Item_ByCodeALL_Result.SellpriceLarge;
						dataRow["SumPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal value3 = Convert.ToDecimal(get_Item_ByCodeALL_Result.Tax_Value) / 100m + 1m;
							switch (num2)
							{
							case 1:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge / (decimal?)value3);
								dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["Total"];
								break;
							case 2:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge2);
								dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge2), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["Total"];
								break;
							case 3:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge3);
								dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge3), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["Total"];
								break;
							default:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge / (decimal?)value3);
								dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["Total"];
								break;
							}
						}
						else
						{
							switch (num2)
							{
							case 1:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
								dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
								break;
							case 2:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge2);
								dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge2), DecimalNum);
								break;
							case 3:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge3);
								dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge3), DecimalNum);
								break;
							default:
								dataRow["UnitPrice"] = FUNC_DecimalNum(get_Item_ByCodeALL_Result.SellpriceLarge);
								dataRow["Total"] = Math.Round(Convert.ToDecimal(get_Item_ByCodeALL_Result.SellpriceLarge), DecimalNum);
								dataRow["OrginalPrice"] = dataRow["Total"];
								break;
							}
						}
					}
					dataRow["StoreName"] = Comp_Store.Text;
					dataRow["StoreID"] = Comp_Store.SelectedValue;
					dataRow["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["Total"]));
					decimal num5 = ((dataRow["SumPriceDisc"] != DBNull.Value) ? Convert.ToDecimal(dataRow["SumPriceDisc"]) : 0m);
					decimal num6 = ((dataRow["SumAvergItem"] != DBNull.Value) ? Convert.ToDecimal(dataRow["SumAvergItem"]) : 0m);
					dataRow["Profit"] = FUNC_DecimalNum(num5 - num6);
					datatable.Rows.Add(dataRow);
				}
				CheckFfistRow();
				InsertNewRow();
				ArrangeDatatable();
				frmSearchItems.Close();
				frmSearchItems.Hide();
				LoginDetails.ItemSearchAll2.Clear();
				LoginDetails.ISPurches = false;
				if (num > 1)
				{
					Taffit(text);
				}
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

	private decimal SafeConvertToDecimal(object value)
	{
		return (value == DBNull.Value || value == null) ? 0m : Convert.ToDecimal(value);
	}

	public bool FilterDataTableSave(DataTable ItemOrder, int Purchese_ID, bool IsWait)
	{
		int num = 1;
		dtCurrentTable.Clear();
		foreach (DataRow row in ItemOrder.Rows)
		{
			if (row["ItemCode"].ToString() != string.Empty)
			{
				decimal num2 = SafeConvertToDecimal(row["Net"]);
				if (num2 == 0m)
				{
					MessageBox.Show("لا يمكن حفظ الفاتورة، يوجد أصناف بصافي قيمة = 0", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					return false;
				}
				int num3 = ((row["ItemUnitID"] != DBNull.Value) ? Convert.ToInt32(row["ItemUnitID"]) : 0);
				dtCurrentTable.Rows.Add(num, Purchese_ID, (row["ItemID"] != DBNull.Value) ? Convert.ToInt32(row["ItemID"]) : 0, LoginDetails.BranchID, IsWait, (row["StoreID"] != DBNull.Value) ? Convert.ToInt32(row["StoreID"]) : 0, num3, (row["Quantity"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["Quantity"]), (row["AvergItem"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["AvergItem"]), (row["UnitPrice"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["UnitPrice"]), (row["UnitPrice"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["UnitPrice"]), (row["Total"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["Total"]), (row["ItemVat"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["ItemVat"]), (row["UnitPriceDisc"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["UnitPriceDisc"]), (row["Net"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["Net"]), (row["ItemVatDisc"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["ItemVatDisc"]), (row["UnitType"] == DBNull.Value) ? string.Empty : row["UnitType"].ToString(), 1, (row["ItemNote"] == DBNull.Value) ? string.Empty : row["ItemNote"].ToString(), 1, (row["UnitNum"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["UnitNum"]), (row["Weight"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["Weight"]), (row["Long"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["Long"]), string.Empty, (row["Width"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["Width"]), (row["Height"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["Height"]), (row["BounceItem"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["BounceItem"]), (row["AmountDisc"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["AmountDisc"]), (row["PercentDisc"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["PercentDisc"]), (row["TafqitId"] != DBNull.Value) ? Convert.ToInt32(row["TafqitId"]) : 0, (row["QuantityTF"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["QuantityTF"]), (row["TaxTobacco"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["TaxTobacco"]), (row["TaxTobaccoDis"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["TaxTobaccoDis"]), (row["TotalWithTaxTobacco"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["TotalWithTaxTobacco"]), (row["InstallationPrice"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["InstallationPrice"]), (row["SumInstallationDis"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["SumInstallationDis"]));
				num++;
			}
		}
		return true;
	}

	public void GetData(int orderId, int BranchID, string storedProcedureName)
	{
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
		sqlConnection.Open();
		try
		{
			using SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.AddWithValue("@PurBranchID", orderId);
			sqlCommand.Parameters.AddWithValue("@BranchID", BranchID);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);
			if (dataSet.Tables.Count > 0)
			{
				DataTable dataTable = dataSet.Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					BtnCustomerSearch.Text = dataRow["SupplierID"].ToString();
					if (storedProcedureName == "GetOrdersWithDetailsDraft")
					{
						TxtCode.Text = dataRow["ID"].ToString();
						if (Convert.ToInt32(dataRow["IsWaiting"]) == 1)
						{
							toggleHold.IsOn = true;
							DateHold.DateTime = Convert.ToDateTime(dataRow["DateHold"]);
							DateHold.ReadOnly = true;
						}
						else
						{
							toggleHold.IsOn = false;
						}
					}
					TxtDate.DateTime = Convert.ToDateTime(dataRow["Purchases_Date"]);
					TxtNote.Text = dataRow["Note"].ToString();
					TxtNumNote.Text = dataRow["NoteNum"].ToString();
					TxtTotals.Text = dataRow["TotalPrices"].ToString();
					TxtVat.Text = dataRow["Tax"].ToString();
					TxtDiscount.Text = dataRow["DiscountNum"].ToString();
					txtdicoutprege.Text = dataRow["DiscountPerantage"].ToString();
					CBSaleTybe.SelectedValue = Convert.ToInt32(dataRow["Sectoral"]);
					if (storedProcedureName == "GetOrdersWithDetailsDraft")
					{
						TxtAllDiscount.Text = dataRow["AllDiscount"].ToString();
						TxtTotalDiscount.Text = dataRow["TotalPrices_Discount"].ToString();
						TxtVatDisc.Text = dataRow["Tax_Discount"].ToString();
						TxtBounce.Text = ((dataRow["BounsAmount"] != DBNull.Value) ? dataRow["BounsAmount"].ToString() : "0");
						CBBounce.Text = ORD.GetBonuceByID(Convert.ToInt32(dataRow["BounceID"]));
						BtnDisCode.Text = dataRow["DisCode"].ToString();
						Cars.CarName = dataRow["CarName"].ToString();
						Cars.CarModel = dataRow["CarModel"].ToString();
						Cars.PlateNum = dataRow["PlateNumber"].ToString();
						Cars.ChassisNum = dataRow["ChassisNum"].ToString();
						Cars.Color = dataRow["CarColor"].ToString();
						Cars.Counter = dataRow["Counter"].ToString();
					}
					else
					{
						TxtTotalDiscount.Text = dataRow["Safy"].ToString();
						TxtAllDiscount.Text = dataRow["TotalPrices_Discount"].ToString();
						TxtVatDisc.Text = dataRow["AllTax"].ToString();
						TxtNote.Text = dataRow["Note2"].ToString();
					}
					TxtNet.Text = dataRow["Net"].ToString();
					TxtCost.Text = dataRow["CostOrder"].ToString();
					TxtTabocc.Text = dataRow["TobaccoTax"].ToString();
					TxtHasmAmount.Text = dataRow["HasmAmount"].ToString();
					TxtHasmPer.Text = dataRow["HasmPer"].ToString();
					TxtType.Text = ORD.GetAccountNameById(Convert.ToInt32(dataRow["Order_Paymant_Type"]));
					CBCostCenter.Text = ORD.GetCostCenterByID(Convert.ToInt32(dataRow["CostCentersID"]));
					LoginDetails.NetCash = Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum);
					LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum);
					LblSup.Text = dataRow["SupplierID"].ToString();
					TxtSuppliers.Text = dataRow["SupplierName"].ToString();
					TxtPhone.Text = dataRow["SupplierPhone"].ToString();
					TxtAdditionalNum.Text = dataRow["AdditionalNum"].ToString();
					TxtPostalCode.Text = dataRow["PostalCode"].ToString();
					TxtCommercialRecord.Text = dataRow["CommercialRecord"].ToString();
					TxtSales.Text = dataRow["SalesMan"].ToString();
					TxtCity.Text = dataRow["City"].ToString();
					TxtStreet.Text = dataRow["Street"].ToString();
					TxtDistrict.Text = dataRow["District"].ToString();
					TxtAddress.Text = dataRow["Country"].ToString();
					TxtNumVat.Text = dataRow["SupplierVatNum"].ToString();
				}
				if (dataSet.Tables.Count >= 1)
				{
					DataTable dataTable2 = dataSet.Tables[0];
					datatable.Rows.Clear();
					foreach (DataRow row in dataTable2.Rows)
					{
						DataRow dataRow3 = datatable.NewRow();
						dataRow3["ItemID"] = row["ItemID"] ?? ((object)0);
						dataRow3["ItemUnitID"] = row["ItemUnitID"] ?? ((object)0);
						dataRow3["ItemNote"] = row["ItemNote"] ?? "";
						dataRow3["ItemCode"] = row["item_Code"] ?? "";
						dataRow3["ItemName"] = row["item_Name"] ?? "";
						dataRow3["BounceItem"] = row["Bounce"] ?? ((object)0);
						dataRow3["Height"] = row["Height"] ?? ((object)1);
						dataRow3["Width"] = row["Width"] ?? ((object)1);
						dataRow3["Long"] = row["Long"] ?? ((object)1);
						dataRow3["Weight"] = row["Weight"] ?? ((object)1);
						dataRow3["UnitNameTF"] = row["UnitName"] ?? "";
						dataRow3["QuantityTF"] = row["TafqitQunitity"] ?? ((object)1);
						dataRow3["TafqitId"] = row["TafqitUnitId"] ?? ((object)1);
						dataRow3["AmountDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["Amount_Discount"]));
						dataRow3["PercentDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["Per_Discount"]));
						dataRow3["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["NetTotalPrice"]) - Convert.ToDecimal(row["VAT_Discount"]));
						dataRow3["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["NetTotalPrice"]));
						dataRow3["UnitNum"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitNumber"]));
						dataRow3["ItemUnitID"] = row["ItemUnitID"] ?? ((object)1);
						decimal num = FUNC_DecimalNum((Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["Bounce"])) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Width"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Weight"]);
						decimal num2 = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * (Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Width"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Weight"]));
						decimal num3 = FUNC_DecimalNum(Convert.IsDBNull(row["LastCost"]) ? 0m : Convert.ToDecimal(row["LastCost"]));
						dataRow3["UnitType"] = row["ItemUnitType"] ?? "";
						dataRow3["UnitName"] = row["UnitName"] ?? "";
						dataRow3["StoreID"] = row["StoreID"] ?? ((object)1);
						dataRow3["StoreName"] = row["Store_Name"] ?? "";
						dataRow3["Quantity"] = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]));
						dataRow3["AvergItem"] = num3;
						dataRow3["SumAvergItem"] = FUNC_DecimalNum(num3 * num);
						dataRow3["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"] ?? ((object)0)));
						dataRow3["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["NetUnitPrice"] ?? ((object)0)));
						if (storedProcedureName == "GetOrdersWithDetailsDraft")
						{
							dataRow3["InstallationPrice"] = FUNC_DecimalNum(Convert.ToDecimal((row["PriceInstall"] == DBNull.Value) ? ((object)0) : row["PriceInstall"]));
							dataRow3["SumInstallationDis"] = FUNC_DecimalNum(Convert.ToDecimal((row["PriceInstallDise"] == DBNull.Value) ? ((object)0) : row["PriceInstallDise"]));
						}
						else
						{
							dataRow3["InstallationPrice"] = 0.0;
							dataRow3["SumInstallationDis"] = 0.0;
						}
						decimal num4 = Convert.ToDecimal(row["VAT"]);
						decimal num5 = Convert.ToDecimal(row["Tax_Value"]);
						decimal num6 = Convert.ToDecimal(row["UnitPrice"]);
						decimal num7 = Convert.ToDecimal(row["TotalPrice"]);
						if (LoginDetails.ItemTotalWithVat)
						{
							if (num4 > 0m)
							{
								decimal num8 = FUNC_DecimalNum(num5 / 100m + 1m);
								dataRow3["OrginalPrice"] = FUNC_DecimalNum(num6 * num8);
							}
						}
						else
						{
							dataRow3["OrginalPrice"] = FUNC_DecimalNum(num6);
						}
						if (num4 > 0m)
						{
							dataRow3["ItemVat"] = FUNC_DecimalNum(num4);
							dataRow3["Total"] = FUNC_DecimalNum(num7 + num4);
							dataRow3["IsItemTax"] = 1;
							dataRow3["Tax_Value"] = FUNC_DecimalNum(num5);
							dataRow3["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["VAT_Discount"]));
						}
						else
						{
							dataRow3["IsItemTax"] = 0;
							dataRow3["ItemVatDisc"] = 0;
							dataRow3["ItemVat"] = FUNC_DecimalNum(num4);
							dataRow3["Total"] = FUNC_DecimalNum(Convert.ToDecimal(num7) + Convert.ToDecimal(num4));
						}
						if (LoginDetails.ItemTotalWithVat)
						{
							dataRow3["SumPrice"] = FUNC_DecimalNum(num7 - num4);
							dataRow3["Total"] = FUNC_DecimalNum(num7);
						}
						else
						{
							dataRow3["SumPrice"] = FUNC_DecimalNum(num7);
							dataRow3["Total"] = FUNC_DecimalNum(num7);
						}
						dataRow3["Profit"] = ((dataRow3["SumPriceDisc"] != DBNull.Value) ? Convert.ToDecimal(dataRow3["SumPriceDisc"]) : 0m) - ((dataRow3["SumAvergItem"] != DBNull.Value) ? Convert.ToDecimal(dataRow3["SumAvergItem"]) : 0m);
						datatable.Rows.Add(dataRow3);
					}
					gridControl1.DataSource = datatable;
				}
				BtnDelete.Enabled = true;
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = false;
				CheckFfistRow();
				ArrangeDatatable();
				if (storedProcedureName == "GetOrdersWithDetailsDraft")
				{
					IsEnableFalse();
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnEdit.Text = "F6 تعديل";
					BtnDisCode.Enabled = false;
				}
				Charge(Convert.ToInt32(LblSup.Text));
			}
			else
			{
				MessageBox.Show("لا يوجد فاتوره بهذا الرقم");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message);
		}
		sqlConnection.Close();
	}

	private void SearchOrdNum(int OrderSearch)
	{
		try
		{
			GetData(OrderSearch, LoginDetails.BranchID, "GetOrdersWithDetailsDraft");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void GetPricePffer()
	{
		try
		{
			using (new GTSdbEntities())
			{
				FrmSearchPriceOffer frmSearchPriceOffer = new FrmSearchPriceOffer();
				frmSearchPriceOffer.ShowDialog();
				if (frmSearchPriceOffer.CustCode > 0)
				{
					int custCode = frmSearchPriceOffer.CustCode;
					GetData(custCode, LoginDetails.BranchID, "GetPriceOffersWithDetails");
					InsertNewRow();
					gridView1.OptionsBehavior.Editable = true;
					Tafqit();
				}
			}
		}
		catch (Exception)
		{
			Clear();
		}
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
				break;
			}
		}
	}

	public int PermissionSellPrice()
	{
		int result = 1;
		foreach (DataRow row in datatable.Rows)
		{
			string text = row["ItemCode"].ToString();
			if (text == string.Empty)
			{
				break;
			}
			if (row["UnitType"].ToString() == "الصغرى")
			{
				decimal sellPermission = ORD.GetSellPermission(text, UserSellPrice, "الصغرى");
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
				decimal sellPermission2 = ORD.GetSellPermission(text, UserSellPrice, "المتوسطة");
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
				decimal sellPermission3 = ORD.GetSellPermission(text, UserSellPrice, "الكبرى");
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

	private void Tafqit()
	{
		try
		{
			decimal d = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
			decimal num = Math.Round(d, 2);
			decimal num2 = Convert.ToDecimal(TxtBounce.Text);
			decimal num3 = num + num2;
			int num4 = (int)Math.Floor(num3);
			int num5 = (int)Math.Round((num3 - (decimal)num4) * 100m);
			string empty = string.Empty;
			if (LoginDetails.Lang == "ar-EG")
			{
				string text = num4.ToWords(new CultureInfo("ar"));
				string text2 = ((num5 > 0) ? ("و" + num5.ToWords(new CultureInfo("ar")) + " هللة") : string.Empty);
				empty = text + " ريالا\u064b " + text2 + " فقط لا غير";
			}
			else
			{
				string text3 = num4.ToWords(new CultureInfo("en"));
				string text4 = ((num5 > 0) ? ("and " + num5.ToWords(new CultureInfo("en")) + " Halalah") : string.Empty);
				empty = text3 + " Saudi Riyal " + text4 + " only";
			}
			TxtTafqit.Text = empty;
		}
		catch (Exception)
		{
		}
	}

	private void TafqitHusm()
	{
		try
		{
			decimal num = Convert.ToDecimal(TxtNet.Text);
			decimal num2 = Convert.ToDecimal(TxtBounce.Text);
			decimal num3 = num + num2;
			int num4 = (int)Math.Floor(num3);
			int num5 = (int)((num3 - (decimal)num4) * 100m);
			string text = ((!(LoginDetails.Lang == "ar-EG")) ? (num4.ToWords(new CultureInfo("en")) + ((num5 > 0) ? (" point " + num5.ToWords(new CultureInfo("en"))) : "Saudi riyal") + "Saudi riyal") : (num4.ToWords(new CultureInfo("ar")) + ((num5 > 0) ? (" فاصلة " + num5.ToWords(new CultureInfo("ar"))) : "") + " ريال سعودي"));
			TxtTafqit.Text = text;
		}
		catch (Exception)
		{
		}
	}

	private void HusmAndBouns()
	{
		if (LoginDetails.Bouns)
		{
			BounnsPre = Convert.ToDecimal(BNC.GetProfitByID(Convert.ToInt32(CBBounce.SelectedValue)));
		}
		else
		{
			LableBouns.Visible = false;
			TxtBounce.Visible = false;
			BounnsPre = 0m;
		}
		if (!LoginDetails.Hasm)
		{
			LblHasm.Visible = false;
			TxtHasmAmount.Visible = false;
			LblAmountHasm.Visible = false;
			TxtHasmPer.Visible = false;
			LblPerHasm.Visible = false;
		}
		if (!LoginDetails.DisCode)
		{
			BtnDisCode.Visible = false;
		}
	}

	private async void DiscountCode()
	{
		try
		{
			string disCode = BtnDisCode.Text.Trim();
			string companyName = LoginDetails.CompanyNameAR ?? "";
			string companyVatNum = LoginDetails.VatNum ?? "";
			if (string.IsNullOrWhiteSpace(disCode))
			{
				MessageBox.Show("يرجى إدخال كود الخصم", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			(decimal DiscountValue, decimal DiscountPercentage)? discountDetails = await DisCode.GetDisAsync(disCode, companyName, companyVatNum);
			if (discountDetails.HasValue)
			{
				decimal DisAmount = discountDetails.Value.DiscountValue;
				decimal Disper = discountDetails.Value.DiscountPercentage;
				TxtDiscount.Text = DisAmount.ToString();
				txtdicoutprege.Text = Disper.ToString();
				if (Convert.ToDecimal(DisAmount) > 0m)
				{
					Cal_FinalResultTotals(DisAmount, "DiscAmount");
				}
				else if (Convert.ToDecimal(Disper) > 0m)
				{
					Cal_FinalResultTotals(Disper, "Percent");
				}
				else
				{
					ZeroDisc();
				}
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show("حدث خطأ: " + ex2.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void repositoryItemButtonUnits_ButtonClick(object sender, ButtonPressedEventArgs e)
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
							}
						}
						frmSearchUnit.Close();
					}
				}
				frmSearchUnit.Close();
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
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

	private void repositoryItemButtonDelete_ButtonPressed(object sender, ButtonPressedEventArgs e)
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
		Cal_FinalResultTotals(Convert.ToDecimal(txtdicoutprege.Text), "Percent");
		gridControl1.DataSource = datatable;
	}

	private void repositoryItemButtonStores_ButtonClick(object sender, ButtonPressedEventArgs e)
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
		CBSaleTybe.SelectedIndex = 0;
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
			return;
		}
		if (e.KeyCode.ToString() == "F1")
		{
			FrmCars frmCars = new FrmCars();
			frmCars.ShowDialog();
			return;
		}
		if (e.KeyCode.ToString() == "F2")
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
		if (e.KeyCode.ToString() == "F8")
		{
			if (LoginDetails.Active_Virgin == 9)
			{
				new FrmReportReser().Show();
			}
			return;
		}
		if (e.KeyCode.ToString() == "F9")
		{
			GetPricePffer();
			return;
		}
		if (e.KeyCode.ToString() == "F10")
		{
			FrmSearchOrders frmSearchOrders = new FrmSearchOrders();
			frmSearchOrders.ShowDialog();
			if (frmSearchOrders.CustCode > 0)
			{
				SearchOrdNum(frmSearchOrders.CustCode);
				InsertNewRow();
				TxtCode.Text = string.Empty;
				gridView1.OptionsBehavior.Editable = true;
				BtnDelete.Enabled = false;
				BtnEdit.Enabled = false;
				BtnSave.Enabled = true;
			}
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
		else if (e.KeyCode == Keys.M && e.Modifiers == Keys.Control)
		{
			new FrmCustomer().ShowDialog();
		}
		else if (e.KeyCode == Keys.R && e.Modifiers == Keys.Control)
		{
			new FrmReservation().Show();
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
			TxtSuppliers.Text = "عميل نقدى";
			LblSup.Text = "0";
			TxtAdditionalNum.Text = string.Empty;
			TxtCity.Text = string.Empty;
			TxtStreet.Text = string.Empty;
			TxtDistrict.Text = string.Empty;
			TxtPostalCode.Text = string.Empty;
			TxtCharge.Text = "0.00";
			TxtCommercialRecord.Text = string.Empty;
			TxtSales.Text = string.Empty;
			TxtPhone.Text = string.Empty;
			TxtNumVat.Text = string.Empty;
			TxtSuppliers.ForeColor = Color.Black;
			TxtType.ForeColor = Color.Black;
		}
		if (TxtType.Text == "آجلة")
		{
			TxtSuppliers.Text = string.Empty;
			TxtAdditionalNum.Text = string.Empty;
			TxtCity.Text = string.Empty;
			TxtStreet.Text = string.Empty;
			TxtDistrict.Text = string.Empty;
			TxtPostalCode.Text = string.Empty;
			TxtCharge.Text = "0.00";
			TxtCommercialRecord.Text = string.Empty;
			TxtSales.Text = string.Empty;
			TxtPhone.Text = string.Empty;
			TxtNumVat.Text = string.Empty;
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
		Tafqit();
	}

	private void LblSup_TextChanged(object sender, EventArgs e)
	{
		try
		{
			LBlSuppAccount.Text = Cust.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
		}
		catch
		{
		}
	}

	private void TxtHasmAmount_TextChanged(object sender, EventArgs e)
	{
	}

	private void TxtPhone_KeyDown(object sender, KeyEventArgs e)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		Account_CustSup account_CustSup = gTSdbEntities.Account_CustSup.Where((Account_CustSup x) => x.Phone == TxtPhone.Text).FirstOrDefault();
		if (account_CustSup != null)
		{
			SearchCustNum(Convert.ToInt32(account_CustSup.ID));
		}
	}

	private void toggleHold_Toggled(object sender, EventArgs e)
	{
		if (toggleHold.IsOn)
		{
			LblDataHold.Visible = true;
			DateHold.Visible = true;
		}
		else
		{
			LblDataHold.Visible = false;
			DateHold.Visible = false;
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

	private void CBBounce_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (LoginDetails.Bouns)
		{
			if (CBBounce.SelectedValue != null && int.TryParse(CBBounce.SelectedValue.ToString(), out var result))
			{
				BounnsPre = Convert.ToDecimal(BNC.GetProfitByID(result));
			}
		}
		else
		{
			BounnsPre = 0m;
		}
	}

	private void toggleHold_Properties_Toggled(object sender, EventArgs e)
	{
		if (toggleHold.IsOn)
		{
			LblDataHold.Visible = true;
			DateHold.Visible = true;
		}
		else
		{
			LblDataHold.Visible = false;
			DateHold.Visible = false;
		}
	}

	private void حركةصنفToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Class_MovementItem class_MovementItem = new Class_MovementItem();
		class_MovementItem.MovementItemNormal(ItmID);
	}

	private void CkSentInvoice_CheckedChanged(object sender, EventArgs e)
	{
		if (CkSentInvoice.Checked)
		{
			LoginDetails.SentInvoice = true;
		}
		else
		{
			LoginDetails.SentInvoice = false;
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

	private void MenuPrintQued_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text != string.Empty)
		{
			Class_Print_DailyRestrictionsALL class_Print_DailyRestrictionsALL = new Class_Print_DailyRestrictionsALL();
			class_Print_DailyRestrictionsALL.Print_DailyRestrictionsALL(Convert.ToInt32(TxtCode.Text), 5);
		}
	}

	private void FrmOrders_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameAR);
		}
		else
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameEN);
		}
		Cars.CarName = "";
		Cars.CarModel = "";
		Cars.PlateNum = "";
		Cars.ChassisNum = "";
		Cars.Color = "";
		Cars.Counter = "";
	}

	private void MenuPurchOrder_Click(object sender, EventArgs e)
	{
		GetPricePffer();
	}

	private void label23_Click(object sender, EventArgs e)
	{
	}

	private void TxtNote_EditValueChanged(object sender, EventArgs e)
	{
	}

	private void StripMenuItem1_Click(object sender, EventArgs e)
	{
		FrmSearchOrders frmSearchOrders = new FrmSearchOrders();
		frmSearchOrders.ShowDialog();
		if (frmSearchOrders.CustCode <= 0)
		{
			return;
		}
		SearchOrdNum(frmSearchOrders.CustCode);
		InsertNewRow();
		gridView1.OptionsBehavior.Editable = true;
		TxtCode.Text = "";
		TxtAvergCost.Text = "";
		IsEnableTrue();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		int num = 1;
		foreach (DataRow row in datatable.Rows)
		{
			row["SN"] = num;
			num++;
			SN = Convert.ToInt32(row["SN"]);
		}
		Tafqit();
	}

	private void تخصيصToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		new FrmCustomer().ShowDialog();
	}

	private void BtnOrderSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SearchOrdNum(Convert.ToInt32(BtnOrderSearch.Text));
		}
	}

	private void BtnCustomerSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && !string.IsNullOrWhiteSpace(BtnCustomerSearch.Text))
		{
			SearchCustNum(Convert.ToInt32(BtnCustomerSearch.Text), 1);
		}
	}

	private void BtnOrderSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(BtnOrderSearch.Text))
		{
			SearchOrdNum(Convert.ToInt32(BtnOrderSearch.Text));
		}
	}

	private void BtnCustomerSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(BtnCustomerSearch.Text))
		{
			SearchCustNum(Convert.ToInt32(BtnCustomerSearch.Text), 1);
		}
	}

	private void MenuExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!(TxtSuppliers.Text != string.Empty))
		{
			return;
		}
		SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.Tables["TableLadger"].Clear();
		SqlCommand sqlCommand = new SqlCommand("Ledger_Account", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlCommand.Parameters.AddWithValue("@Date1", DateTime.Now.AddMonths(-1));
		sqlCommand.Parameters.AddWithValue("@Date2", DateTime.Now);
		sqlCommand.Parameters.AddWithValue("@AccountID", Convert.ToInt32(LBlSuppAccount.Text));
		sqlCommand.Parameters.AddWithValue("@TypeID", 1);
		sqlDataAdapter.Fill(dataSetALL.TableLadger);
		sqlConnection.Close();
		if (dataSetALL.Tables["TableLadger"].Rows.Count > 0)
		{
			StiReport stiReport = new StiReport();
			if (LoginDetails.Lang == "ar-EG")
			{
				stiReport.Load(Application.StartupPath + "\\Reports\\ReportLedger.mrt");
			}
			else
			{
				stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportLedger.mrt");
			}
			stiReport.RegData(dataSetALL.TableLadger);
			stiReport.Dictionary.Synchronize();
			stiReport.Compile();
			stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
			stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
			stiReport["VatNumComp"] = LoginDetails.VatNum;
			stiReport["Date1"] = DateTime.Now.AddMonths(-1);
			stiReport["Date2"] = DateTime.Now;
			stiReport.Render();
			stiReport.Show(dialogForm: true);
			FrmReportGetOrder frmReportGetOrder = new FrmReportGetOrder();
			frmReportGetOrder.stiViewerControl1.Report = stiReport;
		}
		else
		{
			MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void حركهصنففعليةToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Class_MovementItem class_MovementItem = new Class_MovementItem();
		class_MovementItem.MovementItemActive(ItmID);
	}

	private void BtnNewCustomer_Click(object sender, EventArgs e)
	{
		new FrmCustomer().Show();
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

	private decimal FUNC_DecimalNumQty(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNumQty);
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
		gridControl1.DataSource = datatable;
	}

	private void TxtNumNote_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
		{
			e.Handled = true;
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

	private void NoHusm()
	{
		TxtHasmAmount.Text = "0.00";
		TxtHasmPer.Text = "0.00";
		TxtNet.Value = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text), 1);
	}

	private void NoBounce()
	{
		TxtBounce.Text = "0.00";
		TxtNet.Value = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text), DecimalNum);
	}

	private void TxtHasmAmount_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (TxtHasmAmount.Text != string.Empty && Convert.ToDecimal(TxtHasmAmount.Text) != 0m && Convert.ToDecimal(TxtHasmAmount.Text) <= Convert.ToDecimal(TxtTotalDiscount.Text))
			{
				decimal num = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text) - Convert.ToDecimal(TxtHasmAmount.Text), DecimalNum);
				TxtNet.Value = num + Convert.ToDecimal(TxtVatDisc.Text);
				decimal num2 = Math.Round(Convert.ToDecimal(TxtHasmAmount.Text) / Convert.ToDecimal(TxtTotalDiscount.Text) * 100m, DecimalNum);
				TxtHasmPer.Value = num2;
			}
			else
			{
				NoHusm();
			}
			TafqitHusm();
		}
		catch (Exception)
		{
			NoHusm();
		}
	}

	private void TxtHasmPer_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (TxtHasmPer.Text != string.Empty && Convert.ToDecimal(TxtHasmPer.Text) != 0m && Convert.ToDecimal(TxtHasmPer.Text) <= 100m)
			{
				decimal num = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text) * Convert.ToDecimal(TxtHasmPer.Text) / 100m, 3);
				TxtHasmAmount.Text = num.ToString();
				decimal num2 = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text) - Convert.ToDecimal(TxtHasmAmount.Text), DecimalNum);
				TxtNet.Value = num2 + Convert.ToDecimal(TxtVatDisc.Text);
				decimal num3 = Math.Round(Convert.ToDecimal(TxtHasmAmount.Text) / Convert.ToDecimal(TxtTotalDiscount.Text) * 100m, DecimalNum);
				TxtHasmPer.Value = num3;
			}
			else
			{
				NoHusm();
			}
			TafqitHusm();
		}
		catch (Exception)
		{
			NoHusm();
		}
	}

	private void TxtBounce_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (TxtBounce.Text != string.Empty && Convert.ToDecimal(TxtBounce.Text) != 0m)
			{
				decimal num = Convert.ToDecimal(TxtBounce.Text);
				decimal num2 = FUNC_DecimalNum(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text));
				decimal num3 = Math.Round(num2 + num, 2);
				TxtNet.Text = num3.ToString();
			}
			else
			{
				NoBounce();
			}
		}
		catch (Exception)
		{
			NoBounce();
		}
	}

	private void BtnDisCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			DiscountCode();
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
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '+')
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

	private void TxtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
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

	private CSIDInfoDto CSIDInfoDto()
	{
		return new CSIDInfoDto
		{
			CertPem = LoginDetails.PublicKey,
			CSR = LoginDetails.Crs,
			PrivateKey = LoginDetails.PrivateKey,
			Secretkey = LoginDetails.SecretKey
		};
	}

	private InvoiceDto invoiceDto(OrderOrdersDto model)
	{
		return new InvoiceDto
		{
			ID = "Inv-" + model.PurBranchID,
			OrderId = model.PurBranchID.ToString(),
			IssueDate = model.Purchases_Date.Value.ToString("yyyy-MM-dd"),
			IssueTime = model.Purchases_Date.Value.ToString("HH:mm:ss"),
			InvoiceType = model.OrderTypeElectronicInvoiceId.Value,
			InvoiceTypeName = GenereateInvoiceTypeName(model),
			DocumentCurrencyCode = "SAR",
			TaxCurrencyCode = "SAR",
			UUID = model.PurBranchID.ToString(),
			PaymentCode = GetPaymentMethod(model),
			PayableAmount = 0m,
			InstructionNote = model.FinCash.ToString()
		};
	}

	private string GenereateInvoiceTypeName(OrderOrdersDto order)
	{
		return order.NormalOrSimpleInvoice + order.ThirdParty + order.NominalInvoice + order.ExportInvoice + order.SummaryInvoice + order.SelfInvoice;
	}

	private List<string> GetPaymentMethod(OrderOrdersDto order)
	{
		List<string> list = new List<string>();
		decimal? cashMoney = order.CashMoney;
		if ((cashMoney.GetValueOrDefault() > 0m) & cashMoney.HasValue)
		{
			list.Add("10");
		}
		else
		{
			cashMoney = order.CashBank;
			if ((cashMoney.GetValueOrDefault() > 0m) & cashMoney.HasValue)
			{
				list.Add("30");
			}
			else
			{
				cashMoney = order.BankTransfare;
				if ((cashMoney.GetValueOrDefault() > 0m) & cashMoney.HasValue)
				{
					list.Add("42");
				}
				else
				{
					list.Add("");
				}
			}
		}
		return list;
	}

	private List<InvoiceItems> invoiceItems(DataTable orderDetails)
	{
		List<InvoiceItems> list = new List<InvoiceItems>();
		foreach (DataRow row in orderDetails.Rows)
		{
			decimal num = row.Field<decimal>("ItemVatDisc");
			decimal num2 = row.Field<decimal>("UnitPriceDisc");
			decimal num3 = row.Field<decimal>("Quantity");
			decimal num4 = row.Field<decimal>("ItemVatDisc") / (row.Field<decimal>("UnitPriceDisc") * row.Field<decimal>("Quantity")) * 100m;
			list.Add(new InvoiceItems
			{
				ProductName = row["ItemName"]?.ToString(),
				DiscountValue = row.Field<decimal>("AmountDisc"),
				ProductPrice = row.Field<decimal>("UnitPriceDisc"),
				ProductQuantity = row.Field<decimal>("Quantity"),
				TotalPrice = row.Field<decimal>("SumPrice"),
				TotalWithVat = row.Field<decimal>("ItemVatDisc") + row.Field<decimal>("SumPrice"),
				TotalPriceAfterDiscount = row.Field<decimal>("Net"),
				VatCode = row["VatCode"]?.ToString(),
				TaxExemptionReason = row["TaxExemptionReason"]?.ToString(),
				TaxExemptionReasonCode = row["TaxExemptionReasonCode"]?.ToString(),
				VatValue = row.Field<decimal>("ItemVatDisc"),
				VatPercentage = Math.Round((row.Field<decimal>("Net") == 0m) ? 0m : (row.Field<decimal>("ItemVatDisc") / (row.Field<decimal>("UnitPriceDisc") * row.Field<decimal>("Quantity")) * 100m), 0)
			});
		}
		return list;
	}

	private CustomerPartyDto CustomerPartyDto(OrderOrdersDto order)
	{
		return new CustomerPartyDto
		{
			StreetName = order.Street,
			BuildingNumber = order.BuildingNum,
			CityName = order.City,
			PostalZone = order.PostalCode,
			CitySubdivisionName = order.District,
			IdentificationCode = "SA",
			RegistrationName = order.SupplierName,
			CompanyID = order.SupplierVatNum,
			Name = order.SupplierName
		};
	}

	private SupplierPartyDto supplierPartyDto(OrderOrdersDto order)
	{
		return new SupplierPartyDto
		{
			ID = LoginDetails.SupplierPartyIdentificationID,
			schemeID = LoginDetails.SupplierPartyIdentificationschemeID,
			StreetName = LoginDetails.Street,
			BuildingNumber = LoginDetails.BuildingNum,
			PostalZone = LoginDetails.PostalCode,
			CitySubdivisionName = LoginDetails.District,
			RegistrationName = LoginDetails.CompanyNameAR,
			CompanyID = LoginDetails.VatNum,
			CityName = LoginDetails.City,
			IdentificationCode = "SA"
		};
	}

	private async Task<OrderOrdersDto> GenerateOrder(int ID)
	{
		bool IsWait = toggleHold.IsOn;
		return new OrderOrdersDto
		{
			ID = ID,
			PurBranchID = ID,
			SupplierID = Convert.ToInt32(LBlSuppAccount.Text),
			IsWaiting = IsWait,
			SupplierName = TxtSuppliers.Text,
			SupplierPhone = TxtPhone.Text,
			SupplierVatNum = TxtNumVat.Text,
			Purchases_Date = Convert.ToDateTime(TxtDate.DateTime),
			Order_Paymant_Type = Convert.ToInt32(TxtType.SelectedValue),
			Restaurant_PayType = 0,
			Restaurant_TypeID = 1,
			CostCentersID = 1,
			BounceID = Convert.ToInt32(CBBounce.SelectedValue),
			Note = TxtNote.Text,
			NoteNum = TxtNumNote.Text,
			CostOrder = Math.Round(Convert.ToDecimal(TxtCost.Text), 2),
			Tax = Math.Round(Convert.ToDecimal(TxtVat.Text), 2),
			TotalPrices = Math.Round(Convert.ToDecimal(TxtTotals.Text), 2),
			Safy = Math.Round(Convert.ToDecimal(TxtTotals.Text), 2),
			DiscountNum = Math.Round(Convert.ToDecimal(TxtAllDiscount.Text), 2),
			DiscountPerantage = Math.Round(Convert.ToDecimal(txtdicoutprege.Text), 2),
			Tax_Discount = Math.Round(Convert.ToDecimal(TxtVatDisc.Text), 2),
			TotalPrices_Discount = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), 2),
			TobaccoTax = 0m,
			AllTax = Math.Round(Convert.ToDecimal(TxtVatDisc.Text), 2),
			Net = Math.Round(Convert.ToDecimal(TxtNet.Text), 2),
			CashMoney = FrmCash,
			CashBank = FrmBank,
			RoomNum = 1,
			TableNum = 1,
			Sectoral = true,
			SalesMan = TxtSales.Text,
			DateHold = DateTime.Now,
			BuildingNum = string.Empty,
			Street = TxtStreet.Text,
			District = TxtDistrict.Text,
			City = TxtCity.Text,
			Country = "السعوديه",
			PostalCode = TxtPostalCode.Text,
			CommercialRecord = TxtCommercialRecord.Text,
			OrderTypeElectronicInvoiceId = ((LoginDetails.OrderTypeElectronicInvoiceId == 0) ? 388 : LoginDetails.OrderTypeElectronicInvoiceId),
			NormalOrSimpleInvoice = (string.IsNullOrEmpty(LoginDetails.NormalOrSimpleInvoice) ? "02" : LoginDetails.NormalOrSimpleInvoice),
			ThirdParty = (LoginDetails.ThirdParty ? "1" : "0"),
			NominalInvoice = (LoginDetails.NominalInvoice ? "1" : "0"),
			ExportInvoice = (LoginDetails.ExportInvoice ? "1" : "0"),
			SummaryInvoice = (LoginDetails.SummaryInvoice ? "1" : "0"),
			SelfInvoice = (LoginDetails.SelfInvoice ? "1" : "0")
		};
	}

	private void TxtHasmAmount_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	public async Task SentToSazakt(int id)
	{
		waitForm.Show();
		if (LoginDetails.ModeInvoice == "developer")
		{
			mode = Mode.developer;
		}
		else if (LoginDetails.ModeInvoice == "Simulation")
		{
			mode = Mode.Simulation;
		}
		else if (LoginDetails.ModeInvoice == "Production")
		{
			mode = Mode.Production;
		}
		ElectronicInvoice invoiceZakat = new ElectronicInvoice();
		ResultElectronicInvoiceXml res = db.ResultElectronicInvoiceXmls.FirstOrDefault((ResultElectronicInvoiceXml s) => s.Id == id);
		if ((res?.IsValid ?? false) && (await invoiceZakat.SentInvoiceToZakat(GenerateResult(res), CSIDInfoDto(), res.InvoiceTypeName, mode)).IsSuccess)
		{
			Zakat.UpdateZakatById(res.Id, res.QRCode);
			LSucced.Visible = true;
			LSucced.Text = "Success";
			LSucced.BackColor = Color.Green;
		}
		waitForm.Close();
	}

	private Result GenerateResult(ResultElectronicInvoiceXml resultElectronic)
	{
		return new Result
		{
			QRCode = resultElectronic.QRCode,
			AllowanceTotalAmount = resultElectronic.AllowanceTotalAmount,
			ChargeTotalAmount = resultElectronic.ChargeTotalAmount,
			EncodedInvoice = resultElectronic.EncodedInvoice,
			InvoiceHash = resultElectronic.InvoiceHash,
			IsValid = resultElectronic.IsValid,
			LineExtensionAmount = resultElectronic.LineExtensionAmount,
			SingedXMLFileNameFullPath = resultElectronic.SingedXMLFileNameFullPath,
			UUID = resultElectronic.UUID,
			TaxInclusiveAmount = resultElectronic.TaxInclusiveAmount,
			PayableAmount = resultElectronic.PayableAmount,
			TaxAmount = resultElectronic.TaxAmount,
			TaxExclusiveAmount = resultElectronic.TaxExclusiveAmount,
			ErrorMessage = resultElectronic.ErrorMessage,
			SingedXMLFileName = resultElectronic.SingedXMLFileName,
			SingedXML = resultElectronic.SingedXML,
			ResultedValue = resultElectronic.ResultedValue,
			PrepaidAmount = resultElectronic.PrepaidAmount,
			SingedXMLFileNameShortPath = resultElectronic.SingedXMLFileNameShortPath,
			PIH = resultElectronic.PIH,
			Operation = resultElectronic.Operation,
			NormalXMLFileNameShortPath = resultElectronic.NormalXMLFileNameShortPath,
			NormalXMLFileNameFullPath = resultElectronic.NormalXMLFileNameFullPath
		};
	}

	private string ExportInvoiceAndUploadToServer()
	{
		try
		{
			if (string.IsNullOrWhiteSpace(TxtCode.Text))
			{
				MessageBox.Show("اختر رقم الفاتورة أولا\u064b", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return null;
			}
			SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Print_Order_Orders", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
			sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
			sqlConnection.Close();
			if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count == 0)
			{
				MessageBox.Show("لا يوجد بيانات", "طباعة", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return null;
			}
			StiReport stiReport = new StiReport();
			if (LoginDetails.Lang == "ar-EG")
			{
				stiReport.Load(Application.StartupPath + "\\Reports\\PrintOrders.mrt");
			}
			else
			{
				stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintOrders.mrt");
			}
			stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
			stiReport.Dictionary.Synchronize();
			stiReport.Compile();
			stiReport["Title"] = (string.IsNullOrWhiteSpace(TxtNumVat.Text) ? "فاتورة ضريبية مبسطة" : "فاتورة ضريبية");
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
			Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
			string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString());
			stiReport["EncHash"] = value;
			string text = $"Invoice_{TxtCode.Text}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
			string text2 = Path.Combine(Path.GetTempPath(), text);
			stiReport.Render(showProgress: false);
			stiReport.ExportDocument(StiExportFormat.Pdf, text2);
			string requestUriString = "ftp://win1135.site4now.net/" + text;
			string userName = "moelsayed";
			string password = "78789878";
			FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUriString);
			ftpWebRequest.Method = "STOR";
			ftpWebRequest.Credentials = new NetworkCredential(userName, password);
			byte[] array = File.ReadAllBytes(text2);
			ftpWebRequest.ContentLength = array.Length;
			using (Stream stream = ftpWebRequest.GetRequestStream())
			{
				stream.Write(array, 0, array.Length);
			}
			using ((FtpWebResponse)ftpWebRequest.GetResponse())
			{
			}
			File.Delete(text2);
			return "http://gtsdev-001-site28.atempurl.com/sentinvoices/" + text;
		}
		catch (Exception ex)
		{
			MessageBox.Show("⚠\ufe0f خطأ أثناء حفظ الفاتورة:\n" + ex.Message);
			return null;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmOrdersDraft));
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions8 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions9 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled10 = new DevExpress.Utils.SerializableAppearanceObject();
		this.repositoryItemButtonStores = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEditUnit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemTextEditTafqit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.TxtLocation = new DevExpress.XtraEditors.TextEdit();
		this.label25 = new System.Windows.Forms.Label();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.label14 = new System.Windows.Forms.Label();
		this.TxtQuntity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.LblNowQuintity = new System.Windows.Forms.Label();
		this.TxtAllQuantity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtLastCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtAvergCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.تخصيصToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.toggleSectoral = new DevExpress.XtraEditors.ToggleSwitch();
		this.TxtCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTabocc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label29 = new System.Windows.Forms.Label();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.ItemID = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn47 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn48 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.TxtSellPrice1 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtSellPrice2 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtSellPrice3 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtProfit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label8 = new System.Windows.Forms.Label();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotalDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
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
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.TxtNumVat = new DevExpress.XtraEditors.TextEdit();
		this.label18 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.CBBounce = new System.Windows.Forms.ComboBox();
		this.toggleHold = new DevExpress.XtraEditors.ToggleSwitch();
		this.BtnCustomerSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnNewCustomer = new DevExpress.XtraEditors.SimpleButton();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnOrderSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.StripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
		this.كشفحسابToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.حركهصنففعليةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.CkSentInvoice = new DevExpress.XtraEditors.CheckEdit();
		this.label28 = new System.Windows.Forms.Label();
		this.BtnDisCode = new DevExpress.XtraEditors.ButtonEdit();
		this.LableBouns = new System.Windows.Forms.Label();
		this.TxtBounce = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label34 = new System.Windows.Forms.Label();
		this.label38 = new System.Windows.Forms.Label();
		this.TxtDistrict = new DevExpress.XtraEditors.TextEdit();
		this.label33 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.TxtAllDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtAddress = new DevExpress.XtraEditors.TextEdit();
		this.TxtStreet = new DevExpress.XtraEditors.TextEdit();
		this.TxtHasmPer = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnOrderReturn = new DevExpress.XtraEditors.SimpleButton();
		this.BtnRecipt = new DevExpress.XtraEditors.SimpleButton();
		this.BtnShowPrice = new DevExpress.XtraEditors.SimpleButton();
		this.LblPerHasm = new System.Windows.Forms.Label();
		this.LblHasm = new System.Windows.Forms.Label();
		this.LblAmountHasm = new System.Windows.Forms.Label();
		this.TxtHasmAmount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.BtnTypeInvoic = new DevExpress.XtraEditors.SimpleButton();
		this.LValid = new System.Windows.Forms.Label();
		this.LSucced = new System.Windows.Forms.Label();
		this.BtnUpload = new DevExpress.XtraEditors.SimpleButton();
		this.label22 = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label42 = new System.Windows.Forms.Label();
		this.label43 = new System.Windows.Forms.Label();
		this.TxtAdditionalNum = new DevExpress.XtraEditors.TextEdit();
		this.TxtCommercialRecord = new DevExpress.XtraEditors.TextEdit();
		this.TxtPostalCode = new DevExpress.XtraEditors.TextEdit();
		this.TxtCity = new DevExpress.XtraEditors.TextEdit();
		this.label37 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label46 = new System.Windows.Forms.Label();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.TxtSales = new DevExpress.XtraEditors.TextEdit();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.label6 = new System.Windows.Forms.Label();
		this.CBSaleTybe = new System.Windows.Forms.ComboBox();
		this.label48 = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.Comp_Store = new System.Windows.Forms.ComboBox();
		this.label39 = new System.Windows.Forms.Label();
		this.CBProject = new System.Windows.Forms.ComboBox();
		this.DateHold = new DevExpress.XtraEditors.DateEdit();
		this.LblDataHold = new System.Windows.Forms.Label();
		this.LblCustLast = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonStores).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonDelete).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEditTafqit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.toggleSectoral.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
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
		((System.ComponentModel.ISupportInitialize)this.toggleHold.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.CkSentInvoice.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnDisCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDistrict.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtStreet.Properties).BeginInit();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtAdditionalNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales.Properties).BeginInit();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions10");
		this.repositoryItemButtonStores.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonStores.Buttons"), resources.GetString("repositoryItemButtonStores.Buttons1"), (int)resources.GetObject("repositoryItemButtonStores.Buttons2"), (bool)resources.GetObject("repositoryItemButtonStores.Buttons3"), (bool)resources.GetObject("repositoryItemButtonStores.Buttons4"), (bool)resources.GetObject("repositoryItemButtonStores.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonStores.Buttons6"), resources.GetObject("repositoryItemButtonStores.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonStores.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonStores.Buttons9"))
		});
		this.repositoryItemButtonStores.Name = "repositoryItemButtonStores";
		this.repositoryItemButtonStores.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonStores_ButtonClick);
		resources.ApplyResources(this.repositoryItemButtonEditCodeSearch, "repositoryItemButtonEditCodeSearch");
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions11");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons"), resources.GetString("repositoryItemButtonEditCodeSearch.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEditCodeSearch.Buttons6"), resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons9"))
		});
		this.repositoryItemButtonEditCodeSearch.Name = "repositoryItemButtonEditCodeSearch";
		this.repositoryItemButtonEditCodeSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEditCodeSearch_ButtonClick);
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions12");
		this.repositoryItemButtonEditUnit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditUnit.Buttons"), resources.GetString("repositoryItemButtonEditUnit.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditUnit.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditUnit.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditUnit.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditUnit.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEditUnit.Buttons6"), resources.GetObject("repositoryItemButtonEditUnit.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditUnit.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditUnit.Buttons9"))
		});
		this.repositoryItemButtonEditUnit.Name = "repositoryItemButtonEditUnit";
		this.repositoryItemButtonEditUnit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonUnits_ButtonClick);
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions13");
		this.repositoryItemButtonDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonDelete.Buttons"), resources.GetString("repositoryItemButtonDelete.Buttons1"), (int)resources.GetObject("repositoryItemButtonDelete.Buttons2"), (bool)resources.GetObject("repositoryItemButtonDelete.Buttons3"), (bool)resources.GetObject("repositoryItemButtonDelete.Buttons4"), (bool)resources.GetObject("repositoryItemButtonDelete.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonDelete.Buttons6"), resources.GetObject("repositoryItemButtonDelete.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonDelete.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonDelete.Buttons9"))
		});
		this.repositoryItemButtonDelete.Name = "repositoryItemButtonDelete";
		this.repositoryItemButtonDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonDelete_ButtonPressed);
		this.repositoryItemButtonEdit7.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit7.Buttons"), resources.GetString("repositoryItemButtonEdit7.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit7.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("repositoryItemButtonEdit7.Buttons6"), resources.GetObject("repositoryItemButtonEdit7.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit7.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit7.Buttons9"))
		});
		this.repositoryItemButtonEdit7.Name = "repositoryItemButtonEdit7";
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions14");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"), resources.GetString("repositoryItemButtonEdit5.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit5.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("repositoryItemButtonEdit5.Buttons6"), resources.GetObject("repositoryItemButtonEdit5.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit5.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit5.Buttons9"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
		editorButtonImageOptions6.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions15");
		editorButtonImageOptions6.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemTextEditTafqit.Name = "repositoryItemTextEditTafqit";
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit6, "repositoryItemButtonEdit6");
		resources.ApplyResources(editorButtonImageOptions7, "editorButtonImageOptions16");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
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
		resources.ApplyResources(this.LblNowQuintity, "LblNowQuintity");
		this.LblNowQuintity.Name = "LblNowQuintity";
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
		this.gridColumn4.ColumnEdit = this.repositoryItemButtonStores;
		this.gridColumn4.FieldName = "StoreName";
		this.gridColumn4.MinWidth = 27;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.تخصيصToolStripMenuItem1, "تخصيصToolStripMenuItem1");
		this.تخصيصToolStripMenuItem1.Name = "تخصيصToolStripMenuItem1";
		this.تخصيصToolStripMenuItem1.Click += new System.EventHandler(تخصيصToolStripMenuItem1_Click);
		resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		resources.ApplyResources(this.MenuExit, "MenuExit");
		this.MenuExit.Name = "MenuExit";
		this.MenuExit.Click += new System.EventHandler(MenuExit_Click);
		resources.ApplyResources(this.groupBox3, "groupBox3");
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.label27);
		this.groupBox3.Controls.Add(this.toggleSectoral);
		this.groupBox3.Controls.Add(this.TxtCost);
		this.groupBox3.Controls.Add(this.TxtTabocc);
		this.groupBox3.Controls.Add(this.label29);
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Controls.Add(this.TxtQuntity);
		this.groupBox3.Controls.Add(this.LblNowQuintity);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.TabStop = false;
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.label27, "label27");
		this.label27.Name = "label27";
		resources.ApplyResources(this.toggleSectoral, "toggleSectoral");
		this.toggleSectoral.Name = "toggleSectoral";
		this.toggleSectoral.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleSectoral.Properties.Appearance.Font");
		this.toggleSectoral.Properties.Appearance.Options.UseFont = true;
		this.toggleSectoral.Properties.Appearance.Options.UseForeColor = true;
		this.toggleSectoral.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleSectoral.Properties.GlyphVerticalAlignment");
		this.toggleSectoral.Properties.OffText = resources.GetString("toggleSectoral.Properties.OffText");
		this.toggleSectoral.Properties.OnText = resources.GetString("toggleSectoral.Properties.OnText");
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
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView1.Appearance.HeaderPanel.Font");
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		resources.ApplyResources(this.gridView1, "gridView1");
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[42]
		{
			this.gridColumn1, this.gridColumn2, this.gridColumn20, this.gridColumn21, this.gridColumn27, this.shearch, this.gridColumn4, this.gridColumn23, this.gridColumn24, this.gridColumn25,
			this.gridColumn26, this.gridColumn36, this.gridColumn28, this.gridColumn3, this.gridColumn6, this.gridColumn7, this.gridColumn8, this.gridColumn22, this.gridColumn5, this.gridColumn9,
			this.gridColumn29, this.gridColumn30, this.gridColumn31, this.gridColumn32, this.gridColumn37, this.gridColumn33, this.gridColumn34, this.ItemID, this.gridColumn35, this.gridColumn38,
			this.gridColumn39, this.gridColumn40, this.gridColumn41, this.gridColumn42, this.gridColumn43, this.gridColumn19, this.gridColumn44, this.gridColumn45, this.gridColumn46, this.gridColumn47,
			this.gridColumn48, this.gridColumn49
		});
		this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(831, 81, 343, 706);
		this.gridView1.DetailHeight = 530;
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.HorzScrollStep = 2;
		this.gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
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
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridView1.OptionsView.ShowGroupPanel = false;
		this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
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
		this.gridColumn1.MinWidth = 27;
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
		this.gridColumn2.MinWidth = 27;
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn20.MinWidth = 27;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn20.Summary"), resources.GetString("gridColumn20.Summary1"), resources.GetString("gridColumn20.Summary2"))
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
		this.gridColumn21.MinWidth = 27;
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn27.FieldName = "ItemNote";
		this.gridColumn27.MinWidth = 33;
		this.gridColumn27.Name = "gridColumn27";
		this.gridColumn27.OptionsColumn.FixedWidth = true;
		this.gridColumn27.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.shearch.ColumnEdit = this.repositoryItemButtonEditUnit;
		this.shearch.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.FieldName = "UnitName";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.MinWidth = 27;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		this.shearch.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn23.FieldName = "AvergItem";
		this.gridColumn23.MinWidth = 27;
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn24.FieldName = "SumAvergItem";
		this.gridColumn24.MinWidth = 27;
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn24.OptionsColumn.AllowFocus = false;
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
		this.gridColumn25.FieldName = "UnitNameTF";
		this.gridColumn25.MinWidth = 33;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.OptionsColumn.AllowFocus = false;
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
		this.gridColumn26.ColumnEdit = this.repositoryItemTextEditTafqit;
		this.gridColumn26.FieldName = "QuantityTF";
		this.gridColumn26.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn26.MinWidth = 33;
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
		this.gridColumn26.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn26.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn36, "gridColumn36");
		this.gridColumn36.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceCell.Font");
		this.gridColumn36.AppearanceCell.Options.UseFont = true;
		this.gridColumn36.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn36.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn36.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceHeader.Font");
		this.gridColumn36.AppearanceHeader.Options.UseFont = true;
		this.gridColumn36.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn36.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn36.FieldName = "Net";
		this.gridColumn36.MinWidth = 33;
		this.gridColumn36.Name = "gridColumn36";
		this.gridColumn36.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn36.Summary"), resources.GetString("gridColumn36.Summary1"), resources.GetString("gridColumn36.Summary2"))
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
		this.gridColumn28.FieldName = "AmountDisc";
		this.gridColumn28.MinWidth = 27;
		this.gridColumn28.Name = "gridColumn28";
		this.gridColumn28.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn3.MinWidth = 27;
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
		this.gridColumn6.MinWidth = 27;
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
		this.gridColumn7.FieldName = "Total";
		this.gridColumn7.MinWidth = 27;
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
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonDelete;
		this.gridColumn8.FieldName = "DeleteItem";
		this.gridColumn8.MinWidth = 27;
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
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
		this.gridColumn22.MinWidth = 27;
		this.gridColumn22.Name = "gridColumn22";
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
		this.gridColumn5.MinWidth = 27;
		this.gridColumn5.Name = "gridColumn5";
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
		this.gridColumn9.MinWidth = 27;
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn29.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn29.FieldName = "PercentDisc";
		this.gridColumn29.MinWidth = 27;
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
		this.gridColumn30.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn30.FieldName = "BounceItem";
		this.gridColumn30.MinWidth = 27;
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
		this.gridColumn31.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn31.FieldName = "Height";
		this.gridColumn31.MinWidth = 27;
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
		this.gridColumn32.FieldName = "Width";
		this.gridColumn32.MinWidth = 27;
		this.gridColumn32.Name = "gridColumn32";
		this.gridColumn32.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn37.FieldName = "Profit";
		this.gridColumn37.MinWidth = 34;
		this.gridColumn37.Name = "gridColumn37";
		this.gridColumn37.OptionsColumn.AllowEdit = false;
		this.gridColumn37.OptionsColumn.AllowFocus = false;
		this.gridColumn37.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn33.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn33.FieldName = "Long";
		this.gridColumn33.MinWidth = 27;
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
		this.gridColumn34.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn34.FieldName = "Weight";
		this.gridColumn34.MinWidth = 27;
		this.gridColumn34.Name = "gridColumn34";
		this.gridColumn34.OptionsColumn.FixedWidth = true;
		this.gridColumn34.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.ItemID, "ItemID");
		this.ItemID.FieldName = "ItemID";
		this.ItemID.MinWidth = 27;
		this.ItemID.Name = "ItemID";
		this.ItemID.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn35.FieldName = "UnitType";
		this.gridColumn35.MinWidth = 28;
		this.gridColumn35.Name = "gridColumn35";
		this.gridColumn35.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn38.MinWidth = 28;
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
		this.gridColumn39.FieldName = "TafqitID";
		this.gridColumn39.MinWidth = 28;
		this.gridColumn39.Name = "gridColumn39";
		this.gridColumn39.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn40, "gridColumn40");
		this.gridColumn40.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn40.AppearanceCell.Font");
		this.gridColumn40.AppearanceCell.GradientMode = (System.Drawing.Drawing2D.LinearGradientMode)resources.GetObject("gridColumn40.AppearanceCell.GradientMode");
		this.gridColumn40.AppearanceCell.Options.UseFont = true;
		this.gridColumn40.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn40.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn40.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn40.AppearanceHeader.Font");
		this.gridColumn40.AppearanceHeader.Options.UseFont = true;
		this.gridColumn40.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn40.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn40.FieldName = "UnitNum";
		this.gridColumn40.MinWidth = 28;
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
		this.gridColumn41.FieldName = "StoreID";
		this.gridColumn41.MinWidth = 28;
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
		this.gridColumn42.FieldName = "OrginalPrice";
		this.gridColumn42.MinWidth = 28;
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
		this.gridColumn43.FieldName = "UnitPriceDisc";
		this.gridColumn43.MinWidth = 28;
		this.gridColumn43.Name = "gridColumn43";
		this.gridColumn43.OptionsColumn.AllowEdit = false;
		this.gridColumn43.OptionsColumn.AllowFocus = false;
		this.gridColumn43.OptionsColumn.ReadOnly = true;
		this.gridColumn43.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
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
		this.gridColumn19.FieldName = "Tax_Value";
		this.gridColumn19.MinWidth = 28;
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn44.FieldName = "Done";
		this.gridColumn44.MinWidth = 28;
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
		this.gridColumn45.FieldName = "InstallationPrice";
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
		this.gridColumn46.FieldName = "SumInstallationDis";
		this.gridColumn46.MinWidth = 29;
		this.gridColumn46.Name = "gridColumn46";
		this.gridColumn46.OptionsColumn.AllowEdit = false;
		this.gridColumn46.OptionsColumn.AllowFocus = false;
		this.gridColumn46.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn47, "gridColumn47");
		this.gridColumn47.FieldName = "VatCode";
		this.gridColumn47.MinWidth = 25;
		this.gridColumn47.Name = "gridColumn47";
		this.gridColumn47.OptionsColumn.AllowEdit = false;
		this.gridColumn47.OptionsColumn.AllowFocus = false;
		this.gridColumn47.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn48, "gridColumn48");
		this.gridColumn48.FieldName = "TaxExemptionReason";
		this.gridColumn48.MinWidth = 25;
		this.gridColumn48.Name = "gridColumn48";
		this.gridColumn48.OptionsColumn.AllowEdit = false;
		this.gridColumn48.OptionsColumn.AllowFocus = false;
		this.gridColumn48.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn49, "gridColumn49");
		this.gridColumn49.FieldName = "TaxExemptionReasonCode";
		this.gridColumn49.MinWidth = 25;
		this.gridColumn49.Name = "gridColumn49";
		this.gridColumn49.OptionsColumn.AllowEdit = false;
		this.gridColumn49.OptionsColumn.AllowFocus = false;
		this.gridColumn49.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.cardView1, "cardView1");
		this.cardView1.CardWidth = 271;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.DetailHeight = 530;
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		resources.ApplyResources(this.gridColumn10, "gridColumn10");
		this.gridColumn10.MinWidth = 27;
		this.gridColumn10.Name = "gridColumn10";
		resources.ApplyResources(this.gridColumn11, "gridColumn11");
		this.gridColumn11.MinWidth = 27;
		this.gridColumn11.Name = "gridColumn11";
		resources.ApplyResources(this.gridColumn12, "gridColumn12");
		this.gridColumn12.MinWidth = 27;
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn13, "gridColumn13");
		this.gridColumn13.MinWidth = 27;
		this.gridColumn13.Name = "gridColumn13";
		resources.ApplyResources(this.gridColumn14, "gridColumn14");
		this.gridColumn14.MinWidth = 27;
		this.gridColumn14.Name = "gridColumn14";
		resources.ApplyResources(this.gridColumn15, "gridColumn15");
		this.gridColumn15.MinWidth = 27;
		this.gridColumn15.Name = "gridColumn15";
		resources.ApplyResources(this.gridColumn16, "gridColumn16");
		this.gridColumn16.MinWidth = 27;
		this.gridColumn16.Name = "gridColumn16";
		resources.ApplyResources(this.gridColumn17, "gridColumn17");
		this.gridColumn17.MinWidth = 27;
		this.gridColumn17.Name = "gridColumn17";
		resources.ApplyResources(this.gridColumn18, "gridColumn18");
		this.gridColumn18.MinWidth = 27;
		this.gridColumn18.Name = "gridColumn18";
		resources.ApplyResources(this.TxtSellPrice1, "TxtSellPrice1");
		this.TxtSellPrice1.DecimalDigits = 3;
		this.TxtSellPrice1.ForeColor = System.Drawing.Color.Black;
		this.TxtSellPrice1.Name = "TxtSellPrice1";
		this.TxtSellPrice1.ReadOnly = true;
		this.TxtSellPrice1.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtSellPrice1.Value = new decimal(new int[4]);
		this.TxtSellPrice1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtSellPrice2, "TxtSellPrice2");
		this.TxtSellPrice2.DecimalDigits = 3;
		this.TxtSellPrice2.ForeColor = System.Drawing.Color.Black;
		this.TxtSellPrice2.Name = "TxtSellPrice2";
		this.TxtSellPrice2.ReadOnly = true;
		this.TxtSellPrice2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtSellPrice2.Value = new decimal(new int[4]);
		this.TxtSellPrice2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.label11, "label11");
		this.label11.Name = "label11";
		resources.ApplyResources(this.TxtSellPrice3, "TxtSellPrice3");
		this.TxtSellPrice3.DecimalDigits = 3;
		this.TxtSellPrice3.ForeColor = System.Drawing.Color.Black;
		this.TxtSellPrice3.Name = "TxtSellPrice3";
		this.TxtSellPrice3.ReadOnly = true;
		this.TxtSellPrice3.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtSellPrice3.Value = new decimal(new int[4]);
		this.TxtSellPrice3.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.TxtProfit, "TxtProfit");
		this.TxtProfit.DecimalDigits = 3;
		this.TxtProfit.ForeColor = System.Drawing.Color.Black;
		this.TxtProfit.Name = "TxtProfit";
		this.TxtProfit.ReadOnly = true;
		this.TxtProfit.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtProfit.Value = new decimal(new int[4]);
		this.TxtProfit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		resources.ApplyResources(this.BtnPrint, "BtnPrint");
		this.BtnPrint.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrint.Appearance.Font");
		this.BtnPrint.Appearance.Options.UseFont = true;
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
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtTotals, "TxtTotals");
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.ReadOnly = true;
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotals.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		this.TxtNumNote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumNote_KeyPress);
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions17");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance9, appearanceHovered9, appearancePressed9, appearanceDisabled9, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions9.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		editorButtonImageOptions9.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions9, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance10, appearanceHovered10, appearancePressed10, appearanceDisabled10, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		resources.ApplyResources(this.label23, "label23");
		this.label23.Name = "label23";
		this.label23.Click += new System.EventHandler(label23_Click);
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNote.Properties.Appearance.Font");
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNote.EditValueChanged += new System.EventHandler(TxtNote_EditValueChanged);
		this.TxtNote.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNote.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtPhone, "TxtPhone");
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPhone.Properties.Appearance.Font");
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtPhone_KeyDown);
		this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPhone_KeyPress);
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.TxtNumVat, "TxtNumVat");
		this.TxtNumVat.Name = "TxtNumVat";
		this.TxtNumVat.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumVat.Properties.Appearance.Font");
		this.TxtNumVat.Properties.Appearance.Options.UseFont = true;
		this.TxtNumVat.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumVat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumVat.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNumVat.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtNumVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumVat_KeyPress);
		resources.ApplyResources(this.label18, "label18");
		this.label18.Name = "label18";
		resources.ApplyResources(this.label31, "label31");
		this.label31.Name = "label31";
		resources.ApplyResources(this.CBBounce, "CBBounce");
		this.CBBounce.FormattingEnabled = true;
		this.CBBounce.Name = "CBBounce";
		this.CBBounce.SelectedIndexChanged += new System.EventHandler(CBBounce_SelectedIndexChanged);
		resources.ApplyResources(this.toggleHold, "toggleHold");
		this.toggleHold.Name = "toggleHold";
		this.toggleHold.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleHold.Properties.Appearance.Font");
		this.toggleHold.Properties.Appearance.Options.UseFont = true;
		this.toggleHold.Properties.Appearance.Options.UseForeColor = true;
		this.toggleHold.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleHold.Properties.GlyphVerticalAlignment");
		this.toggleHold.Properties.OffText = resources.GetString("toggleHold.Properties.OffText");
		this.toggleHold.Properties.OnText = resources.GetString("toggleHold.Properties.OnText");
		this.toggleHold.Properties.Toggled += new System.EventHandler(toggleHold_Properties_Toggled);
		this.toggleHold.Toggled += new System.EventHandler(toggleHold_Toggled);
		this.toggleHold.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlTxtSuppliers_KeyDown);
		resources.ApplyResources(this.BtnCustomerSearch, "BtnCustomerSearch");
		this.BtnCustomerSearch.Name = "BtnCustomerSearch";
		this.BtnCustomerSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnCustomerSearch.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCustomerSearch.Properties.Appearance.Font");
		this.BtnCustomerSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnCustomerSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnCustomerSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnCustomerSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnCustomerSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCustomerSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnCustomerSearch.Properties.Buttons"))
		});
		this.BtnCustomerSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCustomerSearch.Properties.NullText = resources.GetString("BtnCustomerSearch.Properties.NullText");
		this.BtnCustomerSearch.Tag = "Search";
		this.BtnCustomerSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnCustomerSearch_ButtonClick);
		this.BtnCustomerSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnCustomerSearch_KeyDown);
		this.BtnCustomerSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.BtnNewCustomer, "BtnNewCustomer");
		this.BtnNewCustomer.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNewCustomer.Appearance.Font");
		this.BtnNewCustomer.Appearance.Options.UseFont = true;
		this.BtnNewCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewCustomer.ImageOptions.Image");
		this.BtnNewCustomer.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewCustomer.Name = "BtnNewCustomer";
		this.BtnNewCustomer.Click += new System.EventHandler(BtnNewCustomer_Click);
		this.BtnNewCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.LBlSuppAccount, "LBlSuppAccount");
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		resources.ApplyResources(this.label20, "label20");
		this.label20.Name = "label20";
		resources.ApplyResources(this.TxtType, "TxtType");
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Name = "TxtType";
		this.TxtType.SelectedIndexChanged += new System.EventHandler(TxtType_SelectedIndexChanged_1);
		this.TxtType.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
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
		this.TxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		this.BtnOrderSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.repositoryItemButtonEditCode, "repositoryItemButtonEditCode");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCode.Buttons"))
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
		resources.ApplyResources(this.StripMenuItem1, "StripMenuItem1");
		this.StripMenuItem1.Name = "StripMenuItem1";
		this.StripMenuItem1.Click += new System.EventHandler(StripMenuItem1_Click);
		resources.ApplyResources(this.MenuPurchOrder, "MenuPurchOrder");
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		this.MenuPurchOrder.Click += new System.EventHandler(MenuPurchOrder_Click);
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
		this.MenuPrintQued.Click += new System.EventHandler(MenuPrintQued_Click);
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
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[18]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.طباعةToolStripMenuItem1, this.toolStripSeparator1, this.كشفحسابToolStripMenuItem, this.حركةصنفToolStripMenuItem, this.حركهصنففعليةToolStripMenuItem, this.MenuNewItem,
			this.MenuSearchItem, this.toolStripSeparator4, this.MenuPurchOrder, this.StripMenuItem1, this.toolStripSeparator2, this.تخصيصToolStripMenuItem1, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		resources.ApplyResources(this.كشفحسابToolStripMenuItem, "كشفحسابToolStripMenuItem");
		this.كشفحسابToolStripMenuItem.Name = "كشفحسابToolStripMenuItem";
		this.كشفحسابToolStripMenuItem.Click += new System.EventHandler(كشفحسابToolStripMenuItem_Click);
		resources.ApplyResources(this.حركهصنففعليةToolStripMenuItem, "حركهصنففعليةToolStripMenuItem");
		this.حركهصنففعليةToolStripMenuItem.Name = "حركهصنففعليةToolStripMenuItem";
		this.حركهصنففعليةToolStripMenuItem.Click += new System.EventHandler(حركهصنففعليةToolStripMenuItem_Click);
		this.ribbonPage2.Name = "ribbonPage2";
		resources.ApplyResources(this.ribbonPage2, "ribbonPage2");
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.CkSentInvoice);
		this.groupBox2.Controls.Add(this.label28);
		this.groupBox2.Controls.Add(this.BtnDisCode);
		this.groupBox2.Controls.Add(this.LableBouns);
		this.groupBox2.Controls.Add(this.TxtBounce);
		this.groupBox2.Controls.Add(this.label34);
		this.groupBox2.Controls.Add(this.TxtVat);
		this.groupBox2.Controls.Add(this.label38);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.TxtDistrict);
		this.groupBox2.Controls.Add(this.label33);
		this.groupBox2.Controls.Add(this.label32);
		this.groupBox2.Controls.Add(this.TxtAllDiscount);
		this.groupBox2.Controls.Add(this.TxtLocation);
		this.groupBox2.Controls.Add(this.TxtTafqit);
		this.groupBox2.Controls.Add(this.label25);
		this.groupBox2.Controls.Add(this.TxtSellPrice1);
		this.groupBox2.Controls.Add(this.TxtAddress);
		this.groupBox2.Controls.Add(this.TxtStreet);
		this.groupBox2.Controls.Add(this.TxtSellPrice2);
		this.groupBox2.Controls.Add(this.TxtHasmPer);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.txtdicoutprege);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtNet);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.TxtSellPrice3);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnOrderReturn);
		this.groupBox2.Controls.Add(this.TxtProfit);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.label16);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.TxtAvergCost);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.BtnRecipt);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.BtnShowPrice);
		this.groupBox2.Controls.Add(this.TxtLastCost);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.TxtAllQuantity);
		this.groupBox2.Controls.Add(this.TxtTotals);
		this.groupBox2.Controls.Add(this.TxtTotalDiscount);
		this.groupBox2.Controls.Add(this.LblPerHasm);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.LblHasm);
		this.groupBox2.Controls.Add(this.LblAmountHasm);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.TxtHasmAmount);
		this.groupBox2.Controls.Add(this.TxtDiscount);
		this.groupBox2.Controls.Add(this.TxtVatDisc);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		this.groupBox2.Enter += new System.EventHandler(groupBox2_Enter);
		resources.ApplyResources(this.CkSentInvoice, "CkSentInvoice");
		this.CkSentInvoice.Name = "CkSentInvoice";
		this.CkSentInvoice.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("CkSentInvoice.Properties.Appearance.Font");
		this.CkSentInvoice.Properties.Appearance.Options.UseFont = true;
		this.CkSentInvoice.Properties.Caption = resources.GetString("CkSentInvoice.Properties.Caption");
		this.CkSentInvoice.Properties.DisplayValueChecked = resources.GetString("CkSentInvoice.Properties.DisplayValueChecked");
		this.CkSentInvoice.Properties.DisplayValueGrayed = resources.GetString("CkSentInvoice.Properties.DisplayValueGrayed");
		this.CkSentInvoice.Properties.DisplayValueUnchecked = resources.GetString("CkSentInvoice.Properties.DisplayValueUnchecked");
		this.CkSentInvoice.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("CkSentInvoice.Properties.GlyphVerticalAlignment");
		this.CkSentInvoice.CheckedChanged += new System.EventHandler(CkSentInvoice_CheckedChanged);
		resources.ApplyResources(this.label28, "label28");
		this.label28.Name = "label28";
		resources.ApplyResources(this.BtnDisCode, "BtnDisCode");
		this.BtnDisCode.Name = "BtnDisCode";
		this.BtnDisCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnDisCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDisCode.Properties.Appearance.Font");
		this.BtnDisCode.Properties.Appearance.Options.UseBackColor = true;
		this.BtnDisCode.Properties.Appearance.Options.UseFont = true;
		this.BtnDisCode.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnDisCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnDisCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnDisCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnDisCode.Properties.Buttons"))
		});
		this.BtnDisCode.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnDisCode.Properties.NullText = resources.GetString("BtnDisCode.Properties.NullText");
		this.BtnDisCode.Tag = "";
		this.BtnDisCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnDisCode_ButtonClick);
		this.BtnDisCode.KeyUp += new System.Windows.Forms.KeyEventHandler(BtnDisCode_KeyUp);
		resources.ApplyResources(this.LableBouns, "LableBouns");
		this.LableBouns.Name = "LableBouns";
		resources.ApplyResources(this.TxtBounce, "TxtBounce");
		this.TxtBounce.DecimalDigits = 3;
		this.TxtBounce.ForeColor = System.Drawing.Color.Black;
		this.TxtBounce.Name = "TxtBounce";
		this.TxtBounce.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtBounce.Value = new decimal(new int[4]);
		this.TxtBounce.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtBounce.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtBounce_KeyUp);
		resources.ApplyResources(this.label34, "label34");
		this.label34.Name = "label34";
		resources.ApplyResources(this.label38, "label38");
		this.label38.Name = "label38";
		resources.ApplyResources(this.TxtDistrict, "TxtDistrict");
		this.TxtDistrict.Name = "TxtDistrict";
		this.TxtDistrict.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDistrict.Properties.Appearance.Font");
		this.TxtDistrict.Properties.Appearance.Options.UseFont = true;
		this.TxtDistrict.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDistrict.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDistrict.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtDistrict.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label33, "label33");
		this.label33.Name = "label33";
		resources.ApplyResources(this.label32, "label32");
		this.label32.Name = "label32";
		resources.ApplyResources(this.TxtAllDiscount, "TxtAllDiscount");
		this.TxtAllDiscount.DecimalDigits = 3;
		this.TxtAllDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtAllDiscount.Name = "TxtAllDiscount";
		this.TxtAllDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllDiscount.Value = new decimal(new int[4]);
		this.TxtAllDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtAllDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtAllDiscount_KeyUp);
		resources.ApplyResources(this.TxtAddress, "TxtAddress");
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAddress.Properties.Appearance.Font");
		this.TxtAddress.Properties.Appearance.Options.UseFont = true;
		this.TxtAddress.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAddress.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAddress.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtStreet, "TxtStreet");
		this.TxtStreet.Name = "TxtStreet";
		this.TxtStreet.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtStreet.Properties.Appearance.Font");
		this.TxtStreet.Properties.Appearance.Options.UseFont = true;
		this.TxtStreet.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtStreet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtStreet.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtStreet.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtHasmPer, "TxtHasmPer");
		this.TxtHasmPer.DecimalDigits = 3;
		this.TxtHasmPer.ForeColor = System.Drawing.Color.Black;
		this.TxtHasmPer.Name = "TxtHasmPer";
		this.TxtHasmPer.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtHasmPer.Value = new decimal(new int[4]);
		this.TxtHasmPer.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtHasmPer.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtHasmPer_KeyUp);
		resources.ApplyResources(this.BtnOrderReturn, "BtnOrderReturn");
		this.BtnOrderReturn.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnOrderReturn.Appearance.Font");
		this.BtnOrderReturn.Appearance.Options.UseFont = true;
		this.BtnOrderReturn.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOrderReturn.ImageOptions.Image");
		this.BtnOrderReturn.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnOrderReturn.Name = "BtnOrderReturn";
		this.BtnOrderReturn.Click += new System.EventHandler(BtnOrderReturn_Click);
		this.BtnOrderReturn.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnRecipt, "BtnRecipt");
		this.BtnRecipt.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnRecipt.Appearance.Font");
		this.BtnRecipt.Appearance.Options.UseFont = true;
		this.BtnRecipt.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRecipt.ImageOptions.Image");
		this.BtnRecipt.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnRecipt.Name = "BtnRecipt";
		this.BtnRecipt.Click += new System.EventHandler(BtnRecipt_Click);
		this.BtnRecipt.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnShowPrice, "BtnShowPrice");
		this.BtnShowPrice.Appearance.Options.UseFont = true;
		this.BtnShowPrice.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnShowPrice.ImageOptions.Image");
		this.BtnShowPrice.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnShowPrice.Name = "BtnShowPrice";
		this.BtnShowPrice.Click += new System.EventHandler(BtnShowPrice_Click);
		this.BtnShowPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.LblPerHasm, "LblPerHasm");
		this.LblPerHasm.Name = "LblPerHasm";
		resources.ApplyResources(this.LblHasm, "LblHasm");
		this.LblHasm.ForeColor = System.Drawing.Color.Brown;
		this.LblHasm.Name = "LblHasm";
		resources.ApplyResources(this.LblAmountHasm, "LblAmountHasm");
		this.LblAmountHasm.Name = "LblAmountHasm";
		resources.ApplyResources(this.TxtHasmAmount, "TxtHasmAmount");
		this.TxtHasmAmount.DecimalDigits = 3;
		this.TxtHasmAmount.ForeColor = System.Drawing.Color.Black;
		this.TxtHasmAmount.Name = "TxtHasmAmount";
		this.TxtHasmAmount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtHasmAmount.Value = new decimal(new int[4]);
		this.TxtHasmAmount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtHasmAmount.TextChanged += new System.EventHandler(TxtHasmAmount_TextChanged);
		this.TxtHasmAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtHasmAmount_KeyDown);
		this.TxtHasmAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtHasmAmount_KeyUp);
		resources.ApplyResources(this.groupBox4, "groupBox4");
		this.groupBox4.Controls.Add(this.BtnTypeInvoic);
		this.groupBox4.Controls.Add(this.LValid);
		this.groupBox4.Controls.Add(this.LSucced);
		this.groupBox4.Controls.Add(this.BtnCustomerSearch);
		this.groupBox4.Controls.Add(this.BtnUpload);
		this.groupBox4.Controls.Add(this.BtnNewCustomer);
		this.groupBox4.Controls.Add(this.label22);
		this.groupBox4.Controls.Add(this.label35);
		this.groupBox4.Controls.Add(this.label19);
		this.groupBox4.Controls.Add(this.label42);
		this.groupBox4.Controls.Add(this.label43);
		this.groupBox4.Controls.Add(this.TxtAdditionalNum);
		this.groupBox4.Controls.Add(this.TxtCommercialRecord);
		this.groupBox4.Controls.Add(this.TxtPostalCode);
		this.groupBox4.Controls.Add(this.TxtCity);
		this.groupBox4.Controls.Add(this.LBlSuppAccount);
		this.groupBox4.Controls.Add(this.LblSup);
		this.groupBox4.Controls.Add(this.label37);
		this.groupBox4.Controls.Add(this.label15);
		this.groupBox4.Controls.Add(this.label46);
		this.groupBox4.Controls.Add(this.TxtPhone);
		this.groupBox4.Controls.Add(this.TxtCharge);
		this.groupBox4.Controls.Add(this.TxtSales);
		this.groupBox4.Controls.Add(this.TxtSuppliers);
		this.groupBox4.Controls.Add(this.TxtNumVat);
		this.groupBox4.Controls.Add(this.label18);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.TabStop = false;
		resources.ApplyResources(this.BtnTypeInvoic, "BtnTypeInvoic");
		this.BtnTypeInvoic.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnTypeInvoic.Appearance.Font");
		this.BtnTypeInvoic.Appearance.Options.UseFont = true;
		this.BtnTypeInvoic.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnTypeInvoic.ImageOptions.Image");
		this.BtnTypeInvoic.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnTypeInvoic.Name = "BtnTypeInvoic";
		this.BtnTypeInvoic.Click += new System.EventHandler(BtnTypeInvoic_Click);
		resources.ApplyResources(this.LValid, "LValid");
		this.LValid.BackColor = System.Drawing.Color.Green;
		this.LValid.ForeColor = System.Drawing.Color.White;
		this.LValid.Name = "LValid";
		resources.ApplyResources(this.LSucced, "LSucced");
		this.LSucced.BackColor = System.Drawing.Color.Green;
		this.LSucced.ForeColor = System.Drawing.Color.White;
		this.LSucced.Name = "LSucced";
		resources.ApplyResources(this.BtnUpload, "BtnUpload");
		this.BtnUpload.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnUpload.Appearance.Font");
		this.BtnUpload.Appearance.Options.UseFont = true;
		this.BtnUpload.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUpload.ImageOptions.Image");
		this.BtnUpload.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUpload.Name = "BtnUpload";
		this.BtnUpload.Click += new System.EventHandler(BtnUpload_Click);
		resources.ApplyResources(this.label22, "label22");
		this.label22.Name = "label22";
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
		this.TxtAdditionalNum.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtAdditionalNum.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtAdditionalNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtAdditionalNum_KeyPress);
		resources.ApplyResources(this.TxtCommercialRecord, "TxtCommercialRecord");
		this.TxtCommercialRecord.Name = "TxtCommercialRecord";
		this.TxtCommercialRecord.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCommercialRecord.Properties.Appearance.Font");
		this.TxtCommercialRecord.Properties.Appearance.Options.UseFont = true;
		this.TxtCommercialRecord.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCommercialRecord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCommercialRecord.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtCommercialRecord.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtCommercialRecord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtCommercialRecord_KeyPress);
		resources.ApplyResources(this.TxtPostalCode, "TxtPostalCode");
		this.TxtPostalCode.Name = "TxtPostalCode";
		this.TxtPostalCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPostalCode.Properties.Appearance.Font");
		this.TxtPostalCode.Properties.Appearance.Options.UseFont = true;
		this.TxtPostalCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPostalCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPostalCode.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtPostalCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtPostalCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPostalCode_KeyPress);
		resources.ApplyResources(this.TxtCity, "TxtCity");
		this.TxtCity.Name = "TxtCity";
		this.TxtCity.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCity.Properties.Appearance.Font");
		this.TxtCity.Properties.Appearance.Options.UseFont = true;
		this.TxtCity.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCity.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtCity.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label37, "label37");
		this.label37.Name = "label37";
		resources.ApplyResources(this.label15, "label15");
		this.label15.Name = "label15";
		resources.ApplyResources(this.label46, "label46");
		this.label46.Name = "label46";
		resources.ApplyResources(this.TxtCharge, "TxtCharge");
		this.TxtCharge.Name = "TxtCharge";
		this.TxtCharge.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCharge.Properties.Appearance.Font");
		this.TxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCharge.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtCharge.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtCharge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtSales, "TxtSales");
		this.TxtSales.Name = "TxtSales";
		this.TxtSales.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtSales.Properties.Appearance.Font");
		this.TxtSales.Properties.Appearance.Options.UseFont = true;
		this.TxtSales.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSales.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSales.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtSales.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.groupBox5, "groupBox5");
		this.groupBox5.Controls.Add(this.label6);
		this.groupBox5.Controls.Add(this.CBSaleTybe);
		this.groupBox5.Controls.Add(this.label48);
		this.groupBox5.Controls.Add(this.TxtNote);
		this.groupBox5.Controls.Add(this.label36);
		this.groupBox5.Controls.Add(this.label31);
		this.groupBox5.Controls.Add(this.Comp_Store);
		this.groupBox5.Controls.Add(this.CBBounce);
		this.groupBox5.Controls.Add(this.TxtNumNote);
		this.groupBox5.Controls.Add(this.toggleHold);
		this.groupBox5.Controls.Add(this.label17);
		this.groupBox5.Controls.Add(this.label39);
		this.groupBox5.Controls.Add(this.label26);
		this.groupBox5.Controls.Add(this.TxtCode);
		this.groupBox5.Controls.Add(this.CBProject);
		this.groupBox5.Controls.Add(this.CBCostCenter);
		this.groupBox5.Controls.Add(this.BtnOrderSearch);
		this.groupBox5.Controls.Add(this.DateHold);
		this.groupBox5.Controls.Add(this.TxtDate);
		this.groupBox5.Controls.Add(this.LblDataHold);
		this.groupBox5.Controls.Add(this.LblCustLast);
		this.groupBox5.Controls.Add(this.label21);
		this.groupBox5.Controls.Add(this.TxtType);
		this.groupBox5.Controls.Add(this.label23);
		this.groupBox5.Controls.Add(this.label20);
		this.groupBox5.Controls.Add(this.label24);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.TabStop = false;
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.CBSaleTybe, "CBSaleTybe");
		this.CBSaleTybe.FormattingEnabled = true;
		this.CBSaleTybe.Name = "CBSaleTybe";
		resources.ApplyResources(this.label48, "label48");
		this.label48.Name = "label48";
		this.label48.TextChanged += new System.EventHandler(LblSup_TextChanged);
		resources.ApplyResources(this.label36, "label36");
		this.label36.Name = "label36";
		resources.ApplyResources(this.Comp_Store, "Comp_Store");
		this.Comp_Store.FormattingEnabled = true;
		this.Comp_Store.Name = "Comp_Store";
		resources.ApplyResources(this.label39, "label39");
		this.label39.Name = "label39";
		resources.ApplyResources(this.CBProject, "CBProject");
		this.CBProject.FormattingEnabled = true;
		this.CBProject.Name = "CBProject";
		this.CBProject.SelectedIndexChanged += new System.EventHandler(CBProject_SelectedIndexChanged);
		this.CBProject.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		this.CBProject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress_1);
		resources.ApplyResources(this.DateHold, "DateHold");
		this.DateHold.Name = "DateHold";
		this.DateHold.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.DateHold.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.DateHold.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("DateHold.Properties.Appearance.Font");
		this.DateHold.Properties.Appearance.Options.UseBackColor = true;
		this.DateHold.Properties.Appearance.Options.UseFont = true;
		this.DateHold.Properties.Appearance.Options.UseTextOptions = true;
		this.DateHold.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.DateHold.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("DateHold.Properties.Buttons"))
		});
		this.DateHold.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("DateHold.Properties.CalendarTimeProperties.Buttons"))
		});
		this.DateHold.Properties.Mask.EditMask = resources.GetString("DateHold.Properties.Mask.EditMask");
		this.DateHold.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("DateHold.Properties.Mask.UseMaskAsDisplayFormat");
		this.DateHold.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.LblDataHold, "LblDataHold");
		this.LblDataHold.Name = "LblDataHold";
		resources.ApplyResources(this.LblCustLast, "LblCustLast");
		this.LblCustLast.Name = "LblCustLast";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmOrdersDraft";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmOrders_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonStores).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonDelete).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEditTafqit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.toggleSectoral.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
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
		((System.ComponentModel.ISupportInitialize)this.toggleHold.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.CkSentInvoice.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnDisCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDistrict.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtStreet.Properties).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtAdditionalNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPostalCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCity.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSales.Properties).EndInit();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DateHold.Properties).EndInit();
		base.ResumeLayout(false);
	}
}
