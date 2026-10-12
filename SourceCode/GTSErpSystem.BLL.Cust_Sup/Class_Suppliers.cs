using System;
using System.Collections.Generic;
using System.Linq;
using GTSErpSystem.BLL.Main.Setting;

namespace GTSErpSystem.BLL.Cust_Sup;

internal class Class_Suppliers
{
	private GTSdbEntities db = new GTSdbEntities();

	private int AccId = 21010;

	private Class_DefualtAccount Stting = new Class_DefualtAccount();

	public List<Account_CustSup> GetAll()
	{
		return db.Account_CustSup.ToList();
	}

	public int Insert(out int id, out int Code, out int AccountNo, string Name, string VatNum, string Phone, string Fax, string Address, decimal CreditLimit, decimal AlarmLimit, bool IsSuppliers, bool IsCustomers, string Note, string BuildingNum, string Street, string District, string city, string Country, string PostalCode, string AdditionalNumber, string CommercialRecord, int BranchID, int AccFatherNum)
	{
		id = 0;
		Code = 0;
		AccountNo = 0;
		int result = 0;
		Account_CustSup account_CustSup = db.Account_CustSup.Where((Account_CustSup x) => x.CustSuppName == Name).FirstOrDefault();
		if (account_CustSup == null)
		{
			Select_AccountDefualt_Result select_AccountDefualt_Result = (from x in Stting.GetAccountDefualt(LoginDetails.BranchID)
				where x.ID == 7
				select x).FirstOrDefault();
			if (select_AccountDefualt_Result != null)
			{
				AccId = Convert.ToInt32(select_AccountDefualt_Result.AccountID);
			}
			string text = (from x in db.Account_Accounts
				where x.ID == AccId
				select x.Account_No).FirstOrDefault().ToString();
			int startRange = Convert.ToInt32(text + "01");
			int endRange = Convert.ToInt32(text + "99");
			int num = Convert.ToInt32((from x in db.Account_Accounts
				where x.Account_No > (int?)startRange && x.Account_No < (int?)endRange
				select x.Account_No).DefaultIfEmpty(startRange).Max() + 1);
			int num2 = Convert.ToInt32(db.Account_CustSup.Select((Account_CustSup x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			int num3 = Convert.ToInt32((from x in db.Account_CustSup
				where x.FrmCust == (bool?)false
				select x.CustSuppCode).DefaultIfEmpty(0).Max()) + 1;
			Account_CustSup account_CustSup2 = new Account_CustSup();
			id = num2;
			account_CustSup2.ID = num2;
			account_CustSup2.CustSuppCode = num3;
			Code = num3;
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
			account_CustSup2.PlaceID = 0;
			account_CustSup2.SalesManID = 0;
			account_CustSup2.CreditLimit = CreditLimit;
			account_CustSup2.AlarmLimit = AlarmLimit;
			account_CustSup2.IsSuppliers = IsSuppliers;
			account_CustSup2.IsCustomers = IsCustomers;
			account_CustSup2.Note = Note;
			account_CustSup2.FrmCust = false;
			account_CustSup2.BranchID = BranchID;
			account_CustSup2.UserID_Add = LoginDetails.UserID;
			account_CustSup2.UserBranch_Add = LoginDetails.BranchID;
			account_CustSup2.UserMacAddress_Add = LoginDetails.macAddress;
			account_CustSup2.UserDate_Add = DateTime.Now;
			db.Account_CustSup.Add(account_CustSup2);
			result = db.SaveChanges();
			InsertAccount(AccountNo, Name, Phone, BranchID, AccFatherNum);
		}
		return result;
	}

	public int Update(int ID, int AccountNum, string Name, string VatNum, string Phone, string Fax, string Address, decimal CreditLimit, decimal AlarmLimit, bool IsSuppliers, bool IsCustomers, string Note, string BuildingNum, string Street, string District, string city, string Country, string PostalCode, string AdditionalNumber, string CommercialRecord, int BranchID, int AccFatherNum)
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
			account_CustSup.CreditLimit = CreditLimit;
			account_CustSup.AlarmLimit = AlarmLimit;
			account_CustSup.IsSuppliers = IsSuppliers;
			account_CustSup.IsCustomers = IsCustomers;
			account_CustSup.Note = Note;
			account_CustSup.BranchID = BranchID;
			account_CustSup.UserID_Update = LoginDetails.UserID;
			account_CustSup.UserBranch_Update = LoginDetails.BranchID;
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
		Account_CustSup account_CustSup = db.Account_CustSup.Where((Account_CustSup x) => x.ID == Sn && x.FrmCust == (bool?)false).FirstOrDefault();
		CustSN = account_CustSup.ID;
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)AccountNum).FirstOrDefault();
		if (account_Accounts != null)
		{
			AccSN = account_Accounts.ID;
		}
		Account_DefualtAccount account_DefualtAccount = db.Account_DefualtAccount.Where((Account_DefualtAccount x) => x.Acc_supplierCash == (int?)AccSN).FirstOrDefault();
		Order_Purchases order_Purchases = db.Order_Purchases.Where((Order_Purchases x) => x.SupplierID == (int?)CustSN).FirstOrDefault();
		Order_PurchasesReturn order_PurchasesReturn = db.Order_PurchasesReturn.Where((Order_PurchasesReturn x) => x.SupplierID == (int?)CustSN).FirstOrDefault();
		Order_PurchasesOrder order_PurchasesOrder = db.Order_PurchasesOrder.Where((Order_PurchasesOrder x) => x.SupplierID == (int?)CustSN).FirstOrDefault();
		Tran_TranDetails tran_TranDetails = db.Tran_TranDetails.Where((Tran_TranDetails x) => x.Account_Sn == (int?)CustSN).FirstOrDefault();
		if (account_CustSup != null && account_DefualtAccount == null && order_Purchases == null && order_PurchasesReturn == null && order_PurchasesOrder == null)
		{
			db.Account_CustSup.Attach(account_CustSup);
			db.Account_CustSup.Remove(account_CustSup);
			result = db.SaveChanges();
		}
		DeleteAccount(AccountNum);
		return result;
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
		account_Accounts.Account_Type = 2;
		account_Accounts.Account_Nature = 1;
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
			account_Accounts.BranchID = BranchID;
			account_Accounts.Main_Account_No = AccFatherNum;
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

	public string GetName(int Code)
	{
		return (from x in db.Account_CustSup
			where x.ID == Code
			select x.CustSuppName).FirstOrDefault();
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

	public string GetFirstSuppliers()
	{
		string text = (from x in db.Account_CustSup
			where x.IsSuppliers == (bool?)true && x.BranchID == (int?)LoginDetails.BranchID
			orderby x.ID
			select x.CustSuppName).FirstOrDefault();
		return text ?? "No customer found";
	}

	public int GetFirstSuppliersId()
	{
		return (from x in db.Account_CustSup
			where x.IsSuppliers == (bool?)true && x.BranchID == (int?)LoginDetails.BranchID
			orderby x.ID
			select x.ID).FirstOrDefault();
	}
}


