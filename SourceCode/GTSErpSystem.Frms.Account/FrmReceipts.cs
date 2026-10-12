using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Account.Search;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.FrmsMain.Search;
using GTSErpSystem.Helper;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Account;

public class FrmReceipts : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private ClassProject Project = new ClassProject();

	private Class_Receipts RCP = new Class_Receipts();

	private Class_AccountCard ACNT = new Class_AccountCard();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private int tafqitID = LoginDetails.TafqitID;

	public string txtWordAr;

	public string txtWordEn;

	private int DecimalNum = LoginDetails.DecimalNum;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label LblAccountbox;

	private Label label3;

	private System.Windows.Forms.ComboBox CBCostCenter;

	private Label label38;

	private SeparatorControl separatorControl4;

	private ButtonEdit TxtDepit;

	private Label label2;

	private NumericUpDown TxMoney;

	private DateEdit TxtDate;

	private Label label21;

	private ButtonEdit TxtCredit;

	private Label label12;

	private Label label11;

	private TextBox TxtNote;

	private Label label10;

	private TextBox TxtNoteNum;

	private Label label8;

	private TextBox TxtMan;

	private Label label7;

	private Label label6;

	private System.Windows.Forms.ComboBox CBReceiptType;

	private ButtonEdit TxtCode;

	private Label label1;

	private SimpleButton BtnPrint;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private Label LblAccountNum;

	private Label label4;

	private System.Windows.Forms.ComboBox DropProject;

	private Label label37;

	private TextEdit TxtCharge;

	private Label Lphone;

	public CheckEdit CkSentInvoice;

	public FrmReceipts()
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
		TxMoney.DecimalPlaces = DecimalNum;
		TxtDate.DateTime = DateTime.Now;
		BindComboBox();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BtnPrint.Enabled = false;
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		Clear();
		if (LoginDetails.FrmOrderID != 0)
		{
			SearchFrm(LoginDetails.FrmOrderID);
		}
		bool flag = CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 31);
		bool flag2 = CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 31);
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
		CkSentInvoice.Checked = LoginDetails.SentInvoice;
	}

	private void FrmUsers_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
	}

	private void FrmUsers_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F4")
		{
			Clear();
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
		else if (e.KeyCode == Keys.P && e.Modifiers == Keys.Control && BtnDelete.Enabled)
		{
			Print();
		}
	}

	private void BindComboBox()
	{
		CBReceiptType.DataSource = (from c in RCP.GetCashReceiptsType()
			where c.ID > 0
			select c).ToList();
		CBReceiptType.DisplayMember = "Name";
		CBReceiptType.ValueMember = "ID";
		try
		{
			DropProject.DataSource = Project.GetSearchAccountProjects();
			DropProject.DisplayMember = "ProjectName";
			DropProject.ValueMember = "ProjectID";
			if (DropProject.Items.Count > 0)
			{
				CBCostCenter.DataSource = Cost.GetCostCentersByProject(Convert.ToInt32(DropProject.SelectedValue));
				CBCostCenter.DisplayMember = "CostCentersName";
				CBCostCenter.ValueMember = "CostCentersID";
				CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Clear()
	{
		TxtCode.Text = "";
		TxtCharge.Text = "0.00";
		TxtDate.DateTime = DateTime.Now;
		TxtNoteNum.Text = "";
		CBReceiptType.SelectedValue = 1;
		TxMoney.Value = 0m;
		TxtDepit.Text = "";
		LblAccountbox.Text = "1";
		TxtCredit.Text = "";
		LblAccountNum.Text = "1";
		TxtMan.Text = "";
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault((Select_SearchAccountCostCenter_Result x) => x.IsDefaulte == true)?.CostCentersID;
		TxtNote.Text = "";
		TxMoney.Focus();
		LblAccountbox.Text = LoginDetails.Acc_CashBox.ToString();
		TxtDepit.Text = RCP.GetAccountName(LoginDetails.Acc_CashBox);
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 31))
		{
			int num = 0;
			if (TxtDepit.Text != "" && TxtCredit.Text != "" && Convert.ToInt32(LblAccountbox.Text) > 0 && Convert.ToInt32(LblAccountNum.Text) > 0 && TxMoney.Value > 0m)
			{
				if (CBCostCenter.SelectedValue == null)
				{
					CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
				}
				num = RCP.Insert(out var TRNCode, TxtNoteNum.Text, Convert.ToInt32(CBReceiptType.SelectedValue), TxtMan.Text, TxtDate.DateTime, TxtNote.Text, Convert.ToDecimal(TxMoney.Value), 14, Convert.ToInt32(DropProject.SelectedValue));
				RCP.InsertItemsReceipts(num, Convert.ToInt32(LblAccountbox.Text), Convert.ToInt32(LblAccountNum.Text), TxtNote.Text, TxMoney.Value, Convert.ToInt32(CBCostCenter.SelectedValue), 14, Convert.ToInt32(DropProject.SelectedValue));
				RCP.InsertTran(out var TRNCode2, TRNCode, TxtNoteNum.Text, TxtDate.DateTime, TxtNote.Text, 14);
				RCP.InsertItems(TRNCode2, Convert.ToInt32(LblAccountbox.Text), Convert.ToInt32(LblAccountNum.Text), TxtNote.Text, TxMoney.Value, Convert.ToInt32(CBCostCenter.SelectedValue), IsEdit: false, Convert.ToInt32(DropProject.SelectedValue));
				TxtCode.Text = TRNCode.ToString();
				if (num <= 0)
				{
					return;
				}
				if (LoginDetails.SentInvoice)
				{
					string text = ExportInvoiceAndUploadToServer();
					if (!string.IsNullOrWhiteSpace(text))
					{
						bool flag = TwilioHelper.SendPdf(Lphone.Text, text, num, Convert.ToDecimal(TxMoney.Text), "سند", out var _);
					}
				}
				switch (MessageBox.Show("تم حفظ ايصال القبض بنجاح، هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					Print();
					break;
				}
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
				BtnPrint.Enabled = true;
				IsEnableFalse();
				Charge(Convert.ToInt32(LblAccountNum.Text));
			}
			else if (TxMoney.Value <= 0m)
			{
				MessageBox.Show(" من فضلك قم بإدخال مبلغ إيصال القبض ", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxMoney.Focus();
			}
			else if (TxtDepit.Text == "")
			{
				MessageBox.Show(" من فضلك قم بإدخال حساب المدين ", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtDepit.Focus();
			}
			else if (TxtCredit.Text == "")
			{
				MessageBox.Show(" من فضلك قم بإدخال حساب الدائن ", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtCredit.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private string ExportInvoiceAndUploadToServer()
	{
		try
		{
			if (string.IsNullOrWhiteSpace(TxtCode.Text))
			{
				MessageBox.Show("اختر رقم الفاتورة أولا\u064b", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return null;
			}
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["Account_Receipts_Print"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Print_Account_Receipts", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@TranTypeID", 14);
			sqlCommand.Parameters.AddWithValue("@ReferenceCode", Convert.ToInt32(TxtCode.Text));
			sqlCommand.Parameters.AddWithValue("@BranchID", Convert.ToInt32(LoginDetails.BranchID));
			sqlDataAdapter.Fill(dataSetALL.Account_Receipts_Print);
			sqlConnection.Close();
			StiReport stiReport = new StiReport();
			ToWord toWord = new ToWord(Convert.ToDecimal(TxMoney.Text), currencies[tafqitID]);
			if (LoginDetails.Lang == "ar-EG")
			{
				txtWordAr = toWord.ConvertToArabic();
				stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashReceipts.mrt");
			}
			else
			{
				txtWordAr = toWord.ConvertToEnglish();
				stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCashReceipts.mrt");
			}
			stiReport.RegData(dataSetALL.Account_Receipts_Print);
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
			stiReport["CustName"] = TxtMan.Text;
			stiReport["CustAccount"] = TxtCredit.Text;
			stiReport["DepitAccount"] = TxtDepit.Text;
			stiReport["Money"] = TxMoney.Text;
			stiReport["Tafqit"] = txtWordAr;
			stiReport["Note"] = TxtNote.Text;
			Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
			string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), TxMoney.Text, (Convert.ToDecimal(TxMoney.Text) - Convert.ToDecimal(TxMoney.Text) / Convert.ToDecimal(1.15)).ToString());
			stiReport["EncHash"] = value;
			string text = $"Invoice_{TxtCode.Text}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
			string text2 = Path.Combine(Path.GetTempPath(), text);
			stiReport.Render(showProgress: false);
			stiReport.ExportDocument(StiExportFormat.Pdf, text2);
			string requestUriString = "ftp://win1135.site4now.net/" + text;
			string userName = "moelsayed";
			string password = "78789878";
			FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUriString);
			ftpWebRequest.Method = "STOR";
			ftpWebRequest.Credentials = new NetworkCredential(userName, password);
			byte[] array = File.ReadAllBytes(text2);
			ftpWebRequest.ContentLength = array.Length;
			using (Stream stream = ftpWebRequest.GetRequestStream())
			{
				stream.Write(array, 0, array.Length);
			}
			using ((FtpWebResponse)ftpWebRequest.GetResponse())
			{
			}
			File.Delete(text2);
			return "http://gtsdev-001-site28.atempurl.com/sentinvoices/" + text;
		}
		catch (Exception ex)
		{
			MessageBox.Show("âڑ \ufe0f خطأ أثناء حفظ الفاتورة:\n" + ex.Message);
			return null;
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 31))
		{
			if (!TxtDate.Enabled)
			{
				IsEnableTrue();
				BtnEdit.Text = "F5 حفظ";
				return;
			}
			int num = 0;
			int num2 = 0;
			if (TxtDepit.Text != "" && TxtCredit.Text != "" && Convert.ToInt32(LblAccountbox.Text) > 0 && Convert.ToInt32(LblAccountNum.Text) > 0 && TxMoney.Value > 0m)
			{
				if (CBCostCenter.SelectedValue == null)
				{
					CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
				}
				RCP.DeleteDetails(Convert.ToInt32(TxtCode.Text), 14);
				num = RCP.Update(Convert.ToInt32(TxtCode.Text), TxtNoteNum.Text, Convert.ToInt32(CBReceiptType.SelectedValue), TxtMan.Text, TxtDate.DateTime, TxtNote.Text, Convert.ToDecimal(TxMoney.Value), 14, Convert.ToInt32(DropProject.SelectedValue));
				RCP.InsertItemsReceipts(Convert.ToInt32(TxtCode.Text), Convert.ToInt32(LblAccountbox.Text), Convert.ToInt32(LblAccountNum.Text), TxtNote.Text, TxMoney.Value, Convert.ToInt32(CBCostCenter.SelectedValue), 14, Convert.ToInt32(DropProject.SelectedValue));
				RCP.DeleteTranDetails(Convert.ToInt32(TxtCode.Text), 14);
				num2 = RCP.UpdateTran(Convert.ToInt32(TxtCode.Text), TxtNoteNum.Text, TxtDate.DateTime, TxtNote.Text, 14);
				RCP.InsertItems(num2, Convert.ToInt32(LblAccountbox.Text), Convert.ToInt32(LblAccountNum.Text), TxtNote.Text, TxMoney.Value, Convert.ToInt32(CBCostCenter.SelectedValue), IsEdit: true, Convert.ToInt32(DropProject.SelectedValue));
				if (num > 0)
				{
					DialogResult dialogResult = MessageBox.Show("تم تعديل ايصال القبض بنجاح، هل تريد الطباعة الآن", "نجاح التعديل", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					if (dialogResult == DialogResult.Yes)
					{
						Print();
					}
					BtnEdit.Text = "F6 تعديل";
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					BtnPrint.Enabled = true;
					IsEnableFalse();
					Charge(Convert.ToInt32(LblAccountNum.Text));
				}
			}
			else if (TxMoney.Value <= 0m)
			{
				MessageBox.Show(" من فضلك قم بإدخال مبلغ إيصال القبض ", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxMoney.Focus();
			}
			else if (TxtDepit.Text == "")
			{
				MessageBox.Show(" من فضلك قم بإدخال حساب المدين ", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtDepit.Focus();
			}
			else if (TxtCredit.Text == "")
			{
				MessageBox.Show(" من فضلك قم بإدخال حساب الدائن ", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtCredit.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 31))
		{
			int num = 0;
			if (TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف إيصال القبض رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = RCP.DeleteDetails(Convert.ToInt32(TxtCode.Text), 14);
					num = RCP.Delete(Convert.ToInt32(TxtCode.Text), 14);
					num = RCP.DeleteTranDetails(Convert.ToInt32(TxtCode.Text), 14);
					num = RCP.DeleteTran(Convert.ToInt32(TxtCode.Text), 14);
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات إيصال القبض رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						IsEnableTrue();
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BtnPrint.Enabled = false;
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات إيصال القبض رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtCode.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات إيصال القبض بشكل صحيح", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 31))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Account_Receipts_Print"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Print_Account_Receipts", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@TranTypeID", 14);
				sqlCommand.Parameters.AddWithValue("@ReferenceCode", Convert.ToInt32(TxtCode.Text));
				sqlCommand.Parameters.AddWithValue("@BranchID", Convert.ToInt32(LoginDetails.BranchID));
				sqlDataAdapter.Fill(dataSetALL.Account_Receipts_Print);
				sqlConnection.Close();
				if (dataSetALL.Tables["Account_Receipts_Print"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					ToWord toWord = new ToWord(Convert.ToDecimal(TxMoney.Text), currencies[tafqitID]);
					if (LoginDetails.Lang == "ar-EG")
					{
						txtWordAr = toWord.ConvertToArabic();
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashReceipts.mrt");
					}
					else
					{
						txtWordAr = toWord.ConvertToEnglish();
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCashReceipts.mrt");
					}
					stiReport.RegData(dataSetALL.Account_Receipts_Print);
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
					stiReport["CustName"] = TxtMan.Text;
					stiReport["CustAccount"] = TxtCredit.Text;
					stiReport["DepitAccount"] = TxtDepit.Text;
					stiReport["Money"] = TxMoney.Text;
					stiReport["Tafqit"] = txtWordAr;
					stiReport["Note"] = TxtNote.Text;
					Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
					string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), TxMoney.Text, (Convert.ToDecimal(TxMoney.Text) - Convert.ToDecimal(TxMoney.Text) / Convert.ToDecimal(1.15)).ToString());
					stiReport["EncHash"] = value;
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
				MessageBox.Show("عفوا، اختر رقم السند ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Charge(int ID)
	{
		try
		{
			GTSdbEntities gTSdbEntities = new GTSdbEntities();
			List<decimal?> list = gTSdbEntities.GetAccountBalanceByAccID(ID).ToList();
			foreach (decimal? item in list)
			{
				TxtCharge.Text = Math.Round(item.Value, 2).ToString();
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

	private void Save_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnSave.Focus();
		}
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

	private void SetKeyboardLayout(InputLanguage layout)
	{
		InputLanguage.CurrentInputLanguage = layout;
	}

	private void Comb_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void IsEnableFalse()
	{
		TxtNoteNum.Enabled = false;
		CBReceiptType.Enabled = false;
		TxMoney.Enabled = false;
		TxtNote.Enabled = false;
		TxtDate.Enabled = false;
		TxtDepit.Enabled = false;
		TxtMan.Enabled = false;
		CBCostCenter.Enabled = false;
		DropProject.Enabled = false;
		TxtCredit.Enabled = false;
	}

	private void IsEnableTrue()
	{
		TxtNoteNum.Enabled = true;
		CBReceiptType.Enabled = true;
		TxMoney.Enabled = true;
		TxtNote.Enabled = true;
		TxtDate.Enabled = true;
		TxtDepit.Enabled = true;
		TxtMan.Enabled = true;
		CBCostCenter.Enabled = true;
		TxtCredit.Enabled = true;
		DropProject.Enabled = true;
	}

	private void SearchFrm(int PurchacheId)
	{
		try
		{
			if (PurchacheId > 0)
			{
				Account_Receipts PurchacheDtalis = (from x in RCP.GetALLReceipts()
					where (x.ReferenceCode == PurchacheId) & (x.TranTypeID == 14)
					select x).FirstOrDefault();
				Account_ReceiptsDetails account_ReceiptsDetails = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex == 1)
					select x).FirstOrDefault();
				Account_ReceiptsDetails account_ReceiptsDetails2 = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex == 2)
					select x).FirstOrDefault();
				TxtCode.Text = PurchacheId.ToString();
				TxtDate.Text = Convert.ToDateTime(PurchacheDtalis.TranDate).ToShortDateString();
				TxtNoteNum.Text = PurchacheDtalis.DocCode.ToString();
				CBReceiptType.SelectedValue = Convert.ToInt32(PurchacheDtalis.CashReceiptsTypeID);
				TxMoney.Value = Convert.ToDecimal(PurchacheDtalis.Money);
				TxtDepit.Text = ACNT.GetAccountName(Convert.ToInt32(account_ReceiptsDetails.Account_Sn));
				LblAccountbox.Text = account_ReceiptsDetails.Account_Sn.ToString();
				TxtCredit.Text = ACNT.GetAccountName(Convert.ToInt32(account_ReceiptsDetails2.Account_Sn));
				LblAccountNum.Text = account_ReceiptsDetails2.Account_Sn.ToString();
				TxtMan.Text = PurchacheDtalis.ReceivedFrom;
				CBCostCenter.SelectedValue = Convert.ToInt32(account_ReceiptsDetails.CostCentersID);
				TxtNote.Text = PurchacheDtalis.Note;
				BtnEdit.Text = "F6 تعديل";
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
				BtnPrint.Enabled = true;
				IsEnableFalse();
				Charge(Convert.ToInt32(LblAccountNum.Text));
			}
			else
			{
				TxMoney.Focus();
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BtnPrint.Enabled = false;
		Clear();
		IsEnableTrue();
		BindComboBox();
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

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TxtCredit_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccountReceipts frmSearchAccountReceipts = new FrmSearchAccountReceipts();
			frmSearchAccountReceipts.ShowDialog();
			if (frmSearchAccountReceipts.CustCode > 0)
			{
				int custCode = frmSearchAccountReceipts.CustCode;
				LblAccountNum.Text = custCode.ToString();
				TxtCredit.Text = frmSearchAccountReceipts.AccountName;
				Lphone.Text = frmSearchAccountReceipts.Phone;
				Charge(Convert.ToInt32(LblAccountNum.Text));
			}
			else
			{
				TxtCredit.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void CBReceiptType_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchReceipts frmSearchReceipts = new FrmSearchReceipts();
			frmSearchReceipts.ShowDialog();
			if (frmSearchReceipts.CustCode > 0)
			{
				int PurchacheId = frmSearchReceipts.CustCode;
				Account_Receipts PurchacheDtalis = (from x in RCP.GetALLReceipts()
					where (x.ReferenceCode == PurchacheId) & (x.TranTypeID == 14)
					select x).FirstOrDefault();
				Account_ReceiptsDetails account_ReceiptsDetails = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex == 1)
					select x).FirstOrDefault();
				Account_ReceiptsDetails account_ReceiptsDetails2 = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex == 2)
					select x).FirstOrDefault();
				TxtCode.Text = PurchacheId.ToString();
				TxtDate.Text = Convert.ToDateTime(PurchacheDtalis.TranDate).ToShortDateString();
				TxtNoteNum.Text = PurchacheDtalis.DocCode.ToString();
				CBReceiptType.SelectedValue = Convert.ToInt32(PurchacheDtalis.CashReceiptsTypeID);
				TxMoney.Value = Convert.ToDecimal(PurchacheDtalis.Money);
				TxtDepit.Text = ACNT.GetAccountName(Convert.ToInt32(account_ReceiptsDetails.Account_Sn));
				LblAccountbox.Text = account_ReceiptsDetails.Account_Sn.ToString();
				TxtCredit.Text = ACNT.GetAccountName(Convert.ToInt32(account_ReceiptsDetails2.Account_Sn));
				LblAccountNum.Text = account_ReceiptsDetails2.Account_Sn.ToString();
				TxtMan.Text = PurchacheDtalis.ReceivedFrom;
				CBCostCenter.SelectedValue = Convert.ToInt32(account_ReceiptsDetails.CostCentersID);
				DropProject.SelectedValue = Convert.ToInt32(account_ReceiptsDetails.ProjectId);
				TxtNote.Text = PurchacheDtalis.Note;
				BtnEdit.Text = "F6 تعديل";
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
				BtnPrint.Enabled = true;
				IsEnableFalse();
				Charge(Convert.ToInt32(LblAccountNum.Text));
			}
			else
			{
				TxMoney.Focus();
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void DropProject_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (DropProject.Items.Count > 0)
			{
				CBCostCenter.DataSource = Cost.GetCostCentersByProject(Convert.ToInt32(DropProject.SelectedValue));
				CBCostCenter.DisplayMember = "CostCentersName";
				CBCostCenter.ValueMember = "CostCentersID";
			}
		}
		catch
		{
		}
	}

	private void CkSentInvoice_CheckedChanged(object sender, EventArgs e)
	{
		if (CkSentInvoice.Checked)
		{
			LoginDetails.SentInvoice = true;
		}
		else
		{
			LoginDetails.SentInvoice = false;
		}
	}

	private void TxtNoteNum_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void TxtDepit_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			LoginDetails.FrmName = "FrmReceiptsCash";
			GTSErpSystem.Frms.FrmsMain.Search.FrmSearchAccount frmSearchAccount = new GTSErpSystem.Frms.FrmsMain.Search.FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblAccountbox.Text = custCode.ToString();
				TxtDepit.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtCredit.Focus();
			}
			LoginDetails.FrmName = "";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Account.FrmReceipts));
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.CkSentInvoice = new DevExpress.XtraEditors.CheckEdit();
		this.Lphone = new System.Windows.Forms.Label();
		this.label37 = new System.Windows.Forms.Label();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.LblAccountNum = new System.Windows.Forms.Label();
		this.LblAccountbox = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.DropProject = new System.Windows.Forms.ComboBox();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.label38 = new System.Windows.Forms.Label();
		this.separatorControl4 = new DevExpress.XtraEditors.SeparatorControl();
		this.TxtDepit = new DevExpress.XtraEditors.ButtonEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.TxMoney = new System.Windows.Forms.NumericUpDown();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.TxtCredit = new DevExpress.XtraEditors.ButtonEdit();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtNoteNum = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtMan = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.CBReceiptType = new System.Windows.Forms.ComboBox();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.CkSentInvoice.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDepit.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxMoney).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCredit.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.CkSentInvoice);
		this.groupBox1.Controls.Add(this.Lphone);
		this.groupBox1.Controls.Add(this.label37);
		this.groupBox1.Controls.Add(this.TxtCharge);
		this.groupBox1.Controls.Add(this.LblAccountNum);
		this.groupBox1.Controls.Add(this.LblAccountbox);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.DropProject);
		this.groupBox1.Controls.Add(this.CBCostCenter);
		this.groupBox1.Controls.Add(this.label38);
		this.groupBox1.Controls.Add(this.separatorControl4);
		this.groupBox1.Controls.Add(this.TxtDepit);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxMoney);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.TxtCredit);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.TxtNoteNum);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.TxtMan);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.CBReceiptType);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.CkSentInvoice, "CkSentInvoice");
		this.CkSentInvoice.Name = "CkSentInvoice";
		this.CkSentInvoice.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("CkSentInvoice.Properties.Appearance.Font");
		this.CkSentInvoice.Properties.Appearance.Options.UseFont = true;
		this.CkSentInvoice.Properties.Caption = resources.GetString("CkSentInvoice.Properties.Caption");
		this.CkSentInvoice.Properties.DisplayValueChecked = resources.GetString("CkSentInvoice.Properties.DisplayValueChecked");
		this.CkSentInvoice.Properties.DisplayValueGrayed = resources.GetString("CkSentInvoice.Properties.DisplayValueGrayed");
		this.CkSentInvoice.Properties.DisplayValueUnchecked = resources.GetString("CkSentInvoice.Properties.DisplayValueUnchecked");
		this.CkSentInvoice.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("CkSentInvoice.Properties.GlyphVerticalAlignment");
		this.CkSentInvoice.CheckedChanged += new System.EventHandler(CkSentInvoice_CheckedChanged);
		resources.ApplyResources(this.Lphone, "Lphone");
		this.Lphone.Name = "Lphone";
		resources.ApplyResources(this.label37, "label37");
		this.label37.Name = "label37";
		resources.ApplyResources(this.TxtCharge, "TxtCharge");
		this.TxtCharge.Name = "TxtCharge";
		this.TxtCharge.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCharge.Properties.Appearance.Font");
		this.TxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.LblAccountNum, "LblAccountNum");
		this.LblAccountNum.Name = "LblAccountNum";
		resources.ApplyResources(this.LblAccountbox, "LblAccountbox");
		this.LblAccountbox.Name = "LblAccountbox";
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.DropProject, "DropProject");
		this.DropProject.FormattingEnabled = true;
		this.DropProject.Name = "DropProject";
		this.DropProject.SelectedIndexChanged += new System.EventHandler(DropProject_SelectedIndexChanged);
		this.DropProject.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.DropProject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Comb_KeyPress);
		resources.ApplyResources(this.CBCostCenter, "CBCostCenter");
		this.CBCostCenter.FormattingEnabled = true;
		this.CBCostCenter.Name = "CBCostCenter";
		this.CBCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBCostCenter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Comb_KeyPress);
		resources.ApplyResources(this.label38, "label38");
		this.label38.ForeColor = System.Drawing.Color.FromArgb(32, 31, 53);
		this.label38.Name = "label38";
		resources.ApplyResources(this.separatorControl4, "separatorControl4");
		this.separatorControl4.LineColor = System.Drawing.Color.FromArgb(32, 31, 53);
		this.separatorControl4.LineThickness = 1;
		this.separatorControl4.Name = "separatorControl4";
		resources.ApplyResources(this.TxtDepit, "TxtDepit");
		this.TxtDepit.Name = "TxtDepit";
		this.TxtDepit.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDepit.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDepit.Properties.Appearance.Font");
		this.TxtDepit.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDepit.Properties.Appearance.Options.UseFont = true;
		this.TxtDepit.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDepit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDepit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.TxtDepit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDepit.Properties.Buttons"), resources.GetString("TxtDepit.Properties.Buttons1"), (int)resources.GetObject("TxtDepit.Properties.Buttons2"), (bool)resources.GetObject("TxtDepit.Properties.Buttons3"), (bool)resources.GetObject("TxtDepit.Properties.Buttons4"), (bool)resources.GetObject("TxtDepit.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtDepit.Properties.Buttons6"), resources.GetObject("TxtDepit.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtDepit.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtDepit.Properties.Buttons9"))
		});
		this.TxtDepit.Properties.ReadOnly = true;
		this.TxtDepit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtDepit_ButtonClick);
		this.TxtDepit.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtDepit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxMoney, "TxMoney");
		this.TxMoney.DecimalPlaces = 3;
		this.TxMoney.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxMoney.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxMoney.Name = "TxMoney";
		this.TxMoney.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		this.TxtDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDate.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.TxtCredit, "TxtCredit");
		this.TxtCredit.Name = "TxtCredit";
		this.TxtCredit.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtCredit.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCredit.Properties.Appearance.Font");
		this.TxtCredit.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCredit.Properties.Appearance.Options.UseFont = true;
		this.TxtCredit.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCredit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCredit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.TxtCredit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtCredit.Properties.Buttons"), resources.GetString("TxtCredit.Properties.Buttons1"), (int)resources.GetObject("TxtCredit.Properties.Buttons2"), (bool)resources.GetObject("TxtCredit.Properties.Buttons3"), (bool)resources.GetObject("TxtCredit.Properties.Buttons4"), (bool)resources.GetObject("TxtCredit.Properties.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("TxtCredit.Properties.Buttons6"), resources.GetObject("TxtCredit.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtCredit.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtCredit.Properties.Buttons9"))
		});
		this.TxtCredit.Properties.ReadOnly = true;
		this.TxtCredit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCredit_ButtonClick);
		this.TxtCredit.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtCredit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.label11, "label11");
		this.label11.Name = "label11";
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNote.KeyDown += new System.Windows.Forms.KeyEventHandler(Save_KeyDown);
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.TxtNoteNum, "TxtNoteNum");
		this.TxtNoteNum.Name = "TxtNoteNum";
		this.TxtNoteNum.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNoteNum.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtNoteNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNoteNum_KeyPress);
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.TxtMan, "TxtMan");
		this.TxtMan.Name = "TxtMan";
		this.TxtMan.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtMan.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.CBReceiptType, "CBReceiptType");
		this.CBReceiptType.FormattingEnabled = true;
		this.CBReceiptType.Name = "CBReceiptType";
		this.CBReceiptType.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBReceiptType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Comb_KeyPress);
		resources.ApplyResources(this.TxtCode, "TxtCode");
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 255, 210);
		this.TxtCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCode.Properties.Appearance.Font");
		this.TxtCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCode.Properties.Appearance.Options.UseFont = true;
		this.TxtCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCode.Properties.ReadOnly = true;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.TxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.BtnPrint, "BtnPrint");
		this.BtnPrint.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnPrint.Appearance.Font");
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnEdit.Appearance.Font");
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNew.Appearance.Font");
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDelete.Appearance.Font");
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnPrint);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmReceipts";
		base.Load += new System.EventHandler(FrmUsers_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FrmUsers_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.CkSentInvoice.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDepit.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxMoney).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCredit.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


