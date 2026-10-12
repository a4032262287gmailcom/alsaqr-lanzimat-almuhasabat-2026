using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Security;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Account;

public class FrmCostCenter : XtraForm
{
	private Calss_CostCenter UNT = new Calss_CostCenter();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private GTSdbEntities db = new GTSdbEntities();

	public int GrpID;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label4;

	private TextBox TxtNote;

	private ButtonEdit TxtAccountSonCode;

	private Label label3;

	private ButtonEdit TxtAccountSonNamer;

	private Label label1;

	private Label label9;

	private GridEX gridEX1;

	private SimpleButton simpleButton1;

	private SimpleButton BtnDelete;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private System.Windows.Forms.ComboBox CpFather;

	private System.Windows.Forms.ComboBox CBProject;

	private Label label2;

	private Label label5;

	private TextEdit TxtNumCostCenter;

	private CheckBox CkHideCost;

	private CheckBox CkCostDetaulte;

	public FrmCostCenter()
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
		BindGrid();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BindCPFather();
		TxtAccountSonNamer.Focus();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
		CpFather.Enabled = true;
		CBProject.Enabled = true;
		if (LoginDetails.ActionName == "Bro")
		{
			CpFather.SelectedValue = LoginDetails.AccountBrother;
			CpFather.Enabled = false;
			CBProject.Enabled = false;
		}
		if (LoginDetails.ActionName == "Son")
		{
			CpFather.SelectedValue = LoginDetails.AccountSN;
			CpFather.Enabled = false;
			CBProject.Enabled = false;
		}
		if (LoginDetails.ActionName == "Edit")
		{
			EbitTree(LoginDetails.AccountSN);
			CpFather.Enabled = true;
			CBProject.Enabled = true;
		}
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
	}

	private void Frms_KeyDown(object sender, KeyEventArgs e)
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

	private void TxtName_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		Clear();
	}

	private void gridEX1_MouseClick_1(object sender, MouseEventArgs e)
	{
		try
		{
			int position = gridEX1.SelectedItems[0].Position;
			int id = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
			Select_SearchAccountCostCenterAll_Result select_SearchAccountCostCenterAll_Result = (from x in UNT.GetSearchAccountCostCenterAll()
				where x.CostCentersID == id
				select x).FirstOrDefault();
			int num = 1;
			Account_ProjectsDetails account_ProjectsDetails = db.Account_ProjectsDetails.Where((Account_ProjectsDetails x) => x.CostCenterID == (int?)id).FirstOrDefault();
			if (account_ProjectsDetails != null)
			{
				num = Convert.ToInt32(account_ProjectsDetails.ProjectAccountID);
			}
			TxtAccountSonCode.Text = select_SearchAccountCostCenterAll_Result.CostCentersID.ToString();
			TxtNumCostCenter.Text = select_SearchAccountCostCenterAll_Result.CostCentersNo.ToString();
			TxtAccountSonNamer.Text = select_SearchAccountCostCenterAll_Result.CostCentersName;
			CpFather.SelectedValue = select_SearchAccountCostCenterAll_Result.MainCostCentersID;
			CBProject.SelectedValue = num;
			TxtNote.Text = select_SearchAccountCostCenterAll_Result.Note;
			CkHideCost.Checked = select_SearchAccountCostCenterAll_Result.HideCost == true;
			CkCostDetaulte.Checked = select_SearchAccountCostCenterAll_Result.IsDefaulte == true;
			TxtAccountSonNamer.Focus();
			BtnSave.Enabled = false;
			BtnEdit.Enabled = true;
			BtnDelete.Enabled = true;
			CpFather.Enabled = true;
			CBProject.Enabled = true;
		}
		catch (Exception)
		{
		}
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

	public void BindCPFather()
	{
		CpFather.DataSource = UNT.GetSearchAccountCostCenterAll();
		CpFather.DisplayMember = "CostCentersName";
		CpFather.ValueMember = "CostCentersID";
		CBProject.DataSource = db.Account_Projects.ToList();
		CBProject.DisplayMember = "ProjectName";
		CBProject.ValueMember = "ProjectID";
	}

	private void BindGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			BindCPFather();
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns.Add("كود", typeof(int));
			dataTable.Columns.Add("اسم المركز", typeof(string));
			dataTable.Columns.Add("اسم الاب", typeof(string));
			foreach (Select_SearchAccountCostCenterAll_Result item in UNT.GetSearchAccountCostCenterAll())
			{
				dataTable.Rows.Add(item.CostCentersID, item.CostCentersNo, item.CostCentersName, UNT.GetName(Convert.ToInt32(item.MainCostCentersID)));
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["ID"].Visible = false;
			gridEX1.RootTable.Columns["كود"].Width = 30;
			gridEX1.RootTable.Columns["كود"].RightToLeft = RightToLeft.No;
			gridEX1.RootTable.Columns["كود"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["اسم المركز"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["اسم الاب"].TextAlignment = TextAlignment.Center;
			return;
		}
		BindCPFather();
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable2 = new DataTable("gridEX1");
		dataTable2.Columns.Add("ID", typeof(int));
		dataTable2.Columns.Add("Code", typeof(int));
		dataTable2.Columns.Add("Account Name", typeof(string));
		dataTable2.Columns.Add("Parent Name", typeof(string));
		foreach (Select_SearchAccountCostCenterAll_Result item2 in UNT.GetSearchAccountCostCenterAll())
		{
			dataTable2.Rows.Add(item2.CostCentersID, item2.CostCentersNo, item2.CostCentersName, UNT.GetName(Convert.ToInt32(item2.MainCostCentersID)));
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["ID"].Visible = false;
		gridEX1.RootTable.Columns["Code"].Width = 30;
		gridEX1.RootTable.Columns["Code"].RightToLeft = RightToLeft.No;
		gridEX1.RootTable.Columns["Code"].TextAlignment = TextAlignment.Center;
		gridEX1.RootTable.Columns["Account Name"].TextAlignment = TextAlignment.Center;
		gridEX1.RootTable.Columns["Parent Name"].TextAlignment = TextAlignment.Center;
	}

	private void Clear()
	{
		TxtAccountSonCode.Text = "";
		TxtAccountSonNamer.Text = "";
		CpFather.Text = "";
		TxtNote.Text = "";
		TxtNumCostCenter.Text = "";
		TxtAccountSonNamer.Focus();
		BindCPFather();
		CpFather.Enabled = true;
		CBProject.Enabled = true;
		CkHideCost.Checked = false;
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 2))
		{
			if (TxtNumCostCenter.Text == "")
			{
				MessageBox.Show("يجب ادخال رقم حساب مركز التكلفه ", "عفوا", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return;
			}
			int num = 0;
			if (TxtAccountSonNamer.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtAccountSonNamer.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = UNT.Insert(TxtAccountSonNamer.Text, Convert.ToInt32(TxtNumCostCenter.Text), Convert.ToInt32(CpFather.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), TxtNote.Text, CkHideCost.Checked, CkCostDetaulte.Checked);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات مركز التكلفة  ( " + TxtAccountSonNamer.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات مركز التكلفة بنفس الاسم ( " + TxtAccountSonNamer.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtAccountSonNamer.Focus();
					break;
				}
			}
			else if (TxtAccountSonNamer.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم مركز التكلفة ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtAccountSonNamer.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 2))
		{
			if (TxtNumCostCenter.Text == "")
			{
				MessageBox.Show("يجب ادخال رقم حساب مركز التكلفه ", "عفوا", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return;
			}
			int num = 0;
			if (TxtAccountSonNamer.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + TxtAccountSonNamer.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					int position = gridEX1.SelectedItems[0].Position;
					int iD = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
					num = UNT.Update(iD, TxtAccountSonNamer.Text, Convert.ToInt32(TxtNumCostCenter.Text), Convert.ToInt32(CpFather.SelectedValue), Convert.ToInt32(CBProject.SelectedValue), TxtNote.Text, CkHideCost.Checked, CkCostDetaulte.Checked);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات مركز التكلفة ( " + TxtAccountSonNamer.Text + " ) بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات مركز التكلفة بنفس الاسم ( " + TxtAccountSonNamer.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
				case DialogResult.No:
					TxtAccountSonNamer.Focus();
					break;
				}
			}
			else if (TxtAccountSonNamer.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم مركز التكلفة ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtAccountSonNamer.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 2))
		{
			int num = 0;
			if (TxtAccountSonNamer.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtAccountSonNamer.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					int position = gridEX1.SelectedItems[0].Position;
					int sn = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
					num = UNT.Delete(sn);
					if (num > 0)
					{
						BindGrid();
						MessageBox.Show("تم حذف بيانات مركز التكلفة ( " + TxtAccountSonNamer.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						Clear();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtAccountSonNamer.Text + " ) لوجود عمليات جارية بهذا مركز التكلفة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				}
				case DialogResult.No:
					TxtAccountSonNamer.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر اسم مركز التكلفة ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				gridEX1.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void EbitTree(int id)
	{
		Select_SearchAccountCostCenterAll_Result select_SearchAccountCostCenterAll_Result = (from x in UNT.GetSearchAccountCostCenterAll()
			where x.CostCentersID == id
			select x).FirstOrDefault();
		int num = 1;
		Account_ProjectsDetails account_ProjectsDetails = db.Account_ProjectsDetails.Where((Account_ProjectsDetails x) => x.CostCenterID == (int?)id).FirstOrDefault();
		if (account_ProjectsDetails != null)
		{
			num = Convert.ToInt32(account_ProjectsDetails.ProjectAccountID);
		}
		TxtAccountSonCode.Text = select_SearchAccountCostCenterAll_Result.CostCentersID.ToString();
		TxtNumCostCenter.Text = select_SearchAccountCostCenterAll_Result.CostCentersNo.ToString();
		TxtAccountSonNamer.Text = select_SearchAccountCostCenterAll_Result.CostCentersName;
		CpFather.SelectedValue = select_SearchAccountCostCenterAll_Result.MainCostCentersID;
		CBProject.SelectedValue = num;
		TxtNote.Text = select_SearchAccountCostCenterAll_Result.Note;
		TxtAccountSonNamer.Focus();
		BtnSave.Enabled = false;
		BtnEdit.Enabled = true;
		BtnDelete.Enabled = true;
		CpFather.Enabled = true;
		CBProject.Enabled = true;
	}

	private void CpFather_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void TxtNumCostCenter_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Account.FrmCostCenter));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.CkHideCost = new System.Windows.Forms.CheckBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtNumCostCenter = new DevExpress.XtraEditors.TextEdit();
		this.CBProject = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.CpFather = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.TxtAccountSonCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtAccountSonNamer = new DevExpress.XtraEditors.ButtonEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.CkCostDetaulte = new System.Windows.Forms.CheckBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNumCostCenter.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccountSonCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccountSonNamer.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.CkCostDetaulte);
		this.groupBox1.Controls.Add(this.CkHideCost);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.TxtNumCostCenter);
		this.groupBox1.Controls.Add(this.CBProject);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.CpFather);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.TxtAccountSonCode);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.TxtAccountSonNamer);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.CkHideCost, "CkHideCost");
		this.CkHideCost.Name = "CkHideCost";
		this.CkHideCost.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.TxtNumCostCenter, "TxtNumCostCenter");
		this.TxtNumCostCenter.Name = "TxtNumCostCenter";
		this.TxtNumCostCenter.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtNumCostCenter.Properties.Appearance.Font");
		this.TxtNumCostCenter.Properties.Appearance.Options.UseFont = true;
		this.TxtNumCostCenter.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumCostCenter.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumCostCenter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumCostCenter_KeyPress);
		resources.ApplyResources(this.CBProject, "CBProject");
		this.CBProject.FormattingEnabled = true;
		this.CBProject.Name = "CBProject";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.CpFather, "CpFather");
		this.CpFather.FormattingEnabled = true;
		this.CpFather.Name = "CpFather";
		this.CpFather.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CpFather_KeyPress);
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
		resources.ApplyResources(this.TxtAccountSonCode, "TxtAccountSonCode");
		this.TxtAccountSonCode.Name = "TxtAccountSonCode";
		this.TxtAccountSonCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtAccountSonCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAccountSonCode.Properties.Appearance.Font");
		this.TxtAccountSonCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtAccountSonCode.Properties.Appearance.Options.UseFont = true;
		this.TxtAccountSonCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAccountSonCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAccountSonCode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtAccountSonCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.TxtAccountSonCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtAccountSonCode.Properties.Buttons"), resources.GetString("TxtAccountSonCode.Properties.Buttons1"), (int)resources.GetObject("TxtAccountSonCode.Properties.Buttons2"), (bool)resources.GetObject("TxtAccountSonCode.Properties.Buttons3"), (bool)resources.GetObject("TxtAccountSonCode.Properties.Buttons4"), (bool)resources.GetObject("TxtAccountSonCode.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtAccountSonCode.Properties.Buttons6"), resources.GetObject("TxtAccountSonCode.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtAccountSonCode.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtAccountSonCode.Properties.Buttons9"))
		});
		this.TxtAccountSonCode.Properties.ReadOnly = true;
		this.TxtAccountSonCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.TxtAccountSonNamer, "TxtAccountSonNamer");
		this.TxtAccountSonNamer.Name = "TxtAccountSonNamer";
		this.TxtAccountSonNamer.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtAccountSonNamer.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAccountSonNamer.Properties.Appearance.Font");
		this.TxtAccountSonNamer.Properties.Appearance.Options.UseBackColor = true;
		this.TxtAccountSonNamer.Properties.Appearance.Options.UseFont = true;
		this.TxtAccountSonNamer.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAccountSonNamer.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAccountSonNamer.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtAccountSonNamer.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.TxtAccountSonNamer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtAccountSonNamer.Properties.Buttons"), resources.GetString("TxtAccountSonNamer.Properties.Buttons1"), (int)resources.GetObject("TxtAccountSonNamer.Properties.Buttons2"), (bool)resources.GetObject("TxtAccountSonNamer.Properties.Buttons3"), (bool)resources.GetObject("TxtAccountSonNamer.Properties.Buttons4"), (bool)resources.GetObject("TxtAccountSonNamer.Properties.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("TxtAccountSonNamer.Properties.Buttons6"), resources.GetObject("TxtAccountSonNamer.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtAccountSonNamer.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtAccountSonNamer.Properties.Buttons9"))
		});
		this.TxtAccountSonNamer.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtAccountSonNamer.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.gridEX1, "gridEX1");
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX1.MouseClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseClick_1);
		resources.ApplyResources(this.simpleButton1, "simpleButton1");
		this.simpleButton1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
		this.simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.simpleButton1.Name = "simpleButton1";
		this.simpleButton1.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		resources.ApplyResources(this.CkCostDetaulte, "CkCostDetaulte");
		this.CkCostDetaulte.Name = "CkCostDetaulte";
		this.CkCostDetaulte.UseVisualStyleBackColor = true;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.gridEX1);
		base.Controls.Add(this.simpleButton1);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmCostCenter";
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNumCostCenter.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccountSonCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccountSonNamer.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		base.ResumeLayout(false);
	}
}


