using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.FrmRestaurant.LuckUp;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.FrmRestaurant;

public class FrmMoneyDelivery : XtraForm
{
	public bool IsComplateMony = false;

	private GTSdbEntities db = new GTSdbEntities();

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

	public NumericEditBox TxtDelivery;

	private XtraTabControl xtraTabControl1;

	private XtraTabPage xtraTabPage1;

	private TileControl TileRoom;

	private TileGroup GroupRoom;

	public ToggleSwitch ToggleDelivery;

	private CheckBox CkPrintCook;

	private CheckBox CkPrintCheck;

	private SeparatorControl separatorControl2;

	public ToggleSwitch ToggelHanger;

	public NumericEditBox TxtHanger;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	public TextBox TxtDeliveryMan;

	public Label LblDelivery;

	public TextBox TxtCustPhone;

	public TextBox TxtCustPlace;

	private SimpleButton BtnCustomer;

	public ButtonEdit TxtCustName;

	public Label lblComp;

	public Label LblCompAccount;

	public ButtonEdit TxtCompanyName;

	private SimpleButton BtnCalcCash;

	public FrmMoneyDelivery()
	{
		InitializeComponent();
		TxtDigital();
		IsComplateMony = false;
		TxtCash.Text = Convert.ToString(LoginDetails.TotalFatorPrice);
		List<Restaurant_DeliveryMan> list = db.Restaurant_DeliveryMan.Where((Restaurant_DeliveryMan x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
		GroupRoom.Items.Clear();
		foreach (Restaurant_DeliveryMan item in list)
		{
			TileItem tileItem = new TileItem();
			TileRoom.ItemSize = 80;
			GroupRoom.Items.Add(tileItem);
			tileItem.Text = item.DeliveryName;
			tileItem.Id = item.ID;
			tileItem.ItemSize = TileItemSize.Wide;
			tileItem.AllowGlyphSkinning = DefaultBoolean.True;
			tileItem.Appearance.Font = new Font("Tahoma", 12.35f);
			tileItem.Appearance.BackColor = Color.Teal;
			tileItem.Appearance.ForeColor = Color.White;
			tileItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
			tileItem.ItemClick += tileBarItem1_ItemClick;
			TileItemElement element = new TileItemElement();
			tileItem.Elements.Add(element);
		}
		lblComp.Text = LoginDetails.Acc_Hanger.ToString();
		LblCompAccount.Text = LoginDetails.Acc_HangerAccount.ToString();
		Account_CustSup account_CustSup = db.Account_CustSup.Where((Account_CustSup x) => x.ID == LoginDetails.Acc_Hanger).FirstOrDefault();
		if (account_CustSup != null)
		{
			TxtCompanyName.Text = account_CustSup.CustSuppName;
		}
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave);
		}
	}

	private void tileBarItem1_ItemClick(object sender, TileItemEventArgs e)
	{
		try
		{
			Restaurant_DeliveryMan restaurant_DeliveryMan = db.Restaurant_DeliveryMan.Where((Restaurant_DeliveryMan x) => x.ID == e.Item.Id && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
			TxtDeliveryMan.Text = restaurant_DeliveryMan.DeliveryName.ToString();
			LblDelivery.Text = restaurant_DeliveryMan.ID.ToString();
		}
		catch (Exception)
		{
		}
	}

	private void TxtCash_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (!ToggleDelivery.IsOn && !ToggelHanger.IsOn)
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
			if (!ToggleDelivery.IsOn && !ToggelHanger.IsOn)
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
		if (ToggleDelivery.IsOn || ToggelHanger.IsOn)
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
		if (ToggelHanger.IsOn)
		{
			TxtCash.Text = "0.00";
			TxtVisa.Text = "0.00";
			TxtCash.Enabled = false;
			TxtVisa.Enabled = false;
			toggleVisa.IsOn = false;
			toggleCash.IsOn = false;
			TxtVisa.Enabled = false;
			TxtCash.Enabled = false;
		}
	}

	private void ToggleIsOff()
	{
		if (ToggleDelivery.IsOn || ToggelHanger.IsOn)
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
		if (ToggelHanger.IsOn)
		{
			TxtCash.Text = "0.00";
			TxtVisa.Text = "0.00";
			TxtCash.Enabled = false;
			TxtVisa.Enabled = false;
			toggleVisa.IsOn = false;
			toggleCash.IsOn = false;
			TxtVisa.Enabled = false;
			TxtCash.Enabled = false;
		}
	}

	private void TxtDigital()
	{
		TxtVisa.DecimalDigits = DecimalNum;
		TxtCash.DecimalDigits = DecimalNum;
		TxtHanger.DecimalDigits = DecimalNum;
		TxtDelivery.DecimalDigits = DecimalNum;
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		if (TxtDeliveryMan.Text != "")
		{
			if (!ToggleDelivery.IsOn && !ToggelHanger.IsOn)
			{
				if (LoginDetails.TotalFatorPrice == Convert.ToDecimal(TxtCash.Text) + Convert.ToDecimal(TxtVisa.Text))
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
			MessageBox.Show("نعتذر، يجب اختيار اسم الديلفري ثم حاول مرة اخري", "مستكشف السداد ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		LoginDetails.IsComplateMony = false;
		Close();
	}

	private void ToggelHanger_Toggled(object sender, EventArgs e)
	{
		if (ToggelHanger.IsOn)
		{
			TxtHanger.Enabled = true;
			ToggleDelivery.IsOn = false;
			toggleCash.Enabled = false;
			toggleVisa.Enabled = false;
			ToggleDelivery.Enabled = false;
			TxtVisa.Enabled = false;
			TxtCash.Enabled = false;
			TxtDelivery.Enabled = false;
			toggleCash.IsOn = false;
			toggleVisa.IsOn = false;
			TxtVisa.Text = "0.00";
			TxtCash.Text = "0.00";
			TxtDelivery.Text = "0.00";
			TxtHanger.Text = LoginDetails.TotalFatorPrice.ToString();
		}
		else
		{
			TxtDelivery.Enabled = false;
			TxtHanger.Enabled = false;
			ToggleDelivery.Enabled = true;
			toggleCash.Enabled = true;
			toggleVisa.Enabled = true;
			TxtHanger.Text = "0.00";
			TxtVisa.Text = "0.00";
			TxtCash.Text = LoginDetails.TotalFatorPrice.ToString();
			toggleVisa.IsOn = false;
			toggleCash.IsOn = true;
			ToggleIsOn();
		}
	}

	private void BtnCustomer_Click(object sender, EventArgs e)
	{
		new FrmCustomer().ShowDialog();
	}

	private void TxtCustName_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		GetCustomer();
	}

	private void GetCustomer()
	{
		try
		{
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustCode > 0)
			{
				Class_Customer class_Customer = new Class_Customer();
				int FrmID = frmSearchCustomer.CustCode;
				Account_CustSup account_CustSup = (from x in class_Customer.GetAll()
					where x.ID == FrmID && x.FrmCust == true
					select x).FirstOrDefault();
				TxtCustPlace.Text = account_CustSup.Address;
				TxtCustName.Text = account_CustSup.CustSuppName;
				TxtCustPhone.Text = account_CustSup.Phone;
			}
		}
		catch (Exception)
		{
		}
	}

	private void GetCompany()
	{
		try
		{
			FrmSearchCustomer frmSearchCustomer = new FrmSearchCustomer();
			frmSearchCustomer.ShowDialog();
			if (frmSearchCustomer.CustCode > 0)
			{
				Class_Customer class_Customer = new Class_Customer();
				int FrmID = frmSearchCustomer.CustCode;
				Account_CustSup account_CustSup = (from x in class_Customer.GetAll()
					where x.ID == FrmID && x.FrmCust == true
					select x).FirstOrDefault();
				TxtCompanyName.Text = account_CustSup.CustSuppName;
				lblComp.Text = account_CustSup.ID.ToString();
				int AccountNo = Convert.ToInt32(account_CustSup.AccountNo);
				Account_Accounts account_Accounts = db.Account_Accounts.Where((Account_Accounts x) => x.Account_No == (int?)AccountNo).FirstOrDefault();
				if (account_Accounts != null)
				{
					LblCompAccount.Text = account_Accounts.ID.ToString();
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void ToggleDelivery_Toggled(object sender, EventArgs e)
	{
		if (ToggleDelivery.IsOn)
		{
			TxtDelivery.Enabled = true;
			toggleCash.IsOn = false;
			toggleVisa.IsOn = false;
			ToggelHanger.IsOn = false;
			toggleCash.Enabled = false;
			toggleVisa.Enabled = false;
			ToggelHanger.Enabled = false;
			TxtVisa.Enabled = false;
			TxtCash.Enabled = false;
			TxtHanger.Enabled = false;
			TxtVisa.Text = "0.00";
			TxtCash.Text = "0.00";
			TxtHanger.Text = "0.00";
			TxtDelivery.Text = LoginDetails.TotalFatorPrice.ToString();
		}
		else
		{
			TxtDelivery.Enabled = false;
			TxtHanger.Enabled = false;
			ToggelHanger.Enabled = true;
			toggleCash.Enabled = true;
			toggleVisa.Enabled = true;
			TxtDelivery.Text = "0.00";
			TxtHanger.Text = "0.00";
			TxtVisa.Text = "0.00";
			TxtCash.Text = LoginDetails.TotalFatorPrice.ToString();
			toggleVisa.IsOn = false;
			toggleCash.IsOn = true;
			ToggleIsOn();
		}
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		try
		{
			base.KeyPreview = true;
		}
		catch (Exception)
		{
		}
	}

	private void TxtCompanyName_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		GetCompany();
	}

	private void BtnCalcCash_Click(object sender, EventArgs e)
	{
		LoginDetails.TotalCalc = Convert.ToDecimal(TxtCash.Text);
		new FrmCalcRes().ShowDialog();
	}

	private void TxtCustPhone_KeyDown(object sender, KeyEventArgs e)
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		Account_CustSup account_CustSup = gTSdbEntities.Account_CustSup.Where((Account_CustSup x) => x.Phone == TxtCustPhone.Text).FirstOrDefault();
		if (account_CustSup != null)
		{
			SearchCustNum(Convert.ToInt32(account_CustSup.ID));
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
					TxtCustPlace.Text = account_CustSup.Address;
					TxtCustName.Text = account_CustSup.CustSuppName;
					TxtCustPhone.Text = account_CustSup.Phone;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void Frms_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F3")
		{
			new FrmCustomer().ShowDialog();
		}
		if (e.KeyCode.ToString() == "F4")
		{
			GetCustomer();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmRestaurant.FrmMoneyDelivery));
		this.lbl = new System.Windows.Forms.GroupBox();
		this.BtnCalcCash = new DevExpress.XtraEditors.SimpleButton();
		this.TxtCompanyName = new DevExpress.XtraEditors.ButtonEdit();
		this.LblCompAccount = new System.Windows.Forms.Label();
		this.lblComp = new System.Windows.Forms.Label();
		this.TxtCustName = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnCustomer = new DevExpress.XtraEditors.SimpleButton();
		this.LblDelivery = new System.Windows.Forms.Label();
		this.TxtDeliveryMan = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtCustPhone = new System.Windows.Forms.TextBox();
		this.TxtCustPlace = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
		this.ToggelHanger = new DevExpress.XtraEditors.ToggleSwitch();
		this.TxtHanger = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.CkPrintCook = new System.Windows.Forms.CheckBox();
		this.CkPrintCheck = new System.Windows.Forms.CheckBox();
		this.TxtDelivery = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
		this.ToggleDelivery = new DevExpress.XtraEditors.ToggleSwitch();
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
		this.lbl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCompanyName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCustName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ToggelHanger.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ToggleDelivery.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleVisa.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toggleCash.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl1).BeginInit();
		this.xtraTabControl1.SuspendLayout();
		this.xtraTabPage1.SuspendLayout();
		base.SuspendLayout();
		this.lbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lbl.Controls.Add(this.BtnCalcCash);
		this.lbl.Controls.Add(this.TxtCompanyName);
		this.lbl.Controls.Add(this.LblCompAccount);
		this.lbl.Controls.Add(this.lblComp);
		this.lbl.Controls.Add(this.TxtCustName);
		this.lbl.Controls.Add(this.BtnCustomer);
		this.lbl.Controls.Add(this.LblDelivery);
		this.lbl.Controls.Add(this.TxtDeliveryMan);
		this.lbl.Controls.Add(this.label4);
		this.lbl.Controls.Add(this.label3);
		this.lbl.Controls.Add(this.label2);
		this.lbl.Controls.Add(this.TxtCustPhone);
		this.lbl.Controls.Add(this.TxtCustPlace);
		this.lbl.Controls.Add(this.label1);
		this.lbl.Controls.Add(this.separatorControl2);
		this.lbl.Controls.Add(this.ToggelHanger);
		this.lbl.Controls.Add(this.TxtHanger);
		this.lbl.Controls.Add(this.CkPrintCook);
		this.lbl.Controls.Add(this.CkPrintCheck);
		this.lbl.Controls.Add(this.TxtDelivery);
		this.lbl.Controls.Add(this.separatorControl1);
		this.lbl.Controls.Add(this.ToggleDelivery);
		this.lbl.Controls.Add(this.toggleVisa);
		this.lbl.Controls.Add(this.toggleCash);
		this.lbl.Controls.Add(this.BtnExit);
		this.lbl.Controls.Add(this.BtnSave);
		this.lbl.Controls.Add(this.TxtVisa);
		this.lbl.Controls.Add(this.TxtCash);
		this.lbl.Location = new System.Drawing.Point(12, 242);
		this.lbl.Name = "lbl";
		this.lbl.Size = new System.Drawing.Size(606, 472);
		this.lbl.TabIndex = 195;
		this.lbl.TabStop = false;
		this.BtnCalcCash.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.BtnCalcCash.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcCash.ImageOptions.Image");
		this.BtnCalcCash.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcCash.Location = new System.Drawing.Point(354, 284);
		this.BtnCalcCash.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCalcCash.Name = "BtnCalcCash";
		this.BtnCalcCash.Size = new System.Drawing.Size(70, 38);
		this.BtnCalcCash.TabIndex = 92;
		this.BtnCalcCash.Text = "Calc";
		this.BtnCalcCash.Click += new System.EventHandler(BtnCalcCash_Click);
		this.TxtCompanyName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCompanyName.Location = new System.Drawing.Point(214, 216);
		this.TxtCompanyName.Name = "TxtCompanyName";
		this.TxtCompanyName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCompanyName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCompanyName.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCompanyName.Properties.Appearance.Options.UseFont = true;
		this.TxtCompanyName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCompanyName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCompanyName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.TxtCompanyName.Size = new System.Drawing.Size(175, 28);
		this.TxtCompanyName.TabIndex = 91;
		this.TxtCompanyName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCompanyName_ButtonClick);
		this.LblCompAccount.AutoSize = true;
		this.LblCompAccount.Font = new System.Drawing.Font("Tahoma", 9.8f);
		this.LblCompAccount.Location = new System.Drawing.Point(553, 404);
		this.LblCompAccount.Name = "LblCompAccount";
		this.LblCompAccount.Size = new System.Drawing.Size(19, 21);
		this.LblCompAccount.TabIndex = 90;
		this.LblCompAccount.Text = "2";
		this.LblCompAccount.Visible = false;
		this.lblComp.AutoSize = true;
		this.lblComp.Font = new System.Drawing.Font("Tahoma", 9.8f);
		this.lblComp.Location = new System.Drawing.Point(488, 404);
		this.lblComp.Name = "lblComp";
		this.lblComp.Size = new System.Drawing.Size(19, 21);
		this.lblComp.TabIndex = 89;
		this.lblComp.Text = "2";
		this.lblComp.Visible = false;
		this.TxtCustName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCustName.Location = new System.Drawing.Point(12, 73);
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
		this.TxtCustName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.TxtCustName.Size = new System.Drawing.Size(335, 28);
		this.TxtCustName.TabIndex = 88;
		this.TxtCustName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCustName_ButtonClick);
		this.BtnCustomer.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCustomer.ImageOptions.Image");
		this.BtnCustomer.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCustomer.Location = new System.Drawing.Point(478, 73);
		this.BtnCustomer.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCustomer.Name = "BtnCustomer";
		this.BtnCustomer.Size = new System.Drawing.Size(122, 28);
		this.BtnCustomer.TabIndex = 87;
		this.BtnCustomer.Text = "عميل جديد";
		this.BtnCustomer.Click += new System.EventHandler(BtnCustomer_Click);
		this.LblDelivery.AutoSize = true;
		this.LblDelivery.Font = new System.Drawing.Font("Tahoma", 9.8f);
		this.LblDelivery.Location = new System.Drawing.Point(553, 80);
		this.LblDelivery.Name = "LblDelivery";
		this.LblDelivery.Size = new System.Drawing.Size(19, 21);
		this.LblDelivery.TabIndex = 86;
		this.LblDelivery.Text = "0";
		this.LblDelivery.Visible = false;
		this.TxtDeliveryMan.BackColor = System.Drawing.Color.White;
		this.TxtDeliveryMan.Font = new System.Drawing.Font("Tahoma", 8f);
		this.TxtDeliveryMan.ForeColor = System.Drawing.Color.Teal;
		this.TxtDeliveryMan.Location = new System.Drawing.Point(428, 154);
		this.TxtDeliveryMan.Name = "TxtDeliveryMan";
		this.TxtDeliveryMan.ReadOnly = true;
		this.TxtDeliveryMan.Size = new System.Drawing.Size(172, 24);
		this.TxtDeliveryMan.TabIndex = 85;
		this.TxtDeliveryMan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 9.8f);
		this.label4.Location = new System.Drawing.Point(464, 115);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(108, 21);
		this.label4.TabIndex = 84;
		this.label4.Text = "اسم الديلفري";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9.8f);
		this.label3.Location = new System.Drawing.Point(353, 115);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(84, 21);
		this.label3.TabIndex = 83;
		this.label3.Text = "رقم الجوال";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9.8f);
		this.label2.Location = new System.Drawing.Point(353, 154);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 21);
		this.label2.TabIndex = 82;
		this.label2.Text = "المنطقة";
		this.TxtCustPhone.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCustPhone.ForeColor = System.Drawing.Color.Teal;
		this.TxtCustPhone.Location = new System.Drawing.Point(12, 111);
		this.TxtCustPhone.Name = "TxtCustPhone";
		this.TxtCustPhone.Size = new System.Drawing.Size(335, 28);
		this.TxtCustPhone.TabIndex = 81;
		this.TxtCustPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtCustPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtCustPhone_KeyDown);
		this.TxtCustPlace.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCustPlace.ForeColor = System.Drawing.Color.Teal;
		this.TxtCustPlace.Location = new System.Drawing.Point(12, 150);
		this.TxtCustPlace.Name = "TxtCustPlace";
		this.TxtCustPlace.Size = new System.Drawing.Size(335, 28);
		this.TxtCustPlace.TabIndex = 80;
		this.TxtCustPlace.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 9.8f);
		this.label1.Location = new System.Drawing.Point(353, 77);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(93, 21);
		this.label1.TabIndex = 78;
		this.label1.Text = "اسم العميل";
		this.separatorControl2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.separatorControl2.Location = new System.Drawing.Point(6, 256);
		this.separatorControl2.Name = "separatorControl2";
		this.separatorControl2.Size = new System.Drawing.Size(594, 23);
		this.separatorControl2.TabIndex = 77;
		this.ToggelHanger.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ToggelHanger.Location = new System.Drawing.Point(352, 217);
		this.ToggelHanger.Name = "ToggelHanger";
		this.ToggelHanger.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.ToggelHanger.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
		this.ToggelHanger.Properties.Appearance.Options.UseFont = true;
		this.ToggelHanger.Properties.Appearance.Options.UseForeColor = true;
		this.ToggelHanger.Properties.OffText = "إلغاء الشركات";
		this.ToggelHanger.Properties.OnText = "شركات التوصيل";
		this.ToggelHanger.Size = new System.Drawing.Size(248, 25);
		this.ToggelHanger.TabIndex = 76;
		this.ToggelHanger.Toggled += new System.EventHandler(ToggelHanger_Toggled);
		this.TxtHanger.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtHanger.DecimalDigits = 3;
		this.TxtHanger.Enabled = false;
		this.TxtHanger.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtHanger.ForeColor = System.Drawing.Color.Red;
		this.TxtHanger.Location = new System.Drawing.Point(12, 212);
		this.TxtHanger.Name = "TxtHanger";
		this.TxtHanger.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtHanger.Size = new System.Drawing.Size(196, 38);
		this.TxtHanger.TabIndex = 75;
		this.TxtHanger.Text = "0.000";
		this.TxtHanger.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtHanger.UseCompatibleTextRendering = false;
		this.TxtHanger.Value = new decimal(new int[4]);
		this.TxtHanger.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.CkPrintCook.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.CkPrintCook.AutoSize = true;
		this.CkPrintCook.Checked = true;
		this.CkPrintCook.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CkPrintCook.Location = new System.Drawing.Point(238, 384);
		this.CkPrintCook.Name = "CkPrintCook";
		this.CkPrintCook.Size = new System.Drawing.Size(109, 21);
		this.CkPrintCook.TabIndex = 72;
		this.CkPrintCook.Text = "طباعة المطبخ";
		this.CkPrintCook.UseVisualStyleBackColor = true;
		this.CkPrintCook.Visible = false;
		this.CkPrintCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.CkPrintCheck.AutoSize = true;
		this.CkPrintCheck.Checked = true;
		this.CkPrintCheck.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CkPrintCheck.Location = new System.Drawing.Point(12, 384);
		this.CkPrintCheck.Name = "CkPrintCheck";
		this.CkPrintCheck.Size = new System.Drawing.Size(106, 21);
		this.CkPrintCheck.TabIndex = 71;
		this.CkPrintCheck.Text = "طباعة الشيك";
		this.CkPrintCheck.UseVisualStyleBackColor = true;
		this.CkPrintCheck.Visible = false;
		this.TxtDelivery.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtDelivery.DecimalDigits = 3;
		this.TxtDelivery.Enabled = false;
		this.TxtDelivery.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtDelivery.ForeColor = System.Drawing.Color.Teal;
		this.TxtDelivery.Location = new System.Drawing.Point(12, 22);
		this.TxtDelivery.Name = "TxtDelivery";
		this.TxtDelivery.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDelivery.Size = new System.Drawing.Size(335, 38);
		this.TxtDelivery.TabIndex = 61;
		this.TxtDelivery.Text = "0.000";
		this.TxtDelivery.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDelivery.UseCompatibleTextRendering = false;
		this.TxtDelivery.Value = new decimal(new int[4]);
		this.TxtDelivery.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.separatorControl1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.separatorControl1.Location = new System.Drawing.Point(6, 187);
		this.separatorControl1.Name = "separatorControl1";
		this.separatorControl1.Size = new System.Drawing.Size(581, 23);
		this.separatorControl1.TabIndex = 60;
		this.ToggleDelivery.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ToggleDelivery.Location = new System.Drawing.Point(391, 22);
		this.ToggleDelivery.Name = "ToggleDelivery";
		this.ToggleDelivery.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.ToggleDelivery.Properties.Appearance.Options.UseFont = true;
		this.ToggleDelivery.Properties.OffText = "إلغاء الديلفري ";
		this.ToggleDelivery.Properties.OnText = "الديلفري";
		this.ToggleDelivery.Size = new System.Drawing.Size(206, 25);
		this.ToggleDelivery.TabIndex = 59;
		this.ToggleDelivery.Toggled += new System.EventHandler(ToggleDelivery_Toggled);
		this.toggleVisa.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.toggleVisa.Location = new System.Drawing.Point(387, 338);
		this.toggleVisa.Name = "toggleVisa";
		this.toggleVisa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.toggleVisa.Properties.Appearance.Options.UseFont = true;
		this.toggleVisa.Properties.OffText = "إلغاء الشبكة";
		this.toggleVisa.Properties.OnText = "شبكة";
		this.toggleVisa.Size = new System.Drawing.Size(201, 25);
		this.toggleVisa.TabIndex = 58;
		this.toggleVisa.Toggled += new System.EventHandler(toggleVisa_Toggled);
		this.toggleCash.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.toggleCash.EditValue = true;
		this.toggleCash.Location = new System.Drawing.Point(385, 291);
		this.toggleCash.Name = "toggleCash";
		this.toggleCash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.toggleCash.Properties.Appearance.Options.UseFont = true;
		this.toggleCash.Properties.OffText = "إلغاء النقدي";
		this.toggleCash.Properties.OnText = "نقدي";
		this.toggleCash.Size = new System.Drawing.Size(203, 25);
		this.toggleCash.TabIndex = 57;
		this.toggleCash.Toggled += new System.EventHandler(toggleCash_Toggled);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(12, 416);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(149, 40);
		this.BtnExit.TabIndex = 56;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(198, 416);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(149, 40);
		this.BtnSave.TabIndex = 51;
		this.BtnSave.Text = "حفظ";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.TxtVisa.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtVisa.DecimalDigits = 3;
		this.TxtVisa.Enabled = false;
		this.TxtVisa.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtVisa.ForeColor = System.Drawing.Color.Black;
		this.TxtVisa.Location = new System.Drawing.Point(12, 336);
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
		this.TxtCash.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtCash.DecimalDigits = 3;
		this.TxtCash.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtCash.ForeColor = System.Drawing.Color.Blue;
		this.TxtCash.Location = new System.Drawing.Point(12, 285);
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
		this.xtraTabControl1.MultiLine = DevExpress.Utils.DefaultBoolean.True;
		this.xtraTabControl1.Name = "xtraTabControl1";
		this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
		this.xtraTabControl1.Size = new System.Drawing.Size(606, 224);
		this.xtraTabControl1.TabIndex = 195;
		this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[1] { this.xtraTabPage1 });
		this.xtraTabPage1.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 15f);
		this.xtraTabPage1.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage1.Appearance.Header.Options.UseTextOptions = true;
		this.xtraTabPage1.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.xtraTabPage1.Controls.Add(this.TileRoom);
		this.xtraTabPage1.Name = "xtraTabPage1";
		this.xtraTabPage1.Size = new System.Drawing.Size(604, 180);
		this.xtraTabPage1.Text = "الديلفري";
		this.TileRoom.AllowDrag = false;
		this.TileRoom.AllowDragTilesBetweenGroups = false;
		this.TileRoom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileRoom.Groups.Add(this.GroupRoom);
		this.TileRoom.Location = new System.Drawing.Point(3, 3);
		this.TileRoom.Name = "TileRoom";
		this.TileRoom.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.TileRoom.Size = new System.Drawing.Size(593, 170);
		this.TileRoom.TabIndex = 195;
		this.TileRoom.Text = "tileControl1";
		this.GroupRoom.Name = "GroupRoom";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(630, 726);
		base.Controls.Add(this.xtraTabControl1);
		base.Controls.Add(this.lbl);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmMoneyDelivery";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = " الدفع";
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.lbl.ResumeLayout(false);
		this.lbl.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCompanyName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCustName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ToggelHanger.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ToggleDelivery.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleVisa.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toggleCash.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl1).EndInit();
		this.xtraTabControl1.ResumeLayout(false);
		this.xtraTabPage1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


