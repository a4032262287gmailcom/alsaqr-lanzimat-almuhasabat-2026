using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.Payroll;

internal class Class_EntrustedEmployee
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Emp_Entrusted> GetEntrusted(int ID)
	{
		return db.Emp_Entrusted.Where((Emp_Entrusted x) => x.ID == ID).ToList();
	}

	public List<Emp_Employee> GetEmployee(int ID)
	{
		return db.Emp_Employee.Where((Emp_Employee x) => x.ID == ID).ToList();
	}

	public List<Emp_EntrustedEmployeeDetails> GetPayrollDetails(int ID)
	{
		return db.Emp_EntrustedEmployeeDetails.Where((Emp_EntrustedEmployeeDetails x) => x.EmpID == (int?)ID).ToList();
	}

	public string GetEnterName(int ID)
	{
		Emp_Entrusted emp_Entrusted = db.Emp_Entrusted.Where((Emp_Entrusted x) => x.ID == ID).FirstOrDefault();
		return emp_Entrusted.Name;
	}

	public int InsertItems(DataTable Items, int EmpID)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["رقم العهدة"].ToString() != "")
			{
				Emp_EntrustedEmployeeDetails emp_EntrustedEmployeeDetails = new Emp_EntrustedEmployeeDetails();
				emp_EntrustedEmployeeDetails.EmpID = EmpID;
				emp_EntrustedEmployeeDetails.EnterID = Convert.ToInt32(row["رقم العهدة"]);
				emp_EntrustedEmployeeDetails.EnterMoney = Convert.ToInt32(row["السعر"]);
				emp_EntrustedEmployeeDetails.Note = Convert.ToString(row["البيان"]);
				emp_EntrustedEmployeeDetails.DateEnter = Convert.ToDateTime(row["التاريخ"]);
				emp_EntrustedEmployeeDetails.Quantity = Convert.ToDecimal(row["Quantity"]);
				emp_EntrustedEmployeeDetails.Total = Convert.ToDecimal(row["Total"]);
				db.Emp_EntrustedEmployeeDetails.Add(emp_EntrustedEmployeeDetails);
				result = db.SaveChanges();
			}
		}
		return result;
	}

	public int DeleteDetails(int Code)
	{
		int result = 0;
		List<Emp_EntrustedEmployeeDetails> list = db.Emp_EntrustedEmployeeDetails.Where((Emp_EntrustedEmployeeDetails x) => x.EmpID == (int?)Code).ToList();
		if (list != null)
		{
			foreach (Emp_EntrustedEmployeeDetails item in list)
			{
				db.Emp_EntrustedEmployeeDetails.Attach(item);
				db.Emp_EntrustedEmployeeDetails.Remove(item);
			}
			result = db.SaveChanges();
		}
		return result;
	}
}


