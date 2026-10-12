using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Restaurant;

public class Class_Add
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Item_Add> GetAll()
	{
		return db.Item_Add.ToList();
	}

	public int Insert(string Name, string NameEN, decimal SellPrice, decimal CostPrice, bool Is_Tax, decimal Tax_Value, int PrintID, bool Type)
	{
		int result = 0;
		Item_Add item_Add = db.Item_Add.Where((Item_Add x) => x.Name == Name).FirstOrDefault();
		if (item_Add == null)
		{
			int iD = Convert.ToInt32(db.Item_Add.Select((Item_Add x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Item_Add item_Add2 = new Item_Add();
			item_Add2.ID = iD;
			item_Add2.Name = Name;
			item_Add2.NameEN = NameEN;
			item_Add2.SellPrice = SellPrice;
			item_Add2.CostPrice = CostPrice;
			item_Add2.Is_Tax = Is_Tax;
			item_Add2.Tax_Value = Tax_Value;
			item_Add2.PrintID = PrintID;
			item_Add2.Type = Type;
			db.Item_Add.Add(item_Add2);
			result = db.SaveChanges();
		}
		return result;
	}

	public int Update(int ID, string Name, string NameEN, decimal SellPrice, decimal CostPrice, bool Is_Tax, decimal Tax_Value, int PrintID, bool Type)
	{
		int num = 0;
		Item_Add item_Add = db.Item_Add.Where((Item_Add x) => x.ID == ID).FirstOrDefault();
		Item_Add item_Add2 = db.Item_Add.Where((Item_Add x) => x.Name == Name && x.ID != ID).FirstOrDefault();
		if (item_Add != null && item_Add2 == null)
		{
			item_Add.Name = Name;
			item_Add.Name = Name;
			item_Add.NameEN = NameEN;
			item_Add.SellPrice = SellPrice;
			item_Add.CostPrice = CostPrice;
			item_Add.Is_Tax = Is_Tax;
			item_Add.Tax_Value = Tax_Value;
			item_Add.PrintID = PrintID;
			item_Add.Type = Type;
			num = db.SaveChanges();
			return 1;
		}
		return 0;
	}

	public int Delete(int Sn)
	{
		int result = 0;
		Item_Add item_Add = db.Item_Add.Where((Item_Add x) => x.ID == Sn).FirstOrDefault();
		if (item_Add != null)
		{
			db.Item_Add.Attach(item_Add);
			db.Item_Add.Remove(item_Add);
			result = db.SaveChanges();
		}
		return result;
	}

	public string GetUnitName(int Code)
	{
		return (from x in db.Item_Add
			where x.ID == Code
			select x.Name).FirstOrDefault();
	}
}


