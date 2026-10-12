using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Restaurant;

internal class Class_Delivery
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Restaurant_DeliveryMan> GetAllUsers()
	{
		return db.Restaurant_DeliveryMan.Where((Restaurant_DeliveryMan x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Restaurant_DeliveryMan restaurant_DeliveryMan = db.Restaurant_DeliveryMan.Where((Restaurant_DeliveryMan x) => x.DeliveryName == Name && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (restaurant_DeliveryMan == null)
		{
			int iD = Convert.ToInt32((from x in db.Restaurant_DeliveryMan
				where x.BranchID == (int?)LoginDetails.BranchID
				select x.ID).DefaultIfEmpty(0).Max()) + 1;
			Restaurant_DeliveryMan restaurant_DeliveryMan2 = new Restaurant_DeliveryMan();
			restaurant_DeliveryMan2.ID = iD;
			restaurant_DeliveryMan2.DeliveryName = Name;
			restaurant_DeliveryMan2.BranchID = LoginDetails.BranchID;
			restaurant_DeliveryMan2.UserID_Add = LoginDetails.UserID;
			restaurant_DeliveryMan2.UserBranch_Add = LoginDetails.BranchID;
			restaurant_DeliveryMan2.UserMacAddress_Add = LoginDetails.macAddress;
			restaurant_DeliveryMan2.UserDate_Add = DateTime.Now;
			db.Restaurant_DeliveryMan.Add(restaurant_DeliveryMan2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Restaurant_DeliveryMan restaurant_DeliveryMan = db.Restaurant_DeliveryMan.Where((Restaurant_DeliveryMan x) => x.ID == ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Restaurant_DeliveryMan restaurant_DeliveryMan2 = db.Restaurant_DeliveryMan.Where((Restaurant_DeliveryMan x) => x.DeliveryName == Name && x.ID != ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (restaurant_DeliveryMan != null && restaurant_DeliveryMan2 == null)
		{
			restaurant_DeliveryMan.DeliveryName = Name;
			restaurant_DeliveryMan.UserID_Update = LoginDetails.UserID;
			restaurant_DeliveryMan.UserBranch_Update = LoginDetails.GroupID;
			restaurant_DeliveryMan.UserMacAddress_Update = LoginDetails.macAddress;
			restaurant_DeliveryMan.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Restaurant_DeliveryMan restaurant_DeliveryMan = db.Restaurant_DeliveryMan.Where((Restaurant_DeliveryMan x) => x.ID == Sn && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Restaurant_Delivery restaurant_Delivery = db.Restaurant_Delivery.Where((Restaurant_Delivery x) => x.DeliveryID == (int?)Sn && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (restaurant_DeliveryMan != null && restaurant_Delivery == null)
		{
			db.Restaurant_DeliveryMan.Attach(restaurant_DeliveryMan);
			db.Restaurant_DeliveryMan.Remove(restaurant_DeliveryMan);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetUnitName(int Code)
	{
		return (from x in db.Restaurant_DeliveryMan
			where x.ID == Code && x.BranchID == (int?)LoginDetails.BranchID
			select x.DeliveryName).FirstOrDefault();
	}
}


