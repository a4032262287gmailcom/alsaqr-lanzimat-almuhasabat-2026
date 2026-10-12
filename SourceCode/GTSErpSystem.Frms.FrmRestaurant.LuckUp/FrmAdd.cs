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
using GTSErpSystem.BLL.Restaurant;
using GTSErpSystem.BLL.Security;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.FrmRestaurant.LuckUp;

public class FrmAdd : XtraForm
{
	private Class_Add UNT = new Class_Add();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	public int GrpID;

	private IContainer components = null;

	private SimpleButton BtnExit;

	private SimpleButton BtnDelete;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private Label label2;

	private GroupBox groupBox1;

	private ButtonEdit TxtNameAr;

	private GridEX gridEX1;

	private ButtonEdit TxtNameEn;

	private Label label1;

	private NumericUpDown TxtCostPrice;

	private Label label19;

	private NumericUpDown TxtSellPrice;

	private Label label20;

	private System.Windows.Forms.ComboBox comboPrint;

	private Label label3;

	private ToggleSwitch toggleAdd;

	private CheckBox CHVat;

	private Label label49;

	private NumericUpDown TxtVat;

	public FrmAdd()
	{
		InitializeComponent();
		Clear();
		BindGrid();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		GetPrinters();
		TxtNameAr.Focus();
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
			TxtNameAr.Text = gridEX1.GetRow(position).Cells[1].Text;
			TxtNameEn.Text = gridEX1.GetRow(position).Cells[2].Text;
			TxtSellPrice.Value = Convert.ToDecimal(gridEX1.GetRow(position).Cells[3].Text);
			TxtCostPrice.Value = Convert.ToDecimal(gridEX1.GetRow(position).Cells[4].Text);
			comboPrint.SelectedValue = Convert.ToInt32(gridEX1.GetRow(position).Cells[5].Text);
			toggleAdd.IsOn = Convert.ToBoolean(gridEX1.GetRow(position).Cells[6].Text);
			CHVat.Checked = Convert.ToBoolean(gridEX1.GetRow(position).Cells[7].Text);
			TxtVat.Value = Convert.ToDecimal(gridEX1.GetRow(position).Cells[8].Text);
			TxtNameAr.Focus();
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

	private void BindGrid()
	{
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable = new DataTable("gridEX1");
		dataTable.Columns.Add("كود", typeof(int));
		dataTable.Columns.Add("اسم العربي", typeof(string));
		dataTable.Columns.Add("اسم الانجليزي", typeof(string));
		dataTable.Columns.Add("سعر البيع", typeof(string));
		dataTable.Columns.Add("سعر الشراء", typeof(string));
		dataTable.Columns.Add("comboPrint", typeof(int));
		dataTable.Columns.Add("toggleAdd", typeof(bool));
		dataTable.Columns.Add("CHVat", typeof(bool));
		dataTable.Columns.Add("TxtVat", typeof(string));
		foreach (Item_Add item in from x in UNT.GetAll()
			where x.ID > 0
			select x)
		{
			dataTable.Rows.Add(item.ID, item.Name, item.NameEN, item.SellPrice, item.CostPrice, item.PrintID, item.Type, item.Is_Tax, item.Tax_Value);
		}
		gridEX1.SetDataBinding(dataTable, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["كود"].Width = 30;
		gridEX1.RootTable.Columns["كود"].RightToLeft = RightToLeft.No;
		gridEX1.RootTable.Columns["كود"].TextAlignment = TextAlignment.Center;
		gridEX1.RootTable.Columns["اسم العربي"].TextAlignment = TextAlignment.Center;
		gridEX1.RootTable.Columns["سعر البيع"].TextAlignment = TextAlignment.Center;
		gridEX1.RootTable.Columns["سعر الشراء"].TextAlignment = TextAlignment.Center;
		gridEX1.RootTable.Columns["اسم الانجليزي"].Visible = false;
		gridEX1.RootTable.Columns["comboPrint"].Visible = false;
		gridEX1.RootTable.Columns["toggleAdd"].Visible = false;
		gridEX1.RootTable.Columns["CHVat"].Visible = false;
		gridEX1.RootTable.Columns["TxtVat"].Visible = false;
	}

	private void Clear()
	{
		TxtNameAr.Text = "";
		TxtNameEn.Text = "";
		TxtSellPrice.Value = 0m;
		TxtCostPrice.Value = 0m;
		comboPrint.SelectedValue = 1;
		toggleAdd.IsOn = true;
		CHVat.Checked = true;
		TxtVat.Value = 0m;
		TxtNameAr.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 85))
		{
			int num = 0;
			if (TxtNameAr.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtNameAr.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = UNT.Insert(TxtNameAr.Text, TxtNameEn.Text, TxtSellPrice.Value, TxtCostPrice.Value, CHVat.Checked, TxtVat.Value, Convert.ToInt32(comboPrint.SelectedValue), toggleAdd.IsOn);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات الطاولات  ( " + TxtNameAr.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات الطاولات بنفس الاسم ( " + TxtNameAr.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtNameAr.Focus();
					break;
				}
			}
			else if (TxtNameAr.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم الطاولات ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtNameAr.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 85))
		{
			int num = 0;
			if (TxtNameAr.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + TxtNameAr.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					int position = gridEX1.SelectedItems[0].Position;
					int iD = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
					num = UNT.Update(iD, TxtNameAr.Text, TxtNameEn.Text, TxtSellPrice.Value, TxtCostPrice.Value, CHVat.Checked, TxtVat.Value, Convert.ToInt32(comboPrint.SelectedValue), toggleAdd.IsOn);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات الطاولات ( " + TxtNameAr.Text + " ) بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BindGrid();
						Clear();
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات الطاولات بنفس الاسم ( " + TxtNameAr.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
				case DialogResult.No:
					TxtNameAr.Focus();
					break;
				}
			}
			else if (TxtNameAr.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم الطاولات ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtNameAr.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 85))
		{
			int num = 0;
			if (TxtNameAr.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtNameAr.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					int position = gridEX1.SelectedItems[0].Position;
					int sn = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
					num = UNT.Delete(sn);
					if (num > 0)
					{
						BindGrid();
						MessageBox.Show("تم حذف بيانات الطاولات ( " + TxtNameAr.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						Clear();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtNameAr.Text + " ) لوجود عمليات جارية بهذا الطاولات", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				}
				case DialogResult.No:
					TxtNameAr.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر اسم الطاولات ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				gridEX1.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	public void GetPrinters()
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		List<Printer> list = gTSdbEntities.Printers.Where((Printer x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
		if (list != null)
		{
			comboPrint.DataSource = list;
			comboPrint.DisplayMember = "Name";
			comboPrint.ValueMember = "ID";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmRestaurant.LuckUp.FrmAdd));
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
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.toggleAdd = new DevExpress.XtraEditors.ToggleSwitch();
		this.comboPrint = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtSellPrice = new System.Windows.Forms.NumericUpDown();
		this.label20 = new System.Windows.Forms.Label();
		this.TxtCostPrice = new System.Windows.Forms.NumericUpDown();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtNameEn = new DevExpress.XtraEditors.ButtonEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtNameAr = new DevExpress.XtraEditors.ButtonEdit();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.CHVat = new System.Windows.Forms.CheckBox();
		this.label49 = new System.Windows.Forms.Label();
		this.TxtVat = new System.Windows.Forms.NumericUpDown();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.toggleAdd.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSellPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCostPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNameEn.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNameAr.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtVat).BeginInit();
		base.SuspendLayout();
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(448, 437);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(101, 40);
		this.BtnExit.TabIndex = 4;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(340, 437);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(100, 40);
		this.BtnDelete.TabIndex = 2;
		this.BtnDelete.Text = "حذف F7";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(232, 437);
		this.BtnEdit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Size = new System.Drawing.Size(100, 40);
		this.BtnEdit.TabIndex = 1;
		this.BtnEdit.Text = "تعديل F6";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(124, 437);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(100, 40);
		this.BtnSave.TabIndex = 0;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(14, 437);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(102, 40);
		this.BtnNew.TabIndex = 3;
		this.BtnNew.Text = "جديد F4";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(423, 33);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(86, 17);
		this.label2.TabIndex = 3;
		this.label2.Text = "الاسم العربي";
		this.groupBox1.Controls.Add(this.CHVat);
		this.groupBox1.Controls.Add(this.label49);
		this.groupBox1.Controls.Add(this.TxtVat);
		this.groupBox1.Controls.Add(this.toggleAdd);
		this.groupBox1.Controls.Add(this.comboPrint);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.TxtSellPrice);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.TxtCostPrice);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.TxtNameEn);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtNameAr);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Location = new System.Drawing.Point(14, 18);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(533, 211);
		this.groupBox1.TabIndex = 24;
		this.groupBox1.TabStop = false;
		this.toggleAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.toggleAdd.Location = new System.Drawing.Point(15, 128);
		this.toggleAdd.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.toggleAdd.Name = "toggleAdd";
		this.toggleAdd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.toggleAdd.Properties.Appearance.Options.UseFont = true;
		this.toggleAdd.Properties.Appearance.Options.UseForeColor = true;
		this.toggleAdd.Properties.OffText = "اضافة";
		this.toggleAdd.Properties.OnText = "بدون";
		this.toggleAdd.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.toggleAdd.Size = new System.Drawing.Size(167, 25);
		this.toggleAdd.TabIndex = 200;
		this.comboPrint.Font = new System.Drawing.Font("Tahoma", 8f);
		this.comboPrint.FormattingEnabled = true;
		this.comboPrint.Location = new System.Drawing.Point(226, 131);
		this.comboPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.comboPrint.Name = "comboPrint";
		this.comboPrint.Size = new System.Drawing.Size(191, 24);
		this.comboPrint.TabIndex = 120;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(442, 137);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(48, 17);
		this.label3.TabIndex = 121;
		this.label3.Text = "الطابعه";
		this.TxtSellPrice.DecimalPlaces = 3;
		this.TxtSellPrice.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtSellPrice.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtSellPrice.Location = new System.Drawing.Point(226, 93);
		this.TxtSellPrice.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtSellPrice.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtSellPrice.Name = "TxtSellPrice";
		this.TxtSellPrice.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtSellPrice.Size = new System.Drawing.Size(191, 26);
		this.TxtSellPrice.TabIndex = 118;
		this.TxtSellPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label20.Location = new System.Drawing.Point(422, 94);
		this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(68, 18);
		this.label20.TabIndex = 119;
		this.label20.Text = "سعر البيع";
		this.TxtCostPrice.DecimalPlaces = 3;
		this.TxtCostPrice.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtCostPrice.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtCostPrice.Location = new System.Drawing.Point(15, 91);
		this.TxtCostPrice.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCostPrice.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.TxtCostPrice.Name = "TxtCostPrice";
		this.TxtCostPrice.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtCostPrice.Size = new System.Drawing.Size(151, 26);
		this.TxtCostPrice.TabIndex = 116;
		this.TxtCostPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label19.Location = new System.Drawing.Point(170, 94);
		this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(48, 18);
		this.label19.TabIndex = 117;
		this.label19.Text = "الشراء";
		this.TxtNameEn.Location = new System.Drawing.Point(15, 60);
		this.TxtNameEn.Name = "TxtNameEn";
		this.TxtNameEn.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtNameEn.Properties.Appearance.Options.UseFont = true;
		this.TxtNameEn.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNameEn.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNameEn.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtNameEn.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions2.Image");
		this.TxtNameEn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.TxtNameEn.Size = new System.Drawing.Size(402, 28);
		this.TxtNameEn.TabIndex = 4;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(423, 67);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(102, 17);
		this.label1.TabIndex = 5;
		this.label1.Text = "الاسم الانجليزي";
		this.TxtNameAr.Location = new System.Drawing.Point(15, 26);
		this.TxtNameAr.Name = "TxtNameAr";
		this.TxtNameAr.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtNameAr.Properties.Appearance.Options.UseFont = true;
		this.TxtNameAr.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNameAr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNameAr.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtNameAr.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions3.Image");
		this.TxtNameAr.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.TxtNameAr.Size = new System.Drawing.Size(402, 28);
		this.TxtNameAr.TabIndex = 0;
		this.TxtNameAr.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtName_ButtonClick);
		this.TxtNameAr.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNameAr.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
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
		this.gridEX1.Location = new System.Drawing.Point(12, 235);
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.Size = new System.Drawing.Size(535, 195);
		this.gridEX1.TabIndex = 32;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX1.MouseClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseClick_1);
		this.CHVat.AutoSize = true;
		this.CHVat.Checked = true;
		this.CHVat.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CHVat.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.CHVat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.CHVat.Location = new System.Drawing.Point(226, 164);
		this.CHVat.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CHVat.Name = "CHVat";
		this.CHVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CHVat.Size = new System.Drawing.Size(140, 23);
		this.CHVat.TabIndex = 202;
		this.CHVat.Text = "الضريبة المضافة ";
		this.CHVat.UseVisualStyleBackColor = true;
		this.label49.AutoSize = true;
		this.label49.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label49.Location = new System.Drawing.Point(11, 165);
		this.label49.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label49.Name = "label49";
		this.label49.Size = new System.Drawing.Size(25, 19);
		this.label49.TabIndex = 203;
		this.label49.Text = "%";
		this.TxtVat.DecimalPlaces = 2;
		this.TxtVat.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtVat.Location = new System.Drawing.Point(42, 163);
		this.TxtVat.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtVat.Maximum = new decimal(new int[4] { 50, 0, 0, 0 });
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVat.Size = new System.Drawing.Size(124, 26);
		this.TxtVat.TabIndex = 201;
		this.TxtVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtVat.Value = new decimal(new int[4] { 15, 0, 0, 0 });
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(559, 490);
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
		base.Name = "FrmAdd";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "الاضافات";
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.toggleAdd.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSellPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCostPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNameEn.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNameAr.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtVat).EndInit();
		base.ResumeLayout(false);
	}
}


