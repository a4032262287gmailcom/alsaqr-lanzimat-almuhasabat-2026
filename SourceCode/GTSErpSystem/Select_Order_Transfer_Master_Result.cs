using System;

namespace GTSErpSystem;

public class Select_Order_Transfer_Master_Result
{
	public int ID { get; set; }

	public int? BranchID { get; set; }

	public string FromBranchID { get; set; }

	public string ToBranchID { get; set; }

	public string StatusTypeID { get; set; }

	public string TrackingTypeID { get; set; }

	public DateTime? Purchases_Date { get; set; }

	public string NoteNum { get; set; }

	public decimal? TotalPrices_Discount { get; set; }

	public string PurchasesType { get; set; }

	public string Note { get; set; }

	public decimal? TotalPrices { get; set; }

	public decimal? DiscountNum { get; set; }

	public decimal? DiscountPerantage { get; set; }

	public decimal? Net { get; set; }
}


