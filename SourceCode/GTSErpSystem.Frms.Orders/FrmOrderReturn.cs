using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Media;
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
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Dto;
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
using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.APIHelper;

namespace GTSErpSystem.Frms.Orders;

public class FrmOrderReturn : XtraForm
{
	private Class_OrdersReturn PurRTN = new Class_OrdersReturn();

	private Class_Orders Pur = new Class_Orders();

	private Class_Customer Cust = new Class_Customer();

	private Class_Contract Contract = new Class_Contract();

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

	public decimal Cash;

	public decimal Bank;

	private int CridetNode;

	private int ItmID = 0;

	private string ItmName = string.Empty;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	private string fileNameAR = Application.StartupPath + "\\Layout\\OrderReturnAR.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\OrderReturnEN.xml";

	private Class_WaitFormFunc waitForm = new Class_WaitFormFunc();

	private Class_ElctroncInvoice Zakat = new Class_ElctroncInvoice();

	private Class_Orders ORD = new Class_Orders();

	private SqlConnection conn = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);

	private IContainer components = null;

	private GridColumn gridColumn2;

	private GridColumn gridColumn1;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private GridColumn gridColumn5;

	private ToolStripMenuItem MenuExit;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem تخصيصToolStripMenuItem1;

	private TextEdit TxtLocation;

	private Label label25;

	private Label label13;

	private TextEdit TxtTafqit;

	private ToolStripMenuItem طباعةToolStripMenuItem1;

	private ToolStripMenuItem MenuPrintQued;

	private ToolStripMenuItem MenuPreviewFatoura;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem كلالكمياتبالمستودعاتToolStripMenuItem;

	private ToolStripMenuItem الكميةالحاليةToolStripMenuItem;

	private ToolStripMenuItem الكمياتToolStripMenuItem1;

	private ToolStripMenuItem كلالكمياتبالفروعToolStripMenuItem;

	private ToolStripMenuItem MenuSearchItem;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem MenuNew;

	private ToolStripMenuItem MenuSave;

	private ToolStripMenuItem MenuEdit;

	private ToolStripMenuItem MenuDelete;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuNewItem;

	private ToolStripMenuItem حركةصنفToolStripMenuItem;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem MenuPurchOrder;

	private ToolStripMenuItem استعلامToolStripMenuItem1;

	private ToolStripMenuItem رقمالفاتورةToolStripMenuItem1;

	private ToolStripMenuItem اسمالموردToolStripMenuItem1;

	private ToolStripMenuItem نوعالفاتورةToolStripMenuItem1;

	private Label label14;

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

	private NumericEditBox TxtQuntity;

	private GroupBox groupBox3;

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

	private SimpleButton BtnDelete;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtSellPrice3;

	private NumericEditBox TxtTotals;

	private SimpleButton BtnExit;

	private NumericEditBox TxtTotalAll;

	private Label label11;

	private NumericEditBox TxtTotalDiscount;

	private Label label8;

	private Label label12;

	private Label LBlSuppAccount;

	private ButtonEdit TxtPurCode;

	private Label label26;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private Label LblSup;

	private System.Windows.Forms.ComboBox TxtType;

	private DateEdit TxtDate;

	private Label label24;

	private TextEdit TxtNumNote;

	private Label label23;

	private RepositoryItemButtonEdit repositoryItemButtonEditCode;

	private TextEdit TxtNote;

	private Label label22;

	private TextEdit TxtPhone;

	private Label label21;

	private Label label20;

	private Label label19;

	private TextEdit TxtNumVat;

	private Label label18;

	private ButtonEdit TxtSuppliers;

	private Label label17;

	private ButtonEdit TxtCode;

	private GroupBox groupBox1;

	private RibbonPage ribbonPage2;

	private Label label1;

	private Label label2;

	private NumericEditBox TxtSellPrice2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private NumericEditBox TxtSellPrice1;

	private Label label4;

	private NumericEditBox TxtVatDisc;

	private Label label5;

	private Label label6;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private GroupBox groupBox2;

	private ToggleSwitch toggleHold;

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

	private Label LblSales;

	private GridColumn gridColumn26;

	private ToggleSwitch toggleItems;

	private Label label31;

	private TextEdit TxtCreditNote;

	private Label label39;

	private System.Windows.Forms.ComboBox CBProject;

	private GridColumn gridColumn27;

	private GridColumn gridColumn28;

	private GridColumn gridColumn29;

	private GridColumn gridColumn30;

	private Label label32;

	private GridColumn gridColumn31;

	private GridColumn gridColumn32;

	private GridColumn gridColumn33;

	private Label label3;

	private Label LValid;

	private Label LSucced;

	private SimpleButton BtnUpload;

	private SimpleButton BtnTypeInvoic;

	private GridColumn gridColumn34;

	private GridColumn gridColumn35;

	private GridColumn gridColumn36;

	private NumericEditBox TxtDiscount2;

	private NumericEditBox txtdicoutprege;

	private NumericEditBox TxtAllDiscount;

	private GridColumn gridColumn37;

	private Mode mode { get; set; }

	public FrmOrderReturn()
	{
		InitializeComponent();
		TxtDigital();
		SetInitialRow();
		Bind();
		BtnDefualte();
		if (LoginDetails.CashierOrderNum > 0)
		{
			try
			{
				CridetNode = PurRTN.GetReturnCount(LoginDetails.CashierOrderNum);
				if (CridetNode > 0)
				{
					GetData2(LoginDetails.CashierOrderNum);
					TxtCode.Text = "";
					TxtCreditNote.Text = CridetNode.ToString();
				}
				else
				{
					SearchOrderNum(LoginDetails.CashierOrderNum);
					TxtCode.Text = "";
				}
				LoginDetails.CashierOrderNum = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		else
		{
			Clear();
		}
		if (LoginDetails.FrmOrderID > 0)
		{
			try
			{
				CridetNode = PurRTN.GetReturnCount(LoginDetails.FrmOrderID);
				if (CridetNode > 0)
				{
					GetData2(LoginDetails.FrmOrderID);
					TxtCode.Text = "";
					TxtCreditNote.Text = CridetNode.ToString();
				}
				else
				{
					SearchOrderNum(LoginDetails.FrmOrderID);
					TxtCode.Text = "";
				}
				LoginDetails.FrmOrderID = 0;
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message);
			}
		}
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
		e.ExceptionMode = ExceptionMode.NoAction;
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[3];
	}

	private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
	{
	}

	private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
	{
		if (e.Column.FieldName == "Quantity")
		{
			string value = Convert.ToString(e.Value);
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("ItemCode");
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("SN");
			if (!(focusedRowCellDisplayText != ""))
			{
				return;
			}
			Item_Items item_Items = PurRTN.GetItems(focusedRowCellDisplayText).FirstOrDefault();
			if (item_Items == null)
			{
				return;
			}
			foreach (DataRow row in datatable.Rows)
			{
				int num = Convert.ToInt32(row["SN"]);
				if (row["SN"].ToString() == focusedRowCellDisplayText2)
				{
					decimal num2 = Convert.ToDecimal(row["Unitprice"]);
					decimal num3 = Convert.ToDecimal(value);
					if (Convert.ToDecimal(row["SellQuantity"]) <= 0m)
					{
						MessageBox.Show("عفوا\u064b يجب ان تكون كميه البيع  اكبر من الصفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						row["Quantity"] = 0;
						return;
					}
					if (Convert.ToDecimal(row["Quantity"]) > Convert.ToDecimal(row["SellQuantity"]))
					{
						row["Quantity"] = FUNC_DecimalNumQty(row["SellQuantity"].ToString());
						value = row["Quantity"].ToString();
					}
					row["SumAvergItem"] = Math.Round(Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]), DecimalNum);
					if (PurRTN.IsVatActive())
					{
						if (LoginDetails.ItemTotalWithVat)
						{
							decimal num4 = FUNC_DecimalNum(Convert.ToDecimal(item_Items.Tax_Value) / 100m + 1m);
							row["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstall"]) * num3);
							decimal num5 = Math.Round(FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(num3) * num4 + Convert.ToDecimal(row["SumPriceInstallDisc"])), 2);
							if (num5 > 200m)
							{
								num5 = Math.Round(num5, 1);
							}
							decimal num6 = FUNC_DecimalNum(num5 / num4);
							row["Total"] = num5;
							row["SumPrice"] = num6;
							row["SumPriceDisc"] = num6;
							row["IsItemTax"] = item_Items.Is_Tax;
							if (item_Items.Is_Tax == true)
							{
								row["ItemVat"] = FUNC_DecimalNum(num5 - num6);
							}
							else
							{
								row["ItemVat"] = 0;
							}
							row["Tax_Value"] = item_Items.Tax_Value;
							row["ItemVatDisc"] = row["ItemVat"];
						}
						else
						{
							row["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstallDisc"]) / Convert.ToDecimal(row["SellQuantity"]) * num3);
							decimal num7 = FUNC_DecimalNum(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"])) * Convert.ToDecimal(num3) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
							row["Total"] = num7;
							row["SumPriceDisc"] = num7;
							row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]) + num7);
							row["IsItemTax"] = item_Items.Is_Tax;
							if (item_Items.Is_Tax == true)
							{
								row["ItemVat"] = FUNC_DecimalNum(num7 * (Convert.ToDecimal(item_Items.Tax_Value) / 100m));
							}
							else
							{
								row["ItemVat"] = 0;
							}
							row["Tax_Value"] = item_Items.Tax_Value;
							row["ItemVatDisc"] = row["ItemVat"];
						}
					}
					else
					{
						row["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstallDisc"]) / Convert.ToDecimal(row["SellQuantity"]) * num3);
						decimal num8 = FUNC_DecimalNum(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"])) * Convert.ToDecimal(num3) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
						row["ItemVat"] = 0;
						row["Total"] = num8;
						row["Net"] = row["Total"];
						row["IsItemTax"] = 0;
						row["Tax_Value"] = 0;
						row["ItemVatDisc"] = row["ItemVat"];
					}
					break;
				}
				ArrangeDatatable();
			}
			CheckName();
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount2.Text), "DiscAmount");
		}
		else if (e.Column.FieldName == "SumPriceInstallDisc")
		{
			string value2 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("ItemCode");
			string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("SN");
			if (!(focusedRowCellDisplayText3 != ""))
			{
				return;
			}
			Item_Items item_Items2 = PurRTN.GetItems(focusedRowCellDisplayText3).FirstOrDefault();
			if (item_Items2 == null)
			{
				return;
			}
			foreach (DataRow row2 in datatable.Rows)
			{
				if (row2["SN"].ToString() == focusedRowCellDisplayText4)
				{
					decimal value3 = Convert.ToDecimal(row2["UnitPriceDisc"]);
					decimal value4 = Convert.ToDecimal(row2["Quantity"]);
					if (row2["UnitType"].ToString() == "الصغرى")
					{
						if (Convert.ToDecimal(row2["Quantity"]) > Convert.ToDecimal(row2["SellQuantity"]))
						{
							row2["Quantity"] = FUNC_DecimalNumQty(row2["SellQuantity"].ToString());
						}
						row2["SumAvergItem"] = Math.Round(Convert.ToDecimal(row2["AvergItem"]) * Convert.ToDecimal(row2["Quantity"]), DecimalNum);
						if (PurRTN.IsVatActive())
						{
							if (item_Items2.Is_Tax == true)
							{
								row2["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value2));
								decimal num9 = FUNC_DecimalNum(Convert.ToDecimal(value3) * Convert.ToDecimal(value4) + Convert.ToDecimal(row2["SumPriceInstallDisc"]));
								row2["ItemVat"] = FUNC_DecimalNum(num9 * (Convert.ToDecimal(item_Items2.Tax_Value) / 100m));
								row2["Total"] = num9;
								row2["SumPriceDisc"] = num9;
								row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["ItemVat"]) + num9);
								row2["IsItemTax"] = 1;
								row2["Tax_Value"] = item_Items2.Tax_Value;
								row2["ItemVatDisc"] = row2["ItemVat"];
							}
							else
							{
								row2["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value2));
								decimal num10 = FUNC_DecimalNum(Convert.ToDecimal(value3) * Convert.ToDecimal(value4) + Convert.ToDecimal(row2["SumPriceInstallDisc"]));
								row2["ItemVat"] = 0;
								row2["Total"] = num10;
								row2["Net"] = row2["Total"];
								row2["IsItemTax"] = 0;
								row2["Tax_Value"] = 0;
								row2["ItemVatDisc"] = row2["ItemVat"];
							}
						}
						else
						{
							row2["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value2));
							decimal num11 = FUNC_DecimalNum(Convert.ToDecimal(value3) * Convert.ToDecimal(value4) + Convert.ToDecimal(row2["SumPriceInstallDisc"]));
							row2["ItemVat"] = 0;
							row2["Total"] = num11;
							row2["Net"] = row2["Total"];
							row2["IsItemTax"] = 0;
							row2["Tax_Value"] = 0;
							row2["ItemVatDisc"] = row2["ItemVat"];
						}
						break;
					}
					if (row2["UnitType"].ToString() == "المتوسطة")
					{
						decimal num12 = Convert.ToDecimal(item_Items2.ConvertMediumUnit);
						row2["SumAvergItem"] = Math.Round(Convert.ToDecimal(row2["AvergItem"]) * (Convert.ToDecimal(row2["Quantity"]) * num12), DecimalNum);
						if (PurRTN.IsVatActive())
						{
							if (item_Items2.Is_Tax == true)
							{
								row2["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value2));
								decimal num13 = FUNC_DecimalNum(Convert.ToDecimal(value3) * Convert.ToDecimal(value4) + Convert.ToDecimal(row2["SumPriceInstallDisc"]));
								row2["ItemVat"] = FUNC_DecimalNum(num13 * (Convert.ToDecimal(item_Items2.Tax_Value) / 100m));
								row2["Total"] = num13;
								row2["SumPriceDisc"] = num13;
								row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["ItemVat"]) + num13);
								row2["IsItemTax"] = 1;
								row2["Tax_Value"] = item_Items2.Tax_Value;
								row2["ItemVatDisc"] = row2["ItemVat"];
							}
							else
							{
								row2["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value2));
								decimal num14 = FUNC_DecimalNum(Convert.ToDecimal(value3) * Convert.ToDecimal(value4) + Convert.ToDecimal(row2["SumPriceInstallDisc"]));
								row2["ItemVat"] = 0;
								row2["Total"] = num14;
								row2["Net"] = row2["Total"];
								row2["IsItemTax"] = 0;
								row2["Tax_Value"] = 0;
								row2["ItemVatDisc"] = row2["ItemVat"];
							}
						}
						else
						{
							row2["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value2));
							decimal num15 = FUNC_DecimalNum(Convert.ToDecimal(value3) * Convert.ToDecimal(value4) + Convert.ToDecimal(row2["SumPriceInstallDisc"]));
							row2["ItemVat"] = 0;
							row2["Total"] = num15;
							row2["Net"] = row2["Total"];
							row2["IsItemTax"] = 0;
							row2["Tax_Value"] = 0;
							row2["ItemVatDisc"] = row2["ItemVat"];
						}
						break;
					}
					if (row2["UnitType"].ToString() == "الكبرى")
					{
						decimal num16 = Convert.ToDecimal(item_Items2.ConvertBigUnit);
						row2["SumAvergItem"] = Math.Round(Convert.ToDecimal(row2["AvergItem"]) * (Convert.ToDecimal(row2["Quantity"]) * num16), DecimalNum);
						if (PurRTN.IsVatActive())
						{
							if (item_Items2.Is_Tax == true)
							{
								row2["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value2));
								decimal num17 = FUNC_DecimalNum(Convert.ToDecimal(value3) * Convert.ToDecimal(value4) + Convert.ToDecimal(row2["SumPriceInstallDisc"]));
								row2["ItemVat"] = FUNC_DecimalNum(num17 * (Convert.ToDecimal(item_Items2.Tax_Value) / 100m));
								row2["Total"] = num17;
								row2["SumPriceDisc"] = num17;
								row2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row2["ItemVat"]) + num17);
								row2["IsItemTax"] = 1;
								row2["Tax_Value"] = item_Items2.Tax_Value;
								row2["ItemVatDisc"] = row2["ItemVat"];
							}
							else
							{
								row2["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value2));
								decimal num18 = FUNC_DecimalNum(Convert.ToDecimal(value3) * Convert.ToDecimal(value4) + Convert.ToDecimal(row2["SumPriceInstallDisc"]));
								row2["ItemVat"] = 0;
								row2["Total"] = num18;
								row2["Net"] = row2["Total"];
								row2["IsItemTax"] = 0;
								row2["Tax_Value"] = 0;
								row2["ItemVatDisc"] = row2["ItemVat"];
							}
						}
						else
						{
							row2["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value2));
							decimal num19 = FUNC_DecimalNum(Convert.ToDecimal(value3) * Convert.ToDecimal(value4) + Convert.ToDecimal(row2["SumPriceInstallDisc"]));
							row2["ItemVat"] = 0;
							row2["Total"] = num19;
							row2["Net"] = row2["Total"];
							row2["IsItemTax"] = 0;
							row2["Tax_Value"] = 0;
							row2["ItemVatDisc"] = row2["ItemVat"];
						}
					}
				}
				ArrangeDatatable();
			}
			CheckName();
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount2.Text), "DiscAmount");
		}
		else if (e.Column.FieldName == "AmountDisc")
		{
			string value5 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText5 = gridView1.GetFocusedRowCellDisplayText("ItemCode");
			string focusedRowCellDisplayText6 = gridView1.GetFocusedRowCellDisplayText("SN");
			if (!(focusedRowCellDisplayText5 != ""))
			{
				return;
			}
			Item_Items item_Items3 = PurRTN.GetItems(focusedRowCellDisplayText5).FirstOrDefault();
			if (item_Items3 == null)
			{
				return;
			}
			foreach (DataRow row3 in datatable.Rows)
			{
				if (row3["SN"].ToString() == focusedRowCellDisplayText6)
				{
					decimal num20 = Convert.ToDecimal(row3["UnitPrice"]);
					decimal num21 = Convert.ToDecimal(row3["Quantity"]);
					decimal num22 = Convert.ToDecimal(row3["SumPriceInstallDisc"]);
					decimal num23 = Convert.ToDecimal(item_Items3.Tax_Value) / 100m;
					row3["AmountDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value5));
					decimal num24 = FUNC_DecimalNum(Convert.ToDecimal(num20) * Convert.ToDecimal(num21) + Convert.ToDecimal(row3["SumPriceInstallDisc"]));
					row3["SumPrice"] = (num20 + num22) * num21;
					row3["SumAvergItem"] = Math.Round(Convert.ToDecimal(row3["AvergItem"]) * Convert.ToDecimal(row3["Quantity"]), DecimalNum);
					if (Convert.ToDecimal(row3["Quantity"]) > Convert.ToDecimal(row3["SellQuantity"]))
					{
						row3["Quantity"] = FUNC_DecimalNumQty(row3["SellQuantity"].ToString());
					}
					if (PurRTN.IsVatActive())
					{
						decimal num25 = FUNC_DecimalNum(Convert.ToDecimal(value5) / Convert.ToDecimal(row3["SumPrice"]) * 100m);
						row3["PercentDisc"] = FUNC_DecimalNum(num25);
						row3["ItemVat"] = FUNC_DecimalNum(num24 * num23);
						row3["Total"] = FUNC_DecimalNum(num24 + Convert.ToDecimal(row3["ItemVat"]));
						row3["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row3["SumPrice"]) - Convert.ToDecimal(row3["AmountDisc"]));
						row3["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row3["UnitPriceDisc"]) - Convert.ToDecimal(row3["UnitPriceDisc"]) * (num25 / 100m));
						row3["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row3["SumPriceDisc"]) * (1m + num23));
						row3["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row3["Net"]) - Convert.ToDecimal(row3["SumPriceDisc"]));
						row3["IsItemTax"] = 1;
						row3["Tax_Value"] = item_Items3.Tax_Value;
					}
					else
					{
						decimal num25 = FUNC_DecimalNum(Convert.ToDecimal(value5) / Convert.ToDecimal(row3["SumPrice"]) * 100m);
						row3["PercentDisc"] = FUNC_DecimalNum(num25);
						row3["ItemVat"] = 0.0;
						row3["Total"] = num24;
						row3["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row3["SumPrice"]) - Convert.ToDecimal(row3["AmountDisc"]));
						row3["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row3["UnitPriceDisc"]) - Convert.ToDecimal(row3["UnitPriceDisc"]) * (num25 / 100m));
						row3["ItemVatDisc"] = 0.0;
						row3["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row3["SumPriceDisc"]) + Convert.ToDecimal(row3["ItemVatDisc"]));
						row3["IsItemTax"] = 1;
						row3["Tax_Value"] = item_Items3.Tax_Value;
					}
					break;
				}
				ArrangeDatatable();
			}
			CheckName();
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount2.Text), "DiscAmount");
		}
		else
		{
			if (!(e.Column.FieldName == "PercentDisc"))
			{
				return;
			}
			string value6 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText7 = gridView1.GetFocusedRowCellDisplayText("ItemCode");
			string focusedRowCellDisplayText8 = gridView1.GetFocusedRowCellDisplayText("SN");
			if (!(focusedRowCellDisplayText7 != ""))
			{
				return;
			}
			Item_Items item_Items4 = PurRTN.GetItems(focusedRowCellDisplayText7).FirstOrDefault();
			if (item_Items4 == null)
			{
				return;
			}
			foreach (DataRow row4 in datatable.Rows)
			{
				if (row4["SN"].ToString() == focusedRowCellDisplayText8)
				{
					decimal num26 = Convert.ToDecimal(row4["UnitPrice"]);
					decimal num27 = Convert.ToDecimal(row4["Quantity"]);
					decimal num28 = Convert.ToDecimal(row4["SumPriceInstallDisc"]);
					decimal num29 = Convert.ToDecimal(item_Items4.Tax_Value) / 100m;
					row4["PercentDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value6));
					decimal num30 = FUNC_DecimalNum(Convert.ToDecimal(num26) * Convert.ToDecimal(num27) + Convert.ToDecimal(row4["SumPriceInstallDisc"]));
					row4["SumPrice"] = (num26 + num28) * num27;
					row4["SumAvergItem"] = Math.Round(Convert.ToDecimal(row4["AvergItem"]) * Convert.ToDecimal(row4["Quantity"]), DecimalNum);
					if (Convert.ToDecimal(row4["Quantity"]) > Convert.ToDecimal(row4["SellQuantity"]))
					{
						row4["Quantity"] = FUNC_DecimalNumQty(row4["SellQuantity"].ToString());
					}
					if (PurRTN.IsVatActive())
					{
						row4["AmountDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value6) * (num26 * num27) / 100m);
						row4["ItemVat"] = FUNC_DecimalNum(num30 * num29);
						row4["Total"] = num30;
						row4["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row4["SumPrice"]) - Convert.ToDecimal(row4["AmountDisc"]));
						row4["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row4["UnitPriceDisc"]) - Convert.ToDecimal(row4["UnitPriceDisc"]) * (Convert.ToDecimal(value6) / 100m));
						row4["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row4["SumPriceDisc"]) * (1m + num29));
						row4["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row4["Net"]) - Convert.ToDecimal(row4["SumPriceDisc"]));
						row4["IsItemTax"] = 1;
						row4["Tax_Value"] = item_Items4.Tax_Value;
					}
					else
					{
						row4["AmountDisc"] = FUNC_DecimalNum(Convert.ToDecimal(value6) * (num26 * num27) / 100m);
						row4["ItemVat"] = 0.0;
						row4["Total"] = num30;
						row4["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row4["SumPrice"]) - Convert.ToDecimal(row4["AmountDisc"]));
						row4["UnitPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row4["UnitPriceDisc"]) - Convert.ToDecimal(row4["UnitPriceDisc"]) * (Convert.ToDecimal(value6) / 100m));
						row4["ItemVatDisc"] = 0.0;
						row4["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row4["SumPriceDisc"]) + Convert.ToDecimal(row4["ItemVatDisc"]));
						row4["IsItemTax"] = 1;
						row4["Tax_Value"] = item_Items4.Tax_Value;
					}
					break;
				}
				ArrangeDatatable();
			}
			CheckName();
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount2.Text), "DiscAmount");
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
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("ItemCode");
			string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("ItemName");
			string StoreName = gridView1.GetFocusedRowCellDisplayText("StoreName");
			string text = gridView1.GetFocusedRowCellDisplayText("SellQuantity");
			int storeID = (from x in db.Account_Stores
				where x.Store_Name == StoreName
				select x.ID).FirstOrDefault();
			GetAllitems_alternatives(focusedRowCellDisplayText3, focusedRowCellDisplayText4, storeID);
			CodeItm = focusedRowCellDisplayText3;
			if (text == "")
			{
				text = "0";
			}
			QuanITM = Convert.ToDecimal(text);
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
			FrmSearchOrderReturn frmSearchOrderReturn = new FrmSearchOrderReturn();
			frmSearchOrderReturn.ShowDialog();
			if (frmSearchOrderReturn.CustCode > 0)
			{
				int custCode = frmSearchOrderReturn.CustCode;
				Order_OrderReturn order_OrderReturn = PurRTN.GetPurchases(custCode).FirstOrDefault();
				datatable.Rows.Clear();
				List<Order_OrderReturnDetails> itemsPurchesByID = PurRTN.GetItemsPurchesByID(custCode);
				int num = 1;
				foreach (Order_OrderReturnDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = PurRTN.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemId"] = item_Items.ItemId;
					dataRow["ItemCode"] = item_Items.Item_code;
					dataRow["ItemName"] = item_Items.item_Name;
					dataRow["ItemNote"] = item_Items.Note;
					dataRow["SnOrder"] = item.SnOrderDetails;
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
					dataRow["UnitType"] = item.ItemUnitType;
					dataRow["StoreName"] = (from x in db.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["SellQuantity"] = FUNC_DecimalNum(item.Originalquantity.GetValueOrDefault());
					dataRow["Quantity"] = FUNC_DecimalNumQty(item.Quantity);
					dataRow["UnitPrice"] = FUNC_DecimalNum(item.UnitPrice);
					dataRow["SumPrice"] = FUNC_DecimalNum(item.TotalPrice);
					dataRow["AvergItem"] = FUNC_DecimalNum(item.CostAverg);
					dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
					dataRow["Total"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
					dataRow["AmountDisc"] = FUNC_DecimalNum(item.AmountDisc.GetValueOrDefault());
					dataRow["PercentDisc"] = FUNC_DecimalNum(item.PercentDisc.GetValueOrDefault());
					dataRow["UnitPriceDisc"] = FUNC_DecimalNum(item.NetUnitPrice.GetValueOrDefault());
					dataRow["SumPriceInstall"] = FUNC_DecimalNum(item.SumPriceInstall.GetValueOrDefault());
					dataRow["SumPriceInstallDisc"] = FUNC_DecimalNum(item.SumPriceInstallDisc);
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["DeleteItem"] = 0;
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
						dataRow["Net"] = Math.Round(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["IsItemTax"] = 1;
						dataRow["Tax_Value"] = item_Items.Tax_Value;
					}
					else
					{
						dataRow["IsItemTax"] = 0;
						dataRow["Tax_Value"] = 0;
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
						dataRow["Net"] = Math.Round(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT), DecimalNum);
					}
					dataRow["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(dataRow["Net"]) - Convert.ToDecimal(item.VAT_Discount));
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				PrintID = custCode;
				toggleHold.IsOn = Convert.ToBoolean(order_OrderReturn.IsWaiting);
				TxtType.SelectedValue = order_OrderReturn.Order_Paymant_Type;
				TxtCode.Text = order_OrderReturn.PurBranchID.ToString();
				TxtPurCode.Text = order_OrderReturn.FromPurchesID.ToString();
				LblSup.Text = order_OrderReturn.SupplierID.ToString();
				LBlSuppAccount.Text = Cust.GetAccountCust(Convert.ToInt32(LblSup.Text)).ToString();
				TxtSuppliers.Text = order_OrderReturn.SupplierName;
				TxtPhone.Text = order_OrderReturn.SupplierPhone;
				TxtNumVat.Text = order_OrderReturn.SupplierVatNum;
				TxtDate.DateTime = Convert.ToDateTime(order_OrderReturn.Purchases_Date);
				TxtNote.Text = order_OrderReturn.Note;
				TxtNumNote.Text = order_OrderReturn.NoteNum;
				TxtTotals.Text = Convert.ToString(order_OrderReturn.TotalPrices);
				TxtVat.Text = Convert.ToString(order_OrderReturn.Tax);
				TxtTotalAll.Text = order_OrderReturn.Safy.ToString();
				TxtDiscount2.Text = order_OrderReturn.DiscountNum.ToString();
				txtdicoutprege.Text = order_OrderReturn.DiscountPerantage.ToString();
				TxtAllDiscount.Text = order_OrderReturn.AllDis.ToString();
				TxtVatDisc.Text = order_OrderReturn.Tax_Discount.ToString();
				TxtTotalDiscount.Text = order_OrderReturn.TotalPrices_Discount.ToString();
				TxtNet.Text = order_OrderReturn.Net.ToString();
				TxtTabocc.Text = order_OrderReturn.TobaccoTax.ToString();
				TxtAllVat.Text = order_OrderReturn.AllTax.ToString();
				int cusID = Convert.ToInt32(order_OrderReturn.SupplierID);
				LblSales.Text = Cust.GetSalesManByCusID(cusID);
				if (!order_OrderReturn.CreditNote.HasValue)
				{
					TxtCreditNote.Text = "0";
				}
				else
				{
					TxtCreditNote.Text = order_OrderReturn.CreditNote.ToString();
				}
				BtnSave.Enabled = false;
				if (!LoginDetails.IsElectronicInvoice)
				{
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					BtnEdit.Text = "F6 تعديل";
				}
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = false;
				ArrangeDatatable();
				IsEnableFalse();
				TxtDiscount2.ReadOnly = true;
				TxtAllDiscount.ReadOnly = true;
				txtdicoutprege.ReadOnly = true;
				TxtDiscount2.BackColor = Color.White;
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
		GetElctornc();
	}

	private void TxtCust_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchOrders frmSearchOrders = new FrmSearchOrders();
		frmSearchOrders.ShowDialog();
		if (frmSearchOrders.CustCode > 0)
		{
			CridetNode = PurRTN.GetReturnCount(frmSearchOrders.CustCode);
			if (CridetNode > 0)
			{
				GetData2(frmSearchOrders.CustCode);
				TxtCreditNote.Text = CridetNode.ToString();
			}
			else
			{
				SearchOrderNum(frmSearchOrders.CustCode);
			}
			TxtNet.Text = "0.00";
		}
	}

	private void SearchOrderNum(int OrderSearch)
	{
		try
		{
			GetData(OrderSearch, LoginDetails.BranchID, "GetOrdersWithDetails");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void GetData2(int ID)
	{
		conn.Open();
		try
		{
			using SqlCommand sqlCommand = new SqlCommand("GetOrderDetailsWithReturns", conn);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.AddWithValue("@OrderID", Convert.ToInt32(ID));
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);
			if (dataSet.Tables.Count > 0)
			{
				DataTable dataTable = dataSet.Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					TxtPurCode.Text = ID.ToString() ?? "";
					LblSup.Text = dataRow["SupplierID"].ToString() ?? "";
					TxtSuppliers.Text = dataRow["SupplierName"].ToString() ?? "";
					TxtPhone.Text = dataRow["SupplierPhone"].ToString() ?? "";
					TxtNumVat.Text = dataRow["SupplierVatNum"].ToString() ?? "";
				}
				if (dataSet.Tables.Count >= 1)
				{
					DataTable dataTable2 = dataSet.Tables[0];
					datatable.Rows.Clear();
					foreach (DataRow row in dataTable2.Rows)
					{
						DataRow dataRow3 = datatable.NewRow();
						dataRow3["SnOrder"] = row["SN"] ?? "";
						dataRow3["ItemCode"] = row["ItemCode"] ?? "";
						dataRow3["ItemId"] = row["ItemId"];
						dataRow3["ItemName"] = row["ItemName"] ?? "";
						dataRow3["AmountDisc"] = 0;
						dataRow3["PercentDisc"] = 0;
						dataRow3["SellQuantity"] = FUNC_DecimalNum(Convert.ToDecimal(row["QuantityResult"]));
						dataRow3["AvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["CostAverg"]));
						dataRow3["UnitType"] = row["ItemUnitType"] ?? "";
						dataRow3["UnitName"] = row["UnitName"] ?? "";
						dataRow3["StoreName"] = row["StoreName"] ?? "";
						dataRow3["ItemNote"] = row["ItemNote"] ?? "";
						dataRow3["Quantity"] = FUNC_DecimalNum(Convert.ToDecimal(row["QuantityResult"]) - Convert.ToDecimal(row["QuantityResult"]));
						dataRow3["UnitPrice"] = FUNC_DecimalNum((row["NetUnitPrice"] != DBNull.Value) ? Convert.ToDecimal(row["NetUnitPrice"]) : 0m);
						dataRow3["SumPriceInstall"] = FUNC_DecimalNum((row["SumPriceInstall"] != DBNull.Value) ? Convert.ToDecimal(row["SumPriceInstall"]) : 0m);
						dataRow3["SumPriceInstallDisc"] = dataRow3["SumPriceInstall"];
						dataRow3["UnitPriceDisc"] = 0;
						dataRow3["SumPrice"] = 0;
						dataRow3["Total"] = 0;
						dataRow3["Net"] = 0;
						dataRow3["ItemVat"] = 0;
						dataRow3["IsItemTax"] = false;
						dataRow3["ItemVatDisc"] = 0;
						dataRow3["SumPriceDisc"] = 0;
						dataRow3["VatCode"] = row["VatCode"] ?? "";
						dataRow3["TaxExemptionReason"] = row["TaxExemptionReason"] ?? "";
						dataRow3["TaxExemptionReasonCode"] = row["TaxExemptionReasonCode"] ?? "";
						datatable.Rows.Add(dataRow3);
					}
				}
			}
			TxtSuppliers.Focus();
			gridView1.OptionsBehavior.Editable = true;
			ArrangeDatatable();
			IsEnableFalse();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message);
		}
		conn.Close();
	}

	public void GetData(int orderId, int BranchID, string storedProcedureName)
	{
		conn.Open();
		try
		{
			using SqlCommand sqlCommand = new SqlCommand(storedProcedureName, conn);
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
					TxtPurCode.Text = dataRow["PurBranchID"].ToString();
					LblSup.Text = dataRow["SupplierID"].ToString();
					TxtSuppliers.Text = dataRow["SupplierName"].ToString();
					TxtPhone.Text = dataRow["SupplierPhone"].ToString();
					TxtNumVat.Text = dataRow["SupplierVatNum"].ToString();
					TxtNote.Text = dataRow["Note"].ToString();
					TxtNumNote.Text = dataRow["NoteNum"].ToString();
					TxtType.Text = dataRow["OrderType"].ToString();
					CBCostCenter.Text = ORD.GetCostCenterByID(Convert.ToInt32(dataRow["CostCentersID"]));
					if (dataRow["ProjectId"] != DBNull.Value)
					{
						CBProject.SelectedValue = Convert.ToInt32(dataRow["ProjectId"]);
					}
					else
					{
						CBProject.SelectedIndex = 0;
					}
				}
				if (dataSet.Tables.Count >= 1)
				{
					DataTable dataTable2 = dataSet.Tables[0];
					datatable.Rows.Clear();
					foreach (DataRow row in dataTable2.Rows)
					{
						DataRow dataRow3 = datatable.NewRow();
						dataRow3["SnOrder"] = row["SN"] ?? "";
						dataRow3["ItemCode"] = row["item_Code"] ?? "";
						dataRow3["ItemId"] = row["ItemId"];
						dataRow3["ItemName"] = row["item_Name"] ?? "";
						dataRow3["AmountDisc"] = 0.0;
						dataRow3["PercentDisc"] = 0.0;
						dataRow3["SumPriceDisc"] = 0;
						decimal num = FUNC_DecimalNum((Convert.ToDecimal(row["Quantity"]) + Convert.ToDecimal(row["Bounce"])) * Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Width"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Weight"]);
						decimal num2 = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * (Convert.ToDecimal(row["Long"]) * Convert.ToDecimal(row["Width"])) * Convert.ToDecimal(row["Height"]) * Convert.ToDecimal(row["Weight"]));
						decimal num3 = FUNC_DecimalNum(Convert.IsDBNull(row["LastCost"]) ? 0m : Convert.ToDecimal(row["LastCost"]));
						dataRow3["SumPriceInstall"] = FUNC_DecimalNum((row["PriceInstall"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["PriceInstall"]));
						dataRow3["SumPriceInstallDisc"] = FUNC_DecimalNum((row["PriceInstallDise"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["PriceInstallDise"]));
						dataRow3["Net"] = 0;
						dataRow3["UnitType"] = row["ItemUnitType"] ?? "";
						dataRow3["UnitName"] = row["UnitName"] ?? "";
						dataRow3["StoreName"] = row["Store_Name"] ?? "";
						dataRow3["ItemNote"] = row["ItemNote"] ?? "";
						dataRow3["SellQuantity"] = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]));
						dataRow3["Quantity"] = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) - Convert.ToDecimal(row["Quantity"]));
						dataRow3["AvergItem"] = num3;
						dataRow3["SumAvergItem"] = FUNC_DecimalNum(num3 * num);
						dataRow3["VatCode"] = row["VatCode"] ?? "";
						dataRow3["TaxExemptionReason"] = row["TaxExemptionReason"] ?? "";
						dataRow3["TaxExemptionReasonCode"] = row["TaxExemptionReasonCode"] ?? "";
						dataRow3["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["NetUnitPrice"] ?? ((object)0)));
						dataRow3["UnitPriceDisc"] = dataRow3["UnitPrice"];
						decimal num4 = Convert.ToDecimal(row["VAT"]);
						decimal num5 = Convert.ToDecimal(row["Tax_Value"]);
						decimal num6 = Convert.ToDecimal(row["UnitPrice"]);
						decimal num7 = Convert.ToDecimal(row["TotalPrice"]);
						if (num4 > 0m)
						{
							dataRow3["ItemVat"] = FUNC_DecimalNum(num4);
							dataRow3["Total"] = 0;
							dataRow3["ItemVatDisc"] = 1;
							dataRow3["Tax_Value"] = FUNC_DecimalNum(Convert.ToDecimal(row["Tax_Value"]));
						}
						else
						{
							dataRow3["ItemVatDisc"] = 0;
							dataRow3["ItemVat"] = FUNC_DecimalNum(num4);
							dataRow3["Total"] = 0;
						}
						if (LoginDetails.ItemTotalWithVat)
						{
							dataRow3["SumPrice"] = FUNC_DecimalNum(num7 - num4);
							dataRow3["Total"] = 0;
							dataRow3["Tax_Value"] = FUNC_DecimalNum(Convert.ToDecimal(row["Tax_Value"]));
						}
						else
						{
							dataRow3["SumPrice"] = FUNC_DecimalNum(num7);
							dataRow3["Total"] = 0;
						}
						datatable.Rows.Add(dataRow3);
					}
					gridControl1.DataSource = datatable;
					gridControl1.RefreshDataSource();
				}
				TxtSuppliers.Focus();
				gridView1.OptionsBehavior.Editable = true;
				CheckFfistRow();
				ArrangeDatatable();
				IsEnableFalse();
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
		conn.Close();
	}

	private void BtnDefualte()
	{
		GroupPermission();
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		TxtDate.DateTime = DateTime.Now;
		gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
		base.KeyPreview = true;
		gridView1.FocusedRowHandle = 1;
		gridView1.FocusedColumn = gridView1.VisibleColumns[3];
		gridView1.ShowEditor();
		TxtDate.Focus();
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		gridView1.OptionsBehavior.Editable = false;
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 14);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 14);
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

	public void Bind()
	{
		try
		{
			TxtType.DataSource = PurRTN.GetAccountType();
			TxtType.DisplayMember = "Name";
			TxtType.ValueMember = "ID";
			ClassProject classProject = new ClassProject();
			CBProject.DataSource = classProject.GetSearchAccountProjects();
			CBProject.DisplayMember = "ProjectName";
			CBProject.ValueMember = "ProjectID";
			if (CBProject.Items.Count > 0)
			{
				Calss_CostCenter calss_CostCenter = new Calss_CostCenter();
				CBCostCenter.DataSource = calss_CostCenter.GetCostCentersByProject(Convert.ToInt32(CBProject.SelectedValue));
				CBCostCenter.DisplayMember = "CostCentersName";
				CBCostCenter.ValueMember = "CostCentersID";
				CBCostCenter.SelectedValue = calss_CostCenter.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
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
			if (dataRow["SellQuantity"].ToString() == "0" || dataRow["UnitPrice"].ToString() == "0")
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
			if (dataRow["ItemCode"].ToString() == "")
			{
				dataRow.Delete();
				datatable.AcceptChanges();
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

	public void GetAllitems_alternatives(string Itmcode, string unit, int StoreID)
	{
		if (Itmcode != "")
		{
			TxtQuntity.Text = FUNC_DecimalNumQty(Convert.ToDecimal(PurRTN.GetQantityForItems(Itmcode, unit, StoreID))).ToString();
			TxtAllQuantity.Text = FUNC_DecimalNumQty(Convert.ToDecimal(PurRTN.GetQantityForItems(Itmcode, unit))).ToString();
			TxtAvergCost.Text = Math.Round(Convert.ToDecimal(PurRTN.GetAvargeCost(Itmcode)), DecimalNum).ToString();
			TxtSellPrice1.Text = Math.Round(Convert.ToDecimal(PurRTN.GetSellprice1(Itmcode)), DecimalNum).ToString();
			TxtSellPrice2.Text = Math.Round(Convert.ToDecimal(PurRTN.GetSellprice2(Itmcode)), DecimalNum).ToString();
			TxtSellPrice3.Text = Math.Round(Convert.ToDecimal(PurRTN.GetSellprice3(Itmcode)), DecimalNum).ToString();
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
		datatable.Columns.Add("ItemId", typeof(int));
		datatable.Columns.Add("ItemCode", typeof(string));
		datatable.Columns.Add("ItemName", typeof(string));
		datatable.Columns.Add("ItemNote", typeof(string));
		datatable.Columns.Add("UnitType", typeof(string));
		datatable.Columns.Add("UnitName", typeof(string));
		datatable.Columns.Add("SellQuantity", typeof(decimal));
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
		datatable.Columns.Add("SnOrder", typeof(int));
		datatable.Columns.Add("Done", typeof(int));
		datatable.Columns.Add("IsItemTax", typeof(bool));
		datatable.Columns.Add("SumPriceInstall", typeof(decimal));
		datatable.Columns.Add("SumPriceInstallDisc", typeof(decimal));
		datatable.Columns.Add("VatCode", typeof(string));
		datatable.Columns.Add("TaxExemptionReason", typeof(string));
		datatable.Columns.Add("TaxExemptionReasonCode", typeof(string));
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
		dataRow["ItemId"] = 0;
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

	private void Cal_FinalResultTotals(decimal Per, string DicountType)
	{
		try
		{
			if (datatable.Rows.Count < 1)
			{
				TxtTotals.Text = "0.00";
				TxtDiscount2.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtAllDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtVat.Text = "0.000";
				TxtVatDisc.Text = "0.00";
				TxtTabocc.Text = "0.00";
				TxtCost.Text = "0.00";
			}
			else if (!Pur.IsVatActive())
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

	private decimal ToDecimalSafe(object value)
	{
		return (value == null || value == DBNull.Value) ? 0m : Convert.ToDecimal(value);
	}

	private int ToIntSafe(object value)
	{
		return (value != null && value != DBNull.Value) ? Convert.ToInt32(value) : 0;
	}

	private void No_Discount_NoVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]);
			if (Convert.ToInt32(row["ItemId"]) > 0)
			{
				row["SumPrice"] = Convert.ToDecimal(row["UnitPrice"]) * num;
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = row["UnitPrice"];
					row["ItemVatDisc"] = row["ItemVat"];
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
				}
				else
				{
					row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m);
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num) + FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstallDisc"]));
					row["ItemVatDisc"] = row["ItemVat"];
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
				}
				row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * num;
			}
		}
		TxtDiscount2.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
		TxtTotals.Text = Convert.ToString(num2);
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtNet.Text = num3.ToString();
	}

	private void No_Discount_Vat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]);
			if (Convert.ToInt32(row["ItemId"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num) + FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstallDisc"]));
				decimal num2 = ((row["SumPrice"] != DBNull.Value) ? Convert.ToDecimal(row["SumPrice"]) : 0m);
				decimal num3 = ((row["Tax_Value"] != DBNull.Value) ? Convert.ToDecimal(row["Tax_Value"]) : 0m);
				if (row["IsItemTax"] != DBNull.Value && Convert.ToBoolean(row["IsItemTax"]))
				{
					row["ItemVat"] = FUNC_DecimalNum(num2 * (num3 / 100m));
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
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * (Convert.ToDecimal(row["Tax_Value"]) / 100m));
					row["Net"] = Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]);
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		TxtDiscount2.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		TxtCost.Text = Convert.ToString(num4);
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num5);
		decimal num6 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		decimal num7 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num6.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = num7.ToString();
		TxtNet.Text = (num5 + num7).ToString();
	}

	private void No_Discount_PriceWithVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]);
			if (Convert.ToInt32(row["ItemId"]) > 0 && Convert.ToDecimal(row["Quantity"]) >= 1m)
			{
				if (row["IsItemTax"] != DBNull.Value && Convert.ToBoolean(row["IsItemTax"]))
				{
					decimal num2 = ((row["Total"] != DBNull.Value) ? Convert.ToDecimal(row["Total"]) : 0m);
					decimal num3 = ((row["Tax_Value"] != DBNull.Value) ? Convert.ToDecimal(row["Tax_Value"]) : 0m);
					decimal value = num2 / (num3 / 100m + 1m);
					row["SumPrice"] = FUNC_DecimalNum(value);
					row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]));
				}
				else
				{
					decimal num4 = ((row["Total"] != DBNull.Value) ? Convert.ToDecimal(row["Total"]) : 0m);
					decimal num5 = ((row["Tax_Value"] != DBNull.Value) ? Convert.ToDecimal(row["Tax_Value"]) : 0m);
					decimal value2 = num4;
					row["SumPrice"] = FUNC_DecimalNum(value2);
					row["ItemVat"] = 0;
				}
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
		TxtDiscount2.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num6 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		TxtCost.Text = Convert.ToString(num6);
		decimal num7 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num7);
		decimal num8 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num8.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = TxtVat.Text;
		TxtNet.Text = (num7 + num8).ToString();
	}

	private void DiscountPercent_NoVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["Quantity"]);
			if (Convert.ToInt32(row["ItemId"]) > 0)
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
			PerNum = Convert.ToDecimal(TxtDiscount2.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount2.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			TxtDiscount2.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount2.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num3 = Convert.ToDecimal(row2["Quantity"]);
			if (Convert.ToInt32(row2["ItemId"]) > 0)
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
			if (num > 0m && Convert.ToInt32(row["ItemId"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
				if (row["IsItemTax"] != DBNull.Value && Convert.ToBoolean(row["IsItemTax"]))
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
			PerNum = Convert.ToDecimal(TxtDiscount2.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount2.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(TxtTotals.Text) + num2) * 100m;
			TxtDiscount2.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount2.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num4 = Convert.ToDecimal(row2["Quantity"]);
			if (num4 > 0m && Convert.ToInt32(row2["ItemId"]) > 0)
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
			if (num > 0m && Convert.ToInt32(row["ItemId"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) / (Convert.ToDecimal(row["Tax_Value"]) / 100m + 1m));
				if (row["IsItemTax"] != DBNull.Value && Convert.ToBoolean(row["IsItemTax"]))
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
			PerNum = Convert.ToDecimal(TxtDiscount2.Text) / Convert.ToDecimal(TxtTotals.Text) * 100m;
			txtdicoutprege.Text = PerNum.ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "Percent")
		{
			TxtDiscount2.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			TxtAllDiscount.Text = ((Convert.ToDecimal(TxtTotals.Text) + num2) * (PerNum / 100m)).ToString();
		}
		if (DicountType == "AllDiscount")
		{
			PerNum = Convert.ToDecimal(TxtAllDiscount.Text) / (Convert.ToDecimal(TxtTotals.Text) + num2) * 100m;
			TxtDiscount2.Text = (PerNum / 100m * Convert.ToDecimal(TxtTotals.Text)).ToString();
			txtdicoutprege.Text = PerNum.ToString();
		}
		TxtTotalDiscount.Text = (Convert.ToDecimal(TxtTotals.Text) - Convert.ToDecimal(TxtDiscount2.Text)).ToString();
		foreach (DataRow row2 in datatable.Rows)
		{
			decimal num4 = Convert.ToDecimal(row2["Quantity"]);
			if (num4 > 0m && Convert.ToInt32(row2["ItemId"]) > 0)
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
		TxtNet.Text = Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text) + Convert.ToDecimal(TxtVatDisc.Text), 1).ToString();
	}

	private void Cal_FinalResultTotals2()
	{
		try
		{
			if (datatable.Rows.Count < 1)
			{
				TxtTotals.Text = "0.00";
				TxtDiscount2.Text = "0.00";
				txtdicoutprege.Text = "0.00";
				TxtAllDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtTotalDiscount.Text = "0.00";
				TxtVat.Text = "0.000";
				TxtVatDisc.Text = "0.00";
				TxtTabocc.Text = "0.00";
				TxtCost.Text = "0.00";
			}
			else if (!Pur.IsVatActive())
			{
				No_Discount_NoVat2();
			}
			else if (LoginDetails.ItemTotalWithVat)
			{
				No_Discount_PriceWithVat2();
			}
			else
			{
				No_Discount_Vat2();
			}
			gridControl1.DataSource = datatable;
		}
		catch (Exception)
		{
		}
	}

	private void No_Discount_NoVat2()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["SellQuantity"]);
			if (Convert.ToInt32(row["ItemId"]) > 0)
			{
				row["SumPrice"] = Convert.ToDecimal(row["UnitPrice"]) * num;
				row["ItemVat"] = 0;
				row["Total"] = row["SumPrice"];
				if (((Convert.ToDecimal(row["AmountDisc"]) == 0m || row["AmountDisc"].ToString() == string.Empty) && Convert.ToDecimal(row["PercentDisc"]) == 0m) || row["PercentDisc"].ToString() == string.Empty)
				{
					row["UnitPriceDisc"] = row["UnitPrice"];
					row["ItemVatDisc"] = row["ItemVat"];
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPrice"]) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
				}
				else
				{
					row["UnitPriceDisc"] = Convert.ToDecimal(row["UnitPrice"]) - Convert.ToDecimal(row["UnitPrice"]) * (Convert.ToDecimal(row["PercentDisc"]) / 100m);
					row["SumPriceDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPriceDisc"]) * num) + FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstallDisc"]));
					row["ItemVatDisc"] = row["ItemVat"];
					row["Net"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
				}
				row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * num;
			}
		}
		TxtDiscount2.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtVatDisc.Text = "0.00";
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
		TxtTotals.Text = Convert.ToString(num2);
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtNet.Text = num3.ToString();
	}

	private void No_Discount_Vat2()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["SellQuantity"]);
			if (Convert.ToInt32(row["ItemId"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num) + FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstallDisc"]));
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
					row["Net"] = Convert.ToDecimal(row["SumPriceDisc"]) + Convert.ToDecimal(row["ItemVatDisc"]);
				}
				row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
			}
		}
		TxtDiscount2.Text = "0.00";
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

	private void No_Discount_PriceWithVat2()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = Convert.ToDecimal(row["SellQuantity"]);
			if (Convert.ToInt32(row["ItemId"]) > 0)
			{
				decimal num2 = ((row["Total"] != DBNull.Value) ? Convert.ToDecimal(row["Total"]) : 0m);
				decimal num3 = ((row["Tax_Value"] != DBNull.Value) ? Convert.ToDecimal(row["Tax_Value"]) : 0m);
				decimal value = num2 / (num3 / 100m + 1m);
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
		TxtDiscount2.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(SumAvergItem)", string.Empty));
		TxtCost.Text = Convert.ToString(num4);
		decimal num5 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num5);
		decimal num6 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num6.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = TxtVat.Text;
		TxtNet.Text = (num5 + num6).ToString();
	}

	private void SetKeyboardLayout(InputLanguage layout)
	{
		InputLanguage.CurrentInputLanguage = layout;
	}

	private void Clear()
	{
		LblSales.Text = "";
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtCode.Text = "";
		datatable.Rows.Clear();
		dtCurrentTable.Rows.Clear();
		gridControl1.DataSource = datatable;
		TxtSuppliers.Text = "";
		TxtNumVat.Text = "";
		TxtType.Text = "";
		TxtNote.Text = "";
		TxtDiscount2.Text = "";
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtType.Text = "نقدى";
		TxtSuppliers.Text = "";
		TxtPhone.Text = "";
		TxtNumNote.Text = "";
		TxtVatDisc.Text = "0.00";
		TxtTafqit.Text = "";
		TxtTotalAll.Text = "0.00";
		TxtTotalDiscount.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtLocation.Text = "";
		TxtSellPrice1.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		TxtSellPrice2.Text = "0.00";
		TxtSellPrice2.Text = "0.00";
		TxtTotals.Text = "0.00";
		TxtProfit.Text = "0.00";
		TxtQuntity.Text = FUNC_DecimalNumQty(0m).ToString();
		TxtAllQuantity.Text = FUNC_DecimalNumQty(0m).ToString();
		TxtAvergCost.Text = "0.00";
		TxtLastCost.Text = "0.00";
		TxtPurCode.Text = "";
		TxtCreditNote.Text = "0";
		TxtDate.DateTime = DateTime.Now;
		CBCostCenter.SelectedValue = 0;
		TxtNote.Enabled = true;
		TxtType.Enabled = true;
		TxtDate.Enabled = true;
		LoginDetails.FrmOrderID = 0;
		LoginDetails.CashierOrderNum = 0;
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
		LValid.Visible = false;
		LSucced.Visible = false;
		BtnUpload.Visible = false;
		toggleItems.IsOn = false;
	}

	private async Task Save()
	{
		if (TxtCode.Text != "")
		{
			SoundPlayer player = new SoundPlayer("SOUND38.WAV");
			player.Play();
			MessageBox.Show("عفوا،لايمكن حفظ الفاتوره مرتين \r\n الرجاء المراجعة والمحاولة مرة اخري.", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		else if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 14))
		{
			CheckFfistRow();
			if (datatable.Rows.Count > 0)
			{
				decimal Quantity = Convert.ToDecimal(datatable.Compute("SUM(Quantity)", string.Empty));
				if (Quantity <= 0m)
				{
					MessageBox.Show("عفوا\u064b يجب ادخال كميه اكبر من الصفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					return;
				}
				List<DataRow> rowsToDelete = (from r in datatable.AsEnumerable()
					where r["Quantity"] != DBNull.Value && Convert.ToDecimal(r["Quantity"]) < 0m
					select r).ToList();
				foreach (DataRow row in rowsToDelete)
				{
					datatable.Rows.Remove(row);
				}
				gridControl1.DataSource = datatable;
				if (TxtSuppliers.Text != "")
				{
					if (TxtType.Text != "")
					{
						if (TxtType.Text == "آجلة" && LblSup.Text == "1")
						{
							MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع حساب مورد نقدى", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							TxtSuppliers.Focus();
							return;
						}
						FiterDatatableItemsUpdate();
						if (Convert.ToInt32(TxtType.SelectedValue) == 1)
						{
							LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
							FrmMoney FrmShow = new FrmMoney();
							FrmShow.ShowDialog();
							if (!FrmShow.IsComplateMony)
							{
								return;
							}
							Cash = Convert.ToDecimal(FrmShow.TxtCash.Text);
							Bank = Convert.ToDecimal(FrmShow.TxtVisa.Text);
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
							Cash = 0m;
							Bank = 0m;
						}
						int Result = PurRTN.InsertPurch(out var CodePur, Convert.ToInt32(TxtPurCode.Text), Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), TxtNote.Text, TxtNumNote.Text, Convert.ToBoolean(toggleHold.IsOn), Math.Round(Convert.ToDecimal(TxtCost.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount2.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), ISPurReturnNum: true, Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Cash, Bank, Convert.ToInt32(TxtCreditNote.Text) + 1, Math.Round(Convert.ToDecimal(TxtAllDiscount.Text), DecimalNum));
						PurRTN.InsertItems(datatable, Result, Convert.ToInt32(TxtCreditNote.Text) + 1, Convert.ToInt32(TxtPurCode.Text), Convert.ToBoolean(toggleHold.IsOn));
						gridControl1.DataSource = datatable;
						gridView1.OptionsBehavior.Editable = false;
						PurRTN.InsertTranNum(out var CodeTranDS, CodePur, Convert.ToInt32(TxtType.SelectedValue), TxtSuppliers.Text, TxtNumNote.Text, TxtDate.DateTime);
						if (Convert.ToInt32(TxtType.SelectedValue) == 2)
						{
							LBlSuppAccount.Text = Cust.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
							PurRTN.InsertTranDetailsAjilaNUM(CodeTranDS, Convert.ToInt32(TxtPurCode.Text), CodePur, Convert.ToInt32(LBlSuppAccount.Text), TxtSuppliers.Text, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtNet.Text), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue));
						}
						else
						{
							PurRTN.InsertTranDetailsCashFrmNum(CodeTranDS, Convert.ToInt32(TxtPurCode.Text), CodePur, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtVatDisc.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), Cash, Bank);
						}
						if (Result > 0)
						{
							OrderOrdersDto OrderDto = await GenerateOrder(Convert.ToInt32(TxtPurCode.Text));
							bool isSuccess = false;
							if (LoginDetails.IsElectronicInvoice)
							{
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
								InvoiceDto inv = invoiceDto(OrderDto);
								Result res = await invoiceZakat.CreateXml(inv, LoginDetails.InvoiceHash, supplierPartyDto(OrderDto), CustomerPartyDto(OrderDto), invoiceItems(datatable), CSIDInfoDto());
								if (!res.IsValid)
								{
									PurRTN.Delete(Result, datatable);
									PurRTN.DeleteTranDetailsNum(Result, 9);
									PurRTN.DeleteTranNum(Result, 9);
									MessageBox.Show(res.ErrorMessage, "الفاتورة غير صالحة");
									LValid.Visible = true;
									LValid.BackColor = Color.Red;
									LValid.Text = "NotValid";
									LSucced.Visible = true;
									LSucced.BackColor = Color.Red;
									LSucced.Text = "NotSuccess";
									return;
								}
								if (LoginDetails.SentXmlZakateNow && res.IsValid)
								{
									isSuccess = (await invoiceZakat.SentInvoiceToZakat(res, CSIDInfoDto(), inv.InvoiceTypeName, mode)).IsSuccess;
									LValid.Visible = true;
									LValid.Text = "Valid";
									LValid.BackColor = Color.Green;
									LSucced.Visible = true;
									LSucced.Text = "Success";
									LSucced.BackColor = Color.Green;
								}
								else
								{
									LValid.Visible = true;
									LValid.Text = "Valid";
									LValid.BackColor = Color.Green;
									LSucced.Visible = true;
									LSucced.BackColor = Color.Red;
									LSucced.Text = "NotSuccess";
								}
								LoginDetails.EncHash = res.QRCode;
								string OrderTyprId = ((LoginDetails.OrderTypeElectronicInvoiceId == 0) ? "381" : LoginDetails.OrderTypeElectronicInvoiceId.ToString());
								string NormalOrSimpleInvoice = (string.IsNullOrEmpty(LoginDetails.NormalOrSimpleInvoice) ? "02" : LoginDetails.NormalOrSimpleInvoice);
								PurRTN.UpdateOrderReturnAFterElctorncFatora(Result, res.QRCode, OrderTyprId, NormalOrSimpleInvoice, LoginDetails.ThirdParty, LoginDetails.NominalInvoice, LoginDetails.ExportInvoice, LoginDetails.SummaryInvoice, LoginDetails.SelfInvoice);
								ResultElectronicInvoiceXmlService resultElectronicInvoiceXmlService = new ResultElectronicInvoiceXmlService();
								await resultElectronicInvoiceXmlService.AddResult(LoginDetails.UserID, LoginDetails.CsIdForUser, res, LoginDetails.BranchID, isSuccess, inv.OrderId, OrderDto.Purchases_Date.Value, inv.InvoiceTypeName, "اشعار دائن");
							}
							BtnSave.Enabled = false;
							if (!LoginDetails.IsElectronicInvoice)
							{
								BtnEdit.Enabled = true;
								BtnDelete.Enabled = true;
							}
							PrintID = Result;
							IsEnableFalse();
							TxtCode.Text = CodePur.ToString();
							TxtCreditNote.Text = (Convert.ToInt32(TxtCreditNote.Text) + 1).ToString();
							DialogResult dialogResult = MessageBox.Show("تم حفظ بيانات فاتورة مرتجعات المبيعات بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
							if (dialogResult == DialogResult.Yes)
							{
								Print();
							}
						}
						else
						{
							MessageBox.Show("تم حفظ بيانات فاتورة مرتجعات المبيعات رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 14))
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
			if (TxtType.Text == "آجلة" && LblSup.Text == "1")
			{
				MessageBox.Show("عفوا\u064b لا يمكن حفظ فاتورة أجلة مع مورد نقدى", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtSuppliers.Focus();
				return;
			}
			CheckFfistRow();
			if (datatable.Rows.Count > 0)
			{
				ArrangeDatatable();
				FiterDatatableItemsUpdate();
				if (Convert.ToInt32(TxtType.SelectedValue) == 1)
				{
					LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
					FrmMoney frmMoney = new FrmMoney();
					frmMoney.ShowDialog();
					if (!frmMoney.IsComplateMony)
					{
						return;
					}
					Cash = Convert.ToDecimal(frmMoney.TxtCash.Text);
					Bank = Convert.ToDecimal(frmMoney.TxtVisa.Text);
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
					Cash = 0m;
					Bank = 0m;
				}
				num2 = PurRTN.DeleteTranDetailsNum(Convert.ToInt32(TxtCode.Text), 9);
				num = PurRTN.UpdatePurch(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtNumVat.Text, TxtDate.DateTime, Convert.ToInt32(TxtType.SelectedValue), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), TxtNote.Text, TxtNumNote.Text, toggleHold.IsOn, Convert.ToDecimal(TxtCost.Text), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalAll.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtDiscount2.Text), DecimalNum), Math.Round(Convert.ToDecimal(txtdicoutprege.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtVatDisc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtTabocc.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtAllVat.Text), DecimalNum), Cash, Bank);
				decimal num3 = Math.Round((Convert.ToDecimal(txtdicoutprege.Text) * Convert.ToDecimal(TxtVat.Text) / 100m - Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum)) * -1m, DecimalNum);
				decimal num4 = Math.Round((Convert.ToDecimal(txtdicoutprege.Text) * Convert.ToDecimal(TxtTotals.Text) / 100m - Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum)) * -1m, DecimalNum);
				PurRTN.deleteItemsByPurcahesId(num);
				PurRTN.InsertItems(datatable, num, Convert.ToInt32(TxtCreditNote.Text), Convert.ToInt32(TxtPurCode.Text), Convert.ToBoolean(toggleHold.IsOn));
				PurRTN.UpdateTranNUM(out var TRNIDDS, Convert.ToInt32(TxtCode.Text), Convert.ToInt32(TxtType.SelectedValue), TxtSuppliers.Text, TxtNumNote.Text, TxtDate.DateTime);
				if (Convert.ToInt32(TxtType.SelectedValue) == 2)
				{
					LBlSuppAccount.Text = Cust.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
					PurRTN.InsertTranDetailsAjilaNUM(TRNIDDS, Convert.ToInt32(TxtPurCode.Text), Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LBlSuppAccount.Text), TxtSuppliers.Text, Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtAllVat.Text), Convert.ToDecimal(TxtNet.Text), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue));
				}
				else
				{
					PurRTN.InsertTranDetailsCashFrmNum(TRNIDDS, Convert.ToInt32(TxtPurCode.Text), Convert.ToInt32(TxtCode.Text), Convert.ToDecimal(TxtCost.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToDecimal(TxtTabocc.Text), Convert.ToDecimal(TxtAllVat.Text), Convert.ToDecimal(TxtTotalDiscount.Text), Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), Cash, Bank);
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 14))
		{
			int num = 0;
			int num2 = 0;
			if (TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة مرتجعات المبيعات رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = PurRTN.Delete(Convert.ToInt32(TxtCode.Text), datatable);
					PurRTN.DeleteTranDetailsNum(Convert.ToInt32(TxtCode.Text), 9);
					num2 = PurRTN.DeleteTranNum(Convert.ToInt32(TxtCode.Text), 9);
					if (num > 0 && num2 > 0)
					{
						MessageBox.Show("تم حذف بيانات فاتورة مرتجعات المبيعات رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 14))
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
				sqlCommand.Parameters.AddWithValue("@CreditNote", Convert.ToInt32(TxtCreditNote.Text));
				sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
				List<DataSetALL.Order_PurchasesTablePrintRow> list = dataSetALL.Order_PurchasesTablePrint.AsEnumerable().Where(delegate(DataSetALL.Order_PurchasesTablePrintRow r)
				{
					object obj = r["Quantity"];
					if (obj == DBNull.Value)
					{
						return false;
					}
					decimal result;
					return decimal.TryParse(obj.ToString(), out result) && result == 0m;
				}).ToList();
				foreach (DataSetALL.Order_PurchasesTablePrintRow item in list)
				{
					dataSetALL.Order_PurchasesTablePrint.Rows.Remove(item);
				}
				sqlConnection.Close();
				if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					if (LoginDetails.Lang == "ar-EG")
					{
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintOrdersReturn.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintOrdersReturn.mrt");
					}
					stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
					stiReport["Phone"] = LoginDetails.Phone;
					stiReport["Bank"] = LoginDetails.CompanyBank;
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["Type"] = TxtType.Text;
					stiReport["Date"] = TxtDate.DateTime;
					stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
					stiReport["CustName"] = TxtSuppliers.Text;
					stiReport["CustVatNum"] = TxtNumVat.Text;
					stiReport["Custphone"] = TxtPhone.Text;
					stiReport["Tafqit"] = txtWordAr;
					stiReport["CashMoney"] = Cash.ToString();
					stiReport["CashBank"] = Bank.ToString();
					stiReport["MobileComp"] = LoginDetails.Mobile;
					stiReport["AddressComp"] = LoginDetails.Address;
					stiReport["PrintComp"] = LoginDetails.PrintComp;
					Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
					string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), LoginDetails.NetCash.ToString(), LoginDetails.VatCash.ToString());
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
		TxtNote.ReadOnly = false;
		TxtDate.ReadOnly = false;
		TxtNumNote.ReadOnly = false;
		BtnEdit.Text = "F6 تعديل";
		txtdicoutprege.ReadOnly = false;
		TxtAllDiscount.ReadOnly = false;
		TxtDiscount2.ReadOnly = false;
	}

	public void IsEnableFalse()
	{
		TxtNote.ReadOnly = true;
		TxtDate.ReadOnly = true;
		TxtDiscount2.BackColor = Color.White;
		TxtNote.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
		CBCostCenter.Enabled = true;
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
						dataRow["ItemCode"] = item_Items.Item_code;
						dataRow["ItemName"] = item_Items.item_Name;
						dataRow["UnitName"] = item_Items.Item_Unit.Name;
						dataRow["Quantity"] = FUNC_DecimalNumQty(1);
						dataRow["StoreName"] = PurRTN.GetStoreName();
						dataRow["UnitType"] = "الصغرى";
						dataRow["AvergItem"] = item_Items.Average_cost;
						dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
						dataRow["UnitPrice"] = item_Items.SellPriceSmall;
						dataRow["Total"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1)), DecimalNum);
						if (PurRTN.IsVatActive())
						{
							if (item_Items.Is_Tax == true)
							{
								dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1) * item_Items.Tax_Value / (decimal?)100), DecimalNum);
								dataRow["Net"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1) * item_Items.Tax_Value / (decimal?)100 + (decimal?)Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
								dataRow["IsItemTax"] = 1;
								dataRow["Tax_Value"] = item_Items.Tax_Value;
							}
							else
							{
								dataRow["IsItemTax"] = 0;
								dataRow["Tax_Value"] = 0;
								dataRow["ItemVat"] = 0.0;
								dataRow["Net"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
							}
						}
						else
						{
							dataRow["IsItemTax"] = 0;
							dataRow["Tax_Value"] = 0;
							dataRow["ItemVat"] = 0.0;
							dataRow["Net"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1))), DecimalNum);
						}
						object obj = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
						int num = Convert.ToInt32(obj.ToString());
						dataRow["SN"] = num + 1;
						datatable.Rows.Add(dataRow);
					}
					IsShow = 1;
				}
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
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount2.Text), "DiscAmount");
		}
		catch (Exception)
		{
			Clear();
			Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount2.Text), "DiscAmount");
			LoginDetails.ISCheckSearchItem = true;
			LoginDetails.ISPurches = false;
			TxtSuppliers.Focus();
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
		TxtTotalAll.DecimalDigits = DecimalNum;
		TxtTotalDiscount.DecimalDigits = DecimalNum;
		TxtNet.DecimalDigits = DecimalNum;
		TxtCost.DecimalDigits = DecimalNum;
		TxtTabocc.DecimalDigits = DecimalNum;
		TxtAllVat.DecimalDigits = DecimalNum;
		TxtAllTotal.DecimalDigits = DecimalNum;
		TxtDiscount2.DecimalDigits = DecimalNum;
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

	private void GridSelectAll()
	{
		foreach (DataRow row in datatable.Rows)
		{
			if (!(Convert.ToDecimal(row["SellQuantity"]) > 0m))
			{
				continue;
			}
			row["Quantity"] = row["SellQuantity"];
			decimal num = Convert.ToDecimal(row["Quantity"]);
			string code = row["ItemCode"].ToString();
			Item_Items item_Items = PurRTN.GetItems(code).FirstOrDefault();
			row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]);
			if (PurRTN.IsVatActive())
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num2 = FUNC_DecimalNum(Convert.ToDecimal(item_Items.Tax_Value) / 100m + 1m);
					row["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstall"]) * num);
					decimal num3 = Math.Round(FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(num) * num2 + Convert.ToDecimal(row["SumPriceInstallDisc"])), 2);
					if (num3 > 200m)
					{
						num3 = Math.Round(num3, 1);
					}
					decimal num4 = FUNC_DecimalNum(num3 / num2);
					if (item_Items.Is_Tax == true)
					{
						row["ItemVat"] = FUNC_DecimalNum(num3 - num4);
					}
					else
					{
						row["ItemVat"] = 0;
					}
					row["Total"] = num3;
					row["SumPrice"] = num4;
					row["SumPriceDisc"] = num4;
					row["IsItemTax"] = item_Items.Is_Tax;
					row["Tax_Value"] = item_Items.Tax_Value;
					row["ItemVatDisc"] = row["ItemVat"];
				}
				else
				{
					row["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstallDisc"]) / Convert.ToDecimal(row["SellQuantity"]) * num);
					decimal num5 = FUNC_DecimalNum(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"])) * Convert.ToDecimal(num) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
					if (item_Items.Is_Tax == true)
					{
						row["ItemVat"] = FUNC_DecimalNum(num5 * (Convert.ToDecimal(item_Items.Tax_Value) / 100m));
					}
					else
					{
						row["ItemVat"] = 0;
					}
					row["Total"] = num5;
					row["SumPriceDisc"] = num5;
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]) + num5);
					row["IsItemTax"] = item_Items.Is_Tax;
					row["Tax_Value"] = item_Items.Tax_Value;
					row["ItemVatDisc"] = row["ItemVat"];
				}
			}
			else
			{
				row["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstallDisc"]) / Convert.ToDecimal(row["SellQuantity"]) * num);
				decimal num6 = FUNC_DecimalNum(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"])) * Convert.ToDecimal(num) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
				row["ItemVat"] = 0;
				row["Total"] = num6;
				row["Net"] = row["Total"];
				row["IsItemTax"] = 0;
				row["Tax_Value"] = 0;
				row["ItemVatDisc"] = row["ItemVat"];
			}
		}
		ArrangeDatatable();
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount2.Text), "DiscAmount");
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[6];
		gridView1.ShowEditor();
	}

	private void GridDisSelectAll()
	{
		foreach (DataRow row in datatable.Rows)
		{
			row["Quantity"] = "0.00";
			row["SumPriceDisc"] = "0.00";
			row["ItemVatDisc"] = "0.00";
			string code = row["ItemCode"].ToString();
			Item_Items itemsCode = Pur.GetItemsCode2(code);
			row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]);
			if (Pur.IsVatActive())
			{
				if (itemsCode.Is_Tax == true)
				{
					decimal value = Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(row["Quantity"]);
					decimal? tax_Value = itemsCode.Tax_Value;
					row["ItemVat"] = Math.Round(Convert.ToDecimal((decimal?)value * tax_Value / (decimal?)100), DecimalNum);
					string text = row["ItemVat"].ToString();
					row["Total"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					decimal value2 = Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(row["Quantity"]);
					decimal? tax_Value2 = itemsCode.Tax_Value;
					row["Net"] = Math.Round(Convert.ToDecimal((decimal?)value2 * tax_Value2 / (decimal?)100 + (decimal?)Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(row["Quantity"]))), DecimalNum);
					row["IsItemTax"] = 1;
					row["Tax_Value"] = itemsCode.Tax_Value;
				}
				else
				{
					row["ItemVat"] = 0.0;
					row["Total"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["Net"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(row["Quantity"])), DecimalNum);
					row["IsItemTax"] = 0;
					row["Tax_Value"] = 0;
				}
			}
			else
			{
				row["ItemVat"] = 0.0;
				row["Total"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(row["Quantity"])), DecimalNum);
				row["Net"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(row["Quantity"])), DecimalNum);
				row["IsItemTax"] = 0;
				row["Tax_Value"] = 0;
			}
		}
		ArrangeDatatable();
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount2.Text), "DiscAmount");
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[6];
		gridView1.ShowEditor();
	}

	private void SearchFrm(int PurchacheId)
	{
		try
		{
			if (PurchacheId > 0)
			{
				Order_OrderReturn order_OrderReturn = PurRTN.GetPurchases(PurchacheId).FirstOrDefault();
				datatable.Rows.Clear();
				List<Order_OrderReturnDetails> itemsPurchesByID = PurRTN.GetItemsPurchesByID(PurchacheId);
				int num = 1;
				foreach (Order_OrderReturnDetails item in itemsPurchesByID)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = PurRTN.GetItemsByitemid(itemID).FirstOrDefault();
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
					dataRow["UnitType"] = item.ItemUnitType;
					dataRow["StoreName"] = (from x in db.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["SellQuantity"] = Math.Round(Convert.ToDecimal(item.Quantity), DecimalNum);
					dataRow["Quantity"] = FUNC_DecimalNumQty(item.Quantity);
					dataRow["UnitPrice"] = item.UnitPrice;
					dataRow["AvergItem"] = item.CostAverg;
					dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
					dataRow["Total"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["DeleteItem"] = 0;
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["Net"] = Math.Round(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["IsItemTax"] = 1;
						dataRow["Tax_Value"] = item_Items.Tax_Value;
					}
					else
					{
						dataRow["IsItemTax"] = 0;
						dataRow["Tax_Value"] = 0;
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["Net"] = Math.Round(Convert.ToDecimal(item.TotalPrice) + Convert.ToDecimal(item.VAT), DecimalNum);
					}
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				PrintID = PurchacheId;
				toggleHold.IsOn = Convert.ToBoolean(order_OrderReturn.IsWaiting);
				TxtType.SelectedValue = order_OrderReturn.Order_Paymant_Type;
				TxtCode.Text = order_OrderReturn.PurBranchID.ToString();
				TxtPurCode.Text = order_OrderReturn.FromPurchesID.ToString();
				LblSup.Text = order_OrderReturn.SupplierID.ToString();
				LBlSuppAccount.Text = Cust.GetAccountCust(Convert.ToInt32(LblSup.Text)).ToString();
				TxtSuppliers.Text = order_OrderReturn.SupplierName;
				TxtPhone.Text = order_OrderReturn.SupplierPhone;
				TxtNumVat.Text = order_OrderReturn.SupplierVatNum;
				TxtDate.DateTime = Convert.ToDateTime(order_OrderReturn.Purchases_Date);
				TxtNote.Text = order_OrderReturn.Note;
				TxtNumNote.Text = order_OrderReturn.NoteNum;
				TxtTotals.Text = Convert.ToString(order_OrderReturn.TotalPrices);
				TxtVat.Text = Convert.ToString(order_OrderReturn.Tax);
				TxtTotalAll.Text = order_OrderReturn.Safy.ToString();
				TxtDiscount2.Text = order_OrderReturn.DiscountNum.ToString();
				TxtVatDisc.Text = order_OrderReturn.Tax_Discount.ToString();
				TxtTotalDiscount.Text = order_OrderReturn.Net.ToString();
				TxtNet.Text = order_OrderReturn.Net.ToString();
				TxtTabocc.Text = order_OrderReturn.TobaccoTax.ToString();
				TxtAllVat.Text = order_OrderReturn.AllTax.ToString();
				int cusID = Convert.ToInt32(order_OrderReturn.SupplierID);
				LblSales.Text = Cust.GetSalesManByCusID(cusID);
				if (!order_OrderReturn.CreditNote.HasValue)
				{
					TxtCreditNote.Text = "0";
				}
				else
				{
					TxtCreditNote.Text = order_OrderReturn.CreditNote.ToString();
				}
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
				TxtDate.Focus();
			}
		}
		catch (Exception)
		{
			Clear();
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
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount2.Text), "DiscAmount");
	}

	private void GridPlus()
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (!(row["SN"].ToString() == Convert.ToString(num)) || Convert.ToInt32(row["Quantity"]) >= Convert.ToInt32(row["SellQuantity"]))
			{
				continue;
			}
			row["Quantity"] = Convert.ToDecimal(row["Quantity"]) + 1m;
			decimal num2 = Convert.ToDecimal(row["Quantity"]);
			string code = row["ItemCode"].ToString();
			Item_Items item_Items = PurRTN.GetItems(code).FirstOrDefault();
			row["SumAvergItem"] = Convert.ToDecimal(row["AvergItem"]) * Convert.ToDecimal(row["Quantity"]);
			if (PurRTN.IsVatActive())
			{
				if (LoginDetails.ItemTotalWithVat)
				{
					decimal num3 = FUNC_DecimalNum(Convert.ToDecimal(item_Items.Tax_Value) / 100m + 1m);
					row["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstall"]) * num2);
					decimal num4 = Math.Round(FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(num2) * num3 + Convert.ToDecimal(row["SumPriceInstallDisc"])), 1);
					decimal num5 = FUNC_DecimalNum(num4 / num3);
					row["Total"] = num4;
					row["SumPrice"] = num5;
					row["SumPriceDisc"] = num5;
					row["IsItemTax"] = item_Items.Is_Tax;
					row["Tax_Value"] = item_Items.Tax_Value;
					if (item_Items.Is_Tax == true)
					{
						row["ItemVat"] = FUNC_DecimalNum(num4 - num5);
					}
					else
					{
						row["ItemVat"] = 0;
					}
					row["ItemVatDisc"] = row["ItemVat"];
				}
				else
				{
					row["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstallDisc"]) / Convert.ToDecimal(row["SellQuantity"]) * num2);
					decimal num6 = FUNC_DecimalNum(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"])) * Convert.ToDecimal(num2) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
					if (item_Items.Is_Tax == true)
					{
						row["ItemVat"] = FUNC_DecimalNum(num6 * (Convert.ToDecimal(item_Items.Tax_Value) / 100m));
					}
					else
					{
						row["ItemVat"] = 0;
					}
					row["Total"] = num6;
					row["SumPriceDisc"] = num6;
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]) + num6);
					row["IsItemTax"] = item_Items.Is_Tax;
					row["Tax_Value"] = item_Items.Tax_Value;
					row["ItemVatDisc"] = row["ItemVat"];
				}
			}
			else
			{
				row["SumPriceInstallDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceInstallDisc"]) / Convert.ToDecimal(row["SellQuantity"]) * num2);
				decimal num7 = FUNC_DecimalNum(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"])) * Convert.ToDecimal(num2) + Convert.ToDecimal(row["SumPriceInstallDisc"]));
				row["ItemVat"] = 0;
				row["Total"] = num7;
				row["Net"] = row["Total"];
				row["IsItemTax"] = 0;
				row["Tax_Value"] = 0;
				row["ItemVatDisc"] = row["ItemVat"];
			}
			break;
		}
		gridView1.FocusedColumn = gridView1.VisibleColumns[3];
		gridView1.ShowEditor();
		ArrangeDatatable();
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount2.Text), "DiscAmount");
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[6];
		gridView1.ShowEditor();
	}

	private void BtnPlus_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		GridPlus();
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
		if (LoginDetails.FrmOrderID != 0)
		{
			SearchFrm(LoginDetails.FrmOrderID);
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
				ToWord toWord = new ToWord(Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty)), currencies[tafqitID]);
				txtWordAr = toWord.ConvertToArabic();
				TxtTafqit.Text = txtWordAr;
			}
			else
			{
				ToWord toWord2 = new ToWord(Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty)), currencies[tafqitID]);
				txtWordAr = toWord2.ConvertToEnglish();
				TxtTafqit.Text = txtWordAr;
			}
		}
		catch (Exception)
		{
		}
	}

	private void FrmOrderReturn_FormClosing(object sender, FormClosingEventArgs e)
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

	private void LblSup_TextChanged(object sender, EventArgs e)
	{
	}

	private void MenuPrintQued_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text != "")
		{
			Class_Print_DailyRestrictionsALL class_Print_DailyRestrictionsALL = new Class_Print_DailyRestrictionsALL();
			class_Print_DailyRestrictionsALL.Print_DailyRestrictionsALL(Convert.ToInt32(TxtCode.Text), 9);
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

	private decimal FUNC_DecimalNum(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNum);
	}

	private void toggleItems_Toggled(object sender, EventArgs e)
	{
		if (toggleItems.IsOn)
		{
			GridSelectAll();
		}
		else
		{
			GridDisSelectAll();
		}
	}

	private void CBProject_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (CBProject.Items.Count > 0)
			{
				Calss_CostCenter calss_CostCenter = new Calss_CostCenter();
				CBCostCenter.DataSource = calss_CostCenter.GetCostCentersByProject(Convert.ToInt32(CBProject.SelectedValue));
				CBCostCenter.DisplayMember = "CostCentersName";
				CBCostCenter.ValueMember = "CostCentersID";
				CBCostCenter.SelectedValue = calss_CostCenter.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
				CBCostCenter.SelectedIndex = 1;
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

	private decimal FUNC_DecimalNumQty(string Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNumQty);
	}

	private void ZeroDisc()
	{
		TxtDiscount2.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
		Cal_FinalResultTotals(0m, "DiscAmount");
	}

	private void TxtDiscount2_KeyUp_1(object sender, KeyEventArgs e)
	{
		try
		{
			if (!TxtDiscount2.ReadOnly)
			{
				if (TxtDiscount2.Text != string.Empty && Convert.ToDecimal(TxtDiscount2.Text) != 0m && Convert.ToDecimal(TxtDiscount2.Text) < Convert.ToDecimal(TxtTotals.Text))
				{
					decimal per = Convert.ToDecimal(TxtDiscount2.Text);
					Cal_FinalResultTotals(per, "DiscAmount");
				}
				else
				{
					ZeroDisc();
				}
			}
		}
		catch (Exception)
		{
			ZeroDisc();
		}
	}

	private void txtdicoutprege_KeyUp_1(object sender, KeyEventArgs e)
	{
		try
		{
			if (!txtdicoutprege.ReadOnly)
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
		}
		catch (Exception)
		{
			ZeroDisc();
		}
	}

	private void TxtAllDiscount_KeyUp_1(object sender, KeyEventArgs e)
	{
		try
		{
			if (!TxtAllDiscount.ReadOnly)
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
		}
		catch (Exception)
		{
			ZeroDisc();
		}
	}

	private void TxtDiscount2_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void txtdicoutprege_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void TxtAllDiscount_KeyPress(object sender, KeyPressEventArgs e)
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

	private void BtnUpload_Click(object sender, EventArgs e)
	{
		if (LSucced.Text == "Success")
		{
			MessageBox.Show("تم ارسال الفاتوره بالفعل", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			return;
		}
		if (TxtCode.Text == "" || TxtCode.Text == string.Empty)
		{
			MessageBox.Show("يرجعي تحديد فاتوره ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			return;
		}
		int orderId = Convert.ToInt32(TxtCode.Text);
		int valueOrDefault = Zakat.GetIdByOrderId(orderId).GetValueOrDefault();
		SentToSazakt(valueOrDefault);
	}

	private void BtnTypeInvoic_Click(object sender, EventArgs e)
	{
		new FrmInvoicType("OrderRetrn").Show();
	}

	private List<string> GetPaymentMethod(OrderOrdersDto order)
	{
		List<string> list = new List<string>();
		decimal? cashMoney = order.CashMoney;
		if ((cashMoney.GetValueOrDefault() > 0m) & cashMoney.HasValue)
		{
			list.Add("10");
		}
		cashMoney = order.CashBank;
		if ((cashMoney.GetValueOrDefault() > 0m) & cashMoney.HasValue)
		{
			list.Add("48");
		}
		cashMoney = order.BankTransfare;
		if ((cashMoney.GetValueOrDefault() > 0m) & cashMoney.HasValue)
		{
			list.Add("42");
		}
		cashMoney = order.CashMoney;
		if ((cashMoney.GetValueOrDefault() <= 0m) & cashMoney.HasValue)
		{
			cashMoney = order.CashBank;
			if ((cashMoney.GetValueOrDefault() <= 0m) & cashMoney.HasValue)
			{
				cashMoney = order.BankTransfare;
				if ((cashMoney.GetValueOrDefault() <= 0m) & cashMoney.HasValue)
				{
					list.Add("30");
					goto IL_013a;
				}
			}
		}
		list.Add("1");
		goto IL_013a;
		IL_013a:
		return list;
	}

	private List<InvoiceItems> invoiceItems(DataTable orderDetails)
	{
		List<InvoiceItems> list = new List<InvoiceItems>();
		foreach (DataRow row in orderDetails.Rows)
		{
			if (row.Field<decimal>("Quantity") > 0m)
			{
				decimal num = row.Field<decimal>("ItemVatDisc");
				decimal num2 = row.Field<decimal>("UnitPriceDisc");
				decimal num3 = row.Field<decimal>("Quantity");
				decimal num4 = row.Field<decimal>("ItemVatDisc") / (row.Field<decimal>("UnitPriceDisc") * row.Field<decimal>("Quantity")) * 100m;
				decimal value = Math.Round((row.Field<decimal>("Net") == 0m) ? 0m : (row.Field<decimal>("ItemVatDisc") / (row.Field<decimal>("UnitPriceDisc") * row.Field<decimal>("Quantity")) * 100m), 0);
				if (row.Field<decimal>("UnitPriceDisc") < 0.50m || row.Field<decimal>("ItemVatDisc") > 0m)
				{
					value = 15m;
				}
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
					VatPercentage = value
				});
			}
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
			CashMoney = Cash,
			FinCash = Cash,
			CashBank = Bank,
			RoomNum = 1,
			TableNum = 1,
			Sectoral = true,
			DateHold = DateTime.Now,
			BuildingNum = string.Empty,
			Country = "السعوديه",
			OrderTypeElectronicInvoiceId = ((LoginDetails.OrderTypeElectronicInvoiceId == 0) ? 381 : LoginDetails.OrderTypeElectronicInvoiceId),
			NormalOrSimpleInvoice = (string.IsNullOrEmpty(LoginDetails.NormalOrSimpleInvoice) ? "02" : LoginDetails.NormalOrSimpleInvoice),
			ThirdParty = (LoginDetails.ThirdParty ? "1" : "0"),
			NominalInvoice = (LoginDetails.NominalInvoice ? "1" : "0"),
			ExportInvoice = (LoginDetails.ExportInvoice ? "1" : "0"),
			SummaryInvoice = (LoginDetails.SummaryInvoice ? "1" : "0"),
			SelfInvoice = (LoginDetails.SelfInvoice ? "1" : "0")
		};
	}

	private void BtnUpload_Click_1(object sender, EventArgs e)
	{
		if (LSucced.Text == "Success")
		{
			MessageBox.Show("تم ارسال الفاتوره بالفعل", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			return;
		}
		if (TxtPurCode.Text == "" || TxtPurCode.Text == string.Empty)
		{
			MessageBox.Show("يرجعي تحديد فاتوره ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			return;
		}
		int orderId = Convert.ToInt32(TxtPurCode.Text);
		int valueOrDefault = Zakat.GetIdByOrderId(orderId).GetValueOrDefault();
		SentToSazakt(valueOrDefault);
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

	private void GetElctornc()
	{
		if (!LoginDetails.IsElectronicInvoice)
		{
			return;
		}
		int num = 0;
		if (!string.IsNullOrWhiteSpace(TxtCode.Text))
		{
			int orderId = Convert.ToInt32(TxtPurCode.Text);
			num = Zakat.GetIdByOrderId(orderId).GetValueOrDefault();
		}
		if (num != 0)
		{
			ResultElectronicInvoiceXml rowById = Zakat.GetRowById(num);
			LoginDetails.EncHash = rowById.QRCode;
			bool isValid = rowById.IsValid;
			bool valueOrDefault = rowById.Uploaded == true;
			if (isValid)
			{
				LValid.Visible = true;
				LValid.Text = "Valid";
				LValid.BackColor = Color.Green;
			}
			if (valueOrDefault)
			{
				LSucced.Visible = true;
				LSucced.Text = "Success";
				LSucced.BackColor = Color.Green;
			}
			else
			{
				LSucced.Visible = true;
				LSucced.BackColor = Color.Red;
				LSucced.Text = "NotSuccess";
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmOrderReturn));
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
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.تخصيصToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.TxtLocation = new DevExpress.XtraEditors.TextEdit();
		this.label25 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.طباعةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintQued = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPreviewFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.كلالكمياتبالمستودعاتToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.الكميةالحاليةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.الكمياتToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.كلالكمياتبالفروعToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuNewItem = new System.Windows.Forms.ToolStripMenuItem();
		this.حركةصنفToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPurchOrder = new System.Windows.Forms.ToolStripMenuItem();
		this.استعلامToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.رقمالفاتورةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.اسمالموردToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.نوعالفاتورةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.label14 = new System.Windows.Forms.Label();
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
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.TxtQuntity = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.TxtTotalAll = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
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
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtSellPrice3 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtTotalDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.TxtPurCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label26 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label24 = new System.Windows.Forms.Label();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.repositoryItemButtonEditCode = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtNumVat = new DevExpress.XtraEditors.TextEdit();
		this.label18 = new System.Windows.Forms.Label();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnTypeInvoic = new DevExpress.XtraEditors.SimpleButton();
		this.LValid = new System.Windows.Forms.Label();
		this.LSucced = new System.Windows.Forms.Label();
		this.BtnUpload = new DevExpress.XtraEditors.SimpleButton();
		this.label39 = new System.Windows.Forms.Label();
		this.CBProject = new System.Windows.Forms.ComboBox();
		this.label31 = new System.Windows.Forms.Label();
		this.TxtCreditNote = new DevExpress.XtraEditors.TextEdit();
		this.toggleItems = new DevExpress.XtraEditors.ToggleSwitch();
		this.LblSales = new System.Windows.Forms.Label();
		this.toggleHold = new DevExpress.XtraEditors.ToggleSwitch();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.TxtSellPrice2 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.TxtSellPrice1 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtVatDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.TxtAllDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtDiscount2 = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtCost = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTabocc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label29 = new System.Windows.Forms.Label();
		this.TxtAllVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label28 = new System.Windows.Forms.Label();
		this.TxtAllTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label27 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
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
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCreditNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleItems.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleHold.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
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
		this.gridColumn2.OptionsColumn.AllowFocus = false;
		this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn2.OptionsColumn.ReadOnly = true;
		this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
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
		this.gridColumn1.OptionsColumn.AllowEdit = false;
		this.gridColumn1.OptionsColumn.AllowFocus = false;
		this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn1.OptionsColumn.ReadOnly = true;
		this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonEditCodeSearch, "repositoryItemButtonEditCodeSearch");
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons"), resources.GetString("repositoryItemButtonEditCodeSearch.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEditCodeSearch.Buttons6"), resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons9"))
		});
		this.repositoryItemButtonEditCodeSearch.Name = "repositoryItemButtonEditCodeSearch";
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
		resources.ApplyResources(this.MenuExit, "MenuExit");
		this.MenuExit.Name = "MenuExit";
		this.MenuExit.Click += new System.EventHandler(MenuExit_Click);
		resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		resources.ApplyResources(this.تخصيصToolStripMenuItem1, "تخصيصToolStripMenuItem1");
		this.تخصيصToolStripMenuItem1.Name = "تخصيصToolStripMenuItem1";
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
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.كلالكمياتبالمستودعاتToolStripMenuItem, "كلالكمياتبالمستودعاتToolStripMenuItem");
		this.كلالكمياتبالمستودعاتToolStripMenuItem.Name = "كلالكمياتبالمستودعاتToolStripMenuItem";
		resources.ApplyResources(this.الكميةالحاليةToolStripMenuItem, "الكميةالحاليةToolStripMenuItem");
		this.الكميةالحاليةToolStripMenuItem.Name = "الكميةالحاليةToolStripMenuItem";
		resources.ApplyResources(this.الكمياتToolStripMenuItem1, "الكمياتToolStripMenuItem1");
		this.الكمياتToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.الكميةالحاليةToolStripMenuItem, this.كلالكمياتبالمستودعاتToolStripMenuItem, this.كلالكمياتبالفروعToolStripMenuItem });
		this.الكمياتToolStripMenuItem1.Name = "الكمياتToolStripMenuItem1";
		resources.ApplyResources(this.كلالكمياتبالفروعToolStripMenuItem, "كلالكمياتبالفروعToolStripMenuItem");
		this.كلالكمياتبالفروعToolStripMenuItem.Name = "كلالكمياتبالفروعToolStripMenuItem";
		resources.ApplyResources(this.MenuSearchItem, "MenuSearchItem");
		this.MenuSearchItem.Name = "MenuSearchItem";
		this.MenuSearchItem.Click += new System.EventHandler(MenuSearchItem_Click);
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.طباعةToolStripMenuItem1, this.toolStripSeparator1, this.MenuNewItem, this.حركةصنفToolStripMenuItem, this.MenuSearchItem, this.toolStripSeparator4,
			this.MenuPurchOrder, this.استعلامToolStripMenuItem1, this.الكمياتToolStripMenuItem1, this.toolStripSeparator2, this.تخصيصToolStripMenuItem1, this.toolStripSeparator3, this.MenuExit
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
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.MenuNewItem, "MenuNewItem");
		this.MenuNewItem.Name = "MenuNewItem";
		this.MenuNewItem.Click += new System.EventHandler(MenuNewItem_Click);
		resources.ApplyResources(this.حركةصنفToolStripMenuItem, "حركةصنفToolStripMenuItem");
		this.حركةصنفToolStripMenuItem.Name = "حركةصنفToolStripMenuItem";
		this.حركةصنفToolStripMenuItem.Click += new System.EventHandler(حركةصنفToolStripMenuItem_Click);
		resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		resources.ApplyResources(this.MenuPurchOrder, "MenuPurchOrder");
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		this.MenuPurchOrder.Click += new System.EventHandler(MenuPurchOrder_Click);
		resources.ApplyResources(this.استعلامToolStripMenuItem1, "استعلامToolStripMenuItem1");
		this.استعلامToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.رقمالفاتورةToolStripMenuItem1, this.اسمالموردToolStripMenuItem1, this.نوعالفاتورةToolStripMenuItem1 });
		this.استعلامToolStripMenuItem1.Name = "استعلامToolStripMenuItem1";
		resources.ApplyResources(this.رقمالفاتورةToolStripMenuItem1, "رقمالفاتورةToolStripMenuItem1");
		this.رقمالفاتورةToolStripMenuItem1.Name = "رقمالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.اسمالموردToolStripMenuItem1, "اسمالموردToolStripMenuItem1");
		this.اسمالموردToolStripMenuItem1.Name = "اسمالموردToolStripMenuItem1";
		resources.ApplyResources(this.نوعالفاتورةToolStripMenuItem1, "نوعالفاتورةToolStripMenuItem1");
		this.نوعالفاتورةToolStripMenuItem1.Name = "نوعالفاتورةToolStripMenuItem1";
		resources.ApplyResources(this.label14, "label14");
		this.label14.Name = "label14";
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
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[10] { this.repositoryItemButtonEdit4, this.repositoryItemButtonEdit6, this.repositoryItemTextEdit1, this.repositoryItemTextEdit2, this.repositoryItemDateEdit1, this.repositoryItemTextEdit3, this.repositoryItemPictureEdit1, this.repositoryItemButtonEdit5, this.repositoryItemButtonEditCodeSearch, this.BtnPlus });
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView1.Appearance.HeaderPanel.Font");
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		resources.ApplyResources(this.gridView1, "gridView1");
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[29]
		{
			this.gridColumn1, this.gridColumn2, this.shearch, this.gridColumn19, this.gridColumn4, this.gridColumn23, this.gridColumn25, this.gridColumn20, this.gridColumn21, this.gridColumn3,
			this.gridColumn6, this.gridColumn7, this.gridColumn8, this.gridColumn22, this.gridColumn5, this.gridColumn9, this.gridColumn24, this.gridColumn26, this.gridColumn27, this.gridColumn28,
			this.gridColumn30, this.gridColumn29, this.gridColumn31, this.gridColumn32, this.gridColumn33, this.gridColumn34, this.gridColumn35, this.gridColumn36, this.gridColumn37
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
		this.shearch.FieldName = "UnitType";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.AllowEdit = false;
		this.shearch.OptionsColumn.AllowFocus = false;
		this.shearch.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.shearch.OptionsColumn.ReadOnly = true;
		this.shearch.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
		editorButtonImageOptions2.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		editorButtonImageOptions2.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceCell.Font");
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.FieldName = "UnitName";
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.AllowFocus = false;
		this.gridColumn19.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		this.gridColumn19.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
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
		this.gridColumn4.OptionsColumn.AllowEdit = false;
		this.gridColumn4.OptionsColumn.AllowFocus = false;
		this.gridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"), resources.GetString("repositoryItemButtonEdit5.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit5.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit5.Buttons6"), resources.GetObject("repositoryItemButtonEdit5.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit5.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit5.Buttons9"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
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
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn25.ColumnEdit = this.BtnPlus;
		this.gridColumn25.FieldName = "SellQuantity";
		this.gridColumn25.MinWidth = 24;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.OptionsColumn.AllowEdit = false;
		this.gridColumn25.OptionsColumn.AllowFocus = false;
		this.gridColumn25.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.BtnPlus, "BtnPlus");
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.BtnPlus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("BtnPlus.Buttons"), resources.GetString("BtnPlus.Buttons1"), (int)resources.GetObject("BtnPlus.Buttons2"), (bool)resources.GetObject("BtnPlus.Buttons3"), (bool)resources.GetObject("BtnPlus.Buttons4"), (bool)resources.GetObject("BtnPlus.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("BtnPlus.Buttons6"), resources.GetObject("BtnPlus.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("BtnPlus.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("BtnPlus.Buttons9"))
		});
		this.BtnPlus.Name = "BtnPlus";
		this.BtnPlus.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnPlus_ButtonPressed);
		resources.ApplyResources(this.gridColumn20, "gridColumn20");
		this.gridColumn20.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn20.AppearanceCell.Font");
		this.gridColumn20.AppearanceCell.Options.UseFont = true;
		this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn20.AppearanceHeader.Font");
		this.gridColumn20.AppearanceHeader.Options.UseFont = true;
		this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.ColumnEdit = this.BtnPlus;
		this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.FieldName = "Quantity";
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn21.FieldName = "UnitPrice";
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.OptionsColumn.AllowEdit = false;
		this.gridColumn21.OptionsColumn.AllowFocus = false;
		this.gridColumn21.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn21.OptionsColumn.ReadOnly = true;
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
		this.gridColumn3.FieldName = "Total";
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.OptionsColumn.AllowFocus = false;
		this.gridColumn3.OptionsColumn.ReadOnly = true;
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
		this.gridColumn8.FieldName = "DeleteItem";
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit6, "repositoryItemButtonEdit6");
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions5");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
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
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn24.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn26.FieldName = "ItemNote";
		this.gridColumn26.MinWidth = 23;
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
		this.gridColumn27.FieldName = "SumPriceInstall";
		this.gridColumn27.MinWidth = 25;
		this.gridColumn27.Name = "gridColumn27";
		this.gridColumn27.OptionsColumn.AllowEdit = false;
		this.gridColumn27.OptionsColumn.AllowFocus = false;
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
		this.gridColumn28.FieldName = "SumPriceInstallDisc";
		this.gridColumn28.MinWidth = 25;
		this.gridColumn28.Name = "gridColumn28";
		this.gridColumn28.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn30.MinWidth = 25;
		this.gridColumn30.Name = "gridColumn30";
		this.gridColumn30.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn29.FieldName = "AmountDisc";
		this.gridColumn29.MinWidth = 25;
		this.gridColumn29.Name = "gridColumn29";
		this.gridColumn29.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		this.gridColumn31.FieldName = "UnitPriceDisc";
		this.gridColumn31.MinWidth = 25;
		this.gridColumn31.Name = "gridColumn31";
		this.gridColumn31.OptionsColumn.AllowEdit = false;
		this.gridColumn31.OptionsColumn.AllowFocus = false;
		this.gridColumn31.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn32, "gridColumn32");
		this.gridColumn32.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn32.AppearanceCell.Font");
		this.gridColumn32.AppearanceCell.Options.UseFont = true;
		this.gridColumn32.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn32.AppearanceHeader.Font");
		this.gridColumn32.AppearanceHeader.Options.UseFont = true;
		this.gridColumn32.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn32.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn32.FieldName = "IsItemTax";
		this.gridColumn32.MinWidth = 25;
		this.gridColumn32.Name = "gridColumn32";
		this.gridColumn32.OptionsColumn.AllowEdit = false;
		this.gridColumn32.OptionsColumn.AllowFocus = false;
		this.gridColumn32.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn33, "gridColumn33");
		this.gridColumn33.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn33.AppearanceCell.Font");
		this.gridColumn33.AppearanceCell.Options.UseFont = true;
		this.gridColumn33.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn33.AppearanceHeader.Font");
		this.gridColumn33.AppearanceHeader.Options.UseFont = true;
		this.gridColumn33.FieldName = "Tax_Value";
		this.gridColumn33.MinWidth = 25;
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
		this.gridColumn34.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn34.AppearanceHeader.Font");
		this.gridColumn34.AppearanceHeader.Options.UseFont = true;
		this.gridColumn34.FieldName = "VatCode";
		this.gridColumn34.MinWidth = 25;
		this.gridColumn34.Name = "gridColumn34";
		this.gridColumn34.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn35, "gridColumn35");
		this.gridColumn35.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn35.AppearanceCell.Font");
		this.gridColumn35.AppearanceCell.Options.UseFont = true;
		this.gridColumn35.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn35.AppearanceHeader.Font");
		this.gridColumn35.AppearanceHeader.Options.UseFont = true;
		this.gridColumn35.FieldName = "TaxExemptionReason";
		this.gridColumn35.MinWidth = 25;
		this.gridColumn35.Name = "gridColumn35";
		this.gridColumn35.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn36, "gridColumn36");
		this.gridColumn36.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceCell.Font");
		this.gridColumn36.AppearanceCell.Options.UseFont = true;
		this.gridColumn36.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn36.AppearanceHeader.Font");
		this.gridColumn36.AppearanceHeader.Options.UseFont = true;
		this.gridColumn36.FieldName = "TaxExemptionReasonCode";
		this.gridColumn36.MinWidth = 25;
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
		this.gridColumn37.FieldName = "SnOrder";
		this.gridColumn37.MinWidth = 25;
		this.gridColumn37.Name = "gridColumn37";
		this.gridColumn37.OptionsColumn.AllowEdit = false;
		this.gridColumn37.OptionsColumn.AllowFocus = false;
		this.gridColumn37.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
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
		resources.ApplyResources(this.TxtQuntity, "TxtQuntity");
		this.TxtQuntity.DecimalDigits = 3;
		this.TxtQuntity.ForeColor = System.Drawing.Color.Black;
		this.TxtQuntity.Name = "TxtQuntity";
		this.TxtQuntity.ReadOnly = true;
		this.TxtQuntity.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtQuntity.Value = new decimal(new int[4]);
		this.TxtQuntity.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.groupBox3, "groupBox3");
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Controls.Add(this.TxtTotalAll);
		this.groupBox3.Controls.Add(this.label2);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.TabStop = false;
		resources.ApplyResources(this.TxtTotalAll, "TxtTotalAll");
		this.TxtTotalAll.DecimalDigits = 3;
		this.TxtTotalAll.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalAll.Name = "TxtTotalAll";
		this.TxtTotalAll.ReadOnly = true;
		this.TxtTotalAll.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalAll.Value = new decimal(new int[4]);
		this.TxtTotalAll.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalAll.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label32, "label32");
		this.label32.Name = "label32";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
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
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.LBlSuppAccount, "LBlSuppAccount");
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		resources.ApplyResources(this.TxtPurCode, "TxtPurCode");
		this.TxtPurCode.Name = "TxtPurCode";
		this.TxtPurCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtPurCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPurCode.Properties.Appearance.Font");
		this.TxtPurCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtPurCode.Properties.Appearance.Options.UseFont = true;
		this.TxtPurCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPurCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPurCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtPurCode.Properties.ReadOnly = true;
		this.TxtPurCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCust_ButtonClick);
		this.TxtPurCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		resources.ApplyResources(this.TxtType, "TxtType");
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Name = "TxtType";
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
		resources.ApplyResources(this.label23, "label23");
		this.label23.Name = "label23";
		resources.ApplyResources(this.repositoryItemButtonEditCode, "repositoryItemButtonEditCode");
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions6");
		this.repositoryItemButtonEditCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCode.Buttons"), resources.GetString("repositoryItemButtonEditCode.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCode.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCode.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("repositoryItemButtonEditCode.Buttons6"), resources.GetObject("repositoryItemButtonEditCode.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCode.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCode.Buttons9"))
		});
		this.repositoryItemButtonEditCode.Name = "repositoryItemButtonEditCode";
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
		this.TxtPhone.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtPhone.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPhone.Properties.Appearance.Font");
		this.TxtPhone.Properties.Appearance.Options.UseBackColor = true;
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.Properties.ReadOnly = true;
		this.TxtPhone.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.label20, "label20");
		this.label20.Name = "label20";
		resources.ApplyResources(this.label19, "label19");
		this.label19.Name = "label19";
		resources.ApplyResources(this.TxtNumVat, "TxtNumVat");
		this.TxtNumVat.Name = "TxtNumVat";
		this.TxtNumVat.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtNumVat.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumVat.Properties.Appearance.Font");
		this.TxtNumVat.Properties.Appearance.Options.UseBackColor = true;
		this.TxtNumVat.Properties.Appearance.Options.UseFont = true;
		this.TxtNumVat.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumVat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumVat.Properties.ReadOnly = true;
		this.TxtNumVat.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNumVat.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label18, "label18");
		this.label18.Name = "label18";
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
		this.TxtSuppliers.Properties.ReadOnly = true;
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
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.BtnTypeInvoic);
		this.groupBox1.Controls.Add(this.LValid);
		this.groupBox1.Controls.Add(this.LSucced);
		this.groupBox1.Controls.Add(this.BtnUpload);
		this.groupBox1.Controls.Add(this.label39);
		this.groupBox1.Controls.Add(this.CBProject);
		this.groupBox1.Controls.Add(this.label31);
		this.groupBox1.Controls.Add(this.TxtCreditNote);
		this.groupBox1.Controls.Add(this.toggleItems);
		this.groupBox1.Controls.Add(this.LblSales);
		this.groupBox1.Controls.Add(this.toggleHold);
		this.groupBox1.Controls.Add(this.LBlSuppAccount);
		this.groupBox1.Controls.Add(this.TxtPurCode);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Controls.Add(this.CBCostCenter);
		this.groupBox1.Controls.Add(this.LblSup);
		this.groupBox1.Controls.Add(this.TxtType);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.TxtNumNote);
		this.groupBox1.Controls.Add(this.label23);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.TxtPhone);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.TxtNumVat);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.TxtSuppliers);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
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
		this.BtnUpload.Click += new System.EventHandler(BtnUpload_Click_1);
		resources.ApplyResources(this.label39, "label39");
		this.label39.Name = "label39";
		resources.ApplyResources(this.CBProject, "CBProject");
		this.CBProject.FormattingEnabled = true;
		this.CBProject.Name = "CBProject";
		this.CBProject.SelectedIndexChanged += new System.EventHandler(CBProject_SelectedIndexChanged);
		resources.ApplyResources(this.label31, "label31");
		this.label31.Name = "label31";
		resources.ApplyResources(this.TxtCreditNote, "TxtCreditNote");
		this.TxtCreditNote.Name = "TxtCreditNote";
		this.TxtCreditNote.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCreditNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCreditNote.Properties.Appearance.Font");
		this.TxtCreditNote.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCreditNote.Properties.Appearance.Options.UseFont = true;
		this.TxtCreditNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCreditNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCreditNote.Properties.ReadOnly = true;
		resources.ApplyResources(this.toggleItems, "toggleItems");
		this.toggleItems.Name = "toggleItems";
		this.toggleItems.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleItems.Properties.Appearance.Font");
		this.toggleItems.Properties.Appearance.Options.UseFont = true;
		this.toggleItems.Properties.Appearance.Options.UseForeColor = true;
		this.toggleItems.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleItems.Properties.GlyphVerticalAlignment");
		this.toggleItems.Properties.OffText = resources.GetString("toggleItems.Properties.OffText");
		this.toggleItems.Properties.OnText = resources.GetString("toggleItems.Properties.OnText");
		this.toggleItems.Toggled += new System.EventHandler(toggleItems_Toggled);
		resources.ApplyResources(this.LblSales, "LblSales");
		this.LblSales.ForeColor = System.Drawing.Color.RoyalBlue;
		this.LblSales.Name = "LblSales";
		resources.ApplyResources(this.toggleHold, "toggleHold");
		this.toggleHold.Name = "toggleHold";
		this.toggleHold.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleHold.Properties.Appearance.Font");
		this.toggleHold.Properties.Appearance.Options.UseFont = true;
		this.toggleHold.Properties.Appearance.Options.UseForeColor = true;
		this.toggleHold.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleHold.Properties.GlyphVerticalAlignment");
		this.toggleHold.Properties.OffText = resources.GetString("toggleHold.Properties.OffText");
		this.toggleHold.Properties.OnText = resources.GetString("toggleHold.Properties.OnText");
		this.toggleHold.Properties.ReadOnly = true;
		this.ribbonPage2.Name = "ribbonPage2";
		resources.ApplyResources(this.ribbonPage2, "ribbonPage2");
		resources.ApplyResources(this.TxtSellPrice2, "TxtSellPrice2");
		this.TxtSellPrice2.DecimalDigits = 3;
		this.TxtSellPrice2.ForeColor = System.Drawing.Color.Black;
		this.TxtSellPrice2.Name = "TxtSellPrice2";
		this.TxtSellPrice2.ReadOnly = true;
		this.TxtSellPrice2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtSellPrice2.Value = new decimal(new int[4]);
		this.TxtSellPrice2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions8");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		resources.ApplyResources(editorButtonImageOptions9, "editorButtonImageOptions9");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"), resources.GetString("repositoryItemButtonEdit3.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit3.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit3.Buttons5"), editorButtonImageOptions9, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance9, appearanceHovered9, appearancePressed9, appearanceDisabled9, resources.GetString("repositoryItemButtonEdit3.Buttons6"), resources.GetObject("repositoryItemButtonEdit3.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit3.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit3.Buttons9"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.TxtAllDiscount);
		this.groupBox2.Controls.Add(this.txtdicoutprege);
		this.groupBox2.Controls.Add(this.TxtDiscount2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtCost);
		this.groupBox2.Controls.Add(this.TxtTabocc);
		this.groupBox2.Controls.Add(this.label29);
		this.groupBox2.Controls.Add(this.TxtAllVat);
		this.groupBox2.Controls.Add(this.label28);
		this.groupBox2.Controls.Add(this.label32);
		this.groupBox2.Controls.Add(this.TxtAllTotal);
		this.groupBox2.Controls.Add(this.label27);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label30);
		this.groupBox2.Controls.Add(this.TxtLocation);
		this.groupBox2.Controls.Add(this.label25);
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
		this.groupBox2.Controls.Add(this.TxtSellPrice2);
		this.groupBox2.Controls.Add(this.TxtSellPrice1);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.TxtVatDisc);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.TxtAllDiscount, "TxtAllDiscount");
		this.TxtAllDiscount.DecimalDigits = 3;
		this.TxtAllDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtAllDiscount.Name = "TxtAllDiscount";
		this.TxtAllDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllDiscount.Value = new decimal(new int[4]);
		this.TxtAllDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtAllDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtAllDiscount_KeyPress);
		this.TxtAllDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtAllDiscount_KeyUp_1);
		resources.ApplyResources(this.txtdicoutprege, "txtdicoutprege");
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Black;
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.txtdicoutprege.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtdicoutprege_KeyPress);
		this.txtdicoutprege.KeyUp += new System.Windows.Forms.KeyEventHandler(txtdicoutprege_KeyUp_1);
		resources.ApplyResources(this.TxtDiscount2, "TxtDiscount2");
		this.TxtDiscount2.DecimalDigits = 3;
		this.TxtDiscount2.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscount2.Name = "TxtDiscount2";
		this.TxtDiscount2.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount2.Value = new decimal(new int[4]);
		this.TxtDiscount2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscount2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtDiscount2_KeyPress);
		this.TxtDiscount2.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtDiscount2_KeyUp_1);
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
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmOrderReturn";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmOrderReturn_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
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
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtPurCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCreditNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleItems.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleHold.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
