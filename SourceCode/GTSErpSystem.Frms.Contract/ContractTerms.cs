using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.Properties;

namespace GTSErpSystem.Frms.Contract;

public class ContractTerms : XtraForm
{
	private Class_Setting Userx = new Class_Setting();

	private IContainer components = null;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private GroupBox Btn;

	private SimpleButton BtnSave;

	private SimpleButton BtnExit;

	private TextBox TxtBContract;

	private TextBox TxtCotractBasic;

	private SimpleButton BtnTranFar;

	public ContractTerms()
	{
		InitializeComponent();
		TxtCotractBasic.Text = LoginDetails.BasicContract;
		TxtBContract.Text = LoginDetails.contractTerms;
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		int num = Userx.UpdateItemContract(TxtCotractBasic.Text);
		LoginDetails.BasicContract = TxtCotractBasic.Text;
		LoginDetails.contractTerms = (string.IsNullOrWhiteSpace(TxtBContract.Text) ? LoginDetails.BasicContract : TxtBContract.Text);
		MessageBox.Show("تم الحفظ بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		Close();
	}

	private void simpleButton1_Click(object sender, EventArgs e)
	{
		TxtBContract.Text = TxtCotractBasic.Text;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Contract.ContractTerms));
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.TxtBContract = new System.Windows.Forms.TextBox();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.TxtCotractBasic = new System.Windows.Forms.TextBox();
		this.Btn = new System.Windows.Forms.GroupBox();
		this.BtnTranFar = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.Btn.SuspendLayout();
		base.SuspendLayout();
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(12, 3);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.tabControl1.RightToLeftLayout = true;
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(742, 463);
		this.tabControl1.TabIndex = 0;
		this.tabPage1.Controls.Add(this.TxtBContract);
		this.tabPage1.Location = new System.Drawing.Point(4, 25);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.tabPage1.Size = new System.Drawing.Size(734, 434);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "بنود العقد";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.TxtBContract.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtBContract.Location = new System.Drawing.Point(6, 6);
		this.TxtBContract.Multiline = true;
		this.TxtBContract.Name = "TxtBContract";
		this.TxtBContract.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.TxtBContract.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.TxtBContract.Size = new System.Drawing.Size(722, 422);
		this.TxtBContract.TabIndex = 0;
		this.tabPage2.Controls.Add(this.TxtCotractBasic);
		this.tabPage2.Location = new System.Drawing.Point(4, 25);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(734, 434);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "بنود العقد الاساسيه";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.TxtCotractBasic.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtCotractBasic.Location = new System.Drawing.Point(6, 6);
		this.TxtCotractBasic.Multiline = true;
		this.TxtCotractBasic.Name = "TxtCotractBasic";
		this.TxtCotractBasic.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.TxtCotractBasic.Size = new System.Drawing.Size(722, 422);
		this.TxtCotractBasic.TabIndex = 1;
		this.Btn.Controls.Add(this.BtnTranFar);
		this.Btn.Controls.Add(this.BtnExit);
		this.Btn.Controls.Add(this.BtnSave);
		this.Btn.Location = new System.Drawing.Point(16, 468);
		this.Btn.Name = "Btn";
		this.Btn.Size = new System.Drawing.Size(734, 72);
		this.Btn.TabIndex = 1;
		this.Btn.TabStop = false;
		this.BtnTranFar.ImageOptions.Image = GTSErpSystem.Properties.Resources.next_16x16;
		this.BtnTranFar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnTranFar.Location = new System.Drawing.Point(143, 24);
		this.BtnTranFar.Margin = new System.Windows.Forms.Padding(5);
		this.BtnTranFar.Name = "BtnTranFar";
		this.BtnTranFar.Size = new System.Drawing.Size(287, 39);
		this.BtnTranFar.TabIndex = 7;
		this.BtnTranFar.Text = "نقل بنود العقد الاساسه الي بنود العقد";
		this.BtnTranFar.Click += new System.EventHandler(simpleButton1_Click);
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(8, 24);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(5);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(125, 39);
		this.BtnExit.TabIndex = 6;
		this.BtnExit.Text = "خروج Exit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(601, 26);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(5);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(125, 39);
		this.BtnSave.TabIndex = 1;
		this.BtnSave.Text = "حفظ Save";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(766, 547);
		base.Controls.Add(this.Btn);
		base.Controls.Add(this.tabControl1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "ContractTerms";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "بنود العقد";
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		this.Btn.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


