using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Cust_Sup;

public class Class_Stores
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Account_Stores> GetAll()
	{
		return db.Account_Stores.ToList();
	}

	public List<Account_Stores> GetAllStores(int BranchID)
	{
		return db.Account_Stores.Where((Account_Stores x) => x.BranchID == (int?)BranchID).ToList();
	}

	public int Insert(string Name, int BranchID, string Phone, string Fax, string Address)
	{
		int result = 0;
		Account_Stores account_Stores = db.Account_Stores.Where((Account_Stores x) => x.Store_Name == Name).FirstOrDefault();
		if (account_Stores == null)
		{
			int iD = Convert.ToInt32(db.Account_Stores.Select((Account_Stores x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Account_Stores account_Stores2 = new Account_Stores();
			account_Stores2.ID = iD;
			account_Stores2.Store_Name = Name;
			account_Stores2.BranchID = BranchID;
			account_Stores2.Phone = Phone;
			account_Stores2.Fax = Fax;
			account_Stores2.Address = Address;
			account_Stores2.UserID_Add = LoginDetails.UserID;
			account_Stores2.UserBranch_Add = LoginDetails.BranchID;
			account_Stores2.UserMacAddress_Add = LoginDetails.macAddress;
			account_Stores2.UserDate_Add = DateTime.Now;
			db.Account_Stores.Add(account_Stores2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name, int BranchID, string Phone, string Fax, string Address)
	{
		int num = 0;
		Account_Stores account_Stores = db.Account_Stores.Where((Account_Stores x) => x.ID == ID).FirstOrDefault();
		if (account_Stores != null)
		{
			account_Stores.Store_Name = Name;
			account_Stores.BranchID = BranchID;
			account_Stores.Phone = Phone;
			account_Stores.Fax = Fax;
			account_Stores.Address = Address;
			account_Stores.UserID_Update = LoginDetails.UserID;
			account_Stores.UserBranch_Update = LoginDetails.GroupID;
			account_Stores.UserMacAddress_Update = LoginDetails.macAddress;
			account_Stores.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Account_Stores account_Stores = db.Account_Stores.Where((Account_Stores x) => x.ID == Sn).FirstOrDefault();
		ItemQuantity itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.StoreID == (int?)Sn).FirstOrDefault();
		if (account_Stores != null && itemQuantity == null)
		{
			db.Account_Stores.Attach(account_Stores);
			db.Account_Stores.Remove(account_Stores);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetName(int Code)
	{
		return db.Account_Stores.Where((Account_Stores x) => x.ID == Code).FirstOrDefault().Store_Name;
	}
}


