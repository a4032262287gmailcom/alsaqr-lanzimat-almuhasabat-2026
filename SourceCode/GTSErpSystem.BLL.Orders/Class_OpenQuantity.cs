using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GTSErpSystem.BLL.Orders;

internal class Class_OpenQuantity
{
	private GTSdbEntities db = new GTSdbEntities();

	private SqlConnection con = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);

	private DataTable AvergItemTable = new DataTable();

	private Class_OrdersReturn ORDR = new Class_OrdersReturn();

	public List<Account_AccountType> GetAccountType()
	{
		return db.Account_AccountType.ToList();
	}

	public List<Select_SearchAccountCostCenter_Result> GetCostCenters()
	{
		return db.Select_SearchAccountCostCenter(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<Order_OpenQuantity> GetPurchases()
	{
		return db.Order_OpenQuantity.Where((Order_OpenQuantity x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Order_OpenQuantity> GetPurchases(int Code)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Order_OpenQuantity.Where((Order_OpenQuantity x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
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

	public List<Account_Stores> AllStores()
	{
		return db.Account_Stores.ToList();
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
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
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

	public List<Order_OpenQuantityDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.Order_OpenQuantityDetails.Where((Order_OpenQuantityDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_Order_OpenQuantity_Result> GetSelectAllPurchases()
	{
		return db.Select_Order_OpenQuantity(LoginDetails.BranchID).ToList();
	}

	public int GetUnitIdByName(string Nmae)
	{
		int num = 0;
		return (from x in db.Item_Unit
			where x.Name == Nmae
			select x.ID).FirstOrDefault();
	}

	public int InsertOrders(out int Code, DateTime Purchases_Date, string NoteNum, decimal TotalPrices, DataTable Items)
	{
		if (!AvergItemTable.Columns.Contains("ItemId"))
		{
			AvergItemTable.Columns.Add("ItemId", typeof(int));
		}
		using (SqlCommand sqlCommand = new SqlCommand("Insert_OpenQuantity", con))
		{
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = LoginDetails.BranchID;
			sqlCommand.Parameters.Add("@Purchases_Date", SqlDbType.DateTime).Value = Purchases_Date;
			sqlCommand.Parameters.Add("@NoteNum", SqlDbType.NVarChar).Value = NoteNum;
			sqlCommand.Parameters.Add("@TotalPrices", SqlDbType.Decimal).Value = TotalPrices;
			sqlCommand.Parameters.Add("@UserID_Add", SqlDbType.Int).Value = LoginDetails.UserID;
			sqlCommand.Parameters.Add("@UserBranch_Add", SqlDbType.Int).Value = LoginDetails.BranchID;
			sqlCommand.Parameters.Add("@UserMacAddress_Add", SqlDbType.NVarChar).Value = LoginDetails.macAddress;
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
		return Code;
	}

	public int UpdateOrders(int Code, DateTime Purchases_Date, string NoteNum, decimal TotalPrices, DataTable Items)
	{
		if (!AvergItemTable.Columns.Contains("ItemId"))
		{
			AvergItemTable.Columns.Add("ItemId", typeof(int));
		}
		int result = 0;
		using (SqlCommand sqlCommand = new SqlCommand("Update_OpenQuantity", con))
		{
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@PurBranchID", SqlDbType.Int).Value = Code;
			sqlCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = LoginDetails.BranchID;
			sqlCommand.Parameters.Add("@Purchases_Date", SqlDbType.DateTime).Value = Purchases_Date;
			sqlCommand.Parameters.Add("@NoteNum", SqlDbType.NVarChar).Value = NoteNum;
			sqlCommand.Parameters.Add("@TotalPrices", SqlDbType.Decimal).Value = TotalPrices;
			sqlCommand.Parameters.Add("@UserID_Add", SqlDbType.Int).Value = LoginDetails.UserID;
			sqlCommand.Parameters.Add("@UserBranch_Add", SqlDbType.Int).Value = LoginDetails.BranchID;
			sqlCommand.Parameters.Add("@UserMacAddress_Add", SqlDbType.NVarChar).Value = LoginDetails.macAddress;
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

	public int DeleteOrders(int PurBranchID, DataTable Items)
	{
		if (!AvergItemTable.Columns.Contains("ItemId"))
		{
			AvergItemTable.Columns.Add("ItemId", typeof(int));
		}
		int result = 0;
		using (SqlCommand sqlCommand = new SqlCommand("Delete_Order_OpenQuantity_ALL", con))
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

	public void Get_Item_AvargeCost(string itemcode, string unittype, decimal Lastcost)
	{
		Item_Items ItemDtails = db.Item_Items.Where((Item_Items y) => y.Item_code == itemcode).FirstOrDefault();
		if (ItemDtails == null)
		{
			return;
		}
		decimal num = 0m;
		decimal num2 = 0m;
		decimal num3 = 0m;
		decimal num4 = Convert.ToDecimal(db.Order_OpenQuantityDetails.Where((Order_OpenQuantityDetails x) => x.ItemUnitType == "الصغرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_OpenQuantityDetails x) => x.Quantity));
		decimal num5 = Convert.ToDecimal(db.Order_OpenQuantityDetails.Where((Order_OpenQuantityDetails x) => x.ItemUnitType == "المتوسطة" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_OpenQuantityDetails x) => x.Quantity));
		decimal num6 = Convert.ToDecimal(db.Order_OpenQuantityDetails.Where((Order_OpenQuantityDetails x) => x.ItemUnitType == "الكبرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_OpenQuantityDetails x) => x.Quantity));
		decimal num7 = Convert.ToDecimal(db.Order_OpenQuantityDetails.Where((Order_OpenQuantityDetails x) => x.ItemUnitType == "الصغرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_OpenQuantityDetails x) => x.TotalPrice));
		decimal num8 = Convert.ToDecimal(db.Order_OpenQuantityDetails.Where((Order_OpenQuantityDetails x) => x.ItemUnitType == "المتوسطة" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_OpenQuantityDetails x) => x.TotalPrice));
		decimal num9 = Convert.ToDecimal(db.Order_OpenQuantityDetails.Where((Order_OpenQuantityDetails x) => x.ItemUnitType == "الكبرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_OpenQuantityDetails x) => x.TotalPrice));
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
		db.SaveChanges();
	}

	public int GetItmID(string Code)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return (from xx in gTSdbEntities.Item_Items
			where xx.Item_code == Code
			select xx.ItemId).FirstOrDefault();
	}

	public List<Get_Item_ByCodeALL_Result> GetItemByCode(string Code)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Get_Item_ByCodeALL(Code, LoginDetails.BranchID).ToList();
	}

	public List<Get_Item_BySearchOpenQauntity_Result> Get_Item_BySearchOpenQauntity(int Purchese_ID)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Get_Item_BySearchOpenQauntity(Purchese_ID, LoginDetails.BranchID).ToList();
	}

	public List<GetAllitems_Alternatives_Result> GetAllitems_Alternatives(int ItmID, int StoreID, int UnitType)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.GetAllitems_Alternatives(ItmID, StoreID, UnitType).ToList();
	}
}


