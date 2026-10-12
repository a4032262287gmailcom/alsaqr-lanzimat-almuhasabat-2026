using System;
using System.Collections.Generic;

namespace GTSErpSystem;

public class Item_Unit
{
	public int ID { get; set; }

	public string Name { get; set; }

	public int? UserID_Add { get; set; }

	public int? UserBranch_Add { get; set; }

	public string UserMacAddress_Add { get; set; }

	public DateTime? UserDate_Add { get; set; }

	public int? UserID_Update { get; set; }

	public int? UserBranch_Update { get; set; }

	public string UserMacAddress_Update { get; set; }

	public DateTime? UserDate_Update { get; set; }

	public virtual ICollection<Item_Items> Item_Items { get; set; }

	public virtual ICollection<Item_Items> Item_Items1 { get; set; }

	public virtual ICollection<Item_Items> Item_Items2 { get; set; }

	public virtual ICollection<Order_PurchasesDetails> Order_PurchasesDetails { get; set; }

	public virtual ICollection<Order_PurchasesOrderDetails> Order_PurchasesOrderDetails { get; set; }

	public Item_Unit()
	{
		Item_Items = new HashSet<Item_Items>();
		Item_Items1 = new HashSet<Item_Items>();
		Item_Items2 = new HashSet<Item_Items>();
		Order_PurchasesDetails = new HashSet<Order_PurchasesDetails>();
		Order_PurchasesOrderDetails = new HashSet<Order_PurchasesOrderDetails>();
	}
}


