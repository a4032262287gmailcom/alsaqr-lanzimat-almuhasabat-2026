using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Items;

public class Class_Class
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Item_Class> GetAll()
	{
		return db.Item_Class.ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Item_Class item_Class = db.Item_Class.Where((Item_Class x) => x.Name == Name).FirstOrDefault();
		if (item_Class == null)
		{
			int iD = Convert.ToInt32(db.Item_Class.Select((Item_Class x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Item_Class item_Class2 = new Item_Class();
			item_Class2.ID = iD;
			item_Class2.Name = Name;
			item_Class2.UserID_Add = LoginDetails.UserID;
			item_Class2.UserBranch_Add = LoginDetails.BranchID;
			item_Class2.UserMacAddress_Add = LoginDetails.macAddress;
			item_Class2.UserDate_Add = DateTime.Now;
			db.Item_Class.Add(item_Class2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int result = 0;
		Item_Class item_Class = db.Item_Class.Where((Item_Class x) => x.ID == ID).FirstOrDefault();
		Item_Class item_Class2 = db.Item_Class.Where((Item_Class x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (item_Class != null && item_Class2 == null)
		{
			item_Class.Name = Name;
			item_Class.UserID_Update = LoginDetails.UserID;
			item_Class.UserBranch_Update = LoginDetails.GroupID;
			item_Class.UserMacAddress_Update = LoginDetails.macAddress;
			item_Class.UserDate_Update = DateTime.Now;
			result = db.SaveChanges();
			result = 1;
		}
		return result;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Item_Class item_Class = db.Item_Class.Where((Item_Class x) => x.ID == Sn).FirstOrDefault();
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.Class_ID == (int?)Sn).FirstOrDefault();
		if (item_Class != null && item_Items == null)
		{
			db.Item_Class.Attach(item_Class);
			db.Item_Class.Remove(item_Class);
			result = db.SaveChanges();
		}
		return result;
	}
}


