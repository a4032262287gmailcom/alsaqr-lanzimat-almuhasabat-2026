using System;

namespace GTSErpSystem.Dto;

public class OrderOrdersDto
{
	public int ID { get; set; }

	public int? PurBranchID { get; set; }

	public int? BranchID { get; set; }

	public int? SupplierID { get; set; }

	public string SupplierName { get; set; }

	public string SupplierPhone { get; set; }

	public string SupplierVatNum { get; set; }

	public bool? IsWaiting { get; set; }

	public DateTime? Purchases_Date { get; set; }

	public DateTime? DateHold { get; set; }

	public int? Order_Paymant_Type { get; set; }

	public int? OrderTypeId { get; set; }

	public int? Restaurant_TypeID { get; set; }

	public string SalesMan { get; set; }

	public int? Restaurant_PayType { get; set; }

	public int? CostCentersID { get; set; }

	public int? ProjectId { get; set; }

	public string Note { get; set; }

	public string NoteNum { get; set; }

	public decimal CostOrder { get; set; }

	public string Street { get; set; }

	public int BounceID { get; set; }

	public string District { get; set; }

	public string City { get; set; }

	public string Country { get; set; }

	public string AdditionalNum { get; set; }

	public string PostalCode { get; set; }

	public string CommercialRecord { get; set; }

	public string BuildingNum { get; set; }

	public decimal? Tax { get; set; }

	public decimal? AllTax { get; set; }

	public decimal? Tax_Discount { get; set; }

	public decimal? TotalPrices { get; set; }

	public decimal? Safy { get; set; }

	public decimal? TotalPrices_Discount { get; set; }

	public decimal? DiscountNum { get; set; }

	public decimal? DiscountPerantage { get; set; }

	public decimal? TobaccoTax { get; set; }

	public decimal? CashMoney { get; set; }

	public decimal? FinCash { get; set; }

	public decimal? CashBank { get; set; }

	public decimal? FinBank { get; set; }

	public decimal? BankTransfare { get; set; }

	public decimal? FinBankTransfare { get; set; }

	public decimal? Net { get; set; }

	public int? YearId { get; set; }

	public int? UserID_Add { get; set; }

	public int? UserBranch_Add { get; set; }

	public bool? OrderCashierType { get; set; }

	public int? RoomNum { get; set; }

	public int? TableNum { get; set; }

	public bool Sectoral { get; set; }

	public string QRCode { get; set; }

	public DateTime? UserDate_Add { get; set; }

	public int? UserID_Update { get; set; }

	public int? UserBranch_Update { get; set; }

	public DateTime? UserDate_Update { get; set; }

	public string NormalOrSimpleInvoice { get; set; }

	public string ThirdParty { get; set; }

	public string NominalInvoice { get; set; }

	public string ExportInvoice { get; set; }

	public string SummaryInvoice { get; set; }

	public string SelfInvoice { get; set; }

	public int? OrderTypeElectronicInvoiceId { get; set; }
}


