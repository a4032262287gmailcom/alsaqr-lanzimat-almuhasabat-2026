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

	public int SaveItemAccountDefaults(int BRNID, int StoreMain, int Store2, DataTable Items)
	{
		if (BRNID <= 0)
			throw new ArgumentOutOfRangeException(nameof(BRNID));
		if (StoreMain <= 0 || Store2 <= 0 || StoreMain == Store2)
			throw new ArgumentException("يجب اختيار مستودعين مختلفين.");
		if (Items == null || Items.Rows.Count == 0)
			throw new ArgumentException("لا توجد حسابات افتراضية لحفظها.", nameof(Items));

		var mappings = new List<Account_DefualtAccount2>();
		foreach (DataRow row in Items.Rows)
		{
			if (!int.TryParse(Convert.ToString(row["كود"]), out int id) || id <= 0)
				throw new InvalidOperationException("يوجد حساب رئيسي غير صالح في القائمة.");
			if (!int.TryParse(Convert.ToString(row["رقم الحساب"]), out int accountID) || accountID <= 0)
				throw new InvalidOperationException("اختر الحساب المقابل لكل صف قبل الحفظ.");

			mappings.Add(new Account_DefualtAccount2
			{
				ID = id,
				Name = Convert.ToString(row["اسم الحساب الرئيسي"]),
				AccountID = accountID,
				BranchID = BRNID
			});
		}

		TblSetting setting = db.TblSettings.FirstOrDefault(x => x.ID == BRNID);
		Account_DefualtAccount defaults = db.Account_DefualtAccount
			.FirstOrDefault(x => x.BranchID == (int?)BRNID);
		if (setting == null || defaults == null)
			return 0;

		using (var transaction = db.Database.BeginTransaction())
		{
			List<Account_DefualtAccount2> existing = db.Account_DefualtAccount2
				.Where(x => x.BranchID == (int?)BRNID)
				.ToList();
			db.Account_DefualtAccount2.RemoveRange(existing);
			setting.StoreID = StoreMain;
			setting.StoreIDWaiying = Store2;
			defaults.Acc_StoreMain = StoreMain;
			defaults.Acc_StoreWaiting = Store2;

			int result = db.SaveChanges();
			db.Account_DefualtAccount2.AddRange(mappings);
			result += db.SaveChanges();
			transaction.Commit();
			return result;
		}
	}

	public int UpdateBranchDefaults(int BRNID, Account_DefualtAccount values, int StoreMain, int Store2)
	{
		if (BRNID <= 0)
			throw new ArgumentOutOfRangeException(nameof(BRNID));
		if (values == null)
			throw new ArgumentNullException(nameof(values));
		if (values.BranchID != BRNID)
			throw new ArgumentException("بيانات الحسابات لا تطابق الفرع المحدد.", nameof(values));
		if (StoreMain <= 0 || Store2 <= 0 || StoreMain == Store2)
			throw new ArgumentException("يجب اختيار مستودعين مختلفين.");

		Account_DefualtAccount account = db.Account_DefualtAccount
			.FirstOrDefault(x => x.BranchID == (int?)BRNID);
		TblSetting setting = db.TblSettings.FirstOrDefault(x => x.ID == BRNID);
		if (account == null || setting == null)
			return 0;

		account.Acc_StoreMain = StoreMain;
		account.Acc_StoreWaiting = Store2;
		account.Acc_CostItemOrder = values.Acc_CostItemOrder;
		account.Acc_VatNum = values.Acc_VatNum;
		account.Acc_CashBox = values.Acc_CashBox;
		account.Acc_OrderCash = values.Acc_OrderCash;
		account.Acc_OrderAjila = values.Acc_OrderAjila;
		account.Acc_OrderReturnCash = values.Acc_OrderReturnCash;
		account.Acc_BankNumCachir = values.Acc_BankNumCachir;
		account.Acc_CustomerCash = values.Acc_CustomerCash;
		account.Acc_supplierCash = values.Acc_supplierCash;
		account.StoreEntagTam = values.StoreEntagTam;
		account.StoreMawadAwalyaa = values.StoreMawadAwalyaa;
		setting.StoreID = StoreMain;
		setting.StoreIDWaiying = Store2;

		using (var transaction = db.Database.BeginTransaction())
		{
			int result = db.SaveChanges();
			transaction.Commit();
			return result;
		}
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
