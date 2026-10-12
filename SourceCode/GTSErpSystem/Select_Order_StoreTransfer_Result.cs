using System;

namespace GTSErpSystem;

public class Select_Order_StoreTransfer_Result
{
	public long? SN { get; set; }

	public int? ID { get; set; }

	public DateTime? DateTransfer { get; set; }

	public string FromStore { get; set; }

	public string ToStore { get; set; }

	public string Note { get; set; }

	public int? FromStoreID { get; set; }

	public int? ToStoreID { get; set; }
}


