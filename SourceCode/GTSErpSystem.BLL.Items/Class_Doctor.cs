using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Items;

internal class Class_Doctor
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Item_Doctor> GetAllUsers()
	{
		return db.Item_Doctor.ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Item_Doctor item_Doctor = db.Item_Doctor.Where((Item_Doctor x) => x.Name == Name).FirstOrDefault();
		if (item_Doctor == null)
		{
			int iD = Convert.ToInt32(db.Item_Doctor.Select((Item_Doctor x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Item_Doctor item_Doctor2 = new Item_Doctor();
			item_Doctor2.ID = iD;
			item_Doctor2.Name = Name;
			item_Doctor2.UserID_Add = LoginDetails.UserID;
			item_Doctor2.UserBranch_Add = LoginDetails.BranchID;
			item_Doctor2.UserMacAddress_Add = LoginDetails.macAddress;
			item_Doctor2.UserDate_Add = DateTime.Now;
			db.Item_Doctor.Add(item_Doctor2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Item_Doctor item_Doctor = db.Item_Doctor.Where((Item_Doctor x) => x.ID == ID).FirstOrDefault();
		Item_Doctor item_Doctor2 = db.Item_Doctor.Where((Item_Doctor x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (item_Doctor != null && item_Doctor2 == null)
		{
			item_Doctor.Name = Name;
			item_Doctor.UserID_Update = LoginDetails.UserID;
			item_Doctor.UserBranch_Update = LoginDetails.GroupID;
			item_Doctor.UserMacAddress_Update = LoginDetails.macAddress;
			item_Doctor.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Item_Doctor item_Doctor = db.Item_Doctor.Where((Item_Doctor x) => x.ID == Sn).FirstOrDefault();
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.UnitSmall == (int?)Sn || x.UnitMedium == (int?)Sn || x.UnitLarge == (int?)Sn).FirstOrDefault();
		if (item_Doctor != null && item_Items == null)
		{
			db.Item_Doctor.Attach(item_Doctor);
			db.Item_Doctor.Remove(item_Doctor);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetUnitName(int Code)
	{
		return (from x in db.Item_Doctor
			where x.ID == Code
			select x.Name).FirstOrDefault();
	}
}


