using System;

namespace GTSErpSystem;

public class Print_Account_Receipts_Result
{
	public long? ID { get; set; }

	public int? ReferenceCode { get; set; }

	public int? TranTypeID { get; set; }

	public string DocCode { get; set; }

	public DateTime? TranDate { get; set; }

	public string ReceivedFrom { get; set; }

	public string Note { get; set; }

	public double? Money { get; set; }

	public string TranDesc { get; set; }

	public double? Debit { get; set; }

	public double? Credit { get; set; }

	public string CashReceiptsTypeName { get; set; }

	public string Account_Name { get; set; }

	public int? Account_No { get; set; }
}


