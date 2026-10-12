using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.OrderRent;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.OrderRent.Search;
using GTSErpSystem.Frms.Orders.FrmCachMoney;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using Humanizer;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.OrderRent;

public class FrmRecipt3 : XtraForm
{
	private Calss_CostCenter Cost = new Calss_CostCenter();

	private Class_Orders ORD = new Class_Orders();

	private Class_Customer Cust = new Class_Customer();

	private Class_Recipt Recipt = new Class_Recipt();

	private Class_Contract Contract = new Class_Contract();

	private Class_Rent Rent = new Class_Rent();

	private DataTable datatable = new DataTable();

	private GTSdbEntities db = new GTSdbEntities();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_Items ITM = new Class_Items();

	private string fileNameAR = Application.StartupPath + "\\Layout\\FrmRecipt3.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\FrmRecipt3.xml";

	public decimal FrmCash;

	public decimal FrmBank;

	public decimal Paid;

	public decimal Rest;

	public string words;

	private int SN = 1;

	private int ItmID = 0;

	private int StorId = 0;

	private string UnitName = string.Empty;

	public string CodeItm;

	private int DecimalNum = LoginDetails.DecimalNum;

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private SqlConnection conn = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label13;

	private ButtonEdit TxtCodeRent;

	private DateEdit TxtRentalDate;

	private Label label24;

	private Label label11;

	private TextEdit TxtNote;

	private Label label10;

	private TextEdit TxtSuppIDNum;

	private Label label9;

	private TextEdit TxtAddress;

	private Label LBlSuppAccount;

	private Label label26;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private Label LblSup;

	private DateEdit TxtDate;

	private Label label23;

	private TextEdit TxtParentName;

	private Label label22;

	private TextEdit TxtPhone;

	private Label label21;

	private Label label18;

	private ButtonEdit TxtSuppliers;

	private Label label17;

	private ButtonEdit TxtCode;

	private DateEdit TxtDateReceipt;

	private Label label1;

	private Label label2;

	private TextEdit TxtDayNum;

	private Label label37;

	private TextEdit TxtCharge;

	private GroupBox groupBox3;

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

	private GroupBox groupBox2;

	private Label label12;

	private NumericEditBox TxtDeposit;

	private TextEdit TxtLocation;

	private Label label25;

	private NumericEditBox txtdicoutprege;

	private TextEdit TxtTafqit;

	private NumericEditBox TxtNet;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private NumericEditBox TxtVat;

	private NumericEditBox TxtAllDiscount;

	private SimpleButton BtnExit;

	private NumericEditBox TxtTotals;

	private NumericEditBox TxtTotalDiscount;

	private NumericEditBox TxtDiscount;

	private NumericEditBox TxtVatDisc;

	private Label label16;

	private NumericEditBox TxtResidual;

	private Label label19;

	private NumericEditBox TxtRecipient;

	private Label label3;

	private NumericEditBox TxtFine;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private Label label20;

	private System.Windows.Forms.ComboBox ComCalcDays;

	private GridColumn gridColumn38;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private GroupBox groupBox5;

	private NumericEditBox TxtOrderRest;

	private NumericEditBox TxtOrderCashMoney;

	private Label label4;

	private NumericEditBox TxtOrderBank;

	private Label label5;

	private Label label6;

	private Label label7;

	private NumericEditBox TxtOrderTotal;

	public FrmRecipt3()
	{
		InitializeComponent();
		SetInitialRow();
		Bind();
		BtnDefault();
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "ItemCode"));
		decimal num = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "Quantity"));
		if (text == string.Empty || num <= 0m)
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
			if (e.Column.FieldName == "QuantityDev" || e.Column.FieldName == "DayNum")
			{
				UpdateItemRow(e.Column.FieldName, e);
			}
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

	private void TxtSuppliers_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			if (TxtSuppliers.ReadOnly)
			{
				return;
			}
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustCode > 0)
			{
				int FrmID = frmSearchCustomer.CustCode;
				Account_CustSup account_CustSup = (from x in Cust.GetAll()
					where x.ID == FrmID && x.IsCustomers == true
					select x).FirstOrDefault();
				LblSup.Text = FrmID.ToString();
				TxtSuppliers.Text = account_CustSup.CustSuppName;
				TxtPhone.Text = account_CustSup.Phone;
				TxtAddress.Text = account_CustSup.Address;
				TxtSuppIDNum.Text = account_CustSup.VatNum;
				Charge(FrmID);
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		NewFrm();
		SN = 1;
	}

	private void TxtCodeRent_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchRentalInvoice frmSearchRentalInvoice = new FrmSearchRentalInvoice();
			frmSearchRentalInvoice.ShowDialog();
			if (Recipt.ISReturn(frmSearchRentalInvoice.CustCode))
			{
				GetData2(frmSearchRentalInvoice.CustCode);
			}
			else
			{
				BindFromRent(frmSearchRentalInvoice.CustCode);
			}
			BtnSave.Enabled = true;
		}
		catch (Exception)
		{
			Clear();
		}
		Tafqit();
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchRecipt frmSearchRecipt = new FrmSearchRecipt();
		frmSearchRecipt.ShowDialog();
		if (frmSearchRecipt.CustCode > 0)
		{
			int custCode = frmSearchRecipt.CustCode;
			OrderRent_Recipt3 orderRent_Recipt = Recipt.GetPurchasesRecipt3(custCode).FirstOrDefault();
			CBCostCenter.SelectedValue = orderRent_Recipt.CostCentersID;
			TxtCode.Text = orderRent_Recipt.PurBranchID.ToString();
			LblSup.Text = orderRent_Recipt.SupplierID.ToString();
			TxtSuppliers.Text = orderRent_Recipt.SupplierName;
			TxtRentalDate.DateTime = Convert.ToDateTime(orderRent_Recipt.RentalDate);
			TxtDateReceipt.DateTime = Convert.ToDateTime(orderRent_Recipt.DateReceipt);
			TxtDateReceipt.DateTime = Convert.ToDateTime(orderRent_Recipt.Date);
			TxtParentName.Text = orderRent_Recipt.ParentName;
			TxtPhone.Text = orderRent_Recipt.SupplierPhone;
			TxtNote.Text = orderRent_Recipt.Note;
			TxtDeposit.Text = orderRent_Recipt.deposit.ToString();
			TxtFine.Text = orderRent_Recipt.Fine.ToString();
			TxtRecipient.Text = orderRent_Recipt.Recipient.ToString();
			TxtResidual.Text = orderRent_Recipt.depositRest.ToString();
			TxtAddress.Text = orderRent_Recipt.Address;
			TxtSuppIDNum.Text = orderRent_Recipt.SupplierIDNum;
			TxtDayNum.Text = Convert.ToInt32(orderRent_Recipt.DaysNum).ToString();
			TxtOrderTotal.Text = orderRent_Recipt.Net.ToString();
			TxtOrderCashMoney.Text = orderRent_Recipt.CashMoney.ToString();
			TxtOrderBank.Text = orderRent_Recipt.CashBank.ToString();
			TxtOrderRest.Text = orderRent_Recipt.rest.ToString();
			Charge(Convert.ToInt32(LblSup.Text));
			TxtSuppliers.Focus();
			datatable.Rows.Clear();
			List<OrderRent_RecpitDetails3> purchasesRecipt3Details = Recipt.GetPurchasesRecipt3Details(custCode);
			int num = 1;
			foreach (OrderRent_RecpitDetails3 item in purchasesRecipt3Details)
			{
				DataRow dataRow = null;
				int itemID = Convert.ToInt32(item.ItemID);
				Item_Items item_Items = ORD.GetItemsByitemid(itemID).FirstOrDefault();
				dataRow = datatable.NewRow();
				dataRow["ItemID"] = Convert.ToInt32(item_Items.ItemId);
				dataRow["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
				dataRow["ItemNote"] = item.ItemNote;
				dataRow["DayNum"] = FUNC_DecimalNum(Convert.ToDecimal(item.DayNum));
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
				dataRow["Quantity"] = FUNC_DecimalNum(Convert.ToDecimal(item.Quantity));
				dataRow["QuantityDev"] = FUNC_DecimalNum(Convert.ToDecimal(item.Quantity));
				dataRow["AvergItem"] = item.LastCost;
				dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
				dataRow["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.UnitPrice));
				dataRow["UnitPriceDisc"] = Math.Round(Convert.ToDecimal(item.NetUnitPrice), DecimalNum);
				dataRow["PercentDisc"] = Math.Round(Convert.ToDecimal(item.PercentDisc), DecimalNum);
				dataRow["AmountDisc"] = Math.Round(Convert.ToDecimal(item.AmountDisc), DecimalNum);
				dataRow["SumPrice"] = Math.Round(Convert.ToDecimal(item.SumPrice), DecimalNum);
				dataRow["Total"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
				dataRow["SumPriceDisc"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
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
			decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
			TxtNet.Text = num2.ToString();
			TxtAddress.ReadOnly = true;
			TxtSuppIDNum.ReadOnly = true;
			TxtParentName.ReadOnly = true;
			TxtSuppliers.ReadOnly = true;
			TxtPhone.ReadOnly = true;
			TxtNote.ReadOnly = true;
			txtdicoutprege.ReadOnly = true;
			CBCostCenter.Enabled = false;
			BtnDelete.Enabled = true;
			BtnSave.Enabled = false;
			TxtNote.BackColor = Color.White;
			TxtSuppliers.BackColor = Color.White;
			TxtAddress.BackColor = Color.White;
			TxtParentName.BackColor = Color.White;
			TxtSuppIDNum.BackColor = Color.White;
			TxtPhone.BackColor = Color.White;
			ArrangeDatatable();
		}
		else
		{
			TxtSuppliers.Focus();
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void BtnDefault()
	{
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		BtnSave.Enabled = false;
		if (LoginDetails.FrmOrderClose)
		{
			BtnNew.Visible = false;
			BtnSave.Visible = false;
			BtnEdit.Visible = false;
			BtnDelete.Visible = false;
			BtnExit.Visible = false;
			BtnPrint.Visible = false;
		}
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtDate.DateTime = DateTime.Now;
		TxtRentalDate.DateTime = DateTime.Now;
		TxtDateReceipt.DateTime = DateTime.Now;
		TxtCharge.BackColor = Color.White;
		TxtDate.ReadOnly = true;
		TxtRentalDate.ReadOnly = true;
		TxtDateReceipt.ReadOnly = true;
		TxtDate.BackColor = Color.White;
		TxtDayNum.Enabled = false;
		TxtDayNum.BackColor = Color.White;
		TxtDayNum.ForeColor = Color.Black;
		TxtRentalDate.BackColor = Color.White;
		TxtRentalDate.ForeColor = Color.Black;
		TxtDateReceipt.BackColor = Color.White;
		TxtDateReceipt.ForeColor = Color.Black;
		TxtCode.Focus();
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
		datatable.Columns.Add("QuantityDev", typeof(decimal));
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
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
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

	private void Clear()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		datatable.Rows.Clear();
		TxtCode.Text = "";
		TxtCodeRent.Text = "";
		gridControl1.DataSource = datatable;
		TxtSuppliers.Text = "";
		TxtAddress.Text = "";
		TxtSuppIDNum.Text = "";
		TxtParentName.Text = "";
		TxtDiscount.Text = "";
		TxtVat.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtOrderTotal.Text = "0.00";
		TxtOrderCashMoney.Text = "0.00";
		TxtOrderBank.Text = "0.00";
		TxtOrderRest.Text = "0.00";
		TxtFine.Text = "0.00";
		TxtRecipient.Text = "0.00";
		TxtResidual.Text = "0.00";
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
		TxtDate.DateTime = DateTime.Now;
		TxtDateReceipt.DateTime = DateTime.Now;
		TxtRentalDate.DateTime = DateTime.Now;
		TxtDayNum.Text = "0";
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
		TxtDate.Enabled = false;
		ComCalcDays.SelectedIndex = 0;
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
			decimal num = Convert.ToDecimal(TxtNet.Text);
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

	private void NewFrm()
	{
		datatable.Rows.Clear();
		gridControl1.Enabled = true;
		gridView1.OptionsBehavior.Editable = true;
		Clear();
		BtnSave.Enabled = false;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		InsertNewRow();
		TxtCode.Text = string.Empty;
		IsEnableTrue();
		LblSup.Text = LoginDetails.Acc_CustomerCash.ToString();
		TxtSuppliers.Focus();
		BtnDefault();
	}

	private void BindFromRent(int Code)
	{
		if (Code > 0)
		{
			Orders_RentalInvoice orders_RentalInvoice = Rent.GetPurchasesRental(Code).FirstOrDefault();
			CBCostCenter.SelectedValue = orders_RentalInvoice.CostCentersID;
			TxtCodeRent.Text = orders_RentalInvoice.PurBranchID.ToString();
			LblSup.Text = orders_RentalInvoice.SupplierID.ToString();
			TxtSuppliers.Text = orders_RentalInvoice.SupplierName;
			TxtRentalDate.DateTime = Convert.ToDateTime(orders_RentalInvoice.RentalDate);
			TxtDateReceipt.DateTime = Convert.ToDateTime(orders_RentalInvoice.ReturnDate);
			TxtParentName.Text = orders_RentalInvoice.ParentName;
			TxtPhone.Text = orders_RentalInvoice.SupplierPhone;
			TxtNote.Text = orders_RentalInvoice.Note;
			TxtDeposit.Text = orders_RentalInvoice.deposit.ToString();
			TxtAddress.Text = orders_RentalInvoice.Address;
			TxtSuppIDNum.Text = orders_RentalInvoice.SupplierIDNum;
			TxtDayNum.Text = Convert.ToInt32(orders_RentalInvoice.DayNums).ToString();
			TxtOrderTotal.Text = orders_RentalInvoice.Net.ToString();
			TxtOrderCashMoney.Text = orders_RentalInvoice.CashMoney.ToString();
			TxtOrderBank.Text = orders_RentalInvoice.CashBank.ToString();
			TxtOrderRest.Text = orders_RentalInvoice.rest.ToString();
			TxtNet.Text = orders_RentalInvoice.rest.ToString();
			Charge(Convert.ToInt32(LblSup.Text));
			TxtSuppliers.Focus();
			datatable.Rows.Clear();
			List<Ordert_RentalInvoiceDetails> purchasesRentalDetails = Rent.GetPurchasesRentalDetails(Code);
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
				dataRow["Quantity"] = FUNC_DecimalNum(Convert.ToDecimal(item.Quantity));
				dataRow["QuantityDev"] = FUNC_DecimalNum(Convert.ToDecimal(item.Quantity));
				dataRow["AvergItem"] = item.LastCost;
				dataRow["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow["AvergItem"]) * Convert.ToDecimal(dataRow["Quantity"]), DecimalNum);
				dataRow["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.UnitPrice));
				dataRow["UnitPriceDisc"] = Math.Round(Convert.ToDecimal(item.NetUnitPrice), DecimalNum);
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
			TxtAddress.ReadOnly = true;
			TxtSuppIDNum.ReadOnly = true;
			TxtParentName.ReadOnly = true;
			TxtSuppliers.ReadOnly = true;
			TxtPhone.ReadOnly = true;
			TxtNote.ReadOnly = true;
			txtdicoutprege.ReadOnly = true;
			CBCostCenter.Enabled = false;
			TxtNote.BackColor = Color.White;
			TxtSuppliers.BackColor = Color.White;
			TxtAddress.BackColor = Color.White;
			TxtParentName.BackColor = Color.White;
			TxtSuppIDNum.BackColor = Color.White;
			TxtPhone.BackColor = Color.White;
			ArrangeDatatable();
		}
		else
		{
			TxtSuppliers.Focus();
		}
	}

	public void GetData2(int ID)
	{
		try
		{
			conn.Open();
			using (SqlCommand sqlCommand = new SqlCommand("GetOrderRentalDetails", conn))
			{
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.AddWithValue("@ID", ID);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				DataSet dataSet = new DataSet();
				sqlDataAdapter.Fill(dataSet);
				if (dataSet.Tables.Count > 0)
				{
					DataTable dataTable = dataSet.Tables[0];
					if (dataTable.Rows.Count > 0)
					{
						DataRow dataRow = dataTable.Rows[0];
						TxtCodeRent.Text = ID.ToString() ?? "";
						LblSup.Text = dataRow["SupplierID"].ToString() ?? "";
						TxtSuppliers.Text = dataRow["SupplierName"].ToString() ?? "";
						TxtPhone.Text = dataRow["SupplierPhone"].ToString() ?? "";
						CBCostCenter.SelectedValue = dataRow["CostCentersID"].ToString() ?? "";
						TxtRentalDate.DateTime = Convert.ToDateTime(dataRow["RentalDate"]);
						TxtDateReceipt.DateTime = Convert.ToDateTime(dataRow["ReturnDate"]);
						TxtParentName.Text = dataRow["ParentName"].ToString() ?? "";
						TxtNote.Text = dataRow["ParentName"].ToString() ?? "";
						TxtDeposit.Text = dataRow["deposit"].ToString() ?? "";
						TxtAddress.Text = dataRow["Address"].ToString() ?? "";
						TxtSuppIDNum.Text = dataRow["SupplierIDNum"].ToString() ?? "";
						TxtDayNum.Text = dataRow["DayNums"].ToString() ?? "";
						TxtOrderTotal.Text = dataRow["Net"].ToString() ?? "";
						TxtOrderCashMoney.Text = dataRow["CashMoney"].ToString() ?? "";
						TxtOrderBank.Text = dataRow["CashBank"].ToString() ?? "";
						TxtOrderRest.Text = dataRow["rest"].ToString() ?? "";
						TxtNet.Text = dataRow["rest"].ToString() ?? "";
						TxtNote.Text = dataRow["Note"].ToString() ?? "";
						Charge(Convert.ToInt32(LblSup.Text));
					}
				}
			}
			conn.Close();
			int purchasesID = ID;
			List<Ordert_RentalInvoiceDetails> purchasesRentalDetails = Rent.GetPurchasesRentalDetails(purchasesID);
			int num = 1;
			foreach (Ordert_RentalInvoiceDetails item in purchasesRentalDetails)
			{
				DataRow dataRow2 = null;
				int itemID = Convert.ToInt32(item.ItemID);
				Item_Items item_Items = ORD.GetItemsByitemid(itemID).FirstOrDefault();
				dataRow2 = datatable.NewRow();
				dataRow2["ItemID"] = Convert.ToInt32(item_Items.ItemId);
				dataRow2["ItemUnitID"] = Convert.ToInt32(item_Items.UnitSmall);
				dataRow2["ItemNote"] = item.ItemNote;
				dataRow2["DayNum"] = FUNC_DecimalNum(Convert.ToDecimal(item.DaysNum));
				dataRow2["ItemCode"] = item_Items.Item_code;
				dataRow2["ItemName"] = item_Items.item_Name;
				if (item.ItemUnitType == "الصغرى")
				{
					dataRow2["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
				}
				if (item.ItemUnitType == "المتوسطة")
				{
					dataRow2["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
				}
				if (item.ItemUnitType == "الكبرى")
				{
					dataRow2["UnitName"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
				}
				dataRow2["UnitType"] = item.ItemUnitType;
				dataRow2["StoreID"] = item.StoreID;
				dataRow2["StoreName"] = (from x in db.Account_Stores
					where (int?)x.ID == item.StoreID
					select x.Store_Name).FirstOrDefault();
				dataRow2["Quantity"] = FUNC_DecimalNum(Convert.ToDecimal(item.Quantity));
				dataRow2["QuantityDev"] = FUNC_DecimalNum(Convert.ToDecimal(item.Quantity));
				dataRow2["AvergItem"] = item.LastCost;
				dataRow2["SumAvergItem"] = Math.Round(Convert.ToDecimal(dataRow2["AvergItem"]) * Convert.ToDecimal(dataRow2["Quantity"]), DecimalNum);
				dataRow2["UnitPrice"] = FUNC_DecimalNum(Convert.ToDecimal(item.UnitPrice));
				dataRow2["UnitPriceDisc"] = Math.Round(Convert.ToDecimal(item.NetUnitPrice), DecimalNum);
				dataRow2["PercentDisc"] = Math.Round(Convert.ToDecimal(item.PercentDisc), DecimalNum);
				dataRow2["AmountDisc"] = Math.Round(Convert.ToDecimal(item.AmountDisc), DecimalNum);
				dataRow2["SumPrice"] = Math.Round(Convert.ToDecimal(item.SumPrice), DecimalNum);
				dataRow2["Total"] = Math.Round(Convert.ToDecimal(item.Total), DecimalNum);
				dataRow2["SumPriceDisc"] = Math.Round(Convert.ToDecimal(item.SumPriceDisc), DecimalNum);
				decimal? vAT = item.VAT;
				if ((vAT.GetValueOrDefault() > 0m) & vAT.HasValue)
				{
					dataRow2["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
					dataRow2["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
					dataRow2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice));
					dataRow2["IsItemTax"] = 1;
					dataRow2["Tax_Value"] = item_Items.Tax_Value;
				}
				else
				{
					dataRow2["IsItemTax"] = 0;
					dataRow2["Tax_Value"] = 0;
					dataRow2["ItemVat"] = Math.Round(Convert.ToDecimal(item.VAT), DecimalNum);
					dataRow2["ItemVatDisc"] = Math.Round(Convert.ToDecimal(item.VAT_Discount), DecimalNum);
					dataRow2["Net"] = FUNC_DecimalNum(Convert.ToDecimal(item.NetTotalPrice));
				}
				datatable.Rows.Add(dataRow2);
				num++;
			}
			CheckFfistRow();
			gridControl1.DataSource = datatable;
			TxtAddress.ReadOnly = true;
			TxtSuppIDNum.ReadOnly = true;
			TxtParentName.ReadOnly = true;
			TxtSuppliers.ReadOnly = true;
			TxtPhone.ReadOnly = true;
			TxtNote.ReadOnly = true;
			txtdicoutprege.ReadOnly = true;
			CBCostCenter.Enabled = false;
			TxtNote.BackColor = Color.White;
			TxtSuppliers.BackColor = Color.White;
			TxtAddress.BackColor = Color.White;
			TxtParentName.BackColor = Color.White;
			TxtSuppIDNum.BackColor = Color.White;
			TxtPhone.BackColor = Color.White;
			ArrangeDatatable();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message);
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
				decimal quantity = ((TxtChange == "QuantityDev") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["QuantityDev"])));
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
				decimal daysNum = ((TxtChange == "DayNum") ? num : FUNC_DecimalNum(Convert.ToDecimal(row["DayNum"])));
				UpdateRowFactionAccount(quantity, unitPrice, flag2, flag, num2, amountDisc, avergItem, daysNum);
			}
		}
	}

	public void UpdateRowFactionAccount(decimal Quantity, decimal UnitPrice, bool IsDisc, bool IsDiscAmount, decimal PercentDisc, decimal AmountDisc, decimal AvergItem, decimal DaysNum)
	{
		decimal num = Quantity * DaysNum;
		decimal value = UnitPrice * num;
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToInt32(row["SN"]) != SN)
			{
				continue;
			}
			bool flag = row["IsItemTax"] != DBNull.Value && Convert.ToBoolean(row["IsItemTax"]);
			row["QuantityDev"] = Quantity;
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
					decimal num2 = Convert.ToDecimal(LoginDetails.PerVat) / 100m;
					row["Total"] = FUNC_DecimalNum(Convert.ToDecimal(row["OrginalPrice"]) * num);
					row["SumPrice"] = FUNC_DecimalNum(Convert.ToDecimal(row["Total"]) / (num2 + 1m));
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
					row["ItemVatDisc"] = FUNC_DecimalNum(Convert.ToDecimal(row["SumPriceDisc"]) * num2);
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
			row["SumAvergItem"] = FUNC_DecimalNum(Convert.ToDecimal(row["AvergItem"]) * num);
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
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["QuantityDev"]) * Convert.ToDecimal(row["DayNum"]));
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
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["QuantityDev"]) * Convert.ToDecimal(row["DayNum"]));
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
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["QuantityDev"]) * Convert.ToDecimal(row["DayNum"]));
			if (Convert.ToInt32(row["ItemID"]) > 0)
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
		TxtDiscount.Text = "0.00";
		txtdicoutprege.Text = "0.00";
		TxtAllDiscount.Text = "0.00";
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
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["QuantityDev"]) * Convert.ToDecimal(row["DayNum"]));
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
			decimal num3 = FUNC_DecimalNum(Convert.ToDecimal(row2["QuantityDev"]) * Convert.ToDecimal(row2["DayNum"]));
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
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["QuantityDev"]) * Convert.ToDecimal(row["DayNum"]));
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
			decimal num4 = FUNC_DecimalNum(Convert.ToDecimal(row2["QuantityDev"]) * Convert.ToDecimal(row2["DayNum"]));
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
			decimal num = FUNC_DecimalNum(Convert.ToDecimal(row["QuantityDev"]) * Convert.ToDecimal(row["DayNum"]));
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
			decimal num4 = FUNC_DecimalNum(Convert.ToDecimal(row2["QuantityDev"]) * Convert.ToDecimal(row2["DayNum"]));
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

	public void IsEnableTrue()
	{
		TxtDiscount.ReadOnly = false;
		TxtRentalDate.ReadOnly = false;
		TxtDayNum.ReadOnly = false;
		TxtDateReceipt.ReadOnly = false;
		TxtAddress.ReadOnly = false;
		TxtSuppIDNum.ReadOnly = false;
		TxtParentName.ReadOnly = false;
		TxtSuppliers.ReadOnly = false;
		TxtPhone.ReadOnly = false;
		TxtNote.ReadOnly = false;
		TxtDiscount.ReadOnly = false;
		TxtAllDiscount.ReadOnly = false;
		txtdicoutprege.ReadOnly = false;
		CBCostCenter.Enabled = true;
		TxtDate.BackColor = Color.White;
		TxtDate.ForeColor = Color.Black;
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		TxtDiscount.ReadOnly = true;
		TxtRentalDate.ReadOnly = true;
		TxtDateReceipt.ReadOnly = true;
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
		TxtDateReceipt.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
		TxtNote.BackColor = Color.White;
		TxtSuppliers.BackColor = Color.White;
		TxtAddress.BackColor = Color.White;
		TxtParentName.BackColor = Color.White;
		TxtSuppIDNum.BackColor = Color.White;
		TxtPhone.BackColor = Color.White;
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

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 111))
		{
			int num = 0;
			if (datatable.Rows.Count >= 1)
			{
				if (TxtSuppliers.Text != "")
				{
					decimal num2 = IsItemPriceZero();
					if (num2 == 0m)
					{
						return;
					}
					LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
					LoginDetails.TotalFatorPrice = Convert.ToDecimal(TxtNet.Text);
					FrmMoney frmMoney = new FrmMoney();
					frmMoney.ShowDialog();
					if (!frmMoney.IsComplateMony)
					{
						return;
					}
					FrmCash = Convert.ToDecimal(frmMoney.TxtCash.Text);
					FrmBank = Convert.ToDecimal(frmMoney.TxtVisa.Text);
					if (CBCostCenter.SelectedValue == null)
					{
						CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
					}
					num = Recipt.InsertRecipt(out var _, Convert.ToInt32(TxtCodeRent.Text), Convert.ToInt32(LblSup.Text), TxtSuppliers.Text, TxtPhone.Text, TxtSuppIDNum.Text, TxtRentalDate.DateTime, TxtDateReceipt.DateTime, TxtDate.DateTime, TxtParentName.Text, Convert.ToInt32(CBCostCenter.SelectedValue), TxtNote.Text, TxtAddress.Text, Convert.ToDecimal(TxtDayNum.Text), Convert.ToInt32(ComCalcDays.SelectedValue), Math.Round(Convert.ToDecimal(TxtTotals.Text), DecimalNum), Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtAllDiscount.Text), Convert.ToDecimal(TxtDiscount.Text), Convert.ToDecimal(txtdicoutprege.Text), Convert.ToDecimal(TxtVatDisc.Text), Math.Round(Convert.ToDecimal(TxtTotalDiscount.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtNet.Text), DecimalNum), 2, FrmCash, FrmBank, Paid, Rest, Math.Round(Convert.ToDecimal(TxtDeposit.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtFine.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtRecipient.Text), DecimalNum), Math.Round(Convert.ToDecimal(TxtResidual.Text), DecimalNum));
					Recipt.InsertItemsRecipt(datatable, num);
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
						Charge(Convert.ToInt32(LblSup.Text));
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات فاتورة الاستلام رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else
				{
					MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة الاستلام بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 111))
		{
			int num = 0;
			if (TxtCode.Text != "")
			{
				int num2 = Convert.ToInt32(TxtCode.Text);
				switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة الاستلام رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = Recipt.DeleteRecpit(Convert.ToInt32(TxtCode.Text));
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات فاتورة الاستلام رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						gridView1.OptionsBehavior.Editable = true;
						IsEnableTrue();
						TxtSuppliers.Focus();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات فاتورة الاستلام رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					break;
				default:
					MessageBox.Show("عفوا\u064b لا يمكن التعديل على فاتورة المبيعات ليها حركات مرتجعات او صرف ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					break;
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
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 111))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Recipt3", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
				sqlConnection.Close();
				if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintRentRecipt2.mrt");
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
					stiReport["Tafqit"] = words;
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

	private void FrmRecipt3_Load(object sender, EventArgs e)
	{
		try
		{
			ComCalcDays.Items.Add("حساب على تاريخ التسليم");
			ComCalcDays.Items.Add("حساب على تاريخ اليوم");
			ComCalcDays.SelectedIndex = 0;
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

	private void FrmRecipt3_FormClosing(object sender, FormClosingEventArgs e)
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

	private void ComCalcDays_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (ComCalcDays.SelectedIndex == 0)
		{
			try
			{
				if (TxtCodeRent.Text == "")
				{
					return;
				}
				BindFromRent(Convert.ToInt32(TxtCodeRent.Text));
			}
			catch (Exception)
			{
				Clear();
			}
			Tafqit();
			return;
		}
		TxtDayNum.Text = "0";
		DateTime dateTime = Convert.ToDateTime(TxtRentalDate.Text);
		DateTime dateTime2 = Convert.ToDateTime(TxtDate.Text);
		int num = (int)Math.Ceiling((dateTime2 - dateTime).TotalDays);
		TxtDayNum.Text = num.ToString();
		foreach (DataRow row in datatable.Rows)
		{
			row["DayNum"] = num;
		}
		int rowHandle = 0;
		GridColumn column = gridView1.Columns["DayNum"];
		CellValueChangedEventArgs e2 = new CellValueChangedEventArgs(rowHandle, column, num);
		UpdateItemRow("DayNum", e2);
		gridControl1.DataSource = datatable;
		Cal_FinalResultTotals(Convert.ToDecimal(TxtDiscount.Text), "DiscAmount");
	}

	private decimal FUNC_DecimalNum(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNum);
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
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void repositoryItemButtonEdit4_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int itemID = 0;
		int storeID = 0;
		foreach (DataRow row in datatable.Rows)
		{
			if (Convert.ToInt32(row["SN"]) == SN)
			{
				itemID = Convert.ToInt32(row["ItemID"]);
				storeID = Convert.ToInt32(row["StoreID"]);
			}
		}
		FrmItemRentDeatils frmItemRentDeatils = new FrmItemRentDeatils(itemID, LoginDetails.BranchID, storeID);
		frmItemRentDeatils.ShowDialog();
	}

	private void TxtFine_KeyUp(object sender, KeyEventArgs e)
	{
		decimal result = 0m;
		decimal result2 = 0m;
		if (decimal.TryParse(TxtFine.Text, out result) && decimal.TryParse(TxtRecipient.Text, out result2))
		{
			TxtResidual.Text = (result - result2).ToString();
		}
	}

	private void TxtRecipient_KeyUp(object sender, KeyEventArgs e)
	{
		decimal result = 0m;
		decimal result2 = 0m;
		if (decimal.TryParse(TxtFine.Text, out result) && decimal.TryParse(TxtRecipient.Text, out result2))
		{
			TxtResidual.Text = (result - result2).ToString();
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.OrderRent.FrmRecipt3));
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
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label20 = new System.Windows.Forms.Label();
		this.ComCalcDays = new System.Windows.Forms.ComboBox();
		this.label37 = new System.Windows.Forms.Label();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtDayNum = new DevExpress.XtraEditors.TextEdit();
		this.TxtDateReceipt = new DevExpress.XtraEditors.DateEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtCodeRent = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtRentalDate = new DevExpress.XtraEditors.DateEdit();
		this.label24 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtSuppIDNum = new DevExpress.XtraEditors.TextEdit();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtAddress = new DevExpress.XtraEditors.TextEdit();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.LblSup = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtParentName = new DevExpress.XtraEditors.TextEdit();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
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
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
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
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
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
		this.TxtAllDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVatDisc = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotalDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtVat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.TxtOrderRest = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtOrderTotal = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtOrderCashMoney = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtOrderBank = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.TxtResidual = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtRecipient = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtFine = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtDeposit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtLocation = new DevExpress.XtraEditors.TextEdit();
		this.label25 = new System.Windows.Forms.Label();
		this.TxtTafqit = new DevExpress.XtraEditors.TextEdit();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDayNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeRent.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppIDNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtParentName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).BeginInit();
		base.SuspendLayout();
		this.repositoryItemButtonEdit1.AutoHeight = false;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions13.Image");
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.ComCalcDays);
		this.groupBox1.Controls.Add(this.label37);
		this.groupBox1.Controls.Add(this.TxtCharge);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtDayNum);
		this.groupBox1.Controls.Add(this.TxtDateReceipt);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.TxtCodeRent);
		this.groupBox1.Controls.Add(this.TxtRentalDate);
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
		this.groupBox1.Size = new System.Drawing.Size(1307, 214);
		this.groupBox1.TabIndex = 4;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "بيانات فاتورة الاستلام";
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label20.Location = new System.Drawing.Point(279, 177);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(97, 21);
		this.label20.TabIndex = 154;
		this.label20.Text = "حساب الايام";
		this.ComCalcDays.Font = new System.Drawing.Font("Tahoma", 10f);
		this.ComCalcDays.FormattingEnabled = true;
		this.ComCalcDays.Location = new System.Drawing.Point(7, 175);
		this.ComCalcDays.Name = "ComCalcDays";
		this.ComCalcDays.Size = new System.Drawing.Size(261, 29);
		this.ComCalcDays.TabIndex = 153;
		this.ComCalcDays.SelectedIndexChanged += new System.EventHandler(ComCalcDays_SelectedIndexChanged);
		this.label37.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label37.Location = new System.Drawing.Point(275, 25);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(94, 21);
		this.label37.TabIndex = 152;
		this.label37.Text = "رصيد العميل";
		this.TxtCharge.Enabled = false;
		this.TxtCharge.Location = new System.Drawing.Point(6, 22);
		this.TxtCharge.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCharge.Name = "TxtCharge";
		this.TxtCharge.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCharge.Size = new System.Drawing.Size(262, 28);
		this.TxtCharge.TabIndex = 151;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label2.Location = new System.Drawing.Point(276, 139);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(76, 21);
		this.label2.TabIndex = 150;
		this.label2.Text = "عدد الايام";
		this.TxtDayNum.EditValue = "";
		this.TxtDayNum.Location = new System.Drawing.Point(7, 137);
		this.TxtDayNum.Name = "TxtDayNum";
		this.TxtDayNum.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDayNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDayNum.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDayNum.Properties.Appearance.Options.UseFont = true;
		this.TxtDayNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDayNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDayNum.Properties.ReadOnly = true;
		this.TxtDayNum.Size = new System.Drawing.Size(263, 28);
		this.TxtDayNum.TabIndex = 149;
		this.TxtDateReceipt.EditValue = null;
		this.TxtDateReceipt.Location = new System.Drawing.Point(7, 98);
		this.TxtDateReceipt.Name = "TxtDateReceipt";
		this.TxtDateReceipt.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateReceipt.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDateReceipt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateReceipt.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDateReceipt.Properties.Appearance.Options.UseFont = true;
		this.TxtDateReceipt.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateReceipt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateReceipt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateReceipt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateReceipt.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDateReceipt.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDateReceipt.Size = new System.Drawing.Size(262, 28);
		this.TxtDateReceipt.TabIndex = 147;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label1.Location = new System.Drawing.Point(275, 101);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(105, 21);
		this.label1.TabIndex = 148;
		this.label1.Text = "تاريخ التسليم";
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label13.Location = new System.Drawing.Point(669, 25);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(82, 21);
		this.label13.TabIndex = 144;
		this.label13.Text = "رقم الايجار";
		this.TxtCodeRent.Location = new System.Drawing.Point(394, 22);
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
		this.TxtCodeRent.Size = new System.Drawing.Size(269, 28);
		this.TxtCodeRent.TabIndex = 143;
		this.TxtCodeRent.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCodeRent_ButtonClick);
		this.TxtRentalDate.EditValue = null;
		this.TxtRentalDate.Location = new System.Drawing.Point(394, 60);
		this.TxtRentalDate.Name = "TxtRentalDate";
		this.TxtRentalDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtRentalDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtRentalDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtRentalDate.Properties.Appearance.Options.UseBackColor = true;
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
		this.TxtRentalDate.Size = new System.Drawing.Size(269, 28);
		this.TxtRentalDate.TabIndex = 141;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label24.Location = new System.Drawing.Point(669, 63);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(90, 21);
		this.label24.TabIndex = 142;
		this.label24.Text = "تاريخ الايجار";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label11.Location = new System.Drawing.Point(1186, 177);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(73, 21);
		this.label11.TabIndex = 134;
		this.label11.Text = "ملاحظات";
		this.TxtNote.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNote.Location = new System.Drawing.Point(768, 175);
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNote.Size = new System.Drawing.Size(412, 28);
		this.TxtNote.TabIndex = 133;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label10.Location = new System.Drawing.Point(669, 101);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(81, 21);
		this.label10.TabIndex = 132;
		this.label10.Text = "رقم الهوية";
		this.TxtSuppIDNum.Location = new System.Drawing.Point(394, 98);
		this.TxtSuppIDNum.Name = "TxtSuppIDNum";
		this.TxtSuppIDNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSuppIDNum.Properties.Appearance.Options.UseFont = true;
		this.TxtSuppIDNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSuppIDNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSuppIDNum.Size = new System.Drawing.Size(269, 28);
		this.TxtSuppIDNum.TabIndex = 131;
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label9.Location = new System.Drawing.Point(1186, 140);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(56, 21);
		this.label9.TabIndex = 130;
		this.label9.Text = "العنوان";
		this.TxtAddress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtAddress.Location = new System.Drawing.Point(768, 137);
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtAddress.Properties.Appearance.Options.UseFont = true;
		this.TxtAddress.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAddress.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAddress.Size = new System.Drawing.Size(412, 28);
		this.TxtAddress.TabIndex = 129;
		this.LBlSuppAccount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LBlSuppAccount.AutoSize = true;
		this.LBlSuppAccount.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LBlSuppAccount.Location = new System.Drawing.Point(1281, 29);
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		this.LBlSuppAccount.Size = new System.Drawing.Size(19, 21);
		this.LBlSuppAccount.TabIndex = 128;
		this.LBlSuppAccount.Text = "1";
		this.LBlSuppAccount.Visible = false;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label26.Location = new System.Drawing.Point(666, 177);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(95, 21);
		this.label26.TabIndex = 127;
		this.label26.Text = "مركز التكلفة";
		this.CBCostCenter.Font = new System.Drawing.Font("Tahoma", 10f);
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Location = new System.Drawing.Point(394, 175);
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.Size = new System.Drawing.Size(269, 29);
		this.CBCostCenter.TabIndex = 8;
		this.LblSup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblSup.AutoSize = true;
		this.LblSup.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblSup.Location = new System.Drawing.Point(1281, 63);
		this.LblSup.Name = "LblSup";
		this.LblSup.Size = new System.Drawing.Size(19, 21);
		this.LblSup.TabIndex = 125;
		this.LblSup.Text = "1";
		this.TxtDate.EditValue = null;
		this.TxtDate.Location = new System.Drawing.Point(7, 60);
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
		this.TxtDate.Size = new System.Drawing.Size(262, 28);
		this.TxtDate.TabIndex = 0;
		this.label23.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label23.Location = new System.Drawing.Point(1186, 101);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(93, 21);
		this.label23.TabIndex = 117;
		this.label23.Text = "اسم الكفيل";
		this.TxtParentName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtParentName.Location = new System.Drawing.Point(768, 98);
		this.TxtParentName.Name = "TxtParentName";
		this.TxtParentName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtParentName.Properties.Appearance.Options.UseFont = true;
		this.TxtParentName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtParentName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtParentName.Size = new System.Drawing.Size(412, 28);
		this.TxtParentName.TabIndex = 6;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label22.Location = new System.Drawing.Point(666, 140);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(84, 21);
		this.label22.TabIndex = 115;
		this.label22.Text = "رقم الجوال";
		this.TxtPhone.Location = new System.Drawing.Point(394, 137);
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.Size = new System.Drawing.Size(269, 28);
		this.TxtPhone.TabIndex = 5;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label21.Location = new System.Drawing.Point(276, 63);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(53, 21);
		this.label21.TabIndex = 113;
		this.label21.Text = "التاريخ";
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label18.Location = new System.Drawing.Point(1186, 63);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(93, 21);
		this.label18.TabIndex = 107;
		this.label18.Text = "اسم العميل";
		this.TxtSuppliers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSuppliers.Location = new System.Drawing.Point(768, 60);
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
		this.TxtSuppliers.Size = new System.Drawing.Size(412, 28);
		this.TxtSuppliers.TabIndex = 3;
		this.TxtSuppliers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtSuppliers_ButtonClick);
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label17.Location = new System.Drawing.Point(1186, 25);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(90, 21);
		this.label17.TabIndex = 105;
		this.label17.Text = "رقم الفاتورة";
		this.TxtCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCode.Location = new System.Drawing.Point(768, 22);
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
		this.TxtCode.Size = new System.Drawing.Size(412, 28);
		this.TxtCode.TabIndex = 50;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.gridControl1);
		this.groupBox3.Controls.Add(this.TxtAllDiscount);
		this.groupBox3.Controls.Add(this.TxtVatDisc);
		this.groupBox3.Controls.Add(this.TxtDiscount);
		this.groupBox3.Controls.Add(this.TxtTotalDiscount);
		this.groupBox3.Controls.Add(this.TxtTotals);
		this.groupBox3.Controls.Add(this.TxtVat);
		this.groupBox3.Controls.Add(this.txtdicoutprege);
		this.groupBox3.Controls.Add(this.BtnEdit);
		this.groupBox3.Location = new System.Drawing.Point(12, 232);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1307, 307);
		this.groupBox3.TabIndex = 9;
		this.groupBox3.TabStop = false;
		this.gridControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridControl1.Location = new System.Drawing.Point(3, 16);
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[4] { this.repositoryItemButtonEdit1, this.repositoryItemButtonEdit2, this.repositoryItemButtonEdit3, this.repositoryItemButtonEdit4 });
		this.gridControl1.Size = new System.Drawing.Size(1301, 285);
		this.gridControl1.TabIndex = 0;
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
		this.gridColumn1.Width = 100;
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
		this.gridColumn2.Width = 168;
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
		this.gridColumn27.Width = 134;
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
		this.shearch.Width = 40;
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
		this.gridColumn4.Width = 50;
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
		this.gridColumn20.Caption = "الكمية";
		this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.FieldName = "Quantity";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.OptionsColumn.AllowEdit = false;
		this.gridColumn20.OptionsColumn.AllowFocus = false;
		this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", "SUM={0:0.##}")
		});
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.gridColumn20.Visible = true;
		this.gridColumn20.VisibleIndex = 5;
		this.gridColumn20.Width = 53;
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
		this.gridColumn3.Width = 50;
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
		this.gridColumn6.Width = 50;
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
		this.gridColumn7.Width = 50;
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
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit2;
		this.gridColumn8.CustomizationCaption = "ط­1ظپ";
		this.gridColumn8.FieldName = "DeleteItem";
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Visible = true;
		this.gridColumn8.VisibleIndex = 10;
		this.gridColumn8.Width = 20;
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit2.AutoHeight = false;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions14.Image");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
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
		this.gridColumn9.Width = 24;
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
		this.gridColumn26.Width = 60;
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
		this.gridColumn28.Width = 60;
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
		this.gridColumn29.OptionsFilter.AllowAutoFilter = false;
		this.gridColumn29.OptionsFilter.AllowFilter = false;
		this.gridColumn29.Width = 60;
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
		this.gridColumn30.OptionsFilter.AllowAutoFilter = false;
		this.gridColumn30.OptionsFilter.AllowFilter = false;
		this.gridColumn30.Width = 60;
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
		this.gridColumn34.VisibleIndex = 14;
		this.gridColumn34.Width = 60;
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
		this.gridColumn35.VisibleIndex = 16;
		this.gridColumn35.Width = 60;
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
		this.gridColumn37.ColumnEdit = this.repositoryItemButtonEdit3;
		this.gridColumn37.FieldName = "Info";
		this.gridColumn37.MinWidth = 25;
		this.gridColumn37.Name = "gridColumn37";
		this.gridColumn37.Visible = true;
		this.gridColumn37.VisibleIndex = 15;
		this.gridColumn37.Width = 71;
		this.repositoryItemButtonEdit3.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit3.AutoHeight = false;
		editorButtonImageOptions3.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions15.Image");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.repositoryItemButtonEdit3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit4_ButtonPressed);
		this.gridColumn38.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn38.AppearanceCell.Options.UseFont = true;
		this.gridColumn38.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn38.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn38.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn38.AppearanceHeader.Options.UseFont = true;
		this.gridColumn38.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn38.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn38.Caption = "الكميه المسلمه";
		this.gridColumn38.FieldName = "QuantityDev";
		this.gridColumn38.MinWidth = 25;
		this.gridColumn38.Name = "gridColumn38";
		this.gridColumn38.OptionsColumn.AllowEdit = false;
		this.gridColumn38.OptionsColumn.AllowFocus = false;
		this.gridColumn38.Width = 94;
		this.repositoryItemButtonEdit4.AutoHeight = false;
		editorButtonImageOptions4.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions16.Image");
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
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
		this.TxtAllDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtAllDiscount.DecimalDigits = 3;
		this.TxtAllDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtAllDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtAllDiscount.Location = new System.Drawing.Point(478, 150);
		this.TxtAllDiscount.Name = "TxtAllDiscount";
		this.TxtAllDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtAllDiscount.Size = new System.Drawing.Size(52, 27);
		this.TxtAllDiscount.TabIndex = 169;
		this.TxtAllDiscount.Text = "0.000";
		this.TxtAllDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAllDiscount.Value = new decimal(new int[4]);
		this.TxtAllDiscount.Visible = false;
		this.TxtAllDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtVatDisc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtVatDisc.DecimalDigits = 3;
		this.TxtVatDisc.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtVatDisc.ForeColor = System.Drawing.Color.Black;
		this.TxtVatDisc.Location = new System.Drawing.Point(478, 217);
		this.TxtVatDisc.Name = "TxtVatDisc";
		this.TxtVatDisc.ReadOnly = true;
		this.TxtVatDisc.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVatDisc.Size = new System.Drawing.Size(52, 27);
		this.TxtVatDisc.TabIndex = 162;
		this.TxtVatDisc.Text = "0.000";
		this.TxtVatDisc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVatDisc.Value = new decimal(new int[4]);
		this.TxtVatDisc.Visible = false;
		this.TxtVatDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtDiscount.Location = new System.Drawing.Point(579, 161);
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDiscount.Size = new System.Drawing.Size(28, 27);
		this.TxtDiscount.TabIndex = 160;
		this.TxtDiscount.Text = "0.000";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.Visible = false;
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotalDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTotalDiscount.DecimalDigits = 3;
		this.TxtTotalDiscount.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotalDiscount.ForeColor = System.Drawing.Color.Black;
		this.TxtTotalDiscount.Location = new System.Drawing.Point(571, 220);
		this.TxtTotalDiscount.Name = "TxtTotalDiscount";
		this.TxtTotalDiscount.ReadOnly = true;
		this.TxtTotalDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotalDiscount.Size = new System.Drawing.Size(36, 27);
		this.TxtTotalDiscount.TabIndex = 159;
		this.TxtTotalDiscount.Text = "0.000";
		this.TxtTotalDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotalDiscount.Value = new decimal(new int[4]);
		this.TxtTotalDiscount.Visible = false;
		this.TxtTotalDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtTotals.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Location = new System.Drawing.Point(618, 153);
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.ReadOnly = true;
		this.TxtTotals.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotals.Size = new System.Drawing.Size(28, 27);
		this.TxtTotals.TabIndex = 161;
		this.TxtTotals.Text = "0.000";
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.Visible = false;
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtVat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtVat.DecimalDigits = 3;
		this.TxtVat.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtVat.ForeColor = System.Drawing.Color.Black;
		this.TxtVat.Location = new System.Drawing.Point(478, 184);
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.ReadOnly = true;
		this.TxtVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVat.Size = new System.Drawing.Size(52, 27);
		this.TxtVat.TabIndex = 170;
		this.TxtVat.Text = "0.000";
		this.TxtVat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4]);
		this.TxtVat.Visible = false;
		this.TxtVat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.txtdicoutprege.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtdicoutprege.DecimalDigits = 3;
		this.txtdicoutprege.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.txtdicoutprege.ForeColor = System.Drawing.Color.Black;
		this.txtdicoutprege.Location = new System.Drawing.Point(571, 195);
		this.txtdicoutprege.Name = "txtdicoutprege";
		this.txtdicoutprege.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtdicoutprege.Size = new System.Drawing.Size(36, 27);
		this.txtdicoutprege.TabIndex = 189;
		this.txtdicoutprege.Text = "0.000";
		this.txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.txtdicoutprege.Value = new decimal(new int[4]);
		this.txtdicoutprege.Visible = false;
		this.txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.groupBox5);
		this.groupBox2.Controls.Add(this.label16);
		this.groupBox2.Controls.Add(this.TxtResidual);
		this.groupBox2.Controls.Add(this.label19);
		this.groupBox2.Controls.Add(this.TxtRecipient);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtFine);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.TxtDeposit);
		this.groupBox2.Controls.Add(this.TxtLocation);
		this.groupBox2.Controls.Add(this.label25);
		this.groupBox2.Controls.Add(this.TxtTafqit);
		this.groupBox2.Controls.Add(this.TxtNet);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Location = new System.Drawing.Point(15, 545);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1304, 249);
		this.groupBox2.TabIndex = 10;
		this.groupBox2.TabStop = false;
		this.groupBox5.BackColor = System.Drawing.Color.MistyRose;
		this.groupBox5.Controls.Add(this.TxtOrderRest);
		this.groupBox5.Controls.Add(this.TxtOrderTotal);
		this.groupBox5.Controls.Add(this.TxtOrderCashMoney);
		this.groupBox5.Controls.Add(this.label4);
		this.groupBox5.Controls.Add(this.TxtOrderBank);
		this.groupBox5.Controls.Add(this.label5);
		this.groupBox5.Controls.Add(this.label6);
		this.groupBox5.Controls.Add(this.label7);
		this.groupBox5.Location = new System.Drawing.Point(7, 15);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(537, 164);
		this.groupBox5.TabIndex = 220;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "بيانات فاتورة التأجير";
		this.TxtOrderRest.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtOrderRest.DecimalDigits = 3;
		this.TxtOrderRest.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtOrderRest.ForeColor = System.Drawing.Color.Red;
		this.TxtOrderRest.Location = new System.Drawing.Point(6, 124);
		this.TxtOrderRest.Name = "TxtOrderRest";
		this.TxtOrderRest.ReadOnly = true;
		this.TxtOrderRest.Size = new System.Drawing.Size(393, 28);
		this.TxtOrderRest.TabIndex = 198;
		this.TxtOrderRest.Text = "0.000";
		this.TxtOrderRest.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtOrderRest.Value = new decimal(new int[4]);
		this.TxtOrderRest.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtOrderTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtOrderTotal.DecimalDigits = 3;
		this.TxtOrderTotal.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtOrderTotal.ForeColor = System.Drawing.Color.Blue;
		this.TxtOrderTotal.Location = new System.Drawing.Point(6, 22);
		this.TxtOrderTotal.Name = "TxtOrderTotal";
		this.TxtOrderTotal.ReadOnly = true;
		this.TxtOrderTotal.Size = new System.Drawing.Size(393, 28);
		this.TxtOrderTotal.TabIndex = 195;
		this.TxtOrderTotal.Text = "0.000";
		this.TxtOrderTotal.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtOrderTotal.Value = new decimal(new int[4]);
		this.TxtOrderTotal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtOrderCashMoney.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtOrderCashMoney.DecimalDigits = 3;
		this.TxtOrderCashMoney.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtOrderCashMoney.ForeColor = System.Drawing.Color.Green;
		this.TxtOrderCashMoney.Location = new System.Drawing.Point(6, 56);
		this.TxtOrderCashMoney.Name = "TxtOrderCashMoney";
		this.TxtOrderCashMoney.ReadOnly = true;
		this.TxtOrderCashMoney.Size = new System.Drawing.Size(393, 28);
		this.TxtOrderCashMoney.TabIndex = 196;
		this.TxtOrderCashMoney.Text = "0.000";
		this.TxtOrderCashMoney.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtOrderCashMoney.Value = new decimal(new int[4]);
		this.TxtOrderCashMoney.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Bold);
		this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label4.Location = new System.Drawing.Point(406, 131);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(68, 19);
		this.label4.TabIndex = 166;
		this.label4.Text = "المتبقي";
		this.TxtOrderBank.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtOrderBank.DecimalDigits = 3;
		this.TxtOrderBank.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtOrderBank.ForeColor = System.Drawing.Color.Maroon;
		this.TxtOrderBank.Location = new System.Drawing.Point(6, 90);
		this.TxtOrderBank.Name = "TxtOrderBank";
		this.TxtOrderBank.ReadOnly = true;
		this.TxtOrderBank.Size = new System.Drawing.Size(393, 28);
		this.TxtOrderBank.TabIndex = 197;
		this.TxtOrderBank.Text = "0.000";
		this.TxtOrderBank.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtOrderBank.Value = new decimal(new int[4]);
		this.TxtOrderBank.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Bold);
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(406, 63);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(108, 19);
		this.label5.TabIndex = 165;
		this.label5.Text = "المدفوع كاش";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Bold);
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(406, 29);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(122, 19);
		this.label6.TabIndex = 164;
		this.label6.Text = "اجمالي الفاتورة";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Bold);
		this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label7.Location = new System.Drawing.Point(406, 97);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(122, 19);
		this.label7.TabIndex = 163;
		this.label7.Text = "المدفوع شبكة ";
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label16.Location = new System.Drawing.Point(835, 188);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(64, 19);
		this.label16.TabIndex = 217;
		this.label16.Text = "المتبقي";
		this.TxtResidual.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtResidual.DecimalDigits = 2;
		this.TxtResidual.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtResidual.ForeColor = System.Drawing.Color.Maroon;
		this.TxtResidual.Location = new System.Drawing.Point(550, 182);
		this.TxtResidual.Name = "TxtResidual";
		this.TxtResidual.ReadOnly = true;
		this.TxtResidual.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtResidual.Size = new System.Drawing.Size(277, 28);
		this.TxtResidual.TabIndex = 216;
		this.TxtResidual.Text = "0.00";
		this.TxtResidual.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtResidual.UseCompatibleTextRendering = false;
		this.TxtResidual.Value = new decimal(new int[4]);
		this.TxtResidual.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label19.Location = new System.Drawing.Point(835, 154);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(68, 19);
		this.label19.TabIndex = 215;
		this.label19.Text = "المستلم";
		this.TxtRecipient.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtRecipient.DecimalDigits = 2;
		this.TxtRecipient.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtRecipient.ForeColor = System.Drawing.Color.Green;
		this.TxtRecipient.Location = new System.Drawing.Point(550, 147);
		this.TxtRecipient.Name = "TxtRecipient";
		this.TxtRecipient.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtRecipient.Size = new System.Drawing.Size(277, 28);
		this.TxtRecipient.TabIndex = 214;
		this.TxtRecipient.Text = "0.00";
		this.TxtRecipient.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtRecipient.UseCompatibleTextRendering = false;
		this.TxtRecipient.Value = new decimal(new int[4]);
		this.TxtRecipient.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtRecipient.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtRecipient_KeyUp);
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label3.Location = new System.Drawing.Point(838, 120);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(54, 19);
		this.label3.TabIndex = 213;
		this.label3.Text = "الغرامه";
		this.TxtFine.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtFine.DecimalDigits = 2;
		this.TxtFine.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtFine.ForeColor = System.Drawing.Color.Red;
		this.TxtFine.Location = new System.Drawing.Point(550, 114);
		this.TxtFine.Name = "TxtFine";
		this.TxtFine.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtFine.Size = new System.Drawing.Size(277, 28);
		this.TxtFine.TabIndex = 212;
		this.TxtFine.Text = "0.00";
		this.TxtFine.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtFine.UseCompatibleTextRendering = false;
		this.TxtFine.Value = new decimal(new int[4]);
		this.TxtFine.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtFine.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtFine_KeyUp);
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label12.Location = new System.Drawing.Point(832, 88);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(55, 19);
		this.label12.TabIndex = 211;
		this.label12.Text = "التأمين";
		this.TxtDeposit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDeposit.DecimalDigits = 2;
		this.TxtDeposit.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtDeposit.ForeColor = System.Drawing.Color.Blue;
		this.TxtDeposit.Location = new System.Drawing.Point(550, 81);
		this.TxtDeposit.Name = "TxtDeposit";
		this.TxtDeposit.ReadOnly = true;
		this.TxtDeposit.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDeposit.Size = new System.Drawing.Size(277, 28);
		this.TxtDeposit.TabIndex = 210;
		this.TxtDeposit.Text = "0.00";
		this.TxtDeposit.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDeposit.UseCompatibleTextRendering = false;
		this.TxtDeposit.Value = new decimal(new int[4]);
		this.TxtDeposit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtLocation.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtLocation.EditValue = "";
		this.TxtLocation.Location = new System.Drawing.Point(6, 185);
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
		this.TxtLocation.Size = new System.Drawing.Size(492, 28);
		this.TxtLocation.TabIndex = 126;
		this.label25.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label25.Location = new System.Drawing.Point(497, 189);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(51, 19);
		this.label25.TabIndex = 191;
		this.label25.Text = "الموقع";
		this.TxtTafqit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtTafqit.Location = new System.Drawing.Point(6, 215);
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
		this.TxtTafqit.Size = new System.Drawing.Size(891, 28);
		this.TxtTafqit.TabIndex = 93;
		this.TxtNet.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Location = new System.Drawing.Point(907, 93);
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
		this.BtnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(758, 184);
		this.BtnEdit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Size = new System.Drawing.Size(125, 40);
		this.BtnEdit.TabIndex = 4;
		this.BtnEdit.Text = "تعديل F6";
		this.BtnEdit.Visible = false;
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(907, 151);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(258, 40);
		this.BtnSave.TabIndex = 3;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(1040, 199);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(125, 40);
		this.BtnPrint.TabIndex = 6;
		this.BtnPrint.Text = "طباعة Ctrl P";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(1173, 151);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(125, 40);
		this.BtnNew.TabIndex = 7;
		this.BtnNew.Text = "جديد F4";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(1173, 199);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(125, 40);
		this.BtnDelete.TabIndex = 5;
		this.BtnDelete.Text = "حذف F7";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(907, 199);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(125, 40);
		this.BtnExit.TabIndex = 8;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1331, 806);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmRecipt3";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		this.Text = "فاتوره الاستلام";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmRecipt3_FormClosing);
		base.Load += new System.EventHandler(FrmRecipt3_Load);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDayNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeRent.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRentalDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppIDNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtParentName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTafqit.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


