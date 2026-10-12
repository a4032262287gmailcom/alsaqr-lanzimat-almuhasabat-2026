using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GTSErpSystem.Frms.Orders;

public class FrmInvoicType : XtraForm
{
	public class OrderTypeElectronicInvoice
	{
		public int OrderTypeElectronicInvoiceId { get; set; } = 388;

		public string NormalOrSimpleInvoice { get; set; } = "02";

		public bool ThirdParty { get; set; } = false;

		public bool NominalInvoice { get; set; } = false;

		public bool ExportInvoice { get; set; } = false;

		public bool SummaryInvoice { get; set; } = false;

		public bool SelfInvoice { get; set; } = false;
	}

	private int Value;

	private string ValueType;

	private IContainer components = null;

	private GroupBox groupBox1;

	private RadioButton StandaredInvoice;

	private RadioButton SimplifiedDebitNote;

	private RadioButton StandaredDebitNote;

	private RadioButton StandardCreditNote;

	private RadioButton SimplifiedCreditNote;

	private RadioButton SimplifiedInvoice;

	private GroupBox groupBox2;

	private RadioButton Invoic;

	private RadioButton simpliInvoic;

	private GroupBox groupBox3;

	private CheckBox CkSelfInvoice;

	private CheckBox CkExportInvoice;

	private CheckBox CkSummaryInvoice;

	private CheckBox CkNominalInvoice;

	private CheckBox CkThirdParty;

	private SimpleButton BtnGo;

	public FrmInvoicType(string Type)
	{
		InitializeComponent();
		if (Type == "Order")
		{
			SimplifiedInvoice.Checked = true;
		}
		else if (Type == "OrderRetrn")
		{
			SimplifiedCreditNote.Checked = true;
		}
		simpliInvoic.Checked = true;
	}

	private void CheckChange()
	{
		if (SimplifiedInvoice.Checked)
		{
			Value = 388;
		}
		else if (StandaredInvoice.Checked)
		{
			Value = 388;
		}
		else if (SimplifiedDebitNote.Checked)
		{
			Value = 383;
		}
		else if (StandaredDebitNote.Checked)
		{
			Value = 383;
		}
		else if (SimplifiedCreditNote.Checked)
		{
			Value = 381;
		}
		if (Invoic.Checked)
		{
			ValueType = "01";
		}
		else if (simpliInvoic.Checked)
		{
			ValueType = "02";
		}
	}

	private void SimplifiedInvoice_CheckedChanged(object sender, EventArgs e)
	{
		CheckChange();
	}

	private void StandaredInvoice_CheckedChanged(object sender, EventArgs e)
	{
		CheckChange();
	}

	private void SimplifiedDebitNote_CheckedChanged(object sender, EventArgs e)
	{
		CheckChange();
	}

	private void StandaredDebitNote_CheckedChanged(object sender, EventArgs e)
	{
		CheckChange();
	}

	private void SimplifiedCreditNote_CheckedChanged(object sender, EventArgs e)
	{
		CheckChange();
	}

	private void StandardCreditNote_CheckedChanged(object sender, EventArgs e)
	{
		CheckChange();
	}

	private void Invoic_CheckedChanged(object sender, EventArgs e)
	{
		CheckChange();
	}

	private void simpliInvoic_CheckedChanged(object sender, EventArgs e)
	{
		CheckChange();
	}

	private void CkSummaryInvoice_CheckedChanged(object sender, EventArgs e)
	{
		if (CkSummaryInvoice.Checked)
		{
			CkSelfInvoice.Checked = false;
		}
	}

	private void CkSelfInvoice_CheckedChanged(object sender, EventArgs e)
	{
		if (CkSelfInvoice.Checked)
		{
			CkSummaryInvoice.Checked = false;
		}
	}

	private void FrmInvoicType_FormClosing(object sender, FormClosingEventArgs e)
	{
		LoginDetails.OrderTypeElectronicInvoiceId = Value;
		LoginDetails.NormalOrSimpleInvoice = ValueType;
		LoginDetails.ThirdParty = CkThirdParty.Checked;
		LoginDetails.NominalInvoice = CkNominalInvoice.Checked;
		LoginDetails.ExportInvoice = CkExportInvoice.Checked;
		LoginDetails.SummaryInvoice = CkSummaryInvoice.Checked;
		LoginDetails.SelfInvoice = CkSelfInvoice.Checked;
	}

	private void BtnGo_Click(object sender, EventArgs e)
	{
		LoginDetails.OrderTypeElectronicInvoiceId = Value;
		LoginDetails.NormalOrSimpleInvoice = ValueType;
		LoginDetails.ThirdParty = CkThirdParty.Checked;
		LoginDetails.NominalInvoice = CkNominalInvoice.Checked;
		LoginDetails.ExportInvoice = CkExportInvoice.Checked;
		LoginDetails.SummaryInvoice = CkSummaryInvoice.Checked;
		LoginDetails.SelfInvoice = CkSelfInvoice.Checked;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmInvoicType));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.StandaredInvoice = new System.Windows.Forms.RadioButton();
		this.SimplifiedDebitNote = new System.Windows.Forms.RadioButton();
		this.StandaredDebitNote = new System.Windows.Forms.RadioButton();
		this.StandardCreditNote = new System.Windows.Forms.RadioButton();
		this.SimplifiedCreditNote = new System.Windows.Forms.RadioButton();
		this.SimplifiedInvoice = new System.Windows.Forms.RadioButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.simpliInvoic = new System.Windows.Forms.RadioButton();
		this.Invoic = new System.Windows.Forms.RadioButton();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.CkSelfInvoice = new System.Windows.Forms.CheckBox();
		this.CkExportInvoice = new System.Windows.Forms.CheckBox();
		this.CkSummaryInvoice = new System.Windows.Forms.CheckBox();
		this.CkNominalInvoice = new System.Windows.Forms.CheckBox();
		this.CkThirdParty = new System.Windows.Forms.CheckBox();
		this.BtnGo = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.StandaredInvoice);
		this.groupBox1.Controls.Add(this.SimplifiedDebitNote);
		this.groupBox1.Controls.Add(this.StandaredDebitNote);
		this.groupBox1.Controls.Add(this.StandardCreditNote);
		this.groupBox1.Controls.Add(this.SimplifiedCreditNote);
		this.groupBox1.Controls.Add(this.SimplifiedInvoice);
		this.groupBox1.Location = new System.Drawing.Point(6, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(578, 134);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.StandaredInvoice.AutoSize = true;
		this.StandaredInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.StandaredInvoice.Location = new System.Drawing.Point(14, 21);
		this.StandaredInvoice.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.StandaredInvoice.Name = "StandaredInvoice";
		this.StandaredInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.StandaredInvoice.Size = new System.Drawing.Size(222, 20);
		this.StandaredInvoice.TabIndex = 176;
		this.StandaredInvoice.Text = " فاتورة قياسية (Standared Invoice)";
		this.StandaredInvoice.UseVisualStyleBackColor = true;
		this.StandaredInvoice.CheckedChanged += new System.EventHandler(StandaredInvoice_CheckedChanged);
		this.SimplifiedDebitNote.AutoSize = true;
		this.SimplifiedDebitNote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.SimplifiedDebitNote.Location = new System.Drawing.Point(308, 58);
		this.SimplifiedDebitNote.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.SimplifiedDebitNote.Name = "SimplifiedDebitNote";
		this.SimplifiedDebitNote.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.SimplifiedDebitNote.Size = new System.Drawing.Size(263, 20);
		this.SimplifiedDebitNote.TabIndex = 175;
		this.SimplifiedDebitNote.Text = " إشعار مدين مبسط (Simplified DebitNote)";
		this.SimplifiedDebitNote.UseVisualStyleBackColor = true;
		this.SimplifiedDebitNote.CheckedChanged += new System.EventHandler(SimplifiedDebitNote_CheckedChanged);
		this.StandaredDebitNote.AutoSize = true;
		this.StandaredDebitNote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.StandaredDebitNote.Location = new System.Drawing.Point(14, 58);
		this.StandaredDebitNote.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.StandaredDebitNote.Name = "StandaredDebitNote";
		this.StandaredDebitNote.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.StandaredDebitNote.Size = new System.Drawing.Size(267, 20);
		this.StandaredDebitNote.TabIndex = 174;
		this.StandaredDebitNote.Text = "إشعار مدين قياسي (Standared DebitNote)";
		this.StandaredDebitNote.UseVisualStyleBackColor = true;
		this.StandaredDebitNote.CheckedChanged += new System.EventHandler(StandaredDebitNote_CheckedChanged);
		this.StandardCreditNote.AutoSize = true;
		this.StandardCreditNote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.StandardCreditNote.Location = new System.Drawing.Point(14, 95);
		this.StandardCreditNote.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.StandardCreditNote.Name = "StandardCreditNote";
		this.StandardCreditNote.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.StandardCreditNote.Size = new System.Drawing.Size(260, 20);
		this.StandardCreditNote.TabIndex = 173;
		this.StandardCreditNote.Text = "إشعار دائن قياسي (Standard CreditNote)";
		this.StandardCreditNote.UseVisualStyleBackColor = true;
		this.StandardCreditNote.CheckedChanged += new System.EventHandler(StandardCreditNote_CheckedChanged);
		this.SimplifiedCreditNote.AutoSize = true;
		this.SimplifiedCreditNote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.SimplifiedCreditNote.Location = new System.Drawing.Point(308, 95);
		this.SimplifiedCreditNote.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.SimplifiedCreditNote.Name = "SimplifiedCreditNote";
		this.SimplifiedCreditNote.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.SimplifiedCreditNote.Size = new System.Drawing.Size(259, 20);
		this.SimplifiedCreditNote.TabIndex = 172;
		this.SimplifiedCreditNote.Text = "إشعار دائن مبسط (Simplified CreditNote)";
		this.SimplifiedCreditNote.UseVisualStyleBackColor = true;
		this.SimplifiedCreditNote.CheckedChanged += new System.EventHandler(SimplifiedCreditNote_CheckedChanged);
		this.SimplifiedInvoice.AutoSize = true;
		this.SimplifiedInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.SimplifiedInvoice.Location = new System.Drawing.Point(308, 21);
		this.SimplifiedInvoice.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.SimplifiedInvoice.Name = "SimplifiedInvoice";
		this.SimplifiedInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.SimplifiedInvoice.Size = new System.Drawing.Size(222, 20);
		this.SimplifiedInvoice.TabIndex = 171;
		this.SimplifiedInvoice.Text = " فاتورة مبسطة (Simplified Invoice)";
		this.SimplifiedInvoice.UseVisualStyleBackColor = true;
		this.SimplifiedInvoice.CheckedChanged += new System.EventHandler(SimplifiedInvoice_CheckedChanged);
		this.groupBox2.Controls.Add(this.simpliInvoic);
		this.groupBox2.Controls.Add(this.Invoic);
		this.groupBox2.Location = new System.Drawing.Point(6, 141);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(578, 57);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.simpliInvoic.AutoSize = true;
		this.simpliInvoic.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.simpliInvoic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.simpliInvoic.Location = new System.Drawing.Point(108, 21);
		this.simpliInvoic.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.simpliInvoic.Name = "simpliInvoic";
		this.simpliInvoic.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.simpliInvoic.Size = new System.Drawing.Size(116, 22);
		this.simpliInvoic.TabIndex = 174;
		this.simpliInvoic.Text = "فاتوره مبسطه";
		this.simpliInvoic.UseVisualStyleBackColor = true;
		this.simpliInvoic.CheckedChanged += new System.EventHandler(simpliInvoic_CheckedChanged);
		this.Invoic.AutoSize = true;
		this.Invoic.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Invoic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Invoic.Location = new System.Drawing.Point(367, 21);
		this.Invoic.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Invoic.Name = "Invoic";
		this.Invoic.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Invoic.Size = new System.Drawing.Size(100, 22);
		this.Invoic.TabIndex = 173;
		this.Invoic.Text = "فاتوره عاديه";
		this.Invoic.UseVisualStyleBackColor = true;
		this.Invoic.CheckedChanged += new System.EventHandler(Invoic_CheckedChanged);
		this.groupBox3.Controls.Add(this.CkSelfInvoice);
		this.groupBox3.Controls.Add(this.CkExportInvoice);
		this.groupBox3.Controls.Add(this.CkSummaryInvoice);
		this.groupBox3.Controls.Add(this.CkNominalInvoice);
		this.groupBox3.Controls.Add(this.CkThirdParty);
		this.groupBox3.Location = new System.Drawing.Point(6, 204);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(578, 80);
		this.groupBox3.TabIndex = 2;
		this.groupBox3.TabStop = false;
		this.CkSelfInvoice.AutoSize = true;
		this.CkSelfInvoice.Location = new System.Drawing.Point(479, 48);
		this.CkSelfInvoice.Name = "CkSelfInvoice";
		this.CkSelfInvoice.Size = new System.Drawing.Size(88, 20);
		this.CkSelfInvoice.TabIndex = 5;
		this.CkSelfInvoice.Text = "فاتورة ذاتية";
		this.CkSelfInvoice.UseVisualStyleBackColor = true;
		this.CkSelfInvoice.CheckedChanged += new System.EventHandler(CkSelfInvoice_CheckedChanged);
		this.CkExportInvoice.AutoSize = true;
		this.CkExportInvoice.Location = new System.Drawing.Point(158, 22);
		this.CkExportInvoice.Name = "CkExportInvoice";
		this.CkExportInvoice.Size = new System.Drawing.Size(105, 20);
		this.CkExportInvoice.TabIndex = 3;
		this.CkExportInvoice.Text = " فاتورة ملخصة";
		this.CkExportInvoice.UseVisualStyleBackColor = true;
		this.CkSummaryInvoice.AutoSize = true;
		this.CkSummaryInvoice.Location = new System.Drawing.Point(14, 22);
		this.CkSummaryInvoice.Name = "CkSummaryInvoice";
		this.CkSummaryInvoice.Size = new System.Drawing.Size(102, 20);
		this.CkSummaryInvoice.TabIndex = 2;
		this.CkSummaryInvoice.Text = "فاتورة صادرات";
		this.CkSummaryInvoice.UseVisualStyleBackColor = true;
		this.CkSummaryInvoice.CheckedChanged += new System.EventHandler(CkSummaryInvoice_CheckedChanged);
		this.CkNominalInvoice.AutoSize = true;
		this.CkNominalInvoice.Location = new System.Drawing.Point(299, 22);
		this.CkNominalInvoice.Name = "CkNominalInvoice";
		this.CkNominalInvoice.Size = new System.Drawing.Size(103, 20);
		this.CkNominalInvoice.TabIndex = 1;
		this.CkNominalInvoice.Text = " فاتورة اسمية";
		this.CkNominalInvoice.UseVisualStyleBackColor = true;
		this.CkThirdParty.AutoSize = true;
		this.CkThirdParty.Location = new System.Drawing.Point(447, 22);
		this.CkThirdParty.Name = "CkThirdParty";
		this.CkThirdParty.Size = new System.Drawing.Size(120, 20);
		this.CkThirdParty.TabIndex = 0;
		this.CkThirdParty.Text = "فاتورة لطرف ثالث";
		this.CkThirdParty.UseVisualStyleBackColor = true;
		this.BtnGo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnGo.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnGo.Appearance.Options.UseFont = true;
		this.BtnGo.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnGo.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnGo.Location = new System.Drawing.Point(6, 290);
		this.BtnGo.Margin = new System.Windows.Forms.Padding(5);
		this.BtnGo.Name = "BtnGo";
		this.BtnGo.Size = new System.Drawing.Size(578, 39);
		this.BtnGo.TabIndex = 110;
		this.BtnGo.Text = "استمرار";
		this.BtnGo.Click += new System.EventHandler(BtnGo_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(593, 333);
		base.Controls.Add(this.BtnGo);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmInvoicType";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "اعدادت الفاتوره الضريبه";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmInvoicType_FormClosing);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		base.ResumeLayout(false);
	}
}


