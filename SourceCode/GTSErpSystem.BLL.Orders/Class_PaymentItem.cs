using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.Orders;

public class Class_PaymentItem
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

	public List<Order_PaymentItem> GetPurchases()
	{
		return db.Order_PaymentItem.Where((Order_PaymentItem x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Order_PaymentItem> GetPurchases(int Code)
	{
		return db.Order_PaymentItem.Where((Order_PaymentItem x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public Item_Items GetItems(int itemid)
	{
		return db.Item_Items.Where((Item_Items x) => x.ItemId == itemid).FirstOrDefault();
	}

	public Item_Items GetItemsMizan(string BarcodeMizan)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Item_Items.Where((Item_Items x) => x.SmallBarCode1 == BarcodeMizan || x.SmallBarCode2 == BarcodeMizan || x.SmallBarCode3 == BarcodeMizan || x.MediumBarCode1 == BarcodeMizan || x.MediumBarCode2 == BarcodeMizan || x.MediumBarCode3 == BarcodeMizan || x.BigBarCode1 == BarcodeMizan || x.BigBarCode2 == BarcodeMizan || x.BigBarCode3 == BarcodeMizan).FirstOrDefault();
	}

	public Item_Items GetItemsOnly(string Code)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Item_Items.Where((Item_Items x) => x.Item_code == Code || x.SmallBarCode1 == Code || x.SmallBarCode2 == Code || x.SmallBarCode3 == Code || x.MediumBarCode1 == Code || x.MediumBarCode2 == Code || x.MediumBarCode3 == Code || x.BigBarCode1 == Code || x.BigBarCode2 == Code || x.BigBarCode3 == Code).FirstOrDefault();
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

	public int ItemQuantityIn(int ItemID, decimal Quantity, string unitType, int StoreID, int itemType)
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
					ItemQuantity itemQuantity4 = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == item.ItemID_Complant && x.StoreID == (int?)StoreID).FirstOrDefault();
					if (itemQuantity4 != null)
					{
						ItemQuantity itemQuantity5 = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == item.ItemID_Complant && x.StoreID == (int?)StoreID).FirstOrDefault();
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
					ItemQuantity itemQuantity4 = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == item.ItemID_Complant && x.StoreID == (int?)StoreID).FirstOrDefault();
					if (itemQuantity4 != null)
					{
						ItemQuantity itemQuantity5 = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == item.ItemID_Complant && x.StoreID == (int?)StoreID).FirstOrDefault();
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
					itemQuantity6.StoreID = StoreID;
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

	public Item_Items GetItemsCode2(string Code)
	{
		return db.Item_Items.Where((Item_Items x) => x.Item_code == Code || x.item_Name == Code || x.item_Name_English == Code || x.SmallBarCode1 == Code || x.SmallBarCode2 == Code || x.SmallBarCode3 == Code || x.MediumBarCode1 == Code || x.MediumBarCode2 == Code || x.MediumBarCode3 == Code || x.BigBarCode1 == Code || x.BigBarCode2 == Code || x.BigBarCode3 == Code).FirstOrDefault();
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

	public List<Order_PaymentItemDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.Order_PaymentItemDetails.Where((Order_PaymentItemDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_Order_PaymentItem_Result> GetSelectAllPurchases()
	{
		return db.Select_Order_PaymentItem(LoginDetails.BranchID).ToList();
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

	public int InsertItems(DataTable Items, int Purchese_ID)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (!(row["كود_الصنف"].ToString() != ""))
			{
				continue;
			}
			Order_PaymentItemDetails order_PaymentItemDetails = new Order_PaymentItemDetails();
			string Itemcod = row["كود_الصنف"].ToString();
			string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
			int ItemRowChek = Convert.ToInt32(value);
			Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
			int value2 = Convert.ToInt32(db.TblSettings.Select((TblSetting x) => x.StoreID).FirstOrDefault());
			if (item_Items == null)
			{
				continue;
			}
			order_PaymentItemDetails.Purchese_ID = Purchese_ID;
			order_PaymentItemDetails.BranchID = LoginDetails.BranchID;
			order_PaymentItemDetails.ItemID = Convert.ToInt32(item_Items.ItemId);
			string nmae = row["الوحدة"].ToString();
			order_PaymentItemDetails.ItemUnitID = GetUnitIdByName(nmae);
			order_PaymentItemDetails.SmallUnitPrice = Convert.ToDecimal(row["سعر الوحدة"].ToString());
			order_PaymentItemDetails.Quantity = Convert.ToDecimal(row["الكمية"].ToString());
			order_PaymentItemDetails.StoreID = value2;
			order_PaymentItemDetails.LastCost = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"].ToString()), DecimalNum);
			order_PaymentItemDetails.UnitPrice = Math.Round(Convert.ToDecimal(row["سعر الوحدة"].ToString()), DecimalNum);
			order_PaymentItemDetails.TotalPrice = Math.Round(Convert.ToDecimal(row["الاجمالى"].ToString()), DecimalNum);
			order_PaymentItemDetails.NetUnitPrice = Math.Round(Convert.ToDecimal(row["سعربعدالخصم"].ToString()), DecimalNum);
			order_PaymentItemDetails.NetTotalPrice = Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"].ToString()), DecimalNum);
			order_PaymentItemDetails.VAT = Math.Round(Convert.ToDecimal(row["الضربية"].ToString()), DecimalNum);
			order_PaymentItemDetails.ItemUnitType = row["أسم_الوحدة"].ToString();
			order_PaymentItemDetails.VAT_Discount = Math.Round(Convert.ToDecimal(row["الضريبه_بعد_الخصم"].ToString()), DecimalNum);
			order_PaymentItemDetails.StoreID = Convert.ToInt32(row["رقم المخزن"]);
			db.Order_PaymentItemDetails.Add(order_PaymentItemDetails);
			result = db.SaveChanges();
			if (item_Items.item_Type == 1)
			{
				ItemQuantityIn(Convert.ToInt32(value), Convert.ToDecimal(row["الكمية"].ToString()), row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"].ToString()), 1);
			}
			if (item_Items.item_Type != 3)
			{
				continue;
			}
			decimal num = SLCTQuantity(Convert.ToInt32(value), Convert.ToInt32(row["رقم المخزن"].ToString()));
			decimal num2 = Convert.ToDecimal(row["الكمية"].ToString());
			if (num > 0m)
			{
				if (num - num2 >= 0m)
				{
					ItemQuantityIn(Convert.ToInt32(value), num2, row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"].ToString()), 1);
					continue;
				}
				ItemQuantityIn(Convert.ToInt32(value), num, row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"].ToString()), 1);
				ItemQuantityIn(Convert.ToInt32(value), num2 - num, row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"].ToString()), DecimalNum);
			}
			else
			{
				ItemQuantityIn(Convert.ToInt32(value), num2, row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"].ToString()), DecimalNum);
			}
		}
		return result;
	}

	public decimal SLCTQuantity(int ItmID, int StoreID)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		int num = 0;
		ItemQuantity itemQuantity = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItmID && x.StoreID == (int?)StoreID).FirstOrDefault();
		num = ((itemQuantity != null) ? Convert.ToInt32(itemQuantity.CurrentBalance) : 0);
		return num;
	}

	public int InsertPurch(out int Code, int SupplierID, string SupplierName, DateTime Purchases_Date, int CostCenter, int ProjectId, string Note, string NoteNum, decimal CostOrder, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal CashMoney, decimal CashBank, bool cashierType, decimal TobaccoTax, decimal AllTax)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.Order_PaymentItem.Select((Order_PaymentItem p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Order_PaymentItem
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		Order_PaymentItem order_PaymentItem = new Order_PaymentItem();
		order_PaymentItem.ID = iD;
		CachCode = num2;
		Code = num2;
		order_PaymentItem.BranchID = LoginDetails.BranchID;
		order_PaymentItem.PurBranchID = num2;
		order_PaymentItem.SupplierID = SupplierID;
		order_PaymentItem.SupplierName = SupplierName;
		order_PaymentItem.Purchases_Date = Purchases_Date;
		order_PaymentItem.CostCentersID = CostCenter;
		order_PaymentItem.ProjectId = ProjectId;
		order_PaymentItem.Note = Note;
		order_PaymentItem.NoteNum = NoteNum;
		order_PaymentItem.CostOrder = CostOrder;
		order_PaymentItem.TotalPrices = TotalPrices;
		order_PaymentItem.Tax = Tax;
		order_PaymentItem.Safy = Safy;
		order_PaymentItem.DiscountNum = DiscountNum;
		order_PaymentItem.DiscountPerantage = DiscountPerantage;
		order_PaymentItem.Tax_Discount = Tax_Discount;
		order_PaymentItem.TotalPrices_Discount = TotalPrices_Discount;
		order_PaymentItem.Net = Net;
		order_PaymentItem.OrderCashierType = cashierType;
		order_PaymentItem.CashMoney = CashMoney;
		order_PaymentItem.CashBank = CashBank;
		order_PaymentItem.TobaccoTax = TobaccoTax;
		order_PaymentItem.AllTax = AllTax;
		order_PaymentItem.UserID_Add = LoginDetails.UserID;
		order_PaymentItem.UserBranch_Add = LoginDetails.BranchID;
		order_PaymentItem.UserMacAddress_Add = LoginDetails.macAddress;
		order_PaymentItem.UserDate_Add = DateTime.Now;
		db.Order_PaymentItem.Add(order_PaymentItem);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID)
	{
		List<Order_PaymentItemDetails> list = db.Order_PaymentItemDetails.Where((Order_PaymentItemDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Order_PaymentItemDetails item in list)
		{
			Item_Items item_Items = db.Item_Items.Where((Item_Items x) => (int?)x.ItemId == item.ItemID).FirstOrDefault();
			ItemQuantityOut(Convert.ToInt32(item.ItemID), Convert.ToDecimal(item.Quantity), item.ItemUnitType, Convert.ToInt32(item.StoreID), Convert.ToInt32(item_Items.item_Type));
			db.Order_PaymentItemDetails.Attach(item);
			db.Order_PaymentItemDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, int SupplierID, string SupplierName, DateTime Purchases_Date, int CostCenter, int ProjectId, string Note, string NoteNum, decimal CostOrder, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal CashMoney, decimal CashBank, bool cashierType, decimal TobaccoTax, decimal AllTax)
	{
		int result = 0;
		Order_PaymentItem order_PaymentItem = db.Order_PaymentItem.Where((Order_PaymentItem x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.Order_PaymentItem.Where((Order_PaymentItem x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (order_PaymentItem != null)
		{
			order_PaymentItem.SupplierID = SupplierID;
			order_PaymentItem.SupplierName = SupplierName;
			order_PaymentItem.Purchases_Date = Purchases_Date;
			order_PaymentItem.CostCentersID = CostCenter;
			order_PaymentItem.ProjectId = ProjectId;
			order_PaymentItem.Note = Note;
			order_PaymentItem.NoteNum = NoteNum;
			order_PaymentItem.TotalPrices = TotalPrices;
			order_PaymentItem.Tax = Tax;
			order_PaymentItem.Safy = Safy;
			order_PaymentItem.CostOrder = CostOrder;
			order_PaymentItem.DiscountNum = DiscountNum;
			order_PaymentItem.DiscountPerantage = DiscountPerantage;
			order_PaymentItem.Tax_Discount = Tax_Discount;
			order_PaymentItem.TotalPrices_Discount = TotalPrices_Discount;
			order_PaymentItem.Net = Net;
			order_PaymentItem.CashMoney = CashMoney;
			order_PaymentItem.CashBank = CashBank;
			order_PaymentItem.OrderCashierType = cashierType;
			order_PaymentItem.TobaccoTax = TobaccoTax;
			order_PaymentItem.AllTax = AllTax;
			order_PaymentItem.UserID_Update = LoginDetails.UserID;
			order_PaymentItem.UserBranch_Update = LoginDetails.GroupID;
			order_PaymentItem.UserMacAddress_Update = LoginDetails.macAddress;
			order_PaymentItem.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public int Delete(int PurID)
	{
		int result = 0;
		Order_PaymentItem order_PaymentItem = db.Order_PaymentItem.Where((Order_PaymentItem x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (order_PaymentItem != null)
		{
			deleteItemsByPurcahesId(PurID);
			db.Order_PaymentItem.Attach(order_PaymentItem);
			db.Order_PaymentItem.Remove(order_PaymentItem);
			result = db.SaveChanges();
		}
		return result;
	}

	public int InsertTran(out int TRNIDDS, int PurID, string Name, string NoteNum, DateTime PurDate)
	{
		int num = 0;
		TRNIDDS = 0;
		int num2 = Convert.ToInt32(db.Tran_Tran.Select((Tran_Tran p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		Tran_Tran tran_Tran = new Tran_Tran();
		tran_Tran.ID = num2;
		tran_Tran.ReferenceCode = PurID;
		tran_Tran.TranTypeID = 19;
		tran_Tran.DocCode = NoteNum;
		tran_Tran.TranDate = PurDate;
		tran_Tran.BranchID = LoginDetails.BranchID;
		tran_Tran.Note = " أوامر الصرف رقم  ( " + PurID + " ) ط­ /  " + Name;
		tran_Tran.UserID_Add = LoginDetails.UserID;
		tran_Tran.UserBranch_Add = LoginDetails.BranchID;
		tran_Tran.UserMacAddress_Add = LoginDetails.macAddress;
		tran_Tran.UserDate_Add = DateTime.Now;
		db.Tran_Tran.Add(tran_Tran);
		num = db.SaveChanges();
		TRNIDDS = num2;
		return num;
	}

	public int InsertTranDetails(int TranID, int PurID, int AccNum, string Name, decimal TotalCost, int CostCenter, int ProjectId)
	{
		int result = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = AccNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = "  أوامر الصرف رقم  ( " + PurID + " ) لحساب " + Name;
		tran_TranDetails.Debit = TotalCost;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = 0;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = " حساب مخزون أوامر الصرف رقم  ( " + PurID + " )  ";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = TotalCost;
		tran_TranDetails2.CostCentersID = CostCenter;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		return result;
	}

	public int UpdateTran(out int TRNIDDS, int PurID, string Name, string NoteNum, DateTime PurDate)
	{
		int result = 0;
		TRNIDDS = 0;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)PurID && x.TranTypeID == (int?)19 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_Tran != null)
		{
			tran_Tran.DocCode = NoteNum;
			tran_Tran.TranDate = PurDate;
			tran_Tran.BranchID = LoginDetails.BranchID;
			tran_Tran.Note = " أوامر الصرف  رقم  ( " + PurID + " ) ط­ /  " + Name + " معدلة ";
			tran_Tran.UserID_Update = LoginDetails.UserID;
			tran_Tran.UserBranch_Update = LoginDetails.GroupID;
			tran_Tran.UserMacAddress_Update = LoginDetails.macAddress;
			tran_Tran.UserDate_Update = DateTime.Now;
			TRNIDDS = Convert.ToInt32(tran_Tran.ID);
			result = db.SaveChanges();
		}
		return result;
	}

	public int UpdateTranDetails(int TranID, int PurID, int AccNum, string Name, decimal NetCash, int CostCenter, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = AccNum;
		tran_TranDetails.AccounIindex = 1;
		tran_TranDetails.TranDesc = "  أوامر الصرف رقم  ( " + PurID + " ) لحساب " + Name + " معدلة ";
		tran_TranDetails.Debit = NetCash;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = 0;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		num = db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails2.AccounIindex = 1;
		tran_TranDetails2.TranDesc = " حساب مخزون أوامر الصرف رقم  ( " + PurID + " )  معدلة";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = NetCash;
		tran_TranDetails2.CostCentersID = CostCenter;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		return db.SaveChanges();
	}

	public int DeleteTran(int Code)
	{
		int result = 0;
		try
		{
			int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)19 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
			Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ID == TranID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
			if (tran_Tran != null)
			{
				db.Tran_Tran.Attach(tran_Tran);
				db.Tran_Tran.Remove(tran_Tran);
				result = db.SaveChanges();
			}
		}
		catch (Exception)
		{
		}
		return result;
	}

	public int DeleteTranDetails(int Code)
	{
		int result = 0;
		try
		{
			int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)19 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
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
		}
		catch (Exception)
		{
		}
		return result;
	}

	public int GetItmID(string Code)
	{
		return (from xx in db.Item_Items
			where xx.Item_code == Code
			select xx.ItemId).FirstOrDefault();
	}

	public decimal GetAverageCostID(int ItmID)
	{
		return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.ItemId == ItmID).FirstOrDefault().Average_cost);
	}

	public decimal GetSellPermission(string Item_code, int UserSellPricePer, string Type)
	{
		decimal result = 0m;
		try
		{
			switch (Type)
			{
			case "الصغرى":
				if (UserSellPricePer == 1)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().SellPriceSmall);
				}
				if (UserSellPricePer == 2)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().SellPriceSmall2);
				}
				if (UserSellPricePer == 3)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().SellPriceSmall3);
				}
				if (UserSellPricePer >= 4)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().LastCost);
				}
				break;
			case "المتوسطة":
				if (UserSellPricePer == 1)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().SellPriceMedium);
				}
				if (UserSellPricePer == 2)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().SellPriceMedium2);
				}
				if (UserSellPricePer == 3)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().SellPriceMedium3);
				}
				if (UserSellPricePer >= 4)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().LastCost2);
				}
				break;
			case "الكبرى":
				if (UserSellPricePer == 1)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().SellpriceLarge);
				}
				if (UserSellPricePer == 2)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().SellpriceLarge2);
				}
				if (UserSellPricePer == 3)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().SellpriceLarge3);
				}
				if (UserSellPricePer >= 4)
				{
					return Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.Item_code == Item_code).FirstOrDefault().LastCost3);
				}
				break;
			}
		}
		catch (Exception)
		{
			result = 0m;
		}
		return result;
	}

	public decimal GetQuantityPermission(int ItemID, int StoreID, string Type)
	{
		decimal result = 0m;
		try
		{
			switch (Type)
			{
			case "الصغرى":
				return Convert.ToDecimal(db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault().CurrentBalance);
			case "المتوسطة":
			{
				decimal num4 = Convert.ToDecimal(db.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault().ConvertMediumUnit);
				decimal num5 = Convert.ToDecimal(db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault().CurrentBalance);
				return num5 / num4;
			}
			case "الكبرى":
			{
				Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault();
				decimal num = Convert.ToDecimal(item_Items.ConvertMediumUnit);
				decimal num2 = Convert.ToDecimal(item_Items.ConvertBigUnit);
				decimal num3 = Convert.ToDecimal(db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID && x.StoreID == (int?)StoreID).FirstOrDefault().CurrentBalance);
				return num3 / (num * num2);
			}
			}
		}
		catch (Exception)
		{
			result = 0m;
		}
		return result;
	}

	public int GetAccID(int Acc)
	{
		int num = 0;
		return db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)Acc).FirstOrDefault().ID;
	}
}


