namespace GTSErpSystem;

public class GetReport_Orders_Result
{
	public long? SN { get; set; }

	public int? PurBranchID { get; set; }

	public string Purchases_Date { get; set; }

	public string Order_Paymant_Type { get; set; }

	public string SupplierName { get; set; }

	public string SupplierVatNum { get; set; }

	public string Note { get; set; }

	public string NoteNum { get; set; }

	public string SupplierPhone { get; set; }

	public decimal? TotalPrices_Discount { get; set; }

	public decimal? Tax_Discount { get; set; }

	public decimal? Net { get; set; }

	public decimal? CashMoney { get; set; }

	public decimal? CashBank { get; set; }

	public decimal? TotalCash { get; set; }

	public decimal? RestNet { get; set; }

	public string BranchName { get; set; }

	public int? CustSuppCode { get; set; }

	public string UserName { get; set; }
}


