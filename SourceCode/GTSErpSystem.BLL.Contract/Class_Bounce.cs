using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Contract;

public class Class_Bounce
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Contract_Bounce> GetAll()
	{
		return db.Contract_Bounce.Where((Contract_Bounce x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public decimal? GetProfitByID(int ID)
	{
		return (from x in db.Contract_Bounce
			where x.ID == (int?)ID && x.BranchID == (int?)LoginDetails.BranchID
			select x.profit).FirstOrDefault();
	}

	public int Insert(string Name, decimal profit, bool ISProfitOrder, string Phone, int PlaceID, string PlateNumber)
	{
		int result = 0;
		Contract_Bounce contract_Bounce = db.Contract_Bounce.Where((Contract_Bounce x) => x.Name == Name && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (contract_Bounce == null)
		{
			int value = Convert.ToInt32((from x in db.Contract_Bounce
				where x.BranchID == (int?)LoginDetails.BranchID
				select x.ID).DefaultIfEmpty(0).Max()) + 1;
			Contract_Bounce contract_Bounce2 = new Contract_Bounce();
			contract_Bounce2.ID = value;
			contract_Bounce2.Name = Name;
			contract_Bounce2.profit = profit;
			contract_Bounce2.ISProfitOrder = ISProfitOrder;
			contract_Bounce2.Phone = Phone;
			contract_Bounce2.PlaceID = PlaceID;
			contract_Bounce2.PlateNumber = PlateNumber;
			contract_Bounce2.BranchID = LoginDetails.BranchID;
			contract_Bounce2.UserID_Add = LoginDetails.UserID;
			contract_Bounce2.UserBranch_Add = LoginDetails.BranchID;
			contract_Bounce2.UserMacAddress_Add = LoginDetails.macAddress;
			contract_Bounce2.UserDate_Add = DateTime.Now;
			db.Contract_Bounce.Add(contract_Bounce2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name, decimal profit, bool ISProfitOrder, string Phone, int PlaceID, string PlateNumber)
	{
		int num = 0;
		Contract_Bounce contract_Bounce = db.Contract_Bounce.Where((Contract_Bounce x) => x.ID == (int?)ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Contract_Bounce contract_Bounce2 = db.Contract_Bounce.Where((Contract_Bounce x) => x.Name == Name && x.ID != (int?)ID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (contract_Bounce != null && contract_Bounce2 == null)
		{
			contract_Bounce.Name = Name;
			contract_Bounce.profit = profit;
			contract_Bounce.ISProfitOrder = ISProfitOrder;
			contract_Bounce.Phone = Phone;
			contract_Bounce.PlaceID = PlaceID;
			contract_Bounce.PlateNumber = PlateNumber;
			contract_Bounce.UserID_Update = LoginDetails.UserID;
			contract_Bounce.UserBranch_Update = LoginDetails.GroupID;
			contract_Bounce.UserMacAddress_Update = LoginDetails.macAddress;
			contract_Bounce.UserDate_Update = DateTime.Now;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Contract_Bounce contract_Bounce = db.Contract_Bounce.Where((Contract_Bounce x) => x.ID == (int?)Sn && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (contract_Bounce != null)
		{
			db.Contract_Bounce.Attach(contract_Bounce);
			db.Contract_Bounce.Remove(contract_Bounce);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetName(int Code)
	{
		return (from x in db.Contract_Bounce
			where x.ID == (int?)Code && x.BranchID == (int?)LoginDetails.BranchID
			select x.Name).FirstOrDefault();
	}
}


