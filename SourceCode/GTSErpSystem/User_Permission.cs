using System;

namespace GTSErpSystem;

public class User_Permission
{
	public int ID { get; set; }

	public int? ScreenID { get; set; }

	public int? GroupID { get; set; }

	public bool? Allow_Branch { get; set; }

	public bool? Allow_Enter { get; set; }

	public bool? Allow_Save { get; set; }

	public bool? Allow_Edit { get; set; }

	public bool? Allow_Delete { get; set; }

	public bool? Allow_Print { get; set; }

	public bool? Allow_Export { get; set; }

	public int? UserID_Add { get; set; }

	public int? UserBranch_Add { get; set; }

	public string UserMacAddress_Add { get; set; }

	public DateTime? UserDate_Add { get; set; }

	public int? UserID_Update { get; set; }

	public int? UserBranch_Update { get; set; }

	public string UserMacAddress_Update { get; set; }

	public DateTime? UserDate_Update { get; set; }
}


