using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GTSErpSystem.BLL.Orders;

public class Class_OrdersReturn
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

	private DataTable AvergItemTable = new DataTable();

	public List<Account_AccountType> GetAccountType()
	{
		return db.Account_AccountType.ToList();
	}

	public List<Select_SearchAccountCostCenter_Result> GetCostCenters()
	{
		return db.Select_SearchAccountCostCenter(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<Order_OrderReturn> GetPurchases()
	{
		return db.Order_OrderReturn.Where((Order_OrderReturn x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Order_OrderReturn> GetPurchases(int Code)
	{
		return db.Order_OrderReturn.Where((Order_OrderReturn x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int GetStoreIDByName(string Name)
	{
		return (from a in db.Account_Stores
			where a.Store_Name == Name
			select a.ID).FirstOrDefault();
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

	public int ItemQuantityOut(int ItemID, decimal Quantity, string unitType, int StoreID, int itemType)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		int result = 0;
		if (ItemID > 0)
		{
			if (itemType == 1)
			{
				ItemQuantity itemQuantity = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
				Item_Items item_Items = gTSdbEntities.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault();
				if (itemQuantity != null)
				{
					ItemQuantity itemQuantity2 = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
					if (itemQuantity2 != null)
					{
						if (unitType == "الصغرى")
						{
							itemQuantity2.CurrentBalance -= (decimal?)Quantity;
							result = gTSdbEntities.SaveChanges();
						}
						if (unitType == "المتوسطة")
						{
							decimal? currentBalance = itemQuantity2.CurrentBalance;
							decimal value = Quantity;
							decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
							itemQuantity2.CurrentBalance = currentBalance - (decimal?)value * convertMediumUnit;
							result = gTSdbEntities.SaveChanges();
						}
						if (unitType == "الكبرى")
						{
							decimal? currentBalance2 = itemQuantity2.CurrentBalance;
							decimal value = Quantity;
							decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
							itemQuantity2.CurrentBalance = currentBalance2 - (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
							result = gTSdbEntities.SaveChanges();
						}
					}
					return result;
				}
				ItemQuantity itemQuantity3 = new ItemQuantity();
				int itemQuantityID = Convert.ToInt32(gTSdbEntities.ItemQuantities.Select((ItemQuantity p) => p.ItemQuantityID).DefaultIfEmpty(0).Max()) + 1;
				itemQuantity3.ItemQuantityID = itemQuantityID;
				itemQuantity3.ItemID = ItemID;
				if (unitType == "الصغرى")
				{
					itemQuantity3.CurrentBalance = -Quantity;
				}
				if (unitType == "المتوسطة")
				{
					decimal value = -Quantity;
					decimal? currentBalance = item_Items.ConvertMediumUnit;
					itemQuantity3.CurrentBalance = (decimal?)value * currentBalance;
				}
				if (unitType == "الكبرى")
				{
					decimal value = -Quantity;
					decimal? convertMediumUnit3 = item_Items.ConvertMediumUnit;
					itemQuantity3.CurrentBalance = (decimal?)value * convertMediumUnit3 * item_Items.ConvertBigUnit;
				}
				itemQuantity3.BeginningInventory = 0m;
				itemQuantity3.BeginningInventoryPrice = 0m;
				itemQuantity3.OpeningBalance = 0m;
				itemQuantity3.StoreID = StoreID;
				gTSdbEntities.ItemQuantities.Add(itemQuantity3);
				result = gTSdbEntities.SaveChanges();
			}
			if (itemType == 3)
			{
				List<Item_ItemComponent> list = gTSdbEntities.Item_ItemComponent.Where((Item_ItemComponent x) => x.ItemID_Master == (int?)ItemID).ToList();
				Item_Items item_Items2 = gTSdbEntities.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault();
				foreach (Item_ItemComponent item in list)
				{
					Item_Items item_Items3 = gTSdbEntities.Item_Items.Where((Item_Items x) => (int?)x.ItemId == item.ItemID_Complant).FirstOrDefault();
					ItemQuantity itemQuantity4 = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == item.ItemID_Complant && x.StoreID == item.StoreID).FirstOrDefault();
					if (itemQuantity4 != null)
					{
						ItemQuantity itemQuantity5 = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == item.ItemID_Complant && x.StoreID == item.StoreID).FirstOrDefault();
						if (itemQuantity5 == null)
						{
							continue;
						}
						if (unitType == "الصغرى")
						{
							if (item.ItemUnitType == "الصغرى")
							{
								itemQuantity5.CurrentBalance -= item.Amount * (decimal?)Quantity;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "المتوسطة")
							{
								itemQuantity5.CurrentBalance -= item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "الكبرى")
							{
								itemQuantity5.CurrentBalance -= item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit;
								result = gTSdbEntities.SaveChanges();
							}
						}
						if (unitType == "المتوسطة")
						{
							if (item.ItemUnitType == "الصغرى")
							{
								itemQuantity5.CurrentBalance -= item.Amount * (decimal?)Quantity * item_Items2.ConvertMediumUnit;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "المتوسطة")
							{
								itemQuantity5.CurrentBalance -= item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items2.ConvertMediumUnit;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "الكبرى")
							{
								itemQuantity5.CurrentBalance -= item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit * item_Items2.ConvertMediumUnit;
								result = gTSdbEntities.SaveChanges();
							}
						}
						if (unitType == "الكبرى")
						{
							if (item.ItemUnitType == "الصغرى")
							{
								itemQuantity5.CurrentBalance -= item.Amount * (decimal?)Quantity * item_Items2.ConvertBigUnit;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "المتوسطة")
							{
								itemQuantity5.CurrentBalance -= item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items2.ConvertBigUnit;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "الكبرى")
							{
								itemQuantity5.CurrentBalance -= item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit * item_Items2.ConvertBigUnit;
								result = gTSdbEntities.SaveChanges();
							}
						}
						continue;
					}
					ItemQuantity itemQuantity6 = new ItemQuantity();
					int itemQuantityID2 = Convert.ToInt32(gTSdbEntities.ItemQuantities.Select((ItemQuantity p) => p.ItemQuantityID).DefaultIfEmpty(0).Max()) + 1;
					itemQuantity6.ItemQuantityID = itemQuantityID2;
					itemQuantity6.ItemID = item.ItemID_Complant;
					if (unitType == "الصغرى")
					{
						if (item.ItemUnitType == "الصغرى")
						{
							itemQuantity6.CurrentBalance = -item.Amount * (decimal?)Quantity;
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "المتوسطة")
						{
							itemQuantity6.CurrentBalance = -(item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit);
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "الكبرى")
						{
							itemQuantity6.CurrentBalance = -(item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit);
							result = gTSdbEntities.SaveChanges();
						}
					}
					if (unitType == "المتوسطة")
					{
						if (item.ItemUnitType == "الصغرى")
						{
							itemQuantity6.CurrentBalance = -(item.Amount * (decimal?)Quantity * item_Items2.ConvertMediumUnit);
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "المتوسطة")
						{
							itemQuantity6.CurrentBalance = -(item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items2.ConvertMediumUnit);
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "الكبرى")
						{
							itemQuantity6.CurrentBalance = -(item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit * item_Items2.ConvertMediumUnit);
							result = gTSdbEntities.SaveChanges();
						}
					}
					if (unitType == "الكبرى")
					{
						if (item.ItemUnitType == "الصغرى")
						{
							itemQuantity6.CurrentBalance = -(item.Amount * (decimal?)Quantity * item_Items2.ConvertBigUnit);
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "المتوسطة")
						{
							itemQuantity6.CurrentBalance = -(item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items2.ConvertBigUnit);
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "الكبرى")
						{
							itemQuantity6.CurrentBalance = -(item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit * item_Items2.ConvertBigUnit);
							result = gTSdbEntities.SaveChanges();
						}
					}
					itemQuantity6.BeginningInventory = 0m;
					itemQuantity6.BeginningInventoryPrice = 0m;
					itemQuantity6.OpeningBalance = 0m;
					itemQuantity6.StoreID = item.StoreID;
					gTSdbEntities.ItemQuantities.Add(itemQuantity6);
					result = gTSdbEntities.SaveChanges();
				}
			}
		}
		return result;
	}

	public int ItemQuantityIN(int ItemID, decimal Quantity, string unitType, int StoreID, int itemType)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		int result = 0;
		if (ItemID > 0)
		{
			if (itemType == 1)
			{
				ItemQuantity itemQuantity = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
				Item_Items item_Items = gTSdbEntities.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault();
				if (itemQuantity != null)
				{
					ItemQuantity itemQuantity2 = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault();
					if (itemQuantity2 != null)
					{
						if (unitType == "الصغرى")
						{
							itemQuantity2.CurrentBalance += (decimal?)Quantity;
							result = gTSdbEntities.SaveChanges();
						}
						if (unitType == "المتوسطة")
						{
							decimal? currentBalance = itemQuantity2.CurrentBalance;
							decimal value = Quantity;
							decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
							itemQuantity2.CurrentBalance = currentBalance + (decimal?)value * convertMediumUnit;
							result = gTSdbEntities.SaveChanges();
						}
						if (unitType == "الكبرى")
						{
							decimal? currentBalance2 = itemQuantity2.CurrentBalance;
							decimal value = Quantity;
							decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
							itemQuantity2.CurrentBalance = currentBalance2 + (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
							result = gTSdbEntities.SaveChanges();
						}
					}
					return result;
				}
				ItemQuantity itemQuantity3 = new ItemQuantity();
				int itemQuantityID = Convert.ToInt32(gTSdbEntities.ItemQuantities.Select((ItemQuantity p) => p.ItemQuantityID).DefaultIfEmpty(0).Max()) + 1;
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
				gTSdbEntities.ItemQuantities.Add(itemQuantity3);
				result = gTSdbEntities.SaveChanges();
			}
			if (itemType == 3)
			{
				List<Item_ItemComponent> list = gTSdbEntities.Item_ItemComponent.Where((Item_ItemComponent x) => x.ItemID_Master == (int?)ItemID).ToList();
				Item_Items item_Items2 = gTSdbEntities.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault();
				foreach (Item_ItemComponent item in list)
				{
					Item_Items item_Items3 = gTSdbEntities.Item_Items.Where((Item_Items x) => (int?)x.ItemId == item.ItemID_Complant).FirstOrDefault();
					ItemQuantity itemQuantity4 = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == item.ItemID_Complant && x.StoreID == item.StoreID).FirstOrDefault();
					if (itemQuantity4 != null)
					{
						ItemQuantity itemQuantity5 = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == item.ItemID_Complant && x.StoreID == item.StoreID).FirstOrDefault();
						if (itemQuantity5 == null)
						{
							continue;
						}
						if (unitType == "الصغرى")
						{
							if (item.ItemUnitType == "الصغرى")
							{
								itemQuantity5.CurrentBalance += item.Amount * (decimal?)Quantity;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "المتوسطة")
							{
								itemQuantity5.CurrentBalance += item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "الكبرى")
							{
								itemQuantity5.CurrentBalance += item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit;
								result = gTSdbEntities.SaveChanges();
							}
						}
						if (unitType == "المتوسطة")
						{
							if (item.ItemUnitType == "الصغرى")
							{
								itemQuantity5.CurrentBalance += item.Amount * (decimal?)Quantity * item_Items2.ConvertMediumUnit;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "المتوسطة")
							{
								itemQuantity5.CurrentBalance += item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items2.ConvertMediumUnit;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "الكبرى")
							{
								itemQuantity5.CurrentBalance += item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit * item_Items2.ConvertMediumUnit;
								result = gTSdbEntities.SaveChanges();
							}
						}
						if (unitType == "الكبرى")
						{
							if (item.ItemUnitType == "الصغرى")
							{
								itemQuantity5.CurrentBalance += item.Amount * (decimal?)Quantity * item_Items2.ConvertBigUnit;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "المتوسطة")
							{
								itemQuantity5.CurrentBalance += item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items2.ConvertBigUnit;
								result = gTSdbEntities.SaveChanges();
							}
							if (item.ItemUnitType == "الكبرى")
							{
								itemQuantity5.CurrentBalance += item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit * item_Items2.ConvertBigUnit;
								result = gTSdbEntities.SaveChanges();
							}
						}
						continue;
					}
					ItemQuantity itemQuantity6 = new ItemQuantity();
					int itemQuantityID2 = Convert.ToInt32(gTSdbEntities.ItemQuantities.Select((ItemQuantity p) => p.ItemQuantityID).DefaultIfEmpty(0).Max()) + 1;
					itemQuantity6.ItemQuantityID = itemQuantityID2;
					itemQuantity6.ItemID = item.ItemID_Complant;
					if (unitType == "الصغرى")
					{
						if (item.ItemUnitType == "الصغرى")
						{
							itemQuantity6.CurrentBalance = item.Amount * (decimal?)Quantity;
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "المتوسطة")
						{
							itemQuantity6.CurrentBalance = item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit;
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "الكبرى")
						{
							itemQuantity6.CurrentBalance = item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit;
							result = gTSdbEntities.SaveChanges();
						}
					}
					if (unitType == "المتوسطة")
					{
						if (item.ItemUnitType == "الصغرى")
						{
							itemQuantity6.CurrentBalance = item.Amount * (decimal?)Quantity * item_Items2.ConvertMediumUnit;
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "المتوسطة")
						{
							itemQuantity6.CurrentBalance = item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items2.ConvertMediumUnit;
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "الكبرى")
						{
							itemQuantity6.CurrentBalance = item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit * item_Items2.ConvertMediumUnit;
							result = gTSdbEntities.SaveChanges();
						}
					}
					if (unitType == "الكبرى")
					{
						if (item.ItemUnitType == "الصغرى")
						{
							itemQuantity6.CurrentBalance = item.Amount * (decimal?)Quantity * item_Items2.ConvertBigUnit;
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "المتوسطة")
						{
							itemQuantity6.CurrentBalance = item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items2.ConvertBigUnit;
							result = gTSdbEntities.SaveChanges();
						}
						if (item.ItemUnitType == "الكبرى")
						{
							itemQuantity6.CurrentBalance = item.Amount * (decimal?)Quantity * item_Items3.ConvertMediumUnit * item_Items3.ConvertBigUnit * item_Items2.ConvertBigUnit;
							result = gTSdbEntities.SaveChanges();
						}
					}
					itemQuantity6.BeginningInventory = 0m;
					itemQuantity6.BeginningInventoryPrice = 0m;
					itemQuantity6.OpeningBalance = 0m;
					itemQuantity6.StoreID = item.StoreID;
					gTSdbEntities.ItemQuantities.Add(itemQuantity6);
					result = gTSdbEntities.SaveChanges();
				}
			}
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
		decimal num2 = Convert.ToDecimal(db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)Itemid).Sum((ItemQuantity x) => x.CurrentBalance));
		num = num2;
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
		decimal num2 = Convert.ToDecimal(db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)Itemid && x.StoreID == (int?)storeid).Sum((ItemQuantity x) => x.CurrentBalance));
		num = num2;
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

	public List<Order_OrderReturnDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.Order_OrderReturnDetails.Where((Order_OrderReturnDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_Order_OrderReturn_Result> GetSelectAllPurchases(bool IsNum)
	{
		return db.Select_Order_OrderReturn(IsNum, LoginDetails.BranchID).ToList();
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

	public int InsertItems(DataTable Items, int Purchese_ID, int CreditNote, int FromPurchID, bool IsWait)
	{
		int result = 0;
		if (!AvergItemTable.Columns.Contains("ItemId"))
		{
			AvergItemTable.Columns.Add("ItemId", typeof(int));
		}
		AvergItemTable.Clear();
		foreach (DataRow row in Items.Rows)
		{
			if (string.IsNullOrEmpty(row["ItemCode"].ToString()))
			{
				continue;
			}
			string Itemcod = row["ItemCode"].ToString();
			Item_Items item_Items = db.Item_Items.FirstOrDefault((Item_Items x) => x.Item_code == Itemcod);
			if (item_Items == null)
			{
				continue;
			}
			decimal num = Convert.ToDecimal(row["Quantity"]);
			if (!(num < 1m))
			{
				Order_OrderReturnDetails entity = new Order_OrderReturnDetails
				{
					SnOrderDetails = Convert.ToInt32(row["SnOrder"]),
					Purchese_ID = Purchese_ID,
					FromPurchID = FromPurchID,
					BranchID = LoginDetails.BranchID,
					ItemID = item_Items.ItemId,
					ItemUnitID = GetUnitIdByName(row["UnitName"].ToString()),
					IsWaiting = IsWait,
					SmallUnitPrice = Convert.ToDecimal(row["UnitPrice"]),
					Quantity = Convert.ToDecimal(row["Quantity"]),
					StoreID = GetStoreIDByName(row["StoreName"].ToString()),
					CostAverg = Convert.ToDecimal(row["AvergItem"]),
					UnitPrice = Math.Round(Convert.ToDecimal(row["UnitPrice"]), DecimalNum),
					TotalPrice = Math.Round(Convert.ToDecimal(row["SumPrice"]), DecimalNum),
					NetUnitPrice = Math.Round(Convert.ToDecimal(row["UnitPriceDisc"]), DecimalNum),
					NetTotalPrice = Math.Round(Convert.ToDecimal(row["Net"]), DecimalNum),
					VAT = Math.Round(Convert.ToDecimal(row["ItemVat"]), DecimalNum),
					ItemUnitType = row["UnitType"].ToString(),
					VAT_Discount = Math.Round(Convert.ToDecimal(row["ItemVatDisc"]), DecimalNum),
					ItemNote = row["ItemNote"].ToString(),
					CreditNote = CreditNote,
					SumPriceInstall = ((row["SumPriceInstall"] != DBNull.Value) ? Convert.ToDecimal(row["SumPriceInstall"]) : 0m),
					SumPriceInstallDisc = ((row["SumPriceInstallDisc"] != DBNull.Value) ? Convert.ToDecimal(row["SumPriceInstallDisc"]) : 0m),
					Originalquantity = ((row["SellQuantity"] != DBNull.Value) ? Convert.ToDecimal(row["SellQuantity"]) : 0m),
					PercentDisc = ((row["PercentDisc"] != DBNull.Value) ? Convert.ToDecimal(row["PercentDisc"]) : 0m),
					AmountDisc = ((row["AmountDisc"] != DBNull.Value) ? Convert.ToDecimal(row["AmountDisc"]) : 0m)
				};
				int storeIDByName = GetStoreIDByName(row["StoreName"].ToString());
				db.Order_OrderReturnDetails.Add(entity);
				result = db.SaveChanges();
				AvergItemTable.Rows.Add(item_Items.ItemId);
				if (item_Items.item_Type == 1 || item_Items.item_Type == 3)
				{
					ItemQuantityIN(item_Items.ItemId, Convert.ToDecimal(row["Quantity"]), row["UnitType"].ToString(), storeIDByName, 1);
				}
				if (IsWait)
				{
					ItemQuantityOut(item_Items.ItemId, Convert.ToDecimal(row["Quantity"]), row["UnitType"].ToString(), LoginDetails.StoreIDWaiting, 1);
				}
			}
		}
		if (AvergItemTable.Rows.Count > 0)
		{
			ExecuteBase(AvergItemTable);
		}
		return result;
	}

	public int InsertItemsNoPurchCode(DataTable Items, int Purchese_ID, int CreditNote, int FromPurchID, bool IsWait)
	{
		int result = 0;
		if (!AvergItemTable.Columns.Contains("ItemId"))
		{
			AvergItemTable.Columns.Add("ItemId", typeof(int));
		}
		AvergItemTable.Clear();
		foreach (DataRow row in Items.Rows)
		{
			if (string.IsNullOrEmpty(row["ItemCode"].ToString()))
			{
				continue;
			}
			string Itemcod = row["ItemCode"].ToString();
			Item_Items item_Items = db.Item_Items.FirstOrDefault((Item_Items x) => x.Item_code == Itemcod);
			if (item_Items == null)
			{
				continue;
			}
			decimal num = Convert.ToDecimal(row["Quantity"]);
			if (!(num < 1m))
			{
				Order_OrderReturnDetails entity = new Order_OrderReturnDetails
				{
					Purchese_ID = Purchese_ID,
					FromPurchID = FromPurchID,
					BranchID = LoginDetails.BranchID,
					ItemID = item_Items.ItemId,
					ItemUnitID = GetUnitIdByName(row["UnitName"].ToString()),
					IsWaiting = IsWait,
					SmallUnitPrice = Convert.ToDecimal(row["UnitPrice"]),
					Quantity = Convert.ToDecimal(row["Quantity"]),
					StoreID = GetStoreIDByName(row["StoreName"].ToString()),
					CostAverg = Convert.ToDecimal(row["AvergItem"]),
					UnitPrice = Math.Round(Convert.ToDecimal(row["UnitPrice"]), DecimalNum),
					TotalPrice = Math.Round(Convert.ToDecimal(row["SumPrice"]), DecimalNum),
					NetUnitPrice = Math.Round(Convert.ToDecimal(row["UnitPriceDisc"]), DecimalNum),
					NetTotalPrice = Math.Round(Convert.ToDecimal(row["Net"]), DecimalNum),
					VAT = Math.Round(Convert.ToDecimal(row["ItemVat"]), DecimalNum),
					ItemUnitType = row["UnitType"].ToString(),
					VAT_Discount = Math.Round(Convert.ToDecimal(row["ItemVatDisc"]), DecimalNum),
					ItemNote = row["ItemNote"].ToString(),
					CreditNote = CreditNote,
					SumPriceInstall = ((row["SumPriceInstall"] != DBNull.Value) ? Convert.ToDecimal(row["SumPriceInstall"]) : 0m),
					SumPriceInstallDisc = ((row["SumPriceInstallDisc"] != DBNull.Value) ? Convert.ToDecimal(row["SumPriceInstallDisc"]) : 0m),
					Originalquantity = ((row["SellQuantity"] != DBNull.Value) ? Convert.ToDecimal(row["SellQuantity"]) : 0m),
					PercentDisc = ((row["PercentDisc"] != DBNull.Value) ? Convert.ToDecimal(row["PercentDisc"]) : 0m),
					AmountDisc = ((row["AmountDisc"] != DBNull.Value) ? Convert.ToDecimal(row["AmountDisc"]) : 0m)
				};
				int storeIDByName = GetStoreIDByName(row["StoreName"].ToString());
				db.Order_OrderReturnDetails.Add(entity);
				result = db.SaveChanges();
				AvergItemTable.Rows.Add(item_Items.ItemId);
				if (item_Items.item_Type == 1 || item_Items.item_Type == 3)
				{
					ItemQuantityIN(item_Items.ItemId, Convert.ToDecimal(row["Quantity"]), row["UnitType"].ToString(), storeIDByName, 1);
				}
				if (IsWait)
				{
					ItemQuantityOut(item_Items.ItemId, Convert.ToDecimal(row["Quantity"]), row["UnitType"].ToString(), LoginDetails.StoreIDWaiting, 1);
				}
			}
		}
		if (AvergItemTable.Rows.Count > 0)
		{
			ExecuteBase(AvergItemTable);
		}
		return result;
	}

	public void ExecuteBase(DataTable AvergItemTable)
	{
		try
		{
			using SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
			using SqlCommand sqlCommand = new SqlCommand("Get_AvergItemByTable", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandTimeout = 30;
			SqlParameter sqlParameter = sqlCommand.Parameters.AddWithValue("@AvergItem", AvergItemTable);
			sqlParameter.SqlDbType = SqlDbType.Structured;
			sqlConnection.Open();
			sqlCommand.ExecuteNonQuery();
		}
		catch (SqlException)
		{
		}
		catch (Exception)
		{
		}
	}

	public int InsertPurch(out int Code, int FromPurchID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, int PurchType, int CostCenter, int ProjectId, int ProjectID, string Note, string NoteNum, bool ISWait, decimal CostAverg, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, bool ISPurReturnNum, decimal TobaccoTax, decimal AllTax, decimal Cash, decimal Bank, int CreditNote, decimal AllDis)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.Order_OrderReturn.Select((Order_OrderReturn p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Order_OrderReturn
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		Order_OrderReturn order_OrderReturn = new Order_OrderReturn();
		order_OrderReturn.ID = iD;
		CachCode = num2;
		Code = num2;
		order_OrderReturn.FromPurchesID = FromPurchID;
		order_OrderReturn.BranchID = LoginDetails.BranchID;
		order_OrderReturn.ISPurReturnNum = ISPurReturnNum;
		order_OrderReturn.PurBranchID = num2;
		order_OrderReturn.SupplierID = SupplierID;
		order_OrderReturn.SupplierName = SupplierName;
		order_OrderReturn.SupplierPhone = SupplierPhone;
		order_OrderReturn.SupplierVatNum = SupplierVatNum;
		order_OrderReturn.Purchases_Date = Purchases_Date;
		order_OrderReturn.Order_Paymant_Type = PurchType;
		order_OrderReturn.ProjectId = ProjectID;
		order_OrderReturn.CostCentersID = CostCenter;
		order_OrderReturn.ProjectId = ProjectId;
		order_OrderReturn.Note = Note;
		order_OrderReturn.IsWaiting = ISWait;
		order_OrderReturn.CostAverg = CostAverg;
		order_OrderReturn.NoteNum = NoteNum;
		order_OrderReturn.TotalPrices = TotalPrices;
		order_OrderReturn.Tax = Tax;
		order_OrderReturn.Safy = Safy;
		order_OrderReturn.DiscountNum = DiscountNum;
		order_OrderReturn.DiscountPerantage = DiscountPerantage;
		order_OrderReturn.Tax_Discount = Tax_Discount;
		order_OrderReturn.TotalPrices_Discount = TotalPrices_Discount;
		order_OrderReturn.Net = Net;
		order_OrderReturn.TobaccoTax = TobaccoTax;
		order_OrderReturn.AllTax = AllTax;
		order_OrderReturn.Cash = Cash;
		order_OrderReturn.Bank = Bank;
		order_OrderReturn.CreditNote = CreditNote;
		order_OrderReturn.AllDis = AllDis;
		order_OrderReturn.UserID_Add = LoginDetails.UserID;
		order_OrderReturn.UserBranch_Add = LoginDetails.BranchID;
		order_OrderReturn.UserMacAddress_Add = LoginDetails.macAddress;
		order_OrderReturn.UserDate_Add = DateTime.Now;
		db.Order_OrderReturn.Add(order_OrderReturn);
		num = db.SaveChanges();
		Order_Orders order_Orders = db.Order_Orders.Where((Order_Orders x) => x.PurBranchID == (int?)FromPurchID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (order_Orders != null)
		{
			order_Orders.CreditNote = CreditNote;
			order_Orders.IsWaiting = false;
			db.SaveChanges();
		}
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID)
	{
		List<Order_OrderReturnDetails> list = db.Order_OrderReturnDetails.Where((Order_OrderReturnDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Order_OrderReturnDetails item in list)
		{
			Item_Items item_Items = db.Item_Items.Where((Item_Items x) => (int?)x.ItemId == item.ItemID).FirstOrDefault();
			ItemQuantityOut(Convert.ToInt32(item.ItemID), Convert.ToDecimal(item.Quantity), item.ItemUnitType, Convert.ToInt32(item.StoreID), 1);
			Order_OrderReturn order_OrderReturn = db.Order_OrderReturn.Where((Order_OrderReturn z) => z.ID == PurID).FirstOrDefault();
			if (Convert.ToBoolean(order_OrderReturn.IsWaiting))
			{
				ItemQuantityIN(Convert.ToInt32(item.ItemID), Convert.ToDecimal(item.Quantity), item.ItemUnitType, LoginDetails.StoreIDWaiting, 1);
			}
			db.Order_OrderReturnDetails.Attach(item);
			db.Order_OrderReturnDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, int PurchType, int CostCenter, int ProjectId, int ProjectID, string Note, string NoteNum, bool ISWait, decimal CostAverg, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal TobaccoTax, decimal AllTax, decimal Cash, decimal Bank)
	{
		int result = 0;
		Order_OrderReturn order_OrderReturn = db.Order_OrderReturn.Where((Order_OrderReturn x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.Order_OrderReturn.Where((Order_OrderReturn x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (order_OrderReturn != null)
		{
			order_OrderReturn.SupplierID = SupplierID;
			order_OrderReturn.SupplierName = SupplierName;
			order_OrderReturn.SupplierPhone = SupplierPhone;
			order_OrderReturn.SupplierVatNum = SupplierVatNum;
			order_OrderReturn.Purchases_Date = Purchases_Date;
			order_OrderReturn.Order_Paymant_Type = PurchType;
			order_OrderReturn.ProjectId = ProjectID;
			order_OrderReturn.CostCentersID = CostCenter;
			order_OrderReturn.ProjectId = ProjectId;
			order_OrderReturn.Note = Note;
			order_OrderReturn.NoteNum = NoteNum;
			order_OrderReturn.IsWaiting = ISWait;
			order_OrderReturn.CostAverg = CostAverg;
			order_OrderReturn.TotalPrices = TotalPrices;
			order_OrderReturn.Tax = Tax;
			order_OrderReturn.Safy = Safy;
			order_OrderReturn.DiscountNum = DiscountNum;
			order_OrderReturn.DiscountPerantage = DiscountPerantage;
			order_OrderReturn.Tax_Discount = Tax_Discount;
			order_OrderReturn.TotalPrices_Discount = TotalPrices_Discount;
			order_OrderReturn.Net = Net;
			order_OrderReturn.TobaccoTax = TobaccoTax;
			order_OrderReturn.AllTax = AllTax;
			order_OrderReturn.Cash = Cash;
			order_OrderReturn.Bank = Bank;
			order_OrderReturn.UserID_Update = LoginDetails.UserID;
			order_OrderReturn.UserBranch_Update = LoginDetails.GroupID;
			order_OrderReturn.UserMacAddress_Update = LoginDetails.macAddress;
			order_OrderReturn.UserDate_Update = DateTime.Now;
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
		Order_OrderReturn order_OrderReturn = db.Order_OrderReturn.Where((Order_OrderReturn x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int PurchCode = Convert.ToInt32(order_OrderReturn.FromPurchesID);
		if (order_OrderReturn != null)
		{
			deleteItemsByPurcahesId(PurID);
			db.Order_OrderReturn.Attach(order_OrderReturn);
			db.Order_OrderReturn.Remove(order_OrderReturn);
			result = db.SaveChanges();
		}
		Order_Orders order_Orders = db.Order_Orders.Where((Order_Orders x) => x.PurBranchID == (int?)PurchCode && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (order_Orders != null)
		{
			order_Orders.CreditNote = Convert.ToInt32(order_Orders.CreditNote) - 1;
			db.SaveChanges();
		}
		AvergItemTable.Clear();
		foreach (DataRow row in Items.Rows)
		{
			string Itemcod = row["ItemCode"].ToString();
			if (!string.IsNullOrEmpty(Itemcod))
			{
				Item_Items item_Items = db.Item_Items.FirstOrDefault((Item_Items x) => x.Item_code == Itemcod);
				AvergItemTable.Rows.Add(item_Items.ItemId);
			}
		}
		if (AvergItemTable.Rows.Count > 0)
		{
			ExecuteBase(AvergItemTable);
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
		tran_Tran.TranTypeID = 13;
		tran_Tran.DocCode = NoteNum;
		tran_Tran.TranDate = PurDate;
		tran_Tran.BranchID = LoginDetails.BranchID;
		if (PurType == 2)
		{
			tran_Tran.Note = " مرتجعات مبيعات أجلة بدون فاتورة رقم  ( " + PurID + " ) ط­ /  " + Name;
		}
		else
		{
			tran_Tran.Note = " مرتجعات مبيعات نقدية بدون فاتورة رقم ( " + PurID + " )";
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

	public int InsertTranDetailsCash(int TranID, int PurID, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal TotalCash, int NoteNum, int ProjectId, decimal Cash, decimal Bank)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = " تكلفة فاتورة مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurID + " )  ";
		tran_TranDetails.Debit = TotalCost;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = 0;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = " تكلفة فاتورة مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurID + " )  ";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = TotalCost;
		tran_TranDetails2.CostCentersID = 0;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
			tran_TranDetails3.TranSn = TranID;
			tran_TranDetails3.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails3.AccounIindex = 0;
			tran_TranDetails3.TranDesc = " ضريبة مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurID + " )  ";
			tran_TranDetails3.Debit = Vat;
			tran_TranDetails3.Credit = 0m;
			tran_TranDetails3.CostCentersID = NoteNum;
			tran_TranDetails3.ProjectId = ProjectId;
			tran_TranDetails3.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails3);
			db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
		tran_TranDetails4.TranSn = TranID;
		tran_TranDetails4.Account_Sn = LoginDetails.Acc_OrderReturnCash;
		tran_TranDetails4.AccounIindex = 1;
		tran_TranDetails4.TranDesc = "  مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurID + " ) ";
		tran_TranDetails4.Debit = TotalSell;
		tran_TranDetails4.Credit = 0m;
		tran_TranDetails4.CostCentersID = NoteNum;
		tran_TranDetails4.ProjectId = ProjectId;
		tran_TranDetails4.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails4);
		num = db.SaveChanges();
		Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
		tran_TranDetails5.TranSn = TranID;
		tran_TranDetails5.Account_Sn = LoginDetails.Acc_CashBox;
		tran_TranDetails5.AccounIindex = 1;
		tran_TranDetails5.TranDesc = "  مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurID + " ) ";
		tran_TranDetails5.Debit = 0m;
		tran_TranDetails5.Credit = TotalCash;
		tran_TranDetails5.CostCentersID = 0;
		tran_TranDetails5.ProjectId = ProjectId;
		tran_TranDetails5.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails5);
		num = db.SaveChanges();
		if (TotalTobacc > 0m)
		{
			Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
			tran_TranDetails6.TranSn = TranID;
			tran_TranDetails6.Account_Sn = LoginDetails.TobaccoNum;
			tran_TranDetails6.AccounIindex = 1;
			tran_TranDetails6.TranDesc = "  حساب ضريبة التبغ   ( " + PurID + " ) ";
			tran_TranDetails6.Credit = 0m;
			tran_TranDetails6.Debit = TotalTobacc;
			tran_TranDetails6.CostCentersID = NoteNum;
			tran_TranDetails6.ProjectId = ProjectId;
			tran_TranDetails6.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails6);
			num = db.SaveChanges();
		}
		return num;
	}

	public int InsertTranDetailsAjila(int TranID, int PurID, int SuppNum, string Name, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal TotalCash, int NoteNum, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = " تكلفة فاتورة مرتجعات مبيعات آجلة بدون فاتورة رقم  ( " + PurID + " ) ط­/ " + Name + " ";
		tran_TranDetails.Debit = TotalCost;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = 0;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = " تكلفة فاتورة مرتجعات مبيعات آجلة بدون فاتورة رقم  ( " + PurID + " ) ط­/ " + Name + " ";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = TotalCost;
		tran_TranDetails2.CostCentersID = 0;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
			tran_TranDetails3.TranSn = TranID;
			tran_TranDetails3.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails3.AccounIindex = 0;
			tran_TranDetails3.TranDesc = " ضريبة مرتجعات مبيعات آجلة بدون فاتورة رقم  ( " + PurID + " ) ط­/ " + Name + " ";
			tran_TranDetails3.Debit = Vat;
			tran_TranDetails3.Credit = 0m;
			tran_TranDetails3.CostCentersID = NoteNum;
			tran_TranDetails3.ProjectId = ProjectId;
			tran_TranDetails3.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails3);
			db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
		tran_TranDetails4.TranSn = TranID;
		tran_TranDetails4.Account_Sn = LoginDetails.Acc_OrderReturnCash;
		tran_TranDetails4.AccounIindex = 1;
		tran_TranDetails4.TranDesc = "  مرتجعات مبيعات آجلة بدون فاتورة رقم  ( " + PurID + " ) ط­/ " + Name + " ";
		tran_TranDetails4.Debit = TotalSell;
		tran_TranDetails4.Credit = 0m;
		tran_TranDetails4.CostCentersID = NoteNum;
		tran_TranDetails4.ProjectId = ProjectId;
		tran_TranDetails4.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails4);
		num = db.SaveChanges();
		Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
		tran_TranDetails5.TranSn = TranID;
		tran_TranDetails5.Account_Sn = SuppNum;
		tran_TranDetails5.AccounIindex = 1;
		tran_TranDetails5.TranDesc = "  مرتجعات مبيعات آجلة بدون فاتورة رقم  ( " + PurID + " ) ط­/ " + Name;
		tran_TranDetails5.Debit = 0m;
		tran_TranDetails5.Credit = TotalCash;
		tran_TranDetails5.CostCentersID = 0;
		tran_TranDetails5.ProjectId = ProjectId;
		tran_TranDetails5.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails5);
		num = db.SaveChanges();
		if (TotalTobacc > 0m)
		{
			Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
			tran_TranDetails6.TranSn = TranID;
			tran_TranDetails6.Account_Sn = LoginDetails.TobaccoNum;
			tran_TranDetails6.AccounIindex = 1;
			tran_TranDetails6.TranDesc = "  حساب ضريبة التبغ   ( " + PurID + " ) ";
			tran_TranDetails6.Credit = 0m;
			tran_TranDetails6.Debit = TotalTobacc;
			tran_TranDetails6.CostCentersID = NoteNum;
			tran_TranDetails6.ProjectId = ProjectId;
			tran_TranDetails6.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails6);
			num = db.SaveChanges();
		}
		return num;
	}

	public int UpdateTran(out int TRNIDDS, int PurID, int PurType, string Name, string NoteNum, DateTime PurDate, int TranTypeID)
	{
		int result = 0;
		TRNIDDS = 0;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)PurID && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_Tran != null)
		{
			tran_Tran.DocCode = NoteNum;
			tran_Tran.TranDate = PurDate;
			tran_Tran.BranchID = LoginDetails.BranchID;
			if (PurType == 2)
			{
				tran_Tran.Note = " مرتجعات مبيعات أجلة بدون رقم فاتورة رقم  ( " + PurID + " ) ط­ /  " + Name + " معدلة ";
			}
			else
			{
				tran_Tran.Note = " مرتجعات مبيعات نقدية بدون رقم فاتورة رقم ( " + PurID + " ) معدلة ";
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

	public int UpdateTranDetailsCash(int TranID, int PurID, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal TotalCash, int NoteNum, int ProjectId, decimal Cash, decimal Bank)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = " تكلفة فاتورة مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails.Debit = TotalCost;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = 0;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = " تكلفة فاتورة مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = TotalCost;
		tran_TranDetails2.CostCentersID = 0;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
			tran_TranDetails3.TranSn = TranID;
			tran_TranDetails3.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails3.AccounIindex = 0;
			tran_TranDetails3.TranDesc = " ضريبة مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurID + " ) معدلة";
			tran_TranDetails3.Debit = Vat;
			tran_TranDetails3.Credit = 0m;
			tran_TranDetails3.CostCentersID = NoteNum;
			tran_TranDetails3.ProjectId = ProjectId;
			tran_TranDetails3.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails3);
			db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
		tran_TranDetails4.TranSn = TranID;
		tran_TranDetails4.Account_Sn = LoginDetails.Acc_OrderReturnCash;
		tran_TranDetails4.AccounIindex = 1;
		tran_TranDetails4.TranDesc = "  مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails4.Debit = TotalSell;
		tran_TranDetails4.Credit = 0m;
		tran_TranDetails4.CostCentersID = NoteNum;
		tran_TranDetails4.ProjectId = ProjectId;
		tran_TranDetails4.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails4);
		num = db.SaveChanges();
		Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
		tran_TranDetails5.TranSn = TranID;
		tran_TranDetails5.Account_Sn = LoginDetails.Acc_CashBox;
		tran_TranDetails5.AccounIindex = 1;
		tran_TranDetails5.TranDesc = "  مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails5.Debit = 0m;
		tran_TranDetails5.Credit = TotalCash;
		tran_TranDetails5.CostCentersID = 0;
		tran_TranDetails5.ProjectId = ProjectId;
		tran_TranDetails5.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails5);
		num = db.SaveChanges();
		if (TotalTobacc > 0m)
		{
			Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
			tran_TranDetails6.TranSn = TranID;
			tran_TranDetails6.Account_Sn = LoginDetails.TobaccoNum;
			tran_TranDetails6.AccounIindex = 1;
			tran_TranDetails6.TranDesc = "  حساب ضريبة التبغ   ( " + PurID + " ) معدلة";
			tran_TranDetails6.Credit = 0m;
			tran_TranDetails6.Debit = TotalTobacc;
			tran_TranDetails6.CostCentersID = NoteNum;
			tran_TranDetails6.ProjectId = ProjectId;
			tran_TranDetails6.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails6);
			num = db.SaveChanges();
		}
		return num;
	}

	public int UpdateTranDetailsAjila(int TranID, int PurID, int SuppNum, string Name, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal TotalCash, int NoteNum, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = " تكلفة فاتورة مرتجعات مبيعات آجلة بدون فاتورة رقم  ( " + PurID + " ) ط­/ " + Name + " معدلة ";
		tran_TranDetails.Debit = TotalCost;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = 0;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = " تكلفة فاتورة مرتجعات مبيعات آجلة بدون فاتورة رقم  ( " + PurID + " ) ط­/ " + Name + " معدلة ";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = TotalCost;
		tran_TranDetails2.CostCentersID = 0;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
			tran_TranDetails3.TranSn = TranID;
			tran_TranDetails3.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails3.AccounIindex = 0;
			tran_TranDetails3.TranDesc = " ضريبة مرتجعات مبيعات آجلة بدون فاتورة رقم  ( " + PurID + " ) ط­/ " + Name + " معدلة ";
			tran_TranDetails3.Debit = Vat;
			tran_TranDetails3.Credit = 0m;
			tran_TranDetails3.CostCentersID = NoteNum;
			tran_TranDetails3.ProjectId = ProjectId;
			tran_TranDetails3.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails3);
			db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
		tran_TranDetails4.TranSn = TranID;
		tran_TranDetails4.Account_Sn = LoginDetails.Acc_OrderReturnCash;
		tran_TranDetails4.AccounIindex = 1;
		tran_TranDetails4.TranDesc = "  مرتجعات مبيعات آجلة بدون فاتورة رقم  ( " + PurID + " ) ط­/ " + Name + " معدلة ";
		tran_TranDetails4.Debit = TotalSell;
		tran_TranDetails4.Credit = 0m;
		tran_TranDetails4.CostCentersID = NoteNum;
		tran_TranDetails4.ProjectId = ProjectId;
		tran_TranDetails4.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails4);
		num = db.SaveChanges();
		Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
		tran_TranDetails5.TranSn = TranID;
		tran_TranDetails5.Account_Sn = SuppNum;
		tran_TranDetails5.AccounIindex = 1;
		tran_TranDetails5.TranDesc = "  مرتجعات مبيعات آجلة بدون فاتورة رقم  ( " + PurID + " ) ط­/ " + Name + " معدلة ";
		tran_TranDetails5.Debit = 0m;
		tran_TranDetails5.Credit = TotalCash;
		tran_TranDetails5.CostCentersID = 0;
		tran_TranDetails5.ProjectId = ProjectId;
		tran_TranDetails5.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails5);
		num = db.SaveChanges();
		if (TotalTobacc > 0m)
		{
			Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
			tran_TranDetails6.TranSn = TranID;
			tran_TranDetails6.Account_Sn = LoginDetails.TobaccoNum;
			tran_TranDetails6.AccounIindex = 1;
			tran_TranDetails6.TranDesc = "  حساب ضريبة التبغ   ( " + PurID + " ) معدلة";
			tran_TranDetails6.Credit = 0m;
			tran_TranDetails6.Debit = TotalTobacc;
			tran_TranDetails6.CostCentersID = NoteNum;
			tran_TranDetails6.ProjectId = ProjectId;
			tran_TranDetails6.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails6);
			num = db.SaveChanges();
		}
		return num;
	}

	public int DeleteTran(int Code, int TranTypeID)
	{
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ID == TranID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_Tran != null)
		{
			db.Tran_Tran.Attach(tran_Tran);
			db.Tran_Tran.Remove(tran_Tran);
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteTranDetails(int Code, int TranTypeID)
	{
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
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
		tran_Tran.TranTypeID = 9;
		tran_Tran.DocCode = NoteNum;
		tran_Tran.TranDate = PurDate;
		tran_Tran.BranchID = LoginDetails.BranchID;
		if (PurType == 2)
		{
			tran_Tran.Note = "مرتجعات مبيعات أجلة رقم ( " + PurID + " ) ط­ /  " + Name;
		}
		else
		{
			tran_Tran.Note = "مرتجعات مبيعات نقدية رقم ( " + PurID + " )";
		}
		tran_Tran.UserID_Add = LoginDetails.UserID;
		tran_Tran.UserBranch_Add = LoginDetails.BranchID;
		tran_Tran.UserMacAddress_Add = LoginDetails.macAddress;
		tran_Tran.UserDate_Add = DateTime.Now;
		db.Tran_Tran.Add(tran_Tran);
		num = db.SaveChanges();
		TRNIDDS = num2;
		return TRNIDDS;
	}

	public int InsertTranDetailsCashFrmNum(int TranID, int PurReturnID, int ORDID, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal TotalCash, int NoteNum, int ProjectId, decimal Cash, decimal Bank)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_OrderReturnCash;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = "مرتجع مبيعات نقدية  رقم  ( " + PurReturnID + " )  من فاتورة بيع رقم   ( " + ORDID + " )  ";
		tran_TranDetails.Debit = TotalSell;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = NoteNum;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
			tran_TranDetails2.TranSn = TranID;
			tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails2.AccounIindex = 1;
			tran_TranDetails2.TranDesc = " ضريبة مرتجعات مبيعات نقدية  رقم  ( " + PurReturnID + " )  ";
			tran_TranDetails2.Debit = Vat;
			tran_TranDetails2.Credit = 0m;
			tran_TranDetails2.CostCentersID = NoteNum;
			tran_TranDetails2.ProjectId = ProjectId;
			tran_TranDetails2.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails2);
			db.SaveChanges();
		}
		if (Cash > 0m)
		{
			Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
			tran_TranDetails3.TranSn = TranID;
			tran_TranDetails3.Account_Sn = LoginDetails.Acc_CashBox;
			tran_TranDetails3.AccounIindex = 1;
			tran_TranDetails3.TranDesc = "  مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurReturnID + " ) ";
			tran_TranDetails3.Debit = 0m;
			tran_TranDetails3.Credit = Cash;
			tran_TranDetails3.CostCentersID = 0;
			tran_TranDetails3.ProjectId = ProjectId;
			tran_TranDetails3.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails3);
			num = db.SaveChanges();
		}
		if (Bank > 0m)
		{
			Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
			tran_TranDetails4.TranSn = TranID;
			tran_TranDetails4.Account_Sn = LoginDetails.Acc_BankNumCachir;
			tran_TranDetails4.AccounIindex = 1;
			tran_TranDetails4.TranDesc = "  مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurReturnID + " ) ";
			tran_TranDetails4.Debit = 0m;
			tran_TranDetails4.Credit = Bank;
			tran_TranDetails4.CostCentersID = 0;
			tran_TranDetails4.ProjectId = ProjectId;
			tran_TranDetails4.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails4);
			num = db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
		tran_TranDetails5.TranSn = TranID;
		tran_TranDetails5.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails5.AccounIindex = 3;
		tran_TranDetails5.TranDesc = "مرتجعات مبيعات نقدية  رقم  ( " + PurReturnID + " )  ";
		tran_TranDetails5.Debit = TotalCost;
		tran_TranDetails5.Credit = 0m;
		tran_TranDetails5.CostCentersID = 0;
		tran_TranDetails5.ProjectId = ProjectId;
		tran_TranDetails5.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails5);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
		tran_TranDetails6.TranSn = TranID;
		tran_TranDetails6.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails6.AccounIindex = 4;
		tran_TranDetails6.TranDesc = "  مرتجعات مرتجعات مبيعات نقدية  رقم  ( " + PurReturnID + " ) ";
		tran_TranDetails6.Debit = 0m;
		tran_TranDetails6.Credit = TotalCost;
		tran_TranDetails6.CostCentersID = 0;
		tran_TranDetails6.ProjectId = ProjectId;
		tran_TranDetails6.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails6);
		num = db.SaveChanges();
		if (TotalTobacc > 0m)
		{
			Tran_TranDetails tran_TranDetails7 = new Tran_TranDetails();
			tran_TranDetails7.TranSn = TranID;
			tran_TranDetails7.Account_Sn = LoginDetails.TobaccoNum;
			tran_TranDetails7.AccounIindex = 1;
			tran_TranDetails7.TranDesc = "  حساب ضريبة التبغ   ( " + PurReturnID + " ) ";
			tran_TranDetails7.Credit = 0m;
			tran_TranDetails7.Debit = TotalTobacc;
			tran_TranDetails7.CostCentersID = NoteNum;
			tran_TranDetails7.ProjectId = ProjectId;
			tran_TranDetails7.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails7);
			num = db.SaveChanges();
		}
		return num;
	}

	public int InsertTranDetailsAjilaNUM(int TranID, int PurReturnID, int ORDID, int SuppNum, string Name, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal TotalCash, int NoteNum, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_OrderReturnAgl;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = "مرتجع مبيعات آجلة رقم  ( " + PurReturnID + " )  من فاتورة بيع رقم  ( " + ORDID + " ) ط­/ " + Name + " ";
		tran_TranDetails.Debit = TotalSell;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = NoteNum;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
			tran_TranDetails2.TranSn = TranID;
			tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails2.AccounIindex = 1;
			tran_TranDetails2.TranDesc = " ضريبة مرتجعات مبيعات آجلة  رقم  ( " + PurReturnID + " ) ط­/ " + Name + " ";
			tran_TranDetails2.Debit = Vat;
			tran_TranDetails2.Credit = 0m;
			tran_TranDetails2.CostCentersID = NoteNum;
			tran_TranDetails2.ProjectId = ProjectId;
			tran_TranDetails2.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails2);
			db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = SuppNum;
		tran_TranDetails3.AccounIindex = 2;
		tran_TranDetails3.TranDesc = " مرتجعات مبيعات آجلة رقم  ( " + PurReturnID + " ) ط­/ " + Name + " ";
		tran_TranDetails3.Debit = 0m;
		tran_TranDetails3.Credit = TotalCash;
		tran_TranDetails3.CostCentersID = 0;
		tran_TranDetails3.ProjectId = ProjectId;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		num = db.SaveChanges();
		Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
		tran_TranDetails4.TranSn = TranID;
		tran_TranDetails4.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails4.AccounIindex = 3;
		tran_TranDetails4.TranDesc = "مرتجعات مبيعات آجلة  رقم  ( " + PurReturnID + " )  ";
		tran_TranDetails4.Debit = TotalCost;
		tran_TranDetails4.Credit = 0m;
		tran_TranDetails4.CostCentersID = 0;
		tran_TranDetails4.ProjectId = ProjectId;
		tran_TranDetails4.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails4);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
		tran_TranDetails5.TranSn = TranID;
		tran_TranDetails5.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails5.AccounIindex = 4;
		tran_TranDetails5.TranDesc = "  مرتجعات مرتجعات مبيعات آجلة  رقم  ( " + PurReturnID + " ) ";
		tran_TranDetails5.Debit = 0m;
		tran_TranDetails5.Credit = TotalCost;
		tran_TranDetails5.CostCentersID = 0;
		tran_TranDetails5.ProjectId = ProjectId;
		tran_TranDetails5.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails5);
		num = db.SaveChanges();
		if (TotalTobacc > 0m)
		{
			Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
			tran_TranDetails6.TranSn = TranID;
			tran_TranDetails6.Account_Sn = LoginDetails.TobaccoNum;
			tran_TranDetails6.AccounIindex = 1;
			tran_TranDetails6.TranDesc = "  حساب ضريبة التبغ   ( " + PurReturnID + " ) ";
			tran_TranDetails6.Credit = 0m;
			tran_TranDetails6.Debit = TotalTobacc;
			tran_TranDetails6.CostCentersID = NoteNum;
			tran_TranDetails6.ProjectId = ProjectId;
			tran_TranDetails6.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails6);
			num = db.SaveChanges();
		}
		return num;
	}

	public int UpdateTranNUM(out int TRNIDDS, int PurID, int PurType, string Name, string NoteNum, DateTime PurDate)
	{
		int result = 0;
		TRNIDDS = 0;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)PurID && x.TranTypeID == (int?)9 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_Tran != null)
		{
			tran_Tran.DocCode = NoteNum;
			tran_Tran.TranDate = PurDate;
			tran_Tran.BranchID = LoginDetails.BranchID;
			if (PurType == 2)
			{
				tran_Tran.Note = " مرتجعات مبيعات أجلة رقم  ( " + PurID + " ) ط­ /  " + Name + " معدلة ";
			}
			else
			{
				tran_Tran.Note = " مرتجعات مبيعات نقدية رقم ( " + PurID + " ) معدلة ";
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

	public int UpdateTranDetailsCashNUM(int TranID, int PurReturnID, int ORDID, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal TotalCash, int NoteNum, int ProjectId, decimal Cash, decimal Bank)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_OrderReturnCash;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = "مرتجع مبيعات نقدية رقم  ( " + PurReturnID + " )  من فاتورة بيع رقم   ( " + ORDID + " ) معدلة ";
		tran_TranDetails.Debit = TotalSell;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = NoteNum;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
			tran_TranDetails2.TranSn = TranID;
			tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails2.AccounIindex = 1;
			tran_TranDetails2.TranDesc = " ضريبة مرتجعات مبيعات نقدية  رقم  ( " + PurReturnID + " ) معدلة ";
			tran_TranDetails2.Debit = Vat;
			tran_TranDetails2.Credit = 0m;
			tran_TranDetails2.CostCentersID = NoteNum;
			tran_TranDetails2.ProjectId = ProjectId;
			tran_TranDetails2.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails2);
			db.SaveChanges();
		}
		if (Cash > 0m)
		{
			Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
			tran_TranDetails3.TranSn = TranID;
			tran_TranDetails3.Account_Sn = LoginDetails.Acc_CashBox;
			tran_TranDetails3.AccounIindex = 1;
			tran_TranDetails3.TranDesc = "  مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurReturnID + " ) معدلة";
			tran_TranDetails3.Debit = 0m;
			tran_TranDetails3.Credit = Cash;
			tran_TranDetails3.CostCentersID = 0;
			tran_TranDetails3.ProjectId = ProjectId;
			tran_TranDetails3.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails3);
			num = db.SaveChanges();
		}
		if (Bank > 0m)
		{
			Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
			tran_TranDetails4.TranSn = TranID;
			tran_TranDetails4.Account_Sn = LoginDetails.Acc_BankNumCachir;
			tran_TranDetails4.AccounIindex = 1;
			tran_TranDetails4.TranDesc = "  مرتجعات مبيعات نقدية بدون فاتورة رقم  ( " + PurReturnID + " ) معدلة";
			tran_TranDetails4.Debit = 0m;
			tran_TranDetails4.Credit = Bank;
			tran_TranDetails4.CostCentersID = 0;
			tran_TranDetails4.ProjectId = ProjectId;
			tran_TranDetails4.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails4);
			num = db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
		tran_TranDetails5.TranSn = TranID;
		tran_TranDetails5.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails5.AccounIindex = 3;
		tran_TranDetails5.TranDesc = "مرتجعات مبيعات نقدية  رقم  ( " + PurReturnID + " ) معدلة ";
		tran_TranDetails5.Debit = TotalCost;
		tran_TranDetails5.Credit = 0m;
		tran_TranDetails5.CostCentersID = 0;
		tran_TranDetails5.ProjectId = ProjectId;
		tran_TranDetails5.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails5);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
		tran_TranDetails6.TranSn = TranID;
		tran_TranDetails6.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails6.AccounIindex = 4;
		tran_TranDetails6.TranDesc = "  مرتجعات مرتجعات مبيعات نقدية  رقم  ( " + PurReturnID + " ) معدلة ";
		tran_TranDetails6.Debit = 0m;
		tran_TranDetails6.Credit = TotalCost;
		tran_TranDetails6.CostCentersID = 0;
		tran_TranDetails6.ProjectId = ProjectId;
		tran_TranDetails6.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails6);
		num = db.SaveChanges();
		if (TotalTobacc > 0m)
		{
			Tran_TranDetails tran_TranDetails7 = new Tran_TranDetails();
			tran_TranDetails7.TranSn = TranID;
			tran_TranDetails7.Account_Sn = LoginDetails.TobaccoNum;
			tran_TranDetails7.AccounIindex = 1;
			tran_TranDetails7.TranDesc = "  حساب ضريبة التبغ   ( " + PurReturnID + " ) معدلة";
			tran_TranDetails7.Credit = 0m;
			tran_TranDetails7.Debit = TotalTobacc;
			tran_TranDetails7.CostCentersID = NoteNum;
			tran_TranDetails7.ProjectId = ProjectId;
			tran_TranDetails7.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails7);
			num = db.SaveChanges();
		}
		return num;
	}

	public int UpdateTranDetailsAjilaNUM(int TranID, int PurReturnID, int ORDID, int SuppNum, string Name, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal TotalCash, int NoteNum, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_OrderReturnCash;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = "مرتجع مبيعات آجلة رقم  ( " + PurReturnID + " )  من فاتورة بيع رقم  ( " + ORDID + " ) ط­/ " + Name + " معدلة ";
		tran_TranDetails.Debit = TotalSell;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = NoteNum;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
			tran_TranDetails2.TranSn = TranID;
			tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails2.AccounIindex = 1;
			tran_TranDetails2.TranDesc = " ضريبة مرتجعات مبيعات آجلة  رقم  ( " + PurReturnID + " ) ط­/ " + Name + " معدلة ";
			tran_TranDetails2.Debit = Vat;
			tran_TranDetails2.Credit = 0m;
			tran_TranDetails2.CostCentersID = NoteNum;
			tran_TranDetails2.ProjectId = ProjectId;
			tran_TranDetails2.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails2);
			db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = SuppNum;
		tran_TranDetails3.AccounIindex = 2;
		tran_TranDetails3.TranDesc = "  مرتجعات مرتجعات مبيعات آجلة  رقم  ( " + PurReturnID + " ) ط­/ " + Name + " معدلة ";
		tran_TranDetails3.Debit = 0m;
		tran_TranDetails3.Credit = TotalCash;
		tran_TranDetails3.CostCentersID = 0;
		tran_TranDetails3.ProjectId = ProjectId;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		num = db.SaveChanges();
		Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
		tran_TranDetails4.TranSn = TranID;
		tran_TranDetails4.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails4.AccounIindex = 3;
		tran_TranDetails4.TranDesc = "مرتجعات مبيعات آجلة  رقم  ( " + PurReturnID + " ) معدلة ";
		tran_TranDetails4.Debit = TotalCost;
		tran_TranDetails4.Credit = 0m;
		tran_TranDetails4.CostCentersID = 0;
		tran_TranDetails4.ProjectId = ProjectId;
		tran_TranDetails4.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails4);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
		tran_TranDetails5.TranSn = TranID;
		tran_TranDetails5.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails5.AccounIindex = 4;
		tran_TranDetails5.TranDesc = "  مرتجعات مرتجعات مبيعات آجلة  رقم  ( " + PurReturnID + " ) معدلة ";
		tran_TranDetails5.Debit = 0m;
		tran_TranDetails5.Credit = TotalCost;
		tran_TranDetails5.CostCentersID = 0;
		tran_TranDetails5.ProjectId = ProjectId;
		tran_TranDetails5.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails5);
		num = db.SaveChanges();
		if (TotalTobacc > 0m)
		{
			Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
			tran_TranDetails6.TranSn = TranID;
			tran_TranDetails6.Account_Sn = LoginDetails.TobaccoNum;
			tran_TranDetails6.AccounIindex = 1;
			tran_TranDetails6.TranDesc = "  حساب ضريبة التبغ   ( " + PurReturnID + " ) معدلة";
			tran_TranDetails6.Credit = 0m;
			tran_TranDetails6.Debit = TotalTobacc;
			tran_TranDetails6.CostCentersID = NoteNum;
			tran_TranDetails6.ProjectId = ProjectId;
			tran_TranDetails6.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails6);
			num = db.SaveChanges();
		}
		return num;
	}

	public int DeleteTranNum(int Code, int TranTypeID)
	{
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ID == TranID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_Tran != null)
		{
			db.Tran_Tran.Attach(tran_Tran);
			db.Tran_Tran.Remove(tran_Tran);
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteTranDetailsNum(int Code, int TranTypeID)
	{
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
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

	public int GetItmID(string Code)
	{
		return (from xx in db.Item_Items
			where xx.Item_code == Code
			select xx.ItemId).FirstOrDefault();
	}

	public decimal GetTotalQantityCheckout(int PurcheseID, int ItemOd)
	{
		decimal num = 0m;
		return Convert.ToDecimal(db.Order_CheckoutOrdersDetails.Where((Order_CheckoutOrdersDetails x) => x.FromPurchID == (int?)PurcheseID && x.ItemID == (int?)ItemOd && x.BranchID == (int?)LoginDetails.BranchID).Sum((Order_CheckoutOrdersDetails x) => x.Quantity));
	}

	public decimal GetTotalQantityOrderReturn(int PurcheseID, int ItemOd)
	{
		decimal num = 0m;
		return Convert.ToDecimal(db.Order_OrderReturnDetails.Where((Order_OrderReturnDetails x) => x.FromPurchID == (int?)PurcheseID && x.ItemID == (int?)ItemOd && x.BranchID == (int?)LoginDetails.BranchID).Sum((Order_OrderReturnDetails x) => x.Quantity));
	}

	public decimal GetTotalReturnOrderByOrderId(int ID)
	{
		decimal num = 0m;
		return Convert.ToDecimal(db.Order_OrderReturn.Where((Order_OrderReturn x) => x.FromPurchesID == (int?)ID && x.BranchID == (int?)LoginDetails.BranchID).Sum((Order_OrderReturn x) => x.Net));
	}

	public List<Order_Orders> GetOrders()
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Order_Orders.Where((Order_Orders x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int GetReturnCount(int ID)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Order_OrderReturn.Count((Order_OrderReturn x) => x.FromPurchesID == (int?)ID);
	}

	public void UpdateOrderReturnAFterElctorncFatora(int orderId, string newQRCode, string OrderTypeId, string NormalOrSimpleInvoice, bool ThirdParty, bool NominalInvoice, bool ExportInvoice, bool SummaryInvoice, bool SelfInvoice)
	{
		Order_OrderReturn order_OrderReturn = db.Order_OrderReturn.FirstOrDefault((Order_OrderReturn x) => x.ID == orderId);
		if (order_OrderReturn != null)
		{
			order_OrderReturn.QRCode = newQRCode;
			order_OrderReturn.OrderTypeElectronicInvoiceId = OrderTypeId;
			order_OrderReturn.NormalOrSimpleInvoice = NormalOrSimpleInvoice;
			order_OrderReturn.ThirdParty = ThirdParty;
			order_OrderReturn.NominalInvoice = NominalInvoice;
			order_OrderReturn.ExportInvoice = ExportInvoice;
			order_OrderReturn.SummaryInvoice = SummaryInvoice;
			order_OrderReturn.SelfInvoice = SelfInvoice;
			db.SaveChanges();
		}
	}
}


