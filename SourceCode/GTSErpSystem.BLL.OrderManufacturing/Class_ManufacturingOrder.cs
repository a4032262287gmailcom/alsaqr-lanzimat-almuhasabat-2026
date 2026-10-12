using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GTSErpSystem.BLL.Orders;

namespace GTSErpSystem.BLL.OrderManufacturing;

public class Class_ManufacturingOrder
{
	private int CachCode = 0;

	private DataTable dtCurrentTable = new DataTable();

	private GTSdbEntities db = new GTSdbEntities();

	private int DecimalNum = LoginDetails.DecimalNum;

	private DataTable AvergItemTable = new DataTable();

	private Class_OrdersReturn ORDR = new Class_OrdersReturn();

	public List<Select_SearchAccountCostCenter_Result> GetCostCenters()
	{
		return db.Select_SearchAccountCostCenter(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<Order_ManufacturingOrder> GetPurchases()
	{
		return db.Order_ManufacturingOrder.Where((Order_ManufacturingOrder x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Order_ManufacturingOrder> GetPurchases(int Code)
	{
		return db.Order_ManufacturingOrder.Where((Order_ManufacturingOrder x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
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

	public List<Order_ManufacturingOrderDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.Order_ManufacturingOrderDetails.Where((Order_ManufacturingOrderDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_Order_ManufacturingOrder_Result> GetSelectAllPurchases()
	{
		return db.Select_Order_ManufacturingOrder(LoginDetails.BranchID).ToList();
	}

	public int GetUnitIdByName(string Nmae)
	{
		int num = 0;
		return (from x in db.Item_Unit
			where x.Name == Nmae
			select x.ID).FirstOrDefault();
	}

	public int InsertItems(DataTable Items, int Purchese_ID)
	{
		int result = 0;
		if (!AvergItemTable.Columns.Contains("ItemId"))
		{
			AvergItemTable.Columns.Add("ItemId", typeof(int));
		}
		AvergItemTable.Clear();
		foreach (DataRow row in Items.Rows)
		{
			if (!(row["كود_الصنف"].ToString() != ""))
			{
				continue;
			}
			Order_ManufacturingOrderDetails order_ManufacturingOrderDetails = new Order_ManufacturingOrderDetails();
			string Itemcod = row["كود_الصنف"].ToString();
			if (!string.IsNullOrEmpty(Itemcod))
			{
				Item_Items item_Items = db.Item_Items.FirstOrDefault((Item_Items x) => x.Item_code == Itemcod);
				AvergItemTable.Rows.Add(item_Items.ItemId);
				string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
				int ItemRowChek = Convert.ToInt32(value);
				Item_Items item_Items2 = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
				int value2 = Convert.ToInt32(db.TblSettings.Select((TblSetting x) => x.StoreID).FirstOrDefault());
				if (item_Items2 != null)
				{
					order_ManufacturingOrderDetails.Purchese_ID = Purchese_ID;
					order_ManufacturingOrderDetails.BranchID = LoginDetails.BranchID;
					order_ManufacturingOrderDetails.ItemID = Convert.ToInt32(item_Items2.ItemId);
					string nmae = row["الوحدة"].ToString();
					order_ManufacturingOrderDetails.ItemUnitID = GetUnitIdByName(nmae);
					order_ManufacturingOrderDetails.SmallUnitPrice = Convert.ToDecimal(row["سعر الوحدة"].ToString());
					order_ManufacturingOrderDetails.Quantity = Convert.ToDecimal(row["الكمية"].ToString());
					order_ManufacturingOrderDetails.StoreID = value2;
					order_ManufacturingOrderDetails.UnitPrice = Math.Round(Convert.ToDecimal(row["سعر الوحدة"].ToString()), DecimalNum);
					order_ManufacturingOrderDetails.TotalPrice = Math.Round(Convert.ToDecimal(row["الاجمالى"].ToString()), DecimalNum);
					order_ManufacturingOrderDetails.NetUnitPrice = Math.Round(Convert.ToDecimal(row["سعربعدالخصم"].ToString()), DecimalNum);
					order_ManufacturingOrderDetails.NetTotalPrice = Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"].ToString()), DecimalNum);
					order_ManufacturingOrderDetails.VAT = Math.Round(Convert.ToDecimal(row["الضربية"].ToString()), DecimalNum);
					order_ManufacturingOrderDetails.ItemUnitType = row["أسم_الوحدة"].ToString();
					order_ManufacturingOrderDetails.VAT_Discount = Math.Round(Convert.ToDecimal(row["الضريبه_بعد_الخصم"].ToString()), DecimalNum);
					order_ManufacturingOrderDetails.StoreID = Convert.ToInt32(row["رقم المخزن"]);
					db.Order_ManufacturingOrderDetails.Add(order_ManufacturingOrderDetails);
					result = db.SaveChanges();
				}
				AvergItemTable.Rows.Add(item_Items.ItemId);
				if (AvergItemTable.Rows.Count > 0)
				{
					ORDR.ExecuteBase(AvergItemTable);
				}
			}
		}
		return result;
	}

	public int InsertPurch(out int Code, DateTime Purchases_Date, int CostCenter, string Note, string NoteNum, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.Order_ManufacturingOrder.Select((Order_ManufacturingOrder p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Order_ManufacturingOrder
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		Order_ManufacturingOrder order_ManufacturingOrder = new Order_ManufacturingOrder();
		order_ManufacturingOrder.ID = iD;
		CachCode = num2;
		Code = num2;
		order_ManufacturingOrder.BranchID = LoginDetails.BranchID;
		order_ManufacturingOrder.PurBranchID = num2;
		order_ManufacturingOrder.Purchases_Date = Purchases_Date;
		order_ManufacturingOrder.CostCentersID = CostCenter;
		order_ManufacturingOrder.Note = Note;
		order_ManufacturingOrder.NoteNum = NoteNum;
		order_ManufacturingOrder.TotalPrices = TotalPrices;
		order_ManufacturingOrder.Tax = Tax;
		order_ManufacturingOrder.Safy = Safy;
		order_ManufacturingOrder.DiscountNum = DiscountNum;
		order_ManufacturingOrder.DiscountPerantage = DiscountPerantage;
		order_ManufacturingOrder.Tax_Discount = Tax_Discount;
		order_ManufacturingOrder.TotalPrices_Discount = TotalPrices_Discount;
		order_ManufacturingOrder.Net = Net;
		order_ManufacturingOrder.UserID_Add = LoginDetails.UserID;
		order_ManufacturingOrder.UserBranch_Add = LoginDetails.BranchID;
		order_ManufacturingOrder.UserMacAddress_Add = LoginDetails.macAddress;
		order_ManufacturingOrder.UserDate_Add = DateTime.Now;
		db.Order_ManufacturingOrder.Add(order_ManufacturingOrder);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID)
	{
		List<Order_ManufacturingOrderDetails> list = db.Order_ManufacturingOrderDetails.Where((Order_ManufacturingOrderDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Order_ManufacturingOrderDetails item in list)
		{
			db.Order_ManufacturingOrderDetails.Attach(item);
			db.Order_ManufacturingOrderDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, DateTime Purchases_Date, int CostCenter, string Note, string NoteNum, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net)
	{
		int result = 0;
		Order_ManufacturingOrder order_ManufacturingOrder = db.Order_ManufacturingOrder.Where((Order_ManufacturingOrder x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.Order_ManufacturingOrder.Where((Order_ManufacturingOrder x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (order_ManufacturingOrder != null)
		{
			order_ManufacturingOrder.Purchases_Date = Purchases_Date;
			order_ManufacturingOrder.CostCentersID = CostCenter;
			order_ManufacturingOrder.Note = Note;
			order_ManufacturingOrder.NoteNum = NoteNum;
			order_ManufacturingOrder.TotalPrices = TotalPrices;
			order_ManufacturingOrder.Tax = Tax;
			order_ManufacturingOrder.Safy = Safy;
			order_ManufacturingOrder.DiscountNum = DiscountNum;
			order_ManufacturingOrder.DiscountPerantage = DiscountPerantage;
			order_ManufacturingOrder.Tax_Discount = Tax_Discount;
			order_ManufacturingOrder.TotalPrices_Discount = TotalPrices_Discount;
			order_ManufacturingOrder.Net = Net;
			order_ManufacturingOrder.UserID_Update = LoginDetails.UserID;
			order_ManufacturingOrder.UserBranch_Update = LoginDetails.GroupID;
			order_ManufacturingOrder.UserMacAddress_Update = LoginDetails.macAddress;
			order_ManufacturingOrder.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public int Delete(int PurID, DataTable Items)
	{
		if (!AvergItemTable.Columns.Contains("ItemId"))
		{
			AvergItemTable.Columns.Add("ItemId", typeof(int));
		}
		int result = 0;
		Order_ManufacturingOrder order_ManufacturingOrder = db.Order_ManufacturingOrder.Where((Order_ManufacturingOrder x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (order_ManufacturingOrder != null)
		{
			deleteItemsByPurcahesId(PurID);
			db.Order_ManufacturingOrder.Attach(order_ManufacturingOrder);
			db.Order_ManufacturingOrder.Remove(order_ManufacturingOrder);
			result = db.SaveChanges();
		}
		AvergItemTable.Clear();
		foreach (DataRow row in Items.Rows)
		{
			string Itemcod = row["كود_الصنف"].ToString();
			if (!string.IsNullOrEmpty(Itemcod))
			{
				Item_Items item_Items = db.Item_Items.FirstOrDefault((Item_Items x) => x.Item_code == Itemcod);
				AvergItemTable.Rows.Add(item_Items.ItemId);
			}
		}
		if (AvergItemTable.Rows.Count > 0)
		{
			ORDR.ExecuteBase(AvergItemTable);
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


