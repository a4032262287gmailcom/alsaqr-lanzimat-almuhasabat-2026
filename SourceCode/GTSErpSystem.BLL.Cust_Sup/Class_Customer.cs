using System;
using System.Collections.Generic;
using System.Linq;
using GTSErpSystem.BLL.Main.Setting;

namespace GTSErpSystem.BLL.Cust_Sup;

public class Class_Customer
{
	private GTSdbEntities db = new GTSdbEntities();

	private Class_DefualtAccount Stting = new Class_DefualtAccount();

	private int AccId = 11013;

	public List<Account_CustSup> GetAll()
	{
		return (from x in db.Account_CustSup
			orderby x.ID descending
			where x.BranchID == (int?)LoginDetails.BranchID || x.BranchID == (int?)0
			select x).ToList();
	}

	public List<Account_CustSup> GetAllSup()
	{
		return (from x in db.Account_CustSup
			orderby x.ID descending
			where x.BranchID == (int?)LoginDetails.BranchID || x.BranchID == (int?)0
			select x).ToList();
	}

	public List<Account_SalesMan> GetAllSales()
	{
		return (from x in db.Account_SalesMan
			orderby x.ID
			where x.BranchID == (int?)LoginDetails.BranchID
			select x).ToList();
	}

	public string GetSalesMan(int CusID)
	{
		string result = "";
		Account_CustSup Cust = db.Account_CustSup.Where((Account_CustSup x) => x.ID == CusID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (Cust != null)
		{
			Account_SalesMan account_SalesMan = db.Account_SalesMan.Where((Account_SalesMan x) => x.ID == (int?)Cust.ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
			if (account_SalesMan != null)
			{
				result = account_SalesMan.Name;
			}
		}
		return result;
	}

	public string GetSalesManByCusID(int CusID)
	{
		string result = "";
		Account_SalesMan account_SalesMan = db.Account_SalesMan.Where((Account_SalesMan x) => x.ID == (int?)CusID).FirstOrDefault();
		if (account_SalesMan != null)
		{
			result = account_SalesMan.Name;
		}
		return result;
	}

	public string GetFirstCustomer()
	{
		string text = (from x in db.Account_CustSup
			where x.IsCustomers == (bool?)true && x.BranchID == (int?)LoginDetails.BranchID
			orderby x.ID
			select x.CustSuppName).FirstOrDefault();
		return text ?? "No customer found";
	}

	public int GetFirstCustomerId()
	{
		return (from x in db.Account_CustSup
			where x.IsCustomers == (bool?)true && x.BranchID == (int?)LoginDetails.BranchID
			orderby x.ID
			select x.ID).FirstOrDefault();
	}

	public int Insert(out int id, out int Code, out int AccountNo, string Name, string VatNum, string Phone, string Fax, string Address, int PlaceID, int SalesManID, decimal CreditLimit, decimal AlarmLimit, bool IsSuppliers, bool IsCustomers, string Note, string BuildingNum, string Street, string District, string city, string Country, string PostalCode, string AdditionalNumber, string CommercialRecord, int BranchID, int AccFatherNum)
	{
		id = 0;
		Code = 0;
		AccountNo = 0;
		int result = 0;
		Account_CustSup account_CustSup = db.Account_CustSup.Where((Account_CustSup x) => x.CustSuppName == Name && x.FrmCust == (bool?)true).FirstOrDefault();
		if (account_CustSup == null)
		{
			Select_AccountDefualt_Result select_AccountDefualt_Result = (from x in Stting.GetAccountDefualt(LoginDetails.BranchID)
				where x.ID == 8
				select x).FirstOrDefault();
			if (select_AccountDefualt_Result != null)
			{
				AccId = Convert.ToInt32(select_AccountDefualt_Result.AccountID);
			}
			string text = (from x in db.Account_Accounts
				where x.ID == AccId
				select x.Account_No).FirstOrDefault().ToString();
			int startRange = Convert.ToInt32(text + "00");
			int endRange = Convert.ToInt32(text + "99");
			int num = Convert.ToInt32((from x in db.Account_Accounts
				where x.Account_No > (int?)startRange && x.Account_No < (int?)endRange
				select x.Account_No).DefaultIfEmpty(startRange).Max() + 1);
			int num2 = Convert.ToInt32(db.Account_CustSup.Select((Account_CustSup x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			int num3 = Convert.ToInt32((from x in db.Account_CustSup
				where x.FrmCust == (bool?)true
				select x.CustSuppCode).DefaultIfEmpty(0).Max()) + 1;
			Account_CustSup account_CustSup2 = new Account_CustSup();
			id = num2;
			account_CustSup2.ID = num2;
			Code = num3;
			account_CustSup2.CustSuppCode = num3;
			account_CustSup2.AccountNo = num;
			AccountNo = num;
			account_CustSup2.CustSuppName = Name;
			account_CustSup2.VatNum = VatNum;
			account_CustSup2.Phone = Phone;
			account_CustSup2.Fax = Fax;
			account_CustSup2.BuildingNum = BuildingNum;
			account_CustSup2.Street = Street;
			account_CustSup2.District = District;
			account_CustSup2.City = city;
			account_CustSup2.Country = Country;
			account_CustSup2.PostalCode = PostalCode;
			account_CustSup2.AdditionalNum = AdditionalNumber;
			account_CustSup2.CommercialRecord = CommercialRecord;
			account_CustSup2.Address = Address;
			account_CustSup2.PlaceID = PlaceID;
			account_CustSup2.SalesManID = SalesManID;
			account_CustSup2.CreditLimit = CreditLimit;
			account_CustSup2.AlarmLimit = AlarmLimit;
			account_CustSup2.IsSuppliers = IsSuppliers;
			account_CustSup2.IsCustomers = IsCustomers;
			account_CustSup2.Note = Note;
			account_CustSup2.FrmCust = true;
			account_CustSup2.BranchID = BranchID;
			account_CustSup2.UserID_Add = LoginDetails.UserID;
			account_CustSup2.UserBranch_Add = LoginDetails.BranchID;
			account_CustSup2.UserMacAddress_Add = LoginDetails.macAddress;
			account_CustSup2.UserDate_Add = DateTime.Now;
			account_CustSup2.FranchiseID = LoginDetails.Active_FranchiseID;
			db.Account_CustSup.Add(account_CustSup2);
			result = db.SaveChanges();
			InsertAccount(AccountNo, Name, Phone, BranchID, AccFatherNum);
		}
		return result;
	}

	public int Update(int ID, int AccountNum, string Name, string VatNum, string Phone, string Fax, string Address, int PlaceID, int SalesManID, decimal CreditLimit, decimal AlarmLimit, bool IsSuppliers, bool IsCustomers, string Note, string BuildingNum, string Street, string District, string city, string Country, string PostalCode, string AdditionalNumber, string CommercialRecord, int BranchID, int AccFatherNum)
	{
		int num = 0;
		Account_CustSup account_CustSup = db.Account_CustSup.Where((Account_CustSup x) => x.ID == ID).FirstOrDefault();
		if (account_CustSup != null)
		{
			account_CustSup.CustSuppName = Name;
			account_CustSup.VatNum = VatNum;
			account_CustSup.Phone = Phone;
			account_CustSup.Fax = Fax;
			account_CustSup.BuildingNum = BuildingNum;
			account_CustSup.Street = Street;
			account_CustSup.District = District;
			account_CustSup.City = city;
			account_CustSup.Country = Country;
			account_CustSup.PostalCode = PostalCode;
			account_CustSup.AdditionalNum = AdditionalNumber;
			account_CustSup.CommercialRecord = CommercialRecord;
			account_CustSup.Address = Address;
			account_CustSup.PlaceID = PlaceID;
			account_CustSup.SalesManID = SalesManID;
			account_CustSup.CreditLimit = CreditLimit;
			account_CustSup.AlarmLimit = AlarmLimit;
			account_CustSup.IsSuppliers = IsSuppliers;
			account_CustSup.IsCustomers = IsCustomers;
			account_CustSup.Note = Note;
			account_CustSup.BranchID = BranchID;
			account_CustSup.UserID_Update = LoginDetails.UserID;
			account_CustSup.UserBranch_Update = LoginDetails.GroupID;
			account_CustSup.UserMacAddress_Update = LoginDetails.macAddress;
			account_CustSup.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			num = 1;
		}
		else
		{
			num = 0;
		}
		UpdateAccount(AccountNum, Name, Phone, BranchID, AccFatherNum);
		return num;
	}

	public int Delete(int Sn, int AccountNum)
	{
		int result = 0;
		int AccSN = 0;
		int CustSN = 0;
		Account_CustSup account_CustSup = db.Account_CustSup.Where((Account_CustSup x) => x.ID == Sn).FirstOrDefault();
		CustSN = account_CustSup.ID;
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)AccountNum).FirstOrDefault();
		if (account_Accounts != null)
		{
			AccSN = account_Accounts.ID;
		}
		Account_DefualtAccount2 account_DefualtAccount = db.Account_DefualtAccount2.Where((Account_DefualtAccount2 x) => x.AccountID == (int?)AccSN).FirstOrDefault();
		Account_DefualtCustomer account_DefualtCustomer = db.Account_DefualtCustomer.Where((Account_DefualtCustomer x) => x.AccountID == (int?)CustSN).FirstOrDefault();
		Order_Orders order_Orders = db.Order_Orders.Where((Order_Orders x) => x.SupplierID == (int?)CustSN).FirstOrDefault();
		Order_OrderReturn order_OrderReturn = db.Order_OrderReturn.Where((Order_OrderReturn x) => x.SupplierID == (int?)CustSN).FirstOrDefault();
		Order_PriceOffer order_PriceOffer = db.Order_PriceOffer.Where((Order_PriceOffer x) => x.SupplierID == (int?)CustSN).FirstOrDefault();
		Order_CheckoutOrders order_CheckoutOrders = db.Order_CheckoutOrders.Where((Order_CheckoutOrders x) => x.SupplierID == (int?)CustSN).FirstOrDefault();
		Tran_TranDetails tran_TranDetails = db.Tran_TranDetails.Where((Tran_TranDetails x) => x.Account_Sn == (int?)CustSN).FirstOrDefault();
		if (account_CustSup != null && account_DefualtAccount == null && account_DefualtCustomer == null && order_Orders == null && order_OrderReturn == null && order_PriceOffer == null && order_CheckoutOrders == null)
		{
			db.Account_CustSup.Attach(account_CustSup);
			db.Account_CustSup.Remove(account_CustSup);
			result = db.SaveChanges();
			DeleteAccount(AccountNum);
		}
		return result;
	}

	public bool CheckIsFatrher(int AccountNum)
	{
		Account_Accounts account_Accounts = db.Account_Accounts.FirstOrDefault((Account_Accounts x) => x.Account_No == (int?)AccountNum);
		if (account_Accounts == null)
		{
			return false;
		}
		int id = account_Accounts.ID;
		return db.Account_Accounts.Any((Account_Accounts x) => x.Main_Account_No == (int?)id);
	}

	public string GetName(int Code)
	{
		return (from x in db.Account_CustSup
			where x.ID == Code
			select x.CustSuppName).FirstOrDefault();
	}

	public void InsertAccount(int AccountNum, string Name, string Phone, int BranchID, int AccFatherNum)
	{
		int iD = Convert.ToInt32(db.Account_Accounts.Select((Account_Accounts x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
		Account_Accounts account_Accounts = new Account_Accounts();
		account_Accounts.ID = iD;
		account_Accounts.Account_No = AccountNum;
		account_Accounts.Suspended = 1;
		account_Accounts.Account_Level = 4;
		account_Accounts.Final_Account = 2;
		account_Accounts.Account_Type = 1;
		account_Accounts.Account_Nature = 2;
		account_Accounts.Main_Account_No = AccFatherNum;
		account_Accounts.Account_Name = Name;
		account_Accounts.E_Account_Name = Name;
		account_Accounts.Priv_Debit = 0m;
		account_Accounts.Priv_Credit = 0m;
		account_Accounts.PhoneA = Phone;
		account_Accounts.BranchID = BranchID;
		db.Account_Accounts.Add(account_Accounts);
		db.SaveChanges();
	}

	public int UpdateAccount(int AccountNum, string Name, string Phone, int BranchID, int AccFatherNum)
	{
		int num = 0;
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)AccountNum).FirstOrDefault();
		if (account_Accounts != null)
		{
			account_Accounts.Account_Name = Name;
			account_Accounts.E_Account_Name = Name;
			account_Accounts.PhoneA = Phone;
			account_Accounts.Main_Account_No = AccFatherNum;
			account_Accounts.BranchID = BranchID;
			account_Accounts.UserID_Update = LoginDetails.UserID;
			account_Accounts.UserBranch_Update = LoginDetails.GroupID;
			account_Accounts.UserMacAddress_Update = LoginDetails.macAddress;
			account_Accounts.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int DeleteAccount(int Account_No)
	{
		int result = 0;
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)Account_No).FirstOrDefault();
		if (account_Accounts != null)
		{
			db.Account_Accounts.Attach(account_Accounts);
			db.Account_Accounts.Remove(account_Accounts);
			result = db.SaveChanges();
		}
		return result;
	}

	public int GetAccount(int ID)
	{
		int CustAccount = Convert.ToInt32((from x in db.Account_CustSup
			where x.ID == ID
			select x.AccountNo).FirstOrDefault());
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No == (int?)CustAccount
			select x.ID).FirstOrDefault());
	}

	public int GetAccountCust(int ID)
	{
		int CustAccount = Convert.ToInt32((from x in db.Account_CustSup
			where x.ID == ID
			select x.AccountNo).FirstOrDefault());
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No == (int?)CustAccount
			select x.ID).FirstOrDefault());
	}
}


