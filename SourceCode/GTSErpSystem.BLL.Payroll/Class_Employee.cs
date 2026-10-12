using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace GTSErpSystem.BLL.Payroll;

public class Class_Employee
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Emp_Employee> GetAllUsers()
	{
		return db.Emp_Employee.ToList();
	}

	public int Insert(string Name, decimal Money, string Phone, string Address, int Branch_ID, DataTable Allowances, decimal NetAllowances)
	{
		int result = 0;
		Emp_Employee emp_Employee = db.Emp_Employee.Where((Emp_Employee x) => x.Name == Name).FirstOrDefault();
		if (emp_Employee == null)
		{
			int num = Convert.ToInt32((from x in db.Account_Accounts
				where x.Account_No > (int?)1101510 && x.Account_No < (int?)1101599
				select x.Account_No).DefaultIfEmpty(1101510).Max() + 1);
			int num2 = Convert.ToInt32(db.Emp_Employee.Select((Emp_Employee x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Emp_Employee emp_Employee2 = new Emp_Employee();
			emp_Employee2.ID = num2;
			emp_Employee2.AccountNo = num;
			emp_Employee2.Name = Name;
			emp_Employee2.Money = Money;
			emp_Employee2.Phone = Phone;
			emp_Employee2.Address = Address;
			emp_Employee2.Branch_ID = Branch_ID;
			emp_Employee2.UserID_Add = LoginDetails.UserID;
			emp_Employee2.UserBranch_Add = LoginDetails.BranchID;
			emp_Employee2.UserMacAddress_Add = LoginDetails.macAddress;
			emp_Employee2.UserDate_Add = DateTime.Now;
			emp_Employee2.NetAllowances = NetAllowances;
			db.Emp_Employee.Add(emp_Employee2);
			result = db.SaveChanges();
			InsertAccount(num, Name);
			ManageEmpAllowances(num2, Allowances);
		}
		return result;
	}

	public void ManageEmpAllowances(int EmpID, DataTable Allowances)
	{
		using SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
		sqlConnection.Open();
		using SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
		try
		{
			using (SqlCommand sqlCommand = new SqlCommand(" DELETE FROM EmployeeAllowances WHERE EmployeeID = @EmpID;", sqlConnection, sqlTransaction))
			{
				sqlCommand.Parameters.AddWithValue("@EmpID", EmpID);
				sqlCommand.ExecuteNonQuery();
			}
			foreach (DataRow row in Allowances.Rows)
			{
				int num = Convert.ToInt32(row["كود"]);
				decimal num2 = 0m;
				if (Allowances.Columns.Contains("Money"))
				{
					num2 = Convert.ToDecimal(row["Money"]);
				}
				using SqlCommand sqlCommand2 = new SqlCommand("\r\n                                    INSERT INTO EmployeeAllowances (EmployeeID, AllowanceID, Money)\r\n                                    VALUES (@EmpID, @AllowanceID, @Money);", sqlConnection, sqlTransaction);
				sqlCommand2.Parameters.AddWithValue("@EmpID", EmpID);
				sqlCommand2.Parameters.AddWithValue("@AllowanceID", num);
				sqlCommand2.Parameters.AddWithValue("@Money", num2);
				sqlCommand2.ExecuteNonQuery();
			}
			sqlTransaction.Commit();
		}
		catch (Exception ex)
		{
			sqlTransaction.Rollback();
			MessageBox.Show("حدث خطأ : " + ex.Message);
		}
	}

	public DataTable GetEmployeeAllowances(int EmpID)
	{
		DataTable dataTable = new DataTable();
		using (SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString))
		{
			sqlConnection.Open();
			using SqlCommand sqlCommand = new SqlCommand("Get_EmployeeAllowances", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.AddWithValue("@EmpID", EmpID);
			using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlDataAdapter.Fill(dataTable);
		}
		return dataTable;
	}

	public int Update(int ID, string Name, decimal Money, string Phone, string Address, int Branch_ID, DataTable Allowances, decimal NetAllowances)
	{
		int num = 0;
		Emp_Employee emp_Employee = db.Emp_Employee.Where((Emp_Employee x) => x.ID == ID).FirstOrDefault();
		Emp_Employee emp_Employee2 = db.Emp_Employee.Where((Emp_Employee x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (emp_Employee != null && emp_Employee2 == null)
		{
			emp_Employee.Name = Name;
			emp_Employee.Money = Money;
			emp_Employee.Phone = Phone;
			emp_Employee.Address = Address;
			emp_Employee.Branch_ID = Branch_ID;
			emp_Employee.UserID_Update = LoginDetails.UserID;
			emp_Employee.UserBranch_Update = LoginDetails.GroupID;
			emp_Employee.UserMacAddress_Update = LoginDetails.macAddress;
			emp_Employee.UserDate_Update = DateTime.Now;
			emp_Employee.NetAllowances = NetAllowances;
			num = db.SaveChanges();
			num = 1;
			int accountNum = Convert.ToInt32(emp_Employee.AccountNo);
			UpdateAccount(accountNum, Name);
			ManageEmpAllowances(ID, Allowances);
		}
		else
		{
			num = 0;
		}
		return num;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Emp_Employee emp_Employee = db.Emp_Employee.Where((Emp_Employee x) => x.ID == Sn).FirstOrDefault();
		Emp_PayrollDetails emp_PayrollDetails = db.Emp_PayrollDetails.Where((Emp_PayrollDetails x) => x.EmpID == (int?)Sn).FirstOrDefault();
		Emp_PayrollTempDetails emp_PayrollTempDetails = db.Emp_PayrollTempDetails.Where((Emp_PayrollTempDetails x) => x.EmpID == (int?)Sn).FirstOrDefault();
		List<EmployeeAllowance> entities = db.EmployeeAllowances.Where((EmployeeAllowance x) => x.EmployeeID == Sn).ToList();
		if (emp_Employee != null && emp_PayrollDetails == null && emp_PayrollTempDetails == null)
		{
			DeleteAccount(Convert.ToInt32(emp_Employee.AccountNo));
			db.Emp_Employee.Attach(emp_Employee);
			db.Emp_Employee.Remove(emp_Employee);
			db.EmployeeAllowances.RemoveRange(entities);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetUnitName(int Code)
	{
		return (from x in db.Emp_Employee
			where x.ID == Code
			select x.Name).FirstOrDefault();
	}

	public void InsertAccount(int AccountNum, string Name)
	{
		int iD = Convert.ToInt32(db.Account_Accounts.Select((Account_Accounts x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
		Account_Accounts account_Accounts = new Account_Accounts();
		account_Accounts.ID = iD;
		account_Accounts.Account_No = AccountNum;
		account_Accounts.Suspended = 1;
		account_Accounts.Account_Level = 4;
		account_Accounts.Final_Account = 2;
		account_Accounts.Account_Type = 1;
		account_Accounts.Account_Nature = 2;
		account_Accounts.Main_Account_No = 135;
		account_Accounts.Account_Name = Name;
		account_Accounts.Priv_Debit = 0m;
		account_Accounts.Priv_Credit = 0m;
		account_Accounts.BranchID = LoginDetails.BranchID;
		db.Account_Accounts.Add(account_Accounts);
		db.SaveChanges();
	}

	public int UpdateAccount(int AccountNum, string Name)
	{
		int num = 0;
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)AccountNum).FirstOrDefault();
		if (account_Accounts != null)
		{
			account_Accounts.Account_Name = Name;
			account_Accounts.UserID_Update = LoginDetails.UserID;
			account_Accounts.UserBranch_Update = LoginDetails.GroupID;
			account_Accounts.UserMacAddress_Update = LoginDetails.macAddress;
			account_Accounts.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int DeleteAccount(int Account_No)
	{
		int result = 0;
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)Account_No).FirstOrDefault();
		if (account_Accounts != null)
		{
			db.Account_Accounts.Attach(account_Accounts);
			db.Account_Accounts.Remove(account_Accounts);
			result = db.SaveChanges();
		}
		return result;
	}

	public List<Account_Branch> GetAllBranch()
	{
		return db.Account_Branch.ToList();
	}

	public List<Emp_Allowances> GetAllowances()
	{
		return db.Emp_Allowances.ToList();
	}

	public decimal? GetAllowanceMoney(int id)
	{
		return (from a in db.Emp_Allowances
			where a.ID == id
			select a.Money).FirstOrDefault();
	}
}


