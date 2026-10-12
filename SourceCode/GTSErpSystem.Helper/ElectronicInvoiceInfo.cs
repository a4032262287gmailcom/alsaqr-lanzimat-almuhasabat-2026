using GTSErpSystem.Dto;

namespace GTSErpSystem.Helper;

public static class ElectronicInvoiceInfo
{
	public static SupplierPartyDto SupplierPartyDto()
	{
		return new SupplierPartyDto
		{
			ID = LoginDetails.SupplierPartyIdentificationID,
			schemeID = LoginDetails.SupplierPartyIdentificationschemeID,
			StreetName = LoginDetails.Street,
			BuildingNumber = LoginDetails.BuildingNum,
			PostalZone = LoginDetails.PostalCode,
			CitySubdivisionName = LoginDetails.District,
			RegistrationName = LoginDetails.CompanyNameAR,
			CompanyID = LoginDetails.VatNum,
			CityName = LoginDetails.Address,
			IdentificationCode = "SA"
		};
	}

	public static CSIDInfoDto CSIDInfoDto()
	{
		return new CSIDInfoDto
		{
			CertPem = LoginDetails.PublicKey,
			CSR = LoginDetails.Crs,
			PrivateKey = LoginDetails.PrivateKey,
			Secretkey = LoginDetails.SecretKey
		};
	}
}


