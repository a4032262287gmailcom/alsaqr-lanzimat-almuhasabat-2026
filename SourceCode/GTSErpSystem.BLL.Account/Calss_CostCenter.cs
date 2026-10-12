using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Account;

public class Calss_CostCenter
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Account_Projects> GetSearchAccountProjects()
	{
		return db.Account_Projects.Where((Account_Projects x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Select_SearchAccountCostCenter_Result> GetSearchAccountCostCenter()
	{
		return db.Select_SearchAccountCostCenter(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<Select_SearchAccountCostCenterAll_Result> GetSearchAccountCostCenterAll()
	{
		return db.Select_SearchAccountCostCenterAll(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<Select_SearchAccountCostCenterByProject_Result> GetCostCentersByProject(int projectId)
	{
		return db.Select_SearchAccountCostCenterByProject(projectId, Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public int InsertProjectCost(int CostID, int ProjectID)
	{
		int result = 0;
		Account_ProjectsDetails account_ProjectsDetails = db.Account_ProjectsDetails.Where((Account_ProjectsDetails x) => x.CostCenterID == (int?)CostID && x.ProjectAccountID == (int?)ProjectID).FirstOrDefault();
		if (account_ProjectsDetails == null)
		{
			Account_ProjectsDetails account_ProjectsDetails2 = new Account_ProjectsDetails();
			account_ProjectsDetails2.CostCenterID = CostID;
			account_ProjectsDetails2.ProjectAccountID = ProjectID;
			db.Account_ProjectsDetails.Add(account_ProjectsDetails2);
			db.SaveChanges();
			result = db.SaveChanges();
		}
		return result;
	}

	public int UpdateProjectCost(int CostID, int ProjectID)
	{
		int num = 0;
		Account_ProjectsDetails account_ProjectsDetails = db.Account_ProjectsDetails.Where((Account_ProjectsDetails x) => x.CostCenterID == (int?)CostID && x.ProjectAccountID == (int?)ProjectID).FirstOrDefault();
		if (account_ProjectsDetails != null)
		{
			account_ProjectsDetails.CostCenterID = CostID;
			account_ProjectsDetails.ProjectAccountID = ProjectID;
			return db.SaveChanges();
		}
		return 0;
	}

	public int DeleteProjectCost(int CostID)
	{
		int result = 0;
		Account_ProjectsDetails account_ProjectsDetails = db.Account_ProjectsDetails.Where((Account_ProjectsDetails x) => x.CostCenterID == (int?)CostID).FirstOrDefault();
		if (account_ProjectsDetails != null)
		{
			db.Account_ProjectsDetails.Attach(account_ProjectsDetails);
			db.Account_ProjectsDetails.Remove(account_ProjectsDetails);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Insert(string NameSon, int CostCenterNum, int FatherID, int ProjectID, string Note, bool HideCost, bool CostDetaulte)
	{
		int result = 0;
		Account_CostCenters account_CostCenters = db.Account_CostCenters.Where((Account_CostCenters x) => x.CostCentersName == NameSon).FirstOrDefault();
		if (account_CostCenters == null)
		{
			int num = Convert.ToInt32(db.Account_CostCenters.Select((Account_CostCenters x) => x.CostCentersID).DefaultIfEmpty(0).Max()) + 1;
			int num2 = Convert.ToInt32((from x in db.Account_CostCenters
				where x.BranchID == (int?)LoginDetails.BranchID
				select x.CostCentersNo).DefaultIfEmpty(0).Max()) + 1;
			Account_CostCenters account_CostCenters2 = new Account_CostCenters();
			account_CostCenters2.CostCentersID = num;
			account_CostCenters2.CostCentersNo = CostCenterNum;
			account_CostCenters2.CostCentersName = NameSon;
			account_CostCenters2.MainCostCentersID = FatherID;
			account_CostCenters2.Note = Note;
			account_CostCenters2.Priv_Credit = 0m;
			account_CostCenters2.Priv_Debit = 0m;
			account_CostCenters2.HideCost = HideCost;
			account_CostCenters2.IsDefaulte = CostDetaulte;
			account_CostCenters2.BranchID = LoginDetails.BranchID;
			account_CostCenters2.UserID_Add = LoginDetails.UserID;
			account_CostCenters2.UserBranch_Add = LoginDetails.BranchID;
			account_CostCenters2.UserMacAddress_Add = LoginDetails.macAddress;
			account_CostCenters2.UserDate_Add = DateTime.Now;
			db.Account_CostCenters.Add(account_CostCenters2);
			db.SaveChanges();
			InsertProjectCost(num, ProjectID);
			result = num2;
		}
		return result;
	}

	public int Update(int ID, string Name, int CostCenterNum, int FatherID, int ProjectID, string Note, bool HideCost, bool CostDetaulte)
	{
		int num = 0;
		Account_CostCenters account_CostCenters = db.Account_CostCenters.Where((Account_CostCenters x) => x.CostCentersID == (int?)ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Account_CostCenters account_CostCenters2 = db.Account_CostCenters.Where((Account_CostCenters x) => x.CostCentersName == Name && x.CostCentersID != (int?)ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (account_CostCenters != null && account_CostCenters2 == null)
		{
			account_CostCenters.CostCentersName = Name;
			account_CostCenters.CostCentersNo = CostCenterNum;
			account_CostCenters.MainCostCentersID = FatherID;
			account_CostCenters.Note = Note;
			account_CostCenters.HideCost = HideCost;
			account_CostCenters.IsDefaulte = CostDetaulte;
			account_CostCenters.UserID_Update = LoginDetails.UserID;
			account_CostCenters.UserBranch_Update = LoginDetails.GroupID;
			account_CostCenters.UserMacAddress_Update = LoginDetails.macAddress;
			account_CostCenters.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			UpdateProjectCost(ID, ProjectID);
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
		Account_CostCenters account_CostCenters = db.Account_CostCenters.Where((Account_CostCenters x) => x.CostCentersID == (int?)Sn && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Order_Orders order_Orders = db.Order_Orders.Where((Order_Orders x) => x.CostCentersID == (int?)Sn && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Tran_TranDetails tran_TranDetails = db.Tran_TranDetails.Where((Tran_TranDetails x) => x.CostCentersID == (int?)Sn && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (account_CostCenters != null && order_Orders == null && tran_TranDetails == null)
		{
			DeleteProjectCost(Sn);
			db.Account_CostCenters.Attach(account_CostCenters);
			db.Account_CostCenters.Remove(account_CostCenters);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetName(int sn)
	{
		Account_CostCenters account_CostCenters = db.Account_CostCenters.Where((Account_CostCenters x) => x.CostCentersID == (int?)sn).FirstOrDefault();
		return account_CostCenters.CostCentersName;
	}

	public string GetNameProJect(int sn)
	{
		return db.Account_Projects.Where((Account_Projects x) => x.ProjectID == (int?)sn).FirstOrDefault()?.ProjectName;
	}
}


