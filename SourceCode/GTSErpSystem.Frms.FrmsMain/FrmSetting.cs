using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.ElctorncInvoic;
using GTSErpSystem.Helper;
using WhatsAppApi;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmSetting : XtraForm
{
	private Class_Setting Userx = new Class_Setting();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private string folderPath = "";

	private bool Vat;

	private bool TotalWithVat;

	private bool ImageShape;

	private int InstallAll;

	private MailMessage message;

	private SmtpClient smtp;

	private IContainer components = null;

	private GroupBox groupBox1;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	private XtraTabControl TapPrice;

	private XtraTabPage xtraTabPage1;

	private TextBox TxtCompanyAR;

	private TextBox TxtCompanyEN;

	private Label label8;

	private TextBox TxtVatNum;

	private Label label1;

	private Label label2;

	private XtraTabPage xtraTabPage2;

	private XtraTabPage xtraTabPage3;

	private CheckBox checkVat;

	private System.Windows.Forms.ComboBox combStore;

	private Label label3;

	private Label label16;

	private System.Windows.Forms.ComboBox comboTafqit;

	private System.Windows.Forms.ComboBox CBStoreWaiting;

	private Label label4;

	private ColorEdit CEGroupColor;

	private Label label10;

	private ColorEdit CEGroupFontColor;

	private Label label9;

	private ColorEdit CEItemFontColor;

	private Label label5;

	private NumericUpDown CEItemSize;

	private Label label7;

	private ColorEdit CEItemColor;

	private Label label6;

	private NumericUpDown CEGroupSize;

	private Label label20;

	private System.Windows.Forms.ComboBox CBItemNameEN;

	private CheckBox CBActiveNameEN;

	private System.Windows.Forms.ComboBox CBItemPrice;

	private CheckBox CBActivePrice;

	private System.Windows.Forms.ComboBox CBItemNameAR;

	private CheckBox CBActiveNameAR;

	private CheckBox CBTabocco;

	private CheckBox CBActiveImage;

	private SeparatorControl separatorControl1;

	private TextBox TxtWeb;

	private Label label18;

	private TextBox TxtFax;

	private Label label17;

	private TextBox TxtAddress;

	private Label label15;

	private TextBox TxtMobile;

	private Label label14;

	private TextBox TxtPhone;

	private Label label13;

	private TextBox TxtCommercialRegister;

	private Label label12;

	private GroupBox groupBox2;

	private CheckBox CBBackup;

	private Label label11;

	private ButtonEdit TxtBackup;

	private TextBox TxtBank;

	private Label label19;

	private Label label22;

	private TextBox TxtNamePrintFatora;

	private NumericUpDown TxtDecimal;

	private Label label23;

	private XtraTabPage xtraTabPage4;

	private GroupBox groupBox5;

	private CheckBox CBPrintReturnOrder;

	private CheckBox CBPrintOrder;

	private CheckBox CBPrintCashier;

	private GroupBox groupBox4;

	private RadioButton RBDiscountDecimal;

	private RadioButton RBDiscountDecimalForCustomer;

	private RadioButton RBNoDiscountDecimal;

	private Label label25;

	private Label label24;

	private NumericUpDown TxtDiscount;

	private CheckBox CBIsSecureCashier;

	private Label label26;

	private TextBox TxtPassWordSupervisor;

	private System.Windows.Forms.ComboBox combStorePurches;

	private Label label27;

	private Label label28;

	private Label label29;

	private Label label30;

	private Label label31;

	private TextBox TxtCity;

	private TextBox TxtDistrict;

	private Label label33;

	private TextBox TxtStreetNum;

	private Label label34;

	private TextBox TxtBuildingNum;

	private TextBox TxtAdditionalNum;

	private Label label35;

	private TextBox TxtPostalCode;

	private TextBox TxtCountry;

	private NumericUpDown TxtPerVat;

	private Label label36;

	private XtraTabPage xtraTabPage5;

	private GroupBox groupBox6;

	private CheckBox ckRemember;

	private DateEdit TxtDateRem;

	private Label label32;

	private DataGridView DgvRemember;

	private SimpleButton BtnDeleteRem;

	private SimpleButton BtnEditRem;

	private SimpleButton BtnSaveRem;

	private TextBox TxtRemember;

	private Label label37;

	private SimpleButton BtnNewRem;

	private CheckBox CBOrderStore;

	private CheckBox CKGabr;

	private ToggleSwitch BtnMezan;

	private NumericUpDown TxtDecimalQty;

	private Label label38;

	private SimpleButton BtnCopyMac;

	private Label Connect;

	private Label lblcoo;

	private CheckBox IsTimeInRestaurant;

	private CheckBox checkItemTotalWithVat;

	private CheckBox CBIsPrintItemCook;

	private NumericUpDown TobaccoValue;

	private Label label39;

	private NumericUpDown TxtFontSize;

	private Label label40;

	private System.Windows.Forms.ComboBox ComVat;

	private Label label41;

	private CheckBox CkBouns;

	private System.Windows.Forms.ComboBox ComImageShape;

	private CheckBox CkDisCode;

	private Label label42;

	private System.Windows.Forms.ComboBox ComInstall;

	private CheckBox ElcotroncInvCK;

	private Label label43;

	private System.Windows.Forms.ComboBox CBScheme;

	private TextBox TxtNumSchma;

	private Label label44;

	private CheckBox CBIsPrintSecondFatoraCashier;

	private CheckBox CBIsResturant;

	private CheckBox CBIsPrintItemCook1;

	private CheckBox Ckglass;

	private Label label45;

	private CheckBox CKIsShowRoomInFormRestaurant;

	private CheckBox CBISTakeDateApp;

	private NumericUpDown TxtPerCashir;

	private CheckBox CKBondTax;

	private CheckBox CkHasm;

	private Label label21;

	private System.Windows.Forms.ComboBox comboPrint;

	private Label label46;

	private System.Windows.Forms.ComboBox ComMizan;

	private CheckBox CkBarCodeRest;

	private CheckBox CkShowFatora;

	private XtraTabPage xtraTabPage6;

	private GroupBox groupBox3;

	private TextBox TxtPhoneSent;

	private Label label47;

	private Label label48;

	private Label label49;

	private TextBox TxtAccountSid;

	private TextBox TxtToken;

	private SimpleButton BtnNew;

	private SimpleButton BtnSid;

	private SimpleButton BtnPastTocken;

	private CheckBox CKShowRoom;

	private CheckBox CkAlItemInRow;

	private NumericUpDown TxtPerGrid;

	private Label label50;

	public FrmSetting()
	{
		InitializeComponent();
		ComBox();
		TxtDateRem.DateTime = DateTime.Now;
		Vats();
		BindTxt();
		TxtAccountSid.ContextMenu = new ContextMenu();
		TxtToken.ContextMenu = new ContextMenu();
	}

	private void ComBox()
	{
		ComVat.Items.Add("بدون ضريبه");
		ComVat.Items.Add("السعر غير شامل الضريبه");
		ComVat.Items.Add("السعر شامل الضريبه ");
		ComImageShape.Items.Add("صوره كامله");
		ComImageShape.Items.Add("صوره جانبيه (نص_نص)");
		ComInstall.Items.Add("بدون تركيب");
		ComInstall.Items.Add("تركيب فردي");
		ComInstall.Items.Add("تركيب مجمع");
		ComMizan.Items.Add("1:5");
		ComMizan.Items.Add("2:4");
		ComMizan.Items.Add("3:3");
		ComMizan.Items.Add("4:2");
		ComMizan.Items.Add("5:1");
		List<SchemeItem> dataSource = new List<SchemeItem>
		{
			new SchemeItem
			{
				Text = "رقم السجل التجارى",
				Value = "CRN"
			},
			new SchemeItem
			{
				Text = "رخصة وزارة الشؤون البلدية والقروية والإسكان",
				Value = "MOM"
			},
			new SchemeItem
			{
				Text = "رخصة وزارة الموارد البشرية والتنمية الاجتماعية",
				Value = "MLS"
			},
			new SchemeItem
			{
				Text = "رخصة وزارة الاستثمار",
				Value = "SAG"
			},
			new SchemeItem
			{
				Text = "معرف آخر",
				Value = "OTH"
			}
		};
		CBScheme.DisplayMember = "Text";
		CBScheme.ValueMember = "Value";
		CBScheme.DataSource = dataSource;
		CBScheme.DropDownStyle = ComboBoxStyle.DropDownList;
	}

	private void Vats()
	{
		if (ComVat.SelectedIndex == 0)
		{
			Vat = false;
			TotalWithVat = false;
		}
		else if (ComVat.SelectedIndex == 1)
		{
			Vat = true;
			TotalWithVat = false;
		}
		else if (ComVat.SelectedIndex == 2)
		{
			Vat = true;
			TotalWithVat = true;
		}
		if (ComImageShape.SelectedIndex == 0)
		{
			ImageShape = false;
		}
		else if (ComImageShape.SelectedIndex == 1)
		{
			ImageShape = true;
		}
		if (ComInstall.SelectedIndex == 0)
		{
			InstallAll = 0;
		}
		else if (ComInstall.SelectedIndex == 1)
		{
			InstallAll = 1;
		}
		else if (ComInstall.SelectedIndex == 2)
		{
			InstallAll = 2;
		}
	}

	public bool IsValidSaudiVat(string vatNumber)
	{
		if (string.IsNullOrWhiteSpace(vatNumber))
		{
			return false;
		}
		vatNumber = vatNumber.Trim();
		if (!Regex.IsMatch(vatNumber, "^3\\d{14}$"))
		{
			return false;
		}
		return true;
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 38))
		{
			int num = 0;
			if (TxtCompanyAR.Text != "")
			{
				switch (MessageBox.Show("هل انت متأكد من حفظ بيانات اعدادات البرنامج", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = Userx.UpdateItem(TxtCompanyAR.Text, TxtCompanyEN.Text, TxtNamePrintFatora.Text, TxtVatNum.Text, Vat, Convert.ToInt32(comboTafqit.SelectedValue), Convert.ToInt32(TxtDecimal.Value), Convert.ToInt32(TxtDecimalQty.Value), Convert.ToInt32(combStorePurches.SelectedValue), Convert.ToInt32(combStore.SelectedValue), Convert.ToInt32(CBStoreWaiting.SelectedValue), CBIsPrintSecondFatoraCashier.Checked, CBIsPrintSecondFatoraCashier.Checked, CBISTakeDateApp.Checked, Convert.ToInt32(CEGroupSize.Value), CEGroupColor.Text, CEGroupFontColor.Text, Convert.ToInt32(CEItemSize.Value), CEItemColor.Text, CEItemFontColor.Text, CBActiveNameAR.Checked, CBActiveNameEN.Checked, CBActivePrice.Checked, CBItemNameAR.Text, CBItemNameEN.Text, CBItemPrice.Text, CBIsResturant.Checked, TxtBackup.Text, CBBackup.Checked, CBTabocco.Checked, CBActiveImage.Checked, TxtCommercialRegister.Text, TxtBank.Text, TxtPhone.Text, TxtFax.Text, TxtMobile.Text, TxtWeb.Text, TxtAddress.Text, comboPrint.Text, CKIsShowRoomInFormRestaurant.Checked, CBPrintCashier.Checked, CBPrintOrder.Checked, CBPrintReturnOrder.Checked, RBDiscountDecimal.Checked, RBNoDiscountDecimal.Checked, RBDiscountDecimalForCustomer.Checked, CBIsPrintItemCook1.Checked, Convert.ToDecimal(TxtDiscount.Text), CBIsSecureCashier.Checked, TxtPassWordSupervisor.Text, TxtBuildingNum.Text, TxtStreetNum.Text, TxtDistrict.Text, TxtCity.Text, TxtCountry.Text, TxtPostalCode.Text, TxtAdditionalNum.Text, TxtPerVat.Value, CBOrderStore.Checked, CkHasm.Checked, CKGabr.Checked, BtnMezan.IsOn, IsTimeInRestaurant.Checked, TotalWithVat, Convert.ToDecimal(TobaccoValue.Value), Convert.ToDecimal(TxtFontSize.Value), CkBouns.Checked, ImageShape, CkDisCode.Checked, InstallAll, CBScheme.SelectedValue.ToString(), TxtNumSchma.Text.ToString(), Convert.ToBoolean(ElcotroncInvCK.Checked), Ckglass.Checked, Convert.ToDecimal(TxtPerCashir.Text), CKBondTax.Checked, ComMizan.Text, CkBarCodeRest.Checked, CkShowFatora.Checked, TxtPhoneSent.Text, TxtToken.Text, TxtAccountSid.Text, CKShowRoom.Checked, CkAlItemInRow.Checked, Convert.ToDecimal(TxtPerGrid.Text));
					LoginDetails.CompanyNameAR = TxtCompanyAR.Text;
					LoginDetails.CompanyNameEN = TxtCompanyEN.Text;
					LoginDetails.VatNum = TxtVatNum.Text;
					LoginDetails.IsVat = Vat;
					LoginDetails.ItemTotalWithVat = TotalWithVat;
					LoginDetails.ImgShape = ImageShape;
					LoginDetails.InstallAll = InstallAll;
					LoginDetails.SecondPrintDefualt = comboPrint.Text;
					LoginDetails.TafqitID = Convert.ToInt32(comboTafqit.SelectedValue);
					LoginDetails.StoreIDPurches = Convert.ToInt32(combStorePurches.SelectedValue);
					LoginDetails.StoreSN = Convert.ToInt32(combStore.SelectedValue);
					LoginDetails.StoreIDWaiting = Convert.ToInt32(CBStoreWaiting.SelectedValue);
					LoginDetails.IsTakeDateApp = CBISTakeDateApp.Checked;
					LoginDetails.IsPrintSecondCasheir = CBIsPrintSecondFatoraCashier.Checked;
					LoginDetails.IsPrintSecondFatoraCasheir = CBIsPrintSecondFatoraCashier.Checked;
					LoginDetails.GroupSize = Convert.ToInt32(CEGroupSize.Value);
					LoginDetails.GroupColor = CEGroupColor.Text;
					LoginDetails.GroupFontColor = CEGroupFontColor.Text;
					LoginDetails.ItemSize = Convert.ToInt32(CEItemSize.Value);
					LoginDetails.ItemColor = CEItemColor.Text;
					LoginDetails.ItemFontColor = CEItemFontColor.Text;
					LoginDetails.IsItemNameAR = CBActiveNameAR.Checked;
					LoginDetails.IsItemNameEN = CBActiveNameEN.Checked;
					LoginDetails.IsItemShowPrice = CBActivePrice.Checked;
					LoginDetails.ItemNameAR = CBItemNameAR.Text;
					LoginDetails.ItemNameEN = CBItemNameEN.Text;
					LoginDetails.ItemShowPrice = CBItemPrice.Text;
					LoginDetails.IsRestaurant = CBIsResturant.Checked;
					LoginDetails.BackupPath = TxtBackup.Text;
					LoginDetails.IsBackupWhenClose = CBBackup.Checked;
					LoginDetails.IsTobacc = CBTabocco.Checked;
					LoginDetails.IsImage = CBActiveImage.Checked;
					LoginDetails.CommercialRegister = TxtCommercialRegister.Text;
					LoginDetails.CompanyBank = TxtBank.Text;
					LoginDetails.Phone = TxtPhone.Text;
					LoginDetails.Fax = TxtFax.Text;
					LoginDetails.Mobile = TxtMobile.Text;
					LoginDetails.Web = TxtWeb.Text;
					LoginDetails.Address = TxtAddress.Text;
					LoginDetails.IsShowRoomInFormRestaurant = CKIsShowRoomInFormRestaurant.Checked;
					LoginDetails.NamePrintFatora = TxtNamePrintFatora.Text;
					LoginDetails.DecimalNum = Convert.ToInt32(TxtDecimal.Value);
					LoginDetails.DiscountDecimal = RBDiscountDecimal.Checked;
					LoginDetails.NoDiscountDecimal = RBNoDiscountDecimal.Checked;
					LoginDetails.DiscountDecimalForCustomer = RBDiscountDecimalForCustomer.Checked;
					LoginDetails.IsPrintCashier = CBPrintCashier.Checked;
					LoginDetails.IsPrintOrder = CBPrintOrder.Checked;
					LoginDetails.IsPrintReturnOrder = CBPrintReturnOrder.Checked;
					LoginDetails.IsPrintItemCook = CBIsPrintItemCook1.Checked;
					LoginDetails.DiscountForRestaurant = Convert.ToDecimal(TxtDiscount.Text);
					LoginDetails.IsSecureCashier = CBIsSecureCashier.Checked;
					LoginDetails.OrderStore = CBOrderStore.Checked;
					LoginDetails.DecimalNumQty = Convert.ToInt32(TxtDecimalQty.Value);
					LoginDetails.BuildingNum = TxtBuildingNum.Text;
					LoginDetails.Street = TxtStreetNum.Text;
					LoginDetails.District = TxtDistrict.Text;
					LoginDetails.City = TxtCity.Text;
					LoginDetails.Country = TxtCountry.Text;
					LoginDetails.PostalCode = TxtPostalCode.Text;
					LoginDetails.AdditionalNum = TxtAdditionalNum.Text;
					LoginDetails.PerVat = TxtPerVat.Value;
					LoginDetails.Hasm = CkHasm.Checked;
					LoginDetails.Bouns = CkBouns.Checked;
					LoginDetails.DisCode = CkDisCode.Checked;
					LoginDetails.Gabr = CKGabr.Checked;
					LoginDetails.IsMizanWeight = BtnMezan.IsOn;
					LoginDetails.IsTimeInRestaurant = IsTimeInRestaurant.Checked;
					LoginDetails.MinTobaccoTax = Convert.ToDecimal(TobaccoValue.Value);
					LoginDetails.FontSize = Convert.ToDecimal(TxtFontSize.Value);
					LoginDetails.Glasses = Ckglass.Checked;
					LoginDetails.SupplierPartyIdentificationschemeID = CBScheme.SelectedValue.ToString();
					LoginDetails.SupplierPartyIdentificationID = TxtNumSchma.Text.ToString();
					LoginDetails.IsElectronicInvoice = Convert.ToBoolean(ElcotroncInvCK.Checked);
					LoginDetails.DisPerCashier = Convert.ToDecimal(TxtPerCashir.Text);
					LoginDetails.PerGrid = Convert.ToDecimal(TxtPerGrid.Text);
					LoginDetails.BondTax = CKBondTax.Checked;
					LoginDetails.MizanRate = ComMizan.Text;
					LoginDetails.BarcodeResturant = CkBarCodeRest.Checked;
					LoginDetails.ShowFatora = CkShowFatora.Checked;
					LoginDetails.FromPhone = TxtPhoneSent.Text;
					LoginDetails.AuthToken = TxtToken.Text;
					LoginDetails.AccountSid = TxtAccountSid.Text;
					LoginDetails.ShowRoom = CKShowRoom.Checked;
					LoginDetails.AlLItemInRow = CkAlItemInRow.Checked;
					MessageBox.Show("تم حفظ بيانات اعدادات البرنامج بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					TxtCompanyAR.Focus();
					break;
				case DialogResult.No:
					TxtCompanyAR.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل البيانات بشكل صحيح", "خطأ في ادخال البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				TxtCompanyAR.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void BindTxt()
	{
		try
		{
			BindComboTafqit();
			BindComboStore();
			BindComboContentAlignment();
			GetPrinters();
			TxtCompanyAR.Text = LoginDetails.CompanyNameAR;
			TxtCompanyEN.Text = LoginDetails.CompanyNameEN;
			TxtVatNum.Text = LoginDetails.VatNum;
			comboTafqit.SelectedValue = LoginDetails.TafqitID;
			comboPrint.Text = LoginDetails.SecondPrintDefualt;
			CBIsPrintSecondFatoraCashier.Checked = LoginDetails.IsPrintSecondFatoraCasheir;
			CBIsPrintSecondFatoraCashier.Checked = LoginDetails.IsPrintSecondCasheir;
			CBISTakeDateApp.Checked = LoginDetails.IsTakeDateApp;
			CEGroupSize.Value = LoginDetails.GroupSize;
			CEGroupColor.Text = LoginDetails.GroupColor;
			CEGroupFontColor.Text = LoginDetails.GroupFontColor;
			CEItemSize.Value = LoginDetails.ItemSize;
			CEItemColor.Text = LoginDetails.ItemColor;
			CEItemFontColor.Text = LoginDetails.ItemFontColor;
			CBActiveNameAR.Checked = LoginDetails.IsItemNameAR;
			CBActiveNameEN.Checked = LoginDetails.IsItemNameEN;
			CBActivePrice.Checked = LoginDetails.IsItemShowPrice;
			CBItemNameAR.Text = LoginDetails.ItemNameAR;
			CBItemNameEN.Text = LoginDetails.ItemNameEN;
			CBItemPrice.Text = LoginDetails.ItemShowPrice;
			CBIsResturant.Checked = LoginDetails.IsRestaurant;
			TxtBackup.Text = LoginDetails.BackupPath;
			CBBackup.Checked = LoginDetails.IsBackupWhenClose;
			CBTabocco.Checked = LoginDetails.IsTobacc;
			CBActiveImage.Checked = LoginDetails.IsImage;
			TxtCommercialRegister.Text = LoginDetails.CommercialRegister;
			TxtBank.Text = LoginDetails.CompanyBank;
			TxtPhone.Text = LoginDetails.Phone;
			TxtFax.Text = LoginDetails.Fax;
			TxtMobile.Text = LoginDetails.Mobile;
			TxtBuildingNum.Text = LoginDetails.BuildingNum;
			TxtStreetNum.Text = LoginDetails.Street;
			TxtDistrict.Text = LoginDetails.District;
			TxtCity.Text = LoginDetails.City;
			TxtCountry.Text = LoginDetails.Country;
			TxtPostalCode.Text = LoginDetails.PostalCode;
			TxtAdditionalNum.Text = LoginDetails.AdditionalNum;
			TxtWeb.Text = LoginDetails.Web;
			TxtAddress.Text = LoginDetails.Address;
			CKIsShowRoomInFormRestaurant.Checked = LoginDetails.IsShowRoomInFormRestaurant;
			TxtNamePrintFatora.Text = LoginDetails.NamePrintFatora;
			TxtDecimal.Value = LoginDetails.DecimalNum;
			TxtDecimalQty.Value = LoginDetails.DecimalNumQty;
			TxtPerVat.Value = LoginDetails.PerVat;
			RBDiscountDecimal.Checked = LoginDetails.DiscountDecimal;
			RBNoDiscountDecimal.Checked = LoginDetails.NoDiscountDecimal;
			RBDiscountDecimalForCustomer.Checked = LoginDetails.DiscountDecimalForCustomer;
			CBPrintCashier.Checked = LoginDetails.IsPrintCashier;
			CBPrintOrder.Checked = LoginDetails.IsPrintOrder;
			CBPrintReturnOrder.Checked = LoginDetails.IsPrintReturnOrder;
			CBIsPrintItemCook1.Checked = LoginDetails.IsPrintItemCook;
			CBIsSecureCashier.Checked = LoginDetails.IsSecureCashier;
			CBOrderStore.Checked = LoginDetails.OrderStore;
			TxtDiscount.Text = LoginDetails.DiscountForRestaurant.ToString();
			CkHasm.Checked = LoginDetails.Hasm;
			CkBouns.Checked = LoginDetails.Bouns;
			CkDisCode.Checked = LoginDetails.DisCode;
			CKGabr.Checked = LoginDetails.Gabr;
			BtnMezan.IsOn = LoginDetails.IsMizanWeight;
			IsTimeInRestaurant.Checked = LoginDetails.IsTimeInRestaurant;
			LoginDetails.IsVat = Vat;
			LoginDetails.ItemTotalWithVat = TotalWithVat;
			LoginDetails.ImgShape = ImageShape;
			Ckglass.Checked = LoginDetails.Glasses;
			CKBondTax.Checked = LoginDetails.BondTax;
			InstallAll = LoginDetails.InstallAll;
			ElcotroncInvCK.Checked = LoginDetails.IsElectronicInvoice;
			TxtNumSchma.Text = LoginDetails.SupplierPartyIdentificationID;
			CBScheme.SelectedValue = (string.IsNullOrEmpty(LoginDetails.SupplierPartyIdentificationschemeID) ? "CRN" : LoginDetails.SupplierPartyIdentificationschemeID);
			TxtPerCashir.Text = LoginDetails.DisPerCashier.ToString();
			using (GTSdbEntities gTSdbEntities = new GTSdbEntities())
			{
				DgvRemember.DataSource = gTSdbEntities.Select_Remember();
				DgvRemember.Columns[0].Visible = false;
			}
			ComMizan.Text = LoginDetails.MizanRate;
			CkBarCodeRest.Checked = LoginDetails.BarcodeResturant;
			CkShowFatora.Checked = LoginDetails.ShowFatora;
			TxtPhoneSent.Text = LoginDetails.FromPhone;
			TxtToken.Text = MaskMiddle(LoginDetails.AuthToken);
			TxtAccountSid.Text = MaskMiddle(LoginDetails.AccountSid);
			CKShowRoom.Checked = LoginDetails.ShowRoom;
			CkAlItemInRow.Checked = LoginDetails.AlLItemInRow;
			TxtPerGrid.Text = LoginDetails.PerGrid.ToString();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "خطأ ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void GetPrinters()
	{
		try
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			int num = 1;
			foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
			{
				dictionary.Add(num, installedPrinter);
				num++;
			}
			comboPrint.DataSource = dictionary.ToList();
			comboPrint.DisplayMember = "value";
			comboPrint.ValueMember = "key";
			string secondPrintDefualt = LoginDetails.SecondPrintDefualt;
			comboPrint.Text = secondPrintDefualt;
		}
		catch (Exception)
		{
			comboPrint.Text = "";
		}
	}

	public void BindComboTafqit()
	{
		try
		{
			comboTafqit.DataSource = Userx.GetAllTafqit();
			comboTafqit.DisplayMember = "Tafqit_Name";
			comboTafqit.ValueMember = "Tafqit_ID";
			comboTafqit.SelectedValue = LoginDetails.TafqitID;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void BindComboStore()
	{
		try
		{
			combStore.DataSource = Userx.GetAllStore();
			combStore.DisplayMember = "Store_Name";
			combStore.ValueMember = "ID";
			combStore.SelectedValue = LoginDetails.StoreSN;
			CBStoreWaiting.DataSource = Userx.GetAllStore();
			CBStoreWaiting.DisplayMember = "Store_Name";
			CBStoreWaiting.ValueMember = "ID";
			CBStoreWaiting.SelectedValue = LoginDetails.StoreIDWaiting;
			combStorePurches.DataSource = Userx.GetAllStore();
			combStorePurches.DisplayMember = "Store_Name";
			combStorePurches.ValueMember = "ID";
			combStorePurches.SelectedValue = LoginDetails.StoreIDPurches;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void BindComboContentAlignment()
	{
		try
		{
			CBItemNameAR.DataSource = Userx.GetItems_ContentAlignment();
			CBItemNameAR.DisplayMember = "Name";
			CBItemNameAR.ValueMember = "ID";
			CBItemNameAR.Text = LoginDetails.ItemNameAR;
			CBItemNameEN.DataSource = Userx.GetItems_ContentAlignment();
			CBItemNameEN.DisplayMember = "Name";
			CBItemNameEN.ValueMember = "ID";
			CBItemNameEN.Text = LoginDetails.ItemNameEN;
			CBItemPrice.DataSource = Userx.GetItems_ContentAlignment();
			CBItemPrice.DisplayMember = "Name";
			CBItemPrice.ValueMember = "ID";
			CBItemPrice.Text = LoginDetails.ItemShowPrice;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void CombNoDelete_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void CBActiveNameAR_CheckedChanged(object sender, EventArgs e)
	{
		if (CBActiveNameAR.Checked)
		{
			CBItemNameAR.Enabled = true;
		}
		else
		{
			CBItemNameAR.Enabled = false;
		}
	}

	private void CBActiveNameEN_CheckedChanged(object sender, EventArgs e)
	{
		if (CBActiveNameEN.Checked)
		{
			CBItemNameEN.Enabled = true;
		}
		else
		{
			CBItemNameEN.Enabled = false;
		}
	}

	private void CBActivePrice_CheckedChanged(object sender, EventArgs e)
	{
		if (CBActivePrice.Checked)
		{
			CBItemPrice.Enabled = true;
		}
		else
		{
			CBItemPrice.Enabled = false;
		}
	}

	private void CombName_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void TxtNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
	}

	private void TxtBackup_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
		if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
		{
			folderPath = folderBrowserDialog.SelectedPath;
			TxtBackup.Text = folderPath;
		}
	}

	private void ckRemember_CheckedChanged(object sender, EventArgs e)
	{
		if (ckRemember.Checked)
		{
			TxtDateRem.Enabled = true;
			TxtRemember.Enabled = true;
			DgvRemember.Enabled = true;
			BtnSaveRem.Enabled = true;
			BtnEditRem.Enabled = true;
			BtnDeleteRem.Enabled = true;
			BtnNewRem.Enabled = true;
		}
		else
		{
			TxtDateRem.Enabled = false;
			TxtRemember.Enabled = false;
			DgvRemember.Enabled = false;
			BtnSaveRem.Enabled = false;
			BtnEditRem.Enabled = false;
			BtnDeleteRem.Enabled = false;
			BtnNewRem.Enabled = false;
		}
	}

	private void BtnSaveRem_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(TxtRemember.Text))
		{
			if (Userx.InsertRem(TxtDateRem.DateTime, TxtRemember.Text) > 0)
			{
				MessageBox.Show("تم الحفظ");
				using GTSdbEntities gTSdbEntities = new GTSdbEntities();
				DgvRemember.Columns[0].Visible = false;
				DgvRemember.DataSource = gTSdbEntities.Select_Remember();
				GetRemmember();
			}
		}
		else
		{
			MessageBox.Show("من قضلك اكتب التذكير");
			TxtRemember.Focus();
		}
	}

	private void DgvRemember_Click(object sender, EventArgs e)
	{
		if (DgvRemember.Rows.Count > 0)
		{
			BtnSaveRem.Enabled = false;
			BtnEditRem.Enabled = true;
			BtnDeleteRem.Enabled = true;
			TxtDateRem.DateTime = DateTime.Parse(DgvRemember.CurrentRow.Cells[1].Value.ToString());
			TxtRemember.Text = DgvRemember.CurrentRow.Cells[2].Value.ToString();
		}
	}

	private void BtnNewRem_Click(object sender, EventArgs e)
	{
		TxtDateRem.DateTime = DateTime.Now;
		TxtRemember.Clear();
		BtnSaveRem.Enabled = true;
		BtnEditRem.Enabled = false;
		BtnDeleteRem.Enabled = false;
	}

	private void BtnEditRem_Click(object sender, EventArgs e)
	{
		if (DgvRemember.Rows.Count <= 0)
		{
			return;
		}
		if (!string.IsNullOrWhiteSpace(TxtRemember.Text))
		{
			if (DgvRemember.CurrentRow.Cells[0].Value == null)
			{
				return;
			}
			int id = int.Parse(DgvRemember.CurrentRow.Cells[0].Value.ToString());
			if (Userx.UpdateRem(id, TxtDateRem.DateTime, TxtRemember.Text) > 0)
			{
				MessageBox.Show("تم التعديل");
				using GTSdbEntities gTSdbEntities = new GTSdbEntities();
				DgvRemember.Columns[0].Visible = false;
				DgvRemember.DataSource = gTSdbEntities.Select_Remember();
				GetRemmember();
			}
		}
		else
		{
			MessageBox.Show("من قضلك اكتب التذكير");
			TxtRemember.Focus();
		}
	}

	private void BtnDeleteRem_Click(object sender, EventArgs e)
	{
		try
		{
			int num = int.Parse(DgvRemember.CurrentRow.Cells[0].Value.ToString());
			if (num > 0 && Userx.DeleteRem(num) > 0)
			{
				MessageBox.Show("تم الحذف");
				using GTSdbEntities gTSdbEntities = new GTSdbEntities();
				DgvRemember.Columns[0].Visible = false;
				DgvRemember.DataSource = gTSdbEntities.Select_Remember();
				GetRemmember();
				return;
			}
		}
		catch
		{
		}
	}

	private void GetRemmember()
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		LoginDetails.remembers = new List<TblRemember>();
		foreach (TblRemember item in (IEnumerable<TblRemember>)gTSdbEntities.TblRemembers)
		{
			if (item.DateTimeRem.Value.Date == DateTime.Now.Date)
			{
				LoginDetails.remembers.Add(item);
			}
		}
	}

	private void TxtPerVat_Validated(object sender, EventArgs e)
	{
		FrmPassWordVat frmPassWordVat = new FrmPassWordVat();
		frmPassWordVat.ShowDialog();
		if (frmPassWordVat.ISOK == Convert.ToBoolean("True"))
		{
			LoginDetails.PerVat = TxtPerVat.Value;
		}
		else
		{
			TxtPerVat.Value = LoginDetails.PerVat;
		}
	}

	private void FrmSetting_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F5")
		{
			BtnSave_Click(null, null);
		}
	}

	private void FrmSetting_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
		TobaccoValue.Value = (from x in Userx.GetAll()
			select x.MinTobaccoTax).FirstOrDefault().GetValueOrDefault();
		TxtFontSize.Value = (from x in Userx.GetAll()
			select x.FontSize).FirstOrDefault().GetValueOrDefault();
		TblSetting tblSetting = Userx.GetAll().FirstOrDefault();
		if (tblSetting != null)
		{
			Vat = tblSetting.IsVat == true;
			TotalWithVat = tblSetting.ItemTotalWithVat == true;
			ImageShape = tblSetting.ImageShape == true;
			if (!Vat && !TotalWithVat)
			{
				ComVat.SelectedIndex = 0;
			}
			else if (Vat && !TotalWithVat)
			{
				ComVat.SelectedIndex = 1;
			}
			else if (Vat && TotalWithVat)
			{
				ComVat.SelectedIndex = 2;
			}
			if (!ImageShape)
			{
				ComImageShape.SelectedIndex = 0;
			}
			else if (ImageShape)
			{
				ComImageShape.SelectedIndex = 1;
			}
			if (InstallAll == 0)
			{
				ComInstall.SelectedIndex = 0;
			}
			else if (InstallAll == 1)
			{
				ComInstall.SelectedIndex = 1;
			}
			else if (InstallAll == 2)
			{
				ComInstall.SelectedIndex = 2;
			}
		}
		LoginDetails.IsVat = Vat;
		LoginDetails.ItemTotalWithVat = TotalWithVat;
		LoginDetails.ImgShape = ImageShape;
		LoginDetails.InstallAll = InstallAll;
	}

	private void BtnCopyMac_Click(object sender, EventArgs e)
	{
		string text = "";
		ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
		foreach (ManagementObject item in managementObjectSearcher.Get())
		{
			text = item["SerialNumber"].ToString();
		}
		string text2 = "";
		string text3 = Path.GetPathRoot(Environment.SystemDirectory).TrimEnd('\\');
		using (ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" + text3 + "'} WHERE ResultClass=Win32_DiskPartition"))
		{
			using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator2 = managementObjectSearcher2.Get().GetEnumerator();
			if (managementObjectEnumerator2.MoveNext())
			{
				ManagementBaseObject current = managementObjectEnumerator2.Current;
				using ManagementObjectSearcher managementObjectSearcher3 = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + current["DeviceID"]?.ToString() + "'} WHERE ResultClass=Win32_DiskDrive");
				using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator3 = managementObjectSearcher3.Get().GetEnumerator();
				if (managementObjectEnumerator3.MoveNext())
				{
					ManagementBaseObject current2 = managementObjectEnumerator3.Current;
					text2 = "SerialNumber: " + current2["SerialNumber"];
				}
			}
		}
		string plainText = Class_Encryption.EncryptString("MS-" + Convert.ToString(text) + "-19-5-01-" + Convert.ToString(text2), "EFms");
		string text4 = Class_Encryption.EncryptString(plainText, "EFms");
		if (File.Exists(Application.StartupPath + "\\Serial.xml"))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SaveActiveForXml>));
			List<SaveActiveForXml> list = new List<SaveActiveForXml>();
			FileStream fileStream = new FileStream(Application.StartupPath + "\\Serial.xml", FileMode.Open, FileAccess.Read);
			list = (List<SaveActiveForXml>)xmlSerializer.Deserialize(fileStream);
			SaveActiveForXml saveActiveForXml = list[0];
			fileStream.Close();
			Clipboard.SetText(saveActiveForXml._Active);
			MessageBox.Show("تم نسخ الكود", "Mac Address", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void whatsapp()
	{
		WhatsApp wa = new WhatsApp("00201007487031", "0111", "name", debug: true);
		wa.OnConnectSuccess += delegate
		{
			Connect.Text = "Connected...";
			wa.OnLoginSuccess += delegate
			{
				Connect.Text += "\r\nLogin success !";
				wa.SendMessageImage(lblcoo.Text, null, ApiBase.ImageType.GIF);
				Connect.Text += "\r\nMessage sent !";
			};
			wa.OnLoginFailed += delegate(string data)
			{
				Connect.Text += $"\r\nLogin failed {data}";
			};
			wa.Login();
		};
		wa.OnConnectFailed += delegate(Exception ex)
		{
			Connect.Text += $"\r\nConnect failed {ex.StackTrace}";
		};
		wa.Connect();
	}

	private void Email(string Mac)
	{
		try
		{
			MailMessage mailMessage = new MailMessage();
			mailMessage.To.Add("DevAmmarAhmed@gmail.com");
			mailMessage.From = new MailAddress("gtsdev7@gmail.com");
			mailMessage.Subject = "School Name";
			SmtpClient smtpClient = new SmtpClient("localhost", 25);
			smtpClient.Host = "smtp.gmail.com";
			smtpClient.Credentials = new NetworkCredential("gtsdev7@gmail.com", "gts@sa=admin");
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.EnableSsl = true;
			smtpClient.Send(mailMessage);
			MessageBox.Show("Mail Send");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void checkVat_CheckedChanged(object sender, EventArgs e)
	{
		if (!checkVat.Checked)
		{
			checkItemTotalWithVat.Checked = false;
		}
	}

	private void checkItemTotalWithVat_CheckedChanged(object sender, EventArgs e)
	{
		if (checkItemTotalWithVat.Checked)
		{
			checkVat.Checked = true;
		}
	}

	private void CBTabocco_CheckedChanged(object sender, EventArgs e)
	{
		TobaccoValue.ReadOnly = !CBTabocco.Checked;
		TobaccoValue.Text = (CBTabocco.Checked ? "25" : "0");
	}

	private void ComVat_SelectedValueChanged(object sender, EventArgs e)
	{
		Vats();
	}

	private void ComInstall_SelectedIndexChanged(object sender, EventArgs e)
	{
		Vats();
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void ComImageShape_SelectedIndexChanged(object sender, EventArgs e)
	{
		Vats();
	}

	private void TobaccoValue_ValueChanged(object sender, EventArgs e)
	{
		LoginDetails.MinTobaccoTax = Convert.ToDecimal(TobaccoValue.Text);
	}

	private void TxtPerCashir_ValueChanged(object sender, EventArgs e)
	{
		if (TxtPerCashir.Value > 100m)
		{
			TxtPerCashir.Value = 0m;
		}
	}

	private void TapPrice_Click(object sender, EventArgs e)
	{
	}

	private void xtraTabPage4_Paint(object sender, PaintEventArgs e)
	{
	}

	private void TxtNumber_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
		{
			e.Handled = true;
		}
	}

	private void ElcotroncInvCK_MouseClick(object sender, MouseEventArgs e)
	{
		if (!ElcotroncInvCK.Checked)
		{
			ElcotroncInvCK.Checked = false;
			BtnSave_Click(null, null);
			Application.Restart();
			Environment.Exit(0);
		}
		else
		{
			ElcotroncInvCK.Checked = false;
			FrmCheckActivationElctroncInv frmCheckActivationElctroncInv = new FrmCheckActivationElctroncInv();
			frmCheckActivationElctroncInv.ShowDialog();
			if (frmCheckActivationElctroncInv.Active)
			{
				ElcotroncInvCK.Checked = true;
				BtnSave_Click(null, null);
				Application.Restart();
				Environment.Exit(0);
			}
			else
			{
				ElcotroncInvCK.Checked = false;
			}
		}
		BtnSave_Click(null, null);
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		TxtAccountSid.Text = string.Empty;
		TxtToken.Text = string.Empty;
		TxtPhoneSent.Text = string.Empty;
	}

	private void BtnPastTocken_Click(object sender, EventArgs e)
	{
		TxtToken.Text = string.Empty;
		string input = Clipboard.GetText();
		TxtToken.Text = MaskMiddle(input);
	}

	private void BtnSid_Click(object sender, EventArgs e)
	{
		TxtAccountSid.Text = string.Empty;
		string input = Clipboard.GetText();
		TxtAccountSid.Text = MaskMiddle(input);
	}

	private string MaskMiddle(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return input;
		}
		if (input.Length <= 8)
		{
			return new string('*', input.Length);
		}
		string text = input.Substring(0, 4);
		string text2 = input.Substring(input.Length - 4, 4);
		string text3 = new string('*', input.Length - 8);
		return text + text3 + text2;
	}

	private void TxtAccountSid_KeyDown(object sender, KeyEventArgs e)
	{
		if ((e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.Insert)) || (e.Shift && e.KeyCode == Keys.Insert))
		{
			e.SuppressKeyPress = true;
		}
	}

	private void TxtToken_KeyDown(object sender, KeyEventArgs e)
	{
		if ((e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.Insert)) || (e.Shift && e.KeyCode == Keys.Insert))
		{
			e.SuppressKeyPress = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.FrmSetting));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.TapPrice = new DevExpress.XtraTab.XtraTabControl();
		this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
		this.TxtNumSchma = new System.Windows.Forms.TextBox();
		this.label44 = new System.Windows.Forms.Label();
		this.label43 = new System.Windows.Forms.Label();
		this.CBScheme = new System.Windows.Forms.ComboBox();
		this.label28 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.TxtCity = new System.Windows.Forms.TextBox();
		this.TxtDistrict = new System.Windows.Forms.TextBox();
		this.label33 = new System.Windows.Forms.Label();
		this.TxtStreetNum = new System.Windows.Forms.TextBox();
		this.label34 = new System.Windows.Forms.Label();
		this.TxtBuildingNum = new System.Windows.Forms.TextBox();
		this.TxtAdditionalNum = new System.Windows.Forms.TextBox();
		this.label35 = new System.Windows.Forms.Label();
		this.TxtPostalCode = new System.Windows.Forms.TextBox();
		this.TxtCountry = new System.Windows.Forms.TextBox();
		this.label22 = new System.Windows.Forms.Label();
		this.TxtNamePrintFatora = new System.Windows.Forms.TextBox();
		this.TxtBank = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtWeb = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.TxtFax = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtAddress = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtMobile = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.TxtPhone = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtCommercialRegister = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtCompanyAR = new System.Windows.Forms.TextBox();
		this.TxtCompanyEN = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtVatNum = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
		this.CkShowFatora = new System.Windows.Forms.CheckBox();
		this.CKBondTax = new System.Windows.Forms.CheckBox();
		this.ElcotroncInvCK = new System.Windows.Forms.CheckBox();
		this.label42 = new System.Windows.Forms.Label();
		this.ComInstall = new System.Windows.Forms.ComboBox();
		this.CkDisCode = new System.Windows.Forms.CheckBox();
		this.CkBouns = new System.Windows.Forms.CheckBox();
		this.label41 = new System.Windows.Forms.Label();
		this.ComVat = new System.Windows.Forms.ComboBox();
		this.TobaccoValue = new System.Windows.Forms.NumericUpDown();
		this.label39 = new System.Windows.Forms.Label();
		this.combStorePurches = new System.Windows.Forms.ComboBox();
		this.label27 = new System.Windows.Forms.Label();
		this.TxtPerVat = new System.Windows.Forms.NumericUpDown();
		this.TxtDecimalQty = new System.Windows.Forms.NumericUpDown();
		this.CkHasm = new System.Windows.Forms.CheckBox();
		this.TxtDecimal = new System.Windows.Forms.NumericUpDown();
		this.label38 = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.CBBackup = new System.Windows.Forms.CheckBox();
		this.lblcoo = new System.Windows.Forms.Label();
		this.Connect = new System.Windows.Forms.Label();
		this.BtnCopyMac = new DevExpress.XtraEditors.SimpleButton();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtBackup = new DevExpress.XtraEditors.ButtonEdit();
		this.CKGabr = new System.Windows.Forms.CheckBox();
		this.CBTabocco = new System.Windows.Forms.CheckBox();
		this.CBStoreWaiting = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.combStore = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.comboTafqit = new System.Windows.Forms.ComboBox();
		this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
		this.TxtPerGrid = new System.Windows.Forms.NumericUpDown();
		this.label50 = new System.Windows.Forms.Label();
		this.CkAlItemInRow = new System.Windows.Forms.CheckBox();
		this.CKShowRoom = new System.Windows.Forms.CheckBox();
		this.CkBarCodeRest = new System.Windows.Forms.CheckBox();
		this.label21 = new System.Windows.Forms.Label();
		this.comboPrint = new System.Windows.Forms.ComboBox();
		this.TxtPerCashir = new System.Windows.Forms.NumericUpDown();
		this.label45 = new System.Windows.Forms.Label();
		this.CKIsShowRoomInFormRestaurant = new System.Windows.Forms.CheckBox();
		this.CBISTakeDateApp = new System.Windows.Forms.CheckBox();
		this.CBIsPrintSecondFatoraCashier = new System.Windows.Forms.CheckBox();
		this.CBIsResturant = new System.Windows.Forms.CheckBox();
		this.CBIsPrintItemCook1 = new System.Windows.Forms.CheckBox();
		this.Ckglass = new System.Windows.Forms.CheckBox();
		this.ComImageShape = new System.Windows.Forms.ComboBox();
		this.TxtFontSize = new System.Windows.Forms.NumericUpDown();
		this.label40 = new System.Windows.Forms.Label();
		this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
		this.CBActiveImage = new System.Windows.Forms.CheckBox();
		this.CBItemNameEN = new System.Windows.Forms.ComboBox();
		this.CBActiveNameEN = new System.Windows.Forms.CheckBox();
		this.CBItemPrice = new System.Windows.Forms.ComboBox();
		this.CBActivePrice = new System.Windows.Forms.CheckBox();
		this.CBItemNameAR = new System.Windows.Forms.ComboBox();
		this.CBActiveNameAR = new System.Windows.Forms.CheckBox();
		this.label10 = new System.Windows.Forms.Label();
		this.CEGroupFontColor = new DevExpress.XtraEditors.ColorEdit();
		this.label9 = new System.Windows.Forms.Label();
		this.CEItemFontColor = new DevExpress.XtraEditors.ColorEdit();
		this.label5 = new System.Windows.Forms.Label();
		this.CEItemSize = new System.Windows.Forms.NumericUpDown();
		this.label7 = new System.Windows.Forms.Label();
		this.CEItemColor = new DevExpress.XtraEditors.ColorEdit();
		this.label6 = new System.Windows.Forms.Label();
		this.CEGroupSize = new System.Windows.Forms.NumericUpDown();
		this.label20 = new System.Windows.Forms.Label();
		this.CEGroupColor = new DevExpress.XtraEditors.ColorEdit();
		this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
		this.label46 = new System.Windows.Forms.Label();
		this.ComMizan = new System.Windows.Forms.ComboBox();
		this.CBIsPrintItemCook = new System.Windows.Forms.CheckBox();
		this.IsTimeInRestaurant = new System.Windows.Forms.CheckBox();
		this.label26 = new System.Windows.Forms.Label();
		this.checkItemTotalWithVat = new System.Windows.Forms.CheckBox();
		this.TxtPassWordSupervisor = new System.Windows.Forms.TextBox();
		this.checkVat = new System.Windows.Forms.CheckBox();
		this.TxtDiscount = new System.Windows.Forms.NumericUpDown();
		this.label25 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.BtnMezan = new DevExpress.XtraEditors.ToggleSwitch();
		this.CBOrderStore = new System.Windows.Forms.CheckBox();
		this.CBIsSecureCashier = new System.Windows.Forms.CheckBox();
		this.CBPrintReturnOrder = new System.Windows.Forms.CheckBox();
		this.CBPrintOrder = new System.Windows.Forms.CheckBox();
		this.CBPrintCashier = new System.Windows.Forms.CheckBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.RBDiscountDecimal = new System.Windows.Forms.RadioButton();
		this.RBDiscountDecimalForCustomer = new System.Windows.Forms.RadioButton();
		this.RBNoDiscountDecimal = new System.Windows.Forms.RadioButton();
		this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.DgvRemember = new System.Windows.Forms.DataGridView();
		this.BtnDeleteRem = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEditRem = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNewRem = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSaveRem = new DevExpress.XtraEditors.SimpleButton();
		this.TxtRemember = new System.Windows.Forms.TextBox();
		this.TxtDateRem = new DevExpress.XtraEditors.DateEdit();
		this.label37 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.ckRemember = new System.Windows.Forms.CheckBox();
		this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.TxtAccountSid = new System.Windows.Forms.TextBox();
		this.BtnSid = new DevExpress.XtraEditors.SimpleButton();
		this.TxtToken = new System.Windows.Forms.TextBox();
		this.BtnPastTocken = new DevExpress.XtraEditors.SimpleButton();
		this.label49 = new System.Windows.Forms.Label();
		this.label48 = new System.Windows.Forms.Label();
		this.TxtPhoneSent = new System.Windows.Forms.TextBox();
		this.label47 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TapPrice).BeginInit();
		this.TapPrice.SuspendLayout();
		this.xtraTabPage1.SuspendLayout();
		this.xtraTabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TobaccoValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerVat).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDecimalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDecimal).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtBackup.Properties).BeginInit();
		this.xtraTabPage3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtPerGrid).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerCashir).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtFontSize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CEGroupFontColor.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CEItemFontColor.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CEItemSize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CEItemColor.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CEGroupSize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CEGroupColor.Properties).BeginInit();
		this.xtraTabPage4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDiscount).BeginInit();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnMezan.Properties).BeginInit();
		this.groupBox4.SuspendLayout();
		this.xtraTabPage5.SuspendLayout();
		this.groupBox6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DgvRemember).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRem.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRem.Properties).BeginInit();
		this.xtraTabPage6.SuspendLayout();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.BtnExit);
		this.groupBox1.Controls.Add(this.BtnSave);
		this.groupBox1.Controls.Add(this.TapPrice);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox1.Size = new System.Drawing.Size(860, 639);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(31, 594);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(149, 39);
		this.BtnExit.TabIndex = 1;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(676, 593);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(149, 39);
		this.BtnSave.TabIndex = 0;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.TapPrice.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TapPrice.Location = new System.Drawing.Point(6, 22);
		this.TapPrice.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TapPrice.MultiLine = DevExpress.Utils.DefaultBoolean.False;
		this.TapPrice.Name = "TapPrice";
		this.TapPrice.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.Center;
		this.TapPrice.SelectedTabPage = this.xtraTabPage1;
		this.TapPrice.Size = new System.Drawing.Size(848, 563);
		this.TapPrice.TabIndex = 59;
		this.TapPrice.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[6] { this.xtraTabPage1, this.xtraTabPage2, this.xtraTabPage3, this.xtraTabPage4, this.xtraTabPage5, this.xtraTabPage6 });
		this.TapPrice.Click += new System.EventHandler(TapPrice_Click);
		this.xtraTabPage1.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 9f);
		this.xtraTabPage1.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage1.Controls.Add(this.TxtNumSchma);
		this.xtraTabPage1.Controls.Add(this.label44);
		this.xtraTabPage1.Controls.Add(this.label43);
		this.xtraTabPage1.Controls.Add(this.CBScheme);
		this.xtraTabPage1.Controls.Add(this.label28);
		this.xtraTabPage1.Controls.Add(this.label29);
		this.xtraTabPage1.Controls.Add(this.label30);
		this.xtraTabPage1.Controls.Add(this.label31);
		this.xtraTabPage1.Controls.Add(this.TxtCity);
		this.xtraTabPage1.Controls.Add(this.TxtDistrict);
		this.xtraTabPage1.Controls.Add(this.label33);
		this.xtraTabPage1.Controls.Add(this.TxtStreetNum);
		this.xtraTabPage1.Controls.Add(this.label34);
		this.xtraTabPage1.Controls.Add(this.TxtBuildingNum);
		this.xtraTabPage1.Controls.Add(this.TxtAdditionalNum);
		this.xtraTabPage1.Controls.Add(this.label35);
		this.xtraTabPage1.Controls.Add(this.TxtPostalCode);
		this.xtraTabPage1.Controls.Add(this.TxtCountry);
		this.xtraTabPage1.Controls.Add(this.label22);
		this.xtraTabPage1.Controls.Add(this.TxtNamePrintFatora);
		this.xtraTabPage1.Controls.Add(this.TxtBank);
		this.xtraTabPage1.Controls.Add(this.label19);
		this.xtraTabPage1.Controls.Add(this.TxtWeb);
		this.xtraTabPage1.Controls.Add(this.label18);
		this.xtraTabPage1.Controls.Add(this.TxtFax);
		this.xtraTabPage1.Controls.Add(this.label17);
		this.xtraTabPage1.Controls.Add(this.TxtAddress);
		this.xtraTabPage1.Controls.Add(this.label15);
		this.xtraTabPage1.Controls.Add(this.TxtMobile);
		this.xtraTabPage1.Controls.Add(this.label14);
		this.xtraTabPage1.Controls.Add(this.TxtPhone);
		this.xtraTabPage1.Controls.Add(this.label13);
		this.xtraTabPage1.Controls.Add(this.TxtCommercialRegister);
		this.xtraTabPage1.Controls.Add(this.label12);
		this.xtraTabPage1.Controls.Add(this.TxtCompanyAR);
		this.xtraTabPage1.Controls.Add(this.TxtCompanyEN);
		this.xtraTabPage1.Controls.Add(this.label8);
		this.xtraTabPage1.Controls.Add(this.TxtVatNum);
		this.xtraTabPage1.Controls.Add(this.label1);
		this.xtraTabPage1.Controls.Add(this.label2);
		this.xtraTabPage1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabPage1.Name = "xtraTabPage1";
		this.xtraTabPage1.Size = new System.Drawing.Size(846, 531);
		this.xtraTabPage1.Text = "بيانات الشركة";
		this.TxtNumSchma.Location = new System.Drawing.Point(24, 386);
		this.TxtNumSchma.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtNumSchma.Name = "TxtNumSchma";
		this.TxtNumSchma.Size = new System.Drawing.Size(270, 23);
		this.TxtNumSchma.TabIndex = 213;
		this.TxtNumSchma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtNumSchma.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumber_KeyPress);
		this.label44.AutoSize = true;
		this.label44.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label44.Location = new System.Drawing.Point(315, 389);
		this.label44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label44.Name = "label44";
		this.label44.Size = new System.Drawing.Size(66, 17);
		this.label44.TabIndex = 214;
		this.label44.Text = "رقم الهويه";
		this.label43.AutoSize = true;
		this.label43.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label43.Location = new System.Drawing.Point(725, 426);
		this.label43.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label43.Name = "label43";
		this.label43.Size = new System.Drawing.Size(57, 17);
		this.label43.TabIndex = 212;
		this.label43.Text = "Scheme";
		this.CBScheme.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.CBScheme.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.CBScheme.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CBScheme.FormattingEnabled = true;
		this.CBScheme.Location = new System.Drawing.Point(24, 421);
		this.CBScheme.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBScheme.Name = "CBScheme";
		this.CBScheme.Size = new System.Drawing.Size(694, 26);
		this.CBScheme.TabIndex = 211;
		this.label28.AutoSize = true;
		this.label28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label28.Location = new System.Drawing.Point(737, 390);
		this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(45, 16);
		this.label28.TabIndex = 191;
		this.label28.Text = "المدينة";
		this.label29.AutoSize = true;
		this.label29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label29.Location = new System.Drawing.Point(743, 352);
		this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(36, 16);
		this.label29.TabIndex = 192;
		this.label29.Text = "الحى";
		this.label30.AutoSize = true;
		this.label30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label30.Location = new System.Drawing.Point(743, 321);
		this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(45, 16);
		this.label30.TabIndex = 193;
		this.label30.Text = "الشارع";
		this.label31.AutoSize = true;
		this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label31.Location = new System.Drawing.Point(743, 288);
		this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(68, 16);
		this.label31.TabIndex = 194;
		this.label31.Text = "رقم المبنى";
		this.TxtCity.Location = new System.Drawing.Point(449, 386);
		this.TxtCity.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCity.Name = "TxtCity";
		this.TxtCity.Size = new System.Drawing.Size(270, 23);
		this.TxtCity.TabIndex = 179;
		this.TxtCity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtDistrict.Location = new System.Drawing.Point(449, 352);
		this.TxtDistrict.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDistrict.Name = "TxtDistrict";
		this.TxtDistrict.Size = new System.Drawing.Size(270, 23);
		this.TxtDistrict.TabIndex = 180;
		this.TxtDistrict.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label33.AutoSize = true;
		this.label33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label33.Location = new System.Drawing.Point(315, 327);
		this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(80, 16);
		this.label33.TabIndex = 188;
		this.label33.Text = "الرقم الإضافى";
		this.TxtStreetNum.Location = new System.Drawing.Point(449, 321);
		this.TxtStreetNum.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtStreetNum.Name = "TxtStreetNum";
		this.TxtStreetNum.Size = new System.Drawing.Size(270, 23);
		this.TxtStreetNum.TabIndex = 181;
		this.TxtStreetNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label34.AutoSize = true;
		this.label34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label34.Location = new System.Drawing.Point(315, 296);
		this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(74, 16);
		this.label34.TabIndex = 189;
		this.label34.Text = "الرمز البريدى";
		this.TxtBuildingNum.Location = new System.Drawing.Point(449, 288);
		this.TxtBuildingNum.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtBuildingNum.Name = "TxtBuildingNum";
		this.TxtBuildingNum.Size = new System.Drawing.Size(270, 23);
		this.TxtBuildingNum.TabIndex = 182;
		this.TxtBuildingNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtAdditionalNum.Location = new System.Drawing.Point(26, 325);
		this.TxtAdditionalNum.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtAdditionalNum.Name = "TxtAdditionalNum";
		this.TxtAdditionalNum.Size = new System.Drawing.Size(270, 23);
		this.TxtAdditionalNum.TabIndex = 184;
		this.TxtAdditionalNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label35.AutoSize = true;
		this.label35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label35.Location = new System.Drawing.Point(315, 263);
		this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(31, 16);
		this.label35.TabIndex = 190;
		this.label35.Text = "البلد";
		this.TxtPostalCode.Location = new System.Drawing.Point(26, 293);
		this.TxtPostalCode.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPostalCode.Name = "TxtPostalCode";
		this.TxtPostalCode.Size = new System.Drawing.Size(270, 23);
		this.TxtPostalCode.TabIndex = 185;
		this.TxtPostalCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtCountry.Location = new System.Drawing.Point(26, 260);
		this.TxtCountry.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCountry.Name = "TxtCountry";
		this.TxtCountry.Size = new System.Drawing.Size(270, 23);
		this.TxtCountry.TabIndex = 186;
		this.TxtCountry.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label22.Location = new System.Drawing.Point(722, 158);
		this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(121, 17);
		this.label22.TabIndex = 70;
		this.label22.Text = "الاسم علي الطباعة";
		this.TxtNamePrintFatora.Location = new System.Drawing.Point(449, 158);
		this.TxtNamePrintFatora.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtNamePrintFatora.Name = "TxtNamePrintFatora";
		this.TxtNamePrintFatora.Size = new System.Drawing.Size(269, 23);
		this.TxtNamePrintFatora.TabIndex = 69;
		this.TxtNamePrintFatora.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtBank.Location = new System.Drawing.Point(26, 227);
		this.TxtBank.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtBank.Name = "TxtBank";
		this.TxtBank.Size = new System.Drawing.Size(270, 23);
		this.TxtBank.TabIndex = 7;
		this.TxtBank.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtBank.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtBank.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label19.Location = new System.Drawing.Point(315, 230);
		this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(78, 17);
		this.label19.TabIndex = 68;
		this.label19.Text = "رقم ط­. البنك";
		this.TxtWeb.Location = new System.Drawing.Point(26, 357);
		this.TxtWeb.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtWeb.Name = "TxtWeb";
		this.TxtWeb.Size = new System.Drawing.Size(270, 23);
		this.TxtWeb.TabIndex = 8;
		this.TxtWeb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtWeb.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label18.Location = new System.Drawing.Point(304, 358);
		this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(106, 17);
		this.label18.TabIndex = 66;
		this.label18.Text = "الموقع الالكتروني";
		this.TxtFax.Location = new System.Drawing.Point(26, 193);
		this.TxtFax.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtFax.Name = "TxtFax";
		this.TxtFax.Size = new System.Drawing.Size(270, 23);
		this.TxtFax.TabIndex = 5;
		this.TxtFax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtFax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label17.Location = new System.Drawing.Point(315, 196);
		this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(76, 17);
		this.label17.TabIndex = 64;
		this.label17.Text = "رقم الفاكس";
		this.TxtAddress.Location = new System.Drawing.Point(26, 460);
		this.TxtAddress.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtAddress.Multiline = true;
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtAddress.Size = new System.Drawing.Size(694, 60);
		this.TxtAddress.TabIndex = 9;
		this.TxtAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label15.Location = new System.Drawing.Point(743, 460);
		this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(45, 17);
		this.label15.TabIndex = 62;
		this.label15.Text = "العنوان";
		this.TxtMobile.Location = new System.Drawing.Point(449, 256);
		this.TxtMobile.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtMobile.Name = "TxtMobile";
		this.TxtMobile.Size = new System.Drawing.Size(270, 23);
		this.TxtMobile.TabIndex = 6;
		this.TxtMobile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtMobile.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label14.Location = new System.Drawing.Point(737, 256);
		this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(68, 17);
		this.label14.TabIndex = 60;
		this.label14.Text = "رقم الجوال";
		this.TxtPhone.Location = new System.Drawing.Point(449, 222);
		this.TxtPhone.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Size = new System.Drawing.Size(270, 23);
		this.TxtPhone.TabIndex = 4;
		this.TxtPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label13.Location = new System.Drawing.Point(737, 225);
		this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(69, 17);
		this.label13.TabIndex = 58;
		this.label13.Text = "رقم الهاتف";
		this.TxtCommercialRegister.Location = new System.Drawing.Point(26, 162);
		this.TxtCommercialRegister.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCommercialRegister.Name = "TxtCommercialRegister";
		this.TxtCommercialRegister.Size = new System.Drawing.Size(270, 23);
		this.TxtCommercialRegister.TabIndex = 3;
		this.TxtCommercialRegister.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtCommercialRegister.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label12.Location = new System.Drawing.Point(315, 165);
		this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(97, 17);
		this.label12.TabIndex = 56;
		this.label12.Text = "السجل التجاري";
		this.TxtCompanyAR.Location = new System.Drawing.Point(24, 22);
		this.TxtCompanyAR.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCompanyAR.Multiline = true;
		this.TxtCompanyAR.Name = "TxtCompanyAR";
		this.TxtCompanyAR.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtCompanyAR.Size = new System.Drawing.Size(694, 61);
		this.TxtCompanyAR.TabIndex = 0;
		this.TxtCompanyAR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtCompanyAR.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtCompanyEN.Location = new System.Drawing.Point(24, 89);
		this.TxtCompanyEN.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCompanyEN.Multiline = true;
		this.TxtCompanyEN.Name = "TxtCompanyEN";
		this.TxtCompanyEN.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtCompanyEN.Size = new System.Drawing.Size(694, 61);
		this.TxtCompanyEN.TabIndex = 1;
		this.TxtCompanyEN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtCompanyEN.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label8.Location = new System.Drawing.Point(743, 25);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(82, 17);
		this.label8.TabIndex = 49;
		this.label8.Text = "اسم الشركة";
		this.TxtVatNum.Location = new System.Drawing.Point(449, 191);
		this.TxtVatNum.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtVatNum.Name = "TxtVatNum";
		this.TxtVatNum.Size = new System.Drawing.Size(270, 23);
		this.TxtVatNum.TabIndex = 2;
		this.TxtVatNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtVatNum.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtVatNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label1.Location = new System.Drawing.Point(737, 194);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(88, 17);
		this.label1.TabIndex = 51;
		this.label1.Text = "الرقم الضريبي";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label2.Location = new System.Drawing.Point(737, 91);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(93, 17);
		this.label2.TabIndex = 54;
		this.label2.Text = "اسم الانجليزي";
		this.xtraTabPage2.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 9f);
		this.xtraTabPage2.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage2.Controls.Add(this.CkShowFatora);
		this.xtraTabPage2.Controls.Add(this.CKBondTax);
		this.xtraTabPage2.Controls.Add(this.ElcotroncInvCK);
		this.xtraTabPage2.Controls.Add(this.label42);
		this.xtraTabPage2.Controls.Add(this.ComInstall);
		this.xtraTabPage2.Controls.Add(this.CkDisCode);
		this.xtraTabPage2.Controls.Add(this.CkBouns);
		this.xtraTabPage2.Controls.Add(this.label41);
		this.xtraTabPage2.Controls.Add(this.ComVat);
		this.xtraTabPage2.Controls.Add(this.TobaccoValue);
		this.xtraTabPage2.Controls.Add(this.label39);
		this.xtraTabPage2.Controls.Add(this.combStorePurches);
		this.xtraTabPage2.Controls.Add(this.label27);
		this.xtraTabPage2.Controls.Add(this.TxtPerVat);
		this.xtraTabPage2.Controls.Add(this.TxtDecimalQty);
		this.xtraTabPage2.Controls.Add(this.CkHasm);
		this.xtraTabPage2.Controls.Add(this.TxtDecimal);
		this.xtraTabPage2.Controls.Add(this.label38);
		this.xtraTabPage2.Controls.Add(this.label36);
		this.xtraTabPage2.Controls.Add(this.label23);
		this.xtraTabPage2.Controls.Add(this.groupBox2);
		this.xtraTabPage2.Controls.Add(this.CKGabr);
		this.xtraTabPage2.Controls.Add(this.CBTabocco);
		this.xtraTabPage2.Controls.Add(this.CBStoreWaiting);
		this.xtraTabPage2.Controls.Add(this.label4);
		this.xtraTabPage2.Controls.Add(this.combStore);
		this.xtraTabPage2.Controls.Add(this.label3);
		this.xtraTabPage2.Controls.Add(this.label16);
		this.xtraTabPage2.Controls.Add(this.comboTafqit);
		this.xtraTabPage2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabPage2.Name = "xtraTabPage2";
		this.xtraTabPage2.Size = new System.Drawing.Size(846, 531);
		this.xtraTabPage2.Text = "الاعدادات الرئيسية";
		this.CkShowFatora.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CkShowFatora.AutoSize = true;
		this.CkShowFatora.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CkShowFatora.Location = new System.Drawing.Point(345, 254);
		this.CkShowFatora.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CkShowFatora.Name = "CkShowFatora";
		this.CkShowFatora.Size = new System.Drawing.Size(333, 21);
		this.CkShowFatora.TabIndex = 217;
		this.CkShowFatora.Text = "اظهار الفاوتير التي لم يتم رفعها فقط في شاشه الرفع";
		this.CkShowFatora.UseVisualStyleBackColor = true;
		this.CKBondTax.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CKBondTax.AutoSize = true;
		this.CKBondTax.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CKBondTax.Location = new System.Drawing.Point(100, 251);
		this.CKBondTax.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CKBondTax.Name = "CKBondTax";
		this.CKBondTax.Size = new System.Drawing.Size(185, 21);
		this.CKBondTax.TabIndex = 216;
		this.CKBondTax.Text = "تفعيل الضريبه في السندات";
		this.CKBondTax.UseVisualStyleBackColor = true;
		this.ElcotroncInvCK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ElcotroncInvCK.AutoSize = true;
		this.ElcotroncInvCK.Font = new System.Drawing.Font("Tahoma", 8f);
		this.ElcotroncInvCK.Location = new System.Drawing.Point(510, 229);
		this.ElcotroncInvCK.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.ElcotroncInvCK.Name = "ElcotroncInvCK";
		this.ElcotroncInvCK.Size = new System.Drawing.Size(168, 21);
		this.ElcotroncInvCK.TabIndex = 215;
		this.ElcotroncInvCK.Text = "تفعيل الفاتوره الالكترونيه";
		this.ElcotroncInvCK.UseVisualStyleBackColor = true;
		this.ElcotroncInvCK.MouseClick += new System.Windows.Forms.MouseEventHandler(ElcotroncInvCK_MouseClick);
		this.label42.AutoSize = true;
		this.label42.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label42.Location = new System.Drawing.Point(290, 35);
		this.label42.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label42.Name = "label42";
		this.label42.Size = new System.Drawing.Size(80, 17);
		this.label42.TabIndex = 214;
		this.label42.Text = "حاله التركيب";
		this.ComInstall.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.ComInstall.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.ComInstall.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ComInstall.FormattingEnabled = true;
		this.ComInstall.Location = new System.Drawing.Point(52, 31);
		this.ComInstall.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.ComInstall.Name = "ComInstall";
		this.ComInstall.Size = new System.Drawing.Size(234, 26);
		this.ComInstall.TabIndex = 213;
		this.ComInstall.SelectedIndexChanged += new System.EventHandler(ComInstall_SelectedIndexChanged);
		this.CkDisCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CkDisCode.AutoSize = true;
		this.CkDisCode.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CkDisCode.Location = new System.Drawing.Point(27, 222);
		this.CkDisCode.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CkDisCode.Name = "CkDisCode";
		this.CkDisCode.Size = new System.Drawing.Size(258, 21);
		this.CkDisCode.TabIndex = 212;
		this.CkDisCode.Text = "تفعيل كود الخصم في المبيعات والكاشير";
		this.CkDisCode.UseVisualStyleBackColor = true;
		this.CkBouns.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CkBouns.AutoSize = true;
		this.CkBouns.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CkBouns.Location = new System.Drawing.Point(497, 204);
		this.CkBouns.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CkBouns.Name = "CkBouns";
		this.CkBouns.Size = new System.Drawing.Size(181, 21);
		this.CkBouns.TabIndex = 211;
		this.CkBouns.Text = "تفعيل البونص فى المبيعات";
		this.CkBouns.UseVisualStyleBackColor = true;
		this.label41.AutoSize = true;
		this.label41.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label41.Location = new System.Drawing.Point(687, 155);
		this.label41.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label41.Name = "label41";
		this.label41.Size = new System.Drawing.Size(78, 17);
		this.label41.TabIndex = 210;
		this.label41.Text = "حاله الضريبه";
		this.ComVat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.ComVat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.ComVat.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ComVat.FormattingEnabled = true;
		this.ComVat.Location = new System.Drawing.Point(444, 151);
		this.ComVat.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.ComVat.Name = "ComVat";
		this.ComVat.Size = new System.Drawing.Size(234, 26);
		this.ComVat.TabIndex = 209;
		this.ComVat.SelectedValueChanged += new System.EventHandler(ComVat_SelectedValueChanged);
		this.TobaccoValue.BackColor = System.Drawing.Color.White;
		this.TobaccoValue.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TobaccoValue.Location = new System.Drawing.Point(41, 184);
		this.TobaccoValue.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TobaccoValue.Maximum = new decimal(new int[4] { 999999, 0, 0, 0 });
		this.TobaccoValue.Name = "TobaccoValue";
		this.TobaccoValue.ReadOnly = true;
		this.TobaccoValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TobaccoValue.Size = new System.Drawing.Size(84, 26);
		this.TobaccoValue.TabIndex = 207;
		this.TobaccoValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TobaccoValue.ValueChanged += new System.EventHandler(TobaccoValue_ValueChanged);
		this.label39.AutoSize = true;
		this.label39.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label39.Location = new System.Drawing.Point(152, 190);
		this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label39.Name = "label39";
		this.label39.Size = new System.Drawing.Size(136, 16);
		this.label39.TabIndex = 208;
		this.label39.Text = "الحد الادني لضريبه التبغ";
		this.combStorePurches.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.combStorePurches.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.combStorePurches.Font = new System.Drawing.Font("Tahoma", 8f);
		this.combStorePurches.FormattingEnabled = true;
		this.combStorePurches.Location = new System.Drawing.Point(52, 93);
		this.combStorePurches.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.combStorePurches.Name = "combStorePurches";
		this.combStorePurches.Size = new System.Drawing.Size(233, 24);
		this.combStorePurches.TabIndex = 203;
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label27.Location = new System.Drawing.Point(290, 99);
		this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(122, 19);
		this.label27.TabIndex = 204;
		this.label27.Text = "مخزن المشتريات";
		this.TxtPerVat.BackColor = System.Drawing.Color.White;
		this.TxtPerVat.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtPerVat.Location = new System.Drawing.Point(444, 90);
		this.TxtPerVat.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPerVat.Name = "TxtPerVat";
		this.TxtPerVat.ReadOnly = true;
		this.TxtPerVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerVat.Size = new System.Drawing.Size(234, 26);
		this.TxtPerVat.TabIndex = 200;
		this.TxtPerVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPerVat.Value = new decimal(new int[4] { 2, 0, 0, 0 });
		this.TxtPerVat.Validated += new System.EventHandler(TxtPerVat_Validated);
		this.TxtDecimalQty.BackColor = System.Drawing.Color.White;
		this.TxtDecimalQty.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtDecimalQty.Location = new System.Drawing.Point(444, 58);
		this.TxtDecimalQty.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDecimalQty.Maximum = new decimal(new int[4] { 6, 0, 0, 0 });
		this.TxtDecimalQty.Name = "TxtDecimalQty";
		this.TxtDecimalQty.ReadOnly = true;
		this.TxtDecimalQty.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDecimalQty.Size = new System.Drawing.Size(234, 26);
		this.TxtDecimalQty.TabIndex = 200;
		this.TxtDecimalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtDecimalQty.Value = new decimal(new int[4] { 2, 0, 0, 0 });
		this.CkHasm.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CkHasm.AutoSize = true;
		this.CkHasm.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CkHasm.Location = new System.Drawing.Point(493, 179);
		this.CkHasm.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CkHasm.Name = "CkHasm";
		this.CkHasm.Size = new System.Drawing.Size(185, 21);
		this.CkHasm.TabIndex = 68;
		this.CkHasm.Text = "تفعيل الحسم فى المبيعات";
		this.CkHasm.UseVisualStyleBackColor = true;
		this.TxtDecimal.BackColor = System.Drawing.Color.White;
		this.TxtDecimal.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtDecimal.Location = new System.Drawing.Point(52, 61);
		this.TxtDecimal.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDecimal.Maximum = new decimal(new int[4] { 6, 0, 0, 0 });
		this.TxtDecimal.Name = "TxtDecimal";
		this.TxtDecimal.ReadOnly = true;
		this.TxtDecimal.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDecimal.Size = new System.Drawing.Size(233, 26);
		this.TxtDecimal.TabIndex = 200;
		this.TxtDecimal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtDecimal.Value = new decimal(new int[4] { 2, 0, 0, 0 });
		this.label38.AutoSize = true;
		this.label38.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label38.Location = new System.Drawing.Point(686, 63);
		this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label38.Name = "label38";
		this.label38.Size = new System.Drawing.Size(164, 18);
		this.label38.TabIndex = 201;
		this.label38.Text = "الكسور العشرية  للكميات";
		this.label36.AutoSize = true;
		this.label36.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label36.Location = new System.Drawing.Point(686, 95);
		this.label36.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(149, 19);
		this.label36.TabIndex = 201;
		this.label36.Text = "نسبة القيمة المضافة";
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label23.Location = new System.Drawing.Point(290, 62);
		this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(158, 18);
		this.label23.TabIndex = 201;
		this.label23.Text = "الكسور العشرية للاسعار";
		this.groupBox2.Controls.Add(this.CBBackup);
		this.groupBox2.Controls.Add(this.lblcoo);
		this.groupBox2.Controls.Add(this.Connect);
		this.groupBox2.Controls.Add(this.BtnCopyMac);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtBackup);
		this.groupBox2.Location = new System.Drawing.Point(24, 311);
		this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox2.Size = new System.Drawing.Size(802, 144);
		this.groupBox2.TabIndex = 198;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "النسخ الاحتياطية لقواعد البيانات";
		this.CBBackup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBBackup.AutoSize = true;
		this.CBBackup.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBBackup.Location = new System.Drawing.Point(429, 91);
		this.CBBackup.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBBackup.Name = "CBBackup";
		this.CBBackup.Size = new System.Drawing.Size(252, 21);
		this.CBBackup.TabIndex = 197;
		this.CBBackup.Text = "اخذ نسخة احتياطية عند اغلاق البرنامج";
		this.CBBackup.UseVisualStyleBackColor = true;
		this.lblcoo.AutoSize = true;
		this.lblcoo.Font = new System.Drawing.Font("Tahoma", 8f);
		this.lblcoo.Location = new System.Drawing.Point(166, 108);
		this.lblcoo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblcoo.Name = "lblcoo";
		this.lblcoo.Size = new System.Drawing.Size(28, 17);
		this.lblcoo.TabIndex = 6;
		this.lblcoo.Text = ".....";
		this.Connect.AutoSize = true;
		this.Connect.Font = new System.Drawing.Font("Tahoma", 8f);
		this.Connect.Location = new System.Drawing.Point(27, 105);
		this.Connect.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Connect.Name = "Connect";
		this.Connect.Size = new System.Drawing.Size(28, 17);
		this.Connect.TabIndex = 6;
		this.Connect.Text = ".....";
		this.BtnCopyMac.Appearance.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.BtnCopyMac.Appearance.Options.UseForeColor = true;
		this.BtnCopyMac.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCopyMac.ImageOptions.Image");
		this.BtnCopyMac.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCopyMac.Location = new System.Drawing.Point(27, 39);
		this.BtnCopyMac.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCopyMac.Name = "BtnCopyMac";
		this.BtnCopyMac.Size = new System.Drawing.Size(167, 62);
		this.BtnCopyMac.TabIndex = 0;
		this.BtnCopyMac.Text = "Copy Mac Address";
		this.BtnCopyMac.Click += new System.EventHandler(BtnCopyMac_Click);
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label11.Location = new System.Drawing.Point(688, 54);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(89, 19);
		this.label11.TabIndex = 196;
		this.label11.Text = "مسار الحفظ";
		this.TxtBackup.EditValue = "D:\\Backup";
		this.TxtBackup.Location = new System.Drawing.Point(201, 52);
		this.TxtBackup.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtBackup.Name = "TxtBackup";
		this.TxtBackup.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtBackup.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtBackup.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBackup.Properties.Appearance.Options.UseFont = true;
		this.TxtBackup.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBackup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
		this.TxtBackup.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBackup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions2.Image");
		this.TxtBackup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.TxtBackup.Properties.ReadOnly = true;
		this.TxtBackup.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtBackup.Size = new System.Drawing.Size(472, 28);
		this.TxtBackup.TabIndex = 195;
		this.TxtBackup.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBackup_ButtonClick);
		this.CKGabr.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CKGabr.AutoSize = true;
		this.CKGabr.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CKGabr.Location = new System.Drawing.Point(648, 506);
		this.CKGabr.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CKGabr.Name = "CKGabr";
		this.CKGabr.Size = new System.Drawing.Size(166, 21);
		this.CKGabr.TabIndex = 71;
		this.CKGabr.Text = "تفعيل الجبر فى الكاشير";
		this.CKGabr.UseVisualStyleBackColor = true;
		this.CKGabr.Visible = false;
		this.CBTabocco.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBTabocco.AutoSize = true;
		this.CBTabocco.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBTabocco.Location = new System.Drawing.Point(109, 157);
		this.CBTabocco.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBTabocco.Name = "CBTabocco";
		this.CBTabocco.Size = new System.Drawing.Size(176, 21);
		this.CBTabocco.TabIndex = 71;
		this.CBTabocco.Text = "تفعيل ضريبة التبغ 100 %";
		this.CBTabocco.UseVisualStyleBackColor = true;
		this.CBTabocco.CheckedChanged += new System.EventHandler(CBTabocco_CheckedChanged);
		this.CBStoreWaiting.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.CBStoreWaiting.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.CBStoreWaiting.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBStoreWaiting.FormattingEnabled = true;
		this.CBStoreWaiting.Location = new System.Drawing.Point(52, 124);
		this.CBStoreWaiting.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBStoreWaiting.Name = "CBStoreWaiting";
		this.CBStoreWaiting.Size = new System.Drawing.Size(233, 24);
		this.CBStoreWaiting.TabIndex = 63;
		this.CBStoreWaiting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label4.Location = new System.Drawing.Point(290, 128);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(89, 19);
		this.label4.TabIndex = 64;
		this.label4.Text = "مخزن الحجز";
		this.combStore.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.combStore.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.combStore.Font = new System.Drawing.Font("Tahoma", 8f);
		this.combStore.FormattingEnabled = true;
		this.combStore.Location = new System.Drawing.Point(444, 122);
		this.combStore.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.combStore.Name = "combStore";
		this.combStore.Size = new System.Drawing.Size(234, 24);
		this.combStore.TabIndex = 59;
		this.combStore.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label3.Location = new System.Drawing.Point(686, 126);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(104, 17);
		this.label3.TabIndex = 62;
		this.label3.Text = "المخزن الرئيسي";
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label16.Location = new System.Drawing.Point(687, 31);
		this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(44, 17);
		this.label16.TabIndex = 61;
		this.label16.Text = "العملة";
		this.comboTafqit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboTafqit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.comboTafqit.Font = new System.Drawing.Font("Tahoma", 8f);
		this.comboTafqit.FormattingEnabled = true;
		this.comboTafqit.Location = new System.Drawing.Point(444, 27);
		this.comboTafqit.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.comboTafqit.Name = "comboTafqit";
		this.comboTafqit.Size = new System.Drawing.Size(234, 24);
		this.comboTafqit.TabIndex = 58;
		this.comboTafqit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		this.xtraTabPage3.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 9f);
		this.xtraTabPage3.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage3.Controls.Add(this.TxtPerGrid);
		this.xtraTabPage3.Controls.Add(this.label50);
		this.xtraTabPage3.Controls.Add(this.CkAlItemInRow);
		this.xtraTabPage3.Controls.Add(this.CKShowRoom);
		this.xtraTabPage3.Controls.Add(this.CkBarCodeRest);
		this.xtraTabPage3.Controls.Add(this.label21);
		this.xtraTabPage3.Controls.Add(this.comboPrint);
		this.xtraTabPage3.Controls.Add(this.TxtPerCashir);
		this.xtraTabPage3.Controls.Add(this.label45);
		this.xtraTabPage3.Controls.Add(this.CKIsShowRoomInFormRestaurant);
		this.xtraTabPage3.Controls.Add(this.CBISTakeDateApp);
		this.xtraTabPage3.Controls.Add(this.CBIsPrintSecondFatoraCashier);
		this.xtraTabPage3.Controls.Add(this.CBIsResturant);
		this.xtraTabPage3.Controls.Add(this.CBIsPrintItemCook1);
		this.xtraTabPage3.Controls.Add(this.Ckglass);
		this.xtraTabPage3.Controls.Add(this.ComImageShape);
		this.xtraTabPage3.Controls.Add(this.TxtFontSize);
		this.xtraTabPage3.Controls.Add(this.label40);
		this.xtraTabPage3.Controls.Add(this.separatorControl1);
		this.xtraTabPage3.Controls.Add(this.CBActiveImage);
		this.xtraTabPage3.Controls.Add(this.CBItemNameEN);
		this.xtraTabPage3.Controls.Add(this.CBActiveNameEN);
		this.xtraTabPage3.Controls.Add(this.CBItemPrice);
		this.xtraTabPage3.Controls.Add(this.CBActivePrice);
		this.xtraTabPage3.Controls.Add(this.CBItemNameAR);
		this.xtraTabPage3.Controls.Add(this.CBActiveNameAR);
		this.xtraTabPage3.Controls.Add(this.label10);
		this.xtraTabPage3.Controls.Add(this.CEGroupFontColor);
		this.xtraTabPage3.Controls.Add(this.label9);
		this.xtraTabPage3.Controls.Add(this.CEItemFontColor);
		this.xtraTabPage3.Controls.Add(this.label5);
		this.xtraTabPage3.Controls.Add(this.CEItemSize);
		this.xtraTabPage3.Controls.Add(this.label7);
		this.xtraTabPage3.Controls.Add(this.CEItemColor);
		this.xtraTabPage3.Controls.Add(this.label6);
		this.xtraTabPage3.Controls.Add(this.CEGroupSize);
		this.xtraTabPage3.Controls.Add(this.label20);
		this.xtraTabPage3.Controls.Add(this.CEGroupColor);
		this.xtraTabPage3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabPage3.Name = "xtraTabPage3";
		this.xtraTabPage3.Size = new System.Drawing.Size(846, 531);
		this.xtraTabPage3.Text = "اعدادت الكاشير";
		this.TxtPerGrid.DecimalPlaces = 2;
		this.TxtPerGrid.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtPerGrid.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerGrid.Location = new System.Drawing.Point(24, 44);
		this.TxtPerGrid.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPerGrid.Maximum = new decimal(new int[4] { 90, 0, 0, 0 });
		this.TxtPerGrid.Name = "TxtPerGrid";
		this.TxtPerGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerGrid.Size = new System.Drawing.Size(179, 26);
		this.TxtPerGrid.TabIndex = 230;
		this.TxtPerGrid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label50.AutoSize = true;
		this.label50.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label50.Location = new System.Drawing.Point(211, 49);
		this.label50.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label50.Name = "label50";
		this.label50.Size = new System.Drawing.Size(260, 19);
		this.label50.TabIndex = 229;
		this.label50.Text = "نسه حجم جريد الاصناف من الشاشه ";
		this.CkAlItemInRow.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CkAlItemInRow.AutoSize = true;
		this.CkAlItemInRow.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CkAlItemInRow.Location = new System.Drawing.Point(392, 499);
		this.CkAlItemInRow.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CkAlItemInRow.Name = "CkAlItemInRow";
		this.CkAlItemInRow.Size = new System.Drawing.Size(279, 21);
		this.CkAlItemInRow.TabIndex = 228;
		this.CkAlItemInRow.Text = "تفعيل جمع الاصناف المتشابه في صف واحد";
		this.CkAlItemInRow.UseVisualStyleBackColor = true;
		this.CKShowRoom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CKShowRoom.AutoSize = true;
		this.CKShowRoom.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CKShowRoom.Location = new System.Drawing.Point(42, 474);
		this.CKShowRoom.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CKShowRoom.Name = "CKShowRoom";
		this.CKShowRoom.Size = new System.Drawing.Size(211, 21);
		this.CKShowRoom.TabIndex = 227;
		this.CKShowRoom.Text = "اظهار الغرف في كاشير المطلعم";
		this.CKShowRoom.UseVisualStyleBackColor = true;
		this.CkBarCodeRest.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CkBarCodeRest.AutoSize = true;
		this.CkBarCodeRest.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CkBarCodeRest.Location = new System.Drawing.Point(492, 399);
		this.CkBarCodeRest.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CkBarCodeRest.Name = "CkBarCodeRest";
		this.CkBarCodeRest.Size = new System.Drawing.Size(179, 21);
		this.CkBarCodeRest.TabIndex = 226;
		this.CkBarCodeRest.Text = "تفعيل الباركود في المطعم";
		this.CkBarCodeRest.UseVisualStyleBackColor = true;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label21.Location = new System.Drawing.Point(655, 370);
		this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(91, 17);
		this.label21.TabIndex = 6;
		this.label21.Text = "اختيار الطابعة  ";
		this.comboPrint.FormattingEnabled = true;
		this.comboPrint.Location = new System.Drawing.Point(24, 367);
		this.comboPrint.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.comboPrint.Name = "comboPrint";
		this.comboPrint.Size = new System.Drawing.Size(625, 24);
		this.comboPrint.TabIndex = 49;
		this.TxtPerCashir.DecimalPlaces = 2;
		this.TxtPerCashir.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtPerCashir.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerCashir.Location = new System.Drawing.Point(24, 14);
		this.TxtPerCashir.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPerCashir.Name = "TxtPerCashir";
		this.TxtPerCashir.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerCashir.Size = new System.Drawing.Size(179, 26);
		this.TxtPerCashir.TabIndex = 225;
		this.TxtPerCashir.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPerCashir.ValueChanged += new System.EventHandler(TxtPerCashir_ValueChanged);
		this.label45.AutoSize = true;
		this.label45.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label45.Location = new System.Drawing.Point(211, 16);
		this.label45.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label45.Name = "label45";
		this.label45.Size = new System.Drawing.Size(239, 19);
		this.label45.TabIndex = 224;
		this.label45.Text = "نسبه الخصم في الكاشير والمطعم";
		this.CKIsShowRoomInFormRestaurant.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CKIsShowRoomInFormRestaurant.AutoSize = true;
		this.CKIsShowRoomInFormRestaurant.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CKIsShowRoomInFormRestaurant.Location = new System.Drawing.Point(351, 474);
		this.CKIsShowRoomInFormRestaurant.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CKIsShowRoomInFormRestaurant.Name = "CKIsShowRoomInFormRestaurant";
		this.CKIsShowRoomInFormRestaurant.Size = new System.Drawing.Size(320, 21);
		this.CKIsShowRoomInFormRestaurant.TabIndex = 222;
		this.CKIsShowRoomInFormRestaurant.Text = "تفعيل اظهار الغرف علي اعلي يسار شاشة المطعم";
		this.CKIsShowRoomInFormRestaurant.UseVisualStyleBackColor = true;
		this.CBISTakeDateApp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBISTakeDateApp.AutoSize = true;
		this.CBISTakeDateApp.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBISTakeDateApp.Location = new System.Drawing.Point(34, 448);
		this.CBISTakeDateApp.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBISTakeDateApp.Name = "CBISTakeDateApp";
		this.CBISTakeDateApp.Size = new System.Drawing.Size(219, 21);
		this.CBISTakeDateApp.TabIndex = 221;
		this.CBISTakeDateApp.Text = "تفعيل تاريخ البرنامج علي الكاشير";
		this.CBISTakeDateApp.UseVisualStyleBackColor = true;
		this.CBIsPrintSecondFatoraCashier.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBIsPrintSecondFatoraCashier.AutoSize = true;
		this.CBIsPrintSecondFatoraCashier.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBIsPrintSecondFatoraCashier.Location = new System.Drawing.Point(44, 398);
		this.CBIsPrintSecondFatoraCashier.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBIsPrintSecondFatoraCashier.Name = "CBIsPrintSecondFatoraCashier";
		this.CBIsPrintSecondFatoraCashier.Size = new System.Drawing.Size(209, 21);
		this.CBIsPrintSecondFatoraCashier.TabIndex = 220;
		this.CBIsPrintSecondFatoraCashier.Text = "تفعيل طباعه فاتوره الكابتن اوردر";
		this.CBIsPrintSecondFatoraCashier.UseVisualStyleBackColor = true;
		this.CBIsResturant.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBIsResturant.AutoSize = true;
		this.CBIsResturant.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBIsResturant.Location = new System.Drawing.Point(24, 423);
		this.CBIsResturant.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBIsResturant.Name = "CBIsResturant";
		this.CBIsResturant.Size = new System.Drawing.Size(229, 21);
		this.CBIsResturant.TabIndex = 219;
		this.CBIsResturant.Text = "تفعيل خصائص المطعم في الكاشير";
		this.CBIsResturant.UseVisualStyleBackColor = true;
		this.CBIsPrintItemCook1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBIsPrintItemCook1.AutoSize = true;
		this.CBIsPrintItemCook1.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBIsPrintItemCook1.Location = new System.Drawing.Point(485, 449);
		this.CBIsPrintItemCook1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBIsPrintItemCook1.Name = "CBIsPrintItemCook1";
		this.CBIsPrintItemCook1.Size = new System.Drawing.Size(186, 21);
		this.CBIsPrintItemCook1.TabIndex = 218;
		this.CBIsPrintItemCook1.Text = "تفعيل طباعة فاتورة المطبخ ";
		this.CBIsPrintItemCook1.UseVisualStyleBackColor = true;
		this.Ckglass.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Ckglass.AutoSize = true;
		this.Ckglass.Font = new System.Drawing.Font("Tahoma", 8f);
		this.Ckglass.Location = new System.Drawing.Point(519, 424);
		this.Ckglass.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Ckglass.Name = "Ckglass";
		this.Ckglass.Size = new System.Drawing.Size(152, 21);
		this.Ckglass.TabIndex = 217;
		this.Ckglass.Text = "تفعيل كاشير النظارات";
		this.Ckglass.UseVisualStyleBackColor = true;
		this.ComImageShape.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.ComImageShape.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.ComImageShape.Font = new System.Drawing.Font("Tahoma", 8f);
		this.ComImageShape.FormattingEnabled = true;
		this.ComImageShape.Location = new System.Drawing.Point(24, 330);
		this.ComImageShape.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.ComImageShape.Name = "ComImageShape";
		this.ComImageShape.Size = new System.Drawing.Size(624, 24);
		this.ComImageShape.TabIndex = 137;
		this.ComImageShape.SelectedIndexChanged += new System.EventHandler(ComImageShape_SelectedIndexChanged);
		this.TxtFontSize.DecimalPlaces = 2;
		this.TxtFontSize.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtFontSize.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtFontSize.Location = new System.Drawing.Point(479, 49);
		this.TxtFontSize.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtFontSize.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtFontSize.Name = "TxtFontSize";
		this.TxtFontSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtFontSize.Size = new System.Drawing.Size(192, 26);
		this.TxtFontSize.TabIndex = 135;
		this.TxtFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label40.AutoSize = true;
		this.label40.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label40.Location = new System.Drawing.Point(679, 50);
		this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label40.Name = "label40";
		this.label40.Size = new System.Drawing.Size(78, 19);
		this.label40.TabIndex = 136;
		this.label40.Text = "حجم الخط";
		this.separatorControl1.Location = new System.Drawing.Point(4, 139);
		this.separatorControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.separatorControl1.Name = "separatorControl1";
		this.separatorControl1.Size = new System.Drawing.Size(840, 18);
		this.separatorControl1.TabIndex = 134;
		this.CBActiveImage.AutoSize = true;
		this.CBActiveImage.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBActiveImage.Location = new System.Drawing.Point(656, 331);
		this.CBActiveImage.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBActiveImage.Name = "CBActiveImage";
		this.CBActiveImage.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CBActiveImage.Size = new System.Drawing.Size(103, 21);
		this.CBActiveImage.TabIndex = 133;
		this.CBActiveImage.Text = "تفعيل الصورة";
		this.CBActiveImage.UseVisualStyleBackColor = true;
		this.CBItemNameEN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.CBItemNameEN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.CBItemNameEN.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBItemNameEN.FormattingEnabled = true;
		this.CBItemNameEN.Location = new System.Drawing.Point(24, 261);
		this.CBItemNameEN.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBItemNameEN.Name = "CBItemNameEN";
		this.CBItemNameEN.Size = new System.Drawing.Size(625, 24);
		this.CBItemNameEN.TabIndex = 132;
		this.CBItemNameEN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		this.CBActiveNameEN.AutoSize = true;
		this.CBActiveNameEN.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBActiveNameEN.Location = new System.Drawing.Point(656, 264);
		this.CBActiveNameEN.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBActiveNameEN.Name = "CBActiveNameEN";
		this.CBActiveNameEN.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CBActiveNameEN.Size = new System.Drawing.Size(121, 21);
		this.CBActiveNameEN.TabIndex = 131;
		this.CBActiveNameEN.Text = "تفعيل الانجليزي";
		this.CBActiveNameEN.UseVisualStyleBackColor = true;
		this.CBActiveNameEN.CheckedChanged += new System.EventHandler(CBActiveNameEN_CheckedChanged);
		this.CBItemPrice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.CBItemPrice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.CBItemPrice.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBItemPrice.FormattingEnabled = true;
		this.CBItemPrice.Location = new System.Drawing.Point(24, 296);
		this.CBItemPrice.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBItemPrice.Name = "CBItemPrice";
		this.CBItemPrice.Size = new System.Drawing.Size(625, 24);
		this.CBItemPrice.TabIndex = 130;
		this.CBItemPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		this.CBActivePrice.AutoSize = true;
		this.CBActivePrice.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBActivePrice.Location = new System.Drawing.Point(656, 298);
		this.CBActivePrice.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBActivePrice.Name = "CBActivePrice";
		this.CBActivePrice.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CBActivePrice.Size = new System.Drawing.Size(102, 21);
		this.CBActivePrice.TabIndex = 129;
		this.CBActivePrice.Text = "تفعيل السعر";
		this.CBActivePrice.UseVisualStyleBackColor = true;
		this.CBActivePrice.CheckedChanged += new System.EventHandler(CBActivePrice_CheckedChanged);
		this.CBItemNameAR.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.CBItemNameAR.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.CBItemNameAR.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBItemNameAR.FormattingEnabled = true;
		this.CBItemNameAR.Location = new System.Drawing.Point(24, 225);
		this.CBItemNameAR.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBItemNameAR.Name = "CBItemNameAR";
		this.CBItemNameAR.Size = new System.Drawing.Size(625, 24);
		this.CBItemNameAR.TabIndex = 128;
		this.CBItemNameAR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		this.CBActiveNameAR.AutoSize = true;
		this.CBActiveNameAR.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBActiveNameAR.Location = new System.Drawing.Point(656, 227);
		this.CBActiveNameAR.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBActiveNameAR.Name = "CBActiveNameAR";
		this.CBActiveNameAR.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CBActiveNameAR.Size = new System.Drawing.Size(145, 21);
		this.CBActiveNameAR.TabIndex = 127;
		this.CBActiveNameAR.Text = "تفعيل الاسم العربي";
		this.CBActiveNameAR.UseVisualStyleBackColor = true;
		this.CBActiveNameAR.CheckedChanged += new System.EventHandler(CBActiveNameAR_CheckedChanged);
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(679, 115);
		this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(68, 19);
		this.label10.TabIndex = 126;
		this.label10.Text = "لون الخط";
		this.CEGroupFontColor.EditValue = System.Drawing.Color.Empty;
		this.CEGroupFontColor.Location = new System.Drawing.Point(24, 115);
		this.CEGroupFontColor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CEGroupFontColor.Name = "CEGroupFontColor";
		this.CEGroupFontColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.CEGroupFontColor.Size = new System.Drawing.Size(648, 22);
		this.CEGroupFontColor.TabIndex = 125;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(679, 191);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(68, 19);
		this.label9.TabIndex = 124;
		this.label9.Text = "لون الخط";
		this.CEItemFontColor.EditValue = System.Drawing.Color.Empty;
		this.CEItemFontColor.Location = new System.Drawing.Point(24, 191);
		this.CEItemFontColor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CEItemFontColor.Name = "CEItemFontColor";
		this.CEItemFontColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.CEItemFontColor.Size = new System.Drawing.Size(648, 22);
		this.CEItemFontColor.TabIndex = 123;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(357, 158);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(88, 19);
		this.label5.TabIndex = 122;
		this.label5.Text = "لون الاصناف";
		this.CEItemSize.DecimalPlaces = 2;
		this.CEItemSize.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CEItemSize.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.CEItemSize.Location = new System.Drawing.Point(479, 158);
		this.CEItemSize.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CEItemSize.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.CEItemSize.Name = "CEItemSize";
		this.CEItemSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CEItemSize.Size = new System.Drawing.Size(192, 26);
		this.CEItemSize.TabIndex = 120;
		this.CEItemSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(679, 158);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(98, 19);
		this.label7.TabIndex = 121;
		this.label7.Text = "حجم الاصناف";
		this.CEItemColor.EditValue = System.Drawing.Color.Empty;
		this.CEItemColor.Location = new System.Drawing.Point(24, 158);
		this.CEItemColor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CEItemColor.Name = "CEItemColor";
		this.CEItemColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.CEItemColor.Size = new System.Drawing.Size(325, 22);
		this.CEItemColor.TabIndex = 119;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(679, 83);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(111, 19);
		this.label6.TabIndex = 118;
		this.label6.Text = "لون المجموعات";
		this.CEGroupSize.DecimalPlaces = 2;
		this.CEGroupSize.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CEGroupSize.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.CEGroupSize.Location = new System.Drawing.Point(479, 15);
		this.CEGroupSize.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CEGroupSize.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.CEGroupSize.Name = "CEGroupSize";
		this.CEGroupSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.CEGroupSize.Size = new System.Drawing.Size(192, 26);
		this.CEGroupSize.TabIndex = 115;
		this.CEGroupSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.Location = new System.Drawing.Point(679, 16);
		this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(121, 19);
		this.label20.TabIndex = 116;
		this.label20.Text = "حجم المحموعات";
		this.CEGroupColor.EditValue = System.Drawing.Color.Empty;
		this.CEGroupColor.Location = new System.Drawing.Point(24, 83);
		this.CEGroupColor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CEGroupColor.Name = "CEGroupColor";
		this.CEGroupColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.CEGroupColor.Size = new System.Drawing.Size(648, 22);
		this.CEGroupColor.TabIndex = 68;
		this.xtraTabPage4.Controls.Add(this.label46);
		this.xtraTabPage4.Controls.Add(this.ComMizan);
		this.xtraTabPage4.Controls.Add(this.CBIsPrintItemCook);
		this.xtraTabPage4.Controls.Add(this.IsTimeInRestaurant);
		this.xtraTabPage4.Controls.Add(this.label26);
		this.xtraTabPage4.Controls.Add(this.checkItemTotalWithVat);
		this.xtraTabPage4.Controls.Add(this.TxtPassWordSupervisor);
		this.xtraTabPage4.Controls.Add(this.checkVat);
		this.xtraTabPage4.Controls.Add(this.TxtDiscount);
		this.xtraTabPage4.Controls.Add(this.label25);
		this.xtraTabPage4.Controls.Add(this.label24);
		this.xtraTabPage4.Controls.Add(this.groupBox5);
		this.xtraTabPage4.Controls.Add(this.groupBox4);
		this.xtraTabPage4.Margin = new System.Windows.Forms.Padding(4);
		this.xtraTabPage4.Name = "xtraTabPage4";
		this.xtraTabPage4.Size = new System.Drawing.Size(846, 531);
		this.xtraTabPage4.Text = "اعداد الخصم والطباعه في الكاشير";
		this.xtraTabPage4.Paint += new System.Windows.Forms.PaintEventHandler(xtraTabPage4_Paint);
		this.label46.AutoSize = true;
		this.label46.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label46.Location = new System.Drawing.Point(728, 171);
		this.label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label46.Name = "label46";
		this.label46.Size = new System.Drawing.Size(82, 17);
		this.label46.TabIndex = 254;
		this.label46.Text = "معدل الميزان";
		this.ComMizan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.ComMizan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.ComMizan.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ComMizan.FormattingEnabled = true;
		this.ComMizan.Location = new System.Drawing.Point(439, 164);
		this.ComMizan.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.ComMizan.Name = "ComMizan";
		this.ComMizan.Size = new System.Drawing.Size(281, 26);
		this.ComMizan.TabIndex = 253;
		this.CBIsPrintItemCook.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBIsPrintItemCook.AutoSize = true;
		this.CBIsPrintItemCook.Font = new System.Drawing.Font("Tahoma", 8f);
		this.CBIsPrintItemCook.Location = new System.Drawing.Point(614, 401);
		this.CBIsPrintItemCook.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBIsPrintItemCook.Name = "CBIsPrintItemCook";
		this.CBIsPrintItemCook.Size = new System.Drawing.Size(170, 21);
		this.CBIsPrintItemCook.TabIndex = 252;
		this.CBIsPrintItemCook.Text = "تفعيل طباعة المواد الخام";
		this.CBIsPrintItemCook.UseVisualStyleBackColor = true;
		this.CBIsPrintItemCook.Visible = false;
		this.IsTimeInRestaurant.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.IsTimeInRestaurant.AutoSize = true;
		this.IsTimeInRestaurant.Location = new System.Drawing.Point(635, 476);
		this.IsTimeInRestaurant.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.IsTimeInRestaurant.Name = "IsTimeInRestaurant";
		this.IsTimeInRestaurant.Size = new System.Drawing.Size(149, 20);
		this.IsTimeInRestaurant.TabIndex = 251;
		this.IsTimeInRestaurant.Text = "تفعيل الوقت في الغرف";
		this.IsTimeInRestaurant.UseVisualStyleBackColor = true;
		this.IsTimeInRestaurant.Visible = false;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label26.Location = new System.Drawing.Point(728, 216);
		this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(116, 19);
		this.label26.TabIndex = 250;
		this.label26.Text = "باسورد المشرف";
		this.checkItemTotalWithVat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.checkItemTotalWithVat.AutoSize = true;
		this.checkItemTotalWithVat.Font = new System.Drawing.Font("Tahoma", 8f);
		this.checkItemTotalWithVat.Location = new System.Drawing.Point(499, 451);
		this.checkItemTotalWithVat.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.checkItemTotalWithVat.Name = "checkItemTotalWithVat";
		this.checkItemTotalWithVat.Size = new System.Drawing.Size(285, 21);
		this.checkItemTotalWithVat.TabIndex = 206;
		this.checkItemTotalWithVat.Text = "تفعيل الاسعار شامل الضريبة في كرت الصنف";
		this.checkItemTotalWithVat.UseVisualStyleBackColor = true;
		this.checkItemTotalWithVat.Visible = false;
		this.checkItemTotalWithVat.CheckedChanged += new System.EventHandler(checkItemTotalWithVat_CheckedChanged);
		this.TxtPassWordSupervisor.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtPassWordSupervisor.Location = new System.Drawing.Point(439, 216);
		this.TxtPassWordSupervisor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPassWordSupervisor.Name = "TxtPassWordSupervisor";
		this.TxtPassWordSupervisor.PasswordChar = '#';
		this.TxtPassWordSupervisor.Size = new System.Drawing.Size(281, 28);
		this.TxtPassWordSupervisor.TabIndex = 249;
		this.TxtPassWordSupervisor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.checkVat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.checkVat.AutoSize = true;
		this.checkVat.Font = new System.Drawing.Font("Tahoma", 8f);
		this.checkVat.Location = new System.Drawing.Point(590, 426);
		this.checkVat.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.checkVat.Name = "checkVat";
		this.checkVat.Size = new System.Drawing.Size(194, 21);
		this.checkVat.TabIndex = 60;
		this.checkVat.Text = "تفعيل ضريبة القيمة المضافة ";
		this.checkVat.UseVisualStyleBackColor = true;
		this.checkVat.Visible = false;
		this.checkVat.CheckedChanged += new System.EventHandler(checkVat_CheckedChanged);
		this.TxtDiscount.DecimalPlaces = 2;
		this.TxtDiscount.Font = new System.Drawing.Font("Tahoma", 11f);
		this.TxtDiscount.ForeColor = System.Drawing.Color.Red;
		this.TxtDiscount.Location = new System.Drawing.Point(231, 451);
		this.TxtDiscount.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDiscount.Maximum = new decimal(new int[4] { 99, 0, 0, 0 });
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.Size = new System.Drawing.Size(107, 30);
		this.TxtDiscount.TabIndex = 248;
		this.TxtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtDiscount.Visible = false;
		this.label25.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Tahoma", 13f);
		this.label25.Location = new System.Drawing.Point(190, 453);
		this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(33, 27);
		this.label25.TabIndex = 247;
		this.label25.Text = "%";
		this.label25.Visible = false;
		this.label24.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label24.Location = new System.Drawing.Point(342, 455);
		this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(138, 19);
		this.label24.TabIndex = 246;
		this.label24.Text = "نسبة خصم المطعم";
		this.label24.Visible = false;
		this.groupBox5.Controls.Add(this.BtnMezan);
		this.groupBox5.Controls.Add(this.CBOrderStore);
		this.groupBox5.Controls.Add(this.CBIsSecureCashier);
		this.groupBox5.Controls.Add(this.CBPrintReturnOrder);
		this.groupBox5.Controls.Add(this.CBPrintOrder);
		this.groupBox5.Controls.Add(this.CBPrintCashier);
		this.groupBox5.Location = new System.Drawing.Point(4, 20);
		this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox5.Size = new System.Drawing.Size(840, 140);
		this.groupBox5.TabIndex = 5;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "رسايل الطباعه";
		this.BtnMezan.Location = new System.Drawing.Point(169, 89);
		this.BtnMezan.Margin = new System.Windows.Forms.Padding(4);
		this.BtnMezan.Name = "BtnMezan";
		this.BtnMezan.Properties.OffText = "أصناف الميزان بالسعر";
		this.BtnMezan.Properties.OnText = "أصناف الميزان بالوزن";
		this.BtnMezan.Size = new System.Drawing.Size(197, 24);
		this.BtnMezan.TabIndex = 11;
		this.CBOrderStore.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBOrderStore.AutoSize = true;
		this.CBOrderStore.Location = new System.Drawing.Point(169, 55);
		this.CBOrderStore.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBOrderStore.Name = "CBOrderStore";
		this.CBOrderStore.Size = new System.Drawing.Size(198, 20);
		this.CBOrderStore.TabIndex = 10;
		this.CBOrderStore.Text = "تفعيل أمر التحميل فى المبيعات";
		this.CBOrderStore.UseVisualStyleBackColor = true;
		this.CBIsSecureCashier.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBIsSecureCashier.AutoSize = true;
		this.CBIsSecureCashier.Location = new System.Drawing.Point(197, 23);
		this.CBIsSecureCashier.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBIsSecureCashier.Name = "CBIsSecureCashier";
		this.CBIsSecureCashier.Size = new System.Drawing.Size(170, 20);
		this.CBIsSecureCashier.TabIndex = 10;
		this.CBIsSecureCashier.Text = "تفعيل الحماية في الكاشير";
		this.CBIsSecureCashier.UseVisualStyleBackColor = true;
		this.CBPrintReturnOrder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBPrintReturnOrder.AutoSize = true;
		this.CBPrintReturnOrder.Location = new System.Drawing.Point(555, 89);
		this.CBPrintReturnOrder.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBPrintReturnOrder.Name = "CBPrintReturnOrder";
		this.CBPrintReturnOrder.Size = new System.Drawing.Size(226, 20);
		this.CBPrintReturnOrder.TabIndex = 9;
		this.CBPrintReturnOrder.Text = "تفعيل الطباعة في مرتجعات المبيعات";
		this.CBPrintReturnOrder.UseVisualStyleBackColor = true;
		this.CBPrintOrder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBPrintOrder.AutoSize = true;
		this.CBPrintOrder.Enabled = false;
		this.CBPrintOrder.Location = new System.Drawing.Point(600, 55);
		this.CBPrintOrder.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBPrintOrder.Name = "CBPrintOrder";
		this.CBPrintOrder.Size = new System.Drawing.Size(180, 20);
		this.CBPrintOrder.TabIndex = 8;
		this.CBPrintOrder.Text = "تفعيل طباعة الكاشير مباشر";
		this.CBPrintOrder.UseVisualStyleBackColor = true;
		this.CBPrintCashier.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CBPrintCashier.AutoSize = true;
		this.CBPrintCashier.Location = new System.Drawing.Point(610, 23);
		this.CBPrintCashier.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBPrintCashier.Name = "CBPrintCashier";
		this.CBPrintCashier.Size = new System.Drawing.Size(171, 20);
		this.CBPrintCashier.TabIndex = 7;
		this.CBPrintCashier.Text = "تفعيل الطباعة في الكاشير";
		this.CBPrintCashier.UseVisualStyleBackColor = true;
		this.groupBox4.Controls.Add(this.RBDiscountDecimal);
		this.groupBox4.Controls.Add(this.RBDiscountDecimalForCustomer);
		this.groupBox4.Controls.Add(this.RBNoDiscountDecimal);
		this.groupBox4.Location = new System.Drawing.Point(4, 166);
		this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox4.Size = new System.Drawing.Size(334, 129);
		this.groupBox4.TabIndex = 4;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = " اعدادات الخصم في الكاشير";
		this.groupBox4.Visible = false;
		this.RBDiscountDecimal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.RBDiscountDecimal.AutoSize = true;
		this.RBDiscountDecimal.Location = new System.Drawing.Point(43, 92);
		this.RBDiscountDecimal.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.RBDiscountDecimal.Name = "RBDiscountDecimal";
		this.RBDiscountDecimal.Size = new System.Drawing.Size(232, 20);
		this.RBDiscountDecimal.TabIndex = 3;
		this.RBDiscountDecimal.TabStop = true;
		this.RBDiscountDecimal.Text = "خصم الارقام العشرية (1.75 الي 1.00)";
		this.RBDiscountDecimal.UseVisualStyleBackColor = true;
		this.RBDiscountDecimalForCustomer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.RBDiscountDecimalForCustomer.AutoSize = true;
		this.RBDiscountDecimalForCustomer.Location = new System.Drawing.Point(49, 60);
		this.RBDiscountDecimalForCustomer.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.RBDiscountDecimalForCustomer.Name = "RBDiscountDecimalForCustomer";
		this.RBDiscountDecimalForCustomer.Size = new System.Drawing.Size(225, 20);
		this.RBDiscountDecimalForCustomer.TabIndex = 2;
		this.RBDiscountDecimalForCustomer.TabStop = true;
		this.RBDiscountDecimalForCustomer.Text = "خصم الرقم العشري (1.75 الي 1.70)";
		this.RBDiscountDecimalForCustomer.UseVisualStyleBackColor = true;
		this.RBNoDiscountDecimal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.RBNoDiscountDecimal.AutoSize = true;
		this.RBNoDiscountDecimal.Location = new System.Drawing.Point(101, 28);
		this.RBNoDiscountDecimal.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.RBNoDiscountDecimal.Name = "RBNoDiscountDecimal";
		this.RBNoDiscountDecimal.Size = new System.Drawing.Size(175, 20);
		this.RBNoDiscountDecimal.TabIndex = 1;
		this.RBNoDiscountDecimal.TabStop = true;
		this.RBNoDiscountDecimal.Text = "بدون خصم الكسور العشرية";
		this.RBNoDiscountDecimal.UseVisualStyleBackColor = true;
		this.xtraTabPage5.Controls.Add(this.groupBox6);
		this.xtraTabPage5.Margin = new System.Windows.Forms.Padding(4);
		this.xtraTabPage5.Name = "xtraTabPage5";
		this.xtraTabPage5.Size = new System.Drawing.Size(846, 531);
		this.xtraTabPage5.Text = "المهام";
		this.groupBox6.Controls.Add(this.DgvRemember);
		this.groupBox6.Controls.Add(this.BtnDeleteRem);
		this.groupBox6.Controls.Add(this.BtnEditRem);
		this.groupBox6.Controls.Add(this.BtnNewRem);
		this.groupBox6.Controls.Add(this.BtnSaveRem);
		this.groupBox6.Controls.Add(this.TxtRemember);
		this.groupBox6.Controls.Add(this.TxtDateRem);
		this.groupBox6.Controls.Add(this.label37);
		this.groupBox6.Controls.Add(this.label32);
		this.groupBox6.Controls.Add(this.ckRemember);
		this.groupBox6.Location = new System.Drawing.Point(2, 17);
		this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox6.Size = new System.Drawing.Size(840, 194);
		this.groupBox6.TabIndex = 6;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "التذكير";
		this.DgvRemember.AllowUserToAddRows = false;
		this.DgvRemember.AllowUserToDeleteRows = false;
		this.DgvRemember.BackgroundColor = System.Drawing.Color.White;
		this.DgvRemember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DgvRemember.Location = new System.Drawing.Point(22, 49);
		this.DgvRemember.Margin = new System.Windows.Forms.Padding(4);
		this.DgvRemember.MultiSelect = false;
		this.DgvRemember.Name = "DgvRemember";
		this.DgvRemember.ReadOnly = true;
		this.DgvRemember.RowHeadersWidth = 51;
		this.DgvRemember.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DgvRemember.Size = new System.Drawing.Size(394, 89);
		this.DgvRemember.TabIndex = 117;
		this.DgvRemember.Click += new System.EventHandler(DgvRemember_Click);
		this.BtnDeleteRem.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDeleteRem.ImageOptions.Image");
		this.BtnDeleteRem.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDeleteRem.Location = new System.Drawing.Point(22, 148);
		this.BtnDeleteRem.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDeleteRem.Name = "BtnDeleteRem";
		this.BtnDeleteRem.Size = new System.Drawing.Size(149, 27);
		this.BtnDeleteRem.TabIndex = 0;
		this.BtnDeleteRem.Text = "حذف";
		this.BtnDeleteRem.Click += new System.EventHandler(BtnDeleteRem_Click);
		this.BtnEditRem.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEditRem.ImageOptions.Image");
		this.BtnEditRem.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEditRem.Location = new System.Drawing.Point(267, 148);
		this.BtnEditRem.Margin = new System.Windows.Forms.Padding(4);
		this.BtnEditRem.Name = "BtnEditRem";
		this.BtnEditRem.Size = new System.Drawing.Size(149, 27);
		this.BtnEditRem.TabIndex = 0;
		this.BtnEditRem.Text = "تعديل";
		this.BtnEditRem.Click += new System.EventHandler(BtnEditRem_Click);
		this.BtnNewRem.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewRem.ImageOptions.Image");
		this.BtnNewRem.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewRem.Location = new System.Drawing.Point(580, 148);
		this.BtnNewRem.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNewRem.Name = "BtnNewRem";
		this.BtnNewRem.Size = new System.Drawing.Size(149, 27);
		this.BtnNewRem.TabIndex = 0;
		this.BtnNewRem.Text = "جديد";
		this.BtnNewRem.Click += new System.EventHandler(BtnNewRem_Click);
		this.BtnSaveRem.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSaveRem.ImageOptions.Image");
		this.BtnSaveRem.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSaveRem.Location = new System.Drawing.Point(424, 148);
		this.BtnSaveRem.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSaveRem.Name = "BtnSaveRem";
		this.BtnSaveRem.Size = new System.Drawing.Size(149, 27);
		this.BtnSaveRem.TabIndex = 0;
		this.BtnSaveRem.Text = "حفظ ";
		this.BtnSaveRem.Click += new System.EventHandler(BtnSaveRem_Click);
		this.TxtRemember.Location = new System.Drawing.Point(429, 80);
		this.TxtRemember.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtRemember.Multiline = true;
		this.TxtRemember.Name = "TxtRemember";
		this.TxtRemember.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtRemember.Size = new System.Drawing.Size(299, 57);
		this.TxtRemember.TabIndex = 116;
		this.TxtRemember.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtDateRem.EditValue = null;
		this.TxtDateRem.Location = new System.Drawing.Point(429, 48);
		this.TxtDateRem.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDateRem.Name = "TxtDateRem";
		this.TxtDateRem.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateRem.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDateRem.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateRem.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDateRem.Properties.Appearance.Options.UseFont = true;
		this.TxtDateRem.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateRem.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateRem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateRem.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateRem.Properties.Mask.EditMask = "yyyy/MM/dd HH:mm:ss tt";
		this.TxtDateRem.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDateRem.Size = new System.Drawing.Size(300, 28);
		this.TxtDateRem.TabIndex = 114;
		this.label37.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label37.Location = new System.Drawing.Point(736, 98);
		this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(55, 21);
		this.label37.TabIndex = 115;
		this.label37.Text = "التذكير";
		this.label32.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label32.Location = new System.Drawing.Point(735, 49);
		this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(110, 21);
		this.label32.TabIndex = 115;
		this.label32.Text = "التاريخ ظˆ الوقت";
		this.ckRemember.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ckRemember.AutoSize = true;
		this.ckRemember.Checked = true;
		this.ckRemember.CheckState = System.Windows.Forms.CheckState.Checked;
		this.ckRemember.Location = new System.Drawing.Point(707, 22);
		this.ckRemember.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.ckRemember.Name = "ckRemember";
		this.ckRemember.Size = new System.Drawing.Size(99, 20);
		this.ckRemember.TabIndex = 7;
		this.ckRemember.Text = "تفعيل التذكير";
		this.ckRemember.UseVisualStyleBackColor = true;
		this.ckRemember.CheckedChanged += new System.EventHandler(ckRemember_CheckedChanged);
		this.xtraTabPage6.Controls.Add(this.groupBox3);
		this.xtraTabPage6.Name = "xtraTabPage6";
		this.xtraTabPage6.Size = new System.Drawing.Size(846, 531);
		this.xtraTabPage6.Text = "واتساب";
		this.groupBox3.Controls.Add(this.BtnNew);
		this.groupBox3.Controls.Add(this.TxtAccountSid);
		this.groupBox3.Controls.Add(this.BtnSid);
		this.groupBox3.Controls.Add(this.TxtToken);
		this.groupBox3.Controls.Add(this.BtnPastTocken);
		this.groupBox3.Controls.Add(this.label49);
		this.groupBox3.Controls.Add(this.label48);
		this.groupBox3.Controls.Add(this.TxtPhoneSent);
		this.groupBox3.Controls.Add(this.label47);
		this.groupBox3.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox3.Location = new System.Drawing.Point(3, 38);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(840, 260);
		this.groupBox3.TabIndex = 219;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "اعدادات ارسال الفواتير واتساب";
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(666, 215);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(149, 36);
		this.BtnNew.TabIndex = 222;
		this.BtnNew.Text = "جديد";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.TxtAccountSid.BackColor = System.Drawing.Color.White;
		this.TxtAccountSid.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtAccountSid.Location = new System.Drawing.Point(116, 165);
		this.TxtAccountSid.Name = "TxtAccountSid";
		this.TxtAccountSid.ReadOnly = true;
		this.TxtAccountSid.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtAccountSid.Size = new System.Drawing.Size(625, 26);
		this.TxtAccountSid.TabIndex = 216;
		this.TxtAccountSid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtAccountSid.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtAccountSid_KeyDown);
		this.BtnSid.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSid.ImageOptions.Image");
		this.BtnSid.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSid.Location = new System.Drawing.Point(7, 165);
		this.BtnSid.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSid.Name = "BtnSid";
		this.BtnSid.Size = new System.Drawing.Size(103, 27);
		this.BtnSid.TabIndex = 221;
		this.BtnSid.Text = "لصق";
		this.BtnSid.Click += new System.EventHandler(BtnSid_Click);
		this.TxtToken.BackColor = System.Drawing.Color.White;
		this.TxtToken.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtToken.Location = new System.Drawing.Point(116, 101);
		this.TxtToken.Name = "TxtToken";
		this.TxtToken.ReadOnly = true;
		this.TxtToken.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtToken.Size = new System.Drawing.Size(625, 26);
		this.TxtToken.TabIndex = 215;
		this.TxtToken.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtToken.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtToken_KeyDown);
		this.BtnPastTocken.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPastTocken.ImageOptions.Image");
		this.BtnPastTocken.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPastTocken.Location = new System.Drawing.Point(6, 102);
		this.BtnPastTocken.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPastTocken.Name = "BtnPastTocken";
		this.BtnPastTocken.Size = new System.Drawing.Size(103, 26);
		this.BtnPastTocken.TabIndex = 220;
		this.BtnPastTocken.Text = "لصق";
		this.BtnPastTocken.Click += new System.EventHandler(BtnPastTocken_Click);
		this.label49.AutoSize = true;
		this.label49.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label49.Location = new System.Drawing.Point(746, 170);
		this.label49.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label49.Name = "label49";
		this.label49.Size = new System.Drawing.Size(77, 17);
		this.label49.TabIndex = 214;
		this.label49.Text = "AccountSid";
		this.label48.AutoSize = true;
		this.label48.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label48.Location = new System.Drawing.Point(748, 106);
		this.label48.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label48.Name = "label48";
		this.label48.Size = new System.Drawing.Size(75, 17);
		this.label48.TabIndex = 213;
		this.label48.Text = "AuthToken";
		this.TxtPhoneSent.BackColor = System.Drawing.Color.White;
		this.TxtPhoneSent.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtPhoneSent.Location = new System.Drawing.Point(6, 37);
		this.TxtPhoneSent.Name = "TxtPhoneSent";
		this.TxtPhoneSent.ReadOnly = true;
		this.TxtPhoneSent.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPhoneSent.Size = new System.Drawing.Size(733, 26);
		this.TxtPhoneSent.TabIndex = 212;
		this.TxtPhoneSent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label47.AutoSize = true;
		this.label47.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label47.Location = new System.Drawing.Point(746, 40);
		this.label47.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label47.Name = "label47";
		this.label47.Size = new System.Drawing.Size(77, 17);
		this.label47.TabIndex = 211;
		this.label47.Text = "رقم الارسال";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(884, 702);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		base.MaximizeBox = false;
		base.Name = "FrmSetting";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "اعدادات البرنامج";
		base.Load += new System.EventHandler(FrmSetting_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FrmSetting_KeyDown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TapPrice).EndInit();
		this.TapPrice.ResumeLayout(false);
		this.xtraTabPage1.ResumeLayout(false);
		this.xtraTabPage1.PerformLayout();
		this.xtraTabPage2.ResumeLayout(false);
		this.xtraTabPage2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TobaccoValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerVat).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDecimalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDecimal).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtBackup.Properties).EndInit();
		this.xtraTabPage3.ResumeLayout(false);
		this.xtraTabPage3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtPerGrid).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerCashir).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtFontSize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CEGroupFontColor.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CEItemFontColor.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CEItemSize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CEItemColor.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CEGroupSize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CEGroupColor.Properties).EndInit();
		this.xtraTabPage4.ResumeLayout(false);
		this.xtraTabPage4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDiscount).EndInit();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnMezan.Properties).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.xtraTabPage5.ResumeLayout(false);
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.DgvRemember).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRem.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateRem.Properties).EndInit();
		this.xtraTabPage6.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		base.ResumeLayout(false);
	}
}


