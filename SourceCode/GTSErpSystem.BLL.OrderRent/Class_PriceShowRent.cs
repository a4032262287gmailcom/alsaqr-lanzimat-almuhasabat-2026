using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GTSErpSystem.BLL.OrderRent;

public class Class_PriceShowRent
{
	private int CachCode = 0;

	private DataTable dtCurrentTable = new DataTable();

	private GTSdbEntities db = new GTSdbEntities();

	private int DecimalNum = LoginDetails.DecimalNum;

	private SqlConnection con = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);

	public int GetUnitIdByName(string Nmae)
	{
		int num = 0;
		return (from x in db.Item_Unit
			where x.Name == Nmae
			select x.ID).FirstOrDefault();
	}

	public int InsertRental(out int Code, int SupplierID, string SupplierName, string SupplierPhone, string SupplierIDNum, DateTime Purchases_Date, DateTime Return_Date, string ParentName, int CostCenter, string Note, string Address, decimal DaysNum, decimal TotalPrices, decimal Tax, decimal AllDisCount, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal deposit, int PaymantType, decimal CashMoney, decimal CashBank, decimal Paid, decimal Rest)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.Orders_PriceShowRental.Select((Orders_PriceShowRental p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Orders_PriceShowRental
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		Orders_PriceShowRental orders_PriceShowRental = new Orders_PriceShowRental();
		orders_PriceShowRental.ID = iD;
		CachCode = num2;
		Code = num2;
		orders_PriceShowRental.BranchID = LoginDetails.BranchID;
		orders_PriceShowRental.PurBranchID = num2;
		orders_PriceShowRental.SupplierID = SupplierID;
		orders_PriceShowRental.SupplierName = SupplierName;
		orders_PriceShowRental.SupplierPhone = SupplierPhone;
		orders_PriceShowRental.SupplierIDNum = SupplierIDNum;
		orders_PriceShowRental.RentalDate = Purchases_Date;
		orders_PriceShowRental.ReturnDate = Return_Date;
		orders_PriceShowRental.ParentName = ParentName;
		orders_PriceShowRental.CostCentersID = CostCenter;
		orders_PriceShowRental.Note = Note;
		orders_PriceShowRental.Address = Address;
		orders_PriceShowRental.DayNums = DaysNum;
		orders_PriceShowRental.TotalPrices = TotalPrices;
		orders_PriceShowRental.Tax = Tax;
		orders_PriceShowRental.AllDiscount = AllDisCount;
		orders_PriceShowRental.DiscountNum = DiscountNum;
		orders_PriceShowRental.DiscountPerantage = DiscountPerantage;
		orders_PriceShowRental.Tax_Discount = Tax_Discount;
		orders_PriceShowRental.TotalPrices_Discount = TotalPrices_Discount;
		orders_PriceShowRental.Net = Net;
		orders_PriceShowRental.deposit = deposit;
		orders_PriceShowRental.UserID_Add = LoginDetails.UserID;
		orders_PriceShowRental.UserBranch_Add = LoginDetails.BranchID;
		orders_PriceShowRental.UserMacAddress_Add = LoginDetails.macAddress;
		orders_PriceShowRental.UserDate_Add = DateTime.Now;
		orders_PriceShowRental.Order_Paymant_Type = PaymantType;
		orders_PriceShowRental.CashMoney = CashMoney;
		orders_PriceShowRental.CashBank = CashBank;
		orders_PriceShowRental.Paid = Paid;
		orders_PriceShowRental.rest = Rest;
		db.Orders_PriceShowRental.Add(orders_PriceShowRental);
		num = db.SaveChanges();
		return num2;
	}

	public int InsertItemsRent(DataTable Items, int Purchese_ID)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["ItemCode"].ToString() != "")
			{
				Ordert_PriceShowRentalDetails ordert_PriceShowRentalDetails = new Ordert_PriceShowRentalDetails();
				int ItemRowChek = Convert.ToInt32(row["ItemID"]);
				Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
				string text = row["ItemCode"].ToString();
				ordert_PriceShowRentalDetails.Purchese_ID = Purchese_ID;
				ordert_PriceShowRentalDetails.BranchID = LoginDetails.BranchID;
				ordert_PriceShowRentalDetails.ItemID = Convert.ToInt32(row["ItemID"]);
				string nmae = row["UnitName"].ToString();
				ordert_PriceShowRentalDetails.ItemUnitID = GetUnitIdByName(nmae);
				ordert_PriceShowRentalDetails.SmallUnitPrice = Convert.ToDecimal(row["UnitPrice"].ToString());
				ordert_PriceShowRentalDetails.Quantity = Convert.ToDecimal(row["Quantity"].ToString());
				ordert_PriceShowRentalDetails.StoreID = Convert.ToInt32(row["StoreID"]);
				ordert_PriceShowRentalDetails.LastCost = Math.Round(Convert.ToDecimal(row["AvergItem"].ToString()), DecimalNum);
				ordert_PriceShowRentalDetails.UnitPrice = Math.Round(Convert.ToDecimal(row["UnitPrice"].ToString()), DecimalNum);
				ordert_PriceShowRentalDetails.Total = Math.Round(Convert.ToDecimal(row["Total"].ToString()), DecimalNum);
				ordert_PriceShowRentalDetails.DaysNum = Math.Round(Convert.ToDecimal(row["DayNum"].ToString()), DecimalNum);
				ordert_PriceShowRentalDetails.SumPrice = Math.Round(Convert.ToDecimal(row["SumPrice"].ToString()), DecimalNum);
				ordert_PriceShowRentalDetails.SumPriceDisc = Math.Round(Convert.ToDecimal(row["SumPriceDisc"].ToString()), DecimalNum);
				ordert_PriceShowRentalDetails.NetUnitPrice = Math.Round(Convert.ToDecimal(row["UnitPriceDisc"].ToString()), DecimalNum);
				ordert_PriceShowRentalDetails.NetTotalPrice = Math.Round(Convert.ToDecimal(row["Net"].ToString()), DecimalNum);
				ordert_PriceShowRentalDetails.VAT = Math.Round(Convert.ToDecimal(row["ItemVat"].ToString()), DecimalNum);
				ordert_PriceShowRentalDetails.ItemUnitType = row["UnitType"].ToString();
				ordert_PriceShowRentalDetails.VAT_Discount = Math.Round(Convert.ToDecimal(row["ItemVatDisc"].ToString()), DecimalNum);
				ordert_PriceShowRentalDetails.AmountDisc = Convert.ToDecimal(row["AmountDisc"]);
				ordert_PriceShowRentalDetails.PercentDisc = Convert.ToDecimal(row["PercentDisc"]);
				ordert_PriceShowRentalDetails.ItemNote = row["ItemNote"].ToString();
				db.Ordert_PriceShowRentalDetails.Add(ordert_PriceShowRentalDetails);
				result = db.SaveChanges();
			}
		}
		return result;
	}

	public int UpdateRental(int PurID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierIDNum, DateTime Purchases_Date, DateTime Return_Date, string ParentName, int CostCenter, string Note, string Address, decimal DaysNum, decimal TotalPrices, decimal Tax, decimal AllDisCount, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal deposit, int PaymantType, decimal CashMoney, decimal CashBank, decimal Paid, decimal Rest)
	{
		int result = 0;
		Orders_PriceShowRental orders_PriceShowRental = db.Orders_PriceShowRental.Where((Orders_PriceShowRental x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (orders_PriceShowRental != null)
		{
			orders_PriceShowRental.BranchID = LoginDetails.BranchID;
			orders_PriceShowRental.SupplierID = SupplierID;
			orders_PriceShowRental.SupplierName = SupplierName;
			orders_PriceShowRental.SupplierPhone = SupplierPhone;
			orders_PriceShowRental.SupplierIDNum = SupplierIDNum;
			orders_PriceShowRental.RentalDate = Purchases_Date;
			orders_PriceShowRental.ReturnDate = Return_Date;
			orders_PriceShowRental.ParentName = ParentName;
			orders_PriceShowRental.CostCentersID = CostCenter;
			orders_PriceShowRental.Note = Note;
			orders_PriceShowRental.Address = Address;
			orders_PriceShowRental.DayNums = DaysNum;
			orders_PriceShowRental.TotalPrices = TotalPrices;
			orders_PriceShowRental.Tax = Tax;
			orders_PriceShowRental.AllDiscount = AllDisCount;
			orders_PriceShowRental.DiscountNum = DiscountNum;
			orders_PriceShowRental.DiscountPerantage = DiscountPerantage;
			orders_PriceShowRental.Tax_Discount = Tax_Discount;
			orders_PriceShowRental.TotalPrices_Discount = TotalPrices_Discount;
			orders_PriceShowRental.Net = Net;
			orders_PriceShowRental.deposit = deposit;
			orders_PriceShowRental.UserID_Update = LoginDetails.UserID;
			orders_PriceShowRental.UserBranch_Update = LoginDetails.BranchID;
			orders_PriceShowRental.UserMacAddress_Update = LoginDetails.macAddress;
			orders_PriceShowRental.UserDate_Update = DateTime.Now;
			orders_PriceShowRental.Order_Paymant_Type = PaymantType;
			orders_PriceShowRental.CashMoney = CashMoney;
			orders_PriceShowRental.CashBank = CashBank;
			orders_PriceShowRental.Paid = Paid;
			orders_PriceShowRental.rest = Rest;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public void deleteItemsRentalByPurcahesId(int PurID)
	{
		List<Ordert_PriceShowRentalDetails> list = db.Ordert_PriceShowRentalDetails.Where((Ordert_PriceShowRentalDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Ordert_PriceShowRentalDetails item in list)
		{
			Item_Items item_Items = db.Item_Items.Where((Item_Items x) => (int?)x.ItemId == item.ItemID).FirstOrDefault();
			db.Ordert_PriceShowRentalDetails.Attach(item);
			db.Ordert_PriceShowRentalDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int DeleteRental(int PurID)
	{
		int result = 0;
		Orders_PriceShowRental orders_PriceShowRental = db.Orders_PriceShowRental.Where((Orders_PriceShowRental x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (orders_PriceShowRental != null)
		{
			deleteItemsRentalByPurcahesId(PurID);
			db.Orders_PriceShowRental.Attach(orders_PriceShowRental);
			db.Orders_PriceShowRental.Remove(orders_PriceShowRental);
			result = db.SaveChanges();
		}
		return result;
	}

	public List<Orders_PriceShowRental> GetPurchasesRental(int Code)
	{
		return db.Orders_PriceShowRental.Where((Orders_PriceShowRental x) => x.ID == Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Ordert_PriceShowRentalDetails> GetPurchasesRentalDetails(int PurchasesID)
	{
		return db.Ordert_PriceShowRentalDetails.Where((Ordert_PriceShowRentalDetails x) => x.Purchese_ID == (int?)PurchasesID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Orders_PriceShowRental> GetPurchases2()
	{
		return db.Orders_PriceShowRental.OrderByDescending((Orders_PriceShowRental o) => o.ID).ToList();
	}
}


