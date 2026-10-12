using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using GTSErpSystem.DataSetRep;

namespace GTSErpSystem.BLL.Payroll;

public class Class_Payroll
{
	private GTSdbEntities db = new GTSdbEntities();

	private int Num;

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	public List<Emp_Payroll> GetPayroll()
	{
		return db.Emp_Payroll.OrderByDescending((Emp_Payroll x) => x.ID).ToList();
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
		int result = 0;
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)Account_No).FirstOrDefault();
		if (account_Accounts != null)
		{
			result = account_Accounts.ID;
		}
		return result;
	}

	public List<Emp_Employee> GetEmp()
	{
		return db.Emp_Employee.ToList();
	}

	public List<Emp_Payroll> GetPayroll(int ID)
	{
		return db.Emp_Payroll.Where((Emp_Payroll x) => x.ID == ID).ToList();
	}

	public List<Emp_PayrollDetails> GetPayrollDetails(int ID)
	{
		return db.Emp_PayrollDetails.Where((Emp_PayrollDetails x) => x.PayRollID == (int?)ID).ToList();
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
		int num2 = Convert.ToInt32(db.Emp_Payroll.Select((Emp_Payroll p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		Emp_Payroll emp_Payroll = new Emp_Payroll();
		emp_Payroll.ID = num2;
		emp_Payroll.PayrollDate = PayrollDate;
		emp_Payroll.Note = Note;
		emp_Payroll.NumNote = NumNote;
		emp_Payroll.Branch_ID = LoginDetails.BranchID;
		emp_Payroll.UserID_Add = LoginDetails.UserID;
		emp_Payroll.UserBranch_Add = LoginDetails.BranchID;
		emp_Payroll.UserMacAddress_Add = LoginDetails.macAddress;
		emp_Payroll.UserDate_Add = DateTime.Now;
		db.Emp_Payroll.Add(emp_Payroll);
		num = db.SaveChanges();
		TRNCode = num2;
		return num;
	}

	public int InsertItems(DataTable Items, int TraNCodeFatora)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["رقم الحساب"].ToString() != "")
			{
				Emp_PayrollDetails emp_PayrollDetails = new Emp_PayrollDetails();
				emp_PayrollDetails.PayRollID = TraNCodeFatora;
				emp_PayrollDetails.AccountNum = Convert.ToInt32(row["AccountID"]);
				emp_PayrollDetails.EmpID = Convert.ToInt32(row["رقم الحساب"]);
				emp_PayrollDetails.Salary = Convert.ToDecimal(row["الراتب"]);
				emp_PayrollDetails.AccountBankBox = Convert.ToInt32(row["رقم حساب الصندوق/البنك"]);
				emp_PayrollDetails.DaysOfBsence = Convert.ToDecimal(row["أيام غياب"]);
				emp_PayrollDetails.HourExtra = Convert.ToDecimal(row["OverTime"]);
				emp_PayrollDetails.Deduct = Convert.ToDecimal(row["جزاءات"]);
				emp_PayrollDetails.Merit = Convert.ToDecimal(row["مكافئات"]);
				emp_PayrollDetails.PaymentOfAdvance = Convert.ToDecimal(row["تسديد سلفة"]);
				emp_PayrollDetails.NetSalary = Convert.ToDecimal(row["صافي الراتب"]);
				emp_PayrollDetails.CostCentersID = Convert.ToInt32(row["مركز التكلفة"]);
				emp_PayrollDetails.NetAllowances = Convert.ToInt32(row["NetBadl"]);
				emp_PayrollDetails.Delays = Convert.ToDecimal(row["Delays"]);
				emp_PayrollDetails.Vacations = Convert.ToDecimal(row["Vacations"]);
				emp_PayrollDetails.Bouns = Convert.ToDecimal(row["Bouns"]);
				emp_PayrollDetails.HourExtraValue = ((row["OverTimeValue"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["OverTimeValue"]));
				emp_PayrollDetails.HourDelaysValue = ((row["DelaysValue"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["DelaysValue"]));
				emp_PayrollDetails.DaysOfBsenceValue = ((row["ValueOfDaysAbsence"] == DBNull.Value) ? 0m : Convert.ToDecimal(row["ValueOfDaysAbsence"]));
				db.Emp_PayrollDetails.Add(emp_PayrollDetails);
				result = db.SaveChanges();
			}
		}
		return result;
	}

	public int Update(int TRNCode, DateTime PayrollDate, string NumNote, string Note)
	{
		int result = 0;
		Emp_Payroll emp_Payroll = db.Emp_Payroll.Where((Emp_Payroll x) => x.ID == TRNCode).FirstOrDefault();
		if (emp_Payroll != null)
		{
			emp_Payroll.PayrollDate = PayrollDate;
			emp_Payroll.Note = Note;
			emp_Payroll.NumNote = NumNote;
			emp_Payroll.UserID_Update = LoginDetails.UserID;
			emp_Payroll.UserBranch_Update = LoginDetails.GroupID;
			emp_Payroll.UserMacAddress_Update = LoginDetails.macAddress;
			emp_Payroll.UserDate_Update = DateTime.Now;
			result = db.SaveChanges();
		}
		return result;
	}

	public int Delete(int Code)
	{
		int result = 0;
		Emp_Payroll emp_Payroll = db.Emp_Payroll.Where((Emp_Payroll x) => x.ID == Code).FirstOrDefault();
		if (emp_Payroll != null)
		{
			db.Emp_Payroll.Attach(emp_Payroll);
			db.Emp_Payroll.Remove(emp_Payroll);
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteDetails(int Code)
	{
		int result = 0;
		List<Emp_PayrollDetails> list = db.Emp_PayrollDetails.Where((Emp_PayrollDetails x) => x.PayRollID == (int?)Code).ToList();
		if (list != null)
		{
			foreach (Emp_PayrollDetails item in list)
			{
				db.Emp_PayrollDetails.Attach(item);
				db.Emp_PayrollDetails.Remove(item);
			}
			result = db.SaveChanges();
		}
		return result;
	}

	public int InsertTran(out int TRNCode, int PayCode, string NoteNum, DateTime PurDate, string Note)
	{
		int num = 0;
		TRNCode = 0;
		int num2 = Convert.ToInt32(db.Tran_Tran.Select((Tran_Tran p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		Tran_Tran tran_Tran = new Tran_Tran();
		tran_Tran.ID = num2;
		tran_Tran.ReferenceCode = PayCode;
		tran_Tran.TranTypeID = 36;
		tran_Tran.DocCode = NoteNum;
		tran_Tran.TranDate = PurDate;
		tran_Tran.BranchID = LoginDetails.BranchID;
		tran_Tran.Note = Note;
		tran_Tran.UserID_Add = LoginDetails.UserID;
		tran_Tran.UserBranch_Add = LoginDetails.BranchID;
		tran_Tran.UserMacAddress_Add = LoginDetails.macAddress;
		tran_Tran.UserDate_Add = DateTime.Now;
		db.Tran_Tran.Add(tran_Tran);
		num = db.SaveChanges();
		TRNCode = num2;
		return num;
	}

	public int UpdateTran(out int TRNCode, int PayCode, string NoteNum, DateTime PurDate, string Note)
	{
		int result = 0;
		TRNCode = 0;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)PayCode && x.TranTypeID == (int?)36).FirstOrDefault();
		if (tran_Tran != null)
		{
			tran_Tran.DocCode = NoteNum;
			tran_Tran.TranDate = PurDate;
			tran_Tran.BranchID = LoginDetails.BranchID;
			tran_Tran.Note = Note;
			tran_Tran.UserID_Update = LoginDetails.UserID;
			tran_Tran.UserBranch_Update = LoginDetails.GroupID;
			tran_Tran.UserMacAddress_Update = LoginDetails.macAddress;
			tran_Tran.UserDate_Update = DateTime.Now;
			TRNCode = Convert.ToInt32(tran_Tran.ID);
			result = db.SaveChanges();
		}
		return result;
	}

	public int InsertItemsTran(DataTable Items, int Datepay, int TraNCodeFatora, bool ISEdit)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (!(row["رقم الحساب"].ToString() != ""))
			{
				continue;
			}
			string text = Convert.ToString(row["اسم الموظف"]);
			Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
			tran_TranDetails.TranSn = TraNCodeFatora;
			tran_TranDetails.Account_Sn = LoginDetails.Acc_Payroll;
			tran_TranDetails.AccounIindex = 1;
			tran_TranDetails.Debit = Convert.ToDecimal(row["الراتب"]);
			tran_TranDetails.Credit = 0m;
			tran_TranDetails.BranchID = LoginDetails.BranchID;
			tran_TranDetails.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
			if (!ISEdit)
			{
				tran_TranDetails.TranDesc = "استحقاق راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text;
			}
			else
			{
				tran_TranDetails.TranDesc = "استحقاق راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text + " معدلة ";
			}
			db.Tran_TranDetails.Add(tran_TranDetails);
			result = db.SaveChanges();
			if (Convert.ToDecimal(row["مكافئات"]) > 0m)
			{
				Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
				tran_TranDetails2.TranSn = TraNCodeFatora;
				tran_TranDetails2.Account_Sn = LoginDetails.Acc_Rewards;
				tran_TranDetails2.AccounIindex = 1;
				tran_TranDetails2.Debit = Math.Round(Convert.ToDecimal(row["مكافئات"]), 2);
				tran_TranDetails2.Credit = 0m;
				tran_TranDetails2.BranchID = LoginDetails.BranchID;
				tran_TranDetails2.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails2.TranDesc = "مكافئات راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text;
				}
				else
				{
					tran_TranDetails2.TranDesc = "مكافئات راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text + " معدلة ";
				}
				db.Tran_TranDetails.Add(tran_TranDetails2);
				result = db.SaveChanges();
			}
			if (Convert.ToDecimal(row["Bouns"]) > 0m)
			{
				Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
				tran_TranDetails3.TranSn = TraNCodeFatora;
				tran_TranDetails3.Account_Sn = LoginDetails.Acc_Bonus;
				tran_TranDetails3.AccounIindex = 1;
				tran_TranDetails3.Debit = Math.Round(Convert.ToDecimal(row["Bouns"]), 2);
				tran_TranDetails3.Credit = 0m;
				tran_TranDetails3.BranchID = LoginDetails.BranchID;
				tran_TranDetails3.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails3.TranDesc = "بونص راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text;
				}
				else
				{
					tran_TranDetails3.TranDesc = "بونص راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text + " معدلة ";
				}
				db.Tran_TranDetails.Add(tran_TranDetails3);
				result = db.SaveChanges();
			}
			if (Convert.ToDecimal(row["OverTimeValue"]) > 0m)
			{
				Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
				tran_TranDetails4.TranSn = TraNCodeFatora;
				tran_TranDetails4.Account_Sn = LoginDetails.Acc_Overtime;
				tran_TranDetails4.AccounIindex = 1;
				tran_TranDetails4.Debit = Convert.ToDecimal(row["OverTimeValue"]);
				tran_TranDetails4.Credit = 0m;
				tran_TranDetails4.BranchID = LoginDetails.BranchID;
				tran_TranDetails4.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails4.TranDesc = "وقت اضافي راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text;
				}
				else
				{
					tran_TranDetails4.TranDesc = "وقت اضافي راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text + " معدلة ";
				}
				db.Tran_TranDetails.Add(tran_TranDetails4);
				result = db.SaveChanges();
			}
			if (Convert.ToDecimal(row["NetBadl"]) > 0m)
			{
				Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
				tran_TranDetails5.TranSn = TraNCodeFatora;
				tran_TranDetails5.Account_Sn = LoginDetails.Acc_Allowances;
				tran_TranDetails5.AccounIindex = 1;
				tran_TranDetails5.Debit = Math.Round(Convert.ToDecimal(row["NetBadl"]), 2);
				tran_TranDetails5.Credit = 0m;
				tran_TranDetails5.BranchID = LoginDetails.BranchID;
				tran_TranDetails5.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails5.TranDesc = "بدلات راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text;
				}
				else
				{
					tran_TranDetails5.TranDesc = "بدلات راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text + " معدلة ";
				}
				db.Tran_TranDetails.Add(tran_TranDetails5);
				result = db.SaveChanges();
			}
			if (Convert.ToDecimal(row["ValueOfDaysAbsence"]) > 0m)
			{
				Tran_TranDetails tran_TranDetails6 = new Tran_TranDetails();
				tran_TranDetails6.TranSn = TraNCodeFatora;
				tran_TranDetails6.Account_Sn = LoginDetails.Acc_Absences;
				tran_TranDetails6.AccounIindex = 1;
				tran_TranDetails6.Debit = 0m;
				tran_TranDetails6.Credit = Convert.ToDecimal(row["ValueOfDaysAbsence"]);
				tran_TranDetails6.BranchID = LoginDetails.BranchID;
				tran_TranDetails6.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails6.TranDesc = "غيابات راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text;
				}
				else
				{
					tran_TranDetails6.TranDesc = "غيابات راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text + " معدلة ";
				}
				db.Tran_TranDetails.Add(tran_TranDetails6);
				result = db.SaveChanges();
			}
			if (Convert.ToDecimal(row["جزاءات"]) > 0m)
			{
				Tran_TranDetails tran_TranDetails7 = new Tran_TranDetails();
				tran_TranDetails7.TranSn = TraNCodeFatora;
				tran_TranDetails7.Account_Sn = LoginDetails.Acc_Penalties;
				tran_TranDetails7.AccounIindex = 1;
				tran_TranDetails7.Debit = 0m;
				tran_TranDetails7.Credit = Math.Round(Convert.ToDecimal(row["جزاءات"]), 2);
				tran_TranDetails7.BranchID = LoginDetails.BranchID;
				tran_TranDetails7.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails7.TranDesc = "جزاءات راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text;
				}
				else
				{
					tran_TranDetails7.TranDesc = "جزاءات راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text + " معدلة ";
				}
				db.Tran_TranDetails.Add(tran_TranDetails7);
				result = db.SaveChanges();
			}
			if (Convert.ToDecimal(row["DelaysValue"]) > 0m)
			{
				Tran_TranDetails tran_TranDetails8 = new Tran_TranDetails();
				tran_TranDetails8.TranSn = TraNCodeFatora;
				tran_TranDetails8.Account_Sn = LoginDetails.Acc_LateArrivals;
				tran_TranDetails8.AccounIindex = 1;
				tran_TranDetails8.Debit = 0m;
				tran_TranDetails8.Credit = Convert.ToDecimal(row["DelaysValue"]);
				tran_TranDetails8.BranchID = LoginDetails.BranchID;
				tran_TranDetails8.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails8.TranDesc = "تاخيرات راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text;
				}
				else
				{
					tran_TranDetails8.TranDesc = "تاخيرات راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text + " معدلة ";
				}
				db.Tran_TranDetails.Add(tran_TranDetails8);
				result = db.SaveChanges();
			}
			Tran_TranDetails tran_TranDetails9 = new Tran_TranDetails();
			tran_TranDetails9.TranSn = TraNCodeFatora;
			tran_TranDetails9.Account_Sn = GetEmpAccount(Convert.ToInt32(row["AccountID"]));
			tran_TranDetails9.AccounIindex = 2;
			tran_TranDetails9.Debit = 0m;
			tran_TranDetails9.Credit = Math.Round(Convert.ToDecimal(row["صافي الراتب"]) + Convert.ToDecimal(row["تسديد سلفة"]), 2);
			tran_TranDetails9.BranchID = LoginDetails.BranchID;
			tran_TranDetails9.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
			if (!ISEdit)
			{
				tran_TranDetails9.TranDesc = "استحقاق راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " )";
			}
			else
			{
				tran_TranDetails9.TranDesc = "استحقاق راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) معدلة ";
			}
			db.Tran_TranDetails.Add(tran_TranDetails9);
			result = db.SaveChanges();
			Tran_TranDetails tran_TranDetails10 = new Tran_TranDetails();
			tran_TranDetails10.TranSn = TraNCodeFatora;
			tran_TranDetails10.Account_Sn = Convert.ToInt32(row["رقم حساب الصندوق/البنك"]);
			tran_TranDetails10.AccounIindex = 4;
			tran_TranDetails10.Debit = 0m;
			tran_TranDetails10.Credit = Math.Round(Convert.ToDecimal(row["صافي الراتب"]) + Convert.ToDecimal(row["تسديد سلفة"]), 2);
			tran_TranDetails10.BranchID = LoginDetails.BranchID;
			tran_TranDetails10.CostCentersID = Convert.ToInt32(row["مركز التكلفة"]);
			if (!ISEdit)
			{
				tran_TranDetails10.TranDesc = "صرف راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text;
			}
			else
			{
				tran_TranDetails10.TranDesc = "صرف راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text + " معدلة ";
			}
			db.Tran_TranDetails.Add(tran_TranDetails10);
			result = db.SaveChanges();
			Tran_TranDetails tran_TranDetails11 = new Tran_TranDetails();
			tran_TranDetails11.TranSn = TraNCodeFatora;
			tran_TranDetails11.Account_Sn = GetEmpAccount(Convert.ToInt32(row["AccountID"]));
			tran_TranDetails11.AccounIindex = 3;
			tran_TranDetails11.Debit = Math.Round(Convert.ToDecimal(row["صافي الراتب"]) + Convert.ToDecimal(row["تسديد سلفة"]), 2);
			tran_TranDetails11.Credit = 0m;
			tran_TranDetails11.BranchID = LoginDetails.BranchID;
			tran_TranDetails11.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
			if (!ISEdit)
			{
				tran_TranDetails11.TranDesc = "صرف راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " )";
			}
			else
			{
				tran_TranDetails11.TranDesc = "صرف راتب شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) معدلة ";
			}
			db.Tran_TranDetails.Add(tran_TranDetails11);
			result = db.SaveChanges();
			Tran_TranDetails tran_TranDetails12 = new Tran_TranDetails();
			tran_TranDetails12.TranSn = TraNCodeFatora;
			tran_TranDetails12.Account_Sn = LoginDetails.Acc_AdvanceMoney;
			tran_TranDetails12.AccounIindex = 4;
			tran_TranDetails12.Debit = Math.Round(Convert.ToDecimal(row["تسديد سلفة"]), 2);
			tran_TranDetails12.Credit = 0m;
			tran_TranDetails12.BranchID = LoginDetails.BranchID;
			tran_TranDetails12.CostCentersID = Convert.ToInt32(row["مركز التكلفة"]);
			if (!ISEdit)
			{
				tran_TranDetails12.TranDesc = " استحقاق رد السلفة من شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text;
			}
			else
			{
				tran_TranDetails12.TranDesc = "استحقاق رد السلفة من شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) للموظف " + text + " معدلة ";
			}
			db.Tran_TranDetails.Add(tran_TranDetails12);
			result = db.SaveChanges();
			if (Convert.ToDecimal(row["تسديد سلفة"]) > 0m)
			{
				Tran_TranDetails tran_TranDetails13 = new Tran_TranDetails();
				tran_TranDetails13.TranSn = TraNCodeFatora;
				tran_TranDetails13.Account_Sn = GetEmpAccount(Convert.ToInt32(row["AccountID"]));
				tran_TranDetails13.AccounIindex = 3;
				tran_TranDetails13.Credit = Math.Round(Convert.ToDecimal(row["تسديد سلفة"]), 2);
				tran_TranDetails13.Debit = 0m;
				tran_TranDetails13.BranchID = LoginDetails.BranchID;
				tran_TranDetails13.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails13.TranDesc = "تسديد سلفة من شهر " + Datepay + " رقم ( " + TraNCodeFatora + " )";
				}
				else
				{
					tran_TranDetails13.TranDesc = "تسديد سلفة من شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) معدلة ";
				}
				db.Tran_TranDetails.Add(tran_TranDetails13);
				result = db.SaveChanges();
				Tran_TranDetails tran_TranDetails14 = new Tran_TranDetails();
				tran_TranDetails14.TranSn = TraNCodeFatora;
				tran_TranDetails14.Account_Sn = Convert.ToInt32(row["رقم حساب الصندوق/البنك"]);
				tran_TranDetails14.AccounIindex = 3;
				tran_TranDetails14.Debit = Math.Round(Convert.ToDecimal(row["تسديد سلفة"]), 2);
				tran_TranDetails14.Credit = 0m;
				tran_TranDetails14.BranchID = LoginDetails.BranchID;
				tran_TranDetails14.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails14.TranDesc = "تسديد سلفة من شهر " + Datepay + " رقم ( " + TraNCodeFatora + " )";
				}
				else
				{
					tran_TranDetails14.TranDesc = "تسديد سلفة من شهر " + Datepay + " رقم ( " + TraNCodeFatora + " ) معدلة ";
				}
				db.Tran_TranDetails.Add(tran_TranDetails14);
				result = db.SaveChanges();
				Tran_TranDetails tran_TranDetails15 = new Tran_TranDetails();
				tran_TranDetails15.TranSn = TraNCodeFatora;
				tran_TranDetails15.Account_Sn = LoginDetails.Acc_AdvanceMoneRecipt;
				tran_TranDetails15.AccounIindex = 3;
				tran_TranDetails15.Debit = 0m;
				tran_TranDetails15.Credit = Math.Round(Convert.ToDecimal(row["تسديد سلفة"]), 2);
				tran_TranDetails15.BranchID = LoginDetails.BranchID;
				tran_TranDetails15.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails15.TranDesc = "تسديد سلفة من شهر  " + Datepay + " رقم ( " + TraNCodeFatora + " )";
				}
				else
				{
					tran_TranDetails15.TranDesc = "تسديد سلفة من شهر  " + Datepay + " رقم ( " + TraNCodeFatora + " ) معدلة ";
				}
				db.Tran_TranDetails.Add(tran_TranDetails15);
				result = db.SaveChanges();
			}
		}
		return result;
	}

	public int DeleteTran(int Code)
	{
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)36).FirstOrDefault().ID;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ID == TranID).FirstOrDefault();
		if (tran_Tran != null)
		{
			db.Tran_Tran.Attach(tran_Tran);
			db.Tran_Tran.Remove(tran_Tran);
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteTranDetails(int Code)
	{
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)36).FirstOrDefault().ID;
		List<Tran_TranDetails> list = db.Tran_TranDetails.Where((Tran_TranDetails x) => x.TranSn == (int?)TranID).ToList();
		if (list != null)
		{
			foreach (Tran_TranDetails item in list)
			{
				db.Tran_TranDetails.Attach(item);
				db.Tran_TranDetails.Remove(item);
			}
			result = db.SaveChanges();
		}
		return result;
	}

	public decimal Ledger_Employee(int AccID)
	{
		decimal num = 0m;
		try
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableGetLedger_Employee"].Clear();
			SqlCommand sqlCommand = new SqlCommand("GetLedger_Employee", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@AccountID", AccID);
			sqlDataAdapter.Fill(dataSetALL.TableGetLedger_Employee);
			sqlConnection.Close();
			DataSetALL.TableGetLedger_EmployeeRow tableGetLedger_EmployeeRow = dataSetALL.TableGetLedger_Employee.FirstOrDefault();
			return Convert.ToDecimal(tableGetLedger_EmployeeRow.TotalPayment);
		}
		catch (Exception)
		{
			num = 0m;
		}
		return num;
	}
}


