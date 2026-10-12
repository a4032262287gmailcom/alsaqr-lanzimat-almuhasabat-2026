using System;

namespace GTSErpSystem;

public class OrderDalala_SendingDetails
{
	public int SN { get; set; }

	public int? Purchese_ID { get; set; }

	public int? ItemID { get; set; }

	public int? BranchID { get; set; }

	public int? StoreID { get; set; }

	public int? ItemUnitID { get; set; }

	public decimal? Quantity { get; set; }

	public string ItemUnitType { get; set; }

	public string NoteItem { get; set; }

	public DateTime? DateExpire { get; set; }
}


