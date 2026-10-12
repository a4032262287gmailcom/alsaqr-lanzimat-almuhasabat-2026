using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Orders;

internal class Class_Transporation
{
	private GTSdbEntities db = new GTSdbEntities();

	public int InsertTran(int PurID, int SuppNum, string Name, DateTime PurDate, int CostCenter, string NoteNum, decimal TotalSell, decimal Vat, decimal NetCash)
	{
		int num = 0;
		int num2 = Convert.ToInt32(db.Tran_Tran.Select((Tran_Tran p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		Tran_Tran tran_Tran = new Tran_Tran();
		tran_Tran.ID = num2;
		tran_Tran.ReferenceCode = PurID;
		tran_Tran.TranTypeID = 10;
		tran_Tran.DocCode = NoteNum;
		tran_Tran.TranDate = PurDate;
		tran_Tran.BranchID = LoginDetails.BranchID;
		tran_Tran.Note = " سند استلام سيارة رقم  ( " + PurID + " ) ط­ /  " + Name;
		tran_Tran.UserID_Add = LoginDetails.UserID;
		tran_Tran.UserBranch_Add = LoginDetails.BranchID;
		tran_Tran.UserMacAddress_Add = LoginDetails.macAddress;
		tran_Tran.UserDate_Add = DateTime.Now;
		db.Tran_Tran.Add(tran_Tran);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = num2;
		tran_TranDetails.Account_Sn = SuppNum;
		tran_TranDetails.AccounIindex = 1;
		tran_TranDetails.TranDesc = "  سند استلام سيارة رقم ( " + PurID + " ) ";
		tran_TranDetails.Debit = TotalSell + Vat;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = CostCenter;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
		tran_TranDetails2.TranSn = num2;
		tran_TranDetails2.Account_Sn = LoginDetails.Acc_OrderAjila;
		tran_TranDetails2.AccounIindex = 1;
		tran_TranDetails2.TranDesc = "  سند استلام سيارة رقم  ( " + PurID + " ) ط­/ " + Name;
		tran_TranDetails2.Debit = 0m;
		tran_TranDetails2.Credit = TotalSell;
		tran_TranDetails2.CostCentersID = CostCenter;
		tran_TranDetails2.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails2);
		db.SaveChanges();
		if (Vat > 0m)
		{
			Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
			tran_TranDetails3.TranSn = num2;
			tran_TranDetails3.Account_Sn = LoginDetails.Acc_VatNum;
			tran_TranDetails3.AccounIindex = 0;
			tran_TranDetails3.TranDesc = "  سند استلام سيارة رقم  ( " + PurID + " ) ط­/ " + Name + " ";
			tran_TranDetails3.Debit = 0m;
			tran_TranDetails3.Credit = Vat;
			tran_TranDetails3.CostCentersID = CostCenter;
			tran_TranDetails3.BranchID = LoginDetails.BranchID;
			db.Tran_TranDetails.Add(tran_TranDetails3);
			db.SaveChanges();
		}
		Tran_TranDetails tran_TranDetails4 = new Tran_TranDetails();
		tran_TranDetails4.TranSn = num2;
		tran_TranDetails4.Account_Sn = LoginDetails.Acc_CashBox;
		tran_TranDetails4.AccounIindex = 1;
		tran_TranDetails4.Debit = NetCash;
		tran_TranDetails4.Credit = 0m;
		tran_TranDetails4.CostCentersID = CostCenter;
		tran_TranDetails4.BranchID = LoginDetails.BranchID;
		tran_TranDetails4.TranDesc = "  سند استلام سيارة رقم  ( " + PurID + " ) ط­/ " + Name + " ";
		db.Tran_TranDetails.Add(tran_TranDetails4);
		db.SaveChanges();
		Tran_TranDetails tran_TranDetails5 = new Tran_TranDetails();
		tran_TranDetails5.TranSn = num2;
		tran_TranDetails5.Account_Sn = SuppNum;
		tran_TranDetails5.AccounIindex = 2;
		tran_TranDetails5.Debit = 0m;
		tran_TranDetails5.Credit = NetCash;
		tran_TranDetails5.CostCentersID = CostCenter;
		tran_TranDetails5.BranchID = LoginDetails.BranchID;
		tran_TranDetails5.TranDesc = "  سند استلام سيارة رقم  ( " + PurID + " ) ط­/ " + Name + " ";
		db.Tran_TranDetails.Add(tran_TranDetails5);
		return db.SaveChanges();
	}

	public int DeleteTran(int Code, int TranTypeID)
	{
		DeleteTranDetails(Code, TranTypeID);
		int result = 0;
		int TranID = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ID == TranID).FirstOrDefault();
		if (tran_Tran != null)
		{
			db.Tran_Tran.Attach(tran_Tran);
			db.Tran_Tran.Remove(tran_Tran);
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteTranDetails(int Code, int TranTypeID)
	{
		int result = 0;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		int TranID = tran_Tran.ID;
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


