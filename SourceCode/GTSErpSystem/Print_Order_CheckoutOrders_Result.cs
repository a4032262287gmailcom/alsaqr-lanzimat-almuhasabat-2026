using System;

namespace GTSErpSystem;

public class Print_Order_CheckoutOrders_Result
{
	public int? Purchese_ID { get; set; }

	public int? FromPurchID { get; set; }

	public DateTime? Purchases_Date { get; set; }

	public int? SupplierID { get; set; }

	public string SupplierName { get; set; }

	public string SupplierPhone { get; set; }

	public string SupplierVatNum { get; set; }

	public string Note { get; set; }

	public string Item_code { get; set; }

	public string item_Name { get; set; }

	public string UnitsName { get; set; }

	public string Store_Name { get; set; }

	public double? Quantity { get; set; }

	public double? NetOrder { get; set; }

	public double? CashOrder { get; set; }

	public double? BankOrder { get; set; }

	public double? NetCheckOut { get; set; }

	public double? CashCheckOut { get; set; }

	public double? BankCheckOut { get; set; }

	public double? TotalPricesPurch { get; set; }

	public double? TotalPrices_DiscountPurch { get; set; }

	public double? VATPurch { get; set; }

	public double? VATDiscountPurch { get; set; }

	public double? DiscountNumPurch { get; set; }

	public double? Safy { get; set; }

	public double? Net { get; set; }

	public string Note1 { get; set; }

	public string Order_Type { get; set; }

	public string EmployeeName { get; set; }

	public string EmployeePhone { get; set; }
}


