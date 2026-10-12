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
using DevExpress.XtraEditors.Repository;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Reports.Stores;

public class FrmRPTasferByGroup : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_Group ORD = new Class_Group();

	private GTSdbEntities db = new GTSdbEntities();

	private IContainer components = null;

	private GroupBox groupBox22;

	private GroupBox groupBox23;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	private GroupBox groupBox24;

	private System.Windows.Forms.ComboBox comboGroup;

	private Label label2;

	private TimeEdit TxtTime2;

	private TimeEdit TxtTime1;

	private DateEdit TxtDate2;

	private Label label3;

	private DateEdit TxtDate1;

	private Label label21;

	public FrmRPTasferByGroup()
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
		BindComboBox();
		TxtDate1.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
		TxtDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");
		TxtTime1.Text = "00:00:00";
		TxtTime2.Text = "23:59:59";
	}

	public void BindComboBox()
	{
		try
		{
			comboGroup.ValueMember = "ID";
			comboGroup.DisplayMember = "Name";
			comboGroup.DataSource = db.Item_Groups.ToList();
			comboGroup.SelectedValue = 1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void groupBox24_Enter(object sender, EventArgs e)
	{
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 54))
		{
			if (!(TxtDate1.Text != ""))
			{
				return;
			}
			SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["Get_Report_QuantityOrdersPurches"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Get_Report_QuantityOrdersPurches", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@Date1", Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text));
			sqlCommand.Parameters.AddWithValue("@Date2", Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text));
			sqlCommand.Parameters.AddWithValue("@BranchID", Convert.ToInt32(LoginDetails.BranchID));
			sqlCommand.Parameters.AddWithValue("@Category_ID", Convert.ToInt32(comboGroup.SelectedValue));
			sqlDataAdapter.Fill(dataSetALL.Get_Report_QuantityOrdersPurches);
			sqlConnection.Close();
			if (dataSetALL.Tables["Get_Report_QuantityOrdersPurches"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\ReportTasreef.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportTasreef.mrt");
				}
				stiReport.RegData(dataSetALL.Get_Report_QuantityOrdersPurches);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				stiReport["Date1"] = Convert.ToDateTime(TxtDate1.Text);
				stiReport["Date2"] = Convert.ToDateTime(TxtDate2.Text);
				stiReport["GroupName"] = comboGroup.Text;
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

	private void comboGroup_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.Stores.FrmRPTasferByGroup));
		this.groupBox22 = new System.Windows.Forms.GroupBox();
		this.groupBox23 = new System.Windows.Forms.GroupBox();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox24 = new System.Windows.Forms.GroupBox();
		this.TxtTime2 = new DevExpress.XtraEditors.TimeEdit();
		this.TxtTime1 = new DevExpress.XtraEditors.TimeEdit();
		this.TxtDate2 = new DevExpress.XtraEditors.DateEdit();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtDate1 = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.comboGroup = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox22.SuspendLayout();
		this.groupBox23.SuspendLayout();
		this.groupBox24.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).BeginInit();
		base.SuspendLayout();
		this.groupBox22.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox22.Controls.Add(this.groupBox23);
		this.groupBox22.Controls.Add(this.groupBox24);
		this.groupBox22.Location = new System.Drawing.Point(5, 8);
		this.groupBox22.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox22.Name = "groupBox22";
		this.groupBox22.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox22.Size = new System.Drawing.Size(494, 207);
		this.groupBox22.TabIndex = 5;
		this.groupBox22.TabStop = false;
		this.groupBox23.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox23.Controls.Add(this.BtnContinue);
		this.groupBox23.Controls.Add(this.BtnExit);
		this.groupBox23.Location = new System.Drawing.Point(12, 131);
		this.groupBox23.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox23.Name = "groupBox23";
		this.groupBox23.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox23.Size = new System.Drawing.Size(470, 61);
		this.groupBox23.TabIndex = 5;
		this.groupBox23.TabStop = false;
		this.BtnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnContinue.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Location = new System.Drawing.Point(227, 19);
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Size = new System.Drawing.Size(143, 32);
		this.BtnContinue.TabIndex = 10;
		this.BtnContinue.Text = "استمرار";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(21, 19);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(143, 32);
		this.BtnExit.TabIndex = 9;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.groupBox24.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox24.Controls.Add(this.TxtTime2);
		this.groupBox24.Controls.Add(this.TxtTime1);
		this.groupBox24.Controls.Add(this.TxtDate2);
		this.groupBox24.Controls.Add(this.label3);
		this.groupBox24.Controls.Add(this.TxtDate1);
		this.groupBox24.Controls.Add(this.label21);
		this.groupBox24.Controls.Add(this.comboGroup);
		this.groupBox24.Controls.Add(this.label2);
		this.groupBox24.Location = new System.Drawing.Point(12, 18);
		this.groupBox24.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox24.Name = "groupBox24";
		this.groupBox24.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox24.Size = new System.Drawing.Size(470, 109);
		this.groupBox24.TabIndex = 0;
		this.groupBox24.TabStop = false;
		this.groupBox24.Enter += new System.EventHandler(groupBox24_Enter);
		this.TxtTime2.EditValue = new System.DateTime(2020, 6, 22, 23, 59, 59, 0);
		this.TxtTime2.Location = new System.Drawing.Point(21, 75);
		this.TxtTime2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtTime2.Name = "TxtTime2";
		this.TxtTime2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTime2.Properties.Appearance.Options.UseFont = true;
		this.TxtTime2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTime2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTime2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtTime2.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
		this.TxtTime2.Size = new System.Drawing.Size(143, 22);
		this.TxtTime2.TabIndex = 204;
		this.TxtTime1.EditValue = new System.DateTime(2020, 6, 22, 0, 0, 0, 0);
		this.TxtTime1.Location = new System.Drawing.Point(21, 47);
		this.TxtTime1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtTime1.Name = "TxtTime1";
		this.TxtTime1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTime1.Properties.Appearance.Options.UseFont = true;
		this.TxtTime1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTime1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTime1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtTime1.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
		this.TxtTime1.Size = new System.Drawing.Size(143, 22);
		this.TxtTime1.TabIndex = 203;
		this.TxtDate2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDate2.EditValue = null;
		this.TxtDate2.Location = new System.Drawing.Point(168, 75);
		this.TxtDate2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtDate2.Name = "TxtDate2";
		this.TxtDate2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtDate2.Properties.Appearance.Options.UseFont = true;
		this.TxtDate2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate2.Properties.Mask.EditMask = "yyyy/MM/dd";
		this.TxtDate2.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate2.Properties.TodayDate = new System.DateTime(2019, 6, 17, 21, 19, 56, 0);
		this.TxtDate2.Size = new System.Drawing.Size(203, 22);
		this.TxtDate2.TabIndex = 200;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(381, 78);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(58, 16);
		this.label3.TabIndex = 202;
		this.label3.Text = "الي تاريخ";
		this.TxtDate1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDate1.EditValue = null;
		this.TxtDate1.Location = new System.Drawing.Point(168, 47);
		this.TxtDate1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtDate1.Name = "TxtDate1";
		this.TxtDate1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.5f);
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
		this.TxtDate1.Size = new System.Drawing.Size(203, 22);
		this.TxtDate1.TabIndex = 199;
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label21.Location = new System.Drawing.Point(381, 50);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(55, 16);
		this.label21.TabIndex = 201;
		this.label21.Text = "من تاريخ";
		this.comboGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.comboGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.comboGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f);
		this.comboGroup.FormattingEnabled = true;
		this.comboGroup.Location = new System.Drawing.Point(21, 21);
		this.comboGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.comboGroup.Name = "comboGroup";
		this.comboGroup.Size = new System.Drawing.Size(350, 21);
		this.comboGroup.TabIndex = 16;
		this.comboGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(comboGroup_KeyDown);
		this.label2.AutoSize = true;
		this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label2.Location = new System.Drawing.Point(375, 24);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(74, 13);
		this.label2.TabIndex = 15;
		this.label2.Text = "اختر المجموعة";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(504, 226);
		base.Controls.Add(this.groupBox22);
		base.IconOptions.Image = (System.Drawing.Image)resources.GetObject("FrmRPTasferByGroup.IconOptions.Image");
		base.Name = "FrmRPTasferByGroup";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تقرير التصريف";
		this.groupBox22.ResumeLayout(false);
		this.groupBox23.ResumeLayout(false);
		this.groupBox24.ResumeLayout(false);
		this.groupBox24.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


