using System;

namespace GTSErpSystem;

public class GetRecpitInvoiceDetails_Result
{
	public int ID { get; set; }

	public int? PurBranchID { get; set; }

	public int? FormRentID { get; set; }

	public int? BranchID { get; set; }

	public int? SupplierID { get; set; }

	public string SupplierName { get; set; }

	public string SupplierPhone { get; set; }

	public string SupplierIDNum { get; set; }

	public string ParentName { get; set; }

	public string Address { get; set; }

	public string Note { get; set; }

	public int? OrderPaymantType { get; set; }

	public DateTime? RentalDate { get; set; }

	public DateTime? ReturnDate { get; set; }

	public DateTime? Purchases_Date { get; set; }

	public decimal? DaysNum { get; set; }

	public int? DaysAcc { get; set; }

	public int? CostCentersID { get; set; }

	public decimal? Tax { get; set; }

	public decimal? TotalPrices { get; set; }

	public decimal? DiscountNum { get; set; }

	public decimal? DiscountPerantage { get; set; }

	public decimal? AllDiscount { get; set; }

	public decimal? Tax_Discount { get; set; }

	public decimal? TotalPrices_Discount { get; set; }

	public decimal? Net { get; set; }

	public decimal? deposit { get; set; }

	public decimal? Fine { get; set; }

	public decimal? Recipient { get; set; }

	public decimal? depositRest { get; set; }

	public decimal? CashMoney { get; set; }

	public decimal? CashBank { get; set; }

	public decimal? Paid { get; set; }

	public decimal? rest { get; set; }

	public int? ProjectId { get; set; }

	public int? YearId { get; set; }

	public int? UserID_Add { get; set; }

	public int? UserBranch_Add { get; set; }

	public string UserMacAddress_Add { get; set; }

	public DateTime? UserDate_Add { get; set; }

	public int? UserID_Update { get; set; }

	public int? UserBranch_Update { get; set; }

	public string UserMacAddress_Update { get; set; }

	public DateTime? UserDate_Update { get; set; }

	public int? Purchese_ID { get; set; }

	public int? ItemID { get; set; }

	public int? DetailsBranchID { get; set; }

	public int? StoreID { get; set; }

	public int? ItemUnitID { get; set; }

	public decimal? DayNum { get; set; }

	public decimal? Quantity { get; set; }

	public decimal? QuantityDelivered { get; set; }

	public decimal? LastCost { get; set; }

	public decimal? SmallUnitPrice { get; set; }

	public decimal? UnitPrice { get; set; }

	public decimal? SumPrice { get; set; }

	public decimal? TotalPrice { get; set; }

	public decimal? AmountDisc { get; set; }

	public decimal? PercentDisc { get; set; }

	public decimal? VAT { get; set; }

	public decimal? NetUnitPrice { get; set; }

	public decimal? NetTotalPrice { get; set; }

	public decimal? VAT_Discount { get; set; }

	public string ItemUnitType { get; set; }

	public string ItemNote { get; set; }

	public string item_Name { get; set; }
}


