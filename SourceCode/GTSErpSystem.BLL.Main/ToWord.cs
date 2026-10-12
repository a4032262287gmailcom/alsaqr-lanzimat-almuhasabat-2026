using System;

namespace GTSErpSystem.BLL.Main;

internal class ToWord
{
	private long _intergerValue;

	private int _decimalValue;

	private static string[] englishOnes = new string[20]
	{
		"Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
		"Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
	};

	private static string[] englishTens = new string[8] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

	private static string[] englishGroup = new string[35]
	{
		"Hundred", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillian", "Septillion", "Octillion",
		"Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion",
		"Novemdecillion", "Vigintillion", "Unvigintillion", "Duovigintillion", "10^72", "10^75", "10^78", "10^81", "10^84", "10^87",
		"Vigintinonillion", "10^93", "10^96", "Duotrigintillion", "Trestrigintillion"
	};

	private static string[] arabicOnes = new string[20]
	{
		string.Empty,
		"واحد",
		"اثنان",
		"ثلاثة",
		"أربعة",
		"خمسة",
		"ستة",
		"سبعة",
		"ثمانية",
		"تسعة",
		"عشرة",
		"أحد عشر",
		"اثنتا عشر",
		"ثلاثة عشر",
		"أربعة عشر",
		"خمسة عشر",
		"ستة عشر",
		"سبعة عشر",
		"ثمانية عشر",
		"تسعة عشر"
	};

	private static string[] arabicFeminineOnes = new string[20]
	{
		string.Empty,
		"إحدى",
		"اثنتان",
		"ثلاث",
		"أربع",
		"خمس",
		"ستة",
		"سبعة",
		"ثمانية",
		"تسعة",
		"عشر",
		"إحدى عشرة",
		"اثنتا عشرة",
		"ثلاثة عشرة",
		"أربعة عشرة",
		"خمسة عشرة",
		"ستة عشرة",
		"سبعة عشرة",
		"ثمانية عشرة",
		"تسعة عشرة"
	};

	private static string[] arabicTens = new string[8] { "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };

	private static string[] arabicHundreds = new string[10] { "", "مائة", "مئتان", "ثلاثمائة", "أربعمائة", "خمسمائة", "ستمائة", "سبعمائة", "ثمانمائة", "تسعمائة" };

	private static string[] arabicAppendedTwos = new string[8] { "مئتان", "ألفان", "مليونان", "مليارا", "تريليونا", "كوادريليونا", "كوينتليونا", "سكستيليونا" };

	private static string[] arabicTwos = new string[8] { "مئتان", "ألفان", "مليونان", "ملياران", "تريليونان", "كوادريليونان", "كوينتليونان", "سكستيليونان" };

	private static string[] arabicGroup = new string[8] { "مائة", "ألف", "مليون", "مليار", "تريليون", "كوادريليون", "كوينتليون", "سكستيليون" };

	private static string[] arabicAppendedGroup = new string[8] { "", "ألفا\u064b", "مليونا\u064b", "مليارا\u064b", "تريليونا\u064b", "كوادريليونا\u064b", "كوينتليونا\u064b", "سكستيليونا\u064b" };

	private static string[] arabicPluralGroups = new string[8] { "", "آلاف", "ملايين", "مليارات", "تريليونات", "كوادريليونات", "كوينتليونات", "سكستيليونات" };

	public decimal Number { get; set; }

	public CurrencyInfo Currency { get; set; }

	public string EnglishPrefixText { get; set; }

	public string EnglishSuffixText { get; set; }

	public string ArabicPrefixText { get; set; }

	public string ArabicSuffixText { get; set; }

	public ToWord(decimal number, CurrencyInfo currency)
	{
		InitializeClass(number, currency, string.Empty, "only.", "", "فقط لا غير");
	}

	public ToWord(decimal number, CurrencyInfo currency, string englishPrefixText, string englishSuffixText, string arabicPrefixText, string arabicSuffixText)
	{
		InitializeClass(number, currency, englishPrefixText, englishSuffixText, arabicPrefixText, arabicSuffixText);
	}

	private void InitializeClass(decimal number, CurrencyInfo currency, string englishPrefixText, string englishSuffixText, string arabicPrefixText, string arabicSuffixText)
	{
		Number = number;
		Currency = currency;
		EnglishPrefixText = englishPrefixText;
		EnglishSuffixText = englishSuffixText;
		ArabicPrefixText = arabicPrefixText;
		ArabicSuffixText = arabicSuffixText;
		ExtractIntegerAndDecimalParts();
	}

	private string GetDecimalValue(string decimalPart)
	{
		string empty = string.Empty;
		if (Currency.PartPrecision != decimalPart.Length)
		{
			empty = $"{decimalPart.Substring(0, Currency.PartPrecision)}.{decimalPart.Substring(Currency.PartPrecision, decimalPart.Length - Currency.PartPrecision)}";
			empty = Math.Round(Convert.ToDecimal(empty)).ToString();
		}
		else
		{
			empty = decimalPart;
		}
		for (int i = 0; i < Currency.PartPrecision - empty.Length; i++)
		{
			empty += "0";
		}
		return empty;
	}

	private void ExtractIntegerAndDecimalParts()
	{
		string[] array = Number.ToString().Split('.');
		_intergerValue = Convert.ToInt32(array[0]);
		if (array.Length > 1)
		{
			_decimalValue = Convert.ToInt32(GetDecimalValue(array[1]));
		}
	}

	private string ProcessGroup(int groupNumber)
	{
		int num = groupNumber % 100;
		int num2 = groupNumber / 100;
		string text = string.Empty;
		if (num2 > 0)
		{
			text = $"{englishOnes[num2]} {englishGroup[0]}";
		}
		if (num > 0)
		{
			if (num < 20)
			{
				text = text + ((text != string.Empty) ? " " : string.Empty) + englishOnes[num];
			}
			else
			{
				int num3 = num % 10;
				num = num / 10 - 2;
				text = text + ((text != string.Empty) ? " " : string.Empty) + englishTens[num];
				if (num3 > 0)
				{
					text = text + ((text != string.Empty) ? " " : string.Empty) + englishOnes[num3];
				}
			}
		}
		return text;
	}

	public string ConvertToEnglish()
	{
		decimal number = Number;
		if (number == 0m)
		{
			return "Zero";
		}
		string text = ProcessGroup(_decimalValue);
		string text2 = string.Empty;
		int num = 0;
		if (number < 1m)
		{
			text2 = englishOnes[0];
		}
		else
		{
			while (number >= 1m)
			{
				int groupNumber = (int)(number % 1000m);
				number /= 1000m;
				string text3 = ProcessGroup(groupNumber);
				if (text3 != string.Empty)
				{
					if (num > 0)
					{
						text2 = $"{englishGroup[num]} {text2}";
					}
					text2 = $"{text3} {text2}";
				}
				num++;
			}
		}
		string empty = string.Empty;
		empty += ((EnglishPrefixText != string.Empty) ? $"{EnglishPrefixText} " : string.Empty);
		empty += ((text2 != string.Empty) ? text2 : string.Empty);
		empty += ((!(text2 != string.Empty)) ? string.Empty : ((_intergerValue == 1) ? Currency.EnglishCurrencyName : Currency.EnglishPluralCurrencyName));
		empty += ((text != string.Empty) ? " and " : string.Empty);
		empty += ((text != string.Empty) ? text : string.Empty);
		empty += ((text != string.Empty) ? (" " + ((_decimalValue == 1) ? Currency.EnglishCurrencyPartName : Currency.EnglishPluralCurrencyPartName)) : string.Empty);
		return empty + ((EnglishSuffixText != string.Empty) ? $" {EnglishSuffixText}" : string.Empty);
	}

	private string GetDigitFeminineStatus(int digit, int groupLevel)
	{
		switch (groupLevel)
		{
		case -1:
			if (Currency.IsCurrencyPartNameFeminine)
			{
				return arabicFeminineOnes[digit];
			}
			return arabicOnes[digit];
		case 0:
			if (Currency.IsCurrencyNameFeminine)
			{
				return arabicFeminineOnes[digit];
			}
			return arabicOnes[digit];
		default:
			return arabicOnes[digit];
		}
	}

	private string ProcessArabicGroup(int groupNumber, int groupLevel, decimal remainingNumber)
	{
		int num = groupNumber % 100;
		int num2 = groupNumber / 100;
		string text = string.Empty;
		if (num2 > 0)
		{
			text = ((num != 0 || num2 != 2) ? $"{arabicHundreds[num2]}" : $"{arabicAppendedTwos[0]}");
		}
		if (num > 0)
		{
			if (num < 20)
			{
				if (num == 2 && num2 == 0 && groupLevel > 0)
				{
					text = ((_intergerValue != 2000 && _intergerValue != 2000000 && _intergerValue != 2000000000 && _intergerValue != 2000000000000L && _intergerValue != 2000000000000000L && _intergerValue != 2000000000000000000L) ? $"{arabicTwos[groupLevel]}" : $"{arabicAppendedTwos[groupLevel]}");
				}
				else
				{
					if (text != string.Empty)
					{
						text += " ظˆ ";
					}
					text = ((num == 1 && groupLevel > 0) ? (text + arabicGroup[groupLevel]) : (((num != 1 && num != 2) || (groupLevel != 0 && groupLevel != -1) || num2 != 0 || !(remainingNumber == 0m)) ? (text + GetDigitFeminineStatus(num, groupLevel)) : (text + string.Empty)));
				}
			}
			else
			{
				int num3 = num % 10;
				num = num / 10 - 2;
				if (num3 > 0)
				{
					if (text != string.Empty)
					{
						text += " ظˆ ";
					}
					text += GetDigitFeminineStatus(num3, groupLevel);
				}
				if (text != string.Empty)
				{
					text += " ظˆ ";
				}
				text += arabicTens[num];
			}
		}
		return text;
	}

	public string ConvertToArabic()
	{
		decimal number = Number;
		if (number == 0m)
		{
			return "صفر";
		}
		string text = ProcessArabicGroup(_decimalValue, -1, 0m);
		string text2 = string.Empty;
		byte b = 0;
		while (number >= 1m)
		{
			int num = (int)(number % 1000m);
			number /= 1000m;
			string text3 = ProcessArabicGroup(num, b, Math.Floor(number));
			if (text3 != string.Empty)
			{
				if (b > 0)
				{
					if (text2 != string.Empty)
					{
						text2 = string.Format("{0} {1}", "ظˆ", text2);
					}
					if (num != 2 && num % 100 != 1)
					{
						text2 = ((num >= 3 && num <= 10) ? $"{arabicPluralGroups[b]} {text2}" : ((!(text2 != string.Empty)) ? $"{arabicGroup[b]} {text2}" : $"{arabicAppendedGroup[b]} {text2}"));
					}
				}
				text2 = $"{text3} {text2}";
			}
			b++;
		}
		string empty = string.Empty;
		empty += ((ArabicPrefixText != string.Empty) ? $"{ArabicPrefixText} " : string.Empty);
		empty += ((text2 != string.Empty) ? text2 : string.Empty);
		if (_intergerValue != 0)
		{
			int num2 = (int)(_intergerValue % 100);
			if (num2 == 0)
			{
				empty += Currency.Arabic1CurrencyName;
			}
			else if (num2 == 1)
			{
				empty += Currency.Arabic1CurrencyName;
			}
			else if (num2 == 2)
			{
				empty = ((_intergerValue != 2) ? (empty + Currency.Arabic1CurrencyName) : (empty + Currency.Arabic2CurrencyName));
			}
			else if (num2 >= 3 && num2 <= 10)
			{
				empty += Currency.Arabic310CurrencyName;
			}
			else if (num2 >= 11 && num2 <= 99)
			{
				empty += Currency.Arabic1199CurrencyName;
			}
		}
		empty += ((_decimalValue != 0) ? " ظˆ " : string.Empty);
		empty += ((_decimalValue != 0) ? text : string.Empty);
		if (_decimalValue != 0)
		{
			empty += " ";
			int num3 = _decimalValue % 100;
			if (num3 == 0)
			{
				empty += Currency.Arabic1CurrencyPartName;
			}
			else if (num3 == 1)
			{
				empty += Currency.Arabic1CurrencyPartName;
			}
			else if (num3 == 2)
			{
				empty += Currency.Arabic2CurrencyPartName;
			}
			else if (num3 >= 3 && num3 <= 10)
			{
				empty += Currency.Arabic310CurrencyPartName;
			}
			else if (num3 >= 11 && num3 <= 99)
			{
				empty += Currency.Arabic1199CurrencyPartName;
			}
		}
		return empty + ((ArabicSuffixText != string.Empty) ? $" {ArabicSuffixText}" : string.Empty);
	}
}


