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
using GTSErpSystem.DataSetRep;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Items.FrmSearch;

public class FrmPrintBarcode : XtraForm
{
	public string BracodeItem;

	private IContainer components = null;

	private GroupBox groupBox1;

	private ButtonEdit TxtBarcodeSmall3;

	private Label label40;

	private ButtonEdit TxtBarcodeSmall1;

	private Label label34;

	private ButtonEdit TxtBarcodeSmall2;

	private Label label39;

	private Label label2;

	private SeparatorControl separatorControl1;

	private ButtonEdit TxtItemName;

	public ButtonEdit TxtItemCode;

	private Label label5;

	private Label label1;

	private SeparatorControl separatorControl6;

	private Label label8;

	private ButtonEdit TxtBarcodeLarg3;

	private Label label9;

	private ButtonEdit TxtBarcodeLarg1;

	private Label label10;

	private ButtonEdit TxtBarcodeLarg2;

	private Label label11;

	private Label label3;

	private SeparatorControl separatorControl2;

	private ButtonEdit TxtBarcodeMeduim3;

	private Label label4;

	private ButtonEdit TxtBarcodeMeduim1;

	private Label label6;

	private ButtonEdit TxtBarcodeMeduim2;

	private Label label7;

	private NumericUpDown NPrice1unit3;

	private Label label13;

	private NumericUpDown NPrice1unit2;

	private Label label12;

	private NumericUpDown NPrice1unit1;

	private Label label20;

	private Label label14;

	private Label label16;

	private ButtonEdit TxtCompany;

	private Label label17;

	private Label label15;

	public FrmPrintBarcode()
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
		BindTxt(LoginDetails.ItemCode);
	}

	private void Print(string Company, string ItemName, string Barcode, decimal Price)
	{
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.BarcodeItem.AddBarcodeItemRow("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
		StiReport stiReport = new StiReport();
		stiReport.Load(Application.StartupPath + "\\Reports\\RptBarcode.mrt");
		stiReport.RegData(dataSetALL.BarcodeItem);
		stiReport.Dictionary.Synchronize();
		stiReport.Compile();
		stiReport["Company"] = Company;
		stiReport["ItemName"] = ItemName;
		stiReport["Barcode"] = Barcode;
		stiReport["Price"] = Price.ToString();
		stiReport.Render();
		stiReport.Print();
	}

	private void BindTxt(string Code)
	{
		if (!(Code != ""))
		{
			return;
		}
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		Select_BarCode_Result select_BarCode_Result = gTSdbEntities.Select_BarCode(Code).FirstOrDefault();
		if (select_BarCode_Result != null)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			string text8 = "";
			string text9 = "";
			string text10 = "";
			string text11 = "";
			decimal value = 0m;
			decimal value2 = 0m;
			decimal value3 = 0m;
			if (select_BarCode_Result.Item_Code != null)
			{
				text = select_BarCode_Result.Item_Code;
			}
			if (select_BarCode_Result.Item_Name != null)
			{
				text2 = select_BarCode_Result.Item_Name;
			}
			if (select_BarCode_Result.SmallBarCode1 != null)
			{
				text3 = select_BarCode_Result.SmallBarCode1;
			}
			if (select_BarCode_Result.SmallBarCode2 != null)
			{
				text4 = select_BarCode_Result.SmallBarCode2;
			}
			if (select_BarCode_Result.SmallBarCode3 != null)
			{
				text5 = select_BarCode_Result.SmallBarCode3;
			}
			if (select_BarCode_Result.MediumBarCode1 != null)
			{
				text6 = select_BarCode_Result.MediumBarCode1;
			}
			if (select_BarCode_Result.MediumBarCode2 != null)
			{
				text7 = select_BarCode_Result.MediumBarCode2;
			}
			if (select_BarCode_Result.MediumBarCode3 != null)
			{
				text8 = select_BarCode_Result.MediumBarCode3;
			}
			if (select_BarCode_Result.BigBarCode1 != null)
			{
				text9 = select_BarCode_Result.BigBarCode1;
			}
			if (select_BarCode_Result.BigBarCode2 != null)
			{
				text10 = select_BarCode_Result.BigBarCode2;
			}
			if (select_BarCode_Result.BigBarCode3 != null)
			{
				text11 = select_BarCode_Result.BigBarCode3;
			}
			if (select_BarCode_Result.SmallSellPriceWithVat.HasValue)
			{
				value = Convert.ToDecimal(select_BarCode_Result.SmallSellPriceWithVat);
			}
			if (select_BarCode_Result.SellPriceMediumWithVat.HasValue)
			{
				value2 = Convert.ToDecimal(select_BarCode_Result.SellPriceMediumWithVat);
			}
			if (select_BarCode_Result.SellpriceLargeWithVat.HasValue)
			{
				value3 = Convert.ToDecimal(select_BarCode_Result.SellpriceLargeWithVat);
			}
			TxtItemCode.Text = text;
			TxtItemName.Text = text2;
			TxtBarcodeSmall1.Text = text3;
			TxtBarcodeSmall2.Text = text4;
			TxtBarcodeSmall3.Text = text5;
			TxtBarcodeMeduim1.Text = text6;
			TxtBarcodeMeduim2.Text = text7;
			TxtBarcodeMeduim3.Text = text8;
			TxtBarcodeLarg1.Text = text9;
			TxtBarcodeLarg2.Text = text10;
			TxtBarcodeLarg3.Text = text11;
			NPrice1unit1.Value = value;
			NPrice1unit2.Value = value2;
			NPrice1unit3.Value = value3;
			TxtCompany.Text = LoginDetails.NamePrintFatora;
		}
	}

	private void TxtBarcodeSmall1_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (TxtBarcodeSmall1.Text != "" && NPrice1unit1.Value > 0m)
		{
			Print(TxtCompany.Text, TxtItemName.Text, TxtBarcodeSmall1.Text, NPrice1unit1.Value);
			return;
		}
		MessageBox.Show("عفوا، من فضلك تأكد من الباركود ظˆ من سعر الصنف ثم حاول مرة أخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		TxtBarcodeSmall1.Focus();
	}

	private void TxtBarcodeSmall2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (TxtBarcodeSmall2.Text != "" && NPrice1unit1.Value > 0m)
		{
			Print(TxtCompany.Text, TxtItemName.Text, TxtBarcodeSmall2.Text, NPrice1unit1.Value);
			return;
		}
		MessageBox.Show("عفوا، من فضلك تأكد من الباركود ظˆ من سعر الصنف ثم حاول مرة أخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		TxtBarcodeSmall2.Focus();
	}

	private void TxtBarcodeSmall3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (TxtBarcodeSmall3.Text != "" && NPrice1unit1.Value > 0m)
		{
			Print(TxtCompany.Text, TxtItemName.Text, TxtBarcodeSmall3.Text, NPrice1unit1.Value);
			return;
		}
		MessageBox.Show("عفوا، من فضلك تأكد من الباركود ظˆ من سعر الصنف ثم حاول مرة أخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		TxtBarcodeSmall3.Focus();
	}

	private void TxtBarcodeMeduim1_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (TxtBarcodeMeduim1.Text != "" && NPrice1unit2.Value > 0m)
		{
			Print(TxtCompany.Text, TxtItemName.Text, TxtBarcodeMeduim1.Text, NPrice1unit2.Value);
			return;
		}
		MessageBox.Show("عفوا، من فضلك تأكد من الباركود ظˆ من سعر الصنف ثم حاول مرة أخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		TxtBarcodeMeduim1.Focus();
	}

	private void TxtBarcodeMeduim2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (TxtBarcodeMeduim2.Text != "" && NPrice1unit2.Value > 0m)
		{
			Print(TxtCompany.Text, TxtItemName.Text, TxtBarcodeMeduim2.Text, NPrice1unit2.Value);
			return;
		}
		MessageBox.Show("عفوا، من فضلك تأكد من الباركود ظˆ من سعر الصنف ثم حاول مرة أخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		TxtBarcodeMeduim2.Focus();
	}

	private void TxtBarcodeMeduim3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (TxtBarcodeMeduim3.Text != "" && NPrice1unit2.Value > 0m)
		{
			Print(TxtCompany.Text, TxtItemName.Text, TxtBarcodeMeduim3.Text, NPrice1unit2.Value);
			return;
		}
		MessageBox.Show("عفوا، من فضلك تأكد من الباركود ظˆ من سعر الصنف ثم حاول مرة أخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		TxtBarcodeMeduim3.Focus();
	}

	private void TxtBarcodeLarg1_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (TxtBarcodeLarg1.Text != "" && NPrice1unit3.Value > 0m)
		{
			Print(TxtCompany.Text, TxtItemName.Text, TxtBarcodeLarg1.Text, NPrice1unit3.Value);
			return;
		}
		MessageBox.Show("عفوا، من فضلك تأكد من الباركود ظˆ من سعر الصنف ثم حاول مرة أخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		TxtBarcodeLarg1.Focus();
	}

	private void TxtBarcodeLarg2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (TxtBarcodeLarg2.Text != "" && NPrice1unit3.Value > 0m)
		{
			Print(TxtCompany.Text, TxtItemName.Text, TxtBarcodeLarg2.Text, NPrice1unit3.Value);
			return;
		}
		MessageBox.Show("عفوا، من فضلك تأكد من الباركود ظˆ من سعر الصنف ثم حاول مرة أخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		TxtBarcodeLarg2.Focus();
	}

	private void TxtBarcodeLarg3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (TxtBarcodeLarg3.Text != "" && NPrice1unit3.Value > 0m)
		{
			Print(TxtCompany.Text, TxtItemName.Text, TxtBarcodeLarg3.Text, NPrice1unit3.Value);
			return;
		}
		MessageBox.Show("عفوا، من فضلك تأكد من الباركود ظˆ من سعر الصنف ثم حاول مرة أخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		TxtBarcodeLarg3.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Items.FrmSearch.FrmPrintBarcode));
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions5 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions6 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions7 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions8 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions9 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions10 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions11 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions12 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance12 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered12 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed12 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled12 = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label17 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtCompany = new DevExpress.XtraEditors.ButtonEdit();
		this.label16 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.NPrice1unit3 = new System.Windows.Forms.NumericUpDown();
		this.label13 = new System.Windows.Forms.Label();
		this.NPrice1unit2 = new System.Windows.Forms.NumericUpDown();
		this.label12 = new System.Windows.Forms.Label();
		this.NPrice1unit1 = new System.Windows.Forms.NumericUpDown();
		this.label20 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtBarcodeLarg3 = new DevExpress.XtraEditors.ButtonEdit();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtBarcodeLarg1 = new DevExpress.XtraEditors.ButtonEdit();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtBarcodeLarg2 = new DevExpress.XtraEditors.ButtonEdit();
		this.label11 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
		this.TxtBarcodeMeduim3 = new DevExpress.XtraEditors.ButtonEdit();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtBarcodeMeduim1 = new DevExpress.XtraEditors.ButtonEdit();
		this.label6 = new System.Windows.Forms.Label();
		this.TxtBarcodeMeduim2 = new DevExpress.XtraEditors.ButtonEdit();
		this.label7 = new System.Windows.Forms.Label();
		this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
		this.TxtItemName = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtItemCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label5 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.separatorControl6 = new DevExpress.XtraEditors.SeparatorControl();
		this.TxtBarcodeSmall3 = new DevExpress.XtraEditors.ButtonEdit();
		this.label40 = new System.Windows.Forms.Label();
		this.TxtBarcodeSmall1 = new DevExpress.XtraEditors.ButtonEdit();
		this.label34 = new System.Windows.Forms.Label();
		this.TxtBarcodeSmall2 = new DevExpress.XtraEditors.ButtonEdit();
		this.label39 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCompany.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeLarg3.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeLarg1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeLarg2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMeduim3.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMeduim1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMeduim2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall3.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall2.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.TxtCompany);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.NPrice1unit3);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.NPrice1unit2);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.NPrice1unit1);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.TxtBarcodeLarg3);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.TxtBarcodeLarg1);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.TxtBarcodeLarg2);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.separatorControl2);
		this.groupBox1.Controls.Add(this.TxtBarcodeMeduim3);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.TxtBarcodeMeduim1);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.TxtBarcodeMeduim2);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.separatorControl1);
		this.groupBox1.Controls.Add(this.TxtItemName);
		this.groupBox1.Controls.Add(this.TxtItemCode);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.separatorControl6);
		this.groupBox1.Controls.Add(this.TxtBarcodeSmall3);
		this.groupBox1.Controls.Add(this.label40);
		this.groupBox1.Controls.Add(this.TxtBarcodeSmall1);
		this.groupBox1.Controls.Add(this.label34);
		this.groupBox1.Controls.Add(this.TxtBarcodeSmall2);
		this.groupBox1.Controls.Add(this.label39);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.label17, "label17");
		this.label17.Name = "label17";
		resources.ApplyResources(this.label15, "label15");
		this.label15.Name = "label15";
		resources.ApplyResources(this.TxtCompany, "TxtCompany");
		this.TxtCompany.Name = "TxtCompany";
		this.TxtCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtCompany.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCompany.Properties.Appearance.Font");
		this.TxtCompany.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCompany.Properties.Appearance.Options.UseFont = true;
		this.TxtCompany.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCompany.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCompany.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.TxtCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtCompany.Properties.Buttons"), resources.GetString("TxtCompany.Properties.Buttons1"), (int)resources.GetObject("TxtCompany.Properties.Buttons2"), (bool)resources.GetObject("TxtCompany.Properties.Buttons3"), (bool)resources.GetObject("TxtCompany.Properties.Buttons4"), (bool)resources.GetObject("TxtCompany.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtCompany.Properties.Buttons6"), resources.GetObject("TxtCompany.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtCompany.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtCompany.Properties.Buttons9"))
		});
		resources.ApplyResources(this.label16, "label16");
		this.label16.Name = "label16";
		resources.ApplyResources(this.label14, "label14");
		this.label14.Name = "label14";
		resources.ApplyResources(this.NPrice1unit3, "NPrice1unit3");
		this.NPrice1unit3.DecimalPlaces = 2;
		this.NPrice1unit3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPrice1unit3.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.NPrice1unit3.Name = "NPrice1unit3";
		resources.ApplyResources(this.label13, "label13");
		this.label13.Name = "label13";
		resources.ApplyResources(this.NPrice1unit2, "NPrice1unit2");
		this.NPrice1unit2.DecimalPlaces = 2;
		this.NPrice1unit2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPrice1unit2.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.NPrice1unit2.Name = "NPrice1unit2";
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.NPrice1unit1, "NPrice1unit1");
		this.NPrice1unit1.DecimalPlaces = 2;
		this.NPrice1unit1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPrice1unit1.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.NPrice1unit1.Name = "NPrice1unit1";
		resources.ApplyResources(this.label20, "label20");
		this.label20.Name = "label20";
		resources.ApplyResources(this.label8, "label8");
		this.label8.Cursor = System.Windows.Forms.Cursors.Default;
		this.label8.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.label8.Name = "label8";
		resources.ApplyResources(this.TxtBarcodeLarg3, "TxtBarcodeLarg3");
		this.TxtBarcodeLarg3.Name = "TxtBarcodeLarg3";
		this.TxtBarcodeLarg3.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtBarcodeLarg3.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeLarg3.Properties.Appearance.Font");
		this.TxtBarcodeLarg3.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBarcodeLarg3.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeLarg3.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeLarg3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeLarg3.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeLarg3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.TxtBarcodeLarg3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeLarg3.Properties.Buttons"), resources.GetString("TxtBarcodeLarg3.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeLarg3.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeLarg3.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeLarg3.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeLarg3.Properties.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("TxtBarcodeLarg3.Properties.Buttons6"), resources.GetObject("TxtBarcodeLarg3.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeLarg3.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeLarg3.Properties.Buttons9"))
		});
		this.TxtBarcodeLarg3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeLarg3_ButtonClick);
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.TxtBarcodeLarg1, "TxtBarcodeLarg1");
		this.TxtBarcodeLarg1.Name = "TxtBarcodeLarg1";
		this.TxtBarcodeLarg1.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtBarcodeLarg1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeLarg1.Properties.Appearance.Font");
		this.TxtBarcodeLarg1.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBarcodeLarg1.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeLarg1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeLarg1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeLarg1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeLarg1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		this.TxtBarcodeLarg1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeLarg1.Properties.Buttons"), resources.GetString("TxtBarcodeLarg1.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeLarg1.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeLarg1.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeLarg1.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeLarg1.Properties.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("TxtBarcodeLarg1.Properties.Buttons6"), resources.GetObject("TxtBarcodeLarg1.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeLarg1.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeLarg1.Properties.Buttons9"))
		});
		this.TxtBarcodeLarg1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeLarg1_ButtonClick);
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.TxtBarcodeLarg2, "TxtBarcodeLarg2");
		this.TxtBarcodeLarg2.Name = "TxtBarcodeLarg2";
		this.TxtBarcodeLarg2.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtBarcodeLarg2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeLarg2.Properties.Appearance.Font");
		this.TxtBarcodeLarg2.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBarcodeLarg2.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeLarg2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeLarg2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeLarg2.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeLarg2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.TxtBarcodeLarg2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeLarg2.Properties.Buttons"), resources.GetString("TxtBarcodeLarg2.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeLarg2.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeLarg2.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeLarg2.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeLarg2.Properties.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("TxtBarcodeLarg2.Properties.Buttons6"), resources.GetObject("TxtBarcodeLarg2.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeLarg2.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeLarg2.Properties.Buttons9"))
		});
		this.TxtBarcodeLarg2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeLarg2_ButtonClick);
		resources.ApplyResources(this.label11, "label11");
		this.label11.Name = "label11";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Cursor = System.Windows.Forms.Cursors.Default;
		this.label3.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.label3.Name = "label3";
		resources.ApplyResources(this.separatorControl2, "separatorControl2");
		this.separatorControl2.LineThickness = 1;
		this.separatorControl2.Name = "separatorControl2";
		resources.ApplyResources(this.TxtBarcodeMeduim3, "TxtBarcodeMeduim3");
		this.TxtBarcodeMeduim3.Name = "TxtBarcodeMeduim3";
		this.TxtBarcodeMeduim3.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtBarcodeMeduim3.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeMeduim3.Properties.Appearance.Font");
		this.TxtBarcodeMeduim3.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBarcodeMeduim3.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeMeduim3.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeMeduim3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeMeduim3.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeMeduim3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions5");
		this.TxtBarcodeMeduim3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeMeduim3.Properties.Buttons"), resources.GetString("TxtBarcodeMeduim3.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeMeduim3.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeMeduim3.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeMeduim3.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeMeduim3.Properties.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("TxtBarcodeMeduim3.Properties.Buttons6"), resources.GetObject("TxtBarcodeMeduim3.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeMeduim3.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeMeduim3.Properties.Buttons9"))
		});
		this.TxtBarcodeMeduim3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeMeduim3_ButtonClick);
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.TxtBarcodeMeduim1, "TxtBarcodeMeduim1");
		this.TxtBarcodeMeduim1.Name = "TxtBarcodeMeduim1";
		this.TxtBarcodeMeduim1.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtBarcodeMeduim1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeMeduim1.Properties.Appearance.Font");
		this.TxtBarcodeMeduim1.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBarcodeMeduim1.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeMeduim1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeMeduim1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeMeduim1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeMeduim1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions6");
		this.TxtBarcodeMeduim1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeMeduim1.Properties.Buttons"), resources.GetString("TxtBarcodeMeduim1.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeMeduim1.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeMeduim1.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeMeduim1.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeMeduim1.Properties.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("TxtBarcodeMeduim1.Properties.Buttons6"), resources.GetObject("TxtBarcodeMeduim1.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeMeduim1.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeMeduim1.Properties.Buttons9"))
		});
		this.TxtBarcodeMeduim1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeMeduim1_ButtonClick);
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.TxtBarcodeMeduim2, "TxtBarcodeMeduim2");
		this.TxtBarcodeMeduim2.Name = "TxtBarcodeMeduim2";
		this.TxtBarcodeMeduim2.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtBarcodeMeduim2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeMeduim2.Properties.Appearance.Font");
		this.TxtBarcodeMeduim2.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBarcodeMeduim2.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeMeduim2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeMeduim2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeMeduim2.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeMeduim2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions7, "editorButtonImageOptions7");
		this.TxtBarcodeMeduim2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeMeduim2.Properties.Buttons"), resources.GetString("TxtBarcodeMeduim2.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeMeduim2.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeMeduim2.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeMeduim2.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeMeduim2.Properties.Buttons5"), editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, resources.GetString("TxtBarcodeMeduim2.Properties.Buttons6"), resources.GetObject("TxtBarcodeMeduim2.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeMeduim2.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeMeduim2.Properties.Buttons9"))
		});
		this.TxtBarcodeMeduim2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeMeduim2_ButtonClick);
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.separatorControl1, "separatorControl1");
		this.separatorControl1.LineThickness = 1;
		this.separatorControl1.Name = "separatorControl1";
		resources.ApplyResources(this.TxtItemName, "TxtItemName");
		this.TxtItemName.Name = "TxtItemName";
		this.TxtItemName.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtItemName.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtItemName.Properties.Appearance.Font");
		this.TxtItemName.Properties.Appearance.Options.UseBackColor = true;
		this.TxtItemName.Properties.Appearance.Options.UseFont = true;
		this.TxtItemName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtItemName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtItemName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions8");
		this.TxtItemName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtItemName.Properties.Buttons"), resources.GetString("TxtItemName.Properties.Buttons1"), (int)resources.GetObject("TxtItemName.Properties.Buttons2"), (bool)resources.GetObject("TxtItemName.Properties.Buttons3"), (bool)resources.GetObject("TxtItemName.Properties.Buttons4"), (bool)resources.GetObject("TxtItemName.Properties.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("TxtItemName.Properties.Buttons6"), resources.GetObject("TxtItemName.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtItemName.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtItemName.Properties.Buttons9"))
		});
		resources.ApplyResources(this.TxtItemCode, "TxtItemCode");
		this.TxtItemCode.Name = "TxtItemCode";
		this.TxtItemCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtItemCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtItemCode.Properties.Appearance.Font");
		this.TxtItemCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtItemCode.Properties.Appearance.Options.UseFont = true;
		this.TxtItemCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtItemCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtItemCode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtItemCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions9, "editorButtonImageOptions9");
		this.TxtItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtItemCode.Properties.Buttons"), resources.GetString("TxtItemCode.Properties.Buttons1"), (int)resources.GetObject("TxtItemCode.Properties.Buttons2"), (bool)resources.GetObject("TxtItemCode.Properties.Buttons3"), (bool)resources.GetObject("TxtItemCode.Properties.Buttons4"), (bool)resources.GetObject("TxtItemCode.Properties.Buttons5"), editorButtonImageOptions9, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance9, appearanceHovered9, appearancePressed9, appearanceDisabled9, resources.GetString("TxtItemCode.Properties.Buttons6"), resources.GetObject("TxtItemCode.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtItemCode.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtItemCode.Properties.Buttons9"))
		});
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Cursor = System.Windows.Forms.Cursors.Default;
		this.label2.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.label2.Name = "label2";
		resources.ApplyResources(this.separatorControl6, "separatorControl6");
		this.separatorControl6.LineThickness = 1;
		this.separatorControl6.Name = "separatorControl6";
		resources.ApplyResources(this.TxtBarcodeSmall3, "TxtBarcodeSmall3");
		this.TxtBarcodeSmall3.Name = "TxtBarcodeSmall3";
		this.TxtBarcodeSmall3.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtBarcodeSmall3.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeSmall3.Properties.Appearance.Font");
		this.TxtBarcodeSmall3.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBarcodeSmall3.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeSmall3.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeSmall3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeSmall3.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeSmall3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions10, "editorButtonImageOptions10");
		this.TxtBarcodeSmall3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons"), resources.GetString("TxtBarcodeSmall3.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons5"), editorButtonImageOptions10, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance10, appearanceHovered10, appearancePressed10, appearanceDisabled10, resources.GetString("TxtBarcodeSmall3.Properties.Buttons6"), resources.GetObject("TxtBarcodeSmall3.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons9"))
		});
		this.TxtBarcodeSmall3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeSmall3_ButtonClick);
		resources.ApplyResources(this.label40, "label40");
		this.label40.Name = "label40";
		resources.ApplyResources(this.TxtBarcodeSmall1, "TxtBarcodeSmall1");
		this.TxtBarcodeSmall1.Name = "TxtBarcodeSmall1";
		this.TxtBarcodeSmall1.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtBarcodeSmall1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeSmall1.Properties.Appearance.Font");
		this.TxtBarcodeSmall1.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBarcodeSmall1.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeSmall1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeSmall1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeSmall1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeSmall1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions11, "editorButtonImageOptions11");
		this.TxtBarcodeSmall1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons"), resources.GetString("TxtBarcodeSmall1.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons5"), editorButtonImageOptions11, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance11, appearanceHovered11, appearancePressed11, appearanceDisabled11, resources.GetString("TxtBarcodeSmall1.Properties.Buttons6"), resources.GetObject("TxtBarcodeSmall1.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons9"))
		});
		this.TxtBarcodeSmall1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeSmall1_ButtonClick);
		resources.ApplyResources(this.label34, "label34");
		this.label34.Name = "label34";
		resources.ApplyResources(this.TxtBarcodeSmall2, "TxtBarcodeSmall2");
		this.TxtBarcodeSmall2.Name = "TxtBarcodeSmall2";
		this.TxtBarcodeSmall2.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtBarcodeSmall2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeSmall2.Properties.Appearance.Font");
		this.TxtBarcodeSmall2.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBarcodeSmall2.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeSmall2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeSmall2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeSmall2.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeSmall2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions12, "editorButtonImageOptions12");
		this.TxtBarcodeSmall2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons"), resources.GetString("TxtBarcodeSmall2.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons5"), editorButtonImageOptions12, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance12, appearanceHovered12, appearancePressed12, appearanceDisabled12, resources.GetString("TxtBarcodeSmall2.Properties.Buttons6"), resources.GetObject("TxtBarcodeSmall2.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons9"))
		});
		this.TxtBarcodeSmall2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeSmall2_ButtonClick);
		resources.ApplyResources(this.label39, "label39");
		this.label39.Name = "label39";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.IconOptions.ShowIcon = false;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmPrintBarcode";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCompany.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeLarg3.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeLarg1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeLarg2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMeduim3.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMeduim1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMeduim2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall3.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall2.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


