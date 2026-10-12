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
using GTSErpSystem.BLL.Cust_Sup;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Cust_Sup.Search;

public class FrmSearchCustomerAll : XtraForm
{
	private Class_Customer Cust = new Class_Customer();

	private Class_Place PLC = new Class_Place();

	private Class_SalesMan SLSMAN = new Class_SalesMan();

	public int CustCode;

	public string CusName;

	public int CustID;

	private int Sn = 1;

	private bool isAllSelected = false;

	public string SelectedCustomerIds = "";

	public string SelectedCustomerNames = "";

	private IContainer components = null;

	private GroupBox groupBox1;

	public GridEX gridEX1;

	private GroupBox groupBox2;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	public FrmSearchCustomerAll()
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
		gridEX1.ColumnHeaderClick += GridEX1_ColumnHeaderClick;
		BindGrid();
	}

	public void BindGrid()
	{
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable = new DataTable("gridEX1");
		dataTable.Columns.Add("Select", typeof(bool));
		if (LoginDetails.Lang == "ar-EG")
		{
			dataTable.Columns.Add("التسلسل", typeof(int));
			dataTable.Columns.Add("كود العميل", typeof(int));
			dataTable.Columns.Add("اسم العميل", typeof(string));
			dataTable.Columns.Add("CusID", typeof(int));
			dataTable.Columns.Add("رقم الجوال", typeof(string));
			if (LoginDetails.Active_ShowAllCustomer)
			{
				foreach (Account_CustSup item in (from x in Cust.GetAll()
					where x.FrmCust == true
					select x).ToList())
				{
					dataTable.Rows.Add(false, item.ID, item.ID, item.CustSuppName, item.ID, item.Phone);
				}
			}
			else
			{
				foreach (Account_CustSup item2 in (from x in Cust.GetAll()
					where x.FrmCust == true && x.FranchiseID == LoginDetails.Active_FranchiseID
					select x).ToList())
				{
					dataTable.Rows.Add(false, item2.ID, item2.ID, item2.CustSuppName, item2.ID, item2.Phone);
				}
			}
		}
		else
		{
			dataTable.Columns.Add("SN", typeof(int));
			dataTable.Columns.Add("Customer Code", typeof(int));
			dataTable.Columns.Add("Customer Name", typeof(string));
			dataTable.Columns.Add("CustID", typeof(int));
			dataTable.Columns.Add("Mobil Number", typeof(string));
			dataTable.Columns.Add("TAX Number", typeof(string));
			if (LoginDetails.Active_ShowAllCustomer)
			{
				foreach (Account_CustSup item3 in (from x in Cust.GetAll()
					where x.FrmCust == true
					select x).ToList())
				{
					dataTable.Rows.Add(false, item3.ID, item3.ID, item3.CustSuppName, item3.ID, item3.Phone, item3.VatNum);
				}
			}
			else
			{
				foreach (Account_CustSup item4 in (from x in Cust.GetAll()
					where x.FrmCust == true && x.FranchiseID == LoginDetails.Active_FranchiseID
					select x).ToList())
				{
					dataTable.Rows.Add(false, item4.ID, item4.ID, item4.CustSuppName, item4.ID, item4.Phone, item4.VatNum);
				}
			}
		}
		gridEX1.SetDataBinding(dataTable, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["Select"].ColumnType = ColumnType.CheckBox;
		gridEX1.RootTable.Columns["Select"].Position = 0;
		gridEX1.RootTable.Columns["Select"].Width = 50;
		gridEX1.RootTable.Columns["Select"].Caption = "âœ“";
		gridEX1.RootTable.RowHeight = 28;
		gridEX1.RootTable.Columns["CusID"].Visible = false;
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.RootTable.Columns["التسلسل"].Width = 50;
			gridEX1.RootTable.Columns["التسلسل"].RightToLeft = RightToLeft.No;
		}
		else
		{
			gridEX1.RootTable.Columns["SN"].Width = 50;
			gridEX1.RootTable.Columns["SN"].RightToLeft = RightToLeft.No;
		}
	}

	private void GridEX1_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
	{
		if (!(e.Column.Key != "Select"))
		{
			isAllSelected = !isAllSelected;
			GridEXRow[] rows = gridEX1.GetRows();
			foreach (GridEXRow gridEXRow in rows)
			{
				gridEXRow.Cells["Select"].Value = isAllSelected;
			}
		}
	}

	private void LoadSelectedCustomers()
	{
		List<int> list = new List<int>();
		List<string> list2 = new List<string>();
		GridEXRow[] rows = gridEX1.GetRows();
		foreach (GridEXRow gridEXRow in rows)
		{
			bool flag = false;
			if (gridEXRow.Cells["Select"].Value != DBNull.Value)
			{
				flag = Convert.ToBoolean(gridEXRow.Cells["Select"].Value);
			}
			if (flag)
			{
				list.Add(Convert.ToInt32(gridEXRow.Cells["CustID"].Value));
				if (LoginDetails.Lang == "ar-EG")
				{
					list2.Add(Convert.ToString(gridEXRow.Cells["اسم العميل"].Value));
				}
				else
				{
					list2.Add(Convert.ToString(gridEXRow.Cells["Customer Name"].Value));
				}
			}
		}
		SelectedCustomerIds = string.Join(",", list);
		SelectedCustomerNames = string.Join(",", list2);
		if (list.Count <= 0)
		{
			return;
		}
		CustID = list[0];
		if (LoginDetails.Lang == "ar-EG")
		{
			GridEXRow[] rows2 = gridEX1.GetRows();
			foreach (GridEXRow gridEXRow2 in rows2)
			{
				if (Convert.ToBoolean(gridEXRow2.Cells["Select"].Value))
				{
					CustCode = Convert.ToInt32(gridEXRow2.Cells["كود العميل"].Value);
					CusName = Convert.ToString(gridEXRow2.Cells["اسم العميل"].Value);
					break;
				}
			}
			return;
		}
		GridEXRow[] rows3 = gridEX1.GetRows();
		foreach (GridEXRow gridEXRow3 in rows3)
		{
			if (Convert.ToBoolean(gridEXRow3.Cells["Select"].Value))
			{
				CustCode = Convert.ToInt32(gridEXRow3.Cells["Customer Code"].Value);
				CusName = Convert.ToString(gridEXRow3.Cells["Customer Name"].Value);
				break;
			}
		}
	}

	private void BtnNext_Click(object sender, EventArgs e)
	{
		LoadSelectedCustomers();
		Close();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void gridEX1_DoubleClick(object sender, EventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		if (LoginDetails.Lang == "ar-EG")
		{
			CustCode = Convert.ToInt32(gridEX1.GetRow(position).Cells["كود العميل"].Value);
			CusName = Convert.ToString(gridEX1.GetRow(position).Cells["اسم العميل"].Value);
		}
		else
		{
			CustCode = Convert.ToInt32(gridEX1.GetRow(position).Cells["Customer Code"].Value);
			CusName = Convert.ToString(gridEX1.GetRow(position).Cells["Customer Name"].Value);
		}
		CustID = Convert.ToInt32(gridEX1.GetRow(position).Cells["CustID"].Value);
		SelectedCustomerIds = CustID.ToString();
		SelectedCustomerNames = CusName;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Cust_Sup.Search.FrmSearchCustomerAll));
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
		base.Name = "FrmSearchCustomerAll";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


