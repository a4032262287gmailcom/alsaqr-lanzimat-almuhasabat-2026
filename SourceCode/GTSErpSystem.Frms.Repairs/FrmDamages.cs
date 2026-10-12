using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Repairs;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Repairs;

public class FrmDamages : XtraForm
{
	private Class_Damages Malfunctionsrs = new Class_Damages();

	private Class_RepairsDepartment Repairs = new Class_RepairsDepartment();

	private int id;

	private int DepartmentId;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label4;

	private ButtonEdit TxtName;

	private Label label2;

	private SimpleButton BtnExit;

	private SimpleButton BtnDelete;

	private GridEX gridEX1;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private System.Windows.Forms.ComboBox ComDepartment;

	private Label label3;

	public FrmDamages()
	{
		InitializeComponent();
		Clear();
	}

	private void Clear()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BindGrid();
		TxtName.Text = "";
		TxtName.Focus();
		ComDepartment.DataSource = Repairs.GetAll();
		ComDepartment.ValueMember = "Id";
		ComDepartment.DisplayMember = "GroupName";
		DepartmentId = Convert.ToInt32(ComDepartment.SelectedValue);
	}

	public void BindGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("كود ", typeof(int));
			dataTable.Columns.Add("المجموعة", typeof(string));
			dataTable.Columns.Add("القسم", typeof(string));
			foreach (Repairs_Malfunctions item in from x in Malfunctionsrs.GetAll()
				where x.Id > 0
				select x)
			{
				string text = Repairs.GetAll().FirstOrDefault((Repairs_Groups g) => g.Id == item.GroupId)?.GroupName ?? "";
				dataTable.Rows.Add(item.Id, item.Name, text);
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["كود "].Width = 20;
			gridEX1.RootTable.Columns["كود "].RightToLeft = RightToLeft.No;
			return;
		}
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable2 = new DataTable("gridEX1");
		dataTable2.Columns.Add("Code", typeof(int));
		dataTable2.Columns.Add(" Name", typeof(string));
		dataTable2.Columns.Add("Group Name", typeof(string));
		foreach (Repairs_Malfunctions item2 in from x in Malfunctionsrs.GetAll()
			where x.Id > 0
			select x)
		{
			string text2 = Repairs.GetAll().FirstOrDefault((Repairs_Groups g) => g.Id == item2.GroupId)?.GroupName ?? "";
			dataTable2.Rows.Add(item2.Id, item2.Name, text2);
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["Code"].Width = 20;
		gridEX1.RootTable.Columns["Code"].RightToLeft = RightToLeft.No;
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		Clear();
	}

	private void TxtName_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		BtnNew_Click(null, null);
	}

	private void gridEX1_MouseClick(object sender, MouseEventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		id = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
		Repairs_Malfunctions repairs_Malfunctions = (from x in Malfunctionsrs.GetAll()
			where x.Id == id
			select x).FirstOrDefault();
		TxtName.Text = repairs_Malfunctions.Name;
		ComDepartment.SelectedValue = repairs_Malfunctions.GroupId;
		TxtName.Focus();
		BtnSave.Enabled = false;
		BtnEdit.Enabled = true;
		BtnDelete.Enabled = true;
	}

	private void Save()
	{
		int num = 0;
		if (TxtName.Text != "")
		{
			switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtName.Text + " ) الي التلفيات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
			case DialogResult.Yes:
				num = Malfunctionsrs.Insert(TxtName.Text, DepartmentId);
				if (num > 0)
				{
					MessageBox.Show("تم حفظ التلفيات ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					BtnSave.Enabled = true;
					BtnEdit.Enabled = false;
					BtnDelete.Enabled = false;
					BindGrid();
					Clear();
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات التلفيات بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				break;
			case DialogResult.No:
				TxtName.Focus();
				break;
			}
		}
		else
		{
			MessageBox.Show("عفوا\u064b من فضلك ادخل اسم مجموعة الصنف", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void Edit()
	{
		int num = 0;
		if (TxtName.Text != "")
		{
			switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + TxtName.Text + " ) الي الاقسام", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
			case DialogResult.Yes:
				num = Malfunctionsrs.Update(Convert.ToInt32(id), TxtName.Text, DepartmentId);
				if (num > 0)
				{
					MessageBox.Show("تم حفظ بيانات التلفيات ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					BtnSave.Enabled = true;
					BtnEdit.Enabled = false;
					BtnDelete.Enabled = false;
					BindGrid();
					Clear();
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات التلفيات بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				break;
			case DialogResult.No:
				TxtName.Focus();
				break;
			}
		}
		else
		{
			MessageBox.Show("عفوا\u064b من فضلك ادخل اسم القسم", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void Delete()
	{
		int num = 0;
		if (TxtName.Text != "")
		{
			switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtName.Text + " ) من التلفيات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
			case DialogResult.Yes:
				num = Malfunctionsrs.Delete(TxtName.Text);
				if (num > 0)
				{
					BindGrid();
					MessageBox.Show("تم حذف بيانات التلفيات ( " + TxtName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					BtnSave.Enabled = true;
					BtnEdit.Enabled = false;
					BtnDelete.Enabled = false;
					Clear();
				}
				break;
			case DialogResult.No:
				TxtName.Focus();
				break;
			}
		}
		else
		{
			MessageBox.Show("عفوا\u064b من فضلك ادخل اسم القسم", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void ComDepartment_SelectedValueChanged(object sender, EventArgs e)
	{
		if (ComDepartment.SelectedValue != null)
		{
			if (int.TryParse(ComDepartment.SelectedValue.ToString(), out var result))
			{
				DepartmentId = result;
			}
			else
			{
				DepartmentId = 0;
			}
		}
		else
		{
			DepartmentId = 0;
		}
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Repairs.FrmDamages));
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ComDepartment = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtName = new DevExpress.XtraEditors.ButtonEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.ComDepartment);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.TxtName);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Location = new System.Drawing.Point(8, -7);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Size = new System.Drawing.Size(639, 91);
		this.groupBox1.TabIndex = 59;
		this.groupBox1.TabStop = false;
		this.ComDepartment.Font = new System.Drawing.Font("Tahoma", 8f);
		this.ComDepartment.FormattingEnabled = true;
		this.ComDepartment.Location = new System.Drawing.Point(15, 50);
		this.ComDepartment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.ComDepartment.Name = "ComDepartment";
		this.ComDepartment.Size = new System.Drawing.Size(563, 24);
		this.ComDepartment.TabIndex = 8;
		this.ComDepartment.SelectedValueChanged += new System.EventHandler(ComDepartment_SelectedValueChanged);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(584, 53);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(47, 17);
		this.label3.TabIndex = 9;
		this.label3.Text = "القسم";
		this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label4.Location = new System.Drawing.Point(15, 128);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(100, 23);
		this.label4.TabIndex = 7;
		this.TxtName.Location = new System.Drawing.Point(15, 15);
		this.TxtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtName.Name = "TxtName";
		this.TxtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtName.Properties.Appearance.Options.UseFont = true;
		this.TxtName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions1.Image");
		this.TxtName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.TxtName.Size = new System.Drawing.Size(563, 28);
		this.TxtName.TabIndex = 0;
		this.TxtName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtName_ButtonClick);
		this.label2.AutoSize = true;
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(586, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(40, 16);
		this.label2.TabIndex = 3;
		this.label2.Text = "الاسم";
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(546, 295);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(5);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(101, 44);
		this.BtnExit.TabIndex = 58;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(417, 295);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(5);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(105, 44);
		this.BtnDelete.TabIndex = 56;
		this.BtnDelete.Text = "حذف F7";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.gridEX1.Font = new System.Drawing.Font("Tahoma", 9f);
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Location = new System.Drawing.Point(8, 88);
		this.gridEX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.Size = new System.Drawing.Size(639, 200);
		this.gridEX1.TabIndex = 60;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX1.MouseClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseClick);
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(279, 295);
		this.BtnEdit.Margin = new System.Windows.Forms.Padding(5);
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Size = new System.Drawing.Size(107, 44);
		this.BtnEdit.TabIndex = 55;
		this.BtnEdit.Text = "تعديل F6";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(142, 295);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(5);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(107, 44);
		this.BtnSave.TabIndex = 54;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(8, 295);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(5);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(108, 44);
		this.BtnNew.TabIndex = 57;
		this.BtnNew.Text = "جديد F4";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(651, 351);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.gridEX1);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmDamages";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "التلفيات";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		base.ResumeLayout(false);
	}
}


