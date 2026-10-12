using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Orders;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Orders.FrmSearchOrder;

public class FrmSearchTransfer : XtraForm
{
	private Class_StoresTransfer Userx = new Class_StoresTransfer();

	public int ItemIdStore;

	public int StorID;

	private IContainer components = null;

	private GroupBox groupBox1;

	public GridEX gridEX1;

	private GroupBox groupBox2;

	private SimpleButton BtnOk;

	private SimpleButton BtnExit;

	public FrmSearchTransfer()
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
		BindGrid();
	}

	public void BindGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("كود", typeof(int));
			dataTable.Columns.Add("اسم المخزن", typeof(string));
			dataTable.Columns.Add("الكمية المتوفرة", typeof(string));
			ItemIdStore = LoginDetails.ItemcodeForStore;
			foreach (Account_Stores item in from x in Userx.GetStore()
				where x.BranchID == LoginDetails.BranchID
				select x)
			{
				dataTable.Rows.Add(item.ID, item.Store_Name, Math.Round(Convert.ToDecimal(item.ItemQuantities.Where((ItemQuantity x) => x.ItemID == ItemIdStore).Sum((ItemQuantity x) => x.CurrentBalance)), 2));
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["كود"].Width = 30;
			gridEX1.RootTable.RowHeight = 28;
			gridEX1.RootTable.Columns["كود"].RightToLeft = RightToLeft.No;
			LoginDetails.ItemcodeForStore = 0;
			return;
		}
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable2 = new DataTable("gridEX1");
		dataTable2.Columns.Add("Code", typeof(int));
		dataTable2.Columns.Add("Store Name", typeof(string));
		dataTable2.Columns.Add("Quantity", typeof(string));
		ItemIdStore = LoginDetails.ItemcodeForStore;
		foreach (Account_Stores item2 in from x in Userx.GetStore()
			where x.BranchID == LoginDetails.BranchID
			select x)
		{
			dataTable2.Rows.Add(item2.ID, item2.Store_Name, Math.Round(Convert.ToDecimal(item2.ItemQuantities.Where((ItemQuantity x) => x.ItemID == ItemIdStore).Sum((ItemQuantity x) => x.CurrentBalance)), 2));
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["Code"].Width = 30;
		gridEX1.RootTable.RowHeight = 28;
		gridEX1.RootTable.Columns["Code"].RightToLeft = RightToLeft.No;
		LoginDetails.ItemcodeForStore = 0;
	}

	private void BtnNext_Click(object sender, EventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		LoginDetails.StoreID = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
		StorID = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
		Close();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		LoginDetails.StoreID = 1;
		Close();
	}

	private void gridEX1_DoubleClick(object sender, EventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		LoginDetails.StoreID = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
		StorID = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmSearchOrder.FrmSearchTransfer));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnOk = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
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
		this.gridEX1.EmptyRows = true;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX1.DoubleClick += new System.EventHandler(gridEX1_DoubleClick);
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.BtnOk);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.BtnOk, "BtnOk");
		this.BtnOk.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOk.ImageOptions.Image");
		this.BtnOk.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnOk.Name = "BtnOk";
		this.BtnOk.Click += new System.EventHandler(BtnNext_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmSearchTransfer";
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


