namespace GTSErpSystem;

public class GetReport_Car_Result
{
	public int ID { get; set; }

	public int? CustID { get; set; }

	public string custname { get; set; }

	public string custphone { get; set; }

	public string custvatnum { get; set; }

	public string custadress { get; set; }

	public string custCommercialRecord { get; set; }

	public int? Cust_Fin_Num { get; set; }

	public string TypeOrder { get; set; }

	public string Order_Date { get; set; }

	public string Order_DateReceipt { get; set; }

	public string Order_DateResend { get; set; }

	public string Order_Uploader { get; set; }

	public string Order_Destination { get; set; }

	public string Car_bodynumber { get; set; }

	public string CarType { get; set; }

	public string CarColor { get; set; }

	public string Car_Model { get; set; }

	public string Car_PlateNumber { get; set; }

	public string Driver { get; set; }

	public string Driver_PlateNumber { get; set; }

	public decimal? ShippingAmount { get; set; }

	public decimal? DiscountAmount { get; set; }

	public decimal? TotalAmount { get; set; }

	public decimal? VatAmount { get; set; }

	public decimal? SafyAmount { get; set; }

	public decimal? CashAmount { get; set; }

	public decimal? LastCashAmount { get; set; }

	public bool? IsDone { get; set; }
}


