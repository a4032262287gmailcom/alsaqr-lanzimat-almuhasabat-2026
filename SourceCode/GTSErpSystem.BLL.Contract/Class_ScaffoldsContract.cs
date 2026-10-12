using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace GTSErpSystem.BLL.Contract;

public class Class_ScaffoldsContract
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

	public List<Contract_Contract> GetPurchases()
	{
		return db.Contract_Contract.Where((Contract_Contract x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Scaffolds_Contract> GetPurchases(int Code)
	{
		return db.Scaffolds_Contract.Where((Scaffolds_Contract x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
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
						contract_Store2.Quantity -= (decimal?)Quantity;
						contract_Store2.QuantityExpire -= (decimal?)Quantity;
						result = gTSdbEntities.SaveChanges();
					}
					if (unitType == "المتوسطة")
					{
						decimal? quantity = contract_Store2.Quantity;
						decimal value = Quantity;
						decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
						contract_Store2.Quantity = quantity - (decimal?)value * convertMediumUnit;
						decimal? quantityExpire = contract_Store2.QuantityExpire;
						value = Quantity;
						convertMediumUnit = item_Items.ConvertMediumUnit;
						contract_Store2.QuantityExpire = quantityExpire - (decimal?)value * convertMediumUnit;
						result = gTSdbEntities.SaveChanges();
					}
					if (unitType == "الكبرى")
					{
						decimal? quantity = contract_Store2.Quantity;
						decimal value = Quantity;
						decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
						contract_Store2.Quantity = quantity - (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
						decimal? quantityExpire = contract_Store2.QuantityExpire;
						value = Quantity;
						convertMediumUnit2 = item_Items.ConvertMediumUnit;
						contract_Store2.QuantityExpire = quantityExpire - (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
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
				contract_Store3.Quantity = -Quantity;
				contract_Store3.QuantityExpire = -Quantity;
			}
			if (unitType == "المتوسطة")
			{
				decimal value = -Quantity;
				decimal? quantity = item_Items.ConvertMediumUnit;
				contract_Store3.Quantity = (decimal?)value * quantity;
				value = -Quantity;
				quantity = item_Items.ConvertMediumUnit;
				contract_Store3.QuantityExpire = (decimal?)value * quantity;
			}
			if (unitType == "الكبرى")
			{
				decimal value = -Quantity;
				decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
				contract_Store3.Quantity = (decimal?)value * convertMediumUnit * item_Items.ConvertBigUnit;
				value = -Quantity;
				convertMediumUnit = item_Items.ConvertMediumUnit;
				contract_Store3.QuantityExpire = (decimal?)value * convertMediumUnit * item_Items.ConvertBigUnit;
			}
			gTSdbEntities.Contract_Store.Add(contract_Store3);
			result = gTSdbEntities.SaveChanges();
		}
		return result;
	}

	public int ItemQuantityIn(int CustID, int ItemID, DateTime ItemDate, decimal Quantity, string unitType)
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
						contract_Store2.Quantity += (decimal?)Quantity;
						contract_Store2.QuantityExpire += (decimal?)Quantity;
						result = gTSdbEntities.SaveChanges();
					}
					if (unitType == "المتوسطة")
					{
						decimal? quantity = contract_Store2.Quantity;
						decimal value = Quantity;
						decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
						contract_Store2.Quantity = quantity + (decimal?)value * convertMediumUnit;
						decimal? quantityExpire = contract_Store2.QuantityExpire;
						value = Quantity;
						convertMediumUnit = item_Items.ConvertMediumUnit;
						contract_Store2.QuantityExpire = quantityExpire + (decimal?)value * convertMediumUnit;
						result = gTSdbEntities.SaveChanges();
					}
					if (unitType == "الكبرى")
					{
						decimal? quantity = contract_Store2.Quantity;
						decimal value = Quantity;
						decimal? convertMediumUnit2 = item_Items.ConvertMediumUnit;
						contract_Store2.Quantity = quantity + (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
						decimal? quantityExpire = contract_Store2.QuantityExpire;
						value = Quantity;
						convertMediumUnit2 = item_Items.ConvertMediumUnit;
						contract_Store2.QuantityExpire = quantityExpire + (decimal?)value * convertMediumUnit2 * item_Items.ConvertBigUnit;
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
				contract_Store3.Quantity = Quantity;
				contract_Store3.QuantityExpire = Quantity;
			}
			if (unitType == "المتوسطة")
			{
				decimal value = Quantity;
				decimal? quantity = item_Items.ConvertMediumUnit;
				contract_Store3.Quantity = (decimal?)value * quantity;
				value = Quantity;
				quantity = item_Items.ConvertMediumUnit;
				contract_Store3.QuantityExpire = (decimal?)value * quantity;
			}
			if (unitType == "الكبرى")
			{
				decimal value = Quantity;
				decimal? convertMediumUnit = item_Items.ConvertMediumUnit;
				contract_Store3.Quantity = (decimal?)value * convertMediumUnit * item_Items.ConvertBigUnit;
				value = Quantity;
				convertMediumUnit = item_Items.ConvertMediumUnit;
				contract_Store3.QuantityExpire = (decimal?)value * convertMediumUnit * item_Items.ConvertBigUnit;
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

	public List<Scaffolds_ContractDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.Scaffolds_ContractDetails.Where((Scaffolds_ContractDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_Scaffolds_Contract_Result> GetSelectAllPurchases()
	{
		return db.Select_Scaffolds_Contract(LoginDetails.BranchID).ToList();
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
				Scaffolds_ContractDetails scaffolds_ContractDetails = new Scaffolds_ContractDetails();
				string Itemcod = row["كود_الصنف"].ToString();
				string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
				int ItemRowChek = Convert.ToInt32(value);
				Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
				int num = Convert.ToInt32(db.TblSettings.Select((TblSetting x) => x.StoreID).FirstOrDefault());
				if (item_Items != null)
				{
					scaffolds_ContractDetails.Purchese_ID = Purchese_ID;
					scaffolds_ContractDetails.BranchID = LoginDetails.BranchID;
					scaffolds_ContractDetails.ItemID = Convert.ToInt32(item_Items.ItemId);
					string nmae = row["الوحدة"].ToString();
					scaffolds_ContractDetails.ItemUnitID = GetUnitIdByName(nmae);
					scaffolds_ContractDetails.SmallUnitPrice = Convert.ToDecimal(row["سعر الوحدة"].ToString());
					scaffolds_ContractDetails.Quantity = Convert.ToDecimal(row["الكمية"].ToString());
					scaffolds_ContractDetails.LastCost = Math.Round(Convert.ToDecimal(row["متوسط_التكلفة"].ToString()), DecimalNum);
					scaffolds_ContractDetails.UnitPrice = Math.Round(Convert.ToDecimal(row["سعر الوحدة"].ToString()), DecimalNum);
					scaffolds_ContractDetails.TotalPrice = Math.Round(Convert.ToDecimal(row["الاجمالى"].ToString()), DecimalNum);
					scaffolds_ContractDetails.NetUnitPrice = Math.Round(Convert.ToDecimal(row["سعربعدالخصم"].ToString()), DecimalNum);
					scaffolds_ContractDetails.NetTotalPrice = Math.Round(Convert.ToDecimal(row["إجمالى_بعدالخصم"].ToString()), DecimalNum);
					scaffolds_ContractDetails.VAT = Math.Round(Convert.ToDecimal(row["الضربية"].ToString()), DecimalNum);
					scaffolds_ContractDetails.ItemUnitType = row["أسم_الوحدة"].ToString();
					scaffolds_ContractDetails.VAT_Discount = Math.Round(Convert.ToDecimal(row["الضريبه_بعد_الخصم"].ToString()), DecimalNum);
					scaffolds_ContractDetails.ItemDate = Convert.ToDateTime(row["التاريخ"].ToString());
					scaffolds_ContractDetails.LastPrice = Convert.ToDecimal(row["LastPrice"].ToString());
					db.Scaffolds_ContractDetails.Add(scaffolds_ContractDetails);
					result = db.SaveChanges();
					ItemQuantityIn(CustID, Convert.ToInt32(value), Convert.ToDateTime(Convert.ToDateTime(row["التاريخ"]).ToShortDateString()), Convert.ToDecimal(row["الكمية"].ToString()), row["أسم_الوحدة"].ToString());
				}
			}
		}
		return result;
	}

	public void SaveContractFiles(int contractId, List<string> filePaths)
	{
		foreach (string filePath in filePaths)
		{
			Contract_Files entity = new Contract_Files
			{
				ContractID = contractId,
				FilePath = filePath,
				UploadDate = DateTime.Now
			};
			db.Contract_Files.Add(entity);
		}
		db.SaveChanges();
	}

	public List<string> GetContractFilePaths(int contractId)
	{
		return (from f in db.Contract_Files
			where f.ContractID == contractId
			select f.FilePath).ToList();
	}

	public void DeleteContractFiles(int contractId)
	{
		List<Contract_Files> list = db.Contract_Files.Where((Contract_Files f) => f.ContractID == contractId).ToList();
		if (!list.Any())
		{
			return;
		}
		foreach (Contract_Files item in list)
		{
			if (File.Exists(item.FilePath))
			{
				try
				{
					File.Delete(item.FilePath);
				}
				catch (Exception)
				{
				}
			}
		}
		db.Contract_Files.RemoveRange(list);
		db.SaveChanges();
	}

	public int InsertPurch(out int Code, int BounceID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime From_Date, DateTime To_Date, string Note, string NoteNum, decimal CostOrder, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal AllTax, string Location, int DaysNum, string Payment, int ContractType)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.Scaffolds_Contract.Select((Scaffolds_Contract p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Scaffolds_Contract
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		Scaffolds_Contract scaffolds_Contract = new Scaffolds_Contract();
		scaffolds_Contract.ID = iD;
		Code = num2;
		scaffolds_Contract.BranchID = LoginDetails.BranchID;
		scaffolds_Contract.PurBranchID = num2;
		scaffolds_Contract.BounceID = BounceID;
		scaffolds_Contract.SupplierID = SupplierID;
		scaffolds_Contract.SupplierName = SupplierName;
		scaffolds_Contract.SupplierPhone = SupplierPhone;
		scaffolds_Contract.SupplierVatNum = SupplierVatNum;
		scaffolds_Contract.From_Date = From_Date;
		scaffolds_Contract.To_Date = To_Date;
		scaffolds_Contract.Note = Note;
		scaffolds_Contract.DayNum = DaysNum;
		scaffolds_Contract.NoteNum = NoteNum;
		scaffolds_Contract.CostOrder = CostOrder;
		scaffolds_Contract.TotalPrices = TotalPrices;
		scaffolds_Contract.Tax = Tax;
		scaffolds_Contract.Safy = Safy;
		scaffolds_Contract.DiscountNum = DiscountNum;
		scaffolds_Contract.DiscountPerantage = DiscountPerantage;
		scaffolds_Contract.Tax_Discount = Tax_Discount;
		scaffolds_Contract.TotalPrices_Discount = TotalPrices_Discount;
		scaffolds_Contract.Net = Net;
		scaffolds_Contract.AllTax = AllTax;
		scaffolds_Contract.Location = Location;
		scaffolds_Contract.Pyment = Payment;
		scaffolds_Contract.IsClosed = false;
		scaffolds_Contract.ContractType = ContractType;
		scaffolds_Contract.UserID_Add = LoginDetails.UserID;
		scaffolds_Contract.UserBranch_Add = LoginDetails.BranchID;
		scaffolds_Contract.UserMacAddress_Add = LoginDetails.macAddress;
		scaffolds_Contract.UserDate_Add = DateTime.Now;
		db.Scaffolds_Contract.Add(scaffolds_Contract);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID, int CustID)
	{
		List<Scaffolds_ContractDetails> list = db.Scaffolds_ContractDetails.Where((Scaffolds_ContractDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Scaffolds_ContractDetails item in list)
		{
			ItemQuantityOut(CustID, Convert.ToInt32(item.ItemID), Convert.ToDateTime(Convert.ToDateTime(item.ItemDate).ToShortDateString()), Convert.ToDecimal(item.Quantity), item.ItemUnitType);
			db.Scaffolds_ContractDetails.Attach(item);
			db.Scaffolds_ContractDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, int BounceID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Form_Date, DateTime To_Date, string Note, string NoteNum, decimal CostOrder, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal TotalPrices_Discount, decimal Net, decimal AllTax, string Location, int DaysNum, string Payment, int ContractType)
	{
		int result = 0;
		Scaffolds_Contract scaffolds_Contract = db.Scaffolds_Contract.Where((Scaffolds_Contract x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.Scaffolds_Contract.Where((Scaffolds_Contract x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (scaffolds_Contract != null)
		{
			scaffolds_Contract.BounceID = BounceID;
			scaffolds_Contract.SupplierID = SupplierID;
			scaffolds_Contract.SupplierName = SupplierName;
			scaffolds_Contract.SupplierPhone = SupplierPhone;
			scaffolds_Contract.SupplierVatNum = SupplierVatNum;
			scaffolds_Contract.From_Date = Form_Date;
			scaffolds_Contract.To_Date = To_Date;
			scaffolds_Contract.Note = Note;
			scaffolds_Contract.NoteNum = NoteNum;
			scaffolds_Contract.DayNum = DaysNum;
			scaffolds_Contract.TotalPrices = TotalPrices;
			scaffolds_Contract.Tax = Tax;
			scaffolds_Contract.Safy = Safy;
			scaffolds_Contract.CostOrder = CostOrder;
			scaffolds_Contract.DiscountNum = DiscountNum;
			scaffolds_Contract.DiscountPerantage = DiscountPerantage;
			scaffolds_Contract.Tax_Discount = Tax_Discount;
			scaffolds_Contract.TotalPrices_Discount = TotalPrices_Discount;
			scaffolds_Contract.Net = Net;
			scaffolds_Contract.AllTax = AllTax;
			scaffolds_Contract.Location = Location;
			scaffolds_Contract.Pyment = Payment;
			scaffolds_Contract.ContractType = ContractType;
			scaffolds_Contract.UserID_Update = LoginDetails.UserID;
			scaffolds_Contract.UserBranch_Update = LoginDetails.GroupID;
			scaffolds_Contract.UserMacAddress_Update = LoginDetails.macAddress;
			scaffolds_Contract.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public int Delete(int PurID, int CusID)
	{
		int result = 0;
		Scaffolds_Contract scaffolds_Contract = db.Scaffolds_Contract.Where((Scaffolds_Contract x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (scaffolds_Contract != null)
		{
			deleteItemsByPurcahesId(PurID, CusID);
			db.Scaffolds_Contract.Attach(scaffolds_Contract);
			db.Scaffolds_Contract.Remove(scaffolds_Contract);
			result = db.SaveChanges();
		}
		return result;
	}

	public List<Account_Stores> Get_Stores()
	{
		return db.Account_Stores.Where((Account_Stores x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int Get_StoresIDByName(string StoreName)
	{
		return (from x in db.Account_Stores
			where x.Store_Name == StoreName
			select x.ID).FirstOrDefault();
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

	public int InsertTranNum(out int TRNIDDS, int PurID, int PurType, string Name, string NoteNum, DateTime PurDate)
	{
		int num = 0;
		TRNIDDS = 0;
		int num2 = Convert.ToInt32(db.Tran_Tran.Select((Tran_Tran p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		Tran_Tran tran_Tran = new Tran_Tran();
		tran_Tran.ID = num2;
		tran_Tran.ReferenceCode = PurID;
		tran_Tran.TranTypeID = 5;
		tran_Tran.DocCode = NoteNum;
		tran_Tran.TranDate = PurDate;
		tran_Tran.BranchID = LoginDetails.BranchID;
		tran_Tran.Note = "عقد تأجير سقلات رقم ( " + PurID + " ) ط­ /  " + Name;
		tran_Tran.UserID_Add = LoginDetails.UserID;
		tran_Tran.UserBranch_Add = LoginDetails.BranchID;
		tran_Tran.UserMacAddress_Add = LoginDetails.macAddress;
		tran_Tran.UserDate_Add = DateTime.Now;
		db.Tran_Tran.Add(tran_Tran);
		num = db.SaveChanges();
		TRNIDDS = num2;
		return TRNIDDS;
	}

	public int InsertTranDetailsAjilaNUM(int TranID, int ORDID, int SuppNum, string Name, decimal Cash, decimal TotalSell, decimal TotalTobacc, decimal Vat, decimal TotalCash, int NoteNum, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranID;
		tran_TranDetails.Account_Sn = LoginDetails.Acc_OrderReturnAgl;
		tran_TranDetails.AccounIindex = 1;
		tran_TranDetails.TranDesc = "تاجير سقلات  رقم  ( " + ORDID + " ) " + Name + " ";
		tran_TranDetails.Debit = TotalCash;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = NoteNum;
		tran_TranDetails.ProjectId = ProjectId;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranID;
		tran_TranDetails2.Account_Sn = SuppNum;
		tran_TranDetails2.AccounIindex = 2;
		tran_TranDetails2.TranDesc = "تاجير سقلات  رقم  ( " + ORDID + " ) " + Name + " ";
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = TotalSell;
		tran_TranDetails2.CostCentersID = 0;
		tran_TranDetails2.ProjectId = ProjectId;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		num = db.SaveChanges();
		Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
		tran_TranDetails3.TranSn = TranID;
		tran_TranDetails3.Account_Sn = LoginDetails.Acc_StockNum;
		tran_TranDetails3.AccounIindex = 3;
		tran_TranDetails3.TranDesc = "  مدفوعات تاجير سقلات رقم( " + ORDID + " ) " + Name + " ";
		tran_TranDetails3.Debit = 0m;
		tran_TranDetails3.Credit = Cash;
		tran_TranDetails3.CostCentersID = 0;
		tran_TranDetails3.ProjectId = ProjectId;
		tran_TranDetails3.BranchID = LoginDetails.BranchID;
		Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
		tran_TranDetails4.TranSn = TranID;
		tran_TranDetails4.Account_Sn = LoginDetails.Acc_CostItemOrder;
		tran_TranDetails4.AccounIindex = 4;
		tran_TranDetails3.TranDesc = "   تاجير سقلات رقم( " + ORDID + " ) " + Name + " ";
		tran_TranDetails4.Debit = Cash;
		tran_TranDetails4.Credit = 0m;
		tran_TranDetails4.CostCentersID = 0;
		tran_TranDetails4.ProjectId = ProjectId;
		tran_TranDetails4.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails4);
		num = db.SaveChanges();
		db.Tran_TranDetails.Add(tran_TranDetails3);
		db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
			tran_TranDetails5.TranSn = TranID;
			tran_TranDetails5.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails5.AccounIindex = 5;
			tran_TranDetails5.TranDesc = " ضريبة القيمه المضافه لسقلات  رقم  ( " + ORDID + " ) ط­/ " + Name + " ";
			tran_TranDetails5.Debit = Vat;
			tran_TranDetails5.Credit = 0m;
			tran_TranDetails5.CostCentersID = NoteNum;
			tran_TranDetails5.ProjectId = ProjectId;
			tran_TranDetails5.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails5);
			db.SaveChanges();
		}
		return num;
	}
}


