using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.Account;

public class ClassProject
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Select_SearchAccountProjects_Result> GetSearchAccountProjects()
	{
		return db.Select_SearchAccountProjects(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public int Insert(out int id, string NameProject, DateTime DateStart, DateTime DateEnd, decimal ProjectPrice, decimal ProjectVat, decimal InsuranceAmount, decimal InsurancePer, decimal TotalAmount, string Note, DataTable IdCostCenter)
	{
		int result = 0;
		id = 0;
		Account_Projects account_Projects = db.Account_Projects.Where((Account_Projects x) => x.ProjectName == NameProject).FirstOrDefault();
		if (account_Projects == null)
		{
			int num = Convert.ToInt32(db.Account_Projects.Select((Account_Projects x) => x.ProjectID).DefaultIfEmpty(0).Max()) + 1;
			int num2 = Convert.ToInt32((from x in db.Account_Projects
				where x.BranchID == (int?)LoginDetails.BranchID
				select x.ProjectNo).DefaultIfEmpty(0).Max()) + 1;
			Account_Projects account_Projects2 = new Account_Projects();
			id = num;
			account_Projects2.ProjectID = num;
			account_Projects2.ProjectNo = num2;
			account_Projects2.ProjectName = NameProject;
			account_Projects2.DateStart = DateStart;
			account_Projects2.DateEnd = DateEnd;
			account_Projects2.InsuranceAmount = InsuranceAmount;
			account_Projects2.InsurancePer = InsurancePer;
			account_Projects2.ProjectPrice = ProjectPrice;
			account_Projects2.ProjectVat = ProjectVat;
			account_Projects2.TotalAmount = TotalAmount;
			account_Projects2.Note = Note;
			account_Projects2.BranchID = LoginDetails.BranchID;
			account_Projects2.UserID_Add = LoginDetails.UserID;
			account_Projects2.UserBranch_Add = LoginDetails.BranchID;
			account_Projects2.UserMacAddress_Add = LoginDetails.macAddress;
			account_Projects2.UserDate_Add = DateTime.Now;
			db.Account_Projects.Add(account_Projects2);
			List<Account_ProjectsDetails> list = new List<Account_ProjectsDetails>();
			foreach (DataRow row in IdCostCenter.Rows)
			{
				list.Add(new Account_ProjectsDetails
				{
					CostCenterID = Convert.ToInt32(row["الكود"]),
					ProjectAccountID = num
				});
			}
			db.Account_ProjectsDetails.AddRange(list);
			db.SaveChanges();
			result = num2;
		}
		return result;
	}

	public int Update(int ID, string NameProject, DateTime DateStart, DateTime DateEnd, decimal ProjectPrice, decimal ProjectVat, decimal InsuranceAmount, decimal InsurancePer, decimal TotalAmount, string Note, DataTable IdCostCenter)
	{
		int num = 0;
		Account_Projects account_Projects = db.Account_Projects.Where((Account_Projects x) => x.ProjectID == (int?)ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Account_Projects account_Projects2 = db.Account_Projects.Where((Account_Projects x) => x.ProjectName == NameProject && x.ProjectID != (int?)ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (account_Projects != null && account_Projects2 == null)
		{
			account_Projects.ProjectName = NameProject;
			account_Projects.DateStart = DateStart;
			account_Projects.DateEnd = DateEnd;
			account_Projects.InsuranceAmount = InsuranceAmount;
			account_Projects.InsurancePer = InsurancePer;
			account_Projects.ProjectPrice = ProjectPrice;
			account_Projects.ProjectVat = ProjectVat;
			account_Projects.TotalAmount = TotalAmount;
			account_Projects.Note = Note;
			account_Projects.UserID_Update = LoginDetails.UserID;
			account_Projects.UserBranch_Update = LoginDetails.GroupID;
			account_Projects.UserMacAddress_Update = LoginDetails.macAddress;
			account_Projects.UserDate_Update = DateTime.Now;
			List<Account_ProjectsDetails> list = db.Account_ProjectsDetails.Where((Account_ProjectsDetails s) => s.ProjectAccountID == (int?)ID).ToList();
			if (list.Count > 0)
			{
				db.Account_ProjectsDetails.RemoveRange(list);
			}
			List<Account_ProjectsDetails> list2 = new List<Account_ProjectsDetails>();
			foreach (DataRow row in IdCostCenter.Rows)
			{
				list2.Add(new Account_ProjectsDetails
				{
					CostCenterID = Convert.ToInt32(row["الكود"]),
					ProjectAccountID = ID
				});
			}
			db.Account_ProjectsDetails.AddRange(list2);
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Account_Projects Check = db.Account_Projects.Where((Account_Projects x) => x.ProjectID == (int?)Sn && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Order_Orders order_Orders = db.Order_Orders.Where((Order_Orders x) => x.ProjectId == (int?)Sn && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (Check != null && order_Orders == null)
		{
			IQueryable<Account_ProjectsDetails> entities = db.Account_ProjectsDetails.Where((Account_ProjectsDetails x) => x.ProjectAccountID == Check.ProjectID);
			db.Account_Projects.Attach(Check);
			db.Account_Projects.Remove(Check);
			db.Account_ProjectsDetails.RemoveRange(entities);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetName(int sn)
	{
		Account_Projects account_Projects = db.Account_Projects.Where((Account_Projects x) => x.ProjectID == (int?)sn).FirstOrDefault();
		return account_Projects.ProjectName;
	}
}


