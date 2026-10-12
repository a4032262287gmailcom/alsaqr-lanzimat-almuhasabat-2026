using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using GTSErpSystem.BLL.Account;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

namespace GTSErpSystem.Frms.FrmRestaurant;

public class FrmEditRoom : XtraForm
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

	private Label label2;

	private Label LblOld;

	private XtraTabControl xtraTabControl1;

	private XtraTabPage xtraTabPage1;

	private TileControl TileRoom;

	private TileGroup GroupRoom;

	private XtraTabPage xtraTabPage2;

	private TileControl TileTable;

	private TileGroup GroupTable;

	public TextBox TxtRoomold;

	public Label LblTypeOLD;

	public Label LblRoomIDOLD;

	public NumericEditBox TxtMoneyOld;

	private XtraTabPage xtraTabPage3;

	private TileControl tileControl1;

	private TileGroup tileGroup1;

	private XtraTabPage xtraTabPage4;

	private TileControl tileControl2;

	private XtraTabControl xtraTabControl2;

	private XtraTabControl xtraTabControl3;

	private XtraTabPage xtraTabPage5;

	private TileControl TileRoom2;

	private TileGroup GroupRoom2;

	private XtraTabPage xtraTabPage6;

	private TileControl TileTable2;

	private TileGroup GroupTable2;

	private Label Lblnew;

	public TextBox TxtRoomNew;

	public Label LblTypeNew;

	public Label LblRoomIDNew;

	public Label LblPauseOLD;

	public Label LblPauseNew;

	public FrmEditRoom()
	{
		InitializeComponent();
		IsComplateMony = false;
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
			if (item.ISPause == true)
			{
				tileItem.Appearance.BackColor = Color.Red;
				tileItem.Appearance.ForeColor = Color.White;
			}
		}
		GroupRoom2.Items.Clear();
		foreach (Restaurant_Room item2 in list)
		{
			TileItem tileItem2 = new TileItem();
			TileRoom2.ItemSize = 70;
			GroupRoom2.Items.Add(tileItem2);
			tileItem2.Text = item2.Name;
			tileItem2.Id = item2.ID;
			tileItem2.ItemSize = TileItemSize.Wide;
			tileItem2.AllowGlyphSkinning = DefaultBoolean.True;
			tileItem2.Appearance.Font = new Font("Tahoma", 12.35f);
			tileItem2.Appearance.BackColor = Color.Green;
			tileItem2.Appearance.ForeColor = Color.White;
			tileItem2.TextAlignment = TileItemContentAlignment.MiddleCenter;
			tileItem2.ItemClick += tileBarItem3_ItemClick;
			TileItemElement element2 = new TileItemElement();
			tileItem2.Elements.Add(element2);
			if (item2.ISPause == true)
			{
				tileItem2.Appearance.BackColor = Color.Red;
				tileItem2.Appearance.ForeColor = Color.White;
			}
		}
		List<Restaurant_Table> list2 = db.Restaurant_Table.Where((Restaurant_Table x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
		GroupTable.Items.Clear();
		foreach (Restaurant_Table item3 in list2)
		{
			TileItem tileItem3 = new TileItem();
			TileTable.ItemSize = 70;
			GroupTable.Items.Add(tileItem3);
			tileItem3.Text = item3.Name;
			tileItem3.Id = item3.ID;
			tileItem3.ItemSize = TileItemSize.Wide;
			tileItem3.AllowGlyphSkinning = DefaultBoolean.True;
			tileItem3.Appearance.Font = new Font("Tahoma", 12.35f);
			tileItem3.Appearance.BackColor = Color.Blue;
			tileItem3.Appearance.ForeColor = Color.White;
			tileItem3.TextAlignment = TileItemContentAlignment.MiddleCenter;
			tileItem3.ItemClick += tileBarItem2_ItemClick;
			TileItemElement element3 = new TileItemElement();
			tileItem3.Elements.Add(element3);
			if (item3.ISPause == true)
			{
				tileItem3.Appearance.BackColor = Color.Red;
				tileItem3.Appearance.ForeColor = Color.White;
			}
		}
		GroupTable2.Items.Clear();
		foreach (Restaurant_Table item4 in list2)
		{
			TileItem tileItem4 = new TileItem();
			TileTable2.ItemSize = 70;
			GroupTable2.Items.Add(tileItem4);
			tileItem4.Text = item4.Name;
			tileItem4.Id = item4.ID;
			tileItem4.ItemSize = TileItemSize.Wide;
			tileItem4.AllowGlyphSkinning = DefaultBoolean.True;
			tileItem4.Appearance.Font = new Font("Tahoma", 12.35f);
			tileItem4.Appearance.BackColor = Color.Blue;
			tileItem4.Appearance.ForeColor = Color.White;
			tileItem4.TextAlignment = TileItemContentAlignment.MiddleCenter;
			tileItem4.ItemClick += tileBarItem4_ItemClick;
			TileItemElement element4 = new TileItemElement();
			tileItem4.Elements.Add(element4);
			if (item4.ISPause == true)
			{
				tileItem4.Appearance.BackColor = Color.Red;
				tileItem4.Appearance.ForeColor = Color.White;
			}
		}
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
			LblOld.Text = "رقم الغرفة القديمة";
			LblTypeOLD.Text = "1";
			TxtRoomold.Text = restaurant_Room.Name;
			LblRoomIDOLD.Text = restaurant_Room.ID.ToString();
		}
		catch (Exception)
		{
		}
	}

	private void tileBarItem3_ItemClick(object sender, TileItemEventArgs e)
	{
		try
		{
			Restaurant_Room restaurant_Room = db.Restaurant_Room.Where((Restaurant_Room x) => x.ID == e.Item.Id && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
			Lblnew.Text = "رقم الغرفة الجديدة";
			if (restaurant_Room.ISPause == true)
			{
				MessageBox.Show("عفوا، لايمكن اختيار الغرفة لانها مشغولة الان الرجاء المحاولة مرة أخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return;
			}
			LblTypeNew.Text = "3";
			TxtRoomNew.Text = restaurant_Room.Name;
			LblRoomIDNew.Text = restaurant_Room.ID.ToString();
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
			LblOld.Text = "رقم الطاولة القديمة";
			LblTypeOLD.Text = "2";
			TxtRoomold.Text = restaurant_Table.Name;
			LblRoomIDOLD.Text = restaurant_Table.ID.ToString();
		}
		catch (Exception)
		{
		}
	}

	private void tileBarItem4_ItemClick(object sender, TileItemEventArgs e)
	{
		try
		{
			Restaurant_Table restaurant_Table = db.Restaurant_Table.Where((Restaurant_Table x) => x.ID == e.Item.Id && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
			Lblnew.Text = "رقم الطاولة الجديدة";
			if (restaurant_Table.ISPause == true)
			{
				MessageBox.Show("عفوا، لايمكن اختيار الطاولة لانها مشغولة الان الرجاء المحاولة مرة أخري", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return;
			}
			LblTypeNew.Text = "4";
			TxtRoomNew.Text = restaurant_Table.Name;
			LblRoomIDNew.Text = restaurant_Table.ID.ToString();
		}
		catch (Exception)
		{
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		try
		{
			if (TxtRoomold.Text != string.Empty || TxtRoomNew.Text != string.Empty)
			{
				int iDOLD = Convert.ToInt32(LblRoomIDOLD.Text);
				int iDNew = Convert.ToInt32(LblRoomIDNew.Text);
				int typeOLD = Convert.ToInt32(LblTypeOLD.Text);
				int typeNew = Convert.ToInt32(LblTypeNew.Text);
				UpdateRoom(iDOLD, iDNew, typeOLD, typeNew);
				MessageBox.Show("تم التعديل بنجاح، شكرا لك", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				Close();
				return;
			}
			LoginDetails.IsComplateMony = false;
			if (TxtRoomold.Text != string.Empty)
			{
				MessageBox.Show("نعتذر، يجب اختيار رقم الغرفة القديمة / رقم الطاولة القديمة ثم حاول مرة اخري", "مستكشف السداد ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtRoomold.Focus();
			}
			if (TxtRoomNew.Text != string.Empty)
			{
				MessageBox.Show("نعتذر، يجب اختيار رقم الغرفة الجديدة / رقم الطاولة الجديدة ثم حاول مرة اخري", "مستكشف السداد ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtRoomNew.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "مستكشف ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		LoginDetails.IsComplateMony = false;
		Close();
	}

	public int UpdateRoom(int IDOLD, int IDNew, int TypeOLD, int TypeNew)
	{
		int result = 0;
		Restaurant_Room restaurant_Room = db.Restaurant_Room.Where((Restaurant_Room x) => x.ID == IDOLD && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Restaurant_Room restaurant_Room2 = db.Restaurant_Room.Where((Restaurant_Room x) => x.ID == IDNew && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Restaurant_Table restaurant_Table = db.Restaurant_Table.Where((Restaurant_Table x) => x.ID == IDOLD && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		Restaurant_Table restaurant_Table2 = db.Restaurant_Table.Where((Restaurant_Table x) => x.ID == IDNew && x.BranchID == (int?)LoginDetails.BranchID).FirstOrDefault();
		if (TypeOLD == 1 && TypeNew == 3 && restaurant_Room != null && restaurant_Room2 != null)
		{
			restaurant_Room2.OrderID = restaurant_Room.OrderID;
			restaurant_Room2.ISPause = true;
			restaurant_Room2.Money = restaurant_Room.Money;
			restaurant_Room2.Time1 = DateTime.Now;
			db.SaveChanges();
			restaurant_Room.OrderID = 0;
			restaurant_Room.ISPause = false;
			restaurant_Room.Money = 0m;
			restaurant_Room.Time1 = DateTime.Now;
			db.SaveChanges();
			result = 1;
		}
		if (TypeOLD == 1 && TypeNew == 4 && restaurant_Room != null && restaurant_Table2 != null)
		{
			restaurant_Table2.OrderID = restaurant_Room.OrderID;
			restaurant_Table2.ISPause = true;
			restaurant_Table2.Money = restaurant_Room.Money;
			restaurant_Table2.Time1 = DateTime.Now;
			db.SaveChanges();
			restaurant_Room.OrderID = 0;
			restaurant_Room.ISPause = false;
			restaurant_Room.Money = 0m;
			restaurant_Room.Time1 = DateTime.Now;
			db.SaveChanges();
			result = 1;
		}
		if (TypeOLD == 2 && TypeNew == 4 && restaurant_Table != null && restaurant_Table2 != null)
		{
			restaurant_Table2.OrderID = restaurant_Table.OrderID;
			restaurant_Table2.ISPause = true;
			restaurant_Table2.Money = restaurant_Table.Money;
			restaurant_Table2.Time1 = DateTime.Now;
			db.SaveChanges();
			restaurant_Table.OrderID = 0;
			restaurant_Table.ISPause = false;
			restaurant_Table.Money = 0m;
			restaurant_Table.Time1 = DateTime.Now;
			db.SaveChanges();
			result = 1;
		}
		if (TypeOLD == 2 && TypeNew == 3)
		{
			if (restaurant_Room != null && restaurant_Table2 != null)
			{
				restaurant_Room2.OrderID = restaurant_Table.OrderID;
				restaurant_Room2.ISPause = true;
				restaurant_Room2.Money = restaurant_Table.Money;
				restaurant_Room2.Time1 = DateTime.Now;
				db.SaveChanges();
				restaurant_Table.OrderID = 0;
				restaurant_Table.ISPause = false;
				restaurant_Table.Money = 0m;
				restaurant_Table.Time1 = DateTime.Now;
				db.SaveChanges();
				result = 1;
			}
		}
		else
		{
			result = 0;
		}
		return result;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmRestaurant.FrmEditRoom));
		this.lbl = new System.Windows.Forms.GroupBox();
		this.LblRoomIDOLD = new System.Windows.Forms.Label();
		this.LblTypeOLD = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.LblOld = new System.Windows.Forms.Label();
		this.TxtRoomold = new System.Windows.Forms.TextBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
		this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
		this.TileRoom = new DevExpress.XtraEditors.TileControl();
		this.GroupRoom = new DevExpress.XtraEditors.TileGroup();
		this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
		this.TileTable = new DevExpress.XtraEditors.TileControl();
		this.GroupTable = new DevExpress.XtraEditors.TileGroup();
		this.TxtMoneyOld = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
		this.tileControl1 = new DevExpress.XtraEditors.TileControl();
		this.tileGroup1 = new DevExpress.XtraEditors.TileGroup();
		this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
		this.tileControl2 = new DevExpress.XtraEditors.TileControl();
		this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
		this.xtraTabControl3 = new DevExpress.XtraTab.XtraTabControl();
		this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
		this.TileRoom2 = new DevExpress.XtraEditors.TileControl();
		this.GroupRoom2 = new DevExpress.XtraEditors.TileGroup();
		this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
		this.TileTable2 = new DevExpress.XtraEditors.TileControl();
		this.GroupTable2 = new DevExpress.XtraEditors.TileGroup();
		this.Lblnew = new System.Windows.Forms.Label();
		this.TxtRoomNew = new System.Windows.Forms.TextBox();
		this.LblTypeNew = new System.Windows.Forms.Label();
		this.LblRoomIDNew = new System.Windows.Forms.Label();
		this.LblPauseOLD = new System.Windows.Forms.Label();
		this.LblPauseNew = new System.Windows.Forms.Label();
		this.lbl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl1).BeginInit();
		this.xtraTabControl1.SuspendLayout();
		this.xtraTabPage1.SuspendLayout();
		this.xtraTabPage2.SuspendLayout();
		this.xtraTabPage3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl2).BeginInit();
		this.xtraTabControl2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl3).BeginInit();
		this.xtraTabControl3.SuspendLayout();
		this.xtraTabPage5.SuspendLayout();
		this.xtraTabPage6.SuspendLayout();
		base.SuspendLayout();
		this.lbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lbl.Controls.Add(this.LblPauseNew);
		this.lbl.Controls.Add(this.LblPauseOLD);
		this.lbl.Controls.Add(this.LblRoomIDNew);
		this.lbl.Controls.Add(this.LblTypeNew);
		this.lbl.Controls.Add(this.Lblnew);
		this.lbl.Controls.Add(this.TxtRoomNew);
		this.lbl.Controls.Add(this.LblRoomIDOLD);
		this.lbl.Controls.Add(this.LblTypeOLD);
		this.lbl.Controls.Add(this.label2);
		this.lbl.Controls.Add(this.LblOld);
		this.lbl.Controls.Add(this.TxtRoomold);
		this.lbl.Controls.Add(this.TxtMoneyOld);
		this.lbl.Controls.Add(this.BtnExit);
		this.lbl.Controls.Add(this.BtnSave);
		this.lbl.Location = new System.Drawing.Point(12, 563);
		this.lbl.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.lbl.Name = "lbl";
		this.lbl.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.lbl.Size = new System.Drawing.Size(606, 142);
		this.lbl.TabIndex = 195;
		this.lbl.TabStop = false;
		this.LblRoomIDOLD.AutoSize = true;
		this.LblRoomIDOLD.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblRoomIDOLD.Location = new System.Drawing.Point(579, 73);
		this.LblRoomIDOLD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblRoomIDOLD.Name = "LblRoomIDOLD";
		this.LblRoomIDOLD.Size = new System.Drawing.Size(19, 21);
		this.LblRoomIDOLD.TabIndex = 74;
		this.LblRoomIDOLD.Text = "1";
		this.LblRoomIDOLD.Visible = false;
		this.LblTypeOLD.AutoSize = true;
		this.LblTypeOLD.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblTypeOLD.Location = new System.Drawing.Point(521, 73);
		this.LblTypeOLD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblTypeOLD.Name = "LblTypeOLD";
		this.LblTypeOLD.Size = new System.Drawing.Size(19, 21);
		this.LblTypeOLD.TabIndex = 70;
		this.LblTypeOLD.Text = "1";
		this.LblTypeOLD.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.label2.Location = new System.Drawing.Point(471, 94);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(119, 21);
		this.label2.TabIndex = 69;
		this.label2.Text = "اجمالي السابق";
		this.LblOld.AutoSize = true;
		this.LblOld.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblOld.Location = new System.Drawing.Point(471, 37);
		this.LblOld.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblOld.Name = "LblOld";
		this.LblOld.Size = new System.Drawing.Size(130, 21);
		this.LblOld.TabIndex = 68;
		this.LblOld.Text = "رقم الغرفة القديم";
		this.TxtRoomold.BackColor = System.Drawing.Color.White;
		this.TxtRoomold.Font = new System.Drawing.Font("Tahoma", 15f);
		this.TxtRoomold.ForeColor = System.Drawing.Color.Teal;
		this.TxtRoomold.Location = new System.Drawing.Point(326, 26);
		this.TxtRoomold.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtRoomold.Name = "TxtRoomold";
		this.TxtRoomold.ReadOnly = true;
		this.TxtRoomold.Size = new System.Drawing.Size(137, 38);
		this.TxtRoomold.TabIndex = 67;
		this.TxtRoomold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(12, 85);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(149, 39);
		this.BtnExit.TabIndex = 56;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(169, 85);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(149, 39);
		this.BtnSave.TabIndex = 51;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.xtraTabControl1.Location = new System.Drawing.Point(12, 11);
		this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabControl1.MultiLine = DevExpress.Utils.DefaultBoolean.True;
		this.xtraTabControl1.Name = "xtraTabControl1";
		this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
		this.xtraTabControl1.Size = new System.Drawing.Size(606, 273);
		this.xtraTabControl1.TabIndex = 195;
		this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[2] { this.xtraTabPage1, this.xtraTabPage2 });
		this.xtraTabPage1.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 15f);
		this.xtraTabPage1.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage1.Appearance.Header.Options.UseTextOptions = true;
		this.xtraTabPage1.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.xtraTabPage1.Controls.Add(this.TileRoom);
		this.xtraTabPage1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabPage1.Name = "xtraTabPage1";
		this.xtraTabPage1.Size = new System.Drawing.Size(604, 229);
		this.xtraTabPage1.Text = "الغرف القديمة";
		this.TileRoom.AllowDrag = false;
		this.TileRoom.AllowDragTilesBetweenGroups = false;
		this.TileRoom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileRoom.Groups.Add(this.GroupRoom);
		this.TileRoom.Location = new System.Drawing.Point(4, 2);
		this.TileRoom.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TileRoom.Name = "TileRoom";
		this.TileRoom.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.TileRoom.Size = new System.Drawing.Size(593, 218);
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
		this.xtraTabPage2.Size = new System.Drawing.Size(604, 229);
		this.xtraTabPage2.Text = "الطاولات القديمة";
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
		this.TxtMoneyOld.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.TxtMoneyOld.DecimalDigits = 3;
		this.TxtMoneyOld.Font = new System.Drawing.Font("Tahoma", 15f, System.Drawing.FontStyle.Bold);
		this.TxtMoneyOld.ForeColor = System.Drawing.Color.Teal;
		this.TxtMoneyOld.Location = new System.Drawing.Point(326, 86);
		this.TxtMoneyOld.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtMoneyOld.Name = "TxtMoneyOld";
		this.TxtMoneyOld.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMoneyOld.Size = new System.Drawing.Size(137, 38);
		this.TxtMoneyOld.TabIndex = 66;
		this.TxtMoneyOld.Text = "0.000";
		this.TxtMoneyOld.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtMoneyOld.UseCompatibleTextRendering = false;
		this.TxtMoneyOld.Value = new decimal(new int[4]);
		this.TxtMoneyOld.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.xtraTabPage3.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 15f);
		this.xtraTabPage3.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage3.Appearance.Header.Options.UseTextOptions = true;
		this.xtraTabPage3.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.xtraTabPage3.Controls.Add(this.tileControl1);
		this.xtraTabPage3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabPage3.Name = "xtraTabPage3";
		this.xtraTabPage3.Size = new System.Drawing.Size(604, 229);
		this.xtraTabPage3.Text = "الغرف";
		this.tileControl1.AllowDrag = false;
		this.tileControl1.AllowDragTilesBetweenGroups = false;
		this.tileControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tileControl1.Groups.Add(this.tileGroup1);
		this.tileControl1.Location = new System.Drawing.Point(4, 2);
		this.tileControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.tileControl1.Name = "tileControl1";
		this.tileControl1.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.tileControl1.Size = new System.Drawing.Size(593, 218);
		this.tileControl1.TabIndex = 195;
		this.tileControl1.Text = "tileControl1";
		this.tileGroup1.Name = "tileGroup1";
		this.xtraTabPage4.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 15f);
		this.xtraTabPage4.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage4.Appearance.Header.Options.UseTextOptions = true;
		this.xtraTabPage4.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.xtraTabPage4.Name = "xtraTabPage4";
		this.xtraTabPage4.Size = new System.Drawing.Size(0, 0);
		this.tileControl2.AllowDrag = false;
		this.tileControl2.AllowDragTilesBetweenGroups = false;
		this.tileControl2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tileControl2.Location = new System.Drawing.Point(0, 0);
		this.tileControl2.Name = "tileControl2";
		this.tileControl2.Size = new System.Drawing.Size(240, 150);
		this.tileControl2.TabIndex = 0;
		this.xtraTabControl2.Location = new System.Drawing.Point(12, 12);
		this.xtraTabControl2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabControl2.MultiLine = DevExpress.Utils.DefaultBoolean.True;
		this.xtraTabControl2.Name = "xtraTabControl2";
		this.xtraTabControl2.SelectedTabPage = this.xtraTabPage3;
		this.xtraTabControl2.Size = new System.Drawing.Size(606, 273);
		this.xtraTabControl2.TabIndex = 195;
		this.xtraTabControl3.Location = new System.Drawing.Point(12, 288);
		this.xtraTabControl3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabControl3.MultiLine = DevExpress.Utils.DefaultBoolean.True;
		this.xtraTabControl3.Name = "xtraTabControl3";
		this.xtraTabControl3.SelectedTabPage = this.xtraTabPage5;
		this.xtraTabControl3.Size = new System.Drawing.Size(606, 273);
		this.xtraTabControl3.TabIndex = 196;
		this.xtraTabControl3.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[2] { this.xtraTabPage5, this.xtraTabPage6 });
		this.xtraTabPage5.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 15f);
		this.xtraTabPage5.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage5.Appearance.Header.Options.UseTextOptions = true;
		this.xtraTabPage5.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.xtraTabPage5.Controls.Add(this.TileRoom2);
		this.xtraTabPage5.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabPage5.Name = "xtraTabPage5";
		this.xtraTabPage5.Size = new System.Drawing.Size(604, 229);
		this.xtraTabPage5.Text = "الغرف الجديدة";
		this.TileRoom2.AllowDrag = false;
		this.TileRoom2.AllowDragTilesBetweenGroups = false;
		this.TileRoom2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileRoom2.Groups.Add(this.GroupRoom2);
		this.TileRoom2.Location = new System.Drawing.Point(4, 2);
		this.TileRoom2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TileRoom2.Name = "TileRoom2";
		this.TileRoom2.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.TileRoom2.Size = new System.Drawing.Size(593, 218);
		this.TileRoom2.TabIndex = 195;
		this.TileRoom2.Text = "tileControl1";
		this.GroupRoom2.Name = "GroupRoom2";
		this.xtraTabPage6.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 15f);
		this.xtraTabPage6.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage6.Appearance.Header.Options.UseTextOptions = true;
		this.xtraTabPage6.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.xtraTabPage6.Controls.Add(this.TileTable2);
		this.xtraTabPage6.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.xtraTabPage6.Name = "xtraTabPage6";
		this.xtraTabPage6.Size = new System.Drawing.Size(604, 229);
		this.xtraTabPage6.Text = "الطاولات الجديدة";
		this.TileTable2.AllowDrag = false;
		this.TileTable2.AllowDragTilesBetweenGroups = false;
		this.TileTable2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileTable2.Groups.Add(this.GroupTable2);
		this.TileTable2.Location = new System.Drawing.Point(4, 2);
		this.TileTable2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TileTable2.Name = "TileTable2";
		this.TileTable2.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar;
		this.TileTable2.Size = new System.Drawing.Size(593, 247);
		this.TileTable2.TabIndex = 196;
		this.TileTable2.Text = "tileControl2";
		this.GroupTable2.Name = "GroupTable2";
		this.Lblnew.AutoSize = true;
		this.Lblnew.Font = new System.Drawing.Font("Tahoma", 10f);
		this.Lblnew.Location = new System.Drawing.Point(157, 37);
		this.Lblnew.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.Lblnew.Name = "Lblnew";
		this.Lblnew.Size = new System.Drawing.Size(130, 21);
		this.Lblnew.TabIndex = 76;
		this.Lblnew.Text = "رقم الغرفة الجديد";
		this.TxtRoomNew.BackColor = System.Drawing.Color.White;
		this.TxtRoomNew.Font = new System.Drawing.Font("Tahoma", 15f);
		this.TxtRoomNew.ForeColor = System.Drawing.Color.Teal;
		this.TxtRoomNew.Location = new System.Drawing.Point(12, 26);
		this.TxtRoomNew.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtRoomNew.Name = "TxtRoomNew";
		this.TxtRoomNew.ReadOnly = true;
		this.TxtRoomNew.Size = new System.Drawing.Size(137, 38);
		this.TxtRoomNew.TabIndex = 75;
		this.TxtRoomNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.LblTypeNew.AutoSize = true;
		this.LblTypeNew.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblTypeNew.Location = new System.Drawing.Point(494, 73);
		this.LblTypeNew.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblTypeNew.Name = "LblTypeNew";
		this.LblTypeNew.Size = new System.Drawing.Size(19, 21);
		this.LblTypeNew.TabIndex = 77;
		this.LblTypeNew.Text = "1";
		this.LblTypeNew.Visible = false;
		this.LblRoomIDNew.AutoSize = true;
		this.LblRoomIDNew.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblRoomIDNew.Location = new System.Drawing.Point(552, 73);
		this.LblRoomIDNew.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblRoomIDNew.Name = "LblRoomIDNew";
		this.LblRoomIDNew.Size = new System.Drawing.Size(19, 21);
		this.LblRoomIDNew.TabIndex = 78;
		this.LblRoomIDNew.Text = "1";
		this.LblRoomIDNew.Visible = false;
		this.LblPauseOLD.AutoSize = true;
		this.LblPauseOLD.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblPauseOLD.Location = new System.Drawing.Point(284, 60);
		this.LblPauseOLD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblPauseOLD.Name = "LblPauseOLD";
		this.LblPauseOLD.Size = new System.Drawing.Size(19, 21);
		this.LblPauseOLD.TabIndex = 79;
		this.LblPauseOLD.Text = "0";
		this.LblPauseOLD.Visible = false;
		this.LblPauseNew.AutoSize = true;
		this.LblPauseNew.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblPauseNew.Location = new System.Drawing.Point(240, 60);
		this.LblPauseNew.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.LblPauseNew.Name = "LblPauseNew";
		this.LblPauseNew.Size = new System.Drawing.Size(19, 21);
		this.LblPauseNew.TabIndex = 80;
		this.LblPauseNew.Text = "0";
		this.LblPauseNew.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(630, 718);
		base.Controls.Add(this.xtraTabControl3);
		base.Controls.Add(this.xtraTabControl1);
		base.Controls.Add(this.lbl);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		base.MaximizeBox = false;
		base.Name = "FrmEditRoom";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تعديل الغرف والطاولات";
		this.lbl.ResumeLayout(false);
		this.lbl.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl1).EndInit();
		this.xtraTabControl1.ResumeLayout(false);
		this.xtraTabPage1.ResumeLayout(false);
		this.xtraTabPage2.ResumeLayout(false);
		this.xtraTabPage3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl2).EndInit();
		this.xtraTabControl2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.xtraTabControl3).EndInit();
		this.xtraTabControl3.ResumeLayout(false);
		this.xtraTabPage5.ResumeLayout(false);
		this.xtraTabPage6.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


