using System;

namespace GTSErpSystem;

public class Get_Cust_Guarantee_Result
{
	public string CustSuppName { get; set; }

	public string Name { get; set; }

	public int ItemId { get; set; }

	public string item_Name { get; set; }

	public string Item_code { get; set; }

	public int? UnitSmall { get; set; }

	public decimal? PriceExportunit1 { get; set; }

	public decimal? LastCost { get; set; }

	public bool? Is_Tax { get; set; }

	public decimal? Tax_Value { get; set; }

	public decimal? Quantity { get; set; }

	public DateTime? ItemDate { get; set; }

	public DateTime? ItemDateExpire { get; set; }

	public decimal? QuantityExpire { get; set; }
}


