using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GTSErpSystem.BLL.Orders;

internal class Class_Purchases
{
	private int CachCode = 0;

	private DataTable dtCurrentTable = new DataTable();

	private GTSdbEntities db = new GTSdbEntities();

	private SqlConnection con = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);

	private int DecimalNum = LoginDetails.DecimalNum;

	public List<Account_AccountType> GetAccountType()
	{
		return db.Account_AccountType.ToList();
	}

	public List<Select_SearchAccountCostCenter_Result> GetCostCenters()
	{
		return db.Select_SearchAccountCostCenter(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<Order_Purchases> GetPurchases()
	{
		return db.Order_Purchases.Where((Order_Purchases x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Order_Purchases> GetPurchases(int Code)
	{
		return db.Order_Purchases.Where((Order_Purchases x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int InsertItems_Complant(DataTable Items, string ItemCode)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["كود_الصنف"].ToString() != "")
			{
				Item_ItemComponent item_ItemComponent = new Item_ItemComponent();
				string Itemcod = row["كود_الصنف"].ToString();
				string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == ItemCode).FirstOrDefault().ItemId);
				string value2 = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
				int ItemRowChek = Convert.ToInt32(value2);
				Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
				if (item_Items != null)
				{
					item_ItemComponent.ItemID_Master = Convert.ToInt32(value);
					item_ItemComponent.ItemID_Complant = Convert.ToInt32(item_Items.ItemId);
					item_ItemComponent.Amount = Convert.ToDecimal(row["الكمية"].ToString());
					string nmae = row["الوحدة"].ToString();
					item_ItemComponent.ItemUnitID = GetUnitIdByName(nmae);
					item_ItemComponent.SmallUnitPrice = Convert.ToDecimal(row["ظ….التكلفة"].ToString());
					item_ItemComponent.TotalPrice = Math.Round(Convert.ToDecimal(row["الاجمالى"].ToString()), DecimalNum);
					item_ItemComponent.ItemUnitType = row["أسم_الوحدة"].ToString();
					item_ItemComponent.StoreID = Convert.ToInt32(row["رقم المخزن"]);
					db.Item_ItemComponent.Add(item_ItemComponent);
					result = db.SaveChanges();
				}
			}
		}
		return result;
	}

	public int ItemQuantityIn(int ItemID, decimal Quantity, string unitType, int StoreID, DateTime ExpireDate, decimal UnitNumber)
	{
		int result = 0;
		if (ItemID > 0)
		{
			DateTime dateTime = Convert.ToDateTime(ExpireDate.ToString("d"));
			ItemQuantity itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
			if (LoginDetails.IsPharmacy)
			{
				itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
			}
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

	public int ItemQuantityOut(int ItemID, decimal Quantity, string unitType, int StoreID, decimal UnitNumber)
	{
		int result = 0;
		if (ItemID > 0)
		{
			ItemQuantity itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
			if (LoginDetails.IsPharmacy)
			{
				itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
			}
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
		List<Order_PurchasesDetails> entities = new List<Order_PurchasesDetails>();
		gTSdbEntities.Order_PurchasesDetails.AddRange(entities);
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

	public List<Order_PurchasesDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.Order_PurchasesDetails.Where((Order_PurchasesDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_Order_Purchases_Result> GetSelectAllPurchases()
	{
		return db.Select_Order_Purchases(LoginDetails.BranchID).ToList();
	}

	public int GetUnitIdByName(string Nmae)
	{
		int num = 0;
		return (from x in db.Item_Unit
			where x.Name == Nmae
			select x.ID).FirstOrDefault();
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

	public int GetItmID(string Code)
	{
		return (from xx in db.Item_Items
			where xx.Item_code == Code
			select xx.ItemId).FirstOrDefault();
	}

	public List<Get_Item_ByCodeALL_Result> GetItemByCode(string Code)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Get_Item_ByCodeALL(Code, LoginDetails.BranchID).ToList();
	}

	public List<Get_Item_BySearchPurchases_Result> Get_Item_BySearchPurchases(int Purchese_ID)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Get_Item_BySearchPurchases(Purchese_ID, LoginDetails.BranchID).ToList();
	}

	public List<GetAllitems_Alternatives_Result> GetAllitems_Alternatives(int ItmID, int StoreID, int UnitType)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.GetAllitems_Alternatives(ItmID, StoreID, UnitType).ToList();
	}

	public int InsertOrders(out int Code, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, int PurchType, int CostCenter, string Note, string NoteNum, decimal Tax, decimal TotalPrices, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal Cash, decimal Bank, int Acc_Cash, int Acc_Bank, DataTable Items, decimal Charge, int ProjectId)
	{
		using (SqlCommand sqlCommand = new SqlCommand("Insert_Order_Purchases", con))
		{
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = LoginDetails.BranchID;
			sqlCommand.Parameters.Add("@SupplierID", SqlDbType.Int).Value = SupplierID;
			sqlCommand.Parameters.Add("@SupplierName", SqlDbType.NVarChar).Value = SupplierName;
			sqlCommand.Parameters.Add("@SupplierPhone", SqlDbType.NVarChar).Value = SupplierPhone;
			sqlCommand.Parameters.Add("@SupplierVatNum", SqlDbType.NVarChar).Value = SupplierVatNum;
			sqlCommand.Parameters.Add("@Purchases_Date", SqlDbType.DateTime).Value = Purchases_Date;
			sqlCommand.Parameters.Add("@Order_Paymant_Type", SqlDbType.Int).Value = PurchType;
			sqlCommand.Parameters.Add("@CostCentersID", SqlDbType.Int).Value = CostCenter;
			sqlCommand.Parameters.Add("@Note", SqlDbType.NVarChar).Value = Note;
			sqlCommand.Parameters.Add("@NoteNum", SqlDbType.NVarChar).Value = NoteNum;
			sqlCommand.Parameters.Add("@Tax", SqlDbType.Decimal).Value = Tax;
			sqlCommand.Parameters.Add("@TotalPrices", SqlDbType.Decimal).Value = TotalPrices;
			sqlCommand.Parameters.Add("@Safy", SqlDbType.Decimal).Value = Safy;
			sqlCommand.Parameters.Add("@DiscountNum", SqlDbType.Decimal).Value = DiscountNum;
			sqlCommand.Parameters.Add("@DiscountPerantage", SqlDbType.Decimal).Value = DiscountPerantage;
			sqlCommand.Parameters.Add("@Tax_Discount", SqlDbType.Decimal).Value = Tax_Discount;
			sqlCommand.Parameters.Add("@TotalPrices_Discount", SqlDbType.Decimal).Value = TotalPrices_Discount;
			sqlCommand.Parameters.Add("@Net", SqlDbType.Decimal).Value = Net;
			sqlCommand.Parameters.Add("@Cash", SqlDbType.Decimal).Value = Cash;
			sqlCommand.Parameters.Add("@Bank", SqlDbType.Decimal).Value = Bank;
			sqlCommand.Parameters.Add("@Acc_Cash", SqlDbType.Int).Value = Acc_Cash;
			sqlCommand.Parameters.Add("@Acc_Bank", SqlDbType.Int).Value = Acc_Bank;
			sqlCommand.Parameters.Add("@UserID_Add", SqlDbType.Int).Value = LoginDetails.UserID;
			sqlCommand.Parameters.Add("@UserBranch_Add", SqlDbType.Int).Value = LoginDetails.BranchID;
			sqlCommand.Parameters.Add("@UserMacAddress_Add", SqlDbType.NVarChar).Value = LoginDetails.macAddress;
			sqlCommand.Parameters.Add("@SalesMan", SqlDbType.NVarChar).Value = LoginDetails.UserName;
			sqlCommand.Parameters.Add("@Charge", SqlDbType.Decimal).Value = Charge;
			sqlCommand.Parameters.Add("@ProjectId", SqlDbType.Int).Value = ProjectId;
			DataTable changes = Items.GetChanges(DataRowState.Added);
			sqlCommand.Parameters.AddWithValue("@Items", changes);
			SqlParameter sqlParameter = sqlCommand.Parameters.Add("@Return Value", SqlDbType.Int);
			sqlParameter.Direction = ParameterDirection.ReturnValue;
			sqlCommand.CommandTimeout = 999999;
			if (con.State == ConnectionState.Closed)
			{
				con.Open();
			}
			Code = sqlCommand.ExecuteNonQuery();
			object value = sqlParameter.Value;
			con.Close();
			Code = Convert.ToInt32(value);
		}
		return Code;
	}

	public int UpdateOrders(int Code, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, int PurchType, int CostCenter, string Note, string NoteNum, decimal Tax, decimal TotalPrices, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal Cash, decimal Bank, int Acc_Cash, int Acc_Bank, DataTable Items, decimal Charge, int projectId)
	{
		int result = 0;
		using (SqlCommand sqlCommand = new SqlCommand("Update_Order_Purchases", con))
		{
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@PurBranchID", SqlDbType.Int).Value = Code;
			sqlCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = LoginDetails.BranchID;
			sqlCommand.Parameters.Add("@SupplierID", SqlDbType.Int).Value = SupplierID;
			sqlCommand.Parameters.Add("@SupplierName", SqlDbType.NVarChar).Value = SupplierName;
			sqlCommand.Parameters.Add("@SupplierPhone", SqlDbType.NVarChar).Value = SupplierPhone;
			sqlCommand.Parameters.Add("@SupplierVatNum", SqlDbType.NVarChar).Value = SupplierVatNum;
			sqlCommand.Parameters.Add("@Purchases_Date", SqlDbType.DateTime).Value = Purchases_Date;
			sqlCommand.Parameters.Add("@Order_Paymant_Type", SqlDbType.Int).Value = PurchType;
			sqlCommand.Parameters.Add("@CostCentersID", SqlDbType.Int).Value = CostCenter;
			sqlCommand.Parameters.Add("@Note", SqlDbType.NVarChar).Value = Note;
			sqlCommand.Parameters.Add("@NoteNum", SqlDbType.NVarChar).Value = NoteNum;
			sqlCommand.Parameters.Add("@Tax", SqlDbType.Decimal).Value = Tax;
			sqlCommand.Parameters.Add("@TotalPrices", SqlDbType.Decimal).Value = TotalPrices;
			sqlCommand.Parameters.Add("@Safy", SqlDbType.Decimal).Value = Safy;
			sqlCommand.Parameters.Add("@DiscountNum", SqlDbType.Decimal).Value = DiscountNum;
			sqlCommand.Parameters.Add("@DiscountPerantage", SqlDbType.Decimal).Value = DiscountPerantage;
			sqlCommand.Parameters.Add("@Tax_Discount", SqlDbType.Decimal).Value = Tax_Discount;
			sqlCommand.Parameters.Add("@TotalPrices_Discount", SqlDbType.Decimal).Value = TotalPrices_Discount;
			sqlCommand.Parameters.Add("@Net", SqlDbType.Decimal).Value = Net;
			sqlCommand.Parameters.Add("@Cash", SqlDbType.Decimal).Value = Cash;
			sqlCommand.Parameters.Add("@Bank", SqlDbType.Decimal).Value = Bank;
			sqlCommand.Parameters.Add("@Acc_Cash", SqlDbType.Int).Value = Acc_Cash;
			sqlCommand.Parameters.Add("@Acc_Bank", SqlDbType.Int).Value = Acc_Bank;
			sqlCommand.Parameters.Add("@UserID_Add", SqlDbType.Int).Value = LoginDetails.UserID;
			sqlCommand.Parameters.Add("@UserBranch_Add", SqlDbType.Int).Value = LoginDetails.BranchID;
			sqlCommand.Parameters.Add("@UserMacAddress_Add", SqlDbType.NVarChar).Value = LoginDetails.macAddress;
			sqlCommand.Parameters.Add("@SalesMan", SqlDbType.NVarChar).Value = LoginDetails.UserName;
			sqlCommand.Parameters.Add("@Charge", SqlDbType.Decimal).Value = Charge;
			sqlCommand.Parameters.Add("@projectId", SqlDbType.Decimal).Value = projectId;
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
			con.Close();
			result = 1;
		}
		return result;
	}

	public int DeleteOrders(int PurBranchID)
	{
		int result = 0;
		using (SqlCommand sqlCommand = new SqlCommand("Delete_Order_Purchases_ALL", con))
		{
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@PurBranchID", SqlDbType.Int).Value = PurBranchID;
			sqlCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = LoginDetails.BranchID;
			sqlCommand.CommandTimeout = 999999;
			if (con.State == ConnectionState.Closed)
			{
				con.Open();
			}
			result = sqlCommand.ExecuteNonQuery();
			con.Close();
			result = 1;
		}
		return result;
	}

	public bool IsItemVat(int ItemID)
	{
		GTSdbEntities db = new GTSdbEntities();
		try
		{
			return (from s in db.TblSettings
				from i in db.Item_Items
				where i.ItemId == ItemID
				select (s.IsVat == (bool?)true && s.ItemTotalWithVat == (bool?)false && i.Is_Tax == (bool?)false) ? ((bool?)false) : ((bool?)null)).FirstOrDefault() ?? true;
		}
		finally
		{
			if (db != null)
			{
				((IDisposable)db).Dispose();
			}
		}
	}
}


