using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Security;

public class Class_Groups
{
	private GTSdbEntities db = new GTSdbEntities();

	public int Insert(string Name, int Branch, bool SellPrice2, bool SellPrice3, bool LastPrice, bool AvergPrice, bool ProvePrice, int SelectSellPrice, bool QuantityMinZero)
	{
		int result = 0;
		User_Groups user_Groups = db.User_Groups.Where((User_Groups x) => x.Name == Name).FirstOrDefault();
		if (user_Groups == null)
		{
			int iD = Convert.ToInt32(db.User_Groups.Select((User_Groups x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			User_Groups user_Groups2 = new User_Groups();
			user_Groups2.ID = iD;
			user_Groups2.Name = Name;
			user_Groups2.BranchID = Branch;
			user_Groups2.SellPrice2 = SellPrice2;
			user_Groups2.SellPrice3 = SellPrice3;
			user_Groups2.LastPrice = LastPrice;
			user_Groups2.AvergPrice = AvergPrice;
			user_Groups2.ProvePrice = ProvePrice;
			user_Groups2.SelectSellPrice = SelectSellPrice;
			user_Groups2.SelectQuantityPrice = QuantityMinZero;
			user_Groups2.UserID_Add = LoginDetails.UserID;
			user_Groups2.UserBranch_Add = LoginDetails.BranchID;
			user_Groups2.UserMacAddress_Add = LoginDetails.macAddress;
			user_Groups2.UserDate_Add = DateTime.Now;
			db.User_Groups.Add(user_Groups2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name, int Branch, bool SellPrice2, bool SellPrice3, bool LastPrice, bool AvergPrice, bool ProvePrice, int SelectSellPrice, bool QuantityMinZero)
	{
		int num = 0;
		User_Groups user_Groups = db.User_Groups.Where((User_Groups x) => x.ID == ID).FirstOrDefault();
		User_Groups user_Groups2 = db.User_Groups.Where((User_Groups x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (user_Groups != null && user_Groups2 == null)
		{
			user_Groups.Name = Name;
			user_Groups.BranchID = Branch;
			user_Groups.SellPrice2 = SellPrice2;
			user_Groups.SellPrice3 = SellPrice3;
			user_Groups.LastPrice = LastPrice;
			user_Groups.AvergPrice = AvergPrice;
			user_Groups.ProvePrice = ProvePrice;
			user_Groups.SelectSellPrice = SelectSellPrice;
			user_Groups.SelectQuantityPrice = QuantityMinZero;
			user_Groups.UserID_Update = LoginDetails.UserID;
			user_Groups.UserBranch_Update = LoginDetails.GroupID;
			user_Groups.UserMacAddress_Update = LoginDetails.macAddress;
			user_Groups.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		User_Groups user_Groups = db.User_Groups.Where((User_Groups x) => x.ID == Sn).FirstOrDefault();
		User_Login user_Login = db.User_Login.Where((User_Login x) => x.GroupID == (int?)Sn).FirstOrDefault();
		if (user_Groups != null && user_Login == null)
		{
			db.User_Groups.Attach(user_Groups);
			db.User_Groups.Remove(user_Groups);
			result = db.SaveChanges();
			DeletePermission(Sn);
		}
		return result;
	}

	public void DeletePermission(int GPID)
	{
		List<User_Permission> list = db.User_Permission.Where((User_Permission x) => x.GroupID == (int?)GPID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (User_Permission item in list)
		{
			db.User_Permission.Attach(item);
			db.User_Permission.Remove(item);
		}
		db.SaveChanges();
	}

	public List<User_Groups> GetAllUsers()
	{
		return db.User_Groups.ToList();
	}

	public List<User_Login> GetUsers()
	{
		return db.User_Login.ToList();
	}

	public List<Account_Branch> GetAllBranch()
	{
		return db.Account_Branch.ToList();
	}

	public string GetBranchName(int ID2)
	{
		return (from z in db.Account_Branch
			where z.ID == ID2
			select z.Name).FirstOrDefault();
	}

	public List<User_Groups> GetAllGroups()
	{
		return db.User_Groups.ToList();
	}

	public List<User_SellPrice> GetAllUser_SellPrice()
	{
		return db.User_SellPrice.ToList();
	}

	public List<User_Screens> GetAllGroupsPermaion()
	{
		return db.User_Screens.Where((User_Screens x) => x.ISShow == (bool?)true).ToList();
	}

	public List<User_Permission> GetPermassionByGroupID(int GroupId)
	{
		return db.User_Permission.Where((User_Permission x) => x.GroupID == (int?)GroupId).ToList();
	}

	public int SLCID()
	{
		return Convert.ToInt32(db.User_Groups.Select((User_Groups x) => x.ID > 0).FirstOrDefault());
	}

	public int InsertOnePermassion(int GroupID, int ScreenID, bool Allow_Enter, bool Allow_Save, bool Allow_Edit, bool Allow_Delete, bool Allow_Print, bool Allow_Export, int User_Add, string mac)
	{
		int num = 0;
		User_Permission user_Permission = db.User_Permission.Where((User_Permission x) => x.GroupID == (int?)GroupID && x.ScreenID == (int?)ScreenID).FirstOrDefault();
		if (user_Permission == null)
		{
			int iD = Convert.ToInt32(db.User_Permission.Select((User_Permission p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
			User_Permission user_Permission2 = new User_Permission();
			user_Permission2.ID = iD;
			user_Permission2.GroupID = GroupID;
			user_Permission2.ScreenID = ScreenID;
			user_Permission2.Allow_Enter = Allow_Enter;
			user_Permission2.Allow_Save = Allow_Save;
			user_Permission2.Allow_Edit = Allow_Edit;
			user_Permission2.Allow_Delete = Allow_Delete;
			user_Permission2.Allow_Print = Allow_Print;
			user_Permission2.Allow_Export = Allow_Export;
			user_Permission2.UserID_Add = LoginDetails.UserID;
			user_Permission2.UserBranch_Add = LoginDetails.BranchID;
			user_Permission2.UserMacAddress_Add = LoginDetails.macAddress;
			user_Permission2.UserDate_Add = DateTime.Now;
			db.User_Permission.Add(user_Permission2);
			return db.SaveChanges();
		}
		user_Permission.Allow_Enter = Allow_Enter;
		user_Permission.Allow_Save = Allow_Save;
		user_Permission.Allow_Edit = Allow_Edit;
		user_Permission.Allow_Delete = Allow_Delete;
		user_Permission.Allow_Print = Allow_Print;
		user_Permission.Allow_Export = Allow_Export;
		user_Permission.UserID_Update = LoginDetails.UserID;
		user_Permission.UserBranch_Update = LoginDetails.GroupID;
		user_Permission.UserMacAddress_Update = LoginDetails.macAddress;
		user_Permission.UserDate_Update = DateTime.Now;
		num = db.SaveChanges();
		return 1;
	}
}


