using System;
using System.Collections.Generic;
using System.Linq;
using GTSErpSystem.BLL.Main;

namespace GTSErpSystem.BLL.Security;

public class Class_Users
{
	private GTSdbEntities db = new GTSdbEntities();

	public int Insert(string Name, string PassWord, int Branch, int Group, bool Statues, bool SentXmlZakateNow)
	{
		int result = 0;
		User_Login user_Login = db.User_Login.Where((User_Login x) => x.Name == Name && x.GroupID == (int?)Group).FirstOrDefault();
		if (user_Login == null)
		{
			int iD = Convert.ToInt32(db.User_Login.Select((User_Login x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			User_Login user_Login2 = new User_Login();
			user_Login2.ID = iD;
			user_Login2.Name = Name;
			user_Login2.PassWord = Class_Encryption.EncryptString(PassWord, iD.ToString());
			user_Login2.BranchID = Branch;
			user_Login2.GroupID = Group;
			user_Login2.IsActive = Statues;
			user_Login2.SentXmlZakateNow = SentXmlZakateNow;
			user_Login2.Note = "";
			user_Login2.MoneyOpenDay = 0m;
			user_Login2.IsOpenDay = false;
			user_Login2.UserID_Add = LoginDetails.UserID;
			user_Login2.UserBranch_Add = LoginDetails.BranchID;
			user_Login2.UserMacAddress_Add = LoginDetails.macAddress;
			user_Login2.UserDate_Add = DateTime.Now;
			db.User_Login.Add(user_Login2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name, string PassWord, int Branch, int Group, bool Statues)
	{
		int num = 0;
		User_Login user_Login = db.User_Login.Where((User_Login x) => x.ID == ID).FirstOrDefault();
		User_Login user_Login2 = db.User_Login.Where((User_Login x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (user_Login != null && user_Login2 == null)
		{
			user_Login.Name = Name;
			user_Login.PassWord = Class_Encryption.EncryptString(PassWord, ID.ToString());
			user_Login.BranchID = Branch;
			user_Login.GroupID = Group;
			user_Login.IsActive = Statues;
			user_Login.MoneyOpenDay = 0m;
			user_Login.IsOpenDay = false;
			user_Login.UserID_Update = LoginDetails.UserID;
			user_Login.UserBranch_Update = LoginDetails.GroupID;
			user_Login.UserMacAddress_Update = LoginDetails.macAddress;
			user_Login.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int UpdateAndZkah(int ID, string Name, string PassWord, int Branch, int Group, bool Statues, bool SentXmlZakateNow)
	{
		int num = 0;
		User_Login user_Login = db.User_Login.Where((User_Login x) => x.ID == ID).FirstOrDefault();
		User_Login user_Login2 = db.User_Login.Where((User_Login x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (user_Login != null && user_Login2 == null)
		{
			user_Login.Name = Name;
			user_Login.PassWord = Class_Encryption.EncryptString(PassWord, ID.ToString());
			user_Login.BranchID = Branch;
			user_Login.GroupID = Group;
			user_Login.IsActive = Statues;
			user_Login.SentXmlZakateNow = SentXmlZakateNow;
			user_Login.MoneyOpenDay = 0m;
			user_Login.IsOpenDay = false;
			user_Login.UserID_Update = LoginDetails.UserID;
			user_Login.UserBranch_Update = LoginDetails.GroupID;
			user_Login.UserMacAddress_Update = LoginDetails.macAddress;
			user_Login.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int UpdateToElctroncInvoic(int ID, string Name, string Crs, string PrivateKey, string PublicKey, string SecretKey, string Mode)
	{
		int num = 0;
		User_Login user_Login = db.User_Login.Where((User_Login x) => x.ID == ID).FirstOrDefault();
		User_Login user_Login2 = db.User_Login.Where((User_Login x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (user_Login != null && user_Login2 == null)
		{
			user_Login.Name = Name;
			user_Login.Crs = Crs;
			user_Login.PrivateKey = PrivateKey;
			user_Login.PublicKey = PublicKey;
			user_Login.SecretKey = SecretKey;
			user_Login.ModeInvoice = Mode;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int UpdateFrmEditUser(int ID, string Name, string PassWord, int Branch, int Group, bool Statues)
	{
		int num = 0;
		User_Login user_Login = db.User_Login.Where((User_Login x) => x.ID == ID).FirstOrDefault();
		User_Login user_Login2 = db.User_Login.Where((User_Login x) => x.Name == Name).FirstOrDefault();
		if (user_Login != null && user_Login2 == null)
		{
			user_Login.Name = Name;
			user_Login.PassWord = PassWord;
			user_Login.BranchID = Branch;
			user_Login.GroupID = Group;
			user_Login.IsActive = Statues;
			user_Login.UserID_Update = LoginDetails.UserID;
			user_Login.UserBranch_Update = LoginDetails.GroupID;
			user_Login.UserMacAddress_Update = LoginDetails.macAddress;
			user_Login.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		User_Login user_Login = db.User_Login.Where((User_Login x) => x.ID == Sn).FirstOrDefault();
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.UserID_Add == (int?)Sn).FirstOrDefault();
		Tran_Tran tran_Tran2 = db.Tran_Tran.Where((Tran_Tran x) => x.UserID_Update == (int?)Sn).FirstOrDefault();
		if (user_Login != null && tran_Tran == null && tran_Tran2 == null)
		{
			db.User_Login.Attach(user_Login);
			db.User_Login.Remove(user_Login);
			result = db.SaveChanges();
		}
		return result;
	}

	public List<User_Login> GetAllUsers()
	{
		return db.User_Login.ToList();
	}

	public List<User_Groups> GetAllGroups()
	{
		return db.User_Groups.ToList();
	}

	public List<Account_Branch> GetAllBranch()
	{
		return db.Account_Branch.ToList();
	}

	public string GetBranchById(int ID)
	{
		return (from x in db.Account_Branch
			where x.ID == ID
			select x.Name).FirstOrDefault();
	}

	public string GetGroupName(int ID2)
	{
		return (from z in db.User_Groups
			where z.ID == ID2
			select z.Name).FirstOrDefault();
	}

	public User_Login GetUserById(int id)
	{
		return db.User_Login.FirstOrDefault((User_Login x) => x.ID == id);
	}
}


