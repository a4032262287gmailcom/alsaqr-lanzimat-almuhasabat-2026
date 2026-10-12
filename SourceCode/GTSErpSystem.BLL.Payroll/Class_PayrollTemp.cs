using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.Payroll;

public class Class_PayrollTemp
{
	private GTSdbEntities db = new GTSdbEntities();

	private int Num;

	public List<Emp_PayrollTemp> GetPayroll()
	{
		return db.Emp_PayrollTemp.ToList();
	}

	public List<Emp_Employee> GetEmp(int ID)
	{
		return db.Emp_Employee.Where((Emp_Employee x) => x.ID == ID).ToList();
	}

	public string GetEmpName(int ID)
	{
		Emp_Employee emp_Employee = db.Emp_Employee.Where((Emp_Employee x) => x.ID == ID).FirstOrDefault();
		return emp_Employee.Name;
	}

	public int GetEmpAccount(int Account_No)
	{
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)Account_No).FirstOrDefault();
		return account_Accounts.ID;
	}

	public List<Emp_Employee> GetEmp()
	{
		return db.Emp_Employee.ToList();
	}

	public List<Emp_PayrollTemp> GetPayroll(int ID)
	{
		return db.Emp_PayrollTemp.Where((Emp_PayrollTemp x) => x.ID == ID).ToList();
	}

	public List<Emp_PayrollTempDetails> GetPayrollDetails(int ID)
	{
		return db.Emp_PayrollTempDetails.Where((Emp_PayrollTempDetails x) => x.PayRollID == (int?)ID).ToList();
	}

	public List<Select_SearchAccount_ByAccountNumber_Result> GetSearchAccountByAccountNum(string ID)
	{
		try
		{
			Num = Convert.ToInt32(ID);
		}
		catch (Exception)
		{
		}
		return db.Select_SearchAccount_ByAccountNumber(Num).ToList();
	}

	public string GetAccountName(int AccountID)
	{
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.ID == AccountID).FirstOrDefault();
		return account_Accounts.Account_Name;
	}

	public int GetAccountNum(int Num)
	{
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)Num).FirstOrDefault();
		return account_Accounts.ID;
	}

	public int Insert(out int TRNCode, DateTime PayrollDate, string NumNote, string Note)
	{
		int num = 0;
		TRNCode = 0;
		int num2 = Convert.ToInt32(db.Emp_PayrollTemp.Select((Emp_PayrollTemp p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		Emp_PayrollTemp emp_PayrollTemp = new Emp_PayrollTemp();
		emp_PayrollTemp.ID = num2;
		emp_PayrollTemp.PayrollDate = PayrollDate;
		emp_PayrollTemp.Note = Note;
		emp_PayrollTemp.NumNote = NumNote;
		emp_PayrollTemp.Branch_ID = LoginDetails.BranchID;
		emp_PayrollTemp.UserID_Add = LoginDetails.UserID;
		emp_PayrollTemp.UserBranch_Add = LoginDetails.BranchID;
		emp_PayrollTemp.UserMacAddress_Add = LoginDetails.macAddress;
		emp_PayrollTemp.UserDate_Add = DateTime.Now;
		db.Emp_PayrollTemp.Add(emp_PayrollTemp);
		num = db.SaveChanges();
		TRNCode = num2;
		return num;
	}

	public int InsertItems(DataTable Items, int TraNCodeFatora)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			decimal num = Convert.ToDecimal(row["صافي الراتب"]);
			if (row["رقم الحساب"].ToString() != "")
			{
				Emp_PayrollTempDetails emp_PayrollTempDetails = new Emp_PayrollTempDetails();
				emp_PayrollTempDetails.PayRollID = TraNCodeFatora;
				emp_PayrollTempDetails.AccountNum = Convert.ToInt32(row["AccountID"]);
				emp_PayrollTempDetails.EmpID = Convert.ToInt32(row["رقم الحساب"]);
				emp_PayrollTempDetails.Salary = Convert.ToDecimal(row["الراتب"]);
				emp_PayrollTempDetails.AccountBankBox = Convert.ToInt32(row["رقم حساب الصندوق/البنك"]);
				emp_PayrollTempDetails.DaysOfBsence = Convert.ToDecimal(row["أيام غياب"]);
				emp_PayrollTempDetails.DaysExtra = Convert.ToDecimal(row["OverTime"]);
				emp_PayrollTempDetails.Deduct = Convert.ToDecimal(row["جزاءات"]);
				emp_PayrollTempDetails.Merit = Convert.ToDecimal(row["مكافئات"]);
				emp_PayrollTempDetails.PaymentOfAdvance = Convert.ToDecimal(row["تسديد سلفة"]);
				emp_PayrollTempDetails.NetSalary = Convert.ToDecimal(row["صافي الراتب"]);
				emp_PayrollTempDetails.NetAllowances = Convert.ToDecimal(row["NetBadl"]);
				emp_PayrollTempDetails.Delays = Convert.ToDecimal(row["Delays"]);
				emp_PayrollTempDetails.Vacations = Convert.ToDecimal(row["Vacations"]);
				emp_PayrollTempDetails.Bouns = Convert.ToDecimal(row["Bouns"]);
				emp_PayrollTempDetails.CostCentersID = Convert.ToInt32(row["مركز التكلفة"]);
				db.Emp_PayrollTempDetails.Add(emp_PayrollTempDetails);
				result = db.SaveChanges();
			}
		}
		return result;
	}

	public int Update(int TRNCode, DateTime PayrollDate, string NumNote, string Note)
	{
		int result = 0;
		Emp_PayrollTemp emp_PayrollTemp = db.Emp_PayrollTemp.Where((Emp_PayrollTemp x) => x.ID == TRNCode).FirstOrDefault();
		if (emp_PayrollTemp != null)
		{
			emp_PayrollTemp.PayrollDate = PayrollDate;
			emp_PayrollTemp.Note = Note;
			emp_PayrollTemp.NumNote = NumNote;
			emp_PayrollTemp.UserID_Update = LoginDetails.UserID;
			emp_PayrollTemp.UserBranch_Update = LoginDetails.GroupID;
			emp_PayrollTemp.UserMacAddress_Update = LoginDetails.macAddress;
			emp_PayrollTemp.UserDate_Update = DateTime.Now;
			result = db.SaveChanges();
		}
		return result;
	}

	public int Delete(int Code)
	{
		int result = 0;
		Emp_PayrollTemp emp_PayrollTemp = db.Emp_PayrollTemp.Where((Emp_PayrollTemp x) => x.ID == Code).FirstOrDefault();
		if (emp_PayrollTemp != null)
		{
			db.Emp_PayrollTemp.Attach(emp_PayrollTemp);
			db.Emp_PayrollTemp.Remove(emp_PayrollTemp);
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteDetails(int Code)
	{
		int result = 0;
		List<Emp_PayrollTempDetails> list = db.Emp_PayrollTempDetails.Where((Emp_PayrollTempDetails x) => x.PayRollID == (int?)Code).ToList();
		if (list != null)
		{
			foreach (Emp_PayrollTempDetails item in list)
			{
				db.Emp_PayrollTempDetails.Attach(item);
				db.Emp_PayrollTempDetails.Remove(item);
			}
			result = db.SaveChanges();
		}
		return result;
	}
}


