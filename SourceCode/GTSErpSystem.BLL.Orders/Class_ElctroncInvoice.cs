using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Orders;

public class Class_ElctroncInvoice
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<ResultElectronicInvoiceXml> GetAllZakat()
	{
		return db.ResultElectronicInvoiceXmls.OrderByDescending((ResultElectronicInvoiceXml x) => x.Id).ToList();
	}

	public List<ResultElectronicInvoiceXml> GetAllZakatNotUploaded()
	{
		return (from x in db.ResultElectronicInvoiceXmls
			where x.Uploaded == (bool?)false
			orderby x.Id descending
			select x).ToList();
	}

	public void DeleteZakatById(string id)
	{
		ResultElectronicInvoiceXml resultElectronicInvoiceXml = db.ResultElectronicInvoiceXmls.FirstOrDefault((ResultElectronicInvoiceXml x) => x.OrderId == id);
		if (resultElectronicInvoiceXml != null)
		{
			db.ResultElectronicInvoiceXmls.Remove(resultElectronicInvoiceXml);
			db.SaveChanges();
		}
	}

	public void UpdateZakatById(int id, string newQrCode)
	{
		ResultElectronicInvoiceXml resultElectronicInvoiceXml = db.ResultElectronicInvoiceXmls.FirstOrDefault((ResultElectronicInvoiceXml x) => x.Id == id);
		if (resultElectronicInvoiceXml != null)
		{
			resultElectronicInvoiceXml.QRCode = newQrCode;
			resultElectronicInvoiceXml.Uploaded = true;
			resultElectronicInvoiceXml.UploadedDate = DateTime.Now;
			db.SaveChanges();
		}
	}

	public int? GetIdByOrderId(int orderId)
	{
		string OrederID = orderId.ToString();
		return db.ResultElectronicInvoiceXmls.FirstOrDefault((ResultElectronicInvoiceXml x) => x.OrderId == OrederID)?.Id;
	}

	public ResultElectronicInvoiceXml GetRowById(int id)
	{
		return db.ResultElectronicInvoiceXmls.FirstOrDefault((ResultElectronicInvoiceXml x) => x.Id == id);
	}
}


