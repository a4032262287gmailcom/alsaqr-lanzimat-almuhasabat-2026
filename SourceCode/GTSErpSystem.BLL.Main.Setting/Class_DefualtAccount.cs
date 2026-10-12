using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.Main.Setting;

public class Class_DefualtAccount
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Select_SearchAccountReceipt_Result> GetSearchAccount()
	{
		return db.Select_SearchAccountReceipt(LoginDetails.BranchID).ToList();
	}

	public List<Select_SearchAccount_Result> GetSearchDefualtAccount()
	{
		return db.Select_SearchAccount().ToList();
	}

	public List<Select_AccountDefualt_Result> GetAccountDefualt(int BranchID)
	{
		return db.Select_AccountDefualt(BranchID).ToList();
	}

	public List<Select_AccountCustomer_Result> GetCustomerDefualt(int BranchID)
	{
		return db.Select_AccountCustomer(BranchID).ToList();
	}

	public List<Select_SearchAccountReceiptForAccountTree_Result> GetSearchAccountForTree()
	{
		return db.Select_SearchAccountReceiptForAccountTree().ToList();
	}

	public List<Account_DefualtAccount> GetDefualtAccount()
	{
		return db.Account_DefualtAccount.ToList();
	}

	public List<Account_Accounts> GetAccounts()
	{
		return db.Account_Accounts.ToList();
	}

	public string GetAccountType(int Code)
	{
		return db.Account_Type.Where((Account_Type z) => z.ID == Code).FirstOrDefault().Name;
	}

	public string GetAccountName(int Code)
	{
		return db.Account_Accounts.Where((Account_Accounts z) => z.ID == Code).FirstOrDefault().Account_Name;
	}

	public List<Account_Branch> GetBranches()
	{
		return db.Account_Branch.ToList();
	}

	public List<TblSetting> GetSettings(int BrnID)
	{
		return db.TblSettings.Where((TblSetting x) => x.ID == BrnID).ToList();
	}

	public int UpdateSetting(int BRNID, int StoreMain, int Store2)
	{
		int num = 0;
		TblSetting tblSetting = db.TblSettings.Where((TblSetting x) => x.ID == BRNID).FirstOrDefault();
		if (tblSetting != null)
		{
			tblSetting.StoreID = StoreMain;
			tblSetting.StoreIDWaiying = Store2;
			db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Insert(int BRNID, DataTable Items)
	{
		int result = 0;
		List<Account_DefualtAccount2> list = db.Account_DefualtAccount2.Where((Account_DefualtAccount2 x) => x.BranchID == (int?)BRNID).ToList();
		if (list != null)
		{
			DeleteDefaultAccount(BRNID);
			foreach (DataRow row in Items.Rows)
			{
				Account_DefualtAccount2 account_DefualtAccount = new Account_DefualtAccount2();
				account_DefualtAccount.ID = Convert.ToInt32(row["كود"].ToString());
				account_DefualtAccount.Name = row["اسم الحساب الرئيسي"].ToString();
				account_DefualtAccount.AccountID = Convert.ToInt32(row["رقم الحساب"].ToString());
				account_DefualtAccount.BranchID = BRNID;
				db.Account_DefualtAccount2.Add(account_DefualtAccount);
				result = db.SaveChanges();
			}
		}
		return result;
	}

	public int InsertCustomer(int BRNID, DataTable Items)
	{
		int result = 0;
		List<Account_DefualtCustomer> list = db.Account_DefualtCustomer.Where((Account_DefualtCustomer x) => x.BranchID == (int?)BRNID).ToList();
		if (list != null)
		{
			DeleteDefaultCustomer(BRNID);
			foreach (DataRow row in Items.Rows)
			{
				Account_DefualtCustomer account_DefualtCustomer = new Account_DefualtCustomer();
				account_DefualtCustomer.ID = Convert.ToInt32(row["كود"].ToString());
				account_DefualtCustomer.Name = row["اسم الحساب الرئيسي"].ToString();
				account_DefualtCustomer.AccountID = Convert.ToInt32(row["رقم الحساب"].ToString());
				account_DefualtCustomer.BranchID = BRNID;
				db.Account_DefualtCustomer.Add(account_DefualtCustomer);
				result = db.SaveChanges();
			}
		}
		return result;
	}

	public void DeleteDefaultAccount(int BRNID)
	{
		List<Account_DefualtAccount2> list = db.Account_DefualtAccount2.Where((Account_DefualtAccount2 x) => x.BranchID == (int?)BRNID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Account_DefualtAccount2 item in list)
		{
			db.Account_DefualtAccount2.Attach(item);
			db.Account_DefualtAccount2.Remove(item);
		}
		db.SaveChanges();
	}

	public void DeleteDefaultCustomer(int BRNID)
	{
		List<Account_DefualtCustomer> list = db.Account_DefualtCustomer.Where((Account_DefualtCustomer x) => x.BranchID == (int?)BRNID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Account_DefualtCustomer item in list)
		{
			db.Account_DefualtCustomer.Attach(item);
			db.Account_DefualtCustomer.Remove(item);
		}
		db.SaveChanges();
	}

	public List<Select_SearchAccountReceipt_Result> SearchAcct(string Code)
	{
		return (from x in db.Select_SearchAccountReceipt(LoginDetails.BranchID)
			where x.Account_Name.Contains(Code)
			select x).ToList();
	}
}


