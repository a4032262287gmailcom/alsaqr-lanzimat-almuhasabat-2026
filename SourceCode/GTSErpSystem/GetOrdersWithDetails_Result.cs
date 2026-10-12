using System;

namespace GTSErpSystem;

public class GetOrdersWithDetails_Result
{
	public int ID { get; set; }

	public int? PurBranchID { get; set; }

	public int? BranchID { get; set; }

	public int? CreditNote { get; set; }

	public int? SupplierID { get; set; }

	public bool? IsWaiting { get; set; }

	public string SupplierName { get; set; }

	public string SupplierPhone { get; set; }

	public string SupplierVatNum { get; set; }

	public DateTime? Purchases_Date { get; set; }

	public int? Order_Paymant_Type { get; set; }

	public string OrderType { get; set; }

	public string Restaurant_PayType { get; set; }

	public int? Restaurant_TypeID { get; set; }

	public int? CostCentersID { get; set; }

	public string Note { get; set; }

	public string NoteNum { get; set; }

	public decimal? CostOrder { get; set; }

	public decimal? Tax { get; set; }

	public decimal? TotalPrices { get; set; }

	public decimal? Safy { get; set; }

	public decimal? DiscountNum { get; set; }

	public decimal? DiscountPerantage { get; set; }

	public decimal? AllDiscount { get; set; }

	public decimal? Tax_Discount { get; set; }

	public decimal? TotalPrices_Discount { get; set; }

	public decimal? TobaccoTax { get; set; }

	public decimal? AllTax { get; set; }

	public decimal? Net { get; set; }

	public decimal? CashMoney { get; set; }

	public decimal? CashBank { get; set; }

	public int? CashAcc { get; set; }

	public int? BankAcc { get; set; }

	public decimal? Motbqy { get; set; }

	public decimal? CashRecipt { get; set; }

	public decimal? BankRecipt { get; set; }

	public int? CashReciptAcc { get; set; }

	public int? BankReciptAcc { get; set; }

	public DateTime? DateRecipt { get; set; }

	public bool? OrderCashierType { get; set; }

	public int? RoomNum { get; set; }

	public int? TableNum { get; set; }

	public int? UserID_Add { get; set; }

	public int? UserBranch_Add { get; set; }

	public string UserMacAddress_Add { get; set; }

	public DateTime? UserDate_Add { get; set; }

	public int? UserID_Update { get; set; }

	public int? UserBranch_Update { get; set; }

	public string UserMacAddress_Update { get; set; }

	public DateTime? UserDate_Update { get; set; }

	public int? RestBraID { get; set; }

	public bool? RestBraISClosed { get; set; }

	public int? BounceID { get; set; }

	public int? Res_Delivery_ID { get; set; }

	public string BuildingNum { get; set; }

	public string Street { get; set; }

	public string District { get; set; }

	public string City { get; set; }

	public string Country { get; set; }

	public string PostalCode { get; set; }

	public string AdditionalNum { get; set; }

	public string CommercialRecord { get; set; }

	public int? ProjectId { get; set; }

	public int? YearId { get; set; }

	public decimal? HasmPer { get; set; }

	public decimal? HasmAmount { get; set; }

	public int? Sectoral { get; set; }

	public string SalesMan { get; set; }

	public DateTime? DateHold { get; set; }

	public decimal? Charge { get; set; }

	public decimal? BounsAmount { get; set; }

	public string DisCode { get; set; }

	public string QRCode { get; set; }

	public string OrderTypeElectronicInvoiceId { get; set; }

	public string NormalOrSimpleInvoice { get; set; }

	public bool? ThirdParty { get; set; }

	public bool? NominalInvoice { get; set; }

	public bool? ExportInvoice { get; set; }

	public bool? SummaryInvoice { get; set; }

	public bool? SelfInvoice { get; set; }

	public string CarName { get; set; }

	public string CarModel { get; set; }

	public string PlateNumber { get; set; }

	public string ChassisNum { get; set; }

	public string CarColor { get; set; }

	public string Counter { get; set; }

	public int SN { get; set; }

	public int? Purchese_ID { get; set; }

	public int? R_ItmSN { get; set; }

	public int? R_RowId { get; set; }

	public int? R_ADDId { get; set; }

	public int? ItemID { get; set; }

	public int? ItemIDADD { get; set; }

	public int? DetailBranchID { get; set; }

	public bool? DetailIsWaiting { get; set; }

	public int? StoreID { get; set; }

	public string Store_Name { get; set; }

	public int? ItemUnitID { get; set; }

	public string UnitName { get; set; }

	public decimal? Quantity { get; set; }

	public decimal? LastCost { get; set; }

	public decimal? SmallUnitPrice { get; set; }

	public decimal? UnitPrice { get; set; }

	public decimal? TotalPrice { get; set; }

	public decimal? VAT { get; set; }

	public decimal? NetUnitPrice { get; set; }

	public decimal? NetTotalPrice { get; set; }

	public decimal? VAT_Discount { get; set; }

	public string ItemUnitType { get; set; }

	public bool? IsPrint { get; set; }

	public string ItemNote { get; set; }

	public bool? IsPrintCook { get; set; }

	public decimal? UnitNumber { get; set; }

	public decimal? Weight { get; set; }

	public decimal? Height { get; set; }

	public string DetailsData { get; set; }

	public decimal? width { get; set; }

	public decimal? Long { get; set; }

	public decimal? Bounce { get; set; }

	public decimal? Amount_Discount { get; set; }

	public decimal? Per_Discount { get; set; }

	public int? TafqitUnitId { get; set; }

	public decimal? TafqitQunitity { get; set; }

	public decimal? TobaccoTax1 { get; set; }

	public decimal? TobaccoTaxDis { get; set; }

	public decimal? TotalWithTaxTobacco { get; set; }

	public decimal? PriceInstall { get; set; }

	public decimal? PriceInstallDise { get; set; }

	public string Item_code { get; set; }

	public string item_Name { get; set; }

	public string item_Name_English { get; set; }

	public string ItemNote1 { get; set; }

	public int? item_Type { get; set; }

	public int? Category_ID { get; set; }

	public int? Class_ID { get; set; }

	public int? CompanyID { get; set; }

	public int? GuaranteeID { get; set; }

	public int? CountryID { get; set; }

	public int? UnitSmall { get; set; }

	public int? UnitMedium { get; set; }

	public int? UnitLarge { get; set; }

	public string SmallBarCode1 { get; set; }

	public string SmallBarCode2 { get; set; }

	public string SmallBarCode3 { get; set; }

	public string LocationSmall { get; set; }

	public decimal? SellPriceSmall { get; set; }

	public decimal? SellPriceSmall2 { get; set; }

	public decimal? SellPriceSmall3 { get; set; }

	public decimal? ItemLastCost { get; set; }

	public decimal? Average_cost { get; set; }

	public decimal? PriceExportunit1 { get; set; }

	public decimal? PriceMowazaunit1 { get; set; }

	public decimal? SmallUnitQuantity { get; set; }

	public string MediumBarCode1 { get; set; }

	public string MediumBarCode2 { get; set; }

	public string MediumBarCode3 { get; set; }

	public string LocationMedium { get; set; }

	public decimal? ConvertMediumUnit { get; set; }

	public decimal? SellPriceMedium { get; set; }

	public decimal? SellPriceMedium2 { get; set; }

	public decimal? SellPriceMedium3 { get; set; }

	public decimal? LastCost2 { get; set; }

	public decimal? PriceExportunit2 { get; set; }

	public decimal? PriceMowazaunit2 { get; set; }

	public decimal? SmallUnitQuantity2 { get; set; }

	public string BigBarCode1 { get; set; }

	public string BigBarCode2 { get; set; }

	public string BigBarCode3 { get; set; }

	public string LocationLarge { get; set; }

	public decimal? ConvertBigUnit { get; set; }

	public decimal? SellpriceLarge { get; set; }

	public decimal? SellpriceLarge2 { get; set; }

	public decimal? SellpriceLarge3 { get; set; }

	public decimal? LastCost3 { get; set; }

	public decimal? PriceExportunit3 { get; set; }

	public decimal? PriceMowazaunit3 { get; set; }

	public decimal? SmallUnitQuantity3 { get; set; }

	public bool? IsOffer { get; set; }

	public decimal? NumOffer { get; set; }

	public decimal? PriceOffer { get; set; }

	public bool? Is_Tax { get; set; }

	public decimal? Tax_Value { get; set; }

	public bool? Is_Item_mizan { get; set; }

	public string Image { get; set; }

	public int? ItemUserID_Add { get; set; }

	public int? ItemUserBranch_Add { get; set; }

	public string ItemUserMacAddress_Add { get; set; }

	public DateTime? ItemUserDate_Add { get; set; }

	public int? ItemUserID_Update { get; set; }

	public int? ItemUserBranch_Update { get; set; }

	public string ItemUserMacAddress_Update { get; set; }

	public DateTime? ItemUserDate_Update { get; set; }

	public int? UnitTafqitID { get; set; }

	public int? PrinterID { get; set; }

	public decimal? DiscItemPer { get; set; }

	public decimal? PharmPer { get; set; }

	public decimal? DoctorPer { get; set; }

	public string Code_Company { get; set; }

	public string BarcodeCountry { get; set; }

	public decimal? Tax_Tobacco { get; set; }

	public string VatCode { get; set; }

	public string TaxExemptionReasonCode { get; set; }

	public string TaxExemptionReason { get; set; }
}


