using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using GTSErpSystem.BLL.Orders;

namespace GTSErpSystem.BLL.Account;

internal class Class_AccountCard
{
	private GTSdbEntities db = new GTSdbEntities();

	private SqlConnection con = new SqlConnection(LoginDetails.ConnString);

	private DataTable AvergItemTable = new DataTable();

	private Class_OrdersReturn ORDR = new Class_OrdersReturn();

	public List<GetAccountTree_Result> GetAccountTree()
	{
		return db.GetAccountTree().ToList();
	}

	public decimal GetItemTotalQuantity(int itemId, int StoreId)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)itemId && x.StoreID == (int?)StoreId).Sum((ItemQuantity x) => x.CurrentBalance).GetValueOrDefault();
	}

	public List<Account_Accounts> GetAccounts()
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Account_Accounts.ToList();
	}

	public List<Account_Final> GetAccountFinal()
	{
		return db.Account_Final.ToList();
	}

	public List<Account_Nature> GetAccountNature()
	{
		return db.Account_Nature.ToList();
	}

	public List<Account_Suspended> GetAccountSuspended()
	{
		return db.Account_Suspended.ToList();
	}

	public List<Account_Type> GetAccountType()
	{
		return db.Account_Type.ToList();
	}

	public List<Account_Branch> GetAccountBranches()
	{
		return db.Account_Branch.ToList();
	}

	public string GetAccountName(int AccountID)
	{
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.ID == AccountID).FirstOrDefault();
		return account_Accounts.Account_Name;
	}

	public int GetAccountNum(int AccountID)
	{
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.ID == AccountID).FirstOrDefault();
		return Convert.ToInt32(account_Accounts.Account_No);
	}

	public int Insert(int CodeFather, int CodeSon, string Account_Name_Ar, string Account_Name_En, int GroupAccount, int AccountSuspended, int AccountNature, int Final_Account, int Account_Level, string Phone, string Fax, string Address, string Note, int BranchID)
	{
		int num = 0;
		int num2 = Convert.ToInt32(db.Account_Accounts.Select((Account_Accounts x) => x.Account_No).DefaultIfEmpty(11013).Max()) + 1;
		int num3 = Convert.ToInt32(db.Account_Accounts.Select((Account_Accounts x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
		Account_Accounts account_Accounts = new Account_Accounts();
		account_Accounts.ID = num3;
		account_Accounts.Main_Account_No = CodeFather;
		account_Accounts.Account_No = CodeSon;
		account_Accounts.Account_Name = Account_Name_Ar;
		account_Accounts.E_Account_Name = Account_Name_En;
		account_Accounts.Account_Type = GroupAccount;
		account_Accounts.Suspended = AccountSuspended;
		account_Accounts.Account_Nature = AccountNature;
		account_Accounts.Final_Account = Final_Account;
		account_Accounts.Account_Level = Account_Level;
		account_Accounts.Priv_Debit = 0m;
		account_Accounts.Priv_Credit = 0m;
		account_Accounts.AddressA = Address;
		account_Accounts.PhoneA = Phone;
		account_Accounts.FaxA = Fax;
		account_Accounts.Note = Note;
		account_Accounts.BranchID = BranchID;
		account_Accounts.UserID_Add = LoginDetails.UserID;
		account_Accounts.UserBranch_Add = LoginDetails.BranchID;
		account_Accounts.UserMacAddress_Add = LoginDetails.macAddress;
		account_Accounts.UserDate_Add = DateTime.Now;
		db.Account_Accounts.Add(account_Accounts);
		db.SaveChanges();
		return num3;
	}

	public int Update(int AccntID, int CodeFather, int CodeSon, string Account_Name_Ar, string Account_Name_En, int GroupAccount, int AccountSuspended, int AccountNature, int Final_Account, int Account_Level, string Phone, string Fax, string Address, string Note, int BranchID)
	{
		int result = 0;
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.ID == AccntID).FirstOrDefault();
		if (account_Accounts != null)
		{
			account_Accounts.Main_Account_No = CodeFather;
			account_Accounts.Account_No = CodeSon;
			account_Accounts.Account_Name = Account_Name_Ar;
			account_Accounts.E_Account_Name = Account_Name_En;
			account_Accounts.Account_Type = GroupAccount;
			account_Accounts.Suspended = AccountSuspended;
			account_Accounts.Account_Nature = AccountNature;
			account_Accounts.Final_Account = Final_Account;
			account_Accounts.Account_Level = Account_Level;
			account_Accounts.AddressA = Address;
			account_Accounts.PhoneA = Phone;
			account_Accounts.FaxA = Fax;
			account_Accounts.Note = Note;
			account_Accounts.BranchID = BranchID;
			account_Accounts.UserID_Update = LoginDetails.UserID;
			account_Accounts.UserBranch_Update = LoginDetails.GroupID;
			account_Accounts.UserMacAddress_Update = LoginDetails.macAddress;
			account_Accounts.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = 1;
		}
		return result;
	}

	public int Delete(int AccntID)
	{
		int result = 0;
		Account_Accounts account_Accounts = db.Account_Accounts.FirstOrDefault((Account_Accounts x) => x.ID == AccntID);
		Account_Accounts account_Accounts2 = db.Account_Accounts.FirstOrDefault((Account_Accounts x) => x.Main_Account_No == (int?)AccntID);
		Tran_TranDetails tran_TranDetails = db.Tran_TranDetails.FirstOrDefault((Tran_TranDetails x) => x.Account_Sn == (int?)AccntID);
		if (account_Accounts != null && account_Accounts2 == null && tran_TranDetails == null)
		{
			db.Account_Accounts.Attach(account_Accounts);
			db.Account_Accounts.Remove(account_Accounts);
			result = db.SaveChanges();
		}
		return result;
	}

	public int GetISFind(int AcctNum)
	{
		int num = 0;
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)AcctNum).FirstOrDefault();
		if (account_Accounts != null)
		{
			return 0;
		}
		return 1;
	}

	public ObjectResult<decimal?> Charge(int sn_Account)
	{
		return db.AccountCharge(sn_Account);
	}

	public int SaveGard(int BranchId, DateTime GardDate, string NoteNum, int UserAdd, int UserBarnchAdd, string UserMacAdd, DataTable Items)
	{
		if (!AvergItemTable.Columns.Contains("ItemId"))
		{
			AvergItemTable.Columns.Add("ItemId", typeof(int));
		}
		int result = 0;
		using (SqlCommand sqlCommand = new SqlCommand("Insert_Gard", con))
		{
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = LoginDetails.BranchID;
			sqlCommand.Parameters.Add("@GardDate", SqlDbType.DateTime).Value = GardDate;
			sqlCommand.Parameters.Add("@NoteNum", SqlDbType.NVarChar).Value = NoteNum;
			sqlCommand.Parameters.Add("@UserID_Add", SqlDbType.Int).Value = UserAdd;
			sqlCommand.Parameters.Add("@UserBranch_Add", SqlDbType.Int).Value = UserBarnchAdd;
			sqlCommand.Parameters.Add("@UserMacAddress_Add", SqlDbType.NVarChar).Value = UserMacAdd;
			DataTable changes = Items.GetChanges(DataRowState.Added);
			sqlCommand.Parameters.AddWithValue("@Items", changes);
			SqlParameter sqlParameter = sqlCommand.Parameters.Add("@Return Value", SqlDbType.Int);
			sqlParameter.Direction = ParameterDirection.ReturnValue;
			sqlCommand.CommandTimeout = 999999;
			if (con.State == ConnectionState.Closed)
			{
				con.Open();
			}
			result = sqlCommand.ExecuteNonQuery();
			object value = sqlParameter.Value;
			con.Close();
			result = Convert.ToInt32(value);
		}
		AvergItemTable.Clear();
		foreach (DataRow row in Items.Rows)
		{
			int num = Convert.ToInt32(row["ItemID"]);
			if (num > 0)
			{
				AvergItemTable.Rows.Add(num);
			}
		}
		if (AvergItemTable.Rows.Count > 0)
		{
			ORDR.ExecuteBase(AvergItemTable);
		}
		return result;
	}
}


