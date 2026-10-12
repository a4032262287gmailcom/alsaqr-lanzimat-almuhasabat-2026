using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Items.FrmSearch;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Items;

public class FrmOilChange : XtraForm
{
	private Class_Items Items = new Class_Items();

	private IContainer components = null;

	private GroupBox groupBox23;

	private Label lblItemID;

	private Label lblCode;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	private GroupBox groupBox24;

	public ButtonEdit TxtItemCode;

	private Label label16;

	private DateEdit TxtDate;

	private Label label21;

	private Label label2;

	private Label label1;

	private Label label43;

	private TextEdit TxtNote;

	private TextEdit TxtEndCount;

	private TextEdit TxtStartCount;

	private Label label3;

	private TextEdit TxtCounter;

	public FrmOilChange()
	{
		InitializeComponent();
		TxtDate.DateTime = DateTime.Now.Date;
		TxtStartCount.Text = "0";
		TxtCounter.Text = "5000";
		TxtEndCount.Text = "5000";
	}

	private void TxtItemCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		LoginDetails.ISCheckSearchItem = true;
		FrmSearchItems1 frmSearchItems = new FrmSearchItems1();
		frmSearchItems.ShowDialog();
		if (LoginDetails.ItemSearchID <= 0)
		{
			return;
		}
		foreach (Item_Items item in Items.GetItemsByitemid(LoginDetails.ItemSearchID))
		{
			lblCode.Text = item.Item_code;
			lblItemID.Text = item.ItemId.ToString();
			TxtItemCode.Text = item.item_Name;
		}
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void Print()
	{
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.Tables["ChangeOil"].Clear();
		StiReport stiReport = new StiReport();
		stiReport.Load(Application.StartupPath + "\\Reports\\RptOil.mrt");
		stiReport.RegData(dataSetALL.ChangeOil);
		stiReport.Dictionary.Synchronize();
		stiReport.Compile();
		stiReport["CompanyName"] = LoginDetails.CompanyNameAR;
		stiReport["ItemName"] = TxtItemCode.Text;
		stiReport["Date"] = Convert.ToDateTime(TxtDate.Text);
		stiReport["StartCounter"] = TxtStartCount.Text;
		stiReport["EndCounter"] = TxtEndCount.Text;
		stiReport["Notes"] = TxtNote.Text;
		stiReport.Render();
		stiReport.Show(dialogForm: true);
		stiReport.Print();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TxtCounter_TextChanged(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(TxtCounter.Text))
		{
			TxtEndCount.Text = (Convert.ToInt64(TxtCounter.Text) + Convert.ToInt64(TxtStartCount.Text)).ToString();
		}
	}

	private void TxtCounter_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void TxtEndCount_TextChanged(object sender, EventArgs e)
	{
	}

	private void TxtStartCount_TextChanged(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(TxtCounter.Text))
		{
			TxtEndCount.Text = (Convert.ToInt64(TxtCounter.Text) + Convert.ToInt64(TxtStartCount.Text)).ToString();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Items.FrmOilChange));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox23 = new System.Windows.Forms.GroupBox();
		this.lblItemID = new System.Windows.Forms.Label();
		this.lblCode = new System.Windows.Forms.Label();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox24 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label43 = new System.Windows.Forms.Label();
		this.TxtNote = new DevExpress.XtraEditors.TextEdit();
		this.TxtEndCount = new DevExpress.XtraEditors.TextEdit();
		this.TxtStartCount = new DevExpress.XtraEditors.TextEdit();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.TxtItemCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label16 = new System.Windows.Forms.Label();
		this.TxtCounter = new DevExpress.XtraEditors.TextEdit();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox23.SuspendLayout();
		this.groupBox24.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtEndCount.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtStartCount.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCounter.Properties).BeginInit();
		base.SuspendLayout();
		this.groupBox23.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox23.Controls.Add(this.lblItemID);
		this.groupBox23.Controls.Add(this.lblCode);
		this.groupBox23.Controls.Add(this.BtnContinue);
		this.groupBox23.Controls.Add(this.BtnExit);
		this.groupBox23.Location = new System.Drawing.Point(12, 181);
		this.groupBox23.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox23.Name = "groupBox23";
		this.groupBox23.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox23.Size = new System.Drawing.Size(450, 61);
		this.groupBox23.TabIndex = 7;
		this.groupBox23.TabStop = false;
		this.lblItemID.AutoSize = true;
		this.lblItemID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblItemID.Location = new System.Drawing.Point(387, 37);
		this.lblItemID.Name = "lblItemID";
		this.lblItemID.Size = new System.Drawing.Size(13, 13);
		this.lblItemID.TabIndex = 11;
		this.lblItemID.Text = "1";
		this.lblItemID.Visible = false;
		this.lblCode.AutoSize = true;
		this.lblCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblCode.Location = new System.Drawing.Point(387, 19);
		this.lblCode.Name = "lblCode";
		this.lblCode.Size = new System.Drawing.Size(13, 13);
		this.lblCode.TabIndex = 7;
		this.lblCode.Text = "1";
		this.lblCode.Visible = false;
		this.BtnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnContinue.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Location = new System.Drawing.Point(227, 19);
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Size = new System.Drawing.Size(143, 32);
		this.BtnContinue.TabIndex = 10;
		this.BtnContinue.Text = "استمرار";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(21, 19);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(143, 32);
		this.BtnExit.TabIndex = 9;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.groupBox24.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox24.Controls.Add(this.label2);
		this.groupBox24.Controls.Add(this.label1);
		this.groupBox24.Controls.Add(this.label3);
		this.groupBox24.Controls.Add(this.label43);
		this.groupBox24.Controls.Add(this.TxtNote);
		this.groupBox24.Controls.Add(this.TxtEndCount);
		this.groupBox24.Controls.Add(this.TxtCounter);
		this.groupBox24.Controls.Add(this.TxtStartCount);
		this.groupBox24.Controls.Add(this.TxtDate);
		this.groupBox24.Controls.Add(this.label21);
		this.groupBox24.Controls.Add(this.TxtItemCode);
		this.groupBox24.Controls.Add(this.label16);
		this.groupBox24.Location = new System.Drawing.Point(12, 11);
		this.groupBox24.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox24.Name = "groupBox24";
		this.groupBox24.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox24.Size = new System.Drawing.Size(450, 166);
		this.groupBox24.TabIndex = 6;
		this.groupBox24.TabStop = false;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(377, 138);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(47, 17);
		this.label2.TabIndex = 205;
		this.label2.Text = "المرجع";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label1.Location = new System.Drawing.Point(170, 108);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(72, 17);
		this.label1.TabIndex = 205;
		this.label1.Text = "نهاية العداد";
		this.label43.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label43.AutoSize = true;
		this.label43.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label43.Location = new System.Drawing.Point(377, 109);
		this.label43.Name = "label43";
		this.label43.Size = new System.Drawing.Size(71, 17);
		this.label43.TabIndex = 205;
		this.label43.Text = "بداية العداد";
		this.TxtNote.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNote.Location = new System.Drawing.Point(20, 136);
		this.TxtNote.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNote.Properties.Appearance.Options.UseFont = true;
		this.TxtNote.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNote.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNote.Size = new System.Drawing.Size(350, 22);
		this.TxtNote.TabIndex = 204;
		this.TxtEndCount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtEndCount.Enabled = false;
		this.TxtEndCount.Location = new System.Drawing.Point(21, 106);
		this.TxtEndCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtEndCount.Name = "TxtEndCount";
		this.TxtEndCount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtEndCount.Properties.Appearance.Options.UseFont = true;
		this.TxtEndCount.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtEndCount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtEndCount.Size = new System.Drawing.Size(143, 22);
		this.TxtEndCount.TabIndex = 204;
		this.TxtEndCount.TextChanged += new System.EventHandler(TxtEndCount_TextChanged);
		this.TxtEndCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtCounter_KeyPress);
		this.TxtStartCount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtStartCount.Location = new System.Drawing.Point(248, 106);
		this.TxtStartCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtStartCount.Name = "TxtStartCount";
		this.TxtStartCount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtStartCount.Properties.Appearance.Options.UseFont = true;
		this.TxtStartCount.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtStartCount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtStartCount.Size = new System.Drawing.Size(123, 22);
		this.TxtStartCount.TabIndex = 204;
		this.TxtStartCount.TextChanged += new System.EventHandler(TxtStartCount_TextChanged);
		this.TxtStartCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtCounter_KeyPress);
		this.TxtDate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDate.EditValue = null;
		this.TxtDate.Location = new System.Drawing.Point(21, 49);
		this.TxtDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5f);
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
		this.TxtDate.Properties.Mask.EditMask = "yyyy/MM/dd";
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate.Size = new System.Drawing.Size(349, 24);
		this.TxtDate.TabIndex = 202;
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label21.Location = new System.Drawing.Point(377, 52);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(42, 16);
		this.label21.TabIndex = 203;
		this.label21.Text = "التاريخ";
		this.TxtItemCode.Location = new System.Drawing.Point(21, 18);
		this.TxtItemCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtItemCode.Name = "TxtItemCode";
		this.TxtItemCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtItemCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtItemCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtItemCode.Properties.Appearance.Options.UseFont = true;
		this.TxtItemCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtItemCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtItemCode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtItemCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions1.Image");
		this.TxtItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.TxtItemCode.Properties.ReadOnly = true;
		this.TxtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtItemCode.Size = new System.Drawing.Size(350, 26);
		this.TxtItemCode.TabIndex = 6;
		this.TxtItemCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtItemCode_ButtonClick);
		this.label16.AutoSize = true;
		this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label16.Location = new System.Drawing.Point(377, 24);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(61, 13);
		this.label16.TabIndex = 0;
		this.label16.Text = "اسم الصنف";
		this.TxtCounter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCounter.Location = new System.Drawing.Point(21, 78);
		this.TxtCounter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtCounter.Name = "TxtCounter";
		this.TxtCounter.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCounter.Properties.Appearance.Options.UseFont = true;
		this.TxtCounter.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCounter.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCounter.Size = new System.Drawing.Size(350, 22);
		this.TxtCounter.TabIndex = 204;
		this.TxtCounter.TextChanged += new System.EventHandler(TxtCounter_TextChanged);
		this.TxtCounter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtCounter_KeyPress);
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(374, 81);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(75, 17);
		this.label3.TabIndex = 205;
		this.label3.Text = "قيمة الزيادة";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(470, 264);
		base.Controls.Add(this.groupBox23);
		base.Controls.Add(this.groupBox24);
		base.Name = "FrmOilChange";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "غيار الزيت";
		this.groupBox23.ResumeLayout(false);
		this.groupBox23.PerformLayout();
		this.groupBox24.ResumeLayout(false);
		this.groupBox24.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNote.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtEndCount.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtStartCount.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCounter.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


