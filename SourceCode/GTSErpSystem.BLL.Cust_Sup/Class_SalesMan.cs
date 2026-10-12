using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Cust_Sup;

public class Class_SalesMan
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Account_Branch> GetBranches()
	{
		return db.Account_Branch.ToList();
	}

	public List<Account_SalesMan> GetAll()
	{
		return db.Account_SalesMan.Where((Account_SalesMan x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int Insert(string Name, decimal profit, bool ISProfitOrder, string Phone, int PlaceID)
	{
		int result = 0;
		Account_SalesMan account_SalesMan = db.Account_SalesMan.Where((Account_SalesMan x) => x.Name == Name && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (account_SalesMan == null)
		{
			int value = Convert.ToInt32((from x in db.Account_SalesMan
				where x.BranchID == (int?)LoginDetails.BranchID
				select x.ID).DefaultIfEmpty(0).Max()) + 1;
			Account_SalesMan account_SalesMan2 = new Account_SalesMan();
			account_SalesMan2.ID = value;
			account_SalesMan2.Name = Name;
			account_SalesMan2.profit = profit;
			account_SalesMan2.ISProfitOrder = ISProfitOrder;
			account_SalesMan2.Phone = Phone;
			account_SalesMan2.PlaceID = PlaceID;
			account_SalesMan2.BranchID = LoginDetails.BranchID;
			account_SalesMan2.UserID_Add = LoginDetails.UserID;
			account_SalesMan2.UserBranch_Add = LoginDetails.BranchID;
			account_SalesMan2.UserMacAddress_Add = LoginDetails.macAddress;
			account_SalesMan2.UserDate_Add = DateTime.Now;
			db.Account_SalesMan.Add(account_SalesMan2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name, decimal profit, bool ISProfitOrder, string Phone, int PlaceID)
	{
		int num = 0;
		Account_SalesMan account_SalesMan = db.Account_SalesMan.Where((Account_SalesMan x) => x.ID == (int?)ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Account_SalesMan account_SalesMan2 = db.Account_SalesMan.Where((Account_SalesMan x) => x.Name == Name && x.ID != (int?)ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (account_SalesMan != null && account_SalesMan2 == null)
		{
			account_SalesMan.Name = Name;
			account_SalesMan.profit = profit;
			account_SalesMan.ISProfitOrder = ISProfitOrder;
			account_SalesMan.Phone = Phone;
			account_SalesMan.PlaceID = PlaceID;
			account_SalesMan.UserID_Update = LoginDetails.UserID;
			account_SalesMan.UserBranch_Update = LoginDetails.GroupID;
			account_SalesMan.UserMacAddress_Update = LoginDetails.macAddress;
			account_SalesMan.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Account_SalesMan Check = db.Account_SalesMan.Where((Account_SalesMan x) => x.ID == (int?)Sn && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (Check != null && !db.Account_CustSup.Any((Account_CustSup s) => s.SalesManID == Check.ID))
		{
			db.Account_SalesMan.Attach(Check);
			db.Account_SalesMan.Remove(Check);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetName(int Code)
	{
		return (from x in db.Account_SalesMan
			where x.ID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID
			select x.Name).FirstOrDefault();
	}
}


