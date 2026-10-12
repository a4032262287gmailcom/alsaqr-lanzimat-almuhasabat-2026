using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Repairs;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Repairs;

public class FrmRepairs : XtraForm
{
	private Class_RepairsDepartment Repairs = new Class_RepairsDepartment();

	private int id;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label4;

	private ButtonEdit TxtName;

	private Label label2;

	private SimpleButton BtnExit;

	private SimpleButton BtnDelete;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private GridEX gridEX1;

	public FrmRepairs()
	{
		InitializeComponent();
		BindGrid();
		Clear();
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		Clear();
	}

	private void TxtName_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		BtnNew_Click(null, null);
	}

	private void Clear()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BindGrid();
		TxtName.Text = "";
		TxtName.Focus();
	}

	private void Save()
	{
		int num = 0;
		if (TxtName.Text != "")
		{
			switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtName.Text + " ) الي مجموعة الاصناف", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
			case DialogResult.Yes:
				num = Repairs.Insert(TxtName.Text);
				if (num > 0)
				{
					MessageBox.Show("تم حفظ القسم ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					BtnSave.Enabled = true;
					BtnEdit.Enabled = false;
					BtnDelete.Enabled = false;
					BindGrid();
					Clear();
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات مجموعة الصنف بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
				num = Repairs.Update(Convert.ToInt32(id), TxtName.Text);
				if (num > 0)
				{
					MessageBox.Show("تم حفظ بيانات القسم ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					BtnSave.Enabled = true;
					BtnEdit.Enabled = false;
					BtnDelete.Enabled = false;
					BindGrid();
					Clear();
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات القسم بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
			switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtName.Text + " ) من مجموعة الاصناف", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
			case DialogResult.Yes:
				num = Repairs.Delete(TxtName.Text);
				if (num > 0)
				{
					BindGrid();
					MessageBox.Show("تم حذف بيانات القسم ( " + TxtName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

	public string ImageToBase64(Image image, ImageFormat format)
	{
		using MemoryStream memoryStream = new MemoryStream();
		image.Save(memoryStream, format);
		byte[] inArray = memoryStream.ToArray();
		return Convert.ToBase64String(inArray);
	}

	public Image Base64ToImage(string base64String)
	{
		byte[] array = Convert.FromBase64String(base64String);
		MemoryStream memoryStream = new MemoryStream(array, 0, array.Length);
		memoryStream.Write(array, 0, array.Length);
		return Image.FromStream(memoryStream, useEmbeddedColorManagement: true);
	}

	public void BindGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("كود ", typeof(int));
			dataTable.Columns.Add("المجموعة", typeof(string));
			foreach (Repairs_Groups item in from x in Repairs.GetAll()
				where x.Id > 0
				select x)
			{
				dataTable.Rows.Add(item.Id, item.GroupName);
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
		dataTable2.Columns.Add("Group Name", typeof(string));
		foreach (Repairs_Groups item2 in from x in Repairs.GetAll()
			where x.Id > 0
			select x)
		{
			dataTable2.Rows.Add(item2.Id, item2.GroupName);
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["Code"].Width = 20;
		gridEX1.RootTable.Columns["Code"].RightToLeft = RightToLeft.No;
	}

	private void gridEX1_MouseClick(object sender, MouseEventArgs e)
	{
		int position = gridEX1.SelectedItems[0].Position;
		id = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
		Repairs_Groups repairs_Groups = (from x in Repairs.GetAll()
			where x.Id == id
			select x).FirstOrDefault();
		TxtName.Text = repairs_Groups.GroupName;
		TxtName.Focus();
		BtnSave.Enabled = false;
		BtnEdit.Enabled = true;
		BtnDelete.Enabled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Repairs.FrmRepairs));
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtName = new DevExpress.XtraEditors.ButtonEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.TxtName);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Location = new System.Drawing.Point(12, 11);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Size = new System.Drawing.Size(533, 57);
		this.groupBox1.TabIndex = 52;
		this.groupBox1.TabStop = false;
		this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label4.Location = new System.Drawing.Point(15, 128);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(100, 23);
		this.label4.TabIndex = 7;
		this.TxtName.Location = new System.Drawing.Point(15, 17);
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
		this.TxtName.Size = new System.Drawing.Size(461, 28);
		this.TxtName.TabIndex = 0;
		this.TxtName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtName_ButtonClick);
		this.label2.AutoSize = true;
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(487, 24);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(40, 16);
		this.label2.TabIndex = 3;
		this.label2.Text = "الاسم";
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(444, 265);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(5);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(101, 44);
		this.BtnExit.TabIndex = 51;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(336, 265);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(5);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(100, 44);
		this.BtnDelete.TabIndex = 49;
		this.BtnDelete.Text = "حذف F7";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(228, 265);
		this.BtnEdit.Margin = new System.Windows.Forms.Padding(5);
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Size = new System.Drawing.Size(100, 44);
		this.BtnEdit.TabIndex = 48;
		this.BtnEdit.Text = "تعديل F6";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(120, 265);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(5);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(100, 44);
		this.BtnSave.TabIndex = 47;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(10, 265);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(5);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(101, 44);
		this.BtnNew.TabIndex = 50;
		this.BtnNew.Text = "جديد F4";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
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
		this.gridEX1.Location = new System.Drawing.Point(12, 72);
		this.gridEX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.Size = new System.Drawing.Size(535, 186);
		this.gridEX1.TabIndex = 53;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX1.MouseClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(559, 323);
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
		base.Name = "FrmRepairs";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "الاقسام";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		base.ResumeLayout(false);
	}
}


