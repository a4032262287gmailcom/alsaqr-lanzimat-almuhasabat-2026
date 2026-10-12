using System;

namespace GTSErpSystem;

public class View_TranDetails
{
	public int ID { get; set; }

	public DateTime? TranDate { get; set; }

	public int? Account_Sn { get; set; }

	public decimal? Credit { get; set; }

	public decimal? Debit { get; set; }

	public int? Account_No { get; set; }

	public int? BranchID { get; set; }
}


