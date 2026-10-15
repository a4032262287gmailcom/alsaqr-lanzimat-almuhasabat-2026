using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
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
using GTSErpSystem.BLL.Payroll;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.FrmsMain.Search;
using GTSErpSystem.Frms.Payroll.Search;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Payroll;

public class FrmPayroll : XtraForm
{
	private DataTable datatable = new DataTable();

	private Class_Payroll Daily = new Class_Payroll();

	private Class_PayrollTemp DailyTemp = new Class_PayrollTemp();

	private Calss_CostCenter Cost = new Calss_CostCenter();

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

	private string fileNameAR = Application.StartupPath + "\\Layout\\Payroll.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\Payroll.xml";

	private string accountNumber;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private DateEdit TxtDate;

	private Label label21;

	private Label label17;

	private ButtonEdit TxtCode;

	private Label label24;

	private TextEdit TxtNumNote;

	private GridView gridView1;

	private GridColumn gridColumn9;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private GridColumn gridColumn2;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private RepositoryItemTextEdit repositoryItemTextEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit2;

	private RepositoryItemDateEdit repositoryItemDateEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit3;

	private RepositoryItemPictureEdit repositoryItemPictureEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

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

	private RepositoryItemComboBox repositoryItemComboBox1;

	private RepositoryItemLookUpEdit repositoryItemLookUpEdit1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem MenuNew;

	private ToolStripMenuItem MenuSave;

	private ToolStripMenuItem MenuEdit;

	private ToolStripMenuItem MenuDelete;

	private ToolStripMenuItem RecoveredMenuItemFix1;

	private ToolStripMenuItem MenuPrintFatoura;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem MenuSearchItem;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem MenuPurchOrder;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem RecoveredMenuItemFix2;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem MenuExit;

	private Label label1;

	private TextBox TxtNotes;

	private GridColumn gridColumn22;

	private GridColumn gridColumn5;

	private GridColumn gridColumn19;

	private GridColumn gridColumn23;

	private GridColumn gridColumn1;

	private SimpleButton BtnAddFromTest;

	private RepositoryItemButtonEdit repositoryItemButtonBankBox;

	private ToolStripMenuItem MenuPrintQuaid;

	private GridColumn gridColumn24;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private GridColumn gridColumn25;

	public GridControl gridControl1;

	private GridColumn gridColumn26;

	private GridColumn gridColumn27;

	private GridColumn gridColumn28;

	private GridColumn gridColumn4;

	private GridColumn gridColumn29;

	private GridColumn gridColumn30;

	private GridColumn gridColumn31;

	private GridColumn gridColumn32;

	private SimpleButton BtnPrintAll;

	public FrmPayroll()
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
		TxtDate.DateTime = DateTime.Now;
		SetInitialRow();
		gridView1.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
		base.KeyPreview = true;
		gridView1.Columns["صافي الراتب"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["صافي الراتب"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["الراتب"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["الراتب"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["أيام غياب"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["أيام غياب"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["OverTime"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["OverTime"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["تسديد سلفة"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["تسديد سلفة"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["جزاءات"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["جزاءات"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.Columns["مكافئات"].DisplayFormat.FormatType = FormatType.Numeric;
		gridView1.Columns["مكافئات"].DisplayFormat.FormatString = "n" + DecimalNum;
		gridView1.FocusedRowHandle = 1;
		gridView1.FocusedColumn = gridView1.VisibleColumns[1];
		gridView1.ShowEditor();
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Tunisia));
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 78);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 78);
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
		RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit();
		repositoryItemCheckEdit.CheckedChanged += RepoCheck_CheckedChanged;
		gridView1.Columns["Select"].ColumnEdit = repositoryItemCheckEdit;
	}

	private void RepoCheck_CheckedChanged(object sender, EventArgs e)
	{
		gridView1.PostEditor();
		if (!(sender is CheckEdit))
		{
			return;
		}
		int focusedRowHandle = gridView1.FocusedRowHandle;
		if (focusedRowHandle < 0)
		{
			return;
		}
		object rowCellValue = gridView1.GetRowCellValue(focusedRowHandle, "Select");
		if (rowCellValue != null && rowCellValue != DBNull.Value && Convert.ToBoolean(rowCellValue))
		{
			object rowCellValue2 = gridView1.GetRowCellValue(focusedRowHandle, "رقم الحساب");
			if (rowCellValue2 != null && rowCellValue2 != DBNull.Value)
			{
				accountNumber = rowCellValue2.ToString();
			}
		}
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string iD = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "رقم الحساب"));
		string text = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "اسم الموظف"));
		if (!(text == ""))
		{
			return;
		}
		Select_SearchAccount_ByAccountNumber_Result select_SearchAccount_ByAccountNumber_Result = Daily.GetSearchAccountByAccountNum(iD).FirstOrDefault();
		if (select_SearchAccount_ByAccountNumber_Result != null)
		{
			e.Valid = false;
			if (text == "")
			{
				MessageBox.Show("برجاء إدخال أسم الموظف");
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[1];
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
		try
		{
			int num = 0;
			if (e.Column.FieldName == "رقم الحساب")
			{
				int iD = Convert.ToInt32(e.Value);
				Emp_Employee emp_Employee = Daily.GetEmp(iD).FirstOrDefault();
				if (0 == 0)
				{
					object rowCellValue = gridView1.GetRowCellValue(e.RowHandle, "تم");
					string text = rowCellValue.ToString();
					if (text == "0")
					{
						if (emp_Employee == null)
						{
							gridView1.FocusedRowHandle = e.RowHandle;
							gridView1.FocusedColumn = gridView1.VisibleColumns[0];
							gridView1.ShowEditor();
							return;
						}
						DataRow dataRow = null;
						dataRow = datatable.NewRow();
						dataRow["ظ…"] = 1;
						dataRow["AccountID"] = emp_Employee.AccountNo;
						dataRow["رقم الحساب"] = emp_Employee.ID;
						dataRow["اسم الموظف"] = emp_Employee.Name;
						dataRow["الراتب"] = emp_Employee.Money;
						dataRow["رقم حساب الصندوق/البنك"] = LoginDetails.Acc_CashBox;
						dataRow["الصندوق/البنك"] = Daily.GetAccountName(Convert.ToInt32(LoginDetails.Acc_CashBox));
						dataRow["أيام غياب"] = 0;
						dataRow["OverTime"] = 0;
						dataRow["OverTimeValue"] = 0;
						int empAccount = Daily.GetEmpAccount(Convert.ToInt32(emp_Employee.AccountNo));
						dataRow["تسديد سلفة"] = Daily.Ledger_Employee(empAccount);
						decimal num2 = Convert.ToDecimal(dataRow["تسديد سلفة"]);
						dataRow["جزاءات"] = 0.0;
						dataRow["مكافئات"] = 0.0;
						dataRow["NetBadl"] = Math.Round(Convert.ToDecimal(emp_Employee.NetAllowances), 2);
						dataRow["Delays"] = 0.0;
						dataRow["DelaysValue"] = 0.0;
						dataRow["Vacations"] = 0.0;
						dataRow["ValueOfDaysAbsence"] = 0.0;
						dataRow["Bouns"] = 0.0;
						dataRow["صافي الراتب"] = emp_Employee.Money - (decimal?)Convert.ToDecimal(dataRow["تسديد سلفة"]) + (decimal?)Convert.ToDecimal(dataRow["NetBadl"]);
						dataRow["مركز التكلفة"] = 0;
						dataRow["اسم مركز التكلفة"] = string.Empty;
						dataRow["مسح"] = 0;
						dataRow["تم"] = 1;
						CheckFfistRow();
						datatable.Rows.Add(dataRow);
						insertnewrow();
						gridView1.FocusedRowHandle = e.RowHandle;
						gridView1.FocusedColumn = gridView1.VisibleColumns[1];
						gridView1.ShowEditor();
					}
					else
					{
						foreach (DataRow row in datatable.Rows)
						{
							string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("ظ…");
							if (row["ظ…"].ToString() == focusedRowCellDisplayText)
							{
								int num3 = Convert.ToInt32(e.Value);
								Emp_Employee emp_Employee2 = Daily.GetEmp(iD).FirstOrDefault();
								if (emp_Employee2 != null)
								{
									row["AccountID"] = emp_Employee2.AccountNo;
									row["رقم الحساب"] = emp_Employee2.ID;
									row["اسم الموظف"] = emp_Employee2.Name;
									row["الراتب"] = emp_Employee2.Money;
									row["رقم حساب الصندوق/البنك"] = LoginDetails.Acc_CashBox;
									row["الصندوق/البنك"] = Daily.GetAccountName(Convert.ToInt32(LoginDetails.Acc_CashBox));
									row["أيام غياب"] = 0;
									row["OverTime"] = 0;
									row["Delays"] = 0.0;
									row["Vacations"] = 0.0;
									row["Bouns"] = 0.0;
									int empAccount2 = Daily.GetEmpAccount(Convert.ToInt32(emp_Employee2.AccountNo));
									row["تسديد سلفة"] = Daily.Ledger_Employee(empAccount2);
									row["جزاءات"] = 0.0;
									row["مكافئات"] = 0.0;
									row["NetBadl"] = Math.Round(Convert.ToDecimal(emp_Employee.NetAllowances), 2);
									row["صافي الراتب"] = emp_Employee2.Money - (decimal?)Convert.ToDecimal(row["تسديد سلفة"]) + (decimal?)Convert.ToDecimal(row["NetBadl"]);
									row["مركز التكلفة"] = 0;
									row["اسم مركز التكلفة"] = string.Empty;
									row["مسح"] = 0;
									break;
								}
							}
						}
						gridView1.FocusedRowHandle = e.RowHandle;
						gridView1.FocusedColumn = gridView1.VisibleColumns[1];
						gridView1.ShowEditor();
					}
				}
			}
			else if (e.Column.FieldName == "الراتب")
			{
				string value = Convert.ToString(e.Value);
				string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				foreach (DataRow row2 in datatable.Rows)
				{
					if (row2["ظ…"].ToString() == focusedRowCellDisplayText2)
					{
						decimal num4 = Convert.ToDecimal(value) / 30m;
						decimal num5 = num4 / 9m;
						row2["الراتب"] = value;
						decimal num6 = num4 * Convert.ToDecimal(row2["أيام غياب"]);
						row2["صافي الراتب"] = Convert.ToDecimal(row2["الراتب"]) + Convert.ToDecimal(row2["OverTimeValue"]) + Convert.ToDecimal(row2["NetBadl"]) + Convert.ToDecimal(row2["مكافئات"]) + Convert.ToDecimal(row2["Bouns"]) - (Convert.ToDecimal(row2["جزاءات"]) + Convert.ToDecimal(row2["ValueOfDaysAbsence"]) + Convert.ToDecimal(row2["DelaysValue"]) + Convert.ToDecimal(row2["تسديد سلفة"]));
						break;
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[5];
				gridView1.ShowEditor();
			}
			else if (e.Column.FieldName == "أيام غياب")
			{
				string value2 = Convert.ToString(e.Value);
				string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				foreach (DataRow row3 in datatable.Rows)
				{
					if (row3["ظ…"].ToString() == focusedRowCellDisplayText3)
					{
						row3["أيام غياب"] = value2;
						decimal num7 = Convert.ToDecimal(row3["الراتب"]) / 30m;
						decimal d = num7 * Convert.ToDecimal(row3["أيام غياب"]);
						row3["ValueOfDaysAbsence"] = Math.Round(d, 2);
						row3["صافي الراتب"] = Convert.ToDecimal(row3["الراتب"]) + Convert.ToDecimal(row3["OverTimeValue"]) + Convert.ToDecimal(row3["NetBadl"]) + Convert.ToDecimal(row3["مكافئات"]) + Convert.ToDecimal(row3["Bouns"]) - (Convert.ToDecimal(row3["جزاءات"]) + Convert.ToDecimal(row3["ValueOfDaysAbsence"]) + Convert.ToDecimal(row3["DelaysValue"]) + Convert.ToDecimal(row3["تسديد سلفة"]));
						break;
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[5];
				gridView1.ShowEditor();
			}
			else if (e.Column.FieldName == "ValueOfDaysAbsence")
			{
				string value3 = Convert.ToString(e.Value);
				string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				foreach (DataRow row4 in datatable.Rows)
				{
					if (row4["ظ…"].ToString() == focusedRowCellDisplayText4)
					{
						row4["ValueOfDaysAbsence"] = value3;
						decimal value4 = Convert.ToDecimal(row4["الراتب"]) / 30m;
						row4["أيام غياب"] = Math.Round(Convert.ToDecimal(row4["ValueOfDaysAbsence"]) / Convert.ToDecimal(value4), 2).ToString();
						row4["صافي الراتب"] = Convert.ToDecimal(row4["الراتب"]) + Convert.ToDecimal(row4["OverTimeValue"]) + Convert.ToDecimal(row4["NetBadl"]) + Convert.ToDecimal(row4["مكافئات"]) + Convert.ToDecimal(row4["Bouns"]) - (Convert.ToDecimal(row4["جزاءات"]) + Convert.ToDecimal(row4["ValueOfDaysAbsence"]) + Convert.ToDecimal(row4["DelaysValue"]) + Convert.ToDecimal(row4["تسديد سلفة"]));
						break;
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[5];
				gridView1.ShowEditor();
			}
			else if (e.Column.FieldName == "OverTime")
			{
				string value5 = Convert.ToString(e.Value);
				string focusedRowCellDisplayText5 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				foreach (DataRow row5 in datatable.Rows)
				{
					if (row5["ظ…"].ToString() == focusedRowCellDisplayText5)
					{
						row5["OverTime"] = value5;
						decimal num8 = Convert.ToDecimal(row5["الراتب"]) / 30m;
						decimal num9 = num8 / 9m;
						decimal d2 = num9 * Convert.ToDecimal(row5["OverTime"]);
						row5["OverTimeValue"] = Math.Round(d2, 2).ToString();
						row5["صافي الراتب"] = Convert.ToDecimal(row5["الراتب"]) + Convert.ToDecimal(row5["OverTimeValue"]) + Convert.ToDecimal(row5["NetBadl"]) + Convert.ToDecimal(row5["مكافئات"]) + Convert.ToDecimal(row5["Bouns"]) - (Convert.ToDecimal(row5["جزاءات"]) + Convert.ToDecimal(row5["ValueOfDaysAbsence"]) + Convert.ToDecimal(row5["DelaysValue"]) + Convert.ToDecimal(row5["تسديد سلفة"]));
						break;
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[6];
				gridView1.ShowEditor();
			}
			else if (e.Column.FieldName == "OverTimeValue")
			{
				string value6 = Convert.ToString(e.Value);
				string focusedRowCellDisplayText6 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				foreach (DataRow row6 in datatable.Rows)
				{
					if (row6["ظ…"].ToString() == focusedRowCellDisplayText6)
					{
						row6["OverTimeValue"] = value6;
						decimal value7 = Convert.ToDecimal(row6["الراتب"]) / 30m;
						row6["OverTime"] = Math.Round(Convert.ToDecimal(value6) / Convert.ToDecimal(value7) * 9m, 2).ToString();
						row6["صافي الراتب"] = Convert.ToDecimal(row6["الراتب"]) + Convert.ToDecimal(row6["OverTimeValue"]) + Convert.ToDecimal(row6["NetBadl"]) + Convert.ToDecimal(row6["مكافئات"]) + Convert.ToDecimal(row6["Bouns"]) - (Convert.ToDecimal(row6["جزاءات"]) + Convert.ToDecimal(row6["ValueOfDaysAbsence"]) + Convert.ToDecimal(row6["DelaysValue"]) + Convert.ToDecimal(row6["تسديد سلفة"]));
						break;
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[6];
				gridView1.ShowEditor();
			}
			else if (e.Column.FieldName == "Delays")
			{
				string value8 = Convert.ToString(e.Value);
				string focusedRowCellDisplayText7 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				foreach (DataRow row7 in datatable.Rows)
				{
					if (row7["ظ…"].ToString() == focusedRowCellDisplayText7)
					{
						row7["Delays"] = value8;
						decimal num10 = 0m;
						decimal num11 = Convert.ToDecimal(row7["الراتب"]) / 30m;
						decimal num12 = num11 / 9m;
						num10 = num12 * Convert.ToDecimal(row7["Delays"]);
						row7["DelaysValue"] = Math.Round(num10, 2).ToString();
						row7["صافي الراتب"] = Convert.ToDecimal(row7["الراتب"]) + Convert.ToDecimal(row7["OverTimeValue"]) + Convert.ToDecimal(row7["NetBadl"]) + Convert.ToDecimal(row7["مكافئات"]) + Convert.ToDecimal(row7["Bouns"]) - (Convert.ToDecimal(row7["جزاءات"]) + Convert.ToDecimal(row7["ValueOfDaysAbsence"]) + Convert.ToDecimal(row7["DelaysValue"]) + Convert.ToDecimal(row7["تسديد سلفة"]));
						break;
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[6];
				gridView1.ShowEditor();
			}
			else if (e.Column.FieldName == "DelaysValue")
			{
				string value9 = Convert.ToString(e.Value);
				string focusedRowCellDisplayText8 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				foreach (DataRow row8 in datatable.Rows)
				{
					if (row8["ظ…"].ToString() == focusedRowCellDisplayText8)
					{
						row8["DelaysValue"] = value9;
						decimal num13 = Convert.ToDecimal(row8["الراتب"]) / 30m;
						row8["Delays"] = Math.Round(Convert.ToDecimal(row8["DelaysValue"]) / num13 * 9m, 2).ToString();
						row8["صافي الراتب"] = Convert.ToDecimal(row8["الراتب"]) + Convert.ToDecimal(row8["OverTimeValue"]) + Convert.ToDecimal(row8["NetBadl"]) + Convert.ToDecimal(row8["مكافئات"]) + Convert.ToDecimal(row8["Bouns"]) - (Convert.ToDecimal(row8["جزاءات"]) + Convert.ToDecimal(row8["ValueOfDaysAbsence"]) + Convert.ToDecimal(row8["DelaysValue"]) + Convert.ToDecimal(row8["تسديد سلفة"]));
						break;
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[6];
				gridView1.ShowEditor();
			}
			else if (e.Column.FieldName == "تسديد سلفة")
			{
				string value10 = Convert.ToString(e.Value);
				string focusedRowCellDisplayText9 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				foreach (DataRow row9 in datatable.Rows)
				{
					if (row9["ظ…"].ToString() == focusedRowCellDisplayText9)
					{
						row9["تسديد سلفة"] = value10;
						row9["صافي الراتب"] = Convert.ToDecimal(row9["الراتب"]) + Convert.ToDecimal(row9["OverTimeValue"]) + Convert.ToDecimal(row9["NetBadl"]) + Convert.ToDecimal(row9["مكافئات"]) + Convert.ToDecimal(row9["Bouns"]) - (Convert.ToDecimal(row9["جزاءات"]) + Convert.ToDecimal(row9["ValueOfDaysAbsence"]) + Convert.ToDecimal(row9["DelaysValue"]) + Convert.ToDecimal(row9["تسديد سلفة"]));
						break;
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[7];
				gridView1.ShowEditor();
			}
			else if (e.Column.FieldName == "جزاءات")
			{
				string value11 = Convert.ToString(e.Value);
				string focusedRowCellDisplayText10 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				foreach (DataRow row10 in datatable.Rows)
				{
					if (row10["ظ…"].ToString() == focusedRowCellDisplayText10)
					{
						row10["جزاءات"] = value11;
						row10["صافي الراتب"] = Convert.ToDecimal(row10["الراتب"]) + Convert.ToDecimal(row10["OverTimeValue"]) + Convert.ToDecimal(row10["NetBadl"]) + Convert.ToDecimal(row10["مكافئات"]) + Convert.ToDecimal(row10["Bouns"]) - (Convert.ToDecimal(row10["جزاءات"]) + Convert.ToDecimal(row10["ValueOfDaysAbsence"]) + Convert.ToDecimal(row10["DelaysValue"]) + Convert.ToDecimal(row10["تسديد سلفة"]));
						break;
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[8];
				gridView1.ShowEditor();
			}
			else if (e.Column.FieldName == "مكافئات")
			{
				string value12 = Convert.ToString(e.Value);
				string focusedRowCellDisplayText11 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				foreach (DataRow row11 in datatable.Rows)
				{
					if (row11["ظ…"].ToString() == focusedRowCellDisplayText11)
					{
						row11["مكافئات"] = value12;
						row11["صافي الراتب"] = Convert.ToDecimal(row11["الراتب"]) + Convert.ToDecimal(row11["OverTimeValue"]) + Convert.ToDecimal(row11["NetBadl"]) + Convert.ToDecimal(row11["مكافئات"]) + Convert.ToDecimal(row11["Bouns"]) - (Convert.ToDecimal(row11["جزاءات"]) + Convert.ToDecimal(row11["ValueOfDaysAbsence"]) + Convert.ToDecimal(row11["DelaysValue"]) + Convert.ToDecimal(row11["تسديد سلفة"]));
						break;
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[9];
				gridView1.ShowEditor();
			}
			else if (e.Column.FieldName == "Bouns")
			{
				string value13 = Convert.ToString(e.Value);
				string focusedRowCellDisplayText12 = gridView1.GetFocusedRowCellDisplayText("ظ…");
				foreach (DataRow row12 in datatable.Rows)
				{
					if (row12["ظ…"].ToString() == focusedRowCellDisplayText12)
					{
						row12["Bouns"] = value13;
						row12["صافي الراتب"] = Convert.ToDecimal(row12["الراتب"]) + Convert.ToDecimal(row12["OverTimeValue"]) + Convert.ToDecimal(row12["NetBadl"]) + Convert.ToDecimal(row12["مكافئات"]) + Convert.ToDecimal(row12["Bouns"]) - (Convert.ToDecimal(row12["جزاءات"]) + Convert.ToDecimal(row12["ValueOfDaysAbsence"]) + Convert.ToDecimal(row12["DelaysValue"]) + Convert.ToDecimal(row12["تسديد سلفة"]));
						break;
					}
				}
				CheckFfistRow();
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[9];
				gridView1.ShowEditor();
			}
			else
			{
				if (!(e.Column.FieldName == "مركز التكلفة"))
				{
					return;
				}
				string value14 = Convert.ToString(e.Value);
				try
				{
					int Num = Convert.ToInt32(value14);
					string focusedRowCellDisplayText13 = gridView1.GetFocusedRowCellDisplayText("ظ…");
					Select_SearchAccountCostCenter_Result select_SearchAccountCostCenter_Result = (from x in Cost.GetSearchAccountCostCenter()
						where x.CostCentersID == Num
						select x).FirstOrDefault();
					if (select_SearchAccountCostCenter_Result != null)
					{
						foreach (DataRow row13 in datatable.Rows)
						{
							if (row13["ظ…"].ToString() == focusedRowCellDisplayText13)
							{
								row13["مركز التكلفة"] = select_SearchAccountCostCenter_Result.CostCentersID;
								row13["اسم مركز التكلفة"] = select_SearchAccountCostCenter_Result.CostCentersName;
								break;
							}
						}
					}
					else
					{
						foreach (DataRow row14 in datatable.Rows)
						{
							if (row14["ظ…"].ToString() == focusedRowCellDisplayText13)
							{
								row14["مركز التكلفة"] = 0;
								row14["اسم مركز التكلفة"] = "";
								break;
							}
						}
					}
					CheckFfistRow();
					insertnewrow();
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[11];
					gridView1.ShowEditor();
				}
				catch (Exception)
				{
				}
			}
			ArrangeDatatable();
		}
		catch (Exception)
		{
		}
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

	public void UpdateAllowancesInGrid(CellValueChangedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["ظ…"].ToString() == Convert.ToString(num))
			{
				decimal num2 = Convert.ToDecimal(row["صافي الراتب"]) - Convert.ToDecimal(row["NetBadl"]);
				row["NetBadl"] = Convert.ToDecimal(e.Value);
				row["صافي الراتب"] = Math.Round(num2 + Convert.ToDecimal(row["NetBadl"]), 2);
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[5];
				gridView1.ShowEditor();
			}
		}
	}

	private void repositoryItemButtonEdit2_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["ظ…"].ToString() == Convert.ToString(num))
			{
				if (row["اسم الموظف"].ToString() != "")
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
		FrmSearchCostCenter frmSearchCostCenter = new FrmSearchCostCenter();
		frmSearchCostCenter.ShowDialog();
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["ظ…"].ToString() == Convert.ToString(num))
			{
				row["مركز التكلفة"] = frmSearchCostCenter.CustCode;
				row["اسم مركز التكلفة"] = frmSearchCostCenter.AccountName;
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

	private void repositoryItemButtonBankBox_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "رقم الحساب"));
		if (!(text != ""))
		{
			return;
		}
		LoginDetails.FrmName = "FrmReceiptsCash";
		FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
		frmSearchAccount.ShowDialog();
		if (frmSearchAccount.CustCode > 0)
		{
			int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
			foreach (DataRow row in datatable.Rows)
			{
				if (row["ظ…"].ToString() == Convert.ToString(num))
				{
					row["رقم حساب الصندوق/البنك"] = frmSearchAccount.CustCode;
					row["الصندوق/البنك"] = frmSearchAccount.AccountName;
				}
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[5];
			gridView1.ShowEditor();
			frmSearchAccount.Close();
			LoginDetails.FrmName = "";
			return;
		}
		int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row2 in datatable.Rows)
		{
			if (row2["ظ…"].ToString() == Convert.ToString(num2))
			{
				row2["رقم حساب الصندوق/البنك"] = LoginDetails.Acc_CashBox;
				row2["الصندوق/البنك"] = Daily.GetAccountName(Convert.ToInt32(LoginDetails.Acc_CashBox));
			}
		}
	}

	private void repositoryItemButtonEdit1_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int empId = 0;
		foreach (DataRow row in datatable.Rows)
		{
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (row["ظ…"] != DBNull.Value && row["ظ…"].ToString() == focusedRowCellDisplayText && row["رقم الحساب"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["رقم الحساب"].ToString()))
			{
				empId = Convert.ToInt32(row["رقم الحساب"]);
				break;
			}
		}
		InfoAllowances infoAllowances = new InfoAllowances
		{
			Owner = this
		};
		infoAllowances.EmpId = empId;
		infoAllowances.ShowDialog();
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("ظ…", typeof(int));
		datatable.Columns.Add("AccountID", typeof(string));
		datatable.Columns.Add("رقم الحساب", typeof(string));
		datatable.Columns.Add("اسم الموظف", typeof(string));
		datatable.Columns.Add("الراتب", typeof(decimal));
		datatable.Columns.Add("رقم حساب الصندوق/البنك", typeof(int));
		datatable.Columns.Add("الصندوق/البنك", typeof(string));
		datatable.Columns.Add("أيام غياب", typeof(decimal));
		datatable.Columns.Add("OverTime", typeof(decimal));
		datatable.Columns.Add("OverTimeValue", typeof(decimal));
		datatable.Columns.Add("تسديد سلفة", typeof(decimal));
		datatable.Columns.Add("جزاءات", typeof(decimal));
		datatable.Columns.Add("مكافئات", typeof(decimal));
		datatable.Columns.Add("صافي الراتب", typeof(decimal));
		datatable.Columns.Add("مركز التكلفة", typeof(int));
		datatable.Columns.Add("اسم مركز التكلفة", typeof(string));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تم", typeof(int));
		datatable.Columns.Add("NetBadl", typeof(decimal));
		datatable.Columns.Add("Delays", typeof(decimal));
		datatable.Columns.Add("DelaysValue", typeof(decimal));
		datatable.Columns.Add("Vacations", typeof(decimal));
		datatable.Columns.Add("Bouns", typeof(decimal));
		datatable.Columns.Add("ValueOfDaysAbsence", typeof(decimal));
		datatable.Columns.Add("Select", typeof(bool));
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["ظ…"] = 1;
		dataRow["AccountID"] = string.Empty;
		dataRow["رقم الحساب"] = string.Empty;
		dataRow["اسم الموظف"] = string.Empty;
		dataRow["الراتب"] = 0.0;
		dataRow["رقم حساب الصندوق/البنك"] = LoginDetails.Acc_CashBox;
		dataRow["الصندوق/البنك"] = Daily.GetAccountName(Convert.ToInt32(LoginDetails.Acc_CashBox));
		dataRow["أيام غياب"] = 0.0;
		dataRow["OverTime"] = 0.0;
		dataRow["OverTimeValue"] = 0.0;
		dataRow["تسديد سلفة"] = 0.0;
		dataRow["جزاءات"] = 0.0;
		dataRow["مكافئات"] = 0.0;
		dataRow["صافي الراتب"] = 0.0;
		dataRow["Delays"] = 0.0;
		dataRow["DelaysValue"] = 0.0;
		dataRow["Vacations"] = 0.0;
		dataRow["Bouns"] = 0.0;
		dataRow["NetBadl"] = 0.0;
		dataRow["ValueOfDaysAbsence"] = 0.0;
		dataRow["مركز التكلفة"] = 0;
		dataRow["اسم مركز التكلفة"] = string.Empty;
		dataRow["مسح"] = 0;
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridControl1.ProcessGridKey += gridControl_ProcessGridKey;
		gridView1.RowCellClick += gridView1_RowCellClick;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		gridView1.CustomDrawFooterCell += gridView1_CustomDrawFooterCell;
		gridView1.Columns["صافي الراتب"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.00}", "0.")
		});
		gridView1.Columns["الراتب"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.00}", "0.")
		});
		gridView1.Columns["أيام غياب"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.00}", "0.")
		});
		gridView1.Columns["OverTime"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.00}", "0.")
		});
		gridView1.Columns["تسديد سلفة"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.00}", "0.")
		});
		gridView1.Columns["جزاءات"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.00}", "0.")
		});
		gridView1.Columns["مكافئات"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, "", "{0:0.00}", "0.")
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
			if (dataRow["اسم الموظف"].ToString() == "" || dataRow["رقم الحساب"].ToString() == "0")
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
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["ظ…"] = 1;
		dataRow["AccountID"] = string.Empty;
		dataRow["رقم الحساب"] = string.Empty;
		dataRow["اسم الموظف"] = string.Empty;
		dataRow["الراتب"] = 0.0;
		dataRow["رقم حساب الصندوق/البنك"] = LoginDetails.Acc_CashBox;
		dataRow["الصندوق/البنك"] = Daily.GetAccountName(Convert.ToInt32(LoginDetails.Acc_CashBox));
		dataRow["أيام غياب"] = 0.0;
		dataRow["OverTime"] = 0.0;
		dataRow["OverTimeValue"] = 0.0;
		dataRow["تسديد سلفة"] = 0.0;
		dataRow["جزاءات"] = 0.0;
		dataRow["مكافئات"] = 0.0;
		dataRow["صافي الراتب"] = 0.0;
		dataRow["NetBadl"] = 0.0;
		dataRow["Delays"] = 0.0;
		dataRow["DelaysValue"] = 0.0;
		dataRow["Vacations"] = 0.0;
		dataRow["ValueOfDaysAbsence"] = 0.0;
		dataRow["Bouns"] = 0.0;
		dataRow["مركز التكلفة"] = 0;
		dataRow["اسم مركز التكلفة"] = string.Empty;
		dataRow["مسح"] = 0;
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
	}

	private void Clear()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		datatable.Rows.Clear();
		gridControl1.DataSource = datatable;
		TxtCode.Text = "";
		TxtNumNote.Text = "";
		TxtNotes.Text = "";
		TxtDate.DateTime = DateTime.Now;
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
		insertnewrow();
		IsEnableTrue();
		TxtDate.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 78))
		{
			int num = 0;
			int num2 = 0;
			if (datatable.Rows.Count > 1)
			{
				CheckFfistRow();
				num = Daily.Insert(out var TRNCode, TxtDate.DateTime, TxtNumNote.Text, TxtNotes.Text);
				Daily.InsertItems(datatable, TRNCode);
				num2 = Daily.InsertTran(out var TRNCode2, TRNCode, TxtNumNote.Text, TxtDate.DateTime, TxtNotes.Text);
				Daily.InsertItemsTran(datatable, TxtDate.DateTime.Month, TRNCode2, ISEdit: false);
				gridControl1.DataSource = datatable;
				gridView1.OptionsBehavior.Editable = false;
				if (num > 0)
				{
					PrintID = num;
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					IsEnableFalse();
					TxtCode.Text = TRNCode.ToString();
					switch (MessageBox.Show("تم حفظ بيانات صرف الرواتب بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						Print();
						break;
					}
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات صرف الرواتب رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 78))
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
			int num2 = 0;
			int TRNCode = 0;
			if (datatable.Rows.Count > 1)
			{
				ArrangeDatatable();
				CheckFfistRow();
				num2 = Daily.DeleteDetails(Convert.ToInt32(TxtCode.Text));
				num2 = Daily.DeleteTranDetails(Convert.ToInt32(TxtCode.Text));
				num = Daily.Update(Convert.ToInt32(TxtCode.Text), TxtDate.DateTime, TxtNumNote.Text, TxtNotes.Text);
				num = Daily.UpdateTran(out TRNCode, Convert.ToInt32(TxtCode.Text), TxtNumNote.Text, TxtDate.DateTime, TxtNotes.Text);
				Daily.InsertItems(datatable, Convert.ToInt32(TxtCode.Text));
				Daily.InsertItemsTran(datatable, TxtDate.DateTime.Month, TRNCode, ISEdit: true);
				gridControl1.DataSource = datatable;
				gridView1.OptionsBehavior.Editable = false;
				if (num > 0)
				{
					PrintID = num;
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					BtnEdit.Text = "F6 تعديل";
					IsEnableFalse();
					switch (MessageBox.Show("تم تعديل بيانات صرف الرواتب بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						Print();
						break;
					}
				}
				else
				{
					MessageBox.Show("تم تعديل بيانات صرف الرواتب رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 78))
		{
			int num = 0;
			int num2 = 0;
			if (TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف صرف الرواتب رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = Daily.DeleteDetails(Convert.ToInt32(TxtCode.Text));
					num = Daily.DeleteTranDetails(Convert.ToInt32(TxtCode.Text));
					num2 = Daily.Delete(Convert.ToInt32(TxtCode.Text));
					num2 = Daily.DeleteTran(Convert.ToInt32(TxtCode.Text));
					if (num > 0 && num2 > 0)
					{
						MessageBox.Show("تم حذف بيانات صرف الرواتب رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						gridView1.OptionsBehavior.Editable = true;
						IsEnableTrue();
						BtnDelete.Enabled = false;
						BtnEdit.Enabled = false;
						BtnSave.Enabled = true;
						insertnewrow();
						TxtDate.Focus();
						PrintID = 0;
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات صرف الرواتب رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات صرف الرواتب بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void SearchAccount()
	{
		FrmAccountPayroll frmAccountPayroll = new FrmAccountPayroll();
		frmAccountPayroll.ShowDialog();
		if (frmAccountPayroll.CustCode > 0)
		{
			int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
			foreach (DataRow row in datatable.Rows)
			{
				if (row["ظ…"].ToString() == Convert.ToString(num))
				{
					Emp_Employee emp_Employee = Daily.GetEmp(frmAccountPayroll.CustCode).FirstOrDefault();
					row["ظ…"] = 1;
					row["AccountID"] = emp_Employee.AccountNo;
					row["رقم الحساب"] = emp_Employee.ID;
					row["اسم الموظف"] = emp_Employee.Name;
					row["الراتب"] = emp_Employee.Money;
					row["رقم حساب الصندوق/البنك"] = LoginDetails.Acc_CashBox;
					row["الصندوق/البنك"] = Daily.GetAccountName(Convert.ToInt32(LoginDetails.Acc_CashBox));
					row["أيام غياب"] = 0;
					row["OverTime"] = 0;
					row["OverTimeValue"] = 0;
					int empAccount = Daily.GetEmpAccount(Convert.ToInt32(emp_Employee.AccountNo));
					row["تسديد سلفة"] = Daily.Ledger_Employee(empAccount);
					row["جزاءات"] = 0.0;
					row["مكافئات"] = 0.0;
					row["Delays"] = 0.0;
					row["DelaysValue"] = 0.0;
					row["Vacations"] = 0.0;
					row["Bouns"] = 0.0;
					row["ValueOfDaysAbsence"] = 0.0;
					row["NetBadl"] = Math.Round(Convert.ToDecimal(emp_Employee.NetAllowances), 2);
					row["صافي الراتب"] = emp_Employee.Money - (decimal?)Convert.ToDecimal(row["تسديد سلفة"]) + (decimal?)Convert.ToDecimal(row["NetBadl"]);
					row["مركز التكلفة"] = 0;
					row["اسم مركز التكلفة"] = string.Empty;
					row["مسح"] = 0;
					row["تم"] = 0;
				}
			}
			CheckFfistRow();
			insertnewrow();
			ArrangeDatatable();
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[1];
			gridView1.ShowEditor();
			frmAccountPayroll.Close();
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
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 78))
		{
			if (TxtCode.Text != "")
			{
				FrmSelectEmp frmSelectEmp = new FrmSelectEmp(datatable, Convert.ToInt32(TxtCode.Text), TxtDate.DateTime);
				frmSelectEmp.ShowDialog();
			}
			else
			{
				MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void PrintAll()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 78))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["TableEmpPayroll"].Clear();
				SqlCommand sqlCommand = new SqlCommand("[Print_Emp_Payroll_All]", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
				sqlDataAdapter.Fill(dataSetALL.TableEmpPayroll);
				sqlConnection.Close();
				if (dataSetALL.Tables["TableEmpPayroll"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					if (LoginDetails.Lang == "ar-EG")
					{
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintEmpPayrollByAll.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintEmpPayrollByAll.mrt");
					}
					stiReport.RegData(dataSetALL.TableEmpPayroll);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
					stiReport["Address"] = LoginDetails.Address;
					stiReport["Phone"] = LoginDetails.Phone;
					stiReport["Bank"] = LoginDetails.CompanyBank;
					stiReport["Date"] = TxtDate.DateTime.ToString("yyyy/MMMM");
					stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
					stiReport.Render();
					stiReport.Show(dialogForm: true);
					FrmReportGetOrder frmReportGetOrder = new FrmReportGetOrder();
					frmReportGetOrder.stiViewerControl1.Report = stiReport;
					PrinterSettings printerSettings = new PrinterSettings();
					printerSettings.PrinterName = LoginDetails.DefualtPrint;
					printerSettings.DefaultPageSettings.PaperSize = new PaperSize("Custom", 80, 3276);
					stiReport.Print(showPrintDialog: false, printerSettings);
				}
				else
				{
					MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		BtnEdit.Text = "تعديل F6";
	}

	public void IsEnableFalse()
	{
		TxtNumNote.ReadOnly = true;
		TxtNotes.ReadOnly = true;
		TxtDate.ReadOnly = true;
		TxtNumNote.BackColor = Color.White;
		TxtNotes.BackColor = Color.White;
		TxtDate.BackColor = Color.White;
	}

	public void GetDailyRestrictionsTest()
	{
		try
		{
			FrmSearchPayrollTemp frmSearchPayrollTemp = new FrmSearchPayrollTemp();
			frmSearchPayrollTemp.ShowDialog();
			if (frmSearchPayrollTemp.CustCode > 0)
			{
				int custCode = frmSearchPayrollTemp.CustCode;
				Emp_PayrollTemp emp_PayrollTemp = DailyTemp.GetPayroll(custCode).FirstOrDefault();
				TxtCode.Text = "";
				TxtNumNote.Text = emp_PayrollTemp.NumNote.ToString();
				TxtDate.Text = Convert.ToDateTime(emp_PayrollTemp.PayrollDate).ToShortDateString();
				TxtNotes.Text = emp_PayrollTemp.Note;
				datatable.Rows.Clear();
				List<Emp_PayrollTempDetails> payrollDetails = DailyTemp.GetPayrollDetails(custCode);
				int num = 1;
				foreach (Emp_PayrollTempDetails item in payrollDetails)
				{
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					dataRow["AccountID"] = item.AccountNum;
					dataRow["رقم الحساب"] = item.EmpID;
					dataRow["اسم الموظف"] = DailyTemp.GetEmpName(Convert.ToInt32(item.EmpID));
					dataRow["الراتب"] = Math.Round(Convert.ToDecimal(item.Salary), 2);
					dataRow["رقم حساب الصندوق/البنك"] = Convert.ToInt32(item.AccountBankBox);
					dataRow["الصندوق/البنك"] = DailyTemp.GetAccountName(Convert.ToInt32(item.AccountBankBox));
					dataRow["أيام غياب"] = Math.Round(Convert.ToDecimal(item.DaysOfBsence), 2);
					dataRow["OverTime"] = Math.Round(Convert.ToDecimal(item.DaysExtra), 2);
					dataRow["تسديد سلفة"] = Math.Round(Convert.ToDecimal(item.PaymentOfAdvance), 2);
					dataRow["جزاءات"] = Math.Round(Convert.ToDecimal(item.Deduct), 2);
					dataRow["مكافئات"] = Math.Round(Convert.ToDecimal(item.Merit), 2);
					dataRow["NetBadl"] = Math.Round(Convert.ToDecimal(item.NetAllowances), 2);
					dataRow["Delays"] = Math.Round(Convert.ToDecimal(item.Delays), 2);
					dataRow["Vacations"] = Math.Round(Convert.ToDecimal(item.Vacations), 2);
					dataRow["Bouns"] = Math.Round(Convert.ToDecimal(item.Bouns), 2);
					dataRow["صافي الراتب"] = item.NetSalary;
					dataRow["مركز التكلفة"] = item.CostCentersID;
					dataRow["اسم مركز التكلفة"] = (from x in Cost.GetSearchAccountCostCenter()
						where x.CostCentersID == Convert.ToInt32(item.CostCentersID)
						select x).FirstOrDefault().CostCentersName;
					dataRow["مسح"] = 0;
					dataRow["OverTimeValue"] = 0;
					dataRow["DelaysValue"] = 0;
					dataRow["ValueOfDaysAbsence"] = 0;
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				PrintID = custCode;
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = false;
				gridView1.OptionsBehavior.Editable = true;
				insertnewrow();
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
			FrmSearchPayroll frmSearchPayroll = new FrmSearchPayroll();
			frmSearchPayroll.ShowDialog();
			if (frmSearchPayroll.CustCode > 0)
			{
				int custCode = frmSearchPayroll.CustCode;
				Emp_Payroll emp_Payroll = Daily.GetPayroll(custCode).FirstOrDefault();
				TxtCode.Text = emp_Payroll.ID.ToString();
				TxtNumNote.Text = emp_Payroll.NumNote.ToString();
				TxtDate.Text = Convert.ToDateTime(emp_Payroll.PayrollDate).ToShortDateString();
				TxtNotes.Text = emp_Payroll.Note;
				datatable.Rows.Clear();
				List<Emp_PayrollDetails> payrollDetails = Daily.GetPayrollDetails(custCode);
				int num = 1;
				foreach (Emp_PayrollDetails item in payrollDetails)
				{
					DataRow dataRow = null;
					dataRow = datatable.NewRow();
					dataRow["AccountID"] = item.AccountNum;
					dataRow["رقم الحساب"] = item.EmpID;
					dataRow["اسم الموظف"] = Daily.GetEmpName(Convert.ToInt32(item.EmpID));
					dataRow["الراتب"] = Math.Round(Convert.ToDecimal(item.Salary), 2);
					dataRow["رقم حساب الصندوق/البنك"] = Convert.ToInt32(item.AccountBankBox);
					dataRow["الصندوق/البنك"] = Daily.GetAccountName(Convert.ToInt32(item.AccountBankBox));
					dataRow["أيام غياب"] = Math.Round(Convert.ToDecimal(item.DaysOfBsence), 2);
					dataRow["OverTime"] = Math.Round(Convert.ToDecimal(item.HourExtra), 2);
					dataRow["OverTimeValue"] = Math.Round(Convert.ToDecimal(item.HourExtraValue), 2);
					dataRow["تسديد سلفة"] = Math.Round(Convert.ToDecimal(item.PaymentOfAdvance), 2);
					dataRow["جزاءات"] = Math.Round(Convert.ToDecimal(item.Deduct), 2);
					dataRow["مكافئات"] = Math.Round(Convert.ToDecimal(item.Merit), 2);
					dataRow["صافي الراتب"] = Math.Round(Convert.ToDecimal(item.NetSalary), 2);
					dataRow["NetBadl"] = Math.Round(Convert.ToDecimal(item.NetAllowances), 2);
					dataRow["Delays"] = Math.Round(Convert.ToDecimal(item.Delays), 2);
					dataRow["DelaysValue"] = Math.Round(Convert.ToDecimal(item.HourDelaysValue), 2);
					dataRow["Vacations"] = Math.Round(Convert.ToDecimal(item.Vacations), 2);
					dataRow["Bouns"] = Math.Round(Convert.ToDecimal(item.Bouns), 2);
					dataRow["ValueOfDaysAbsence"] = Math.Round(Convert.ToDecimal(item.DaysOfBsenceValue), 2);
					dataRow["مركز التكلفة"] = item.CostCentersID;
					dataRow["اسم مركز التكلفة"] = (from x in Cost.GetSearchAccountCostCenter()
						where x.CostCentersID == Convert.ToInt32(item.CostCentersID)
						select x).FirstOrDefault().CostCentersName;
					dataRow["مسح"] = 0;
					datatable.Rows.Add(dataRow);
					num++;
				}
				gridControl1.DataSource = datatable;
				PrintID = custCode;
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnEdit.Text = "F6 تعديل";
				BtnDelete.Enabled = true;
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

	private void BtnAddFromTest_Click(object sender, EventArgs e)
	{
		GetDailyRestrictionsTest();
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

	private void MenuPrintQuaid_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text != "")
		{
			Class_Print_DailyRestrictionsALL class_Print_DailyRestrictionsALL = new Class_Print_DailyRestrictionsALL();
			class_Print_DailyRestrictionsALL.Print_DailyRestrictionsALL(Convert.ToInt32(TxtCode.Text), 36);
		}
	}

	private void FrmPayroll_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameAR);
		}
		else
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameEN);
		}
	}

	private void BtnPrintAll_Click(object sender, EventArgs e)
	{
		PrintAll();
	}

	private void TxtNote_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Return)
		{
			groupBox2.Focus();
			gridView1.Focus();
		}
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Payroll.FrmPayroll));
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
		this.label24 = new System.Windows.Forms.Label();
		this.TxtNumNote = new DevExpress.XtraEditors.TextEdit();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnPrintAll = new DevExpress.XtraEditors.SimpleButton();
		this.BtnAddFromTest = new DevExpress.XtraEditors.SimpleButton();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtNotes = new System.Windows.Forms.TextBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonBankBox = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
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
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.MenuNew = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.RecoveredMenuItemFix1 = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintFatoura = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuPrintQuaid = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuSearchItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuPurchOrder = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.RecoveredMenuItemFix2 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonBankBox).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemComboBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.TxtNumNote);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
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
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.BtnPrintAll);
		this.groupBox2.Controls.Add(this.BtnAddFromTest);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.TxtNotes);
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.BtnPrintAll, "BtnPrintAll");
		this.BtnPrintAll.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrintAll.ImageOptions.Image");
		this.BtnPrintAll.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrintAll.Name = "BtnPrintAll";
		this.BtnPrintAll.Click += new System.EventHandler(BtnPrintAll_Click);
		resources.ApplyResources(this.BtnAddFromTest, "BtnAddFromTest");
		this.BtnAddFromTest.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAddFromTest.ImageOptions.Image");
		this.BtnAddFromTest.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnAddFromTest.Name = "BtnAddFromTest";
		this.BtnAddFromTest.Click += new System.EventHandler(BtnAddFromTest_Click);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.TxtNotes, "TxtNotes");
		this.TxtNotes.Name = "TxtNotes";
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
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[13]
		{
			this.repositoryItemButtonEdit4, this.repositoryItemButtonEdit6, this.repositoryItemTextEdit1, this.repositoryItemTextEdit2, this.repositoryItemDateEdit1, this.repositoryItemTextEdit3, this.repositoryItemPictureEdit1, this.repositoryItemButtonEdit5, this.repositoryItemButtonEditCodeSearch, this.repositoryItemComboBox1,
			this.repositoryItemLookUpEdit1, this.repositoryItemButtonBankBox, this.repositoryItemButtonEdit1
		});
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.FilterPanel.BackColor = System.Drawing.Color.Gray;
		this.gridView1.Appearance.FilterPanel.Options.UseBackColor = true;
		resources.ApplyResources(this.gridView1, "gridView1");
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[23]
		{
			this.gridColumn9, this.gridColumn1, this.gridColumn2, this.gridColumn21, this.gridColumn20, this.gridColumn3, this.gridColumn22, this.gridColumn5, this.gridColumn19, this.gridColumn23,
			this.gridColumn6, this.gridColumn7, this.gridColumn8, this.gridColumn24, this.gridColumn25, this.gridColumn26, this.gridColumn27, this.gridColumn28, this.gridColumn4, this.gridColumn29,
			this.gridColumn30, this.gridColumn31, this.gridColumn32
		});
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
		this.gridColumn9.MinWidth = 17;
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn1, "gridColumn1");
		this.gridColumn1.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceCell.Font");
		this.gridColumn1.AppearanceCell.Options.UseFont = true;
		this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceHeader.Font");
		this.gridColumn1.AppearanceHeader.Options.UseFont = true;
		this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.ColumnEdit = this.repositoryItemButtonEditCodeSearch;
		this.gridColumn1.FieldName = "رقم الحساب";
		this.gridColumn1.MinWidth = 17;
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
		this.gridColumn2.FieldName = "اسم الموظف";
		this.gridColumn2.MinWidth = 17;
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
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
		this.gridColumn21.ColumnEdit = this.repositoryItemButtonBankBox;
		this.gridColumn21.FieldName = "الصندوق/البنك";
		this.gridColumn21.MinWidth = 17;
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.repositoryItemButtonBankBox, "repositoryItemButtonBankBox");
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.repositoryItemButtonBankBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonBankBox.Buttons"), resources.GetString("repositoryItemButtonBankBox.Buttons1"), (int)resources.GetObject("repositoryItemButtonBankBox.Buttons2"), (bool)resources.GetObject("repositoryItemButtonBankBox.Buttons3"), (bool)resources.GetObject("repositoryItemButtonBankBox.Buttons4"), (bool)resources.GetObject("repositoryItemButtonBankBox.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("repositoryItemButtonBankBox.Buttons6"), resources.GetObject("repositoryItemButtonBankBox.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonBankBox.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonBankBox.Buttons9"))
		});
		this.repositoryItemButtonBankBox.Name = "repositoryItemButtonBankBox";
		this.repositoryItemButtonBankBox.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonBankBox_ButtonClick);
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
		this.gridColumn20.FieldName = "الراتب";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.MinWidth = 17;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn20.Summary"), resources.GetString("gridColumn20.Summary1"), resources.GetString("gridColumn20.Summary2"))
		});
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn3, "gridColumn3");
		this.gridColumn3.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceCell.Font");
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceHeader.Font");
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.FieldName = "أيام غياب";
		this.gridColumn3.MinWidth = 17;
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn3.Summary"), resources.GetString("gridColumn3.Summary1"), resources.GetString("gridColumn3.Summary2"))
		});
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn22, "gridColumn22");
		this.gridColumn22.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceCell.Font");
		this.gridColumn22.AppearanceCell.Options.UseFont = true;
		this.gridColumn22.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn22.AppearanceHeader.Font");
		this.gridColumn22.AppearanceHeader.Options.UseFont = true;
		this.gridColumn22.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn22.FieldName = "تسديد سلفة";
		this.gridColumn22.MinWidth = 17;
		this.gridColumn22.Name = "gridColumn22";
		this.gridColumn22.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn22.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn5, "gridColumn5");
		this.gridColumn5.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceCell.Font");
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceHeader.Font");
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.FieldName = "جزاءات";
		this.gridColumn5.MinWidth = 17;
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceCell.Font");
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.FieldName = "مكافئات";
		this.gridColumn19.MinWidth = 17;
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn19.Summary"), resources.GetString("gridColumn19.Summary1"), resources.GetString("gridColumn19.Summary2"))
		});
		this.gridColumn19.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn23, "gridColumn23");
		this.gridColumn23.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceCell.Font");
		this.gridColumn23.AppearanceCell.Options.UseFont = true;
		this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn23.AppearanceHeader.Font");
		this.gridColumn23.AppearanceHeader.Options.UseFont = true;
		this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn23.FieldName = "صافي الراتب";
		this.gridColumn23.MinWidth = 17;
		this.gridColumn23.Name = "gridColumn23";
		this.gridColumn23.OptionsColumn.AllowEdit = false;
		this.gridColumn23.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn23.Summary"), resources.GetString("gridColumn23.Summary1"), resources.GetString("gridColumn23.Summary2"))
		});
		this.gridColumn23.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
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
		this.gridColumn6.MinWidth = 17;
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
		editorButtonImageOptions3.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		editorButtonImageOptions3.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
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
		this.gridColumn7.MinWidth = 17;
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
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
		this.gridColumn8.MinWidth = 17;
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit6, "repositoryItemButtonEdit6");
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
		resources.ApplyResources(this.gridColumn24, "gridColumn24");
		this.gridColumn24.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceCell.Font");
		this.gridColumn24.AppearanceCell.Options.UseFont = true;
		this.gridColumn24.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn24.AppearanceHeader.Font");
		this.gridColumn24.AppearanceHeader.Options.UseFont = true;
		this.gridColumn24.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn24.ColumnEdit = this.repositoryItemButtonEdit1;
		this.gridColumn24.FieldName = "Badl";
		this.gridColumn24.MinWidth = 25;
		this.gridColumn24.Name = "gridColumn24";
		this.gridColumn24.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions5");
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonPressed);
		resources.ApplyResources(this.gridColumn25, "gridColumn25");
		this.gridColumn25.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceCell.Font");
		this.gridColumn25.AppearanceCell.Options.UseFont = true;
		this.gridColumn25.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn25.AppearanceHeader.Font");
		this.gridColumn25.AppearanceHeader.Options.UseFont = true;
		this.gridColumn25.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn25.FieldName = "NetBadl";
		this.gridColumn25.MinWidth = 25;
		this.gridColumn25.Name = "gridColumn25";
		this.gridColumn25.OptionsColumn.AllowEdit = false;
		this.gridColumn25.OptionsColumn.AllowFocus = false;
		this.gridColumn25.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn25.Summary"), resources.GetString("gridColumn25.Summary1"), resources.GetString("gridColumn25.Summary2"))
		});
		resources.ApplyResources(this.gridColumn26, "gridColumn26");
		this.gridColumn26.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceCell.Font");
		this.gridColumn26.AppearanceCell.Options.UseFont = true;
		this.gridColumn26.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn26.AppearanceHeader.Font");
		this.gridColumn26.AppearanceHeader.Options.UseFont = true;
		this.gridColumn26.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn26.FieldName = "Delays";
		this.gridColumn26.MinWidth = 25;
		this.gridColumn26.Name = "gridColumn26";
		this.gridColumn26.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn26.Summary"), resources.GetString("gridColumn26.Summary1"), resources.GetString("gridColumn26.Summary2"))
		});
		resources.ApplyResources(this.gridColumn27, "gridColumn27");
		this.gridColumn27.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceCell.Font");
		this.gridColumn27.AppearanceCell.Options.UseFont = true;
		this.gridColumn27.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn27.AppearanceHeader.Font");
		this.gridColumn27.AppearanceHeader.Options.UseFont = true;
		this.gridColumn27.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn27.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn27.FieldName = "Vacations";
		this.gridColumn27.MinWidth = 25;
		this.gridColumn27.Name = "gridColumn27";
		this.gridColumn27.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn27.Summary"), resources.GetString("gridColumn27.Summary1"), resources.GetString("gridColumn27.Summary2"))
		});
		resources.ApplyResources(this.gridColumn28, "gridColumn28");
		this.gridColumn28.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn28.AppearanceCell.Font");
		this.gridColumn28.AppearanceCell.Options.UseFont = true;
		this.gridColumn28.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn28.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn28.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn28.AppearanceHeader.Font");
		this.gridColumn28.AppearanceHeader.Options.UseFont = true;
		this.gridColumn28.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn28.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn28.FieldName = "Bouns";
		this.gridColumn28.MinWidth = 25;
		this.gridColumn28.Name = "gridColumn28";
		this.gridColumn28.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn28.Summary"), resources.GetString("gridColumn28.Summary1"), resources.GetString("gridColumn28.Summary2"))
		});
		resources.ApplyResources(this.gridColumn4, "gridColumn4");
		this.gridColumn4.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceCell.Font");
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceHeader.Font");
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.FieldName = "OverTime";
		this.gridColumn4.MinWidth = 25;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn4.Summary"), resources.GetString("gridColumn4.Summary1"), resources.GetString("gridColumn4.Summary2"))
		});
		resources.ApplyResources(this.gridColumn29, "gridColumn29");
		this.gridColumn29.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceCell.Font");
		this.gridColumn29.AppearanceCell.Options.UseFont = true;
		this.gridColumn29.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn29.AppearanceHeader.Font");
		this.gridColumn29.AppearanceHeader.Options.UseFont = true;
		this.gridColumn29.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn29.FieldName = "Select";
		this.gridColumn29.MinWidth = 25;
		this.gridColumn29.Name = "gridColumn29";
		this.gridColumn29.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem()
		});
		resources.ApplyResources(this.gridColumn30, "gridColumn30");
		this.gridColumn30.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn30.AppearanceCell.Font");
		this.gridColumn30.AppearanceCell.Options.UseFont = true;
		this.gridColumn30.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn30.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn30.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn30.AppearanceHeader.Font");
		this.gridColumn30.AppearanceHeader.Options.UseFont = true;
		this.gridColumn30.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn30.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn30.FieldName = "OverTimeValue";
		this.gridColumn30.MinWidth = 25;
		this.gridColumn30.Name = "gridColumn30";
		this.gridColumn30.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn30.Summary"), resources.GetString("gridColumn30.Summary1"), resources.GetString("gridColumn30.Summary2"))
		});
		resources.ApplyResources(this.gridColumn31, "gridColumn31");
		this.gridColumn31.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn31.AppearanceCell.Font");
		this.gridColumn31.AppearanceCell.Options.UseFont = true;
		this.gridColumn31.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn31.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn31.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn31.AppearanceHeader.Font");
		this.gridColumn31.AppearanceHeader.Options.UseFont = true;
		this.gridColumn31.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn31.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn31.FieldName = "DelaysValue";
		this.gridColumn31.MinWidth = 25;
		this.gridColumn31.Name = "gridColumn31";
		this.gridColumn31.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn31.Summary"), resources.GetString("gridColumn31.Summary1"), resources.GetString("gridColumn31.Summary2"))
		});
		resources.ApplyResources(this.gridColumn32, "gridColumn32");
		this.gridColumn32.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn32.AppearanceCell.Font");
		this.gridColumn32.AppearanceCell.Options.UseFont = true;
		this.gridColumn32.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn32.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn32.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn32.AppearanceHeader.Font");
		this.gridColumn32.AppearanceHeader.Options.UseFont = true;
		this.gridColumn32.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn32.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn32.FieldName = "ValueOfDaysAbsence";
		this.gridColumn32.MinWidth = 25;
		this.gridColumn32.Name = "gridColumn32";
		this.gridColumn32.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem((DevExpress.Data.SummaryItemType)resources.GetObject("gridColumn32.Summary"), resources.GetString("gridColumn32.Summary1"), resources.GetString("gridColumn32.Summary2"))
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
		resources.ApplyResources(this.repositoryItemTextEdit3, "repositoryItemTextEdit3");
		this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
		this.repositoryItemPictureEdit1.Appearance.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceDisabled.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceReadOnly.Options.UseImage = true;
		this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
		this.repositoryItemPictureEdit1.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions6");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"), resources.GetString("repositoryItemButtonEdit5.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit5.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit5.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("repositoryItemButtonEdit5.Buttons6"), resources.GetObject("repositoryItemButtonEdit5.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit5.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit5.Buttons9"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEdit5.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
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
		this.cardView1.CardWidth = 171;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.DetailHeight = 284;
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		resources.ApplyResources(this.gridColumn10, "gridColumn10");
		this.gridColumn10.MinWidth = 17;
		this.gridColumn10.Name = "gridColumn10";
		resources.ApplyResources(this.gridColumn11, "gridColumn11");
		this.gridColumn11.MinWidth = 17;
		this.gridColumn11.Name = "gridColumn11";
		resources.ApplyResources(this.gridColumn12, "gridColumn12");
		this.gridColumn12.MinWidth = 17;
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn13, "gridColumn13");
		this.gridColumn13.MinWidth = 17;
		this.gridColumn13.Name = "gridColumn13";
		resources.ApplyResources(this.gridColumn14, "gridColumn14");
		this.gridColumn14.MinWidth = 17;
		this.gridColumn14.Name = "gridColumn14";
		resources.ApplyResources(this.gridColumn15, "gridColumn15");
		this.gridColumn15.MinWidth = 17;
		this.gridColumn15.Name = "gridColumn15";
		resources.ApplyResources(this.gridColumn16, "gridColumn16");
		this.gridColumn16.MinWidth = 17;
		this.gridColumn16.Name = "gridColumn16";
		resources.ApplyResources(this.gridColumn17, "gridColumn17");
		this.gridColumn17.MinWidth = 17;
		this.gridColumn17.Name = "gridColumn17";
		resources.ApplyResources(this.gridColumn18, "gridColumn18");
		this.gridColumn18.MinWidth = 17;
		this.gridColumn18.Name = "gridColumn18";
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[13]
		{
			this.MenuNew, this.MenuSave, this.MenuEdit, this.MenuDelete, this.RecoveredMenuItemFix1, this.toolStripSeparator1, this.MenuSearchItem, this.toolStripSeparator4, this.MenuPurchOrder, this.toolStripSeparator2,
			this.RecoveredMenuItemFix2, this.toolStripSeparator3, this.MenuExit
		});
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		resources.ApplyResources(this.MenuNew, "MenuNew");
		this.MenuNew.Name = "MenuNew";
		this.MenuNew.Click += new System.EventHandler(BtnNew_Click);
		resources.ApplyResources(this.MenuSave, "MenuSave");
		this.MenuSave.Name = "MenuSave";
		this.MenuSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.MenuEdit, "MenuEdit");
		this.MenuEdit.Name = "MenuEdit";
		this.MenuEdit.Click += new System.EventHandler(BtnEdit_Click);
		resources.ApplyResources(this.MenuDelete, "MenuDelete");
		this.MenuDelete.Name = "MenuDelete";
		this.MenuDelete.Click += new System.EventHandler(BtnDelete_Click);
		resources.ApplyResources(this.RecoveredMenuItemFix1, "طباعةToolStripMenuItem1");
		this.RecoveredMenuItemFix1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.MenuPrintFatoura, this.MenuPrintQuaid });
		this.RecoveredMenuItemFix1.Name = "طباعةToolStripMenuItem1";
		resources.ApplyResources(this.MenuPrintFatoura, "MenuPrintFatoura");
		this.MenuPrintFatoura.Name = "MenuPrintFatoura";
		this.MenuPrintFatoura.Click += new System.EventHandler(BtnPrint_Click);
		resources.ApplyResources(this.MenuPrintQuaid, "MenuPrintQuaid");
		this.MenuPrintQuaid.Name = "MenuPrintQuaid";
		this.MenuPrintQuaid.Click += new System.EventHandler(MenuPrintQuaid_Click);
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.MenuSearchItem, "MenuSearchItem");
		this.MenuSearchItem.Name = "MenuSearchItem";
		this.MenuSearchItem.Click += new System.EventHandler(MenuSearchItem_Click);
		resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		resources.ApplyResources(this.MenuPurchOrder, "MenuPurchOrder");
		this.MenuPurchOrder.Name = "MenuPurchOrder";
		this.MenuPurchOrder.Click += new System.EventHandler(MenuPurchOrder_Click);
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.RecoveredMenuItemFix2, "تخصيصToolStripMenuItem1");
		this.RecoveredMenuItemFix2.Name = "تخصيصToolStripMenuItem1";
		resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		resources.ApplyResources(this.MenuExit, "MenuExit");
		this.MenuExit.Name = "MenuExit";
		this.MenuExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ContextMenuStrip = this.contextMenuStrip1;
		base.Controls.Add(this.gridControl1);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.BtnExit);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmPayroll";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmPayroll_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNumNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonBankBox).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemComboBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


