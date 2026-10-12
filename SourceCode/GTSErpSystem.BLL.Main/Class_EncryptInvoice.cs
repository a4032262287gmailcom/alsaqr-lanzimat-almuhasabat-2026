using System;
using System.Collections.Generic;
using System.Text;

namespace GTSErpSystem.BLL.Main;

public class Class_EncryptInvoice
{
	public string GenerateQRBase64(string CompanyName, string VatNumber, string InvoiceDate, string Total, string VatAmount)
	{
		string text = Convert.ToDateTime(InvoiceDate).ToString("HH:mm:ss");
		string text2 = Convert.ToDateTime(InvoiceDate).ToString("yyyy-MM-dd");
		string s = (text2 + "T" + text + "Z").ToString();
		byte[] bytes = Encoding.UTF8.GetBytes(CompanyName);
		byte[] bytes2 = Encoding.UTF8.GetBytes(VatNumber);
		byte[] bytes3 = Encoding.UTF8.GetBytes(s);
		byte[] bytes4 = Encoding.UTF8.GetBytes(Total);
		byte[] bytes5 = Encoding.UTF8.GetBytes(VatAmount);
		List<byte> list = new List<byte>();
		list.AddRange(getBytes(1, bytes));
		list.AddRange(getBytes(2, bytes2));
		list.AddRange(getBytes(3, bytes3));
		list.AddRange(getBytes(4, bytes4));
		list.AddRange(getBytes(5, bytes5));
		return Convert.ToBase64String(list.ToArray());
	}

	private byte[] getBytes(int id, byte[] Value)
	{
		byte[] array = new byte[2 + Value.Length];
		array[0] = (byte)id;
		array[1] = (byte)Value.Length;
		Value.CopyTo(array, 2);
		return array;
	}
}


