namespace GTSErpSystem.Dto;

public class CertificateRequestDto
{
	public int? BranchId { get; set; }

	public string CommonName { get; set; }

	public string OrganizationName { get; set; }

	public string OrganizationUnitName { get; set; }

	public string CountryName { get; set; }

	public string SerialNumber { get; set; }

	public string OrganizationIdentifier { get; set; }

	public string InvoiceType { get; set; }

	public string Location { get; set; }

	public string BusinessCategory { get; set; }

	public string OTP { get; set; }

	public string Mode { get; set; }

	public string CertPem { get; set; }

	public string Csr { get; set; }

	public string Privatekey { get; set; }

	public string Publickey { get; set; }

	public string Secretkey { get; set; }

	public string ErrorMessage { get; set; }

	public string ModeInvoice { get; set; }

	public string InvoiceHash { get; set; }

	public int? UserId { get; set; }

	public bool CsIdForUser { get; set; }
}


