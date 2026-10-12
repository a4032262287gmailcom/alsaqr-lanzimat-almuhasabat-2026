using System;
using System.Collections.Generic;

namespace GTSErpSystem;

public class Account_Receipts
{
	public int ID { get; set; }

	public int? ReferenceCode { get; set; }

	public int? TranTypeID { get; set; }

	public int? CashReceiptsTypeID { get; set; }

	public string DocCode { get; set; }

	public DateTime? TranDate { get; set; }

	public string ReceivedFrom { get; set; }

	public string Note { get; set; }

	public decimal? Money { get; set; }

	public int? BranchID { get; set; }

	public int? UserID_Add { get; set; }

	public int? UserBranch_Add { get; set; }

	public string UserMacAddress_Add { get; set; }

	public DateTime? UserDate_Add { get; set; }

	public int? UserID_Update { get; set; }

	public int? UserBranch_Update { get; set; }

	public string UserMacAddress_Update { get; set; }

	public DateTime? UserDate_Update { get; set; }

	public decimal? TranCode { get; set; }

	public string CheckID { get; set; }

	public DateTime? DateDue { get; set; }

	public bool? CheckType { get; set; }

	public bool? ISCheckDone { get; set; }

	public int? ProjectId { get; set; }

	public int? YearId { get; set; }

	public virtual ICollection<Account_ReceiptsDetails> Account_ReceiptsDetails { get; set; }

	public Account_Receipts()
	{
		Account_ReceiptsDetails = new HashSet<Account_ReceiptsDetails>();
	}
}


