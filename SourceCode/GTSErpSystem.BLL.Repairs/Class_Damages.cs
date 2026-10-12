using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Repairs;

public class Class_Damages
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Repairs_Malfunctions> GetAll()
	{
		return db.Repairs_Malfunctions.ToList();
	}

	public int Insert(string Name, int DepartmentId)
	{
		int result = 0;
		Repairs_Malfunctions repairs_Malfunctions = db.Repairs_Malfunctions.Where((Repairs_Malfunctions x) => x.Name == Name).FirstOrDefault();
		if (repairs_Malfunctions == null)
		{
			int id = Convert.ToInt32(db.Repairs_Groups.Select((Repairs_Groups x) => x.Id).DefaultIfEmpty(0).Max()) + 1;
			Repairs_Malfunctions repairs_Malfunctions2 = new Repairs_Malfunctions();
			repairs_Malfunctions2.Id = id;
			repairs_Malfunctions2.Name = Name;
			repairs_Malfunctions2.GroupId = DepartmentId;
			db.Repairs_Malfunctions.Add(repairs_Malfunctions2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name, int DepartmentId)
	{
		int num = 0;
		Repairs_Malfunctions repairs_Malfunctions = db.Repairs_Malfunctions.Where((Repairs_Malfunctions x) => x.Id == ID).FirstOrDefault();
		Repairs_Malfunctions repairs_Malfunctions2 = db.Repairs_Malfunctions.Where((Repairs_Malfunctions x) => x.Name == Name && x.Id != ID).FirstOrDefault();
		if (repairs_Malfunctions != null && repairs_Malfunctions2 == null)
		{
			repairs_Malfunctions.Name = Name;
			repairs_Malfunctions.GroupId = DepartmentId;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(string _Malfunctions)
	{
		int result = 0;
		Repairs_Malfunctions repairs_Malfunctions = db.Repairs_Malfunctions.Where((Repairs_Malfunctions x) => x.Name == _Malfunctions).FirstOrDefault();
		if (repairs_Malfunctions != null)
		{
			db.Repairs_Malfunctions.Attach(repairs_Malfunctions);
			db.Repairs_Malfunctions.Remove(repairs_Malfunctions);
			result = db.SaveChanges();
		}
		return result;
	}
}


