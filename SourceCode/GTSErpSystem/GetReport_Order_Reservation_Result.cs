using System;

namespace GTSErpSystem;

public class GetReport_Order_Reservation_Result
{
	public int رقم_الحجز { get; set; }

	public string اسم_العميل { get; set; }

	public string رقم_الجوال { get; set; }

	public DateTime? تاريخ_الحجز { get; set; }

	public string IsRent { get; set; }

	public DateTime? تاريخ_البروفة { get; set; }

	public DateTime? تاريخ_الاستلام { get; set; }

	public DateTime? تاريخ_المناسبة { get; set; }

	public DateTime? تاريخ_الاسترجاع { get; set; }

	public decimal? مبلغ_الائتمان { get; set; }
}
