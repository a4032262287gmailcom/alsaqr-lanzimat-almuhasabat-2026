using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Items;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Orders.FrmSearchOrder;

public class FrmSearchUnit : XtraForm
{
	private Class_Items Items = new Class_Items();

	public string ItemCode;

	public string unitName;

	private IContainer components = null;

	private GridEX gridEX1;

	private SimpleButton BtnSave;

	private SimpleButton BtnExit;

	public FrmSearchUnit()
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
			string itemcodeForUnits = LoginDetails.ItemcodeForUnits;
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("نوع الوحدة", typeof(string));
			dataTable.Columns.Add("الوحدة ", typeof(string));
			IEnumerable<Item_Unit> source = from x in Items.GetUnitsItems(itemcodeForUnits)
				where x.ID != 0
				select x;
			int num = 1;
			foreach (Item_Unit item in source.ToList())
			{
				if (num == 1)
				{
					dataTable.Rows.Add("الصغرى", item.Name);
				}
				if (num == 2)
				{
					dataTable.Rows.Add("المتوسطة", item.Name);
				}
				if (num == 3)
				{
					dataTable.Rows.Add("الكبرى", item.Name);
				}
				num++;
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			return;
		}
		string itemcodeForUnits2 = LoginDetails.ItemcodeForUnits;
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable2 = new DataTable("gridEX1");
		dataTable2.Columns.Add("Unit Type", typeof(string));
		dataTable2.Columns.Add("Unit ", typeof(string));
		IEnumerable<Item_Unit> source2 = from x in Items.GetUnitsItems(itemcodeForUnits2)
			where x.ID != 0
			select x;
		int num2 = 1;
		foreach (Item_Unit item2 in source2.ToList())
		{
			if (num2 == 1)
			{
				dataTable2.Rows.Add("الصغرى", item2.Name);
			}
			if (num2 == 2)
			{
				dataTable2.Rows.Add("المتوسطة", item2.Name);
			}
			if (num2 == 3)
			{
				dataTable2.Rows.Add("الكبرى", item2.Name);
			}
			num2++;
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
	}

	private void BtnNext_Click(object sender, EventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		unitName = gridEX1.GetRow(position).Cells[0].Text;
		ItemCode = LoginDetails.ItemcodeForUnits;
		Close();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void gridEX1_DoubleClick(object sender, EventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		unitName = gridEX1.GetRow(position).Cells[0].Text;
		ItemCode = LoginDetails.ItemcodeForUnits;
		Close();
	}

	private void gridEX1_MouseClick(object sender, MouseEventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		unitName = gridEX1.GetRow(position).Cells[0].Text;
		ItemCode = LoginDetails.ItemcodeForUnits;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmSearchOrder.FrmSearchUnit));
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.gridEX1, "gridEX1");
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.AlternatingRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.EmptyRows = true;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.RowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseClick);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnNext_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.gridEX1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmSearchUnit";
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		base.ResumeLayout(false);
	}
}


