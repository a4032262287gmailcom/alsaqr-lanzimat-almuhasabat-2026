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
using System.Xml.Linq;
using System.Xml.Serialization;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Main.Setting;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.FrmsHome;
using GTSErpSystem.Properties;
using MaterialSkin;
using MaterialSkin.Controls;

namespace GTSErpSystem;

public class FrmLogin3 : MaterialForm
{
	private Class_Login LGN = new Class_Login();

	private Class_Users USR = new Class_Users();

	private Class_Groups GRP = new Class_Groups();

	private Class_Setting Userx = new Class_Setting();

	private Class_Setting Stting = new Class_Setting();

	private Class_DefualtAccount DfltAccnt = new Class_DefualtAccount();

	private GTSdbEntities db = new GTSdbEntities();

	private IContainer components = null;

	private Label label1;

	private MaterialRaisedButton materialRaisedButton12;

	private Label label8;

	private Label label5;

	private MaterialRaisedButton BtnExit;

	private System.Windows.Forms.ComboBox TxtName;

	private System.Windows.Forms.ComboBox TxtBranch;

	private Label label2;

	private TextBox TxtPassword;

	private ToggleSwitch BtnLang;

	private MaterialRaisedButton BtnDatabase;

	private MaterialRaisedButton BtnClear;

	private MaterialRaisedButton Btn0;

	private MaterialRaisedButton Btn6;

	private MaterialRaisedButton Btn4;

	private MaterialRaisedButton Btn5;

	private MaterialRaisedButton Btn3;

	private MaterialRaisedButton Btn1;

	private MaterialRaisedButton Btn2;

	private MaterialRaisedButton Btn9;

	private MaterialRaisedButton Btn7;

	private MaterialRaisedButton Btn8;

	private LinkLabel linkLabel1;

	private Label LblLang;

	private MaterialRaisedButton BtnVirginApp;

	private Label LblVirgin;

	private System.Windows.Forms.ComboBox CBVirgin;

	public FrmLogin3()
	{
		string text = LoadLangFromXml();
		Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(text);
		LoginDetails.Lang = text;
		InitializeComponent();
		BindCBBranch();
		BindCBGroups();
		TxtBranch.Focus();
		MaterialSkinManager instance = MaterialSkinManager.Instance;
		instance.AddFormToManage(this);
		instance.Theme = MaterialSkinManager.Themes.LIGHT;
		LblVirgin.Visible = false;
		CBVirgin.Visible = false;
		BtnVirginApp.Visible = false;
		if (text == "en-US")
		{
			LblLang.Text = "AR";
		}
		else
		{
			LblLang.Text = "EN";
		}
	}

	private string LoadLangFromXml()
	{
		string text = Path.Combine(Application.StartupPath, "LangConfig.xml");
		if (File.Exists(text))
		{
			XDocument xDocument = XDocument.Load(text);
			return xDocument.Root.Element("Culture")?.Value ?? "en-US";
		}
		return "en-US";
	}

	private void BindCBGroups()
	{
		int BID = Convert.ToInt32(TxtBranch.SelectedValue);
		TxtName.DataSource = (from x in USR.GetAllUsers()
			where x.BranchID == BID && x.IsActive == true
			select x).ToList();
		TxtName.DisplayMember = "Name";
		TxtName.ValueMember = "ID";
		CBVirgin.DataSource = db.Virg_Virgins.Where((Virg_Virgins x) => x.GroupShow >= (int?)LoginDetails.GroupID && x.FranchiseShow == (bool?)true).ToList();
		CBVirgin.DisplayMember = "Name";
		CBVirgin.ValueMember = "ID";
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
		User_Login userById = USR.GetUserById(Convert.ToInt32(TxtName.SelectedValue));
		LoginDetails.MinTobaccoTax = (from x in Userx.GetAll()
			select x.MinTobaccoTax).FirstOrDefault().GetValueOrDefault();
		LoginDetails.StoreIDPurches = Convert.ToInt32(tblSetting.StoreIDPurches);
		LoginDetails.CloseYear = Convert.ToBoolean(tblSetting.CloseYear);
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
		LoginDetails.Acc_OrderReturnAgl = Convert.ToInt32((from x in accountDefualt
			where x.ID == 4 && x.BranchID == BrnchCode
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
		try
		{
			LoginDetails.Acc_TransferCash = Convert.ToInt32((from x in accountDefualt
				where x.ID == 30 && x.BranchID == BrnchCode
				select x.AccountID).FirstOrDefault());
			LoginDetails.Acc_TransferAjila = Convert.ToInt32((from x in accountDefualt
				where x.ID == 31 && x.BranchID == BrnchCode
				select x.AccountID).FirstOrDefault());
		}
		catch (Exception)
		{
		}
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
		LoginDetails.Acc_AdvanceMoney = Convert.ToInt32((from x in accountDefualt
			where x.ID == 38 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.Acc_AdvanceMoneRecipt = Convert.ToInt32((from x in accountDefualt
			where x.ID == 39 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.InventoryUnderTransfer = Convert.ToInt32((from x in accountDefualt
			where x.ID == 40 && x.BranchID == BrnchCode
			select x.AccountID).FirstOrDefault());
		LoginDetails.BuildingNum = tblSetting.BuildingNum;
		LoginDetails.Street = tblSetting.Street;
		LoginDetails.District = tblSetting.District;
		LoginDetails.City = tblSetting.City;
		LoginDetails.Country = tblSetting.Country;
		LoginDetails.PostalCode = tblSetting.PostalCode;
		LoginDetails.AdditionalNum = tblSetting.AdditionalNum;
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
		LoginDetails.Bouns = Convert.ToBoolean(tblSetting.ISBouns);
		LoginDetails.DisCode = Convert.ToBoolean(tblSetting.DisCode);
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
		LoginDetails.PerVat = Convert.ToDecimal(tblSetting.PerVat);
		LoginDetails.Gabr = Convert.ToBoolean(tblSetting.Gabr);
		LoginDetails.DecimalNumQty = Convert.ToInt32(tblSetting.Qty_Dividing);
		LoginDetails.FontSize = Convert.ToInt32(tblSetting.FontSize);
		if (!tblSetting.IsTimeInRestaurant.HasValue)
		{
			tblSetting.IsTimeInRestaurant = false;
		}
		LoginDetails.IsTimeInRestaurant = Convert.ToBoolean(tblSetting.IsTimeInRestaurant);
		if (!tblSetting.ItemTotalWithVat.HasValue)
		{
			tblSetting.ItemTotalWithVat = false;
		}
		LoginDetails.IsVat = Convert.ToBoolean(tblSetting.IsVat);
		LoginDetails.ItemTotalWithVat = Convert.ToBoolean(tblSetting.ItemTotalWithVat);
		LoginDetails.ImgShape = Convert.ToBoolean(tblSetting.ImageShape);
		LoginDetails.InstallAll = Convert.ToInt32(tblSetting.InstallationAll);
		LoginDetails.ConCloud = tblSetting.ConString;
		LoginDetails.SupplierPartyIdentificationID = tblSetting.SupplierPartyIdentificationID;
		LoginDetails.SupplierPartyIdentificationschemeID = tblSetting.SupplierPartyIdentificationschemeID;
		LoginDetails.CsIdForUser = Convert.ToBoolean(tblSetting.CsIdForUser);
		LoginDetails.Glasses = Convert.ToBoolean(tblSetting.Glasses);
		if (!LoginDetails.CsIdForUser)
		{
			LoginDetails.Crs = tblSetting.Crs;
			LoginDetails.PrivateKey = tblSetting.PrivateKey;
			LoginDetails.PublicKey = tblSetting.PublicKey;
			LoginDetails.SecretKey = tblSetting.SecretKey;
			LoginDetails.InvoiceHash = tblSetting.InvoiceHash;
			LoginDetails.ModeInvoice = tblSetting.ModeInvoice;
		}
		else
		{
			LoginDetails.Crs = userById.Crs;
			LoginDetails.PrivateKey = userById.PrivateKey;
			LoginDetails.PublicKey = userById.PublicKey;
			LoginDetails.SecretKey = userById.SecretKey;
			LoginDetails.InvoiceHash = userById.InvoiceHash;
			LoginDetails.ModeInvoice = tblSetting.ModeInvoice;
		}
		LoginDetails.SentXmlZakateNow = Convert.ToBoolean(userById.SentXmlZakateNow);
		LoginDetails.DisPerCashier = Convert.ToDecimal(tblSetting.DisPerCashier);
		LoginDetails.BondTax = Convert.ToBoolean(tblSetting.BondTax);
		LoginDetails.MizanRate = Convert.ToString(tblSetting.SplitMizan);
		LoginDetails.BasicContract = Convert.ToString(tblSetting.BasicContractTerms);
		LoginDetails.contractTerms = LoginDetails.BasicContract;
		LoginDetails.BarcodeResturant = Convert.ToBoolean(tblSetting.BarcodeResturant);
		LoginDetails.ShowFatora = Convert.ToBoolean(tblSetting.CkShowFatoraElctornc);
		LoginDetails.FromPhone = tblSetting.FromPhone;
		LoginDetails.AuthToken = tblSetting.AuthToken;
		LoginDetails.AccountSid = tblSetting.AccountSid;
		LoginDetails.ShowRoom = Convert.ToBoolean(tblSetting.ShowRoom);
		LoginDetails.AlLItemInRow = Convert.ToBoolean(tblSetting.AlItemInRow);
		LoginDetails.PerGrid = Convert.ToDecimal(tblSetting.GridWidth);
		GetPrintFromXml();
	}

	public void LoginApp()
	{
		int Result = 0;
		Result = LGN.LOGIN(Convert.ToInt32(TxtName.SelectedValue), TxtPassword.Text);
		if (Result > 0)
		{
			LoginDetails.GroupID = Result;
			LoginDetails.UserID = Convert.ToInt32(TxtName.SelectedValue);
			LoginDetails.UserName = TxtName.Text;
			LoginDetails.BranchID = Convert.ToInt32(TxtBranch.SelectedValue);
			LoginDetails.macAddress = GetMACAddress10();
			BindLogin(Result);
			int BrnchCode = Convert.ToInt32(TxtBranch.SelectedValue);
			TblSetting tblSetting = (from x in Stting.GetAll()
				where x.ID == BrnchCode
				select x).FirstOrDefault();
			LoginDetails.IsElectronicInvoice = Convert.ToBoolean(tblSetting.IsElectronicInvoice);
			GetRemmember();
			BindTxt();
			GetDataFromXml();
			try
			{
			    FrmNameLogin(LoginDetails.TypeVerg);
			    string databaseVersion = tblSetting.DatabaseVersion;
			    int num = Stting.AddLoginLog(LoginDetails.UserID, LoginDetails.macAddress, LoginDetails.VergDate, databaseVersion);
			    Hide();
			}
			catch (System.Exception ex)
			{
			    System.Windows.Forms.MessageBox.Show(
			        "تعذر فتح الشاشة الرئيسية: " + ex.ToString(),
			        "الصقر لأنظمة المحاسبة",
			        System.Windows.Forms.MessageBoxButtons.OK,
			        System.Windows.Forms.MessageBoxIcon.Error);
			}
		}
		else
		{
			MessageBox.Show("لقد أدخلت اسم المستخدم أو كلمة المرور خاطئة برجاء ادخال البيانات صحيحة ثم حاول مرة أخري", "تسجيل الدخول", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			TxtPassword.Text = "";
			TxtPassword.Focus();
		}
	}

	private void FrmNameLogin(string Type)
	{
	    FrmMainUnified main = new FrmMainUnified();
	    main.Show();
	    main.BringToFront();
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
			string xmlPath = Path.Combine(Application.StartupPath, "ERP.xml");
			if (File.Exists(xmlPath))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<SaveConnectionForXml>));
				List<SaveConnectionForXml> settings;
				using (FileStream stream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					settings = (List<SaveConnectionForXml>)serializer.Deserialize(stream);
				}

				if (settings == null || settings.Count == 0)
					throw new InvalidDataException("ملف ERP.xml لا يحتوي على إعدادات اتصال.");

				SaveConnectionForXml saved = settings[0];
				LoginDetails.nameServer = saved._comboServer;
				LoginDetails.databaseServer = saved._comboDatabase;
				LoginDetails.userNameServer = Class_Encryption.DecryptString(saved._TxtUser, "EF");
				LoginDetails.passServer = Class_Encryption.DecryptString(saved._TxtPassword, "EF");

				System.Data.SqlClient.SqlConnectionStringBuilder sqlBuilder =
					new System.Data.SqlClient.SqlConnectionStringBuilder
					{
						DataSource = LoginDetails.nameServer,
						InitialCatalog = LoginDetails.databaseServer,
						UserID = LoginDetails.userNameServer,
						Password = LoginDetails.passServer,
						PersistSecurityInfo = true,
						MultipleActiveResultSets = true
					};
				LoginDetails.ConnString = sqlBuilder.ConnectionString;
				return;
			}

			// ERP.xml is optional when the current executable configuration has
			// a valid Entity Framework connection string.
			System.Configuration.ConnectionStringSettings configured =
				System.Configuration.ConfigurationManager.ConnectionStrings["GTSdbEntities"];
			if (configured == null || string.IsNullOrWhiteSpace(configured.ConnectionString))
				throw new InvalidOperationException(
					"لم يتم العثور على إعداد اتصال GTSdbEntities في ملف إعدادات البرنامج.");

			System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder entityBuilder =
				new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(configured.ConnectionString);
			System.Data.SqlClient.SqlConnectionStringBuilder providerBuilder =
				new System.Data.SqlClient.SqlConnectionStringBuilder(entityBuilder.ProviderConnectionString);

			LoginDetails.nameServer = providerBuilder.DataSource;
			LoginDetails.databaseServer = providerBuilder.InitialCatalog;
			LoginDetails.userNameServer = providerBuilder.UserID;
			LoginDetails.passServer = providerBuilder.Password;
			LoginDetails.ConnString = providerBuilder.ConnectionString;
		}
		catch (Exception ex)
		{
			MessageBox.Show(
				"تعذر قراءة إعدادات الاتصال الحالية. تحقق من ERP.xml أو ملف إعدادات البرنامج.\r\n" + ex.Message,
				"مستكشف البيانات",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				MessageBoxOptions.RightAlign);
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
			LoginDetails.DefualtPrintA4 = savePrintForXml._PrintNameA4;
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
			Stting.SelectUSR(Convert.ToInt32(TxtBranch.SelectedValue), out var itemNameAr, out var itemNameEN, out var itemVatNum, out var _, out var itemtafqitID, out var itemStoreID);
			LoginDetails.CompanyNameAR = itemNameAr;
			LoginDetails.CompanyNameEN = itemNameEN;
			LoginDetails.VatNum = itemVatNum;
			LoginDetails.TafqitID = itemtafqitID;
			LoginDetails.StoreSN = itemStoreID;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			LoginDetails.CompanyNameAR = "جي تي اس";
			LoginDetails.CompanyNameEN = "GTS";
			LoginDetails.VatNum = "123";
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

	private void linkLabel1_Click(object sender, EventArgs e)
	{
		linkLabel1.LinkVisited = true;
		Process.Start("http://www.gts-sa.net");
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
				string text2 = "ar-EG";
				Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(text2);
				LoginDetails.Lang = text2;
				LblLang.Text = "AR";
				SaveLangToXml(text2);
			}
		}
		else
		{
			string text2 = "en-US";
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(text2);
			LoginDetails.Lang = text2;
			LblLang.Text = "EN";
			SaveLangToXml(text2);
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
		CBVirgin.Visible = LoginDetails.Lbl_CB_VirginShow;
	}

	private void SaveLangToXml(string culture)
	{
		string fileName = Path.Combine(Application.StartupPath, "LangConfig.xml");
		XDocument xDocument = new XDocument(new XElement("LangSettings", new XElement("Culture", culture)));
		xDocument.Save(fileName);
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
	}

	private void BtnVirginApp_Click(object sender, EventArgs e)
	{
		// ط§ظ„طھظپط¹ظٹظ„/ط§ظ„طھط±ط®ظٹطµ طھظ… ط¥ظٹظ‚ط§ظپظ‡ ظ„ظپطھط­ ط§ظ„ط¨ط±ظ†ط§ظ…ط¬ ظ…ط¨ط§ط´ط±ط© ط¹ظ„ظ‰ ط´ط§ط´ط© طھط³ط¬ظٹظ„ ط§ظ„ط¯ط®ظˆظ„.
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.FrmLogin3));
		this.label1 = new System.Windows.Forms.Label();
		this.materialRaisedButton12 = new MaterialSkin.Controls.MaterialRaisedButton();
		this.label8 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.BtnExit = new MaterialSkin.Controls.MaterialRaisedButton();
		this.TxtName = new System.Windows.Forms.ComboBox();
		this.TxtBranch = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtPassword = new System.Windows.Forms.TextBox();
		this.BtnLang = new DevExpress.XtraEditors.ToggleSwitch();
		this.BtnDatabase = new MaterialSkin.Controls.MaterialRaisedButton();
		this.BtnClear = new MaterialSkin.Controls.MaterialRaisedButton();
		this.Btn0 = new MaterialSkin.Controls.MaterialRaisedButton();
		this.Btn6 = new MaterialSkin.Controls.MaterialRaisedButton();
		this.Btn4 = new MaterialSkin.Controls.MaterialRaisedButton();
		this.Btn5 = new MaterialSkin.Controls.MaterialRaisedButton();
		this.Btn3 = new MaterialSkin.Controls.MaterialRaisedButton();
		this.Btn1 = new MaterialSkin.Controls.MaterialRaisedButton();
		this.Btn2 = new MaterialSkin.Controls.MaterialRaisedButton();
		this.Btn9 = new MaterialSkin.Controls.MaterialRaisedButton();
		this.Btn7 = new MaterialSkin.Controls.MaterialRaisedButton();
		this.Btn8 = new MaterialSkin.Controls.MaterialRaisedButton();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.LblLang = new System.Windows.Forms.Label();
		this.BtnVirginApp = new MaterialSkin.Controls.MaterialRaisedButton();
		this.LblVirgin = new System.Windows.Forms.Label();
		this.CBVirgin = new System.Windows.Forms.ComboBox();
		((System.ComponentModel.ISupportInitialize)this.BtnLang.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.label1, "label1");
		this.label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Name = "label1";
		resources.ApplyResources(this.materialRaisedButton12, "materialRaisedButton12");
		this.materialRaisedButton12.Depth = 0;
		this.materialRaisedButton12.MouseState = MaterialSkin.MouseState.HOVER;
		this.materialRaisedButton12.Name = "materialRaisedButton12";
		this.materialRaisedButton12.Primary = true;
		this.materialRaisedButton12.UseVisualStyleBackColor = true;
		this.materialRaisedButton12.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.label8, "label8");
		this.label8.ForeColor = System.Drawing.Color.Black;
		this.label8.Name = "label8";
		resources.ApplyResources(this.label5, "label5");
		this.label5.ForeColor = System.Drawing.Color.Black;
		this.label5.Name = "label5";
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Depth = 0;
		this.BtnExit.MouseState = MaterialSkin.MouseState.HOVER;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Primary = true;
		this.BtnExit.UseVisualStyleBackColor = true;
		this.BtnExit.Click += new System.EventHandler(BtnLogin_Click);
		resources.ApplyResources(this.TxtName, "TxtName");
		this.TxtName.FormattingEnabled = true;
		this.TxtName.Name = "TxtName";
		this.TxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtName_KeyDown);
		this.TxtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		resources.ApplyResources(this.TxtBranch, "TxtBranch");
		this.TxtBranch.FormattingEnabled = true;
		this.TxtBranch.Name = "TxtBranch";
		this.TxtBranch.SelectedValueChanged += new System.EventHandler(TxtBranch_SelectedValueChanged);
		this.TxtBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtBranch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		resources.ApplyResources(this.label2, "label2");
		this.label2.ForeColor = System.Drawing.Color.Black;
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtPassword, "TxtPassword");
		this.TxtPassword.Name = "TxtPassword";
		this.TxtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtPassword_KeyDown);
		resources.ApplyResources(this.BtnLang, "BtnLang");
		this.BtnLang.Name = "BtnLang";
		this.BtnLang.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnLang.Properties.Appearance.Font");
		this.BtnLang.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnLang.Properties.Appearance.Options.UseFont = true;
		this.BtnLang.Properties.Appearance.Options.UseForeColor = true;
		this.BtnLang.Properties.GlyphVerticalAlignment = (DevExpress.Utils.VertAlignment)resources.GetObject("BtnLang.Properties.GlyphVerticalAlignment");
		this.BtnLang.Properties.OffText = resources.GetString("BtnLang.Properties.OffText");
		this.BtnLang.Properties.OnText = resources.GetString("BtnLang.Properties.OnText");
		this.BtnLang.Toggled += new System.EventHandler(toggleSwitch1_Toggled);
		resources.ApplyResources(this.BtnDatabase, "BtnDatabase");
		this.BtnDatabase.Depth = 0;
		this.BtnDatabase.MouseState = MaterialSkin.MouseState.HOVER;
		this.BtnDatabase.Name = "BtnDatabase";
		this.BtnDatabase.Primary = true;
		this.BtnDatabase.UseVisualStyleBackColor = true;
		this.BtnDatabase.Click += new System.EventHandler(BtnDatabase_Click);
		resources.ApplyResources(this.BtnClear, "BtnClear");
		this.BtnClear.Depth = 0;
		this.BtnClear.MouseState = MaterialSkin.MouseState.HOVER;
		this.BtnClear.Name = "BtnClear";
		this.BtnClear.Primary = true;
		this.BtnClear.UseVisualStyleBackColor = true;
		this.BtnClear.Click += new System.EventHandler(BtnClear_Click);
		resources.ApplyResources(this.Btn0, "Btn0");
		this.Btn0.Depth = 0;
		this.Btn0.MouseState = MaterialSkin.MouseState.HOVER;
		this.Btn0.Name = "Btn0";
		this.Btn0.Primary = true;
		this.Btn0.UseVisualStyleBackColor = true;
		this.Btn0.Click += new System.EventHandler(Btn0_Click);
		resources.ApplyResources(this.Btn6, "Btn6");
		this.Btn6.Depth = 0;
		this.Btn6.MouseState = MaterialSkin.MouseState.HOVER;
		this.Btn6.Name = "Btn6";
		this.Btn6.Primary = true;
		this.Btn6.UseVisualStyleBackColor = true;
		this.Btn6.Click += new System.EventHandler(Btn6_Click);
		resources.ApplyResources(this.Btn4, "Btn4");
		this.Btn4.Depth = 0;
		this.Btn4.MouseState = MaterialSkin.MouseState.HOVER;
		this.Btn4.Name = "Btn4";
		this.Btn4.Primary = true;
		this.Btn4.UseVisualStyleBackColor = true;
		this.Btn4.Click += new System.EventHandler(Btn4_Click);
		resources.ApplyResources(this.Btn5, "Btn5");
		this.Btn5.Depth = 0;
		this.Btn5.MouseState = MaterialSkin.MouseState.HOVER;
		this.Btn5.Name = "Btn5";
		this.Btn5.Primary = true;
		this.Btn5.UseVisualStyleBackColor = true;
		this.Btn5.Click += new System.EventHandler(Btn5_Click);
		resources.ApplyResources(this.Btn3, "Btn3");
		this.Btn3.Depth = 0;
		this.Btn3.MouseState = MaterialSkin.MouseState.HOVER;
		this.Btn3.Name = "Btn3";
		this.Btn3.Primary = true;
		this.Btn3.UseVisualStyleBackColor = true;
		this.Btn3.Click += new System.EventHandler(Btn3_Click);
		resources.ApplyResources(this.Btn1, "Btn1");
		this.Btn1.Depth = 0;
		this.Btn1.MouseState = MaterialSkin.MouseState.HOVER;
		this.Btn1.Name = "Btn1";
		this.Btn1.Primary = true;
		this.Btn1.UseVisualStyleBackColor = true;
		this.Btn1.Click += new System.EventHandler(Btn1_Click);
		resources.ApplyResources(this.Btn2, "Btn2");
		this.Btn2.Depth = 0;
		this.Btn2.MouseState = MaterialSkin.MouseState.HOVER;
		this.Btn2.Name = "Btn2";
		this.Btn2.Primary = true;
		this.Btn2.UseVisualStyleBackColor = true;
		this.Btn2.Click += new System.EventHandler(Btn2_Click);
		resources.ApplyResources(this.Btn9, "Btn9");
		this.Btn9.Depth = 0;
		this.Btn9.MouseState = MaterialSkin.MouseState.HOVER;
		this.Btn9.Name = "Btn9";
		this.Btn9.Primary = true;
		this.Btn9.UseVisualStyleBackColor = true;
		this.Btn9.Click += new System.EventHandler(Btn9_Click);
		resources.ApplyResources(this.Btn7, "Btn7");
		this.Btn7.Depth = 0;
		this.Btn7.MouseState = MaterialSkin.MouseState.HOVER;
		this.Btn7.Name = "Btn7";
		this.Btn7.Primary = true;
		this.Btn7.UseVisualStyleBackColor = true;
		this.Btn7.Click += new System.EventHandler(Btn7_Click);
		resources.ApplyResources(this.Btn8, "Btn8");
		this.Btn8.Depth = 0;
		this.Btn8.MouseState = MaterialSkin.MouseState.HOVER;
		this.Btn8.Name = "Btn8";
		this.Btn8.Primary = true;
		this.Btn8.UseVisualStyleBackColor = true;
		this.Btn8.Click += new System.EventHandler(Btn8_Click);
		resources.ApplyResources(this.linkLabel1, "linkLabel1");
		this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Silver;
		this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
		this.linkLabel1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
		this.linkLabel1.LinkColor = System.Drawing.Color.White;
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.TabStop = true;
		this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
		this.linkLabel1.Click += new System.EventHandler(linkLabel1_Click);
		resources.ApplyResources(this.LblLang, "LblLang");
		this.LblLang.BackColor = System.Drawing.Color.Transparent;
		this.LblLang.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.LblLang.ForeColor = System.Drawing.Color.Black;
		this.LblLang.Name = "LblLang";
		this.LblLang.Click += new System.EventHandler(LblLang_Click);
		resources.ApplyResources(this.BtnVirginApp, "BtnVirginApp");
		this.BtnVirginApp.Depth = 0;
		this.BtnVirginApp.MouseState = MaterialSkin.MouseState.HOVER;
		this.BtnVirginApp.Name = "BtnVirginApp";
		this.BtnVirginApp.Primary = true;
		this.BtnVirginApp.UseVisualStyleBackColor = true;
		this.BtnVirginApp.Click += new System.EventHandler(BtnVirginApp_Click);
		resources.ApplyResources(this.LblVirgin, "LblVirgin");
		this.LblVirgin.ForeColor = System.Drawing.Color.Black;
		this.LblVirgin.Name = "LblVirgin";
		resources.ApplyResources(this.CBVirgin, "CBVirgin");
		this.CBVirgin.FormattingEnabled = true;
		this.CBVirgin.Name = "CBVirgin";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.LblVirgin);
		base.Controls.Add(this.CBVirgin);
		base.Controls.Add(this.BtnVirginApp);
		base.Controls.Add(this.LblLang);
		base.Controls.Add(this.linkLabel1);
		base.Controls.Add(this.BtnDatabase);
		base.Controls.Add(this.BtnClear);
		base.Controls.Add(this.Btn0);
		base.Controls.Add(this.Btn6);
		base.Controls.Add(this.Btn4);
		base.Controls.Add(this.Btn5);
		base.Controls.Add(this.Btn3);
		base.Controls.Add(this.Btn1);
		base.Controls.Add(this.Btn2);
		base.Controls.Add(this.Btn9);
		base.Controls.Add(this.Btn7);
		base.Controls.Add(this.Btn8);
		base.Controls.Add(this.BtnLang);
		base.Controls.Add(this.TxtPassword);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.TxtBranch);
		base.Controls.Add(this.TxtName);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.materialRaisedButton12);
		base.Controls.Add(this.label1);
		base.MaximizeBox = false;
		base.Name = "FrmLogin3";
		base.Sizable = false;
		((System.ComponentModel.ISupportInitialize)this.BtnLang.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


