using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.Frms.Orders.FrmLuckUpOrders;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.Orders.FrmOpenDay;

public class FrmOpenDay : XtraForm
{
	public bool ISOpen = false;

	private IContainer components = null;

	private GroupBox group;

	private TextBox TxtNote;

	private Label label3;

	private Label label2;

	private NumericEditBox TxtNet;

	private TextBox TxtName;

	private Label label1;

	private Label LblID;

	private ToggleSwitch RBIsOpen;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	private DateEdit TxtDate;

	public FrmOpenDay()
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
		Bind();
	}

	private void Bind()
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		User_Login user_Login = gTSdbEntities.User_Login.Where((User_Login x) => x.ID == LoginDetails.UserID).FirstOrDefault();
		if (user_Login != null)
		{
			TxtName.Text = LoginDetails.UserName;
			LblID.Text = LoginDetails.UserID.ToString();
			TxtNet.Text = user_Login.MoneyOpenDay.ToString();
			TxtNote.Text = user_Login.Note;
			RBIsOpen.IsOn = Convert.ToBoolean(user_Login.IsOpenDay);
			TxtDate.DateTime = DateTime.Now;
			if (Convert.ToBoolean(user_Login.IsOpenDay))
			{
				group.Enabled = false;
				ISOpen = Convert.ToBoolean(user_Login.IsOpenDay);
			}
		}
	}

	private int Save(int ID, decimal MoneyOpenDay, string Note, DateTime Date)
	{
		int result = 0;
		using (GTSdbEntities gTSdbEntities = new GTSdbEntities())
		{
			User_Login user_Login = gTSdbEntities.User_Login.Where((User_Login x) => x.ID == ID).FirstOrDefault();
			if (user_Login != null)
			{
				user_Login.MoneyOpenDay = MoneyOpenDay;
				user_Login.IsOpenDay = true;
				user_Login.Note = Note;
				user_Login.DateOpen = Date;
				result = gTSdbEntities.SaveChanges();
			}
		}
		return result;
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		FrmPassWordSupervisor frmPassWordSupervisor = new FrmPassWordSupervisor();
		frmPassWordSupervisor.ShowDialog();
		if (frmPassWordSupervisor.ISClose)
		{
			int num = Save(Convert.ToInt32(LblID.Text), Convert.ToDecimal(TxtNet.Text), TxtNote.Text, TxtDate.DateTime);
			if (num > 0)
			{
				ISOpen = true;
				MessageBox.Show("تم فتح اليومية بنجاح", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				Close();
			}
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmOpenDay.FrmOpenDay));
		this.group = new System.Windows.Forms.GroupBox();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.RBIsOpen = new DevExpress.XtraEditors.ToggleSwitch();
		this.LblID = new System.Windows.Forms.Label();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtName = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.group.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.RBIsOpen.Properties).BeginInit();
		base.SuspendLayout();
		this.group.Controls.Add(this.TxtDate);
		this.group.Controls.Add(this.BtnExit);
		this.group.Controls.Add(this.BtnSave);
		this.group.Controls.Add(this.RBIsOpen);
		this.group.Controls.Add(this.LblID);
		this.group.Controls.Add(this.TxtNote);
		this.group.Controls.Add(this.label3);
		this.group.Controls.Add(this.label2);
		this.group.Controls.Add(this.TxtNet);
		this.group.Controls.Add(this.TxtName);
		this.group.Controls.Add(this.label1);
		resources.ApplyResources(this.group, "group");
		this.group.Name = "group";
		this.group.TabStop = false;
		resources.ApplyResources(this.TxtDate, "TxtDate");
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDate.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDate.Properties.Appearance.Font");
		this.TxtDate.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDate.Properties.Appearance.Options.UseFont = true;
		this.TxtDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate.Properties.Buttons"))
		});
		this.TxtDate.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate.Properties.DisplayFormat.FormatString = "";
		this.TxtDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDate.Properties.EditFormat.FormatString = "";
		this.TxtDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDate.Properties.Mask.EditMask = resources.GetString("TxtDate.Properties.Mask.EditMask");
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate.Properties.ReadOnly = true;
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.RBIsOpen, "RBIsOpen");
		this.RBIsOpen.Name = "RBIsOpen";
		this.RBIsOpen.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("RBIsOpen.Properties.Appearance.Font");
		this.RBIsOpen.Properties.Appearance.Options.UseFont = true;
		this.RBIsOpen.Properties.Appearance.Options.UseForeColor = true;
		this.RBIsOpen.Properties.OffText = resources.GetString("RBIsOpen.Properties.OffText");
		this.RBIsOpen.Properties.OnText = resources.GetString("RBIsOpen.Properties.OnText");
		this.RBIsOpen.Properties.ReadOnly = true;
		resources.ApplyResources(this.LblID, "LblID");
		this.LblID.Name = "LblID";
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtNet, "TxtNet");
		this.TxtNet.DecimalDigits = 2;
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtName.BackColor = System.Drawing.Color.White;
		resources.ApplyResources(this.TxtName, "TxtName");
		this.TxtName.Name = "TxtName";
		this.TxtName.ReadOnly = true;
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.group);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmOpenDay";
		this.group.ResumeLayout(false);
		this.group.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.RBIsOpen.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


