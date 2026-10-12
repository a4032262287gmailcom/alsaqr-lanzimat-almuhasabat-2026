namespace GTSErpSystem;

public class GetReport_Order_TransferfromBranch_Result
{
	public int ID { get; set; }

	public string Purchases_Date { get; set; }

	public int? BranchID { get; set; }

	public string BranchName { get; set; }

	public double? Net { get; set; }

	public double? CashMoney { get; set; }

	public double? CashBank { get; set; }

	public double? LastNet { get; set; }

	public int? ItemID { get; set; }

	public string ItemName { get; set; }

	public string ItemCode { get; set; }

	public double? Quantity { get; set; }

	public double? LastCost { get; set; }

	public double? TotalCost { get; set; }
}


