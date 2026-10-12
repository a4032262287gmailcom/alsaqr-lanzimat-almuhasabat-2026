using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.Orders;

internal class Class_PriceOffer
{
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

	public List<Order_PriceOffer> GetPurchases()
	{
		return db.Order_PriceOffer.Where((Order_PriceOffer x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Order_PriceOffer> GetPurchases(int Code)
	{
		return db.Order_PriceOffer.Where((Order_PriceOffer x) => x.PurBranchID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
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

	public List<Order_PriceOfferDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.Order_PriceOfferDetails.Where((Order_PriceOfferDetails X) => X.Purchese_ID == (int?)PurchasesID && X.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_Order_PriceOffer_Result> GetSelectAllPurchases()
	{
		return db.Select_Order_PriceOffer(LoginDetails.BranchID).ToList();
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
			if (row["ItemCode"].ToString() != "")
			{
				Order_PriceOfferDetails order_PriceOfferDetails = new Order_PriceOfferDetails();
				string Itemcod = row["ItemCode"].ToString();
				string value = Convert.ToString(db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcod).FirstOrDefault().ItemId);
				int ItemRowChek = Convert.ToInt32(value);
				Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.ItemId == ItemRowChek).FirstOrDefault();
				int value2 = Convert.ToInt32(db.TblSettings.Select((TblSetting x) => x.StoreID).FirstOrDefault());
				if (item_Items != null)
				{
					order_PriceOfferDetails.Purchese_ID = Purchese_ID;
					order_PriceOfferDetails.BranchID = LoginDetails.BranchID;
					order_PriceOfferDetails.ItemID = Convert.ToInt32(item_Items.ItemId);
					string nmae = row["UnitName"].ToString();
					order_PriceOfferDetails.ItemUnitID = GetUnitIdByName(nmae);
					order_PriceOfferDetails.SmallUnitPrice = Convert.ToDecimal(row["UnitPrice"].ToString());
					order_PriceOfferDetails.Quantity = Convert.ToDecimal(row["Quantity"].ToString());
					order_PriceOfferDetails.StoreID = value2;
					order_PriceOfferDetails.ItemNote = row["ItemNote"].ToString();
					order_PriceOfferDetails.UnitPrice = Math.Round(Convert.ToDecimal(row["UnitPrice"].ToString()), DecimalNum);
					order_PriceOfferDetails.TotalPrice = Math.Round(Convert.ToDecimal(row["Total"].ToString()), DecimalNum);
					order_PriceOfferDetails.NetUnitPrice = Math.Round(Convert.ToDecimal(row["UnitPriceDisc"].ToString()), DecimalNum);
					order_PriceOfferDetails.NetTotalPrice = Math.Round(Convert.ToDecimal(row["Net"].ToString()), DecimalNum);
					order_PriceOfferDetails.VAT = Math.Round(Convert.ToDecimal(row["ItemVat"].ToString()), DecimalNum);
					order_PriceOfferDetails.ItemUnitType = row["UnitType"].ToString();
					order_PriceOfferDetails.VAT_Discount = Math.Round(Convert.ToDecimal(row["ItemVatDisc"].ToString()), DecimalNum);
					order_PriceOfferDetails.StoreID = Convert.ToInt32(row["StoreID"]);
					order_PriceOfferDetails.UnitNumber = Convert.ToDecimal(row["UnitNum"]);
					order_PriceOfferDetails.Weight = Convert.ToDecimal(row["Weight"]);
					order_PriceOfferDetails.Height = Convert.ToDecimal(row["Height"]);
					order_PriceOfferDetails.width = Convert.ToDecimal(row["width"]);
					order_PriceOfferDetails.Long = Convert.ToDecimal(row["Long"]);
					order_PriceOfferDetails.LastCost = Convert.ToDecimal(row["AvergItem"]);
					order_PriceOfferDetails.Bounce = Convert.ToDecimal(row["BounceItem"]);
					order_PriceOfferDetails.Amount_Discount = Convert.ToDecimal(row["AmountDisc"]);
					order_PriceOfferDetails.Per_Discount = Convert.ToDecimal(row["PercentDisc"]);
					order_PriceOfferDetails.TafqitUnitId = Convert.ToInt32(row["TafqitID"]);
					order_PriceOfferDetails.TafqitQunitity = Convert.ToDecimal(row["QuantityTF"]);
					order_PriceOfferDetails.TobaccoTax = ((row["TaxTobacco"] != DBNull.Value) ? Convert.ToDecimal(row["TaxTobacco"]) : 0m);
					order_PriceOfferDetails.TobaccoTaxDis = ((row["TaxTobaccoDis"] != DBNull.Value) ? Convert.ToDecimal(row["TaxTobaccoDis"]) : 0m);
					order_PriceOfferDetails.TotalWithTaxTobacco = ((row["TotalWithTaxTobacco"] != DBNull.Value) ? Convert.ToDecimal(row["TotalWithTaxTobacco"]) : 0m);
					db.Order_PriceOfferDetails.Add(order_PriceOfferDetails);
					result = db.SaveChanges();
				}
			}
		}
		return result;
	}

	public int InsertPurch(out int Code, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, int CostCenter, string NoteBig, string Note, string NoteNum, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal AllDiscount, decimal Net, decimal TobaccoTax, decimal AllTax, string Street, string District, string City, string Country, string PostalCode, string AdditionalNum, string CommercialRecord, int Sectoral, decimal Charge, int OrderType, int ProjectId)
	{
		int num = 0;
		Code = 0;
		int iD = Convert.ToInt32(db.Order_PriceOffer.Select((Order_PriceOffer p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Order_PriceOffer
			where p.BranchID == (int?)LoginDetails.BranchID
			select p.PurBranchID).DefaultIfEmpty(0).Max()) + 1;
		Order_PriceOffer order_PriceOffer = new Order_PriceOffer();
		order_PriceOffer.ID = iD;
		CachCode = num2;
		Code = num2;
		order_PriceOffer.BranchID = LoginDetails.BranchID;
		order_PriceOffer.PurBranchID = num2;
		order_PriceOffer.SupplierID = SupplierID;
		order_PriceOffer.SupplierName = SupplierName;
		order_PriceOffer.SupplierPhone = SupplierPhone;
		order_PriceOffer.SupplierVatNum = SupplierVatNum;
		order_PriceOffer.Purchases_Date = Purchases_Date;
		order_PriceOffer.CostCentersID = CostCenter;
		order_PriceOffer.Note = Note;
		order_PriceOffer.NoteBig = NoteBig;
		order_PriceOffer.NoteNum = NoteNum;
		order_PriceOffer.TotalPrices = TotalPrices;
		order_PriceOffer.Tax = Tax;
		order_PriceOffer.Safy = Safy;
		order_PriceOffer.DiscountNum = DiscountNum;
		order_PriceOffer.DiscountPerantage = DiscountPerantage;
		order_PriceOffer.Tax_Discount = Tax_Discount;
		order_PriceOffer.AllDiscount = AllDiscount;
		order_PriceOffer.Net = Net;
		order_PriceOffer.TobaccoTax = TobaccoTax;
		order_PriceOffer.AllTax = AllTax;
		order_PriceOffer.Street = Street;
		order_PriceOffer.District = District;
		order_PriceOffer.City = City;
		order_PriceOffer.Country = Country;
		order_PriceOffer.PostalCode = PostalCode;
		order_PriceOffer.AdditionalNum = AdditionalNum;
		order_PriceOffer.CommercialRecord = CommercialRecord;
		order_PriceOffer.Sectoral = Sectoral;
		order_PriceOffer.Charge = Charge;
		order_PriceOffer.OrderType = OrderType;
		order_PriceOffer.ProjectId = ProjectId;
		order_PriceOffer.UserID_Add = LoginDetails.UserID;
		order_PriceOffer.UserBranch_Add = LoginDetails.BranchID;
		order_PriceOffer.UserMacAddress_Add = LoginDetails.macAddress;
		order_PriceOffer.UserDate_Add = DateTime.Now;
		db.Order_PriceOffer.Add(order_PriceOffer);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID)
	{
		List<Order_PriceOfferDetails> list = db.Order_PriceOfferDetails.Where((Order_PriceOfferDetails x) => x.Purchese_ID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Order_PriceOfferDetails item in list)
		{
			db.Order_PriceOfferDetails.Attach(item);
			db.Order_PriceOfferDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, int SupplierID, string SupplierName, string SupplierPhone, string SupplierVatNum, DateTime Purchases_Date, int CostCenter, string NoteBig, string Note, string NoteNum, decimal TotalPrices, decimal Tax, decimal Safy, decimal DiscountNum, decimal DiscountPerantage, decimal Tax_Discount, decimal AllDiscount, decimal Net, decimal TobaccoTax, decimal AllTax, string Street, string District, string City, string Country, string PostalCode, string AdditionalNum, string CommercialRecord, int Sectoral, decimal Charge, int OrderType, int projectId)
	{
		int result = 0;
		Order_PriceOffer order_PriceOffer = db.Order_PriceOffer.Where((Order_PriceOffer x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int iD = db.Order_PriceOffer.Where((Order_PriceOffer x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		if (order_PriceOffer != null)
		{
			order_PriceOffer.SupplierID = SupplierID;
			order_PriceOffer.SupplierName = SupplierName;
			order_PriceOffer.SupplierPhone = SupplierPhone;
			order_PriceOffer.SupplierVatNum = SupplierVatNum;
			order_PriceOffer.Purchases_Date = Purchases_Date;
			order_PriceOffer.CostCentersID = CostCenter;
			order_PriceOffer.Note = Note;
			order_PriceOffer.NoteBig = NoteBig;
			order_PriceOffer.NoteNum = NoteNum;
			order_PriceOffer.TotalPrices = TotalPrices;
			order_PriceOffer.Tax = Tax;
			order_PriceOffer.Safy = Safy;
			order_PriceOffer.DiscountNum = DiscountNum;
			order_PriceOffer.DiscountPerantage = DiscountPerantage;
			order_PriceOffer.Tax_Discount = Tax_Discount;
			order_PriceOffer.AllDiscount = AllDiscount;
			order_PriceOffer.Net = Net;
			order_PriceOffer.TobaccoTax = TobaccoTax;
			order_PriceOffer.AllTax = AllTax;
			order_PriceOffer.Street = Street;
			order_PriceOffer.District = District;
			order_PriceOffer.City = City;
			order_PriceOffer.Country = Country;
			order_PriceOffer.PostalCode = PostalCode;
			order_PriceOffer.AdditionalNum = AdditionalNum;
			order_PriceOffer.CommercialRecord = CommercialRecord;
			order_PriceOffer.Sectoral = Sectoral;
			order_PriceOffer.Charge = Charge;
			order_PriceOffer.OrderType = OrderType;
			order_PriceOffer.ProjectId = projectId;
			order_PriceOffer.UserID_Add = LoginDetails.UserID;
			order_PriceOffer.UserBranch_Add = LoginDetails.BranchID;
			order_PriceOffer.UserMacAddress_Add = LoginDetails.macAddress;
			order_PriceOffer.UserDate_Add = DateTime.Now;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public int Delete(int PurID)
	{
		int result = 0;
		Order_PriceOffer order_PriceOffer = db.Order_PriceOffer.Where((Order_PriceOffer x) => x.PurBranchID == (int?)PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (order_PriceOffer != null)
		{
			deleteItemsByPurcahesId(PurID);
			db.Order_PriceOffer.Attach(order_PriceOffer);
			db.Order_PriceOffer.Remove(order_PriceOffer);
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

	public int InsertRes(int PriceID, int HallsID, DateTime BookingDate, DateTime InstallationDate, DateTime jawDate)
	{
		int num = 0;
		int num2 = Convert.ToInt32(db.PriceOffer_Reservations.Select((PriceOffer_Reservations p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		PriceOffer_Reservations priceOffer_Reservations = new PriceOffer_Reservations();
		priceOffer_Reservations.ID = num2;
		priceOffer_Reservations.PriceOfferID = PriceID;
		priceOffer_Reservations.HallsID = HallsID;
		priceOffer_Reservations.BookingDate = BookingDate;
		priceOffer_Reservations.InstallationDate = InstallationDate;
		priceOffer_Reservations.jawDate = jawDate;
		priceOffer_Reservations.BranchID = LoginDetails.BranchID;
		priceOffer_Reservations.UserID_Add = LoginDetails.UserID;
		priceOffer_Reservations.UserBranch_Add = LoginDetails.BranchID;
		priceOffer_Reservations.UserMacAddress_Add = LoginDetails.macAddress;
		priceOffer_Reservations.UserDate_Add = DateTime.Now;
		db.PriceOffer_Reservations.Add(priceOffer_Reservations);
		num = db.SaveChanges();
		return num2;
	}

	public int UpdateRes(int PriceID, int HallsID, DateTime BookingDate, DateTime InstallationDate, DateTime jawDate)
	{
		int result = 0;
		PriceOffer_Reservations priceOffer_Reservations = db.PriceOffer_Reservations.Where((PriceOffer_Reservations x) => x.PriceOfferID == (int?)PriceID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (priceOffer_Reservations != null)
		{
			priceOffer_Reservations.HallsID = HallsID;
			priceOffer_Reservations.BookingDate = BookingDate;
			priceOffer_Reservations.InstallationDate = InstallationDate;
			priceOffer_Reservations.jawDate = jawDate;
			priceOffer_Reservations.UserID_Add = LoginDetails.UserID;
			priceOffer_Reservations.UserBranch_Add = LoginDetails.BranchID;
			priceOffer_Reservations.UserMacAddress_Add = LoginDetails.macAddress;
			priceOffer_Reservations.UserDate_Add = DateTime.Now;
			db.SaveChanges();
			result = PriceID;
		}
		return result;
	}

	public int DeleteRes(int ID)
	{
		int result = 0;
		PriceOffer_Reservations priceOffer_Reservations = db.PriceOffer_Reservations.Where((PriceOffer_Reservations x) => x.PriceOfferID == (int?)ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (priceOffer_Reservations != null)
		{
			db.PriceOffer_Reservations.Attach(priceOffer_Reservations);
			db.PriceOffer_Reservations.Remove(priceOffer_Reservations);
			result = db.SaveChanges();
		}
		return result;
	}
}


