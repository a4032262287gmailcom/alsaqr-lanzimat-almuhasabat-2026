using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Security;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Cust_Sup;

public class FrmStores : XtraForm
{
	private Class_Branches BRN = new Class_Branches();

	private Class_Stores STR = new Class_Stores();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	public int GrpID;

	public bool IsCustomer;

	public bool IsSupplier;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label12;

	private Label label10;

	private TextBox TxtPhone;

	private Label label9;

	private TextBox TxtFax;

	private Label label8;

	private TextBox TxtAddress;

	private Label label5;

	private TextBox TxtName;

	private System.Windows.Forms.ComboBox CBBranch;

	public GridEX gridEX1;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	public FrmStores()
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
		BindBranch();
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
			Account_Stores account_Stores = (from x in STR.GetAll()
				where x.ID == id
				select x).FirstOrDefault();
			TxtName.Text = account_Stores.Store_Name;
			TxtPhone.Text = account_Stores.Phone;
			TxtFax.Text = account_Stores.Fax;
			CBBranch.SelectedValue = account_Stores.BranchID;
			TxtAddress.Text = account_Stores.Address;
			TxtName.Focus();
			BtnSave.Enabled = false;
			BtnEdit.Enabled = true;
			BtnDelete.Enabled = true;
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

	private void CombName_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void TxtNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
	}

	private void BindGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("كود", typeof(int));
			dataTable.Columns.Add("اسم المستودع", typeof(string));
			dataTable.Columns.Add("الفرع", typeof(string));
			dataTable.Columns.Add("العنوان", typeof(string));
			foreach (Account_Stores item in from x in STR.GetAll()
				where x.ID > 0
				select x)
			{
				dataTable.Rows.Add(item.ID, item.Store_Name, BRN.GetName(Convert.ToInt32(item.BranchID)), item.Address);
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["كود"].Width = 30;
			gridEX1.RootTable.Columns["كود"].RightToLeft = RightToLeft.No;
			return;
		}
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable2 = new DataTable("gridEX1");
		dataTable2.Columns.Add("Code", typeof(int));
		dataTable2.Columns.Add("Store Name", typeof(string));
		dataTable2.Columns.Add("Branch", typeof(string));
		dataTable2.Columns.Add("Address", typeof(string));
		foreach (Account_Stores item2 in from x in STR.GetAll()
			where x.ID > 0
			select x)
		{
			dataTable2.Rows.Add(item2.ID, item2.Store_Name, BRN.GetName(Convert.ToInt32(item2.BranchID)), item2.Address);
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["Code"].Width = 30;
		gridEX1.RootTable.Columns["Code"].RightToLeft = RightToLeft.No;
	}

	private void Clear()
	{
		TxtName.Text = "";
		TxtPhone.Text = "";
		TxtFax.Text = "";
		TxtAddress.Text = "";
		CBBranch.SelectedValue = 1;
		BindGrid();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtName.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 2))
		{
			int num = 0;
			if (TxtName.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = STR.Insert(TxtName.Text, Convert.ToInt32(CBBranch.SelectedValue), TxtPhone.Text, TxtFax.Text, TxtAddress.Text);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات المستودعات  ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات المستودعات بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (TxtName.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم المستودعات ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
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
			int num = 0;
			if (TxtName.Text != "" && CBBranch.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + TxtName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					int position = gridEX1.SelectedItems[0].Position;
					int iD = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
					num = STR.Update(iD, TxtName.Text, Convert.ToInt32(CBBranch.SelectedValue), TxtPhone.Text, TxtFax.Text, TxtAddress.Text);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات المستودعات ( " + TxtName.Text + " ) بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات المستودعات بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (TxtName.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم المستودعات ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
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
			if (TxtName.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtName.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					int position = gridEX1.SelectedItems[0].Position;
					int sn = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
					num = STR.Delete(sn);
					if (num > 0)
					{
						BindGrid();
						MessageBox.Show("تم حذف بيانات المستودعات ( " + TxtName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						Clear();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtName.Text + " ) لوجود عمليات جارية بهذا المستودعات", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				}
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر اسم المستودعات ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BindBranch()
	{
		CBBranch.DataSource = BRN.GetAll();
		CBBranch.DisplayMember = "Name";
		CBBranch.ValueMember = "ID";
		CBBranch.SelectedValue = 1;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Cust_Sup.FrmStores));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.CBBranch = new System.Windows.Forms.ComboBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtPhone = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtFax = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtAddress = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtName = new System.Windows.Forms.TextBox();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.CBBranch);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.TxtPhone);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.TxtFax);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.TxtAddress);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.TxtName);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.CBBranch, "CBBranch");
		this.CBBranch.FormattingEnabled = true;
		this.CBBranch.Name = "CBBranch";
		this.CBBranch.Enter += new System.EventHandler(textBox1_Enter);
		this.CBBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.CBBranch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.TxtPhone, "TxtPhone");
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.TxtFax, "TxtFax");
		this.TxtFax.Name = "TxtFax";
		this.TxtFax.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.TxtAddress, "TxtAddress");
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.TxtName, "TxtName");
		this.TxtName.Name = "TxtName";
		this.TxtName.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.gridEX1, "gridEX1");
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.AlternatingRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
		this.gridEX1.FocusCellFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FocusCellFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxInfoFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.LinkFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.LinkFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.NewRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.NewRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.PreviewRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.PreviewRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedInactiveFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedInactiveFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TableHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX1.MouseClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseClick_1);
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnEdit.Appearance.Font");
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNew.Appearance.Font");
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDelete.Appearance.Font");
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.gridEX1);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmStores";
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		base.ResumeLayout(false);
	}
}


