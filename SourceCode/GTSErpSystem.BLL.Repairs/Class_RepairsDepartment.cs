using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Repairs;

public class Class_RepairsDepartment
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Repairs_Groups> GetAll()
	{
		return db.Repairs_Groups.ToList();
	}

	public int Insert(string Name)
	{
		int result = 0;
		Repairs_Groups repairs_Groups = db.Repairs_Groups.Where((Repairs_Groups x) => x.GroupName == Name).FirstOrDefault();
		if (repairs_Groups == null)
		{
			int id = Convert.ToInt32(db.Repairs_Groups.Select((Repairs_Groups x) => x.Id).DefaultIfEmpty(0).Max()) + 1;
			Repairs_Groups repairs_Groups2 = new Repairs_Groups();
			repairs_Groups2.Id = id;
			repairs_Groups2.GroupName = Name;
			db.Repairs_Groups.Add(repairs_Groups2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Repairs_Groups repairs_Groups = db.Repairs_Groups.Where((Repairs_Groups x) => x.Id == ID).FirstOrDefault();
		Repairs_Groups repairs_Groups2 = db.Repairs_Groups.Where((Repairs_Groups x) => x.GroupName == Name && x.Id != ID).FirstOrDefault();
		if (repairs_Groups != null && repairs_Groups2 == null)
		{
			repairs_Groups.GroupName = Name;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(string CatName)
	{
		int result = 0;
		Repairs_Groups repairs_Groups = db.Repairs_Groups.Where((Repairs_Groups x) => x.GroupName == CatName).FirstOrDefault();
		if (repairs_Groups != null)
		{
			db.Repairs_Groups.Attach(repairs_Groups);
			db.Repairs_Groups.Remove(repairs_Groups);
			result = db.SaveChanges();
		}
		return result;
	}
}


