namespace GTSErpSystem.BLL.Main;

public class CurrencyInfo
{
	public enum Currencies
	{
		Egypt,
		UAE,
		SaudiArabia,
		Tunisia,
		Gold
	}

	public int CurrencyID { get; set; }

	public string CurrencyCode { get; set; }

	public bool IsCurrencyNameFeminine { get; set; }

	public string EnglishCurrencyName { get; set; }

	public string EnglishPluralCurrencyName { get; set; }

	public string Arabic1CurrencyName { get; set; }

	public string Arabic2CurrencyName { get; set; }

	public string Arabic310CurrencyName { get; set; }

	public string Arabic1199CurrencyName { get; set; }

	public byte PartPrecision { get; set; }

	public bool IsCurrencyPartNameFeminine { get; set; }

	public string EnglishCurrencyPartName { get; set; }

	public string EnglishPluralCurrencyPartName { get; set; }

	public string Arabic1CurrencyPartName { get; set; }

	public string Arabic2CurrencyPartName { get; set; }

	public string Arabic310CurrencyPartName { get; set; }

	public string Arabic1199CurrencyPartName { get; set; }

	public CurrencyInfo(Currencies currency)
	{
		switch (currency)
		{
		case Currencies.Egypt:
			CurrencyID = 0;
			CurrencyCode = "Egy";
			IsCurrencyNameFeminine = true;
			EnglishCurrencyName = "Egyption Pound";
			EnglishPluralCurrencyName = "Egyption Pounds";
			EnglishCurrencyPartName = "Piaster";
			EnglishPluralCurrencyPartName = "Piasteres";
			Arabic1CurrencyName = "جنيه مصري";
			Arabic2CurrencyName = "جنيهان مصري";
			Arabic310CurrencyName = "جنيهات مصري";
			Arabic1199CurrencyName = "جنيه مصري";
			Arabic1CurrencyPartName = "قرش";
			Arabic2CurrencyPartName = "قرشان";
			Arabic310CurrencyPartName = "قروش";
			Arabic1199CurrencyPartName = "قرشا\u064b";
			PartPrecision = 2;
			IsCurrencyPartNameFeminine = false;
			break;
		case Currencies.UAE:
			CurrencyID = 1;
			CurrencyCode = "AED";
			IsCurrencyNameFeminine = false;
			EnglishCurrencyName = "UAE Dirham";
			EnglishPluralCurrencyName = "UAE Dirhams";
			EnglishCurrencyPartName = "Fils";
			EnglishPluralCurrencyPartName = "Fils";
			Arabic1CurrencyName = "درهم إماراتي";
			Arabic2CurrencyName = "درهمان إماراتيان";
			Arabic310CurrencyName = "دراهم إماراتية";
			Arabic1199CurrencyName = "درهما\u064b إماراتيا\u064b";
			Arabic1CurrencyPartName = "فلس";
			Arabic2CurrencyPartName = "فلسان";
			Arabic310CurrencyPartName = "فلوس";
			Arabic1199CurrencyPartName = "فلسا\u064b";
			PartPrecision = 2;
			IsCurrencyPartNameFeminine = false;
			break;
		case Currencies.SaudiArabia:
			CurrencyID = 2;
			CurrencyCode = "SAR";
			IsCurrencyNameFeminine = false;
			EnglishCurrencyName = "Saudi Riyal";
			EnglishPluralCurrencyName = "Saudi Riyals";
			EnglishCurrencyPartName = "Halala";
			EnglishPluralCurrencyPartName = "Halalas";
			Arabic1CurrencyName = "ريال سعودي";
			Arabic2CurrencyName = "ريالان سعوديان";
			Arabic310CurrencyName = "ريالات سعودية";
			Arabic1199CurrencyName = "ريالا\u064b سعوديا\u064b";
			Arabic1CurrencyPartName = "هللة";
			Arabic2CurrencyPartName = "هللتان";
			Arabic310CurrencyPartName = "هللات";
			Arabic1199CurrencyPartName = "هللة";
			PartPrecision = 2;
			IsCurrencyPartNameFeminine = true;
			break;
		case Currencies.Tunisia:
			CurrencyID = 3;
			CurrencyCode = "TND";
			IsCurrencyNameFeminine = false;
			EnglishCurrencyName = "Tunisian Dinar";
			EnglishPluralCurrencyName = "Tunisian Dinars";
			EnglishCurrencyPartName = "milim";
			EnglishPluralCurrencyPartName = "millimes";
			Arabic1CurrencyName = "درهم إماراتي";
			Arabic2CurrencyName = "درهمان إماراتيان";
			Arabic310CurrencyName = "دراهم إماراتية";
			Arabic1199CurrencyName = "درهما\u064b إماراتيا\u064b";
			Arabic1CurrencyPartName = "فلس";
			Arabic2CurrencyPartName = "فلسان";
			Arabic310CurrencyPartName = "فلوس";
			Arabic1199CurrencyPartName = "فلسا\u064b";
			PartPrecision = 3;
			IsCurrencyPartNameFeminine = false;
			break;
		case Currencies.Gold:
			CurrencyID = 4;
			CurrencyCode = "XAU";
			IsCurrencyNameFeminine = false;
			EnglishCurrencyName = "Gram";
			EnglishPluralCurrencyName = "Grams";
			EnglishCurrencyPartName = "Milligram";
			EnglishPluralCurrencyPartName = "Milligrams";
			Arabic1CurrencyName = "جرام";
			Arabic2CurrencyName = "جرامان";
			Arabic310CurrencyName = "جرامات";
			Arabic1199CurrencyName = "جراما\u064b";
			Arabic1CurrencyPartName = "ملجرام";
			Arabic2CurrencyPartName = "ملجرامان";
			Arabic310CurrencyPartName = "ملجرامات";
			Arabic1199CurrencyPartName = "ملجراما\u064b";
			PartPrecision = 2;
			IsCurrencyPartNameFeminine = false;
			break;
		}
	}
}


