using System;

namespace GTSErpSystem;

public class Print_Order_PurchasesReturn_Result
{
	public int? Purchese_ID { get; set; }

	public string DateHjri { get; set; }

	public DateTime? Purchases_Date { get; set; }

	public int? SupplierID { get; set; }

	public string SupplierName { get; set; }

	public string SupplierPhone { get; set; }

	public string SupplierVatNum { get; set; }

	public string Item_code { get; set; }

	public string item_Name { get; set; }

	public string UnitsName { get; set; }

	public string Store_Name { get; set; }

	public double? Quantity { get; set; }

	public double? UnitPrice { get; set; }

	public double? NetUnitPrice { get; set; }

	public double? TotalPrice { get; set; }

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

	public string Order_Type { get; set; }

	public string SupplierAddress { get; set; }

	public string SupplierName1 { get; set; }

	public string SupplierPhone1 { get; set; }

	public string SupplierVatNum1 { get; set; }

	public string Sales { get; set; }
}


