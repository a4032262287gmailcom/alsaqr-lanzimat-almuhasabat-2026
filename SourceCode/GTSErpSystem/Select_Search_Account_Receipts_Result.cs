using System;

namespace GTSErpSystem;

public class Select_Search_Account_Receipts_Result
{
	public long? ID { get; set; }

	public int? ReferenceCode { get; set; }

	public string DocCode { get; set; }

	public DateTime? TranDate { get; set; }

	public DateTime? DateDue { get; set; }

	public string CheckID { get; set; }

	public bool? ISCheckDone { get; set; }

	public bool? CheckType { get; set; }

	public string ReceivedFrom { get; set; }

	public string Note { get; set; }

	public decimal? Money { get; set; }

	public decimal? Credit { get; set; }

	public decimal? Debit { get; set; }

	public string CashReceiptsTypeName { get; set; }

	public string Account_Name { get; set; }

	public string Account_Name2 { get; set; }

	public string BranchName { get; set; }
}


