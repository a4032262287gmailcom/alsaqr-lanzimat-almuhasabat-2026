namespace GTSErpSystem;

public class Repairs_RepairDetails
{
	public int Id { get; set; }

	public int RepairId { get; set; }

	public int GroupId { get; set; }

	public int MalfunctionId { get; set; }

	public virtual Repairs_Groups Repairs_Groups { get; set; }

	public virtual Repairs_Malfunctions Repairs_Malfunctions { get; set; }

	public virtual Repairs_Repair Repairs_Repair { get; set; }
}


