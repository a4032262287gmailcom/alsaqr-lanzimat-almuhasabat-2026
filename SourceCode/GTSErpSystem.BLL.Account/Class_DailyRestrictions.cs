using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.Account;

public class Class_DailyRestrictions
{
	private int Num;

	private int Index;

	private DataTable dtCurrentTable = new DataTable();

	private GTSdbEntities db = new GTSdbEntities();

	public List<Select_SearchAccountTranDetailsByCode_Result> GetSearchTranDetails(int Code)
	{
		return db.Select_SearchAccountTranDetailsByCode(Code).ToList();
	}

	public List<Select_SearchAccountTran_Result> GetSearchTran()
	{
		return db.Select_SearchAccountTran(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<Select_SearchAccount_Result> GetSearchAccount()
	{
		return db.Select_SearchAccount().ToList();
	}

	public List<Select_SearchAccount_ByAccountNumber_Result> GetSearchAccountByAccountNum(string ID)
	{
		try
		{
			Num = Convert.ToInt32(ID);
		}
		catch (Exception)
		{
			Num = 0;
		}
		return db.Select_SearchAccount_ByAccountNumber(Num).ToList();
	}

	public List<Select_SearchAccount_ByAccountNumber_Result> GetSearchAccountByAccountNum(int ID)
	{
		return db.Select_SearchAccount_ByAccountNumber(ID).ToList();
	}

	public int InsertTran(out int TRNCode, out int TxtCode, string NoteNum, DateTime PurDate, string Note)
	{
		int num = 0;
		TRNCode = 0;
		int num2 = Convert.ToInt32(db.Tran_Tran.Select((Tran_Tran p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num3 = Convert.ToInt32((from p in db.Tran_Tran
			where p.TranTypeID == (int?)1 && p.BranchID == (int?)LoginDetails.BranchID
			select p.ReferenceCode).DefaultIfEmpty(0).Max()) + 1;
		Tran_Tran tran_Tran = new Tran_Tran();
		tran_Tran.ID = num2;
		tran_Tran.ReferenceCode = num3;
		tran_Tran.TranTypeID = 1;
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
		TxtCode = num3;
		return num;
	}

	public int InsertItems(DataTable Items, int TraNCodeFatora, bool ISEdit)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["رقم الحساب"].ToString() != "")
			{
				GTSdbEntities gTSdbEntities = new GTSdbEntities();
				Index = 1;
				Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
				tran_TranDetails.TranSn = TraNCodeFatora;
				tran_TranDetails.Account_Sn = Convert.ToInt32(row["AccountID"].ToString());
				tran_TranDetails.AccounIindex = Index;
				tran_TranDetails.Debit = Convert.ToDecimal(row["المدين"].ToString());
				tran_TranDetails.Credit = Convert.ToDecimal(row["الدائن"].ToString());
				tran_TranDetails.BranchID = LoginDetails.BranchID;
				tran_TranDetails.ProjectId = Convert.ToInt32(row["رقم المشروع"].ToString());
				tran_TranDetails.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails.TranDesc = row["البيان"].ToString() ?? "";
				}
				else
				{
					tran_TranDetails.TranDesc = row["البيان"].ToString() + " معدلة ";
				}
				db.Tran_TranDetails.Add(tran_TranDetails);
				result = db.SaveChanges();
			}
		}
		return result;
	}

	public int InsertItems(int TranSn, int Account_Sn1, int Account_Sn2, string TranDesc, decimal Money, int CostCentersID, bool IsEdit, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranSn;
		tran_TranDetails.Account_Sn = Account_Sn1;
		tran_TranDetails.AccounIindex = 1;
		tran_TranDetails.Debit = Money;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = CostCentersID;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		if (IsEdit)
		{
			tran_TranDetails.TranDesc = TranDesc ?? "";
		}
		else
		{
			tran_TranDetails.TranDesc = TranDesc;
		}
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = TranSn;
		tran_TranDetails2.Account_Sn = Account_Sn2;
		tran_TranDetails2.AccounIindex = 2;
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = Money;
		tran_TranDetails2.CostCentersID = CostCentersID;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		if (IsEdit)
		{
			tran_TranDetails2.TranDesc = TranDesc ?? "";
		}
		else
		{
			tran_TranDetails2.TranDesc = TranDesc;
		}
		db.Tran_TranDetails.Add(tran_TranDetails2);
		return db.SaveChanges();
	}

	public int UpdateTran(int TRNCode, string NoteNum, DateTime PurDate, string Note)
	{
		int result = 0;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)TRNCode && x.TranTypeID == (int?)1 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
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
			db.SaveChanges();
			result = tran_Tran.ID;
		}
		return result;
	}

	public int DeleteTran(int Code)
	{
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)1 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
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
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)1 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
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
}


