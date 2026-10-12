namespace GTSErpSystem;

public class Get_Items_Stock_Report_Result
{
	public long? SN { get; set; }

	public int ItemID { get; set; }

	public string Item_Name { get; set; }

	public decimal? LastCost { get; set; }

	public decimal? Average_cost { get; set; }

	public int? ItemUnitID { get; set; }

	public string UnitName { get; set; }

	public decimal? OpeningBalance { get; set; }

	public decimal? CurrentBalance { get; set; }

	public decimal? QuantityExit { get; set; }

	public decimal? QuantityEntry { get; set; }

	public decimal? TotalPrice_Entry { get; set; }

	public decimal? VAT_Discount_Entry { get; set; }

	public decimal? Net_Entry { get; set; }

	public decimal? TotalPrice_Exit { get; set; }

	public decimal? VAT_Discount_Exit { get; set; }

	public decimal? Net_Exit { get; set; }
}


