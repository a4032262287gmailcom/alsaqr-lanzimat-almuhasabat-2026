using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.DAL.Models;
using Newtonsoft.Json;

namespace GTSErpSystem.Frms.ElctorncInvoic;

public class FrmZatcaIntgration : XtraForm
{
	private Class_WaitFormFunc waitForm = new Class_WaitFormFunc();

	private IContainer components = null;

	private SimpleButton BtnSentData;

	private GroupBox groupBox1;

	private SimpleButton BtnGetData;

	private TextBox TxtActivationId;

	private Label label1;

	public FrmZatcaIntgration()
	{
		InitializeComponent();
	}

	private async void BtnSentData_Click(object sender, EventArgs e)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(TxtActivationId.Text))
			{
				MessageBox.Show("يرجي ادخال رقم التفعيل", " â‌Œ فشل الإرسال:\n");
				return;
			}
			waitForm.Show();
			int branchId = LoginDetails.BranchID;
			string apiUrl = LoginDetails.Crm + "/Zakat/ReceiveZakatDesktopDetailsAsync";
			List<GetDataOfZakatByBranchIdStoredProcedureResponse> branchDetails = GetBranchDetails(branchId);
			if (branchDetails == null || branchDetails.Count == 0)
			{
				waitForm.Close();
				MessageBox.Show("لم يتم العثور على بيانات للفرع المحدد.", "تنبيه");
				return;
			}
			var payload = new
			{
				request = "ZakatDesktopData",
				userId = LoginDetails.EmployeeId,
				serialNumber = DecriptData(),
				branchDetails = branchDetails,
				activationId = Convert.ToInt32(TxtActivationId.Text)
			};
			string json = JsonConvert.SerializeObject(payload, Formatting.Indented);
			using HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginDetails.Token);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync(apiUrl, content);
			waitForm.Close();
			if (response.IsSuccessStatusCode)
			{
				if (int.TryParse(await response.Content.ReadAsStringAsync(), out var activationId))
				{
					LoginDetails.ActivationId = activationId;
					MessageBox.Show($"âœ… تم إرسال البيانات بنجاح!\nActivation ID: {activationId}");
				}
				else
				{
					MessageBox.Show("تم الإرسال بنجاح، ولكن لم يتم التعرف على رقم التفعيل.", "تنبيه");
				}
			}
			else
			{
				MessageBox.Show("â‌Œ فشل الإرسال:\n" + await response.Content.ReadAsStringAsync());
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			waitForm.Close();
			MessageBox.Show("حدث خطأ: " + ex2.Message, "خطأ");
		}
	}

	private void BtnGetData_Click(object sender, EventArgs e)
	{
		FrmGetDataFromCRM frmGetDataFromCRM = new FrmGetDataFromCRM();
		Hide();
		frmGetDataFromCRM.ShowDialog();
	}

	private List<GetDataOfZakatByBranchIdStoredProcedureResponse> GetBranchDetails(int branchId)
	{
		List<GetDataOfZakatByBranchIdStoredProcedureResponse> result = new List<GetDataOfZakatByBranchIdStoredProcedureResponse>();
		string connString = LoginDetails.ConnString;
		using (SqlConnection sqlConnection = new SqlConnection(connString))
		{
			using SqlCommand sqlCommand = new SqlCommand("GetDataForZakatToCrm", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.AddWithValue("@BranchId", branchId);
			sqlConnection.Open();
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			Dictionary<int, GetDataOfZakatByBranchIdStoredProcedureResponse> dictionary = new Dictionary<int, GetDataOfZakatByBranchIdStoredProcedureResponse>();
			while (sqlDataReader.Read())
			{
				int key = Convert.ToInt32(sqlDataReader["ID"]);
				if (!dictionary.ContainsKey(key))
				{
					GetDataOfZakatByBranchIdStoredProcedureResponse value = new GetDataOfZakatByBranchIdStoredProcedureResponse
					{
						Id = Convert.ToInt32(sqlDataReader["ID"]),
						Name = sqlDataReader["CompanyName"].ToString(),
						IdentifierID = sqlDataReader["IdentifierID"].ToString(),
						IdentifierSchemeID = sqlDataReader["IdentifierSchemeID"].ToString(),
						StreetName = sqlDataReader["StreetName"].ToString(),
						BuildingNumber = sqlDataReader["BuildingNumber"].ToString(),
						CityName = sqlDataReader["CityName"].ToString(),
						PostalZone = sqlDataReader["PostalZone"].ToString(),
						District = sqlDataReader["District"].ToString(),
						IdentificationCode = sqlDataReader["IdentificationCode"].ToString(),
						CompanyName = sqlDataReader["CompanyName"].ToString(),
						VatNumber = sqlDataReader["VatNumber"].ToString(),
						IdVat = sqlDataReader["VatNumber"].ToString(),
						Location = sqlDataReader["Location"].ToString(),
						BusinessCategory = "DisTop",
						BranchUsersData = new List<BranchUserData>()
					};
					dictionary.Add(key, value);
				}
				BranchUserData item = new BranchUserData
				{
					UserId = Convert.ToInt32(sqlDataReader["UserId"]),
					UserName = sqlDataReader["UserName"].ToString(),
					UserModeInvoice = sqlDataReader["userModeInvoice"].ToString()
				};
				dictionary[key].BranchUsersData.Add(item);
			}
			result = dictionary.Values.ToList();
		}
		return result;
	}

	private string DecriptData()
	{
		string machineName = Environment.MachineName;
		string text = Class_Crypto.SetData("b14ca5898a4e4133bbce2ea2315a1500", "b14ca5898a4e4133bbce2ea2315a1916", MacAddress(), SerialNumber(), HardDisk(), LoginDetails.VergDate, DateTime.Now.ToString("yyyy-MM-dd"), machineName);
		return Class_Crypto.SetDataSecondWay("b14ca5898a4e4133bbce2ea2315a1500", "b14ca5898a4e4133bbce2ea2315a1916", text);
	}

	public static string MacAddress()
	{
		string result = string.Empty;
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		foreach (NetworkInterface networkInterface in allNetworkInterfaces)
		{
			if (networkInterface.OperationalStatus == OperationalStatus.Up)
			{
				result = networkInterface.GetPhysicalAddress().ToString();
				break;
			}
		}
		return result;
	}

	public static string SerialNumber()
	{
		string result = string.Empty;
		ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * from Win32_BaseBoard");
		foreach (ManagementObject item in managementObjectSearcher.Get())
		{
			result = item["SerialNumber"].ToString().Trim();
		}
		return result;
	}

	public static string HardDisk()
	{
		string result = string.Empty;
		string text = Path.GetPathRoot(Environment.SystemDirectory).TrimEnd('\\');
		using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" + text + "'} WHERE ResultClass=Win32_DiskPartition"))
		{
			using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = managementObjectSearcher.Get().GetEnumerator();
			if (managementObjectEnumerator.MoveNext())
			{
				ManagementBaseObject current = managementObjectEnumerator.Current;
				using ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + current["DeviceID"]?.ToString() + "'} WHERE ResultClass=Win32_DiskDrive");
				using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator2 = managementObjectSearcher2.Get().GetEnumerator();
				if (managementObjectEnumerator2.MoveNext())
				{
					ManagementBaseObject current2 = managementObjectEnumerator2.Current;
					result = current2["SerialNumber"].ToString();
				}
			}
		}
		return result;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.ElctorncInvoic.FrmZatcaIntgration));
		this.BtnSentData = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtActivationId = new System.Windows.Forms.TextBox();
		this.BtnGetData = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.BtnSentData.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSentData.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSentData.Appearance.Options.UseFont = true;
		this.BtnSentData.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSentData.ImageOptions.Image");
		this.BtnSentData.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSentData.Location = new System.Drawing.Point(11, 62);
		this.BtnSentData.Margin = new System.Windows.Forms.Padding(5);
		this.BtnSentData.Name = "BtnSentData";
		this.BtnSentData.Size = new System.Drawing.Size(330, 33);
		this.BtnSentData.TabIndex = 238;
		this.BtnSentData.Text = "ارسال البيانات ";
		this.BtnSentData.Click += new System.EventHandler(BtnSentData_Click);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtActivationId);
		this.groupBox1.Controls.Add(this.BtnGetData);
		this.groupBox1.Controls.Add(this.BtnSentData);
		this.groupBox1.Location = new System.Drawing.Point(12, -2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(349, 158);
		this.groupBox1.TabIndex = 239;
		this.groupBox1.TabStop = false;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(255, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(90, 21);
		this.label1.TabIndex = 241;
		this.label1.Text = "رقم التفعيل";
		this.TxtActivationId.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtActivationId.Location = new System.Drawing.Point(11, 23);
		this.TxtActivationId.Name = "TxtActivationId";
		this.TxtActivationId.Size = new System.Drawing.Size(238, 26);
		this.TxtActivationId.TabIndex = 240;
		this.TxtActivationId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.BtnGetData.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnGetData.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnGetData.Appearance.Options.UseFont = true;
		this.BtnGetData.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnGetData.ImageOptions.Image");
		this.BtnGetData.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnGetData.Location = new System.Drawing.Point(11, 105);
		this.BtnGetData.Margin = new System.Windows.Forms.Padding(5);
		this.BtnGetData.Name = "BtnGetData";
		this.BtnGetData.Size = new System.Drawing.Size(330, 33);
		this.BtnGetData.TabIndex = 239;
		this.BtnGetData.Text = "استقبال البيانات";
		this.BtnGetData.Click += new System.EventHandler(BtnGetData_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(373, 168);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmZatcaIntgration";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "CRMIntegration";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}


