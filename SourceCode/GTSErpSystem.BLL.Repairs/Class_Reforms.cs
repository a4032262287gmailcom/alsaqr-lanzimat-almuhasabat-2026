using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Repairs;

public class Class_Reforms
{
	public class RepairDetailVM
	{
		public int GroupId { get; set; }

		public int MalfunctionId { get; set; }
	}

	private GTSdbEntities db = new GTSdbEntities();

	public List<Repairs_Repair> GetAll()
	{
		return db.Repairs_Repair.OrderByDescending((Repairs_Repair x) => x.Id).ToList();
	}

	public int Insert(string CustomerName, string PhoneNumber, string PlateNumber, string CarModel, string CarType, string CarColor, string Note, List<RepairDetailVM> detailsList)
	{
		int num = 0;
		Repairs_Repair repairs_Repair = new Repairs_Repair();
		repairs_Repair.CustomerName = CustomerName;
		repairs_Repair.PhoneNumber = PhoneNumber;
		repairs_Repair.PlateNumber = PlateNumber;
		repairs_Repair.CarModel = CarModel;
		repairs_Repair.CarType = CarType;
		repairs_Repair.CarColor = CarColor;
		repairs_Repair.Notes = Note;
		db.Repairs_Repair.Add(repairs_Repair);
		db.SaveChanges();
		foreach (RepairDetailVM details in detailsList)
		{
			Repairs_RepairDetails repairs_RepairDetails = new Repairs_RepairDetails();
			repairs_RepairDetails.RepairId = repairs_Repair.Id;
			repairs_RepairDetails.GroupId = details.GroupId;
			repairs_RepairDetails.MalfunctionId = details.MalfunctionId;
			db.Repairs_RepairDetails.Add(repairs_RepairDetails);
		}
		return db.SaveChanges();
	}

	public int Update(int RepairId, string CustomerName, string PhoneNumber, string PlateNumber, string CarModel, string CarType, string CarColor, string Note, List<RepairDetailVM> detailsList)
	{
		Repairs_Repair repairs_Repair = db.Repairs_Repair.FirstOrDefault((Repairs_Repair r) => r.Id == RepairId);
		if (repairs_Repair == null)
		{
			return 0;
		}
		repairs_Repair.CustomerName = CustomerName;
		repairs_Repair.PhoneNumber = PhoneNumber;
		repairs_Repair.PlateNumber = PlateNumber;
		repairs_Repair.CarModel = CarModel;
		repairs_Repair.CarType = CarType;
		repairs_Repair.CarColor = CarColor;
		repairs_Repair.Notes = Note;
		List<Repairs_RepairDetails> entities = db.Repairs_RepairDetails.Where((Repairs_RepairDetails d) => d.RepairId == RepairId).ToList();
		db.Repairs_RepairDetails.RemoveRange(entities);
		foreach (RepairDetailVM details in detailsList)
		{
			Repairs_RepairDetails repairs_RepairDetails = new Repairs_RepairDetails();
			repairs_RepairDetails.RepairId = RepairId;
			repairs_RepairDetails.GroupId = details.GroupId;
			repairs_RepairDetails.MalfunctionId = details.MalfunctionId;
			db.Repairs_RepairDetails.Add(repairs_RepairDetails);
		}
		return db.SaveChanges();
	}

	public int Delete(int RepairId)
	{
		Repairs_Repair repairs_Repair = db.Repairs_Repair.FirstOrDefault((Repairs_Repair r) => r.Id == RepairId);
		if (repairs_Repair == null)
		{
			return 0;
		}
		List<Repairs_RepairDetails> entities = db.Repairs_RepairDetails.Where((Repairs_RepairDetails d) => d.RepairId == RepairId).ToList();
		db.Repairs_RepairDetails.RemoveRange(entities);
		db.Repairs_Repair.Remove(repairs_Repair);
		return db.SaveChanges();
	}

	public Repairs_Repair GetById(int repairId)
	{
		return db.Repairs_Repair.Include("Repairs_RepairDetails").FirstOrDefault((Repairs_Repair r) => r.Id == repairId);
	}
}


