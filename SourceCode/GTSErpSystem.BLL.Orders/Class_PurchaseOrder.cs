using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.Orders;

internal class Class_PurchaseOrder
{
	private int CachCode = 0;

	private DataTable dtCurrentTable = new DataTable();

	private GTSdbEntities db = new GTSdbEntities();

	private int DecimalNum = LoginDetails.DecimalNum;

	public List<Account_AccountType> GetAccountType()
	{
		return db.Account_AccountType.ToList();
	}

	public List<Select_SearchAccountCostCenter_Result> GetCostCenters()
	{
		return db.Select_SearchAccountCostCenter(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<Select_SearchAccountCostCenterByProject_Result> GetCostCentersByProject(int projectId)
	{
		return db.Select_SearchAccountCostCenterByProject(projectId, Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<Order_PurchasesOrder> GetPurchases()
	{
		return db.Order_PurchasesOrder.Where((Order_PurchasesOrder x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Order_PurchasesOrder> GetPurchases(int Code)
	{
		return db.Order_PurchasesOrder.Where((Order_PurchasesOrder x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public string GetStoreName()
	{
		int StoreID = Convert.ToInt32((from x in db.TblSettings
			where x.ID == LoginDetails.BranchID
			select x.StoreID).SingleOrDefault());
		return (from x in db.Account_Stores
			where x.ID == StoreID
			select x.Store_Name).FirstOrDefault();
	}

	public int GetStoreID()
	{
		return Convert.ToInt32((from x in db.TblSettings
			where x.ID == LoginDetails.BranchID
			select x.StoreID).SingleOrDefault());
	}

	public bool IsVatActive()
	{
		return Convert.ToBoolean((from x in db.TblSettings
			where x.ID == LoginDetails.BranchID
			select x.IsVat).FirstOrDefault());
	}

	public decimal GetAvargeCost(string Itemcode)
	{
		decimal result = 0m;
		if (Itemcode != "")
		{
			return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcode || x.item_Name == Itemcode || x.item_Name_English == Itemcode || x.SmallBarCode1 == Itemcode || x.SmallBarCode2 == Itemcode || x.SmallBarCode3 == Itemcode || x.MediumBarCode1 == Itemcode || x.MediumBarCode2 == Itemcode || x.MediumBarCode3 == Itemcode || x.BigBarCode1 == Itemcode || x.BigBarCode2 == Itemcode || x.BigBarCode3 == Itemcode).FirstOrDefault().Average_cost);
		}
		return result;
	}

	public decimal GetSellprice1(string Itemcode)
	{
		decimal result = 0m;
		if (Itemcode != "")
		{
			return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcode || x.item_Name == Itemcode || x.item_Name_English == Itemcode || x.SmallBarCode1 == Itemcode || x.SmallBarCode2 == Itemcode || x.SmallBarCode3 == Itemcode || x.MediumBarCode1 == Itemcode || x.MediumBarCode2 == Itemcode || x.MediumBarCode3 == Itemcode || x.BigBarCode1 == Itemcode || x.BigBarCode2 == Itemcode || x.BigBarCode3 == Itemcode).FirstOrDefault().SellPriceSmall);
		}
		return result;
	}

	public decimal GetSellprice2(string Itemcode)
	{
		decimal result = 0m;
		if (Itemcode != "")
		{
			return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcode || x.item_Name == Itemcode || x.item_Name_English == Itemcode || x.SmallBarCode1 == Itemcode || x.SmallBarCode2 == Itemcode || x.SmallBarCode3 == Itemcode || x.MediumBarCode1 == Itemcode || x.MediumBarCode2 == Itemcode || x.MediumBarCode3 == Itemcode || x.BigBarCode1 == Itemcode || x.BigBarCode2 == Itemcode || x.BigBarCode3 == Itemcode).FirstOrDefault().SellPriceSmall2);
		}
		return result;
	}

	public decimal GetSellprice3(string Itemcode)
	{
		decimal result = 0m;
		if (Itemcode != "")
		{
			return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcode || x.item_Name == Itemcode || x.item_Name_English == Itemcode || x.SmallBarCode1 == Itemcode || x.SmallBarCode2 == Itemcode || x.SmallBarCode3 == Itemcode || x.MediumBarCode1 == Itemcode || x.MediumBarCode2 == Itemcode || x.MediumBarCode3 == Itemcode || x.BigBarCode1 == Itemcode || x.BigBarCode2 == Itemcode || x.BigBarCode3 == Itemcode).FirstOrDefault().SellPriceSmall3);
		}
		return result;
	}

	public List<Item_Items> GetItems(string Code)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Item_Items.Where((Item_Items x) => x.Item_code == Code || x.item_Name == Code || x.item_Name_English == Code || x.SmallBarCode1 == Code || x.SmallBarCode2 == Code || x.SmallBarCode3 == Code || x.MediumBarCode1 == Code || x.MediumBarCode2 == Code || x.MediumBarCode3 == Code || x.BigBarCode1 == Code || x.BigBarCode2 == Code || x.BigBarCode3 == Code).ToList();
	}

	public decimal GetQantityForItems(string itemCode, string unit)
	{
		decimal num = 0m;
		int Itemid = (from x in GetItems(itemCode)
			select x.ItemId).FirstOrDefault();
		decimal num2 = Convert.ToDecimal(db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)Itemid).Sum((ItemQuantity x) => x.OpeningBalance));
		decimal num3 = Convert.ToDecimal(db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)Itemid).Sum((ItemQuantity x) => x.CurrentBalance));
		num = num2 + num3;
		if (unit == "المتوسطة")
		{
			num /= Convert.ToDecimal((from x in GetItems(itemCode)
				select x.ConvertMediumUnit).FirstOrDefault());
		}
		if (unit == "الكبرى")
		{
			num /= Convert.ToDecimal((from x in GetItems(itemCode)
				select x.ConvertMediumUnit).FirstOrDefault());
			num /= Convert.ToDecimal((from x in GetItems(itemCode)
				select x.ConvertBigUnit).FirstOrDefault());
		}
		return num;
	}

	public List<Item_Items> GetItemsByitemid(int ItemID)
	{
		return db.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).ToList();
	}

	public List<Item_Items> GetItems()
	{
		return db.Item_Items.ToList();
	}

	public decimal GetQantityForItems(string itemCode, string unit, int storeid)
	{
		decimal num = 0m;
		int Itemid = (from x in GetItems(itemCode)
			select x.ItemId).FirstOrDefault();
		decimal num2 = Convert.ToDecimal(db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)Itemid && x.StoreID == (int?)storeid).Sum((ItemQuantity x) => x.OpeningBalance));
		decimal num3 = Convert.ToDecimal(db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)Itemid && x.StoreID == (int?)storeid).Sum((ItemQuantity x) => x.CurrentBalance));
		num = num2 + num3;
		if (unit == "المتوسطة")
		{
			num /= Convert.ToDecimal((from x in GetItems(itemCode)
				select x.ConvertMediumUnit).FirstOrDefault());
		}
		if (unit == "الكبرى")
		{
			num /= Convert.ToDecimal((from x in GetItems(itemCode)
				select x.ConvertMediumUnit).FirstOrDefault());
			num /= Convert.ToDecimal((from x in GetItems(itemCode)
				select x.ConvertBigUnit).FirstOrDefault());
		}
		return num;
	}

	public List<Order_PurchasesOrderDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.Order_PurchasesOrderDetails.Where((Order_PurchasesOrderDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_Order_PurchasesOrder_Result> GetSelectAllPurchases()
	{
		return db.Select_Order_PurchasesOrder(LoginDetails.BranchID).ToList();
	}

	public int GetUnitIdByName(string Nmae)
	{
		int num = 0;
		return (from x in db.Item_Unit
			where x.Name == Nmae
			select x.ID).FirstOrDefault();
	}

	public string GetUnitNameByID(int ID)
	{
		return (from x in db.Item_Unit
			where x.ID == ID
			select x.Name).FirstOrDefault();
	}

	public int InsertItems(DataTable Items, int Purchese_ID)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["كود_الصنف"].ToString() != "")
			{
				Order_PurchasesOrderDetails order_PurchasesOrderDetails = new Order_PurchasesOrderDetails();
				string Itemcod = row["كود_الصنف"].ToString();
				string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
				int ItemRowChek = Convert.ToInt32(value);
				Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
				int value2 = Convert.ToInt32(db.TblSettings.Select((TblSetting x) => x.StoreID).FirstOrDefault());
				if (item_Items != null)
				{
					order_PurchasesOrderDetails.Purchese_ID = Purchese_ID;
					order_PurchasesOrderDetails.BranchID = LoginDetails.BranchID;
					order_PurchasesOrderDetails.ItemID = Convert.ToInt32(item_Items.ItemId);
					string nmae = row["الوحدة"].ToString();
					order_PurchasesOrderDetails.ItemUnitID = GetUnitIdByName(nmae);
					order_PurchasesOrderDetails.SmallUnitPrice = Convert.ToDecimal(row["سعر الوحدة"].ToString());
					order_PurchasesOrderDetails.ItemNote = row["الشرح"].ToString();
					order_PurchasesOrderDetails.Quantity = Convert.ToDecimal(row["الكمية"].ToString());
					order_PurchasesOrderDetails.StoreID = value2;
					order_PurchasesOrderDetails.UnitPrice = Math.Round(Convert.ToDecimal(row["سعر الوحدة"].ToString()), DecimalNum);
					order_PurchasesOrderDetails.TotalPrice = Math.Round(Convert.ToDecimal(row["الاجمالى"].ToString()), DecimalNum);
					order_PurchasesOrderDetails.NetUnitPrice = Math.Round(Convert.ToDecimal(row["سعربعدالخصم"].ToString()), DecimalNum);
					order_PurchasesOrderDetails.NetTotalPrice = Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"].ToString()), DecimalNum);
					order_PurchasesOrderDetails.VAT = Math.Round(Convert.ToDecimal(row["الضربية"].ToString()), DecimalNum);
					order_PurchasesOrderDetails.ItemUnitType = row["أسم_الوحدة"].ToString();
					order_PurchasesOrderDetails.VAT_Discount = Math.Round(Convert.ToDecimal(row["الضريبه_بعد_الخصم"].ToString()), DecimalNum);
					order_PurchasesOrderDetails.StoreID = Convert.ToInt32(row["رقم المخزن"]);
					db.Order_PurchasesOrderDetails.Add(order_PurchasesOrderDetails);
					result = db.SaveChanges();
				}
			}
		}
		return result;
	}

	public int InsertPurch(out int Code, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, int PurchType, int CostCenter, string Note, string NoteNum, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, int ProjectId)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.Order_PurchasesOrder.Select((Order_PurchasesOrder p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Order_PurchasesOrder
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		Order_PurchasesOrder order_PurchasesOrder = new Order_PurchasesOrder();
		order_PurchasesOrder.ID = iD;
		CachCode = num2;
		Code = num2;
		order_PurchasesOrder.BranchID = LoginDetails.BranchID;
		order_PurchasesOrder.PurBranchID = num2;
		order_PurchasesOrder.SupplierID = SupplierID;
		order_PurchasesOrder.SupplierName = SupplierName;
		order_PurchasesOrder.SupplierPhone = SupplierPhone;
		order_PurchasesOrder.SupplierVatNum = SupplierVatNum;
		order_PurchasesOrder.Purchases_Date = Purchases_Date;
		order_PurchasesOrder.Order_Paymant_Type = PurchType;
		order_PurchasesOrder.CostCentersID = CostCenter;
		order_PurchasesOrder.Note = Note;
		order_PurchasesOrder.NoteNum = NoteNum;
		order_PurchasesOrder.TotalPrices = TotalPrices;
		order_PurchasesOrder.Tax = Tax;
		order_PurchasesOrder.Safy = Safy;
		order_PurchasesOrder.DiscountNum = DiscountNum;
		order_PurchasesOrder.DiscountPerantage = DiscountPerantage;
		order_PurchasesOrder.Tax_Discount = Tax_Discount;
		order_PurchasesOrder.TotalPrices_Discount = TotalPrices_Discount;
		order_PurchasesOrder.Net = Net;
		order_PurchasesOrder.ProjectId = ProjectId;
		order_PurchasesOrder.UserID_Add = LoginDetails.UserID;
		order_PurchasesOrder.UserBranch_Add = LoginDetails.BranchID;
		order_PurchasesOrder.UserMacAddress_Add = LoginDetails.macAddress;
		order_PurchasesOrder.UserDate_Add = DateTime.Now;
		db.Order_PurchasesOrder.Add(order_PurchasesOrder);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID)
	{
		List<Order_PurchasesOrderDetails> list = db.Order_PurchasesOrderDetails.Where((Order_PurchasesOrderDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Order_PurchasesOrderDetails item in list)
		{
			db.Order_PurchasesOrderDetails.Attach(item);
			db.Order_PurchasesOrderDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, int PurchType, int CostCenter, string Note, string NoteNum, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, int projectId)
	{
		int result = 0;
		Order_PurchasesOrder order_PurchasesOrder = db.Order_PurchasesOrder.Where((Order_PurchasesOrder x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.Order_PurchasesOrder.Where((Order_PurchasesOrder x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (order_PurchasesOrder != null)
		{
			order_PurchasesOrder.SupplierID = SupplierID;
			order_PurchasesOrder.SupplierName = SupplierName;
			order_PurchasesOrder.SupplierPhone = SupplierPhone;
			order_PurchasesOrder.SupplierVatNum = SupplierVatNum;
			order_PurchasesOrder.Purchases_Date = Purchases_Date;
			order_PurchasesOrder.Order_Paymant_Type = PurchType;
			order_PurchasesOrder.CostCentersID = CostCenter;
			order_PurchasesOrder.Note = Note;
			order_PurchasesOrder.NoteNum = NoteNum;
			order_PurchasesOrder.TotalPrices = TotalPrices;
			order_PurchasesOrder.Tax = Tax;
			order_PurchasesOrder.Safy = Safy;
			order_PurchasesOrder.DiscountNum = DiscountNum;
			order_PurchasesOrder.DiscountPerantage = DiscountPerantage;
			order_PurchasesOrder.Tax_Discount = Tax_Discount;
			order_PurchasesOrder.TotalPrices_Discount = TotalPrices_Discount;
			order_PurchasesOrder.Net = Net;
			order_PurchasesOrder.ProjectId = projectId;
			order_PurchasesOrder.UserID_Update = LoginDetails.UserID;
			order_PurchasesOrder.UserBranch_Update = LoginDetails.GroupID;
			order_PurchasesOrder.UserMacAddress_Update = LoginDetails.macAddress;
			order_PurchasesOrder.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public int Delete(int PurID)
	{
		int result = 0;
		Order_PurchasesOrder order_PurchasesOrder = db.Order_PurchasesOrder.Where((Order_PurchasesOrder x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (order_PurchasesOrder != null)
		{
			deleteItemsByPurcahesId(PurID);
			db.Order_PurchasesOrder.Attach(order_PurchasesOrder);
			db.Order_PurchasesOrder.Remove(order_PurchasesOrder);
			result = db.SaveChanges();
		}
		return result;
	}

	public int GetItmID(string Code)
	{
		return (from xx in db.Item_Items
			where xx.Item_code == Code
			select xx.ItemId).FirstOrDefault();
	}
}


