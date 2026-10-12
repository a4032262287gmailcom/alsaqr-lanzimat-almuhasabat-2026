using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Transportation.SearchTransportation;

public class FrmSearchReceive : XtraForm
{
	private GTSdbEntities entities = new GTSdbEntities();

	public int ID;

	private int Sn = 1;

	private IContainer components = null;

	public GridEX gridEX1;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	public FrmSearchReceive()
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
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable = new DataTable("gridEX1");
		dataTable.Columns.Add("التسلسل", typeof(int));
		dataTable.Columns.Add("CustID", typeof(int));
		dataTable.Columns.Add("رقم السند", typeof(int));
		dataTable.Columns.Add("اسم العميل", typeof(string));
		dataTable.Columns.Add("رقم الجوال", typeof(string));
		dataTable.Columns.Add("رقم لوحة العميل", typeof(string));
		dataTable.Columns.Add("رقم الهيكل", typeof(string));
		dataTable.Columns.Add("التاريخ", typeof(string));
		dataTable.Columns.Add("جهة الشحن ", typeof(string));
		dataTable.Columns.Add("جهة الوصول", typeof(string));
		dataTable.Columns.Add("اسم السائق", typeof(string));
		dataTable.Columns.Add("رقم لوحة السائق", typeof(string));
		dataTable.Columns.Add("الصافي", typeof(string));
		dataTable.Columns.Add("المدفوع", typeof(string));
		dataTable.Columns.Add("المتبقي", typeof(string));
		dataTable.Columns.Add("تم التسليم", typeof(string));
		foreach (Select_Car_Receive_Result item in entities.Select_Car_Receive(false).ToList())
		{
			dataTable.Rows.Add(Sn, item.ID, item.ID, item.CustomerName, item.CustomerPhone, item.Car_PlateNumber, item.Car_bodynumber, item.Order_Date, item.Order_Uploader, item.Order_Destination, item.DriverName, item.Driver_PlateNumber, item.SafyAmount, item.CashAmount, item.LastCashAmount, item.IsDone);
			Sn++;
		}
		gridEX1.SetDataBinding(dataTable, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["التسلسل"].Width = 50;
		gridEX1.RootTable.RowHeight = 28;
		gridEX1.RootTable.Columns["التسلسل"].RightToLeft = RightToLeft.No;
		gridEX1.RootTable.Columns["CustID"].Visible = false;
	}

	private void BtnNext_Click(object sender, EventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		ID = Convert.ToInt32(gridEX1.GetRow(position).Cells[1].Text);
		Close();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void gridEX1_DoubleClick(object sender, EventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		ID = Convert.ToInt32(gridEX1.GetRow(position).Cells[1].Text);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Transportation.SearchTransportation.FrmSearchReceive));
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.AlternatingRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
		this.gridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
		this.gridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
		this.gridEX1.FilterRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
		this.gridEX1.FocusCellFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FocusCellFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Font = new System.Drawing.Font("Tahoma", 10f);
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
		this.gridEX1.Location = new System.Drawing.Point(6, 21);
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
		this.gridEX1.Size = new System.Drawing.Size(932, 513);
		this.gridEX1.TabIndex = 12;
		this.gridEX1.TableHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.DoubleClick += new System.EventHandler(gridEX1_DoubleClick);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.gridEX1);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(944, 615);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Location = new System.Drawing.Point(6, 540);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(932, 69);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(800, 23);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(125, 39);
		this.BtnExit.TabIndex = 6;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(7, 23);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(125, 39);
		this.BtnSave.TabIndex = 1;
		this.BtnSave.Text = "استمرار";
		this.BtnSave.Click += new System.EventHandler(BtnNext_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(968, 639);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmSearchReceive";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "مستكشف الإستلامات";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


