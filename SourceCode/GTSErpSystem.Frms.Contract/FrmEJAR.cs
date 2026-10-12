using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Contract;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Contract.Search;
using GTSErpSystem.Frms.FrmsMain;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Contract;

public class FrmEJAR : XtraForm
{
	private Class_EJAR CUS = new Class_EJAR();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private IContainer components = null;

	private Label label1;

	private Label LblCode;

	private ButtonEdit TxtContract;

	private TextBox TxtContractRegistryNumber;

	private TextBox TxtTypeOfContract;

	private Label label10;

	private Label lblll;

	private GroupBox g1;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private SimpleButton BtnPrint;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private Label label20;

	private TextBox TxtNote;

	private GroupBox groupBox1;

	private Label label22;

	private Label label21;

	private Label label5;

	private TextBox TxtMethodOfPaymentOfContractFees;

	private Label label24;

	private Label label23;

	private TextBox TxtThePlaceOfConclusionOfTheContract;

	private TextBox TxtLandlordsEmail;

	private TextBox TxtLessorIdentificationNumber;

	private TextBox TxtThelandlordsMobileNumber;

	private TextBox TxtLessorIDType;

	private TextBox TxtThelessorsNationality;

	private TextBox TxtLessorsName;

	private Label label8;

	private Label label7;

	private Label label6;

	private Label label4;

	private Label label3;

	private Label label2;

	private TextBox TxtTheLessorIsRepresentedByHimselfOrHerself;

	private Label label9;

	private GroupBox g2;

	private TextBox TxtTheDateOfTheCommercialRegistrationOfTheTenant;

	private TextBox TxtUnifiedNumberOfTheTenant;

	private TextBox TxtTheNameOfTheTenantCompany;

	private TextBox TxtTheIssuingEntityOfTheTenant;

	private TextBox TxtCommercialRegistrationNumberOfTheTenant;

	private TextBox TxtTenantAreaType;

	private Label label16;

	private Label label15;

	private Label label14;

	private Label label13;

	private Label label12;

	private Label label11;

	private Label label17;

	private Label label18;

	private GroupBox groupBox5;

	private TextBox TxtTheNationalityOfTheTenantsRepresentative;

	private TextBox TxtTheNameOfTheTenantsRepresentative;

	private DateEdit TxtTheEndDateOfTheLeaseTerm;

	private DateEdit TxtTheStartDateOftheLeaseTerm;

	private DateEdit TxtTheDateOfTheContract;

	private TextBox TxtLandlordNationalAddress;

	private Label label19;

	private Label label25;

	private TextBox TxtconditionallySuspended;

	public FrmEJAR()
	{
		InitializeComponent();
		Clear();
		if (LoginDetails.FrmOrderClose)
		{
			BtnNew.Visible = false;
			BtnSave.Visible = false;
			BtnDelete.Visible = false;
			BtnExit.Visible = false;
			BtnPrint.Visible = false;
		}
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnDelete);
		}
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		Clear();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void Clear()
	{
		TxtContract.Text = "0";
		TxtContractRegistryNumber.Text = "";
		TxtTheDateOfTheContract.DateTime = DateTime.Now;
		TxtTheStartDateOftheLeaseTerm.DateTime = DateTime.Now;
		TxtTheEndDateOfTheLeaseTerm.DateTime = DateTime.Now.AddYears(1);
		TxtconditionallySuspended.Text = "";
		TxtMethodOfPaymentOfContractFees.Text = "";
		TxtTypeOfContract.Text = "";
		TxtThePlaceOfConclusionOfTheContract.Text = "";
		TxtLessorsName.Text = "";
		TxtThelessorsNationality.Text = "";
		TxtLessorIDType.Text = "";
		TxtThelandlordsMobileNumber.Text = "";
		TxtLandlordNationalAddress.Text = "";
		TxtLessorIdentificationNumber.Text = "";
		TxtLandlordsEmail.Text = "";
		TxtTheLessorIsRepresentedByHimselfOrHerself.Text = "";
		TxtTenantAreaType.Text = "";
		TxtCommercialRegistrationNumberOfTheTenant.Text = "";
		TxtTheIssuingEntityOfTheTenant.Text = "";
		TxtTheNameOfTheTenantCompany.Text = "";
		TxtUnifiedNumberOfTheTenant.Text = "";
		TxtTheDateOfTheCommercialRegistrationOfTheTenant.Text = "";
		TxtTheNameOfTheTenantsRepresentative.Text = "";
		TxtTheNationalityOfTheTenantsRepresentative.Text = "";
		TxtNote.Text = "";
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 147))
		{
			int num = 0;
			if (LblCode.Text != "" || LblCode.Text != "0")
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtContract.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = CUS.InsertUpdate(Convert.ToInt32(TxtContract.Text), TxtContractRegistryNumber.Text, TxtTheDateOfTheContract.DateTime, TxtTheStartDateOftheLeaseTerm.DateTime, TxtconditionallySuspended.Text, TxtMethodOfPaymentOfContractFees.Text, TxtTypeOfContract.Text, TxtThePlaceOfConclusionOfTheContract.Text, TxtTheEndDateOfTheLeaseTerm.DateTime, TxtLessorsName.Text, TxtThelessorsNationality.Text, TxtLessorIDType.Text, TxtThelandlordsMobileNumber.Text, TxtLandlordNationalAddress.Text, TxtLessorIdentificationNumber.Text, TxtLandlordsEmail.Text, TxtTheLessorIsRepresentedByHimselfOrHerself.Text, TxtTenantAreaType.Text, TxtCommercialRegistrationNumberOfTheTenant.Text, TxtTheIssuingEntityOfTheTenant.Text, TxtTheNameOfTheTenantCompany.Text, TxtUnifiedNumberOfTheTenant.Text, TxtTheDateOfTheCommercialRegistrationOfTheTenant.Text, TxtTheNameOfTheTenantsRepresentative.Text, TxtTheNationalityOfTheTenantsRepresentative.Text, TxtNote.Text);
					if (num > 0)
					{
						TxtContract.Text = num.ToString();
						MessageBox.Show("تم حفظ بيانات العملاء  ( " + TxtContract.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات العملاء بنفس الاسم ( " + TxtContract.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtContract.Focus();
					break;
				}
			}
			else if (LblCode.Text == "" || LblCode.Text == "0")
			{
				MessageBox.Show(" عفوا، ادخل اسم العميل ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtContract.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 147))
		{
			int num = 0;
			if (TxtContract.Text != "0")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtContract.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = CUS.Delete(Convert.ToInt32(TxtContract.Text));
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات العملاء ( " + TxtContract.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						Clear();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtContract.Text + " ) لوجود عمليات جارية بهذا العملاء", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				case DialogResult.No:
					TxtContract.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر اسم العملاء ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtContract.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 147))
		{
			if (TxtContract.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Contract_EJAR"].Clear();
				SqlCommand sqlCommand = new SqlCommand("SELECT_Contract_EJAR", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtContract.Text));
				sqlDataAdapter.Fill(dataSetALL.Contract_EJAR);
				sqlConnection.Close();
				if (dataSetALL.Tables["Contract_EJAR"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintEJAR.mrt");
					stiReport.RegData(dataSetALL.Contract_EJAR);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport.Render();
					stiReport.Show(dialogForm: true);
					FrmReportGetOrder frmReportGetOrder = new FrmReportGetOrder();
					frmReportGetOrder.stiViewerControl1.Report = stiReport;
				}
				else
				{
					MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر رقم العميل ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchEJAR frmSearchEJAR = new FrmSearchEJAR();
			frmSearchEJAR.ShowDialog();
			if (frmSearchEJAR.Code > 0)
			{
				SearchCust(frmSearchEJAR.Code);
			}
			else
			{
				TxtContract.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
			Clear();
		}
	}

	private void SearchCust(int CustCode)
	{
		SELECT_Contract_EJAR_Result sELECT_Contract_EJAR_Result = CUS.GetSelectAllEJAR(CustCode).FirstOrDefault();
		if (sELECT_Contract_EJAR_Result != null)
		{
			TxtContract.Text = CustCode.ToString();
			TxtContractRegistryNumber.Text = sELECT_Contract_EJAR_Result.ContractRegistryNumber;
			TxtTheDateOfTheContract.DateTime = Convert.ToDateTime(sELECT_Contract_EJAR_Result.TheDateOfTheContract);
			TxtTheStartDateOftheLeaseTerm.DateTime = Convert.ToDateTime(sELECT_Contract_EJAR_Result.TheStartDateOftheLeaseTerm);
			TxtTheEndDateOfTheLeaseTerm.DateTime = Convert.ToDateTime(sELECT_Contract_EJAR_Result.TheEndDateOfTheLeaseTerm);
			TxtconditionallySuspended.Text = sELECT_Contract_EJAR_Result.conditionallySuspended;
			TxtMethodOfPaymentOfContractFees.Text = sELECT_Contract_EJAR_Result.MethodOfPaymentOfContractFees;
			TxtTypeOfContract.Text = sELECT_Contract_EJAR_Result.TypeOfContract;
			TxtThePlaceOfConclusionOfTheContract.Text = sELECT_Contract_EJAR_Result.ThePlaceOfConclusionOfTheContract;
			TxtLessorsName.Text = sELECT_Contract_EJAR_Result.LessorsName;
			TxtThelessorsNationality.Text = sELECT_Contract_EJAR_Result.ThelessorsNationality;
			TxtLessorIDType.Text = sELECT_Contract_EJAR_Result.LessorIDType;
			TxtThelandlordsMobileNumber.Text = sELECT_Contract_EJAR_Result.ThelandlordsMobileNumber;
			TxtLandlordNationalAddress.Text = sELECT_Contract_EJAR_Result.LandlordNationalAddress;
			TxtLessorIdentificationNumber.Text = sELECT_Contract_EJAR_Result.LessorIdentificationNumber;
			TxtLandlordsEmail.Text = sELECT_Contract_EJAR_Result.LandlordsEmail;
			TxtTheLessorIsRepresentedByHimselfOrHerself.Text = sELECT_Contract_EJAR_Result.TheLessorIsRepresentedByHimselfOrHerself;
			TxtTenantAreaType.Text = sELECT_Contract_EJAR_Result.TenantAreaType;
			TxtCommercialRegistrationNumberOfTheTenant.Text = sELECT_Contract_EJAR_Result.CommercialRegistrationNumberOfTheTenant;
			TxtTheIssuingEntityOfTheTenant.Text = sELECT_Contract_EJAR_Result.TheIssuingEntityOfTheTenant;
			TxtTheNameOfTheTenantCompany.Text = sELECT_Contract_EJAR_Result.TheNameOfTheTenantCompany;
			TxtUnifiedNumberOfTheTenant.Text = sELECT_Contract_EJAR_Result.UnifiedNumberOfTheTenant;
			TxtTheDateOfTheCommercialRegistrationOfTheTenant.Text = sELECT_Contract_EJAR_Result.TheDateOfTheCommercialRegistrationOfTheTenant;
			TxtTheNameOfTheTenantsRepresentative.Text = sELECT_Contract_EJAR_Result.TheNameOfTheTenantsRepresentative;
			TxtTheNationalityOfTheTenantsRepresentative.Text = sELECT_Contract_EJAR_Result.TheNationalityOfTheTenantsRepresentative;
			TxtNote.Text = sELECT_Contract_EJAR_Result.Note;
		}
		else
		{
			Clear();
		}
	}

	private void TxtTheEndDateOfTheLeaseTerm_TextChanged(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Contract.FrmEJAR));
		this.label1 = new System.Windows.Forms.Label();
		this.LblCode = new System.Windows.Forms.Label();
		this.TxtContract = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtContractRegistryNumber = new System.Windows.Forms.TextBox();
		this.TxtTypeOfContract = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.lblll = new System.Windows.Forms.Label();
		this.g1 = new System.Windows.Forms.GroupBox();
		this.TxtTheEndDateOfTheLeaseTerm = new DevExpress.XtraEditors.DateEdit();
		this.TxtTheStartDateOftheLeaseTerm = new DevExpress.XtraEditors.DateEdit();
		this.TxtTheDateOfTheContract = new DevExpress.XtraEditors.DateEdit();
		this.TxtMethodOfPaymentOfContractFees = new System.Windows.Forms.TextBox();
		this.label24 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtThePlaceOfConclusionOfTheContract = new System.Windows.Forms.TextBox();
		this.label22 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.TxtLandlordsEmail = new System.Windows.Forms.TextBox();
		this.TxtLessorIdentificationNumber = new System.Windows.Forms.TextBox();
		this.TxtThelandlordsMobileNumber = new System.Windows.Forms.TextBox();
		this.TxtLessorIDType = new System.Windows.Forms.TextBox();
		this.TxtThelessorsNationality = new System.Windows.Forms.TextBox();
		this.TxtLessorsName = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.TxtTheLessorIsRepresentedByHimselfOrHerself = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.label20 = new System.Windows.Forms.Label();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.g2 = new System.Windows.Forms.GroupBox();
		this.TxtTheDateOfTheCommercialRegistrationOfTheTenant = new System.Windows.Forms.TextBox();
		this.TxtUnifiedNumberOfTheTenant = new System.Windows.Forms.TextBox();
		this.TxtTheNameOfTheTenantCompany = new System.Windows.Forms.TextBox();
		this.TxtTheIssuingEntityOfTheTenant = new System.Windows.Forms.TextBox();
		this.TxtCommercialRegistrationNumberOfTheTenant = new System.Windows.Forms.TextBox();
		this.TxtTenantAreaType = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.TxtTheNationalityOfTheTenantsRepresentative = new System.Windows.Forms.TextBox();
		this.TxtTheNameOfTheTenantsRepresentative = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtLandlordNationalAddress = new System.Windows.Forms.TextBox();
		this.TxtconditionallySuspended = new System.Windows.Forms.TextBox();
		this.label25 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.TxtContract.Properties).BeginInit();
		this.g1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTheEndDateOfTheLeaseTerm.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTheEndDateOfTheLeaseTerm.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTheStartDateOftheLeaseTerm.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTheStartDateOftheLeaseTerm.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTheDateOfTheContract.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTheDateOfTheContract.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.g2.SuspendLayout();
		this.groupBox5.SuspendLayout();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label1.Location = new System.Drawing.Point(941, 22);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label1.Size = new System.Drawing.Size(70, 19);
		this.label1.TabIndex = 0;
		this.label1.Text = "كود العقد";
		this.LblCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblCode.Location = new System.Drawing.Point(552, 19);
		this.LblCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblCode.Name = "LblCode";
		this.LblCode.Size = new System.Drawing.Size(22, 23);
		this.LblCode.TabIndex = 0;
		this.LblCode.Text = "0";
		this.TxtContract.EditValue = "0";
		this.TxtContract.Location = new System.Drawing.Point(598, 19);
		this.TxtContract.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtContract.Name = "TxtContract";
		this.TxtContract.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtContract.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtContract.Properties.Appearance.Options.UseBackColor = true;
		this.TxtContract.Properties.Appearance.Options.UseFont = true;
		this.TxtContract.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtContract.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtContract.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtContract.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtContract.Properties.ReadOnly = true;
		this.TxtContract.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.TxtContract.Size = new System.Drawing.Size(337, 26);
		this.TxtContract.TabIndex = 111;
		this.TxtContract.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.TxtContractRegistryNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtContractRegistryNumber.Location = new System.Drawing.Point(599, 49);
		this.TxtContractRegistryNumber.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtContractRegistryNumber.Name = "TxtContractRegistryNumber";
		this.TxtContractRegistryNumber.Size = new System.Drawing.Size(336, 27);
		this.TxtContractRegistryNumber.TabIndex = 5;
		this.TxtContractRegistryNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtTypeOfContract.Location = new System.Drawing.Point(8, 19);
		this.TxtTypeOfContract.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTypeOfContract.Name = "TxtTypeOfContract";
		this.TxtTypeOfContract.Size = new System.Drawing.Size(353, 27);
		this.TxtTypeOfContract.TabIndex = 6;
		this.TxtTypeOfContract.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label10.AutoSize = true;
		this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label10.Location = new System.Drawing.Point(369, 22);
		this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label10.Name = "label10";
		this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label10.Size = new System.Drawing.Size(69, 19);
		this.label10.TabIndex = 178;
		this.label10.Text = "نوع العقد";
		this.lblll.AutoSize = true;
		this.lblll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblll.Location = new System.Drawing.Point(941, 52);
		this.lblll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblll.Name = "lblll";
		this.lblll.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblll.Size = new System.Drawing.Size(116, 19);
		this.lblll.TabIndex = 9;
		this.lblll.Text = "رقم سجل العقد";
		this.g1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.g1.Controls.Add(this.TxtTheEndDateOfTheLeaseTerm);
		this.g1.Controls.Add(this.TxtTheStartDateOftheLeaseTerm);
		this.g1.Controls.Add(this.TxtTheDateOfTheContract);
		this.g1.Controls.Add(this.TxtMethodOfPaymentOfContractFees);
		this.g1.Controls.Add(this.label24);
		this.g1.Controls.Add(this.label23);
		this.g1.Controls.Add(this.TxtThePlaceOfConclusionOfTheContract);
		this.g1.Controls.Add(this.label22);
		this.g1.Controls.Add(this.label21);
		this.g1.Controls.Add(this.label5);
		this.g1.Controls.Add(this.label25);
		this.g1.Controls.Add(this.lblll);
		this.g1.Controls.Add(this.label10);
		this.g1.Controls.Add(this.TxtTypeOfContract);
		this.g1.Controls.Add(this.TxtconditionallySuspended);
		this.g1.Controls.Add(this.TxtContractRegistryNumber);
		this.g1.Controls.Add(this.TxtContract);
		this.g1.Controls.Add(this.LblCode);
		this.g1.Controls.Add(this.label1);
		this.g1.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.g1.Location = new System.Drawing.Point(13, 11);
		this.g1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.g1.Name = "g1";
		this.g1.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.g1.Size = new System.Drawing.Size(1107, 181);
		this.g1.TabIndex = 1;
		this.g1.TabStop = false;
		this.TxtTheEndDateOfTheLeaseTerm.EditValue = null;
		this.TxtTheEndDateOfTheLeaseTerm.Location = new System.Drawing.Point(9, 83);
		this.TxtTheEndDateOfTheLeaseTerm.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTheEndDateOfTheLeaseTerm.Name = "TxtTheEndDateOfTheLeaseTerm";
		this.TxtTheEndDateOfTheLeaseTerm.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtTheEndDateOfTheLeaseTerm.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTheEndDateOfTheLeaseTerm.Properties.Appearance.Options.UseFont = true;
		this.TxtTheEndDateOfTheLeaseTerm.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTheEndDateOfTheLeaseTerm.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTheEndDateOfTheLeaseTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtTheEndDateOfTheLeaseTerm.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtTheEndDateOfTheLeaseTerm.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtTheEndDateOfTheLeaseTerm.Properties.MaskSettings.Set("mask", "yyyy/MM/dd");
		this.TxtTheEndDateOfTheLeaseTerm.Size = new System.Drawing.Size(353, 26);
		this.TxtTheEndDateOfTheLeaseTerm.TabIndex = 187;
		this.TxtTheStartDateOftheLeaseTerm.EditValue = null;
		this.TxtTheStartDateOftheLeaseTerm.Location = new System.Drawing.Point(599, 111);
		this.TxtTheStartDateOftheLeaseTerm.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTheStartDateOftheLeaseTerm.Name = "TxtTheStartDateOftheLeaseTerm";
		this.TxtTheStartDateOftheLeaseTerm.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtTheStartDateOftheLeaseTerm.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTheStartDateOftheLeaseTerm.Properties.Appearance.Options.UseFont = true;
		this.TxtTheStartDateOftheLeaseTerm.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTheStartDateOftheLeaseTerm.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTheStartDateOftheLeaseTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtTheStartDateOftheLeaseTerm.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtTheStartDateOftheLeaseTerm.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtTheStartDateOftheLeaseTerm.Properties.MaskSettings.Set("mask", "yyyy/MM/dd");
		this.TxtTheStartDateOftheLeaseTerm.Size = new System.Drawing.Size(336, 26);
		this.TxtTheStartDateOftheLeaseTerm.TabIndex = 186;
		this.TxtTheDateOfTheContract.EditValue = null;
		this.TxtTheDateOfTheContract.Location = new System.Drawing.Point(599, 80);
		this.TxtTheDateOfTheContract.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTheDateOfTheContract.Name = "TxtTheDateOfTheContract";
		this.TxtTheDateOfTheContract.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtTheDateOfTheContract.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTheDateOfTheContract.Properties.Appearance.Options.UseFont = true;
		this.TxtTheDateOfTheContract.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTheDateOfTheContract.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTheDateOfTheContract.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtTheDateOfTheContract.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtTheDateOfTheContract.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtTheDateOfTheContract.Properties.MaskSettings.Set("mask", "yyyy/MM/dd");
		this.TxtTheDateOfTheContract.Size = new System.Drawing.Size(336, 26);
		this.TxtTheDateOfTheContract.TabIndex = 185;
		this.TxtMethodOfPaymentOfContractFees.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtMethodOfPaymentOfContractFees.Location = new System.Drawing.Point(8, 114);
		this.TxtMethodOfPaymentOfContractFees.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtMethodOfPaymentOfContractFees.Name = "TxtMethodOfPaymentOfContractFees";
		this.TxtMethodOfPaymentOfContractFees.Size = new System.Drawing.Size(353, 27);
		this.TxtMethodOfPaymentOfContractFees.TabIndex = 183;
		this.TxtMethodOfPaymentOfContractFees.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtMethodOfPaymentOfContractFees.TextChanged += new System.EventHandler(TxtTheEndDateOfTheLeaseTerm_TextChanged);
		this.label24.AutoSize = true;
		this.label24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label24.Location = new System.Drawing.Point(369, 86);
		this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label24.Name = "label24";
		this.label24.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label24.Size = new System.Drawing.Size(150, 19);
		this.label24.TabIndex = 180;
		this.label24.Text = "تاريخ نهاية مدة الايجار";
		this.label23.AutoSize = true;
		this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label23.Location = new System.Drawing.Point(369, 53);
		this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label23.Name = "label23";
		this.label23.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label23.Size = new System.Drawing.Size(114, 19);
		this.label23.TabIndex = 180;
		this.label23.Text = "مكان ابرام العقد";
		this.TxtThePlaceOfConclusionOfTheContract.Location = new System.Drawing.Point(8, 50);
		this.TxtThePlaceOfConclusionOfTheContract.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtThePlaceOfConclusionOfTheContract.Name = "TxtThePlaceOfConclusionOfTheContract";
		this.TxtThePlaceOfConclusionOfTheContract.Size = new System.Drawing.Size(353, 27);
		this.TxtThePlaceOfConclusionOfTheContract.TabIndex = 179;
		this.TxtThePlaceOfConclusionOfTheContract.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label22.AutoSize = true;
		this.label22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label22.Location = new System.Drawing.Point(369, 117);
		this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label22.Name = "label22";
		this.label22.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label22.Size = new System.Drawing.Size(158, 19);
		this.label22.TabIndex = 9;
		this.label22.Text = "طريقة دفع رسوم العقد";
		this.label21.AutoSize = true;
		this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label21.Location = new System.Drawing.Point(941, 114);
		this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label21.Name = "label21";
		this.label21.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label21.Size = new System.Drawing.Size(151, 19);
		this.label21.TabIndex = 9;
		this.label21.Text = "تاريخ اجراء مدة الايجار";
		this.label5.AutoSize = true;
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(941, 83);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label5.Size = new System.Drawing.Size(115, 19);
		this.label5.TabIndex = 9;
		this.label5.Text = "تاريخ اجراء العقد";
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.TxtLandlordsEmail);
		this.groupBox2.Controls.Add(this.TxtLessorIdentificationNumber);
		this.groupBox2.Controls.Add(this.TxtThelandlordsMobileNumber);
		this.groupBox2.Controls.Add(this.TxtLandlordNationalAddress);
		this.groupBox2.Controls.Add(this.TxtLessorIDType);
		this.groupBox2.Controls.Add(this.TxtThelessorsNationality);
		this.groupBox2.Controls.Add(this.TxtLessorsName);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.label19);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Location = new System.Drawing.Point(12, 197);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1108, 145);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.TxtLandlordsEmail.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtLandlordsEmail.Location = new System.Drawing.Point(9, 48);
		this.TxtLandlordsEmail.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtLandlordsEmail.Name = "TxtLandlordsEmail";
		this.TxtLandlordsEmail.Size = new System.Drawing.Size(353, 23);
		this.TxtLandlordsEmail.TabIndex = 15;
		this.TxtLandlordsEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtLessorIdentificationNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtLessorIdentificationNumber.Location = new System.Drawing.Point(8, 21);
		this.TxtLessorIdentificationNumber.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtLessorIdentificationNumber.Name = "TxtLessorIdentificationNumber";
		this.TxtLessorIdentificationNumber.Size = new System.Drawing.Size(354, 23);
		this.TxtLessorIdentificationNumber.TabIndex = 14;
		this.TxtLessorIdentificationNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtThelandlordsMobileNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtThelandlordsMobileNumber.Location = new System.Drawing.Point(9, 75);
		this.TxtThelandlordsMobileNumber.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtThelandlordsMobileNumber.Name = "TxtThelandlordsMobileNumber";
		this.TxtThelandlordsMobileNumber.Size = new System.Drawing.Size(353, 23);
		this.TxtThelandlordsMobileNumber.TabIndex = 13;
		this.TxtThelandlordsMobileNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtLessorIDType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtLessorIDType.Location = new System.Drawing.Point(598, 75);
		this.TxtLessorIDType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtLessorIDType.Name = "TxtLessorIDType";
		this.TxtLessorIDType.Size = new System.Drawing.Size(334, 23);
		this.TxtLessorIDType.TabIndex = 12;
		this.TxtLessorIDType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtThelessorsNationality.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtThelessorsNationality.Location = new System.Drawing.Point(597, 48);
		this.TxtThelessorsNationality.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtThelessorsNationality.Name = "TxtThelessorsNationality";
		this.TxtThelessorsNationality.Size = new System.Drawing.Size(336, 23);
		this.TxtThelessorsNationality.TabIndex = 11;
		this.TxtThelessorsNationality.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtLessorsName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtLessorsName.Location = new System.Drawing.Point(597, 21);
		this.TxtLessorsName.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtLessorsName.Name = "TxtLessorsName";
		this.TxtLessorsName.Size = new System.Drawing.Size(336, 23);
		this.TxtLessorsName.TabIndex = 10;
		this.TxtLessorsName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label8.AutoSize = true;
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(370, 51);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label8.Size = new System.Drawing.Size(93, 16);
		this.label8.TabIndex = 9;
		this.label8.Text = "البريد الالكتروني";
		this.label7.AutoSize = true;
		this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label7.Location = new System.Drawing.Point(370, 25);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label7.Size = new System.Drawing.Size(61, 16);
		this.label7.TabIndex = 9;
		this.label7.Text = "رقم الهوية";
		this.label6.AutoSize = true;
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(372, 78);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label6.Size = new System.Drawing.Size(63, 16);
		this.label6.TabIndex = 9;
		this.label6.Text = "رقم الجوال";
		this.label4.AutoSize = true;
		this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label4.Location = new System.Drawing.Point(940, 78);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label4.Size = new System.Drawing.Size(60, 16);
		this.label4.TabIndex = 9;
		this.label4.Text = "نوع الهوية";
		this.label3.AutoSize = true;
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(941, 51);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label3.Size = new System.Drawing.Size(54, 16);
		this.label3.TabIndex = 9;
		this.label3.Text = "الجنسية";
		this.label2.AutoSize = true;
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(942, 25);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label2.Size = new System.Drawing.Size(40, 16);
		this.label2.TabIndex = 9;
		this.label2.Text = "الاسم";
		this.groupBox3.Controls.Add(this.TxtTheLessorIsRepresentedByHimselfOrHerself);
		this.groupBox3.Controls.Add(this.label9);
		this.groupBox3.Location = new System.Drawing.Point(12, 348);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1108, 58);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.TxtTheLessorIsRepresentedByHimselfOrHerself.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtTheLessorIsRepresentedByHimselfOrHerself.Location = new System.Drawing.Point(599, 21);
		this.TxtTheLessorIsRepresentedByHimselfOrHerself.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTheLessorIsRepresentedByHimselfOrHerself.Name = "TxtTheLessorIsRepresentedByHimselfOrHerself";
		this.TxtTheLessorIsRepresentedByHimselfOrHerself.Size = new System.Drawing.Size(334, 23);
		this.TxtTheLessorIsRepresentedByHimselfOrHerself.TabIndex = 10;
		this.TxtTheLessorIsRepresentedByHimselfOrHerself.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label9.AutoSize = true;
		this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label9.Location = new System.Drawing.Point(941, 24);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label9.Size = new System.Drawing.Size(111, 16);
		this.label9.TabIndex = 9;
		this.label9.Text = "بيانات ممثل المؤجر";
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(694, 698);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(155, 39);
		this.BtnPrint.TabIndex = 11;
		this.BtnPrint.Text = "طباعة";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(368, 698);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(155, 39);
		this.BtnSave.TabIndex = 6;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(205, 698);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(155, 39);
		this.BtnNew.TabIndex = 9;
		this.BtnNew.Text = "جديد F4";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(531, 698);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(155, 39);
		this.BtnDelete.TabIndex = 8;
		this.BtnDelete.Text = "حذف F7";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(857, 698);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(152, 39);
		this.BtnExit.TabIndex = 10;
		this.BtnExit.Text = "خروج";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 9.25f);
		this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label20.Location = new System.Drawing.Point(943, 22);
		this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(68, 19);
		this.label20.TabIndex = 182;
		this.label20.Text = "ملاحظات";
		this.TxtNote.Location = new System.Drawing.Point(10, 21);
		this.TxtNote.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtNote.Multiline = true;
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtNote.Size = new System.Drawing.Size(925, 50);
		this.TxtNote.TabIndex = 181;
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Location = new System.Drawing.Point(12, 601);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1109, 80);
		this.groupBox1.TabIndex = 183;
		this.groupBox1.TabStop = false;
		this.g2.Controls.Add(this.TxtTheDateOfTheCommercialRegistrationOfTheTenant);
		this.g2.Controls.Add(this.TxtUnifiedNumberOfTheTenant);
		this.g2.Controls.Add(this.TxtTheNameOfTheTenantCompany);
		this.g2.Controls.Add(this.TxtTheIssuingEntityOfTheTenant);
		this.g2.Controls.Add(this.TxtCommercialRegistrationNumberOfTheTenant);
		this.g2.Controls.Add(this.TxtTenantAreaType);
		this.g2.Controls.Add(this.label16);
		this.g2.Controls.Add(this.label15);
		this.g2.Controls.Add(this.label14);
		this.g2.Controls.Add(this.label13);
		this.g2.Controls.Add(this.label12);
		this.g2.Controls.Add(this.label11);
		this.g2.Location = new System.Drawing.Point(12, 412);
		this.g2.Name = "g2";
		this.g2.Size = new System.Drawing.Size(1108, 126);
		this.g2.TabIndex = 3;
		this.g2.TabStop = false;
		this.TxtTheDateOfTheCommercialRegistrationOfTheTenant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtTheDateOfTheCommercialRegistrationOfTheTenant.Location = new System.Drawing.Point(7, 75);
		this.TxtTheDateOfTheCommercialRegistrationOfTheTenant.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTheDateOfTheCommercialRegistrationOfTheTenant.Name = "TxtTheDateOfTheCommercialRegistrationOfTheTenant";
		this.TxtTheDateOfTheCommercialRegistrationOfTheTenant.Size = new System.Drawing.Size(353, 23);
		this.TxtTheDateOfTheCommercialRegistrationOfTheTenant.TabIndex = 15;
		this.TxtTheDateOfTheCommercialRegistrationOfTheTenant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtUnifiedNumberOfTheTenant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtUnifiedNumberOfTheTenant.Location = new System.Drawing.Point(7, 48);
		this.TxtUnifiedNumberOfTheTenant.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtUnifiedNumberOfTheTenant.Name = "TxtUnifiedNumberOfTheTenant";
		this.TxtUnifiedNumberOfTheTenant.Size = new System.Drawing.Size(353, 23);
		this.TxtUnifiedNumberOfTheTenant.TabIndex = 14;
		this.TxtUnifiedNumberOfTheTenant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtTheNameOfTheTenantCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtTheNameOfTheTenantCompany.Location = new System.Drawing.Point(7, 21);
		this.TxtTheNameOfTheTenantCompany.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTheNameOfTheTenantCompany.Name = "TxtTheNameOfTheTenantCompany";
		this.TxtTheNameOfTheTenantCompany.Size = new System.Drawing.Size(353, 23);
		this.TxtTheNameOfTheTenantCompany.TabIndex = 13;
		this.TxtTheNameOfTheTenantCompany.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtTheIssuingEntityOfTheTenant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtTheIssuingEntityOfTheTenant.Location = new System.Drawing.Point(598, 76);
		this.TxtTheIssuingEntityOfTheTenant.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTheIssuingEntityOfTheTenant.Name = "TxtTheIssuingEntityOfTheTenant";
		this.TxtTheIssuingEntityOfTheTenant.Size = new System.Drawing.Size(334, 23);
		this.TxtTheIssuingEntityOfTheTenant.TabIndex = 12;
		this.TxtTheIssuingEntityOfTheTenant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtCommercialRegistrationNumberOfTheTenant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtCommercialRegistrationNumberOfTheTenant.Location = new System.Drawing.Point(598, 49);
		this.TxtCommercialRegistrationNumberOfTheTenant.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCommercialRegistrationNumberOfTheTenant.Name = "TxtCommercialRegistrationNumberOfTheTenant";
		this.TxtCommercialRegistrationNumberOfTheTenant.Size = new System.Drawing.Size(334, 23);
		this.TxtCommercialRegistrationNumberOfTheTenant.TabIndex = 11;
		this.TxtCommercialRegistrationNumberOfTheTenant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtTenantAreaType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtTenantAreaType.Location = new System.Drawing.Point(598, 21);
		this.TxtTenantAreaType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTenantAreaType.Name = "TxtTenantAreaType";
		this.TxtTenantAreaType.Size = new System.Drawing.Size(334, 23);
		this.TxtTenantAreaType.TabIndex = 10;
		this.TxtTenantAreaType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label16.AutoSize = true;
		this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label16.Location = new System.Drawing.Point(368, 79);
		this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label16.Name = "label16";
		this.label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label16.Size = new System.Drawing.Size(121, 16);
		this.label16.TabIndex = 9;
		this.label16.Text = "تاريخ السجل التجاري";
		this.label15.AutoSize = true;
		this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label15.Location = new System.Drawing.Point(368, 51);
		this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label15.Name = "label15";
		this.label15.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label15.Size = new System.Drawing.Size(74, 16);
		this.label15.TabIndex = 9;
		this.label15.Text = "الرقم الموحد";
		this.label14.AutoSize = true;
		this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label14.Location = new System.Drawing.Point(367, 24);
		this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label14.Name = "label14";
		this.label14.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label14.Size = new System.Drawing.Size(143, 16);
		this.label14.TabIndex = 9;
		this.label14.Text = "اسم الشركة / المؤسسة";
		this.label13.AutoSize = true;
		this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label13.Location = new System.Drawing.Point(941, 79);
		this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label13.Name = "label13";
		this.label13.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label13.Size = new System.Drawing.Size(70, 16);
		this.label13.TabIndex = 9;
		this.label13.Text = "جهة الاصدار";
		this.label12.AutoSize = true;
		this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label12.Location = new System.Drawing.Point(940, 52);
		this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label12.Name = "label12";
		this.label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label12.Size = new System.Drawing.Size(114, 16);
		this.label12.TabIndex = 9;
		this.label12.Text = "رقم السجل التجاري";
		this.label11.AutoSize = true;
		this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label11.Location = new System.Drawing.Point(940, 24);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label11.Size = new System.Drawing.Size(72, 16);
		this.label11.TabIndex = 9;
		this.label11.Text = "نوع المنطقة";
		this.label17.AutoSize = true;
		this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label17.Location = new System.Drawing.Point(942, 21);
		this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label17.Name = "label17";
		this.label17.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label17.Size = new System.Drawing.Size(40, 16);
		this.label17.TabIndex = 9;
		this.label17.Text = "الاسم";
		this.label18.AutoSize = true;
		this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label18.Location = new System.Drawing.Point(369, 24);
		this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label18.Name = "label18";
		this.label18.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label18.Size = new System.Drawing.Size(54, 16);
		this.label18.TabIndex = 9;
		this.label18.Text = "الجنسية";
		this.groupBox5.Controls.Add(this.TxtTheNationalityOfTheTenantsRepresentative);
		this.groupBox5.Controls.Add(this.TxtTheNameOfTheTenantsRepresentative);
		this.groupBox5.Controls.Add(this.label17);
		this.groupBox5.Controls.Add(this.label18);
		this.groupBox5.Location = new System.Drawing.Point(12, 544);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(1108, 51);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.TxtTheNationalityOfTheTenantsRepresentative.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtTheNationalityOfTheTenantsRepresentative.Location = new System.Drawing.Point(7, 21);
		this.TxtTheNationalityOfTheTenantsRepresentative.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTheNationalityOfTheTenantsRepresentative.Name = "TxtTheNationalityOfTheTenantsRepresentative";
		this.TxtTheNationalityOfTheTenantsRepresentative.Size = new System.Drawing.Size(354, 23);
		this.TxtTheNationalityOfTheTenantsRepresentative.TabIndex = 11;
		this.TxtTheNationalityOfTheTenantsRepresentative.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtTheNameOfTheTenantsRepresentative.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtTheNameOfTheTenantsRepresentative.Location = new System.Drawing.Point(599, 18);
		this.TxtTheNameOfTheTenantsRepresentative.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTheNameOfTheTenantsRepresentative.Name = "TxtTheNameOfTheTenantsRepresentative";
		this.TxtTheNameOfTheTenantsRepresentative.Size = new System.Drawing.Size(335, 23);
		this.TxtTheNameOfTheTenantsRepresentative.TabIndex = 10;
		this.TxtTheNameOfTheTenantsRepresentative.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label19.AutoSize = true;
		this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label19.Location = new System.Drawing.Point(939, 105);
		this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label19.Name = "label19";
		this.label19.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label19.Size = new System.Drawing.Size(87, 16);
		this.label19.TabIndex = 9;
		this.label19.Text = "العنوان الوطني";
		this.TxtLandlordNationalAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtLandlordNationalAddress.Location = new System.Drawing.Point(9, 102);
		this.TxtLandlordNationalAddress.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtLandlordNationalAddress.Name = "TxtLandlordNationalAddress";
		this.TxtLandlordNationalAddress.Size = new System.Drawing.Size(922, 23);
		this.TxtLandlordNationalAddress.TabIndex = 12;
		this.TxtLandlordNationalAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtconditionallySuspended.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtconditionallySuspended.Location = new System.Drawing.Point(598, 141);
		this.TxtconditionallySuspended.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtconditionallySuspended.Name = "TxtconditionallySuspended";
		this.TxtconditionallySuspended.Size = new System.Drawing.Size(336, 27);
		this.TxtconditionallySuspended.TabIndex = 5;
		this.TxtconditionallySuspended.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label25.AutoSize = true;
		this.label25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label25.Location = new System.Drawing.Point(940, 144);
		this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label25.Name = "label25";
		this.label25.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label25.Size = new System.Drawing.Size(89, 19);
		this.label25.TabIndex = 9;
		this.label25.Text = "معلق بشرط";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1133, 750);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.BtnPrint);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.g2);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.g1);
		base.MaximizeBox = false;
		base.Name = "FrmEJAR";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "عقد ايجار";
		((System.ComponentModel.ISupportInitialize)this.TxtContract.Properties).EndInit();
		this.g1.ResumeLayout(false);
		this.g1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTheEndDateOfTheLeaseTerm.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTheEndDateOfTheLeaseTerm.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTheStartDateOftheLeaseTerm.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTheStartDateOftheLeaseTerm.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTheDateOfTheContract.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTheDateOfTheContract.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.g2.ResumeLayout(false);
		this.g2.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		base.ResumeLayout(false);
	}
}


