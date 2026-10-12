using System;

namespace GTSErpSystem;

public class Print_RentalExpenses_Result
{
	public long? SN { get; set; }

	public DateTime? Date { get; set; }

	public decimal? TotalPrice { get; set; }

	public decimal? AllTax { get; set; }

	public decimal? NetTotalPrice { get; set; }

	public int? ExpensesId { get; set; }

	public int? ItemId { get; set; }

	public string ItemCode { get; set; }

	public string ItemName { get; set; }

	public string Note { get; set; }

	public decimal? Total { get; set; }

	public decimal? Vat { get; set; }

	public bool? IsVat { get; set; }

	public decimal? Net { get; set; }
}


