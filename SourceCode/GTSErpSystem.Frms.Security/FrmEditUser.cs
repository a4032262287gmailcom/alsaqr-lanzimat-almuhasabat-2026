using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Security;

namespace GTSErpSystem.Frms.Security;

public class FrmEditUser : XtraForm
{
	private Class_Users USR = new Class_Users();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private IContainer components = null;

	private GroupBox groupBox1;

	private CheckBox CHKActive;

	private Label label1;

	private Label label2;

	private Label label3;

	private System.Windows.Forms.ComboBox CBBranch;

	private Label label4;

	private System.Windows.Forms.ComboBox CBGroups;

	private Label label5;

	private TextBox TxtPassword;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	private Label LblUserID;

	private System.Windows.Forms.ComboBox cbUsers;

	public FrmEditUser()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
		}
		InitializeComponent();
		BindCBUsers();
		BindCBBranch();
		BindCBGroups();
		BindUserData();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		if (TxtPassword.Text != "" && Convert.ToInt32(CBBranch.SelectedValue) > 0 && Convert.ToInt32(CBGroups.SelectedValue) > 0)
		{
			Edit();
		}
		else if (TxtPassword.Text == "")
		{
			MessageBox.Show("يجدب ادخال الرقم السري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			TxtPassword.Focus();
		}
		else if (Convert.ToInt32(CBBranch.SelectedValue) == 0)
		{
			MessageBox.Show("يجدب ادخال اسم الفرع ", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			CBBranch.Focus();
		}
		else if (Convert.ToInt32(CBGroups.SelectedValue) == 0)
		{
			MessageBox.Show("يجدب ادخال اسم المجموعة", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			CBGroups.Focus();
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 1))
		{
			int num = Convert.ToInt32(CBGroups.SelectedValue);
			int num2 = Convert.ToInt32(CBBranch.SelectedValue);
			int iD = Convert.ToInt32(cbUsers.SelectedValue);
			int num3 = 0;
			if (TxtPassword.Text != "" && num > 0 && num2 > 0)
			{
				switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + cbUsers.Text + " ) الي المستخدمين", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num3 = USR.Update(iD, cbUsers.Text, TxtPassword.Text, num2, num, CHKActive.Checked);
					if (num3 > 0)
					{
						MessageBox.Show("تم حفظ بيانات المستخدم ( " + cbUsers.Text + " ) بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = true;
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات المستخدم بنفس الاسم ( " + cbUsers.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
			}
			else if (TxtPassword.Text == "")
			{
				MessageBox.Show("عفوا، ادخل كلمة المرور ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtPassword.Focus();
			}
			else if (num2 == 0)
			{
				MessageBox.Show(" عفوا، اختر الفرع ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				CBBranch.Focus();
			}
			else if (num == 0)
			{
				MessageBox.Show("عفوا، اختر المجموعة ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				CBGroups.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BindCBUsers()
	{
		cbUsers.DataSource = USR.GetAllUsers().ToList();
		cbUsers.DisplayMember = "Name";
		cbUsers.ValueMember = "ID";
	}

	private void BindCBGroups()
	{
		int BID = Convert.ToInt32(CBBranch.SelectedValue);
		CBGroups.DataSource = (from x in USR.GetAllGroups()
			where x.BranchID == BID
			select x).ToList();
		CBGroups.DisplayMember = "Name";
		CBGroups.ValueMember = "ID";
		CBGroups.SelectedValue = 1;
	}

	private void BindCBBranch()
	{
		CBBranch.DataSource = USR.GetAllBranch();
		CBBranch.DisplayMember = "Name";
		CBBranch.ValueMember = "ID";
		CBBranch.SelectedValue = 1;
	}

	private void BindUserData()
	{
		try
		{
		}
		catch (Exception)
		{
			throw;
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Security.FrmEditUser));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbUsers = new System.Windows.Forms.ComboBox();
		this.CHKActive = new System.Windows.Forms.CheckBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.CBBranch = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.CBGroups = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtPassword = new System.Windows.Forms.TextBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.LblUserID = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.cbUsers);
		this.groupBox1.Controls.Add(this.CHKActive);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.CBBranch);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.CBGroups);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.TxtPassword);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.cbUsers, "cbUsers");
		this.cbUsers.FormattingEnabled = true;
		this.cbUsers.Name = "cbUsers";
		resources.ApplyResources(this.CHKActive, "CHKActive");
		this.CHKActive.Checked = true;
		this.CHKActive.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CHKActive.Name = "CHKActive";
		this.CHKActive.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.CBBranch, "CBBranch");
		this.CBBranch.FormattingEnabled = true;
		this.CBBranch.Name = "CBBranch";
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.CBGroups, "CBGroups");
		this.CBGroups.FormattingEnabled = true;
		this.CBGroups.Name = "CBGroups";
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.TxtPassword, "TxtPassword");
		this.TxtPassword.Name = "TxtPassword";
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.LblUserID, "LblUserID");
		this.LblUserID.Name = "LblUserID";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.LblUserID);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmEditUser";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


