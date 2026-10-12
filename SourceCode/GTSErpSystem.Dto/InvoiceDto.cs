using System.Collections.Generic;

namespace GTSErpSystem.Dto;

public class InvoiceDto
{
	public string OrderId { get; set; }

	public string ProfileID { get; set; }

	public string ID { get; set; }

	public string UUID { get; set; }

	public string IssueDate { get; set; }

	public string IssueTime { get; set; }

	public int InvoiceType { get; set; }

	public string InvoiceTypeName { get; set; }

	public string InstructionNote { get; set; }

	public string Note { get; set; }

	public List<string> PaymentCode { get; set; }

	public string DocumentCurrencyCode { get; set; }

	public string TaxCurrencyCode { get; set; }

	public int LineCountNumeric { get; set; }

	public decimal CurrencyRate { get; set; }

	public decimal DiscountValue { get; set; }

	public decimal PayableAmount { get; set; }
}


