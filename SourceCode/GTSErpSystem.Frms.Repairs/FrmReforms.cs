using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Repairs;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Properties;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Repairs;

public class FrmReforms : XtraForm
{
	private Class_Damages Malfunctionsrs = new Class_Damages();

	private Class_RepairsDepartment Repairs = new Class_RepairsDepartment();

	private Class_Customer Cust = new Class_Customer();

	private Class_Reforms Reforms = new Class_Reforms();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private SimpleButton BtnNewCustomer;

	private ButtonEdit TxtSuppliers;

	private Label label43;

	private TextEdit TxtPhone;

	private GroupBox groupBox2;

	private Label label4;

	private TextEdit TxtModel;

	private Label label3;

	private TextEdit TxtColor;

	private Label label2;

	private TextEdit TxtPainting;

	private Label label6;

	private TextEdit TxtNote;

	private Label label5;

	private TextEdit TxtType;

	private TableLayoutPanel tableLayoutPanelGroups;

	private SimpleButton BtnExit;

	private SimpleButton BtnDelete;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private Label label7;

	private ButtonEdit BtnReforams;

	private GroupBox groupBox3;

	private SimpleButton BtnPrint;

	public FrmReforms()
	{
		InitializeComponent();
	}

	private void SearchCustNum(int ID, int codeCust = 0)
	{
		try
		{
			Account_CustSup account_CustSup = null;
			if (codeCust > 0)
			{
				account_CustSup = (from x in Cust.GetAll()
					where x.ID == ID && x.IsCustomers == true
					select x).FirstOrDefault();
				if (account_CustSup == null)
				{
					return;
				}
				ID = account_CustSup.ID;
			}
			if (ID > 0)
			{
				account_CustSup = (from x in Cust.GetAll()
					where x.ID == ID && x.IsCustomers == true
					select x).FirstOrDefault();
				if (account_CustSup != null)
				{
					TxtSuppliers.Text = account_CustSup.CustSuppName;
					TxtPhone.Text = account_CustSup.Phone;
				}
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void Clear()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtSuppliers.Text = "";
		TxtColor.Text = "";
		TxtPhone.Text = "";
		TxtType.Text = "";
		TxtModel.Text = "";
		TxtPainting.Text = "";
		TxtNote.Text = "";
		BtnReforams.Text = "";
		TxtSuppliers.Focus();
		foreach (GroupBox control2 in tableLayoutPanelGroups.Controls)
		{
			foreach (Control control3 in control2.Controls)
			{
				if (!(control3 is FlowLayoutPanel flowLayoutPanel))
				{
					continue;
				}
				foreach (CheckBox control4 in flowLayoutPanel.Controls)
				{
					control4.Checked = false;
				}
			}
		}
	}

	private void LoadRepair(int id)
	{
		Repairs_Repair byId = Reforms.GetById(id);
		if (byId == null)
		{
			MessageBox.Show("الاصلاح غير موجودة");
			return;
		}
		BtnReforams.Text = byId.Id.ToString();
		TxtSuppliers.Text = byId.CustomerName;
		TxtPhone.Text = byId.PhoneNumber;
		TxtPainting.Text = byId.PlateNumber;
		TxtModel.Text = byId.CarModel;
		TxtType.Text = byId.CarType;
		TxtColor.Text = byId.CarColor;
		TxtNote.Text = byId.Notes;
		List<int> list = byId.Repairs_RepairDetails.Select((Repairs_RepairDetails d) => d.MalfunctionId).ToList();
		foreach (GroupBox control2 in tableLayoutPanelGroups.Controls)
		{
			foreach (Control control3 in control2.Controls)
			{
				if (!(control3 is FlowLayoutPanel flowLayoutPanel))
				{
					continue;
				}
				foreach (CheckBox control4 in flowLayoutPanel.Controls)
				{
					int item = Convert.ToInt32(control4.Tag);
					control4.Checked = list.Contains(item);
				}
			}
		}
	}

	private void Save()
	{
		int num = 0;
		if (TxtSuppliers.Text != "")
		{
			List<Class_Reforms.RepairDetailVM> list = new List<Class_Reforms.RepairDetailVM>();
			foreach (GroupBox control2 in tableLayoutPanelGroups.Controls)
			{
				int groupId = Convert.ToInt32(control2.Tag);
				foreach (Control control3 in control2.Controls)
				{
					if (!(control3 is FlowLayoutPanel flowLayoutPanel))
					{
						continue;
					}
					foreach (CheckBox control4 in flowLayoutPanel.Controls)
					{
						if (control4.Checked)
						{
							int malfunctionId = Convert.ToInt32(control4.Tag);
							list.Add(new Class_Reforms.RepairDetailVM
							{
								GroupId = groupId,
								MalfunctionId = malfunctionId
							});
						}
					}
				}
			}
			if (list.Count == 0)
			{
				MessageBox.Show("عفوا\u064b من فضلك اختر الأعطال قبل الحفظ", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			num = Reforms.Insert(TxtSuppliers.Text, TxtPhone.Text, TxtPainting.Text, TxtModel.Text, TxtType.Text, TxtColor.Text, TxtNote.Text, list);
			if (num > 0)
			{
				MessageBox.Show("تم حفظ الاصلاحات بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnDelete.Enabled = false;
				Clear();
			}
			else
			{
				MessageBox.Show("حدث خطأ أثناء الحفظ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا\u064b من فضلك ادخل اسم العميل", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private new void Update()
	{
		if (TxtSuppliers.Text != "")
		{
			DialogResult dialogResult = MessageBox.Show("هل أنت متأكد من تعديل البيانات؟", "تأكيد التعديل", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult != DialogResult.Yes)
			{
				return;
			}
			List<Class_Reforms.RepairDetailVM> list = new List<Class_Reforms.RepairDetailVM>();
			foreach (GroupBox control2 in tableLayoutPanelGroups.Controls)
			{
				int groupId = Convert.ToInt32(control2.Tag);
				foreach (Control control3 in control2.Controls)
				{
					if (!(control3 is FlowLayoutPanel flowLayoutPanel))
					{
						continue;
					}
					foreach (CheckBox control4 in flowLayoutPanel.Controls)
					{
						if (control4.Checked)
						{
							int malfunctionId = Convert.ToInt32(control4.Tag);
							list.Add(new Class_Reforms.RepairDetailVM
							{
								GroupId = groupId,
								MalfunctionId = malfunctionId
							});
						}
					}
				}
			}
			if (list.Count == 0)
			{
				MessageBox.Show("عفوا\u064b من فضلك اختر الأعطال قبل التعديل", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			int num = Reforms.Update(Convert.ToInt32(BtnReforams.Text), TxtSuppliers.Text, TxtPhone.Text, TxtPainting.Text, TxtModel.Text, TxtType.Text, TxtColor.Text, TxtNote.Text, list);
			if (num > 0)
			{
				MessageBox.Show("تم تعديل الإصلاحات بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnDelete.Enabled = false;
				Clear();
			}
			else
			{
				MessageBox.Show("حدث خطأ أثناء التعديل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا\u064b من فضلك ادخل اسم العميل", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void Delete()
	{
		if (string.IsNullOrWhiteSpace(BtnReforams.Text) || !int.TryParse(BtnReforams.Text, out var result))
		{
			MessageBox.Show("يرجى تحديد إصلاح لحذفه", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		DialogResult dialogResult = MessageBox.Show("هل أنت متأكد من حذف السجل؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult == DialogResult.Yes)
		{
			int num = Reforms.Delete(result);
			if (num > 0)
			{
				MessageBox.Show("تم حذف السجل بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				BtnSave.Enabled = true;
				BtnEdit.Enabled = false;
				BtnDelete.Enabled = false;
				Clear();
			}
			else
			{
				MessageBox.Show("حدث خطأ أثناء الحذف", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void Print()
	{
		if (BtnReforams.Text != string.Empty)
		{
			SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["Repair"].Clear();
			SqlCommand sqlCommand = new SqlCommand("GetRepairWithDetails", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@RepairId", Convert.ToInt32(BtnReforams.Text));
			sqlDataAdapter.Fill(dataSetALL.Repair);
			sqlConnection.Close();
			if (dataSetALL.Tables["Repair"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintRepair.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintRepair.mrt");
				}
				stiReport.RegData(dataSetALL.Repair);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				stiReport["Type"] = TxtType.Text;
				stiReport["CustName"] = TxtSuppliers.Text;
				stiReport["Custphone"] = TxtPhone.Text;
				stiReport.Render();
				stiReport.Show(dialogForm: true);
			}
			else
			{
				MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void BtnNewCustomer_Click(object sender, EventArgs e)
	{
		new FrmCustomer().Show();
	}

	private void FrmReforms_Load(object sender, EventArgs e)
	{
		List<Repairs_Groups> all = Repairs.GetAll();
		List<Repairs_Malfunctions> all2 = Malfunctionsrs.GetAll();
		tableLayoutPanelGroups.Controls.Clear();
		tableLayoutPanelGroups.ColumnStyles.Clear();
		tableLayoutPanelGroups.ColumnCount = all.Count;
		tableLayoutPanelGroups.RowCount = 1;
		tableLayoutPanelGroups.RowStyles.Clear();
		tableLayoutPanelGroups.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
		for (int i = 0; i < all.Count; i++)
		{
			tableLayoutPanelGroups.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / (float)all.Count));
		}
		int num = 0;
		foreach (Repairs_Groups group in all)
		{
			GroupBox groupBox = new GroupBox();
			groupBox.Text = group.GroupName;
			groupBox.Tag = group.Id;
			groupBox.Dock = DockStyle.Fill;
			groupBox.RightToLeft = RightToLeft.Yes;
			groupBox.Padding = new Padding(10);
			FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
			flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
			flowLayoutPanel.Dock = DockStyle.Fill;
			flowLayoutPanel.Padding = new Padding(0, 18, 0, 0);
			flowLayoutPanel.AutoSize = true;
			List<Repairs_Malfunctions> list = all2.Where((Repairs_Malfunctions x) => x.GroupId == group.Id).ToList();
			foreach (Repairs_Malfunctions item in list)
			{
				CheckBox checkBox = new CheckBox();
				checkBox.Text = item.Name;
				checkBox.Tag = item.Id;
				checkBox.RightToLeft = RightToLeft.Yes;
				flowLayoutPanel.Controls.Add(checkBox);
			}
			groupBox.Controls.Add(flowLayoutPanel);
			tableLayoutPanelGroups.Controls.Add(groupBox, num, 0);
			num++;
		}
	}

	private void TxtSuppliers_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		using (new GTSdbEntities())
		{
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustID > 0)
			{
				SearchCustNum(frmSearchCustomer.CustID);
			}
		}
	}

	private void BtnReforams_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchRepairs frmSearchRepairs = new FrmSearchRepairs();
		frmSearchRepairs.ShowDialog();
		if (frmSearchRepairs.CustCode > 0)
		{
			LoadRepair(frmSearchRepairs.CustCode);
		}
		BtnSave.Enabled = false;
		BtnEdit.Enabled = true;
		BtnDelete.Enabled = true;
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		Clear();
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		Update();
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Repairs.FrmReforms));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.BtnReforams = new DevExpress.XtraEditors.ButtonEdit();
		this.label43 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtPhone = new DevExpress.XtraEditors.TextEdit();
		this.BtnNewCustomer = new DevExpress.XtraEditors.SimpleButton();
		this.TxtSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtType = new DevExpress.XtraEditors.TextEdit();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtModel = new DevExpress.XtraEditors.TextEdit();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtColor = new DevExpress.XtraEditors.TextEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtPainting = new DevExpress.XtraEditors.TextEdit();
		this.tableLayoutPanelGroups = new System.Windows.Forms.TableLayoutPanel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnReforams.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtType.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtModel.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtColor.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPainting.Properties).BeginInit();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.BtnReforams);
		this.groupBox1.Controls.Add(this.label43);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtPhone);
		this.groupBox1.Controls.Add(this.BtnNewCustomer);
		this.groupBox1.Controls.Add(this.TxtSuppliers);
		this.groupBox1.Location = new System.Drawing.Point(3, 1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1160, 116);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "بيانات العميل";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(477, 71);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(73, 21);
		this.label6.TabIndex = 126;
		this.label6.Text = "ملاحظات";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(1070, 29);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(79, 21);
		this.label7.TabIndex = 118;
		this.label7.Text = "الاصلاحات";
		this.TxtNote.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNote.Location = new System.Drawing.Point(8, 71);
		this.TxtNote.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNote.Size = new System.Drawing.Size(461, 28);
		this.TxtNote.TabIndex = 125;
		this.BtnReforams.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnReforams.Location = new System.Drawing.Point(613, 26);
		this.BtnReforams.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.BtnReforams.Name = "BtnReforams";
		this.BtnReforams.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnReforams.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnReforams.Properties.Appearance.Options.UseBackColor = true;
		this.BtnReforams.Properties.Appearance.Options.UseFont = true;
		this.BtnReforams.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnReforams.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnReforams.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.BtnReforams.Size = new System.Drawing.Size(437, 28);
		this.BtnReforams.TabIndex = 119;
		this.BtnReforams.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnReforams_ButtonClick);
		this.label43.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label43.AutoSize = true;
		this.label43.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label43.Location = new System.Drawing.Point(477, 29);
		this.label43.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label43.Name = "label43";
		this.label43.Size = new System.Drawing.Size(84, 21);
		this.label43.TabIndex = 117;
		this.label43.Text = "رقم الجوال";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(1051, 71);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(98, 21);
		this.label1.TabIndex = 0;
		this.label1.Text = "اسم العميل ";
		this.TxtPhone.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtPhone.Location = new System.Drawing.Point(8, 26);
		this.TxtPhone.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPhone.Properties.Appearance.Options.UseFont = true;
		this.TxtPhone.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPhone.Size = new System.Drawing.Size(461, 28);
		this.TxtPhone.TabIndex = 116;
		this.BtnNewCustomer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNewCustomer.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNewCustomer.Appearance.Options.UseFont = true;
		this.BtnNewCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewCustomer.ImageOptions.Image");
		this.BtnNewCustomer.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewCustomer.Location = new System.Drawing.Point(613, 68);
		this.BtnNewCustomer.Margin = new System.Windows.Forms.Padding(0);
		this.BtnNewCustomer.Name = "BtnNewCustomer";
		this.BtnNewCustomer.Size = new System.Drawing.Size(31, 27);
		this.BtnNewCustomer.TabIndex = 4;
		this.BtnNewCustomer.Click += new System.EventHandler(BtnNewCustomer_Click);
		this.TxtSuppliers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSuppliers.Location = new System.Drawing.Point(648, 68);
		this.TxtSuppliers.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtSuppliers.Name = "TxtSuppliers";
		this.TxtSuppliers.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtSuppliers.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSuppliers.Properties.Appearance.Options.UseBackColor = true;
		this.TxtSuppliers.Properties.Appearance.Options.UseFont = true;
		this.TxtSuppliers.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSuppliers.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSuppliers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtSuppliers.Size = new System.Drawing.Size(402, 28);
		this.TxtSuppliers.TabIndex = 5;
		this.TxtSuppliers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtSuppliers_ButtonClick);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.TxtType);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.TxtModel);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtColor);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.TxtPainting);
		this.groupBox2.Location = new System.Drawing.Point(3, 123);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1160, 116);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "بيانات السياره";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(1068, 75);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(43, 21);
		this.label5.TabIndex = 124;
		this.label5.Text = "النوع";
		this.TxtType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtType.Location = new System.Drawing.Point(613, 72);
		this.TxtType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtType.Name = "TxtType";
		this.TxtType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtType.Properties.Appearance.Options.UseFont = true;
		this.TxtType.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtType.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtType.Size = new System.Drawing.Size(434, 28);
		this.TxtType.TabIndex = 123;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label4.Location = new System.Drawing.Point(477, 27);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(62, 21);
		this.label4.TabIndex = 122;
		this.label4.Text = "الموديل";
		this.TxtModel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtModel.Location = new System.Drawing.Point(7, 24);
		this.TxtModel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtModel.Name = "TxtModel";
		this.TxtModel.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtModel.Properties.Appearance.Options.UseFont = true;
		this.TxtModel.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtModel.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtModel.Size = new System.Drawing.Size(462, 28);
		this.TxtModel.TabIndex = 121;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(477, 75);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(44, 21);
		this.label3.TabIndex = 120;
		this.label3.Text = "اللون";
		this.TxtColor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtColor.Location = new System.Drawing.Point(7, 72);
		this.TxtColor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtColor.Name = "TxtColor";
		this.TxtColor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtColor.Properties.Appearance.Options.UseFont = true;
		this.TxtColor.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtColor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtColor.Size = new System.Drawing.Size(462, 28);
		this.TxtColor.TabIndex = 119;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(1068, 27);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(84, 21);
		this.label2.TabIndex = 118;
		this.label2.Text = "رقم اللوحه";
		this.TxtPainting.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtPainting.Location = new System.Drawing.Point(613, 24);
		this.TxtPainting.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPainting.Name = "TxtPainting";
		this.TxtPainting.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtPainting.Properties.Appearance.Options.UseFont = true;
		this.TxtPainting.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPainting.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPainting.Size = new System.Drawing.Size(434, 28);
		this.TxtPainting.TabIndex = 117;
		this.tableLayoutPanelGroups.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tableLayoutPanelGroups.ColumnCount = 2;
		this.tableLayoutPanelGroups.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.tableLayoutPanelGroups.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.tableLayoutPanelGroups.Location = new System.Drawing.Point(3, 245);
		this.tableLayoutPanelGroups.Name = "tableLayoutPanelGroups";
		this.tableLayoutPanelGroups.RowCount = 2;
		this.tableLayoutPanelGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.tableLayoutPanelGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.tableLayoutPanelGroups.Size = new System.Drawing.Size(1160, 246);
		this.tableLayoutPanelGroups.TabIndex = 3;
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.BtnPrint);
		this.groupBox3.Controls.Add(this.BtnNew);
		this.groupBox3.Controls.Add(this.BtnExit);
		this.groupBox3.Controls.Add(this.BtnSave);
		this.groupBox3.Controls.Add(this.BtnDelete);
		this.groupBox3.Controls.Add(this.BtnEdit);
		this.groupBox3.Location = new System.Drawing.Point(3, 489);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1160, 80);
		this.groupBox3.TabIndex = 64;
		this.groupBox3.TabStop = false;
		this.BtnPrint.ImageOptions.Image = GTSErpSystem.Properties.Resources.printer_16x16;
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(390, 24);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(5);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(186, 44);
		this.BtnPrint.TabIndex = 64;
		this.BtnPrint.Text = "طباعه";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(971, 24);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(5);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(186, 44);
		this.BtnNew.TabIndex = 62;
		this.BtnNew.Text = "جديد ";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(8, 24);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(5);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(186, 44);
		this.BtnExit.TabIndex = 63;
		this.BtnExit.Text = "خروج ";
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(778, 24);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(5);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(186, 44);
		this.BtnSave.TabIndex = 59;
		this.BtnSave.Text = "حفظ ";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(197, 24);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(5);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(186, 44);
		this.BtnDelete.TabIndex = 61;
		this.BtnDelete.Text = "حذف ";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnEdit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(584, 24);
		this.BtnEdit.Margin = new System.Windows.Forms.Padding(5);
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Size = new System.Drawing.Size(186, 44);
		this.BtnEdit.TabIndex = 60;
		this.BtnEdit.Text = "تعديل";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		base.ActiveGlowColor = System.Drawing.Color.WhiteSmoke;
		base.Appearance.BackColor = System.Drawing.Color.White;
		base.Appearance.Options.UseBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1168, 648);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.tableLayoutPanelGroups);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmReforms";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "الاصلاحات";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(FrmReforms_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnReforams.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPhone.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSuppliers.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtType.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtModel.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtColor.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPainting.Properties).EndInit();
		this.groupBox3.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


