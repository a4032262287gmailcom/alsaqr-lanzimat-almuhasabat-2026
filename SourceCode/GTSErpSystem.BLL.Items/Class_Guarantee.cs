using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Items;

public class Class_Guarantee
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Item_Guarantee> GetAllUsers()
	{
		return db.Item_Guarantee.ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Item_Guarantee item_Guarantee = db.Item_Guarantee.Where((Item_Guarantee x) => x.Name == Name).FirstOrDefault();
		if (item_Guarantee == null)
		{
			int iD = Convert.ToInt32(db.Item_Guarantee.Select((Item_Guarantee x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Item_Guarantee item_Guarantee2 = new Item_Guarantee();
			item_Guarantee2.ID = iD;
			item_Guarantee2.Name = Name;
			item_Guarantee2.UserID_Add = LoginDetails.UserID;
			item_Guarantee2.UserBranch_Add = LoginDetails.BranchID;
			item_Guarantee2.UserMacAddress_Add = LoginDetails.macAddress;
			item_Guarantee2.UserDate_Add = DateTime.Now;
			db.Item_Guarantee.Add(item_Guarantee2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Item_Guarantee item_Guarantee = db.Item_Guarantee.Where((Item_Guarantee x) => x.ID == ID).FirstOrDefault();
		Item_Guarantee item_Guarantee2 = db.Item_Guarantee.Where((Item_Guarantee x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (item_Guarantee != null && item_Guarantee2 == null)
		{
			item_Guarantee.Name = Name;
			item_Guarantee.UserID_Update = LoginDetails.UserID;
			item_Guarantee.UserBranch_Update = LoginDetails.GroupID;
			item_Guarantee.UserMacAddress_Update = LoginDetails.macAddress;
			item_Guarantee.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Item_Guarantee item_Guarantee = db.Item_Guarantee.Where((Item_Guarantee x) => x.ID == Sn).FirstOrDefault();
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.GuaranteeID == (int?)Sn).FirstOrDefault();
		if (item_Guarantee != null && item_Items == null)
		{
			db.Item_Guarantee.Attach(item_Guarantee);
			db.Item_Guarantee.Remove(item_Guarantee);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetUnitName(int Code)
	{
		return (from x in db.Item_Guarantee
			where x.ID == Code
			select x.Name).FirstOrDefault();
	}
}


