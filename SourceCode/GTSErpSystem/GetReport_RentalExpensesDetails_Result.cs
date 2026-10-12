namespace GTSErpSystem;

public class GetReport_RentalExpensesDetails_Result
{
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


