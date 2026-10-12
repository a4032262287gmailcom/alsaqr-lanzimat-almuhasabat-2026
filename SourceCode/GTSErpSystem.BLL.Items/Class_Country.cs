using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Items;

public class Class_Country
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Item_Country> GetAllUsers()
	{
		return db.Item_Country.ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Item_Country item_Country = db.Item_Country.Where((Item_Country x) => x.Name == Name).FirstOrDefault();
		if (item_Country == null)
		{
			int iD = Convert.ToInt32(db.Item_Country.Select((Item_Country x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Item_Country item_Country2 = new Item_Country();
			item_Country2.ID = iD;
			item_Country2.Name = Name;
			item_Country2.UserID_Add = LoginDetails.UserID;
			item_Country2.UserBranch_Add = LoginDetails.BranchID;
			item_Country2.UserMacAddress_Add = LoginDetails.macAddress;
			item_Country2.UserDate_Add = DateTime.Now;
			db.Item_Country.Add(item_Country2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Item_Country item_Country = db.Item_Country.Where((Item_Country x) => x.ID == ID).FirstOrDefault();
		Item_Country item_Country2 = db.Item_Country.Where((Item_Country x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (item_Country != null && item_Country2 == null)
		{
			item_Country.Name = Name;
			item_Country.UserID_Update = LoginDetails.UserID;
			item_Country.UserBranch_Update = LoginDetails.GroupID;
			item_Country.UserMacAddress_Update = LoginDetails.macAddress;
			item_Country.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Item_Country item_Country = db.Item_Country.Where((Item_Country x) => x.ID == Sn).FirstOrDefault();
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.CountryID == (int?)Sn).FirstOrDefault();
		if (item_Country != null && item_Items == null)
		{
			db.Item_Country.Attach(item_Country);
			db.Item_Country.Remove(item_Country);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetUnitName(int Code)
	{
		return (from x in db.Item_Country
			where x.ID == Code
			select x.Name).FirstOrDefault();
	}
}


