using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.Cust_Sup.Search;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Virgins;

public class FrmVirgin : XtraForm
{
	private GTSdbEntities db = new GTSdbEntities();

	private DataTable myTable = new DataTable("gridEX1");

	public bool ISActiveTrue = true;

	private DateTime FrmDate = GetNistTime();

	private int TypeValue = 1;

	private string MasterID;

	private IContainer components = null;

	private SimpleButton BtnExit;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private Label label8;

	private Label label10;

	private TextBox TxtPhone;

	private TextBox TxtName;

	private Label label9;

	private TextBox TxtVatNum;

	private ButtonEdit TxtCode;

	private Label label12;

	private Label label19;

	private TextBox TxtCommercialRecord;

	private Label label13;

	private TextBox TxtAddress;

	private GridEX gridEX1;

	private SimpleButton BtnNew;

	private Label LblID;

	private SimpleButton BtnAddCustomer;

	private SimpleButton BtnActive;

	private TextEdit TxtCharge;

	private SimpleButton BtnTest;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private Label label21;

	private System.Windows.Forms.ComboBox CBMarkter;

	private CheckBox CKClosed;

	private Label label18;

	private System.Windows.Forms.ComboBox CBCompany;

	private CheckBox CKMessage;

	private Label label17;

	private Label label16;

	private NumericUpDown TxtNumMessage;

	private Label label20;

	private SimpleButton BtnPast;

	private Label label7;

	private NumericUpDown TxtNumCopy;

	private Label label6;

	private DateEdit TxtDateTo;

	private Label label27;

	private DateEdit TxtDateFrom;

	private Label label11;

	private TextBox TxtSerial;

	private Label label14;

	private Label label4;

	private System.Windows.Forms.ComboBox CBType;

	private System.Windows.Forms.ComboBox CBVirgin;

	private Label label3;

	private NumericUpDown TxtNumDayes;

	private TextBox TxtDateVirg;

	private TabPage tabPage2;

	private SeparatorControl separatorControl2;

	private TextBox TxtHDD;

	private Label label1;

	private Label label2;

	private TextBox TxtMac;

	private SimpleButton BtnCopyActive;

	private TextBox TxtActive;

	private Label SerialNumber;

	private TextBox TxtSerialBoard;

	private CheckBox CKShowVirginInLogin;

	private SeparatorControl separatorControl3;

	public Label LblUser;

	private Label label22;

	private NumericUpDown NPermission;

	private TextBox TxtKey1;

	private Label label25;

	private Label label26;

	private TextBox TxtSKey2;

	private Label label23;

	private TextBox TxtDeviceName;

	private Label label24;

	private TextBox TxtNote;

	private SimpleButton BtnCopyTrial;

	private TextBox TxtTrial;

	private SimpleButton BtnTrial;

	private SimpleButton BtnOrderActive;

	private TextBox TxtStatuse;

	private SimpleButton BtnCancelActive;

	private Label LblMasterDetailsSN;

	private Label label15;

	private DateEdit TxtDateDay;

	private Label label5;

	private System.Windows.Forms.ComboBox CbAppType;

	private SimpleButton BtnOrders;

	private TabPage tabPage3;

	private TextBox TxtEncrActiveTest;

	private SimpleButton BtnPastTestActive;

	private TextBox TxtActiveTest;

	private TextBox textBox1;

	private SimpleButton simpleButton1;

	private TextBox textBox2;

	private ButtonEdit BtnSuplierSearch;

	private ButtonEdit BtnOrderSearch;

	private ToggleSwitch TglType;

	public FrmVirgin()
	{
		InitializeComponent();
		Bind();
		ClearCustomer();
		Clear();
		TxtSerial.Focus();
		CreateGrid();
		tabControl1.TabPages.Remove(tabPage3);
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		ClearCustomer();
		Clear();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnPast_Click(object sender, EventArgs e)
	{
		Clear();
		TxtSerial.Text = string.Empty;
		TxtSerial.Text = Clipboard.GetText();
	}

	private void BtnCopy_Click(object sender, EventArgs e)
	{
		try
		{
			Clipboard.SetText(TxtActive.Text);
		}
		catch (Exception ex)
		{
			MessageBox.Show("عفوا، لم يتم النسخ \r\n يرجي المحاولة مرة اخري \r\n " + ex.ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchCust();
	}

	private void BtnOrders_Click(object sender, EventArgs e)
	{
		SearchStatuse();
	}

	private void BtnOrderActive_Click(object sender, EventArgs e)
	{
		ActiveCode();
		UpdateStatuse(Convert.ToInt32(LblMasterDetailsSN.Text), 1);
		TxtStatuse.Text = "طلب تفعيل جديد";
		TxtStatuse.ForeColor = Color.Green;
	}

	private void BtnCancelActive_Click(object sender, EventArgs e)
	{
		UpdateStatuse(Convert.ToInt32(LblMasterDetailsSN.Text), 3);
		TxtStatuse.Text = "رفض التفعيل";
		TxtStatuse.ForeColor = Color.Red;
	}

	private void gridEX1_MouseClick_1(object sender, MouseEventArgs e)
	{
		try
		{
			int position = gridEX1.SelectedItems[0].Position;
			int sN = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
			int custID = Convert.ToInt32(LblID.Text);
			BindVirgin(custID, sN);
		}
		catch (Exception)
		{
		}
	}

	public static DateTime GetNistTime()
	{
		try
		{
			using WebResponse webResponse = WebRequest.Create("http://www.google.com").GetResponse();
			return DateTime.ParseExact(webResponse.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
		}
		catch (Exception)
		{
			return Convert.ToDateTime("0001-01-01");
		}
	}

	private void Clear()
	{
		TxtActive.Clear();
		TxtSerial.Clear();
		TxtHDD.Clear();
		TxtMac.Clear();
		TxtSerialBoard.Clear();
		TxtNumDayes.Value = 3m;
		TxtNumCopy.Value = 0m;
		TxtNumMessage.Value = 0m;
		TxtDateDay.DateTime = FrmDate;
		TxtDateTo.DateTime = FrmDate.AddDays(3.0);
		TxtDateFrom.DateTime = FrmDate;
		TxtDateVirg.Clear();
		CBType.SelectedValue = 1;
		TglType.IsOn = false;
		CBVirgin.SelectedValue = 1;
		CBCompany.SelectedValue = 1;
		CBMarkter.SelectedValue = 1;
		CbAppType.SelectedValue = 1;
		NPermission.Value = 0m;
		CKMessage.Checked = false;
		CKClosed.Checked = true;
		CKShowVirginInLogin.Checked = false;
		TxtSerial.ForeColor = Color.Black;
		TxtKey1.Text = "b14ca5898a4e4133bbce2ea2315a1500";
		TxtSKey2.Text = "b14ca5898a4e4133bbce2ea2315a1916";
		TxtDeviceName.Clear();
		TxtNote.Clear();
		TxtTrial.Clear();
		TxtStatuse.Clear();
		LblMasterDetailsSN.Text = "0";
		TxtActive.Enabled = false;
		BtnActive.Enabled = false;
		BtnCancelActive.Enabled = false;
		TxtTrial.Enabled = true;
		BtnCopyTrial.Enabled = true;
		BtnTrial.Enabled = true;
		LblUser.Text = LoginDetails.Active_LblUserName;
		if (LoginDetails.Active_ShowVirgin)
		{
			CKShowVirginInLogin.Enabled = true;
		}
		else
		{
			CKShowVirginInLogin.Enabled = false;
		}
		if (LoginDetails.Active_TypeID > 0)
		{
			CBType.Enabled = false;
			TglType.Enabled = false;
		}
		else
		{
			TglType.Enabled = true;
			CBType.Enabled = true;
		}
		if (LoginDetails.Active_CompID > 0)
		{
			CBCompany.Enabled = false;
			CBCompany.SelectedValue = LoginDetails.Active_CompID;
		}
		else
		{
			CBCompany.Enabled = true;
		}
		BtnOrderSearch.Text = string.Empty;
		BtnSuplierSearch.Text = string.Empty;
	}

	private void ClearCustomer()
	{
		TxtCharge.Text = "0.00";
		LblID.Text = "0";
		TxtCode.Text = string.Empty;
		TxtName.Clear();
		TxtPhone.Clear();
		TxtCommercialRecord.Clear();
		TxtVatNum.Clear();
		TxtAddress.Clear();
		myTable.Clear();
		gridEX1.SetDataBinding(myTable, string.Empty);
	}

	public void Bind()
	{
		CBVirgin.DataSource = db.Virg_Virgins.ToList();
		CBVirgin.DisplayMember = "Name";
		CBVirgin.ValueMember = "ID";
		CBType.DataSource = db.Virg_Type.ToList();
		CBType.DisplayMember = "Name";
		CBType.ValueMember = "ID";
		CBCompany.DataSource = db.Virg_Company.ToList();
		CBCompany.DisplayMember = "Name";
		CBCompany.ValueMember = "ID";
		CBMarkter.DataSource = db.Virg_Branch.ToList();
		CBMarkter.DisplayMember = "Name";
		CBMarkter.ValueMember = "ID";
		CbAppType.DataSource = db.Virg_AppType.ToList();
		CbAppType.DisplayMember = "Name";
		CbAppType.ValueMember = "ID";
	}

	private void CreateGrid()
	{
		gridEX1.ColumnAutoResize = true;
		myTable.Columns.Add("الكود", typeof(int));
		myTable.Columns.Add("التاريخ", typeof(string));
		myTable.Columns.Add("الشركة", typeof(string));
		myTable.Columns.Add("الفيرجن", typeof(string));
	}

	private void BindGridData()
	{
		int value = Convert.ToInt32(LblID.Text);
		myTable.Clear();
		List<Select_Virgins_Result> list = db.Select_Virgins(value, 0).ToList();
		if (list != null)
		{
			foreach (Select_Virgins_Result item in list)
			{
				myTable.Rows.Add(item.SN, item.Date, item.CompName, item.VirgName);
			}
			gridEX1.SetDataBinding(myTable, string.Empty);
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["الكود"].Width = 40;
			gridEX1.RootTable.Columns["الكود"].RightToLeft = RightToLeft.No;
			gridEX1.RootTable.Columns["الكود"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["التاريخ"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الشركة"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الفيرجن"].TextAlignment = TextAlignment.Center;
		}
		if (myTable.Rows.Count == 0)
		{
			Clear();
		}
	}

	private void BindVirgin(int CustID, int SN)
	{
		try
		{
			Select_Virgins_Result select_Virgins_Result = db.Select_Virgins(CustID, SN).ToList().FirstOrDefault();
			MasterID = select_Virgins_Result.ID.ToString();
			LblMasterDetailsSN.Text = select_Virgins_Result.SN.ToString();
			TxtActive.Text = select_Virgins_Result.Active;
			TxtTrial.Text = select_Virgins_Result.Trial;
			TxtSerial.Text = select_Virgins_Result.Serial;
			TxtHDD.Text = select_Virgins_Result.HDD;
			TxtMac.Text = select_Virgins_Result.Mac;
			TxtSerialBoard.Text = select_Virgins_Result.SerialBoard;
			CBType.SelectedValue = Convert.ToInt32(select_Virgins_Result.VirgTypeID);
			if (Convert.ToInt32(select_Virgins_Result.VirgTypeID) > 1)
			{
				TglType.IsOn = true;
			}
			else
			{
				TglType.IsOn = false;
			}
			CBVirgin.SelectedValue = Convert.ToInt32(select_Virgins_Result.VirgID);
			CBCompany.SelectedValue = Convert.ToInt32(select_Virgins_Result.CompID);
			CBMarkter.SelectedValue = Convert.ToInt32(select_Virgins_Result.MarkterID);
			TxtNumDayes.Value = Convert.ToInt32(select_Virgins_Result.NumDays);
			TxtNumCopy.Value = Convert.ToInt32(select_Virgins_Result.NumCopys);
			TxtNumMessage.Value = Convert.ToInt32(select_Virgins_Result.NumMessage);
			TxtDateDay.DateTime = Convert.ToDateTime(select_Virgins_Result.Date);
			TxtDateTo.DateTime = Convert.ToDateTime(select_Virgins_Result.DateTo);
			TxtDateFrom.DateTime = Convert.ToDateTime(select_Virgins_Result.DateCreate);
			TxtDateVirg.Text = select_Virgins_Result.VirgDue;
			CKMessage.Checked = Convert.ToBoolean(select_Virgins_Result.AlertMessage);
			CKClosed.Checked = Convert.ToBoolean(select_Virgins_Result.AlertClose);
			CKShowVirginInLogin.Checked = Convert.ToBoolean(select_Virgins_Result.ShowVirginInLogin);
			NPermission.Value = Convert.ToInt32(select_Virgins_Result.NPermission);
			LblUser.Text = select_Virgins_Result.UserName;
			TxtDeviceName.Text = select_Virgins_Result.DeviceName;
			TxtNote.Text = select_Virgins_Result.Notes;
			int StID = Convert.ToInt32(select_Virgins_Result.StatuseID);
			Virg_Statuse virg_Statuse = db.Virg_Statuse.Where((Virg_Statuse x) => x.ID == StID).FirstOrDefault();
			if (virg_Statuse != null)
			{
				TxtStatuse.Text = virg_Statuse.Name;
			}
		}
		catch (Exception)
		{
		}
	}

	private void SearchCust()
	{
		FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
		frmSearchCustomer.ShowDialog();
		if (frmSearchCustomer.CustID > 0)
		{
			int CustID = frmSearchCustomer.CustID;
			Account_CustSup account_CustSup = db.Account_CustSup.Where((Account_CustSup x) => x.ID == CustID).FirstOrDefault();
			if (account_CustSup != null)
			{
				ClearCustomer();
				LblID.Text = account_CustSup.ID.ToString();
				TxtCode.Text = account_CustSup.ID.ToString();
				TxtVatNum.Text = account_CustSup.VatNum.ToString();
				TxtName.Text = account_CustSup.CustSuppName;
				TxtPhone.Text = account_CustSup.Phone;
				TxtCommercialRecord.Text = account_CustSup.CommercialRecord;
				TxtAddress.Text = account_CustSup.City;
				BindGridData();
			}
		}
	}

	private void SearchStatuse()
	{
		FrmSearchStatuse frmSearchStatuse = new FrmSearchStatuse();
		frmSearchStatuse.ShowDialog();
		if (frmSearchStatuse.CustID > 0)
		{
			int CustID = frmSearchStatuse.CustID;
			int sN = frmSearchStatuse.SN;
			Account_CustSup account_CustSup = db.Account_CustSup.AsNoTracking().FirstOrDefault((Account_CustSup x) => x.ID == CustID);
			if (account_CustSup != null)
			{
				ClearCustomer();
				LblID.Text = account_CustSup.ID.ToString();
				TxtCode.Text = account_CustSup.ID.ToString();
				TxtVatNum.Text = account_CustSup.VatNum;
				TxtName.Text = account_CustSup.CustSuppName;
				TxtPhone.Text = account_CustSup.Phone;
				TxtCommercialRecord.Text = account_CustSup.CommercialRecord;
				TxtAddress.Text = account_CustSup.City;
				Charge(CustID);
				BindGridData();
			}
			if (sN > 0)
			{
				BindVirgin(CustID, sN);
			}
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

	private void Master(int CustID)
	{
		int num = 0;
		Virg_Master virg_Master = db.Virg_Master.Where((Virg_Master x) => x.CustID == (int?)CustID).FirstOrDefault();
		if (virg_Master == null)
		{
			int num2 = Convert.ToInt32(db.Virg_Master.Select((Virg_Master x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
			Virg_Master virg_Master2 = new Virg_Master();
			virg_Master2.ID = num2;
			virg_Master2.CustID = CustID;
			db.Virg_Master.Add(virg_Master2);
			db.SaveChanges();
			num = num2;
		}
		else
		{
			num = virg_Master.ID;
		}
		if (num > 0)
		{
			MasterDetails(num);
		}
	}

	private void MasterDetails(int ID)
	{
		int num = Convert.ToInt32(db.Virg_Master.Select((Virg_Master x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
		Virg_MasterDetails virg_MasterDetails = new Virg_MasterDetails();
		virg_MasterDetails.MasterID = ID;
		virg_MasterDetails.Active = TxtActive.Text;
		virg_MasterDetails.Trial = TxtTrial.Text;
		virg_MasterDetails.Serial = TxtSerial.Text;
		virg_MasterDetails.HDD = TxtHDD.Text;
		virg_MasterDetails.Mac = TxtMac.Text;
		virg_MasterDetails.SerialBoard = TxtSerialBoard.Text;
		virg_MasterDetails.NumDays = Convert.ToInt32(TxtNumDayes.Value);
		virg_MasterDetails.NumCopys = Convert.ToInt32(TxtNumCopy.Value);
		virg_MasterDetails.NumMessage = Convert.ToInt32(TxtNumMessage.Value);
		virg_MasterDetails.Date = TxtDateDay.DateTime;
		virg_MasterDetails.DateTo = TxtDateTo.DateTime;
		virg_MasterDetails.DateCreate = TxtDateFrom.DateTime;
		virg_MasterDetails.VirgDue = TxtDateVirg.Text;
		virg_MasterDetails.VirgTypeID = TypeValue;
		virg_MasterDetails.VirgID = Convert.ToInt32(CBVirgin.SelectedValue);
		virg_MasterDetails.CompID = Convert.ToInt32(CBCompany.SelectedValue);
		virg_MasterDetails.MarkterID = Convert.ToInt32(CBMarkter.SelectedValue);
		virg_MasterDetails.NPermission = Convert.ToInt32(NPermission.Value);
		virg_MasterDetails.AlertMessage = Convert.ToBoolean(CKMessage.Checked);
		virg_MasterDetails.AlertClose = Convert.ToBoolean(CKClosed.Checked);
		virg_MasterDetails.ShowVirginInLogin = Convert.ToBoolean(CKShowVirginInLogin.Checked);
		virg_MasterDetails.UserID_Add = LoginDetails.Active_LblUserID;
		virg_MasterDetails.UserBranch_Add = LoginDetails.GroupID;
		virg_MasterDetails.UserMacAddress_Add = LoginDetails.macAddress;
		virg_MasterDetails.UserDate_Add = FrmDate;
		virg_MasterDetails.DeviceName = TxtDeviceName.Text;
		virg_MasterDetails.Notes = TxtNote.Text;
		virg_MasterDetails.AppTypeID = Convert.ToInt32(CbAppType.SelectedValue);
		db.Virg_MasterDetails.Add(virg_MasterDetails);
		db.SaveChanges();
		LblMasterDetailsSN.Text = db.Virg_MasterDetails.Select((Virg_MasterDetails x) => x.SN).DefaultIfEmpty(0).Max()
			.ToString();
	}

	private void DeleteMasterDetailsBySN(int sn)
	{
		Virg_MasterDetails virg_MasterDetails = db.Virg_MasterDetails.FirstOrDefault((Virg_MasterDetails x) => x.SN == sn);
		if (virg_MasterDetails != null)
		{
			db.Virg_MasterDetails.Remove(virg_MasterDetails);
			db.SaveChanges();
		}
	}

	private void ActiveCode()
	{
		try
		{
			string text = (TglType.IsOn ? TglType.Properties.OnText : TglType.Properties.OffText);
			if (TxtName.Text != string.Empty && TxtSerial.Text != string.Empty)
			{
				if (ISActiveTrue)
				{
					DialogResult dialogResult = MessageBox.Show("اسم العميل: " + TxtName.Text + " \r\n الفيرجن: " + CBVirgin.Text + " \r\n تاريخ الانتهاء: " + TxtDateTo.DateTime.ToShortDateString() + " \r\n  \r\n الشركة: " + CBCompany.Text + " \r\n نوع التفعيل: " + text + " \r\n  \r\n موظف التفعيل: " + LoginDetails.UserName + " \r\n المسوق: " + CBMarkter.Text, "تم التفعيل بنجاح", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					if (dialogResult == DialogResult.Yes && DecriptData() == 1)
					{
						Master(Convert.ToInt32(LblID.Text));
						BindGridData();
						MessageBox.Show("اسم العميل: " + TxtName.Text + " \r\n الفيرجن: " + CBVirgin.Text + " \r\n تاريخ الانتهاء: " + TxtDateTo.DateTime.ToShortDateString() + " \r\n  \r\n الشركة: " + CBCompany.Text + " \r\n نوع التفعيل: " + text + " \r\n  \r\n موظف التفعيل: " + LoginDetails.UserName + " \r\n المسوق: " + CBMarkter.Text + " \r\n تاريخ التفعيل: " + DateTime.Now.ToString(" yyyy/MM/dd hh:mm "), "تم التفعيل بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
				}
				else
				{
					MessageBox.Show("عفوا، انتهت عدد مرات التفعيل لهذا الجهاز الرجاء الترخيص ", "Result", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				if (TxtName.Text == string.Empty)
				{
					MessageBox.Show("عفوا، يرجي اختيار اسم المستخدم والمحاولة مرة اخري ", "Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
				if (TxtSerial.Text == string.Empty)
				{
					MessageBox.Show("عفوا، يرجي لصق السيريال والمحاولة مرة اخري ", "Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("عفوا، لم يتم التفعيل \r\n يرجي المحاولة مرة اخري \r\n " + ex.ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private int DecriptData()
	{
		int num = 0;
		try
		{
			string text = Class_Crypto.SetData(TxtKey1.Text, TxtSKey2.Text, TxtMac.Text, TxtSerialBoard.Text, TxtHDD.Text, CBCompany.SelectedValue.ToString(), TypeValue.ToString(), CBVirgin.SelectedValue.ToString(), TxtDateTo.DateTime.ToString("yyyy-MM-dd"), CKMessage.Checked.ToString(), CKClosed.Checked.ToString(), TxtNumMessage.Value.ToString(), CKShowVirginInLogin.Checked.ToString(), NPermission.Value.ToString());
			string text2 = Class_Crypto.SetDataSecondWay(TxtKey1.Text, TxtSKey2.Text, text);
			if (Convert.ToInt32(TypeValue) == 1)
			{
				TxtTrial.Text = text2;
			}
			else
			{
				TxtActive.Text = text2;
			}
			return 1;
		}
		catch (Exception)
		{
			throw;
		}
	}

	private void SearchHDD(string HDD)
	{
		LoginDetails.HDD = HDD;
		FrmSearch frmSearch = new FrmSearch();
		frmSearch.ShowDialog();
		LoginDetails.HDD = string.Empty;
		LoginDetails.Mac = string.Empty;
		LoginDetails.SerialBoard = string.Empty;
	}

	private void UpdateStatuse(int SN, int StsID)
	{
		Virg_MasterDetails virg_MasterDetails = db.Virg_MasterDetails.Where((Virg_MasterDetails x) => x.SN == SN).FirstOrDefault();
		if (virg_MasterDetails != null)
		{
			virg_MasterDetails.StatuseID = StsID;
			virg_MasterDetails.Notes = TxtNote.Text;
			db.SaveChanges();
		}
	}

	private void TxtDateTo_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void TxtNumDayes_ValueChanged(object sender, EventArgs e)
	{
		try
		{
			int num = Convert.ToInt32(TxtNumDayes.Value);
			TxtDateTo.DateTime = TxtDateDay.DateTime.AddDays(num);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TxtDateTo_EditValueChanged(object sender, EventArgs e)
	{
		try
		{
			DateTime dateTime = TxtDateTo.DateTime;
			DateTime dateTime2 = TxtDateDay.DateTime;
			double totalDays = dateTime.Subtract(dateTime2).TotalDays;
			int num = Convert.ToInt32(totalDays);
			if (Convert.ToInt32(TypeValue) == 1 && num >= 5)
			{
				num = 5;
				TxtDateTo.DateTime = TxtDateDay.DateTime.AddDays(num);
			}
			if (Convert.ToInt32(TypeValue) > 1 && num >= 365)
			{
				num = 365;
				TxtDateTo.DateTime = TxtDateDay.DateTime.AddDays(num);
			}
			if (num < 0)
			{
				num = 1;
				TxtDateTo.DateTime = TxtDateDay.DateTime.AddDays(num);
			}
			TxtNumDayes.Value = num;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TxtSerial_TextChanged(object sender, EventArgs e)
	{
		GetDataFromEncrption(TxtSerial.Text);
		DataTable dataTable = new DataTable();
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
		SqlCommand sqlCommand = new SqlCommand("Get_Serial", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlCommand.Parameters.AddWithValue("@HDD", TxtHDD.Text);
		sqlDataAdapter.Fill(dataTable);
		sqlConnection.Close();
		if (dataTable.Rows.Count > 0)
		{
			MessageBox.Show("انتبه، بيانات الجهاز متشابهة مع عدد ( " + dataTable.Rows.Count + " ) اجهزة في السيستم \r\n الرجاء البحث والتأكد من البيانات والمحاولة مرة اخري ", "Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			TxtSerial.ForeColor = Color.Red;
			TxtNumCopy.Value = dataTable.Rows.Count;
		}
	}

	private void BtnAddCustomer_Click(object sender, EventArgs e)
	{
		new FrmCustomer().Show();
	}

	private void BtnTrial_Click(object sender, EventArgs e)
	{
		ActiveCode();
	}

	private void BtnActive_Click(object sender, EventArgs e)
	{
		string text = (TglType.IsOn ? TglType.Properties.OnText : TglType.Properties.OffText);
		if (Convert.ToInt32(TypeValue) != 2)
		{
			return;
		}
		DialogResult dialogResult = MessageBox.Show("اسم العميل: " + TxtName.Text + " \r\n الفيرجن: " + CBVirgin.Text + " \r\n تاريخ الانتهاء: " + TxtDateTo.DateTime.ToShortDateString() + " \r\n  \r\n الشركة: " + CBCompany.Text + " \r\n نوع التفعيل: " + text + " \r\n  \r\n موظف التفعيل: " + LoginDetails.UserName + " \r\n المسوق: " + CBMarkter.Text, "تم التفعيل بنجاح", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		if (dialogResult != DialogResult.Yes || DecriptData() != 1)
		{
			return;
		}
		if (TxtTrial.Text == "")
		{
			Master(Convert.ToInt32(LblID.Text));
		}
		else
		{
			int SNM = Convert.ToInt32(LblMasterDetailsSN.Text);
			Virg_MasterDetails virg_MasterDetails = db.Virg_MasterDetails.Where((Virg_MasterDetails x) => x.SN == SNM).FirstOrDefault();
			if (virg_MasterDetails != null)
			{
				virg_MasterDetails.StatuseID = 2;
				virg_MasterDetails.Notes = TxtNote.Text;
				virg_MasterDetails.Active = TxtActive.Text;
				db.SaveChanges();
			}
			TxtStatuse.Text = "قبول التفعيل";
			TxtStatuse.ForeColor = Color.Blue;
			DeleteMasterDetailsBySN(Convert.ToInt32(LblMasterDetailsSN.Text));
			MasterDetails(Convert.ToInt32(MasterID));
		}
		BindGridData();
		MessageBox.Show("اسم العميل: " + TxtName.Text + " \r\n الفيرجن: " + CBVirgin.Text + " \r\n تاريخ الانتهاء: " + TxtDateTo.DateTime.ToShortDateString() + " \r\n  \r\n الشركة: " + CBCompany.Text + " \r\n نوع التفعيل: " + text + " \r\n  \r\n موظف التفعيل: " + LoginDetails.UserName + " \r\n المسوق: " + CBMarkter.Text + " \r\n تاريخ التفعيل: " + DateTime.Now.ToString(" yyyy/MM/dd hh:mm "), "تم التفعيل بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
	}

	private void BtnCopyTrial_Click(object sender, EventArgs e)
	{
		try
		{
			Clipboard.SetText(TxtTrial.Text);
		}
		catch (Exception ex)
		{
			MessageBox.Show("عفوا، لم يتم النسخ \r\n يرجي المحاولة مرة اخري \r\n " + ex.ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnTest_Click(object sender, EventArgs e)
	{
		if (TxtHDD.Text != string.Empty)
		{
			SearchHDD(TxtHDD.Text);
		}
		else
		{
			MessageBox.Show("عفوا، لم يتم ادخال بيانات السيريال \r\n الرجاء ادخال السيريال والتأكد من البيانات والمحاولة مرة اخري ", "Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void CBType_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToInt32(CBType.SelectedValue) > 1)
			{
				TxtNumDayes.Maximum = 365m;
				ISActiveTrue = true;
				TxtKey1.Text = "b14ca5898a4e4133bbce2ea2315a1500";
				TxtSKey2.Text = "b14ca5898a4e4133bbce2ea2315a1916";
				TxtKey1.Enabled = true;
				TxtSKey2.Enabled = true;
				NPermission.Enabled = true;
				TxtDateTo.DateTime = FrmDate.AddYears(1);
				CKShowVirginInLogin.Enabled = true;
				TxtDateTo.Enabled = true;
				TxtActive.Enabled = true;
				BtnActive.Enabled = true;
				BtnOrderActive.Enabled = false;
				BtnCancelActive.Enabled = true;
				TxtTrial.Enabled = false;
				BtnTrial.Enabled = false;
			}
			if (Convert.ToInt32(CBType.SelectedValue) == 1)
			{
				TxtNumDayes.Maximum = 5m;
				CKShowVirginInLogin.Checked = false;
				CKShowVirginInLogin.Enabled = false;
				NPermission.Value = 0m;
				NPermission.Enabled = false;
				TxtDateTo.Enabled = false;
				TxtDateTo.DateTime = FrmDate.AddDays(3.0);
				TxtKey1.Text = "b14ca5898a4e4133bbce2ea2315a1500";
				TxtSKey2.Text = "b14ca5898a4e4133bbce2ea2315a1916";
				TxtKey1.Enabled = false;
				TxtSKey2.Enabled = false;
				TxtActive.Enabled = false;
				BtnActive.Enabled = false;
				BtnOrderActive.Enabled = true;
				BtnCancelActive.Enabled = false;
				TxtTrial.Enabled = true;
				BtnTrial.Enabled = true;
				if (TxtNumCopy.Value > 3m)
				{
					ISActiveTrue = true;
				}
				else
				{
					ISActiveTrue = false;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void TxtNumCopy_ValueChanged(object sender, EventArgs e)
	{
		if (TxtNumCopy.Value <= 3m)
		{
			ISActiveTrue = true;
		}
		else
		{
			ISActiveTrue = false;
		}
	}

	private void GetDataFromEncrption(string SerialEncr)
	{
		try
		{
			if (SerialEncr != "")
			{
				string data = Class_Crypto.GetData(SerialEncr, TxtKey1.Text);
				string[] array = Class_Crypto.ShowData(data, TxtSKey2.Text);
				string data2 = Class_Crypto.GetData(array[1], TxtKey1.Text);
				string[] array2 = Class_Crypto.ShowData(data2, TxtSKey2.Text);
				TxtMac.Text = array2[0].ToString();
				TxtSerialBoard.Text = array2[1].ToString();
				TxtHDD.Text = array2[2].ToString();
				TxtDateVirg.Text = array2[3].ToString();
				TxtDateFrom.Text = array2[4].ToString();
				TxtDeviceName.Text = array2[5].ToString();
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	private void BtnPastTestActive_Click(object sender, EventArgs e)
	{
		TxtActiveTest.Text = string.Empty;
		TxtActiveTest.Text = Clipboard.GetText();
	}

	private void TxtActiveTest_TextChanged(object sender, EventArgs e)
	{
		try
		{
			TxtEncrActiveTest.Text = "";
			string text = TxtActiveTest.Text;
			if (text != null)
			{
				string data = Class_Crypto.GetData(text, "b14ca5898a4e4133bbce2ea2315a1500");
				string[] array = Class_Crypto.ShowData(data, "b14ca5898a4e4133bbce2ea2315a1916");
				string data2 = Class_Crypto.GetData(array[1], "b14ca5898a4e4133bbce2ea2315a1500");
				string[] array2 = Class_Crypto.ShowData(data2, "b14ca5898a4e4133bbce2ea2315a1916");
				TxtEncrActiveTest.Text = array2[0].ToString() + " \r\n " + array2[1].ToString() + " \r\n " + array2[2].ToString() + " \r\n " + Convert.ToInt32(array2[3]) + " \r\n " + Convert.ToInt32(array2[4]) + " \r\n " + Convert.ToInt32(array2[5]) + " \r\n " + Convert.ToDateTime(DateTime.Parse(array2[6])).ToString("yyyy-MM-dd") + " \r\n " + Convert.ToDateTime(Convert.ToDateTime(DateTime.Parse(array2[6])).ToString("yyyy-MM-dd")).ToString() + " \r\n " + Convert.ToBoolean(array2[7]) + " \r\n " + Convert.ToBoolean(array2[8]) + " \r\n " + Convert.ToInt32(array2[9]) + " \r\n " + Convert.ToBoolean(array2[10]) + " \r\n " + Convert.ToInt32(array2[11]) + " \r\n ";
			}
		}
		catch (Exception)
		{
		}
	}

	private void simpleButton1_Click(object sender, EventArgs e)
	{
		textBox2.Text = string.Empty;
		textBox2.Text = Clipboard.GetText();
	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (textBox2.Text != "")
			{
				string data = Class_Crypto.GetData(textBox2.Text, TxtKey1.Text);
				string[] array = Class_Crypto.ShowData(data, TxtSKey2.Text);
				string data2 = Class_Crypto.GetData(array[1], TxtKey1.Text);
				string[] array2 = Class_Crypto.ShowData(data2, TxtSKey2.Text);
				textBox1.Text = array2[0].ToString() + " \r\n " + array2[1].ToString() + " \r\n " + array2[2].ToString() + " \r\n " + array2[3].ToString() + " \r\n " + array2[4].ToString() + " \r\n " + array2[5].ToString() + " \r\n ";
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	private void BtnSuplierSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SearchSup(Convert.ToInt32(BtnSuplierSearch.Text));
		}
	}

	private void BtnSuplierSearch_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
	}

	private void BtnSuplierSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchSup(Convert.ToInt32(BtnSuplierSearch.Text));
	}

	private void SearchSup(int CustID)
	{
		Account_CustSup account_CustSup = db.Account_CustSup.Where((Account_CustSup x) => x.ID == CustID).FirstOrDefault();
		if (account_CustSup != null)
		{
			ClearCustomer();
			LblID.Text = account_CustSup.ID.ToString();
			TxtCode.Text = account_CustSup.ID.ToString();
			TxtVatNum.Text = account_CustSup.VatNum.ToString();
			TxtName.Text = account_CustSup.CustSuppName;
			TxtPhone.Text = account_CustSup.Phone;
			TxtCommercialRecord.Text = account_CustSup.CommercialRecord;
			TxtAddress.Text = account_CustSup.City;
			BindGridData();
		}
	}

	private void SearchOrder(int CustId)
	{
	}

	private void BtnOrderSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
	}

	private void toggleHold_Properties_Click(object sender, EventArgs e)
	{
	}

	private void TglType_Toggled(object sender, EventArgs e)
	{
		if (TglType.IsOn)
		{
			TypeValue = 2;
		}
		else
		{
			TypeValue = 1;
		}
	}

	private void TglType_Click(object sender, EventArgs e)
	{
		try
		{
			if (!TglType.IsOn)
			{
				TypeValue = 2;
				TxtNumDayes.Maximum = 365m;
				ISActiveTrue = true;
				TxtKey1.Text = "b14ca5898a4e4133bbce2ea2315a1500";
				TxtSKey2.Text = "b14ca5898a4e4133bbce2ea2315a1916";
				TxtKey1.Enabled = true;
				TxtSKey2.Enabled = true;
				NPermission.Enabled = true;
				TxtDateTo.DateTime = FrmDate.AddYears(1);
				CKShowVirginInLogin.Enabled = true;
				TxtDateTo.Enabled = true;
				TxtActive.Enabled = true;
				BtnActive.Enabled = true;
				BtnOrderActive.Enabled = false;
				BtnCancelActive.Enabled = true;
				TxtTrial.Enabled = false;
				BtnTrial.Enabled = false;
			}
			if (TglType.IsOn)
			{
				TypeValue = 1;
				TxtNumDayes.Maximum = 5m;
				CKShowVirginInLogin.Checked = false;
				CKShowVirginInLogin.Enabled = false;
				NPermission.Value = 0m;
				NPermission.Enabled = false;
				TxtDateTo.Enabled = false;
				TxtDateTo.DateTime = FrmDate.AddDays(3.0);
				TxtKey1.Text = "b14ca5898a4e4133bbce2ea2315a1500";
				TxtSKey2.Text = "b14ca5898a4e4133bbce2ea2315a1916";
				TxtKey1.Enabled = false;
				TxtSKey2.Enabled = false;
				TxtActive.Enabled = false;
				BtnActive.Enabled = false;
				BtnOrderActive.Enabled = true;
				BtnCancelActive.Enabled = false;
				TxtTrial.Enabled = true;
				BtnTrial.Enabled = true;
				if (TxtNumCopy.Value > 3m)
				{
					ISActiveTrue = true;
				}
				else
				{
					ISActiveTrue = false;
				}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Virgins.FrmVirgin));
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnOrderSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnSuplierSearch = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnOrders = new DevExpress.XtraEditors.SimpleButton();
		this.BtnAddCustomer = new DevExpress.XtraEditors.SimpleButton();
		this.LblID = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtAddress = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtCommercialRecord = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtPhone = new System.Windows.Forms.TextBox();
		this.TxtName = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtVatNum = new System.Windows.Forms.TextBox();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtCharge = new DevExpress.XtraEditors.TextEdit();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.LblUser = new System.Windows.Forms.Label();
		this.BtnActive = new DevExpress.XtraEditors.SimpleButton();
		this.BtnTest = new DevExpress.XtraEditors.SimpleButton();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.TglType = new DevExpress.XtraEditors.ToggleSwitch();
		this.label5 = new System.Windows.Forms.Label();
		this.CbAppType = new System.Windows.Forms.ComboBox();
		this.TxtStatuse = new System.Windows.Forms.TextBox();
		this.BtnCancelActive = new DevExpress.XtraEditors.SimpleButton();
		this.BtnOrderActive = new DevExpress.XtraEditors.SimpleButton();
		this.BtnTrial = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCopyTrial = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTrial = new System.Windows.Forms.TextBox();
		this.label24 = new System.Windows.Forms.Label();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtDeviceName = new System.Windows.Forms.TextBox();
		this.CKShowVirginInLogin = new System.Windows.Forms.CheckBox();
		this.BtnCopyActive = new DevExpress.XtraEditors.SimpleButton();
		this.TxtActive = new System.Windows.Forms.TextBox();
		this.label21 = new System.Windows.Forms.Label();
		this.CBMarkter = new System.Windows.Forms.ComboBox();
		this.CKClosed = new System.Windows.Forms.CheckBox();
		this.label18 = new System.Windows.Forms.Label();
		this.CBCompany = new System.Windows.Forms.ComboBox();
		this.CKMessage = new System.Windows.Forms.CheckBox();
		this.label17 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.TxtNumMessage = new System.Windows.Forms.NumericUpDown();
		this.label20 = new System.Windows.Forms.Label();
		this.BtnPast = new DevExpress.XtraEditors.SimpleButton();
		this.label7 = new System.Windows.Forms.Label();
		this.TxtNumCopy = new System.Windows.Forms.NumericUpDown();
		this.label6 = new System.Windows.Forms.Label();
		this.TxtDateTo = new DevExpress.XtraEditors.DateEdit();
		this.label27 = new System.Windows.Forms.Label();
		this.TxtDateFrom = new DevExpress.XtraEditors.DateEdit();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtSerial = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.CBType = new System.Windows.Forms.ComboBox();
		this.CBVirgin = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtNumDayes = new System.Windows.Forms.NumericUpDown();
		this.TxtDateVirg = new System.Windows.Forms.TextBox();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtDateDay = new DevExpress.XtraEditors.DateEdit();
		this.label22 = new System.Windows.Forms.Label();
		this.NPermission = new System.Windows.Forms.NumericUpDown();
		this.TxtKey1 = new System.Windows.Forms.TextBox();
		this.label25 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.TxtSKey2 = new System.Windows.Forms.TextBox();
		this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
		this.SerialNumber = new System.Windows.Forms.Label();
		this.TxtSerialBoard = new System.Windows.Forms.TextBox();
		this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
		this.TxtHDD = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtMac = new System.Windows.Forms.TextBox();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.TxtEncrActiveTest = new System.Windows.Forms.TextBox();
		this.BtnPastTestActive = new DevExpress.XtraEditors.SimpleButton();
		this.TxtActiveTest = new System.Windows.Forms.TextBox();
		this.LblMasterDetailsSN = new System.Windows.Forms.Label();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BtnSuplierSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TglType.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumMessage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumCopy).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateTo.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateTo.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateFrom.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateFrom.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumDayes).BeginInit();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDateDay.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateDay.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPermission).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).BeginInit();
		this.tabPage3.SuspendLayout();
		base.SuspendLayout();
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(1066, 705);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(195, 40);
		this.BtnExit.TabIndex = 29;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.groupBox2.Controls.Add(this.BtnOrderSearch);
		this.groupBox2.Controls.Add(this.BtnSuplierSearch);
		this.groupBox2.Controls.Add(this.BtnOrders);
		this.groupBox2.Controls.Add(this.BtnAddCustomer);
		this.groupBox2.Controls.Add(this.LblID);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.TxtAddress);
		this.groupBox2.Controls.Add(this.label19);
		this.groupBox2.Controls.Add(this.TxtCommercialRecord);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.TxtPhone);
		this.groupBox2.Controls.Add(this.TxtName);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.TxtVatNum);
		this.groupBox2.Controls.Add(this.TxtCode);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Location = new System.Drawing.Point(13, 13);
		this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.groupBox2.Size = new System.Drawing.Size(531, 223);
		this.groupBox2.TabIndex = 31;
		this.groupBox2.TabStop = false;
		this.BtnOrderSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.BtnOrderSearch.EditValue = "";
		this.BtnOrderSearch.Location = new System.Drawing.Point(417, 49);
		this.BtnOrderSearch.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.BtnOrderSearch.Name = "BtnOrderSearch";
		this.BtnOrderSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnOrderSearch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnOrderSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnOrderSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnOrderSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnOrderSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnOrderSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnOrderSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
		});
		this.BtnOrderSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnOrderSearch.Properties.NullText = "بحث";
		this.BtnOrderSearch.Size = new System.Drawing.Size(74, 28);
		this.BtnOrderSearch.TabIndex = 207;
		this.BtnOrderSearch.Tag = "بحث بالكود";
		this.BtnOrderSearch.Visible = false;
		this.BtnOrderSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnOrderSearch_ButtonClick);
		this.BtnSuplierSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSuplierSearch.EditValue = "";
		this.BtnSuplierSearch.Location = new System.Drawing.Point(239, 22);
		this.BtnSuplierSearch.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.BtnSuplierSearch.Name = "BtnSuplierSearch";
		this.BtnSuplierSearch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.BtnSuplierSearch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSuplierSearch.Properties.Appearance.Options.UseBackColor = true;
		this.BtnSuplierSearch.Properties.Appearance.Options.UseFont = true;
		this.BtnSuplierSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.BtnSuplierSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.BtnSuplierSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnSuplierSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
		});
		this.BtnSuplierSearch.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
		this.BtnSuplierSearch.Properties.NullText = "بحث";
		this.BtnSuplierSearch.Size = new System.Drawing.Size(65, 28);
		this.BtnSuplierSearch.TabIndex = 206;
		this.BtnSuplierSearch.Tag = "بحث بالكود";
		this.BtnSuplierSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(BtnSuplierSearch_ButtonClick);
		this.BtnSuplierSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(BtnSuplierSearch_KeyDown);
		this.BtnSuplierSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(BtnSuplierSearch_KeyPress);
		this.BtnOrders.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnOrders.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnOrders.Location = new System.Drawing.Point(8, 23);
		this.BtnOrders.Margin = new System.Windows.Forms.Padding(4);
		this.BtnOrders.Name = "BtnOrders";
		this.BtnOrders.Size = new System.Drawing.Size(184, 28);
		this.BtnOrders.TabIndex = 205;
		this.BtnOrders.Text = "الطلبات";
		this.BtnOrders.Click += new System.EventHandler(BtnOrders_Click);
		this.BtnAddCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAddCustomer.ImageOptions.Image");
		this.BtnAddCustomer.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnAddCustomer.Location = new System.Drawing.Point(198, 25);
		this.BtnAddCustomer.Margin = new System.Windows.Forms.Padding(4);
		this.BtnAddCustomer.Name = "BtnAddCustomer";
		this.BtnAddCustomer.Size = new System.Drawing.Size(35, 23);
		this.BtnAddCustomer.TabIndex = 202;
		this.BtnAddCustomer.Click += new System.EventHandler(BtnAddCustomer_Click);
		this.LblID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblID.Location = new System.Drawing.Point(492, 26);
		this.LblID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblID.Name = "LblID";
		this.LblID.Size = new System.Drawing.Size(34, 23);
		this.LblID.TabIndex = 191;
		this.LblID.Text = "0";
		this.LblID.Visible = false;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 9.25f);
		this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label13.Location = new System.Drawing.Point(424, 190);
		this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(54, 19);
		this.label13.TabIndex = 190;
		this.label13.Text = "العنوان";
		this.TxtAddress.BackColor = System.Drawing.Color.White;
		this.TxtAddress.ForeColor = System.Drawing.Color.Blue;
		this.TxtAddress.Location = new System.Drawing.Point(8, 185);
		this.TxtAddress.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.ReadOnly = true;
		this.TxtAddress.Size = new System.Drawing.Size(409, 23);
		this.TxtAddress.TabIndex = 189;
		this.TxtAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label19.Location = new System.Drawing.Point(426, 126);
		this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label19.Name = "label19";
		this.label19.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label19.Size = new System.Drawing.Size(107, 23);
		this.label19.TabIndex = 188;
		this.label19.Text = "السجل التجاري";
		this.TxtCommercialRecord.BackColor = System.Drawing.Color.White;
		this.TxtCommercialRecord.ForeColor = System.Drawing.Color.Blue;
		this.TxtCommercialRecord.Location = new System.Drawing.Point(8, 123);
		this.TxtCommercialRecord.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCommercialRecord.Name = "TxtCommercialRecord";
		this.TxtCommercialRecord.ReadOnly = true;
		this.TxtCommercialRecord.Size = new System.Drawing.Size(410, 23);
		this.TxtCommercialRecord.TabIndex = 187;
		this.TxtCommercialRecord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label8.AutoSize = true;
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(427, 64);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label8.Size = new System.Drawing.Size(71, 16);
		this.label8.TabIndex = 184;
		this.label8.Text = "إسم العميل";
		this.label10.AutoSize = true;
		this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label10.Location = new System.Drawing.Point(428, 94);
		this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label10.Name = "label10";
		this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label10.Size = new System.Drawing.Size(40, 16);
		this.label10.TabIndex = 186;
		this.label10.Text = "الجوال";
		this.TxtPhone.BackColor = System.Drawing.Color.White;
		this.TxtPhone.ForeColor = System.Drawing.Color.Blue;
		this.TxtPhone.Location = new System.Drawing.Point(8, 90);
		this.TxtPhone.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.ReadOnly = true;
		this.TxtPhone.Size = new System.Drawing.Size(410, 23);
		this.TxtPhone.TabIndex = 183;
		this.TxtPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.TxtName.BackColor = System.Drawing.Color.White;
		this.TxtName.ForeColor = System.Drawing.Color.Blue;
		this.TxtName.Location = new System.Drawing.Point(8, 58);
		this.TxtName.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtName.Name = "TxtName";
		this.TxtName.ReadOnly = true;
		this.TxtName.Size = new System.Drawing.Size(409, 23);
		this.TxtName.TabIndex = 182;
		this.TxtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label9.AutoSize = true;
		this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label9.Location = new System.Drawing.Point(425, 158);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(81, 16);
		this.label9.TabIndex = 181;
		this.label9.Text = "الرقم الضريبى";
		this.TxtVatNum.BackColor = System.Drawing.Color.White;
		this.TxtVatNum.ForeColor = System.Drawing.Color.Blue;
		this.TxtVatNum.Location = new System.Drawing.Point(8, 154);
		this.TxtVatNum.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtVatNum.Name = "TxtVatNum";
		this.TxtVatNum.ReadOnly = true;
		this.TxtVatNum.Size = new System.Drawing.Size(410, 23);
		this.TxtVatNum.TabIndex = 179;
		this.TxtVatNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtCode.Location = new System.Drawing.Point(308, 23);
		this.TxtCode.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtCode.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
		this.TxtCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCode.Properties.Appearance.Options.UseFont = true;
		this.TxtCode.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtCode.Properties.AppearanceDisabled.Options.UseTextOptions = true;
		this.TxtCode.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCode.Properties.ReadOnly = true;
		this.TxtCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.TxtCode.Size = new System.Drawing.Size(110, 26);
		this.TxtCode.TabIndex = 185;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.label12.AutoSize = true;
		this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label12.Location = new System.Drawing.Point(427, 31);
		this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label12.Name = "label12";
		this.label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label12.Size = new System.Drawing.Size(64, 16);
		this.label12.TabIndex = 180;
		this.label12.Text = "كود العميل";
		this.TxtCharge.EditValue = "0.00";
		this.TxtCharge.Enabled = false;
		this.TxtCharge.Location = new System.Drawing.Point(892, 712);
		this.TxtCharge.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCharge.Name = "TxtCharge";
		this.TxtCharge.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.TxtCharge.Properties.Appearance.Options.UseFont = true;
		this.TxtCharge.Properties.Appearance.Options.UseForeColor = true;
		this.TxtCharge.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCharge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCharge.Size = new System.Drawing.Size(99, 28);
		this.TxtCharge.TabIndex = 203;
		this.TxtCharge.Visible = false;
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox3.Controls.Add(this.gridEX1);
		this.groupBox3.Location = new System.Drawing.Point(13, 244);
		this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.groupBox3.Size = new System.Drawing.Size(531, 453);
		this.groupBox3.TabIndex = 32;
		this.groupBox3.TabStop = false;
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridEX1.Font = new System.Drawing.Font("Tahoma", 9f);
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Location = new System.Drawing.Point(8, 23);
		this.gridEX1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.Size = new System.Drawing.Size(515, 424);
		this.gridEX1.TabIndex = 33;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX1.MouseClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseClick_1);
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(13, 705);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(260, 40);
		this.BtnNew.TabIndex = 33;
		this.BtnNew.Text = "جديد";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.LblUser.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.LblUser.AutoSize = true;
		this.LblUser.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblUser.ForeColor = System.Drawing.Color.Green;
		this.LblUser.Location = new System.Drawing.Point(448, 715);
		this.LblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblUser.Name = "LblUser";
		this.LblUser.Size = new System.Drawing.Size(45, 21);
		this.LblUser.TabIndex = 198;
		this.LblUser.Text = ".......";
		this.BtnActive.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnActive.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnActive.Location = new System.Drawing.Point(607, 590);
		this.BtnActive.Margin = new System.Windows.Forms.Padding(4);
		this.BtnActive.Name = "BtnActive";
		this.BtnActive.Size = new System.Drawing.Size(87, 27);
		this.BtnActive.TabIndex = 33;
		this.BtnActive.Text = "قبول";
		this.BtnActive.Click += new System.EventHandler(BtnActive_Click);
		this.BtnTest.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnTest.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnTest.Location = new System.Drawing.Point(657, 705);
		this.BtnTest.Margin = new System.Windows.Forms.Padding(4);
		this.BtnTest.Name = "BtnTest";
		this.BtnTest.Size = new System.Drawing.Size(195, 40);
		this.BtnTest.TabIndex = 202;
		this.BtnTest.Text = "بحث عن السيريال";
		this.BtnTest.Click += new System.EventHandler(BtnTest_Click);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Location = new System.Drawing.Point(551, 12);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.RightToLeftLayout = true;
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(711, 686);
		this.tabControl1.TabIndex = 202;
		this.tabPage1.Controls.Add(this.TglType);
		this.tabPage1.Controls.Add(this.label5);
		this.tabPage1.Controls.Add(this.CbAppType);
		this.tabPage1.Controls.Add(this.TxtStatuse);
		this.tabPage1.Controls.Add(this.BtnCancelActive);
		this.tabPage1.Controls.Add(this.BtnOrderActive);
		this.tabPage1.Controls.Add(this.BtnTrial);
		this.tabPage1.Controls.Add(this.BtnCopyTrial);
		this.tabPage1.Controls.Add(this.TxtTrial);
		this.tabPage1.Controls.Add(this.BtnActive);
		this.tabPage1.Controls.Add(this.label24);
		this.tabPage1.Controls.Add(this.TxtNote);
		this.tabPage1.Controls.Add(this.label23);
		this.tabPage1.Controls.Add(this.TxtDeviceName);
		this.tabPage1.Controls.Add(this.CKShowVirginInLogin);
		this.tabPage1.Controls.Add(this.BtnCopyActive);
		this.tabPage1.Controls.Add(this.TxtActive);
		this.tabPage1.Controls.Add(this.label21);
		this.tabPage1.Controls.Add(this.CBMarkter);
		this.tabPage1.Controls.Add(this.CKClosed);
		this.tabPage1.Controls.Add(this.label18);
		this.tabPage1.Controls.Add(this.CBCompany);
		this.tabPage1.Controls.Add(this.CKMessage);
		this.tabPage1.Controls.Add(this.label17);
		this.tabPage1.Controls.Add(this.label16);
		this.tabPage1.Controls.Add(this.TxtNumMessage);
		this.tabPage1.Controls.Add(this.label20);
		this.tabPage1.Controls.Add(this.BtnPast);
		this.tabPage1.Controls.Add(this.label7);
		this.tabPage1.Controls.Add(this.TxtNumCopy);
		this.tabPage1.Controls.Add(this.label6);
		this.tabPage1.Controls.Add(this.TxtDateTo);
		this.tabPage1.Controls.Add(this.label27);
		this.tabPage1.Controls.Add(this.TxtDateFrom);
		this.tabPage1.Controls.Add(this.label11);
		this.tabPage1.Controls.Add(this.TxtSerial);
		this.tabPage1.Controls.Add(this.label14);
		this.tabPage1.Controls.Add(this.label4);
		this.tabPage1.Controls.Add(this.CBType);
		this.tabPage1.Controls.Add(this.CBVirgin);
		this.tabPage1.Controls.Add(this.label3);
		this.tabPage1.Controls.Add(this.TxtNumDayes);
		this.tabPage1.Controls.Add(this.TxtDateVirg);
		this.tabPage1.Location = new System.Drawing.Point(4, 25);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(703, 657);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "بيانات السيريال";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.TglType.Location = new System.Drawing.Point(124, 131);
		this.TglType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TglType.Name = "TglType";
		this.TglType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TglType.Properties.Appearance.Options.UseFont = true;
		this.TglType.Properties.Appearance.Options.UseForeColor = true;
		this.TglType.Properties.OffText = "تجريبي";
		this.TglType.Properties.OnText = "مرخص";
		this.TglType.Properties.Click += new System.EventHandler(toggleHold_Properties_Click);
		this.TglType.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TglType.Size = new System.Drawing.Size(164, 25);
		this.TglType.TabIndex = 264;
		this.TglType.Toggled += new System.EventHandler(TglType_Toggled);
		this.TglType.Click += new System.EventHandler(TglType_Click);
		this.label5.AutoSize = true;
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(609, 106);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label5.Size = new System.Drawing.Size(70, 16);
		this.label5.TabIndex = 263;
		this.label5.Text = "نوع البرنامج";
		this.CbAppType.BackColor = System.Drawing.Color.White;
		this.CbAppType.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.CbAppType.FormattingEnabled = true;
		this.CbAppType.Location = new System.Drawing.Point(355, 103);
		this.CbAppType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CbAppType.Name = "CbAppType";
		this.CbAppType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.CbAppType.Size = new System.Drawing.Size(246, 24);
		this.CbAppType.TabIndex = 262;
		this.TxtStatuse.BackColor = System.Drawing.Color.White;
		this.TxtStatuse.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtStatuse.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtStatuse.Location = new System.Drawing.Point(7, 522);
		this.TxtStatuse.Margin = new System.Windows.Forms.Padding(4);
		this.TxtStatuse.Name = "TxtStatuse";
		this.TxtStatuse.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtStatuse.Size = new System.Drawing.Size(594, 28);
		this.TxtStatuse.TabIndex = 261;
		this.TxtStatuse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.BtnCancelActive.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnCancelActive.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCancelActive.Location = new System.Drawing.Point(607, 556);
		this.BtnCancelActive.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCancelActive.Name = "BtnCancelActive";
		this.BtnCancelActive.Size = new System.Drawing.Size(87, 27);
		this.BtnCancelActive.TabIndex = 260;
		this.BtnCancelActive.Text = "رفض";
		this.BtnCancelActive.Click += new System.EventHandler(BtnCancelActive_Click);
		this.BtnOrderActive.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnOrderActive.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnOrderActive.Location = new System.Drawing.Point(607, 523);
		this.BtnOrderActive.Margin = new System.Windows.Forms.Padding(4);
		this.BtnOrderActive.Name = "BtnOrderActive";
		this.BtnOrderActive.Size = new System.Drawing.Size(87, 27);
		this.BtnOrderActive.TabIndex = 260;
		this.BtnOrderActive.Text = "طلب تفعيل";
		this.BtnOrderActive.Click += new System.EventHandler(BtnOrderActive_Click);
		this.BtnTrial.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnTrial.Location = new System.Drawing.Point(609, 442);
		this.BtnTrial.Margin = new System.Windows.Forms.Padding(4);
		this.BtnTrial.Name = "BtnTrial";
		this.BtnTrial.Size = new System.Drawing.Size(87, 27);
		this.BtnTrial.TabIndex = 259;
		this.BtnTrial.Text = "تجريبي";
		this.BtnTrial.Click += new System.EventHandler(BtnTrial_Click);
		this.BtnCopyTrial.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCopyTrial.Location = new System.Drawing.Point(609, 477);
		this.BtnCopyTrial.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCopyTrial.Name = "BtnCopyTrial";
		this.BtnCopyTrial.Size = new System.Drawing.Size(87, 27);
		this.BtnCopyTrial.TabIndex = 258;
		this.BtnCopyTrial.Text = "نسخ";
		this.BtnCopyTrial.Click += new System.EventHandler(BtnCopyTrial_Click);
		this.TxtTrial.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtTrial.BackColor = System.Drawing.Color.White;
		this.TxtTrial.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtTrial.Location = new System.Drawing.Point(7, 442);
		this.TxtTrial.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTrial.Multiline = true;
		this.TxtTrial.Name = "TxtTrial";
		this.TxtTrial.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtTrial.Size = new System.Drawing.Size(594, 74);
		this.TxtTrial.TabIndex = 256;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Tahoma", 9.25f);
		this.label24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label24.Location = new System.Drawing.Point(609, 371);
		this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(68, 19);
		this.label24.TabIndex = 244;
		this.label24.Text = "ملاحظات";
		this.TxtNote.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtNote.BackColor = System.Drawing.Color.White;
		this.TxtNote.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtNote.Location = new System.Drawing.Point(7, 370);
		this.TxtNote.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtNote.Multiline = true;
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtNote.Size = new System.Drawing.Size(594, 68);
		this.TxtNote.TabIndex = 243;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label23.Location = new System.Drawing.Point(609, 343);
		this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(73, 17);
		this.label23.TabIndex = 242;
		this.label23.Text = "اسم الجهاز";
		this.TxtDeviceName.BackColor = System.Drawing.Color.White;
		this.TxtDeviceName.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDeviceName.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtDeviceName.Location = new System.Drawing.Point(7, 336);
		this.TxtDeviceName.Margin = new System.Windows.Forms.Padding(4);
		this.TxtDeviceName.Name = "TxtDeviceName";
		this.TxtDeviceName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDeviceName.Size = new System.Drawing.Size(594, 28);
		this.TxtDeviceName.TabIndex = 241;
		this.TxtDeviceName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.CKShowVirginInLogin.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CKShowVirginInLogin.AutoSize = true;
		this.CKShowVirginInLogin.Enabled = false;
		this.CKShowVirginInLogin.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.CKShowVirginInLogin.Location = new System.Drawing.Point(52, 308);
		this.CKShowVirginInLogin.Name = "CKShowVirginInLogin";
		this.CKShowVirginInLogin.Size = new System.Drawing.Size(208, 20);
		this.CKShowVirginInLogin.TabIndex = 239;
		this.CKShowVirginInLogin.Text = "اظهار الفيرجن في شاشة الدخول";
		this.CKShowVirginInLogin.UseVisualStyleBackColor = true;
		this.BtnCopyActive.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCopyActive.Location = new System.Drawing.Point(607, 625);
		this.BtnCopyActive.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCopyActive.Name = "BtnCopyActive";
		this.BtnCopyActive.Size = new System.Drawing.Size(87, 27);
		this.BtnCopyActive.TabIndex = 236;
		this.BtnCopyActive.Text = "نسخ";
		this.BtnCopyActive.Click += new System.EventHandler(BtnCopy_Click);
		this.TxtActive.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtActive.BackColor = System.Drawing.Color.White;
		this.TxtActive.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtActive.Location = new System.Drawing.Point(5, 556);
		this.TxtActive.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtActive.Multiline = true;
		this.TxtActive.Name = "TxtActive";
		this.TxtActive.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtActive.Size = new System.Drawing.Size(594, 96);
		this.TxtActive.TabIndex = 237;
		this.label21.AutoSize = true;
		this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label21.Location = new System.Drawing.Point(609, 309);
		this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label21.Name = "label21";
		this.label21.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label21.Size = new System.Drawing.Size(53, 16);
		this.label21.TabIndex = 235;
		this.label21.Text = "المسوق";
		this.CBMarkter.BackColor = System.Drawing.Color.White;
		this.CBMarkter.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.CBMarkter.FormattingEnabled = true;
		this.CBMarkter.Location = new System.Drawing.Point(355, 309);
		this.CBMarkter.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBMarkter.Name = "CBMarkter";
		this.CBMarkter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.CBMarkter.Size = new System.Drawing.Size(246, 24);
		this.CBMarkter.TabIndex = 234;
		this.CKClosed.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CKClosed.AutoSize = true;
		this.CKClosed.Checked = true;
		this.CKClosed.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CKClosed.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.CKClosed.Location = new System.Drawing.Point(25, 276);
		this.CKClosed.Name = "CKClosed";
		this.CKClosed.Size = new System.Drawing.Size(235, 20);
		this.CKClosed.TabIndex = 233;
		this.CKClosed.Text = "ايقاف تشغيل البرنامج عند انتهاء الفترة";
		this.CKClosed.UseVisualStyleBackColor = true;
		this.label18.AutoSize = true;
		this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label18.Location = new System.Drawing.Point(268, 106);
		this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label18.Name = "label18";
		this.label18.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label18.Size = new System.Drawing.Size(46, 16);
		this.label18.TabIndex = 232;
		this.label18.Text = "الشركة";
		this.CBCompany.BackColor = System.Drawing.Color.White;
		this.CBCompany.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.CBCompany.FormattingEnabled = true;
		this.CBCompany.Location = new System.Drawing.Point(7, 103);
		this.CBCompany.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBCompany.Name = "CBCompany";
		this.CBCompany.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.CBCompany.Size = new System.Drawing.Size(253, 24);
		this.CBCompany.TabIndex = 231;
		this.CKMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CKMessage.AutoSize = true;
		this.CKMessage.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.CKMessage.Location = new System.Drawing.Point(35, 242);
		this.CKMessage.Name = "CKMessage";
		this.CKMessage.Size = new System.Drawing.Size(225, 20);
		this.CKMessage.TabIndex = 230;
		this.CKMessage.Text = "اظهار الرسالة عند انتهاء فترة التفعيل";
		this.CKMessage.UseVisualStyleBackColor = true;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(353, 277);
		this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(37, 16);
		this.label17.TabIndex = 229;
		this.label17.Text = "دقيقة";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(606, 277);
		this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(59, 16);
		this.label16.TabIndex = 228;
		this.label16.Text = "رسالة كل";
		this.TxtNumMessage.BackColor = System.Drawing.Color.White;
		this.TxtNumMessage.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNumMessage.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtNumMessage.Location = new System.Drawing.Point(401, 271);
		this.TxtNumMessage.Maximum = new decimal(new int[4] { 1440, 0, 0, 0 });
		this.TxtNumMessage.Name = "TxtNumMessage";
		this.TxtNumMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNumMessage.Size = new System.Drawing.Size(198, 28);
		this.TxtNumMessage.TabIndex = 227;
		this.TxtNumMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label20.Location = new System.Drawing.Point(607, 140);
		this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(78, 17);
		this.label20.TabIndex = 226;
		this.label20.Text = "تاريخ الاصدار";
		this.BtnPast.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPast.ImageOptions.Image");
		this.BtnPast.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPast.Location = new System.Drawing.Point(609, 50);
		this.BtnPast.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPast.Name = "BtnPast";
		this.BtnPast.Size = new System.Drawing.Size(75, 40);
		this.BtnPast.TabIndex = 209;
		this.BtnPast.Text = "لصق";
		this.BtnPast.Click += new System.EventHandler(BtnPast_Click);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(267, 207);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(71, 16);
		this.label7.TabIndex = 222;
		this.label7.Text = "عدد التفعيل";
		this.TxtNumCopy.BackColor = System.Drawing.Color.White;
		this.TxtNumCopy.Enabled = false;
		this.TxtNumCopy.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNumCopy.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtNumCopy.Location = new System.Drawing.Point(7, 203);
		this.TxtNumCopy.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.TxtNumCopy.Name = "TxtNumCopy";
		this.TxtNumCopy.ReadOnly = true;
		this.TxtNumCopy.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNumCopy.Size = new System.Drawing.Size(253, 28);
		this.TxtNumCopy.TabIndex = 221;
		this.TxtNumCopy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtNumCopy.ValueChanged += new System.EventHandler(TxtNumCopy_ValueChanged);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(268, 173);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(60, 17);
		this.label6.TabIndex = 219;
		this.label6.Text = "الي تاريخ";
		this.TxtDateTo.EditValue = null;
		this.TxtDateTo.Enabled = false;
		this.TxtDateTo.Location = new System.Drawing.Point(7, 166);
		this.TxtDateTo.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDateTo.Name = "TxtDateTo";
		this.TxtDateTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateTo.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDateTo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateTo.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtDateTo.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDateTo.Properties.Appearance.Options.UseFont = true;
		this.TxtDateTo.Properties.Appearance.Options.UseForeColor = true;
		this.TxtDateTo.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateTo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateTo.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateTo.Properties.DisplayFormat.FormatString = "";
		this.TxtDateTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateTo.Properties.EditFormat.FormatString = "";
		this.TxtDateTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateTo.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDateTo.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDateTo.Size = new System.Drawing.Size(253, 28);
		this.TxtDateTo.TabIndex = 218;
		this.TxtDateTo.EditValueChanged += new System.EventHandler(TxtDateTo_EditValueChanged);
		this.TxtDateTo.KeyUp += new System.Windows.Forms.KeyEventHandler(TxtDateTo_KeyUp);
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label27.Location = new System.Drawing.Point(607, 207);
		this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(79, 17);
		this.label27.TabIndex = 216;
		this.label27.Text = "تاريخ الانشاء";
		this.TxtDateFrom.EditValue = null;
		this.TxtDateFrom.Location = new System.Drawing.Point(355, 200);
		this.TxtDateFrom.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDateFrom.Name = "TxtDateFrom";
		this.TxtDateFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateFrom.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDateFrom.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateFrom.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtDateFrom.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDateFrom.Properties.Appearance.Options.UseFont = true;
		this.TxtDateFrom.Properties.Appearance.Options.UseForeColor = true;
		this.TxtDateFrom.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateFrom.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateFrom.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateFrom.Properties.DisplayFormat.FormatString = "";
		this.TxtDateFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateFrom.Properties.EditFormat.FormatString = "";
		this.TxtDateFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateFrom.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDateFrom.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDateFrom.Properties.ReadOnly = true;
		this.TxtDateFrom.Size = new System.Drawing.Size(244, 28);
		this.TxtDateFrom.TabIndex = 215;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 9.25f);
		this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label11.Location = new System.Drawing.Point(609, 15);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(67, 19);
		this.label11.TabIndex = 212;
		this.label11.Text = "السيريال";
		this.TxtSerial.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtSerial.BackColor = System.Drawing.Color.White;
		this.TxtSerial.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtSerial.Location = new System.Drawing.Point(7, 14);
		this.TxtSerial.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtSerial.Multiline = true;
		this.TxtSerial.Name = "TxtSerial";
		this.TxtSerial.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtSerial.Size = new System.Drawing.Size(594, 76);
		this.TxtSerial.TabIndex = 211;
		this.TxtSerial.TextChanged += new System.EventHandler(TxtSerial_TextChanged);
		this.label14.AutoSize = true;
		this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label14.Location = new System.Drawing.Point(277, 283);
		this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label14.Name = "label14";
		this.label14.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label14.Size = new System.Drawing.Size(68, 16);
		this.label14.TabIndex = 208;
		this.label14.Text = "نوع التفعيل";
		this.label14.Visible = false;
		this.label4.AutoSize = true;
		this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label4.Location = new System.Drawing.Point(607, 171);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label4.Size = new System.Drawing.Size(50, 16);
		this.label4.TabIndex = 207;
		this.label4.Text = "الفيرجن";
		this.CBType.BackColor = System.Drawing.Color.White;
		this.CBType.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.CBType.FormattingEnabled = true;
		this.CBType.Location = new System.Drawing.Point(278, 308);
		this.CBType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBType.Name = "CBType";
		this.CBType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.CBType.Size = new System.Drawing.Size(57, 24);
		this.CBType.TabIndex = 206;
		this.CBType.Visible = false;
		this.CBType.SelectedIndexChanged += new System.EventHandler(CBType_SelectedIndexChanged);
		this.CBVirgin.BackColor = System.Drawing.Color.White;
		this.CBVirgin.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.CBVirgin.FormattingEnabled = true;
		this.CBVirgin.Location = new System.Drawing.Point(355, 168);
		this.CBVirgin.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CBVirgin.Name = "CBVirgin";
		this.CBVirgin.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.CBVirgin.Size = new System.Drawing.Size(244, 24);
		this.CBVirgin.TabIndex = 205;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(611, 241);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(59, 16);
		this.label3.TabIndex = 204;
		this.label3.Text = "عدد الايام";
		this.TxtNumDayes.BackColor = System.Drawing.Color.White;
		this.TxtNumDayes.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtNumDayes.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtNumDayes.Location = new System.Drawing.Point(355, 237);
		this.TxtNumDayes.Maximum = new decimal(new int[4] { 5, 0, 0, 0 });
		this.TxtNumDayes.Name = "TxtNumDayes";
		this.TxtNumDayes.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNumDayes.Size = new System.Drawing.Size(244, 28);
		this.TxtNumDayes.TabIndex = 203;
		this.TxtNumDayes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtNumDayes.Value = new decimal(new int[4] { 3, 0, 0, 0 });
		this.TxtNumDayes.ValueChanged += new System.EventHandler(TxtNumDayes_ValueChanged);
		this.TxtDateVirg.BackColor = System.Drawing.Color.White;
		this.TxtDateVirg.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateVirg.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtDateVirg.Location = new System.Drawing.Point(355, 133);
		this.TxtDateVirg.Margin = new System.Windows.Forms.Padding(4);
		this.TxtDateVirg.Name = "TxtDateVirg";
		this.TxtDateVirg.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDateVirg.Size = new System.Drawing.Size(244, 28);
		this.TxtDateVirg.TabIndex = 202;
		this.TxtDateVirg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tabPage2.Controls.Add(this.label15);
		this.tabPage2.Controls.Add(this.TxtDateDay);
		this.tabPage2.Controls.Add(this.label22);
		this.tabPage2.Controls.Add(this.NPermission);
		this.tabPage2.Controls.Add(this.TxtKey1);
		this.tabPage2.Controls.Add(this.label25);
		this.tabPage2.Controls.Add(this.label26);
		this.tabPage2.Controls.Add(this.TxtSKey2);
		this.tabPage2.Controls.Add(this.separatorControl3);
		this.tabPage2.Controls.Add(this.SerialNumber);
		this.tabPage2.Controls.Add(this.TxtSerialBoard);
		this.tabPage2.Controls.Add(this.separatorControl2);
		this.tabPage2.Controls.Add(this.TxtHDD);
		this.tabPage2.Controls.Add(this.label1);
		this.tabPage2.Controls.Add(this.label2);
		this.tabPage2.Controls.Add(this.TxtMac);
		this.tabPage2.Location = new System.Drawing.Point(4, 25);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(703, 657);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "بيانات الجهاز";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label15.Location = new System.Drawing.Point(259, 318);
		this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(80, 17);
		this.label15.TabIndex = 257;
		this.label15.Text = "تاريخ التفعيل";
		this.TxtDateDay.EditValue = null;
		this.TxtDateDay.Location = new System.Drawing.Point(7, 311);
		this.TxtDateDay.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDateDay.Name = "TxtDateDay";
		this.TxtDateDay.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateDay.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtDateDay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDateDay.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtDateDay.Properties.Appearance.Options.UseBackColor = true;
		this.TxtDateDay.Properties.Appearance.Options.UseFont = true;
		this.TxtDateDay.Properties.Appearance.Options.UseForeColor = true;
		this.TxtDateDay.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateDay.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateDay.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateDay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateDay.Properties.DisplayFormat.FormatString = "";
		this.TxtDateDay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateDay.Properties.EditFormat.FormatString = "";
		this.TxtDateDay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateDay.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDateDay.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDateDay.Properties.ReadOnly = true;
		this.TxtDateDay.Size = new System.Drawing.Size(244, 28);
		this.TxtDateDay.TabIndex = 256;
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(624, 318);
		this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(62, 16);
		this.label22.TabIndex = 251;
		this.label22.Text = "الصلاحيات";
		this.NPermission.BackColor = System.Drawing.Color.White;
		this.NPermission.Enabled = false;
		this.NPermission.Font = new System.Drawing.Font("Tahoma", 10f);
		this.NPermission.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.NPermission.Location = new System.Drawing.Point(364, 312);
		this.NPermission.Maximum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.NPermission.Name = "NPermission";
		this.NPermission.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.NPermission.Size = new System.Drawing.Size(253, 28);
		this.NPermission.TabIndex = 250;
		this.NPermission.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtKey1.BackColor = System.Drawing.Color.White;
		this.TxtKey1.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtKey1.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtKey1.Location = new System.Drawing.Point(7, 230);
		this.TxtKey1.Margin = new System.Windows.Forms.Padding(4);
		this.TxtKey1.Name = "TxtKey1";
		this.TxtKey1.PasswordChar = '*';
		this.TxtKey1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtKey1.Size = new System.Drawing.Size(610, 28);
		this.TxtKey1.TabIndex = 246;
		this.TxtKey1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label25.AutoSize = true;
		this.label25.Location = new System.Drawing.Point(625, 273);
		this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(42, 16);
		this.label25.TabIndex = 248;
		this.label25.Text = "SKey2";
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(625, 237);
		this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(34, 16);
		this.label26.TabIndex = 249;
		this.label26.Text = "Key1";
		this.TxtSKey2.BackColor = System.Drawing.Color.White;
		this.TxtSKey2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSKey2.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtSKey2.Location = new System.Drawing.Point(7, 266);
		this.TxtSKey2.Margin = new System.Windows.Forms.Padding(4);
		this.TxtSKey2.Name = "TxtSKey2";
		this.TxtSKey2.PasswordChar = '*';
		this.TxtSKey2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtSKey2.Size = new System.Drawing.Size(610, 28);
		this.TxtSKey2.TabIndex = 247;
		this.TxtSKey2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.separatorControl3.LineThickness = 1;
		this.separatorControl3.Location = new System.Drawing.Point(7, 54);
		this.separatorControl3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.separatorControl3.Name = "separatorControl3";
		this.separatorControl3.Size = new System.Drawing.Size(610, 35);
		this.separatorControl3.TabIndex = 230;
		this.SerialNumber.AutoSize = true;
		this.SerialNumber.Location = new System.Drawing.Point(625, 174);
		this.SerialNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.SerialNumber.Name = "SerialNumber";
		this.SerialNumber.Size = new System.Drawing.Size(66, 16);
		this.SerialNumber.TabIndex = 229;
		this.SerialNumber.Text = "SerialNum";
		this.TxtSerialBoard.BackColor = System.Drawing.Color.White;
		this.TxtSerialBoard.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSerialBoard.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtSerialBoard.Location = new System.Drawing.Point(7, 167);
		this.TxtSerialBoard.Margin = new System.Windows.Forms.Padding(4);
		this.TxtSerialBoard.Name = "TxtSerialBoard";
		this.TxtSerialBoard.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtSerialBoard.Size = new System.Drawing.Size(610, 28);
		this.TxtSerialBoard.TabIndex = 228;
		this.TxtSerialBoard.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.separatorControl2.LineThickness = 1;
		this.separatorControl2.Location = new System.Drawing.Point(7, 201);
		this.separatorControl2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.separatorControl2.Name = "separatorControl2";
		this.separatorControl2.Size = new System.Drawing.Size(610, 35);
		this.separatorControl2.TabIndex = 227;
		this.TxtHDD.BackColor = System.Drawing.Color.White;
		this.TxtHDD.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtHDD.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtHDD.Location = new System.Drawing.Point(7, 95);
		this.TxtHDD.Margin = new System.Windows.Forms.Padding(4);
		this.TxtHDD.Name = "TxtHDD";
		this.TxtHDD.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtHDD.Size = new System.Drawing.Size(610, 28);
		this.TxtHDD.TabIndex = 17;
		this.TxtHDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(625, 138);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(57, 16);
		this.label1.TabIndex = 19;
		this.label1.Text = "MACADD";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(625, 102);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(31, 16);
		this.label2.TabIndex = 20;
		this.label2.Text = "HDD";
		this.TxtMac.BackColor = System.Drawing.Color.White;
		this.TxtMac.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtMac.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtMac.Location = new System.Drawing.Point(7, 131);
		this.TxtMac.Margin = new System.Windows.Forms.Padding(4);
		this.TxtMac.Name = "TxtMac";
		this.TxtMac.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMac.Size = new System.Drawing.Size(610, 28);
		this.TxtMac.TabIndex = 18;
		this.TxtMac.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tabPage3.Controls.Add(this.textBox1);
		this.tabPage3.Controls.Add(this.simpleButton1);
		this.tabPage3.Controls.Add(this.textBox2);
		this.tabPage3.Controls.Add(this.TxtEncrActiveTest);
		this.tabPage3.Controls.Add(this.BtnPastTestActive);
		this.tabPage3.Controls.Add(this.TxtActiveTest);
		this.tabPage3.Location = new System.Drawing.Point(4, 25);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage3.Size = new System.Drawing.Size(703, 657);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "tabPage3";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.textBox1.BackColor = System.Drawing.Color.White;
		this.textBox1.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.textBox1.Location = new System.Drawing.Point(4, 409);
		this.textBox1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.textBox1.Size = new System.Drawing.Size(594, 221);
		this.textBox1.TabIndex = 218;
		this.simpleButton1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
		this.simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.simpleButton1.Location = new System.Drawing.Point(606, 327);
		this.simpleButton1.Margin = new System.Windows.Forms.Padding(4);
		this.simpleButton1.Name = "simpleButton1";
		this.simpleButton1.Size = new System.Drawing.Size(75, 40);
		this.simpleButton1.TabIndex = 216;
		this.simpleButton1.Text = "لصق";
		this.simpleButton1.Click += new System.EventHandler(simpleButton1_Click);
		this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.textBox2.BackColor = System.Drawing.Color.White;
		this.textBox2.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.textBox2.Location = new System.Drawing.Point(4, 324);
		this.textBox2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.textBox2.Multiline = true;
		this.textBox2.Name = "textBox2";
		this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.textBox2.Size = new System.Drawing.Size(594, 83);
		this.textBox2.TabIndex = 217;
		this.textBox2.TextChanged += new System.EventHandler(textBox2_TextChanged);
		this.TxtEncrActiveTest.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtEncrActiveTest.BackColor = System.Drawing.Color.White;
		this.TxtEncrActiveTest.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtEncrActiveTest.Location = new System.Drawing.Point(7, 99);
		this.TxtEncrActiveTest.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtEncrActiveTest.Multiline = true;
		this.TxtEncrActiveTest.Name = "TxtEncrActiveTest";
		this.TxtEncrActiveTest.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtEncrActiveTest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtEncrActiveTest.Size = new System.Drawing.Size(594, 221);
		this.TxtEncrActiveTest.TabIndex = 215;
		this.BtnPastTestActive.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPastTestActive.ImageOptions.Image");
		this.BtnPastTestActive.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPastTestActive.Location = new System.Drawing.Point(609, 17);
		this.BtnPastTestActive.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPastTestActive.Name = "BtnPastTestActive";
		this.BtnPastTestActive.Size = new System.Drawing.Size(75, 40);
		this.BtnPastTestActive.TabIndex = 212;
		this.BtnPastTestActive.Text = "لصق";
		this.BtnPastTestActive.Click += new System.EventHandler(BtnPastTestActive_Click);
		this.TxtActiveTest.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtActiveTest.BackColor = System.Drawing.Color.White;
		this.TxtActiveTest.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtActiveTest.Location = new System.Drawing.Point(7, 14);
		this.TxtActiveTest.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtActiveTest.Multiline = true;
		this.TxtActiveTest.Name = "TxtActiveTest";
		this.TxtActiveTest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.TxtActiveTest.Size = new System.Drawing.Size(594, 83);
		this.TxtActiveTest.TabIndex = 213;
		this.TxtActiveTest.TextChanged += new System.EventHandler(TxtActiveTest_TextChanged);
		this.LblMasterDetailsSN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblMasterDetailsSN.Location = new System.Drawing.Point(290, 719);
		this.LblMasterDetailsSN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblMasterDetailsSN.Name = "LblMasterDetailsSN";
		this.LblMasterDetailsSN.Size = new System.Drawing.Size(46, 23);
		this.LblMasterDetailsSN.TabIndex = 204;
		this.LblMasterDetailsSN.Text = "0";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1274, 758);
		base.Controls.Add(this.LblMasterDetailsSN);
		base.Controls.Add(this.TxtCharge);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.BtnTest);
		base.Controls.Add(this.LblUser);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.BtnExit);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmVirgin";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "توليد الرخصة";
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BtnOrderSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BtnSuplierSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCharge.Properties).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TglType.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumMessage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumCopy).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateTo.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateTo.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateFrom.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateFrom.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtNumDayes).EndInit();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDateDay.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateDay.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPermission).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).EndInit();
		this.tabPage3.ResumeLayout(false);
		this.tabPage3.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


