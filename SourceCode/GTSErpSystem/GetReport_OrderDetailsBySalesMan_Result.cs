namespace GTSErpSystem;

public class GetReport_OrderDetailsBySalesMan_Result
{
	public long? SN { get; set; }

	public int? PurBranchID { get; set; }

	public string SupplierName { get; set; }

	public string Sales { get; set; }

	public string Purchases_Date { get; set; }

	public string BranchName { get; set; }

	public string Order_Paymant_Type { get; set; }

	public string Item_code { get; set; }

	public string item_Name { get; set; }

	public string UnitsName { get; set; }

	public string Store_Name { get; set; }

	public decimal? Quantity { get; set; }

	public decimal? NetUnitPrice { get; set; }

	public decimal? VAT_Discount { get; set; }

	public decimal? NetTotalPrice { get; set; }

	public decimal? TotalPrices_DiscountPurch { get; set; }

	public decimal? VATDiscountPurch { get; set; }

	public decimal? Net { get; set; }
}


