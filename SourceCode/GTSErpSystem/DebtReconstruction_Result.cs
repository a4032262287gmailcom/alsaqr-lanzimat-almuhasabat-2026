namespace GTSErpSystem;

public class DebtReconstruction_Result
{
	public int? SupplierID { get; set; }

	public string SupplierName { get; set; }

	public decimal? Days30 { get; set; }

	public decimal? Days60 { get; set; }

	public decimal? Days90 { get; set; }

	public decimal? Days120 { get; set; }

	public decimal? Over120 { get; set; }

	public decimal? Returns { get; set; }

	public decimal Receipts { get; set; }

	public decimal? TotalAmount { get; set; }
}


