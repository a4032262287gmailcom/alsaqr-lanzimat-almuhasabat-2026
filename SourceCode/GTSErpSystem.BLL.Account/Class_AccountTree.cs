using System;
using System.Linq;

namespace GTSErpSystem.BLL.Account;

public class Class_AccountTree
{
	private GTSdbEntities db = new GTSdbEntities();

	public int GetLevel_0()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)0 && x.Account_No < (int?)10
			select x.Account_No).DefaultIfEmpty(0).Max() + 1);
	}

	public int GetLevel_110()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)109 && x.Account_No < (int?)200
			select x.Account_No).DefaultIfEmpty(109).Max() + 1);
	}

	public int GetLevel_110_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)11009 && x.Account_No < (int?)11099
			select x.Account_No).DefaultIfEmpty(11009).Max() + 1);
	}

	public int GetLevel_110_10_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)1101009 && x.Account_No < (int?)1101099
			select x.Account_No).DefaultIfEmpty(1101009).Max() + 1);
	}

	public int GetLevel_110_11_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)1101110 && x.Account_No < (int?)1101199
			select x.Account_No).DefaultIfEmpty(1101110).Max() + 1);
	}

	public int GetLevel_110_12_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)1101210 && x.Account_No < (int?)1101299
			select x.Account_No).DefaultIfEmpty(1101210).Max() + 1);
	}

	public int GetLevel_110_13_11()
	{
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)110130001 && x.Account_No < (int?)110139999
			select x.Account_No).DefaultIfEmpty(110130001).Max() + 1);
	}

	public int GetLevel_110_14_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)1101410 && x.Account_No < (int?)1101499
			select x.Account_No).DefaultIfEmpty(1101410).Max() + 1);
	}

	public int GetLevel_110_15_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)1101510 && x.Account_No < (int?)1101599
			select x.Account_No).DefaultIfEmpty(1101510).Max() + 1);
	}

	public int GetLevel_110_16_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)1101610 && x.Account_No < (int?)1101699
			select x.Account_No).DefaultIfEmpty(1101610).Max() + 1);
	}

	public int GetLevel_110_17_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)1101710 && x.Account_No < (int?)1101799
			select x.Account_No).DefaultIfEmpty(1101710).Max() + 1);
	}

	public int GetLevel_110_18_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)1101810 && x.Account_No < (int?)1101899
			select x.Account_No).DefaultIfEmpty(1101810).Max() + 1);
	}

	public int GetLevel_110_19_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)1101910 && x.Account_No < (int?)1101999
			select x.Account_No).DefaultIfEmpty(1101910).Max() + 1);
	}

	public int GetLevel_111_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)11109 && x.Account_No < (int?)11999
			select x.Account_No).DefaultIfEmpty(11109).Max() + 1);
	}

	public int GetLevel_110_20_20()
	{
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)1102020 && x.Account_No < (int?)1102999
			select x.Account_No).DefaultIfEmpty(1102010).Max() + 1);
	}

	public int GetLevel_210()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)209 && x.Account_No < (int?)300
			select x.Account_No).DefaultIfEmpty(209).Max() + 1);
	}

	public int GetLevel_210_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)21009 && x.Account_No < (int?)21099
			select x.Account_No).DefaultIfEmpty(21009).Max() + 1);
	}

	public int GetLevel_210_10_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)210100001 && x.Account_No < (int?)210109999
			select x.Account_No).DefaultIfEmpty(210100001).Max() + 1);
	}

	public int GetLevel_210_11_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)2101110 && x.Account_No < (int?)2101199
			select x.Account_No).DefaultIfEmpty(2101110).Max() + 1);
	}

	public int GetLevel_210_12_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)2101210 && x.Account_No < (int?)2101299
			select x.Account_No).DefaultIfEmpty(2101210).Max() + 1);
	}

	public int GetLevel_210_13_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)2101310 && x.Account_No < (int?)2101399
			select x.Account_No).DefaultIfEmpty(2101310).Max() + 1);
	}

	public int GetLevel_210_14_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)2101410 && x.Account_No < (int?)2101499
			select x.Account_No).DefaultIfEmpty(2101410).Max() + 1);
	}

	public int GetLevel_210_15_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)2101510 && x.Account_No < (int?)2101599
			select x.Account_No).DefaultIfEmpty(2101510).Max() + 1);
	}

	public int GetLevel_210_16_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)2101610 && x.Account_No < (int?)2101699
			select x.Account_No).DefaultIfEmpty(2101610).Max() + 1);
	}

	public int GetLevel_210_17_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)2101710 && x.Account_No < (int?)2101799
			select x.Account_No).DefaultIfEmpty(2101710).Max() + 1);
	}

	public int GetLevel_210_18_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)2101810 && x.Account_No < (int?)2101899
			select x.Account_No).DefaultIfEmpty(2101810).Max() + 1);
	}

	public int GetLevel_210_19_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)2101910 && x.Account_No < (int?)2101999
			select x.Account_No).DefaultIfEmpty(2101910).Max() + 1);
	}

	public int GetLevel_211_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)21109 && x.Account_No < (int?)21999
			select x.Account_No).DefaultIfEmpty(21109).Max() + 1);
	}

	public int GetLevel_210_20_20()
	{
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)2102020 && x.Account_No < (int?)2102999
			select x.Account_No).DefaultIfEmpty(2102010).Max() + 1);
	}

	public int GetLevel_310()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)309 && x.Account_No < (int?)300
			select x.Account_No).DefaultIfEmpty(309).Max() + 1);
	}

	public int GetLevel_310_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)31009 && x.Account_No < (int?)31099
			select x.Account_No).DefaultIfEmpty(31009).Max() + 1);
	}

	public int GetLevel_310_10_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3101009 && x.Account_No < (int?)3101099
			select x.Account_No).DefaultIfEmpty(3101009).Max() + 1);
	}

	public int GetLevel_310_11_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3101110 && x.Account_No < (int?)3101199
			select x.Account_No).DefaultIfEmpty(3101110).Max() + 1);
	}

	public int GetLevel_310_12_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3101210 && x.Account_No < (int?)3101299
			select x.Account_No).DefaultIfEmpty(3101210).Max() + 1);
	}

	public int GetLevel_310_13_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3101310 && x.Account_No < (int?)3101399
			select x.Account_No).DefaultIfEmpty(3101310).Max() + 1);
	}

	public int GetLevel_310_14_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3101410 && x.Account_No < (int?)3101499
			select x.Account_No).DefaultIfEmpty(3101410).Max() + 1);
	}

	public int GetLevel_310_15_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3101510 && x.Account_No < (int?)3101599
			select x.Account_No).DefaultIfEmpty(3101510).Max() + 1);
	}

	public int GetLevel_310_16_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3101610 && x.Account_No < (int?)3101699
			select x.Account_No).DefaultIfEmpty(3101610).Max() + 1);
	}

	public int GetLevel_310_17_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3101710 && x.Account_No < (int?)3101799
			select x.Account_No).DefaultIfEmpty(3101710).Max() + 1);
	}

	public int GetLevel_310_18_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3101810 && x.Account_No < (int?)3101899
			select x.Account_No).DefaultIfEmpty(3101810).Max() + 1);
	}

	public int GetLevel_310_19_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3101910 && x.Account_No < (int?)3101999
			select x.Account_No).DefaultIfEmpty(3101910).Max() + 1);
	}

	public int GetLevel_310_20_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3102009 && x.Account_No < (int?)3102099
			select x.Account_No).DefaultIfEmpty(3102009).Max() + 1);
	}

	public int GetLevel_310_21_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3102110 && x.Account_No < (int?)3102199
			select x.Account_No).DefaultIfEmpty(3102110).Max() + 1);
	}

	public int GetLevel_310_22_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3102210 && x.Account_No < (int?)3102299
			select x.Account_No).DefaultIfEmpty(3102210).Max() + 1);
	}

	public int GetLevel_310_23_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3102310 && x.Account_No < (int?)3102399
			select x.Account_No).DefaultIfEmpty(3102310).Max() + 1);
	}

	public int GetLevel_310_24_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3102410 && x.Account_No < (int?)3102499
			select x.Account_No).DefaultIfEmpty(3102410).Max() + 1);
	}

	public int GetLevel_310_25_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3102510 && x.Account_No < (int?)3102599
			select x.Account_No).DefaultIfEmpty(3102510).Max() + 1);
	}

	public int GetLevel_310_26_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3102610 && x.Account_No < (int?)3102699
			select x.Account_No).DefaultIfEmpty(3102610).Max() + 1);
	}

	public int GetLevel_310_27_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3102710 && x.Account_No < (int?)3102799
			select x.Account_No).DefaultIfEmpty(3102710).Max() + 1);
	}

	public int GetLevel_310_28_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3102810 && x.Account_No < (int?)3102899
			select x.Account_No).DefaultIfEmpty(3102810).Max() + 1);
	}

	public int GetLevel_311_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)31109 && x.Account_No < (int?)31999
			select x.Account_No).DefaultIfEmpty(31109).Max() + 1);
	}

	public int GetLevel_310_20_20()
	{
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)3102020 && x.Account_No < (int?)3102999
			select x.Account_No).DefaultIfEmpty(3102010).Max() + 1);
	}

	public int GetLevel_410()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)109 && x.Account_No < (int?)200
			select x.Account_No).DefaultIfEmpty(109).Max() + 1);
	}

	public int GetLevel_410_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)11009 && x.Account_No < (int?)11099
			select x.Account_No).DefaultIfEmpty(11009).Max() + 1);
	}

	public int GetLevel_410_10_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4101009 && x.Account_No < (int?)4101099
			select x.Account_No).DefaultIfEmpty(4101009).Max() + 1);
	}

	public int GetLevel_410_11_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4101110 && x.Account_No < (int?)4101199
			select x.Account_No).DefaultIfEmpty(4101110).Max() + 1);
	}

	public int GetLevel_410_12_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4101210 && x.Account_No < (int?)4101299
			select x.Account_No).DefaultIfEmpty(4101210).Max() + 1);
	}

	public int GetLevel_410_13_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4101310 && x.Account_No < (int?)4101399
			select x.Account_No).DefaultIfEmpty(4101310).Max() + 1);
	}

	public int GetLevel_410_14_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4101410 && x.Account_No < (int?)4101499
			select x.Account_No).DefaultIfEmpty(4101410).Max() + 1);
	}

	public int GetLevel_410_15_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4101510 && x.Account_No < (int?)4101599
			select x.Account_No).DefaultIfEmpty(4101510).Max() + 1);
	}

	public int GetLevel_410_16_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4101610 && x.Account_No < (int?)4101699
			select x.Account_No).DefaultIfEmpty(4101610).Max() + 1);
	}

	public int GetLevel_410_17_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4101710 && x.Account_No < (int?)4101799
			select x.Account_No).DefaultIfEmpty(4101710).Max() + 1);
	}

	public int GetLevel_410_18_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4101810 && x.Account_No < (int?)4101899
			select x.Account_No).DefaultIfEmpty(4101810).Max() + 1);
	}

	public int GetLevel_410_19_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4101910 && x.Account_No < (int?)4101999
			select x.Account_No).DefaultIfEmpty(4101910).Max() + 1);
	}

	public int GetLevel_411_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)11109 && x.Account_No < (int?)11999
			select x.Account_No).DefaultIfEmpty(11109).Max() + 1);
	}

	public int GetLevel_411_10_10()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4111009 && x.Account_No < (int?)4111099
			select x.Account_No).DefaultIfEmpty(4111009).Max() + 1);
	}

	public int GetLevel_411_11_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4111110 && x.Account_No < (int?)4111199
			select x.Account_No).DefaultIfEmpty(4111110).Max() + 1);
	}

	public int GetLevel_411_12_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4111210 && x.Account_No < (int?)4111299
			select x.Account_No).DefaultIfEmpty(4111210).Max() + 1);
	}

	public int GetLevel_411_13_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4111310 && x.Account_No < (int?)4111399
			select x.Account_No).DefaultIfEmpty(4111310).Max() + 1);
	}

	public int GetLevel_411_14_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4111410 && x.Account_No < (int?)4111499
			select x.Account_No).DefaultIfEmpty(4111410).Max() + 1);
	}

	public int GetLevel_411_15_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4111510 && x.Account_No < (int?)4111599
			select x.Account_No).DefaultIfEmpty(4111510).Max() + 1);
	}

	public int GetLevel_411_16_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4111610 && x.Account_No < (int?)4111699
			select x.Account_No).DefaultIfEmpty(4111610).Max() + 1);
	}

	public int GetLevel_411_17_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4111710 && x.Account_No < (int?)4111799
			select x.Account_No).DefaultIfEmpty(4111710).Max() + 1);
	}

	public int GetLevel_411_18_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4111810 && x.Account_No < (int?)4111899
			select x.Account_No).DefaultIfEmpty(4111810).Max() + 1);
	}

	public int GetLevel_411_19_11()
	{
		int num = 0;
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4111910 && x.Account_No < (int?)4111999
			select x.Account_No).DefaultIfEmpty(4111910).Max() + 1);
	}

	public int GetLevel_410_20_20()
	{
		return Convert.ToInt32((from x in db.Account_Accounts
			where x.Account_No > (int?)4102020 && x.Account_No < (int?)4102999
			select x.Account_No).DefaultIfEmpty(4102010).Max() + 1);
	}
}


