using System;
using System.Collections.Generic;
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
using GTSErpSystem.BLL.Payroll;
using GTSErpSystem.BLL.Security;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Payroll;

public class FrmEmployee : XtraForm
{
	private Class_Employee Emp = new Class_Employee();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	public int GrpID;

	private DataTable dt = new DataTable();

	private decimal NetAllowances;

	private IContainer components = null;

	private SimpleButton BtnExit;

	private SimpleButton BtnDelete;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private Label label2;

	private GroupBox groupBox1;

	private ButtonEdit TxtName;

	private GridEX gridEX1;

	private Label label10;

	private TextBox TxtPhone;

	private Label label8;

	private TextBox TxtAddress;

	private System.Windows.Forms.ComboBox CBBranch;

	private Label label5;

	private NumericUpDown Txtprofit;

	private Label label7;

	private System.Windows.Forms.ComboBox CBAllowances;

	private Label label1;

	private GridEX gridEX2;

	public FrmEmployee()
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
		BindCB();
		BindGrid();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtName.Focus();
		Txtprofit.DecimalPlaces = 2;
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		dt.Columns.Add("كود", typeof(int));
		dt.Columns.Add("اسم البدل", typeof(string));
		dt.Columns.Add("Money", typeof(decimal));
		dt.Columns.Add("حذف", typeof(string));
		gridEX2.SetDataBinding(dt, "");
		base.KeyPreview = true;
		Clear();
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
		dt.Clear();
		try
		{
			int position = gridEX1.SelectedItems[0].Position;
			int id = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
			Emp_Employee emp_Employee = (from x in Emp.GetAllUsers()
				where x.ID == id
				select x).FirstOrDefault();
			DataTable employeeAllowances = Emp.GetEmployeeAllowances(id);
			CBAllowances.Text = "اضافه بدل";
			if (employeeAllowances.Rows.Count > 0)
			{
				foreach (DataRow row in employeeAllowances.Rows)
				{
					int AllowanceID = Convert.ToInt32(row["AllowanceID"]);
					string text = row["AllowanceName"].ToString();
					decimal num = Convert.ToDecimal(row["Money"]);
					if (!dt.AsEnumerable().Any((DataRow r) => r.Field<int>("كود") == AllowanceID))
					{
						dt.Rows.Add(AllowanceID, text, num, "â‌Œ");
					}
				}
			}
			gridEX2.DataSource = dt;
			gridEX2.Refetch();
			TxtName.Text = emp_Employee.Name;
			TxtPhone.Text = emp_Employee.Phone;
			Txtprofit.Value = Convert.ToDecimal(emp_Employee.Money);
			TxtAddress.Text = emp_Employee.Address;
			CBBranch.SelectedValue = emp_Employee.Branch_ID;
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

	private void Comb_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void CBAllowances_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void CBBranch_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindGridByBranch();
	}

	private void BindCB()
	{
		CBBranch.DataSource = Emp.GetAllBranch();
		CBBranch.DisplayMember = "Name";
		CBBranch.ValueMember = "ID";
		CBBranch.SelectedValue = 1;
		List<Emp_Allowances> allowances = Emp.GetAllowances();
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("ID", typeof(int));
		dataTable.Columns.Add("Name", typeof(string));
		dataTable.Rows.Add(0, "لا يوجد");
		foreach (Emp_Allowances item in allowances)
		{
			dataTable.Rows.Add(item.ID, item.Name);
		}
		CBAllowances.DataSource = dataTable;
		CBAllowances.DisplayMember = "Name";
		CBAllowances.ValueMember = "ID";
		CBAllowances.SelectedValue = 0;
	}

	private void BindGrid()
	{
		try
		{
			if (LoginDetails.Lang == "ar-EG")
			{
				gridEX1.ColumnAutoResize = true;
				DataTable dataTable = new DataTable("gridEX1");
				dataTable.Columns.Add("كود", typeof(int));
				dataTable.Columns.Add("اسم الموظف", typeof(string));
				dataTable.Columns.Add("الجوال", typeof(string));
				dataTable.Columns.Add("الراتب", typeof(decimal));
				dataTable.Columns.Add("العنوان", typeof(string));
				dataTable.Columns.Add("الفرع", typeof(int));
				foreach (Emp_Employee item in from x in Emp.GetAllUsers()
					where x.ID > 0
					select x)
				{
					dataTable.Rows.Add(item.ID, item.Name, item.Phone, item.Money, item.Address, item.Branch_ID);
				}
				gridEX1.SetDataBinding(dataTable, "");
				gridEX1.RetrieveStructure();
				gridEX1.RootTable.Columns["كود"].Width = 30;
				gridEX1.RootTable.Columns["كود"].RightToLeft = RightToLeft.No;
				gridEX1.RootTable.Columns["كود"].TextAlignment = TextAlignment.Center;
				gridEX1.RootTable.Columns["اسم الموظف"].TextAlignment = TextAlignment.Center;
				gridEX1.RootTable.Columns["الجوال"].TextAlignment = TextAlignment.Center;
				gridEX1.RootTable.Columns["الفرع"].Visible = false;
				gridEX1.RootTable.Columns["الراتب"].Visible = false;
				gridEX1.RootTable.Columns["العنوان"].Visible = false;
			}
			else
			{
				gridEX1.ColumnAutoResize = true;
				DataTable dataTable2 = new DataTable("gridEX1");
				dataTable2.Columns.Add("Code", typeof(int));
				dataTable2.Columns.Add("Name", typeof(string));
				dataTable2.Columns.Add("Number", typeof(string));
				dataTable2.Columns.Add("الراتب", typeof(decimal));
				dataTable2.Columns.Add("العنوان", typeof(string));
				dataTable2.Columns.Add("الفرع", typeof(int));
				foreach (Emp_Employee item2 in from x in Emp.GetAllUsers()
					where x.ID > 0
					select x)
				{
					dataTable2.Rows.Add(item2.ID, item2.Name, item2.Phone, item2.Money, item2.Address, item2.Branch_ID);
				}
				gridEX1.SetDataBinding(dataTable2, "");
				gridEX1.RetrieveStructure();
				gridEX1.RootTable.Columns["Code"].Width = 30;
				gridEX1.RootTable.Columns["Code"].RightToLeft = RightToLeft.No;
				gridEX1.RootTable.Columns["Code"].TextAlignment = TextAlignment.Center;
				gridEX1.RootTable.Columns["Name"].TextAlignment = TextAlignment.Center;
				gridEX1.RootTable.Columns["Number"].TextAlignment = TextAlignment.Center;
				gridEX1.RootTable.Columns["الفرع"].Visible = false;
				gridEX1.RootTable.Columns["الراتب"].Visible = false;
				gridEX1.RootTable.Columns["العنوان"].Visible = false;
			}
		}
		catch (Exception)
		{
		}
		try
		{
			if (LoginDetails.Lang == "ar-EG")
			{
				gridEX2.ColumnAutoResize = true;
				DataTable dataTable3 = new DataTable("gridEX2");
				dataTable3.Columns.Add("كود", typeof(int));
				dataTable3.Columns.Add("اسم البدل", typeof(string));
				dataTable3.Columns.Add("Mony", typeof(decimal));
				dataTable3.Columns.Add("حذف", typeof(string));
				gridEX2.SetDataBinding(dataTable3, "");
				gridEX2.RetrieveStructure();
				gridEX2.RootTable.Columns["كود"].Width = 60;
				gridEX2.RootTable.Columns["كود"].RightToLeft = RightToLeft.No;
				gridEX2.RootTable.Columns["كود"].TextAlignment = TextAlignment.Center;
				gridEX2.RootTable.Columns["اسم البدل"].Width = 120;
				gridEX2.RootTable.Columns["اسم البدل"].TextAlignment = TextAlignment.Center;
				gridEX2.RootTable.Columns["Mony"].Visible = false;
				gridEX2.RootTable.Columns["حذف"].Width = 70;
				gridEX2.RootTable.Columns["حذف"].ButtonStyle = ButtonStyle.ButtonCell;
				gridEX2.RootTable.Columns["حذف"].TextAlignment = TextAlignment.Center;
				gridEX2.RootTable.Columns["حذف"].Caption = "حذف";
				gridEX2.RootTable.Columns["حذف"].EditType = EditType.NoEdit;
			}
			else
			{
				gridEX2.ColumnAutoResize = true;
				DataTable dataTable4 = new DataTable("gridEX2");
				dataTable4.Columns.Add("Code", typeof(int));
				dataTable4.Columns.Add("Name", typeof(string));
				gridEX2.SetDataBinding(dataTable4, "");
				gridEX2.RetrieveStructure();
				gridEX2.RootTable.Columns["Code"].Width = 30;
				gridEX2.RootTable.Columns["Code"].RightToLeft = RightToLeft.No;
				gridEX2.RootTable.Columns["Code"].TextAlignment = TextAlignment.Center;
				gridEX2.RootTable.Columns["Name"].TextAlignment = TextAlignment.Center;
			}
		}
		catch (Exception)
		{
		}
	}

	private void BindGridByBranch()
	{
		try
		{
			int BRID = Convert.ToInt32(CBBranch.SelectedValue);
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("كود", typeof(int));
			dataTable.Columns.Add("اسم الموظف", typeof(string));
			dataTable.Columns.Add("الجوال", typeof(string));
			dataTable.Columns.Add("الراتب", typeof(decimal));
			dataTable.Columns.Add("العنوان", typeof(string));
			dataTable.Columns.Add("الفرع", typeof(int));
			foreach (Emp_Employee item in from x in Emp.GetAllUsers()
				where x.Branch_ID == BRID
				select x)
			{
				dataTable.Rows.Add(item.ID, item.Name, item.Phone, item.Money, item.Address, item.Branch_ID);
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["كود"].Width = 30;
			gridEX1.RootTable.Columns["كود"].RightToLeft = RightToLeft.No;
			gridEX1.RootTable.Columns["كود"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["اسم الموظف"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الجوال"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الفرع"].Visible = false;
			gridEX1.RootTable.Columns["الراتب"].Visible = false;
			gridEX1.RootTable.Columns["العنوان"].Visible = false;
		}
		catch (Exception)
		{
			BindGrid();
		}
	}

	private void Clear()
	{
		TxtName.Text = "";
		TxtPhone.Text = "";
		Txtprofit.Value = 0m;
		TxtAddress.Text = "";
		CBBranch.SelectedValue = 1;
		CBAllowances.SelectedValue = 0;
		TxtName.Focus();
		dt.Clear();
		((DataTable)gridEX2.DataSource)?.Clear();
		if (!dt.Columns.Contains("كود"))
		{
			dt.Columns.Add("كود", typeof(int));
		}
		if (!dt.Columns.Contains("اسم البدل"))
		{
			dt.Columns.Add("اسم البدل", typeof(string));
		}
		if (!dt.Columns.Contains("Money"))
		{
			dt.Columns.Add("Money", typeof(decimal));
		}
		if (!dt.Columns.Contains("حذف"))
		{
			dt.Columns.Add("حذف", typeof(string));
		}
		gridEX2.SetDataBinding(dt, "");
		base.KeyPreview = true;
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 76))
		{
			int num = 0;
			if (TxtName.Text != "" || Txtprofit.Value == 0m || Convert.ToInt32(CBBranch.SelectedValue) == 0)
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					if (gridEX2.DataSource is DataTable dataTable)
					{
						NetAllowances = ((dataTable.Compute("SUM(Money)", "") != DBNull.Value) ? Convert.ToDecimal(dataTable.Compute("SUM(Money)", "")) : 0m);
					}
					num = Emp.Insert(TxtName.Text, Txtprofit.Value, TxtPhone.Text, TxtAddress.Text, Convert.ToInt32(CBBranch.SelectedValue), dt, Math.Round(NetAllowances, 2));
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات الموظف  ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات الموظف بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (TxtName.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم الموظف ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
			else if (Txtprofit.Value == 0m)
			{
				MessageBox.Show(" عفوا، ادخل الراتب ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				Txtprofit.Focus();
			}
			else if (Convert.ToInt32(CBBranch.SelectedValue) == 0)
			{
				MessageBox.Show(" عفوا، ادخل الفرع ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				CBBranch.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 76))
		{
			int num = 0;
			if (TxtName.Text != "" || Txtprofit.Value == 0m || Convert.ToInt32(CBBranch.SelectedValue) == 0)
			{
				switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + TxtName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					if (gridEX2.DataSource is DataTable dataTable)
					{
						NetAllowances = ((dataTable.Compute("SUM(Money)", "") != DBNull.Value) ? Convert.ToDecimal(dataTable.Compute("SUM(Money)", "")) : 0m);
					}
					int position = gridEX1.SelectedItems[0].Position;
					int iD = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
					num = Emp.Update(iD, TxtName.Text, Txtprofit.Value, TxtPhone.Text, TxtAddress.Text, Convert.ToInt32(CBBranch.SelectedValue), dt, Math.Round(NetAllowances, 2));
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات الموظف ( " + TxtName.Text + " ) بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات الموظف بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
				MessageBox.Show(" عفوا، ادخل اسم الموظف ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
			else if (Txtprofit.Value == 0m)
			{
				MessageBox.Show(" عفوا، ادخل الراتب ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				Txtprofit.Focus();
			}
			else if (Convert.ToInt32(CBBranch.SelectedValue) == 0)
			{
				MessageBox.Show(" عفوا، ادخل الفرع ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				CBBranch.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 76))
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
					num = Emp.Delete(sn);
					if (num > 0)
					{
						BindGrid();
						MessageBox.Show("تم حذف بيانات الموظف ( " + TxtName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						Clear();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtName.Text + " ) لوجود عمليات جارية بهذا الموظف", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
				MessageBox.Show(" عفوا، ادخل اسم الموظف ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
			else if (Txtprofit.Value == 0m)
			{
				MessageBox.Show(" عفوا، ادخل الراتب ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				Txtprofit.Focus();
			}
			else if (Convert.ToInt32(CBBranch.SelectedValue) == 0)
			{
				MessageBox.Show(" عفوا، ادخل الفرع ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				CBBranch.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void CBAllowances_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (CBAllowances.SelectedItem != null)
		{
			int selectedID = 0;
			string text = CBAllowances.Text;
			if (CBAllowances.SelectedValue is DataRowView dataRowView)
			{
				selectedID = Convert.ToInt32(dataRowView["ID"]);
			}
			else
			{
				selectedID = Convert.ToInt32(CBAllowances.SelectedValue);
			}
			decimal valueOrDefault = Emp.GetAllowanceMoney(selectedID).GetValueOrDefault();
			if (selectedID != 0 && !dt.AsEnumerable().Any((DataRow row) => row.Field<int>("كود") == selectedID))
			{
				dt.Rows.Add(selectedID, text, valueOrDefault, "â‌Œ");
				gridEX2.DataSource = dt;
				gridEX2.Refetch();
			}
		}
	}

	private void gridEX2_MouseClick(object sender, MouseEventArgs e)
	{
		GridEXRow currentRow = gridEX2.CurrentRow;
		if (currentRow != null && currentRow.RowType == RowType.Record)
		{
			GridEXColumn currentColumn = gridEX2.CurrentColumn;
			if (currentColumn != null && currentColumn.Key == "حذف")
			{
				gridEX2.CurrentRow.Delete();
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Payroll.FrmEmployee));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.CBAllowances = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.Txtprofit = new System.Windows.Forms.NumericUpDown();
		this.label7 = new System.Windows.Forms.Label();
		this.CBBranch = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtAddress = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtPhone = new System.Windows.Forms.TextBox();
		this.TxtName = new DevExpress.XtraEditors.ButtonEdit();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.gridEX2 = new Janus.Windows.GridEX.GridEX();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Txtprofit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX2).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.CBAllowances);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.Txtprofit);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.CBBranch);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.TxtAddress);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.TxtPhone);
		this.groupBox1.Controls.Add(this.TxtName);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.CBAllowances, "CBAllowances");
		this.CBAllowances.FormattingEnabled = true;
		this.CBAllowances.Name = "CBAllowances";
		this.CBAllowances.SelectedIndexChanged += new System.EventHandler(CBAllowances_SelectedIndexChanged);
		this.CBAllowances.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CBAllowances_KeyPress);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.Txtprofit, "Txtprofit");
		this.Txtprofit.DecimalPlaces = 2;
		this.Txtprofit.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.Txtprofit.Maximum = new decimal(new int[4] { 99999999, 0, 0, 0 });
		this.Txtprofit.Name = "Txtprofit";
		this.Txtprofit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.CBBranch, "CBBranch");
		this.CBBranch.FormattingEnabled = true;
		this.CBBranch.Name = "CBBranch";
		this.CBBranch.SelectedIndexChanged += new System.EventHandler(CBBranch_SelectedIndexChanged);
		this.CBBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
		this.CBBranch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Comb_KeyPress);
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.TxtAddress, "TxtAddress");
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.TxtPhone, "TxtPhone");
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtName, "TxtName");
		this.TxtName.Name = "TxtName";
		this.TxtName.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtName.Properties.Appearance.Font");
		this.TxtName.Properties.Appearance.Options.UseFont = true;
		this.TxtName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions2");
		this.TxtName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtName.Properties.Buttons"), resources.GetString("TxtName.Properties.Buttons1"), (int)resources.GetObject("TxtName.Properties.Buttons2"), (bool)resources.GetObject("TxtName.Properties.Buttons3"), (bool)resources.GetObject("TxtName.Properties.Buttons4"), (bool)resources.GetObject("TxtName.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtName.Properties.Buttons6"), resources.GetObject("TxtName.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtName.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtName.Properties.Buttons9"))
		});
		this.TxtName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtName_ButtonClick);
		this.TxtName.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		resources.ApplyResources(this.gridEX2, "gridEX2");
		this.gridEX2.AllowCardSizing = false;
		this.gridEX2.AllowColumnDrag = false;
		this.gridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX2.AlternatingColors = true;
		this.gridEX2.GroupByBoxVisible = false;
		this.gridEX2.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX2.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX2.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX2.Name = "gridEX2";
		this.gridEX2.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX2.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX2.MouseClick += new System.Windows.Forms.MouseEventHandler(gridEX2_MouseClick);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.gridEX2);
		base.Controls.Add(this.gridEX1);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmEmployee";
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.Txtprofit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX2).EndInit();
		base.ResumeLayout(false);
	}
}


