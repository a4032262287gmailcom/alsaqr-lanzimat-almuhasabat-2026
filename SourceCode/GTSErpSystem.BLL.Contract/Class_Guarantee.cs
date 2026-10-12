using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.Contract;

public class Class_Guarantee
{
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

	public List<Contract_Guarantee> GetPurchases()
	{
		return db.Contract_Guarantee.Where((Contract_Guarantee x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Contract_Guarantee> GetPurchases(int Code)
	{
		return db.Contract_Guarantee.Where((Contract_Guarantee x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
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
					item_ItemComponent.Amount = Convert.ToDecimal(row["كمية الانتهاء"].ToString());
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

	public int ItemQuantityOut(int CustID, int ItemID, DateTime ItemDate, decimal Quantity, string unitType)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		int result = 0;
		if (ItemID > 0)
		{
			Contract_Store contract_Store = gTSdbEntities.Contract_Store.Where((Contract_Store x) => x.ItemID == (int?)ItemID && x.CustID == (int?)CustID && x.ItemDate == ItemDate).FirstOrDefault();
			Item_Items item_Items = gTSdbEntities.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault();
			if (contract_Store != null)
			{
				Contract_Store contract_Store2 = gTSdbEntities.Contract_Store.Where((Contract_Store x) => x.ItemID == (int?)ItemID && x.CustID == (int?)CustID && x.ItemDate == ItemDate).FirstOrDefault();
				if (contract_Store2 != null)
				{
					if (unitType == "الصغرى")
					{
						contract_Store2.QuantityExpire = Quantity;
						contract_Store2.ItemDateExpire = ItemDate;
						result = gTSdbEntities.SaveChanges();
					}
					if (unitType == "المتوسطة")
					{
						decimal value = Quantity;
						decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
						contract_Store2.QuantityExpire = (decimal?)value * convertMediumUnit;
						contract_Store2.ItemDateExpire = ItemDate;
						result = gTSdbEntities.SaveChanges();
					}
					if (unitType == "الكبرى")
					{
						decimal value = Quantity;
						decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
						contract_Store2.QuantityExpire = (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
						contract_Store2.ItemDateExpire = ItemDate;
						result = gTSdbEntities.SaveChanges();
					}
				}
				return result;
			}
			Contract_Store contract_Store3 = new Contract_Store();
			int iD = Convert.ToInt32(gTSdbEntities.Contract_Store.Select((Contract_Store p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
			contract_Store3.ID = iD;
			contract_Store3.ItemID = ItemID;
			contract_Store3.CustID = CustID;
			contract_Store3.ItemDate = ItemDate;
			contract_Store3.ItemDateExpire = ItemDate;
			if (unitType == "الصغرى")
			{
				contract_Store3.QuantityExpire = Quantity;
				contract_Store3.Quantity = Quantity;
			}
			if (unitType == "المتوسطة")
			{
				decimal value = Quantity;
				decimal? convertMediumUnit3 = item_Items.ConvertMediumUnit;
				contract_Store3.QuantityExpire = (decimal?)value * convertMediumUnit3;
				value = Quantity;
				convertMediumUnit3 = item_Items.ConvertMediumUnit;
				contract_Store3.Quantity = (decimal?)value * convertMediumUnit3;
			}
			if (unitType == "الكبرى")
			{
				decimal value = Quantity;
				decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
				contract_Store3.QuantityExpire = (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
				value = Quantity;
				convertMediumUnit2 = item_Items.ConvertMediumUnit;
				contract_Store3.Quantity = (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
			}
			gTSdbEntities.Contract_Store.Add(contract_Store3);
			result = gTSdbEntities.SaveChanges();
		}
		return result;
	}

	public int ItemQuantityIn(int CustID, int ItemID, DateTime ItemDate, DateTime ItemDateExpire, decimal QuantityExpire, string unitType)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		int result = 0;
		if (ItemID > 0)
		{
			Contract_Store contract_Store = gTSdbEntities.Contract_Store.Where((Contract_Store x) => x.ItemID == (int?)ItemID && x.CustID == (int?)CustID && x.ItemDate == ItemDate).FirstOrDefault();
			Item_Items item_Items = gTSdbEntities.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).FirstOrDefault();
			if (contract_Store != null)
			{
				Contract_Store contract_Store2 = gTSdbEntities.Contract_Store.Where((Contract_Store x) => x.ItemID == (int?)ItemID && x.CustID == (int?)CustID && x.ItemDate == ItemDate).FirstOrDefault();
				if (contract_Store2 != null)
				{
					if (unitType == "الصغرى")
					{
						contract_Store2.QuantityExpire = QuantityExpire;
						contract_Store2.ItemDateExpire = ItemDateExpire;
						result = gTSdbEntities.SaveChanges();
					}
					if (unitType == "المتوسطة")
					{
						decimal value = QuantityExpire;
						decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
						contract_Store2.QuantityExpire = (decimal?)value * convertMediumUnit;
						contract_Store2.ItemDateExpire = ItemDateExpire;
						result = gTSdbEntities.SaveChanges();
					}
					if (unitType == "الكبرى")
					{
						decimal value = QuantityExpire;
						decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
						contract_Store2.QuantityExpire = (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
						contract_Store2.ItemDateExpire = ItemDateExpire;
						result = gTSdbEntities.SaveChanges();
					}
				}
				return result;
			}
			Contract_Store contract_Store3 = new Contract_Store();
			int iD = Convert.ToInt32(gTSdbEntities.Contract_Store.Select((Contract_Store p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
			contract_Store3.ID = iD;
			contract_Store3.ItemID = ItemID;
			contract_Store3.CustID = CustID;
			contract_Store3.ItemDate = ItemDateExpire;
			contract_Store3.ItemDateExpire = ItemDateExpire;
			if (unitType == "الصغرى")
			{
				contract_Store3.Quantity = QuantityExpire;
				contract_Store3.QuantityExpire = QuantityExpire;
			}
			if (unitType == "المتوسطة")
			{
				decimal value = QuantityExpire;
				decimal? convertMediumUnit3 = item_Items.ConvertMediumUnit;
				contract_Store3.Quantity = (decimal?)value * convertMediumUnit3;
				value = QuantityExpire;
				convertMediumUnit3 = item_Items.ConvertMediumUnit;
				contract_Store3.QuantityExpire = (decimal?)value * convertMediumUnit3;
			}
			if (unitType == "الكبرى")
			{
				decimal value = QuantityExpire;
				decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
				contract_Store3.Quantity = (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
				value = QuantityExpire;
				convertMediumUnit2 = item_Items.ConvertMediumUnit;
				contract_Store3.QuantityExpire = (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
			}
			gTSdbEntities.Contract_Store.Add(contract_Store3);
			result = gTSdbEntities.SaveChanges();
		}
		return result;
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
		decimal num2 = Convert.ToDecimal(db.Contract_Store.Where((Contract_Store x) => x.ItemID == (int?)Itemid).Sum((Contract_Store x) => x.Quantity));
		decimal num3 = Convert.ToDecimal(db.Contract_Store.Where((Contract_Store x) => x.ItemID == (int?)Itemid).Sum((Contract_Store x) => x.Quantity));
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

	public int GetItmID(string Code)
	{
		return (from xx in db.Item_Items
			where xx.Item_code == Code
			select xx.ItemId).FirstOrDefault();
	}

	public List<Contract_GuaranteeDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.Contract_GuaranteeDetails.Where((Contract_GuaranteeDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_Contract_Guarantee_Result> GetSelectAllPurchases()
	{
		return db.Select_Contract_Guarantee(LoginDetails.BranchID).ToList();
	}

	public int GetUnitIdByName(string Nmae)
	{
		int num = 0;
		return (from x in db.Item_Unit
			where x.Name == Nmae
			select x.ID).FirstOrDefault();
	}

	public int InsertItems(DataTable Items, int Purchese_ID, int CustID)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["كود_الصنف"].ToString() != "")
			{
				Contract_GuaranteeDetails contract_GuaranteeDetails = new Contract_GuaranteeDetails();
				string Itemcod = row["كود_الصنف"].ToString();
				string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
				int ItemRowChek = Convert.ToInt32(value);
				Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
				int num = Convert.ToInt32(db.TblSettings.Select((TblSetting x) => x.StoreID).FirstOrDefault());
				if (item_Items != null)
				{
					contract_GuaranteeDetails.Purchese_ID = Purchese_ID;
					contract_GuaranteeDetails.BranchID = LoginDetails.BranchID;
					contract_GuaranteeDetails.ItemID = Convert.ToInt32(item_Items.ItemId);
					string nmae = row["الوحدة"].ToString();
					contract_GuaranteeDetails.ItemUnitID = GetUnitIdByName(nmae);
					contract_GuaranteeDetails.SmallUnitPrice = Convert.ToDecimal(row["سعر الوحدة"].ToString());
					contract_GuaranteeDetails.Quantity = Convert.ToDecimal(row["كمية الشراء"].ToString());
					contract_GuaranteeDetails.QuantityExpire = Convert.ToDecimal(row["كمية الانتهاء"].ToString());
					contract_GuaranteeDetails.LastCost = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"].ToString()), DecimalNum);
					contract_GuaranteeDetails.UnitPrice = Math.Round(Convert.ToDecimal(row["سعر الوحدة"].ToString()), DecimalNum);
					contract_GuaranteeDetails.TotalPrice = Math.Round(Convert.ToDecimal(row["الاجمالى"].ToString()), DecimalNum);
					contract_GuaranteeDetails.NetUnitPrice = Math.Round(Convert.ToDecimal(row["سعربعدالخصم"].ToString()), DecimalNum);
					contract_GuaranteeDetails.NetTotalPrice = Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"].ToString()), DecimalNum);
					contract_GuaranteeDetails.VAT = Math.Round(Convert.ToDecimal(row["الضربية"].ToString()), DecimalNum);
					contract_GuaranteeDetails.ItemUnitType = row["أسم_الوحدة"].ToString();
					contract_GuaranteeDetails.VAT_Discount = Math.Round(Convert.ToDecimal(row["الضريبه_بعد_الخصم"].ToString()), DecimalNum);
					contract_GuaranteeDetails.ItemDate = Convert.ToDateTime(row["تاريخ الشراء"].ToString());
					contract_GuaranteeDetails.ItemDateExpire = Convert.ToDateTime(row["تاريخ الانتهاء"].ToString());
					db.Contract_GuaranteeDetails.Add(contract_GuaranteeDetails);
					result = db.SaveChanges();
					ItemQuantityIn(CustID, Convert.ToInt32(value), Convert.ToDateTime(Convert.ToDateTime(row["تاريخ الشراء"]).ToShortDateString()), Convert.ToDateTime(Convert.ToDateTime(row["تاريخ الانتهاء"]).ToShortDateString()), Convert.ToDecimal(row["كمية الانتهاء"].ToString()), row["أسم_الوحدة"].ToString());
				}
			}
		}
		return result;
	}

	public int InsertPurch(out int Code, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, string Note, string NoteNum, decimal CostOrder, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal AllTax)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.Contract_Guarantee.Select((Contract_Guarantee p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Contract_Guarantee
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		Contract_Guarantee contract_Guarantee = new Contract_Guarantee();
		contract_Guarantee.ID = iD;
		Code = num2;
		contract_Guarantee.BranchID = LoginDetails.BranchID;
		contract_Guarantee.PurBranchID = num2;
		contract_Guarantee.SupplierID = SupplierID;
		contract_Guarantee.SupplierName = SupplierName;
		contract_Guarantee.SupplierPhone = SupplierPhone;
		contract_Guarantee.SupplierVatNum = SupplierVatNum;
		contract_Guarantee.Purchases_Date = Purchases_Date;
		contract_Guarantee.Note = Note;
		contract_Guarantee.NoteNum = NoteNum;
		contract_Guarantee.CostOrder = CostOrder;
		contract_Guarantee.TotalPrices = TotalPrices;
		contract_Guarantee.Tax = Tax;
		contract_Guarantee.Safy = Safy;
		contract_Guarantee.DiscountNum = DiscountNum;
		contract_Guarantee.DiscountPerantage = DiscountPerantage;
		contract_Guarantee.Tax_Discount = Tax_Discount;
		contract_Guarantee.TotalPrices_Discount = TotalPrices_Discount;
		contract_Guarantee.Net = Net;
		contract_Guarantee.AllTax = AllTax;
		contract_Guarantee.UserID_Add = LoginDetails.UserID;
		contract_Guarantee.UserBranch_Add = LoginDetails.BranchID;
		contract_Guarantee.UserMacAddress_Add = LoginDetails.macAddress;
		contract_Guarantee.UserDate_Add = DateTime.Now;
		db.Contract_Guarantee.Add(contract_Guarantee);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID, int CustID)
	{
		List<Contract_GuaranteeDetails> list = db.Contract_GuaranteeDetails.Where((Contract_GuaranteeDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Contract_GuaranteeDetails item in list)
		{
			ItemQuantityOut(CustID, Convert.ToInt32(item.ItemID), Convert.ToDateTime(Convert.ToDateTime(item.ItemDate).ToShortDateString()), Convert.ToDecimal(item.Quantity), item.ItemUnitType);
			db.Contract_GuaranteeDetails.Attach(item);
			db.Contract_GuaranteeDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, string Note, string NoteNum, decimal CostOrder, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal AllTax)
	{
		int result = 0;
		Contract_Guarantee contract_Guarantee = db.Contract_Guarantee.Where((Contract_Guarantee x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.Contract_Guarantee.Where((Contract_Guarantee x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (contract_Guarantee != null)
		{
			contract_Guarantee.SupplierID = SupplierID;
			contract_Guarantee.SupplierName = SupplierName;
			contract_Guarantee.SupplierPhone = SupplierPhone;
			contract_Guarantee.SupplierVatNum = SupplierVatNum;
			contract_Guarantee.Purchases_Date = Purchases_Date;
			contract_Guarantee.Note = Note;
			contract_Guarantee.NoteNum = NoteNum;
			contract_Guarantee.TotalPrices = TotalPrices;
			contract_Guarantee.Tax = Tax;
			contract_Guarantee.Safy = Safy;
			contract_Guarantee.CostOrder = CostOrder;
			contract_Guarantee.DiscountNum = DiscountNum;
			contract_Guarantee.DiscountPerantage = DiscountPerantage;
			contract_Guarantee.Tax_Discount = Tax_Discount;
			contract_Guarantee.TotalPrices_Discount = TotalPrices_Discount;
			contract_Guarantee.Net = Net;
			contract_Guarantee.AllTax = AllTax;
			contract_Guarantee.UserID_Update = LoginDetails.UserID;
			contract_Guarantee.UserBranch_Update = LoginDetails.GroupID;
			contract_Guarantee.UserMacAddress_Update = LoginDetails.macAddress;
			contract_Guarantee.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public int Delete(int PurID, int CusID)
	{
		int result = 0;
		Contract_Guarantee contract_Guarantee = db.Contract_Guarantee.Where((Contract_Guarantee x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (contract_Guarantee != null)
		{
			deleteItemsByPurcahesId(PurID, CusID);
			db.Contract_Guarantee.Attach(contract_Guarantee);
			db.Contract_Guarantee.Remove(contract_Guarantee);
			result = db.SaveChanges();
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

	public List<Get_Cust_Guarantee_Result> GetCustGuarantee(int CustID)
	{
		return db.Get_Cust_Guarantee(CustID, LoginDetails.BranchID).ToList();
	}
}


