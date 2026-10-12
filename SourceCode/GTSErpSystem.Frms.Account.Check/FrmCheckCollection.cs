using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.Account.Check.Search;
using GTSErpSystem.Frms.Account.Search;
using GTSErpSystem.Frms.FrmsMain.Search;

namespace GTSErpSystem.Frms.Account.Check;

public class FrmCheckCollection : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private Class_Receipts RCP = new Class_Receipts();

	private Class_AccountCard ACNT = new Class_AccountCard();

	private Calss_CostCenter Cost = new Calss_CostCenter();

	private ClassProject Project = new ClassProject();

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

	private DateEdit TxtDateCheck;

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

	private ButtonEdit TxtCode;

	private Label label1;

	private SimpleButton BtnPrint;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private Label LblAccountNum;

	private DateEdit TxtDateDue;

	private Label label4;

	private TextBox TxtCheck;

	private ToggleSwitch toggleHold;

	private ButtonEdit BtnCheck;

	private Label label5;

	private Label label9;

	private Label label13;

	private System.Windows.Forms.ComboBox CBProject;

	public FrmCheckCollection()
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
		TxtDateCheck.DateTime = DateTime.Now;
		BindComboBox();
		BtnSave.Enabled = false;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BtnPrint.Enabled = false;
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
		Clear();
		IsEnableFalse();
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
		try
		{
			CBProject.DataSource = Project.GetSearchAccountProjects();
			CBProject.DisplayMember = "ProjectName";
			CBProject.ValueMember = "ProjectID";
			if (CBProject.Items.Count > 0)
			{
				CBCostCenter.DataSource = Cost.GetCostCentersByProject(Convert.ToInt32(CBProject.SelectedValue));
				CBCostCenter.DisplayMember = "CostCentersName";
				CBCostCenter.ValueMember = "CostCentersID";
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Clear()
	{
		BtnCheck.Text = "";
		TxtCheck.Text = "";
		TxtCode.Text = "";
		TxtDateCheck.DateTime = DateTime.Now;
		TxtNoteNum.Text = "";
		TxtDateDue.DateTime = DateTime.Now;
		TxMoney.Value = 0m;
		TxtDepit.Text = "";
		LblAccountbox.Text = "1";
		TxtCredit.Text = "";
		LblAccountNum.Text = "1";
		TxtMan.Text = "";
		CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
		TxtNote.Text = "";
		TxMoney.Focus();
		LblAccountbox.Text = LoginDetails.Acc_BankNumCachir.ToString();
		TxtDepit.Text = RCP.GetAccountName(LoginDetails.Acc_BankNumCachir);
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 123))
		{
			int num = 0;
			if (TxtDepit.Text != "" && TxtCredit.Text != "" && Convert.ToInt32(LblAccountbox.Text) > 0 && Convert.ToInt32(LblAccountNum.Text) > 0 && TxMoney.Value > 0m && TxtCheck.Text != "")
			{
				if (CBCostCenter.SelectedValue == null)
				{
					CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
				}
				int account_Sn = ((!toggleHold.IsOn) ? Convert.ToInt32(LblAccountNum.Text) : Convert.ToInt32(LblAccountbox.Text));
				num = RCP.UpdateCheckCollection(Convert.ToInt32(TxtCode.Text), TxtNoteNum.Text, TxtMan.Text, TxtDateCheck.DateTime, TxtNote.Text, Convert.ToDecimal(TxMoney.Value), TxtCheck.Text, TxtDateDue.DateTime, toggleHold.IsOn, 39);
				RCP.InsertTran(out var TRNCode, Convert.ToInt32(TxtCode.Text), TxtNoteNum.Text, TxtDateCheck.DateTime, TxtNote.Text, 41);
				RCP.InsertItems(TRNCode, account_Sn, LoginDetails.Acc_CheckIncome, TxtNote.Text, TxMoney.Value, Convert.ToInt32(CBCostCenter.SelectedValue), IsEdit: false, Convert.ToInt32(CBProject.SelectedValue));
				if (num > 0)
				{
					DialogResult dialogResult = MessageBox.Show("تم حفظ تحصيل الشيكات بنجاح، هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					if (dialogResult == DialogResult.Yes)
					{
						Print();
					}
					BtnCheck.Text = TxtCode.Text;
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					BtnPrint.Enabled = true;
					IsEnableFalse();
				}
			}
			else if (TxMoney.Value <= 0m)
			{
				MessageBox.Show(" من فضلك قم بإدخال مبلغ تحصيل الشيكات ", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
			else if (TxtCheck.Text == "")
			{
				MessageBox.Show(" من فضلك قم بإدخال رقم الشيك ", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtCredit.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 123))
		{
			int num = 0;
			int num2 = 0;
			if (TxtDepit.Text != "" && TxtCredit.Text != "" && Convert.ToInt32(LblAccountbox.Text) > 0 && Convert.ToInt32(LblAccountNum.Text) > 0 && TxMoney.Value > 0m && TxtCheck.Text != "")
			{
				if (CBCostCenter.SelectedValue == null)
				{
					CBCostCenter.SelectedValue = Cost.GetSearchAccountCostCenter().FirstOrDefault().CostCentersID;
				}
				int account_Sn = ((!toggleHold.IsOn) ? Convert.ToInt32(LblAccountNum.Text) : Convert.ToInt32(LblAccountbox.Text));
				num = RCP.UpdateCheckCollection(Convert.ToInt32(TxtCode.Text), TxtNoteNum.Text, TxtMan.Text, TxtDateCheck.DateTime, TxtNote.Text, Convert.ToDecimal(TxMoney.Value), TxtCheck.Text, TxtDateDue.DateTime, toggleHold.IsOn, 39);
				RCP.DeleteTranDetails(Convert.ToInt32(TxtCode.Text), 41);
				num2 = RCP.UpdateTran(Convert.ToInt32(TxtCode.Text), TxtNoteNum.Text, TxtDateCheck.DateTime, TxtNote.Text, 41);
				RCP.InsertItems(num2, account_Sn, LoginDetails.Acc_CheckIncome, TxtNote.Text, TxMoney.Value, Convert.ToInt32(CBCostCenter.SelectedValue), IsEdit: true, Convert.ToInt32(CBProject.SelectedValue));
				if (num > 0)
				{
					DialogResult dialogResult = MessageBox.Show("تم تعديل تحصيل الشيكات بنجاح، هل تريد الطباعة الآن", "نجاح التعديل", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
				}
			}
			else if (TxMoney.Value <= 0m)
			{
				MessageBox.Show(" من فضلك قم بإدخال مبلغ تحصيل الشيكات ", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
			else if (TxtCheck.Text == "")
			{
				MessageBox.Show(" من فضلك قم بإدخال رقم الشيك ", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 123))
		{
			int num = 0;
			if (TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف تحصيل الشيكات رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					num = RCP.DeleteCollection(Convert.ToInt32(TxtCode.Text), 39);
					num = RCP.DeleteTranDetails(Convert.ToInt32(TxtCode.Text), 41);
					num = RCP.DeleteTran(Convert.ToInt32(TxtCode.Text), 41);
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات تحصيل الشيكات رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						Clear();
						IsEnableTrue();
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BtnPrint.Enabled = false;
					}
					else
					{
						MessageBox.Show("لا يمكن حذف بيانات تحصيل الشيكات رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtCode.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات تحصيل الشيكات بشكل صحيح", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 123))
		{
			if (TxtCode.Text != "")
			{
				Class_Print_DailyRestrictionsALL class_Print_DailyRestrictionsALL = new Class_Print_DailyRestrictionsALL();
				class_Print_DailyRestrictionsALL.Print_DailyRestrictionsALL(Convert.ToInt32(TxtCode.Text), 41);
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

	private void TxtNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
	}

	private void IsEnableFalse()
	{
		TxtNoteNum.Enabled = false;
		TxtDateDue.Enabled = false;
		TxtCheck.Enabled = false;
		TxMoney.Enabled = false;
		TxtNote.Enabled = false;
		TxtDateCheck.Enabled = false;
		TxtDepit.Enabled = false;
		TxtMan.Enabled = false;
		CBCostCenter.Enabled = false;
		TxtCredit.Enabled = false;
	}

	private void IsEnableTrue()
	{
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		BtnSave.Enabled = false;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BtnPrint.Enabled = false;
		Clear();
		IsEnableTrue();
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
			LoginDetails.FrmName = "FrmCheckCollection";
			FrmSearchCheckRecipt frmSearchCheckRecipt = new FrmSearchCheckRecipt();
			frmSearchCheckRecipt.ShowDialog();
			if (frmSearchCheckRecipt.CustCode > 0)
			{
				int PurchacheId = frmSearchCheckRecipt.CustCode;
				Account_Receipts PurchacheDtalis = (from x in RCP.GetALLReceipts()
					where (x.ReferenceCode == PurchacheId) & (x.TranTypeID == 39)
					select x).FirstOrDefault();
				Account_ReceiptsDetails account_ReceiptsDetails = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex == 1)
					select x).FirstOrDefault();
				Account_ReceiptsDetails account_ReceiptsDetails2 = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex == 2)
					select x).FirstOrDefault();
				TxtCode.Text = PurchacheId.ToString();
				BtnCheck.Text = PurchacheId.ToString();
				TxtCheck.Text = PurchacheDtalis.CheckID;
				TxtDateCheck.DateTime = Convert.ToDateTime(PurchacheDtalis.TranDate);
				TxtNoteNum.Text = PurchacheDtalis.DocCode.ToString();
				TxtDateDue.DateTime = Convert.ToDateTime(PurchacheDtalis.DateDue);
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
				LoginDetails.FrmName = "";
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

	private void BtnCheck_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			LoginDetails.FrmName = "FrmCheck";
			FrmSearchCheckRecipt frmSearchCheckRecipt = new FrmSearchCheckRecipt();
			frmSearchCheckRecipt.ShowDialog();
			if (frmSearchCheckRecipt.CustCode > 0)
			{
				int PurchacheId = frmSearchCheckRecipt.CustCode;
				Account_Receipts PurchacheDtalis = (from x in RCP.GetALLReceipts()
					where (x.ReferenceCode == PurchacheId) & (x.TranTypeID == 39)
					select x).FirstOrDefault();
				Account_ReceiptsDetails account_ReceiptsDetails = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex == 1)
					select x).FirstOrDefault();
				Account_ReceiptsDetails account_ReceiptsDetails2 = (from x in RCP.GetALLReceiptsDetails()
					where (x.TranSn == PurchacheDtalis.ID) & (x.AccounIindex == 2)
					select x).FirstOrDefault();
				BtnCheck.Text = "";
				TxtCode.Text = PurchacheId.ToString();
				TxtCheck.Text = PurchacheDtalis.CheckID;
				TxtDateCheck.DateTime = Convert.ToDateTime(PurchacheDtalis.TranDate);
				TxtNoteNum.Text = PurchacheDtalis.DocCode.ToString();
				TxtDateDue.DateTime = Convert.ToDateTime(PurchacheDtalis.DateDue);
				TxMoney.Value = Convert.ToDecimal(PurchacheDtalis.Money);
				TxtDepit.Text = ACNT.GetAccountName(Convert.ToInt32(account_ReceiptsDetails.Account_Sn));
				LblAccountbox.Text = account_ReceiptsDetails.Account_Sn.ToString();
				TxtCredit.Text = ACNT.GetAccountName(Convert.ToInt32(account_ReceiptsDetails2.Account_Sn));
				LblAccountNum.Text = account_ReceiptsDetails2.Account_Sn.ToString();
				TxtMan.Text = PurchacheDtalis.ReceivedFrom;
				CBCostCenter.SelectedValue = Convert.ToInt32(account_ReceiptsDetails.CostCentersID);
				TxtNote.Text = PurchacheDtalis.Note;
				BtnEdit.Text = "F6 تعديل";
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnDelete.Enabled = false;
				BtnPrint.Enabled = false;
				IsEnableFalse();
				LoginDetails.FrmName = "";
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

	private void CBProject_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (CBProject.Items.Count > 0)
			{
				CBCostCenter.DataSource = Cost.GetCostCentersByProject(Convert.ToInt32(CBProject.SelectedValue));
				CBCostCenter.DisplayMember = "CostCentersName";
				CBCostCenter.ValueMember = "CostCentersID";
			}
		}
		catch
		{
		}
	}

	private void label4_Click(object sender, EventArgs e)
	{
	}

	private void TxtDepit_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			LoginDetails.FrmName = "FrmReceiptsCheckBank";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Account.Check.FrmCheckCollection));
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
		this.label13 = new System.Windows.Forms.Label();
		this.CBProject = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.BtnCheck = new DevExpress.XtraEditors.ButtonEdit();
		this.label5 = new System.Windows.Forms.Label();
		this.toggleHold = new DevExpress.XtraEditors.ToggleSwitch();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtCheck = new System.Windows.Forms.TextBox();
		this.TxtDateDue = new DevExpress.XtraEditors.DateEdit();
		this.LblAccountNum = new System.Windows.Forms.Label();
		this.LblAccountbox = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.CBCostCenter = new System.Windows.Forms.ComboBox();
		this.label38 = new System.Windows.Forms.Label();
		this.separatorControl4 = new DevExpress.XtraEditors.SeparatorControl();
		this.TxtDepit = new DevExpress.XtraEditors.ButtonEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.TxMoney = new System.Windows.Forms.NumericUpDown();
		this.TxtDateCheck = new DevExpress.XtraEditors.DateEdit();
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
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnCheck.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleHold.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateDue.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateDue.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDepit.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxMoney).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateCheck.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateCheck.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCredit.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.CBProject);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.BtnCheck);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.toggleHold);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.TxtCheck);
		this.groupBox1.Controls.Add(this.TxtDateDue);
		this.groupBox1.Controls.Add(this.LblAccountNum);
		this.groupBox1.Controls.Add(this.LblAccountbox);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.CBCostCenter);
		this.groupBox1.Controls.Add(this.label38);
		this.groupBox1.Controls.Add(this.separatorControl4);
		this.groupBox1.Controls.Add(this.TxtDepit);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxMoney);
		this.groupBox1.Controls.Add(this.TxtDateCheck);
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
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.label13, "label13");
		this.label13.Name = "label13";
		resources.ApplyResources(this.CBProject, "CBProject");
		this.CBProject.FormattingEnabled = true;
		this.CBProject.Name = "CBProject";
		this.CBProject.SelectedIndexChanged += new System.EventHandler(CBProject_SelectedIndexChanged);
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.BtnCheck, "BtnCheck");
		this.BtnCheck.Name = "BtnCheck";
		this.BtnCheck.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 255, 210);
		this.BtnCheck.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnCheck.Properties.Appearance.Font");
		this.BtnCheck.Properties.Appearance.Options.UseBackColor = true;
		this.BtnCheck.Properties.Appearance.Options.UseFont = true;
		this.BtnCheck.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnCheck.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnCheck.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.BtnCheck.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.BtnCheck.Properties.ReadOnly = true;
		this.BtnCheck.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnCheck_ButtonClick);
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.toggleHold, "toggleHold");
		this.toggleHold.Name = "toggleHold";
		this.toggleHold.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("toggleHold.Properties.Appearance.Font");
		this.toggleHold.Properties.Appearance.Options.UseFont = true;
		this.toggleHold.Properties.Appearance.Options.UseForeColor = true;
		this.toggleHold.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("toggleHold.Properties.GlyphVerticalAlignment");
		this.toggleHold.Properties.OffText = resources.GetString("toggleHold.Properties.OffText");
		this.toggleHold.Properties.OnText = resources.GetString("toggleHold.Properties.OnText");
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		this.label4.Click += new System.EventHandler(label4_Click);
		resources.ApplyResources(this.TxtCheck, "TxtCheck");
		this.TxtCheck.ForeColor = System.Drawing.Color.DodgerBlue;
		this.TxtCheck.Name = "TxtCheck";
		this.TxtCheck.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtDateDue, "TxtDateDue");
		this.TxtDateDue.Name = "TxtDateDue";
		this.TxtDateDue.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateDue.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDateDue.Properties.Appearance.Font");
		this.TxtDateDue.Properties.Appearance.Options.UseFont = true;
		this.TxtDateDue.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateDue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateDue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDateDue.Properties.Buttons"))
		});
		this.TxtDateDue.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDateDue.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDateDue.Properties.CalendarTimeProperties.Mask.EditMask = resources.GetString("TxtDateDue.Properties.CalendarTimeProperties.Mask.EditMask");
		this.TxtDateDue.Properties.Mask.EditMask = resources.GetString("TxtDateDue.Properties.Mask.EditMask");
		this.TxtDateDue.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDateDue.Properties.Mask.UseMaskAsDisplayFormat");
		resources.ApplyResources(this.LblAccountNum, "LblAccountNum");
		this.LblAccountNum.Name = "LblAccountNum";
		resources.ApplyResources(this.LblAccountbox, "LblAccountbox");
		this.LblAccountbox.Name = "LblAccountbox";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
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
		resources.ApplyResources(this.TxtDateCheck, "TxtDateCheck");
		this.TxtDateCheck.Name = "TxtDateCheck";
		this.TxtDateCheck.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateCheck.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDateCheck.Properties.Appearance.Font");
		this.TxtDateCheck.Properties.Appearance.Options.UseFont = true;
		this.TxtDateCheck.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateCheck.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateCheck.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDateCheck.Properties.Buttons"))
		});
		this.TxtDateCheck.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDateCheck.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDateCheck.Properties.CalendarTimeProperties.Mask.EditMask = resources.GetString("TxtDateCheck.Properties.CalendarTimeProperties.Mask.EditMask");
		this.TxtDateCheck.Properties.Mask.EditMask = resources.GetString("TxtDateCheck.Properties.Mask.EditMask");
		this.TxtDateCheck.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDateCheck.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDateCheck.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		base.Name = "FrmCheckCollection";
		base.Load += new System.EventHandler(FrmUsers_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FrmUsers_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnCheck.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleHold.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateDue.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateDue.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDepit.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxMoney).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateCheck.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateCheck.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCredit.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


