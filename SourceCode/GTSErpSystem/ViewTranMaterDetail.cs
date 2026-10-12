using System;

namespace GTSErpSystem;

public class ViewTranMaterDetail
{
	public int ID { get; set; }

	public int? TranTypeID { get; set; }

	public decimal? TranCode { get; set; }

	public DateTime? TranDate { get; set; }

	public string Name { get; set; }

	public string Note { get; set; }

	public int? Account_Sn { get; set; }

	public string TranDesc { get; set; }

	public decimal? Debit { get; set; }

	public decimal? Credit { get; set; }

	public decimal? SumValues { get; set; }

	public string Account_No { get; set; }

	public string Account_Name { get; set; }

	public int? AccounIindex { get; set; }

	public string ReceivedFrom { get; set; }

	public int? ReferenceCode { get; set; }

	public decimal? TransBalance { get; set; }

	public decimal? Priv_Debit { get; set; }

	public decimal? Priv_Credit { get; set; }

	public int Sn { get; set; }

	public int? CostCentersID { get; set; }

	public string DocCode { get; set; }

	public Guid? LedgerID { get; set; }

	public int? BranchID { get; set; }

	public int? CostCentersNo { get; set; }

	public string CostCentersName { get; set; }

	public int? MainCostCentersID { get; set; }
}


