namespace GTSErpSystem;

public class ItemQuantity
{
	public int ItemQuantityID { get; set; }

	public int? ItemID { get; set; }

	public int? StoreID { get; set; }

	public decimal? OpeningBalance { get; set; }

	public decimal? CurrentBalance { get; set; }

	public decimal? BeginningInventory { get; set; }

	public decimal? BeginningInventoryPrice { get; set; }

	public decimal? UnitNumber { get; set; }

	public virtual Account_Stores Account_Stores { get; set; }
}


