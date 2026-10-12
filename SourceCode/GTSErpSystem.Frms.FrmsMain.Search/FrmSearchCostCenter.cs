using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Account;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.FrmsMain.Search;

public class FrmSearchCostCenter : XtraForm
{
	private Calss_CostCenter Center = new Calss_CostCenter();

	public int CustCode;

	public int Account_Number;

	public string AccountName;

	private int _ProjectId;

	private IContainer components = null;

	private GroupBox groupBox1;

	public GridEX gridEX1;

	private GroupBox groupBox2;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	public FrmSearchCostCenter(int projectId = 1)
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
		_ProjectId = projectId;
		BindGrid();
	}

	public void BindGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("التسلسل", typeof(int));
			dataTable.Columns.Add("رقم الحساب", typeof(int));
			dataTable.Columns.Add("اسم الحساب", typeof(string));
			dataTable.Columns.Add("اول المدة مدين", typeof(decimal));
			dataTable.Columns.Add("اول المدة دائن", typeof(decimal));
			foreach (Select_SearchAccountCostCenterByProject_Result item in Center.GetCostCentersByProject(_ProjectId))
			{
				dataTable.Rows.Add(item.SN, item.CostCentersID, item.CostCentersName, item.Priv_Debit, item.Priv_Credit);
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
		dataTable2.Columns.Add("Account Number", typeof(int));
		dataTable2.Columns.Add("Account Name", typeof(string));
		dataTable2.Columns.Add("Depit", typeof(decimal));
		dataTable2.Columns.Add("Credit", typeof(decimal));
		foreach (Select_SearchAccountCostCenterByProject_Result item2 in Center.GetCostCentersByProject(_ProjectId))
		{
			dataTable2.Rows.Add(item2.SN, item2.CostCentersID, item2.CostCentersName, item2.Priv_Debit, item2.Priv_Credit);
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["SN"].Width = 50;
		gridEX1.RootTable.RowHeight = 28;
		gridEX1.RootTable.Columns["SN"].RightToLeft = RightToLeft.No;
	}

	private void BtnNext_Click(object sender, EventArgs e)
	{
		try
		{
			int position = gridEX1.SelectedItems[0].Position;
			CustCode = Convert.ToInt32(gridEX1.GetRow(position).Cells[1].Text);
			AccountName = Convert.ToString(gridEX1.GetRow(position).Cells[2].Text);
			Close();
		}
		catch
		{
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void gridEX1_DoubleClick(object sender, EventArgs e)
	{
		try
		{
			int position = gridEX1.SelectedItems[0].Position;
			CustCode = Convert.ToInt32(gridEX1.GetRow(position).Cells[1].Text);
			AccountName = Convert.ToString(gridEX1.GetRow(position).Cells[2].Text);
			Close();
		}
		catch
		{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.Search.FrmSearchCostCenter));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.gridEX1);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Location = new System.Drawing.Point(10, 10);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Size = new System.Drawing.Size(617, 376);
		this.groupBox1.TabIndex = 4;
		this.groupBox1.TabStop = false;
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.AlternatingRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.gridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
		this.gridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
		this.gridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
		this.gridEX1.FilterRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
		this.gridEX1.FocusCellFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FocusCellFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Font = new System.Drawing.Font("Tahoma", 10f, System.Drawing.FontStyle.Bold);
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
		this.gridEX1.Location = new System.Drawing.Point(11, 18);
		this.gridEX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
		this.gridEX1.Size = new System.Drawing.Size(595, 293);
		this.gridEX1.TabIndex = 12;
		this.gridEX1.TableHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.DoubleClick += new System.EventHandler(gridEX1_DoubleClick);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Location = new System.Drawing.Point(5, 315);
		this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox2.Size = new System.Drawing.Size(607, 56);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(494, 19);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(107, 32);
		this.BtnExit.TabIndex = 6;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(6, 19);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(107, 32);
		this.BtnSave.TabIndex = 1;
		this.BtnSave.Text = "استمرار";
		this.BtnSave.Click += new System.EventHandler(BtnNext_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(638, 396);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		base.MaximizeBox = false;
		base.Name = "FrmSearchCostCenter";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "مستكشف مراكز التكلفة";
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


