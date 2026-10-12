using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Account;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Account.Check.Search;

public class FrmSearchCheckIssued : XtraForm
{
	private Class_Receipts RCP = new Class_Receipts();

	public int CustCode;

	private string CheckType = LoginDetails.FrmName;

	private IContainer components = null;

	private GroupBox groupBox1;

	public GridEX gridEX1;

	private GroupBox groupBox2;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	public FrmSearchCheckIssued()
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
			dataTable.Columns.Add("التسلسل", typeof(int));
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns.Add("رقم الشيك", typeof(string));
			dataTable.Columns.Add("الحساب", typeof(string));
			dataTable.Columns.Add("المبلغ", typeof(string));
			dataTable.Columns.Add("الشرح", typeof(string));
			dataTable.Columns.Add("الرقم الدفتري", typeof(string));
			dataTable.Columns.Add("التاريخ", typeof(DateTime));
			dataTable.Columns.Add("تاريخ الصرف", typeof(DateTime));
			dataTable.Columns.Add("الفرع", typeof(string));
			if (CheckType == "FrmCheck")
			{
				foreach (Select_Search_Account_Receipts_Result item in from x in RCP.GetSearchAccount_Receipts(40)
					where x.ISCheckDone != true
					orderby x.DateDue
					select x)
				{
					dataTable.Rows.Add(item.ID, item.ReferenceCode, item.CheckID, item.Account_Name2, item.Money, item.Note, item.DocCode, Convert.ToDateTime(item.TranDate), Convert.ToDateTime(item.DateDue), item.BranchName);
				}
			}
			else if (CheckType == "FrmCheckCollection")
			{
				foreach (Select_Search_Account_Receipts_Result item2 in from x in RCP.GetSearchAccount_Receipts(40)
					where x.ISCheckDone == true
					select x)
				{
					dataTable.Rows.Add(item2.ID, item2.ReferenceCode, item2.CheckID, item2.Account_Name2, item2.Money, item2.Note, item2.DocCode, Convert.ToDateTime(item2.TranDate), Convert.ToDateTime(item2.DateDue), item2.BranchName);
				}
			}
			else
			{
				foreach (Select_Search_Account_Receipts_Result searchAccount_Receipt in RCP.GetSearchAccount_Receipts(40))
				{
					dataTable.Rows.Add(searchAccount_Receipt.ID, searchAccount_Receipt.ReferenceCode, searchAccount_Receipt.CheckID, searchAccount_Receipt.Account_Name2, searchAccount_Receipt.Money, searchAccount_Receipt.Note, searchAccount_Receipt.DocCode, Convert.ToDateTime(searchAccount_Receipt.TranDate), Convert.ToDateTime(searchAccount_Receipt.DateDue), searchAccount_Receipt.BranchName);
				}
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["التسلسل"].Width = 50;
			gridEX1.RootTable.RowHeight = 28;
			gridEX1.RootTable.Columns["التسلسل"].RightToLeft = RightToLeft.No;
			return;
		}
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable2 = new DataTable("gridEX1");
		dataTable2.Columns.Add("SN", typeof(int));
		dataTable2.Columns.Add("ID", typeof(int));
		dataTable2.Columns.Add("Check Num", typeof(string));
		dataTable2.Columns.Add("Account", typeof(string));
		dataTable2.Columns.Add("Money", typeof(string));
		dataTable2.Columns.Add("Notes", typeof(string));
		dataTable2.Columns.Add("Book Number", typeof(string));
		dataTable2.Columns.Add("Date Check", typeof(DateTime));
		dataTable2.Columns.Add("Issued Date", typeof(DateTime));
		dataTable2.Columns.Add("Branch", typeof(string));
		if (CheckType == "FrmCheck")
		{
			foreach (Select_Search_Account_Receipts_Result item3 in from x in RCP.GetSearchAccount_Receipts(40)
				where x.ISCheckDone != true
				orderby x.DateDue
				select x)
			{
				dataTable2.Rows.Add(item3.ID, item3.ReferenceCode, item3.CheckID, item3.Account_Name2, item3.Money, item3.Note, item3.DocCode, Convert.ToDateTime(item3.TranDate), Convert.ToDateTime(item3.DateDue), item3.BranchName);
			}
		}
		else if (CheckType == "FrmCheckCollection")
		{
			foreach (Select_Search_Account_Receipts_Result item4 in from x in RCP.GetSearchAccount_Receipts(40)
				where x.ISCheckDone == true
				select x)
			{
				dataTable2.Rows.Add(item4.ID, item4.ReferenceCode, item4.CheckID, item4.Account_Name2, item4.Money, item4.Note, item4.DocCode, Convert.ToDateTime(item4.TranDate), Convert.ToDateTime(item4.DateDue), item4.BranchName);
			}
		}
		else
		{
			foreach (Select_Search_Account_Receipts_Result searchAccount_Receipt2 in RCP.GetSearchAccount_Receipts(40))
			{
				dataTable2.Rows.Add(searchAccount_Receipt2.ID, searchAccount_Receipt2.ReferenceCode, searchAccount_Receipt2.CheckID, searchAccount_Receipt2.Account_Name2, searchAccount_Receipt2.Money, searchAccount_Receipt2.Note, searchAccount_Receipt2.DocCode, Convert.ToDateTime(searchAccount_Receipt2.TranDate), Convert.ToDateTime(searchAccount_Receipt2.DateDue), searchAccount_Receipt2.BranchName);
			}
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["SN"].Width = 50;
		gridEX1.RootTable.RowHeight = 28;
		gridEX1.RootTable.Columns["SN"].RightToLeft = RightToLeft.No;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Account.Check.Search.FrmSearchCheckIssued));
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
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
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
		base.Name = "FrmSearchCheckIssued";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


