using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Main.Setting;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.Cust_Sup.Search;
using GTSErpSystem.Frms.FrmsMain.Search;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.Cust_Sup;

public class FrmCustomer : XtraForm
{
	private Class_SalesMan SLSMAN = new Class_SalesMan();

	private Class_Place PLC = new Class_Place();

	private Class_Customer CUS = new Class_Customer();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	public int GrpID;

	public bool IsCustomer;

	public bool IsSupplier;

	private AutoCompleteStringCollection NameSugg = new AutoCompleteStringCollection();

	private Class_DefualtAccount Stting = new Class_DefualtAccount();

	private Class_AccountCard Acct = new Class_AccountCard();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label5;

	private TextBox TxtName;

	private Label label4;

	private System.Windows.Forms.ComboBox CBPlace;

	private Label label3;

	private Label label2;

	private TextBox TxtVatNum;

	private System.Windows.Forms.ComboBox CBSalesMan;

	private ButtonEdit TxtCode;

	private Label label1;

	private ButtonEdit TxtAccount;

	private Label label12;

	private RadioButton RbCustSup;

	private RadioButton RBCustOnly;

	private Label label11;

	private TextBox TxtNote;

	private Label label10;

	private TextBox TxtPhone;

	private Label label9;

	private TextBox TxtFax;

	private Label label8;

	private TextBox TxtAddress;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private NumericEditBox TxtCreditLimit;

	private Label label6;

	private NumericEditBox TxtAlertLimit;

	private Label label7;

	private Label label20;

	private Label label18;

	private Label label16;

	private Label label14;

	private TextBox TxtCity;

	private Label label19;

	private TextBox TxtDistrict;

	private Label label17;

	private TextBox TxtStreetNum;

	private Label label15;

	private TextBox TxtCommercialRecord;

	private TextBox TxtBuildingNum;

	private TextBox TxtAdditionalNum;

	private Label label13;

	private TextBox TxtPostalCode;

	private TextBox TxtCountry;

	private GroupBox groupBox2;

	private Label LblID;

	private Label label21;

	private System.Windows.Forms.ComboBox CbBranch;

	private ButtonEdit BtnFatherNum;

	private Label label22;

	private Label LFatherNumId;

	public FrmCustomer()
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
		BindPlace();
		BindSalesMan();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtVatNum.Focus();
		Clear();
		AutoComplate();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
		Select_AccountDefualt_Result select_AccountDefualt_Result = (from x in Stting.GetAccountDefualt(LoginDetails.BranchID)
			where x.ID == 8
			select x).FirstOrDefault();
		if (select_AccountDefualt_Result != null)
		{
			BtnFatherNum.Text = select_AccountDefualt_Result.AcccountName.ToString();
			LFatherNumId.Text = select_AccountDefualt_Result.AccountID.ToString();
		}
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
	}

	private void Frms_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F4")
		{
			Clear();
		}
		else if (e.KeyCode.ToString() == "F5")
		{
			Save();
		}
		else if (e.KeyCode.ToString() == "F6")
		{
			Edit();
		}
		else if (e.KeyCode.ToString() == "F7")
		{
			Delete();
		}
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		Clear();
		Select_AccountDefualt_Result select_AccountDefualt_Result = (from x in Stting.GetAccountDefualt(LoginDetails.BranchID)
			where x.ID == 8
			select x).FirstOrDefault();
		if (select_AccountDefualt_Result != null)
		{
			BtnFatherNum.Text = select_AccountDefualt_Result.AcccountName.ToString();
			LFatherNumId.Text = select_AccountDefualt_Result.AccountID.ToString();
		}
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

	private void TxtName_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		Clear();
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SearchCust(TxtName.Text, 1);
		}
	}

	private void ControlSave_KeyDown(object sender, KeyEventArgs e)
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

	private void CombName_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void TxtNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '+')
		{
			e.Handled = true;
		}
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustID > 0)
			{
				SearchCust(frmSearchCustomer.CustID.ToString(), 0);
			}
			else
			{
				TxtName.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
			Clear();
		}
	}

	private void Clear()
	{
		TxtCode.Text = "";
		LblID.Text = "";
		TxtVatNum.Text = "";
		TxtBuildingNum.Text = "";
		TxtStreetNum.Text = "";
		TxtDistrict.Text = "";
		TxtCity.Text = "";
		TxtCountry.Text = "";
		TxtPostalCode.Text = "";
		TxtAdditionalNum.Text = "";
		TxtCommercialRecord.Text = "";
		TxtCreditLimit.Text = "0.00";
		TxtAlertLimit.Text = "0.00";
		TxtName.Text = "";
		TxtPhone.Text = "";
		TxtFax.Text = "";
		RBCustOnly.Checked = true;
		RbCustSup.Checked = false;
		CBPlace.SelectedValue = 0;
		CBSalesMan.SelectedValue = 0;
		CbBranch.SelectedValue = LoginDetails.BranchID;
		TxtAccount.Text = "";
		TxtAddress.Text = "";
		TxtNote.Text = "";
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		IsEnableTrue();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 23))
		{
			int num = 0;
			if (TxtName.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					if (Convert.ToDecimal(TxtAlertLimit.Text) > Convert.ToDecimal(TxtCreditLimit.Text))
					{
						MessageBox.Show("لا يمكن ان يكون حد التنبيه اكبر من الحد الائتماني", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						TxtCreditLimit.Focus();
						break;
					}
					if (RBCustOnly.Checked)
					{
						IsCustomer = true;
						IsSupplier = false;
					}
					else
					{
						IsCustomer = true;
						IsSupplier = true;
					}
					num = CUS.Insert(out var id, out var _, out var AccountNo, TxtName.Text, TxtVatNum.Text, TxtPhone.Text, TxtFax.Text, TxtAddress.Text, Convert.ToInt32(CBPlace.SelectedValue), Convert.ToInt32(CBSalesMan.SelectedValue), Convert.ToDecimal(TxtCreditLimit.Text), Convert.ToDecimal(TxtAlertLimit.Text), IsSupplier, IsCustomer, TxtNote.Text, TxtBuildingNum.Text, TxtStreetNum.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, Convert.ToInt32(CbBranch.SelectedValue), Convert.ToInt32(LFatherNumId.Text));
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات العملاء  ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						TxtCode.Text = id.ToString();
						LblID.Text = id.ToString();
						TxtAccount.Text = AccountNo.ToString();
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						IsEnableFalse();
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات العملاء بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (TxtName.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم العملاء ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 23))
		{
			if (!TxtName.Enabled)
			{
				IsEnableTrue();
				BtnEdit.Text = "حفظ F6";
				return;
			}
			int num = 0;
			if (TxtName.Text != "" && TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + TxtName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					if (Convert.ToDecimal(TxtAlertLimit.Text) > Convert.ToDecimal(TxtCreditLimit.Text))
					{
						MessageBox.Show("لا يمكن ان يكون حد التنبيه اكبر من الحد الائتماني", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						TxtCreditLimit.Focus();
						break;
					}
					if (RBCustOnly.Checked)
					{
						IsCustomer = true;
						IsSupplier = false;
					}
					else
					{
						IsCustomer = true;
						IsSupplier = true;
					}
					num = CUS.Update(Convert.ToInt32(LblID.Text), Convert.ToInt32(TxtAccount.Text), TxtName.Text, TxtVatNum.Text, TxtPhone.Text, TxtFax.Text, TxtAddress.Text, Convert.ToInt32(CBPlace.SelectedValue), Convert.ToInt32(CBSalesMan.SelectedValue), Convert.ToDecimal(TxtCreditLimit.Text), Convert.ToDecimal(TxtAlertLimit.Text), IsSupplier, IsCustomer, TxtNote.Text, TxtBuildingNum.Text, TxtStreetNum.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtCommercialRecord.Text, Convert.ToInt32(CbBranch.SelectedValue), Convert.ToInt32(LFatherNumId.Text));
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات العملاء ( " + TxtName.Text + " ) بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						IsEnableFalse();
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات العملاء بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (TxtName.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم العملاء ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 23))
		{
			int num = 0;
			if (TxtName.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtName.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					if (CUS.CheckIsFatrher(Convert.ToInt32(TxtAccount.Text)))
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtName.Text + " ) لوجود ابناء له في شجره الحسابات", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						break;
					}
					num = CUS.Delete(Convert.ToInt32(LblID.Text), Convert.ToInt32(TxtAccount.Text));
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات العملاء ( " + TxtName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						Clear();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtName.Text + " ) لوجود عمليات جارية بهذا العملاء", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر اسم العملاء ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BindPlace()
	{
		CBPlace.DataSource = PLC.GetAll();
		CBPlace.DisplayMember = "Name";
		CBPlace.ValueMember = "ID";
	}

	private void BindSalesMan()
	{
		CBSalesMan.DataSource = SLSMAN.GetAll();
		CBSalesMan.DisplayMember = "Name";
		CBSalesMan.ValueMember = "ID";
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("ID", typeof(int));
		dataTable.Columns.Add("Name", typeof(string));
		foreach (Account_Branch branch in SLSMAN.GetBranches())
		{
			dataTable.Rows.Add(branch.ID, branch.Name);
		}
		DataRow dataRow = dataTable.NewRow();
		dataRow.ItemArray = new object[2] { 0, "الكل" };
		dataTable.Rows.InsertAt(dataRow, 0);
		CbBranch.ValueMember = "ID";
		CbBranch.DisplayMember = "Name";
		CbBranch.DataSource = dataTable;
		CbBranch.SelectedValue = LoginDetails.BranchID;
	}

	private void IsEnableFalse()
	{
		TxtAddress.Enabled = false;
		TxtPhone.Enabled = false;
		TxtName.Enabled = false;
		TxtNote.Enabled = false;
		TxtPhone.Enabled = false;
		TxtVatNum.Enabled = false;
		TxtAlertLimit.Enabled = false;
		TxtCreditLimit.Enabled = false;
		TxtFax.Enabled = false;
		TxtCommercialRecord.Enabled = false;
		TxtBuildingNum.Enabled = false;
		TxtStreetNum.Enabled = false;
		TxtDistrict.Enabled = false;
		TxtCity.Enabled = false;
		TxtCountry.Enabled = false;
		TxtPostalCode.Enabled = false;
		TxtAlertLimit.Enabled = false;
		TxtCreditLimit.Enabled = false;
		TxtFax.Enabled = false;
		TxtCommercialRecord.Enabled = false;
		TxtAdditionalNum.Enabled = false;
		CBPlace.Enabled = false;
		CBSalesMan.Enabled = false;
		RBCustOnly.Enabled = false;
		RbCustSup.Enabled = false;
		BtnFatherNum.Enabled = false;
		BtnEdit.Text = "تعديل F6";
	}

	private void TxtName_TextChanged(object sender, EventArgs e)
	{
	}

	private void IsEnableTrue()
	{
		TxtAddress.Enabled = true;
		TxtPhone.Enabled = true;
		TxtName.Enabled = true;
		TxtNote.Enabled = true;
		TxtPhone.Enabled = true;
		TxtVatNum.Enabled = true;
		TxtAlertLimit.Enabled = true;
		TxtCreditLimit.Enabled = true;
		TxtFax.Enabled = true;
		CBPlace.Enabled = true;
		CBSalesMan.Enabled = true;
		RBCustOnly.Enabled = true;
		RbCustSup.Enabled = true;
		TxtBuildingNum.Enabled = true;
		TxtStreetNum.Enabled = true;
		TxtDistrict.Enabled = true;
		TxtCity.Enabled = true;
		TxtCountry.Enabled = true;
		TxtPostalCode.Enabled = true;
		TxtAlertLimit.Enabled = true;
		TxtCreditLimit.Enabled = true;
		TxtFax.Enabled = true;
		TxtCommercialRecord.Enabled = true;
		TxtAdditionalNum.Enabled = true;
		BtnFatherNum.Enabled = true;
		BtnEdit.Text = "تعديل F6";
	}

	private void AutoComplate()
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		TxtName.AutoCompleteCustomSource = NameSugg;
		TxtName.AutoCompleteMode = AutoCompleteMode.Suggest;
		TxtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
		foreach (Account_CustSup item in gTSdbEntities.Account_CustSup.Where((Account_CustSup s) => s.IsCustomers == (bool?)true))
		{
			NameSugg.Add(item.CustSuppName);
		}
		TxtName.AutoCompleteCustomSource = NameSugg;
	}

	private void BtnFatherNum_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccountForTree frmSearchAccountForTree = new FrmSearchAccountForTree();
			frmSearchAccountForTree.ShowDialog();
			if (frmSearchAccountForTree.CustCode > 0)
			{
				int FrmID = frmSearchAccountForTree.CustCode;
				Account_Accounts account_Accounts = (from x in Acct.GetAccounts()
					where x.ID == FrmID
					select x).FirstOrDefault();
				BtnFatherNum.Text = account_Accounts.Account_Name.ToString();
				LFatherNumId.Text = account_Accounts.ID.ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void SearchCust(string Customer, int Active)
	{
		Account_CustSup CustDetails;
		if (Active == 0)
		{
			int FrmID = int.Parse(Customer);
			CustDetails = (from x in CUS.GetAll()
				where x.ID == FrmID
				select x).FirstOrDefault();
		}
		else
		{
			CustDetails = (from x in CUS.GetAll()
				where x.CustSuppName == Customer && x.IsCustomers == true && x.FrmCust == true
				select x).FirstOrDefault();
		}
		if (CustDetails != null)
		{
			LblID.Text = CustDetails.ID.ToString();
			TxtCode.Text = CustDetails.ID.ToString();
			TxtVatNum.Text = CustDetails.VatNum.ToString();
			CBPlace.SelectedValue = CustDetails.PlaceID;
			CBSalesMan.SelectedValue = CustDetails.SalesManID;
			TxtName.Text = CustDetails.CustSuppName;
			TxtPhone.Text = CustDetails.Phone;
			TxtFax.Text = CustDetails.Fax;
			TxtBuildingNum.Text = CustDetails.BuildingNum;
			TxtStreetNum.Text = CustDetails.Street;
			TxtDistrict.Text = CustDetails.District;
			TxtCity.Text = CustDetails.City;
			TxtCountry.Text = CustDetails.Country;
			TxtPostalCode.Text = CustDetails.PostalCode;
			TxtAdditionalNum.Text = CustDetails.AdditionalNum;
			TxtCommercialRecord.Text = CustDetails.CommercialRecord;
			TxtAlertLimit.Text = CustDetails.AlarmLimit.ToString();
			TxtCreditLimit.Text = CustDetails.CreditLimit.ToString();
			TxtAccount.Text = CustDetails.AccountNo.ToString();
			TxtAddress.Text = CustDetails.Address;
			TxtNote.Text = CustDetails.Note;
			CbBranch.SelectedValue = CustDetails.BranchID;
			Account_Accounts account_Accounts = (from x in Acct.GetAccounts()
				where x.Account_No == CustDetails.AccountNo
				select x).FirstOrDefault();
			int AccountFather = Convert.ToInt32(account_Accounts.Main_Account_No);
			Account_Accounts account_Accounts2 = (from x in Acct.GetAccounts()
				where x.ID == AccountFather
				select x).FirstOrDefault();
			BtnFatherNum.Text = account_Accounts2.Account_Name.ToString();
			LFatherNumId.Text = account_Accounts2.ID.ToString();
			if (CustDetails.IsCustomers == true && CustDetails.IsSuppliers == true)
			{
				RbCustSup.Checked = true;
				RBCustOnly.Checked = false;
			}
			else
			{
				RbCustSup.Checked = false;
				RBCustOnly.Checked = true;
			}
			BtnSave.Enabled = false;
			BtnEdit.Enabled = true;
			BtnDelete.Enabled = true;
			IsEnableFalse();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Cust_Sup.FrmCustomer));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label21 = new System.Windows.Forms.Label();
		this.CbBranch = new System.Windows.Forms.ComboBox();
		this.TxtCreditLimit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label6 = new System.Windows.Forms.Label();
		this.TxtAlertLimit = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtCity = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtDistrict = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtStreetNum = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtCommercialRecord = new System.Windows.Forms.TextBox();
		this.TxtBuildingNum = new System.Windows.Forms.TextBox();
		this.TxtAdditionalNum = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtPostalCode = new System.Windows.Forms.TextBox();
		this.TxtPhone = new System.Windows.Forms.TextBox();
		this.TxtCountry = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtFax = new System.Windows.Forms.TextBox();
		this.TxtName = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.CBPlace = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtVatNum = new System.Windows.Forms.TextBox();
		this.CBSalesMan = new System.Windows.Forms.ComboBox();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.LblID = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtAccount = new DevExpress.XtraEditors.ButtonEdit();
		this.label12 = new System.Windows.Forms.Label();
		this.RbCustSup = new System.Windows.Forms.RadioButton();
		this.label11 = new System.Windows.Forms.Label();
		this.RBCustOnly = new System.Windows.Forms.RadioButton();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtAddress = new System.Windows.Forms.TextBox();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnFatherNum = new DevExpress.XtraEditors.ButtonEdit();
		this.label22 = new System.Windows.Forms.Label();
		this.LFatherNumId = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccount.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnFatherNum.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.CbBranch);
		this.groupBox1.Controls.Add(this.TxtCreditLimit);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.TxtAlertLimit);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.TxtCity);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.TxtDistrict);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtStreetNum);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.TxtCommercialRecord);
		this.groupBox1.Controls.Add(this.TxtBuildingNum);
		this.groupBox1.Controls.Add(this.TxtAdditionalNum);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.TxtPostalCode);
		this.groupBox1.Controls.Add(this.TxtPhone);
		this.groupBox1.Controls.Add(this.TxtCountry);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.TxtFax);
		this.groupBox1.Controls.Add(this.TxtName);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.CBPlace);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtVatNum);
		this.groupBox1.Controls.Add(this.CBSalesMan);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Controls.Add(this.LblID);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.CbBranch, "CbBranch");
		this.CbBranch.FormattingEnabled = true;
		this.CbBranch.Name = "CbBranch";
		resources.ApplyResources(this.TxtCreditLimit, "TxtCreditLimit");
		this.TxtCreditLimit.ForeColor = System.Drawing.Color.Black;
		this.TxtCreditLimit.Name = "TxtCreditLimit";
		this.TxtCreditLimit.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCreditLimit.Value = new decimal(new int[4]);
		this.TxtCreditLimit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.TxtAlertLimit, "TxtAlertLimit");
		this.TxtAlertLimit.ForeColor = System.Drawing.Color.Black;
		this.TxtAlertLimit.Name = "TxtAlertLimit";
		this.TxtAlertLimit.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtAlertLimit.Value = new decimal(new int[4]);
		this.TxtAlertLimit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.label20, "label20");
		this.label20.Name = "label20";
		this.label20.Tag = "";
		resources.ApplyResources(this.label18, "label18");
		this.label18.Name = "label18";
		resources.ApplyResources(this.label16, "label16");
		this.label16.Name = "label16";
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.label14, "label14");
		this.label14.Name = "label14";
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.TxtCity, "TxtCity");
		this.TxtCity.Name = "TxtCity";
		this.TxtCity.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label19, "label19");
		this.label19.Name = "label19";
		resources.ApplyResources(this.TxtDistrict, "TxtDistrict");
		this.TxtDistrict.Name = "TxtDistrict";
		this.TxtDistrict.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label17, "label17");
		this.label17.Name = "label17";
		resources.ApplyResources(this.TxtStreetNum, "TxtStreetNum");
		this.TxtStreetNum.Name = "TxtStreetNum";
		this.TxtStreetNum.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label15, "label15");
		this.label15.Name = "label15";
		resources.ApplyResources(this.TxtCommercialRecord, "TxtCommercialRecord");
		this.TxtCommercialRecord.Name = "TxtCommercialRecord";
		this.TxtCommercialRecord.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtCommercialRecord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtBuildingNum, "TxtBuildingNum");
		this.TxtBuildingNum.Name = "TxtBuildingNum";
		this.TxtBuildingNum.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtAdditionalNum, "TxtAdditionalNum");
		this.TxtAdditionalNum.Name = "TxtAdditionalNum";
		this.TxtAdditionalNum.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtAdditionalNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.label13, "label13");
		this.label13.Name = "label13";
		resources.ApplyResources(this.TxtPostalCode, "TxtPostalCode");
		this.TxtPostalCode.Name = "TxtPostalCode";
		this.TxtPostalCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtPostalCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtPhone, "TxtPhone");
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtCountry, "TxtCountry");
		this.TxtCountry.Name = "TxtCountry";
		this.TxtCountry.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.TxtFax, "TxtFax");
		this.TxtFax.Name = "TxtFax";
		this.TxtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtFax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtName, "TxtName");
		this.TxtName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtName.Name = "TxtName";
		this.TxtName.TextChanged += new System.EventHandler(TxtName_TextChanged);
		this.TxtName.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.CBPlace, "CBPlace");
		this.CBPlace.FormattingEnabled = true;
		this.CBPlace.Name = "CBPlace";
		this.CBPlace.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBPlace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtVatNum, "TxtVatNum");
		this.TxtVatNum.Name = "TxtVatNum";
		this.TxtVatNum.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtVatNum.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtVatNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.CBSalesMan, "CBSalesMan");
		this.CBSalesMan.FormattingEnabled = true;
		this.CBSalesMan.Name = "CBSalesMan";
		this.CBSalesMan.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBSalesMan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		resources.ApplyResources(this.TxtCode, "TxtCode");
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
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
		resources.ApplyResources(this.LblID, "LblID");
		this.LblID.Name = "LblID";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.TxtAccount, "TxtAccount");
		this.TxtAccount.Name = "TxtAccount";
		this.TxtAccount.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtAccount.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAccount.Properties.Appearance.Font");
		this.TxtAccount.Properties.Appearance.Options.UseBackColor = true;
		this.TxtAccount.Properties.Appearance.Options.UseFont = true;
		this.TxtAccount.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAccount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAccount.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtAccount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtAccount.Properties.ReadOnly = true;
		this.TxtAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.RbCustSup, "RbCustSup");
		this.RbCustSup.Name = "RbCustSup";
		this.RbCustSup.UseVisualStyleBackColor = true;
		this.RbCustSup.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label11, "label11");
		this.label11.Name = "label11";
		resources.ApplyResources(this.RBCustOnly, "RBCustOnly");
		this.RBCustOnly.Checked = true;
		this.RBCustOnly.Name = "RBCustOnly";
		this.RBCustOnly.TabStop = true;
		this.RBCustOnly.UseVisualStyleBackColor = true;
		this.RBCustOnly.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNote.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.TxtAddress, "TxtAddress");
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnEdit.Appearance.Font");
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNew.Appearance.Font");
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDelete.Appearance.Font");
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.LFatherNumId);
		this.groupBox2.Controls.Add(this.BtnFatherNum);
		this.groupBox2.Controls.Add(this.label22);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.RBCustOnly);
		this.groupBox2.Controls.Add(this.TxtAddress);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtNote);
		this.groupBox2.Controls.Add(this.TxtAccount);
		this.groupBox2.Controls.Add(this.RbCustSup);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.BtnFatherNum, "BtnFatherNum");
		this.BtnFatherNum.Name = "BtnFatherNum";
		this.BtnFatherNum.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.BtnFatherNum.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("buttonEdit1.Properties.Appearance.Font");
		this.BtnFatherNum.Properties.Appearance.Options.UseBackColor = true;
		this.BtnFatherNum.Properties.Appearance.Options.UseFont = true;
		this.BtnFatherNum.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnFatherNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnFatherNum.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.BtnFatherNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.BtnFatherNum.Properties.ReadOnly = true;
		this.BtnFatherNum.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnFatherNum_ButtonClick);
		resources.ApplyResources(this.label22, "label22");
		this.label22.Name = "label22";
		resources.ApplyResources(this.LFatherNumId, "LFatherNumId");
		this.LFatherNumId.Name = "LFatherNumId";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmCustomer";
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccount.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnFatherNum.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


