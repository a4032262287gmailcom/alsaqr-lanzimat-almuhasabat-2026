using System;

namespace GTSErpSystem;

public class Print_Order_OrderReturn_Result
{
	public int? OrderID { get; set; }

	public DateTime? OrderDate { get; set; }

	public string OrderType { get; set; }

	public int? ORDReturnID { get; set; }

	public DateTime? ORDReturnDate { get; set; }

	public int? CreditNote { get; set; }

	public string SupplierName { get; set; }

	public string SupplierPhone { get; set; }

	public string SupplierAddress { get; set; }

	public string Sales { get; set; }

	public string SupplierVatNum { get; set; }

	public string Item_code { get; set; }

	public string item_Name { get; set; }

	public string item_NameEN { get; set; }

	public string ItemNote { get; set; }

	public string UnitsName { get; set; }

	public string Guarantee_Name { get; set; }

	public string Country_Name { get; set; }

	public string Store_Name { get; set; }

	public int? GroupID { get; set; }

	public string GroupName { get; set; }

	public int? SupplierID { get; set; }

	public string ItemNote1 { get; set; }

	public double? Quantity { get; set; }

	public double? UnitPrice { get; set; }

	public double? NetUnitPrice { get; set; }

	public double? TotalPrice { get; set; }

	public double? SafyTotal { get; set; }

	public double? NetTotalPrice { get; set; }

	public double? VAT { get; set; }

	public double? VAT_Discount { get; set; }

	public double? TotalPricesPurch { get; set; }

	public double? TotalPrices_DiscountPurch { get; set; }

	public double? VATPurch { get; set; }

	public double? VATDiscountPurch { get; set; }

	public double? DiscountNumPurch { get; set; }

	public double? Safy { get; set; }

	public double? Net { get; set; }

	public string Note { get; set; }

	public double? TobaccoTax { get; set; }

	public double? AllTax { get; set; }

	public string ORDReturen_Type { get; set; }

	public double NetOrdReturn { get; set; }

	public double NetafterReturn { get; set; }

	public string EmployeeName { get; set; }

	public string EmployeePhone { get; set; }

	public double? CashMoney { get; set; }

	public double? CashBank { get; set; }
}


