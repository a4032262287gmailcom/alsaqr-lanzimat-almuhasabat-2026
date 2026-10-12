using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Security;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Security;

public class FrmUsers : XtraForm
{
	private Class_Users USR = new Class_Users();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private IContainer components = null;

	private SimpleButton BtnDelete;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private SimpleButton BtnExit;

	private System.Windows.Forms.ComboBox CBBranch;

	private Label label5;

	private TextBox TxtPassword;

	private TextBox TxtName;

	private System.Windows.Forms.ComboBox CBGroups;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private GridEX gridEX1;

	private GroupBox groupBox1;

	private CheckBox CHKActive;

	private Label LblID;

	private CheckBox Cksentfatora;

	public FrmUsers()
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
		Clear();
		BindCBBranch();
		BindCBGroups();
		BindGrid();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtName.Focus();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
	}

	private void FrmUsers_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
	}

	private void FrmUsers_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F4")
		{
			Clear();
		}
		else if (e.KeyCode.ToString() == "F5")
		{
			Save();
		}
		else if (e.KeyCode.ToString() == "F6")
		{
			Edit();
		}
		else if (e.KeyCode.ToString() == "F7")
		{
			Delete();
		}
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		Clear();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void gridEX1_MouseClick_1(object sender, MouseEventArgs e)
	{
		try
		{
			int position = gridEX1.SelectedItems[0].Position;
			int id = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
			LblID.Text = id.ToString();
			User_Login user_Login = (from x in USR.GetAllUsers()
				where x.ID == id
				select x).FirstOrDefault();
			TxtName.Text = user_Login.Name;
			TxtPassword.Text = Class_Encryption.DecryptString(user_Login.PassWord, user_Login.ID.ToString());
			CBGroups.SelectedValue = user_Login.GroupID;
			CBBranch.SelectedValue = user_Login.BranchID;
			Cksentfatora.Checked = Convert.ToBoolean(user_Login.SentXmlZakateNow);
			CHKActive.Checked = Convert.ToBoolean(user_Login.IsActive);
			TxtName.Focus();
			BtnSave.Enabled = false;
			BtnEdit.Enabled = true;
			BtnDelete.Enabled = true;
		}
		catch (Exception)
		{
		}
	}

	private void CBBranch_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			int BID = Convert.ToInt32(CBBranch.SelectedValue);
			CBGroups.DataSource = (from x in USR.GetAllGroups()
				where x.BranchID == BID
				select x).ToList();
			CBGroups.DisplayMember = "Name";
			CBGroups.ValueMember = "ID";
			if (Convert.ToInt32(CBGroups.SelectedValue) == 0)
			{
				CBGroups.Text = "";
			}
			BindGrid();
		}
		catch (Exception)
		{
			int BID2 = 1;
			CBGroups.DataSource = (from x in USR.GetAllGroups()
				where x.BranchID == BID2
				select x).ToList();
			CBGroups.DisplayMember = "Name";
			CBGroups.ValueMember = "ID";
			BindGrid();
		}
	}

	private void Comb_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void ControlSave_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnSave.Focus();
		}
	}

	private void textBox1_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("ar"));
	}

	public static InputLanguage GetInputLanguageByName(string inputName)
	{
		foreach (InputLanguage installedInputLanguage in InputLanguage.InstalledInputLanguages)
		{
			if (installedInputLanguage.Culture.EnglishName.ToLower().StartsWith(inputName))
			{
				return installedInputLanguage;
			}
		}
		return null;
	}

	private void SetKeyboardLayout(InputLanguage layout)
	{
		InputLanguage.CurrentInputLanguage = layout;
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

	private void BindGrid()
	{
		int GID = Convert.ToInt32(CBGroups.SelectedValue);
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable = new DataTable("gridEX1");
		dataTable.Columns.Add("كود", typeof(int));
		dataTable.Columns.Add("اسم المستخدم", typeof(string));
		dataTable.Columns.Add("باسورد", typeof(string));
		dataTable.Columns.Add("الفرع", typeof(string));
		dataTable.Columns.Add("المجموعة", typeof(string));
		dataTable.Columns.Add("الحالة", typeof(string));
		if (GID > 0)
		{
			foreach (User_Login item in from x in USR.GetAllUsers()
				where x.GroupID == GID
				select x)
			{
				string text = "نشط";
				if (item.IsActive == false)
				{
					text = "غير نشط";
				}
				dataTable.Rows.Add(item.ID, item.Name, item.PassWord, item.BranchID, USR.GetGroupName(Convert.ToInt32(item.GroupID)), text);
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["كود"].Width = 30;
			gridEX1.RootTable.Columns["كود"].RightToLeft = RightToLeft.No;
			gridEX1.RootTable.Columns["الفرع"].Visible = false;
			gridEX1.RootTable.Columns["باسورد"].Visible = false;
			gridEX1.RootTable.Columns["المجموعة"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["كود"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["اسم المستخدم"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الحالة"].TextAlignment = TextAlignment.Center;
		}
		else
		{
			dataTable.Clear();
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["كود"].Width = 30;
			gridEX1.RootTable.Columns["كود"].RightToLeft = RightToLeft.No;
			gridEX1.RootTable.Columns["الفرع"].Visible = false;
			gridEX1.RootTable.Columns["باسورد"].Visible = false;
			gridEX1.RootTable.Columns["المجموعة"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["كود"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["اسم المستخدم"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الحالة"].TextAlignment = TextAlignment.Center;
		}
	}

	private void BindGridByGroup()
	{
		try
		{
			int GID = Convert.ToInt32(CBGroups.SelectedValue);
			int BRID = Convert.ToInt32(CBBranch.SelectedValue);
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("كود", typeof(int));
			dataTable.Columns.Add("اسم المستخدم", typeof(string));
			dataTable.Columns.Add("باسورد", typeof(string));
			dataTable.Columns.Add("الفرع", typeof(string));
			dataTable.Columns.Add("المجموعة", typeof(string));
			dataTable.Columns.Add("الحالة", typeof(string));
			foreach (User_Login item in from x in USR.GetAllUsers()
				where x.GroupID == GID && x.BranchID == BRID
				select x)
			{
				string text = "نشط";
				if (item.IsActive == false)
				{
					text = "غير نشط";
				}
				dataTable.Rows.Add(item.ID, item.Name, item.PassWord, item.BranchID, USR.GetGroupName(Convert.ToInt32(item.GroupID)), text);
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["كود"].Width = 30;
			gridEX1.RootTable.Columns["كود"].RightToLeft = RightToLeft.No;
			gridEX1.RootTable.Columns["الفرع"].Visible = false;
			gridEX1.RootTable.Columns["باسورد"].Visible = false;
			gridEX1.RootTable.Columns["المجموعة"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["كود"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["اسم المستخدم"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الحالة"].TextAlignment = TextAlignment.Center;
		}
		catch (Exception)
		{
			int GID2 = 1;
			int BRID2 = 1;
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable2 = new DataTable("gridEX1");
			dataTable2.Columns.Add("كود", typeof(int));
			dataTable2.Columns.Add("اسم المستخدم", typeof(string));
			dataTable2.Columns.Add("باسورد", typeof(string));
			dataTable2.Columns.Add("الفرع", typeof(string));
			dataTable2.Columns.Add("المجموعة", typeof(string));
			dataTable2.Columns.Add("الحالة", typeof(string));
			foreach (User_Login item2 in from x in USR.GetAllUsers()
				where x.GroupID == GID2 && x.BranchID == BRID2
				select x)
			{
				string text2 = "نشط";
				if (item2.IsActive == false)
				{
					text2 = "غير نشط";
				}
				dataTable2.Rows.Add(item2.ID, item2.Name, item2.PassWord, item2.BranchID, USR.GetGroupName(Convert.ToInt32(item2.GroupID)), text2);
			}
			gridEX1.SetDataBinding(dataTable2, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["كود"].Width = 30;
			gridEX1.RootTable.Columns["كود"].RightToLeft = RightToLeft.No;
			gridEX1.RootTable.Columns["الفرع"].Visible = false;
			gridEX1.RootTable.Columns["باسورد"].Visible = false;
			gridEX1.RootTable.Columns["المجموعة"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["كود"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["اسم المستخدم"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الحالة"].TextAlignment = TextAlignment.Center;
		}
	}

	private void Clear()
	{
		TxtName.Text = "";
		TxtPassword.Text = "";
		CBBranch.SelectedValue = 1;
		CBGroups.SelectedValue = 1;
		CHKActive.Checked = true;
		TxtName.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 1))
		{
			int num = 0;
			int num2 = Convert.ToInt32(CBGroups.SelectedValue);
			int num3 = Convert.ToInt32(CBBranch.SelectedValue);
			if (TxtName.Text != "" && TxtPassword.Text != "" && num2 > 0 && num3 > 0)
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtName.Text + " ) الي المستخدمين", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = USR.Insert(TxtName.Text, TxtPassword.Text, num3, num2, CHKActive.Checked, Cksentfatora.Checked);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات المستخدم  ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = true;
						LoginDetails.SentXmlZakateNow = Cksentfatora.Checked;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات المستخدم بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (TxtName.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم المستخدم ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
			else if (TxtPassword.Text == "")
			{
				MessageBox.Show("عفوا، ادخل كلمة المرور ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtPassword.Focus();
			}
			else if (num3 == 0)
			{
				MessageBox.Show(" عفوا، اختر الفرع ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				CBBranch.Focus();
			}
			else if (num2 == 0)
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

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 1))
		{
			int num = Convert.ToInt32(CBGroups.SelectedValue);
			int num2 = Convert.ToInt32(CBBranch.SelectedValue);
			int num3 = 0;
			if (TxtName.Text != "" && TxtPassword.Text != "" && num > 0 && num2 > 0)
			{
				switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + TxtName.Text + " ) الي المستخدمين", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num3 = USR.UpdateAndZkah(Convert.ToInt32(LblID.Text), TxtName.Text, TxtPassword.Text, num2, num, CHKActive.Checked, Cksentfatora.Checked);
					if (num3 > 0)
					{
						MessageBox.Show("تم حفظ بيانات المستخدم ( " + TxtName.Text + " ) بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						LoginDetails.SentXmlZakateNow = Cksentfatora.Checked;
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات المستخدم بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (TxtName.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم المستخدم ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
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

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 1))
		{
			int num = 0;
			if (TxtName.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtName.Text + " ) من المستخدم", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = USR.Delete(Convert.ToInt32(LblID.Text));
					if (num > 0)
					{
						BindGrid();
						MessageBox.Show("تم حذف بيانات المستخدم ( " + TxtName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						Clear();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtName.Text + " ) لوجود عمليات جارية بهذا المستخدم", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر اسم المستخدم ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				gridEX1.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void CBGroups_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindGridByGroup();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Security.FrmUsers));
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.CBBranch = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtPassword = new System.Windows.Forms.TextBox();
		this.TxtName = new System.Windows.Forms.TextBox();
		this.CBGroups = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.LblID = new System.Windows.Forms.Label();
		this.CHKActive = new System.Windows.Forms.CheckBox();
		this.Cksentfatora = new System.Windows.Forms.CheckBox();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBBranch.FormattingEnabled = true;
		resources.ApplyResources(this.CBBranch, "CBBranch");
		this.CBBranch.Name = "CBBranch";
		this.CBBranch.SelectedIndexChanged += new System.EventHandler(CBBranch_SelectedIndexChanged);
		this.CBBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBBranch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Comb_KeyPress);
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.TxtPassword, "TxtPassword");
		this.TxtPassword.Name = "TxtPassword";
		this.TxtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtName, "TxtName");
		this.TxtName.Name = "TxtName";
		this.TxtName.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBGroups.FormattingEnabled = true;
		resources.ApplyResources(this.CBGroups, "CBGroups");
		this.CBGroups.Name = "CBGroups";
		this.CBGroups.SelectedIndexChanged += new System.EventHandler(CBGroups_SelectedIndexChanged);
		this.CBGroups.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBGroups.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Comb_KeyPress);
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		resources.ApplyResources(this.gridEX1, "gridEX1");
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.MouseClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseClick_1);
		this.groupBox1.Controls.Add(this.Cksentfatora);
		this.groupBox1.Controls.Add(this.LblID);
		this.groupBox1.Controls.Add(this.CHKActive);
		this.groupBox1.Controls.Add(this.TxtName);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.CBBranch);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.CBGroups);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.TxtPassword);
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.LblID, "LblID");
		this.LblID.Name = "LblID";
		resources.ApplyResources(this.CHKActive, "CHKActive");
		this.CHKActive.Checked = true;
		this.CHKActive.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CHKActive.Name = "CHKActive";
		this.CHKActive.UseVisualStyleBackColor = true;
		this.CHKActive.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
		resources.ApplyResources(this.Cksentfatora, "Cksentfatora");
		this.Cksentfatora.Name = "Cksentfatora";
		this.Cksentfatora.UseVisualStyleBackColor = true;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.gridEX1);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.IconOptions.Icon = (System.Drawing.Icon)resources.GetObject("FrmUsers.IconOptions.Icon");
		base.MaximizeBox = false;
		base.Name = "FrmUsers";
		base.Load += new System.EventHandler(FrmUsers_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FrmUsers_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}


