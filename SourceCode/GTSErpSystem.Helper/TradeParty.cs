using System.Collections.Generic;

namespace GTSErpSystem.Helper;

public class TradeParty
{
	public string ID { get; set; }

	public string Name { get; set; }

	public string ContactName { get; set; }

	public string City { get; set; }

	public string Postcode { get; set; }

	public CountryCode Country { get; set; }

	public string Street { get; set; }

	public List<TaxID> SpecifiedTaxRegistrations { get; set; } = new List<TaxID>();
}


