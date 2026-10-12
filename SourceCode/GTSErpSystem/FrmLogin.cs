using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Main.Setting;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.FrmsHome;
using GTSErpSystem.Properties;

namespace GTSErpSystem;

public class FrmLogin : Form
{
	private Class_Login LGN = new Class_Login();

	private Class_Users USR = new Class_Users();

	private Class_Groups GRP = new Class_Groups();

	private Class_Setting Stting = new Class_Setting();

	private Class_DefualtAccount DfltAccnt = new Class_DefualtAccount();

	private GTSdbEntities db = new GTSdbEntities();

	private IContainer components = null;

	private Panel panel1;

	private LinkLabel linkLabel1;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label8;

	private Label label5;

	private Button BtnLogin;

	private TextBox TxtPassword;

	private ComboBox TxtName;

	private ComboBox TxtBranch;

	private PictureBox pictureBox1;

	private Button BtnExit;

	private Button Btn1;

	private Button Btn2;

	private Button Btn3;

	private Button Btn4;

	private Button Btn5;

	private Button Btn6;

	private Button Btn7;

	private Button Btn8;

	private Button Btn9;

	private Button Btn0;

	private Button BtnDelete;

	private Label LblLang;

	private PictureBox pictureBox2;

	private PictureBox pictureBox3;

	private PictureBox pictureBox4;

	private PictureBox pictureBox5;

	private PictureBox pictureBox6;

	public FrmLogin()
	{
		Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
		LoginDetails.Lang = "ar-EG";
		InitializeComponent();
		BindCBBranch();
		BindCBGroups();
		TxtBranch.Focus();
	}

	private void BindCBGroups()
	{
		int BID = Convert.ToInt32(TxtBranch.SelectedValue);
		TxtName.DataSource = (from x in USR.GetAllUsers()
			where x.BranchID == BID && x.IsActive == true
			select x).ToList();
		TxtName.DisplayMember = "Name";
		TxtName.ValueMember = "ID";
	}

	private void BindCBBranch()
	{
		try
		{
			TxtBranch.DataSource = USR.GetAllBranch();
			TxtBranch.DisplayMember = "Name";
			TxtBranch.ValueMember = "ID";
			TxtBranch.SelectedValue = 1;
		}
		catch (Exception)
		{
			MessageBox.Show("حدث خطأ اثناء الدخول برجاء الاتصال بالدعم الفني الخاص بالبرنامج", "تسجيل الدخول", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BindLogin(int Result)
	{
		int BrnchCode = Convert.ToInt32(TxtBranch.SelectedValue);
		List<Select_AccountDefualt_Result> accountDefualt = DfltAccnt.GetAccountDefualt(BrnchCode);
		TblSetting tblSetting = (from x in Stting.GetAll()
			where x.ID == BrnchCode
			select x).FirstOrDefault();
		User_Groups user_Groups = (from x in GRP.GetAllGroups()
			where x.ID == Result
			select x).FirstOrDefault();
		List<Select_AccountCustomer_Result> customerDefualt = DfltAccnt.GetCustomerDefualt(BrnchCode);
		LoginDetails.StoreIDPurches = Convert.ToInt32(tblSetting.StoreIDPurches);
		LoginDetails.StoreID = Convert.ToInt32(tblSetting.StoreID);
		LoginDetails.StoreIDWaiting = Convert.ToInt32(tblSetting.StoreIDWaiying);
		LoginDetails.Acc_CostItemOrder = Convert.ToInt32((from x in accountDefualt
			where x.ID == 19 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_VatNum = Convert.ToInt32((from x in accountDefualt
			where x.ID == 21 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_CashBox = Convert.ToInt32((from x in accountDefualt
			where x.ID == 11 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_OrderCash = Convert.ToInt32((from x in accountDefualt
			where x.ID == 1 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_OrderAjila = Convert.ToInt32((from x in accountDefualt
			where x.ID == 3 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_OrderReturnCash = Convert.ToInt32((from x in accountDefualt
			where x.ID == 2 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_BankNumCachir = Convert.ToInt32((from x in accountDefualt
			where x.ID == 20 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.StoreEntagTam = Convert.ToInt32((from x in accountDefualt
			where x.ID == 17 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_StockNum = Convert.ToInt32((from x in accountDefualt
			where x.ID == 18 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_Payroll = Convert.ToInt32((from x in accountDefualt
			where x.ID == 22 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_CustomerCashAccount = Convert.ToInt32((from x in accountDefualt
			where x.ID == 10 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_HangerAccount = Convert.ToInt32((from x in accountDefualt
			where x.ID == 23 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_RoomAccount = Convert.ToInt32((from x in accountDefualt
			where x.ID == 24 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_TableAccount = Convert.ToInt32((from x in accountDefualt
			where x.ID == 25 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_TawselAccount = Convert.ToInt32((from x in accountDefualt
			where x.ID == 26 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_InventorySettlementMinus = Convert.ToInt32((from x in accountDefualt
			where x.ID == 27 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_CheckIncome = Convert.ToInt32((from x in accountDefualt
			where x.ID == 28 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_CheckOutCome = Convert.ToInt32((from x in accountDefualt
			where x.ID == 29 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_Rewards = Convert.ToInt32((from x in accountDefualt
			where x.ID == 31 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_Penalties = Convert.ToInt32((from x in accountDefualt
			where x.ID == 32 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_Bonus = Convert.ToInt32((from x in accountDefualt
			where x.ID == 33 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_Absences = Convert.ToInt32((from x in accountDefualt
			where x.ID == 34 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_LateArrivals = Convert.ToInt32((from x in accountDefualt
			where x.ID == 35 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_Overtime = Convert.ToInt32((from x in accountDefualt
			where x.ID == 36 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_Allowances = Convert.ToInt32((from x in accountDefualt
			where x.ID == 37 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_CustomerCash = Convert.ToInt32((from x in customerDefualt
			where x.ID == 5 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_Hanger = Convert.ToInt32((from x in customerDefualt
			where x.ID == 1 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_Room = Convert.ToInt32((from x in customerDefualt
			where x.ID == 2 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_Table = Convert.ToInt32((from x in customerDefualt
			where x.ID == 3 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_Tawsel = Convert.ToInt32((from x in customerDefualt
			where x.ID == 4 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.IsPrintSecondFatoraCasheir = Convert.ToBoolean(tblSetting.IsPrintSecondFatoraCasheir);
		LoginDetails.SecondPrintDefualt = tblSetting.SecondPrintDefualt;
		LoginDetails.IsPrintSecondCasheir = Convert.ToBoolean(tblSetting.IsPrintSecondCasheir);
		LoginDetails.IsTakeDateApp = Convert.ToBoolean(tblSetting.IsTakeDateApp);
		LoginDetails.DateApp = DateTime.Now;
		LoginDetails.GroupSize = Convert.ToInt32(tblSetting.GroupSize);
		LoginDetails.GroupColor = tblSetting.GroupColor;
		LoginDetails.GroupFontColor = tblSetting.GroupFontColor;
		LoginDetails.ItemSize = Convert.ToInt32(tblSetting.ItemSize);
		LoginDetails.ItemColor = tblSetting.ItemColor;
		LoginDetails.ItemFontColor = tblSetting.ItemFontColor;
		LoginDetails.IsItemNameAR = Convert.ToBoolean(tblSetting.IsItemNameAR);
		LoginDetails.IsItemNameEN = Convert.ToBoolean(tblSetting.IsItemNameEN);
		LoginDetails.IsItemShowPrice = Convert.ToBoolean(tblSetting.IsItemShowPrice);
		LoginDetails.ItemNameAR = tblSetting.ItemNameAR;
		LoginDetails.ItemNameEN = tblSetting.ItemNameEN;
		LoginDetails.CommercialRegister = tblSetting.CommercialRegister;
		LoginDetails.CompanyBank = tblSetting.Bank;
		LoginDetails.Phone = tblSetting.Phone;
		LoginDetails.Fax = tblSetting.Fax;
		LoginDetails.Mobile = tblSetting.Mobile;
		LoginDetails.PrintComp = tblSetting.CompanyNamePrintFatora;
		LoginDetails.Web = tblSetting.Web;
		LoginDetails.Address = tblSetting.Address;
		LoginDetails.ItemShowPrice = tblSetting.ItemShowPrice;
		LoginDetails.BackupPath = tblSetting.PathBackup;
		LoginDetails.IsBackupWhenClose = Convert.ToBoolean(tblSetting.IsBackupwhenClose);
		LoginDetails.DiscountDecimal = Convert.ToBoolean(tblSetting.DiscountDecimal);
		LoginDetails.NoDiscountDecimal = Convert.ToBoolean(tblSetting.NoDiscountDecimal);
		LoginDetails.DiscountDecimalForCustomer = Convert.ToBoolean(tblSetting.DiscountDecimalForCustomer);
		LoginDetails.IsPrintCashier = Convert.ToBoolean(tblSetting.IsPrintCashier);
		LoginDetails.IsPrintOrder = Convert.ToBoolean(tblSetting.IsPrintOrder);
		LoginDetails.IsPrintReturnOrder = Convert.ToBoolean(tblSetting.IsPrintReturnOrder);
		LoginDetails.IsPharmacy = Convert.ToBoolean(tblSetting.IsPharmacy);
		LoginDetails.IsPrintItemCook = Convert.ToBoolean(tblSetting.IsPrintItemCook);
		LoginDetails.DiscountForRestaurant = Convert.ToDecimal(tblSetting.DiscountForRestaurant);
		LoginDetails.IsSecureCashier = Convert.ToBoolean(tblSetting.IsSecureCashier);
		LoginDetails.IsMizanWeight = Convert.ToBoolean(tblSetting.IsMizanWeight);
		LoginDetails.OrderStore = Convert.ToBoolean(tblSetting.OrderStore);
		LoginDetails.Hasm = Convert.ToBoolean(tblSetting.Hasm);
		LoginDetails.SellPrice2 = Convert.ToBoolean(user_Groups.SellPrice2);
		LoginDetails.SellPrice3 = Convert.ToBoolean(user_Groups.SellPrice3);
		LoginDetails.LastPrice = Convert.ToBoolean(user_Groups.LastPrice);
		LoginDetails.AvergPrice = Convert.ToBoolean(user_Groups.AvergPrice);
		LoginDetails.ProvePrice = Convert.ToBoolean(user_Groups.ProvePrice);
		LoginDetails.UserSellPrice = Convert.ToInt32(user_Groups.SelectSellPrice);
		LoginDetails.IsQuantityMinZero = Convert.ToBoolean(user_Groups.SelectQuantityPrice);
		LoginDetails.IsTobacc = Convert.ToBoolean(tblSetting.IsTobacc);
		LoginDetails.IsImage = Convert.ToBoolean(tblSetting.IsImage);
		LoginDetails.IsRestaurant = Convert.ToBoolean(tblSetting.IsRestaurant);
		LoginDetails.IsShowRoomInFormRestaurant = Convert.ToBoolean(tblSetting.IsShowRoomInFormRestaurant);
		LoginDetails.NamePrintFatora = tblSetting.CompanyNamePrintFatora;
		LoginDetails.DecimalNum = Convert.ToInt32(tblSetting.Currency_Dividing);
		LoginDetails.IsVat = Convert.ToBoolean(tblSetting.IsVat);
		LoginDetails.PerVat = Convert.ToDecimal(tblSetting.PerVat);
		LoginDetails.Gabr = Convert.ToBoolean(tblSetting.Gabr);
		LoginDetails.DecimalNumQty = Convert.ToInt32(tblSetting.Qty_Dividing);
		GetPrintFromXml();
	}

	public void LoginApp()
	{
		int Result = 0;
		Result = LGN.LOGIN(Convert.ToInt32(TxtName.SelectedValue), TxtPassword.Text);
		if (Result > 0)
		{
			Thread thread = new Thread((ThreadStart)delegate
			{
				BeginInvoke((Action)delegate
				{
					BindLogin(Result);
				});
			});
			thread.Start();
			LoginDetails.GroupID = Result;
			LoginDetails.UserID = Convert.ToInt32(TxtName.SelectedValue);
			LoginDetails.UserName = TxtName.Text;
			LoginDetails.BranchID = (LoginDetails.BranchID = Convert.ToInt32(TxtBranch.SelectedValue));
			LoginDetails.macAddress = GetMACAddress10();
			GetRemmember();
			BindTxt();
			GetDataFromXml();
			Class_WaitFormFunc class_WaitFormFunc = new Class_WaitFormFunc();
			class_WaitFormFunc.Show(this);
			Thread.Sleep(50);
			new FrmRestaurant().Show();
			class_WaitFormFunc.Close();
			Hide();
		}
		else
		{
			MessageBox.Show("لقد أدخلت اسم المستخدم أو كلمة المرور خاطئة برجاء ادخال البيانات صحيحة ثم حاول مرة أخري", "تسجيل الدخول", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			TxtPassword.Text = "";
			TxtPassword.Focus();
		}
	}

	private void BtnLogin_Click(object sender, EventArgs e)
	{
		LoginApp();
	}

	private string GetMacAddress()
	{
		string text = string.Empty;
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		foreach (NetworkInterface networkInterface in allNetworkInterfaces)
		{
			if (networkInterface.OperationalStatus == OperationalStatus.Up)
			{
				text += networkInterface.GetPhysicalAddress().ToString();
				break;
			}
		}
		return text;
	}

	public string GetMACAddress10()
	{
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		string text = string.Empty;
		NetworkInterface[] array = allNetworkInterfaces;
		foreach (NetworkInterface networkInterface in array)
		{
			if (text == string.Empty)
			{
				IPInterfaceProperties iPProperties = networkInterface.GetIPProperties();
				text = networkInterface.GetPhysicalAddress().ToString();
			}
		}
		return text;
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void CombName_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			LoginApp();
		}
	}

	private void GetDataFromXml()
	{
		try
		{
			FileStream fileStream = new FileStream(Application.StartupPath + "\\ERP.xml", FileMode.Open, FileAccess.Read);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SaveConnectionForXml>));
			List<SaveConnectionForXml> list = new List<SaveConnectionForXml>();
			list = (List<SaveConnectionForXml>)xmlSerializer.Deserialize(fileStream);
			SaveConnectionForXml saveConnectionForXml = list[0];
			LoginDetails.nameServer = saveConnectionForXml._comboServer;
			LoginDetails.databaseServer = saveConnectionForXml._comboDatabase;
			LoginDetails.userNameServer = Class_Encryption.DecryptString(saveConnectionForXml._TxtUser, "EF");
			LoginDetails.passServer = Class_Encryption.DecryptString(saveConnectionForXml._TxtPassword, "EF");
			LoginDetails.ConnString = "Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer;
			fileStream.Close();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void GetPrintFromXml()
	{
		try
		{
			FileStream fileStream = new FileStream(Application.StartupPath + "\\Print.xml", FileMode.Open, FileAccess.Read);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SavePrintForXml>));
			List<SavePrintForXml> list = new List<SavePrintForXml>();
			list = (List<SavePrintForXml>)xmlSerializer.Deserialize(fileStream);
			SavePrintForXml savePrintForXml = list[0];
			LoginDetails.DefualtPrint = savePrintForXml._PrintName;
			fileStream.Close();
		}
		catch (Exception)
		{
		}
	}

	public void BindTxt()
	{
		try
		{
			Stting.SelectUSR(Convert.ToInt32(TxtBranch.SelectedValue), out var itemNameAr, out var itemNameEN, out var itemVatNum, out var itemIsVat, out var itemtafqitID, out var itemStoreID);
			LoginDetails.CompanyNameAR = itemNameAr;
			LoginDetails.CompanyNameEN = itemNameEN;
			LoginDetails.VatNum = itemVatNum;
			LoginDetails.IsVat = itemIsVat;
			LoginDetails.TafqitID = itemtafqitID;
			LoginDetails.StoreSN = itemStoreID;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			LoginDetails.CompanyNameAR = "مايكرو سمارت";
			LoginDetails.CompanyNameEN = "EgySmart";
			LoginDetails.VatNum = "123";
			LoginDetails.IsVat = false;
			LoginDetails.TafqitID = 1;
		}
	}

	private void BtnClear_Click(object sender, EventArgs e)
	{
		TxtPassword.Text = "";
	}

	private void Btn0_Click(object sender, EventArgs e)
	{
		TxtPassword.Text += "0";
	}

	private void Btn1_Click(object sender, EventArgs e)
	{
		TxtPassword.Text += "1";
	}

	private void Btn2_Click(object sender, EventArgs e)
	{
		TxtPassword.Text += "2";
	}

	private void Btn3_Click(object sender, EventArgs e)
	{
		TxtPassword.Text += "3";
	}

	private void Btn4_Click(object sender, EventArgs e)
	{
		TxtPassword.Text += "4";
	}

	private void Btn5_Click(object sender, EventArgs e)
	{
		TxtPassword.Text += "5";
	}

	private void Btn6_Click(object sender, EventArgs e)
	{
		TxtPassword.Text += "6";
	}

	private void Btn7_Click(object sender, EventArgs e)
	{
		TxtPassword.Text += "7";
	}

	private void Btn8_Click(object sender, EventArgs e)
	{
		TxtPassword.Text += "8";
	}

	private void Btn9_Click(object sender, EventArgs e)
	{
		TxtPassword.Text += "9";
	}

	private void TxtBranch_SelectedValueChanged(object sender, EventArgs e)
	{
		try
		{
			int BID = Convert.ToInt32(TxtBranch.SelectedValue);
			TxtName.DataSource = (from x in USR.GetAllUsers()
				where x.BranchID == BID
				select x).ToList();
			TxtName.DisplayMember = "Name";
			TxtName.ValueMember = "ID";
			if (Convert.ToInt32(TxtName.SelectedValue) == 0)
			{
				TxtName.Text = "";
			}
		}
		catch (Exception)
		{
			int BID2 = 1;
			TxtName.DataSource = (from x in USR.GetAllGroups()
				where x.BranchID == BID2
				select x).ToList();
			TxtName.DisplayMember = "Name";
			TxtName.ValueMember = "ID";
		}
	}

	private void TxtName_KeyDown(object sender, KeyEventArgs e)
	{
		TxtPassword.Focus();
	}

	private void BtnDatabase_Click(object sender, EventArgs e)
	{
		new FrmSqlConnection().ShowDialog();
	}

	private void toggleSwitch1_Toggled(object sender, EventArgs e)
	{
		string ietfLanguageTag = Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;
		string text = ietfLanguageTag;
		if (!(text == "ar-EG"))
		{
			if (text == "en-US")
			{
				Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
				LoginDetails.Lang = "ar-EG";
			}
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
			LoginDetails.Lang = "en-US";
		}
		base.Controls.Clear();
		InitializeComponent();
		BindCBBranch();
		BindCBGroups();
		TxtBranch.Focus();
	}

	private void linkLabel1_Click(object sender, EventArgs e)
	{
		linkLabel1.LinkVisited = true;
		Process.Start("http://www.gts-sa.com");
	}

	private void GetRemmember()
	{
		foreach (TblRemember item in (IEnumerable<TblRemember>)db.TblRemembers)
		{
			if (item.DateTimeRem.Value.Date == DateTime.Now.Date)
			{
				LoginDetails.remembers.Add(item);
			}
		}
	}

	private void LblLang_Click(object sender, EventArgs e)
	{
		string ietfLanguageTag = Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;
		string text = ietfLanguageTag;
		if (!(text == "ar-EG"))
		{
			if (text == "en-US")
			{
				Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
				LoginDetails.Lang = "ar-EG";
				LblLang.Text = "AR";
			}
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
			LoginDetails.Lang = "en-US";
			LblLang.Text = "EN";
		}
		base.Controls.Clear();
		InitializeComponent();
		BindCBBranch();
		BindCBGroups();
		TxtBranch.Focus();
		if (LoginDetails.Lang == "en-US")
		{
			LblLang.Text = "AR";
		}
		else if (LoginDetails.Lang == "ar-EG")
		{
			LblLang.Text = "EN";
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.BtnLogin = new System.Windows.Forms.Button();
		this.TxtPassword = new System.Windows.Forms.TextBox();
		this.TxtName = new System.Windows.Forms.ComboBox();
		this.TxtBranch = new System.Windows.Forms.ComboBox();
		this.BtnExit = new System.Windows.Forms.Button();
		this.Btn1 = new System.Windows.Forms.Button();
		this.Btn2 = new System.Windows.Forms.Button();
		this.Btn3 = new System.Windows.Forms.Button();
		this.Btn4 = new System.Windows.Forms.Button();
		this.Btn5 = new System.Windows.Forms.Button();
		this.Btn6 = new System.Windows.Forms.Button();
		this.Btn7 = new System.Windows.Forms.Button();
		this.Btn8 = new System.Windows.Forms.Button();
		this.Btn9 = new System.Windows.Forms.Button();
		this.Btn0 = new System.Windows.Forms.Button();
		this.BtnDelete = new System.Windows.Forms.Button();
		this.LblLang = new System.Windows.Forms.Label();
		this.pictureBox6 = new System.Windows.Forms.PictureBox();
		this.pictureBox5 = new System.Windows.Forms.PictureBox();
		this.pictureBox4 = new System.Windows.Forms.PictureBox();
		this.pictureBox3 = new System.Windows.Forms.PictureBox();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.Navy;
		this.panel1.Controls.Add(this.LblLang);
		this.panel1.Controls.Add(this.pictureBox1);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.linkLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(864, 33);
		this.panel1.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25f);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(42, 5);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(94, 22);
		this.label1.TabIndex = 1;
		this.label1.Text = "تسجيل الدخول";
		this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Silver;
		this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.linkLabel1.AutoSize = true;
		this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
		this.linkLabel1.Font = new System.Drawing.Font("Calisto MT", 16.75f, System.Drawing.FontStyle.Bold);
		this.linkLabel1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
		this.linkLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.linkLabel1.LinkColor = System.Drawing.Color.White;
		this.linkLabel1.Location = new System.Drawing.Point(463, 5);
		this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(184, 26);
		this.linkLabel1.TabIndex = 93;
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Text = "www.gts-sa.com";
		this.linkLabel1.Click += new System.EventHandler(linkLabel1_Click);
		this.label2.Cursor = System.Windows.Forms.Cursors.Default;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
		this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(11, 35);
		this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label2.Name = "label2";
		this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label2.Size = new System.Drawing.Size(639, 41);
		this.label2.TabIndex = 65;
		this.label2.Text = "برنامج GTS لنقاط البيع والمخازن والمحاسبة ترحب بك الرجاء اختار الفرع\r\n ثم اختار اسم المستخدم وادخال كلمة المرور ثم دخول.";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 8.2f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
		this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(18, 87);
		this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(35, 13);
		this.label3.TabIndex = 92;
		this.label3.Text = "الفرع";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 8.2f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
		this.label8.ForeColor = System.Drawing.SystemColors.Highlight;
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(18, 173);
		this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(66, 13);
		this.label8.TabIndex = 91;
		this.label8.Text = "كلمة المرور";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 8.2f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
		this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(18, 131);
		this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(87, 13);
		this.label5.TabIndex = 90;
		this.label5.Text = "اسم المستخدم";
		this.BtnLogin.BackColor = System.Drawing.SystemColors.Highlight;
		this.BtnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.BtnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.BtnLogin.ForeColor = System.Drawing.Color.PaleGreen;
		this.BtnLogin.Location = new System.Drawing.Point(14, 219);
		this.BtnLogin.Name = "BtnLogin";
		this.BtnLogin.Size = new System.Drawing.Size(188, 27);
		this.BtnLogin.TabIndex = 93;
		this.BtnLogin.Text = "دخــــــول";
		this.BtnLogin.UseVisualStyleBackColor = false;
		this.BtnLogin.Click += new System.EventHandler(BtnLogin_Click);
		this.TxtPassword.Location = new System.Drawing.Point(14, 191);
		this.TxtPassword.Name = "TxtPassword";
		this.TxtPassword.PasswordChar = '*';
		this.TxtPassword.Size = new System.Drawing.Size(188, 20);
		this.TxtPassword.TabIndex = 94;
		this.TxtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPassword.UseSystemPasswordChar = true;
		this.TxtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtPassword_KeyDown);
		this.TxtName.FormattingEnabled = true;
		this.TxtName.Location = new System.Drawing.Point(14, 149);
		this.TxtName.Name = "TxtName";
		this.TxtName.Size = new System.Drawing.Size(188, 21);
		this.TxtName.TabIndex = 95;
		this.TxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtName_KeyDown);
		this.TxtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		this.TxtBranch.FormattingEnabled = true;
		this.TxtBranch.Location = new System.Drawing.Point(14, 105);
		this.TxtBranch.Name = "TxtBranch";
		this.TxtBranch.Size = new System.Drawing.Size(188, 21);
		this.TxtBranch.TabIndex = 95;
		this.TxtBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnExit.BackColor = System.Drawing.SystemColors.Highlight;
		this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.BtnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25f);
		this.BtnExit.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.BtnExit.Location = new System.Drawing.Point(14, 252);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(188, 23);
		this.BtnExit.TabIndex = 93;
		this.BtnExit.Text = "خـــــــــروج";
		this.BtnExit.UseVisualStyleBackColor = false;
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.Btn1.BackColor = System.Drawing.SystemColors.Highlight;
		this.Btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Btn1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25f, System.Drawing.FontStyle.Bold);
		this.Btn1.ForeColor = System.Drawing.Color.White;
		this.Btn1.Location = new System.Drawing.Point(353, 87);
		this.Btn1.Name = "Btn1";
		this.Btn1.Size = new System.Drawing.Size(60, 38);
		this.Btn1.TabIndex = 93;
		this.Btn1.Text = "1";
		this.Btn1.UseVisualStyleBackColor = false;
		this.Btn1.Click += new System.EventHandler(Btn1_Click);
		this.Btn2.BackColor = System.Drawing.SystemColors.Highlight;
		this.Btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Btn2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25f, System.Drawing.FontStyle.Bold);
		this.Btn2.ForeColor = System.Drawing.Color.White;
		this.Btn2.Location = new System.Drawing.Point(292, 88);
		this.Btn2.Name = "Btn2";
		this.Btn2.Size = new System.Drawing.Size(60, 38);
		this.Btn2.TabIndex = 93;
		this.Btn2.Text = "2";
		this.Btn2.UseVisualStyleBackColor = false;
		this.Btn2.Click += new System.EventHandler(Btn2_Click);
		this.Btn3.BackColor = System.Drawing.SystemColors.Highlight;
		this.Btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Btn3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25f, System.Drawing.FontStyle.Bold);
		this.Btn3.ForeColor = System.Drawing.Color.White;
		this.Btn3.Location = new System.Drawing.Point(231, 88);
		this.Btn3.Name = "Btn3";
		this.Btn3.Size = new System.Drawing.Size(60, 38);
		this.Btn3.TabIndex = 93;
		this.Btn3.Text = "3";
		this.Btn3.UseVisualStyleBackColor = false;
		this.Btn3.Click += new System.EventHandler(Btn3_Click);
		this.Btn4.BackColor = System.Drawing.SystemColors.Highlight;
		this.Btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Btn4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25f, System.Drawing.FontStyle.Bold);
		this.Btn4.ForeColor = System.Drawing.Color.White;
		this.Btn4.Location = new System.Drawing.Point(353, 131);
		this.Btn4.Name = "Btn4";
		this.Btn4.Size = new System.Drawing.Size(60, 38);
		this.Btn4.TabIndex = 93;
		this.Btn4.Text = "4";
		this.Btn4.UseVisualStyleBackColor = false;
		this.Btn4.Click += new System.EventHandler(Btn4_Click);
		this.Btn5.BackColor = System.Drawing.SystemColors.Highlight;
		this.Btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Btn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25f, System.Drawing.FontStyle.Bold);
		this.Btn5.ForeColor = System.Drawing.Color.White;
		this.Btn5.Location = new System.Drawing.Point(292, 132);
		this.Btn5.Name = "Btn5";
		this.Btn5.Size = new System.Drawing.Size(60, 38);
		this.Btn5.TabIndex = 93;
		this.Btn5.Text = "5";
		this.Btn5.UseVisualStyleBackColor = false;
		this.Btn5.Click += new System.EventHandler(Btn5_Click);
		this.Btn6.BackColor = System.Drawing.SystemColors.Highlight;
		this.Btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Btn6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25f, System.Drawing.FontStyle.Bold);
		this.Btn6.ForeColor = System.Drawing.Color.White;
		this.Btn6.Location = new System.Drawing.Point(231, 132);
		this.Btn6.Name = "Btn6";
		this.Btn6.Size = new System.Drawing.Size(60, 38);
		this.Btn6.TabIndex = 93;
		this.Btn6.Text = "6";
		this.Btn6.UseVisualStyleBackColor = false;
		this.Btn6.Click += new System.EventHandler(Btn6_Click);
		this.Btn7.BackColor = System.Drawing.SystemColors.Highlight;
		this.Btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Btn7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25f, System.Drawing.FontStyle.Bold);
		this.Btn7.ForeColor = System.Drawing.Color.White;
		this.Btn7.Location = new System.Drawing.Point(353, 176);
		this.Btn7.Name = "Btn7";
		this.Btn7.Size = new System.Drawing.Size(60, 38);
		this.Btn7.TabIndex = 93;
		this.Btn7.Text = "7";
		this.Btn7.UseVisualStyleBackColor = false;
		this.Btn7.Click += new System.EventHandler(Btn7_Click);
		this.Btn8.BackColor = System.Drawing.SystemColors.Highlight;
		this.Btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Btn8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25f, System.Drawing.FontStyle.Bold);
		this.Btn8.ForeColor = System.Drawing.Color.White;
		this.Btn8.Location = new System.Drawing.Point(292, 176);
		this.Btn8.Name = "Btn8";
		this.Btn8.Size = new System.Drawing.Size(60, 38);
		this.Btn8.TabIndex = 93;
		this.Btn8.Text = "8";
		this.Btn8.UseVisualStyleBackColor = false;
		this.Btn8.Click += new System.EventHandler(Btn8_Click);
		this.Btn9.BackColor = System.Drawing.SystemColors.Highlight;
		this.Btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Btn9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25f, System.Drawing.FontStyle.Bold);
		this.Btn9.ForeColor = System.Drawing.Color.White;
		this.Btn9.Location = new System.Drawing.Point(231, 176);
		this.Btn9.Name = "Btn9";
		this.Btn9.Size = new System.Drawing.Size(60, 38);
		this.Btn9.TabIndex = 93;
		this.Btn9.Text = "9";
		this.Btn9.UseVisualStyleBackColor = false;
		this.Btn9.Click += new System.EventHandler(Btn9_Click);
		this.Btn0.BackColor = System.Drawing.SystemColors.Highlight;
		this.Btn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Btn0.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25f, System.Drawing.FontStyle.Bold);
		this.Btn0.ForeColor = System.Drawing.Color.White;
		this.Btn0.Location = new System.Drawing.Point(353, 220);
		this.Btn0.Name = "Btn0";
		this.Btn0.Size = new System.Drawing.Size(60, 38);
		this.Btn0.TabIndex = 93;
		this.Btn0.Text = "0";
		this.Btn0.UseVisualStyleBackColor = false;
		this.Btn0.Click += new System.EventHandler(Btn0_Click);
		this.BtnDelete.BackColor = System.Drawing.SystemColors.Highlight;
		this.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.BtnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25f, System.Drawing.FontStyle.Italic);
		this.BtnDelete.ForeColor = System.Drawing.Color.White;
		this.BtnDelete.Location = new System.Drawing.Point(231, 220);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(121, 38);
		this.BtnDelete.TabIndex = 93;
		this.BtnDelete.Text = "مـســـــح";
		this.BtnDelete.UseVisualStyleBackColor = false;
		this.BtnDelete.Click += new System.EventHandler(BtnClear_Click);
		this.LblLang.AutoSize = true;
		this.LblLang.ForeColor = System.Drawing.Color.White;
		this.LblLang.Location = new System.Drawing.Point(152, 14);
		this.LblLang.Name = "LblLang";
		this.LblLang.Size = new System.Drawing.Size(22, 13);
		this.LblLang.TabIndex = 96;
		this.LblLang.Text = "EN";
		this.LblLang.Visible = false;
		this.pictureBox6.Image = GTSErpSystem.Properties.Resources.ore;
		this.pictureBox6.Location = new System.Drawing.Point(443, 87);
		this.pictureBox6.Name = "pictureBox6";
		this.pictureBox6.Size = new System.Drawing.Size(40, 263);
		this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox6.TabIndex = 96;
		this.pictureBox6.TabStop = false;
		this.pictureBox5.Image = GTSErpSystem.Properties.Resources.faceb;
		this.pictureBox5.Location = new System.Drawing.Point(489, 220);
		this.pictureBox5.Name = "pictureBox5";
		this.pictureBox5.Size = new System.Drawing.Size(237, 38);
		this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox5.TabIndex = 96;
		this.pictureBox5.TabStop = false;
		this.pictureBox4.Image = GTSErpSystem.Properties.Resources.faceb;
		this.pictureBox4.Location = new System.Drawing.Point(489, 176);
		this.pictureBox4.Name = "pictureBox4";
		this.pictureBox4.Size = new System.Drawing.Size(237, 38);
		this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox4.TabIndex = 96;
		this.pictureBox4.TabStop = false;
		this.pictureBox3.Image = GTSErpSystem.Properties.Resources.faceb;
		this.pictureBox3.Location = new System.Drawing.Point(489, 132);
		this.pictureBox3.Name = "pictureBox3";
		this.pictureBox3.Size = new System.Drawing.Size(237, 38);
		this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox3.TabIndex = 96;
		this.pictureBox3.TabStop = false;
		this.pictureBox2.Image = GTSErpSystem.Properties.Resources.faceb;
		this.pictureBox2.Location = new System.Drawing.Point(489, 88);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(237, 38);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox2.TabIndex = 96;
		this.pictureBox2.TabStop = false;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox1.Image = GTSErpSystem.Properties.Resources.language_32x32;
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(33, 33);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox1.TabIndex = 96;
		this.pictureBox1.TabStop = false;
		this.pictureBox1.Click += new System.EventHandler(LblLang_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(864, 362);
		base.Controls.Add(this.pictureBox6);
		base.Controls.Add(this.pictureBox5);
		base.Controls.Add(this.pictureBox4);
		base.Controls.Add(this.pictureBox3);
		base.Controls.Add(this.pictureBox2);
		base.Controls.Add(this.TxtBranch);
		base.Controls.Add(this.TxtName);
		base.Controls.Add(this.TxtPassword);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.Btn0);
		base.Controls.Add(this.Btn9);
		base.Controls.Add(this.Btn8);
		base.Controls.Add(this.Btn6);
		base.Controls.Add(this.Btn5);
		base.Controls.Add(this.Btn7);
		base.Controls.Add(this.Btn3);
		base.Controls.Add(this.Btn4);
		base.Controls.Add(this.Btn2);
		base.Controls.Add(this.Btn1);
		base.Controls.Add(this.BtnLogin);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FrmLogin";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


