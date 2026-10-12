namespace GTSErpSystem;

public class GetReport_OrderRent_Result
{
	public long? SN { get; set; }

	public int? PurBranchID { get; set; }

	public string SupplierName { get; set; }

	public string Purchases_Date { get; set; }

	public string Item_code { get; set; }

	public string item_Name { get; set; }

	public string UnitsName { get; set; }

	public double? Quantity { get; set; }

	public double? NetUnitPrice { get; set; }

	public double? VAT_Discount { get; set; }

	public double? NetTotalPrice { get; set; }

	public decimal? TotalPrices_DiscountPurch { get; set; }

	public decimal? VATDiscountPurch { get; set; }

	public double? Net { get; set; }
}


