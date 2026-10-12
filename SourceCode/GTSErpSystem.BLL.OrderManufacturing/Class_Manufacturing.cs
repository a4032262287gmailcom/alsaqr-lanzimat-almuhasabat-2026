using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GTSErpSystem.BLL.Orders;

namespace GTSErpSystem.BLL.OrderManufacturing;

public class Class_Manufacturing
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

	public List<Order_Manufacturing> GetPurchases()
	{
		return db.Order_Manufacturing.Where((Order_Manufacturing x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Order_Manufacturing> GetPurchases(int Code)
	{
		return db.Order_Manufacturing.Where((Order_Manufacturing x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Order_ManufacturingOrder> GetPurchasesOrder(int Code)
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

	public List<Order_ManufacturingDetailsItems> GetItemsPurchesDetailsItems(int PurchasesID)
	{
		return db.Order_ManufacturingDetailsItems.Where((Order_ManufacturingDetailsItems X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Order_ManufacturingDetailsNew> GetItemsPurchesDetailsNew(int PurchasesID)
	{
		return db.Order_ManufacturingDetailsNew.Where((Order_ManufacturingDetailsNew X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_Order_Manufacturing_Result> GetSelectAllPurchases()
	{
		return db.Select_Order_Manufacturing(LoginDetails.BranchID).ToList();
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
			Order_ManufacturingDetailsItems order_ManufacturingDetailsItems = new Order_ManufacturingDetailsItems();
			string Itemcod = row["كود_الصنف"].ToString();
			if (string.IsNullOrEmpty(Itemcod))
			{
				continue;
			}
			Item_Items item_Items = db.Item_Items.FirstOrDefault((Item_Items x) => x.Item_code == Itemcod);
			AvergItemTable.Rows.Add(item_Items.ItemId);
			string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
			int ItemRowChek = Convert.ToInt32(value);
			Item_Items item_Items2 = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
			if (item_Items2 != null)
			{
				order_ManufacturingDetailsItems.Purchese_ID = Purchese_ID;
				order_ManufacturingDetailsItems.BranchID = LoginDetails.BranchID;
				order_ManufacturingDetailsItems.ItemID = Convert.ToInt32(item_Items2.ItemId);
				string nmae = row["الوحدة"].ToString();
				order_ManufacturingDetailsItems.ItemUnitID = GetUnitIdByName(nmae);
				order_ManufacturingDetailsItems.SmallUnitPrice = Convert.ToDecimal(row["سعر الوحدة"].ToString());
				order_ManufacturingDetailsItems.Quantity = Convert.ToDecimal(row["الكمية"].ToString());
				order_ManufacturingDetailsItems.QtyNumber = Convert.ToDecimal(row["عدد الوحدة"].ToString());
				order_ManufacturingDetailsItems.UnitPrice = Math.Round(Convert.ToDecimal(row["سعر الوحدة"].ToString()), DecimalNum);
				order_ManufacturingDetailsItems.TotalPrice = Math.Round(Convert.ToDecimal(row["الاجمالى"].ToString()), DecimalNum);
				order_ManufacturingDetailsItems.NetUnitPrice = Math.Round(Convert.ToDecimal(row["سعربعدالخصم"].ToString()), DecimalNum);
				order_ManufacturingDetailsItems.NetTotalPrice = Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"].ToString()), DecimalNum);
				order_ManufacturingDetailsItems.ItemUnitType = row["أسم_الوحدة"].ToString();
				order_ManufacturingDetailsItems.StoreID = Convert.ToInt32(row["رقم المخزن"]);
				db.Order_ManufacturingDetailsItems.Add(order_ManufacturingDetailsItems);
				result = db.SaveChanges();
				ItemQuantityOut(Convert.ToInt32(value), Convert.ToDecimal(row["الكمية"].ToString()), row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"].ToString()), Convert.ToDecimal(row["عدد الوحدة"].ToString()));
				AvergItemTable.Rows.Add(item_Items.ItemId);
				if (AvergItemTable.Rows.Count > 0)
				{
					ORDR.ExecuteBase(AvergItemTable);
				}
			}
		}
		return result;
	}

	public int InsertItemsNew(DataTable Items, int Purchese_ID)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["كود_الصنف"].ToString() != "")
			{
				Order_ManufacturingDetailsNew order_ManufacturingDetailsNew = new Order_ManufacturingDetailsNew();
				string Itemcod = row["كود_الصنف"].ToString();
				string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
				int ItemRowChek = Convert.ToInt32(value);
				Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
				if (item_Items != null)
				{
					order_ManufacturingDetailsNew.Purchese_ID = Purchese_ID;
					order_ManufacturingDetailsNew.BranchID = LoginDetails.BranchID;
					order_ManufacturingDetailsNew.ItemID = Convert.ToInt32(item_Items.ItemId);
					string nmae = row["الوحدة"].ToString();
					order_ManufacturingDetailsNew.ItemUnitID = GetUnitIdByName(nmae);
					order_ManufacturingDetailsNew.SmallUnitPrice = Convert.ToDecimal(row["سعر التكلفة"].ToString());
					order_ManufacturingDetailsNew.PerProfit = Convert.ToDecimal(row["نسبة الربح"].ToString());
					order_ManufacturingDetailsNew.AmountProfit = Convert.ToDecimal(row["قيمة الربح"].ToString());
					order_ManufacturingDetailsNew.SalePrice = Convert.ToDecimal(row["سعر البيع"].ToString());
					order_ManufacturingDetailsNew.TotalSalePrice = Convert.ToDecimal(row["اجمالى البيع"].ToString());
					order_ManufacturingDetailsNew.Quantity = Convert.ToDecimal(row["الكمية"].ToString());
					order_ManufacturingDetailsNew.Percentage = Convert.ToDecimal(row["النسبة"].ToString());
					order_ManufacturingDetailsNew.UnitPrice = Math.Round(Convert.ToDecimal(row["سعر التكلفة"].ToString()), DecimalNum);
					order_ManufacturingDetailsNew.TotalPrice = Math.Round(Convert.ToDecimal(row["الاجمالى"].ToString()), DecimalNum);
					order_ManufacturingDetailsNew.ItemUnitType = row["أسم_الوحدة"].ToString();
					order_ManufacturingDetailsNew.StoreID = Convert.ToInt32(row["رقم المخزن"]);
					db.Order_ManufacturingDetailsNew.Add(order_ManufacturingDetailsNew);
					result = db.SaveChanges();
					ItemQuantityIn(Convert.ToInt32(value), Convert.ToDecimal(row["الكمية"].ToString()), row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"].ToString()), 0m);
				}
			}
		}
		foreach (DataRow row2 in Items.Rows)
		{
			string Itemcod2 = row2["كود_الصنف"].ToString();
			string value2 = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod2).FirstOrDefault().ItemId);
			int ItemRowChek2 = Convert.ToInt32(value2);
			Item_Items item_Items2 = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek2).FirstOrDefault();
			if (item_Items2 != null)
			{
				Get_Item_AvargeCost(Itemcod2, row2["أسم_الوحدة"].ToString(), Math.Round(Convert.ToDecimal(row2["سعر التكلفة"].ToString()), DecimalNum));
			}
		}
		return result;
	}

	public int InsertPurch(out int Code, DateTime Purchases_Date, int CostCenter, int ProjectId, string Note, string NoteNum, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.Order_Manufacturing.Select((Order_Manufacturing p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Order_Manufacturing
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		Order_Manufacturing order_Manufacturing = new Order_Manufacturing();
		order_Manufacturing.ID = iD;
		CachCode = num2;
		Code = num2;
		order_Manufacturing.BranchID = LoginDetails.BranchID;
		order_Manufacturing.PurBranchID = num2;
		order_Manufacturing.Purchases_Date = Purchases_Date;
		order_Manufacturing.CostCentersID = CostCenter;
		order_Manufacturing.ProjectId = ProjectId;
		order_Manufacturing.Note = Note;
		order_Manufacturing.NoteNum = NoteNum;
		order_Manufacturing.TotalPrices = TotalPrices;
		order_Manufacturing.Tax = Tax;
		order_Manufacturing.Safy = Safy;
		order_Manufacturing.DiscountNum = DiscountNum;
		order_Manufacturing.DiscountPerantage = DiscountPerantage;
		order_Manufacturing.Tax_Discount = Tax_Discount;
		order_Manufacturing.TotalPrices_Discount = TotalPrices_Discount;
		order_Manufacturing.Net = Net;
		order_Manufacturing.UserID_Add = LoginDetails.UserID;
		order_Manufacturing.UserBranch_Add = LoginDetails.BranchID;
		order_Manufacturing.UserMacAddress_Add = LoginDetails.macAddress;
		order_Manufacturing.UserDate_Add = DateTime.Now;
		db.Order_Manufacturing.Add(order_Manufacturing);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID)
	{
		List<Order_ManufacturingDetailsItems> list = db.Order_ManufacturingDetailsItems.Where((Order_ManufacturingDetailsItems x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list != null)
		{
			foreach (Order_ManufacturingDetailsItems item in list)
			{
				ItemQuantityIn(Convert.ToInt32(item.ItemID), Convert.ToDecimal(item.Quantity), item.ItemUnitType, Convert.ToInt32(item.StoreID), Convert.ToDecimal(item.QtyNumber));
				db.Order_ManufacturingDetailsItems.Attach(item);
				db.Order_ManufacturingDetailsItems.Remove(item);
			}
			db.SaveChanges();
		}
		List<Order_ManufacturingDetailsNew> list2 = db.Order_ManufacturingDetailsNew.Where((Order_ManufacturingDetailsNew x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list2 == null)
		{
			return;
		}
		foreach (Order_ManufacturingDetailsNew item2 in list2)
		{
			ItemQuantityOut(Convert.ToInt32(item2.ItemID), Convert.ToDecimal(item2.Quantity), item2.ItemUnitType, Convert.ToInt32(item2.StoreID), 0m);
			db.Order_ManufacturingDetailsNew.Attach(item2);
			db.Order_ManufacturingDetailsNew.Remove(item2);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, DateTime Purchases_Date, int CostCenter, int ProjectId, string Note, string NoteNum, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net)
	{
		int result = 0;
		Order_Manufacturing order_Manufacturing = db.Order_Manufacturing.Where((Order_Manufacturing x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.Order_Manufacturing.Where((Order_Manufacturing x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (order_Manufacturing != null)
		{
			order_Manufacturing.Purchases_Date = Purchases_Date;
			order_Manufacturing.CostCentersID = CostCenter;
			order_Manufacturing.ProjectId = ProjectId;
			order_Manufacturing.Note = Note;
			order_Manufacturing.NoteNum = NoteNum;
			order_Manufacturing.TotalPrices = TotalPrices;
			order_Manufacturing.Tax = Tax;
			order_Manufacturing.Safy = Safy;
			order_Manufacturing.DiscountNum = DiscountNum;
			order_Manufacturing.DiscountPerantage = DiscountPerantage;
			order_Manufacturing.Tax_Discount = Tax_Discount;
			order_Manufacturing.TotalPrices_Discount = TotalPrices_Discount;
			order_Manufacturing.Net = Net;
			order_Manufacturing.UserID_Update = LoginDetails.UserID;
			order_Manufacturing.UserBranch_Update = LoginDetails.GroupID;
			order_Manufacturing.UserMacAddress_Update = LoginDetails.macAddress;
			order_Manufacturing.UserDate_Update = DateTime.Now;
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
		Order_Manufacturing order_Manufacturing = db.Order_Manufacturing.Where((Order_Manufacturing x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (order_Manufacturing != null)
		{
			deleteItemsByPurcahesId(PurID);
			db.Order_Manufacturing.Attach(order_Manufacturing);
			db.Order_Manufacturing.Remove(order_Manufacturing);
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

	public void Get_Item_AvargeCost(string itemcode, string unittype, decimal Lastcost)
	{
		try
		{
			using GTSdbEntities gTSdbEntities = new GTSdbEntities();
			Item_Items ItemDtails = gTSdbEntities.Item_Items.Where((Item_Items y) => y.Item_code == itemcode).FirstOrDefault();
			if (ItemDtails == null)
			{
				return;
			}
			decimal num = 0m;
			decimal num2 = 0m;
			decimal num3 = 0m;
			decimal num4 = Convert.ToDecimal(gTSdbEntities.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "الصغرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.Quantity));
			decimal num5 = Convert.ToDecimal(gTSdbEntities.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "المتوسطة" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.Quantity));
			decimal num6 = Convert.ToDecimal(gTSdbEntities.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "الكبرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.Quantity));
			decimal num7 = Convert.ToDecimal(gTSdbEntities.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "الصغرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.NetTotalPrice));
			decimal num8 = Convert.ToDecimal(gTSdbEntities.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "المتوسطة" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.NetTotalPrice));
			decimal num9 = Convert.ToDecimal(gTSdbEntities.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "الكبرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.NetTotalPrice));
			decimal value = num4;
			decimal value2 = num5;
			decimal? convertMediumUnit = ItemDtails.ConvertMediumUnit;
			decimal? num10 = (decimal?)value2 * convertMediumUnit;
			decimal? num11 = (decimal?)value + num10;
			value = num6;
			decimal? convertBigUnit = ItemDtails.ConvertBigUnit;
			num3 = Convert.ToDecimal(num11 + (decimal?)value * convertBigUnit * ItemDtails.ConvertMediumUnit);
			num2 = Convert.ToDecimal(num7 + num8 + num9);
			num = ((!(num3 == 0m)) ? (num2 / num3) : Lastcost);
			ItemDtails.Average_cost = num;
			if (unittype == "الصغرى")
			{
				ItemDtails.LastCost = Lastcost;
				if (ItemDtails.UnitMedium > 0)
				{
					Item_Items item_Items = ItemDtails;
					value = Lastcost;
					decimal? convertMediumUnit2 = ItemDtails.ConvertMediumUnit;
					item_Items.LastCost2 = (decimal?)value * convertMediumUnit2;
				}
				if (ItemDtails.UnitLarge > 0)
				{
					Item_Items item_Items2 = ItemDtails;
					value = Lastcost;
					convertMediumUnit = ItemDtails.ConvertMediumUnit;
					item_Items2.LastCost3 = (decimal?)value * convertMediumUnit * ItemDtails.ConvertBigUnit;
				}
			}
			if (unittype == "المتوسطة")
			{
				ItemDtails.LastCost2 = Lastcost;
				Item_Items item_Items3 = ItemDtails;
				value = Lastcost;
				num11 = ItemDtails.ConvertMediumUnit;
				item_Items3.LastCost = (decimal?)value / num11;
				if (ItemDtails.UnitLarge > 0)
				{
					Item_Items item_Items4 = ItemDtails;
					value = Lastcost;
					num11 = ItemDtails.ConvertBigUnit;
					item_Items4.LastCost3 = (decimal?)value * num11;
				}
			}
			if (unittype == "الكبرى")
			{
				ItemDtails.LastCost3 = Lastcost;
				Item_Items item_Items5 = ItemDtails;
				value = Lastcost;
				num11 = ItemDtails.ConvertBigUnit;
				item_Items5.LastCost2 = (decimal?)value / num11;
				Item_Items item_Items6 = ItemDtails;
				value = Lastcost;
				convertMediumUnit = ItemDtails.ConvertBigUnit;
				item_Items6.LastCost = (decimal?)value / convertMediumUnit / ItemDtails.ConvertMediumUnit;
			}
			gTSdbEntities.SaveChanges();
		}
		catch (Exception)
		{
		}
	}

	public int ItemQuantityIn(int ItemID, decimal Quantity, string unitType, int StoreID, decimal UnitNumber)
	{
		int result = 0;
		if (ItemID > 0)
		{
			ItemQuantity itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
			Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault();
			if (itemQuantity != null)
			{
				ItemQuantity itemQuantity2 = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
				if (itemQuantity2 != null)
				{
					if (unitType == "الصغرى")
					{
						itemQuantity2.CurrentBalance += (decimal?)Quantity;
						itemQuantity2.UnitNumber += (decimal?)UnitNumber;
						result = db.SaveChanges();
					}
					if (unitType == "المتوسطة")
					{
						decimal? currentBalance = itemQuantity2.CurrentBalance;
						decimal value = Quantity;
						decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
						itemQuantity2.CurrentBalance = currentBalance + (decimal?)value * convertMediumUnit;
						itemQuantity2.UnitNumber += (decimal?)UnitNumber;
						result = db.SaveChanges();
					}
					if (unitType == "الكبرى")
					{
						decimal? currentBalance2 = itemQuantity2.CurrentBalance;
						decimal value = Quantity;
						decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
						itemQuantity2.CurrentBalance = currentBalance2 + (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
						itemQuantity2.UnitNumber += (decimal?)UnitNumber;
						result = db.SaveChanges();
					}
				}
				return result;
			}
			ItemQuantity itemQuantity3 = new ItemQuantity();
			int itemQuantityID = Convert.ToInt32(db.ItemQuantities.Select((ItemQuantity p) => p.ItemQuantityID).DefaultIfEmpty(0).Max()) + 1;
			itemQuantity3.ItemQuantityID = itemQuantityID;
			itemQuantity3.ItemID = ItemID;
			if (unitType == "الصغرى")
			{
				itemQuantity3.CurrentBalance = Quantity;
			}
			if (unitType == "المتوسطة")
			{
				decimal value = Quantity;
				decimal? currentBalance = item_Items.ConvertMediumUnit;
				itemQuantity3.CurrentBalance = (decimal?)value * currentBalance;
			}
			if (unitType == "الكبرى")
			{
				decimal value = Quantity;
				decimal? convertMediumUnit3 = item_Items.ConvertMediumUnit;
				itemQuantity3.CurrentBalance = (decimal?)value * convertMediumUnit3 * item_Items.ConvertBigUnit;
			}
			itemQuantity3.UnitNumber = UnitNumber;
			itemQuantity3.BeginningInventory = 0m;
			itemQuantity3.BeginningInventoryPrice = 0m;
			itemQuantity3.OpeningBalance = 0m;
			itemQuantity3.StoreID = StoreID;
			db.ItemQuantities.Add(itemQuantity3);
			result = db.SaveChanges();
		}
		return result;
	}

	public int ItemQuantityOut(int ItemID, decimal Quantity, string unitType, int StoreID, decimal UnitNumber)
	{
		int result = 0;
		if (ItemID > 0)
		{
			ItemQuantity itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
			Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault();
			if (itemQuantity != null)
			{
				ItemQuantity itemQuantity2 = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
				if (unitType == "الصغرى")
				{
					itemQuantity2.CurrentBalance -= (decimal?)Quantity;
					itemQuantity2.UnitNumber -= (decimal?)UnitNumber;
					result = db.SaveChanges();
				}
				if (unitType == "المتوسطة")
				{
					decimal? currentBalance = itemQuantity2.CurrentBalance;
					decimal value = Quantity;
					decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
					itemQuantity2.CurrentBalance = currentBalance - (decimal?)value * convertMediumUnit;
					itemQuantity2.UnitNumber -= (decimal?)UnitNumber;
					result = db.SaveChanges();
				}
				if (unitType == "الكبرى")
				{
					decimal? currentBalance2 = itemQuantity2.CurrentBalance;
					decimal value = Quantity;
					decimal? convertBigUnit = item_Items.ConvertBigUnit;
					itemQuantity2.CurrentBalance = currentBalance2 - (decimal?)value * convertBigUnit * item_Items.ConvertMediumUnit;
					itemQuantity2.UnitNumber -= (decimal?)UnitNumber;
					result = db.SaveChanges();
				}
				return result;
			}
			ItemQuantity itemQuantity3 = new ItemQuantity();
			int itemQuantityID = Convert.ToInt32(db.ItemQuantities.Select((ItemQuantity p) => p.ItemQuantityID).DefaultIfEmpty(0).Max()) + 1;
			itemQuantity3.ItemQuantityID = itemQuantityID;
			itemQuantity3.ItemID = ItemID;
			if (unitType == "الصغرى")
			{
				itemQuantity3.CurrentBalance = -Quantity;
			}
			if (unitType == "المتوسطة")
			{
				decimal value = Quantity;
				decimal? currentBalance = item_Items.ConvertMediumUnit;
				itemQuantity3.CurrentBalance = -((decimal?)value * currentBalance);
			}
			if (unitType == "الكبرى")
			{
				decimal value = Quantity;
				decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
				itemQuantity3.CurrentBalance = -((decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit);
			}
			itemQuantity3.UnitNumber = -UnitNumber;
			itemQuantity3.BeginningInventory = 0m;
			itemQuantity3.BeginningInventoryPrice = 0m;
			itemQuantity3.OpeningBalance = 0m;
			itemQuantity3.StoreID = StoreID;
			db.ItemQuantities.Add(itemQuantity3);
			result = db.SaveChanges();
		}
		return result;
	}
}


