using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.FrmsMain.Search;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Reports.AllBranch;

public class FrmMovementsDailyReportAllBran2 : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private Class_DailyRestrictions Daily = new Class_DailyRestrictions();

	private DataSetALL ST = new DataSetALL();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private int tafqitID = LoginDetails.TafqitID;

	public string txtWordAr;

	public string txtWordEn;

	private IContainer components = null;

	private Label label1;

	private Label label2;

	private Label label6;

	private Label label3;

	private TextBox TxtOrdCash;

	private TextBox TxtOrdAjil;

	private Label label7;

	private TextBox TxtOrdRTNAjil;

	private Label label8;

	private TextBox TxtOrdRTNCash;

	private Label label9;

	private SeparatorControl separatorControl1;

	private TextBox TxtOrdNetAjil;

	private Label label10;

	private TextBox TxtOrdNetCash;

	private Label label11;

	private GroupBox groupBox1;

	private Label LblTafqit;

	private TextBox TxtBank;

	private Label label22;

	private TextBox TxtCash;

	private Label label21;

	private TextBox TxtNet;

	private SeparatorControl separatorControl4;

	private TextBox TxtExpenses;

	private Label label18;

	private TextBox TxtIncome;

	private Label label19;

	private SeparatorControl separatorControl3;

	private TextBox TxtPurchCash;

	private TextBox TxtPurchNetAjil;

	private Label label12;

	private Label label13;

	private Label label14;

	private TextBox TxtPurchNetCash;

	private Label label15;

	private TextBox TxtPurchAjil;

	private TextBox TxtPurchRtnCash;

	private Label label16;

	private TextBox TxtPurchRtnAjil;

	private Label label17;

	private SeparatorControl separatorControl2;

	private DateEdit TxtDate1;

	private DateEdit TxtDate2;

	private SimpleButton BtnSearch;

	private SimpleButton BtnPrint;

	private Label LblAccountbox;

	private TextBox TxtExpensesNet;

	private Label label20;

	private TextBox TxtIncomeNet;

	private Label label5;

	private TimeEdit TxtTime1;

	private TimeEdit TxtTime2;

	private SeparatorControl separatorControl5;

	private TextBox TxtTransferToCash;

	private Label label26;

	private Label label25;

	private TextBox TxtTransferFromCash;

	private TextBox TxtTransferToAjil;

	private Label label24;

	private TextBox TxtTransferFromAjil;

	private Label label23;

	private Label label27;

	private Label LblAccount;

	private Label label4;

	private ButtonEdit TxtAccount;

	private SimpleButton BtnSave;

	public FrmMovementsDailyReportAllBran2()
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
		LblAccountbox.Text = LoginDetails.Acc_CashBox.ToString();
		TxtDate1.Text = DateTime.Now.ToString("yyyy/MM/dd");
		TxtDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");
		TxtTime1.Text = "00:00:00";
		TxtTime2.Text = "23:59:59";
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
	}

	private void BtnSearch_Click(object sender, EventArgs e)
	{
		Search();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		try
		{
			Save();
			Search();
		}
		catch (Exception)
		{
			throw;
		}
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		if (ST.Tables["MovementsDailyReport"].Rows.Count > 0)
		{
			StiReport stiReport = new StiReport();
			stiReport.Load(Application.StartupPath + "\\Reports\\PrintMovementsDailyReport.mrt");
			stiReport.RegData(ST.MovementsDailyReport);
			stiReport.Dictionary.Synchronize();
			stiReport.Compile();
			stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
			stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
			stiReport["VatNumComp"] = LoginDetails.VatNum;
			stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
			stiReport["Address"] = LoginDetails.Address;
			stiReport["Phone"] = LoginDetails.Phone;
			stiReport["Bank"] = LoginDetails.CompanyBank;
			stiReport["Date1"] = Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text);
			stiReport["Date2"] = Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text);
			stiReport["Tafqit"] = txtWordAr;
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

	private void TxtDepit_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			LoginDetails.FrmName = "FrmMovementsDaily";
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblAccount.Text = custCode.ToString();
				TxtAccount.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtAccount.Focus();
			}
			LoginDetails.FrmName = "";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 41))
		{
			int num = 0;
			int TxtCode = 0;
			if (TxtNet.Text != "" && Convert.ToDecimal(TxtNet.Text) > 0m && TxtAccount.Text != "")
			{
				string value = DateTime.Now.ToShortDateString();
				num = Daily.InsertTran(out var TRNCode, out TxtCode, "", Convert.ToDateTime(value), "قيد اغلاق حركة يومية من فترة " + TxtDate1.Text + " الي فترة " + TxtDate2.Text);
				num = Daily.InsertItems(TRNCode, Convert.ToInt32(LblAccount.Text), Convert.ToInt32(LblAccountbox.Text), "قيد اغلاق حركة يومية من فترة " + TxtDate1.Text + " الي فترة " + TxtDate2.Text, Convert.ToDecimal(TxtNet.Text), 0, IsEdit: false, 0);
				if (num > 0)
				{
					MessageBox.Show("تم حفظ قيد اغلاق حركة يومية بنجاح", "نجاح الاغلاق", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else if (Convert.ToDecimal(TxtNet.Text) == 0m)
			{
				MessageBox.Show("لم يتم الحفظ، لعدم توفر نقود في الصندوق النقدي ", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
			else if (TxtAccount.Text == "")
			{
				MessageBox.Show("لم يتم الحفظ، من فضلك اختار حساب الايداع ثم حاول مرة اخري ", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtAccount.Focus();
			}
			else
			{
				MessageBox.Show("لم يتم الحفظ ", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Search()
	{
		try
		{
			if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 41))
			{
				if (TxtDate1.Text != "" && TxtDate2.Text != "")
				{
					if (TxtDate1.DateTime <= TxtDate2.DateTime)
					{
						SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
						ST.Tables["MovementsDailyReport"].Clear();
						SqlCommand sqlCommand = new SqlCommand("GetReport_MovementsDailyReportAllBranch", sqlConnection);
						sqlCommand.CommandType = CommandType.StoredProcedure;
						SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
						string value = TxtDate1.Text + " " + TxtTime1.Text;
						string value2 = TxtDate2.Text + " " + TxtTime2.Text;
						sqlCommand.Parameters.AddWithValue("@Date1", Convert.ToDateTime(value));
						sqlCommand.Parameters.AddWithValue("@Date2", Convert.ToDateTime(value2));
						sqlDataAdapter.Fill(ST.MovementsDailyReport);
						sqlConnection.Close();
						if (ST.Tables["MovementsDailyReport"].Rows.Count > 0)
						{
							DataSetALL.MovementsDailyReportRow movementsDailyReportRow = ST.MovementsDailyReport.FirstOrDefault();
							TxtOrdCash.Text = movementsDailyReportRow.TotalOrderCash;
							TxtOrdRTNCash.Text = movementsDailyReportRow.TotalOrderCashReturn;
							TxtOrdNetCash.Text = movementsDailyReportRow.NetOrderCash;
							TxtOrdAjil.Text = movementsDailyReportRow.TotalOrderAjil;
							TxtOrdRTNAjil.Text = movementsDailyReportRow.TotalOrderAjilReturn;
							TxtOrdNetAjil.Text = movementsDailyReportRow.NetOrderAjil;
							TxtPurchCash.Text = movementsDailyReportRow.TotalPurshesCash;
							TxtPurchRtnCash.Text = movementsDailyReportRow.TotalPurshesCashReturn;
							TxtPurchNetCash.Text = movementsDailyReportRow.NetPurshesCash;
							TxtPurchAjil.Text = movementsDailyReportRow.TotalPurshesAjil;
							TxtPurchRtnAjil.Text = movementsDailyReportRow.TotalPurshesAjilReturn;
							TxtPurchNetAjil.Text = movementsDailyReportRow.NetPurshesAjil;
							TxtTransferToCash.Text = movementsDailyReportRow.TransferToBranchCash;
							TxtTransferToAjil.Text = movementsDailyReportRow.TransferToBranchAjil;
							TxtTransferFromCash.Text = movementsDailyReportRow.TransferFromBranchCash;
							TxtTransferFromAjil.Text = movementsDailyReportRow.TransferFromBranchAjil;
							TxtCash.Text = movementsDailyReportRow.CashMoney;
							TxtBank.Text = movementsDailyReportRow.CashBank;
							TxtIncome.Text = movementsDailyReportRow.TotalRecipt;
							TxtExpenses.Text = movementsDailyReportRow.TotalPayment;
							TxtIncomeNet.Text = movementsDailyReportRow.NetRecipt;
							TxtExpensesNet.Text = movementsDailyReportRow.NetPayment;
							TxtNet.Text = movementsDailyReportRow.Safy;
							ToWord toWord = new ToWord(Convert.ToDecimal(TxtNet.Text), currencies[tafqitID]);
							txtWordAr = toWord.ConvertToArabic();
							StiReport stiReport = new StiReport();
							LblTafqit.Text = txtWordAr;
						}
						else
						{
							MessageBox.Show("لا يوجد سجلات متاحة ", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					else
					{
						MessageBox.Show("لا يمكن ان يكون التاريخ الاول اكبر من التاريخ الثاني ", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						TxtDate1.Focus();
					}
				}
				else
				{
					MessageBox.Show("لا يمكن ان يكون التاريخ فارغ ", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					if (TxtDate1.Text != "")
					{
						TxtDate1.Text = DateTime.Now.ToShortDateString();
						TxtDate1.Focus();
					}
					else if (TxtDate2.Text != "")
					{
						TxtDate2.Text = DateTime.Now.ToShortDateString();
						TxtDate2.Focus();
					}
				}
			}
			else
			{
				MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception)
		{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.AllBranch.FrmMovementsDailyReportAllBran2));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtOrdCash = new System.Windows.Forms.TextBox();
		this.TxtOrdAjil = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.TxtOrdRTNAjil = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtOrdRTNCash = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
		this.TxtOrdNetAjil = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtOrdNetCash = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label27 = new System.Windows.Forms.Label();
		this.LblTafqit = new System.Windows.Forms.Label();
		this.TxtBank = new System.Windows.Forms.TextBox();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtCash = new System.Windows.Forms.TextBox();
		this.label21 = new System.Windows.Forms.Label();
		this.TxtNet = new System.Windows.Forms.TextBox();
		this.separatorControl4 = new DevExpress.XtraEditors.SeparatorControl();
		this.TxtExpensesNet = new System.Windows.Forms.TextBox();
		this.TxtExpenses = new System.Windows.Forms.TextBox();
		this.label20 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.TxtIncomeNet = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtIncome = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.separatorControl5 = new DevExpress.XtraEditors.SeparatorControl();
		this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
		this.TxtTransferToCash = new System.Windows.Forms.TextBox();
		this.TxtPurchCash = new System.Windows.Forms.TextBox();
		this.label26 = new System.Windows.Forms.Label();
		this.TxtPurchNetAjil = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.TxtPurchNetCash = new System.Windows.Forms.TextBox();
		this.TxtTransferFromCash = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtTransferToAjil = new System.Windows.Forms.TextBox();
		this.TxtPurchAjil = new System.Windows.Forms.TextBox();
		this.label24 = new System.Windows.Forms.Label();
		this.TxtPurchRtnCash = new System.Windows.Forms.TextBox();
		this.TxtTransferFromAjil = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtPurchRtnAjil = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
		this.TxtDate1 = new DevExpress.XtraEditors.DateEdit();
		this.TxtDate2 = new DevExpress.XtraEditors.DateEdit();
		this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.LblAccountbox = new System.Windows.Forms.Label();
		this.TxtTime1 = new DevExpress.XtraEditors.TimeEdit();
		this.TxtTime2 = new DevExpress.XtraEditors.TimeEdit();
		this.LblAccount = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtAccount = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.separatorControl4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccount.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.label1, "label1");
		this.label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.label1.ForeColor = System.Drawing.Color.Teal;
		this.label1.Name = "label1";
		resources.ApplyResources(this.label2, "label2");
		this.label2.ForeColor = System.Drawing.Color.Teal;
		this.label2.Name = "label2";
		resources.ApplyResources(this.label6, "label6");
		this.label6.ForeColor = System.Drawing.Color.Teal;
		this.label6.Name = "label6";
		resources.ApplyResources(this.label3, "label3");
		this.label3.ForeColor = System.Drawing.Color.Teal;
		this.label3.Name = "label3";
		this.TxtOrdCash.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtOrdCash, "TxtOrdCash");
		this.TxtOrdCash.Name = "TxtOrdCash";
		this.TxtOrdCash.ReadOnly = true;
		this.TxtOrdAjil.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtOrdAjil, "TxtOrdAjil");
		this.TxtOrdAjil.ForeColor = System.Drawing.Color.Red;
		this.TxtOrdAjil.Name = "TxtOrdAjil";
		this.TxtOrdAjil.ReadOnly = true;
		resources.ApplyResources(this.label7, "label7");
		this.label7.ForeColor = System.Drawing.Color.Teal;
		this.label7.Name = "label7";
		this.TxtOrdRTNAjil.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtOrdRTNAjil, "TxtOrdRTNAjil");
		this.TxtOrdRTNAjil.ForeColor = System.Drawing.Color.Red;
		this.TxtOrdRTNAjil.Name = "TxtOrdRTNAjil";
		this.TxtOrdRTNAjil.ReadOnly = true;
		resources.ApplyResources(this.label8, "label8");
		this.label8.ForeColor = System.Drawing.Color.Teal;
		this.label8.Name = "label8";
		this.TxtOrdRTNCash.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtOrdRTNCash, "TxtOrdRTNCash");
		this.TxtOrdRTNCash.Name = "TxtOrdRTNCash";
		this.TxtOrdRTNCash.ReadOnly = true;
		resources.ApplyResources(this.label9, "label9");
		this.label9.ForeColor = System.Drawing.Color.Teal;
		this.label9.Name = "label9";
		resources.ApplyResources(this.separatorControl1, "separatorControl1");
		this.separatorControl1.Name = "separatorControl1";
		this.TxtOrdNetAjil.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtOrdNetAjil, "TxtOrdNetAjil");
		this.TxtOrdNetAjil.ForeColor = System.Drawing.Color.Red;
		this.TxtOrdNetAjil.Name = "TxtOrdNetAjil";
		this.TxtOrdNetAjil.ReadOnly = true;
		resources.ApplyResources(this.label10, "label10");
		this.label10.ForeColor = System.Drawing.Color.Teal;
		this.label10.Name = "label10";
		this.TxtOrdNetCash.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtOrdNetCash, "TxtOrdNetCash");
		this.TxtOrdNetCash.Name = "TxtOrdNetCash";
		this.TxtOrdNetCash.ReadOnly = true;
		resources.ApplyResources(this.label11, "label11");
		this.label11.ForeColor = System.Drawing.Color.Teal;
		this.label11.Name = "label11";
		this.groupBox1.Controls.Add(this.label27);
		this.groupBox1.Controls.Add(this.LblTafqit);
		this.groupBox1.Controls.Add(this.TxtBank);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.TxtCash);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.TxtNet);
		this.groupBox1.Controls.Add(this.separatorControl4);
		this.groupBox1.Controls.Add(this.TxtExpensesNet);
		this.groupBox1.Controls.Add(this.TxtExpenses);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.TxtIncomeNet);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.TxtIncome);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.separatorControl5);
		this.groupBox1.Controls.Add(this.separatorControl3);
		this.groupBox1.Controls.Add(this.TxtTransferToCash);
		this.groupBox1.Controls.Add(this.TxtPurchCash);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Controls.Add(this.TxtPurchNetAjil);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label25);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.TxtPurchNetCash);
		this.groupBox1.Controls.Add(this.TxtTransferFromCash);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.TxtTransferToAjil);
		this.groupBox1.Controls.Add(this.TxtPurchAjil);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.TxtPurchRtnCash);
		this.groupBox1.Controls.Add(this.TxtTransferFromAjil);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label23);
		this.groupBox1.Controls.Add(this.TxtPurchRtnAjil);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.separatorControl2);
		this.groupBox1.Controls.Add(this.TxtOrdCash);
		this.groupBox1.Controls.Add(this.TxtOrdNetAjil);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.TxtOrdNetCash);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.TxtOrdAjil);
		this.groupBox1.Controls.Add(this.TxtOrdRTNCash);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.TxtOrdRTNAjil);
		this.groupBox1.Controls.Add(this.label8);
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		this.label27.Cursor = System.Windows.Forms.Cursors.Default;
		resources.ApplyResources(this.label27, "label27");
		this.label27.ForeColor = System.Drawing.Color.Teal;
		this.label27.Name = "label27";
		resources.ApplyResources(this.LblTafqit, "LblTafqit");
		this.LblTafqit.Cursor = System.Windows.Forms.Cursors.Default;
		this.LblTafqit.ForeColor = System.Drawing.Color.Teal;
		this.LblTafqit.Name = "LblTafqit";
		this.TxtBank.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtBank, "TxtBank");
		this.TxtBank.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.TxtBank.Name = "TxtBank";
		this.TxtBank.ReadOnly = true;
		resources.ApplyResources(this.label22, "label22");
		this.label22.ForeColor = System.Drawing.Color.Teal;
		this.label22.Name = "label22";
		this.TxtCash.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtCash, "TxtCash");
		this.TxtCash.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.TxtCash.Name = "TxtCash";
		this.TxtCash.ReadOnly = true;
		resources.ApplyResources(this.label21, "label21");
		this.label21.ForeColor = System.Drawing.Color.Teal;
		this.label21.Name = "label21";
		this.TxtNet.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtNet, "TxtNet");
		this.TxtNet.ForeColor = System.Drawing.Color.Teal;
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		resources.ApplyResources(this.separatorControl4, "separatorControl4");
		this.separatorControl4.Name = "separatorControl4";
		this.TxtExpensesNet.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtExpensesNet, "TxtExpensesNet");
		this.TxtExpensesNet.Name = "TxtExpensesNet";
		this.TxtExpensesNet.ReadOnly = true;
		this.TxtExpenses.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtExpenses, "TxtExpenses");
		this.TxtExpenses.Name = "TxtExpenses";
		this.TxtExpenses.ReadOnly = true;
		resources.ApplyResources(this.label20, "label20");
		this.label20.ForeColor = System.Drawing.Color.Teal;
		this.label20.Name = "label20";
		resources.ApplyResources(this.label18, "label18");
		this.label18.ForeColor = System.Drawing.Color.Teal;
		this.label18.Name = "label18";
		this.TxtIncomeNet.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtIncomeNet, "TxtIncomeNet");
		this.TxtIncomeNet.Name = "TxtIncomeNet";
		this.TxtIncomeNet.ReadOnly = true;
		resources.ApplyResources(this.label5, "label5");
		this.label5.ForeColor = System.Drawing.Color.Teal;
		this.label5.Name = "label5";
		this.TxtIncome.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtIncome, "TxtIncome");
		this.TxtIncome.Name = "TxtIncome";
		this.TxtIncome.ReadOnly = true;
		resources.ApplyResources(this.label19, "label19");
		this.label19.ForeColor = System.Drawing.Color.Teal;
		this.label19.Name = "label19";
		resources.ApplyResources(this.separatorControl5, "separatorControl5");
		this.separatorControl5.Name = "separatorControl5";
		resources.ApplyResources(this.separatorControl3, "separatorControl3");
		this.separatorControl3.Name = "separatorControl3";
		this.TxtTransferToCash.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtTransferToCash, "TxtTransferToCash");
		this.TxtTransferToCash.Name = "TxtTransferToCash";
		this.TxtTransferToCash.ReadOnly = true;
		this.TxtPurchCash.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtPurchCash, "TxtPurchCash");
		this.TxtPurchCash.Name = "TxtPurchCash";
		this.TxtPurchCash.ReadOnly = true;
		resources.ApplyResources(this.label26, "label26");
		this.label26.ForeColor = System.Drawing.Color.Teal;
		this.label26.Name = "label26";
		this.TxtPurchNetAjil.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtPurchNetAjil, "TxtPurchNetAjil");
		this.TxtPurchNetAjil.ForeColor = System.Drawing.Color.Red;
		this.TxtPurchNetAjil.Name = "TxtPurchNetAjil";
		this.TxtPurchNetAjil.ReadOnly = true;
		resources.ApplyResources(this.label12, "label12");
		this.label12.ForeColor = System.Drawing.Color.Teal;
		this.label12.Name = "label12";
		resources.ApplyResources(this.label25, "label25");
		this.label25.ForeColor = System.Drawing.Color.Teal;
		this.label25.Name = "label25";
		resources.ApplyResources(this.label13, "label13");
		this.label13.ForeColor = System.Drawing.Color.Teal;
		this.label13.Name = "label13";
		resources.ApplyResources(this.label14, "label14");
		this.label14.ForeColor = System.Drawing.Color.Teal;
		this.label14.Name = "label14";
		this.TxtPurchNetCash.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtPurchNetCash, "TxtPurchNetCash");
		this.TxtPurchNetCash.Name = "TxtPurchNetCash";
		this.TxtPurchNetCash.ReadOnly = true;
		this.TxtTransferFromCash.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtTransferFromCash, "TxtTransferFromCash");
		this.TxtTransferFromCash.ForeColor = System.Drawing.Color.Red;
		this.TxtTransferFromCash.Name = "TxtTransferFromCash";
		this.TxtTransferFromCash.ReadOnly = true;
		resources.ApplyResources(this.label15, "label15");
		this.label15.ForeColor = System.Drawing.Color.Teal;
		this.label15.Name = "label15";
		this.TxtTransferToAjil.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtTransferToAjil, "TxtTransferToAjil");
		this.TxtTransferToAjil.Name = "TxtTransferToAjil";
		this.TxtTransferToAjil.ReadOnly = true;
		this.TxtPurchAjil.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtPurchAjil, "TxtPurchAjil");
		this.TxtPurchAjil.ForeColor = System.Drawing.Color.Red;
		this.TxtPurchAjil.Name = "TxtPurchAjil";
		this.TxtPurchAjil.ReadOnly = true;
		resources.ApplyResources(this.label24, "label24");
		this.label24.ForeColor = System.Drawing.Color.Teal;
		this.label24.Name = "label24";
		this.TxtPurchRtnCash.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtPurchRtnCash, "TxtPurchRtnCash");
		this.TxtPurchRtnCash.Name = "TxtPurchRtnCash";
		this.TxtPurchRtnCash.ReadOnly = true;
		this.TxtTransferFromAjil.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtTransferFromAjil, "TxtTransferFromAjil");
		this.TxtTransferFromAjil.ForeColor = System.Drawing.Color.Red;
		this.TxtTransferFromAjil.Name = "TxtTransferFromAjil";
		this.TxtTransferFromAjil.ReadOnly = true;
		resources.ApplyResources(this.label16, "label16");
		this.label16.ForeColor = System.Drawing.Color.Teal;
		this.label16.Name = "label16";
		resources.ApplyResources(this.label23, "label23");
		this.label23.ForeColor = System.Drawing.Color.Teal;
		this.label23.Name = "label23";
		this.TxtPurchRtnAjil.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtPurchRtnAjil, "TxtPurchRtnAjil");
		this.TxtPurchRtnAjil.ForeColor = System.Drawing.Color.Red;
		this.TxtPurchRtnAjil.Name = "TxtPurchRtnAjil";
		this.TxtPurchRtnAjil.ReadOnly = true;
		resources.ApplyResources(this.label17, "label17");
		this.label17.ForeColor = System.Drawing.Color.Teal;
		this.label17.Name = "label17";
		resources.ApplyResources(this.separatorControl2, "separatorControl2");
		this.separatorControl2.Name = "separatorControl2";
		resources.ApplyResources(this.TxtDate1, "TxtDate1");
		this.TxtDate1.Name = "TxtDate1";
		this.TxtDate1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDate1.Properties.Appearance.Font");
		this.TxtDate1.Properties.Appearance.Options.UseFont = true;
		this.TxtDate1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate1.Properties.Buttons"))
		});
		this.TxtDate1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate1.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate1.Properties.Mask.EditMask = resources.GetString("TxtDate1.Properties.Mask.EditMask");
		this.TxtDate1.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate1.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtDate2, "TxtDate2");
		this.TxtDate2.Name = "TxtDate2";
		this.TxtDate2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDate2.Properties.Appearance.Font");
		this.TxtDate2.Properties.Appearance.Options.UseFont = true;
		this.TxtDate2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate2.Properties.Buttons"))
		});
		this.TxtDate2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate2.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate2.Properties.Mask.EditMask = resources.GetString("TxtDate2.Properties.Mask.EditMask");
		this.TxtDate2.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate2.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate2.Properties.TodayDate = new System.DateTime(2019, 6, 17, 21, 19, 56, 0);
		this.TxtDate2.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnSearch.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSearch.ImageOptions.Image");
		this.BtnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnSearch, "BtnSearch");
		this.BtnSearch.Name = "BtnSearch";
		this.BtnSearch.Click += new System.EventHandler(BtnSearch_Click);
		this.BtnSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnPrint.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrint.Appearance.Font");
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnPrint, "BtnPrint");
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		resources.ApplyResources(this.LblAccountbox, "LblAccountbox");
		this.LblAccountbox.Name = "LblAccountbox";
		resources.ApplyResources(this.TxtTime1, "TxtTime1");
		this.TxtTime1.Name = "TxtTime1";
		this.TxtTime1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtTime1.Properties.Appearance.Font");
		this.TxtTime1.Properties.Appearance.Options.UseFont = true;
		this.TxtTime1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTime1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTime1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtTime1.Properties.Buttons"))
		});
		this.TxtTime1.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
		this.TxtTime1.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtTime2, "TxtTime2");
		this.TxtTime2.Name = "TxtTime2";
		this.TxtTime2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtTime2.Properties.Appearance.Font");
		this.TxtTime2.Properties.Appearance.Options.UseFont = true;
		this.TxtTime2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTime2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTime2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtTime2.Properties.Buttons"))
		});
		this.TxtTime2.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
		this.TxtTime2.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.LblAccount, "LblAccount");
		this.LblAccount.Name = "LblAccount";
		resources.ApplyResources(this.label4, "label4");
		this.label4.ForeColor = System.Drawing.Color.Teal;
		this.label4.Name = "label4";
		resources.ApplyResources(this.TxtAccount, "TxtAccount");
		this.TxtAccount.Name = "TxtAccount";
		this.TxtAccount.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtAccount.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAccount.Properties.Appearance.Font");
		this.TxtAccount.Properties.Appearance.Options.UseBackColor = true;
		this.TxtAccount.Properties.Appearance.Options.UseFont = true;
		this.TxtAccount.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAccount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAccount.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtAccount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.TxtAccount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtAccount.Properties.Buttons"), resources.GetString("TxtAccount.Properties.Buttons1"), (int)resources.GetObject("TxtAccount.Properties.Buttons2"), (bool)resources.GetObject("TxtAccount.Properties.Buttons3"), (bool)resources.GetObject("TxtAccount.Properties.Buttons4"), (bool)resources.GetObject("TxtAccount.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtAccount.Properties.Buttons6"), resources.GetObject("TxtAccount.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtAccount.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtAccount.Properties.Buttons9"))
		});
		this.TxtAccount.Properties.ReadOnly = true;
		this.TxtAccount.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtDepit_ButtonClick);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.LblAccount);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.TxtAccount);
		base.Controls.Add(this.TxtTime2);
		base.Controls.Add(this.TxtTime1);
		base.Controls.Add(this.LblAccountbox);
		base.Controls.Add(this.BtnPrint);
		base.Controls.Add(this.BtnSearch);
		base.Controls.Add(this.TxtDate2);
		base.Controls.Add(this.TxtDate1);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.separatorControl1);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmMovementsDailyReportAllBran2";
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.separatorControl4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccount.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


