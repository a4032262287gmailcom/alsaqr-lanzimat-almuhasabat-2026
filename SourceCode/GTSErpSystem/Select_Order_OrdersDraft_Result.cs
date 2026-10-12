using System;

namespace GTSErpSystem;

public class Select_Order_OrdersDraft_Result
{
	public long? SN { get; set; }

	public int? ID { get; set; }

	public int? SupplierID { get; set; }

	public string SupplierName { get; set; }

	public string SupplierPhone { get; set; }

	public string SupplierVatNum { get; set; }

	public DateTime? Purchases_Date { get; set; }

	public DateTime? DateHold { get; set; }

	public string NoteNum { get; set; }

	public decimal? Tax_Discount { get; set; }

	public decimal? TotalPrices_Discount { get; set; }

	public bool? IsWaiting { get; set; }

	public int? TableNum { get; set; }

	public int? RoomNum { get; set; }

	public decimal? HasmPer { get; set; }

	public decimal? HasmAmount { get; set; }

	public int CreditNote { get; set; }

	public string PurchasesType { get; set; }

	public string Note { get; set; }

	public decimal? TotalPrices { get; set; }

	public decimal? DiscountNum { get; set; }

	public decimal? DiscountPerantage { get; set; }

	public decimal? Tax { get; set; }

	public decimal? Safy { get; set; }

	public decimal? Net { get; set; }

	public decimal? CashMoney { get; set; }

	public decimal? CashBank { get; set; }

	public DateTime? BookingDate { get; set; }

	public DateTime? InstallationDate { get; set; }

	public DateTime? jawDate { get; set; }

	public int? HallsID { get; set; }

	public int? OrderNum { get; set; }

	public string HallsName { get; set; }
}


