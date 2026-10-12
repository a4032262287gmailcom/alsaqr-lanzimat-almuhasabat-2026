using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Cust_Sup.Search;

public class FrmSearchReservation : XtraForm
{
	public int CustCode;

	public string CusName;

	public int CustID;

	private int Sn = 1;

	private IContainer components = null;

	private GroupBox groupBox1;

	public GridEX gridEX1;

	private GroupBox groupBox2;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	private GroupBox groupBox3;

	private SimpleButton BtnSearch;

	private DateEdit TxtDate2;

	private Label label1;

	private DateEdit TxtDate1;

	private Label label21;

	private Label label31;

	private System.Windows.Forms.ComboBox CBType;

	public FrmSearchReservation()
	{
		Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
		InitializeComponent();
		TxtDate1.DateTime = DateTime.Now;
		TxtDate2.DateTime = DateTime.Now.AddMonths(1);
		BindDropDawn();
		BindReport();
	}

	private void BindDropDawn()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("ID", typeof(int));
		dataTable.Columns.Add("Name", typeof(string));
		DataRow dataRow = dataTable.NewRow();
		dataRow.ItemArray = new object[2] { 0, "تاريخ السند" };
		dataTable.Rows.InsertAt(dataRow, 0);
		dataRow = dataTable.NewRow();
		dataRow.ItemArray = new object[2] { 1, "تاريخ البروفة" };
		dataTable.Rows.InsertAt(dataRow, 0);
		dataRow = dataTable.NewRow();
		dataRow.ItemArray = new object[2] { 2, "تاريخ الاستلام" };
		dataTable.Rows.InsertAt(dataRow, 0);
		dataRow = dataTable.NewRow();
		dataRow.ItemArray = new object[2] { 3, "تاريخ المناسبة" };
		dataTable.Rows.InsertAt(dataRow, 0);
		dataRow = dataTable.NewRow();
		dataRow.ItemArray = new object[2] { 4, "تاريخ الاسترجاع" };
		dataTable.Rows.InsertAt(dataRow, 0);
		CBType.ValueMember = "ID";
		CBType.DisplayMember = "Name";
		CBType.DataSource = dataTable;
		CBType.SelectedValue = 0;
	}

	private void BindReport()
	{
		if (TxtDate1.DateTime > TxtDate2.DateTime)
		{
			MessageBox.Show("لا يمكن ان يكون التاريخ الاول اقل من التاريخ الثاني", "مستكشف التقرير", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			return;
		}
		DataTable dataTable = new DataTable();
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
		SqlCommand sqlCommand = new SqlCommand("GetReport_Order_Reservation", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlCommand.Parameters.AddWithValue("@TypeID", Convert.ToInt32(CBType.SelectedValue));
		sqlCommand.Parameters.AddWithValue("@Date1", Convert.ToDateTime(TxtDate1.Text));
		sqlCommand.Parameters.AddWithValue("@Date2", Convert.ToDateTime(TxtDate2.Text));
		sqlDataAdapter.Fill(dataTable);
		sqlConnection.Close();
		gridEX1.SetDataBinding(dataTable, "");
		gridEX1.RetrieveStructure();
		gridEX1.ColumnAutoSizeMode = ColumnAutoSizeMode.Default;
	}

	private void BtnNext_Click(object sender, EventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		CustCode = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
		Close();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void gridEX1_DoubleClick(object sender, EventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		CustCode = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
		Close();
	}

	private void BtnSearch_Click(object sender, EventArgs e)
	{
		BindReport();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Cust_Sup.Search.FrmSearchReservation));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label31 = new System.Windows.Forms.Label();
		this.CBType = new System.Windows.Forms.ComboBox();
		this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
		this.TxtDate2 = new DevExpress.XtraEditors.DateEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtDate1 = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.gridEX1);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Location = new System.Drawing.Point(12, 125);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(944, 502);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.AlternatingRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridEX1.ColumnAutoResize = true;
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
		this.gridEX1.Size = new System.Drawing.Size(932, 400);
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
		this.groupBox2.Location = new System.Drawing.Point(6, 427);
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
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.label31);
		this.groupBox3.Controls.Add(this.CBType);
		this.groupBox3.Controls.Add(this.BtnSearch);
		this.groupBox3.Controls.Add(this.TxtDate2);
		this.groupBox3.Controls.Add(this.label1);
		this.groupBox3.Controls.Add(this.TxtDate1);
		this.groupBox3.Controls.Add(this.label21);
		this.groupBox3.Location = new System.Drawing.Point(13, 13);
		this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox3.Size = new System.Drawing.Size(940, 105);
		this.groupBox3.TabIndex = 5;
		this.groupBox3.TabStop = false;
		this.label31.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label31.AutoSize = true;
		this.label31.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label31.Location = new System.Drawing.Point(434, 26);
		this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(100, 21);
		this.label31.TabIndex = 201;
		this.label31.Text = "طريقة البحث";
		this.CBType.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.CBType.FormattingEnabled = true;
		this.CBType.Location = new System.Drawing.Point(210, 23);
		this.CBType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBType.Name = "CBType";
		this.CBType.Size = new System.Drawing.Size(218, 27);
		this.CBType.TabIndex = 200;
		this.BtnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSearch.Appearance.Options.UseFont = true;
		this.BtnSearch.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
		this.BtnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSearch.Location = new System.Drawing.Point(7, 25);
		this.BtnSearch.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSearch.Name = "BtnSearch";
		this.BtnSearch.Size = new System.Drawing.Size(182, 57);
		this.BtnSearch.TabIndex = 199;
		this.BtnSearch.Text = "بحث Search";
		this.BtnSearch.Click += new System.EventHandler(BtnSearch_Click);
		this.TxtDate2.EditValue = null;
		this.TxtDate2.Location = new System.Drawing.Point(551, 56);
		this.TxtDate2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDate2.Name = "TxtDate2";
		this.TxtDate2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDate2.Properties.Appearance.Options.UseFont = true;
		this.TxtDate2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate2.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate2.Properties.MaskSettings.Set("mask", "yyyy/MM/dd");
		this.TxtDate2.Properties.TodayDate = new System.DateTime(2019, 6, 17, 21, 19, 56, 0);
		this.TxtDate2.Size = new System.Drawing.Size(288, 26);
		this.TxtDate2.TabIndex = 196;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label1.Location = new System.Drawing.Point(845, 59);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(69, 19);
		this.label1.TabIndex = 198;
		this.label1.Text = "الي تاريخ";
		this.TxtDate1.EditValue = null;
		this.TxtDate1.Location = new System.Drawing.Point(551, 23);
		this.TxtDate1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDate1.Name = "TxtDate1";
		this.TxtDate1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDate1.Properties.Appearance.Options.UseFont = true;
		this.TxtDate1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate1.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate1.Properties.MaskSettings.Set("mask", "yyyy/MM/dd");
		this.TxtDate1.Size = new System.Drawing.Size(288, 26);
		this.TxtDate1.TabIndex = 195;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label21.Location = new System.Drawing.Point(845, 26);
		this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(64, 19);
		this.label21.TabIndex = 197;
		this.label21.Text = "من تاريخ";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(968, 639);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmSearchReservation";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "مستكشف الحجوزات";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


