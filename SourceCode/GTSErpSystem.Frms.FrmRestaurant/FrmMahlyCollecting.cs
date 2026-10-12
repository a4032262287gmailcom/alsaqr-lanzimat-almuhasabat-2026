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
using DevExpress.XtraTab;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.DataSetRep;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.FrmRestaurant;

public class FrmMahlyCollecting : XtraForm
{
	public bool IsComplateMony = false;

	private GTSdbEntities db = new GTSdbEntities();

	private Class_OrdersReturn ORDRTN = new Class_OrdersReturn();

	public bool IsMoneyPay;

	public bool IsBtnOpen = false;

	public int Rom;

	private int DecimalNum = LoginDetails.DecimalNum;

	private IContainer components = null;

	private GroupBox lbl;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	public NumericEditBox TxtVisa;

	public NumericEditBox TxtCash;

	private ToggleSwitch toggleVisa;

	private ToggleSwitch toggleCash;

	private SeparatorControl separatorControl1;

	public NumericEditBox TxtPause;

	private Label label2;

	public NumericEditBox TxtMoneyOld;

	private XtraTabControl xtraTabControl1;

	private XtraTabPage xtraTabPage1;

	private TileControl TileRoom;

	private TileGroup GroupRoom;

	private XtraTabPage xtraTabPage2;

	private TileControl TileTable;

	private TileGroup GroupTable;

	public TextBox TxtRoom;

	public Label LblType;

	public ToggleSwitch TogglePause;

	public Label LblOrderID;

	public Label LblRoomID;

	public CheckBox CkPrintCook;

	public CheckBox CkPrintCheck;

	private Label label1;

	public NumericEditBox TxtDiscount;

	private SimpleButton BtnCalcVisa;

	private SimpleButton BtnCalcCash;

	private SimpleButton BtnCalcDisc;

	private Label label3;

	public NumericEditBox TxtReturnOrd;

	private Label label4;

	public Label LblOld;

	private SimpleButton BtnPerviewCashier;

	public Label LblDate;

	public FrmMahlyCollecting()
	{
		InitializeComponent();
		TxtDigital();
		IsComplateMony = false;
		List<Restaurant_Room> list = db.Restaurant_Room.Where((Restaurant_Room x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
		GroupRoom.Items.Clear();
		LoginDetails.TotalFatorPrice = 0m;
		foreach (Restaurant_Room item in list)
		{
			TileItem tileItem = new TileItem();
			TileRoom.ItemSize = 70;
			GroupRoom.Items.Add(tileItem);
			tileItem.Text = item.Name;
			tileItem.Id = item.ID;
			tileItem.ItemSize = TileItemSize.Wide;
			tileItem.AllowGlyphSkinning = DefaultBoolean.True;
			tileItem.Appearance.Font = new Font("Tahoma", 12.35f);
			tileItem.Appearance.BackColor = Color.Green;
			tileItem.Appearance.ForeColor = Color.White;
			tileItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
			tileItem.ItemClick += tileBarItem1_ItemClick;
			TileItemElement element = new TileItemElement();
			tileItem.Elements.Add(element);
			if (item.ISPause != true)
			{
				continue;
			}
			tileItem.Appearance.BackColor = Color.Red;
			tileItem.Appearance.ForeColor = Color.White;
			if (LoginDetails.IsTimeInRestaurant)
			{
				tileItem.TextAlignment = TileItemContentAlignment.TopCenter;
				TileItemElement tileItemElement = new TileItemElement();
				tileItem.Elements.Add(tileItemElement);
				tileItemElement.TextAlignment = TileItemContentAlignment.BottomCenter;
				string s = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
				DateTime dateTime = DateTime.Parse(s);
				DateTime value = dateTime;
				DateTime? time = item.Time1;
				tileItemElement.Text = (value - time).ToString();
				value = dateTime;
				time = item.Time1;
				TimeSpan timeSpan = TimeSpan.Parse((value - time).ToString());
				if (timeSpan > TimeSpan.Parse("00:00:00"))
				{
					tileItem.Appearance.BackColor = Color.Red;
				}
				if (timeSpan > TimeSpan.Parse("-00:10:00") && timeSpan < TimeSpan.Parse("00:00:00"))
				{
					tileItem.Appearance.BackColor = Color.Purple;
				}
			}
		}
		List<Restaurant_Table> list2 = db.Restaurant_Table.Where((Restaurant_Table x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
		GroupTable.Items.Clear();
		foreach (Restaurant_Table item2 in list2)
		{
			TileItem tileItem2 = new TileItem();
			TileTable.ItemSize = 70;
			GroupTable.Items.Add(tileItem2);
			tileItem2.Text = item2.Name;
			tileItem2.Id = item2.ID;
			tileItem2.ItemSize = TileItemSize.Wide;
			tileItem2.AllowGlyphSkinning = DefaultBoolean.True;
			tileItem2.Appearance.Font = new Font("Tahoma", 12.35f);
			tileItem2.Appearance.BackColor = Color.Blue;
			tileItem2.Appearance.ForeColor = Color.White;
			tileItem2.TextAlignment = TileItemContentAlignment.MiddleCenter;
			tileItem2.ItemClick += tileBarItem2_ItemClick;
			TileItemElement element2 = new TileItemElement();
			tileItem2.Elements.Add(element2);
			if (item2.ISPause != true)
			{
				continue;
			}
			tileItem2.Appearance.BackColor = Color.Red;
			tileItem2.Appearance.ForeColor = Color.White;
			if (LoginDetails.IsTimeInRestaurant)
			{
				tileItem2.TextAlignment = TileItemContentAlignment.TopCenter;
				TileItemElement tileItemElement2 = new TileItemElement();
				tileItem2.Elements.Add(tileItemElement2);
				tileItemElement2.TextAlignment = TileItemContentAlignment.BottomCenter;
				string s2 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
				DateTime dateTime2 = DateTime.Parse(s2);
				DateTime value = dateTime2;
				DateTime? time = item2.Time1;
				tileItemElement2.Text = (value - time).ToString();
				value = dateTime2;
				time = item2.Time1;
				TimeSpan timeSpan2 = TimeSpan.Parse((value - time).ToString());
				if (timeSpan2 > TimeSpan.Parse("00:00:00"))
				{
					tileItemElement2.Text = TimeSpan.Parse("00:00:00").ToString();
					tileItem2.Appearance.BackColor = Color.Red;
				}
				if (timeSpan2 > TimeSpan.Parse("-00:10:00") && timeSpan2 < TimeSpan.Parse("00:00:00"))
				{
					tileItem2.Appearance.BackColor = Color.Purple;
				}
			}
		}
		TxtCash.Text = "0.00";
		TxtVisa.Text = "0.00";
		TxtReturnOrd.Text = "0.00";
		TxtMoneyOld.Text = "0.00";
	}

	private void tileBarItem1_ItemClick(object sender, TileItemEventArgs e)
	{
		try
		{
			GTSdbEntities gTSdbEntities = new GTSdbEntities();
			Restaurant_Room restaurant_Room = gTSdbEntities.Restaurant_Room.Where((Restaurant_Room x) => x.ID == e.Item.Id && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
			TxtMoneyOld.Text = restaurant_Room.Money.ToString();
			LblOld.Text = "رقم الغرفة";
			if (restaurant_Room.ISPause == false)
			{
				TxtRoom.Text = "";
				MessageBox.Show("نعتذر طŒ هذه الغرفة غير مشغولة الان ", "مستكشف  ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return;
			}
			LblOrderID.Text = restaurant_Room.OrderID.ToString();
			LblType.Text = "1";
			TxtRoom.Text = restaurant_Room.Name;
			LblRoomID.Text = restaurant_Room.ID.ToString();
			TxtDiscount.Text = "0.00";
			decimal totalReturnOrderByOrderId = ORDRTN.GetTotalReturnOrderByOrderId(Convert.ToInt32(restaurant_Room.OrderID));
			TxtReturnOrd.Text = totalReturnOrderByOrderId.ToString();
			LoginDetails.TotalFatorPrice = Convert.ToDecimal(restaurant_Room.Money - (decimal?)totalReturnOrderByOrderId);
			TxtVisa.Text = "0.00";
			TxtCash.Text = LoginDetails.TotalFatorPrice.ToString();
			toggleCash.IsOn = true;
			TxtPause.Text = LoginDetails.TotalFatorPrice.ToString();
			LblDate.Text = restaurant_Room.Time1.ToString();
			TxtPause.Enabled = false;
			TogglePause.IsOn = false;
			TogglePause.Enabled = false;
		}
		catch (Exception ex)
		{
			string text = ex.ToString();
		}
	}

	private void tileBarItem2_ItemClick(object sender, TileItemEventArgs e)
	{
		try
		{
			Restaurant_Table restaurant_Table = db.Restaurant_Table.Where((Restaurant_Table x) => x.ID == e.Item.Id && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
			TxtRoom.Text = restaurant_Table.Name;
			TxtMoneyOld.Text = restaurant_Table.Money.ToString();
			LblOld.Text = "رقم الطاولة";
			if (restaurant_Table.ISPause == false)
			{
				TxtRoom.Text = "";
				MessageBox.Show("نعتذر طŒ هذه الطاولة غير مشغولة الان ", "مستكشف  ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return;
			}
			LblOrderID.Text = restaurant_Table.OrderID.ToString();
			LblType.Text = "2";
			TxtRoom.Text = restaurant_Table.Name;
			LblRoomID.Text = restaurant_Table.ID.ToString();
			decimal totalReturnOrderByOrderId = ORDRTN.GetTotalReturnOrderByOrderId(Convert.ToInt32(restaurant_Table.OrderID));
			TxtReturnOrd.Text = totalReturnOrderByOrderId.ToString();
			LoginDetails.TotalFatorPrice = Convert.ToDecimal(restaurant_Table.Money - (decimal?)totalReturnOrderByOrderId);
			TxtDiscount.Text = "0.00";
			TxtVisa.Text = "0.00";
			TxtCash.Text = LoginDetails.TotalFatorPrice.ToString();
			toggleCash.IsOn = true;
			TxtPause.Text = LoginDetails.TotalFatorPrice.ToString();
			TxtPause.Enabled = false;
			TogglePause.IsOn = false;
			TogglePause.Enabled = false;
			LblDate.Text = restaurant_Table.Time1.ToString();
		}
		catch (Exception)
		{
		}
	}

	private void TxtCash_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (!TogglePause.IsOn)
			{
				if (!toggleVisa.IsOn)
				{
					TxtCash.ReadOnly = true;
				}
				else
				{
					TxtCash.ReadOnly = false;
				}
				if (Convert.ToDecimal(TxtCash.Text) <= LoginDetails.TotalFatorPrice)
				{
					TxtVisa.Text = (Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtDiscount.Text) - Convert.ToDecimal(TxtCash.Text)).ToString();
					return;
				}
				TxtCash.Text = "0.00";
				TxtVisa.Text = (Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtDiscount.Text) - Convert.ToDecimal(TxtCash.Text)).ToString();
			}
		}
		catch (Exception)
		{
		}
	}

	private void TxtVisa_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (!TogglePause.IsOn)
			{
				if (!toggleCash.IsOn)
				{
					TxtVisa.ReadOnly = true;
				}
				else
				{
					TxtVisa.ReadOnly = false;
				}
				if (Convert.ToDecimal(TxtVisa.Text) <= LoginDetails.TotalFatorPrice)
				{
					TxtCash.Text = (Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtDiscount.Text) - Convert.ToDecimal(TxtVisa.Text)).ToString();
					return;
				}
				TxtVisa.Text = "0.00";
				TxtCash.Text = (Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtDiscount.Text) - Convert.ToDecimal(TxtVisa.Text)).ToString();
			}
		}
		catch (Exception)
		{
		}
	}

	private void toggleCash_Toggled(object sender, EventArgs e)
	{
		ToggleIsOn();
	}

	private void toggleVisa_Toggled(object sender, EventArgs e)
	{
		ToggleIsOff();
	}

	private void ToggleIsOn()
	{
		if (TogglePause.IsOn)
		{
			return;
		}
		if (toggleCash.IsOn)
		{
			TxtCash.ReadOnly = false;
			TxtVisa.ReadOnly = false;
			BtnCalcCash.Enabled = true;
			if (!toggleVisa.IsOn)
			{
				TxtVisa.Text = "0.00";
				TxtVisa.Enabled = false;
				TxtCash.Enabled = true;
				TxtCash.ReadOnly = false;
				BtnCalcVisa.Enabled = false;
			}
			else if (toggleVisa.IsOn)
			{
				TxtCash.Text = (Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
				TxtVisa.Text = (Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtDiscount.Text) - Convert.ToDecimal(TxtCash.Text)).ToString();
				TxtVisa.Enabled = true;
				TxtCash.Enabled = true;
				BtnCalcVisa.Enabled = true;
			}
		}
		else if (!toggleCash.IsOn)
		{
			BtnCalcCash.Enabled = false;
			TxtVisa.ReadOnly = true;
			TxtCash.Text = "0.00";
			TxtCash.Enabled = false;
			toggleVisa.IsOn = true;
			TxtVisa.Enabled = true;
			TxtCash.ReadOnly = true;
		}
	}

	private void ToggleIsOff()
	{
		if (TogglePause.IsOn)
		{
			return;
		}
		if (toggleVisa.IsOn)
		{
			TxtVisa.ReadOnly = false;
			TxtCash.ReadOnly = false;
			BtnCalcVisa.Enabled = true;
			if (!toggleCash.IsOn)
			{
				TxtCash.Text = "0.00";
				TxtCash.Enabled = false;
				TxtVisa.Enabled = true;
				TxtVisa.ReadOnly = false;
				BtnCalcCash.Enabled = false;
			}
			else if (toggleCash.IsOn)
			{
				TxtVisa.Text = (Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtDiscount.Text) - Convert.ToDecimal(TxtCash.Text)).ToString();
				TxtVisa.Enabled = true;
				TxtCash.Enabled = true;
				BtnCalcCash.Enabled = true;
			}
		}
		else if (!toggleVisa.IsOn)
		{
			TxtVisa.ReadOnly = true;
			TxtCash.ReadOnly = true;
			TxtVisa.Text = "0.00";
			TxtVisa.Enabled = false;
			toggleCash.IsOn = true;
			TxtCash.Enabled = true;
			BtnCalcVisa.Enabled = false;
		}
	}

	private void TxtDigital()
	{
		TxtMoneyOld.DecimalDigits = DecimalNum;
		TxtVisa.DecimalDigits = DecimalNum;
		TxtReturnOrd.DecimalDigits = DecimalNum;
		TxtPause.DecimalDigits = DecimalNum;
		TxtCash.DecimalDigits = DecimalNum;
		TxtDiscount.DecimalDigits = DecimalNum;
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		if (TxtRoom.Text != "")
		{
			if (!TogglePause.IsOn)
			{
				if (Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtDiscount.Text) == Convert.ToDecimal(TxtCash.Text) + Convert.ToDecimal(TxtVisa.Text))
				{
					IsComplateMony = true;
					IsMoneyPay = true;
					IsBtnOpen = false;
					Close();
				}
				else
				{
					LoginDetails.IsComplateMony = false;
					IsBtnOpen = false;
					MessageBox.Show("المبلغ المسدد لا يساوى المبلغ المطلوب", "مستكشف السداد ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				IsComplateMony = true;
				IsBtnOpen = false;
				Close();
			}
		}
		else
		{
			LoginDetails.IsComplateMony = false;
			MessageBox.Show("نعتذر، يجب اختيار رقم الغرفة / رقم الطاولة ثم حاول مرة اخري", "مستكشف السداد ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		LoginDetails.IsComplateMony = false;
		Close();
	}

	private void TxtDiscount_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToDecimal(LoginDetails.TotalFatorPrice) > 0m && Convert.ToDecimal(TxtDiscount.Text) < Convert.ToDecimal(LoginDetails.TotalFatorPrice))
			{
				TxtCash.Text = (Convert.ToDecimal(LoginDetails.TotalFatorPrice) - Convert.ToDecimal(TxtDiscount.Text)).ToString();
				return;
			}
			TxtCash.Text = LoginDetails.TotalFatorPrice.ToString();
			TxtDiscount.Text = "0.00";
		}
		catch (Exception)
		{
			TxtCash.Text = LoginDetails.TotalFatorPrice.ToString();
			TxtDiscount.Text = "0.00";
		}
	}

	private void BtnCalcDisc_Click(object sender, EventArgs e)
	{
		FrmCalculator frmCalculator = new FrmCalculator();
		frmCalculator.ShowDialog();
		TxtDiscount.Text = frmCalculator.textBox_Result.Text;
	}

	private void BtnCalcCash_Click(object sender, EventArgs e)
	{
		FrmCalculator frmCalculator = new FrmCalculator();
		frmCalculator.ShowDialog();
		TxtCash.Text = frmCalculator.textBox_Result.Text;
	}

	private void BtnCalcVisa_Click(object sender, EventArgs e)
	{
		FrmCalculator frmCalculator = new FrmCalculator();
		frmCalculator.ShowDialog();
		TxtVisa.Text = frmCalculator.textBox_Result.Text;
	}

	private void BtnPerviewCashier_Click(object sender, EventArgs e)
	{
		try
		{
			int num = Convert.ToInt32(LblOrderID.Text);
			if (num > 0)
			{
				using (SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer))
				{
					DataSetALL dataSetALL = new DataSetALL();
					dataSetALL.Tables["Order_PurchasesTablePrint"].Clear();
					SqlCommand sqlCommand = new SqlCommand("Print_Order_OrdersAfterReturn", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
					sqlCommand.Parameters.AddWithValue("@ID", num);
					sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
					sqlDataAdapter.Fill(dataSetALL.Order_PurchasesTablePrint);
					sqlConnection.Close();
					if (dataSetALL.Tables["Order_PurchasesTablePrint"].Rows.Count > 0)
					{
						StiReport stiReport = new StiReport();
						stiReport.Load(Application.StartupPath + "\\Reports\\PrintCashirRestaurant.mrt");
						stiReport.RegData(dataSetALL.Order_PurchasesTablePrint);
						stiReport.Dictionary.Synchronize();
						stiReport.Compile();
						stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
						stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
						stiReport["VatNumComp"] = LoginDetails.VatNum;
						stiReport["PrintComp"] = LoginDetails.PrintComp;
						stiReport["MobileComp"] = LoginDetails.Mobile;
						stiReport["AddressComp"] = LoginDetails.Address;
						string vatAmount = Math.Round(Convert.ToDecimal(TxtMoneyOld.Text) - Convert.ToDecimal(TxtReturnOrd.Text) - (Convert.ToDecimal(TxtMoneyOld.Text) - Convert.ToDecimal(TxtReturnOrd.Text)) / Convert.ToDecimal(1.15), 2).ToString();
						Class_EncryptInvoice class_EncryptInvoice = new Class_EncryptInvoice();
						string value = class_EncryptInvoice.GenerateQRBase64(LoginDetails.PrintComp, LoginDetails.VatNum, LblDate.Text, (Convert.ToDecimal(TxtMoneyOld.Text) - Convert.ToDecimal(TxtReturnOrd.Text)).ToString(), vatAmount);
						stiReport["EncHash"] = value;
						stiReport.Show(dialogForm: true);
					}
					else
					{
						MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					return;
				}
			}
			MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception)
		{
			MessageBox.Show("عفوا، لم يتم الطباعة ", "مستكشف البيانات ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void TogglePause_Toggled(object sender, EventArgs e)
	{
		if (TogglePause.IsOn)
		{
			toggleCash.IsOn = false;
			toggleVisa.IsOn = false;
			toggleCash.Enabled = false;
			toggleVisa.Enabled = false;
			TxtVisa.Enabled = false;
			TxtCash.Enabled = false;
			TxtPause.Enabled = true;
			TxtVisa.Text = "0.00";
			TxtCash.Text = "0.00";
			TxtPause.Text = LoginDetails.TotalFatorPrice.ToString();
		}
		else
		{
			TxtPause.Enabled = false;
			toggleCash.Enabled = true;
			toggleVisa.Enabled = true;
			TxtPause.Text = "0.00";
			TxtVisa.Text = "0.00";
			TxtCash.Text = LoginDetails.TotalFatorPrice.ToString();
			toggleVisa.IsOn = false;
			toggleCash.IsOn = true;
			ToggleIsOn();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmRestaurant.FrmMahlyCollecting));
		this.lbl = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtReturnOrd = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnCalcVisa = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCalcCash = new DevExpress.XtraEditors.SimpleButton();
		this.LblRoomID = new System.Windows.Forms.Label();
		this.LblOrderID = new System.Windows.Forms.Label();
		this.CkPrintCook = new System.Windows.Forms.CheckBox();
		this.CkPrintCheck = new System.Windows.Forms.CheckBox();
		this.LblType = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.LblOld = new System.Windows.Forms.Label();
		this.TxtRoom = new System.Windows.Forms.TextBox();
		this.TxtMoneyOld = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
		this.toggleVisa = new DevExpress.XtraEditors.ToggleSwitch();
		this.toggleCash = new DevExpress.XtraEditors.ToggleSwitch();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVisa = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtCash = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.BtnCalcDisc = new DevExpress.XtraEditors.SimpleButton();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtPause = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TogglePause = new DevExpress.XtraEditors.ToggleSwitch();
		this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
		this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
		this.TileRoom = new DevExpress.XtraEditors.TileControl();
		this.GroupRoom = new DevExpress.XtraEditors.TileGroup();
		this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
		this.TileTable = new DevExpress.XtraEditors.TileControl();
		this.GroupTable = new DevExpress.XtraEditors.TileGroup();
		this.BtnPerviewCashier = new DevExpress.XtraEditors.SimpleButton();
		this.LblDate = new System.Windows.Forms.Label();
		this.lbl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleVisa.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleCash.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TogglePause.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl1).BeginInit();
		this.xtraTabControl1.SuspendLayout();
		this.xtraTabPage1.SuspendLayout();
		this.xtraTabPage2.SuspendLayout();
		base.SuspendLayout();
		this.lbl.Controls.Add(this.LblDate);
		this.lbl.Controls.Add(this.BtnPerviewCashier);
		this.lbl.Controls.Add(this.label4);
		this.lbl.Controls.Add(this.label3);
		this.lbl.Controls.Add(this.TxtReturnOrd);
		this.lbl.Controls.Add(this.BtnCalcVisa);
		this.lbl.Controls.Add(this.BtnCalcCash);
		this.lbl.Controls.Add(this.LblRoomID);
		this.lbl.Controls.Add(this.LblOrderID);
		this.lbl.Controls.Add(this.CkPrintCook);
		this.lbl.Controls.Add(this.CkPrintCheck);
		this.lbl.Controls.Add(this.LblType);
		this.lbl.Controls.Add(this.label2);
		this.lbl.Controls.Add(this.LblOld);
		this.lbl.Controls.Add(this.TxtRoom);
		this.lbl.Controls.Add(this.TxtMoneyOld);
		this.lbl.Controls.Add(this.separatorControl1);
		this.lbl.Controls.Add(this.toggleVisa);
		this.lbl.Controls.Add(this.toggleCash);
		this.lbl.Controls.Add(this.BtnExit);
		this.lbl.Controls.Add(this.BtnSave);
		this.lbl.Controls.Add(this.TxtVisa);
		this.lbl.Controls.Add(this.TxtCash);
		this.lbl.Location = new System.Drawing.Point(12, 319);
		this.lbl.Name = "lbl";
		this.lbl.Size = new System.Drawing.Size(653, 299);
		this.lbl.TabIndex = 195;
		this.lbl.TabStop = false;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label4.Location = new System.Drawing.Point(524, 58);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(90, 21);
		this.label4.TabIndex = 82;
		this.label4.Text = "رقم الفاتورة";
		this.label4.Visible = false;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label3.Location = new System.Drawing.Point(377, 91);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(80, 21);
		this.label3.TabIndex = 81;
		this.label3.Text = "المرتجعات";
		this.TxtReturnOrd.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.TxtReturnOrd.DecimalDigits = 3;
		this.TxtReturnOrd.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtReturnOrd.ForeColor = System.Drawing.Color.Teal;
		this.TxtReturnOrd.Location = new System.Drawing.Point(35, 83);
		this.TxtReturnOrd.Name = "TxtReturnOrd";
		this.TxtReturnOrd.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtReturnOrd.Size = new System.Drawing.Size(335, 38);
		this.TxtReturnOrd.TabIndex = 80;
		this.TxtReturnOrd.Text = "0.000";
		this.TxtReturnOrd.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtReturnOrd.UseCompatibleTextRendering = false;
		this.TxtReturnOrd.Value = new decimal(new int[4]);
		this.TxtReturnOrd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.BtnCalcVisa.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.BtnCalcVisa.Enabled = false;
		this.BtnCalcVisa.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcVisa.ImageOptions.Image");
		this.BtnCalcVisa.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcVisa.Location = new System.Drawing.Point(377, 200);
		this.BtnCalcVisa.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCalcVisa.Name = "BtnCalcVisa";
		this.BtnCalcVisa.Size = new System.Drawing.Size(70, 38);
		this.BtnCalcVisa.TabIndex = 79;
		this.BtnCalcVisa.Text = "Calc";
		this.BtnCalcVisa.Click += new System.EventHandler(BtnCalcVisa_Click);
		this.BtnCalcCash.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.BtnCalcCash.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcCash.ImageOptions.Image");
		this.BtnCalcCash.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcCash.Location = new System.Drawing.Point(377, 156);
		this.BtnCalcCash.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCalcCash.Name = "BtnCalcCash";
		this.BtnCalcCash.Size = new System.Drawing.Size(70, 38);
		this.BtnCalcCash.TabIndex = 78;
		this.BtnCalcCash.Text = "Calc";
		this.BtnCalcCash.Click += new System.EventHandler(BtnCalcCash_Click);
		this.LblRoomID.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.LblRoomID.AutoSize = true;
		this.LblRoomID.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblRoomID.Location = new System.Drawing.Point(596, 54);
		this.LblRoomID.Name = "LblRoomID";
		this.LblRoomID.Size = new System.Drawing.Size(19, 21);
		this.LblRoomID.TabIndex = 74;
		this.LblRoomID.Text = "1";
		this.LblRoomID.Visible = false;
		this.LblOrderID.AutoSize = true;
		this.LblOrderID.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblOrderID.Location = new System.Drawing.Point(475, 58);
		this.LblOrderID.Name = "LblOrderID";
		this.LblOrderID.Size = new System.Drawing.Size(19, 21);
		this.LblOrderID.TabIndex = 73;
		this.LblOrderID.Text = "0";
		this.LblOrderID.Visible = false;
		this.CkPrintCook.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.CkPrintCook.AutoSize = true;
		this.CkPrintCook.Checked = true;
		this.CkPrintCook.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CkPrintCook.Location = new System.Drawing.Point(381, 258);
		this.CkPrintCook.Name = "CkPrintCook";
		this.CkPrintCook.Size = new System.Drawing.Size(109, 21);
		this.CkPrintCook.TabIndex = 72;
		this.CkPrintCook.Text = "طباعة المطبخ";
		this.CkPrintCook.UseVisualStyleBackColor = true;
		this.CkPrintCook.Visible = false;
		this.CkPrintCheck.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.CkPrintCheck.AutoSize = true;
		this.CkPrintCheck.Checked = true;
		this.CkPrintCheck.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CkPrintCheck.Location = new System.Drawing.Point(517, 258);
		this.CkPrintCheck.Name = "CkPrintCheck";
		this.CkPrintCheck.Size = new System.Drawing.Size(106, 21);
		this.CkPrintCheck.TabIndex = 71;
		this.CkPrintCheck.Text = "طباعة الشيك";
		this.CkPrintCheck.UseVisualStyleBackColor = true;
		this.LblType.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.LblType.AutoSize = true;
		this.LblType.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblType.Location = new System.Drawing.Point(604, 30);
		this.LblType.Name = "LblType";
		this.LblType.Size = new System.Drawing.Size(19, 21);
		this.LblType.TabIndex = 70;
		this.LblType.Text = "1";
		this.LblType.Visible = false;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label2.Location = new System.Drawing.Point(377, 33);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(117, 21);
		this.label2.TabIndex = 69;
		this.label2.Text = "اجمالي الفاتورة";
		this.LblOld.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.LblOld.AutoSize = true;
		this.LblOld.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblOld.Location = new System.Drawing.Point(522, 30);
		this.LblOld.Name = "LblOld";
		this.LblOld.Size = new System.Drawing.Size(81, 21);
		this.LblOld.TabIndex = 68;
		this.LblOld.Text = "رقم الغرفة";
		this.LblOld.Visible = false;
		this.TxtRoom.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.TxtRoom.BackColor = System.Drawing.Color.White;
		this.TxtRoom.Font = new System.Drawing.Font("Tahoma", 15f);
		this.TxtRoom.ForeColor = System.Drawing.Color.Teal;
		this.TxtRoom.Location = new System.Drawing.Point(500, 19);
		this.TxtRoom.Name = "TxtRoom";
		this.TxtRoom.ReadOnly = true;
		this.TxtRoom.Size = new System.Drawing.Size(131, 38);
		this.TxtRoom.TabIndex = 67;
		this.TxtRoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtRoom.Visible = false;
		this.TxtMoneyOld.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.TxtMoneyOld.DecimalDigits = 3;
		this.TxtMoneyOld.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtMoneyOld.ForeColor = System.Drawing.Color.Teal;
		this.TxtMoneyOld.Location = new System.Drawing.Point(35, 26);
		this.TxtMoneyOld.Name = "TxtMoneyOld";
		this.TxtMoneyOld.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMoneyOld.Size = new System.Drawing.Size(335, 38);
		this.TxtMoneyOld.TabIndex = 66;
		this.TxtMoneyOld.Text = "0.000";
		this.TxtMoneyOld.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMoneyOld.UseCompatibleTextRendering = false;
		this.TxtMoneyOld.Value = new decimal(new int[4]);
		this.TxtMoneyOld.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.separatorControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.separatorControl1.Location = new System.Drawing.Point(29, 127);
		this.separatorControl1.Name = "separatorControl1";
		this.separatorControl1.Size = new System.Drawing.Size(581, 23);
		this.separatorControl1.TabIndex = 60;
		this.toggleVisa.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.toggleVisa.Location = new System.Drawing.Point(446, 207);
		this.toggleVisa.Name = "toggleVisa";
		this.toggleVisa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.toggleVisa.Properties.Appearance.Options.UseFont = true;
		this.toggleVisa.Properties.OffText = "إلغاء الشبكة";
		this.toggleVisa.Properties.OnText = "شبكة";
		this.toggleVisa.Size = new System.Drawing.Size(201, 25);
		this.toggleVisa.TabIndex = 58;
		this.toggleVisa.Toggled += new System.EventHandler(toggleVisa_Toggled);
		this.toggleCash.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.toggleCash.EditValue = true;
		this.toggleCash.Location = new System.Drawing.Point(444, 163);
		this.toggleCash.Name = "toggleCash";
		this.toggleCash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.toggleCash.Properties.Appearance.Options.UseFont = true;
		this.toggleCash.Properties.OffText = "إلغاء النقدي";
		this.toggleCash.Properties.OnText = "نقدي";
		this.toggleCash.Size = new System.Drawing.Size(203, 25);
		this.toggleCash.TabIndex = 57;
		this.toggleCash.Toggled += new System.EventHandler(toggleCash_Toggled);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(35, 245);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(149, 40);
		this.BtnExit.TabIndex = 56;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(221, 245);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(149, 40);
		this.BtnSave.TabIndex = 51;
		this.BtnSave.Text = "تحصيل وقفل الغرفة";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.TxtVisa.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.TxtVisa.DecimalDigits = 3;
		this.TxtVisa.Enabled = false;
		this.TxtVisa.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtVisa.ForeColor = System.Drawing.Color.Black;
		this.TxtVisa.Location = new System.Drawing.Point(35, 200);
		this.TxtVisa.Name = "TxtVisa";
		this.TxtVisa.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtVisa.Size = new System.Drawing.Size(335, 38);
		this.TxtVisa.TabIndex = 55;
		this.TxtVisa.Text = "0.000";
		this.TxtVisa.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtVisa.UseCompatibleTextRendering = false;
		this.TxtVisa.Value = new decimal(new int[4]);
		this.TxtVisa.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtVisa.TextChanged += new System.EventHandler(TxtVisa_TextChanged);
		this.TxtCash.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.TxtCash.DecimalDigits = 3;
		this.TxtCash.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtCash.ForeColor = System.Drawing.Color.Blue;
		this.TxtCash.Location = new System.Drawing.Point(35, 156);
		this.TxtCash.Name = "TxtCash";
		this.TxtCash.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtCash.Size = new System.Drawing.Size(335, 38);
		this.TxtCash.TabIndex = 52;
		this.TxtCash.Text = "0.000";
		this.TxtCash.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtCash.UseCompatibleTextRendering = false;
		this.TxtCash.Value = new decimal(new int[4]);
		this.TxtCash.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtCash.TextChanged += new System.EventHandler(TxtCash_TextChanged);
		this.BtnCalcDisc.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.BtnCalcDisc.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcDisc.ImageOptions.Image");
		this.BtnCalcDisc.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcDisc.Location = new System.Drawing.Point(367, 133);
		this.BtnCalcDisc.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCalcDisc.Name = "BtnCalcDisc";
		this.BtnCalcDisc.Size = new System.Drawing.Size(70, 38);
		this.BtnCalcDisc.TabIndex = 77;
		this.BtnCalcDisc.Text = "Calc";
		this.BtnCalcDisc.Visible = false;
		this.BtnCalcDisc.Click += new System.EventHandler(BtnCalcDisc_Click);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label1.Location = new System.Drawing.Point(507, 142);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(56, 21);
		this.label1.TabIndex = 76;
		this.label1.Text = "الخصم";
		this.label1.Visible = false;
		this.TxtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtDiscount.ForeColor = System.Drawing.Color.Red;
		this.TxtDiscount.Location = new System.Drawing.Point(25, 135);
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDiscount.Size = new System.Drawing.Size(335, 38);
		this.TxtDiscount.TabIndex = 75;
		this.TxtDiscount.Text = "0.000";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.UseCompatibleTextRendering = false;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.Visible = false;
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtDiscount.TextChanged += new System.EventHandler(TxtDiscount_TextChanged);
		this.TxtPause.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.TxtPause.Enabled = false;
		this.TxtPause.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtPause.ForeColor = System.Drawing.Color.Blue;
		this.TxtPause.Location = new System.Drawing.Point(22, 193);
		this.TxtPause.Name = "TxtPause";
		this.TxtPause.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPause.Size = new System.Drawing.Size(335, 38);
		this.TxtPause.TabIndex = 61;
		this.TxtPause.Text = "0.00";
		this.TxtPause.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtPause.UseCompatibleTextRendering = false;
		this.TxtPause.Value = new decimal(new int[4]);
		this.TxtPause.Visible = false;
		this.TxtPause.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TogglePause.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.TogglePause.Location = new System.Drawing.Point(392, 198);
		this.TogglePause.Name = "TogglePause";
		this.TogglePause.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TogglePause.Properties.Appearance.Options.UseFont = true;
		this.TogglePause.Properties.OffText = "إلغاء التعليق ";
		this.TogglePause.Properties.OnText = "تعليق الفاتورة";
		this.TogglePause.Size = new System.Drawing.Size(206, 25);
		this.TogglePause.TabIndex = 59;
		this.TogglePause.Visible = false;
		this.TogglePause.Toggled += new System.EventHandler(TogglePause_Toggled);
		this.xtraTabControl1.Location = new System.Drawing.Point(12, 12);
		this.xtraTabControl1.MultiLine = DevExpress.Utils.DefaultBoolean.True;
		this.xtraTabControl1.Name = "xtraTabControl1";
		this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
		this.xtraTabControl1.Size = new System.Drawing.Size(653, 301);
		this.xtraTabControl1.TabIndex = 195;
		this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[2] { this.xtraTabPage1, this.xtraTabPage2 });
		this.xtraTabPage1.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 15f);
		this.xtraTabPage1.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage1.Appearance.Header.Options.UseTextOptions = true;
		this.xtraTabPage1.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.xtraTabPage1.Controls.Add(this.TileRoom);
		this.xtraTabPage1.Controls.Add(this.TxtPause);
		this.xtraTabPage1.Controls.Add(this.TogglePause);
		this.xtraTabPage1.Controls.Add(this.TxtDiscount);
		this.xtraTabPage1.Controls.Add(this.BtnCalcDisc);
		this.xtraTabPage1.Controls.Add(this.label1);
		this.xtraTabPage1.Name = "xtraTabPage1";
		this.xtraTabPage1.Size = new System.Drawing.Size(651, 257);
		this.xtraTabPage1.Text = "الغرف";
		this.TileRoom.AllowDrag = false;
		this.TileRoom.AllowDragTilesBetweenGroups = false;
		this.TileRoom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileRoom.Groups.Add(this.GroupRoom);
		this.TileRoom.Location = new System.Drawing.Point(3, 3);
		this.TileRoom.Name = "TileRoom";
		this.TileRoom.Orientation = System.Windows.Forms.Orientation.Vertical;
		this.TileRoom.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.TileRoom.Size = new System.Drawing.Size(640, 251);
		this.TileRoom.TabIndex = 195;
		this.TileRoom.Text = "tileControl1";
		this.GroupRoom.Name = "GroupRoom";
		this.xtraTabPage2.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 15f);
		this.xtraTabPage2.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage2.Appearance.Header.Options.UseTextOptions = true;
		this.xtraTabPage2.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.xtraTabPage2.Controls.Add(this.TileTable);
		this.xtraTabPage2.Name = "xtraTabPage2";
		this.xtraTabPage2.Size = new System.Drawing.Size(651, 257);
		this.xtraTabPage2.Text = "الطاولات";
		this.TileTable.AllowDrag = false;
		this.TileTable.AllowDragTilesBetweenGroups = false;
		this.TileTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileTable.Groups.Add(this.GroupTable);
		this.TileTable.Location = new System.Drawing.Point(3, 7);
		this.TileTable.Name = "TileTable";
		this.TileTable.Orientation = System.Windows.Forms.Orientation.Vertical;
		this.TileTable.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.TileTable.Size = new System.Drawing.Size(645, 247);
		this.TileTable.TabIndex = 196;
		this.TileTable.Text = "tileControl2";
		this.GroupTable.Name = "GroupTable";
		this.BtnPerviewCashier.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.BtnPerviewCashier.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPerviewCashier.Location = new System.Drawing.Point(500, 83);
		this.BtnPerviewCashier.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPerviewCashier.Name = "BtnPerviewCashier";
		this.BtnPerviewCashier.Size = new System.Drawing.Size(144, 38);
		this.BtnPerviewCashier.TabIndex = 83;
		this.BtnPerviewCashier.Text = "عرض الفاتورة";
		this.BtnPerviewCashier.Click += new System.EventHandler(BtnPerviewCashier_Click);
		this.LblDate.AutoSize = true;
		this.LblDate.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblDate.Location = new System.Drawing.Point(474, 91);
		this.LblDate.Name = "LblDate";
		this.LblDate.Size = new System.Drawing.Size(19, 21);
		this.LblDate.TabIndex = 84;
		this.LblDate.Text = "0";
		this.LblDate.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(677, 628);
		base.Controls.Add(this.xtraTabControl1);
		base.Controls.Add(this.lbl);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmMahlyCollecting";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = " الدفع";
		this.lbl.ResumeLayout(false);
		this.lbl.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleVisa.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleCash.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TogglePause.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl1).EndInit();
		this.xtraTabControl1.ResumeLayout(false);
		this.xtraTabPage1.ResumeLayout(false);
		this.xtraTabPage1.PerformLayout();
		this.xtraTabPage2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


