using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GTSErpSystem.Dto;
using ZatcaIntegrationSDK;

namespace GTSErpSystem.Helper;

public class ResultElectronicInvoiceXmlService
{
	private GTSdbEntities _context = new GTSdbEntities();

	public async Task<int> AddResult(int userId, bool CsIdForUser, Result result, int branchId, bool uploaded, string orderId, DateTime created, string invoiceTypeName, string FatoraType)
	{
		if (result == null)
		{
			return 0;
		}
		ResultElectronicInvoiceXml invoiceXml = GenerateInvoice(result, branchId, uploaded, orderId, created, invoiceTypeName, FatoraType);
		_context.ResultElectronicInvoiceXmls.Add(invoiceXml);
		await _context.SaveChangesAsync();
		await UpdateInvoiceHash(new CertificateRequestDto
		{
			InvoiceHash = result.InvoiceHash,
			BranchId = branchId,
			CsIdForUser = CsIdForUser,
			UserId = userId
		});
		return invoiceXml.Id;
	}

	private Result GenerateResult(ResultElectronicInvoiceXml resultElectronic)
	{
		return new Result
		{
			QRCode = resultElectronic.QRCode,
			AllowanceTotalAmount = resultElectronic.AllowanceTotalAmount,
			ChargeTotalAmount = resultElectronic.ChargeTotalAmount,
			EncodedInvoice = resultElectronic.EncodedInvoice,
			InvoiceHash = resultElectronic.InvoiceHash,
			IsValid = resultElectronic.IsValid,
			LineExtensionAmount = resultElectronic.LineExtensionAmount,
			SingedXMLFileNameFullPath = resultElectronic.SingedXMLFileNameFullPath,
			UUID = resultElectronic.UUID,
			TaxInclusiveAmount = resultElectronic.TaxInclusiveAmount,
			PayableAmount = resultElectronic.PayableAmount,
			TaxAmount = resultElectronic.TaxAmount,
			TaxExclusiveAmount = resultElectronic.TaxExclusiveAmount,
			ErrorMessage = resultElectronic.ErrorMessage,
			SingedXMLFileName = resultElectronic.SingedXMLFileName,
			SingedXML = resultElectronic.SingedXML,
			ResultedValue = resultElectronic.ResultedValue,
			PrepaidAmount = resultElectronic.PrepaidAmount,
			SingedXMLFileNameShortPath = resultElectronic.SingedXMLFileNameShortPath,
			PIH = resultElectronic.PIH,
			Operation = resultElectronic.Operation,
			NormalXMLFileNameShortPath = resultElectronic.NormalXMLFileNameShortPath,
			NormalXMLFileNameFullPath = resultElectronic.NormalXMLFileNameFullPath
		};
	}

	private ResultElectronicInvoiceXml GenerateInvoice(Result result, int branchId, bool uploaded, string orderId, DateTime created, string invoiceTypeName, string FatoraType)
	{
		return new ResultElectronicInvoiceXml
		{
			QRCode = result.QRCode,
			AllowanceTotalAmount = result.AllowanceTotalAmount,
			BranchId = branchId,
			ChargeTotalAmount = result.ChargeTotalAmount,
			EncodedInvoice = result.EncodedInvoice,
			InvoiceHash = result.InvoiceHash,
			IsValid = result.IsValid,
			LineExtensionAmount = result.LineExtensionAmount,
			SingedXMLFileNameFullPath = result.SingedXMLFileNameFullPath,
			UUID = result.UUID,
			TaxInclusiveAmount = Math.Round(Convert.ToDecimal(result.TaxInclusiveAmount), 1).ToString(),
			PayableAmount = Math.Round(Convert.ToDecimal(result.PayableAmount), 1).ToString(),
			TaxAmount = result.TaxAmount,
			TaxExclusiveAmount = result.TaxExclusiveAmount,
			Uploaded = uploaded,
			OrderId = orderId,
			Created = created,
			ErrorMessage = result.ErrorMessage,
			NormalXMLFileNameFullPath = result.NormalXMLFileNameFullPath,
			NormalXMLFileNameShortPath = result.NormalXMLFileNameShortPath,
			SingedXMLFileNameShortPath = result.SingedXMLFileNameShortPath,
			Operation = result.Operation,
			PIH = result.PIH,
			PrepaidAmount = result.PrepaidAmount,
			ResultedValue = result.ResultedValue,
			SingedXML = result.SingedXML,
			SingedXMLFileName = result.SingedXMLFileName,
			InvoiceTypeName = invoiceTypeName,
			FatoraType = FatoraType
		};
	}

	public async Task<int> UpdateInvoiceHash(CertificateRequestDto requestDto)
	{
		if (requestDto.CsIdForUser)
		{
			User_Login user = _context.User_Login.FirstOrDefault((User_Login s) => (int?)s.ID == requestDto.UserId);
			if (user != null)
			{
				user.InvoiceHash = requestDto.InvoiceHash;
				_context.Entry(user).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return 1;
			}
		}
		else
		{
			TblSetting branch = _context.TblSettings.FirstOrDefault((TblSetting s) => (int?)s.ID == requestDto.BranchId);
			if (branch != null)
			{
				branch.InvoiceHash = requestDto.InvoiceHash;
				_context.Entry(branch).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return 1;
			}
		}
		return 0;
	}
}


