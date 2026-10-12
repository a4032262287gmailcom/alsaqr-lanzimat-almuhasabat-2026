using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.OrderDalala;

public class Class_Sending
{
	private int CachCode = 0;

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

	public List<OrderDalala_Sending> GetPurchases()
	{
		return db.OrderDalala_Sending.Where((OrderDalala_Sending x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<OrderDalala_Sending> GetPurchases(int Code)
	{
		return db.OrderDalala_Sending.Where((OrderDalala_Sending x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int ItemQuantityIn(int ItemID, decimal Quantity, string unitType, int StoreID, DateTime ExpireDate)
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
						result = db.SaveChanges();
					}
					if (unitType == "المتوسطة")
					{
						decimal? currentBalance = itemQuantity2.CurrentBalance;
						decimal value = Quantity;
						decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
						itemQuantity2.CurrentBalance = currentBalance + (decimal?)value * convertMediumUnit;
						result = db.SaveChanges();
					}
					if (unitType == "الكبرى")
					{
						decimal? currentBalance2 = itemQuantity2.CurrentBalance;
						decimal value = Quantity;
						decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
						itemQuantity2.CurrentBalance = currentBalance2 + (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
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

	public int ItemQuantityOut(int ItemID, decimal Quantity, string unitType, int StoreID, DateTime ExpireDate)
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
				if (unitType == "الصغرى")
				{
					itemQuantity2.CurrentBalance -= (decimal?)Quantity;
					result = db.SaveChanges();
				}
				if (unitType == "المتوسطة")
				{
					decimal? currentBalance = itemQuantity2.CurrentBalance;
					decimal value = Quantity;
					decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
					itemQuantity2.CurrentBalance = currentBalance - (decimal?)value * convertMediumUnit;
					result = db.SaveChanges();
				}
				if (unitType == "الكبرى")
				{
					decimal? currentBalance2 = itemQuantity2.CurrentBalance;
					decimal value = Quantity;
					decimal? convertBigUnit = item_Items.ConvertBigUnit;
					itemQuantity2.CurrentBalance = currentBalance2 - (decimal?)value * convertBigUnit * item_Items.ConvertMediumUnit;
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
				itemQuantity3.CurrentBalance -= (decimal?)Quantity;
			}
			if (unitType == "المتوسطة")
			{
				decimal? currentBalance = itemQuantity3.CurrentBalance;
				decimal value = Quantity;
				decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
				itemQuantity3.CurrentBalance = currentBalance - (decimal?)value * convertMediumUnit2;
			}
			if (unitType == "الكبرى")
			{
				decimal? currentBalance2 = itemQuantity3.CurrentBalance;
				decimal value = Quantity;
				decimal? convertBigUnit = item_Items.ConvertMediumUnit;
				itemQuantity3.CurrentBalance = currentBalance2 - (decimal?)value * convertBigUnit * item_Items.ConvertBigUnit;
			}
			itemQuantity3.BeginningInventory = 0m;
			itemQuantity3.BeginningInventoryPrice = 0m;
			itemQuantity3.OpeningBalance = 0m;
			itemQuantity3.StoreID = StoreID;
			db.ItemQuantities.Add(itemQuantity3);
			result = db.SaveChanges();
		}
		return result;
	}

	public List<Item_Items> GetItems(string Code)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		List<OrderDalala_SendingDetails> entities = new List<OrderDalala_SendingDetails>();
		gTSdbEntities.OrderDalala_SendingDetails.AddRange(entities);
		return gTSdbEntities.Item_Items.Where((Item_Items x) => x.Item_code == Code || x.item_Name == Code || x.item_Name_English == Code || x.SmallBarCode1 == Code || x.SmallBarCode2 == Code || x.SmallBarCode3 == Code || x.MediumBarCode1 == Code || x.MediumBarCode2 == Code || x.MediumBarCode3 == Code || x.BigBarCode1 == Code || x.BigBarCode2 == Code || x.BigBarCode3 == Code).ToList();
	}

	public List<Item_Items> GetItemsByitemid(int ItemID)
	{
		return db.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).ToList();
	}

	public List<Item_Items> GetItems()
	{
		return db.Item_Items.ToList();
	}

	public List<OrderDalala_SendingDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.OrderDalala_SendingDetails.Where((OrderDalala_SendingDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_OrderDalala_Sending_Result> GetSelectAllPurchases()
	{
		return db.Select_OrderDalala_Sending(LoginDetails.BranchID).ToList();
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
		int num = 0;
		List<OrderDalala_SendingDetails> list = new List<OrderDalala_SendingDetails>();
		foreach (DataRow row in Items.Rows)
		{
			if (row["كود_الصنف"].ToString() != "")
			{
				OrderDalala_SendingDetails orderDalala_SendingDetails = new OrderDalala_SendingDetails();
				string Itemcod = row["كود_الصنف"].ToString();
				string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
				int ItemRowChek = Convert.ToInt32(value);
				Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
				int value2 = Convert.ToInt32(db.TblSettings.Select((TblSetting x) => x.StoreID).FirstOrDefault());
				if (item_Items != null)
				{
					orderDalala_SendingDetails.Purchese_ID = Purchese_ID;
					orderDalala_SendingDetails.BranchID = LoginDetails.BranchID;
					orderDalala_SendingDetails.ItemID = Convert.ToInt32(item_Items.ItemId);
					string nmae = row["الوحدة"].ToString();
					orderDalala_SendingDetails.ItemUnitID = GetUnitIdByName(nmae);
					orderDalala_SendingDetails.Quantity = Convert.ToDecimal(row["الكمية"].ToString());
					orderDalala_SendingDetails.NoteItem = row["بيان الصنف"].ToString();
					orderDalala_SendingDetails.StoreID = value2;
					orderDalala_SendingDetails.ItemUnitType = row["أسم_الوحدة"].ToString();
					orderDalala_SendingDetails.StoreID = Convert.ToInt32(row["رقم المخزن"]);
					list.Add(orderDalala_SendingDetails);
					ItemQuantityIn(Convert.ToInt32(value), Convert.ToDecimal(row["الكمية"].ToString()), row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"].ToString()), DateTime.Now);
				}
			}
		}
		db.OrderDalala_SendingDetails.AddRange(list);
		return db.SaveChanges();
	}

	public int InsertPurch(out int Code, int SupplierID, string SupplierName, string DriverName, string DriverPhone, string CarNum, string City, DateTime Purchases_Date)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.OrderDalala_Sending.Select((OrderDalala_Sending p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.OrderDalala_Sending
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		OrderDalala_Sending orderDalala_Sending = new OrderDalala_Sending();
		orderDalala_Sending.ID = iD;
		CachCode = num2;
		Code = num2;
		orderDalala_Sending.BranchID = LoginDetails.BranchID;
		orderDalala_Sending.PurBranchID = num2;
		orderDalala_Sending.SupplierID = SupplierID;
		orderDalala_Sending.SupplierName = SupplierName;
		orderDalala_Sending.DriverName = DriverName;
		orderDalala_Sending.DriverPhone = DriverPhone;
		orderDalala_Sending.CarNum = CarNum;
		orderDalala_Sending.City = City;
		orderDalala_Sending.Purchases_Date = Purchases_Date;
		orderDalala_Sending.UserID_Add = LoginDetails.UserID;
		orderDalala_Sending.UserBranch_Add = LoginDetails.BranchID;
		orderDalala_Sending.UserMacAddress_Add = LoginDetails.macAddress;
		orderDalala_Sending.UserDate_Add = DateTime.Now;
		db.OrderDalala_Sending.Add(orderDalala_Sending);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID)
	{
		List<OrderDalala_SendingDetails> list = db.OrderDalala_SendingDetails.Where((OrderDalala_SendingDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (OrderDalala_SendingDetails item in list)
		{
			ItemQuantityOut(Convert.ToInt32(item.ItemID), Convert.ToDecimal(item.Quantity), item.ItemUnitType, Convert.ToInt32(item.StoreID), Convert.ToDateTime(item.DateExpire));
			db.OrderDalala_SendingDetails.Attach(item);
			db.OrderDalala_SendingDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, int SupplierID, string SupplierName, string DriverName, string DriverPhone, string CarNum, string City, DateTime Purchases_Date)
	{
		int result = 0;
		OrderDalala_Sending orderDalala_Sending = db.OrderDalala_Sending.Where((OrderDalala_Sending x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.OrderDalala_Sending.Where((OrderDalala_Sending x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (orderDalala_Sending != null)
		{
			orderDalala_Sending.SupplierID = SupplierID;
			orderDalala_Sending.SupplierName = SupplierName;
			orderDalala_Sending.DriverName = DriverName;
			orderDalala_Sending.DriverPhone = DriverPhone;
			orderDalala_Sending.CarNum = CarNum;
			orderDalala_Sending.City = City;
			orderDalala_Sending.Purchases_Date = Purchases_Date;
			orderDalala_Sending.UserID_Update = LoginDetails.UserID;
			orderDalala_Sending.UserBranch_Update = LoginDetails.BranchID;
			orderDalala_Sending.UserMacAddress_Update = LoginDetails.macAddress;
			orderDalala_Sending.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public int Delete(int PurID)
	{
		int result = 0;
		OrderDalala_Sending orderDalala_Sending = db.OrderDalala_Sending.Where((OrderDalala_Sending x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (orderDalala_Sending != null)
		{
			deleteItemsByPurcahesId(PurID);
			db.OrderDalala_Sending.Attach(orderDalala_Sending);
			db.OrderDalala_Sending.Remove(orderDalala_Sending);
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


