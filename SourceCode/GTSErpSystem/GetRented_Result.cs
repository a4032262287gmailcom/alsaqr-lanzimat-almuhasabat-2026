using System;

namespace GTSErpSystem;

public class GetRented_Result
{
	public string Item_Name { get; set; }

	public decimal? OpeningBalance { get; set; }

	public decimal? CurrentBalance { get; set; }

	public decimal? RentedQuantity { get; set; }

	public string SupplierName { get; set; }

	public string ParentName { get; set; }

	public DateTime? RentalDate { get; set; }

	public DateTime? ReturnDate { get; set; }

	public decimal? DayNums { get; set; }

	public decimal? Quantity { get; set; }

	public int? RestDays { get; set; }
}


