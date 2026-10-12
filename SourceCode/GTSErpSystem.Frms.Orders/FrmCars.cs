using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.Properties;

namespace GTSErpSystem.Frms.Orders;

public class FrmCars : XtraForm
{
	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox Txtname;

	private Label label1;

	private TextBox TxtChassisNum;

	private Label label6;

	private TextBox Txtcounter;

	private Label label5;

	private TextBox TxtModel;

	private Label label4;

	private TextBox TxtColor;

	private Label label3;

	private TextBox TxtPlateNum;

	private Label label2;

	private SimpleButton BtnSave;

	public FrmCars()
	{
		InitializeComponent();
		GetData();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		PassData();
		Close();
	}

	private void PassData()
	{
		Cars.CarName = Txtname.Text;
		Cars.CarModel = TxtModel.Text;
		Cars.PlateNum = TxtPlateNum.Text;
		Cars.ChassisNum = TxtChassisNum.Text;
		Cars.Color = TxtColor.Text;
		Cars.Counter = Txtcounter.Text;
	}

	private void GetData()
	{
		Txtname.Text = Cars.CarName;
		TxtModel.Text = Cars.CarModel;
		TxtPlateNum.Text = Cars.PlateNum;
		TxtChassisNum.Text = Cars.ChassisNum;
		TxtColor.Text = Cars.Color;
		Txtcounter.Text = Cars.Counter;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmCars));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TxtChassisNum = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.Txtcounter = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtModel = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtColor = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtPlateNum = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.Txtname = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.TxtChassisNum);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.Txtcounter);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.TxtModel);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.TxtColor);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.TxtPlateNum);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.Txtname);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.ForeColor = System.Drawing.Color.Green;
		this.groupBox1.Location = new System.Drawing.Point(8, 1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(784, 175);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "بيانات السياره";
		this.TxtChassisNum.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtChassisNum.Location = new System.Drawing.Point(16, 38);
		this.TxtChassisNum.Name = "TxtChassisNum";
		this.TxtChassisNum.Size = new System.Drawing.Size(265, 26);
		this.TxtChassisNum.TabIndex = 11;
		this.TxtChassisNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(287, 43);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(105, 21);
		this.label6.TabIndex = 10;
		this.label6.Text = "رقم الشاسيه";
		this.Txtcounter.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Txtcounter.Location = new System.Drawing.Point(16, 128);
		this.Txtcounter.Name = "Txtcounter";
		this.Txtcounter.Size = new System.Drawing.Size(265, 26);
		this.Txtcounter.TabIndex = 9;
		this.Txtcounter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(287, 131);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(48, 21);
		this.label5.TabIndex = 8;
		this.label5.Text = "العداد";
		this.TxtModel.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtModel.Location = new System.Drawing.Point(409, 83);
		this.TxtModel.Name = "TxtModel";
		this.TxtModel.Size = new System.Drawing.Size(265, 26);
		this.TxtModel.TabIndex = 7;
		this.TxtModel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(680, 86);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(67, 21);
		this.label4.TabIndex = 6;
		this.label4.Text = "الموديل ";
		this.TxtColor.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtColor.Location = new System.Drawing.Point(16, 83);
		this.TxtColor.Name = "TxtColor";
		this.TxtColor.Size = new System.Drawing.Size(265, 26);
		this.TxtColor.TabIndex = 5;
		this.TxtColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(287, 86);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(44, 21);
		this.label3.TabIndex = 4;
		this.label3.Text = "اللون";
		this.TxtPlateNum.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtPlateNum.Location = new System.Drawing.Point(409, 128);
		this.TxtPlateNum.Name = "TxtPlateNum";
		this.TxtPlateNum.Size = new System.Drawing.Size(265, 26);
		this.TxtPlateNum.TabIndex = 3;
		this.TxtPlateNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(680, 131);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(84, 21);
		this.label2.TabIndex = 2;
		this.label2.Text = "رقم اللوحه";
		this.Txtname.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Txtname.Location = new System.Drawing.Point(409, 38);
		this.Txtname.Name = "Txtname";
		this.Txtname.Size = new System.Drawing.Size(265, 26);
		this.Txtname.TabIndex = 1;
		this.Txtname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(680, 41);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(100, 21);
		this.label1.TabIndex = 0;
		this.label1.Text = "اسم السياره";
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(8, 183);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(784, 39);
		this.BtnSave.TabIndex = 4;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 229);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.IconOptions.Image = GTSErpSystem.Properties.Resources.transit_16x16;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmCars";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "السيارات";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}


