using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Main;

internal class Class_Printers
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Printer> GetAll()
	{
		return db.Printers.Where((Printer x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
	}

	public int Insert(string Name)
	{
		int num = 0;
		int iD = Convert.ToInt32(db.Printers.Select((Printer x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
		Printer printer = new Printer();
		printer.ID = iD;
		printer.Name = Name;
		printer.BranchID = LoginDetails.BranchID;
		db.Printers.Add(printer);
		return db.SaveChanges();
	}

	public int Update(int ID, string Name)
	{
		int num = 0;
		Printer printer = db.Printers.Where((Printer x) => x.ID == ID).FirstOrDefault();
		if (printer != null)
		{
			printer.Name = Name;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int PrintID)
	{
		int result = 0;
		Printer CheckCat = db.Printers.Where((Printer x) => x.ID == PrintID).FirstOrDefault();
		Item_Groups item_Groups = db.Item_Groups.Where((Item_Groups x) => x.PrinterID == (int?)CheckCat.ID).FirstOrDefault();
		if (CheckCat != null && item_Groups == null)
		{
			db.Printers.Attach(CheckCat);
			db.Printers.Remove(CheckCat);
			result = db.SaveChanges();
		}
		return result;
	}
}


