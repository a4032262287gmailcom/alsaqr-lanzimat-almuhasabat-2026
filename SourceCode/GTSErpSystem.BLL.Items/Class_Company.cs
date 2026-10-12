using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Items;

public class Class_Company
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Item_Company> GetAll()
	{
		return db.Item_Company.ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Item_Company item_Company = db.Item_Company.Where((Item_Company x) => x.Name == Name).FirstOrDefault();
		if (item_Company == null)
		{
			int iD = Convert.ToInt32(db.Item_Company.Select((Item_Company x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Item_Company item_Company2 = new Item_Company();
			item_Company2.ID = iD;
			item_Company2.Name = Name;
			item_Company2.UserID_Add = LoginDetails.UserID;
			item_Company2.UserBranch_Add = LoginDetails.BranchID;
			item_Company2.UserMacAddress_Add = LoginDetails.macAddress;
			item_Company2.UserDate_Add = DateTime.Now;
			db.Item_Company.Add(item_Company2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Item_Company item_Company = db.Item_Company.Where((Item_Company x) => x.ID == ID).FirstOrDefault();
		Item_Company item_Company2 = db.Item_Company.Where((Item_Company x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (item_Company != null && item_Company2 == null)
		{
			item_Company.Name = Name;
			item_Company.UserID_Update = LoginDetails.UserID;
			item_Company.UserBranch_Update = LoginDetails.GroupID;
			item_Company.UserMacAddress_Update = LoginDetails.macAddress;
			item_Company.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Item_Company item_Company = db.Item_Company.Where((Item_Company x) => x.ID == Sn).FirstOrDefault();
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.CompanyID == (int?)Sn).FirstOrDefault();
		if (item_Company != null && item_Items == null)
		{
			db.Item_Company.Attach(item_Company);
			db.Item_Company.Remove(item_Company);
			result = db.SaveChanges();
		}
		return result;
	}
}


