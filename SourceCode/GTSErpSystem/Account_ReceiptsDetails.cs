namespace GTSErpSystem;

public class Account_ReceiptsDetails
{
	public int Sn { get; set; }

	public int? TranSn { get; set; }

	public int? Account_Sn { get; set; }

	public int? AccounIindex { get; set; }

	public string TranDesc { get; set; }

	public decimal? Debit { get; set; }

	public decimal? Credit { get; set; }

	public int? CostCentersID { get; set; }

	public int? BranchID { get; set; }

	public int? ProjectId { get; set; }

	public int? YearId { get; set; }

	public bool? IsVat { get; set; }

	public decimal? Vat { get; set; }

	public decimal? Total { get; set; }

	public int? VatNum { get; set; }

	public virtual Account_Receipts Account_Receipts { get; set; }
}


