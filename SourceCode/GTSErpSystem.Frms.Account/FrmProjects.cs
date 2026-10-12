using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Security;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Account;

public class FrmProjects : XtraForm
{
	private Calss_CostCenter Cost = new Calss_CostCenter();

	private ClassProject Pro = new ClassProject();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private GTSdbEntities GTSdb = new GTSdbEntities();

	private List<DataTable> dataTables = new List<DataTable>();

	private DataTable DataTable = new DataTable();

	private bool Here = false;

	private IContainer components = null;

	private GridEX gridEX1;

	private SimpleButton BtnExit;

	private SimpleButton BtnDelete;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private GroupBox groupBox1;

	private System.Windows.Forms.ComboBox DropCostCenter;

	private Label label4;

	private TextBox TxtNote;

	private ButtonEdit TxtCodeProject;

	private Label label3;

	private ButtonEdit TxtProjectName;

	private Label label1;

	private Label label9;

	private Label label2;

	private Label label27;

	private DateEdit TxtDateEnd;

	private DateEdit TxtDateStart;

	private Label label7;

	private Label label5;

	private Label label6;

	private Label label42;

	private TextEdit TxtVat;

	private TextEdit TxtPrice;

	private Label label8;

	private TextEdit TxtTotal;

	private ListBox LstCostCenter;

	private SimpleButton BtnCurrentDelete;

	private SimpleButton BtnDeleteAll;

	private Label LblCodeProject;

	private SimpleButton BtnAddCostCenter;

	private SimpleButton BtnRef;

	private NumericUpDown TxtInsurancePer;

	private NumericUpDown TxtInsuranceAmount;

	public FrmProjects()
	{
		InitializeComponent();
		Clear();
		setIntailal();
		BindGrid();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		FullCostCenter();
		TxtProjectName.Focus();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
	}

	private void setIntailal()
	{
		DataTable.Columns.Add("IdProject", typeof(int));
		DataTable.Columns.Add("الكود", typeof(int));
		DataTable.Columns.Add("مركز التكلفة", typeof(string));
	}

	private void BindGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns.Add("اسم المشروع", typeof(string));
			dataTable.Columns.Add("تاريخ البداية", typeof(string));
			dataTable.Columns.Add("تاريخ الإنتهاء", typeof(string));
			dataTable.Columns.Add("قيمة المشروع", typeof(string));
			dataTable.Columns.Add("ضريبة المشروع", typeof(string));
			dataTable.Columns.Add("قيمة التأمين", typeof(string));
			dataTable.Columns.Add("ضريبة التأمين", typeof(string));
			dataTable.Columns.Add("المبلع شامل الضريبة", typeof(string));
			dataTable.Columns.Add("ملاحظات", typeof(string));
			foreach (Select_SearchAccountProjects_Result searchAccountProject in Pro.GetSearchAccountProjects())
			{
				foreach (Select_SearchAccountCostCenterByProject_Result item in Cost.GetCostCentersByProject(Convert.ToInt32(searchAccountProject.ProjectID)))
				{
					DataTable.Rows.Add(searchAccountProject.ProjectID, item.CostCentersID, item.CostCentersName);
				}
				dataTables.Add(DataTable);
				dataTable.Rows.Add(searchAccountProject.ProjectID, searchAccountProject.ProjectName, searchAccountProject.DateStart, searchAccountProject.DateEnd, searchAccountProject.ProjectPrice, searchAccountProject.ProjectVat, searchAccountProject.InsuranceAmount, searchAccountProject.InsurancePer, searchAccountProject.TotalAmount, searchAccountProject.Note);
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["ID"].Visible = false;
			return;
		}
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable2 = new DataTable("gridEX1");
		dataTable2.Columns.Add("ID", typeof(int));
		dataTable2.Columns.Add("Project Name", typeof(string));
		dataTable2.Columns.Add("Date Start", typeof(string));
		dataTable2.Columns.Add("Date End", typeof(string));
		dataTable2.Columns.Add("Project Price", typeof(string));
		dataTable2.Columns.Add("Project Vat", typeof(string));
		dataTable2.Columns.Add("Insurance Amount", typeof(string));
		dataTable2.Columns.Add("Insurance Per", typeof(string));
		dataTable2.Columns.Add("Total", typeof(string));
		dataTable2.Columns.Add("Note", typeof(string));
		foreach (Select_SearchAccountProjects_Result searchAccountProject2 in Pro.GetSearchAccountProjects())
		{
			dataTable2.Rows.Add(searchAccountProject2.ProjectID, searchAccountProject2.ProjectName, searchAccountProject2.DateStart, searchAccountProject2.DateEnd, searchAccountProject2.ProjectPrice, searchAccountProject2.ProjectVat, searchAccountProject2.InsuranceAmount, searchAccountProject2.InsurancePer, searchAccountProject2.TotalAmount, searchAccountProject2.Note);
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["ID"].Visible = false;
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 139))
		{
			int num = 0;
			if (!string.IsNullOrWhiteSpace(TxtProjectName.Text))
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtProjectName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					if (string.IsNullOrWhiteSpace(TxtPrice.Text))
					{
						TxtPrice.Text = "0";
					}
					if (string.IsNullOrWhiteSpace(TxtTotal.Text))
					{
						TxtTotal.Text = "0";
					}
					if (string.IsNullOrWhiteSpace(TxtInsuranceAmount.Text))
					{
						TxtInsuranceAmount.Text = "0";
					}
					if (string.IsNullOrWhiteSpace(TxtInsurancePer.Text))
					{
						TxtInsurancePer.Text = "0";
					}
					if (string.IsNullOrWhiteSpace(TxtVat.Text))
					{
						TxtVat.Text = "0";
					}
					num = Pro.Insert(out var id, TxtProjectName.Text, Convert.ToDateTime(TxtDateStart.DateTime), Convert.ToDateTime(TxtDateEnd.DateTime), Convert.ToDecimal(TxtPrice.Text), Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtInsuranceAmount.Text), Convert.ToDecimal(TxtInsurancePer.Text), Convert.ToDecimal(TxtTotal.Text), TxtNote.Text, DataTable);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات المشروع  ( " + TxtProjectName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						LblCodeProject.Text = id.ToString();
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات المشروع بنفس الاسم ( " + TxtProjectName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
				case DialogResult.No:
					TxtProjectName.Focus();
					break;
				}
			}
			else if (TxtProjectName.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم المشروع ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtProjectName.Focus();
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
			if (!string.IsNullOrWhiteSpace(TxtProjectName.Text))
			{
				switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + TxtProjectName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					if (string.IsNullOrWhiteSpace(TxtPrice.Text))
					{
						TxtPrice.Text = "0";
					}
					if (string.IsNullOrWhiteSpace(TxtTotal.Text))
					{
						TxtTotal.Text = "0";
					}
					if (string.IsNullOrWhiteSpace(TxtInsuranceAmount.Text))
					{
						TxtInsuranceAmount.Text = "0";
					}
					if (string.IsNullOrWhiteSpace(TxtInsurancePer.Text))
					{
						TxtInsurancePer.Text = "0";
					}
					if (string.IsNullOrWhiteSpace(TxtVat.Text))
					{
						TxtVat.Text = "0";
					}
					int position = gridEX1.SelectedItems[0].Position;
					int iD = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
					num = Pro.Update(iD, TxtProjectName.Text, Convert.ToDateTime(TxtDateStart.DateTime), Convert.ToDateTime(TxtDateEnd.DateTime), Convert.ToDecimal(TxtPrice.Text), Convert.ToDecimal(TxtVat.Text), Convert.ToDecimal(TxtInsuranceAmount.Text), Convert.ToDecimal(TxtInsurancePer.Text), Convert.ToDecimal(TxtTotal.Text), TxtNote.Text, DataTable);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات المشروع ( " + TxtProjectName.Text + " ) بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات المشروع بنفس الاسم ( " + TxtProjectName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
				case DialogResult.No:
					TxtProjectName.Focus();
					break;
				}
			}
			else if (TxtProjectName.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم المشروع ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtProjectName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 139))
		{
			int num = 0;
			if (!string.IsNullOrWhiteSpace(TxtProjectName.Text))
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtProjectName.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					int position = gridEX1.SelectedItems[0].Position;
					int sn = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
					num = Pro.Delete(sn);
					if (num > 0)
					{
						BindGrid();
						MessageBox.Show("تم حذف بيانات المشروع ( " + TxtProjectName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						Clear();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtProjectName.Text + " ) لوجود عمليات جارية بهذا المشروع", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				}
				case DialogResult.No:
					TxtProjectName.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر اسم المشروع ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				gridEX1.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	public void FullCostCenter()
	{
		DropCostCenter.DataSource = Cost.GetSearchAccountCostCenter();
		DropCostCenter.DisplayMember = "CostCentersName";
		DropCostCenter.ValueMember = "CostCentersID";
	}

	private void Clear()
	{
		LblCodeProject.Text = "0";
		TxtCodeProject.Text = "";
		TxtProjectName.Text = "";
		TxtDateEnd.DateTime = DateTime.Now;
		TxtDateStart.DateTime = DateTime.Now;
		TxtInsuranceAmount.Text = "0.0";
		TxtInsurancePer.Text = "0.0";
		TxtNote.Text = "0.0";
		TxtPrice.Text = "0.0";
		TxtTotal.Text = "0.0";
		TxtVat.Text = "0.0";
		DataTable.Rows.Clear();
		LstCostCenter.Items.Clear();
	}

	private void EnabledTrue()
	{
		TxtCodeProject.Enabled = true;
		TxtProjectName.Enabled = true;
		TxtDateEnd.Enabled = true;
		TxtDateStart.Enabled = true;
		TxtInsuranceAmount.Enabled = true;
		TxtInsurancePer.Enabled = true;
		TxtNote.Enabled = true;
		TxtPrice.Enabled = true;
		TxtTotal.Enabled = true;
		TxtVat.Enabled = true;
	}

	private void DataTableFullRow()
	{
		DataRow dataRow = DataTable.NewRow();
		dataRow["IdProject"] = 0;
		dataRow["الكود"] = Convert.ToInt32(DropCostCenter.SelectedValue);
		dataRow["مركز التكلفة"] = DropCostCenter.Text;
		DataTable.Rows.Add(dataRow);
		LstCostCenter.Items.Add(DropCostCenter.Text);
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

	private void DropCostCenter_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (Here)
		{
			return;
		}
		try
		{
			if (LstCostCenter.Items.Count <= 0)
			{
				DataTableFullRow();
				return;
			}
			foreach (object item in LstCostCenter.Items)
			{
				string text = DropCostCenter.Text;
				if (text == item.ToString())
				{
					MessageBox.Show("مركز التكلفة موجود من قبل");
					return;
				}
			}
			DataTableFullRow();
		}
		catch
		{
		}
	}

	private void BtnDeleteAll_Click(object sender, EventArgs e)
	{
		if (LstCostCenter.Items.Count > 0)
		{
			LstCostCenter.Items.Clear();
			DataTable.Rows.Clear();
		}
	}

	private void BtnCurrentDelete_Click(object sender, EventArgs e)
	{
		if (LstCostCenter.Items.Count > 0)
		{
			if (LstCostCenter.SelectedItem != null)
			{
				DataTable.Rows.RemoveAt(LstCostCenter.SelectedIndex);
				LstCostCenter.Items.Remove(LstCostCenter.SelectedItem);
				LstCostCenter.Refresh();
			}
			else
			{
				MessageBox.Show("حدد المركز المراد حذفه");
			}
		}
	}

	private void LstCostCenter_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		Delete();
		BtnNew_Click(null, null);
	}

	private void gridEX1_Click(object sender, EventArgs e)
	{
		DataTable.Rows.Clear();
		LstCostCenter.Items.Clear();
		TxtCodeProject.Text = gridEX1.CurrentRow.Cells[0].Value.ToString();
		TxtProjectName.Text = gridEX1.CurrentRow.Cells[1].Value.ToString();
		TxtDateStart.DateTime = Convert.ToDateTime(gridEX1.CurrentRow.Cells[2].Value.ToString());
		TxtDateEnd.DateTime = Convert.ToDateTime(gridEX1.CurrentRow.Cells[3].Value.ToString());
		TxtPrice.Text = gridEX1.CurrentRow.Cells[4].Value.ToString();
		TxtVat.Text = gridEX1.CurrentRow.Cells[5].Value.ToString();
		TxtInsuranceAmount.Text = gridEX1.CurrentRow.Cells[6].Value.ToString();
		TxtInsurancePer.Text = gridEX1.CurrentRow.Cells[7].Value.ToString();
		TxtTotal.Text = gridEX1.CurrentRow.Cells[8].Value.ToString();
		TxtNote.Text = gridEX1.CurrentRow.Cells[9].Value.ToString();
		List<Select_SearchAccountCostCenterByProject_Result> list = GTSdb.Select_SearchAccountCostCenterByProject(Convert.ToInt32(TxtCodeProject.Text), LoginDetails.BranchID).ToList();
		foreach (Select_SearchAccountCostCenterByProject_Result item in list)
		{
			DataRow dataRow = DataTable.NewRow();
			dataRow["IdProject"] = Convert.ToInt32(TxtCodeProject.Text);
			dataRow["الكود"] = Convert.ToInt32(item.CostCentersID);
			dataRow["مركز التكلفة"] = item.CostCentersName;
			DataTable.Rows.Add(dataRow);
			LstCostCenter.Items.Add(item.CostCentersName);
		}
		BtnSave.Enabled = false;
		BtnEdit.Enabled = true;
		BtnDelete.Enabled = true;
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void TxtPrice_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnAddCostCenter_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 29))
		{
			new FrmCostCenter().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRef_Click(object sender, EventArgs e)
	{
		Here = true;
		FullCostCenter();
		Here = false;
	}

	private void TxMoney_ValueChanged(object sender, EventArgs e)
	{
		TxtInsuranceAmount.Text = (Convert.ToDecimal(TxtPrice.Text) * (TxtInsurancePer.Value / 100m)).ToString();
	}

	private void TxtInsuranceAmount_KeyUp(object sender, KeyEventArgs e)
	{
		if (Convert.ToDecimal(TxtInsuranceAmount.Text) > Convert.ToDecimal(TxtPrice.Text))
		{
			TxtInsurancePer.Value = Math.Round(Convert.ToDecimal(TxtInsuranceAmount.Text) * 100m / Convert.ToDecimal(TxtPrice.Text), 2);
		}
		else
		{
			TxtInsurancePer.Value = 0m;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Account.FrmProjects));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions value = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.LblCodeProject = new System.Windows.Forms.Label();
		this.LstCostCenter = new System.Windows.Forms.ListBox();
		this.label8 = new System.Windows.Forms.Label();
		this.BtnCurrentDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDeleteAll = new DevExpress.XtraEditors.SimpleButton();
		this.BtnRef = new DevExpress.XtraEditors.SimpleButton();
		this.BtnAddCostCenter = new DevExpress.XtraEditors.SimpleButton();
		this.label7 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label42 = new System.Windows.Forms.Label();
		this.TxtTotal = new DevExpress.XtraEditors.TextEdit();
		this.TxtVat = new DevExpress.XtraEditors.TextEdit();
		this.TxtPrice = new DevExpress.XtraEditors.TextEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.TxtDateEnd = new DevExpress.XtraEditors.DateEdit();
		this.TxtDateStart = new DevExpress.XtraEditors.DateEdit();
		this.DropCostCenter = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.TxtCodeProject = new DevExpress.XtraEditors.ButtonEdit();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtProjectName = new DevExpress.XtraEditors.ButtonEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.TxtInsurancePer = new System.Windows.Forms.NumericUpDown();
		this.TxtInsuranceAmount = new System.Windows.Forms.NumericUpDown();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTotal.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtVat.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPrice.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateEnd.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateEnd.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateStart.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateStart.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeProject.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtProjectName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtInsurancePer).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtInsuranceAmount).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.gridEX1, "gridEX1");
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.LinkFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.NewRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowWithErrorsFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedInactiveFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX1.Click += new System.EventHandler(gridEX1_Click);
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.TxtInsuranceAmount);
		this.groupBox1.Controls.Add(this.TxtInsurancePer);
		this.groupBox1.Controls.Add(this.LblCodeProject);
		this.groupBox1.Controls.Add(this.LstCostCenter);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.BtnCurrentDelete);
		this.groupBox1.Controls.Add(this.BtnDeleteAll);
		this.groupBox1.Controls.Add(this.BtnRef);
		this.groupBox1.Controls.Add(this.BtnAddCostCenter);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label42);
		this.groupBox1.Controls.Add(this.TxtTotal);
		this.groupBox1.Controls.Add(this.TxtVat);
		this.groupBox1.Controls.Add(this.TxtPrice);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label27);
		this.groupBox1.Controls.Add(this.TxtDateEnd);
		this.groupBox1.Controls.Add(this.TxtDateStart);
		this.groupBox1.Controls.Add(this.DropCostCenter);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.TxtCodeProject);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.TxtProjectName);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.LblCodeProject, "LblCodeProject");
		this.LblCodeProject.Name = "LblCodeProject";
		resources.ApplyResources(this.LstCostCenter, "LstCostCenter");
		this.LstCostCenter.FormattingEnabled = true;
		this.LstCostCenter.Name = "LstCostCenter";
		this.LstCostCenter.SelectedIndexChanged += new System.EventHandler(LstCostCenter_SelectedIndexChanged);
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.BtnCurrentDelete, "BtnCurrentDelete");
		this.BtnCurrentDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCurrentDelete.ImageOptions.Image");
		this.BtnCurrentDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCurrentDelete.Name = "BtnCurrentDelete";
		this.BtnCurrentDelete.Click += new System.EventHandler(BtnCurrentDelete_Click);
		resources.ApplyResources(this.BtnDeleteAll, "BtnDeleteAll");
		this.BtnDeleteAll.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDeleteAll.ImageOptions.Image");
		this.BtnDeleteAll.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDeleteAll.Name = "BtnDeleteAll";
		this.BtnDeleteAll.Click += new System.EventHandler(BtnDeleteAll_Click);
		resources.ApplyResources(this.BtnRef, "BtnRef");
		this.BtnRef.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRef.ImageOptions.Image");
		this.BtnRef.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnRef.Name = "BtnRef";
		this.BtnRef.Click += new System.EventHandler(BtnRef_Click);
		resources.ApplyResources(this.BtnAddCostCenter, "BtnAddCostCenter");
		this.BtnAddCostCenter.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAddCostCenter.ImageOptions.Image");
		this.BtnAddCostCenter.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnAddCostCenter.Name = "BtnAddCostCenter";
		this.BtnAddCostCenter.Click += new System.EventHandler(BtnAddCostCenter_Click);
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.label42, "label42");
		this.label42.Name = "label42";
		resources.ApplyResources(this.TxtTotal, "TxtTotal");
		this.TxtTotal.Name = "TxtTotal";
		this.TxtTotal.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtTotal.Properties.Appearance.Font");
		this.TxtTotal.Properties.Appearance.Options.UseFont = true;
		this.TxtTotal.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTotal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.TxtTotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPrice_KeyPress);
		resources.ApplyResources(this.TxtVat, "TxtVat");
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtVat.Properties.Appearance.Font");
		this.TxtVat.Properties.Appearance.Options.UseFont = true;
		this.TxtVat.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtVat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtVat.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.TxtVat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPrice_KeyPress);
		resources.ApplyResources(this.TxtPrice, "TxtPrice");
		this.TxtPrice.Name = "TxtPrice";
		this.TxtPrice.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPrice.Properties.Appearance.Font");
		this.TxtPrice.Properties.Appearance.Options.UseFont = true;
		this.TxtPrice.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.TxtPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.TxtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtPrice_KeyPress);
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.label27, "label27");
		this.label27.Name = "label27";
		resources.ApplyResources(this.TxtDateEnd, "TxtDateEnd");
		this.TxtDateEnd.Name = "TxtDateEnd";
		this.TxtDateEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateEnd.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDateEnd.Properties.Appearance.Font");
		this.TxtDateEnd.Properties.Appearance.Options.UseFont = true;
		this.TxtDateEnd.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateEnd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDateEnd.Properties.Buttons"))
		});
		this.TxtDateEnd.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDateEnd.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDateEnd.Properties.DisplayFormat.FormatString = "";
		this.TxtDateEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateEnd.Properties.EditFormat.FormatString = "";
		this.TxtDateEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateEnd.Properties.Mask.EditMask = resources.GetString("TxtDateEnd.Properties.Mask.EditMask");
		this.TxtDateEnd.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDateEnd.Properties.Mask.UseMaskAsDisplayFormat");
		resources.ApplyResources(this.TxtDateStart, "TxtDateStart");
		this.TxtDateStart.Name = "TxtDateStart";
		this.TxtDateStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateStart.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDateStart.Properties.Appearance.Font");
		this.TxtDateStart.Properties.Appearance.Options.UseFont = true;
		this.TxtDateStart.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateStart.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDateStart.Properties.Buttons"))
		});
		this.TxtDateStart.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDateStart.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDateStart.Properties.DisplayFormat.FormatString = "";
		this.TxtDateStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateStart.Properties.EditFormat.FormatString = "";
		this.TxtDateStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateStart.Properties.Mask.EditMask = resources.GetString("TxtDateStart.Properties.Mask.EditMask");
		this.TxtDateStart.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDateStart.Properties.Mask.UseMaskAsDisplayFormat");
		resources.ApplyResources(this.DropCostCenter, "DropCostCenter");
		this.DropCostCenter.FormattingEnabled = true;
		this.DropCostCenter.Name = "DropCostCenter";
		this.DropCostCenter.SelectedIndexChanged += new System.EventHandler(DropCostCenter_SelectedIndexChanged);
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		resources.ApplyResources(this.TxtCodeProject, "TxtCodeProject");
		this.TxtCodeProject.Name = "TxtCodeProject";
		this.TxtCodeProject.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCodeProject.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCodeProject.Properties.Appearance.Font");
		this.TxtCodeProject.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCodeProject.Properties.Appearance.Options.UseFont = true;
		this.TxtCodeProject.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCodeProject.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCodeProject.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtCodeProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(value, "editorButtonImageOptions1");
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.TxtProjectName, "TxtProjectName");
		this.TxtProjectName.Name = "TxtProjectName";
		this.TxtProjectName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtProjectName.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtProjectName.Properties.Appearance.Font");
		this.TxtProjectName.Properties.Appearance.Options.UseBackColor = true;
		this.TxtProjectName.Properties.Appearance.Options.UseFont = true;
		this.TxtProjectName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtProjectName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtProjectName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtProjectName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions2");
		this.TxtProjectName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtProjectName.Properties.Buttons"), resources.GetString("TxtProjectName.Properties.Buttons1"), (int)resources.GetObject("TxtProjectName.Properties.Buttons2"), (bool)resources.GetObject("TxtProjectName.Properties.Buttons3"), (bool)resources.GetObject("TxtProjectName.Properties.Buttons4"), (bool)resources.GetObject("TxtProjectName.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtProjectName.Properties.Buttons6"), resources.GetObject("TxtProjectName.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtProjectName.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtProjectName.Properties.Buttons9"))
		});
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
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
		resources.ApplyResources(this.TxtInsurancePer, "TxtInsurancePer");
		this.TxtInsurancePer.DecimalPlaces = 2;
		this.TxtInsurancePer.Name = "TxtInsurancePer";
		this.TxtInsurancePer.ValueChanged += new System.EventHandler(TxMoney_ValueChanged);
		resources.ApplyResources(this.TxtInsuranceAmount, "TxtInsuranceAmount");
		this.TxtInsuranceAmount.DecimalPlaces = 2;
		this.TxtInsuranceAmount.Maximum = new decimal(new int[4] { 1874919423, 2328306, 0, 0 });
		this.TxtInsuranceAmount.Name = "TxtInsuranceAmount";
		this.TxtInsuranceAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtInsuranceAmount_KeyUp);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.gridEX1);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.groupBox1);
		base.MaximizeBox = false;
		base.Name = "FrmProjects";
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTotal.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtVat.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPrice.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateEnd.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateEnd.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateStart.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateStart.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeProject.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtProjectName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtInsurancePer).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtInsuranceAmount).EndInit();
		base.ResumeLayout(false);
	}
}


