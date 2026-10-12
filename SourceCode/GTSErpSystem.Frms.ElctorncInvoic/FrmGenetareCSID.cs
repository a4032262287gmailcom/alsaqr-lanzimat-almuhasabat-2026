using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Dto;
using GTSErpSystem.Helper;
using Janus.Windows.EditControls;
using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.APIHelper;
using ZatcaIntegrationSDK.HelperContracts;

namespace GTSErpSystem.Frms.ElctorncInvoic;

public class FrmGenetareCSID : XtraForm
{
	private bool ISChange = false;

	private string _Mode;

	private Class_Users USR = new Class_Users();

	private Class_Setting Userx = new Class_Setting();

	private Class_WaitFormFunc waitForm = new Class_WaitFormFunc();

	private IContainer components = null;

	private UIGroupBox uiGroupBox1;

	private Label label1;

	private Label label2;

	private GroupBox groupBox1;

	private CheckBox CKUsers;

	private System.Windows.Forms.ComboBox CBUserName;

	private TextBox TxtOtp;

	private Label label3;

	private Label label4;

	private RadioButton CheckSimulation;

	private RadioButton CheckProduction;

	private Label label5;

	private Label label6;

	private GroupBox groupBox2;

	private Label label7;

	private Label label8;

	private TextBox TxtHull;

	private TextBox TxtNum;

	private Label label9;

	private TextBox TxtVatNum;

	private Label label10;

	private Label label11;

	private TextBox TxtBranch;

	private Label label13;

	private System.Windows.Forms.ComboBox CBInvoicType;

	private TextBox TxtCountryCode;

	private Label label12;

	private Label label15;

	private TextBox TxtTypeApp;

	private TextBox TxtAddress;

	private Label label14;

	private SimpleButton BtnNew;

	private SimpleButton BtnCreate;

	private Label label16;

	private TextBox TxtCrs;

	private TextBox TxtPrivateKey;

	private Label label17;

	private GroupBox TxtSecret;

	private TextBox TxtSecrt;

	private Label label19;

	private Label label18;

	private TextBox TxtPublicKey;

	private SimpleButton BtnSave;

	private Mode mode { get; set; }

	public FrmGenetareCSID()
	{
		InitializeComponent();
		Bind();
	}

	private void Bind()
	{
		CBUserName.DataSource = (from x in USR.GetAllUsers()
			where x.BranchID == LoginDetails.BranchID
			select x).ToList();
		CBUserName.DisplayMember = "Name";
		CBUserName.ValueMember = "ID";
		CBUserName.DropDownStyle = ComboBoxStyle.DropDownList;
		CBUserName.SelectedValue = LoginDetails.UserID;
		if (LoginDetails.CsIdForUser)
		{
			CKUsers.Checked = true;
		}
		List<InvoicType> dataSource = new List<InvoicType>
		{
			new InvoicType
			{
				Text = "Standard &Simplified Invoices ** فاتورة ضريبية & مبسطة ",
				Value = "1100"
			},
			new InvoicType
			{
				Text = "Standard Invoices Only ** فاتورة ضريبية فقط ",
				Value = "1000"
			},
			new InvoicType
			{
				Text = "Simplified Invoices Only ** فاتورة مبسطة فقط",
				Value = "0100"
			}
		};
		CBInvoicType.DisplayMember = "Text";
		CBInvoicType.ValueMember = "Value";
		CBInvoicType.DataSource = dataSource;
		CBInvoicType.DropDownStyle = ComboBoxStyle.DropDownList;
		TxtBranch.Text = USR.GetBranchById(LoginDetails.BranchID);
		TxtAddress.Text = LoginDetails.Address;
		TxtVatNum.Text = LoginDetails.VatNum;
		TxtCrs.Text = LoginDetails.Crs;
		TxtPrivateKey.Text = LoginDetails.PrivateKey;
		TxtPublicKey.Text = LoginDetails.PublicKey;
		TxtSecrt.Text = LoginDetails.SecretKey;
		TxtNum.Text = "1-MajedSoftDiskTop|2-Version 1|3-" + Guid.NewGuid().ToString();
		CheckSimulation.Checked = true;
		if (CKUsers.Checked)
		{
			CBUserName.Visible = true;
		}
		else
		{
			CBUserName.Visible = false;
		}
		ISChange = true;
	}

	private void CKUsers_CheckedChanged(object sender, EventArgs e)
	{
		if (CKUsers.Checked)
		{
			CBUserName.Visible = true;
		}
		else
		{
			CBUserName.Visible = false;
		}
	}

	private void Clear()
	{
		TxtCrs.Text = string.Empty;
		TxtPrivateKey.Text = string.Empty;
		TxtPublicKey.Text = string.Empty;
		TxtSecrt.Text = string.Empty;
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		Clear();
	}

	private void CBUserName_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (ISChange)
		{
			User_Login userById = USR.GetUserById(Convert.ToInt32(CBUserName.SelectedValue));
			LoginDetails.Crs = userById.Crs;
			LoginDetails.PrivateKey = userById.PrivateKey;
			LoginDetails.PublicKey = userById.PublicKey;
			LoginDetails.SecretKey = userById.SecretKey;
			TxtCrs.Text = LoginDetails.Crs;
			TxtPrivateKey.Text = LoginDetails.PrivateKey;
			TxtPublicKey.Text = LoginDetails.PublicKey;
			TxtSecrt.Text = LoginDetails.SecretKey;
		}
	}

	private void BtnCreate_Click(object sender, EventArgs e)
	{
		try
		{
			waitForm.Show();
			CertificateRequestDto certificateRequestDto = GenerateCsId(ElectronicInvoiceInfo.SupplierPartyDto(), CertificateRequestDto());
			if (!string.IsNullOrWhiteSpace(certificateRequestDto.ErrorMessage))
			{
				waitForm.Close();
				MessageBox.Show(certificateRequestDto.ErrorMessage, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			TxtCrs.Text = certificateRequestDto.Csr;
			TxtPrivateKey.Text = certificateRequestDto.Privatekey;
			TxtPublicKey.Text = certificateRequestDto.Publickey;
			TxtSecrt.Text = certificateRequestDto.Secretkey;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		finally
		{
			waitForm.Close();
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		string modeInvoice = "developer";
		if (CheckSimulation.Checked)
		{
			modeInvoice = "Simulation";
		}
		else if (CheckProduction.Checked)
		{
			modeInvoice = "Production";
		}
		int num = 0;
		if (!CKUsers.Checked)
		{
			num = Userx.UpdateToElctorncInvoic(TxtCrs.Text, TxtPrivateKey.Text, TxtPublicKey.Text, TxtSecrt.Text, CsIdForUser: false, modeInvoice);
			LoginDetails.CsIdForUser = false;
		}
		else
		{
			num = USR.UpdateToElctroncInvoic(Convert.ToInt32(CBUserName.SelectedValue), CBUserName.Text, TxtCrs.Text, TxtPrivateKey.Text, TxtPublicKey.Text, TxtSecrt.Text, modeInvoice);
			Userx.UpdateToElctorncInvoicUser();
			LoginDetails.CsIdForUser = true;
		}
		if (num > 0)
		{
			LoginDetails.Crs = TxtCrs.Text;
			LoginDetails.PrivateKey = TxtPrivateKey.Text;
			LoginDetails.PublicKey = TxtPublicKey.Text;
			LoginDetails.SecretKey = TxtSecrt.Text;
			LoginDetails.ModeInvoice = modeInvoice;
			MessageBox.Show("تم حفظ بيانات اعدادات الفاتوره الالكتونيه بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private CertificateRequestDto GenerateCsId(SupplierPartyDto supplier, CertificateRequestDto requestDto)
	{
		ZatcaIntegrationSDK.Invoice invoice = new ZatcaIntegrationSDK.Invoice();
		invoice.ID = "INV00001";
		invoice.IssueDate = DateTime.Now.ToString("yyyy-MM-dd");
		invoice.IssueTime = DateTime.Now.ToString("HH:mm:ss");
		invoice.DocumentCurrencyCode = "SAR";
		invoice.TaxCurrencyCode = "SAR";
		if (invoice.invoiceTypeCode.id == 383 || invoice.invoiceTypeCode.id == 381)
		{
			InvoiceDocumentReference invoiceDocumentReference = new InvoiceDocumentReference();
			invoiceDocumentReference.ID = "Invoice Number: 354; Invoice Issue Date: 2021-02-10";
			invoice.billingReference.invoiceDocumentReferences.Add(invoiceDocumentReference);
		}
		invoice.AdditionalDocumentReferencePIH.EmbeddedDocumentBinaryObject = "NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==";
		invoice.AdditionalDocumentReferenceICV.UUID = 123L;
		PaymentMeans paymentMeans = new PaymentMeans();
		paymentMeans.PaymentMeansCode = "10";
		paymentMeans.InstructionNote = "Payment Notes";
		invoice.paymentmeans.Add(paymentMeans);
		invoice.SupplierParty.partyIdentification.ID = supplier.ID;
		invoice.SupplierParty.partyIdentification.schemeID = supplier.schemeID;
		invoice.SupplierParty.postalAddress.StreetName = supplier.StreetName;
		invoice.SupplierParty.postalAddress.AdditionalStreetName = supplier.AdditionalStreetName;
		invoice.SupplierParty.postalAddress.BuildingNumber = supplier.BuildingNumber;
		invoice.SupplierParty.postalAddress.PlotIdentification = supplier.PlotIdentification;
		invoice.SupplierParty.postalAddress.CityName = supplier.CityName;
		invoice.SupplierParty.postalAddress.PostalZone = supplier.PostalZone;
		invoice.SupplierParty.postalAddress.CountrySubentity = supplier.CountrySubentity;
		invoice.SupplierParty.postalAddress.CitySubdivisionName = supplier.CitySubdivisionName;
		invoice.SupplierParty.postalAddress.country.IdentificationCode = supplier.IdentificationCode;
		invoice.SupplierParty.partyLegalEntity.RegistrationName = supplier.RegistrationName;
		invoice.SupplierParty.partyTaxScheme.CompanyID = supplier.CompanyID;
		invoice.CustomerParty.partyIdentification.ID = "1234567";
		invoice.CustomerParty.partyIdentification.schemeID = "CRN";
		invoice.CustomerParty.postalAddress.StreetName = "شارع تجربة";
		invoice.CustomerParty.postalAddress.AdditionalStreetName = "شارع اضافى";
		invoice.CustomerParty.postalAddress.BuildingNumber = "1234";
		invoice.CustomerParty.postalAddress.PlotIdentification = "9833";
		invoice.CustomerParty.postalAddress.CityName = "Jeddah";
		invoice.CustomerParty.postalAddress.PostalZone = "12345";
		invoice.CustomerParty.postalAddress.CountrySubentity = "Makkah";
		invoice.CustomerParty.postalAddress.CitySubdivisionName = "المحافظة";
		invoice.CustomerParty.postalAddress.country.IdentificationCode = "SA";
		invoice.CustomerParty.partyLegalEntity.RegistrationName = "اسم شركة المشترى";
		invoice.CustomerParty.partyTaxScheme.CompanyID = "310424415000003";
		invoice.legalMonetaryTotal.PrepaidAmount = 0m;
		invoice.legalMonetaryTotal.PayableAmount = 0m;
		InvoiceLine invoiceLine = new InvoiceLine();
		invoiceLine.InvoiceQuantity = 1m;
		invoiceLine.item.Name = "منتج تجربة";
		invoiceLine.item.classifiedTaxCategory.ID = "S";
		invoiceLine.taxTotal.TaxSubtotal.taxCategory.ID = "S";
		invoiceLine.item.classifiedTaxCategory.Percent = 15m;
		invoiceLine.taxTotal.TaxSubtotal.taxCategory.Percent = 15m;
		invoiceLine.price.PriceAmount = 120m;
		invoice.InvoiceLines.Add(invoiceLine);
		CertificateRequest cSRRequest = GetCSRRequest(requestDto);
		if (requestDto.Mode == "Simulation")
		{
			mode = Mode.Simulation;
		}
		else if (requestDto.Mode == "Production")
		{
			mode = Mode.Production;
		}
		else
		{
			mode = Mode.developer;
		}
		CSIDGenerator cSIDGenerator = new CSIDGenerator(mode);
		string directorypath = Directory.GetCurrentDirectory() + "\\Invoices";
		CertificateResponse certificateResponse = cSIDGenerator.GenerateCSID(cSRRequest, invoice, directorypath);
		if (certificateResponse.IsSuccess)
		{
			requestDto.Csr = certificateResponse.CSR;
			requestDto.Privatekey = certificateResponse.PrivateKey;
			requestDto.Publickey = certificateResponse.CSID;
			requestDto.Secretkey = certificateResponse.SecretKey;
		}
		else
		{
			requestDto.ErrorMessage = certificateResponse.ErrorMessage;
		}
		return requestDto;
	}

	private CertificateRequest GetCSRRequest(CertificateRequestDto requestDto)
	{
		CertificateRequest certificateRequest = new CertificateRequest();
		certificateRequest.OTP = requestDto.OTP;
		certificateRequest.CommonName = requestDto.CommonName;
		certificateRequest.OrganizationName = requestDto.OrganizationName;
		certificateRequest.OrganizationUnitName = requestDto.OrganizationUnitName;
		certificateRequest.CountryName = requestDto.CountryName;
		certificateRequest.SerialNumber = requestDto.SerialNumber;
		certificateRequest.OrganizationIdentifier = requestDto.OrganizationIdentifier;
		certificateRequest.Location = requestDto.Location;
		certificateRequest.BusinessCategory = requestDto.BusinessCategory;
		certificateRequest.InvoiceType = requestDto.InvoiceType;
		return certificateRequest;
	}

	private CertificateRequestDto CertificateRequestDto()
	{
		if (CheckProduction.Checked)
		{
			_Mode = "Production";
		}
		else
		{
			_Mode = "Simulation";
		}
		CertificateRequestDto certificateRequestDto = new CertificateRequestDto();
		certificateRequestDto.OTP = TxtOtp.Text;
		certificateRequestDto.CommonName = TxtHull.Text;
		certificateRequestDto.OrganizationName = TxtBranch.Text;
		certificateRequestDto.OrganizationUnitName = TxtVatNum.Text;
		certificateRequestDto.CountryName = TxtCountryCode.Text;
		certificateRequestDto.SerialNumber = TxtNum.Text;
		certificateRequestDto.OrganizationIdentifier = TxtVatNum.Text;
		certificateRequestDto.Location = TxtAddress.Text;
		certificateRequestDto.BusinessCategory = TxtTypeApp.Text;
		certificateRequestDto.InvoiceType = CBInvoicType.SelectedValue.ToString();
		certificateRequestDto.Mode = _Mode;
		return certificateRequestDto;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.ElctorncInvoic.FrmGenetareCSID));
		this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtOtp = new System.Windows.Forms.TextBox();
		this.CBUserName = new System.Windows.Forms.ComboBox();
		this.CKUsers = new System.Windows.Forms.CheckBox();
		this.CheckSimulation = new System.Windows.Forms.RadioButton();
		this.CheckProduction = new System.Windows.Forms.RadioButton();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnCreate = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtTypeApp = new System.Windows.Forms.TextBox();
		this.TxtAddress = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.CBInvoicType = new System.Windows.Forms.ComboBox();
		this.TxtCountryCode = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtBranch = new System.Windows.Forms.TextBox();
		this.TxtVatNum = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtNum = new System.Windows.Forms.TextBox();
		this.TxtHull = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.TxtCrs = new System.Windows.Forms.TextBox();
		this.TxtPrivateKey = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtSecret = new System.Windows.Forms.GroupBox();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.TxtSecrt = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.TxtPublicKey = new System.Windows.Forms.TextBox();
		((System.ComponentModel.ISupportInitialize)this.uiGroupBox1).BeginInit();
		this.uiGroupBox1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.TxtSecret.SuspendLayout();
		base.SuspendLayout();
		this.uiGroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.uiGroupBox1.BackColor = System.Drawing.Color.SteelBlue;
		this.uiGroupBox1.Controls.Add(this.label1);
		this.uiGroupBox1.Location = new System.Drawing.Point(12, 0);
		this.uiGroupBox1.Name = "uiGroupBox1";
		this.uiGroupBox1.Size = new System.Drawing.Size(1346, 35);
		this.uiGroupBox1.TabIndex = 14;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Snow;
		this.label1.Location = new System.Drawing.Point(711, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(204, 24);
		this.label1.TabIndex = 1;
		this.label1.Text = "Zatca CSID Generator";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(1150, 12);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(179, 24);
		this.label2.TabIndex = 15;
		this.label2.Text = "الرمز المتغير (6أرقام)";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.TxtOtp);
		this.groupBox1.Controls.Add(this.CBUserName);
		this.groupBox1.Controls.Add(this.CKUsers);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Location = new System.Drawing.Point(12, 41);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1346, 92);
		this.groupBox1.TabIndex = 17;
		this.groupBox1.TabStop = false;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(583, 15);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(120, 24);
		this.label3.TabIndex = 212;
		this.label3.Text = "تحديد موظف ";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(446, 15);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(98, 24);
		this.label4.TabIndex = 62;
		this.label4.Text = "المستخدم";
		this.TxtOtp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtOtp.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtOtp.Location = new System.Drawing.Point(946, 42);
		this.TxtOtp.MaxLength = 6;
		this.TxtOtp.Name = "TxtOtp";
		this.TxtOtp.Size = new System.Drawing.Size(379, 32);
		this.TxtOtp.TabIndex = 61;
		this.CBUserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.CBUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.CBUserName.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CBUserName.FormattingEnabled = true;
		this.CBUserName.Location = new System.Drawing.Point(7, 45);
		this.CBUserName.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBUserName.Name = "CBUserName";
		this.CBUserName.Size = new System.Drawing.Size(551, 29);
		this.CBUserName.TabIndex = 60;
		this.CBUserName.SelectedIndexChanged += new System.EventHandler(CBUserName_SelectedIndexChanged);
		this.CKUsers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CKUsers.AutoSize = true;
		this.CKUsers.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CKUsers.ForeColor = System.Drawing.Color.SteelBlue;
		this.CKUsers.Location = new System.Drawing.Point(645, 50);
		this.CKUsers.Name = "CKUsers";
		this.CKUsers.Size = new System.Drawing.Size(18, 17);
		this.CKUsers.TabIndex = 17;
		this.CKUsers.UseVisualStyleBackColor = true;
		this.CKUsers.CheckedChanged += new System.EventHandler(CKUsers_CheckedChanged);
		this.CheckSimulation.AutoSize = true;
		this.CheckSimulation.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CheckSimulation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.CheckSimulation.Location = new System.Drawing.Point(283, 43);
		this.CheckSimulation.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CheckSimulation.Name = "CheckSimulation";
		this.CheckSimulation.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.CheckSimulation.Size = new System.Drawing.Size(17, 16);
		this.CheckSimulation.TabIndex = 211;
		this.CheckSimulation.UseVisualStyleBackColor = true;
		this.CheckProduction.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CheckProduction.AutoSize = true;
		this.CheckProduction.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CheckProduction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.CheckProduction.Location = new System.Drawing.Point(1101, 43);
		this.CheckProduction.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CheckProduction.Name = "CheckProduction";
		this.CheckProduction.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.CheckProduction.Size = new System.Drawing.Size(17, 16);
		this.CheckProduction.TabIndex = 210;
		this.CheckProduction.UseVisualStyleBackColor = true;
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(1165, 19);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(163, 24);
		this.label5.TabIndex = 212;
		this.label5.Text = "Production(فعليه)";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(375, 19);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(175, 24);
		this.label6.TabIndex = 213;
		this.label6.Text = "Simulation(محاكاه)";
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.BtnCreate);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.label15);
		this.groupBox2.Controls.Add(this.TxtTypeApp);
		this.groupBox2.Controls.Add(this.TxtAddress);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.CBInvoicType);
		this.groupBox2.Controls.Add(this.TxtCountryCode);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.TxtBranch);
		this.groupBox2.Controls.Add(this.TxtVatNum);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.TxtNum);
		this.groupBox2.Controls.Add(this.CheckProduction);
		this.groupBox2.Controls.Add(this.TxtHull);
		this.groupBox2.Controls.Add(this.CheckSimulation);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Location = new System.Drawing.Point(12, 139);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1346, 369);
		this.groupBox2.TabIndex = 214;
		this.groupBox2.TabStop = false;
		this.BtnCreate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnCreate.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnCreate.Appearance.Options.UseFont = true;
		this.BtnCreate.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCreate.ImageOptions.Image");
		this.BtnCreate.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCreate.Location = new System.Drawing.Point(685, 315);
		this.BtnCreate.Margin = new System.Windows.Forms.Padding(5);
		this.BtnCreate.Name = "BtnCreate";
		this.BtnCreate.Size = new System.Drawing.Size(406, 39);
		this.BtnCreate.TabIndex = 233;
		this.BtnCreate.Text = "Generate CSID انشاء المفتاح العام";
		this.BtnCreate.Click += new System.EventHandler(BtnCreate_Click);
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(1101, 316);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(5);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(224, 39);
		this.BtnNew.TabIndex = 232;
		this.BtnNew.Text = "جديد";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.Location = new System.Drawing.Point(440, 248);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(117, 24);
		this.label15.TabIndex = 231;
		this.label15.Text = "اسم الصناعه";
		this.TxtTypeApp.BackColor = System.Drawing.Color.White;
		this.TxtTypeApp.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtTypeApp.Location = new System.Drawing.Point(7, 275);
		this.TxtTypeApp.MaxLength = 9999;
		this.TxtTypeApp.Name = "TxtTypeApp";
		this.TxtTypeApp.ReadOnly = true;
		this.TxtTypeApp.Size = new System.Drawing.Size(547, 32);
		this.TxtTypeApp.TabIndex = 230;
		this.TxtTypeApp.Text = "DiskTop";
		this.TxtAddress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtAddress.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtAddress.Location = new System.Drawing.Point(685, 276);
		this.TxtAddress.MaxLength = 9999;
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Size = new System.Drawing.Size(640, 32);
		this.TxtAddress.TabIndex = 229;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(1202, 251);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(129, 24);
		this.label14.TabIndex = 228;
		this.label14.Text = "الفرع او المكان";
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(453, 191);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(101, 24);
		this.label13.TabIndex = 227;
		this.label13.Text = "نوع الفاتوره";
		this.CBInvoicType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.CBInvoicType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.CBInvoicType.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CBInvoicType.FormattingEnabled = true;
		this.CBInvoicType.Location = new System.Drawing.Point(7, 217);
		this.CBInvoicType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBInvoicType.Name = "CBInvoicType";
		this.CBInvoicType.Size = new System.Drawing.Size(547, 29);
		this.CBInvoicType.TabIndex = 226;
		this.TxtCountryCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCountryCode.BackColor = System.Drawing.Color.White;
		this.TxtCountryCode.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtCountryCode.Location = new System.Drawing.Point(685, 217);
		this.TxtCountryCode.MaxLength = 9999;
		this.TxtCountryCode.Name = "TxtCountryCode";
		this.TxtCountryCode.ReadOnly = true;
		this.TxtCountryCode.Size = new System.Drawing.Size(640, 32);
		this.TxtCountryCode.TabIndex = 225;
		this.TxtCountryCode.Text = "SA";
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(1240, 191);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(88, 24);
		this.label12.TabIndex = 224;
		this.label12.Text = "رمز الدوله";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(462, 132);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(96, 24);
		this.label11.TabIndex = 223;
		this.label11.Text = "اسم الفرع";
		this.TxtBranch.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtBranch.Location = new System.Drawing.Point(7, 157);
		this.TxtBranch.MaxLength = 9999;
		this.TxtBranch.Name = "TxtBranch";
		this.TxtBranch.Size = new System.Drawing.Size(547, 32);
		this.TxtBranch.TabIndex = 222;
		this.TxtVatNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtVatNum.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtVatNum.Location = new System.Drawing.Point(685, 157);
		this.TxtVatNum.MaxLength = 9999;
		this.TxtVatNum.Name = "TxtVatNum";
		this.TxtVatNum.Size = new System.Drawing.Size(640, 32);
		this.TxtVatNum.TabIndex = 221;
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(1203, 132);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(124, 24);
		this.label10.TabIndex = 220;
		this.label10.Text = "الرقم الضريبي";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(421, 68);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(144, 24);
		this.label9.TabIndex = 219;
		this.label9.Text = "الرقم التسلسي";
		this.TxtNum.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtNum.Location = new System.Drawing.Point(7, 98);
		this.TxtNum.MaxLength = 9999;
		this.TxtNum.Name = "TxtNum";
		this.TxtNum.Size = new System.Drawing.Size(551, 32);
		this.TxtNum.TabIndex = 218;
		this.TxtHull.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtHull.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtHull.Location = new System.Drawing.Point(685, 98);
		this.TxtHull.MaxLength = 9999;
		this.TxtHull.Name = "TxtHull";
		this.TxtHull.Size = new System.Drawing.Size(640, 32);
		this.TxtHull.TabIndex = 217;
		this.TxtHull.Text = "DisktopDevice000001";
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.Olive;
		this.label8.Location = new System.Drawing.Point(894, 69);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(278, 24);
		this.label8.TabIndex = 216;
		this.label8.Text = "(يفضل ان يكتب باللغه الانجليزيه)";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(1173, 69);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(162, 24);
		this.label7.TabIndex = 215;
		this.label7.Text = "اسم الحل التقتني";
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.Location = new System.Drawing.Point(1120, 15);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(217, 24);
		this.label16.TabIndex = 229;
		this.label16.Text = "CSR طلب توقيع الشهادة ";
		this.TxtCrs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCrs.Location = new System.Drawing.Point(685, 43);
		this.TxtCrs.Multiline = true;
		this.TxtCrs.Name = "TxtCrs";
		this.TxtCrs.Size = new System.Drawing.Size(640, 83);
		this.TxtCrs.TabIndex = 230;
		this.TxtPrivateKey.Location = new System.Drawing.Point(7, 43);
		this.TxtPrivateKey.Multiline = true;
		this.TxtPrivateKey.Name = "TxtPrivateKey";
		this.TxtPrivateKey.Size = new System.Drawing.Size(546, 83);
		this.TxtPrivateKey.TabIndex = 232;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label17.Location = new System.Drawing.Point(314, 15);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(235, 24);
		this.label17.TabIndex = 231;
		this.label17.Text = "PrivateKey المفتاح الخاص ";
		this.TxtSecret.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSecret.Controls.Add(this.BtnSave);
		this.TxtSecret.Controls.Add(this.TxtSecrt);
		this.TxtSecret.Controls.Add(this.label19);
		this.TxtSecret.Controls.Add(this.label18);
		this.TxtSecret.Controls.Add(this.TxtPublicKey);
		this.TxtSecret.Controls.Add(this.label16);
		this.TxtSecret.Controls.Add(this.label17);
		this.TxtSecret.Controls.Add(this.TxtPrivateKey);
		this.TxtSecret.Controls.Add(this.TxtCrs);
		this.TxtSecret.Location = new System.Drawing.Point(12, 514);
		this.TxtSecret.Name = "TxtSecret";
		this.TxtSecret.Size = new System.Drawing.Size(1346, 338);
		this.TxtSecret.TabIndex = 233;
		this.TxtSecret.TabStop = false;
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(470, 292);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(5);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(855, 39);
		this.BtnSave.TabIndex = 234;
		this.BtnSave.Text = "حفظ الكل في قاعده البيانات";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.TxtSecrt.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSecrt.Location = new System.Drawing.Point(8, 230);
		this.TxtSecrt.Multiline = true;
		this.TxtSecrt.Name = "TxtSecrt";
		this.TxtSecrt.Size = new System.Drawing.Size(1136, 42);
		this.TxtSecrt.TabIndex = 236;
		this.TxtSecrt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(1156, 240);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(176, 24);
		this.label19.TabIndex = 235;
		this.label19.Text = "Secret الرقم السرى";
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label18.Location = new System.Drawing.Point(1129, 133);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(204, 24);
		this.label18.TabIndex = 233;
		this.label18.Text = "PublicKey المفتاح العام";
		this.TxtPublicKey.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtPublicKey.Location = new System.Drawing.Point(8, 159);
		this.TxtPublicKey.Multiline = true;
		this.TxtPublicKey.Name = "TxtPublicKey";
		this.TxtPublicKey.Size = new System.Drawing.Size(1317, 60);
		this.TxtPublicKey.TabIndex = 234;
		base.Appearance.BackColor = System.Drawing.Color.White;
		base.Appearance.Options.UseBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1364, 864);
		base.Controls.Add(this.TxtSecret);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.uiGroupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MinimizeBox = false;
		base.Name = "FrmGenetareCSID";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "GenetareCSID";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		((System.ComponentModel.ISupportInitialize)this.uiGroupBox1).EndInit();
		this.uiGroupBox1.ResumeLayout(false);
		this.uiGroupBox1.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.TxtSecret.ResumeLayout(false);
		this.TxtSecret.PerformLayout();
		base.ResumeLayout(false);
	}
}


