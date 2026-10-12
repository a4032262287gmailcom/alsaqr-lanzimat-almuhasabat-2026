using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Restaurant;

public class Class_Room
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Restaurant_Room> GetAllUsers()
	{
		return db.Restaurant_Room.Where((Restaurant_Room x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Restaurant_Room restaurant_Room = db.Restaurant_Room.Where((Restaurant_Room x) => x.Name == Name && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (restaurant_Room == null)
		{
			int iD = Convert.ToInt32((from x in db.Restaurant_Room
				where x.BranchID == (int?)LoginDetails.BranchID
				select x.ID).DefaultIfEmpty(0).Max()) + 1;
			Restaurant_Room restaurant_Room2 = new Restaurant_Room();
			restaurant_Room2.ID = iD;
			restaurant_Room2.Name = Name;
			restaurant_Room2.ISPause = false;
			restaurant_Room2.Money = 0m;
			restaurant_Room2.OrderID = 0;
			restaurant_Room2.Time1 = DateTime.Now;
			restaurant_Room2.BranchID = LoginDetails.BranchID;
			restaurant_Room2.UserID_Add = LoginDetails.UserID;
			restaurant_Room2.UserBranch_Add = LoginDetails.BranchID;
			restaurant_Room2.UserMacAddress_Add = LoginDetails.macAddress;
			restaurant_Room2.UserDate_Add = DateTime.Now;
			db.Restaurant_Room.Add(restaurant_Room2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Restaurant_Room restaurant_Room = db.Restaurant_Room.Where((Restaurant_Room x) => x.ID == ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Restaurant_Room restaurant_Room2 = db.Restaurant_Room.Where((Restaurant_Room x) => x.Name == Name && x.ID != ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (restaurant_Room != null && restaurant_Room2 == null)
		{
			restaurant_Room.Name = Name;
			restaurant_Room.Time1 = DateTime.Now;
			restaurant_Room.UserID_Update = LoginDetails.UserID;
			restaurant_Room.UserBranch_Update = LoginDetails.GroupID;
			restaurant_Room.UserMacAddress_Update = LoginDetails.macAddress;
			restaurant_Room.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Restaurant_Room restaurant_Room = db.Restaurant_Room.Where((Restaurant_Room x) => x.ID == Sn && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		decimal? money = restaurant_Room.Money;
		if (((money.GetValueOrDefault() == 0m) & money.HasValue) && restaurant_Room.OrderID == 0 && restaurant_Room.ISPause == false)
		{
			db.Restaurant_Room.Attach(restaurant_Room);
			db.Restaurant_Room.Remove(restaurant_Room);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetUnitName(int Code)
	{
		return (from x in db.Restaurant_Room
			where x.ID == Code && x.BranchID == (int?)LoginDetails.BranchID
			select x.Name).FirstOrDefault();
	}
}


