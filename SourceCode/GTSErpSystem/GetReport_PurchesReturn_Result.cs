namespace GTSErpSystem;

public class GetReport_PurchesReturn_Result
{
	public long? SN { get; set; }

	public int? PurBranchID { get; set; }

	public int? FromPurchesID { get; set; }

	public string Purchases_Date { get; set; }

	public string Order_Paymant_Type { get; set; }

	public string SupplierName { get; set; }

	public double? TotalPrices_Discount { get; set; }

	public double? Tax_Discount { get; set; }

	public double? Net { get; set; }

	public string BranchName { get; set; }

	public string UserName { get; set; }
}


