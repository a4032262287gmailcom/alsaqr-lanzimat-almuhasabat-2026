using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Payroll;

internal class Class_Entrusted
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Emp_Entrusted> GetAll()
	{
		return db.Emp_Entrusted.ToList();
	}

	public int Insert(string Name, decimal Money)
	{
		int result = 0;
		Emp_Entrusted emp_Entrusted = db.Emp_Entrusted.Where((Emp_Entrusted x) => x.Name == Name).FirstOrDefault();
		if (emp_Entrusted == null)
		{
			int iD = Convert.ToInt32(db.Emp_Entrusted.Select((Emp_Entrusted x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Emp_Entrusted emp_Entrusted2 = new Emp_Entrusted();
			emp_Entrusted2.ID = iD;
			emp_Entrusted2.Name = Name;
			emp_Entrusted2.Money = Money;
			emp_Entrusted2.BranchID = LoginDetails.BranchID;
			emp_Entrusted2.UserID_Add = LoginDetails.UserID;
			emp_Entrusted2.UserBranch_Add = LoginDetails.BranchID;
			emp_Entrusted2.UserMacAddress_Add = LoginDetails.macAddress;
			emp_Entrusted2.UserDate_Add = DateTime.Now;
			db.Emp_Entrusted.Add(emp_Entrusted2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name, decimal Money)
	{
		int result = 0;
		Emp_Entrusted emp_Entrusted = db.Emp_Entrusted.Where((Emp_Entrusted x) => x.ID == ID).FirstOrDefault();
		Emp_Entrusted emp_Entrusted2 = db.Emp_Entrusted.Where((Emp_Entrusted x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (emp_Entrusted != null && emp_Entrusted2 == null)
		{
			emp_Entrusted.Name = Name;
			emp_Entrusted.Money = Money;
			emp_Entrusted.BranchID = LoginDetails.BranchID;
			emp_Entrusted.UserID_Update = LoginDetails.UserID;
			emp_Entrusted.UserBranch_Update = LoginDetails.GroupID;
			emp_Entrusted.UserMacAddress_Update = LoginDetails.macAddress;
			emp_Entrusted.UserDate_Update = DateTime.Now;
			result = db.SaveChanges();
			result = 1;
		}
		return result;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Emp_Entrusted emp_Entrusted = db.Emp_Entrusted.Where((Emp_Entrusted x) => x.ID == Sn).FirstOrDefault();
		Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.Class_ID == (int?)Sn).FirstOrDefault();
		if (emp_Entrusted != null && item_Items == null)
		{
			db.Emp_Entrusted.Attach(emp_Entrusted);
			db.Emp_Entrusted.Remove(emp_Entrusted);
			result = db.SaveChanges();
		}
		return result;
	}
}


