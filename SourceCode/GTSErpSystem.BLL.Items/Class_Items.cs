using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using GTSErpSystem.DataSetRep;
using Stimulsoft.Report;

namespace GTSErpSystem.BLL.Items;

public class Class_Items
{
	private GTSdbEntities db = new GTSdbEntities();

	private SqlConnection con = new SqlConnection(LoginDetails.ConnString);

	public int InsertNewItem(string Itemcode, string Itemname, string NameEn, int CatID, int ClassID, int CompanyID, int ItemType, int GuaranteeID, int CountryID, int Unit1, string SmallBarcode1, string SmallBarcode2, string SmallBarcode3, int unit2, decimal ConverTMedi, string MediumBarcode1, string MediumBarcode2, string MediumBarcode3, int unit3, decimal ConverGig, string BigBarcode1, string BigBarcode2, string BigBarcode3, decimal Price1unit1, decimal Price2unit1, decimal Price3unit1, decimal lastpriceunit1, decimal PriceExportunit1, decimal PriceMowazaunit1, int Requestunit1, string Location1, decimal Price1unit2, decimal Price2unit2, decimal Price3unit2, decimal lastpriceunit2, decimal PriceExportunit2, decimal PriceMowazaunit2, int Requestunit2, string Location2, decimal Price1unit3, decimal Price2unit3, decimal Price3unit3, decimal lastpriceunit3, decimal PriceExportunit3, decimal PriceMowazaunit3, int Requestunit3, string Location3, string image, bool IsVat, decimal Vat, string Notes, bool IsOffer, decimal NumOffer, decimal PriceOffer, decimal DiscItemPer, decimal PharmPer, decimal DoctorPer, bool CHISItemMizan, int UnitTafqitID, int PrinterID, decimal ItmQty, decimal VatTobacco, string VatCode, string ExemptionReason)
	{
		int result = 0;
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.Item_code == Itemcode).FirstOrDefault();
		Item_Items item_Items2 = db.Item_Items.Where((Item_Items x) => x.item_Name == Itemname).FirstOrDefault();
		if (item_Items == null && item_Items2 == null)
		{
			int num = Convert.ToInt32(db.Item_Items.Select((Item_Items p) => p.ItemId).DefaultIfEmpty(0).Max()) + 1;
			Item_Items item_Items3 = new Item_Items();
			item_Items3.ItemId = num;
			item_Items3.Item_code = Itemcode;
			item_Items3.item_Name = Itemname;
			item_Items3.item_Name_English = NameEn;
			item_Items3.Category_ID = CatID;
			item_Items3.Class_ID = ClassID;
			item_Items3.CompanyID = CompanyID;
			item_Items3.item_Type = ItemType;
			item_Items3.GuaranteeID = GuaranteeID;
			item_Items3.CountryID = CountryID;
			item_Items3.UnitTafqitID = UnitTafqitID;
			item_Items3.UnitSmall = Unit1;
			item_Items3.SmallBarCode1 = SmallBarcode1;
			item_Items3.SmallBarCode2 = SmallBarcode2;
			item_Items3.SmallBarCode3 = SmallBarcode3;
			item_Items3.UnitMedium = unit2;
			item_Items3.ConvertMediumUnit = ConverTMedi;
			item_Items3.MediumBarCode1 = MediumBarcode1;
			item_Items3.MediumBarCode2 = MediumBarcode2;
			item_Items3.MediumBarCode3 = MediumBarcode3;
			item_Items3.UnitLarge = unit3;
			item_Items3.ConvertBigUnit = ConverGig;
			item_Items3.BigBarCode1 = BigBarcode1;
			item_Items3.BigBarCode2 = BigBarcode2;
			item_Items3.BigBarCode3 = BigBarcode3;
			item_Items3.SellPriceSmall = Price1unit1;
			item_Items3.SellPriceSmall2 = Price2unit1;
			item_Items3.SellPriceSmall3 = Price3unit1;
			item_Items3.PriceExportunit1 = PriceExportunit1;
			item_Items3.Average_cost = PriceMowazaunit1;
			item_Items3.LastCost = lastpriceunit1;
			item_Items3.SmallUnitQuantity = Requestunit1;
			item_Items3.LocationSmall = Location1;
			item_Items3.SellPriceMedium = Price1unit2;
			item_Items3.SellPriceMedium2 = Price2unit2;
			item_Items3.SellPriceMedium3 = Price3unit2;
			item_Items3.LastCost2 = lastpriceunit2;
			item_Items3.PriceExportunit2 = PriceExportunit2;
			item_Items3.PriceMowazaunit2 = PriceMowazaunit2;
			item_Items3.SmallUnitQuantity2 = Requestunit2;
			item_Items3.LocationMedium = Location2;
			item_Items3.SellpriceLarge = Price1unit3;
			item_Items3.SellpriceLarge2 = Price2unit3;
			item_Items3.SellpriceLarge3 = Price3unit3;
			item_Items3.LastCost3 = lastpriceunit3;
			item_Items3.PriceExportunit3 = PriceExportunit3;
			item_Items3.PriceMowazaunit3 = PriceMowazaunit3;
			item_Items3.SmallUnitQuantity3 = Requestunit3;
			item_Items3.LocationLarge = Location3;
			item_Items3.Image = image;
			item_Items3.PrinterID = PrinterID;
			item_Items3.Is_Tax = IsVat;
			item_Items3.Tax_Value = Vat;
			item_Items3.Note = Notes;
			item_Items3.IsOffer = IsOffer;
			item_Items3.NumOffer = NumOffer;
			item_Items3.PriceOffer = PriceOffer;
			item_Items3.DiscItemPer = DiscItemPer;
			item_Items3.PharmPer = PharmPer;
			item_Items3.DoctorPer = DoctorPer;
			item_Items3.UserID_Add = LoginDetails.UserID;
			item_Items3.UserBranch_Add = LoginDetails.BranchID;
			item_Items3.UserMacAddress_Add = LoginDetails.macAddress;
			item_Items3.UserDate_Add = DateTime.Now;
			item_Items3.Is_Item_mizan = CHISItemMizan;
			item_Items3.Tax_Tobacco = VatTobacco;
			item_Items3.VatCode = VatCode;
			item_Items3.TaxExemptionReason = ExemptionReason;
			item_Items3.TaxExemptionReasonCode = ExemptionReason;
			db.Item_Items.Add(item_Items3);
			result = db.SaveChanges();
			UpdateQty(num, ItmQty, PrinterID);
			result = num;
		}
		return result;
	}

	public int EditItem(int ItmId, string Itemcode, string Itemname, string NameEn, int CatID, int ClassID, int CompanyID, int ItemType, int GuaranteeID, int CountryID, int Unit1, string SmallBarcode1, string SmallBarcode2, string SmallBarcode3, int unit2, decimal ConverTMedi, string MediumBarcode1, string MediumBarcode2, string MediumBarcode3, int unit3, decimal ConverGig, string BigBarcode1, string BigBarcode2, string BigBarcode3, decimal Price1unit1, decimal Price2unit1, decimal Price3unit1, decimal lastpriceunit1, decimal PriceExportunit1, decimal PriceMowazaunit1, int Requestunit1, string Location1, decimal Price1unit2, decimal Price2unit2, decimal Price3unit2, decimal lastpriceunit2, decimal PriceExportunit2, decimal PriceMowazaunit2, int Requestunit2, string Location2, decimal Price1unit3, decimal Price2unit3, decimal Price3unit3, decimal lastpriceunit3, decimal PriceExportunit3, decimal PriceMowazaunit3, int Requestunit3, string Location3, string image, bool IsVat, decimal Vat, string Notes, bool IsOffer, decimal NumOffer, decimal PriceOffer, decimal DiscItemPer, decimal PharmPer, decimal DoctorPer, bool CHISItemMizan, int UnitTafqitID, int PrinterID, decimal ItmQty, decimal VatTobacco, string VatCode, string ExemptionReason)
	{
		int result = 0;
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.ItemId == ItmId).FirstOrDefault();
		if (item_Items != null)
		{
			item_Items.Item_code = Itemcode;
			item_Items.item_Name = Itemname;
			item_Items.item_Name_English = NameEn;
			item_Items.Category_ID = CatID;
			item_Items.Class_ID = ClassID;
			item_Items.CompanyID = CompanyID;
			item_Items.item_Type = ItemType;
			item_Items.GuaranteeID = GuaranteeID;
			item_Items.CountryID = CountryID;
			item_Items.UnitTafqitID = UnitTafqitID;
			item_Items.UnitSmall = Unit1;
			item_Items.SmallBarCode1 = SmallBarcode1;
			item_Items.SmallBarCode2 = SmallBarcode2;
			item_Items.SmallBarCode3 = SmallBarcode3;
			item_Items.UnitMedium = unit2;
			item_Items.ConvertMediumUnit = ConverTMedi;
			item_Items.MediumBarCode1 = MediumBarcode1;
			item_Items.MediumBarCode2 = MediumBarcode2;
			item_Items.MediumBarCode3 = MediumBarcode3;
			item_Items.UnitLarge = unit3;
			item_Items.ConvertBigUnit = ConverGig;
			item_Items.BigBarCode1 = BigBarcode1;
			item_Items.BigBarCode2 = BigBarcode2;
			item_Items.BigBarCode3 = BigBarcode3;
			item_Items.SellPriceSmall = Price1unit1;
			item_Items.SellPriceSmall2 = Price2unit1;
			item_Items.SellPriceSmall3 = Price3unit1;
			item_Items.LastCost = lastpriceunit1;
			item_Items.PriceExportunit1 = PriceExportunit1;
			item_Items.Average_cost = PriceMowazaunit1;
			item_Items.SmallUnitQuantity = Requestunit1;
			item_Items.LocationSmall = Location1;
			item_Items.SellPriceMedium = Price1unit2;
			item_Items.SellPriceMedium2 = Price2unit2;
			item_Items.SellPriceMedium3 = Price3unit2;
			item_Items.LastCost2 = lastpriceunit2;
			item_Items.PriceExportunit2 = PriceExportunit2;
			item_Items.PriceMowazaunit2 = PriceMowazaunit2;
			item_Items.SmallUnitQuantity2 = Requestunit2;
			item_Items.LocationMedium = Location2;
			item_Items.SellpriceLarge = Price1unit3;
			item_Items.SellpriceLarge2 = Price2unit3;
			item_Items.SellpriceLarge3 = Price3unit3;
			item_Items.LastCost3 = lastpriceunit3;
			item_Items.PriceExportunit3 = PriceExportunit3;
			item_Items.PriceMowazaunit3 = PriceMowazaunit3;
			item_Items.SmallUnitQuantity3 = Requestunit3;
			item_Items.LocationLarge = Location3;
			item_Items.Image = image;
			item_Items.PrinterID = PrinterID;
			item_Items.Is_Tax = IsVat;
			item_Items.Tax_Value = Vat;
			item_Items.Note = Notes;
			item_Items.IsOffer = IsOffer;
			item_Items.NumOffer = NumOffer;
			item_Items.PriceOffer = PriceOffer;
			item_Items.DiscItemPer = DiscItemPer;
			item_Items.PharmPer = PharmPer;
			item_Items.DoctorPer = DoctorPer;
			item_Items.Is_Item_mizan = CHISItemMizan;
			item_Items.UserID_Update = LoginDetails.UserID;
			item_Items.UserBranch_Update = LoginDetails.GroupID;
			item_Items.UserMacAddress_Update = LoginDetails.macAddress;
			item_Items.UserDate_Update = DateTime.Now;
			item_Items.Tax_Tobacco = VatTobacco;
			item_Items.VatCode = VatCode;
			item_Items.TaxExemptionReason = ExemptionReason;
			item_Items.TaxExemptionReasonCode = ExemptionReason;
			result = db.SaveChanges();
			UpdateQty(ItmId, ItmQty, PrinterID);
		}
		return result;
	}

	public void deleteItemsComplanyByItemId(string ItemCode)
	{
		int ItemId = (from x in db.Item_Items
			where x.Item_code == ItemCode
			select x.ItemId).First();
		List<Item_ItemComponent> list = db.Item_ItemComponent.Where((Item_ItemComponent x) => x.ItemID_Master == (int?)ItemId).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Item_ItemComponent item in list)
		{
			db.Item_ItemComponent.Attach(item);
			db.Item_ItemComponent.Remove(item);
		}
		db.SaveChanges();
	}

	public List<Item_ItemComponent> GetAllItemsComp(int ItemIdMaster)
	{
		return db.Item_ItemComponent.Where((Item_ItemComponent x) => x.ItemID_Master == (int?)ItemIdMaster).ToList();
	}

	public int DeleteItem(string ItemCode)
	{
		int result = 0;
		Item_Items CheckItem = db.Item_Items.Where((Item_Items x) => x.Item_code == ItemCode).FirstOrDefault();
		int ITMID = Convert.ToInt32(CheckItem.ItemId);
		Order_OrdersDetails order_OrdersDetails = db.Order_OrdersDetails.Where((Order_OrdersDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_OrderReturnDetails order_OrderReturnDetails = db.Order_OrderReturnDetails.Where((Order_OrderReturnDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_PurchasesDetails order_PurchasesDetails = db.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_PurchasesReturnDetails order_PurchasesReturnDetails = db.Order_PurchasesReturnDetails.Where((Order_PurchasesReturnDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_PriceOfferDetails order_PriceOfferDetails = db.Order_PriceOfferDetails.Where((Order_PriceOfferDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_PurchasesOrderDetails order_PurchasesOrderDetails = db.Order_PurchasesOrderDetails.Where((Order_PurchasesOrderDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_OpenQuantityDetails order_OpenQuantityDetails = db.Order_OpenQuantityDetails.Where((Order_OpenQuantityDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_StoreTransferDetails order_StoreTransferDetails = db.Order_StoreTransferDetails.Where((Order_StoreTransferDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_CheckoutOrdersDetails order_CheckoutOrdersDetails = db.Order_CheckoutOrdersDetails.Where((Order_CheckoutOrdersDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_InventorySettlementMinusDetails order_InventorySettlementMinusDetails = db.Order_InventorySettlementMinusDetails.Where((Order_InventorySettlementMinusDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_TransferToBranchDetails order_TransferToBranchDetails = db.Order_TransferToBranchDetails.Where((Order_TransferToBranchDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_TransferFromBranchDetails order_TransferFromBranchDetails = db.Order_TransferFromBranchDetails.Where((Order_TransferFromBranchDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Ordert_RentalInvoiceDetails ordert_RentalInvoiceDetails = db.Ordert_RentalInvoiceDetails.Where((Ordert_RentalInvoiceDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		OrderRent_RecpitDetails3 orderRent_RecpitDetails = db.OrderRent_RecpitDetails3.Where((OrderRent_RecpitDetails3 x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		if (order_OrdersDetails == null && order_OrderReturnDetails == null && order_PurchasesDetails == null && order_PurchasesReturnDetails == null && order_PriceOfferDetails == null && order_PurchasesOrderDetails == null && order_OpenQuantityDetails == null && order_StoreTransferDetails == null && order_CheckoutOrdersDetails == null && order_InventorySettlementMinusDetails == null && order_TransferToBranchDetails == null && order_TransferFromBranchDetails == null && ordert_RentalInvoiceDetails == null && orderRent_RecpitDetails == null)
		{
			List<Item_ItemComponent> list = db.Item_ItemComponent.Where((Item_ItemComponent x) => x.ItemID_Master == (int?)CheckItem.ItemId).ToList();
			if (list != null)
			{
				foreach (Item_ItemComponent item in list)
				{
					db.Item_ItemComponent.Attach(item);
					db.Item_ItemComponent.Remove(item);
					result = db.SaveChanges();
				}
			}
			if (CheckItem != null)
			{
				db.Item_Items.Attach(CheckItem);
				db.Item_Items.Remove(CheckItem);
				result = db.SaveChanges();
				List<ItemQuantity> list2 = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ITMID).ToList();
				foreach (ItemQuantity item2 in list2)
				{
					db.ItemQuantities.Attach(item2);
					db.ItemQuantities.Remove(item2);
					result = db.SaveChanges();
				}
			}
		}
		return result;
	}

	public int IsItemInDatabase(string ItemCode)
	{
		int result = 0;
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.Item_code == ItemCode).FirstOrDefault();
		int ITMID = Convert.ToInt32(item_Items.ItemId);
		Order_OrdersDetails order_OrdersDetails = db.Order_OrdersDetails.Where((Order_OrdersDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_OrderReturnDetails order_OrderReturnDetails = db.Order_OrderReturnDetails.Where((Order_OrderReturnDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_PurchasesDetails order_PurchasesDetails = db.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_PurchasesReturnDetails order_PurchasesReturnDetails = db.Order_PurchasesReturnDetails.Where((Order_PurchasesReturnDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_PriceOfferDetails order_PriceOfferDetails = db.Order_PriceOfferDetails.Where((Order_PriceOfferDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_PurchasesOrderDetails order_PurchasesOrderDetails = db.Order_PurchasesOrderDetails.Where((Order_PurchasesOrderDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_OpenQuantityDetails order_OpenQuantityDetails = db.Order_OpenQuantityDetails.Where((Order_OpenQuantityDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_StoreTransferDetails order_StoreTransferDetails = db.Order_StoreTransferDetails.Where((Order_StoreTransferDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_CheckoutOrdersDetails order_CheckoutOrdersDetails = db.Order_CheckoutOrdersDetails.Where((Order_CheckoutOrdersDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_InventorySettlementMinusDetails order_InventorySettlementMinusDetails = db.Order_InventorySettlementMinusDetails.Where((Order_InventorySettlementMinusDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_TransferToBranchDetails order_TransferToBranchDetails = db.Order_TransferToBranchDetails.Where((Order_TransferToBranchDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		Order_CheckoutOrdersDetails order_CheckoutOrdersDetails2 = db.Order_CheckoutOrdersDetails.Where((Order_CheckoutOrdersDetails x) => x.ItemID == (int?)ITMID).FirstOrDefault();
		if (order_OrdersDetails != null || order_OrderReturnDetails != null || order_PurchasesDetails != null || order_PurchasesReturnDetails != null || order_PriceOfferDetails != null || order_PurchasesOrderDetails != null || order_OpenQuantityDetails != null || order_StoreTransferDetails != null || order_CheckoutOrdersDetails != null || order_InventorySettlementMinusDetails != null || order_TransferToBranchDetails != null || order_CheckoutOrdersDetails2 != null)
		{
			result = 1;
		}
		return result;
	}

	public int ItemQuantityInOut(int ItemID, int Type, decimal Quantity)
	{
		int result = 0;
		ItemQuantity itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItemID).FirstOrDefault();
		if (itemQuantity == null)
		{
			ItemQuantity itemQuantity2 = new ItemQuantity();
			itemQuantity2.ItemID = ItemID;
			itemQuantity2.CurrentBalance = Quantity;
			db.ItemQuantities.Add(itemQuantity2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int GetItemID(string Code)
	{
		int result = 0;
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.Item_code == Code).FirstOrDefault();
		if (item_Items != null)
		{
			result = item_Items.ItemId;
		}
		return result;
	}

	public List<Item_Items> GetItemsBy(int CatID, int ClassID, string Itemcode)
	{
		List<Item_Items> list = new List<Item_Items>();
		IQueryable<Item_Items> source = db.Item_Items.Where((Item_Items m) => m.Item_code.Contains(Itemcode) || m.item_Name.Contains(Itemcode) || m.item_Name_English.Contains(Itemcode));
		list = source.ToList();
		if (CatID > 0)
		{
			list = source.Where((Item_Items x) => x.Category_ID == (int?)CatID).ToList();
		}
		if (ClassID > 0)
		{
			list = source.Where((Item_Items x) => x.Class_ID == (int?)ClassID).ToList();
		}
		return list.ToList();
	}

	public List<Get_All_Items_Result> GetItms()
	{
		return db.Database.SqlQuery<Get_All_Items_Result>("EXEC Get_All_Items", Array.Empty<object>()).ToList();
	}

	public List<Item_Items> GetItems(string Code)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Item_Items.Where((Item_Items x) => x.Item_code == Code).ToList();
	}

	public List<Item_Items> GetItemsByitemid(int ItemID)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).ToList();
	}

	public List<Item_Items> GetItemsByitemCode(string ItemID)
	{
		return db.Item_Items.Where((Item_Items x) => x.Item_code == ItemID).ToList();
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

	public List<Item_Items> GetItemsName(string Namae)
	{
		return db.Item_Items.Where((Item_Items x) => x.item_Name == Namae).ToList();
	}

	public List<Item_Items> GetItems()
	{
		return db.Item_Items.ToList();
	}

	public List<Item_Groups> GetCats()
	{
		return db.Item_Groups.ToList();
	}

	public List<Item_Guarantee> GetGuarantee()
	{
		return db.Item_Guarantee.ToList();
	}

	public List<Item_Country> GetCountry()
	{
		return db.Item_Country.ToList();
	}

	public List<Item_Unit> GetUnits()
	{
		return db.Item_Unit.ToList();
	}

	public string GetUnitNameById(int ID)
	{
		return (from u in db.Item_Unit
			where u.ID == ID
			select u.Name).FirstOrDefault();
	}

	public List<Item_Unit> GetUnitsItems(string ItemCode)
	{
		List<Item_Unit> list = new List<Item_Unit>();
		Item_Items ItemUnit = db.Item_Items.Where((Item_Items x) => x.Item_code == ItemCode).FirstOrDefault();
		if (Convert.ToInt32(ItemUnit.UnitSmall) > 0)
		{
			Item_Unit item = db.Item_Unit.Where((Item_Unit xx) => (int?)xx.ID == ItemUnit.UnitSmall).FirstOrDefault();
			list.Add(item);
		}
		if (Convert.ToInt32(ItemUnit.UnitMedium) > 0)
		{
			Item_Unit item2 = db.Item_Unit.Where((Item_Unit xx) => (int?)xx.ID == ItemUnit.UnitMedium).FirstOrDefault();
			list.Add(item2);
		}
		if (Convert.ToInt32(ItemUnit.UnitLarge) > 0)
		{
			Item_Unit item3 = db.Item_Unit.Where((Item_Unit xx) => (int?)xx.ID == ItemUnit.UnitLarge).FirstOrDefault();
			list.Add(item3);
		}
		return list;
	}

	public List<Item_Type> GetItemType()
	{
		return db.Item_Type.ToList();
	}

	public List<Item_Class> GetItemCalass()
	{
		return db.Item_Class.ToList();
	}

	public List<Item_Company> GetItemCompany()
	{
		return db.Item_Company.ToList();
	}

	public int CheckBarcode(string Barcode, int typUniy)
	{
		int result = 0;
		if (typUniy == 1)
		{
			Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.SmallBarCode1 == Barcode || x.SmallBarCode2 == Barcode || x.SmallBarCode3 == Barcode).FirstOrDefault();
			if (item_Items != null)
			{
				result = 1;
			}
		}
		if (typUniy == 2)
		{
			Item_Items item_Items2 = db.Item_Items.Where((Item_Items x) => x.MediumBarCode1 == Barcode || x.MediumBarCode2 == Barcode || x.MediumBarCode3 == Barcode).FirstOrDefault();
			if (item_Items2 != null)
			{
				result = 1;
			}
		}
		if (typUniy == 3)
		{
			Item_Items item_Items3 = db.Item_Items.Where((Item_Items x) => x.MediumBarCode1 == Barcode || x.MediumBarCode2 == Barcode || x.MediumBarCode3 == Barcode).FirstOrDefault();
			if (item_Items3 != null)
			{
				result = 1;
			}
		}
		return result;
	}

	public int CheckBarcode(string Barcode)
	{
		int result = 0;
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.SmallBarCode1 == Barcode || x.SmallBarCode2 == Barcode || x.SmallBarCode3 == Barcode || x.MediumBarCode1 == Barcode || x.MediumBarCode2 == Barcode || x.MediumBarCode3 == Barcode || x.BigBarCode1 == Barcode || x.BigBarCode2 == Barcode || x.BigBarCode3 == Barcode || x.Item_code == Barcode).FirstOrDefault();
		if (item_Items != null)
		{
			result = 1;
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
		decimal num4 = Convert.ToDecimal(db.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "الصغرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.Quantity));
		decimal num5 = Convert.ToDecimal(db.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "المتوسطة" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.Quantity));
		decimal num6 = Convert.ToDecimal(db.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "الكبرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.Quantity));
		decimal num7 = Convert.ToDecimal(db.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "الصغرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.NetTotalPrice));
		decimal num8 = Convert.ToDecimal(db.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "المتوسطة" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.NetTotalPrice));
		decimal num9 = Convert.ToDecimal(db.Order_PurchasesDetails.Where((Order_PurchasesDetails x) => x.ItemUnitType == "الكبرى" && x.ItemID == (int?)ItemDtails.ItemId).Sum((Order_PurchasesDetails x) => x.NetTotalPrice));
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

	public List<Get_Item_ByCodeALL_Result> GetItemByCode(string Code)
	{
		return db.Get_Item_ByCodeALL(Code, LoginDetails.BranchID).ToList();
	}

	public List<Get_Item_ByItemCode_Result> GetItemByCode2(string Code)
	{
		return db.Get_Item_ByItemCode(Code).ToList();
	}

	public List<Get_Item_ByID_Result> Get_Item_ByID(int ItemId)
	{
		return db.Get_Item_ByID(ItemId, LoginDetails.BranchID).ToList();
	}

	public List<Item_Items> GetItemByCode()
	{
		return db.Item_Items.ToList();
	}

	public string SearchByBarcode(string Barcode)
	{
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.SmallBarCode1 == Barcode || x.SmallBarCode2 == Barcode || x.SmallBarCode3 == Barcode || x.MediumBarCode1 == Barcode || x.MediumBarCode2 == Barcode || x.MediumBarCode3 == Barcode || x.BigBarCode1 == Barcode || x.BigBarCode2 == Barcode || x.BigBarCode3 == Barcode).FirstOrDefault();
		if (item_Items != null)
		{
			return item_Items.Item_code;
		}
		return "";
	}

	public string GetUnitName(int? UnitID)
	{
		string text = "";
		return db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == UnitID).FirstOrDefault().Name;
	}

	public List<Printer> GetAllPrinter()
	{
		return db.Printers.Where((Printer x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public void GetAllItems()
	{
		string nameServer = LoginDetails.nameServer;
		string databaseServer = LoginDetails.databaseServer;
		string userNameServer = LoginDetails.userNameServer;
		string passServer = LoginDetails.passServer;
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + nameServer + ";Initial Catalog=" + databaseServer + ";Persist Security Info=True;User ID=" + userNameServer + ";Password=" + passServer);
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.Tables["TableAllItems"].Clear();
		SqlCommand sqlCommand = new SqlCommand("GetReport_ItemsMenu", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlDataAdapter.Fill(dataSetALL.TableAllItems);
		sqlConnection.Close();
		if (dataSetALL.Tables["TableAllItems"].Rows.Count > 0)
		{
			StiReport stiReport = new StiReport();
			if (LoginDetails.Lang == "ar-EG")
			{
				stiReport.Load(Application.StartupPath + "\\Reports\\ReportAllItems.mrt");
			}
			else
			{
				stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportAllItems.mrt");
			}
			stiReport.RegData(dataSetALL.TableAllItems);
			stiReport.Dictionary.Synchronize();
			stiReport.Compile();
			stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
			stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
			stiReport["VatNumComp"] = LoginDetails.VatNum;
			stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
			stiReport["Address"] = LoginDetails.Address;
			stiReport["Phone"] = LoginDetails.Phone;
			stiReport["Bank"] = LoginDetails.CompanyBank;
			stiReport.Render();
			stiReport.Show(dialogForm: true);
		}
	}

	public int SaveItems(DataTable Items)
	{
		int result = 0;
		using (SqlCommand sqlCommand = new SqlCommand("Insert_Items", con))
		{
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
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
			object value = sqlParameter.Value;
			con.Close();
			result = Convert.ToInt32(value);
		}
		return result;
	}

	public decimal UpdateQty(int ItmID, decimal Qty, int StordID)
	{
		if (Qty != 0m)
		{
			ItemQuantity itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItmID && x.StoreID == (int?)StordID).FirstOrDefault();
			if (itemQuantity == null)
			{
				ItemQuantity itemQuantity2 = new ItemQuantity();
				int itemQuantityID = Convert.ToInt32(db.ItemQuantities.Select((ItemQuantity p) => p.ItemQuantityID).DefaultIfEmpty(0).Max()) + 1;
				itemQuantity2.ItemQuantityID = itemQuantityID;
				itemQuantity2.ItemID = ItmID;
				itemQuantity2.StoreID = StordID;
				itemQuantity2.OpeningBalance = Qty;
				itemQuantity2.CurrentBalance = Qty;
				itemQuantity2.BeginningInventory = 0m;
				itemQuantity2.BeginningInventoryPrice = 0m;
				itemQuantity2.UnitNumber = 0m;
				db.ItemQuantities.Add(itemQuantity2);
				db.SaveChanges();
			}
			else
			{
				decimal num = Convert.ToDecimal(itemQuantity.CurrentBalance);
				decimal num2 = Convert.ToDecimal(itemQuantity.OpeningBalance);
				itemQuantity.OpeningBalance = Qty;
				itemQuantity.CurrentBalance = num - num2 + Qty;
				db.SaveChanges();
			}
		}
		return Qty;
	}
}


