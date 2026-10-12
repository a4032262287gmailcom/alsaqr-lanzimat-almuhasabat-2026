using System;

namespace GTSErpSystem;

public class api_GetAllRestaurant_RoomByPause_Result
{
	public int ID { get; set; }

	public string Name { get; set; }

	public bool? ISPause { get; set; }

	public decimal? Money { get; set; }

	public int? OrderID { get; set; }

	public int? BranchID { get; set; }

	public DateTime? Time1 { get; set; }
}


