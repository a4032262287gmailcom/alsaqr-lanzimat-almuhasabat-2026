using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GTSErpSystem.BLL.OrderRent;

internal class Class_Recipt
{
	private int CachCode = 0;

	private DataTable dtCurrentTable = new DataTable();

	private GTSdbEntities db = new GTSdbEntities();

	private int DecimalNum = LoginDetails.DecimalNum;

	private SqlConnection con = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);

	public List<Account_AccountType> GetAccountType()
	{
		return db.Account_AccountType.ToList();
	}

	public List<Select_SearchAccountCostCenter_Result> GetCostCenters()
	{
		return db.Select_SearchAccountCostCenter(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<OrderRent_Recipt> GetPurchases()
	{
		return db.OrderRent_Recipt.Where((OrderRent_Recipt x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<OrderRent_Recipt3> Recipt3()
	{
		return (from x in db.OrderRent_Recipt3
			where x.BranchID == (int?)LoginDetails.BranchID
			orderby x.ID descending
			select x).ToList();
	}

	public List<Orders_RentalInvoice> GetPurchases2()
	{
		return (from x in db.Orders_RentalInvoice
			where x.BranchID == (int?)LoginDetails.BranchID && !db.OrderRent_Recipt3.Any((OrderRent_Recipt3 r) => r.FormRentID == (int?)x.ID && r.rest == (decimal?)0m)
			orderby x.ID descending
			select x).ToList();
	}

	public bool ISReturn(int ID)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.OrderRent_Recipt3.Any((OrderRent_Recipt3 x) => x.FormRentID == (int?)ID);
	}

	public List<OrderRent_Recipt3> GetPurchasesRecipt3(int Code)
	{
		return db.OrderRent_Recipt3.Where((OrderRent_Recipt3 x) => x.ID == Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<OrderRent_RecpitDetails3> GetPurchasesRecipt3Details(int PurchasesID)
	{
		return db.OrderRent_RecpitDetails3.Where((OrderRent_RecpitDetails3 x) => x.Purchese_ID == (int?)PurchasesID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public void deleteItemsRecpitByPurcahesId(int PurID)
	{
		List<OrderRent_RecpitDetails3> list = db.OrderRent_RecpitDetails3.Where((OrderRent_RecpitDetails3 x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (OrderRent_RecpitDetails3 item in list)
		{
			Item_Items item_Items = db.Item_Items.Where((Item_Items x) => (int?)x.ItemId == item.ItemID).FirstOrDefault();
			ItemQuantityIn(Convert.ToInt32(item.ItemID), Convert.ToDecimal(item.Quantity), item.ItemUnitType, Convert.ToInt32(item.StoreID), Convert.ToInt32(item_Items.item_Type));
			db.OrderRent_RecpitDetails3.Attach(item);
			db.OrderRent_RecpitDetails3.Remove(item);
		}
		db.SaveChanges();
	}

	public int DeleteRecpit(int PurID)
	{
		int result = 0;
		OrderRent_Recipt3 orderRent_Recipt = db.OrderRent_Recipt3.Where((OrderRent_Recipt3 x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (orderRent_Recipt != null)
		{
			deleteItemsRecpitByPurcahesId(PurID);
			db.OrderRent_Recipt3.Attach(orderRent_Recipt);
			db.OrderRent_Recipt3.Remove(orderRent_Recipt);
			result = db.SaveChanges();
		}
		return result;
	}

	public List<OrderRent_Recipt> GetPurchases(int Code)
	{
		return db.OrderRent_Recipt.Where((OrderRent_Recipt x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public Item_Items GetItems(int itemid)
	{
		return db.Item_Items.Where((Item_Items x) => x.ItemId == itemid).FirstOrDefault();
	}

	public Printer GetPrinter(int itemid)
	{
		int CatID = Convert.ToInt32(db.Item_Items.Where((Item_Items x) => x.ItemId == itemid).FirstOrDefault().Category_ID);
		int PrintID = Convert.ToInt32(db.Item_Groups.Where((Item_Groups x) => x.ID == CatID).FirstOrDefault().PrinterID);
		return db.Printers.Where((Printer x) => x.ID == PrintID).FirstOrDefault();
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

	public List<OrderRent_RecpitDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.OrderRent_RecpitDetails.Where((OrderRent_RecpitDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
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
			OrderRent_RecpitDetails orderRent_RecpitDetails = new OrderRent_RecpitDetails();
			string Itemcod = row["كود_الصنف"].ToString();
			string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
			int ItemRowChek = Convert.ToInt32(value);
			Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
			int value2 = Convert.ToInt32(db.TblSettings.Select((TblSetting x) => x.StoreID).FirstOrDefault());
			if (item_Items != null)
			{
				orderRent_RecpitDetails.Purchese_ID = Purchese_ID;
				orderRent_RecpitDetails.BranchID = LoginDetails.BranchID;
				orderRent_RecpitDetails.ItemID = Convert.ToInt32(item_Items.ItemId);
				string nmae = row["الوحدة"].ToString();
				orderRent_RecpitDetails.ItemUnitID = GetUnitIdByName(nmae);
				orderRent_RecpitDetails.SmallUnitPrice = Convert.ToDecimal(row["سعر الوحدة"].ToString());
				orderRent_RecpitDetails.Quantity = Convert.ToDecimal(row["الكمية"].ToString());
				orderRent_RecpitDetails.DayNum = Convert.ToDecimal(row["عدد الايام"].ToString());
				orderRent_RecpitDetails.StoreID = value2;
				orderRent_RecpitDetails.LastCost = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"].ToString()), DecimalNum);
				orderRent_RecpitDetails.UnitPrice = Math.Round(Convert.ToDecimal(row["سعر الوحدة"].ToString()), DecimalNum);
				orderRent_RecpitDetails.TotalPrice = Math.Round(Convert.ToDecimal(row["الاجمالى"].ToString()), DecimalNum);
				orderRent_RecpitDetails.NetUnitPrice = Math.Round(Convert.ToDecimal(row["سعربعدالخصم"].ToString()), DecimalNum);
				orderRent_RecpitDetails.NetTotalPrice = Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"].ToString()), DecimalNum);
				orderRent_RecpitDetails.VAT = Math.Round(Convert.ToDecimal(row["الضربية"].ToString()), DecimalNum);
				orderRent_RecpitDetails.ItemUnitType = row["أسم_الوحدة"].ToString();
				orderRent_RecpitDetails.VAT_Discount = Math.Round(Convert.ToDecimal(row["الضريبه_بعد_الخصم"].ToString()), DecimalNum);
				orderRent_RecpitDetails.StoreID = Convert.ToInt32(row["رقم المخزن"]);
				orderRent_RecpitDetails.ItemNote = row["ItemNote"].ToString();
				db.OrderRent_RecpitDetails.Add(orderRent_RecpitDetails);
				result = db.SaveChanges();
				if (item_Items.item_Type == 1)
				{
					ItemQuantityOut(Convert.ToInt32(value), Convert.ToDecimal(row["الكمية"].ToString()), row["أسم_الوحدة"].ToString(), Convert.ToInt32(row["رقم المخزن"].ToString()), 1);
				}
			}
		}
		return result;
	}

	public int InsertItemsNoPrint(DataTable Items, int Purchese_ID, bool IsWait)
	{
		int num = 0;
		DataTable changes = Items.GetChanges(DataRowState.Added);
		SqlCommand sqlCommand = new SqlCommand("Insert_OrderRent_ReciptDetails", con);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.AddWithValue("@ISWaitng", IsWait);
		SqlParameter sqlParameter = sqlCommand.Parameters.AddWithValue("@Items", changes);
		sqlParameter.SqlDbType = SqlDbType.Structured;
		sqlCommand.CommandTimeout = 999999;
		if (con.State == ConnectionState.Closed)
		{
			con.Open();
		}
		num = sqlCommand.ExecuteNonQuery();
		con.Close();
		return num;
	}

	public decimal SLCTQuantity(int ItmID, int StoreID)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		int num = 0;
		ItemQuantity itemQuantity = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItmID && x.StoreID == (int?)StoreID).FirstOrDefault();
		num = ((itemQuantity != null) ? Convert.ToInt32(itemQuantity.CurrentBalance) : 0);
		return num;
	}

	public int InsertPurch(out int Code, int RentCode, int SupplierID, string SupplierName, string SupplierPhone, string SupplierIDNum, DateTime Purchases_Date, DateTime Rent_Date, int DateNum, string ParentName, int CostCenter, string Note, string Address, decimal CostOrder, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal deposit)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.OrderRent_Recipt.Select((OrderRent_Recipt p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.OrderRent_Recipt
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		OrderRent_Recipt orderRent_Recipt = new OrderRent_Recipt();
		orderRent_Recipt.ID = iD;
		CachCode = num2;
		Code = num2;
		orderRent_Recipt.BranchID = LoginDetails.BranchID;
		orderRent_Recipt.PurBranchID = num2;
		orderRent_Recipt.FormRentID = RentCode;
		orderRent_Recipt.SupplierID = SupplierID;
		orderRent_Recipt.SupplierName = SupplierName;
		orderRent_Recipt.SupplierPhone = SupplierPhone;
		orderRent_Recipt.SupplierIDNum = SupplierIDNum;
		orderRent_Recipt.Purchases_Date = Purchases_Date;
		orderRent_Recipt.Rent_Date = Rent_Date;
		orderRent_Recipt.DateNum = DateNum;
		orderRent_Recipt.ParentName = ParentName;
		orderRent_Recipt.CostCentersID = CostCenter;
		orderRent_Recipt.Note = Note;
		orderRent_Recipt.Address = Address;
		orderRent_Recipt.CostOrder = CostOrder;
		orderRent_Recipt.TotalPrices = TotalPrices;
		orderRent_Recipt.Tax = Tax;
		orderRent_Recipt.Safy = Safy;
		orderRent_Recipt.DiscountNum = DiscountNum;
		orderRent_Recipt.DiscountPerantage = DiscountPerantage;
		orderRent_Recipt.Tax_Discount = Tax_Discount;
		orderRent_Recipt.TotalPrices_Discount = TotalPrices_Discount;
		orderRent_Recipt.Net = Net;
		orderRent_Recipt.deposit = deposit;
		orderRent_Recipt.UserID_Add = LoginDetails.UserID;
		orderRent_Recipt.UserBranch_Add = LoginDetails.BranchID;
		orderRent_Recipt.UserMacAddress_Add = LoginDetails.macAddress;
		orderRent_Recipt.UserDate_Add = DateTime.Now;
		db.OrderRent_Recipt.Add(orderRent_Recipt);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID)
	{
		List<OrderRent_RecpitDetails> list = db.OrderRent_RecpitDetails.Where((OrderRent_RecpitDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (OrderRent_RecpitDetails item in list)
		{
			Item_Items item_Items = db.Item_Items.Where((Item_Items x) => (int?)x.ItemId == item.ItemID).FirstOrDefault();
			ItemQuantityIn(Convert.ToInt32(item.ItemID), Convert.ToDecimal(item.Quantity), item.ItemUnitType, Convert.ToInt32(item.StoreID), Convert.ToInt32(item_Items.item_Type));
			db.OrderRent_RecpitDetails.Attach(item);
			db.OrderRent_RecpitDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierIDNum, DateTime Purchases_Date, string ParentName, int CostCenter, string Note, string Address, decimal CostOrder, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal deposit)
	{
		int result = 0;
		OrderRent_Recipt orderRent_Recipt = db.OrderRent_Recipt.Where((OrderRent_Recipt x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.OrderRent_Recipt.Where((OrderRent_Recipt x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (orderRent_Recipt != null)
		{
			orderRent_Recipt.SupplierID = SupplierID;
			orderRent_Recipt.SupplierName = SupplierName;
			orderRent_Recipt.SupplierPhone = SupplierPhone;
			orderRent_Recipt.SupplierIDNum = SupplierIDNum;
			orderRent_Recipt.Purchases_Date = Purchases_Date;
			orderRent_Recipt.ParentName = ParentName;
			orderRent_Recipt.CostCentersID = CostCenter;
			orderRent_Recipt.Note = Note;
			orderRent_Recipt.Address = Address;
			orderRent_Recipt.CostOrder = CostOrder;
			orderRent_Recipt.TotalPrices = TotalPrices;
			orderRent_Recipt.Tax = Tax;
			orderRent_Recipt.Safy = Safy;
			orderRent_Recipt.DiscountNum = DiscountNum;
			orderRent_Recipt.DiscountPerantage = DiscountPerantage;
			orderRent_Recipt.Tax_Discount = Tax_Discount;
			orderRent_Recipt.TotalPrices_Discount = TotalPrices_Discount;
			orderRent_Recipt.Net = Net;
			orderRent_Recipt.deposit = deposit;
			orderRent_Recipt.UserID_Update = LoginDetails.UserID;
			orderRent_Recipt.UserBranch_Update = LoginDetails.GroupID;
			orderRent_Recipt.UserMacAddress_Update = LoginDetails.macAddress;
			orderRent_Recipt.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public int Delete(int PurID)
	{
		int result = 0;
		OrderRent_Recipt orderRent_Recipt = db.OrderRent_Recipt.Where((OrderRent_Recipt x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (orderRent_Recipt != null)
		{
			deleteItemsByPurcahesId(PurID);
			db.OrderRent_Recipt.Attach(orderRent_Recipt);
			db.OrderRent_Recipt.Remove(orderRent_Recipt);
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
		tran_Tran.TranTypeID = 17;
		tran_Tran.DocCode = NoteNum;
		tran_Tran.TranDate = PurDate;
		tran_Tran.BranchID = LoginDetails.BranchID;
		if (PurType == 2)
		{
			tran_Tran.Note = " تأجير أجلة  رقم  ( " + PurID + " ) ط­ /  " + Name;
		}
		else
		{
			tran_Tran.Note = " تأجير نقدية  رقم ( " + PurID + " )";
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

	public int InsertTranDetailsCash(int TranID, int PurID, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal TotalCash, decimal TotalBank, decimal NetCash, int CostCenter)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_CashBox;
		tran_TranDetails.AccounIindex = 1;
		tran_TranDetails.TranDesc = "  تأجير نقدية  رقم  ( " + PurID + " ) ";
		tran_TranDetails.Debit = TotalCash;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = CostCenter;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		num = db.SaveChanges();
		if (TotalBank > 0m)
		{
			Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
			tran_TranDetails2.TranSn = TranID;
			tran_TranDetails2.Account_Sn = LoginDetails.Acc_BankNumCachir;
			tran_TranDetails2.AccounIindex = 1;
			tran_TranDetails2.TranDesc = "  تأجير نقدية  رقم  ( " + PurID + " ) ";
			tran_TranDetails2.Debit = TotalBank;
			tran_TranDetails2.Credit = 0m;
			tran_TranDetails2.CostCentersID = CostCenter;
			tran_TranDetails2.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails2);
			num = db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = LoginDetails.Acc_OrderCash;
		tran_TranDetails3.AccounIindex = 1;
		tran_TranDetails3.TranDesc = "  تأجير نقدية  رقم  ( " + PurID + " ) ";
		tran_TranDetails3.Debit = 0m;
		tran_TranDetails3.Credit = TotalSell;
		tran_TranDetails3.CostCentersID = CostCenter;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		num = db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
			tran_TranDetails4.TranSn = TranID;
			tran_TranDetails4.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails4.AccounIindex = 0;
			tran_TranDetails4.TranDesc = " ضريبة تأجير نقدية  رقم  ( " + PurID + " )  ";
			tran_TranDetails4.Debit = 0m;
			tran_TranDetails4.Credit = Vat;
			tran_TranDetails4.CostCentersID = CostCenter;
			tran_TranDetails4.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails4);
			db.SaveChanges();
		}
		if (TotalTobacc > 0m)
		{
			Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
			tran_TranDetails5.TranSn = TranID;
			tran_TranDetails5.Account_Sn = LoginDetails.TobaccoNum;
			tran_TranDetails5.AccounIindex = 1;
			tran_TranDetails5.TranDesc = "  حساب ضريبة التبغ   ( " + PurID + " ) ";
			tran_TranDetails5.Debit = 0m;
			tran_TranDetails5.Credit = TotalTobacc;
			tran_TranDetails5.CostCentersID = CostCenter;
			tran_TranDetails5.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails5);
			num = db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
		tran_TranDetails6.TranSn = TranID;
		tran_TranDetails6.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails6.AccounIindex = 0;
		tran_TranDetails6.TranDesc = " تكلفة فاتورة تأجير نقدية رقم  ( " + PurID + " )  ";
		tran_TranDetails6.Debit = TotalCost;
		tran_TranDetails6.Credit = 0m;
		tran_TranDetails6.CostCentersID = CostCenter;
		tran_TranDetails6.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails6);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails7 = new Tran_TranDetails();
		tran_TranDetails7.TranSn = TranID;
		tran_TranDetails7.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails7.AccounIindex = 0;
		tran_TranDetails7.TranDesc = " تكلفة فاتورة تأجير نقدية رقم  ( " + PurID + " )  ";
		tran_TranDetails7.Debit = 0m;
		tran_TranDetails7.Credit = TotalCost;
		tran_TranDetails7.CostCentersID = CostCenter;
		tran_TranDetails7.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails7);
		db.SaveChanges();
		return num;
	}

	public int InsertTranDetailsAjila(int TranID, int PurID, int SuppNum, string Name, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal NetCash, int CostCenter)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = SuppNum;
		tran_TranDetails.AccounIindex = 1;
		tran_TranDetails.TranDesc = "  تأجير آجلة  رقم  ( " + PurID + " ) ";
		tran_TranDetails.Debit = NetCash;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = CostCenter;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		num = db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
			tran_TranDetails2.TranSn = TranID;
			tran_TranDetails2.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails2.AccounIindex = 0;
			tran_TranDetails2.TranDesc = "  تأجير آجلة  رقم  ( " + PurID + " ) ط­/ " + Name + " ";
			tran_TranDetails2.Debit = 0m;
			tran_TranDetails2.Credit = Vat;
			tran_TranDetails2.CostCentersID = CostCenter;
			tran_TranDetails2.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails2);
			db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = LoginDetails.Acc_OrderAjila;
		tran_TranDetails3.AccounIindex = 1;
		tran_TranDetails3.TranDesc = "  تأجير آجلة  رقم  ( " + PurID + " ) ط­/ " + Name;
		tran_TranDetails3.Debit = 0m;
		tran_TranDetails3.Credit = TotalSell;
		tran_TranDetails3.CostCentersID = CostCenter;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		num = db.SaveChanges();
		if (TotalTobacc > 0m)
		{
			Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
			tran_TranDetails4.TranSn = TranID;
			tran_TranDetails4.Account_Sn = LoginDetails.TobaccoNum;
			tran_TranDetails4.AccounIindex = 1;
			tran_TranDetails4.TranDesc = "  حساب ضريبة التبغ   ( " + PurID + " ) ";
			tran_TranDetails4.Debit = 0m;
			tran_TranDetails4.Credit = TotalTobacc;
			tran_TranDetails4.CostCentersID = CostCenter;
			tran_TranDetails4.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails4);
			num = db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
		tran_TranDetails5.TranSn = TranID;
		tran_TranDetails5.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails5.AccounIindex = 0;
		tran_TranDetails5.TranDesc = " تكلفة فاتورة تأجير آجلة رقم  ( " + PurID + " )  ";
		tran_TranDetails5.Debit = TotalCost;
		tran_TranDetails5.Credit = 0m;
		tran_TranDetails5.CostCentersID = CostCenter;
		tran_TranDetails5.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails5);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
		tran_TranDetails6.TranSn = TranID;
		tran_TranDetails6.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails6.AccounIindex = 0;
		tran_TranDetails6.TranDesc = " تكلفة فاتورة تأجير آجلة رقم  ( " + PurID + " )  ";
		tran_TranDetails6.Debit = 0m;
		tran_TranDetails6.Credit = TotalCost;
		tran_TranDetails6.CostCentersID = CostCenter;
		tran_TranDetails6.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails6);
		db.SaveChanges();
		return num;
	}

	public int UpdateTran(out int TRNIDDS, int PurID, int PurType, string Name, string NoteNum, DateTime PurDate)
	{
		int result = 0;
		TRNIDDS = 0;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)PurID && x.TranTypeID == (int?)17 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_Tran != null)
		{
			tran_Tran.DocCode = NoteNum;
			tran_Tran.TranDate = PurDate;
			tran_Tran.BranchID = LoginDetails.BranchID;
			if (PurType == 2)
			{
				tran_Tran.Note = " تأجير أجلة  رقم  ( " + PurID + " ) ط­ /  " + Name + " معدلة ";
			}
			else
			{
				tran_Tran.Note = " تأجير نقدية  رقم ( " + PurID + " ) معدلة ";
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

	public int UpdateTranDetailsCash(int TranID, int PurID, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal TotalCash, decimal TotalBank, decimal NetCash, int CostCenter)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = " تكلفة فاتورة تأجير نقدية رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails.Debit = 0m;
		tran_TranDetails.Credit = TotalCost;
		tran_TranDetails.CostCentersID = CostCenter;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = " تكلفة فاتورة تأجير نقدية رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails2.Debit = TotalCost;
		tran_TranDetails2.Credit = 0m;
		tran_TranDetails2.CostCentersID = CostCenter;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = LoginDetails.Acc_VatNum;
		tran_TranDetails3.AccounIindex = 0;
		tran_TranDetails3.TranDesc = " ضريبة تأجير نقدية  رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails3.Debit = 0m;
		tran_TranDetails3.Credit = Vat;
		tran_TranDetails3.CostCentersID = CostCenter;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
		tran_TranDetails4.TranSn = TranID;
		tran_TranDetails4.Account_Sn = LoginDetails.Acc_CashBox;
		tran_TranDetails4.AccounIindex = 1;
		tran_TranDetails4.TranDesc = "  تأجير نقدية  رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails4.Debit = TotalCash;
		tran_TranDetails4.Credit = 0m;
		tran_TranDetails4.CostCentersID = CostCenter;
		tran_TranDetails4.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails4);
		num = db.SaveChanges();
		if (TotalBank > 0m)
		{
			Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
			tran_TranDetails5.TranSn = TranID;
			tran_TranDetails5.Account_Sn = LoginDetails.Acc_BankNumCachir;
			tran_TranDetails5.AccounIindex = 1;
			tran_TranDetails5.TranDesc = "  تأجير نقدية  رقم  ( " + PurID + " ) معدلة";
			tran_TranDetails5.Debit = TotalBank;
			tran_TranDetails5.Credit = 0m;
			tran_TranDetails5.CostCentersID = CostCenter;
			tran_TranDetails5.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails5);
			num = db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
		tran_TranDetails6.TranSn = TranID;
		tran_TranDetails6.Account_Sn = LoginDetails.Acc_OrderCash;
		tran_TranDetails6.AccounIindex = 1;
		tran_TranDetails6.TranDesc = "  تأجير نقدية  رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails6.Debit = 0m;
		tran_TranDetails6.Credit = TotalSell;
		tran_TranDetails6.CostCentersID = CostCenter;
		tran_TranDetails6.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails6);
		num = db.SaveChanges();
		if (TotalTobacc > 0m)
		{
			Tran_TranDetails tran_TranDetails7 = new Tran_TranDetails();
			tran_TranDetails7.TranSn = TranID;
			tran_TranDetails7.Account_Sn = LoginDetails.TobaccoNum;
			tran_TranDetails7.AccounIindex = 1;
			tran_TranDetails7.TranDesc = "  حساب ضريبة التبغ   ( " + PurID + " ) معدلة";
			tran_TranDetails7.Debit = 0m;
			tran_TranDetails7.Credit = TotalTobacc;
			tran_TranDetails7.CostCentersID = CostCenter;
			tran_TranDetails7.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails7);
			num = db.SaveChanges();
		}
		return num;
	}

	public int UpdateTranDetailsAjila(int TranID, int PurID, int SuppNum, string Name, decimal TotalCost, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal NetCash, int CostCenter)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails.AccounIindex = 0;
		tran_TranDetails.TranDesc = " تكلفة فاتورة تأجير آجلة رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails.Debit = 0m;
		tran_TranDetails.Credit = TotalCost;
		tran_TranDetails.CostCentersID = CostCenter;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails2.AccounIindex = 0;
		tran_TranDetails2.TranDesc = " تكلفة فاتورة تأجير آجلة رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails2.Debit = TotalCost;
		tran_TranDetails2.Credit = 0m;
		tran_TranDetails2.CostCentersID = CostCenter;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = SuppNum;
		tran_TranDetails3.AccounIindex = 1;
		tran_TranDetails3.TranDesc = "  تأجير آجلة  رقم  ( " + PurID + " ) معدلة";
		tran_TranDetails3.Debit = NetCash;
		tran_TranDetails3.Credit = 0m;
		tran_TranDetails3.CostCentersID = CostCenter;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails3);
		num = db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
			tran_TranDetails4.TranSn = TranID;
			tran_TranDetails4.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails4.AccounIindex = 0;
			tran_TranDetails4.TranDesc = "  تأجير آجلة  رقم  ( " + PurID + " ) ط­/ " + Name + " معدلة";
			tran_TranDetails4.Debit = 0m;
			tran_TranDetails4.Credit = Vat;
			tran_TranDetails4.CostCentersID = CostCenter;
			tran_TranDetails4.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails4);
			db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
		tran_TranDetails5.TranSn = TranID;
		tran_TranDetails5.Account_Sn = LoginDetails.Acc_OrderAjila;
		tran_TranDetails5.AccounIindex = 1;
		tran_TranDetails5.TranDesc = "  تأجير آجلة  رقم  ( " + PurID + " ) ط­/ " + Name + "معدلة ";
		tran_TranDetails5.Debit = 0m;
		tran_TranDetails5.Credit = TotalSell;
		tran_TranDetails5.CostCentersID = CostCenter;
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
			tran_TranDetails6.Debit = 0m;
			tran_TranDetails6.Credit = TotalTobacc;
			tran_TranDetails6.CostCentersID = CostCenter;
			tran_TranDetails6.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails6);
			num = db.SaveChanges();
		}
		return num;
	}

	public int DeleteTran(int Code)
	{
		int result = 0;
		try
		{
			int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)17 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
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
			int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)17 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
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

	public int InsertRecipt(out int Code, int FormRentID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierIDNum, DateTime RentalDate, DateTime DateReceipt, DateTime Date, string ParentName, int CostCenter, string Note, string Address, decimal DaysNum, int DaysAcc, decimal TotalPrices, decimal Tax, decimal AllDisCount, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, int PaymantType, decimal CashMoney, decimal CashBank, decimal Paid, decimal Rest, decimal deposit, decimal Fine, decimal Recipient, decimal depositRest)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.OrderRent_Recipt3.Select((OrderRent_Recipt3 p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.OrderRent_Recipt3
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		OrderRent_Recipt3 orderRent_Recipt = new OrderRent_Recipt3();
		orderRent_Recipt.ID = iD;
		CachCode = num2;
		Code = num2;
		orderRent_Recipt.FormRentID = FormRentID;
		orderRent_Recipt.BranchID = LoginDetails.BranchID;
		orderRent_Recipt.PurBranchID = num2;
		orderRent_Recipt.SupplierID = SupplierID;
		orderRent_Recipt.SupplierName = SupplierName;
		orderRent_Recipt.SupplierPhone = SupplierPhone;
		orderRent_Recipt.SupplierIDNum = SupplierIDNum;
		orderRent_Recipt.ParentName = ParentName;
		orderRent_Recipt.CostCentersID = CostCenter;
		orderRent_Recipt.Note = Note;
		orderRent_Recipt.Address = Address;
		orderRent_Recipt.OrderPaymantType = PaymantType;
		orderRent_Recipt.RentalDate = RentalDate;
		orderRent_Recipt.DateReceipt = DateReceipt;
		orderRent_Recipt.Date = Date;
		orderRent_Recipt.DaysNum = DaysNum;
		orderRent_Recipt.DaysAcc = DaysAcc;
		orderRent_Recipt.TotalPrices = TotalPrices;
		orderRent_Recipt.Tax = Tax;
		orderRent_Recipt.AllDiscount = AllDisCount;
		orderRent_Recipt.DiscountNum = DiscountNum;
		orderRent_Recipt.DiscountPerantage = DiscountPerantage;
		orderRent_Recipt.Tax_Discount = Tax_Discount;
		orderRent_Recipt.TotalPrices_Discount = TotalPrices_Discount;
		orderRent_Recipt.Net = Net;
		orderRent_Recipt.CashMoney = CashMoney;
		orderRent_Recipt.CashBank = CashBank;
		orderRent_Recipt.Paid = Paid;
		orderRent_Recipt.rest = Rest;
		orderRent_Recipt.deposit = deposit;
		orderRent_Recipt.Fine = Fine;
		orderRent_Recipt.Recipient = Recipient;
		orderRent_Recipt.depositRest = depositRest;
		orderRent_Recipt.UserID_Add = LoginDetails.UserID;
		orderRent_Recipt.UserBranch_Add = LoginDetails.BranchID;
		orderRent_Recipt.UserMacAddress_Add = LoginDetails.macAddress;
		orderRent_Recipt.UserDate_Add = DateTime.Now;
		db.OrderRent_Recipt3.Add(orderRent_Recipt);
		num = db.SaveChanges();
		bool value = false;
		Orders_RentalInvoice orders_RentalInvoice = db.Orders_RentalInvoice.FirstOrDefault((Orders_RentalInvoice o) => o.ID == FormRentID);
		if (orders_RentalInvoice != null)
		{
			orders_RentalInvoice.ISRent = value;
			db.SaveChanges();
		}
		return num2;
	}

	public int InsertItemsRecipt(DataTable Items, int Purchese_ID)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["ItemCode"].ToString() != "")
			{
				OrderRent_RecpitDetails3 orderRent_RecpitDetails = new OrderRent_RecpitDetails3();
				int ItemRowChek = Convert.ToInt32(row["ItemID"]);
				Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
				string text = row["ItemCode"].ToString();
				orderRent_RecpitDetails.Purchese_ID = Purchese_ID;
				orderRent_RecpitDetails.BranchID = LoginDetails.BranchID;
				orderRent_RecpitDetails.ItemID = Convert.ToInt32(row["ItemID"]);
				string nmae = row["UnitName"].ToString();
				orderRent_RecpitDetails.ItemUnitID = GetUnitIdByName(nmae);
				orderRent_RecpitDetails.SmallUnitPrice = Convert.ToDecimal(row["UnitPrice"].ToString());
				orderRent_RecpitDetails.Quantity = Convert.ToDecimal(row["Quantity"].ToString());
				orderRent_RecpitDetails.QuantityDelivered = Convert.ToDecimal(row["QuantityDev"].ToString());
				orderRent_RecpitDetails.StoreID = Convert.ToInt32(row["StoreID"]);
				orderRent_RecpitDetails.LastCost = Math.Round(Convert.ToDecimal(row["AvergItem"].ToString()), DecimalNum);
				orderRent_RecpitDetails.UnitPrice = Math.Round(Convert.ToDecimal(row["UnitPrice"].ToString()), DecimalNum);
				orderRent_RecpitDetails.TotalPrice = Math.Round(Convert.ToDecimal(row["Total"].ToString()), DecimalNum);
				orderRent_RecpitDetails.SumPrice = Math.Round(Convert.ToDecimal(row["SumPrice"].ToString()), DecimalNum);
				orderRent_RecpitDetails.NetUnitPrice = Math.Round(Convert.ToDecimal(row["UnitPriceDisc"].ToString()), DecimalNum);
				orderRent_RecpitDetails.NetTotalPrice = Math.Round(Convert.ToDecimal(row["Net"].ToString()), DecimalNum);
				orderRent_RecpitDetails.VAT = Math.Round(Convert.ToDecimal(row["ItemVat"].ToString()), DecimalNum);
				orderRent_RecpitDetails.ItemUnitType = row["UnitType"].ToString();
				orderRent_RecpitDetails.VAT_Discount = Math.Round(Convert.ToDecimal(row["ItemVatDisc"].ToString()), DecimalNum);
				orderRent_RecpitDetails.AmountDisc = Convert.ToDecimal(row["AmountDisc"]);
				orderRent_RecpitDetails.PercentDisc = Convert.ToDecimal(row["PercentDisc"]);
				orderRent_RecpitDetails.ItemNote = row["ItemNote"].ToString();
				db.OrderRent_RecpitDetails3.Add(orderRent_RecpitDetails);
				result = db.SaveChanges();
				if (item_Items.item_Type == 1)
				{
					ItemQuantityOut(Convert.ToInt32(row["ItemID"]), Convert.ToDecimal(row["Quantity"].ToString()), row["UnitType"].ToString(), Convert.ToInt32(row["StoreID"].ToString()), 1);
				}
			}
		}
		return result;
	}
}


