using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Main.Setting;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Account.Search;

public class FrmSearchAccount : XtraForm
{
	private Class_DefualtAccount Stting = new Class_DefualtAccount();

	public DataTable myTable = new DataTable("gridEX1");

	public int CustCode;

	public int Account_Number;

	public string AccountName;

	public string FrmName = LoginDetails.FrmName;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	private TextEdit TxtSearch;

	private SeparatorControl separatorControl1;

	private Label label1;

	private GridEX gridEX1;

	public FrmSearchAccount()
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
		CreateGrid();
		TxtSearch.Text = LoginDetails.AccountNameSearch;
		BindGrid(TxtSearch.Text);
	}

	private void CreateGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			myTable.Columns.Add("#", typeof(bool));
			myTable.Columns.Add("كود", typeof(int));
			myTable.Columns.Add("رقم الحساب", typeof(int));
			myTable.Columns.Add("اسم الحساب", typeof(string));
			gridEX1.SetDataBinding(myTable, "");
			gridEX1.AllowEdit = InheritableBoolean.True;
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["كود"].EditType = EditType.NoEdit;
			gridEX1.RootTable.Columns["رقم الحساب"].EditType = EditType.NoEdit;
			gridEX1.RootTable.Columns["اسم الحساب"].EditType = EditType.NoEdit;
			gridEX1.RootTable.Columns["#"].Width = 30;
			gridEX1.RootTable.Columns["كود"].Visible = false;
		}
		else
		{
			gridEX1.ColumnAutoResize = true;
			myTable.Columns.Add("#", typeof(bool));
			myTable.Columns.Add("Code", typeof(int));
			myTable.Columns.Add("Account Number", typeof(int));
			myTable.Columns.Add("Account Name", typeof(string));
			gridEX1.SetDataBinding(myTable, "");
			gridEX1.AllowEdit = InheritableBoolean.True;
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["Code"].EditType = EditType.NoEdit;
			gridEX1.RootTable.Columns["Account Number"].EditType = EditType.NoEdit;
			gridEX1.RootTable.Columns["Account Name"].EditType = EditType.NoEdit;
			gridEX1.RootTable.Columns["#"].Width = 30;
			gridEX1.RootTable.Columns["Code"].Visible = false;
		}
	}

	public void BindGrid(string Code)
	{
		myTable.Clear();
		if (LoginDetails.Lang == "ar-EG")
		{
			foreach (Select_SearchAccountReceipt_Result item in Stting.SearchAcct(Code))
			{
				myTable.Rows.Add(0, item.ID, item.Account_No, item.Account_Name);
			}
			return;
		}
		foreach (Select_SearchAccountReceipt_Result item2 in Stting.SearchAcct(Code))
		{
			myTable.Rows.Add(0, item2.ID, item2.Account_No, item2.E_Account_Name);
		}
	}

	private void BtnNext_Click(object sender, EventArgs e)
	{
		LoginDetails.ItemSearchAll = new List<string>();
		foreach (DataRow row in myTable.Rows)
		{
			if (row["#"].ToString() == "True")
			{
				if (LoginDetails.Lang == "ar-EG")
				{
					LoginDetails.ItemSearchAll.Add(row["رقم الحساب"].ToString());
				}
				else
				{
					LoginDetails.ItemSearchAll.Add(row["Account Number"].ToString());
				}
			}
		}
		LoginDetails.ItemSearchAll2 = new List<string>();
		LoginDetails.ItemSearchAll2 = LoginDetails.ItemSearchAll;
		LoginDetails.ItemSearchAll = new List<string>();
		Close();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BindGrid(TxtSearch.Text);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Account.Search.FrmSearchAccount));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.TxtSearch = new DevExpress.XtraEditors.TextEdit();
		this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSearch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.gridEX1);
		this.groupBox1.Controls.Add(this.TxtSearch);
		this.groupBox1.Controls.Add(this.separatorControl1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.gridEX1, "gridEX1");
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.AlternatingRowFormatStyle.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.gridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		resources.ApplyResources(this.TxtSearch, "TxtSearch");
		this.TxtSearch.Name = "TxtSearch";
		this.TxtSearch.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtSearch.Properties.Appearance.Font");
		this.TxtSearch.Properties.Appearance.Options.UseFont = true;
		this.TxtSearch.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSearch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
		this.TxtSearch.Properties.ContextImageOptions.Image = (System.Drawing.Image)resources.GetObject("TxtSearch.Properties.ContextImageOptions.Image");
		this.TxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtSearch_KeyDown);
		resources.ApplyResources(this.separatorControl1, "separatorControl1");
		this.separatorControl1.Name = "separatorControl1";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnNext_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmSearchAccount";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSearch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


