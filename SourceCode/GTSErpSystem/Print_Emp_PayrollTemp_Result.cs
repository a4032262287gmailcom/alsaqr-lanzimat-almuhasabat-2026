using System;

namespace GTSErpSystem;

public class Print_Emp_PayrollTemp_Result
{
	public DateTime? PayrollDate { get; set; }

	public string Name { get; set; }

	public decimal? DaysOfBsence { get; set; }

	public decimal? DaysExtra { get; set; }

	public decimal? PaymentOfAdvance { get; set; }

	public decimal? Deduct { get; set; }

	public decimal? NetSalary { get; set; }

	public int? CostCentersID { get; set; }

	public double? Salary { get; set; }

	public double? NetSalary1 { get; set; }

	public double? NetAllowances { get; set; }
}


