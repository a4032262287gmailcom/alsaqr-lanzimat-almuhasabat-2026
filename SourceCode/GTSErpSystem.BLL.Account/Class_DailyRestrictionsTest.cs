using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.Account;

public class Class_DailyRestrictionsTest
{
	private int Num;

	private int Index;

	private DataTable dtCurrentTable = new DataTable();

	private GTSdbEntities db = new GTSdbEntities();

	public List<Select_SearchAccountTranTempDetailsByCode_Result> GetSearchTranTempDetails(int Code)
	{
		return db.Select_SearchAccountTranTempDetailsByCode(Code).ToList();
	}

	public List<Select_SearchAccountTranTemp_Result> GetSearchTranTemp()
	{
		return db.Select_SearchAccountTranTemp(Convert.ToInt32(LoginDetails.BranchID)).ToList();
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

	public int InsertTranTemp(out int TRNCode, string NoteNum, DateTime PurDate, string Note)
	{
		int num = 0;
		TRNCode = 0;
		int iD = Convert.ToInt32(db.Tran_TranTemp.Select((Tran_TranTemp p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Tran_TranTemp
			where p.TranTypeID == (int?)1 && p.BranchID == (int?)LoginDetails.BranchID
			select p.ReferenceCode).DefaultIfEmpty(0).Max()) + 1;
		Tran_TranTemp tran_TranTemp = new Tran_TranTemp();
		tran_TranTemp.ID = iD;
		tran_TranTemp.ReferenceCode = num2;
		tran_TranTemp.TranTypeID = 1;
		tran_TranTemp.DocCode = NoteNum;
		tran_TranTemp.TranDate = PurDate;
		tran_TranTemp.BranchID = LoginDetails.BranchID;
		tran_TranTemp.Note = Note;
		tran_TranTemp.UserID_Add = LoginDetails.UserID;
		tran_TranTemp.UserBranch_Add = LoginDetails.BranchID;
		tran_TranTemp.UserMacAddress_Add = LoginDetails.macAddress;
		tran_TranTemp.UserDate_Add = DateTime.Now;
		db.Tran_TranTemp.Add(tran_TranTemp);
		num = db.SaveChanges();
		TRNCode = num2;
		return num;
	}

	public int InsertItemsTemp(DataTable Items, int TraNCodeFatora, bool ISEdit)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["رقم الحساب"].ToString() != "")
			{
				Index = 1;
				Tran_TranTempDetails tran_TranTempDetails = new Tran_TranTempDetails();
				tran_TranTempDetails.TranSn = TraNCodeFatora;
				tran_TranTempDetails.Account_Sn = Convert.ToInt32(row["AccountID"].ToString());
				tran_TranTempDetails.AccounIindex = Index;
				tran_TranTempDetails.Debit = Convert.ToDecimal(row["المدين"].ToString());
				tran_TranTempDetails.Credit = Convert.ToDecimal(row["الدائن"].ToString());
				tran_TranTempDetails.BranchID = LoginDetails.BranchID;
				tran_TranTempDetails.ProjectId = Convert.ToInt32(row["رقم المشروع"].ToString());
				tran_TranTempDetails.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranTempDetails.TranDesc = row["البيان"].ToString() ?? "";
				}
				else
				{
					tran_TranTempDetails.TranDesc = row["البيان"].ToString() + " معدلة ";
				}
				db.Tran_TranTempDetails.Add(tran_TranTempDetails);
				result = db.SaveChanges();
			}
		}
		return result;
	}

	public int UpdateTranTemp(int TRNCode, string NoteNum, DateTime PurDate, string Note)
	{
		int result = 0;
		Tran_TranTemp tran_TranTemp = db.Tran_TranTemp.Where((Tran_TranTemp x) => x.ReferenceCode == (int?)TRNCode && x.TranTypeID == (int?)1 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (tran_TranTemp != null)
		{
			tran_TranTemp.DocCode = NoteNum;
			tran_TranTemp.TranDate = PurDate;
			tran_TranTemp.BranchID = LoginDetails.BranchID;
			tran_TranTemp.Note = Note + " معدلة ";
			tran_TranTemp.UserID_Update = LoginDetails.UserID;
			tran_TranTemp.UserBranch_Update = LoginDetails.GroupID;
			tran_TranTemp.UserMacAddress_Update = LoginDetails.macAddress;
			tran_TranTemp.UserDate_Update = DateTime.Now;
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteTranTemp(int Code)
	{
		int result = 0;
		int TranID = db.Tran_TranTemp.Where((Tran_TranTemp x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)1 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		Tran_TranTemp tran_TranTemp = db.Tran_TranTemp.Where((Tran_TranTemp x) => x.ID == TranID).FirstOrDefault();
		if (tran_TranTemp != null)
		{
			db.Tran_TranTemp.Attach(tran_TranTemp);
			db.Tran_TranTemp.Remove(tran_TranTemp);
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteTranDetailsTemp(int Code)
	{
		int result = 0;
		int TranID = db.Tran_TranTemp.Where((Tran_TranTemp x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)1 && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		List<Tran_TranTempDetails> list = db.Tran_TranTempDetails.Where((Tran_TranTempDetails x) => x.TranSn == (int?)TranID).ToList();
		if (list != null)
		{
			foreach (Tran_TranTempDetails item in list)
			{
				db.Tran_TranTempDetails.Attach(item);
				db.Tran_TranTempDetails.Remove(item);
			}
			result = db.SaveChanges();
		}
		return result;
	}
}


