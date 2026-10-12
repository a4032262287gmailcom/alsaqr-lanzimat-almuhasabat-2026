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
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.Items;

public class FrmItemReader : XtraForm
{
	private GTSdbEntities db = new GTSdbEntities();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label3;

	private Label label2;

	private Label label1;

	private ButtonEdit TxtBarcode;

	private NumericEditBox TxtNet;

	private TextEdit TxtNameItem;

	public FrmItemReader()
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
	}

	private void TxtBarcode_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			GetItem(TxtBarcode.Text);
			TxtBarcode.Text = "";
			TxtBarcode.Focus();
		}
	}

	private void GetItem(string Barcode)
	{
		try
		{
			TxtNet.Text = "0.00";
			TxtNameItem.Text = "";
			Item_Items item_Items = db.Item_Items.Where((Item_Items x) => x.Item_code == Barcode || x.SmallBarCode1 == Barcode || x.SmallBarCode2 == Barcode || x.SmallBarCode3 == Barcode).FirstOrDefault();
			if (item_Items != null)
			{
				if (item_Items.SellPriceSmall.HasValue)
				{
					if (item_Items.Is_Tax == true)
					{
						decimal value = Convert.ToDecimal(item_Items.SellPriceSmall) * Convert.ToDecimal(item_Items.Tax_Value) / 100m;
						TxtNet.Text = (item_Items.SellPriceSmall + (decimal?)value).ToString();
					}
					else
					{
						TxtNet.Text = item_Items.SellPriceSmall.ToString();
					}
				}
				if (item_Items.item_Name != null)
				{
					TxtNameItem.Text = item_Items.item_Name.ToString();
				}
			}
			Item_Items item_Items2 = db.Item_Items.Where((Item_Items x) => x.MediumBarCode1 == Barcode || x.MediumBarCode2 == Barcode || x.MediumBarCode3 == Barcode).FirstOrDefault();
			if (item_Items2 != null)
			{
				if (item_Items2.SellPriceSmall.HasValue)
				{
					if (item_Items.Is_Tax == true)
					{
						decimal value2 = Convert.ToDecimal(item_Items2.SellPriceMedium) * Convert.ToDecimal(item_Items2.Tax_Value) / 100m;
						TxtNet.Text = (item_Items2.SellPriceMedium + (decimal?)value2).ToString();
					}
					else
					{
						TxtNet.Text = item_Items.SellPriceSmall.ToString();
					}
				}
				if (item_Items2.item_Name != null)
				{
					TxtNameItem.Text = item_Items2.item_Name.ToString();
				}
			}
			Item_Items item_Items3 = db.Item_Items.Where((Item_Items x) => x.BigBarCode1 == Barcode || x.BigBarCode2 == Barcode || x.BigBarCode3 == Barcode).FirstOrDefault();
			if (item_Items3 == null)
			{
				return;
			}
			if (item_Items3.SellPriceSmall.HasValue)
			{
				TxtNet.Text = item_Items3.SellpriceLarge.ToString();
				if (item_Items.Is_Tax == true)
				{
					decimal value3 = Convert.ToDecimal(item_Items3.SellpriceLarge) * Convert.ToDecimal(item_Items3.Tax_Value) / 100m;
					TxtNet.Text = (item_Items3.SellpriceLarge + (decimal?)value3).ToString();
				}
				else
				{
					TxtNet.Text = item_Items.SellPriceSmall.ToString();
				}
			}
			if (item_Items3.item_Name != null)
			{
				TxtNameItem.Text = item_Items3.item_Name.ToString();
			}
		}
		catch (Exception)
		{
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions imageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TxtNameItem = new DevExpress.XtraEditors.TextEdit();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtBarcode = new DevExpress.XtraEditors.ButtonEdit();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNameItem.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.TxtNameItem);
		this.groupBox1.Controls.Add(this.TxtNet);
		this.groupBox1.Controls.Add(this.TxtBarcode);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(915, 273);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.TxtNameItem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNameItem.Location = new System.Drawing.Point(148, 112);
		this.TxtNameItem.Name = "TxtNameItem";
		this.TxtNameItem.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtNameItem.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtNameItem.Properties.Appearance.Options.UseBackColor = true;
		this.TxtNameItem.Properties.Appearance.Options.UseFont = true;
		this.TxtNameItem.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNameItem.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNameItem.Properties.ReadOnly = true;
		this.TxtNameItem.Size = new System.Drawing.Size(622, 46);
		this.TxtNameItem.TabIndex = 114;
		this.TxtNet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNet.DecimalDigits = 2;
		this.TxtNet.Font = new System.Drawing.Font("Tahoma", 25f);
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Location = new System.Drawing.Point(148, 202);
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNet.Size = new System.Drawing.Size(622, 58);
		this.TxtNet.TabIndex = 113;
		this.TxtNet.Text = "0.00";
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtBarcode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtBarcode.Location = new System.Drawing.Point(148, 36);
		this.TxtBarcode.Margin = new System.Windows.Forms.Padding(4);
		this.TxtBarcode.Name = "TxtBarcode";
		this.TxtBarcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtBarcode.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		this.TxtBarcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, imageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.TxtBarcode.Size = new System.Drawing.Size(622, 48);
		this.TxtBarcode.TabIndex = 6;
		this.TxtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtBarcode_KeyDown);
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 15f);
		this.label4.Location = new System.Drawing.Point(53, 48);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(88, 30);
		this.label4.TabIndex = 5;
		this.label4.Text = "Search";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 15f);
		this.label5.Location = new System.Drawing.Point(16, 209);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(126, 30);
		this.label5.TabIndex = 4;
		this.label5.Text = "Item Price";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 15f);
		this.label6.Location = new System.Drawing.Point(6, 123);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(136, 30);
		this.label6.TabIndex = 3;
		this.label6.Text = "Item Name";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 15f);
		this.label3.Location = new System.Drawing.Point(777, 48);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(75, 30);
		this.label3.TabIndex = 2;
		this.label3.Text = "البحث";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 15f);
		this.label2.Location = new System.Drawing.Point(775, 209);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(132, 30);
		this.label2.TabIndex = 1;
		this.label2.Text = "سعر الصنف";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 15f);
		this.label1.Location = new System.Drawing.Point(775, 123);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(133, 30);
		this.label1.TabIndex = 0;
		this.label1.Text = "اسم الصنف";
		base.Appearance.BackColor = System.Drawing.Color.White;
		base.Appearance.Options.UseBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(939, 297);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmItemReader";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "البحث / Search";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNameItem.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcode.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


