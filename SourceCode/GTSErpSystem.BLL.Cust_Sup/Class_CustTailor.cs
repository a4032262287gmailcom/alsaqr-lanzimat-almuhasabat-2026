using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Cust_Sup;

internal class Class_CustTailor
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Select_Account_CusTailor_Result> GetCustCode(int CustCode)
	{
		return db.Select_Account_CusTailor(CustCode).ToList();
	}

	public int InsertUpdate(int CustCode, string CollarH, string CollarW, string ShoulderH, string ShoulderW, string ChestH, string ChestW, string WaistH, string WaistW, string HipsH, string HipsW, string SleeveH, string SleeveW, string CuffH, string CuffW, string LongthH, string LongthW, string DownDressH, string DownDressW, string starlings, string chestpocket, string QuantumPlain, string QuantumCuff, string ButtonNumber, string Note)
	{
		int num = 0;
		Account_CustTailor account_CustTailor = db.Account_CustTailor.Where((Account_CustTailor x) => x.CustCode == (int?)CustCode).FirstOrDefault();
		if (account_CustTailor != null)
		{
			account_CustTailor.CustCode = CustCode;
			account_CustTailor.CollarH = CollarH;
			account_CustTailor.CollarW = CollarW;
			account_CustTailor.ShoulderH = ShoulderH;
			account_CustTailor.ShoulderW = ShoulderW;
			account_CustTailor.ChestH = ChestH;
			account_CustTailor.ChestW = ChestW;
			account_CustTailor.WaistH = WaistH;
			account_CustTailor.WaistW = WaistW;
			account_CustTailor.HipsH = HipsH;
			account_CustTailor.HipsW = HipsW;
			account_CustTailor.SleeveH = SleeveH;
			account_CustTailor.SleeveW = SleeveW;
			account_CustTailor.CuffH = CuffH;
			account_CustTailor.CuffW = CuffW;
			account_CustTailor.LongthH = LongthH;
			account_CustTailor.LongthW = LongthW;
			account_CustTailor.DownDressH = DownDressH;
			account_CustTailor.DownDressW = DownDressW;
			account_CustTailor.starlings = starlings;
			account_CustTailor.chestpocket = chestpocket;
			account_CustTailor.QuantumPlain = QuantumPlain;
			account_CustTailor.QuantumCuff = QuantumCuff;
			account_CustTailor.ButtonNumber = ButtonNumber;
			account_CustTailor.Note = Note;
			db.SaveChanges();
			return 1;
		}
		Account_CustTailor account_CustTailor2 = new Account_CustTailor();
		account_CustTailor2.CustCode = CustCode;
		account_CustTailor2.CollarH = CollarH;
		account_CustTailor2.CollarW = CollarW;
		account_CustTailor2.ShoulderH = ShoulderH;
		account_CustTailor2.ShoulderW = ShoulderW;
		account_CustTailor2.ChestH = ChestH;
		account_CustTailor2.ChestW = ChestW;
		account_CustTailor2.WaistH = WaistH;
		account_CustTailor2.WaistW = WaistW;
		account_CustTailor2.HipsH = HipsH;
		account_CustTailor2.HipsW = HipsW;
		account_CustTailor2.SleeveH = SleeveH;
		account_CustTailor2.SleeveW = SleeveW;
		account_CustTailor2.CuffH = CuffH;
		account_CustTailor2.CuffW = CuffW;
		account_CustTailor2.LongthH = LongthH;
		account_CustTailor2.LongthW = LongthW;
		account_CustTailor2.DownDressH = DownDressH;
		account_CustTailor2.DownDressW = DownDressW;
		account_CustTailor2.starlings = starlings;
		account_CustTailor2.chestpocket = chestpocket;
		account_CustTailor2.QuantumPlain = QuantumPlain;
		account_CustTailor2.QuantumCuff = QuantumCuff;
		account_CustTailor2.ButtonNumber = ButtonNumber;
		account_CustTailor2.Note = Note;
		db.Account_CustTailor.Add(account_CustTailor2);
		db.SaveChanges();
		return 1;
	}

	public int Delete(int CustCode)
	{
		int result = 0;
		Account_CustTailor account_CustTailor = db.Account_CustTailor.Where((Account_CustTailor x) => x.CustCode == (int?)CustCode).FirstOrDefault();
		if (account_CustTailor != null)
		{
			db.Account_CustTailor.Attach(account_CustTailor);
			db.Account_CustTailor.Remove(account_CustTailor);
			result = db.SaveChanges();
		}
		return result;
	}
}


