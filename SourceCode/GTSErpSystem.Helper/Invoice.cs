using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace GTSErpSystem.Helper;

public class Invoice
{
	public List<LineItem> LineItems = new List<LineItem>();

	public string Number { get; set; }

	public DateTime InvoiceDate { get; set; }

	public CurrencyCode Currency { get; set; }

	public InvoiceType Type { get; set; }

	public TradeParty Buyer { get; set; }

	public TradeParty Seller { get; set; }

	public Profile Profile { get; set; }

	public float TotalAmount { get; set; }

	public DateTime DueDate { get; set; } = DateTime.Now.AddDays(30.0);

	public float ApplicableTax { get; set; } = 0.19f;

	public Invoice(string invoiceNumber, DateTime invoiceDate, CurrencyCode currency)
	{
		Number = invoiceNumber;
		InvoiceDate = invoiceDate;
		Currency = currency;
	}

	public string CreateXml()
	{
		MemoryStream memoryStream = new MemoryStream();
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
		xmlTextWriter.Formatting = Formatting.Indented;
		xmlTextWriter.WriteStartDocument();
		RenderDocumentHeader(xmlTextWriter);
		RenderExchangeDocument(xmlTextWriter);
		xmlTextWriter.WriteStartElement("rsm:SupplyChainTradeTransaction");
		AddTradeLineItems(xmlTextWriter);
		xmlTextWriter.WriteStartElement("ram:ApplicableHeaderTradeAgreement");
		RenderUserDetails(xmlTextWriter, "ram:SellerTradeParty", Seller);
		RenderUserDetails(xmlTextWriter, "ram:BuyerTradeParty", Buyer);
		xmlTextWriter.WriteEndElement();
		RenderTradeDelivery(xmlTextWriter);
		RenderTradeSettlement(xmlTextWriter);
		RenderMonetarySummation(xmlTextWriter);
		xmlTextWriter.WriteEndElement();
		xmlTextWriter.WriteEndDocument();
		xmlTextWriter.Flush();
		memoryStream.Position = 0L;
		return Encoding.UTF8.GetString(memoryStream.ToArray());
	}

	private void AddTradeLineItems(XmlTextWriter writer)
	{
		foreach (LineItem lineItem in LineItems)
		{
			writer.WriteStartElement("ram:IncludedSupplyChainTradeLineItem");
			writer.WriteStartElement("ram:AssociatedDocumentLineDocument");
			RenderOptionalElement(writer, "ram:LineID", "1");
			writer.WriteEndElement();
			writer.WriteStartElement("ram:SpecifiedTradeProduct");
			RenderOptionalElement(writer, "ram:Name", lineItem.Name);
			writer.WriteEndElement();
			if (Profile != Profile.Basic)
			{
				writer.WriteStartElement("ram:SpecifiedLineTradeAgreement");
				writer.WriteStartElement("ram:NetPriceProductTradePrice");
				RenderOptionalElement(writer, "ram:ChargeAmount", lineItem.Price.ToString());
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			writer.WriteStartElement("ram:SpecifiedLineTradeDelivery");
			RenderAttribute(writer, "ram:BilledQuantity", "unitCode", lineItem.UnitCode.ToString(), lineItem.Quantity.ToString());
			writer.WriteEndElement();
			writer.WriteStartElement("ram:SpecifiedLineTradeSettlement");
			writer.WriteStartElement("ram:ApplicableTradeTax");
			RenderOptionalElement(writer, "ram:TypeCode", "VAT");
			RenderOptionalElement(writer, "ram:CategoryCode", "S");
			RenderOptionalElement(writer, "ram:RateApplicablePercent", "19");
			writer.WriteEndElement();
			writer.WriteStartElement("ram:SpecifiedTradeSettlementLineMonetarySummation");
			RenderOptionalElement(writer, "ram:LineTotalAmount", FormatValue(lineItem.Price * lineItem.Quantity));
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.WriteEndElement();
		}
	}

	private void RenderAttribute(XmlTextWriter writer, string tagName, string attributeName, string attributeValue, string nodeValue)
	{
		writer.WriteStartElement(tagName);
		writer.WriteAttributeString(attributeName, attributeValue);
		writer.WriteValue(nodeValue);
		writer.WriteEndElement();
	}

	private void RenderOptionalElement(XmlTextWriter writer, string tagName, string value)
	{
		if (!string.IsNullOrEmpty(value))
		{
			writer.WriteElementString(tagName, value);
		}
	}

	private void RenderOptionalAmount(XmlTextWriter writer, string tagName, float value, int decimals = 2, bool renderCurrency = false)
	{
		if (value != float.MinValue)
		{
			writer.WriteStartElement(tagName);
			if (renderCurrency)
			{
				writer.WriteAttributeString("currencyID", Currency.ToString("g"));
			}
			writer.WriteValue(FormatValue(value, decimals));
			writer.WriteEndElement();
		}
	}

	private void RenderUserDetails(XmlTextWriter writer, string customerTag, TradeParty user)
	{
		if (user == null)
		{
			return;
		}
		writer.WriteStartElement(customerTag);
		if (!string.IsNullOrEmpty(user.Name))
		{
			writer.WriteElementString("ram:Name", user.Name);
		}
		writer.WriteStartElement("ram:PostalTradeAddress");
		writer.WriteElementString("ram:PostcodeCode", user.Postcode);
		writer.WriteElementString("ram:LineOne", string.IsNullOrEmpty(user.ContactName) ? user.Street : user.ContactName);
		if (!string.IsNullOrEmpty(user.ContactName))
		{
			writer.WriteElementString("ram:LineTwo", user.Street);
		}
		writer.WriteElementString("ram:CityName", user.City);
		writer.WriteElementString("ram:CountryID", user.Country.ToString("g"));
		writer.WriteEndElement();
		foreach (TaxID specifiedTaxRegistration in user.SpecifiedTaxRegistrations)
		{
			writer.WriteStartElement("ram:SpecifiedTaxRegistration");
			RenderAttribute(writer, "ram:ID", "schemeID", specifiedTaxRegistration.Scheme.ToString(), specifiedTaxRegistration.ID);
			writer.WriteEndElement();
		}
		writer.WriteEndElement();
	}

	private void RenderTradeDelivery(XmlTextWriter writer)
	{
		writer.WriteStartElement("ram:ApplicableHeaderTradeDelivery");
		writer.WriteStartElement("ram:ActualDeliverySupplyChainEvent");
		writer.WriteStartElement("ram:OccurrenceDateTime");
		RenderAttribute(writer, "udt:DateTimeString", "format", "102", ConvertDateFormat(DateTime.Now));
		writer.WriteEndElement();
		writer.WriteEndElement();
		writer.WriteEndElement();
	}

	private void RenderTradeSettlement(XmlTextWriter writer)
	{
		writer.WriteStartElement("ram:ApplicableHeaderTradeSettlement");
		writer.WriteElementString("ram:InvoiceCurrencyCode", Currency.ToString("g"));
		writer.WriteStartElement("ram:ApplicableTradeTax");
		RenderOptionalAmount(writer, "ram:CalculatedAmount", TotalAmount * ApplicableTax);
		writer.WriteElementString("ram:TypeCode", "VAT");
		RenderOptionalAmount(writer, "ram:BasisAmount", TotalAmount);
		writer.WriteElementString("ram:CategoryCode", "S");
		writer.WriteElementString("ram:RateApplicablePercent", (ApplicableTax * 100f).ToString());
		writer.WriteEndElement();
		writer.WriteStartElement("ram:SpecifiedTradePaymentTerms");
		writer.WriteStartElement("ram:DueDateDateTime");
		RenderAttribute(writer, "udt:DateTimeString", "format", "102", ConvertDateFormat(DueDate));
		writer.WriteEndElement();
		writer.WriteEndElement();
	}

	private void RenderMonetarySummation(XmlTextWriter writer)
	{
		writer.WriteStartElement("ram:SpecifiedTradeSettlementHeaderMonetarySummation");
		RenderOptionalAmount(writer, "ram:LineTotalAmount", TotalAmount);
		RenderOptionalAmount(writer, "ram:ChargeTotalAmount", 0f);
		RenderOptionalAmount(writer, "ram:AllowanceTotalAmount", 0f);
		RenderOptionalAmount(writer, "ram:TaxBasisTotalAmount", TotalAmount);
		RenderOptionalAmount(writer, "ram:TaxTotalAmount", TotalAmount * ApplicableTax, 2, renderCurrency: true);
		RenderOptionalAmount(writer, "ram:GrandTotalAmount", TotalAmount + TotalAmount * ApplicableTax);
		RenderOptionalAmount(writer, "ram:DuePayableAmount", TotalAmount + TotalAmount * ApplicableTax);
		writer.WriteEndElement();
	}

	private void RenderDocumentHeader(XmlTextWriter writer)
	{
		writer.WriteStartElement("rsm:CrossIndustryInvoice");
		writer.WriteAttributeString("xmlns", "a", null, "urn:un:unece:uncefact:data:standard:QualifiedDataType:100");
		writer.WriteAttributeString("xmlns", "rsm", null, "urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100");
		writer.WriteAttributeString("xmlns", "qdt", null, "urn:un:unece:uncefact:data:standard:QualifiedDataType:10");
		writer.WriteAttributeString("xmlns", "ram", null, "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100");
		writer.WriteAttributeString("xmlns", "xs", null, "http://www.w3.org/2001/XMLSchema");
		writer.WriteAttributeString("xmlns", "udt", null, "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:100");
		writer.WriteStartElement("rsm:ExchangedDocumentContext");
		writer.WriteStartElement("ram:GuidelineSpecifiedDocumentContextParameter");
		writer.WriteElementString("ram:ID", "urn:cen.eu:en16931:2017#compliant#urn:zugferd.de:2p0:" + Profile.ToString().ToLower());
		writer.WriteEndElement();
		writer.WriteEndElement();
	}

	private void RenderExchangeDocument(XmlTextWriter writer)
	{
		writer.WriteStartElement("rsm:ExchangedDocument");
		writer.WriteElementString("ram:ID", Number);
		writer.WriteElementString("ram:TypeCode", $"{(int)Type}");
		writer.WriteStartElement("ram:IssueDateTime");
		writer.WriteStartElement("udt:DateTimeString");
		writer.WriteAttributeString("format", "102");
		writer.WriteValue(ConvertDateFormat(InvoiceDate));
		writer.WriteEndElement();
		writer.WriteEndElement();
		writer.WriteEndElement();
	}

	private string ConvertDateFormat(DateTime date, string format = "102")
	{
		if (format.Equals("102"))
		{
			return date.ToString("yyyyMMdd");
		}
		return date.ToString("yyyy-MM-ddTHH:mm:ss");
	}

	private string FormatValue(float value, int numDecimals = 2)
	{
		string text = "0.";
		for (int i = 0; i < numDecimals; i++)
		{
			text += "0";
		}
		return value.ToString(text).Replace(",", ".");
	}
}


