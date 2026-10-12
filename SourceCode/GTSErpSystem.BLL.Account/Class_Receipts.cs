using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.Account;

public class Class_Receipts
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

	public List<CashReceiptsType> GetCashReceiptsType()
	{
		return db.CashReceiptsTypes.ToList();
	}

	public List<Select_SearchAccountCostCenter_Result> GetCostCenters()
	{
		return db.Select_SearchAccountCostCenter(Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<Select_Search_Account_Receipts_Result> GetSearchAccount_Receipts(int ID)
	{
		return db.Select_Search_Account_Receipts(ID, Convert.ToInt32(LoginDetails.BranchID)).ToList();
	}

	public List<Account_Receipts> GetALLReceipts()
	{
		return db.Account_Receipts.Where((Account_Receipts x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Account_ReceiptsDetails> GetALLReceiptsDetails()
	{
		return db.Account_ReceiptsDetails.Where((Account_ReceiptsDetails x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int GetEditRecipt(int Code, int TranTypeID)
	{
		int result = 0;
		Account_Receipts account_Receipts = db.Account_Receipts.Where((Account_Receipts x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)TranTypeID && x.ISCheckDone != (bool?)true && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (account_Receipts != null)
		{
			result = 1;
		}
		return result;
	}

	public int Insert(out int TRNCode, string NoteNum, int CashReceiptsTypeID, string ReceivedFrom, DateTime PurDate, string Note, decimal money, int TranTypeID, int ProjectId)
	{
		int num = 0;
		TRNCode = 0;
		int iD = Convert.ToInt32(db.Account_Receipts.Select((Account_Receipts p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Account_Receipts
			where p.TranTypeID == (int?)TranTypeID && p.BranchID == (int?)LoginDetails.BranchID
			select p.ReferenceCode).DefaultIfEmpty(0).Max()) + 1;
		Account_Receipts account_Receipts = new Account_Receipts();
		account_Receipts.ID = iD;
		account_Receipts.ReferenceCode = num2;
		account_Receipts.TranTypeID = TranTypeID;
		account_Receipts.DocCode = NoteNum;
		account_Receipts.TranDate = PurDate;
		account_Receipts.BranchID = LoginDetails.BranchID;
		account_Receipts.Note = Note;
		account_Receipts.Money = money;
		account_Receipts.ProjectId = ProjectId;
		account_Receipts.ReceivedFrom = ReceivedFrom;
		account_Receipts.CashReceiptsTypeID = CashReceiptsTypeID;
		account_Receipts.UserID_Add = LoginDetails.UserID;
		account_Receipts.UserBranch_Add = LoginDetails.BranchID;
		account_Receipts.UserMacAddress_Add = LoginDetails.macAddress;
		account_Receipts.UserDate_Add = DateTime.Now;
		db.Account_Receipts.Add(account_Receipts);
		db.SaveChanges();
		num = num2;
		TRNCode = num2;
		return num;
	}

	public int InsertItemsReceipts(int TranSn, int Account_Sn1, int Account_Sn2, string TranDesc, decimal Money, int CostCentersID, int TranTypeID, int ProjectId)
	{
		int iD = db.Account_Receipts.Where((Account_Receipts x) => x.ReferenceCode == (int?)TranSn && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		int num = 0;
		Account_ReceiptsDetails account_ReceiptsDetails = new Account_ReceiptsDetails();
		account_ReceiptsDetails.TranSn = iD;
		account_ReceiptsDetails.Account_Sn = Account_Sn1;
		account_ReceiptsDetails.AccounIindex = 1;
		account_ReceiptsDetails.TranDesc = TranDesc;
		account_ReceiptsDetails.Debit = Money;
		account_ReceiptsDetails.Credit = 0m;
		account_ReceiptsDetails.ProjectId = ProjectId;
		account_ReceiptsDetails.CostCentersID = CostCentersID;
		account_ReceiptsDetails.BranchID = LoginDetails.BranchID;
		db.Account_ReceiptsDetails.Add(account_ReceiptsDetails);
		db.SaveChanges();
		Account_ReceiptsDetails account_ReceiptsDetails2 = new Account_ReceiptsDetails();
		account_ReceiptsDetails2.TranSn = iD;
		account_ReceiptsDetails2.Account_Sn = Account_Sn2;
		account_ReceiptsDetails2.AccounIindex = 2;
		account_ReceiptsDetails2.TranDesc = TranDesc;
		account_ReceiptsDetails2.Debit = 0m;
		account_ReceiptsDetails2.Credit = Money;
		account_ReceiptsDetails.ProjectId = ProjectId;
		account_ReceiptsDetails2.CostCentersID = CostCentersID;
		account_ReceiptsDetails2.BranchID = LoginDetails.BranchID;
		db.Account_ReceiptsDetails.Add(account_ReceiptsDetails2);
		return db.SaveChanges();
	}

	public int InsertItemsReceipts(int AccountNum, DataTable Items, decimal Debit, decimal Credit, string TranDesc, int CostCentersID, int TraNCodeFatora, bool ISEdit, int TranTypeID, int ProjectId, decimal Vats)
	{
		int result = 0;
		int iD = db.Account_Receipts.Where((Account_Receipts x) => x.ReferenceCode == (int?)TraNCodeFatora && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		Account_ReceiptsDetails account_ReceiptsDetails = new Account_ReceiptsDetails();
		account_ReceiptsDetails.TranSn = iD;
		account_ReceiptsDetails.Account_Sn = AccountNum;
		account_ReceiptsDetails.AccounIindex = 1;
		account_ReceiptsDetails.TranDesc = TranDesc;
		account_ReceiptsDetails.Debit = Debit;
		account_ReceiptsDetails.Credit = Credit;
		account_ReceiptsDetails.ProjectId = ProjectId;
		account_ReceiptsDetails.CostCentersID = CostCentersID;
		if (Vats > 0m)
		{
			account_ReceiptsDetails.IsVat = true;
		}
		else
		{
			account_ReceiptsDetails.IsVat = false;
		}
		account_ReceiptsDetails.Vat = 0m;
		account_ReceiptsDetails.Total = 0m;
		account_ReceiptsDetails.VatNum = 0;
		account_ReceiptsDetails.BranchID = LoginDetails.BranchID;
		db.Account_ReceiptsDetails.Add(account_ReceiptsDetails);
		db.SaveChanges();
		Index = 2;
		foreach (DataRow row in Items.Rows)
		{
			if (Convert.ToBoolean(row["تفعيل الضريبه"]))
			{
				if (row["رقم الحساب"].ToString() != "")
				{
					Account_ReceiptsDetails entity = new Account_ReceiptsDetails
					{
						TranSn = iD,
						Account_Sn = Convert.ToInt32(row["AccountID"].ToString()),
						AccounIindex = Index,
						Debit = Convert.ToDecimal(row["المدين"].ToString()),
						Credit = Convert.ToDecimal(row["الدائن"].ToString()),
						BranchID = LoginDetails.BranchID,
						ProjectId = Convert.ToInt32(row["رقم المشروع"].ToString()),
						CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString()),
						IsVat = Convert.ToBoolean(row["تفعيل الضريبه"].ToString()),
						Vat = Convert.ToDecimal(row["الضريبه"].ToString()),
						Total = Convert.ToDecimal(row["الاجمالي"].ToString()),
						TranDesc = (ISEdit ? row["البيان"].ToString() : (row["البيان"].ToString() ?? "")),
						VatNum = LoginDetails.Acc_VatNum
					};
					db.Account_ReceiptsDetails.Add(entity);
					result = db.SaveChanges();
					Index++;
				}
			}
			else if (row["رقم الحساب"].ToString() != "")
			{
				Account_ReceiptsDetails account_ReceiptsDetails2 = new Account_ReceiptsDetails();
				account_ReceiptsDetails2.TranSn = iD;
				account_ReceiptsDetails2.Account_Sn = Convert.ToInt32(row["AccountID"].ToString());
				account_ReceiptsDetails2.AccounIindex = Index;
				account_ReceiptsDetails2.Debit = Convert.ToDecimal(row["المدين"].ToString());
				account_ReceiptsDetails2.Credit = Convert.ToDecimal(row["الدائن"].ToString());
				account_ReceiptsDetails2.BranchID = LoginDetails.BranchID;
				account_ReceiptsDetails2.ProjectId = Convert.ToInt32(row["رقم المشروع"].ToString());
				account_ReceiptsDetails2.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				account_ReceiptsDetails2.IsVat = Convert.ToBoolean(row["تفعيل الضريبه"].ToString());
				account_ReceiptsDetails2.Vat = Convert.ToDecimal(row["الضريبه"].ToString());
				account_ReceiptsDetails2.Total = Convert.ToDecimal(row["الاجمالي"].ToString());
				if (!ISEdit)
				{
					account_ReceiptsDetails2.TranDesc = row["البيان"].ToString() ?? "";
				}
				else
				{
					account_ReceiptsDetails2.TranDesc = row["البيان"].ToString();
				}
				account_ReceiptsDetails2.VatNum = 0;
				db.Account_ReceiptsDetails.Add(account_ReceiptsDetails2);
				result = db.SaveChanges();
				Index++;
			}
		}
		return result;
	}

	public int Update(int TRNCode, string NoteNum, int CashReceiptsTypeID, string ReceivedFrom, DateTime PurDate, string Note, decimal money, int TranTypeID, int ProjectId)
	{
		int result = 0;
		Account_Receipts account_Receipts = db.Account_Receipts.Where((Account_Receipts x) => x.ReferenceCode == (int?)TRNCode && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (account_Receipts != null)
		{
			account_Receipts.DocCode = NoteNum;
			account_Receipts.TranDate = PurDate;
			account_Receipts.BranchID = LoginDetails.BranchID;
			account_Receipts.Note = Note;
			account_Receipts.Money = money;
			account_Receipts.ReceivedFrom = ReceivedFrom;
			account_Receipts.CashReceiptsTypeID = CashReceiptsTypeID;
			account_Receipts.ProjectId = ProjectId;
			account_Receipts.UserID_Update = LoginDetails.UserID;
			account_Receipts.UserBranch_Update = LoginDetails.GroupID;
			account_Receipts.UserMacAddress_Update = LoginDetails.macAddress;
			account_Receipts.UserDate_Update = DateTime.Now;
			result = db.SaveChanges();
		}
		return result;
	}

	public int Delete(int Code, int TranTypeID)
	{
		int result = 0;
		Account_Receipts account_Receipts = db.Account_Receipts.Where((Account_Receipts x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (account_Receipts != null)
		{
			db.Account_Receipts.Attach(account_Receipts);
			db.Account_Receipts.Remove(account_Receipts);
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteDetails(int Code, int TranTypeID)
	{
		int result = 0;
		int TranID = db.Account_Receipts.Where((Account_Receipts x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		List<Account_ReceiptsDetails> list = db.Account_ReceiptsDetails.Where((Account_ReceiptsDetails x) => x.TranSn == (int?)TranID).ToList();
		if (list != null)
		{
			foreach (Account_ReceiptsDetails item in list)
			{
				db.Account_ReceiptsDetails.Attach(item);
				db.Account_ReceiptsDetails.Remove(item);
			}
			result = db.SaveChanges();
		}
		return result;
	}

	public int InsertTran(out int TRNCode, int RcpCode, string NoteNum, DateTime PurDate, string Note, int TranTypeID)
	{
		int num = 0;
		TRNCode = 0;
		int num2 = Convert.ToInt32(db.Tran_Tran.Select((Tran_Tran p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		Tran_Tran tran_Tran = new Tran_Tran();
		tran_Tran.ID = num2;
		tran_Tran.ReferenceCode = RcpCode;
		tran_Tran.TranTypeID = TranTypeID;
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

	public int InsertItems(int TranSn, int Account_Sn1, int Account_Sn2, string TranDesc, decimal Money, int CostCentersID, bool IsEdit, int ProjectId)
	{
		int num = 0;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = TranSn;
		tran_TranDetails.Account_Sn = Account_Sn1;
		tran_TranDetails.AccounIindex = 1;
		tran_TranDetails.Debit = Money;
		tran_TranDetails.Credit = 0m;
		tran_TranDetails.CostCentersID = 0;
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

	public int InsertItemsPayment(int TranSn, int Account_Sn1, int Account_Sn2, string TranDesc, decimal Money, int CostCentersID, bool IsEdit, int ProjectId)
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
		tran_TranDetails2.CostCentersID = 0;
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

	public int InsertItems(int AccountNum, DataTable Items, decimal Debit, decimal Credit, string TranDesc, int CostCentersID, int TraNCodeFatora, bool ISEdit, int TranTypeID, int ProjectId, string TranDesc2)
	{
		int result = 0;
		int iD = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)TraNCodeFatora && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault().ID;
		Tran_TranDetails tran_TranDetails = new Tran_TranDetails();
		tran_TranDetails.TranSn = iD;
		tran_TranDetails.Account_Sn = AccountNum;
		tran_TranDetails.AccounIindex = 1;
		tran_TranDetails.TranDesc = TranDesc;
		tran_TranDetails.Debit = Debit;
		tran_TranDetails.Credit = Credit;
		tran_TranDetails.CostCentersID = CostCentersID;
		tran_TranDetails.BranchID = LoginDetails.BranchID;
		db.Tran_TranDetails.Add(tran_TranDetails);
		db.SaveChanges();
		Index = 2;
		foreach (DataRow row in Items.Rows)
		{
			if (Convert.ToBoolean(row["تفعيل الضريبه"]))
			{
				if (row["رقم الحساب"].ToString() != "")
				{
					Tran_TranDetails tran_TranDetails2 = new Tran_TranDetails();
					tran_TranDetails2.TranSn = iD;
					tran_TranDetails2.Account_Sn = Convert.ToInt32(row["AccountID"].ToString());
					tran_TranDetails2.AccounIindex = Index;
					tran_TranDetails2.Debit = Convert.ToDecimal(row["المدين"].ToString());
					tran_TranDetails2.Credit = Convert.ToDecimal(row["الدائن"].ToString());
					tran_TranDetails2.BranchID = LoginDetails.BranchID;
					tran_TranDetails2.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
					if (!ISEdit)
					{
						tran_TranDetails2.TranDesc = row["البيان"].ToString() ?? "";
					}
					else
					{
						tran_TranDetails2.TranDesc = row["البيان"].ToString();
					}
					db.Tran_TranDetails.Add(tran_TranDetails2);
					result = db.SaveChanges();
				}
				if (Debit == 0m)
				{
					Tran_TranDetails entity = new Tran_TranDetails
					{
						TranSn = iD,
						Account_Sn = LoginDetails.Acc_VatNum,
						AccounIindex = Index,
						Debit = Convert.ToDecimal(row["الضريبه"].ToString()),
						Credit = 0m,
						BranchID = LoginDetails.BranchID,
						CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString()),
						TranDesc = TranDesc2 + row["اسم الحساب"].ToString()
					};
					Index++;
					db.Tran_TranDetails.Add(entity);
					result = db.SaveChanges();
				}
				else
				{
					Tran_TranDetails entity2 = new Tran_TranDetails
					{
						TranSn = iD,
						Account_Sn = LoginDetails.Acc_VatNum,
						AccounIindex = Index,
						Debit = 0m,
						Credit = Convert.ToDecimal(row["الضريبه"].ToString()),
						BranchID = LoginDetails.BranchID,
						CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString()),
						TranDesc = TranDesc2 + row["اسم الحساب"].ToString()
					};
					Index++;
					db.Tran_TranDetails.Add(entity2);
					result = db.SaveChanges();
				}
			}
			else if (row["رقم الحساب"].ToString() != "")
			{
				Tran_TranDetails tran_TranDetails3 = new Tran_TranDetails();
				tran_TranDetails3.TranSn = iD;
				tran_TranDetails3.Account_Sn = Convert.ToInt32(row["AccountID"].ToString());
				tran_TranDetails3.AccounIindex = Index;
				tran_TranDetails3.Debit = Convert.ToDecimal(row["المدين"].ToString());
				tran_TranDetails3.Credit = Convert.ToDecimal(row["الدائن"].ToString());
				tran_TranDetails3.BranchID = LoginDetails.BranchID;
				tran_TranDetails3.CostCentersID = Convert.ToInt32(row["مركز التكلفة"].ToString());
				if (!ISEdit)
				{
					tran_TranDetails3.TranDesc = row["البيان"].ToString() ?? "";
				}
				else
				{
					tran_TranDetails3.TranDesc = row["البيان"].ToString();
				}
				db.Tran_TranDetails.Add(tran_TranDetails3);
				result = db.SaveChanges();
				Index++;
			}
		}
		return result;
	}

	public int UpdateTran(int TRNCode, string NoteNum, DateTime PurDate, string Note, int TranTypeID)
	{
		int result = 0;
		Tran_Tran tran_Tran = db.Tran_Tran.Where((Tran_Tran x) => x.ReferenceCode == (int?)TRNCode && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
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

	public int DeleteTran(int Code, int TranTypeID)
	{
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

	public string GetAccountName(int AccountID)
	{
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.ID == AccountID).FirstOrDefault();
		return account_Accounts.Account_Name;
	}

	public int GetAccountNum(int AccountID)
	{
		Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.ID == AccountID).FirstOrDefault();
		return Convert.ToInt32(account_Accounts.Account_No);
	}

	public int InsertCheck(out int TRNCode, string NoteNum, string ReceivedFrom, DateTime PurDate, string Note, decimal money, string CheckID, DateTime DateDue, int TranTypeID, int ProjectId)
	{
		int num = 0;
		TRNCode = 0;
		int iD = Convert.ToInt32(db.Account_Receipts.Select((Account_Receipts p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		int num2 = Convert.ToInt32((from p in db.Account_Receipts
			where p.TranTypeID == (int?)TranTypeID && p.BranchID == (int?)LoginDetails.BranchID
			select p.ReferenceCode).DefaultIfEmpty(0).Max()) + 1;
		Account_Receipts account_Receipts = new Account_Receipts();
		account_Receipts.ID = iD;
		account_Receipts.ReferenceCode = num2;
		account_Receipts.TranTypeID = TranTypeID;
		account_Receipts.DocCode = NoteNum;
		account_Receipts.TranDate = PurDate;
		account_Receipts.BranchID = LoginDetails.BranchID;
		account_Receipts.Note = Note;
		account_Receipts.Money = money;
		account_Receipts.ReceivedFrom = ReceivedFrom;
		account_Receipts.CashReceiptsTypeID = 3;
		account_Receipts.CheckID = CheckID;
		account_Receipts.DateDue = DateDue;
		account_Receipts.ProjectId = ProjectId;
		account_Receipts.UserID_Add = LoginDetails.UserID;
		account_Receipts.UserBranch_Add = LoginDetails.BranchID;
		account_Receipts.UserMacAddress_Add = LoginDetails.macAddress;
		account_Receipts.UserDate_Add = DateTime.Now;
		db.Account_Receipts.Add(account_Receipts);
		db.SaveChanges();
		num = num2;
		TRNCode = num2;
		return num;
	}

	public int UpdateCheck(int TRNCode, string NoteNum, string ReceivedFrom, DateTime PurDate, string Note, decimal money, string CheckID, DateTime DateDue, int TranTypeID, int ProjectId)
	{
		int result = 0;
		Account_Receipts account_Receipts = db.Account_Receipts.Where((Account_Receipts x) => x.ReferenceCode == (int?)TRNCode && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (account_Receipts != null)
		{
			account_Receipts.DocCode = NoteNum;
			account_Receipts.TranDate = PurDate;
			account_Receipts.BranchID = LoginDetails.BranchID;
			account_Receipts.Note = Note;
			account_Receipts.Money = money;
			account_Receipts.ReceivedFrom = ReceivedFrom;
			account_Receipts.CheckID = CheckID;
			account_Receipts.DateDue = DateDue;
			account_Receipts.ProjectId = ProjectId;
			account_Receipts.UserID_Update = LoginDetails.UserID;
			account_Receipts.UserBranch_Update = LoginDetails.GroupID;
			account_Receipts.UserMacAddress_Update = LoginDetails.macAddress;
			account_Receipts.UserDate_Update = DateTime.Now;
			result = db.SaveChanges();
		}
		return result;
	}

	public int UpdateCheckCollection(int TRNCode, string NoteNum, string ReceivedFrom, DateTime PurDate, string Note, decimal money, string CheckID, DateTime DateDue, bool CheckType, int TranTypeID)
	{
		int result = 0;
		Account_Receipts account_Receipts = db.Account_Receipts.Where((Account_Receipts x) => x.ReferenceCode == (int?)TRNCode && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (account_Receipts != null)
		{
			account_Receipts.DocCode = NoteNum;
			account_Receipts.TranDate = PurDate;
			account_Receipts.BranchID = LoginDetails.BranchID;
			account_Receipts.Note = Note;
			account_Receipts.Money = money;
			account_Receipts.ReceivedFrom = ReceivedFrom;
			account_Receipts.CheckID = CheckID;
			account_Receipts.DateDue = DateDue;
			account_Receipts.CheckType = CheckType;
			account_Receipts.ISCheckDone = true;
			account_Receipts.UserID_Update = LoginDetails.UserID;
			account_Receipts.UserBranch_Update = LoginDetails.GroupID;
			account_Receipts.UserMacAddress_Update = LoginDetails.macAddress;
			account_Receipts.UserDate_Update = DateTime.Now;
			result = db.SaveChanges();
		}
		return result;
	}

	public int DeleteCollection(int Code, int TranTypeID)
	{
		int result = 0;
		Account_Receipts account_Receipts = db.Account_Receipts.Where((Account_Receipts x) => x.ReferenceCode == (int?)Code && x.TranTypeID == (int?)TranTypeID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (account_Receipts != null)
		{
			account_Receipts.CheckType = false;
			account_Receipts.ISCheckDone = false;
			result = db.SaveChanges();
		}
		return result;
	}
}


