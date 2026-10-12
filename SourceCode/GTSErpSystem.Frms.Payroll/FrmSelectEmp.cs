using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using GTSErpSystem.DataSetRep;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Payroll;

public class FrmSelectEmp : XtraForm
{
	private DataTable datatable = new DataTable();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private int Code;

	private DateTime Date;

	private string fileNameAR = Application.StartupPath + "\\Layout\\SelectEmp.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\SelectEmp.xml";

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	public GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn9;

	private GridColumn gridColumn2;

	private GridColumn gridColumn29;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private RepositoryItemTextEdit repositoryItemTextEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit2;

	private RepositoryItemDateEdit repositoryItemDateEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit3;

	private RepositoryItemPictureEdit repositoryItemPictureEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private RepositoryItemComboBox repositoryItemComboBox1;

	private RepositoryItemLookUpEdit repositoryItemLookUpEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonBankBox;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

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

	private SimpleButton BtnPrint;

	private GridColumn gridColumn1;

	private SimpleButton BtnSelectAll;

	private SimpleButton BtnDontSelect;

	private CheckBox CkPrintS;

	private CheckBox CkPrint;

	public System.Windows.Forms.ComboBox comboPrintA4;

	private Label label1;

	public FrmSelectEmp(DataTable data, int ID, DateTime date)
	{
		InitializeComponent();
		SetInitialRow();
		FillDataFromSource(data);
		Code = ID;
		Date = date;
		GetPrinters();
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("ظ…", typeof(int));
		datatable.Columns.Add("AccountID", typeof(string));
		datatable.Columns.Add("EmpName", typeof(string));
		datatable.Columns.Add("Select", typeof(bool));
	}

	private void FillDataFromSource(DataTable data)
	{
		int num = 1;
		foreach (DataRow row in data.Rows)
		{
			DataRow dataRow2 = datatable.NewRow();
			dataRow2["ظ…"] = num++;
			dataRow2["AccountID"] = row["رقم الحساب"];
			dataRow2["EmpName"] = row["اسم الموظف"];
			dataRow2["Select"] = false;
			datatable.Rows.Add(dataRow2);
		}
		gridControl1.DataSource = datatable;
	}

	public void Print()
	{
		using SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.Tables["TableEmpPayroll"].Clear();
		SqlCommand sqlCommand = new SqlCommand("Print_Emp_Payroll", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.AddWithValue("@ID", Code);
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("EmpID", typeof(int));
		EnumerableRowCollection<DataRow> enumerableRowCollection = from r in datatable.AsEnumerable()
			where r.Field<bool>("Select")
			select r;
		foreach (DataRow item in enumerableRowCollection)
		{
			if (int.TryParse(item["AccountID"].ToString(), out var result))
			{
				dataTable.Rows.Add(result);
			}
		}
		SqlParameter sqlParameter = sqlCommand.Parameters.AddWithValue("@EmpIDs", dataTable);
		sqlParameter.SqlDbType = SqlDbType.Structured;
		sqlParameter.TypeName = "dbo.IntList";
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlDataAdapter.Fill(dataSetALL.TableEmpPayroll);
		sqlConnection.Close();
		if (dataSetALL.Tables["TableEmpPayroll"].Rows.Count > 0)
		{
			foreach (DataRow row in dataSetALL.TableEmpPayroll.Rows)
			{
				DataTable dataTable2 = dataSetALL.TableEmpPayroll.Clone();
				dataTable2.ImportRow(row);
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintEmpPayrollByEmp.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintEmpPayrollByEmp.mrt");
				}
				stiReport.RegData("TableEmpPayroll", dataTable2);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				CultureInfo cultureInfo = new CultureInfo("ar-EG");
				stiReport["Date"] = Date.ToString("yyyy/MMMM", cultureInfo);
				if (CkPrintS.Checked)
				{
					stiReport.Show(dialogForm: true);
					stiReport.Render();
				}
				if (CkPrint.Checked)
				{
					PrinterSettings printerSettings = new PrinterSettings();
					printerSettings.PrinterName = comboPrintA4.Text;
					printerSettings.DefaultPageSettings.PaperSize = new PaperSize("Custom", 80, 3276);
					stiReport.Print(showPrintDialog: false, printerSettings);
				}
			}
			return;
		}
		MessageBox.Show("عفوا، لا توجد بيانات للعرض. تأكد من تحديد الموظفين بشكل صحيح.", "مستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}

	public void GetPrinters()
	{
		Dictionary<int, string> dictionary = new Dictionary<int, string>();
		int num = 1;
		foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
		{
			dictionary.Add(num, installedPrinter);
			num++;
		}
		comboPrintA4.DataSource = dictionary.ToList();
		comboPrintA4.DisplayMember = "value";
		comboPrintA4.ValueMember = "key";
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void BtnSelectAll_Click(object sender, EventArgs e)
	{
		foreach (DataRow row in datatable.Rows)
		{
			row["Select"] = true;
		}
		gridControl1.DataSource = datatable;
	}

	private void BtnDontSelect_Click(object sender, EventArgs e)
	{
		foreach (DataRow row in datatable.Rows)
		{
			row["Select"] = false;
		}
		gridControl1.DataSource = datatable;
	}

	private void FrmSelectEmp_Load(object sender, EventArgs e)
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

	private void FrmSelectEmp_FormClosing(object sender, FormClosingEventArgs e)
	{
		gridControl1.MainView.SaveLayoutToXml(fileNameAR);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Payroll.FrmSelectEmp));
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions5 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions6 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled6 = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
		this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
		this.repositoryItemButtonBankBox = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
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
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSelectAll = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDontSelect = new DevExpress.XtraEditors.SimpleButton();
		this.CkPrintS = new System.Windows.Forms.CheckBox();
		this.CkPrint = new System.Windows.Forms.CheckBox();
		this.comboPrintA4 = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemComboBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonBankBox).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.gridControl1);
		this.groupBox1.Location = new System.Drawing.Point(5, 52);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(658, 415);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.gridControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.gridControl1.Font = new System.Drawing.Font("Tahoma", 10.2f);
		this.gridControl1.Location = new System.Drawing.Point(6, 22);
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[13]
		{
			this.repositoryItemButtonEdit4, this.repositoryItemButtonEdit6, this.repositoryItemTextEdit1, this.repositoryItemTextEdit2, this.repositoryItemDateEdit1, this.repositoryItemTextEdit3, this.repositoryItemPictureEdit1, this.repositoryItemButtonEdit5, this.repositoryItemButtonEditCodeSearch, this.repositoryItemComboBox1,
			this.repositoryItemLookUpEdit1, this.repositoryItemButtonBankBox, this.repositoryItemButtonEdit1
		});
		this.gridControl1.Size = new System.Drawing.Size(649, 387);
		this.gridControl1.TabIndex = 9;
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.FilterPanel.BackColor = System.Drawing.Color.Gray;
		this.gridView1.Appearance.FilterPanel.Options.UseBackColor = true;
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[4] { this.gridColumn9, this.gridColumn2, this.gridColumn29, this.gridColumn1 });
		this.gridView1.DetailHeight = 284;
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.Name = "gridView1";
		this.gridView1.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.False;
		this.gridView1.OptionsBehavior.KeepGroupExpandedOnSorting = false;
		this.gridView1.OptionsCustomization.AllowSort = false;
		this.gridView1.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
		this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
		this.gridView1.OptionsFilter.AllowFilterEditor = false;
		this.gridView1.OptionsFilter.AllowFilterIncrementalSearch = false;
		this.gridView1.OptionsFilter.AllowMRUFilterList = false;
		this.gridView1.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = false;
		this.gridView1.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = false;
		this.gridView1.OptionsMenu.ShowGroupSortSummaryItems = false;
		this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
		this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
		this.gridView1.OptionsPrint.EnableAppearanceOddRow = true;
		this.gridView1.OptionsPrint.PrintFilterInfo = true;
		this.gridView1.OptionsPrint.PrintPreview = true;
		this.gridView1.OptionsView.RowAutoHeight = true;
		this.gridView1.OptionsView.ShowAutoFilterRow = true;
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.Caption = "ظ…";
		this.gridColumn9.FieldName = "ظ…";
		this.gridColumn9.MinWidth = 17;
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn9.Visible = true;
		this.gridColumn9.VisibleIndex = 0;
		this.gridColumn9.Width = 40;
		this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.Caption = "اسم الموظف";
		this.gridColumn2.FieldName = "EmpName";
		this.gridColumn2.MinWidth = 17;
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.gridColumn2.OptionsColumn.AllowFocus = false;
		this.gridColumn2.Visible = true;
		this.gridColumn2.VisibleIndex = 2;
		this.gridColumn2.Width = 178;
		this.gridColumn29.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn29.AppearanceCell.Options.UseFont = true;
		this.gridColumn29.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn29.AppearanceHeader.Options.UseFont = true;
		this.gridColumn29.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.Caption = "تحديد";
		this.gridColumn29.FieldName = "Select";
		this.gridColumn29.MinWidth = 25;
		this.gridColumn29.Name = "gridColumn29";
		this.gridColumn29.Visible = true;
		this.gridColumn29.VisibleIndex = 3;
		this.gridColumn29.Width = 94;
		this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn1.AppearanceCell.Options.UseFont = true;
		this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn1.AppearanceHeader.Options.UseFont = true;
		this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.Caption = "رقم الحساب";
		this.gridColumn1.FieldName = "AccountID";
		this.gridColumn1.MinWidth = 25;
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.OptionsColumn.AllowEdit = false;
		this.gridColumn1.OptionsColumn.AllowFocus = false;
		this.gridColumn1.Visible = true;
		this.gridColumn1.VisibleIndex = 1;
		this.gridColumn1.Width = 94;
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.repositoryItemButtonEdit4.AutoHeight = false;
		editorButtonImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions37.Image");
		editorButtonImageOptions.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit6.AutoHeight = false;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions38.Image");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "حذف", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemTextEdit1.AutoHeight = false;
		this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
		this.repositoryItemTextEdit2.AutoHeight = false;
		this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
		this.repositoryItemDateEdit1.AutoHeight = false;
		this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
		this.repositoryItemTextEdit3.AutoHeight = false;
		this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
		this.repositoryItemPictureEdit1.Appearance.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceDisabled.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceReadOnly.Options.UseImage = true;
		this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
		this.repositoryItemPictureEdit1.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
		this.repositoryItemButtonEdit5.AutoHeight = false;
		editorButtonImageOptions3.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions39.Image");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEditCodeSearch.AutoHeight = false;
		editorButtonImageOptions4.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions40.Image");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEditCodeSearch.Name = "repositoryItemButtonEditCodeSearch";
		this.repositoryItemComboBox1.AutoHeight = false;
		this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
		this.repositoryItemLookUpEdit1.AutoHeight = false;
		this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
		this.repositoryItemButtonBankBox.AutoHeight = false;
		editorButtonImageOptions5.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions41.Image");
		this.repositoryItemButtonBankBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonBankBox.Name = "repositoryItemButtonBankBox";
		editorButtonImageOptions6.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions42.Image");
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.cardView1.CardWidth = 171;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.DetailHeight = 284;
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		this.gridColumn10.Caption = "كود_الصنف";
		this.gridColumn10.MinWidth = 17;
		this.gridColumn10.Name = "gridColumn10";
		this.gridColumn10.Visible = true;
		this.gridColumn10.VisibleIndex = 0;
		this.gridColumn11.Caption = "أسم الصنف";
		this.gridColumn11.MinWidth = 17;
		this.gridColumn11.Name = "gridColumn11";
		this.gridColumn11.Visible = true;
		this.gridColumn11.VisibleIndex = 1;
		this.gridColumn12.Caption = "الوحدة";
		this.gridColumn12.MinWidth = 17;
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		this.gridColumn12.Visible = true;
		this.gridColumn12.VisibleIndex = 2;
		this.gridColumn13.Caption = "الكمية";
		this.gridColumn13.MinWidth = 17;
		this.gridColumn13.Name = "gridColumn13";
		this.gridColumn13.Visible = true;
		this.gridColumn13.VisibleIndex = 3;
		this.gridColumn14.Caption = "سعر الوحدة";
		this.gridColumn14.MinWidth = 17;
		this.gridColumn14.Name = "gridColumn14";
		this.gridColumn14.Visible = true;
		this.gridColumn14.VisibleIndex = 4;
		this.gridColumn15.Caption = "الاجمالى";
		this.gridColumn15.MinWidth = 17;
		this.gridColumn15.Name = "gridColumn15";
		this.gridColumn15.Visible = true;
		this.gridColumn15.VisibleIndex = 5;
		this.gridColumn16.Caption = "الضربية";
		this.gridColumn16.MinWidth = 17;
		this.gridColumn16.Name = "gridColumn16";
		this.gridColumn16.Visible = true;
		this.gridColumn16.VisibleIndex = 6;
		this.gridColumn17.Caption = "إجمالى المبلغ";
		this.gridColumn17.MinWidth = 17;
		this.gridColumn17.Name = "gridColumn17";
		this.gridColumn17.Visible = true;
		this.gridColumn17.VisibleIndex = 7;
		this.gridColumn18.Caption = "حذف الصنف";
		this.gridColumn18.MinWidth = 17;
		this.gridColumn18.Name = "gridColumn18";
		this.gridColumn18.Visible = true;
		this.gridColumn18.VisibleIndex = 8;
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Location = new System.Drawing.Point(8, 473);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(655, 64);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(6, 17);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(5);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(625, 39);
		this.BtnPrint.TabIndex = 5;
		this.BtnPrint.Text = "طباعه ";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSelectAll.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSelectAll.ImageOptions.Image");
		this.BtnSelectAll.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSelectAll.Location = new System.Drawing.Point(5, 4);
		this.BtnSelectAll.Margin = new System.Windows.Forms.Padding(5);
		this.BtnSelectAll.Name = "BtnSelectAll";
		this.BtnSelectAll.Size = new System.Drawing.Size(116, 49);
		this.BtnSelectAll.TabIndex = 5;
		this.BtnSelectAll.Text = "تحديد االكل";
		this.BtnSelectAll.Click += new System.EventHandler(BtnSelectAll_Click);
		this.BtnDontSelect.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnDontSelect.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDontSelect.ImageOptions.Image");
		this.BtnDontSelect.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDontSelect.Location = new System.Drawing.Point(547, 4);
		this.BtnDontSelect.Margin = new System.Windows.Forms.Padding(5);
		this.BtnDontSelect.Name = "BtnDontSelect";
		this.BtnDontSelect.Size = new System.Drawing.Size(116, 49);
		this.BtnDontSelect.TabIndex = 6;
		this.BtnDontSelect.Text = "الغاء تحديد الكل";
		this.BtnDontSelect.Click += new System.EventHandler(BtnDontSelect_Click);
		this.CkPrintS.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CkPrintS.AutoSize = true;
		this.CkPrintS.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CkPrintS.ForeColor = System.Drawing.Color.Black;
		this.CkPrintS.Location = new System.Drawing.Point(131, 4);
		this.CkPrintS.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CkPrintS.Name = "CkPrintS";
		this.CkPrintS.Size = new System.Drawing.Size(140, 21);
		this.CkPrintS.TabIndex = 69;
		this.CkPrintS.Text = "معاينه قبل الطباعه ";
		this.CkPrintS.UseVisualStyleBackColor = true;
		this.CkPrint.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CkPrint.AutoSize = true;
		this.CkPrint.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CkPrint.ForeColor = System.Drawing.Color.Black;
		this.CkPrint.Location = new System.Drawing.Point(134, 32);
		this.CkPrint.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CkPrint.Name = "CkPrint";
		this.CkPrint.Size = new System.Drawing.Size(64, 21);
		this.CkPrint.TabIndex = 70;
		this.CkPrint.Text = "طباعه";
		this.CkPrint.UseVisualStyleBackColor = true;
		this.comboPrintA4.FormattingEnabled = true;
		this.comboPrintA4.Location = new System.Drawing.Point(292, 29);
		this.comboPrintA4.Name = "comboPrintA4";
		this.comboPrintA4.Size = new System.Drawing.Size(247, 24);
		this.comboPrintA4.TabIndex = 71;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(289, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(81, 18);
		this.label1.TabIndex = 72;
		this.label1.Text = "اختر الطابعه";
		base.Appearance.BackColor = System.Drawing.Color.White;
		base.Appearance.ForeColor = System.Drawing.Color.White;
		base.Appearance.Options.UseBackColor = true;
		base.Appearance.Options.UseForeColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(665, 548);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.comboPrintA4);
		base.Controls.Add(this.CkPrint);
		base.Controls.Add(this.CkPrintS);
		base.Controls.Add(this.BtnDontSelect);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.BtnSelectAll);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmSelectEmp";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تحديد الموظفين";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmSelectEmp_FormClosing);
		base.Load += new System.EventHandler(FrmSelectEmp_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemComboBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonBankBox).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


