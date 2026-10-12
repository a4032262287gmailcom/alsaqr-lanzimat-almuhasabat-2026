using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Orders;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Items.FrmSearch;

public class FrmSearchItems1 : XtraForm
{
	private AutoCompleteStringCollection NameSugg = new AutoCompleteStringCollection();

	private Class_Items Itm = new Class_Items();

	public DataTable myTable = new DataTable("gridEX1");

	private List<string> cust = null;

	private Class_Orders ORD = new Class_Orders();

	private bool isAllChecked = false;

	private IContainer components = null;

	private Label label3;

	private Label label2;

	public GridEX gridEX1;

	private SeparatorControl separatorControl1;

	private Label label1;

	private System.Windows.Forms.ComboBox CBCat;

	private System.Windows.Forms.ComboBox CBClass;

	private SimpleButton BtnOk;

	private SimpleButton BtnExit;

	private SimpleButton BtnSearch;

	private SimpleButton BtnSearchSave;

	private TextBox TxtSearch;

	public FrmSearchItems1(string ItemSearchText = null)
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
		FillItemCat();
		FillItemClass();
		if (LoginDetails.ISCheckSearchItem)
		{
			CrateGrid2();
		}
		else
		{
			CrateGrid();
		}
		if (!string.IsNullOrWhiteSpace(ItemSearchText))
		{
			TxtSearch.Text = ItemSearchText;
		}
		TxtSearch.Focus();
		TxtSearch.Select();
		itemsearch2();
		BtnSearch_Click(null, null);
	}

	public void FillItemCat()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Sn", typeof(int));
			dataTable.Columns.Add("name", typeof(string));
			foreach (Item_Groups item in Itm.GetCats().ToList())
			{
				dataTable.Rows.Add(item.ID, item.Name);
			}
			DataRow dataRow = dataTable.NewRow();
			dataRow.ItemArray = new object[2] { 0, "الكل" };
			dataTable.Rows.InsertAt(dataRow, 0);
			CBCat.ValueMember = "Sn";
			CBCat.DisplayMember = "name";
			CBCat.DataSource = dataTable;
			CBCat.SelectedValue = LoginDetails.CBGroupIDItem;
			return;
		}
		DataTable dataTable2 = new DataTable();
		dataTable2.Columns.Add("Sn", typeof(int));
		dataTable2.Columns.Add("name", typeof(string));
		foreach (Item_Groups item2 in Itm.GetCats().ToList())
		{
			dataTable2.Rows.Add(item2.ID, item2.Name);
		}
		DataRow dataRow2 = dataTable2.NewRow();
		dataRow2.ItemArray = new object[2] { 0, "All Groups" };
		dataTable2.Rows.InsertAt(dataRow2, 0);
		CBCat.ValueMember = "Sn";
		CBCat.DisplayMember = "name";
		CBCat.DataSource = dataTable2;
		CBCat.SelectedValue = LoginDetails.CBGroupIDItem;
	}

	public void FillItemClass()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Sn", typeof(int));
			dataTable.Columns.Add("name", typeof(string));
			foreach (Item_Class item in Itm.GetItemCalass().ToList())
			{
				dataTable.Rows.Add(item.ID, item.Name);
			}
			DataRow dataRow = dataTable.NewRow();
			dataRow.ItemArray = new object[2] { 0, "الكل" };
			dataTable.Rows.InsertAt(dataRow, 0);
			CBClass.ValueMember = "Sn";
			CBClass.DisplayMember = "name";
			CBClass.DataSource = dataTable;
			return;
		}
		DataTable dataTable2 = new DataTable();
		dataTable2.Columns.Add("Sn", typeof(int));
		dataTable2.Columns.Add("name", typeof(string));
		foreach (Item_Class item2 in Itm.GetItemCalass().ToList())
		{
			dataTable2.Rows.Add(item2.ID, item2.Name);
		}
		DataRow dataRow2 = dataTable2.NewRow();
		dataRow2.ItemArray = new object[2] { 0, "All Classes" };
		dataTable2.Rows.InsertAt(dataRow2, 0);
		CBClass.ValueMember = "Sn";
		CBClass.DisplayMember = "name";
		CBClass.DataSource = dataTable2;
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		LoginDetails.ItemSearchAll = new List<string>();
		Close();
	}

	private async void BtnOk_Click(object sender, EventArgs e)
	{
		Task task = Task.Run(delegate
		{
			NextSelct();
		});
		if (await Task.WhenAny(task, Task.Delay(TimeSpan.FromMinutes(2.0))) == task)
		{
			LoginDetails.ItemSearchAll2 = LoginDetails.ItemSearchAll;
			LoginDetails.ItemSearchAll = new List<string>();
			Close();
		}
		else
		{
			MessageBox.Show("انتهت المهلة ولم يتم تنفيذ العملية", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void BtnSearch_Click(object sender, EventArgs e)
	{
		BindGrid();
	}

	private void BtnSearchSave_Click(object sender, EventArgs e)
	{
		if (LoginDetails.ISPurches)
		{
			foreach (DataRow row in myTable.Rows)
			{
				if (row["#"].ToString() == "True")
				{
					if (LoginDetails.Lang == "ar-EG")
					{
						LoginDetails.ItemSearchAll.Add(row["كود الصنف"].ToString());
					}
					else
					{
						LoginDetails.ItemSearchAll.Add(row["Item Code"].ToString());
					}
				}
			}
			BindGridPurches();
		}
		else
		{
			BindGrid();
		}
	}

	private void itemsearch2()
	{
		LoginDetails.ItemSearchAll2 = new List<string>();
		LoginDetails.ItemSearchAll = new List<string>();
	}

	public void CrateGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			myTable.Columns.Add("#", typeof(bool));
			myTable.Columns.Add("ظ…", typeof(int));
			myTable.Columns.Add("ItemId", typeof(int));
			myTable.Columns.Add("كود الصنف", typeof(string));
			myTable.Columns.Add("الاسم العربي", typeof(string));
			myTable.Columns.Add("الاسم الانجليزي", typeof(string));
			myTable.Columns.Add("سعر البيع", typeof(string));
			gridEX1.SetDataBinding(myTable, "");
			gridEX1.AllowEdit = InheritableBoolean.True;
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.RowHeight = 28;
			gridEX1.RootTable.Columns["ItemId"].Visible = false;
			gridEX1.RootTable.Columns["ظ…"].Width = 30;
			gridEX1.RootTable.Columns["#"].Width = 30;
			gridEX1.RootTable.Columns["ظ…"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["#"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["كود الصنف"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الاسم العربي"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الاسم العربي"].Width = 300;
			gridEX1.RootTable.Columns["الاسم الانجليزي"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["سعر البيع"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["ظ…"].EditType = EditType.NoEdit;
			gridEX1.RootTable.Columns["ItemId"].EditType = EditType.NoEdit;
			gridEX1.RootTable.Columns["سعر البيع"].EditType = EditType.NoEdit;
			GridEXColumn gridEXColumn = gridEX1.RootTable.Columns["#"];
			gridEXColumn.ColumnType = ColumnType.CheckBox;
			gridEXColumn.EditType = EditType.CheckBox;
			gridEXColumn.AllowSort = false;
			gridEX1.ColumnHeaderClick += gridEX1_ColumnHeaderClick;
		}
		else
		{
			gridEX1.ColumnAutoResize = true;
			myTable.Columns.Add("#", typeof(bool));
			myTable.Columns.Add("SN", typeof(int));
			myTable.Columns.Add("ItemId", typeof(int));
			myTable.Columns.Add("Item Code", typeof(string));
			myTable.Columns.Add("Item Arabic", typeof(string));
			myTable.Columns.Add("Item English", typeof(string));
			myTable.Columns.Add("Price", typeof(string));
			gridEX1.SetDataBinding(myTable, "");
			gridEX1.AllowEdit = InheritableBoolean.True;
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.RowHeight = 28;
			gridEX1.RootTable.Columns["ItemId"].Visible = false;
			gridEX1.RootTable.Columns["SN"].Width = 30;
			gridEX1.RootTable.Columns["#"].Width = 30;
			gridEX1.RootTable.Columns["SN"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["#"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["Item Code"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["Item Arabic"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["Item English"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["Price"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["SN"].EditType = EditType.NoEdit;
			gridEX1.RootTable.Columns["ItemId"].EditType = EditType.NoEdit;
			gridEX1.RootTable.Columns["Item English"].EditType = EditType.NoEdit;
			gridEX1.RootTable.Columns["Price"].EditType = EditType.NoEdit;
		}
	}

	public void CrateGrid2()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			myTable.Columns.Add("#", typeof(bool));
			myTable.Columns.Add("ظ…", typeof(int));
			myTable.Columns.Add("ItemId", typeof(int));
			myTable.Columns.Add("كود الصنف", typeof(string));
			myTable.Columns.Add("الاسم العربي", typeof(string));
			myTable.Columns.Add("الاسم الانجليزي", typeof(string));
			myTable.Columns.Add("سعر البيع", typeof(string));
			gridEX1.SetDataBinding(myTable, "");
			gridEX1.AllowEdit = InheritableBoolean.False;
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.RowHeight = 28;
			gridEX1.RootTable.Columns["#"].Visible = false;
			gridEX1.RootTable.Columns["ItemId"].Visible = false;
			gridEX1.RootTable.Columns["ظ…"].Width = 30;
			gridEX1.RootTable.Columns["#"].Width = 30;
			gridEX1.RootTable.Columns["ظ…"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["#"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["كود الصنف"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الاسم العربي"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["الاسم العربي"].Width = 300;
			gridEX1.RootTable.Columns["الاسم الانجليزي"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["سعر البيع"].TextAlignment = TextAlignment.Center;
		}
		else
		{
			gridEX1.ColumnAutoResize = true;
			myTable.Columns.Add("#", typeof(bool));
			myTable.Columns.Add("SN", typeof(int));
			myTable.Columns.Add("ItemId", typeof(int));
			myTable.Columns.Add("Item Code", typeof(string));
			myTable.Columns.Add("Item Arabic", typeof(string));
			myTable.Columns.Add("Item English", typeof(string));
			myTable.Columns.Add("Price", typeof(string));
			gridEX1.SetDataBinding(myTable, "");
			gridEX1.AllowEdit = InheritableBoolean.False;
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.RowHeight = 28;
			gridEX1.RootTable.Columns["#"].Visible = false;
			gridEX1.RootTable.Columns["ItemId"].Visible = false;
			gridEX1.RootTable.Columns["SN"].Width = 30;
			gridEX1.RootTable.Columns["#"].Width = 30;
			gridEX1.RootTable.Columns["SN"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["#"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["Item Code"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["Item Arabic"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["Item English"].TextAlignment = TextAlignment.Center;
			gridEX1.RootTable.Columns["Price"].TextAlignment = TextAlignment.Center;
		}
	}

	public void NextSelct()
	{
		try
		{
			if (LoginDetails.ISCheckSearchItem)
			{
				int position = gridEX1.SelectedItems[0].Position;
				int itemSearchID = Convert.ToInt32(gridEX1.GetRow(position).Cells[2].Text);
				LoginDetails.ItemSearchID = itemSearchID;
				return;
			}
			foreach (DataRow row in myTable.Rows)
			{
				if (row["#"].ToString() == "True")
				{
					if (LoginDetails.Lang == "ar-EG")
					{
						LoginDetails.ItemSearchAll.Add(row["كود الصنف"].ToString());
					}
					else
					{
						LoginDetails.ItemSearchAll.Add(row["Item Code"].ToString());
					}
				}
			}
		}
		catch (Exception)
		{
			MessageBox.Show("من فضلك قم بتحديد الصنف ثم حاول مرة اخري", "تسجيل الدخول", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	public void BindGrid()
	{
		try
		{
			myTable.Clear();
			foreach (Item_Items item in Itm.GetItemsBy(Convert.ToInt32(CBCat.SelectedValue), Convert.ToInt32(CBClass.SelectedValue), TxtSearch.Text).Take(5000))
			{
				myTable.Rows.Add(0, 1, item.ItemId, item.Item_code, item.item_Name, item.item_Name_English, item.SellPriceSmall.ToString());
			}
			int num = 1;
			foreach (DataRow row in myTable.Rows)
			{
				if (LoginDetails.Lang == "ar-EG")
				{
					row["ظ…"] = num;
				}
				else
				{
					row["SN"] = num;
				}
				num++;
			}
		}
		catch (Exception)
		{
		}
	}

	public void BindGridPurches()
	{
		try
		{
			myTable.Clear();
			foreach (Get_All_Items_Result item in Itm.GetItms().Take(5000))
			{
				myTable.Rows.Add(0, 1, item.ItemId, item.Item_code, item.item_Name, item.item_Name_English, item.SellPriceSmall.ToString());
			}
			int num = 1;
			foreach (DataRow row in myTable.Rows)
			{
				if (LoginDetails.Lang == "ar-EG")
				{
					row["ظ…"] = num;
				}
				else
				{
					row["SN"] = num;
				}
				num++;
			}
			if (myTable.Rows.Count == 0)
			{
				MessageBox.Show(" نعتذر، لايوجد أصناف تحتوي علي هذا الكود ( " + TxtSearch.Text + " ) \r\n الرجاء التأكد من الكود ثم حاول مرة أخري.", "مستكشف الاصناف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		catch (Exception)
		{
		}
	}

	private void gridEX1_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		NextSelct();
		LoginDetails.ItemSearchAll2 = new List<string>();
		LoginDetails.ItemSearchAll2 = LoginDetails.ItemSearchAll;
		LoginDetails.ItemSearchAll = new List<string>();
		Close();
	}

	private void FrmSearchItems1_FormClosing(object sender, FormClosingEventArgs e)
	{
		LoginDetails.ItemSearchAll = new List<string>();
		LoginDetails.ISCheckSearchItem = false;
		LoginDetails.CBGroupIDItem = Convert.ToInt32(CBCat.SelectedValue);
	}

	private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (!LoginDetails.ISPurches)
			{
				BindGridPurches();
			}
			else
			{
				BindGrid();
			}
		}
	}

	private void CBCat_SelectedIndexChanged(object sender, EventArgs e)
	{
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

	private void Frms_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F2")
		{
			BtnOk.Focus();
			NextSelct();
			LoginDetails.ItemSearchAll2 = LoginDetails.ItemSearchAll;
			LoginDetails.ItemSearchAll = new List<string>();
			Close();
		}
		else if (e.KeyCode.ToString() == "F3")
		{
			BindGrid();
		}
		else
		{
			if (!(e.KeyCode.ToString() == "F4"))
			{
				return;
			}
			if (LoginDetails.ISPurches)
			{
				foreach (DataRow row in myTable.Rows)
				{
					if (row["#"].ToString() == "True")
					{
						if (LoginDetails.Lang == "ar-EG")
						{
							LoginDetails.ItemSearchAll.Add(row["كود الصنف"].ToString());
						}
						else
						{
							LoginDetails.ItemSearchAll.Add(row["Item Code"].ToString());
						}
					}
				}
				BindGridPurches();
			}
			else
			{
				BindGrid();
			}
		}
	}

	private void AutoComplate()
	{
		using GTSdbEntities gTSdbEntities = new GTSdbEntities();
		TxtSearch.AutoCompleteCustomSource = NameSugg;
		TxtSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
		TxtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
		foreach (Item_Items item in (IEnumerable<Item_Items>)gTSdbEntities.Item_Items)
		{
			NameSugg.Add(item.item_Name);
		}
		TxtSearch.AutoCompleteCustomSource = NameSugg;
	}

	private void gridEX1_DoubleClick(object sender, EventArgs e)
	{
		NextSelct();
		LoginDetails.ItemSearchAll2 = LoginDetails.ItemSearchAll;
		LoginDetails.ItemSearchAll = new List<string>();
		Close();
	}

	private void gridEX1_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
	{
		if (e.Column.Key != "#")
		{
			return;
		}
		isAllChecked = !isAllChecked;
		foreach (DataRow row in myTable.Rows)
		{
			row["#"] = isAllChecked;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Items.FrmSearch.FrmSearchItems1));
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
		this.label1 = new System.Windows.Forms.Label();
		this.CBCat = new System.Windows.Forms.ComboBox();
		this.CBClass = new System.Windows.Forms.ComboBox();
		this.BtnOk = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSearchSave = new DevExpress.XtraEditors.SimpleButton();
		this.TxtSearch = new System.Windows.Forms.TextBox();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.gridEX1, "gridEX1");
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.AlternatingRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
		this.gridEX1.DynamicFiltering = true;
		this.gridEX1.EmptyRows = true;
		this.gridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
		this.gridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
		this.gridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.PreviewRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.PreviewRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TreeLineColor = System.Drawing.Color.Blue;
		this.gridEX1.UseCompatibleTextRendering = true;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.gridEX1.DoubleClick += new System.EventHandler(gridEX1_DoubleClick);
		this.gridEX1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseDoubleClick);
		resources.ApplyResources(this.separatorControl1, "separatorControl1");
		this.separatorControl1.Name = "separatorControl1";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.CBCat, "CBCat");
		this.CBCat.FormattingEnabled = true;
		this.CBCat.Name = "CBCat";
		this.CBCat.SelectedIndexChanged += new System.EventHandler(CBCat_SelectedIndexChanged);
		resources.ApplyResources(this.CBClass, "CBClass");
		this.CBClass.FormattingEnabled = true;
		this.CBClass.Name = "CBClass";
		resources.ApplyResources(this.BtnOk, "BtnOk");
		this.BtnOk.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOk.ImageOptions.Image");
		this.BtnOk.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnOk.Name = "BtnOk";
		this.BtnOk.Click += new System.EventHandler(BtnOk_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.BtnSearch, "BtnSearch");
		this.BtnSearch.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSearch.ImageOptions.Image");
		this.BtnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSearch.Name = "BtnSearch";
		this.BtnSearch.Click += new System.EventHandler(BtnSearch_Click);
		resources.ApplyResources(this.BtnSearchSave, "BtnSearchSave");
		this.BtnSearchSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSearchSave.ImageOptions.Image");
		this.BtnSearchSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSearchSave.Name = "BtnSearchSave";
		this.BtnSearchSave.Click += new System.EventHandler(BtnSearchSave_Click);
		resources.ApplyResources(this.TxtSearch, "TxtSearch");
		this.TxtSearch.Name = "TxtSearch";
		this.TxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtSearch_KeyDown);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.TxtSearch);
		base.Controls.Add(this.BtnSearchSave);
		base.Controls.Add(this.BtnSearch);
		base.Controls.Add(this.BtnOk);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.CBClass);
		base.Controls.Add(this.CBCat);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.gridEX1);
		base.Controls.Add(this.separatorControl1);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmSearchItems1";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmSearchItems1_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


