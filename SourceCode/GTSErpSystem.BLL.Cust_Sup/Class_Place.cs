using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Cust_Sup;

public class Class_Place
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Account_Place> GetAll()
	{
		return db.Account_Place.ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Account_Place account_Place = db.Account_Place.Where((Account_Place x) => x.Name == Name).FirstOrDefault();
		if (account_Place == null)
		{
			int iD = Convert.ToInt32(db.Account_Place.Select((Account_Place x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Account_Place account_Place2 = new Account_Place();
			account_Place2.ID = iD;
			account_Place2.Name = Name;
			account_Place2.UserID_Add = LoginDetails.UserID;
			account_Place2.UserBranch_Add = LoginDetails.BranchID;
			account_Place2.UserMacAddress_Add = LoginDetails.macAddress;
			account_Place2.UserDate_Add = DateTime.Now;
			db.Account_Place.Add(account_Place2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Account_Place account_Place = db.Account_Place.Where((Account_Place x) => x.ID == ID).FirstOrDefault();
		Account_Place account_Place2 = db.Account_Place.Where((Account_Place x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (account_Place != null && account_Place2 == null)
		{
			account_Place.Name = Name;
			account_Place.UserID_Update = LoginDetails.UserID;
			account_Place.UserBranch_Update = LoginDetails.GroupID;
			account_Place.UserMacAddress_Update = LoginDetails.macAddress;
			account_Place.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Account_Place account_Place = db.Account_Place.Where((Account_Place x) => x.ID == Sn).FirstOrDefault();
		if (account_Place != null)
		{
			db.Account_Place.Attach(account_Place);
			db.Account_Place.Remove(account_Place);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetName(int Code)
	{
		return (from x in db.Account_Place
			where x.ID == Code
			select x.Name).FirstOrDefault();
	}
}


