using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Items;

namespace GTSErpSystem.Frms.Items.FrmSearch;

public class FrmSearchByBarcode : XtraForm
{
	private Class_Items Itm = new Class_Items();

	public string BracodeItem;

	private IContainer components = null;

	private GroupBox groupBox1;

	private SimpleButton BtnSave;

	private SimpleButton BtnExit;

	public ButtonEdit TxtBarcode;

	private Label label2;

	public FrmSearchByBarcode()
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
		TxtBarcode.Focus();
	}

	private void TxtBarcode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		TxtBarcode.Text = "";
		TxtBarcode.Focus();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		CashierReturn();
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

	private void CashierReturn()
	{
		try
		{
			if (TxtBarcode.Text == "")
			{
				MessageBox.Show("عفوا\u064eطŒ برجاء ادخال رقم الباركود ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtBarcode.Text = "";
				TxtBarcode.Focus();
				return;
			}
			BracodeItem = Itm.SearchByBarcode(TxtBarcode.Text);
			if (BracodeItem == "")
			{
				MessageBox.Show("عفوا، هذا الباركود ( " + TxtBarcode.Text + " ) غير مسجل في قاعدة البيانات", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtBarcode.Text = "";
				TxtBarcode.Focus();
			}
			else
			{
				Close();
			}
		}
		catch (Exception)
		{
		}
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
	}

	private void Frms_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F3")
		{
			CashierReturn();
		}
	}

	private void TxtBarcode_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			CashierReturn();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Items.FrmSearch.FrmSearchByBarcode));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions imageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtBarcode = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.BtnSave);
		this.groupBox1.Controls.Add(this.BtnExit);
		this.groupBox1.Controls.Add(this.TxtBarcode);
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		this.label2.Cursor = System.Windows.Forms.Cursors.Default;
		resources.ApplyResources(this.label2, "label2");
		this.label2.ForeColor = System.Drawing.Color.Teal;
		this.label2.Name = "label2";
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.TxtBarcode, "TxtBarcode");
		this.TxtBarcode.Name = "TxtBarcode";
		this.TxtBarcode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcode.Properties.Appearance.Font");
		this.TxtBarcode.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		this.TxtBarcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcode.Properties.Buttons"), resources.GetString("TxtBarcode.Properties.Buttons1"), (int)resources.GetObject("TxtBarcode.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcode.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcode.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcode.Properties.Buttons5"), imageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtBarcode.Properties.Buttons6"), resources.GetObject("TxtBarcode.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcode.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcode.Properties.Buttons9"))
		});
		this.TxtBarcode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcode_ButtonClick);
		this.TxtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtBarcode_KeyDown);
		this.TxtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.IconOptions.ShowIcon = false;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmSearchByBarcode";
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


