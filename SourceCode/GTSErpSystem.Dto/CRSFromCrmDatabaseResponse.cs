namespace GTSErpSystem.Dto;

public class CRSFromCrmDatabaseResponse
{
	public string CSR { get; set; }

	public string PublicKey { get; set; }

	public string PrivateKey { get; set; }

	public string SecretKey { get; set; }

	public bool CsIdForUser { get; set; }

	public string ModeInvoice { get; set; }

	public string BranchId { get; set; }

	public int? UserId { get; set; }
}


