using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Reports.Stores;

public class FrmRPStoresGard : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private Class_Stores LGN = new Class_Stores();

	private IContainer components = null;

	private GroupBox groupBox22;

	private GroupBox groupBox23;

	private GroupBox groupBox24;

	private RadioButton RadioAll;

	private RadioButton RadioC;

	private RadioButton RadioB;

	private RadioButton RadioA;

	private System.Windows.Forms.ComboBox comboStore;

	private Label label16;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	public FrmRPStoresGard()
	{
		InitializeComponent();
		BindComboStore();
	}

	public void BindComboStore()
	{
		try
		{
			comboStore.DataSource = LGN.GetAll();
			comboStore.DisplayMember = "Store_Name";
			comboStore.ValueMember = "ID";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void CombNoDelete_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		if (!CheckPermession.CheckUserPage(LoginDetails.GroupID, 10))
		{
			return;
		}
		if (comboStore.Text != "")
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["StoreTable"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Select_ItemQuantity_Only", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@StoreID", Convert.ToInt32(comboStore.SelectedValue));
			if (RadioA.Checked)
			{
				sqlCommand.Parameters.AddWithValue("@ID ", 1);
				sqlDataAdapter.Fill(dataSetALL.StoreTable);
				sqlConnection.Close();
			}
			else if (RadioB.Checked)
			{
				sqlCommand.Parameters.AddWithValue("@ID ", 0);
				sqlDataAdapter.Fill(dataSetALL.StoreTable);
				sqlConnection.Close();
			}
			else if (RadioC.Checked)
			{
				sqlCommand.Parameters.AddWithValue("@ID ", -1);
				sqlDataAdapter.Fill(dataSetALL.StoreTable);
				sqlConnection.Close();
			}
			else if (RadioAll.Checked)
			{
				sqlCommand.Parameters.AddWithValue("@ID ", 2);
				sqlDataAdapter.Fill(dataSetALL.StoreTable);
				sqlConnection.Close();
			}
			if (dataSetALL.Tables["StoreTable"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				stiReport.Load(Application.StartupPath + "\\Reports\\Reportgard.mrt");
				stiReport.RegData(dataSetALL.StoreTable);
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
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.Stores.FrmRPStoresGard));
		this.groupBox22 = new System.Windows.Forms.GroupBox();
		this.groupBox23 = new System.Windows.Forms.GroupBox();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox24 = new System.Windows.Forms.GroupBox();
		this.RadioAll = new System.Windows.Forms.RadioButton();
		this.RadioC = new System.Windows.Forms.RadioButton();
		this.RadioB = new System.Windows.Forms.RadioButton();
		this.RadioA = new System.Windows.Forms.RadioButton();
		this.comboStore = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.groupBox22.SuspendLayout();
		this.groupBox23.SuspendLayout();
		this.groupBox24.SuspendLayout();
		base.SuspendLayout();
		this.groupBox22.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox22.Controls.Add(this.groupBox23);
		this.groupBox22.Controls.Add(this.groupBox24);
		this.groupBox22.Location = new System.Drawing.Point(15, 15);
		this.groupBox22.Name = "groupBox22";
		this.groupBox22.Size = new System.Drawing.Size(576, 268);
		this.groupBox22.TabIndex = 4;
		this.groupBox22.TabStop = false;
		this.groupBox23.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox23.Controls.Add(this.BtnContinue);
		this.groupBox23.Controls.Add(this.BtnExit);
		this.groupBox23.Location = new System.Drawing.Point(14, 178);
		this.groupBox23.Name = "groupBox23";
		this.groupBox23.Size = new System.Drawing.Size(548, 75);
		this.groupBox23.TabIndex = 5;
		this.groupBox23.TabStop = false;
		this.BtnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnContinue.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Location = new System.Drawing.Point(265, 23);
		this.BtnContinue.Margin = new System.Windows.Forms.Padding(4);
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Size = new System.Drawing.Size(167, 40);
		this.BtnContinue.TabIndex = 10;
		this.BtnContinue.Text = "استمرار";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(24, 23);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(167, 40);
		this.BtnExit.TabIndex = 9;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.groupBox24.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox24.Controls.Add(this.RadioAll);
		this.groupBox24.Controls.Add(this.RadioC);
		this.groupBox24.Controls.Add(this.RadioB);
		this.groupBox24.Controls.Add(this.RadioA);
		this.groupBox24.Controls.Add(this.comboStore);
		this.groupBox24.Controls.Add(this.label16);
		this.groupBox24.Location = new System.Drawing.Point(14, 22);
		this.groupBox24.Name = "groupBox24";
		this.groupBox24.Size = new System.Drawing.Size(548, 150);
		this.groupBox24.TabIndex = 0;
		this.groupBox24.TabStop = false;
		this.RadioAll.AutoSize = true;
		this.RadioAll.Location = new System.Drawing.Point(412, 110);
		this.RadioAll.Name = "RadioAll";
		this.RadioAll.Size = new System.Drawing.Size(93, 21);
		this.RadioAll.TabIndex = 12;
		this.RadioAll.TabStop = true;
		this.RadioAll.Text = "كل الكميات";
		this.RadioAll.UseVisualStyleBackColor = true;
		this.RadioC.AutoSize = true;
		this.RadioC.Location = new System.Drawing.Point(46, 74);
		this.RadioC.Name = "RadioC";
		this.RadioC.Size = new System.Drawing.Size(105, 21);
		this.RadioC.TabIndex = 11;
		this.RadioC.TabStop = true;
		this.RadioC.Text = "اقل من الصفر";
		this.RadioC.UseVisualStyleBackColor = true;
		this.RadioB.AutoSize = true;
		this.RadioB.Location = new System.Drawing.Point(217, 74);
		this.RadioB.Name = "RadioB";
		this.RadioB.Size = new System.Drawing.Size(105, 21);
		this.RadioB.TabIndex = 10;
		this.RadioB.TabStop = true;
		this.RadioB.Text = "تساوي الصفر";
		this.RadioB.UseVisualStyleBackColor = true;
		this.RadioA.AutoSize = true;
		this.RadioA.Checked = true;
		this.RadioA.Location = new System.Drawing.Point(398, 74);
		this.RadioA.Name = "RadioA";
		this.RadioA.Size = new System.Drawing.Size(107, 21);
		this.RadioA.TabIndex = 9;
		this.RadioA.TabStop = true;
		this.RadioA.Text = "اكبر من الصفر";
		this.RadioA.UseVisualStyleBackColor = true;
		this.comboStore.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboStore.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.comboStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.comboStore.FormattingEnabled = true;
		this.comboStore.Location = new System.Drawing.Point(24, 27);
		this.comboStore.Name = "comboStore";
		this.comboStore.Size = new System.Drawing.Size(408, 24);
		this.comboStore.TabIndex = 8;
		this.comboStore.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombNoDelete_KeyPress);
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(452, 30);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(80, 17);
		this.label16.TabIndex = 0;
		this.label16.Text = "اسم المخزن";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(607, 295);
		base.Controls.Add(this.groupBox22);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmRPStoresGard";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تقرير الجرد";
		this.groupBox22.ResumeLayout(false);
		this.groupBox23.ResumeLayout(false);
		this.groupBox24.ResumeLayout(false);
		this.groupBox24.PerformLayout();
		base.ResumeLayout(false);
	}
}


