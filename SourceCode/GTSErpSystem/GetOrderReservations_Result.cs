using System;

namespace GTSErpSystem;

public class GetOrderReservations_Result
{
	public int ID { get; set; }

	public int? OrderID { get; set; }

	public int? HallsID { get; set; }

	public string HallName { get; set; }

	public int? SupplierID { get; set; }

	public string SupplierName { get; set; }

	public string SupplierPhone { get; set; }

	public decimal? Tax { get; set; }

	public decimal? TotalPrices { get; set; }

	public decimal? Safy { get; set; }

	public decimal? DiscountNum { get; set; }

	public decimal? DiscountPerantage { get; set; }

	public decimal? Tax_Discount { get; set; }

	public decimal? TotalPrices_Discount { get; set; }

	public decimal? TobaccoTax { get; set; }

	public decimal? AllTax { get; set; }

	public decimal? Net { get; set; }

	public decimal? CashMoney { get; set; }

	public decimal? CashBank { get; set; }

	public int? CashAcc { get; set; }

	public int? BankAcc { get; set; }

	public string BuildingNum { get; set; }

	public string Street { get; set; }

	public string District { get; set; }

	public string City { get; set; }

	public string Country { get; set; }

	public string PostalCode { get; set; }

	public string AdditionalNum { get; set; }

	public string SupplierVatNum { get; set; }

	public DateTime? Purchases_Date { get; set; }

	public DateTime? BookingDate { get; set; }

	public DateTime? InstallationDate { get; set; }

	public DateTime? jawDate { get; set; }

	public int? BranchID { get; set; }

	public decimal? RestNet { get; set; }
}


