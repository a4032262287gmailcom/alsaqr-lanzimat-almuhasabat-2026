using System;

namespace GTSErpSystem;

public class Get_ExitItems_ByContractID_Result
{
	public int ID { get; set; }

	public string NoteNum { get; set; }

	public DateTime? Purchases_Date { get; set; }

	public string SupplierName { get; set; }

	public string Driver { get; set; }

	public string Location { get; set; }

	public int? ItemID { get; set; }

	public string Item_Name { get; set; }

	public int? ItemUnitID { get; set; }

	public decimal? Quantity { get; set; }

	public decimal? UnitPrice { get; set; }

	public decimal? TotalPrice { get; set; }

	public decimal? Net { get; set; }

	public int? StoreId { get; set; }

	public DateTime? ItemDate { get; set; }
}


