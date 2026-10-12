using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Restaurant;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.FrmRestaurant.LuckUp;

public class FrmSearchAdds : XtraForm
{
	private AutoCompleteStringCollection NameSugg = new AutoCompleteStringCollection();

	private Class_Add Itm = new Class_Add();

	public DataTable myTable = new DataTable("gridEX1");

	private List<string> cust = null;

	private IContainer components = null;

	public GridEX gridEX1;

	private SimpleButton BtnOk;

	public FrmSearchAdds(string ItemSearchText = null)
	{
		InitializeComponent();
		CrateGrid();
		itemsearch2();
		BindGrid();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		LoginDetails.ItemSearchAll = new List<string>();
		Close();
	}

	private void BtnOk_Click(object sender, EventArgs e)
	{
		NextSelct();
		LoginDetails.ItemSearchAll2 = LoginDetails.ItemSearchAll;
		LoginDetails.ItemSearchAll = new List<string>();
		Close();
	}

	private void itemsearch2()
	{
		LoginDetails.ItemSearchAll2 = new List<string>();
		LoginDetails.ItemSearchAll = new List<string>();
	}

	public void CrateGrid()
	{
		gridEX1.ColumnAutoResize = true;
		myTable.Columns.Add("#", typeof(bool));
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
		gridEX1.RootTable.Columns["الاسم الانجليزي"].Visible = false;
		gridEX1.RootTable.Columns["#"].Width = 30;
		gridEX1.RootTable.Columns["#"].TextAlignment = TextAlignment.Center;
		gridEX1.RootTable.Columns["كود الصنف"].TextAlignment = TextAlignment.Center;
		gridEX1.RootTable.Columns["الاسم العربي"].TextAlignment = TextAlignment.Center;
		gridEX1.RootTable.Columns["الاسم الانجليزي"].TextAlignment = TextAlignment.Center;
		gridEX1.RootTable.Columns["سعر البيع"].TextAlignment = TextAlignment.Center;
		gridEX1.RootTable.Columns["ItemId"].EditType = EditType.NoEdit;
		gridEX1.RootTable.Columns["الاسم الانجليزي"].EditType = EditType.NoEdit;
		gridEX1.RootTable.Columns["سعر البيع"].EditType = EditType.NoEdit;
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
			foreach (Item_Add item in Itm.GetAll())
			{
				myTable.Rows.Add(0, item.ID, item.ID, item.Name, item.NameEN, item.SellPrice);
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
	}

	private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BindGrid();
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
	}

	private void gridEX1_DoubleClick(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmRestaurant.LuckUp.FrmSearchAdds));
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.BtnOk = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		base.SuspendLayout();
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
		resources.ApplyResources(this.BtnOk, "BtnOk");
		this.BtnOk.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOk.ImageOptions.Image");
		this.BtnOk.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnOk.Name = "BtnOk";
		this.BtnOk.Click += new System.EventHandler(BtnOk_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnOk);
		base.Controls.Add(this.gridEX1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmSearchAdds";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmSearchItems1_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		base.ResumeLayout(false);
	}
}


