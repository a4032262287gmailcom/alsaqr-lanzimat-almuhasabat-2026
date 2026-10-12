using System.Collections.Generic;
using System.Drawing;

namespace GTSErpSystem.Helper;

public class ReturnSentInvoiceToZakat
{
	public bool IsSuccess { get; set; }

	public Bitmap Image { get; set; }

	public List<string[]> MessageErrors { get; set; }

	public List<string[]> MessageWarning { get; set; }

	public static List<string[]> RetMessageErrors(params string[] error)
	{
		List<string[]> list = new List<string[]>();
		list.Add(error);
		return list;
	}

	public static List<string[]> RetMessageWarning(params string[] error)
	{
		List<string[]> list = new List<string[]>();
		list.Add(error);
		return list;
	}
}


