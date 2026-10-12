using System;

namespace GTSErpSystem;

public class Select_SearchAccountTran_Result
{
	public long? SN { get; set; }

	public int ID { get; set; }

	public int? ReferenceCode { get; set; }

	public string DocCode { get; set; }

	public DateTime? TranDate { get; set; }

	public string Note { get; set; }

	public string BranchID { get; set; }

	public decimal? Debit { get; set; }
}


