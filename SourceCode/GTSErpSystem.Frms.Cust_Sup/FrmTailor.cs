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
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Cust_Sup.Search;
using GTSErpSystem.Frms.FrmsMain;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Cust_Sup;

public class FrmTailor : XtraForm
{
	private Class_CustTailor CUS = new Class_CustTailor();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private IContainer components = null;

	private Label label1;

	private Label LblCode;

	private ButtonEdit TxtCode;

	private TextBox TxtName;

	private TextBox TxtPhone;

	private Label label10;

	private Label lblll;

	private GroupBox group;

	private GroupBox groupBox2;

	private Label label14;

	private Label label13;

	private Label label12;

	private Label label11;

	private Label label9;

	private Label label8;

	private Label label7;

	private Label label6;

	private TextBox TxtDownDressW;

	private TextBox TxtDownDressH;

	private TextBox TxtLongthW;

	private TextBox TxtLongthH;

	private TextBox TxtCuffW;

	private TextBox TxtCuffH;

	private TextBox TxtSleeveW;

	private TextBox TxtSleeveH;

	private TextBox TxtHipsW;

	private TextBox TxtHipsH;

	private TextBox TxtWaistW;

	private TextBox TxtWaistH;

	private TextBox TxtChestW;

	private TextBox TxtChestH;

	private TextBox TxtShoulderW;

	private TextBox TxtShoulderH;

	private Label label4;

	private Label label3;

	private Label label2;

	private TextBox TxtCollarW;

	private TextBox TxtCollarH;

	private GroupBox groupBox3;

	private Label label19;

	private TextBox TxtButtonNumber;

	private Label label18;

	private TextBox TxtQuantumCuff;

	private Label label17;

	private TextBox TxtQuantumPlain;

	private Label label16;

	private TextBox Txtchestpocket;

	private Label label15;

	private TextBox Txtstarlings;

	private SimpleButton BtnPrint;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private Label label20;

	private TextBox TxtNote;

	private GroupBox groupBox1;

	public FrmTailor()
	{
		InitializeComponent();
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
		TxtButtonNumber.Text = "";
		TxtChestH.Text = "";
		TxtChestW.Text = "";
		TxtCode.Text = "";
		TxtCollarH.Text = "";
		TxtCollarW.Text = "";
		TxtCuffH.Text = "";
		TxtCuffW.Text = "";
		TxtDownDressH.Text = "";
		TxtDownDressW.Text = "";
		TxtHipsH.Text = "";
		TxtHipsW.Text = "";
		TxtLongthH.Text = "";
		TxtLongthW.Text = "";
		TxtName.Text = "";
		TxtNote.Text = "";
		TxtPhone.Text = "";
		TxtQuantumCuff.Text = "";
		TxtQuantumPlain.Text = "";
		TxtShoulderH.Text = "";
		TxtShoulderW.Text = "";
		TxtSleeveH.Text = "";
		TxtSleeveW.Text = "";
		Txtstarlings.Text = "";
		TxtWaistH.Text = "";
		TxtWaistW.Text = "";
		LblCode.Text = "";
		Txtchestpocket.Text = "";
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 23))
		{
			int num = 0;
			if (LblCode.Text != "" || LblCode.Text != "0")
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = CUS.InsertUpdate(Convert.ToInt32(LblCode.Text), TxtCollarH.Text, TxtCollarW.Text, TxtShoulderH.Text, TxtShoulderW.Text, TxtChestH.Text, TxtChestW.Text, TxtWaistH.Text, TxtWaistW.Text, TxtHipsH.Text, TxtHipsW.Text, TxtSleeveH.Text, TxtSleeveW.Text, TxtCuffH.Text, TxtCuffW.Text, TxtLongthH.Text, TxtLongthW.Text, TxtDownDressH.Text, TxtDownDressW.Text, Txtstarlings.Text, Txtchestpocket.Text, TxtQuantumPlain.Text, TxtQuantumCuff.Text, TxtButtonNumber.Text, TxtNote.Text);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات العملاء  ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات العملاء بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (LblCode.Text == "" || LblCode.Text == "0")
			{
				MessageBox.Show(" عفوا، ادخل اسم العميل ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 23))
		{
			int num = 0;
			if (TxtName.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtName.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = CUS.Delete(Convert.ToInt32(LblCode.Text));
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات العملاء ( " + TxtName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						Clear();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtName.Text + " ) لوجود عمليات جارية بهذا العملاء", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر اسم العملاء ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 23))
		{
			if (TxtCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Account_CusTailor"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Select_Account_CusTailor", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@CustCode", Convert.ToInt32(LblCode.Text));
				sqlDataAdapter.Fill(dataSetALL.Account_CusTailor);
				sqlConnection.Close();
				if (dataSetALL.Tables["Account_CusTailor"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					stiReport.Load(Application.StartupPath + "\\Reports\\Print_CustTailor.mrt");
					stiReport.RegData(dataSetALL.Account_CusTailor);
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
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustID > 0)
			{
				SearchCust(frmSearchCustomer.CustID);
			}
			else
			{
				TxtName.Focus();
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
		Select_Account_CusTailor_Result select_Account_CusTailor_Result = CUS.GetCustCode(CustCode).FirstOrDefault();
		if (select_Account_CusTailor_Result != null)
		{
			LblCode.Text = select_Account_CusTailor_Result.CustCode.ToString();
			TxtCode.Text = select_Account_CusTailor_Result.CustCode.ToString();
			TxtName.Text = select_Account_CusTailor_Result.CustName;
			TxtPhone.Text = select_Account_CusTailor_Result.Phone;
			TxtCollarH.Text = select_Account_CusTailor_Result.CollarH;
			TxtCollarW.Text = select_Account_CusTailor_Result.CollarW;
			TxtShoulderH.Text = select_Account_CusTailor_Result.ShoulderH;
			TxtShoulderW.Text = select_Account_CusTailor_Result.ShoulderW;
			TxtChestH.Text = select_Account_CusTailor_Result.ChestH;
			TxtChestW.Text = select_Account_CusTailor_Result.ChestW;
			TxtWaistH.Text = select_Account_CusTailor_Result.WaistH;
			TxtWaistW.Text = select_Account_CusTailor_Result.WaistW;
			TxtHipsH.Text = select_Account_CusTailor_Result.HipsH;
			TxtHipsW.Text = select_Account_CusTailor_Result.HipsW;
			TxtSleeveH.Text = select_Account_CusTailor_Result.SleeveH;
			TxtSleeveW.Text = select_Account_CusTailor_Result.SleeveW;
			TxtCuffH.Text = select_Account_CusTailor_Result.CuffH;
			TxtCuffW.Text = select_Account_CusTailor_Result.CuffW;
			TxtLongthH.Text = select_Account_CusTailor_Result.LongthH;
			TxtLongthW.Text = select_Account_CusTailor_Result.LongthW;
			TxtDownDressH.Text = select_Account_CusTailor_Result.DownDressH;
			TxtDownDressW.Text = select_Account_CusTailor_Result.DownDressW;
			Txtstarlings.Text = select_Account_CusTailor_Result.starlings;
			Txtchestpocket.Text = select_Account_CusTailor_Result.chestpocket;
			TxtQuantumPlain.Text = select_Account_CusTailor_Result.QuantumPlain;
			TxtQuantumCuff.Text = select_Account_CusTailor_Result.QuantumCuff;
			TxtButtonNumber.Text = select_Account_CusTailor_Result.ButtonNumber;
			TxtNote.Text = select_Account_CusTailor_Result.Note;
		}
		else
		{
			Clear();
			LblCode.Text = select_Account_CusTailor_Result.CustCode.ToString();
			TxtCode.Text = select_Account_CusTailor_Result.CustCode.ToString();
			TxtName.Text = select_Account_CusTailor_Result.CustName;
			TxtPhone.Text = select_Account_CusTailor_Result.Phone;
		}
	}

	private void TxtCollarH_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtShoulderH.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtShoulderH_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtChestH.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtChestH_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtWaistH.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtWaistH_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtHipsH.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtHipsH_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtSleeveH.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtSleeveH_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtCuffH.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtCuffH_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtLongthH.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtLongthH_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtDownDressH.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtDownDressH_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtCollarW.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtCollarW_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtShoulderW.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtShoulderW_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtChestW.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtChestW_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtWaistW.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtWaistW_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtHipsW.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtHipsW_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtSleeveW.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtSleeveW_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtCuffW.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtCuffW_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtLongthW.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtLongthW_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtDownDressW.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtDownDressW_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			Txtstarlings.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void Txtstarlings_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			Txtchestpocket.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void Txtchestpocket_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtQuantumPlain.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtQuantumPlain_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtQuantumCuff.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtQuantumCuff_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtButtonNumber.Focus();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TxtButtonNumber_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtNote.Focus();
			e.Handled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Cust_Sup.FrmTailor));
		this.label1 = new System.Windows.Forms.Label();
		this.LblCode = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtName = new System.Windows.Forms.TextBox();
		this.TxtPhone = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.lblll = new System.Windows.Forms.Label();
		this.group = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.TxtDownDressW = new System.Windows.Forms.TextBox();
		this.TxtDownDressH = new System.Windows.Forms.TextBox();
		this.TxtLongthW = new System.Windows.Forms.TextBox();
		this.TxtLongthH = new System.Windows.Forms.TextBox();
		this.TxtCuffW = new System.Windows.Forms.TextBox();
		this.TxtCuffH = new System.Windows.Forms.TextBox();
		this.TxtSleeveW = new System.Windows.Forms.TextBox();
		this.TxtSleeveH = new System.Windows.Forms.TextBox();
		this.TxtHipsW = new System.Windows.Forms.TextBox();
		this.TxtHipsH = new System.Windows.Forms.TextBox();
		this.TxtWaistW = new System.Windows.Forms.TextBox();
		this.TxtWaistH = new System.Windows.Forms.TextBox();
		this.TxtChestW = new System.Windows.Forms.TextBox();
		this.TxtChestH = new System.Windows.Forms.TextBox();
		this.TxtShoulderW = new System.Windows.Forms.TextBox();
		this.TxtShoulderH = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtCollarW = new System.Windows.Forms.TextBox();
		this.TxtCollarH = new System.Windows.Forms.TextBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtButtonNumber = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.TxtQuantumCuff = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtQuantumPlain = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.Txtchestpocket = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.Txtstarlings = new System.Windows.Forms.TextBox();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.label20 = new System.Windows.Forms.Label();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		this.group.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label1.Location = new System.Drawing.Point(1008, 37);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label1.Size = new System.Drawing.Size(81, 19);
		this.label1.TabIndex = 0;
		this.label1.Text = "كود العميل";
		this.LblCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblCode.Location = new System.Drawing.Point(706, 32);
		this.LblCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblCode.Name = "LblCode";
		this.LblCode.Size = new System.Drawing.Size(22, 23);
		this.LblCode.TabIndex = 0;
		this.LblCode.Text = "0";
		this.LblCode.Visible = false;
		this.TxtCode.Location = new System.Drawing.Point(734, 29);
		this.TxtCode.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCode.Properties.Appearance.Options.UseFont = true;
		this.TxtCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCode.Properties.ReadOnly = true;
		this.TxtCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.TxtCode.Size = new System.Drawing.Size(265, 26);
		this.TxtCode.TabIndex = 111;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.TxtName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtName.Location = new System.Drawing.Point(356, 61);
		this.TxtName.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtName.Name = "TxtName";
		this.TxtName.Size = new System.Drawing.Size(642, 27);
		this.TxtName.TabIndex = 5;
		this.TxtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPhone.Location = new System.Drawing.Point(31, 61);
		this.TxtPhone.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Size = new System.Drawing.Size(241, 27);
		this.TxtPhone.TabIndex = 6;
		this.TxtPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label10.AutoSize = true;
		this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label10.Location = new System.Drawing.Point(282, 65);
		this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label10.Name = "label10";
		this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label10.Size = new System.Drawing.Size(50, 19);
		this.label10.TabIndex = 178;
		this.label10.Text = "الجوال";
		this.lblll.AutoSize = true;
		this.lblll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblll.Location = new System.Drawing.Point(1008, 67);
		this.lblll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblll.Name = "lblll";
		this.lblll.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblll.Size = new System.Drawing.Size(90, 19);
		this.lblll.TabIndex = 9;
		this.lblll.Text = "إسم العميل";
		this.group.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.group.Controls.Add(this.lblll);
		this.group.Controls.Add(this.label10);
		this.group.Controls.Add(this.TxtPhone);
		this.group.Controls.Add(this.TxtName);
		this.group.Controls.Add(this.TxtCode);
		this.group.Controls.Add(this.LblCode);
		this.group.Controls.Add(this.label1);
		this.group.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.group.Location = new System.Drawing.Point(13, 11);
		this.group.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.group.Name = "group";
		this.group.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.group.Size = new System.Drawing.Size(1107, 113);
		this.group.TabIndex = 1;
		this.group.TabStop = false;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.TxtDownDressW);
		this.groupBox2.Controls.Add(this.TxtDownDressH);
		this.groupBox2.Controls.Add(this.TxtLongthW);
		this.groupBox2.Controls.Add(this.TxtLongthH);
		this.groupBox2.Controls.Add(this.TxtCuffW);
		this.groupBox2.Controls.Add(this.TxtCuffH);
		this.groupBox2.Controls.Add(this.TxtSleeveW);
		this.groupBox2.Controls.Add(this.TxtSleeveH);
		this.groupBox2.Controls.Add(this.TxtHipsW);
		this.groupBox2.Controls.Add(this.TxtHipsH);
		this.groupBox2.Controls.Add(this.TxtWaistW);
		this.groupBox2.Controls.Add(this.TxtWaistH);
		this.groupBox2.Controls.Add(this.TxtChestW);
		this.groupBox2.Controls.Add(this.TxtChestH);
		this.groupBox2.Controls.Add(this.TxtShoulderW);
		this.groupBox2.Controls.Add(this.TxtShoulderH);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.TxtCollarW);
		this.groupBox2.Controls.Add(this.TxtCollarH);
		this.groupBox2.Location = new System.Drawing.Point(12, 129);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1108, 157);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.label14.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label14.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label14.Location = new System.Drawing.Point(35, 19);
		this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label14.Name = "label14";
		this.label14.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label14.Size = new System.Drawing.Size(100, 52);
		this.label14.TabIndex = 211;
		this.label14.Text = "اسفل \r\nالثوب";
		this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label13.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label13.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label13.Location = new System.Drawing.Point(143, 19);
		this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label13.Name = "label13";
		this.label13.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label13.Size = new System.Drawing.Size(100, 52);
		this.label13.TabIndex = 210;
		this.label13.Text = "الطول\r\nLongth";
		this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label12.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label12.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label12.Location = new System.Drawing.Point(251, 19);
		this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label12.Name = "label12";
		this.label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label12.Size = new System.Drawing.Size(100, 52);
		this.label12.TabIndex = 209;
		this.label12.Text = "طول الكم كبك";
		this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label11.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label11.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label11.Location = new System.Drawing.Point(359, 20);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label11.Size = new System.Drawing.Size(100, 52);
		this.label11.TabIndex = 208;
		this.label11.Text = "طول الكم الساده\r\n";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label9.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label9.Location = new System.Drawing.Point(467, 19);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label9.Size = new System.Drawing.Size(100, 52);
		this.label9.TabIndex = 207;
		this.label9.Text = "اسفل الخصر\r\nHips";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label8.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(575, 19);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label8.Size = new System.Drawing.Size(100, 52);
		this.label8.TabIndex = 206;
		this.label8.Text = "الخصر\r\nWaist";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label7.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label7.Location = new System.Drawing.Point(683, 19);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label7.Size = new System.Drawing.Size(100, 52);
		this.label7.TabIndex = 205;
		this.label7.Text = "الصدر\r\nChest";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(791, 19);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label6.Size = new System.Drawing.Size(100, 52);
		this.label6.TabIndex = 204;
		this.label6.Text = "الكتف\r\nShoulder";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.TxtDownDressW.Location = new System.Drawing.Point(33, 107);
		this.TxtDownDressW.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDownDressW.Name = "TxtDownDressW";
		this.TxtDownDressW.Size = new System.Drawing.Size(100, 23);
		this.TxtDownDressW.TabIndex = 203;
		this.TxtDownDressW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtDownDressW.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtDownDressW_KeyDown);
		this.TxtDownDressH.Location = new System.Drawing.Point(33, 77);
		this.TxtDownDressH.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDownDressH.Name = "TxtDownDressH";
		this.TxtDownDressH.Size = new System.Drawing.Size(100, 23);
		this.TxtDownDressH.TabIndex = 202;
		this.TxtDownDressH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtDownDressH.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtDownDressH_KeyDown);
		this.TxtLongthW.Location = new System.Drawing.Point(141, 107);
		this.TxtLongthW.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtLongthW.Name = "TxtLongthW";
		this.TxtLongthW.Size = new System.Drawing.Size(100, 23);
		this.TxtLongthW.TabIndex = 200;
		this.TxtLongthW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtLongthW.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtLongthW_KeyDown);
		this.TxtLongthH.Location = new System.Drawing.Point(140, 77);
		this.TxtLongthH.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtLongthH.Name = "TxtLongthH";
		this.TxtLongthH.Size = new System.Drawing.Size(101, 23);
		this.TxtLongthH.TabIndex = 199;
		this.TxtLongthH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtLongthH.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtLongthH_KeyDown);
		this.TxtCuffW.Location = new System.Drawing.Point(249, 107);
		this.TxtCuffW.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCuffW.Name = "TxtCuffW";
		this.TxtCuffW.Size = new System.Drawing.Size(100, 23);
		this.TxtCuffW.TabIndex = 197;
		this.TxtCuffW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtCuffW.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtCuffW_KeyDown);
		this.TxtCuffH.Location = new System.Drawing.Point(249, 77);
		this.TxtCuffH.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCuffH.Name = "TxtCuffH";
		this.TxtCuffH.Size = new System.Drawing.Size(100, 23);
		this.TxtCuffH.TabIndex = 196;
		this.TxtCuffH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtCuffH.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtCuffH_KeyDown);
		this.TxtSleeveW.Location = new System.Drawing.Point(359, 107);
		this.TxtSleeveW.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtSleeveW.Name = "TxtSleeveW";
		this.TxtSleeveW.Size = new System.Drawing.Size(100, 23);
		this.TxtSleeveW.TabIndex = 194;
		this.TxtSleeveW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtSleeveW.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtSleeveW_KeyDown);
		this.TxtSleeveH.Location = new System.Drawing.Point(359, 77);
		this.TxtSleeveH.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtSleeveH.Name = "TxtSleeveH";
		this.TxtSleeveH.Size = new System.Drawing.Size(100, 23);
		this.TxtSleeveH.TabIndex = 193;
		this.TxtSleeveH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtSleeveH.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtSleeveH_KeyDown);
		this.TxtHipsW.Location = new System.Drawing.Point(467, 107);
		this.TxtHipsW.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtHipsW.Name = "TxtHipsW";
		this.TxtHipsW.Size = new System.Drawing.Size(100, 23);
		this.TxtHipsW.TabIndex = 191;
		this.TxtHipsW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtHipsW.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtHipsW_KeyDown);
		this.TxtHipsH.Location = new System.Drawing.Point(467, 77);
		this.TxtHipsH.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtHipsH.Name = "TxtHipsH";
		this.TxtHipsH.Size = new System.Drawing.Size(100, 23);
		this.TxtHipsH.TabIndex = 190;
		this.TxtHipsH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtHipsH.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtHipsH_KeyDown);
		this.TxtWaistW.Location = new System.Drawing.Point(575, 107);
		this.TxtWaistW.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtWaistW.Name = "TxtWaistW";
		this.TxtWaistW.Size = new System.Drawing.Size(100, 23);
		this.TxtWaistW.TabIndex = 188;
		this.TxtWaistW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtWaistW.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtWaistW_KeyDown);
		this.TxtWaistH.Location = new System.Drawing.Point(574, 77);
		this.TxtWaistH.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtWaistH.Name = "TxtWaistH";
		this.TxtWaistH.Size = new System.Drawing.Size(101, 23);
		this.TxtWaistH.TabIndex = 187;
		this.TxtWaistH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtWaistH.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtWaistH_KeyDown);
		this.TxtChestW.Location = new System.Drawing.Point(683, 107);
		this.TxtChestW.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtChestW.Name = "TxtChestW";
		this.TxtChestW.Size = new System.Drawing.Size(100, 23);
		this.TxtChestW.TabIndex = 185;
		this.TxtChestW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtChestW.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtChestW_KeyDown);
		this.TxtChestH.Location = new System.Drawing.Point(683, 77);
		this.TxtChestH.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtChestH.Name = "TxtChestH";
		this.TxtChestH.Size = new System.Drawing.Size(100, 23);
		this.TxtChestH.TabIndex = 184;
		this.TxtChestH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtChestH.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtChestH_KeyDown);
		this.TxtShoulderW.Location = new System.Drawing.Point(791, 107);
		this.TxtShoulderW.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtShoulderW.Name = "TxtShoulderW";
		this.TxtShoulderW.Size = new System.Drawing.Size(100, 23);
		this.TxtShoulderW.TabIndex = 182;
		this.TxtShoulderW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtShoulderW.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtShoulderW_KeyDown);
		this.TxtShoulderH.Location = new System.Drawing.Point(791, 77);
		this.TxtShoulderH.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtShoulderH.Name = "TxtShoulderH";
		this.TxtShoulderH.Size = new System.Drawing.Size(100, 23);
		this.TxtShoulderH.TabIndex = 181;
		this.TxtShoulderH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtShoulderH.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtShoulderH_KeyDown);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label4.Location = new System.Drawing.Point(1005, 105);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label4.Size = new System.Drawing.Size(55, 18);
		this.label4.TabIndex = 180;
		this.label4.Text = "العرض";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(1007, 78);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label3.Size = new System.Drawing.Size(50, 18);
		this.label3.TabIndex = 179;
		this.label3.Text = "الطول";
		this.label2.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(899, 19);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label2.Size = new System.Drawing.Size(100, 52);
		this.label2.TabIndex = 179;
		this.label2.Text = "الياقة\r\nCollar";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.TxtCollarW.Location = new System.Drawing.Point(899, 107);
		this.TxtCollarW.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCollarW.Name = "TxtCollarW";
		this.TxtCollarW.Size = new System.Drawing.Size(100, 23);
		this.TxtCollarW.TabIndex = 8;
		this.TxtCollarW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtCollarW.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtCollarW_KeyDown);
		this.TxtCollarH.Location = new System.Drawing.Point(899, 77);
		this.TxtCollarH.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCollarH.Name = "TxtCollarH";
		this.TxtCollarH.Size = new System.Drawing.Size(100, 23);
		this.TxtCollarH.TabIndex = 7;
		this.TxtCollarH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtCollarH.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtCollarH_KeyDown);
		this.groupBox3.Controls.Add(this.label19);
		this.groupBox3.Controls.Add(this.TxtButtonNumber);
		this.groupBox3.Controls.Add(this.label18);
		this.groupBox3.Controls.Add(this.TxtQuantumCuff);
		this.groupBox3.Controls.Add(this.label17);
		this.groupBox3.Controls.Add(this.TxtQuantumPlain);
		this.groupBox3.Controls.Add(this.label16);
		this.groupBox3.Controls.Add(this.Txtchestpocket);
		this.groupBox3.Controls.Add(this.label15);
		this.groupBox3.Controls.Add(this.Txtstarlings);
		this.groupBox3.Location = new System.Drawing.Point(12, 292);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1108, 117);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.label19.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label19.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label19.Location = new System.Drawing.Point(33, 19);
		this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label19.Name = "label19";
		this.label19.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label19.Size = new System.Drawing.Size(100, 52);
		this.label19.TabIndex = 213;
		this.label19.Text = "رقم الزرار";
		this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.TxtButtonNumber.Location = new System.Drawing.Point(31, 73);
		this.TxtButtonNumber.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtButtonNumber.Name = "TxtButtonNumber";
		this.TxtButtonNumber.Size = new System.Drawing.Size(102, 23);
		this.TxtButtonNumber.TabIndex = 212;
		this.TxtButtonNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtButtonNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtButtonNumber_KeyDown);
		this.label18.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label18.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label18.Location = new System.Drawing.Point(140, 19);
		this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label18.Name = "label18";
		this.label18.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label18.Size = new System.Drawing.Size(209, 52);
		this.label18.TabIndex = 219;
		this.label18.Text = "الكم كبك";
		this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.TxtQuantumCuff.Location = new System.Drawing.Point(141, 73);
		this.TxtQuantumCuff.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtQuantumCuff.Name = "TxtQuantumCuff";
		this.TxtQuantumCuff.Size = new System.Drawing.Size(208, 23);
		this.TxtQuantumCuff.TabIndex = 218;
		this.TxtQuantumCuff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtQuantumCuff.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtQuantumCuff_KeyDown);
		this.label17.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label17.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label17.Location = new System.Drawing.Point(358, 19);
		this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label17.Name = "label17";
		this.label17.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label17.Size = new System.Drawing.Size(209, 52);
		this.label17.TabIndex = 217;
		this.label17.Text = "الكم سادة";
		this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.TxtQuantumPlain.Location = new System.Drawing.Point(358, 73);
		this.TxtQuantumPlain.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtQuantumPlain.Name = "TxtQuantumPlain";
		this.TxtQuantumPlain.Size = new System.Drawing.Size(209, 23);
		this.TxtQuantumPlain.TabIndex = 216;
		this.TxtQuantumPlain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtQuantumPlain.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtQuantumPlain_KeyDown);
		this.label16.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label16.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label16.Location = new System.Drawing.Point(574, 19);
		this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label16.Name = "label16";
		this.label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label16.Size = new System.Drawing.Size(209, 52);
		this.label16.TabIndex = 215;
		this.label16.Text = "جيب الصدر";
		this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.Txtchestpocket.Location = new System.Drawing.Point(574, 73);
		this.Txtchestpocket.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Txtchestpocket.Name = "Txtchestpocket";
		this.Txtchestpocket.Size = new System.Drawing.Size(209, 23);
		this.Txtchestpocket.TabIndex = 214;
		this.Txtchestpocket.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Txtchestpocket.KeyDown += new System.Windows.Forms.KeyEventHandler(Txtchestpocket_KeyDown);
		this.label15.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label15.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label15.Location = new System.Drawing.Point(791, 19);
		this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label15.Name = "label15";
		this.label15.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label15.Size = new System.Drawing.Size(209, 52);
		this.label15.TabIndex = 213;
		this.label15.Text = "الزرزور";
		this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.Txtstarlings.Location = new System.Drawing.Point(791, 73);
		this.Txtstarlings.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Txtstarlings.Name = "Txtstarlings";
		this.Txtstarlings.Size = new System.Drawing.Size(209, 23);
		this.Txtstarlings.TabIndex = 212;
		this.Txtstarlings.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Txtstarlings.KeyDown += new System.Windows.Forms.KeyEventHandler(Txtstarlings_KeyDown);
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(694, 548);
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
		this.BtnSave.Location = new System.Drawing.Point(368, 548);
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
		this.BtnNew.Location = new System.Drawing.Point(205, 548);
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
		this.BtnDelete.Location = new System.Drawing.Point(531, 548);
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
		this.BtnExit.Location = new System.Drawing.Point(857, 548);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(152, 39);
		this.BtnExit.TabIndex = 10;
		this.BtnExit.Text = "خروج";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 9.25f);
		this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label20.Location = new System.Drawing.Point(1013, 41);
		this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(68, 19);
		this.label20.TabIndex = 182;
		this.label20.Text = "ملاحظات";
		this.TxtNote.Location = new System.Drawing.Point(32, 21);
		this.TxtNote.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtNote.Multiline = true;
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtNote.Size = new System.Drawing.Size(968, 85);
		this.TxtNote.TabIndex = 181;
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Location = new System.Drawing.Point(12, 415);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1109, 121);
		this.groupBox1.TabIndex = 183;
		this.groupBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1133, 600);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.BtnPrint);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.group);
		base.MaximizeBox = false;
		base.Name = "FrmTailor";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "مقاسات الثوب للعميل";
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		this.group.ResumeLayout(false);
		this.group.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}


