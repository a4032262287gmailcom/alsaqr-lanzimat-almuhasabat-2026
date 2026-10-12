using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Dto;
using Newtonsoft.Json;

namespace GTSErpSystem.Frms.ElctorncInvoic;

public class FrmGetDataFromCRM : XtraForm
{
	public enum InvoiceMode
	{
		Developer,
		Simulation,
		Production
	}

	private Class_Users USR = new Class_Users();

	private Class_WaitFormFunc waitForm = new Class_WaitFormFunc();

	private IContainer components = null;

	private GroupBox groupBox1;

	private SimpleButton BtnGetData;

	private Label label2;

	private Label label1;

	private System.Windows.Forms.ComboBox TxtBranch;

	private System.Windows.Forms.ComboBox TxtName;

	public FrmGetDataFromCRM()
	{
		InitializeComponent();
		BindCBBranch();
		BindCBGroups();
	}

	private void BindCBBranch()
	{
		try
		{
			TxtBranch.DataSource = USR.GetAllBranch();
			TxtBranch.DisplayMember = "Name";
			TxtBranch.ValueMember = "ID";
			TxtBranch.SelectedValue = 1;
		}
		catch (Exception)
		{
			MessageBox.Show("حدث خطأ اثناء الدخول برجاء الاتصال بالدعم الفني الخاص بالبرنامج", "تسجيل الدخول", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BindCBGroups()
	{
		int BID = Convert.ToInt32(TxtBranch.SelectedValue);
		var list = (from x in USR.GetAllUsers()
			where x.BranchID == BID && x.IsActive == true
			select new { ID = (int?)x.ID, x.Name }).ToList();
		list.Insert(0, new
		{
			ID = (int?)null,
			Name = "لا يوجد"
		});
		TxtName.DataSource = list;
		TxtName.DisplayMember = "Name";
		TxtName.ValueMember = "ID";
		TxtName.SelectedIndex = 0;
	}

	private async void BtnGetData_Click(object sender, EventArgs e)
	{
		try
		{
			waitForm.Show();
			if (LoginDetails.ActivationId == 0 || TxtBranch.SelectedValue == null || TxtName.SelectedValue == null)
			{
				waitForm.Close();
				MessageBox.Show("من فضلك تأكد من اختيار الفرع والمستخدم بشكل صحيح.", "تنبيه");
				return;
			}
			int branchId = Convert.ToInt32(TxtBranch.SelectedValue);
			int userId = Convert.ToInt32(TxtName.SelectedValue);
			string baseUrl = LoginDetails.Crm?.TrimEnd('/') ?? throw new InvalidOperationException("LoginDetails.Crm غير محدد");
			string apiUrl = $"{baseUrl}/Zakat/GetDesktopCSRAsync?activationId={LoginDetails.ActivationId}&branchId={branchId}&userId={userId}";
			using HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginDetails.Token);
			HttpResponseMessage response = await client.GetAsync(apiUrl);
			waitForm.Close();
			string resultJson = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
			{
				CRSFromCrmDatabaseResponse data = JsonConvert.DeserializeObject<CRSFromCrmDatabaseResponse>(resultJson);
				if (data != null)
				{
					SaveCsrDataToDatabase(data);
					MessageBox.Show("تم جلب البيانات وتحديثها بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
				else
				{
					MessageBox.Show("âڑ \ufe0f لم يتم العثور على بيانات في الرد من السيرفر.");
				}
			}
			else
			{
				MessageBox.Show("â‌Œ فشل الاتصال بالخادم:\n" + resultJson);
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			waitForm.Close();
			MessageBox.Show("حدث خطأ: " + ex2.Message, "خطأ");
		}
	}

	private void SaveCsrDataToDatabase(CRSFromCrmDatabaseResponse data)
	{
		using SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
		using SqlCommand sqlCommand = new SqlCommand("UpdateCsr", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.AddWithValue("@BranchId", ((object)data.BranchId) ?? ((object)DBNull.Value));
		sqlCommand.Parameters.AddWithValue("@CsIdForUser", data.CsIdForUser);
		sqlCommand.Parameters.AddWithValue("@CSR", ((object)data.CSR) ?? ((object)DBNull.Value));
		sqlCommand.Parameters.AddWithValue("@PublicKey", ((object)data.PublicKey) ?? ((object)DBNull.Value));
		sqlCommand.Parameters.AddWithValue("@PrivateKey", ((object)data.PrivateKey) ?? ((object)DBNull.Value));
		sqlCommand.Parameters.AddWithValue("@SecretKey", ((object)data.SecretKey) ?? ((object)DBNull.Value));
		string value = Enum.GetName(typeof(InvoiceMode), Convert.ToInt32(data.ModeInvoice)) ?? "Developer";
		sqlCommand.Parameters.AddWithValue("@ModeInvoice", value);
		sqlCommand.Parameters.AddWithValue("@UserId", data.UserId.HasValue ? ((object)data.UserId.Value) : DBNull.Value);
		sqlConnection.Open();
		sqlCommand.ExecuteNonQuery();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.ElctorncInvoic.FrmGetDataFromCRM));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnGetData = new DevExpress.XtraEditors.SimpleButton();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtBranch = new System.Windows.Forms.ComboBox();
		this.TxtName = new System.Windows.Forms.ComboBox();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.TxtBranch);
		this.groupBox1.Controls.Add(this.TxtName);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(363, 116);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.BtnGetData.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnGetData.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnGetData.Appearance.Options.UseFont = true;
		this.BtnGetData.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnGetData.ImageOptions.Image");
		this.BtnGetData.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnGetData.Location = new System.Drawing.Point(12, 126);
		this.BtnGetData.Margin = new System.Windows.Forms.Padding(5);
		this.BtnGetData.Name = "BtnGetData";
		this.BtnGetData.Size = new System.Drawing.Size(177, 33);
		this.BtnGetData.TabIndex = 240;
		this.BtnGetData.Text = "حفظ";
		this.BtnGetData.Click += new System.EventHandler(BtnGetData_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(277, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(45, 21);
		this.label1.TabIndex = 0;
		this.label1.Text = "الفرع";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(274, 77);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(84, 21);
		this.label2.TabIndex = 1;
		this.label2.Text = "المستخدم";
		this.TxtBranch.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtBranch.FormattingEnabled = true;
		this.TxtBranch.Location = new System.Drawing.Point(6, 26);
		this.TxtBranch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtBranch.Name = "TxtBranch";
		this.TxtBranch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.TxtBranch.Size = new System.Drawing.Size(263, 26);
		this.TxtBranch.TabIndex = 2;
		this.TxtName.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtName.FormattingEnabled = true;
		this.TxtName.Location = new System.Drawing.Point(6, 76);
		this.TxtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtName.Name = "TxtName";
		this.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.TxtName.Size = new System.Drawing.Size(263, 26);
		this.TxtName.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(387, 168);
		base.Controls.Add(this.BtnGetData);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmGetDataFromCRM";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "CRMData";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}


