using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Threading;
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
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Account.Search;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.FrmsMain.Search;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Account;

public class FrmPaymentBig : XtraForm
{
	private DataTable datatable = new DataTable();

	private Class_Receipts RCP = new Class_Receipts();

	private Class_DailyRestrictions Daily = new Class_DailyRestrictions();

	private Class_DailyRestrictionsTest DailyTemp = new Class_DailyRestrictionsTest();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private ClassProject Project = new ClassProject();

	private Class_AccountCard ACT = new Class_AccountCard();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private static int PrintID;

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private int tafqitID = LoginDetails.TafqitID;

	public string txtWordAr;

	public string txtWordEn;

	public decimal DepitAll;

	public decimal CreditAll;

	private int DecimalNum = LoginDetails.DecimalNum;

	private int ProjectId = 0;

	private string fileNameAR = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AlSaqr Accounting System", "Layout", "FrmPaymentBigAR.xml");

	private string fileNameEN = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AlSaqr Accounting System", "Layout", "FrmPaymentBigEN.xml");

	private IContainer components = null;

	private ToolStripSeparator toolStripSeparator3;

	private RepositoryItemTextEdit repositoryItemTextEdit3;

	private RepositoryItemPictureEdit repositoryItemPictureEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private RepositoryItemComboBox repositoryItemComboBox1;

	private RepositoryItemLookUpEdit repositoryItemLookUpEdit1;

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

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn9;

	private GridColumn gridColumn1;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private GridColumn gridColumn2;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private RepositoryItemTextEdit repositoryItemTextEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit2;

	private RepositoryItemDateEdit repositoryItemDateEdit1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem MenuNew;

	private ToolStripMenuItem MenuSave;

	private ToolStripMenuItem MenuEdit;

	private ToolStripMenuItem MenuDelete;

	private ToolStripMenuItem RecoveredMenuItem1;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuSearchItem;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem MenuPurchOrder;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem RecoveredMenuItem2;

	private ToolStripMenuItem MenuExit;

	private GroupBox groupBox1;

	private Label label2;

	private ButtonEdit TxtAccountName;

	private Label label1;

	private TextBox TxtNotes;

	private Label label24;

	private TextEdit TxtNumNote;

	private DateEdit TxtDate;

	private Label label21;

	private Label label17;

	private ButtonEdit TxtCode;

	private GroupBox groupBox2;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private Label label3;

	private TextEdit TxtMan;

	private Label LblAccountName;

	private Label label4;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private GridColumn gridColumn4;

	private GridColumn gridColumn5;

	private RepositoryItemButtonEdit repositoryItemButtonProjectSearch;

	private GridColumn gridColumn19;

	private GridColumn gridColumn22;

	private GridColumn gridColumn23;

	public FrmPaymentBig()
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
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		BtnPrint.Enabled = false;
		TxtDate.DateTime = DateTime.Now;
		SetInitialRow();
		BindComboBox();
		gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
		base.KeyPreview = true;
		gridView1.Columns["المدين"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["المدين"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["الدائن"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["الدائن"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.FocusedRowHandle = 1;
		gridView1.FocusedColumn = gridView1.VisibleColumns[1];
		gridView1.ShowEditor();
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		Clear();
		if (LoginDetails.FrmOrderID != 0)
		{
			SearchFrm(LoginDetails.FrmOrderID);
		}
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 34);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 34);
		if (!flag)
		{
			BtnEdit.Visible = false;
		}
		if (!flag2)
		{
			BtnDelete.Visible = false;
		}
		if (LoginDetails.FrmOrderClose)
		{
			BtnNew.Visible = false;
			BtnSave.Visible = false;
			BtnEdit.Visible = false;
			BtnDelete.Visible = false;
			BtnExit.Visible = false;
			BtnPrint.Visible = false;
		}
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "رقم الحساب"));
		string text2 = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "اسم الحساب"));
		decimal num = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "المدين"));
		decimal num2 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "الدائن"));
		if (!(text == "") && !(text2 == "") && !(num + num2 == 0m))
		{
			return;
		}
		Select_SearchAccount_ByAccountNumber_Result select_SearchAccount_ByAccountNumber_Result = Daily.GetSearchAccountByAccountNum(text).FirstOrDefault();
		if (select_SearchAccount_ByAccountNumber_Result != null)
		{
			e.Valid = false;
			if (text == "")
			{
				MessageBox.Show("برجاء إدخال رقم الحساب");
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[1];
				gridView1.ShowEditor();
			}
			else if (text2 == "")
			{
				MessageBox.Show("برجاء إدخال أسم الحساب");
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[1];
				gridView1.ShowEditor();
			}
			else if (num + num2 == 0m)
			{
				MessageBox.Show("برجاء إدخال قيمة أكبر من الصفر");
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[2];
				gridView1.ShowEditor();
			}
		}
	}

	private void gridView1_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
	{
		e.ExceptionMode = ExceptionMode.NoAction;
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[3];
	}

	private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
	{
	}

	private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
	{
		Select_SearchAccountCostCenter_Result select_SearchAccountCostCenter_Result = (from x in Cost.GetSearchAccountCostCenter()
			where x.CostCentersID >= 0
			select x).FirstOrDefault();
		Select_SearchAccountProjects_Result select_SearchAccountProjects_Result = Project.GetSearchAccountProjects().FirstOrDefault();
		if (e.Column.FieldName == "رقم الحساب")
		{
			string text = Convert.ToString(e.Value);
			Select_SearchAccount_ByAccountNumber_Result select_SearchAccount_ByAccountNumber_Result = Daily.GetSearchAccountByAccountNum(text).FirstOrDefault();
			if (select_SearchAccount_ByAccountNumber_Result == null)
			{
				LoginDetails.AccountNameSearch = text;
				GTSErpSystem.Frms.Account.Search.FrmSearchAccount frmSearchAccount = new GTSErpSystem.Frms.Account.Search.FrmSearchAccount();
				frmSearchAccount.ShowDialog();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[0];
				gridView1.ShowEditor();
				GetAcctBySearch();
				LoginDetails.AccountNameSearch = "";
				return;
			}
			DataRow dataRow = null;
			dataRow = datatable.NewRow();
			dataRow["ظ…"] = 1;
			dataRow["AccountID"] = select_SearchAccount_ByAccountNumber_Result.ID;
			dataRow["رقم الحساب"] = select_SearchAccount_ByAccountNumber_Result.Account_No;
			dataRow["اسم الحساب"] = select_SearchAccount_ByAccountNumber_Result.Account_Name;
			dataRow["المدين"] = 0.0;
			dataRow["الدائن"] = 0.0;
			dataRow["البيان"] = string.Empty;
			dataRow["رقم المشروع"] = select_SearchAccountProjects_Result.ProjectID;
			dataRow["إسم المشروع"] = select_SearchAccountProjects_Result.ProjectName;
			dataRow["مركز التكلفة"] = select_SearchAccountCostCenter_Result.CostCentersID;
			dataRow["اسم مركز التكلفة"] = Cost.GetName(Convert.ToInt32(select_SearchAccountCostCenter_Result.CostCentersID));
			if (LoginDetails.BondTax)
			{
				dataRow["تفعيل الضريبه"] = true;
			}
			else
			{
				dataRow["تفعيل الضريبه"] = false;
			}
			dataRow["الضريبه"] = 0.0;
			dataRow["الاجمالي"] = 0.0;
			CheckFfistRow();
			datatable.Rows.Add(dataRow);
			insertnewrow();
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[0];
			gridView1.ShowEditor();
		}
		else if (e.Column.FieldName == "المدين")
		{
			string text2 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("ظ…");
			foreach (DataRow row in datatable.Rows)
			{
				if (!(row["ظ…"].ToString() == focusedRowCellDisplayText))
				{
					continue;
				}
				if (Convert.ToDecimal(row["المدين"]) > 0m)
				{
					row["الدائن"] = 0.0;
					if (row["تفعيل الضريبه"] != DBNull.Value && Convert.ToBoolean(row["تفعيل الضريبه"]))
					{
						decimal num = Math.Round(Convert.ToDecimal(LoginDetails.PerVat) / 100m, 2);
						row["الضريبه"] = Math.Round(Convert.ToDecimal(row["المدين"]) * num, 2);
						row["الاجمالي"] = Math.Round(Convert.ToDecimal(row["المدين"]) + Convert.ToDecimal(row["الضريبه"]), 2);
					}
					else
					{
						row["الضريبه"] = 0;
						row["الاجمالي"] = Math.Round(Convert.ToDecimal(row["المدين"]));
					}
				}
				break;
			}
			CheckFfistRow();
			insertnewrow();
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
		}
		else if (e.Column.FieldName == "الدائن")
		{
			string text3 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			foreach (DataRow row2 in datatable.Rows)
			{
				if (row2["ظ…"].ToString() == focusedRowCellDisplayText2)
				{
					if (Convert.ToDecimal(row2["الدائن"]) > 0m)
					{
						row2["المدين"] = 0.0;
					}
					break;
				}
			}
			CheckFfistRow();
			insertnewrow();
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
		}
		else if (e.Column.FieldName == "الاجمالي")
		{
			string text4 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			foreach (DataRow row3 in datatable.Rows)
			{
				if (row3["ظ…"].ToString() == focusedRowCellDisplayText3)
				{
					if (row3["تفعيل الضريبه"] != DBNull.Value && Convert.ToBoolean(row3["تفعيل الضريبه"]))
					{
						decimal num2 = Math.Round(Convert.ToDecimal(LoginDetails.PerVat) / 100m, 2);
						row3["المدين"] = Math.Round(Convert.ToDecimal(row3["الاجمالي"]) / (num2 + 1m), 2);
						row3["الضريبه"] = Math.Round(Convert.ToDecimal(row3["الاجمالي"]) - Convert.ToDecimal(row3["المدين"]), 2);
					}
					else
					{
						row3["الضريبه"] = 0;
						row3["المدين"] = Math.Round(Convert.ToDecimal(row3["الاجمالي"]));
					}
					break;
				}
			}
			CheckFfistRow();
			insertnewrow();
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
		}
		else
		{
			if (!(e.Column.FieldName == "مركز التكلفة"))
			{
				return;
			}
			string value = Convert.ToString(e.Value);
			try
			{
				int Num = Convert.ToInt32(value);
				string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				Select_SearchAccountCostCenter_Result select_SearchAccountCostCenter_Result2 = (from x in Cost.GetSearchAccountCostCenter()
					where x.CostCentersID == Num
					select x).FirstOrDefault();
				if (select_SearchAccountCostCenter_Result2 != null)
				{
					foreach (DataRow row4 in datatable.Rows)
					{
						if (row4["ظ…"].ToString() == focusedRowCellDisplayText4)
						{
							row4["مركز التكلفة"] = select_SearchAccountCostCenter_Result2.CostCentersID;
							row4["اسم مركز التكلفة"] = select_SearchAccountCostCenter_Result2.CostCentersName;
							break;
						}
					}
				}
				else
				{
					foreach (DataRow row5 in datatable.Rows)
					{
						if (row5["ظ…"].ToString() == focusedRowCellDisplayText4)
						{
							row5["مركز التكلفة"] = select_SearchAccountCostCenter_Result.CostCentersID;
							row5["اسم مركز التكلفة"] = Cost.GetName(Convert.ToInt32(select_SearchAccountCostCenter_Result.CostCentersID));
							break;
						}
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[5];
				gridView1.ShowEditor();
			}
			catch (Exception)
			{
			}
		}
		ArrangeDatatable();
	}

	private void gridView1_CellValueChanging(object sender, CellValueChangedEventArgs e)
	{
		if (!(e.Column.FieldName == "تفعيل الضريبه"))
		{
			return;
		}
		string text = Convert.ToString(e.Value);
		string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("ظ…");
		foreach (DataRow row in datatable.Rows)
		{
			if (row["ظ…"].ToString() == focusedRowCellDisplayText)
			{
				if (Convert.ToBoolean(row["تفعيل الضريبه"]))
				{
					row["تفعيل الضريبه"] = false;
				}
				else
				{
					row["تفعيل الضريبه"] = true;
				}
				if (row["تفعيل الضريبه"] != DBNull.Value && Convert.ToBoolean(row["تفعيل الضريبه"]))
				{
					decimal num = Math.Round(Convert.ToDecimal(LoginDetails.PerVat) / 100m, 2);
					row["الضريبه"] = Math.Round(Convert.ToDecimal(row["المدين"]) * num, 2);
					row["الاجمالي"] = Math.Round(Convert.ToDecimal(row["المدين"]) + Convert.ToDecimal(row["الضريبه"]), 2);
				}
				else
				{
					row["الضريبه"] = 0;
					row["الاجمالي"] = Math.Round(Convert.ToDecimal(row["المدين"]), 2);
				}
				break;
			}
		}
		CheckFfistRow();
		insertnewrow();
		gridView1.FocusedRowHandle = e.RowHandle;
		gridView1.FocusedColumn = gridView1.VisibleColumns[8];
		gridView1.ShowEditor();
	}

	private void gridView1_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
	{
		int num = e.Bounds.Height;
		Brush gradientBrush = e.Cache.GetGradientBrush(e.Bounds, Color.LightGreen, Color.FloralWhite, LinearGradientMode.Vertical);
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

	private void gridControl_ProcessGridKey(object sender, KeyEventArgs e)
	{
		GridControl gridControl = sender as GridControl;
		GridView gridView = gridControl.FocusedView as GridView;
		if (e.KeyData == Keys.Delete)
		{
			gridView.DeleteSelectedRows();
			e.Handled = true;
		}
	}

	private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
	{
	}

	private void repositoryItemButtonEdit2_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["ظ…"].ToString() == Convert.ToString(num))
			{
				if (row["اسم الحساب"].ToString() != "")
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

	private void repositoryItemButtonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["ظ…"].ToString() == Convert.ToString(num))
			{
				ProjectId = Convert.ToInt32(row["رقم المشروع"]);
				break;
			}
		}
		FrmSearchCostCenter frmSearchCostCenter = new FrmSearchCostCenter();
		frmSearchCostCenter.ShowDialog();
		foreach (DataRow row2 in datatable.Rows)
		{
			if (row2["ظ…"].ToString() == Convert.ToString(num))
			{
				row2["مركز التكلفة"] = frmSearchCostCenter.CustCode;
				row2["اسم مركز التكلفة"] = frmSearchCostCenter.AccountName;
			}
		}
		CheckFfistRow();
		insertnewrow();
		ArrangeDatatable();
		gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
		gridView1.FocusedColumn = gridView1.VisibleColumns[1];
		gridView1.ShowEditor();
		frmSearchCostCenter.Close();
	}

	private void repositoryItemButtonEditCodeSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchAccount();
	}

	private void repositoryItemButtonProjectSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchProjects frmSearchProjects = new FrmSearchProjects();
		frmSearchProjects.ShowDialog();
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["ظ…"].ToString() == Convert.ToString(num) && frmSearchProjects.Code > 0)
			{
				Select_SearchAccountCostCenterByProject_Result select_SearchAccountCostCenterByProject_Result = Cost.GetCostCentersByProject(frmSearchProjects.Code).FirstOrDefault();
				row["رقم المشروع"] = frmSearchProjects.Code;
				row["إسم المشروع"] = frmSearchProjects.ProjectName;
				ProjectId = frmSearchProjects.Code;
				row["مركز التكلفة"] = select_SearchAccountCostCenterByProject_Result.CostCentersID;
				row["اسم مركز التكلفة"] = select_SearchAccountCostCenterByProject_Result.CostCentersName;
			}
		}
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("ظ…", typeof(int));
		datatable.Columns.Add("AccountID", typeof(string));
		datatable.Columns.Add("رقم الحساب", typeof(string));
		datatable.Columns.Add("اسم الحساب", typeof(string));
		datatable.Columns.Add("المدين", typeof(decimal));
		datatable.Columns.Add("الدائن", typeof(decimal));
		datatable.Columns.Add("البيان", typeof(string));
		datatable.Columns.Add("رقم المشروع", typeof(int));
		datatable.Columns.Add("إسم المشروع", typeof(string));
		datatable.Columns.Add("مركز التكلفة", typeof(int));
		datatable.Columns.Add("اسم مركز التكلفة", typeof(string));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تفعيل الضريبه", typeof(bool));
		datatable.Columns.Add("الضريبه", typeof(decimal));
		datatable.Columns.Add("الاجمالي", typeof(decimal));
		Select_SearchAccountCostCenter_Result select_SearchAccountCostCenter_Result = (from x in Cost.GetSearchAccountCostCenter()
			where x.CostCentersID >= 0
			select x).FirstOrDefault();
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["ظ…"] = 1;
		dataRow["AccountID"] = string.Empty;
		dataRow["رقم الحساب"] = string.Empty;
		dataRow["اسم الحساب"] = string.Empty;
		dataRow["المدين"] = 0.0;
		dataRow["الدائن"] = 0.0;
		dataRow["البيان"] = string.Empty;
		dataRow["رقم المشروع"] = 0;
		dataRow["إسم المشروع"] = "";
		dataRow["مركز التكلفة"] = 0;
		dataRow["اسم مركز التكلفة"] = "";
		dataRow["مسح"] = 0;
		dataRow["الضريبه"] = 0.0;
		dataRow["تفعيل الضريبه"] = false;
		dataRow["الاجمالي"] = 0.0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridControl1.ProcessGridKey += gridControl_ProcessGridKey;
		gridView1.RowCellClick += gridView1_RowCellClick;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		gridView1.CustomDrawFooterCell += gridView1_CustomDrawFooterCell;
		gridView1.Columns["المدين"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.000}", "0")
		});
		gridView1.Columns["الدائن"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.000}", "0.")
		});
	}

	public void ArrangeDatatable()
	{
		int num = 1;
		foreach (DataRow row in datatable.Rows)
		{
			row["ظ…"] = num;
			num++;
		}
	}

	public void CheckFfistRow()
	{
		for (int num = datatable.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = datatable.Rows[num];
			if (dataRow["اسم الحساب"].ToString() == "" || dataRow["رقم الحساب"].ToString() == "0")
			{
				dataRow.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
		gridControl1.DataSource = datatable;
	}

	public void insertnewrow()
	{
		Select_SearchAccountCostCenter_Result select_SearchAccountCostCenter_Result = (from x in Cost.GetSearchAccountCostCenter()
			where x.CostCentersID >= 0
			select x).FirstOrDefault();
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["ظ…"] = 1;
		dataRow["AccountID"] = string.Empty;
		dataRow["رقم الحساب"] = string.Empty;
		dataRow["اسم الحساب"] = string.Empty;
		dataRow["المدين"] = 0.0;
		dataRow["الدائن"] = 0.0;
		dataRow["البيان"] = string.Empty;
		dataRow["رقم المشروع"] = 0;
		dataRow["إسم المشروع"] = "";
		dataRow["مركز التكلفة"] = 0;
		dataRow["اسم مركز التكلفة"] = "";
		dataRow["مسح"] = 0;
		dataRow["الضريبه"] = 0;
		if (LoginDetails.BondTax)
		{
			dataRow["تفعيل الضريبه"] = true;
		}
		else
		{
			dataRow["تفعيل الضريبه"] = false;
		}
		dataRow["الاجمالي"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
	}

	private void Clear()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BtnPrint.Enabled = false;
		datatable.Rows.Clear();
		insertnewrow();
		gridControl1.DataSource = datatable;
		TxtCode.Text = "";
		TxtNumNote.Text = "";
		TxtNotes.Text = "";
		TxtMan.Text = "";
		TxtAccountName.Text = "";
		LblAccountName.Text = "";
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
		TxtDate.DateTime = DateTime.Now;
		LblAccountName.Text = LoginDetails.Acc_CashBox.ToString();
		TxtAccountName.Text = RCP.GetAccountName(LoginDetails.Acc_CashBox);
	}

	private void NewFrm()
	{
		datatable.Rows.Clear();
		gridControl1.Enabled = true;
		gridView1.OptionsBehavior.Editable = true;
		Clear();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BtnPrint.Enabled = false;
		IsEnableTrue();
		TxtDate.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 34))
		{
			int num = 0;
			decimal vats = Convert.ToDecimal(datatable.Compute("SUM(الضريبه)", string.Empty));
			decimal value = Convert.ToDecimal(datatable.Compute("SUM(الاجمالي)", string.Empty));
			DepitAll = Convert.ToDecimal(value);
			decimal value2 = Convert.ToDecimal(datatable.Compute("SUM(الدائن)", string.Empty));
			CreditAll = Convert.ToDecimal(value2);
			decimal value3 = Convert.ToDecimal(datatable.Compute("SUM(المدين)", string.Empty));
			decimal num2 = Convert.ToDecimal(value3);
			if (num2 <= 0m)
			{
				MessageBox.Show("عفوا\u064b لايمكن ان يكون اجمالي المدين أقل من او يساوي الصفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
			else if (datatable.Rows.Count > 1)
			{
				if (TxtNotes.Text != "" && TxtAccountName.Text != "")
				{
					if (CBCostCenter.SelectedValue == null)
					{
						CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
					}
					CheckFfistRow();
					num = RCP.Insert(out var TRNCode, TxtNumNote.Text, 0, TxtMan.Text, TxtDate.DateTime, TxtNotes.Text, DepitAll, 3, 0);
					RCP.InsertItemsReceipts(Convert.ToInt32(LblAccountName.Text), datatable, CreditAll, DepitAll, "أيصال صرف متعدد رقم الإيصال  ( " + TRNCode + " )", Convert.ToInt32(CBCostCenter.SelectedValue), TRNCode, ISEdit: false, 3, 1, vats);
					RCP.InsertTran(out var _, TRNCode, TxtNumNote.Text, TxtDate.DateTime, TxtNotes.Text, 3);
					RCP.InsertItems(Convert.ToInt32(LblAccountName.Text), datatable, CreditAll, DepitAll, TxtNotes.Text, Convert.ToInt32(CBCostCenter.SelectedValue), TRNCode, ISEdit: false, 3, 1, "ضريبة القيمة المضافة لسند صرف مجمع رقم   ( " + TRNCode + " )");
					gridControl1.DataSource = datatable;
					gridView1.OptionsBehavior.Editable = false;
					if (num > 0)
					{
						PrintID = num;
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						BtnPrint.Enabled = true;
						IsEnableFalse();
						TxtCode.Text = TRNCode.ToString();
						switch (MessageBox.Show("تم حفظ بيانات ايصال الصرف المتعدد  بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
						{
						case DialogResult.Yes:
							Print();
							break;
						}
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات ايصال الصرف المتعدد رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else if (TxtAccountName.Text == "")
				{
					MessageBox.Show("عفوا\u064b من فضلك اختر رقم الحساب", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtAccountName.Focus();
				}
				else if (TxtNotes.Text == "")
				{
					MessageBox.Show("عفوا\u064b من فضلك اكتب شرح القيد", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtNotes.Focus();
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل حساب واحد على الاقل", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				gridControl1.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 34))
		{
			if (TxtDate.ReadOnly)
			{
				IsEnableTrue();
				BtnEdit.Text = "F5 حفظ";
				insertnewrow();
				ArrangeDatatable();
				gridView1.OptionsBehavior.Editable = true;
				return;
			}
			int num = 0;
			decimal vats = Convert.ToDecimal(datatable.Compute("SUM(الضريبه)", string.Empty));
			decimal value = Convert.ToDecimal(datatable.Compute("SUM(الاجمالي)", string.Empty));
			DepitAll = Convert.ToDecimal(value);
			decimal value2 = Convert.ToDecimal(datatable.Compute("SUM(الدائن)", string.Empty));
			CreditAll = Convert.ToDecimal(value2);
			decimal value3 = Convert.ToDecimal(datatable.Compute("SUM(المدين)", string.Empty));
			decimal num2 = Convert.ToDecimal(value3);
			if (num2 <= 0m)
			{
				MessageBox.Show("عفوا\u064b لايمكن ان يكون اجمالي المدين أقل من او يساوي الصفر", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
			else if (datatable.Rows.Count > 1)
			{
				if (TxtNotes.Text != "" && TxtAccountName.Text != "")
				{
					if (CBCostCenter.SelectedValue == null)
					{
						CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
					}
					ArrangeDatatable();
					CheckFfistRow();
					RCP.DeleteDetails(Convert.ToInt32(TxtCode.Text), 3);
					RCP.DeleteTranDetails(Convert.ToInt32(TxtCode.Text), 3);
					num = RCP.Update(Convert.ToInt32(TxtCode.Text), TxtNumNote.Text, 0, TxtMan.Text, TxtDate.DateTime, TxtNotes.Text, DepitAll, 3, 0);
					RCP.InsertItemsReceipts(Convert.ToInt32(LblAccountName.Text), datatable, CreditAll, DepitAll, "أيصال صرف متعدد رقم الإيصال  ( " + Convert.ToInt32(TxtCode.Text) + " ) معدلة", Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(TxtCode.Text), ISEdit: false, 3, 1, vats);
					RCP.UpdateTran(Convert.ToInt32(TxtCode.Text), TxtNumNote.Text, TxtDate.DateTime, TxtNotes.Text, 3);
					RCP.InsertItems(Convert.ToInt32(LblAccountName.Text), datatable, CreditAll, DepitAll, TxtNotes.Text, Convert.ToInt32(CBCostCenter.SelectedValue), Convert.ToInt32(TxtCode.Text), ISEdit: false, 3, 1, "ضريبة القيمة المضافة لسند صرف مجمع رقم   ( " + TxtCode.Text + " )");
					gridControl1.DataSource = datatable;
					gridView1.OptionsBehavior.Editable = false;
					if (num > 0)
					{
						PrintID = num;
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						BtnPrint.Enabled = true;
						BtnEdit.Text = "F6 تعديل";
						IsEnableFalse();
						switch (MessageBox.Show("تم تعديل بيانات ايصال الصرف المتعدد  بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
						{
						case DialogResult.Yes:
							Print();
							break;
						}
					}
					else
					{
						MessageBox.Show("تم تعديل بيانات ايصال الصرف المتعدد  رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else if (TxtAccountName.Text == "")
				{
					MessageBox.Show("عفوا\u064b من فضلك اختر رقم الحساب", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtAccountName.Focus();
				}
				else if (TxtNotes.Text == "")
				{
					MessageBox.Show("عفوا\u064b من فضلك اكتب شرح القيد", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtNotes.Focus();
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل حساب واحد على الاقل", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 34))
		{
			int num = 0;
			int num2 = 0;
			if (TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ايصال الصرف المتعدد  رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = RCP.DeleteDetails(Convert.ToInt32(TxtCode.Text), 3);
					num2 = RCP.Delete(Convert.ToInt32(TxtCode.Text), 3);
					num = RCP.DeleteTranDetails(Convert.ToInt32(TxtCode.Text), 3);
					num2 = RCP.DeleteTran(Convert.ToInt32(TxtCode.Text), 3);
					if (num > 0 && num2 > 0)
					{
						MessageBox.Show("تم حذف بيانات ايصال الصرف المتعدد  رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						gridView1.OptionsBehavior.Editable = true;
						IsEnableTrue();
						BtnPrint.Enabled = false;
						BtnDelete.Enabled = false;
						BtnEdit.Enabled = false;
						BtnSave.Enabled = true;
						insertnewrow();
						TxtDate.Focus();
						PrintID = 0;
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات ايصال الصرف المتعدد  رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات ايصال الصرف المتعدد  بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void SearchAccount()
	{
		LoginDetails.FrmName = "";
		GTSErpSystem.Frms.FrmsMain.Search.FrmSearchAccount frmSearchAccount = new GTSErpSystem.Frms.FrmsMain.Search.FrmSearchAccount();
		frmSearchAccount.ShowDialog();
		if (frmSearchAccount.Account_Number > 0)
		{
			Select_SearchAccountProjects_Result select_SearchAccountProjects_Result = Project.GetSearchAccountProjects().FirstOrDefault();
			if (select_SearchAccountProjects_Result != null)
			{
				Select_SearchAccountCostCenterByProject_Result select_SearchAccountCostCenterByProject_Result = Cost.GetCostCentersByProject(Convert.ToInt32(select_SearchAccountProjects_Result.ProjectID)).FirstOrDefault();
				if (select_SearchAccountCostCenterByProject_Result != null)
				{
					int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
					foreach (DataRow row in datatable.Rows)
					{
						if (row["ظ…"].ToString() == Convert.ToString(num))
						{
							row["ظ…"] = 1;
							row["AccountID"] = frmSearchAccount.CustCode;
							row["رقم الحساب"] = frmSearchAccount.Account_Number;
							row["اسم الحساب"] = frmSearchAccount.AccountName;
							row["المدين"] = 0.0;
							row["الدائن"] = 0.0;
							row["البيان"] = string.Empty;
							row["رقم المشروع"] = select_SearchAccountProjects_Result.ProjectID;
							row["إسم المشروع"] = select_SearchAccountProjects_Result.ProjectName;
							row["مركز التكلفة"] = select_SearchAccountCostCenterByProject_Result.CostCentersID;
							row["اسم مركز التكلفة"] = select_SearchAccountCostCenterByProject_Result.CostCentersName;
						}
					}
					CheckFfistRow();
					insertnewrow();
					ArrangeDatatable();
					gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[1];
					gridView1.ShowEditor();
					frmSearchAccount.Close();
				}
				else
				{
					MessageBox.Show("عفوا لا يوجد مراكز تكلفة تابعة للمشروع!!");
				}
			}
			else
			{
				MessageBox.Show("عفوا لا يوجد مشاريع!!");
			}
		}
		else
		{
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[1];
			gridView1.ShowEditor();
		}
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 34))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["RestrictionsTablePrint"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Account_Receipts2", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@TranTypeID", 3);
				sqlCommand.Parameters.AddWithValue("@ReferenceCode", Convert.ToInt32(TxtCode.Text));
				sqlCommand.Parameters.AddWithValue("@BranchID", Convert.ToInt32(LoginDetails.BranchID));
				sqlDataAdapter.Fill(dataSetALL.RestrictionsTablePrint);
				sqlConnection.Close();
				decimal value = Convert.ToDecimal(datatable.Compute("SUM(الاجمالي)", string.Empty));
				DepitAll = Convert.ToDecimal(value);
				ToWord toWord = new ToWord(Convert.ToDecimal(DepitAll), currencies[tafqitID]);
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					txtWordAr = toWord.ConvertToArabic();
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashReceiptsBig.mrt");
				}
				else
				{
					txtWordAr = toWord.ConvertToEnglish();
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCashReceiptsBig.mrt");
				}
				if (dataSetALL.Tables["RestrictionsTablePrint"].Rows.Count > 0)
				{
					stiReport.RegData(dataSetALL.RestrictionsTablePrint);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
					stiReport["Address"] = LoginDetails.Address;
					stiReport["Phone"] = LoginDetails.Phone;
					stiReport["Bank"] = LoginDetails.CompanyBank;
					stiReport["Date"] = TxtDate.DateTime;
					stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
					stiReport["Tafqit"] = txtWordAr;
					stiReport["NoteTran"] = TxtNotes.Text;
					stiReport["AddressFrm"] = "سند صرف متعدد";
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

	public void IsEnableTrue()
	{
		TxtNumNote.ReadOnly = false;
		TxtNotes.ReadOnly = false;
		TxtDate.ReadOnly = false;
		TxtMan.ReadOnly = false;
		CBCostCenter.Enabled = true;
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		TxtNumNote.ReadOnly = true;
		TxtNotes.ReadOnly = true;
		TxtDate.ReadOnly = true;
		TxtMan.ReadOnly = true;
		CBCostCenter.Enabled = false;
		TxtNumNote.BackColor = Color.White;
		TxtNotes.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
		TxtMan.BackColor = Color.White;
	}

	private void BindComboBox()
	{
		CBCostCenter.DataSource = Cost.GetSearchAccountCostCenter();
		CBCostCenter.DisplayMember = "CostCentersName";
		CBCostCenter.ValueMember = "CostCentersID";
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
	}

	private void Comb_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	public void GetDailyRestrictionsTest()
	{
		try
		{
			FrmSearchTranTemp frmSearchTranTemp = new FrmSearchTranTemp();
			frmSearchTranTemp.ShowDialog();
			if (frmSearchTranTemp.CustCode > 0)
			{
				int CodeId = frmSearchTranTemp.CustCode;
				Select_SearchAccountTranTemp_Result select_SearchAccountTranTemp_Result = (from x in DailyTemp.GetSearchTranTemp()
					where x.ID == CodeId
					select x).FirstOrDefault();
				TxtCode.Text = "";
				TxtNumNote.Text = select_SearchAccountTranTemp_Result.DocCode.ToString();
				TxtDate.Text = Convert.ToDateTime(select_SearchAccountTranTemp_Result.TranDate).ToShortDateString();
				TxtNotes.Text = select_SearchAccountTranTemp_Result.Note;
				datatable.Rows.Clear();
				List<Select_SearchAccountTranTempDetailsByCode_Result> searchTranTempDetails = DailyTemp.GetSearchTranTempDetails(CodeId);
				int num = 1;
				foreach (Select_SearchAccountTranTempDetailsByCode_Result item in searchTranTempDetails)
				{
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					dataRow["AccountID"] = CodeId;
					dataRow["رقم الحساب"] = item.Account_Sn;
					dataRow["اسم الحساب"] = (from x in ACT.GetAccounts()
						where x.Account_No == Convert.ToInt32(item.Account_Sn)
						select x).FirstOrDefault().Account_Name;
					dataRow["المدين"] = item.Debit;
					dataRow["الدائن"] = item.Credit;
					dataRow["البيان"] = item.TranDesc;
					dataRow["مركز التكلفة"] = item.CostCentersID;
					dataRow["اسم مركز التكلفة"] = Cost.GetName(Convert.ToInt32(item.CostCentersID));
					dataRow["مسح"] = 0;
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				PrintID = CodeId;
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = false;
				gridView1.OptionsBehavior.Editable = true;
				ArrangeDatatable();
				insertnewrow();
				IsEnableTrue();
			}
			else
			{
				TxtDate.Focus();
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	public void GetAcctBySearch()
	{
		if (LoginDetails.ItemSearchAll2 == null)
		{
			return;
		}
		if (LoginDetails.ItemSearchAll2.Count > 0)
		{
			Select_SearchAccountProjects_Result select_SearchAccountProjects_Result = Project.GetSearchAccountProjects().FirstOrDefault();
			Select_SearchAccountCostCenter_Result select_SearchAccountCostCenter_Result = (from x in Cost.GetSearchAccountCostCenter()
				where x.CostCentersID >= 0
				select x).FirstOrDefault();
			foreach (string item in LoginDetails.ItemSearchAll2)
			{
				string iD = item.ToString();
				Select_SearchAccount_ByAccountNumber_Result select_SearchAccount_ByAccountNumber_Result = Daily.GetSearchAccountByAccountNum(iD).FirstOrDefault();
				if (select_SearchAccount_ByAccountNumber_Result != null)
				{
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					dataRow["ظ…"] = 1;
					dataRow["AccountID"] = select_SearchAccount_ByAccountNumber_Result.ID;
					dataRow["رقم الحساب"] = select_SearchAccount_ByAccountNumber_Result.Account_No;
					dataRow["اسم الحساب"] = select_SearchAccount_ByAccountNumber_Result.Account_Name;
					dataRow["المدين"] = 0.0;
					dataRow["الدائن"] = 0.0;
					dataRow["البيان"] = string.Empty;
					dataRow["رقم المشروع"] = select_SearchAccountProjects_Result.ProjectID;
					dataRow["إسم المشروع"] = select_SearchAccountProjects_Result.ProjectName;
					dataRow["مركز التكلفة"] = select_SearchAccountCostCenter_Result.CostCentersID;
					dataRow["اسم مركز التكلفة"] = Cost.GetName(Convert.ToInt32(select_SearchAccountCostCenter_Result.CostCentersID));
					dataRow["تفعيل الضريبه"] = true;
					dataRow["الضريبه"] = 0.0;
					dataRow["الاجمالي"] = 0.0;
					CheckFfistRow();
					datatable.Rows.Add(dataRow);
				}
			}
			CheckFfistRow();
			insertnewrow();
			ArrangeDatatable();
			LoginDetails.ItemSearchAll2.Clear();
		}
		LoginDetails.ItemSearchAll2.Clear();
		LoginDetails.ISCheckSearchItem = true;
	}

	private void SearchFrm(int CodeId)
	{
		try
		{
			if (CodeId > 0)
			{
				Account_Receipts PurchacheDtalis = (from x in RCP.GetALLReceipts()
					where (x.ReferenceCode == CodeId) & (x.TranTypeID == 3)
					select x).FirstOrDefault();
				Account_ReceiptsDetails account_ReceiptsDetails = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex == 1)
					select x).FirstOrDefault();
				List<Account_ReceiptsDetails> list = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex >= 2)
					select x).ToList();
				TxtCode.Text = PurchacheDtalis.ReferenceCode.ToString();
				TxtNumNote.Text = PurchacheDtalis.DocCode.ToString();
				TxtDate.Text = Convert.ToDateTime(PurchacheDtalis.TranDate).ToShortDateString();
				TxtNotes.Text = PurchacheDtalis.Note;
				TxtMan.Text = PurchacheDtalis.ReceivedFrom;
				CBCostCenter.SelectedValue = account_ReceiptsDetails.CostCentersID;
				TxtAccountName.Text = RCP.GetAccountName(Convert.ToInt32(account_ReceiptsDetails.Account_Sn));
				LblAccountName.Text = account_ReceiptsDetails.Account_Sn.ToString();
				datatable.Rows.Clear();
				int num = 1;
				foreach (Account_ReceiptsDetails item in list)
				{
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					dataRow["AccountID"] = item.Account_Sn;
					dataRow["رقم الحساب"] = RCP.GetAccountNum(Convert.ToInt32(item.Account_Sn));
					dataRow["اسم الحساب"] = RCP.GetAccountName(Convert.ToInt32(item.Account_Sn));
					dataRow["المدين"] = item.Debit;
					dataRow["الدائن"] = item.Credit;
					dataRow["البيان"] = item.TranDesc;
					dataRow["مركز التكلفة"] = item.CostCentersID;
					dataRow["اسم مركز التكلفة"] = Cost.GetName(Convert.ToInt32(item.CostCentersID));
					dataRow["رقم المشروع"] = item.ProjectId;
					dataRow["إسم المشروع"] = Cost.GetNameProJect(Convert.ToInt32(item.ProjectId));
					dataRow["مسح"] = 0;
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				PrintID = CodeId;
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = true;
				BtnPrint.Enabled = true;
				gridView1.OptionsBehavior.Editable = false;
				ArrangeDatatable();
				IsEnableFalse();
			}
			else
			{
				TxtDate.Focus();
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
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchPaymentBig frmSearchPaymentBig = new FrmSearchPaymentBig();
			frmSearchPaymentBig.ShowDialog();
			if (frmSearchPaymentBig.CustCode > 0)
			{
				int CodeId = frmSearchPaymentBig.CustCode;
				Account_Receipts PurchacheDtalis = (from x in RCP.GetALLReceipts()
					where (x.ReferenceCode == CodeId) & (x.TranTypeID == 3)
					select x).FirstOrDefault();
				Account_ReceiptsDetails account_ReceiptsDetails = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex == 1)
					select x).FirstOrDefault();
				List<Account_ReceiptsDetails> list = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex >= 2)
					select x).ToList();
				TxtCode.Text = PurchacheDtalis.ReferenceCode.ToString();
				TxtNumNote.Text = PurchacheDtalis.DocCode.ToString();
				TxtDate.Text = Convert.ToDateTime(PurchacheDtalis.TranDate).ToShortDateString();
				TxtNotes.Text = PurchacheDtalis.Note;
				TxtMan.Text = PurchacheDtalis.ReceivedFrom;
				CBCostCenter.SelectedValue = account_ReceiptsDetails.CostCentersID;
				TxtAccountName.Text = RCP.GetAccountName(Convert.ToInt32(account_ReceiptsDetails.Account_Sn));
				LblAccountName.Text = account_ReceiptsDetails.Account_Sn.ToString();
				datatable.Rows.Clear();
				int num = 1;
				foreach (Account_ReceiptsDetails item in list)
				{
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					if (item.Account_Sn != LoginDetails.Acc_VatNum)
					{
						dataRow["AccountID"] = item.Account_Sn;
						dataRow["رقم الحساب"] = RCP.GetAccountNum(Convert.ToInt32(item.Account_Sn));
						dataRow["اسم الحساب"] = RCP.GetAccountName(Convert.ToInt32(item.Account_Sn));
						dataRow["المدين"] = item.Debit;
						dataRow["الدائن"] = item.Credit;
						dataRow["البيان"] = item.TranDesc;
						dataRow["تفعيل الضريبه"] = item.IsVat;
						dataRow["الضريبه"] = item.Vat;
						dataRow["الاجمالي"] = item.Total;
						dataRow["مركز التكلفة"] = item.CostCentersID;
						dataRow["اسم مركز التكلفة"] = Cost.GetName(Convert.ToInt32(item.CostCentersID));
						dataRow["رقم المشروع"] = item.ProjectId;
						dataRow["إسم المشروع"] = Cost.GetNameProJect(Convert.ToInt32(item.ProjectId));
						dataRow["مسح"] = 0;
						datatable.Rows.Add(dataRow);
						num++;
					}
				}
				gridControl1.DataSource = datatable;
				PrintID = CodeId;
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = true;
				BtnPrint.Enabled = true;
				gridView1.OptionsBehavior.Editable = false;
				ArrangeDatatable();
				IsEnableFalse();
			}
			else
			{
				TxtDate.Focus();
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void MenuSearchItem_Click(object sender, EventArgs e)
	{
		SearchAccount();
	}

	private void MenuPurchOrder_Click(object sender, EventArgs e)
	{
		GetDailyRestrictionsTest();
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
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

	private void Frms_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F3")
		{
			if (gridView1.OptionsBehavior.Editable)
			{
				SearchAccount();
			}
		}
		else if (e.KeyCode.ToString() == "F4")
		{
			NewFrm();
		}
		else if (e.KeyCode.ToString() == "F5")
		{
			if (BtnSave.Enabled)
			{
				Save();
			}
		}
		else if (e.KeyCode.ToString() == "F6")
		{
			if (BtnEdit.Enabled)
			{
				Edit();
			}
		}
		else if (e.KeyCode.ToString() == "F7")
		{
			if (BtnDelete.Enabled)
			{
				Delete();
			}
		}
		else if (e.KeyCode.ToString() == "F9")
		{
			GetDailyRestrictionsTest();
		}
		else
		{
			if (e.KeyCode.ToString() == "F12")
			{
				return;
			}
			if (e.KeyCode == Keys.P && e.Modifiers == Keys.Control)
			{
				if (BtnDelete.Enabled)
				{
					Print();
				}
			}
			else if (e.KeyCode == Keys.L && e.Modifiers == Keys.Control)
			{
				new FrmLock().ShowDialog();
			}
		}
	}

	private void SetKeyboardLayout(InputLanguage layout)
	{
		InputLanguage.CurrentInputLanguage = layout;
	}

	private void textBox1_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("ar"));
	}

	public static InputLanguage GetInputLanguageByName(string inputName)
	{
		foreach (InputLanguage installedInputLanguage in InputLanguage.InstalledInputLanguages)
		{
			if (installedInputLanguage.Culture.EnglishName.ToLower().StartsWith(inputName))
			{
				return installedInputLanguage;
			}
		}
		return null;
	}

	private void TxtNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void FrmPaymentBig_FormClosing(object sender, FormClosingEventArgs e)
	{
		string layoutPath = LoginDetails.Lang == "ar-EG" ? fileNameAR : fileNameEN;
		try
		{
			System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(layoutPath));
			gridControl1.MainView.SaveLayoutToXml(layoutPath);
		}
		catch (Exception ex)
		{
			// Layout is a user preference; never prevent the form from closing if it cannot be saved.
			System.Diagnostics.Trace.WriteLine("تعذر حفظ تخطيط شاشة سندات الصرف المتعددة: " + ex);
		}
	}
	private void TxtAccountName_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			GTSErpSystem.Frms.FrmsMain.Search.FrmSearchAccount frmSearchAccount = new GTSErpSystem.Frms.FrmsMain.Search.FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblAccountName.Text = custCode.ToString();
				TxtAccountName.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtAccountName.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtNote_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Return)
		{
			groupBox2.Focus();
			gridView1.Focus();
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Account.FrmPaymentBig));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
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
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
		this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
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
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonProjectSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPurchOrder = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.RecoveredMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.LblAccountName = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtMan = new DevExpress.XtraEditors.TextEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtAccountName = new DevExpress.XtraEditors.ButtonEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtNotes = new System.Windows.Forms.TextBox();
		this.label24 = new System.Windows.Forms.Label();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemComboBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonProjectSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtMan.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccountName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		resources.ApplyResources(this.repositoryItemTextEdit3, "repositoryItemTextEdit3");
		this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
		this.repositoryItemPictureEdit1.Appearance.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceDisabled.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceReadOnly.Options.UseImage = true;
		this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
		this.repositoryItemPictureEdit1.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		resources.ApplyResources(this.repositoryItemComboBox1, "repositoryItemComboBox1");
		this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemComboBox1.Buttons"))
		});
		this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
		resources.ApplyResources(this.repositoryItemLookUpEdit1, "repositoryItemLookUpEdit1");
		this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemLookUpEdit1.Buttons"))
		});
		this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
		resources.ApplyResources(this.cardView1, "cardView1");
		this.cardView1.CardWidth = 199;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		resources.ApplyResources(this.gridColumn10, "gridColumn10");
		this.gridColumn10.Name = "gridColumn10";
		resources.ApplyResources(this.gridColumn11, "gridColumn11");
		this.gridColumn11.Name = "gridColumn11";
		resources.ApplyResources(this.gridColumn12, "gridColumn12");
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn13, "gridColumn13");
		this.gridColumn13.Name = "gridColumn13";
		resources.ApplyResources(this.gridColumn14, "gridColumn14");
		this.gridColumn14.Name = "gridColumn14";
		resources.ApplyResources(this.gridColumn15, "gridColumn15");
		this.gridColumn15.Name = "gridColumn15";
		resources.ApplyResources(this.gridColumn16, "gridColumn16");
		this.gridColumn16.Name = "gridColumn16";
		resources.ApplyResources(this.gridColumn17, "gridColumn17");
		this.gridColumn17.Name = "gridColumn17";
		resources.ApplyResources(this.gridColumn18, "gridColumn18");
		this.gridColumn18.Name = "gridColumn18";
		resources.ApplyResources(this.gridControl1, "gridControl1");
		this.gridControl1.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControl1.EmbeddedNavigator.AccessibleDescription");
		this.gridControl1.EmbeddedNavigator.AccessibleName = resources.GetString("gridControl1.EmbeddedNavigator.AccessibleName");
		this.gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip = (DevExpress.Utils.DefaultBoolean)resources.GetObject("gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip");
		this.gridControl1.EmbeddedNavigator.Anchor = (System.Windows.Forms.AnchorStyles)resources.GetObject("gridControl1.EmbeddedNavigator.Anchor");
		this.gridControl1.EmbeddedNavigator.BackgroundImage = (System.Drawing.Image)resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImage");
		this.gridControl1.EmbeddedNavigator.BackgroundImageLayout = (System.Windows.Forms.ImageLayout)resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImageLayout");
		this.gridControl1.EmbeddedNavigator.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("gridControl1.EmbeddedNavigator.ImeMode");
		this.gridControl1.EmbeddedNavigator.Margin = (System.Windows.Forms.Padding)resources.GetObject("gridControl1.EmbeddedNavigator.Margin");
		this.gridControl1.EmbeddedNavigator.MaximumSize = (System.Drawing.Size)resources.GetObject("gridControl1.EmbeddedNavigator.MaximumSize");
		this.gridControl1.EmbeddedNavigator.TextLocation = (DevExpress.XtraEditors.NavigatorButtonsTextLocation)resources.GetObject("gridControl1.EmbeddedNavigator.TextLocation");
		this.gridControl1.EmbeddedNavigator.ToolTip = resources.GetString("gridControl1.EmbeddedNavigator.ToolTip");
		this.gridControl1.EmbeddedNavigator.ToolTipIconType = (DevExpress.Utils.ToolTipIconType)resources.GetObject("gridControl1.EmbeddedNavigator.ToolTipIconType");
		this.gridControl1.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControl1.EmbeddedNavigator.ToolTipTitle");
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[12]
		{
			this.repositoryItemButtonEdit4, this.repositoryItemButtonEdit6, this.repositoryItemTextEdit1, this.repositoryItemTextEdit2, this.repositoryItemDateEdit1, this.repositoryItemTextEdit3, this.repositoryItemPictureEdit1, this.repositoryItemButtonEdit5, this.repositoryItemButtonEditCodeSearch, this.repositoryItemComboBox1,
			this.repositoryItemLookUpEdit1, this.repositoryItemButtonProjectSearch
		});
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView1.Appearance.HeaderPanel.Font");
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		resources.ApplyResources(this.gridView1, "gridView1");
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[14]
		{
			this.gridColumn9, this.gridColumn1, this.gridColumn2, this.gridColumn20, this.gridColumn21, this.gridColumn3, this.gridColumn6, this.gridColumn7, this.gridColumn8, this.gridColumn4,
			this.gridColumn5, this.gridColumn19, this.gridColumn22, this.gridColumn23
		});
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
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridView1.OptionsView.ShowGroupPanel = false;
		this.gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanging);
		resources.ApplyResources(this.gridColumn9, "gridColumn9");
		this.gridColumn9.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceCell.Font");
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceHeader.Font");
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.FieldName = "ظ…";
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn1, "gridColumn1");
		this.gridColumn1.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceCell.Font");
		this.gridColumn1.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn1.AppearanceCell.FontStyleDelta");
		this.gridColumn1.AppearanceCell.Options.UseFont = true;
		this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceHeader.Font");
		this.gridColumn1.AppearanceHeader.Options.UseFont = true;
		this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.ColumnEdit = this.repositoryItemButtonEditCodeSearch;
		this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.FieldName = "رقم الحساب";
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonEditCodeSearch, "repositoryItemButtonEditCodeSearch");
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons"), resources.GetString("repositoryItemButtonEditCodeSearch.Buttons1"), (int)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemButtonEditCodeSearch.Buttons6"), resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEditCodeSearch.Buttons9"))
		});
		this.repositoryItemButtonEditCodeSearch.Name = "repositoryItemButtonEditCodeSearch";
		this.repositoryItemButtonEditCodeSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEditCodeSearch_ButtonClick);
		resources.ApplyResources(this.gridColumn2, "gridColumn2");
		this.gridColumn2.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn2.AppearanceCell.Font");
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn2.AppearanceHeader.Font");
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.FieldName = "اسم الحساب";
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		resources.ApplyResources(this.gridColumn20, "gridColumn20");
		this.gridColumn20.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn20.AppearanceCell.Font");
		this.gridColumn20.AppearanceCell.Options.UseFont = true;
		this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn20.AppearanceHeader.Font");
		this.gridColumn20.AppearanceHeader.Options.UseFont = true;
		this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.FieldName = "المدين";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn21, "gridColumn21");
		this.gridColumn21.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn21.AppearanceCell.Font");
		this.gridColumn21.AppearanceCell.Options.UseFont = true;
		this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn21.AppearanceHeader.Font");
		this.gridColumn21.AppearanceHeader.Options.UseFont = true;
		this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.gridColumn21.FieldName = "الدائن";
		this.gridColumn21.Name = "gridColumn21";
		resources.ApplyResources(this.gridColumn3, "gridColumn3");
		this.gridColumn3.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceCell.Font");
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceHeader.Font");
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.FieldName = "البيان";
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn6, "gridColumn6");
		this.gridColumn6.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceCell.Font");
		this.gridColumn6.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn6.AppearanceCell.FontStyleDelta");
		this.gridColumn6.AppearanceCell.Options.UseFont = true;
		this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceHeader.Font");
		this.gridColumn6.AppearanceHeader.Options.UseFont = true;
		this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.ColumnEdit = this.repositoryItemButtonEdit4;
		this.gridColumn6.FieldName = "مركز التكلفة";
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
		editorButtonImageOptions2.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		editorButtonImageOptions2.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemButtonEdit4.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
		resources.ApplyResources(this.gridColumn7, "gridColumn7");
		this.gridColumn7.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceCell.Font");
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceHeader.Font");
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.FieldName = "اسم مركز التكلفة";
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn8, "gridColumn8");
		this.gridColumn8.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn8.AppearanceCell.Font");
		this.gridColumn8.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn8.AppearanceCell.FontStyleDelta");
		this.gridColumn8.AppearanceCell.Options.UseFont = true;
		this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn8.AppearanceHeader.Font");
		this.gridColumn8.AppearanceHeader.Options.UseFont = true;
		this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit6;
		this.gridColumn8.FieldName = "حذف";
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit6, "repositoryItemButtonEdit6");
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
		resources.ApplyResources(this.gridColumn4, "gridColumn4");
		this.gridColumn4.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceCell.Font");
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceHeader.Font");
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.ColumnEdit = this.repositoryItemButtonProjectSearch;
		this.gridColumn4.FieldName = "رقم المشروع";
		this.gridColumn4.MinWidth = 23;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.repositoryItemButtonProjectSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.repositoryItemButtonProjectSearch.Name = "repositoryItemButtonProjectSearch";
		this.repositoryItemButtonProjectSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonProjectSearch_ButtonClick);
		resources.ApplyResources(this.gridColumn5, "gridColumn5");
		this.gridColumn5.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceCell.Font");
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceHeader.Font");
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.FieldName = "إسم المشروع";
		this.gridColumn5.MinWidth = 23;
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.OptionsColumn.AllowEdit = false;
		this.gridColumn5.OptionsColumn.AllowFocus = false;
		this.gridColumn5.OptionsColumn.ReadOnly = true;
		this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceCell.Font");
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.FieldName = "الضريبه";
		this.gridColumn19.MinWidth = 25;
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.AllowFocus = false;
		this.gridColumn19.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn19.Summary"), resources.GetString("gridColumn19.Summary1"), resources.GetString("gridColumn19.Summary2"))
		});
		resources.ApplyResources(this.gridColumn22, "gridColumn22");
		this.gridColumn22.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceCell.Font");
		this.gridColumn22.AppearanceCell.Options.UseFont = true;
		this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceHeader.Font");
		this.gridColumn22.AppearanceHeader.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.FieldName = "تفعيل الضريبه";
		this.gridColumn22.MinWidth = 30;
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn22.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn23, "gridColumn23");
		this.gridColumn23.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceCell.Font");
		this.gridColumn23.AppearanceCell.Options.UseFont = true;
		this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceHeader.Font");
		this.gridColumn23.AppearanceHeader.Options.UseFont = true;
		this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.FieldName = "الاجمالي";
		this.gridColumn23.MinWidth = 25;
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn23.Summary"), resources.GetString("gridColumn23.Summary1"), resources.GetString("gridColumn23.Summary2"))
		});
		resources.ApplyResources(this.repositoryItemTextEdit1, "repositoryItemTextEdit1");
		this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
		resources.ApplyResources(this.repositoryItemTextEdit2, "repositoryItemTextEdit2");
		this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
		resources.ApplyResources(this.repositoryItemDateEdit1, "repositoryItemDateEdit1");
		this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit1.Buttons"))
		});
		this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit1.CalendarTimeProperties.Buttons"))
		});
		this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[13]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.RecoveredMenuItem1, this.toolStripSeparator1, this.MenuSearchItem, this.toolStripSeparator4, this.MenuPurchOrder, this.toolStripSeparator2,
			this.RecoveredMenuItem2, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		resources.ApplyResources(this.MenuNew, "MenuNew");
		this.MenuNew.Name = "MenuNew";
		resources.ApplyResources(this.MenuSave, "MenuSave");
		this.MenuSave.Name = "MenuSave";
		resources.ApplyResources(this.MenuEdit, "MenuEdit");
		this.MenuEdit.Name = "MenuEdit";
		resources.ApplyResources(this.MenuDelete, "MenuDelete");
		this.MenuDelete.Name = "MenuDelete";
		resources.ApplyResources(this.RecoveredMenuItem1, "طباعةToolStripMenuItem1");
		this.RecoveredMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.MenuPrintFatoura });
		this.RecoveredMenuItem1.Name = "طباعةToolStripMenuItem1";
		resources.ApplyResources(this.MenuPrintFatoura, "MenuPrintFatoura");
		this.MenuPrintFatoura.Name = "MenuPrintFatoura";
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.MenuSearchItem, "MenuSearchItem");
		this.MenuSearchItem.Name = "MenuSearchItem";
		resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		resources.ApplyResources(this.MenuPurchOrder, "MenuPurchOrder");
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.RecoveredMenuItem2, "تخصيصToolStripMenuItem1");
		this.RecoveredMenuItem2.Name = "تخصيصToolStripMenuItem1";
		resources.ApplyResources(this.MenuExit, "MenuExit");
		this.MenuExit.Name = "MenuExit";
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.CBCostCenter);
		this.groupBox1.Controls.Add(this.LblAccountName);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.TxtMan);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtAccountName);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtNotes);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.TxtNumNote);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.CBCostCenter, "CBCostCenter");
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBCostCenter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Comb_KeyPress);
		resources.ApplyResources(this.LblAccountName, "LblAccountName");
		this.LblAccountName.Name = "LblAccountName";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.TxtMan, "TxtMan");
		this.TxtMan.Name = "TxtMan";
		this.TxtMan.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtMan.Properties.Appearance.Font");
		this.TxtMan.Properties.Appearance.Options.UseFont = true;
		this.TxtMan.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtMan.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtMan.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtMan.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtAccountName, "TxtAccountName");
		this.TxtAccountName.Name = "TxtAccountName";
		this.TxtAccountName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtAccountName.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAccountName.Properties.Appearance.Font");
		this.TxtAccountName.Properties.Appearance.Options.UseBackColor = true;
		this.TxtAccountName.Properties.Appearance.Options.UseFont = true;
		this.TxtAccountName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAccountName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.TxtAccountName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtAccountName.Properties.Buttons"), resources.GetString("TxtAccountName.Properties.Buttons1"), (int)resources.GetObject("TxtAccountName.Properties.Buttons2"), (bool)resources.GetObject("TxtAccountName.Properties.Buttons3"), (bool)resources.GetObject("TxtAccountName.Properties.Buttons4"), (bool)resources.GetObject("TxtAccountName.Properties.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("TxtAccountName.Properties.Buttons6"), resources.GetObject("TxtAccountName.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtAccountName.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtAccountName.Properties.Buttons9"))
		});
		this.TxtAccountName.Properties.ReadOnly = true;
		this.TxtAccountName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtAccountName_ButtonClick);
		this.TxtAccountName.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.TxtNotes, "TxtNotes");
		this.TxtNotes.Name = "TxtNotes";
		this.TxtNotes.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNotes.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		resources.ApplyResources(this.label24, "label24");
		this.label24.Name = "label24";
		resources.ApplyResources(this.TxtNumNote, "TxtNumNote");
		this.TxtNumNote.Name = "TxtNumNote";
		this.TxtNumNote.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumNote.Properties.Appearance.Font");
		this.TxtNumNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNumNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumNote.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNumNote.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtDate, "TxtDate");
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDate.Properties.Appearance.Font");
		this.TxtDate.Properties.Appearance.Options.UseFont = true;
		this.TxtDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate.Properties.Buttons"))
		});
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate.Properties.Mask.EditMask = resources.GetString("TxtDate.Properties.Mask.EditMask");
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.label17, "label17");
		this.label17.Name = "label17";
		resources.ApplyResources(this.TxtCode, "TxtCode");
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCode.Properties.Appearance.Font");
		this.TxtCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCode.Properties.Appearance.Options.UseFont = true;
		this.TxtCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCode.Properties.ReadOnly = true;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.TxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnPrint, "BtnPrint");
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.gridControl1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmPaymentBig";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmPaymentBig_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemComboBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonProjectSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtMan.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccountName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


