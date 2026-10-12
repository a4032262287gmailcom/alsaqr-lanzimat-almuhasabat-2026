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
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Cust_Sup.Search;
using GTSErpSystem.Frms.FrmsMain;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Cust_Sup;

public class FrmReservation : XtraForm
{
	private Class_CustTailor CUS = new Class_CustTailor();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private IContainer components = null;

	private Label label1;

	private Label LblCodeCustomer;

	private ButtonEdit TxtCodeCustomer;

	private TextBox TxtName;

	private TextBox TxtPhone;

	private Label label10;

	private Label lblll;

	private GroupBox group;

	private GroupBox groupBox2;

	private SimpleButton BtnPrint;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	private Label label20;

	private TextBox TxtNote;

	private GroupBox groupBox1;

	private Label label27;

	private DateEdit TxtDate;

	private Label label5;

	private DateEdit TxtReturndate;

	private Label label4;

	private DateEdit TxtEventdate;

	private Label label3;

	private DateEdit TxtReceiveddate;

	private Label label2;

	private DateEdit TxtRehearsaldate;

	private NumericEditBox TxtNet;

	private Label label6;

	private ToggleSwitch toggle;

	private GroupBox groupBox3;

	private ButtonEdit TxtCode;

	private Label label18;

	private ButtonEdit BtnOrderSearch;

	private ButtonEdit BtnCustomerSearch;

	private SimpleButton BtnNewCustomer;

	private TextBox TxtReceived;

	private TextBox TxtEvent;

	private TextBox TxtReturn;

	private TextBox TxtRehearsal;

	private Label LblCode;

	private SimpleButton BtnEdit;

	private Label label37;

	private TextEdit TxtCharge;

	public FrmReservation()
	{
		InitializeComponent();
		Clear();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
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

	private void BtnEdit_Click_1(object sender, EventArgs e)
	{
		Edit();
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

	private void BtnCustomerSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && !string.IsNullOrWhiteSpace(BtnCustomerSearch.Text))
		{
			SearchCust(Convert.ToInt32(BtnCustomerSearch.Text));
		}
	}

	private void BtnOrderSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SearchOrder(Convert.ToInt32(BtnOrderSearch.Text));
		}
	}

	private void TxtCodeRever_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		FrmSearchReservation frmSearchReservation = new FrmSearchReservation();
		frmSearchReservation.ShowDialog();
		if (frmSearchReservation.CustCode > 0)
		{
			SearchOrder(frmSearchReservation.CustCode);
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

	private void Clear()
	{
		TxtCode.Text = "";
		BtnOrderSearch.Text = "";
		TxtDate.DateTime = DateTime.Now;
		toggle.IsOn = true;
		LblCodeCustomer.Text = "";
		TxtCodeCustomer.Text = "";
		BtnCustomerSearch.Text = "";
		TxtName.Text = "";
		TxtPhone.Text = "";
		TxtRehearsaldate.DateTime = DateTime.Now;
		TxtReceiveddate.DateTime = DateTime.Now;
		TxtEventdate.DateTime = DateTime.Now;
		TxtReturndate.DateTime = DateTime.Now;
		TxtNet.Value = 0;
		TxtNote.Text = "";
		LblCodeCustomer.Text = "0";
		LblCode.Text = "0";
		CalcDate();
		BtnDelete.Enabled = false;
		BtnEdit.Enabled = false;
		BtnSave.Enabled = true;
	}

	private void CalcDate()
	{
		TxtRehearsal.Text = ((TxtRehearsaldate.DateTime - DateTime.Now).Days + 1).ToString();
		TxtReceived.Text = ((TxtReceiveddate.DateTime - DateTime.Now).Days + 1).ToString();
		TxtEvent.Text = ((TxtEventdate.DateTime - DateTime.Now).Days + 1).ToString();
		TxtReturn.Text = ((TxtReturndate.DateTime - DateTime.Now).Days + 1).ToString();
	}

	private void SwitchToggle()
	{
		if (toggle.IsOn)
		{
			TxtReturndate.Visible = true;
			TxtReturn.Visible = true;
			label5.Visible = true;
			TxtNet.Visible = true;
			label6.Visible = true;
		}
		else
		{
			TxtReturndate.DateTime = DateTime.Now;
			TxtReturndate.Visible = false;
			TxtReturn.Visible = false;
			label5.Visible = false;
			TxtNet.Visible = false;
			label6.Visible = false;
			TxtNet.Value = 0;
		}
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 23))
		{
			int num = 0;
			if (Convert.ToInt32(LblCodeCustomer.Text) > 0)
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					using (GTSdbEntities gTSdbEntities = new GTSdbEntities())
					{
						int num2 = Convert.ToInt32(gTSdbEntities.Order_Reservation.Select((Order_Reservation x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
						Order_Reservation order_Reservation = new Order_Reservation();
						order_Reservation.ID = num2;
						order_Reservation.CustID = Convert.ToInt32(LblCodeCustomer.Text);
						order_Reservation.DateReserv = TxtDate.DateTime;
						order_Reservation.IsRent = toggle.IsOn;
						order_Reservation.Rehearsaldate = TxtRehearsaldate.DateTime;
						order_Reservation.Receiveddate = TxtReceiveddate.DateTime;
						order_Reservation.Eventdate = TxtEventdate.DateTime;
						order_Reservation.Returndate = TxtReturndate.DateTime;
						order_Reservation.Net = Convert.ToDecimal(TxtNet.Text);
						order_Reservation.Note = TxtNote.Text;
						gTSdbEntities.Order_Reservation.Add(order_Reservation);
						num = gTSdbEntities.SaveChanges();
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						Charge(Convert.ToInt32(LblCodeCustomer.Text));
						TxtCode.Text = Convert.ToString(num2);
						LblCode.Text = Convert.ToString(num2);
					}
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات الحجز  ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات الحجز بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (LblCodeCustomer.Text == "" || LblCodeCustomer.Text == "0")
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

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 23))
		{
			int num = 0;
			int Code = Convert.ToInt32(TxtCode.Text);
			if (Convert.ToInt32(LblCode.Text) > 0 && Convert.ToInt32(LblCodeCustomer.Text) > 0)
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					using (GTSdbEntities gTSdbEntities = new GTSdbEntities())
					{
						Order_Reservation order_Reservation = gTSdbEntities.Order_Reservation.Where((Order_Reservation x) => x.ID == Code).FirstOrDefault();
						if (order_Reservation != null)
						{
							order_Reservation.CustID = Convert.ToInt32(LblCodeCustomer.Text);
							order_Reservation.DateReserv = TxtDate.DateTime;
							order_Reservation.IsRent = toggle.IsOn;
							order_Reservation.Rehearsaldate = TxtRehearsaldate.DateTime;
							order_Reservation.Receiveddate = TxtReceiveddate.DateTime;
							order_Reservation.Eventdate = TxtEventdate.DateTime;
							order_Reservation.Returndate = TxtReturndate.DateTime;
							order_Reservation.Net = Convert.ToDecimal(TxtNet.Text);
							order_Reservation.Note = TxtNote.Text;
							num = gTSdbEntities.SaveChanges();
							BtnSave.Enabled = false;
							BtnEdit.Enabled = true;
							BtnDelete.Enabled = true;
							Charge(Convert.ToInt32(LblCodeCustomer.Text));
							num = 1;
						}
					}
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات الحجز  ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات الحجز بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (LblCodeCustomer.Text == "" || LblCodeCustomer.Text == "0")
			{
				MessageBox.Show(" عفوا، اختر الحجز ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
			int Code = Convert.ToInt32(TxtCode.Text);
			if (Convert.ToInt32(LblCode.Text) > 0 && Convert.ToInt32(LblCodeCustomer.Text) > 0)
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtName.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					using (GTSdbEntities gTSdbEntities = new GTSdbEntities())
					{
						Order_Reservation order_Reservation = gTSdbEntities.Order_Reservation.Where((Order_Reservation x) => x.ID == Code).FirstOrDefault();
						if (order_Reservation != null)
						{
							gTSdbEntities.Order_Reservation.Attach(order_Reservation);
							gTSdbEntities.Order_Reservation.Remove(order_Reservation);
							num = gTSdbEntities.SaveChanges();
						}
					}
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات الحجز ( " + TxtName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						Clear();
						BtnDelete.Enabled = false;
						BtnEdit.Enabled = false;
						BtnSave.Enabled = true;
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtName.Text + " ) لوجود عمليات جارية بهذا الحجز", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				}
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر الحجز ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
			if (TxtCodeCustomer.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["Account_CusTailor"].Clear();
				SqlCommand sqlCommand = new SqlCommand("Select_Account_CusTailor", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@CustCode", Convert.ToInt32(LblCodeCustomer.Text));
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

	private void SearchCust(int CustCode)
	{
		Select_Account_CusTailor_Result select_Account_CusTailor_Result = CUS.GetCustCode(CustCode).FirstOrDefault();
		if (select_Account_CusTailor_Result != null)
		{
			LblCodeCustomer.Text = select_Account_CusTailor_Result.CustCode.ToString();
			TxtCodeCustomer.Text = select_Account_CusTailor_Result.CustCode.ToString();
			TxtName.Text = select_Account_CusTailor_Result.CustName;
			TxtPhone.Text = select_Account_CusTailor_Result.Phone;
			TxtRehearsal.Text = select_Account_CusTailor_Result.CollarH;
			TxtNote.Text = select_Account_CusTailor_Result.Note;
			Charge(CustCode);
		}
		else
		{
			Clear();
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
				TxtCharge.Text = Math.Round(item.Value, 2).ToString();
			}
		}
		catch (Exception)
		{
		}
	}

	private void SearchOrder(int OrdID)
	{
		if (OrdID <= 0)
		{
			return;
		}
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		try
		{
			Select_Order_Reservation_Result select_Order_Reservation_Result = gTSdbEntities.Select_Order_Reservation(OrdID).FirstOrDefault();
			if (select_Order_Reservation_Result != null)
			{
				TxtCode.Text = OrdID.ToString();
				LblCode.Text = OrdID.ToString();
				TxtDate.DateTime = Convert.ToDateTime(select_Order_Reservation_Result.DateReserv);
				toggle.IsOn = Convert.ToBoolean(select_Order_Reservation_Result.IsRent);
				LblCodeCustomer.Text = select_Order_Reservation_Result.CustID.ToString();
				TxtCodeCustomer.Text = select_Order_Reservation_Result.CustID.ToString();
				TxtName.Text = select_Order_Reservation_Result.CustName;
				TxtPhone.Text = select_Order_Reservation_Result.Phone;
				TxtRehearsaldate.DateTime = Convert.ToDateTime(select_Order_Reservation_Result.Rehearsaldate);
				TxtReceiveddate.DateTime = Convert.ToDateTime(select_Order_Reservation_Result.Receiveddate);
				TxtEventdate.DateTime = Convert.ToDateTime(select_Order_Reservation_Result.Eventdate);
				TxtReturndate.DateTime = Convert.ToDateTime(select_Order_Reservation_Result.Returndate);
				TxtNet.Value = select_Order_Reservation_Result.Net;
				TxtNote.Text = select_Order_Reservation_Result.Note;
				CalcDate();
				SwitchToggle();
				Charge(Convert.ToInt32(select_Order_Reservation_Result.CustID));
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
			}
			else
			{
				Clear();
			}
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void toggle_Toggled(object sender, EventArgs e)
	{
		SwitchToggle();
	}

	private void TxtRehearsaldate_EditValueChanged(object sender, EventArgs e)
	{
		CalcDate();
	}

	private void BtnNewCustomer_Click(object sender, EventArgs e)
	{
		new FrmCustomer().Show();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Cust_Sup.FrmReservation));
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		this.label1 = new System.Windows.Forms.Label();
		this.LblCodeCustomer = new System.Windows.Forms.Label();
		this.TxtCodeCustomer = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtName = new System.Windows.Forms.TextBox();
		this.TxtPhone = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.lblll = new System.Windows.Forms.Label();
		this.group = new System.Windows.Forms.GroupBox();
		this.label37 = new System.Windows.Forms.Label();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.BtnCustomerSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnNewCustomer = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.TxtReceived = new System.Windows.Forms.TextBox();
		this.TxtEvent = new System.Windows.Forms.TextBox();
		this.TxtReturn = new System.Windows.Forms.TextBox();
		this.TxtRehearsal = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtReturndate = new DevExpress.XtraEditors.DateEdit();
		this.label4 = new System.Windows.Forms.Label();
		this.TxtEventdate = new DevExpress.XtraEditors.DateEdit();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtReceiveddate = new DevExpress.XtraEditors.DateEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtRehearsaldate = new DevExpress.XtraEditors.DateEdit();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.label20 = new System.Windows.Forms.Label();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label27 = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.toggle = new DevExpress.XtraEditors.ToggleSwitch();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.LblCode = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label18 = new System.Windows.Forms.Label();
		this.BtnOrderSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.TxtCodeCustomer.Properties).BeginInit();
		this.group.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtReturndate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtReturndate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtEventdate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtEventdate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtReceiveddate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtReceiveddate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRehearsaldate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRehearsaldate.Properties).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggle.Properties).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label1.Location = new System.Drawing.Point(1007, 32);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label1.Size = new System.Drawing.Size(81, 19);
		this.label1.TabIndex = 0;
		this.label1.Text = "كود العميل";
		this.LblCodeCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblCodeCustomer.Location = new System.Drawing.Point(562, 33);
		this.LblCodeCustomer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblCodeCustomer.Name = "LblCodeCustomer";
		this.LblCodeCustomer.Size = new System.Drawing.Size(22, 23);
		this.LblCodeCustomer.TabIndex = 0;
		this.LblCodeCustomer.Text = "0";
		this.LblCodeCustomer.Visible = false;
		this.TxtCodeCustomer.Location = new System.Drawing.Point(722, 29);
		this.TxtCodeCustomer.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCodeCustomer.Name = "TxtCodeCustomer";
		this.TxtCodeCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtCodeCustomer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtCodeCustomer.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCodeCustomer.Properties.Appearance.Options.UseFont = true;
		this.TxtCodeCustomer.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCodeCustomer.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCodeCustomer.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtCodeCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCodeCustomer.Properties.ReadOnly = true;
		this.TxtCodeCustomer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.TxtCodeCustomer.Size = new System.Drawing.Size(278, 26);
		this.TxtCodeCustomer.TabIndex = 111;
		this.TxtCodeCustomer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.TxtName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtName.BackColor = System.Drawing.Color.White;
		this.TxtName.Location = new System.Drawing.Point(356, 61);
		this.TxtName.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtName.Name = "TxtName";
		this.TxtName.ReadOnly = true;
		this.TxtName.Size = new System.Drawing.Size(644, 27);
		this.TxtName.TabIndex = 5;
		this.TxtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPhone.BackColor = System.Drawing.Color.White;
		this.TxtPhone.Location = new System.Drawing.Point(32, 61);
		this.TxtPhone.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.ReadOnly = true;
		this.TxtPhone.Size = new System.Drawing.Size(240, 27);
		this.TxtPhone.TabIndex = 6;
		this.TxtPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label10.AutoSize = true;
		this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label10.Location = new System.Drawing.Point(280, 64);
		this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label10.Name = "label10";
		this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label10.Size = new System.Drawing.Size(50, 19);
		this.label10.TabIndex = 178;
		this.label10.Text = "الجوال";
		this.lblll.AutoSize = true;
		this.lblll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblll.Location = new System.Drawing.Point(1008, 64);
		this.lblll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblll.Name = "lblll";
		this.lblll.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.lblll.Size = new System.Drawing.Size(90, 19);
		this.lblll.TabIndex = 9;
		this.lblll.Text = "إسم العميل";
		this.group.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.group.Controls.Add(this.label37);
		this.group.Controls.Add(this.TxtCharge);
		this.group.Controls.Add(this.BtnCustomerSearch);
		this.group.Controls.Add(this.BtnNewCustomer);
		this.group.Controls.Add(this.lblll);
		this.group.Controls.Add(this.label10);
		this.group.Controls.Add(this.TxtPhone);
		this.group.Controls.Add(this.TxtName);
		this.group.Controls.Add(this.TxtCodeCustomer);
		this.group.Controls.Add(this.LblCodeCustomer);
		this.group.Controls.Add(this.label1);
		this.group.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.group.Location = new System.Drawing.Point(14, 92);
		this.group.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.group.Name = "group";
		this.group.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.group.Size = new System.Drawing.Size(1107, 113);
		this.group.TabIndex = 1;
		this.group.TabStop = false;
		this.label37.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label37.Location = new System.Drawing.Point(277, 30);
		this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(94, 21);
		this.label37.TabIndex = 187;
		this.label37.Text = "رصيد العميل";
		this.TxtCharge.EditValue = "0.00";
		this.TxtCharge.Enabled = false;
		this.TxtCharge.Location = new System.Drawing.Point(32, 28);
		this.TxtCharge.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCharge.Name = "TxtCharge";
		this.TxtCharge.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCharge.Size = new System.Drawing.Size(240, 28);
		this.TxtCharge.TabIndex = 186;
		this.BtnCustomerSearch.EditValue = "";
		this.BtnCustomerSearch.Location = new System.Drawing.Point(623, 29);
		this.BtnCustomerSearch.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.BtnCustomerSearch.Name = "BtnCustomerSearch";
		this.BtnCustomerSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnCustomerSearch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnCustomerSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnCustomerSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnCustomerSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnCustomerSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnCustomerSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions1.Image");
		this.BtnCustomerSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.BtnCustomerSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnCustomerSearch.Properties.NullText = "بحث";
		this.BtnCustomerSearch.Size = new System.Drawing.Size(91, 28);
		this.BtnCustomerSearch.TabIndex = 185;
		this.BtnCustomerSearch.Tag = "بحث بالكود";
		this.BtnCustomerSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnCustomerSearch_KeyDown);
		this.BtnNewCustomer.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNewCustomer.Appearance.Options.UseFont = true;
		this.BtnNewCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNewCustomer.ImageOptions.Image");
		this.BtnNewCustomer.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNewCustomer.Location = new System.Drawing.Point(588, 29);
		this.BtnNewCustomer.Margin = new System.Windows.Forms.Padding(0);
		this.BtnNewCustomer.Name = "BtnNewCustomer";
		this.BtnNewCustomer.Size = new System.Drawing.Size(31, 27);
		this.BtnNewCustomer.TabIndex = 184;
		this.BtnNewCustomer.Click += new System.EventHandler(BtnNewCustomer_Click);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.TxtReceived);
		this.groupBox2.Controls.Add(this.TxtEvent);
		this.groupBox2.Controls.Add(this.TxtReturn);
		this.groupBox2.Controls.Add(this.TxtRehearsal);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.TxtNet);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.TxtReturndate);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.TxtEventdate);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.TxtReceiveddate);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.TxtRehearsaldate);
		this.groupBox2.Location = new System.Drawing.Point(13, 210);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1108, 164);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.TxtReceived.BackColor = System.Drawing.Color.White;
		this.TxtReceived.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtReceived.Location = new System.Drawing.Point(579, 57);
		this.TxtReceived.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtReceived.Name = "TxtReceived";
		this.TxtReceived.ReadOnly = true;
		this.TxtReceived.Size = new System.Drawing.Size(78, 28);
		this.TxtReceived.TabIndex = 195;
		this.TxtReceived.Text = "0";
		this.TxtReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtEvent.BackColor = System.Drawing.Color.White;
		this.TxtEvent.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtEvent.Location = new System.Drawing.Point(7, 22);
		this.TxtEvent.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtEvent.Name = "TxtEvent";
		this.TxtEvent.ReadOnly = true;
		this.TxtEvent.Size = new System.Drawing.Size(78, 28);
		this.TxtEvent.TabIndex = 194;
		this.TxtEvent.Text = "0";
		this.TxtEvent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtReturn.BackColor = System.Drawing.Color.White;
		this.TxtReturn.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtReturn.Location = new System.Drawing.Point(7, 57);
		this.TxtReturn.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtReturn.Name = "TxtReturn";
		this.TxtReturn.ReadOnly = true;
		this.TxtReturn.Size = new System.Drawing.Size(78, 28);
		this.TxtReturn.TabIndex = 193;
		this.TxtReturn.Text = "0";
		this.TxtReturn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtRehearsal.BackColor = System.Drawing.Color.White;
		this.TxtRehearsal.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtRehearsal.Location = new System.Drawing.Point(579, 22);
		this.TxtRehearsal.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtRehearsal.Name = "TxtRehearsal";
		this.TxtRehearsal.ReadOnly = true;
		this.TxtRehearsal.Size = new System.Drawing.Size(78, 28);
		this.TxtRehearsal.TabIndex = 192;
		this.TxtRehearsal.Text = "0";
		this.TxtRehearsal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.label6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(425, 98);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label6.Size = new System.Drawing.Size(100, 48);
		this.label6.TabIndex = 190;
		this.label6.Text = "مبلغ التأمين";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.TxtNet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtNet.DecimalDigits = 2;
		this.TxtNet.Font = new System.Drawing.Font("Tahoma", 20f, System.Drawing.FontStyle.Bold);
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Location = new System.Drawing.Point(93, 98);
		this.TxtNet.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNet.Size = new System.Drawing.Size(324, 48);
		this.TxtNet.TabIndex = 189;
		this.TxtNet.Text = "0.00";
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(423, 59);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(118, 21);
		this.label5.TabIndex = 188;
		this.label5.Text = "تاريخ الاسترجاع";
		this.TxtReturndate.EditValue = null;
		this.TxtReturndate.Location = new System.Drawing.Point(93, 57);
		this.TxtReturndate.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtReturndate.Name = "TxtReturndate";
		this.TxtReturndate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtReturndate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtReturndate.Properties.Appearance.Options.UseFont = true;
		this.TxtReturndate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtReturndate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtReturndate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtReturndate.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtReturndate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtReturndate.Properties.DisplayFormat.FormatString = "";
		this.TxtReturndate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtReturndate.Properties.EditFormat.FormatString = "";
		this.TxtReturndate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtReturndate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtReturndate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtReturndate.Size = new System.Drawing.Size(323, 28);
		this.TxtReturndate.TabIndex = 187;
		this.TxtReturndate.EditValueChanged += new System.EventHandler(TxtRehearsaldate_EditValueChanged);
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label4.Location = new System.Drawing.Point(424, 24);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(111, 21);
		this.label4.TabIndex = 186;
		this.label4.Text = "تاريخ المناسبة";
		this.TxtEventdate.EditValue = null;
		this.TxtEventdate.Location = new System.Drawing.Point(93, 22);
		this.TxtEventdate.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtEventdate.Name = "TxtEventdate";
		this.TxtEventdate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtEventdate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtEventdate.Properties.Appearance.Options.UseFont = true;
		this.TxtEventdate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtEventdate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtEventdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtEventdate.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtEventdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtEventdate.Properties.DisplayFormat.FormatString = "";
		this.TxtEventdate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtEventdate.Properties.EditFormat.FormatString = "";
		this.TxtEventdate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtEventdate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtEventdate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtEventdate.Size = new System.Drawing.Size(324, 28);
		this.TxtEventdate.TabIndex = 185;
		this.TxtEventdate.EditValueChanged += new System.EventHandler(TxtRehearsaldate_EditValueChanged);
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(996, 59);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(107, 21);
		this.label3.TabIndex = 184;
		this.label3.Text = "تاريخ الاستلام";
		this.TxtReceiveddate.EditValue = null;
		this.TxtReceiveddate.Location = new System.Drawing.Point(664, 57);
		this.TxtReceiveddate.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtReceiveddate.Name = "TxtReceiveddate";
		this.TxtReceiveddate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtReceiveddate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtReceiveddate.Properties.Appearance.Options.UseFont = true;
		this.TxtReceiveddate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtReceiveddate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtReceiveddate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtReceiveddate.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtReceiveddate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtReceiveddate.Properties.DisplayFormat.FormatString = "";
		this.TxtReceiveddate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtReceiveddate.Properties.EditFormat.FormatString = "";
		this.TxtReceiveddate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtReceiveddate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtReceiveddate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtReceiveddate.Size = new System.Drawing.Size(325, 28);
		this.TxtReceiveddate.TabIndex = 183;
		this.TxtReceiveddate.EditValueChanged += new System.EventHandler(TxtRehearsaldate_EditValueChanged);
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(997, 24);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(94, 21);
		this.label2.TabIndex = 182;
		this.label2.Text = "تاريخ البروفة";
		this.TxtRehearsaldate.EditValue = null;
		this.TxtRehearsaldate.Location = new System.Drawing.Point(665, 22);
		this.TxtRehearsaldate.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtRehearsaldate.Name = "TxtRehearsaldate";
		this.TxtRehearsaldate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtRehearsaldate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtRehearsaldate.Properties.Appearance.Options.UseFont = true;
		this.TxtRehearsaldate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtRehearsaldate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtRehearsaldate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtRehearsaldate.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtRehearsaldate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtRehearsaldate.Properties.DisplayFormat.FormatString = "";
		this.TxtRehearsaldate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtRehearsaldate.Properties.EditFormat.FormatString = "";
		this.TxtRehearsaldate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtRehearsaldate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtRehearsaldate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtRehearsaldate.Size = new System.Drawing.Size(325, 28);
		this.TxtRehearsaldate.TabIndex = 181;
		this.TxtRehearsaldate.EditValueChanged += new System.EventHandler(TxtRehearsaldate_EditValueChanged);
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(975, 519);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(155, 39);
		this.BtnPrint.TabIndex = 11;
		this.BtnPrint.Text = "طباعة";
		this.BtnPrint.Visible = false;
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(326, 519);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(155, 39);
		this.BtnSave.TabIndex = 6;
		this.BtnSave.Text = "حفظ ";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(163, 519);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(155, 39);
		this.BtnNew.TabIndex = 9;
		this.BtnNew.Text = "جديد ";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(652, 519);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(155, 39);
		this.BtnDelete.TabIndex = 8;
		this.BtnDelete.Text = "حذف ";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(815, 519);
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
		this.groupBox1.Location = new System.Drawing.Point(12, 380);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1109, 121);
		this.groupBox1.TabIndex = 183;
		this.groupBox1.TabStop = false;
		this.label27.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label27.Location = new System.Drawing.Point(280, 25);
		this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(53, 21);
		this.label27.TabIndex = 180;
		this.label27.Text = "التاريخ";
		this.TxtDate.EditValue = null;
		this.TxtDate.Location = new System.Drawing.Point(32, 23);
		this.TxtDate.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDate.Properties.Appearance.Options.UseFont = true;
		this.TxtDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate.Properties.DisplayFormat.FormatString = "";
		this.TxtDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDate.Properties.EditFormat.FormatString = "";
		this.TxtDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDate.Size = new System.Drawing.Size(241, 28);
		this.TxtDate.TabIndex = 179;
		this.toggle.EditValue = true;
		this.toggle.Location = new System.Drawing.Point(399, 23);
		this.toggle.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.toggle.Name = "toggle";
		this.toggle.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.toggle.Properties.Appearance.Options.UseFont = true;
		this.toggle.Properties.Appearance.Options.UseForeColor = true;
		this.toggle.Properties.OffText = "شراء";
		this.toggle.Properties.OnText = "تأجير";
		this.toggle.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.toggle.Size = new System.Drawing.Size(157, 25);
		this.toggle.TabIndex = 199;
		this.toggle.Toggled += new System.EventHandler(toggle_Toggled);
		this.groupBox3.Controls.Add(this.LblCode);
		this.groupBox3.Controls.Add(this.toggle);
		this.groupBox3.Controls.Add(this.TxtCode);
		this.groupBox3.Controls.Add(this.label27);
		this.groupBox3.Controls.Add(this.label18);
		this.groupBox3.Controls.Add(this.TxtDate);
		this.groupBox3.Controls.Add(this.BtnOrderSearch);
		this.groupBox3.Location = new System.Drawing.Point(12, 12);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1109, 75);
		this.groupBox3.TabIndex = 184;
		this.groupBox3.TabStop = false;
		this.LblCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblCode.Location = new System.Drawing.Point(593, 23);
		this.LblCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblCode.Name = "LblCode";
		this.LblCode.Size = new System.Drawing.Size(22, 23);
		this.LblCode.TabIndex = 211;
		this.LblCode.Text = "0";
		this.LblCode.Visible = false;
		this.TxtCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCode.Location = new System.Drawing.Point(722, 22);
		this.TxtCode.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
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
		this.TxtCode.Size = new System.Drawing.Size(278, 28);
		this.TxtCode.TabIndex = 210;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCodeRever_ButtonClick);
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label18.Location = new System.Drawing.Point(1007, 26);
		this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label18.Name = "label18";
		this.label18.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label18.Size = new System.Drawing.Size(82, 21);
		this.label18.TabIndex = 208;
		this.label18.Text = "رقم السند";
		this.BtnOrderSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.BtnOrderSearch.EditValue = "";
		this.BtnOrderSearch.Location = new System.Drawing.Point(623, 22);
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
		this.BtnOrderSearch.Size = new System.Drawing.Size(91, 28);
		this.BtnOrderSearch.TabIndex = 209;
		this.BtnOrderSearch.Tag = "بحث بالكود";
		this.BtnOrderSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnOrderSearch_KeyDown);
		this.BtnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(489, 519);
		this.BtnEdit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Size = new System.Drawing.Size(155, 39);
		this.BtnEdit.TabIndex = 6;
		this.BtnEdit.Text = "تعديل";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click_1);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1133, 571);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.BtnPrint);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.group);
		base.MaximizeBox = false;
		base.Name = "FrmReservation";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "الحجوزات";
		((System.ComponentModel.ISupportInitialize)this.TxtCodeCustomer.Properties).EndInit();
		this.group.ResumeLayout(false);
		this.group.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnCustomerSearch.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtReturndate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtReturndate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtEventdate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtEventdate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtReceiveddate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtReceiveddate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRehearsaldate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRehearsaldate.Properties).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggle.Properties).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


