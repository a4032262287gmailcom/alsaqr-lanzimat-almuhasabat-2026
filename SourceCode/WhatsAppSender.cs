using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

public static class WhatsAppSender
{
	private static readonly string AccessToken = "YOUR_ACCESS_TOKEN";

	private static readonly string PhoneNumberId = "YOUR_PHONE_NUMBER_ID";

	private static readonly string ApiVersion = "21.0";

	public static async Task<bool> SendDocumentLinkAsync(string toPhone, string pdfUrl, string caption)
	{
		try
		{
			using HttpClient http = new HttpClient();
			http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
			string requestUrl = "https://graph.facebook.com/v" + ApiVersion + "/" + PhoneNumberId + "/messages";
			var payload = new
			{
				messaging_product = "whatsapp",
				to = toPhone,
				type = "document",
				document = new
				{
					link = pdfUrl,
					caption = caption
				}
			};
			string json = JsonConvert.SerializeObject(payload);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			HttpResponseMessage resp = await http.PostAsync(requestUrl, content);
			string respBody = await resp.Content.ReadAsStringAsync();
			if (resp.IsSuccessStatusCode)
			{
				return true;
			}
			MessageBox.Show("فشل إرسال رسالة واتساب:\n" + respBody, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show("استثناء أثناء إرسال واتساب:\n" + ex2.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
	}

	public static void SendTextMessage(string toNumber, string message)
	{
		try
		{
			string requestUriString = "https://graph.facebook.com/" + ApiVersion + "/" + PhoneNumberId + "/messages";
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Headers["Authorization"] = "Bearer " + AccessToken;
			string value = "\r\n            {\r\n                \"messaging_product\": \"whatsapp\",\r\n                \"to\": \"" + toNumber + "\",\r\n                \"type\": \"text\",\r\n                \"text\": { \"body\": \"" + message + "\" }\r\n            }";
			using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				streamWriter.Write(value);
			}
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			using StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
			string text = streamReader.ReadToEnd();
			Console.WriteLine("âœ… Response: " + text);
		}
		catch (Exception ex)
		{
			Console.WriteLine("âڑ \ufe0f Error: " + ex.Message);
		}
	}
}


