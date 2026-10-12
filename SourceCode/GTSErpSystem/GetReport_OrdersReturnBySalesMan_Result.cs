namespace GTSErpSystem;

public class GetReport_OrdersReturnBySalesMan_Result
{
	public long? SN { get; set; }

	public int? PurBranchID { get; set; }

	public int? FromPurchesID { get; set; }

	public string Purchases_Date { get; set; }

	public string Order_Paymant_Type { get; set; }

	public string SupplierName { get; set; }

	public string Sales { get; set; }

	public decimal? TotalPrices_Discount { get; set; }

	public decimal? Tax_Discount { get; set; }

	public decimal? Net { get; set; }

	public string BranchName { get; set; }

	public string UserName { get; set; }
}


