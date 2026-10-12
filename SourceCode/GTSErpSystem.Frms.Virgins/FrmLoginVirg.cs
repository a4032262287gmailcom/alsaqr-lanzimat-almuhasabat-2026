using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;

namespace GTSErpSystem.Frms.Virgins;

public class FrmLoginVirg : XtraForm
{
	private GTSdbEntities db = new GTSdbEntities();

	private string Ver;

	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox TxtPassword;

	private Label label2;

	private TextBox TxtUserName;

	private Label label1;

	private TextBox TxtCode;

	private Label label26;

	private Label label3;

	private SimpleButton BtnLogin;

	private SimpleButton BtnSearch;

	public TextBox TxtBranch;

	private Label LblID;

	private Label LblVer;

	public TextBox TxtFranchise;

	private Label label4;

	public FrmLoginVirg()
	{
		InitializeComponent();
	}

	private void BtnSearch_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text != "" && TxtUserName.Text != "" && TxtPassword.Text != "")
		{
			string text = Class_Encryption.EncryptString(TxtPassword.Text, TxtCode.Text.ToString());
			Virg_Branch virg_Branch = db.Virg_Branch.Where((Virg_Branch xx) => xx.Code == TxtCode.Text).FirstOrDefault();
			if (virg_Branch != null)
			{
				TxtBranch.Text = virg_Branch.Branch;
				LblID.Text = virg_Branch.ID.ToString();
				Ver = virg_Branch.VirgLogin;
				int FraID = Convert.ToInt32(virg_Branch.FranchiseID);
				Virg_Franchise virg_Franchise = db.Virg_Franchise.Where((Virg_Franchise x) => x.ID == FraID).FirstOrDefault();
				TxtFranchise.Text = virg_Franchise.Name;
				LoginDetails.Active_AdminPermission = Convert.ToBoolean(virg_Branch.AdminPermission);
				LoginDetails.Active_CompID = Convert.ToInt32(virg_Branch.CompID);
				LoginDetails.Active_TypeID = Convert.ToInt32(virg_Branch.TypeID);
				LoginDetails.Active_FranchiseID = Convert.ToInt32(virg_Branch.FranchiseID);
				LoginDetails.Active_ShowAllCustomer = Convert.ToBoolean(virg_Branch.ShowAllCustomer);
				LoginDetails.Active_ShowVirgin = Convert.ToBoolean(virg_Branch.ShowVirg);
			}
			else
			{
				MessageBox.Show("عفوا، لا يوجد مستخدم مسجل بهذه البيانات \r\n الرجاء التأكد من البيانات والمحاولة مرة اخري ", "Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show(" الرجاء التأكد من البيانات والمحاولة مرة اخري ", "Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnLogin_Click(object sender, EventArgs e)
	{
		if (TxtBranch.Text != "" && LblVer.Text == Ver)
		{
			LoginDetails.Active_LblUserName = TxtBranch.Text;
			LoginDetails.Active_LblUserID = Convert.ToInt32(LblID.Text);
			Hide();
			new FrmVirgin().Show();
		}
		else
		{
			Close();
		}
		Hide();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Virgins.FrmLoginVirg));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TxtFranchise = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.LblVer = new System.Windows.Forms.Label();
		this.LblID = new System.Windows.Forms.Label();
		this.BtnLogin = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
		this.TxtBranch = new System.Windows.Forms.TextBox();
		this.TxtPassword = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtUserName = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtCode = new System.Windows.Forms.TextBox();
		this.label26 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.TxtFranchise);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.LblVer);
		this.groupBox1.Controls.Add(this.LblID);
		this.groupBox1.Controls.Add(this.BtnLogin);
		this.groupBox1.Controls.Add(this.BtnSearch);
		this.groupBox1.Controls.Add(this.TxtBranch);
		this.groupBox1.Controls.Add(this.TxtPassword);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtUserName);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(456, 275);
		this.groupBox1.TabIndex = 248;
		this.groupBox1.TabStop = false;
		this.TxtFranchise.BackColor = System.Drawing.Color.White;
		this.TxtFranchise.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtFranchise.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtFranchise.Location = new System.Drawing.Point(7, 168);
		this.TxtFranchise.Margin = new System.Windows.Forms.Padding(4);
		this.TxtFranchise.Name = "TxtFranchise";
		this.TxtFranchise.ReadOnly = true;
		this.TxtFranchise.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtFranchise.Size = new System.Drawing.Size(314, 28);
		this.TxtFranchise.TabIndex = 259;
		this.TxtFranchise.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(329, 175);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(34, 16);
		this.label4.TabIndex = 258;
		this.label4.Text = "الفرع";
		this.LblVer.AutoSize = true;
		this.LblVer.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Bold);
		this.LblVer.ForeColor = System.Drawing.Color.Green;
		this.LblVer.Location = new System.Drawing.Point(347, 225);
		this.LblVer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblVer.Name = "LblVer";
		this.LblVer.Size = new System.Drawing.Size(74, 17);
		this.LblVer.TabIndex = 257;
		this.LblVer.Text = "26-04-04";
		this.LblID.AutoSize = true;
		this.LblID.Location = new System.Drawing.Point(382, 31);
		this.LblID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblID.Name = "LblID";
		this.LblID.Size = new System.Drawing.Size(14, 16);
		this.LblID.TabIndex = 257;
		this.LblID.Text = "0";
		this.LblID.Visible = false;
		this.BtnLogin.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnLogin.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnLogin.Location = new System.Drawing.Point(7, 211);
		this.BtnLogin.Margin = new System.Windows.Forms.Padding(4);
		this.BtnLogin.Name = "BtnLogin";
		this.BtnLogin.Size = new System.Drawing.Size(153, 40);
		this.BtnLogin.TabIndex = 256;
		this.BtnLogin.Text = "دخول";
		this.BtnLogin.Click += new System.EventHandler(BtnLogin_Click);
		this.BtnSearch.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSearch.Location = new System.Drawing.Point(168, 211);
		this.BtnSearch.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSearch.Name = "BtnSearch";
		this.BtnSearch.Size = new System.Drawing.Size(153, 40);
		this.BtnSearch.TabIndex = 255;
		this.BtnSearch.Text = "بحث";
		this.BtnSearch.Click += new System.EventHandler(BtnSearch_Click);
		this.TxtBranch.BackColor = System.Drawing.Color.White;
		this.TxtBranch.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtBranch.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtBranch.Location = new System.Drawing.Point(7, 132);
		this.TxtBranch.Margin = new System.Windows.Forms.Padding(4);
		this.TxtBranch.Name = "TxtBranch";
		this.TxtBranch.ReadOnly = true;
		this.TxtBranch.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtBranch.Size = new System.Drawing.Size(314, 28);
		this.TxtBranch.TabIndex = 254;
		this.TxtBranch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPassword.BackColor = System.Drawing.Color.White;
		this.TxtPassword.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPassword.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtPassword.Location = new System.Drawing.Point(7, 96);
		this.TxtPassword.Margin = new System.Windows.Forms.Padding(4);
		this.TxtPassword.Name = "TxtPassword";
		this.TxtPassword.PasswordChar = '*';
		this.TxtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPassword.Size = new System.Drawing.Size(314, 28);
		this.TxtPassword.TabIndex = 252;
		this.TxtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(329, 139);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(40, 16);
		this.label3.TabIndex = 253;
		this.label3.Text = "الاسم";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(329, 103);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(67, 16);
		this.label2.TabIndex = 253;
		this.label2.Text = "كلمة المرور";
		this.TxtUserName.BackColor = System.Drawing.Color.White;
		this.TxtUserName.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtUserName.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtUserName.Location = new System.Drawing.Point(7, 60);
		this.TxtUserName.Margin = new System.Windows.Forms.Padding(4);
		this.TxtUserName.Name = "TxtUserName";
		this.TxtUserName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtUserName.Size = new System.Drawing.Size(314, 28);
		this.TxtUserName.TabIndex = 250;
		this.TxtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(329, 67);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(94, 16);
		this.label1.TabIndex = 251;
		this.label1.Text = "اسم المستخدم";
		this.TxtCode.BackColor = System.Drawing.Color.White;
		this.TxtCode.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCode.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtCode.Location = new System.Drawing.Point(7, 24);
		this.TxtCode.Margin = new System.Windows.Forms.Padding(4);
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtCode.Size = new System.Drawing.Size(314, 28);
		this.TxtCode.TabIndex = 248;
		this.TxtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(329, 31);
		this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(33, 16);
		this.label26.TabIndex = 249;
		this.label26.Text = "الكود";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(480, 299);
		base.Controls.Add(this.groupBox1);
		base.IconOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("FrmLoginVirg.IconOptions.SvgImage");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmLoginVirg";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تسجيل الدخول";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}


