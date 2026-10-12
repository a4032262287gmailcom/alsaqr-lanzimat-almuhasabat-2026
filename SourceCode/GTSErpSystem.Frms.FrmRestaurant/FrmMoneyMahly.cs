using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.Frms.FrmRestaurant.LuckUp;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.FrmRestaurant;

public class FrmMoneyMahly : XtraForm
{
	public bool IsComplateMony = false;

	private GTSdbEntities db = new GTSdbEntities();

	public bool IsMoneyPay;

	public bool ISPause = false;

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

	private Label LblOld;

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

	public RadioButton RadioPause;

	public CheckBox CkPrintCook;

	public CheckBox CkPrintCheck;

	public TimeEdit TxtTime1;

	public DateEdit TxtDate1;

	private SimpleButton BtnCalcCash;

	public FrmMoneyMahly()
	{
		InitializeComponent();
		TxtDigital();
		TxtTime1.Time = DateTime.Now.AddHours(1.0);
		TxtDate1.DateTime = DateTime.Now.Date;
		IsComplateMony = false;
		TxtCash.Text = Convert.ToString(LoginDetails.TotalFatorPrice);
		List<Restaurant_Room> list = db.Restaurant_Room.Where((Restaurant_Room x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
		GroupRoom.Items.Clear();
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
				tileItemElement.Text = (dateTime - DateTime.Parse(item.Time1.Value.ToString("yyyy/MM/dd HH:mm:ss"))).ToString();
				TimeSpan timeSpan = TimeSpan.Parse((item.Time1 - dateTime).ToString());
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
				tileItemElement2.Text = (dateTime2 - DateTime.Parse(item2.Time1.Value.ToString("yyyy/MM/dd HH:mm:ss"))).ToString();
				TimeSpan timeOfDay = Convert.ToDateTime(item2.Time1.Value).TimeOfDay;
				if (timeOfDay > TimeSpan.Parse("00:00:00"))
				{
					tileItemElement2.Text = TimeSpan.Parse("00:00:00").ToString();
					tileItem2.Appearance.BackColor = Color.Red;
				}
				if (timeOfDay > TimeSpan.Parse("-00:10:00") && timeOfDay < TimeSpan.Parse("00:00:00"))
				{
					tileItem2.Appearance.BackColor = Color.Purple;
				}
			}
		}
		TxtCash.Text = "0.00";
		TxtVisa.Text = "0.00";
		TxtPause.Text = "0.00";
		TxtMoneyOld.Text = "0.00";
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave);
		}
	}

	private void tileBarItem1_ItemClick(object sender, TileItemEventArgs e)
	{
		try
		{
			Restaurant_Room restaurant_Room = db.Restaurant_Room.Where((Restaurant_Room x) => x.ID == e.Item.Id && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
			TxtMoneyOld.Text = restaurant_Room.Money.ToString();
			LblOld.Text = "رقم الغرفة";
			LblType.Text = "1";
			TxtRoom.Text = restaurant_Room.Name;
			LblRoomID.Text = restaurant_Room.ID.ToString();
			LblOrderID.Text = restaurant_Room.OrderID.ToString();
			if (restaurant_Room.ISPause == true)
			{
				RadioPause.Checked = true;
			}
			else
			{
				RadioPause.Checked = false;
			}
		}
		catch (Exception)
		{
		}
	}

	private void tileBarItem2_ItemClick(object sender, TileItemEventArgs e)
	{
		try
		{
			Restaurant_Table restaurant_Table = db.Restaurant_Table.Where((Restaurant_Table x) => x.ID == e.Item.Id && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
			TxtMoneyOld.Text = restaurant_Table.Money.ToString();
			LblOld.Text = "رقم الطاولة";
			LblType.Text = "2";
			TxtRoom.Text = restaurant_Table.Name;
			LblRoomID.Text = restaurant_Table.ID.ToString();
			LblOrderID.Text = restaurant_Table.OrderID.ToString();
			if (restaurant_Table.ISPause == true)
			{
				RadioPause.Checked = true;
			}
			else
			{
				RadioPause.Checked = false;
			}
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
					TxtVisa.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtCash.Text)).ToString();
					return;
				}
				TxtCash.Text = "0.00";
				TxtVisa.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtCash.Text)).ToString();
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
					TxtCash.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtVisa.Text)).ToString();
					return;
				}
				TxtVisa.Text = "0.00";
				TxtCash.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtVisa.Text)).ToString();
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
			if (!toggleVisa.IsOn)
			{
				TxtVisa.Text = "0.00";
				TxtVisa.Enabled = false;
				TxtCash.Enabled = true;
				TxtCash.ReadOnly = false;
			}
			else if (toggleVisa.IsOn)
			{
				TxtCash.Text = LoginDetails.TotalFatorPrice.ToString();
				TxtVisa.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtCash.Text)).ToString();
				TxtVisa.Enabled = true;
				TxtCash.Enabled = true;
			}
		}
		else if (!toggleCash.IsOn)
		{
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
			if (!toggleCash.IsOn)
			{
				TxtCash.Text = "0.00";
				TxtCash.Enabled = false;
				TxtVisa.Enabled = true;
				TxtVisa.ReadOnly = false;
			}
			else if (toggleCash.IsOn)
			{
				TxtVisa.Text = (LoginDetails.TotalFatorPrice - Convert.ToDecimal(TxtCash.Text)).ToString();
				TxtVisa.Enabled = true;
				TxtCash.Enabled = true;
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
		}
	}

	private void TxtDigital()
	{
		TxtMoneyOld.DecimalDigits = DecimalNum;
		TxtVisa.DecimalDigits = DecimalNum;
		TxtPause.DecimalDigits = DecimalNum;
		TxtCash.DecimalDigits = DecimalNum;
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		if (TxtRoom.Text != "")
		{
			if (!TogglePause.IsOn)
			{
				if (LoginDetails.TotalFatorPrice == Convert.ToDecimal(TxtCash.Text) + Convert.ToDecimal(TxtVisa.Text))
				{
					IsComplateMony = true;
					IsMoneyPay = true;
					Close();
				}
				else
				{
					LoginDetails.IsComplateMony = false;
					MessageBox.Show("المبلغ المسدد لا يساوى المبلغ المطلوب", "مستكشف السداد ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				IsComplateMony = true;
				IsMoneyPay = false;
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

	private void BtnCalcCash_Click(object sender, EventArgs e)
	{
		LoginDetails.TotalCalc = Convert.ToDecimal(TxtCash.Text);
		new FrmCalcRes().ShowDialog();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmRestaurant.FrmMoneyMahly));
		this.lbl = new System.Windows.Forms.GroupBox();
		this.RadioPause = new System.Windows.Forms.RadioButton();
		this.LblRoomID = new System.Windows.Forms.Label();
		this.LblOrderID = new System.Windows.Forms.Label();
		this.CkPrintCook = new System.Windows.Forms.CheckBox();
		this.CkPrintCheck = new System.Windows.Forms.CheckBox();
		this.LblType = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.LblOld = new System.Windows.Forms.Label();
		this.TxtRoom = new System.Windows.Forms.TextBox();
		this.TxtMoneyOld = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtPause = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
		this.TogglePause = new DevExpress.XtraEditors.ToggleSwitch();
		this.toggleVisa = new DevExpress.XtraEditors.ToggleSwitch();
		this.toggleCash = new DevExpress.XtraEditors.ToggleSwitch();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.TxtVisa = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtCash = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
		this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
		this.TileRoom = new DevExpress.XtraEditors.TileControl();
		this.GroupRoom = new DevExpress.XtraEditors.TileGroup();
		this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
		this.TileTable = new DevExpress.XtraEditors.TileControl();
		this.GroupTable = new DevExpress.XtraEditors.TileGroup();
		this.TxtTime1 = new DevExpress.XtraEditors.TimeEdit();
		this.TxtDate1 = new DevExpress.XtraEditors.DateEdit();
		this.BtnCalcCash = new DevExpress.XtraEditors.SimpleButton();
		this.lbl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TogglePause.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleVisa.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleCash.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl1).BeginInit();
		this.xtraTabControl1.SuspendLayout();
		this.xtraTabPage1.SuspendLayout();
		this.xtraTabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).BeginInit();
		base.SuspendLayout();
		this.lbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lbl.Controls.Add(this.BtnCalcCash);
		this.lbl.Controls.Add(this.RadioPause);
		this.lbl.Controls.Add(this.LblRoomID);
		this.lbl.Controls.Add(this.LblOrderID);
		this.lbl.Controls.Add(this.CkPrintCook);
		this.lbl.Controls.Add(this.CkPrintCheck);
		this.lbl.Controls.Add(this.LblType);
		this.lbl.Controls.Add(this.label2);
		this.lbl.Controls.Add(this.LblOld);
		this.lbl.Controls.Add(this.TxtRoom);
		this.lbl.Controls.Add(this.TxtMoneyOld);
		this.lbl.Controls.Add(this.TxtPause);
		this.lbl.Controls.Add(this.separatorControl1);
		this.lbl.Controls.Add(this.TogglePause);
		this.lbl.Controls.Add(this.toggleVisa);
		this.lbl.Controls.Add(this.toggleCash);
		this.lbl.Controls.Add(this.BtnExit);
		this.lbl.Controls.Add(this.BtnSave);
		this.lbl.Controls.Add(this.TxtVisa);
		this.lbl.Controls.Add(this.TxtCash);
		this.lbl.Location = new System.Drawing.Point(12, 358);
		this.lbl.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.lbl.Name = "lbl";
		this.lbl.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.lbl.Size = new System.Drawing.Size(606, 347);
		this.lbl.TabIndex = 195;
		this.lbl.TabStop = false;
		this.RadioPause.AutoSize = true;
		this.RadioPause.Location = new System.Drawing.Point(440, 244);
		this.RadioPause.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.RadioPause.Name = "RadioPause";
		this.RadioPause.Size = new System.Drawing.Size(137, 21);
		this.RadioPause.TabIndex = 75;
		this.RadioPause.TabStop = true;
		this.RadioPause.Text = "هل الغرفة مشغولة";
		this.RadioPause.UseVisualStyleBackColor = true;
		this.RadioPause.Visible = false;
		this.LblRoomID.AutoSize = true;
		this.LblRoomID.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblRoomID.Location = new System.Drawing.Point(573, 54);
		this.LblRoomID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblRoomID.Name = "LblRoomID";
		this.LblRoomID.Size = new System.Drawing.Size(19, 21);
		this.LblRoomID.TabIndex = 74;
		this.LblRoomID.Text = "1";
		this.LblRoomID.Visible = false;
		this.LblOrderID.AutoSize = true;
		this.LblOrderID.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblOrderID.Location = new System.Drawing.Point(584, 117);
		this.LblOrderID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblOrderID.Name = "LblOrderID";
		this.LblOrderID.Size = new System.Drawing.Size(19, 21);
		this.LblOrderID.TabIndex = 73;
		this.LblOrderID.Text = "1";
		this.LblOrderID.Visible = false;
		this.CkPrintCook.AutoSize = true;
		this.CkPrintCook.Checked = true;
		this.CkPrintCook.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CkPrintCook.Location = new System.Drawing.Point(238, 258);
		this.CkPrintCook.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CkPrintCook.Name = "CkPrintCook";
		this.CkPrintCook.Size = new System.Drawing.Size(109, 21);
		this.CkPrintCook.TabIndex = 72;
		this.CkPrintCook.Text = "طباعة المطبخ";
		this.CkPrintCook.UseVisualStyleBackColor = true;
		this.CkPrintCheck.AutoSize = true;
		this.CkPrintCheck.Location = new System.Drawing.Point(12, 258);
		this.CkPrintCheck.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CkPrintCheck.Name = "CkPrintCheck";
		this.CkPrintCheck.Size = new System.Drawing.Size(106, 21);
		this.CkPrintCheck.TabIndex = 71;
		this.CkPrintCheck.Text = "طباعة الشيك";
		this.CkPrintCheck.UseVisualStyleBackColor = true;
		this.LblType.AutoSize = true;
		this.LblType.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblType.Location = new System.Drawing.Point(581, 30);
		this.LblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblType.Name = "LblType";
		this.LblType.Size = new System.Drawing.Size(19, 21);
		this.LblType.TabIndex = 70;
		this.LblType.Text = "1";
		this.LblType.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label2.Location = new System.Drawing.Point(232, 37);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(119, 21);
		this.label2.TabIndex = 69;
		this.label2.Text = "اجمالي السابق";
		this.LblOld.AutoSize = true;
		this.LblOld.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblOld.Location = new System.Drawing.Point(500, 37);
		this.LblOld.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblOld.Name = "LblOld";
		this.LblOld.Size = new System.Drawing.Size(81, 21);
		this.LblOld.TabIndex = 68;
		this.LblOld.Text = "رقم الغرفة";
		this.TxtRoom.BackColor = System.Drawing.Color.White;
		this.TxtRoom.Font = new System.Drawing.Font("Tahoma", 15f);
		this.TxtRoom.ForeColor = System.Drawing.Color.Teal;
		this.TxtRoom.Location = new System.Drawing.Point(363, 26);
		this.TxtRoom.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtRoom.Name = "TxtRoom";
		this.TxtRoom.ReadOnly = true;
		this.TxtRoom.Size = new System.Drawing.Size(131, 38);
		this.TxtRoom.TabIndex = 67;
		this.TxtRoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtMoneyOld.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.TxtMoneyOld.DecimalDigits = 3;
		this.TxtMoneyOld.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtMoneyOld.ForeColor = System.Drawing.Color.Teal;
		this.TxtMoneyOld.Location = new System.Drawing.Point(12, 26);
		this.TxtMoneyOld.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtMoneyOld.Name = "TxtMoneyOld";
		this.TxtMoneyOld.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMoneyOld.Size = new System.Drawing.Size(214, 38);
		this.TxtMoneyOld.TabIndex = 66;
		this.TxtMoneyOld.Text = "0.000";
		this.TxtMoneyOld.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMoneyOld.UseCompatibleTextRendering = false;
		this.TxtMoneyOld.Value = new decimal(new int[4]);
		this.TxtMoneyOld.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtPause.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.TxtPause.DecimalDigits = 3;
		this.TxtPause.Enabled = false;
		this.TxtPause.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtPause.ForeColor = System.Drawing.Color.Blue;
		this.TxtPause.Location = new System.Drawing.Point(12, 79);
		this.TxtPause.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtPause.Name = "TxtPause";
		this.TxtPause.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPause.Size = new System.Drawing.Size(335, 38);
		this.TxtPause.TabIndex = 61;
		this.TxtPause.Text = "0.000";
		this.TxtPause.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtPause.UseCompatibleTextRendering = false;
		this.TxtPause.Value = new decimal(new int[4]);
		this.TxtPause.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.separatorControl1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.separatorControl1.Location = new System.Drawing.Point(6, 127);
		this.separatorControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.separatorControl1.Name = "separatorControl1";
		this.separatorControl1.Size = new System.Drawing.Size(581, 23);
		this.separatorControl1.TabIndex = 60;
		this.TogglePause.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.TogglePause.Location = new System.Drawing.Point(382, 84);
		this.TogglePause.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TogglePause.Name = "TogglePause";
		this.TogglePause.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TogglePause.Properties.Appearance.Options.UseFont = true;
		this.TogglePause.Properties.OffText = "إلغاء التعليق ";
		this.TogglePause.Properties.OnText = "تعليق الفاتورة";
		this.TogglePause.Size = new System.Drawing.Size(206, 25);
		this.TogglePause.TabIndex = 59;
		this.TogglePause.Toggled += new System.EventHandler(TogglePause_Toggled);
		this.toggleVisa.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.toggleVisa.Location = new System.Drawing.Point(387, 213);
		this.toggleVisa.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.toggleVisa.Name = "toggleVisa";
		this.toggleVisa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.toggleVisa.Properties.Appearance.Options.UseFont = true;
		this.toggleVisa.Properties.OffText = "إلغاء الشبكة";
		this.toggleVisa.Properties.OnText = "شبكة";
		this.toggleVisa.Size = new System.Drawing.Size(201, 25);
		this.toggleVisa.TabIndex = 58;
		this.toggleVisa.Toggled += new System.EventHandler(toggleVisa_Toggled);
		this.toggleCash.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.toggleCash.EditValue = true;
		this.toggleCash.Location = new System.Drawing.Point(385, 166);
		this.toggleCash.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.toggleCash.Name = "toggleCash";
		this.toggleCash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.toggleCash.Properties.Appearance.Options.UseFont = true;
		this.toggleCash.Properties.OffText = "إلغاء النقدي";
		this.toggleCash.Properties.OnText = "نقدي";
		this.toggleCash.Size = new System.Drawing.Size(203, 25);
		this.toggleCash.TabIndex = 57;
		this.toggleCash.Toggled += new System.EventHandler(toggleCash_Toggled);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(12, 290);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(149, 39);
		this.BtnExit.TabIndex = 56;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(198, 290);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(149, 39);
		this.BtnSave.TabIndex = 51;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.TxtVisa.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.TxtVisa.DecimalDigits = 3;
		this.TxtVisa.Enabled = false;
		this.TxtVisa.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtVisa.ForeColor = System.Drawing.Color.Black;
		this.TxtVisa.Location = new System.Drawing.Point(12, 210);
		this.TxtVisa.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
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
		this.TxtCash.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.TxtCash.DecimalDigits = 3;
		this.TxtCash.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtCash.ForeColor = System.Drawing.Color.Blue;
		this.TxtCash.Location = new System.Drawing.Point(12, 160);
		this.TxtCash.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
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
		this.xtraTabControl1.Location = new System.Drawing.Point(12, 12);
		this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabControl1.MultiLine = DevExpress.Utils.DefaultBoolean.True;
		this.xtraTabControl1.Name = "xtraTabControl1";
		this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
		this.xtraTabControl1.Size = new System.Drawing.Size(606, 302);
		this.xtraTabControl1.TabIndex = 195;
		this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[2] { this.xtraTabPage1, this.xtraTabPage2 });
		this.xtraTabPage1.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 15f);
		this.xtraTabPage1.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage1.Appearance.Header.Options.UseTextOptions = true;
		this.xtraTabPage1.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.xtraTabPage1.Controls.Add(this.TileRoom);
		this.xtraTabPage1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabPage1.Name = "xtraTabPage1";
		this.xtraTabPage1.Size = new System.Drawing.Size(604, 258);
		this.xtraTabPage1.Text = "الغرف";
		this.TileRoom.AllowDrag = false;
		this.TileRoom.AllowDragTilesBetweenGroups = false;
		this.TileRoom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileRoom.Groups.Add(this.GroupRoom);
		this.TileRoom.Location = new System.Drawing.Point(4, 2);
		this.TileRoom.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TileRoom.Name = "TileRoom";
		this.TileRoom.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.TileRoom.Size = new System.Drawing.Size(593, 247);
		this.TileRoom.TabIndex = 195;
		this.TileRoom.Text = "tileControl1";
		this.GroupRoom.Name = "GroupRoom";
		this.xtraTabPage2.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 15f);
		this.xtraTabPage2.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage2.Appearance.Header.Options.UseTextOptions = true;
		this.xtraTabPage2.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.xtraTabPage2.Controls.Add(this.TileTable);
		this.xtraTabPage2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabPage2.Name = "xtraTabPage2";
		this.xtraTabPage2.Size = new System.Drawing.Size(604, 258);
		this.xtraTabPage2.Text = "الطاولات";
		this.TileTable.AllowDrag = false;
		this.TileTable.AllowDragTilesBetweenGroups = false;
		this.TileTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileTable.Groups.Add(this.GroupTable);
		this.TileTable.Location = new System.Drawing.Point(4, 2);
		this.TileTable.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TileTable.Name = "TileTable";
		this.TileTable.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.TileTable.Size = new System.Drawing.Size(593, 247);
		this.TileTable.TabIndex = 196;
		this.TileTable.Text = "tileControl2";
		this.GroupTable.Name = "GroupTable";
		this.TxtTime1.EditValue = new System.DateTime(2020, 6, 22, 0, 0, 0, 0);
		this.TxtTime1.Location = new System.Drawing.Point(13, 319);
		this.TxtTime1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTime1.Name = "TxtTime1";
		this.TxtTime1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
		this.TxtTime1.Properties.Appearance.Options.UseFont = true;
		this.TxtTime1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTime1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTime1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtTime1.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
		this.TxtTime1.Size = new System.Drawing.Size(267, 34);
		this.TxtTime1.TabIndex = 196;
		this.TxtDate1.EditValue = new System.DateTime(2021, 12, 18, 15, 52, 12, 0);
		this.TxtDate1.Location = new System.Drawing.Point(287, 319);
		this.TxtDate1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDate1.Name = "TxtDate1";
		this.TxtDate1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
		this.TxtDate1.Properties.Appearance.Options.UseFont = true;
		this.TxtDate1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate1.Properties.Mask.EditMask = "yyyy/MM/dd";
		this.TxtDate1.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate1.Size = new System.Drawing.Size(318, 34);
		this.TxtDate1.TabIndex = 197;
		this.BtnCalcCash.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.BtnCalcCash.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcCash.ImageOptions.Image");
		this.BtnCalcCash.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcCash.Location = new System.Drawing.Point(355, 160);
		this.BtnCalcCash.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCalcCash.Name = "BtnCalcCash";
		this.BtnCalcCash.Size = new System.Drawing.Size(70, 38);
		this.BtnCalcCash.TabIndex = 79;
		this.BtnCalcCash.Text = "Calc";
		this.BtnCalcCash.Click += new System.EventHandler(BtnCalcCash_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(630, 718);
		base.Controls.Add(this.TxtDate1);
		base.Controls.Add(this.TxtTime1);
		base.Controls.Add(this.xtraTabControl1);
		base.Controls.Add(this.lbl);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		base.MaximizeBox = false;
		base.Name = "FrmMoneyMahly";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = " الدفع";
		this.lbl.ResumeLayout(false);
		this.lbl.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TogglePause.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleVisa.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleCash.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl1).EndInit();
		this.xtraTabControl1.ResumeLayout(false);
		this.xtraTabPage1.ResumeLayout(false);
		this.xtraTabPage2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


