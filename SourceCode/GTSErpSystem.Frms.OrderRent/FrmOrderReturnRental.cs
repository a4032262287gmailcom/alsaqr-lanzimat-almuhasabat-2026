using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
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
using GTSErpSystem.BLL.OrderRent;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.Frms.OrderRent.Search;
using Humanizer;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.OrderRent;

public class FrmOrderReturnRental : XtraForm
{
	private DataTable datatable = new DataTable();

	private Class_Contract Contract = new Class_Contract();

	private Class_Rent Rent = new Class_Rent();

	private Class_Orders ORD = new Class_Orders();

	private Class_Customer Cust = new Class_Customer();

	private Class_PriceShowRent PSR = new Class_PriceShowRent();

	private GTSdbEntities db = new GTSdbEntities();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private Class_OrdersReturn PurRTN = new Class_OrdersReturn();

	private Class_Orders Pur = new Class_Orders();

	public string words;

	public string CodeItm;

	private int SN = 1;

	private int ItmID = 0;

	private int StorId = 0;

	private string UnitName = string.Empty;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	private string fileNameAR = Application.StartupPath + "\\Layout\\FrmRentalReturnInvoice.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\FrmRentalReturnInvoice.xml";

	private IContainer components = null;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn gridColumn27;

	private GridColumn shearch;

	private GridColumn gridColumn19;

	private GridColumn gridColumn4;

	private GridColumn gridColumn23;

	private GridColumn gridColumn24;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private GridColumn gridColumn22;

	private GridColumn gridColumn5;

	private GridColumn gridColumn9;

	private GridColumn gridColumn26;

	private GridColumn gridColumn28;

	private GridColumn gridColumn29;

	private GridColumn gridColumn30;

	private GridColumn gridColumn31;

	private GridColumn gridColumn32;

	private GridColumn gridColumn33;

	private GridColumn gridColumn34;

	private GridColumn gridColumn35;

	private GridColumn gridColumn36;

	private GridColumn gridColumn25;

	private GridColumn gridColumn37;

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

	private GroupBox groupBox1;

	private Label label14;

	private TextEdit TxtDayNum;

	private DateEdit TxtDate;

	private Label label13;

	private Label label10;

	private TextEdit TxtSuppIDNum;

	private Label label9;

	private Label LBlSuppAccount;

	private Label label26;

	private TextEdit TxtAddress;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private Label LblSup;

	private DateEdit TxtRentalDate;

	private Label label23;

	private TextEdit TxtParentName;

	private Label label22;

	private TextEdit TxtPhone;

	private Label label21;

	private Label label18;

	private ButtonEdit TxtSuppliers;

	private Label label17;

	private ButtonEdit TxtCode;

	private GroupBox groupBox2;

	private Label label12;

	private NumericEditBox TxtDeposit;

	private TextEdit TxtLocation;

	private Label label25;

	private NumericEditBox txtdicoutprege;

	private TextEdit TxtTafqit;

	private Label label11;

	private NumericEditBox TxtNet;

	private TextEdit TxtNote;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private SimpleButton BtnNew;

	private Label label8;

	private SimpleButton BtnDelete;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtAllDiscount;

	private SimpleButton BtnExit;

	private NumericEditBox TxtTotals;

	private NumericEditBox TxtTotalDiscount;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private NumericEditBox TxtDiscount;

	private NumericEditBox TxtVatDisc;

	private Label label5;

	private Label label6;

	private GridColumn gridColumn38;

	private Label label7;

	private ButtonEdit BtnRental;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private RepositoryItemButtonEdit BtnPlus;

	public FrmOrderReturnRental()
	{
		InitializeComponent();
		SetInitialRow();
		Clear();
		Bind();
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
								decimal num5 = Math.Round(FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * Convert.ToDecimal(num3) * num4), 2);
								if (num5 > 200m)
								{
									num5 = Math.Round(num5, 1);
								}
								decimal num6 = FUNC_DecimalNum(num5 / num4);
								row["ItemVat"] = FUNC_DecimalNum(num5 - num6);
								row["Total"] = num5;
								row["SumPrice"] = num6;
								row["SumPriceDisc"] = num6;
								row["IsItemTax"] = 1;
								row["Tax_Value"] = item_Items.Tax_Value;
								row["ItemVatDisc"] = row["ItemVat"];
							}
							else
							{
								decimal num7 = FUNC_DecimalNum(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"])) * Convert.ToDecimal(num3));
								row["ItemVat"] = FUNC_DecimalNum(num7 * (Convert.ToDecimal(item_Items.Tax_Value) / 100m));
								row["Total"] = num7;
								row["SumPriceDisc"] = num7;
								row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["ItemVat"]) + num7);
								row["IsItemTax"] = 1;
								row["Tax_Value"] = item_Items.Tax_Value;
								row["ItemVatDisc"] = row["ItemVat"];
							}
						}
						else
						{
							decimal num8 = FUNC_DecimalNum(Convert.ToDecimal(Convert.ToDecimal(row["UnitPrice"])) * Convert.ToDecimal(num3));
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
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
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
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
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
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
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
				Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
			}
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

	private void FrmOrderReturnRental_Load(object sender, EventArgs e)
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

	private void FrmOrderReturnRental_FormClosing(object sender, FormClosingEventArgs e)
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

	private void BtnRental_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchRentalInvoice frmSearchRentalInvoice = new FrmSearchRentalInvoice();
			frmSearchRentalInvoice.ShowDialog();
			if (frmSearchRentalInvoice.CustCode > 0)
			{
				int custCode = frmSearchRentalInvoice.CustCode;
				Orders_RentalInvoice orders_RentalInvoice = Rent.GetPurchasesRental(custCode).FirstOrDefault();
				CBCostCenter.SelectedValue = orders_RentalInvoice.CostCentersID;
				BtnRental.Text = orders_RentalInvoice.PurBranchID.ToString();
				LblSup.Text = orders_RentalInvoice.SupplierID.ToString();
				TxtSuppliers.Text = orders_RentalInvoice.SupplierName;
				TxtRentalDate.DateTime = Convert.ToDateTime(orders_RentalInvoice.RentalDate);
				TxtParentName.Text = orders_RentalInvoice.ParentName;
				TxtPhone.Text = orders_RentalInvoice.SupplierPhone;
				TxtNote.Text = orders_RentalInvoice.Note;
				TxtDeposit.Text = orders_RentalInvoice.deposit.ToString();
				TxtAddress.Text = orders_RentalInvoice.Address;
				TxtSuppIDNum.Text = orders_RentalInvoice.SupplierIDNum;
				TxtDayNum.Text = orders_RentalInvoice.DayNums.ToString();
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = true;
				TxtSuppliers.Focus();
				datatable.Rows.Clear();
				List<Ordert_RentalInvoiceDetails> purchasesRentalDetails = Rent.GetPurchasesRentalDetails(custCode);
				int num = 1;
				foreach (Ordert_RentalInvoiceDetails item in purchasesRentalDetails)
				{
					DataRow dataRow = null;
					int itemID = Convert.ToInt32(item.ItemID);
					Item_Items item_Items = ORD.GetItemsByitemid(itemID).FirstOrDefault();
					dataRow = datatable.NewRow();
					dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
					dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
					dataRow["ItemNote"] = item.ItemNote;
					dataRow["DayNum"] = FUNC_DecimalNum(Convert.ToDecimal(item.DaysNum));
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
					dataRow["StoreID"] = item.StoreID;
					dataRow["StoreName"] = (from x in db.Account_Stores
						where (int?)x.ID == item.StoreID
						select x.Store_Name).FirstOrDefault();
					dataRow["SellQuantity"] = FUNC_DecimalNum(Convert.ToDecimal(item.Quantity));
					dataRow["Quantity"] = 0.0;
					dataRow["AvergItem"] = item.LastCost;
					dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["SellQuantity"]), DecimalNum);
					dataRow["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.UnitPrice));
					dataRow["OrginalPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.Total) / Convert.ToDecimal(item.Quantity));
					dataRow["UnitPriceDisc"] = Math.Round(Convert.ToDecimal(item.Total), DecimalNum);
					dataRow["PercentDisc"] = Math.Round(Convert.ToDecimal(item.PercentDisc), DecimalNum);
					dataRow["AmountDisc"] = Math.Round(Convert.ToDecimal(item.AmountDisc), DecimalNum);
					dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(item.SumPrice), DecimalNum);
					dataRow["Total"] = Math.Round(Convert.ToDecimal(item.Total), DecimalNum);
					dataRow["SumPriceDisc"] = Math.Round(Convert.ToDecimal(item.SumPriceDisc), DecimalNum);
					decimal? vAT = item.VAT;
					if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
					{
						dataRow["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
						dataRow["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
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
						dataRow["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice));
					}
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
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
		Tafqit();
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		NewFrm();
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
		datatable.Columns.Add("SellQuantity", typeof(decimal));
		datatable.Columns.Add("DayNum", typeof(decimal));
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
		datatable.Columns.Add("IsItemTax", typeof(bool));
		datatable.Columns.Add("Tax_Value", typeof(decimal));
		datatable.Columns.Add("DeleteItem", typeof(int));
		datatable.Columns.Add("SN", typeof(int));
		datatable.Columns.Add("OriginalSN", typeof(int));
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		gridView1.CustomDrawFooterCell += gridView1_CustomDrawFooterCell;
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
		dataRow["DayNum"] = 0;
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
		dataRow["StoreID"] = Contract.Get_StoresIDByName(dataRow["StoreName"].ToString());
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
		dataRow["IsItemTax"] = false;
		dataRow["Tax_Value"] = 0;
		dataRow["DeleteItem"] = 1;
		datatable.Rows.Add(dataRow);
		ArrangeDatatable();
		gridControl1.DataSource = datatable;
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
			row["SN"] = num;
			num++;
		}
	}

	private void Tafqit()
	{
		try
		{
			decimal num = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
			int num2 = (int)Math.Floor(num);
			int num3 = (int)((num - (decimal)num2) * 100m);
			if (LoginDetails.Lang == "ar-EG")
			{
				words = num2.ToWords(new CultureInfo("ar")) + ((num3 > 0) ? (" فاصلة " + num3.ToWords(new CultureInfo("ar"))) : "") + " ريال سعودي";
			}
			else
			{
				words = num2.ToWords(new CultureInfo("en")) + ((num3 > 0) ? (" point " + num3.ToWords(new CultureInfo("en"))) : "Saudi riyal") + "Saudi riyal";
			}
			TxtTafqit.Text = words;
		}
		catch (Exception)
		{
		}
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = true;
		TxtRentalDate.ReadOnly = true;
		TxtDayNum.ReadOnly = true;
		TxtDate.ReadOnly = true;
		TxtAddress.ReadOnly = true;
		TxtSuppIDNum.ReadOnly = true;
		TxtParentName.ReadOnly = true;
		TxtSuppliers.ReadOnly = true;
		TxtPhone.ReadOnly = true;
		TxtNote.ReadOnly = true;
		TxtDiscount.ReadOnly = true;
		TxtAllDiscount.ReadOnly = true;
		txtdicoutprege.ReadOnly = true;
		CBCostCenter.Enabled = false;
		TxtDiscount.BackColor = Color.White;
		TxtRentalDate.BackColor = Color.White;
		TxtDayNum.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
		TxtNote.BackColor = Color.White;
		TxtSuppliers.BackColor = Color.White;
		TxtAddress.BackColor = Color.White;
		TxtParentName.BackColor = Color.White;
		TxtSuppIDNum.BackColor = Color.White;
		TxtPhone.BackColor = Color.White;
	}

	private void Clear()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtCode.Text = "";
		BtnRental.Text = "";
		datatable.Rows.Clear();
		gridControl1.DataSource = datatable;
		TxtSuppliers.Text = "";
		TxtAddress.Text = "";
		TxtSuppIDNum.Text = "";
		TxtParentName.Text = "";
		TxtDiscount.Text = "";
		TxtDayNum.Text = "";
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtVat.Text = "0.00";
		TxtSuppliers.Text = Cust.GetFirstCustomer();
		TxtPhone.Text = "";
		TxtNote.Text = "";
		TxtVatDisc.Text = "0.00";
		TxtTafqit.Text = "";
		TxtTotals.Text = "0.00";
		TxtDeposit.Text = "0.00";
		TxtTotalDiscount.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtLocation.Text = "";
		TxtRentalDate.DateTime = DateTime.Now;
		TxtDate.DateTime = DateTime.Now;
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
		TxtDiscount.Enabled = true;
		TxtParentName.Enabled = true;
		TxtRentalDate.Enabled = true;
		TxtDate.Enabled = true;
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
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
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
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
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
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num2);
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(ItemVat)", string.Empty));
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num3.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = num4.ToString();
		TxtNet.Text = (num2 + num4).ToString();
	}

	private void No_Discount_PriceWithVat()
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
			if (Convert.ToInt32(row["ItemID"]) > 0)
			{
				row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["UnitPrice"]) * num);
				decimal num2 = ((row["Tax_Value"] != DBNull.Value) ? Convert.ToDecimal(row["Tax_Value"]) : 0m);
				row["Total"] = Math.Round(Convert.ToDecimal(row["SumPrice"]) * (num2 / 100m + 1m), 1);
				row["ItemVat"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) - Convert.ToDecimal(row["SumPrice"]));
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
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(SumPriceDisc)", string.Empty));
		TxtTotals.Text = Convert.ToString(num3);
		decimal num4 = Convert.ToDecimal(datatable.Compute("SUM(ItemVatDisc)", string.Empty));
		TxtVat.Text = num4.ToString();
		TxtTotalDiscount.Text = TxtTotals.Text;
		TxtVatDisc.Text = TxtVat.Text;
		TxtNet.Text = (num3 + num4).ToString();
	}

	private void DiscountPercent_NoVat(decimal PerNum, string DicountType)
	{
		foreach (DataRow row in datatable.Rows)
		{
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
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
			decimal num3 = FUNC_DecimalNum(Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["DayNum"]));
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
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
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
			decimal num4 = FUNC_DecimalNum(Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["DayNum"]));
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
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["Quantity"]) * Convert.ToDecimal(row["DayNum"]));
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
			decimal num4 = FUNC_DecimalNum(Convert.ToDecimal(row2["Quantity"]) * Convert.ToDecimal(row2["DayNum"]));
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

	private void NewFrm()
	{
		datatable.Rows.Clear();
		gridControl1.Enabled = true;
		gridView1.OptionsBehavior.Editable = true;
		Clear();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		InsertNewRow();
		TxtCode.Text = string.Empty;
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtSuppliers.Focus();
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

	public void Bind()
	{
		try
		{
			CBCostCenter.DataSource = Cost.GetSearchAccountCostCenter();
			CBCostCenter.DisplayMember = "CostCentersName";
			CBCostCenter.ValueMember = "CostCentersID";
			CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
			TxtSuppliers.Text = Cust.GetFirstCustomer();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
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

	private decimal FUNC_DecimalNumQty(string Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNumQty);
	}

	private void repositoryItemButtonEdit1_ButtonPressed(object sender, ButtonPressedEventArgs e)
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

	private void GridPlus()
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		gridView1.FocusedColumn = gridView1.VisibleColumns[3];
		gridView1.ShowEditor();
		ArrangeDatatable();
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[6];
		gridView1.ShowEditor();
	}

	private void BtnPlus_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		GridPlus();
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.OrderRent.FrmOrderReturnRental));
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.BtnPlus = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label7 = new System.Windows.Forms.Label();
		this.BtnRental = new DevExpress.XtraEditors.ButtonEdit();
		this.label14 = new System.Windows.Forms.Label();
		this.TxtDayNum = new DevExpress.XtraEditors.TextEdit();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label13 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtSuppIDNum = new DevExpress.XtraEditors.TextEdit();
		this.label9 = new System.Windows.Forms.Label();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.TxtAddress = new DevExpress.XtraEditors.TextEdit();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtRentalDate = new DevExpress.XtraEditors.DateEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtParentName = new DevExpress.XtraEditors.TextEdit();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtDeposit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtLocation = new DevExpress.XtraEditors.TextEdit();
		this.label25 = new System.Windows.Forms.Label();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label8 = new System.Windows.Forms.Label();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtAllDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotalDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnPlus).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnRental.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDayNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppIDNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtParentName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		base.SuspendLayout();
		this.gridControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridControl1.Location = new System.Drawing.Point(12, 180);
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[2] { this.repositoryItemButtonEdit1, this.BtnPlus });
		this.gridControl1.Size = new System.Drawing.Size(1208, 271);
		this.gridControl1.TabIndex = 7;
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 20f);
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[30]
		{
			this.gridColumn1, this.gridColumn2, this.gridColumn27, this.shearch, this.gridColumn19, this.gridColumn4, this.gridColumn23, this.gridColumn24, this.gridColumn20, this.gridColumn21,
			this.gridColumn3, this.gridColumn6, this.gridColumn7, this.gridColumn8, this.gridColumn22, this.gridColumn5, this.gridColumn9, this.gridColumn26, this.gridColumn28, this.gridColumn29,
			this.gridColumn30, this.gridColumn31, this.gridColumn32, this.gridColumn33, this.gridColumn34, this.gridColumn35, this.gridColumn36, this.gridColumn25, this.gridColumn37, this.gridColumn38
		});
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "", null, "")
		});
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
		this.gridColumn1.Caption = "كود الصنف";
		this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.FieldName = "ItemCode";
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.OptionsColumn.AllowEdit = false;
		this.gridColumn1.OptionsColumn.AllowFocus = false;
		this.gridColumn1.Visible = true;
		this.gridColumn1.VisibleIndex = 0;
		this.gridColumn1.Width = 149;
		this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.Caption = "أسم الصنف";
		this.gridColumn2.FieldName = "ItemName";
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.gridColumn2.OptionsColumn.AllowFocus = false;
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
		this.gridColumn27.Visible = true;
		this.gridColumn27.VisibleIndex = 2;
		this.gridColumn27.Width = 200;
		this.shearch.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.shearch.AppearanceCell.Options.UseFont = true;
		this.shearch.AppearanceCell.Options.UseTextOptions = true;
		this.shearch.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.shearch.AppearanceHeader.Options.UseFont = true;
		this.shearch.AppearanceHeader.Options.UseTextOptions = true;
		this.shearch.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.Caption = "اسم الوحده";
		this.shearch.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.FieldName = "UnitName";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		this.shearch.Visible = true;
		this.shearch.VisibleIndex = 3;
		this.shearch.Width = 61;
		this.gridColumn19.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.Caption = "نوع الوحده";
		this.gridColumn19.FieldName = "UnitType";
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.AllowFocus = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.Caption = "المخزن";
		this.gridColumn4.FieldName = "StoreName";
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		this.gridColumn4.Visible = true;
		this.gridColumn4.VisibleIndex = 4;
		this.gridColumn23.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn23.AppearanceCell.Options.UseFont = true;
		this.gridColumn23.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn23.AppearanceHeader.Options.UseFont = true;
		this.gridColumn23.Caption = "متوسط التكلفة";
		this.gridColumn23.FieldName = "AvergItem";
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.OptionsColumn.AllowEdit = false;
		this.gridColumn23.OptionsColumn.AllowFocus = false;
		this.gridColumn24.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn24.AppearanceCell.Options.UseFont = true;
		this.gridColumn24.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn24.AppearanceHeader.Options.UseFont = true;
		this.gridColumn24.Caption = "اجمالي متوسط التكلفة";
		this.gridColumn24.FieldName = "SumAvergItem";
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn24.OptionsColumn.AllowEdit = false;
		this.gridColumn24.OptionsColumn.AllowFocus = false;
		this.gridColumn20.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn20.AppearanceCell.Options.UseFont = true;
		this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn20.AppearanceHeader.Options.UseFont = true;
		this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.Caption = "الكميه المرتجعه";
		this.gridColumn20.ColumnEdit = this.BtnPlus;
		this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.FieldName = "Quantity";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", "SUM={0:0.##}")
		});
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn20.Visible = true;
		this.gridColumn20.VisibleIndex = 5;
		this.gridColumn20.Width = 80;
		this.BtnPlus.AutoHeight = false;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions3.Image");
		this.BtnPlus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.BtnPlus.Name = "BtnPlus";
		this.BtnPlus.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnPlus_ButtonPressed);
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
		this.gridColumn21.FieldName = "UnitPrice";
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.OptionsColumn.AllowEdit = false;
		this.gridColumn21.OptionsColumn.AllowFocus = false;
		this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "UnitPrice", "SUM={0:0.##}")
		});
		this.gridColumn21.Visible = true;
		this.gridColumn21.VisibleIndex = 6;
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
		this.gridColumn3.FieldName = "Total";
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.OptionsColumn.AllowFocus = false;
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn3.Visible = true;
		this.gridColumn3.VisibleIndex = 8;
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
		this.gridColumn6.FieldName = "ItemVat";
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.AllowFocus = false;
		this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
		this.gridColumn6.OptionsColumn.ReadOnly = true;
		this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ItemVat", "SUM={0:0.##}")
		});
		this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn6.Visible = true;
		this.gridColumn6.VisibleIndex = 7;
		this.gridColumn6.Width = 80;
		this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.Caption = "الاجمالي بالضريبه";
		this.gridColumn7.FieldName = "Net";
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.OptionsColumn.AllowFocus = false;
		this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Net", "SUM={0:0.##}")
		});
		this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn7.Visible = true;
		this.gridColumn7.VisibleIndex = 9;
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
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit1;
		this.gridColumn8.CustomizationCaption = "ط­1ظپ";
		this.gridColumn8.FieldName = "DeleteItem";
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Visible = true;
		this.gridColumn8.VisibleIndex = 10;
		this.gridColumn8.Width = 27;
		this.repositoryItemButtonEdit1.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit1.AutoHeight = false;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions4.Image");
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonPressed);
		this.gridColumn22.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn22.AppearanceCell.Options.UseFont = true;
		this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn22.AppearanceHeader.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.Caption = "إجمالى بعد الخصم";
		this.gridColumn22.FieldName = "SumPriceDisc";
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn22.OptionsColumn.AllowEdit = false;
		this.gridColumn22.OptionsColumn.AllowFocus = false;
		this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.Caption = "الضريبه بعد الخصم";
		this.gridColumn5.FieldName = "ItemVatDisc";
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.OptionsColumn.AllowEdit = false;
		this.gridColumn5.OptionsColumn.AllowFocus = false;
		this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.Caption = "ظ…";
		this.gridColumn9.FieldName = "SN";
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn9.Visible = true;
		this.gridColumn9.VisibleIndex = 11;
		this.gridColumn9.Width = 40;
		this.gridColumn26.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn26.AppearanceCell.Options.UseFont = true;
		this.gridColumn26.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn26.AppearanceHeader.Options.UseFont = true;
		this.gridColumn26.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.Caption = "المجموع";
		this.gridColumn26.FieldName = "SumPrice";
		this.gridColumn26.MinWidth = 25;
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.OptionsColumn.AllowEdit = false;
		this.gridColumn26.OptionsColumn.AllowFocus = false;
		this.gridColumn26.Visible = true;
		this.gridColumn26.VisibleIndex = 12;
		this.gridColumn26.Width = 94;
		this.gridColumn28.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn28.AppearanceCell.Options.UseFont = true;
		this.gridColumn28.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn28.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn28.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn28.AppearanceHeader.Options.UseFont = true;
		this.gridColumn28.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn28.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn28.Caption = "رقم المخزن";
		this.gridColumn28.FieldName = "StoreID";
		this.gridColumn28.MinWidth = 25;
		this.gridColumn28.Name = "gridColumn28";
		this.gridColumn28.OptionsColumn.AllowEdit = false;
		this.gridColumn28.OptionsColumn.AllowFocus = false;
		this.gridColumn28.Visible = true;
		this.gridColumn28.VisibleIndex = 13;
		this.gridColumn28.Width = 94;
		this.gridColumn29.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn29.AppearanceCell.Options.UseFont = true;
		this.gridColumn29.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn29.AppearanceHeader.Options.UseFont = true;
		this.gridColumn29.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.Caption = "قيمه الخصم";
		this.gridColumn29.FieldName = "AmountDisc";
		this.gridColumn29.MinWidth = 25;
		this.gridColumn29.Name = "gridColumn29";
		this.gridColumn29.Visible = true;
		this.gridColumn29.VisibleIndex = 14;
		this.gridColumn29.Width = 94;
		this.gridColumn30.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn30.AppearanceCell.Options.UseFont = true;
		this.gridColumn30.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn30.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn30.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn30.AppearanceHeader.Options.UseFont = true;
		this.gridColumn30.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn30.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn30.Caption = "نسبه الخصم";
		this.gridColumn30.FieldName = "PercentDisc";
		this.gridColumn30.MinWidth = 25;
		this.gridColumn30.Name = "gridColumn30";
		this.gridColumn30.Visible = true;
		this.gridColumn30.VisibleIndex = 15;
		this.gridColumn30.Width = 94;
		this.gridColumn31.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn31.AppearanceCell.Options.UseFont = true;
		this.gridColumn31.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn31.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn31.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn31.AppearanceHeader.Options.UseFont = true;
		this.gridColumn31.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn31.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn31.Caption = "هل ضريبه";
		this.gridColumn31.FieldName = "IsItemTax";
		this.gridColumn31.MinWidth = 25;
		this.gridColumn31.Name = "gridColumn31";
		this.gridColumn31.OptionsColumn.AllowEdit = false;
		this.gridColumn31.OptionsColumn.AllowFocus = false;
		this.gridColumn31.Width = 94;
		this.gridColumn32.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn32.AppearanceCell.Options.UseFont = true;
		this.gridColumn32.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn32.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn32.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn32.AppearanceHeader.Options.UseFont = true;
		this.gridColumn32.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn32.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn32.Caption = "قيمه الضريبه";
		this.gridColumn32.FieldName = "Tax_Value";
		this.gridColumn32.MinWidth = 25;
		this.gridColumn32.Name = "gridColumn32";
		this.gridColumn32.OptionsColumn.AllowEdit = false;
		this.gridColumn32.OptionsColumn.AllowFocus = false;
		this.gridColumn32.Width = 94;
		this.gridColumn33.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn33.AppearanceCell.Options.UseFont = true;
		this.gridColumn33.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn33.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn33.AppearanceHeader.Options.UseFont = true;
		this.gridColumn33.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn33.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn33.Caption = "ItemID";
		this.gridColumn33.FieldName = "ItemID";
		this.gridColumn33.MinWidth = 25;
		this.gridColumn33.Name = "gridColumn33";
		this.gridColumn33.OptionsColumn.AllowEdit = false;
		this.gridColumn33.OptionsColumn.AllowFocus = false;
		this.gridColumn33.Width = 94;
		this.gridColumn34.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn34.AppearanceCell.Options.UseFont = true;
		this.gridColumn34.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn34.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn34.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn34.AppearanceHeader.Options.UseFont = true;
		this.gridColumn34.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn34.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn34.Caption = "سعر بعد الخصم";
		this.gridColumn34.FieldName = "UnitPriceDisc";
		this.gridColumn34.MinWidth = 25;
		this.gridColumn34.Name = "gridColumn34";
		this.gridColumn34.OptionsColumn.AllowEdit = false;
		this.gridColumn34.OptionsColumn.AllowFocus = false;
		this.gridColumn34.Visible = true;
		this.gridColumn34.VisibleIndex = 16;
		this.gridColumn34.Width = 94;
		this.gridColumn35.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn35.AppearanceCell.Options.UseFont = true;
		this.gridColumn35.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn35.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn35.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn35.AppearanceHeader.Options.UseFont = true;
		this.gridColumn35.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn35.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn35.Caption = "رقم الوحده";
		this.gridColumn35.FieldName = "ItemUnitID";
		this.gridColumn35.MinWidth = 25;
		this.gridColumn35.Name = "gridColumn35";
		this.gridColumn35.OptionsColumn.AllowEdit = false;
		this.gridColumn35.OptionsColumn.AllowFocus = false;
		this.gridColumn35.Visible = true;
		this.gridColumn35.VisibleIndex = 17;
		this.gridColumn35.Width = 94;
		this.gridColumn36.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn36.AppearanceCell.Options.UseFont = true;
		this.gridColumn36.Caption = "OrginalPrice";
		this.gridColumn36.FieldName = "OrginalPrice";
		this.gridColumn36.MinWidth = 25;
		this.gridColumn36.Name = "gridColumn36";
		this.gridColumn36.OptionsColumn.AllowEdit = false;
		this.gridColumn36.OptionsColumn.AllowFocus = false;
		this.gridColumn36.Width = 94;
		this.gridColumn25.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.Caption = "عدد الايام";
		this.gridColumn25.FieldName = "DayNum";
		this.gridColumn25.MinWidth = 25;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.OptionsColumn.AllowEdit = false;
		this.gridColumn25.OptionsColumn.AllowFocus = false;
		this.gridColumn25.OptionsFilter.AllowAutoFilter = false;
		this.gridColumn25.OptionsFilter.AllowFilter = false;
		this.gridColumn25.Visible = true;
		this.gridColumn25.VisibleIndex = 18;
		this.gridColumn25.Width = 94;
		this.gridColumn37.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn37.AppearanceCell.Options.UseFont = true;
		this.gridColumn37.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn37.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn37.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn37.AppearanceHeader.Options.UseFont = true;
		this.gridColumn37.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn37.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn37.Caption = "تفاصيل الصنف";
		this.gridColumn37.FieldName = "Info";
		this.gridColumn37.MinWidth = 25;
		this.gridColumn37.Name = "gridColumn37";
		this.gridColumn37.Visible = true;
		this.gridColumn37.VisibleIndex = 19;
		this.gridColumn37.Width = 94;
		this.gridColumn38.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn38.AppearanceCell.Options.UseFont = true;
		this.gridColumn38.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn38.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn38.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn38.AppearanceHeader.Options.UseFont = true;
		this.gridColumn38.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn38.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn38.Caption = "كميه البيع";
		this.gridColumn38.FieldName = "SellQuantity";
		this.gridColumn38.MinWidth = 25;
		this.gridColumn38.Name = "gridColumn38";
		this.gridColumn38.OptionsColumn.AllowEdit = false;
		this.gridColumn38.OptionsColumn.AllowFocus = false;
		this.gridColumn38.Visible = true;
		this.gridColumn38.VisibleIndex = 20;
		this.gridColumn38.Width = 94;
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
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.BtnRental);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.TxtDayNum);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.TxtSuppIDNum);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.LBlSuppAccount);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Controls.Add(this.TxtAddress);
		this.groupBox1.Controls.Add(this.CBCostCenter);
		this.groupBox1.Controls.Add(this.LblSup);
		this.groupBox1.Controls.Add(this.TxtRentalDate);
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
		this.groupBox1.Size = new System.Drawing.Size(1208, 162);
		this.groupBox1.TabIndex = 8;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "بيانات فاتورة التأجير";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label7.Location = new System.Drawing.Point(632, 25);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(84, 21);
		this.label7.TabIndex = 144;
		this.label7.Text = "رقم التاجير";
		this.BtnRental.Location = new System.Drawing.Point(6, 22);
		this.BtnRental.Name = "BtnRental";
		this.BtnRental.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnRental.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnRental.Properties.Appearance.Options.UseBackColor = true;
		this.BtnRental.Properties.Appearance.Options.UseFont = true;
		this.BtnRental.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnRental.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnRental.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.BtnRental.Properties.ReadOnly = true;
		this.BtnRental.Size = new System.Drawing.Size(615, 28);
		this.BtnRental.TabIndex = 143;
		this.BtnRental.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnRental_ButtonClick);
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label14.Location = new System.Drawing.Point(226, 126);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(76, 21);
		this.label14.TabIndex = 138;
		this.label14.Text = "عدد الايام";
		this.TxtDayNum.Location = new System.Drawing.Point(6, 124);
		this.TxtDayNum.Name = "TxtDayNum";
		this.TxtDayNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDayNum.Properties.Appearance.Options.UseFont = true;
		this.TxtDayNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDayNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDayNum.Size = new System.Drawing.Size(214, 28);
		this.TxtDayNum.TabIndex = 137;
		this.TxtDate.EditValue = null;
		this.TxtDate.Location = new System.Drawing.Point(6, 56);
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
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDate.Size = new System.Drawing.Size(214, 28);
		this.TxtDate.TabIndex = 135;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label13.Location = new System.Drawing.Point(226, 59);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(53, 21);
		this.label13.TabIndex = 136;
		this.label13.Text = "التاريخ";
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label10.Location = new System.Drawing.Point(627, 58);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(81, 21);
		this.label10.TabIndex = 132;
		this.label10.Text = "رقم الهوية";
		this.TxtSuppIDNum.Location = new System.Drawing.Point(341, 55);
		this.TxtSuppIDNum.Name = "TxtSuppIDNum";
		this.TxtSuppIDNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSuppIDNum.Properties.Appearance.Options.UseFont = true;
		this.TxtSuppIDNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSuppIDNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSuppIDNum.Size = new System.Drawing.Size(280, 28);
		this.TxtSuppIDNum.TabIndex = 131;
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label9.Location = new System.Drawing.Point(1102, 126);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(56, 21);
		this.label9.TabIndex = 130;
		this.label9.Text = "العنوان";
		this.LBlSuppAccount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LBlSuppAccount.AutoSize = true;
		this.LBlSuppAccount.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LBlSuppAccount.Location = new System.Drawing.Point(1182, 29);
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		this.LBlSuppAccount.Size = new System.Drawing.Size(19, 21);
		this.LBlSuppAccount.TabIndex = 128;
		this.LBlSuppAccount.Text = "1";
		this.LBlSuppAccount.Visible = false;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label26.Location = new System.Drawing.Point(627, 92);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(95, 21);
		this.label26.TabIndex = 127;
		this.label26.Text = "مركز التكلفة";
		this.TxtAddress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtAddress.Location = new System.Drawing.Point(728, 124);
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtAddress.Properties.Appearance.Options.UseFont = true;
		this.TxtAddress.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAddress.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAddress.Size = new System.Drawing.Size(353, 28);
		this.TxtAddress.TabIndex = 129;
		this.CBCostCenter.Font = new System.Drawing.Font("Tahoma", 10f);
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Location = new System.Drawing.Point(341, 89);
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.Size = new System.Drawing.Size(280, 29);
		this.CBCostCenter.TabIndex = 8;
		this.LblSup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblSup.AutoSize = true;
		this.LblSup.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblSup.Location = new System.Drawing.Point(1182, 59);
		this.LblSup.Name = "LblSup";
		this.LblSup.Size = new System.Drawing.Size(19, 21);
		this.LblSup.TabIndex = 125;
		this.LblSup.Text = "1";
		this.TxtRentalDate.EditValue = null;
		this.TxtRentalDate.Location = new System.Drawing.Point(6, 90);
		this.TxtRentalDate.Name = "TxtRentalDate";
		this.TxtRentalDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtRentalDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtRentalDate.Properties.Appearance.Options.UseFont = true;
		this.TxtRentalDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtRentalDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtRentalDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtRentalDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtRentalDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtRentalDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtRentalDate.Size = new System.Drawing.Size(214, 28);
		this.TxtRentalDate.TabIndex = 0;
		this.label23.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label23.Location = new System.Drawing.Point(1087, 93);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(93, 21);
		this.label23.TabIndex = 117;
		this.label23.Text = "اسم الكفيل";
		this.TxtParentName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtParentName.Location = new System.Drawing.Point(728, 90);
		this.TxtParentName.Name = "TxtParentName";
		this.TxtParentName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtParentName.Properties.Appearance.Options.UseFont = true;
		this.TxtParentName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtParentName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtParentName.Size = new System.Drawing.Size(353, 28);
		this.TxtParentName.TabIndex = 6;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label22.Location = new System.Drawing.Point(627, 126);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(84, 21);
		this.label22.TabIndex = 115;
		this.label22.Text = "رقم الجوال";
		this.TxtPhone.Location = new System.Drawing.Point(341, 123);
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.Size = new System.Drawing.Size(280, 28);
		this.TxtPhone.TabIndex = 5;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label21.Location = new System.Drawing.Point(226, 93);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(92, 21);
		this.label21.TabIndex = 113;
		this.label21.Text = "تاريخ التأجير";
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label18.Location = new System.Drawing.Point(1087, 59);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(93, 21);
		this.label18.TabIndex = 107;
		this.label18.Text = "اسم العميل";
		this.TxtSuppliers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSuppliers.Location = new System.Drawing.Point(728, 56);
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
		this.TxtSuppliers.Size = new System.Drawing.Size(353, 28);
		this.TxtSuppliers.TabIndex = 3;
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label17.Location = new System.Drawing.Point(1087, 25);
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
		this.TxtCode.Size = new System.Drawing.Size(353, 28);
		this.TxtCode.TabIndex = 50;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.TxtDeposit);
		this.groupBox2.Controls.Add(this.TxtLocation);
		this.groupBox2.Controls.Add(this.label25);
		this.groupBox2.Controls.Add(this.txtdicoutprege);
		this.groupBox2.Controls.Add(this.TxtTafqit);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtNet);
		this.groupBox2.Controls.Add(this.TxtNote);
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.TxtVat);
		this.groupBox2.Controls.Add(this.TxtAllDiscount);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.TxtTotals);
		this.groupBox2.Controls.Add(this.TxtTotalDiscount);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.TxtDiscount);
		this.groupBox2.Controls.Add(this.TxtVatDisc);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Location = new System.Drawing.Point(12, 457);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1208, 219);
		this.groupBox2.TabIndex = 9;
		this.groupBox2.TabStop = false;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label12.Location = new System.Drawing.Point(542, 76);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(146, 19);
		this.label12.TabIndex = 211;
		this.label12.Text = "المبلغ المدفوع تأمين";
		this.TxtDeposit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDeposit.DecimalDigits = 2;
		this.TxtDeposit.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtDeposit.ForeColor = System.Drawing.Color.Blue;
		this.TxtDeposit.Location = new System.Drawing.Point(507, 98);
		this.TxtDeposit.Name = "TxtDeposit";
		this.TxtDeposit.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDeposit.Size = new System.Drawing.Size(221, 48);
		this.TxtDeposit.TabIndex = 210;
		this.TxtDeposit.Text = "0.00";
		this.TxtDeposit.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDeposit.UseCompatibleTextRendering = false;
		this.TxtDeposit.Value = new decimal(new int[4]);
		this.TxtDeposit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtLocation.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtLocation.EditValue = "";
		this.TxtLocation.Location = new System.Drawing.Point(507, 185);
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
		this.label25.Location = new System.Drawing.Point(734, 189);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(51, 19);
		this.label25.TabIndex = 191;
		this.label25.Text = "الموقع";
		this.txtdicoutprege.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Black;
		this.txtdicoutprege.Location = new System.Drawing.Point(6, 81);
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtdicoutprege.Size = new System.Drawing.Size(107, 27);
		this.txtdicoutprege.TabIndex = 189;
		this.txtdicoutprege.Text = "0.000";
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTafqit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtTafqit.Location = new System.Drawing.Point(6, 185);
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
		this.TxtTafqit.Size = new System.Drawing.Size(495, 28);
		this.TxtTafqit.TabIndex = 93;
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label11.Location = new System.Drawing.Point(734, 155);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(73, 21);
		this.label11.TabIndex = 134;
		this.label11.Text = "ملاحظات";
		this.TxtNet.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Location = new System.Drawing.Point(811, 63);
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNet.Size = new System.Drawing.Size(392, 48);
		this.TxtNet.TabIndex = 92;
		this.TxtNet.Text = "0.000";
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNote.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNote.Location = new System.Drawing.Point(6, 152);
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNote.Size = new System.Drawing.Size(722, 28);
		this.TxtNote.TabIndex = 133;
		this.BtnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(811, 121);
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
		this.BtnSave.Location = new System.Drawing.Point(944, 121);
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
		this.BtnPrint.Location = new System.Drawing.Point(944, 169);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(125, 40);
		this.BtnPrint.TabIndex = 6;
		this.BtnPrint.Text = "طباعة Ctrl P";
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(1077, 121);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(125, 40);
		this.BtnNew.TabIndex = 7;
		this.BtnNew.Text = "جديد F4";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label8.Location = new System.Drawing.Point(349, 121);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(109, 19);
		this.label8.TabIndex = 171;
		this.label8.Text = "الضريبة بالخصم";
		this.BtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(1077, 169);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(125, 40);
		this.BtnDelete.TabIndex = 5;
		this.BtnDelete.Text = "حذف F7";
		this.TxtVat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtVat.DecimalDigits = 3;
		this.TxtVat.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtVat.ForeColor = System.Drawing.Color.Black;
		this.TxtVat.Location = new System.Drawing.Point(236, 84);
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.ReadOnly = true;
		this.TxtVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVat.Size = new System.Drawing.Size(107, 27);
		this.TxtVat.TabIndex = 170;
		this.TxtVat.Text = "0.000";
		this.TxtVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4]);
		this.TxtVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtAllDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtAllDiscount.DecimalDigits = 3;
		this.TxtAllDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtAllDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtAllDiscount.Location = new System.Drawing.Point(236, 53);
		this.TxtAllDiscount.Name = "TxtAllDiscount";
		this.TxtAllDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtAllDiscount.Size = new System.Drawing.Size(107, 27);
		this.TxtAllDiscount.TabIndex = 169;
		this.TxtAllDiscount.Text = "0.000";
		this.TxtAllDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllDiscount.Value = new decimal(new int[4]);
		this.TxtAllDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(811, 169);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(125, 40);
		this.BtnExit.TabIndex = 8;
		this.BtnExit.Text = "خروج ";
		this.TxtTotals.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Location = new System.Drawing.Point(6, 13);
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.ReadOnly = true;
		this.TxtTotals.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotals.Size = new System.Drawing.Size(107, 27);
		this.TxtTotals.TabIndex = 161;
		this.TxtTotals.Text = "0.000";
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTotalDiscount.DecimalDigits = 3;
		this.TxtTotalDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotalDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalDiscount.Location = new System.Drawing.Point(6, 115);
		this.TxtTotalDiscount.Name = "TxtTotalDiscount";
		this.TxtTotalDiscount.ReadOnly = true;
		this.TxtTotalDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotalDiscount.Size = new System.Drawing.Size(107, 27);
		this.TxtTotalDiscount.TabIndex = 159;
		this.TxtTotalDiscount.Text = "0.000";
		this.TxtTotalDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalDiscount.Value = new decimal(new int[4]);
		this.TxtTotalDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label1.Location = new System.Drawing.Point(119, 87);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(93, 19);
		this.label1.TabIndex = 163;
		this.label1.Text = "نسبة الخصم";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label2.Location = new System.Drawing.Point(119, 19);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(70, 19);
		this.label2.TabIndex = 164;
		this.label2.Text = "الاجمالي";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label3.Location = new System.Drawing.Point(119, 53);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(89, 19);
		this.label3.TabIndex = 165;
		this.label3.Text = "قيمة الخصم";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label4.Location = new System.Drawing.Point(119, 121);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(58, 19);
		this.label4.TabIndex = 166;
		this.label4.Text = "الصافي";
		this.TxtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscount.Location = new System.Drawing.Point(6, 48);
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDiscount.Size = new System.Drawing.Size(107, 27);
		this.TxtDiscount.TabIndex = 160;
		this.TxtDiscount.Text = "0.000";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtVatDisc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtVatDisc.DecimalDigits = 3;
		this.TxtVatDisc.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtVatDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatDisc.Location = new System.Drawing.Point(236, 115);
		this.TxtVatDisc.Name = "TxtVatDisc";
		this.TxtVatDisc.ReadOnly = true;
		this.TxtVatDisc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVatDisc.Size = new System.Drawing.Size(107, 27);
		this.TxtVatDisc.TabIndex = 162;
		this.TxtVatDisc.Text = "0.000";
		this.TxtVatDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatDisc.Value = new decimal(new int[4]);
		this.TxtVatDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label5.Location = new System.Drawing.Point(349, 89);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(92, 19);
		this.label5.TabIndex = 167;
		this.label5.Text = "الضريبة VAT";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label6.Location = new System.Drawing.Point(349, 58);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(118, 19);
		this.label6.TabIndex = 168;
		this.label6.Text = "الخصم الاجمالي";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1232, 750);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.gridControl1);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmOrderReturnRental";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		this.Text = "مرتجعات فواتر الايجارات";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmOrderReturnRental_FormClosing);
		base.Load += new System.EventHandler(FrmOrderReturnRental_Load);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnPlus).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnRental.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDayNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppIDNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtParentName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


