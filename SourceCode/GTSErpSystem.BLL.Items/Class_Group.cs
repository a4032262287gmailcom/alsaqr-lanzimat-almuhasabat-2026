using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Items;

internal class Class_Group
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Item_Groups> GetAll()
	{
		return db.Item_Groups.ToList();
	}

	public List<Printer> GetAllPrinter()
	{
		return db.Printers.Where((Printer x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int Insert(string Name, int PrinterID, bool IsShow, string Images, int AccountID)
	{
		int result = 0;
		Item_Groups item_Groups = db.Item_Groups.Where((Item_Groups x) => x.Name == Name).FirstOrDefault();
		if (item_Groups == null)
		{
			int iD = Convert.ToInt32(db.Item_Groups.Select((Item_Groups x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Item_Groups item_Groups2 = new Item_Groups();
			item_Groups2.ID = iD;
			item_Groups2.Name = Name;
			item_Groups2.IsShow = IsShow;
			item_Groups2.PrinterID = PrinterID;
			item_Groups2.Image = Images;
			item_Groups2.AccountID = AccountID;
			item_Groups2.UserID_Add = LoginDetails.UserID;
			item_Groups2.UserBranch_Add = LoginDetails.BranchID;
			item_Groups2.UserMacAddress_Add = LoginDetails.macAddress;
			item_Groups2.UserDate_Add = DateTime.Now;
			db.Item_Groups.Add(item_Groups2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name, int PrinterID, bool IsShow, string Images, int AccountID)
	{
		int num = 0;
		Item_Groups item_Groups = db.Item_Groups.Where((Item_Groups x) => x.ID == ID).FirstOrDefault();
		Item_Groups item_Groups2 = db.Item_Groups.Where((Item_Groups x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (item_Groups != null && item_Groups2 == null)
		{
			item_Groups.Name = Name;
			item_Groups.PrinterID = PrinterID;
			item_Groups.AccountID = AccountID;
			item_Groups.Image = Images;
			item_Groups.IsShow = IsShow;
			item_Groups.UserID_Update = LoginDetails.UserID;
			item_Groups.UserBranch_Update = LoginDetails.GroupID;
			item_Groups.UserMacAddress_Update = LoginDetails.macAddress;
			item_Groups.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(string CatName)
	{
		int result = 0;
		Item_Groups CheckCat = db.Item_Groups.Where((Item_Groups x) => x.Name == CatName).FirstOrDefault();
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.Category_ID == (int?)CheckCat.ID).FirstOrDefault();
		if (CheckCat != null && item_Items == null)
		{
			db.Item_Groups.Attach(CheckCat);
			db.Item_Groups.Remove(CheckCat);
			result = db.SaveChanges();
		}
		return result;
	}

	public void InsertPrinter(string Printer)
	{
		Printer printer = new Printer();
		printer.Name = Printer;
		db.Printers.Add(printer);
		db.SaveChanges();
	}
}


