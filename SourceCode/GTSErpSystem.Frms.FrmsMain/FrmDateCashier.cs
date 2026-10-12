using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmDateCashier : XtraForm
{
	private IContainer components = null;

	private DateEdit TxtDate;

	private Label label21;

	private GroupBox groupBox1;

	private SimpleButton BtnRefresh;

	private SimpleButton BtnSave;

	public FrmDateCashier()
	{
		InitializeComponent();
		TxtDate.DateTime = LoginDetails.DateApp;
	}

	private void BtnRefresh_Click(object sender, EventArgs e)
	{
		TxtDate.DateTime = DateTime.Now;
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		LoginDetails.DateApp = TxtDate.DateTime;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.FrmDateCashier));
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnRefresh = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.TxtDate.EditValue = null;
		this.TxtDate.Location = new System.Drawing.Point(154, 35);
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
		this.TxtDate.Properties.Appearance.Options.UseFont = true;
		this.TxtDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate.Size = new System.Drawing.Size(220, 34);
		this.TxtDate.TabIndex = 114;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label21.Location = new System.Drawing.Point(394, 42);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(53, 21);
		this.label21.TabIndex = 115;
		this.label21.Text = "التاريخ";
		this.groupBox1.Controls.Add(this.BtnRefresh);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(469, 87);
		this.groupBox1.TabIndex = 116;
		this.groupBox1.TabStop = false;
		this.BtnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnRefresh.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnRefresh.Appearance.Options.UseFont = true;
		this.BtnRefresh.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnRefresh.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnRefresh.Location = new System.Drawing.Point(7, 34);
		this.BtnRefresh.Margin = new System.Windows.Forms.Padding(4);
		this.BtnRefresh.Name = "BtnRefresh";
		this.BtnRefresh.Size = new System.Drawing.Size(140, 37);
		this.BtnRefresh.TabIndex = 116;
		this.BtnRefresh.Text = "تحديث ";
		this.BtnRefresh.Click += new System.EventHandler(BtnRefresh_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(107, 106);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(367, 50);
		this.BtnSave.TabIndex = 117;
		this.BtnSave.Text = "تثبيت التاريخ";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(493, 169);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmDateCashier";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "اعداد تثبيت التاريخ في الكاشير";
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}


