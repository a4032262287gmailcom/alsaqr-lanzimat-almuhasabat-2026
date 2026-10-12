namespace GTSErpSystem;

public class GetReport_OrderDetails_Result
{
	public string item_Name { get; set; }

	public string SupplierName { get; set; }

	public double? Quantity { get; set; }

	public decimal? VAT_Discount { get; set; }

	public decimal? NetTotalPrice { get; set; }

	public decimal? Net { get; set; }
}


