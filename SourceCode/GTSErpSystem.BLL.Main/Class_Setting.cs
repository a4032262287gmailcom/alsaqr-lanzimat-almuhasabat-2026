using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GTSErpSystem.BLL.Main;

internal class Class_Setting
{
	private string folderPath = "";

	private SqlConnection Conn;

	private SqlCommand Command;

	private SqlDataReader Reader;

	private string sql = "";

	private string ConnectionString = "";

	private GTSdbEntities db = new GTSdbEntities();

	public List<TblSetting> GetAll()
	{
		return db.TblSettings.ToList();
	}

	public List<Items_ContentAlignment_ForCashier> GetItems_ContentAlignment()
	{
		return db.Items_ContentAlignment_ForCashier.ToList();
	}

	public List<Tafqit> GetAllTafqit()
	{
		return db.Tafqits.ToList();
	}

	public List<Account_Stores> GetAllStore()
	{
		return db.Account_Stores.ToList();
	}

	public int updatedemo(string Date2, string Date3, string isdemo)
	{
		int result = 0;
		TblSetting tblSetting = db.TblSettings.Where((TblSetting x) => x.ID == LoginDetails.BranchID).FirstOrDefault();
		if (tblSetting != null)
		{
			tblSetting.DateDemo = Date2;
			tblSetting.DateDemoStart = Date3;
			tblSetting.ISDemo = isdemo;
			result = db.SaveChanges();
		}
		return result;
	}

	public int UpdateItem(string itemNameAr, string itemNameEN, string itemNamePrintFatora, string itemVatNum, bool itemIsVat, int itemtafqitID, int DecimalNum, int DecimalNumQty, int itemStoreIDPurches, int itemStoreID, int ItemStoreWait, bool IsPrintSecondCasher, bool IsPrintSecondFatoraCasheir, bool IsTakeDateApp, int GroupSize, string GroupColor, string GroupFontColor, int ItemSize, string ItemColor, string ItemFontColor, bool IsItemNameAR, bool IsItemNameEN, bool IsItemShowPrice, string ItemNameAR, string ItemNameEN, string ItemShowPrice, bool IsRestaurant, string PathBackup, bool IsBackupwhenClose, bool IsTobacc, bool IsImage, string CommercialRegister, string Bank, string Phone, string Fax, string Mobile, string Web, string Address, string SecondPrintDefualt, bool IsShowRoomInFormRestaurant, bool IsPrintCashier, bool IsPrintOrder, bool IsPrintReturnOrder, bool DiscountDecimal, bool NoDiscountDecimal, bool DiscountDecimalForCustomer, bool IsPrintItemCook, decimal DiscountForRestaurant, bool IsSecureCashier, string TxtPassWordSupervisor, string BuildingNum, string Street, string District, string city, string Country, string PostalCode, string AdditionalNumber, decimal PerVat, bool OrderStore, bool Hasm, bool Gabr, bool IsMezan, bool IsTimeInRestaurant, bool ItemTotalWithVat, decimal MinTobaccoTax, decimal FontSize, bool Bouns, bool ImageShape, bool DisCode, int InstallAll, string Scheme, string SchemeNum, bool ISElctornc, bool Glasses, decimal DisPerCashier, bool BondTax, string MizanRate, bool CKBarRest, bool ShowFatora, string PhoneSent, string Token, string AccountSid, bool ShowRoom, bool AlItemInRow, decimal PerGrid)
	{
		int result = 0;
		TblSetting tblSetting = db.TblSettings.Where((TblSetting x) => x.ID == LoginDetails.BranchID).FirstOrDefault();
		if (tblSetting != null)
		{
			tblSetting.CompanyNameAr = itemNameAr;
			tblSetting.CompanyNameEn = itemNameEN;
			tblSetting.VatNum = itemVatNum;
			tblSetting.IsVat = itemIsVat;
			tblSetting.Tafqit_ID = itemtafqitID;
			tblSetting.StoreIDPurches = itemStoreIDPurches;
			tblSetting.StoreID = itemStoreID;
			tblSetting.StoreIDWaiying = ItemStoreWait;
			tblSetting.IsPrintSecondCasheir = IsPrintSecondCasher;
			tblSetting.IsPrintSecondFatoraCasheir = IsPrintSecondFatoraCasheir;
			tblSetting.IsPrintItemCook = IsPrintItemCook;
			tblSetting.IsTakeDateApp = IsTakeDateApp;
			tblSetting.GroupSize = GroupSize;
			tblSetting.GroupColor = GroupColor;
			tblSetting.GroupFontColor = GroupFontColor;
			tblSetting.ItemSize = ItemSize;
			tblSetting.ItemColor = ItemColor;
			tblSetting.ItemFontColor = ItemFontColor;
			tblSetting.IsItemNameAR = IsItemNameAR;
			tblSetting.IsItemNameEN = IsItemNameEN;
			tblSetting.IsItemShowPrice = IsItemShowPrice;
			tblSetting.ItemNameAR = ItemNameAR;
			tblSetting.ItemNameEN = ItemNameEN;
			tblSetting.ItemShowPrice = ItemShowPrice;
			tblSetting.IsRestaurant = IsRestaurant;
			tblSetting.PathBackup = PathBackup;
			tblSetting.IsBackupwhenClose = IsBackupwhenClose;
			tblSetting.IsTobacc = IsTobacc;
			tblSetting.IsImage = IsImage;
			tblSetting.Hasm = Hasm;
			tblSetting.Gabr = Gabr;
			tblSetting.IsMizanWeight = IsMezan;
			tblSetting.CommercialRegister = CommercialRegister;
			tblSetting.Bank = Bank;
			tblSetting.Phone = Phone;
			tblSetting.Fax = Fax;
			tblSetting.Mobile = Mobile;
			tblSetting.Web = Web;
			tblSetting.Address = Address;
			tblSetting.SecondPrintDefualt = SecondPrintDefualt;
			tblSetting.IsShowRoomInFormRestaurant = IsShowRoomInFormRestaurant;
			tblSetting.Currency_Dividing = DecimalNum;
			tblSetting.Qty_Dividing = DecimalNumQty;
			tblSetting.CompanyNamePrintFatora = itemNamePrintFatora;
			tblSetting.BuildingNum = BuildingNum;
			tblSetting.Street = Street;
			tblSetting.District = District;
			tblSetting.City = city;
			tblSetting.Country = Country;
			tblSetting.PostalCode = PostalCode;
			tblSetting.AdditionalNum = AdditionalNumber;
			tblSetting.PerVat = PerVat;
			tblSetting.OrderStore = OrderStore;
			tblSetting.IsPrintCashier = IsPrintCashier;
			tblSetting.IsPrintOrder = IsPrintOrder;
			tblSetting.IsPrintReturnOrder = IsPrintReturnOrder;
			tblSetting.DiscountDecimal = DiscountDecimal;
			tblSetting.NoDiscountDecimal = NoDiscountDecimal;
			tblSetting.DiscountDecimalForCustomer = DiscountDecimalForCustomer;
			tblSetting.DiscountForRestaurant = DiscountForRestaurant;
			tblSetting.IsSecureCashier = IsSecureCashier;
			tblSetting.PassWordSupervisor = TxtPassWordSupervisor;
			tblSetting.IsTimeInRestaurant = IsTimeInRestaurant;
			tblSetting.ItemTotalWithVat = ItemTotalWithVat;
			tblSetting.MinTobaccoTax = MinTobaccoTax;
			tblSetting.FontSize = FontSize;
			tblSetting.ISBouns = Bouns;
			tblSetting.ImageShape = ImageShape;
			tblSetting.DisCode = DisCode;
			tblSetting.InstallationAll = InstallAll;
			tblSetting.SupplierPartyIdentificationID = SchemeNum;
			tblSetting.SupplierPartyIdentificationschemeID = Scheme;
			tblSetting.IsElectronicInvoice = ISElctornc;
			tblSetting.Glasses = Glasses;
			tblSetting.DisPerCashier = DisPerCashier;
			tblSetting.BondTax = BondTax;
			tblSetting.SplitMizan = MizanRate;
			tblSetting.BarcodeResturant = CKBarRest;
			tblSetting.CkShowFatoraElctornc = ShowFatora;
			tblSetting.FromPhone = PhoneSent;
			tblSetting.AuthToken = Token;
			tblSetting.AccountSid = AccountSid;
			tblSetting.ShowRoom = ShowRoom;
			tblSetting.AlItemInRow = AlItemInRow;
			tblSetting.GridWidth = PerGrid;
			result = db.SaveChanges();
		}
		return result;
	}

	public int UpdateItemContract(string BasicContract)
	{
		int result = 0;
		TblSetting tblSetting = db.TblSettings.Where((TblSetting x) => x.ID == LoginDetails.BranchID).FirstOrDefault();
		if (tblSetting != null)
		{
			tblSetting.BasicContractTerms = BasicContract;
			result = db.SaveChanges();
		}
		return result;
	}

	public int UpdateToElctorncInvoic(string Crs, string PrivateKey, string PublicKey, string SecretKey, bool CsIdForUser, string Mode)
	{
		int result = 0;
		TblSetting tblSetting = db.TblSettings.Where((TblSetting x) => x.ID == LoginDetails.BranchID).FirstOrDefault();
		if (tblSetting != null)
		{
			tblSetting.Crs = Crs;
			tblSetting.PrivateKey = PrivateKey;
			tblSetting.PublicKey = PublicKey;
			tblSetting.SecretKey = SecretKey;
			tblSetting.CsIdForUser = CsIdForUser;
			tblSetting.ModeInvoice = Mode;
			result = db.SaveChanges();
		}
		return result;
	}

	public int UpdateToElctorncInvoicUser()
	{
		int result = 0;
		TblSetting tblSetting = db.TblSettings.Where((TblSetting x) => x.ID == LoginDetails.BranchID).FirstOrDefault();
		if (tblSetting != null)
		{
			tblSetting.CsIdForUser = true;
			result = db.SaveChanges();
		}
		return result;
	}

	public void SelectUSR(int IDCode, out string itemNameAr, out string itemNameEN, out string itemVatNum, out bool itemIsVat, out int itemtafqitID, out int itemStoreID)
	{
		itemNameAr = "مايكروسمارت";
		itemNameEN = "MicroSmart";
		itemVatNum = "123";
		itemIsVat = false;
		itemtafqitID = 1;
		itemStoreID = 1;
		IQueryable<TblSetting> queryable = db.TblSettings.Where((TblSetting d) => d.ID == IDCode);
		foreach (TblSetting item in queryable)
		{
			itemNameAr = item.CompanyNameAr;
			itemNameEN = item.CompanyNameEn;
			itemVatNum = item.VatNum;
			itemIsVat = Convert.ToBoolean(item.IsVat);
			itemtafqitID = Convert.ToInt32(item.Tafqit_ID);
			itemStoreID = Convert.ToInt32(item.StoreID);
		}
	}

	public int UpdatePrinterCashier(int ID, string Name)
	{
		int result = 0;
		Printer printer = db.Printers.Where((Printer x) => x.ID == ID).FirstOrDefault();
		if (printer != null)
		{
			printer.Name = Name;
			db.SaveChanges();
			result = 1;
		}
		return result;
	}

	public string SelectPrint(int ID)
	{
		return db.Printers.Where((Printer x) => x.ID == ID).FirstOrDefault().Name;
	}

	public int TakeBackup()
	{
		int num = 0;
		try
		{
			string nameServer = LoginDetails.nameServer;
			string databaseServer = LoginDetails.databaseServer;
			string userNameServer = LoginDetails.userNameServer;
			string passServer = LoginDetails.passServer;
			string backupPath = LoginDetails.BackupPath;
			DateTime now = DateTime.Now;
			string connectionString = "Data Source = " + nameServer + ";User Id=" + userNameServer + ";Password=" + passServer;
			Conn = new SqlConnection(connectionString);
			Conn.Open();
			sql = "BACKUP DATABASE [" + databaseServer + "] TO DISK = '" + backupPath + "\\" + databaseServer + "_" + now.Day + "-" + now.Month + "-" + now.Year + "-" + now.Hour + ".bak'";
			Command = new SqlCommand(sql, Conn);
			Command.CommandType = CommandType.Text;
			Command.CommandTimeout = 900000;
			Command.ExecuteNonQuery();
			Conn.Close();
			return 1;
		}
		catch (Exception)
		{
			return 0;
		}
	}

	public void RebuildCGEAllIndexes()
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		gTSdbEntities.RebuildCGEAllIndexes(LoginDetails.databaseServer);
	}

	public int InsertRem(DateTime DatetImeRem, string Remmember)
	{
		int num = 0;
		TblRemember entity = new TblRemember
		{
			DateTimeRem = DatetImeRem,
			NameRem = Remmember
		};
		db.TblRemembers.Add(entity);
		return db.SaveChanges();
	}

	public int UpdateRem(int id, DateTime DatetImeRem, string Remmember)
	{
		int num = 0;
		TblRemember tblRemember = db.TblRemembers.Find(id);
		if (tblRemember != null)
		{
			tblRemember.DateTimeRem = DatetImeRem;
			tblRemember.NameRem = Remmember;
		}
		return db.SaveChanges();
	}

	public int DeleteRem(int id)
	{
		int result = 0;
		TblRemember tblRemember = db.TblRemembers.Find(id);
		if (tblRemember != null)
		{
			db.TblRemembers.Remove(tblRemember);
			result = db.SaveChanges();
		}
		return result;
	}

	public int AddLoginLog(int userId, string macAddress, string appVersion, string databaseVersion)
	{
		int num = 0;
		LoginLog entity = new LoginLog
		{
			UserID = userId,
			UserLogsDate = DateTime.Now,
			MacAddressLogs = macAddress,
			AppVersion = appVersion,
			DatabaseVersion = databaseVersion
		};
		db.LoginLogs.Add(entity);
		return db.SaveChanges();
	}
}


