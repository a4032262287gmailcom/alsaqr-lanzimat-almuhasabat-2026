namespace GTSErpSystem;

public class Select_OrderGard_Result
{
	public int SN { get; set; }

	public int? GardID { get; set; }

	public int? ItemID { get; set; }

	public string Item_code { get; set; }

	public string item_Name { get; set; }

	public int? UnitSmall { get; set; }

	public int? StoreID { get; set; }

	public string StoreName { get; set; }

	public int? ItemUnitID { get; set; }

	public decimal? SellPrice { get; set; }

	public decimal? LastPrice { get; set; }

	public decimal? AvergPrice { get; set; }

	public decimal? QuantityApp { get; set; }

	public decimal? QuantityGard { get; set; }

	public decimal? QuantitiesDifference { get; set; }
}


