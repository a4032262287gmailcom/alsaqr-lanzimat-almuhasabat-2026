using System.Collections.Generic;

namespace GTSErpSystem;

public class Repairs_Malfunctions
{
	public int Id { get; set; }

	public string Name { get; set; }

	public int GroupId { get; set; }

	public virtual Repairs_Groups Repairs_Groups { get; set; }

	public virtual ICollection<Repairs_RepairDetails> Repairs_RepairDetails { get; set; }

	public Repairs_Malfunctions()
	{
		Repairs_RepairDetails = new HashSet<Repairs_RepairDetails>();
	}
}


