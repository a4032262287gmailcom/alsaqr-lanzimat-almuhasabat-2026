using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Main.Setting;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.FrmsMain.Search;

public class FrmSearchAccount : XtraForm
{
	private Class_DefualtAccount Stting = new Class_DefualtAccount();

	private Class_AccountCard Acct = new Class_AccountCard();

	public int CustCode;

	public int Account_Number;

	public string AccountName;

	public string FrmName = LoginDetails.FrmName;

	private IContainer components = null;

	private GroupBox groupBox1;

	public GridEX gridEX1;

	private GroupBox groupBox2;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

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
		BindGrid();
	}

	public void BindGrid()
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridEX1.ColumnAutoResize = true;
			DataTable dataTable = new DataTable("gridEX1");
			dataTable.Columns.Add("التسلسل", typeof(int));
			dataTable.Columns.Add("كود", typeof(int));
			dataTable.Columns.Add("رقم الحساب", typeof(int));
			dataTable.Columns.Add("اسم الحساب", typeof(string));
			dataTable.Columns.Add("مجموعة الحساب", typeof(string));
			if (FrmName == "FrmReceiptsCash")
			{
				foreach (Select_SearchAccountReceipt_Result item in (from x in Stting.GetSearchAccount()
					where x.Account_No >= 1101010 && x.Account_No <= 1101053
					select x).ToList())
				{
					dataTable.Rows.Add(item.SN, item.ID, item.Account_No, item.Account_Name, item.AccountType);
				}
				foreach (Select_SearchAccountReceipt_Result item2 in (from x in Stting.GetSearchAccount()
					where x.Account_No >= 1101111 && x.Account_No <= 1101153
					select x).ToList())
				{
					dataTable.Rows.Add(item2.SN, item2.ID, item2.Account_No, item2.Account_Name, item2.AccountType);
				}
			}
			else if (FrmName == "FrmCashOnly")
			{
				foreach (Select_SearchAccountReceipt_Result item3 in (from x in Stting.GetSearchAccount()
					where x.Account_No >= 1101010 && x.Account_No <= 1101053
					select x).ToList())
				{
					dataTable.Rows.Add(item3.SN, item3.ID, item3.Account_No, item3.Account_Name, item3.AccountType);
				}
			}
			else if (FrmName == "FrmReceiptsCheckBank")
			{
				foreach (Select_SearchAccountReceipt_Result item4 in (from x in Stting.GetSearchAccount()
					where x.Account_No >= 1101111 && x.Account_No <= 1101153
					select x).ToList())
				{
					dataTable.Rows.Add(item4.SN, item4.ID, item4.Account_No, item4.Account_Name, item4.AccountType);
				}
			}
			else if (FrmName == "FrmMovementsDaily")
			{
				int BoxAcct = Acct.GetAccountNum(LoginDetails.Acc_CashBox);
				foreach (Select_SearchAccountReceipt_Result item5 in (from x in Stting.GetSearchAccount()
					where x.Account_No > 1101009 && x.Account_No < 1101099 && x.Account_No != BoxAcct
					select x).ToList())
				{
					dataTable.Rows.Add(item5.SN, item5.ID, item5.Account_No, item5.Account_Name, item5.AccountType);
				}
				foreach (Select_SearchAccountReceipt_Result item6 in (from x in Stting.GetSearchAccount()
					where x.Account_No >= 1101111 && x.Account_No <= 1101153
					select x).ToList())
				{
					dataTable.Rows.Add(item6.SN, item6.ID, item6.Account_No, item6.Account_Name, item6.AccountType);
				}
			}
			else if (FrmName == "FrmMizan")
			{
				foreach (Select_SearchAccount_Result item7 in Stting.GetSearchDefualtAccount())
				{
					dataTable.Rows.Add(item7.SN, item7.ID, item7.Account_No, item7.Account_Name, item7.AccountType);
				}
			}
			else
			{
				foreach (Select_SearchAccountReceipt_Result item8 in Stting.GetSearchAccount())
				{
					dataTable.Rows.Add(item8.SN, item8.ID, item8.Account_No, item8.Account_Name, item8.AccountType);
				}
			}
			gridEX1.SetDataBinding(dataTable, "");
			gridEX1.RetrieveStructure();
			gridEX1.RootTable.Columns["التسلسل"].Width = 50;
			gridEX1.RootTable.RowHeight = 28;
			gridEX1.RootTable.Columns["التسلسل"].RightToLeft = RightToLeft.No;
			gridEX1.RootTable.Columns["كود"].Visible = false;
			return;
		}
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable2 = new DataTable("gridEX1");
		dataTable2.Columns.Add("SN", typeof(int));
		dataTable2.Columns.Add("Code", typeof(int));
		dataTable2.Columns.Add("Account Number", typeof(int));
		dataTable2.Columns.Add("Account Name", typeof(string));
		dataTable2.Columns.Add("Account Group", typeof(string));
		if (FrmName == "FrmReceiptsCash")
		{
			foreach (Select_SearchAccountReceipt_Result item9 in (from x in Stting.GetSearchAccount()
				where x.Account_No >= 1101010 && x.Account_No <= 1101053
				select x).ToList())
			{
				dataTable2.Rows.Add(item9.SN, item9.ID, item9.Account_No, item9.E_Account_Name, item9.AccountType);
			}
			foreach (Select_SearchAccountReceipt_Result item10 in (from x in Stting.GetSearchAccount()
				where x.Account_No >= 1101111 && x.Account_No <= 1101153
				select x).ToList())
			{
				dataTable2.Rows.Add(item10.SN, item10.ID, item10.Account_No, item10.E_Account_Name, item10.AccountType);
			}
		}
		else if (FrmName == "FrmCashOnly")
		{
			foreach (Select_SearchAccountReceipt_Result item11 in (from x in Stting.GetSearchAccount()
				where x.Account_No >= 1101010 && x.Account_No <= 1101053
				select x).ToList())
			{
				dataTable2.Rows.Add(item11.SN, item11.ID, item11.Account_No, item11.Account_Name, item11.AccountType);
			}
		}
		else if (FrmName == "FrmReceiptsCheckBank")
		{
			foreach (Select_SearchAccountReceipt_Result item12 in (from x in Stting.GetSearchAccount()
				where x.Account_No >= 1101111 && x.Account_No <= 1101153
				select x).ToList())
			{
				dataTable2.Rows.Add(item12.SN, item12.ID, item12.Account_No, item12.Account_Name, item12.AccountType);
			}
		}
		else if (FrmName == "FrmMovementsDaily")
		{
			int BoxAcct2 = Acct.GetAccountNum(LoginDetails.Acc_CashBox);
			foreach (Select_SearchAccountReceipt_Result item13 in (from x in Stting.GetSearchAccount()
				where x.Account_No > 1101009 && x.Account_No < 1101099 && x.Account_No != BoxAcct2
				select x).ToList())
			{
				dataTable2.Rows.Add(item13.SN, item13.ID, item13.Account_No, item13.E_Account_Name, item13.AccountType);
			}
			foreach (Select_SearchAccountReceipt_Result item14 in (from x in Stting.GetSearchAccount()
				where x.Account_No >= 1101111 && x.Account_No <= 1101153
				select x).ToList())
			{
				dataTable2.Rows.Add(item14.SN, item14.ID, item14.Account_No, item14.E_Account_Name, item14.AccountType);
			}
		}
		else if (FrmName == "FrmMizan")
		{
			foreach (Select_SearchAccount_Result item15 in Stting.GetSearchDefualtAccount())
			{
				dataTable2.Rows.Add(item15.SN, item15.ID, item15.Account_No, item15.Account_Name, item15.AccountType);
			}
		}
		else
		{
			foreach (Select_SearchAccountReceipt_Result item16 in Stting.GetSearchAccount())
			{
				dataTable2.Rows.Add(item16.SN, item16.ID, item16.Account_No, item16.E_Account_Name, item16.AccountType);
			}
		}
		gridEX1.SetDataBinding(dataTable2, "");
		gridEX1.RetrieveStructure();
		gridEX1.RootTable.Columns["SN"].Width = 50;
		gridEX1.RootTable.RowHeight = 28;
		gridEX1.RootTable.Columns["SN"].RightToLeft = RightToLeft.No;
		gridEX1.RootTable.Columns["Code"].Visible = false;
	}

	private void BtnNext_Click(object sender, EventArgs e)
	{
		ChooseAccount();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void gridEX1_DoubleClick(object sender, EventArgs e)
	{
		ChooseAccount();
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			ChooseAccount();
		}
	}

	private void ChooseAccount()
	{
		int position = gridEX1.SelectedItems[0].Position;
		CustCode = Convert.ToInt32(gridEX1.GetRow(position).Cells[1].Text);
		AccountName = Convert.ToString(gridEX1.GetRow(position).Cells[3].Text);
		Account_Number = Convert.ToInt32(gridEX1.GetRow(position).Cells[2].Text);
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.Search.FrmSearchAccount));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.gridEX1);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.gridEX1, "gridEX1");
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.AlternatingRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
		this.gridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
		this.gridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
		this.gridEX1.FilterRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
		this.gridEX1.FocusCellFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FocusCellFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxInfoFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.LinkFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.LinkFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.NewRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.NewRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Black;
		this.gridEX1.PreviewRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.PreviewRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedInactiveFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedInactiveFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TableHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.DoubleClick += new System.EventHandler(gridEX1_DoubleClick);
		this.gridEX1.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
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
		base.Name = "FrmSearchAccount";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}


