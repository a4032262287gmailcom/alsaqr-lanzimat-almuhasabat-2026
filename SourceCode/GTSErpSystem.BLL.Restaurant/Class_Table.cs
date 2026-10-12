using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Restaurant;

public class Class_Table
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Restaurant_Table> GetAllUsers()
	{
		return db.Restaurant_Table.Where((Restaurant_Table x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Restaurant_Table restaurant_Table = db.Restaurant_Table.Where((Restaurant_Table x) => x.Name == Name && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (restaurant_Table == null)
		{
			int iD = Convert.ToInt32((from x in db.Restaurant_Table
				where x.BranchID == (int?)LoginDetails.BranchID
				select x.ID).DefaultIfEmpty(0).Max()) + 1;
			Restaurant_Table restaurant_Table2 = new Restaurant_Table();
			restaurant_Table2.ID = iD;
			restaurant_Table2.Name = Name;
			restaurant_Table2.ISPause = false;
			restaurant_Table2.Money = 0m;
			restaurant_Table2.OrderID = 0;
			restaurant_Table2.Time1 = DateTime.Now;
			restaurant_Table2.BranchID = LoginDetails.BranchID;
			restaurant_Table2.UserID_Add = LoginDetails.UserID;
			restaurant_Table2.UserBranch_Add = LoginDetails.BranchID;
			restaurant_Table2.UserMacAddress_Add = LoginDetails.macAddress;
			restaurant_Table2.UserDate_Add = DateTime.Now;
			db.Restaurant_Table.Add(restaurant_Table2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Restaurant_Table restaurant_Table = db.Restaurant_Table.Where((Restaurant_Table x) => x.ID == ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Restaurant_Table restaurant_Table2 = db.Restaurant_Table.Where((Restaurant_Table x) => x.Name == Name && x.ID != ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (restaurant_Table != null && restaurant_Table2 == null)
		{
			restaurant_Table.Name = Name;
			restaurant_Table.Time1 = DateTime.Now;
			restaurant_Table.UserID_Update = LoginDetails.UserID;
			restaurant_Table.UserBranch_Update = LoginDetails.GroupID;
			restaurant_Table.UserMacAddress_Update = LoginDetails.macAddress;
			restaurant_Table.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Restaurant_Table restaurant_Table = db.Restaurant_Table.Where((Restaurant_Table x) => x.ID == Sn && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		decimal? money = restaurant_Table.Money;
		if (((money.GetValueOrDefault() == 0m) & money.HasValue) && restaurant_Table.OrderID == 0 && restaurant_Table.ISPause == false)
		{
			db.Restaurant_Table.Attach(restaurant_Table);
			db.Restaurant_Table.Remove(restaurant_Table);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetUnitName(int Code)
	{
		return (from x in db.Restaurant_Table
			where x.ID == Code && x.BranchID == (int?)LoginDetails.BranchID
			select x.Name).FirstOrDefault();
	}
}


