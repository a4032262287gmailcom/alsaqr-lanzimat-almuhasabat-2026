using System.Collections.Generic;

namespace GTSErpSystem.DAL.Models;

public class GetDataOfZakatByBranchIdStoredProcedureResponse
{
	public int Id { get; set; }

	public string IdentifierID { get; set; }

	public string Name { get; set; }

	public string IdentifierSchemeID { get; set; }

	public string StreetName { get; set; }

	public string BuildingNumber { get; set; }

	public string CityName { get; set; }

	public string PostalZone { get; set; }

	public string IdVat { get; set; }

	public string District { get; set; }

	public string IdentificationCode { get; set; }

	public string CompanyName { get; set; }

	public string VatNumber { get; set; }

	public string Location { get; set; }

	public string BusinessCategory { get; set; }

	public List<BranchUserData> BranchUsersData { get; set; }
}


