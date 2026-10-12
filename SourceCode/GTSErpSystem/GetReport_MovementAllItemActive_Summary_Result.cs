namespace GTSErpSystem;

public class GetReport_MovementAllItemActive_Summary_Result
{
	public long? ID { get; set; }

	public int ItemID { get; set; }

	public string item_Name { get; set; }

	public decimal? TotalSales { get; set; }

	public decimal? TotalSalesReturn { get; set; }

	public decimal? TotalPurchases { get; set; }

	public decimal? TotalPurchasesReturn { get; set; }

	public decimal? TotalFromBranch { get; set; }

	public decimal? TotalToBranch { get; set; }

	public decimal? TotalDamage { get; set; }

	public decimal? TotalPayment { get; set; }

	public decimal? TotalPaymentStore { get; set; }

	public decimal? TotalRecStore { get; set; }

	public decimal? TotalGard { get; set; }
}


