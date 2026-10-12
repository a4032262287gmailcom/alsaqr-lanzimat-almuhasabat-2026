using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Items;

public class Class_Unit
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Item_Unit> GetAllUsers()
	{
		return db.Item_Unit.ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Item_Unit item_Unit = db.Item_Unit.Where((Item_Unit x) => x.Name == Name).FirstOrDefault();
		if (item_Unit == null)
		{
			int iD = Convert.ToInt32(db.Item_Unit.Select((Item_Unit x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Item_Unit item_Unit2 = new Item_Unit();
			item_Unit2.ID = iD;
			item_Unit2.Name = Name;
			item_Unit2.UserID_Add = LoginDetails.UserID;
			item_Unit2.UserBranch_Add = LoginDetails.BranchID;
			item_Unit2.UserMacAddress_Add = LoginDetails.macAddress;
			item_Unit2.UserDate_Add = DateTime.Now;
			db.Item_Unit.Add(item_Unit2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Item_Unit item_Unit = db.Item_Unit.Where((Item_Unit x) => x.ID == ID).FirstOrDefault();
		Item_Unit item_Unit2 = db.Item_Unit.Where((Item_Unit x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (item_Unit != null && item_Unit2 == null)
		{
			item_Unit.Name = Name;
			item_Unit.UserID_Update = LoginDetails.UserID;
			item_Unit.UserBranch_Update = LoginDetails.GroupID;
			item_Unit.UserMacAddress_Update = LoginDetails.macAddress;
			item_Unit.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Item_Unit item_Unit = db.Item_Unit.Where((Item_Unit x) => x.ID == Sn).FirstOrDefault();
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.UnitSmall == (int?)Sn || x.UnitMedium == (int?)Sn || x.UnitLarge == (int?)Sn).FirstOrDefault();
		if (item_Unit != null && item_Items == null)
		{
			db.Item_Unit.Attach(item_Unit);
			db.Item_Unit.Remove(item_Unit);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetUnitName(int Code)
	{
		return (from x in db.Item_Unit
			where x.ID == Code
			select x.Name).FirstOrDefault();
	}
}


