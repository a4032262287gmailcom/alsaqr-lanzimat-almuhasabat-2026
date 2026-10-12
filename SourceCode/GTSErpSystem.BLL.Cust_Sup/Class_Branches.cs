using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Cust_Sup;

public class Class_Branches
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Account_Branch> GetAll()
	{
		return db.Account_Branch.ToList();
	}

	public int Insert(out int Code, out int AccountNo, string Name, string Phone, string Fax, string Address, string Note)
	{
		Code = 0;
		AccountNo = 0;
		int result = 0;
		AccountNo = Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)2101310 && x.Account_No < (int?)2101399
			select x.Account_No).DefaultIfEmpty(2101310).Max() + 1);
		Account_Branch account_Branch = db.Account_Branch.Where((Account_Branch x) => x.Name == Name).FirstOrDefault();
		if (account_Branch == null)
		{
			int num = Convert.ToInt32(db.Account_Branch.Select((Account_Branch x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Account_Branch account_Branch2 = new Account_Branch();
			account_Branch2.ID = num;
			account_Branch2.Name = Name;
			account_Branch2.Phone = Phone;
			account_Branch2.Fax = Fax;
			account_Branch2.Address = Address;
			account_Branch2.Note = Note;
			account_Branch2.AccountNo = AccountNo;
			account_Branch2.UserID_Add = LoginDetails.UserID;
			account_Branch2.UserBranch_Add = LoginDetails.BranchID;
			account_Branch2.UserMacAddress_Add = LoginDetails.macAddress;
			account_Branch2.UserDate_Add = DateTime.Now;
			db.Account_Branch.Add(account_Branch2);
			result = db.SaveChanges();
			Code = num;
		}
		InsertAccount(AccountNo, Name);
		return result;
	}

	public int Update(int CustSuppCode, string Name, string Phone, string Fax, string Address, string Note)
	{
		int num = 0;
		Account_Branch account_Branch = db.Account_Branch.Where((Account_Branch x) => x.ID == CustSuppCode).FirstOrDefault();
		if (account_Branch != null)
		{
			account_Branch.Name = Name;
			account_Branch.Phone = Phone;
			account_Branch.Fax = Fax;
			account_Branch.Address = Address;
			account_Branch.Note = Note;
			account_Branch.UserID_Update = LoginDetails.UserID;
			account_Branch.UserBranch_Update = LoginDetails.GroupID;
			account_Branch.UserMacAddress_Update = LoginDetails.macAddress;
			account_Branch.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			num = 1;
			int accountNum = Convert.ToInt32(account_Branch.AccountNo);
			UpdateAccount(accountNum, Name);
		}
		else
		{
			num = 0;
		}
		return num;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Account_Branch account_Branch = db.Account_Branch.Where((Account_Branch x) => x.ID == Sn).FirstOrDefault();
		if (account_Branch != null)
		{
			db.Account_Branch.Attach(account_Branch);
			db.Account_Branch.Remove(account_Branch);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetName(int Code)
	{
		return db.Account_Branch.Where((Account_Branch x) => x.ID == Code).FirstOrDefault().Name;
	}

	public void InsertAccount(int AccountNum, string Name)
	{
		int iD = Convert.ToInt32(db.Account_Accounts.Select((Account_Accounts x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
		Account_Accounts account_Accounts = new Account_Accounts();
		account_Accounts.ID = iD;
		account_Accounts.Account_No = AccountNum;
		account_Accounts.Suspended = 1;
		account_Accounts.Account_Level = 4;
		account_Accounts.Final_Account = 2;
		account_Accounts.Account_Type = 2;
		account_Accounts.Account_Nature = 3;
		account_Accounts.Main_Account_No = 162;
		account_Accounts.Account_Name = Name;
		account_Accounts.Priv_Debit = 0m;
		account_Accounts.Priv_Credit = 0m;
		db.Account_Accounts.Add(account_Accounts);
		db.SaveChanges();
	}

	public int UpdateAccount(int AccountNum, string Name)
	{
		int num = 0;
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)AccountNum).FirstOrDefault();
		if (account_Accounts != null)
		{
			account_Accounts.Account_Name = Name;
			account_Accounts.UserID_Update = LoginDetails.UserID;
			account_Accounts.UserBranch_Update = LoginDetails.GroupID;
			account_Accounts.UserMacAddress_Update = LoginDetails.macAddress;
			account_Accounts.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int GetAccount(int ID)
	{
		int CustAccount = Convert.ToInt32((from x in db.Account_Branch
			where x.ID == ID
			select x.AccountNo).FirstOrDefault());
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No == (int?)CustAccount
			select x.ID).FirstOrDefault());
	}
}


