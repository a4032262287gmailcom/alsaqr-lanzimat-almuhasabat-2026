namespace GTSErpSystem;

public class GetReport_OrderProfitByItem_Result
{
	public int? ItemID { get; set; }

	public string item_Name { get; set; }

	public double? Quantity { get; set; }

	public double? LastCost { get; set; }

	public double? NetTotalPrice { get; set; }

	public decimal? Profit { get; set; }
}


