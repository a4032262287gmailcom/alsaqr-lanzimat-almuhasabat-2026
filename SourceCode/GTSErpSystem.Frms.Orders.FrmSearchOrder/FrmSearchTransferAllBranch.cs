using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.Frms.FrmRestaurant;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Orders.FrmSearchOrder;

public class FrmSearchTransferAllBranch : XtraForm
{
	private Class_StoresTransfer Userx = new Class_StoresTransfer();

	private Class_Orders ORD = new Class_Orders();

	public int ItemID;

	private int DecimalNumQty = LoginDetails.DecimalNumQty;

	private IContainer components = null;

	private GroupBox groupBox1;

	public GridEX gridEX1;

	private GroupBox groupBox2;

	private Label label13;

	private Label label14;

	private Label label16;

	private Label label9;

	private Label label11;

	private Label label12;

	private TextEdit TxtLocation;

	private Label label25;

	private Label LabelItemName;

	private ButtonEdit TxtSellPrice1;

	private ButtonEdit TxtSellPrice3;

	private ButtonEdit TxtSellPrice2;

	private TextEdit ItemName;

	private ButtonEdit TxtAllQuantity;

	private ButtonEdit TxtAvergCost;

	private ButtonEdit TxtLastCost;

	private GroupBox groupBox4;

	private Label label5;

	private ButtonEdit LastPrice;

	private ButtonEdit LastQuantity;

	private TextEdit TxtLastCustomer;

	private Label label34;

	private Label label2;

	private GroupBox groupBox5;

	private Label label6;

	private ButtonEdit SallPrice;

	private ButtonEdit Quantity;

	private TextEdit TxtCurrentCustomer;

	private Label label7;

	private Label label8;

	public int ItmID { get; set; }

	public int StorId { get; set; }

	public decimal CurrentQuantity { get; set; }

	public decimal UnitPrice { get; set; }

	public string UnitName { get; set; }

	public string CustName { get; set; }

	public FrmSearchTransferAllBranch()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
		}
		InitializeComponent();
		ItemID = LoginDetails.ItemcodeForStore;
		BindGrid();
	}

	public FrmSearchTransferAllBranch(int itmID, int storId, decimal currentQuantity, decimal unitPrice, string unitName, string custName)
	{
		InitializeComponent();
		ItemID = LoginDetails.ItemcodeForStore;
		BindGrid();
		ItmID = itmID;
		StorId = storId;
		UnitName = unitName;
		UnitPrice = unitPrice;
		CustName = custName;
		CurrentQuantity = currentQuantity;
		GetAllitems_alternatives(ItmID, StorId, UnitName);
	}

	public void BindGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("اسم الفرع", typeof(string));
			dataTable.Columns.Add("اسم المخزن", typeof(string));
			dataTable.Columns.Add("الكمية المتوفرة", typeof(double));
			foreach (GetItemQuantityByBranch_Result item in Userx.GetItemQuantity(ItemID))
			{
				ItemName.Text = item.ItemName;
				dataTable.Rows.Add(item.BranchName, item.StoreName, item.CurrentBalance);
			}
			gridEX1.SetDataBinding(dataTable, string.Empty);
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["اسم الفرع"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["اسم المخزن"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الكمية المتوفرة"].TextAlignment = TextAlignment.Center;
			return;
		}
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable2 = new DataTable("gridEX1");
		dataTable2.Columns.Add("Branch Name", typeof(string));
		dataTable2.Columns.Add("Store Name", typeof(string));
		dataTable2.Columns.Add("Quantity", typeof(double));
		foreach (GetItemQuantityByBranch_Result item2 in Userx.GetItemQuantity(ItemID))
		{
			dataTable2.Rows.Add(item2.ItemName, item2.BranchName, item2.StoreName, item2.CurrentBalance);
		}
		gridEX1.SetDataBinding(dataTable2, string.Empty);
		gridEX1.RetrieveStructure();
	}

	private void gridEX1_DoubleClick(object sender, EventArgs e)
	{
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
	}

	private void Frms_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			Close();
		}
	}

	private decimal FUNC_DecimalNumQty(decimal? Value)
	{
		decimal num;
		return num = Math.Round(Convert.ToDecimal(Value), DecimalNumQty);
	}

	public void GetAllitems_alternatives(int ItmID, int StoreID, string Type)
	{
		try
		{
			int unitType = 1;
			if (Type == "المتوسطة")
			{
				unitType = 2;
			}
			if (Type == "الكبرى")
			{
				unitType = 3;
			}
			GetAllitems_Alternatives_Result getAllitems_Alternatives_Result = ORD.GetAllitems_Alternatives(ItmID, StoreID, unitType).FirstOrDefault();
			GetLastOrderDetailsByItemID_Result getLastOrderDetailsByItemID_Result = ORD.GetLastOrderDetailsByItemID(ItmID).FirstOrDefault();
			if (getAllitems_Alternatives_Result != null)
			{
				TxtAllQuantity.Text = (getAllitems_Alternatives_Result.AllQuantity.HasValue ? FUNC_DecimalNumQty(getAllitems_Alternatives_Result.AllQuantity).ToString() : string.Empty);
				ButtonEdit txtAvergCost = TxtAvergCost;
				_ = getAllitems_Alternatives_Result.Average_cost;
				txtAvergCost.Text = FUNC_DecimalNumQty(getAllitems_Alternatives_Result.Average_cost).ToString();
				ButtonEdit txtLastCost = TxtLastCost;
				_ = getAllitems_Alternatives_Result.LastCost;
				txtLastCost.Text = FUNC_DecimalNumQty(getAllitems_Alternatives_Result.LastCost).ToString();
				ButtonEdit txtSellPrice = TxtSellPrice1;
				_ = getAllitems_Alternatives_Result.SellPriceSmall;
				txtSellPrice.Text = FUNC_DecimalNumQty(getAllitems_Alternatives_Result.SellPriceSmall).ToString();
				ButtonEdit txtSellPrice2 = TxtSellPrice2;
				_ = getAllitems_Alternatives_Result.SellPriceSmall2;
				txtSellPrice2.Text = FUNC_DecimalNumQty(getAllitems_Alternatives_Result.SellPriceSmall2).ToString();
				ButtonEdit txtSellPrice3 = TxtSellPrice3;
				_ = getAllitems_Alternatives_Result.SellPriceSmall3;
				txtSellPrice3.Text = FUNC_DecimalNumQty(getAllitems_Alternatives_Result.SellPriceSmall3).ToString();
				TxtLastCustomer.Text = ((getLastOrderDetailsByItemID_Result.SuppilerName != null) ? getLastOrderDetailsByItemID_Result.SuppilerName.ToString() : string.Empty);
				LastQuantity.Text = (getLastOrderDetailsByItemID_Result.Quantity.HasValue ? FUNC_DecimalNumQty(getLastOrderDetailsByItemID_Result.Quantity).ToString() : string.Empty);
				LastPrice.Text = (getLastOrderDetailsByItemID_Result.NetUnitPrice.HasValue ? FUNC_DecimalNumQty(getLastOrderDetailsByItemID_Result.NetUnitPrice).ToString() : string.Empty);
				TxtCurrentCustomer.Text = CustName;
				Quantity.Text = FUNC_DecimalNumQty(CurrentQuantity).ToString();
				SallPrice.Text = FUNC_DecimalNumQty(UnitPrice).ToString();
			}
			else
			{
				TxtAllQuantity.Text = FUNC_DecimalNumQty(0m).ToString();
				TxtAvergCost.Text = "0.00";
				TxtLastCost.Text = "0.00";
				TxtSellPrice1.Text = "0.00";
				TxtSellPrice2.Text = "0.00";
				TxtSellPrice3.Text = "0.00";
				TxtLocation.Text = string.Empty;
			}
		}
		catch (Exception)
		{
		}
	}

	private void SetPrice(string TextName)
	{
		decimal num = FUNC_DecimalNumQty(Convert.ToDecimal(TextName));
		if (base.Owner != null && base.Owner is FrmOrders)
		{
			FrmOrders frmOrders = (FrmOrders)base.Owner;
			if (frmOrders.gridControl1.MainView is GridView gridView)
			{
				CellValueChangedEventArgs e = new CellValueChangedEventArgs(gridView.FocusedRowHandle, gridView.FocusedColumn, num);
				frmOrders.UpdateUnitPriceInGrid(e);
			}
		}
		else if (base.Owner != null && base.Owner is FrmPriceOffer)
		{
			FrmPriceOffer frmPriceOffer = (FrmPriceOffer)base.Owner;
			if (frmPriceOffer.gridControl1.MainView is GridView gridView2)
			{
				CellValueChangedEventArgs e2 = new CellValueChangedEventArgs(gridView2.FocusedRowHandle, gridView2.FocusedColumn, num);
				frmPriceOffer.UpdateUnitPriceInGrid(e2);
			}
		}
		else if (base.Owner != null && base.Owner is FrmCashir)
		{
			FrmCashir frmCashir = (FrmCashir)base.Owner;
			if (frmCashir.gridControl1.MainView is GridView gridView3)
			{
				CellValueChangedEventArgs e3 = new CellValueChangedEventArgs(gridView3.FocusedRowHandle, gridView3.FocusedColumn, num);
				frmCashir.UpdateUnitPriceInGrid(e3);
			}
		}
		else if (base.Owner != null && base.Owner is FrmCashirRestaurant)
		{
			FrmCashirRestaurant frmCashirRestaurant = (FrmCashirRestaurant)base.Owner;
			if (frmCashirRestaurant.gridControl1.MainView is GridView gridView4)
			{
				CellValueChangedEventArgs e4 = new CellValueChangedEventArgs(gridView4.FocusedRowHandle, gridView4.FocusedColumn, num);
				frmCashirRestaurant.UpdateUnitPriceInGrid(e4);
			}
		}
		Close();
	}

	private void TxtSellPrice1_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SetPrice(TxtSellPrice1.Text);
	}

	private void TxtSellPrice2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SetPrice(TxtSellPrice2.Text);
	}

	private void TxtLastCost_ButtonClick_1(object sender, ButtonPressedEventArgs e)
	{
		SetPrice(TxtLastCost.Text);
	}

	private void TxtSellPrice3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SetPrice(TxtSellPrice3.Text);
	}

	private void LastPrice_ButtonClick_1(object sender, ButtonPressedEventArgs e)
	{
		SetPrice(LastPrice.Text);
	}

	private void SallPrice_ButtonClick_1(object sender, ButtonPressedEventArgs e)
	{
		SetPrice(SallPrice.Text);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmSearchOrder.FrmSearchTransferAllBranch));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions5 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions6 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions7 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions8 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions9 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions10 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled10 = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ItemName = new DevExpress.XtraEditors.TextEdit();
		this.LabelItemName = new System.Windows.Forms.Label();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.TxtAllQuantity = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtAvergCost = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtLastCost = new DevExpress.XtraEditors.ButtonEdit();
		this.label16 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtSellPrice3 = new DevExpress.XtraEditors.ButtonEdit();
		this.label14 = new System.Windows.Forms.Label();
		this.TxtSellPrice2 = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtLocation = new DevExpress.XtraEditors.TextEdit();
		this.label25 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.TxtSellPrice1 = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.label5 = new System.Windows.Forms.Label();
		this.LastPrice = new DevExpress.XtraEditors.ButtonEdit();
		this.LastQuantity = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtLastCustomer = new DevExpress.XtraEditors.TextEdit();
		this.label34 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.label6 = new System.Windows.Forms.Label();
		this.SallPrice = new DevExpress.XtraEditors.ButtonEdit();
		this.Quantity = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtCurrentCustomer = new DevExpress.XtraEditors.TextEdit();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ItemName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtAllQuantity.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAvergCost.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLastCost.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSellPrice3.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSellPrice2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSellPrice1.Properties).BeginInit();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.LastPrice.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.LastQuantity.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLastCustomer.Properties).BeginInit();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SallPrice.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Quantity.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCurrentCustomer.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.ItemName);
		this.groupBox1.Controls.Add(this.LabelItemName);
		this.groupBox1.Controls.Add(this.gridEX1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.ItemName, "ItemName");
		this.ItemName.Name = "ItemName";
		this.ItemName.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("ItemName.Properties.Appearance.Font");
		this.ItemName.Properties.Appearance.ForeColor = System.Drawing.Color.Green;
		this.ItemName.Properties.Appearance.Options.UseFont = true;
		this.ItemName.Properties.Appearance.Options.UseForeColor = true;
		this.ItemName.Properties.Appearance.Options.UseTextOptions = true;
		this.ItemName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.ItemName.Properties.AppearanceDisabled.Options.UseTextOptions = true;
		this.ItemName.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.LabelItemName, "LabelItemName");
		this.LabelItemName.Name = "LabelItemName";
		resources.ApplyResources(this.gridEX1, "gridEX1");
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.EmptyRows = true;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX1.DoubleClick += new System.EventHandler(gridEX1_DoubleClick);
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.TxtAllQuantity);
		this.groupBox2.Controls.Add(this.TxtAvergCost);
		this.groupBox2.Controls.Add(this.TxtLastCost);
		this.groupBox2.Controls.Add(this.label16);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.TxtSellPrice3);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.TxtSellPrice2);
		this.groupBox2.Controls.Add(this.TxtLocation);
		this.groupBox2.Controls.Add(this.label25);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.TxtSellPrice1);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.TxtAllQuantity, "TxtAllQuantity");
		this.TxtAllQuantity.Name = "TxtAllQuantity";
		this.TxtAllQuantity.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtAllQuantity.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAllQuantity.Properties.Appearance.Font");
		this.TxtAllQuantity.Properties.Appearance.Options.UseBackColor = true;
		this.TxtAllQuantity.Properties.Appearance.Options.UseFont = true;
		this.TxtAllQuantity.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAllQuantity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.TxtAllQuantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtAllQuantity.Properties.Buttons"), resources.GetString("TxtAllQuantity.Properties.Buttons1"), (int)resources.GetObject("TxtAllQuantity.Properties.Buttons2"), (bool)resources.GetObject("TxtAllQuantity.Properties.Buttons3"), (bool)resources.GetObject("TxtAllQuantity.Properties.Buttons4"), (bool)resources.GetObject("TxtAllQuantity.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtAllQuantity.Properties.Buttons6"), resources.GetObject("TxtAllQuantity.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtAllQuantity.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtAllQuantity.Properties.Buttons9"))
		});
		this.TxtAllQuantity.Properties.ReadOnly = true;
		resources.ApplyResources(this.TxtAvergCost, "TxtAvergCost");
		this.TxtAvergCost.Name = "TxtAvergCost";
		this.TxtAvergCost.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtAvergCost.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAvergCost.Properties.Appearance.Font");
		this.TxtAvergCost.Properties.Appearance.Options.UseBackColor = true;
		this.TxtAvergCost.Properties.Appearance.Options.UseFont = true;
		this.TxtAvergCost.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAvergCost.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.TxtAvergCost.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtAvergCost.Properties.Buttons"), resources.GetString("TxtAvergCost.Properties.Buttons1"), (int)resources.GetObject("TxtAvergCost.Properties.Buttons2"), (bool)resources.GetObject("TxtAvergCost.Properties.Buttons3"), (bool)resources.GetObject("TxtAvergCost.Properties.Buttons4"), (bool)resources.GetObject("TxtAvergCost.Properties.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("TxtAvergCost.Properties.Buttons6"), resources.GetObject("TxtAvergCost.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtAvergCost.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtAvergCost.Properties.Buttons9"))
		});
		this.TxtAvergCost.Properties.ReadOnly = true;
		resources.ApplyResources(this.TxtLastCost, "TxtLastCost");
		this.TxtLastCost.Name = "TxtLastCost";
		this.TxtLastCost.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtLastCost.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtLastCost.Properties.Appearance.Font");
		this.TxtLastCost.Properties.Appearance.Options.UseBackColor = true;
		this.TxtLastCost.Properties.Appearance.Options.UseFont = true;
		this.TxtLastCost.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLastCost.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		this.TxtLastCost.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtLastCost.Properties.Buttons"), resources.GetString("TxtLastCost.Properties.Buttons1"), (int)resources.GetObject("TxtLastCost.Properties.Buttons2"), (bool)resources.GetObject("TxtLastCost.Properties.Buttons3"), (bool)resources.GetObject("TxtLastCost.Properties.Buttons4"), (bool)resources.GetObject("TxtLastCost.Properties.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("TxtLastCost.Properties.Buttons6"), resources.GetObject("TxtLastCost.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtLastCost.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtLastCost.Properties.Buttons9"))
		});
		this.TxtLastCost.Properties.ReadOnly = true;
		this.TxtLastCost.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtLastCost_ButtonClick_1);
		resources.ApplyResources(this.label16, "label16");
		this.label16.Name = "label16";
		resources.ApplyResources(this.label13, "label13");
		this.label13.Name = "label13";
		resources.ApplyResources(this.TxtSellPrice3, "TxtSellPrice3");
		this.TxtSellPrice3.Name = "TxtSellPrice3";
		this.TxtSellPrice3.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtSellPrice3.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtSellPrice3.Properties.Appearance.Font");
		this.TxtSellPrice3.Properties.Appearance.Options.UseBackColor = true;
		this.TxtSellPrice3.Properties.Appearance.Options.UseFont = true;
		this.TxtSellPrice3.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSellPrice3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.TxtSellPrice3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtSellPrice3.Properties.Buttons"), resources.GetString("TxtSellPrice3.Properties.Buttons1"), (int)resources.GetObject("TxtSellPrice3.Properties.Buttons2"), (bool)resources.GetObject("TxtSellPrice3.Properties.Buttons3"), (bool)resources.GetObject("TxtSellPrice3.Properties.Buttons4"), (bool)resources.GetObject("TxtSellPrice3.Properties.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("TxtSellPrice3.Properties.Buttons6"), resources.GetObject("TxtSellPrice3.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtSellPrice3.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtSellPrice3.Properties.Buttons9"))
		});
		this.TxtSellPrice3.Properties.ReadOnly = true;
		this.TxtSellPrice3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtSellPrice3_ButtonClick);
		resources.ApplyResources(this.label14, "label14");
		this.label14.Name = "label14";
		resources.ApplyResources(this.TxtSellPrice2, "TxtSellPrice2");
		this.TxtSellPrice2.Name = "TxtSellPrice2";
		this.TxtSellPrice2.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtSellPrice2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtSellPrice2.Properties.Appearance.Font");
		this.TxtSellPrice2.Properties.Appearance.Options.UseBackColor = true;
		this.TxtSellPrice2.Properties.Appearance.Options.UseFont = true;
		this.TxtSellPrice2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSellPrice2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions5");
		this.TxtSellPrice2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtSellPrice2.Properties.Buttons"), resources.GetString("TxtSellPrice2.Properties.Buttons1"), (int)resources.GetObject("TxtSellPrice2.Properties.Buttons2"), (bool)resources.GetObject("TxtSellPrice2.Properties.Buttons3"), (bool)resources.GetObject("TxtSellPrice2.Properties.Buttons4"), (bool)resources.GetObject("TxtSellPrice2.Properties.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("TxtSellPrice2.Properties.Buttons6"), resources.GetObject("TxtSellPrice2.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtSellPrice2.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtSellPrice2.Properties.Buttons9"))
		});
		this.TxtSellPrice2.Properties.ReadOnly = true;
		this.TxtSellPrice2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtSellPrice2_ButtonClick);
		resources.ApplyResources(this.TxtLocation, "TxtLocation");
		this.TxtLocation.Name = "TxtLocation";
		this.TxtLocation.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtLocation.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtLocation.Properties.Appearance.Font");
		this.TxtLocation.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
		this.TxtLocation.Properties.Appearance.Options.UseBackColor = true;
		this.TxtLocation.Properties.Appearance.Options.UseFont = true;
		this.TxtLocation.Properties.Appearance.Options.UseForeColor = true;
		this.TxtLocation.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLocation.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtLocation.Properties.ReadOnly = true;
		resources.ApplyResources(this.label25, "label25");
		this.label25.Name = "label25";
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.label11, "label11");
		this.label11.Name = "label11";
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.TxtSellPrice1, "TxtSellPrice1");
		this.TxtSellPrice1.Name = "TxtSellPrice1";
		this.TxtSellPrice1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtSellPrice1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtSellPrice1.Properties.Appearance.Font");
		this.TxtSellPrice1.Properties.Appearance.Options.UseBackColor = true;
		this.TxtSellPrice1.Properties.Appearance.Options.UseFont = true;
		this.TxtSellPrice1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSellPrice1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions6");
		this.TxtSellPrice1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtSellPrice1.Properties.Buttons"), resources.GetString("TxtSellPrice1.Properties.Buttons1"), (int)resources.GetObject("TxtSellPrice1.Properties.Buttons2"), (bool)resources.GetObject("TxtSellPrice1.Properties.Buttons3"), (bool)resources.GetObject("TxtSellPrice1.Properties.Buttons4"), (bool)resources.GetObject("TxtSellPrice1.Properties.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("TxtSellPrice1.Properties.Buttons6"), resources.GetObject("TxtSellPrice1.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtSellPrice1.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtSellPrice1.Properties.Buttons9"))
		});
		this.TxtSellPrice1.Properties.ReadOnly = true;
		this.TxtSellPrice1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtSellPrice1_ButtonClick);
		resources.ApplyResources(this.groupBox4, "groupBox4");
		this.groupBox4.Controls.Add(this.label5);
		this.groupBox4.Controls.Add(this.LastPrice);
		this.groupBox4.Controls.Add(this.LastQuantity);
		this.groupBox4.Controls.Add(this.TxtLastCustomer);
		this.groupBox4.Controls.Add(this.label34);
		this.groupBox4.Controls.Add(this.label2);
		this.groupBox4.ForeColor = System.Drawing.Color.Red;
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.TabStop = false;
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.LastPrice, "LastPrice");
		this.LastPrice.Name = "LastPrice";
		this.LastPrice.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.LastPrice.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("LastPrice.Properties.Appearance.Font");
		this.LastPrice.Properties.Appearance.Options.UseBackColor = true;
		this.LastPrice.Properties.Appearance.Options.UseFont = true;
		this.LastPrice.Properties.Appearance.Options.UseTextOptions = true;
		this.LastPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions7, "editorButtonImageOptions7");
		this.LastPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("LastPrice.Properties.Buttons"), resources.GetString("LastPrice.Properties.Buttons1"), (int)resources.GetObject("LastPrice.Properties.Buttons2"), (bool)resources.GetObject("LastPrice.Properties.Buttons3"), (bool)resources.GetObject("LastPrice.Properties.Buttons4"), (bool)resources.GetObject("LastPrice.Properties.Buttons5"), editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, resources.GetString("LastPrice.Properties.Buttons6"), resources.GetObject("LastPrice.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("LastPrice.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("LastPrice.Properties.Buttons9"))
		});
		this.LastPrice.Properties.ReadOnly = true;
		this.LastPrice.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(LastPrice_ButtonClick_1);
		resources.ApplyResources(this.LastQuantity, "LastQuantity");
		this.LastQuantity.Name = "LastQuantity";
		this.LastQuantity.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.LastQuantity.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("LastQuantity.Properties.Appearance.Font");
		this.LastQuantity.Properties.Appearance.Options.UseBackColor = true;
		this.LastQuantity.Properties.Appearance.Options.UseFont = true;
		this.LastQuantity.Properties.Appearance.Options.UseTextOptions = true;
		this.LastQuantity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions8");
		this.LastQuantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("LastQuantity.Properties.Buttons"), resources.GetString("LastQuantity.Properties.Buttons1"), (int)resources.GetObject("LastQuantity.Properties.Buttons2"), (bool)resources.GetObject("LastQuantity.Properties.Buttons3"), (bool)resources.GetObject("LastQuantity.Properties.Buttons4"), (bool)resources.GetObject("LastQuantity.Properties.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("LastQuantity.Properties.Buttons6"), resources.GetObject("LastQuantity.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("LastQuantity.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("LastQuantity.Properties.Buttons9"))
		});
		this.LastQuantity.Properties.ReadOnly = true;
		resources.ApplyResources(this.TxtLastCustomer, "TxtLastCustomer");
		this.TxtLastCustomer.Name = "TxtLastCustomer";
		this.TxtLastCustomer.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtLastCustomer.Properties.Appearance.Font");
		this.TxtLastCustomer.Properties.Appearance.Options.UseFont = true;
		this.TxtLastCustomer.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLastCustomer.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.label34, "label34");
		this.label34.Name = "label34";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.groupBox5, "groupBox5");
		this.groupBox5.Controls.Add(this.label6);
		this.groupBox5.Controls.Add(this.SallPrice);
		this.groupBox5.Controls.Add(this.Quantity);
		this.groupBox5.Controls.Add(this.TxtCurrentCustomer);
		this.groupBox5.Controls.Add(this.label7);
		this.groupBox5.Controls.Add(this.label8);
		this.groupBox5.ForeColor = System.Drawing.Color.Green;
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.TabStop = false;
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.SallPrice, "SallPrice");
		this.SallPrice.Name = "SallPrice";
		this.SallPrice.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.SallPrice.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("SallPrice.Properties.Appearance.Font");
		this.SallPrice.Properties.Appearance.Options.UseBackColor = true;
		this.SallPrice.Properties.Appearance.Options.UseFont = true;
		this.SallPrice.Properties.Appearance.Options.UseTextOptions = true;
		this.SallPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions9, "editorButtonImageOptions9");
		this.SallPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("SallPrice.Properties.Buttons"), resources.GetString("SallPrice.Properties.Buttons1"), (int)resources.GetObject("SallPrice.Properties.Buttons2"), (bool)resources.GetObject("SallPrice.Properties.Buttons3"), (bool)resources.GetObject("SallPrice.Properties.Buttons4"), (bool)resources.GetObject("SallPrice.Properties.Buttons5"), editorButtonImageOptions9, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance9, appearanceHovered9, appearancePressed9, appearanceDisabled9, resources.GetString("SallPrice.Properties.Buttons6"), resources.GetObject("SallPrice.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("SallPrice.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("SallPrice.Properties.Buttons9"))
		});
		this.SallPrice.Properties.ReadOnly = true;
		this.SallPrice.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(SallPrice_ButtonClick_1);
		resources.ApplyResources(this.Quantity, "Quantity");
		this.Quantity.Name = "Quantity";
		this.Quantity.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.Quantity.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("Quantity.Properties.Appearance.Font");
		this.Quantity.Properties.Appearance.Options.UseBackColor = true;
		this.Quantity.Properties.Appearance.Options.UseFont = true;
		this.Quantity.Properties.Appearance.Options.UseTextOptions = true;
		this.Quantity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(editorButtonImageOptions10, "editorButtonImageOptions10");
		this.Quantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("Quantity.Properties.Buttons"), resources.GetString("Quantity.Properties.Buttons1"), (int)resources.GetObject("Quantity.Properties.Buttons2"), (bool)resources.GetObject("Quantity.Properties.Buttons3"), (bool)resources.GetObject("Quantity.Properties.Buttons4"), (bool)resources.GetObject("Quantity.Properties.Buttons5"), editorButtonImageOptions10, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance10, appearanceHovered10, appearancePressed10, appearanceDisabled10, resources.GetString("Quantity.Properties.Buttons6"), resources.GetObject("Quantity.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("Quantity.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("Quantity.Properties.Buttons9"))
		});
		this.Quantity.Properties.ReadOnly = true;
		resources.ApplyResources(this.TxtCurrentCustomer, "TxtCurrentCustomer");
		this.TxtCurrentCustomer.Name = "TxtCurrentCustomer";
		this.TxtCurrentCustomer.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCurrentCustomer.Properties.Appearance.Font");
		this.TxtCurrentCustomer.Properties.Appearance.Options.UseFont = true;
		this.TxtCurrentCustomer.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCurrentCustomer.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmSearchTransferAllBranch";
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ItemName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtAllQuantity.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAvergCost.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLastCost.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSellPrice3.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSellPrice2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocation.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSellPrice1.Properties).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.LastPrice.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.LastQuantity.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLastCustomer.Properties).EndInit();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.SallPrice.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Quantity.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCurrentCustomer.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


