using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Orders;

public class Class_Halls
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Hall> GetAllHalls()
	{
		return db.Halls.ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Hall hall = db.Halls.Where((Hall x) => x.Name == Name).FirstOrDefault();
		if (hall == null)
		{
			int iD = Convert.ToInt32(db.Halls.Select((Hall x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Hall hall2 = new Hall();
			hall2.ID = iD;
			hall2.Name = Name;
			hall2.UserID_Add = LoginDetails.UserID;
			hall2.UserBranch_Add = LoginDetails.BranchID;
			hall2.UserMacAddress_Add = LoginDetails.macAddress;
			hall2.UserDate_Add = DateTime.Now;
			db.Halls.Add(hall2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Hall hall = db.Halls.Where((Hall x) => x.ID == ID).FirstOrDefault();
		Hall hall2 = db.Halls.Where((Hall x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (hall != null && hall2 == null)
		{
			hall.Name = Name;
			hall.UserID_Update = LoginDetails.UserID;
			hall.UserBranch_Update = LoginDetails.GroupID;
			hall.UserMacAddress_Update = LoginDetails.macAddress;
			hall.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Hall hall = db.Halls.Where((Hall x) => x.ID == Sn).FirstOrDefault();
		if (hall != null)
		{
			db.Halls.Attach(hall);
			db.Halls.Remove(hall);
			result = db.SaveChanges();
		}
		return result;
	}
}


