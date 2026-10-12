using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Orders;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Orders.FrmSearchOrder;

public class FrmSearchPriceOffer : XtraForm
{
	private Class_PriceOffer Userx = new Class_PriceOffer();

	public int CustCode;

	private IContainer components = null;

	private GroupBox groupBox1;

	public GridEX gridEX1;

	private GroupBox groupBox2;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	public FrmSearchPriceOffer()
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
		if (LoginDetails.Active_Virgin == 9)
		{
			BindGrid2();
		}
		else
		{
			BindGrid();
		}
	}

	public void BindGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("التسلسل", typeof(int));
			dataTable.Columns.Add("كود ", typeof(int));
			dataTable.Columns.Add("كود العميل", typeof(int));
			dataTable.Columns.Add("اسم العميل", typeof(string));
			dataTable.Columns.Add("رقم الجوال", typeof(string));
			dataTable.Columns.Add("الرقم الضريبي", typeof(string));
			dataTable.Columns.Add("التاريخ", typeof(string));
			dataTable.Columns.Add("نوع الفاتورة", typeof(string));
			dataTable.Columns.Add("ملاحظات", typeof(string));
			dataTable.Columns.Add("الاجمالي", typeof(string));
			dataTable.Columns.Add("قيمة الخصم", typeof(string));
			dataTable.Columns.Add("نسبة الخصم", typeof(string));
			dataTable.Columns.Add("الضريبة", typeof(string));
			dataTable.Columns.Add("الصافي", typeof(string));
			foreach (Select_Order_PriceOffer_Result selectAllPurchase in Userx.GetSelectAllPurchases())
			{
				dataTable.Rows.Add(selectAllPurchase.SN, selectAllPurchase.ID, selectAllPurchase.SupplierID, selectAllPurchase.SupplierName, selectAllPurchase.SupplierPhone, selectAllPurchase.SupplierVatNum, Convert.ToDateTime(selectAllPurchase.Purchases_Date).ToShortDateString(), selectAllPurchase.PurchasesType, selectAllPurchase.Note, selectAllPurchase.TotalPrices, selectAllPurchase.DiscountNum, selectAllPurchase.DiscountPerantage, selectAllPurchase.Tax, selectAllPurchase.Net);
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["التسلسل"].Width = 30;
			gridEX1.RootTable.RowHeight = 28;
			gridEX1.RootTable.Columns["التسلسل"].RightToLeft = RightToLeft.No;
			gridEX1.RootTable.Columns["كود العميل"].Visible = false;
			gridEX1.RootTable.Columns["نسبة الخصم"].Visible = false;
			gridEX1.RootTable.Columns["قيمة الخصم"].Visible = false;
			gridEX1.RootTable.Columns["الرقم الضريبي"].Visible = false;
			return;
		}
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable2 = new DataTable("gridEX1");
		dataTable2.Columns.Add("SN", typeof(int));
		dataTable2.Columns.Add("Code ", typeof(int));
		dataTable2.Columns.Add("Customer Code", typeof(int));
		dataTable2.Columns.Add("Customer Name", typeof(string));
		dataTable2.Columns.Add("Mobil Number", typeof(string));
		dataTable2.Columns.Add("Tax Number", typeof(string));
		dataTable2.Columns.Add("Date", typeof(string));
		dataTable2.Columns.Add("Order Type", typeof(string));
		dataTable2.Columns.Add("Notes", typeof(string));
		dataTable2.Columns.Add("Total", typeof(string));
		dataTable2.Columns.Add("Disc Money", typeof(string));
		dataTable2.Columns.Add("Disc Perc", typeof(string));
		dataTable2.Columns.Add("TAX", typeof(string));
		dataTable2.Columns.Add("Net", typeof(string));
		foreach (Select_Order_PriceOffer_Result selectAllPurchase2 in Userx.GetSelectAllPurchases())
		{
			dataTable2.Rows.Add(selectAllPurchase2.SN, selectAllPurchase2.ID, selectAllPurchase2.SupplierID, selectAllPurchase2.SupplierName, selectAllPurchase2.SupplierPhone, selectAllPurchase2.SupplierVatNum, Convert.ToDateTime(selectAllPurchase2.Purchases_Date).ToShortDateString(), selectAllPurchase2.PurchasesType, selectAllPurchase2.Note, selectAllPurchase2.TotalPrices, selectAllPurchase2.DiscountNum, selectAllPurchase2.DiscountPerantage, selectAllPurchase2.Tax, selectAllPurchase2.Safy);
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["SN"].Width = 30;
		gridEX1.RootTable.RowHeight = 28;
		gridEX1.RootTable.Columns["SN"].RightToLeft = RightToLeft.No;
		gridEX1.RootTable.Columns["Customer Code"].Visible = false;
		gridEX1.RootTable.Columns["Disc Money"].Visible = false;
		gridEX1.RootTable.Columns["Disc Perc"].Visible = false;
		gridEX1.RootTable.Columns["Tax Number"].Visible = false;
	}

	public void BindGrid2()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("التسلسل", typeof(int));
			dataTable.Columns.Add("كود ", typeof(int));
			dataTable.Columns.Add("كود العميل", typeof(int));
			dataTable.Columns.Add("اسم العميل", typeof(string));
			dataTable.Columns.Add("رقم الجوال", typeof(string));
			dataTable.Columns.Add("الرقم الضريبي", typeof(string));
			dataTable.Columns.Add("التاريخ", typeof(string));
			dataTable.Columns.Add("تاريخ الحجز", typeof(string));
			dataTable.Columns.Add("تاريخ التركيب", typeof(string));
			dataTable.Columns.Add("تاريخ الفك", typeof(string));
			dataTable.Columns.Add("اسم القاعه", typeof(string));
			dataTable.Columns.Add("نوع الفاتورة", typeof(string));
			dataTable.Columns.Add("ملاحظات", typeof(string));
			dataTable.Columns.Add("الاجمالي", typeof(string));
			dataTable.Columns.Add("قيمة الخصم", typeof(string));
			dataTable.Columns.Add("نسبة الخصم", typeof(string));
			dataTable.Columns.Add("الضريبة", typeof(string));
			dataTable.Columns.Add("الصافي", typeof(string));
			foreach (Select_Order_PriceOffer_Result selectAllPurchase in Userx.GetSelectAllPurchases())
			{
				dataTable.Rows.Add(selectAllPurchase.SN, selectAllPurchase.ID, selectAllPurchase.SupplierID, selectAllPurchase.SupplierName, selectAllPurchase.SupplierPhone, selectAllPurchase.SupplierVatNum, Convert.ToDateTime(selectAllPurchase.Purchases_Date).ToShortDateString(), Convert.ToDateTime(selectAllPurchase.BookingDate).ToShortDateString(), Convert.ToDateTime(selectAllPurchase.InstallationDate).ToShortDateString(), Convert.ToDateTime(selectAllPurchase.jawDate).ToShortDateString(), selectAllPurchase.HallsName, selectAllPurchase.PurchasesType, selectAllPurchase.Note, selectAllPurchase.TotalPrices, selectAllPurchase.DiscountNum, selectAllPurchase.DiscountPerantage, selectAllPurchase.Tax, selectAllPurchase.Safy);
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["التسلسل"].Width = 30;
			gridEX1.RootTable.RowHeight = 28;
			gridEX1.RootTable.Columns["التسلسل"].RightToLeft = RightToLeft.No;
			gridEX1.RootTable.Columns["كود العميل"].Visible = false;
			gridEX1.RootTable.Columns["نسبة الخصم"].Visible = false;
			gridEX1.RootTable.Columns["قيمة الخصم"].Visible = false;
			gridEX1.RootTable.Columns["الرقم الضريبي"].Visible = false;
			return;
		}
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable2 = new DataTable("gridEX1");
		dataTable2.Columns.Add("SN", typeof(int));
		dataTable2.Columns.Add("Code ", typeof(int));
		dataTable2.Columns.Add("Customer Code", typeof(int));
		dataTable2.Columns.Add("Customer Name", typeof(string));
		dataTable2.Columns.Add("Mobil Number", typeof(string));
		dataTable2.Columns.Add("Tax Number", typeof(string));
		dataTable2.Columns.Add("Date", typeof(string));
		dataTable2.Columns.Add("Order Type", typeof(string));
		dataTable2.Columns.Add("Notes", typeof(string));
		dataTable2.Columns.Add("Total", typeof(string));
		dataTable2.Columns.Add("Disc Money", typeof(string));
		dataTable2.Columns.Add("Disc Perc", typeof(string));
		dataTable2.Columns.Add("TAX", typeof(string));
		dataTable2.Columns.Add("Net", typeof(string));
		foreach (Select_Order_PriceOffer_Result selectAllPurchase2 in Userx.GetSelectAllPurchases())
		{
			dataTable2.Rows.Add(selectAllPurchase2.SN, selectAllPurchase2.ID, selectAllPurchase2.SupplierID, selectAllPurchase2.SupplierName, selectAllPurchase2.SupplierPhone, selectAllPurchase2.SupplierVatNum, Convert.ToDateTime(selectAllPurchase2.Purchases_Date).ToShortDateString(), selectAllPurchase2.PurchasesType, selectAllPurchase2.Note, selectAllPurchase2.TotalPrices, selectAllPurchase2.DiscountNum, selectAllPurchase2.DiscountPerantage, selectAllPurchase2.Tax, selectAllPurchase2.Safy);
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["SN"].Width = 30;
		gridEX1.RootTable.RowHeight = 28;
		gridEX1.RootTable.Columns["SN"].RightToLeft = RightToLeft.No;
		gridEX1.RootTable.Columns["Customer Code"].Visible = false;
		gridEX1.RootTable.Columns["Disc Money"].Visible = false;
		gridEX1.RootTable.Columns["Disc Perc"].Visible = false;
		gridEX1.RootTable.Columns["Tax Number"].Visible = false;
	}

	private void BtnNext_Click(object sender, EventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		CustCode = Convert.ToInt32(gridEX1.GetRow(position).Cells[1].Text);
		Close();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void gridEX1_DoubleClick(object sender, EventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		CustCode = Convert.ToInt32(gridEX1.GetRow(position).Cells[1].Text);
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmSearchOrder.FrmSearchPriceOffer));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.gridEX1);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.gridEX1, "gridEX1");
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.AlternatingRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
		this.gridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
		this.gridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
		this.gridEX1.FilterRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
		this.gridEX1.FocusCellFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FocusCellFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxInfoFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.LinkFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.LinkFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.NewRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.NewRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Black;
		this.gridEX1.PreviewRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.PreviewRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedInactiveFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedInactiveFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TableHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.DoubleClick += new System.EventHandler(gridEX1_DoubleClick);
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnNext_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmSearchPriceOffer";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


