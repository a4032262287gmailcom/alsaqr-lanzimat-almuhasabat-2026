namespace GTSErpSystem;

public class GetReport_OrderProfitBySalesMan_Result
{
	public int? PurBranchID { get; set; }

	public string OrderType { get; set; }

	public string SupplierName { get; set; }

	public string Sales { get; set; }

	public string Purchases_Date { get; set; }

	public decimal? TotalPrices_Discount { get; set; }

	public decimal? CostOrder { get; set; }

	public decimal? profit { get; set; }

	public decimal? ProfitRatio { get; set; }
}


