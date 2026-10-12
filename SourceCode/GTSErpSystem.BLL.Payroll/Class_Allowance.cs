using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Payroll;

public class Class_Allowance
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Emp_Allowances> GetAll()
	{
		return db.Emp_Allowances.ToList();
	}

	public int Insert(string Name, decimal Money, bool ISAffected)
	{
		int result = 0;
		Emp_Allowances emp_Allowances = db.Emp_Allowances.Where((Emp_Allowances x) => x.Name == Name).FirstOrDefault();
		if (emp_Allowances == null)
		{
			int iD = Convert.ToInt32(db.Emp_Allowances.Select((Emp_Allowances x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Emp_Allowances emp_Allowances2 = new Emp_Allowances();
			emp_Allowances2.ID = iD;
			emp_Allowances2.Name = Name;
			emp_Allowances2.Money = Money;
			emp_Allowances2.ISAffected = ISAffected;
			emp_Allowances2.UserID_Add = LoginDetails.UserID;
			emp_Allowances2.UserBranch_Add = LoginDetails.BranchID;
			emp_Allowances2.UserMacAddress_Add = LoginDetails.macAddress;
			emp_Allowances2.UserDate_Add = DateTime.Now;
			db.Emp_Allowances.Add(emp_Allowances2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name, decimal Money, bool ISAffected)
	{
		int result = 0;
		Emp_Allowances emp_Allowances = db.Emp_Allowances.Where((Emp_Allowances x) => x.ID == ID).FirstOrDefault();
		Emp_Allowances emp_Allowances2 = db.Emp_Allowances.Where((Emp_Allowances x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (emp_Allowances != null && emp_Allowances2 == null)
		{
			emp_Allowances.Name = Name;
			emp_Allowances.Money = Money;
			emp_Allowances.ISAffected = ISAffected;
			emp_Allowances.UserID_Update = LoginDetails.UserID;
			emp_Allowances.UserBranch_Update = LoginDetails.GroupID;
			emp_Allowances.UserMacAddress_Update = LoginDetails.macAddress;
			emp_Allowances.UserDate_Update = DateTime.Now;
			result = db.SaveChanges();
			result = 1;
		}
		return result;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Emp_Allowances emp_Allowances = db.Emp_Allowances.Where((Emp_Allowances x) => x.ID == Sn).FirstOrDefault();
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.Class_ID == (int?)Sn).FirstOrDefault();
		if (emp_Allowances != null && item_Items == null)
		{
			db.Emp_Allowances.Attach(emp_Allowances);
			db.Emp_Allowances.Remove(emp_Allowances);
			result = db.SaveChanges();
		}
		return result;
	}
}


