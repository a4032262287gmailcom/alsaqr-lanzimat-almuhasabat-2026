using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Security;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Items;

public class FrmGroupsRes : XtraForm
{
	private Class_Group Cats = new Class_Group();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_Customer Cus = new Class_Customer();

	private GTSdbEntities GTSdbEntities = new GTSdbEntities();

	private IContainer components = null;

	private Label label2;

	private GroupBox groupBox1;

	private PictureEdit pictureEdit1;

	private System.Windows.Forms.ComboBox comboPrint;

	private Label label1;

	private ButtonEdit TxtName;

	private SimpleButton BtnExit;

	private SimpleButton BtnDelete;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private GridEX gridEX1;

	private OpenFileDialog openFileDialog1;

	private CheckBox CBShow;

	private SimpleButton BtnDeletePic;

	private System.Windows.Forms.ComboBox DropSupp;

	private Label label3;

	private Label label4;

	public FrmGroupsRes()
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
		BindGrid();
		TxtName.Focus();
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		GetPrinters();
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

	private void BtnImage_Click(object sender, EventArgs e)
	{
		openFileDialog1.Title = "Choose Image";
		openFileDialog1.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			Image image = new Bitmap(openFileDialog1.FileName);
			pictureEdit1.Image = image;
		}
	}

	private void gridEX1_MouseClick_1(object sender, MouseEventArgs e)
	{
		try
		{
			int position = gridEX1.SelectedItems[0].Position;
			int id = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
			Item_Groups item_Groups = (from x in Cats.GetAll()
				where x.ID == id
				select x).FirstOrDefault();
			TxtName.Text = item_Groups.Name;
			CBShow.Checked = Convert.ToBoolean(item_Groups.IsShow);
			if (item_Groups.PrinterID.HasValue)
			{
				comboPrint.SelectedValue = item_Groups.PrinterID;
			}
			if (item_Groups.AccountID.HasValue)
			{
				DropSupp.SelectedValue = item_Groups.AccountID;
			}
			string image = item_Groups.Image;
			try
			{
				if (image == null || image == "")
				{
					pictureEdit1.EditValue = null;
				}
				else
				{
					pictureEdit1.EditValue = Base64ToImage(image);
				}
			}
			catch (Exception)
			{
				pictureEdit1.EditValue = null;
			}
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

	private void Clear()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BindGrid();
		GetPrinters();
		TxtName.Text = "";
		pictureEdit1.Image = null;
		CBShow.Checked = true;
		TxtName.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 7))
		{
			int num = 0;
			if (TxtName.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtName.Text + " ) الي مجموعة الاصناف", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					string text = "";
					text = ((pictureEdit1.Image != null) ? ImageToBase64(pictureEdit1.Image, ImageFormat.Png) : "");
					num = Cats.Insert(TxtName.Text, Convert.ToInt32(comboPrint.SelectedValue), CBShow.Checked, text, Convert.ToInt32(DropSupp.SelectedValue));
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات مجموعة الاصناف ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
				}
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
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 7))
		{
			int num = 0;
			if (TxtName.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + TxtName.Text + " ) الي مجموعة الاصناف", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					int position = gridEX1.SelectedItems[0].Position;
					string value = gridEX1.GetRow(position).Cells[0].Text;
					string text = "";
					text = ((pictureEdit1.Image != null) ? ImageToBase64(pictureEdit1.Image, ImageFormat.Png) : "");
					num = Cats.Update(Convert.ToInt32(value), TxtName.Text, Convert.ToInt32(comboPrint.SelectedValue), CBShow.Checked, text, Convert.ToInt32(DropSupp.SelectedValue));
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات مجموعة الاصناف ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
				}
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
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 7))
		{
			int num = 0;
			if (TxtName.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtName.Text + " ) من مجموعة الاصناف", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = Cats.Delete(TxtName.Text);
					if (num > 0)
					{
						BindGrid();
						MessageBox.Show("تم حذف بيانات مجموعة الاصناف ( " + TxtName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						Clear();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtName.Text + " ) بسبب ادارج بيانات الاصناف تحت هذه المجموعة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

	public void GetPrinters()
	{
		comboPrint.DataSource = Cats.GetAllPrinter();
		comboPrint.DisplayMember = "Name";
		comboPrint.ValueMember = "ID";
	}

	public void BindGrid()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("ID", typeof(int));
		dataTable.Columns.Add("Account_Name", typeof(string));
		foreach (Get_SupplierAccount_Result item in GTSdbEntities.Get_SupplierAccount().ToList())
		{
			dataTable.Rows.Add(item.ID, item.Account_Name);
		}
		DataRow dataRow = dataTable.NewRow();
		dataRow.ItemArray = new object[2] { 0, "الكل" };
		dataTable.Rows.InsertAt(dataRow, 0);
		DropSupp.ValueMember = "ID";
		DropSupp.DisplayMember = "Account_Name";
		DropSupp.DataSource = dataTable;
		DropSupp.SelectedValue = 0;
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable2 = new DataTable("gridEX1");
			dataTable2.Columns.Add("كود ", typeof(int));
			dataTable2.Columns.Add("المجموعة", typeof(string));
			dataTable2.Columns.Add("الطابعة", typeof(int));
			dataTable2.Columns.Add("show", typeof(string));
			dataTable2.Columns.Add("الصورة", typeof(string));
			dataTable2.Columns.Add("AccountID", typeof(int));
			foreach (Item_Groups item2 in from x in Cats.GetAll()
				where x.ID > 0
				select x)
			{
				dataTable2.Rows.Add(item2.ID, item2.Name, item2.PrinterID, item2.IsShow, item2.Image, item2.AccountID);
			}
			gridEX1.SetDataBinding(dataTable2, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["كود "].Width = 20;
			gridEX1.RootTable.Columns["كود "].RightToLeft = RightToLeft.No;
			gridEX1.RootTable.Columns["الطابعة"].Visible = false;
			gridEX1.RootTable.Columns["الصورة"].Visible = false;
			gridEX1.RootTable.Columns["show"].Visible = false;
			gridEX1.RootTable.Columns["AccountID"].Visible = false;
			return;
		}
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable3 = new DataTable("gridEX1");
		dataTable3.Columns.Add("Code", typeof(int));
		dataTable3.Columns.Add("Group Name", typeof(string));
		dataTable3.Columns.Add("Printer", typeof(int));
		dataTable3.Columns.Add("show", typeof(string));
		dataTable3.Columns.Add("Image", typeof(string));
		dataTable3.Columns.Add("AccountID", typeof(int));
		foreach (Item_Groups item3 in from x in Cats.GetAll()
			where x.ID > 0
			select x)
		{
			dataTable3.Rows.Add(item3.ID, item3.Name, item3.PrinterID, item3.IsShow, item3.Image, item3.AccountID);
		}
		gridEX1.SetDataBinding(dataTable3, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["Code"].Width = 20;
		gridEX1.RootTable.Columns["Code"].RightToLeft = RightToLeft.No;
		gridEX1.RootTable.Columns["Printer"].Visible = false;
		gridEX1.RootTable.Columns["Image"].Visible = false;
		gridEX1.RootTable.Columns["show"].Visible = false;
		gridEX1.RootTable.Columns["AccountID"].Visible = false;
	}

	private void TxtName_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		BtnNew_Click(null, null);
	}

	private void BtnDeletePic_Click(object sender, EventArgs e)
	{
		pictureEdit1.Image = null;
	}

	private void DropSupp_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void comboPrint_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Items.FrmGroupsRes));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.CBShow = new System.Windows.Forms.CheckBox();
		this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
		this.DropSupp = new System.Windows.Forms.ComboBox();
		this.comboPrint = new System.Windows.Forms.ComboBox();
		this.BtnDeletePic = new DevExpress.XtraEditors.SimpleButton();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtName = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureEdit1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.CBShow);
		this.groupBox1.Controls.Add(this.pictureEdit1);
		this.groupBox1.Controls.Add(this.DropSupp);
		this.groupBox1.Controls.Add(this.comboPrint);
		this.groupBox1.Controls.Add(this.BtnDeletePic);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtName);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.CBShow, "CBShow");
		this.CBShow.Checked = true;
		this.CBShow.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CBShow.Name = "CBShow";
		this.CBShow.UseVisualStyleBackColor = true;
		this.CBShow.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
		resources.ApplyResources(this.pictureEdit1, "pictureEdit1");
		this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
		this.pictureEdit1.Name = "pictureEdit1";
		this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
		this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
		this.pictureEdit1.Click += new System.EventHandler(BtnImage_Click);
		this.pictureEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
		resources.ApplyResources(this.DropSupp, "DropSupp");
		this.DropSupp.FormattingEnabled = true;
		this.DropSupp.Name = "DropSupp";
		this.DropSupp.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.DropSupp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(DropSupp_KeyPress);
		resources.ApplyResources(this.comboPrint, "comboPrint");
		this.comboPrint.FormattingEnabled = true;
		this.comboPrint.Name = "comboPrint";
		this.comboPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.comboPrint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(comboPrint_KeyPress);
		resources.ApplyResources(this.BtnDeletePic, "BtnDeletePic");
		this.BtnDeletePic.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDeletePic.ImageOptions.Image");
		this.BtnDeletePic.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDeletePic.Name = "BtnDeletePic";
		this.BtnDeletePic.Click += new System.EventHandler(BtnDeletePic_Click);
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.TxtName, "TxtName");
		this.TxtName.Name = "TxtName";
		this.TxtName.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtName.Properties.Appearance.Font");
		this.TxtName.Properties.Appearance.Options.UseFont = true;
		this.TxtName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.TxtName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtName.Properties.Buttons"), resources.GetString("TxtName.Properties.Buttons1"), (int)resources.GetObject("TxtName.Properties.Buttons2"), (bool)resources.GetObject("TxtName.Properties.Buttons3"), (bool)resources.GetObject("TxtName.Properties.Buttons4"), (bool)resources.GetObject("TxtName.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtName.Properties.Buttons6"), resources.GetObject("TxtName.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtName.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtName.Properties.Buttons9"))
		});
		this.TxtName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtName_ButtonClick);
		this.TxtName.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		this.openFileDialog1.FileName = "openFileDialog1";
		resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.gridEX1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmGroupsRes";
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureEdit1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		base.ResumeLayout(false);
	}
}


