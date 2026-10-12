using System;

namespace GTSErpSystem.DAL.Models;

public class DiscountCodes
{
	public int Id { get; set; }

	public string DisCode { get; set; }

	public DateTime? CreationDate { get; set; }

	public decimal? DiscountValue { get; set; }

	public decimal? DiscountPercentage { get; set; }

	public DateTime? DistExpirationDate { get; set; }

	public decimal? NumberUsed { get; set; }

	public string CompanyName { get; set; }

	public string CompanyVatNum { get; set; }

	public int? UserID_Add { get; set; }

	public int? UserBranch_Add { get; set; }

	public string UserMacAddress_Add { get; set; }

	public DateTime? UserDate_Add { get; set; }

	public int? UserID_Update { get; set; }

	public int? UserBranch_Update { get; set; }

	public string UserMacAddress_Update { get; set; }

	public DateTime? UserDate_Update { get; set; }
}


