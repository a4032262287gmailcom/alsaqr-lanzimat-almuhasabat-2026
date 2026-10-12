using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Main;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmLock : XtraForm
{
	private Class_Login LGN = new Class_Login();

	private IContainer components = null;

	public ButtonEdit TxtName;

	private Label label1;

	private Label label2;

	public ButtonEdit TxtPassword;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	private PictureBox pictureBox1;

	private Label LblUser;

	public FrmLock()
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
		TxtName.Text = LoginDetails.UserName;
		LblUser.Text = LoginDetails.UserID.ToString();
		TxtPassword.Focus();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		Login();
	}

	private void Login()
	{
		if (TxtPassword.Text != "")
		{
			int num = 0;
			num = LGN.LOGIN(Convert.ToInt32(LblUser.Text), TxtPassword.Text);
			if (num > 0)
			{
				Hide();
				return;
			}
			MessageBox.Show("كلمة المرور التي ادخلتها غير صحيحة طŒ برجاء التأكد من كلمة المرور ثم حاول مرة أخري", "تسجيل الدخول", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			TxtPassword.Text = "";
			TxtPassword.Focus();
		}
		else
		{
			MessageBox.Show("عفوا\u064bطŒ يرجي ادخال كلمة المرور ثم حاول مرة أخري", "تسجيل الدخول", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			TxtPassword.Text = "";
			TxtPassword.Focus();
		}
	}

	private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			Login();
		}
	}

	private void TxtPassword_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		TxtPassword.Text = "";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.FrmLock));
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
		this.TxtName = new DevExpress.XtraEditors.ButtonEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtPassword = new DevExpress.XtraEditors.ButtonEdit();
		this.LblUser = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.TxtName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPassword.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.TxtName, "TxtName");
		this.TxtName.Name = "TxtName";
		this.TxtName.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtName.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtName.Properties.Appearance.Font");
		this.TxtName.Properties.Appearance.Options.UseBackColor = true;
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
		this.TxtName.Properties.ReadOnly = true;
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtPassword, "TxtPassword");
		this.TxtPassword.Name = "TxtPassword";
		this.TxtPassword.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtPassword.Properties.Appearance.Font");
		this.TxtPassword.Properties.Appearance.ForeColor = System.Drawing.Color.Teal;
		this.TxtPassword.Properties.Appearance.Options.UseFont = true;
		this.TxtPassword.Properties.Appearance.Options.UseForeColor = true;
		this.TxtPassword.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPassword.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPassword.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.TxtPassword.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtPassword.Properties.Buttons"), resources.GetString("TxtPassword.Properties.Buttons1"), (int)resources.GetObject("TxtPassword.Properties.Buttons2"), (bool)resources.GetObject("TxtPassword.Properties.Buttons3"), (bool)resources.GetObject("TxtPassword.Properties.Buttons4"), (bool)resources.GetObject("TxtPassword.Properties.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("TxtPassword.Properties.Buttons6"), resources.GetObject("TxtPassword.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtPassword.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtPassword.Properties.Buttons9"))
		});
		this.TxtPassword.Properties.PasswordChar = '#';
		this.TxtPassword.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtPassword_ButtonClick);
		this.TxtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtPassword_KeyDown);
		resources.ApplyResources(this.LblUser, "LblUser");
		this.LblUser.Name = "LblUser";
		resources.ApplyResources(this.pictureBox1, "pictureBox1");
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.TabStop = false;
		resources.ApplyResources(this.BtnContinue, "BtnContinue");
		this.BtnContinue.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnContinue.Appearance.Font");
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ControlBox = false;
		base.Controls.Add(this.LblUser);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.BtnContinue);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.TxtPassword);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.TxtName);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmLock";
		((System.ComponentModel.ISupportInitialize)this.TxtName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPassword.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


