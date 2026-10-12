using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace GTSErpSystem.Helper;

public static class TwilioHelper
{
	private static readonly string AccountSid = LoginDetails.AccountSid;

	private static readonly string AuthToken = LoginDetails.AuthToken;

	private static readonly string FromNumber = LoginDetails.FromPhone;

	public static bool SendText(string toPhoneNumber, string messageBody, out string statusMessage)
	{
		try
		{
			TwilioClient.Init(AccountSid, AuthToken);
			PhoneNumber phoneNumber = new PhoneNumber("whatsapp:" + FromNumber);
			MessageResource messageResource = MessageResource.Create(new PhoneNumber("whatsapp:" + toPhoneNumber), null, phoneNumber, null, messageBody);
			statusMessage = "تم إرسال الرسالة بنجاح" + messageResource.Sid;
			return true;
		}
		catch (Exception ex)
		{
			statusMessage = " فشل الإرسال" + ex.Message;
			return false;
		}
	}

	public static bool SendPdf(string toPhoneNumber, string pdfUrl, int OrderNum, decimal Net, string docType, out string statusMessage)
	{
		try
		{
			TwilioClient.Init(AccountSid, AuthToken);
			string body = $"تم إنشاء {docType} رقم {OrderNum}\nقيمة الـ {docType}: {Net} ريال";
			PhoneNumber phoneNumber = new PhoneNumber("whatsapp:" + FromNumber);
			MessageResource messageResource = MessageResource.Create(new PhoneNumber("whatsapp:" + toPhoneNumber), null, phoneNumber, null, body, new List<Uri>
			{
				new Uri(pdfUrl)
			});
			statusMessage = "تم إرسال الرسالة بنجاح: " + messageResource.Sid;
			return true;
		}
		catch (Exception ex)
		{
			statusMessage = "فشل الإرسال: " + ex.Message;
			return false;
		}
	}
}


