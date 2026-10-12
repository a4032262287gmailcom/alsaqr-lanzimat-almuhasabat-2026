using System;
using System.Globalization;

namespace GTSErpSystem.BLL.Main;

public class Class_GetDate
{
	public DateTime GetDate()
	{
		string value = DateTime.Now.ToString("yyyy/MM/dd", new CultureInfo("ar-JO"));
		return Convert.ToDateTime(value);
	}
}


