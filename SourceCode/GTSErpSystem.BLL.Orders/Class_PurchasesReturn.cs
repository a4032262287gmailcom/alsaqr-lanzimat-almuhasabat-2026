using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GTSErpSystem.BLL.Orders;

internal class Class_PurchasesReturn
{
	public class MyObject
	{
		public string ItemId { get; set; }

		public int ItemUnitID { get; set; }

		public decimal LastPriceSmall { get; set; }

		public decimal SmallUnitPrice { get; set; }

		public decimal Discount { get; set; }

		public decimal Quantity { get; set; }

		public int StoreID { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal TotalPrice { get; set; }

		public decimal NetUnitPrice { get; set; }

		public decimal NetTotalPrice { get; set; }

		public decimal VAT { get; set; }

		public decimal VAT_discount { get; set; }

		public string UnitType { get; set; }
	}

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

	public List<Order_PurchasesReturn> GetPurchases()
	{
		return db.Order_PurchasesReturn.Where((Order_PurchasesReturn x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Order_PurchasesReturn> GetPurchases(int Code)
	{
		return db.Order_PurchasesReturn.Where((Order_PurchasesReturn x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
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
			int itemQuantityID = Convert.ToInt32(db.ItemQuantities.Select((ItemQuantity p) => p.ItemQuantityID).DefaultIfEmpty(0).Max()) + 1;
			ItemQuantity itemQuantity3 = new ItemQuantity();
			itemQuantity3.ItemQuantityID = itemQuantityID;
			itemQuantity3.ItemID = ItemID;
			itemQuantity3.CurrentBalance = 0m;
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

	public List<Order_PurchasesReturnDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.Order_PurchasesReturnDetails.Where((Order_PurchasesReturnDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_Order_PurchasesReturn_Result> GetSelectAllPurchases(bool IsNum)
	{
		return db.Select_Order_PurchasesReturn(IsNum, LoginDetails.BranchID).ToList();
	}

	public MyObject ConvertRowToMyObject(DataRow row)
	{
		MyObject myObject = new MyObject();
		if (row["كود_الصنف"].ToString() != "")
		{
			string Itemcod = row["كود_الصنف"].ToString();
			myObject.ItemId = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
			myObject.SmallUnitPrice = Convert.ToDecimal(row["سعر الوحدة"].ToString());
			myObject.UnitPrice = Convert.ToDecimal(row["سعر الوحدة"].ToString());
			myObject.TotalPrice = Convert.ToDecimal(row["الاجمالى"].ToString());
			myObject.Quantity = Convert.ToDecimal(row["الكمية"].ToString());
			myObject.NetUnitPrice = Convert.ToDecimal(row["سعربعدالخصم"].ToString());
			myObject.NetTotalPrice = Convert.ToDecimal(row["إجمالى_بعدالخصم"].ToString());
			myObject.VAT = Convert.ToDecimal(row["الضربية"].ToString());
			myObject.VAT_discount = Convert.ToDecimal(row["الضريبه_بعد_الخصم"].ToString());
			myObject.UnitType = row["أسم_الوحدة"].ToString();
			string nmae = row["الوحدة"].ToString();
			myObject.ItemUnitID = GetUnitIdByName(nmae);
		}
		return myObject;
	}

	public int GetUnitIdByName(string Nmae)
	{
		int num = 0;
		return (from x in db.Item_Unit
			where x.Name == Nmae
			select x.ID).FirstOrDefault();
	}

	public int InsertItems(DataTable Items, int Purchese_ID, int FromPurchID)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["كود_الصنف"].ToString() != "")
			{
				Order_PurchasesReturnDetails order_PurchasesReturnDetails = new Order_PurchasesReturnDetails();
				string Itemcod = row["كود_الصنف"].ToString();
				string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
				int ItemRowChek = Convert.ToInt32(value);
				Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
				if (item_Items != null)
				{
					order_PurchasesReturnDetails.Purchese_ID = Purchese_ID;
					order_PurchasesReturnDetails.FromPurchID = FromPurchID;
					order_PurchasesReturnDetails.BranchID = LoginDetails.BranchID;
					order_PurchasesReturnDetails.ItemID = Convert.ToInt32(item_Items.ItemId);
					string nmae = row["الوحدة"].ToString();
					order_PurchasesReturnDetails.ItemUnitID = GetUnitIdByName(nmae);
					order_PurchasesReturnDetails.SmallUnitPrice = Convert.ToDecimal(row["سعر الوحدة"].ToString());
					order_PurchasesReturnDetails.Quantity = Convert.ToDecimal(row["الكمية"].ToString());
					order_PurchasesReturnDetails.UnitNumber = Convert.ToDecimal(row["عدد الوحدة"].ToString());
					order_PurchasesReturnDetails.UnitPrice = Math.Round(Convert.ToDecimal(row["سعر الوحدة"].ToString()), DecimalNum);
					order_PurchasesReturnDetails.TotalPrice = Math.Round(Convert.ToDecimal(row["الاجمالى"].ToString()), DecimalNum);
					order_PurchasesReturnDetails.NetUnitPrice = Math.Round(Convert.ToDecimal(row["سعربعدالخصم"].ToString()), DecimalNum);
					order_PurchasesReturnDetails.NetTotalPrice = Math.Round(Convert.ToDecimal(row["إجمالى_المبلغ"].ToString()), DecimalNum);
					order_PurchasesReturnDetails.VAT = Math.Round(Convert.ToDecimal(row["الضربية"].ToString()), DecimalNum);
					order_PurchasesReturnDetails.ItemUnitType = row["أسم_الوحدة"].ToString();
					order_PurchasesReturnDetails.VAT_Discount = Math.Round(Convert.ToDecimal(row["الضريبه_بعد_الخصم"].ToString()), DecimalNum);
					order_PurchasesReturnDetails.StoreID = Convert.ToInt32(row["رقم المخزن"]);
					order_PurchasesReturnDetails.ItemNote = row["الشرح"].ToString();
					db.Order_PurchasesReturnDetails.Add(order_PurchasesReturnDetails);
					result = db.SaveChanges();
					ItemQuantityOut(Convert.ToInt32(value), Convert.ToDecimal(row["الكمية"]), row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"]), Convert.ToDecimal(row["عدد الوحدة"]));
					UpdateAvergCost(ItemRowChek);
				}
			}
		}
		return result;
	}

	private void UpdateAvergCost(int ItemId)
	{
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
		decimal num = 0m;
		using SqlCommand sqlCommand = new SqlCommand("Get_AvergItem", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemId;
		sqlCommand.Parameters.Add("@AvergItm", SqlDbType.Decimal).Value = num;
		sqlCommand.CommandTimeout = 999999;
		if (sqlConnection.State == ConnectionState.Closed)
		{
			sqlConnection.Open();
		}
		sqlCommand.ExecuteNonQuery();
		sqlConnection.Close();
	}

	public int InsertPurch(out int Code, int FromPurchID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, int PurchType, int CostCenter, int ProjectId, string Note, string NoteNum, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, bool ISPurReturnNum)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.Order_PurchasesReturn.Select((Order_PurchasesReturn p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Order_PurchasesReturn
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		Order_PurchasesReturn order_PurchasesReturn = new Order_PurchasesReturn();
		order_PurchasesReturn.ID = iD;
		CachCode = num2;
		Code = num2;
		order_PurchasesReturn.FromPurchesID = FromPurchID;
		order_PurchasesReturn.BranchID = LoginDetails.BranchID;
		order_PurchasesReturn.ISPurReturnNum = ISPurReturnNum;
		order_PurchasesReturn.PurBranchID = num2;
		order_PurchasesReturn.SupplierID = SupplierID;
		order_PurchasesReturn.SupplierName = SupplierName;
		order_PurchasesReturn.SupplierPhone = SupplierPhone;
		order_PurchasesReturn.SupplierVatNum = SupplierVatNum;
		order_PurchasesReturn.Purchases_Date = Purchases_Date;
		order_PurchasesReturn.Order_Paymant_Type = PurchType;
		order_PurchasesReturn.CostCentersID = CostCenter;
		order_PurchasesReturn.ProjectId = ProjectId;
		order_PurchasesReturn.Note = Note;
		order_PurchasesReturn.NoteNum = NoteNum;
		order_PurchasesReturn.TotalPrices = TotalPrices;
		order_PurchasesReturn.Tax = Tax;
		order_PurchasesReturn.Safy = Safy;
		order_PurchasesReturn.DiscountNum = DiscountNum;
		order_PurchasesReturn.DiscountPerantage = DiscountPerantage;
		order_PurchasesReturn.Tax_Discount = Tax_Discount;
		order_PurchasesReturn.TotalPrices_Discount = TotalPrices_Discount;
		order_PurchasesReturn.Net = Net;
		order_PurchasesReturn.UserID_Add = LoginDetails.UserID;
		order_PurchasesReturn.UserBranch_Add = LoginDetails.BranchID;
		order_PurchasesReturn.UserMacAddress_Add = LoginDetails.macAddress;
		order_PurchasesReturn.UserDate_Add = DateTime.Now;
		db.Order_PurchasesReturn.Add(order_PurchasesReturn);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID)
	{
		List<Order_PurchasesReturnDetails> list = db.Order_PurchasesReturnDetails.Where((Order_PurchasesReturnDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Order_PurchasesReturnDetails item in list)
		{
			ItemQuantityIn(Convert.ToInt32(item.ItemID), Convert.ToDecimal(item.Quantity), item.ItemUnitType, Convert.ToInt32(item.StoreID), Convert.ToDecimal(item.UnitNumber));
			db.Order_PurchasesReturnDetails.Attach(item);
			db.Order_PurchasesReturnDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, int PurchType, int CostCenter, int ProjectId, string Note, string NoteNum, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net)
	{
		int result = 0;
		Order_PurchasesReturn order_PurchasesReturn = db.Order_PurchasesReturn.Where((Order_PurchasesReturn x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.Order_PurchasesReturn.Where((Order_PurchasesReturn x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (order_PurchasesReturn != null)
		{
			order_PurchasesReturn.SupplierID = SupplierID;
			order_PurchasesReturn.SupplierName = SupplierName;
			order_PurchasesReturn.SupplierPhone = SupplierPhone;
			order_PurchasesReturn.SupplierVatNum = SupplierVatNum;
			order_PurchasesReturn.Purchases_Date = Purchases_Date;
			order_PurchasesReturn.Order_Paymant_Type = PurchType;
			order_PurchasesReturn.CostCentersID = CostCenter;
			order_PurchasesReturn.ProjectId = ProjectId;
			order_PurchasesReturn.Note = Note;
			order_PurchasesReturn.NoteNum = NoteNum;
			order_PurchasesReturn.TotalPrices = TotalPrices;
			order_PurchasesReturn.Tax = Tax;
			order_PurchasesReturn.Safy = Safy;
			order_PurchasesReturn.DiscountNum = DiscountNum;
			order_PurchasesReturn.DiscountPerantage = DiscountPerantage;
			order_PurchasesReturn.Tax_Discount = Tax_Discount;
			order_PurchasesReturn.TotalPrices_Discount = TotalPrices_Discount;
			order_PurchasesReturn.Net = Net;
			order_PurchasesReturn.UserID_Update = LoginDetails.UserID;
			order_PurchasesReturn.UserBranch_Update = LoginDetails.GroupID;
			order_PurchasesReturn.UserMacAddress_Update = LoginDetails.macAddress;
			order_PurchasesReturn.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public int Delete(int PurID)
	{
		int result = 0;
		Order_PurchasesReturn order_PurchasesReturn = db.Order_PurchasesReturn.Where((Order_PurchasesReturn x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (order_PurchasesReturn != null)
		{
			deleteItemsByPurcahesId(PurID);
			db.Order_PurchasesReturn.Attach(order_PurchasesReturn);
			db.Order_PurchasesReturn.Remove(order_PurchasesReturn);
			result = db.SaveChanges();
		}
		return result;
	}

	public int InsertTran(out int TRNIDDS, int PurID, int PurType, string Name, string NoteNum, DateTime PurDate)
	{
		int num = 0;
		TRNIDDS = 0;
		int num2 = Convert.ToInt32(db.Tran_Tran.Select((Tran_Tran p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		Tran_Tran tran_Tran = new Tran_Tran();
		tran_Tran.ID = num2;
		tran_Tran.ReferenceCode = PurID;
		tran_Tran.TranTypeID = 18;
		tran_Tran.DocCode = NoteNum;
		tran_Tran.TranDate = PurDate;
		tran_Tran.BranchID = LoginDetails.BranchID;
		if (PurType == 2)
		{
			tran_Tran.Note = "مرتجعات مشتريات أجلة بدون رقم فاتورة رقم ( " + PurID + " ) ط­ /  " + Name;
		}
		else
		{
			tran_Tran.Note = "مرتجعات مشتريات نقدية بدون رقم فاتورة رقم ( " + PurID + " )";
		}
		tran_Tran.UserID_Add = LoginDetails.UserID;
		tran_Tran.UserBranch_Add = LoginDetails.BranchID;
		tran_Tran.UserMacAddress_Add = LoginDetails.macAddress;
		tran_Tran.UserDate_Add = DateTime.Now;
		db.Tran_Tran.Add(tran_Tran);
		num = db.SaveChanges();
		TRNIDDS = num2;
		return num;
	}

	public int InsertTranDetailsCash(int TranID, int PurID, decimal Total, decimal Vat, decimal Net, int CostCentersID, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = "مرتجعات مشتريات نقدية بدون رقم فاتورة رقم ( " + PurID + " ) ";
		tran_TranDetails.Debit = 0m;
		tran_TranDetails.Credit = Total;
		tran_TranDetails.CostCentersID = CostCentersID;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = " ضريبة مرتجعات مشتريات نقدية بدون رقم فاتورة رقم ( " + PurID + " )  ";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = Vat;
		tran_TranDetails2.CostCentersID = CostCentersID;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = LoginDetails.Acc_CashBox;
		tran_TranDetails3.AccounIindex = 1;
		tran_TranDetails3.TranDesc = "مرتجعات مشتريات نقدية بدون رقم فاتورة رقم  ( " + PurID + " ) ";
		tran_TranDetails3.Debit = Net;
		tran_TranDetails3.Credit = 0m;
		tran_TranDetails3.CostCentersID = 0;
		tran_TranDetails3.ProjectId = ProjectId;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		return db.SaveChanges();
	}

	public int InsertTranDetailsAjila(int TranID, int PurID, int SuppNum, string Name, decimal Total, decimal Vat, decimal Net, int CostCentersID, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = "  مرتجعات مشتريات أجلة بدون رقم فاتورة رقم  ( " + PurID + " ) ط­/ " + Name;
		tran_TranDetails.Debit = 0m;
		tran_TranDetails.Credit = Total;
		tran_TranDetails.CostCentersID = CostCentersID;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = "  مرتجعات مشتريات أجلة بدون رقم فاتورة رقم  ( " + PurID + " ) ط­/ " + Name;
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = Vat;
		tran_TranDetails2.CostCentersID = CostCentersID;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = SuppNum;
		tran_TranDetails3.AccounIindex = 1;
		tran_TranDetails3.TranDesc = "  مرتجعات مشتريات أجلة بدون رقم فاتورة رقم  ( " + PurID + " ) ";
		tran_TranDetails3.Debit = Net;
		tran_TranDetails3.Credit = 0m;
		tran_TranDetails3.CostCentersID = 0;
		tran_TranDetails3.ProjectId = ProjectId;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		return db.SaveChanges();
	}

	public int UpdateTran(out int TRNIDDS, int PurID, int PurType, string Name, string NoteNum, DateTime PurDate)
	{
		int result = 0;
		TRNIDDS = 0;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)PurID && x.TranTypeID == (int?)18 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_Tran != null)
		{
			tran_Tran.DocCode = NoteNum;
			tran_Tran.TranDate = PurDate;
			tran_Tran.BranchID = LoginDetails.BranchID;
			if (PurType == 2)
			{
				tran_Tran.Note = " مرتجعات مشتريات أجلة بدون رقم فاتورة رقم  ( " + PurID + " ) ط­ /  " + Name + " معدلة ";
			}
			else
			{
				tran_Tran.Note = " مرتجعات مشتريات نقدية بدون رقم فاتورة رقم ( " + PurID + " ) معدلة ";
			}
			tran_Tran.UserID_Update = LoginDetails.UserID;
			tran_Tran.UserBranch_Update = LoginDetails.GroupID;
			tran_Tran.UserMacAddress_Update = LoginDetails.macAddress;
			tran_Tran.UserDate_Update = DateTime.Now;
			TRNIDDS = Convert.ToInt32(tran_Tran.ID);
			result = db.SaveChanges();
		}
		return result;
	}

	public int UpdateTranDetailsCash(int TranID, int PurID, decimal Total, decimal Vat, decimal Net, int CostCentersID, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = " مرتجعات مشتريات نقدية بدون رقم فاتورة رقم ( " + PurID + " ) معدلة";
		tran_TranDetails.Debit = 0m;
		tran_TranDetails.Credit = Total;
		tran_TranDetails.CostCentersID = CostCentersID;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = " ضريبة مرتجعات مشتريات نقدية بدون رقم فاتورة رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = Vat;
		tran_TranDetails2.CostCentersID = CostCentersID;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = LoginDetails.Acc_CashBox;
		tran_TranDetails3.AccounIindex = 1;
		tran_TranDetails3.TranDesc = " مرتجعات مشتريات نقدية بدون رقم فاتورة رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails3.Debit = Net;
		tran_TranDetails3.Credit = 0m;
		tran_TranDetails3.CostCentersID = 0;
		tran_TranDetails3.ProjectId = ProjectId;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		return db.SaveChanges();
	}

	public int UpdateTranDetailsAjila(int TranID, int PurID, int SuppNum, string Name, decimal Total, decimal Vat, decimal Net, int CostCentersID, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = " مرتجعات مشتريات أجلة بدون رقم فاتورة رقم  ( " + PurID + " ) ط­/ " + Name + " معدلة ";
		tran_TranDetails.Debit = 0m;
		tran_TranDetails.Credit = Total;
		tran_TranDetails.CostCentersID = CostCentersID;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = "  مرتجعات مشتريات أجلة بدون رقم فاتورة رقم  ( " + PurID + " ) ط­/ " + Name + " معدلة ";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = Vat;
		tran_TranDetails2.CostCentersID = CostCentersID;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = SuppNum;
		tran_TranDetails3.AccounIindex = 1;
		tran_TranDetails3.TranDesc = "  مرتجعات مشتريات أجلة بدون رقم فاتورة رقم  ( " + PurID + " ) معدلة ";
		tran_TranDetails3.Debit = Net;
		tran_TranDetails3.Credit = 0m;
		tran_TranDetails3.CostCentersID = 0;
		tran_TranDetails3.ProjectId = ProjectId;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		return db.SaveChanges();
	}

	public int DeleteTran(int Code)
	{
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)18 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ID == TranID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_Tran != null)
		{
			db.Tran_Tran.Attach(tran_Tran);
			db.Tran_Tran.Remove(tran_Tran);
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteTranDetails(int Code)
	{
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)18 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		List<Tran_TranDetails> list = db.Tran_TranDetails.Where((Tran_TranDetails x) => x.TranSn == (int?)TranID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list != null)
		{
			foreach (Tran_TranDetails item in list)
			{
				db.Tran_TranDetails.Attach(item);
				db.Tran_TranDetails.Remove(item);
			}
			result = db.SaveChanges();
		}
		return result;
	}

	public int InsertTranNum(out int TRNIDDS, int PurID, int PurType, string Name, string NoteNum, DateTime PurDate)
	{
		int num = 0;
		TRNIDDS = 0;
		int num2 = Convert.ToInt32(db.Tran_Tran.Select((Tran_Tran p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		Tran_Tran tran_Tran = new Tran_Tran();
		tran_Tran.ID = num2;
		tran_Tran.ReferenceCode = PurID;
		tran_Tran.TranTypeID = 8;
		tran_Tran.DocCode = NoteNum;
		tran_Tran.TranDate = PurDate;
		tran_Tran.BranchID = LoginDetails.BranchID;
		if (PurType == 2)
		{
			tran_Tran.Note = "مرتجعات مشتريات أجلة رقم ( " + PurID + " ) ط­ /  " + Name;
		}
		else
		{
			tran_Tran.Note = "مرتجعات مشتريات نقدية رقم ( " + PurID + " )";
		}
		tran_Tran.UserID_Add = LoginDetails.UserID;
		tran_Tran.UserBranch_Add = LoginDetails.BranchID;
		tran_Tran.UserMacAddress_Add = LoginDetails.macAddress;
		tran_Tran.UserDate_Add = DateTime.Now;
		db.Tran_Tran.Add(tran_Tran);
		num = db.SaveChanges();
		TRNIDDS = num2;
		return num;
	}

	public int InsertTranDetailsCashNUM(int TranID, int PurID, decimal Total, decimal Vat, decimal Net, int CostCentersID, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = "مرتجعات مشتريات نقدية رقم ( " + PurID + " ) ";
		tran_TranDetails.Debit = 0m;
		tran_TranDetails.Credit = Total;
		tran_TranDetails.CostCentersID = CostCentersID;
		tran_TranDetails.CostCentersID = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = " ضريبة مرتجعات مشتريات نقدية رقم ( " + PurID + " )  ";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = Vat;
		tran_TranDetails2.CostCentersID = CostCentersID;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = LoginDetails.Acc_CashBox;
		tran_TranDetails3.AccounIindex = 1;
		tran_TranDetails3.TranDesc = "مرتجعات مشتريات نقدية رقم  ( " + PurID + " ) ";
		tran_TranDetails3.Debit = Net;
		tran_TranDetails3.Credit = 0m;
		tran_TranDetails3.CostCentersID = 0;
		tran_TranDetails3.ProjectId = ProjectId;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		return db.SaveChanges();
	}

	public int InsertTranDetailsAjilaNUM(int TranID, int PurID, int SuppNum, string Name, decimal Total, decimal Vat, decimal Net, int CostCentersID, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = "  مرتجعات مشتريات أجلة رقم  ( " + PurID + " ) ط­/ " + Name;
		tran_TranDetails.Debit = 0m;
		tran_TranDetails.Credit = Total;
		tran_TranDetails.CostCentersID = CostCentersID;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = "  مرتجعات مشتريات أجلة رقم  ( " + PurID + " ) ط­/ " + Name;
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = Vat;
		tran_TranDetails2.CostCentersID = CostCentersID;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = SuppNum;
		tran_TranDetails3.AccounIindex = 1;
		tran_TranDetails3.TranDesc = "  مرتجعات مشتريات أجلة رقم  ( " + PurID + " ) ";
		tran_TranDetails3.Debit = Net;
		tran_TranDetails3.Credit = 0m;
		tran_TranDetails3.CostCentersID = 0;
		tran_TranDetails3.ProjectId = ProjectId;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		return db.SaveChanges();
	}

	public int UpdateTranNUM(out int TRNIDDS, int PurID, int PurType, string Name, string NoteNum, DateTime PurDate)
	{
		int result = 0;
		TRNIDDS = 0;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)PurID && x.TranTypeID == (int?)8 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_Tran != null)
		{
			tran_Tran.DocCode = NoteNum;
			tran_Tran.TranDate = PurDate;
			tran_Tran.BranchID = LoginDetails.BranchID;
			if (PurType == 2)
			{
				tran_Tran.Note = " مرتجعات مشتريات أجلة رقم  ( " + PurID + " ) ط­ /  " + Name + " معدلة ";
			}
			else
			{
				tran_Tran.Note = " مرتجعات مشتريات نقدية رقم ( " + PurID + " ) معدلة ";
			}
			tran_Tran.UserID_Update = LoginDetails.UserID;
			tran_Tran.UserBranch_Update = LoginDetails.GroupID;
			tran_Tran.UserMacAddress_Update = LoginDetails.macAddress;
			tran_Tran.UserDate_Update = DateTime.Now;
			TRNIDDS = Convert.ToInt32(tran_Tran.ID);
			result = db.SaveChanges();
		}
		return result;
	}

	public int UpdateTranDetailsCashNUM(int TranID, int PurID, decimal Total, decimal Vat, decimal Net, int CostCentersID, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = " مرتجعات مشتريات نقدية رقم ( " + PurID + " ) معدلة";
		tran_TranDetails.Debit = 0m;
		tran_TranDetails.Credit = Total;
		tran_TranDetails.CostCentersID = CostCentersID;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = " ضريبة مرتجعات مشتريات نقدية رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = Vat;
		tran_TranDetails2.CostCentersID = CostCentersID;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = LoginDetails.Acc_CashBox;
		tran_TranDetails3.AccounIindex = 1;
		tran_TranDetails3.TranDesc = " مرتجعات مشتريات نقدية رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails3.Debit = Net;
		tran_TranDetails3.Credit = 0m;
		tran_TranDetails3.CostCentersID = 0;
		tran_TranDetails3.ProjectId = ProjectId;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		return db.SaveChanges();
	}

	public int UpdateTranDetailsAjilaNUM(int TranID, int PurID, int SuppNum, string Name, decimal Total, decimal Vat, decimal Net, int CostCentersID, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = " مرتجعات مشتريات أجلة رقم  ( " + PurID + " ) ط­/ " + Name + " معدلة ";
		tran_TranDetails.Debit = 0m;
		tran_TranDetails.Credit = Total;
		tran_TranDetails.CostCentersID = CostCentersID;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = "  مرتجعات مشتريات أجلة رقم  ( " + PurID + " ) ط­/ " + Name + " معدلة ";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = Vat;
		tran_TranDetails2.CostCentersID = CostCentersID;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = SuppNum;
		tran_TranDetails3.AccounIindex = 1;
		tran_TranDetails3.TranDesc = "  مرتجعات مشتريات أجلة رقم  ( " + PurID + " ) معدلة ";
		tran_TranDetails3.Debit = Net;
		tran_TranDetails3.Credit = 0m;
		tran_TranDetails3.CostCentersID = 0;
		tran_TranDetails3.ProjectId = ProjectId;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		return db.SaveChanges();
	}

	public int DeleteTranNum(int Code)
	{
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)8 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ID == TranID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_Tran != null)
		{
			db.Tran_Tran.Attach(tran_Tran);
			db.Tran_Tran.Remove(tran_Tran);
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteTranDetailsNum(int Code)
	{
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)8 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		List<Tran_TranDetails> list = db.Tran_TranDetails.Where((Tran_TranDetails x) => x.TranSn == (int?)TranID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list != null)
		{
			foreach (Tran_TranDetails item in list)
			{
				db.Tran_TranDetails.Attach(item);
				db.Tran_TranDetails.Remove(item);
			}
			result = db.SaveChanges();
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
		decimal num4 = Convert.ToDecimal(db.Order_PurchasesReturnDetails.Where((Order_PurchasesReturnDetails x) => x.ItemUnitType == "الصغرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesReturnDetails x) => x.Quantity));
		decimal num5 = Convert.ToDecimal(db.Order_PurchasesReturnDetails.Where((Order_PurchasesReturnDetails x) => x.ItemUnitType == "المتوسطة" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesReturnDetails x) => x.Quantity));
		decimal num6 = Convert.ToDecimal(db.Order_PurchasesReturnDetails.Where((Order_PurchasesReturnDetails x) => x.ItemUnitType == "الكبرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesReturnDetails x) => x.Quantity));
		decimal num7 = Convert.ToDecimal(db.Order_PurchasesReturnDetails.Where((Order_PurchasesReturnDetails x) => x.ItemUnitType == "الصغرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesReturnDetails x) => x.NetTotalPrice));
		decimal num8 = Convert.ToDecimal(db.Order_PurchasesReturnDetails.Where((Order_PurchasesReturnDetails x) => x.ItemUnitType == "المتوسطة" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesReturnDetails x) => x.NetTotalPrice));
		decimal num9 = Convert.ToDecimal(db.Order_PurchasesReturnDetails.Where((Order_PurchasesReturnDetails x) => x.ItemUnitType == "الكبرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesReturnDetails x) => x.NetTotalPrice));
		decimal value = num4;
		decimal value2 = num5;
		decimal? convertMediumUnit = ItemDtails.ConvertMediumUnit;
		decimal? num10 = (decimal?)value2 * convertMediumUnit;
		decimal? num11 = (decimal?)value + num10;
		value = num6;
		decimal? convertBigUnit = ItemDtails.ConvertBigUnit;
		num3 = Convert.ToDecimal(num11 + (decimal?)value * convertBigUnit * ItemDtails.ConvertMediumUnit);
		num2 = Convert.ToDecimal(num7 + num8 + num9);
		num = num2 / num3;
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
		return (from xx in db.Item_Items
			where xx.Item_code == Code
			select xx.ItemId).FirstOrDefault();
	}

	public decimal GetTotalQantity(int PurcheseID, int ItemOd)
	{
		decimal num = 0m;
		return Convert.ToDecimal(db.Order_PurchasesReturnDetails.Where((Order_PurchasesReturnDetails x) => x.FromPurchID == (int?)PurcheseID && x.ItemID == (int?)ItemOd && x.BranchID == (int?)LoginDetails.BranchID).Sum((Order_PurchasesReturnDetails x) => x.Quantity));
	}
}


