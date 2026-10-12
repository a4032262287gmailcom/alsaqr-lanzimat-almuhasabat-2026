namespace GTSErpSystem;

public class GetReport_TransActionItems_Result
{
	public string Item_code { get; set; }

	public string item_Name { get; set; }

	public string Column1 { get; set; }

	public string SmallBarCode2 { get; set; }

	public string SmallBarCode3 { get; set; }

	public decimal? LastCost { get; set; }

	public decimal? Average_cost { get; set; }

	public decimal? SellPriceSmall { get; set; }

	public decimal? SellPriceSmall2 { get; set; }

	public decimal? SellPriceSmall3 { get; set; }

	public string LocationSmall { get; set; }

	public string UnitSmall { get; set; }

	public string Item_Groups { get; set; }

	public decimal? Quantity_Orders { get; set; }

	public decimal? Quantity_OrdersRtn { get; set; }

	public decimal? Quantity_Purshes { get; set; }

	public decimal? Quantity_PurshesRtn { get; set; }

	public decimal? Quantity_Open { get; set; }

	public decimal? Quantity_Minus { get; set; }

	public decimal? Quantity_TrsToBranch { get; set; }

	public decimal? Quantity_TrsFromBranch { get; set; }

	public decimal? Quantity_PayItm { get; set; }

	public decimal Quantity_All { get; set; }
}


