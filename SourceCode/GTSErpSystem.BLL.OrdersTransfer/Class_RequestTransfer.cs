using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GTSErpSystem.BLL.OrdersTransfer;

public class Class_RequestTransfer
{
	private int CachCode;

	private GTSdbEntities db = new GTSdbEntities();

	public List<Account_AccountType> GetAccountType()
	{
		return db.Account_AccountType.ToList();
	}

	public List<Order_Transfer_StatusType> GetStatusType()
	{
		return db.Order_Transfer_StatusType.ToList();
	}

	public List<Order_Transfer_TrackingType> GetTrackingType()
	{
		return db.Order_Transfer_TrackingType.ToList();
	}

	public List<Select_Order_Transfer_Master_Result> GetSelectAllPurchases()
	{
		return db.Select_Order_Transfer_Master(LoginDetails.BranchID).ToList();
	}

	public List<Order_Transfer_Master> GetPurchases(int Code)
	{
		return db.Order_Transfer_Master.Where((Order_Transfer_Master x) => x.ID == Code && x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public List<Item_Items> GetItems(string Code)
	{
		GTSdbEntities gTSdbEntities = new GTSdbEntities();
		return gTSdbEntities.Item_Items.Where((Item_Items x) => x.Item_code == Code || x.item_Name == Code || x.item_Name_English == Code || x.SmallBarCode1 == Code || x.SmallBarCode2 == Code || x.SmallBarCode3 == Code || x.MediumBarCode1 == Code || x.MediumBarCode2 == Code || x.MediumBarCode3 == Code || x.BigBarCode1 == Code || x.BigBarCode2 == Code || x.BigBarCode3 == Code).ToList();
	}

	public List<Item_Items> GetItemsByitemid(int ItemID)
	{
		return db.Item_Items.Where((Item_Items x) => x.ItemId == ItemID).ToList();
	}

	public List<Order_Transfer_MasterDetails> GetItemsPurchesByID(int PurchasesID)
	{
		return db.Order_Transfer_MasterDetails.Where((Order_Transfer_MasterDetails X) => X.Transfer_ID == (int?)PurchasesID).ToList();
	}

	public int GetUnitIdByName(string Nmae)
	{
		int num = 0;
		return (from x in db.Item_Unit
			where x.Name == Nmae
			select x.ID).FirstOrDefault();
	}

	public int InsertItems(DataTable Items, int Purchese_ID)
	{
		int result = 0;
		foreach (DataRow row in Items.Rows)
		{
			if (row["كود_الصنف"].ToString() != "")
			{
				Order_Transfer_MasterDetails order_Transfer_MasterDetails = new Order_Transfer_MasterDetails();
				string Itemcod = row["كود_الصنف"].ToString();
				Item_Items item_Items = db.Item_Items.Where((Item_Items z) => z.Item_code == Itemcod).FirstOrDefault();
				if (item_Items != null)
				{
					order_Transfer_MasterDetails.Transfer_ID = Purchese_ID;
					order_Transfer_MasterDetails.ItemID = Convert.ToInt32(item_Items.ItemId);
					order_Transfer_MasterDetails.ItemUnitID = GetUnitIdByName(row["الوحدة"].ToString());
					order_Transfer_MasterDetails.QuantityNeed = Convert.ToDecimal(row["الكمية"]);
					order_Transfer_MasterDetails.ItemNote = row["الشرح"].ToString();
					order_Transfer_MasterDetails.ItemUnitType = row["أسم_الوحدة"].ToString();
					db.Order_Transfer_MasterDetails.Add(order_Transfer_MasterDetails);
					result = db.SaveChanges();
				}
			}
		}
		return result;
	}

	public int InsertPurch(int FromBranchID, int ToBranchID, int StatusTypeID, int TrackingTypeID, DateTime Purchases_Date, int PurchType, int ProjectId, int CostCenter, string Note, string NoteNum, int YearId)
	{
		int num = 0;
		int num2 = Convert.ToInt32(db.Order_Transfer_Master.Select((Order_Transfer_Master p) => p.ID).DefaultIfEmpty(0).Max()) + 1;
		Order_Transfer_Master order_Transfer_Master = new Order_Transfer_Master();
		order_Transfer_Master.ID = num2;
		order_Transfer_Master.BranchID = LoginDetails.BranchID;
		order_Transfer_Master.FromBranchID = FromBranchID;
		order_Transfer_Master.ToBranchID = ToBranchID;
		order_Transfer_Master.StatusTypeID = StatusTypeID;
		order_Transfer_Master.TrackingTypeID = TrackingTypeID;
		order_Transfer_Master.Purchases_Date = Purchases_Date;
		order_Transfer_Master.Order_Paymant_Type = PurchType;
		order_Transfer_Master.ProjectId = ProjectId;
		order_Transfer_Master.CostCentersID = CostCenter;
		order_Transfer_Master.Note = Note;
		order_Transfer_Master.NoteNum = NoteNum;
		order_Transfer_Master.YearId = YearId;
		order_Transfer_Master.UserID_Add = LoginDetails.UserID;
		order_Transfer_Master.UserBranch_Add = LoginDetails.BranchID;
		order_Transfer_Master.UserMacAddress_Add = LoginDetails.macAddress;
		order_Transfer_Master.UserDate_Add = DateTime.Now;
		db.Order_Transfer_Master.Add(order_Transfer_Master);
		num = db.SaveChanges();
		return num2;
	}

	public void deleteItemsByPurcahesId(int PurID)
	{
		List<Order_Transfer_MasterDetails> list = db.Order_Transfer_MasterDetails.Where((Order_Transfer_MasterDetails x) => x.Transfer_ID == (int?)PurID).ToList();
		if (list == null)
		{
			return;
		}
		foreach (Order_Transfer_MasterDetails item in list)
		{
			db.Order_Transfer_MasterDetails.Attach(item);
			db.Order_Transfer_MasterDetails.Remove(item);
		}
		db.SaveChanges();
	}

	public int UpdatePurch(int PurID, int FromBranchID, int ToBranchID, int StatusTypeID, int TrackingTypeID, DateTime Purchases_Date, int PurchType, int ProjectId, int CostCenter, string Note, string NoteNum, int YearId)
	{
		int result = 0;
		Order_Transfer_Master order_Transfer_Master = db.Order_Transfer_Master.Where((Order_Transfer_Master x) => x.ID == PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (order_Transfer_Master != null)
		{
			order_Transfer_Master.BranchID = LoginDetails.BranchID;
			order_Transfer_Master.FromBranchID = FromBranchID;
			order_Transfer_Master.ToBranchID = ToBranchID;
			order_Transfer_Master.StatusTypeID = StatusTypeID;
			order_Transfer_Master.TrackingTypeID = TrackingTypeID;
			order_Transfer_Master.Purchases_Date = Purchases_Date;
			order_Transfer_Master.Order_Paymant_Type = PurchType;
			order_Transfer_Master.ProjectId = ProjectId;
			order_Transfer_Master.CostCentersID = CostCenter;
			order_Transfer_Master.Note = Note;
			order_Transfer_Master.NoteNum = NoteNum;
			order_Transfer_Master.YearId = YearId;
			order_Transfer_Master.UserID_Update = LoginDetails.UserID;
			order_Transfer_Master.UserBranch_Update = LoginDetails.GroupID;
			order_Transfer_Master.UserMacAddress_Update = LoginDetails.macAddress;
			order_Transfer_Master.UserDate_Update = DateTime.Now;
			db.SaveChanges();
			result = PurID;
		}
		return result;
	}

	public int Delete(int PurID)
	{
		int result = 0;
		Order_Transfer_Master order_Transfer_Master = db.Order_Transfer_Master.Where((Order_Transfer_Master x) => x.ID == PurID && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (order_Transfer_Master != null)
		{
			deleteItemsByPurcahesId(PurID);
			db.Order_Transfer_Master.Attach(order_Transfer_Master);
			db.Order_Transfer_Master.Remove(order_Transfer_Master);
			result = db.SaveChanges();
		}
		return result;
	}

	public int GetItmID(string Code)
	{
		return (from xx in db.Item_Items
			where xx.Item_code == Code
			select xx.ItemId).FirstOrDefault();
	}

	public string GetStoreName()
	{
		int StoreID = Convert.ToInt32((from x in db.TblSettings
			where x.ID == LoginDetails.BranchID
			select x.StoreID).SingleOrDefault());
		return (from x in db.Account_Stores
			where x.ID == StoreID
			select x.Store_Name).FirstOrDefault();
	}

	public int GetStoreID()
	{
		return Convert.ToInt32((from x in db.TblSettings
			where x.ID == LoginDetails.BranchID
			select x.StoreID).SingleOrDefault());
	}
}


