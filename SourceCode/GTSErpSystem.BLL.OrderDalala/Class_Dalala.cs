using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.OrderDalala;

internal class Class_Dalala
{
	private int CachCode = 0;

	private DataTable dtCurrentTable = new DataTable();

	private GTSdbEntities db = new GTSdbEntities();

	private int DecimalNum = LoginDetails.DecimalNum;

	public List<Account_AccountType> GetAccountType()
	{
		return db.Account_AccountType.ToList();
	}

	public int GetIDAccountSupp(int ID)
	{
		int SuppID = Convert.ToInt32(db.Account_CustSup.Where((Account_CustSup z) => z.ID == ID).FirstOrDefault().AccountNo);
		return Convert.ToInt32(db.Account_Accounts.Where((Account_Accounts z) => z.Account_No == (int?)SuppID).FirstOrDefault().ID);
	}

	public int GetIDAccount(int ID)
	{
		int result = 1;
		try
		{
			int SuppID = Convert.ToInt32(db.Account_CustSup.Where((Account_CustSup z) => z.CustSuppCode == (int?)ID && z.IsCustomers == (bool?)true && z.FrmCust == (bool?)true).FirstOrDefault().AccountNo);
			result = Convert.ToInt32(db.Account_Accounts.Where((Account_Accounts z) => z.Account_No == (int?)SuppID).FirstOrDefault().ID);
		}
		catch (Exception)
		{
		}
		return result;
	}

	public List<Select_SearchAccountCostCenter_Result> GetCostCenters()
	{
		return db.Select_SearchAccountCostCenter(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<OrderDalala_Dalala> GetPurchases()
	{
		return db.OrderDalala_Dalala.Where((OrderDalala_Dalala x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<OrderDalala_Sending> GetPurchases(int Code)
	{
		return db.OrderDalala_Sending.Where((OrderDalala_Sending x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<OrderDalala_Dalala> GetDalala(int Code)
	{
		return db.OrderDalala_Dalala.Where((OrderDalala_Dalala x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
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

	public List<OrderDalala_DalalaDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.OrderDalala_DalalaDetails.Where((OrderDalala_DalalaDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_OrderDalala_Dalala_Result> GetSelectAllPurchases()
	{
		return db.Select_OrderDalala_Dalala(LoginDetails.BranchID).ToList();
	}

	public int GetUnitIdByName(string Nmae)
	{
		int num = 0;
		return (from x in db.Item_Unit
			where x.Name == Nmae
			select x.ID).FirstOrDefault();
	}

	public int InsertItems(DataTable Items, int Purchese_ID, int FromSendingID)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (!(row["كود_الصنف"].ToString() != ""))
			{
				continue;
			}
			OrderDalala_DalalaDetails orderDalala_DalalaDetails = new OrderDalala_DalalaDetails();
			string Itemcod = row["كود_الصنف"].ToString();
			string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
			int ItemRowChek = Convert.ToInt32(value);
			Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
			int value2 = Convert.ToInt32(db.TblSettings.Select((TblSetting x) => x.StoreID).FirstOrDefault());
			if (item_Items != null)
			{
				orderDalala_DalalaDetails.Purchese_ID = Purchese_ID;
				orderDalala_DalalaDetails.BranchID = LoginDetails.BranchID;
				orderDalala_DalalaDetails.FromSendingID = FromSendingID;
				orderDalala_DalalaDetails.ItemID = Convert.ToInt32(item_Items.ItemId);
				string nmae = row["الوحدة"].ToString();
				orderDalala_DalalaDetails.ItemUnitID = GetUnitIdByName(nmae);
				orderDalala_DalalaDetails.SmallUnitPrice = Convert.ToDecimal(row["سعر الوحدة"].ToString());
				orderDalala_DalalaDetails.Quantity = Convert.ToDecimal(row["الكمية"].ToString());
				orderDalala_DalalaDetails.QuantityMinus = Convert.ToDecimal(row["كمية الهدر"].ToString());
				orderDalala_DalalaDetails.ItemNote = row["الشرح"].ToString();
				orderDalala_DalalaDetails.StoreID = value2;
				orderDalala_DalalaDetails.LastCost = Convert.ToDecimal(row["متوسط_التكلفة"].ToString());
				orderDalala_DalalaDetails.UnitPrice = Math.Round(Convert.ToDecimal(row["سعر الوحدة"].ToString()), DecimalNum);
				orderDalala_DalalaDetails.TotalPrice = Math.Round(Convert.ToDecimal(row["الاجمالى"].ToString()), DecimalNum);
				orderDalala_DalalaDetails.NetUnitPrice = Math.Round(Convert.ToDecimal(row["سعربعدالخصم"].ToString()), DecimalNum);
				orderDalala_DalalaDetails.NetTotalPrice = Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"].ToString()), DecimalNum);
				orderDalala_DalalaDetails.VAT = Math.Round(Convert.ToDecimal(row["الضربية"].ToString()), DecimalNum);
				orderDalala_DalalaDetails.ItemUnitType = row["أسم_الوحدة"].ToString();
				orderDalala_DalalaDetails.VAT_Discount = Math.Round(Convert.ToDecimal(row["الضريبه_بعد_الخصم"].ToString()), DecimalNum);
				orderDalala_DalalaDetails.StoreID = Convert.ToInt32(row["رقم المخزن"]);
				db.OrderDalala_DalalaDetails.Add(orderDalala_DalalaDetails);
				result = db.SaveChanges();
				if (item_Items.item_Type == 1)
				{
					ItemQuantityOut(Convert.ToInt32(value), Convert.ToDecimal(row["الكمية"].ToString()) + Convert.ToDecimal(row["كمية الهدر"].ToString()), row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"].ToString()), 1);
				}
				if (item_Items.item_Type == 3)
				{
					ItemQuantityOut(Convert.ToInt32(value), Convert.ToDecimal(row["الكمية"].ToString()) + Convert.ToDecimal(row["كمية الهدر"].ToString()), row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"].ToString()), 1);
				}
			}
		}
		return result;
	}

	public int InsertPurch(out int Code, int FromSendingID, int TypeID, int CustID, string CustName, int SupplierID, string SupplierName, string DriverName, string DriverPhone, DateTime Purchases_Date, string CarNum, string City, decimal BouncePerc, decimal Bounce, string ExpensesText, decimal Expenses, decimal Employee, decimal CareRent, decimal Market, decimal CostAverg, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal TobaccoTax, decimal AllTax, int CostCenter)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.OrderDalala_Dalala.Select((OrderDalala_Dalala p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.OrderDalala_Dalala
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		OrderDalala_Dalala orderDalala_Dalala = new OrderDalala_Dalala();
		orderDalala_Dalala.ID = iD;
		CachCode = num2;
		Code = num2;
		orderDalala_Dalala.FromSendingID = FromSendingID;
		orderDalala_Dalala.BranchID = LoginDetails.BranchID;
		orderDalala_Dalala.PurBranchID = num2;
		orderDalala_Dalala.CostCentersID = CostCenter;
		orderDalala_Dalala.TypeID = TypeID;
		orderDalala_Dalala.CustID = CustID;
		orderDalala_Dalala.CustName = CustName;
		orderDalala_Dalala.SupplierID = SupplierID;
		orderDalala_Dalala.SupplierName = SupplierName;
		orderDalala_Dalala.DriverName = DriverName;
		orderDalala_Dalala.DriverPhone = DriverPhone;
		orderDalala_Dalala.Purchases_Date = Purchases_Date;
		orderDalala_Dalala.CarNum = CarNum;
		orderDalala_Dalala.City = City;
		orderDalala_Dalala.CostAverg = CostAverg;
		orderDalala_Dalala.BouncePerc = BouncePerc;
		orderDalala_Dalala.Bounce = Bounce;
		orderDalala_Dalala.ExpensesText = ExpensesText;
		orderDalala_Dalala.Expenses = Expenses;
		orderDalala_Dalala.Employee = Employee;
		orderDalala_Dalala.CareRent = CareRent;
		orderDalala_Dalala.Market = Market;
		orderDalala_Dalala.TotalPrices = TotalPrices;
		orderDalala_Dalala.Tax = Tax;
		orderDalala_Dalala.Safy = Safy;
		orderDalala_Dalala.DiscountNum = DiscountNum;
		orderDalala_Dalala.DiscountPerantage = DiscountPerantage;
		orderDalala_Dalala.Tax_Discount = Tax_Discount;
		orderDalala_Dalala.TotalPrices_Discount = TotalPrices_Discount;
		orderDalala_Dalala.Net = Net;
		orderDalala_Dalala.TobaccoTax = TobaccoTax;
		orderDalala_Dalala.AllTax = AllTax;
		orderDalala_Dalala.UserID_Add = LoginDetails.UserID;
		orderDalala_Dalala.UserBranch_Add = LoginDetails.BranchID;
		orderDalala_Dalala.UserMacAddress_Add = LoginDetails.macAddress;
		orderDalala_Dalala.UserDate_Add = DateTime.Now;
		db.OrderDalala_Dalala.Add(orderDalala_Dalala);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID)
	{
		List<OrderDalala_DalalaDetails> list = db.OrderDalala_DalalaDetails.Where((OrderDalala_DalalaDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (OrderDalala_DalalaDetails item in list)
		{
			Item_Items item_Items = db.Item_Items.Where((Item_Items x) => (int?)x.ItemId == item.ItemID).FirstOrDefault();
			ItemQuantityIN(Convert.ToInt32(item.ItemID), Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.QuantityMinus), item.ItemUnitType, Convert.ToInt32(item.StoreID), 1);
			db.OrderDalala_DalalaDetails.Attach(item);
			db.OrderDalala_DalalaDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, int TypeID, int CustID, string CustName, int SupplierID, string SupplierName, string DriverName, string DriverPhone, DateTime Purchases_Date, string CarNum, string City, decimal BouncePerc, decimal Bounce, string ExpensesText, decimal Expenses, decimal Employee, decimal CareRent, decimal Market, decimal CostAverg, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal TobaccoTax, decimal AllTax, int CostCenter)
	{
		int result = 0;
		OrderDalala_Dalala orderDalala_Dalala = db.OrderDalala_Dalala.Where((OrderDalala_Dalala x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.OrderDalala_Dalala.Where((OrderDalala_Dalala x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (orderDalala_Dalala != null)
		{
			orderDalala_Dalala.CostCentersID = CostCenter;
			orderDalala_Dalala.TypeID = TypeID;
			orderDalala_Dalala.CustID = CustID;
			orderDalala_Dalala.CustName = CustName;
			orderDalala_Dalala.SupplierID = SupplierID;
			orderDalala_Dalala.SupplierName = SupplierName;
			orderDalala_Dalala.DriverName = DriverName;
			orderDalala_Dalala.DriverPhone = DriverPhone;
			orderDalala_Dalala.Purchases_Date = Purchases_Date;
			orderDalala_Dalala.CarNum = CarNum;
			orderDalala_Dalala.City = City;
			orderDalala_Dalala.BouncePerc = BouncePerc;
			orderDalala_Dalala.Bounce = Bounce;
			orderDalala_Dalala.Expenses = Expenses;
			orderDalala_Dalala.ExpensesText = ExpensesText;
			orderDalala_Dalala.Employee = Employee;
			orderDalala_Dalala.CareRent = CareRent;
			orderDalala_Dalala.Market = Market;
			orderDalala_Dalala.CostAverg = CostAverg;
			orderDalala_Dalala.TotalPrices = TotalPrices;
			orderDalala_Dalala.Tax = Tax;
			orderDalala_Dalala.Safy = Safy;
			orderDalala_Dalala.DiscountNum = DiscountNum;
			orderDalala_Dalala.DiscountPerantage = DiscountPerantage;
			orderDalala_Dalala.Tax_Discount = Tax_Discount;
			orderDalala_Dalala.TotalPrices_Discount = TotalPrices_Discount;
			orderDalala_Dalala.Net = Net;
			orderDalala_Dalala.TobaccoTax = TobaccoTax;
			orderDalala_Dalala.AllTax = AllTax;
			orderDalala_Dalala.UserID_Update = LoginDetails.UserID;
			orderDalala_Dalala.UserBranch_Update = LoginDetails.GroupID;
			orderDalala_Dalala.UserMacAddress_Update = LoginDetails.macAddress;
			orderDalala_Dalala.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public int Delete(int PurID)
	{
		int result = 0;
		OrderDalala_Dalala orderDalala_Dalala = db.OrderDalala_Dalala.Where((OrderDalala_Dalala x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (orderDalala_Dalala != null)
		{
			deleteItemsByPurcahesId(PurID);
			db.OrderDalala_Dalala.Attach(orderDalala_Dalala);
			db.OrderDalala_Dalala.Remove(orderDalala_Dalala);
			result = db.SaveChanges();
		}
		return result;
	}

	public int InsertTranNum(out int TRNIDDS, int PurID, string Name, DateTime PurDate)
	{
		int num = 0;
		TRNIDDS = 0;
		int num2 = Convert.ToInt32(db.Tran_Tran.Select((Tran_Tran p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		Tran_Tran tran_Tran = new Tran_Tran();
		tran_Tran.ID = num2;
		tran_Tran.ReferenceCode = PurID;
		tran_Tran.TranTypeID = 39;
		tran_Tran.TranDate = PurDate;
		tran_Tran.BranchID = LoginDetails.BranchID;
		tran_Tran.Note = "فاتورة الدلالة أجلة رقم ( " + PurID + " ) ط­ /  " + Name;
		tran_Tran.UserID_Add = LoginDetails.UserID;
		tran_Tran.UserBranch_Add = LoginDetails.BranchID;
		tran_Tran.UserMacAddress_Add = LoginDetails.macAddress;
		tran_Tran.UserDate_Add = DateTime.Now;
		db.Tran_Tran.Add(tran_Tran);
		num = db.SaveChanges();
		TRNIDDS = num2;
		return TRNIDDS;
	}

	public int InsertTranDetailsCash(int TranID, int PurID, int SuppNum, string Name, decimal TotalSup, decimal TotalSell, decimal Vat, decimal TotalCash, int CostCenter, decimal TxtExpenses, string TxtExpensesText, decimal TxtEmployee, decimal TxtCareRent, decimal TxtMarket)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_CashBox;
		tran_TranDetails.AccounIindex = 1;
		tran_TranDetails.TranDesc = "  مبيعات نقدية  رقم  ( " + PurID + " ) ";
		tran_TranDetails.Debit = TotalCash;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = CostCenter;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		num = db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_OrderCash;
		tran_TranDetails2.AccounIindex = 1;
		tran_TranDetails2.TranDesc = "  مبيعات نقدية  رقم  ( " + PurID + " ) ";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = TotalSell;
		tran_TranDetails2.CostCentersID = CostCenter;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		num = db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
			tran_TranDetails3.TranSn = TranID;
			tran_TranDetails3.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails3.AccounIindex = 0;
			tran_TranDetails3.TranDesc = " ضريبة مبيعات نقدية  رقم  ( " + PurID + " )  ";
			tran_TranDetails3.Debit = 0m;
			tran_TranDetails3.Credit = Vat;
			tran_TranDetails3.CostCentersID = CostCenter;
			tran_TranDetails3.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails3);
			db.SaveChanges();
		}
		if (TotalSup > 0m)
		{
			Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
			tran_TranDetails4.TranSn = TranID;
			tran_TranDetails4.Account_Sn = SuppNum;
			tran_TranDetails4.AccounIindex = 1;
			tran_TranDetails4.TranDesc = "  مبيعات نقدية رقم  ( " + PurID + " ) " + Name;
			tran_TranDetails4.Debit = 0m;
			tran_TranDetails4.Credit = TotalSup;
			tran_TranDetails4.CostCentersID = CostCenter;
			tran_TranDetails4.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails4);
			num = db.SaveChanges();
		}
		if (TxtEmployee > 0m)
		{
			Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
			tran_TranDetails5.TranSn = TranID;
			tran_TranDetails5.Account_Sn = 497;
			tran_TranDetails5.AccounIindex = 5;
			tran_TranDetails5.TranDesc = " أجور عمال لدلالة رقم  ( " + PurID + " ) ";
			tran_TranDetails5.Credit = 0m;
			tran_TranDetails5.Debit = TxtEmployee;
			tran_TranDetails5.CostCentersID = CostCenter;
			tran_TranDetails5.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails5);
			num = db.SaveChanges();
		}
		if (TxtCareRent > 0m)
		{
			Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
			tran_TranDetails6.TranSn = TranID;
			tran_TranDetails6.Account_Sn = 290;
			tran_TranDetails6.AccounIindex = 6;
			tran_TranDetails6.TranDesc = " ايجار سيارة لدلالة رقم  ( " + PurID + " ) ";
			tran_TranDetails6.Credit = 0m;
			tran_TranDetails6.Debit = TxtCareRent;
			tran_TranDetails6.CostCentersID = CostCenter;
			tran_TranDetails6.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails6);
			num = db.SaveChanges();
		}
		if (TxtMarket > 0m)
		{
			Tran_TranDetails tran_TranDetails7 = new Tran_TranDetails();
			tran_TranDetails7.TranSn = TranID;
			tran_TranDetails7.Account_Sn = 499;
			tran_TranDetails7.AccounIindex = 7;
			tran_TranDetails7.TranDesc = " كرت السوق لدلالة رقم  ( " + PurID + " ) ";
			tran_TranDetails7.Credit = 0m;
			tran_TranDetails7.Debit = TxtMarket;
			tran_TranDetails7.CostCentersID = CostCenter;
			tran_TranDetails7.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails7);
			num = db.SaveChanges();
		}
		if (TxtExpenses > 0m)
		{
			Tran_TranDetails tran_TranDetails8 = new Tran_TranDetails();
			tran_TranDetails8.TranSn = TranID;
			tran_TranDetails8.Account_Sn = 500;
			tran_TranDetails8.AccounIindex = 8;
			tran_TranDetails8.TranDesc = " مصاريف اخري لدلالة رقم  ( " + PurID + " ) " + TxtExpensesText;
			tran_TranDetails8.Credit = 0m;
			tran_TranDetails8.Debit = TxtExpenses;
			tran_TranDetails8.CostCentersID = CostCenter;
			tran_TranDetails8.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails8);
			num = db.SaveChanges();
		}
		return num;
	}

	public int InsertTranDetailsAjil(int TranID, int PurID, int CustID, int SuppNum, string Name, decimal TotalSup, decimal TotalSell, decimal Vat, decimal TotalCash, int CostCenter, decimal TxtExpenses, string TxtExpensesText, decimal TxtEmployee, decimal TxtCareRent, decimal TxtMarket)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = CustID;
		tran_TranDetails.AccounIindex = 1;
		tran_TranDetails.TranDesc = "  مبيعات أجلة رقم  ( " + PurID + " ) ";
		tran_TranDetails.Debit = TotalCash;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = CostCenter;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		num = db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_OrderAjila;
		tran_TranDetails2.AccounIindex = 1;
		tran_TranDetails2.TranDesc = "  مبيعات أجلة  رقم  ( " + PurID + " ) ";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = TotalSell;
		tran_TranDetails2.CostCentersID = CostCenter;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		num = db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
			tran_TranDetails3.TranSn = TranID;
			tran_TranDetails3.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails3.AccounIindex = 0;
			tran_TranDetails3.TranDesc = " ضريبة مبيعات أجلة  رقم  ( " + PurID + " )  ";
			tran_TranDetails3.Debit = 0m;
			tran_TranDetails3.Credit = Vat;
			tran_TranDetails3.CostCentersID = CostCenter;
			tran_TranDetails3.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails3);
			db.SaveChanges();
		}
		if (TotalSup > 0m)
		{
			Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
			tran_TranDetails4.TranSn = TranID;
			tran_TranDetails4.Account_Sn = SuppNum;
			tran_TranDetails4.AccounIindex = 1;
			tran_TranDetails4.TranDesc = "  مبيعات أجلة رقم  ( " + PurID + " ) " + Name;
			tran_TranDetails4.Debit = 0m;
			tran_TranDetails4.Credit = TotalSup;
			tran_TranDetails4.CostCentersID = CostCenter;
			tran_TranDetails4.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails4);
			num = db.SaveChanges();
		}
		if (TxtEmployee > 0m)
		{
			Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
			tran_TranDetails5.TranSn = TranID;
			tran_TranDetails5.Account_Sn = 497;
			tran_TranDetails5.AccounIindex = 5;
			tran_TranDetails5.TranDesc = " أجور عمال لدلالة رقم  ( " + PurID + " ) ";
			tran_TranDetails5.Credit = 0m;
			tran_TranDetails5.Debit = TxtEmployee;
			tran_TranDetails5.CostCentersID = CostCenter;
			tran_TranDetails5.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails5);
			num = db.SaveChanges();
		}
		if (TxtCareRent > 0m)
		{
			Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
			tran_TranDetails6.TranSn = TranID;
			tran_TranDetails6.Account_Sn = 290;
			tran_TranDetails6.AccounIindex = 6;
			tran_TranDetails6.TranDesc = " ايجار سيارة لدلالة رقم  ( " + PurID + " ) ";
			tran_TranDetails6.Credit = 0m;
			tran_TranDetails6.Debit = TxtCareRent;
			tran_TranDetails6.CostCentersID = CostCenter;
			tran_TranDetails6.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails6);
			num = db.SaveChanges();
		}
		if (TxtMarket > 0m)
		{
			Tran_TranDetails tran_TranDetails7 = new Tran_TranDetails();
			tran_TranDetails7.TranSn = TranID;
			tran_TranDetails7.Account_Sn = 499;
			tran_TranDetails7.AccounIindex = 7;
			tran_TranDetails7.TranDesc = " كرت السوق لدلالة رقم  ( " + PurID + " ) ";
			tran_TranDetails7.Credit = 0m;
			tran_TranDetails7.Debit = TxtMarket;
			tran_TranDetails7.CostCentersID = CostCenter;
			tran_TranDetails7.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails7);
			num = db.SaveChanges();
		}
		if (TxtExpenses > 0m)
		{
			Tran_TranDetails tran_TranDetails8 = new Tran_TranDetails();
			tran_TranDetails8.TranSn = TranID;
			tran_TranDetails8.Account_Sn = 500;
			tran_TranDetails8.AccounIindex = 8;
			tran_TranDetails8.TranDesc = " مصاريف اخري لدلالة رقم  ( " + PurID + " ) " + TxtExpensesText;
			tran_TranDetails8.Credit = 0m;
			tran_TranDetails8.Debit = TxtExpenses;
			tran_TranDetails8.CostCentersID = CostCenter;
			tran_TranDetails8.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails8);
			num = db.SaveChanges();
		}
		return num;
	}

	public int UpdateTranNUM(out int TRNIDDS, int PurID, string Name, DateTime PurDate)
	{
		int result = 0;
		TRNIDDS = 0;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)PurID && x.TranTypeID == (int?)39 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_Tran != null)
		{
			tran_Tran.TranDate = PurDate;
			tran_Tran.BranchID = LoginDetails.BranchID;
			tran_Tran.Note = " فاتورة الدلالة أجلة رقم  ( " + PurID + " ) ط­ /  " + Name + " معدلة ";
			tran_Tran.UserID_Update = LoginDetails.UserID;
			tran_Tran.UserBranch_Update = LoginDetails.GroupID;
			tran_Tran.UserMacAddress_Update = LoginDetails.macAddress;
			tran_Tran.UserDate_Update = DateTime.Now;
			TRNIDDS = Convert.ToInt32(tran_Tran.ID);
			result = db.SaveChanges();
		}
		return result;
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

	public decimal GetTotalQantityDalala(int PurcheseID, int ItemOd)
	{
		decimal result = 0m;
		decimal? num = db.GetTotal_DalalaQuantity(PurcheseID, ItemOd).FirstOrDefault();
		if (num.HasValue)
		{
			return Convert.ToDecimal(num);
		}
		return result;
	}

	public decimal GetTotalQantitysending(int PurcheseID, int ItemOd)
	{
		decimal result = 0m;
		OrderDalala_SendingDetails orderDalala_SendingDetails = db.OrderDalala_SendingDetails.Where((OrderDalala_SendingDetails x) => x.Purchese_ID == (int?)PurcheseID && x.ItemID == (int?)ItemOd && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (orderDalala_SendingDetails != null)
		{
			return Convert.ToDecimal(orderDalala_SendingDetails.Quantity);
		}
		return result;
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
}


