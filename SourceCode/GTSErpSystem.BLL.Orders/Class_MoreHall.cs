using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Orders;

public class Class_MoreHall
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<Order_MoveHall> GetCustCode(int OrderId)
	{
		return db.Order_MoveHall.ToList();
	}

	public int InsertUpdate(int OrderId, string NumberOfMen, string TheNumberOfMenslengths, string TheNumberOfCarcasses, string CookingCarcassesOnItsBottom, string CarcassType, decimal ThePriceOfTheSacrifice, string Trombeh, string Bff, string NumberOfWomen, string TheNumberOfWomensTables, string NumberOfWomensCandlesticks, string NumberOfHeaters, string Kosha, string TurnOnHeadphones, string AddAwedding, string AddCurains, string NescafeAndCappuccino, string LaserDevices, string AddADisplay, string TheAdditionOfMensRapture, string AnotherGroom)
	{
		int num = 0;
		Order_MoveHall order_MoveHall = db.Order_MoveHall.FirstOrDefault((Order_MoveHall x) => x.OrderId == (int?)OrderId);
		if (order_MoveHall != null)
		{
			order_MoveHall.NumberOfMen = NumberOfMen;
			order_MoveHall.TheNumberOfMenslengths = TheNumberOfMenslengths;
			order_MoveHall.TheNumberOfCarcasses = TheNumberOfCarcasses;
			order_MoveHall.CookingCarcassesOnItsBottom = CookingCarcassesOnItsBottom;
			order_MoveHall.CarcassType = CarcassType;
			order_MoveHall.ThePriceOfTheSacrifice = ThePriceOfTheSacrifice;
			order_MoveHall.Trombeh = Trombeh;
			order_MoveHall.Bff = Bff;
			order_MoveHall.NumberOfWomen = NumberOfWomen;
			order_MoveHall.TheNumberOfWomensTables = TheNumberOfWomensTables;
			order_MoveHall.NumberOfWomensCandlesticks = NumberOfWomensCandlesticks;
			order_MoveHall.NumberOfHeaters = NumberOfHeaters;
			order_MoveHall.Kosha = Kosha;
			order_MoveHall.TurnOnHeadphones = TurnOnHeadphones;
			order_MoveHall.AddAwedding = AddAwedding;
			order_MoveHall.AddCurains = AddCurains;
			order_MoveHall.NescafeAndCappuccino = NescafeAndCappuccino;
			order_MoveHall.LaserDevices = LaserDevices;
			order_MoveHall.AddADisplay = AddADisplay;
			order_MoveHall.TheAdditionOfMensRapture = TheAdditionOfMensRapture;
			order_MoveHall.AnotherGroom = AnotherGroom;
			db.SaveChanges();
			return 1;
		}
		Order_MoveHall entity = new Order_MoveHall
		{
			NumberOfMen = NumberOfMen,
			TheNumberOfMenslengths = TheNumberOfMenslengths,
			TheNumberOfCarcasses = TheNumberOfCarcasses,
			CookingCarcassesOnItsBottom = CookingCarcassesOnItsBottom,
			CarcassType = CarcassType,
			ThePriceOfTheSacrifice = ThePriceOfTheSacrifice,
			Trombeh = Trombeh,
			Bff = Bff,
			NumberOfWomen = NumberOfWomen,
			TheNumberOfWomensTables = TheNumberOfWomensTables,
			NumberOfWomensCandlesticks = NumberOfWomensCandlesticks,
			NumberOfHeaters = NumberOfHeaters,
			Kosha = Kosha,
			TurnOnHeadphones = TurnOnHeadphones,
			AddAwedding = AddAwedding,
			AddCurains = AddCurains,
			NescafeAndCappuccino = NescafeAndCappuccino,
			LaserDevices = LaserDevices,
			AddADisplay = AddADisplay,
			TheAdditionOfMensRapture = TheAdditionOfMensRapture,
			AnotherGroom = AnotherGroom,
			OrderId = OrderId
		};
		db.Order_MoveHall.Add(entity);
		db.SaveChanges();
		return 1;
	}

	public int Delete(int OrderId)
	{
		int result = 0;
		Order_MoveHall order_MoveHall = db.Order_MoveHall.FirstOrDefault((Order_MoveHall x) => x.OrderId == (int?)OrderId);
		if (order_MoveHall != null)
		{
			db.Order_MoveHall.Attach(order_MoveHall);
			db.Order_MoveHall.Remove(order_MoveHall);
			result = db.SaveChanges();
		}
		return result;
	}
}


