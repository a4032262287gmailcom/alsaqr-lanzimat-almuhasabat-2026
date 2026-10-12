using System.Collections.Generic;

namespace GTSErpSystem;

public class Repairs_Repair
{
	public int Id { get; set; }

	public string CustomerName { get; set; }

	public string PhoneNumber { get; set; }

	public string PlateNumber { get; set; }

	public string CarModel { get; set; }

	public string CarType { get; set; }

	public string CarColor { get; set; }

	public string Notes { get; set; }

	public virtual ICollection<Repairs_RepairDetails> Repairs_RepairDetails { get; set; }

	public Repairs_Repair()
	{
		Repairs_RepairDetails = new HashSet<Repairs_RepairDetails>();
	}
}


