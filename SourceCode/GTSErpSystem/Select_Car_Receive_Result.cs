namespace GTSErpSystem;

public class Select_Car_Receive_Result
{
	public int ID { get; set; }

	public string CustomerName { get; set; }

	public string CustomerPhone { get; set; }

	public string DriverName { get; set; }

	public string DriverPhone { get; set; }

	public string Car_PlateNumber { get; set; }

	public string Car_bodynumber { get; set; }

	public string Driver_PlateNumber { get; set; }

	public string Order_Date { get; set; }

	public string Order_Uploader { get; set; }

	public string Order_Destination { get; set; }

	public decimal? SafyAmount { get; set; }

	public decimal? CashAmount { get; set; }

	public decimal? LastCashAmount { get; set; }

	public bool? IsDone { get; set; }
}


