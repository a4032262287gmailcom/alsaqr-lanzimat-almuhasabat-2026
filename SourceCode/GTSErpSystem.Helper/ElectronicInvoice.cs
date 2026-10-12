using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using GTSErpSystem.Dto;
using ZXing;
using ZXing.Common;
using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.APIHelper;
using ZatcaIntegrationSDK.BLL;
using ZatcaIntegrationSDK.HelperContracts;

namespace GTSErpSystem.Helper;

public class ElectronicInvoice
{
	public Bitmap QrCodeImage(string Qrcode, int width = 250, int height = 250)
	{
		BarcodeWriter barcodeWriter = new BarcodeWriter
		{
			Format = BarcodeFormat.QR_CODE,
			Options = new EncodingOptions
			{
				Width = width,
				Height = height
			}
		};
		return barcodeWriter.Write(Qrcode);
	}

	public byte[] QrCodeToByteArray(string Qrcode, int width = 250, int height = 250)
	{
		BarcodeWriter barcodeWriter = new BarcodeWriter
		{
			Format = BarcodeFormat.QR_CODE,
			Options = new EncodingOptions
			{
				Width = width,
				Height = height
			}
		};
		Bitmap bitmap = barcodeWriter.Write(Qrcode);
		using MemoryStream memoryStream = new MemoryStream();
		bitmap.Save(memoryStream, ImageFormat.Bmp);
		return memoryStream.ToArray();
	}

	public async Task<ZatcaIntegrationSDK.Result> CreateXml(InvoiceDto invoiceDto, string invoiceHash, SupplierPartyDto supplier, CustomerPartyDto customer, List<InvoiceItems> invlines, CSIDInfoDto cSIDInfoDto)
	{
		UBLXML ubl = new UBLXML();
		ZatcaIntegrationSDK.Invoice inv = new ZatcaIntegrationSDK.Invoice();
		ZatcaIntegrationSDK.Result res = new ZatcaIntegrationSDK.Result();
		InvoiceReportingRequest invrequestbody = new InvoiceReportingRequest();
		inv.ID = invoiceDto.ID;
		inv.IssueDate = invoiceDto.IssueDate;
		inv.IssueTime = invoiceDto.IssueTime;
		inv.invoiceTypeCode.id = invoiceDto.InvoiceType;
		inv.invoiceTypeCode.Name = invoiceDto.InvoiceTypeName;
		inv.DocumentCurrencyCode = invoiceDto.DocumentCurrencyCode;
		inv.TaxCurrencyCode = invoiceDto.TaxCurrencyCode;
		if (inv.invoiceTypeCode.id == 383 || inv.invoiceTypeCode.id == 381)
		{
			InvoiceDocumentReference invoiceDocumentReference = new InvoiceDocumentReference
			{
				ID = "Invoice Number: " + invoiceDto.OrderId + "; Invoice Issue Date:" + invoiceDto.IssueDate
			};
			inv.billingReference.invoiceDocumentReferences.Add(invoiceDocumentReference);
		}
		inv.AdditionalDocumentReferencePIH.EmbeddedDocumentBinaryObject = invoiceHash;
		inv.AdditionalDocumentReferenceICV.UUID = int.Parse(invoiceDto.UUID);
		if (inv.invoiceTypeCode.Name.Substring(0, 2) == "01")
		{
			inv.delivery.ActualDeliveryDate = invoiceDto.IssueDate;
			inv.delivery.LatestDeliveryDate = invoiceDto.IssueDate;
		}
		if (invoiceDto.PaymentCode.Count > 0)
		{
			foreach (string item in invoiceDto.PaymentCode)
			{
				PaymentMeans paymentMeans = new PaymentMeans
				{
					PaymentMeansCode = item
				};
				if (inv.invoiceTypeCode.id == 383 || inv.invoiceTypeCode.id == 381)
				{
					switch (item)
					{
					case "10":
						paymentMeans.InstructionNote = "Payment made in cash.";
						break;
					case "48":
						paymentMeans.InstructionNote = "Payment made via bank card.";
						break;
					case "42":
						paymentMeans.InstructionNote = "Payment made via bank transfer.";
						break;
					case "30":
						paymentMeans.InstructionNote = "Payment due within Some days";
						break;
					case "1":
						paymentMeans.InstructionNote = "Paid via PayPal";
						break;
					}
				}
				inv.paymentmeans.Add(paymentMeans);
			}
		}
		inv.SupplierParty.partyIdentification.ID = supplier.ID;
		inv.SupplierParty.partyIdentification.schemeID = supplier.schemeID;
		inv.SupplierParty.postalAddress.StreetName = supplier.StreetName;
		inv.SupplierParty.postalAddress.AdditionalStreetName = supplier.AdditionalStreetName;
		inv.SupplierParty.postalAddress.BuildingNumber = supplier.BuildingNumber;
		inv.SupplierParty.postalAddress.PlotIdentification = supplier.PlotIdentification;
		inv.SupplierParty.postalAddress.CityName = supplier.CityName;
		inv.SupplierParty.postalAddress.PostalZone = supplier.PostalZone;
		inv.SupplierParty.postalAddress.CountrySubentity = supplier.CountrySubentity;
		inv.SupplierParty.postalAddress.CitySubdivisionName = supplier.CitySubdivisionName;
		inv.SupplierParty.postalAddress.country.IdentificationCode = supplier.IdentificationCode;
		inv.SupplierParty.partyLegalEntity.RegistrationName = supplier.RegistrationName;
		inv.SupplierParty.partyTaxScheme.CompanyID = supplier.CompanyID;
		inv.CustomerParty.postalAddress.StreetName = customer.StreetName;
		inv.CustomerParty.postalAddress.AdditionalStreetName = customer.AdditionalStreetName;
		inv.CustomerParty.postalAddress.BuildingNumber = customer.BuildingNumber;
		inv.CustomerParty.postalAddress.PlotIdentification = customer.PlotIdentification;
		inv.CustomerParty.postalAddress.CityName = customer.CityName;
		inv.CustomerParty.postalAddress.PostalZone = customer.PostalZone;
		inv.CustomerParty.postalAddress.CountrySubentity = customer.CountrySubentity;
		inv.CustomerParty.postalAddress.CitySubdivisionName = customer.CitySubdivisionName;
		inv.CustomerParty.postalAddress.country.IdentificationCode = customer.IdentificationCode;
		inv.CustomerParty.partyLegalEntity.RegistrationName = customer.RegistrationName;
		inv.CustomerParty.partyTaxScheme.CompanyID = customer.CompanyID;
		inv.CustomerParty.contact.Name = customer.Name;
		inv.CustomerParty.contact.Telephone = customer.Telephone;
		inv.CustomerParty.contact.ElectronicMail = customer.ElectronicMail;
		inv.CustomerParty.contact.Note = customer.Note;
		decimal invoicediscount = 0m;
		decimal.TryParse(invoiceDto.DiscountValue.ToString(), out invoicediscount);
		decimal payableamount = 0m;
		decimal.TryParse(invoiceDto.PayableAmount.ToString(), out payableamount);
		inv.legalMonetaryTotal.PayableAmount = payableamount;
		foreach (InvoiceItems item2 in invlines)
		{
			InvoiceLine invline = new InvoiceLine
			{
				InvoiceQuantity = item2.ProductQuantity.Value,
				item = 
				{
					Name = item2.ProductName
				}
			};
			decimal? vatPercentage = item2.VatPercentage;
			if ((vatPercentage.GetValueOrDefault() == 0m) & vatPercentage.HasValue)
			{
				invline.item.classifiedTaxCategory.ID = item2.VatCode;
				invline.taxTotal.TaxSubtotal.taxCategory.ID = item2.VatCode;
				invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReasonCode = item2.TaxExemptionReasonCode;
				invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReason = item2.TaxExemptionReason;
			}
			else
			{
				invline.item.classifiedTaxCategory.ID = item2.VatCode;
				invline.taxTotal.TaxSubtotal.taxCategory.ID = item2.VatCode;
			}
			invline.item.classifiedTaxCategory.Percent = item2.VatPercentage.Value;
			invline.taxTotal.TaxSubtotal.taxCategory.Percent = item2.VatPercentage.Value;
			invline.price.EncludingVat = false;
			invline.price.PriceAmount = item2.ProductPrice.Value;
			vatPercentage = item2.DiscountValue;
			if ((vatPercentage.GetValueOrDefault() > 0m) & vatPercentage.HasValue)
			{
				AllowanceCharge allowanceCharge = new AllowanceCharge
				{
					ChargeIndicator = false,
					AllowanceChargeReason = "discount",
					Amount = item2.DiscountValue.Value,
					MultiplierFactorNumeric = 0m,
					BaseAmount = 0m
				};
				invline.allowanceCharges.Add(allowanceCharge);
			}
			inv.InvoiceLines.Add(invline);
			if (invoicediscount > 0m)
			{
				AllowanceCharge allowance = new AllowanceCharge
				{
					ChargeIndicator = false,
					MultiplierFactorNumeric = 0m,
					BaseAmount = 0m,
					Amount = invoicediscount,
					AllowanceChargeReasonCode = "",
					AllowanceChargeReason = "discount",
					taxCategory = 
					{
						ID = item2.VatCode,
						Percent = 15m
					}
				};
				inv.allowanceCharges.Add(allowance);
			}
		}
		inv.cSIDInfo.CertPem = cSIDInfoDto.CertPem;
		inv.cSIDInfo.PrivateKey = cSIDInfoDto.PrivateKey;
		try
		{
			string pathXml = Directory.GetCurrentDirectory() + "\\wwwroot";
			bool savexmlfile = true;
			res = ubl.GenerateInvoiceXML(inv, pathXml, savexmlfile);
			invrequestbody.invoice = res.EncodedInvoice;
			invrequestbody.invoiceHash = res.InvoiceHash;
			invrequestbody.uuid = res.UUID;
			return res;
		}
		catch (Exception)
		{
			return res;
		}
	}

	public async Task<ReturnSentInvoiceToZakat> SentInvoiceToZakat(ZatcaIntegrationSDK.Result res, CSIDInfoDto cSIDInfo, string invoiceTypeName, Mode mode)
	{
		ApiRequestLogic apireqlogic = new ApiRequestLogic(mode);
		InvoiceReportingRequest invrequestbody = new InvoiceReportingRequest
		{
			invoice = res.EncodedInvoice,
			invoiceHash = res.InvoiceHash,
			uuid = res.UUID
		};
		ReturnSentInvoiceToZakat returnSentInvoiceToZakat;
		if (mode == Mode.developer)
		{
			new ComplianceCsrResponse();
			string csr = cSIDInfo.CSR;
			ComplianceCsrResponse tokenresponse = apireqlogic.GetComplianceCSIDAPI("12345", csr);
			if (string.IsNullOrEmpty(tokenresponse.ErrorMessage))
			{
				InvoiceReportingResponse responsemodel = apireqlogic.CallComplianceInvoiceAPI(tokenresponse.BinarySecurityToken, tokenresponse.Secret, invrequestbody);
				if (responsemodel.IsSuccess)
				{
					if (responsemodel.StatusCode == 202)
					{
					}
					returnSentInvoiceToZakat = new ReturnSentInvoiceToZakat();
					returnSentInvoiceToZakat.IsSuccess = responsemodel.IsSuccess;
					returnSentInvoiceToZakat.Image = QrCodeImage(res.QRCode, 200, 200);
					returnSentInvoiceToZakat.MessageErrors = ReturnSentInvoiceToZakat.RetMessageErrors(responsemodel.ReportingStatus + responsemodel.ClearanceStatus);
					returnSentInvoiceToZakat.MessageWarning = ReturnSentInvoiceToZakat.RetMessageWarning(responsemodel.WarningMessage);
					return returnSentInvoiceToZakat;
				}
				returnSentInvoiceToZakat = new ReturnSentInvoiceToZakat();
				returnSentInvoiceToZakat.IsSuccess = responsemodel.IsSuccess;
				returnSentInvoiceToZakat.MessageErrors = ReturnSentInvoiceToZakat.RetMessageErrors(responsemodel.ErrorMessage);
				return returnSentInvoiceToZakat;
			}
			returnSentInvoiceToZakat = new ReturnSentInvoiceToZakat();
			returnSentInvoiceToZakat.IsSuccess = false;
			returnSentInvoiceToZakat.MessageErrors = ReturnSentInvoiceToZakat.RetMessageErrors(tokenresponse.ErrorMessage);
			return returnSentInvoiceToZakat;
		}
		if (invoiceTypeName.Substring(0, 2) == "01")
		{
			InvoiceClearanceResponse responsemodel2 = apireqlogic.CallClearanceAPI(Utility.ToBase64Encode(cSIDInfo.CertPem), cSIDInfo.Secretkey, invrequestbody);
			if (responsemodel2.IsSuccess)
			{
				if (responsemodel2.StatusCode == 202)
				{
				}
				returnSentInvoiceToZakat = new ReturnSentInvoiceToZakat();
				returnSentInvoiceToZakat.IsSuccess = responsemodel2.IsSuccess;
				returnSentInvoiceToZakat.MessageErrors = ReturnSentInvoiceToZakat.RetMessageErrors(responsemodel2.ClearanceStatus, responsemodel2.QRCode);
				returnSentInvoiceToZakat.Image = QrCodeImage(responsemodel2.QRCode);
				return returnSentInvoiceToZakat;
			}
			returnSentInvoiceToZakat = new ReturnSentInvoiceToZakat();
			returnSentInvoiceToZakat.IsSuccess = responsemodel2.IsSuccess;
			returnSentInvoiceToZakat.MessageErrors = ReturnSentInvoiceToZakat.RetMessageErrors(responsemodel2.ErrorMessage);
			returnSentInvoiceToZakat.Image = QrCodeImage(responsemodel2.QRCode);
			return returnSentInvoiceToZakat;
		}
		InvoiceReportingResponse responsemodel3 = apireqlogic.CallReportingAPI(Utility.ToBase64Encode(cSIDInfo.CertPem), cSIDInfo.Secretkey, invrequestbody);
		if (responsemodel3.IsSuccess)
		{
			if (responsemodel3.StatusCode == 202)
			{
			}
			returnSentInvoiceToZakat = new ReturnSentInvoiceToZakat();
			returnSentInvoiceToZakat.IsSuccess = responsemodel3.IsSuccess;
			returnSentInvoiceToZakat.MessageErrors = ReturnSentInvoiceToZakat.RetMessageErrors(responsemodel3.ReportingStatus);
			returnSentInvoiceToZakat.Image = QrCodeImage(res.QRCode);
			return returnSentInvoiceToZakat;
		}
		returnSentInvoiceToZakat = new ReturnSentInvoiceToZakat();
		returnSentInvoiceToZakat.IsSuccess = responsemodel3.IsSuccess;
		returnSentInvoiceToZakat.MessageErrors = ReturnSentInvoiceToZakat.RetMessageErrors(responsemodel3.ErrorMessage);
		return returnSentInvoiceToZakat;
	}
}


