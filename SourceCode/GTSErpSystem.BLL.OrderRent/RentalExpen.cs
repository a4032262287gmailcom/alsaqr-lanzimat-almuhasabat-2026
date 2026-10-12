using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GTSErpSystem.BLL.OrderRent;

public class RentalExpen
{
	private GTSdbEntities db = new GTSdbEntities();

	private SqlConnection con = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);

	public List<RentalExpens> GetPurchases()
	{
		return db.RentalExpenses.OrderByDescending((RentalExpens o) => o.Id).ToList();
	}

	public List<RentalExpensesDetail> GetPurchasesDetails(int ID)
	{
		return db.RentalExpensesDetails.Where((RentalExpensesDetail x) => x.ExpensesId == (int?)ID).ToList();
	}

	public List<RentalExpens> GetPurchasesExp(int ID)
	{
		return db.RentalExpenses.Where((RentalExpens x) => x.Id == ID).ToList();
	}

	public int InsertExpenses(out int Code, DateTime Date, decimal TotalPrice, decimal AllTax, decimal Net)
	{
		int num = 0;
		Code = 0;
		int num2 = Convert.ToInt32(db.RentalExpenses.Select((RentalExpens p) => p.Id).DefaultIfEmpty(0).Max()) + 1;
		RentalExpens rentalExpens = new RentalExpens();
		rentalExpens.Id = num2;
		rentalExpens.Date = Date;
		rentalExpens.TotalPrice = TotalPrice;
		rentalExpens.AllTax = AllTax;
		rentalExpens.NetTotalPrice = Net;
		rentalExpens.UserID_Add = LoginDetails.UserID;
		rentalExpens.UserBranch_Add = LoginDetails.BranchID;
		rentalExpens.UserMacAddress_Add = LoginDetails.macAddress;
		rentalExpens.UserDate_Add = DateTime.Now;
		db.RentalExpenses.Add(rentalExpens);
		num = db.SaveChanges();
		return num2;
	}

	public int InsertItemsExpenses(DataTable Items, int ID)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["ItemCode"].ToString() != "")
			{
				RentalExpensesDetail rentalExpensesDetail = new RentalExpensesDetail();
				int num = Convert.ToInt32(row["ItemID"]);
				rentalExpensesDetail.ExpensesId = ID;
				rentalExpensesDetail.ItemId = Convert.ToInt32(row["ItemId"]);
				rentalExpensesDetail.ItemCode = row["ItemCode"].ToString();
				rentalExpensesDetail.ItemName = row["ItemName"].ToString();
				rentalExpensesDetail.Note = row["Note"].ToString();
				rentalExpensesDetail.Total = Convert.ToDecimal(row["Total"]);
				rentalExpensesDetail.Vat = Convert.ToDecimal(row["ItemVat"]);
				rentalExpensesDetail.IsVat = Convert.ToBoolean(row["IsVat"]);
				rentalExpensesDetail.Net = Convert.ToDecimal(row["Net"]);
				db.RentalExpensesDetails.Add(rentalExpensesDetail);
				result = db.SaveChanges();
			}
		}
		return result;
	}

	public int UpdateRental(int ExpID, decimal TotalPrice, decimal AllTax, decimal Net)
	{
		int result = 0;
		RentalExpens rentalExpens = db.RentalExpenses.Where((RentalExpens x) => x.Id == ExpID).FirstOrDefault();
		if (rentalExpens != null)
		{
			rentalExpens.TotalPrice = TotalPrice;
			rentalExpens.AllTax = AllTax;
			rentalExpens.NetTotalPrice = Net;
			rentalExpens.UserID_Update = LoginDetails.UserID;
			rentalExpens.UserBranch_Update = LoginDetails.BranchID;
			rentalExpens.UserMacAddress_Update = LoginDetails.macAddress;
			rentalExpens.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = ExpID;
		}
		return result;
	}

	public void deleteItems(int PurID)
	{
		List<RentalExpensesDetail> list = db.RentalExpensesDetails.Where((RentalExpensesDetail x) => x.ExpensesId == (int?)PurID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (RentalExpensesDetail item in list)
		{
			db.RentalExpensesDetails.Attach(item);
			db.RentalExpensesDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int DeleteRental(int PurID)
	{
		int result = 0;
		RentalExpens rentalExpens = db.RentalExpenses.Where((RentalExpens x) => x.Id == PurID).FirstOrDefault();
		if (rentalExpens != null)
		{
			deleteItems(PurID);
			db.RentalExpenses.Attach(rentalExpens);
			db.RentalExpenses.Remove(rentalExpens);
			result = db.SaveChanges();
		}
		return result;
	}
}


