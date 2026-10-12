using System.Collections.Generic;

namespace GTSErpSystem;

public class Account_Type
{
	public int ID { get; set; }

	public string Name { get; set; }

	public virtual ICollection<Account_Accounts> Account_Accounts { get; set; }

	public Account_Type()
	{
		Account_Accounts = new HashSet<Account_Accounts>();
	}
}


