using System;

namespace GTSErpSystem;

public class Contract_Store
{
	public int ID { get; set; }

	public int? CustID { get; set; }

	public int? ItemID { get; set; }

	public DateTime? ItemDate { get; set; }

	public decimal? Quantity { get; set; }

	public DateTime? ItemDateExpire { get; set; }

	public decimal? QuantityExpire { get; set; }
}


