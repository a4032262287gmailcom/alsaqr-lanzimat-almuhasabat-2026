namespace GTSErpSystem;

public class api_GetAllItemsByGategories_Result
{
	public int ItemId { get; set; }

	public string Item_code { get; set; }

	public string item_Name { get; set; }

	public string item_Name_English { get; set; }

	public int? UnitSmall { get; set; }

	public decimal? SellPriceSmall { get; set; }

	public decimal? LastCost { get; set; }

	public decimal? Average_cost { get; set; }

	public bool? Is_Tax { get; set; }

	public decimal? Tax_Value { get; set; }
}


