using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.FrmOthers;

public class FrmRepair : XtraForm
{
	private int tafqitID = LoginDetails.TafqitID;

	public string TxtSamkraTafqit;

	public string TxtMikanikaTafqit;

	public string TxtElectricTafqit;

	public string TxtNetTafqit;

	private List<CurrencyInfo> currencies = new List<CurrencyInfo>();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label KJJKHJ;

	private TextEdit TxtCustmoer;

	private TextEdit TxtColor;

	private TextEdit TxtCarNum;

	private TextEdit TxtModel;

	private Label label4;

	private Label label3;

	private TextEdit TxtType;

	private Label label2;

	private Label label1;

	private TextBox TxtNote;

	private Label label5;

	private GroupBox groupBox2;

	private Label label6;

	private Label label8;

	private Label label10;

	private Label label11;

	private TextEdit TxtNet;

	private TextEdit TxtMikanika;

	private TextEdit TxtElectric;

	private TextEdit TxtSamkra;

	private GroupBox groupBox3;

	private TextEdit Txt16;

	private Label label25;

	private Label label24;

	private Label label23;

	private Label label22;

	private Label label21;

	private Label label20;

	private Label label19;

	private Label label18;

	private Label label17;

	private Label label16;

	private Label label15;

	private Label label14;

	private Label label12;

	private Label label9;

	private Label label7;

	private TextEdit textEdit16;

	private TextEdit Txt15;

	private TextEdit Txt14;

	private TextEdit Txt13;

	private TextEdit Txt12;

	private TextEdit Txt11;

	private TextEdit Txt10;

	private TextEdit Txt9;

	private TextEdit Txt8;

	private TextEdit Txt7;

	private TextEdit Txt6;

	private TextEdit Txt5;

	private TextEdit Txt4;

	private TextEdit Txt3;

	private TextEdit Txt2;

	private TextEdit Txt1;

	private Label label13;

	private SimpleButton BtnPrint;

	private SimpleButton BtnNew;

	private SimpleButton BtnExit;

	public FrmRepair()
	{
		InitializeComponent();
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
		currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TxtSamkra_EditValueChanged(object sender, EventArgs e)
	{
		TxtNet.Text = (Convert.ToDecimal(TxtSamkra.Text) + Convert.ToDecimal(TxtMikanika.Text) + Convert.ToDecimal(TxtElectric.Text)).ToString();
		if (Convert.ToDecimal(TxtSamkra.Text) > 0m)
		{
			ToWord toWord = new ToWord(Convert.ToDecimal(TxtSamkra.Text), currencies[tafqitID]);
			TxtSamkraTafqit = toWord.ConvertToArabic();
		}
		if (Convert.ToDecimal(TxtMikanika.Text) > 0m)
		{
			ToWord toWord2 = new ToWord(Convert.ToDecimal(TxtMikanika.Text), currencies[tafqitID]);
			TxtMikanikaTafqit = toWord2.ConvertToArabic();
		}
		if (Convert.ToDecimal(TxtElectric.Text) > 0m)
		{
			ToWord toWord3 = new ToWord(Convert.ToDecimal(TxtElectric.Text), currencies[tafqitID]);
			TxtElectricTafqit = toWord3.ConvertToArabic();
		}
		if (Convert.ToDecimal(TxtNet.Text) > 0m)
		{
			ToWord toWord4 = new ToWord(Convert.ToDecimal(TxtNet.Text), currencies[tafqitID]);
			TxtNetTafqit = toWord4.ConvertToArabic();
		}
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(TxtCustmoer.Text))
		{
			StiReport stiReport = new StiReport();
			stiReport.Load(Application.StartupPath + "\\Reports\\Print_Repair.mrt");
			stiReport.Dictionary.Synchronize();
			stiReport.Compile();
			stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
			stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
			stiReport["VatNumComp"] = LoginDetails.VatNum;
			stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
			stiReport["Address"] = LoginDetails.Address;
			stiReport["Phone"] = LoginDetails.Phone;
			stiReport["Bank"] = LoginDetails.CompanyBank;
			stiReport["TxtCustmoer"] = TxtCustmoer.Text;
			stiReport["TxtCarNum"] = TxtCarNum.Text;
			stiReport["TxtColor"] = TxtColor.Text;
			stiReport["TxtType"] = TxtType.Text;
			stiReport["TxtModel"] = TxtModel.Text;
			stiReport["TxtNote"] = TxtNote.Text;
			stiReport["TxtSamkra"] = TxtSamkra.Text;
			stiReport["TxtElectric"] = TxtElectric.Text;
			stiReport["TxtMikanika"] = TxtMikanika.Text;
			stiReport["TxtNet"] = TxtNet.Text;
			stiReport["TxtSamkraTafqit"] = TxtSamkraTafqit;
			stiReport["TxtMikanikaTafqit"] = TxtMikanikaTafqit;
			stiReport["TxtElectricTafqit"] = TxtElectricTafqit;
			stiReport["TxtNetTafqit"] = TxtNetTafqit;
			stiReport["Txt1"] = Txt1.Text;
			stiReport["Txt2"] = Txt2.Text;
			stiReport["Txt3"] = Txt3.Text;
			stiReport["Txt4"] = Txt4.Text;
			stiReport["Txt5"] = Txt5.Text;
			stiReport["Txt6"] = Txt6.Text;
			stiReport["Txt7"] = Txt7.Text;
			stiReport["Txt8"] = Txt8.Text;
			stiReport["Txt9"] = Txt9.Text;
			stiReport["Txt10"] = Txt10.Text;
			stiReport["Txt11"] = Txt11.Text;
			stiReport["Txt12"] = Txt12.Text;
			stiReport["Txt13"] = Txt13.Text;
			stiReport["Txt14"] = Txt14.Text;
			stiReport["Txt15"] = Txt15.Text;
			stiReport["Txt16"] = Txt16.Text;
			stiReport.Render();
			stiReport.Show(dialogForm: true);
		}
		else
		{
			MessageBox.Show("عفوا، سجل اسم العميل ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		TxtCustmoer.Text = "";
		TxtCarNum.Text = "";
		TxtColor.Text = "";
		TxtType.Text = "";
		TxtModel.Text = "";
		TxtNote.Text = "";
		TxtMikanika.Text = "0.00";
		TxtSamkra.Text = "0.00";
		TxtElectric.Text = "0.00";
		TxtMikanika.Text = "0.00";
		TxtNet.Text = "0.00";
		Txt1.Text = "";
		Txt2.Text = "";
		Txt3.Text = "";
		Txt4.Text = "";
		Txt5.Text = "";
		Txt6.Text = "";
		Txt7.Text = "";
		Txt8.Text = "";
		Txt9.Text = "";
		Txt10.Text = "";
		Txt11.Text = "";
		Txt12.Text = "";
		Txt13.Text = "";
		Txt14.Text = "";
		Txt15.Text = "";
		Txt16.Text = "";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmOthers.FrmRepair));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.KJJKHJ = new System.Windows.Forms.Label();
		this.TxtCustmoer = new DevExpress.XtraEditors.TextEdit();
		this.TxtCarNum = new DevExpress.XtraEditors.TextEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtType = new DevExpress.XtraEditors.TextEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtModel = new DevExpress.XtraEditors.TextEdit();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtColor = new DevExpress.XtraEditors.TextEdit();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.TxtSamkra = new DevExpress.XtraEditors.TextEdit();
		this.TxtElectric = new DevExpress.XtraEditors.TextEdit();
		this.TxtMikanika = new DevExpress.XtraEditors.TextEdit();
		this.TxtNet = new DevExpress.XtraEditors.TextEdit();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label13 = new System.Windows.Forms.Label();
		this.Txt1 = new DevExpress.XtraEditors.TextEdit();
		this.Txt2 = new DevExpress.XtraEditors.TextEdit();
		this.Txt4 = new DevExpress.XtraEditors.TextEdit();
		this.Txt3 = new DevExpress.XtraEditors.TextEdit();
		this.Txt6 = new DevExpress.XtraEditors.TextEdit();
		this.Txt5 = new DevExpress.XtraEditors.TextEdit();
		this.Txt8 = new DevExpress.XtraEditors.TextEdit();
		this.Txt7 = new DevExpress.XtraEditors.TextEdit();
		this.textEdit16 = new DevExpress.XtraEditors.TextEdit();
		this.Txt15 = new DevExpress.XtraEditors.TextEdit();
		this.Txt14 = new DevExpress.XtraEditors.TextEdit();
		this.Txt13 = new DevExpress.XtraEditors.TextEdit();
		this.Txt12 = new DevExpress.XtraEditors.TextEdit();
		this.Txt11 = new DevExpress.XtraEditors.TextEdit();
		this.Txt10 = new DevExpress.XtraEditors.TextEdit();
		this.Txt9 = new DevExpress.XtraEditors.TextEdit();
		this.label7 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.Txt16 = new DevExpress.XtraEditors.TextEdit();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCustmoer.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCarNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtType.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtModel.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtColor.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtSamkra.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtElectric.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMikanika.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNet.Properties).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Txt1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt4.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt3.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt6.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt5.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt8.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt7.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.textEdit16.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt15.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt14.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt13.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt12.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt11.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt10.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt9.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txt16.Properties).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.TxtColor);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.TxtCarNum);
		this.groupBox1.Controls.Add(this.TxtModel);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.TxtType);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtCustmoer);
		this.groupBox1.Controls.Add(this.KJJKHJ);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(922, 221);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.KJJKHJ.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.KJJKHJ.AutoSize = true;
		this.KJJKHJ.Font = new System.Drawing.Font("Tahoma", 10f);
		this.KJJKHJ.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.KJJKHJ.Location = new System.Drawing.Point(822, 36);
		this.KJJKHJ.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.KJJKHJ.Name = "KJJKHJ";
		this.KJJKHJ.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.KJJKHJ.Size = new System.Drawing.Size(93, 21);
		this.KJJKHJ.TabIndex = 106;
		this.KJJKHJ.Text = "اسم العميل";
		this.TxtCustmoer.Location = new System.Drawing.Point(12, 33);
		this.TxtCustmoer.Margin = new System.Windows.Forms.Padding(9, 2, 9, 2);
		this.TxtCustmoer.Name = "TxtCustmoer";
		this.TxtCustmoer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCustmoer.Properties.Appearance.Options.UseFont = true;
		this.TxtCustmoer.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCustmoer.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCustmoer.Size = new System.Drawing.Size(797, 28);
		this.TxtCustmoer.TabIndex = 194;
		this.TxtCarNum.Location = new System.Drawing.Point(469, 65);
		this.TxtCarNum.Margin = new System.Windows.Forms.Padding(11, 2, 11, 2);
		this.TxtCarNum.Name = "TxtCarNum";
		this.TxtCarNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCarNum.Properties.Appearance.Options.UseFont = true;
		this.TxtCarNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCarNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCarNum.Size = new System.Drawing.Size(340, 28);
		this.TxtCarNum.TabIndex = 196;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label1.Location = new System.Drawing.Point(824, 68);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label1.Size = new System.Drawing.Size(84, 21);
		this.label1.TabIndex = 195;
		this.label1.Text = "رقم اللوحة";
		this.TxtType.Location = new System.Drawing.Point(12, 65);
		this.TxtType.Margin = new System.Windows.Forms.Padding(14, 2, 14, 2);
		this.TxtType.Name = "TxtType";
		this.TxtType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtType.Properties.Appearance.Options.UseFont = true;
		this.TxtType.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtType.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtType.Size = new System.Drawing.Size(340, 28);
		this.TxtType.TabIndex = 198;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(370, 68);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label2.Size = new System.Drawing.Size(43, 21);
		this.label2.TabIndex = 197;
		this.label2.Text = "النوع";
		this.TxtModel.Location = new System.Drawing.Point(469, 97);
		this.TxtModel.Margin = new System.Windows.Forms.Padding(14, 2, 14, 2);
		this.TxtModel.Name = "TxtModel";
		this.TxtModel.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtModel.Properties.Appearance.Options.UseFont = true;
		this.TxtModel.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtModel.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtModel.Size = new System.Drawing.Size(340, 28);
		this.TxtModel.TabIndex = 200;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(827, 100);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label3.Size = new System.Drawing.Size(62, 21);
		this.label3.TabIndex = 199;
		this.label3.Text = "الموديل";
		this.TxtColor.Location = new System.Drawing.Point(12, 97);
		this.TxtColor.Margin = new System.Windows.Forms.Padding(14, 2, 14, 2);
		this.TxtColor.Name = "TxtColor";
		this.TxtColor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtColor.Properties.Appearance.Options.UseFont = true;
		this.TxtColor.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtColor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtColor.Size = new System.Drawing.Size(340, 28);
		this.TxtColor.TabIndex = 202;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label4.Location = new System.Drawing.Point(370, 100);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label4.Size = new System.Drawing.Size(44, 21);
		this.label4.TabIndex = 201;
		this.label4.Text = "اللون";
		this.TxtNote.Location = new System.Drawing.Point(12, 129);
		this.TxtNote.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtNote.Multiline = true;
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtNote.Size = new System.Drawing.Size(797, 78);
		this.TxtNote.TabIndex = 204;
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(822, 128);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label5.Size = new System.Drawing.Size(73, 21);
		this.label5.TabIndex = 203;
		this.label5.Text = "ملاحظات";
		this.groupBox2.Controls.Add(this.TxtNet);
		this.groupBox2.Controls.Add(this.TxtMikanika);
		this.groupBox2.Controls.Add(this.TxtElectric);
		this.groupBox2.Controls.Add(this.TxtSamkra);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Location = new System.Drawing.Point(12, 239);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(922, 70);
		this.groupBox2.TabIndex = 205;
		this.groupBox2.TabStop = false;
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(351, 25);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label8.Size = new System.Drawing.Size(74, 21);
		this.label8.TabIndex = 199;
		this.label8.Text = "الميكانيكا";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label10.Location = new System.Drawing.Point(594, 25);
		this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label10.Name = "label10";
		this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label10.Size = new System.Drawing.Size(60, 21);
		this.label10.TabIndex = 195;
		this.label10.Text = "الكهرباء";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label11.Location = new System.Drawing.Point(819, 25);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label11.Size = new System.Drawing.Size(76, 21);
		this.label11.TabIndex = 106;
		this.label11.Text = "السمكرة ";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(165, 25);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label6.Size = new System.Drawing.Size(22, 21);
		this.label6.TabIndex = 203;
		this.label6.Text = "=";
		this.TxtSamkra.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSamkra.EditValue = "0.00";
		this.TxtSamkra.Location = new System.Drawing.Point(664, 22);
		this.TxtSamkra.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
		this.TxtSamkra.Name = "TxtSamkra";
		this.TxtSamkra.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSamkra.Properties.Appearance.Options.UseFont = true;
		this.TxtSamkra.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSamkra.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSamkra.Size = new System.Drawing.Size(145, 28);
		this.TxtSamkra.TabIndex = 204;
		this.TxtSamkra.EditValueChanged += new System.EventHandler(TxtSamkra_EditValueChanged);
		this.TxtElectric.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtElectric.EditValue = "0.00";
		this.TxtElectric.Location = new System.Drawing.Point(437, 22);
		this.TxtElectric.Margin = new System.Windows.Forms.Padding(8, 2, 8, 2);
		this.TxtElectric.Name = "TxtElectric";
		this.TxtElectric.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtElectric.Properties.Appearance.Options.UseFont = true;
		this.TxtElectric.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtElectric.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtElectric.Size = new System.Drawing.Size(145, 28);
		this.TxtElectric.TabIndex = 206;
		this.TxtElectric.EditValueChanged += new System.EventHandler(TxtSamkra_EditValueChanged);
		this.TxtMikanika.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtMikanika.EditValue = "0.00";
		this.TxtMikanika.Location = new System.Drawing.Point(194, 22);
		this.TxtMikanika.Margin = new System.Windows.Forms.Padding(8, 2, 8, 2);
		this.TxtMikanika.Name = "TxtMikanika";
		this.TxtMikanika.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtMikanika.Properties.Appearance.Options.UseFont = true;
		this.TxtMikanika.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtMikanika.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtMikanika.Size = new System.Drawing.Size(145, 28);
		this.TxtMikanika.TabIndex = 208;
		this.TxtMikanika.EditValueChanged += new System.EventHandler(TxtSamkra_EditValueChanged);
		this.TxtNet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNet.EditValue = "0.00";
		this.TxtNet.Location = new System.Drawing.Point(12, 22);
		this.TxtNet.Margin = new System.Windows.Forms.Padding(10, 2, 10, 2);
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNet.Properties.Appearance.Options.UseFont = true;
		this.TxtNet.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNet.Size = new System.Drawing.Size(145, 28);
		this.TxtNet.TabIndex = 209;
		this.groupBox3.Controls.Add(this.Txt16);
		this.groupBox3.Controls.Add(this.label25);
		this.groupBox3.Controls.Add(this.label24);
		this.groupBox3.Controls.Add(this.label23);
		this.groupBox3.Controls.Add(this.label22);
		this.groupBox3.Controls.Add(this.label21);
		this.groupBox3.Controls.Add(this.label20);
		this.groupBox3.Controls.Add(this.label19);
		this.groupBox3.Controls.Add(this.label18);
		this.groupBox3.Controls.Add(this.label17);
		this.groupBox3.Controls.Add(this.label16);
		this.groupBox3.Controls.Add(this.label15);
		this.groupBox3.Controls.Add(this.label14);
		this.groupBox3.Controls.Add(this.label12);
		this.groupBox3.Controls.Add(this.label9);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.textEdit16);
		this.groupBox3.Controls.Add(this.Txt15);
		this.groupBox3.Controls.Add(this.Txt14);
		this.groupBox3.Controls.Add(this.Txt13);
		this.groupBox3.Controls.Add(this.Txt12);
		this.groupBox3.Controls.Add(this.Txt11);
		this.groupBox3.Controls.Add(this.Txt10);
		this.groupBox3.Controls.Add(this.Txt9);
		this.groupBox3.Controls.Add(this.Txt8);
		this.groupBox3.Controls.Add(this.Txt7);
		this.groupBox3.Controls.Add(this.Txt6);
		this.groupBox3.Controls.Add(this.Txt5);
		this.groupBox3.Controls.Add(this.Txt4);
		this.groupBox3.Controls.Add(this.Txt3);
		this.groupBox3.Controls.Add(this.Txt2);
		this.groupBox3.Controls.Add(this.Txt1);
		this.groupBox3.Controls.Add(this.label13);
		this.groupBox3.Location = new System.Drawing.Point(12, 315);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(922, 305);
		this.groupBox3.TabIndex = 210;
		this.groupBox3.TabStop = false;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label13.Location = new System.Drawing.Point(819, 36);
		this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label13.Name = "label13";
		this.label13.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label13.Size = new System.Drawing.Size(53, 21);
		this.label13.TabIndex = 106;
		this.label13.Text = "قطع 1";
		this.Txt1.Location = new System.Drawing.Point(469, 33);
		this.Txt1.Margin = new System.Windows.Forms.Padding(14, 2, 14, 2);
		this.Txt1.Name = "Txt1";
		this.Txt1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt1.Properties.Appearance.Options.UseFont = true;
		this.Txt1.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt1.Size = new System.Drawing.Size(340, 28);
		this.Txt1.TabIndex = 197;
		this.Txt2.Location = new System.Drawing.Point(469, 65);
		this.Txt2.Margin = new System.Windows.Forms.Padding(18, 2, 18, 2);
		this.Txt2.Name = "Txt2";
		this.Txt2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt2.Properties.Appearance.Options.UseFont = true;
		this.Txt2.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt2.Size = new System.Drawing.Size(340, 28);
		this.Txt2.TabIndex = 198;
		this.Txt4.Location = new System.Drawing.Point(469, 129);
		this.Txt4.Margin = new System.Windows.Forms.Padding(22, 2, 22, 2);
		this.Txt4.Name = "Txt4";
		this.Txt4.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt4.Properties.Appearance.Options.UseFont = true;
		this.Txt4.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt4.Size = new System.Drawing.Size(340, 28);
		this.Txt4.TabIndex = 200;
		this.Txt3.Location = new System.Drawing.Point(469, 97);
		this.Txt3.Margin = new System.Windows.Forms.Padding(18, 2, 18, 2);
		this.Txt3.Name = "Txt3";
		this.Txt3.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt3.Properties.Appearance.Options.UseFont = true;
		this.Txt3.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt3.Size = new System.Drawing.Size(340, 28);
		this.Txt3.TabIndex = 199;
		this.Txt6.Location = new System.Drawing.Point(469, 193);
		this.Txt6.Margin = new System.Windows.Forms.Padding(28, 2, 28, 2);
		this.Txt6.Name = "Txt6";
		this.Txt6.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt6.Properties.Appearance.Options.UseFont = true;
		this.Txt6.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt6.Size = new System.Drawing.Size(340, 28);
		this.Txt6.TabIndex = 202;
		this.Txt5.Location = new System.Drawing.Point(469, 161);
		this.Txt5.Margin = new System.Windows.Forms.Padding(22, 2, 22, 2);
		this.Txt5.Name = "Txt5";
		this.Txt5.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt5.Properties.Appearance.Options.UseFont = true;
		this.Txt5.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt5.Size = new System.Drawing.Size(340, 28);
		this.Txt5.TabIndex = 201;
		this.Txt8.Location = new System.Drawing.Point(469, 257);
		this.Txt8.Margin = new System.Windows.Forms.Padding(35, 2, 35, 2);
		this.Txt8.Name = "Txt8";
		this.Txt8.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt8.Properties.Appearance.Options.UseFont = true;
		this.Txt8.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt8.Size = new System.Drawing.Size(340, 28);
		this.Txt8.TabIndex = 204;
		this.Txt7.Location = new System.Drawing.Point(469, 225);
		this.Txt7.Margin = new System.Windows.Forms.Padding(28, 2, 28, 2);
		this.Txt7.Name = "Txt7";
		this.Txt7.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt7.Properties.Appearance.Options.UseFont = true;
		this.Txt7.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt7.Size = new System.Drawing.Size(340, 28);
		this.Txt7.TabIndex = 203;
		this.textEdit16.Location = new System.Drawing.Point(15, 313);
		this.textEdit16.Margin = new System.Windows.Forms.Padding(44, 2, 44, 2);
		this.textEdit16.Name = "textEdit16";
		this.textEdit16.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.textEdit16.Properties.Appearance.Options.UseFont = true;
		this.textEdit16.Properties.Appearance.Options.UseTextOptions = true;
		this.textEdit16.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.textEdit16.Size = new System.Drawing.Size(337, 28);
		this.textEdit16.TabIndex = 212;
		this.Txt15.Location = new System.Drawing.Point(15, 225);
		this.Txt15.Margin = new System.Windows.Forms.Padding(35, 2, 35, 2);
		this.Txt15.Name = "Txt15";
		this.Txt15.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt15.Properties.Appearance.Options.UseFont = true;
		this.Txt15.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt15.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt15.Size = new System.Drawing.Size(337, 28);
		this.Txt15.TabIndex = 211;
		this.Txt14.Location = new System.Drawing.Point(15, 193);
		this.Txt14.Margin = new System.Windows.Forms.Padding(35, 2, 35, 2);
		this.Txt14.Name = "Txt14";
		this.Txt14.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt14.Properties.Appearance.Options.UseFont = true;
		this.Txt14.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt14.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt14.Size = new System.Drawing.Size(337, 28);
		this.Txt14.TabIndex = 210;
		this.Txt13.Location = new System.Drawing.Point(15, 161);
		this.Txt13.Margin = new System.Windows.Forms.Padding(28, 2, 28, 2);
		this.Txt13.Name = "Txt13";
		this.Txt13.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt13.Properties.Appearance.Options.UseFont = true;
		this.Txt13.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt13.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt13.Size = new System.Drawing.Size(337, 28);
		this.Txt13.TabIndex = 209;
		this.Txt12.Location = new System.Drawing.Point(15, 129);
		this.Txt12.Margin = new System.Windows.Forms.Padding(28, 2, 28, 2);
		this.Txt12.Name = "Txt12";
		this.Txt12.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt12.Properties.Appearance.Options.UseFont = true;
		this.Txt12.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt12.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt12.Size = new System.Drawing.Size(337, 28);
		this.Txt12.TabIndex = 208;
		this.Txt11.Location = new System.Drawing.Point(15, 97);
		this.Txt11.Margin = new System.Windows.Forms.Padding(22, 2, 22, 2);
		this.Txt11.Name = "Txt11";
		this.Txt11.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt11.Properties.Appearance.Options.UseFont = true;
		this.Txt11.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt11.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt11.Size = new System.Drawing.Size(337, 28);
		this.Txt11.TabIndex = 207;
		this.Txt10.Location = new System.Drawing.Point(15, 65);
		this.Txt10.Margin = new System.Windows.Forms.Padding(22, 2, 22, 2);
		this.Txt10.Name = "Txt10";
		this.Txt10.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt10.Properties.Appearance.Options.UseFont = true;
		this.Txt10.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt10.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt10.Size = new System.Drawing.Size(337, 28);
		this.Txt10.TabIndex = 206;
		this.Txt9.Location = new System.Drawing.Point(15, 33);
		this.Txt9.Margin = new System.Windows.Forms.Padding(18, 2, 18, 2);
		this.Txt9.Name = "Txt9";
		this.Txt9.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt9.Properties.Appearance.Options.UseFont = true;
		this.Txt9.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt9.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt9.Size = new System.Drawing.Size(337, 28);
		this.Txt9.TabIndex = 205;
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label7.Location = new System.Drawing.Point(361, 260);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label7.Size = new System.Drawing.Size(62, 21);
		this.label7.TabIndex = 213;
		this.label7.Text = "قطع 16";
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label9.Location = new System.Drawing.Point(361, 228);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label9.Size = new System.Drawing.Size(62, 21);
		this.label9.TabIndex = 214;
		this.label9.Text = "قطع 15";
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label12.Location = new System.Drawing.Point(361, 196);
		this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label12.Name = "label12";
		this.label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label12.Size = new System.Drawing.Size(62, 21);
		this.label12.TabIndex = 215;
		this.label12.Text = "قطع 14";
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label14.Location = new System.Drawing.Point(361, 164);
		this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label14.Name = "label14";
		this.label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label14.Size = new System.Drawing.Size(62, 21);
		this.label14.TabIndex = 216;
		this.label14.Text = "قطع 13";
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label15.Location = new System.Drawing.Point(361, 132);
		this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label15.Name = "label15";
		this.label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label15.Size = new System.Drawing.Size(62, 21);
		this.label15.TabIndex = 217;
		this.label15.Text = "قطع 12";
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label16.Location = new System.Drawing.Point(361, 100);
		this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label16.Name = "label16";
		this.label16.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label16.Size = new System.Drawing.Size(62, 21);
		this.label16.TabIndex = 218;
		this.label16.Text = "قطع 11";
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label17.Location = new System.Drawing.Point(361, 68);
		this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label17.Name = "label17";
		this.label17.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label17.Size = new System.Drawing.Size(62, 21);
		this.label17.TabIndex = 219;
		this.label17.Text = "قطع 10";
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label18.Location = new System.Drawing.Point(361, 36);
		this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label18.Name = "label18";
		this.label18.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label18.Size = new System.Drawing.Size(53, 21);
		this.label18.TabIndex = 220;
		this.label18.Text = "قطع 9";
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label19.Location = new System.Drawing.Point(819, 260);
		this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label19.Name = "label19";
		this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label19.Size = new System.Drawing.Size(53, 21);
		this.label19.TabIndex = 221;
		this.label19.Text = "قطع 8";
		this.label20.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label20.Location = new System.Drawing.Point(819, 228);
		this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label20.Name = "label20";
		this.label20.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label20.Size = new System.Drawing.Size(53, 21);
		this.label20.TabIndex = 222;
		this.label20.Text = "قطع 7";
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label21.Location = new System.Drawing.Point(819, 196);
		this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label21.Name = "label21";
		this.label21.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label21.Size = new System.Drawing.Size(53, 21);
		this.label21.TabIndex = 223;
		this.label21.Text = "قطع 6";
		this.label22.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label22.Location = new System.Drawing.Point(819, 164);
		this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label22.Name = "label22";
		this.label22.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label22.Size = new System.Drawing.Size(53, 21);
		this.label22.TabIndex = 224;
		this.label22.Text = "قطع 5";
		this.label23.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label23.Location = new System.Drawing.Point(819, 132);
		this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label23.Name = "label23";
		this.label23.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label23.Size = new System.Drawing.Size(53, 21);
		this.label23.TabIndex = 225;
		this.label23.Text = "قطع 4";
		this.label24.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label24.Location = new System.Drawing.Point(819, 100);
		this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label24.Name = "label24";
		this.label24.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label24.Size = new System.Drawing.Size(53, 21);
		this.label24.TabIndex = 226;
		this.label24.Text = "قطع 3";
		this.label25.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label25.Location = new System.Drawing.Point(819, 68);
		this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label25.Name = "label25";
		this.label25.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label25.Size = new System.Drawing.Size(53, 21);
		this.label25.TabIndex = 227;
		this.label25.Text = "قطع 2";
		this.Txt16.Location = new System.Drawing.Point(15, 257);
		this.Txt16.Margin = new System.Windows.Forms.Padding(44, 2, 44, 2);
		this.Txt16.Name = "Txt16";
		this.Txt16.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txt16.Properties.Appearance.Options.UseFont = true;
		this.Txt16.Properties.Appearance.Options.UseTextOptions = true;
		this.Txt16.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txt16.Size = new System.Drawing.Size(337, 28);
		this.Txt16.TabIndex = 228;
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(429, 633);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(120, 37);
		this.BtnPrint.TabIndex = 214;
		this.BtnPrint.Text = "طباعة Ctrl P";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(297, 633);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(120, 37);
		this.BtnNew.TabIndex = 215;
		this.BtnNew.Text = "جديد F4";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(561, 633);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(120, 37);
		this.BtnExit.TabIndex = 216;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(946, 685);
		base.Controls.Add(this.BtnPrint);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.MaximizeBox = false;
		base.Name = "FrmRepair";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "الاصلاح";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCustmoer.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCarNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtType.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtModel.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtColor.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtSamkra.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtElectric.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMikanika.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNet.Properties).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.Txt1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt4.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt3.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt6.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt5.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt8.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt7.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.textEdit16.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt15.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt14.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt13.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt12.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt11.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt10.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt9.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txt16.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


