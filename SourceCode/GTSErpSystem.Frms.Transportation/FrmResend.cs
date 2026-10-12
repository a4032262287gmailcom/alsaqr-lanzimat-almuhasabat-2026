using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Contract;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Frms.Transportation.SearchTransportation;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Transportation;

public class FrmResend : Form
{
	private Class_Bounce BNC = new Class_Bounce();

	private GTSdbEntities entities = new GTSdbEntities();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private int DecimalNum = LoginDetails.DecimalNum;

	private IContainer components = null;

	private GroupBox groupBox5;

	private Label TxtpurchaseID;

	private ButtonEdit TxtCode;

	private System.Windows.Forms.ComboBox TxtType;

	private Label Txtordertype;

	private DateEdit TxtDate;

	private Label LblCustLast;

	private GroupBox groupBox1;

	private Label KJJKHJ;

	private ButtonEdit TxtCustName;

	private Label label43;

	private TextEdit TxtMobile;

	private Label label46;

	private TextEdit TxtNumVat;

	private Label label35;

	private TextEdit TxtCommercialRecord;

	private Label label3;

	private TextEdit TxtAddress;

	private GroupBox groupBox2;

	private Label label2;

	private Label label4;

	private TextEdit Txtbodynumber;

	private Label label5;

	private Label label7;

	private TextEdit txtcarNumber;

	private TextEdit TxtModel;

	private Label label6;

	private GroupBox groupBox4;

	private Label label14;

	private Label label15;

	private DateEdit TxtDateResend;

	private Label label16;

	private DateEdit TxtDateReceipt;

	private Label label18;

	private DateEdit TxtDateOrd;

	private Label label19;

	private TextEdit Txtdestination;

	private TextEdit TxtUploader;

	private GroupBox groupBox6;

	private TextEdit Txtmobiledriver;

	private Label label23;

	private TextEdit txtcarNumberdriver;

	private Label label24;

	private Label label25;

	private GroupBox groupBox7;

	private Label label29;

	private Label label28;

	private TextEdit TxtVat;

	private TextEdit TxtLastNet;

	private TextEdit Txttotal;

	private Label label30;

	private Label label27;

	private TextEdit Txtcash;

	private TextEdit Txtdiscount;

	private Label label21;

	private TextEdit Txtsafy;

	private TextEdit Txtshippingvalue;

	private Label label22;

	private Label label26;

	private System.Windows.Forms.ComboBox Txtdrivername;

	private GroupBox groupBox3;

	private SimpleButton BtnPrint;

	private SimpleButton BtnResend;

	private SimpleButton BtnExit;

	private ButtonEdit BtnOrderSearch;

	private Label LBlSuppAccount;

	private Label LblSup;

	private Label label37;

	private TextEdit TxtCharge;

	private Label label1;

	private ButtonEdit BtnSearchReceive;

	private ButtonEdit BtnSearchReceiveId;

	private SimpleButton BtnRec;

	private SimpleButton BtnPay;

	private TextEdit CmpColor;

	private TextEdit CmpCarType;

	private TextBox TxtNote;

	private Label label8;

	public FrmResend()
	{
		InitializeComponent();
		BindFrm();
		BtnResend.Enabled = false;
		BtnPrint.Enabled = false;
	}

	private void toggleHold_Properties_Toggled(object sender, EventArgs e)
	{
	}

	private void label17_Click(object sender, EventArgs e)
	{
	}

	private void label1_Click(object sender, EventArgs e)
	{
	}

	private void label43_Click(object sender, EventArgs e)
	{
	}

	private void label46_Click(object sender, EventArgs e)
	{
	}

	private void label35_Click(object sender, EventArgs e)
	{
	}

	private void TxtCommercialRecord_EditValueChanged(object sender, EventArgs e)
	{
	}

	private void label3_Click(object sender, EventArgs e)
	{
	}

	private void TxtNumVat_EditValueChanged(object sender, EventArgs e)
	{
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	private void buttonEdit2_EditValueChanged(object sender, EventArgs e)
	{
	}

	private void label15_Click(object sender, EventArgs e)
	{
	}

	private void label24_Click(object sender, EventArgs e)
	{
	}

	private void groupBox5_Enter(object sender, EventArgs e)
	{
	}

	private void label7_Click(object sender, EventArgs e)
	{
	}

	private void TxtCustName_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		using (new GTSdbEntities())
		{
			try
			{
				FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
				frmSearchCustomer.ShowDialog();
				if (frmSearchCustomer.CustID > 0)
				{
					SearchCustNum(frmSearchCustomer.CustID);
				}
			}
			catch (Exception)
			{
			}
		}
	}

	private void SearchCustNum(int ID, int codeCust = 0)
	{
		try
		{
			Class_Customer class_Customer = new Class_Customer();
			Account_CustSup account_CustSup = null;
			if (codeCust > 0)
			{
				account_CustSup = (from x in class_Customer.GetAll()
					where x.ID == ID && x.IsCustomers == true
					select x).FirstOrDefault();
				if (account_CustSup == null)
				{
					return;
				}
				ID = account_CustSup.ID;
			}
			if (ID > 0)
			{
				account_CustSup = (from x in class_Customer.GetAll()
					where x.ID == ID && x.IsCustomers == true
					select x).FirstOrDefault();
				if (account_CustSup != null)
				{
					LblSup.Text = ID.ToString();
					TxtNumVat.Text = account_CustSup.VatNum.ToString();
					TxtCustName.Text = account_CustSup.CustSuppName;
					TxtMobile.Text = account_CustSup.Phone;
					TxtCommercialRecord.Text = account_CustSup.CommercialRecord;
					TxtAddress.Text = account_CustSup.Address;
					Charge(ID);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void Charge(int ID)
	{
		try
		{
			GTSdbEntities gTSdbEntities = new GTSdbEntities();
			List<decimal?> list = gTSdbEntities.GetAccountBalance(ID).ToList();
			foreach (decimal? item in list)
			{
				TxtCharge.Text = FUNC_DecimalNum(item.Value).ToString();
			}
		}
		catch (Exception)
		{
		}
	}

	private decimal FUNC_DecimalNum(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNum);
	}

	private void BtnNewCustomer_Click(object sender, EventArgs e)
	{
		new FrmCustomer().Show();
	}

	private void BtnCustomerSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
	}

	private void BtnCustomerSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
		}
	}

	private void TxtNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void BindFrm()
	{
		TxtDate.DateTime = DateTime.Now;
		TxtDateOrd.DateTime = DateTime.Now;
		TxtDateReceipt.DateTime = DateTime.Now.AddDays(2.0);
		TxtDateResend.DateTime = DateTime.Now.AddDays(7.0);
		Txtdrivername.DataSource = BNC.GetAll();
		Txtdrivername.DisplayMember = "Name";
		Txtdrivername.ValueMember = "ID";
		Class_Orders class_Orders = new Class_Orders();
		TxtType.DataSource = class_Orders.GetAccountType();
		TxtType.DisplayMember = "Name";
		TxtType.ValueMember = "ID";
	}

	private void TxtType_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void Txtdrivername_SelectedValueChanged(object sender, EventArgs e)
	{
		try
		{
			int DrID = Convert.ToInt32(Txtdrivername.SelectedValue);
			Txtmobiledriver.Text = (from x in BNC.GetAll()
				where x.ID == DrID
				select x).FirstOrDefault().Phone;
		}
		catch (Exception)
		{
		}
	}

	private void simpleButton1_Click(object sender, EventArgs e)
	{
		new FrmDrivers().ShowDialog();
		Txtdrivername.DataSource = null;
		Txtdrivername.DataSource = BNC.GetAll().ToList();
		Txtdrivername.DisplayMember = "Name";
		Txtdrivername.ValueMember = "ID";
		Txtdrivername.SelectedValue = 0;
	}

	private void BtnSearchReceive_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		using (new GTSdbEntities())
		{
			try
			{
				FrmSearchReceive frmSearchReceive = new FrmSearchReceive();
				frmSearchReceive.ShowDialog();
				if (frmSearchReceive.ID > 0)
				{
					SearchReceive(frmSearchReceive.ID);
				}
			}
			catch (Exception)
			{
			}
		}
	}

	private void SearchReceive(int id)
	{
		Car_Receive car_Receive = entities.Car_Receive.FirstOrDefault((Car_Receive s) => s.ID == id);
		if (car_Receive != null && !car_Receive.IsDone.Value)
		{
			Class_Customer class_Customer = new Class_Customer();
			SearchCustNum(car_Receive.CustID.Value);
			SearchDriver(car_Receive.DriverID.Value);
			BtnSearchReceive.Text = id.ToString();
			BtnSearchReceiveId.Text = id.ToString();
			Txtbodynumber.Text = car_Receive.Car_bodynumber;
			CmpColor.Text = car_Receive.Car_ColorID;
			TxtModel.Text = car_Receive.Car_Model;
			txtcarNumber.Text = car_Receive.Car_PlateNumber;
			CmpCarType.Text = car_Receive.Car_TypeID;
			LblSup.Text = car_Receive.CustID.ToString();
			LBlSuppAccount.Text = class_Customer.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
			Txtdrivername.SelectedValue = car_Receive.DriverID;
			txtcarNumberdriver.Text = car_Receive.Driver_PlateNumber;
			TxtDateOrd.Text = car_Receive.Order_Date;
			TxtDateReceipt.Text = car_Receive.Order_DateReceipt;
			TxtDateResend.Text = car_Receive.Order_DateResend;
			Txtdestination.Text = car_Receive.Order_Destination;
			TxtType.SelectedValue = car_Receive.Order_TypeID;
			TxtUploader.Text = car_Receive.Order_Uploader;
			Txtcash.Text = car_Receive.CashAmount.ToString();
			Txtdiscount.Text = car_Receive.DiscountAmount.ToString();
			TxtLastNet.Text = car_Receive.LastCashAmount.ToString();
			Txtsafy.Text = car_Receive.SafyAmount.ToString();
			Txtshippingvalue.Text = car_Receive.ShippingAmount.ToString();
			Txttotal.Text = car_Receive.TotalAmount.ToString();
			TxtNote.Text = car_Receive.Note.ToString();
			ShowBtns(IsResend: true);
		}
		else
		{
			MessageBox.Show("لا يوجد بحث");
			ClearControls();
		}
	}

	private void SearchResend(int id)
	{
		Car_Receive car_Receive = entities.Car_Receive.FirstOrDefault((Car_Receive s) => s.ID == id);
		if (car_Receive != null)
		{
			Class_Customer class_Customer = new Class_Customer();
			SearchCustNum(car_Receive.CustID.Value);
			SearchDriver(car_Receive.DriverID.Value);
			TxtCode.Text = id.ToString();
			BtnOrderSearch.Text = id.ToString();
			Txtbodynumber.Text = car_Receive.Car_bodynumber;
			CmpColor.Text = car_Receive.Car_ColorID;
			TxtModel.Text = car_Receive.Car_Model;
			txtcarNumber.Text = car_Receive.Car_PlateNumber;
			CmpCarType.Text = car_Receive.Car_TypeID;
			LblSup.Text = car_Receive.CustID.ToString();
			LBlSuppAccount.Text = class_Customer.GetAccount(Convert.ToInt32(LblSup.Text)).ToString();
			Txtdrivername.SelectedValue = car_Receive.DriverID;
			txtcarNumberdriver.Text = car_Receive.Driver_PlateNumber;
			TxtDateOrd.Text = car_Receive.Order_Date;
			TxtDateReceipt.Text = car_Receive.Order_DateReceipt;
			TxtDateResend.Text = car_Receive.Order_DateResend;
			Txtdestination.Text = car_Receive.Order_Destination;
			TxtType.SelectedValue = car_Receive.Order_TypeID;
			TxtUploader.Text = car_Receive.Order_Uploader;
			Txtcash.Text = car_Receive.CashAmount.ToString();
			Txtdiscount.Text = car_Receive.DiscountAmount.ToString();
			TxtLastNet.Text = car_Receive.LastCashAmount.ToString();
			Txtsafy.Text = car_Receive.SafyAmount.ToString();
			Txtshippingvalue.Text = car_Receive.ShippingAmount.ToString();
			Txttotal.Text = car_Receive.TotalAmount.ToString();
			TxtVat.Text = car_Receive.VatAmount.ToString();
			TxtNote.Text = car_Receive.Note.ToString();
			ShowBtns(IsResend: true);
		}
		else
		{
			MessageBox.Show("لا يوجد بحث");
			ClearControls();
		}
	}

	private void SearchDriver(int id)
	{
		Contract_Bounce contract_Bounce = BNC.GetAll().FirstOrDefault((Contract_Bounce x) => x.ID == id);
		if (contract_Bounce != null)
		{
			TxtMobile.Text = contract_Bounce.Phone;
		}
	}

	private void Resend(int id)
	{
		Car_Receive car_Receive = entities.Car_Receive.FirstOrDefault((Car_Receive s) => s.ID == id);
		if (car_Receive != null && !car_Receive.IsDone.Value)
		{
			car_Receive.IsDone = true;
			entities.SaveChanges();
			MessageBox.Show("تم الاستلام بنجاح");
			ShowBtns(IsResend: false);
		}
	}

	private void Print()
	{
		LoginDetails.NetCash = Math.Round(Convert.ToDecimal(Txttotal.Text), DecimalNum);
		LoginDetails.VatCash = Math.Round(Convert.ToDecimal(TxtVat.Text), DecimalNum);
		if (!string.IsNullOrWhiteSpace(TxtCode.Text))
		{
			SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["ChangeOil"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Print_Car", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@Id", Convert.ToInt32(TxtCode.Text));
			sqlCommand.Parameters.AddWithValue("@IsDone", true);
			sqlDataAdapter.Fill(dataSetALL.ChangeOil);
			sqlConnection.Close();
			if (dataSetALL.Tables["ChangeOil"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintRecend.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintRecend.mrt");
				}
				stiReport.RegData(dataSetALL.ChangeOil);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				stiReport["Type"] = TxtType.Text;
				stiReport["Date"] = TxtDate.DateTime;
				stiReport["PurchID"] = Convert.ToInt32(TxtCode.Text);
				Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
				string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, TxtDate.DateTime.ToString(), (LoginDetails.NetCash + LoginDetails.VatCash).ToString(), LoginDetails.VatCash.ToString());
				stiReport["EncHash"] = value;
				stiReport.Render();
				stiReport.Show(dialogForm: true);
			}
			else
			{
				MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void ShowBtns(bool IsResend)
	{
		if (IsResend)
		{
			BtnResend.Enabled = true;
			BtnPrint.Enabled = true;
		}
		else
		{
			BtnResend.Enabled = true;
			BtnPrint.Enabled = true;
		}
	}

	private void ClearControls()
	{
		LblSup.Text = "0";
		LBlSuppAccount.Text = "0";
		TxtCode.Text = "";
		BtnOrderSearch.Text = "";
		TxtType.SelectedIndex = 1;
		TxtCustName.Text = "";
		TxtCharge.Text = "00.00";
		TxtMobile.Text = "";
		TxtNumVat.Text = "";
		TxtCommercialRecord.Text = "";
		TxtAddress.Text = "";
		CmpCarType.Text = "";
		Txtbodynumber.Text = "";
		txtcarNumber.Text = "";
		CmpColor.Text = "";
		TxtModel.Text = "";
		Txtdrivername.SelectedIndex = 0;
		txtcarNumberdriver.Text = "";
		Txtmobiledriver.Text = "";
		TxtDateOrd.DateTime = DateTime.Now;
		TxtDateReceipt.DateTime = DateTime.Now;
		TxtDateResend.DateTime = DateTime.Now;
		TxtUploader.Text = "";
		Txtdestination.Text = "";
		TxtNote.Text = "";
		Txtshippingvalue.Text = "0.00";
		Txtdiscount.Text = "0.00";
		Txttotal.Text = "0.00";
		Txtsafy.Text = "0.00";
		Txtcash.Text = "0.00";
		TxtLastNet.Text = "0.00";
	}

	private void BtnSearchReceiveId_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(BtnSearchReceiveId.Text))
		{
			SearchReceive(Convert.ToInt32(BtnSearchReceiveId.Text));
		}
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		using (new GTSdbEntities())
		{
			try
			{
				GTSErpSystem.Frms.Transportation.SearchTransportation.FrmResend frmResend = new GTSErpSystem.Frms.Transportation.SearchTransportation.FrmResend();
				frmResend.ShowDialog();
				if (frmResend.ID > 0)
				{
					SearchResend(frmResend.ID);
				}
			}
			catch (Exception)
			{
			}
		}
	}

	private void BtnOrderSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(BtnOrderSearch.Text))
		{
			SearchResend(Convert.ToInt32(BtnOrderSearch.Text));
		}
	}

	private void BtnOrderSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && !string.IsNullOrWhiteSpace(BtnOrderSearch.Text))
		{
			SearchResend(Convert.ToInt32(BtnOrderSearch.Text));
		}
	}

	private void BtnRec_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 31))
		{
			new FrmReceipts().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnPay_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 34))
		{
			new FrmPaymentBig().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnResend_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(TxtCode.Text))
		{
			int id = Convert.ToInt32(TxtCode.Text);
			Resend(id);
		}
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Transportation.FrmResend));
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.BtnSearchReceive = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnSearchReceiveId = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtpurchaseID = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtType = new System.Windows.Forms.ComboBox();
		this.Txtordertype = new System.Windows.Forms.Label();
		this.BtnOrderSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.LblCustLast = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label37 = new System.Windows.Forms.Label();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.LblSup = new System.Windows.Forms.Label();
		this.LBlSuppAccount = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtAddress = new DevExpress.XtraEditors.TextEdit();
		this.label35 = new System.Windows.Forms.Label();
		this.TxtCommercialRecord = new DevExpress.XtraEditors.TextEdit();
		this.TxtNumVat = new DevExpress.XtraEditors.TextEdit();
		this.label46 = new System.Windows.Forms.Label();
		this.TxtMobile = new DevExpress.XtraEditors.TextEdit();
		this.label43 = new System.Windows.Forms.Label();
		this.TxtCustName = new DevExpress.XtraEditors.ButtonEdit();
		this.KJJKHJ = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.CmpColor = new DevExpress.XtraEditors.TextEdit();
		this.CmpCarType = new DevExpress.XtraEditors.TextEdit();
		this.label7 = new System.Windows.Forms.Label();
		this.txtcarNumber = new DevExpress.XtraEditors.TextEdit();
		this.TxtModel = new DevExpress.XtraEditors.TextEdit();
		this.label6 = new System.Windows.Forms.Label();
		this.Txtbodynumber = new DevExpress.XtraEditors.TextEdit();
		this.label5 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.Txtdestination = new DevExpress.XtraEditors.TextEdit();
		this.TxtUploader = new DevExpress.XtraEditors.TextEdit();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtDateResend = new DevExpress.XtraEditors.DateEdit();
		this.label16 = new System.Windows.Forms.Label();
		this.TxtDateReceipt = new DevExpress.XtraEditors.DateEdit();
		this.label18 = new System.Windows.Forms.Label();
		this.TxtDateOrd = new DevExpress.XtraEditors.DateEdit();
		this.label19 = new System.Windows.Forms.Label();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.Txtdrivername = new System.Windows.Forms.ComboBox();
		this.Txtmobiledriver = new DevExpress.XtraEditors.TextEdit();
		this.label23 = new System.Windows.Forms.Label();
		this.txtcarNumberdriver = new DevExpress.XtraEditors.TextEdit();
		this.label24 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		this.label29 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.TxtVat = new DevExpress.XtraEditors.TextEdit();
		this.TxtLastNet = new DevExpress.XtraEditors.TextEdit();
		this.Txttotal = new DevExpress.XtraEditors.TextEdit();
		this.label30 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.Txtcash = new DevExpress.XtraEditors.TextEdit();
		this.Txtdiscount = new DevExpress.XtraEditors.TextEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.Txtsafy = new DevExpress.XtraEditors.TextEdit();
		this.Txtshippingvalue = new DevExpress.XtraEditors.TextEdit();
		this.label22 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnResend = new DevExpress.XtraEditors.SimpleButton();
		this.BtnRec = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPay = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnSearchReceive.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnSearchReceiveId.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMobile.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCustName.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.CmpColor.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CmpCarType.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtcarNumber.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtModel.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txtbodynumber.Properties).BeginInit();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Txtdestination.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtUploader.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateResend.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateResend.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateOrd.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateOrd.Properties).BeginInit();
		this.groupBox6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Txtmobiledriver.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtcarNumberdriver.Properties).BeginInit();
		this.groupBox7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtVat.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLastNet.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txttotal.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txtcash.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txtdiscount.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txtsafy.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Txtshippingvalue.Properties).BeginInit();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupBox5.Controls.Add(this.TxtNote);
		this.groupBox5.Controls.Add(this.label8);
		this.groupBox5.Controls.Add(this.label1);
		this.groupBox5.Controls.Add(this.BtnSearchReceive);
		this.groupBox5.Controls.Add(this.BtnSearchReceiveId);
		this.groupBox5.Controls.Add(this.TxtpurchaseID);
		this.groupBox5.Controls.Add(this.TxtCode);
		this.groupBox5.Controls.Add(this.TxtType);
		this.groupBox5.Controls.Add(this.Txtordertype);
		this.groupBox5.Controls.Add(this.BtnOrderSearch);
		this.groupBox5.Location = new System.Drawing.Point(10, 9);
		this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.groupBox5.Size = new System.Drawing.Size(1055, 121);
		this.groupBox5.TabIndex = 4;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "بيانات فاتورة التسليم";
		this.groupBox5.Enter += new System.EventHandler(groupBox5_Enter);
		this.TxtNote.Location = new System.Drawing.Point(10, 53);
		this.TxtNote.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtNote.Multiline = true;
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtNote.Size = new System.Drawing.Size(946, 64);
		this.TxtNote.TabIndex = 190;
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(960, 54);
		this.label8.Name = "label8";
		this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label8.Size = new System.Drawing.Size(59, 17);
		this.label8.TabIndex = 189;
		this.label8.Text = "ملاحظات";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label1.Location = new System.Drawing.Point(612, 28);
		this.label1.Name = "label1";
		this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label1.Size = new System.Drawing.Size(82, 17);
		this.label1.TabIndex = 187;
		this.label1.Text = "رقم الاستلام";
		this.label1.Visible = false;
		this.BtnSearchReceive.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSearchReceive.EditValue = "";
		this.BtnSearchReceive.Location = new System.Drawing.Point(431, 27);
		this.BtnSearchReceive.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.BtnSearchReceive.Name = "BtnSearchReceive";
		this.BtnSearchReceive.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnSearchReceive.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSearchReceive.Properties.Appearance.Options.UseBackColor = true;
		this.BtnSearchReceive.Properties.Appearance.Options.UseFont = true;
		this.BtnSearchReceive.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnSearchReceive.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnSearchReceive.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.BtnSearchReceive.Properties.ReadOnly = true;
		this.BtnSearchReceive.Size = new System.Drawing.Size(175, 22);
		this.BtnSearchReceive.TabIndex = 186;
		this.BtnSearchReceive.Visible = false;
		this.BtnSearchReceive.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnSearchReceive_ButtonClick);
		this.BtnSearchReceiveId.EditValue = "";
		this.BtnSearchReceiveId.Location = new System.Drawing.Point(356, 27);
		this.BtnSearchReceiveId.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.BtnSearchReceiveId.Name = "BtnSearchReceiveId";
		this.BtnSearchReceiveId.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnSearchReceiveId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSearchReceiveId.Properties.Appearance.Options.UseBackColor = true;
		this.BtnSearchReceiveId.Properties.Appearance.Options.UseFont = true;
		this.BtnSearchReceiveId.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnSearchReceiveId.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnSearchReceiveId.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions1.Image");
		this.BtnSearchReceiveId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.BtnSearchReceiveId.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnSearchReceiveId.Properties.NullText = "بحث";
		this.BtnSearchReceiveId.Size = new System.Drawing.Size(68, 22);
		this.BtnSearchReceiveId.TabIndex = 188;
		this.BtnSearchReceiveId.Tag = "Search";
		this.BtnSearchReceiveId.Visible = false;
		this.BtnSearchReceiveId.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnSearchReceiveId_ButtonClick);
		this.TxtpurchaseID.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtpurchaseID.AutoSize = true;
		this.TxtpurchaseID.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtpurchaseID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.TxtpurchaseID.Location = new System.Drawing.Point(961, 28);
		this.TxtpurchaseID.Name = "TxtpurchaseID";
		this.TxtpurchaseID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.TxtpurchaseID.Size = new System.Drawing.Size(80, 17);
		this.TxtpurchaseID.TabIndex = 105;
		this.TxtpurchaseID.Text = "رقم التسليم";
		this.TxtpurchaseID.Click += new System.EventHandler(label17_Click);
		this.TxtCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCode.EditValue = "";
		this.TxtCode.Location = new System.Drawing.Point(780, 27);
		this.TxtCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCode.Properties.Appearance.Options.UseFont = true;
		this.TxtCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCode.Properties.ReadOnly = true;
		this.TxtCode.Size = new System.Drawing.Size(175, 22);
		this.TxtCode.TabIndex = 50;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.TxtType.Enabled = false;
		this.TxtType.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtType.FormattingEnabled = true;
		this.TxtType.Location = new System.Drawing.Point(9, 25);
		this.TxtType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtType.Name = "TxtType";
		this.TxtType.Size = new System.Drawing.Size(250, 24);
		this.TxtType.TabIndex = 1;
		this.TxtType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress);
		this.Txtordertype.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Txtordertype.AutoSize = true;
		this.Txtordertype.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txtordertype.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Txtordertype.Location = new System.Drawing.Point(264, 27);
		this.Txtordertype.Name = "Txtordertype";
		this.Txtordertype.Size = new System.Drawing.Size(70, 17);
		this.Txtordertype.TabIndex = 112;
		this.Txtordertype.Text = "نوع الفاتورة";
		this.BtnOrderSearch.EditValue = "";
		this.BtnOrderSearch.Location = new System.Drawing.Point(706, 27);
		this.BtnOrderSearch.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.BtnOrderSearch.Name = "BtnOrderSearch";
		this.BtnOrderSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnOrderSearch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnOrderSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnOrderSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnOrderSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnOrderSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnOrderSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions2.Image");
		this.BtnOrderSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.BtnOrderSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnOrderSearch.Properties.NullText = "بحث";
		this.BtnOrderSearch.Size = new System.Drawing.Size(68, 22);
		this.BtnOrderSearch.TabIndex = 185;
		this.BtnOrderSearch.Tag = "Search";
		this.BtnOrderSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnOrderSearch_ButtonClick);
		this.BtnOrderSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnOrderSearch_KeyDown);
		this.TxtDate.EditValue = null;
		this.TxtDate.Location = new System.Drawing.Point(824, 1231);
		this.TxtDate.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDate.Properties.Appearance.Options.UseBackColor = true;
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
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDate.Size = new System.Drawing.Size(296, 22);
		this.TxtDate.TabIndex = 0;
		this.LblCustLast.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LblCustLast.AutoSize = true;
		this.LblCustLast.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblCustLast.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblCustLast.Location = new System.Drawing.Point(717, 609);
		this.LblCustLast.Name = "LblCustLast";
		this.LblCustLast.Size = new System.Drawing.Size(0, 17);
		this.LblCustLast.TabIndex = 113;
		this.groupBox1.Controls.Add(this.label37);
		this.groupBox1.Controls.Add(this.TxtCharge);
		this.groupBox1.Controls.Add(this.LblSup);
		this.groupBox1.Controls.Add(this.LBlSuppAccount);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.TxtAddress);
		this.groupBox1.Controls.Add(this.label35);
		this.groupBox1.Controls.Add(this.TxtCommercialRecord);
		this.groupBox1.Controls.Add(this.TxtNumVat);
		this.groupBox1.Controls.Add(this.label46);
		this.groupBox1.Controls.Add(this.TxtMobile);
		this.groupBox1.Controls.Add(this.label43);
		this.groupBox1.Controls.Add(this.TxtCustName);
		this.groupBox1.Controls.Add(this.KJJKHJ);
		this.groupBox1.Location = new System.Drawing.Point(10, 134);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.groupBox1.Size = new System.Drawing.Size(1055, 106);
		this.groupBox1.TabIndex = 120;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "بيانات العميل";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.label37.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label37.Location = new System.Drawing.Point(265, 23);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(79, 17);
		this.label37.TabIndex = 127;
		this.label37.Text = "رصيد العميل";
		this.TxtCharge.EditValue = "0.00";
		this.TxtCharge.Enabled = false;
		this.TxtCharge.Location = new System.Drawing.Point(9, 20);
		this.TxtCharge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtCharge.Name = "TxtCharge";
		this.TxtCharge.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCharge.Size = new System.Drawing.Size(250, 22);
		this.TxtCharge.TabIndex = 126;
		this.LblSup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.LblSup.AutoSize = true;
		this.LblSup.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblSup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblSup.Location = new System.Drawing.Point(1013, 77);
		this.LblSup.Name = "LblSup";
		this.LblSup.Size = new System.Drawing.Size(16, 17);
		this.LblSup.TabIndex = 196;
		this.LblSup.Text = "0";
		this.LblSup.Visible = false;
		this.LBlSuppAccount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LBlSuppAccount.AutoSize = true;
		this.LBlSuppAccount.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LBlSuppAccount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LBlSuppAccount.Location = new System.Drawing.Point(1035, 75);
		this.LBlSuppAccount.Name = "LBlSuppAccount";
		this.LBlSuppAccount.Size = new System.Drawing.Size(16, 17);
		this.LBlSuppAccount.TabIndex = 195;
		this.LBlSuppAccount.Text = "1";
		this.LBlSuppAccount.Visible = false;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(962, 75);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(45, 17);
		this.label3.TabIndex = 194;
		this.label3.Text = "العنوان";
		this.label3.Click += new System.EventHandler(label3_Click);
		this.TxtAddress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtAddress.Enabled = false;
		this.TxtAddress.Location = new System.Drawing.Point(9, 72);
		this.TxtAddress.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtAddress.Properties.Appearance.Options.UseFont = true;
		this.TxtAddress.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAddress.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAddress.Properties.ReadOnly = true;
		this.TxtAddress.Size = new System.Drawing.Size(946, 22);
		this.TxtAddress.TabIndex = 192;
		this.label35.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label35.AutoSize = true;
		this.label35.Font = new System.Drawing.Font("Tahoma", 9f);
		this.label35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label35.Location = new System.Drawing.Point(266, 50);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(83, 14);
		this.label35.TabIndex = 191;
		this.label35.Text = "السجل التجارى";
		this.label35.Click += new System.EventHandler(label35_Click);
		this.TxtCommercialRecord.Enabled = false;
		this.TxtCommercialRecord.Location = new System.Drawing.Point(9, 46);
		this.TxtCommercialRecord.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCommercialRecord.Name = "TxtCommercialRecord";
		this.TxtCommercialRecord.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCommercialRecord.Properties.Appearance.Options.UseFont = true;
		this.TxtCommercialRecord.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCommercialRecord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCommercialRecord.Properties.ReadOnly = true;
		this.TxtCommercialRecord.Size = new System.Drawing.Size(250, 22);
		this.TxtCommercialRecord.TabIndex = 190;
		this.TxtCommercialRecord.EditValueChanged += new System.EventHandler(TxtCommercialRecord_EditValueChanged);
		this.TxtNumVat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNumVat.Enabled = false;
		this.TxtNumVat.Location = new System.Drawing.Point(356, 47);
		this.TxtNumVat.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtNumVat.Name = "TxtNumVat";
		this.TxtNumVat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNumVat.Properties.Appearance.Options.UseFont = true;
		this.TxtNumVat.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNumVat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNumVat.Properties.ReadOnly = true;
		this.TxtNumVat.Size = new System.Drawing.Size(242, 22);
		this.TxtNumVat.TabIndex = 189;
		this.TxtNumVat.EditValueChanged += new System.EventHandler(TxtNumVat_EditValueChanged);
		this.label46.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label46.AutoSize = true;
		this.label46.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label46.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label46.Location = new System.Drawing.Point(605, 49);
		this.label46.Name = "label46";
		this.label46.Size = new System.Drawing.Size(88, 17);
		this.label46.TabIndex = 188;
		this.label46.Text = "الرقم الضريبي";
		this.label46.Click += new System.EventHandler(label46_Click);
		this.TxtMobile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtMobile.Enabled = false;
		this.TxtMobile.Location = new System.Drawing.Point(705, 46);
		this.TxtMobile.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtMobile.Name = "TxtMobile";
		this.TxtMobile.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtMobile.Properties.Appearance.Options.UseFont = true;
		this.TxtMobile.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtMobile.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtMobile.Properties.ReadOnly = true;
		this.TxtMobile.Size = new System.Drawing.Size(250, 22);
		this.TxtMobile.TabIndex = 187;
		this.label43.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label43.AutoSize = true;
		this.label43.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label43.Location = new System.Drawing.Point(962, 49);
		this.label43.Name = "label43";
		this.label43.Size = new System.Drawing.Size(68, 17);
		this.label43.TabIndex = 186;
		this.label43.Text = "رقم الجوال";
		this.label43.Click += new System.EventHandler(label43_Click);
		this.TxtCustName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCustName.Enabled = false;
		this.TxtCustName.Location = new System.Drawing.Point(356, 20);
		this.TxtCustName.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCustName.Name = "TxtCustName";
		this.TxtCustName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCustName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCustName.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCustName.Properties.Appearance.Options.UseFont = true;
		this.TxtCustName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCustName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCustName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCustName.Size = new System.Drawing.Size(598, 22);
		this.TxtCustName.TabIndex = 184;
		this.TxtCustName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCustName_ButtonClick);
		this.KJJKHJ.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.KJJKHJ.AutoSize = true;
		this.KJJKHJ.Font = new System.Drawing.Font("Tahoma", 10f);
		this.KJJKHJ.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.KJJKHJ.Location = new System.Drawing.Point(962, 23);
		this.KJJKHJ.Name = "KJJKHJ";
		this.KJJKHJ.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.KJJKHJ.Size = new System.Drawing.Size(77, 17);
		this.KJJKHJ.TabIndex = 105;
		this.KJJKHJ.Text = "اسم العميل";
		this.KJJKHJ.Click += new System.EventHandler(label1_Click);
		this.groupBox2.Controls.Add(this.CmpColor);
		this.groupBox2.Controls.Add(this.CmpCarType);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.txtcarNumber);
		this.groupBox2.Controls.Add(this.TxtModel);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.Txtbodynumber);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Location = new System.Drawing.Point(9, 244);
		this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.groupBox2.Size = new System.Drawing.Size(1055, 77);
		this.groupBox2.TabIndex = 121;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "بيانات السيارة ";
		this.groupBox2.Enter += new System.EventHandler(groupBox2_Enter);
		this.CmpColor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.CmpColor.Location = new System.Drawing.Point(9, 17);
		this.CmpColor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CmpColor.Name = "CmpColor";
		this.CmpColor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.CmpColor.Properties.Appearance.Options.UseFont = true;
		this.CmpColor.Properties.Appearance.Options.UseTextOptions = true;
		this.CmpColor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.CmpColor.Size = new System.Drawing.Size(250, 22);
		this.CmpColor.TabIndex = 196;
		this.CmpCarType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.CmpCarType.Location = new System.Drawing.Point(705, 15);
		this.CmpCarType.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
		this.CmpCarType.Name = "CmpCarType";
		this.CmpCarType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.CmpCarType.Properties.Appearance.Options.UseFont = true;
		this.CmpCarType.Properties.Appearance.Options.UseTextOptions = true;
		this.CmpCarType.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.CmpCarType.Size = new System.Drawing.Size(250, 22);
		this.CmpCarType.TabIndex = 197;
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label7.Location = new System.Drawing.Point(607, 48);
		this.label7.Name = "label7";
		this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label7.Size = new System.Drawing.Size(68, 17);
		this.label7.TabIndex = 195;
		this.label7.Text = "رقم اللوحة";
		this.label7.Click += new System.EventHandler(label7_Click);
		this.txtcarNumber.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtcarNumber.Enabled = false;
		this.txtcarNumber.Location = new System.Drawing.Point(356, 46);
		this.txtcarNumber.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
		this.txtcarNumber.Name = "txtcarNumber";
		this.txtcarNumber.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.txtcarNumber.Properties.Appearance.Options.UseFont = true;
		this.txtcarNumber.Properties.Appearance.Options.UseTextOptions = true;
		this.txtcarNumber.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.txtcarNumber.Size = new System.Drawing.Size(242, 22);
		this.txtcarNumber.TabIndex = 194;
		this.TxtModel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtModel.Enabled = false;
		this.TxtModel.Location = new System.Drawing.Point(705, 46);
		this.TxtModel.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
		this.TxtModel.Name = "TxtModel";
		this.TxtModel.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtModel.Properties.Appearance.Options.UseFont = true;
		this.TxtModel.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtModel.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtModel.Size = new System.Drawing.Size(250, 22);
		this.TxtModel.TabIndex = 193;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(964, 48);
		this.label6.Name = "label6";
		this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label6.Size = new System.Drawing.Size(51, 17);
		this.label6.TabIndex = 192;
		this.label6.Text = "الموديل";
		this.Txtbodynumber.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Txtbodynumber.Enabled = false;
		this.Txtbodynumber.Location = new System.Drawing.Point(356, 17);
		this.Txtbodynumber.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Txtbodynumber.Name = "Txtbodynumber";
		this.Txtbodynumber.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txtbodynumber.Properties.Appearance.Options.UseFont = true;
		this.Txtbodynumber.Properties.Appearance.Options.UseTextOptions = true;
		this.Txtbodynumber.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txtbodynumber.Size = new System.Drawing.Size(242, 22);
		this.Txtbodynumber.TabIndex = 188;
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(605, 20);
		this.label5.Name = "label5";
		this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label5.Size = new System.Drawing.Size(70, 17);
		this.label5.TabIndex = 184;
		this.label5.Text = "رقم الهيكل";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(962, 18);
		this.label2.Name = "label2";
		this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label2.Size = new System.Drawing.Size(77, 17);
		this.label2.TabIndex = 105;
		this.label2.Text = "نوع السيارة ";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label4.Location = new System.Drawing.Point(266, 20);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(74, 17);
		this.label4.TabIndex = 112;
		this.label4.Text = "لون السياره";
		this.groupBox4.Controls.Add(this.Txtdestination);
		this.groupBox4.Controls.Add(this.TxtUploader);
		this.groupBox4.Controls.Add(this.label14);
		this.groupBox4.Controls.Add(this.label15);
		this.groupBox4.Controls.Add(this.TxtDateResend);
		this.groupBox4.Controls.Add(this.label16);
		this.groupBox4.Controls.Add(this.TxtDateReceipt);
		this.groupBox4.Controls.Add(this.label18);
		this.groupBox4.Controls.Add(this.TxtDateOrd);
		this.groupBox4.Controls.Add(this.label19);
		this.groupBox4.Location = new System.Drawing.Point(9, 384);
		this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.groupBox4.Size = new System.Drawing.Size(1055, 102);
		this.groupBox4.TabIndex = 123;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "بيانات الرحله";
		this.Txtdestination.Enabled = false;
		this.Txtdestination.Location = new System.Drawing.Point(9, 72);
		this.Txtdestination.Margin = new System.Windows.Forms.Padding(8, 2, 8, 2);
		this.Txtdestination.Name = "Txtdestination";
		this.Txtdestination.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txtdestination.Properties.Appearance.Options.UseFont = true;
		this.Txtdestination.Properties.Appearance.Options.UseTextOptions = true;
		this.Txtdestination.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txtdestination.Size = new System.Drawing.Size(946, 22);
		this.Txtdestination.TabIndex = 192;
		this.TxtUploader.Enabled = false;
		this.TxtUploader.Location = new System.Drawing.Point(9, 45);
		this.TxtUploader.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
		this.TxtUploader.Name = "TxtUploader";
		this.TxtUploader.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtUploader.Properties.Appearance.Options.UseFont = true;
		this.TxtUploader.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtUploader.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtUploader.Size = new System.Drawing.Size(946, 22);
		this.TxtUploader.TabIndex = 192;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label14.Location = new System.Drawing.Point(966, 75);
		this.label14.Name = "label14";
		this.label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label14.Size = new System.Drawing.Size(76, 17);
		this.label14.TabIndex = 191;
		this.label14.Text = "جهة الوصول";
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label15.Location = new System.Drawing.Point(964, 48);
		this.label15.Name = "label15";
		this.label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label15.Size = new System.Drawing.Size(83, 17);
		this.label15.TabIndex = 189;
		this.label15.Text = "جهة التحميل";
		this.label15.Click += new System.EventHandler(label15_Click);
		this.TxtDateResend.EditValue = null;
		this.TxtDateResend.Enabled = false;
		this.TxtDateResend.Location = new System.Drawing.Point(9, 19);
		this.TxtDateResend.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
		this.TxtDateResend.Name = "TxtDateResend";
		this.TxtDateResend.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateResend.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDateResend.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateResend.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDateResend.Properties.Appearance.Options.UseFont = true;
		this.TxtDateResend.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateResend.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateResend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateResend.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateResend.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDateResend.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDateResend.Size = new System.Drawing.Size(250, 22);
		this.TxtDateResend.TabIndex = 188;
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label16.Location = new System.Drawing.Point(268, 21);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(86, 17);
		this.label16.TabIndex = 187;
		this.label16.Text = "تاريخ التسليم";
		this.TxtDateReceipt.EditValue = null;
		this.TxtDateReceipt.Enabled = false;
		this.TxtDateReceipt.Location = new System.Drawing.Point(356, 18);
		this.TxtDateReceipt.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDateReceipt.Name = "TxtDateReceipt";
		this.TxtDateReceipt.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateReceipt.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDateReceipt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateReceipt.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDateReceipt.Properties.Appearance.Options.UseFont = true;
		this.TxtDateReceipt.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateReceipt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateReceipt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateReceipt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateReceipt.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDateReceipt.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDateReceipt.Size = new System.Drawing.Size(242, 22);
		this.TxtDateReceipt.TabIndex = 186;
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label18.Location = new System.Drawing.Point(605, 21);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(88, 17);
		this.label18.TabIndex = 185;
		this.label18.Text = "تاريخ الاستلام";
		this.TxtDateOrd.EditValue = null;
		this.TxtDateOrd.Enabled = false;
		this.TxtDateOrd.Location = new System.Drawing.Point(705, 19);
		this.TxtDateOrd.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDateOrd.Name = "TxtDateOrd";
		this.TxtDateOrd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateOrd.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDateOrd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateOrd.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDateOrd.Properties.Appearance.Options.UseFont = true;
		this.TxtDateOrd.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateOrd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateOrd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateOrd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateOrd.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDateOrd.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDateOrd.Size = new System.Drawing.Size(250, 22);
		this.TxtDateOrd.TabIndex = 184;
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label19.Location = new System.Drawing.Point(962, 21);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(79, 17);
		this.label19.TabIndex = 183;
		this.label19.Text = "تاريخ الفاتورة";
		this.groupBox6.Controls.Add(this.Txtdrivername);
		this.groupBox6.Controls.Add(this.Txtmobiledriver);
		this.groupBox6.Controls.Add(this.label23);
		this.groupBox6.Controls.Add(this.txtcarNumberdriver);
		this.groupBox6.Controls.Add(this.label24);
		this.groupBox6.Controls.Add(this.label25);
		this.groupBox6.Location = new System.Drawing.Point(9, 325);
		this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.groupBox6.Size = new System.Drawing.Size(1055, 55);
		this.groupBox6.TabIndex = 124;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "بيانات السائق";
		this.Txtdrivername.Enabled = false;
		this.Txtdrivername.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txtdrivername.FormattingEnabled = true;
		this.Txtdrivername.Location = new System.Drawing.Point(706, 19);
		this.Txtdrivername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.Txtdrivername.Name = "Txtdrivername";
		this.Txtdrivername.Size = new System.Drawing.Size(249, 24);
		this.Txtdrivername.TabIndex = 197;
		this.Txtdrivername.SelectedValueChanged += new System.EventHandler(Txtdrivername_SelectedValueChanged);
		this.Txtdrivername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtType_KeyPress);
		this.Txtmobiledriver.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Txtmobiledriver.Enabled = false;
		this.Txtmobiledriver.Location = new System.Drawing.Point(9, 19);
		this.Txtmobiledriver.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Txtmobiledriver.Name = "Txtmobiledriver";
		this.Txtmobiledriver.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txtmobiledriver.Properties.Appearance.Options.UseFont = true;
		this.Txtmobiledriver.Properties.Appearance.Options.UseTextOptions = true;
		this.Txtmobiledriver.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txtmobiledriver.Size = new System.Drawing.Size(250, 22);
		this.Txtmobiledriver.TabIndex = 189;
		this.label23.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label23.Location = new System.Drawing.Point(265, 22);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(68, 17);
		this.label23.TabIndex = 188;
		this.label23.Text = "رقم الجوال";
		this.txtcarNumberdriver.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtcarNumberdriver.Enabled = false;
		this.txtcarNumberdriver.Location = new System.Drawing.Point(356, 19);
		this.txtcarNumberdriver.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.txtcarNumberdriver.Name = "txtcarNumberdriver";
		this.txtcarNumberdriver.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.txtcarNumberdriver.Properties.Appearance.Options.UseFont = true;
		this.txtcarNumberdriver.Properties.Appearance.Options.UseTextOptions = true;
		this.txtcarNumberdriver.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.txtcarNumberdriver.Size = new System.Drawing.Size(242, 22);
		this.txtcarNumberdriver.TabIndex = 187;
		this.label24.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label24.Location = new System.Drawing.Point(605, 22);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(72, 17);
		this.label24.TabIndex = 186;
		this.label24.Text = "رقم اللوحة ";
		this.label24.Click += new System.EventHandler(label24_Click);
		this.label25.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label25.Location = new System.Drawing.Point(961, 22);
		this.label25.Name = "label25";
		this.label25.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label25.Size = new System.Drawing.Size(81, 17);
		this.label25.TabIndex = 105;
		this.label25.Text = "اسم السائق";
		this.groupBox7.Controls.Add(this.label29);
		this.groupBox7.Controls.Add(this.label28);
		this.groupBox7.Controls.Add(this.TxtVat);
		this.groupBox7.Controls.Add(this.TxtLastNet);
		this.groupBox7.Controls.Add(this.Txttotal);
		this.groupBox7.Controls.Add(this.label30);
		this.groupBox7.Controls.Add(this.label27);
		this.groupBox7.Controls.Add(this.Txtcash);
		this.groupBox7.Controls.Add(this.Txtdiscount);
		this.groupBox7.Controls.Add(this.label21);
		this.groupBox7.Controls.Add(this.Txtsafy);
		this.groupBox7.Controls.Add(this.Txtshippingvalue);
		this.groupBox7.Controls.Add(this.label22);
		this.groupBox7.Controls.Add(this.label26);
		this.groupBox7.Location = new System.Drawing.Point(9, 490);
		this.groupBox7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.groupBox7.Size = new System.Drawing.Size(1055, 87);
		this.groupBox7.TabIndex = 125;
		this.groupBox7.TabStop = false;
		this.groupBox7.Text = "التكلفه";
		this.label29.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label29.AutoSize = true;
		this.label29.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label29.Location = new System.Drawing.Point(706, 45);
		this.label29.Name = "label29";
		this.label29.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label29.Size = new System.Drawing.Size(52, 17);
		this.label29.TabIndex = 193;
		this.label29.Text = "المدفوع";
		this.label29.Visible = false;
		this.label28.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label28.AutoSize = true;
		this.label28.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label28.Location = new System.Drawing.Point(962, 46);
		this.label28.Name = "label28";
		this.label28.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label28.Size = new System.Drawing.Size(50, 17);
		this.label28.TabIndex = 193;
		this.label28.Text = "الصافي";
		this.TxtVat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtVat.EditValue = "0.00";
		this.TxtVat.Enabled = false;
		this.TxtVat.Location = new System.Drawing.Point(9, 17);
		this.TxtVat.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtVat.Name = "TxtVat";
		this.TxtVat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtVat.Properties.Appearance.Options.UseFont = true;
		this.TxtVat.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtVat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtVat.Size = new System.Drawing.Size(175, 22);
		this.TxtVat.TabIndex = 192;
		this.TxtLastNet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtLastNet.EditValue = "0.00";
		this.TxtLastNet.Enabled = false;
		this.TxtLastNet.Location = new System.Drawing.Point(258, 43);
		this.TxtLastNet.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
		this.TxtLastNet.Name = "TxtLastNet";
		this.TxtLastNet.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtLastNet.Properties.Appearance.Options.UseFont = true;
		this.TxtLastNet.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLastNet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtLastNet.Size = new System.Drawing.Size(175, 22);
		this.TxtLastNet.TabIndex = 191;
		this.TxtLastNet.Visible = false;
		this.Txttotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Txttotal.EditValue = "0.00";
		this.Txttotal.Enabled = false;
		this.Txttotal.Location = new System.Drawing.Point(258, 17);
		this.Txttotal.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Txttotal.Name = "Txttotal";
		this.Txttotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txttotal.Properties.Appearance.Options.UseFont = true;
		this.Txttotal.Properties.Appearance.Options.UseTextOptions = true;
		this.Txttotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txttotal.Size = new System.Drawing.Size(175, 22);
		this.Txttotal.TabIndex = 191;
		this.label30.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label30.AutoSize = true;
		this.label30.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label30.Location = new System.Drawing.Point(442, 46);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(55, 17);
		this.label30.TabIndex = 190;
		this.label30.Text = "المتبقي";
		this.label30.Visible = false;
		this.label27.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label27.Location = new System.Drawing.Point(440, 20);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(60, 17);
		this.label27.TabIndex = 190;
		this.label27.Text = "الاجمالي";
		this.Txtcash.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Txtcash.EditValue = "0.00";
		this.Txtcash.Enabled = false;
		this.Txtcash.Location = new System.Drawing.Point(524, 43);
		this.Txtcash.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Txtcash.Name = "Txtcash";
		this.Txtcash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txtcash.Properties.Appearance.Options.UseFont = true;
		this.Txtcash.Properties.Appearance.Options.UseTextOptions = true;
		this.Txtcash.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txtcash.Size = new System.Drawing.Size(175, 22);
		this.Txtcash.TabIndex = 189;
		this.Txtcash.Visible = false;
		this.Txtdiscount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Txtdiscount.EditValue = "0.00";
		this.Txtdiscount.Enabled = false;
		this.Txtdiscount.Location = new System.Drawing.Point(524, 17);
		this.Txtdiscount.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Txtdiscount.Name = "Txtdiscount";
		this.Txtdiscount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txtdiscount.Properties.Appearance.Options.UseFont = true;
		this.Txtdiscount.Properties.Appearance.Options.UseTextOptions = true;
		this.Txtdiscount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txtdiscount.Size = new System.Drawing.Size(175, 22);
		this.Txtdiscount.TabIndex = 189;
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label21.Location = new System.Drawing.Point(706, 20);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(46, 17);
		this.label21.TabIndex = 188;
		this.label21.Text = "الخصم";
		this.Txtsafy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Txtsafy.EditValue = "0.00";
		this.Txtsafy.Enabled = false;
		this.Txtsafy.Location = new System.Drawing.Point(780, 43);
		this.Txtsafy.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Txtsafy.Name = "Txtsafy";
		this.Txtsafy.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txtsafy.Properties.Appearance.Options.UseFont = true;
		this.Txtsafy.Properties.Appearance.Options.UseTextOptions = true;
		this.Txtsafy.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txtsafy.Size = new System.Drawing.Size(175, 22);
		this.Txtsafy.TabIndex = 187;
		this.Txtshippingvalue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Txtshippingvalue.EditValue = "0.00";
		this.Txtshippingvalue.Enabled = false;
		this.Txtshippingvalue.Location = new System.Drawing.Point(780, 17);
		this.Txtshippingvalue.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Txtshippingvalue.Name = "Txtshippingvalue";
		this.Txtshippingvalue.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Txtshippingvalue.Properties.Appearance.Options.UseFont = true;
		this.Txtshippingvalue.Properties.Appearance.Options.UseTextOptions = true;
		this.Txtshippingvalue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.Txtshippingvalue.Size = new System.Drawing.Size(175, 22);
		this.Txtshippingvalue.TabIndex = 187;
		this.label22.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label22.Location = new System.Drawing.Point(191, 20);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(49, 17);
		this.label22.TabIndex = 186;
		this.label22.Text = "الضريبه";
		this.label26.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label26.Location = new System.Drawing.Point(962, 20);
		this.label26.Name = "label26";
		this.label26.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label26.Size = new System.Drawing.Size(83, 17);
		this.label26.TabIndex = 105;
		this.label26.Text = "قيمة الشحن";
		this.groupBox3.Controls.Add(this.BtnPrint);
		this.groupBox3.Controls.Add(this.BtnResend);
		this.groupBox3.Controls.Add(this.BtnRec);
		this.groupBox3.Controls.Add(this.BtnPay);
		this.groupBox3.Controls.Add(this.BtnExit);
		this.groupBox3.Location = new System.Drawing.Point(10, 581);
		this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.groupBox3.Size = new System.Drawing.Size(1055, 70);
		this.groupBox3.TabIndex = 125;
		this.groupBox3.TabStop = false;
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(802, 18);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(117, 40);
		this.BtnPrint.TabIndex = 12;
		this.BtnPrint.Text = "طباعة Ctrl P";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnResend.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnResend.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnResend.Appearance.Options.UseFont = true;
		this.BtnResend.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnResend.ImageOptions.Image");
		this.BtnResend.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnResend.Location = new System.Drawing.Point(927, 18);
		this.BtnResend.Margin = new System.Windows.Forms.Padding(4);
		this.BtnResend.Name = "BtnResend";
		this.BtnResend.Size = new System.Drawing.Size(117, 40);
		this.BtnResend.TabIndex = 13;
		this.BtnResend.Text = "تسليم";
		this.BtnResend.Click += new System.EventHandler(BtnResend_Click);
		this.BtnRec.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnRec.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnRec.Appearance.Options.UseFont = true;
		this.BtnRec.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRec.ImageOptions.Image");
		this.BtnRec.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnRec.Location = new System.Drawing.Point(125, 18);
		this.BtnRec.Margin = new System.Windows.Forms.Padding(4);
		this.BtnRec.Name = "BtnRec";
		this.BtnRec.Size = new System.Drawing.Size(117, 40);
		this.BtnRec.TabIndex = 14;
		this.BtnRec.Text = "سند قبض";
		this.BtnRec.Click += new System.EventHandler(BtnRec_Click);
		this.BtnPay.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPay.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPay.Appearance.Options.UseFont = true;
		this.BtnPay.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPay.ImageOptions.Image");
		this.BtnPay.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPay.Location = new System.Drawing.Point(1, 18);
		this.BtnPay.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPay.Name = "BtnPay";
		this.BtnPay.Size = new System.Drawing.Size(117, 40);
		this.BtnPay.TabIndex = 14;
		this.BtnPay.Text = "سند صرف";
		this.BtnPay.Click += new System.EventHandler(BtnPay_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(678, 18);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(117, 40);
		this.BtnExit.TabIndex = 14;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1075, 662);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox7);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.TxtDate);
		base.Controls.Add(this.LblCustLast);
		base.Margin = new System.Windows.Forms.Padding(2);
		base.MaximizeBox = false;
		base.Name = "FrmResend";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تسليم سيارة";
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnSearchReceive.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnSearchReceiveId.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAddress.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCommercialRecord.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumVat.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMobile.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCustName.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.CmpColor.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CmpCarType.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtcarNumber.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtModel.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txtbodynumber.Properties).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.Txtdestination.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtUploader.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateResend.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateResend.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateReceipt.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateOrd.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateOrd.Properties).EndInit();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.Txtmobiledriver.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtcarNumberdriver.Properties).EndInit();
		this.groupBox7.ResumeLayout(false);
		this.groupBox7.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtVat.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLastNet.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txttotal.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txtcash.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txtdiscount.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txtsafy.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Txtshippingvalue.Properties).EndInit();
		this.groupBox3.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


