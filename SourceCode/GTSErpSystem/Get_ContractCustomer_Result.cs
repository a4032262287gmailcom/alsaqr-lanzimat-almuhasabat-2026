using System;

namespace GTSErpSystem;

public class Get_ContractCustomer_Result
{
	public string CustSuppName { get; set; }

	public string Phone { get; set; }

	public string UnitName { get; set; }

	public string item_Name { get; set; }

	public string Item_code { get; set; }

	public int? UnitSmall { get; set; }

	public decimal? Quantity { get; set; }

	public DateTime? ItemDate { get; set; }

	public DateTime? ItemDateExpire { get; set; }

	public decimal? QuantityExpire { get; set; }
}


