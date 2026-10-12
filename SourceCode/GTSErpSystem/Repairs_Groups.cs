using System.Collections.Generic;

namespace GTSErpSystem;

public class Repairs_Groups
{
	public int Id { get; set; }

	public string GroupName { get; set; }

	public virtual ICollection<Repairs_Malfunctions> Repairs_Malfunctions { get; set; }

	public virtual ICollection<Repairs_RepairDetails> Repairs_RepairDetails { get; set; }

	public Repairs_Groups()
	{
		Repairs_Malfunctions = new HashSet<Repairs_Malfunctions>();
		Repairs_RepairDetails = new HashSet<Repairs_RepairDetails>();
	}
}


