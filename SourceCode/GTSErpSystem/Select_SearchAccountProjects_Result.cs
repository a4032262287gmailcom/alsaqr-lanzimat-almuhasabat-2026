using System;

namespace GTSErpSystem;

public class Select_SearchAccountProjects_Result
{
	public long? SN { get; set; }

	public int? ProjectID { get; set; }

	public int? ProjectNo { get; set; }

	public string ProjectName { get; set; }

	public DateTime? DateStart { get; set; }

	public DateTime? DateEnd { get; set; }

	public decimal? ProjectPrice { get; set; }

	public decimal? ProjectVat { get; set; }

	public decimal? InsuranceAmount { get; set; }

	public decimal? InsurancePer { get; set; }

	public decimal? TotalAmount { get; set; }

	public string Note { get; set; }

	public int? BranchID { get; set; }
}


