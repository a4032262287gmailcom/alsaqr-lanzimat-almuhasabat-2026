using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Orders;

namespace GTSErpSystem.Frms.Orders.FrmReservations;

public class FrmReservations : XtraForm
{
	private Class_Halls hall = new Class_Halls();

	private IContainer components = null;

	private SimpleButton simpleButton1;

	private Label label52;

	private System.Windows.Forms.ComboBox ComHalls;

	private GroupBox groupBox1;

	private DateEdit TxtInstallationDate;

	private Label label21;

	private DateEdit TxtBookingDate;

	private DateEdit TxtjawDate;

	private Label label3;

	private Label label2;

	private GroupBox groupBox2;

	private SimpleButton BtnSave;

	private SimpleButton BtnExit;

	public DateTime BookingDate { get; set; }

	public DateTime InstallationDate { get; set; }

	public DateTime JawDate { get; set; }

	public int HallsID { get; set; }

	public FrmReservations()
	{
		InitializeComponent();
		Bind();
	}

	private void simpleButton1_Click(object sender, EventArgs e)
	{
		new FrmHalls().Show();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		BookingDate = TxtBookingDate.DateTime;
		InstallationDate = TxtInstallationDate.DateTime;
		JawDate = TxtjawDate.DateTime;
		HallsID = Convert.ToInt32(ComHalls.SelectedValue);
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void Bind()
	{
		TxtBookingDate.DateTime = DateTime.Now;
		TxtInstallationDate.DateTime = DateTime.Now;
		TxtjawDate.DateTime = DateTime.Now;
		ComHalls.DataSource = hall.GetAllHalls();
		ComHalls.DisplayMember = "Name";
		ComHalls.ValueMember = "ID";
		ComHalls.SelectedValue = 1;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmReservations.FrmReservations));
		this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
		this.label52 = new System.Windows.Forms.Label();
		this.ComHalls = new System.Windows.Forms.ComboBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label21 = new System.Windows.Forms.Label();
		this.TxtBookingDate = new DevExpress.XtraEditors.DateEdit();
		this.TxtInstallationDate = new DevExpress.XtraEditors.DateEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtjawDate = new DevExpress.XtraEditors.DateEdit();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtBookingDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBookingDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtInstallationDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtInstallationDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtjawDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtjawDate.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.simpleButton1.Appearance.Options.UseFont = true;
		this.simpleButton1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
		this.simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.simpleButton1.Location = new System.Drawing.Point(13, 158);
		this.simpleButton1.Margin = new System.Windows.Forms.Padding(0);
		this.simpleButton1.Name = "simpleButton1";
		this.simpleButton1.Size = new System.Drawing.Size(31, 30);
		this.simpleButton1.TabIndex = 196;
		this.simpleButton1.Click += new System.EventHandler(simpleButton1_Click);
		this.label52.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label52.AutoSize = true;
		this.label52.Font = new System.Drawing.Font("Tahoma", 9f);
		this.label52.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label52.Location = new System.Drawing.Point(386, 164);
		this.label52.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label52.Name = "label52";
		this.label52.Size = new System.Drawing.Size(80, 18);
		this.label52.TabIndex = 195;
		this.label52.Text = "اسم القاعه";
		this.ComHalls.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ComHalls.Font = new System.Drawing.Font("Tahoma", 10f);
		this.ComHalls.FormattingEnabled = true;
		this.ComHalls.Location = new System.Drawing.Point(48, 159);
		this.ComHalls.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.ComHalls.Name = "ComHalls";
		this.ComHalls.Size = new System.Drawing.Size(324, 29);
		this.ComHalls.TabIndex = 194;
		this.groupBox1.Controls.Add(this.TxtjawDate);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtInstallationDate);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.TxtBookingDate);
		this.groupBox1.Controls.Add(this.ComHalls);
		this.groupBox1.Controls.Add(this.simpleButton1);
		this.groupBox1.Controls.Add(this.label52);
		this.groupBox1.Location = new System.Drawing.Point(9, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(494, 204);
		this.groupBox1.TabIndex = 197;
		this.groupBox1.TabStop = false;
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label21.Location = new System.Drawing.Point(387, 29);
		this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(88, 21);
		this.label21.TabIndex = 198;
		this.label21.Text = "تاريخ الحجز";
		this.TxtBookingDate.EditValue = null;
		this.TxtBookingDate.Location = new System.Drawing.Point(13, 26);
		this.TxtBookingDate.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtBookingDate.Name = "TxtBookingDate";
		this.TxtBookingDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtBookingDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtBookingDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtBookingDate.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBookingDate.Properties.Appearance.Options.UseFont = true;
		this.TxtBookingDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBookingDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBookingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtBookingDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtBookingDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtBookingDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtBookingDate.Size = new System.Drawing.Size(359, 28);
		this.TxtBookingDate.TabIndex = 197;
		this.TxtInstallationDate.EditValue = null;
		this.TxtInstallationDate.Location = new System.Drawing.Point(13, 70);
		this.TxtInstallationDate.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtInstallationDate.Name = "TxtInstallationDate";
		this.TxtInstallationDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtInstallationDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtInstallationDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtInstallationDate.Properties.Appearance.Options.UseBackColor = true;
		this.TxtInstallationDate.Properties.Appearance.Options.UseFont = true;
		this.TxtInstallationDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtInstallationDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtInstallationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtInstallationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtInstallationDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtInstallationDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtInstallationDate.Size = new System.Drawing.Size(359, 28);
		this.TxtInstallationDate.TabIndex = 199;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(387, 77);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(100, 21);
		this.label2.TabIndex = 201;
		this.label2.Text = "تاريخ التركيب";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(387, 121);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(80, 21);
		this.label3.TabIndex = 202;
		this.label3.Text = "تاريخ الفك";
		this.TxtjawDate.EditValue = null;
		this.TxtjawDate.Location = new System.Drawing.Point(13, 118);
		this.TxtjawDate.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtjawDate.Name = "TxtjawDate";
		this.TxtjawDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtjawDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtjawDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtjawDate.Properties.Appearance.Options.UseBackColor = true;
		this.TxtjawDate.Properties.Appearance.Options.UseFont = true;
		this.TxtjawDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtjawDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtjawDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtjawDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtjawDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtjawDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtjawDate.Size = new System.Drawing.Size(359, 28);
		this.TxtjawDate.TabIndex = 203;
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Location = new System.Drawing.Point(9, 222);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(494, 56);
		this.groupBox2.TabIndex = 198;
		this.groupBox2.TabStop = false;
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(364, 12);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(5);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(125, 39);
		this.BtnSave.TabIndex = 4;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(6, 12);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(5);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(125, 39);
		this.BtnExit.TabIndex = 9;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(512, 282);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmReservations";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تفاصيل الحجز";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtBookingDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBookingDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtInstallationDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtInstallationDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtjawDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtjawDate.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


