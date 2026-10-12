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
using GTSErpSystem.Frms.FrmsMain.Search;

namespace GTSErpSystem.Frms.Account;

public class FrmCardAccount : XtraForm
{
	private Class_AccountCard Acct = new Class_AccountCard();

	private Class_AccountTree Tree = new Class_AccountTree();

	private GTSdbEntities db = new GTSdbEntities();

	public int AccountNum = LoginDetails.AccountSN;

	public int AccountBro = LoginDetails.AccountBrother;

	public string ActionName = LoginDetails.ActionName;

	private IContainer components = null;

	private GroupBox groupBox1;

	private ButtonEdit TxtNameSonEng;

	private ButtonEdit TxtCodeSon;

	private ButtonEdit TxtNameSonAr;

	private ButtonEdit TxtNameFather;

	private ButtonEdit TxtCodeFather;

	private TextBox TxtNote;

	private TextBox TxtAddress;

	private Label label10;

	private TextBox TxtFax;

	private TextBox TxtPhone;

	private System.Windows.Forms.ComboBox CBAccountFinal;

	private System.Windows.Forms.ComboBox CBAccountNature;

	private System.Windows.Forms.ComboBox CBAccountSuspended;

	private Label label6;

	private System.Windows.Forms.ComboBox CBGroupAccount;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private Label LblFatherCode;

	private Label LblLevelSon;

	private Label LblLevelFather;

	private SimpleButton BtnEdit;

	private SimpleButton BtnDelete;

	private Label LblID;

	private SimpleButton BtnCreateNumSon;

	private System.Windows.Forms.ComboBox CbBranches;

	private Label label14;

	private Label label12;

	private Label label11;

	private Label label9;

	private Label label8;

	private Label label7;

	private Label label2;

	private Label label4;

	private Label label5;

	private Label label3;

	private Label label1;

	private Label label13;

	public FrmCardAccount()
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
		BindCombox();
		if (ActionName == "Edit")
		{
			GetAccountDetails();
		}
		else if (ActionName == "Son")
		{
			CreateSon();
		}
		else if (ActionName == "Bro")
		{
			CreateBrother();
		}
		else
		{
			Clear();
		}
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
	}

	private void BindCombox()
	{
		CBGroupAccount.DataSource = Acct.GetAccountType();
		CBGroupAccount.DisplayMember = "Name";
		CBGroupAccount.ValueMember = "ID";
		CBAccountSuspended.DataSource = Acct.GetAccountSuspended();
		CBAccountSuspended.DisplayMember = "Name";
		CBAccountSuspended.ValueMember = "ID";
		CBAccountNature.DataSource = Acct.GetAccountNature();
		CBAccountNature.DisplayMember = "Name";
		CBAccountNature.ValueMember = "ID";
		CBAccountFinal.DataSource = Acct.GetAccountFinal();
		CBAccountFinal.DisplayMember = "Name";
		CBAccountFinal.ValueMember = "ID";
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("ID", typeof(int));
		dataTable.Columns.Add("Name", typeof(string));
		foreach (Account_Branch item in Acct.GetAccountBranches().ToList())
		{
			dataTable.Rows.Add(item.ID, item.Name);
		}
		DataRow dataRow = dataTable.NewRow();
		dataRow.ItemArray = new object[2] { 0, "كل الفروع" };
		dataTable.Rows.InsertAt(dataRow, 0);
		CbBranches.ValueMember = "ID";
		CbBranches.DisplayMember = "Name";
		CbBranches.DataSource = dataTable;
		CbBranches.SelectedValue = 0;
	}

	private void Clear()
	{
		LblID.Text = "0";
		LblFatherCode.Text = "0";
		LblLevelFather.Text = "0";
		LblLevelSon.Text = "1";
		TxtCodeFather.Text = "0";
		TxtNameFather.Text = "";
		TxtCodeSon.Text = "0";
		TxtNameSonAr.Text = "";
		TxtNameSonEng.Text = "";
		CbBranches.SelectedValue = 0;
		CBGroupAccount.SelectedValue = 1;
		CBAccountSuspended.SelectedValue = 1;
		CBAccountNature.SelectedValue = 1;
		CBAccountFinal.SelectedValue = 1;
		TxtPhone.Text = "";
		TxtFax.Text = "";
		TxtAddress.Text = "";
		TxtNote.Text = "";
		TxtNameSonAr.Focus();
	}

	public void GetAccountSon()
	{
		try
		{
			long num = 0L;
			if (TxtCodeFather.Text != "")
			{
				long num2 = Convert.ToInt64(TxtCodeFather.Text);
				if (num2 == 0)
				{
					num = Convert.ToInt64(Tree.GetLevel_0());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 1)
				{
					num = Convert.ToInt64(Tree.GetLevel_110());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 110)
				{
					num = Convert.ToInt64(Tree.GetLevel_110_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 111)
				{
					num = Convert.ToInt64(Tree.GetLevel_111_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 11010 || (num2 > 1101009 && num2 < 1101099))
				{
					num = Convert.ToInt64(Tree.GetLevel_110_10_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 11011 || (num2 > 1101109 && num2 < 1101199))
				{
					num = Convert.ToInt64(Tree.GetLevel_110_11_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 11012 || (num2 > 1101209 && num2 < 1101299))
				{
					num = Convert.ToInt64(Tree.GetLevel_110_12_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 11013 || (num2 >= 110130001 && num2 <= 110139999))
				{
					num = Convert.ToInt64(Tree.GetLevel_110_13_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 11014 || (num2 > 1101409 && num2 < 1101499))
				{
					num = Convert.ToInt64(Tree.GetLevel_110_14_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 11015 || (num2 > 1101509 && num2 < 1101599))
				{
					num = Convert.ToInt64(Tree.GetLevel_110_15_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 11016 || (num2 > 1101609 && num2 < 1101699))
				{
					num = Convert.ToInt64(Tree.GetLevel_110_16_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 11017 || (num2 > 1101709 && num2 < 1101799))
				{
					num = Convert.ToInt64(Tree.GetLevel_110_17_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 11018 || (num2 > 1101809 && num2 < 1101899))
				{
					num = Convert.ToInt64(Tree.GetLevel_110_18_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 11019 || (num2 > 1101909 && num2 < 1101999))
				{
					num = Convert.ToInt64(Tree.GetLevel_110_19_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 111 || (num2 > 11109 && num2 < 11199))
				{
					num = Convert.ToInt64(Tree.GetLevel_111_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 2)
				{
					num = Convert.ToInt64(Tree.GetLevel_210());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 210)
				{
					num = Convert.ToInt64(Tree.GetLevel_210_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 211)
				{
					num = Convert.ToInt64(Tree.GetLevel_211_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 21010 || (num2 >= 210100001 && num2 <= 210109999))
				{
					num = Convert.ToInt64(Tree.GetLevel_210_10_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 21011 || (num2 > 2101109 && num2 < 2101199))
				{
					num = Convert.ToInt64(Tree.GetLevel_210_11_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 21012 || (num2 > 2101209 && num2 < 2101299))
				{
					num = Convert.ToInt64(Tree.GetLevel_210_12_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 21013 || (num2 > 2101309 && num2 < 2101399))
				{
					num = Convert.ToInt64(Tree.GetLevel_210_13_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 21014 || (num2 > 2101409 && num2 < 2101499))
				{
					num = Convert.ToInt64(Tree.GetLevel_210_14_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 21015 || (num2 > 2101509 && num2 < 2101599))
				{
					num = Convert.ToInt64(Tree.GetLevel_210_15_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 21016 || (num2 > 2101609 && num2 < 2101699))
				{
					num = Convert.ToInt64(Tree.GetLevel_210_16_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 21017 || (num2 > 2101709 && num2 < 2101799))
				{
					num = Convert.ToInt64(Tree.GetLevel_210_17_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 21018 || (num2 > 2101809 && num2 < 2101899))
				{
					num = Convert.ToInt64(Tree.GetLevel_210_18_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 21019 || (num2 > 2101909 && num2 < 2101999))
				{
					num = Convert.ToInt64(Tree.GetLevel_210_19_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 211 || (num2 > 21109 && num2 < 21199))
				{
					num = Convert.ToInt64(Tree.GetLevel_211_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 3)
				{
					num = Convert.ToInt64(Tree.GetLevel_310());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 310)
				{
					num = Convert.ToInt64(Tree.GetLevel_310_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 311)
				{
					num = Convert.ToInt64(Tree.GetLevel_311_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31010 || (num2 > 3101009 && num2 < 3101099))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_10_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31011 || (num2 > 3101109 && num2 < 3101199))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_11_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31012 || (num2 > 3101209 && num2 < 3101299))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_12_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31013 || (num2 > 3101309 && num2 < 3101399))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_13_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31014 || (num2 > 3101409 && num2 < 3101499))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_14_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31015 || (num2 > 3101509 && num2 < 3101599))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_15_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31016 || (num2 > 3101609 && num2 < 3101699))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_16_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31017 || (num2 > 3101709 && num2 < 3101799))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_17_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31018 || (num2 > 3101809 && num2 < 3101899))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_18_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31019 || (num2 > 3101909 && num2 < 3101999))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_19_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31020 || (num2 > 3102009 && num2 < 3102099))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_20_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31021 || (num2 > 3102109 && num2 < 3102199))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_21_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31022 || (num2 > 3102209 && num2 < 3102299))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_22_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31023 || (num2 > 3102309 && num2 < 3102399))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_23_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31024 || (num2 > 3102409 && num2 < 3102499))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_24_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31025 || (num2 > 3102509 && num2 < 3102599))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_25_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31026 || (num2 > 3102609 && num2 < 3102699))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_26_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31027 || (num2 > 3102709 && num2 < 3102799))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_27_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 31028 || (num2 > 3102809 && num2 < 3102899))
				{
					num = Convert.ToInt64(Tree.GetLevel_310_28_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 311 || (num2 > 31109 && num2 < 31199))
				{
					num = Convert.ToInt64(Tree.GetLevel_311_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 4)
				{
					num = Convert.ToInt64(Tree.GetLevel_410());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 410)
				{
					num = Convert.ToInt64(Tree.GetLevel_410_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 411)
				{
					num = Convert.ToInt64(Tree.GetLevel_411_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41010 || (num2 > 4101009 && num2 < 4101099))
				{
					num = Convert.ToInt64(Tree.GetLevel_410_10_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41011 || (num2 > 4101109 && num2 < 4101199))
				{
					num = Convert.ToInt64(Tree.GetLevel_410_11_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41012 || (num2 > 4101209 && num2 < 4101299))
				{
					num = Convert.ToInt64(Tree.GetLevel_410_12_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41013 || (num2 > 4101309 && num2 < 4101399))
				{
					num = Convert.ToInt64(Tree.GetLevel_410_13_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41014 || (num2 > 4101409 && num2 < 4101499))
				{
					num = Convert.ToInt64(Tree.GetLevel_410_14_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41015 || (num2 > 4101509 && num2 < 4101599))
				{
					num = Convert.ToInt64(Tree.GetLevel_410_15_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41016 || (num2 > 4101609 && num2 < 4101699))
				{
					num = Convert.ToInt64(Tree.GetLevel_410_16_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41017 || (num2 > 4101709 && num2 < 4101799))
				{
					num = Convert.ToInt64(Tree.GetLevel_410_17_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41018 || (num2 > 4101809 && num2 < 4101899))
				{
					num = Convert.ToInt64(Tree.GetLevel_410_18_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41019 || (num2 > 4101909 && num2 < 4101999))
				{
					num = Convert.ToInt64(Tree.GetLevel_410_19_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41110 || (num2 > 4111009 && num2 < 4111099))
				{
					num = Convert.ToInt64(Tree.GetLevel_411_10_10());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41111 || (num2 > 4111109 && num2 < 4111199))
				{
					num = Convert.ToInt64(Tree.GetLevel_411_11_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41112 || (num2 > 4111209 && num2 < 4111299))
				{
					num = Convert.ToInt64(Tree.GetLevel_411_12_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41113 || (num2 > 4111309 && num2 < 4111399))
				{
					num = Convert.ToInt64(Tree.GetLevel_410_13_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41114 || (num2 > 4111409 && num2 < 4111499))
				{
					num = Convert.ToInt64(Tree.GetLevel_411_14_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41115 || (num2 > 4111509 && num2 < 4111599))
				{
					num = Convert.ToInt64(Tree.GetLevel_411_15_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41116 || (num2 > 4111609 && num2 < 4111699))
				{
					num = Convert.ToInt64(Tree.GetLevel_411_16_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41117 || (num2 > 4111709 && num2 < 4111799))
				{
					num = Convert.ToInt64(Tree.GetLevel_411_17_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41118 || (num2 > 4111809 && num2 < 4111899))
				{
					num = Convert.ToInt64(Tree.GetLevel_411_18_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				if (num2 == 41119 || (num2 > 4111909 && num2 < 4111999))
				{
					num = Convert.ToInt64(Tree.GetLevel_411_19_11());
					TxtCodeSon.Text = Convert.ToString(num);
					return;
				}
				string text = Convert.ToString(num2);
				if (text.Substring(0, 1) == "1")
				{
					string text2 = text.Substring(0, 1);
					num = Convert.ToInt64(Tree.GetLevel_110_20_20());
					TxtCodeSon.Text = Convert.ToString(num);
				}
				else if (text.Substring(0, 1) == "2")
				{
					num = Convert.ToInt64(Tree.GetLevel_210_20_20());
					TxtCodeSon.Text = Convert.ToString(num);
				}
				else if (text.Substring(0, 1) == "3")
				{
					num = Convert.ToInt64(Tree.GetLevel_310_20_20());
					TxtCodeSon.Text = Convert.ToString(num);
				}
				else
				{
					num = Convert.ToInt64(Tree.GetLevel_410_20_20());
					TxtCodeSon.Text = Convert.ToString(num);
				}
			}
			else
			{
				MessageBox.Show("نعتذر، من فضلك اختر حساب الاب ثم حاول مرة أخري", "فشل التعديل", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		catch (Exception)
		{
		}
	}

	private void CombName_KeyPress(object sender, KeyPressEventArgs e)
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

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void TxtNote_KeyDown(object sender, KeyEventArgs e)
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

	private void textBoxEn_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("en"));
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

	public void GetAccountDetails()
	{
		LblLevelFather.Text = "0";
		int FrmID = AccountNum;
		Account_Accounts account_Accounts = (from x in Acct.GetAccounts()
			where x.ID == FrmID
			select x).FirstOrDefault();
		int FatherCode = Convert.ToInt32(account_Accounts.Main_Account_No);
		Account_Accounts account_Accounts2 = (from x in Acct.GetAccounts()
			where x.ID == FatherCode
			select x).FirstOrDefault();
		LblID.Text = FrmID.ToString();
		LblFatherCode.Text = FatherCode.ToString();
		if (account_Accounts2 != null)
		{
			LblLevelFather.Text = account_Accounts2.Account_Level.ToString();
			TxtNameFather.Text = account_Accounts2.Account_Name.ToString();
			TxtCodeFather.Text = account_Accounts2.Account_No.ToString();
		}
		LblLevelSon.Text = account_Accounts.Account_Level.ToString();
		TxtCodeSon.Text = account_Accounts.Account_No.ToString();
		TxtNameSonAr.Text = account_Accounts.Account_Name.ToString();
		TxtNameSonEng.Text = account_Accounts.E_Account_Name.ToString();
		CbBranches.SelectedValue = account_Accounts.BranchID;
		CBGroupAccount.SelectedValue = account_Accounts.Account_Type;
		CBAccountSuspended.SelectedValue = account_Accounts.Suspended;
		CBAccountNature.SelectedValue = account_Accounts.Account_Nature;
		CBAccountFinal.SelectedValue = account_Accounts.Final_Account;
		TxtPhone.Text = account_Accounts.PhoneA.ToString();
		TxtFax.Text = account_Accounts.FaxA ?? "";
		TxtAddress.Text = account_Accounts.AddressA ?? "";
		TxtNote.Text = account_Accounts.Note ?? "";
		BtnSave.Enabled = false;
		BtnEdit.Enabled = true;
		BtnDelete.Enabled = true;
		TxtNameSonAr.Focus();
	}

	public void CreateSon()
	{
		LblLevelFather.Text = "0";
		int FrmID = AccountNum;
		Account_Accounts account_Accounts = (from x in Acct.GetAccounts()
			where x.ID == FrmID
			select x).FirstOrDefault();
		LblID.Text = FrmID.ToString();
		LblFatherCode.Text = FrmID.ToString();
		LblLevelFather.Text = account_Accounts.Account_Level.ToString();
		TxtCodeFather.Text = account_Accounts.Account_No.ToString();
		TxtNameFather.Text = account_Accounts.Account_Name.ToString();
		CbBranches.SelectedValue = account_Accounts.BranchID;
		CBAccountFinal.SelectedValue = account_Accounts.Final_Account;
		CBGroupAccount.SelectedValue = account_Accounts.Account_Type;
		CBAccountSuspended.SelectedValue = account_Accounts.Suspended;
		CBAccountNature.SelectedValue = account_Accounts.Account_Nature;
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtNameSonAr.Focus();
	}

	public void CreateBrother()
	{
		LblLevelFather.Text = "0";
		int FrmID = AccountBro;
		Account_Accounts account_Accounts = (from x in Acct.GetAccounts()
			where x.ID == FrmID
			select x).FirstOrDefault();
		LblID.Text = FrmID.ToString();
		LblFatherCode.Text = FrmID.ToString();
		if (account_Accounts.Account_Level.HasValue)
		{
			LblLevelFather.Text = account_Accounts.Account_Level.ToString();
		}
		else
		{
			LblLevelFather.Text = "0";
		}
		TxtCodeFather.Text = account_Accounts.Account_No.ToString();
		TxtNameFather.Text = account_Accounts.Account_Name.ToString();
		CbBranches.SelectedValue = account_Accounts.BranchID;
		CBAccountFinal.SelectedValue = account_Accounts.Final_Account;
		CBGroupAccount.SelectedValue = account_Accounts.Account_Type;
		CBAccountSuspended.SelectedValue = account_Accounts.Suspended;
		CBAccountNature.SelectedValue = account_Accounts.Account_Nature;
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtNameSonAr.Focus();
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		Clear();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		try
		{
			if (TxtCodeFather.Text == "0" || TxtNameSonAr.Text == "" || TxtCodeSon.Text == "0" || Convert.ToInt32(TxtCodeSon.Text) == Convert.ToInt32(TxtCodeFather.Text))
			{
				if (TxtCodeFather.Text == "0")
				{
					MessageBox.Show("عفوا، اختر رقم حساب الاب", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtCodeFather.Focus();
				}
				else if (TxtCodeSon.Text == "0")
				{
					MessageBox.Show("عفوا، اكتب رقم حساب الابن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtCodeSon.Focus();
				}
				else if (TxtNameSonAr.Text == "")
				{
					MessageBox.Show("عفوا، اكتب اسم الحساب ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtNameSonAr.Focus();
				}
				else if (Convert.ToInt32(TxtCodeSon.Text) == Convert.ToInt32(TxtCodeFather.Text))
				{
					MessageBox.Show("عفوا، لايمكن ان يكون حساب الابن اكبر من او يساوي رقم حساب الاب ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtCodeSon.Focus();
				}
				return;
			}
			int iSFind = Acct.GetISFind(Convert.ToInt32(TxtCodeSon.Text));
			if (iSFind > 0)
			{
				string text = "0";
				int num = 0;
				if (TxtCodeFather.Text.Count() == 3)
				{
					text = TxtCodeFather.Text + "10";
				}
				num = Acct.Insert(Convert.ToInt32(LblFatherCode.Text), Convert.ToInt32(TxtCodeSon.Text), TxtNameSonAr.Text, TxtNameSonEng.Text, Convert.ToInt32(CBGroupAccount.SelectedValue), Convert.ToInt32(CBAccountSuspended.SelectedValue), Convert.ToInt32(CBAccountNature.SelectedValue), Convert.ToInt32(CBAccountFinal.SelectedValue), Convert.ToInt32(LblLevelSon.Text), TxtPhone.Text, TxtFax.Text, TxtAddress.Text, TxtNote.Text, Convert.ToInt32(CbBranches.SelectedValue));
				LblID.Text = num.ToString();
				if (num > 0)
				{
					MessageBox.Show("تم حفظ بيانات الحسابات  ( " + TxtNameSonAr.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
				}
				else
				{
					MessageBox.Show("عفوا، فشل حفظ بيانات الحسابات  ( " + TxtNameSonAr.Text + " )", "فشل الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				MessageBox.Show("عفوا، رقم الحساب مستخدم من قبل  ( " + TxtCodeSon.Text + " ) برجاء المحاولة مرة اخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtCodeSon.Focus();
			}
		}
		catch (Exception)
		{
		}
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		try
		{
			if (TxtCodeFather.Text == "0" || TxtNameSonAr.Text == "" || TxtCodeSon.Text == "0" || Convert.ToInt32(TxtCodeSon.Text) == Convert.ToInt32(TxtCodeFather.Text))
			{
				if (TxtCodeFather.Text == "0")
				{
					MessageBox.Show("عفوا، اختر رقم حساب الاب", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtCodeFather.Focus();
				}
				else if (TxtCodeSon.Text == "0")
				{
					MessageBox.Show("عفوا، اكتب رقم حساب الابن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtCodeSon.Focus();
				}
				else if (TxtNameSonAr.Text == "")
				{
					MessageBox.Show("عفوا، اكتب اسم الحساب ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtNameSonAr.Focus();
				}
				else if (Convert.ToInt32(TxtCodeSon.Text) == Convert.ToInt32(TxtCodeFather.Text))
				{
					MessageBox.Show("عفوا، لايمكن ان يكون حساب الابن اكبر من او يساوي رقم حساب الاب ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtCodeSon.Focus();
				}
			}
			else if (Acct.GetISFind(Convert.ToInt32(TxtCodeSon.Text)) == 0)
			{
				string text = "0";
				int num = 0;
				if (TxtCodeFather.Text.Count() == 3)
				{
					text = TxtCodeFather.Text + "10";
				}
				num = Acct.Update(Convert.ToInt32(LblID.Text), Convert.ToInt32(LblFatherCode.Text), Convert.ToInt32(TxtCodeSon.Text), TxtNameSonAr.Text, TxtNameSonEng.Text, Convert.ToInt32(CBGroupAccount.SelectedValue), Convert.ToInt32(CBAccountSuspended.SelectedValue), Convert.ToInt32(CBAccountNature.SelectedValue), Convert.ToInt32(CBAccountFinal.SelectedValue), Convert.ToInt32(LblLevelSon.Text), TxtPhone.Text, TxtFax.Text, TxtAddress.Text, TxtNote.Text, Convert.ToInt32(CbBranches.SelectedValue));
				if (num > 0)
				{
					MessageBox.Show("تم تعديل بيانات الحسابات  ( " + TxtNameSonAr.Text + " ) بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
				}
				else
				{
					MessageBox.Show("عفوا، فشل تعديل بيانات الحسابات  ( " + TxtNameSonAr.Text + " )", "فشل التعديل", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				MessageBox.Show("عفوا، رقم الحساب مستخدم من قبل  ( " + TxtCodeSon.Text + " ) برجاء المحاولة مرة اخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtCodeSon.Focus();
			}
		}
		catch (Exception)
		{
		}
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		try
		{
			if (TxtCodeFather.Text == "0" || TxtNameSonAr.Text == "" || TxtCodeSon.Text == "0" || Convert.ToInt32(TxtCodeSon.Text) == Convert.ToInt32(TxtCodeFather.Text))
			{
				if (TxtCodeFather.Text == "0")
				{
					MessageBox.Show("عفوا، اختر رقم حساب الاب", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtCodeFather.Focus();
				}
				else if (TxtCodeSon.Text == "0")
				{
					MessageBox.Show("عفوا، اكتب رقم حساب الابن", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtCodeSon.Focus();
				}
				else if (TxtNameSonAr.Text == "")
				{
					MessageBox.Show("عفوا، اكتب اسم الحساب ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtNameSonAr.Focus();
				}
				else if (Convert.ToInt32(TxtCodeSon.Text) == Convert.ToInt32(TxtCodeFather.Text))
				{
					MessageBox.Show("عفوا، لايمكن ان يكون حساب الابن اكبر من او يساوي رقم حساب الاب ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					TxtCodeSon.Focus();
				}
			}
			else
			{
				int num = 0;
				num = Acct.Delete(Convert.ToInt32(LblID.Text));
				if (num > 0)
				{
					MessageBox.Show("تم حذف بيانات الحسابات  ( " + TxtNameSonAr.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					Clear();
					BtnSave.Enabled = true;
					BtnEdit.Enabled = false;
					BtnDelete.Enabled = false;
				}
				else
				{
					MessageBox.Show("عفوا، فشل حذف بيانات الحسابات  ( " + TxtNameSonAr.Text + " )", "فشل الحذف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TxtCodeFather_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccountForTree frmSearchAccountForTree = new FrmSearchAccountForTree();
			frmSearchAccountForTree.ShowDialog();
			if (frmSearchAccountForTree.CustCode > 0)
			{
				LblLevelFather.Text = "0";
				int FrmID = frmSearchAccountForTree.CustCode;
				Account_Accounts account_Accounts = (from x in Acct.GetAccounts()
					where x.ID == FrmID
					select x).FirstOrDefault();
				LblID.Text = FrmID.ToString();
				LblFatherCode.Text = FrmID.ToString();
				LblLevelFather.Text = account_Accounts.Account_Level.ToString();
				TxtCodeFather.Text = account_Accounts.Account_No.ToString();
				TxtNameFather.Text = account_Accounts.Account_Name.ToString();
				CBAccountFinal.SelectedValue = account_Accounts.Final_Account;
				CBGroupAccount.SelectedValue = account_Accounts.Account_Type;
				CBAccountSuspended.SelectedValue = account_Accounts.Suspended;
				CBAccountNature.SelectedValue = account_Accounts.Account_Nature;
				CbBranches.SelectedValue = account_Accounts.BranchID;
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnDelete.Enabled = false;
			}
			else
			{
				TxtNameFather.Focus();
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnDelete.Enabled = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtCodeSon_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccountForTree frmSearchAccountForTree = new FrmSearchAccountForTree();
			frmSearchAccountForTree.ShowDialog();
			if (frmSearchAccountForTree.CustCode > 0)
			{
				LblLevelFather.Text = "0";
				int FrmID = frmSearchAccountForTree.CustCode;
				Account_Accounts account_Accounts = (from x in Acct.GetAccounts()
					where x.ID == FrmID
					select x).FirstOrDefault();
				int FatherCode = Convert.ToInt32(account_Accounts.Main_Account_No);
				Account_Accounts account_Accounts2 = (from x in Acct.GetAccounts()
					where x.ID == FatherCode
					select x).FirstOrDefault();
				LblID.Text = FrmID.ToString();
				LblFatherCode.Text = FatherCode.ToString();
				LblLevelFather.Text = account_Accounts2.Account_Level.ToString();
				TxtCodeFather.Text = account_Accounts2.Account_No.ToString();
				TxtNameFather.Text = account_Accounts2.Account_Name.ToString();
				CbBranches.SelectedValue = account_Accounts.BranchID;
				LblLevelSon.Text = account_Accounts.Account_Level.ToString();
				TxtCodeSon.Text = account_Accounts.Account_No.ToString();
				TxtNameSonAr.Text = account_Accounts.Account_Name.ToString();
				TxtNameSonEng.Text = account_Accounts.E_Account_Name.ToString();
				CBGroupAccount.SelectedValue = account_Accounts.Account_Type;
				CBAccountSuspended.SelectedValue = account_Accounts.Suspended;
				CBAccountNature.SelectedValue = account_Accounts.Account_Nature;
				CBAccountFinal.SelectedValue = account_Accounts.Final_Account;
				TxtPhone.Text = account_Accounts.PhoneA.ToString();
				TxtFax.Text = account_Accounts.FaxA.ToString();
				TxtAddress.Text = account_Accounts.AddressA.ToString();
				TxtNote.Text = account_Accounts.Note.ToString();
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
			}
			else
			{
				TxtNameSonAr.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void LblLevelFather_TextChanged(object sender, EventArgs e)
	{
		LblLevelSon.Text = (Convert.ToInt32(LblLevelFather.Text) + 1).ToString();
	}

	private void BtnCreateNumSon_Click(object sender, EventArgs e)
	{
		GetAccountSon();
	}

	private void TxtCodeFather_TextChanged(object sender, EventArgs e)
	{
		GetAccountSon();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Account.FrmCardAccount));
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.CbBranches = new System.Windows.Forms.ComboBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCreateNumSon = new DevExpress.XtraEditors.SimpleButton();
		this.LblID = new System.Windows.Forms.Label();
		this.LblLevelSon = new System.Windows.Forms.Label();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.LblLevelFather = new System.Windows.Forms.Label();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.LblFatherCode = new System.Windows.Forms.Label();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.TxtAddress = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtFax = new System.Windows.Forms.TextBox();
		this.TxtPhone = new System.Windows.Forms.TextBox();
		this.CBAccountFinal = new System.Windows.Forms.ComboBox();
		this.CBAccountNature = new System.Windows.Forms.ComboBox();
		this.CBAccountSuspended = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.CBGroupAccount = new System.Windows.Forms.ComboBox();
		this.TxtNameSonEng = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtCodeSon = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtNameSonAr = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtNameFather = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtCodeFather = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNameSonEng.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeSon.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNameSonAr.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNameFather.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeFather.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.CbBranches);
		this.groupBox1.Controls.Add(this.BtnExit);
		this.groupBox1.Controls.Add(this.BtnDelete);
		this.groupBox1.Controls.Add(this.BtnEdit);
		this.groupBox1.Controls.Add(this.BtnCreateNumSon);
		this.groupBox1.Controls.Add(this.LblID);
		this.groupBox1.Controls.Add(this.LblLevelSon);
		this.groupBox1.Controls.Add(this.BtnSave);
		this.groupBox1.Controls.Add(this.LblLevelFather);
		this.groupBox1.Controls.Add(this.BtnNew);
		this.groupBox1.Controls.Add(this.LblFatherCode);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.TxtAddress);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.TxtFax);
		this.groupBox1.Controls.Add(this.TxtPhone);
		this.groupBox1.Controls.Add(this.CBAccountFinal);
		this.groupBox1.Controls.Add(this.CBAccountNature);
		this.groupBox1.Controls.Add(this.CBAccountSuspended);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.CBGroupAccount);
		this.groupBox1.Controls.Add(this.TxtNameSonEng);
		this.groupBox1.Controls.Add(this.TxtCodeSon);
		this.groupBox1.Controls.Add(this.TxtNameSonAr);
		this.groupBox1.Controls.Add(this.TxtNameFather);
		this.groupBox1.Controls.Add(this.TxtCodeFather);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.label14, "label14");
		this.label14.Name = "label14";
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.label11, "label11");
		this.label11.Name = "label11";
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.label13, "label13");
		this.label13.Name = "label13";
		resources.ApplyResources(this.CbBranches, "CbBranches");
		this.CbBranches.FormattingEnabled = true;
		this.CbBranches.Name = "CbBranches";
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDelete.Appearance.Font");
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnEdit.Appearance.Font");
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		resources.ApplyResources(this.BtnCreateNumSon, "BtnCreateNumSon");
		this.BtnCreateNumSon.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCreateNumSon.ImageOptions.Image");
		this.BtnCreateNumSon.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCreateNumSon.Name = "BtnCreateNumSon";
		this.BtnCreateNumSon.Click += new System.EventHandler(BtnCreateNumSon_Click);
		resources.ApplyResources(this.LblID, "LblID");
		this.LblID.Name = "LblID";
		resources.ApplyResources(this.LblLevelSon, "LblLevelSon");
		this.LblLevelSon.Name = "LblLevelSon";
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.LblLevelFather, "LblLevelFather");
		this.LblLevelFather.Name = "LblLevelFather";
		this.LblLevelFather.TextChanged += new System.EventHandler(LblLevelFather_TextChanged);
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.LblFatherCode, "LblFatherCode");
		this.LblFatherCode.Name = "LblFatherCode";
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNote.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtNote_KeyDown);
		resources.ApplyResources(this.TxtAddress, "TxtAddress");
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.TxtFax, "TxtFax");
		this.TxtFax.Name = "TxtFax";
		this.TxtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtFax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtPhone, "TxtPhone");
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.CBAccountFinal, "CBAccountFinal");
		this.CBAccountFinal.FormattingEnabled = true;
		this.CBAccountFinal.Name = "CBAccountFinal";
		this.CBAccountFinal.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBAccountFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		resources.ApplyResources(this.CBAccountNature, "CBAccountNature");
		this.CBAccountNature.FormattingEnabled = true;
		this.CBAccountNature.Name = "CBAccountNature";
		this.CBAccountNature.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBAccountNature.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		resources.ApplyResources(this.CBAccountSuspended, "CBAccountSuspended");
		this.CBAccountSuspended.FormattingEnabled = true;
		this.CBAccountSuspended.Name = "CBAccountSuspended";
		this.CBAccountSuspended.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBAccountSuspended.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.CBGroupAccount, "CBGroupAccount");
		this.CBGroupAccount.FormattingEnabled = true;
		this.CBGroupAccount.Name = "CBGroupAccount";
		this.CBGroupAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBGroupAccount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		resources.ApplyResources(this.TxtNameSonEng, "TxtNameSonEng");
		this.TxtNameSonEng.Name = "TxtNameSonEng";
		this.TxtNameSonEng.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtNameSonEng.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNameSonEng.Properties.Appearance.Font");
		this.TxtNameSonEng.Properties.Appearance.Options.UseBackColor = true;
		this.TxtNameSonEng.Properties.Appearance.Options.UseFont = true;
		this.TxtNameSonEng.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNameSonEng.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNameSonEng.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtNameSonEng.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.TxtNameSonEng.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtNameSonEng.Properties.Buttons"), resources.GetString("TxtNameSonEng.Properties.Buttons1"), (int)resources.GetObject("TxtNameSonEng.Properties.Buttons2"), (bool)resources.GetObject("TxtNameSonEng.Properties.Buttons3"), (bool)resources.GetObject("TxtNameSonEng.Properties.Buttons4"), (bool)resources.GetObject("TxtNameSonEng.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtNameSonEng.Properties.Buttons6"), resources.GetObject("TxtNameSonEng.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtNameSonEng.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtNameSonEng.Properties.Buttons9"))
		});
		this.TxtNameSonEng.Enter += new System.EventHandler(textBoxEn_Enter);
		this.TxtNameSonEng.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtCodeSon, "TxtCodeSon");
		this.TxtCodeSon.Name = "TxtCodeSon";
		this.TxtCodeSon.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCodeSon.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCodeSon.Properties.Appearance.Font");
		this.TxtCodeSon.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCodeSon.Properties.Appearance.Options.UseFont = true;
		this.TxtCodeSon.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCodeSon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCodeSon.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtCodeSon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.TxtCodeSon.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtCodeSon.Properties.Buttons"), resources.GetString("TxtCodeSon.Properties.Buttons1"), (int)resources.GetObject("TxtCodeSon.Properties.Buttons2"), (bool)resources.GetObject("TxtCodeSon.Properties.Buttons3"), (bool)resources.GetObject("TxtCodeSon.Properties.Buttons4"), (bool)resources.GetObject("TxtCodeSon.Properties.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("TxtCodeSon.Properties.Buttons6"), resources.GetObject("TxtCodeSon.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtCodeSon.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtCodeSon.Properties.Buttons9"))
		});
		this.TxtCodeSon.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCodeSon_ButtonClick);
		this.TxtCodeSon.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtCodeSon.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtNameSonAr, "TxtNameSonAr");
		this.TxtNameSonAr.Name = "TxtNameSonAr";
		this.TxtNameSonAr.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtNameSonAr.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNameSonAr.Properties.Appearance.Font");
		this.TxtNameSonAr.Properties.Appearance.Options.UseBackColor = true;
		this.TxtNameSonAr.Properties.Appearance.Options.UseFont = true;
		this.TxtNameSonAr.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNameSonAr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNameSonAr.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtNameSonAr.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		this.TxtNameSonAr.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtNameSonAr.Properties.Buttons"), resources.GetString("TxtNameSonAr.Properties.Buttons1"), (int)resources.GetObject("TxtNameSonAr.Properties.Buttons2"), (bool)resources.GetObject("TxtNameSonAr.Properties.Buttons3"), (bool)resources.GetObject("TxtNameSonAr.Properties.Buttons4"), (bool)resources.GetObject("TxtNameSonAr.Properties.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("TxtNameSonAr.Properties.Buttons6"), resources.GetObject("TxtNameSonAr.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtNameSonAr.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtNameSonAr.Properties.Buttons9"))
		});
		this.TxtNameSonAr.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNameSonAr.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtNameFather, "TxtNameFather");
		this.TxtNameFather.Name = "TxtNameFather";
		this.TxtNameFather.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 255, 210);
		this.TxtNameFather.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNameFather.Properties.Appearance.Font");
		this.TxtNameFather.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(32, 31, 53);
		this.TxtNameFather.Properties.Appearance.Options.UseBackColor = true;
		this.TxtNameFather.Properties.Appearance.Options.UseFont = true;
		this.TxtNameFather.Properties.Appearance.Options.UseForeColor = true;
		this.TxtNameFather.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNameFather.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNameFather.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtNameFather.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.TxtNameFather.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtNameFather.Properties.Buttons"), resources.GetString("TxtNameFather.Properties.Buttons1"), (int)resources.GetObject("TxtNameFather.Properties.Buttons2"), (bool)resources.GetObject("TxtNameFather.Properties.Buttons3"), (bool)resources.GetObject("TxtNameFather.Properties.Buttons4"), (bool)resources.GetObject("TxtNameFather.Properties.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("TxtNameFather.Properties.Buttons6"), resources.GetObject("TxtNameFather.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtNameFather.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtNameFather.Properties.Buttons9"))
		});
		this.TxtNameFather.Properties.ReadOnly = true;
		this.TxtNameFather.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNameFather.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtCodeFather, "TxtCodeFather");
		this.TxtCodeFather.Name = "TxtCodeFather";
		this.TxtCodeFather.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCodeFather.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCodeFather.Properties.Appearance.Font");
		this.TxtCodeFather.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCodeFather.Properties.Appearance.Options.UseFont = true;
		this.TxtCodeFather.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCodeFather.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCodeFather.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtCodeFather.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions5");
		this.TxtCodeFather.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtCodeFather.Properties.Buttons"), resources.GetString("TxtCodeFather.Properties.Buttons1"), (int)resources.GetObject("TxtCodeFather.Properties.Buttons2"), (bool)resources.GetObject("TxtCodeFather.Properties.Buttons3"), (bool)resources.GetObject("TxtCodeFather.Properties.Buttons4"), (bool)resources.GetObject("TxtCodeFather.Properties.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("TxtCodeFather.Properties.Buttons6"), resources.GetObject("TxtCodeFather.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtCodeFather.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtCodeFather.Properties.Buttons9"))
		});
		this.TxtCodeFather.Properties.ReadOnly = true;
		this.TxtCodeFather.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCodeFather_ButtonClick);
		this.TxtCodeFather.TextChanged += new System.EventHandler(TxtCodeFather_TextChanged);
		this.TxtCodeFather.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtCodeFather.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmCardAccount";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNameSonEng.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeSon.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNameSonAr.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNameFather.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeFather.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


