using System;
using System.Linq;

namespace GTSErpSystem.BLL.Account;

public class Class_OpenAccount
{
	private GTSdbEntities db = new GTSdbEntities();

	public int Update(int CodeSon, decimal Depit, decimal Credit, DateTime DateOpen)
	{
		int result = 0;
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)CodeSon).FirstOrDefault();
		if (account_Accounts != null)
		{
			account_Accounts.Priv_Debit = Depit;
			account_Accounts.Priv_Credit = Credit;
			account_Accounts.Date_OpenCharge = DateOpen;
			result = db.SaveChanges();
		}
		return result;
	}
}


