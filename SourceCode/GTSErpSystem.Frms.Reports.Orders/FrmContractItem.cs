using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Contract;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Contract.Search;
using GTSErpSystem.Frms.FrmsMain;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Reports.Orders;

public class FrmContractItem : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_Orders ORD = new Class_Orders();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private Class_Stores LGN = new Class_Stores();

	private Class_ScaffoldsContract ORD1 = new Class_ScaffoldsContract();

	private IContainer components = null;

	private SimpleButton BtnExit;

	private SimpleButton BtnContinue;

	private Label label20;

	private GroupBox groupBox1;

	private ButtonEdit TxtCode;

	public FrmContractItem()
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
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		if (TxtCode.Text == "")
		{
			MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.Tables["Scaffold_Permission"].Clear();
		SqlCommand sqlCommand = new SqlCommand("Get_ExitItems_ByContractID", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlCommand.Parameters.AddWithValue("@ContractID", Convert.ToInt32(TxtCode.Text));
		sqlDataAdapter.Fill(dataSetALL.Scaffold_Permission);
		sqlConnection.Close();
		if (dataSetALL.Tables["Scaffold_Permission"].Rows.Count > 0)
		{
			StiReport stiReport = new StiReport();
			if (LoginDetails.Lang == "ar-EG")
			{
				stiReport.Load(Application.StartupPath + "\\Reports\\ReportAllContractRentItems.mrt");
			}
			else
			{
				stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportAllContractRentItems.mrt");
			}
			stiReport.RegData(dataSetALL.Scaffold_Permission);
			stiReport.Dictionary.Synchronize();
			stiReport.Compile();
			stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
			stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
			stiReport["VatNumComp"] = LoginDetails.VatNum;
			stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
			stiReport["Address"] = LoginDetails.Address;
			stiReport["Phone"] = LoginDetails.Phone;
			stiReport["Bank"] = LoginDetails.CompanyBank;
			stiReport.Render();
			stiReport.Show(dialogForm: true);
			FrmReportGetOrder frmReportGetOrder = new FrmReportGetOrder();
			frmReportGetOrder.stiViewerControl1.Report = stiReport;
		}
		else
		{
			MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void TxtDate2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnContinue.Focus();
		}
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchScaffoldsContract frmSearchScaffoldsContract = new FrmSearchScaffoldsContract();
			frmSearchScaffoldsContract.ShowDialog();
			if (frmSearchScaffoldsContract.CustCode > 0)
			{
				int custCode = frmSearchScaffoldsContract.CustCode;
				Scaffolds_Contract scaffolds_Contract = ORD1.GetPurchases(custCode).FirstOrDefault();
				TxtCode.Text = scaffolds_Contract.PurBranchID.ToString();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.Orders.FrmContractItem));
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.label20 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnContinue, "BtnContinue");
		this.BtnContinue.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnContinue.Appearance.Font");
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		this.BtnContinue.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label20, "label20");
		this.label20.Name = "label20";
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.TxtCode, "TxtCode");
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCode.Properties.Appearance.Font");
		this.TxtCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCode.Properties.Appearance.Options.UseFont = true;
		this.TxtCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCode.Properties.ReadOnly = true;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.BtnContinue);
		base.Controls.Add(this.BtnExit);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmContractItem";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


