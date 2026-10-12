using System;

namespace GTSErpSystem;

public class Select_OrderRent_Rent_Result
{
	public long? SN { get; set; }

	public int? ID { get; set; }

	public int? SupplierID { get; set; }

	public string SupplierName { get; set; }

	public string SupplierPhone { get; set; }

	public DateTime? Purchases_Date { get; set; }

	public string SupplierIDNum { get; set; }

	public string ParentName { get; set; }

	public string Address { get; set; }

	public decimal? TotalPrices_Discount { get; set; }

	public string Note { get; set; }

	public decimal? TotalPrices { get; set; }

	public decimal? DiscountNum { get; set; }

	public decimal? DiscountPerantage { get; set; }

	public decimal? Tax { get; set; }

	public decimal? Safy { get; set; }

	public decimal? Net { get; set; }

	public string TypeRent { get; set; }
}


