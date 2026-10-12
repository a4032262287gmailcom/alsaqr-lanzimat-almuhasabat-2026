using System;

namespace GTSErpSystem;

public class GetCostCenterDetails_Result
{
	public int? TranSn { get; set; }

	public int? Account_Sn { get; set; }

	public string Account_Name { get; set; }

	public string TranDesc { get; set; }

	public decimal? Debit { get; set; }

	public decimal? Credit { get; set; }

	public int? CostCentersID { get; set; }

	public string CostName { get; set; }

	public DateTime? TranDate { get; set; }
}


