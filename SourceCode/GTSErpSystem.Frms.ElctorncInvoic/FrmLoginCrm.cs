using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;
using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GTSErpSystem.Frms.ElctorncInvoic;

public class FrmLoginCrm : XtraForm
{
	private Class_WaitFormFunc waitForm = new Class_WaitFormFunc();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private TextBox TxtPassword;

	private TextBox TxtUserName;

	private Label label2;

	private MaterialRaisedButton BtnLogin;

	public FrmLoginCrm()
	{
		InitializeComponent();
	}

	private async void BtnLogin_Click(object sender, EventArgs e)
	{
		Login();
	}

	private async void FrmLoginCrm_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			Login();
		}
	}

	private async void Login()
	{
		waitForm.Show();
		string apiUrl = LoginDetails.Crm + "/AuthUsers/UserLogin";
		var loginData = new
		{
			UserName = TxtUserName.Text.Trim(),
			Password = TxtPassword.Text.Trim()
		};
		using HttpClient client = new HttpClient();
		try
		{
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			string json = JsonConvert.SerializeObject(loginData);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync(apiUrl, content);
			if (response.IsSuccessStatusCode)
			{
				JObject data = JObject.Parse(await response.Content.ReadAsStringAsync());
				LoginDetails.EmployeeId = data["employee"]?["id"]?.ToString();
				LoginDetails.Token = data["token"]?.ToString();
				if (!string.IsNullOrEmpty(LoginDetails.EmployeeId))
				{
					waitForm.Close();
					FrmZatcaIntgration frm = new FrmZatcaIntgration();
					Hide();
					frm.ShowDialog();
				}
				else
				{
					waitForm.Close();
					MessageBox.Show("تم الاتصال بنجاح، لكن لم يتم العثور على Employee ID", "تنبيه");
				}
			}
			else
			{
				waitForm.Close();
				MessageBox.Show("فشل تسجيل الدخول â‌Œ\n" + await response.Content.ReadAsStringAsync(), "خطأ");
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			waitForm.Close();
			MessageBox.Show("حدث خطأ أثناء الاتصال بالسيرفر:\n" + ex2.Message, "خطأ");
		}
	}

	private void FrmLoginCrm_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.ElctorncInvoic.FrmLoginCrm));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtUserName = new System.Windows.Forms.TextBox();
		this.TxtPassword = new System.Windows.Forms.TextBox();
		this.BtnLogin = new MaterialSkin.Controls.MaterialRaisedButton();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.TxtPassword);
		this.groupBox1.Controls.Add(this.TxtUserName);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(5, 1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(447, 115);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(320, 24);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(122, 21);
		this.label1.TabIndex = 0;
		this.label1.Text = "اسم المستخدم";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(320, 64);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(88, 21);
		this.label2.TabIndex = 1;
		this.label2.Text = "كلمه المرور";
		this.TxtUserName.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtUserName.Location = new System.Drawing.Point(8, 23);
		this.TxtUserName.Name = "TxtUserName";
		this.TxtUserName.Size = new System.Drawing.Size(295, 26);
		this.TxtUserName.TabIndex = 2;
		this.TxtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPassword.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtPassword.Location = new System.Drawing.Point(8, 63);
		this.TxtPassword.Name = "TxtPassword";
		this.TxtPassword.PasswordChar = '*';
		this.TxtPassword.Size = new System.Drawing.Size(295, 26);
		this.TxtPassword.TabIndex = 3;
		this.TxtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.BtnLogin.BackColor = System.Drawing.Color.LightGray;
		this.BtnLogin.Depth = 0;
		this.BtnLogin.ForeColor = System.Drawing.Color.Red;
		this.BtnLogin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.BtnLogin.Location = new System.Drawing.Point(173, 121);
		this.BtnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.BtnLogin.MouseState = MaterialSkin.MouseState.HOVER;
		this.BtnLogin.Name = "BtnLogin";
		this.BtnLogin.Primary = true;
		this.BtnLogin.Size = new System.Drawing.Size(150, 42);
		this.BtnLogin.TabIndex = 4;
		this.BtnLogin.Text = "دخول";
		this.BtnLogin.UseVisualStyleBackColor = false;
		this.BtnLogin.Click += new System.EventHandler(BtnLogin_Click);
		base.Appearance.BackColor = System.Drawing.Color.White;
		base.Appearance.Options.UseBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(457, 168);
		base.Controls.Add(this.BtnLogin);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.IconOptions.Image = (System.Drawing.Image)resources.GetObject("FrmLoginCrm.IconOptions.Image");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmLoginCrm";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تسجيل الدخول ";
		base.Load += new System.EventHandler(FrmLoginCrm_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FrmLoginCrm_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}


