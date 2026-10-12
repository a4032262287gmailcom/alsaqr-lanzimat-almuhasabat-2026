using System;

namespace GTSErpSystem;

public class InvoiceTax_Result
{
	public int InvoiceId { get; set; }

	public string Customer { get; set; }

	public string BranchID { get; set; }

	public DateTime? BillDate { get; set; }

	public string Currency { get; set; }

	public int ExchangeRate { get; set; }

	public int? ItemID { get; set; }

	public string ItemName { get; set; }

	public decimal? Quantity { get; set; }

	public decimal? SalePrice { get; set; }

	public decimal? SalePrice1 { get; set; }

	public int? CustomerId { get; set; }

	public decimal? DiscountAmount { get; set; }

	public decimal? DiscountPercent { get; set; }

	public int InvoiceType { get; set; }

	public string TaxRegistration { get; set; }

	public int Tax { get; set; }
}


