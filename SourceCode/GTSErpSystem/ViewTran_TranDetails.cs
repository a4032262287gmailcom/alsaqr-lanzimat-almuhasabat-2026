using System;

namespace GTSErpSystem;

public class ViewTran_TranDetails
{
	public int Sn { get; set; }

	public int? TranSn { get; set; }

	public int? Account_Sn { get; set; }

	public string TranDesc { get; set; }

	public decimal? Debit { get; set; }

	public decimal? Credit { get; set; }

	public string Account_No { get; set; }

	public string Account_Name { get; set; }

	public int? AccounIindex { get; set; }

	public int? CostCentersID { get; set; }

	public int? CostCentersNo { get; set; }

	public string CostCentersName { get; set; }

	public Guid? LedgerID { get; set; }

	public int ID { get; set; }

	public string Name { get; set; }

	public int? BranchID { get; set; }
}


