using System;

namespace GTSErpSystem;

public class Print_Scaffolds_MinutesEnd_Result
{
	public int ID { get; set; }

	public int? PurBranchID { get; set; }

	public int? BranchID { get; set; }

	public int? StartedID { get; set; }

	public int? ContractID { get; set; }

	public int? SupplierID { get; set; }

	public string SupplierName { get; set; }

	public string SupplierPhone { get; set; }

	public string SupplierVatNum { get; set; }

	public DateTime? Purchases_Date { get; set; }

	public DateTime? To_Date { get; set; }

	public string HeaderNote { get; set; }

	public decimal? DayNum { get; set; }

	public string NoteNum { get; set; }

	public string Location { get; set; }

	public decimal? CostOrder { get; set; }

	public decimal? Tax { get; set; }

	public decimal? TotalPrices { get; set; }

	public decimal? Safy { get; set; }

	public decimal? DiscountNum { get; set; }

	public decimal? DiscountPerantage { get; set; }

	public decimal? Tax_Discount { get; set; }

	public decimal? TotalPrices_Discount { get; set; }

	public decimal? AllTax { get; set; }

	public decimal? Net { get; set; }

	public int? BounceID { get; set; }

	public int SN { get; set; }

	public int? ItemID { get; set; }

	public string item_Name { get; set; }

	public int? ItemUnitID { get; set; }

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

	public DateTime? ItemDate { get; set; }

	public decimal? LastPrice { get; set; }
}


